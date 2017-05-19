Imports System.Drawing
Imports System.Xml
Public Class BlockDetail
    Inherits baseclass

    Dim UserSecurity As B2PAdminBLL.Role
    Dim profileAccount As New B2PAdminBLL.BlockAccount.ProfileAccount
    Dim TypeEdit As String

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

        If Not UserSecurity.BlockAccount Then
            Response.Redirect("/error/default.aspx?type=security")
        End If

        TypeEdit = UCase(HttpUtility.HtmlEncode(Request.QueryString("source")))

        If Not IsPostBack Then
            If Not Session("Block_ID") Is Nothing Then
                LoadData()
            Else
                lblMessage.Text = "An unexpected error has occurred. Please review the search parameters on the previous page and try again."
                lblMessage.ForeColor = Color.Red
                lblMessage.Font.Italic = True
            End If
        End If

    End Sub

#End Region

#Region " Private Methods "


    Private Sub LoadData()
        Dim Block_ID As String = Session("Block_ID").ToString()
        Dim ds As New DataSet

        'retrieve data for block id
        Dim ba As New B2PAdminBLL.BlockAccount.Account
        ba = B2PAdminBLL.BlockAccount.RetrieveBlockedAccount(Block_ID)
        ds = B2PAdminBLL.BlockAccount.RetrieveBlockedAccountHistory(Block_ID)

        RadGrid1.DataSource = ds
        RadGrid1.DataBind()
        Session("UserClientCode") = ba.ClientCode

        If CheckProfileAccount() Then
            If profileAccount.Profile_ID > 0 Then
                'Session("Profile_ID") = profileAccount.Profile_ID

                If profileAccount.AccountPayment_ID > 0 Then
                    pnlProfile.Visible = True
                    If profileAccount.PaymentType = "C" Then
                        lblAutoPay.Text = "This account is setup for automated payment by Credit/Debit card. " + vbCrLf + "Blocking this account for Credit/Debit card will cancel all future payments."
                    Else
                        lblAutoPay.Text = "This account is setup for automated payment by eCheck." + vbCrLf + "Blocking this account for eCheck will cancel all future payments."
                    End If
                    lblAutoPay.ForeColor = Color.Red
                Else
                    lblAutoPay.Text = ""
                End If
            End If
        End If

        lblAccount.Text = String.Format("{0} - {1} - {2}", HttpUtility.HtmlEncode(ba.AccountNumber1), HttpUtility.HtmlEncode(ba.AccountNumber2), HttpUtility.HtmlEncode(ba.AccountNumber3))
        txtEmailAddress.Text = HttpUtility.HtmlEncode(ba.EmailAddress.ToString())
        If ba.BlockACH Then
            cbECheck.Checked = True
        Else
            cbECheck.Checked = False
        End If
        If ba.BlockCreditCard Then
            cbCredit.Checked = True
        Else
            cbCredit.Checked = False
        End If


        If IsNothing(ba.ReleaseBy) Then     'Account has active block
            If TypeEdit = "EDIT" Then
                txtEmailAddress.ReadOnly = True
                cbECheck.Enabled = True
                cbCredit.Enabled = True
                txtEmailAddress.ReadOnly = True
                cbSendEmail.Visible = False
                RequiredTextValidator1.Enabled = False
                lblSendEmail.Visible = False
                pnlComment.Visible = False
                pnlEdit.Visible = True
                pnlUnBlock.Visible = False
                pnlSearch.Visible = True
                pnlAdd.Visible = False
            ElseIf TypeEdit = "ADD" Then        'were coming from the add screen
                txtEmailAddress.ReadOnly = True
                cbECheck.Enabled = True
                cbCredit.Enabled = True
                cbSendEmail.Visible = False
                RequiredTextValidator1.Enabled = False
                lblSendEmail.Visible = False
                pnlComment.Visible = False
                pnlEdit.Visible = True
                pnlUnBlock.Visible = False
                pnlSearch.Visible = False
                pnlAdd.Visible = True
            Else
                txtEmailAddress.ReadOnly = False
                cbECheck.Enabled = False
                cbCredit.Enabled = False
                cbSendEmail.Visible = True
                RequiredTextValidator1.Enabled = True
                lblSendEmail.Visible = True
                pnlComment.Visible = True
                pnlEdit.Visible = False
                pnlUnBlock.Visible = True
                pnlSearch.Visible = True
                pnlAdd.Visible = False
                pnlProfile.Visible = False
            End If
        Else

            txtEmailAddress.ReadOnly = True
            cbECheck.Enabled = False
            cbCredit.Enabled = False
            cbSendEmail.Visible = False
            RequiredTextValidator1.Enabled = False
            lblSendEmail.Visible = False
            pnlProfile.Visible = False
            pnlSearch.Visible = True
            'everything read only
            'set update/unblock buttons inactive
            'user can only return to search screen
        End If

    End Sub

    Private Sub SaveChanges()
        Dim ba As New B2PAdminBLL.BlockAccount.Account
        Dim Block_ID As String = Session("Block_ID").ToString()

        'retrieve data for block id
        ba = B2PAdminBLL.BlockAccount.RetrieveBlockedAccount(Block_ID)

        ba.BlockAccount_ID = Block_ID
        ba.BlockACH = cbECheck.Checked
        ba.BlockCreditCard = cbCredit.Checked

        If B2PAdminBLL.BlockAccount.UpdateAccountBlock(ba, Convert.ToInt32(Session("SecurityID"))) Then
            If CheckProfileAccount() Then
                If profileAccount.AccountPayment_ID > 0 Then
                    If ((profileAccount.PaymentType = "C" And cbCredit.Checked) Or (profileAccount.PaymentType = "A" And cbECheck.Checked)) Then
                        B2PAdminBLL.BlockAccount.CancelProfileAccountPayment(profileAccount, ba, Convert.ToInt32(Session("SecurityID")))
                    End If
                End If
            End If

            lblMessage.Text = "Block Account successfully updated"
            lblMessage.ForeColor = Color.Blue
            lblMessage.Font.Italic = True
            pnlEdit.Visible = False
            pnlUnBlock.Visible = False
        Else
            lblMessage.Text = "Block Account not successfully updated"
            lblMessage.ForeColor = Color.Red
            lblMessage.Font.Italic = True
        End If

    End Sub

    Private Sub UnblockAccount()
        Dim pa As New B2PAdminBLL.BlockAccount.ProfileAccount
        Dim ba As New B2PAdminBLL.BlockAccount.Account

        ba = B2PAdminBLL.BlockAccount.RetrieveBlockedAccount(Session("Block_ID").ToString())
        If Not IsNothing(Session("BlockProfile")) Then
            pa = Session("BlockProfile")
        End If

        'ba.BlockAccount_ID = Session("Block_ID").ToString()
        ba.BlockComments = HttpUtility.HtmlEncode(txtComment.Text)
        If cbSendEmail.Checked = True Then
            ba.EmailAddress = HttpUtility.HtmlEncode(txtEmailAddress.Text)
        End If

        If B2PAdminBLL.BlockAccount.RemoveAccountBlock(ba, cbSendEmail.Checked, Convert.ToInt32(Session("SecurityID"))) Then
            lblMessage.Text = "Account successfully unblocked"
            lblMessage.ForeColor = Color.Blue
            lblMessage.Font.Italic = True
            pnlEdit.Visible = False
            pnlUnBlock.Visible = False
            If cbSendEmail.Checked And pa.Profile_ID > 0 Then
                SendText()
            End If
        Else
            lblMessage.Text = "Block Account not successfully updated"
            lblMessage.ForeColor = Color.Red
            lblMessage.Font.Italic = True
        End If
    End Sub

    Private Function CheckProfileAccount() As Boolean
        Dim HasProfile As Boolean = False
        Dim Block_ID As String = Session("Block_ID").ToString()

        'retrieve data for block id
        Dim ba As New B2PAdminBLL.BlockAccount.Account
        ba = B2PAdminBLL.BlockAccount.RetrieveBlockedAccount(Block_ID)

        Dim productSelected As String = HttpUtility.HtmlEncode(ba.ProductName)
        Dim SearchResults As New B2PAdminBLL.BlockAccount.ProfileAccount

        'Session("BlockProfile") = Nothing

        Dim x As New B2PAdminBLL.BlockAccount.SearchCriteria
        x.ClientCode = HttpUtility.HtmlEncode(Trim(ba.ClientCode))
        x.ProductName = productSelected
        x.AccountNumber1 = HttpUtility.HtmlEncode(Trim(ba.AccountNumber1))
        ViewState("AccountNumber1") = x.AccountNumber1
        ViewState("AccountNumber2") = x.AccountNumber2
        ViewState("AccountNumber3") = x.AccountNumber3
        ViewState("ProductName") = x.ProductName

        If Trim(ba.AccountNumber2) <> "" Then
            x.AccountNumber2 = HttpUtility.HtmlEncode(Trim(ba.AccountNumber2))
        Else
            x.AccountNumber2 = ""
        End If
        If Trim(ba.AccountNumber3) <> "" Then
            x.AccountNumber3 = HttpUtility.HtmlEncode(Trim(ba.AccountNumber3))
        Else
            x.AccountNumber3 = ""
        End If

        SearchResults = B2PAdminBLL.BlockAccount.CheckProfileAccount(x)

        If SearchResults.Profile_ID > 0 Then
            profileAccount = SearchResults
            Session("BlockProfile") = profileAccount
            HasProfile = True
        End If

        Return HasProfile
    End Function
    Private Sub SendText()
        Dim errMsg As String = String.Empty

        Dim x As B2P.Profile.User = New B2P.Profile.User
        profileAccount = CType(Session("BlockProfile"), B2PAdminBLL.BlockAccount.ProfileAccount)
        x = B2P.Profile.ProfileManagement.GetUserByProfile_ID(profileAccount.Profile_ID)
        Dim ClientOptions As B2P.Objects.Client = B2P.Objects.Client.GetClient(x.ClientCode)

        Try

            If ClientOptions.TextNotify And x.CellPhoneStatus =B2P.Profile.User.EmailPhoneStatus.Validated Then
                Dim ntfOption As B2P.Profile.Notification = B2P.Profile.Notifications.GetNotificationOptions(profileAccount.Profile_ID)
                If ntfOption.SystemText Then
                    Dim ct As New B2P.Common.Messaging.CustomerText("UnBlockAccount", Session("UserClientCode"))
                    'ct.textMessage.AddParameter(ClientOptions.ClientName, "ClientName")
                    ct.textMessage.AddParameter(ViewState("ProductName"), "ProductName")
                    If cbECheck.Checked And cbCredit.Checked Then
                        ct.textMessage.AddParameter("eCheck and Credit/Debit Card", "PaymentMethod")
                    ElseIf cbECheck.Checked Then
                        ct.textMessage.AddParameter("eCheck", "PaymentMethod")
                    ElseIf cbCredit.Checked Then
                        ct.textMessage.AddParameter("Credit/Debit Card", "PaymentMethod")
                    End If
                    ct.textMessage.AddParameter(String.Format("{0} {1} {2}", ViewState("AccountNumber1"), ViewState("AccountNumber2"), ViewState("AccountNumber3")), "ACCOUNTNUMBER")
                    ct.textMessage.PhoneNumber = x.CellPhoneNumber
                    ct.Reference_ID = x.Profile_ID
                    ct.ReferenceType = 10
                    ct.SendText()
                End If

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
#End Region

#Region " Events "
    Protected Sub btnSave_Click(sender As Object, e As ImageClickEventArgs) Handles btnSave.Click
        SaveChanges()
    End Sub

    Protected Sub btnUnBlock_Click(sender As Object, e As ImageClickEventArgs) Handles btnUnBlock.Click
        UnblockAccount()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As ImageClickEventArgs) Handles btnCancel.Click
        Response.Redirect("~/BlockAccount/SearchBlock.aspx")
    End Sub

#End Region
End Class