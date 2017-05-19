<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Bill2PayAdmin45.login" %>

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
        <div class="title">total payment solutions
            <%--Client Administration Tool--%>
            <%--<div class="sub">Your payments are your business. Processing them right is ours.</div>--%>
        </div>
        <div class="right"></div>
    </div>
    <div class="top_header_shadow"></div>

    <div class="content"><br />
        <span style="font-size:14pt; padding-left:10px;">Client Portal - Secure Login Page</span>
        <div style="padding-left:300px; margin-top:50px; margin-bottom:200px;"> 
    	    <div id="login"> 
    	        <p class="ErrorSummary"><asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label></p>
            
							        
							        
    	    <form id="frmLogin" name="frmLogin" method="post" runat="server" defaultbutton="btnLogin" autocomplete="off"> 
    	       <%-- <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />--%>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    	        <des:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="ErrorSummary" HeaderText="There's an error in the information you entered. Please correct the following errors and continue." DisplayMode="BulletList" />

    	    <table style="border-spacing: 8px">
    	        <tr>
    	            <td><strong>User ID:
                        </strong></td>
    	            <td><asp:TextBox ID="txtLoginID" CssClass="inputText" runat="server" MaxLength="15" Width="200" autocomplete="off" />
    	            <asp:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" 
                            runat="server" Enabled="True" TargetControlID="txtLoginID" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
    	                 <des:MultiConditionValidator ID="MultiConditionValidator2" runat="server" 
                          ErrorFormatter="Text (class: TextErrorFormatter)" ErrorMessage="*" 
                          Operator="AND" 
                          
                          SummaryErrorMessage="User ID is required">
                          <ErrorFormatterContainer>
                              <des:TextErrorFormatter />
                          </ErrorFormatterContainer>
                          <Conditions>
                              <des:RequiredTextCondition ControlIDToEvaluate="txtLoginID" 
                                  Name="RequiredTextCondition" />
                           
                          </Conditions>
                      </des:MultiConditionValidator>
    	            </td>
    	        </tr>
    	        <tr>
    	            <td><strong>Password:</strong></td>
    	            <td>
                        <asp:TextBox ID="txtPassword" CssClass="inputText" TextMode="Password" MaxLength="20" Width="200" runat="server" autocomplete="off" />    	            
    	                <des:MultiConditionValidator ID="MultiConditionValidator1" 
                                                    runat="server" 
                                                    ErrorFormatter="Text (class: TextErrorFormatter)"   
                                                    ErrorMessage="*" 
                                                    Operator="AND" 
                                                    SummaryErrorMessage="Password does not meet requirements">
                          <ErrorFormatterContainer>
                              <des:TextErrorFormatter />
                          </ErrorFormatterContainer>
                          <Conditions>
                              <des:RequiredTextCondition ControlIDToEvaluate="txtPassword" Name="RequiredTextCondition" />
                              <des:RegexCondition ControlIDToEvaluate="txtPassword" Expression="(?=^.{8,20}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*" Name="RegexCondition" />
                              <des:CharacterCondition ControlIDToEvaluate="txtPassword" Exclude="True" Name="CharacterCondition" OtherCharacters="&lt;&gt;;&quot;&amp;" />
                          </Conditions>
                      </des:MultiConditionValidator>
    	            </td>
    	        </tr>
    	       <tr>
    	            <td></td>
    	            <td><des:ImageButton  ID="btnLogin" ImageUrl="/images/btnLogin2.jpg" runat="server" ToolTip="Click here to login" /></td>
    	        </tr>    	        
    	    </table>    	
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
