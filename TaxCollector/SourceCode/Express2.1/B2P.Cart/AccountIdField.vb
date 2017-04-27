'==========================================================================
' File:     AccountIdField.vb
' Date:     06.04.2017
' Author:   RS
'  
' Summary:  AccountIdField class is used to manage account details 
'==========================================================================
Public Class AccountIdField
    Sub New(lbl As String, val As String)
        Label = lbl
        Value = val
    End Sub
    ''' <summary>
    ''' Set Account field caption
    ''' </summary>
    Public Property Label As String
    ''' <summary>
    ''' Set Account field value
    ''' </summary>
    Public Property Value As String

End Class


