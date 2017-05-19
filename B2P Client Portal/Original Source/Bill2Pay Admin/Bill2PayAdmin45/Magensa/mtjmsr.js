
function SetCardData(CardData) {

    //document.form1.CardData.value = CardData;

}
function SetCardDataMsk(CardDataMsk) {
    //document.form1.tbCreditCardNumber.value = CardDataMsk;

}
function SetPAN(pan) {

    var elem = document.getElementById("tbCreditCardNumber");
    elem.value = pan.substring(0, 6) + "******" + pan.substr(pan.length - 4, 4); 
    ShowAsDisabled(elem);

}
function SetFirstName(firstName) {

    var elem = document.getElementById("tbFirstName");
    elem.value = firstName;

    if (firstName != '') {
        ShowAsDisabled(elem);
    }

}
function SetLastName(lastName) {

    var elem = document.getElementById("tbLastName");
    elem.value = lastName;

    if (lastName != '') {

        ShowAsDisabled(elem);
    }
}


function SetMonth(month) {
    //document.getElementById("ddlMonth2").value = month;
    //alert(month);
    //var combo = $find("<%=ddlMonth %>");
    var combo = document.getElementById("ddlMonth2");
    combo.value = month;

    //combo.findItemByText("09").select();
    //combo.findItemByValue("09").select();
    //combo.selectedIndex = '09';

    //document.getElementById("ddlMonth").value = month;
    //document.getElementById("ddlMonth").selectedindex = month;


    //alert(combo.value);
    //var item = combo.findItemByValue("09");
    //var item = combo.findItemByValue('09');
    //item.select();

    //combo.selectedIndex = "09";
    //alert(item.value);

    //var item = combo.findItemByText(month);
    //if (item) {
    //    item.select();
    //}

    combo.disabled = true;
    //ShowAsDisabled(combo);

}


function SetYear(year) {
    var elem = document.getElementById("ddlYear2");
    elem.value = "20" + year;
    //ShowAsDisabled(elem);
    elem.disabled = true;
    elem.focus();
}

function SetTrack1Len(Tk1Ln) {
    //document.form1.Tk1Ln.value = Tk1Ln;
}

function SetTrack2Len(Tk2Ln) {
    //document.form1.Tk2Ln.value = Tk2Ln;
}

function SetTrack3Len(Tk3Ln) {
    //document.form1.Tk3Ln.value = Tk3Ln;
}

function SetTrack1MskLen(Tk1LnMsk) {
    //document.form1.Tk1LnMsk.value = Tk1LnMsk;
}

function SetTrack2MskLen(Tk2LnMsk) {
    //document.form1.Tk2LnMsk.value = Tk2LnMsk;
}

function SetTrack3MskLen(Tk3LnMsk) {
    //document.form1.Tk3LnMsk.value = Tk3LnMsk;
}

function SetTrk1Encrypted(trk1Encrypted) {
    //document.form1.Trk1Encrypted.value = trk1Encrypted;
}

function SetTrk2Encrypted(trk2Encrypted) {
    //document.form1.Trk2Encrypted.value = trk2Encrypted;
}

function SetTrk3Encrypted(trk3Encrypted) {
    //document.form1.Trk3Encrypted.value = trk3Encrypted;
}

function SetTrack1MskData(track1) {
    //document.form1.Trk1Masked.value = track1;
}

function SetTrack2MskData(track2) {
    //document.form1.Trk2Masked.value = track2;
}

function SetTrack3MskData(track3) {
    //document.form1.Trk3Masked.value = track3;

}
function SetMPStatus(mpStatus) {
    //document.form1.MPStatus.value = mpStatus;
}

function SetMPData(mpData) {
    //document.form1.MPData.value = mpData;
}

function SetMPLen(mpDataLen) {
    //document.form1.MPLength.value = mpDataLen;
}

function SetDeviceSN(deviceSN) {
    //document.form1.DeviceSN.value = deviceSN;
}

function SetReaderID(readerID) {
    //alert("reader iD = " + readerID);
    //document.form1.ReaderID.value = readerID;
}

function SetEncodeType(encodeType) {
    //document.form1.EncodeType.value = encodeType;
}

function SetDUKPTKSN(dukptSN) {
    //document.form1.DUKPTSN.value = dukptSN;
}

function SetSessionID(sessionID) {
    //document.form1.SessionID.value = sessionID;
}

function SetCRStatus(crStatus) {
    //document.form1.Rsp.value = crStatus;
}

function SetResults(msgResults) {
    //document.form1.Rsp.value = msgResults;
}

function ShowAsDisabled(control) {
    control.readOnly = true;
    control.style.color = "gray";
}

function ShowAsEnabled(control) {
    control.readOnly = false;
    control.style.color = "black";
}

function ResetDisabledFields() {
    //document.form1.tbCreditCardNumber.value = "";
    //document.form1.tbLastName.value = "";
    //document.form1.tbFirstName.value = "";
    //document.form1.CardData.value = "";
    //document.form1.tbSecurityCode.value = "";

    // ShowAsEnabled(document.form1.tbCreditCardNumber);
    //ShowAsEnabled(document.form1.tbFirstName);
    //ShowAsEnabled(document.form1.tbLastName);
    //ShowAsEnabled(document.form1.ddlYear);
    //ShowAsEnabled(document.form1.ddlMonth);

    return false;
}

function SendCmdStr() {
    //document.JMSR.SendStrCmd(document.form1.Rsp.value); 
}

function SendCmdStrWithLength() {
    //document.JMSR.SendCmdStrWithLength(document.form1.Rsp.value); 
}

function SetAllCardData(allCardData) {
    var elem = document.getElementById("CardData");
    elem.value = allCardData

    //	alert("All Card Data = " + allCardData);
}

function SetCardReadingError() {
    alert("Card Reading Error");
    alert("Track 1 status = " + document.JMSR.GetTrack1DecodeStatus() + ".");
    alert("Track 2 status = " + document.JMSR.GetTrack2DecodeStatus() + ".");
    alert("Track 3 status = " + document.JMSR.GetTrack3DecodeStatus() + ".");
}

function ReportJavaPluginVersion(ver) {
    //alert("Calling ReportJavaPluginVersion version = " + ver);

    if (parseFloat(ver) >= 1.5) {
        //alert(ver);
        ReportCorrectJavaVersion();
    }
    else {
        //alert(ver);
        ReportIncorrectJavaVersion();
    }
}

function DeviceReady(varReady) {
    
}