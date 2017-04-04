<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HttpError.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web.HttpError" %>
<%@ Register TagPrefix="b2p" TagName="PaymentHeader" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="b2p" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
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
	<title>HTTP Error Information</title>

    <!-- CSS -->    
    <link rel="stylesheet" href="/Css/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="/Css/app.css" type="text/css" />

    <!-- JavaScript -->
    <!--[if lt IE 9]>
        <script src="/Js/html5shiv.min.js"></script>
        <script src="/Js/respond.min.js"></script>
    <![endif]-->
  </head>

  <body>

    <form id="frmHttpError" autocomplete="off" runat="server">

      <!--// START LOGO, HEADER AND NAV //-->

      <b2p:PaymentHeader id="phHttpError" runat="server" />

      <!--// END LOGO, HEADER AND NAV //-->
       

      <!--// START MIDDLE CONTENT //-->
      <div class="container">

        <div class="row">
          <div class="col-xs-12 col-sm-6">
            <h3 class="text-primary">Oops! An error occurred</h3>
            <hr />
            <br />
          </div>
        </div>

        <div class="row">
          <div class="col-xs-12 col-sm-6">
            <b2p:PaymentStatusMessage id="psmHttpError" runat="server" />
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
      <!--// END MIDDLE CONTENT //-->

      <!--// START FOOTER CONTENT //-->

      <b2p:PaymentFooter id="pfHttpError" runat="server" />

      <!--// END FOOTER CONTENT //-->

      <!-- JavaScript -->
      <script src="/Js/jquery-1.11.1.min.js"></script>
      <script src="/Js/bootstrap.min.js"></script>

    </form>

  </body>
</html>