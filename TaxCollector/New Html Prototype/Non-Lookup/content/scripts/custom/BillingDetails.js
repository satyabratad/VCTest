// JavaScript source code
var bill2payBillingDetails = {

    addBillingDetails: function(json=null)
    {
        debugger;
        if(json!=null)
        {
            DeSrializeDbObject(json);
        }

        billingDetails={
            
            "NameonCard":$('#txtNameonCard').val(""),
            "CreditCardNumber":$('#txtCreditCardNumber').val(""),
            "CreditCardNumber":$('#txtCreditCardNumber').val(""),
            "ExpireDate":$('#txtExpireDate').val(""),
            "CCV":$('#txtCCV').val(""),
            "BillingZip":$('#txtBillingZip').val(""),
            "Country":$('#ddlCountry').val(""),            
            "NameonBankAccount":$('#txtNameonBankAccount').val(""),
            "BankAccountType":$('#ddlBankAccountType').val(""),
            "BankRoutingNumber":$('#txtBankRoutingNumber').val(""),
            "txtBankAccountNumber":$('#txtBankAccountNumber').val("")
        };
        UpdateDbObject();
        window.location.href = 'file:///C:/TaxCollector/Bill2Pay%20Html/Bill2Pay%20Html/PaymentConfirm.html?products=' + JSON.stringify(products)+',contactInfo='+JSON.stringify(contactInfo)+',billingDetails='+JSON.stringify(billingDetails);  
    }

};