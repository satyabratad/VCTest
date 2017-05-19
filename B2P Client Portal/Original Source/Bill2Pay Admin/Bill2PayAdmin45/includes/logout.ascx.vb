Public Class logout1
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        litLoginID.Text = CStr(Session("AdminLoginID"))
    End Sub

End Class