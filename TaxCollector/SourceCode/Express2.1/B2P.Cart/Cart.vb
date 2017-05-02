'==========================================================================
' File:     Cart.vb
' Date:     06.04.2017
' Author:   RS
'  
' Summary:  Cart is used for individual shopping cart item 
'        
'==========================================================================
Imports System.Collections.Generic
Public Class Cart
    ''' <summary>
    ''' Set Index
    ''' </summary>
    Public Property Index As Int32
    ''' <summary>
    ''' Set Item/Product Name
    ''' </summary>
    Public Property Item As String
    ''' <summary>
    ''' Set Account field list
    ''' </summary>
    Public Property AccountIdFields As List(Of AccountIdField)
    ''' <summary>
    ''' Set Amount
    ''' </summary>
    Public Property Amount As Double
    ''' <summary>
    ''' Set AmountDue
    ''' </summary>
    Public Property AmountDue As Double
    ''' <summary>
    ''' Set PropertyAddress
    ''' </summary>
    Public Property PropertyAddress As PropertyAddress
    ''' <summary>
    ''' This is a boolean property to check if Property address need to be stored
    ''' </summary>
    Public Property CollectPropertyAddress As Boolean
    ''' <summary>
    ''' This is a boolean property to set visibility of edit icon
    ''' </summary>
    Public Property IsEditIconVisible As Boolean
    ''' <summary>
    ''' Client Codde
    ''' </summary>
    Public Property ClientCode As String
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
End Class


