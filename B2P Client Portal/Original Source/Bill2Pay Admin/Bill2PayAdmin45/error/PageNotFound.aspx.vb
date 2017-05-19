Public Class PageNotFound
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("/error/default.aspx?type=invalidpage")
    End Sub

End Class