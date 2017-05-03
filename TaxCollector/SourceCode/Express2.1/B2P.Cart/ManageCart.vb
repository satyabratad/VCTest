'==========================================================================
' File:     ManageCart.vb
' Date:     06.04.2017
' Author:   RS
'  
' Summary:  Manage Cart is used to manage shopping cart items 
'
'==========================================================================
Imports System.Collections.Generic
Imports System.Configuration
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
    ''' <summary>
    ''' Index stored for edit items
    ''' </summary>
    Private _EditItemIndex As Integer = -1
    Public Property EditItemIndex() As Integer
        Get
            Return _EditItemIndex
        End Get
        Set(ByVal value As Integer)
            _EditItemIndex = value
        End Set
    End Property
    ''' <summary>
    ''' Add to cart
    ''' </summary>
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
    ''' <summary>
    ''' Update cart
    ''' </summary>
    Public Sub UpdateCartItem(selectedItem As Cart)
        Dim _SelectedItem = Cart.FirstOrDefault(Function(p) p.Index = selectedItem.Index)
        If Not _SelectedItem Is Nothing Then
            _SelectedItem.Amount = selectedItem.Amount
        End If
    End Sub
    ''' <summary>
    ''' Save property address
    ''' </summary>
    Public Sub SavePropertyAddress(ClientCode As String, BatchId As String)
        For Each cartItem As Cart In Me.Cart
            If cartItem.CollectPropertyAddress Then
                SavePropertyAddress(cartItem, ClientCode, BatchId)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Save property address
    ''' </summary>
    Private Sub SavePropertyAddress(cartItem As Cart, ClientCode As String, BatchId As String)
        Try
            Dim timeUtc = DateTime.UtcNow
            Dim easternZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")
            Dim easternTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone)

            Dim connectionString As String = ConfigurationManager.AppSettings("ConnectionString")
            Using sqlConnection As New SqlClient.SqlConnection(connectionString)
                Using sqlCommand As New SqlClient.SqlCommand
                    sqlCommand.CommandText = "ap_InsertPropertyAddress"
                    sqlCommand.CommandType = CommandType.StoredProcedure
                    sqlCommand.Parameters.Add("@BatchID", SqlDbType.VarChar, 40).Value = BatchId
                    sqlCommand.Parameters.Add("@ClientCode", SqlDbType.VarChar, 10).Value = ClientCode
                    sqlCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 40).Value = cartItem.Item
                    sqlCommand.Parameters.Add("@AccountNumber1", SqlDbType.VarChar, 40).Value = GetAccountFieldValue(cartItem, 0)
                    sqlCommand.Parameters.Add("@AccountNumber2", SqlDbType.VarChar, 40).Value = GetAccountFieldValue(cartItem, 1)
                    sqlCommand.Parameters.Add("@AccountNumber3", SqlDbType.VarChar, 40).Value = GetAccountFieldValue(cartItem, 2)
                    sqlCommand.Parameters.Add("@Address1", SqlDbType.VarChar, 40).Value = cartItem.PropertyAddress.Address1
                    sqlCommand.Parameters.Add("@Address2", SqlDbType.VarChar, 40).Value = cartItem.PropertyAddress.Address2
                    sqlCommand.Parameters.Add("@State", SqlDbType.VarChar, 40).Value = cartItem.PropertyAddress.State
                    sqlCommand.Parameters.Add("@City", SqlDbType.VarChar, 40).Value = cartItem.PropertyAddress.City
                    sqlCommand.Parameters.Add("@ZipCode", SqlDbType.VarChar, 40).Value = cartItem.PropertyAddress.Zip
                    sqlCommand.Parameters.Add("@CreateDateEST", SqlDbType.DateTime, 8).Value = easternTime

                    sqlCommand.Connection = sqlConnection
                    sqlConnection.Open()
                    sqlCommand.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Get account field vaule index wise
    ''' </summary>
    Public Function GetAccountFieldValue(CartItem As Cart, Index As Integer) As String
        Dim maxItemCount As Integer = CartItem.AccountIdFields.Count
        If Index < maxItemCount Then
            Return CartItem.AccountIdFields(Index).Value
        End If
        Return Nothing
    End Function
    ''' <summary>
    ''' IsPaymentForCreditCardVisible - visibility of credit card tab
    ''' </summary>
    Public Function IsPaymentForCreditCardVisible() As Boolean
        For Each cart As Cart In Me.Cart
            If Not cart.PaymentInfo Is Nothing Then
                If cart.PaymentInfo.AllowCreditCardPayment And cart.PaymentInfo.CreditCardAccepted And Not cart.PaymentInfo.BlockedCC Then
                    Return True
                End If
            End If
        Next
        'commented for now
        'Return False
        Return True
    End Function
    ''' <summary>
    ''' IsPaymentForBankVisible - visibility of bank tab
    ''' </summary>
    Public Function IsPaymentForBankVisible() As Boolean
        For Each cart As Cart In Me.Cart
            If Not cart.PaymentInfo Is Nothing Then
                If cart.PaymentInfo.AllowECheckPayment And cart.PaymentInfo.ACHAccepted And Not cart.PaymentInfo.BlockedACH Then
                    Return True
                End If
            End If
        Next
        'commented for now
        'Return False
        Return True
    End Function
End Class


