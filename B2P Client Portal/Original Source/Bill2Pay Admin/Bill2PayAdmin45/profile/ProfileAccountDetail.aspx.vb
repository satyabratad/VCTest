Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI
Imports PeterBlum
Imports System.Threading

Partial Public Class ProfileAccountDetail
    Inherits baseclass
    Dim UserSecurity As B2PAdminBLL.Role
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim ds As New DataSet
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
                getStatementAlerts()
                GetData()
            Else
                lblMessage.Text = "An unexpected error has occurred. Please review the search parameters on the previous page and try again."
                lblMessage.ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

#Region " Custom Methods "
    Private Sub getStatementAlerts()
        Dim errMsg As String = String.Empty
        Try
            ' Set the parameters for the selected account
            Dim stAccount As B2P.Profile.Account = B2P.Profile.Accounts.GetAccount(CInt(Session("Profile_ID")), Session("Account_ID"), B2P.Common.Enumerations.TransactionSources.Portal)
            Dim Params As New B2P.Profile.Statements.SearchParameters
            Params.ClientCode = Session("AccountClientCode")
            Params.ProductName = stAccount.ProductName
            Params.AccountNumber1 = stAccount.AccountNumber1
            If stAccount.AccountNumber2.Length > 0 Then
                Params.AccountNumber2 = stAccount.AccountNumber2
            End If
            If stAccount.AccountNumber3.Length > 0 Then
                Params.AccountNumber3 = stAccount.AccountNumber3
            End If

            Dim p As New B2P.Profile.Statements.Statement(Params, CInt(Session("Profile_ID")))
            Dim s As New B2P.Profile.Statements.StatementOptions
            s = p.RetrieveClientStatementOptions()

            rdStatements.Items.Clear()
            ' If these both come back as False, hide the statement notify options
            If s.DisplayPaper = False AndAlso s.DisplayEnotify = False Then
                pnlStatementsNotify.Visible = False
            Else
                pnlStatementsNotify.Visible = True
                If s.DisplayPaper Then
                    rdStatements.Items.Add(New ListItem("&nbsp;Mail Only", "Mail"))
                    If Not IsNothing(s.PaperValue) AndAlso s.PaperValue.ToUpper = "T" Then
                        rdStatements.SelectedValue = "Mail"
                        ckStatementAlertsText.Enabled = False
                    End If
                End If

                If s.DisplayEnotify Then
                    rdStatements.Items.Add(New ListItem("&nbsp;Electronic/Email Only", "Electronic"))
                    If Not IsNothing(s.EnotifyValue) AndAlso s.EnotifyValue.ToUpper = "T" Then
                        rdStatements.SelectedValue = "Electronic"
                        ckStatementAlertsText.Enabled = True
                    End If
                End If

                If s.DisplayBoth Then
                    rdStatements.Items.Add(New ListItem("&nbsp;Both Mail and Electronic/Email", "Both"))
                    If Not IsNothing(s.BothValue) AndAlso s.BothValue.ToUpper = "T" Then
                        rdStatements.SelectedValue = "Both"
                        ckStatementAlertsText.Enabled = True
                    End If
                End If

                If rdStatements.SelectedValue = "" Then
                    ckStatementAlertsText.Enabled = False
                End If
            End If

            Dim client As B2P.Objects.Client = B2P.Objects.Client.GetClient(Session("AccountClientCode"))
            Dim ProfileUser As New B2P.Profile.User
            ProfileUser = B2P.Profile.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(Session("Profile_ID").ToString))

            If client.TextNotify AndAlso s.DisplayText AndAlso ProfileUser.CellPhoneStatus = B2P.Profile.User.EmailPhoneStatus.Validated Then
                pnlText.Visible = True
                If Not IsNothing(s.TextValue) AndAlso s.TextValue.ToUpper = "T" Then
                    ckStatementAlertsText.Checked = True
                Else
                    ckStatementAlertsText.Checked = False
                End If
            Else
                pnlText.Visible = False
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                               ex.Source, ex.TargetSite.DeclaringType.Name,
                                               ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub

    Private Sub LoadData()
        Try

            Dim x As B2P.Profile.User
            'Dim y As New List(Of B2P.Profile.Phone)
            Dim tFullName As String = ""
            x = New B2P.Profile.User
            x = B2P.Profile.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(Session("Profile_ID").ToString))
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
                lblUsername.Text = HttpUtility.HtmlEncode(.UserName)
                lblName.Text = HttpUtility.HtmlEncode(tFullName)

            End With

            LoadAccount(HttpUtility.HtmlEncode(CStr(x.Profile_ID)), HttpUtility.HtmlEncode(Session("Account_ID").ToString()))
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during loading data for profile account detail " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub LoadAccount(ByVal profile_ID As String, ByVal account_ID As String)
        Try
            Dim a As B2P.Admin.Profile.Account
            Dim x As New List(Of B2P.Admin.Profile.Account.AccountPaymentOption)
            Dim y As New B2P.Admin.Profile.Account.WalletItem
            a = B2P.Admin.Profile.Accounts.GetAccount(CInt(profile_ID), CInt(account_ID), B2P.Common.Enumerations.TransactionSources.NotSpecified)
            x = a.PaymentOption

            With a
                'account data
                lblProduct.Text = .ProductName.ToString()
                lblAcct1.Text = .AccountNumber1.ToString()
                lblAcct2.Text = .AccountNumber2.ToString()
                lblAcct3.Text = .AccountNumber3.ToString()
                lblNickName.Text = .NickName.ToString()
            End With


            If x.Count > 0 Then
                For i As Integer = x.Count - 1 To 0 Step -1
                    If x(i).Type = 4 Then
                        x.RemoveAt(i)
                    End If
                Next

                If x.Count > 0 Then
                    If x.Count = 1 And x.Item(0).Type = 0 Then
                        pnlAutoPay.Visible = False
                        pnlNoAutoPay.Visible = True
                    Else
                        pnlAutoPay.Visible = True
                        pnlNoAutoPay.Visible = False
                        For Each item In x
                            ViewState("AccountPaymentID") = item.AccountPayment_ID
                            lblType.Text = item.TypeName.ToString()
                            If item.Interval.ToString.Length > 0 Then
                                lblInterval.Text = item.Interval.ToString()
                            End If
                            lblNextPayDate.Text = ""
                            lblNexPayAmt.Text = ""
                            If IsNothing(item.PaymentEvent) = False Then
                                btnCancelPayment.Visible = True
                                If IsNothing(item.PaymentEvent.NextPaymentDate) = False Then
                                    lblNextPayDate.Text = CDate(item.PaymentEvent.NextPaymentDate).ToString("d")
                                    lblNexPayAmt.Text = item.PaymentEvent.Amount.ToString("c")
                                End If
                            Else
                                btnCancelPayment.Visible = False
                            End If
                        Next
                    End If
                Else
                        pnlAutoPay.Visible = False
                    pnlNoAutoPay.Visible = True
                End If
            End If




        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during loading account data for profile account detail " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub
    Private Sub updateStatementNotifications()
        Dim errMsg As String = String.Empty
        Try
            Dim stAccount As B2P.Profile.Account = B2P.Profile.Accounts.GetAccount(CInt(Session("Profile_ID")), Session("Account_ID"), B2P.Common.Enumerations.TransactionSources.Portal)
            Dim Params As New B2P.Profile.Statements.SearchParameters
            Params.ClientCode = Session("AccountClientCode")
            Params.ProductName = stAccount.ProductName
            Params.AccountNumber1 = stAccount.AccountNumber1
            If stAccount.AccountNumber2.Length > 0 Then
                Params.AccountNumber2 = stAccount.AccountNumber2
            End If
            If stAccount.AccountNumber3.Length > 0 Then
                Params.AccountNumber3 = stAccount.AccountNumber3
            End If

            Dim p As New B2P.Profile.Statements.Statement(Params, CInt(Session("Profile_ID")))
            Dim s As New B2P.Profile.Statements.StatementOptions

            s = p.RetrieveClientStatementOptions

            If rdStatements.SelectedValue = "Mail" Then
                s.PaperValue = "T"
                ckStatementAlertsText.Checked = False
                ckStatementAlertsText.Enabled = False
            Else
                s.PaperValue = "F"
            End If

            If rdStatements.SelectedValue = "Electronic" Then
                s.EnotifyValue = "T"
                ckStatementAlertsText.Enabled = True
            Else
                s.EnotifyValue = "F"
            End If

            If rdStatements.SelectedValue = "Both" Then
                s.BothValue = "T"
                ckStatementAlertsText.Enabled = True
            Else
                s.BothValue = "F"
            End If

            Dim client As B2P.Objects.Client = B2P.Objects.Client.GetClient(Session("AccountClientCode"))
            Dim ProfileUser As New B2P.Profile.User
            ProfileUser = B2P.Profile.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(Session("Profile_ID").ToString))

            If client.TextNotify AndAlso s.DisplayText AndAlso ProfileUser.CellPhoneStatus = B2P.Profile.User.EmailPhoneStatus.Validated Then
                If ckStatementAlertsText.Checked = True Then
                    s.TextValue = "T"
                Else
                    s.TextValue = "F"
                End If
            End If

            p.SaveOptions(CInt(Session("Profile_ID")), s)
            With lblMessage
                .Visible = True
                .Text = "<p>Statement options updated.</p><br />"
                .ForeColor = Drawing.Color.Green
            End With

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                               ex.Source, ex.TargetSite.DeclaringType.Name,
                                               ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub
#End Region


    Private Sub ckStatementAlertsText_CheckedChanged(sender As Object, e As EventArgs) Handles ckStatementAlertsText.CheckedChanged
        updateStatementNotifications()
    End Sub
    Private Sub rdStatements_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdStatements.SelectedIndexChanged
        updateStatementNotifications()
    End Sub
    Protected Function formatAmount(ByVal Amount As String) As String

        Return String.Format("{0:$####.00}", Convert.ToDecimal(Amount))

    End Function

    Protected Function formatDate(ByVal PaymentDate As String) As String

        Return Convert.ToDateTime(PaymentDate).ToString("MM/dd/yyyy")

    End Function

    Private Sub GetData()
        Dim errMsg As String = String.Empty
        Try
            Dim phl As List(Of B2P.Admin.Profile.Account.AccountPaymentOption)
            phl = B2P.Admin.Profile.Accounts.GetAccount(Session("Profile_ID"), Session("Account_ID"), B2P.Common.Enumerations.TransactionSources.Portal).PaymentOption

            ' Remove everything that's not scheduled
            For i As Integer = phl.Count - 1 To 0 Step -1
                If phl(i).Type <> 4 Then
                    phl.RemoveAt(i)
                End If
            Next

            If phl.Count > 0 Then
                pnlScheduledPayments.Visible = True
                pnlNoScheduledPayments.Visible = False
                gvScheduledPayments.DataSource = phl
                gvScheduledPayments.DataBind()

            Else
                pnlScheduledPayments.Visible = False
                pnlNoScheduledPayments.Visible = True
                'psmStatusMessage.ToggleStatusMessage("No scheduled payments found.", StatusMessageType.Danger, StatusMessageSize.Normal, True, True)
            End If
        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                           ex.Source, ex.TargetSite.DeclaringType.Name,
                                           ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Profile --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Protected Sub gvScheduledPayments_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvScheduledPayments.PageIndexChanging
        gvScheduledPayments.PageIndex = e.NewPageIndex
        GetData()
    End Sub
    Private Sub gvScheduledPayments_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvScheduledPayments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = B2P.Common.Encryption.EncryptPassword(e.Row.Cells(0).Text)
        End If
    End Sub

    Private Sub gvScheduledPayments_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvScheduledPayments.RowDeleting
        Dim errMsg As String = String.Empty
        Try

            If B2P.Admin.Profile.Accounts.CancelPaymentOption(Session("Profile_ID"), Session("Account_ID"), CInt(gvScheduledPayments.DataKeys(e.RowIndex).Value), Session("SecurityID")) Then
                GetData()
                Dim phl As List(Of B2P.Admin.Profile.Account.AccountPaymentOption)
                phl = B2P.Admin.Profile.Accounts.GetAccount(Session("Profile_ID"), Session("Account_ID"), B2P.Common.Enumerations.TransactionSources.Portal).PaymentOption
                ' Remove everything that's not scheduled
                For i As Integer = phl.Count - 1 To 0 Step -1
                    If phl(i).Type <> 4 Then
                        phl.RemoveAt(i)
                    End If
                Next
                If phl.Count > 0 Then
                    lblMessage.Text = "Scheduled payment deleted."
                    lblMessage.ForeColor = Drawing.Color.Green
                Else
                    lblMessage.Text = "Scheduled payment deleted."
                    lblMessage.ForeColor = Drawing.Color.Green
                End If

            Else
                lblMessage.Text = "There was a problem deleting your payment. Please try again."
                lblMessage.ForeColor = Drawing.Color.Red
            End If


        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                           ex.Source, ex.TargetSite.DeclaringType.Name,
                                           ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("Profile --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub btnCancelPayment_Click(sender As Object, e As EventArgs) Handles btnCancelPayment.Click
        'cancel the payment option
        Try
            If B2P.Admin.Profile.Accounts.CancelPaymentOption(Session("Profile_ID"), Session("Account_ID"), ViewState("AccountPaymentID"), Session("SecurityID")) Then
                LoadData()
            Else
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = "Payment was not canceled."
            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during profile cancel payment " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub
End Class

