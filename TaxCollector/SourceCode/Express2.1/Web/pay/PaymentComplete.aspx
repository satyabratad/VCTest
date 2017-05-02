<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaymentComplete.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web.PaymentComplete" %>

<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="JavaScriptCheck" Src="~/UserControls/JavaScriptCheck.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="b2p" TagName="BreadCrumbMenu" Src="~/UserControls/BreadCrumbMenu.ascx" %>
<%@ Register Src="~/UserControls/PaymentCartGrid.ascx" TagPrefix="b2p" TagName="PaymentCartGrid" %>

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

    <title>Express Payment - Payment Complete</title>

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
                                    <b2p:BreadCrumbMenu runat="server" PageTagName="PaymentSuccess" ID="BreadCrumbMenu" />
                                </asp:Panel>
                                <asp:Panel ID="pnlSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu">
                                    <b2p:BreadCrumbMenu runat="server" PageTagName="PaymentSuccess" ID="BreadCrumbMenuSSO" />
                                </asp:Panel>
                                <!--// END BREADCRUMBS //-->

                                <!--// START PRINT CONTENT //-->

                                <div>
                                    <div id="print_content1">
                                        <div id="divAccountDetails2">
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="col-sm-12 " style="font-size: 12px;" id="headerCustName">
                                                        <h3>
                                                            <asp:Literal ID="litClientName" runat="server" /></h3>
                                                    </div>
                                                    <div class="col-sm-12 contentHeadingBlack14" style="color: green;">
                                                        <strong>
                                                            <asp:Literal ID="litThankYou" runat="server" Text="<%$ Resources:WebResources, ThankYouforYourPayment %>" /></strong><br />
                                                    </div>
                                                    <div class="col-sm-12 " style="color: blue;">
                                                        <asp:Literal ID="litConfirmText" runat="server" Text="<%$ Resources:WebResources, LblConfirmationNumber %>" />:
                                                        <asp:Literal ID="litConfirmationNumber" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <b2p:PaymentCartGrid runat="server" ID="PaymentCartGrid" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divLeftConfirmationPanel">
                                        <div id="leftSideButtons" class="col-xs-12 col-sm-6" style="display: block;">
                                            <div class="col-xs-12">
                                                <asp:Panel ID="pnlCreateProfile" runat="server">
                                                    <div id="pnlEmailInfo" class="form-group form-group-sm">
                                                        <button type="button" class="btn btn-primary btn-sm btn-block" title="Create Profile" data-toggle="modal" data-target="#createProfileModal">
                                                            <asp:Literal runat="server" Text=" <%$ Resources:WebResources, btnCreateProfile%> " /></button>
                                                    </div>
                                                    <div class="form-group form-group-sm">
                                                        <span><i>
                                                            <asp:Literal ID="litCreateProfile" runat="server" Text="<%$ Resources:WebResources, lblCreateProfile %>"></asp:Literal></i></span>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="row">
                                                        <div class="col-xs-12">
                                                            <div class="row">
                                                                <div class="col-sm-8 col-xs-12">
                                                                    <asp:Button runat="server" type="button" Width="100%" class="btn btn-sm" title="Print" OnClientClick="javascript:Clickheretoprint();" Text="<%$ Resources:WebResources, btnPrint%>" />

                                                                </div>
                                                                <div class="col-sm-4 col-xs-12">
                                                                    <asp:Button runat="server" ID="btnAddNew" Width="100%" CssClass="btn btn-primary btn-sm pull-right" Text="<%$ Resources:WebResources, btnNewPayment %>" ToolTip="<%$ Resources:WebResources, btnNewPayment %>" />
                                                                </div>
                                                            </div>


                                                        </div>
                                            <br />
                                            <br />
                                            <div class="col-xs-12 text-muted">
                                                <small>
                                                    <asp:Literal ID="litClientMessage" runat="server" Text="<%$ Resources:WebResources, lblCreateProfile %>"></asp:Literal></small>
                                            </div>

                                        </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div id="print_content2">
                                    <div class="col-xs-12 col-sm-6">
                                        <!--PAYMENT DETAILS-->
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-6 contentHeadingBlack13 ">
                                                    <asp:Literal ID="litPaymentDateText" runat="server" Text="<%$ Resources:WebResources, LblPaymentDate %>" />:
                                                </div>
                                                <div class="col-xs-6 contentHeadingBlack13">
                                                    <span id="paymentDate">
                                                        <asp:Literal ID="litPaymentDate" runat="server" /></span>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-6 contentHeadingBlack13">
                                                    <asp:Literal ID="litPaymentMethodText" runat="server" Text="<%$ Resources:WebResources, LblPaymentMethod %>" />:
                                                </div>
                                                <div class="col-xs-6 contentHeadingBlack13">
                                                    <span class="contentHeadingBlack13" id="paymentMethod">
                                                        <asp:Literal ID="litPaymentMethod" runat="server" /></span>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlEmailAddress" runat="server" Visible="false">

                                                <div class="row">
                                                    <div class="col-xs-6 contentHeadingBlack13">
                                                        <asp:Literal ID="litEmailAddressText" runat="server" Text="<%$ Resources:WebResources, LblEmailAddressText %>" />:
                                                    </div>
                                                     <div class="col-xs-6 contentHeadingBlack13">
                                                    <span class="contentHeadingBlack13" id="confirmMail">
                                                        <asp:Literal ID="litEmailAddress" runat="server" /></span>
                                                         </div>
                                                </div>

                                            </asp:Panel>
                                        </div>
                                        <div class="col-xs-12">
                                            <br>
                                            <br>
                                            <br>
                                        </div>
                                        <div class="col-xs-12">
                                            <div class="row pull-right ">
                                                <i>
                                                    <asp:Literal ID="litSystemMessage" runat="server" /></i>
                                            </div>
                                        </div>
                                        <!--END PAYMENT DETAILS-->
                                    </div>
                                </div>
                                <!--END CONTACT INFORMATION-->
                                <!--END CONFIRM EMAIL AND PAYMENT DETAILS-->
                            </div>
                            <div id="print_content3">
                                <b2p:PaymentStatusMessage ID="PostBackStatusMessage" runat="server" />
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



            <!--// START CREATE PROFILE MODAL //-->

        <div class="modal fade" id="createProfileModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4>
                            <asp:Literal ID="litFeesTitle" Text="<%$ Resources:WebResources, CreateProfileTitle %>" runat="server" />
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <asp:Literal ID="litCreateProfileInstructions" runat="server" />
                        </p>

                        <b2p:PaymentStatusMessage ID="psmErrorMessage" runat="server" />
                        <asp:Panel ID="pnlCreateProfileForm" runat="server">

                            <div class="form-group form-group-sm">
                                <label class="control-label" for="txtUserID">
                                    <asp:Literal ID="litUserID" runat="server" Text="<%$ Resources:WebResources, lblUserID %>" />:</label>
                                <asp:TextBox ID="txtUserID" CssClass="form-control input-sm" placeholder="<%$ Resources:WebResources, lblUserID %>" MaxLength="30" runat="server" />
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="control-label" for="txtPassword1">
                                    <asp:Literal ID="litPassword1" runat="server" Text="<%$ Resources:WebResources, lblPassword1 %>" />:                            
                            
                                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree" tabindex="-1">
                                    <small>
                                        <asp:Literal ID="litPasswordRequirements" Text="<%$ Resources:WebResources, lblPasswordRequirements %>" runat="server" /></small>
                                </a>

                                </label>
                                <div id="collapseThree" class="panel-collapse collapse alert-info" role="tabpanel">
                                    <asp:Literal ID="Literal1" Text="<%$ Resources:WebResources, lblPasswordRequirementsIntro %>" runat="server" />
                                    <ul>
                                        <li>
                                            <asp:Literal ID="litPasswordBullet1" Text="<%$ Resources:WebResources, PasswordRequirementBullet1 %>" runat="server" />
                                        </li>
                                        <li>
                                            <asp:Literal ID="litPasswordBullet2" Text="<%$ Resources:WebResources, PasswordRequirementBullet2 %>" runat="server" />
                                        </li>
                                        <li>
                                            <asp:Literal ID="litPasswordBullet3" Text="<%$ Resources:WebResources, PasswordRequirementBullet3 %>" runat="server" />
                                        </li>
                                        <li>
                                            <asp:Literal ID="litPasswordBullet4" Text="<%$ Resources:WebResources, PasswordRequirementBullet4 %>" runat="server" />
                                        </li>
                                    </ul>
                                </div>


                                <asp:TextBox ID="txtPassword1" TextMode="Password" CssClass="form-control input-sm" placeholder="<%$ Resources:WebResources, lblPassword1 %>" MaxLength="100" runat="server" />
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="control-label" for="txtPassword2">
                                    <asp:Literal ID="litPassword2" runat="server" Text="<%$ Resources:WebResources, lblPassword2 %>" />:</label>
                                <asp:TextBox ID="txtPassword2" TextMode="Password" CssClass="form-control input-sm" placeholder="Re-type Password" MaxLength="100" runat="server" />
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="control-label" for="txtProfileEmailAddress">
                                    <asp:Literal ID="litProfileEmailAddress" runat="server" Text="<%$ Resources:WebResources, lblEmailAddress %>" />:</label>
                                <asp:TextBox ID="txtProfileEmailAddress" CssClass="form-control input-sm" placeholder="Email Address" MaxLength="100" runat="server" />
                            </div>


                            <asp:Panel ID="pnlCreditCardSuppInfo" runat="server">
                                <div class="form-group form-group-sm">
                                    <label class="control-label" for="ddlCreditCountry">
                                        <asp:Literal ID="litCountry" runat="server" Text="<%$ Resources:WebResources, lblCountry %>" />:</label>
                                    <asp:DropDownList ID="ddlCreditCountry"
                                        CssClass="form-control input-sm"
                                        AutoPostBack="true"
                                        runat="server" />
                                </div>

                                <div class="form-group form-group-sm">
                                    <label class="control-label" for="txtBillingZip">
                                        <asp:Literal ID="litBillingZip" Text="<%$ Resources:WebResources, lblBillingZip %>" runat="server" />:
                                    </label>
                                    <asp:TextBox ID="txtBillingZip" class="form-control input-sm" placeholder="<%$ Resources:WebResources, lblBillingZip %>" MaxLength="6" runat="server" />
                                </div>

                            </asp:Panel>

                        </asp:Panel>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCancelCreateProfile"
                            Text="<%$ Resources:WebResources, ButtonCancel %>"
                            CssClass="btn btn-link"
                            data-dismiss="modal"
                            runat="server" />
                        <asp:Button ID="btnCreateProfile"
                            Text="<%$ Resources:WebResources, ButtonCreateProfile %>"
                            CssClass="btn btn-primary btn-sm"
                            OnClick="btnCreateProfile_Click"
                            UseSubmitBehavior="true"
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
        <!--[if (gte IE 9)|!(IE)]><!-->
        <script src="/Js/B2P.Enums.js"></script>
        <!--<![endif]-->
        <script src="/Js/B2P.Utility.js"></script>
        <script src="/Js/B2P.FormValidator.js"></script>
        <script src="/Js/B2P.ValidationType.js"></script>

        <script type="text/javascript">
            function Clickheretoprint() {
                var disp_setting = "toolbar=yes,location=no,directories=yes,menubar=yes,";
                disp_setting += "scrollbars=yes,width=650, height=600, left=100, top=25";

                var content_vlue = "";
                var print_content1 = $("#print_content1").clone().html();
                var print_content2 = $("#print_content2").clone().html();
                var print_content3 = $("#print_content3").clone().html();
                content_vlue = print_content1 + print_content2 + print_content3

                var docprint = window.open("", "", disp_setting);
                docprint.document.open();
                docprint.document.write('<html><head><title>Payment Receipt</title>');

                docprint.document.write('<link rel="stylesheet" href="../css/bootstrap.min.css">');
                docprint.document.write('<link rel="stylesheet" href="../css/print.css">');

                docprint.document.write('</head><body onLoad="self.print()">');
                docprint.document.write('<div id="pageheader"> ');
                docprint.document.write('<%=Session("CustTitle") %></div>');
                    docprint.document.write(content_vlue);
                    // docprint.document.write('</body><p style="padding-left:210px;"><a href="javascript:self.close();" class="Paragraph" >Close</a></p></html>');
                    docprint.document.write('</body></html>');
                    docprint.document.close();
                    docprint.focus();
                    return false;
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
        <!--// START FOOTER CONTENT //-->
        <br />
        <br />
        <b2p:PaymentFooter ID="pfDefault" runat="server" />

        <!--// END FOOTER CONTENT //-->
    </form>

</body>
</html>
