Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI
Imports PeterBlum
Imports System.Threading

Partial Public Class textpaydetail
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
            Else
                lblMessage.Text = "An unexpected error has occurred. Please review the search parameters on the previous page and try again."
            End If
        End If
    End Sub
#End Region

#Region " Control Events "
    Private Sub btnCancelTxtPmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelTxtPmt.Click
        Try

       
            If B2P.Customer.TextPay.CancelTextpayEnrollment(Convert.ToInt32(Session("Profile_ID")), Session("PhoneNumber").ToString, "Canceled By Admin", Convert.ToInt32(Session("SecurityID"))) Then
                lblMessage2.Text = "Text Payments have been successfully deactivated."
                LoadData()
                pnlTextPay.Visible = False
                btnCancelTxtPmt.Enabled = False
            Else
                lblMessage2.Text = "There was an error deactivating text payments."
            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during cancel textpay CancelTextpayEnrollment " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub ButtonOk3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk3.Click
        If Page.IsValid Then
            Try

                Dim x As B2P.Customer.ProfileManagement.ChangeEmailResults

                x = B2P.Customer.ProfileManagement.ChangeEmailAddress(Session("ProfileUserName").ToString, HttpUtility.HtmlEncode(txtEmail.Text.Trim), Convert.ToInt32(Session("SecurityID")))
                Select Case x
                    Case B2P.Customer.ProfileManagement.ChangeEmailResults.EmailAddressExists
                        lblMessage2.Text = "Email address was not changed. The new email address already exist within the system."
                    Case B2P.Customer.ProfileManagement.ChangeEmailResults.EmailsMatch
                        lblMessage2.Text = "Email address was not changed. The new and old email addresses match."
                    Case B2P.Customer.ProfileManagement.ChangeEmailResults.Failed
                        lblMessage2.Text = "Email address was not changed."
                    Case B2P.Customer.ProfileManagement.ChangeEmailResults.ProfileNotFound
                        lblMessage2.Text = "Email address was not changed. The profile was not found."
                    Case B2P.Customer.ProfileManagement.ChangeEmailResults.Success
                        lblMessage2.Text = "Email address successfully updated."
                        LoadData()
                End Select

            Catch te As ThreadAbortException

            Catch ex As Exception
                B2P.Common.Logging.LogError("B2P Admin", "Error during ButtonOk3_Click ChangeEmailAddress " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/error/")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End Try
        End If
    End Sub

    Private Sub btnDisableProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisableProfile.Click
        If B2P.Customer.ProfileManagement.DeleteProfile(Convert.ToInt32(Session("Profile_ID")), Convert.ToInt32(Session("SecurityID"))) Then
            lblMessage2.Text = "The profile has been disabled."
            LoadData()
        Else
            lblMessage2.Text = "There was an error disabling the profile."
        End If

    End Sub

    Private Sub btnResetPwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetPwd.Click
        If B2P.Customer.ProfileManagement.ResetPassword(Session("ProfileUserName").ToString, Convert.ToInt32(Session("SecurityID"))) Then
            lblMessage2.Text = "Password reset was successful."
        Else
            lblMessage2.Text = "Password reset was unsuccessful."
        End If
    End Sub

    Private Sub btnSendActivation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendActivation.Click

        If B2P.Customer.ProfileManagement.SendActivationEmail(Convert.ToInt32(Session("Profile_ID")), Convert.ToInt32(Session("SecurityID"))) Then
            lblMessage2.Text = "The activation email has been sent."
        Else
            lblMessage2.Text = "There was a problem sending the activation email."
        End If
    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand

        Select Case e.CommandName
            Case "viewTextPayDetail"
                Dim tAccount_ID As String = ""
                tAccount_ID = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Account_ID").ToString
                If IsNumeric(tAccount_ID) Then
                    LoadTextPayDetails(Convert.ToInt32(tAccount_ID))
                Else
                    'Error b/c it should be a key field for the selected account
                    lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
                End If
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
    End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        'Dim x As B2PAdminBLL.TextPay

        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim tAccount_ID As String = ""
            Dim lnkButton As New LinkButton
            tAccount_ID = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Account_ID").ToString
            If IsNumeric(tAccount_ID) Then
                If B2PAdminBLL.TextPay.VerifyTPEnrollment(Convert.ToInt32(tAccount_ID)) Then
                    lnkButton = CType(dataItem("column4").Controls(0), LinkButton)
                    lnkButton.Text = "Text Pay"
                    btnCancelTxtPmt.Enabled = True
                Else
                    dataItem("column4").Text = "&nbsp;"
                End If
            Else
                'Error b/c it should be a key field for the selected account
                lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
            End If
        End If
    End Sub
#End Region

#Region " Custom Methods "
    Private Sub CheckSession()
        If Session("SessionID") Is Nothing Then
            Response.Redirect("/welcome")
        End If
    End Sub

    Private Sub LoadAccounts(ByVal tCC As String)
        Dim x As New B2P.Customer.Account
        With RadGrid1
            .DataSource = Nothing
            .DataBind()
        End With

        With RadGrid1
            .DataSource = B2P.Customer.Accounts.ListAccounts(Convert.ToInt32(Session("Profile_ID")), tCC, B2P.Common.Enumerations.TransactionSources.NotSpecified)
            .DataBind()
        End With

    End Sub

    Private Sub LoadData()
        Try
            Dim x As B2P.Customer.User
            Dim y As New List(Of B2P.Customer.Phone)
            Dim tFullName As String = ""
            x = New B2P.Customer.User
            Dim myID As String = Session("Profile_ID").ToString
            x = B2P.Customer.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(Session("Profile_ID").ToString))
            If Not x.ClientCode Is Nothing Then
                Dim client As B2P.Objects.Client = B2P.Objects.Client.GetClient(x.ClientCode)
                If client.Found Then
                    lblClientName.Text = HttpUtility.HtmlEncode(client.ClientName)
                Else
                    lblClientName.Text = "Unknown"
                End If
            Else
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
                lblUsername.Text = HttpUtility.HtmlEncode(.UserName)
                lblName.Text = HttpUtility.HtmlEncode(tFullName)
                lblEmail.Text = HttpUtility.HtmlEncode(.EmailAddress)
                lblStatus.Text = HttpUtility.HtmlEncode(.Status.ToString)
                Select Case .Status
                    Case B2P.Customer.User.UserStatus.Active

                        btnSendActivation.Enabled = False
                    Case B2P.Customer.User.UserStatus.Disabled, B2P.Customer.User.UserStatus.Deleted
                        btnDisableProfile.Enabled = False
                        btnSendActivation.Enabled = False
                        btnChangeEmail.Enabled = False
                        btnResetPwd.Enabled = False
                        btnCancelTxtPmt.Enabled = False
                        pnlTextPay.Visible = False
                        ClearTextPayDetails()
                    Case B2P.Customer.User.UserStatus.Pending

                    Case Else
                        btnDisableProfile.Enabled = False
                        btnSendActivation.Enabled = False
                End Select

                y = B2P.Customer.Phones.ListPhones(Convert.ToInt32(Session("Profile_ID")))
                lblPhoneNumber.Text = ""
                If Not y Is Nothing Then
                    If y.Count > 0 Then
                        Session("PhoneNumber") = y.Item(0).PhoneNumber
                        For Each p As B2P.Customer.Phone In y
                            If IsNumeric(p.PhoneNumber) And p.PhoneNumber.Length = 11 Then
                                If lblPhoneNumber.Text.Trim.Length > 0 Then
                                    lblPhoneNumber.Text = lblPhoneNumber.Text & "<BR>"
                                End If
                                lblPhoneNumber.Text = lblPhoneNumber.Text & String.Format("({0}) {1}-{2}", HttpUtility.HtmlEncode(p.PhoneNumber).Substring(1, 3), HttpUtility.HtmlEncode(p.PhoneNumber).Substring(4, 3), HttpUtility.HtmlEncode(p.PhoneNumber).Substring(7, 4))
                            Else
                                lblPhoneNumber.Text = lblPhoneNumber.Text & HttpUtility.HtmlEncode(p.PhoneNumber)
                            End If
                        Next
                    Else
                        Session("PhoneNumber") = ""
                    End If
                Else
                    Session("PhoneNumber") = ""
                End If
            End With

            LoadAccounts(x.ClientCode)
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during textpay details ListAccounts " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub ClearTextPayDetails()
        ltlEnrollTxtPay.Text = ""
        lblPaymentMethod.Text = ""
        lblPaymentDate.Text = ""
        lblOtherInfoTitle.Text = ""
        lblOtherInfoData.Text = ""
        lblNextPaymentDate.Text = ""
        lblPIN.Text = ""
        lblPhActivated.Text = ""
        lblPaymentAmount.Text = ""
        lblPrenotice.Text = ""

        pnlTextPay.Visible = False
    End Sub

    Private Sub LoadTextPayDetails(ByVal iAccount_ID As Int32)
        Try

            Dim x As New B2PAdminBLL.TextPay

            With x
                .Account_ID = iAccount_ID
                .Profile_ID = Convert.ToInt32(Session("Profile_ID"))
                .GetTextPayDetails()
                lblPaymentMethod.Text = HttpUtility.HtmlEncode(.PaymentMethod)
                lblPaymentDate.Text = HttpUtility.HtmlEncode(.PaymentDate)
                If .IsACH Then
                    lblOtherInfoTitle.Text = "Routing Number:"
                    lblOtherInfoData.Text = HttpUtility.HtmlEncode(.RoutingNumber)
                Else
                    lblOtherInfoTitle.Text = "Expires:"
                    lblOtherInfoData.Text = HttpUtility.HtmlEncode(.ExpirationDate)
                End If
                lblNextPaymentDate.Text = HttpUtility.HtmlEncode(.NextPaymentDate)
                lblPIN.Text = .SMSPIN
                lblPhActivated.Text = HttpUtility.HtmlEncode(.PhoneActivated)
                lblPaymentAmount.Text = HttpUtility.HtmlEncode(.PaymentAmount)
                lblPrenotice.Text = HttpUtility.HtmlEncode(.LastTextPrenoticeDate)
            End With
            pnlTextPay.Visible = True

        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during textpay details GetTextPayDetails " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub
#End Region

End Class