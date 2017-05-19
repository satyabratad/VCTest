<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="confirmation.aspx.vb" Inherits="Bill2PayAdmin45.confirmation" %>

<!DOCTYPE html>

<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html lang="en"> <!--<![endif]-->

  <head id="docHead" runat="server">
	<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Payment Confirmation :: Bill2Pay Administration</title>

    <!-- CSS -->    
    <link rel="stylesheet" href="/Css/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="/Css/app.css" type="text/css" />

    <!-- JavaScript -->
    <!--[if lt IE 9]>
        <script src="/Js/html5shiv.min.js"></script>
        <script src="/Js/respond.min.js"></script>
    <![endif]-->
    
    <script type="text/javascript">
        function checkWindow() {
            //if (window.top == window.self) {
                // Top level window
            //} else {
                window.opener.location.replace('/payment/pinpadchip.aspx');
            //}
        }
    </script>
  </head>

  <body onload="checkWindow();">

    <form id="frmConfirmation" autocomplete="off" runat="server">
      <br />
      <div id="pnlButtons">
        <input id="btnPrintMerchant" class="btn btn-primary btn-sm" type="button" value="Print Merchant Copy" onclick="printReceipt('merchant');" />&nbsp;&nbsp;&nbsp;<input id="btnPrintCardholder" class="btn btn-success btn-sm" type="button" value="Print Cardholder Copy" onclick="printReceipt('cardholder');" />
          
        <br />
        <br />
      </div>

      <!--// START MIDDLE CONTENT //-->
      <div id="pnlReceiptContent" class="receipt">

        <!-- START MERCHANT INFO -->
        <div id="pnlMerchantHeaderInfo" class="receipt-merchant-info">
          <asp:Image id="imgClientLogo" ImageUrl="/Img/B2P-Logo4.png" width="150" runat="server" />
          <br />
          <br />
          <asp:Literal id="litMerchantHeaderInfo" runat="server" />
          <br />
          <br />
          <asp:Literal id="litCardAndPaymentType" runat="server" />
          <br />
          <div id="pnlEmvAppId" runat="server">
            <asp:Literal ID="litEmvAppId" runat="server" />
            <br />
          </div>
          <br />
          MERCH LOC CODE : <asp:Literal id="litMechantLocationCode" runat="server" />
        </div>
        <!-- END MERCHANT INFO -->

        <br />

        <!-- START TRANS INFO -->
        <table class="table-trans-info">
          <tr>
            <td>NAME</td>
            <td> : </td>
            <td>
              <asp:Literal id="litCardHolderName" runat="server" />
            </td>
          </tr>
          <tr>
            <td>CONF#</td>
            <td> : </td>
            <td>
              <asp:Literal id="litReferenceNumber" runat="server" />
            </td>
          </tr>
          <tr>
            <td>ACT#</td>
            <td> : </td>
            <td>
              <asp:Literal id="litAccountNumber" runat="server" />
            </td>
          </tr>
          <tr>
            <td>CARD</td>
            <td> : </td>
            <td>
              <asp:Literal id="litCardType" runat="server" />
            </td>
          </tr>
          <tr>
            <td>ENTRY</td>
            <td> : </td>
            <td>
              <asp:Literal id="litEntryMode" runat="server" />
            </td>
          </tr>
        </table>
        <br />
        <table class="table-total-info">
          <tr>
            <td>AMOUNT</td>
            <td class="receipt-subtotal">
              <asp:Literal id="litAmount" runat="server" />
            </td>
          </tr>
          <tr>
            <td>CONV FEE</td>
            <td class="receipt-fee">
              <asp:Literal id="litConvenienceFee" runat="server" />
            </td>
          </tr>
          <tr>
            <td class="receipt-total-separator">
              <asp:Literal id="litPayType" runat="server" /> PURCHASE&nbsp;&nbsp;&nbsp;
            </td>
            <td class="receipt-total receipt-total-separator">
              <asp:Literal id="litTotalAmount" runat="server" />
            </td>
          </tr>
        </table>
        <br />
        <p class="receipt-trans-status">
          <asp:Literal ID="litTransStatus" runat="server" />
        </p>

        <div id="pnlHostResponse" class="host-response-info">

        <div id="pnlApprovalCode" runat="server">
          APPROVAL CODE : <asp:Literal id="litApprovalCode" runat="server" />
          <br />
        </div>

        <div id="pnlHostCodes" runat="server">
          HOST: <asp:Literal id="litHostCode" runat="server" />
          <br />
          TRAN ID: <asp:Literal id="litTransID" runat="server" />
          <br />
        </div>

        </div>
        <br />
        <div id="pnlEmvCrypto" class="receipt-agree-info" runat="server">
          <asp:Literal id="litEmvCrypto" runat="server" />
          <br />
        </div>
        <!-- END TRANS INFO -->

        <!-- START SIGNATURE INFO -->
        <div id="pnlSignatureInfo" runat="server">
          <br />

          <div class="receipt-agree-info">
            I AGREE TO PAY ABOVE TOTAL AMOUNT ACCORDING TO CARD ISSUER AGREEMENT
          </div>

          <br />

          <div class="receipt-signature-not-needed">
            <asp:Literal id="litSignatureNotNeeded" runat="server" />
          </div>
            
          <br />

          <img id="imgSignature" width="236" height="79" alt="Cardholder signature" title="Cardholder signature" runat="server" />

        </div>
        <!-- END SIGNATURE INFO -->

        <!-- START EMV TAGS INFO -->
        <div id="pnlEmvTags" class="emv-tag-list" runat="server">
          <br />
          <asp:Literal id="litEmvTags" runat="server" />
          <br />
        </div>
        <!-- END EMV TAGS INFO -->
            
        <br />

        <p class="receipt-salutation">
          <asp:Literal id="litSalutation" runat="server" />
          <br />
          <br />
          <span id="txtReceiptType"></span>
        </p>
        <asp:HiddenField ID="hidEmailAddress" runat="server" />
      </div>
      <!--// END MIDDLE CONTENT //-->

      <!-- START E-MAIL CONFIRMATION DIALOG -->
      <div id="pnlConfirmationModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" aria-label="E-mail Confirmation Receipt" aria-hidden="true" tabindex="-1">
        <div class="modal-dialog" role="document">
          <div class="modal-content">

            <div class="modal-header">              
              <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
              <h4><span class="fa fa-envelope-o"></span>&nbsp;&nbsp;E-mail Confirmation Receipt</h4>
            </div>

            <div class="modal-body">
              <div class="row" role="form">
                <div class="col-xs-12">
                  Please enter the e-mail address where the confirmation receipt should be sent.
                  <br />
                  <br />
                  <div class="form-group form-group-sm">
                    <label for="txtEmailAddress">Email Address:</label>
                    <input id="txtEmailAddress" type="email" class="form-control input-sm" placeholder="Enter email" />
                  </div>
                </div>
              </div>
            </div>

            <div class="modal-footer">
              <input id="btnCancel" class="btn btn-default btn-sm" type="button" value="Cancel" data-dismiss="modal" onclick="return clearForm();" />
              <input id="btnSendEmail" class="btn btn-primary btn-sm" type="button" value="Send E-mail" onclick="return sendReceipt();" />
            </div>
          </div>
        </div>
      </div>
      <!-- END E-MAIL CONFIRMATION DIALOG -->

      <!-- JavaScript -->
      <script src="/Js/jquery-1.11.1.js"></script>
      <script src="/Js/bootstrap.min.js"></script>
      <script src="/Js/jquery.payment.js"></script>

      <!--[if lt IE 9]><script src="/Js/B2P.Enums.IE8.js"></script><![endif]-->
      <!--[if (gte IE 9)|!(IE)]><!--><script src="/Js/B2P.Enums.js"></script><!--<![endif]-->

      <script src="/Js/B2P.Utility.js"></script>
      <script src="/Js/B2P.FormValidator.js"></script>
      <script src="/Js/B2P.ValidationType.js"></script>

      <script type="text/javascript">
          jQuery(function ($) {
              if (parent !== window) {
                  $("#pnlButtons").css("visibility", "hidden");
              }
              else {
                  $("#pnlButtons").css("visibility", "visible");
              }
          });

          function printReceipt(type) {
              // Get the receipt type
              if (type === "cardholder") {
                  document.getElementById("txtReceiptType").innerHTML = "CARDHOLDER COPY";
              }
              else {
                  document.getElementById("txtReceiptType").innerHTML = "MERCHANT COPY";
              }

              window.print();
          }

          // Sends the receipt
          function sendReceipt() {
             
              var email = $("#hidEmailAddress").val();
              //var validated = validateForm();
              //var email = $("#txtEmailAddress").val();

              // Send the receipt if e-mail is a valid format
              
                  $.ajax({
                      type: "POST",
                      url: "/payment/confirmation.aspx/SendReceipt",
                      contentType: "application/json; charset=utf-8",
                      data: "{'email':'" + email + "','contents':'" + buildReceipt() + "'}",
                      dataType: "json"
                  })
                  .done(function () {
                      //$("#pnlConfirmationModal").modal("hide");
                      $('#txtEmailAddress').val("");
                  })
                  .fail(showError);
              
          }

          function buildReceipt() {
              var contents = null;
              var receiptContent = document.getElementById("pnlReceiptContent").innerHTML;
              var htmlContent = $("<div />").append($(receiptContent));

              // Remove objects from the e-mailed receipt
              htmlContent.find("#imgClientLogo").remove();
              htmlContent.find("#pnlSignatureInfo").remove();
              htmlContent.find("#txtReceiptType").remove();

              contents = '<html><head><title>Payment Receipt</title>';
              contents += '<link rel="stylesheet" href="/Css/app.css" type="text/css" />';

              contents += '</head><body>';
              contents += htmlContent.html();
              contents += '</body></html>';

              return contents;
          }

          // Validate the send e-mail form
          function validateForm() {
              // Create instance of the form validator
              var validator = new FormValidator();
              validator.setErrorMessageHeader("<%=GetGlobalResourceObject("WebResources", "ErrMsgHeader").ToString()%>\n\n");
              validator.setInvalidCssClass("has-error");
              validator.setAlertBoxStatus(false);

              // Add validation items to validator
              validator.addValidationItem(new ValidationItem("txtEmailAddress", fieldTypes.Email, true, "<%=GetGlobalResourceObject("WebResources", "ErrMsgEmailAddress").ToString()%>"));

              return validator.validate();
          }

          // Clear the CC account info
          function clearForm() {
              // Remove any invalid CSS classes
              $('#pnlConfirmationModal').find("*").tooltip("destroy");
              $('#pnlConfirmationModal').find("*").removeClass("has-error tooltip-danger");
              $('#pnlConfirmationModal').find("*").removeAttr("data-original-title title");

              // Clear the form fields
              $('#txtEmailAddress').val("");

              // Hide the error messages
              //$('#pnlStatusMessage').hide();

              return false;
          }

          // Get any error information to display to the end user
          function showError(xhr, status) {
              var results;

              // Build the results msg
              results = "ResponseText: " + xhr.responseText + "\n";
              results += "StatusText: " + xhr.statusText + "\n";
              results += "Status: " + xhr.status;
          }

      </script>

    </form>

  </body>
</html>