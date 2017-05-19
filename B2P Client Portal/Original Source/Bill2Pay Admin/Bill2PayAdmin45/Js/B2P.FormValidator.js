/*=========================================================================
File:     B2P.FormValidator.js
Date:     03.10.2015
Author:   Scott Leonard

Summary:  Validates input elements of HTML forms.

Requires: N/A

==========================================================================
Revision Record
Date          By               Description
-------------------------------------------------------------------------
03.10.2015    Scott Leonard    Written.
04.28.2015    Scott Leonard    Updated function comments.
09.30.2015    Scott Leonard    Updated the HTMLElement.prototype functions
                               to be compatibile with IE8. Also added Trim 
                               function for IE8.

TODO:     
=========================================================================*/

/**
 * Validates input elements of HTML forms.
 *
 * @example
 * // Create instance of the form validator
 * var validator = new FormValidator()
 * validator.setErrorMessageElement("Please review the following errors and resubmit the form:\n\n");
 * validator.setInvalidCssClass("has-errors");       // Sets the CSS class for invalid fields.
 * validator.setAlertStatus(false);                  // Won't show alert box
 * validator.setErrorTipsStatus(false);              // Won't show input element tooltip with error info
 * validator.setErrorMessageElement("lblErrMsg");    // Writes the error info to the lblErrMsg element in the document
 *    
 * // Add items to validate
 * validator.addValidationItem(new ValidationItem("txtEmail", fieldTypes.Email, true, "Invalid e-mail address.");
 *
 * // Validate the form
 * validator.validate();
 */
function FormValidator() {
    var errorAlertBoxStatus = true;     // Turns error alert popups on/off after validation.
    var errorItemListStyle = "• ";      // List-style text to display before each error item in the error dialogue (i.e., -, •, +, etc.).
    var errorMessageElement = "";       // The document element where validation error messages can be written.
    var errorMessageHeader = "";        // Message header text to display in the error dialogue.
    var errorTipsStatus = true;         // Turns input element tooltips on/off after the validation.
    var firstErrantElement = false;     // The element that gets the focus after the validation.
    var invalidCssClass = "";           // The CSS class to apply to invalid form items.
    var validCssClass = "";             // The CSS class to apply to valid form items.
    var validationItems = new Array();  // Array containing all the items to be validated.

    /**
     * Adds a validation item to be validated.
     *
     * @param validationItem {@code ValidationItem} to add to the form validator.
     */
    function addValidationItem(validationItem) {
        validationItems[validationItems.length] = validationItem;
    }

    /**
     * Removes a validation item to be validated.
     *
     * @param fieldName The name of the {@code ValidationItem} field to remove from the form validator.
     */
    function removeValidationItem(fieldName) {
        var element = document.getElementById(fieldName);

        // Remove invalid CSS class from the element
        element.removeClass(invalidCssClass);                              // The element itself
        element.parentElement.removeClass(invalidCssClass);                // Handles a Bootstrap form-group -- when styleParent is true
        element.parentElement.parentElement.removeClass(invalidCssClass);  // Handles a Bootstrap input-group in a form-group -- when styleParent is true

        // Remove the element's error info from it's tooltip
        if (errorTipsStatus) {
            $("#" + element.id).tooltip("destroy");
            $("#" + element.id).removeAttr("data-original-title");
            $("#" + element.id).removeClass("tooltip-danger");
        }

        // Remove the item from the validation item array
        for (i = 0; i < validationItems.length; i++) {
            if (validationItems[i].field === fieldName) {                
                delete validationItems[i];
            }
        }
    }

    /**
     * Sets whether the validation should display an alert dialogue box.
     *
     * @param showAlerts Whether the validation should display an alert dialogue box.
     */
    function setAlertBoxStatus(showAlerts) {
        errorAlertBoxStatus = showAlerts;
    }    

    /**
     * Sets the list-style text displayed before each error item in the error message.
     *
     * @param prefix List-style text displayed before each error item in the error message.
     */
    function setErrorItemListStyle(styleText) {
        errorItemListStyle = styleText;
    }    

    /**
     * Sets the specific document element to display the validation error message.
     *
     * @param The document element ID that will display the error message.
     */
    function setErrorMessageElement(elementId) {
        errorMessageElement = elementId;
    }

    /**
     * Sets the error message header displayed in the error dialogue before the error items.
     *
     * @param message The error message header to display in the error dialogue before the error items.
     */
    function setErrorMessageHeader(message) {
        errorMessageHeader = errorMessageHeader;
    }    

    /**
     * Sets whether informative input element tooltips should display after validation.
     *
     * @param showTips Whether or not informative input element tooltips should display after validation.
     */
    function setErrorTipsStatus(showTips) {
        errorTipsStatus = showTips;
    }

    /**
     * Sets the CSS class for invalid fields.
     *
     * @param cssClass The CSS class to apply to invalid form items.
     */
    function setInvalidCssClass(cssClass) {
        invalidCssClass = cssClass;
    }

    /**
     * Sets the CSS class for valid fields.
     *
     * @param cssClass The CSS class to apply to valid form items.
     */
    function setValidCssClass(cssClass) {
        validCssClass = cssClass;
    }

    /**
     * Validates the form items added to the form validator.
     *
     * @return <code>true</code> if all the validation items validate, otherwise <code>false</code>.
     */
    function validate() {
        var errMsg = "";
        var element = null;

        // Loop through the form field items
        for (i = 0; i < validationItems.length; i++) {
            element = document.getElementById(validationItems[i].field);

            // Change the field's visual status
            if (validationItems[i].isValid) {

                // This is basically to handle Bootstrap form groups
                // Looking to style the whole group -- the label and the input
                if (validationItems[i].styleParent === true) {
                    if (element.parentElement.hasClass('input-group')) {
                        element.parentElement.parentElement.removeClass(invalidCssClass);
                        element.parentElement.parentElement.addClass(validCssClass);
                    }
                    else {
                        element.parentElement.removeClass(invalidCssClass);
                        element.parentElement.addClass(validCssClass);
                    }
                }
                else {
                    element.removeClass(invalidCssClass);
                    element.addClass(validCssClass);
                }

                // Remove the element's error info from it's tooltip
                // if (element.type === "text" && errorTipsStatus) {
                if (errorTipsStatus) {
                    $("#" + element.id).tooltip("destroy");
                    $("#" + element.id).removeAttr("data-original-title");
                    $("#" + element.id).removeClass("tooltip-danger");
                }
            }
            else {
                // Update the error message
                errMsg += errorItemListStyle + validationItems[i].errorMessage + "\n";
                
                // This is basically to handle Bootstrap form groups
                // Looking to style the whole group -- the label and the input
                if (validationItems[i].styleParent === true) {
                    if (element.parentElement.hasClass('input-group')) {
                        element.parentElement.parentElement.addClass(invalidCssClass);
                    }
                    else {
                        element.parentElement.addClass(invalidCssClass);
                    }
                }
                else {
                    element.addClass(invalidCssClass);
                }

                // See if an errant input has already been set to receive focus 
                if (firstErrantElement === false) {
                    firstErrantElement = element;
                }

                // Set the input's title to the error message
                element.title = validationItems[i].errorMessage;

                // Show the element's error info in it's Bootstrap tooltip
                // if ((element.type === "text" || element.type === "select-one") && errorTipsStatus) {
                if (errorTipsStatus) {
                    $("#" + element.id).tooltip({
                        show: true,
                        trigger: "focus",
                        placement: function () {
                            if ($("#" + element.id).is(':checkbox') || $(window).width() < 768) {
                                return "top";
                            }
                            return "right";
                        }
                    }).addClass("tooltip-danger");
                }
            }
        }

        // Check if we have any validation error messages
        if (errMsg === "") {
            return true;
        }
        else {        
            // Check if the error message needs to be written to the document
            if (errorMessageElement !== undefined && errorMessageElement !== "") {
                element = document.getElementById(errorMessageElement);

                // Check if the element is visible
                if (element.style.visibility === "hidden") {
                    element.style.visibility = "visible";
                }

                // Since FireFox sucks, we have to check to see if we can use formatted text on the element
                if (document.body.innerText) {
                    element.innerText = errMsg;
                } else {
                    // FireFox sucks
                    element.innerHTML = errMsg.replace(/\n/gi, "<br />");
                }
                //element.textContent = errMsg;
            }

            // Check if alerts are enabled
            if (errorAlertBoxStatus) {
                alert(errorMessageHeader + errMsg);
            }

            // Set the focus to the first errant element
            firstErrantElement.focus();
            return false;
        }
    }

    this.addValidationItem = addValidationItem;
    this.removeValidationItem = removeValidationItem;
    this.setAlertBoxStatus = setAlertBoxStatus;
    this.setErrorItemListStyle = setErrorItemListStyle;
    this.setErrorMessageElement = setErrorMessageElement;
    this.setErrorMessageHeader = setErrorMessageHeader;
    this.setErrorTipsStatus = setErrorTipsStatus;   
    this.setInvalidCssClass = setInvalidCssClass;
    this.setValidCssClass = setValidCssClass;
    this.validate = validate;
}


// Mimic a bit o' jQuery functionality here

// IE8 sucks since it doesn't use HTMLElement
var elementPrototype = typeof HTMLElement !== "undefined" ? HTMLElement.prototype : Element.prototype;

elementPrototype.hasClass = function (className) {
    return this.className.match(new RegExp('(\\s|^)' + className + '(\\s|$)'));
}

elementPrototype.addClass = function (className) {
    if (!this.hasClass(className)) {
        this.className += ' ' + className;
        this.className.trim();
    }
};

elementPrototype.removeClass = function (className) {
    var newClassName = "";
    var i;
    var classes = this.className.split(" ");

    for (i = 0; i < classes.length; i++) {
        if (classes[i] !== className) {
            newClassName += classes[i] + " ";
        }
    }
    this.className = newClassName.trim();
}

elementPrototype.toggleClass = function (className) {
    var newClass = ' ' + this.className.replace(/[\t\r\n]/g, " ") + ' ';

    if (this.hasClass(className)) {
        while (newClass.indexOf(" " + className + " ") >= 0) {
            newClass = newClass.replace(" " + className + " ", " ");
        }
        this.className = newClass.replace(/^\s+|\s+$/g, ' ');
    } else {
        this.className += ' ' + className;
    }
    this.className.trim();
};

// IE8 sucks since it doesn't have native JS trim function
if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    }
}