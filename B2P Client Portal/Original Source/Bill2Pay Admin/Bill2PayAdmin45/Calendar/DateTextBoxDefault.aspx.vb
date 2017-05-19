Imports PeterBlum

Partial Public Class DateTextBoxDefault
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PeterBlum.DES.Globals.WebFormDirector.StyleSheetManager.OverrideDefaultUrl(PeterBlum.DES.Web.StyleSheetFiles.Calendar, "~/DES/Appearance/Date and Time/Calendar_Default.css")
        PeterBlum.DES.Globals.WebFormDirector.Browser.SupportsFilterStyles = False
    End Sub

End Class