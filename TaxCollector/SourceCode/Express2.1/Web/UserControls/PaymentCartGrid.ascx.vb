﻿Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.Web
Imports System.Web.UI
Imports System
Imports B2P

Public Class PaymentCartGrid
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub PopulateGrid(ctrlName As String)
        Dim page As Page = HttpContext.Current.Handler
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup Then
            CType(page.FindControl(ctrlName), PaymentCartGrid).populateNonLookupGrid()
        End If
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.Lookup Then
            CType(page.FindControl(ctrlName), PaymentCartGrid).populateLookupGrid()
        End If
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO Then
            CType(page.FindControl(ctrlName), PaymentCartGrid).populateSSOGrid()
        End If
    End Sub
    Private Sub SetVisibilityOfGrid()
        rptNonLookup.Visible = BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup
        rptLookup.Visible = BLL.SessionManager.ClientType = B2P.Cart.EClientType.Lookup
        rptSSO.Visible = BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO
    End Sub
    Private Sub populateNonLookupGrid()
        rptNonLookup.DataSource = BLL.SessionManager.ManageCart.Cart
        rptNonLookup.DataBind()
    End Sub
    Private Sub populateLookupGrid()
        rptLookup.DataSource = BLL.SessionManager.ManageCart.Cart
        rptLookup.DataBind()
    End Sub
    Private Sub populateSSOGrid()
        rptSSO.DataSource = BLL.SessionManager.ManageCart.Cart
        rptSSO.DataBind()
    End Sub
    Protected Sub SetConvenienceFeesApplicability()
        Dim cf As B2P.Payment.FeeCalculation.CalculatedFee = Nothing
        Select Case BLL.SessionManager.PaymentType
            Case Common.Enumerations.PaymentTypes.BankAccount
                cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, SubTotal())
                BLL.SessionManager.ConvenienceFee = cf.ConvenienceFee
                BLL.SessionManager.TransactionFee = cf.TransactionFee
                pnlACHFee.Visible = True
                litACHFee.Text = BLL.SessionManager.AchFeeDescription
            Case Common.Enumerations.PaymentTypes.CreditCard
                Dim cardType As B2P.Payment.FeeCalculation.PaymentTypes = B2P.Payment.FeeCalculation.GetCardType(BLL.SessionManager.CreditCard.InternalCreditCardNumber)
                cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, cardType, SubTotal())
                BLL.SessionManager.ConvenienceFee = cf.ConvenienceFee
                BLL.SessionManager.TransactionFee = cf.TransactionFee
                pnlCCFee.Visible = True
                litCCFee.Text = BLL.SessionManager.CreditFeeDescription
        End Select
    End Sub
    Protected Function IsConvenienceFeesApplicable() As Boolean
        Dim cf As B2P.Payment.FeeCalculation.CalculatedFee = Nothing
        Select Case BLL.SessionManager.PaymentType
            Case Common.Enumerations.PaymentTypes.BankAccount
                cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, SubTotal())

            Case Common.Enumerations.PaymentTypes.CreditCard
                Dim cardType As B2P.Payment.FeeCalculation.PaymentTypes = B2P.Payment.FeeCalculation.GetCardType(BLL.SessionManager.CreditCard.InternalCreditCardNumber)
                cf = B2P.Payment.FeeCalculation.CalculateFee(BLL.SessionManager.ClientCode, BLL.SessionManager.CurrentCategory.Name, B2P.Common.Enumerations.TransactionSources.Web, cardType, SubTotal())

        End Select
        BLL.SessionManager.IsConvenienceFeesApplicable = BLL.SessionManager.Client.ConfirmFees And cf.ConvenienceFee > 0
        Return BLL.SessionManager.IsConvenienceFeesApplicable
    End Function
    Protected Function GetConvenienceFee() As String
        SetConvenienceFeesApplicability()
        If BLL.SessionManager.IsConvenienceFeesApplicable Then
            Return BLL.SessionManager.ConvenienceFee.ToString("C2")
        Else
            Return 0
        End If
    End Function
    Protected Function GetPropertyAddress(Index As Integer) As String
        Dim CartItem As B2P.Cart.Cart = BLL.SessionManager.ManageCart.Cart(Index)
        Dim propertyAddress As StringBuilder = New StringBuilder()
        If Not CartItem.PropertyAddress Is Nothing Then
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Address1) Then
                propertyAddress.AppendFormat("{0}, ", CartItem.PropertyAddress.Address1)
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Address2) Then
                propertyAddress.AppendFormat("{0}, ", CartItem.PropertyAddress.Address2)
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.City) Then
                propertyAddress.AppendFormat("{0}, ", CartItem.PropertyAddress.City)
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.State) Then
                propertyAddress.AppendFormat("{0}, ", CartItem.PropertyAddress.State)
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Zip) Then
                propertyAddress.AppendFormat("{0}, ", CartItem.PropertyAddress.Zip)
            End If
        End If
        Return IIf(String.IsNullOrEmpty(propertyAddress.ToString().Trim().TrimEnd(",")), "Not Available", propertyAddress.ToString().Trim().TrimEnd(","))
    End Function
    Protected Function GetAccountInformation(Index As Integer) As String
        Dim CartItem As B2P.Cart.Cart = BLL.SessionManager.ManageCart.Cart(Index)
        Dim accountInfo As StringBuilder = New StringBuilder()

        For Each fields In CartItem.AccountIdFields
            If Not String.IsNullOrEmpty(fields.Value) Then
                accountInfo.AppendFormat("{0}, ", fields.Value)
            End If
        Next
        Return accountInfo.ToString().Trim().TrimEnd(",")
    End Function
    Protected Function FormatAmount(Amount As Double) As String
        Return String.Format("{0:C}", Amount)
    End Function
    Protected Function GetCartItemCount() As Integer
        Return BLL.SessionManager.ManageCart.Cart.Count
    End Function
    Protected Function SubTotal() As String
        Dim amount As Double = 0
        For Each cart As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
            amount += cart.Amount
        Next
        Return String.Format("{0:C}", amount)
    End Function
    Protected Function Total() As String
        Dim totalAmount As Double = CType(SubTotal(), Double)
        If IsConvenienceFeesApplicable() Then
            totalAmount += CType(GetConvenienceFee(), Double)
        End If
        Return String.Format("{0:C}", totalAmount)
    End Function
End Class