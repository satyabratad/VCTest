var dbObject={
	"Products":null,
	"ContactInfo":null,
	"BillingDetails":null,
	"ConfirmEmail":null,
	"CustomerName":"City of Melbourne",
	"Breadcrumb":null
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
	dbObject.Breadcrumb=breadcrumbs;
}

function SerializeDbObject(){
	return  JSON.stringify(dbObject);
}
function DeSrializeDbObject(json){
	debugger;
    try{
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
    catch(err){
    }
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
//Menu

function getPageIndex(Page){
	for(i=0;i<breadcrumbs.length;i++){
		if(Page.toUpperCase()==breadcrumbs[i].Page.toUpperCase())
			return i;
	}
}
function populateBreadcrumb(){
	  debugger;
    var page=window.location.href.split('/').slice(-1).pop().replace('.html','');
    if(page.indexOf('?')>=0){
		page=page.split('?')[0];
	}
    
    if(dbObject.Breadcrumb.length==0){
	    var menuItem={"Page":"HomeLookUp","MENUNAME":"Account","SPANHIDDEN":"Details","Index":"1","Visited":"N"};
	    breadcrumbs.push(menuItem);
	    menuItem={"Page":"ContactInfoLookup","MENUNAME":"Contact","SPANHIDDEN":"Info","Index":"2","Visited":"N"};
	    breadcrumbs.push(menuItem);
	    menuItem={"Page":"PaymentLookup","MENUNAME":"Payment","SPANHIDDEN":"Details","Index":"3","Visited":"N"};
	    breadcrumbs.push(menuItem);
	    menuItem={"Page":"PaymentConfirmLookup","MENUNAME":"Confirm","SPANHIDDEN":"Payment","Index":"4","Visited":"N"};
	    breadcrumbs.push(menuItem);	
	    	
	}
	
	breadcrumbs[getPageIndex(page)].Visited='Y';
	UpdateDbObject();
    
    var html=' <ul class="breadcrumb">';
    var itemIndex=0;
    for(i=0;i<dbObject.Breadcrumb.length;i++){
    	
    	if(itemIndex<dbObject.Breadcrumb.length)
    	{
			
				if(dbObject.Breadcrumb[itemIndex].Visited=='Y')
				{
					html+='<li class="active"><a href="#" class="" onclick="breadcrumbRedirect(\''+dbObject.Breadcrumb[itemIndex].Page+'\');"><span class="badge badge-inverse">';
				    	html+=dbObject.Breadcrumb[itemIndex].Index+'</span> '+dbObject.Breadcrumb[itemIndex].MENUNAME+' <span class="hidden-xs hidden-sm">'+dbObject.Breadcrumb[itemIndex].SPANHIDDEN+'</span></a></li>';
				}
				else
				{
					if(parseInt(dbObject.Breadcrumb[itemIndex].Index)<=getPageIndex(page))
					{
							html+='<li class="active"><a href="#" class="" onclick="breadcrumbRedirect('+dbObject.Breadcrumb[itemIndex].Page+');"><span class="badge badge-inverse">';
						    	html+=dbObject.Breadcrumb[itemIndex].Index+'</span> '+dbObject.Breadcrumb[itemIndex].MENUNAME+' <span class="hidden-xs hidden-sm">'+dbObject.Breadcrumb[itemIndex].SPANHIDDEN+'</span></a></li>';
					}
					else
					{
							html+='<li class=""><a href="#" class="inactiveLink"><span class="badge">';
						    	html+=dbObject.Breadcrumb[itemIndex].Index+'</span> '+dbObject.Breadcrumb[itemIndex].MENUNAME+' <span class="hidden-xs hidden-sm">'+dbObject.Breadcrumb[itemIndex].SPANHIDDEN+'</span></a></li>';
					}
	    		}
	    		itemIndex+=1;
	    }		
	    	
	}
	html+='</ul>';
		$('#brdCrumb').html('');
	    $('#brdCrumb').append(html);
		
	  /*  
	    var html+='<li><a href="https://betapay.bill2pay.com/pay/#" class="inactiveLink"><span class="badge">';
	    var html+=' 2</span><span class="hidden-xs hidden-sm"> Contact Info</span></a></li>';
	    var html+='<li><a href="https://betapay.bill2pay.com/pay/#" class="inactiveLink"><span class="badge">';
	    var html+='3</span><span class="hidden-xs hidden-sm"> Payment Details</span></a></li>';
		var html+='<li><a href="https://betapay.bill2pay.com/pay/#" class="inactiveLink"><span class="badge">';
		var html+='4</span><span class="hidden-xs hidden-sm"> Confirm Payment</span></a></li>';
	    var html+='</ul>';
	   */ 
	}
	
  function breadcrumbRedirect(Page){
  	debugger;
	var json=SerializeDbObject();	
	redirect(Page+'.html?dbObject=' + json);		
}