<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web._paydefault" %>

<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="b2p" TagName="JavaScriptCheck" Src="~/UserControls/JavaScriptCheck.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PropertyAddress" Src="~/UserControls/PropertyAddress.ascx" %>
<%@ Register TagPrefix="b2p" TagName="CartGrid" Src="~/UserControls/CartGrid.ascx" %>
<%@ Register TagPrefix="b2p" TagName="BreadCrumbMenu" Src="~/UserControls/BreadCrumbMenu.ascx" %>
<%@ Register TagPrefix="b2p" TagName="EditLookupItem" Src="~/UserControls/EditLookupItem.ascx" %>
<%@ Register Src="~/UserControls/ShoppingCart.ascx" TagPrefix="b2p" TagName="ShoppingCart" %>
<%@ Register Src="~/UserControls/CartDetails.ascx" TagPrefix="b2p" TagName="CartDetails" %>




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

                        <div class="content">
                            <div class="container" style="min-height: 50%;">
                                <!--// START BREADCRUMBS //-->
                                <b2p:BreadCrumbMenu runat="server" ID="BreadCrumbMenu" PageTagName="Home" />
                                <!--// END BREADCRUMBS //-->
                                <div class="col-xs-12 col-sm-12">
                                    <b2p:ShoppingCart runat="server" ID="ShoppingCart" />
                                    <b2p:CartDetails runat="server" ID="CartDetails" />
                                </div>
                                <asp:Panel ID="pnlContent" runat="server" Visible="true">
                                    <div class="col-xs-12 col-sm-6">

                                        <asp:Panel ID="pnlProducts" runat="server">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group form-group-sm">
                                                        <label class="control-label" for="ddlBankAccountType">
                                                            <asp:Literal ID="litSelectProdct" runat="server" Text="<%$ Resources:WebResources, lblSelectProductService %>" /></label>
                                                        <asp:DropDownList ID="ddlCategories"
                                                            CssClass="form-control input-sm"
                                                            AutoPostBack="True"
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
                                                        <asp:TextBox ID="txtLookupAccount1" runat="server" MaxLength="40" CssClass="form-control input-sm" />
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group form-group-sm">
                                                    <asp:Panel ID="pnlAccount2" runat="server" Visible="false">

                                                        <label class="control-label" for="txtLookupAccount2" id="lblLookupAccount2">
                                                            <asp:Label ID="lblAccountNumber2" runat="server" />
                                                        </label>
                                                        <asp:TextBox ID="txtLookupAccount2" runat="server" MaxLength="40" CssClass="form-control input-sm" />
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group form-group-sm">
                                                    <asp:Panel ID="pnlAccount3" runat="server" Visible="false">
                                                        <label class="control-label" for="txtLookupAccount3" id="lblLookupAccount3">
                                                            <asp:Label ID="lblAccountNumber3" runat="server" />
                                                        </label>
                                                        <asp:TextBox ID="txtLookupAccount3" runat="server" MaxLength="40" CssClass="form-control input-sm" />
                                                    </asp:Panel>
                                                </div>
                                                <b2p:PropertyAddress runat="server" ID="ctlPropertyAddress"></b2p:PropertyAddress>

                                                <br />
                                                <div class="pull-right">
                                                    <asp:Button ID="btnLookup" runat="server" Text="Lookup" CssClass="btn btn-primary btn-sm" Width="70px" />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="btnAddtoCart" CssClass="btn btn-primary btn-sm pull-right" Text="<%$ Resources:WebResources, AddToCartButton %>" ToolTip="<%$ Resources:WebResources, AddToCartButton %>" runat="server" Visible="true" OnClientClick="return validateForm();" />
                                        <br />
                                        <br />
                                    </div>

                                    <div class="col-xs-12 col-sm-6">
                                        <br />
                                        <p>
                                            <asp:Label ID="lblClientMessage" runat="server" ToolTip="Client Message"></asp:Label>
                                        </p>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlCart" runat="server" Visible="false">
                                    <div class="col-xs-12 col-sm-12">
                                        <asp:Panel ID="pnlCartGrid" runat="server" Visible="true">
                                            <b2p:CartGrid runat="server" ID="ctlCartGrid" />
                                            <br />
                                            <div class="pull-right">
                                                <asp:Button ID="btnAddMoreItem" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, AddMoreItemsButton %>" ToolTip="<%$ Resources:WebResources, AddMoreItemsButton %>" runat="server" />
                                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, ButtonContinue %>" ToolTip="<%$ Resources:WebResources, ButtonContinue %>" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlCartEmpty" runat="server" Visible="false">
                                            <div class="col-xs-12  bg-primarydark" style="text-align:center">
                                            
                                                <asp:Literal ID="lilEmptyMsg" runat="server"  Text="<%$ Resources:WebResources, lblEmptyCartMsg %>" />
                                            </div> 
                                            <br /><br />
                                            <div class="pull-right">
                                                <asp:Button ID="btnAddItem" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, ButtonAddItems %>" ToolTip="<%$ Resources:WebResources, ButtonAddItems %>" runat="server" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlEditLookupItem">
                                    <b2p:EditLookupItem runat="server" ID="ctlEditLookupItem" />
                                </asp:Panel>
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


        <!-- START LOOKUP MODAL DIALOG -->
        <asp:Panel ID="pnlLookupResults" CssClass="modal fade" TabIndex="-1" role="dialog" aria-label="Lookup Results" aria-hidden="true" runat="server">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Lookup Results</h4>
                    </div>
                    <div class="modal-body">

                        <div role="alert" id="divLookupAlert" runat="server" style="margin-top: 10px;">
                            <div class="fa fa-check-circle-o fa-2x status-msg-icon"></div>
                            <div class="status-msg-text">
                                <asp:Label ID="lblLookupHeader" runat="server" Text="" class="control-label" />
                            </div>
                        </div>

                        <asp:GridView ID="grdLookup" runat="server"
                            AutoGenerateColumns="true"
                            GridLines="None"
                            BorderStyle="none"
                            ShowHeader="false"
                            CellSpacing="5"
                            CssClass="gvhspadding"
                            RowStyle-CssClass="gvhspadding">
                        </asp:GridView>
                        <br />
                        <br />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnClear"
                            runat="server"
                            Text="<%$ Resources:WebResources, ButtonCancel %>"
                            CssClass="btn btn-link btn-sm"
                            ToolTip="<%$ Resources:WebResources, ButtonCancel %>"
                            data-dismiss="modal"
                            OnClick="btnClear_Click"
                            UseSubmitBehavior="false" />


                        <asp:Button ID="btnLookupGo"
                            Text="<%$ Resources:WebResources, ButtonLookupGo %>"
                            CssClass="btn btn-sm btn-primary btn-sm"
                            ToolTip="<%$ Resources:WebResources, ButtonLookupGo %>"
                            data-dismiss="modal"
                            runat="server"
                            OnClick="btnLookupGo_Click"
                            UseSubmitBehavior="false" />
                        <asp:Button ID="btnLookupOK"
                            Text="OK"
                            CssClass="btn btn-sm btn-primary btn-sm"
                            ToolTip="OK"
                            data-dismiss="modal"
                            runat="server"
                            Visible="false"
                            UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- END LOOKUP MODAL DIALOG -->

        <!-- START LOOKUP ERROR MODAL DIALOG -->
        <asp:Panel ID="pnlLookupError" CssClass="modal fade" TabIndex="-1" role="dialog" aria-label="Lookup Results" aria-hidden="true" runat="server">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Lookup Results</h4>
                    </div>
                    <div class="modal-body">
                        <div role="alert" id="divLookupAlertError" runat="server" style="margin-top: 10px;">
                            <div class="fa fa-exclamation-circle fa-2x status-msg-icon"></div>
                            <div class="status-msg-text">
                                <asp:Label ID="lblLookupHeaderError" runat="server" Text="" class="control-label" />
                            </div>
                        </div>


                        <br />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCloseLookupErrorDialog"
                            Text="<%$ Resources:WebResources, ButtonClose %>"
                            CssClass="btn btn-sm btn-primary btn-sm"
                            ToolTip="<%$ Resources:WebResources, ButtonClose %>"
                            data-dismiss="modal"
                            UseSubmitBehavior="false"
                            runat="server" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- END LOOKUP ERROR MODAL DIALOG -->

        <!-- START DUPLICATE ITEM CHECK VALIDATION MODAL DIALOG-->
        <asp:Panel ID="pnlValidationDuplicate" CssClass="modal fade" TabIndex="-1" role="dialog" aria-label="Validation" aria-hidden="true" runat="server">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Validation</h4>
                    </div>
                    <div class="modal-body">
                        <div id="div1" role="alert" style="margin-top: 10px;" class="alert alert-success">
                            <div class="status-msg-text">
                                <asp:Label runat="server" ID="lblDuplicateValidation" Text="<%$ Resources:WebResources, CombineValidationAddToCart %>" class="control-label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnYes" name="btnYes" onclick="bill2payAccountDetails.removeItemsFromCart();"
                            class="btn btn-primary" title="OK" data-dismiss="modal">
                            OK</button>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- END DUPLICATE ITEM CHECK VALIDATION MODAL DIALOG-->

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
            $("#pnlLookupResults").on('shown.bs.modal', function () {
                $('#btnLookupGo').focus();
            });
            $("#pnlLookupError").on('shown.bs.modal', function () {
                $('#btnCloseLookupErrorDialog').focus();
            });
            $("#pnlValidationDuplicate").on('shown.bs.modal', function () {
                $('#btnYes').focus();
            });
            
           
            function validateForm() {
               
                var doc = document;
                var item = new ValidationItem();

                // Create instance of the form validator
                var validator = new FormValidator();
                validator.setErrorMessageHeader("<%=GetGlobalResourceObject("WebResources", "ErrMsgHeader").ToString()%>\n\n")
                validator.setInvalidCssClass("has-error");
                validator.setAlertBoxStatus(false);

                // Add validation items to validator

                if ($("#txtLookupAccount1").css('visibility') == 'visible') {
                    // Check to see if account 1 is present
                    acct1 = doc.getElementById('hdAccount1').value;
                    if (acct1 !== '') {
                        // Set the validator
                        validator.addValidationItem(new ValidationItem("txtLookupAccount1", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                    }
                }

                // Check to see if account 2 is required
                if ($("#txtLookupAccount2").css('visibility') == 'visible') {
                    acct2 = doc.getElementById('hdAccount2').value;
                    if (acct2 !== '') {
                        // Set the validator
                        validator.addValidationItem(new ValidationItem("txtLookupAccount2", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                    }
                }

                // Check to see if account 3 is required
                if ($("#txtLookupAccount3").css('visibility') == 'visible') {
                    acct3 = doc.getElementById('hdAccount3').value;
                    if (acct3 !== '') {
                        // Set the validator
                        validator.addValidationItem(new ValidationItem("txtLookupAccount3", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                    }
                }
               
                <% If B2P.PaymentLanding.Express.BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup Then%>
                // Check to see if amount is required
                // Set the validator

                validator.addValidationItem(new ValidationItem("txtAmount", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                if ($("#txtAmount").val().trim() == "") {
                    // Set the validator
                    validator.addValidationItem(new ValidationItem("txtAmount", fieldTypes.AmountDue, true, "Invalid Amount"));
                    return validator.validate();
                }
                if (!(parseFloat($("#txtAmount").val().trim())) > 0) {
                    // Set the validator
                    validator.addValidationItem(new ValidationItem("txtAmount", fieldTypes.AmountDue, true, "Invalid Amount"));
                    return validator.validate();
                }
                else {
                    var amt = parseFloat($("#txtAmount").val()).toFixed(2);
                    $("#txtAmount").val(amt);
                }
                <%End If%>

                 <%If ctlPropertyAddress.Visible And ctlPropertyAddress.AddressVisible Then %>
                //Validate Zip
               
                if ($("#txtZip").val() != "") {

                    var optiongroup = $('#ddlState').find(":selected").attr("optiongroup");
                    if (optiongroup == null) {
                        validator.addValidationItem(new ValidationItem("txtZip", fieldTypes.ZipCodeInternational, true, "<%=GetGlobalResourceObject("WebResources", "InvalidZipMsg").ToString()%>"));
                    }
                    else {
                        if (optiongroup.toUpperCase() == "USA") {
                            validator.addValidationItem(new ValidationItem("txtZip", fieldTypes.ZipCodeUnitedStates, true, "<%=GetGlobalResourceObject("WebResources", "InvalidZipMsg").ToString()%>"));
                        }
                        else if (optiongroup.toUpperCase() == "CANADA") {
                            validator.addValidationItem(new ValidationItem("txtZip", fieldTypes.ZipCodeCanada, true, "<%=GetGlobalResourceObject("WebResources", "InvalidZipMsg").ToString()%>"));
                        }
                }

            }
                <% End If %>
                return validator.validate();
            }


            function validateFloatKeyPress(el, evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode;
                var number = el.value.split('.');
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                //just one dot
                if (number.length > 1 && charCode == 46) {
                    return false;
                }
                //get the carat position
                var caratPos = getSelectionStart(el);
                var dotPos = el.value.indexOf(".");
                if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                    return false;
                }
                return true;
            }

           
            function getSelectionStart(o) {
                try {
                    if (o.createTextRange) {
                        var r = document.selection.createRange().duplicate()
                        r.moveEnd('character', o.value.length)
                        if (r.text == '') return o.value.length
                        return o.value.lastIndexOf(r.text)
                    } else return o.selectionStart
                }
                catch (exp) { }
            }


        </script>

    </form>
</body>
</html>
