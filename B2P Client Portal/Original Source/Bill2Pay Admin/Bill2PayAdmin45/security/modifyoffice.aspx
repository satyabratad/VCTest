<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="modifyoffice.aspx.vb" Inherits="Bill2PayAdmin45.modifyoffice" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Modify Office :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
  
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Modify Office</div>
            <div class="header_border"></div>
    
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

  
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
      
    
    
    <!--StartContent-->
    <div class="main_content">
    
    <asp:Panel ID="pnlClientCode" runat="server">
    Client Code:
      <telerik:RadComboBox ID="ddlAUClient" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="1" OnSelectedIndexChanged="ddlAUClient_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false">
      <Items>
            <telerik:RadComboBoxItem Text="Select One" Value="" Font-Italic="true" />
        </Items>
      </telerik:RadComboBox>
    </asp:Panel>                 
    <br />
  
  
    <div id="dataGrid" style="padding-left:10px;">
        <telerik:RadGrid runat="server" AllowFilteringByColumn="True" ID="RadGrid1"
            AutoGenerateColumns="False" GridLines="None" Skin="WebBlue" >
            <MasterTableView AutoGenerateColumns="False" GridLines="None"  >
                <RowIndicatorColumn>
                    <HeaderStyle Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn>
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                    
                    <telerik:GridButtonColumn CommandName="viewOffice" Text="Edit" 
                        UniqueName="column1">
                    </telerik:GridButtonColumn>
                   
                    <telerik:GridBoundColumn DataField="Office_ID" HeaderText="Office ID" 
                        UniqueName="column2" Display="false" HtmlEncode="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Name" HeaderText="Office Name" 
                        UniqueName="column3" HtmlEncode="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Address" HeaderText="Address" 
                        UniqueName="column4" HtmlEncode="true">
                    </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn DataField="City" HeaderText="City" 
                        UniqueName="column7" HtmlEncode="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="State" HeaderText="State" 
                        UniqueName="column5" HtmlEncode="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ZipCode" HeaderText="Zip Code" 
                        UniqueName="column6" HtmlEncode="true">
                    </telerik:GridBoundColumn>
                    
                    <telerik:GridBoundColumn DataField="PhoneNumber" HeaderText="Phone Number" 
                        UniqueName="column9" HtmlEncode="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Status" HeaderText="Status" 
                        UniqueName="column8" HtmlEncode="true">
                    </telerik:GridBoundColumn>
                    
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
   <br />
            
   </div>
    <!--EndContent-->
  
  
  </ContentTemplate>
  <Triggers>
    
  </Triggers>
      </asp:UpdatePanel>  
 
  
  </div>
         <custom:timeout ID="timeoutControl" runat="server" />

</form>
      
      <div class="clear"></div> 
      <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
      </div>
</body> 
</html> 