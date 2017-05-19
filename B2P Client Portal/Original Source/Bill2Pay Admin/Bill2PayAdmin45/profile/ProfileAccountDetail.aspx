<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProfileAccountDetail.aspx.vb" Inherits="Bill2PayAdmin45.ProfileAccountDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Profile Account Details :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/grid.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
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
            <div class="header">Profile Account Details</div>
            <div style="padding-left:10px"><asp:HyperLink ID="hypBack" runat="server" NavigateUrl="/profile/ProfileDetail.aspx" Text="&lt;&lt; Back to Profile Detail" /> </div>
            <div class="header_border"></div>
       
            <!--Start Main Content-->
            <div class="main_content">
                <div style="padding-left:20px;">
                <b>Client Code:&nbsp;<asp:Label ID="lblClientName" runat="server" /></b><br />
                <b>Name:&nbsp;<asp:Label ID="lblName" runat="server"></asp:Label></b><br />
                <b>User Name:&nbsp;<asp:Label ID="lblUsername" runat="server" /></b><br /><br />
               </div>
               <asp:UpdatePanel ID="updatePanel" runat="server">
               <ContentTemplate>
                    <span style="font-style:italic;"><asp:Label ID="lblMessage" runat="server" Visible="true" /></span>
                   
                    <div id="ProfileDetail">                        
                        <table cellpadding="2" width="100%">
                            <tr>
                                <td valign="top">
                                    <fieldset style="padding-top:0px;margin-top:0px; padding-left:10px;">
                                        <legend>Account Information</legend>
                                        <table cellpadding="2" cellspacing="6">
                                            <tr>
                                                <td>Product:</td>
                                                <td>&nbsp;<asp:Label ID="lblProduct" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Account 1:</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblAcct1" runat="server" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    NickName:&nbsp;<asp:Label ID="lblNickName" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Account 2:</td>
                                                <td>&nbsp;<asp:Label ID="lblAcct2" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Account 3:</td>
                                                <td>&nbsp;<asp:Label ID="lblAcct3" runat="server" /></td>
                                            </tr>
                                        </table>                                        
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <fieldset style="padding-top:0px;margin-top:0px; padding-left:10px;">
                                        <legend>Payment Option - AutoPay/Recurring</legend>
                                        <asp:Panel ID="pnlAutoPay" runat="server">
                                            <table cellpadding="2" cellspacing="6">
                                                <tr>
                                                    <td>Type:</td>
                                                    <td><asp:Label ID="lblType" runat="server" /></td>
                                                    <td>&nbsp;&nbsp;Interval:</td>
                                                    <td>&nbsp;<asp:Label ID="lblInterval" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td>Next Payment Date:</td>
                                                    <td>&nbsp;<asp:Label ID="lblNextPayDate" runat="server" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>Next Payment Amount:</td>
                                                    <td>&nbsp;<asp:Label ID="lblNexPayAmt" runat="server" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:LinkButton runat="server" CommandName="Delete" ID="btnCancelPayment" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this payment?');">Cancel Payment</asp:LinkButton> </td>
                                                </tr>
                                            </table> 
                                        </asp:Panel>
                                        <asp:Panel ID="pnlNoAutoPay" runat="server">
                                            <br />
                                            <p style="padding-left:10px;">No AutoPay/Recurring</p>
                                            <br />
                                        </asp:Panel>                                       
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <fieldset style="padding-top:0px;margin-top:0px; padding-left:10px;">
                                        <legend>Scheduled Payments</legend>
                                        <br />
                                        <asp:Panel ID="pnlScheduledPayments" runat="server">
                            
                                 <asp:GridView ID="gvScheduledPayments" runat="server" OnPageIndexChanging="gvScheduledPayments_PageIndexChanging"           
                                    AutoGenerateColumns="False" DataKeyNames="AccountPayment_ID"
                                    PageSize="6" Width="50%" GridLines="None"
                                    AllowPaging="true" RowStyle-Height="20px" FooterStyle-CssClass="gridFooter"
                                    CssClass="grid">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="AccountPayment_ID" Visible="false"  />                                          

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Payment Type"  ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:label ID="lblAccountNumber" runat="server" Text='<%#: Eval("WalletItem.Description") %>' ></asp:label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                               
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Payment Date"  ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:label ID="lblPaymentDate" runat="server" Text='<% #: formatDate(Eval("PaymentEvent.NextPaymentDate")) %>' ></asp:label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                       
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  HeaderText="Amount" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:label ID="lblAmount" runat="server" Text='<% #: formatAmount(Eval("Amount")) %>' ></asp:label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  

                                         <asp:TemplateField  ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                               <asp:LinkButton runat="server" CommandName="Delete" ID="btnDelete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this payment?');">Delete Payment</asp:LinkButton>                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                                
                                    </Columns>
                                    
                                   <PagerStyle CssClass="gridFooter"  />
                                    
                                                          
                                </asp:GridView>
                                            <br />
                            </asp:Panel>       
                                        <asp:Panel ID="pnlNoScheduledPayments" runat="server">
                                            <p style="padding-left:10px;">No scheduled payments.</p><br />
                                        </asp:Panel>                            
                                    </fieldset>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlStatementsNotify" runat="server">
                                <tr>
                                    <td valign="top">
                                        <fieldset style="padding-top:0px;margin-top:0px; padding-left:10px;">
                                            <legend>Statement Alerts</legend>
                                               <div style="padding-top:5px; padding-bottom:5px;">
                                                <asp:RadioButtonList ID="rdStatements" runat="server" AutoPostBack="true" />

                                                <asp:panel ID="pnlText" runat="server">
                                                    <asp:CheckBox ID="ckStatementAlertsText" CssClass="checkbox" AutoPostBack="true" runat="server" ToolTip="Statement Alerts via Text" Text=" Statement Alerts via Text"   /> 
                                                </asp:panel>
                                               </div>
                                                                         
                                        </fieldset>
                                    </td>
                                </tr>
                           </asp:Panel>   
                        </table>                    
                    </div>                    
               </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCancelPayment" EventName="Click" />
                    </Triggers>
               </asp:UpdatePanel> 
               
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
      <ProgressTemplate>
            <div id="progressBackgroundFilter">
                
            </div>
            <div id="processMessage"><img src="/images/loader.gif" style="border:none; vertical-align:middle; background-color:White;" alt="Page loader" />&nbsp;&nbsp; Processing ...<br />
                 
            </div>
      </ProgressTemplate>
    </asp:UpdateProgress>
           </div>
        </div> 
        <custom:timeout ID="timeoutControl" runat="server" />

    </form>      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>
</body>
</html>
