Namespace B2P.PaymentLanding.Express.Web
    Public Class MenuFooter : Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If BLL.SessionManager.Client IsNot Nothing Then
                If BLL.SessionManager.Client.Website.Contains("http") Then
                    litLink1.Text = String.Format("<a href={0} target=""_blank"">{1}</a>", BLL.SessionManager.Client.Website, BLL.SessionManager.Client.ClientName)
                Else
                    litLink1.Text = String.Format("<a href=http://{0} target=""_blank"">{1}</a>", BLL.SessionManager.Client.Website, BLL.SessionManager.Client.ClientName)
                End If

            End If

            'litLinkFAQ.Text = String.Format("<a href={0} onclick=""window.open(this.href, 'mywin','width=500,height=500,resizable=1,scrollbars=1'); return false;"">{1}</a>", BLL.SessionManager.ClientFAQ, "Frequently Asked Questions")
        End Sub

    End Class
End Namespace