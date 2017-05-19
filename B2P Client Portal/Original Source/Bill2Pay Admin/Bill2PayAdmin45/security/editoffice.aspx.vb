Imports System.Configuration.ConfigurationManager

Partial Public Class editoffice
    Inherits baseclass
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim UserSecurity As B2PAdminBLL.Role

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)

        If Not UserSecurity.SecurityOfficeMgmt Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
        If CStr(Session("editOfficeID")) = "" Then
            Response.Redirect("/security/modifyoffice.aspx")
        End If
        If Not IsPostBack Then
            loadData()
        End If

    End Sub
    Protected Sub loadData()
        ddlState.DataSourceID = "XmlDataSource1"
        ddlState.DataBind()
        ddlState.SortItems()

        ddlStatus.DataSourceID = "XmlDataSource2"
        ddlStatus.DataBind()

        x.ClientCode = CStr(Session("UserClientCode"))
        x.OfficeID = CInt(Session("editOfficeID"))


        x.GetOffice()
        txtAOName.Text = Trim(HttpUtility.HtmlEncode(x.OfficeName))
        ddlStatus.SelectedValue = Trim(HttpUtility.HtmlEncode(x.Status))
        txtAOAddress.Text = Trim(HttpUtility.HtmlEncode(x.Address))
        txtAOAddress2.Text = HttpUtility.HtmlEncode(x.Address2)
        txtAOCity.Text = Trim(HttpUtility.HtmlEncode(x.City))
        txtAOPhone.Text = HttpUtility.HtmlEncode(x.PhoneNumber)
        txtAOZipCode.Text = HttpUtility.HtmlEncode(x.Zip)
        ddlState.SelectedValue = HttpUtility.HtmlEncode(x.State)





    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            btnSubmit.Enabled = False
            x.OfficeID = CInt(Session("editOfficeID"))
            x.OfficeName = HttpUtility.HtmlEncode(txtAOName.Text)
            x.Address = HttpUtility.HtmlEncode(txtAOAddress.Text)
            x.Address2 = HttpUtility.HtmlEncode(txtAOAddress2.Text)
            x.City = HttpUtility.HtmlEncode(txtAOCity.Text)
            x.State = HttpUtility.HtmlEncode(ddlState.SelectedValue)
            x.Zip = HttpUtility.HtmlEncode(txtAOZipCode.Text)
            x.PhoneNumber = HttpUtility.HtmlEncode(txtAOPhone.Text)
            x.ClientCode = CStr(Session("UserClientCode"))
            x.Status = HttpUtility.HtmlEncode(ddlStatus.SelectedValue)

            If x.EditOffice() Then
                lblAUStatus.ForeColor = Drawing.Color.Green
                pnlResults.Visible = True
                lblAUStatus.Text = "Office information updated"
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
        End Try

    End Sub

    Private Sub lnkViewOffices_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewOffices.Click
        Response.Redirect("/security/modifyoffice.aspx")
    End Sub
End Class