
var bill2payProducts = {

 getProducts: function (json=null) {
       debugger;
       if(json!=null)
       DeSrializeDbObject(json);
       
		var selectedProduct=$("#ddlCategories").val();
		
		if(selectedProduct.toUpperCase()=="TEST & NAME")
		{
			var product = {
								//Product Details
								"ProductName": selectedProduct,
								"ACC1":{"ACCOUNT1":$("#txtLookupAccount1").val()},
								"ACC2":{"ACCOUNT2":$("#txtLookupAccount2").val()},
																
								
								//Property Details
                                "Name": {"NAME":'CHRISTOPHER P. KALIL'},
								"Address1":  {"ADDRESS1":'1135 RIVERMONT DR'},
								//Amount
								"Amount":{"AMOUNT":32.33}
						  };
			
			return product;
			
		}
		else if(selectedProduct.toUpperCase()=="UTILITY PAYMENT")
		{
			var product = {
								//Product Details
								"ProductName": selectedProduct,
								"ACC1":{"ACCOUNT1":$("#txtLookupAccount1").val()},
								"ACC2":{"ACCOUNT2":$("#txtLookupAccount2").val()},
								
								
								//Property Details
                                "Name": {"NAME":'STEPHEN S. HOOPER'},
								"Address1":  {"ADDRESS1":'1145 PARKROAD DR'},
								//Amount
								"Amount":{"AMOUNT":29.21}
						  };
			
			return product;
			
		}
		
    },
    addProducts: function (json=null) {
       debugger;
       if(json!=null)
       DeSrializeDbObject(json);
       
		var selectedProduct=$("#ddlCategories").val();
		
		if(selectedProduct.toUpperCase()=="TEST & NAME")
		{
			var product = {
								//Product Details
								"ProductName": selectedProduct,
								"ACC1":{"ACCOUNT1":$("#txtLookupAccount1").val()},
								"ACC2":{"ACCOUNT2":$("#txtLookupAccount2").val()},
																
								
								//Property Details
                                "Name": {"NAME":'CHRISTOPHER P. KALIL'},
								"Address1":  {"ADDRESS1":'1135 RIVERMONT DR'},
								//Amount
								"Amount":{"AMOUNT":32.33}
						  };
			
			products.push(product);
			
		}
		else if(selectedProduct.toUpperCase()=="UTILITY PAYMENT")
		{
			var product = {
								//Product Details
								"ProductName": selectedProduct,
								"ACC1":{"ACCOUNT1":$("#txtLookupAccount1").val()},
								"ACC2":{"ACCOUNT2":$("#txtLookupAccount2").val()},
								
								
								//Property Details
                                "Name": {"NAME":'STEPHEN S. HOOPER'},
								"Address1":  {"ADDRESS1":'1145 PARKROAD DR'},
								//Amount
								"Amount":{"AMOUNT":29.21}
						  };
			
			products.push(product);
			
		}
		UpdateDbObject();
    },
	 removeProduct: function (itemIndex) {
	 	debugger;
		dbObject.Products.splice( itemIndex, 1 );
		UpdateDbObject();
    },
    getCartTotalAmount: function (){
    debugger;
    	var totalAmount=0;
		for (var i = 0; i < dbObject.Products.length; i++) {
				totalAmount=(parseFloat(totalAmount)+parseFloat(getValueFromJson(dbObject.Products[i].Amount))).toFixed(2);
			}	
			return '$'+totalAmount;
	},
	
};