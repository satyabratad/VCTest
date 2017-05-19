Imports System.Configuration.ConfigurationManager
Imports System.IO
Imports System.Net
Imports Telerik.Web.UI
Imports B2P.Reports
Imports System.Drawing

Partial Public Class ReportViewer
    Inherits baseclass

    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim UserSecurity As B2PAdminBLL.Role
    'Dim myReportDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
    Dim ReportInfo As B2P.Reports.ReportInformation
    Dim ErrorOccurred As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ReportType As String = "A"   'for testing only, this will be an enum from a report class. R = realtime, A = archive
        Session("ReportType") = ReportType

        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
        Dim rptType As String = Request.QueryString("type")

        'Security check
        'check for access rights
        Select Case rptType
            Case "rc", "po"
                If Not UserSecurity.ReportFinanceClient Then
                    Response.Redirect("/error/default.aspx?type=security")
                End If
            Case "ba"
                If Session("ClientCode").ToString <> "B2P" Then
                    Response.Redirect("/error/default.aspx?type=security")
                End If
            Case Else
        End Select

        If Not IsPostBack Then
            ClearForm()
            Session("ReportInformation") = Nothing
            lblTitle.Text = RetrieveTitle(rptType)
            loadData(rptType)
        Else
            'Dim Controls As New List(Of B2P.Reports.ReportParameter)

            'Response.Write(Session.SessionID)

            If IsNothing(Session("ReportInformation")) Then
                Dim reportID As Integer = CInt(RadListBox1.SelectedValue)
                Dim clientCode As String = Session("UserClientCode").ToString()

                ReportInfo = New B2P.Reports.ReportInformation(reportID, clientCode)
                Session("ReportInformation") = ReportInfo
            Else
                'Controls = CType(Session("Controls"), List(Of B2P.Reports.ReportParameter))
                ReportInfo = CType(Session("ReportInformation"), B2P.Reports.ReportInformation)
            End If
            ParametersPanel.Controls.Add(DynamicControls.LoadControls(ReportInfo.Parameters))
        End If
    End Sub

    Private Function RetrieveTitle(ByVal rptType As String) As String
        Dim rg As ReportGroup = New ReportGroup

        Return rg.GroupTitle(rptType)
    End Function

    Private Sub loadData(ByVal rptType As String)

        'need to dynamically load this list box
        'RadListBox1.LoadContentFile("~/resource/rptPayout.xml")
        Dim rg As ReportGroup = New ReportGroup
        Dim rl As List(Of ReportInformation) = rg.RetrieveGroupReports(rptType)

        RadListBox1.DataSource = rl
        RadListBox1.DataBind()
        RadListBox1.SelectedValue = CStr(rl(0).ReportID)

        If IsNothing(Session("ReportInformation")) Then
            If CStr(Session("UserClientCode")) <> "" Then
                ReportInfo = New B2P.Reports.ReportInformation(rl(0).ReportID, Session("UserClientCode").ToString())
            Else
                ReportInfo = New B2P.Reports.ReportInformation(rl(0).ReportID, Session("ClientCode").ToString())
            End If
            Session("ReportInformation") = ReportInfo
        Else
            ReportInfo = CType(Session("ReportInformation"), B2P.Reports.ReportInformation)
            RadListBox1.SelectedValue = CStr(ReportInfo.ReportID)
        End If

        ParametersPanel.Controls.Add(DynamicControls.LoadControls(ReportInfo.Parameters))

        If Not UserSecurity.ReportResearchClient Then
            Response.Redirect("/error?type=security")
        End If

        If CStr(Session("ClientCode")) = "B2P" Then

            pnlClientCode.Visible = True
            x.ClientCode = "B2P"
            x.Role = UserSecurity.UserRole

            Dim ds As DataSet = x.LoadDropDownBoxes
            ddlAUClient.DataSource = ds.Tables(0)
            ddlAUClient.DataTextField = ds.Tables(0).Columns("ClientCode").ColumnName.ToString()
            ddlAUClient.DataBind()
            If rptType = "ba" Then  'internal report
                ddlAUClient.Items.FindItemByText("B2P").Remove()
            Else
                If CStr(Session("UserClientCode")) <> "" Then
                    ddlAUClient.Items.FindItemByText(CStr(Session("UserClientCode"))).Selected = True
                Else
                    ddlAUClient.Items.FindItemByText("B2P").Selected = True
                End If
            End If


        Else
            pnlClientCode.Visible = False

        End If

        x = Nothing
    End Sub

    Private Sub ClearForm()
        ParametersPanel.Controls.Clear()
    End Sub

    Private Sub GetReport()

        If PeterBlum.DES.Globals.WebFormDirector.IsValid = False Then
            Return
        End If

        For Each ci As B2P.Reports.ReportParameter In ReportInfo.Parameters

            Select Case ci.DataType
                Case B2P.Reports.ReportParameter.DataTypes.String
                    Dim tb As PeterBlum.DES.Web.WebControls.TextBox = CType(ParametersPanel.FindControl(String.Format("DC_{0}", ci.ParameterName)), PeterBlum.DES.Web.WebControls.TextBox)
                    ci.Value = tb.Text
                    'Response.Write(String.Format("|{0} - {1}|", ci.ParameterName, tb.Text))
                Case B2P.Reports.ReportParameter.DataTypes.DropDown
                    Dim ddl As DropDownList = CType(ParametersPanel.FindControl(String.Format("DC_{0}", ci.ParameterName)), DropDownList)
                    'Response.Write(String.Format("|{0} - {1}|", ci.ParameterName, ddl.Text))
                    ci.Value = ddl.SelectedValue
                Case B2P.Reports.ReportParameter.DataTypes.DynamicDropDown
                    Dim ddl As DropDownList = CType(ParametersPanel.FindControl(String.Format("DC_{0}", ci.ParameterName)), DropDownList)
                    'Response.Write(String.Format("|{0} - {1}|", ci.ParameterName, ddl.Text))
                    ci.Value = ddl.SelectedValue
                    'If ci.Value.Length = 0 Then
                    '    ci.Value = DBNull
                    'End If
                Case B2P.Reports.ReportParameter.DataTypes.DateTime
                    If Session("ReportType").ToString = "R" Then
                        Dim ddl As DropDownList = CType(ParametersPanel.FindControl(String.Format("DC_{0}", ci.ParameterName)), DropDownList)
                        ci.Value = ddl.Text
                        'Response.Write(String.Format("|{0} - {1}|", ci.ParameterName, ddl.Text))
                    Else
                        Dim dt As PeterBlum.DES.Web.WebControls.DateTextBox = CType(ParametersPanel.FindControl(String.Format("DC_{0}", ci.ParameterName)), PeterBlum.DES.Web.WebControls.DateTextBox)
                        'Response.Write(String.Format("|{0} - {1}|", ci.ParameterName, dt.Text))
                        ci.Value = dt.Text
                    End If
                Case B2P.Reports.ReportParameter.DataTypes.Decimal
                    Dim Value As PeterBlum.DES.Web.WebControls.CurrencyTextBox = CType(ParametersPanel.FindControl(String.Format("DC_{0}", ci.ParameterName)), PeterBlum.DES.Web.WebControls.CurrencyTextBox)
                    'Response.Write(String.Format("|{0} - {1}|", ci.ParameterName, Value.Text))
                    ci.Value = Value.Text

                Case B2P.Reports.ReportParameter.DataTypes.Integer
                    Dim Value As PeterBlum.DES.Web.WebControls.IntegerTextBox = CType(ParametersPanel.FindControl(String.Format("DC_{0}", ci.ParameterName)), PeterBlum.DES.Web.WebControls.IntegerTextBox)
                    Response.Write(String.Format("|{0} - {1}|", ci.ParameterName, Value.Text))
                    ci.Value = Value.Text

                Case B2P.Reports.ReportParameter.DataTypes.Boolean
                    Dim Value As CheckBox = CType(ParametersPanel.FindControl(String.Format("DC_{0}", ci.ParameterName)), CheckBox)
                    Response.Write(String.Format("|{0} - {1}|", ci.ParameterName, Value.Checked))
                    If Value.Checked = True Then
                        ci.Value = "TRUE"
                    Else
                        ci.Value = "FALSE"
                    End If

                Case ReportParameter.DataTypes.Session
                    ci.Value = Session(ci.SessionName.ToString()).ToString()
            End Select

        Next

        ErrorOccurred = False

        Dim Parameters As New List(Of Microsoft.Reporting.WebForms.ReportParameter)

        'add ClientCode parameter
        Dim Parm As New Microsoft.Reporting.WebForms.ReportParameter
        Parm.Name = "ClientCode"
        Parm.Values.Add(Session("UserClientCode").ToString())
        Parameters.Add(Parm)

        'add ISIAdmin parameter
        Parm = New Microsoft.Reporting.WebForms.ReportParameter
        Parm.Name = "ISIAdmin"
        If UserSecurity.UserRole = "ISI Admin" Then
            Parm.Values.Add("True")
        Else
            Parm.Values.Add("False")
        End If
        'Parm.Values.Add(Session("UserClientCode").ToString())
        Parameters.Add(Parm)

        For Each ci As B2P.Reports.ReportParameter In ReportInfo.Parameters
            Parm = New Microsoft.Reporting.WebForms.ReportParameter
            Parm.Name = ci.ParameterName
            Parm.Values.Add(ci.Value)
            Parameters.Add(Parm)
        Next
        Session("Parms") = Parameters
        Dim url As String = "report.aspx"

        ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", String.Format("<script>openNewWin('{0}')</script>", url))

    End Sub

#Region "Events"
    Protected Sub ddlAUClient_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        Session("UserClientCode") = HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text)
        Dim reportID As Integer = CInt(RadListBox1.SelectedValue)
        Dim clientCode As String = Session("UserClientCode").ToString()

        ClearForm()

        ReportInfo = New B2P.Reports.ReportInformation(reportID, clientCode)
        Session("ReportInformation") = ReportInfo
        ParametersPanel.Controls.Add(DynamicControls.LoadControls(ReportInfo.Parameters))
    End Sub

    Protected Sub RadListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadListBox1.SelectedIndexChanged
        Dim reportID As Integer = CInt(RadListBox1.SelectedValue)
        Dim clientCode As String = Session("UserClientCode").ToString()

        ClearForm()

        ReportInfo = New B2P.Reports.ReportInformation(reportID, clientCode)
        Session("ReportInformation") = ReportInfo
        ParametersPanel.Controls.Add(DynamicControls.LoadControls(ReportInfo.Parameters))

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSubmit.Click
        GetReport()
        'GetReportFromSrv()
    End Sub
#End Region
End Class