Imports System
Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web
    Public Class PaymentComplete : Inherits SiteBasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                ' Add client side javascript attributes to the various form controls
                RegisterClientJs()

                'Populate grid
                PaymentCartGrid.PopulateGrid("PaymentCartGrid")

                ' Build the account information
                BuildForm()

                ' Build the payment method
                BuildPaymentMethod()

                ' Load countries for credit card supplemental info
                LoadCountries()

                ' Check the postback message
                If Not BLL.SessionManager.PostBackMessage Is Nothing AndAlso BLL.SessionManager.PostBackMessage <> "" Then
                    If BLL.SessionManager.PostBackMessage = "Success" Then
                        PostBackStatusMessage.ToggleStatusMessage(String.Format("The account update to {0} was successful.", BLL.SessionManager.Client.ClientName), StatusMessageType.Success, StatusMessageSize.Normal, True, True)
                    Else
                        PostBackStatusMessage.ToggleStatusMessage(String.Format(String.Format("{0}", BLL.SessionManager.PostBackMessage), BLL.SessionManager.Client.ClientName), StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                    End If
                End If

                ' If it's an SSO client, omit first step on breadcrumb flow; show SSO breadcrumb panel
                If BLL.SessionManager.IsSSOProduct Then
                    pnlSSOBreadcrumb.Visible = True
                    pnlNonSSOBreadcrumb.Visible = False
                Else
                    pnlSSOBreadcrumb.Visible = False
                    pnlNonSSOBreadcrumb.Visible = True
                End If

                If BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO Then
                    btnAddNew.Visible = False
                End If
            End If
        End Sub

        Protected Sub btnCreateProfile_Click(sender As Object, e As EventArgs) Handles btnCreateProfile.Click
            Me.CheckSession()
            Dim errMsg As String = String.Empty
            Dim firstName As String = String.Empty
            Dim lastName As String = String.Empty
            Dim countryCode As String = String.Empty
            Dim zipCode As String = String.Empty


            Try
                Select Case BLL.SessionManager.PaymentType
                    Case Common.Enumerations.PaymentTypes.BankAccount
                        firstName = BLL.SessionManager.BankAccount.Owner.FirstName
                        lastName = BLL.SessionManager.BankAccount.Owner.LastName
                    Case Common.Enumerations.PaymentTypes.CreditCard
                        firstName = BLL.SessionManager.CreditCard.Owner.FirstName
                        lastName = BLL.SessionManager.CreditCard.Owner.LastName
                        countryCode = BLL.SessionManager.CreditCard.Owner.CountryCode
                        zipCode = Utility.SafeEncode(txtBillingZip.Text)
                End Select

                ' Create the user's profile
                Dim x As New B2P.Profile.User
                x.UserName = Utility.SafeEncode(txtUserID.Text)
                x.ClientCode = BLL.SessionManager.ClientCode
                x.EmailAddress = Utility.SafeEncode(txtProfileEmailAddress.Text)
                x.Status = B2P.Profile.User.UserStatus.Pending
                x.FirstName = firstName
                x.LastName = lastName
                x.Address1 = "NA"
                x.Address2 = ""
                x.City = "NA"
                x.CountryCode = countryCode
                x.State = ""
                x.ZipCode = zipCode
                x.CellPhoneNumber = ""

                Dim cpr As B2P.Profile.CreateProfileResponse = B2P.Profile.ProfileManagement.CreateProfile(x, txtPassword1.Text)

                Select Case cpr.Result
                    Case B2P.Profile.CreateProfileResponse.Results.Success

                        pnlCreateProfileForm.Visible = False
                        btnCreateProfile.Visible = False
                        btnCancelCreateProfile.Visible = False

                        ' Store the payment information with the profile
                        Dim apr As B2P.Profile.Wallet.AddPaymentMethodResults

                        Select Case BLL.SessionManager.PaymentType
                            Case Common.Enumerations.PaymentTypes.CreditCard
                                Dim ccInfo As New B2P.Profile.CreditCard
                                ccInfo.CreditCardNumber = BLL.SessionManager.CreditCard.InternalCreditCardNumber
                                ccInfo.ExpirationMonth = BLL.SessionManager.CreditCard.ExpirationMonth
                                ccInfo.ExpirationYear = BLL.SessionManager.CreditCard.ExpirationYear
                                ccInfo.NickName = BLL.SessionManager.CreditCard.CardIssuer & " " & Right(BLL.SessionManager.CreditCard.InternalCreditCardNumber, 5)
                                ccInfo.Owner.FirstName = BLL.SessionManager.CreditCard.Owner.FirstName
                                ccInfo.Owner.LastName = BLL.SessionManager.CreditCard.Owner.LastName
                                ccInfo.Owner.Address1 = "NA"
                                ccInfo.Owner.Address2 = "NA"
                                ccInfo.Owner.City = "NA"
                                ccInfo.Owner.State = "FL"
                                ccInfo.Owner.CountryCode = countryCode
                                ccInfo.Owner.ZipCode = zipCode
                                apr = B2P.Profile.Wallet.AddCreditCard(cpr.Profile_ID, ccInfo)
                                If apr.Result = Profile.Wallet.AddPaymentMethodResults.Results.Success Then
                                    psmErrorMessage.ToggleStatusMessage("Profile created successfully. Please check your email for instructions on how to activate your account.", StatusMessageType.Success, StatusMessageSize.Normal, True, True)
                                Else
                                    psmErrorMessage.ToggleStatusMessage("Profile created successfully. Your credit card could not be added at this time. Please check your email for instructions on how to activate your account.", StatusMessageType.Success, StatusMessageSize.Normal, True, True)
                                End If
                            Case Common.Enumerations.PaymentTypes.BankAccount
                                Dim achInfo As New B2P.Profile.BankAccount
                                achInfo.Owner.FirstName = BLL.SessionManager.BankAccount.Owner.FirstName
                                achInfo.Owner.LastName = BLL.SessionManager.BankAccount.Owner.LastName
                                achInfo.BankAccountType = BLL.SessionManager.BankAccount.BankAccountType
                                achInfo.BankRoutingNumber = BLL.SessionManager.BankAccount.BankRoutingNumber
                                achInfo.BankAccountNumber = BLL.SessionManager.BankAccount.InternalBankAccount
                                Dim NickName As String = Nothing
                                Dim AccountNumber As String = Right(BLL.SessionManager.BankAccount.InternalBankAccount, BLL.SessionManager.BankAccount.InternalBankAccount.Length - 1)
                                If AccountNumber.Length < 4 Then
                                    AccountNumber = "*" & Right(BLL.SessionManager.BankAccount.InternalBankAccount, BLL.SessionManager.BankAccount.InternalBankAccount.Length - 1)
                                End If
                                Select Case achInfo.BankAccountType
                                    Case "Checking"
                                        NickName = "Personal Checking " & AccountNumber
                                    Case "Savings"
                                        NickName = "Personal Savings " & AccountNumber
                                    Case "CommChecking"
                                        NickName = "Commercial Checking " & AccountNumber
                                    Case "CommSavings"
                                        NickName = "Commercial Savings " & AccountNumber
                                End Select

                                achInfo.NickName = NickName
                                apr = B2P.Profile.Wallet.AddBankAccount(cpr.Profile_ID, achInfo)
                                If apr.Result = Profile.Wallet.AddPaymentMethodResults.Results.Success Then
                                    psmErrorMessage.ToggleStatusMessage("Profile created successfully. Please check your email for instructions on how to activate your account.", StatusMessageType.Success, StatusMessageSize.Normal, True, True)
                                Else
                                    psmErrorMessage.ToggleStatusMessage("Profile created successfully. Your bank account could not be added at this time. Please check your email for instructions on how to activate your account.", StatusMessageType.Success, StatusMessageSize.Normal, True, True)
                                End If
                        End Select


                    Case B2P.Profile.CreateProfileResponse.Results.EmailAddressExists
                        psmErrorMessage.ToggleStatusMessage("Email address already exists. Please choose a different email address.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                    Case B2P.Profile.CreateProfileResponse.Results.ErrorOccurred
                        psmErrorMessage.ToggleStatusMessage("An error has occurred. Please try again.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                    Case B2P.Profile.CreateProfileResponse.Results.InvalidClientCode
                        psmErrorMessage.ToggleStatusMessage("An error has occurred. Please try again.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                    Case B2P.Profile.CreateProfileResponse.Results.InvalidEmail
                        psmErrorMessage.ToggleStatusMessage("An error has occurred. Please try again.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                    Case B2P.Profile.CreateProfileResponse.Results.InvalidPassword
                        psmErrorMessage.ToggleStatusMessage("An error has occurred. Please try again.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                    Case B2P.Profile.CreateProfileResponse.Results.InvalidUserName
                        psmErrorMessage.ToggleStatusMessage("An error has occurred. Please try again.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                    Case B2P.Profile.CreateProfileResponse.Results.UserNameExists
                        psmErrorMessage.ToggleStatusMessage("User name already exists. Please choose a different user name.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                End Select
                ClientScript.RegisterStartupScript([GetType](), "ModalScript", "$(function(){ $('#createProfileModal').modal('show'); });", True)
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
        Private Sub ddlCreditCountry_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCreditCountry.SelectedIndexChanged
            ClearAddressFields()
            SetCountryData()
            ClientScript.RegisterStartupScript([GetType](), "ModalScript", "$(function(){ $('#createProfileModal').modal('show'); });", True)
        End Sub

        ''' <summary>
        ''' Builds the account information panels.
        ''' </summary>
        Private Sub BuildForm()
            Dim CurrentCategory As B2P.Objects.Product
            CurrentCategory = BLL.SessionManager.CurrentCategory
            litClientName.Text = BLL.SessionManager.Client.ClientName
            '' Remove all the leading zero from confirmation number [RS]
            Dim confirmationNumber As String = BLL.SessionManager.ConfirmationNumber.TrimStart("0")
            litConfirmationNumber.Text = confirmationNumber


            ' Set the payment date
            litPaymentDate.Text = BLL.SessionManager.PaymentDate

            ' Set the confirmation email address; only show if not blank
            Select Case BLL.SessionManager.PaymentType
                Case Common.Enumerations.PaymentTypes.CreditCard
                    If BLL.SessionManager.CreditCard.Owner.EMailAddress <> "" Then
                        pnlEmailAddress.Visible = True
                        litEmailAddress.Text = BLL.SessionManager.CreditCard.Owner.EMailAddress
                    End If
                Case Common.Enumerations.PaymentTypes.BankAccount
                    If BLL.SessionManager.BankAccount.Owner.EMailAddress <> "" Then
                        pnlEmailAddress.Visible = True
                        litEmailAddress.Text = BLL.SessionManager.BankAccount.Owner.EMailAddress
                    End If
            End Select

            ' Set the client messages
            litClientMessage.Text = B2P.Objects.Client.GetClientMessage("Confirmation", BLL.SessionManager.ClientCode)
            litSystemMessage.Text = BLL.SessionManager.Client.ClientName & " thanks you for your business."

            ' Determine visibility of create profile button
            Dim productSource As New B2P.Objects.Product(BLL.SessionManager.ClientCode, BLL.SessionManager.ProductName)
            pnlCreateProfile.Visible = productSource.Sources.Portal

        End Sub
        ''' <summary>
        ''' Builds the payment method information panels.
        ''' </summary>
        Private Sub BuildPaymentMethod()

            'Dim cartFee As Decimal '= Utility.IIf(BLL.SessionManager.PaymentType = Common.Enumerations.PaymentTypes.CreditCard, BLL.SessionManager.Cart.CreditCardFee, BLL.SessionManager.Cart.ECheckFee)
            Dim total As Decimal '= BLL.SessionManager.TransactionInformation.CartTotal + cartFee
            Dim cf As B2P.Payment.FeeCalculation.CalculatedFee
            Dim errMsg As String = String.Empty

            Try
                Select Case BLL.SessionManager.PaymentType
                    Case Common.Enumerations.PaymentTypes.BankAccount
                        cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, BLL.SessionManager.PaymentAmount)
                        total = BLL.SessionManager.PaymentAmount + cf.ConvenienceFee
                        ' Show the payment method info
                        litPaymentMethod.Text = BLL.SessionManager.BankAccount.BankAccountType.ToString.Replace("_", " ") & " " & "*" & Right(BLL.SessionManager.BankAccount.BankAccountNumber, BLL.SessionManager.BankAccount.BankAccountNumber.Length - 1)
                        ' Set the payment amounts and fees
                        BLL.SessionManager.ConvenienceFee = cf.ConvenienceFee
                        BLL.SessionManager.TransactionFee = cf.TransactionFee
                        pnlCreditCardSuppInfo.Visible = False
                        If BLL.SessionManager.BankAccount.Owner.EMailAddress <> "" Then
                            txtProfileEmailAddress.Text = BLL.SessionManager.BankAccount.Owner.EMailAddress
                        End If

                    Case Common.Enumerations.PaymentTypes.CreditCard
                        Dim cardType As B2P.Payment.FeeCalculation.PaymentTypes = B2P.Payment.FeeCalculation.GetCardType(BLL.SessionManager.CreditCard.InternalCreditCardNumber)
                        cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, cardType, BLL.SessionManager.PaymentAmount)
                        total = BLL.SessionManager.PaymentAmount + cf.ConvenienceFee
                        ' Show the payment method info
                        litPaymentMethod.Text = BLL.SessionManager.CreditCard.CardIssuer & " " & Right(BLL.SessionManager.CreditCard.CreditCardNumber, 6)
                        ' Set the payment amounts and fees
                        BLL.SessionManager.ConvenienceFee = cf.ConvenienceFee
                        BLL.SessionManager.TransactionFee = cf.TransactionFee
                        pnlCreditCardSuppInfo.Visible = True
                        If BLL.SessionManager.CreditCard.Owner.EMailAddress <> "" Then
                            txtProfileEmailAddress.Text = BLL.SessionManager.CreditCard.Owner.EMailAddress
                        End If
                End Select

                ' Hide convenience fee link if there are no fees
                Dim fees As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode, BLL.SessionManager.ProductName, Common.Enumerations.TransactionSources.Web)

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

        Private Sub ClearAddressFields()
            txtBillingZip.Text = String.Empty
        End Sub

        ''' <summary>
        ''' Loads the available countries to a DropDownList.
        ''' </summary>
        Private Sub LoadCountries()
            ddlCreditCountry.Items.Clear()
            ddlCreditCountry.DataSource = B2P.Payment.PaymentBase.ListCountries
            ddlCreditCountry.DataTextField = "CountryName"
            ddlCreditCountry.DataValueField = "CountryCode"
            ddlCreditCountry.DataBind()

            ' For testing validation
            'ddlCountry.Items.Insert(0, New ListItem("Select an option...", String.Empty))
            'ddlCountry.SelectedIndex = 0
        End Sub
        ''' <summary>
        ''' Adds client side javascript to the various server controls.
        ''' </summary>
        Private Sub RegisterClientJs()
            txtUserID.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumeric)")
            txtProfileEmailAddress.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.Email)")
            txtProfileEmailAddress.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.Email)")
            btnCancelCreateProfile.Attributes.Add("onClick", "clearValidationItems()")

            Select Case BLL.SessionManager.PaymentType
                Case Common.Enumerations.PaymentTypes.CreditCard
                    btnCreateProfile.Attributes.Add("onClick", "return validateForm('CC')")
                Case Else
                    btnCreateProfile.Attributes.Add("onClick", "return validateForm('ACH')")
            End Select
        End Sub

        Private Sub SetCountryData()
            Select Case Me.ddlCreditCountry.SelectedValue
                Case "US"
                    litBillingZip.Text = GetGlobalResourceObject("WebResources", "lblBillingZip").ToString()
                Case "CA"
                    litBillingZip.Text = GetGlobalResourceObject("WebResources", "lblBillingPostalCode").ToString()
                Case Else
                    litBillingZip.Text = GetGlobalResourceObject("WebResources", "lblBillingZip").ToString()
                    txtBillingZip.Text = String.Empty
            End Select
        End Sub

        Protected Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
            Dim clientCode As String = BLL.SessionManager.ClientCode
            BLL.SessionManager.Clear()
            Response.Redirect("/client/" & clientCode)
        End Sub
    End Class
End Namespace