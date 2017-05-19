Imports System
Imports System.Web
Imports B2P.Integration.TriPOS


Public Class _Default : Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim request = HttpContext.Current.Request
            Dim qsToken As String = String.Empty
            Dim info As B2P.SSOLookup.PaymentInformation = Nothing
            Dim item As B2P.SSOLookup.PaymentInformation.CartItem = Nothing
            Dim items As B2P.Payment.PaymentBase.TransactionItems = Nothing
            Dim cart As B2P.ShoppingCart.Cart = Nothing
            Dim errMsg As String = String.Empty



        ' Make sure that client passed the token
        qsToken = HttpUtility.ParseQueryString(request.Url.Query)("token")

            ' Token pseudo-validation
            If Not Utility.IsNullOrEmpty(qsToken) AndAlso qsToken.Trim.Length = 32 Then
                psmErrorMessage.ToggleStatusMessage(String.Empty, StatusMessageType.None, StatusMessageSize.Normal, False, False)

                ' Get some customer data
                info = New B2P.SSOLookup.PaymentInformation(qsToken.Trim)
            cart = CType(Session("ShoppingCart"), B2P.ShoppingCart.Cart)

            ' Make sure there is stuff in the cart
            If cart.TotalAmount > 0 Then
                items = New B2P.Payment.PaymentBase.TransactionItems

                ' Set the session object values
                BLL.SessionManager.Token = qsToken.Trim
                BLL.SessionManager.TransactionInformation = info
                BLL.SessionManager.Cart = cart
                BLL.SessionManager.VendorReferenceCode = info.VendorReferenceCode
                'BLL.SessionManager.ClientCode = info.ClientCode
                BLL.SessionManager.ClientName = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode).ClientName
                BLL.SessionManager.ClientEmail = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode).ContactEmail
                BLL.SessionManager.ClientPhone = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode).ContactPhone

                ' TODO: GET OFFICE LOCATION

                ' Got here -- initialize
                BLL.SessionManager.IsInitialized = True
            Else
                psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
                End If
            Else
                psmErrorMessage.ToggleStatusMessage("Missing or invalid token.", StatusMessageType.Danger, True, True)
            End If

            ' Send them off
            If BLL.SessionManager.IsInitialized Then
            Response.Redirect("/payment/PinPad.aspx", False)
        End If
        End Sub

    End Class

