
var bill2payProducts = {


    addProducts: function (json=null) {
       
       if(json!=null)
       DeSrializeDbObject(json);
       
		var selectedProduct=$("#ddlCategories").val();
		
		if(selectedProduct.toUpperCase()=="TAX BILL")
		{
			var product = {
								//Product Details
								"ProductName": selectedProduct,
								"ACC1":{"PARCEL":$("#txtLookupAccount1").val()},
								"ACC2":{"TAX YEAR":$("#txtLookupAccount2").val()},
								"ACC3":{"OWNER NAME":$("#txtLookupAccount3").val()},								
								
								//Property Details
								"Address1":  $("#txtPropAddress1").val(),
								"Address2":  $("#txtPropAddress2").val(),
								"City":  $("#txtPropCity").val(),
								"State":  $("#ddlPropState").val(),
								"Zip":  $("#txtPropZip").val(),								
								
								//Amount
								"Amount":  $("#txtPropAmount").val(),
						  };
			
			products.push(product);
			
		}
		else if(selectedProduct.toUpperCase()=="DMV APPLICATION")
		{
			var product = {
								//Product Details
								"ProductName": selectedProduct,
								"ACC1":{"LECENSE NUMBER":$("#txtLookupAccount1").val()},
								"ACC2": null,
								"ACC3":null,	
								
								//Property Details
								"Address1":  $("#txtPropAddress1").val(),
								"Address2":  $("#txtPropAddress2").val(),
								"City":  $("#txtPropCity").val(),
								"State":  $("#ddlPropState").val(),
								"Zip":  $("#txtPropZip").val(),								
								
								//Amount
								"Amount":  $("#txtPropAmount").val(),
						  };
			
			products.push(product);
			
		}
		UpdateDbObject();
    },
	 removeProduct: function (itemIndex) {
	 	
		dbObject.Products.splice( itemIndex, 1 );
		UpdateDbObject();
    },
    getCartTotalAmount: function (){
    	var totalAmount=0;
		for (var i = 0; i < dbObject.Products.length; i++) {
				totalAmount=(parseFloat(totalAmount)+parseFloat(dbObject.Products[i].Amount)).toFixed(2);
			}	
			return '$'+totalAmount;
	},
	
};