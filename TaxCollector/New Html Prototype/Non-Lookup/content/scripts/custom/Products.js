
var bill2payProducts = {


    addProducts: function (json) {
       
       if(json!=null)
       DeSrializeDbObject(json);
       
		var selectedProduct=$("#ddlCategories").val();
		
		if(selectedProduct.toUpperCase()=="TAX BILL")
		{
			var product = {
								//Product Details
								"ProductName": selectedProduct,
								"ACC1":{"PARCEL":$.trim($("#txtLookupAccount1").val())},
								"ACC2":{"TAX YEAR":$.trim($("#txtLookupAccount2").val())},
								"ACC3":{"OWNER NAME":$.trim($("#txtLookupAccount3").val())},								
								
								//Property Details
								"Address1":  $.trim($("#txtPropAddress1").val()),
								"Address2":  $.trim($("#txtPropAddress2").val()),
								"City":  $.trim($("#txtPropCity").val()),
								"State":  $.trim($("#ddlPropState").val()),
								"Zip":  $.trim($("#txtPropZip").val()),								
								
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
								"ACC1":{"LECENSE NUMBER":$.trim($("#txtLookupAccount1").val())},
								"ACC2": null,
								"ACC3":null,	
								
								//Property Details
								"Address1":  $.trim($("#txtPropAddress1").val()),
								"Address2":  $.trim($("#txtPropAddress2").val()),
								"City":  $.trim($("#txtPropCity").val()),
								"State":  $.trim($("#ddlPropState").val()),
								"Zip":  $.trim($("#txtPropZip").val()),								
								
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