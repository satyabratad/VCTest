<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BlockDetail.aspx.vb" Inherits="Bill2PayAdmin45.BlockDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Block Account Details :: Bill2Pay Administration</title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../css/content_normal.css"rel="stylesheet" type="text/css"  title="normal" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
        
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
  
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Block Account Details</div>
            <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                <div style="padding-left:10px"><asp:HyperLink ID="hypBack" runat="server" NavigateUrl="~/BlockAccount/SearchBlock.aspx" Text="&lt;&lt; Back to search results" /> </div>
            </asp:Panel>
            <asp:Panel ID="pnlAdd" runat="server" Visible="false">
                <div style="padding-left:10px"><asp:HyperLink ID="hypAdd" runat="server" NavigateUrl="~/BlockAccount/AddBlock.aspx" Text="&lt;&lt; Back to Add Block" /> </div>
            </asp:Panel>
            
            <div class="header_border"></div>
       
            <!--Start Main Content-->
            <div class="main_content">                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ValidationSummary id="ValidationSummary1" 
                                            runat="server"  
                                            HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
                                            ScrollIntoView="Top" 
                                            CssClass="ErrorSummary" 
                                            DisplayMode="bulletList" />
                        <br />
                        <span style="padding-left:20px;">
                            <asp:Label ID="lblMessage" runat="server" Visible="true" />
                           <%-- <asp:Label ID="lblMessage2" Font-Italic="true" runat="server" ForeColor="Red" />--%>
                        </span>
                        <br />
                        <div id="BlockDetail">
                            <div style="padding-left:10px;">
                                <asp:ValidationSummary id="ValidationSummary2"  
                                                    runat="server" 
                                                    CssClass="ErrorSummary"  
                                                    HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
			                                        HyperLinkToFields="True" 
                                                    ScrollIntoView="Top"  
                                                    DisplayMode="bulletList" />

                            </div>
                            <table style="width:100%;padding:2px">
                                <tr>
                                    <td style="vertical-align:top;">
                                        <fieldset style="padding-top:0px;margin-top:0px;">
                                            <legend>Block Account Information - History</legend>
                                            <table style="padding:5px;">
                                                
                                                <tr>
                                                    <td colspan="2">
                                                        <telerik:RadGrid ID="RadGrid1" 
                                                                        runat="server" 
                                                                        
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
                                                                    ShowExportToExcelButton="false" 
                                                                    ShowExportToCsvButton="false"
                                                                    ShowExportToPdfButton="false" />

                                                                <RowIndicatorColumn>
                                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                                </RowIndicatorColumn>

                                                                <ExpandCollapseColumn>
                                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                                </ExpandCollapseColumn>

                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="BlockUser" HeaderText="Blocked By" 
                                                                        UniqueName="column1" HtmlEncode="true">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="BlockDate" DataFormatString="{0:d}" 
                                                                        HeaderText="Block Date" UniqueName="column2" HtmlEncode="true">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="ReleaseUser" HeaderText="Released By" 
                                                                        UniqueName="column4" HtmlEncode="true">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="ReleaseDate" DataFormatString="{0:d}" 
                                                                        HeaderText="Release Date" UniqueName="column5" HtmlEncode="true">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="BlockComment" HeaderText="Comments" 
                                                                        UniqueName="column3" HtmlEncode="true">
                                                                    </telerik:GridBoundColumn>
                                                                </Columns>

                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                                <tr><td colspan="2">&nbsp;</td></tr>
                                                <tr>
                                                    <td style="vertical-align:top">Blocked Account Number:</td>
                                                    <td><asp:Label ID="lblAccount" runat="server" Text="Label"></asp:Label></td>
                                                </tr>
                                                <tr><td colspan="2">&nbsp;</td></tr>
                                                
                                                <asp:Panel ID="pnlProfile" runat="server" Visible="false">
                                                    <tr><td colspan="2">&nbsp;</td></tr>
                                                    <tr>
                                                        <td colspan="2"><asp:Label ID="lblAutoPay" runat="server" ForeColor="Black" Text="" Width="400px"></asp:Label></td>
                                                        <%--<td><asp:LinkButton ID="lnkBtnProfile" runat="server"  >Profile Record</asp:LinkButton></td>--%>
                                                    </tr>
                                                    <tr><td colspan="2">&nbsp;</td></tr>
                                                </asp:Panel>
                                                
                                                <tr>
                                                    <td style="vertical-align:top">Blocked Payment Method:</td>
                                                    <td><asp:CheckBox ID="cbECheck" runat="server" Text="eCheck" /></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:CheckBox ID="cbCredit" runat="server" Text="Credit/Debit Card" />                                                        
                                                        <des:CountTrueConditionsValidator ID="CountTrueConditionsValidator1" runat="server" Minimum="1" ErrorMessage="Pick a least one Payment Method" >
                                                            <Conditions>
                                                                <des:CheckStateCondition ControlIDToEvaluate="cbECheck" />
                                                                <des:CheckStateCondition ControlIDToEvaluate="cbCredit" />
                                                            </Conditions>
                                                        </des:CountTrueConditionsValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp</td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align:top">Email confirmation sent to:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="inputText" Columns="40"></asp:TextBox>
                                                         <asp:FilteredTextBoxExtender 
                                                             ID="FilteredTextBoxExtender5" 
                                                             runat="server" 
                                                             TargetControlID="txtEmailAddress"
                                                             filtertype="UpperCaseLetters,LowerCaseLetters,Numbers,Custom" 
                                                             ValidChars="-@._"/>
                                                        <asp:RegularExpressionValidator 
                                                            ID="regEmail" 
                                                            runat="server" 
                                                            ValidationGroup="Profile"
                                                            ControlToValidate="txtEmailAddress" 
                                                            Display="Dynamic" 
                                                            ErrorMessage="Invalid Email Address" 
                                                            ToolTip="Invalid Email Address" 
                                                            SetFocusOnError="True" 
                                                            ValidationExpression="^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9_]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$" />
                                                        <des:RequiredTextValidator 
                                                            ID="RequiredTextValidator1" 
                                                            runat="server" 
                                                            ControlIDToEvaluate="txtEmailAddress" 
                                                            ErrorMessage="Email Address Required!">
                                                            <EnablerContainer>
                                                                <des:CheckStateCondition ControlIDToEvaluate="cbSendEmail" />
                                                            </EnablerContainer>
                                                        </des:RequiredTextValidator>                                                    
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td><asp:CheckBox ID="cbSendEmail" runat="server" Text=""/>&nbsp;&nbsp;<asp:Label ID="lblSendEmail" runat="server" Text="Send Email"></asp:Label></td>
                                                </tr>

                                                <asp:Panel ID="pnlComment" runat="server" Visible="false">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align:top">Comment:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" MaxLength="500" Width="250px" Height="100"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtComment" 
                                                                FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars="!,.$@-' "/>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlEdit" runat="server" Visible="false"> 
                                    <tr>
                                        <td>
                                            <des:ImageButton ImageUrl="/images/btnOK.jpg" ID="btnSave" runat="server" ToolTip="Save" CssClass="formButton" />
                                        </td>
                                    </tr>                                               
                                </asp:Panel> 
                                <asp:Panel ID="pnlUnBlock" runat="server" Visible="false">    
                                    <tr>
                                        <td>
                                            <des:ImageButton ImageUrl="/images/btnUnBlock.jpg" ID="btnUnBlock" runat="server" ToolTip="Un-Block" CssClass="formButton" />
                                            <des:ImageButton ImageUrl="/images/btnCancel.jpg" ID="btnCancel" runat="server" ToolTip="Cancel" CssClass="formButton" />
                                        </td>
                                    </tr>                                            
                                </asp:Panel> 
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
