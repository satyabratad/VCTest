Imports System.Drawing

Public Class menu
    Inherits System.Web.UI.UserControl

    Dim UserSecurity As B2PAdminBLL.Role

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)

        'Main reports tab
        If UserSecurity.ReportScreen Then
            liMainReport.Visible = True
        End If
        If UserSecurity.SecurityScreen Then
            liMainSecurity.Visible = True
        End If
        If UserSecurity.MakeACCPayment Then
            liPayment.Visible = True
            litCounter.Visible = True
            litPinPad.Visible = True
            litPinPadChip.Visible = True
        End If
        If UserSecurity.CallCenterScreen Then
            liPayment.Visible = True
            litCallCenter.Visible = True
        End If

        If UserSecurity.SecurityOfficeMgmt Then
            liOffice.Visible = True
        End If
        If UserSecurity.ReportFinanceClient Then
            litPayout.Visible = True
        End If
        If UserSecurity.ReportFinanceClient Then
            litRecon.Visible = True
        End If
        If UserSecurity.ReportResearchClient Then
            litPayment.Visible = True
        End If
        If Session("ClientCode").ToString = "B2P" Then
            litB2PAdmin.Visible = True
        End If

        If UserSecurity.BlockAccount Then
            liBlockAccount.Visible = True
        End If

    End Sub

End Class