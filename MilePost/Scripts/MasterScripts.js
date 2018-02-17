function validControl() {
    var txtUserName = document.getElementById('txtUserName');
    var txtPassword = document.getElementById('txtPassword');
    var lblInvalid = document.getElementById('lblInvalid');
    if (txtUserName.value == '') {
        alert('Please enter Username');
        txtUserName.focus();
        return false;
    }
    if (txtPassword.value == '') {
        alert('Please enter Password');
        txtPassword.focus();
        return false;
    }
    return true;
}
function clearTextBox() {
    document.getElementById('txtUserName').value = "";
    document.getElementById('txtPassword').value = "";
    return true;
}
function winOpenRequestDate() {
    window.open('Cal.aspx', "mywindow", "left=500,top=400,width=60,height=190,toolbar=0,resizable=0");
}
function winOpenOverwriteDate() {
    window.open('CalTarget.aspx', "mywindow", "left=500,top=400,width=60,height=190,toolbar=0,resizable=0");
}
function validatePolicyEntry() {
    var txtPolicyNo = document.getElementById("txtPolicyNo");
    var txtRequestDate = document.getElementById("txtRequestDate");
    var txtBusinessReason = document.getElementById("txtBusinessReason");
    var txtRootSegCount = document.getElementById("txtRootSegCount");
    var txtMachineSegCount = document.getElementById("txtMachineSegCount");
    var txtFaceSegCount = document.getElementById("txtFaceSegCount");
    var numbers = /^[0-9]+$/;
    var policy_value = txtPolicyNo.value;
    var businessReason_value = txtBusinessReason.value;

    var currentDate = new Date();
    var twoDigitMonth = ((currentDate.getMonth() + 1) >= 10) ? (currentDate.getMonth() + 1) : '0' + (currentDate.getMonth() + 1);
    var twoDigitDate = ((currentDate.getDate()) >= 10) ? (currentDate.getDate()) : '0' + (currentDate.getDate());
    var today = twoDigitMonth + "/" + twoDigitDate + "/" + currentDate.getFullYear();


    if (txtPolicyNo.value == null || txtPolicyNo.value == "") {
        alert("Please enter Policy No.");
        txtPolicyNo.focus();
        return false;
    }
    else {
        if (policy_value.length == 8) {
            for (var index = 0; index <= 7; index++) {
                if (index == 1) {
                    if (policy_value.charAt(index) != 'Z') {
                        if (policy_value.charAt(index) != 'Y') {
                            if (policy_value.charAt(index) != 'U') {
                                alert('2nd Character of Policy Number should be Z/Y/U');
                                txtPolicyNo.focus();
                                return false;
                            }
                        }
                    }
                }
                else {
                    if (!policy_value.charAt(index).match(numbers)) {
                        alert('Except the 2nd Position, others should be numeric [0-9]');
                        txtPolicyNo.focus();
                        return false;
                    }
                }
            }
        }
        else {
            alert('Policy Number must be 8 characters length');
            txtPolicyNo.focus();
            return false;
        }
    }

    if (txtRequestDate.value == null || txtRequestDate.value == "") {
        alert("Please enter Request Date.");
        txtRequestDate.focus();
        return false;
    }
    else {

        if (!ChkDate(txtRequestDate)) {
            alert("Please enter a valid Request date in MM/DD/YYYY or MM/DD/YY format");
            txtRequestDate.focus();
            return false;
        }
        if (txtRequestDate.value < today) {
            alert("Request Date should be greater than or equal to Current Date");
            txtRequestDate.focus();
            return false;
        }
    }
    if (txtBusinessReason.value == null || txtBusinessReason.value == "") {
        alert("Please enter Business Reason.");
        txtBusinessReason.focus();
        return false;
    }

    if (businessReason_value.length > 2000) {
        alert("Business Reason can not be more than 2000 characters.");
        txtBusinessReason.focus();
        return false;
    }

    if (txtRootSegCount.value == null || txtRootSegCount.value == "") {
        alert("Please enter Root Segment Count.");
        txtRootSegCount.focus();
        return false;
    }
    if (!txtRootSegCount.value.match(numbers)) {
        alert('Please input numeric characters only for Root Segment Count');
        txtRootSegCount.focus();
        return false;
    }
    if (txtMachineSegCount.value == null || txtMachineSegCount.value == "") {
        alert("Please enter Machine Segment Count.");
        txtMachineSegCount.focus();
        return false;
    }
    if (!txtMachineSegCount.value.match(numbers)) {
        alert('Please input numeric characters only for Machine Segment Count');
        txtMachineSegCount.focus();
        return false;
    }
    if (txtFaceSegCount.value == null || txtFaceSegCount.value == "") {
        alert("Please enter Face Segment Count.");
        txtFaceSegCount.focus();
        return false;
    }
    if (!txtFaceSegCount.value.match(numbers)) {
        alert('Please input numeric characters only for Face Segment Count');
        txtFaceSegCount.focus();
        return false;
    }
}


function DeleteConfirmMsg() {
    var status = confirm("Are you sure you want to delete?");
    if (status)
        return true;
    else
        return false;

}
function validatePolicyEnquiry() {
    var txtPolicyNo = document.getElementById("txtPolicyNo");
    var txtRequestDate = document.getElementById("txtRequestDate");
    var txtOverwriteDate = document.getElementById("txtEndDate");
    //var grdView = document.getElementById("GrdView2");  
    var numbers = /^[0-9]+$/;
    var policy_value = txtPolicyNo.value;

    if (txtPolicyNo.value == "") {
        if (txtRequestDate.value != "") {
            if (!ChkDate(txtRequestDate)) {
                alert("Please enter a valid Start date in MM/DD/YYYY format");
                txtRequestDate.focus();
                return false;
            }
            if (txtOverwriteDate.value == "") {
                //grdView.style.display='none';
                //grdView.style.visibility="hidden";
                alert("Please enter both Start Date and End Date");
                txtOverwriteDate.focus();
                return false;
            }
            else {
                if (!ChkDate(txtOverwriteDate)) {
                    alert("Please enter a valid End Date in MM/DD/YYYY format");
                    txtOverwriteDate.focus();
                    return false;
                }
            }
        }
        else {
            if (txtOverwriteDate.value == "") {
                //grdView.style.display='none';
                //grdView.style.visibility="hidden";
                alert("Please enter Policy No and/ Or Date range");
                txtPolicyNo.focus();
                return false;
            }
            else {
                if (!ChkDate(txtOverwriteDate)) {
                    alert("Please enter a valid End Date in MM/DD/YYYY format");
                    txtOverwriteDate.focus();
                    return false;
                }
                else {
                    alert("Please enter Date range and/ Or Policy No");
                    txtPolicyNo.focus();
                    return false;
                }
            }
        }
    }
    else {
        if (policy_value.length == 8) {
            for (var index = 0; index <= 7; index++) {
                if (index == 1) {
                    if (policy_value.charAt(index) != 'Z') {
                        if (policy_value.charAt(index) != 'Y') {
                            if (policy_value.charAt(index) != 'U') {
                                alert('2nd Character of Policy Number should be Z/Y/U');
                                txtPolicyNo.focus();
                                return false;
                            }
                        }
                    }
                }
                else {
                    if (!policy_value.charAt(index).match(numbers)) {
                        alert('Except the 2nd Position, others should be numeric [0-9]');
                        txtPolicyNo.focus();
                        return false;
                    }
                }
            }
        }
        else {
            alert('Policy Number must be 8 characters length');
            txtPolicyNo.focus();
            return false;
        }
        if (txtRequestDate.value != "") {
            if (txtOverwriteDate.value == "") {
                //grdView.style.display='none';
                //grdView.style.visibility="hidden";
                alert("Please enter both Start Date and End Date");
                txtOverwriteDate.focus();
                return false;
            }
        }
        else {
            if (txtOverwriteDate.value != "") {
                //grdView.style.display='none';
                //grdView.style.visibility="hidden";
                if (!ChkDate(txtOverwriteDate)) {
                    alert("Please enter a valid End Date in MM/DD/YYYY format");
                    txtOverwriteDate.focus();
                    return false;
                }
                else {
                    alert("Please enter both Start Date and End Date");
                    txtOverwriteDate.focus();
                    return false;
                }
            }
        }
    }

    //         if(!ChkDate(txtRequestDate))
    //         {
    //            alert("Please enter a valid Start date in MM/DD/YYYY format");
    //            txtRequestDate.focus();
    //            return false;
    //         }
    //         if(!ChkDate(txtOverwriteDate))
    //         {
    //            alert("Please enter a valid End Date in MM/DD/YYYY format");
    //            txtOverwriteDate.focus();
    //            return false;
    //         }
    if (Date.parse(txtRequestDate.value, 'MM/DD/YYYY') > Date.parse(txtOverwriteDate.value, 'MM/DD/YYYY')) {
        alert("End Date should be greater than Start Date")
        txtOverwriteDate.focus();
        return false;
    }
}

function ChkDate(objName) {
    var strDate;
    var strDateArray;
    var strDay;
    var strMonth;
    var strYear;
    var intday;
    var intMonth;
    var intYear;
    var datefield = objName;
    var strSeparatorArray = new Array("/");
    var intElementNr;
    var err = 0;

    strDate = datefield.value;
    if (strDate.length < 1) {
        return true;
    }
    for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++) {
        if (strDate.indexOf(strSeparatorArray[intElementNr]) != -1) {
            strDateArray = strDate.split(strSeparatorArray[intElementNr]);
            if (strDateArray.length != 3) {
                err = 1;
                return false;
            }
            else {
                strMonth = strDateArray[0];
                strDay = strDateArray[1];
                strYear = strDateArray[2];
                break;
            }
        }
    }
    if (strDateArray == null)
        return false;

    if (strMonth.length == 1) {
        strMonth = '0' + strMonth;
    }
    if (strDay.length == 1) {
        strDay = '0' + strDay;
    }
    if (strYear.length != 4) {
        if (strYear.length == 2) {
            strYear = '20' + strYear;
        }
        else {
            return false;
        }
    }
    if (strYear.length == 3) {
        return false;
    }

    intday = parseInt(strDay, 10);
    if (isNaN(intday)) {
        err = 2;
        return false;
    }
    intMonth = parseInt(strMonth, 10);
    if (isNaN(intMonth)) {
        err = 3;
        return false;
    }
    intYear = parseInt(strYear, 10);
    if (isNaN(intYear)) {
        err = 4;
        return false;
    }
    if (intMonth > 12 || intMonth < 1) {
        err = 5;
        return false;
    }
    if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intday > 31 || intday < 1)) {
        err = 6;
        return false;
    }
    if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intday > 30 || intday < 1)) {
        err = 7;
        return false;
    }
    if (intMonth == 2) {
        if (intday < 1) {
            err = 8;
            return false;
        }
        if (LeapYear(intYear) == true) {
            if (intday > 29) {
                err = 9;
                return false;
            }
        }
        else {
            if (intday > 28) {
                err = 10;
                return false;
            }
        }
    }
    if (intday <= 31) {
        strDay = intday;
        strDay = String(strDay);
        if (strDay.length == 1) {
            strDay = '0' + strDay;
        }
    }
    if (intMonth <= 12) {
        strMonth = intMonth;
        strMonth = String(strMonth);
        if (strMonth.length == 1) {
            strMonth = '0' + strMonth;
        }
    }

    datefield.value = strMonth + strSeparatorArray[intElementNr] + strDay + strSeparatorArray[intElementNr] + strYear;
    return true;
}
function LeapYear(intYear) {
    if (intYear % 100 == 0) {
        if (intYear % 400 == 0) {
            return true;
        }
    }
    else {
        if ((intYear % 4) == 0) {
            return true;
        }
    }
    return false;
}

function CheckTextAreaMaxLength(txt, maxLen) {
    var maximumLen = maxLen - 1;
    if (txt.value.length > maximumLen)
        return false;
    else {
        return true;
    }
}

function CheckPolicyNoMaxLength(txt, maxLen) {
    var maximumLen = maxLen - 1;
    if (txt.value.length > maximumLen)
        return false;
    else {
        return true;
    }
}  