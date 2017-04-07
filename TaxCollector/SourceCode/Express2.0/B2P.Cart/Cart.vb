'==========================================================================
' File:     Cart.vb
' Date:     06.04.2017
' Author:   RS
'  
' Summary:  Cart is used for individual shopping cart item 
'           
'
' TODO:     
'
'==========================================================================
Imports System.Collections.Generic
Public Class Cart
    Public Property Index As Int32
    Public Property Item As String
    Public Property AccountIdFields As List(Of AccountIdField)
    Public Property Amount As Double
End Class

