﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Payment.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web.payment" %>

<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="JavaScriptCheck" Src="~/UserControls/JavaScriptCheck.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentCreditCard" Src="~/UserControls/CreditCard.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentBankAccount" Src="~/UserControls/BankAccount.ascx" %>
<%@ Register TagPrefix="b2p" TagName="BreadCrumbMenu" Src="~/UserControls/BreadCrumbMenu.ascx" %>
<%@ Register TagPrefix="b2p" TagName="SubTotal" Src="~/UserControls/SubTotal.ascx" %>
<%@ Register TagPrefix="b2p" TagName="OrderDetails" Src="~/UserControls/OrderDetails.ascx" %>
<%@ Register TagPrefix="b2p" TagName="ShoppingCart" Src="~/UserControls/ShoppingCart.ascx" %>







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
    <title>Express Payment - Payment Details</title>

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
</head>
<body>

    <form id="frmDefault" autocomplete="off" runat="server">
        <div class="container" style="max-width: 970px;">

            <!--// START LOGO, HEADER AND NAV //-->

            <b2p:PaymentHeader ID="phDefault" runat="server" />

            <!--// END LOGO, HEADER AND NAV //-->
            <asp:HiddenField ID="hdCCAmount" runat="server" Value="" />

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
                                    <b2p:BreadCrumbMenu runat="server" PageTagName="PaymentDetails" ID="BreadCrumbMenu" />



                                </asp:Panel>
                                <asp:Panel ID="pnlSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu">
                                    <b2p:BreadCrumbMenu runat="server" PageTagName="PaymentDetails" ID="BreadCrumbMenuSSO" />


                                </asp:Panel>
                                <!--// END BREADCRUMBS -->

                                <div class="col-xs-12 col-sm-12">
                                    <b2p:ShoppingCart runat="server" ID="ShoppingCart" />
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-6 col-sm-push-6">

                                        <b2p:OrderDetails runat="server" ID="OrderDetails" />
                                        <asp:Panel ID="pnlAccountDetails" runat="server" Visible="false">
                                            <br />
                                            <p><strong>My Account Details</strong></p>
                                            <hr />

                                            <div class="table-responsive" style="padding: 0;">
                                                <table id="tblAccountInfo" class="table table-condensed table-no-border">
                                                    <asp:Panel ID="pnlAccount1" runat="server">
                                                        <tr>
                                                            <td class="col-sm-6 text-uppercase">
                                                                <strong>
                                                                    <asp:Literal ID="litAccountNumber1" runat="server" /></strong>
                                                            </td>
                                                            <td>
                                                                <asp:Literal ID="litAccountNumber1Data" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlAccount2" runat="server">
                                                        <tr>
                                                            <td class="col-sm-6 text-uppercase">
                                                                <strong>
                                                                    <asp:Literal ID="litAccountNumber2" runat="server" /></strong>
                                                            </td>
                                                            <td>
                                                                <asp:Literal ID="litAccountNumber2Data" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlAccount3" runat="server">
                                                        <tr>
                                                            <td class="col-sm-6 text-uppercase">
                                                                <strong>
                                                                    <asp:Literal ID="litAccountNumber3" runat="server" /></strong>
                                                            </td>
                                                            <td>
                                                                <asp:Literal ID="litAccountNumber3Data" runat="server" /></td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlAddress" runat="server" Visible="false">
                                                        <tr>
                                                            <td class="col-sm-6 text-uppercase">
                                                                <strong><small>
                                                                    <asp:Literal ID="litAddressLabel" runat="server" Text="<%$ Resources:WebResources, LblServiceAddressText %>" /></small></strong>
                                                            </td>
                                                            <td><small>
                                                                <asp:Literal ID="litAddress" runat="server" /></small></td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlAmountDue" runat="server">
                                                        <tr>
                                                            <td class="col-sm-6 text-uppercase">
                                                                <strong><small>
                                                                    <asp:Literal ID="litAmountDueLabel" runat="server" Text="<%$ Resources:WebResources, LblAmountDue %>" /></small></strong>
                                                            </td>
                                                            <td><small>
                                                                <asp:Literal ID="litAmountDue" runat="server" /></small></td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlDueDate" runat="server">
                                                        <tr>
                                                            <td class="col-sm-6 text-uppercase">
                                                                <strong><small>
                                                                    <asp:Literal ID="litDueDateLabel" runat="server" Text="<%$ Resources:WebResources, LblDueDate %>" /></small></strong>
                                                            </td>
                                                            <td><small>
                                                                <asp:Literal ID="litDueDate" runat="server" /></small></td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlFee" runat="server" Visible="false">
                                                        <tr>
                                                            <td class="col-sm-6 text-uppercase">
                                                                <strong><small>
                                                                    <asp:Literal ID="litFeeLabel" runat="server" Text="<%$ Resources:WebResources, LblFee %>" /></small></strong>
                                                            </td>
                                                            <td><small>"TBD"</small></td>
                                                        </tr>
                                                    </asp:Panel>

                                                </table>
                                            </div>


                                        </asp:Panel>
                                        <div class="hidden-xs">
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


                                    <div class="col-xs-12 col-sm-6 col-sm-pull-6" style="display: none">

                                        <br />
                                        <strong>
                                            <asp:Literal ID="litSelectAmount" runat="server" Text="<%$ Resources:WebResources, lblSelectAmount %>" /></strong>
                                        <asp:Panel ID="pnlCurrentCharges" runat="server">
                                            <div class="col-xs-12 bg-primarydark">

                                                <asp:RadioButton ID="rdCurrentCharges" runat="server" GroupName="PaymentAmount" Checked="true" Text="" CssClass="radio" AutoPostBack="true" />

                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlOtherAmount" runat="server">
                                            <div class="col-xs-12">

                                                <asp:RadioButton ID="rdAmount" runat="server" GroupName="PaymentAmount" CssClass="radio" AutoPostBack="true" />
                                                <label for="txtAmount" class="hidden">Amount</label>
                                                <div class="row">
                                                    <asp:TextBox ID="txtAmount" CssClass="form-control input-sm" placeholder="Payment Amount" MaxLength="10" runat="server" Enabled="false" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <%--<asp:Panel ID="pnlAmountInfo" runat="server">
                                            <div class="col-xs-12 text-right">
                                                <strong>Fee: </strong><asp:Literal id="litConvFee" runat="server" Text="$0.00" /> <strong>Total: </strong><asp:Literal id="litTotalAmount" runat="server" Text="$0.00" />
                                            </div>
                                        </asp:Panel>--%>
                                    </div>


                                    <div class="col-xs-12 col-sm-6 col-sm-pull-6">


                                        <b2p:SubTotal runat="server" ID="SubTotal" />



                                        <div id="pnlFormContents" role="form" runat="server">
                                            <br />
                                            <strong>
                                                <asp:Literal ID="litSelectPaymentMethod" runat="server" Text="<%$ Resources:WebResources, lblSelectPaymentMethod %>" /></strong>
                                            <asp:Button ID="btnFees" CssClass="btn btn-link btn-sm" Text="<%$ Resources:WebResources, ButtonConvFee %>" ToolTip="<%$ Resources:WebResources, ButtonConvFee %>" runat="server" Visible="false" />
                                            <b2p:PaymentStatusMessage ID="psmErrorMessage" runat="server" />


                                            <ul id="paymentTabs" class="nav  nav-tabs" role="tablist">
                                                <li id="tabCredit" runat="server">
                                                    <a href="#pnlTabCredit" onclick="clearValidationItems('ACH');" role="tab" data-toggle="tab">
                                                        <span class="fa fa-credit-card" title="Credit Card"></span>
                                                        <asp:Literal ID="litCreditCardTabName" runat="server" Text="<%$ Resources:WebResources, lblCreditCardTabName %>" />
                                                    </a>
                                                </li>

                                                <li id="tabAch" runat="server">
                                                    <a href="#pnlTabAch" onclick="clearValidationItems('CC')" role="tab" data-toggle="tab">
                                                        <span class="fa fa-bank" title="eCheck"></span>
                                                        <asp:Literal ID="litBankAccountTabName" runat="server" Text="<%$ Resources:WebResources, lblBankAccountTabName %>" />
                                                    </a>
                                                </li>
                                            </ul>

                                            <div class="tab-content">
                                                <br />
                                                <asp:Panel ID="pnlTabCredit" CssClass="tab-pane" DefaultButton="btnSubmitCredit" runat="server">
                                                    <b2p:PaymentCreditCard ID="pccEnterCreditCardInfo" runat="server" />
                                                    <br />
                                                    <div class="pull-right">

                                                        <asp:Button ID="btnCancelCredit" CssClass="btn btn-link btn-sm" Text="<%$ Resources:WebResources, ButtonCancel %>" ToolTip="<%$ Resources:WebResources, ButtonCancel %>" runat="server" OnClientClick="return cancelModal();" />

                                                        <asp:Button ID="btnAddMoreItemCredit" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, AddMoreItemsButton %>" ToolTip="<%$ Resources:WebResources, AddMoreItemsButton %>" runat="server" />
                                                        <asp:Button ID="btnSubmitCredit" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, ButtonContinue %>" ToolTip="<%$ Resources:WebResources, ButtonContinue %>" runat="server" />
                                                        <br />
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlTabAch" CssClass="tab-pane" DefaultButton="btnSubmitAch" runat="server">
                                                    <br />
                                                    <b2p:PaymentBankAccount ID="pbaEnterBankAccountInfo" runat="server" />
                                                    <br />
                                                    <div class="pull-right">
                                                        <asp:Button ID="btnCancelAch" CssClass="btn btn-link btn-sm" Text="<%$ Resources:WebResources, ButtonCancel %>" ToolTip="<%$ Resources:WebResources, ButtonCancel %>" runat="server" OnClientClick="return cancelModal();" />
                                                        <asp:Button ID="btnAddMoreItemAch" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, AddMoreItemsButton %>" ToolTip="<%$ Resources:WebResources, AddMoreItemsButton %>" runat="server" />
                                                        <asp:Button ID="btnSubmitAch" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, ButtonContinue %>" ToolTip="<%$ Resources:WebResources, ButtonContinue %>" runat="server" />
                                                        <br />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>

                            <!--// START SECURITY METRICS LOGO //-->
                            <div class="col-xs-12 col-md-12 text-right" style="padding-right: 10px;">
                                <div class="col-xs-8"></div>
                                <div class="col-xs-6 col-md-2">
                                    <br />
                                    <a href="//www.securitymetrics.com/site_certificate?id=1370759&tk=1f229bcbe3cc16af6c9291efbee0fae5" target="_blank">
                                        <img src="https://www.securitymetrics.com/static/img/site_certified_logos/PCI_DSS_Validated_corporate.png" alt="SecurityMetrics Credit Card Safe" title="SecurityMetrics Credit Card Safe" class="img-responsive" />
                                    </a>
                                </div>
                                <div class="col-xs-6 col-md-2">
                                    <img src="/Img/vs-secure.jpg" alt="Visa Secure" title="Visa Secure" class="img-responsive" />
                                </div>

                            </div>

                            <!--// END SECURITY METRICS LOGO //-->
                        </div>
                    </div>
                </div>

                <!--// END MIDDLE CONTENT //-->

            </div>
            <br />
            <br />
            <%--</div>--%>



            <!-- START PAYMENT AMOUNT MODAL DIALOG -->
            <div class="modal fade" id="amtInfoModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
                            <h4>
                                <asp:Literal ID="litPaymentAmountTitle" Text="<%$ Resources:WebResources, lblPaymentAmountTitle %>" runat="server" />
                            </h4>
                        </div>

                        <div class="modal-body">
                            <asp:Panel ID="pnlBankAccountAmount" runat="server">
                                <p class="text-primarydark">
                                    <strong>
                                        <asp:Literal ID="Literal2" Text="<%$ Resources:WebResources, lblBankAccountPayments %>" runat="server" /></strong>
                                </p>
                                <hr />
                                <p>
                                    <asp:Literal ID="litBankAccountMinMaxAmounts" runat="server" />
                                </p>
                                <br />
                            </asp:Panel>
                            <asp:Panel ID="pnlCreditCardAmount" runat="server">
                                <p class="text-primarydark">
                                    <strong>
                                        <asp:Literal ID="Literal4" Text="<%$ Resources:WebResources, lblCreditCardPayments %>" runat="server" /></strong>
                                </p>
                                <hr />
                                <p>
                                    <asp:Literal ID="litCreditCardMinMaxAmounts" runat="server" />
                                </p>
                            </asp:Panel>

                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="Button2"
                                Text="<%$ Resources:WebResources, ButtonClose %>"
                                CssClass="btn btn-primary btn-sm"
                                data-dismiss="modal"
                                runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- END PAYMENT AMOUNT MODAL DIALOG -->


            <!-- START CVV MODAL DIALOG -->
            <div class="modal fade" id="ccInfoModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
                            <h4>
                                <asp:Literal ID="litCCVTitle" Text="<%$ Resources:WebResources, CVVModalTitle %>" runat="server" />
                            </h4>
                        </div>

                        <div class="modal-body">
                            <p>
                                <asp:Literal ID="litCVVDesc" Text="<%$ Resources:WebResources, CVVModalDescription %>" runat="server" />
                            </p>
                            <p>
                                <img id="imgCVV" class="img-responsive center-block" src="/img/cvv-code-image.jpg" title="" alt="CVV Help Image" />
                            </p>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnCloseCreditModal"
                                Text="<%$ Resources:WebResources, ButtonClose %>"
                                CssClass="btn btn-primary btn-sm"
                                data-dismiss="modal"
                                runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- END CVV MODAL DIALOG -->

            <!-- START CHECK HELP MODAL DIALOG -->
            <div class="modal fade" id="achInfoModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
                            <h4>
                                <asp:Literal ID="litCheckImageTitle" Text="<%$ Resources:WebResources, CheckHelpModalTitle %>" runat="server" />
                            </h4>
                        </div>

                        <div class="modal-body">
                            <p>
                                <asp:Literal ID="litCheckHelpText" Text="<%$ Resources:WebResources, CheckHelpModalDescription %>" runat="server" />
                            </p>
                            <p>
                                <img id="imgCheckHelp" class="img-responsive center-block" src="/img/bank_check.jpg" title="" alt="Routing and Bank Account Number Help" />
                            </p>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnCloseCheckModal"
                                Text="<%$ Resources:WebResources, ButtonClose %>"
                                CssClass="btn btn-primary btn-sm"
                                data-dismiss="modal"
                                runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- END CHECK HELP MODAL DIALOG -->

            <!-- START ACH FAILED VALIDATION MODAL DIALOG -->
            <asp:Panel ID="pnlAchInvalidModal" CssClass="modal fade" TabIndex="-1" role="dialog" aria-label="Invalid Bank Account Number" aria-hidden="true" runat="server">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title"><span class="fa fa-question-circle"></span>&nbsp;&nbsp;<asp:Literal ID="litACHInvalidHeader" Text="<%$ Resources:WebResources, ACHInvalidModalTitle %>" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Literal ID="litAchInvalidMsg" runat="server" />
                            <br />
                            <br />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCloseFailedValidationModal"
                                Text="<%$ Resources:WebResources, ButtonClose %>"
                                CssClass="btn btn-sm btn-primary btn-sm"
                                ToolTip="<%$ Resources:WebResources, ButtonClose %>"
                                data-dismiss="modal"
                                runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- END CONFIRM DELETE MODAL DIALOG -->

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
                            <asp:Panel ID="pnlACHFee" runat="server">
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
                            <asp:Panel ID="pnlCCFee" runat="server">
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

            <!-- START PAYMENT WARNING MODAL DIALOG -->
            <div class="modal fade" id="msgModal" tabindex="-1" role="dialog" aria-label="Message" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header modal-header-danger">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
                            <h4>
                                <asp:Literal ID="litImportantMessage" Text="<%$ Resources:WebResources, lblImportantMessage %>" runat="server" /></h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-group form-group-sm">
                                <asp:Label ID="lblPaymentMsg" runat="server" />
                            </div>
                        </div>

                        <div class="modal-footer modal-footer-danger">
                            <button type="button" class="btn btn-sm btn-confirm" title="OK" data-dismiss="modal">OK</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END PAYMENT WARNING MODAL DIALOG -->
            <!-- START CANCEL CONFIRM MODAL DIALOG -->
            <div id="cancelModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>

                            <h4 class="modal-title">
                                <asp:Literal ID="Literal3" Text="<%$ Resources:WebResources, ModalCancelConfirmation %>" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div id="div1" role="alert" style="margin-top: 10px;" class="alert alert-success">
                                <div class="status-msg-text">
                                    <span class="control-label">
                                        <asp:Literal ID="Literal5" Text="<%$ Resources:WebResources, ModalCancelConfirmationText %>" runat="server" /></span>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button type="button" ID="btnYes" runat="server" CssClass="btn btn-primary" title="Yes" OnClick="btnYes_Click" Text="<%$ Resources:WebResources, YesButton %>"></asp:Button>
                            <asp:Button type="button" ID="btnNo" runat="server" CssClass="btn btn-primary" title="No" OnClientClick="return false;" data-dismiss="modal" Text="<%$ Resources:WebResources, NoButton %>"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END CANCEL CONFIRM MODAL DIALOG -->

            <asp:HiddenField ID="hdfCollapseState" runat="server" />
            <asp:HiddenField ID="hdfTabName" runat="server" />

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


                jQuery(function ($) {
                    var firstTab = $('a[data-toggle="tab"]:first');
                    var tabName = null;

                    // Get the last active tab before postback
                    tabName = $("[id*=hdfTabName]").val() != "" ? $("[id*=hdfTabName]").val() : firstTab.attr("href").replace("#", "");

                    $('#paymentTabs a[href="#' + tabName + '"]').tab('show');

                    $("#paymentTabs a").click(function () {
                        $("[id*=hdfTabName]").val($(this).attr("href").replace("#", ""));
                    });

                    // Use Swipe's payment formatters
                    $("#txtCreditCardNumber").payment("formatCardNumber");
                    $("#txtExpireDate").payment("formatCardExpiry");
                    $("#txtCCV").payment("formatCardCVC");


                });

               


                function validateForm(paymentType) {


                    var doc = document;
                    <% =B2P.PaymentLanding.Express.Web.Utility.BuildAllowedCardsPattern()%>
                    var panel = doc.getElementById("pnlOtherAmount");
                    var cardType = $.payment.cardType(doc.getElementById("txtCreditCardNumber").value);
                    var item = new ValidationItem();


                    // Create instance of the form validator
                    var validator = new FormValidator();
                    validator.setErrorMessageHeader("<%=GetGlobalResourceObject("WebResources", "ErrMsgHeader").ToString()%>\n\n")
                    validator.setInvalidCssClass("has-error");
                    validator.setAlertBoxStatus(false);

                    // Add validation items to validator

                    switch (paymentType) {

                        case "CC":

                     <% =B2P.PaymentLanding.Express.Web.Utility.BuildCCAmount()%>
                            var cardType = $.payment.cardType(doc.getElementById("txtCreditCardNumber").value);
                            var currentYear = "20";
                            var cardMonth = doc.getElementById("txtExpireDate").value.split("/")[0];
                            var cardYear = currentYear.concat(doc.getElementById("txtExpireDate").value.split("/")[1]);
                            var cardYear = cardYear.replace(" ", "");
                            var cardMonth = cardMonth.replace(" ", "");


                            validator.addValidationItem(new ValidationItem("txtNameonCard", fieldTypes.Name, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgNameonCard").ToString()%>"));

                            // Let the next three fields validate with Swipe's payment utility (jquery.payment.js)
                            // Set the objects up using JSON to set the fields
                            validator.addValidationItem(item = {
                                field: "txtCreditCardNumber",
                                styleParent: true,
                                errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgCreditCardNumber").ToString()%>",
                                isValid: ($.payment.validateCardNumber(doc.getElementById("txtCreditCardNumber").value) && validCardPattern.test(cardType))
                            });

                            validator.addValidationItem(item = {


                                field: "txtExpireDate",
                                styleParent: true,
                                errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgCreditCardExpiry").ToString%>",
                                isValid: (!!/^\d{2} \/ \d{2}$/.test(doc.getElementById("txtExpireDate").value) &&
                                          $.payment.validateCardExpiry($.payment.cardExpiryVal(cardMonth.concat("/", cardYear))) &&
                                          cardYear <= new Date().getFullYear() + 10)
                            });


                            validator.addValidationItem(item = {
                                field: "txtCCV",
                                styleParent: true,
                                errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgCreditCardCVC").ToString()%>",
                        isValid: ($.payment.validateCardCVC(doc.getElementById("txtCCV").value, cardType))
                    });

                            // setting default country as US if nothing is selected
                    var selectedCountry = '';
                    if ($("#ddlCountry").val() == '') {
                        selectedCountry = "US";
                    }
                    else {
                        selectedCountry = $("#ddlCountry").val();
                    }

                    zip = doc.getElementById('txtBillingZip').value;
                            // Check to see if zip is required
                    if ($("#hdZipRequired").val() == 'Y') {
                        if (zip == '') {
                            validator.addValidationItem(new ValidationItem("txtBillingZip", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                      }
                  }
                  if (zip !== '') {
                      // Set the validator
                      if (selectedCountry == "US")
                          validator.addValidationItem(new ValidationItem("txtBillingZip", fieldTypes.ZipCodeUnitedStates, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgBillingZip").ToString()%>"));
                          if (selectedCountry == "CA")
                              validator.addValidationItem(new ValidationItem("txtBillingZip", fieldTypes.ZipCodeCanada, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgBillingZip").ToString()%>"));
                              if (selectedCountry == "OT")
                                  validator.addValidationItem(new ValidationItem("txtBillingZip", fieldTypes.ZipCodeInternational, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgBillingZip").ToString()%>"));
                          }
                            //  commented By RS [functionality implemented in server side]
                            //if (panel && doc.getElementById("rdAmount").checked) {

                             <%-- validator.addValidationItem(item = {
                                  field: "txtAmount",
                                  styleParent: true,
                                  errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgAmount").ToString()%>",
                                isValid: (validatePaymentAmount(doc.getElementById("txtAmount").value, ccMinMaxAmounts))
                            });--%>

                            // }



                            break;

                        case "ACH":
                                <% =B2P.PaymentLanding.Express.Web.Utility.BuildACHAmount()%>
                        validator.addValidationItem(new ValidationItem("txtNameonBankAccount", fieldTypes.Name, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgNameOnBankAccount").ToString()%>"));
                        validator.addValidationItem(new ValidationItem("ddlBankAccountType", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                        validator.addValidationItem(new ValidationItem("txtBankRoutingNumber", fieldTypes.RoutingNumber, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRoutingNumber").ToString()%>"));

                        validator.addValidationItem(item = {
                            field: "txtBankAccountNumber",
                            styleParent: true,
                            errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgBankAccount").ToString()%>",
                              isValid: (!!/^\d{1,17}$/.test(doc.getElementById("txtBankAccountNumber").value))
                          });

                        //validator.addValidationItem(item = {
                        //    field: "txtBankAccountNumber2",
                        //    styleParent: true,
                        //    errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgBankAccount").ToString()%>",
                        //    isValid: (!!/^\d{1,17}$/.test(doc.getElementById("txtBankAccountNumber2").value) && !!(doc.getElementById("txtBankAccountNumber").value === doc.getElementById("txtBankAccountNumber2").value))
                        //});

                        // commented By RS [functionality implemented in server side]
                        //if (panel && doc.getElementById("rdAmount").checked) {
                             <%-- validator.addValidationItem(item = {
                                  field: "txtAmount",
                                  styleParent: true,
                                  errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgAmount").ToString()%>",
                                  isValid: (validatePaymentAmount(doc.getElementById("txtAmount").value, achMinMaxAmounts))
                              });--%>

                        //}



                        break;
                }

                return validator.validate();

            }

            // Validate payment amount
            function validatePaymentAmount(field, pattern) {

                var amtParts = pattern.split("|");
                var min = amtParts[0].trim();
                var max = amtParts[1].trim();

                return parseFloat(field) >= parseFloat(min) && parseFloat(field) <= parseFloat(max);
            }



            // Remove any invalid CSS classes left after changing tabs
            function clearValidationItems(formType) {
              
                switch (formType) {
                    case "CC":
                        // Remove any invalid CSS classes left after changing tabs
                        $('#pnlTabAch').find("*").tooltip("destroy");
                        $('#pnlTabAch').find("*").removeClass("has-error tooltip-danger");
                        $('#pnlTabAch').find("*").removeAttr("data-original-title title");

                        // Clear the ACH account form fields
                        $('#ddlBankAccountType').val("");
                        $('#txtBankRoutingNumber').val("");
                        $('#txtBankAccountNumber').val("");
                        $('#txtBankAccountNumber2').val("");

                        // Hide the ACH error messages
                        $('#pnlErrorMessage').hide();

                        // Set focus to name field
                        $('#txtAchFirstName').focus();
                       
                        break;

                    case "ACH":
                        // Remove any invalid CSS classes left after changing tabs
                        $('#pnlTabCredit').find("*").tooltip("destroy");
                        $('#pnlTabCredit').find("*").removeClass("has-error tooltip-danger");
                        $('#pnlTabCredit').find("*").removeAttr("data-original-title title");

                        // Clear the CC account form fields
                        $('#txtCreditCardNumber').val("");
                        $('#txtExpireDate').val("");
                        $('#txtCCV').val("");

                        //$('#ddlCountry').selectedIndex = 0;
                        $('#txtBillingZip').val("");

                        // Hide the CC error messages
                        $('#pnlErrorMessage').hide();


                        break;
                }

                 <% if Not B2P.PaymentLanding.Express.BLL.SessionManager.BankAccType Is Nothing Then %>
                        $("#ddlBankAccountType").val('<%=B2P.PaymentLanding.Express.BLL.SessionManager.BankAccType%>');
                 <%End If%>
            }


            //Open cancel modal window
            function cancelModal() {
                $('#cancelModal').modal();
                return false;
            }



            </script>

        </div>
        <!--// START FOOTER CONTENT //-->

        <b2p:PaymentFooter ID="pfDefault" runat="server" />

        <!--// END FOOTER CONTENT //-->
    </form>

</body>
</html>
