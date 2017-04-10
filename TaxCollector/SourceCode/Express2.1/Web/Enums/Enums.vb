Namespace B2P.PaymentLanding.Express.Web

#Region " ::: Enums ::: "

    ''' <summary>
    ''' One of the values that specifies an email message type.
    ''' </summary>
    Public Enum EmailMessageType
        [Error]
        Normal
    End Enum

    ''' <summary>
    ''' One of the values that specifies a field validation type.
    ''' </summary>
    Public Enum FieldValidationType
        None
        AccountNumber
        BankAccount
        CreditCard
        Currency
        CVV
        [Date]
        Email
        ExpirationDate
        [Integer]
        IpAddress
        Name
        NonEmpty
        Numeric
        PhoneFax
        PositiveInteger
        RoutingNumber
        SSN
        StateAbbreviation
        TrueFalse
        URL
        YesNo
        ZipCodeCanada
        ZipCodeInternational
        ZipCodeUnitedStates
    End Enum

    ''' <summary>
    ''' One of the values that specifies a status message container size.
    ''' </summary>
    Public Enum StatusMessageSize
        ExtraSmall
        Normal
        Small
    End Enum

    ''' <summary>
    ''' One of the values that specifies a status message type.
    ''' </summary>
    Public Enum StatusMessageType
        None
        Danger
        Info
        Success
        Warning
    End Enum
    ''' <summary>
    ''' Bread Crumb menu page tag names
    ''' </summary>
    Public Enum PageTabName
        Home = 1
        ContactInfo = 2
        PaymentDetails = 3
        PaymentConfirm = 4
        PaymentSuccess = 5
        PaymentFaild = 6

    End Enum
#End Region

End Namespace
