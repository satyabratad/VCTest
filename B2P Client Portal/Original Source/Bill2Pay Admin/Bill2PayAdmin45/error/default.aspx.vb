Public Class _default3
    Inherits System.Web.UI.Page

    Private Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        Dim errMsg As String = String.Empty
        Try
            If Not IsNothing(Request.QueryString("Type")) Then
                ViewState("ErrorType") = Request.QueryString("Type")
            Else
                ViewState("ErrorType") = ""
            End If
        Catch ex As Exception
            ' Build the error message
            errMsg = baseclass.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath, _
                                               ex.Source, ex.TargetSite.DeclaringType.Name, _
                                               ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin Site --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error/", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim errMsg As String = String.Empty
        Try
            Select Case UCase(ViewState("ErrorType").ToString)
                Case "SECURITY"
                    litErrorType.Text = "Security Access Error"
                    litErrorMessage.Text = "Oops, you do not have the proper security rights to view that page."
                Case "INVALIDPAGE"
                    litErrorType.Text = "Page Not Found"
                    litErrorMessage.Text = "Oops, the URL you tried is not valid. Please check the URL and try again."
                Case Else
                    Session.Abandon()
                    litErrorType.Text = "System Error"
                    litErrorMessage.Text = "The system has experienced an unforseen error."
            End Select
        Catch ex As Exception
            ' Build the error message
            errMsg = baseclass.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath, _
                                               ex.Source, ex.TargetSite.DeclaringType.Name, _
                                               ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin Site --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error/", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    
End Class