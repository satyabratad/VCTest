<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Profiles.aspx.vb" Inherits="Bill2PayAdmin45.Profiles1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Profile Search :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
      
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Profile Search</div>
            <div class="header_border"></div>
           
            <!--Start Main Content-->
            <div class="main_content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlClient" runat="server">
                            <telerik:RadComboBox ID="ddlAUClient" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="8" AutoPostBack="true" >
                                <Items>
                                    <telerik:RadComboBoxItem Text="All Clients" Value="B2P" Font-Italic="true" />
                                </Items>
                            </telerik:RadComboBox>
                            <br /><br />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch"  EventName="Click" />                                            
                    </Triggers>
                    
                    <ContentTemplate>
                        <span style="font-style:italic;"> <asp:Label ID="lblMessage" runat="server" Visible="true" /></span>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <div id="SearchCriteria">
                    <table cellpadding="2" cellspacing="6" width="100%">
                        <tr>
                            <td>Username:</td>
                            <td>    <asp:TextBox ID="txtUserName" runat="server" Width="125px" CssClass="inputText" MaxLength="30"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtUserName" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers"
                                     ValidChars=" ,.-'"  />
                            </td>  
                            <td>Last Name:</td>
                            <td>
                                <asp:TextBox ID="txtLastName" runat="server" Width="125px" CssClass="inputText" MaxLength="30"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtLastName" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers"
                                    ValidChars=" ,.-'" />
                            </td> 
                            <td>E-mail Address:</td>
                            <td>
                                <asp:TextBox ID="txtEmailAddress" runat="server" Width="300px" CssClass="inputText" MaxLength="50"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                    runat="server" TargetControlID="txtEmailAddress" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                     ValidChars="@._-" />
                            </td>                       
                        </tr>
                        <tr>
                            <td>
                                Account Number:</td>
                            <td>                
                                <asp:TextBox ID="txtAccountNumber" runat="server" Width="125px" CssClass="inputText" MaxLength="40"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                    runat="server" TargetControlID="txtAccountNumber" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                     ValidChars="# _/-" />
                            </td> 
                            <td>CC Last 4:</td>
                            <td>
                                <asp:TextBox ID="txtCCLast4" runat="server" Width="125px" CssClass="inputText" MaxLength="30"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCCLast4" FilterType="Numbers" />
                            </td>  
                            <td>eCheck Last 4:</td>
                            <td>
                                <asp:TextBox ID="txtECheck" runat="server" Width="125px" CssClass="inputText" MaxLength="30"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtECheck" FilterType="Numbers" />
                            </td> 
                            
                        </tr>
                        <tr>
                            <td valign="middle"><asp:CheckBox ID="ckAllProfiles" runat="server" Text=" Check here to display all profiles"  /></td>
                            <td align="left" colspan="5">
                                <p style="padding-top:10px;">
                               
                                <des:ImageButton ID="btnSearch" runat="server" CausesValidation="False" AlternateText="Search" ImageUrl="/images/btnSearch.jpg" OnClick="btnSearch_Click" />&nbsp; 
                                <des:ImageButton ID="btnClear" runat="server" CausesValidation="False" AlternateText=" Clear " ImageUrl="/images/btnClear.jpg" />&nbsp;   
                                 
                                </p>
                            </td>
                            <td>&nbsp;</td> 
                        </tr>
                    </table>
                    <br />
                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch"  EventName="Click" /></Triggers>
                        <ContentTemplate>
                            <telerik:RadGrid ID="RadGrid1" 
                                             runat="server" 
                                             OnItemCreated="RadGrid1_ItemCreated" 
                                             AllowPaging="true" 
                                             AutoGenerateColumns="False" 
                                             GridLines="None" 
                                             Skin="WebBlue" 
                                             ExportSettings-ExportOnlyData="true"
                                             ShowFooter="True" 
                                             AllowSorting="True" 
                                             ExportSettings-OpenInNewWindow="true" 
                                             pagesize="20" >
                                <MasterTableView CommandItemDisplay="Top" GroupsDefaultExpanded="false" >
                                    <GroupHeaderItemStyle  ForeColor="Black" />
                                    <GroupByExpressions>                                
                                        <telerik:GridGroupByExpression>                                
                                            <SelectFields>                                    
                                                <telerik:GridGroupByField FieldAlias="Username" FieldName="Username"></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="Username" SortOrder="Descending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>                               
                                
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
                                        <telerik:GridBoundColumn DataField="Profile_ID" HeaderText="Profile ID"
                                            UniqueName="column1a" HtmlEncode="true" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ClientCode" HeaderText="ClientCode" 
                                            UniqueName="column2" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Username" HeaderText="Username" 
                                            UniqueName="column3" HtmlEncode="true" Aggregate="Custom" FooterText="Total Profiles: ">
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
                                        <telerik:GridBoundColumn DataField="ProductName" HeaderText="Product" 
                                            UniqueName="column7" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountNumber1" HeaderText="Account1" 
                                            UniqueName="column8" HtmlEncode="true" >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountNumber2" HeaderText="Account2" 
                                            UniqueName="column9" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountNumber3" HeaderText="Account3" 
                                            UniqueName="column10" HtmlEncode="true">
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
        
        
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
                
            </div>
            <div id="processMessage"><img src="/images/loader.gif" style="border:none; vertical-align:middle; background-color:White;" alt="Page loader" />&nbsp;&nbsp; Processing ...<br />
                 
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

        <custom:timeout ID="timeoutControl" runat="server" />
    </form>      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>
</body>
</html>
