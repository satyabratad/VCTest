Imports Microsoft.Security.Application
Public Class baseclass
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ViewStateUserKey = Context.Session.SessionID

        CheckSession()
        Dim Meta1, Meta2, Meta3, Meta4, Meta5 As New HtmlMeta
        Meta1.HttpEquiv = "Expires"
        Meta1.Content = "0"

        Meta2.HttpEquiv = "Cache-Control"
        Meta2.Content = "no-cache"

        Meta3.HttpEquiv = "Pragma"
        Meta3.Content = "no-cache"

        Meta4.HttpEquiv = "Refresh"
        Meta4.Content = Convert.ToString((Session.Timeout * 60) + 10) & "; url=/welcome"

        Meta5.HttpEquiv = "X-UA-Compatible"
        Meta5.Content = "IE=EmulateIE9"


        Page.Header.Controls.Add(Meta1)
        Page.Header.Controls.Add(Meta2)
        Page.Header.Controls.Add(Meta3)
        'Page.Header.Controls.Add(Meta4)
        Page.Header.Controls.Add(Meta5)

        'Session("SearchCriteria") = Nothing
    End Sub

    Protected Sub CheckSession()
        If Session("SessionID") Is Nothing Then
            Response.Redirect("/welcome")
        End If
    End Sub
    Friend Shared Function SafeEncode(ByVal TextToEncode As String) As String

        Dim TempValue As String = TextToEncode.Trim
        If TempValue.Length <> 0 Then
            TempValue = Replace(HttpUtility.HtmlEncode(TextToEncode), "&amp;", "&")
            TempValue = Replace(TempValue, "&#39;", "'")
            TempValue = Replace(TempValue, "&#44;", ",")
            Return TempValue
        Else
            Return TempValue
        End If

    End Function

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'NOTE: Session and Auth Cookie check
        If Session("AdminLoginID") IsNot Nothing AndAlso Session("AuthToken") IsNot Nothing AndAlso Request.Cookies("AuthToken") IsNot Nothing Then
            If Not Session("AuthToken").ToString().Equals(Request.Cookies("AuthToken").Value) Then
                Response.Redirect("/welcome")
            End If
        Else
            Response.Redirect("/welcome")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End If
    End Sub
    ''' <summary>
    ''' Builds an error message from a thrown exception.
    ''' </summary>
    ''' <param name="host">The website host URL address.</param>
    ''' <param name="page">The website page where the exception ocurred.</param>
    ''' <param name="source">The name of the application or the object that caused the exception.</param>
    ''' <param name="type">The declaring type that raised the exception.</param>
    ''' <param name="method">The calling method that raised the exception.</param>
    ''' <param name="message">The message that describes the current exception.</param>
    ''' <returns>String.</returns>
    Public Shared Function BuildErrorMessage(ByVal host As String, ByVal page As String, ByVal source As String, ByVal type As String, ByVal method As String, ByVal message As String) As String
        Dim sb As New StringBuilder
        Dim retVal As String = String.Empty

        Try
            sb.AppendLine(message)
            sb.AppendLine()
            sb.AppendLine("Host URL: " & host)
            sb.AppendLine("Offending Page: " & page)
            sb.AppendLine("Application Source: " & source)
            sb.AppendLine("Declaring Type: " & type)
            sb.AppendLine("Calling Method: " & method)
            sb.AppendLine()

            retVal = sb.ToString

        Finally
            ' Clean up a bit
            If sb IsNot Nothing Then
                sb = Nothing
            End If
        End Try

        Return retVal
    End Function
End Class
