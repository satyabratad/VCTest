Namespace B2P.PaymentLanding.Express.Web
    Public Class BreadCrumbTab

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

        Private _pageName As String
        Public Property PageName() As String
            Get
                Return _pageName
            End Get
            Set(ByVal value As String)
                _pageName = value
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

End Namespace

