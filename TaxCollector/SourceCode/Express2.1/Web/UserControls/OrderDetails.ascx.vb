Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.BLL
Imports B2P.PaymentLanding.Express.Web
Public Class OrderDetails
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            lblSubTotal.Text = CalculateSubTotal()
            PopulateOrderGrid()
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

    Protected Function PopulateOrderGrid()

        rptOrder.DataSource = BLL.SessionManager.ManageCart.Cart
        rptOrder.DataBind()
    End Function

    Protected Sub rptOrder_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptOrder.ItemCommand

        Dim drv As DataRowView = DirectCast(e.Item.DataItem, DataRowView)

        'Repeater rptOrderAcc = DirectCast(e.Item.FindControl("rptOrderAccount"), Repeater)
        'rptOrderAcc.DataSource = BLL.SessionManager.ManageCart.Cart(0).AccountIdFields
        'rptOrderAcc.DataBind()
    End Sub
End Class

