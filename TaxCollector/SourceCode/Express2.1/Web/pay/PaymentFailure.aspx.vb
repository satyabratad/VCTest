Imports System

Namespace B2P.PaymentLanding.Express.Web
    Public Class PaymentFailure : Inherits SiteBasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Dim redirectUrl As String = String.Empty

            psmFailureMessage.ToggleStatusMessage(GetGlobalResourceObject("WebResources", "FailPageIntro").ToString(), StatusMessageType.Danger, StatusMessageSize.Normal, True, True)

            ' Save the payment and URLs before clearing the session
            If Not Me.IsPostBack Then
                If BLL.SessionManager.IsSSOProduct Then
                    ViewState("PaymentUrl") = "/sso/" & BLL.SessionManager.Token
                    redirectUrl = "/sso/" & BLL.SessionManager.Token
                Else
                    ViewState("PaymentUrl") = "/client/" & BLL.SessionManager.ClientCode
                    redirectUrl = "/client/" & BLL.SessionManager.ClientCode
                End If
                ViewState("RedirectUrl") = Utility.SafeEncode(redirectUrl.Trim)
            End If


            ' If it's an SSO client, omit first step on breadcrumb flow; show SSO breadcrumb panel
            If BLL.SessionManager.IsSSOProduct Then
                pnlSSOBreadcrumb.Visible = True
                pnlNonSSOBreadcrumb.Visible = False
            Else
                pnlSSOBreadcrumb.Visible = False
                pnlNonSSOBreadcrumb.Visible = True
            End If

        End Sub

        Private Sub btnTryAgain_Click(sender As Object, e As EventArgs) Handles btnTryAgain.Click
            Response.Redirect(ViewState("RedirectUrl").ToString, False)
        End Sub
    End Class
End Namespace