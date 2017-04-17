Imports System
Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.Web
Public Class CartDetails
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadContent()
        SetVisibility()
    End Sub
    Public Sub LoadContent()
        If BLL.SessionManager.ManageCart.Cart.Count = 0 Then
            tblCartDetails.Visible = False
            Exit Sub
        End If
        cartProduct.Text = BLL.SessionManager.ManageCart.Cart(BLL.SessionManager.ManageCart.Cart.Count - 1).Item
        cartHeadingCount.Text = BLL.SessionManager.ManageCart.Cart.Count.ToString()
        cartHeadingAmount.Text = SubTotal()
    End Sub
    Private Sub SetVisibility()
        tblCartDetails.Visible = Me.Parent.FindControl("ctlCartGrid").Visible
    End Sub
    Private Function SubTotal() As String
        Dim amount As Double = 0
        For Each cart As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
            amount += cart.Amount
        Next
        Return String.Format("{0:C}", amount)
    End Function
End Class