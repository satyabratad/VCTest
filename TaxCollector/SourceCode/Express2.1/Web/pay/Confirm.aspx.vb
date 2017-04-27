Imports System
Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web
    Public Class confirm : Inherits SiteBasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Me.CheckSession()
                ' Build the payment method
                BuildPaymentMethod()

                ' Add client side javascript attributes to the various form controls
                RegisterClientJs()

                'Populate grid
                PaymentCartGrid.PopulateGrid("PaymentCartGrid")

                ' If it's an SSO client, omit first step on breadcrumb flow; show SSO breadcrumb panel
                If BLL.SessionManager.IsSSOProduct Then
                    pnlSSOBreadcrumb.Visible = True
                    pnlNonSSOBreadcrumb.Visible = False
                Else
                    pnlSSOBreadcrumb.Visible = False
                    pnlNonSSOBreadcrumb.Visible = True
                End If
            End If

        End Sub
        Protected Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
            Response.Redirect("~/pay/")
        End Sub

        Protected Sub btnFeesConfirm_Click(sender As Object, e As EventArgs) Handles btnFeesConfirm.Click
            cptConfirmFeeInfo.AgreedToTerms = True
        End Sub
        Protected Sub btnTermsConfirm_Click(sender As Object, e As EventArgs) Handles btnTermsConfirm.Click
            cptConfirmPaymentInfo.AgreedToTerms = True
        End Sub


        ''' <summary>
        ''' Builds the payment method information panels.
        ''' </summary>
        Private Sub BuildPaymentMethod()
            Dim sb As New StringBuilder

            Dim total As Decimal '= BLL.SessionManager.TransactionInformation.CartTotal + cartFee
            Dim cf As B2P.Payment.FeeCalculation.CalculatedFee = Nothing
            Dim errMsg As String = String.Empty

            Try
                Select Case BLL.SessionManager.PaymentType
                    Case Common.Enumerations.PaymentTypes.BankAccount
                        cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, BLL.SessionManager.PaymentAmount)
                        total = BLL.SessionManager.PaymentAmount + cf.ConvenienceFee
                        sb.Append("I hereby authorize " & BLL.SessionManager.Client.ClientName & " to deduct my bank account via ACH " & total.ToString("C2") & " on " & Date.Today & " for payment to " & BLL.SessionManager.Client.ClientName & ". ")

                    Case Common.Enumerations.PaymentTypes.CreditCard
                        Dim cardType As B2P.Payment.FeeCalculation.PaymentTypes = B2P.Payment.FeeCalculation.GetCardType(BLL.SessionManager.CreditCard.InternalCreditCardNumber)
                        cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, cardType, BLL.SessionManager.PaymentAmount)
                        total = BLL.SessionManager.PaymentAmount + cf.ConvenienceFee
                        sb.Append("I hereby authorize " & BLL.SessionManager.Client.ClientName & " to deduct from my credit/debit card " & total.ToString("C2") & " on " & Date.Today & " for payment to " & BLL.SessionManager.Client.ClientName & ". ")

                End Select

                sb.Append("I understand this is a transaction performed over the WEB and I will not be able to issue a Stop Payment on this transaction." & Environment.NewLine & Environment.NewLine)
                sb.Append("Please note that, in the event we are unable to secure funds for this transaction for any reason, including but not limited to, insufficient funds in your account or insufficient or inaccurate information provided by you when you submitted your electronic payment, further collection action may be undertaken by " & BLL.SessionManager.Client.ClientName & ", including application of returned check fees to the extent permitted by law. ")
                sb.Append(Environment.NewLine & Environment.NewLine & "I understand that I may obtain a paper copy of the terms of this authorization form and of the ")
                sb.Append(BLL.SessionManager.Client.ClientName & " electronic records related to the debit entry covered by this authorization by contacting Customer Service by phone at ")
                sb.Append(BLL.SessionManager.Client.ContactPhone & ", or email " & BLL.SessionManager.Client.ContactEmail & ". To continue, click on the ""I Agree"" button. If you click on the Close button, you will be taken back to the main screen.")

                ' Load the terms and conditions disclaimer textarea
                txtTerms.Text = sb.ToString

                ' Set the fee text
                txtFees.Text = B2P.Objects.Client.GetClientMessage("FeeTerms", BLL.SessionManager.ClientCode.ToString)

                ' Hide convenience fee link if there are no fees
                Dim fees As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode, BLL.SessionManager.ProductName, Common.Enumerations.TransactionSources.Web)

                If fees.HasACHConvenienceFee Or fees.HasCCConvenienceFee Or fees.HasPDConvenienceFee Or fees.HasPNMConvenienceFee Then
                    '' pnlFees.Visible = True
                End If

                ' Display the fee conditions checkbox based on client flag
                If BLL.SessionManager.Client.ConfirmFees And cf.ConvenienceFee > 0 Then
                    pnlFeeAgreement.Visible = True
                Else
                    pnlFeeAgreement.Visible = False
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
                ' Clean up a bit
                If sb IsNot Nothing Then
                    sb = Nothing
                End If
            End Try
        End Sub


        ''' <summary>
        ''' Adds client side javascript to the various server controls.
        ''' </summary>
        Private Sub RegisterClientJs()
            btnSubmit.Attributes.Add("onClick", "return validateForm()")
            'btnCancel.Attributes.Add("onClick", "return false;")
        End Sub

        Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

            btnSubmit.Enabled = False

            If BLL.SessionManager.SSODisplayType = B2P.Objects.Client.SSODisplayTypes.ReadOnlyGrid Then
                PaybyLookup()
            Else
                Select Case BLL.SessionManager.PaymentType
                    Case Common.Enumerations.PaymentTypes.CreditCard
                        ProcessPaymentCredit()
                    Case Common.Enumerations.PaymentTypes.BankAccount
                        ProcessPaymentACH()
                End Select
            End If

        End Sub

        Private Sub PaybyLookup()
            Dim errMsg As String = String.Empty
            Dim lis As New B2P.Payment.PaymentBase.TransactionItems
            Dim sc As B2P.ShoppingCart.Cart
            Dim card As B2P.Common.Objects.CreditCard = BLL.SessionManager.CreditCard

            Try
                'Commented by RS
                'For Each ci As B2P.SSOLookup.PaymentInformation.CartItem In BLL.SessionManager.TokenInfo.CartItems
                '    'lis.Add(ci.AccountNumber1, ci.AccountNumber2, ci.AccountNumber3, ci.ProductName, ci.Amount, 0, 0)
                'Next
                MapCart(Of B2P.Payment.PaymentBase.TransactionItems)(lis)

                sc = New B2P.ShoppingCart.Cart(BLL.SessionManager.ClientCode, B2P.Common.Enumerations.TransactionSources.Web, lis)

                Select Case BLL.SessionManager.PaymentType
                    Case Common.Enumerations.PaymentTypes.CreditCard
                        If BLL.SessionManager.PaymentAmount > 0 Then
                            If BLL.SessionManager.PaymentMade = False Then

                                Dim x As New B2P.Payment.CreditCardPayment(BLL.SessionManager.ClientCode)
                                card.Owner.EMailAddress = Utility.SafeEncode(cptConfirmPaymentInfo.EmailAddress)
                                BLL.SessionManager.CreditCard = card

                                x.UserComments = ""
                                x.Office_ID = BLL.SessionManager.OfficeID
                                x.ClientCode = BLL.SessionManager.ClientCode
                                x.Security_ID = 0
                                x.AllowConfirmationEmails = True
                                x.VendorReferenceCode = BLL.SessionManager.VendorReferenceCode
                                x.PaymentSource = B2P.Common.Enumerations.TransactionSources.Web


                                Dim cardType As B2P.Payment.FeeCalculation.PaymentTypes = B2P.Payment.FeeCalculation.GetCardType(card.InternalCreditCardNumber)

                                Dim ccpr As B2P.Payment.CreditCardPayment.CreditCardPaymentResults
                                Dim ccpr2 As B2P.Payment.CreditCardPayment.CreditCardPaymentResults

                                If sc.ContainsTaxItems Then
                                    x.Items = sc.GetCartItems(cardType, B2P.ShoppingCart.Cart.CartItemsTypes.Tax)
                                    ccpr = x.PayByCreditCard(card)
                                    If ccpr.Result = B2P.Payment.CreditCardPayment.CreditCardPaymentResults.Results.Success Then
                                        BLL.SessionManager.ConfirmationNumber = Utility.SafeEncode(ccpr.ConfirmationNumber)
                                        BLL.SessionManager.PaymentDate = B2P.Payment.Utility.GetClientTime(BLL.SessionManager.ClientCode)
                                        BLL.SessionManager.PaymentMade = True
                                        'B2PSession.CreditCardReturn = ccpr
                                        Response.Redirect("/pay/PaymentComplete.aspx", False)
                                    Else
                                        BLL.SessionManager.PaymentMade = False
                                        Response.Redirect("/pay/PaymentFailure.aspx", False)
                                    End If
                                End If

                                If sc.ContainsTaxItems = False OrElse ccpr.Result = B2P.Payment.CreditCardPayment.CreditCardPaymentResults.Results.Success Then
                                    If sc.ContainsNonTaxItems Then
                                        x.Items = sc.GetCartItems(cardType, B2P.ShoppingCart.Cart.CartItemsTypes.Nontax)
                                        ccpr2 = x.PayByCreditCard(card)
                                        If ccpr2.Result = B2P.Payment.CreditCardPayment.CreditCardPaymentResults.Results.Success Then
                                            BLL.SessionManager.ConfirmationNumber = Utility.SafeEncode(ccpr.ConfirmationNumber)
                                            BLL.SessionManager.PaymentDate = B2P.Payment.Utility.GetClientTime(BLL.SessionManager.ClientCode)
                                            BLL.SessionManager.PaymentMade = True
                                            'BLL.SessionManager.CreditCardReturn = ccpr
                                            Response.Redirect("/pay/PaymentComplete.aspx", False)
                                            HttpContext.Current.ApplicationInstance.CompleteRequest()
                                        Else
                                            BLL.SessionManager.PaymentMade = False
                                            Response.Redirect("/pay/PaymentFailure.aspx", False)
                                        End If
                                    Else
                                        Response.Redirect("/pay/PaymentComplete.aspx", False)
                                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                                    End If
                                End If
                            Else
                                Response.Redirect("/Errors/SessionExpired.aspx", False)
                            End If
                        Else
                            Response.Redirect("/Errors/SessionExpired.aspx", False)
                        End If

                    Case Common.Enumerations.PaymentTypes.BankAccount
                        PaybyLookupACH(sc)
                End Select

            Catch ex As Exception
                ' Build the error message
                errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/Errors/Error.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()

            End Try
        End Sub

        Private Sub PaybyLookupACH(ByVal cart As B2P.ShoppingCart.Cart)
            Dim errMsg As String = String.Empty
            Dim x As New B2P.Payment.BankAccountPayment(BLL.SessionManager.ClientCode)
            Dim ba As B2P.Common.Objects.BankAccount = BLL.SessionManager.BankAccount

            If BLL.SessionManager.PaymentAmount > 0 Then
                If BLL.SessionManager.PaymentMade = False Then
                    Try
                        ba = BLL.SessionManager.BankAccount
                        ba.Owner.EMailAddress = Utility.SafeEncode(cptConfirmPaymentInfo.EmailAddress)
                        BLL.SessionManager.BankAccount = ba

                        Select Case ba.BankAccountType
                            Case B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Checking
                                BLL.SessionManager.OriginatorID = B2P.Payment.BankAccountPayment.GetCompanyID(BLL.SessionManager.ClientCode)
                            Case B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Savings
                                BLL.SessionManager.OriginatorID = B2P.Payment.BankAccountPayment.GetCompanyID(BLL.SessionManager.ClientCode)
                            Case Else
                                BLL.SessionManager.OriginatorID = ""
                        End Select

                        x.UserComments = ""
                        x.Office_ID = BLL.SessionManager.OfficeID
                        x.ClientCode = BLL.SessionManager.ClientCode
                        x.Security_ID = 0
                        x.AllowConfirmationEmails = True
                        x.VendorReferenceCode = BLL.SessionManager.VendorReferenceCode
                        x.PaymentSource = B2P.Common.Enumerations.TransactionSources.Web
                        x.Items = cart.GetCartItems(B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, B2P.ShoppingCart.Cart.CartItemsTypes.All)

                        Dim bapr As B2P.Payment.BankAccountPayment.BankAccountPaymentResults
                        bapr = x.PayByBankAccount(ba)

                        If bapr.Result = B2P.Payment.BankAccountPayment.BankAccountPaymentResults.Results.Success Then

                            'Added by Rs
                            SavePropertyAddress(bapr.ConfirmationNumber)

                            BLL.SessionManager.ConfirmationNumber = Utility.SafeEncode(bapr.ConfirmationNumber)
                            BLL.SessionManager.PaymentDate = B2P.Payment.Utility.GetClientTime(BLL.SessionManager.ClientCode)
                            Dim b As New B2P.ClientInterface.Manager.ClientInterface.ServiceInformation
                            b = B2P.ClientInterface.Manager.ClientInterface.GetServiceURL(BLL.SessionManager.ClientCode, BLL.SessionManager.LookupProduct)
                            BLL.SessionManager.PaymentMade = True
                            'B2PSession.BankAccountReturn = bapr
                            Response.Redirect("/pay/PaymentComplete.aspx", False)
                        Else
                            BLL.SessionManager.PaymentMade = False
                            Response.Redirect("/pay/PaymentFailure.aspx", False)
                        End If


                    Catch ex As Exception
                        ' Build the error message
                        errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                        B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                        Response.Redirect("/Errors/Error.aspx", False)
                        HttpContext.Current.ApplicationInstance.CompleteRequest()

                    End Try

                End If
            Else
                Response.Redirect("/Errors/SessionExpired.aspx", False)
            End If

        End Sub
        Private Sub ProcessPaymentACH()
            Dim errMsg As String = String.Empty
            Dim ba As B2P.Common.Objects.BankAccount = BLL.SessionManager.BankAccount
            Dim x As New B2P.Payment.BankAccountPayment(BLL.SessionManager.ClientCode)

            Try

                If BLL.SessionManager.PaymentAmount > 0 Then

                    If CBool(BLL.SessionManager.PaymentMade) = False Then

                        ba = BLL.SessionManager.BankAccount
                        ba.Owner.EMailAddress = Utility.SafeEncode(cptConfirmPaymentInfo.EmailAddress)
                        BLL.SessionManager.BankAccount = ba

                        Select Case ba.BankAccountType
                            Case B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Checking
                                BLL.SessionManager.OriginatorID = B2P.Payment.BankAccountPayment.GetCompanyID(BLL.SessionManager.ClientCode)
                            Case B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Savings
                                BLL.SessionManager.OriginatorID = B2P.Payment.BankAccountPayment.GetCompanyID(BLL.SessionManager.ClientCode)
                            Case Else
                                BLL.SessionManager.OriginatorID = ""
                        End Select

                        x.PaymentSource = B2P.Common.Enumerations.TransactionSources.Web

                        'Commented By Rs -- Save Contact Details
                        'x.UserData.Address1 = BLL.SessionManager.ServiceAddress
                        SetUserData(Of B2P.Payment.BankAccountPayment)(x)

                        If BLL.SessionManager.IsSSOProduct Then
                            x.VendorReferenceCode = BLL.SessionManager.TokenInfo.VendorReferenceCode
                        End If
                        'Commented by RS
                        'x.Items.Add(BLL.SessionManager.AccountNumber1, BLL.SessionManager.AccountNumber2, BLL.SessionManager.AccountNumber3, BLL.SessionManager.CurrentCategory.Name.ToString, CDec(BLL.SessionManager.PaymentAmount), BLL.SessionManager.ConvenienceFee, BLL.SessionManager.TransactionFee)
                        MapCart(Of B2P.Payment.BankAccountPayment)(x)

                        Dim bapr As B2P.Payment.BankAccountPayment.BankAccountPaymentResults
                        bapr = x.PayByBankAccount(ba)


                        If bapr.Result = B2P.Payment.BankAccountPayment.BankAccountPaymentResults.Results.Success Then
                            'Added by Rs
                            SavePropertyAddress(bapr.ConfirmationNumber)

                            BLL.SessionManager.ConfirmationNumber = Utility.SafeEncode(bapr.ConfirmationNumber)
                            BLL.SessionManager.PaymentDate = B2P.Payment.Utility.GetClientTime(BLL.SessionManager.ClientCode)
                            Dim b As New B2P.ClientInterface.Manager.ClientInterface.ServiceInformation
                            b = B2P.ClientInterface.Manager.ClientInterface.GetServiceURL(BLL.SessionManager.ClientCode, BLL.SessionManager.LookupProduct)

                            If b.PostPaymentOption = B2P.ClientInterface.Manager.ClientInterface.ServiceInformation.PostPaymentOptions.ACH Or b.PostPaymentOption = B2P.ClientInterface.Manager.ClientInterface.ServiceInformation.PostPaymentOptions.Both Then

                                Dim a As New B2P.ClientInterface.Manager.ClientInterface
                                Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.PostBackResult
                                Dim c As B2P.ClientInterface.Manager.ClientInterfaceWS.PostbackInformation

                                c = B2P.ClientInterface.Manager.Utility.GetPostBackInformation()

                                c.AccountNumber1 = BLL.SessionManager.AccountNumber1
                                c.AccountNumber2 = BLL.SessionManager.AccountNumber2
                                c.AccountNumber3 = BLL.SessionManager.AccountNumber3
                                c.ConfirmationNumber = bapr.ConfirmationNumber
                                c.Amount = BLL.SessionManager.PaymentAmount
                                c.AuthorizationCode = ""
                                c.PaymentType = B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentTypes.eCheck
                                c.ClientCode = BLL.SessionManager.ClientCode
                                c.ProductName = BLL.SessionManager.LookupProduct
                                c.PaymentAccountNumber = Right(ba.BankAccountNumber, 4)
                                c.PassThrough.Field1 = ""
                                c.PaymentDate = Now()


                                y = a.UpdateClientSystem(c)

                                Select Case y.Result
                                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Success
                                        BLL.SessionManager.PostBackMessage = "Success"
                                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Failed
                                        BLL.SessionManager.PostBackMessage = y.Message
                                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.NotApplicable
                                        BLL.SessionManager.PostBackMessage = Nothing
                                End Select
                                a = Nothing
                                y = Nothing
                                c = Nothing
                            Else
                                BLL.SessionManager.PostBackMessage = Nothing
                            End If

                            BLL.SessionManager.PaymentMade = True
                            'B2PSession.BankAccountReturn = bapr
                            Response.Redirect("/pay/PaymentComplete.aspx", False)
                        Else
                            BLL.SessionManager.PaymentMade = False
                            Response.Redirect("/pay/PaymentFailure.aspx", False)
                        End If
                    Else
                        Response.Redirect("/Errors/SessionExpired.aspx", False)
                    End If
                Else
                    Response.Redirect("/Errors/SessionExpired.aspx", False)
                End If

            Catch ex As Exception
                ' Build the error message
                errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/Errors/Error.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()

            End Try
        End Sub
        Private Sub ProcessPaymentCredit()
            Me.CheckSession()
            Dim errMsg As String = String.Empty
            Dim card As B2P.Common.Objects.CreditCard = BLL.SessionManager.CreditCard
            Dim x As New B2P.Payment.CreditCardPayment(BLL.SessionManager.ClientCode)

            Try

                If BLL.SessionManager.PaymentAmount > 0 Then
                    If CBool(BLL.SessionManager.PaymentMade) = False Then
                        card = BLL.SessionManager.CreditCard
                        card.Owner.EMailAddress = Utility.SafeEncode(cptConfirmPaymentInfo.EmailAddress)
                        BLL.SessionManager.CreditCard = card
                        x.PaymentSource = B2P.Common.Enumerations.TransactionSources.Web
                        'Commented by RS
                        'x.Items.Add(BLL.SessionManager.AccountNumber1, BLL.SessionManager.AccountNumber2, BLL.SessionManager.AccountNumber3, BLL.SessionManager.CurrentCategory.Name.ToString, CDec(BLL.SessionManager.PaymentAmount), BLL.SessionManager.ConvenienceFee, BLL.SessionManager.TransactionFee)
                        MapCart(Of B2P.Payment.CreditCardPayment)(x)

                        'Commented By Rs -- Save Contact Details
                        'x.UserData.Address1 = BLL.SessionManager.ServiceAddress
                        SetUserData(Of B2P.Payment.CreditCardPayment)(x)

                        If BLL.SessionManager.IsSSOProduct Then
                            x.VendorReferenceCode = BLL.SessionManager.TokenInfo.VendorReferenceCode
                        End If


                        Dim ccpr As B2P.Payment.CreditCardPayment.CreditCardPaymentResults

                        ccpr = x.PayByCreditCard(card)


                        If ccpr.Result = B2P.Payment.CreditCardPayment.CreditCardPaymentResults.Results.Success Then

                            'Added by Rs
                            SavePropertyAddress(ccpr.ConfirmationNumber)

                            BLL.SessionManager.ConfirmationNumber = Utility.SafeEncode(ccpr.ConfirmationNumber)
                            BLL.SessionManager.PaymentDate = B2P.Payment.Utility.GetClientTime(BLL.SessionManager.ClientCode)

                            Dim b As New B2P.ClientInterface.Manager.ClientInterface.ServiceInformation
                            b = B2P.ClientInterface.Manager.ClientInterface.GetServiceURL(BLL.SessionManager.ClientCode, BLL.SessionManager.LookupProduct)

                            If b.PostPaymentOption = B2P.ClientInterface.Manager.ClientInterface.ServiceInformation.PostPaymentOptions.CreditCard Or b.PostPaymentOption = B2P.ClientInterface.Manager.ClientInterface.ServiceInformation.PostPaymentOptions.Both Then

                                Dim a As New B2P.ClientInterface.Manager.ClientInterface
                                Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.PostBackResult
                                Dim c As B2P.ClientInterface.Manager.ClientInterfaceWS.PostbackInformation

                                c = B2P.ClientInterface.Manager.Utility.GetPostBackInformation()

                                c.AccountNumber1 = BLL.SessionManager.AccountNumber1
                                c.AccountNumber2 = BLL.SessionManager.AccountNumber2
                                c.AccountNumber3 = BLL.SessionManager.AccountNumber3
                                c.ConfirmationNumber = ccpr.ConfirmationNumber
                                c.Amount = BLL.SessionManager.PaymentAmount
                                c.AuthorizationCode = ccpr.AuthorizationCode
                                c.PaymentType = B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentTypes.CreditCard
                                c.ClientCode = BLL.SessionManager.ClientCode
                                c.ProductName = BLL.SessionManager.LookupProduct
                                c.PaymentAccountNumber = Right(card.CreditCardNumber, 4)

                                c.PassThrough.Field1 = ""
                                c.PaymentDate = Now()

                                y = a.UpdateClientSystem(c)

                                Select Case y.Result
                                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Success
                                        BLL.SessionManager.PostBackMessage = "Success"
                                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Failed
                                        BLL.SessionManager.PostBackMessage = y.Message
                                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.NotApplicable
                                        BLL.SessionManager.PostBackMessage = Nothing
                                End Select
                                a = Nothing
                                y = Nothing
                                c = Nothing
                            Else
                                BLL.SessionManager.PostBackMessage = Nothing

                            End If
                            BLL.SessionManager.PaymentMade = True
                            'B2PSession.CreditCardReturn = ccpr
                            Response.Redirect("/pay/PaymentComplete.aspx", False)
                        Else
                            BLL.SessionManager.PaymentMade = False
                            Response.Redirect("/pay/PaymentFailure.aspx", False)

                        End If
                    Else
                        Response.Redirect("/Errors/SessionExpired.aspx", False)
                    End If
                Else
                    Response.Redirect("/Errors/SessionExpired.aspx", False)
                End If
            Catch ex As Exception
                ' Build the error message
                errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                               ex.Source, ex.TargetSite.DeclaringType.Name,
                                               ex.TargetSite.Name, ex.Message)

                B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/Errors/Error.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()

            End Try
        End Sub

        Private Sub MapCart(Of T)(ByRef Param As T)
            Dim ConvenienceFee As Double = IIf(BLL.SessionManager.IsConvenienceFeesApplicable, BLL.SessionManager.ConvenienceFee, 0)
            Dim TransactionFee As Double = BLL.SessionManager.TransactionFee
            Dim instance = Nothing
            If GetType(T) Is GetType(B2P.Payment.CreditCardPayment) Then
                instance = CType(CType(Param, Object), B2P.Payment.CreditCardPayment)
            End If
            If GetType(T) Is GetType(B2P.Payment.BankAccountPayment) Then
                instance = CType(CType(Param, Object), B2P.Payment.BankAccountPayment)
            End If
            If GetType(T) Is GetType(B2P.Payment.PaymentBase.TransactionItems) Then
                instance = CType(CType(Param, Object), B2P.Payment.PaymentBase.TransactionItems)
                ConvenienceFee = 0
                TransactionFee = 0
            End If
            For Each cart As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
                Dim accfldVal1 As String = BLL.SessionManager.ManageCart.GetAccountFieldValue(cart, 0)
                Dim accfldVal2 As String = BLL.SessionManager.ManageCart.GetAccountFieldValue(cart, 0)
                Dim accfldVal3 As String = BLL.SessionManager.ManageCart.GetAccountFieldValue(cart, 0)
                Dim index As Integer = 0


                instance.Items.Add(accfldVal1, accfldVal2, accfldVal3, BLL.SessionManager.CurrentCategory.Name.ToString, CDec(cart.Amount), ConvenienceFee, TransactionFee)
                    If BLL.SessionManager.ManageCart.Cart.Count > 0 Then
                        ConvenienceFee = 0
                        TransactionFee = 0
                    End If
                Next
        End Sub
        Private Sub SetUserData(Of T)(ByRef Param As T)
            Dim instance = Nothing
            If GetType(T) Is GetType(B2P.Payment.CreditCardPayment) Then
                instance = CType(CType(Param, Object), B2P.Payment.CreditCardPayment)
            End If
            If GetType(T) Is GetType(B2P.Payment.BankAccountPayment) Then
                instance = CType(CType(Param, Object), B2P.Payment.BankAccountPayment)
            End If
            If GetType(T) Is GetType(B2P.Payment.PaymentBase.TransactionItems) Then
                instance = CType(CType(Param, Object), B2P.Payment.PaymentBase.TransactionItems)
            End If
            If Not BLL.SessionManager.ContactInfo Is Nothing Then
                With BLL.SessionManager.ContactInfo
                    instance.UserData.UserField1 = .UserField1
                    instance.UserData.Address1 = .Address1
                    instance.UserData.Address2 = .Address2
                    instance.UserData.UserField2 = .UserField2
                    If Not String.IsNullOrEmpty(.State.Trim()) Then
                        instance.UserData.State = .State
                    End If
                    instance.UserData.City = .City
                    instance.UserData.Zip = .Zip
                    instance.UserData.HomePhone = .HomePhone
                End With
            Else
                instance.UserData.Address1 = BLL.SessionManager.ServiceAddress
            End If
        End Sub
        Private Sub SavePropertyAddress(BatchId As String)
            For Each cart As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
                Dim z As New B2P.Objects.Product(BLL.SessionManager.ClientCode, cart.Item, B2P.Common.Enumerations.TransactionSources.Web)
                cart.CollectPropertyAddress = z.CollectAddress
            Next
            BLL.SessionManager.ManageCart.SavePropertyAddress(BLL.SessionManager.ClientCode, BatchId)
        End Sub
    End Class
End Namespace