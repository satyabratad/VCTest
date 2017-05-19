<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="edituser.aspx.vb" Inherits="Bill2PayAdmin45.edituser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Edit User :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
    
      <style type="text/css">
          .style14
          {
              height: 34px;
          }
      </style>
    
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSubmit" defaultfocus="txtAUUserLogin" autocomplete="off">

    <div class="top_header_main"></div>
    <custom:menu ID="menu" runat="server" />
    <div class="top_header_shadow"></div>
    
    <div class="content">
        <custom:logout ID="logout" runat="server" />
        <div class="header">
            <a href="/security/modifyuser.aspx" title="Modify Users">Modify Users</a> &gt; Edit User
        </div>
        <div class="header_border"></div>
   
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"  EnablePartialRendering="true" />
        
        <!--StartContent-->
        <div class="main_content">
    	    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>             
                    <fieldset>  
                        <legend>Edit User Information</legend>
                            <div style="padding-left:10px;">
                                <asp:ValidationSummary 
                                    id="ValidationSummary1" 
                                    ValidationGroup="Info" 
                                    runat="server" 
                                    CssClass="ErrorSummary"  
                                    HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
			                        HyperLinkToFields="True" 
                                    ScrollIntoView="Top"  
                                    DisplayMode="bulletList"/>
                            </div>
	                        <br /> 
                            <asp:Label ID="lblAUStatus" runat="server" CssClass="padding"></asp:Label>
                            <asp:Panel ID="pnlResults" runat="server" Visible="false" CssClass="padding">
                                <br />
                                <asp:LinkButton ID="lnkViewUsers" runat="server" Text="Return to user list"></asp:LinkButton>
                                <br /><br />
                            </asp:Panel>
        
                            <asp:panel ID="pnlModifyUser" runat="server">    
                                <table border="0" style="margin-left:20px;" cellspacing="4" width="75%" >
                                    <tr>
                                        <td style="width:15%;">User Login:</td>
                                        <td>
                                            <asp:Literal ID="litUserLogin" runat="server"></asp:Literal>
                                        </td>
                                        <td valign="top">Email Address:</td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtAUEMailAddress" MaxLength="50" runat="server" TabIndex="7" ValidationGroup="Info"  />
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator3" 
                                                runat="server"  
                                                ValidationGroup="Info"
                                                ControlToValidate="txtAUEMailAddress" 
                                                ErrorMessage="Requierd - Email address" 
                                                Text="*" />
                                            <asp:RegularExpressionValidator 
                                                ID="revAUEmail"  
                                                ValidationGroup="Info" 
                                                ControlToValidate="txtAUEMailAddress" 
                                                ValidationExpression="^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$"
                                                ErrorMessage="Not a valid email address" 
                                                Text="*" 
                                                runat="server" />                        
                                        </td>
                                    </tr>                
               
                                    <tr>
                                        <td>First Name:</td>
                                        <td>
                                            <asp:TextBox ID="txtAUFirstName" MaxLength="20" runat="server" TabIndex="5" ValidationGroup="Info"  />
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator1" 
                                                runat="server"  
                                                ValidationGroup="Info"
                                                ControlToValidate="txtAUFirstName" 
                                                ErrorMessage="Required - First name" 
                                                Text="*" />
                                            <asp:FilteredTextBoxExtender 
                                                ID="FilteredTextBoxExtender2" 
                                                runat="server" 
                                                TargetControlID="txtAUFirstName" 
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                ValidChars="' " />
                                        </td>
                                        
                                        <td valign="top">Role: </td>
                                        <td valign="top">
                                            <telerik:RadComboBox 
                                                ID="ddlAURoles" 
                                                AppendDataBoundItems="true" 
                                                ValidationGroup="Info" 
                                                Runat="server" 
                                                Skin="Windows7" 
                                                EmptyMessage="Type or select role"  
                                                EnableVirtualScrolling="true" 
                                                tabindex="8" />
                                            &nbsp; 
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator5" 
                                                runat="server"  
                                                ValidationGroup="Info"
                                                ControlToValidate="ddlAURoles" 
                                                ErrorMessage="Required - Security role" 
                                                InitialValue="Select One" 
                                                Text="*" />                     
                                        </td>                    
                                    </tr>
                
                                    <tr>
                                        <td>Last Name:</td>
                                        <td>
                                            <asp:TextBox ID="txtAULastName" MaxLength="25" runat="server" TabIndex="6" ValidationGroup="Info"  />
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator2" 
                                                runat="server"  
                                                ValidationGroup="Info"
                                                ControlToValidate="txtAULastName" 
                                                ErrorMessage="Required - Last name" 
                                                Text="*" />
                                            <asp:FilteredTextBoxExtender 
                                                ID="FilteredTextBoxExtender3" 
                                                runat="server" 
                                                TargetControlID="txtAULastName" 
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                ValidChars="' -," />
                                        </td>
                    
                                        <asp:Panel ID="pnlOffice" runat="server" Visible="false">
                                            <td valign="top" rowspan="10">Office:</td>
                                            <td rowspan="10">
                                                <telerik:RadListBox 
                                                    ID="RadListBoxSource" 
                                                    runat="server" 
                                                    Width="200px" 
                                                    Height="150px" 
                                                    DataTextField="Name" 
                                                    DataValueField="Office_Id"
                                                    SelectionMode="Multiple" 
                                                    AllowTransfer="true" 
                                                    TransferToID="RadListBoxDestination" 
                                                    AutoPostBackOnTransfer="true"                             
                                                    AutoPostBackOnReorder="true" 
                                                    EnableDragAndDrop="true" 
                                                    ValidationGroup="Info" />                               
                                                <telerik:RadListBox 
                                                    ID="RadListBoxDestination" 
                                                    runat="server" 
                                                    Width="200px" 
                                                    Height="150px"
                                                    SelectionMode="Multiple"  
                                                    AutoPostBackOnReorder="true" 
                                                    EnableDragAndDrop="true" 
                                                    DataValueField="Office_ID" 
                                                    DataTextField="Name" />                          
                                            </td>
                                        </asp:Panel>   
                                    </tr>
            
                                    <tr valign="top">
                                        <td valign="top">Status:</td>
                                        <td valign="top">
                                            <telerik:RadComboBox 
                                                ID="ddlStatus" 
                                                AppendDataBoundItems="true" 
                                                ValidationGroup="Info" 
                                                Runat="server" 
                                                Skin="Windows7"  
                                                EnableVirtualScrolling="true" 
                                                tabindex="8" 
                                                DataTextField="name" 
                                                DataValueField="value" />
                                            <asp:XmlDataSource runat="server" ID="XmlDataSource1" DataFile="~/resource/status.xml" />
                                            &nbsp; 
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator4" 
                                                runat="server"  
                                                ValidationGroup="Info"
                                                ControlToValidate="ddlStatus" 
                                                ErrorMessage="Required - Security status" 
                                                InitialValue="Select One" Text="*" /> 
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    
                                    <tr><td colspan="4">&nbsp;</td></tr><tr><td colspan="4">&nbsp;</td></tr>
                                    <tr><td colspan="4">&nbsp;</td></tr><tr><td colspan="4">&nbsp;</td></tr>
                                </table>
                              
                                <div style="margin-left:20px;">
                                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="/images/btnSaveUser.jpg" Text="Submit Changes" ToolTip="Submit Changes" ValidationGroup="Info" />
                                    <br /><br />
                                </div>
                            </asp:panel>
 			            </fieldset>
                    </ContentTemplate>
                    
                    <Triggers></Triggers>
                </asp:UpdatePanel>  
                    
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>    
                        <fieldset>
                            <legend>Account Security</legend>
                            <div style="padding-left:10px;">
                                <des:ValidationSummary 
                                    ID="PWSummary" 
                                    Group="password" 
                                    runat="server" 
                                    CssClass="ErrorSummary"  
                                    HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
			                        DisplayMode="bulletList" />
                            </div>
                    
                            <asp:Panel ID="pnlPasswordResults" runat="server" Visible="false" CssClass="padding">
                                <asp:Label ID="lblPWStatus" runat="server"></asp:Label>
                                <br /><br />
                            </asp:Panel>
                                    
                            <asp:panel ID="pnlChgPassword" runat="server">            
                                <table border="0" style="margin-left:20px;" cellspacing="4" width="75%" >
                                    <tr>
                                        <td>Login Attempts:</td>
                                        <td><asp:Literal ID="litLoginAttempts" runat="server"></asp:Literal></td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width:15%;">
                                            <asp:LinkButton ID="lnkBtnColHelp" runat="server" Text="Password:" OnClientClick="return false;" />
                                            <div id="moveMe" class="flyOutDiv">
                                                <div style="float:right;">
                                                    <asp:LinkButton ID="lnkBtnCloseColHelp" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX" />
                                                </div>
                                
                                                Passwords must be at least 8 characters and contain 3 of the 4 conditions:
			                                    <ul>
			                                      <li style="margin-left:35px;">Uppercase letter</li>
			                                      <li style="margin-left:35px;">Lowercase letter</li>
			                                      <li style="margin-left:35px;">Number</li>
			                                      <li style="margin-left:35px;">Special character (excludes: " & ; < >)</li>
			                                    </ul>                                                       
                                            </div>
                                        </td>
                                        
                                        <td>
                                            <asp:TextBox ID="txtAUPassword" ValidationGroup="password" MaxLength="20" TextMode="Password" runat="server" TabIndex="3" autocomplete="off"></asp:TextBox>
                                            <des:MultiConditionValidator 
                                                ID="MultiConditionValidator1" 
                                                runat="server" 
                                                ErrorFormatter="Text (class: TextErrorFormatter)" 
                                                ErrorMessage="*" 
                                                Operator="AND"  
                                                Group="password"
                                                SummaryErrorMessage="Password does not meet requirements (click on password link for help)">
                                                <ErrorFormatterContainer>
                                                    <des:TextErrorFormatter />
                                                </ErrorFormatterContainer>
                                                <Conditions>
                                                    <des:RequiredTextCondition ControlIDToEvaluate="txtAUPassword" Name="RequiredTextCondition" />
                                                    <des:RegexCondition ControlIDToEvaluate="txtAUPassword" Expression="(?=^.{8,20}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*" Name="RegexCondition" CaseInsensitive="False"/>
                                                    <des:CharacterCondition ControlIDToEvaluate="txtAUPassword" Exclude="True" Name="CharacterCondition" OtherCharacters="&lt;&gt;;&quot;&amp;" />
                                                </Conditions>
                                            </des:MultiConditionValidator>
                                        </td>  
                                        <td>&nbsp;</td>                     
                                        <td>&nbsp;</td>                        
                                    </tr>
                                    <tr>
                                        <td>Re-type Password:</td>
                                        <td>
                                            <asp:TextBox ID="txtAUPassword2" MaxLength="20" TextMode="Password" ValidationGroup="password" runat="server" TabIndex="4" autocomplete="off"></asp:TextBox>
                                            <des:MultiConditionValidator 
                                                ID="MultiConditionValidator2" 
                                                runat="server" 
                                                ErrorFormatter="Text (class: TextErrorFormatter)" 
                                                ErrorMessage="*" 
                                                Operator="AND" 
                                                Group="password"
                                                SummaryErrorMessage="Password does not meet requirements or passwords do not match (click on password link for help)">
                                                <ErrorFormatterContainer>
                                                    <des:TextErrorFormatter />
                                                </ErrorFormatterContainer>
                                                <Conditions>
                                                    <des:CompareTwoFieldsCondition ControlIDToEvaluate="txtAUPassword" SecondControlIDToEvaluate="txtAUPassword2" />
                                                    <des:RequiredTextCondition ControlIDToEvaluate="txtAUPassword2" Name="RequiredTextCondition" />
                                                    <des:RegexCondition ControlIDToEvaluate="txtAUPassword2" Expression="(?=^.{8,20}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*" Name="RegexCondition" CaseInsensitive="False"/>
                                                    <des:CharacterCondition ControlIDToEvaluate="txtAUPassword2" Exclude="True" Name="CharacterCondition" OtherCharacters="&lt;&gt;;&quot;&amp;" />
                                                </Conditions>
                                            </des:MultiConditionValidator>  
                                        </td> 
                                    </tr>
                                </table>
                                
                                <br />
                                <div style="margin-left:20px;">
                                    <des:ImageButton ID="btnChgPassword" ImageUrl="/images/btnChangePW.jpg" runat="server" Text="Reset Password" ToolTip="Submit Password" ValidationGroup="password"  /> 
                                    &nbsp;
                                    <des:ImageButton ID="btnUnlock" ImageUrl="/images/btnResetLogin.jpg" runat="server" Text="Reset Login Attempts" ToolTip="Reset Login Attempts" CausesValidation="false" />
                                    <br /><br />
                                </div>
                            </asp:panel>
                        </fieldset>
                        
                        <asp:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkBtnColHelp">
                            <Animations>
                                <OnClick>
                                    <Sequence>
                                        <EnableAction Enabled="false"></EnableAction>
                                        <StyleAction AnimationTarget="moveMe" Attribute="display" Value="block"/>
                                        <Parallel AnimationTarget="moveMe" Duration=".3" Fps="30">
                                            <Move Horizontal="-10" Vertical="50"></Move>
                                            <FadeIn Duration=".5"/>
                                        </Parallel>
                                        <Parallel AnimationTarget="moveMe" Duration=".5">
                                            <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                            <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#999999" />
                                        </Parallel>
                                    </Sequence>
                                </OnClick>
                            </Animations>
                        </asp:AnimationExtender>
                   
                        <asp:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="lnkBtnCloseColHelp">                   
                            <Animations>
                                <OnClick>
                                    <Sequence AnimationTarget="moveMe">
                                        <Parallel AnimationTarget="moveMe" Duration=".3" Fps="20">
                                            <Move Horizontal="10" Vertical="-50"></Move>
                                            <Scale ScaleFactor="0.05" FontUnit="px" />
                                            <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                            <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                            <FadeOut />
                                        </Parallel>
                                        <StyleAction Attribute="display" Value="none"/>
                                        <StyleAction Attribute="height" Value=""/>
                                        <StyleAction Attribute="width" Value="400px"/>
                                        <StyleAction Attribute="fontSize" Value="13px"/>
                                        <EnableAction AnimationTarget="lnkBtnColHelp" Enabled="true" />
                                    </Sequence>
                                </OnClick>
                            </Animations>                   
                        </asp:AnimationExtender>

                    </ContentTemplate>
                    <Triggers></Triggers>
                </asp:UpdatePanel>  
  
                <br /><br />          
            </div>
            <!--EndContent-->
        </div>
        
        <custom:timeout ID="timeoutControl" runat="server" /> 
    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
    </div>
</body> 
</html> 