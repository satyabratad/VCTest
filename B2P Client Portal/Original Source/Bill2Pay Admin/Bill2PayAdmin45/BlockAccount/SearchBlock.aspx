<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SearchBlock.aspx.vb" Inherits="Bill2PayAdmin45.SearchBlock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Block Account :: Bill2Pay Administration</title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../css/content_normal.css"rel="stylesheet" type="text/css"  title="normal" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
             
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Search Account Block</div>
            <div class="header_border"></div>
        
            <!--Start Main Content-->
            <div class="main_content">
                <asp:Panel ID="pnlClient" runat="server">
                    <telerik:RadComboBox ID="ddlAUClient" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="8" AutoPostBack="true" >
                        <Items>
                            <telerik:RadComboBoxItem Text="All Clients" Value="" Font-Italic="true" />
                        </Items>
                    </telerik:RadComboBox>
                    <br /><br />
                </asp:Panel>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch"  EventName="Click" /></Triggers>
                    <ContentTemplate>
                        <span style="font-style:italic;"><asp:Label ID="lblMessage" runat="server" Visible="true" /></span>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div id="SearchCriteria">
                    <table style="width:95%;border-collapse: separate; border-spacing: 5px;padding: 5px;"> 
                        <tr style="height:30px;vertical-align: bottom;">
                            <td colspan="6">
                                <table style="width:800px">
                                    <tr>
                                        <td colspan="7">Date Range</td>
                                    </tr>
                                    <tr>                                                                                            
                                        <td style="width:40px;">From:</td>
                                        <td style="width:150px;">
                                            <des:DateTextBox ID="txtStart" runat="server" CssClass="inputText" AutoHint="false">
                                                <PopupCalendar ToggleImageUrl="/images/Calendar.jpg">
                                                    <Calendar CssClass=""  runat="server" BackColor="#F5F6EE" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                    Font-Names="Verdana" Font-Size="X-Small" BackImageUrl="/images/bg_form.png"
                                                                    JumpBackButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmd2SolidGray.GIF" 
                                                                    JumpForwardButtonImageUrl="{APPEARANCE}/Date And Time/RightCmd2SolidGray.GIF" 
                                                                    NextMonthButtonImageUrl="{APPEARANCE}/Date And Time/RightCmdSolidGray.GIF" 
                                                                    PrevMonthButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmdSolidGray.GIF">
                                                        <PopupMonthYearPicker>
                                                            <MonthYearPicker PrevYearsButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmdSolidGray.GIF" NextYearsButtonImageUrl="{APPEARANCE}/Date And Time/RightCmdSolidGray.GIF"></MonthYearPicker>
                                                        </PopupMonthYearPicker>
                                                    </Calendar>
                                                </PopupCalendar>
                                            </des:DateTextBox>
                                        </td>
                    
                                        <td style="width:25px;">To:</td>
                                        <td style="width:150px;">
                                            <des:DateTextBox ID="txtEnd" runat="server" CssClass="inputText" AutoHint="false">
                                                <PopupCalendar ToggleImageUrl="/images/Calendar.jpg">
                                                    <Calendar CssClass="" runat="server" BackColor="#EFF1DA" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                    Font-Names="Verdana" Font-Size="X-Small"
                                                                    JumpBackButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmd2SolidGray.GIF" 
                                                                    JumpForwardButtonImageUrl="{APPEARANCE}/Date And Time/RightCmd2SolidGray.GIF" 
                                                                    NextMonthButtonImageUrl="{APPEARANCE}/Date And Time/RightCmdSolidGray.GIF" 
                                                                    PrevMonthButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmdSolidGray.GIF">
                                                        <PopupMonthYearPicker>
                                                            <MonthYearPicker PrevYearsButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmdSolidGray.GIF" NextYearsButtonImageUrl="{APPEARANCE}/Date And Time/RightCmdSolidGray.GIF"></MonthYearPicker>
                                                        </PopupMonthYearPicker>
                                                    </Calendar>
                                                </PopupCalendar>                       
                                            </des:DateTextBox>
                                        </td>

                                        <td style="width:50px;">Status:</td>
                                        <td style="width:200px;">
                                            <telerik:RadComboBox ID="ddlStatus" Runat="server" DataTextField="name" DataValueField="value" />
                                            <asp:XmlDataSource ID="xmlStatus" runat="server" DataFile="~/resource/BlockStatus.xml" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr style="height:30px;vertical-align: top;">
                            <td colspan="6">
                                <table style="width:800px;">
                                    <tr>                                        
                                        <td style="width:35px;vertical-align: top;">Product: </td>
                                        <td style="width:200px;vertical-align: top;">
                                            <div id="divProduct" class="noborder">
                                                <telerik:RadComboBox ID="ddlProductList" runat="server"  onSelectedIndexChanged="ddlProductList_SelectedIndexChanged" 
                                                                AutoPostBack="true" CausesValidation="false" AppendDataBoundItems="true">                               
                                                </telerik:RadComboBox>
                                            </div>
                                        </td>
                                        <td style="width:170px;vertical-align: top; text-align:left;">
                                            <asp:TextBox id="txtProduct1Input" runat="server" Width="150px" MaxLength="40"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" TargetControlID="txtProduct1Input" 
                                                    FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                            </asp:FilteredTextBoxExtender>
                                
                                            <asp:TextBoxWatermarkExtender 
                                                ID="TextBoxWatermarkExtender1" 
                                                runat="server" 
                                                Enabled="True" 
                                                TargetControlID="txtProduct1Input" 
                                                WatermarkCssClass="watermarked" 
                                                WatermarkText="Account Number 1"> 
                                            </asp:TextBoxWatermarkExtender>
                                            <%--<des:RequiredTextValidator ID="reqProduct1" runat="server" ControlIDToEvaluate="txtProduct1Input"  Group="Product1" ErrorMessage="*" />--%>
                                        </td>
                             
                                        <td style="width:170px;vertical-align: top;">
                                            <asp:TextBox id="txtProduct1aInput" runat="server" Width="150px"  MaxLength="40"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True" TargetControlID="txtProduct1aInput" 
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'/">
                                            </asp:FilteredTextBoxExtender>
                                
                                            <asp:TextBoxWatermarkExtender 
                                                ID="TextBoxWatermarkExtender11" 
                                                runat="server" 
                                                Enabled="True" 
                                                TargetControlID="txtProduct1aInput" 
                                                WatermarkCssClass="watermarked" 
                                                WatermarkText="Account Number 2"> 
                                            </asp:TextBoxWatermarkExtender>
                                        </td>
                             
                                        <td style="width:170px;vertical-align: top;">
                                            <asp:TextBox id="txtProduct1bInput" runat="server" Width="150px" MaxLength="40"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True" TargetControlID="txtProduct1bInput" 
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'/">
                                            </asp:FilteredTextBoxExtender>
                                
                                            <asp:TextBoxWatermarkExtender 
                                                ID="TextBoxWatermarkExtender12" 
                                                runat="server" 
                                                Enabled="True" 
                                                TargetControlID="txtProduct1bInput" 
                                                WatermarkCssClass="watermarked" 
                                                WatermarkText="Account Number 3"> 
                                            </asp:TextBoxWatermarkExtender>
                                
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;</td>
                            <td style="margin-right:4px;text-align:right" colspan="5">
                                <des:ImageButton ID="btnSearch" runat="server" CausesValidation="False" OnClick="btnSearch_Click" Text="Search" ImageUrl="/images/btnSearch.jpg" />
                                    &nbsp; 
                                <des:ImageButton ID="btnClear" runat="server" CausesValidation="False" Text=" Clear " ImageUrl="/images/btnClear.jpg" />&nbsp;   
                            </td>                        
                        </tr>
                    </table>
                    <br />
                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch"  EventName="Click" /></Triggers>
                        <ContentTemplate>  
                            <telerik:RadGrid ID="RadGrid1" 
                                                runat="server" 
                                                OnItemCreated="RadGrid1_ItemCreated"
                                                Skin="WebBlue"  
                                                ExportSettings-ExportOnlyData="true" 
                                                AutoGenerateColumns="false"
                                                ShowFooter="True" 
                                                AllowSorting="True" 
                                                ExportSettings-OpenInNewWindow="true">

                                <MasterTableView CommandItemDisplay="Top" >
                                    <CommandItemSettings  
                                            ShowRefreshButton="false"
                                            ShowAddNewRecordButton="false"
                                            ShowExportToWordButton="false"
                                            ShowExportToExcelButton="true" 
                                            ShowExportToCsvButton="true"
                                            ShowExportToPdfButton="false" />

                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>

                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </ExpandCollapseColumn>

                                    <Columns>
                                        <telerik:GridButtonColumn CommandName="viewBlock" Text="Edit" 
                                            UniqueName="column10">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="ReleaseBlock" Text="Unblock" 
                                            UniqueName="column11">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="BlockedAccounts_ID" HeaderText="Confirmation Number" 
                                            UniqueName="column12" HtmlEncode="true" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ProductName" HeaderText="Product" 
                                            UniqueName="column1" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountNumber1" HeaderText="Account Number" 
                                            UniqueName="column2" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountNumber2" HeaderText="Account Number2" 
                                            UniqueName="column3" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountNumber3" HeaderText="Account Number3" 
                                            UniqueName="column4" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Customer Name" 
                                            UniqueName="column5" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BlockAdmin" HeaderText="Blocked By" 
                                            UniqueName="column6" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BlockDate" DataFormatString="{0:d}" 
                                            HeaderText="Block Date" UniqueName="column7" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BlockType" HeaderText="Block Type" 
                                            UniqueName="column8" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReleaseDate" DataFormatString="{0:d}" 
                                            HeaderText="Release Date" UniqueName="column9" HtmlEncode="true">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </form>      

    <div class="clear"></div> 
    <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
    </div>
</body>
</html>
