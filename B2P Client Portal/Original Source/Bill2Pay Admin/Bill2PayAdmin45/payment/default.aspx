<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Bill2PayAdmin45._default" %>
<%@ Register TagPrefix="tripos" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>

<!DOCTYPE html>

<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html lang="en"> <!--<![endif]-->

  <head id="docHead" runat="server">
	<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>TriPOS Payment Landing Site - Default</title>

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

    <form id="frmDefault" autocomplete="off" runat="server">

 

      <!--// START MIDDLE CONTENT //-->
      <div class="container">

        <div class="row">
          <div class="col-xs-12">
            <h3 class="text-primary">TriPOS Payment Landing Site</h3>
            <hr />
            <br />
          </div>
        </div>

        <div class="row">
          <div class="col-xs-12 col-sm-6">
            <tripos:PaymentStatusMessage id="psmErrorMessage" runat="server" />
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

      <br />
      <br />

    

      <!-- JavaScript -->
      <script src="/Js/jquery-1.11.1.min.js"></script>
      <script src="/Js/bootstrap.min.js"></script>

    </form>

  </body>
</html>