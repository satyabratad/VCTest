<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Confirm.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web.confirm" %>
<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentMenuFooter" Src="~/UserControls/MenuFooter.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentCreditCard" Src="~/UserControls/CreditCard.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentBankAccount" Src="~/UserControls/BankAccount.ascx" %>
<%@ Register TagPrefix="b2p" TagName="ConfirmPaymentTerms" Src="~/UserControls/TermsConditions.ascx" %>
<%@ Register TagPrefix="b2p" TagName="ConfirmFeeTerms" Src="~/UserControls/ConvenienceFee.ascx" %>



<!DOCTYPE html>


<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html lang="en"> <!--<![endif]-->


<head id="docHead" runat="server">
	<meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<title>Express Payment - Confirm Payment</title>

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
    <script type="text/javascript">
       window.onload = function () {
           document.getElementById("btnPleaseWait").style.display = "none";
        };
    </script>
  </head>
<body>

    <form id="frmDefault" autocomplete="off" runat="server"  DefaultButton="btnSubmit">
    <div class="container" style="max-width:970px;">
       
        <!--// START LOGO, HEADER AND NAV //-->      
             
        <b2p:PaymentHeader id="phDefault" runat="server" />
             
        <!--// END LOGO, HEADER AND NAV //-->
       
        
        <div class="container-maincontent">
        <div class="row" style="padding-top:10px; padding-bottom:20px;">
            <h1 class="text-hide">Confirm Payment</h1> 
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
                                  <asp:Panel ID="pnlNonSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu" >
                                   <div class="container">
	                                    <div class="row">
		                                    <ul class="breadcrumb">
			                                    
			                                    <li class="completed"><a href="/pay/"><span class="badge badge-inverse">1</span> <span class="hidden-xs hidden-sm">Account Details</span></a></li>
                                                <li class="completed"><a href="/pay/payment.aspx"><span class="badge badge-inverse">2</span><span class="hidden-xs hidden-sm"> Payment Details</span></a></li>
			                                    <li class="active"><a href="/pay/confirm.aspx" class="inactiveLink"><span class="badge badge-inverse">3</span> Confirm <span class="hidden-xs hidden-sm">Payment</span></a></li>
			                                    <li><a href="#" class="inactiveLink"><span class="badge">4</span><span class="hidden-xs hidden-sm"> Payment Complete</span></a></li>
		                                    </ul>
	                                    </div>
                                    </div>
                                      
                                </asp:Panel>
                                <asp:Panel ID="pnlSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu" >
                                    <div class="container">
	                                    <div class="row">
		                                    <ul class="breadcrumb">
			                                    
			                                    <li class="completed"><a href="/pay/payment.aspx"><span class="badge badge-inverse">1</span> <span class="hidden-xs hidden-sm">Payment Details</span></a></li>
			                                    <li class="active"><a href="/pay/confirm.aspx" class="inactiveLink"><span class="badge badge-inverse">2</span> Confirm <span class="hidden-xs hidden-sm">Payment</span></a></li>
			                                    <li><a href="#" class="inactiveLink"><span class="badge">3</span><span class="hidden-xs hidden-sm"> Payment Complete</span></a></li>
		                                    </ul>
	                                    </div>
                                    </div>
                                </asp:Panel> 
                                <!--// END BREADCRUMBS //--> 

                                    <div class="col-xs-12 col-sm-6 ">
                                        <br />
                                        <div class="row"><div class="col-xs-6 col-sm-6 col-md-8 col-lg-8"><strong><asp:Literal id="litReviewPayment" text="<%$ Resources:WebResources, lblReviewPayment %>" runat="server" /></strong></div>
                                             
                                        <div class="text-right col-sm-6 col-md-4 col-lg-4"><a href="/pay/payment.aspx"><i class="fa fa-pencil" aria-hidden="true"></i> <asp:Literal id="litEditDetails" text="<%$ Resources:WebResources, lblEditDetails %>" runat="server" /></a></div>
                                            
                                        </div>
                                       <br />
                                        <asp:Panel ID="pnlAccountDetails" runat="server">
                                                                                                      
                                                      <div class="table-responsive" style="padding:0;">
                                                        <table id="tblAccountInfo" class="table table-condensed table-no-border">
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
                                                            <asp:Panel ID="pnlNameonAccount" runat="server" Visible="false">
                                                              <tr> 
                                                                <td class="bold text-uppercase"><asp:Literal ID="litNameonAccountText" runat="server" Text="<%$ Resources:WebResources, LblNameOnAccount %>" /></td>
                                                                <td class="text-right"><asp:Literal ID="litNameOnAccount" runat="server" /></td>
                                                             </tr> 
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlAddress" runat="server" Visible="false">
                                                              <tr> 
                                                                <td class="bold text-uppercase"><asp:Literal ID="litAddressLabel" runat="server" Text="<%$ Resources:WebResources, LblServiceAddressText %>" /></td>
                                                                <td class="text-right"><asp:Literal ID="litAddress" runat="server" /></td>
                                                             </tr> 
                                                            </asp:Panel> 
                                                            
                                                           <!-- PAYMENT METHOD -->
                                                            <tr>
                                                                <td class="bold text-uppercase"><asp:Literal ID="litPaymentMethodText" runat="server" Text="<%$ Resources:WebResources, LblPaymentMethod %>" /></td>
                                                                <td class="text-right"><asp:Literal id="litPaymentMethod" runat="server" /></td>
                                                            </tr>        
                                                          
                                                             <!-- PAYMENT AMOUNT -->
                                                             <tr> 
                                                                <td class="bold text-uppercase"><asp:Literal ID="litAmountText" runat="server" Text="<%$ Resources:WebResources, LblAmount %>" /></td>
                                                                <td class="text-right"><asp:Literal ID="litAmount" runat="server" /></td>
                                                             </tr> 
                                                            <asp:Panel ID="pnlFees" runat="server" Visible="false">
                                                                <!-- FEE AMOUNT -->
                                                                 <tr> 
                                                                    <td class="bold text-uppercase text-nowrap"><asp:Literal ID="litFeeText" runat="server" Text="<%$ Resources:WebResources, LblFee %>" />
                                                                        <a href="#" data-toggle="modal" data-target="#feeInfoModal" data-backdrop="static" data-keyboard="false" title="Fee Info" tabindex="-1"><i class="fa fa-question-circle fa-1" aria-hidden="true"></i>
                                                                            <span class="text-hide">Fee Information</span>
                                                                        </a></td>
                                                                    <td class="text-right"><asp:Literal ID="litFee" runat="server" /></td>
                                                                 </tr> 
                                                                <!-- TOTAL PAYMENT AMOUNT -->
                                                                <tr><td colspan="2"><hr /></td></tr>
                                                                 <tr> 
                                                                    <td class="bold text-uppercase text-primarydark"><asp:Literal ID="litTotalAmountText" runat="server" Text="<%$ Resources:WebResources, LblTotalAmount %>" /></td>
                                                                    <td class="text-right bold text-primarydark"><asp:Literal ID="litTotalAmount" runat="server" /></td>
                                                                 </tr> 
                                                            </asp:Panel>
                                                        </table>

                                                      </div>                                                                                    

                                        </asp:Panel>


                                         <!-- Terms and conditions here -->
                                            <div id="pnlFormContents" class="row" role="form">
                                              <div class="col-xs-12">
                                                <b2p:ConfirmPaymentTerms id="cptConfirmPaymentInfo" runat="server" />
                                              </div>
                                            </div>

                                         <!-- Fee conditions here -->
                                        <asp:panel ID="pnlFeeAgreement" runat="server">
                                            <div id="pnlFeeContents" class="row" role="form">
                                              <div class="col-xs-12">
                                                <b2p:ConfirmFeeTerms id="cptConfirmFeeInfo" runat="server" />
                                              </div>
                                            </div>
                                        </asp:panel>

                                        <div class="pull-right">
                                            <asp:button ID="btnCancel" cssclass="btn btn-link btn-sm"  text="<%$ Resources:WebResources, ButtonCancel %>" tooltip="<%$ Resources:WebResources, ButtonCancel %>" runat="server" />
                                            <asp:Button id="btnSubmit" cssclass="btn btn-primary btn-sm" text="<%$ Resources:WebResources, ButtonMakeAPayment %>" tooltip="<%$ Resources:WebResources, ButtonMakeAPayment %>" runat="server" />
                                            <asp:Button id="btnPleaseWait" cssclass="btn btn-primary btn-sm" text="Please wait" tooltip="<%$ Resources:WebResources, ButtonPaynow %>" runat="server" Enabled="false" />
                                            <br /><br />
                                        </div>
                                        
                                    </div>
                                

                                
                    </div>
                   </div>            
                </div>
                     
                </div>  
                 <!--// END MIDDLE CONTENT //-->                         
               
                                 
               
                    <div class="col-sm-1"></div>
     
          </div>
           
    
    
      <!--// START MENU FOOTER CONTENT //-->

      <b2p:PaymentMenuFooter id="pfMenu" runat="server" />

      <!--// END MENU FOOTER CONTENT //-->

      <!--// START FOOTER CONTENT //-->

      <b2p:PaymentFooter id="pfDefault" runat="server" />

      <!--// END FOOTER CONTENT //-->
    

        <!-- START TERMS-CONDITIONS MODAL DIALOG -->
      <div class="modal fade" id="termsModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
              <h4>
                <asp:Literal id="litTermsTitle" text="<%$ Resources:WebResources, ConfirmTermsConditions %>" runat="server" />
              </h4>
            </div>

            <div class="modal-body">
              <div class="form-group form-group-sm">
                  <label for="txtTerms" class="hidden">Terms and Conditions</label>  
                <asp:TextBox id="txtTerms" cssclass="form-control input-sm" textmode="multiline" rows="12" readonly="true" runat="server" />
              </div>
            </div>

            <div class="modal-footer">
              <asp:Button id="btnTermsConfirm"
                          text="<%$ Resources:WebResources, ButtonAgree %>"
                          cssclass="btn btn-primary btn-sm"
                          onclick="btnTermsConfirm_Click"
                          data-dismiss="modal"                                
                          usesubmitbehavior="false"
                          runat="server" />
            </div>
          </div>
        </div>
      </div>
      <!-- END TERMS-CONDITIONS MODAL DIALOG -->

    <!-- START FEE-CONDITIONS MODAL DIALOG -->
      <div class="modal fade" id="feesModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
              <h4>
                <asp:Literal id="litFeesTitle" text="<%$ Resources:WebResources, ConfirmFeeConditions %>" runat="server" />
              </h4>
            </div>

            <div class="modal-body">
              <div class="form-group form-group-sm">
                   <label for="txtFees" class="hidden">Convenience Fee Information</label>      
                <asp:TextBox id="txtFees" cssclass="form-control input-sm" textmode="multiline" rows="12" readonly="true" runat="server"  />
              </div>
            </div>

            <div class="modal-footer">
              <asp:Button id="btnFeesConfirm"
                          text="<%$ Resources:WebResources, ButtonAgree %>"
                          cssclass="btn btn-primary btn-sm"
                          onclick="btnFeesConfirm_Click"
                          data-dismiss="modal"                                
                          usesubmitbehavior="false"
                          runat="server" />
            </div>
          </div>
        </div>
      </div>
      <!-- END FEE-CONDITIONS MODAL DIALOG -->

      <!-- START FEE DIALOG -->
      <div class="modal fade" id="feeInfoModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
              <h4>
                <asp:Literal id="Literal1" text="<%$ Resources:WebResources, FeeInfoModalTitle %>" runat="server" />
              </h4>
            </div>

            <div class="modal-body">
                <asp:Panel ID="pnlACHFee" runat="server" Visible="false">
                    <p class="text-primarydark"><strong><asp:Literal id="litBankAccountPaymentsTitle" text="<%$ Resources:WebResources, lblBankAccountPayments %>" runat="server" /></strong></p>
                    <hr />
                      <p>
                        <asp:Literal id="litACHFee" runat="server" />
                      </p> 
                    <br />
                </asp:Panel>
                <asp:Panel ID="pnlCCFee" runat="server" Visible="false">
                    <p class="text-primarydark"><strong><asp:Literal id="litCreditCardPaymentsTitle" text="<%$ Resources:WebResources, lblCreditCardPayments %>" runat="server" /></strong></p>
                    <hr />
                       <p>
                        <asp:Literal id="litCCFee" runat="server" />
                      </p>
                </asp:Panel>               
            </div>

            <div class="modal-footer">
              <asp:Button id="Button1"
                          text="<%$ Resources:WebResources, ButtonClose %>"
                          cssclass="btn btn-primary btn-sm"
                          data-dismiss="modal"
                          runat="server" />
            </div>
          </div>
        </div>
      </div>
      <!-- END FEE MODAL DIALOG -->


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
        function validateForm() {
            <%=B2P.PaymentLanding.Express.Web.Utility.BuildClientEmailRequirement()%>
            var doc = document;
            var checkbox = doc.getElementById("chkFeeAgree");
              // Create instance of the form validator
              var validator = new FormValidator();
              validator.setErrorMessageHeader("<%=GetGlobalResourceObject("WebResources", "ErrMsgHeader").ToString()%>\n\n");
              validator.setInvalidCssClass("has-error");
              validator.setAlertBoxStatus(false);
           
            // Add validation items to validator

            // Check if the email address is required; if so, add the validator
            if (RequireEmail == 'True') {
               validator.addValidationItem(new ValidationItem("txtEmailAddress", fieldTypes.Email, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgEmailAddress").ToString()%>"));
            }
            else {
                // Only validate the e-mail address if it's not blank
              if (document.getElementById("txtEmailAddress") != null) {
                  if (document.getElementById("txtEmailAddress").value.trim() !== "") {
                      validator.addValidationItem(new ValidationItem("txtEmailAddress", fieldTypes.Email, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgEmailAddress").ToString()%>"));
                  }
                  else {
                      // Remove any invalid CSS classes left after removing e-mail data
                      validator.removeValidationItem("txtEmailAddress");
                  }
              }
            }
            validator.addValidationItem(new ValidationItem("chkTermsAgree", fieldTypes.CheckboxChecked, false, "<%=GetGlobalResourceObject("WebResources", "ErrMsgAgreeToTerms").ToString()%>"));

            // Add validation for fees agreement only if the checkbox is present
            if (checkbox) {
                validator.addValidationItem(new ValidationItem("chkFeeAgree", fieldTypes.CheckboxChecked, false, "<%=GetGlobalResourceObject("WebResources", "ErrMsgAgreeToFees").ToString()%>"));
            }              
                        
              if (validator.validate() == true) {
                  // Hide the button so the user can't submit more than once
                  document.getElementById("btnSubmit").style.display = "none";
                  document.getElementById("btnPleaseWait").style.display = "inline";
              }

              else {
                  return validator.validate();

              }

              
          }

        



         



      </script>
        
        </div>
    </form>

  </body>
</html>
