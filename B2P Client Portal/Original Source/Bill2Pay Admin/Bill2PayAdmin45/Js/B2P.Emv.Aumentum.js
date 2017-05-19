// Get the EMV service request headers
function createRequestHeaders(requestUrl, requestMethod, requestData, callbackFn) {
    return $.ajax({
        type: "POST",
        url: "/payment/PinPad.aspx/CreateRequestHeaders",
        contentType: "application/json; charset=utf-8",
        data: "{'requestUrl':'" + requestUrl + "','requestMethod':'" + requestMethod + "','requestData':'" + requestData + "'}",
        dataType: "json"
    })
    .done(callbackFn)
    .fail(showError);
}

// Check the EMV service host status
function checkHostStatus(headers) {
    var requestHeaders = JSON.parse(headers.d);
    var hostRequest = {
        'Method': 'GET',
        'Url': 'http://127.0.0.1:8880/api/v1/status/host',
        'Headers': '' + headers.d + '',
        'ContentType': 'application/json' // ; charset=utf-8
    };

    $.ajax({
        type: "GET",
        crossDomain: true,
        url: "https://local.bill2pay.com:9231/api/host/getstatus",
        contentType: "application/json",
        data: hostRequest,
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
    var tpStatus = JSON.parse(response);
    var hostMsg;

    if (tpStatus.hostStatus != "") {
        switch (tpStatus.hostStatus) {
            case "Online":
                hostMsg = "Please use the PIN pad device or manually enter the payment details";

                if (document.getElementById("btnSwipeCard")) {
                    document.getElementById("btnSwipeCard").disabled = false;
                    $("#btnSwipeCard").removeClass("btn-danger").addClass("btn-primary");
                    $("#btnSwipeCard").css("visibility", "visible");
                }

                $("#txtReaderStatus").removeClass("text-danger").addClass("text-success");
                $("#txtReaderStatus").html(hostMsg);

                break;

            case "HostUnreachable":
            case "Offline":
            default:
                hostMsg = "Host is offline or unreachable. Please manually enter the payment details";

                if (document.getElementById("btnSwipeCard")) {
                    document.getElementById("btnSwipeCard").disabled = true;
                    $("#btnSwipeCard").removeClass("btn-primary").addClass("btn-danger");
                    $("#btnSwipeCard").css("visibility", "hidden");
                }

                $("#txtReaderStatus").removeClass("text-success").addClass("text-danger");
                $("#txtReaderStatus").html(hostMsg);

                break;
        }
    }
}

// Get the card reader lane
function getCardReaderLane(headers) {
    var requestHeaders = JSON.parse(headers.d);
    var hostRequest = {
        'Method': 'GET',
        'Url': 'http://127.0.0.1:8880/api/v1/configuration/lanes/serial',
        'Headers': '' + headers.d + '',
        'ContentType': 'application/json' // ; charset=utf-8
    };
    var lane = null;

    $.ajax({
        type: "GET",
        crossDomain: true,
        url: "https://local.bill2pay.com:9231/api/lane/getlane",
        contentType: "application/json",
        data: hostRequest,
        dataType: "json"
    })
    .done(function (laneResponse) {
        var tpLane = JSON.parse(laneResponse);

        if (!tpLane._hasErrors) {
            lane = tpLane.serialLanes[0].laneId;

            // Get the headers
            createRequestHeaders(
                "http://127.0.0.1:8880/api/v1/status/lane/" + lane, "GET", "",
                function (data) {
                    checkLaneStatus(data, lane);
                }
            );
        }
        else {
            $("#pnlProcessing").modal("hide");
            buildErrorMessage(tpLane._errors);
        }
    })
    .fail(showError);
}

// Get the card reader lane status
function checkLaneStatus(headers, lane) {
    var requestHeaders = JSON.parse(headers.d);
    var hostRequest = {
        'Method': 'GET',
        'Url': 'http://127.0.0.1:8880/api/v1/status/lane/' + lane,
        'Headers': '' + headers.d + '',
        'ContentType': 'application/json' // ; charset=utf-8
    };

    $.ajax({
        type: "GET",
        crossDomain: true,
        url: "https://local.bill2pay.com:9231/api/lane/getlanestatus",
        contentType: "application/json",
        data: hostRequest,
        dataType: "json"
    })
    .done(function (laneStatusResponse) {
        var tpLaneStatus = JSON.parse(laneStatusResponse);

        if (!tpLaneStatus._hasErrors) {
            if (tpLaneStatus.laneStatus == "NotInUse") {
                getPaymentRequestInfo(lane);
            }
            else {
                // Display lane in use message
                showLaneStatus(tpLaneStatus.laneStatus);
            }
        }
        else {
            $("#pnlProcessing").modal("hide");
            buildErrorMessage(tpLaneStatus._errors);
        }
    })
    .fail(showError);
}

// Get the payment request info
function getPaymentRequestInfo(lane) {
    $.ajax({
        type: "POST",
        url: "/payment/PinPad.aspx/GetSaleRequest",
        contentType: "application/json; charset=utf-8",
        data: "{'lane':" + lane + "}",
        dataType: "json"
    })
    .done(function (transInfo) {
        createRequestHeaders(
            "http://127.0.0.1:8880/api/v1/sale", "POST", transInfo.d,
            function (data) {
                sendPaymentRequest(data, transInfo);
            }
        );
    })
    .fail(showError);
}

// Send the payment info to the EMV service
function sendPaymentRequest(headers, transRequest) {
    var requestHeaders = JSON.parse(headers.d);
    var hostRequest = {
        'Method': 'POST',
        'Url': 'http://127.0.0.1:8880/api/v1/sale',
        'Headers': '' + headers.d + '',
        'ContentType': 'application/json', // ; charset=utf-8
        'Data': '' + transRequest.d + ''
    };

    // Send to EMV auth/sale service
    $.ajax({
        type: "POST",
        crossDomain: true,
        url: "https://local.bill2pay.com:9231/api/sale/postsale",
        contentType: "application/json",
        data: JSON.stringify(hostRequest),
        dataType: "json"
    })
    .done(function (transResponse) {
        var tpTrans = JSON.parse(transResponse);

        processPaymentResults(tpTrans);
    })
    .fail(showError);
}

// Process the EMV payment call results
function processPaymentResults(responseData) {
    $.ajax({
        type: "POST",
        url: "/payment/PinPad.aspx/ProcessSaleResponse",
        contentType: "application/json; charset=utf-8",
        data: "{'response':" + JSON.stringify(JSON.stringify(responseData)) + "}",
        dataType: "json"
    })
    .done(function (processResponse) {
        var status = JSON.parse(processResponse.d);

        // See if there is a reversal request
        if (status.hasCancel) {
            createRequestHeaders(
                status.cancelItem.link.href, status.cancelItem.link.method, JSON.stringify(status.cancelItem.request),
                function (data) {
                    cancelTransaction(data, status.cancelItem.link, status.cancelItem.request, status.cancelItem.reason);
                }
            );
        }

        showPaymentInfo(processResponse, status.isEmv);
    })
    .fail(showError);
}

// Reverses an EMV card transaction
function cancelTransaction(headers, cancelLink, cancelRequest, cancelReason) {
    var requestHeaders = JSON.parse(headers.d);
    var hostRequest = {
        'Method': '' + cancelLink.method + '',
        'Url': '' + cancelLink.href + '',
        'Headers': '' + headers.d + '',
        'ContentType': 'application/json', // ; charset=utf-8
        'Data': '' + JSON.stringify(cancelRequest) + ''
    };

    // Send to EMV reversal service
    $.ajax({
        type: "POST",
        crossDomain: true,
        url: "https://local.bill2pay.com:9231/api/cancel/postcancelsale",
        contentType: "application/json",
        data: JSON.stringify(hostRequest),
        dataType: "json"
    })
    .done(function (cancelResponse) {
        var tpCancel = JSON.parse(cancelResponse);

        if (!tpCancel._hasErrors) {
            //processCancelResults(tpCancel, cancelRequest.laneId, cancelLink.href);
            processCancelResults(cancelResponse, cancelRequest.laneId, cancelLink.href, cancelReason);
        }
        else {
            $("#pnlProcessing").modal("hide");
            buildErrorMessage(tpCancel._errors);
        }
    })
    .fail(showError);
}

// Process the EMV void call results
function processCancelResults(responseData, lane, link, reason) {
    var tpCancel = JSON.parse(responseData);

    $.ajax({
        type: "POST",
        url: "/payment/PinPad.aspx/ProcessReversalResults",
        contentType: "application/json; charset=utf-8",
        //data: "{'results':" + JSON.stringify(JSON.stringify(responseData)) + ",'lane':" + lane + ",'status':'REVERSED','reason':'Payment card types do not match.','voidLink':'" + link + "'}",
        data: "{'results':" + JSON.stringify(JSON.stringify(tpCancel)) + ",'lane':" + lane + ",'status':'REVERSED','reason':'" + reason + "','voidLink':'" + link + "'}",
        dataType: "json"
    })
    .done()  // Not sure we need to do anything here???
    .fail(showError);
}

// Display the lane status info to the user
// Just when the lane is unavailable
function showLaneStatus(statusMsg) {
    var statusMeaning = "";

    $("#pnlProcessing").modal("hide");

    switch (statusMsg) {
        case "Host":
            statusMeaning = "Please wait, the PIN pad is sending or receiving data.";
            break;

        case "Initializing":
            statusMeaning = "Please wait, the PIN pad is currently initializing.";
            break;

        case "PinPad":
            statusMeaning = "Please wait, the PIN pad is currently in use.";
            break;

        case "Pipeline":
            statusMeaning = "Please wait, a request is currently processing.";
            break;

        case "ShuttingDown":
            statusMeaning = "The gateway service is currently shutting down and the lane is not able to process new transactions.";
            break;

        default:
            statusMeaning = "Unknown PIN pad status issue.";
            break;
    }

    // Set the alert panel properties
    $("#pnlStatusMessage").removeClass().addClass("alert alert-danger alert-sm");
    $("#imgStatusMsgIcon").removeClass().addClass("fa fa-exclamation-circle fa-2x status-msg-icon");

    // Enable the buttons for another go round
    document.getElementById("btnSwipeCard").disabled = false;
    document.getElementById("btnSubmitCardInfo").disabled = false;

    // Set the alert text
    $("#txtStatusMsg").html(statusMeaning);
}

// Display the payment results info back to the user
function showPaymentInfo(processMsg, isEmv) {
    var msg = JSON.parse(processMsg.d);
    var html = "";
    var parsedMsg = msg.status.split(/(?=[A-Z])/).join(" ");

    $("#pnlProcessing").modal("hide");

    switch (msg.status) {
        case "Approved":
        case "PartialApproval":
            // Set the alert panel properties
            $("#pnlStatusMessage").removeClass().addClass("alert alert-success alert-sm");
            $("#imgStatusMsgIcon").removeClass().addClass("fa fa-check-circle-o fa-2x status-msg-icon");

            // Redirect approved transactions
            if (msg.status == "Approved") {
                // similar behavior as an HTTP redirect
                //window.location.replace("/Confirmation.aspx");
                window.open("/payment/confirmation.aspx", "_blank")
            }

            break;

        case "AuthorizationFailed":
        case "BalanceNotAvailable":
        case "Cancelled":
        case "CommunicationError":
        case "Decline":
        case "Declined":
        case "Duplicate":
        case "DuplicateApproved":
        case "Error":
        case "ExpiredCard":
        case "FeePaymentFailed":
        case "HostError":
        case "InvalidAccount":
        case "InvalidData":
        case "InvalidRequest":
        case "NotAllowed":
        case "NotDefined":
        case "OutOfBalance":
        case "PaymentTypesDoNotMatch":
        case "PaymentTypeNotAccepted":
        case "PickUpCard":
        case "ReferralCallIssuer":
        default:
            // Set the alert panel properties
            $("#pnlStatusMessage").removeClass().addClass("alert alert-danger alert-sm");
            $("#imgStatusMsgIcon").removeClass().addClass("fa fa-exclamation-circle fa-2x status-msg-icon");

            // Enable the buttons to try again
            document.getElementById("btnSwipeCard").disabled = false;
            document.getElementById("btnSubmitCardInfo").disabled = false;

            // Build a modal receipt link for EMV declined transactions
            if (isEmv && (msg.status === "Decline" || msg.status === "Declined")) {
                html = '. <a href="#" class="alert-link" data-toggle="modal" data-target="#pnlDeclineReceipt">View Decline Receipt.</a>';

                // Set the modal frame's URL
                document.getElementById("receiptFrame").src = "/payment/Confirmation.aspx";
            }

            break;
    }

    // Set the alert text
    if (msg.status === "PaymentTypesDoNotMatch" || msg.status === "PaymentTypeNotAccepted" || msg.status === "Error") {
        $("#txtStatusMsg").html((msg.statusMessage !== "" ? msg.statusMessage : msg.status) + html);
    }
    else {
        $("#txtStatusMsg").html((msg.status !== parsedMsg ? parsedMsg : msg.status) + html);
    }
}

// Build an error message from the EMV auth/sale/void response errors array
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

    // Enable the buttons for another go round
    document.getElementById("btnSwipeCard").disabled = false;
    document.getElementById("btnSubmitCardInfo").disabled = false;
}

// Get any error information to display to the end user
function showError(xhr, status) {
    var results;

    $("#pnlProcessing").modal("hide");

    // Build the results msg
    //results = "ResponseText: " + xhr.responseText + "\n";
    //results += "StatusText: " + xhr.statusText + "\n";
    //results += "Status: " + xhr.status;

    // Set the alert panel properties
    $("#pnlStatusMessage").removeClass().addClass("alert alert-danger alert-sm");
    $("#imgStatusMsgIcon").removeClass().addClass("fa fa-exclamation-circle fa-2x status-msg-icon");
    //$("#txtStatusMsg").html(results);
    $("#txtStatusMsg").html("An unexpected error has occurred.");

    // Add function name to params
    //$.ajax({
    //    type: "POST",
    //    url: "/payment/PinPad.aspx/LogAjaxError",
    //    contentType: "application/json; charset=utf-8",
    //    data: "{'functionName':'" + fn + "','xhr':'" + xhr + "'}",
    //    dataType: "json"
    //})
    //.done()   // Not sure we need to do anything here???
    //.fail();  // Not sure we need to do anything here???

    // Enable the buttons for another go round
    document.getElementById("btnSwipeCard").disabled = false;
    document.getElementById("btnSubmitCardInfo").disabled = false;
}