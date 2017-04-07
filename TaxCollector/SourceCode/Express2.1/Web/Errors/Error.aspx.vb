Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web

    Public Class _Error : Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim url As String = "/Payment.aspx"

            ' Give the user some status info
            psmError.ToggleStatusMessage("An unexpected error has occurred. We are looking into it and should have it resolved shortly.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)

            ' Clear the session
            BLL.SessionManager.Clear()

            ' Not sure we want to send them to the beginning??
            'hypContinue.NavigateUrl = url
        End Sub

    End Class

End Namespace