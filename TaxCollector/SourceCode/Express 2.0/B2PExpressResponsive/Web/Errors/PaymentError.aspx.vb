Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web
    Public Class _PaymentError : Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim url As String = "/Payment.aspx"

            ' Give the user some status info
            psmError.ToggleStatusMessage("We're sorry, payments are currently not accepted for this item(s).", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)

            ' Clear the session
            BLL.SessionManager.Clear()

        End Sub

    End Class
End Namespace