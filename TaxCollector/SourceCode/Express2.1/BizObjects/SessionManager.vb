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
Imports B2P.ClientInterface.Manager.ClientInterfaceWS
Imports B2P.Common
Imports B2P.Common.Objects
Imports B2P.Objects
Imports B2P.ShoppingCart
Imports B2P.SSOLookup
Imports System
Imports System.Collections.Generic
Imports System.Web


Namespace B2P.PaymentLanding.Express.BLL

    ''' <summary>
    ''' Provides a wrapper to the ASP.NET Session object.
    ''' </summary>
    ''' <remarks>All access to Session variables should go through this class.</remarks>
    Public NotInheritable Class SessionManager

#Region " ::: Private Constants ::: "

        Private Const _accountNumber1 As String = "AccountNumber1"
        Private Const _accountNumber2 As String = "AccountNumber2"
        Private Const _accountNumber3 As String = "AccountNumber3"
        Private Const _achFeeDesc As String = "ACHFeeDescription"
        Private Const _amountEditable As String = "AmountEditable"
        Private Const _bankAccount As String = "BankAccount"
        Private Const _blockedACH As String = "BlockedACH"
        Private Const _blockedCC As String = "BlockedCC"
        Private Const _cart As String = "Cart"
        Private Const _cartFeeAch As String = "CartFeeACH"
        Private Const _cartFeeCredit As String = "CartFeeCredit"
        Private Const _categoryList As String = "CategoryList"
        Private Const _client As String = "Client"
        Private Const _clientCode As String = "ClientCode"
        Private Const _clientCSS As String = "ClientCSS"
        Private Const _clientFAQ As String = "ClientFAQ"
        Private Const _confirmNumber As String = "ConfirmationNumber"
        Private Const _convenienceFee As String = "ConvenienceFee"
        Private Const _creditCard As String = "CreditCard"
        Private Const _creditFeeDesc As String = "CreditFeeDescription"
        Private Const _currentCategory As String = "CurrentCategory"
        Private Const _isInitialized As String = "IsInitialized"
        Private Const _isSSOProduct As String = "IsSSOProduct"
        Private Const _lookupAmount As String = "LookupAmount"
        Private Const _lookupAmountMinimum As String = "LookupAmountMinimum"
        Private Const _lookupAmountEditable As String = "LookupAmountEditable"
        Private Const _lookupData As String = "LookupData"
        Private Const _lookupProduct As String = "LookupProduct"
        Private Const _nameOnLookupAccount As String = "NameOnLookupAccount"
        Private Const _officeID As String = "OfficeID"
        Private Const _originatorID As String = "OriginatorID"
        Private Const _paymentAmount As String = "PaymentAmount"
        Private Const _paymentDate As String = "PaymentDate"
        Private Const _paymentMade As String = "PaymentMade"
        Private Const _paymentType As String = "PaymentType"
        Private Const _postBackMessage As String = "PostBackMessage"
        Private Const _productName As String = "ProductName"
        Private Const _serviceAddress As String = "ServiceAddress"
        Private Const _sSODisplayType As String = "SSODisplayType"
        Private Const _token As String = "Token"
        Private Const _tokenInfo As String = "TokenInfo"
        Private Const _transactionFee As String = "TransactionFee"
        Private Const _transInfo As String = "TransactionInformation"
        Private Const _vendorReferenceCode As String = "VendorReferenceCode"
        'Added By RS
        Private Const _manageCart As String = "ManageCart"
        Private Const _clientType As String = "ClientType"
        Private Const _breadCrumbMenuTab As Object = "BreadCrumbMenuTab"
        Private Const _contactInfo As Object = "ContactInfo"
        Private Const _isContactInfoRequired As String = "IsContactInfoRequired"
        Private Const _isConvenienceFeesApplicable As String = "_isConvenienceFeesApplicable"
#End Region

#Region " ::: Contructors ::: "

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
        ''' The account number 1 associated with a transaction.
        ''' </summary>
        Public Shared Property AccountNumber1() As String
            Get
                If HttpContext.Current.Session(_accountNumber1) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_accountNumber1), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_accountNumber1) = value
            End Set
        End Property
        ''' <summary>
        ''' The account number 2 associated with a transaction.
        ''' </summary>
        Public Shared Property AccountNumber2() As String
            Get
                If HttpContext.Current.Session(_accountNumber2) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_accountNumber2), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_accountNumber2) = value
            End Set
        End Property

        ''' <summary>
        ''' The account number 3 associated with a transaction.
        ''' </summary>
        Public Shared Property AccountNumber3() As String
            Get
                If HttpContext.Current.Session(_accountNumber3) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_accountNumber3), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_accountNumber3) = value
            End Set
        End Property

        ''' <summary>
        ''' The ACH cart fee description associated with a transaction.
        ''' </summary>
        Public Shared Property AchFeeDescription() As String
            Get
                If HttpContext.Current.Session(_achFeeDesc) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_achFeeDesc), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_achFeeDesc) = value
            End Set
        End Property

        ''' <summary>
        ''' Is the payment amount editable?
        ''' </summary>
        Public Shared Property AmountEditable() As String
            Get
                If HttpContext.Current.Session(_amountEditable) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_amountEditable), String)
                End If
                Return AmountEditable
            End Get
            Set(value As String)
                HttpContext.Current.Session(_amountEditable) = value
            End Set
        End Property

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
        '''Is ACH blocked?
        ''' </summary>
        Public Shared Property BlockedACH() As Boolean
            Get
                If HttpContext.Current.Session(_blockedACH) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_blockedACH), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_blockedACH) = value
            End Set
        End Property

        ''' <summary>
        '''Is CC blocked?
        ''' </summary>
        Public Shared Property BlockedCC() As Boolean
            Get
                If HttpContext.Current.Session(_blockedCC) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_blockedCC), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_blockedCC) = value
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
        ''' The ACH cart fee associated with a transaction.
        ''' </summary>
        Public Shared Property CartFeeAch() As Decimal
            Get
                If HttpContext.Current.Session(_cartFeeAch) Is Nothing Then
                    Return Convert.ToDecimal(-1.0)
                Else
                    Return DirectCast(HttpContext.Current.Session(_cartFeeAch), Decimal)
                End If
            End Get
            Set(value As Decimal)
                HttpContext.Current.Session(_cartFeeAch) = value
            End Set
        End Property

        'Public Shared Property BreadCrumbMenuList() As(Of )


        ''' <summary>
        ''' The product list for a transaction.
        ''' </summary>
        Public Shared Property CategoryList() As List(Of String)
            Get
                If HttpContext.Current.Session(_categoryList) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_categoryList), List(Of String))
                End If
            End Get
            Set(ByVal value As List(Of String))
                HttpContext.Current.Session(_categoryList) = value
            End Set
        End Property
        ''' <summary>
        ''' The payment type selected for a transaction.
        ''' </summary>
        Public Shared Property Client() As B2P.Objects.Client
            Get
                If HttpContext.Current.Session(_client) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_client), B2P.Objects.Client)
                End If
            End Get
            Set(ByVal value As B2P.Objects.Client)
                HttpContext.Current.Session(_client) = value
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
        ''' The client css associated with a transaction.
        ''' </summary>
        Public Shared Property ClientCSS() As String
            Get
                If HttpContext.Current.Session(_clientCSS) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_clientCSS), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_clientCSS) = value
            End Set
        End Property

        ''' <summary>
        ''' The client css associated with a transaction.
        ''' </summary>
        Public Shared Property ClientFAQ() As String
            Get
                If HttpContext.Current.Session(_clientFAQ) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_clientFAQ), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_clientFAQ) = value
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

        ''' <summary>
        ''' The convenience fee associated with a transaction.
        ''' </summary>
        Public Shared Property ConvenienceFee() As Decimal
            Get
                If HttpContext.Current.Session(_convenienceFee) Is Nothing Then
                    Return Convert.ToDecimal(-1.0)
                Else
                    Return DirectCast(HttpContext.Current.Session(_convenienceFee), Decimal)
                End If
            End Get
            Set(value As Decimal)
                HttpContext.Current.Session(_convenienceFee) = value
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
        ''' The credit card cart fee description associated with a transaction.
        ''' </summary>
        Public Shared Property CreditFeeDescription() As String
            Get
                If HttpContext.Current.Session(_creditFeeDesc) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_creditFeeDesc), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_creditFeeDesc) = value
            End Set
        End Property

        ''' <summary>
        ''' The product associated with a transaction.
        ''' </summary>
        Public Shared Property CurrentCategory() As B2P.Objects.Product
            Get
                If HttpContext.Current.Session(_currentCategory) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_currentCategory), B2P.Objects.Product)
                End If
            End Get
            Set(value As B2P.Objects.Product)
                HttpContext.Current.Session(_currentCategory) = value
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
        ''' Determines whether the session is SSO session.
        ''' </summary>
        Public Shared Property IsSSOProduct() As Boolean
            Get
                If HttpContext.Current.Session(_isSSOProduct) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_isSSOProduct), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_isSSOProduct) = value
            End Set
        End Property

        ''' <summary>
        ''' The lookup amount associated with a transaction.
        ''' </summary>
        Public Shared Property LookupAmount() As Decimal
            Get
                If HttpContext.Current.Session(_lookupAmount) Is Nothing Then
                    Return Convert.ToDecimal(-1.0)
                Else
                    Return DirectCast(HttpContext.Current.Session(_lookupAmount), Decimal)
                End If
            End Get
            Set(value As Decimal)
                HttpContext.Current.Session(_lookupAmount) = value
            End Set
        End Property

        ''' <summary>
        ''' Is the lookup amount editable?
        ''' </summary>
        Public Shared Property LookupAmountEditable() As Boolean
            Get
                If HttpContext.Current.Session(_lookupAmountEditable) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_lookupAmountEditable), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_lookupAmountEditable) = value
            End Set
        End Property

        ''' <summary>
        ''' The lookup amount minimum associated with a transaction.
        ''' </summary>
        Public Shared Property LookupAmountMinimum() As Boolean
            Get
                If HttpContext.Current.Session(_lookupAmountMinimum) Is Nothing Then
                    Return False
                Else
                    Return DirectCast(HttpContext.Current.Session(_lookupAmountMinimum), Boolean)
                End If
            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_lookupAmountMinimum) = value
            End Set
        End Property

        ''' <summary>
        ''' The lookup data associated with a transaction..
        ''' </summary>
        Public Shared Property LookupData() As ClientInterface.Manager.ClientInterfaceWS.SearchResults
            Get
                If HttpContext.Current.Session(_lookupData) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_lookupData), ClientInterface.Manager.ClientInterfaceWS.SearchResults)
                End If
            End Get
            Set(value As ClientInterface.Manager.ClientInterfaceWS.SearchResults)
                HttpContext.Current.Session(_lookupData) = value
            End Set
        End Property
        ''' <summary>
        ''' The lookup product associated with a transaction.
        ''' </summary>
        Public Shared Property LookupProduct() As String
            Get
                If HttpContext.Current.Session(_lookupProduct) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_lookupProduct), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_lookupProduct) = value
            End Set
        End Property
        ''' <summary>
        ''' The name on the lookup account associated with a transaction.
        ''' </summary>
        Public Shared Property NameOnLookupAccount() As String
            Get
                If HttpContext.Current.Session(_nameOnLookupAccount) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_nameOnLookupAccount), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_nameOnLookupAccount) = value
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
        ''' The client associated with a transaction.
        ''' </summary>
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
        ''' The payment amount associated with a transaction.
        ''' </summary>
        Public Shared Property PaymentAmount() As Decimal
            Get
                If HttpContext.Current.Session(_paymentAmount) Is Nothing Then
                    Return Convert.ToDecimal(-1.0)
                Else
                    Return DirectCast(HttpContext.Current.Session(_paymentAmount), Decimal)
                End If
            End Get
            Set(value As Decimal)
                HttpContext.Current.Session(_paymentAmount) = value
            End Set
        End Property

        ''' <summary>
        ''' The payment date associated with a transaction.
        ''' </summary>
        Public Shared Property PaymentDate() As String
            Get
                If HttpContext.Current.Session(_paymentDate) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_paymentDate), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_paymentDate) = value
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
        ''' The postback message associated with a transaction.
        ''' </summary>
        Public Shared Property PostBackMessage() As String
            Get
                If HttpContext.Current.Session(_postBackMessage) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_postBackMessage), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_postBackMessage) = value
            End Set
        End Property
        ''' <summary>
        ''' The product name associated with a transaction.
        ''' </summary>
        Public Shared Property ProductName() As String
            Get
                If HttpContext.Current.Session(_productName) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_productName), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_productName) = value
            End Set
        End Property


        ''' <summary>
        ''' The service address associated with a transaction.
        ''' </summary>
        Public Shared Property ServiceAddress() As String
            Get
                If HttpContext.Current.Session(_serviceAddress) Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(HttpContext.Current.Session(_serviceAddress), String)
                End If
            End Get
            Set(ByVal value As String)
                HttpContext.Current.Session(_serviceAddress) = value
            End Set
        End Property
        ''' <summary>
        ''' The display type associated with a SSO token.
        ''' </summary>
        Public Shared Property SSODisplayType() As B2P.Objects.Client.SSODisplayTypes
            Get
                If HttpContext.Current.Session(_sSODisplayType) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_sSODisplayType), B2P.Objects.Client.SSODisplayTypes)
                End If
            End Get
            Set(value As B2P.Objects.Client.SSODisplayTypes)
                HttpContext.Current.Session(_sSODisplayType) = value
            End Set
        End Property
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
        ''' An SSO token information associated with a transaction.
        ''' </summary>
        Public Shared Property TokenInfo() As B2P.SSOLookup.PaymentInformation
            Get
                If HttpContext.Current.Session(_tokenInfo) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_tokenInfo), B2P.SSOLookup.PaymentInformation)
                End If
            End Get
            Set(value As B2P.SSOLookup.PaymentInformation)
                HttpContext.Current.Session(_tokenInfo) = value
            End Set
        End Property
        ''' <summary>
        ''' The transaction fee associated with a transaction.
        ''' </summary>
        Public Shared Property TransactionFee() As Decimal
            Get
                If HttpContext.Current.Session(_transactionFee) Is Nothing Then
                    Return Convert.ToDecimal(-1.0)
                Else
                    Return DirectCast(HttpContext.Current.Session(_transactionFee), Decimal)
                End If
            End Get
            Set(value As Decimal)
                HttpContext.Current.Session(_transactionFee) = value
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
        ''' The vendor reference code associated with a transaction.
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
        'Added By RS
        ''' <summary>
        ''' The ManageCart property will provide a single instance of managecart object
        ''' </summary>
        Public Shared Property ManageCart() As Cart.ManageCart
            Get
                If HttpContext.Current.Session(_manageCart) Is Nothing Then
                    HttpContext.Current.Session(_manageCart) = New Cart.ManageCart()
                End If
                Return DirectCast(HttpContext.Current.Session(_manageCart), Cart.ManageCart)
            End Get
            Set(ByVal value As Cart.ManageCart)
                HttpContext.Current.Session(_manageCart) = value
            End Set
        End Property
        Public Shared Property ClientType() As B2P.Cart.EClientType
            Get
                If HttpContext.Current.Session(_clientType) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_clientType), B2P.Cart.EClientType)
                End If
            End Get
            Set(ByVal value As B2P.Cart.EClientType)
                HttpContext.Current.Session(_clientType) = value
            End Set
        End Property
        ''' <summary>
        ''' This BreadCrumbMenuTab property will provide the BreadCrumbMenutab details
        ''' </summary>

        Public Shared Property BreadCrumbMenuTab() As Object
            Get
                If HttpContext.Current.Session(_breadCrumbMenuTab) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_breadCrumbMenuTab), Object)
                End If
            End Get
            Set(value As Object)
                HttpContext.Current.Session(_breadCrumbMenuTab) = value
            End Set
        End Property
        ''' <summary>
        ''' Contact info
        ''' </summary>
        ''' <returns></returns>
        Public Shared Property ContactInfo() As B2P.Payment.PaymentBase.OptionalUserData
            Get
                If HttpContext.Current.Session(_contactInfo) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_contactInfo), B2P.Payment.PaymentBase.OptionalUserData)
                End If
            End Get
            Set(value As  B2P.Payment.PaymentBase.OptionalUserData)
                HttpContext.Current.Session(_contactInfo) = value
            End Set
        End Property
        ''' <summary>
        ''' This will provide information weather Contact Info is Required
        ''' </summary>

        Public Shared Property IsContactInfoRequired() As Boolean
            Get
                If HttpContext.Current.Session(_isContactInfoRequired) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_isContactInfoRequired), Boolean)
                End If

            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_isContactInfoRequired) = value
            End Set
        End Property
        ''' <summary>
        ''' This will provide information weather Convenience Fess applicable
        ''' </summary>

        Public Shared Property IsConvenienceFeesApplicable() As Boolean
            Get
                If HttpContext.Current.Session(_isConvenienceFeesApplicable) Is Nothing Then
                    Return Nothing
                Else
                    Return DirectCast(HttpContext.Current.Session(_isConvenienceFeesApplicable), Boolean)
                End If

            End Get
            Set(ByVal value As Boolean)
                HttpContext.Current.Session(_isConvenienceFeesApplicable) = value
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
        ''' <summary>
        ''' Set Demographics Address
        ''' </summary>
        Public Shared Sub AddContactInfo(Address1 As String, Address2 As String, ContactName As String, Country As String, State As String, City As String, ZipCode As String, HomePhone As String)
            If ContactInfo Is Nothing Then
                ContactInfo = New B2P.Payment.PaymentBase.OptionalUserData()
            End If
            ContactInfo.Address1 = Address1
            ContactInfo.Address2 = Address2
            'ContactInfo.UserField1 = ContactInformation.Address1.Value
            'ContactInfo.UserField2 = ContactInformation.Address1.Value
            If State <> "" Then
                ContactInfo.State = State
            End If
            If State <> "" Then
                ContactInfo.City = City
            End If
            ContactInfo.Zip = ZipCode
            ContactInfo.HomePhone = HomePhone
        End Sub

#End Region

    End Class

End Namespace
