Imports System
Imports System.Globalization
Imports System.IO
Imports System.Xml
''' <summary>
''' User Controller for Property Address
''' </summary>
Public Class PropertyAddress
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
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

    ''' <summary>
    ''' Bind State from XML
    ''' </summary>
    Private Sub BindState()
        ddlState.Items.Clear()
        ddlState.Items.Add(New ListItem("--Select--", ""))

        If ConfigurationManager.AppSettings("UnitedStates") IsNot Nothing Then
            Dim UnitedStatesPath = ConfigurationManager.AppSettings("UnitedStates")

            If Not UnitedStatesPath.StartsWith("~/") Then
                UnitedStatesPath = String.Format("~/{0}", UnitedStatesPath)
            End If
            UnitedStatesPath = Server.MapPath(UnitedStatesPath)


            Dim _USStates = ReadStateXML(UnitedStatesPath)
            For Each item As KeyValuePair(Of String, String) In _USStates
                Dim element = New ListItem(item.Value, item.Key)
                element.Attributes("OptionGroup") = "USA"
                ddlState.Items.Add(element)
            Next

        End If



        If ConfigurationManager.AppSettings("CanadianProvinces") IsNot Nothing Then
            Dim CanadianProvincessPath = ConfigurationManager.AppSettings("CanadianProvinces")

            If Not CanadianProvincessPath.StartsWith("~/") Then
                CanadianProvincessPath = String.Format("~/{0}", CanadianProvincessPath)
            End If
            CanadianProvincessPath = Server.MapPath(CanadianProvincessPath)

            Dim _USStates = ReadStateXML(CanadianProvincessPath)
            For Each item As KeyValuePair(Of String, String) In _USStates
                Dim element = New ListItem(item.Value, item.Key)
                element.Attributes("OptionGroup") = "Canada"
                ddlState.Items.Add(element)
            Next
        End If

    End Sub

    Private Function ReadStateXML(path As String) As IEnumerable(Of KeyValuePair(Of String, String))

        Dim _Values As List(Of KeyValuePair(Of String, String)) =
            New List(Of KeyValuePair(Of String, String))

        If (IO.File.Exists(path)) Then
            Dim document As XmlReader = New XmlTextReader(path)
            While (document.Read())
                Dim type = document.NodeType
                If (type = XmlNodeType.Element) Then
                    If (document.Name = "state") Then
                        Dim key = document.GetAttribute("abbreviation")
                        Dim value = document.GetAttribute("name")
                        Dim element = New KeyValuePair(Of String, String)(key, value)

                        _Values.Add(element)
                    End If
                End If
            End While
        Else
            Throw New FileNotFoundException(String.Format(CultureInfo.InvariantCulture, "{0} Not Found", path))
        End If

        Return _Values
    End Function
#End Region
End Class