Imports B2P.PaymentLanding.Express

Public Class ShoppingCart
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cartCount.InnerHtml = BLL.SessionManager.ManageCart.CartCount
    End Sub

End Class