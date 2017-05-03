'==========================================================================
' File:     PaymentInformation.vb
' Date:     06.04.2017
' Author:   RS
'  
' Summary:  PaymentInformation is used to store cart wise payment details
'        
'==========================================================================
Imports System.Collections.Generic
Public Class PaymentInformation
    ''' <summary>
    ''' This is a boolean property to set visibility of edit icon
    ''' </summary>
    Public Property IsEditIconVisible As Boolean
    ''' <summary>
    ''' This will set payment status code Lookup/SSO
    ''' </summary>
    Public Enum EPaymentStatusCodes
        Allowed = 0
        NotAllowed = 1
        NotEditable = 2
        MinimumPaymentRequired = 3
    End Enum
    Public Property PaymentStatusCodes As EPaymentStatusCodes
    ''' <summary>
    ''' BlockedACH - Block bank transaction
    ''' </summary>
    Public Property BlockedACH As Boolean
    ''' <summary>
    ''' ACHAccepted - accept bank transaction
    ''' </summary>
    Public Property ACHAccepted As Boolean
    ''' <summary>
    ''' AllowECheckPayment - accept AllowECheckPayment transaction
    ''' </summary>
    Public Property AllowECheckPayment As Boolean
    ''' <summary>
    ''' BlockedCC - block CreditCard transaction
    ''' </summary>
    Public Property BlockedCC As Boolean
    ''' <summary>
    ''' CreditCardAccepted - accept CreditCard transaction
    ''' </summary>
    Public Property CreditCardAccepted As Boolean
    ''' <summary>
    ''' AllowCreditCardPayment - accept creaditcard transaction
    ''' </summary>
    Public Property AllowCreditCardPayment As Boolean
End Class


