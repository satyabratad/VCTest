Imports System
Imports System.Web.UI.WebControls
Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web

    Public Class BankAccount : Inherits System.Web.UI.UserControl

#Region "::: Properties :::"

        Public Property BankAccountNumber() As String
            Get
                Return Utility.SafeEncode(txtBankAccountNumber.Text.Trim)
            End Get
            Set(value As String)
                txtBankAccountNumber.Text = value.Trim
            End Set
        End Property

        'Public Property BankAccountNumber2() As String
        '    Get
        '        Return Utility.SafeEncode(txtBankAccountNumber2.Text.Trim)
        '    End Get
        '    Set(value As String)
        '        txtBankAccountNumber2.Text = value.Trim
        '    End Set
        'End Property

        Public ReadOnly Property BankAccountType() As String
            Get
                Return ddlBankAccountType.SelectedValue
            End Get
        End Property

        Public ReadOnly Property BankRoutingNumber() As String
            Get
                Return Utility.SafeEncode(txtBankRoutingNumber.Text.Trim)
            End Get
        End Property

        'Public Property FirstName() As String
        '    Get
        '        Return Utility.SafeEncode(txtAchFirstName.Text.Trim)
        '    End Get
        '    Set(value As String)
        '        txtAchFirstName.Text = value.Trim
        '    End Set
        'End Property

        'Public Property LastName() As String
        '    Get
        '        Return Utility.SafeEncode(txtAchLastName.Text.Trim)
        '    End Get
        '    Set(value As String)
        '        txtAchLastName.Text = value.Trim
        '    End Set
        'End Property

        Public Property NameonBankAccount() As String
            Get
                Return Utility.SafeEncode(txtNameonBankAccount.Text.Trim)
            End Get
            Set(value As String)
                txtNameonBankAccount.Text = value.Trim
            End Set
        End Property

#End Region

#Region "::: Control Event Handlers :::"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            ' Add client side javascript attributes to the various form controls
            RegisterClientJs()

            If Not Me.IsPostBack Then
                LoadAccountTypes()
                'Persist Bank account details
                If Not BLL.SessionManager.BankAccName Is Nothing Then
                    txtNameonBankAccount.Text = BLL.SessionManager.BankAccName
                End If
                If Not BLL.SessionManager.BankAccType Is Nothing Then
                    ddlBankAccountType.ClearSelection()
                    ddlBankAccountType.Items.FindByValue(BLL.SessionManager.BankAccType).Selected = True
                    ddlBankAccountType_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If

        End Sub

        Protected Sub ddlBankAccountType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlBankAccountType.SelectedIndexChanged
            ' Raise an event to pass the info over to another control
            Select Case Me.ddlBankAccountType.SelectedValue
                Case "CommChecking", "CommSavings"
                    RaiseEvent ShowCommercialAccountMessage(GetGlobalResourceObject("WebResources", "BankAccountCommercialMsg").ToString() & " <span class=""bold"">" & BLL.SessionManager.OriginatorID & "</span>", True)

                Case Else
                    RaiseEvent ShowCommercialAccountMessage(String.Empty, False)
            End Select

            Me.ddlBankAccountType.Focus()
        End Sub

#End Region

#Region "::: Methods :::"

        ''' <summary>
        ''' Clears the values of this UserControl.
        ''' </summary>
        Public Sub Clear()
            'txtAchFirstName.Text = String.Empty
            'txtAchLastName.Text = String.Empty
            ddlBankAccountType.SelectedValue = String.Empty
            txtBankRoutingNumber.Text = String.Empty
            txtBankAccountNumber.Text = String.Empty
            'txtBankAccountNumber2.Text = String.Empty
        End Sub

        ''' <summary>
        ''' Loads the available bank account types to a DropDownList.
        ''' </summary>
        Private Sub LoadAccountTypes()
            ddlBankAccountType.Items.Clear()
            ddlBankAccountType.Items.Add(New ListItem("Select an option...", String.Empty))
            ddlBankAccountType.Items.Add(New ListItem("Personal Checking", "Checking"))
            ddlBankAccountType.Items.Add(New ListItem("Personal Savings", "Savings"))
            ddlBankAccountType.Items.Add(New ListItem("Commercial Checking", "CommChecking"))
            ddlBankAccountType.Items.Add(New ListItem("Commercial Savings", "CommSavings"))

            ' For testing validation
            'ddlBankAccountType.Items.Insert(0, New ListItem("Select an option...", String.Empty))
            'ddlBankAccountType.SelectedIndex = 0
            'ddlBankAccountType.SelectedValue = "Checking"
        End Sub

        ''' <summary>
        ''' Adds client side JavaScript to the form controls.
        ''' </summary>
        Private Sub RegisterClientJs()
            'txtAchFirstName.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)")
            'txtAchFirstName.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)")

            'txtAchLastName.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)")
            'txtAchLastName.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)")

            txtNameonBankAccount.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)")
            txtNameonBankAccount.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)")

            txtBankRoutingNumber.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.NumbersOnly)")
            txtBankRoutingNumber.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.NumbersOnly)")

            txtBankAccountNumber.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.NumbersOnly)")
            txtBankAccountNumber.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.NumbersOnly)")

            'txtBankAccountNumber2.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.NumbersOnly)")
            'txtBankAccountNumber2.Attributes.Add("onpaste", "return false")
        End Sub

#End Region

#Region "::: Events :::"

        Public Event ShowCommercialAccountMessage(ByVal message As String, ByVal showMessage As Boolean)

#End Region

    End Class

End Namespace