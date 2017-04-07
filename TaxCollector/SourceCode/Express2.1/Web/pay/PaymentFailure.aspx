<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaymentFailure.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web.PaymentFailure" %>
<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="b2p" TagName="JavaScriptCheck" Src="~/UserControls/JavaScriptCheck.ascx" %>
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
	<title>Express Payment - Payment Failed</title>

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
                                    
                        <div class="row" style="background-color:white; padding:5px;">
                              <!--// START NO SCRIPT CHECK //-->
                                    <b2p:JavaScriptCheck ID="pjsJavascript" runat="server" />
                                <!--// END NO SCRIPT CHECK //-->

                        
                            <div class="content">
                                <div class="container" style="min-height:50%;">
                                <!--// START BREADCRUMBS //-->
                                  <asp:Panel ID="pnlNonSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu" >
                                   
	                                    <div class="row">
		                                    <ul class="breadcrumb">
			                                    
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">1</span> <span class="hidden-xs hidden-sm">Account Details</span></a></li>
                                                <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">2</span><span class="hidden-xs hidden-sm"> Payment Details</span></a></li>
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">3</span><span class="hidden-xs hidden-sm"> Confirm Payment</span></a></li>
			                                    <li class="danger"><a href="#" class="inactiveLink"><span class="badge badge-inverse">4</span> <span class="hidden-xs hidden-sm">Payment </span>Failed</a></li>
		                                    </ul>
	                                    </div>
                                   
                                </asp:Panel>
                                <asp:Panel ID="pnlSSOBreadcrumb" runat="server" aria-hidden="true" aria-label="Breadcrumb Menu" >
                                    
	                                    <div class="row">
		                                    <ul class="breadcrumb">
			                                    
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">1</span> <span class="hidden-xs hidden-sm">Payment Details</span></a></li>
			                                    <li class="completed"><a href="#" class="inactiveLink"><span class="badge badge-inverse">2</span> <span class="hidden-xs hidden-sm">Confirm Payment</span></a></li>
			                                    <li class="danger"><a href="#" class="inactiveLink"><span class="badge badge-inverse">3</span> <span class="hidden-xs hidden-sm">Payment </span>Failed</a></li>
		                                    </ul>
	                                    </div>
                                    
                                </asp:Panel> 
                                <!--// END BREADCRUMBS //--> 

                                 
                                    <div class="col-xs-12 col-sm-6">
                                        <h3 class="text-primary">Payment Failed</h3>
                                        <hr />
                                        <br />
                                        <b2p:PaymentStatusMessage id="psmFailureMessage" runat="server" />
                                        <br />
                                        <p>
                                            <asp:Literal id="litIntro2" text="<%$ Resources:WebResources, FailPageCreditExplanation %>" runat="server" />
                                        </p>
                                        <br />
                                        <div id="failureReasons" class="alert alert-info">     
                                            <ul>
                                            <li>
                                                <asp:Literal id="litBullet1" text="<%$ Resources:WebResources, FailPageCreditReason1 %>" runat="server" />
                                            </li>         
                                            <li>
                                                <asp:Literal id="litBullet2" text="<%$ Resources:WebResources, FailPageCreditReason2 %>" runat="server" /> 
                                                <asp:LinkButton id="lnkBtnColHelp"
                                                                cssclass="alert-link"
                                                                text="<%$ Resources:WebResources, CVVModalLinkText %>"
                                                                tooltip="CVV Info"
                                                                tabindex="-1"
                                                                data-toggle="modal"
                                                                data-target="#ccInfoModal"
                                                                data-backdrop="static"
                                                                data-keyboard="false"
                                                                runat="server" />.
                                            </li>
                                            <li>
                                                <asp:Literal id="litBullet3" text="<%$ Resources:WebResources, FailPageCreditReason3 %>" runat="server" />
                                            </li>
                                            </ul>
                                        </div>
                                        <br />
                                        <p>
                                            <asp:Literal id="litBankAccountIntro" text="<%$ Resources:WebResources, FailPageBankAccountExplanation %>" runat="server" />
                                        </p>
                                        <br />
                                        <br />
                                        <div class="pull-right">
                                            <!--<asp:Button id="btnCancel" cssclass="btn btn-primary btn-sm" text="<%$ Resources:WebResources, ButtonCancel %>" tooltip="<%$ Resources:WebResources, ButtonCancel %>" runat="server" />-->
                                            <asp:Button id="btnTryAgain" cssclass="btn btn-primary btn-sm" text="<%$ Resources:WebResources, ButtonTryAgain %>" tooltip="<%$ Resources:WebResources, ButtonTryAgain %>" runat="server" />
                                        </div>
                                    </div>
                                
                             
                       </div>    
                    </div>  
                    </div>                        
                </div>
                    <!--// END MIDDLE CONTENT //-->               
               
          </div>       <br /><br />      
     </div>
    
      <!--// START FOOTER CONTENT //-->

      <b2p:PaymentFooter id="pfDefault" runat="server" />

      <!--// END FOOTER CONTENT //-->
       
        <!-- START CVV MODAL DIALOG -->
      <div class="modal fade" id="ccInfoModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
              <h4>
                <asp:Literal id="litCCVTitle" text="<%$ Resources:WebResources, CVVModalTitle %>" runat="server" />
              </h4>
            </div>

            <div class="modal-body">
              <p>
                <asp:Literal id="litCVVDesc" text="<%$ Resources:WebResources, CVVModalDescription %>" runat="server" />
              </p>
              <p>
                <img id="imgCVV" class="img-responsive center-block" src="/img/cvv-code-image.jpg" title="" />
              </p>
            </div>

            <div class="modal-footer">
              <asp:Button id="btnCloseCreditModal"
                          text="<%$ Resources:WebResources, ButtonClose %>"
                          cssclass="btn btn-primary btn-sm"
                          data-dismiss="modal"
                          runat="server" />
            </div>
          </div>
        </div>
      </div>
      <!-- END CVV MODAL DIALOG -->  

         

    



     <!-- JavaScript -->
      <script src="/Js/jquery-1.11.1.min.js"></script>
      <script src="/Js/bootstrap.min.js"></script>
      

      <!--[if lt IE 9]><script src="/Js/B2P.Enums.IE8.js"></script><![endif]-->
      <!--[if (gte IE 9)|!(IE)]><!--><script src="/Js/B2P.Enums.js"></script><!--<![endif]-->

      <script src="/Js/B2P.Utility.js"></script>
      <script src="/Js/B2P.FormValidator.js"></script>
      <script src="/Js/B2P.ValidationType.js"></script>

        </div>
    </form>

  </body>
</html>
