Imports System
Imports B2P.PaymentLanding.Express

Public Class ShoppingCart
    Inherits System.Web.UI.UserControl

#Region "::: Control Event Handlers :::"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cartCount.InnerHtml = BLL.SessionManager.ManageCart.CartCount
    End Sub

    Protected Sub btnCart_Click(sender As Object, e As EventArgs) Handles btnCart.Click
        If BLL.SessionManager.ManageCart.CartCount > 0 Then


            BLL.SessionManager.ManageCart.ShowCart = True
            BLL.SessionManager.ManageCart.EditItemIndex = -1

            If BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO Then
                Response.Redirect("~/sso/")
            Else
                Response.Redirect("~/pay/")
            End If
        End If
    End Sub
#End Region

End Class