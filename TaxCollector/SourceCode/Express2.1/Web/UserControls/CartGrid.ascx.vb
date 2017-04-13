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
                PopulateGrid(Me.ID)
                hdMode.Value = ""
                hdSelectedIndex.Value = ""

            End If
        End If
        'Edit cart
        If hdMode.Value.ToUpper().Trim() = "EDIT" Then
            If BLL.SessionManager.ManageCart.Cart.Count > 0 Then
                Dim index As Integer = CType(hdSelectedIndex.Value, Integer)
                BLL.SessionManager.ManageCart.Cart(index).Amount = FormatAmount(CType(hdEditAmount.Value, Double))
                UpdateCartCount()
                PopulateGrid(Me.ID)
                hdMode.Value = ""
                hdSelectedIndex.Value = ""
            End If
        End If


    End Sub
    Public Sub PopulateGrid(ctrlName As String)
        Dim page As Page = HttpContext.Current.Handler
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup Then
            CType(page.FindControl(ctrlName), CartGrid).populateNonLookupGrid()
        End If
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.Lookup Then
            CType(page.FindControl(ctrlName), CartGrid).populateLookupGrid()
        End If
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO Then
            CType(page.FindControl(ctrlName), CartGrid).populateSSOGrid()
        End If
    End Sub
    Private Sub SetVisibilityOfGrid()
        rptNonLookup.Visible = BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup
        rptLookup.Visible = BLL.SessionManager.ClientType = B2P.Cart.EClientType.Lookup
        rptSSO.Visible = BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO
    End Sub
    Private Sub populateNonLookupGrid()
        ResetIndex()
        rptNonLookup.DataSource = BLL.SessionManager.ManageCart.Cart
        rptNonLookup.DataBind()
    End Sub
    Private Sub populateLookupGrid()
        ResetIndex()
        rptLookup.DataSource = BLL.SessionManager.ManageCart.Cart
        rptLookup.DataBind()
    End Sub
    Private Sub populateSSOGrid()
        ResetIndex()
        rptSSO.DataSource = BLL.SessionManager.ManageCart.Cart
        rptSSO.DataBind()
    End Sub
    Private Sub ResetIndex()
        For i As Integer = 0 To BLL.SessionManager.ManageCart.Cart.Count - 1
            BLL.SessionManager.ManageCart.Cart(i).Index = i.ToString()
        Next
    End Sub
    Protected Function GetPropertyAddress(Index As Integer) As String
        Dim CartItem As B2P.Cart.Cart = BLL.SessionManager.ManageCart.Cart(Index)
        Dim propertyAddress As StringBuilder = New StringBuilder()
        If Not CartItem.PropertyAddress Is Nothing Then
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Address1) Then
                propertyAddress.AppendFormat("{0},", CartItem.PropertyAddress.Address1)
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Address2) Then
                propertyAddress.AppendFormat("{0},", CartItem.PropertyAddress.Address2)
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.City) Then
                propertyAddress.AppendFormat("{0},", CartItem.PropertyAddress.City)
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.State) Then
                propertyAddress.AppendFormat("{0},", CartItem.PropertyAddress.State)
            End If
            If Not String.IsNullOrEmpty(CartItem.PropertyAddress.Zip) Then
                propertyAddress.AppendFormat("{0},", CartItem.PropertyAddress.Zip)
            End If
        End If
        Return propertyAddress.ToString().TrimEnd(",")
    End Function
    Protected Function GetAccountInformation(Index As Integer) As String
        Dim CartItem As B2P.Cart.Cart = BLL.SessionManager.ManageCart.Cart(Index)
        Dim accountInfo As StringBuilder = New StringBuilder()

        For Each fields In CartItem.AccountIdFields
            accountInfo.AppendFormat("{0},", fields.Value)
        Next

        Return accountInfo.ToString().TrimEnd(",")

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

    Protected Sub btnEditItem_Click(sender As Object, e As EventArgs) Handles btnEditItem.Click
        BLL.SessionManager.ManageCart.EditItemIndex = hdSelectedIndex.Value
        BLL.SessionManager.ManageCart.ShowCart = False
        Response.Redirect("~/pay/")
    End Sub
End Class