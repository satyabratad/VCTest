'===================================================================
' File:     BreadCrumbMenu.vb
' Date:     06.04.2017
' Author:   RS
'  
' Summary:  Manage the Bread Crumb menu

'===================================================================

Imports B2P.PaymentLanding.Express.Web
Imports B2P.PaymentLanding.Express.BLL
Imports Resources

''' <summary>
'''  This will manage the Bread Crumb menu
''' </summary>
Public Class BreadCrumbMenu
    Inherits System.Web.UI.UserControl
#Region "Properties"
    ''' <summary>
    ''' Identify the account details tab  will be visible or not
    ''' </summary>
    Private _isAccountPageVisible As Boolean = True
    Public Property IsAccountPageVisible() As Boolean
        Get
            Return _isAccountPageVisible
        End Get
        Set(ByVal value As Boolean)
            _isAccountPageVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Identify the Contact Info tab  will be visible or not
    ''' </summary>
    Private _isContactInfoVisible As String
    Public Property IsContactInfoVisible() As String
        Get
            Return _isContactInfoVisible
        End Get
        Set(ByVal value As String)
            _isContactInfoVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Indicate the current page 
    ''' </summary>
    Private _pageTagName As String
    Public Property PageTagName() As String
        Get
            Return _pageTagName
        End Get
        Set(ByVal value As String)
            _pageTagName = value
        End Set
    End Property

    ''' <summary>
    ''' Link address of the current page.
    ''' Uasd if the link is other than defaulft page link
    ''' </summary>
    Private _RedirectAddress As String
    Public Property RedirectAddress() As String
        Get
            Return _RedirectAddress
        End Get
        Set(ByVal value As String)
            _RedirectAddress = value
        End Set
    End Property

#End Region
#Region " Class & Variables"


    Shared tabList As List(Of BreadCrumbTab)
#End Region
#Region "Methods"
    ''' <summary>
    ''' This method will initiate the list of Bread creumb menu tab pages
    ''' </summary>
    Public Sub PopulateTabList()
        Dim tab As BreadCrumbTab
        If SessionManager.BreadCrumbMenuTab Is Nothing Then

            tabList = New List(Of BreadCrumbTab)

            tab = New BreadCrumbTab()
            tab.Index = 1
            tab.PageName = ResolveUrl("~/pay/")
            tab.PageTag = PageTabName.Home.ToString()
            tab.MenuName = WebResources.AccountDetails
            tab.IsVisited = False
            tab.IsPageVisible = True
            tabList.Add(tab)

            tab = New BreadCrumbTab()
            tab.Index = 2
            tab.PageName = ResolveUrl("~/pay/ContactInfo.aspx")
            tab.PageTag = PageTabName.ContactInfo.ToString()
            tab.MenuName = WebResources.ContactInfo
            tab.IsVisited = False
            tab.IsPageVisible = True
            tabList.Add(tab)

            tab = New BreadCrumbTab()
            tab.Index = 3
            tab.PageName = ResolveUrl("~/pay/Payment.aspx")
            tab.PageTag = PageTabName.PaymentDetails.ToString()
            tab.MenuName = WebResources.PaymentDetails
            tab.IsVisited = False
            tab.IsPageVisible = True
            tabList.Add(tab)

            tab = New BreadCrumbTab()
            tab.Index = 4
            tab.PageName = ResolveUrl("~/pay/Confirm.aspx")
            tab.PageTag = PageTabName.PaymentConfirm.ToString()
            tab.MenuName = WebResources.PaymentConfirm
            tab.IsVisited = False
            tab.IsPageVisible = True
            tabList.Add(tab)

            tab = New BreadCrumbTab()
            tab.Index = 5
            tab.PageName = ResolveUrl("~/pay/PaymentComplete.aspx")
            tab.PageTag = PageTabName.PaymentSuccess.ToString()
            tab.MenuName = WebResources.PaymentSuccess
            tab.IsVisited = False
            tab.IsPageVisible = True
            tabList.Add(tab)

            tab = New BreadCrumbTab()
            tab.Index = 6
            tab.PageName = ResolveUrl("~/pay/PaymentFailure.aspx")
            tab.PageTag = PageTabName.PaymentFaild.ToString()
            tab.MenuName = WebResources.PaymentFailed
            tab.IsVisited = False
            tab.IsPageVisible = True
            tabList.Add(tab)

            SessionManager.BreadCrumbMenuTab = tabList
        Else
            tabList = DirectCast(SessionManager.BreadCrumbMenuTab, List(Of BreadCrumbTab))

        End If
    End Sub
    ''' <summary>
    ''' This method will generate the bread crumb menu
    ''' </summary>
    Public Sub GenerateBreadCrumb()
        Dim currentTab As BreadCrumbTab
        Dim finalTab As BreadCrumbTab
        Dim payConfirmTab As BreadCrumbTab
        Dim pageIndex As Integer
        Dim htmlString As New StringBuilder

        IsContactInfoVisible = SessionManager.IsContactInfoRequired
        If tabList Is Nothing Then
            PopulateTabList()
        End If


        If String.IsNullOrEmpty(PageTagName) = False Then
            currentTab = tabList.Where(Function(t As BreadCrumbTab) t.PageTag.Equals(PageTagName, System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()

            If (currentTab.PageTag.Equals(PageTabName.Home.ToString(), System.StringComparison.InvariantCultureIgnoreCase)) And IsAccountPageVisible = False Then
                currentTab.IsPageVisible = False
            End If
            If String.IsNullOrEmpty(RedirectAddress) = False Then

                currentTab.PageName = RedirectAddress
            End If
            currentTab.IsVisited = True
            divBreadCrumb.InnerHtml = ""

            If Not PageTagName.Equals(PageTabName.PaymentConfirm.ToString(), System.StringComparison.InvariantCultureIgnoreCase) And currentTab.Index < PageTabName.PaymentConfirm Then
                payConfirmTab = tabList.Where(Function(t As BreadCrumbTab) t.PageTag.Equals(PageTabName.PaymentConfirm.ToString(), System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()
                payConfirmTab.IsVisited = False
            End If


            htmlString.Append("<ul class='breadcrumb'>")
            finalTab = tabList.Where(Function(t As BreadCrumbTab) (t.PageTag.Equals(PageTabName.PaymentSuccess.ToString(), System.StringComparison.InvariantCultureIgnoreCase)) And t.IsVisited = True).FirstOrDefault()
            If Not finalTab Is Nothing Then
                If Not PageTagName.Equals(PageTabName.PaymentSuccess.ToString(), System.StringComparison.InvariantCultureIgnoreCase) And Not PageTagName.Equals(PageTabName.PaymentFaild.ToString(), System.StringComparison.InvariantCultureIgnoreCase) Then
                    B2P.PaymentLanding.Express.BLL.SessionManager.Clear()
                    Response.Redirect("/Errors/SessionExpired.aspx", True)

                End If
            End If

            If SessionManager.HasZeroAmountItem Then
                For Each tab As BreadCrumbTab In tabList
                    If tab.Index > 1 Then
                        tab.PageName = "#"
                    End If
                Next
            Else
                For Each tab As BreadCrumbTab In tabList
                    If tab.Index > 1 And tab.PageName = "#" Then
                        If tab.Index = 2 Then
                            tab.PageName = ResolveUrl("~/pay/ContactInfo.aspx")
                        End If

                        If tab.Index = 3 Then
                            tab.PageName = ResolveUrl("~/pay/Payment.aspx")
                        End If

                        If tab.Index = 4 Then
                            tab.PageName = ResolveUrl("~/pay/Confirm.aspx")
                        End If

                        If tab.Index = 5 Then
                            tab.PageName = ResolveUrl("~/pay/PaymentComplete.aspx")
                        End If

                        If tab.Index = 6 Then
                            tab.PageName = ResolveUrl("~/pay/PaymentFailure.aspx")
                        End If
                    End If
                Next



            End If

            pageIndex = 1
            For Each tab As BreadCrumbTab In tabList

                Dim delimeter As Char = " "c
                Dim menuNames() As String = tab.MenuName.Trim.Split(delimeter)

                If tab.IsVisited = True Then
                    If Not PageTagName.Equals(PageTabName.PaymentSuccess.ToString(), System.StringComparison.InvariantCultureIgnoreCase) And Not PageTagName.Equals(PageTabName.PaymentFaild.ToString(), System.StringComparison.InvariantCultureIgnoreCase) Then

                        If tab.PageTag.Equals(PageTabName.ContactInfo.ToString(), System.StringComparison.InvariantCultureIgnoreCase) Then
                            If IsContactInfoVisible = True Then

                                htmlString.AppendFormat("<li class='active'><a href='{0}' class='' title='{1} {2}' ><span Class='badge badge-inverse'>", tab.PageName, menuNames(0), menuNames(1))
                                htmlString.AppendFormat("{0}</span> <span class='hidden-xs'>{1}</span> <span class='hidden-xs hidden-sm'>{2}</span></a></li>", pageIndex.ToString(), menuNames(0), menuNames(1))
                                pageIndex = pageIndex + 1
                            End If
                        Else

                            htmlString.AppendFormat("<li class='active'><a href='{0}' class='' title='{1} {2}' ><span Class='badge badge-inverse'>", tab.PageName, menuNames(0), menuNames(1))
                            htmlString.AppendFormat("{0}</span> <span class='hidden-xs'>{1}</span> <span class='hidden-xs hidden-sm'>{2}</span></a></li>", pageIndex.ToString(), menuNames(0), menuNames(1))
                            pageIndex = pageIndex + 1

                        End If
                    Else

                        If tab.PageTag.Equals(PageTabName.PaymentFaild.ToString(), System.StringComparison.InvariantCultureIgnoreCase) Then
                            htmlString.AppendFormat("<li class='danger'><a href='#' class='' title='{0} {1}'><span style='color:transparent !important;background-color:transparent !important' Class='badge'>", menuNames(0), menuNames(1))
                            htmlString.AppendFormat("<span style='padding-left:1px'></span></span> <span class='hidden-xs'>{0}</span> <span class='hidden-xs hidden-sm'>{1}</span></a></li>", menuNames(0), menuNames(1))
                        ElseIf tab.PageTag.Equals(PageTabName.PaymentSuccess.ToString(), System.StringComparison.InvariantCultureIgnoreCase) Then
                            htmlString.AppendFormat("<li class='active'><a href='#' class='' title='{0} {1}' ><span style='color:transparent !important;background-color:transparent !important' Class='badge'>", menuNames(0), menuNames(1))
                            htmlString.AppendFormat("<span style='padding-left:1px'></span></span> <span class='hidden-xs'>{0}</span> <span class='hidden-xs hidden-sm'>{1}</span></a></li>", menuNames(0), menuNames(1))

                        Else

                            htmlString.AppendFormat("<li class='active'><a href='#' class='' title='{0} {1}' ><span Class='badge badge-inverse'>", menuNames(0), menuNames(1))
                            htmlString.AppendFormat("{0}</span> <span class='hidden-xs'>{1}</span> <span class='hidden-xs hidden-sm'>{2}</span></a></li>", pageIndex.ToString(), menuNames(0), menuNames(1))
                        End If
                        pageIndex = pageIndex + 1
                    End If

                Else
                    If finalTab Is Nothing Then

                        If Not PageTagName.Equals(PageTabName.PaymentSuccess.ToString(), System.StringComparison.InvariantCultureIgnoreCase) And Not PageTagName.Equals(PageTabName.PaymentFaild.ToString(), System.StringComparison.InvariantCultureIgnoreCase) Then

                            If tab.PageTag.Equals(PageTabName.ContactInfo.ToString(), System.StringComparison.InvariantCultureIgnoreCase) Then
                                If IsContactInfoVisible = True Then

                                    htmlString.AppendFormat("<li class=''><a href='#' class='inactiveLink' title='{0} {1}'><span class='badge'>", menuNames(0), menuNames(1))
                                    htmlString.AppendFormat("{0}</span> <span class='hidden-xs'>{1}</span> <span class='hidden-xs hidden-sm'>{2}</span></a></li>", pageIndex.ToString(), menuNames(0), menuNames(1))
                                    pageIndex = pageIndex + 1
                                End If
                            Else
                                If Not tab.PageTag.Equals(PageTabName.PaymentSuccess.ToString(), System.StringComparison.InvariantCultureIgnoreCase) And Not tab.PageTag.Equals(PageTabName.PaymentFaild.ToString(), System.StringComparison.InvariantCultureIgnoreCase) Then
                                    htmlString.AppendFormat("<li class=''><a href='#' class='inactiveLink' title='{0} {1}'><span class='badge'>", menuNames(0), menuNames(1))
                                    htmlString.AppendFormat("{0}</span> <span class='hidden-xs'>{1}</span> <span class='hidden-xs hidden-sm'>{2}</span></a></li>", pageIndex.ToString(), menuNames(0), menuNames(1))
                                    pageIndex = pageIndex + 1
                                End If

                            End If
                        End If
                    End If
                End If
            Next

            htmlString.AppendFormat("</ul>")

            divBreadCrumb.InnerHtml = htmlString.ToString()



            SessionManager.BreadCrumbMenuTab = tabList
        End If

    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

            PopulateTabList()


        End If
        If Not IsDBNull(tabList) Then
            GenerateBreadCrumb()
        End If
    End Sub

End Class
