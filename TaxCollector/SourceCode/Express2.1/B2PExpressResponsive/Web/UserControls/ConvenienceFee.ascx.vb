Namespace B2P.PaymentLanding.Express.Web
    Public Class ConvenienceFee : Inherits System.Web.UI.UserControl

#Region "::: Properties :::"

        Public Property AgreedToTerms As Boolean
            Get
                Return chkFeeAgree.Checked
            End Get
            Set(value As Boolean)
                chkFeeAgree.Checked = value
            End Set
        End Property



#End Region

#Region "::: Control Event Handlers :::"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim client As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode)

            ' Update the checkbox label
            chkFeeAgree.Text = "<a href='#' data-toggle='modal' data-target='#feesModal' data-backdrop='static' data-keyboard='false' title='" & GetGlobalResourceObject("WebResources", "ConfirmFeeConditions").ToString & "'>" & GetGlobalResourceObject("WebResources", "ConfirmFeeTerms").ToString & "</a>"

        End Sub

#End Region



    End Class

End Namespace