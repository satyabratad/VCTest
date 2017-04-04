<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web._sessiondefault" %>
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
	<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Session Expired</title>

    <!-- CSS -->    
    <link rel="stylesheet" href="/Css/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="/Css/app.css" type="text/css" id="lnkCSS" runat="server" />

    <!-- JavaScript -->
    <!--[if lt IE 9]>
        <script src="/Js/html5shiv.min.js"></script>
        <script src="/Js/respond.min.js"></script>
    <![endif]-->
  </head>

  <body>

    <form id="frmSessionExpired" autocomplete="off" runat="server">
        <div class="container" style="max-width:970px;">
  <!--// START LOGO, HEADER AND NAV //-->

      <b2p:PaymentHeader id="phConfirmPaymentInfo" runat="server" />

      <!--// END LOGO, HEADER AND NAV //-->
     
        <div class="row" style="padding-top:10px; padding-bottom:10px;">
                   <%-- <div class="col-sm-1"></div>   --%> 
                    <div class="col-sm-10">
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






                              <!--// START MIDDLE CONTENT //-->
                              <div class="container">
                                  <div class="row" style="background-color:white; padding:5px;">
                                      <div class="content">
                                        <div class="row">
                                          <div class="col-xs-12">
                                            <h3 class="text-primary"><asp:Literal id="litMissingClientHeader" text="<%$ Resources:WebResources, MissingClientHeading %>" runat="server" /></h3>
                                            <hr />
                                            <br />
                                          </div>
                                        </div>

                                        <div class="row">
                                          <div class="col-xs-12 col-sm-6">
                                            <b2p:PaymentStatusMessage id="psmErrorMessage" runat="server" />
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
                              </div>

                       <%-- <div class="col-sm-1"></div>--%>
           </div>
           </div>
          
      <!--// END MIDDLE CONTENT //-->

      <!--// START FOOTER CONTENT //-->

      <b2p:PaymentFooter id="pfSessionExpired" runat="server" />

      <!--// END FOOTER CONTENT //-->

      <!-- JavaScript -->
      <script src="/Js/jquery-1.11.1.min.js"></script>
      <script src="/Js/bootstrap.min.js"></script>

           
           
    
            </div>
    </form>

  </body>
</html>