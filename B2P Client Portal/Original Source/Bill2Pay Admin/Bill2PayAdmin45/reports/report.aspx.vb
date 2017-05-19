Imports B2P.Reports

Partial Public Class report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ReportInfo As B2P.Reports.ReportInformation = CType(Session("ReportInformation"), B2P.Reports.ReportInformation)

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
            ReportViewer1.ServerReport.ReportServerUrl = New System.Uri(B2P.Reports.Configuration.ReportServiceURL())
            ReportViewer1.ServerReport.ReportPath = ReportInfo.ReportPath

            Dim Parameters As New List(Of Microsoft.Reporting.WebForms.ReportParameter)
            Parameters = CType(Session("Parms"), List(Of Microsoft.Reporting.WebForms.ReportParameter))
            ReportViewer1.ServerReport.ReportServerCredentials = New B2P.Reports.MyReportServerCredentials
            ReportViewer1.ServerReport.SetParameters(Parameters)
            ReportViewer1.ServerReport.Refresh()
        End If
       
    End Sub

End Class