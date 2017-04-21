'==========================================================================
' File:     ManageCart.vb
' Date:     06.04.2017
' Author:   RS
'  
' Summary:  Manage Cart is used to manage shopping cart items 
'           
'
' TODO:     
'
'==========================================================================
Imports System.Collections.Generic
Imports System.Web
Imports B2P.Cart

Public Class ManageCart
    Dim minAmount As Double = 1
    Private Const _cart As String = "Cart"
    ''' <summary>
    ''' The ShoppingCart ptroperty will provide instance of Cart object
    ''' </summary>
    Public ReadOnly Property Cart() As List(Of Cart)
        Get
            If HttpContext.Current.Session(_cart) Is Nothing Then
                HttpContext.Current.Session(_cart) = New List(Of B2P.Cart.Cart)()
            End If

            Return HttpContext.Current.Session(_cart)
        End Get
    End Property
    ''' <summary>
    ''' Cart items can be added through AddToCart method
    ''' </summary>
    Public Function AddToCart(Item As String, Amount As Double,
                                                    Account1Label As String, Account1Value As String,
                                                    Account2Label As String, Account2Value As String,
                                                    Account3Label As String, Account3Value As String
                                                    ) As Boolean

        'Validate Cart
        If String.IsNullOrEmpty(Item) Then
            Return False
        End If
        If Amount < minAmount Then
            Return False
        End If
        If Account2Value Is Nothing Then
            Return False
        End If

        'Add to Cart
        Dim cartItem As New Cart
        cartItem.Index = IIf(Cart Is Nothing, 0, Cart.Count).ToString()
        cartItem.Amount = Amount

        cartItem.AccountIdFields = New List(Of AccountIdField)

        cartItem.AccountIdFields.Add(New AccountIdField(Account1Label, Account1Value))

        If String.IsNullOrEmpty(Account2Label) And String.IsNullOrEmpty(Account2Value) Then
            cartItem.AccountIdFields.Add(New AccountIdField(Account2Label, Account2Value))
        End If
        If String.IsNullOrEmpty(Account3Label) And String.IsNullOrEmpty(Account3Value) Then
            cartItem.AccountIdFields.Add(New AccountIdField(Account3Label, Account3Value))
        End If
        Return True
    End Function
    ''' <summary>
    ''' Cart items can be removed through RemoveFromCart method
    ''' </summary>
    Public Sub RemoveFromCart(Index As Integer)
        If Not Cart Is Nothing Then
            Cart.RemoveAt(Index)
        End If
    End Sub
    ''' <summary>
    ''' CartCount property will return number of items added in the cart
    ''' </summary>
    Public ReadOnly Property CartCount() As String
        Get
            If Cart Is Nothing Then
                Return String.Empty
            Else
                Return Cart.Count.ToString()
            End If
        End Get
    End Property

    Private _ShowCart As Boolean = False
    ''' <summary>
    ''' True when Cart Grid visible
    ''' </summary>
    ''' <returns></returns>
    Public Property ShowCart() As Boolean
        Get
            Return _ShowCart
        End Get
        Set(ByVal value As Boolean)
            _ShowCart = value
        End Set
    End Property

    Private _EditItemIndex As Integer = -1
    Public Property EditItemIndex() As Integer
        Get
            Return _EditItemIndex
        End Get
        Set(ByVal value As Integer)
            _EditItemIndex = value
        End Set
    End Property

    Public Function AddToCart(cartItem As Cart) As Boolean
        Dim IsExists As Boolean = False

        For Each c As Cart In Cart
            IsExists = True
            For Each entity As AccountIdField In c.AccountIdFields
                IsExists = cartItem.AccountIdFields _
                    .Exists(Function(p)
                                Return (p.Label.Equals(entity.Label, StringComparison.OrdinalIgnoreCase) _
                                                                       And p.Value.Equals(entity.Value, StringComparison.OrdinalIgnoreCase))
                            End Function)

                If Not IsExists Then
                    Exit For
                End If

            Next
            If IsExists Then
                Exit For
            End If
        Next

        If Not IsExists Then
            cartItem.Index = Cart.Count
            Cart.Add(cartItem)
        End If

        Return Not IsExists

    End Function

    Public Sub UpdateCartItem(selectedItem As Cart)
        Dim _SelectedItem = Cart.FirstOrDefault(Function(p) p.Index = selectedItem.Index)
        If Not _SelectedItem Is Nothing Then
            _SelectedItem.Amount = selectedItem.Amount
        End If
    End Sub
    Public Function SavePropertyAddress() As Boolean
        Try

        Catch ex As Exception

        End Try
    End Function
End Class


