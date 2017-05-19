Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.Services
Imports System.Xml
Imports Newtonsoft.Json
Imports B2P.Integration.TriPOS
Imports B2P.Integration.TriPOS.Entities

Public Class PinPad : Inherits SiteBasePage

#Region " ::: Control Event Handlers ::: "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim errMsg As String = String.Empty

        Try
            ' Make sure we have a token
            If Utility.IsNullOrEmpty(BLL.SessionManager.Token) Then
                psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)

                Exit Sub
            Else
                ' Instantiate empty message panel so we can send messages back from AJAX calls to page methods
                psmErrorMessage.ToggleStatusMessage(String.Empty, StatusMessageType.None, StatusMessageSize.Normal, True, True)
            End If

            ' Add client side javascript attributes to the various form controls
            RegisterClientJs()

            ' Gets the last active Bootstrap tab before the postback
            hdfTabName.Value = Request.Form(hdfTabName.UniqueID)


            ' Build the payment UI objects
            If BLL.SessionManager.IsInitialized Then
                If Not Me.IsPostBack Then
                    litCardImages.Text = Utility.BuildAllowedCardImages(BLL.SessionManager.Cart, CardImageSize.Medium)    '.Replace("_small.", "_med.")

                    InitializePaymentOptions()

                    ' Gets the last active Bootstrap tab before the postback
                    'hdfTabName.Value = "pnlTabCredit" 'Request.Form(hdfTabName.UniqueID)

                    BuildCartSummary()
                End If
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub btnClearCardInfo_Click(sender As Object, e As EventArgs) Handles btnClearCardInfo.Click
        pccEnterCreditCardInfo.Clear()
    End Sub

    Private Sub btnSubmitCardInfo_Click(sender As Object, e As EventArgs) Handles btnSubmitCardInfo.Click
        Dim card As B2P.Common.Objects.CreditCard = Nothing
        Dim ccpr As B2P.Payment.CreditCardPayment.CreditCardPaymentResults = Nothing
        Dim receipt As Entities.Receipt = Nothing
        Dim office As B2P.Client.Office = Nothing
        Dim transMsg As String = String.Empty
        Dim errMsg As String = String.Empty
        Dim trans As B2P.Common.TransactionDetail = Nothing

        Me.CheckSession()

        Try
            If ValidateForm("CC") Then
                card = GetCreditCard()

                If card IsNot Nothing Then

                    If Not BLL.SessionManager.PaymentMade Then

                        ' Process the payment
                        ccpr = ProcessPaymentCredit()

                        ' The result doesn't always return a message, so return the stringified
                        ' result enum type instead (i.e., Success, Failed, ErrorOccured, etc.)
                        transMsg = Utility.IIf(Of String)(Not Utility.IsNullOrEmpty(ccpr.Message), ccpr.Message, String.Join(" ", Regex.Split(ccpr.Result.ToString, "(?=[A-Z])")))

                        ' Check the payment results
                        If ccpr.Result = B2P.Payment.CreditCardPayment.CreditCardPaymentResults.Results.Success Then
                            BLL.SessionManager.PaymentMade = True
                            BLL.SessionManager.ConfirmationNumber = ccpr.ConfirmationNumber.TrimStart(CChar("0"))
                            Session("AuthorizationCode") = ccpr.AuthorizationCode
                            ' Do the postback
                            postBackCC()


                            ' Build the receipt object
                            ' Get the client office
                            office = New B2P.Client.Office(BLL.SessionManager.OfficeID)

                            ' Create receipt and store in session
                            If office.Found Then
                                receipt = New Entities.Receipt(ccpr, card, BLL.SessionManager.Token, BLL.SessionManager.Cart, office)
                            Else
                                receipt = New Entities.Receipt(ccpr, card, BLL.SessionManager.Token, BLL.SessionManager.Cart,
                                                                   B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode))
                            End If

                            ' Get the fee amount based on the card type
                            Select Case Utility.GetPaymentCardType(card.BINNumber)
                                Case "C"
                                    receipt.ConvenienceFee = BLL.SessionManager.Cart.CreditCardFee
                                Case "D", "U"
                                    receipt.ConvenienceFee = BLL.SessionManager.Cart.PinDebitFee
                            End Select

                            ' Make sure to add the cart total with the tokenized fee payment
                            receipt.TransactionAmount = BLL.SessionManager.Cart.TotalAmount + receipt.ConvenienceFee

                            ' Set the receipt transaction date
                            trans = New B2P.Common.TransactionDetail(Convert.ToInt32(ccpr.ConfirmationNumber))
                            receipt.TransactionDateTime = trans.TransactionDate.ToString

                            ' Save receipt to the database
                            SaveTransactionReceipt(receipt, BLL.SessionManager.ConfirmationNumber, BLL.SessionManager.Token)

                            ' Save receipt for printing
                            BLL.SessionManager.TransactionReceipt = receipt

                            ' Send them to the confirmation page
                            Response.Redirect("/payment/preConfirmation.aspx", False)

                        Else
                            BLL.SessionManager.PaymentMade = False
                            Session("AuthorizationCode") = ""

                            psmErrorMessage.ToggleStatusMessage(transMsg, StatusMessageType.Danger, True, True)

                            pccEnterCreditCardInfo.Clear()
                        End If

                    End If

                Else
                    ' Missing card info
                    BLL.SessionManager.PaymentMade = False

                    psmErrorMessage.ToggleStatusMessage(GetGlobalResourceObject("WebResources", "ErrMsgMissingCardInfo").ToString, StatusMessageType.Danger, True, True)
                End If

            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Errors/Error.aspx", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Finally
            'Clean up a bit
            If receipt IsNot Nothing Then
                receipt = Nothing
            End If

            If office IsNot Nothing Then
                office = Nothing
            End If

            If card IsNot Nothing Then
                card = Nothing
            End If
        End Try
    End Sub

#End Region

#Region " ::: Methods ::: "

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="response"></param>
    ''' <param name="paymentType"></param>
    ''' <param name="errorMessage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function AddFailedTransaction(ByVal response As Entities.SaleResponse, ByVal paymentType As Entities.PaymentType, ByVal feeResponse As B2P.TriPOS.Utility.Payments.FeeResponse, ByVal errorMessage As String) As Boolean
        Dim items As B2P.Payment.PaymentBase.TransactionItems = Nothing
        Dim feeCalcType As B2P.Payment.FeeCalculation.PaymentTypes
        Dim firstName As String = String.Empty
        Dim lastName As String = String.Empty
        Dim retVal As Boolean
        Dim errMsg As String = String.Empty

        Try
            ' Get the cart items -- non-tax for now, but will need to revisit to handle tax items
            Select Case paymentType
                Case Entities.PaymentType.Credit
                    feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.CreditCard

                Case Entities.PaymentType.Debit
                    feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.PinDebit

                    ' May need to address this at a later date
                    'If response.PinVerified OrElse BLL.SessionManager.UseBinLookup Then
                    '    feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.PinDebit
                    'Else
                    '    feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.DebitCard
                    'End If
            End Select

            ' Get the cart items -- non-tax for now, but will need to revisit to handle tax items
            items = BLL.SessionManager.Cart.GetCartItems(feeCalcType, B2P.ShoppingCart.Cart.CartItemsTypes.Nontax)

            ' Get the name from the cardholder name in the response
            If response.CardHolderName.IndexOf("/"c) > -1 Then
                firstName = response.CardHolderName.Split("/"c)(1).Trim
                lastName = response.CardHolderName.Split("/"c)(0).Trim
            Else
                lastName = response.CardHolderName.Trim
            End If

            ' Create the connection
            Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand()
                    cmd.CommandText = "ap_LogFailedCCTransaction"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 300
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@ClientCode", SqlDbType.VarChar, 10).Value = BLL.SessionManager.ClientCode
                    cmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar, 200).Value = errorMessage.Trim
                    cmd.Parameters.Add("@PayeeFirstName", SqlDbType.VarChar, 20).Value = firstName                                                   'logInfo.Owner.FirstName
                    cmd.Parameters.Add("@PayeeLastName", SqlDbType.VarChar, 30).Value = lastName                                                     'logInfo.Owner.LastName
                    cmd.Parameters.Add("@PayeeAddress1", SqlDbType.VarChar, 40).Value = String.Empty 'BLL.SessionManager.TransactionInformation.Address1           'logInfo.Owner.Address1
                    cmd.Parameters.Add("@BillZip", SqlDbType.Char, 5).Value = String.Empty 'BLL.SessionManager.TransactionInformation.ZipCode                      'logInfo.Owner.ZipCode
                    cmd.Parameters.Add("@PayeeEmail", SqlDbType.VarChar, 100).Value = BLL.SessionManager.CustomerEmail         'logInfo.Owner.EMailAddress
                    cmd.Parameters.Add("@AVSCode", SqlDbType.Char, 1).Value = String.Empty                                                           'System.DBNull.Value
                    cmd.Parameters.Add("@Source", SqlDbType.Int).Value = B2P.Common.Enumerations.TransactionSources.Counter
                    cmd.Parameters.Add("@ItemAuthCode", SqlDbType.Char, 6).Value = response.ApprovalNumber                                           '????
                    cmd.Parameters.Add("@ItemAuthID", SqlDbType.Char, 26).Value = GetHostBatchID(paymentType, response.Processor.RawResponse)        'TriPOS = HostBatchID      rs.ItemResponse.AuthorizationRequestID
                    cmd.Parameters.Add("@ItemReasonCode", SqlDbType.Char, 3).Value = response.Processor.HostResponseCode                             'rs.ItemResponse.ProcessorReasonCode
                    cmd.Parameters.Add("@ItemReverseStatus", SqlDbType.Int).Value = 1                                                                'Successful void value 1 or 0 -- Ask Ken????

                    If Not BLL.SessionManager.UseSingleFee Then
                        If feeResponse IsNot Nothing AndAlso Not Utility.IsNullOrEmpty(feeResponse.AuthorizationCode) Then
                            cmd.Parameters.Add("@FeeAuthCode", SqlDbType.Char, 6).Value = feeResponse.AuthorizationCode
                        Else
                            cmd.Parameters.Add("@FeeAuthCode", SqlDbType.Char, 6).Value = System.DBNull.Value
                        End If

                        cmd.Parameters.Add("@FeeAuthID", SqlDbType.Char, 26).Value = response.TransactionID
                    Else
                        cmd.Parameters.Add("@FeeAuthCode", SqlDbType.Char, 6).Value = System.DBNull.Value
                        cmd.Parameters.Add("@FeeAuthID", SqlDbType.Char, 26).Value = System.DBNull.Value
                    End If

                    cmd.Parameters.Add("@FeeReasonCode", SqlDbType.Char, 3).Value = System.DBNull.Value                                              '????

                    ' If response.IsApproved = True, then something else happened and the TriPOS trans will be reveresed.
                    cmd.Parameters.Add("@FeeReverseStatus", SqlDbType.Int).Value = Utility.IIf(Of Int32)(response.IsApproved, 1, 0)                  '1 or 0 -- Ask Ken????

                    cmd.Parameters.Add("@CountryCode", SqlDbType.Char, 2).Value = "US"                                                               'Put config logInfo.Owner.CountryCode
                    cmd.Parameters.Add("@Profile_ID", SqlDbType.Int).Value = 0
                    cmd.Parameters.Add("@Security_ID", SqlDbType.Int).Value = BLL.SessionManager.SecurityID    'Security_ID Looks at the SSO and map to user
                    cmd.Parameters.Add("@SourceReference_ID", SqlDbType.Int).Value = 0                                                               'SourceReference_ID
                    cmd.Parameters.Add("@Office_ID", SqlDbType.Int).Value = BLL.SessionManager.OfficeID                      'Look at SSO Office_ID
                    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = BLL.SessionManager.ReferenceNumber                                        'Batch_ID

                    'cmd.Parameters.Add("@CardNetwork", SqlDbType.Char, 1).Value = response.PaymentType.Substring(0, 1).ToUpper                       'logInfo.CardNetwork - Credit (C) or Debit (D)
                    cmd.Parameters.Add("@CardNetwork", SqlDbType.Char, 1).Value = paymentType.ToString.Substring(0, 1).ToUpper                       'Credit (C) or Debit (D)

                    cmd.Parameters.Add("@HostResponseCode", SqlDbType.VarChar, 10).Value = response.Processor.HostResponseCode
                    cmd.Parameters.Add("@HostResponseMessage", SqlDbType.VarChar, 20).Value = response.Processor.HostResponseMessage
                    cmd.Parameters.Add("@CCFailedTransactionItems", SqlDbType.Structured).Value = items.ToDataTable                                  'Build the cart items in DtataTable Me.Items.ToDataTable
                    cmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Got to here? Update the return value
                    If Convert.ToInt32(cmd.Parameters("@RetVal").Value) = 1 Then
                        retVal = True
                    End If

                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            ' Set return value
            retVal = False
        End Try

        Return retVal
    End Function

    ''' <summary>
    ''' Adds a TriPOS transaction to the B2P database.
    ''' </summary>
    ''' <param name="paymentType">The payment type used.</param>
    ''' <param name="response">An Entities.AuthorizationResponse object.</param>
    ''' <returns>Boolean.</returns>
    Private Shared Function AddTransaction(ByVal response As Entities.SaleResponse, ByVal paymentType As Entities.PaymentType, ByVal feeResponse As B2P.triPOS.Utility.Payments.FeeResponse) As Boolean
        Dim items As B2P.Payment.PaymentBase.TransactionItems = Nothing
        Dim feeCalcType As B2P.Payment.FeeCalculation.PaymentTypes
        Dim payConfig As Entities.PaymentConfiguration = Nothing
        Dim accountConfig As ClientAccountConfiguration = Nothing
        Dim firstName As String = String.Empty
        Dim lastName As String = String.Empty
        Dim retVal As Boolean
        Dim errMsg As String = String.Empty

        Try
            ' Get the cart items -- non-tax for now, but will need to revisit to handle tax items
            Select Case paymentType
                Case Entities.PaymentType.Credit
                    feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.CreditCard

                Case Entities.PaymentType.Debit
                    feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.PinDebit

                    ' May need to address this at a later date
                    'If response.PinVerified OrElse BLL.SessionManager.UseBinLookup Then
                    '    feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.PinDebit
                    'Else
                    '    feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.DebitCard
                    'End If
            End Select

            ' Get the cart items -- non-tax for now, but will need to revisit to handle tax items
            items = BLL.SessionManager.Cart.GetCartItems(feeCalcType, B2P.ShoppingCart.Cart.CartItemsTypes.Nontax)

            ' Get the client payment configuration info
            If items.Count > 0 Then
                payConfig = Entities.PaymentConfiguration.GetConfiguration(BLL.SessionManager.ClientCode, items(0).ProductName, B2P.Common.Enumerations.TransactionSources.Counter, B2P.Common.Enumerations.PaymentTypes.CreditCard)
            Else
                Return False
            End If

            ' Get the client account configuration info
            accountConfig = Entities.ClientAccountConfiguration.GetConfiguration(BLL.SessionManager.ClientCode)

            ' Get the name from the cardholder name in the response
            If response.CardHolderName.IndexOf("/"c) > -1 Then
                firstName = response.CardHolderName.Split("/"c)(1).Trim
                lastName = response.CardHolderName.Split("/"c)(0).Trim
            Else
                lastName = response.CardHolderName.Trim
            End If

            ' Create the connection
            Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand
                    cmd.CommandText = "ap_LogCCTransaction"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 300
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@ClientCode", SqlDbType.VarChar, 10).Value = BLL.SessionManager.ClientCode
                    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = BLL.SessionManager.ReferenceNumber
                    cmd.Parameters.Add("@PayeeFirstName", SqlDbType.VarChar, 20).Value = firstName                                                   'logInfo.Owner.FirstName
                    cmd.Parameters.Add("@PayeeLastName", SqlDbType.VarChar, 30).Value = lastName                                                     'logInfo.Owner.LastName
                    cmd.Parameters.Add("@PayeeAddress1", SqlDbType.VarChar, 40).Value = String.Empty 'BLL.SessionManager.TransactionInformation.Address1           'logInfo.Owner.Address1
                    cmd.Parameters.Add("@PayeeAddress2", SqlDbType.VarChar, 40).Value = String.Empty 'BLL.SessionManager.TransactionInformation.Address2           'logInfo.Owner.Address2
                    cmd.Parameters.Add("@PayeeCity", SqlDbType.VarChar, 25).Value = String.Empty 'BLL.SessionManager.TransactionInformation.City                   'logInfo.Owner.City
                    cmd.Parameters.Add("@PayeeState", SqlDbType.Char, 2).Value = String.Empty 'BLL.SessionManager.TransactionInformation.State                     'logInfo.Owner.State
                    cmd.Parameters.Add("@PayeeZip", SqlDbType.Char, 10).Value = "32256" 'BLL.SessionManager.TransactionInformation.ZipCode                    'logInfo.Owner.ZipCode
                    cmd.Parameters.Add("@PayeeProvince", SqlDbType.VarChar, 25).Value = System.DBNull.Value                                          'logInfo.Owner.Province
                    cmd.Parameters.Add("@PayeeEmail", SqlDbType.VarChar, 100).Value = BLL.SessionManager.CustomerEmail         'logInfo.Owner.EMailAddress
                    cmd.Parameters.Add("@PayeePhone", SqlDbType.VarChar, 14).Value = String.Empty 'BLL.SessionManager.TransactionInformation.Phone                 'logInfo.Owner.PhoneNumber
                    cmd.Parameters.Add("@Source", SqlDbType.Int).Value = B2P.Common.Enumerations.TransactionSources.Counter
                    cmd.Parameters.Add("@CountryCode", SqlDbType.Char, 2).Value = "US"                                                               'Put in config  logInfo.Owner.CountryCode
                    cmd.Parameters.Add("@UserComments", SqlDbType.VarChar, 1024).Value = BLL.SessionManager.UserComments
                    cmd.Parameters.Add("@ClearCreditCardNumber", SqlDbType.VarChar, 20).Value = response.AccountNumber
                    cmd.Parameters.Add("@BINNumber", SqlDbType.Char, 6).Value = response.BinValue
                    cmd.Parameters.Add("@ExpirationMonth", SqlDbType.Char, 2).Value = response.ExpirationMonth
                    cmd.Parameters.Add("@ExpirationYear", SqlDbType.Char, 4).Value = response.ExpirationYear
                    cmd.Parameters.Add("@CardIssuer", SqlDbType.Char, 1).Value = response.CardLogo.Substring(0, 1).ToUpper
                    cmd.Parameters.Add("@ItemAuthCode", SqlDbType.Char, 6).Value = response.ApprovalNumber                                           '????
                    cmd.Parameters.Add("@ItemAuthID", SqlDbType.Char, 26).Value = GetHostBatchID(paymentType, response.Processor.RawResponse)        'TriPOS = HostBatchID      rs.ItemResponse.AuthorizationRequestID
                    cmd.Parameters.Add("@ItemAuthToken", SqlDbType.VarChar, 255).Value = response.TransactionID                                      'TriPOS = TransactionID    rs.ItemResponse.AuthorizationToken
                    cmd.Parameters.Add("@ItemMerchantID", SqlDbType.VarChar, 20).Value = payConfig.MerchantID


                    If Not BLL.SessionManager.UseSingleFee Then
                        cmd.Parameters.Add("@FeeAuthCode", SqlDbType.Char, 6).Value = feeResponse.AuthorizationCode
                        cmd.Parameters.Add("@FeeAuthID", SqlDbType.Char, 26).Value = response.TransactionID                                          'feeResponse.AuthorizationRequestID
                        cmd.Parameters.Add("@FeeAuthToken", SqlDbType.VarChar, 255).Value = feeResponse.AuthorizationToken
                        cmd.Parameters.Add("@FeeMerchantID", SqlDbType.VarChar, 20).Value = accountConfig.TaxFeeMerchant                                  ' ProcessFee(merchantID
                    Else
                        cmd.Parameters.Add("@FeeAuthCode", SqlDbType.Char, 6).Value = System.DBNull.Value
                        cmd.Parameters.Add("@FeeAuthID", SqlDbType.Char, 26).Value = System.DBNull.Value
                        cmd.Parameters.Add("@FeeAuthToken", SqlDbType.VarChar, 255).Value = System.DBNull.Value
                        cmd.Parameters.Add("@FeeMerchantID", SqlDbType.VarChar, 20).Value = accountConfig.TaxFeeMerchant                                  ' May need to be NULL
                    End If

                    cmd.Parameters.Add("@AVSCode", SqlDbType.Char, 1).Value = String.Empty                                                           'System.DBNull.Value

                    ' Ken used duration to see how long the processing took
                    ' --> Can't really use this with TriPOS, so set the param = NULL
                    cmd.Parameters.Add("@Duration", SqlDbType.Decimal)
                    cmd.Parameters("@Duration").Precision = 4
                    cmd.Parameters("@Duration").Scale = 2
                    cmd.Parameters("@Duration").Value = System.DBNull.Value

                    cmd.Parameters.Add("@Profile_ID", SqlDbType.Int).Value = 0
                    cmd.Parameters.Add("@Security_ID", SqlDbType.Int).Value = BLL.SessionManager.SecurityID    'Security_ID Looks at the SSO and map to user
                    cmd.Parameters.Add("@SourceReference_ID", SqlDbType.Int).Value = 0                                                               'SourceReference_ID
                    cmd.Parameters.Add("@Office_ID", SqlDbType.Int).Value = BLL.SessionManager.OfficeID                      'Look at SSO Office_ID
                    cmd.Parameters.Add("@AppCode", SqlDbType.VarChar, 10).Value = System.DBNull.Value
                    cmd.Parameters.Add("@VendorReferenceCode", SqlDbType.VarChar, 40).Value = BLL.SessionManager.TransactionInformation.VendorReferenceCode

                    'cmd.Parameters.Add("@CardNetwork", SqlDbType.Char, 1).Value = response.PaymentType.Substring(0, 1).ToUpper                       'logInfo.CardNetwork - Credit (C) or Debit (D)
                    cmd.Parameters.Add("@CardNetwork", SqlDbType.Char, 1).Value = paymentType.ToString.Substring(0, 1).ToUpper                       'Credit (C) or Debit (D)

                    cmd.Parameters.Add("@PaymentToken", SqlDbType.VarChar, 200).Value = System.DBNull.Value                                          'In the future for tax payments
                    cmd.Parameters.Add("@HostResponseCode", SqlDbType.VarChar, 10).Value = response.Processor.HostResponseCode
                    cmd.Parameters.Add("@HostResponseMessage", SqlDbType.VarChar, 20).Value = response.Processor.HostResponseMessage
                    cmd.Parameters.Add("@IsCaptured", SqlDbType.Bit).Value = 1

                    ' If there is a table to attach, set the value. Otherwise, son't set the parameter as it defaults to an empty table
                    'cmd.Parameters.Add("@UserData", SqlDbType.Structured).Value = System.DBNull.Value                                                'Me.UserData.ToDataTable

                    cmd.Parameters.Add("@TransactionItems", SqlDbType.Structured).Value = items.ToDataTable                                           'Build the cart items in DataTable Me.Items.ToDataTable
                    cmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Got to here? Update the return value
                    If Convert.ToInt32(cmd.Parameters("@RetVal").Value) = 1 Then
                        retVal = True
                    End If

                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            ' Set return value
            retVal = False
        Finally
            ' Clean up a bit
            If accountConfig IsNot Nothing Then
                accountConfig = Nothing
            End If

            If payConfig IsNot Nothing Then
                payConfig = Nothing
            End If

            If items IsNot Nothing Then
                items = Nothing
            End If
        End Try

        Return retVal
    End Function


    ''' <summary>
    ''' Adds a TriPOS transaction to the database.
    ''' </summary>
    ''' <param name="lane">The lane of the PIN pad terminal.</param>
    ''' <returns>Integer.</returns>
    Private Shared Function AddTriposTransaction(ByVal lane As Int32) As Int32    ' ByVal paymentType As Entities.PaymentType,
        Dim retVal As Int32
        Dim errMsg As String = String.Empty

        Try
            ' Create the connection
            Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand
                    cmd.CommandText = "ap_AddTriposTrans"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@Token", SqlDbType.VarChar, 32).Value = BLL.SessionManager.Token.Trim
                    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = Convert.ToInt32(BLL.SessionManager.ReferenceNumber.Trim)
                    cmd.Parameters.Add("@LaneID", SqlDbType.Int).Value = lane
                    cmd.Parameters.Add("@ClientCode", SqlDbType.VarChar, 10).Value = BLL.SessionManager.ClientCode.Trim
                    cmd.Parameters.Add("@OfficeID", SqlDbType.Int).Value = BLL.SessionManager.OfficeID
                    ' Not sure what the customer will select on the PIN pad --> initialize to NULL 
                    cmd.Parameters.Add("@PaymentTypeSelected", SqlDbType.VarChar, 10).Value = System.DBNull.Value
                    cmd.Parameters.Add("@PaymentType", SqlDbType.VarChar, 10).Value = System.DBNull.Value
                    cmd.Parameters.Add("@CardEntryMode", SqlDbType.VarChar, 20).Value = System.DBNull.Value
                    cmd.Parameters.Add("@BinNumber", SqlDbType.VarChar, 6).Value = System.DBNull.Value
                    cmd.Parameters.Add("@ResponseCode", SqlDbType.VarChar, 10).Value = System.DBNull.Value
                    cmd.Parameters.Add("@ResponseMessage", SqlDbType.VarChar, 1024).Value = System.DBNull.Value
                    cmd.Parameters.Add("@SignatureFormat", SqlDbType.VarChar, 20).Value = System.DBNull.Value
                    cmd.Parameters.Add("@SignatureData", SqlDbType.VarBinary, -1).Value = System.DBNull.Value
                    cmd.Parameters.Add("@SignatureStatusCode", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                    cmd.Parameters.Add("@EmvPaymentName", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                    cmd.Parameters.Add("@EmvApplicationID", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                    cmd.Parameters.Add("@EmvCryptogram", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                    cmd.Parameters.Add("@RawResponse", SqlDbType.VarChar, -1).Value = System.DBNull.Value
                    cmd.Parameters.Add("@HasError", SqlDbType.Bit).Value = 0
                    cmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar, 1024).Value = System.DBNull.Value
                    cmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Get the return value
                    retVal = Convert.ToInt32(cmd.Parameters("@RetVal").Value)
                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            ' Set return value???
            retVal = -9
        End Try

        Return retVal
    End Function

    ''' <summary>
    ''' Builds the fees information for the various allowed payment types.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildCartSummary()
        Dim creditFeeDesc As String = B2P.Objects.Client.GetClientMessage("CounterCreditFee", BLL.SessionManager.ClientCode)
        Dim debitFeeDesc As String = B2P.Objects.Client.GetClientMessage("CounterDebitFee", BLL.SessionManager.ClientCode)
        Dim visaDebitFeeDesc As String = B2P.Objects.Client.GetClientMessage("CounterVisaDebitFee", BLL.SessionManager.ClientCode)
        Dim errMsg As String = String.Empty

        Try
            litCreditCart.Text = BLL.SessionManager.Cart.TotalAmount.ToString("C2")
            litCreditFee.Text = BLL.SessionManager.Cart.CreditCardFee.ToString("C2")
            litCreditTotal.Text = (BLL.SessionManager.Cart.TotalAmount + BLL.SessionManager.Cart.CreditCardFee).ToString("C2")
            litCreditFeeDescription.Text = Utility.IIf(Of String)(Not Utility.IsNullOrEmpty(creditFeeDesc.Trim), Utility.SafeEncode(creditFeeDesc.Trim), String.Empty)

            litDebitCart.Text = BLL.SessionManager.Cart.TotalAmount.ToString("C2")
            litDebitFee.Text = BLL.SessionManager.Cart.PinDebitFee.ToString("C2")
            litDebitTotal.Text = (BLL.SessionManager.Cart.TotalAmount + BLL.SessionManager.Cart.PinDebitFee).ToString("C2")
            litDebitFeeDescription.Text = Utility.IIf(Of String)(Not Utility.IsNullOrEmpty(debitFeeDesc.Trim), Utility.SafeEncode(debitFeeDesc.Trim), String.Empty)

            litVisaDebitCart.Text = BLL.SessionManager.Cart.TotalAmount.ToString("C2")
            litVisaDebitFee.Text = BLL.SessionManager.Cart.DebitFee.ToString("C2")
            litVisaDebitTotal.Text = (BLL.SessionManager.Cart.TotalAmount + BLL.SessionManager.Cart.DebitFee).ToString("C2")
            litVisaDebitFeeDescription.Text = Utility.IIf(Of String)(Not Utility.IsNullOrEmpty(visaDebitFeeDesc.Trim), Utility.SafeEncode(visaDebitFeeDesc.Trim), String.Empty)

            litACHCart.Text = BLL.SessionManager.Cart.TotalAmount.ToString("C2")
            litACHFee.Text = BLL.SessionManager.Cart.ECheckFee.ToString("C2")
            litACHTotal.Text = (BLL.SessionManager.Cart.TotalAmount + BLL.SessionManager.Cart.ECheckFee).ToString("C2")


        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub
    ''' <summary>
    ''' Builds a receipt for a successful transaction.
    ''' </summary>
    ''' <param name="saleResponse">The transaction sale response.</param>
    ''' <param name="paymentType">The payment type used - either Credit or Debit.</param>
    ''' <returns>An Entities.Receipt object.</returns>
    ''' <remarks></remarks>
    Private Shared Function BuildTransactionReceipt(ByVal saleResponse As Entities.SaleResponse, ByVal paymentType As Entities.PaymentType) As Entities.Receipt
        Dim office As B2P.Client.Office = Nothing
        Dim receipt As Entities.Receipt = Nothing
        Dim errMsg As String = String.Empty

        Try
            ' Get the client office
            office = New B2P.Client.Office(BLL.SessionManager.OfficeID)

            ' Create receipt and store in session
            If office.Found Then
                receipt = New Entities.Receipt(saleResponse, BLL.SessionManager.Token, BLL.SessionManager.ReferenceNumber, BLL.SessionManager.Cart, office)
            Else
                receipt = New Entities.Receipt(saleResponse, BLL.SessionManager.Token, BLL.SessionManager.ReferenceNumber, BLL.SessionManager.Cart,
                                           B2P.Objects.Client.GetClient(BLL.SessionManager.TransactionInformation.ClientCode))
            End If

            ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            ' Check UseSingleFee to get the correct amounts on the receipt???
            ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            ' Make sure we display the correct fee on the receipt
            If Not BLL.SessionManager.UseSingleFee Then
                Select Case paymentType
                    Case Entities.PaymentType.Credit
                        receipt.ConvenienceFee = BLL.SessionManager.Cart.CreditCardFee
                    Case Entities.PaymentType.Debit
                        receipt.ConvenienceFee = BLL.SessionManager.Cart.PinDebitFee

                    ' May need to address this at a later date
                    'If saleResponse.PinVerified OrElse BLL.SessionManager.UseBinLookup Then
                    '    receipt.ConvenienceFee = BLL.SessionManager.Cart.PinDebitFee
                    'Else
                    '    receipt.ConvenienceFee = BLL.SessionManager.Cart.DebitFee
                    'End If
                    Case Else
                        ' Maybe VisaDebit, PNM, etc. down the road
                End Select

                ' Make sure to add the cart total with the tokenized fee payment
                receipt.TransactionAmount = BLL.SessionManager.Cart.TotalAmount + receipt.ConvenienceFee
            Else
                receipt.ConvenienceFee = BLL.SessionManager.Cart.CreditCardFee
            End If
            ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                           ex.Source, ex.TargetSite.DeclaringType.Name,
                                           ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.Yes)
            HttpContext.Current.Response.Redirect("/Errors/Error.aspx", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            receipt = Nothing

        Finally
            ' Clean up a bit
            If office IsNot Nothing Then
                office = Nothing
            End If
        End Try

        Return receipt
    End Function

    ''' <summary>
    ''' Builds an error message returned from a TriPOS error list.
    ''' </summary>
    ''' <param name="errors">A List(Of Entities.ApiError).</param>
    ''' <returns>String.</returns>
    Private Shared Function BuildTriposErrorMessage(ByVal errors As List(Of Entities.ApiError)) As String
        Dim err As Entities.ApiError = Nothing
        Dim errMsg As String = String.Empty

        For Each err In errors

            If Not Utility.IsNullOrEmpty(err.UserMessage) Then
                errMsg &= err.UserMessage & Environment.NewLine
            Else
                errMsg &= err.DeveloperMessage & Environment.NewLine
            End If

        Next err

        Return errMsg
    End Function

    ''' <summary>
    ''' Creates signed request headers for transmission with TriPOS.
    ''' </summary>
    ''' <param name="requestUrl">The TriPOS REST endpoint.</param>
    ''' <param name="requestMethod">The type of HTTP request method.</param>
    ''' <param name="requestData">The data to send to TriPOS.</param>
    ''' <returns>String.</returns>
    <WebMethod>
    Public Shared Function CreateRequestHeaders(ByVal requestUrl As String, ByVal requestMethod As String, ByVal requestData As String) As String
        Dim method As New HttpMethod(requestMethod.Trim)
        Dim authHeader As AuthorizationHeader = Nothing
        Dim accountConfig As ClientAccountConfiguration = ClientAccountConfiguration.GetConfiguration(BLL.SessionManager.ClientCode)
        Dim elementConfig As ElementConfiguration = ElementConfiguration.GetConfiguration(accountConfig.ConvenienceMerchant)
        Dim retVal As String = String.Empty

        Using message As New HttpRequestMessage(method, New Uri(requestUrl))
            authHeader = AuthorizationHeader.Create(message.Headers, New Uri(requestUrl), requestData, message.Method.Method, "1.0", "TP-HMAC-SHA1",
                                                        Guid.NewGuid().ToString, DateTime.UtcNow.ToString("O"), elementConfig.DeveloperKey, elementConfig.DeveloperSecret)
        End Using

        retVal = "{""tp-authorization"":""" & authHeader.ToString & """,""tp-application-id"":""" & elementConfig.TriposApplicationID & """,""tp-application-name"":""Bill2Pay""," &
                      """tp-application-version"":""1.0.0"",""tp-request-id"":""" & Guid.NewGuid.ToString & """,""tp-return-logs"":""false"",""accept"":""application/json""}"

        Return retVal
    End Function

    ''' <summary>
    ''' Generates a batch ID for the transaction.
    ''' </summary>
    ''' <param name="connectionString">Connection string to the B2P Configuration database.</param>
    ''' <returns>Integer.</returns>
    Private Shared Function GenerateBatchID(ByVal connectionString As String) As Int32
        Dim retVal As Int32
        Dim errMsg As String = String.Empty

        Try
            ' Create the connection
            Using conn As New SqlClient.SqlConnection(connectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand
                    cmd.CommandText = "ap_GetBatch_ID"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Direction = ParameterDirection.Output

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Get the return value
                    retVal = Convert.ToInt32(cmd.Parameters("@Batch_ID").Value)
                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            ' Set return value???
            retVal = -999
        End Try

        Return retVal
    End Function

    ''' <summary>
    ''' Gets a list of allowable cards by a merchant.
    ''' </summary>
    ''' <returns>
    ''' A List(Of String) object.
    ''' </returns>
    ''' <remarks>
    ''' TriPOS possible values are: Visa, Mastercard, Discover, Amex, Diners Club, JCB, Carte Blanche, Other.
    ''' </remarks>
    Private Shared Function GetCardsAllowed() As List(Of String)
        Dim cardsAllowed As New List(Of String)

        If BLL.SessionManager.Cart.AcceptAmex Then cardsAllowed.Add("Amex")
        If BLL.SessionManager.Cart.AcceptDiscover Then cardsAllowed.Add("Discover")
        If BLL.SessionManager.Cart.AcceptMasterCard Then cardsAllowed.Add("Mastercard")
        If BLL.SessionManager.Cart.AcceptVisa Then cardsAllowed.Add("Visa")

        Return cardsAllowed
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCreditCard() As B2P.Common.Objects.CreditCard
        Dim card As B2P.Common.Objects.CreditCard = Nothing
        Dim dateParts As String()
        Dim errMsg As String = String.Empty

        Try
            card = New B2P.Common.Objects.CreditCard

            card.Owner.FirstName = Regex.Replace(pccEnterCreditCardInfo.FirstName.Trim, "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase)
            card.Owner.LastName = Regex.Replace(pccEnterCreditCardInfo.LastName.Trim, "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase)

            ':::::::::::::::::::::::::::::::::::::::::::::::::::::::
            ' Removed CC address form fields per Ken Ponder
            ' Some address info set to 'NA' so Vantiv will process.
            ' Set international info to FL and 11111 per Ken Ponder
            ' May want to visit this later and validate
            ':::::::::::::::::::::::::::::::::::::::::::::::::::::::
            card.Owner.Address1 = "NA"
            card.Owner.Address2 = "NA"
            card.Owner.City = "NA"
            card.Owner.State = "FL"
            card.Owner.CountryCode = "US"
            card.Owner.ZipCode = "11111"
            ':::::::::::::::::::::::::::::::::::::::::::::::::

            card.Owner.EMailAddress = String.Empty
            card.Owner.PhoneNumber = String.Empty

            card.CreditCardNumber = pccEnterCreditCardInfo.CreditCardNumber.Trim.Replace(" ", "")

            dateParts = pccEnterCreditCardInfo.ExpirationDate.Trim.Replace(" ", "").Split("/"c)
            card.ExpirationMonth = dateParts(0).Trim
            card.ExpirationYear = dateParts(1).Trim

            card.SecurityCode = pccEnterCreditCardInfo.CVV.Trim

            ' Add the card to the session
            BLL.SessionManager.CreditCard = card
            BLL.SessionManager.PaymentType = B2P.Common.Enumerations.PaymentTypes.CreditCard

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

        Return card
    End Function

    ''' <summary>
    ''' Gets the host batch ID for a transaction located in an XML formatted response string. 
    ''' </summary>
    ''' <param name="paymentType">The payment type used - either Credit or Debit.</param>
    ''' <param name="rawResponse">The raw response returned for a TriPOS transaction.</param>
    ''' <returns>String.</returns>
    Private Shared Function GetHostBatchID(ByVal paymentType As Entities.PaymentType, ByVal rawResponse As String) As String
        Dim reader As XmlTextReader = Nothing
        Dim doc As XmlDocument = Nothing
        Dim node As XmlNode = Nothing
        Dim nodePath As String = String.Empty

        Dim retVal As String = String.Empty

        If Not Utility.IsNullOrEmpty(rawResponse) Then
            Try
                ' Load the reader w/o the namespaces
                reader = New XmlTextReader(New StringReader(rawResponse))
                reader.Namespaces = False

                ' Create the document and load the XML
                doc = New XmlDocument
                doc.Load(reader)

                ' Get the node path
                Select Case paymentType
                    Case Entities.PaymentType.Credit
                        nodePath = "/CreditCardSaleResponse/Response/Batch/HostBatchID"

                    Case Entities.PaymentType.Debit
                        nodePath = "/DebitCardSaleResponse/Response/Batch/HostBatchID"

                    Case Else
                        ' May need to check in the future
                End Select

                ' Parse the response child nodes
                If doc.SelectSingleNode(nodePath) IsNot Nothing Then
                    node = doc.SelectSingleNode(nodePath)

                    retVal = node.InnerText.Trim
                End If

            Catch ex As Exception
                Throw New Exception("Failed to retrieve the host batch ID from the response: " & ex.Message)

            Finally
                ' Clean up a bit
                If node IsNot Nothing Then
                    node = Nothing
                End If

                If doc IsNot Nothing Then
                    doc = Nothing
                End If

                If reader IsNot Nothing Then
                    reader = Nothing
                End If
            End Try
        End If

        Return retVal
    End Function

    ''' <summary>
    ''' 
    ''' </summary> 
    ''' <param name="user"></param>
    ''' <param name="clientCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetMappedUser(user As String, clientCode As String) As Int32
        Dim retVal As Int32
        Dim errMsg As String = String.Empty

        Try
            ' Create the connection
            Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand
                    cmd.CommandText = "ap_GetVendorUserMap"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@ThirdPartyVendorID", SqlDbType.VarChar, 50).Value = user
                    cmd.Parameters.Add("@Security_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("@ClientCode", SqlDbType.VarChar, 10).Value = clientCode

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Get the return value
                    retVal = Convert.ToInt32(cmd.Parameters("@Security_ID").Value)
                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.Yes)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            retVal = 0
        End Try

        Return retVal
    End Function

    <WebMethod>
    Public Shared Function GetRequestHeaders() As String
        Dim retVal As String = String.Empty

        retVal = "{""tp-authorization"":""Version=1.0, Credential=120b325c-505a-4b2f-b16b-ec345ff6b436"",""tp-application-id"":""123"",""tp-application-name"":""Bill2Pay"",""tp-application-version"":""1.0.0"",""tp-request-id"":""" & Guid.NewGuid.ToString & """,""tp-return-logs"":""false"",""accept"":""application/json""}"

        Return retVal
    End Function

    ''' <summary>
    ''' Builds a TriPOS reversal request payload.
    ''' </summary>
    ''' <returns>An Entities.ReversalRequest object.</returns>
    Private Shared Function GetReversalRequest() As Entities.ReversalRequest
        Dim request As New Entities.ReversalRequest

        request.CardHolderPresentCode = "Present"
        request.ClerkNumber = BLL.SessionManager.ClerkNumber
        request.LaneID = BLL.SessionManager.LaneID
        request.ReferenceNumber = BLL.SessionManager.ReferenceNumber
        request.ShiftID = "WEB-24/7"
        request.TicketNumber = BLL.SessionManager.ReferenceNumber

        ' Get the correct reversal transaction amount
        If Not BLL.SessionManager.UseSingleFee Then
            ' Just use the cart total -> tokenized fee payments need to be voided with Ken's utility DLL
            request.TransactionAmount = BLL.SessionManager.Cart.TotalAmount
        Else
            If BLL.SessionManager.Cart.CreditCardFee = BLL.SessionManager.Cart.PinDebitFee Then
                request.TransactionAmount = BLL.SessionManager.Cart.TotalAmount + BLL.SessionManager.Cart.CreditCardFee   ' --> or BLL.SessionManager.Cart.PinDebitFee???
            Else
                ' Throw Single Fee Not Matching Error
                Throw New Exception("Single fee for payment types do not match.")
            End If
        End If

        Return request
    End Function

    ''' <summary>
    ''' Gets JSON formatted TriPOS reversal link.
    ''' </summary>
    ''' <param name="links">A List(Of Entities.ApiLink).</param>
    ''' <returns>A JSON formatted string</returns>
    ''' <remarks>This is used when the card payment type does not match the clerk's payment type selection.</remarks>
    Private Shared Function GetReversalTransactionLink(ByVal links As List(Of Entities.ApiLink)) As String
        Dim link As Entities.ApiLink = Nothing
        Dim retVal As String = String.Empty

        For Each link In links
            If link.Relation = "reversal" Then
                retVal = JsonConvert.SerializeObject(link, Newtonsoft.Json.Formatting.Indented)
                Exit For
            End If
        Next link

        Return retVal
    End Function

    ''' <summary>
    ''' Generates a TriPOS sale request payload.
    ''' </summary>
    ''' <param name="lane">The lane ID of the card reader connected to TriPOS.</param>
    ''' <returns>A JSON formatted string.</returns>
    <WebMethod>
    Public Shared Function GetSaleRequest(ByVal lane As Int32) As String    ' ByVal paymentType As String, 
        Dim request As Entities.SaleRequest = Nothing
        Dim batchId As Int32
        Dim triposTranId As Int32
        Dim office As B2P.Client.Office = Nothing
        Dim retVal As String = String.Empty
        Dim errMsg As String = String.Empty

        Try

            ' Check that the lane is > 0
            If lane > 0 Then
                ' Get the batch ID for the request
                batchId = GenerateBatchID(B2P.Common.Configuration.ConnectionString)

                ' Get the client's office
                office = New B2P.Client.Office(BLL.SessionManager.OfficeID)

                ' Create the request
                request = New Entities.SaleRequest

                request.Address = New Entities.Address
                request.Address.BillingName = BLL.SessionManager.TransactionInformation.FirstName & " " & BLL.SessionManager.TransactionInformation.LastName
                request.Address.BillingAddress1 = BLL.SessionManager.TransactionInformation.Address1
                request.Address.BillingAddress2 = BLL.SessionManager.TransactionInformation.Address2
                request.Address.BillingCity = BLL.SessionManager.TransactionInformation.City
                request.Address.BillingState = BLL.SessionManager.TransactionInformation.State
                request.Address.BillingPostalCode = BLL.SessionManager.TransactionInformation.ZipCode
                request.Address.BillingPhone = BLL.SessionManager.TransactionInformation.Phone
                request.Address.BillingEmail = BLL.SessionManager.CustomerEmail

                If office.Found Then
                    request.ClerkNumber = "O" & office.Office_ID.ToString & "-L" & lane.ToString  ' OfficeID & LaneID
                Else
                    request.ClerkNumber = "TI" & BLL.SessionManager.OfficeID.ToString & "-L" & lane.ToString  ' OfficeID & LaneID
                End If

                request.EmvFallbackReason = "None"                            ' Talk to Ken --> try to find the reason types
                request.LaneID = lane                                         ' 1 = Verifone VX805, 2 = Verifone MX915, 9999 = NULL PIN pad
                request.ReferenceNumber = batchId.ToString                    ' Use Batch ID
                request.ShiftID = "WEB-24/7"                                  ' WEB-24/7
                request.TicketNumber = batchId.ToString                       ' Ken suggested to use Batch ID

                ' Get the correct transaction amount
                If Not BLL.SessionManager.UseSingleFee Then
                    ' The fee will be processed later as a tokenized payment
                    request.TransactionAmount = BLL.SessionManager.Cart.TotalAmount
                Else
                    If Not BLL.SessionManager.Cart.AcceptCreditCards OrElse Not BLL.SessionManager.Cart.AcceptPinDebit Then
                        Throw New Exception("Cannot determine single fee as client does not accept either credit or PIN debit. Please contact support.")
                    Else
                        If BLL.SessionManager.Cart.CreditCardFee = BLL.SessionManager.Cart.PinDebitFee Then
                            request.TransactionAmount = BLL.SessionManager.Cart.TotalAmount + BLL.SessionManager.Cart.CreditCardFee   ' --> or BLL.SessionManager.Cart.PinDebitFee???
                        Else
                            ' Throw Single Fee Not Matching Error
                            Throw New Exception("Single fee for payment types do not match.")
                        End If
                    End If
                End If

                ' Update a few session objects
                BLL.SessionManager.ClerkNumber = request.ClerkNumber
                BLL.SessionManager.ReferenceNumber = batchId.ToString
                BLL.SessionManager.LaneID = lane

                ' Add to TriPOS table
                triposTranId = AddTriposTransaction(lane)

                retVal = JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)
            Else
                ' Build the lane error message
                errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                       "B2PAdmin", "PinPad", "GetSaleRequest",
                                                       "Sale request using an invalid lane ID: " & lane.ToString & ". Lane IDs must be greater than 0.")

                B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

        Return retVal
    End Function

    ''' <summary>
    ''' Gets the reference number used for a transaction located in an XML formatted response string.
    ''' </summary>
    ''' <param name="rawResponse">The raw response returned for a TriPOS transaction.</param>
    ''' <returns>String.</returns>
    ''' <remarks>
    ''' Checking if the lane status is "NotInUse" should make this function obsolete. If we notice 
    ''' that there is an issue with the reference numbers not being in sync, then we can use this 
    ''' function to get the reference number from the raw response.
    ''' </remarks>
    Private Shared Function GetTransactionReferenceNumber(ByVal rawResponse As String) As String
        Dim reader As XmlTextReader = Nothing
        Dim doc As XmlDocument = Nothing
        Dim node As XmlNode = Nothing
        Dim nodePath As String = String.Empty
        Dim retVal As String = String.Empty

        If Not Utility.IsNullOrEmpty(rawResponse) Then
            Try
                ' Load the reader w/o the namespaces
                reader = New XmlTextReader(New StringReader(rawResponse))
                reader.Namespaces = False

                ' Create the document and load the XML
                doc = New XmlDocument
                doc.Load(reader)

                ' Get the ReferenceNumber node path based on the root node type
                Select Case doc.DocumentElement.Name
                    Case "CreditCardSaleResponse"
                        nodePath = "/CreditCardSaleResponse/Response/Transaction/ReferenceNumber"

                    Case "DebitCardSaleResponse"
                        nodePath = "/DebitCardSaleResponse/Response/Transaction/ReferenceNumber"

                    Case Else
                        ' May need to check in the future
                End Select

                ' Parse the response child nodes
                If doc.SelectSingleNode(nodePath) IsNot Nothing Then
                    node = doc.SelectSingleNode(nodePath)

                    retVal = node.InnerText.Trim
                End If

            Catch ex As Exception
                Throw New Exception("Failed to retrieve the reference number from the response: " & ex.Message)

            Finally
                ' Clean up a bit
                If node IsNot Nothing Then
                    node = Nothing
                End If

                If doc IsNot Nothing Then
                    doc = Nothing
                End If

                If reader IsNot Nothing Then
                    reader = Nothing
                End If
            End Try
        End If

        Return retVal.Trim
    End Function


    ''' <summary>
    ''' Initializes the payment method panels.
    ''' </summary>
    ''' <remarks>
    ''' The various UI elements are based on the client's allowed payment types.
    ''' </remarks>
    Private Sub InitializePaymentOptions()
        Dim payTypeCount As Int32

        ' Set the ACH panel visibility
        If BLL.SessionManager.Cart.AcceptECheck Then
            pnlPaymentACH.Visible = Not BLL.SessionManager.ACHBlocked
            ' Set the ACH tab/panel visibility
            tabAch.Visible = Not BLL.SessionManager.ACHBlocked
            pnlTabAch.Visible = Not BLL.SessionManager.ACHBlocked

        Else
            tabAch.Visible = BLL.SessionManager.Cart.AcceptECheck
            pnlPaymentACH.Visible = BLL.SessionManager.Cart.AcceptECheck
            pnlPaymentACH.Disabled = Not BLL.SessionManager.Cart.AcceptECheck
        End If

        ' Set up the payment type panels
        If Not BLL.SessionManager.UseSingleFee Then

            If Not BLL.SessionManager.CreditCardBlocked Then

                'Set the credit card panel visibility
                pnlPaymentCredit.Visible = BLL.SessionManager.Cart.AcceptCreditCards
                pnlPaymentCredit.Disabled = Not BLL.SessionManager.Cart.AcceptCreditCards

                litCreditHeading.Text = "Credit"

                ' Set the PIN debit panel visibility
                pnlPaymentDebit.Visible = BLL.SessionManager.Cart.AcceptPinDebit
                pnlPaymentDebit.Disabled = Not BLL.SessionManager.Cart.AcceptPinDebit

                ' Set the PIN debit panel visibility -- set to false for now
                pnlPaymentVisaDebit.Visible = False    'BLL.SessionManager.Cart.AcceptVisa
                pnlPaymentVisaDebit.Disabled = True    'Not BLL.SessionManager.Cart.AcceptVisa

                'Could do the same in the future for gift and PNM panels, too.
            Else
                pnlPaymentCredit.Visible = False
                pnlPaymentDebit.Visible = False
                pnlPaymentCredit.Disabled = True
                pnlPaymentDebit.Disabled = True
                pnlPaymentVisaDebit.Visible = False
                pnlPaymentVisaDebit.Disabled = True
                pnlTabManual.Visible = False
                pnlTabCardSwipe.Visible = False
                pnlPaymentTypes.Visible = False
                tabCardSwipe.Visible = False
                tabManual.Visible = False
                pnlReaderStatus.Visible = False
            End If
        Else

            If Not BLL.SessionManager.CreditCardBlocked Then

                ' Set the credit card panel visibility
                pnlPaymentCredit.Visible = True
                pnlPaymentCredit.Disabled = False

                litCreditHeading.Text = "Credit / Debit"

                ' Set the PIN debit panel visibility
                pnlPaymentDebit.Visible = False
                pnlPaymentDebit.Disabled = True

                ' Set the PIN debit panel visibility
                pnlPaymentVisaDebit.Visible = False
                pnlPaymentVisaDebit.Disabled = True
            Else
                pnlPaymentCredit.Visible = False
                pnlPaymentDebit.Visible = False
                pnlPaymentCredit.Disabled = True
                pnlPaymentDebit.Disabled = True
                pnlPaymentVisaDebit.Visible = False
                pnlPaymentVisaDebit.Disabled = True
                pnlTabManual.Visible = False
                pnlTabCardSwipe.Visible = False
                pnlPaymentTypes.Visible = False
                tabCardSwipe.Visible = False
                tabManual.Visible = False
                pnlReaderStatus.Visible = False
            End If
        End If

        ' Get the count of the allowed payment types
        If pnlPaymentCredit.Visible Then payTypeCount += 1
        If pnlPaymentDebit.Visible Then payTypeCount += 1
        If pnlPaymentVisaDebit.Visible Then payTypeCount += 1
        If pnlPaymentACH.Visible Then payTypeCount += 1

        ' Make sure we can take a payment for the cart 
        If payTypeCount <> 0 Then
            pnlFormContents.Visible = True
            pnlFormContents.Disabled = False

            pnlPaymentTypes.Visible = True
            pnlPaymentTypes.Disabled = False

            pnlReaderStatus.Visible = True
            pnlReaderStatus.Disabled = False

            ' Use the payment type count to evenly space payment type panels
            Select Case payTypeCount
                Case 0
                    Response.Redirect("/error/CounterPayment.aspx", False)
                Case 1, 2
                    pnlPaymentCredit.Attributes.Add("class", "col-xs-12 col-sm-6")
                    pnlPaymentDebit.Attributes.Add("class", "col-xs-12 col-sm-6")
                    pnlPaymentVisaDebit.Attributes.Add("class", "col-xs-12 col-sm-6")
                    pnlPaymentACH.Attributes.Add("class", "col-xs-12 col-sm-6")
                Case 3
                    pnlPaymentCredit.Attributes.Add("class", "col-xs-12 col-sm-4")
                    pnlPaymentDebit.Attributes.Add("class", "col-xs-12 col-sm-4")
                    pnlPaymentVisaDebit.Attributes.Add("class", "col-xs-12 col-sm-4")
                    pnlPaymentACH.Attributes.Add("class", "col-xs-12 col-sm-4")
            End Select
        Else
            pnlFormContents.Visible = False
            pnlFormContents.Disabled = True

            pnlPaymentTypes.Visible = False
            pnlPaymentTypes.Disabled = True

            pnlReaderStatus.Visible = False
            pnlReaderStatus.Disabled = True

            ' Alert the user
            psmErrorMessage.ToggleStatusMessage("Invalid cart and unable to process a payment.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
        End If

    End Sub


    ''' <summary>
    ''' Processes a manual entry credit card payment.
    ''' </summary>
    ''' <returns>
    ''' A B2P.Payment.CreditCardPayment.CreditCardPaymentResults object.
    ''' </returns>
    Private Function ProcessPaymentCredit() As B2P.Payment.CreditCardPayment.CreditCardPaymentResults
        Dim payment As B2P.Payment.CreditCardPayment = Nothing
        Dim card As B2P.Common.Objects.CreditCard = Nothing
        Dim feeCalcType As B2P.Payment.FeeCalculation.PaymentTypes
        Dim ccpr As B2P.Payment.CreditCardPayment.CreditCardPaymentResults = Nothing
        Dim errMsg As String = String.Empty

        Try
            If Not Utility.IsNullOrEmpty(BLL.SessionManager.Token) AndAlso BLL.SessionManager.Cart.TotalAmount > 0 Then
                card = BLL.SessionManager.CreditCard
                If BLL.SessionManager.CustomerEmail <> "" Then
                    card.Owner.EMailAddress = BLL.SessionManager.CustomerEmail
                End If

                ' Put payment data together
                payment = New B2P.Payment.CreditCardPayment(BLL.SessionManager.ClientCode)
                payment.PaymentSource = B2P.Common.Enumerations.TransactionSources.Counter
                payment.VendorReferenceCode = BLL.SessionManager.TransactionInformation.VendorReferenceCode
                ' Set to false here because the confirmation page will send the email if it's available
                payment.AllowConfirmationEmails = False
                payment.Security_ID = BLL.SessionManager.SecurityID
                payment.Office_ID = BLL.SessionManager.OfficeID
                payment.ClientCode = BLL.SessionManager.ClientCode
                payment.UserData.UserField1 = String.Empty
                payment.UserData.UserField2 = String.Empty
                payment.UserData.UserField3 = String.Empty
                payment.UserData.UserField4 = String.Empty
                payment.UserData.UserField5 = String.Empty
                payment.UserComments = BLL.SessionManager.UserComments

                'payment.UserComments = String.Empty
                'payment.AppCode = String.Empty

                ' Get the fee amount based on the card type
                Select Case Utility.GetPaymentCardType(card.BINNumber)
                    Case "C"
                        feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.CreditCard
                    Case "D", "U"
                        feeCalcType = B2P.Payment.FeeCalculation.PaymentTypes.PinDebit
                End Select

                ' Get the cart items -- non-tax for now, but will need to revisit to handle tax items
                payment.Items = BLL.SessionManager.Cart.GetCartItems(feeCalcType, B2P.ShoppingCart.Cart.CartItemsTypes.Nontax)


                ' Process and get the results
                ccpr = payment.PayByCreditCard(card)
            Else
                B2P.Common.Logging.LogError("B2P Admin -->", "Credit payment error: Amount=" & BLL.SessionManager.Cart.TotalAmount.ToString, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/error", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Finally
            'Clean up a bit
            If payment IsNot Nothing Then
                payment = Nothing
            End If

            If card IsNot Nothing Then
                card = Nothing
            End If
        End Try

        Return ccpr
    End Function
    <WebMethod>
    Public Shared Sub LogAjaxError(ByVal functionName As String, ByVal xhrError As String)
        Dim errMsg As String = String.Empty

        ' Build the error message
        'errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath, _
        '                                   ex.Source, ex.TargetSite.DeclaringType.Name, ex.TargetSite.Name, _
        '                                   ex.Message)
        errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                               "B2PAdmin", "PinPad", functionName,
                                               "Credit was selected, but debit was swiped. Reversing TriPOS transaction.")

        B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
    End Sub


    ''' <summary>
    ''' Processes a reversal request results.
    ''' </summary>
    ''' <param name="results">The reversal response results.</param>
    ''' <param name="lane">The lane ID for the reversal request.</param>
    ''' <param name="status">The B2P status.</param>
    ''' <param name="reason">The reversal reason.</param>
    ''' <param name="voidLink">The reversal link for the transaction.</param>
    ''' <returns>String.</returns>
    <WebMethod>
    Public Shared Function ProcessReversalResults(ByVal results As String, ByVal lane As Int32, ByVal status As String, ByVal reason As String, ByVal voidLink As String) As String
        Dim response As Entities.ReversalResponse = Nothing
        Dim responseMsg As String = String.Empty
        Dim retVal As Int32
        Dim errMsg As String = String.Empty

        Try
            If Not Utility.IsNullOrEmpty(results.Trim) Then

                ' Get the response object
                response = JsonConvert.DeserializeObject(Of Entities.ReversalResponse)(results.Trim)

                ' Create the connection
                Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                    ' Create the command
                    Using cmd As New SqlClient.SqlCommand
                        cmd.CommandText = "ap_CancelTriposTrans"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = conn

                        ' Assign the param values
                        cmd.Parameters.Add("@Token", SqlDbType.VarChar, 32).Value = BLL.SessionManager.Token.Trim
                        cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = Convert.ToInt32(BLL.SessionManager.ReferenceNumber.Trim)
                        'cmd.Parameters.Add("@LaneID", SqlDbType.Int).Value = BLL.SessionManager.LaneID
                        cmd.Parameters.Add("@B2PStatus", SqlDbType.VarChar, 30).Value = Utility.IIf(Of String)(response.IsApproved, status, "REVERSEFAILED")
                        cmd.Parameters.Add("@Canceled", SqlDbType.Bit).Value = Utility.IIf(Of Int32)(response.IsApproved, 1, 0)
                        cmd.Parameters.Add("@CancelLaneID", SqlDbType.Int).Value = lane
                        cmd.Parameters.Add("@CancelReason", SqlDbType.VarChar, 100).Value = reason
                        cmd.Parameters.Add("@CancelLink", SqlDbType.VarChar, 100).Value = voidLink.Trim
                        cmd.Parameters.Add("@CancelDate", SqlDbType.DateTime).Value = response.TransactionDateTime
                        cmd.Parameters.Add("@CancelRawResponse", SqlDbType.VarChar, -1).Value = response.Processor.RawResponse
                        cmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output

                        ' Open the database connection
                        conn.Open()

                        ' Execute the query
                        cmd.ExecuteNonQuery()

                        ' Get the return value
                        retVal = Convert.ToInt32(cmd.Parameters("@RetVal").Value)
                    End Using

                End Using

                ' Log the failed reversal attempt
                If Not response.IsApproved Then
                    errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                    "B2PAdmin", "PinPad", "ProcessReversalResults",
                    "Unable to reverse TriPOS transaction #" & response.TransactionID & ".")

                    B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.Yes)
                End If

            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
            ex.Source, ex.TargetSite.DeclaringType.Name,
            ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

        Return responseMsg
    End Function

    ''' <summary>
    ''' Processes a TriPOS sale response.
    ''' </summary>
    ''' <param name="response">The TriPOS sale response.</param>
    ''' <returns>String.</returns>
    <WebMethod>
    Public Shared Function ProcessSaleResponse(ByVal response As String) As String    ' ByVal paymentType As Entities.PaymentType, 
        Dim saleResponse As Entities.SaleResponse = Nothing
        Dim paymentType As Entities.PaymentType
        Dim feeAmount As Decimal
        Dim cardsAllowed As List(Of String) = GetCardsAllowed()
        Dim feeResponse As B2P.triPOS.Utility.Payments.FeeResponse = Nothing
        Dim accountConfig As ClientAccountConfiguration = ClientAccountConfiguration.GetConfiguration(BLL.SessionManager.ClientCode)
        Dim receipt As Entities.Receipt = Nothing
        Dim transAdded As Boolean
        Dim feeReversed As Boolean
        Dim failErrMsg As String = String.Empty
        Dim responseMsg As String = String.Empty
        Dim errMsg As String = String.Empty

        Try
            ' Get the response object
            saleResponse = JsonConvert.DeserializeObject(Of Entities.SaleResponse)(response.Trim)

            ' Determine how the client gets the payment type -> BIN lookup or from the PIN pad selection
            If BLL.SessionManager.UseBinLookup Then
                Select Case Utility.GetPaymentCardType(saleResponse.BinValue)
                    Case "C"
                        paymentType = Entities.PaymentType.Credit
                    Case "D", "U"
                        paymentType = Entities.PaymentType.Debit
                End Select
            Else
                If Not Utility.IsNullOrEmpty(saleResponse.PaymentType.Trim) Then
                    paymentType = DirectCast([Enum].Parse(GetType(Entities.PaymentType), saleResponse.PaymentType.Trim), Entities.PaymentType)
                End If
            End If

            ' Get the fee amount based on the response's payment type
            Select Case paymentType
                Case Entities.PaymentType.Credit
                    feeAmount = BLL.SessionManager.Cart.CreditCardFee
                Case Entities.PaymentType.Debit
                    feeAmount = BLL.SessionManager.Cart.PinDebitFee

                    ' May need to address this at a later date
                    'If saleResponse.PinVerified OrElse BLL.SessionManager.UseBinLookup Then
                    '    feeAmount = BLL.SessionManager.Cart.PinDebitFee
                    'Else
                    '    feeAmount = BLL.SessionManager.Cart.DebitFee
                    'End If
            End Select

            ' Update the TriPOS trans data
            UpdateTriposTransaction(saleResponse, paymentType)

            If saleResponse.IsApproved Then
                ' Make sure the customer used a card type that is allowed by the merchant
                If Not cardsAllowed.Contains(saleResponse.CardLogo) Then
                    ' Customer used a card type that is not allowed by the merchant
                    ' Log the B2P void info
                    errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                           "B2P.TriPOS.Manatron.Web", "PinPad", "ProcessSaleResponse",
                                                           "The " & saleResponse.CardLogo.ToUpper & " card used is not accepted by this merchant. Reversing TriPOS transaction #" & saleResponse.TransactionID & ".")

                    B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)

                    ' Add the failed transaction to the database
                    transAdded = AddFailedTransaction(saleResponse, paymentType, feeResponse, "TriPOS: The card brand used is not accepted by this merchant.")

                    ' Build a REVERSAL request
                    responseMsg = "{""status"":""PaymentTypeNotAccepted"",""statusMessage"":""The card brand used is not accepted by this merchant."",""statusCode"":-9001,""hasCancel"":true,""cancelItem"":{""link"":" &
                                      GetReversalTransactionLink(saleResponse.Links) & ",""request"":" & JsonConvert.SerializeObject(GetReversalRequest(), Newtonsoft.Json.Formatting.Indented) & ",""reason"":""The card brand used is not accepted by this merchant.""}}"

                    ' Send the messgae back to the clerk
                    Return responseMsg
                End If

                ' Make a call to Ken's service for the tokenized fee payment
                '     Need the following params:
                '       -- merchantID    --> use the ConvenienceMerchant
                '       -- feeMerchantID --> use the TaxFeeMerchant
                '       -- transactionID --> use saleResponse.TransactionID
                '       -- amount        --> use feeAmount
                '       -- batch_ID      --> use BLL.SessionManager.ReferenceNumber
                If Not BLL.SessionManager.UseSingleFee AndAlso feeAmount > Convert.ToDecimal(0) Then
                    feeResponse = B2P.triPOS.Utility.Payments.ProcessFee(accountConfig.ConvenienceMerchant, accountConfig.TaxFeeMerchant, saleResponse.TransactionID, feeAmount, Convert.ToInt32(BLL.SessionManager.ReferenceNumber))

                    If feeResponse.Result <> B2P.Common.Enumerations.ReturnCodes.Success Then
                        ' Log the B2P reversal info
                        errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                               "B2P.TriPOS.Manatron.Web", "PinPad", "ProcessSaleResponse",
                                                               "The fee payment transaction request failed. Reversing TriPOS transaction #" & saleResponse.TransactionID & ".")

                        B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)

                        ' Add the failed transaction to the database
                        transAdded = AddFailedTransaction(saleResponse, paymentType, feeResponse, "TriPOS: The fee payment request failed.")

                        ' Build a REVERSAL request
                        responseMsg = "{""status"":""FeePaymentFailed"",""statusMessage"":""The fee payment transaction request failed."",""statusCode"":-9005,""hasCancel"":true,""cancelItem"":{""link"":" &
                                          GetReversalTransactionLink(saleResponse.Links) & ",""request"":" & JsonConvert.SerializeObject(GetReversalRequest(), Newtonsoft.Json.Formatting.Indented) & ",""reason"":""The fee payment transaction request failed.""}}"

                        ' Send the messgae back to the clerk
                        Return responseMsg
                    End If
                End If

                'Made it to here -- try to add the transaction to the B2P database
                transAdded = AddTransaction(saleResponse, paymentType, feeResponse)

                ' For testing fee reversal
                'transAdded = False

                ' See if the transaction was added -- if not, REVERSE it
                If transAdded Then
                    ' Update payment made status
                    BLL.SessionManager.PaymentMade = True

                    ' Update the B2P status in the TriPOS table
                    UpdateTriposStatus("COMPLETED")

                    ' Get the receipt
                    receipt = BuildTransactionReceipt(saleResponse, paymentType)

                    ' Save receipt for printing after Aumentum finalizes the transaction
                    SaveTransactionReceipt(receipt, BLL.SessionManager.ReferenceNumber, BLL.SessionManager.Token)

                    ' Save receipt for printing
                    BLL.SessionManager.TransactionReceipt = receipt

                    ' Build the response message
                    responseMsg = "{""status"":""" & saleResponse.StatusCode & """,""statusMessage"":""" & saleResponse.Processor.ExpressResponseMessage & """,""statusCode"":" & saleResponse.Processor.ExpressResponseCode & "}"

                    ' Do postback
                    postBackEMV()
                Else
                    ' Log the B2P failed attempt and reversal info
                    errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                           "B2P.TriPOS.Manatron.Web", "PinPad", "ProcessSaleResponse",
                                                           "Unable to add TriPOS transaction to B2P database. Reversing TriPOS transaction #" & saleResponse.TransactionID & ".")

                    B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)

                    ' Build a reversal request
                    responseMsg = "{""status"":""Error"",""statusMessage"":""Unable to save the transaction. Please try again."",""statusCode"":-9004,""hasCancel"":true,""cancelItem"":{""link"":" &
                                      GetReversalTransactionLink(saleResponse.Links) & ",""request"":" & JsonConvert.SerializeObject(GetReversalRequest(), Newtonsoft.Json.Formatting.Indented) & "}}"

                    ' See if we need to reverse a fee payment
                    If Not BLL.SessionManager.UseSingleFee AndAlso feeResponse IsNot Nothing Then
                        feeReversed = B2P.triPOS.Utility.Payments.VoidFee(accountConfig.ConvenienceMerchant, Convert.ToInt32(BLL.SessionManager.ReferenceNumber), feeAmount, feeResponse.AuthorizationToken)
                    End If
                End If
            Else
                ' Get the failure message
                If saleResponse.StatusCode.Trim.ToUpper <> "NONE" Then
                    If Not Utility.IsNullOrEmpty(saleResponse.Processor.ExpressResponseMessage) Then
                        failErrMsg = saleResponse.Processor.ExpressResponseMessage
                    Else
                        If saleResponse.HasErrors Then
                            failErrMsg = saleResponse.Errors(0).DeveloperMessage.Replace("[", "").Replace("]", "").Replace(Environment.NewLine, " ").Trim
                        End If
                    End If
                Else
                    If saleResponse.HasErrors Then
                        failErrMsg = saleResponse.Errors(0).DeveloperMessage.Replace("[", "").Replace("]", "").Replace(Environment.NewLine, " ").Trim
                    Else
                        failErrMsg = "Error occurred."
                    End If
                End If

                ' Add the failed transaction to the B2P database
                transAdded = AddFailedTransaction(saleResponse, paymentType, feeResponse, "TriPOS: " & failErrMsg)

                ' Make sure the status <> NONE --> response object properties tend to be NULL
                If saleResponse.StatusCode.Trim.ToUpper <> "NONE" Then
                    UpdateTriposStatus(saleResponse.StatusCode.ToUpper)

                    ' Get the receipt
                    receipt = BuildTransactionReceipt(saleResponse, paymentType)

                    ' Save receipt for printing
                    BLL.SessionManager.TransactionReceipt = receipt

                    ' Build the response message
                    responseMsg = "{""status"":""" & saleResponse.StatusCode & """,""statusMessage"":""" & failErrMsg & """,""statusCode"":" & saleResponse.Processor.ExpressResponseCode & ",""isEmv"":" & Utility.IIf(Of String)(saleResponse.Emv IsNot Nothing, "true", "false") & "}"
                Else
                    ' There should be errors with response status code = NONE. We may want to
                    ' revisit this logic if we notice different statuses in the database.
                    UpdateTriposStatus("ERROR")

                    responseMsg = "{""status"":""Error"",""statusMessage"":""" & failErrMsg & """,""statusCode"":" & saleResponse.Processor.ExpressResponseCode & ",""isEmv"":" & Utility.IIf(Of String)(saleResponse.Emv IsNot Nothing, "true", "false") & "}"
                End If
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Errors/Error.aspx", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Finally
            ' Clean up a bit
            If receipt IsNot Nothing Then
                receipt = Nothing
            End If

            If feeResponse IsNot Nothing Then
                feeResponse = Nothing
            End If

            If cardsAllowed IsNot Nothing Then
                cardsAllowed = Nothing
            End If

            If accountConfig IsNot Nothing Then
                accountConfig = Nothing
            End If

            If saleResponse IsNot Nothing Then
                saleResponse = Nothing
            End If
        End Try

        Return responseMsg
    End Function



    ''' <summary>
    ''' Adds client side javascript to the various server controls.
    ''' </summary>
    Private Sub RegisterClientJs()
        btnSwipeCard.Attributes.Add("onClick", "return startProcessing()")
        btnSubmitCardInfo.Attributes.Add("onClick", "return validateForm('CC')")
        btnSubmitAch.Attributes.Add("onClick", "return validateForm('ACH')")
        btnClearCardInfo.Attributes.Add("onClick", "return clearValidationItems()")
    End Sub


    ''' <summary>
    ''' Saves a transaction receipt to the database for later retrieval. 
    ''' </summary>
    ''' <param name="transReceipt">A receipt for a successful transaction.</param>
    ''' <returns>Boolean.</returns>
    Private Shared Function SaveTransactionReceipt(ByVal transReceipt As Entities.Receipt, ByVal batchId As String, ByVal token As String) As Boolean
        Dim retVal As Boolean
        Dim errMsg As String = String.Empty

        Try
            ' Create the connection
            Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand
                    cmd.CommandText = "ap_SaveReceipt"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@ClientCode", SqlDbType.VarChar, 10).Value = BLL.SessionManager.ClientCode
                    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = Convert.ToInt32(batchId.Trim)
                    cmd.Parameters.Add("@Token", SqlDbType.VarChar, 32).Value = token.Trim
                    cmd.Parameters.Add("@Receipt", SqlDbType.VarChar, -1).Value = JsonConvert.SerializeObject(transReceipt, Newtonsoft.Json.Formatting.None)
                    cmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Get the return value
                    If Convert.ToInt32(cmd.Parameters("@RetVal").Value) > 0 Then
                        retVal = True
                    End If
                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                           ex.Source, ex.TargetSite.DeclaringType.Name,
                                           ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.Yes)
            HttpContext.Current.Response.Redirect("/Errors/Error.aspx", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            retVal = False
        End Try

        Return retVal
    End Function



    ''' <summary>
    ''' Updates the status of a TriPOS transaction.
    ''' </summary>
    ''' <param name="status">The updated status.</param>
    ''' <returns>Integer.</returns>
    Private Shared Function UpdateTriposStatus(ByVal status As String) As Int32
        Dim retVal As Int32
        Dim errMsg As String = String.Empty

        Try
            ' Create the connection
            Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand
                    cmd.CommandText = "ap_UpdateTriposStatus"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@Token", SqlDbType.VarChar, 32).Value = BLL.SessionManager.Token.Trim
                    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = Convert.ToInt32(BLL.SessionManager.ReferenceNumber.Trim)
                    cmd.Parameters.Add("@B2PStatus", SqlDbType.VarChar, 20).Value = status.Trim
                    cmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Get the return value
                    retVal = Convert.ToInt32(cmd.Parameters("@RetVal").Value)
                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            ' Set return value???
            retVal = -9
        End Try

        Return retVal
    End Function

    ''' <summary>
    ''' Updates the information for TriPOS transaction. 
    ''' </summary>
    ''' <param name="response">An SaleResponse object that contains the current transaction information.</param>
    ''' <param name="paymentType"></param>
    ''' <returns>Integer.</returns>
    Private Shared Function UpdateTriposTransaction(ByVal response As Entities.SaleResponse, ByVal paymentType As Entities.PaymentType) As Int32
        Dim retVal As Int32
        Dim errMsg As String = String.Empty

        Try
            ' Create the connection
            Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand
                    cmd.CommandText = "ap_UpdateTriposTrans"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@Token", SqlDbType.VarChar, 32).Value = BLL.SessionManager.Token.Trim

                    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                    ' NOTE: Checking if the lane status is "NotInUse" should make this conditional obsolete. If we 
                    ' notice that there is an issue with the reference numbers not being in sync, then we can add
                    ' this back to get the reference number from the raw response.
                    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                    'If Not Utility.IsNullOrEmpty(response.Processor.RawResponse) Then
                    '    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = Convert.ToInt32(GetTransactionReferenceNumber(response.Processor.RawResponse))    'GetTransactionReferenceNumber(paymentType, response.Processor.RawResponse)
                    'Else
                    '    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = Convert.ToInt32(BLL.SessionManager.ReferenceNumber.Trim)
                    'End If
                    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                    cmd.Parameters.Add("@Batch_ID", SqlDbType.Int).Value = Convert.ToInt32(BLL.SessionManager.ReferenceNumber.Trim)

                    'cmd.Parameters.Add("@LaneID", SqlDbType.Int).Value = BLL.SessionManager.LaneID
                    cmd.Parameters.Add("@ClientCode", SqlDbType.VarChar, 10).Value = BLL.SessionManager.ClientCode.Trim
                    cmd.Parameters.Add("@OfficeID", SqlDbType.Int).Value = BLL.SessionManager.OfficeID

                    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                    ' NOTE: Payment type is no longer tracking what the clerk selects.
                    '     --> PaymentTypeSelected is what the customer selected on the PIN pad.
                    '     --> PaymentType is what is returned from a BIN lookup.
                    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                    cmd.Parameters.Add("@PaymentTypeSelected", SqlDbType.VarChar, 10).Value = response.PaymentType.Trim                     ' paymentType.ToString

                    ' Need to confirm U should be set to debit fee
                    Select Case Utility.GetPaymentCardType(response.BinValue.Trim)
                        Case "C"
                            cmd.Parameters.Add("@PaymentType", SqlDbType.VarChar, 10).Value = Entities.PaymentType.Credit.ToString
                        Case "D", "U"
                            cmd.Parameters.Add("@PaymentType", SqlDbType.VarChar, 10).Value = Entities.PaymentType.Debit.ToString
                    End Select

                    cmd.Parameters.Add("@CardEntryMode", SqlDbType.VarChar, 20).Value = response.EntryMode.Trim
                    cmd.Parameters.Add("@BinNumber", SqlDbType.VarChar, 6).Value = response.BinValue
                    cmd.Parameters.Add("@ResponseCode", SqlDbType.VarChar, 10).Value = response.Processor.HostResponseCode.Trim
                    cmd.Parameters.Add("@ResponseMessage", SqlDbType.VarChar, 1024).Value = response.Processor.HostResponseMessage.Trim

                    If response.Signature IsNot Nothing Then
                        cmd.Parameters.Add("@SignatureFormat", SqlDbType.VarChar, 50).Value = response.Signature.SignatureFormat
                        cmd.Parameters.Add("@SignatureData", SqlDbType.VarBinary, -1).Value = response.Signature.SignatureData
                        cmd.Parameters.Add("@SignatureStatusCode", SqlDbType.VarChar, 50).Value = response.Signature.SignatureStatusCode
                    Else
                        cmd.Parameters.Add("@SignatureFormat", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                        cmd.Parameters.Add("@SignatureData", SqlDbType.VarBinary, -1).Value = System.DBNull.Value
                        cmd.Parameters.Add("@SignatureStatusCode", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                    End If

                    If response.Emv IsNot Nothing Then
                        If Not Utility.IsNullOrEmpty(response.Emv.ApplicationPreferredName) Then
                            cmd.Parameters.Add("@EmvPaymentName", SqlDbType.VarChar, 50).Value = response.Emv.ApplicationPreferredName
                        Else
                            cmd.Parameters.Add("@EmvPaymentName", SqlDbType.VarChar, 50).Value = response.Emv.ApplicationLabel
                        End If

                        cmd.Parameters.Add("@EmvApplicationID", SqlDbType.VarChar, 50).Value = response.Emv.ApplicationIdentifier
                        cmd.Parameters.Add("@EmvCryptogram", SqlDbType.VarChar, 50).Value = response.Emv.Cryptogram
                    Else
                        cmd.Parameters.Add("@EmvPaymentName", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                        cmd.Parameters.Add("@EmvApplicationID", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                        cmd.Parameters.Add("@EmvCryptogram", SqlDbType.VarChar, 50).Value = System.DBNull.Value
                    End If

                    cmd.Parameters.Add("@RawResponse", SqlDbType.VarChar, -1).Value = response.Processor.RawResponse.Trim

                    If response.HasErrors Then
                        cmd.Parameters.Add("@HasError", SqlDbType.Bit).Value = 1
                        cmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar, 1024).Value = BuildTriposErrorMessage(response.Errors)
                    Else
                        cmd.Parameters.Add("@HasError", SqlDbType.Bit).Value = 0
                        cmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar, 1024).Value = System.DBNull.Value
                    End If

                    cmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Get the return value
                    retVal = Convert.ToInt32(cmd.Parameters("@RetVal").Value)
                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            ' Set return value???
            retVal = -9
        End Try

        Return retVal
    End Function



    ''' <summary>
    ''' Validate the form fields.
    ''' </summary>
    ''' <returns>Boolean.</returns>
    Private Function ValidateForm(ByVal formType As String) As Boolean
        Dim errFound As Boolean
        Dim errMsg As String = "<span class=""bold"">" & GetGlobalResourceObject("WebResources", "ErrMsgHeader").ToString() & "</span><br />"

        ' Check the form fields
        Select Case formType
            Case "ACH"
                If Not Utility.IsValidField(pbaEnterBankAccountInfo.FirstName.Trim, FieldValidationType.Name) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgFirstName").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pbaEnterBankAccountInfo.LastName.Trim, FieldValidationType.Name) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgLastName").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pbaEnterBankAccountInfo.BankAccountType.Trim, FieldValidationType.NonEmpty) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pbaEnterBankAccountInfo.BankRoutingNumber.Trim, FieldValidationType.RoutingNumber) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgRoutingNumber").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pbaEnterBankAccountInfo.BankAccountNumber.Trim, FieldValidationType.BankAccount) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgBankAccount").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pbaEnterBankAccountInfo.BankAccountNumber2.Trim, FieldValidationType.BankAccount) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgBankAccount").ToString() & "<br />"
                    errFound = True
                End If

                If pbaEnterBankAccountInfo.BankAccountNumber.Trim <> pbaEnterBankAccountInfo.BankAccountNumber2.Trim Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgBankAccount").ToString() & "<br />"
                    errFound = True
                End If

            Case "CC"
                If Not Utility.IsValidField(pccEnterCreditCardInfo.FirstName.Trim, FieldValidationType.Name) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgFirstName").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pccEnterCreditCardInfo.LastName.Trim, FieldValidationType.Name) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgLastName").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pccEnterCreditCardInfo.CreditCardNumber.Trim, FieldValidationType.CreditCard) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgCreditCardNumber").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pccEnterCreditCardInfo.ExpirationDate.Trim, FieldValidationType.ExpirationDate) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgCreditCardExpiry").ToString() & "<br />"
                    errFound = True
                End If

                If Not Utility.IsValidField(pccEnterCreditCardInfo.CVV.Trim, FieldValidationType.CVV) Then
                    errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgCreditCardCVC").ToString() & "<br />"
                    errFound = True
                End If
        End Select

        ' See if we need to display any error messages
        If errFound Then
            psmErrorMessage.ToggleStatusMessage(errMsg, StatusMessageType.Danger, StatusMessageSize.Normal, True, False)
        Else
            psmErrorMessage.ToggleStatusMessage(String.Empty, StatusMessageType.None, StatusMessageSize.Normal, False, False)
        End If

        Return Utility.IIf(Of Boolean)(errFound, False, True)
    End Function

    Private Sub btnSubmitAch_Click(sender As Object, e As EventArgs) Handles btnSubmitAch.Click
        Dim account As B2P.Common.Objects.BankAccount = Nothing
        Dim failureMsg As String = String.Empty
        Dim accountValid As Boolean
        Dim errMsg As String = String.Empty

        Me.CheckSession()

        Try
            '::::::::::::::::::::::::::::::::::::::::::::
            ' Look at HK Responsive site for ACH parsing
            '::::::::::::::::::::::::::::::::::::::::::::
            If ValidateForm("ACH") Then
                account = New B2P.Common.Objects.BankAccount

                ' Set the bank account properties
                account.Owner.FirstName = Regex.Replace(pbaEnterBankAccountInfo.FirstName.Trim, "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase)
                account.Owner.LastName = Regex.Replace(pbaEnterBankAccountInfo.LastName.Trim, "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase)
                account.Owner.Address1 = String.Empty
                account.Owner.Address2 = String.Empty
                account.Owner.City = String.Empty
                'account.Owner.CountryCode = pbaEnterBankAccountInfo.BankAccountCountry.Trim    ' REQUIRED! but already set to US by the referenced DLL
                account.Owner.PhoneNumber = String.Empty
                account.Owner.State = String.Empty  'FL
                account.Owner.ZipCode = String.Empty
                account.Owner.EMailAddress = String.Empty
                account.BankAccountNumber = pbaEnterBankAccountInfo.BankAccountNumber.Trim

                Select Case pbaEnterBankAccountInfo.BankAccountType
                    Case "Checking"
                        account.BankAccountType = B2P.Common.Objects.BankAccount.BankAccountTypes.Personal_Checking
                    Case "Savings"
                        account.BankAccountType = B2P.Common.Objects.BankAccount.BankAccountTypes.Personal_Savings
                    Case "CommChecking"
                        account.BankAccountType = B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Checking
                    Case "CommSavings"
                        account.BankAccountType = B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Savings
                End Select

                Select Case account.ValidateBankRoutingNumber(pbaEnterBankAccountInfo.BankRoutingNumber, B2P.Common.Objects.BankAccount.RoutingNumberValidationMode.FederalReserveLookup)
                    Case B2P.Common.Objects.BankAccount.ValidationStatus.Invalid
                        '::: Not sure this is needed, since this won't execute unless all the form fields are valid :::
                        'pbaEnterBankAccountInfo.RoutingNumberValidation = False
                        'regRoutingNumber.IsValid = False
                        '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

                        psmErrorMessage.ToggleStatusMessage("Missing or invalid routing number.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)

                        Exit Sub

                    Case B2P.Common.Objects.BankAccount.ValidationStatus.UseAlternate
                        account.RoutingNumberOption = B2P.Common.Objects.BankAccount.RoutingNumberOptions.UseReplacement
                        account.BankRoutingNumber = account.ValidationResponse.AlternateRoutingNumber

                    ' TODO: Show alternate routing number modal???
                    ' Match the info that is contained in: $/Bill2Pay/Payment Site/Website/B2P Payment Site/Bill2Pay/pay/RoutingMessage.aspx

                    Case B2P.Common.Objects.BankAccount.ValidationStatus.Valid
                        account.RoutingNumberOption = B2P.Common.Objects.BankAccount.RoutingNumberOptions.NoReplacement
                        account.BankRoutingNumber = pbaEnterBankAccountInfo.BankRoutingNumber.Trim

                    Case Else
                        ' Default action
                End Select

                ' Reset any routing number error messages
                psmErrorMessage.ToggleStatusMessage(String.Empty, StatusMessageType.None, StatusMessageSize.Normal, False, False)

                ' Do we need to validate the bank account
                If B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode).UseATMVerify Then
                    If Not B2P.Common.Objects.BankAccount.ValidateBankAccount(BLL.SessionManager.ClientCode, pbaEnterBankAccountInfo.BankAccountNumber.Trim, account.BankRoutingNumber, BLL.SessionManager.Cart.TotalAmount, account.BankAccountType) Then
                        pbaEnterBankAccountInfo.BankAccountNumber = String.Empty
                        pbaEnterBankAccountInfo.BankAccountNumber2 = String.Empty

                        ' Set validation failure modal text
                        If Not Utility.IsNullOrEmpty(B2P.Objects.Client.GetClientMessage("ACHVerifyFail", BLL.SessionManager.ClientCode).Trim) Then
                            failureMsg = B2P.Objects.Client.GetClientMessage("ACHVerifyFail", BLL.SessionManager.ClientCode).Trim
                        Else
                            failureMsg = B2P.Objects.Client.GetClientMessage("ACHVerifyFail", "DEFAULT").Trim
                        End If

                        litAchInvalidMsg.Text = failureMsg

                        ' Show the modal
                        Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlAchInvalidModal').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
                    Else
                        accountValid = True
                    End If
                Else
                    accountValid = True
                End If

                ' Check if everything's ok
                If accountValid Then
                    BLL.SessionManager.BankAccount = account
                    BLL.SessionManager.PaymentType = B2P.Common.Enumerations.PaymentTypes.BankAccount

                    ' Process the payment
                    ProcessPaymentACH()
                End If
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                               ex.Source, ex.TargetSite.DeclaringType.Name,
                                               ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Finally
            'Clean up a bit
            If account IsNot Nothing Then
                account = Nothing
            End If
        End Try
    End Sub
    ''' <summary>
    ''' Processes an ACH payment.
    ''' </summary>
    Private Sub ProcessPaymentACH()
        Dim payment As B2P.Payment.BankAccountPayment = Nothing
        Dim account As B2P.Common.Objects.BankAccount = Nothing
        Dim bapr As B2P.Payment.BankAccountPayment.BankAccountPaymentResults = Nothing

        Dim errMsg As String = String.Empty

        Try
            If Not Utility.IsNullOrEmpty(BLL.SessionManager.Token) AndAlso BLL.SessionManager.Cart.TotalAmount > 0 Then
                If Not BLL.SessionManager.PaymentMade Then
                    account = BLL.SessionManager.BankAccount
                    'If BLL.SessionManager.CustomerEmail <> "" Then
                    account.Owner.EMailAddress = BLL.SessionManager.CustomerEmail
                    'End If


                    ' Get the originator
                    Select Case account.BankAccountType
                        Case B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Checking
                            BLL.SessionManager.OriginatorID = B2P.Payment.BankAccountPayment.GetCompanyID(BLL.SessionManager.ClientCode)
                        Case B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Savings
                            BLL.SessionManager.OriginatorID = B2P.Payment.BankAccountPayment.GetCompanyID(BLL.SessionManager.ClientCode)
                        Case Else
                            BLL.SessionManager.OriginatorID = String.Empty
                    End Select

                    ' Put payment data together
                    payment = New B2P.Payment.BankAccountPayment(BLL.SessionManager.ClientCode)
                    payment.PaymentSource = B2P.Common.Enumerations.TransactionSources.Counter
                    payment.VendorReferenceCode = BLL.SessionManager.Token
                    payment.AllowConfirmationEmails = True
                    payment.Security_ID = BLL.SessionManager.SecurityID
                    payment.Office_ID = BLL.SessionManager.OfficeID
                    payment.UserComments = BLL.SessionManager.UserComments

                    ' Do we need to rebuild the cart and loop through the items again? Can we do this?
                    payment.Items = BLL.SessionManager.Cart.GetCartItems(B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, B2P.ShoppingCart.Cart.CartItemsTypes.All)

                    ' Process and get the results
                    bapr = payment.PayByBankAccount(account)

                    If bapr.Result = B2P.Payment.BankAccountPayment.BankAccountPaymentResults.Results.Success Then
                        BLL.SessionManager.PaymentMade = True
                        BLL.SessionManager.ConfirmationNumber = bapr.ConfirmationNumber.TrimStart(CChar("0"))
                        ' Do the postback
                        postBackACH()

                        Response.Redirect("/payment/PinPadReturnChip.aspx", False)
                    Else
                        BLL.SessionManager.PaymentMade = False
                        Session("AuthorizationCode") = ""

                        psmErrorMessage.ToggleStatusMessage("Bank account payment failed.", StatusMessageType.Danger, True, True)

                        pccEnterCreditCardInfo.Clear()

                        ' Build the response message
                        ' Toggle the StatusMessage control
                        'responseMsg = "{""statusMessage"":""" & ccpr.Message & """,""statusCode"":" & ccpr.ReasonCode & "}"
                    End If
                Else
                    Response.Redirect("/Error", False)
                End If
            Else
                B2P.Common.Logging.LogError("Express Payment", "ACH payment error: Amount=" & BLL.SessionManager.Cart.TotalAmount, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/error", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error/", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Finally
            'Clean up a bit
            If bapr IsNot Nothing Then
                bapr = Nothing
            End If

            If payment IsNot Nothing Then
                payment = Nothing
            End If
        End Try
    End Sub
    Private Sub btnCancelAch_Click(sender As Object, e As EventArgs) Handles btnCancelAch.Click
        pbaEnterBankAccountInfo.Clear()
    End Sub

    Private Sub pbaEnterBankAccountInfo_ShowCommercialAccountMessage(message As String, showMessage As Boolean) Handles pbaEnterBankAccountInfo.ShowCommercialAccountMessage
        psmErrorMessage.ToggleStatusMessage(message, StatusMessageType.Danger, showMessage, showMessage)
    End Sub

    <WebMethod>
    Public Shared Function GetProcessingStatus() As String

        Return "{""isProcessing"":" & BLL.SessionManager.CurrentlyProcessingTransaction.ToString.ToLower & "}"

    End Function

    <WebMethod>
    Public Shared Function SetProcessingStatus(ByVal processing As Boolean) As String
        BLL.SessionManager.CurrentlyProcessingTransaction = processing    'Convert.ToBoolean(processing)

        Return Utility.IIf(Of String)(BLL.SessionManager.CurrentlyProcessingTransaction = processing, "{""updated"":true}", "{""updated"":false}")
    End Function

    Private Sub postBackACH()
        Dim b As New B2P.ClientInterface.Manager.ClientInterface.ServiceInformation
        Dim x As New B2P.ClientInterface.Manager.ClientInterface
        Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.PostBackResult
        Dim c As B2P.ClientInterface.Manager.ClientInterfaceWS.PostbackInformation
        Dim tiListItems As B2P.Payment.PaymentBase.TransactionItems
        tiListItems = CType(Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems)

        For Each ti As B2P.Payment.PaymentBase.TransactionItems.LineItem In tiListItems
            b = B2P.ClientInterface.Manager.ClientInterface.GetServiceURL(BLL.SessionManager.ClientCode, ti.ProductName)

            If b.PostPaymentOption = B2P.Common.Enumerations.PostPaymentOptions.CreditCard Or b.PostPaymentOption = B2P.Common.Enumerations.PostPaymentOptions.Both Then

                c = B2P.ClientInterface.Manager.Utility.GetPostBackInformation()

                c.AccountNumber1 = ti.AccountNumber1
                If ti.AccountNumber2.Trim <> "" Then
                    c.AccountNumber2 = ti.AccountNumber2
                Else
                    c.AccountNumber2 = ""
                End If
                If ti.AccountNumber3.Trim <> "" Then
                    c.AccountNumber3 = ti.AccountNumber3
                Else
                    c.AccountNumber3 = ""
                End If

                c.ConfirmationNumber = BLL.SessionManager.ConfirmationNumber
                c.Amount = ti.Amount
                c.AuthorizationCode = CStr(Session("AuthorizationCode"))
                c.PaymentType = B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentTypes.eCheck
                c.ClientCode = BLL.SessionManager.ClientCode
                c.ProductName = ti.ProductName
                c.PaymentAccountNumber = BLL.SessionManager.BankAccount.BankAccountNumber
                c.Comments = BLL.SessionManager.UserComments
                c.Demographics.Address1.Value = ""
                c.PassThrough.Field1 = ""
                c.PaymentDate = Now()


                y = x.UpdateClientSystem(c)
                Session("PostBackMessage") = Nothing
                Select Case y.Result
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Success
                        Session("PostBackMessage") = "Success"
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Failed
                        Session("PostBackMessage") = y.Message
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.NotApplicable
                        Session("PostBackMessage") = Nothing
                End Select

            End If
        Next
    End Sub

    Private Sub postBackCC()
        Dim b As New B2P.ClientInterface.Manager.ClientInterface.ServiceInformation
        Dim x As New B2P.ClientInterface.Manager.ClientInterface
        Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.PostBackResult
        Dim c As B2P.ClientInterface.Manager.ClientInterfaceWS.PostbackInformation
        Dim tiListItems As B2P.Payment.PaymentBase.TransactionItems
        tiListItems = CType(Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems)

        For Each ti As B2P.Payment.PaymentBase.TransactionItems.LineItem In tiListItems
            b = B2P.ClientInterface.Manager.ClientInterface.GetServiceURL(BLL.SessionManager.ClientCode, ti.ProductName)

            If b.PostPaymentOption = B2P.Common.Enumerations.PostPaymentOptions.CreditCard Or b.PostPaymentOption = B2P.Common.Enumerations.PostPaymentOptions.Both Then

                c = B2P.ClientInterface.Manager.Utility.GetPostBackInformation()

                c.AccountNumber1 = ti.AccountNumber1
                If ti.AccountNumber2.Trim <> "" Then
                    c.AccountNumber2 = ti.AccountNumber2
                Else
                    c.AccountNumber2 = ""
                End If
                If ti.AccountNumber3.Trim <> "" Then
                    c.AccountNumber3 = ti.AccountNumber3
                Else
                    c.AccountNumber3 = ""
                End If

                c.ConfirmationNumber = BLL.SessionManager.ConfirmationNumber
                c.Amount = ti.Amount
                c.AuthorizationCode = CStr(Session("AuthorizationCode"))
                c.PaymentType = B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentTypes.CreditCard
                c.ClientCode = BLL.SessionManager.ClientCode
                c.ProductName = ti.ProductName
                c.PaymentAccountNumber = BLL.SessionManager.CreditCard.CreditCardNumber
                c.Comments = BLL.SessionManager.UserComments
                c.Demographics.Address1.Value = ""
                c.PassThrough.Field1 = ""
                c.PaymentDate = Now()


                y = x.UpdateClientSystem(c)
                Session("PostBackMessage") = Nothing
                Select Case y.Result
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Success
                        Session("PostBackMessage") = "Success"
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Failed
                        Session("PostBackMessage") = y.Message
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.NotApplicable
                        Session("PostBackMessage") = Nothing
                End Select

            End If
        Next
    End Sub
    Private Shared Function postBackEMV()
        Dim b As New B2P.ClientInterface.Manager.ClientInterface.ServiceInformation
        Dim x As New B2P.ClientInterface.Manager.ClientInterface
        Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.PostBackResult
        Dim c As B2P.ClientInterface.Manager.ClientInterfaceWS.PostbackInformation
        Dim tiListItems As B2P.Payment.PaymentBase.TransactionItems
        tiListItems = CType(HttpContext.Current.Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems)

        For Each ti As B2P.Payment.PaymentBase.TransactionItems.LineItem In tiListItems
            b = B2P.ClientInterface.Manager.ClientInterface.GetServiceURL(BLL.SessionManager.ClientCode, ti.ProductName)

            If b.PostPaymentOption = B2P.Common.Enumerations.PostPaymentOptions.CreditCard Or b.PostPaymentOption = B2P.Common.Enumerations.PostPaymentOptions.Both Then

                c = B2P.ClientInterface.Manager.Utility.GetPostBackInformation()

                c.AccountNumber1 = ti.AccountNumber1
                If ti.AccountNumber2.Trim <> "" Then
                    c.AccountNumber2 = ti.AccountNumber2
                Else
                    c.AccountNumber2 = ""
                End If
                If ti.AccountNumber3.Trim <> "" Then
                    c.AccountNumber3 = ti.AccountNumber3
                Else
                    c.AccountNumber3 = ""
                End If

                c.ConfirmationNumber = BLL.SessionManager.TransactionReceipt.ReferenceNumber.ToUpper
                c.Amount = ti.Amount
                c.AuthorizationCode = BLL.SessionManager.TransactionReceipt.TransactionApprovalNumber.ToUpper
                c.PaymentType = B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentTypes.CreditCard
                c.ClientCode = BLL.SessionManager.ClientCode
                c.ProductName = ti.ProductName
                c.PaymentAccountNumber = BLL.SessionManager.TransactionReceipt.AccountNumber.ToUpper
                c.Comments = BLL.SessionManager.UserComments
                c.Demographics.Address1.Value = ""
                c.PassThrough.Field1 = ""
                c.PaymentDate = Now()


                y = x.UpdateClientSystem(c)
                HttpContext.Current.Session("PostBackMessage") = Nothing
                Select Case y.Result
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Success
                        HttpContext.Current.Session("PostBackMessage") = "Success"
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Failed
                        HttpContext.Current.Session("PostBackMessage") = y.Message
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.NotApplicable
                        HttpContext.Current.Session("PostBackMessage") = Nothing
                End Select
            End If

        Next
        Return HttpContext.Current.Session("PostBackMessage")
    End Function


#End Region

End Class

