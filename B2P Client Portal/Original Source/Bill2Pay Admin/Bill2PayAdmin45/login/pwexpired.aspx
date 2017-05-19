<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pwexpired.aspx.vb" Inherits="Bill2PayAdmin45.pwexpired" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Login :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
</head>

<body>

    <div class="top_header">
        <div class="logo"></div>
        <div class="title">
            Client Administration Tool
            <div class="sub">Your payments are your business. Processing them right is ours.</div>
        </div>
        <div class="right"></div>
    </div>
    <div class="top_header_shadow"></div>
    <div class="content"><br />
        <span style="font-size:14pt; padding-left:10px;">Password Expired</span>
        <p style="padding-left:20px;">Please enter the new password and confirmation password. Once complete, click on "Change Password".</p>
        <div style="padding-left:300px; margin-top:50px; margin-bottom:200px;"> 
            <div id="login"> 
    	        <p class="ErrorSummary"><asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label></p>
                
                <form id="frmLogin" name="frmLogin" method="post" runat="server" autocomplete="off"> 
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                    <des:PageManager id="PageManager1" runat="server" AJAXFramework="MicrosoftAJAX" ></des:PageManager>
                    <des:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="ErrorSummary" HeaderText="There's an error in the information you entered. Please correct the following errors and continue." DisplayMode="BulletList" />

                    <table border="0" style="margin-left:20px;" cellspacing="4" width="45%" >
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
			                              <li style="margin-left:35px;">Special character (excludes: " & ; < >)</li>
			                        </ul>
                                               
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAUPassword" CssClass="inputText" MaxLength="20" TextMode="Password" runat="server" TabIndex="3" autocomplete="off"></asp:TextBox>                      
                                <des:MultiConditionValidator 
                                    ID="MultiConditionValidator1" 
                                    runat="server" 
                                    ErrorFormatter="Text (class: TextErrorFormatter)" 
                                    ErrorMessage="*" 
                                    Operator="AND" 
                                    SummaryErrorMessage="Password does not meet requirements (click on password link for help)">
                                    <ErrorFormatterContainer>
                                        <des:TextErrorFormatter />
                                    </ErrorFormatterContainer>
                                    <Conditions>
                                        <des:RequiredTextCondition ControlIDToEvaluate="txtAUPassword" Name="RequiredTextCondition" />
                                        <des:RegexCondition ControlIDToEvaluate="txtAUPassword" Expression="(?=^.{8,20}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*" 
                                            Name="RegexCondition" CaseInsensitive="false" />
                                        <des:CharacterCondition ControlIDToEvaluate="txtAUPassword" Exclude="True" Name="CharacterCondition" OtherCharacters="&lt;&gt;;&quot;&amp;" />
                                    </Conditions>
                                </des:MultiConditionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Re-type Password:</td>
                            <td>
                                <asp:TextBox ID="txtAUPassword2" CssClass="inputText" MaxLength="20" TextMode="Password" runat="server" TabIndex="4" autocomplete="off"></asp:TextBox>
                                <des:MultiConditionValidator ID="MultiConditionValidator2" runat="server" 
                                          ErrorFormatter="Text (class: TextErrorFormatter)" ErrorMessage="*" 
                                          Operator="AND" 
                                          SummaryErrorMessage="Password does not meet requirements or the new passwords do not match">
                                    <ErrorFormatterContainer>
                                        <des:TextErrorFormatter />
                                    </ErrorFormatterContainer>
                                    <Conditions>
                                        <des:CompareTwoFieldsCondition ControlIDToEvaluate="txtAUPassword" SecondControlIDToEvaluate="txtAUPassword2" />
                                        <des:RequiredTextCondition ControlIDToEvaluate="txtAUPassword2" 
                                            Name="RequiredTextCondition" />
                                        <des:RegexCondition ControlIDToEvaluate="txtAUPassword2" Expression="(?=^.{8,20}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*" 
                                            Name="RegexCondition" />
                                        <des:CharacterCondition ControlIDToEvaluate="txtAUPassword2" Exclude="True" 
                                            Name="CharacterCondition" OtherCharacters="&lt;&gt;;&quot;&amp;" />
                                    </Conditions>
                                </des:MultiConditionValidator>
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp;</td></tr>
                        <tr><td style="text-align:center" colspan="2"><des:ImageButton ID="btnSubmit"  ImageUrl="/images/btnChangePW.jpg" runat="server" Text="Change Password" ToolTip="Change Password" /></td></tr>
                    </table>
                    
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
    	        </form> 
		    </div> 
        </div> 
    </div>
    <div class="clear"></div> 
    <div class="footer_shadow">
          <p class="text">© <asp:Literal ID="litYear" runat="server"></asp:Literal> Intuition Systems, Inc. All Rights Reserved.   Powered by <a href="http://www.bill2pay.com" target="_blank" title="Bill2Pay Home">Bill2Pay</a> &reg; </p> 
        <%--<p class="text">© 2010 Intuition Systems, Inc. All Rights Reserved. <a href="http://www.bill2pay.com" target="_blank" title="Bill2Pay Home">Bill2Pay</a> &reg; is a registered trademark of Intuition Systems, Inc.</p> --%>
      </div>
</body>
</html>
