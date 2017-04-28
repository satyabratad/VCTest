<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaymentError.aspx.vb" Inherits="B2P.PaymentLanding.Express.Web._PaymentError" %>
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
	<title>Error Occurred</title>

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

    <form id="frmError" autocomplete="off" runat="server">

      <!--// START LOGO, HEADER AND NAV //-->
        
      <b2p:PaymentHeader id="phError" runat="server" />

      <!--// END LOGO, HEADER AND NAV //-->
        <%--<div class="row" style="background-color:white; padding-bottom:10px;">
         <br />
        <div class="col-sm-12">--%> 

      <!--// START MIDDLE CONTENT //-->
      <div class="container">

        <div class="row">
          <div class="col-xs-12 col-sm-6">
            <h3 class="text-primary">Error Occurred</h3>
            <hr />
            <br />
          </div>
        </div>

        <div class="row">
          <div class="col-xs-12 col-sm-6">
            <b2p:PaymentStatusMessage id="psmError" runat="server" />
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

      <b2p:PaymentFooter id="pfError" runat="server" />

      <!--// END FOOTER CONTENT //-->

      <!-- JavaScript -->      
      <script src="/Js/jquery-1.11.1.min.js"></script>
      <script src="/Js/bootstrap.min.js"></script>

    </form>

  </body>
</html>