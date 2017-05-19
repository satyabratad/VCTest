Public Partial Class DateTextBoxOurStyles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PeterBlum.DES.Globals.WebFormDirector.Browser.SupportsFilterStyles = False
    End Sub

End Class