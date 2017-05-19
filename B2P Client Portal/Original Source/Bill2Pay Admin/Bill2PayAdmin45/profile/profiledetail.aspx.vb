Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI
Imports PeterBlum
Imports System.Threading
Imports System.Globalization

Partial Public Class ProfileDetail1
    Inherits baseclass

    Dim UserSecurity As B2PAdminBLL.Role
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim ds As New DataSet

#Region " Page Events "
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        ViewStateUserKey = Context.Session.SessionID

        CheckSession()
        Dim Meta1, Meta2, Meta3, Meta4 As New HtmlMeta
        Meta1.HttpEquiv = "Expires"
        Meta1.Content = "0"

        Meta2.HttpEquiv = "Cache-Control"
        Meta2.Content = "no-cache"

        Meta3.HttpEquiv = "Pragma"
        Meta3.Content = "no-cache"

        Meta4.HttpEquiv = "Refresh"
        Meta4.Content = Convert.ToString((Session.Timeout * 60) + 10) & "; url=/welcome"


        Page.Header.Controls.Add(Meta1)
        Page.Header.Controls.Add(Meta2)
        Page.Header.Controls.Add(Meta3)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)

        If Not UserSecurity.SearchScreen Then
            Response.Redirect("/error/default.aspx?type=security")
        End If

        If Not IsPostBack Then
            If Not Session("Profile_ID") Is Nothing Then
                LoadData()
                rdActivity.ExportSettings.IgnorePaging = True
                RadGrid1.ExportSettings.IgnorePaging = True
                RadGrid2.ExportSettings.IgnorePaging = True
            Else
                lblMessage.Text = "An unexpected error has occurred. Please review the search parameters on the previous page and try again."
            End If
        End If
    End Sub
#End Region

#Region " Control Events "
    'Private Sub btnCancelTxtPmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelTxtPmt.Click
    '    If B2P.Profile.TextPay.CancelTextpayEnrollment(Convert.ToInt32(Session("Profile_ID")), Session("PhoneNumber").ToString, "Canceled By Admin", Convert.ToInt32(Session("SecurityID"))) Then
    '        lblMessage2.Text = "Text Payments have been successfully deactivated."
    '        LoadData()
    '        'pnlTextPay.Visible = False
    '        'btnCancelTxtPmt.Enabled = False
    '    Else
    '        lblMessage2.Text = "There was an error deactivating text payments."
    '    End If
    'End Sub

    'Private Sub ButtonOk3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk3.Click
    '    If Page.IsValid Then
    '        Dim x As B2P.Profile.ProfileManagement.ChangeEmailResults

    '        x = B2P.Profile.ProfileManagement.ChangeEmailAddress(Session("ProfileUserName").ToString, HttpUtility.HtmlEncode(txtEmailAddress.Text.Trim), Convert.ToInt32(Session("SecurityID")))
    '        Select Case x
    '            Case B2P.Profile.ProfileManagement.ChangeEmailResults.EmailAddressExists
    '                lblMessage2.Text = "Email address was not changed. The new email address already exist within the system."
    '            Case B2P.Profile.ProfileManagement.ChangeEmailResults.EmailsMatch
    '                lblMessage2.Text = "Email address was not changed. The new and old email addresses match."
    '            Case B2P.Profile.ProfileManagement.ChangeEmailResults.Failed
    '                lblMessage2.Text = "Email address was not changed."
    '            Case B2P.Profile.ProfileManagement.ChangeEmailResults.ProfileNotFound
    '                lblMessage2.Text = "Email address was not changed. The profile was not found."
    '            Case B2P.Profile.ProfileManagement.ChangeEmailResults.Success
    '                lblMessage2.Text = "Email address successfully updated."
    '                LoadData()
    '        End Select
    '    End If
    'End Sub

    Private Sub btnDisableProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisableProfile.Click
        Try

            If B2P.Admin.Profile.ProfileManagement.DisableProfile(Convert.ToInt32(Session("Profile_ID")), Session("ClientCode").ToString(), Convert.ToInt32(Session("SecurityID"))) Then
                'lblMessage2.Text = "The profile has been disabled."
                'LoadData()
                Response.Redirect("profiledisabled.aspx", False)
            Else
                lblMessage2.Text = "There was an error disabling the profile."
            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during disabling profile " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub btnResetPwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetPwd.Click
        Try

            If B2P.Admin.Profile.ProfileManagement.ResetPassword(Session("ProfileUserName").ToString, Convert.ToInt32(Session("SecurityID"))) Then
                lblMessage2.Text = "Password reset was successful."
            Else
                lblMessage2.Text = "Password reset was unsuccessful."
            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during profile password reset " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Protected Sub btnUpdateAddress_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateAddress.Click
        Try

            Dim u As B2P.Profile.User = New B2P.Profile.User
            u = B2P.Admin.Profile.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(Session("Profile_ID").ToString))

            If (txtAddress1.Text <> "" And txtCity.Text <> "" And ddlState.SelectedValue <> "" And txtZip.Text <> "") Then
                B2P.Admin.Profile.ProfileManagement.UpdateDefaultAddress(Convert.ToInt32(Session("Profile_ID")),
                                                                           u.ProfileAddress_ID,
                                                                           Utility.SafeEncode(txtAddress1.Text),
                                                                           Utility.SafeEncode(txtAddress2.Text),
                                                                           Utility.SafeEncode(txtCity.Text),
                                                                           Utility.SafeEncode(ddlState.SelectedValue),
                                                                           Utility.SafeEncode(txtZip.Text),
                                                                           Utility.SafeEncode(ddlCountry.SelectedValue),
                                                                           Convert.ToInt32(Session("SecurityID")))
                lblMessage2.Text = "Default billing address updated."
            Else
                lblMessage.Text = "There was a problem updating the address. No changes were made to the default billing address."
            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during profile address update " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub

    Protected Sub btnUpdateProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateProfile.Click
        Dim Updated As Boolean = False

        Try
            lblMessage2.Text = ""
            Dim u As B2P.Profile.User = New B2P.Profile.User
            u = B2P.Admin.Profile.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(Session("Profile_ID").ToString))


            If Page.IsValid Then
                If (txtEmailAddress.Text <> "" And txtEmailAddress.Text <> u.EmailAddress) Then
                    Dim x As B2P.Admin.Profile.ProfileManagement.ChangeEmailResults

                    x = B2P.Admin.Profile.ProfileManagement.ChangeEmailAddress(u.UserName,
                                                                           Utility.SafeEncode(txtEmailAddress.Text.Trim),
                                                                           Convert.ToInt32(Session("SecurityID")))

                    Select Case x
                        Case B2P.Admin.Profile.ProfileManagement.ChangeEmailResults.EmailAddressExists
                            lblMessage2.Text = "Email address was not changed. The new email address already exist within the system."
                        Case B2P.Admin.Profile.ProfileManagement.ChangeEmailResults.EmailsMatch
                            lblMessage2.Text = "Email address was not changed. The new and old email addresses match."
                        Case B2P.Admin.Profile.ProfileManagement.ChangeEmailResults.Failed
                            lblMessage2.Text = "Email address was not changed."
                        Case B2P.Admin.Profile.ProfileManagement.ChangeEmailResults.ProfileNotFound
                            lblMessage2.Text = "Email address was not changed. The profile was not found."
                        Case B2P.Admin.Profile.ProfileManagement.ChangeEmailResults.Success
                            lblMessage2.Text = "Email address successfully updated."
                            Updated = True
                            'LoadData()
                    End Select
                Else
                    lblMessage2.Text = "Email address was not changed."
                End If

                If ((txtFirstName.Text <> "" And txtFirstName.Text <> u.FirstName) Or (txtLastName.Text <> "" And txtLastName.Text <> u.LastName)) Then
                    Dim y As B2P.Admin.Profile.ProfileManagement.ChangeNameResults

                    y = B2P.Admin.Profile.ProfileManagement.ChangeFirstLastName(u.UserName,
                                                                                Utility.SafeEncode(txtFirstName.Text.Trim),
                                                                                Utility.SafeEncode(txtLastName.Text.Trim),
                                                                                Convert.ToInt32(Session("SecurityID")))

                    Select Case y
                        Case B2P.Admin.Profile.ProfileManagement.ChangeNameResults.Success
                            lblMessage2.Text = lblMessage2.Text & " Name successfully updated."
                            Updated = True
                        'LoadData()
                        Case B2P.Admin.Profile.ProfileManagement.ChangeNameResults.ProfileNotFound
                            lblMessage2.Text = lblMessage2.Text & " Name was not changed. The profile was not found."
                        Case B2P.Admin.Profile.ProfileManagement.ChangeNameResults.NamesMatch
                            lblMessage2.Text = lblMessage2.Text & " Name was not changed. The new and old names match."
                        Case B2P.Admin.Profile.ProfileManagement.ChangeNameResults.Failed
                            lblMessage2.Text = lblMessage2.Text & " Name was not changed."
                        Case Else
                            lblMessage2.Text = lblMessage2.Text & " Name was not changed."
                    End Select
                Else
                    lblMessage2.Text = lblMessage2.Text & " Name was not changed."
                End If
            End If

            If Updated Then
                LoadData()
            End If

        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during profile update " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub

    Protected Sub btnSendUsername_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSendUsername.Click
        Try
            If B2P.Admin.Profile.ProfileManagement.SendUserName(Convert.ToInt32(Session("Profile_ID").ToString), Convert.ToInt32(Session("SecurityID"))) Then
                lblMessage2.Text = "Username was successfully sent."
            Else
                lblMessage2.Text = "Username was not successfully sent."
            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during send profile user name " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    'Private Sub btnSendActivation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendActivation.Click

    '    If B2P.Profile.ProfileManagement.SendActivationEmail(Convert.ToInt32(Session("Profile_ID")), Convert.ToInt32(Session("SecurityID"))) Then
    '        lblMessage2.Text = "The activation email has been sent."
    '    Else
    '        lblMessage2.Text = "There was a problem sending the activation email."
    '    End If
    'End Sub

#Region " RadGrid1 "

    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand

        Select Case e.CommandName
            'Case "viewTextPayDetail"
            '    Dim tAccount_ID As String = ""
            '    tAccount_ID = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Account_ID").ToString
            '    If IsNumeric(tAccount_ID) Then
            '        LoadTextPayDetails(Convert.ToInt32(tAccount_ID))
            '    Else
            '        'Error b/c it should be a key field for the selected account
            '        lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
            '    End If
            Case "viewProfileAccountDetail"
                Dim tAccount_ID As String = ""
                tAccount_ID = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Account_ID").ToString
                If IsNumeric(tAccount_ID) Then
                    Session("Account_ID") = tAccount_ID
                    Response.Redirect("/profile/ProfileAccountDetail.aspx")
                Else
                    'Error b/c it should be a key field for the selected account
                    lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
                End If
            Case "InitInsert"
                Response.Redirect("profileaccounts.aspx")
            Case Else
                'Do nothing
        End Select
    End Sub

    Protected Sub RadGrid1_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid1.ItemCreated

        If TypeOf e.Item Is GridCommandItem Then
            Dim btncmd As Button
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToExcelButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToCSVButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
        End If

        If TypeOf e.Item Is GridNoRecordsItem Then
            e.Item.OwnerTableView.Visible = False
        End If
    End Sub

    Private Sub RadGrid1_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.PreRender

        If Not UserSecurity.SecurityUserMgmt Then
            For Each dataItem As GridDataItem In RadGrid1.MasterTableView.Items
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Display = False
                'DirectCast(dataItem("DeleteColumn").Controls(0), LinkButton).Attributes.Add("display", "none")
                Dim cmdItem As GridCommandItem = DirectCast(RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)(0), GridCommandItem)
                DirectCast(cmdItem.FindControl("InitInsertButton"), LinkButton).Visible = False
                DirectCast(cmdItem.FindControl("AddNewRecordButton"), Button).Visible = False
            Next
        End If
    End Sub

    Protected Sub RadGrid1_DeleteCommand(ByVal sender As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        Dim ID As String = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Account_ID").ToString()
        Try

            If B2P.Admin.Profile.Accounts.DeleteAccount(CInt(Session("Profile_ID")), CInt(ID), Convert.ToInt32(Session("SecurityID"))) = True Then
                LoadAccounts(ViewState("ClientCode").ToString)
                LoadActivityLog(ViewState("ClientCode").ToString)
            Else
                lblMessage.Text = "There was a problem deleting your account. Please try again."
            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("Bill2Pay Admin", "Error deleting profile account" & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/errors/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Protected Sub RadGrid1_InsertCommand(ByVal sender As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.InsertCommand
        Response.Redirect("profiles.aspx")
    End Sub

#End Region
#Region " RadGrid2 "
    Private Sub RadGrid2_ItemCommand(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.ItemCommand

        'Select Case e.CommandName
        '    'Case "viewTextPayDetail"
        '    '    Dim tAccount_ID As String = ""
        '    '    tAccount_ID = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Account_ID").ToString
        '    '    If IsNumeric(tAccount_ID) Then
        '    '        LoadTextPayDetails(Convert.ToInt32(tAccount_ID))
        '    '    Else
        '    '        'Error b/c it should be a key field for the selected account
        '    '        lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
        '    '    End If
        '    Case "viewProfileAccountDetail"
        '        Dim tAccount_ID As String = ""
        '        tAccount_ID = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Account_ID").ToString
        '        If IsNumeric(tAccount_ID) Then
        '            Session("Account_ID") = tAccount_ID
        '            Response.Redirect("/profile/ProfileAccountDetail.aspx")
        '        Else
        '            'Error b/c it should be a key field for the selected account
        '            lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
        '        End If

        '    Case Else
        '        'Do nothing
        'End Select
    End Sub

    Protected Sub RadGrid2_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid2.ItemCreated

        If TypeOf e.Item Is GridCommandItem Then
            Dim btncmd As Button
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToExcelButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToCSVButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
        End If
    End Sub

    Private Sub RadGrid2_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid2.ItemDataBound
        'Dim x As B2PAdminBLL.TextPay

        'If TypeOf e.Item Is GridDataItem Then
        '    Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
        '    Dim tAccount_ID As String = ""
        '    Dim lnkButton As New LinkButton
        '    tAccount_ID = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Account_ID").ToString
        '    If IsNumeric(tAccount_ID) Then
        '        If B2PAdminBLL.TextPay.VerifyTPEnrollment(Convert.ToInt32(tAccount_ID)) Then
        '            lnkButton = CType(dataItem("column4").Controls(0), LinkButton)
        '            lnkButton.Text = "Text Pay"
        '            'btnCancelTxtPmt.Enabled = True
        '        Else
        '            dataItem("column4").Text = "&nbsp;"
        '        End If
        '    Else
        '        'Error b/c it should be a key field for the selected account
        '        lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
        '    End If
        'End If

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            'Hide expiration date for bank accounts
            If dataItem("column4").Text.ToLower.Contains("1900") Then
                dataItem("column4").Text = "&nbsp;"
            End If
        End If
    End Sub

#End Region
#End Region

#Region " Custom Methods "

    Private Sub LoadAccounts(ByVal tCC As String)
        'Dim x As New B2P.Profile.Account
        Try
            With RadGrid1
                .DataSource = Nothing
                '.DataBind()
            End With

            With RadGrid1
                .DataSource = B2P.Profile.Accounts.ListAccounts(Convert.ToInt32(Session("Profile_ID")), tCC, B2P.Common.Enumerations.TransactionSources.NotSpecified)
                '.DataBind()
            End With


        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during profile details ListAccounts " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub LoadPayMethods(ByVal Profile_ID As String)
        'Dim x As New B2P.Profile.Account

        With RadGrid2
            .DataSource = Nothing
            '.DataBind()
        End With

        With RadGrid2
            .DataSource = B2P.Profile.Wallet.ListWallet(Convert.ToInt32(Session("Profile_ID")))
            '.DataBind()
        End With

    End Sub

    Private Sub LoadActivityLog(ByVal Profile_ID As String)
        'Dim x As New B2P.Profile.Account

        With rdActivity
            .DataSource = Nothing
            '.DataBind()
        End With

        With rdActivity
            .DataSource = B2P.Admin.Profile.ProfileManagement.ListProfileActivity(Convert.ToInt32(Session("Profile_ID")))
            '.DataBind()
        End With

    End Sub

    Private Sub LoadData()
        Try
            If UCase(HttpUtility.HtmlEncode(Request.QueryString("source"))) = "TRANSACTION" Then
                hypBack.NavigateUrl = "/search/"
            Else
                hypBack.NavigateUrl = "/profile/profiles.aspx"
            End If

            'Dim y As New List(Of B2P.Profile.Phone)
            Dim tFullName As String = ""
            litFromEmail.Text = "Payments@Bill2Pay.com"

            Dim x As B2P.Profile.User = New B2P.Profile.User
            x = B2P.Profile.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(Session("Profile_ID")))

            ViewState("ClientCode") = x.ClientCode
            If Not x.ClientCode Is Nothing Then
                Session("AccountClientCode") = x.ClientCode
                Dim client As B2P.Objects.Client = B2P.Objects.Client.GetClient(x.ClientCode)
                If client.Found Then
                    lblClientName.Text = HttpUtility.HtmlEncode(client.ClientName)
                    ViewState("ClientTextNotify") = client.TextNotify
                    ViewState("ClientPaymentNotify") = client.Notifications.PaymentDueNotification
                Else
                    lblClientName.Text = "Unknown"
                End If
            Else
                Session("AccountClientCode") = "B2P"
                lblClientName.Text = "Unknown"
            End If

            With x
                If Not .FirstName Is Nothing Then
                    If .FirstName.Length = 0 Then
                        tFullName = .LastName
                    Else
                        tFullName = String.Format("{0} {1}", .FirstName, .LastName)
                    End If
                End If

                Session("ProfileUserName") = .UserName
                lblProfileID.Text = HttpUtility.HtmlEncode(.Profile_ID.ToString)
                lblUsername.Text = Utility.SafeEncode(.UserName)
                'lblName.Text = HttpUtility.HtmlEncode(tFullName)
                txtFirstName.Text = Utility.SafeEncode(.FirstName)
                txtLastName.Text = Utility.SafeEncode(.LastName)
                txtEmailAddress.Text = Utility.SafeEncode(.EmailAddress)
                lblStatus.Text = HttpUtility.HtmlEncode(.Status.ToString)

                ViewState("CellPhoneStatus") = x.CellPhoneStatus

                If ViewState("ClientTextNotify") = True Then
                    pnlCellPhone.Visible = True
                    If Not IsNothing(.CellPhoneNumber) AndAlso .CellPhoneNumber.Trim <> "" AndAlso .CellPhoneNumber.Trim <> "0" Then
                        ViewState("CellPhoneID") = Utility.SafeEncode(.Phone_ID)
                        lblCellPhone.Text = HttpUtility.HtmlEncode(.CellPhoneNumber.Trim)
                    Else
                        ViewState("CellPhoneID") = 0
                        lblCellPhone.Text = ""
                    End If

                    If .Phone_ID <> 0 Then
                        lnkDeleteCellPhone.Visible = True
                    Else
                        lnkDeleteCellPhone.Visible = False
                    End If
                Else
                    pnlCellPhone.Visible = False
                End If

                If Not IsNothing(.TempCellPhoneNumber) AndAlso .TempCellPhoneNumber.Trim <> "" Then
                    ViewState("CellPhoneID") = Utility.SafeEncode(.Phone_ID)
                    pnlPendingCellPhone.Visible = True
                    lblPendingCellPhone.Text = HttpUtility.HtmlEncode(.TempCellPhoneNumber.Trim)
                    lnkEnterValidationCode.Visible = True
                Else
                    lnkEnterValidationCode.Visible = False
                    pnlPendingCellPhone.Visible = False
                End If

                If Not IsNothing(.TempEmailAddress) AndAlso .TempEmailAddress.Trim <> "" Then
                    pnlPendingEmail.Visible = True
                    lblPendingEmail.Text = HttpUtility.HtmlEncode(.TempEmailAddress.Trim)
                Else
                    pnlPendingEmail.Visible = False
                End If


                Select Case .Status
                    Case B2P.Profile.User.UserStatus.Active
                        btnDisableProfile.Enabled = True
                        lnkResendActivationEmail.Visible = False
                    Case B2P.Profile.User.UserStatus.Disabled, B2P.Profile.User.UserStatus.Deleted
                        btnDisableProfile.Enabled = False
                        lnkResendActivationEmail.Visible = False
                        'btnSendActivation.Enabled = False
                        'btnChangeEmail.Enabled = False
                        btnResetPwd.Enabled = False
                    'btnCancelTxtPmt.Enabled = False
                    'pnlTextPay.Visible = False
                    'ClearTextPayDetails()
                    Case B2P.Profile.User.UserStatus.LockedOut
                        btnDisableProfile.Enabled = True
                        lnkResendActivationEmail.Visible = False
                    Case B2P.Profile.User.UserStatus.PasswordExpired
                        btnDisableProfile.Enabled = True
                        lnkResendActivationEmail.Visible = False
                    Case B2P.Profile.User.UserStatus.Pending
                        lnkResendActivationEmail.Visible = True
                        pnlPendingEmail.Visible = False
                    Case Else
                        btnDisableProfile.Enabled = False
                End Select

                loadCountry()
                loadStates()
                'Default Address
                ddlCountry.SelectedValue = HttpUtility.HtmlEncode(.CountryCode)
                txtAddress1.Text = HttpUtility.HtmlEncode(.Address1)
                txtAddress2.Text = HttpUtility.HtmlEncode(.Address2)
                txtCity.Text = HttpUtility.HtmlEncode(.City)
                ddlState.SelectedValue = HttpUtility.HtmlEncode(.State)
                txtZip.Text = HttpUtility.HtmlEncode(.ZipCode)
                setCountrySpecs()

            End With

            LoadAccounts(x.ClientCode)
            LoadPayMethods(CStr(x.Profile_ID))
            'LoadActivityLog(CStr(x.Profile_ID))
            getNotifications()
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during loading profile detail " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub loadCountry()
        ddlCountry.Items.Clear()
        ddlCountry.DataSource = B2P.Payment.PaymentBase.ListCountries
        ddlCountry.DataTextField = "CountryName"
        ddlCountry.DataValueField = "CountryCode"
        ddlCountry.DataBind()
    End Sub

    Private Sub loadStates()
        'Load states
        ddlState.Items.Clear()

        ddlState.DataSourceID = "XmlDataSource2"
        ddlState.DataBind()
        ddlState.SortItems()
        ddlState.SelectedValue = "FL"

    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        clearAddressFields()
        setCountrySpecs()
    End Sub

    Private Sub clearAddressFields()
        Me.txtAddress1.Text = ""
        Me.txtAddress2.Text = ""
        Me.txtCity.Text = ""
        Me.txtZip.Text = ""
    End Sub

    Private Sub setCountrySpecs()
        Try

            Select Case Me.ddlCountry.SelectedValue
                Case "US"
                    Me.pnlState.Visible = True
                    loadStates()
                    Me.lblBillingZip.Text = "Zip"
                    Me.lblBillingState.Text = "State"
                    Me.txtZip.MaxLength = 5
                    Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.Numbers
                    Me.RegularExpressionValidator3.ValidationExpression = "^[a-zA-Z0-9]{5,9}$"
                Case "CA"
                    Me.pnlState.Visible = True
                    Me.lblBillingState.Text = "Province"
                    loadProvince()
                    Me.lblBillingZip.Text = "Postal Code"
                    Me.txtZip.MaxLength = 7
                    Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters Or AjaxControlToolkit.FilterTypes.Numbers Or AjaxControlToolkit.FilterTypes.UppercaseLetters Or AjaxControlToolkit.FilterTypes.Custom
                    Me.FilteredTextBoxExtender9.ValidChars = " "
                    Me.RegularExpressionValidator3.ValidationExpression = "^[a-zA-Z][0-9][a-zA-Z]\s?[0-9][a-zA-Z][0-9]$"
                Case Else
                    Me.pnlState.Visible = False
                    Me.lblBillingZip.Text = "Postal Code"
                    Me.txtZip.MaxLength = 7
                    Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters Or AjaxControlToolkit.FilterTypes.Numbers Or AjaxControlToolkit.FilterTypes.UppercaseLetters Or AjaxControlToolkit.FilterTypes.Custom
                    Me.FilteredTextBoxExtender9.ValidChars = " "
                    Me.RegularExpressionValidator3.Enabled = False

            End Select
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during setting country specs - profile " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub loadProvince()
        ddlState.Items.Clear()
        ddlState.Items.Add(New RadComboBoxItem("AB - Alberta", "AB"))
        ddlState.Items.Add(New RadComboBoxItem("BC - British Columbia", "BC"))
        ddlState.Items.Add(New RadComboBoxItem("MB - Manitoba", "MB"))
        ddlState.Items.Add(New RadComboBoxItem("NB - New Brunswick", "NB"))
        ddlState.Items.Add(New RadComboBoxItem("NL - Newfoundland And Labrador", "NL"))
        ddlState.Items.Add(New RadComboBoxItem("NS - Nova Scotia", "NS"))
        ddlState.Items.Add(New RadComboBoxItem("NT - Northwest Territories", "NT"))
        ddlState.Items.Add(New RadComboBoxItem("NU - Nunavut", "NU"))
        ddlState.Items.Add(New RadComboBoxItem("ON - Ontario", "ON"))
        ddlState.Items.Add(New RadComboBoxItem("PE - Prince Edward Island", "PE"))
        ddlState.Items.Add(New RadComboBoxItem("QC - Quebec", "QC"))
        ddlState.Items.Add(New RadComboBoxItem("SK - Saskatchewan", "SK"))
        ddlState.Items.Add(New RadComboBoxItem("YT - Yukon", "YT"))
    End Sub
#End Region

    Private Sub btnEmail_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEmail.Click
        modalEmail.Show()
    End Sub

    Private Sub ButtonOkEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOkEmail.Click
        Try
            If Page.IsValid Then
                If HttpUtility.HtmlEncode(txtMessage.Text.Trim).Length <= 2048 Then
                    Dim x As New B2P.Common.Email.CustomerEmail("AdhocAdmin", Session("ClientCode").ToString)

                    With x.EmailMessage
                        .RecipientAddress = HttpUtility.HtmlEncode(txtTo.Text.Trim)
                        .Subject = HttpUtility.HtmlEncode(txtSubject.Text.Trim)
                        .HTMLBody = HttpUtility.HtmlEncode(txtMessage.Text.Trim)
                        .TextBody = HttpUtility.HtmlEncode(txtMessage.Text.Trim)
                        .Parameters.Add(txtSubject.Text.Trim, "Subject")
                        .Parameters.Add(txtMessage.Text.Trim, "Message")
                        x.Reference_ID = CInt(Session("OrderID"))
                    End With
                    If x.SendEmail() Then
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>Email message sent.</p><br />"
                            .ForeColor = Drawing.Color.Green
                        End With
                        'btnSubmitEmail.Enabled = False

                    Else
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>There was a problem completing your request. Please try again.</p><br />"
                        End With
                    End If
                    txtTo.Text = ""
                    txtSubject.Text = ""
                    txtMessage.Text = ""
                Else
                    With lblMessage2
                        .Visible = True
                        .Text = "<p>The email message body was greater than 2048 characters.</p><br />"
                    End With

                End If

            End If

        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during laoding profile detail " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        End Try
    End Sub

    Private Sub ButtonCancelEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancelEmail.Click
        txtTo.Text = ""
        txtSubject.Text = ""
        txtMessage.Text = ""
    End Sub

    Protected Sub rdActivity_ItemCreated(ByVal sender As Object, ByVal e As Global.Telerik.Web.UI.GridItemEventArgs) Handles rdActivity.ItemCreated
        If TypeOf e.Item Is GridCommandItem Then
            Dim btncmd As Button
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToExcelButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToCSVButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Global.Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        LoadAccounts(CStr(ViewState("ClientCode")))
    End Sub


    Private Sub FilteredTextBoxExtender4_Init(sender As Object, e As EventArgs) Handles FilteredTextBoxExtender4.Init
        'allow line break in email message body
        FilteredTextBoxExtender4.ValidChars = FilteredTextBoxExtender4.ValidChars & vbCrLf
    End Sub

    Private Sub btnSubmitCellPhone_Click(sender As Object, e As EventArgs) Handles btnSubmitCellPhone.Click
        Dim errMsg As String = String.Empty
        Try
            Dim p As New B2P.Profile.Phone
            p.PhoneNumber = Utility.SafeEncode(txtCellPhone.Text.Trim)
            p.Profile_ID = lblProfileID.Text
            p.Phone_ID = ViewState("CellPhoneID")

            ' Check if user has exsiting cell phone
            If ViewState("CellPhoneID") <> 0 Then
                Dim cpr As B2P.Profile.Phones.ChangePhoneResults
                cpr = B2P.Profile.Phones.ChangePhoneNumber(p)
                Select Case cpr
                    Case B2P.Profile.Phones.ChangePhoneResults.CellPhoneExists
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>Cell phone exists in the profile system.</p><br />"
                            .ForeColor = Drawing.Color.Red
                        End With
                    Case B2P.Profile.Phones.ChangePhoneResults.Success
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>Cell phone number updated.</p><br />"
                            .ForeColor = Drawing.Color.Green
                        End With
                        LoadData()
                    Case Else
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>Cell phone number update was not successful.</p><br />"
                            .ForeColor = Drawing.Color.Red
                        End With
                End Select
                ' Clear the control contents 
                txtCellPhone.Text = String.Empty
            Else
                p.PhoneType = B2P.Profile.Phone.PhoneTypes.Cell
                Dim apr As B2P.Profile.Phones.AddPhoneResult
                apr = B2P.Profile.Phones.AddPhone(p)
                Select Case apr.Result
                    Case B2P.Profile.Phones.AddPhoneResult.Results.Failed
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>Cell phone number update was not successful.</p><br />"
                            .ForeColor = Drawing.Color.Red
                        End With

                    Case B2P.Profile.Phones.AddPhoneResult.Results.PhoneNumberExists
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>Cell phone exists in the profile system.</p><br />"
                            .ForeColor = Drawing.Color.Red
                        End With
                    Case B2P.Profile.Phones.AddPhoneResult.Results.Success
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>Cell phone number updated.</p><br />"
                            .ForeColor = Drawing.Color.Green
                        End With
                        LoadData()
                    Case Else
                        With lblMessage2
                            .Visible = True
                            .Text = "<p>Cell phone number update was not successful.</p><br />"
                            .ForeColor = Drawing.Color.Red
                        End With
                End Select
                ' Clear the control contents 
                txtCellPhone.Text = String.Empty
            End If
        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                               ex.Source, ex.TargetSite.DeclaringType.Name,
                                               ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        End Try
    End Sub

    Private Sub btnCancelCellPhone_Click(sender As Object, e As EventArgs) Handles btnCancelCellPhone.Click
        ' Clear the validators 
        RegularExpressionValidator2.IsValid = True

        ' Clear the control contents 
        txtCellPhone.Text = String.Empty

        ' Close the popup 
        CellPhoneModalExtender.Hide()
    End Sub
    Private Sub getNotifications()
        Dim errMsg As String = String.Empty
        Try
            Dim ntfOption As B2P.Profile.Notification = B2P.Profile.Notifications.GetNotificationOptions(Session("Profile_ID"))
            ViewState("NotificationID") = ntfOption.Notification_ID

            ' Check the profile email alerts settings
            If ntfOption.ProfileEmail Then
                ckProfileAlertsEmail.Checked = True
            Else
                ckProfileAlertsEmail.Checked = False
            End If

            ' Check the payment email alerts settings
            If ntfOption.PaymentEmail Then
                ckPaymentAlertsEmail.Checked = True
            Else
                ckPaymentAlertsEmail.Checked = False
            End If

            Dim CellPhoneStatus As B2P.Profile.User.EmailPhoneStatus = CType(ViewState("CellPhoneStatus"), B2P.Profile.User.EmailPhoneStatus)
            If ViewState("ClientPaymentNotify") = "True" Then
                pnlPaymentReminders.Visible = True
            Else
                pnlPaymentReminders.Visible = False
            End If

            If ViewState("ClientTextNotify") = "True" Then
                If CellPhoneStatus = B2P.Profile.User.EmailPhoneStatus.Validated Then

                    ckSystemAlertsText.Enabled = True
                    ckProfileAlertsText.Enabled = True
                    ckPaymentAlertsText.Enabled = True
                    lnkEnterValidationCode.Visible = False

                    ' Check the system alerts settings
                    If ntfOption.SystemText Then
                        ckSystemAlertsText.Checked = True
                    Else
                        ckSystemAlertsText.Checked = False
                    End If

                    ' Check the profile text alerts settings
                    If ntfOption.ProfileText Then
                        ckProfileAlertsText.Checked = True
                    Else
                        ckProfileAlertsText.Checked = False
                    End If

                    ' Check the payment text alerts settings
                    If ntfOption.PaymentText Then
                        ckPaymentAlertsText.Checked = True
                    Else
                        ckPaymentAlertsText.Checked = False
                    End If
                Else
                    ckSystemAlertsText.Checked = False
                    ckProfileAlertsText.Checked = False
                    ckPaymentAlertsText.Checked = False
                    ckSystemAlertsText.Enabled = False
                    ckProfileAlertsText.Enabled = False
                    ckPaymentAlertsText.Enabled = False
                End If


            Else
                ckSystemAlertsText.Checked = False
                ckProfileAlertsText.Checked = False
                ckPaymentAlertsText.Checked = False
                ckSystemAlertsText.Enabled = False
                ckProfileAlertsText.Enabled = False
                ckPaymentAlertsText.Enabled = False
            End If


        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub
    Private Sub updateNotifications()
        Dim errMsg As String = String.Empty
        Try
            Dim ntfOption As B2P.Profile.Notification = B2P.Profile.Notifications.GetNotificationOptions(Session("Profile_ID"))
            ntfOption.Profile_ID = Session("Profile_ID")
            ntfOption.Notification_ID = ViewState("NotificationID")

            ' Check the profile email alerts settings
            If ckProfileAlertsEmail.Checked Then
                ntfOption.ProfileEmail = True
            Else
                ntfOption.ProfileEmail = False
            End If

            ' Check the payment email alerts settings
            If ckPaymentAlertsEmail.Checked Then
                ntfOption.PaymentEmail = True
            Else
                ntfOption.PaymentEmail = False
            End If

            Dim CellPhoneStatus As B2P.Profile.User.EmailPhoneStatus = CType(ViewState("CellPhoneStatus"), B2P.Profile.User.EmailPhoneStatus)

            If ViewState("ClientTextNotify") = "True" AndAlso CellPhoneStatus = B2P.Profile.User.EmailPhoneStatus.Validated Then

                ' Check the system alerts settings
                If ckSystemAlertsText.Checked Then
                    ntfOption.SystemText = True
                Else
                    ntfOption.SystemText = False
                End If

                ' Check the profile text alerts settings
                If ckProfileAlertsText.Checked Then
                    ntfOption.ProfileText = True
                Else
                    ntfOption.ProfileText = False
                End If

                ' Check the payment text alerts settings
                If ckPaymentAlertsText.Checked Then
                    ntfOption.PaymentText = True
                Else
                    ntfOption.PaymentText = False
                End If

            End If

            If B2P.Profile.Notifications.UpdateNotificationOptions(ntfOption) Then

            Else

            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub ckPaymentAlertsEmail_CheckedChanged(sender As Object, e As EventArgs) Handles ckPaymentAlertsEmail.CheckedChanged
        updateNotifications()
    End Sub

    Private Sub ckPaymentAlertsText_CheckedChanged(sender As Object, e As EventArgs) Handles ckPaymentAlertsText.CheckedChanged
        updateNotifications()
    End Sub

    Private Sub ckProfileAlertsEmail_CheckedChanged(sender As Object, e As EventArgs) Handles ckProfileAlertsEmail.CheckedChanged
        updateNotifications()
    End Sub

    Private Sub ckProfileAlertsText_CheckedChanged(sender As Object, e As EventArgs) Handles ckProfileAlertsText.CheckedChanged
        updateNotifications()
    End Sub

    Private Sub ckSystemAlertsEmail_CheckedChanged(sender As Object, e As EventArgs) Handles ckSystemAlertsEmail.CheckedChanged
        updateNotifications()
    End Sub

    Private Sub ckSystemAlertsText_CheckedChanged(sender As Object, e As EventArgs) Handles ckSystemAlertsText.CheckedChanged
        updateNotifications()
    End Sub

    Private Sub btnCancelValidationCode_Click(sender As Object, e As EventArgs) Handles btnCancelValidationCode.Click

        ' Clear the control contents 
        txtValidationCode.Text = String.Empty

        ' Close the popup 
        ValidationCodeModal.Hide()
    End Sub

    Private Sub btnSubmitValidationCode_Click(sender As Object, e As EventArgs) Handles btnSubmitValidationCode.Click
        Dim errMsg As String = String.Empty
        Try

            Dim cpr As B2P.Profile.Phones.ChangePhoneResults
            cpr = B2P.Profile.Phones.ValidateCellNumber(Utility.SafeEncode(txtValidationCode.Text.Trim))
            Select Case cpr
                Case B2P.Profile.Phones.ChangePhoneResults.Success
                    LoadData()
                    getNotifications()
                    With lblMessage2
                        .Visible = True
                        .Text = "<p>Validation code accepted.</p><br />"
                        .ForeColor = Drawing.Color.Green
                    End With
                    ' Clear the control contents 
                    txtValidationCode.Text = String.Empty
                Case Else
                    With lblMessage2
                        .Visible = True
                        .Text = "<p>Validation code not accepted.</p><br />"
                        .ForeColor = Drawing.Color.Red
                    End With
                    ' Clear the control contents 
                    txtValidationCode.Text = String.Empty
            End Select

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub

    Private Sub lnkResendActivationEmail_Click(sender As Object, e As EventArgs) Handles lnkResendActivationEmail.Click
        Dim errMsg As String = String.Empty
        Try
            If B2P.Admin.Profile.ProfileManagement.ResendActivationEmail(Convert.ToInt32(Session("Profile_ID")), Convert.ToInt32(Session("SecurityID"))) Then
                With lblMessage2
                    .Visible = True
                    .Text = "<p>Email message sent.</p><br />"
                    .ForeColor = Drawing.Color.Green
                End With
            Else
                With lblMessage2
                    .Visible = True
                    .Text = "<p>There was a problem generating the activation email.</p><br />"
                    .ForeColor = Drawing.Color.Red
                End With
            End If



        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub lnkDeleteCellPhone_Click(sender As Object, e As EventArgs) Handles lnkDeleteCellPhone.Click
        Dim errMsg As String = String.Empty
        Try

            If B2P.Profile.Phones.DeletePhone(Convert.ToInt32(Session("Profile_ID")), ViewState("CellPhoneID"), Convert.ToInt32(Session("SecurityID"))) Then
                With lblMessage2
                    .Visible = True
                    .Text = "<p>Cell phone has been deleted.</p><br />"
                    .ForeColor = Drawing.Color.Green
                End With
                LoadData()
            Else
                With lblMessage2
                    .Visible = True
                    .Text = "<p>There was a problem deleting the cell phone.</p><br />"
                    .ForeColor = Drawing.Color.Red
                End With
            End If



        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub lnkResendValidationEmail_Click(sender As Object, e As EventArgs) Handles lnkResendValidationEmail.Click
        Dim errMsg As String = String.Empty
        Try
            If B2P.Admin.Profile.ProfileManagement.ResendEmailValidation(Convert.ToInt32(Session("Profile_ID")), Convert.ToInt32(Session("SecurityID"))) Then
                With lblMessage2
                    .Visible = True
                    .Text = "<p>Email message sent.</p><br />"
                    .ForeColor = Drawing.Color.Green
                End With
            Else
                With lblMessage2
                    .Visible = True
                    .Text = "<p>There was a problem generating the validation email.</p><br />"
                    .ForeColor = Drawing.Color.Red
                End With
            End If



        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub rdActivity_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdActivity.NeedDataSource
        LoadActivityLog(Session("Profile_ID"))
    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        LoadPayMethods(Session("Profile_ID"))
    End Sub

    Private Sub RadGrid1_DetailTableDataBind(sender As Object, e As GridDetailTableDataBindEventArgs) Handles RadGrid1.DetailTableDataBind
        Dim errMsg As String = String.Empty
        Try
            Dim parentItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If parentItem.Edit Then
                Return
            End If
            Dim a As B2P.Admin.Profile.Account
            Dim phl As List(Of B2P.Admin.Profile.Account.AccountPaymentOption)
            a = B2P.Admin.Profile.Accounts.GetAccount(Session("Profile_ID"), parentItem.OwnerTableView.DataKeyValues(parentItem.ItemIndex)("Account_ID"), B2P.Common.Enumerations.TransactionSources.Portal)
            phl = a.PaymentOption
            e.DetailTableView.DataSource = phl
        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            If e.Item.OwnerTableView.Name = "ChildGrid" Then
                ' Add user friendly payment type description
                Select Case dataItem("PaymentType").Text
                    Case "10"
                        dataItem("PaymentType").Text = "AutoPay"
                    Case "4"
                        dataItem("PaymentType").Text = "Scheduled"
                    Case "3"
                        dataItem("PaymentType").Text = "Recurring"
                End Select
                If dataItem("PaymentType").Text = "0" Then
                    e.Item.Display = False
                    e.Item.Visible = False
                End If
            ElseIf e.Item.OwnerTableView.Name = "ParentGrid" Then
                dataItem("AmountDue").Text = String.Format("{0:$####.00}", Convert.ToDecimal(dataItem("AmountDue").Text))
            End If
        End If

        If RadGrid1.Items.Count = 0 Then
            pnlNoAccounts.Visible = True
            pnlAccounts.Visible = False
        Else
            pnlAccounts.Visible = True
            pnlNoAccounts.Visible = False
        End If
    End Sub
End Class