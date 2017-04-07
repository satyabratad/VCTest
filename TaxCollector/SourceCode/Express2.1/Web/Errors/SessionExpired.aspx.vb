Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web

    Public Class SessionExpired : Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            psmSessionExpired.ToggleStatusMessage("Your session has expired.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)

            ' Clear the session
            BLL.SessionManager.Clear()
        End Sub

    End Class

End Namespace