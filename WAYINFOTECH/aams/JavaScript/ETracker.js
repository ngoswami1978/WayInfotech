//Start of Global Function ------------------------------------------
function isDate(val,format) {
	var strdate=getDateFromFormat(val,format);
	if (strdate==0) { return false; }
	return true;
	}

// -------------------------------------------------------------------
// compareDates(date1,date1format,date2,date2format)
//   Compare two date strings to see which is greater.
//   Returns:
//   1 if date1 is greater than date2
//   0 if date2 is greater than date1 of if they are the same
//  -1 if either of the dates is in an invalid format
//   2 if both are same.
// -------------------------------------------------------------------
function compareDates(date1,dateformat1,date2,dateformat2) {

	var d1=getDateFromFormat(date1,dateformat1);
	var d2=getDateFromFormat(date2,dateformat2);
	if (d1==0 || d2==0) {
		return -1;
		}
	else if (d1 > d2) {
		return 1;
		}
	else if (d1 == d2) {
		return 2;
		}
	return 0;
	}
// -------------------------------------------------------------------
// compareFinancialDates(startdate,dateformat1,enddate,dateformat2,currentdate,dateformat3)
//   Verify date3 string to see whether it lies in between date1 and date2.
//   Returns:
//   1 if date3 lies in between date1 and date2
//   0 if date3  not lies in between date1 and date2
// -------------------------------------------------------------------	
function compareFinancialDates(startdate,dateformat1,enddate,dateformat2,currentdate,dateformat3) 
{
 	var d1=getDateFromFormat(startdate,dateformat1);
	var d2=getDateFromFormat(enddate,dateformat2);
	var d3=getDateFromFormat(currentdate,dateformat3);
	if (d3 >= d1 && d3 <= d2) 
	{
		return 1;
	}
	else return 0;
}

// ------------------------------------------------------------------
// formatDate (date_object, format)
// Returns a date in the output format specified.
// The format string uses the same abbreviations as in getDateFromFormat()
// ------------------------------------------------------------------
function formatDate(date,format) {
	format=format+"";
	var result="";
	var i_format=0;
	var c="";
	var token="";
	var y=date.getYear()+ "";
	var M=date.getMonth()+1;
	var d=date.getDate();
	var E=date.getDay();
	var H=date.getHours();
	var m=date.getMinutes();
	var s=date.getSeconds();
	var yyyy,yy,MMM,MM,dd,hh,h,mm,ss,ampm,HH,H,KK,K,kk,k;
	// Convert real date parts into formatted versions
	var value=new Object();
	if (y.length < 4) {y=""+(y-0+1900);}
	value["y"]=""+y;
	value["yyyy"]=y;
	value["yy"]=y.substring(2,4);
	value["M"]=M;
	value["MM"]=LZ(M);
	value["MMM"]=MONTH_NAMES[M-1];
	value["NNN"]=MONTH_NAMES[M+11];
	value["d"]=d;
	value["dd"]=LZ(d);
	value["E"]=DAY_NAMES[E+7];
	value["EE"]=DAY_NAMES[E];
	value["H"]=H;
	value["HH"]=LZ(H);
	if (H==0){value["h"]=12;}
	else if (H>12){value["h"]=H-12;}
	else {value["h"]=H;}
	value["hh"]=LZ(value["h"]);
	if (H>11){value["K"]=H-12;} else {value["K"]=H;}
	value["k"]=H+1;
	value["KK"]=LZ(value["K"]);
	value["kk"]=LZ(value["k"]);
	if (H > 11) { value["a"]="PM"; }
	else { value["a"]="AM"; }
	value["m"]=m;
	value["mm"]=LZ(m);
	value["s"]=s;
	value["ss"]=LZ(s);
	while (i_format < format.length) {
		c=format.charAt(i_format);
		token="";
		while ((format.charAt(i_format)==c) && (i_format < format.length)) {
			token += format.charAt(i_format++);
			}
		if (value[token] != null) { result=result + value[token]; }
		else { result=result + token; }
		}
	return result;
	}
	
// ------------------------------------------------------------------
// Utility functions for parsing in getDateFromFormat()
// ------------------------------------------------------------------
function _isInteger(val) {
	var digits="1234567890";
	for (var i=0; i < val.length; i++) {
		if (digits.indexOf(val.charAt(i))==-1) { return false; }
		}
	return true;
	}
function _getInt(str,i,minlength,maxlength) {
	for (var x=maxlength; x>=minlength; x--) {
		var token=str.substring(i,i+x);
		if (token.length < minlength) { return null; }
		if (_isInteger(token)) { return token; }
		}
	return null;
	}
	
// ------------------------------------------------------------------
// getDateFromFormat( date_string , format_string )
//
// This function takes a date string and a format string. It matches
// If the date string matches the format string, it returns the 
// getTime() of the date. If it does not match, it returns 0.
// ------------------------------------------------------------------
function getDateFromFormat(val,format) {
	val=val+"";
	format=format+"";
	var i_val=0;
	var i_format=0;
	var c="";
	var token="";
	var token2="";
	var x,y;
	var now=new Date();
	var year=now.getYear();
	var month=now.getMonth()+1;
	var date=1;
	var hh=now.getHours();
	var mm=now.getMinutes();
	var ss=now.getSeconds();
	var ampm="";
	
	while (i_format < format.length) {
		// Get next token from format string
		c=format.charAt(i_format);
		token="";
		while ((format.charAt(i_format)==c) && (i_format < format.length)) {
			token += format.charAt(i_format++);
			}
		// Extract contents of value based on format token
		if (token=="yyyy" || token=="yy" || token=="y") {
			if (token=="yyyy") { x=4;y=4; }
			if (token=="yy")   { x=2;y=2; }
			if (token=="y")    { x=2;y=4; }
			year=_getInt(val,i_val,x,y);
			if (year==null) { return 0; }
			i_val += year.length;
			if (year.length==2) {
				if (year > 70) { year=1900+(year-0); }
				else { year=2000+(year-0); }
				}
			}
		else if (token=="MMM"||token=="NNN"){
			month=0;
			for (var i=0; i<MONTH_NAMES.length; i++) {
				var month_name=MONTH_NAMES[i];
				if (val.substring(i_val,i_val+month_name.length).toLowerCase()==month_name.toLowerCase()) {
					if (token=="MMM"||(token=="NNN"&&i>11)) {
						month=i+1;
						if (month>12) { month -= 12; }
						i_val += month_name.length;
						break;
						}
					}
				}
			if ((month < 1)||(month>12)){return 0;}
			}
		else if (token=="EE"||token=="E"){
			for (var i=0; i<DAY_NAMES.length; i++) {
				var day_name=DAY_NAMES[i];
				if (val.substring(i_val,i_val+day_name.length).toLowerCase()==day_name.toLowerCase()) {
					i_val += day_name.length;
					break;
					}
				}
			}
		else if (token=="MM"||token=="M") {
			month=_getInt(val,i_val,token.length,2);
			if(month==null||(month<1)||(month>12)){return 0;}
			i_val+=month.length;}
		else if (token=="dd"||token=="d") {
			date=_getInt(val,i_val,token.length,2);
			if(date==null||(date<1)||(date>31)){return 0;}
			i_val+=date.length;}
		else if (token=="hh"||token=="h") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<1)||(hh>12)){return 0;}
			i_val+=hh.length;}
		else if (token=="HH"||token=="H") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<0)||(hh>23)){return 0;}
			i_val+=hh.length;}
		else if (token=="KK"||token=="K") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<0)||(hh>11)){return 0;}
			i_val+=hh.length;}
		else if (token=="kk"||token=="k") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<1)||(hh>24)){return 0;}
			i_val+=hh.length;hh--;}
		else if (token=="mm"||token=="m") {
			mm=_getInt(val,i_val,token.length,2);
			if(mm==null||(mm<0)||(mm>59)){return 0;}
			i_val+=mm.length;}
		else if (token=="ss"||token=="s") {
			ss=_getInt(val,i_val,token.length,2);
			if(ss==null||(ss<0)||(ss>59)){return 0;}
			i_val+=ss.length;}
		else if (token=="a") {
			if (val.substring(i_val,i_val+2).toLowerCase()=="am") {ampm="AM";}
			else if (val.substring(i_val,i_val+2).toLowerCase()=="pm") {ampm="PM";}
			else {return 0;}
			i_val+=2;}
		else {
			if (val.substring(i_val,i_val+token.length)!=token) {return 0;}
			else {i_val+=token.length;}
			}
		}
	// If there are any trailing characters left in the value, it doesn't match
	if (i_val != val.length) { return 0; }
	// Is date valid for month?
	if (month==2) {
		// Check for leap year
		if ( ( (year%4==0)&&(year%100 != 0) ) || (year%400==0) ) { // leap year
			if (date > 29){ return 0; }
			}
		else { if (date > 28) { return 0; } }
		}
	if ((month==4)||(month==6)||(month==9)||(month==11)) {
		if (date > 30) { return 0; }
		}
	// Correct hours value
	if (hh<12 && ampm=="PM") { hh=hh-0+12; }
	else if (hh>11 && ampm=="AM") { hh-=12; }
	var newdate=new Date(year,month-1,date,hh,mm,ss);
	return newdate.getTime();
	}

// ------------------------------------------------------------------
// parseDate( date_string [, prefer_euro_format] )
//
// This function takes a date string and tries to match it to a
// number of possible date formats to get the value. It will try to
// match against the following international formats, in this order:
// y-M-d   MMM d, y   MMM d,y   y-MMM-d   d-MMM-y  MMM d
// M/d/y   M-d-y      M.d.y     MMM-d     M/d      M-d
// d/M/y   d-M-y      d.M.y     d-MMM     d/M      d-M
// A second argument may be passed to instruct the method to search
// for formats like d/M/y (european format) before M/d/y (American).
// Returns a Date object or null if no patterns match.
// ------------------------------------------------------------------
function parseDate(val) {
	var preferEuro=(arguments.length==2)?arguments[1]:false;
	generalFormats=new Array('y-M-d','MMM d, y','MMM d,y','y-MMM-d','d-MMM-y','MMM d','ddMMMyyyy','yyyyMMdd');
	monthFirst=new Array('M/d/y','M-d-y','M.d.y','MMM-d','M/d','M-d');
	dateFirst =new Array('d/M/y','d-M-y','d.M.y','d-MMM','d/M','d-M');
	var checkList=new Array('generalFormats',preferEuro?'dateFirst':'monthFirst',preferEuro?'monthFirst':'dateFirst');
	var d=null;
	for (var i=0; i<checkList.length; i++) {
		var l=window[checkList[i]];
		for (var j=0; j<l.length; j++) {
			d=getDateFromFormat(val,l[j]);
			if (d!=0) { return new Date(d); }
			}
		}
	return null;
	}

// -------------------------------------------------------------------
// compareYearDates(date1,date1format,date2,date2format)
//   Compare two date strings to see which is greater.
//   Returns:
//   1 if date1 is greater than current date
//   else 0 
// -------------------------------------------------------------------
function compareYearDates(date1) 
{
    var dateToCheck = new Date();
	var d1=parseDate(date1);
	var db = days_between(dateToCheck,d1);
	if (db > 360)
	{
	    return 1;
	}
    return 0;
}

function days_between(date1, date2) 
{
    // The number of milliseconds in one day
    var ONE_DAY = 1000 * 60 * 60 * 24
    // Convert both dates to milliseconds
    var date1_ms = date1.getTime()
    var date2_ms = date2.getTime()
    // Calculate the difference in milliseconds
    var difference_ms = Math.abs(date1_ms - date2_ms)
    // Convert back to days and return
    return Math.round(difference_ms/ONE_DAY)
}

/*************************************************************************
    Function for confirm delete
*************************************************************************/

function ConfirmDelete()
{
  if (confirm("Are you sure you want to delete?")==true)
    return true;
  else
    return false;
}

function IsDataValid(datavalue,type)
{
/*
type
1 Alphabets without space
2 Alphabets with space
3 Numeric without sign and dot
4 Numeric with sign and dot
5 Numeric with dot
6 Alphabets & Numeric
7 Alphabets & Numeric with Space
8 Phone Number 
9 Alphabete with ( ),-,& and Space 

*/
if(datavalue!='')
{
    datalength = datavalue.length;
    x = datavalue;
    flag=0;
    for(p=0;p < datalength;p++)
    {
        vAscii = x.charCodeAt(p) 
        if (type == 1)
        {
        if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122))
            {flag=1;}
        else
            {flag=0;break;}
        }

        if (type == 2)
        {
            if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) || (vAscii == 32))
                {flag=1;}
            else
                {flag=0;break;}
        }

        if (type == 3)
        {
            if((vAscii >= 48 && vAscii <= 57))
                {flag=1;}
            else
                {flag=0;break;}
        }

        if (type == 4)
        {
            if((vAscii >= 48 && vAscii <= 57) || (vAscii >= 45 && vAscii <= 46))
                {flag=1;}
            else
                {flag=0;break;}
        }

        if (type == 5)
        {
            if((vAscii >= 48 && vAscii <= 57) || (vAscii == 46))
                {flag=1;}
            else
                {flag=0;break;}
        }
        if (type == 6)
        {
            if((vAscii >= 48 && vAscii <= 57) || (vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122))
                {flag=1;}
            else
                {flag=0;break;}
        }
        if(type==7)
        {
            if((vAscii >= 48 && vAscii <= 57) || (vAscii >= 45 && vAscii <= 46) || (vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) || (vAscii == 32))
                {flag=1;}
            else
                {flag=0; break;}
        }
        if(type==8)
       {
      
            if((vAscii >= 48 && vAscii <= 57 ) ||vAscii == 43 || vAscii == 45)
               
                {flag=1;}
            else
                {flag=0;break;}
        }
       if(type==9)
       {
      
            if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) || (vAscii == 32) || (vAscii == 40) || (vAscii == 41) || (vAscii == 38) || (vAscii == 45)|| (vAscii >= 48 && vAscii <= 57))
                {flag=1;}
            else
                {flag=0;break;}
        }
        if (type == 10)
        {
            if((vAscii >= 50 && vAscii <= 57))
                {flag=1;}
            else
                {flag=0;break;}
        }
        if (type == 11)
        {
        if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) ||vAscii == 48)
            {flag=1;}
        else
            {flag=0;break;}
        }
        
        if (type == 12)     // Numeric Value with -ve 
        {
           if(vAscii >= 48 && vAscii <= 57)
                {flag=1;}
           else if (p==0 && x.charAt(p) == '-' && datalength > 1)
            {
               flag=1;
            }
            else
                {flag=0;break;}
        }
    }
}
else
{
    flag=1;
}
if(flag==0)
{return false;}
else
{return true;}
}


function getCurrentDateTime()
{
    
        var currentDate = new Date()
        var day = currentDate.getDate()
        var month = currentDate.getMonth()
        var year = currentDate.getFullYear()
        var hours = currentDate.getHours()
        var Seconds = currentDate.getSeconds()
        var minutes = currentDate.getMinutes()        
        var hours = currentDate.getHours()
        var minutes = currentDate.getMinutes()
        var suffix = "AM";
        var currentDateTime=""
        if (minutes < 10)
          minutes = "0" + minutes

        if (Seconds< 10)
          Seconds= "0" + Seconds
        
        if (hours >= 12)
        {
            suffix = "PM";
            hours = hours - 12;
        }
        if (hours == 0)
        {
            hours = 12;
        }
        month=month+1;
//6/18/2010 11:07:29 AM
        currentDateTime=month + "/" + day + "/" + year + " " + hours + ":" + minutes + ":" + Seconds + " " + suffix ;
        return currentDateTime;
        
    
}
/*************************************************************************
    Validation for Email Address
*************************************************************************/
function checkEmail(incomingString)
{
	if(incomingString.search(/[\_\-\d]*[A-Za-z]+[\w\_\-]*[\@][\d]*[A-Za-z]+[\w\_\-]*[\.][A-Za-z]+/g) == -1)
	{
		return false;
	}
	else
		return true;
}

function CheckEmailForAgency(incomingString)
{
//{debugger;}
      var strEmail ;
      var str1;
      var str2;
      strEmail = incomingString;
      str1="@";
      str2=".";
      if ((strEmail.indexOf(str1)==-1) || (strEmail.indexOf(str2)==-1) )
        {            
            return false;
        }  
        else
        {
            return true;
        }    
}



/*************************************************************************
    Validation for IP Address
*************************************************************************/
function isValidIPAddress(ipaddr) {
   var re = /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/;
   if (re.test(ipaddr)) {
      var parts = ipaddr.split(".");
      if (parseInt(parseFloat(parts[0])) == 0) { return false; }
      for (var i=0; i<parts.length; i++) {
         if (parseInt(parseFloat(parts[i])) > 255) { return false; }
      }
      return true;
   } else {
      return false;
   }
}

function checkMaxLength(objCtrlid,MaxAllowedLength)
{
var tempVal=document.getElementById(objCtrlid).value;
if (tempVal.length>MaxAllowedLength)
{
document.getElementById(objCtrlid).value=tempVal.substring(0,MaxAllowedLength);
}

}


function checknumeric(objCtrlid)
{
var tempVal=document.getElementById(objCtrlid).value;
var validVal="";
for(var i=0;i<tempVal.length;i++)
{
if((parseInt(tempVal.substr(i,1)))>= 0 || (parseInt(tempVal.substr(i,1))<= 9))
{validVal+=tempVal.substr(i,1);}else{ }
}
document.getElementById(objCtrlid).value=validVal;
}

function checknumericGreaterZero(objCtrlid)
{
var tempVal=document.getElementById(objCtrlid).value;
var validVal="";
    for(var i=0;i<tempVal.length;i++)
    {
        if((parseInt(tempVal.substr(i,1)))>= 0 || (parseInt(tempVal.substr(i,1))<= 9))
        {validVal+=tempVal.substr(i,1);}else{ }
    }

    if (validVal=="0")
    {
        validVal=""
        document.getElementById(objCtrlid).value=validVal;
        document.getElementById("lblError").innerText="Quantity must be greater than zero";
        return false;
    }
    else
    {
        document.getElementById(objCtrlid).value=validVal;
    }

}

function checknumericWithDot(objCtrlid)
{
var tempVal=document.getElementById(objCtrlid).value;
var validVal="";
for(var i=0;i<tempVal.length;i++)
{
if((parseInt(tempVal.substr(i,1)))>= 0 || (parseInt(tempVal.substr(i,1))<= 9) || tempVal.substr(i,1)=='.')
{validVal+=tempVal.substr(i,1);}else{ }
}
document.getElementById(objCtrlid).value=validVal;
}

function CheckNumericWithoutZero(objCtrlid)
{
var tempVal=document.getElementById(objCtrlid).value;
var validVal="";
for(var i=0;i<tempVal.length;i++)
{
if((parseInt(tempVal.substr(i,1)))>= 1 || (parseInt(tempVal.substr(i,1))<= 9))
{validVal+=tempVal.substr(i,1);}else{ }
}
document.getElementById(objCtrlid).value=validVal;
}

 function gotop(ddlname)
     {
    
     if (event.keyCode==46 )
     {
        document.getElementById(ddlname).selectedIndex=0;
     }
     }
     


   function allText()
    { 
    
           if ((event.keyCode>=97 && event.keyCode<=122) || (event.keyCode>=65 && event.keyCode<=90))
              event.returnValue = true; 
        else
            event.returnValue = false;      
    }
function allTextWithSpace()
    { 
    
           if ((event.keyCode>=97 && event.keyCode<=122) || (event.keyCode>=65 && event.keyCode<=90)||(event.keyCode==32 ))
              event.returnValue = true; 
        else
            event.returnValue = false;      
    }

String.prototype.ltrim = strltrim;

String.prototype.rtrim = strrtrim;

String.prototype.trim = strtrim;


function strltrim() 
{
    return this.replace(/^\s+/,'');
}

function strrtrim() {
    return this.replace(/\s+$/,'');
}
function strtrim() {
    return this.replace(/^\s+/,'').replace(/\s+$/,'');
}

//function to validate  dropdownlist

function ddlvalidate(ddlname)

{

    if(document.getElementById(ddlname).selectedIndex==0)

    {

   // alert('Please select option');

  //  document.getElementById(ddlname).focus();

    return false;

    }

    return true;

}

//function to validate textbox having alphanumeric values

function textbox(txtname)

{

        var tax = document.getElementById(txtname).value;

        tax = tax.trim();

	    if (tax == "")

	    {

		//    alert("Please enter value");

		//    document.getElementById(txtname).focus();

		    return false;

	    }

        return true;

}



String.prototype.ltrim = strltrim;

String.prototype.rtrim = strrtrim;

String.prototype.trim = strtrim;


function strltrim() 
{
    return this.replace(/^\s+/,'');
}

function strrtrim() {
    return this.replace(/\s+$/,'');
}
function strtrim() {
    return this.replace(/^\s+/,'').replace(/\s+$/,'');
}


//End of Global Function ------------------------------------------





//********************************************************************************************
//  Ecomm Trackers Help Desk





function PopupPageCallLog(id)
         {
         var type;
         if (id=="1")
         {   
             var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
            if (strEmployeePageName!="")
            {
               type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;            
   	            window.open(type,"aaCallLogEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1");         
   	         }   
          }
         if (id=="2")
         {
                type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aaCallLogAgency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
         }
    
         if (id=="3")
         {
                type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T";
   	            window.open(type,"aaCallLogAgencyStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
          }
     }
     
     
     
    function ValidateFormCallLog()
    {
  //  alert("ABHI");
    var strAgencyName=document.getElementById("txtAgencyName").value.trim();
    var strOfficeID=document.getElementById("txtOfficeID").value.trim();
    var strCUSTOMER_CATEGORY_ID=document.getElementById("ddlCustomerCategory").selectedIndex;
    var strLoggedBy=document.getElementById("txtLoggedBy").value.trim();
    var strCALLER_NAME=document.getElementById("txtCallerName").value.trim();
    var strHD_QUERY_GROUP_ID=document.getElementById("ddlQueryGroup").selectedIndex;
    var strCALL_SUB_GROUP_ID=document.getElementById("ddlQuerySubGroup").selectedIndex;
    var strCALL_CATEGORY_ID=document.getElementById("ddlQueryCategory").selectedIndex;
    var strCALL_SUB_CATEGORY_ID=document.getElementById("ddlQuerySubCategory").selectedIndex;
    var strHD_STATUS_ID=document.getElementById("ddlQueryStatus").selectedIndex;
    var strHD_SEVERITY_ID=document.getElementById("ddlQueryPriority").selectedIndex;
    var strCOORDINATOR1=document.getElementById("ddlCoordinator1").selectedIndex;
    var strCOORDINATOR2=document.getElementById("ddlCoordinator2").selectedIndex;
    var strAssignedDatetime=document.getElementById("txtDateAssigned").value.trim();
    var strDISPOSITION_ID=document.getElementById("ddlDisposition").selectedIndex;
    var strQueryOpenedDateFrom=document.getElementById("txtQueryOpenedDateFrom").value.trim();
    var strQueryOpenedDateTo=document.getElementById("txtQueryOpenedDateTo").value.trim();
    var strCloseDateFrom1=document.getElementById("txtCloseDateFrom1").value.trim();
    var strCloseDateTo1=document.getElementById("txtCloseDateTo1").value.trim();
    var strAOffice=document.getElementById("ddlAOffice").selectedIndex;
    var strAgencyAOffice=document.getElementById("ddlAgencyAOffice").selectedIndex;
    var strLTRNo=document.getElementById("txtLTRNo").value.trim();
    var strPTRNo=document.getElementById("txtPTRNo").value.trim();
    var strDisplayLastCall=document.getElementById("chkDisplayLastCall").checked;
    var strWorkOrderNo=document.getElementById("txtWorkOrderNo").value.trim();
    var strCity=document.getElementById("ddlCity").selectedIndex;
    var strState=document.getElementById("ddlState").selectedIndex;
    var strISIATA=document.getElementById("chkISIATA").checked;
    var strIATA=document.getElementById("txtIATA").value.trim();
    var strISPending=document.getElementById("chkISPending").checked;
    var strRegion=document.getElementById("ddlRegion").selectedIndex;
    var strAssignedTo=document.getElementById("txtAssignedTo").value.trim();
    var strContactType=document.getElementById("ddlContactType").selectedIndex;
    var strTitle=document.getElementById("txtTitle").value.trim();
   var strError=false;
   
   if (strDisplayLastCall==false && strISPending ==false)
   {
           if (strAgencyName=="" && strOfficeID=="" && strCUSTOMER_CATEGORY_ID=="0" && strLoggedBy=="" && strCALLER_NAME==""&& strHD_QUERY_GROUP_ID=="0" && strCALL_SUB_GROUP_ID=="0" && strCALL_CATEGORY_ID=="0")
           {
            if ( strCALL_SUB_CATEGORY_ID=="0" && strHD_STATUS_ID=="0" && strHD_SEVERITY_ID=="0" && strCOORDINATOR1=="0" &&  strCOORDINATOR2=="0" && strAssignedDatetime=="" && strDISPOSITION_ID=="0" && strQueryOpenedDateFrom=="" )
                { 
                    if (strQueryOpenedDateTo=="" && strCloseDateFrom1=="" && strCloseDateTo1=="" && strAOffice=="0" && strAgencyAOffice=="0"  && strLTRNo=="" && strPTRNo=="" && strWorkOrderNo=="" && strCity=="0" && strState=="0")
                     {
                        if( strIATA=="" &&  strRegion=="0" &&  strAssignedTo=="" && strContactType=="0" && strTitle=="")
                        {
                            strError=true;
                        }
                    }
                 }   
           }      
    }
    
    if (strError==true)
    {
                    document.getElementById("lblError").innerText="Opened Date From is mandatory";
                    document.getElementById("txtQueryOpenedDateFrom").focus();
                    return false;
    }
        var dtDateAssigned=document.getElementById("txtDateAssigned").value;
        
        if (dtDateAssigned.trim()!="")
        {
            if (isDate(dtDateAssigned.trim(),"dd/MM/yyyy")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtDateAssigned").focus();
                    return false;

            }
        }
        
        var dtOpenFrom=document.getElementById("txtQueryOpenedDateFrom").value;
        var dtOpenTo=document.getElementById("txtQueryOpenedDateTo").value;
    
    
     if (dtOpenFrom.trim()!="")
        {
            if (isDate(dtOpenFrom.trim(),"dd/MM/yyyy HH:mm")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtQueryOpenedDateFrom").focus();
                    return false;

            }
        }
        
         if (dtOpenTo.trim()!="")
        {
            if (isDate(dtOpenTo.trim(),"dd/MM/yyyy HH:mm")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtQueryOpenedDateTo").focus();
                    return false;
            }
      }
    
      document.getElementById('lblError').innerText=''            
   if (compareDates(document.getElementById('txtQueryOpenedDateFrom').value,"dd/MM/yyyy HH:mm",document.getElementById('txtQueryOpenedDateTo').value,"dd/MM/yyyy HH:mm")==1)
       {
            document.getElementById('lblError').innerText ='Open date to should be greater than or equal to open date from.';
            return false;
       }
       var dtFrom=document.getElementById("txtCloseDateFrom1").value;
       var dtTo=document.getElementById("txtCloseDateTo1").value;
        dtFrom=dtFrom.trim();
        dtTo=dtTo.trim();
        
         if (dtFrom.trim()!="")
        {
            if (isDate(dtFrom.trim(),"dd/MM/yyyy HH:mm")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtCloseDateFrom1").focus();
                    return false;

            }
        }
        
         if (dtTo.trim()!="")
        {
            if (isDate(dtTo.trim(),"dd/MM/yyyy HH:mm")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtCloseDateTo1").focus();
                    return false;
            }
        }
       if (compareDates(dtFrom,"dd/MM/yyyy HH:mm",dtTo,"dd/MM/yyyy HH:mm")==1)
       {
            document.getElementById('lblError').innerText ='Close date to should be greater than or equal to close date from.';
            return false;
       }
                     
       return true; 
        
    }
     
     function GetCallLogData()
       {
var strCallType="";
        var intRowID=1;
        document.getElementById("hdData").value=""
           for(i=0;i<document.forms[0].elements.length;i++) 
            {
            var elm = document.forms[0].elements[i]; 
                    if(elm.type == 'checkbox') 
                    {
                         var chkId=elm.id;
                         if (elm.checked == true && chkId.split("_")[2]=="chkSelect")
                         {                           
                            var gvname=chkId.split("_")[0];
                            var ctrlidname=chkId.split("_")[1];
                             intRowID=ctrlidname.substring(3)-1;
                             if (document.getElementById("hdData").value == "")
                             {
                                document.getElementById("hdData").value =document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value + "|" + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdLCode").value;
                                strCallType=document.getElementById('gvCallLog').rows[intRowID].cells[9].innerText;
                             }
                             else
                             {
                                document.getElementById("hdData").value = document.getElementById("hdData").value + "," + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value + "|" + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdLCode").value;
                                 if( strCallType != document.getElementById('gvCallLog').rows[intRowID].cells[9].innerText)
                               {
                                    document.getElementById("lblError").innerText="Select one query group only.";
                                    return false;    
                               }
                             }                         
                         }                      
                    }
            }
        
            var data= document.getElementById("hdData").value;
            if(data=="")
            {
                document.getElementById("lblError").innerText="Checked atleast one checkbox";
                return false;            
            }
            else
            {
              window.location.href="HDUP_CallLog.aspx?Action=U&MULTIHD_RE_ID="+ data + "&strStatus=" + strCallType;  
              return false;     
            }
}

            
       function SelectAllCallLog() 
    {
       CheckAllDataGridCheckBoxesCallLog(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxesCallLog(value) 
    {
   
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
                var chkId=elm.id;
                if (chkId.split("_")[2]=="chkSelect")
                {
                    elm.checked = value
                }
            }
        }
    }
      
    
    
    function StatusChangeDataCallLog()
    {
   
        var strCallType="";
        var intRowID=1;
        document.getElementById("hdData").value=""
           for(i=0;i<document.forms[0].elements.length;i++) 
            {
            var elm = document.forms[0].elements[i]; 
                    if(elm.type == 'checkbox') 
                    {
                         var chkId=elm.id;
                         if (elm.checked == true && chkId.split("_")[2]=="chkSelect")
                         {                           
                            var gvname=chkId.split("_")[0];
                            var ctrlidname=chkId.split("_")[1];
                            intRowID=ctrlidname.substring(3)-1;
                             if (document.getElementById("hdData").value == "")
                             {
                                document.getElementById("hdData").value =document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                                strCallType=document.getElementById('gvCallLog').rows[intRowID].cells[9].innerText;
                             }
                             else
                             {
                                document.getElementById("hdData").value = document.getElementById("hdData").value + "," + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                               if( strCallType != document.getElementById('gvCallLog').rows[intRowID].cells[9].innerText)
                               {
                                    document.getElementById("lblError").innerText="Select one query group only.";
                                    return false;    
                               }
                             }                         
                         }                      
                    }
            }
        
            var data= document.getElementById("hdData").value;
            if(data=="")
            {
                document.getElementById("lblError").innerText="Checked atleast one checkbox";
                return false;            
            }
            else
            {
              var  type = "HDUP_ChangeStatus.aspx?Popup=T&HD_RE_ID=" + data + "&QueryGroup="+ strCallType;
   	          window.open(type,"Taa","height=400,width=900,top=30,left=20,scrollbars=1,status=1");	
                return false;     
            }
    
    }
    
    function ReturnDataCallLog()
       {
     

           for(i=0;i<document.forms[0].elements.length;i++) 
            {
            var elm = document.forms[0].elements[i]; 
                    if(elm.type == 'checkbox') 
                    {
                         var chkId=elm.id;
                         if (elm.checked == true && chkId.split("_")[2]=="chkSelect")
                         {                           
                            var gvname=chkId.split("_")[0];
                            var ctrlidname=chkId.split("_")[1];
                             if (document.getElementById("hdData").value == "")
                             {
                                document.getElementById("hdData").value =document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                             }
                             else
                             {
                                document.getElementById("hdData").value = document.getElementById("hdData").value + "," + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                             }                         
                         }                      
                    }
            }
        
            var data= document.getElementById("hdData").value;
            if(data=="")
            {
                document.getElementById("lblError").innerText="Checked atleast one checkbox";
                return false;            
            }
           else
           {
                 if (window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage']!=null)
                 {
                    if (window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage'].value=="")
                    {
                        window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage'].value=data;
                    }
                    else
                    {
                        window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage'].value=window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage'].value + "," + data;
                    }
                    window.opener.document.forms(0).submit();
                    window.close();
                    return false;
                 }
           }
       } 
  
  
   function ReLogFunctionCallLog(LCode,strStatus)
        {
            window.location.href="HDUP_CallLog.aspx?Action=U&LCode="+LCode + "&strStatus=" + strStatus;               
            return false;
        }
        
        function DeleteFunctionCallLog(HD_RE_ID)
        {
          
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteId").value=HD_RE_ID;       
           
           }
           else
           {
                document.getElementById("hdDeleteId").value="";
                 return false;
           }
        }  
        
          function EditFunctionCallLog(LCode,HD_RE_ID,strStatus)
        {           
          window.location.href="HDUP_CallLog.aspx?Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus;               
          return false;
        }
      
      
      
      
      
      
        function HideShowFunctional()
    {
   
    var strTabtype=document.getElementById("hdTabType").value;
    switch(strTabtype)
    {
    case "0":
            document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";
             break;
    case "1":
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";
             break;
     case "2":
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="block";
            document.getElementById("pnlFollowUp").style.display="none";
             break;
     case "3":
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="block";
             break;        
         
    }
    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
    {
        document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
    }
    
     switch(strTabtype)
    {
    case "0":
            if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
            {
                document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
            }
           
             break;
    case "1":
            if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
            {
                document.getElementById("theTabStrip_ctl01_Button1").className="headingtab"
            }
          
             break;
    case "2":
         if (document.getElementById('theTabStrip_ctl02_Button1').className != "displayNone")
            {
                document.getElementById("theTabStrip_ctl02_Button1").className="headingtab"
            }
           
             break;
    case "3":
            if (document.getElementById('theTabStrip_ctl03_Button1').className != "displayNone")
            {
                document.getElementById("theTabStrip_ctl03_Button1").className="headingtab"
            }
             break;
     default:
             if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
             {
              // document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
             }
             break;
     
             
   
    }
    }  
    
    function ColorMethodFunctional(id,total)
{   
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
            if (document.getElementById(Hcontrol).className != "displayNone")
            {
                document.getElementById(Hcontrol).className="headingtabactive";
            }
           
        }
       
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblPanelClick').value =id; 
       var pgStatus=document.getElementById('hdPageStatus').value;
       
    //   if(pgStatus=='U')
    //   {    
       var strHD_RE_ID =document.getElementById('hdEnPageHD_RE_ID').value;
       var strLCode=document.getElementById('hdEnPageLCode').value;
       if (id == (ctextFront +  "00" + ctextBack))
       {   
        
            document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";
            document.getElementById('rdFunctional').focus();
           document.getElementById('hdTabType').value='0';
         /*  if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="HDUP_CallLog.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }*/
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
           document.getElementById('hdTabType').value='1';
           document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";
            document.getElementById('txtDescription').focus();
       /*    if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_Description.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="HDUP_Description.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }*/
           
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
            document.getElementById('hdTabType').value='2';
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="block";
            document.getElementById("pnlFollowUp").style.display="none";
            document.getElementById('txtSolution').focus();
      /*      if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_Solution.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="HDUP_Solution.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }*/
         
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {
            document.getElementById('hdTabType').value='3';
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="block";
            document.getElementById('ddlMode').focus();
      /*      if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_FollowUp.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="HDUP_FollowUp.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }*/
            
            return false;
           
       }
       
        else if (id == (ctextFront +  "04" + ctextBack))
       {
       var LCode=document.getElementById("hdEnPageLCode").value;
       var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
       var strStatus=document.getElementById("hdEnFunctional").value;
       var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
       var AOFFICE=document.getElementById("hdEnAOffice").value;
       //Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus
            document.getElementById('hdTabType').value='4';
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";
            window.location.href="HDUP_LinkedLTR.aspx?Action=U&strStatus=" + strStatus + "&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;   
            return false;
           
       }
       
       else if (id == (ctextFront +  "05" + ctextBack))
       {
       var strStatus=document.getElementById("hdEnFunctional").value;
       var LCode=document.getElementById("hdEnPageLCode").value;
        var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
       var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
       var AOFFICE=document.getElementById("hdEnAOffice").value;
       //Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus
            document.getElementById('hdTabType').value='5';
         /*   document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";*/
            window.location.href="HDUP_helpDeskFeedBack.aspx?Action=U&strStatus=" + strStatus + "&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;   
            return false;
       }       
  
     //  }    
}


 function FillAgencyDetailsFunctional()
         {
        
           var officeId;
           officeId=  document.getElementById('txtOfficeId').value;
           var officeIdClassName=document.getElementById('txtOfficeId').className;
         
           if (officeId != ""  && officeIdClassName !="textboxgrey")
           {
                document.getElementById('txtAgencyName').value="Searching...";
                CallServerFunctional(officeId,"This is context from client");
           }
           else
           {
           /*
             document.getElementById('lblError').innerText ="";
                document.getElementById('hdEnAOffice').value="";
			    document.getElementById('hdAOffice').value="";
			    document.getElementById('hdCallAgencyName').value="";
			    document.getElementById('hdEnCallAgencyName_LCODE').value="";
			    document.getElementById('txtAgencyName').value="";
			    document.getElementById('txtAddress').value="";
			    document.getElementById('txtCity').value="";
			    document.getElementById('txtCountry').value="";
			    document.getElementById('txtPhone').value="";
			    document.getElementById('txtFax').value="";
			    document.getElementById('txtOnlineStatus').value="";
			    document.getElementById('txtPincode').value="";
			    document.getElementById('txtEmail').value="";
			     document.getElementById('TxtCompVertical').value="";
			    */
           }
           return false;
           }

function ReceiveServerDataFunctional(args, context)
        {
    
          document.getElementById('lblError').innerText ="";
          var obj = new ActiveXObject("MsXml2.DOMDocument");           
            
            if (args =="")
            {                            
                document.getElementById('hdEnAOffice').value="";
			    document.getElementById('hdAOffice').value="";
			    document.getElementById('hdCallAgencyName').value="";
			    document.getElementById('hdEnCallAgencyName_LCODE').value="";
			    document.getElementById('txtAgencyName').value="";
			    document.getElementById('txtAddress').value="";
			    document.getElementById('txtCity').value="";
			    document.getElementById('txtCountry').value="";
			    document.getElementById('txtPhone').value="";
			    document.getElementById('txtFax').value="";
			    document.getElementById('txtOnlineStatus').value="";
			    document.getElementById('txtPincode').value="";
			    document.getElementById('txtEmail').value="";
			    document.getElementById('txtAgencyName').value="";
			    document.getElementById('txtLoggedDate').value="";
			    document.getElementById('TxtCompVertical').value="";
			    
			             var s = $find("BhAutoCompleteExtenderForCaller");  
			              if (s!=null)
			              {			              
			                s._contextKey=" | ";
			              }
			   
            }
            else
            {
             var parts = args.split("$");
                 obj.loadXML(parts[0]);
             var dsRoot=obj.documentElement; 
            
			    if (dsRoot !=null)
			    {
			        
			            document.getElementById('hdEnAOffice').value=parts[1];
			            document.getElementById('hdAOffice').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Aoffice");
			            document.getElementById('hdCallAgencyName').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("LOCATION_CODE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NAME") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS") + "|" + "" + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CITY") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COUNTRY")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PHONE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("OfficeID")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("FAX") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ONLINE_STATUS") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Aoffice")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PINCODE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Email")
			            document.getElementById('hdEnCallAgencyName_LCODE').value=parts[2];
			            document.getElementById('txtAgencyName').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NAME")
			            document.getElementById('txtAddress').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS") + '\n ' + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS1")
			            document.getElementById('txtCity').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CITY")
			            document.getElementById('txtCountry').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COUNTRY")
			            document.getElementById('txtPhone').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PHONE")
			            document.getElementById('txtFax').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("FAX")
			            document.getElementById('txtOnlineStatus').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ONLINE_STATUS")
			            document.getElementById('txtPincode').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PINCODE")
			            document.getElementById('txtEmail').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Email")
			          
			            document.getElementById('TxtCompVertical').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COMP_VERTICAL")
			           
			           
			           //@ Added By Abhishek
			            try
			            { 		
			           // alert(document.getElementById('hdLoggedDatetime').value);	  
			            if ( document.getElementById('HdTechnicalLOgDateTime')  ==null)
			            {
			            // document.getElementById('txtLoggedDate').value=document.getElementById('hdLoggedDatetime').value;
			              document.getElementById('txtLoggedDate').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NEWLOGDATETIME");
			            }           
			             
			            
			              var s = $find("BhAutoCompleteExtenderForCaller");  
			              if (s!=null)
			              {
			                 var Lcode;
                             Lcode=document.getElementById ('hdCallAgencyName').value.split("|")[0];
                             s._contextKey= Lcode + "|" + document.getElementById('txtOfficeId').value;  
                          }
			          
			            }
                        catch(err){}			       
			    }            
            }
        }


 function fillCategoryFunctional()
   {
   
      document.getElementById('ddlQueryCategory').options.length=0;
      document.getElementById('ddlQuerySubCategory').options.length=0;
      var  id=document.getElementById('ddlQuerySubGroup').value;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="--Select One--";
	  document.getElementById('ddlQuerySubCategory').options[0]=new Option(names, codes );
	  var ddlQueryCategory = document.getElementById('ddlQueryCategory');
	  if (document.getElementById("hdCategory").value=="") 
            {
             listItem = new Option(names, codes );
             ddlQueryCategory.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(document.getElementById("hdCategory").value);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			     var item=document.getElementById("ddlQuerySubGroup").selectedIndex;
			     var text1=document.getElementById("ddlQuerySubGroup").options[item].text;		
			   // var orders = dsRoot.getElementsByTagName("CALL_CATEGORY[@CALL_SUB_GROUP_NAME='" + text1 + "']");
			     var orders = dsRoot.getElementsByTagName("CC[@CSGN='" + text1 + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQueryCategory.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
			        codes= orders[count].getAttribute("CCI"); 
			        names=orders[count].getAttribute("CCN"); 
				    listItem = new Option(names, codes);
				    ddlQueryCategory.options[ddlQueryCategory.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQueryCategory.options[0] = listItem;
			    }
			}
			
		
			
			
   }
   
      function fillSubCategoryDefaultValues()
{
//debugger;
    var valQuerySubCategory =document.getElementById("ddlQuerySubCategory").value;
    valQuerySubCategory=valQuerySubCategory.trim();
    try
    {
        if ( valQuerySubCategory != "")
        {
            document.getElementById("ddlPriority").value=valQuerySubCategory.split(',')[1];
            document.getElementById("ddlQueryStatus").value=valQuerySubCategory.split(',')[2];
            document.getElementById("ddlTeamAssignedTo").value=valQuerySubCategory.split(',')[3];
            document.getElementById("ddlContactType").value=valQuerySubCategory.split(',')[4];
            document.getElementById("txtTitle").value=valQuerySubCategory.split(',')[5];
           
           //@ Added By Abhishek For Default Valu on the basis of Employee Setting
           
           
             
                try
                {
                   var subcategoryId=valQuerySubCategory.split(',')[0];
                    if ( valQuerySubCategory != "")
                    {
                        CallServerFunctional1(subcategoryId+"|SC","This is context from client");
                    }
                }
                catch(err){}
                       
                       
           
           
           
           try
               {           
                 if ( document.getElementById("HdDefaultTeamAssignedTo").value!="")
                   {
                         document.getElementById("ddlTeamAssignedTo").value=document.getElementById("HdDefaultTeamAssignedTo").value;
                   }
                    if ( document.getElementById("HdDefaultContactType").value!="")
                   {
                     document.getElementById("ddlContactType").value= document.getElementById("HdDefaultContactType").value;
                   }
                   
                       //If Query status is Online Then Assigned To Online and If Query status is Offline Then Assigned To Offline
                                                
                          var Index=document.getElementById("ddlQueryStatus").selectedIndex;
                          var strqueystatus =document.getElementById("ddlQueryStatus").options[Index].text ;
                        //  alert(strqueystatus);
                          if (strqueystatus.indexOf("Online")>=0 )  
                          {  
                                document.getElementById("ddlTeamAssignedTo").value="1";
                          }
                           if (strqueystatus.indexOf("Offline")>=0 )   
                          {   
                             document.getElementById("ddlTeamAssignedTo").value="2";
                          }
                   
               }
                catch(err){}
            
        }
    }
    catch(err){}
    
}

   
    function fillSubCategoryFunctional()
   {
   
      document.getElementById('ddlQuerySubCategory').options.length=0;
      var id=document.getElementById('ddlQueryCategory').value;
      var item=document.getElementById("ddlQueryCategory").selectedIndex;
	  var text1=document.getElementById("ddlQueryCategory").options[item].text ;
	   var text2=text1;
	   text2=text2.toUpperCase();
	/*  if (text2=="PTR")
	  {
	      var intTeamLength= document.getElementById("ddlTeamAssignedTo").options.length;
	      for (var intCount=0 ;intCount<intTeamLength;intCount++)
	      {
    	     var textTeam=document.getElementById("ddlTeamAssignedTo").options[intCount].text ;
    	    textTeam=textTeam.toUpperCase();
	         if (textTeam=="OFFLINE")
	         {
	           document.getElementById("ddlTeamAssignedTo").value=document.getElementById("ddlTeamAssignedTo").options[intCount].value ;
	         }
	      }
	      var intStatusLength= document.getElementById("ddlQueryStatus").options.length;
	      for (var intCount=0 ;intCount<intStatusLength;intCount++)
	      {
    	     var textStatus=document.getElementById("ddlQueryStatus").options[intCount].text ;
    	     textStatus=textStatus.toUpperCase();
	         if (textStatus=="PENDING - OFFLINE")
	         {
	           document.getElementById("ddlQueryStatus").value=document.getElementById("ddlQueryStatus").options[intCount].value ;
	         }
	      }
	  }*/
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="--Select One--";
	  var ddlQuerySubCategory = document.getElementById('ddlQuerySubCategory');
	  if (document.getElementById("hdSubCategory").value=="") 
            {
             listItem = new Option(names, codes );
             ddlQuerySubCategory.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(document.getElementById("hdSubCategory").value);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			    		
			   // var orders = dsRoot.getElementsByTagName("CALL_SUB_CATEGORY[@CALL_CATEGORY_NAME='" + text1 + "']");
			     var orders = dsRoot.getElementsByTagName("CSC[@CCN='" + text1 + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQuerySubCategory.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
			        codes= orders[count].getAttribute("CSCI")+ "," + orders[count].getAttribute("HEI") + "," + orders[count].getAttribute("HSI")  + "," + orders[count].getAttribute("TI")  + "," + orders[count].getAttribute("CTI")+ "," + orders[count].getAttribute("TE") ; 
			        names=orders[count].getAttribute("CSCN"); 
				    listItem = new Option(names, codes);
				    ddlQuerySubCategory.options[ddlQuerySubCategory.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQuerySubCategory.options[0] = listItem;
			    }
			}
			
   }



function ResetAssingnedToValueForFunc()
{
              try
               {
                 //If Query status is Online Then Assigned To Online and If Query status is Offline Then Assigned To Offline                                                
                          var Index=document.getElementById("ddlQueryStatus").selectedIndex;
                          var strqueystatus =document.getElementById("ddlQueryStatus").options[Index].text ;
                        //  alert(strqueystatus);
                          if (strqueystatus.indexOf("Online")>=0 )    
                          { 
                            document.getElementById("ddlTeamAssignedTo").value="1";
                          }
                           if (strqueystatus.indexOf("Offline")>=0 ) 
                          {   
                            document.getElementById("ddlTeamAssignedTo").value="2";
                          }
                 }
             catch(err){}          
}

function ResetDesc()
{
          document.getElementById("HdDescWriitenByUser").value="1";  
          return true;   
  
}


 function populateDiv()
{

    var strCallerName =document.getElementById("txtCallerName").value;
    var strOfficeId=document.getElementById("txtCallerName").value;
    try
    {
        if ( strCallerName.length >= 3)
        {            
            // CallServerFunctional2(strCallerName+"|CN","This is context from client");
        }
    }
    catch(err){}
    
}


function ManageCallLogPage()
{
 //{debugger;}
 var ClosedStatus="";
 var strClosedStatus="";
            document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";  
if(textbox('txtAgencyName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Agency Name is Mandatory";	        	  
		      return false;} 
		      
		      if(textbox('txtCallerName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Caller Name is Mandatory";	 
	document.getElementById("txtCallerName").focus();      	  
		      return false;} 
		      
		      if(ddlvalidate('ddlQuerySubGroup')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Sub Group is Mandatory";
		    document.getElementById('ddlQuerySubGroup').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlQueryCategory')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Category is Mandatory";
		    document.getElementById('ddlQueryCategory').focus();
		    return false;}	
		      
		    var cnddlQuerySubCategory=document.getElementById('ddlQuerySubCategory').options.length;
		    if(cnddlQuerySubCategory>1)
		    {       document.getElementById("hdSubCategoryMandatory").value="1";
		             if(ddlvalidate('ddlQuerySubCategory')==true )
                    { }
	                 else
	                 { 
	                     document.getElementById("lblError").innerText="Query Sub Category is Mandatory";
	                     document.getElementById('ddlQuerySubCategory').focus();
		                 return false;
		             }	
		     } 
		     else
		     {
		        document.getElementById("hdSubCategoryMandatory").value="0";
		     }
		    
//		    if(document.getElementById("txtCallDuration").value.trim() != "" )
//		     {
//		        if (_isInteger(document.getElementById("txtCallDuration").value)==false)
//		      {
//		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 24";
//		      document.getElementById("txtCallDuration").focus();
//		      return false;
//		      }
//		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value,10);
//		      
//		        if((strCallDur > 24)  || (strCallDur < 0))
//		        {
//		        document.getElementById("lblError").innerText = "Hours must be in between 0 to 24";
//		        document.getElementById("txtCallDuration").focus();
//		        return false;
//		        }
//		     } 
		    
		      if(document.getElementById("txtCallDuration1").value != "")
		     {
		     if (_isInteger(document.getElementById("txtCallDuration1").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 60";
		      document.getElementById("txtCallDuration1").focus();
		      return false;
		      }
		      var strCallDur1=parseInt(document.getElementById("txtCallDuration1").value,10);
		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value,10);
		        if((strCallDur1 > 60)  || (strCallDur1 < 0))
		        {
		        document.getElementById("lblError").innerText = "Minutes must be in between 0 to 60";
		        document.getElementById("txtCallDuration1").focus();
		        return false;
		        }
		       if(strCallDur==24)
		        {
		             if(strCallDur1>0)
		             {
		             document.getElementById("lblError").innerText = "Minutes must be 0";
		             document.getElementById("txtCallDuration1").focus();
		             return false;
		             }
		        }
		     }   
		      
		      if(ddlvalidate('ddlPriority')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Priority is Mandatory";
		    document.getElementById('ddlPriority').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlQueryStatus')==true )
    {   
        ClosedStatus=document.getElementById('ddlQueryStatus').value; 
        strClosedStatus= ClosedStatus.split("|")[1];
        document.getElementById("hdSol").value=strClosedStatus;
    }
	else{ document.getElementById("lblError").innerText="Query Status is Mandatory";
		    document.getElementById('ddlQueryStatus').focus();
		    return false;}	
		      
		    	    
		     if(ddlvalidate('ddlTeamAssignedTo')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Assigned To is Mandatory";
		    document.getElementById('ddlTeamAssignedTo').focus();
		    return false;}	
		    
		    if(ddlvalidate('ddlCoordinator1')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Coordinator1 is Mandatory";
		    document.getElementById('ddlCoordinator1').focus();
		    return false;}	
		    
		    if(ddlvalidate('ddlContactType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Contact Type is Mandatory";
		    document.getElementById('ddlContactType').focus();
		    return false;}	
		    
           //	Start of Code Addedd by Abhishek
		   var grd=document.getElementById("gvDescription"); 
		   var NewDescAddded =false;;
		   if (grd !=null)
		   {
		         for(var i=1;i<grd.rows.length;i++)
                {
                      if (  document.getElementById("gvDescription").rows[i].cells[2].children[2].value=='N' )
                          {
                             NewDescAddded=true;   
                          } 	   
		       }	
		   }
		     //	End of Code Addedd by Abhishek
		       
		    
		    if(textbox('txtDescription')==true || NewDescAddded==true  ||( document.getElementById("hdMULTIHD_RE_ID").value != "" &&  document.getElementById("HdFuncManForDecAndSoln").value=="T2" )||( document.getElementById("hdMULTIHD_RE_ID").value != "" &&  document.getElementById("HdFuncManForDecAndSolnForNav").value=="N" )) 
    {        }
	else{ 
	
	    if (document.getElementById("hdPageHD_RE_ID").value == "" || document.getElementById("hdSaveRights").value == "1")
	    {
	        document.getElementById("lblError").innerText="Description is Mandatory";	 
	
	        document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";  
            document.getElementById("txtDescription").focus(); 
            try
            {
                
                if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtab"
                }
            }
            catch(err){}
               	  
		      return false;} 
		 }
		
		     
		      if(strClosedStatus == "1")
		      {
		      
		      
		      //	Start of Code Addedd by Abhishek
		   var grd=document.getElementById("gvSolution"); 
		   var NewSolnAddded =false;;
		   if (grd !=null)
		   {
		         for(var i=1;i<grd.rows.length;i++)
                 {
                      if (  document.getElementById("gvSolution").rows[i].cells[2].children[2].value=='N' )
                          {
                             NewSolnAddded=true;   
                          }  	   
		         }	
		   }
		     //	End of Code Addedd by Abhishek
		      
		      
		      
		      
		      if(textbox('txtSolution')==true || NewSolnAddded==true    ||( document.getElementById("hdMULTIHD_RE_ID").value != "" &&  document.getElementById("HdFuncManForDecAndSoln").value=="T2" )||( document.getElementById("hdMULTIHD_RE_ID").value != "" &&  document.getElementById("HdFuncManForDecAndSolnForNav").value=="N" ) )
    {        }
	else{ document.getElementById("lblError").innerText="Solution is Mandatory";	 
	 
	        document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="block";
            document.getElementById("pnlFollowUp").style.display="none";  
            document.getElementById("txtSolution").focus();   	 
            try
            {
                if (document.getElementById('theTabStrip_ctl02_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl02_Button1").className="headingtab"
                }
            }
            catch(err){} 
		      return false;} 
		      }
		    
		    if (document.getElementById("ddlMode").selectedIndex > 0 && document.getElementById("txtFollowDesc").value != "" && document.getElementById("txtNxtFollowupDate").value != "" )
		    {
		    
            }
            else
            {
            if (document.getElementById("ddlMode").selectedIndex == 0 && document.getElementById("txtFollowDesc").value == "" && document.getElementById("txtNxtFollowupDate").value == "" )
            {
            }
            else
            {
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="block"; 
             
             if(ddlvalidate('ddlMode')==true )
            {       }
	        else{ document.getElementById("lblError").innerText="Mode is Mandatory";
		    document.getElementById('ddlMode').focus();
		   
		    try
            {
                if (document.getElementById('theTabStrip_ctl03_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl03_Button1").className="headingtab"
                }
            }
            catch(err){} 
             return false;
		    }	
             
             if(textbox('txtFollowDesc')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Description is Mandatory";	 
	document.getElementById("txtFollowDesc").focus();      	  
		      try
            {
                if (document.getElementById('theTabStrip_ctl03_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl03_Button1").className="headingtab"
                }
            }
            catch(err){} 
             return false;
             } 
		      
		      if(textbox('txtNxtFollowupDate')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Next Follow up Date is Mandatory";	 
	document.getElementById("txtNxtFollowupDate").focus();      	  
		       try
            {
                if (document.getElementById('theTabStrip_ctl03_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl03_Button1").className="headingtab"
                }
            }
            catch(err){} 
             return false;
             } 
		              
           
            }
            }
            
            try
            {
                 var itemTeam=document.getElementById("ddlTeamAssignedTo").selectedIndex;
                 var textTeam=document.getElementById("ddlTeamAssignedTo").options[itemTeam].text ;
    	        textTeam=textTeam.toUpperCase();
    	         var itemStatus=document.getElementById("ddlQueryStatus").selectedIndex;
    	        var textStatus=document.getElementById("ddlQueryStatus").options[itemStatus].text ;
    	         textStatus=textStatus.toUpperCase();
	             if (textTeam=="ONLINE" && (textStatus=="SOLVED - OFFLINE" || textStatus=="PENDING - OFFLINE"))
	             {
	              document.getElementById("lblError").innerText="Call Could not be saved because AssignedTo is Online and Query Status is " + textStatus;	 
	              document.getElementById("ddlTeamAssignedTo").focus();
	                try
                 {
                    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                    {
                        document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                        document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                    }
                }
            catch(err){} 
             return false;      	  
		        
	             }
	        }
	        catch(err){}
	     
	     var strPTRNo=document.getElementById("txtPTRNo").value;
	     var strWorkOrderNo=document.getElementById("txtWorkOrderNo").value;
	     var strBDRLetterID=document.getElementById("txtBDRLetterID").value;
	    /* if ( strPTRNo != "" && (strWorkOrderNo != "" || strBDRLetterID != "")) 
	     {
	              document.getElementById("lblError").innerText= "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
	               try
                 {
                    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                    {
                        document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                        document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                    }
                }
            catch(err){} 
	              return false;	      
	     }
	     
	     if ( strWorkOrderNo != "" && (strPTRNo != "" || strBDRLetterID != "")) 
	     {
	              document.getElementById("lblError").innerText= "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
	               try
                 {
                    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                    {
                        document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                        document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                    }
                }
            catch(err){} 
	              return false;	      
	     }
	     
	     if ( strBDRLetterID != "" && (strWorkOrderNo != "" || strPTRNo != "")) 
	     {
	              document.getElementById("lblError").innerText= "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
	               try
                 {
                    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                    {
                        document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                        document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                    }
                }
            catch(err){} 
	              return false;	      
	     }
              */
                
		  var itemQueryCategory=""
         try
         {
            var itemQueryCategory=document.getElementById("ddlQueryCategory").selectedIndex;
	        var textQueryCategory=document.getElementById("ddlQueryCategory").options[itemQueryCategory].text ;
	        textQueryCategory=textQueryCategory.toUpperCase();
	        //For PTR CASE
	        /*if (strPTRNo!="")
	        {
	            if (textQueryCategory != "PTR" )
	            {
    	                document.getElementById("lblError").innerText= "Invalid Query Category";
    	                document.getElementById("ddlQueryCategory").focus();
	                   try
                     {
                        if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                        {
                            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                        }
                    }
                     catch(err){} 
	                  return false;	 
	            }
	        }
	         //For WorkOrder CASE
	        if (strWorkOrderNo!="")
	        {
	            if (textQueryCategory != "WORK ORDER" )
	            {
    	                document.getElementById("lblError").innerText= "Invalid Query Category";
    	                document.getElementById("ddlQueryCategory").focus();
	                   try
                     {
                        if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                        {
                            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                        }
                    }
                     catch(err){} 
	                  return false;	 
	            }
	        }
	        */
	         //For BDRLetter CASE   
	        if (strBDRLetterID!="")
	        {
	            if (textQueryCategory != "BDR LETTER" )
	            {
    	                document.getElementById("lblError").innerText= "Invalid Query Category";
    	                document.getElementById("ddlQueryCategory").focus();
	                   try
                     {
                        if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                        {
                            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                        }
                    }
                     catch(err){} 
	                  return false;	 
	            }
	        }
	    }
	    catch(err){}
}


  function ColorMethodLinkedLTR(id,total,itemIndex)
{ 
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
         document.getElementById('lblPanelClick').value =id; 
       var pgStatus=document.getElementById('hdPageStatus').value;
         var HD_RE_ID =document.getElementById('hdEnPageHD_RE_ID').value;
       var strHD_RE_ID=document.getElementById('hdEnPageHD_RE_ID').value;
       var LCode=document.getElementById('hdEnPageLCode').value;
       var EnstrStatus=document.getElementById('hdEnPageStatus').value;
       var strStatus=document.getElementById('hdPageStatus').value;
       var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
       var strSaveStatus = document.getElementById("hdSaveStatus").value ;
       document.getElementById("hdButton").value="0"
       document.getElementById("hdTabType").value=itemIndex
       var strSaveStatusTemp="0";
       if (strSaveStatus=="0")
       {
        if (confirm("Do you want to save?")==true)
           {
           strSaveStatusTemp="1"  ;             
           }           
       }
       if (strSaveStatusTemp=="0")
       {
       if (strStatus != "Technical")
       {
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action=U&TabType=0&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnstrStatus + "&FeedBackId="+ strFeedBackId;               
          return false;
           }
          return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action=U&TabType=1&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnstrStatus+ "&FeedBackId="+ strFeedBackId;                   
          return false;
           }
           
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
           
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action=U&TabType=2&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnstrStatus+ "&FeedBackId="+ strFeedBackId;                   
          return false;
           }
                    
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {
            
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action=U&TabType=3&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnstrStatus+ "&FeedBackId="+ strFeedBackId;                   
          return false;
           }
          return false;
           
       }
       
        else if (id == (ctextFront +  "04" + ctextBack))
       {
        return false;
         }
       else if (id == (ctextFront +  "05" + ctextBack))
       {
        var AOFFICE=document.getElementById("hdEnAOffice").value;
      var strStatus=document.getElementById("hdEnFunctional").value;
              document.getElementById('hdTabType').value='5';
                    window.location.href="HDUP_helpDeskFeedBack.aspx?Action=U&strStatus="+strStatus+"&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId+ "&AOFFICE="+ AOFFICE ;
            return false;
        
       }
       }
       else
       {
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_CallLog.aspx?Action=U&TabType=0&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnstrStatus+ "&FeedBackId="+ strFeedBackId;                     
          return false;
           }
          return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_CallLog.aspx?Action=U&TabType=1&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnstrStatus+ "&FeedBackId="+ strFeedBackId;                     
          return false;
           }
           
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
           
            if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_CallLog.aspx?Action=U&TabType=2&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnstrStatus+ "&FeedBackId="+ strFeedBackId;                     
          return false;
           }
                    
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {
          return false;   
           
           
       }
       else if (id == (ctextFront +  "04" + ctextBack))
       {
     
        
      var AOFFICE=document.getElementById("hdEnAOffice").value;
       
     
            document.getElementById('hdTabType').value='4';
            
            window.location.href="HDUP_helpDeskFeedBack.aspx?Action=U&strStatus="+EnstrStatus+"&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId+ "&AOFFICE="+ AOFFICE ;
            return false;
           
       }
       
       }
       
       }
       else
       {
       document.getElementById("hdButton").value="1";
       document.forms(0).submit();
       
       }
                                
       
}





function MultipleChangeStatus()
{
    var strClosedStatus="";
    var ClosedStatus="";
    if(ddlvalidate('ddlStatus')==true )
    {   
        ClosedStatus=document.getElementById('ddlStatus').value; 
       strClosedStatus= ClosedStatus.split("|")[1];
    }
	else
	{
	     document.getElementById("lblError").innerText="Query Status is Mandatory";
		 document.getElementById('ddlStatus').focus();
		return false;
   }	
	    
	 if(strClosedStatus == "1")
	 {
	     if(textbox('txtSol')==true )
        {        }
	    else
	    { 
	        document.getElementById("lblError").innerText="Solution is Mandatory";  
	        document.getElementById('txtSol').focus(); 
	        return false;
	    }
	 }
}


 function fillDateCallLog(id)
        {
        
            if (id=="1")
            {
                if(document.getElementById("chkDisplayLastCall").checked)
                {
                 document.getElementById("txtQueryOpenedDateFrom").value="";
                 document.getElementById("txtQueryOpenedDateTo").value="";
                }
                else
                {
                
                     if(document.getElementById("chkISPending").checked)
                     {
                         document.getElementById("txtQueryOpenedDateFrom").value=document.getElementById("hdPendingTime").value;
                         document.getElementById("txtQueryOpenedDateTo").value=document.getElementById("hdToTime").value;
                     }
                    else
                     {
                         document.getElementById("txtQueryOpenedDateFrom").value=document.getElementById("hdFromTime").value;
                         document.getElementById("txtQueryOpenedDateTo").value=document.getElementById("hdToTime").value;
                     }
                }
            }
            else
            {
                if(document.getElementById("chkISPending").checked)
                {
                 document.getElementById("txtQueryOpenedDateFrom").value=document.getElementById("hdPendingTime").value;
                 document.getElementById("txtQueryOpenedDateTo").value=document.getElementById("hdToTime").value;
                }
                else
                {
                 if(document.getElementById("chkDisplayLastCall").checked)
                 {
                     document.getElementById("txtQueryOpenedDateFrom").value="";
                     document.getElementById("txtQueryOpenedDateTo").value="";
                 }
                 else
                 {
                     document.getElementById("txtQueryOpenedDateFrom").value=document.getElementById("hdFromTime").value;
                     document.getElementById("txtQueryOpenedDateTo").value=document.getElementById("hdToTime").value;
                 }
                }
                
            }
        }
        
        
        function LinkedLTRPage()
{

var strVal=document.getElementById("hdCHD_RE_IDList").value;
var strDeleteStatus=document.getElementById("hdDeleteId").value;
var arstrVal=strVal.split("|");
var cn="0";
for (i=0;i<arstrVal.length;i++)
{
if (arstrVal[i]=="")
{
}
else
{
cn=1;
}
}
if (cn=="0" && strDeleteStatus!="-100" )
{
 document.getElementById("lblError").innerText="Please select atleast one LTR";
return false;
}
}


function fnCallLogIDCloseFunctional()
{
 try
    {
        if (document.getElementById("hdPageHD_RE_ID").value != "")
         {
             if (window.opener.document.forms['form1']['hdEHD_RE_ID']!=null)
            { 
             window.opener.document.forms['form1']['hdEHD_RE_ID'].value=document.getElementById("hdPageHD_RE_ID").value;
            window.opener.document.forms['form1']['hdPopupStatus'].value=document.getElementById("hdQueryStatus").value;
            window.opener.document.forms['form1'].submit();          
            return false;
         
            }    
        }
   }
        catch(err){}
}


function fnMultiBDRddl(strSelectedValue)
{
      var codes='';
	  var names="";
	   document.getElementById('ddlBDRLetterID').options.length=0;
	  var ddlBDRLetterID = document.getElementById('ddlBDRLetterID');
	 
	  var strData=document.getElementById('hdMultiBDRID').value;
	  var listItem,strSplitResult;	 
	  listItem = new Option(names, codes);
	  ddlBDRLetterID.options[0] = listItem;
	  if (strData!="")
	  {
	    strSplitResult=strData.split(",");
	    for (i=0;i<strSplitResult.length;i++)
	    {
	       names=strSplitResult[i];
	       codes=strSplitResult[i];
	       listItem = new Option(names, codes);
	       ddlBDRLetterID.options[ddlBDRLetterID.length] = listItem;
	    }
	    ddlBDRLetterID.value=strSelectedValue;
	  }
	  
}


function fnMultiWOddl(strSelectedValue)
{
      var codes='';
	  var names="";
	   document.getElementById('ddlWorkOrderNo').options.length=0;
	  var ddlWorkOrderNo = document.getElementById('ddlWorkOrderNo');
	 
	  var strData=document.getElementById('hdMultiWO').value;
	  var listItem,strSplitResult;	 
	  listItem = new Option(names, codes);
	  ddlWorkOrderNo.options[0] = listItem;
	  if (strData!="")
	  {
	    strSplitResult=strData.split(",");
	    for (i=0;i<strSplitResult.length;i++)
	    {
	       names=strSplitResult[i].split("|")[1];
	       codes=strSplitResult[i].split("|")[0];
	       listItem = new Option(names, codes);
	       ddlWorkOrderNo.options[ddlWorkOrderNo.length] = listItem;
	    }
	    ddlWorkOrderNo.value=strSelectedValue;
	  }
	  
}



function fnMultiPTRddl(strSelectedValue)
{
      var codes='';
	  var names="";
	   document.getElementById('ddlPTRNo').options.length=0;
	  var ddlPTRNo = document.getElementById('ddlPTRNo');
	 
	  var strData=document.getElementById('hdMultiPTR').value;
	  var listItem,strSplitResult;	 
	  listItem = new Option(names, codes);
	  ddlPTRNo.options[0] = listItem;
	  if (strData!="")
	  {
	    strSplitResult=strData.split(",");
	    for (i=0;i<strSplitResult.length;i++)
	    {
	       names=strSplitResult[i].split("|")[1];
	       codes=strSplitResult[i].split("|")[0];
	       listItem = new Option(names, codes);
	       ddlPTRNo.options[ddlPTRNo.length] = listItem;
	    }
	    ddlPTRNo.value=strSelectedValue;
	  }
	  
}




//-------------------- TECHNOCAL


 function HideShowTechnical()
    {
   
    var strTabtype=document.getElementById("hdTabType").value;
    switch(strTabtype)
    {
    case "0":
            document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            break;
    case "1":
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("pnlSol").style.display="none";
            break;
     case "2":
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="block";
            break;
      
         
    }
    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
    {
        document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
    }
    
   
     switch(strTabtype)
    {
    case "0":
            if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
            {
                document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
            }
    
         
             break;
    case "1":
            if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
             {
                document.getElementById("theTabStrip_ctl01_Button1").className="headingtab"
             }
    
           
             break;
    case "2":
             if (document.getElementById('theTabStrip_ctl02_Button1').className != "displayNone")
             {
                document.getElementById("theTabStrip_ctl02_Button1").className="headingtab"
             }
          default:
             if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
             {
               // document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
             }
             break;
            
   
    }
    }
   
   function YesNoTechnical()
   {
        try
        {
            var strReturn;
           var msg =document.getElementById("hdMsg").value;
           if (msg!=null)
            {
                var type = "../popup/confirmwindow.htm?Msg=" + msg;
                      
                strReturn = showPopWin(type, 350, 120, returnRefreshTechnical,null); 
            }
            else
            {
                 document.getElementById("hdReSave").value="1";
             
              // __doPostBack('btnSave','');          
                 document.forms['form1'].submit();
            }
        }
         catch(err)
        {
             
            
               document.getElementById("hdReSave").value="1";
             
              // __doPostBack('btnSave','');          
              document.forms['form1'].submit();
          
        }
       
   }
   function returnRefreshTechnical(returnVal) 
    {
      
         if (returnVal != null)
         {    
            if (returnVal=="1")
            {   
               document.getElementById("hdReSave").value="1";
                         
               document.forms['form1'].submit();
            }
           else
            {   
                try
                {
                    document.getElementById("ddlQuerySubGroup").selectedIndex=0;
                 }
                 catch(err)
                 {}     
               
            }
	     }  
	     
    }


function ColorMethod(id,total)
{  
        document.getElementById("lblError").innerHTML='';
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
           if (document.getElementById(Hcontrol).className != "displayNone")
            {
                document.getElementById(Hcontrol).className="headingtabactive";
            }
        }
        
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblPanelClick').value =id; 
       var pgStatus=document.getElementById('hdPageStatus').value;
       var strStatus=document.getElementById('hdEnTechnical').value;
     //  if(pgStatus=='U')
     //  {    
       var strHD_RE_ID =document.getElementById('hdEnPageHD_RE_ID').value;
       var strLCode=document.getElementById('hdEnPageLCode').value;
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById('rdTechanical').focus();
           document.getElementById('hdTabType').value='0';
       /*    if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_CallLog.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="TCUP_CallLog.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }*/
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
           document.getElementById('hdTabType').value='1';
           document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById('txtDescription').focus();
       /*     if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_Description.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="TCUP_Description.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }*/
           
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
            document.getElementById('hdTabType').value='2';
             document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="block";
            document.getElementById('txtSolution').focus();
       /*      if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_Solution.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="TCUP_Solution.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }*/
         
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {
       var LCode=document.getElementById("hdEnPageLCode").value;
       var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
       var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
       var AOFFICE=document.getElementById("hdEnAOffice").value;
       
       //Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus
            document.getElementById('hdTabType').value='3';
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            window.location.href="HDUP_LinkedLTR.aspx?Action=U&strStatus="+strStatus+"&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;  
            return false;
           
       }
       else if (id == (ctextFront +  "04" + ctextBack))
       {
       var LCode=document.getElementById("hdEnPageLCode").value;
        var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
       var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
        var AOFFICE=document.getElementById("hdEnAOffice").value;
       //Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus
          /*  document.getElementById('hdTabType').value='5';
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";*/
            window.location.href="HDUP_helpDeskFeedBack.aspx?Action=U&strStatus="+strStatus+"&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;  
            return false;
      
            
           
       }
     //  }                               
       
}



function ManageTCallLogPage()
{

 var ClosedStatus="";
 var strClosedStatus="";
 document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
if(textbox('txtAgencyName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Agency Name is Mandatory";	        	  
		      return false;} 
		      
		      if(textbox('txtCallerName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Caller Name is Mandatory";	 
	document.getElementById("txtCallerName").focus();      	  
		      return false;} 
		      
		      if(ddlvalidate('ddlQuerySubGroup')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Sub Group is Mandatory";
		    document.getElementById('ddlQuerySubGroup').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlQueryCategory')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Category is Mandatory";
		    document.getElementById('ddlQueryCategory').focus();
		    return false;}	
		      
		      var cnddlQuerySubCategory=document.getElementById('ddlQuerySubCategory').options.length;
		    if(cnddlQuerySubCategory>1)
		    {       document.getElementById("hdSubCategoryMandatory").value="1";
		             if(ddlvalidate('ddlQuerySubCategory')==true )
                    { }
	                 else
	                 { 
	                     document.getElementById("lblError").innerText="Query Sub Category is Mandatory";
	                     document.getElementById('ddlQuerySubCategory').focus();
		                 return false;
		             }	
		     } 
		     else
		     {
		        document.getElementById("hdSubCategoryMandatory").value="0";
		     }
		      
		    
		     if(document.getElementById("txtCallDuration").value.trim() != "" )
		     {
		        if (_isInteger(document.getElementById("txtCallDuration").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 24";
		      document.getElementById("txtCallDuration").focus();
		      return false;
		      }
		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value);
		      
		        if((strCallDur > 24)  || (strCallDur < 0))
		        {
		        document.getElementById("lblError").innerText = "Hours must be in between 0 to 24";
		        document.getElementById("txtCallDuration").focus();
		        return false;
		        }
		     } 
		    
		      if(document.getElementById("txtCallDuration1").value != "")
		     {
		     if (_isInteger(document.getElementById("txtCallDuration1").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 60";
		      document.getElementById("txtCallDuration1").focus();
		      return false;
		      }
		      var strCallDur1=parseInt(document.getElementById("txtCallDuration1").value);
		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value);
		        if((strCallDur1 > 60)  || (strCallDur1 < 0))
		        {
		        document.getElementById("lblError").innerText = "Minutes must be in between 0 to 60";
		        document.getElementById("txtCallDuration1").focus();
		        return false;
		        }
		        if(strCallDur==24)
		        {
		             if(strCallDur1>0)
		             {
		             document.getElementById("lblError").innerText = "Minutes must be 0";
		             document.getElementById("txtCallDuration1").focus();
		             return false;
		             }
		        }
		       
		     } 
		     
		      
		      if(ddlvalidate('ddlPriority')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Priority is Mandatory";
		    document.getElementById('ddlPriority').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlQueryStatus')==true )
    {   
     ClosedStatus=document.getElementById('ddlQueryStatus').value; 
        strClosedStatus= ClosedStatus.split("|")[1];
        }
	else{ document.getElementById("lblError").innerText="Query Status is Mandatory";
		    document.getElementById('ddlQueryStatus').focus();
		    return false;}	
		      
		    	    
		     if(ddlvalidate('ddlTeamAssignedTo')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Assigned to  is Mandatory";
		    document.getElementById('ddlTeamAssignedTo').focus();
		    return false;}	
		    /*
		    if(ddlvalidate('ddlCoordinator1')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Coordinator1 is Mandatory";
		    document.getElementById('ddlCoordinator1').focus();
		    return false;}	*/
		    
		    if(ddlvalidate('ddlContactType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Contact Type is Mandatory";
		    document.getElementById('ddlContactType').focus();
		    return false;}	
		    
		    
		      //	Start of Code Addedd by Abhishek
		   var grd=document.getElementById("gvDescription"); 
		   var NewTDescAddded =false;;
		   if (grd !=null)
		   {
		         for(var i=1;i<grd.rows.length;i++)
                {
                      if (  document.getElementById("gvDescription").rows[i].cells[2].children[2].value=='N' )
                          {
                             NewTDescAddded=true;   
                          } 	   
		       }	
		   }
		     //	End of Code Addedd by Abhishek
		    
		    
		    if(textbox('txtDescription')==true || NewTDescAddded==true ||( document.getElementById("hdMULTIHD_RE_ID").value != "" &&  document.getElementById("HdTechManForDecAndSoln").value=="T2" )||( document.getElementById("hdMULTIHD_RE_ID").value != "" &&  document.getElementById("HdTechManForDecAndSolnForNav").value=="N" ) )
    {        }
	else{ 
	if (document.getElementById("hdPageHD_RE_ID").value == "")
	    {
	        document.getElementById("lblError").innerText="Description is Mandatory";	 
	
	        document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("txtDescription").focus();   
            try
            {
                
                if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtab"
                }
            }
            catch(err){} 	  
		      return false;} 
		 
		}
		     
		      if(strClosedStatus == "1")
		      {
		      
		      
		     //	Start of Code Addedd by Abhishek
		   var grd=document.getElementById("gvSolution"); 
		   var NewTSolnAddded =false;;
		   if (grd !=null)
		   {
		         for(var i=1;i<grd.rows.length;i++)
                 {
                      if (  document.getElementById("gvSolution").rows[i].cells[2].children[2].value=='N' )
                          {
                             NewTSolnAddded=true;   
                          }  	   
		         }	
		   }
		     //	End of Code Addedd by Abhishek
		      
		      
		  //{debugger;}    
		      
		      if(textbox('txtSolution')==true || NewTSolnAddded==true ||( document.getElementById("hdMULTIHD_RE_ID").value != "" &&  document.getElementById("HdTechManForDecAndSoln").value=="T2" )||( document.getElementById("hdMULTIHD_RE_ID").value != "" &&  document.getElementById("HdTechManForDecAndSolnForNav").value=="N" ) )
    {        }
	else{ document.getElementById("lblError").innerText="Solution is Mandatory";	 
	 
	        document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="block";
             document.getElementById("txtSolution").focus();  
             try
            {
                
                if (document.getElementById('theTabStrip_ctl02_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                     document.getElementById("theTabStrip_ctl02_Button1").className="headingtab"
                }
            }
            catch(err){}
              	  
		      return false;} 
		      }
		    
}



function fillCategoryTechnical()
   {
      document.getElementById('ddlQueryCategory').options.length=0;
      document.getElementById('ddlQuerySubCategory').options.length=0;
      id=document.getElementById('ddlQuerySubGroup').value;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="--Select One--";
	  document.getElementById('ddlQuerySubCategory').options[0]=new Option(names, codes );
	  var ddlQueryCategory = document.getElementById('ddlQueryCategory');
	  if (document.getElementById("hdCategory").value=="") 
            {
             listItem = new Option(names, codes );
             ddlQueryCategory.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(document.getElementById("hdCategory").value);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			     var item=document.getElementById("ddlQuerySubGroup").selectedIndex;
			     var text1=document.getElementById("ddlQuerySubGroup").options[item].text;		
			     //<HD_CC><CC CCI='' CCN='' CSGN='' />
			    //var orders = dsRoot.getElementsByTagName("CALL_CATEGORY[@CALL_SUB_GROUP_NAME='" + text1 + "']");
			    var orders = dsRoot.getElementsByTagName("CC[@CSGN='" + text1 + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQueryCategory.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
			        
				    codes= orders[count].getAttribute("CCI"); 
			        names=orders[count].getAttribute("CCN"); 
				    listItem = new Option(names, codes);
				    ddlQueryCategory.options[ddlQueryCategory.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQueryCategory.options[0] = listItem;
			    }
			}
   }
   
    function fillSubCategoryTechnical()
   {
      document.getElementById('ddlQuerySubCategory').options.length=0;
      id=document.getElementById('ddlQueryCategory').value;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="--Select One--";
	  var ddlQuerySubCategory = document.getElementById('ddlQuerySubCategory');
	  if (document.getElementById("hdSubCategory").value=="") 
            {
             listItem = new Option(names, codes );
             ddlQuerySubCategory.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(document.getElementById("hdSubCategory").value);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			     var item=document.getElementById("ddlQueryCategory").selectedIndex;
			     var text1=document.getElementById("ddlQueryCategory").options[item].text;		
			  //  var orders = dsRoot.getElementsByTagName("CALL_SUB_CATEGORY[@CALL_CATEGORY_NAME='" + text1 + "']");
			   var orders = dsRoot.getElementsByTagName("CSC[@CCN='" + text1 + "']");
			  //<HD_CSC> <CSC CSCI='' CSCN='' CCN='' CSGN='' /> 
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQuerySubCategory.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
				    codes= orders[count].getAttribute("CSCI"); 
			        names=orders[count].getAttribute("CSCN"); 
				    listItem = new Option(names, codes);
				    ddlQuerySubCategory.options[ddlQuerySubCategory.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQuerySubCategory.options[0] = listItem;
			    }
			}
   }

function fillSubCategoryDefaultValuesTechnical()
{
//{debugger;}
     var valQuerySubCategory =document.getElementById("ddlQuerySubCategory").value;
    valQuerySubCategory=valQuerySubCategory.trim();
    try
    {
        if ( valQuerySubCategory != "")
        {
            CallServerTech(valQuerySubCategory+"|SC","This is context from client");
        }
    }
    catch(err){}
}
   



function ET_PopupPageFunctional(id)
         {
  
         var type;
         //var stLCode=document.getElementById("hdCallAgencyName").value; commented on 2 aug 07
         var strLCode=document.getElementById("hdEnCallAgencyName_LCODE").value; //Added on 2 aug 08 
         var itemQueryCategory=""
         var strPTRNo=document.getElementById("txtPTRNo").value;
	     var strWorkOrderNo=document.getElementById("txtWorkOrderNo").value;
	     var strBDRLetterID=document.getElementById("txtBDRLetterID").value;
         try
         {
            var itemQueryCategory=document.getElementById("ddlQueryCategory").selectedIndex;
	        var textQueryCategory=document.getElementById("ddlQueryCategory").options[itemQueryCategory].text ;
	        textQueryCategory=textQueryCategory.toUpperCase();
	    }
	    catch(err){}
	    //code for this added above on 2 Aug 08
       /*  if (stLCode != "")
         {
              //strLCode=stLCode.split("|")[0]; 
         }*/
          if (id=="1")
         {
             type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	         window.open(type,"aa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }
         if (id=="2")
         {
                var strAgencyName=document.getElementById("txtAgencyName").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                if (strAgencyName!="")
                {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName;
   	                window.open(type,"aaHelpDesk","height=600,width=900,top=30,left=20,scrollbars=1,status=1");                  
   	            }
   	            else
   	            {
   	                document.getElementById("lblError").innerHTML='Agency name is mandatory';
                    return false;
   	            }
          }
        
    
         if (id=="3")
         {
                document.getElementById("lblError").innerHTML='';
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
               // var strMultiHD_RE_ID=document.getElementId("hdMULTIHD_RE_ID").value;
                //if (strHD_RE_ID != "" && strLCode != "" && textQueryCategory=="PTR" )
                if (strHD_RE_ID != "" && strLCode != "" )
                {
                    //if(strWorkOrderNo=="" && strBDRLetterID=="")
                   // {
                    
                        if(document.getElementById("ddlPTRNo").value == "")
                        {
                            type = "../ECOMMTrackerHelpDesk/HDUP_Ptr.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode; //+ "&MultiHD_RE_ID=" + strMultiHD_RE_ID ;
   	                        window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                    }
   	                    else
   	                    {
   	                        //var strAction="U|" + document.getElementById("hdEnPTRNo").value ;
   	                        var strAction="U|" + document.getElementById("ddlPTRNo").value ;   	                        
   	                        type = "../ECOMMTrackerHelpDesk/HDUP_Ptr.aspx?Popup=T&Action=" + strAction + "&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;//+ "&MultiHD_RE_ID=" + strMultiHD_RE_ID;
   	                        window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
   	                    }
   	                //}
   	            } 
   	            else
   	            {
   	               // document.getElementById("lblError").innerHTML='Query Category must be PTR';
                   // return false;
   	            }
   	             	                    
          }
          if (id=="4")
         {
                document.getElementById("lblError").innerHTML='';
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                if (strHD_RE_ID != "" && strLCode != "" && textQueryCategory=="BDR LETTER")
                {
                   // if( strPTRNo=="" &&  strWorkOrderNo=="")
                    //{
                        //if(document.getElementById("hdBDRLetterID").value == "")'commented on 13 apr 10
                        if(document.getElementById("ddlBDRLetterID").value == "")
                        
                        {
                            if (document.getElementById("ddlQueryCategory").value != "")
                            {
                                 var item=document.getElementById("ddlQuerySubCategory").selectedIndex;
                                 if (item==0 )                 
                                 {
                                     text1="";
                                 }
                                 else
                                 {
			                     var text1=document.getElementById("ddlQuerySubCategory").options[item].text;	
                                 }
                                 type = "../ECOMMTrackerHelpDesk/HDUP_BDR.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode +"&requestType=" + text1;//'document.getElementById("ddlQueryCategory").value;
   	                            window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                        }
   	                        else
   	                        {
   	                            document.getElementById("lblError").innerText="First Select query Category";
   	                        }
   	                    }
   	                    else
   	                     {
                             type = "../ECOMMTrackerHelpDesk/HDUP_BDR.aspx?Popup=T&Action=U&ReqID="+strHD_RE_ID + "&LCode="+strLCode + "&HD_RE_BDR_ID=" + document.getElementById("ddlBDRLetterID").value;
   	                         window.open(type,"HelpDesks","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	                    }
       	            //}
   	                
   	            }
   	             else
   	            {
   	                document.getElementById("lblError").innerHTML='Query Category must be BDR LETTER';
                    return false;
   	            }
          }
        if (id=="5")
         {      document.getElementById("lblError").innerHTML='';
                 var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
             //   if (strHD_RE_ID != "" && strLCode != "" && textQueryCategory=="WORK ORDER")
                if (strHD_RE_ID != "" && strLCode != "" )
                {
                   // if(strPTRNo=="" && strBDRLetterID=="")
                    //{
                       // if(document.getElementById("hdEnWorkOrderNo").value == "") commented on 13 apr 10
                        if(document.getElementById("ddlWorkOrderNo").value == "")
                        {
                        type = "../ECOMMTrackerHelpDesk/HDUP_WorkOrder.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;
   	                    window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                    }
           	            
   	                    else
   	                    {
                        type = "../ECOMMTrackerHelpDesk/HDUP_WorkOrder.aspx?Popup=T&Action=U&ReqID="+strHD_RE_ID + "&LCode="+strLCode + "&OrderID=" + document.getElementById("ddlWorkOrderNo").value ;
   	                    window.open(type,"HelpDesks1","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	                    }
   	                 //}
   	            }
   	            else
   	            {
   	                //document.getElementById("lblError").innerHTML='Query Category must be WORK ORDER';
                   // return false;
   	            }
          }
          if (id=="6")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="HDUP_Transaction.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"HelpDesksTransaction","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	            return false;
          }
           if (id=="7")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="../ECOMMTrackerHelpDeskPopup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"HelpDesksCallLogHistory","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	            return false;
          }

          if (id=="8")
         {
                 var strStatus=document.getElementById("hdEnFunctional").value;
                 var LCode=document.getElementById("hdEnPageLCode").value;
                var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
               var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
               var AOFFICE=document.getElementById("hdEnAOffice").value;              
      
              type="HDUP_helpDeskFeedBack.aspx?Popup=T&Action=U&strStatus=" + strStatus + "&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;   
              window.open(type,"HelpDesksCallFeedBack","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	          return false;
          }
     }



function ET_PopupPage(id)
         {
      
         var type;
          if (id=="1")
         {
            // type = "../ECOMMTrackerHelpDeskPopup/PUSR_FeedBack.aspx?Popup=T" ;
             type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	         window.open(type,"Taa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }

         if (id=="2")
         {
                var strAgencyName=document.getElementById("txtAgencyName").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                if (strAgencyName!="")
                {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName;
                    window.open(type,"TaaHelpDesk","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
                }                  
               else
   	            {
   	                document.getElementById("lblError").innerHTML='Agency name is mandatory';
                    return false;
   	            }
          }
        
    
         if (id=="3")
         {
                type = "../ECOMMTrackerHelpDeskPopup/HDSR_Ptr.aspx?Popup=T";
   	            window.open(type,"TaaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
          }
          if (id=="4")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="../ECOMMTrackerHelpDeskPopup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"THelpDesksTransaction","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	            return false;          
          }
        if (id=="5")
         {
                 var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="TCUP_Transaction.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"THelpDesksTransaction","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	            return false;         
          }
         if (id=="6")
         {
              var strStatus=document.getElementById('hdEnTechnical').value;  
              var LCode=document.getElementById("hdEnPageLCode").value;
              var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
              var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
              var AOFFICE=document.getElementById("hdEnAOffice").value;
              type="HDUP_helpDeskFeedBack.aspx?Popup=T&Action=U&strStatus="+strStatus+"&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;  
              window.open(type,"THelpDesksFeedBack","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	            return false;   
                
          
         }
     }





// end here


