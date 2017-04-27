Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.BLL
Imports B2P.PaymentLanding.Express.Web
''' <summary>
''' This will display the order details in a list grid format
''' </summary>
Public Class OrderDetails
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            PopulateOrderGrid()
        End If
    End Sub

    ''' <summary>
    ''' This will populate the Order detailsby binding the items in grid
    ''' </summary>
    ''' <returns></returns>
    Protected Function PopulateOrderGrid()

        rptOrder.DataSource = BLL.SessionManager.ManageCart.Cart
        rptOrder.DataBind()
    End Function

    Protected Sub rptOrder_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptOrder.ItemCommand


    End Sub
    ''' <summary>
    ''' This will return display formated amount
    ''' </summary>
    ''' <returns></returns>
    Protected Function FormatAmount(Amount As Double) As String
        Return String.Format("{0:C}", Amount)
    End Function
    ''' <summary>
    ''' This will populate the Order detailsby binding the account details of Item/service in grid
    ''' </summary>
    Protected Sub rptOrder_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptOrder.ItemDataBound
        Dim item As RepeaterItem = e.Item
        Dim AccRepeater As Repeater = DirectCast(e.Item.FindControl("rptOrderAccount"), Repeater)
        If Not item.DataItem Is Nothing Then
            Dim crt As B2P.Cart.Cart = DirectCast(item.DataItem, B2P.Cart.Cart)
            AccRepeater.DataSource = crt.AccountIdFields
            AccRepeater.DataBind()
        End If

    End Sub
End Class

