var dbObject={
	"Products":null,
	"ContactInfo":null,
	"BillingDetails":null,
	"ConfirmEmail":null,
	"CustomerName":"Duval Country Tax Collector",
	"Breadcrumb":breadcrumbs
}
//Products contains 
//1. Product Details
//2. Property Address
//3. Amount
var products = [];
var contactInfo = null;
var billingDetails=null;

var breadcrumbs=[];

function UpdateDbObject(){
	dbObject.Products=products;	
	dbObject.ContactInfo=contactInfo;	
	dbObject.BillingDetails=billingDetails;	
}

function SerializeDbObject(){
	return  JSON.stringify(dbObject);
}
function DeSrializeDbObject(json){
	debugger;
	var obj=jQuery.parseJSON(json)
	dbObject.Products=obj.Products;
	dbObject.ContactInfo=obj.ContactInfo;
	dbObject.BillingDetails=obj.BillingDetails;
	dbObject.ConfirmEmail=obj.ConfirmEmail;
	dbObject.Breadcrumb=obj.Breadcrumb;
	
	products=obj.Products;
	contactInfo = obj.ContactInfo;
	billingDetails=obj.BillingDetails;
	breadcrumbs=obj.Breadcrumb;
}
function clearDbObject(){
	dbObject.Products=null;
	dbObject.ContactInfo=null;
	dbObject.BillingDetails=null;
}
function getParameterByName( name ){
    var regexS = "[\\?&]"+name+"=([^&#]*)", 
  regex = new RegExp( regexS ),
  results = regex.exec( window.location.search );
  if( results == null ){
    return "";
  } else{
    return decodeURIComponent(results[1].replace(/\+/g, " "));
  }
}
 function getKeyFromJson(JsonObj){
		for (var key in JsonObj) {
		    return  key;
		   }
	}
function getValueFromJson(JsonObj){
	debugger;
		for (var key in JsonObj) {
		    return JsonObj[key];
		}	
	}
function redirect(url){
	var baseUrl=window.location.href.split('/').slice(0,-1).join("/");
	window.location.href=baseUrl+"/"+url;
}
function isNumberKey(evt){
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode == 46){
        var inputValue = $("#floor").val();
        var count = (inputValue.match(/'.'/g) || []).length;
        if(count<1){
            if (inputValue.indexOf('.') < 1){
                return true;
            }
            return false;
        }else{
            return false;
        }
    }
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)){
        return false;
    }
    return true;
}
/*
//Menu
function getMenuItem(Page){
	var html='';
	for(i=0;i<dbObject.Breadcrumb.length;i++){
		if(dbObject.Breadcrumb[i]!=null){
			
				if(page==dbObject.Breadcrumb[i].Page)
				{
					html+='<li class="'+dbObject.Breadcrumb[i].LiClass+'"><a href="#" class='+dbObject.Breadcrumb[i].anchorClass+'"><span class="'+dbObject.Breadcrumb[i].SpanClass+'">';
			    	html+=dbObject.Breadcrumb[i].Index+'</span> Account <span class="hidden-xs hidden-sm">Details</span></a></li>';
				}
				return html;
			}
		else{
			switch(page)
			{
				case 'Home':
				
		 			var item={"Page":"Home","Index":"1","LiClass":"active","SpanClass":"badge badge-inverse","anchorClass":"","OnClick":"onclick=breadcrumbRedirect('Home');"};
		 			
		 			dbObject.Breadcrumb.push(item);	
		 			return html;	
				break;
				case 'ContactInfo':
				
		 			var item={"Page":"Home","Index":"1","LiClass":"active","SpanClass":"badge badge-inverse","anchorClass":"","OnClick":"onclick=breadcrumbRedirect('Home');"};
		 			
		 			dbObject.Breadcrumb.push(item);	
		 			return html;	
				break;
				
		}	
			
		}
}

function getPageIndex(Page){
	for(i=0;i<dbObject.Breadcrumb.length;i++){
		if(Page==dbObject.Breadcrumb.Page)
			return (i+1);
	}
}
function populateBreadcrumb(){
	  
    var page=window.location.href.split('/').slice(0,-1).replace('.html','');
    
    if(!(dbObject.Breadcrumb.length>0)){
	    var menuItem={"Page":"Home","Index":"1","Visited":"N"};
	    dbObject.Breadcrumb.push(menuItem);
	    menuItem={"Page":"ContactInfo","Index":"2","Visited":"N"};
	    dbObject.Breadcrumb.push(menuItem);
	    menuItem={"Page":"Payment","Index":"3","Visited":"N"};
	    dbObject.Breadcrumb.push(menuItem);
	    menuItem={"Page":"PaymentConfirm","Index":"4","Visited":"N"};
	    dbObject.Breadcrumb.push(menuItem);		
	}
    
    for(i=0;i<dbObject.Breadcrumb.length;i++){
			
				if(page==dbObject.Breadcrumb[i].Page)
				{
					dbObject.Breadcrumb[i].Visited='Y';
				}
		
	}
    
    var html=' <ul class="breadcrumb">';
    for(i=0;i<dbObject.Breadcrumb.length;i++){
			if(dbObject.Breadcrumb[i].Visited=='Y')
				{
						html+='<li class="active"><a href="#" class="" onclick="breadcrumbRedirect('+dbObject.Breadcrumb[i].Page+');"><span class="badge badge-inverse">';
			    	html+=dbObject.Breadcrumb[i].Index+'</span> Account <span class="hidden-xs hidden-sm">Details</span></a></li>';
				}
			else{
				if(parseInt(dbObject.Breadcrumb[i].Index)<=getPageIndex(page)){
					
				}
				else{
					
				}
			}
		
	}
		
	    
	    var html+='<li><a href="https://betapay.bill2pay.com/pay/#" class="inactiveLink"><span class="badge">';
	    var html+=' 2</span><span class="hidden-xs hidden-sm"> Contact Info</span></a></li>';
	    var html+='<li><a href="https://betapay.bill2pay.com/pay/#" class="inactiveLink"><span class="badge">';
	    var html+='3</span><span class="hidden-xs hidden-sm"> Payment Details</span></a></li>';
		var html+='<li><a href="https://betapay.bill2pay.com/pay/#" class="inactiveLink"><span class="badge">';
		var html+='4</span><span class="hidden-xs hidden-sm"> Confirm Payment</span></a></li>';
	    var html+='</ul>';
	}
	
}
*/
  function breadcrumbRedirect(Page){
  	debugger;
	var json=SerializeDbObject();	
	redirect(Page+'.html?dbObject=' + json);		
}