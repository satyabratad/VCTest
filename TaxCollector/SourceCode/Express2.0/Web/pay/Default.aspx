<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web._paydefault" %>
<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentMenuFooter" Src="~/UserControls/MenuFooter.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>

<!DOCTYPE html>

<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html lang="en"> <!--<![endif]-->



<head id="docHead" runat="server">
	<meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<title>Express Payment - Account Details</title>

    <!-- CSS -->
    <link rel="stylesheet" href="/Css/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="/Css/app.css" type="text/css" id="lnkCSS" runat="server" />
    <link rel="stylesheet" href="/Css/progress.css" type="text/css" runat="server" />
   

    <!-- JavaScript -->
    <!--[if lt IE 9]>
        <script src="/Js/html5shiv.min.js"></script>
        <script src="/Js/respond.min.js"></script>
    <![endif]-->
  </head>
<body>

    <form id="frmDefault" autocomplete="off" runat="server">
    <div class="container" style="max-width:970px;">
       
        <!--// START LOGO, HEADER AND NAV //-->      
             
        <b2p:PaymentHeader id="phDefault" runat="server" />
             
        <!--// END LOGO, HEADER AND NAV //-->
       
      
        <div class="container-maincontent" style="padding:0px;">
        <div class="row" style="padding-top:10px; padding-bottom:20px;">
             <h1 class="text-hide">Account Details</h1> 
                    <div class="col-sm-1"></div>    
                    <div class="col-sm-10">                       
                    <!--// START MIDDLE CONTENT //-->
                                    
                        <div class="row" style="background-color:white; padding:5px;">
                            <!--// START NO SCRIPT CHECK //-->
                             <noscript>
                                    <style type="text/css">
                                        .content {display:none;}
                                    </style>
                                    <div class="container">
                                          <div class="row" style="background-color:white; padding:5px;">
                                                <div class="row">
                                                  <div class="col-xs-12">
                                                   <h3 class="text-primary"><asp:Literal id="litJavascriptHeading" text="<%$ Resources:WebResources, JavascriptHeading %>" runat="server" /></h3>
                                                    <hr />
                                                    <br />
                                                  </div>
                                                </div>

                                                <div class="row">
                                                  <div class="col-xs-12 col-sm-6">
                                                      <asp:Panel ID="pnlJSMessage" runat="server" class="alert alert-danger alert-sm">
                                                          <div id="imgStatusMsgIcon" class="fa fa-exclamation-circle fa-2x status-msg-icon"></div>
                                                              <div id="txtStatusMsg" class="status-msg-text"><asp:Literal id="litJavascriptMessage" text="<%$ Resources:WebResources, JavascriptMessage %>" runat="server" /></div>
                                                      </asp:Panel>
                                            
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                  </div>
                                                </div>
                                              </div>
                                      </div>
                            </noscript>
                            <!--// END NO SCRIPT CHECK //-->
                        
                            <div class="content">

                                 <!--// START BREADCRUMBS //-->
                                 <div class="container">
	                                    <div class="row">
		                                    <ul class="breadcrumb">			                                    
			                                    <li class="active"><a href="/pay/"><span class="badge badge-inverse">1</span> Account <span class="hidden-xs hidden-sm">Details</span></a></li>
                                                <li><a href="#" class="inactiveLink"><span class="badge">2</span><span class="hidden-xs hidden-sm"> Payment Details</span></a></li>
			                                    <li><a href="#" class="inactiveLink"><span class="badge">3</span><span class="hidden-xs hidden-sm"> Confirm Payment</span></a></li>
			                                    <li><a href="#" class="inactiveLink"><span class="badge">4</span><span class="hidden-xs hidden-sm"> Payment Complete</span></a></li>
		                                    </ul>
	                                    </div>
                                    </div>
                                <!--// END BREADCRUMBS //-->

                            <div class="col-xs-12 col-sm-6">  
                                <br />
                                <asp:Panel ID="pnlProducts" runat="server">                                                
                                    <div class="row">
                                        <div class="col-xs-12"> 
                                        <div class="form-group form-group-sm">
                                                <label class="control-label" for="ddlBankAccountType"><asp:Literal ID="litSelectProdct" runat="server" Text="<%$ Resources:WebResources, lblSelectProductService %>" /></label>
                                                <asp:DropDownList id="ddlCategories"
                                                                cssclass="form-control input-sm"                                              
                                                                autopostback="True"
                                                                runat="server" />
                                        </div>  
                                        </div>
                                    </div>
                                </asp:Panel>                             
                                <div class="row">
                                    <div class="col-xs-12"> 
                                        <asp:HiddenField ID="hdAccount1" runat="server" Value="" />
                                        <asp:HiddenField ID="hdAccount2" runat="server" Value="" />
                                        <asp:HiddenField ID="hdAccount3" runat="server" Value="" />                                   
                                                            
                                                    <div class="form-group form-group-sm">                                                                                         
                                                            <asp:Panel ID="pnlAccount1" runat="server" Visible="false">                                                               
                                                                <label class="control-label" for="txtLookupAccount1" id="lblLookupAccount1">
                                                                    <asp:Label ID="lblAccountNumber1" runat="server" />
                                                                </label>
                                                                <asp:TextBox ID="txtLookupAccount1" runat="server" MaxLength="40" cssclass="form-control input-sm" /> 
                                                            </asp:Panel>
                                                    </div>
                                                    <div class="form-group form-group-sm">                                                            
                                                            <asp:Panel ID="pnlAccount2" runat="server" Visible="false">
                                                                                                                          
                                                                <label class="control-label" for="txtLookupAccount2" id="lblLookupAccount2">
                                                                    <asp:Label ID="lblAccountNumber2" runat="server" />
                                                                </label>
                                                                <asp:TextBox ID="txtLookupAccount2" runat="server" MaxLength="40" cssclass="form-control input-sm" /> 
                                                            </asp:Panel> 
                                                    </div> 
                                                    <div class="form-group form-group-sm">                                                            
                                                            <asp:Panel ID="pnlAccount3" runat="server" Visible="false">                                                               
                                                                <label class="control-label" for="txtLookupAccount3" id="lblLookupAccount3">
                                                                    <asp:Label ID="lblAccountNumber3" runat="server" />
                                                                </label>
                                                                <asp:TextBox ID="txtLookupAccount3" runat="server" MaxLength="40" cssclass="form-control input-sm" /> 
                                                            </asp:Panel> 
                                                    </div> 
                                                <br />
                                                    <div class="pull-right">                                                
                                                            <asp:Button ID="btnLookup" runat="server" Text="Lookup" cssclass="btn btn-primary btn-sm" Width="70px" />                                                                                  
                                                    </div>                                                           
                               </div>    
                              </div>
                                                                                
                                    <asp:Button id="btnSubmit" cssclass="btn btn-primary btn-sm pull-right" text="<%$ Resources:WebResources, ButtonContinue %>" tooltip="<%$ Resources:WebResources, ButtonContinue %>" runat="server" />                  
                                <br />
                                <br />

                                
                            </div> 
                             
                            <div class="col-xs-12 col-sm-6">
                                    <br />
                                    <p><asp:Label ID="lblClientMessage" runat="server" ToolTip="Client Message"></asp:Label></p>
                               
                                    
                            </div>
                    </div>  
                    </div>                        
                </div>
                    <!--// END MIDDLE CONTENT //-->               
               
                    <div class="col-sm-1"></div>
           
          </div>
           
     </div>
    
      <!--// START MENU FOOTER CONTENT //-->

      <b2p:PaymentMenuFooter id="pfMenu" runat="server" />

      <!--// END MENU FOOTER CONTENT //-->

      <!--// START FOOTER CONTENT //-->

      <b2p:PaymentFooter id="pfDefault" runat="server" />

      <!--// END FOOTER CONTENT //-->
        

         <!-- START LOOKUP MODAL DIALOG -->
      <asp:Panel ID="pnlLookupResults" cssclass="modal fade" tabindex="-1" role="dialog" aria-label="Lookup Results" aria-hidden="true" runat="server">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">Lookup Results</h4>
            </div>
            <div class="modal-body">
                                                                                                                                          
                <div role="alert" id="divLookupAlert" runat="server" style="margin-top:10px;">   
                    <div class="fa fa-check-circle-o fa-2x status-msg-icon"></div>                                                 
                    <div class="status-msg-text"><asp:label ID="lblLookupHeader" runat="server" Text="" class="control-label" /></div>
                </div>                   
                           
              <asp:GridView ID="grdLookup" runat="server" 
                                            AutoGenerateColumns="true" 
                                            GridLines="None" 
                                            BorderStyle="none"  
                                            ShowHeader="false" 
                                            cellspacing="5"
                                            CssClass="gvhspadding"
                                            RowStyle-CssClass="gvhspadding" >
                                        </asp:GridView>
              <br />
              <br />
            </div>
            <div class="modal-footer">
              <asp:Button ID="btnClear" 
                    runat="server" 
                    text="<%$ Resources:WebResources, ButtonCancel %>"
                    cssclass="btn btn-link btn-sm" 
                    tooltip="<%$ Resources:WebResources, ButtonCancel %>"
                    data-dismiss="modal"
                    onclick="btnClear_Click"
                    usesubmitbehavior="false" />
               

              <asp:Button id="btnLookupGo"
                          text="<%$ Resources:WebResources, ButtonLookupGo %>"
                          cssclass="btn btn-sm btn-primary btn-sm"
                          tooltip="<%$ Resources:WebResources, ButtonLookupGo %>"
                          data-dismiss="modal"
                          runat="server" 
                          onclick="btnLookupGo_Click"
                          usesubmitbehavior="false" />
                <asp:Button id="btnLookupOK"
                          text="OK"
                          cssclass="btn btn-sm btn-primary btn-sm"
                          tooltip="OK"
                          data-dismiss="modal"
                          runat="server" 
                          Visible="false"
                          usesubmitbehavior="false" />
            </div>
          </div>
        </div>
      </asp:Panel>
      <!-- END LOOKUP MODAL DIALOG -->

     <!-- START LOOKUP ERROR MODAL DIALOG -->
      <asp:Panel ID="pnlLookupError" cssclass="modal fade" tabindex="-1" role="dialog" aria-label="Lookup Results" aria-hidden="true" runat="server">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">Lookup Results</h4>
            </div>
            <div class="modal-body">
                 <div role="alert" id="divLookupAlertError" runat="server" style="margin-top:10px;">                                                    
                    <div class="fa fa-exclamation-circle fa-2x status-msg-icon"></div><div class="status-msg-text"><asp:label ID="lblLookupHeaderError" runat="server" Text="" class="control-label" /></div>
                </div>
                
             
              <br />
            </div>
            <div class="modal-footer">
              <asp:Button id="btnCloseLookupErrorDialog"
                          text="<%$ Resources:WebResources, ButtonClose %>"
                          cssclass="btn btn-sm btn-primary btn-sm"
                          tooltip="<%$ Resources:WebResources, ButtonClose %>"
                          data-dismiss="modal"
                          runat="server" />
            </div>
          </div>
        </div>
      </asp:Panel>
      <!-- END LOOKUP ERROR MODAL DIALOG -->




     <!-- JavaScript -->
      <script src="/Js/jquery-1.11.1.min.js"></script>
      <script src="/Js/bootstrap.min.js"></script>
      

      <!--[if lt IE 9]><script src="/Js/B2P.Enums.IE8.js"></script><![endif]-->
      <!--[if (gte IE 9)|!(IE)]><!--><script src="/Js/B2P.Enums.js"></script><!--<![endif]-->

      <script src="/Js/B2P.Utility.js"></script>
      <script src="/Js/B2P.FormValidator.js"></script>
      <script src="/Js/B2P.ValidationType.js"></script>


    <script type="text/javascript">
          
          function validateForm() {
              var doc = document;              
              var item = new ValidationItem();

              // Create instance of the form validator
              var validator = new FormValidator();
              validator.setErrorMessageHeader("<%=GetGlobalResourceObject("WebResources", "ErrMsgHeader").ToString()%>\n\n")
              validator.setInvalidCssClass("has-error");
              validator.setAlertBoxStatus(false);

              // Add validation items to validator

              // Check to see if account 1 is present
               acct1 = doc.getElementById('hdAccount1').value;
              if (acct1 !== '') {
                  // Set the validator
                    validator.addValidationItem(new ValidationItem("txtLookupAccount1", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
              }

              // Check to see if account 2 is required
              acct2 = doc.getElementById('hdAccount2').value;
              if (acct2 !== '') {
                  // Set the validator
                    validator.addValidationItem(new ValidationItem("txtLookupAccount2", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
              }
           
               // Check to see if account 3 is required
              acct3 = doc.getElementById('hdAccount3').value;
              if (acct3 !== '') {
                  // Set the validator
                    validator.addValidationItem(new ValidationItem("txtLookupAccount3", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
              }
              return validator.validate();
          }
       


       
         
      </script>
        
        </div>
    </form>

  </body>
</html>
