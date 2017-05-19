Public Class invalid
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        CheckSession()
        Dim Meta1, Meta2, Meta3, Meta4 As New HtmlMeta
        Meta1.HttpEquiv = "Expires"
        Meta1.Content = "0"

        Meta2.HttpEquiv = "Cache-Control"
        Meta2.Content = "no-cache"

        Meta3.HttpEquiv = "Pragma"
        Meta3.Content = "no-cache"

        Meta4.HttpEquiv = "Refresh"
        Meta4.Content = Convert.ToString((Session.Timeout * 60) + 10) & "; url=/welcome"


        Page.Header.Controls.Add(Meta1)
        Page.Header.Controls.Add(Meta2)
        Page.Header.Controls.Add(Meta3)
        'Page.Header.Controls.Add(Meta4)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case CStr((Session("ErrorType")))
            Case "security"
                litErrorType.Text = "Security Access Error"
                litErrorMessage.Text = "Oops, you do not have the proper security rights to view that page."
            Case "InvalidPage"
                litErrorType.Text = "Page Not Found"
                litErrorMessage.Text = "Oops, the URL you tried is not valid. Please check the URL and try again."
            Case Else
                litErrorType.Text = "System Error"
                litErrorMessage.Text = "The system has experienced an unforseen error."
        End Select
    End Sub

    Private Sub CheckSession()
        If Session("SessionID") Is Nothing Then
            Response.Redirect("/welcome")
        End If
    End Sub
End Class