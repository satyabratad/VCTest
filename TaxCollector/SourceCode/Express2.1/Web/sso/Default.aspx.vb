Imports System
Imports System.IO

Namespace B2P.PaymentLanding.Express.Web
    ''' <summary>
    ''' Default page for SSO client
    ''' </summary>
    Public Class _ssodefault : Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not BLL.SessionManager.ClientType = Cart.EClientType.SSO Then
                Session.Clear()
                BLL.SessionManager.ManageCart.ShowCart = False
            End If

            If BLL.SessionManager.ManageCart.EditItemIndex > -1 Then
                pnlCart.Visible = False
                pnlEdit.Visible = True
                pnlError.Visible = False
                Dim _SelectedItem = BLL.SessionManager.ManageCart.Cart.FirstOrDefault(Function(p) p.Index = BLL.SessionManager.ManageCart.EditItemIndex)
                If Not _SelectedItem Is Nothing Then
                    ctlEditLookupItem.SelectedItem = _SelectedItem
                End If
                Exit Sub
            Else
                pnlCart.Visible = False
                pnlEdit.Visible = False
                pnlError.Visible = False
            End If

            Dim redirectAddress As String = String.Empty


            If BLL.SessionManager.ManageCart.ShowCart = False Then
                If Not IsPostBack Then
                    Dim errMsg As String = String.Empty
                    If Not IsNothing(BLL.SessionManager.LookupData) Then
                        Response.Redirect("/errors/session", False)
                    Else



                        Session.Clear()
                        BLL.SessionManager.PaymentMade = False
                        BLL.SessionManager.CreditCard = Nothing
                        BLL.SessionManager.BankAccount = Nothing
                        BLL.SessionManager.IsSSOProduct = True
                        Try
                            'Dim Token As String = "17762b6e22bc4ed9b4c60571b79ada36" 'f491d9e6ed2242088bf66c073b917cb5

                            If Not IsNothing(Page.RouteData.Values("token")) Then
                                Dim Token As String = Page.RouteData.Values("token")
                                Dim TokenPattern As Regex = New Regex("^[A-Fa-f0-9]{32}$")
                                Dim matches As MatchCollection = TokenPattern.Matches(Token)


                                redirectAddress = ResolveUrl("~/sso/")
                                BreadCrumbMenu.RedirectAddress = redirectAddress

                                If matches.Count > 0 Then

                                    Dim x As New B2P.SSOLookup.PaymentInformation(Token)

                                    Session("ClickedButton") = False
                                    If x.Found = True Then
                                        BLL.SessionManager.Token = Token
                                        BLL.SessionManager.ClientCode = x.ClientCode
                                        BLL.SessionManager.ClientFAQ = "/faq/faq.html"

                                        ' Check to see if custom CSS file exists for client
                                        If File.Exists(Server.MapPath("/Css/ClientCSS/" & BLL.SessionManager.ClientCode & ".css")) Then
                                            BLL.SessionManager.ClientCSS = "/Css/ClientCSS/" & BLL.SessionManager.ClientCode & ".css"
                                        Else
                                            BLL.SessionManager.ClientCSS = "/Css/app.css"
                                        End If

                                        ' Set the CSS link to the appropriate file name
                                        lnkCSS.Attributes("href") = BLL.SessionManager.ClientCSS

                                        ' Check for custom FAQ page
                                        If File.Exists(Server.MapPath("/faq/ClientFAQ/" & BLL.SessionManager.ClientCode & ".html")) Then
                                            BLL.SessionManager.ClientFAQ = "/faq/ClientFAQ/" & BLL.SessionManager.ClientCode & ".html"
                                        Else
                                            BLL.SessionManager.ClientFAQ = "/faq/faq.html"
                                        End If


                                        Dim z As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode.ToString)

                                        'Determine SSO type
                                        Select Case z.SSODisplayType
                                            Case B2P.Objects.Client.SSODisplayTypes.ReadOnlyGrid

                                                BLL.SessionManager.ClientType = Cart.EClientType.SSO
                                                BLL.SessionManager.SSODisplayType = B2P.Objects.Client.SSODisplayTypes.ReadOnlyGrid
                                                loadDataGrid(x, Token)
                                            Case B2P.Objects.Client.SSODisplayTypes.ShoppingCart

                                                BLL.SessionManager.SSODisplayType = B2P.Objects.Client.SSODisplayTypes.ShoppingCart
                                                BLL.SessionManager.ClientType = Cart.EClientType.SSO
                                                loadShopingCartDataGrid(x, Token)
                                            Case B2P.Objects.Client.SSODisplayTypes.SingleItem

                                                BLL.SessionManager.ClientType = Cart.EClientType.SSO
                                                BLL.SessionManager.SSODisplayType = B2P.Objects.Client.SSODisplayTypes.SingleItem
                                                loadData(x, Token)
                                        End Select

                                    Else
                                        Session.Clear()
                                        B2P.Common.Logging.LogError("B2P", Token & " - Token not found", B2P.Common.Logging.NotifySupport.No)
                                        psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
                                        ShowErrorPanel()
                                        Exit Sub
                                    End If
                                Else
                                    psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
                                    ShowErrorPanel()
                                    Exit Sub
                                End If
                            Else
                                psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
                                ShowErrorPanel()
                                Exit Sub
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
                End If
            Else
                pnlCart.Visible = True
                pnlError.Visible = False
                'pnlEditLookupItem.Visible = False
                'If z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Lookup Or z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Table Then
                '    BLL.SessionManager.ClientType = B2P.Cart.EClientType.Lookup
                'Else
                '    BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup
                'End If

                ctlCartGrid.PopulateGrid("ctlCartGrid")
            End If
        End Sub
        Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
            'BLL.SessionManager.LookupData.DueDate = ""
            'Dim CurrentCategory As New B2P.Objects.Product(BLL.SessionManager.ClientCode.ToString, ddlCategories.SelectedValue, B2P.Common.Enumerations.TransactionSources.Web)
            'Dim z As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode.ToString, ddlCategories.SelectedValue, B2P.Common.Enumerations.TransactionSources.Web)

            'BLL.SessionManager.CreditFeeDescription = z.CreditCardFeeDescription
            'BLL.SessionManager.AchFeeDescription = z.BankAccountFeeDescription

            'BLL.SessionManager.CurrentCategory = CurrentCategory
            'BLL.SessionManager.ProductName = ddlCategories.SelectedValue
            'BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(txtLookupAccount1.Text)
            'BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(txtLookupAccount2.Text)
            'BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(txtLookupAccount3.Text)

            If BLL.SessionManager.IsContactInfoRequired Then
                Response.Redirect("/pay/ContactInfo.aspx", False)
            Else
                Response.Redirect("/pay/payment.aspx", False)
            End If

        End Sub
        Private Sub loadData(ByVal TokenInfo As B2P.SSOLookup.PaymentInformation, ByVal Token As String)
            Dim errMsg As String = String.Empty

            Try
                Dim z As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode.ToString)

                BLL.SessionManager.CategoryList = B2P.Objects.Product.ListProducts(BLL.SessionManager.ClientCode, B2P.Common.Enumerations.TransactionSources.Web)
                BLL.SessionManager.Client = z
                BLL.SessionManager.LookupAmount = Nothing
                BLL.SessionManager.LookupAmountMinimum = False
                BLL.SessionManager.LookupAmountEditable = True
                BLL.SessionManager.OfficeID = TokenInfo.Office_ID
                BLL.SessionManager.VendorReferenceCode = TokenInfo.VendorReferenceCode

                Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchResults

                y = B2P.ClientInterface.Manager.ClientInterface.GetClientData(Token)
                BLL.SessionManager.LookupProduct = y.ProductName
                BLL.SessionManager.LookupData = y
                BLL.SessionManager.AmountEditable = True
                BLL.SessionManager.TokenInfo = TokenInfo
                Dim CurrentCategory As New B2P.Objects.Product(BLL.SessionManager.ClientCode.ToString, BLL.SessionManager.LookupProduct, B2P.Common.Enumerations.TransactionSources.Web)


                Select Case y.SearchResult
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.Success

                        BLL.SessionManager.ServiceAddress = Utility.SafeEncode(y.Demographics.Address1.Value)
                        BLL.SessionManager.NameOnLookupAccount = Utility.SafeEncode(y.Demographics.FirstName.Value & " " & y.Demographics.LastName.Value)

                        'Added by RS
                        Dim address1 As String = Utility.SafeEncode(y.Demographics.Address1.Value)
                        Dim address2 As String = Utility.SafeEncode(y.Demographics.Address2.Value)
                        Dim contactName As String = ""
                        Dim country As String = ""
                        Dim city As String = Utility.SafeEncode(y.Demographics.City.Value)
                        Dim state As String = Utility.SafeEncode(y.Demographics.State.Value)
                        Dim zipCode As String = Utility.SafeEncode(y.Demographics.ZipCode.Value)
                        Dim homePhone As String = Utility.SafeEncode(y.Demographics.HomePhone.Value)
                        BLL.SessionManager.AddContactInfo(address1, address2, contactName, country, state, city, zipCode, homePhone)
                        BLL.SessionManager.PaymentStatusCode = y.PaymentStatus
                        Select Case y.PaymentStatus
                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.Allowed

                                BLL.SessionManager.LookupData = y
                                BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(y.AccountNumber1.Value)
                                BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(y.AccountNumber2.Value)
                                BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(y.AccountNumber3.Value)
                                BLL.SessionManager.ProductName = BLL.SessionManager.LookupProduct
                                BLL.SessionManager.CurrentCategory = CurrentCategory
                                BLL.SessionManager.LookupAmount = Utility.SafeEncode(y.AmountDue)

                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.NotAllowed
                                Response.Redirect("errors/")

                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.MinimumPaymentRequired
                                BLL.SessionManager.LookupData = y
                                BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(y.AccountNumber1.Value)
                                BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(y.AccountNumber2.Value)
                                BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(y.AccountNumber3.Value)
                                BLL.SessionManager.ProductName = BLL.SessionManager.LookupProduct
                                BLL.SessionManager.CurrentCategory = CurrentCategory
                                BLL.SessionManager.LookupAmount = Utility.SafeEncode(y.AmountDue)
                                BLL.SessionManager.LookupAmountMinimum = True

                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.NotEditable
                                BLL.SessionManager.LookupData = y
                                BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(y.AccountNumber1.Value)
                                BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(y.AccountNumber2.Value)
                                BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(y.AccountNumber3.Value)
                                BLL.SessionManager.ProductName = BLL.SessionManager.LookupProduct
                                BLL.SessionManager.CurrentCategory = CurrentCategory
                                BLL.SessionManager.LookupAmount = Utility.SafeEncode(y.AmountDue)
                                BLL.SessionManager.LookupAmountEditable = False
                        End Select
                    Case Else
                        psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
                        ShowErrorPanel()
                        Exit Sub
                End Select


                If CurrentCategory.PaymentInformation.ACHAccepted = False AndAlso CurrentCategory.PaymentInformation.CreditCardAccepted = False Then
                    psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                    ShowErrorPanel()
                    Exit Sub
                End If
                '' Add to Cart
                Dim ci As SSOLookup.PaymentInformation.CartItem
                ci = DirectCast(TokenInfo.CartItems(0), SSOLookup.PaymentInformation.CartItem)
                AddToCart(z, CurrentCategory, ci)
                Dim a As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode.ToString, BLL.SessionManager.LookupProduct, B2P.Common.Enumerations.TransactionSources.Web)

                BLL.SessionManager.CreditFeeDescription = a.CreditCardFeeDescription
                BLL.SessionManager.AchFeeDescription = a.BankAccountFeeDescription
                BLL.SessionManager.CurrentCategory = CurrentCategory



                Dim bk As New B2P.Common.BlockedAccounts.BlockedAccountResults
                bk = B2P.Common.BlockedAccounts.CheckForBlockedAccount(BLL.SessionManager.ClientCode, BLL.SessionManager.LookupProduct, Utility.SafeEncode(BLL.SessionManager.LookupData.AccountNumber1.Value),
                                        Utility.SafeEncode(BLL.SessionManager.LookupData.AccountNumber2.Value), Utility.SafeEncode(BLL.SessionManager.LookupData.AccountNumber3.Value))
                If B2P.Objects.Client.GetClientMessage("Welcome", BLL.SessionManager.ClientCode.ToString) <> "" Then

                    If bk.IsAccountBlocked Then
                        If bk.ACHBlocked = True AndAlso bk.CreditCardBlocked = True Then
                            Response.Redirect("payment.message", False)
                            Exit Sub
                        Else
                            BLL.SessionManager.IsInitialized = True
                        End If

                        If bk.ACHBlocked = True Then
                            BLL.SessionManager.BlockedACH = True
                        Else
                            BLL.SessionManager.BlockedACH = False
                        End If
                        If bk.CreditCardBlocked = True Then
                            BLL.SessionManager.BlockedCC = True
                        Else BLL.SessionManager.BlockedCC = False
                        End If
                    Else
                        BLL.SessionManager.BlockedCC = False
                        BLL.SessionManager.BlockedACH = False
                        BLL.SessionManager.IsInitialized = True
                        If BLL.SessionManager.ManageCart.ShowCart = True Then
                            Response.Redirect(BreadCrumbMenu.RedirectAddress, False)

                        End If

                    End If

                Else
                    If BLL.SessionManager.TokenInfo.AllowCreditCard = False And BLL.SessionManager.TokenInfo.AllowECheck = False Then
                        psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                        ShowErrorPanel()
                        Exit Sub
                    Else
                        If bk.IsAccountBlocked Then
                            If bk.ACHBlocked = True AndAlso bk.CreditCardBlocked = True Then
                                psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                                ShowErrorPanel()
                                Exit Sub
                            Else
                                BLL.SessionManager.IsInitialized = True
                            End If

                            If bk.ACHBlocked = True Then
                                BLL.SessionManager.BlockedACH = True
                            Else
                                BLL.SessionManager.BlockedACH = False
                            End If

                            If bk.CreditCardBlocked = True Then
                                BLL.SessionManager.BlockedCC = True
                            Else
                                BLL.SessionManager.BlockedCC = False
                            End If
                        Else
                            BLL.SessionManager.BlockedCC = False
                            BLL.SessionManager.BlockedACH = False
                            BLL.SessionManager.IsInitialized = True
                            If BLL.SessionManager.ManageCart.ShowCart = True Then
                                Response.Redirect(BreadCrumbMenu.RedirectAddress, False)

                            End If

                        End If

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
            End Try

        End Sub

        Private Sub ShowErrorPanel()
            pnlError.Visible = True
            pnlCart.Visible = False
            BreadCrumbMenu.Visible = False
            ShoppingCart.Visible = False
        End Sub

        Private Sub loadDataGrid(ByVal TokenInfo As B2P.SSOLookup.PaymentInformation, ByVal Token As String)
            Dim errMsg As String = String.Empty

            Try

                Dim z As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode.ToString)
                BLL.SessionManager.Client = z
                BLL.SessionManager.LookupAmount = Nothing
                BLL.SessionManager.LookupAmountMinimum = False
                BLL.SessionManager.LookupAmountEditable = False
                BLL.SessionManager.OfficeID = TokenInfo.Office_ID
                BLL.SessionManager.VendorReferenceCode = TokenInfo.VendorReferenceCode
                BLL.SessionManager.LookupProduct = TokenInfo.CartItems(0).ProductName
                BLL.SessionManager.ProductName = TokenInfo.CartItems(0).ProductName
                BLL.SessionManager.LookupAmount = TokenInfo.CartTotal
                BLL.SessionManager.TokenInfo = TokenInfo

                Dim CurrentCategory As New B2P.Objects.Product(BLL.SessionManager.ClientCode.ToString, BLL.SessionManager.LookupProduct, B2P.Common.Enumerations.TransactionSources.Web)
                Dim a As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode.ToString, BLL.SessionManager.LookupProduct, B2P.Common.Enumerations.TransactionSources.Web)

                BLL.SessionManager.CreditFeeDescription = a.CreditCardFeeDescription
                BLL.SessionManager.AchFeeDescription = a.BankAccountFeeDescription
                BLL.SessionManager.CurrentCategory = CurrentCategory

                If (BLL.SessionManager.TokenInfo.AllowCreditCard = False And BLL.SessionManager.TokenInfo.AllowECheck = False) Or TokenInfo.IsCartValid = False Then
                    psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                    ShowErrorPanel()
                    Exit Sub
                Else
                    Dim bk As New B2P.Common.BlockedAccounts.BlockedAccountResults
                    BLL.SessionManager.BlockedACH = False
                    BLL.SessionManager.BlockedCC = False

                    For Each ci As B2P.SSOLookup.PaymentInformation.CartItem In TokenInfo.CartItems
                        bk = B2P.Common.BlockedAccounts.CheckForBlockedAccount(BLL.SessionManager.ClientCode, ci.ProductName, Utility.SafeEncode(ci.AccountNumber1),
                                                        Utility.SafeEncode(ci.AccountNumber2), Utility.SafeEncode(ci.AccountNumber3))
                        If bk.IsAccountBlocked Then
                            If bk.ACHBlocked = True AndAlso bk.CreditCardBlocked = True Then
                                psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                                ShowErrorPanel()
                                Exit Sub
                            End If
                            If bk.ACHBlocked = True Then
                                BLL.SessionManager.BlockedACH = True
                            End If
                            If bk.CreditCardBlocked = True Then
                                BLL.SessionManager.BlockedCC = True
                            End If
                        End If

                        '' Add to Cart
                        AddToCart(z, CurrentCategory, ci)
                    Next
                    If BLL.SessionManager.BlockedCC And BLL.SessionManager.BlockedACH Then
                        psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                        ShowErrorPanel()
                        Exit Sub
                    Else

                        BLL.SessionManager.IsInitialized = True
                        If BLL.SessionManager.ManageCart.ShowCart = True Then
                            Response.Redirect(BreadCrumbMenu.RedirectAddress, False)

                        End If

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
            End Try

        End Sub


        ''' <summary>
        ''' This method will load the shopping cart details in to data grid 
        ''' </summary>
        ''' <param name="TokenInfo"></param>
        ''' <param name="Token"></param>
        Private Sub loadShopingCartDataGrid(ByVal TokenInfo As B2P.SSOLookup.PaymentInformation, ByVal Token As String)
            Dim errMsg As String = String.Empty
            Dim cart As New B2P.Cart.Cart
            Dim paddress As PropertyAddress

            Try

                Dim z As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode.ToString)
                BLL.SessionManager.CategoryList = B2P.Objects.Product.ListProducts(BLL.SessionManager.ClientCode, B2P.Common.Enumerations.TransactionSources.Web)
                BLL.SessionManager.Client = z
                BLL.SessionManager.LookupAmount = Nothing
                BLL.SessionManager.LookupAmountMinimum = False
                BLL.SessionManager.LookupAmountEditable = False
                BLL.SessionManager.OfficeID = TokenInfo.Office_ID
                BLL.SessionManager.VendorReferenceCode = TokenInfo.VendorReferenceCode
                BLL.SessionManager.LookupProduct = TokenInfo.CartItems(0).ProductName
                BLL.SessionManager.ProductName = TokenInfo.CartItems(0).ProductName
                BLL.SessionManager.LookupAmount = TokenInfo.CartTotal
                BLL.SessionManager.TokenInfo = TokenInfo


                Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchResults

                y = B2P.ClientInterface.Manager.ClientInterface.GetClientData(Token)
                ' BLL.SessionManager.LookupProduct = y.ProductName
                BLL.SessionManager.LookupData = y

                Dim p As New B2P.Objects.Product(BLL.SessionManager.ClientCode, TokenInfo.CartItems(0).ProductName, B2P.Common.Enumerations.TransactionSources.Web)
                Utility.SetBreadCrumbContactInfo("BreadCrumbMenu")

                Dim CurrentCategory As New B2P.Objects.Product(BLL.SessionManager.ClientCode.ToString, BLL.SessionManager.LookupProduct, B2P.Common.Enumerations.TransactionSources.Web)
                Dim a As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode.ToString, BLL.SessionManager.LookupProduct, B2P.Common.Enumerations.TransactionSources.Web)

                BLL.SessionManager.CreditFeeDescription = a.CreditCardFeeDescription
                BLL.SessionManager.AchFeeDescription = a.BankAccountFeeDescription
                BLL.SessionManager.CurrentCategory = CurrentCategory

                If (BLL.SessionManager.TokenInfo.AllowCreditCard = False And BLL.SessionManager.TokenInfo.AllowECheck = False) Or TokenInfo.IsCartValid = False Then
                    psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                    ShowErrorPanel()
                    Exit Sub
                Else
                    Dim bk As New B2P.Common.BlockedAccounts.BlockedAccountResults
                    BLL.SessionManager.BlockedACH = False
                    BLL.SessionManager.BlockedCC = False

                    For Each ci As B2P.SSOLookup.PaymentInformation.CartItem In TokenInfo.CartItems
                        bk = B2P.Common.BlockedAccounts.CheckForBlockedAccount(BLL.SessionManager.ClientCode, ci.ProductName, Utility.SafeEncode(ci.AccountNumber1),
                                                        Utility.SafeEncode(ci.AccountNumber2), Utility.SafeEncode(ci.AccountNumber3))
                        If bk.IsAccountBlocked Then
                            If bk.ACHBlocked = True AndAlso bk.CreditCardBlocked = True Then
                                psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                                ShowErrorPanel()
                                Exit Sub
                            End If
                            If bk.ACHBlocked = True Then
                                BLL.SessionManager.BlockedACH = True
                            End If
                            If bk.CreditCardBlocked = True Then
                                BLL.SessionManager.BlockedCC = True
                            End If
                        Else
                            ' Adding to cart 
                            AddToCart(z, CurrentCategory, ci)

                        End If
                    Next

                    If BLL.SessionManager.BlockedCC And BLL.SessionManager.BlockedACH Then
                        psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                        ShowErrorPanel()
                        Exit Sub
                    Else
                        BLL.SessionManager.IsInitialized = True
                        If BLL.SessionManager.ManageCart.ShowCart = True Then
                            Response.Redirect(BreadCrumbMenu.RedirectAddress, False)

                        End If


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
            End Try

        End Sub

        ''' <summary>
        ''' This method will add the items for the SSO client in to  shopping cart
        ''' </summary>
        ''' <param name="z"> Clint object</param>
        ''' <param name="CurrentCategory">Category</param>
        ''' <param name="ci">Cart item</param>
        ''' <returns></returns>
        Private Shared Function AddToCart(z As Objects.Client, CurrentCategory As Objects.Product, ci As SSOLookup.PaymentInformation.CartItem)
            Dim cart As New B2P.Cart.Cart

            cart.Item = ci.ProductName
            Dim acc1 As New B2P.Cart.AccountIdField(CurrentCategory.WebOptions.AccountIDField1.Label, Utility.SafeEncode(ci.AccountNumber1))
            Dim acc2 As New B2P.Cart.AccountIdField(CurrentCategory.WebOptions.AccountIDField2.Label, Utility.SafeEncode(ci.AccountNumber2))
            Dim acc3 As New B2P.Cart.AccountIdField(CurrentCategory.WebOptions.AccountIDField3.Label, Utility.SafeEncode(ci.AccountNumber3))
            cart.AccountIdFields = New List(Of B2P.Cart.AccountIdField)
            cart.AccountIdFields.Add(acc1)
            cart.AccountIdFields.Add(acc2)
            cart.AccountIdFields.Add(acc3)
            cart.Amount = ci.Amount
            cart.AmountDue = ci.Amount

            cart.PropertyAddress = New Cart.PropertyAddress

            cart.PropertyAddress.Address1 = z.Address1
            cart.PropertyAddress.Address2 = z.Address2
            cart.PropertyAddress.City = z.City
            cart.PropertyAddress.State = z.State
            cart.PropertyAddress.Zip = z.ZipCode


            If BLL.SessionManager.ManageCart.AddToCart(cart) Then
                BLL.SessionManager.ManageCart.ShowCart = True
            End If

            Return cart
        End Function
    End Class
End Namespace
