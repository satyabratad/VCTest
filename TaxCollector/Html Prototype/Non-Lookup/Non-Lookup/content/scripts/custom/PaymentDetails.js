// JavaScript source code

var bill2payPaymentDetails = {
showCart: function(){
	debugger;
	var param=getParameterByName('dbObject');
	DeSrializeDbObject(param);
	$("#cartCount").html((dbObject.Products.length==0?"":dbObject.Products.length));
	bill2payPaymentDetails.populateSubtotal();
	bill2payPaymentDetails.populateOrderDetails();
	bill2payPaymentDetails.populateCreditCardDetails();
},
redirectToCartGrid: function(){
	var json=SerializeDbObject();		
      redirect('Home.html?ShowCart=Y&dbObject=' + json);	
},
redirectToPaymentConfirm: function(){
	debugger;
	var firstTab = $('a[data-toggle="tab"]:first');
            var tabName = null;
			var paymentType=null;
            // Get the last active tab before postback
            tabName = $("[id*=hdfTabName]").val() != "" ? $("[id*=hdfTabName]").val() : firstTab.attr("href").replace("#", "");
            
            if(tabName=='pnlTabCredit')
            {
            	paymentType='CC';
				bill2payPaymentDetails.addCreditCardDetails();
			}
				
			else
			{
								paymentType='ACH';
				bill2payPaymentDetails.addBankDetails();	
				}				
			
				
	if(validateForm(paymentType))
	{
		var json=SerializeDbObject();		
      redirect('PaymentConfirm.html?ShowCart=Y&dbObject=' + json);	
	}
	
},
addCreditCardDetails:function(){
	debugger;
	var cc = {
								"PaymentType": "CC",
								"NameOnCard": $("#txtNameonCard").val(),
								"CardNumber": $("#txtCreditCardNumber").val(),
								"ExpDate":  $("#txtExpireDate").val().replace('/','_'),
								"CVV":  $("#txtCCV").val(),
								"Country":  $("#ddlCountry").val(),
								"Zip":  $("#txtBillingZip").val(),								
								
						  };
		billingDetails=cc;
		UpdateDbObject();
},
populateCreditCardDetails:function(){
	debugger;
		
	$("#txtNameonCard").val('');
	$("#txtExpireDate").val('');
	$("#ddlCountry").val('');
	$("#txtBillingZip").val('');
	if(dbObject.BillingDetails!=null){
		
	$("#txtNameonCard").val(dbObject.BillingDetails.NameOnCard);
	$("#txtExpireDate").val(dbObject.BillingDetails.ExpDate.replace('_','/'));
	$("#ddlCountry").val(dbObject.BillingDetails.Country);
	$("#txtBillingZip").val(dbObject.BillingDetails.Zip);
	
	}
	
},
addBankDetails:function(){
	var ach = {
								"PaymentType": "ACH",
								"NameonBank": $("#txtNameonBankAccount").val(),
								"BankRoutingNumber": $("#txtBankRoutingNumber").val(),
								"BankAccountNumber":  $("#txtBankAccountNumber").val(),
															
								
						  };	
						  billingDetails=ach;
						  UpdateDbObject();
},
addMoreItems: function(){
	debugger;
	var json=SerializeDbObject();		
    redirect('Home.html?dbObject=' + json);	
},
populateSubtotal: function(){
	var total=bill2payProducts.getCartTotalAmount();
	$("#paysubTotalAmount").html(total);
},
populateOrderDetails: function(){
	debugger;
		var html = '';
		var totalAmount=0;
		var cellClass='';
		 html += '<div class="table-responsive" style="padding:0;">';
          html += '<table id="tblOrderInfo" class="table table-condensed table-no-border">';
          html += '<div id="pnlItem1">';
           for (var i = 0; i < dbObject.Products.length; i++) {
           		var row=dbObject.Products[i];
          		html += '<tr class="bg-primarydark">';
          		html += '<td class="col-sm-6 text-uppercase"><label id="ItemName">Order Item'+(i+1)+' </label> </td>';
           		html += '<td style="text-align:right"><label id="ItemAmount">$' + parseFloat(row.Amount).toFixed(2)+'</label> </td>';
          		html +=  '</tr>';
          		html +=  '<tr>';
          		if(row.ACC1!=null){
					html += '<td class="col-sm-6 text-uppercase">'+getKeyFromJson(row.ACC1)+'</td>';
          			html += '<td style="text-align:right">'+getValueFromJson(row.ACC1)+'</td>';
				}
				html +=  '</tr>';
           		if(row.ACC2!=null){
					html += '<td class="col-sm-6 text-uppercase">'+getKeyFromJson(row.ACC2)+'</td>';
          			html += '<td style="text-align:right">'+getValueFromJson(row.ACC2)+'</td>';
				}
				html +=  '</tr>';
				if(row.ACC3!=null){
					html += '<td class="col-sm-6 text-uppercase">'+getKeyFromJson(row.ACC3)+'</td>';
          			html += '<td style="text-align:right">'+getValueFromJson(row.ACC3)+'</td>';
				}
           		html += '</tr>';
           		
           }
 		html +=           '</div>  ';                                                             
 		html +=           '</table>';
       	html +=     '<div id="dvsubTotal">';
        html +=    '<table id="tblOrderInfo" class="table table-condensed table-no-border">';
       	html +=     '<div class="col-xs-12 bg-primarydark">';
        html +=    '<tr class="bg-primarydark">';
        html +=   '<td class="col-sm-6 text-uppercase"><label id="subTotal" >Subtotal </label> </td>';
        html +=  '<td style="text-align:right"><label id="subTotalAmount"  >'+bill2payProducts.getCartTotalAmount().toString()+'</label> </td>';
        html += '</tr>';                         
        html +=  '</div>';
        html +=  '</table>';
        html += '</div>';
 		html +=       '</div>';
        
	  	$('#orderDetails').html('');
	    $('#orderDetails').append(html); 
},

};