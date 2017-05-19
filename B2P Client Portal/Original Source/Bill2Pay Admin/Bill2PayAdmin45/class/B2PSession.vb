Imports System.Drawing

Public Class B2PSession

    Public Shared Property ClientCode() As String
        Get
            Return getString("ClientCode").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("ClientCode", Value)
        End Set
    End Property

    Public Shared Property OfficeID() As Integer
        Get
            Return getInt("OfficeID")
        End Get
        Set(ByVal Value As Integer)
            SaveSessionVariable("OfficeID", Value)
        End Set
    End Property
    Public Shared Property ClientName() As String
        Get
            Return getString("ClientName").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("ClientName", Value)
        End Set
    End Property
    Public Shared Property TaxConfirm() As String
        Get
            Return getString("TaxConfirm").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("TaxConfirm", Value)
        End Set
    End Property
    Public Shared Property NonTaxConfirm() As String
        Get
            Return getString("NonTaxConfirm").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("NonTaxConfirm", Value)
        End Set
    End Property
    Public Shared Property TaxAuth() As String
        Get
            Return getString("TaxAuth").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("TaxAuth", Value)
        End Set
    End Property
    Public Shared Property NonTaxAuth() As String
        Get
            Return getString("NonTaxAuth").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("NonTaxAuth", Value)
        End Set
    End Property
    Public Shared Property TaxItemsAmount() As Decimal
        Get
            Dim obj As Object = GetSessionVariable("TaxItemsAmount")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return CDec(obj)
            End If

        End Get
        Set(ByVal Value As Decimal)
            SaveSessionVariable("TaxItemsAmount", Value)
        End Set
    End Property
    Public Shared Property TaxFee() As Decimal
        Get
            Dim obj As Object = GetSessionVariable("TaxFee")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return CDec(obj)
            End If

        End Get
        Set(ByVal Value As Decimal)
            SaveSessionVariable("TaxFee", Value)
        End Set
    End Property
    Public Shared Property NonTaxFee() As Decimal
        Get
            Dim obj As Object = GetSessionVariable("NonTaxFee")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return CDec(obj)
            End If

        End Get
        Set(ByVal Value As Decimal)
            SaveSessionVariable("NonTaxFee", Value)
        End Set
    End Property
    Public Shared Property NonTaxItemsAmount() As Decimal
        Get
            Dim obj As Object = GetSessionVariable("NonTaxItemsAmount")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return CDec(obj)
            End If

        End Get
        Set(ByVal Value As Decimal)
            SaveSessionVariable("NonTaxItemsAmount", Value)
        End Set
    End Property

    Public Shared Property VendorReferenceCode() As String
        Get
            Return getString("VendorReferenceCode").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("VendorReferenceCode", Value)
        End Set
    End Property

  

    Public Shared Property CreditCard() As B2P.Common.Objects.CreditCard
        Get
            Dim obj As Object = GetSessionVariable("CreditCard")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return CType(obj, B2P.Common.Objects.CreditCard)
            End If

        End Get
        Set(ByVal Value As B2P.Common.Objects.CreditCard)
            SaveSessionVariable("CreditCard", Value)
        End Set
    End Property

    Public Shared Property SwipedCreditCard() As B2P.Common.Objects.SwipedCreditCard
        Get
            Dim obj As Object = GetSessionVariable("SwipedCreditCard")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return CType(obj, B2P.Common.Objects.SwipedCreditCard)
            End If

        End Get
        Set(ByVal Value As B2P.Common.Objects.SwipedCreditCard)
            SaveSessionVariable("SwipedCreditCard", Value)
        End Set
    End Property

    Public Shared Property Swiped() As Boolean
        Get
            Dim obj As Object = GetSessionVariable("Swiped")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return Convert.ToBoolean(obj)
            End If

        End Get
        Set(ByVal Value As Boolean)
            SaveSessionVariable("Swiped", Value)
        End Set
    End Property

    Public Shared Property cardType() As B2P.Payment.FeeCalculation.PaymentTypes
        Get
            Dim obj As Object = GetSessionVariable("cardType")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return CType(obj, B2P.Payment.FeeCalculation.PaymentTypes)
            End If

        End Get
        Set(ByVal Value As B2P.Payment.FeeCalculation.PaymentTypes)
            SaveSessionVariable("cardType", Value)
        End Set
    End Property

    Public Shared Property CardIssuer() As String
        Get
            Dim obj As Object = GetSessionVariable("CardIssuer")
            If obj Is Nothing Then
                Return Nothing
            Else
                Return obj.ToString
            End If

        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("CardIssuer", Value)
        End Set
    End Property

  

    Public Shared Property ShoppingCart() As B2P.ShoppingCart.Cart
        Get
            Dim obj As Object = GetSessionVariable("ShoppingCart")
            If obj Is Nothing Then
                B2P.Common.Logging.LogError("Manatron.Session", "ShoppingCart session is nothing", B2P.Common.Logging.NotifySupport.No)
                Return Nothing
            Else
                Return CType(obj, B2P.ShoppingCart.Cart)
            End If

        End Get
        Set(ByVal Value As B2P.ShoppingCart.Cart)
            SaveSessionVariable("ShoppingCart", Value)
        End Set
    End Property

  
    Public Shared Property ClientMessage() As String
        Get
            Return getString("ClientMessage").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("ClientMessage", Value)
        End Set
    End Property

   


    Public Shared Property UserField1() As String
        Get
            Return getString("UserField1").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("UserField1", Value)
        End Set
    End Property

    Public Shared Property UserField2() As String
        Get
            Return getString("UserField2").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("UserField2", Value)
        End Set
    End Property

    Public Shared Property UserField3() As String
        Get
            Return getString("UserField3").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("UserField3", Value)
        End Set
    End Property

    Public Shared Property UserField4() As String
        Get
            Return getString("UserField4").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("UserField4", Value)
        End Set
    End Property

    Public Shared Property UserField5() As String
        Get
            Return getString("UserField5").ToString
        End Get
        Set(ByVal Value As String)
            SaveSessionVariable("UserField5", Value)
        End Set
    End Property


    Public Shared Property PaymentProcessed() As Boolean
        Get
            Dim obj As Object = GetSessionVariable("PaymentProcessed")
            If obj Is Nothing Then
                Return False
            Else
                Return Convert.ToBoolean(obj)
            End If

        End Get
        Set(ByVal Value As Boolean)
            SaveSessionVariable("PaymentProcessed", Value)
        End Set

    End Property

    Public Shared Sub Reset()

        B2PSession.CardIssuer = ""
        B2PSession.ClientCode = ""
        B2PSession.ClientMessage = ""
        B2PSession.CreditCard = Nothing
        B2PSession.NonTaxConfirm = ""
        B2PSession.NonTaxAuth = ""
        B2PSession.NonTaxFee = 0
        B2PSession.NonTaxItemsAmount = 0
        B2PSession.OfficeID = 0
        B2PSession.PaymentProcessed = False
        B2PSession.ShoppingCart = Nothing
        B2PSession.Swiped = False
        B2PSession.SwipedCreditCard = Nothing
        B2PSession.TaxAuth = ""
        B2PSession.TaxConfirm = ""
        B2PSession.TaxFee = 0
        B2PSession.TaxItemsAmount = 0
        B2PSession.UserField1 = ""
        B2PSession.UserField2 = ""
        B2PSession.UserField3 = ""
        B2PSession.UserField4 = ""
        B2PSession.UserField5 = ""
        B2PSession.VendorReferenceCode = ""

    End Sub


    Private Shared Function GetSessionVariable(ByVal variableName As String) As Object
        Return HttpContext.Current.Session(variableName)
    End Function

    Private Shared Sub SaveSessionVariable(ByVal variableName As String, ByVal value As Object)
        HttpContext.Current.Session(variableName) = value
    End Sub

    Private Shared Function getString(ByVal variableName As String) As String
        Dim obj As Object = GetSessionVariable(variableName)
        If obj Is Nothing Then
            Return Nothing
        Else
            Return obj.ToString
        End If

    End Function

    Private Shared Function getBoolean(ByVal variableName As String) As Boolean
        Dim obj As Object = GetSessionVariable(variableName)
        If obj Is Nothing Then
            Return Nothing
        Else
            Return CBool(obj)
        End If

    End Function



    Private Shared Function getInt(ByVal variableName As String) As Integer
        Dim obj As Object = GetSessionVariable(variableName)
        If obj Is Nothing Then
            Return Nothing
        Else
            Return CInt(obj)
        End If

    End Function

End Class
