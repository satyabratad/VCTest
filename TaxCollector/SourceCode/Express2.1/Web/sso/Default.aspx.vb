Imports System
Imports System.IO

Namespace B2P.PaymentLanding.Express.Web
    Public Class _ssodefault : Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
                                            BLL.SessionManager.SSODisplayType = B2P.Objects.Client.SSODisplayTypes.ReadOnlyGrid
                                            loadDataGrid(x, Token)
                                        Case B2P.Objects.Client.SSODisplayTypes.ShoppingCart
                                            'Not used yet
                                        Case B2P.Objects.Client.SSODisplayTypes.SingleItem
                                            BLL.SessionManager.SSODisplayType = B2P.Objects.Client.SSODisplayTypes.SingleItem
                                            loadData(x, Token)
                                    End Select

                                Else
                                    Session.Clear()
                                    B2P.Common.Logging.LogError("B2P", Token & " - Token not found", B2P.Common.Logging.NotifySupport.No)
                                    psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
                                    Exit Sub
                                End If
                            Else
                                psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
                                Exit Sub
                            End If
                        Else
                            psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
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
                        Exit Sub
                End Select

                If CurrentCategory.PaymentInformation.ACHAccepted = False AndAlso CurrentCategory.PaymentInformation.CreditCardAccepted = False Then
                    psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                    Exit Sub
                End If
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
                        Response.Redirect("/pay/payment.aspx", False)
                    End If

                Else
                    If BLL.SessionManager.TokenInfo.AllowCreditCard = False And BLL.SessionManager.TokenInfo.AllowECheck = False Then
                        psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                        Exit Sub
                    Else
                        If bk.IsAccountBlocked Then
                            If bk.ACHBlocked = True AndAlso bk.CreditCardBlocked = True Then
                                psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
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
                            Response.Redirect("/pay/payment.aspx", False)
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
                                Exit Sub
                            End If
                            If bk.ACHBlocked = True Then
                                BLL.SessionManager.BlockedACH = True
                            End If
                            If bk.CreditCardBlocked = True Then
                                BLL.SessionManager.BlockedCC = True
                            End If
                        End If
                    Next
                    If BLL.SessionManager.BlockedCC And BLL.SessionManager.BlockedACH Then
                        psmErrorMessage.ToggleStatusMessage("We are not able to accept any online payments for this account. Please call Customer Service at " & BLL.SessionManager.Client.ContactPhone & ".", StatusMessageType.Danger, True, True)
                        Exit Sub
                    Else

                        BLL.SessionManager.IsInitialized = True
                        Response.Redirect("/pay/payment.aspx", False)
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

    End Class
End Namespace