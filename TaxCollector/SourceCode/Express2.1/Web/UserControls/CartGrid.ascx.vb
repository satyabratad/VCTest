Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.Web
Imports System.Web.UI
Public Class CartGridascx
    Inherits System.Web.UI.UserControl
    Public clientType As B2P.Cart.EClientType
    Public cartItems As List(Of B2P.Cart.Cart)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        'Delete from cart
        If hdMode.Value.ToUpper().Trim() = "DELETE" Then
            If BLL.SessionManager.ManageCart.Cart.Count > 0 Then
                Dim index As Integer = CType(hdSelectedIndex.Value, Integer)
                BLL.SessionManager.ManageCart.Cart.RemoveAt(index)
                UpdateCartCount()
            End If
        End If
        'Edit cart
        If hdMode.Value.ToUpper().Trim() = "EDIT" Then
            If BLL.SessionManager.ManageCart.Cart.Count > 0 Then
                Dim index As Integer = CType(hdSelectedIndex.Value, Integer)
                BLL.SessionManager.ManageCart.Cart(index).Amount = FormatAmount(CType(hdEditAmount.Value, Double))
                UpdateCartCount()
            End If
        End If

        clientType = BLL.SessionManager.ClientType
        If Not BLL.SessionManager.ManageCart Is Nothing Then
            cartItems = BLL.SessionManager.ManageCart.Cart
        End If
    End Sub
    Public Function GetPropertyAddress(CartItem As B2P.Cart.Cart) As String
        Dim propertyAddress As String = String.Empty
        If Not CartItem.PropertyAddress Is Nothing Then
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Address1) Then
                propertyAddress += CartItem.PropertyAddress.Address1 + ","
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Address2) Then
                propertyAddress += CartItem.PropertyAddress.Address2 + ","
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.City) Then
                propertyAddress += CartItem.PropertyAddress.City + ","
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.State) Then
                propertyAddress += CartItem.PropertyAddress.State + ","
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Zip) Then
                propertyAddress += CartItem.PropertyAddress.Zip + ","
            End If
            If Not String.IsNullOrEmpty(propertyAddress.Trim()) Then
                propertyAddress = propertyAddress.Substring(0, propertyAddress.Length - 1)
            End If
        End If
        Return propertyAddress
    End Function
    Public Function GetAccountInformation(CartItem As B2P.Cart.Cart) As String
        Dim accountInfo As String = String.Empty
        If Not String.IsNullOrEmpty(CartItem.AccountIdFields(0).Value) Then
            accountInfo += CartItem.AccountIdFields(0).Value + ","
        End If
        If Not String.IsNullOrEmpty(CartItem.AccountIdFields(1).Value) Then
            accountInfo += CartItem.AccountIdFields(1).Value + ","
        End If
        If Not String.IsNullOrEmpty(CartItem.AccountIdFields(0).Value) Then
            accountInfo += CartItem.AccountIdFields(1).Value + ","
        End If

        If Not String.IsNullOrEmpty(accountInfo.Trim()) Then
            accountInfo = accountInfo.Substring(0, accountInfo.Length - 1)
        End If

        Return accountInfo
    End Function
    Public Function FormatAmount(Amount As Double) As Double
        Return Format(Amount, "N2")
    End Function
    Public Function GetCartItemCount() As Integer
        Return cartItems.Count
    End Function
    Public Function SubTotal() As Double
        Dim amount As Double = 0
        For Each cart As B2P.Cart.Cart In cartItems
            amount += cart.Amount
        Next
        Return Format(amount, "N2")
    End Function
    Sub UpdateCartCount()
        Dim cs As ClientScriptManager = Page.ClientScript
        cs.RegisterStartupScript(Me.GetType(), "tmp", "<script type='text/javascript'>updateCartCount(" + BLL.SessionManager.ManageCart.Cart.Count.ToString() + ");</script>", False)
    End Sub
End Class