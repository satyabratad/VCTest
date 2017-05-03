Imports B2P.PaymentLanding.Express
Imports B2P.PaymentLanding.Express.Web
Imports System.Web.UI
Imports System
Imports B2P

Public Class CartGrid
    Inherits System.Web.UI.UserControl

#Region "::: Control Event Handlers :::"
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
                CType(Me.Parent.FindControl("CartDetails"), CartDetails).LoadContent()

                Utility.SetBreadCrumbContactInfo("BreadCrumbMenu")


                'End Show/Hide Contact Info
            End If
            If BLL.SessionManager.ManageCart.Cart.Count = 0 Then
                BLL.SessionManager.ManageCart.ShowCart = False
                BLL.SessionManager.ManageCart.EditItemIndex = -1
                If BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO Then
                    Response.Redirect("~/sso/")
                Else
                    Response.Redirect("~/pay/")
                End If
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
#End Region
#Region "::: Methods :::"
    Protected Function GetEditIconVisivility(Index As Integer) As String
        Return IIf(BLL.SessionManager.ManageCart.Cart(Index).PaymentInfo.IsEditIconVisible, "display:block;", "display:none;")
    End Function
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
                propertyAddress.AppendFormat("{0},", CartItem.PropertyAddress.Zip)
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
    Protected Function FormatCartItemCount() As String
        If BLL.SessionManager.ManageCart.Cart.Count <= 1 Then
            Return GetGlobalResourceObject("WebResources", "lblCartSubtotal").ToString() + " (" + GetCartItemCount().ToString() + " " + GetGlobalResourceObject("WebResources", "CartHeaderSingleItemCount").ToString() + "): "
        Else
            Return GetGlobalResourceObject("WebResources", "lblCartSubtotal").ToString() + " (" + GetCartItemCount().ToString() + " " + GetGlobalResourceObject("WebResources", "CartHeaderMultipleItemCount").ToString() + "): "
        End If
    End Function
    Protected Function SubTotal() As String
        Dim amount As Double = 0
        For Each cart As B2P.Cart.Cart In BLL.SessionManager.ManageCart.Cart
            amount += cart.Amount
        Next
        Return String.Format("{0:C}", amount)
    End Function
    Private Sub UpdateCartCount()
        Dim cs As ClientScriptManager = Page.ClientScript
        cs.RegisterStartupScript(Me.GetType(), "tmp", "<script type='text/javascript'>updateCartCount(" + BLL.SessionManager.ManageCart.Cart.Count.ToString() + ");</script>", False)
    End Sub

    Protected Sub btnEditItem_Click(sender As Object, e As EventArgs) Handles btnEditItem.Click
        BLL.SessionManager.ManageCart.EditItemIndex = hdSelectedIndex.Value
        BLL.SessionManager.ManageCart.ShowCart = False
        If BLL.SessionManager.ClientType = B2P.Cart.EClientType.SSO Then
            Response.Redirect("~/sso/")
        Else
            Response.Redirect("~/pay/")
        End If
    End Sub
    Protected Function GetCssClass(ItemIndex As String) As String
        Dim index As Integer = CType(ItemIndex, Integer)
        If index Mod 2 = 0 Then
            Return "table-row"
        Else
            Return "table-alternateRow"
        End If
    End Function
#End Region
End Class