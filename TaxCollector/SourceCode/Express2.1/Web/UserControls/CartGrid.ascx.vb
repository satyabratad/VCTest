Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.Web
Imports System.Web.UI
Imports System

Public Class CartGrid
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Delete from cart
        If hdMode.Value.ToUpper().Trim() = "DELETE" Then
            If BLL.SessionManager.ManageCart.Cart.Count > 0 Then
                Dim index As Integer = CType(hdSelectedIndex.Value, Integer)
                BLL.SessionManager.ManageCart.Cart.RemoveAt(index)
                UpdateCartCount()
                populateNonLookupGrid()
            End If
        End If
        'Edit cart
        If hdMode.Value.ToUpper().Trim() = "EDIT" Then
            If BLL.SessionManager.ManageCart.Cart.Count > 0 Then
                Dim index As Integer = CType(hdSelectedIndex.Value, Integer)
                BLL.SessionManager.ManageCart.Cart(index).Amount = FormatAmount(CType(hdEditAmount.Value, Double))
                UpdateCartCount()
                populateNonLookupGrid()
            End If
        End If


    End Sub
    Public Sub PopulateGrid(ctrlName As String)
        Dim page As Page = HttpContext.Current.Handler
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup Then
            CType(page.FindControl(ctrlName), CartGrid).populateNonLookupGrid()
        End If
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.Lookup Then
            CType(page.FindControl(ctrlName), CartGrid).populateNonLookupGrid()
        End If
        'If BLL.SessionManager.ClientCode = B2P.Cart.EClientType.SSO Then
        '    CType(page.FindControl("CartGrid"), CartGridascx).populateSSOGrid()
        'End If

    End Sub
    Private Sub SetVisibilityOfGrid()
        rptNonLookup.Visible = BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup
        rptLookup.Visible = BLL.SessionManager.ClientType = B2P.Cart.EClientType.Lookup
        'rptSSO.Visible = BLL.SessionManager.ClientCode = B2P.Cart.EClientType.SSO
    End Sub
    Protected Sub populateNonLookupGrid()
        rptNonLookup.DataSource = BLL.SessionManager.ManageCart.Cart
        rptNonLookup.DataBind()
    End Sub
    'Protected Sub populateLookupGrid()
    '    rptLookup.DataSource = BLL.SessionManager.ManageCart.Cart
    '    rptLookup.DataBind()
    'End Sub
    Protected Function GetPropertyAddress(Index As Integer) As String
        Dim CartItem As B2P.Cart.Cart = BLL.SessionManager.ManageCart.Cart(Index)
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
    Protected Function GetAccountInformation(Index As Integer) As String
        Dim CartItem As B2P.Cart.Cart = BLL.SessionManager.ManageCart.Cart(Index)
        Dim accountInfo As String = String.Empty
        If Not String.IsNullOrEmpty(CartItem.AccountIdFields(0).Value) Then
            accountInfo += CartItem.AccountIdFields(0).Value + ","
        End If
        If Not String.IsNullOrEmpty(CartItem.AccountIdFields(1).Value) Then
            accountInfo += CartItem.AccountIdFields(1).Value + ","
        End If
        If Not String.IsNullOrEmpty(CartItem.AccountIdFields(2).Value) Then
            accountInfo += CartItem.AccountIdFields(2).Value + ","
        End If

        If Not String.IsNullOrEmpty(accountInfo.Trim()) Then
            accountInfo = accountInfo.Substring(0, accountInfo.Length - 1)
        End If

        Return accountInfo
    End Function
    Protected Function FormatAmount(Amount As Double) As Double
        Return Math.Round(Amount, 2)
    End Function
    Protected Function GetCartItemCount() As Integer
        Return BLL.SessionManager.ManageCart.Cart.Count
    End Function
    Protected Function SubTotal() As Double
        Dim amount As Double = 0
        For Each cart As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
            amount += cart.Amount
        Next
        Return Math.Round(amount, 2)
    End Function
    Private Sub UpdateCartCount()
        Dim cs As ClientScriptManager = Page.ClientScript
        cs.RegisterStartupScript(Me.GetType(), "tmp", "<script type='text/javascript'>updateCartCount(" + BLL.SessionManager.ManageCart.Cart.Count.ToString() + ");</script>", False)
    End Sub
End Class