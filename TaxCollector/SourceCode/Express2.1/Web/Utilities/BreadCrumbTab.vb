Namespace B2P.PaymentLanding.Express.Web
    ''' <summary>
    ''' This class is for maintainting the information in bread crumb menu tab pages
    ''' </summary>
    Public Class BreadCrumbTab

        Private _pageTag As String
        ''' <summary>
        ''' Tag name of the page 
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property PageTag() As String
            Get
                Return _pageTag
            End Get
            Set(ByVal value As String)
                _pageTag = value
            End Set
        End Property
        Private _menuName As String
        ''' <summary>
        ''' Display brad crumb menu name
        ''' </summary>
        ''' <returns></returns>
        Public Property MenuName() As String
            Get
                Return _menuName
            End Get
            Set(ByVal value As String)
                _menuName = value
            End Set
        End Property

        Private _pageName As String
        ''' <summary>
        ''' Linked page or file name 
        ''' </summary>
        ''' <returns></returns>
        Public Property PageName() As String
            Get
                Return _pageName
            End Get
            Set(ByVal value As String)
                _pageName = value
            End Set
        End Property
        Private _isPageVisible As Boolean
        ''' <summary>
        ''' Indicate the page is visible or not
        ''' </summary>
        ''' <returns></returns>
        Public Property IsPageVisible() As Boolean
            Get
                Return _isPageVisible
            End Get
            Set(ByVal value As Boolean)
                _isPageVisible = value
            End Set
        End Property

        Private _index As Integer
        ''' <summary>
        ''' Index of the bread crumb menu
        ''' </summary>
        ''' <returns></returns>
        Public Property Index() As Integer
            Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        Private _isVisited As Boolean
        ''' <summary>
        ''' Indicate the page is already visited or not
        ''' </summary>
        ''' <returns></returns>
        Public Property IsVisited() As Boolean
            Get
                Return _isVisited
            End Get
            Set(ByVal value As Boolean)
                _isVisited = value
            End Set
        End Property
    End Class

End Namespace

