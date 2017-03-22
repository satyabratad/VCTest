/*=========================================================================
File:     B2P.ValidationType.js
Date:     03.10.2015
Author:   Scott Leonard

Summary:  Contains the various validation item types use by in the
          FormValidator.

Requires: B2P.Enums.js (and B2P.Enums.IE8.js for IE8 compatibility)

==========================================================================
Revision Record
Date          By               Description
-------------------------------------------------------------------------
03.10.2015    Scott Leonard    Written.
04.27.2015    Scott Leonard    Moved object enum creation code to it's own 
                               file to be used by various classes.
04.28.2015    Scott Leonard    Moved object enums into the enum file.
                               Updated function comments.
07.26.2016    Scott Leonard    Updated credit card validation regex to
                               include new MasterCard BIN range. Updated
                               validateExpiryDate function to limit credit
                               card expiration year <= current year + 10.
08.05.2016    Scott Leonard    Added credit card expiration validation.

TODO:     
=========================================================================*/

/**
 * A form item that needs to validate it's value.
 *
 * @param field ID of the field to be validated.
 * @param fieldType Type of field to be validated.
 * @param styleParent Whether to apply an invalid CSS style to the field's parent element.
 * @param errorMsg Error message to display if the input does not validate.
 *
 * @requires B2P.Enums.js (and B2P.Enums.IE8.js for IE8 compatibility)
 *
 * @example
 * var item = new ValidationItem("txtEmail", fieldTypes.Email, true, "Invalid e-mail address.");
 */
function ValidationItem(field, fieldType, styleParent, errorMsg) {
    this.errorMessage = errorMsg;
    this.field = field;
    this.fieldType = fieldType;
    this.styleParent = styleParent;

    // Select the field type for validation
    switch (this.fieldType) {
        case fieldTypes.AccountNumber:
            this.isValid = validate(this.field, /^\d{10}$/);
            break;

        case fieldTypes.BankAccount:
            this.isValid = validate(this.field, /^\d{1,17}$/);
            break;

        case fieldTypes.CheckboxChecked:
            this.isValid = validateCheckBox(this.field);
            break;

        case fieldTypes.CreditCard:
            this.isValid = validate(this.field, /^((4\d{3})|(2[2-7]\d{2})|(5[1-5]\d{2})|(6011))-?\d{4}-?\d{4}-?\d{4}|3[4,7]\d{13}$/);
            break;

        case fieldTypes.CreditCardExpiration:
            this.isValid = validateExpirationDate(this.field);
            break;

        case fieldTypes.Currency:
            this.isValid = validate(this.field, /^\$?(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$/);
            break;

        case fieldTypes.CVV:
            this.isValid = validate(this.field, /^[0-9]{3,4}$/);
            break;

        case fieldTypes.Date:
            this.isValid = validate(this.field, /^(0?[1-9]|1[012])\/(0?[1-9]|[12]\d|3[01])\/((19|2\d)\d\d)$/);
            break;

        case fieldTypes.Email:
            // Per RFC specs, e-mail address local and domain parts have no leading, trailing, or
            // consecutive dots and top-level domain has two to six letters.
            this.isValid = validate(this.field, /^[\w%+-]+(?:\.[\w%+-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$/i);
            break;


        case fieldTypes.Integer:
            this.isValid = validate(this.field, /^[\-]?\d+$/);
            break;

        case fieldTypes.IpAddress:
            this.isValid = validate(this.field, /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/);
            break;

        case fieldTypes.Name:
            this.isValid = validate(this.field, /^[a-z0-9\s\.\-\,\']+$/i);
            break;

        case fieldTypes.NonEmptyField:
            this.isValid = validate(this.field, /^\w+/);
            break;

        case fieldTypes.Numeric:
            this.isValid = validate(this.field, /^[\-]?\d+(.\d+)?$/);
            break;

        case fieldTypes.OptionSelected:
            this.isValid = validateRadioButtons(this.field);
            break;

        case fieldTypes.PhoneFax:
            this.isValid = validate(this.field, /^(\(\d{3}\)|\d{3})[\-\s]?\d{3}[\-\s]?\d{4}$/);
            break;

        case fieldTypes.PositiveInteger:
            this.isValid = validate(this.field, /^\d+$/);
            break;

        case fieldTypes.RoutingNumber:
            this.isValid = validate(this.field, /^((0[0-9])|(1[0-2])|(2[1-9])|(3[0-2])|(6[1-9])|(7[0-2])|80)([0-9]{7})$/);
            break;

        case fieldTypes.SSN:
            // First 3 digits are called the area number and cannot be 000, 666, or between 900 and 999.
            // Digits 4 and 5 are called the group number and range from 01 to 99.
            // Last 4 digits are serial numbers from 0001 to 9999.
            this.isValid = validate(this.field, /^(?!000|666)[0-8][0-9]{2}[\-\s]?(?!00)[0-9]{2}[\-\s]?(?!0000)[0-9]{4}$/);
            break;

        case fieldTypes.StateAbbreviation:
            this.isValid = validate(this.field, /^(AL|AK|AZ|AR|CA|CO|CT|DE|DC|FL|GA|HI|ID|IL|IN|IA|KS|KY|LA|ME|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|OH|OK|OR|PA|RI|SC|SD|TN|TX|UT|VT|VA|WA|WV|WI|WY|AB|BC|MB|NB|NL|NS|NT|NU|ON|PE|QC|SK|YT)$/i);
            break;

        case fieldTypes.TrueFalse:
            this.isValid = validate(this.field, /^(T(RUE)?|F(ALSE)?)$/i);
            break;

        case fieldTypes.URL:
            this.isValid = validate(this.field, /^(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:\/~\+#]*[\w\-\@?^=%&\/~\+#])?/i);
            break;

        case fieldTypes.YesNo:
            this.isValid = validatethis.field, (/^(Y(ES)?|N(O)?)$/i);
            break;

        case fieldTypes.ZipCodeCanada:
            // CAN postal codes do not include the letters D, F, I, O, Q or U, and the first position also does not use the letters W or Z
            this.isValid = validate(this.field, /^(?!.*[DFIOQU])[A-VXY]\d[A-Z]\s?\d[A-Z]\d$/i);
            break;

        case fieldTypes.ZipCodeInternational:
            this.isValid = validate(this.field, /^[a-z0-9]+[ \-]?[a-z0-9]+$/i);
            break;

        case fieldTypes.ZipCodeUnitedStates:
            // USA zip codes can match zip or zip+4
            this.isValid = validate(this.field, /^\d{5}(?:-\d{4})?$/);
            break;

        default:
            this.isValid = false;
    }

    // Validate input against a RegEx pattern.
    function validate(field, regexPattern) {
        return regexPattern.test(document.getElementById(field).value);
    }

    // Validate credit card expiration date
    function validateExpirationDate(field) {
        var currentTime = new Date();
        var dateParts = document.getElementById(field).value.replace(" ", "").split("/");
        var month = dateParts[0].trim();
        var year = dateParts[1].trim();
        var expiry = new Date(year, month);

        // Reset the month since they are zero-based in javascript
        expiry.setMonth(expiry.getMonth() - 1);

        // Set expiration date to first day of next month
        expiry.setMonth(expiry.getMonth() + 1, 1);

        return (currentTime < expiry && expiry.getFullYear() <= currentTime.getFullYear() + 10);
    }

    // Validate that at least one radio button in a group is selected.
    function validateRadioButtons(groupName) {
        var options = document.getElementsByName(groupName);

        // Loop through the options to see if anything is selected
        if (options !== null) {
            for (var i = 0; i < options.length; i++) {
                if (options[i].checked) {
                    return true;
                }
            }
        }

        return false;
    }

    // Validate that a checkbox is checked.
    function validateCheckBox(field) {
        return document.getElementById(field).checked;
    }
}