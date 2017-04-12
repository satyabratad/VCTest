Public Class EditLookupItem
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not _SelectedItem Is Nothing Then
                lblSelectedItemValue.Text = _SelectedItem.Item
                ripAccountIds.DataSource = _SelectedItem.AccountIdFields
                ripAccountIds.DataBind()
                txtAmount.Text = _SelectedItem.Amount
            End If
        End If

    End Sub

    Private _SelectedItem As B2P.Cart.Cart

    Public Property SelectedItem() As B2P.Cart.Cart
        Get
            Return _SelectedItem
        End Get
        Set(ByVal value As B2P.Cart.Cart)
            _SelectedItem = value
        End Set
    End Property


End Class