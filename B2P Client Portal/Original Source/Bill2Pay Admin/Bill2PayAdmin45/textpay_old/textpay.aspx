<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="textpay.aspx.vb" Inherits="Bill2PayAdmin45.textpay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>TextPay :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css"  title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
      
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">TextPay Search</div>
            <div class="header_border"></div>
           
            <!--Start Main Content-->
            <div class="main_content">
                <asp:Panel ID="pnlClient" runat="server">
                    <telerik:RadComboBox ID="ddlAUClient" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="8" >
                        <Items>
                            <telerik:RadComboBoxItem Text="All Clients" Value="B2P" Font-Italic="true" />
                        </Items>
                    </telerik:RadComboBox>
                    <br /><br />
                </asp:Panel>
     
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch"  EventName="Click" /></Triggers>
                    <ContentTemplate>
                        <span style="font-style:italic;"> <asp:Label ID="lblMessage" runat="server" Visible="true" /></span>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <div id="SearchCriteria">
                    <table cellpadding="2" cellspacing="3" width="100%">
                        <tr>
                            <td>Username:&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtUserName" runat="server" Width="125px" CssClass="inputText" MaxLength="30"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                    runat="server" TargetControlID="txtUserName" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" />
                            </td>
                            <td>E-mail Address:&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtEmailAddress" runat="server" Width="300px" MaxLength="50" CssClass="inputText"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                    runat="server" TargetControlID="txtEmailAddress" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                     ValidChars="@._" />
                            </td>
                            <td>
                                Account Number:&nbsp;&nbsp;&nbsp;                
                                <asp:TextBox ID="txtAccountNumber" runat="server" Width="125px" MaxLength="40" CssClass="inputText"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                    runat="server" TargetControlID="txtAccountNumber" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                     ValidChars="# _/" />
                            </td>                     
                            <td>
                                Phone Number:&nbsp;&nbsp;&nbsp;                
                                <asp:TextBox id="txtPhoneNumber" runat="server" Width="125px" MaxLength="20" CssClass="inputText"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                    runat="server" TargetControlID="txtPhoneNumber" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                     ValidChars="()-" />
                            </td>                     
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <p style="padding-top:10px;">
                                <des:ImageButton ID="btnSearch" runat="server" CausesValidation="False" AlternateText="Search" ImageUrl="/images/btnSearch.jpg" OnClick="btnSearch_Click" />&nbsp; 
                                <des:ImageButton ID="btnClear" runat="server" CausesValidation="False" AlternateText=" Clear " ImageUrl="/images/btnClear.jpg" />&nbsp;   
                                </p>
                            </td>
                        </tr>
                    </table>
                    <br />
                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch"  EventName="Click" /></Triggers>
                        <ContentTemplate>
                            <telerik:RadGrid ID="RadGrid1" runat="server" OnItemCreated="RadGrid1_ItemCreated"
                                AutoGenerateColumns="False" GridLines="None" Skin="WebBlue"  ExportSettings-ExportOnlyData="true"
                                ShowFooter="True" AllowSorting="True" ExportSettings-OpenInNewWindow="true">
                                <MasterTableView CommandItemDisplay="Top" >
                                    <CommandItemSettings  
                                            ShowRefreshButton="false"
                                            ShowAddNewRecordButton="false"
                                            ShowExportToWordButton="false"
                                            ShowExportToExcelButton="true" 
                                            ShowExportToCsvButton="true"
                                            ShowExportToPdfButton="false"/>

                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>

                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    
                                    <Columns>
                                        <telerik:GridButtonColumn CommandName="viewProfileDetail" Text="Edit" 
                                            UniqueName="column1">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="Profile_ID" 
                                            HeaderText="Profile ID" UniqueName="column2" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Username" 
                                            HeaderText="Username" UniqueName="column3" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FirstName" HeaderText="First Name" 
                                            UniqueName="column4" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" 
                                            UniqueName="column5" HtmlEncode="true">
                                        </telerik:GridBoundColumn>                    
                                        <telerik:GridBoundColumn DataField="EmailAddress" HeaderText="Email Address"
                                            UniqueName="column6" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" 
                                            UniqueName="column7" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </div>
            </div>
        </div> 
        <custom:timeout ID="timeoutControl" runat="server" />

    </form>      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>
</body>
</html>
