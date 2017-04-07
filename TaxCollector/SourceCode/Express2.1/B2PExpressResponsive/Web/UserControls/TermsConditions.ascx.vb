Namespace B2P.PaymentLanding.Express.Web

    Public Class TermsConditions : Inherits System.Web.UI.UserControl

#Region "::: Properties :::"

        Public Property AgreedToTerms As Boolean
            Get
                Return chkTermsAgree.Checked
            End Get
            Set(value As Boolean)
                chkTermsAgree.Checked = value
            End Set
        End Property

        Public ReadOnly Property EmailAddress As String
            Get
                Return txtEmailAddress.Text.Trim
            End Get
        End Property

#End Region

#Region "::: Control Event Handlers :::"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            ' Add client side javascript attributes to the various form controls
            RegisterClientJs()

            ' Update the checkbox label
            chkTermsAgree.Text = "<a href='#' data-toggle='modal' data-target='#termsModal' data-backdrop='static' data-keyboard='false' title='" & GetGlobalResourceObject("WebResources", "ConfirmTermsConditions").ToString & "'>" & GetGlobalResourceObject("WebResources", "ConfirmAgreementTerms").ToString & "</a>"

        End Sub

#End Region

#Region "::: Methods :::"

        ''' <summary>
        ''' Adds client side javascript to validate the form.
        ''' </summary>
        Private Sub RegisterClientJs()
            txtEmailAddress.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.Email)")
            txtEmailAddress.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.Email)")
        End Sub

#End Region

    End Class

End Namespace