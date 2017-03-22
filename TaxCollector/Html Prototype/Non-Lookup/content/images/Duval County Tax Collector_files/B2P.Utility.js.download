/*=========================================================================
File:     B2P.Utility.js
Date:     03.10.2015
Author:   Scott Leonard

Summary:  Contains various utilities used by the website.

Requires: B2P.Enums.js (and B2P.Enums.IE8.js for IE8 compatibility)

==========================================================================
Revision Record
Date          By               Description
-------------------------------------------------------------------------
03.10.2015    Scott Leonard    Written.
04.24.2015    Scott Leonard    Added restrictInput function to handle DOM
                               element character input values.
04.28.2015    Scott Leonard    Moved object enums into the enum file.
                               Added reformatInput function to handle DOM
                               element character input values during
                               paste/blur/change events.
                               Updated function comments.
01.13.2016    Scott Leonard    Added Currency check to the restrictInput
                               function.
01.19.2016    Scott Leonard    Updated the Numeric check in restrictInput
                               function to allow only one decimal point.
TODO:     
=========================================================================*/

/**
 * Looks for a type of credit card within a string.
 *
 * @param cardInfo The string to check.
 *
 * @example
 * cardType = getCardType("Visa  ************1111  (Expires: 01/17)");
 */
function getCardType(cardInfo) {
    switch (cardInfo) {
        case cardInfo.toLowerCase.indexOf("american express") >= 0:
            return "amex";
            break;

        case cardInfo.toLowerCase.indexOf("discover") >= 0:
            return "discover";
            break;

        case cardInfo.toLowerCase.indexOf("mastercard") >= 0:
            return "mastercard";
            break;

        case cardInfo.toLowerCase.indexOf("visa") >= 0:
            return "visa";
            break;

        default:
            return "";
    }
}

/**
 * Reformats DOM element input values by replacing invalid characters.
 *
 * @param item A DOM element.
 * @param retrictionType The restriction type to apply to a DOM element.
 *
 * @requires B2P.Enums.js (and B2P.Enums.IE8.js for IE8 compatibility)
 *
 * @example
 * <input type="text" id="txtEmail" onpaste="return reformatInput(this, restrictionTypes.Email);" />
 */
function reformatInput(item, restrictionType) {
    // Wrapping the field check in a callback in order for a replace to occur on the pasted value.
    // Adding 0 ms to the setTimeout to make it asynchronous.
    return setTimeout(function () {
        // Select the restriction type for validation
        switch (restrictionType) {
            case restrictionTypes.AlphaNumeric:
                item.value = item.value.replace(/[^a-z0-9]/gi, '');
                break;

            case restrictionTypes.AlphaNumericAndExtraChars:
                item.value = item.value.replace(/[^a-z0-9\s\.\-\,\'\_\/]/gi, '');
                break;

            case restrictionTypes.Date:
                item.value = item.value.replace(/[^\d\/]/gi, '');
                break;

            case restrictionTypes.Email:
                item.value = item.value.replace(/[^a-z0-9@_\.\-]/gi, '');
                break;

            case restrictionTypes.LettersOnly:
                item.value = item.value.replace(/[^a-z]/gi, '');
                break;

            case restrictionTypes.NumbersOnly:
                item.value = item.value.replace(/\D/g, '');
                break;

            case restrictionTypes.Numeric:
                item.value = item.value.replace(/[^\d\.]/g, '');
                break;

            case restrictionTypes.ZipCode:
                item.value = item.value.replace(/[^a-z0-9\- ]/gi, '');
                break;

            default:
                // Get outta here
                break;
        }

        return item.value;
    }, 0);
}

/**
 * Restricts DOM element input values to certain characters.
 *
 * @param retrictionType The restriction type to apply to a DOM element.
 * @param e An event that triggers the input check.
 *
 * @requires B2P.Enums.js (and B2P.Enums.IE8.js for IE8 compatibility)
 *
 * @example
 * <input type="text" id="txtEmail" onkeypress="return restrictInput(event, restrictionTypes.Email);" />
 */
function restrictInput(e, restrictionType) {
    e = e || window.event;              // cross-browser event check -- IE doesn't use "e"
    var code = e.which || e.keyCode;    // IE uses event.keyCode
    var input;
    var target = e.target || e.srcElement;

    if (e.metaKey || e.ctrlKey) { return true; }

    // NUL character
    if (code === 0) { return true; }

    // Only restrictionTypes.AlphaNumericAndExtraChars and restrictionTypes.ZipCode allow spaces
    if (restrictionType !== undefined && restrictionType !== "" && restrictionType !== restrictionTypes.AlphaNumericAndExtraChars && restrictionType !== restrictionTypes.ZipCode) {
        if (code === 32) return false;
    }

    // Set the character
    input = String.fromCharCode(code);

    // Special characters, left arrow, right arrow and delete (with FF check -> sends 46 for the decimal point)
    if (code < 33 || (code === 37 && !e.shiftKey) || code === 39 || code === 46 && input != ".") { return true; }

    // Set the character
    //input = String.fromCharCode(code);

    // Select the field type for validation
    switch (restrictionType) {
        case restrictionTypes.AlphaNumeric:
            return !!/[a-z0-9]/i.test(input);
            break;

        case restrictionTypes.AlphaNumericAndExtraChars:
            return !!/[a-z0-9\s\.\-\,\'\_\/]/i.test(input);
            break;

        case restrictionTypes.Currency:
            var value = String(target.value || "");
            var hasDecimal = (value.indexOf(".") > -1);

            // Only digits and the decimal point allowed with max two decimal places
            if (/[\d\.]/.test(input)) {
                if (/[\.]/.test(input) && hasDecimal) {
                    return false;
                }

                if (/[\d]/.test(input) && hasDecimal) {
                    if (value.substring(value.indexOf(".") + 1).length >= 2) {
                        return false;
                    }
                }

                // Got to here -- must be okay
                return true;
            }
            else {
                return false;
            }

            break;

        case restrictionTypes.Date:
            return !!/[\d\/]/.test(input);
            break;

        case restrictionTypes.Email:
            return !!/[a-z0-9@_\.\-]/i.test(input);
            break;

        case restrictionTypes.LettersOnly:
            return !!/[a-z]/i.test(input);
            break;

        case restrictionTypes.NumbersOnly:
            return !!/[\d]/.test(input);
            break;

        case restrictionTypes.Numeric:
            var value = String(target.value || "");
            var hasDecimal = (value.indexOf(".") > -1);

            // Only digits and the decimal point allowed
            if (/[\d\.]/.test(input)) {
                if (/[\.]/.test(input) && hasDecimal) {
                    return false;
                }

                // Got to here -- must be okay
                return true;
            }
            else {
                return false;
            }

            break;

        case restrictionTypes.ZipCode:
            return !!/[a-z0-9\- ]/i.test(input);
            break;

        default:
            return false;
    }
}