/*=========================================================================
File:     B2P.Enums.js
Date:     04.27.2015
Author:   Scott Leonard

Summary:  Creates an object with a property named Enum. Can be used for any
          class that needs a type-safe(ish) enum property.

Requires: N/A

==========================================================================
Revision Record
Date          By               Description
-------------------------------------------------------------------------
04.27.2015    Scott Leonard    Written.
01.13.2016    Scott Leonard    Added Currency to the restrictionTypes.

TODO:     
=========================================================================*/

/**
 * Create an Enum function on the base Object class and set its properties.
 * Set the properties to writeable = false to make them type-safe(ish).
 */
Object.defineProperty(Object.prototype, 'Enum', {
    value: function () {
        for (i in arguments) {
            Object.defineProperty(this, arguments[i], {
                value: parseInt(i),
                writable: false,
                enumerable: true,
                configurable: true
            });
        }
    },
    writable: false,
    enumerable: false,
    configurable: false
});

// Create the objects that need enum values.
var fieldTypes = {};
var modalTypes = {};
var restrictionTypes = {};

// The field type enums used in the form validation classes.
fieldTypes.Enum(
    'AccountNumber',
    'BankAccount',
    'CheckboxChecked',
    'CreditCard',
    'Currency',
    'CVV',
    'Date',
    'Email',
    'Integer',
    'IpAddress',
    'Name',
    'NonEmptyField',
    'Numeric',
    'OptionSelected',
    'PhoneFax',
    'PositiveInteger',
    'RoutingNumber',
    'SSN',
    'StateAbbreviation',
    'TrueFalse',
    'URL',
    'YesNo',
    'ZipCodeCanada',
    'ZipCodeInternational',
    'ZipCodeUnitedStates'
);

// The modal dialog type enums used in client messaging.
modalTypes.Enum(
    'None',
    'Danger',
    'Info',
    'Success',
    'Warning'
);

// The restriction type enums used in form input fields.
restrictionTypes.Enum(
    'Currency',
    'Date',
    'Email',
    'LettersOnly',
    'AlphaNumericAndExtraChars',
    'NumbersOnly',
    'Numeric',
    'ZipCode'
);