Imports System
Imports System.IO
Namespace B2P.PaymentLanding.Express.Web
    Public Class _sessiondefault : Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Session.Abandon()

            ' Set the error message
            psmErrorMessage.ToggleStatusMessage("Multiple browser sessions have been detected. This may impact the proper processing of your payment. Please close your browser and start over.", StatusMessageType.Danger, True, True)
        End Sub

    End Class
End Namespace