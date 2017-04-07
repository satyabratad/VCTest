Imports System
''' <summary>
''' User Controller for Property Address
''' </summary>
Public Class PropertyAddress
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindState()
        End If
    End Sub

#Region " ::: Properties ::: "


    ''' <summary>
    ''' Address 1
    ''' </summary>
    ''' <returns>text</returns>
    Public Property Address1() As String
        Get
            Return txtAddress1.Text.Trim
        End Get
        Set(ByVal value As String)
            txtAddress1.Text = value
        End Set
    End Property


    ''' <summary>
    ''' Address 2
    ''' </summary>
    ''' <returns>text</returns>
    Public Property Address2() As String
        Get
            Return txtAddress2.Text.Trim
        End Get
        Set(ByVal value As String)
            txtAddress2.Text = value
        End Set
    End Property


    ''' <summary>
    ''' City
    ''' </summary>
    ''' <returns>text</returns>
    Public Property City() As String
        Get
            Return txtCity.Text.Trim
        End Get
        Set(ByVal value As String)
            txtCity.Text = value
        End Set
    End Property

    ''' <summary>
    ''' State Value field
    ''' </summary>
    ''' <returns>text</returns>
    Public Property StateValue() As String
        Get
            Return ddlState.SelectedItem.Value.Trim
        End Get
        Set(ByVal value As String)
            ddlState.Items.FindByValue(value).Selected = True
        End Set
    End Property

    ''' <summary>
    ''' State Text
    ''' </summary>
    ''' <returns>text</returns>
    Public Property StateText() As String
        Get
            Return ddlState.SelectedItem.Text.Trim
        End Get
        Set(ByVal value As String)
            ddlState.Items.FindByText(value).Selected = True
        End Set
    End Property
    ''' <summary>
    ''' Zip
    ''' </summary>
    ''' <returns>text</returns>
    Public Property Zip() As String
        Get
            Return txtZip.Text.Trim
        End Get
        Set(ByVal value As String)
            txtZip.Text = value
        End Set
    End Property


    ''' <summary>
    ''' Amount
    ''' </summary>
    ''' <returns>numeric</returns>
    Public Property Amount() As Decimal
        Get
            Return Convert.ToDecimal(txtAmount.Text.Trim)
        End Get
        Set(ByVal value As Decimal)
            txtAmount.Text = value
        End Set
    End Property


#End Region

#Region " ::: Methods ::: "
    Private Sub BindState()
        Dim _States As List(Of KeyValuePair(Of String, String)) =
            New List(Of KeyValuePair(Of String, String))

        _States.Add(New KeyValuePair(Of String, String)("", "--Select--"))
        _States.Add(New KeyValuePair(Of String, String)("AL", "Alabama"))
        _States.Add(New KeyValuePair(Of String, String)("AK", "Alaska"))
        _States.Add(New KeyValuePair(Of String, String)("AZ", "Arizona"))
        _States.Add(New KeyValuePair(Of String, String)("AR", "Arkansas"))
        _States.Add(New KeyValuePair(Of String, String)("CA", "California"))
        _States.Add(New KeyValuePair(Of String, String)("CO", "Colorado"))
        _States.Add(New KeyValuePair(Of String, String)("CT", "Connecticut"))
        _States.Add(New KeyValuePair(Of String, String)("DE", "Delaware"))
        _States.Add(New KeyValuePair(Of String, String)("DC", "District Of Columbia"))
        _States.Add(New KeyValuePair(Of String, String)("FL", "Florida"))
        _States.Add(New KeyValuePair(Of String, String)("GA", "Georgia"))
        _States.Add(New KeyValuePair(Of String, String)("HI", "Hawaii"))
        _States.Add(New KeyValuePair(Of String, String)("ID", "Idaho"))
        _States.Add(New KeyValuePair(Of String, String)("IL", "Illinois"))
        _States.Add(New KeyValuePair(Of String, String)("IN", "Indiana"))
        _States.Add(New KeyValuePair(Of String, String)("IA", "Iowa"))
        _States.Add(New KeyValuePair(Of String, String)("KS", "Kansas"))
        _States.Add(New KeyValuePair(Of String, String)("KY", "Kentucky"))
        _States.Add(New KeyValuePair(Of String, String)("LA", "Louisiana"))
        _States.Add(New KeyValuePair(Of String, String)("ME", "Maine"))
        _States.Add(New KeyValuePair(Of String, String)("MD", "Maryland"))
        _States.Add(New KeyValuePair(Of String, String)("MA", "Massachusetts"))
        _States.Add(New KeyValuePair(Of String, String)("MI", "Michigan"))
        _States.Add(New KeyValuePair(Of String, String)("MN", "Minnesota"))
        _States.Add(New KeyValuePair(Of String, String)("MS", "Mississippi"))
        _States.Add(New KeyValuePair(Of String, String)("MO", "Missouri"))
        _States.Add(New KeyValuePair(Of String, String)("MT", "Montana"))
        _States.Add(New KeyValuePair(Of String, String)("NE", "Nebraska"))
        _States.Add(New KeyValuePair(Of String, String)("NV", "Nevada"))
        _States.Add(New KeyValuePair(Of String, String)("NH", "New Hampshire"))
        _States.Add(New KeyValuePair(Of String, String)("NJ", "New Jersey"))
        _States.Add(New KeyValuePair(Of String, String)("NM", "New Mexico"))
        _States.Add(New KeyValuePair(Of String, String)("NY", "New York"))
        _States.Add(New KeyValuePair(Of String, String)("NC", "North Carolina"))
        _States.Add(New KeyValuePair(Of String, String)("ND", "North Dakota"))
        _States.Add(New KeyValuePair(Of String, String)("OH", "Ohio"))
        _States.Add(New KeyValuePair(Of String, String)("OK", "Oklahoma"))
        _States.Add(New KeyValuePair(Of String, String)("OR", "Oregon"))
        _States.Add(New KeyValuePair(Of String, String)("PA", "Pennsylvania"))
        _States.Add(New KeyValuePair(Of String, String)("RI", "Rhode Island"))
        _States.Add(New KeyValuePair(Of String, String)("SC", "South Carolina"))
        _States.Add(New KeyValuePair(Of String, String)("SD", "South Dakota"))
        _States.Add(New KeyValuePair(Of String, String)("TN", "Tennessee"))
        _States.Add(New KeyValuePair(Of String, String)("TX", "Texas"))
        _States.Add(New KeyValuePair(Of String, String)("UT", "Utah"))
        _States.Add(New KeyValuePair(Of String, String)("VT", "Vermont"))
        _States.Add(New KeyValuePair(Of String, String)("VA", "Virginia"))
        _States.Add(New KeyValuePair(Of String, String)("WA", "Washington"))
        _States.Add(New KeyValuePair(Of String, String)("WV", "West Virginia"))
        _States.Add(New KeyValuePair(Of String, String)("WI", "Wisconsin"))
        _States.Add(New KeyValuePair(Of String, String)("WY", "Wyoming"))

        ddlState.DataSource = _States
        ddlState.DataTextField = "Value"
        ddlState.DataValueField = "Key"
        ddlState.DataBind()

    End Sub
#End Region
End Class