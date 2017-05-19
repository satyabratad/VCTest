Imports System.Configuration.ConfigurationManager
Partial Public Class addoffice
    Inherits baseclass
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim dsOffice As New DataSet
    Dim UserSecurity As B2PAdminBLL.Role
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)

        If Not UserSecurity.SecurityOfficeMgmt Then
            Response.Redirect("/error/default.aspx?type=security")
        End If

        txtAOName.Focus()
        If Not IsPostBack Then
            x.ClientCode = CStr(Session("ClientCode"))
            loadData()
        End If


    End Sub
    Private Sub loadData()

        ddlState.DataSourceID = "XmlDataSource1"
        ddlState.DataBind()
        ddlState.SortItems()

        Dim UserSecurity As B2PAdminBLL.Role = CType(Session("RoleInfo"), B2PAdminBLL.Role)
        x.Role = UserSecurity.UserRole
        Dim ds As DataSet = x.LoadDropDownBoxes

        If CStr(Session("ClientCode")) = "B2P" Then
            ddlAUClient.DataSource = ds.Tables(0)
            ddlAUClient.DataTextField = ds.Tables(0).Columns("ClientCode").ColumnName.ToString()
            ddlAUClient.DataBind()
            ddlAUClient.Visible = True
            lblAUClient.Visible = False
        Else
            RequiredFieldValidator4.Enabled = False
            lblAUClient.Text = CStr(Session("ClientCode"))
            lblAUClient.Visible = True
            ddlAUClient.Visible = False
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            btnSubmit.Enabled = False
            x.OfficeName = HttpUtility.HtmlEncode(txtAOName.Text)
            x.Address = HttpUtility.HtmlEncode(txtAOAddress.Text)
            x.Address2 = HttpUtility.HtmlEncode(txtAOAddress2.Text)
            x.City = HttpUtility.HtmlEncode(txtAOCity.Text)
            x.State = HttpUtility.HtmlEncode(ddlState.SelectedValue)
            x.Zip = HttpUtility.HtmlEncode(txtAOZipCode.Text)
            x.PhoneNumber = HttpUtility.HtmlEncode(txtAOPhone.Text)
            If CStr(Session("ClientCode")) = "B2P" Then
                x.ClientCode = HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text)
            Else
                x.ClientCode = CStr(Session("ClientCode"))
            End If

            x.Status = "A"

            If x.AddNewOffice() Then
                lblAUStatus.ForeColor = Drawing.Color.Green
                pnlResults.Visible = True
                lblAUStatus.Text = "Office created"
                pnlAddOffice.Visible = False
            Else
                btnSubmit.Enabled = True
                pnlResults.Visible = False
                lblAUStatus.Visible = True
                lblAUStatus.Text = "Office not created"
                lblAUStatus.ForeColor = Drawing.Color.Red

            End If

        Catch ex As Exception
            btnSubmit.Enabled = True
            lblAUStatus.Text = "There was a problem generating your request."
            lblAUStatus.ForeColor = Drawing.Color.Red
        End Try

    End Sub


    Protected Sub lnkAddOffice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAddOffice.Click
        pnlAddOffice.Visible = True
        pnlResults.Visible = False
        lblAUStatus.Text = ""
        lblAUStatus.Visible = False
    End Sub

    Protected Sub lnkViewOffices_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewOffices.Click
        Response.Redirect("/security/modifyoffice.aspx")
    End Sub
End Class