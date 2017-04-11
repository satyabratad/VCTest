<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaymentComplete.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web.PaymentComplete" %>
<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="JavaScriptCheck" Src="~/UserControls/JavaScriptCheck.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="b2p" TagName="BreadCrumbMenu" Src="~/UserControls/BreadCrumbMenu.ascx" %>


<!DOCTYPE html>


<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html lang="en"> <!--<![endif]-->


<head id="docHead" runat="server">
	<meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<title>Express Payment - Payment Complete</title>

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
       
      
        <div class="row" style="background-color:white; padding-bottom:10px;">
            <br />
                   
                    <div class="col-sm-12">                       
                  
                    <!--// START MIDDLE CONTENT //-->
                                              
                        <div class="row" style="background-color:white;">

                              <!--// START NO SCRIPT CHECK //-->
                                    <b2p:JavaScriptCheck ID="pjsJavascript" runat="server" />
                                <!--// END NO SCRIPT CHECK //-->

                              <div class="content">  
                                  <div class="container" style="min-height:50%;">
                                  <!--// START BREADCRUMBS //-->
                                  <asp:Panel ID="pnlNonSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu" >
                                        <b2p:BreadCrumbMenu runat="server" PageTagName="PaymentSuccess" ID="BreadCrumbMenu" />

	                                    <%--<div class="row">
		                                    <ul class="breadcrumb">			                                    
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">1</span> <span class="hidden-xs hidden-sm">Account Details</span></a></li>
                                                <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">2</span><span class="hidden-xs hidden-sm"> Payment Details</span></a></li>
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">3</span> <span class="hidden-xs hidden-sm">Confirm Payment</span></a></li>
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">4</span> <span class="hidden-xs hidden-sm">Payment </span>Complete</a></li>
		                                    </ul>
	                                    </div>--%>
                                    
                                      
                                </asp:Panel>
                                <asp:Panel ID="pnlSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu" >
                                        <b2p:BreadCrumbMenu runat="server" PageTagName="PaymentSuccess" ID="BreadCrumbMenuSSO" />
	                                    <%--<div class="row">
		                                    <ul class="breadcrumb">
			                                    
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">1</span> <span class="hidden-xs hidden-sm">Payment Details</span></a></li>
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">2</span> <span class="hidden-xs hidden-sm">Confirm Payment</span></a></li>
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">3</span> <span class="hidden-xs hidden-sm">Payment</span> Complete</a></li>
		                                    </ul>
	                                    </div>--%>
                                  
                                </asp:Panel> 
                                <!--// END BREADCRUMBS //--> 

                                  <!--// START PRINT CONTENT //-->
                                  
                                    <div class="col-xs-12 col-sm-6">
                                        <br />
                                        <div id="print_content">
                                         <div class="row">
                                            <div class="col-xs-12"><h3><asp:Literal ID="litClientName" runat="server"  /></h3>

                                            </div>                                             
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12"><strong><asp:Literal ID="litThankYou" runat="server" Text="<%$ Resources:WebResources, ThankYouforYourPayment %>" /></strong><br />

                                            </div>                                             
                                        </div>
                                       <br />

                                             <b2p:PaymentStatusMessage id="PostBackStatusMessage" runat="server" />
                                          
                                        <asp:Panel ID="pnlAccountDetails" runat="server">
                                                                                                      
                                                      <div class="table-responsive">
                                                        <table id="tblAccountInfo" class="table table-condensed table-no-border">
                                                            
                                                             <tr> 
                                                                <td class="bold text-uppercase text-primarydark"><asp:Literal ID="litConfirmText" runat="server" Text="<%$ Resources:WebResources, LblConfirmationNumber %>" /></td>
                                                                <td class="text-right bold text-primarydark"><asp:Literal ID="litConfirmationNumber" runat="server" /> </td>
                                                             </tr> 
                                                            <tr><td colspan="2"><hr /></td></tr>
                                                            <asp:Panel ID="pnlAccount1" runat="server"> 
                                                              <tr>
                                                                <td class="bold text-uppercase"><asp:Literal ID="litAccountNumber1" runat="server" /></td>
                                                                <td class="text-right"><asp:Literal ID="litAccountNumber1Data" runat="server" /> </td>
                                                              </tr>                                                        
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlAccount2" runat="server">                                                          
                                                              <tr>
                                                                <td class="bold text-uppercase"><asp:Literal ID="litAccountNumber2" runat="server" /></td>
                                                                <td class="text-right"><asp:Literal ID="litAccountNumber2Data" runat="server" /> </td>
                                                              </tr>                                                   
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlAccount3" runat="server">                                                        
                                                              <tr>
                                                                <td class="bold text-uppercase"><asp:Literal ID="litAccountNumber3" runat="server" /></td>
                                                                <td class="text-right"><asp:Literal ID="litAccountNumber3Data" runat="server" /> </td>
                                                              </tr>   
                                                            </asp:Panel>
                                                           
                                                              <tr> 
                                                                <td class="bold text-uppercase"><asp:Literal ID="litPaymentDateText" runat="server" Text="<%$ Resources:WebResources, LblPaymentDate %>" /></td>
                                                                <td class="text-right"><asp:Literal ID="litPaymentDate" runat="server" /></td>
                                                             </tr> 
                                                            <!-- PAYMENT METHOD -->
                                                            <tr>
                                                                <td class="bold text-uppercase"><asp:Literal ID="litPaymentMethodText" runat="server" Text="<%$ Resources:WebResources, LblPaymentMethod %>" /></td>
                                                                <td class="text-right"><asp:Literal id="litPaymentMethod" runat="server" /></td>
                                                            </tr>  

                                                            <!-- EMAIL ADDRESS -->
                                                            <asp:Panel ID="pnlEmailAddress" runat="server" Visible="false">
                                                             <tr> 
                                                                <td class="bold text-uppercase"><asp:Literal ID="litEmailAddressText" runat="server" Text="<%$ Resources:WebResources, LblEmailAddressText %>" /></td>
                                                                <td class="text-right"><asp:Literal ID="litEmailAddress" runat="server" /></td>
                                                             </tr> 
                                                             </asp:Panel>    
                                                          
                                                             <!-- PAYMENT AMOUNT -->
                                                             <tr> 
                                                                <td class="bold text-uppercase"><asp:Literal ID="litAmountText" runat="server" Text="<%$ Resources:WebResources, LblAmount %>" /></td>
                                                                <td class="text-right"><asp:Literal ID="litAmount" runat="server" /></td>
                                                             </tr> 
                                                            <asp:Panel ID="pnlFees" runat="server" Visible="false">
                                                                <!-- FEE AMOUNT -->
                                                                 <tr> 
                                                                    <td class="bold text-uppercase"><asp:Literal ID="litFeeText" runat="server" Text="<%$ Resources:WebResources, LblFee %>" /></td>
                                                                    <td class="text-right"><asp:Literal ID="litFee" runat="server" /></td>
                                                                 </tr> 
                                                                <!-- TOTAL PAYMENT AMOUNT -->
                                                                <tr><td colspan="2"><hr /></td></tr>
                                                                 <tr> 
                                                                    <td class="bold text-uppercase text-primarydark"><asp:Literal ID="litTotalAmountText" runat="server" Text="<%$ Resources:WebResources, LblTotalAmount %>" /></td>
                                                                    <td class="text-right bold text-primarydark"><asp:Literal ID="litTotalAmount" runat="server" /></td>
                                                                 </tr> 
                                                            </asp:panel>
                                                        </table>

                                                      </div>   
                                             <div class="row">
                                                <div class="col-xs-12 text-muted">
                                                    <br />
                                                    <small><em><asp:Literal ID="litSystemMessage" runat="server" /></em></small>
                                                </div>
                                            </div>      
                                        </asp:Panel>
                                        </div>
                                        <!--// END PRINT CONTENT //-->


                                         <!--// CREATE PROFILE //--> 
                                        <div>
                                              <asp:Panel ID="pnlCreateProfile" runat="server">
                                                   <br /><br />
                                                         <div class="row">
                                                            <div class="col-xs-12 text-right"><button type="button" class="btn btn-primary btn-sm btn-block" title="Create Profile" data-toggle="modal" data-target="#createProfileModal"><asp:Literal runat="server" Text=" <%$ Resources:WebResources, btnCreateProfile%> " /></button></div><br /><br />
                                                            <div class="col-xs-12  text-muted"><small><asp:Literal ID="litCreateProfile" runat="server" Text="<%$ Resources:WebResources, lblCreateProfile %>"></asp:Literal></small></div>
                                                           </div> 
                                                        
                                              </asp:Panel>
                                        </div>
                                        <!--// END CREATE PROFILE //--> 

                                         <!--// PRINT PAGE //--> 
                                        <div>
                                              <asp:Panel ID="Panel1" runat="server">
                                                   <br /><br />
                                                        <div class="row">
                                                            <div class="col-xs-12 text-right"><button type="button" class="btn btn-sm btn-default btn-block" style="white-space: normal;" title="Print" onclick="javascript:Clickheretoprint();"><asp:Literal runat="server" Text="<%$ Resources:WebResources, btnPrint%>" /></button></div><br /><br />
                                                            <div class="col-xs-12 text-muted"><small><asp:Literal ID="litClientMessage" runat="server" Text="<%$ Resources:WebResources, lblCreateProfile %>"></asp:Literal></small></div>
                                                            
                                                        </div>
                                              </asp:Panel>
                                        <!--// END PRINT PAGE //--> 
                                        </div>
                               </div>
                           </div>  
                    </div>
                   </div>    
                </div>     
                 <!--// END MIDDLE CONTENT //-->  
          </div><br /><br />  
      </div>

      <!--// START FOOTER CONTENT //-->

      <b2p:PaymentFooter id="pfDefault" runat="server" />

      <!--// END FOOTER CONTENT //-->

      <!--// START CREATE PROFILE MODAL //-->

          <div class="modal fade" id="createProfileModal" tabindex="-1" role="dialog">
              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    
                     <h4>
                        <asp:Literal id="litFeesTitle" text="<%$ Resources:WebResources, CreateProfileTitle %>" runat="server" />
                      </h4>
                  </div>
                  <div class="modal-body">
                      <p><asp:Literal ID="litCreateProfileInstructions" runat="server" /></p>

                     <b2p:PaymentStatusMessage id="psmErrorMessage" runat="server" />
                      <asp:Panel ID="pnlCreateProfileForm" runat="server">

                        <div class="form-group form-group-sm">
                          <label class="control-label" for="txtUserID"><asp:Literal ID="litUserID" runat="server" text="<%$ Resources:WebResources, lblUserID %>" />:</label>
                          <asp:TextBox id="txtUserID" cssclass="form-control input-sm" placeholder="<%$ Resources:WebResources, lblUserID %>" maxlength="30" runat="server" />
                        </div>
                        <div class="form-group form-group-sm">
                          <label class="control-label" for="txtPassword1"><asp:Literal ID="litPassword1" runat="server" text="<%$ Resources:WebResources, lblPassword1 %>" />:                            
                            
                                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree" tabindex="-1">
                                   <small><asp:Literal id="litPasswordRequirements" text="<%$ Resources:WebResources, lblPasswordRequirements %>" runat="server" /></small>
                                </a>
                            
                            </label>
                            <div id="collapseThree" class="panel-collapse collapse alert-info" role="tabpanel">
                                 <asp:Literal id="Literal1" text="<%$ Resources:WebResources, lblPasswordRequirementsIntro %>" runat="server" />
                                <ul>
                                    <li>
                                        <asp:Literal id="litPasswordBullet1" text="<%$ Resources:WebResources, PasswordRequirementBullet1 %>" runat="server" />
                                    </li>
                                     <li>
                                        <asp:Literal id="litPasswordBullet2" text="<%$ Resources:WebResources, PasswordRequirementBullet2 %>" runat="server" />
                                    </li>
                                     <li>
                                        <asp:Literal id="litPasswordBullet3" text="<%$ Resources:WebResources, PasswordRequirementBullet3 %>" runat="server" />
                                    </li>
                                     <li>
                                        <asp:Literal id="litPasswordBullet4" text="<%$ Resources:WebResources, PasswordRequirementBullet4 %>" runat="server" />
                                    </li> 
                                </ul>
                            </div>


                          <asp:TextBox id="txtPassword1" TextMode="Password" cssclass="form-control input-sm" placeholder="<%$ Resources:WebResources, lblPassword1 %>" maxlength="100" runat="server" />
                        </div>
                       <div class="form-group form-group-sm">
                          <label class="control-label" for="txtPassword2"><asp:Literal ID="litPassword2" runat="server" text="<%$ Resources:WebResources, lblPassword2 %>" />:</label>
                          <asp:TextBox id="txtPassword2" TextMode="Password" cssclass="form-control input-sm" placeholder="Re-type Password" maxlength="100" runat="server" />
                        </div>
                      <div class="form-group form-group-sm">
                          <label class="control-label" for="txtProfileEmailAddress"><asp:Literal ID="litProfileEmailAddress" runat="server" text="<%$ Resources:WebResources, lblEmailAddress %>" />:</label>
                          <asp:TextBox id="txtProfileEmailAddress" cssclass="form-control input-sm" placeholder="Email Address" maxlength="100" runat="server" />
                        </div>

                        
                        <asp:panel ID="pnlCreditCardSuppInfo" runat="server" >
                            <div class="form-group form-group-sm">
                              <label class="control-label" for="ddlCreditCountry"><asp:Literal ID="litCountry" runat="server" text="<%$ Resources:WebResources, lblCountry %>" />:</label>
                              <asp:DropDownList id="ddlCreditCountry"
                                                cssclass="form-control input-sm"
                                                autopostback="true"
                                                runat="server" />
                            </div>

                            <div class="form-group form-group-sm">
                              <label class="control-label" for="txtBillingZip">
                                <asp:Literal id="litBillingZip" text="<%$ Resources:WebResources, lblBillingZip %>" runat="server" />:
                              </label>
                              <asp:TextBox id="txtBillingZip" class="form-control input-sm" placeholder="<%$ Resources:WebResources, lblBillingZip %>" maxlength="6" runat="server" />
                            </div>

                        </asp:panel>

                  </asp:Panel> 
                  </div>
                  <div class="modal-footer">
                    <asp:Button id="btnCancelCreateProfile"
                          text="<%$ Resources:WebResources, ButtonCancel %>"
                          cssclass="btn btn-link"
                          data-dismiss="modal"
                          runat="server" />
                    <asp:Button id="btnCreateProfile"
                          text="<%$ Resources:WebResources, ButtonCreateProfile %>"
                          cssclass="btn btn-primary btn-sm"
                          onclick="btnCreateProfile_Click"
                          usesubmitbehavior="true"                          
                          runat="server" />
                  </div>

                    
                </div>
              </div>
            </div>

      <!--// END CREATE PROFILE MODAL //-->


    
     <!-- JavaScript -->
      <script src="/Js/jquery-1.11.1.min.js"></script>
      <script src="/Js/bootstrap.min.js"></script>
      <script src="/Js/jquery.payment.1.4.4.min.js"></script>

      <!--[if lt IE 9]><script src="/Js/B2P.Enums.IE8.js"></script><![endif]-->
      <!--[if (gte IE 9)|!(IE)]><!--><script src="/Js/B2P.Enums.js"></script><!--<![endif]-->
      <script src="/Js/B2P.Utility.js"></script>
      <script src="/Js/B2P.FormValidator.js"></script>
      <script src="/Js/B2P.ValidationType.js"></script>

         <script type="text/javascript">
             function Clickheretoprint()
                    { 
                      var disp_setting="toolbar=yes,location=no,directories=yes,menubar=yes,"; 
                          disp_setting+="scrollbars=yes,width=650, height=600, left=100, top=25"; 
                      var content_vlue = document.getElementById("print_content").innerHTML; 
          
                      var docprint=window.open("","",disp_setting); 
                       docprint.document.open(); 
                       docprint.document.write('<html><head><title>Payment Receipt</title>'); 
                       docprint.document.write('<link rel="stylesheet" href="../css/print.css">'); 
                       docprint.document.write('</head><body onLoad="self.print()">');
                       docprint.document.write('<div id="pageheader"> ');
                       docprint.document.write('<%=Session("CustTitle") %></div>');          
                       docprint.document.write(content_vlue);          
                       docprint.document.write('</body><p style="padding-left:210px;"><a href="javascript:self.close();" class="Paragraph" >Close</a></p></html>'); 
                       docprint.document.close(); 
                       docprint.focus(); 
             }

             function validateForm(paymentType) {
                  var doc = document;        
                  var item = new ValidationItem();
              
                  // Create instance of the form validator
                  var validator = new FormValidator();
                  validator.setErrorMessageHeader("<%=GetGlobalResourceObject("WebResources", "ErrMsgHeader").ToString()%>\n\n")
                  validator.setInvalidCssClass("has-error");
                  validator.setAlertBoxStatus(false);

                // Add validation items to validator            
                  validator.addValidationItem(new ValidationItem("txtUserID", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));      
                  validator.addValidationItem(new ValidationItem("txtProfileEmailAddress", fieldTypes.Email, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgEmailAddress").ToString()%>"));
               
                validator.addValidationItem(item = {
                    field: "txtPassword1",
                    styleParent: true,
                    errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrPassword").ToString()%>",
                    isValid: (!!/(?=^.{8,100}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*$/.test(doc.getElementById("txtPassword1").value))
                });

                validator.addValidationItem(item = {
                    field: "txtPassword2",
                    styleParent: true,
                    errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrPassword").ToString()%>",
                    isValid: (!!/(?=^.{8,100}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*$/.test(doc.getElementById("txtPassword2").value) && !!(doc.getElementById("txtPassword1").value === doc.getElementById("txtPassword2").value))
                });

                 switch (paymentType) {
                     case "CC":

                         // Only check the states and zip codes for USA and Canada
                         switch (doc.getElementById("ddlCreditCountry").value.trim()) {
                             case "US":
                                 validator.addValidationItem(new ValidationItem("txtBillingZip", fieldTypes.ZipCodeUnitedStates, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgZipCode").ToString()%>"));
                                 break;
                             case "CA":
                                 validator.addValidationItem(new ValidationItem("txtBillingZip", fieldTypes.ZipCodeCanada, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgZipCode").ToString()%>"));
                                 break;
                             default:
                                 // Remove any invalid CSS classes left after changing dropdown value                              
                                 validator.removeValidationItem("txtBillingZip");
                                 break;
                         }
                 }


                  return validator.validate();
             }

             // Remove any invalid CSS classes left after cancelling the form
             function clearValidationItems() {
                 $('#createProfileModal').find("*").tooltip("destroy");
                 $('#createProfileModal').find("*").removeClass("has-error tooltip-danger");
                 $('#createProfileModal').find("*").removeAttr("data-original-title title");

                 // Hide the error messages
                 $('#pnlStatusMessage').hide();

                 // Clear the form fields
                 $('#txtUserID').val("");
                 $('#txtPassword1').val("");
                 $('#txtPassword2').val("");
                 $('#txtProfileEmailAddress').val("");
                 $('#txtBillingZip').val("");
             }


         </script>
        
        </div>
    </form>

  </body>
</html>
