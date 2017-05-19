<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adduser.aspx.vb" Inherits="Bill2PayAdmin45.adduser" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Add User :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
    
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSubmit" defaultfocus="txtAUUserLogin">

    <div class="top_header_main"></div>
    <custom:menu ID="menu" runat="server" />

    <div class="top_header_shadow"></div>
  
    <div class="content">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
        <custom:logout ID="logout" runat="server" />
        <div class="header">Add User</div>
        <div class="header_border"></div>
        <des:PageManager id="PageManager1" runat="server" AJAXFramework="MicrosoftAJAX" ></des:PageManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!--StartContent-->
                <div class="main_content">
                    <fieldset>  
                        <legend>Add New User Information</legend>
                        <asp:Label ID="lblAUStatus" runat="server" CssClass="padding"></asp:Label>
                        
                        <asp:Panel ID="pnlResults" runat="server" Visible="false" CssClass="padding">
                            <asp:LinkButton ID="lnkAddUser" runat="server" Text="Add another user" OnClick="lnkAddUser_Click" OnClientClick="document.location.href=document.location.href;"></asp:LinkButton> 
        |                   <asp:LinkButton ID="lnkViewUsers" runat="server" Text="Modify users"></asp:LinkButton>
                            <br /><br />
                        </asp:Panel>

                        <asp:panel ID="pnlAddUser" runat="server">
                            <des:ValidationSummary 
                                ID="ValidationSummary2" 
                                runat="server" 
                                CssClass="ErrorSummary" 
                                HeaderText="There's an error in the information you entered. Please correct the following errors and continue." 
                                DisplayMode="BulletList" />
                            <br />        
                            
                            <table border="0" style="margin-left:20px;" cellspacing="4" width="75%" >
                                <tr>
                                    <td>Client Code:</td>
                                    <td>
                                        <asp:Label ID="lblAUClient" Visible="false" runat="server"></asp:Label> 
                                        <telerik:RadComboBox ID="ddlAUClient" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="1" OnSelectedIndexChanged="ddlAUClient_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Select One" Value="" Font-Italic="true" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        &nbsp;
                                        <des:CompareToValueValidator 
                                            ID="desValidator1" 
                                            runat="server" 
                                            SummaryErrorMessage="Required - Client code" 
                                            ControlIDToEvaluate="ddlAUClient" 
                                            InAJAXUpdate="true"     
                                            ErrorMessage="*" 
                                            ValueToCompare="Select One" 
                                            NotCondition="true"/>
                                        <asp:XmlDataSource runat="server" ID="XmlDataSource3" DataFile="~/resource/testclient.xml" />
                                    </td>
                    
                                    <td>Email Address:</td>
                                    <td>
                                        <asp:TextBox ID="txtAUEMailAddress" MaxLength="50" runat="server" TabIndex="7" CssClass="inputText"  />
                                        <des:MultiConditionValidator 
                                            ID="MultiConditionValidator3" 
                                            runat="server" 
                                            ErrorFormatter="Text (class: TextErrorFormatter)" 
                                            ErrorMessage="*" 
                                            InAJAXUpdate="True" 
                                            Operator="AND" 
                                            SummaryErrorMessage="Email address is missing or is invalid">
                                            <ErrorFormatterContainer>
                                                <des:TextErrorFormatter />
                                            </ErrorFormatterContainer>
                                            <Conditions>
                                                <des:RequiredTextCondition ControlIDToEvaluate="txtAUEMailAddress" Name="RequiredTextCondition" />
                                                <des:EmailAddressCondition ControlIDToEvaluate="txtAUEMailAddress" Name="EmailAddressCondition" />
                                            </Conditions>
                                        </des:MultiConditionValidator>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>User Login:</td>
                                    <td>
                                        <asp:TextBox ID="txtAUUserLogin" MaxLength="15" runat="server" TabIndex="2" CssClass="inputText" AutoComplete="off" />
                                        <des:RequiredTextValidator 
                                            ID="desValidatorLogin" 
                                            runat="server" 
                                            SummaryErrorMessage="Required - User login"
                                            ControlToValidate="txtAUUserLogin" 
                                            ErrorMessage="*" 
                                            ControlIDToEvaluate="txtAUUserLogin" 
                                            InAJAXUpdate="True" >
                                            <ErrorFormatterContainer>
                                                <des:TextErrorFormatter />
                                            </ErrorFormatterContainer>
                                        </des:RequiredTextValidator>
                                        <asp:FilteredTextBoxExtender 
                                            ID="FilteredTextBoxExtender1" 
                                            runat="server" 
                                            TargetControlID="txtAUUserLogin" 
                                            FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                            ValidChars="_" />
                                    </td>
                  
                                    <td>Role:</td>
                                    <td>
                                        <telerik:RadComboBox ID="ddlAURoles" AppendDataBoundItems="true" Runat="server" Skin="Default" tabindex="8" >
                                             <Items>
                                                    <telerik:RadComboBoxItem Text="Select One" Value="Select One" Font-Italic="true" />
                                             </Items>  
                                        </telerik:RadComboBox>
                                        &nbsp; 
                                    </td>
                                </tr>
                            
                                <tr>
                                    <td>
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
			                                  <li style="margin-left:35px;">Special character (excludes: &quot; &amp; ; &lt; &gt;)</li>
			                                </ul>                                               
                                        </div>
                                    </td>
                                    
                                    <td>
                                        <asp:TextBox ID="txtAUPassword" MaxLength="20" TextMode="Password" runat="server" TabIndex="3" CssClass="inputText" AutoComplete="off"></asp:TextBox>
                                        <des:MultiConditionValidator 
                                            ID="MultiConditionValidator1" 
                                            runat="server" 
                                            ErrorFormatter="Text (class: TextErrorFormatter)" 
                                            ErrorMessage="*" 
                                            Operator="AND" 
                                            SummaryErrorMessage="Password does not meet requirements (click on password link for help)" 
                                            InAJAXUpdate="True">
                                            <ErrorFormatterContainer>
                                                <des:TextErrorFormatter />
                                            </ErrorFormatterContainer>
                                            <Conditions>
                                                <des:RequiredTextCondition ControlIDToEvaluate="txtAUPassword" Name="RequiredTextCondition" />
                                                <des:RegexCondition ControlIDToEvaluate="txtAUPassword" Expression="(?=^.{8,20}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*" Name="RegexCondition" CaseInsensitive="False" />
                                                <des:CharacterCondition ControlIDToEvaluate="txtAUPassword" Exclude="True" Name="CharacterCondition" OtherCharacters="&lt;&gt;;&quot;&amp;" />
                                            </Conditions>
                                        </des:MultiConditionValidator>
                                    </td>
                        
                                    <asp:Panel ID="pnlOffice" runat="server" Visible="false">
                                        <td valign="top" rowspan="4">Office:</td>
                                        <td rowspan="4">
                                            <telerik:RadListBox 
                                                tabindex="8" 
                                                runat="server" 
                                                ID="RadListBoxSource" 
                                                SelectionMode="Multiple" 
                                                Height="120px" 
                                                Width="160px" 
                                                DataTextField="Name" 
                                                DataValueField="Office_Id" 
                                                AllowTransfer="true" 
                                                TransferToID="RadListBoxDestination" 
                                                EnableDragAndDrop="true"/>
                                            <telerik:RadListBox runat="server" ID="RadListBoxDestination" Height="120px" Width="140px" />
                                        </td>
                                    </asp:Panel>
                                </tr>
                                
                                <tr>
                                    <td>Re-type Password:</td>
                                    <td>
                                        <asp:TextBox ID="txtAUPassword2" MaxLength="20" TextMode="Password" CssClass="inputText" runat="server" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                        <des:MultiConditionValidator 
                                            ID="MultiConditionValidator2" 
                                            runat="server" 
                                            ErrorFormatter="Text (class: TextErrorFormatter)" 
                                            ErrorMessage="*" 
                                            Operator="AND"                           
                                            SummaryErrorMessage="Password does not meet requirements or passwords do not match (click on password link for help)" 
                                            InAJAXUpdate="True">
                                            <ErrorFormatterContainer>
                                                <des:TextErrorFormatter />
                                            </ErrorFormatterContainer>
                                            <Conditions>
                                                <des:CompareTwoFieldsCondition ControlIDToEvaluate="txtAUPassword" SecondControlIDToEvaluate="txtAUPassword2" />
                                                <des:RequiredTextCondition ControlIDToEvaluate="txtAUPassword2" Name="RequiredTextCondition" />
                                                <des:RegexCondition ControlIDToEvaluate="txtAUPassword2" Expression="(?=^.{8,20}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*" Name="RegexCondition" CaseInsensitive="False" />
                                                <des:CharacterCondition ControlIDToEvaluate="txtAUPassword2" Exclude="True" Name="CharacterCondition" OtherCharacters="&lt;&gt;;&quot;&amp;" />
                                            </Conditions>
                                        </des:MultiConditionValidator>
                                    </td>                   
                                </tr>
                
                                <tr>
                                    <td>First Name:</td>
                                    <td>
                                        <asp:TextBox ID="txtAUFirstName" MaxLength="20" runat="server" TabIndex="5" CssClass="inputText"  />
                                        <des:RequiredTextValidator 
                                            ID="desValidatorFName" 
                                            runat="server" 
                                            ErrorMessage="*"
                                            ControlToValidate="txtAUFirstName" 
                                            summaryErrorMessage="Required - First name" 
                                            ControlIDToEvaluate="txtAUFirstName" 
                                            InAJAXUpdate="True">
                                            <ErrorFormatterContainer>
                                                <des:TextErrorFormatter />
                                            </ErrorFormatterContainer>
                                        </des:RequiredTextValidator>
                                        <asp:FilteredTextBoxExtender 
                                            ID="FilteredTextBoxExtender2" 
                                            runat="server" 
                                            TargetControlID="txtAUFirstName" 
                                            FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                            ValidChars="' " />                 
                                    </td>
                                </tr>
                                <tr>
                                    <td>Last Name:</td>
                                    <td>
                                        <asp:TextBox ID="txtAULastName" MaxLength="25" runat="server" TabIndex="6" CssClass="inputText"  />
                                        <des:RequiredTextValidator ID="desValidatorLName" runat="server" 
                                            ControlToValidate="txtAULastName" 
                                            SummaryErrorMessage="Required - Last name" ErrorMessage="*" 
                                            ControlIDToEvaluate="txtAULastName" InAJAXUpdate="True">
                                            <ErrorFormatterContainer>
                                                <des:TextErrorFormatter />
                                            </ErrorFormatterContainer>
                                        </des:RequiredTextValidator>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtAULastName" FilterType="Custom, LowercaseLetters, UppercaseLetters" ValidChars="' -," />
                                    </td>                    
                                </tr>               
                            </table>
 			       			        
 			                <br />    
                
                            <div style="margin-left:20px;">
                                <des:ImageButton ID="btnSubmit" ImageUrl="/images/btnAddUser.jpg" runat="server" Text="Add User" ToolTip="Add User" CssClass="submit" />
                
              </div>
                
              <br /><br />
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