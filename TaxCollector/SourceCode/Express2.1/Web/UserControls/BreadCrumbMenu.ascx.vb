Public Enum PageTabName
    Home = 1
    ContactInfo = 2
    PaymentDetails = 3
    PaymentConfirm = 4
    PaymentSuccess = 5

End Enum
Public Class BreadCrumbMenu
    Inherits System.Web.UI.UserControl
#Region "Properties"

    Private _pageTagName As String
    Public Property PageTagName() As String
        Get
            Return _pageTagName
        End Get
        Set(ByVal value As String)
            _pageTagName = value
        End Set
    End Property

#End Region
#Region "Variables"
    Class BreadCrumbTabs
        Private _pageTag As String
        Public Property PageTag() As String
            Get
                Return _pageTag
            End Get
            Set(ByVal value As String)
                _pageTag = value
            End Set
        End Property
        Private _menuName As String
        Public Property MenuName() As String
            Get
                Return _menuName
            End Get
            Set(ByVal value As String)
                _menuName = value
            End Set
        End Property

        Private _index As Integer
        Public Property Index() As Integer
            Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        Private _isVisited As Boolean
        Public Property IsVisited() As Boolean
            Get
                Return _isVisited
            End Get
            Set(ByVal value As Boolean)
                _isVisited = value
            End Set
        End Property
    End Class



    Shared tabList As List(Of BreadCrumbTabs)
#End Region
#Region "Methods"
    Private Sub PopulateTabList()
        Dim tab As BreadCrumbTabs

        If tabList Is Nothing Then
            tabList = New List(Of BreadCrumbTabs)
        End If


        tab = New BreadCrumbTabs()
        tab.Index = 1
        tab.PageTag = PageTabName.Home.ToString()
        tab.MenuName = ControlResource.AccountDetails
        tab.IsVisited = False
        tabList.Add(tab)

        tab = New BreadCrumbTabs()
        tab.Index = 2
        tab.PageTag = PageTabName.ContactInfo.ToString()
        tab.MenuName = ControlResource.ContactInfo
        tab.IsVisited = False
        tabList.Add(tab)

        tab = New BreadCrumbTabs()
        tab.Index = 3
        tab.PageTag = PageTabName.PaymentDetails.ToString()
        tab.MenuName = ControlResource.PaymentDetails
        tab.IsVisited = False
        tabList.Add(tab)

        tab = New BreadCrumbTabs()
        tab.Index = 4
        tab.PageTag = PageTabName.PaymentConfirm.ToString()
        tab.MenuName = ControlResource.PaymentConfirm
        tab.IsVisited = False
        tabList.Add(tab)

        tab = New BreadCrumbTabs()
        tab.Index = 5
        tab.PageTag = PageTabName.PaymentSuccess.ToString()
        tab.MenuName = ControlResource.PaymentCompleate
        tab.IsVisited = False
        tabList.Add(tab)
    End Sub

    Private Sub GenerateBreadCrumb()
        Dim CurrentTab As BreadCrumbTabs

        CurrentTab = tabList.Where(Function(t As BreadCrumbTabs) t.PageTag.Equals(PageTagName, System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()

        CurrentTab.IsVisited = True

        Dim html = "<ul class='breadcrumb'>"

        For Each tab As BreadCrumbTabs In tabList

            If tab.IsVisited = True Then
                html += "<li class='active'><a href='#' class='' ><span Class='badge badge-inverse'>"
                html += tab.Index.ToString() + "</span> " + tab.MenuName + " <span class='hidden-xs hidden-sm'></span></a></li>"
            Else
                If tab.Index <= CurrentTab.Index Then
                    html += "<li class='active'><a href='#' class='' ><span Class='badge badge-inverse'>"
                    html += tab.Index.ToString() + "</span> " + tab.MenuName + " <span class='hidden-xs hidden-sm'></span></a></li>"
                Else
                    html += "<li class=''><a href='#' class='inactiveLink'><span class='badge'>"
                    html += tab.Index.ToString() + "</span> " + tab.MenuName + " <span class='hidden-xs hidden-sm'> </span></a></li>"
                End If
            End If

        Next
        html += "</ul>"

        divBreadCrumb.InnerHtml = html


    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            PopulateTabList()
        End If
        GenerateBreadCrumb()
    End Sub

End Class