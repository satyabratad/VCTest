<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web._ssodefault" %>

<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentFooter" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="b2p" TagName="JavaScriptCheck" Src="~/UserControls/JavaScriptCheck.ascx" %>
<%@ Register TagPrefix="b2p" TagName="BreadCrumbMenu" Src="~/UserControls/BreadCrumbMenu.ascx" %>
<%@ Register TagPrefix="b2p" TagName="CartGrid" Src="~/UserControls/CartGrid.ascx" %>
<%@ Register Src="~/UserControls/ShoppingCart.ascx" TagPrefix="b2p" TagName="ShoppingCart" %>
<%@ Register Src="~/UserControls/CartDetails.ascx" TagPrefix="b2p" TagName="CartDetails" %>
<%@ Register Src="~/UserControls/EditLookupItem.ascx" TagPrefix="b2p" TagName="EditLookupItem" %>



<!DOCTYPE html>

<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!-->
<html lang="en">
<!--<![endif]-->

<head id="docHead" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Express - Landing Page</title>

    <!-- CSS -->
    <link rel="stylesheet" href="/Css/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
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

    <form id="frmSessionExpired" autocomplete="off" runat="server">
        <div class="container" style="max-width: 970px;">
            <!--// START LOGO, HEADER AND NAV //-->

            <b2p:PaymentHeader ID="phConfirmPaymentInfo" runat="server" />

            <!--// END LOGO, HEADER AND NAV //-->
            <div class="row" style="background-color: white; padding-bottom: 10px;">
                <br />

                <div class="col-sm-12">
                    <!--// START NO SCRIPT CHECK //-->
                    <b2p:JavaScriptCheck ID="pjsJavascript" runat="server" />
                    <!--// END NO SCRIPT CHECK //-->

                    <!--// START MIDDLE CONTENT //-->
                    <div class="content">
                       <div class="row">
                            
                                <!--// START BREADCRUMBS //-->
                                <b2p:BreadCrumbMenu runat="server" ID="BreadCrumbMenu" PageTagName="Home" />
                                <!--// END BREADCRUMBS //-->
                                <div class="col-xs-12 col-sm-12">
                                    <b2p:ShoppingCart runat="server" ID="ShoppingCart" />
                                    <b2p:CartDetails runat="server" ID="CartDetails" />
                                </div>
                                <asp:Panel ID="pnlCart" runat="server" Visible="false">
                                    <div class="col-xs-12 col-sm-12">
                                        <b2p:CartGrid runat="server" ID="ctlCartGrid" />
                                        <br />
                                        <div class="pull-right">

                                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, ButtonContinue %>" ToolTip="<%$ Resources:WebResources, ButtonContinue %>" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                                    <b2p:EditLookupItem runat="server" ID="ctlEditLookupItem" />
                                </asp:Panel>
                            </div>

                            <asp:Panel ID="pnlError" runat="server" Visible="true">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="text-primary">
                                            <asp:Literal ID="litMissingClientHeader" Text="<%$ Resources:WebResources, MissingClientHeading %>" runat="server" /></h3>
                                        <hr />
                                        <br />
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-xs-12 col-sm-6">
                                        <b2p:PaymentStatusMessage ID="psmErrorMessage" runat="server" />
                                    </div>
                                </div>

                            </asp:Panel>
                       
                    </div>
                    <%-- <div class="col-sm-1"></div>--%>
                </div>
            </div>
            <br />
            <br />
        </div>
        <!--// END MIDDLE CONTENT //-->

        <!--// START FOOTER CONTENT //-->

        <b2p:PaymentFooter ID="pfSessionExpired" runat="server" />

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




    </form>

</body>
</html>
