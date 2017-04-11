<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BreadCrumbMenu.ascx.vb" Inherits=".BreadCrumbMenu" %>
<script type="text/javascript">
    function RedirectBreadCrumbTab(pageurl)
    {
         
        var baseUrl = window.location.href.split('/').slice(0, -1).join("/");         
        var url = baseUrl + "/" + pageurl;         
        window.location.href = url;
    }

   
</script>
 <div class="row">
<div id="divBreadCrumb" runat="server" >
    
</div>
</div>