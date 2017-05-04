Imports System
Imports B2P.Common.ExtensionMethods
Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web
    Public Class payment : Inherits SiteBasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            lnkCSS.Href = BLL.SessionManager.ClientCSS
            BLL.SessionManager.PaymentMade = False
            BLL.SessionManager.CreditCard = Nothing
            BLL.SessionManager.BankAccount = Nothing

            ' Add client side javascript attributes to the various form controls
            RegisterClientJs()


            ' Gets the last active Bootstrap tab before the postback
            hdfTabName.Value = Request.Form(hdfTabName.UniqueID)


            If Not IsPostBack Then

                ' Build the account information panel
                BuildForm()

                ' Build allowed payment types
                BuildPaymentTypes()
                'BuildAmountInfo()

                ' Set the originator ID
                BLL.SessionManager.OriginatorID = B2P.Payment.BankAccountPayment.GetCompanyID(BLL.SessionManager.ClientCode)

                ' If it's an SSO client, omit first step on breadcrumb flow; show SSO breadcrumb panel
                If BLL.SessionManager.IsSSOProduct Then
                    pnlSSOBreadcrumb.Visible = True
                    pnlNonSSOBreadcrumb.Visible = False
                Else
                    pnlSSOBreadcrumb.Visible = False
                    pnlNonSSOBreadcrumb.Visible = True
                End If
                txtAmount.Text = CalculateSubTotal()

                ' set addmore item button in case of SSO client
                If BLL.SessionManager.ClientType = Cart.EClientType.SSO Then
                    btnAddMoreItemCredit.Visible = False
                    btnAddMoreItemAch.Visible = False
                End If
            End If
        End Sub
        ''' <summary>
        ''' Calculate the total amount of item / service in the cart
        ''' </summary>
        ''' <returns></returns>
        Protected Function CalculateSubTotal() As String
            Dim total As Double = 0
            If Not BLL.SessionManager.ManageCart.Cart Is Nothing Then
                For Each item As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
                    total += item.Amount
                Next
            End If
            Return total.ToString()
        End Function
        Protected Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
            Response.Redirect("~/pay/")
        End Sub

        Private Sub btnFees_Click(sender As Object, e As EventArgs) Handles btnFees.Click
            ' Show the convenience fees modal dialog
            Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#feeInfoModal').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
        End Sub

        Private Sub btnSubmitAch_Click(sender As Object, e As EventArgs) Handles btnSubmitAch.Click
            Dim account As B2P.Common.Objects.BankAccount = Nothing
            Dim failureMsg As String = String.Empty
            Dim accountValid As Boolean
            Dim errMsg As String = String.Empty
            Dim names As String()
            Dim firstName As String = String.Empty
            Dim lastName As String = String.Empty

            Me.CheckSession()

            Try
                '::::::::::::::::::::::::::::::::::::::::::::
                ' Look at HK Responsive site for ACH parsing
                '::::::::::::::::::::::::::::::::::::::::::::
                If ValidateForm("ACH") Then
                    account = New B2P.Common.Objects.BankAccount
                    Dim paymentAmount As Decimal = Convert.ToDecimal(txtAmount.Text())
                    If validatePaymentAmount(paymentAmount, BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MinimumAmount, BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount) = False Then
                        psmErrorMessage.ToggleStatusMessage(GetGlobalResourceObject("WebResources", "ErrMsgAmount").ToString(), StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                        Exit Sub
                    End If
                    ' Set the bank account properties
                    names = pbaEnterBankAccountInfo.NameonBankAccount.Split(" ")
                    Select Case names.Length

                        Case 1
                            ' Only first name provided (or blank)
                            account.Owner.FirstName = "NA"
                            account.Owner.LastName = Truncate(Regex.Replace(names(0), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase), 20)
                        Case 2
                            ' First and last names provided
                            account.Owner.FirstName = Truncate(Regex.Replace(names(0), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase), 20)
                            account.Owner.LastName = Truncate(Regex.Replace(names(1), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase), 30)
                        Case Else
                            ' First item is the first name. last item is the last name. Everything else are middle names
                            account.Owner.FirstName = Truncate(Regex.Replace(names(0), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase), 20)
                            Dim rebuildLastName As String = String.Empty
                            For i = 1 To names.Length - 1
                                rebuildLastName &= Regex.Replace(names(i), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase) & " "
                            Next
                            account.Owner.LastName = Truncate(rebuildLastName.Trim, 30)
                    End Select


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

                    ' Set the payment amount
                    If rdCurrentCharges.Checked Then
                        ' Equal to lookup amount
                        BLL.SessionManager.PaymentAmount = BLL.SessionManager.LookupAmount
                    Else
                        ' User entered value
                        BLL.SessionManager.PaymentAmount = Math.Truncate(Utility.SafeEncode(txtAmount.Text) * 100) / 100
                    End If

                    ' Do we need to validate the bank account
                    If B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode).UseATMVerify Then
                        If Not B2P.Common.Objects.BankAccount.ValidateBankAccount(BLL.SessionManager.ClientCode, pbaEnterBankAccountInfo.BankAccountNumber.Trim, account.BankRoutingNumber, BLL.SessionManager.PaymentAmount, account.BankAccountType) Then
                            pbaEnterBankAccountInfo.BankAccountNumber = String.Empty

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

                        'Persist bank account name and type
                        BLL.SessionManager.BankAccType = pbaEnterBankAccountInfo.BankAccountType
                        BLL.SessionManager.BankAccName = IIf(account.Owner.FirstName = "NA", "", account.Owner.FirstName) + " " + IIf(account.Owner.LastName = "NA", "", account.Owner.LastName)

                        ' Send them to the confirmation page
                        Response.Redirect("/pay/Confirm.aspx", False)
                    End If
                End If

            Catch ex As Exception
                ' Build the error message
                errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/Errors/Error.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()

            Finally
                'Clean up a bit
                If account IsNot Nothing Then
                    account = Nothing
                End If
            End Try
        End Sub

        ''' <summary>
        ''' This function will validate the payment amount with its maximum and minimum range
        ''' </summary>
        ''' <param name="amount"></param>
        ''' <param name="pattern"></param>
        ''' <returns></returns>
        Protected Function validatePaymentAmount(amount As Decimal, min As Decimal, max As Decimal) As Boolean
            Dim isValid As Boolean = False

            If amount >= min And amount <= max Then
                isValid = True
            Else
                isValid = False
            End If
            Return isValid
        End Function

        Private Sub btnSubmitCredit_Click(sender As Object, e As EventArgs) Handles btnSubmitCredit.Click
            Dim card As B2P.Common.Objects.CreditCard = Nothing
            Dim dateParts As String()
            Dim errMsg As String = String.Empty
            Dim names As String()
            Dim firstName As String = String.Empty
            Dim lastName As String = String.Empty
            Me.CheckSession()

            Try
                ':::::::::::::::::::::::::::::::::::::::::::
                ' Look at HK Responsive site for CC parsing
                ':::::::::::::::::::::::::::::::::::::::::::
                If ValidateForm("CC") Then
                    card = New B2P.Common.Objects.CreditCard
                    names = pccEnterCreditCardInfo.NameonCard.Split(" ")
                    Select Case names.Length

                        Case 1
                            ' Only first name provided (or blank)
                            card.Owner.FirstName = "NA"
                            card.Owner.LastName = Truncate(Regex.Replace(names(0), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase), 20)
                        Case 2
                            ' First and last names provided
                            card.Owner.FirstName = Truncate(Regex.Replace(names(0), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase), 20)
                            card.Owner.LastName = Truncate(Regex.Replace(names(1), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase), 30)
                        Case Else
                            ' First item is the first name. last item is the last name. Everything else are middle names
                            card.Owner.FirstName = Truncate(Regex.Replace(names(0), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase), 20)
                            Dim rebuildLastName As String = String.Empty
                            For i = 1 To names.Length - 1
                                rebuildLastName &= Regex.Replace(names(i), "[^a-z0-9 ]+", "", RegexOptions.IgnoreCase) & " "
                            Next
                            card.Owner.LastName = Truncate(rebuildLastName.Trim, 30)
                    End Select
                    Dim paymentAmount As Decimal = Convert.ToDecimal(txtAmount.Text())
                    If validatePaymentAmount(paymentAmount, BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MinimumAmount, BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount) = False Then
                        psmErrorMessage.ToggleStatusMessage(GetGlobalResourceObject("WebResources", "ErrMsgAmount").ToString(), StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                        Exit Sub
                    End If
                    ':::::::::::::::::::::::::::::::::::::::::::::::::::::::
                    ' Removed CC address form fields per Ken Ponder
                    ' Some address info set to 'NA' so Vantiv will process.
                    ':::::::::::::::::::::::::::::::::::::::::::::::::::::::
                    card.Owner.Address1 = "NA"
                        card.Owner.Address2 = "NA"
                        card.Owner.City = "NA"
                        ':::::::::::::::::::::::::::::::::::::::::::::::::
                        '
                        If Not String.IsNullOrEmpty(pccEnterCreditCardInfo.CreditCardCountry) Then
                            card.Owner.CountryCode = pccEnterCreditCardInfo.CreditCardCountry
                        Else
                            card.Owner.CountryCode = "US"
                        End If

                        ' Set international info to FL and 11111 per Ken Ponder
                        ' May want to visit this later and validate

                        ''Seting the default state as per coutry setting 
                        If card.Owner.CountryCode = "US" Or card.Owner.CountryCode = "OT" Then
                            card.Owner.State = "FL"
                        ElseIf card.Owner.CountryCode = "CA" Then
                            card.Owner.State = "ON"
                        End If

                        If Not String.IsNullOrEmpty(pccEnterCreditCardInfo.CreditCardBillingZip) Then
                            card.Owner.ZipCode = pccEnterCreditCardInfo.CreditCardBillingZip
                        Else
                            card.Owner.ZipCode = ""
                        End If



                        card.Owner.EMailAddress = String.Empty
                        card.Owner.PhoneNumber = String.Empty

                        card.CreditCardNumber = pccEnterCreditCardInfo.CreditCardNumber.Trim.Replace(" ", "")

                        dateParts = pccEnterCreditCardInfo.ExpirationDate.Trim.Replace(" ", "").Split("/"c)
                        card.ExpirationMonth = dateParts(0).Trim
                        card.ExpirationYear = dateParts(1).Trim

                        card.SecurityCode = pccEnterCreditCardInfo.CVV.Trim

                        BLL.SessionManager.CreditCard = card
                        BLL.SessionManager.PaymentType = B2P.Common.Enumerations.PaymentTypes.CreditCard

                        ' Set the payment amount
                        If rdCurrentCharges.Checked Then
                            ' Equal to lookup amount
                            BLL.SessionManager.PaymentAmount = BLL.SessionManager.LookupAmount
                        Else
                            ' User entered value
                            BLL.SessionManager.PaymentAmount = Math.Truncate(Utility.SafeEncode(txtAmount.Text) * 100) / 100
                        End If

                        'Persist card owner name and exp date
                        BLL.SessionManager.CreditCardExpDate = card.ExpirationMonth + " / " + card.ExpirationYear.Substring(card.ExpirationYear.Length - 2, 2)
                        BLL.SessionManager.CreditCardOwnerName = IIf(card.Owner.FirstName = "NA", "", card.Owner.FirstName) + " " + IIf(card.Owner.LastName = "NA", "", card.Owner.LastName)

                        ' Send them to the confirmation page
                        Response.Redirect("/pay/Confirm.aspx", False)
                    End If

            Catch ex As Exception
                ' Build the error message
                errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/Errors/Error.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()

            Finally
                'Clean up a bit
                If card IsNot Nothing Then
                    card = Nothing
                End If
            End Try
        End Sub


        'Private Sub BuildAmountInfo()
        '    Dim total As Decimal
        '    Dim amt As Decimal
        '    Dim cf As B2P.Payment.FeeCalculation.CalculatedFee

        '    If txtAmount.Text <> "" Then

        '        amt = CDec(txtAmount.Text)

        '        If rdCurrentCharges.Checked Then
        '            Select Case hdfTabName.Value
        '                Case "tabCredit"
        '                    cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, B2P.Payment.FeeCalculation.PaymentTypes.CreditCard, BLL.SessionManager.LookupAmount)
        '                Case "tabACH"
        '                    cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, BLL.SessionManager.LookupAmount)
        '                Case Else
        '                    litConvFee.Text = "TBD"
        '                    litTotalAmount.Text = "TBD"
        '                    Exit Sub
        '            End Select
        '            total = BLL.SessionManager.LookupAmount + cf.ConvenienceFee
        '        Else
        '            Select Case hdfTabName.Value
        '                Case "tabCredit"
        '                    cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, B2P.Payment.FeeCalculation.PaymentTypes.CreditCard, amt)
        '                Case "tabACH"
        '                    cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, amt)
        '                Case Else
        '                    litConvFee.Text = "TBD"
        '                    litTotalAmount.Text = "TBD"
        '                    Exit Sub
        '            End Select
        '            total = txtAmount.Text + cf.ConvenienceFee
        '        End If

        '        litConvFee.Text = cf.ConvenienceFee.ToString("C2")
        '        litTotalAmount.Text = total.ToString("C2")

        '    End If

        'End Sub
        ''' <summary>
        ''' Builds the right hand side information panel.
        ''' </summary>
        Private Sub BuildForm()
            Dim CurrentCategory As B2P.Objects.Product
            CurrentCategory = BLL.SessionManager.CurrentCategory

            If BLL.SessionManager.SSODisplayType <> B2P.Objects.Client.SSODisplayTypes.ReadOnlyGrid Then

                With CurrentCategory.WebOptions

                    If .AccountIDField1.Enabled = True Then
                        litAccountNumber1.Text = Utility.SafeEncode(.AccountIDField1.Label)
                        litAccountNumber1Data.Text = BLL.SessionManager.AccountNumber1
                    Else
                        pnlAccount1.Visible = False
                        pnlAccount1.Style.Add("display", "none")
                    End If

                    If .AccountIDField2.Enabled = True Then
                        litAccountNumber2.Text = Utility.SafeEncode(.AccountIDField2.Label)
                        litAccountNumber2Data.Text = BLL.SessionManager.AccountNumber2
                    Else
                        pnlAccount2.Visible = False
                        pnlAccount2.Style.Add("display", "none")
                    End If

                    If .AccountIDField3.Enabled = True Then
                        litAccountNumber3.Text = Utility.SafeEncode(.AccountIDField3.Label)
                        litAccountNumber3Data.Text = BLL.SessionManager.AccountNumber3
                    Else
                        pnlAccount3.Visible = False
                        pnlAccount3.Style.Add("display", "none")
                    End If

                End With

                ' Show the address panel
                If BLL.SessionManager.ServiceAddress IsNot Nothing AndAlso BLL.SessionManager.ServiceAddress.Trim <> "" Then
                    pnlAddress.Visible = True
                    litAddress.Text = BLL.SessionManager.ServiceAddress
                End If
            Else
                ' Hide the panels so they don't take up white space
                pnlAccount1.Visible = False
                pnlAccount2.Visible = False
                pnlAccount3.Visible = False
                pnlAccount1.Style.Add("display", "none")
                pnlAccount2.Style.Add("display", "none")
                pnlAccount3.Style.Add("display", "none")

            End If

            If BLL.SessionManager.LookupAmount = Nothing Then
                ' This means the session is not using a lookup; then we display the amount textbox
                pnlCurrentCharges.Visible = False
                rdCurrentCharges.Checked = False
                rdAmount.Text = "AMOUNT"
                rdAmount.Checked = True
                rdAmount.CssClass = "hidden"
                txtAmount.Enabled = True
                pnlDueDate.Visible = False
                pnlAmountDue.Visible = False
            Else
                ' Display the lookup information
                litAmountDue.Text = String.Format("{0:C}", BLL.SessionManager.LookupAmount)
                rdCurrentCharges.Text = "AMOUNT DUE : " & String.Format("{0:C}", BLL.SessionManager.LookupAmount)
                If BLL.SessionManager.LookupAmountEditable Then
                    rdAmount.Text = "OTHER AMOUNT"
                Else
                    pnlOtherAmount.Visible = False
                    ' Set the payment amount for the session equal to the lookup value
                    BLL.SessionManager.PaymentAmount = BLL.SessionManager.LookupAmount
                End If
                If BLL.SessionManager.SSODisplayType <> B2P.Objects.Client.SSODisplayTypes.ReadOnlyGrid Then
                    If BLL.SessionManager.LookupData.DueDate <> "" Then
                        Dim dt As DateTime = Convert.ToDateTime(BLL.SessionManager.LookupData.DueDate)
                        litDueDate.Text = String.Format("{0:d}", dt)
                    Else
                        pnlDueDate.Visible = False
                    End If
                Else
                    pnlDueDate.Visible = False
                End If
            End If

            ' Hide convenience fee link if there are no fees
            Dim fees As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode, BLL.SessionManager.ProductName, Common.Enumerations.TransactionSources.Web)

            If fees.HasACHConvenienceFee Or fees.HasCCConvenienceFee Or fees.HasPDConvenienceFee Or fees.HasPNMConvenienceFee Then
                btnFees.Visible = True
                pnlFee.Visible = True
            End If

        End Sub

        ''' <summary>
        ''' Builds the allowable payment methods.
        ''' </summary>
        Private Sub BuildPaymentTypes()
            'Commented by RS
            'If BLL.SessionManager.CurrentCategory.PaymentInformation.ACHAccepted = True AndAlso BLL.SessionManager.BlockedACH = False Then
            If BLL.SessionManager.ManageCart.IsPaymentForBankVisible(BLL.SessionManager.ClientType = Cart.EClientType.NonLookup) Then
                If BLL.SessionManager.IsSSOProduct Then
                    If BLL.SessionManager.TokenInfo.AllowECheck Then
                        tabAch.Visible = True
                        pnlTabAch.Visible = True
                        litACHFee.Text = BLL.SessionManager.AchFeeDescription
                        hdfTabName.Value = "pnlTabAch"
                    Else
                        tabAch.Visible = False
                        pnlTabAch.Visible = False
                        pnlACHFee.Visible = False
                    End If
                Else
                    tabAch.Visible = True
                    pnlTabAch.Visible = True
                    litACHFee.Text = BLL.SessionManager.AchFeeDescription
                    hdfTabName.Value = "pnlTabAch"
                End If
            Else
                tabAch.Visible = False
                pnlACHFee.Visible = False
            End If

            'Commented by RS
            'If BLL.SessionManager.CurrentCategory.PaymentInformation.CreditCardAccepted = True AndAlso BLL.SessionManager.BlockedCC = False Then
            If BLL.SessionManager.ManageCart.IsPaymentForCreditCardVisible(BLL.SessionManager.ClientType = Cart.EClientType.NonLookup) Then
                If BLL.SessionManager.IsSSOProduct Then
                    If BLL.SessionManager.TokenInfo.AllowCreditCard Then
                        tabCredit.Visible = True
                        pnlTabCredit.Visible = True
                        litCCFee.Text = BLL.SessionManager.CreditFeeDescription
                        hdfTabName.Value = "pnlTabCredit"
                    Else
                        tabCredit.Visible = False
                        pnlTabCredit.Visible = False
                        pnlCCFee.Visible = False
                    End If
                Else
                    tabCredit.Visible = True
                    pnlTabCredit.Visible = True
                    litCCFee.Text = BLL.SessionManager.CreditFeeDescription
                    hdfTabName.Value = "pnlTabCredit"
                End If
            Else
                tabCredit.Visible = False
                pnlCCFee.Visible = False
            End If

            ' Hide the cancel ACH and credit buttons for SSO clients
            If BLL.SessionManager.IsSSOProduct Then
                btnCancelCredit.Visible = False
                btnCancelAch.Visible = False
            End If

        End Sub
        Private Sub pbaEnterBankAccountInfo_ShowCommercialAccountMessage(message As String, showMessage As Boolean) Handles pbaEnterBankAccountInfo.ShowCommercialAccountMessage
            psmErrorMessage.ToggleStatusMessage(message, StatusMessageType.Danger, showMessage, showMessage)
        End Sub
        Private Sub rdAmount_CheckedChanged(sender As Object, e As EventArgs) Handles rdAmount.CheckedChanged
            If rdAmount.Checked Then
                txtAmount.Enabled = True
            End If
        End Sub

        Private Sub rdCurrentCharges_CheckedChanged(sender As Object, e As EventArgs) Handles rdCurrentCharges.CheckedChanged
            If rdCurrentCharges.Checked Then
                txtAmount.Enabled = False
            End If
        End Sub

        ''' <summary>
        ''' Adds client side javascript to the various server controls.
        ''' </summary>
        Private Sub RegisterClientJs()
            txtAmount.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.Numeric)")
            txtAmount.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.Numeric)")
            btnSubmitCredit.Attributes.Add("onClick", "return validateForm('CC')")
            btnSubmitAch.Attributes.Add("onClick", "return validateForm('ACH')")
        End Sub

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
                    If Not Utility.IsValidField(pbaEnterBankAccountInfo.NameonBankAccount.Trim, FieldValidationType.Name) Then
                        errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgFirstName").ToString() & "<br />"
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

                Case "CC"
                    If Not Utility.IsValidField(pccEnterCreditCardInfo.NameonCard.Trim, FieldValidationType.Name) Then
                        errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgFirstName").ToString() & "<br />"
                        errFound = True
                    End If

                    If Not Utility.IsValidField(pccEnterCreditCardInfo.CreditCardNumber.Trim, FieldValidationType.CreditCard) Then
                        errMsg &= "- " & GetGlobalResourceObject("WebResources", "ErrMsgCreditCardNumber").ToString() & "<br />"
                        errFound = True
                    End If

                    Dim cardMonth As String = pccEnterCreditCardInfo.ExpirationDate.Trim.Split("/")(0)
                    Dim cardYear As String = "20" & pccEnterCreditCardInfo.ExpirationDate.Trim.Split("/")(1)
                    Dim newDate As String = cardMonth & "/" & cardYear

                    If Not Utility.IsValidField(newDate, FieldValidationType.ExpirationDate) Then
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

            End If

            Return Utility.IIf(Of Boolean)(errFound, False, True)
        End Function
        ''' <summary>
        ''' Redirect to default page for add new item
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Protected Sub btnAddMoreItemCredit_Click(sender As Object, e As EventArgs) Handles btnAddMoreItemCredit.Click

            BLL.SessionManager.ManageCart.ShowCart = False
            BLL.SessionManager.ManageCart.EditItemIndex = -1

            Response.Redirect("~/pay/")
        End Sub
        ''' <summary>
        ''' Redirect to default page for add new item 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Protected Sub btnAddMoreItemAch_Click(sender As Object, e As EventArgs) Handles btnAddMoreItemAch.Click
            'Redirect to default page for add new item
            BLL.SessionManager.ManageCart.ShowCart = False
            BLL.SessionManager.ManageCart.EditItemIndex = -1

            Response.Redirect("~/pay/")
        End Sub
    End Class
End Namespace
