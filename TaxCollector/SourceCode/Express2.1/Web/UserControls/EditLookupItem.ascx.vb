Imports System
Imports B2P.PaymentLanding.Express

Public Class EditLookupItem
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not _SelectedItem Is Nothing Then
                lblSelectedItemValue.Text = _SelectedItem.Item

                _SelectedItem.AccountIdFields.RemoveAll(Function(p) String.IsNullOrEmpty(p.Label))

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
            RedirectCart()
        End If
    End Sub

    Private Sub RedirectCart()
        BLL.SessionManager.ManageCart.ShowCart = True
        BLL.SessionManager.ManageCart.EditItemIndex = -1
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO Then
            Response.Redirect("~/sso/")
        Else
            Response.Redirect("~/pay/")
        End If
    End Sub

    Protected Sub btnUpdateItem_Click(sender As Object, e As EventArgs) Handles btnUpdateItem.Click
        UpdateCartItem()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        RedirectCart()
    End Sub

    Private _MinimumAmountRequired As Double = 0.0
    Public ReadOnly Property MinimumAmountRequired() As Double
        Get
            If Not BLL.SessionManager.CurrentCategory.PaymentInformation Is Nothing Then
                If Not BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard Is Nothing And Not BLL.SessionManager.CurrentCategory.PaymentInformation.ACH Is Nothing And BLL.SessionManager.CurrentCategory.PaymentInformation.CreditCardAccepted And BLL.SessionManager.CurrentCategory.PaymentInformation.ACHAccepted Then
                    _MinimumAmountRequired = IIf(BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MinimumAmount > BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MinimumAmount, BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MinimumAmount, BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MinimumAmount)
                ElseIf Not BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard Is Nothing And BLL.SessionManager.CurrentCategory.PaymentInformation.CreditCardAccepted Then
                    _MinimumAmountRequired = BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MinimumAmount
                ElseIf Not BLL.SessionManager.CurrentCategory.PaymentInformation.ACH Is Nothing And BLL.SessionManager.CurrentCategory.PaymentInformation.ACHAccepted Then
                    _MinimumAmountRequired = BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MinimumAmount
                End If
            End If

            Return _MinimumAmountRequired
        End Get
    End Property


    Private _MaximumAmountRequired As Double = 0.0
    Public ReadOnly Property MaximumAmountRequired() As Double
        Get
            If Not BLL.SessionManager.CurrentCategory.PaymentInformation Is Nothing Then
                If Not BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard Is Nothing And Not BLL.SessionManager.CurrentCategory.PaymentInformation.ACH Is Nothing And BLL.SessionManager.CurrentCategory.PaymentInformation.CreditCardAccepted And BLL.SessionManager.CurrentCategory.PaymentInformation.ACHAccepted Then
                    _MaximumAmountRequired = IIf(BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount < BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount, BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount, BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount)
                ElseIf Not BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard Is Nothing And BLL.SessionManager.CurrentCategory.PaymentInformation.CreditCardAccepted Then
                    _MaximumAmountRequired = BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount
                ElseIf Not BLL.SessionManager.CurrentCategory.PaymentInformation.ACH Is Nothing And BLL.SessionManager.CurrentCategory.PaymentInformation.ACHAccepted Then
                    _MaximumAmountRequired = BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount
                End If
            End If

            Return _MaximumAmountRequired
        End Get
    End Property
End Class