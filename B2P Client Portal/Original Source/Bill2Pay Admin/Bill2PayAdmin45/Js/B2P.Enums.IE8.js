/*=========================================================================
File:     B2P.Enums.IE8.js
Date:     09.30.2015
Author:   Scott Leonard

Summary:  Creates an object with a property named Enum for IE8. Can be 
          used for any class that needs an enum property. Borrowed from
          NPM -- look here for more info: 
          https://github.com/tolgaek/node-enum

Requires: N/A

==========================================================================
Revision Record
Date          By               Description
-------------------------------------------------------------------------
09.30.2015    Scott Leonard    Written.
01.13.2016    Scott Leonard    Added Currency to the restrictionTypes.

TODO:     
=========================================================================*/

/**
 * This is a very small, fully-functional JavaScript enum implementation.
 */
Enum = function () { v = arguments; s = { all: [], keys: v }; for (i = v.length; i--;) s[v[i]] = s.all[i] = i; return s }

// The field type enums used in the form validation classes.
var fieldTypes = Enum(
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
var modalTypes = Enum(
    'None',
    'Danger',
    'Info',
    'Success',
    'Warning'
);

// The restriction type enums used in form input fields.
var restrictionTypes = Enum(
    'Currency',
    'Date',
    'Email',
    'LettersOnly',
    'AlphaNumericAndExtraChars',
    'NumbersOnly',
    'Numeric',
    'ZipCode'
);