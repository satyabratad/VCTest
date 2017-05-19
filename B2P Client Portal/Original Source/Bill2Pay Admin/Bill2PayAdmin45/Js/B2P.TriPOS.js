// Get the TriPOS service request headers
function getRequestHeaders(callbackFn) {
    return $.ajax({
        type: "POST",
        url: "/payment/PinPad.aspx/GetRequestHeaders",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json"
    })
    .done(callbackFn)
    .fail(showError);
}

// Check the TriPOS service host status
function checkHostStatus(headers) {
    var requestHeaders = JSON.parse(headers.d);

    $.ajax({
        type: "GET",
        url: "http://localhost:8080/api/v1/status/host",
        headers: requestHeaders,
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json"
    })
    .done(function (statusResponse) {
        if (!statusResponse._hasErrors) {
            updateCardReaderStatus(statusResponse);
        }
        else {
            buildErrorMessage(statusResponse._errors);
        }
    })
    .fail(showError);
}

// Update the card reader status info
function updateCardReaderStatus(response) {
    var hostMsg;

    if (response.hostStatus != "") {
        switch (response.hostStatus) {
            case "Online":
                hostMsg = "Select a payment method or manually enter the payment details";

                document.getElementById("btnSwipeCredit").disabled = false;
                $("#btnSwipeCredit").removeClass("btn-danger").addClass("btn-success");
                $("#btnSwipeCredit").css("visibility", "visible");

                document.getElementById("btnSwipeDebit").disabled = false;
                $("#btnSwipeDebit").removeClass("btn-danger").addClass("btn-success");
                $("#btnSwipeDebit").css("visibility", "visible");

                $("#txtReaderStatus").removeClass("text-danger").addClass("text-success");
                $("#txtReaderStatus").html(hostMsg);

                break;

            case "HostUnreachable":
            case "Offline":
            default:
                hostMsg = "Host is offline or unreachable. Please use the form below to submit the transaction";

                document.getElementById("btnSwipeCredit").disabled = true;
                $("#btnSwipeCredit").removeClass("btn-success").addClass("btn-danger");
                $("#btnSwipeCredit").css("visibility", "hidden");

                document.getElementById("btnSwipeDebit").disabled = true;
                $("#btnSwipeDebit").removeClass("btn-success").addClass("btn-danger");
                $("#btnSwipeDebit").css("visibility", "hidden");

                $("#txtReaderStatus").removeClass("text-success").addClass("text-danger");
                $("#txtReaderStatus").html(hostMsg);

                break;
        }
    }
}

// Start the payment process
function startProcessing(paymentType) {

    // Let the user know something's happening
    $("#pnlProcessing").modal("show");

    // To view how the processing modal dialog looks for testing
    //setTimeout(function () { }, 5000);

    getRequestHeaders(function (data) {
        getCardReaderLane(paymentType, data);
    });

    //return false;
}

// Get the card reader lane
function getCardReaderLane(paymentType, headers) {
    var requestHeaders = JSON.parse(headers.d);
    var lane = null;

    $.ajax({
        type: "GET",
        url: "http://localhost:8080/api/v1/configuration/lanes/serial",
        headers: requestHeaders,
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json"
    })
    .done(function (laneResponse) {
        if (!laneResponse._hasErrors) {
            lane = laneResponse.serialLanes[0].laneId;

            getPaymentRequestInfo(paymentType, lane);
        }
        else {
            $("#pnlProcessing").modal("hide");
            buildErrorMessage(laneResponse._errors);
        }
    })
    .fail(showError);
}

// Get the payment request info
function getPaymentRequestInfo(paymentType, lane) {
    $.ajax({
        type: "POST",
        url: (paymentType == "Credit" ? "/payment/PinPad.aspx/GetAuthRequest" : "/payment/PinPad.aspx/GetSaleRequest"),
        contentType: "application/json; charset=utf-8",
        //data: {},
        data: "{'lane':" + lane + "}",
        dataType: "json"
    })
    .done(function (transInfo) {
        getRequestHeaders(function (data) {
            sendPaymentRequest(data, transInfo, paymentType);
        });
    })
    .fail(showError);
}

// Send the payment info to the TriPOS service
function sendPaymentRequest(headers, transRequest, paymentType) {
    var requestHeaders = JSON.parse(headers.d);

    // View request data for debugging --> remove in prod
    $("#txtPayRequest").val(transRequest.d);

    // Send to TriPOS auth/sale service
    $.ajax({
        type: "POST",
        url: (paymentType == "Credit" ? "http://localhost:8080/api/v1/authorization" : "http://localhost:8080/api/v1/sale"),
        headers: requestHeaders,
        contentType: "application/json; charset=utf-8",
        data: transRequest.d,
        dataType: "json"
    })
    .done(function (transResponse) {
        if (!transResponse._hasErrors) {
            processPaymentResults(transResponse, paymentType);
        }
        else {
            $("#pnlProcessing").modal("hide");
            buildErrorMessage(transResponse._errors);
        }
    })
    .fail(showError);
}

// Process the TriPOS payment call results
function processPaymentResults(responseData, paymentType) {
    // View response data for debugging --> remove in prod
    $("#txtPayResponse").html(JSON.stringify(responseData));

    $.ajax({
        type: "POST",
        url: "/payment/PinPad.aspx/ProcessPaymentResults",
        contentType: "application/json; charset=utf-8",
        data: "{'paymentType':'" + paymentType + "','results':" + JSON.stringify(JSON.stringify(responseData)) + "}",
        dataType: "json"
    })
    .done(function (processResponse) {
        var status = JSON.parse(processResponse.d);

        //if (paymentType == "Credit") {
        //    getRequestHeaders(function (data) {
        //        getSignature(data, lane);
        //    });
        //}
        //else {
        //    // See if there is a void request -- when card mismatch happens with debit selection
        //    if (status.hasVoid) {
        //        getRequestHeaders(function (data) {
        //            voidTransaction(data, status.voidItem.link, status.voidItem.request);
        //        });
        //    }
        //}

        // See if there is a void request -- when card mismatch happens with debit selection
        if (status.hasVoid) {
            getRequestHeaders(function (data) {
                voidTransaction(data, status.voidItem.link, status.voidItem.request);
            });
        }

        showPaymentInfo(processResponse);
    })
    .fail(showError);
}

//function getSignature(headers, laneId, token, batchId) {
//    var requestHeaders = JSON.parse(headers.d);

//    // Send to TriPOS void service
//    $.ajax({
//        type: "GET",
//        url: "http://localhost:8080/api/v1/signature/" + laneId,
//        headers: requestHeaders,
//        contentType: "application/json; charset=utf-8",
//        data: {},
//        dataType: "json"
//    })
//    .done(function (signatureResponse) {
//        if (!signatureResponse._hasErrors) {
//            processSignatureResults(signatureResponse, token, batchId);
//        }
//        else {
//            $("#pnlProcessing").modal("hide");
//            buildErrorMessage(transResponse._errors);
//        }
//    })
//    .fail(showError);


//}

// Voids a card mismatch transaction
function voidTransaction(headers, voidLink, voidRequest) {
    var requestHeaders = JSON.parse(headers.d);

    // Send to TriPOS void service
    $.ajax({
        type: voidLink.method,
        url: voidLink.href,
        headers: requestHeaders,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(voidRequest),
        dataType: "json"
    })
    .done(function (voidResponse) {
        if (!voidResponse._hasErrors) {
            processVoidResults(voidResponse, voidRequest.laneId, voidLink.href);
        }
        else {
            $("#pnlProcessing").modal("hide");
            buildErrorMessage(transResponse._errors);
        }
    })
    .fail(showError);
}

// Process the TriPOS void call results
function processVoidResults(responseData, lane, link) {
    $("#txtPayResponse").html(JSON.stringify(responseData));

    $.ajax({
        type: "POST",
        url: "/payment/PinPad.aspx/ProcessVoidResults",
        contentType: "application/json; charset=utf-8",
        data: "{'results':" + JSON.stringify(JSON.stringify(responseData)) + ",'lane':" + lane + ",'status':'DECLINED','reason':'Payment card types do not match.','voidLink':'" + link + "'}",
        dataType: "json"
    })
    .done()  // Not sure we need to do anything here???
    .fail(showError);
}

// Display the payment results info back to the user
function showPaymentInfo(processMsg) {
    var msg = JSON.parse(processMsg.d);

    $("#pnlProcessing").modal("hide");

    switch (msg.statusMessage) {
        case "Approved":
        case "PartialApproval":
            // Set the alert panel properties
            $("#pnlStatusMessage").removeClass().addClass("alert alert-success alert-sm");
            $("#imgStatusMsgIcon").removeClass().addClass("fa fa-check-circle-o fa-2x status-msg-icon");

            break;

        case "Decline":
        case "ExpiredCard":
        case "DuplicateApproved":
        case "Duplicate":
        case "PickUpCard":
        case "ReferralCallIssuer":
        case "BalanceNotAvailable":
        case "NotDefined":
        case "InvalidData":
        case "InvalidAccount":
        case "InvalidRequest":
        case "AuthorizationFailed":
        case "NotAllowed":
        case "OutOfBalance":
        case "CommunicationError":
        case "HostError":
        case "Error":
        default:
            // Set the alert panel properties
            $("#pnlStatusMessage").removeClass().addClass("alert alert-danger alert-sm");
            $("#imgStatusMsgIcon").removeClass().addClass("fa fa-exclamation-circle fa-2x status-msg-icon");

            break;
    }

    // Set the alert text
    $("#txtStatusMsg").html(msg.statusMessage.split(/(?=[A-Z])/).join(" "));

    if (msg.statusMessage == "Approved") {
        /**************************************************/
        // To view how the alert panel looks for testing
        // Remove this section for production
        /**************************************************/
        setTimeout(function () { }, 5000);

        var answer = confirm("Go to confirmation page?");

        if (answer) {
            // similar behavior as an HTTP redirect
            window.location.replace("/Confirmation.aspx");
        }
        /**************************************************/

        // similar behavior as an HTTP redirect
        //window.location.replace("/Confirmation.aspx");
    }
}

// Build an error message from the TriPOS auth/sale/void response errors array
function buildErrorMessage(errors) {
    var errMsg = "";

    // Loop through the errors
    for (var i = 0; i < errors.length; i++) {
        if (errors[i].userMessage !== "") {
            errMsg += errors[i].userMessage;
        }
        else {
            errMsg += errors[i].developerMessage + "  ";
        }
    }

    // Set the alert panel properties
    $("#pnlStatusMessage").removeClass().addClass("alert alert-danger");
    $("#imgStatusMsgIcon").removeClass().addClass("fa fa-exclamation-circle fa-2x status-msg-icon");
    $("#txtStatusMsg").html(errMsg);
}

// Get any error information to display to the end user
function showError(xhr, status) {
    var results;

    $("#pnlProcessing").modal("hide");

    // Build the results msg
    results = "ResponseText: " + xhr.responseText + "\n";
    results += "StatusText: " + xhr.statusText + "\n";
    results += "Status: " + xhr.status;

    // Set the alert panel properties
    $("#pnlStatusMessage").removeClass().addClass("alert alert-danger alert-sm");
    $("#imgStatusMsgIcon").removeClass().addClass("fa fa-exclamation-circle fa-2x status-msg-icon");
    $("#txtStatusMsg").html(results);
}