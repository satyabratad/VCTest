Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class timeout
    Inherits System.Web.UI.UserControl

    Public Property TimeoutControlEnabled() As Boolean
        Get
            Return Timeout1.Enabled
        End Get
        Set(ByVal value As Boolean)
            Timeout1.Enabled = value
        End Set
    End Property

    Public ReadOnly Property TimeoutControlClientId() As String
        Get
            Return Timeout1.ClientID
        End Get
    End Property

    Protected Sub Timeout1_RaisingCallbackEvent(sender As Object, e As ImageClickEventArgs)

        ' when the timeout control's callback is fired, this event will fire
        Dim x As String = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class