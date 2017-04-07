'==========================================================================
' File:     AccountIdField.vb
' Date:     06.04.2017
' Author:   RS
'  
' Summary:  AccountIdField class is used to manage account details 
'           
'
' TODO:     
'
'==========================================================================
Public Class AccountIdField
    Sub New(lbl As String, val As String)
        Label = lbl
        Value = val
    End Sub

    Public Property Label As String
    Public Property Value As String

End Class


