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
        lblCartHeadingText1.Text = GetGlobalResourceObject("WebResources", "CartHeaderText1").ToString()
        lblCartText1.Text = GetGlobalResourceObject("WebResources", "CartHeaderText").ToString()
        cartProduct.Text = BLL.SessionManager.ManageCart.Cart(BLL.SessionManager.ManageCart.Cart.Count - 1).Item
        If BLL.SessionManager.ManageCart.Cart.Count <= 1 Then
            lblCartText2.Text = GetGlobalResourceObject("WebResources", "CartHeaderSingleItemCount").ToString() + "): "
        Else
            lblCartText2.Text = GetGlobalResourceObject("WebResources", "CartHeaderMultipleItemCount").ToString() + "): "
        End If
        lblCartHeadingCount.Text = " (" + BLL.SessionManager.ManageCart.Cart.Count.ToString()
        lblCartHeadingAmount.Text = SubTotal()
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