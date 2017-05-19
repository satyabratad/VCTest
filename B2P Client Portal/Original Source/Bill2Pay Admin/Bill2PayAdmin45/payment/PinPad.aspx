<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PinPad.aspx.vb" Inherits="Bill2PayAdmin45.PinPad" %>
<%@ Register TagPrefix="tripos" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>
<%@ Register TagPrefix="tripos" TagName="PaymentCreditCard" Src="~/UserControls/CreditCard.ascx" %>
<%@ Register TagPrefix="tripos" TagName="PaymentBankAccount" Src="~/UserControls/BankAccount.ascx" %>


<!DOCTYPE html>

<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html lang="en"> <!--<![endif]-->

  <head id="docHead" runat="server">
	<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Make a Payment :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />

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

    <form id="frmPinPad" autocomplete="off" runat="server">
<div class="top_header_main"></div>
      <custom:menu ID="menu" runat="server" />
      <div class="top_header_shadow"></div>
  
      <div class="content" style="padding-right:10px;">
        <custom:logout ID="logout" runat="server"  />
        <div class="header">Make a Counter Payment</div>
        <div class="header_border"></div>   
     
      <!--// START MIDDLE CONTENT //-->
      <div class="container">

        <br />

        <div class="row">
          <div class="col-xs-12 col-sm-7">

            <tripos:PaymentStatusMessage id="psmErrorMessage" runat="server" />

          </div>
        </div>

        <br />

        <div class="row">
          <div class="col-xs-12">

            <div id="pnlReaderStatus" runat="server">
              <span class="bold">Reader status:</span> <span id="txtReaderStatus" class="bold"></span>.
            </div>

          </div>
        </div>

        <br />
        <br />

        <div class="row">
          <div class="col-xs-12 col-sm-8">

            <div id="pnlPaymentTypes" class="alert alert-info alert-sm" role="alert" runat="server">
              Payment Types Accepted: <asp:Literal id="litCardImages" runat="server" />
            </div>

          </div>
        </div>



        <!-- Billing payment type info here -->
        <div id="pnlPaymentOptions" class="row">
          <div class="col-xs-12 col-sm-8">

            <div id="pnlPaymentCredit" runat="server">
              <div class="panel panel-credit">
                <div class="panel-heading panel-heading-small bold text-center"><asp:Literal ID="litCreditHeading" runat="server" /></div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Amount:</div><div class="col-xs-6 text-right"><asp:Literal id="litCreditCart" runat="server" /></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Conv Fee:</div><div class="col-xs-6 text-right"><strong><asp:Literal id="litCreditFee" runat="server" /></strong></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Total:</div><div class="col-xs-6 text-right"><asp:Literal id="litCreditTotal" runat="server" /></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-12 text-center feeDescription">
                      <br />
                      <asp:Literal id="litCreditFeeDescription" runat="server" />
                      <br />
                    </div>
                  </div>
                </div>
              </div>
            </div> 


            <div id="pnlPaymentDebit" runat="server">
              <div class="panel panel-debit">
                <div class="panel-heading panel-heading-small bold text-center">Debit</div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Amount:</div><div class="col-xs-6 text-right"><asp:Literal id="litDebitCart" runat="server" /></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Conv Fee:</div><div class="col-xs-6 text-right bold"><asp:Literal id="litDebitFee" runat="server" /></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Total:</div><div class="col-xs-6 text-right"><asp:Literal id="litDebitTotal" runat="server" /></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-12 text-center feeDescription">
                      <br />
                      <asp:Literal id="litDebitFeeDescription" runat="server" />
                      <br />
                    </div>
                  </div>
                </div>
              </div>
            </div>


            <div id="pnlPaymentVisaDebit" runat="server">
              <div class="panel panel-visa-debit">
                <div class="panel-heading panel-heading-small bold text-center">VISA Debit</div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Amount:</div><div class="col-xs-6 text-right"><asp:Literal id="litVisaDebitCart" runat="server" /></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Conv Fee:</div><div class="col-xs-6 text-right bold"><asp:Literal id="litVisaDebitFee" runat="server" /></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-6 bold text-left">Total:</div><div class="col-xs-6 text-right"><asp:Literal id="litVisaDebitTotal" runat="server" /></div>
                  </div>
                  <div class="row">
                    <div class="col-xs-12 text-center feeDescription">
                      <br />
                      <asp:Literal id="litVisaDebitFeeDescription" runat="server" />
                      <br />
                    </div>
                  </div>
                </div>
              </div>
            </div>

        <div id="pnlPaymentACH" runat="server">
            <div class="panel panel-visa-debit">
              <div class="panel-heading panel-heading-small bold text-center">Bank Account</div>

              <div class="panel-body">
                <div class="row">
                  <div class="col-xs-6 bold text-left">Amount:</div><div class="col-xs-6 text-right"><asp:Literal id="litACHCart" runat="server" /></div>
                </div>
                <div class="row">
                  <div class="col-xs-6 bold text-left">Conv Fee:</div><div class="col-xs-6 text-right bold"><asp:Literal id="litACHFee" runat="server" /></div>
                </div>
                <div class="row">
                  <div class="col-xs-6 bold text-left">Total:</div><div class="col-xs-6 text-right"><asp:Literal id="litACHTotal" runat="server" /></div>
                </div>                
                <div class="row">
                  <div class="col-xs-12 text-center feeDescription">
                    <br />
                    <asp:Literal id="litACHDescription" runat="server" />
                    <br />
                  </div>
                </div>
              </div>
            </div>
          </div>

        </div>
   </div>
          

        <br />


        <div id="pnlFormContents" class="row" role="form" runat="server">
           <div class="col-xs-12 col-sm-8">
            <br />

            <ul id="paymentTabs" class="nav nav-tabs" role="tablist">
              <li class="paywith text-primary">
                  Other Payment Options:                      
              </li>
                         

              <li id="tabCardSwipe" runat="server">
                <a href="#pnlTabCardSwipe" onclick="clearValidationItems()" role="tab" data-toggle="tab">
                  <span id="imgCreditCard" class="fa fa-credit-card" title="Card swipe"></span> Card Swipe
                </a>
              </li>

              <li id="tabManual" runat="server">
                <a href="#pnlTabManual" role="tab" data-toggle="tab">
                  <span id="imgKeyboard" class="fa fa-keyboard-o" title="Manual entry"></span> Manual Entry
                </a>
              </li>

               <li id="tabAch" runat="server">
                <a href="#pnlTabAch" onclick="clearValidationItems('CC')" role="tab" data-toggle="tab">
                  <span class="fa fa-bank" title="eCheck"></span> eCheck
                </a>
              </li>
            </ul>

            <div class="tab-content no-gutter">
                 <div id="pnlTabCardSwipe" class="tab-pane" runat="server">
                    <div class="text-center">
                      <br />
                      Please click on the <span class="bold">Swipe / Insert Card</span> button to initiate a PIN pad transaction.
                      <br />
                      <br />
                      <br />
                      <asp:Button id="btnSwipeCard" cssclass="btn btn-primary btn-sm bold" text="Swipe / Insert Card" tooltip="Swipe / Insert Card" UseSubmitBehavior="false" runat="server" />
                    </div>
                  </div>
             
             <asp:Panel id="pnlTabManual" cssclass="tab-pane" defaultbutton="btnSubmitCardInfo" runat="server">
                  <div class="col-xs-12 col-sm-10 no-gutter">
                      <br />
                      <tripos:PaymentCreditCard id="pccEnterCreditCardInfo" runat="server" />
                      <br />
                      <div class="text-right">
                        <asp:LinkButton id="btnClearCardInfo" Text="Clear" ToolTip="Clear Fields" runat="server" />&nbsp;&nbsp;
                        <asp:Button id="btnSubmitCardInfo" cssclass="btn btn-primary btn-sm" text="Submit" tooltip="Submit" runat="server" />
                      </div>
                    </div>
            </asp:Panel>

            <asp:Panel id="pnlTabAch" cssclass="tab-pane" defaultbutton="btnSubmitAch" runat="server">
                <br />
                <tripos:PaymentBankAccount id="pbaEnterBankAccountInfo" runat="server" />
                <br />
                <div class="pull-right">
                  <asp:Button id="btnCancelAch" cssclass="btn btn-primary btn-sm" text="<%$ Resources:WebResources, ButtonCancel %>" tooltip="<%$ Resources:WebResources, ButtonCancel %>" runat="server" />
                  <asp:Button id="btnSubmitAch" cssclass="btn btn-primary btn-sm" text="<%$ Resources:WebResources, ButtonSubmit %>" tooltip="<%$ Resources:WebResources, ButtonSubmit %>" runat="server" />
                </div>
              </asp:Panel>
            
            </div>

          </div>
        </div>


        <br />
        <br />

       
      </div>
      <!--// END MIDDLE CONTENT //-->
          </div>
        
      <br />
      <br />

    

      <!-- START PROCESSING DIALOG -->
      <div id="pnlProcessing" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" aria-label="Processing, please wait." aria-hidden="true" tabindex="-1">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-body">
              <div class="text-center">
                <h4>Processing, please wait...</h4>
                <br />
                <br />
                <br />
                <span class="fa fa-spinner fa-4x fa-pulse text-success"></span>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- END PROCESSING DIALOG -->

      <!-- START CVV MODAL DIALOG -->
      <div id="ccInfoModal" class="modal fade" role="dialog" aria-label="<%=GetGlobalResourceObject("WebResources", "CVVModalTitle").ToString%>" aria-hidden="true" tabindex="-1">
        <div class="modal-dialog" role="document">
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
                <img id="imgCVV" class="img-responsive center-block" src="/img/cvv-code-image.jpg" alt="CVV Code Location Examples" title="CVV Code Location Examples" />
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

        <!-- START CHECK HELP MODAL DIALOG -->
      <div class="modal fade" id="achInfoModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
              <h4>
                <asp:Literal id="litCheckImageTitle" text="<%$ Resources:WebResources, CheckHelpModalTitle %>" runat="server" />
              </h4>
            </div>

            <div class="modal-body">
              <p>
                <asp:Literal id="litCheckHelpText" text="<%$ Resources:WebResources, CheckHelpModalDescription %>" runat="server" />
              </p>
              <p>
                <img id="imgCheckHelp" class="img-responsive center-block" src="/img/bank_check.jpg" title="" />
              </p>
            </div>

            <div class="modal-footer">
              <asp:Button id="btnCloseCheckModal"
                          text="<%$ Resources:WebResources, ButtonClose %>"
                          cssclass="btn btn-primary btn-sm"
                          data-dismiss="modal"
                          runat="server" />
            </div>
          </div>
        </div>
      </div>
      <!-- END CHECK HELP MODAL DIALOG -->

      <!-- START DECLINE RECEIPT MODAL DIALOG -->
      <div id="pnlDeclineReceipt" class="modal fade" role="dialog" aria-label="Print Decline Receipt" aria-hidden="true" tabindex="-1">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header modal-header-danger">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
              <h4><span class="fa fa-print"></span>&nbsp;&nbsp;Print Decline Receipt</h4>
            </div>

            <div class="modal-body">
              <iframe id="receiptFrame" src="JavaScript:''" style="width:100%; height:75vh; display:block;"></iframe>
            </div>

            <div class="modal-footer modal-footer-danger">
              <input id="btnCancel" class="btn btn-default btn-sm" type="button" value="Cancel" data-dismiss="modal" />
              <input id="btnPrintReceipt" class="btn btn-danger btn-sm" type="button" value="Print" onclick="return printDeclineReceipt();" />
            </div>
          </div>
        </div>
      </div>
      <!-- END DECLINE RECEIPT MODAL DIALOG -->


        <!-- START ACH FAILED VALIDATION MODAL DIALOG -->
      <asp:Panel id="pnlAchInvalidModal" cssclass="modal fade" tabindex="-1" role="dialog" aria-label="Invalid Bank Account Number" aria-hidden="true" runat="server">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title"><span class="fa fa-question-circle"></span>&nbsp;&nbsp;Invalid Bank Account Number</h4>
            </div>
            <div class="modal-body">
              <asp:Literal id="litAchInvalidMsg" runat="server" />
              <br />
              <br />
            </div>
            <div class="modal-footer">
              <asp:Button id="btnCloseFailedValidationModal"
                          text="<%$ Resources:WebResources, ButtonClose %>"
                          cssclass="btn btn-sm btn-primary btn-sm"
                          tooltip="<%$ Resources:WebResources, ButtonClose %>"
                          data-dismiss="modal"
                          runat="server" />
            </div>
          </div>
        </div>
      </asp:Panel>
      <!-- END CONFIRM DELETE MODAL DIALOG -->


      <asp:HiddenField ID="hdfCollapseState" runat="server" />
      <asp:HiddenField ID="hdfTabName" runat="server" />

      <!-- JavaScript -->
      <script src="/Js/jquery-1.11.1.js"></script>
      <script src="/Js/bootstrap.min.js"></script>
      <script src="/Js/jquery.payment.1.4.4.min.js"></script>

      <!--[if lt IE 9]><script src="/Js/B2P.Enums.IE8.js"></script><![endif]-->
      <!--[if (gte IE 9)|!(IE)]><!--><script src="/Js/B2P.Enums.js"></script><!--<![endif]-->

      <script src="/Js/B2P.Utility.js"></script>
      <script src="/Js/B2P.FormValidator.js"></script>
      <script src="/Js/B2P.ValidationType.js"></script>

      <!-- HERE FOR NOW - MAY WANT TO MOVE TO ANOTHER LOCATION -->
      <script src="/Js/B2P.Emv.Aumentum.js"></script>

      <script type="text/javascript">
          // Initialize a few page controls
          jQuery(function ($) {
              var firstTab = $('a[data-toggle="tab"]:first');
              var tabName = null;
              var button = document.getElementById("btnSwipeCard");
              var attr = $(firstTab).attr('href');

              // Disable the swipe button during service check
              if (button) {
                    document.getElementById("btnSwipeCard").disabled = true;
              }

              // Get the last active tab before postback; make sure the tabs are visible
              if (typeof attr !== typeof undefined && attr !== false) {
                  tabName = $("[id*=hdfTabName]").val() != "" ? $("[id*=hdfTabName]").val() : firstTab.attr("href").replace("#", "");
              }

              $('#paymentTabs a[href="#' + tabName + '"]').tab("show");

              $("#paymentTabs a").click(function () {
                  $("[id*=hdfTabName]").val($(this).attr("href").replace("#", ""));
              });

              // Use Swipe's payment formatters
              $("#txtCreditCardNumber").payment("formatCardNumber");
              $("#txtExpireDate").payment("formatCardExpiry");
              $("#txtCCV").payment("formatCardCVC");

              // May need to reset the payment type panels
              resetPanelHeight("#pnlPaymentOptions", ".feeDescription");

              $("body").resize(null, resetPanelHeight("#pnlPaymentOptions", ".feeDescription"));

              // Set the tooltip
              $('[data-toggle="tooltip"]').tooltip();

              $.support.cors = true;

              // Make sure the gateway is available
              createRequestHeaders("http://127.0.0.1:8880/api/v1/status/host", "GET", "", checkHostStatus);
          });

          // Resets the panel heights to that of the largest panel
          function resetPanelHeight(selector, item) {
              var heights = null;
              var maxHeight = null;
              var panels = $(selector).find(item);

              // Get the payment type panel heights
              heights = panels.map(function () {
                  return $(this).height();
              }).get();

              // Get the panel with the max height
              maxHeight = Math.max.apply(null, heights);

              // Set the panels to the same height
              panels.height(maxHeight);
          }

          // Start the payment process
          function startProcessing() {
              // Hide any previous messages
              //$("#pnlStatusMessage").hide();

              // Let the user know something's happening
              $("#pnlProcessing").modal("show");

              // To view how the processing modal dialog looks for testing
              //setTimeout(function () { }, 5000);

              createRequestHeaders(
                  "http://127.0.0.1:8880/api/v1/configuration/lanes/serial", "GET", "",
                  function (data) {
                      getCardReaderLane(data);
                  }
              );

              //return false;
          }
          
          // IE11 can't print from dynamic frames like:
          // window.frames[0].print();
          function printDeclineReceipt() {
              var iframe = document.getElementById("receiptFrame");
              var execAllowed = iframe.contentWindow.document.execCommand("print", false, null);

              // For FF
              if (!execAllowed) {
                  iframe.contentWindow.print();
              }
          }


         function validateForm(paymentType) {
              var doc = document;
              <% =Bill2PayAdmin45.Utility.BuildAllowedCardsPattern(B2P.Integration.TriPOS.BLL.SessionManager.Cart)%>              
              var item = new ValidationItem();
              
              // Create instance of the form validator
              var validator = new FormValidator();
              validator.setErrorMessageHeader("<%=GetGlobalResourceObject("WebResources", "ErrMsgHeader").ToString()%>\n\n")
              validator.setInvalidCssClass("has-error");
              validator.setAlertBoxStatus(false);

              // Add validation items to validator
              switch (paymentType) {
                  case "CC":
                      var cardType = $.payment.cardType(doc.getElementById("txtCreditCardNumber").value);
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
                          errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgCreditCardExpiry").ToString()%>",
                          isValid: (!!/^\d{2} \/ \d{4}$/.test(doc.getElementById("txtExpireDate").value) && $.payment.validateCardExpiry($.payment.cardExpiryVal(doc.getElementById("txtExpireDate").value)))
                      });

                      validator.addValidationItem(item = {
                          field: "txtCCV",
                          styleParent: true,
                          errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgCreditCardCVC").ToString()%>",
                          isValid: ($.payment.validateCardCVC(doc.getElementById("txtCCV").value, cardType))
                      });

                      validator.addValidationItem(new ValidationItem("txtCreditFirstName", fieldTypes.Name, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgFirstName").ToString()%>"));
                      validator.addValidationItem(new ValidationItem("txtCreditLastName", fieldTypes.Name, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgLastName").ToString()%>"));

                      break;

                  case "ACH":
                      validator.addValidationItem(new ValidationItem("txtAchFirstName", fieldTypes.Name, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgFirstName").ToString()%>"));
                      validator.addValidationItem(new ValidationItem("txtAchLastName", fieldTypes.Name, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgLastName").ToString()%>"));
                      validator.addValidationItem(new ValidationItem("ddlBankAccountType", fieldTypes.NonEmptyField, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRequired").ToString()%>"));
                      validator.addValidationItem(new ValidationItem("txtBankRoutingNumber", fieldTypes.RoutingNumber, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgRoutingNumber").ToString()%>"));
                      validator.addValidationItem(new ValidationItem("ckNACHA", fieldTypes.CheckboxChecked, false, "<%=GetGlobalResourceObject("WebResources", "ErrMsgNACHA").ToString()%>"));

                      validator.addValidationItem(item = {
                          field: "txtBankAccountNumber",
                          styleParent: true,
                          errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgBankAccount").ToString()%>",
                          isValid: (!!/^\d{1,17}$/.test(doc.getElementById("txtBankAccountNumber").value) && !!(doc.getElementById("txtBankAccountNumber").value === doc.getElementById("txtBankAccountNumber2").value))
                      });

                      validator.addValidationItem(item = {
                          field: "txtBankAccountNumber2",
                          styleParent: true,
                          errorMessage: "<%=GetGlobalResourceObject("WebResources", "ErrMsgBankAccount").ToString()%>",
                          isValid: (!!/^\d{1,17}$/.test(doc.getElementById("txtBankAccountNumber2").value) && !!(doc.getElementById("txtBankAccountNumber").value === doc.getElementById("txtBankAccountNumber2").value))
                      });

                      break;
              }

              return validator.validate();
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
                      $("#pnlTabManual").find("*").not("#lnkCardProcessInfo, #lnkCvvInfo").tooltip("destroy");                       // except the card process tooltip
                      $("#pnlTabManual").find("*").not("#lnkCardProcessInfo, #lnkCvvInfo").removeAttr("data-original-title title");  // except the card process tooltip
                      $("#pnlTabManual").find("*").removeClass("has-error tooltip-danger");
                      $("#txtCreditCardNumber").removeClass("amex discover mastercard visa identified");

                      // Clear the form fields
                      $("#txtCreditCardNumber").val("");
                      $("#txtExpireDate").val("");
                      $("#txtCCV").val("");
                      $("#txtCreditFirstName").val("");
                      $("#txtCreditLastName").val("");

                    
                      break;
              }
          }

        

      </script>

    </form>
  <div class="clear"></div> 
    <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
    </div>
  </body>
</html>