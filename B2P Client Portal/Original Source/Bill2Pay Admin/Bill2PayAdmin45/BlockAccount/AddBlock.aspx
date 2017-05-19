<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddBlock.aspx.vb" Inherits="Bill2PayAdmin45.AddBlock" %>

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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
             
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Add Account Block</div>
            <div class="header_border"></div>
        
            <!--Start Main Content-->
            <div class="main_content">

                <asp:UpdatePanel ID="BlockPanel1" runat="server">
                     <ContentTemplate>
                        <asp:Panel ID="pnlClient" runat="server">
                            <telerik:RadComboBox ID="ddlAUClient" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="0" AutoPostBack="true" >
                                <Items>
                                    <telerik:RadComboBoxItem Text="Select Client" Value="" Font-Italic="true" />
                                </Items>
                            </telerik:RadComboBox>
                            <br /><br />
                        </asp:Panel>

                        <asp:Panel ID="pnlProduct" runat="server">
                            <div>
                                <table style="width:95%;border-collapse: separate; border-spacing: 5px;padding: 5px;">  
                                    <asp:Panel ID="pnlMessages" runat="server" Visible="False">
                                        <tr>
                                            <td colspan="4" >
                                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                            </td>
                                        </tr>
                                    </asp:Panel>

                                    <tr>
                                        <td colspan="2"><h4>Step 1: Choose Product</h4></td>
                                        <asp:Panel ID="pnlHeaders" runat="server" Visible="false">
                                            <td><h4>Step 2: Enter Details</h4></td>
                                            <td><h4>Step 3: Add Block</h4></td>
                                        </asp:Panel>
                                    </tr>                      
                                    <tr style="height:25px;">
                                        <td style="width:5%;vertical-align: top;">Product:</td>
                                        <td style="width:15%;vertical-align: top;margin-left:auto;">
                                            <div id="divProduct" class="noborder">
                                                <telerik:RadComboBox ID="ddlProductList" runat="server"  onSelectedIndexChanged="ddlProductList_SelectedIndexChanged" 
                                                AutoPostBack="true" CausesValidation="false" AppendDataBoundItems="true" TabIndex="1">
                               
                                                </telerik:RadComboBox>
                                                <des:RequiredTextValidator ID="reqProducts" runat="server" ErrorMessage="Required" ControlIDToEvaluate="ddlProductList">
 		                                            <ErrorFormatterContainer>
                                                        <des:TextErrorFormatter Display="Dynamic" />
                                                    </ErrorFormatterContainer>
 		                                         </des:RequiredTextValidator>
                                            </div>
                                        </td>
                                        <td style="width:50%;">
                                            <asp:Panel ID="pnlAccount1" runat="server" Visible="false">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber1" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <des:RegexValidator ID="regAccountNumber1" runat="server" ControlIDToEvaluate="txtAccountNumber1">
                                                             <ErrorFormatterContainer>
                                                                    <des:TextErrorFormatter Display="Dynamic" />
                                                                </ErrorFormatterContainer>
                                                            </des:RegexValidator>
                                                            <des:RequiredTextValidator ID="reqAccountNumber1" runat="server" ControlIDToEvaluate="txtAccountNumber1">
                                                             <ErrorFormatterContainer>
                                                                    <des:TextErrorFormatter Display="Dynamic" />
                                                                </ErrorFormatterContainer>
                                                             </des:RequiredTextValidator>
                                                            <div id="divAccount1">
                                                                <des:TextBox ID="txtAccountNumber1" runat="server" MaxLength="10" Width="180px" DisablePaste="true" TabIndex="2"></des:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtAccountNumber1_FilteredTextBoxExtender" 
                                                                    runat="server" TargetControlID="txtAccountNumber1" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                                </asp:FilteredTextBoxExtender>
                                                            </div>
                                                        </td>     
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlAccount2" runat="server" Visible="false">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber2" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <des:RegexValidator ID="regAccountNumber2" runat="server" ControlIDToEvaluate="txtAccountNumber2">
                                                                <ErrorFormatterContainer>
                                                                    <des:TextErrorFormatter Display="Dynamic" />
                                                                </ErrorFormatterContainer>
                                                            </des:RegexValidator>
                                                            <des:RequiredTextValidator ID="reqAccountNumber2" runat="server" ControlIDToEvaluate="txtAccountNumber2">
                                                                <ErrorFormatterContainer>
                                                                    <des:TextErrorFormatter Display="Dynamic" />
                                                                </ErrorFormatterContainer>
                                                            </des:RequiredTextValidator>
                                        
                                                            <div id="divAccount2">
                                                                <des:TextBox ID="txtAccountNumber2" runat="server" MaxLength="10" Width="180px" DisablePaste="true" DisableAutoComplete="true" TabIndex="4"></des:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtAccountNumber2_FilteredTextBoxExtender" 
                                                                    runat="server" TargetControlID="txtAccountNumber2" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                                </asp:FilteredTextBoxExtender>
                                                            </div>
                                                            </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlAccount3" runat="server" Visible="false">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber3" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <des:RegexValidator ID="regAccountNumber3" runat="server" ControlIDToEvaluate="txtAccountNumber3">
                                                             <ErrorFormatterContainer>
                                                                <des:TextErrorFormatter Display="Dynamic" />
                                                            </ErrorFormatterContainer>
                                                            </des:RegexValidator>
                                                            <des:RequiredTextValidator ID="reqAccountNumber3" runat="server" ControlIDToEvaluate="txtAccountNumber3">
                                                             <ErrorFormatterContainer>
                                                                <des:TextErrorFormatter Display="Dynamic" />
                                                            </ErrorFormatterContainer>
                                                            </des:RequiredTextValidator>
                                                            <div id="divAccount3">
                                                                <des:TextBox ID="txtAccountNumber3" runat="server" MaxLength="10" Width="180px" DisablePaste="true" DisableAutoComplete="True" TabIndex="6"></des:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtAccountNumber3_FilteredTextBoxExtender" 
                                                                    runat="server" TargetControlID="txtAccountNumber3" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                                </asp:FilteredTextBoxExtender>
                                                            </div>
                                                            </td>   
                                                    </tr>
                                                </table>
                                            </asp:Panel>                        
                                        </td>
                                        <td style="margin-left:auto;">
                                            <asp:Panel ID="pnlLookupButton" runat="server" Visible="false">
                                                <des:ImageButton ImageUrl="/images/btnLookupAccount.jpg" ID="btnLookup" runat="server" ToolTip="Lookup Account" CssClass="formButton" TabIndex="20" />
                                            </asp:Panel>
                                            <asp:Panel ID="pnlButton" runat="server" Visible="false">
                                                <des:ImageButton ImageUrl="/images/btnAddBlock.jpg" ID="btnSubmit" runat="server" ToolTip="Add Block" CssClass="formButton" TabIndex="22" />
                                            </asp:Panel>    
                                            <asp:Panel ID="pnlClear" runat="server" Visible="false"> 
                                                <des:ImageButton ImageUrl="/images/btnClear.jpg" ID="btnClear" runat="server" ToolTip="Clear Entries" CssClass="formButton" OnClick="btnClear_Click" CausesValidation="false" TabIndex="24" />
                                            </asp:Panel> 
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pnlReEnterAccount" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="2"></td>
                                            <td><h4>Re-Enter Details</h4></td>
                                            <td></td>
                                        </tr> 
                                        <tr>
                                            <td style="width:5%;"></td>
                                            <td style="width:15%;"></td>
                                            <td style="width:50%;">
                                                
                                                <asp:Panel ID="pnlAccount1r" runat="server" Visible="false">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber1r" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAccountNumber1r" runat="server" MaxLength="10" Width="180px" onCopy="return false" onPaste="return false" onDrag="return false" onDrop="return false" TabIndex="6"></asp:TextBox>
                                                            <des:RegexValidator ID="regAccountNumber1r" runat="server" ControlIDToEvaluate="txtAccountNumber1r">
                                                                <ErrorFormatterContainer>
                                                                    <des:TextErrorFormatter Display="Dynamic" />
                                                                </ErrorFormatterContainer>
                                                            </des:RegexValidator>
                                                            <des:RequiredTextValidator ID="reqAccountNumber1r" runat="server" ControlIDToEvaluate="txtAccountNumber1r">
                                                                <ErrorFormatterContainer>
                                                                        <des:TextErrorFormatter Display="Dynamic" />
                                                                </ErrorFormatterContainer>
                                                            </des:RequiredTextValidator>
                                                            <des:CompareTwoFieldsValidator ID="CompareTwoFieldsValidator1" runat="server" ControlIDToEvaluate="txtAccountNumber1r" SecondControlIDToEvaluate="txtAccountNumber1" ErrorMessage="Account1 values do not match"></des:CompareTwoFieldsValidator>
                                                            <asp:FilteredTextBoxExtender ID="txtAccountNumber1r_FilteredTextBoxExtender" 
                                                                runat="server" TargetControlID="txtAccountNumber1r" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                            </asp:FilteredTextBoxExtender>
                                                          </td>     
                                                    </tr>
                                                </table>                                                
                                                </asp:Panel>
                                                <asp:Panel ID="pnlAccount2r" runat="server" Visible="false">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber2r" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                                <des:RegexValidator ID="regAccountNumber2r" runat="server" ControlIDToEvaluate="txtAccountNumber2r">
                                                                <ErrorFormatterContainer>
                                                                <des:TextErrorFormatter Display="Dynamic" />
                                                            </ErrorFormatterContainer>
                                                            </des:RegexValidator>
                                                            <des:RequiredTextValidator ID="reqAccountNumber2r" runat="server" ControlIDToEvaluate="txtAccountNumber2r">
                                                                <ErrorFormatterContainer>
                                                                <des:TextErrorFormatter Display="Dynamic" />
                                                            </ErrorFormatterContainer>
                                                            </des:RequiredTextValidator>
                                        
                                                            <div id="divAccount2r">                                                                
                                                                <asp:TextBox ID="txtAccountNumber2r" runat="server" MaxLength="10" Width="180px" oncopy="return false" onPaste="return false" onDrag="return false" onDrop="return false" TabIndex="10"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtAccountNumber2r_FilteredTextBoxExtender" 
                                                                    runat="server" TargetControlID="txtAccountNumber2r" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                                </asp:FilteredTextBoxExtender>
                                                                <des:CompareTwoFieldsValidator ID="CompareTwoFieldsValidator2" runat="server" ControlIDToEvaluate="txtAccountNumber2r" SecondControlIDToEvaluate="txtAccountNumber2" ErrorMessage="Account2 values do not match"></des:CompareTwoFieldsValidator>
                                                            </div>
                                                            </td>
                                                    </tr>
                                                </table>    
                                                </asp:Panel>    
                                                <asp:Panel ID="pnlAccount3r" runat="server" Visible="false">                                        
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber3r" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <des:RegexValidator ID="regAccountNumber3r" runat="server" ControlIDToEvaluate="txtAccountNumber3r">
                                                             <ErrorFormatterContainer>
                                                                <des:TextErrorFormatter Display="Dynamic" />
                                                            </ErrorFormatterContainer>
                                                            </des:RegexValidator>
                                                            <des:RequiredTextValidator ID="reqAccountNumber3r" runat="server" ControlIDToEvaluate="txtAccountNumber3r">
                                                             <ErrorFormatterContainer>
                                                                <des:TextErrorFormatter Display="Dynamic" />
                                                            </ErrorFormatterContainer>
                                                            </des:RequiredTextValidator>
                                                            <div id="divAccount3r">
                                                                <asp:TextBox ID="txtAccountNumber3r" runat="server" MaxLength="10" Width="180px" oncopy="return false" onPaste="return false" onDrag="return false" onDrop="return false" TabIndex="12"></asp:TextBox>                                                                
                                                                <asp:FilteredTextBoxExtender ID="txtAccountNumber3r_FilteredTextBoxExtender" 
                                                                    runat="server" TargetControlID="txtAccountNumber3r" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                                </asp:FilteredTextBoxExtender>
                                                                <des:CompareTwoFieldsValidator ID="CompareTwoFieldsValidator3" runat="server" ControlIDToEvaluate="txtAccountNumber3r" SecondControlIDToEvaluate="txtAccountNumber3" ErrorMessage="Account3 values do not match"></des:CompareTwoFieldsValidator>
                                                            </div>
                                                            </td>   
                                                    </tr>
                                                </table>
                                                </asp:Panel>   
                                            </td>
                                            <td></td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </div>
                        </asp:Panel>
                        
                        <asp:Panel ID="pnlBlockDetail" runat="server" Visible="false">
                            <div>
                                <table style="width:95%;border-collapse: separate; border-spacing: 5px;padding: 5px;">  
                                    <tr>
                                        <td colspan="4">
                                            <des:CountTrueConditionsValidator ID="CountTrueConditionsValidator1" runat="server" Minimum="1" ErrorMessage="Pick at least one Payment Method" >
                                                <Conditions>
                                                    <des:CheckStateCondition ControlIDToEvaluate="cbEcheck" />
                                                    <des:CheckStateCondition ControlIDToEvaluate="cbCreditCard" />
                                                </Conditions>
                                            </des:CountTrueConditionsValidator>
                                        </td>
                                    </tr>
                                    
                                    <asp:Panel ID="pnlMessageDetail" runat="server" Visible="False">
                                        <tr>
                                            <td colspan="2"></td>
                                            <td colspan="2" >
                                                <asp:Label ID="lblMessageDetail" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </asp:Panel>

                                    <tr style="height:25px;">
                                        <td style="width:5%;vertical-align: top;">Product:</td>
                                        <td style="width: 15%;vertical-align: top;margin-left:auto;"><asp:Label ID="lblProduct" runat="server" Text=""></asp:Label></td>
                                        <td style="width:50%;">                                             
                                            <asp:Panel ID="pnlAccount1d" runat="server" Visible="false">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber1d" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td><des:TextBox ID="txtAccountNumber1d" runat="server" MaxLength="10" Width="180px" Enabled="false"></des:TextBox></td>
                                                    </tr>
                                                </table>

                                            </asp:Panel>                                          
                                            <asp:Panel ID="pnlAccount2d" runat="server" Visible="false">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber2d" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td><des:TextBox ID="txtAccountNumber2d" runat="server" MaxLength="10" Width="180px" Enabled="false"></des:TextBox></td>
                                                    </tr>
                                                </table>

                                            </asp:Panel>                                          
                                            <asp:Panel ID="pnlAccount3d" runat="server" Visible="false">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:25%;">
                                                            <asp:Label ID="lblAccountNumber3d" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td><des:TextBox ID="txtAccountNumber3d" runat="server" MaxLength="10" Width="180px" Enabled="false"></des:TextBox></td>
                                                    </tr>
                                                </table>

                                            </asp:Panel>
                                        </td>
                                        <td style="margin-left:auto"></td>
                                    </tr> 
                                    <asp:Panel ID="pnlProfile" runat="server" Visible="false">
                                        <tr><td colspan="4">&nbsp;</td></tr>
                                        <tr>
                                            <td colspan="3"><asp:Label ID="lblAutoPay" runat="server" ForeColor="Black" Text="" Width="400px"></asp:Label></td>
                                            <td><asp:LinkButton ID="lnkBtnProfile" runat="server" >Profile Record</asp:LinkButton></td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDetail" runat="server" Visible="true"> 
                                        <tr><td colspan="4">&nbsp;</td></tr>
                                        <tr>
                                            <td colspan="4">
                                                <table>                                 
                                                    <tr>                                    
                                                        <td style="width:25%;vertical-align: top;"></td>
                                                        <td style="vertical-align:top;margin-left:auto;"></td>
                                                    </tr>                           
                                                    <tr>                                    
                                                        <td style="width:25%;vertical-align: top;">First Name: </td>
                                                        <td style="vertical-align:top;margin-left:auto;">                                                            
                                                            <des:TextBox ID="txtFirstName" runat="server" MaxLength="20" Width="180px"></des:TextBox>
                                                            <asp:FilteredTextBoxExtender 
                                                                ID="txtFirstName_FilteredTextBoxExtender" 
                                                                runat="server" 
                                                                TargetControlID="txtFirstName" 
                                                                Enabled="True" 
                                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                                ValidChars=" '">
                                                            </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>                                  
                                                    <tr>                                    
                                                        <td style="width:25%;vertical-align: top;">Last Name: </td>
                                                        <td style="vertical-align:top;margin-left:auto;">                                                            
                                                            <des:TextBox ID="txtLastName" runat="server" MaxLength="30" Width="180px"></des:TextBox>
                                                            <asp:FilteredTextBoxExtender 
                                                                ID="txtLastName_FilteredTextBoxExtender" 
                                                                runat="server" 
                                                                TargetControlID="txtLastName" 
                                                                Enabled="True" 
                                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                                ValidChars=" '"/>
                                                        </td>
                                                    </tr>  
                                                    <tr>
                                                        <td colspan="2" style="margin-left:auto;">&nbsp;</td>
                                                    </tr>                                
                                                    <tr>                                    
                                                        <td style="width:25%;vertical-align: top;">Payment Method: </td>
                                                        <td style="vertical-align:top;margin-left:auto;">
                                                            <asp:CheckBox ID="cbEcheck" Text="" runat="server" Checked="false" />&nbsp;&nbsp;eCheck
                                                        </td>
                                                    </tr>                                    
                                                    <tr>                                    
                                                        <td style="width:25%;vertical-align: top;"></td>
                                                        <td style="vertical-align:top;margin-left:auto;">
                                                            <asp:CheckBox ID="cbCreditCard" Text="" runat="server" Checked="false" />&nbsp;&nbsp;Credit/Debit Card
                                                        </td>
                                                    </tr>  
                                                    <tr>
                                                        <td colspan="2" style="margin-left:auto;">&nbsp;</td> 
                                                    </tr>                                
                                                    <tr>                                    
                                                        <td style="width:25%;vertical-align: top;">Email Address: </td>
                                                        <td style="vertical-align:top;margin-left:auto;">                                                            
                                                            <des:TextBox ID="txtEmailAddress" runat="server" MaxLength="100" Width="250px"></des:TextBox>
                                                            <asp:FilteredTextBoxExtender 
                                                                ID="txtEmailAddress_FilteredTextBoxExtender" 
                                                                runat="server" 
                                                                TargetControlID="txtEmailAddress" 
                                                                Enabled="True" 
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
                                                        <td style="width:25%;vertical-align: top;"></td>
                                                        <td style="vertical-align:top;margin-left:auto;">
                                                            <asp:CheckBox ID="cbSendEmail" Text="" runat="server" />&nbsp;&nbsp;Send Email
                                                        </td>
                                                    </tr>  
                                                    <tr>
                                                        <td colspan="2" style="margin-left:auto;">&nbsp;</td>
                                                    </tr>                                
                                                    <tr>                                    
                                                        <td style="width:25%;vertical-align: top;">Comments:</td>
                                                        <td style="vertical-align:top;margin-left:auto;">
                                                            <des:TextBox ID="txtComments" runat="server" Rows="5" Width="250" TextMode="MultiLine" MaxLength="500"></des:TextBox>
                                                            <asp:FilteredTextBoxExtender 
                                                                ID="FilteredTextBoxExtender2" 
                                                                runat="server" 
                                                                Enabled="True" 
                                                                TargetControlID="txtComments" 
                                                                FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                                ValidChars="!,.$@-' "/>
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                        <td colspan="2" style="margin-left:auto;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="margin-left:auto;vertical-align: top;">
                                                            <des:ImageButton ImageUrl="/images/btnCancel.jpg" ID="btnCancel" runat="server" ToolTip="Cancel" CssClass="formButton" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                                                            <des:ImageButton ImageUrl="/images/btnSubmitBlock.jpg" ID="btnSubmitBlock" runat="server" ToolTip="Save Block" CssClass="formButton" />
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                        </tr>       
                                    </asp:Panel>
                                </table>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

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
    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
    </div>
</body>
</html>
