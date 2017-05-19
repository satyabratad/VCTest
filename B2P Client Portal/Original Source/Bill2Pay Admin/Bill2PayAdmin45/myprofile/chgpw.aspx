<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="chgpw.aspx.vb" Inherits="Bill2PayAdmin45.chgpw" ValidateRequest="true" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Profile :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
    
</head>
<body>
 <form id="form1" runat="server" defaultbutton="btnSubmit" defaultfocus="txtAUPassword" autocomplete="off">

  <div class="top_header_main"></div>
  <custom:menu ID="menu" runat="server" />

  <div class="top_header_shadow"></div>
  
  <div class="content">
   <custom:logout ID="logout" runat="server" />
    <div class="header">Profile - Change Password</div>
    <div class="header_border"></div>
   
    
    
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
<des:PageManager id="PageManager1" runat="server" AJAXFramework="MicrosoftAJAX" ></des:PageManager>
  
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
      
    
    
    <!--StartContent-->
    <div class="main_content">
    
    
    <asp:Panel id="pnlResults" runat="server">
        <asp:Label ID="lblAUStatus" runat="server" CssClass="padding"></asp:Label>
    </asp:Panel>
    <des:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="ErrorSummary" HeaderText="There's an error in the information you entered. Please correct the following errors and continue." DisplayMode="BulletList" />
    
    
              <br />
              <asp:Panel ID="pnlChangePW" runat="server">
              <table border="0" style="margin-left:20px;" cellspacing="4" width="35%" >
                <tr>
                  
                  <td>
                    <asp:LinkButton ID="lnkBtnColHelp" runat="server" Text="Password" OnClientClick="return false;" />
                     
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
                      <asp:TextBox ID="txtAUPassword" MaxLength="20" TextMode="Password" runat="server" Width="150px" CssClass="inputText" autocomplete="off"></asp:TextBox>
                      <des:MultiConditionValidator ID="MultiConditionValidator1" runat="server" 
                          ErrorFormatter="Text (class: TextErrorFormatter)" ErrorMessage="*" 
                          Operator="AND"                           
                          SummaryErrorMessage="Password does not meet requirements (click on password link for help)">
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
                </tr>
                <tr>   
                  <td>Re-type Password</td>
                  <td>
                      <asp:TextBox ID="txtAUPassword2" MaxLength="20" TextMode="Password" runat="server" Width="150px" CssClass="inputText" autocomplete="off"></asp:TextBox>
                      <des:MultiConditionValidator ID="MultiConditionValidator2" runat="server" 
                          ErrorFormatter="Text (class: TextErrorFormatter)" ErrorMessage="*" 
                          Operator="AND" 
                          SummaryErrorMessage="Password does not meet requirements or passwords do not match (click on password link for help)">
                          <ErrorFormatterContainer>
                              <des:TextErrorFormatter />
                          </ErrorFormatterContainer>
                          <Conditions>
                              <des:CompareTwoFieldsCondition ControlIDToEvaluate="txtAUPassword" SecondControlIDToEvaluate="txtAUPassword2" />
                              <des:RequiredTextCondition ControlIDToEvaluate="txtAUPassword2" 
                                  Name="RequiredTextCondition" />
                              <des:RegexCondition ControlIDToEvaluate="txtAUPassword2" Expression="(?=^.{8,20}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*" Name="RegexCondition" CaseInsensitive="False" />
                               <des:CharacterCondition ControlIDToEvaluate="txtAUPassword2" Exclude="True" Name="CharacterCondition" OtherCharacters="&lt;&gt;;&quot;&amp;" />
                          </Conditions>
                      </des:MultiConditionValidator>
                  </td>
                </tr>
                
                </table>
 			      
 			        
 			  <br />    
                
              <div style="margin-left:20px;">
              <des:ImageButton ID="btnSubmit" runat="server" Text="Change Password" ImageUrl="/images/btnResetPW.jpg" ToolTip="Change Password" CssClass="submit" />
                
              </div>
                
              <br /><br />
            </asp:panel>
            
            
            <asp:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkBtnColHelp">
                        <Animations>
                            <OnClick>
                                <Sequence>
                                    <EnableAction Enabled="false"></EnableAction>
             
                                    <StyleAction AnimationTarget="moveMe" Attribute="display" Value="block"/>
                                    <Parallel AnimationTarget="moveMe" Duration=".3" Fps="30">
                                        <Move Horizontal="-50" Vertical="50"></Move>
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
                                        <Move Horizontal="50" Vertical="-50"></Move>
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