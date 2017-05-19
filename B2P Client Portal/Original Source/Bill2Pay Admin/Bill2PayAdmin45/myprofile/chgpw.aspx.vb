Imports System.Configuration.ConfigurationManager

Partial Public Class chgpw
    Inherits baseclass
    Dim UserSecurity As B2PAdminBLL.Role
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtAUPassword.Focus()
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        x.CurrentPassword = x.DecryptPassword(CStr(Session("Password")))
        x.NewPassword = HttpUtility.HtmlEncode(txtAUPassword.Text)
        x.UserLogin = CStr(Session("AdminLoginID"))

        If x.ResetOwnPassword Then
            Session("Password") = x.EncryptPassword(x.NewPassword)
            lblAUStatus.Text = "Password reset"
            lblAUStatus.Visible = True
            lblAUStatus.ForeColor = Drawing.Color.Green
            pnlChangePW.Visible = False
            pnlResults.Visible = True
        Else
            'lblAUStatus.Text = "Password not reset"
            lblAUStatus.Text = x.PasswordError
            lblAUStatus.ForeColor = Drawing.Color.Red
            lblAUStatus.Visible = True
            pnlResults.Visible = True
        End If
    End Sub
End Class