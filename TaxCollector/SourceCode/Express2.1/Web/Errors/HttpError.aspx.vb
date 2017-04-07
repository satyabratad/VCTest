Imports System

Namespace B2P.PaymentLanding.Express.Web

    Public Class HttpError : Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim queryString As String = Request.ServerVariables("QUERY_STRING")
            Dim errCode As String = Request.QueryString("errCode")

            ' Set page title and error message
            If Not Utility.IsNullOrEmpty(queryString) Then
                If Not Page.IsPostBack Then
                    psmHttpError.ToggleStatusMessage(GetErrorMessage(Convert.ToInt32(errCode)), StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
                End If
            Else
                psmHttpError.ToggleStatusMessage("<strong>Uh-oh!</strong><br /><br />You must have landed here by mistake or entered this URL in the address bar.",
                                                 StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
            End If

            ' Clear the session
            BLL.SessionManager.Clear()
        End Sub

        Private Function GetErrorMessage(ByVal errCode As Int32) As String
            Dim retVal As String = String.Empty

            Select Case errCode
                Case 400
                    retVal = "<strong>Bad Request (Error 400)</strong><br /><br />" &
                             "The syntax of the request was not understood by the server."
                Case 401
                    retVal = "<strong>Unauthorized (Error 401)</strong><br /><br />" &
                             "This web request requires user authentication."
                Case 402
                    retVal = "<strong>Payment Required (Error 402)</strong><br /><br />" &
                             "The server is unable to serve the data that was requested."
                Case 403
                    retVal = "<strong>Forbidden (Error 403)</strong><br /><br />" &
                             "The request was denied as there was no permission to access the data."
                Case 404
                    retVal = "<strong>Page Not Found (Error 404)</strong><br /><br />" &
                             "The requested resource does not exist on the server."
                Case 408
                    retVal = "<strong>Request Timeout (Error 408)</strong><br /><br />" &
                             "The client failed to send a request in the time allowed by the server."
                Case 414
                    retVal = "<strong>Request URI Too Long (Error 414)</strong><br /><br />" &
                             "The request was unsuccessful because the URI specified is longer than the server is willing to process."
                Case 500
                    retVal = "<strong>Internal Server Error (Error 500)</strong><br /><br />" &
                             "The request was unsuccessful due to an unexpected condition encountered by the server."
                Case 503
                    retVal = "<strong>Service Unavailable (Error 503)</strong><br /><br />" &
                             "The request was unsuccessful to the server being down or maximum client connections."
                Case Else
                    retVal = "<strong>Uh-oh!</strong><br /><br />An unexpected error has occurred."
            End Select

            Return retVal
        End Function

    End Class

End Namespace