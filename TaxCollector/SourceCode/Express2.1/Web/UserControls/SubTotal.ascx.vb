Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.Web
Public Class SubTotal
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            lblSubTotal.Text = CalculateSubTotal()
        End If

    End Sub
    Protected Function CalculateSubTotal() As String
        Dim total As Double = 0
        If Not BLL.SessionManager.ManageCart.Cart Is Nothing Then
            For Each item As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
                total += item.Amount
            Next
        End If
        Return String.Format("{0:C}", total)
    End Function
End Class