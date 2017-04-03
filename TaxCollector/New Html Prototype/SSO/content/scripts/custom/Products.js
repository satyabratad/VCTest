
var bill2payProducts = {

 getProducts: function (json) {
       debugger;
       if(json!=null)
       DeSrializeDbObject(json);


		if(selectedProduct.toUpperCase()=="TAX BILL")
		{
			var product = {
								//Product Details
								"ProductName": 'Tax Bill',
								"ACC1":{"PARCEL":$("#txtLookupAccount1").val()},
								"ACC2":{"TAX YEAR":$("#txtLookupAccount2").val()},
								"ACC3":{"OWNER NAME":$("#txtLookupAccount3").val()},

								//Property Details
                                "Name": {"NAME":'CHRISTOPHER P. KALIL'},
								"Address1":  {"ADDRESS1":'1135 RIVERMONT DR'},
								//Amount
								"AmountDue":32.33,
								"Amount":{"AMOUNT":32.33}
						  };

			return product;

		}
		else if(selectedProduct.toUpperCase()=="DMV APPLICATION")
		{
			var product = {
								//Product Details
								"ProductName": 'Tax Bill',
								"ACC1":{"PARCEL":$("#txtLookupAccount1").val()},
								"ACC2":{"TAX YEAR":$("#txtLookupAccount2").val()},
								"ACC3":{"OWNER NAME":$("#txtLookupAccount3").val()},


								//Property Details
                                "Name": {"NAME":'STEPHEN S. HOOPER'},
								"Address1":  {"ADDRESS1":'1145 PARKROAD DR'},
								//Amount
								"AmountDue":29.21,
								"Amount":{"AMOUNT":29.21}
						  };

			return product;

		}

    },
    addProducts: function (json) {
       var product1 = {
							//Product Details
							"ProductName": 'Tax Bill',
							"ACC1":{"PARCEL":"Parcel 1"},
								"ACC2":{"TAX YEAR":"2016"},
								"ACC3":{"OWNER NAME":"John Smith"},


							//Property Details
                            "Name": {"NAME":'CHRISTOPHER P. KALIL'},
							"Address1":  {"ADDRESS1":'1135 RIVERMONT DR'},
							//Amount
							"AmountDue":32.33,
							"AmountPaid":0,
							"Amount":{"AMOUNT":32.33}
						};

		products.push(product1);


		var product2 = {
							//Product Details
							"ProductName": 'Tax Bill',
							"ACC1":{"PARCEL":"Parcel 2"},
							"ACC2":{"TAX YEAR":"2016"},
							"ACC3":{"OWNER NAME":"Jacob Turner"},


							//Property Details
                            "Name": {"NAME":'STEPHEN S. HOOPER'},
							"Address1":  {"ADDRESS1":'1145 PARKROAD DR'},
							//Amount
							"AmountDue":29.21,
							"AmountPaid":0,
							"Amount":{"AMOUNT":29.21}
						};

		products.push(product2);

		UpdateDbObject();
    },
	 removeProduct: function (itemIndex) {
	 	debugger;
		products.splice( itemIndex, 1 );
		UpdateDbObject();
    },
    getCartTotalAmount: function (){
    debugger;
    	var totalAmount=0;
		for (var i = 0; i < dbObject.Products.length; i++) {
				totalAmount=(parseFloat(totalAmount)+parseFloat(bill2payAccountDetails.getItemAmount(i))).toFixed(2);
			}
			return '$'+totalAmount;
	},

};