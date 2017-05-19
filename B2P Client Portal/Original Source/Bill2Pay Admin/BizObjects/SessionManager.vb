 '==========================================================================
' File:     SessionManager.vb
' Date:     07.21.2015
' Author:   Scott Leonard
'  
' Summary:  Provides a wrapper to the ASP.NET Session object. All access 
'           to Session variables should go through this class.
'
' TODO:     
'
'==========================================================================
Imports System
Imports System.Web

Namespace B2P.Integration.TriPOS.BLL

    ''' <summary>
    ''' Provides a wrapper to the ASP.NET Session object.
    ''' </summary>
    ''' <remarks>All access to Session variables should go through this class.</remarks>
    Public NotInheritable Class SessionManager

#Region " ::: Private Constants ::: "

        Private Const _bankAccount As String = "BankAccount"
        Private Const _cart As String = "Cart"
        Private Const _clerkNumber As String = "ClerkNumber"
        Private Const _clientCode As String = "ClientCode"
        Private Const _clientEmail As String = "ClientEmail"
        Private Const _clientName As String = "ClientName"
        Private Const _clientPhone As String = "ClientPhone"
        Private Const _customerEmail As String = "CustomerEmail"
        Private Const _userComments As String = "UserComments"
        Private Const _confirmNumber As String = "ConfirmationNumber"
        Private Const _officeID As String = "OfficeID"
        Private Const _creditCard As String = "CreditCard"
        Private Const _isInitialized As String = "IsInitialized"
        Private Const _laneId As String = "LaneID"
        Private Const _originatorID As String = "OriginatorID"
        Private Const _paymentMade As String = "PaymentMade"
        Private Const _paymentType As String = "PaymentType"
        Private Const _paymentTypeSelected As String = "PaymentTypeSelected"
        Private Const _refNumber As String = "ReferenceNumber"
        Private Const _transReceipt As String = "TransactionReceipt"
        Private Const _useSingleFee As String = "UseSingleFee"
        Private Const _token As String = "Token"
        Private Const _transInfo As String = "TransactionInformation"
        Private Const _vendorReferenceCode As String = "VendorReferenceCode"
        Private Const _securityID As String = "SecurityID"
        Private Const _creditCardBlocked As String = "CreditCardBlocked"
        Private Const _ACHBlocked As String = "ACHBlocked"
        Private Const _useBinLookup As String = "UseBinLookup"

#End Region

#Region " ::: Constructors ::: "

        ''' <summary>
        ''' Private constructor prevents the class from being created.
        ''' </summary>
        ''' <remarks>
        ''' Instances of types that define only static members do not need to be created.
        ''' Many compilers will automatically add a public default constructor if no constructor 
        ''' is specified. To prevent this, adding an empty private constructor may be required.
        ''' If public members are added to this class, the "Private" modified needs to be changed
        ''' to "Public".
        ''' </remarks>
        Private Sub New()
        End Sub

#End Region

#Region " ::: Properties ::: "



        ''' <summary>
        ''' A bank account associated with a transaction.
        ''' </summary>
        Public Shared Property BankAccount() As B2P.Common.Objects.BankAccount
            Get
                If HttpContext.Current.Session(_bankAccount) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_bankAccount), B2P.Common.Objects.BankAccount)
                End If
            End Get
            Set(value As B2P.Common.Objects.BankAccount)
                HttpContext.Current.Session(_bankAccount) = value
            End Set
        End Property

        ''' <summary>
        ''' A shopping cart associated with a transaction.
        ''' </summary>
        Public Shared Property Cart() As B2P.ShoppingCart.Cart
            Get
                If HttpContext.Current.Session(_cart) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_cart), B2P.ShoppingCart.Cart)
                End If
            End Get
            Set(value As B2P.ShoppingCart.Cart)
                HttpContext.Current.Session(_cart) = value
            End Set
        End Property

        ''' <summary>
        ''' The clerk number.
        ''' </summary>
        ''' <remarks>
        ''' Concatenating the Office_ID and the TriPOS lane ID on the PC.
        ''' </remarks>
        Public Shared Property ClerkNumber() As String
            Get
                If HttpContext.Current.Session(_clerkNumber) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_clerkNumber), String)
                End If
            End Get
            Set(value As String)
                HttpContext.Current.Session(_clerkNumber) = value
            End Set
        End Property

        ''' <summary>
        ''' The client code associated with a transaction.
        ''' </summary>
        Public Shared Property ClientCode() As String
            Get
                If HttpContext.Current.Session(_clientCode) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_clientCode), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_clientCode) = value
            End Set
        End Property

        ''' <summary>
        ''' The client e-mail associated with a transaction.
        ''' </summary>
        Public Shared Property ClientEmail() As String
            Get
                If HttpContext.Current.Session(_clientEmail) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_clientEmail), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_clientEmail) = value
            End Set
        End Property

        ''' <summary>
        ''' The client name associated with a transaction.
        ''' </summary>
        Public Shared Property ClientName() As String
            Get
                If HttpContext.Current.Session(_clientName) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_clientName), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_clientName) = value
            End Set
        End Property

        ''' <summary>
        ''' The client phone associated with a transaction.
        ''' </summary>
        Public Shared Property ClientPhone() As String
            Get
                If HttpContext.Current.Session(_clientPhone) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_clientPhone), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_clientPhone) = value
            End Set
        End Property

        ''' <summary>
        ''' The confirmation number associated with a transaction's successful payment.
        ''' </summary>
        Public Shared Property ConfirmationNumber() As String
            Get
                If HttpContext.Current.Session(_confirmNumber) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_confirmNumber), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_confirmNumber) = value
            End Set
        End Property
        Private Const _currentlyProcessingTrans As String = "CurrentlyProcessingTransaction"

        ''' <summary>
        ''' Determines whether a transaction is currently being processed.
        ''' </summary>
        Public Shared Property CurrentlyProcessingTransaction() As Boolean
            Get
                If HttpContext.Current.Session(_currentlyProcessingTrans) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_currentlyProcessingTrans), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_currentlyProcessingTrans) = value
            End Set
        End Property


        ''' <summary>
        ''' The office ID associated with a transaction.
        ''' </summary>
        Public Shared Property OfficeID() As String
            Get
                If HttpContext.Current.Session(_officeID) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_officeID), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_officeID) = value
            End Set
        End Property

        ''' <summary>
        ''' The email address associated with the customer.
        ''' </summary>
        Public Shared Property CustomerEmail() As String
            Get
                If HttpContext.Current.Session(_customerEmail) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_customerEmail), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_customerEmail) = value
            End Set
        End Property


        ''' <summary>
        ''' The user comments associated with the transaction.
        ''' </summary>
        Public Shared Property UserComments() As String
            Get
                If HttpContext.Current.Session(_userComments) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_userComments), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_userComments) = value
            End Set
        End Property

        ''' <summary>
        ''' A credit card associated with a transaction.
        ''' </summary>
        Public Shared Property CreditCard() As B2P.Common.Objects.CreditCard
            Get
                If HttpContext.Current.Session(_creditCard) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_creditCard), B2P.Common.Objects.CreditCard)
                End If
            End Get
            Set(value As B2P.Common.Objects.CreditCard)
                HttpContext.Current.Session(_creditCard) = value
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the session has been initialized.
        ''' </summary>
        Public Shared Property IsInitialized() As Boolean
            Get
                If HttpContext.Current.Session(_isInitialized) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_isInitialized), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_isInitialized) = value
            End Set
        End Property

        ''' <summary>
        ''' The lane ID for a TriPOS transaction.
        ''' </summary>
        ''' <remarks>
        ''' This is a unique value for each PC at a given client location. Values must be >= 1.
        ''' </remarks>
        Public Shared Property LaneID() As Int32
            Get
                If HttpContext.Current.Session(_laneId) Is Nothing Then
                    Return -1
                Else
                    Return DirectCast(HttpContext.Current.Session(_laneId), Int32)
                End If
            End Get
            Set(ByVal value As Int32)
                HttpContext.Current.Session(_laneId) = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' A client office associated with a transaction.
        ' ''' </summary>
        'Public Shared Property Office() As B2P.Client.Office
        '    Get
        '        If HttpContext.Current.Session(_office) Is Nothing Then
        '            Return Nothing
        '        Else
        '            Return DirectCast(HttpContext.Current.Session(_office), B2P.Client.Office)
        '        End If
        '    End Get
        '    Set(value As B2P.Client.Office)
        '        HttpContext.Current.Session(_office) = value
        '    End Set
        'End Property

        ' ''' <summary>
        ' ''' The client associated with a transaction.
        ' ''' </summary>
        Public Shared Property OriginatorID() As String
            Get
                If HttpContext.Current.Session(_originatorID) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_originatorID), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_originatorID) = value
            End Set
        End Property

        ''' <summary>
        ''' Determines whether a transaction payment has been made.
        ''' </summary>
        Public Shared Property PaymentMade() As Boolean
            Get
                If HttpContext.Current.Session(_paymentMade) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_paymentMade), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_paymentMade) = value
            End Set
        End Property

        ''' <summary>
        ''' The payment type selected for a transaction.
        ''' </summary>
        Public Shared Property PaymentType() As B2P.Common.Enumerations.PaymentTypes
            Get
                If HttpContext.Current.Session(_paymentType) Is Nothing Then
                    ' Ideally there would be an Enum field of NotSet or None with a value of 0 or -1
                    ' By returning Nothing, this will end up defaulting to 0 (Credit Card)
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_paymentType), B2P.Common.Enumerations.PaymentTypes)
                End If
            End Get
            Set(ByVal value As B2P.Common.Enumerations.PaymentTypes)
                HttpContext.Current.Session(_paymentType) = value
            End Set
        End Property

        ''' <summary>
        ''' Determines if credit card is blocked from payment
        ''' </summary>
        Public Shared Property CreditCardBlocked() As Boolean
            Get
                If HttpContext.Current.Session(_creditCardBlocked) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_creditCardBlocked), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_creditCardBlocked) = value
            End Set
        End Property

        ''' <summary>
        ''' Determines if ACHis blocked from payment
        ''' </summary>
        Public Shared Property ACHBlocked() As Boolean
            Get
                If HttpContext.Current.Session(_ACHBlocked) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_ACHBlocked), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_ACHBlocked) = value
            End Set
        End Property

        ''' <summary>
        ''' The payment type selected by the clerk for a transaction.
        ''' </summary>
        Public Shared Property PaymentTypeSelected() As String
            Get
                If HttpContext.Current.Session(_paymentTypeSelected) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_paymentTypeSelected), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_paymentTypeSelected) = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' The product name associated with a transaction.
        ' ''' </summary>
        'Public Shared Property ProductName() As String
        '    Get
        '        If HttpContext.Current.Session(_productName) Is Nothing Then
        '            Return String.Empty
        '        Else
        '            Return DirectCast(HttpContext.Current.Session(_productName), String)
        '        End If
        '    End Get
        '    Set(ByVal value As String)
        '        HttpContext.Current.Session(_productName) = value
        '    End Set
        'End Property

        ''' <summary>
        ''' The reference number associated with a TriPOS transaction.
        ''' </summary>
        ''' <remarks>
        ''' Using a B2P batch ID.
        ''' </remarks>
        Public Shared Property ReferenceNumber() As String
            Get
                If HttpContext.Current.Session(_refNumber) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_refNumber), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_refNumber) = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' The payment processing result message.
        ' ''' </summary>
        'Public Shared Property ResultsMessage() As String
        '    Get
        '        If HttpContext.Current.Session(_resultsMessage) Is Nothing Then
        '            Return String.Empty
        '        Else
        '            Return DirectCast(HttpContext.Current.Session(_resultsMessage), String)
        '        End If
        '    End Get
        '    Set(ByVal value As String)
        '        HttpContext.Current.Session(_resultsMessage) = value
        '    End Set
        'End Property

        ''' <summary>
        ''' An SSO token associated with a transaction.
        ''' </summary>
        Public Shared Property Token() As String
            Get
                If HttpContext.Current.Session(_token) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_token), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_token) = value
            End Set
        End Property

        ''' <summary>
        ''' The transaction information associated with a SSO token.
        ''' </summary>
        Public Shared Property TransactionInformation() As B2P.SSOLookup.PaymentInformation
            Get
                If HttpContext.Current.Session(_transInfo) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_transInfo), B2P.SSOLookup.PaymentInformation)
                End If
            End Get
            Set(value As B2P.SSOLookup.PaymentInformation)
                HttpContext.Current.Session(_transInfo) = value
            End Set
        End Property

        ''' <summary>
        ''' A receipt associated with an approved transaction.
        ''' </summary>
        Public Shared Property TransactionReceipt() As Entities.Receipt
            Get
                If HttpContext.Current.Session(_transReceipt) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_transReceipt), Entities.Receipt)
                End If
            End Get
            Set(value As Entities.Receipt)
                HttpContext.Current.Session(_transReceipt) = value
            End Set
        End Property
        ''' <summary>
        ''' Whether the client uses a single fee for different types of
        ''' transactions (i.e., Credit, PIN Debit, etc.).
        ''' </summary>
        Public Shared Property UseSingleFee() As Boolean
            Get
                If HttpContext.Current.Session(_useSingleFee) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_useSingleFee), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_useSingleFee) = value
            End Set
        End Property
        ''' <summary>
        ''' The vendor reference info associated with a transaction.
        ''' </summary>
        Public Shared Property VendorReferenceCode() As String
            Get
                If HttpContext.Current.Session(_vendorReferenceCode) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_vendorReferenceCode), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_vendorReferenceCode) = value
            End Set
        End Property


        ''' <summary>
        ''' The security ID associated with the admin user.
        ''' </summary>
        Public Shared Property SecurityID() As String
            Get
                If HttpContext.Current.Session(_securityID) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_securityID), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_securityID) = value
            End Set
        End Property
        ''' <summary>
        ''' Whether the client uses a BIN lookup to determine the card type used.
        ''' </summary>
        Public Shared Property UseBinLookup() As Boolean
            Get
                If HttpContext.Current.Session(_useBinLookup) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_useBinLookup), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_useBinLookup) = value
            End Set
        End Property


#End Region

#Region " ::: Methods ::: "

        ''' <summary>
        ''' Clears the user's current session objects. 
        ''' </summary>
        Public Shared Sub Clear()
            HttpContext.Current.Session.Clear()
            HttpContext.Current.Session.Abandon()
        End Sub

#End Region

    End Class
End Namespace