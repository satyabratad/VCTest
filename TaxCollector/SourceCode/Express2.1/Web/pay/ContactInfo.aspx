<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ContactInfo.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web.ContactInfo" %>

<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="b2p" TagName="JavaScriptCheck" Src="~/UserControls/JavaScriptCheck.ascx" %>
<%@ Register TagPrefix="b2p" TagName="BreadCrumbMenu" Src="~/UserControls/BreadCrumbMenu.ascx"  %>
<%@ Register Src="~/UserControls/ShoppingCart.ascx" TagPrefix="b2p" TagName="ShoppingCart" %>



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
    <title>Express Payment - Account Details</title>

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
                        <asp:Panel ID="pnlContent" runat="server" Visible="true">
                            <div class="content">
                                <div class="container" style="min-height: 50%;">
                                    <!--// START BREADCRUMBS //-->
                                    <b2p:BreadCrumbMenu runat="server" ID="BreadCrumbMenu" PageTagName="ContactInfo" />
                                    

                                    <!--// END BREADCRUMBS //-->
                                    <div class="col-xs-12 col-sm-12">
                                        <b2p:ShoppingCart runat="server" ID="ShoppingCart" />
                                </div>
                                    <div class="col-xs-12 col-sm-6">
                                        <br />

                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:HiddenField ID="hdContactName" runat="server" Value="required" />
                                                <asp:HiddenField ID="hdContactAddress1" runat="server" Value="required" />
                                                <asp:HiddenField ID="hdContactCity" runat="server" Value="required" />
                                                <asp:HiddenField ID="hdContactState" runat="server" Value="required" />
                                                <asp:HiddenField ID="hdContactCountry" runat="server" Value="required" />
                                                <asp:HiddenField ID="hdContactZip" runat="server" Value="required" />

                                                <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlHeading">
                                                        <asp:Label runat="server" ID="lblHeading" class="contentHeading" Text="<%$ Resources:WebResources, ContactInfoTitle %>"></asp:Label>
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlContactName">
                                                        <label class="control-label" for="txtContactName" id="lblContactName">
                                                            <asp:Label ID="lblContName" runat="server" Text="<%$ Resources:WebResources, ContactName %>"></asp:Label>
                                                        </label>
                                                        <asp:TextBox runat="server" MaxLength="40" ID="txtContactName"  required="true" class="form-control input-sm"
                                                            onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
                                                            onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)"></asp:TextBox>
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlAddress1">
                                                        <label class="control-label" for="txtContactAddress1" id="lblContactAddress1">
                                                            <asp:Label ID="lblAddress1" runat="server" Text="<%$ Resources:WebResources, Address1Label %>"></asp:Label>
                                                        </label>
                                                        <asp:TextBox runat="server" MaxLength="40" ID="txtContactAddress1"  required="true" class="form-control input-sm"
                                                            onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
                                                            onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)"></asp:TextBox>
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlAddress2">
                                                        <label class="control-label" for="txtContactAddress2" id="lblContactAddress2">
                                                            <asp:Label ID="lblAddress2" runat="server" Text="<%$ Resources:WebResources, Address2Label %>"></asp:Label>
                                                        </label>
                                                        <asp:TextBox runat="server" MaxLength="40" ID="txtContactAddress2" class="form-control input-sm"
                                                            onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
                                                            onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)"></asp:TextBox>
                                                    </asp:Panel>
                                                </div>
                                                 
                                                <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlCountry">
                                                        <label class="control-label" for="ddlContactCountry" id="lblContactCountry">
                                                            <asp:Label ID="lblCountry" runat="server" Text="<%$ Resources:WebResources, lblCountry %>"></asp:Label>
                                                        </label>
                                                        <asp:DropDownList runat="server" ID="ddlContactCountry"  required="true" class="form-control input-sm" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </asp:Panel>
                                                </div>
                                                 <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlState">
                                                        <label class="control-label" for="ddlContactState" id="lblContactState">
                                                            <asp:Label ID="lblState" runat="server" Text="<%$ Resources:WebResources, StateLabel %>"></asp:Label>
                                                        </label>
                                                        <asp:DropDownList runat="server" ID="ddlContactState"  required="true" class="form-control input-sm">
                                                        </asp:DropDownList>
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlCity">
                                                        <label class="control-label" for="txtContactCity" id="lblContactCity">
                                                            <asp:Label ID="lblCity" runat="server" Text="<%$ Resources:WebResources, CityLabel %>"></asp:Label>
                                                        </label>
                                                        <asp:TextBox runat="server" MaxLength="40" ID="txtContactCity"  required="true" class="form-control input-sm"
                                                            onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
                                                            onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)"></asp:TextBox>
                                                    </asp:Panel>
                                                </div>
                                                                                              <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlZip">
                                                        <label class="control-label" for="txtContactZip" id="lblContactZip">
                                                            <asp:Label ID="lblZip" runat="server" Text="<%$ Resources:WebResources, ZipLabel %>"></asp:Label>
                                                        </label>
                                                        <asp:TextBox runat="server" MaxLength="40" ID="txtContactZip"  required="true" class="form-control input-sm"
                                                            onkeypress="return restrictInput(event, restrictionTypes.ZipCode)" onpaste="return reformatInput(this, restrictionTypes.ZipCode)"></asp:TextBox>
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group form-group-sm">
                                                    <asp:Panel runat="server" ID="pnlPhone">
                                                        <label class="control-label" for="txtContactPhone" id="lblContactPhone">
                                                            <asp:Label ID="lblPhone" runat="server" Text="<%$ Resources:WebResources, PhoneLabel %>"></asp:Label>
                                                        </label>
                                                        <asp:TextBox runat="server" MaxLength="15" ID="txtContactPhone"
                                                            class="form-control input-sm" onkeypress="return restrictInput(event, restrictionTypes.NumbersOnly)" onpaste="return reformatInput(this, restrictionTypes.NumbersOnly)"></asp:TextBox>
                                                    </asp:Panel>

                                                    <br />
                                                    <div class="pull-right">
                                                        <asp:Button ID="btnAddMoreItems" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, AddMoreItemsButton %>" ToolTip="<%$ Resources:WebResources, AddMoreItemsButton %>" runat="server" Visible="true" />
                                                       
                                                        <asp:Button ID="btnContinue" runat="server" Text="<%$ Resources:WebResources, ButtonContinue %>" ToolTip="<%$ Resources:WebResources, ButtonContinue %>" CssClass="btn btn-primary btn-sm"/>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <br />
                                            <br />
                                        </div>

                                        <div class="col-xs-12 col-sm-6">
                                            <br />
                                            <p>
                                                <asp:Label ID="lblClientMessage" runat="server" ToolTip="Client Message"></asp:Label>
                                            </p>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

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


        <!-- JavaScript -->
        <script src="/Js/jquery-1.11.1.min.js"></script>
        <script src="/Js/bootstrap.min.js"></script>


        <!--[if lt IE 9]><script src="/Js/B2P.Enums.IE8.js"></script><![endif]-->
        <!--[if (gte IE 9)|!(IE)]><!-->
        <script src="/Js/B2P.Enums.js"></script>
        <!--<![endif]-->

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
                debugger;
                // Add validation items to validator

                // Check to see if contact name is present
                contactName = doc.getElementById('hdContactName').value;
                if (contactName !== '') {
                    // Set the validator
                    validator.addValidationItem(new ValidationItem("txtContactName", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                }

                // Check to see if address1 is required
                address1 = doc.getElementById('hdContactAddress1').value;
                if (address1 !== '') {
                    // Set the validator
                    validator.addValidationItem(new ValidationItem("txtContactAddress1", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                }
                  // Check to see if country is required
                country = doc.getElementById('hdContactCountry').value;
                if (country !== '') {
                    // Set the validator
                    validator.addValidationItem(new ValidationItem("ddlContactCountry", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                }

                if ($("#ddlContactCountry").val() != "OT") {
                    // Check to see if city is required
                    city = doc.getElementById('hdContactCity').value;
                    if (city !== '') {
                        // Set the validator
                        validator.addValidationItem(new ValidationItem("txtContactCity", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                    }
                    // Check to see if state is required
                    state = doc.getElementById('hdContactState').value;
                    if (state !== '') {
                        // Set the validator
                        validator.addValidationItem(new ValidationItem("ddlContactState", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                    }

                    // Check to see if zip is required
                    zip = doc.getElementById('hdContactZip').value;
                    if (zip !== '') {
                        // Set the validator
                        validator.addValidationItem(new ValidationItem("txtContactZip", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                        if ($("#ddlContactCountry").val() == "US")
                            validator.addValidationItem(new ValidationItem("txtContactZip", fieldTypes.ZipCodeUnitedStates, true, "<%=GetGlobalResourceObject("WebResources", "InvalidZipMsg").ToString()%>"));
                        if ($("#ddlContactCountry").val() == "CA")
                            validator.addValidationItem(new ValidationItem("txtContactZip", fieldTypes.ZipCodeCanada, true, "<%=GetGlobalResourceObject("WebResources", "InvalidZipMsg").ToString()%>"));
                        if ($("#ddlContactCountry").val() == "OT")
                            validator.addValidationItem(new ValidationItem("txtContactZip", fieldTypes.ZipCodeInternational, true, "<%=GetGlobalResourceObject("WebResources", "InvalidZipMsg").ToString()%>"));
                    }
                }
                return validator.validate();
            }

            function disableOrEnableControl(ctrlId,state) {
                $("#"+ctrlId).prop('disabled', (state==0));
            }



        </script>

    </form>

</body>
</html>
