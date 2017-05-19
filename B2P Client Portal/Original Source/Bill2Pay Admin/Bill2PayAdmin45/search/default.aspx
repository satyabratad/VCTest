<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Bill2PayAdmin45._default5" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Search :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" /> 

    <script type="text/javascript">
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ExportToExcelButton ") >= 0)
                args.set_enableAjax(false);
        }
    </script>
</head>

<body>
    <form id="form1" runat="server" >
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
  
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Search - Find a Transaction</div>
            <div class="header_border"></div>
       
            <!--StartContent-->
            <div class="main_content">
                <asp:Panel ID="pnlClient" runat="server">
                    <telerik:RadComboBox ID="ddlAUClient" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="8" AutoPostBack="true" >
                      <Items>
                            <telerik:RadComboBoxItem Text="All Clients" Value="B2P" Font-Italic="true" />
                      </Items>
                    </telerik:RadComboBox>&nbsp;<br /><br />
                </asp:Panel>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch"  EventName="Click" /></Triggers>
                    <ContentTemplate>
                        <span style="font-style:italic;"><asp:Label ID="lblMessage" runat="server" Visible="true" /></span>
                    </ContentTemplate>
                </asp:UpdatePanel>
    
                <div id="SearchCriteria">
                    <table width="100%" cellpadding="2">
                        <tr>
                            <td>From:</td>
                            <td>
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
                    
                            <td>
                                To:
                            </td>
                            <td>
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
                    
                      <td>
                          Total Amount:
                      </td>
                      <td>
                        <asp:TextBox ID="txtTotalAmount" runat="server" Width="125px" MaxLength="8" CssClass="inputText"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" 
                                runat="server" TargetControlID="txtTotalAmount" FilterType="Custom, Numbers" 
                                 ValidChars="." />
                      </td>
                    
                      <td>
                          Payment Amount:
                      </td>
                      <td>
                        <asp:TextBox ID="txtPaymentAmount" runat="server" Width="125px" MaxLength="8" CssClass="inputText"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" TargetControlID="txtPaymentAmount" FilterType="Custom, Numbers" 
                                 ValidChars="." />
                      </td>
                     <td>
                          Payment Type:
                      </td>
                      <td>
                          <telerik:RadComboBox ID="ddlPaymentType" Runat="server" DataTextField="name" DataValueField="value">
                          </telerik:RadComboBox>
                          <asp:XmlDataSource ID="xmlPaymentType" runat="server" DataFile="~/resource/paymentType.xml" />
                      </td>
                      
                    </tr>
                    <tr>
                    <td>
                          Confirmation #:
                      </td>
                      <td>
                        <asp:TextBox ID="txtConfirmation" runat="server" Width="125px" CssClass="inputText" MaxLength="10"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                runat="server" TargetControlID="txtConfirmation" FilterType="Numbers" />
                      </td>
                    
                      <td>
                          First Name:
                      </td>
                      <td>
                        <asp:TextBox ID="txtFirstName" runat="server" Width="125px" MaxLength="20" CssClass="inputText"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="tbBankAccountFirstName_FilteredTextBoxExtender3" 
                                runat="server" TargetControlID="txtFirstName" FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                 ValidChars=" ,'&" />
                      </td>
                        <td>
                            Last Name:
                      </td>
                      <td>
                        <asp:TextBox ID="txtLastName" runat="server" Width="125px" MaxLength="30" CssClass="inputText"></asp:TextBox>
                           <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                runat="server" TargetControlID="txtLastName" FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                 ValidChars=" ,'&" />
                      </td>
                      <td>
                        Account Number:
                      </td>
                      <td>
                        <asp:TextBox ID="txtAccountNumber" runat="server" Width="125px" MaxLength="40" CssClass="inputText"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                runat="server" TargetControlID="txtAccountNumber" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                 ValidChars="# _/-" />
                      </td>
                      <td>
                          Source:
                      </td>
                      <td>
                          <telerik:RadComboBox ID="ddlSource" Runat="server" DataTextField="name" DataValueField="value" >
                          </telerik:RadComboBox>
                          <asp:XmlDataSource ID="xmlSource" runat="server" DataFile="~/resource/paymentSource.xml" />
                      </td>
                    </tr>
                    <tr>
                        <td>
                            CC First 6:
                      </td>
                      <td>
                        <asp:TextBox ID="txtCCFirst6" runat="server" Width="125px" MaxLength="6" CssClass="inputText"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                runat="server" TargetControlID="txtCCFirst6" FilterType="Numbers" />
                      </td>
                      <td>
                          CC Last 4:
                      </td>
                      <td>
                        <asp:TextBox ID="txtCCLast4" runat="server" Width="125px" MaxLength="4" CssClass="inputText"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                runat="server" TargetControlID="txtCCLast4" FilterType="Numbers" />
                      </td>
                      <td>
                          eCheck Last 4:
                      </td>
                      <td>
                        <asp:TextBox ID="txtECheck" runat="server" Width="125px" MaxLength="4" CssClass="inputText"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                                runat="server" TargetControlID="txtECheck" FilterType="Numbers" />
                      </td>
                     <td>
                          Product Type:
                      </td>
                      <td>
                          <telerik:RadComboBox ID="ddlProduct" Runat="server">
                          </telerik:RadComboBox>
                      </td>
                          
                      
                      <td>Notes:</td>
                      <td><asp:CheckBox ID="ckNotes" runat="server" ToolTip="Includes notes in search" /></td>
                      </tr>
                      <tr>
                      <td style="text-align:right" colspan="26">
                          <des:ImageButton ID="btnSearch" runat="server" CausesValidation="False" OnClick="btnSearch_Click" Text="Search" ImageUrl="/images/btnSearch.jpg" />&nbsp; 
                          <des:ImageButton ID="btnClear" runat="server" CausesValidation="False" Text=" Clear " ImageUrl="/images/btnClear.jpg" />&nbsp;   
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
                                        ShowExportToPdfButton="false"
                    
                                        />

                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>

                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>

                                <Columns>
                                    <telerik:GridButtonColumn CommandName="viewTransaction" Text="Edit" 
                                        UniqueName="column7">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="ConfirmationNumber" 
                                        HeaderText="Confirmation Number" UniqueName="column1" HtmlEncode="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TransactionDate" DataFormatString="{0:d}" 
                                        HeaderText="Transaction Date" UniqueName="column2" HtmlEncode="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ProductName" HeaderText="Product" 
                                        UniqueName="column3" HtmlEncode="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AccountNumber1" HeaderText="Account Number" 
                                        UniqueName="column4" HtmlEncode="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PayeeFirstName" HeaderText="First Name" 
                                        UniqueName="column5" HtmlEncode="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PayeeLastName" HeaderText="Last Name" 
                                        UniqueName="column6" HtmlEncode="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Amount" HeaderText="Payment Amount" DataType="System.Decimal" DataFormatString="{0:F2}" 
                                        UniqueName="column7" HtmlEncode="true">                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Status" HeaderText="Status" 
                                        UniqueName="column" Aggregate="Count" HtmlEncode="true">
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
                            <div id="processMessage"> Processing ...<br />
                                 
                            </div>
                      </ProgressTemplate>
        </asp:UpdateProgress>
        <custom:timeout runat="server" ID="timeout" />
        
    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
    </div>
</body>
</html>
