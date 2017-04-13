Imports System
Imports B2P.PaymentLanding.Express

Public Class EditLookupItem
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not _SelectedItem Is Nothing Then
                lblSelectedItemValue.Text = _SelectedItem.Item
                _SelectedItem.AccountIdFields.RemoveAll(Function(p) p.Label.Equals(String.Empty))

                ripAccountIds.DataSource = _SelectedItem.AccountIdFields
                ripAccountIds.DataBind()
                txtAmountEdit.Text = _SelectedItem.Amount
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

    Private Sub UpdateCartItem()
        _SelectedItem = BLL.SessionManager.ManageCart.Cart.FirstOrDefault(Function(p) p.Index = BLL.SessionManager.ManageCart.EditItemIndex)
        If Not _SelectedItem Is Nothing Then
            _SelectedItem.Amount = txtAmountEdit.Text.Trim()
            BLL.SessionManager.ManageCart.UpdateCartItem(_SelectedItem)
            BLL.SessionManager.ManageCart.ShowCart = True
            BLL.SessionManager.ManageCart.EditItemIndex = -1
            Response.Redirect("~/pay/")
        End If
    End Sub

    Protected Sub btnUpdateItem_Click(sender As Object, e As EventArgs) Handles btnUpdateItem.Click
        UpdateCartItem()
    End Sub
End Class