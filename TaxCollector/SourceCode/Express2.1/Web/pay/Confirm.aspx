﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Confirm.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web.confirm" %>

<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="JavaScriptCheck" Src="~/UserControls/JavaScriptCheck.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentCreditCard" Src="~/UserControls/CreditCard.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentBankAccount" Src="~/UserControls/BankAccount.ascx" %>
<%@ Register TagPrefix="b2p" TagName="ConfirmPaymentTerms" Src="~/UserControls/TermsConditions.ascx" %>
<%@ Register TagPrefix="b2p" TagName="ConfirmFeeTerms" Src="~/UserControls/ConvenienceFee.ascx" %>
<%@ Register TagPrefix="b2p" TagName="BreadCrumbMenu" Src="~/UserControls/BreadCrumbMenu.ascx" %>
<%@ Register Src="~/UserControls/ShoppingCart.ascx" TagPrefix="b2p" TagName="ShoppingCart" %>
<%@ Register Src="~/UserControls/PaymentCartGrid.ascx" TagPrefix="b2p" TagName="PaymentCartGrid" %>
<%@ Register Src="~/UserControls/EditPayment.ascx" TagPrefix="b2p" TagName="EditPayment" %>
<%@ Register Src="~/UserControls/EditContactInfo.ascx" TagPrefix="b2p" TagName="EditContactInfo" %>







<!DOCTYPE html>


<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!-->
<html lang="en">
<!--<![endif]-->


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
    <link rel="stylesheet" href="/Css/custom.css" type="text/css" runat="server" />


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

    <form id="frmDefault" autocomplete="off" runat="server" defaultbutton="btnSubmit">
        <div class="container" style="max-width: 970px;">

            <!--// START LOGO, HEADER AND NAV //-->

            <b2p:PaymentHeader ID="phDefault" runat="server" />

            <!--// END LOGO, HEADER AND NAV //-->
            <div class="row" style="background-color: white; padding-bottom: 10px;">
                <br />

                <div class="col-sm-12">

                    <!--// START MIDDLE CONTENT //-->

                    <div class="row" style="background-color: white;">

                        <!--// START NO SCRIPT CHECK //-->
                        <b2p:JavaScriptCheck ID="pjsJavascript" runat="server" />
                        <!--// END NO SCRIPT CHECK //-->

                        <div class="content">
                            <div class="container" style="min-height: 50%;">
                                <!--// START BREADCRUMBS //-->
                                <asp:Panel ID="pnlNonSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu">
                                    <b2p:BreadCrumbMenu runat="server" PageTagName="PaymentConfirm" ID="BreadCrumbMenu" />

                                    <%--<div class="row">
		                                    <ul class="breadcrumb">
			                                    
			                                    <li class="completed"><a href="/pay/"><span class="badge badge-inverse">1</span> <span class="hidden-xs hidden-sm">Account Details</span></a></li>
                                                <li class="completed"><a href="/pay/payment.aspx"><span class="badge badge-inverse">2</span><span class="hidden-xs hidden-sm"> Payment Details</span></a></li>
			                                    <li class="active"><a href="/pay/confirm.aspx" class="inactiveLink"><span class="badge badge-inverse">3</span> Confirm <span class="hidden-xs hidden-sm">Payment</span></a></li>
			                                    <li><a href="#" class="inactiveLink"><span class="badge">4</span><span class="hidden-xs hidden-sm"> Payment Complete</span></a></li>
		                                    </ul>
	                                    </div>--%>
                                </asp:Panel>

                                <asp:Panel ID="pnlSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu">
                                    <b2p:BreadCrumbMenu runat="server" PageTagName="PaymentConfirm" ID="BreadCrumbMenuSSO" />
                                    <%--<div class="row">
		                                    <ul class="breadcrumb">
			                                    
			                                    <li class="completed"><a href="/pay/payment.aspx"><span class="badge badge-inverse">1</span> <span class="hidden-xs hidden-sm">Payment Details</span></a></li>
			                                    <li class="active"><a href="/pay/confirm.aspx" class="inactiveLink"><span class="badge badge-inverse">2</span> Confirm <span class="hidden-xs hidden-sm">Payment</span></a></li>
			                                    <li><a href="#" class="inactiveLink"><span class="badge">3</span><span class="hidden-xs hidden-sm"> Payment Complete</span></a></li>
		                                    </ul>
	                                    </div>--%>
                                </asp:Panel>
                                <!--// END BREADCRUMBS //-->
                                <div class="col-xs-12 col-sm-12">
                                    <b2p:ShoppingCart runat="server" ID="ShoppingCart" />
                                </div>
                                <div class="col-xs-12 col-sm-12 ">
                                   
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-md-8 col-lg-8">
                                            <strong>
                                                <asp:Literal ID="litReviewPayment" Text="<%$ Resources:WebResources, lblReviewPayment %>" runat="server" /></strong>
                                        </div>
                                    </div>
                                     <br />
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                             <b2p:PaymentCartGrid runat="server" ID="PaymentCartGrid" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <asp:Panel ID="pnlAccountDetails" runat="server">

                                   

                                </asp:Panel>
                                <div class="col-xs-12 col-sm-6 ">

                                    <!-- Terms and conditions here -->
                                    <div id="pnlFormContents" class="row" role="form">
                                        <div class="col-xs-12">
                                            <b2p:ConfirmPaymentTerms ID="cptConfirmPaymentInfo" runat="server" />
                                        </div>
                                    </div>

                                    <!-- Fee conditions here -->
                                    <asp:Panel ID="pnlFeeAgreement" runat="server">
                                        <div id="pnlFeeContents" class="row" role="form">
                                            <div class="col-xs-12">
                                                <b2p:ConfirmFeeTerms ID="cptConfirmFeeInfo" runat="server" />
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <div class="pull-right">
                                        <asp:Button ID="btnCancel" CssClass="btn btn-link btn-sm" Text="<%$ Resources:WebResources, ButtonCancel %>" ToolTip="<%$ Resources:WebResources, ButtonCancel %>" runat="server" OnClientClick="return cancelModal();" />
                                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, ButtonMakeAPayment %>" ToolTip="<%$ Resources:WebResources, ButtonMakeAPayment %>" runat="server" />
                                        <asp:Button ID="btnPleaseWait" CssClass="btn btn-primary btn-sm" Text="Please wait" ToolTip="<%$ Resources:WebResources, ButtonPaynow %>" runat="server" Enabled="false" />
                                        <br />
                                        <br />
                                    </div>

                                </div>
                                <div class="col-xs-12 col-sm-6">
                                    <b2p:EditPayment runat="server" ID="EditPayment" />
                                    <br />
                                    <b2p:EditContactInfo runat="server" ID="EditContactInfo" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <!--// END MIDDLE CONTENT //-->

            </div>
            <br />
            <br />
        </div>

        <!--// START FOOTER CONTENT //-->

        <b2p:PaymentFooter ID="pfDefault" runat="server" />

        <!--// END FOOTER CONTENT //-->


        <!-- START TERMS-CONDITIONS MODAL DIALOG -->
        <div class="modal fade" id="termsModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
                        <h4>
                            <asp:Literal ID="litTermsTitle" Text="<%$ Resources:WebResources, ConfirmTermsConditions %>" runat="server" />
                        </h4>
                    </div>

                    <div class="modal-body">
                        <div class="form-group form-group-sm">
                            <label for="txtTerms" class="hidden">Terms and Conditions</label>
                            <asp:TextBox ID="txtTerms" CssClass="form-control input-sm" TextMode="multiline" Rows="12" ReadOnly="true" runat="server" />
                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnTermsConfirm"
                            Text="<%$ Resources:WebResources, ButtonAgree %>"
                            CssClass="btn btn-primary btn-sm"
                            OnClick="btnTermsConfirm_Click"
                            data-dismiss="modal"
                            UseSubmitBehavior="false"
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
                            <asp:Literal ID="litFeesTitle" Text="<%$ Resources:WebResources, ConfirmFeeConditions %>" runat="server" />
                        </h4>
                    </div>

                    <div class="modal-body">
                        <div class="form-group form-group-sm">
                            <label for="txtFees" class="hidden">Convenience Fee Information</label>
                            <asp:TextBox ID="txtFees" CssClass="form-control input-sm" TextMode="multiline" Rows="12" ReadOnly="true" runat="server" />
                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnFeesConfirm"
                            Text="<%$ Resources:WebResources, ButtonAgree %>"
                            CssClass="btn btn-primary btn-sm"
                            OnClick="btnFeesConfirm_Click"
                            data-dismiss="modal"
                            UseSubmitBehavior="false"
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
                            <asp:Literal ID="Literal1" Text="<%$ Resources:WebResources, FeeInfoModalTitle %>" runat="server" />
                        </h4>
                    </div>

                    <div class="modal-body">
                        <asp:Panel ID="pnlACHFee" runat="server" Visible="false">
                            <p class="text-primarydark">
                                <strong>
                                    <asp:Literal ID="litBankAccountPaymentsTitle" Text="<%$ Resources:WebResources, lblBankAccountPayments %>" runat="server" /></strong>
                            </p>
                            <hr />
                            <p>
                                <asp:Literal ID="litACHFee" runat="server" />
                            </p>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlCCFee" runat="server" Visible="false">
                            <p class="text-primarydark">
                                <strong>
                                    <asp:Literal ID="litCreditCardPaymentsTitle" Text="<%$ Resources:WebResources, lblCreditCardPayments %>" runat="server" /></strong>
                            </p>
                            <hr />
                            <p>
                                <asp:Literal ID="litCCFee" runat="server" />
                            </p>
                        </asp:Panel>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="Button1"
                            Text="<%$ Resources:WebResources, ButtonClose %>"
                            CssClass="btn btn-primary btn-sm"
                            data-dismiss="modal"
                            runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <!-- END FEE MODAL DIALOG -->
        <!-- START CANCEL CONFIRM MODAL DIALOG -->
            <div id="cancelModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                           
                            <h4 class="modal-title"><asp:Literal ID="Literal3" Text="<%$ Resources:WebResources, ModalCancelConfirmation %>" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div id="div1" role="alert" style="margin-top: 10px;" class="alert alert-success">
                                <div class="status-msg-text">
                                    <span class="control-label"><asp:Literal ID="Literal5" Text="<%$ Resources:WebResources, ModalCancelConfirmationText %>" runat="server" /></span>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button type="button" ID="btnYes" runat="server" CssClass="btn btn-primary" title="Yes" OnClick="btnYes_Click" Text="<%$ Resources:WebResources, YesButton %>"></asp:Button>
                            <asp:Button type="button" ID="btnNo" runat="server" CssClass="btn btn-primary" title="No" OnClick="btnNo_Click" Text="<%$ Resources:WebResources, NoButton %>"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END CANCEL CONFIRM MODAL DIALOG -->

        <!-- JavaScript -->
        <script src="/Js/jquery-1.11.1.min.js"></script>
        <script src="/Js/bootstrap.min.js"></script>
        <script src="/Js/jquery.payment.1.4.4.min.js"></script>

        <!--[if lt IE 9]><script src="/Js/B2P.Enums.IE8.js"></script><![endif]-->
        <!--[if (gte IE 9)|!(IE)]><!-->
        <script src="/Js/B2P.Enums.js"></script>
        <!--<![endif]-->

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
            //Open cancel modal window
            function cancelModal() {              
                $('#cancelModal').modal();
                return false;
            }

        </script>

    </form>

</body>
</html>
