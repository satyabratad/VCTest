Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.Web

''' <summary>
''' This will calculate and display the sub total of item/service in shopping cart
''' </summary>
Public Class SubTotal
    Inherits System.Web.UI.UserControl
#Region "::: Properties :::"
    ''' <summary>
    ''' This property used to hold subtotal
    ''' </summary>
    Private _subTotalAmount As String
    Public Property SubTotalAmount() As String
        Get
            Return _subTotalAmount
        End Get
        Set(ByVal value As String)
            _subTotalAmount = value
        End Set
    End Property
#End Region
#Region "::: Control Event Handlers :::"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            lblSubTotal.Text = CalculateSubTotal()
        End If
    End Sub
#End Region
#Region "::: Methods :::"
    ''' <summary>
    ''' This will calculate and display the sub total of item/service in shopping cart
    ''' </summary>
    ''' <returns></returns>
    Protected Function CalculateSubTotal() As String
        Dim total As Double = 0
        If Not BLL.SessionManager.ManageCart.Cart Is Nothing Then
            For Each item As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
                total += item.Amount
            Next
        End If
        Return String.Format("{0:C}", total)
    End Function
#End Region
End Class