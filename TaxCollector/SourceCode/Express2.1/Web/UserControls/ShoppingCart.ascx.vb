Imports System
Imports B2P.PaymentLanding.Express

Public Class ShoppingCart
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cartCount.InnerHtml = BLL.SessionManager.ManageCart.CartCount
    End Sub

    Protected Sub btnCart_Click(sender As Object, e As EventArgs) Handles btnCart.Click
        If BLL.SessionManager.ManageCart.CartCount > 0 Then


            BLL.SessionManager.ManageCart.ShowCart = True
            BLL.SessionManager.ManageCart.EditItemIndex = -1
            Response.Redirect("~/pay/")
        End If
    End Sub


End Class