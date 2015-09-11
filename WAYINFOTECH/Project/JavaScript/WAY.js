
// JScript File
// HISTORY
// ------------------------------------------------------------------
// May 17, 2003: Fixed bug in parseDate() for dates <1970
// March 11, 2003: Added parseDate() function
// March 11, 2003: Added "NNN" formatting option. Doesn't match up
//                 perfectly with SimpleDateFormat formats, but 
//                 backwards-compatability was required.

// ------------------------------------------------------------------
// These functions use the same 'format' strings as the 
// java.text.SimpleDateFormat class, with minor exceptions.
// The format string consists of the following abbreviations:
// 
// Field        | Full Form          | Short Form
// -------------+--------------------+-----------------------
// Year         | yyyy (4 digits)    | yy (2 digits), y (2 or 4 digits)
// Month        | MMM (name or abbr.)| MM (2 digits), M (1 or 2 digits)
//              | NNN (abbr.)        |
// Day of Month | dd (2 digits)      | d (1 or 2 digits)
// Day of Week  | EE (name)          | E (abbr)
// Hour (1-12)  | hh (2 digits)      | h (1 or 2 digits)
// Hour (0-23)  | HH (2 digits)      | H (1 or 2 digits)
// Hour (0-11)  | KK (2 digits)      | K (1 or 2 digits)
// Hour (1-24)  | kk (2 digits)      | k (1 or 2 digits)
// Minute       | mm (2 digits)      | m (1 or 2 digits)
// Second       | ss (2 digits)      | s (1 or 2 digits)
// AM/PM        | a                  |
//
// NOTE THE DIFFERENCE BETWEEN MM and mm! Month=MM, not mm!
// Examples:
//  "MMM d, y" matches: January 01, 2000
//                      Dec 1, 1900
//                      Nov 20, 00
//  "M/d/yy"   matches: 01/20/00
//                      9/2/00
//  "MMM dd, yyyy hh:mm:ssa" matches: "January 01, 2000 12:30:45AM"
// ------------------------------------------------------------------
var MONTH_NAMES=new Array('January','February','March','April','May','June','July','August','September','October','November','December','JAN','FEB','MAR','APR','MAY','JUN','JUL','AUG','SEP','OCT','NOV','DEC');
var DAY_NAMES=new Array('Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday','Sun','Mon','Tue','Wed','Thu','Fri','Sat');
var intRoundOff =0
function LZ(x) {return(x<0||x>9?"":"0")+x}

// ------------------------------------------------------------------
// isDate ( date_string, format_string )
// Returns true if date string matches format of format string and
// is a valid date. Else returns false.
// It is recommended that you trim whitespace around the value before
// passing it to this function, as whitespace is NOT ignored!
// ------------------------------------------------------------------

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
		//    document.getElementById(txtname).focus();

		    return false;

	    }

        return true;

}

function ProductGroup(txtname)

{
        var pg = document.getElementById(txtname).value;
        pg = pg.trim();
	    if (pg == "")
	    {
	    document.getElementById("lblError").innerText="Group Name is Mandatory";
		    document.getElementById(txtname).focus();
		    return false;
	    }
        return true;

}

function CheckValidOptionalDate(txtname,errMsg)
{
if(document.getElementById(txtname).value.trim()=="")
  {  return true; }
  else
  {
  var dtDateTo = document.getElementById(txtname).value;
  if((getDateFromFormat(dtDateTo,'dd/MM/yyyy')==false) && (getDateFromFormat(dtDateTo,'dd/MM/yy')==false))
  { document.getElementById("lblError").innerText=errMsg;
    document.getElementById(txtname).focus();
    return false;
  }
  }
   return true;
}


function ProductPage()
{
 if(ddlvalidate('ddlGroupName')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Group Name is Mandatory";
		    document.getElementById('ddlGroupName').focus();
		    return false;}	
		    
		 if(textbox('txtProductName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Name is Mandatory";
		    document.getElementById('txtProductName').focus();
		    return false;}    
		    
		     if(textbox('txtVersion')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Version is Mandatory";
		    document.getElementById('txtVersion').focus();
		    return false;}
		    
		     if(textbox('txtEdition')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Edition is Mandatory";
		    document.getElementById('txtEdition').focus();
		    return false;}
		    
		     if(ddlvalidate('ddlCrs')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Crs is Mandatory";
		    document.getElementById('ddlCrs').focus();
		    return false;}
		    	     if(ddlvalidate('ddlOS')==true )
    {     }
	else{ document.getElementById("lblError").innerText="OS is Mandatory";
		    document.getElementById('ddlOS').focus();
		    return false;}
		  return true;     
	
}





function OrderPage()
{
       
if(textbox('txtOfficeID1')==true)
{
    if(textbox('txtDateApproval')==false )
    {   
        document.getElementById("lblError").innerText="Approval Date is Mandatory";
        document.getElementById('txtDateApproval').focus();
        return false;
     }    
}
if(ddlvalidate('ddlOrderStatus')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Order Status is Mandatory";
		    document.getElementById('ddlOrderStatus').focus();
		    return false;}	
 if(ddlvalidate('ddlOrderType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Order Type is Mandatory";
		    document.getElementById('ddlOrderType').focus();
		    return false;}	
		    
		 if(textbox('txtAgencyName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Agency Name is Mandatory";
	  
		      return false;}  
		      if(textbox('txtDateReceived')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Received date is Mandatory";
	document.getElementById('txtDateReceived').focus();
		      return false;}  
		      if(CheckValidOptionalDate('txtDateApproval',"Please enter correct Approval Date")==true && CheckValidOptionalDate('txtDateApplied',"Please enter correct Applied Date")==true && CheckValidOptionalDate('txtDateMessage',"Please enter correct Message sent Date")==true && CheckValidOptionalDate('txtDateExp',"Please enter correct Exp. Installation Date")==true && CheckValidOptionalDate('txtDateSentBack',"Please enter correct Message Sent back  Date")==true && CheckValidOptionalDate('txtDateMdReceiving',"Please enter correct Receiving Date")==true && CheckValidOptionalDate('txtDateMdResending',"Please enter correct Resending Date")==true  )		      
    {       }
    else
    {
    return false;
    }
	
		return true;         
}

function OrderSearchPage()
{

if(CheckValidOptionalDate('txtProcessedFrom',"Please enter correct Processed From Date")==true && CheckValidOptionalDate('txtProcessedTo',"Please enter correct Processed to Date")==true && CheckValidOptionalDate('txtReceivedFrom',"Please enter correct Received from Date")==true && CheckValidOptionalDate('txtReceivedTo',"Please enter correct Received to Date")==true && CheckValidOptionalDate('txtMessageFrom',"Please enter correct Message sent from Date")==true && CheckValidOptionalDate('txtMessageTo',"Please enter correct Message sent to Date")==true && CheckValidOptionalDate('txtApprovalFrom',"Please enter correct Approval From Date")==true && CheckValidOptionalDate('txtApprovalTo',"Please enter correct Approval to Date")==true  && CheckValidOptionalDate('txtSentFrom',"Please enter correct Sent back From Date")==true && CheckValidOptionalDate('txtSentTo',"Please enter correct Sent back to Date")==true   )
    {       }
    else
    {
    return false;
    }}


function PriorityPage()
{
    if(textbox('txtPriority')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Priority Name is Mandatory";
	        document.getElementById('txtPriority').focus();
	  
		      return false;}  
}

function QueryCategoryPage()
{
if(textbox('txtCategoryName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Category Name is Mandatory";
	        document.getElementById('txtCategoryName').focus();
	  
		      return false;}  
		      
		      if(ddlvalidate('ddlQuerySubGroup')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Sub Group is Mandatory";
		    document.getElementById('ddlQuerySubGroup').focus();
		    return false;}	
}

function QueryStatusPage()
{
    if(textbox('txtQueryStatus')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Query Status is Mandatory";
	        document.getElementById('txtQueryStatus').focus();
	  
		      return false;}  
}

function QuerySubCategoryPage()
{
if(textbox('txtSubCategoryName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Sub Category Name is Mandatory";
	        document.getElementById('txtSubCategoryName').focus();
	  
		      return false;}  
		      
		      if(ddlvalidate('ddlQuerySubGroup')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Sub Group is Mandatory";
		    document.getElementById('ddlQuerySubGroup').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlCategoryName')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Category Name is Mandatory";
		    document.getElementById('ddlCategoryName').focus();
		    return false;}	
}

function QuerySubCategoryFunctionalPage()
{
if(textbox('txtSubCategoryName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Sub Category Name is Mandatory";
	        document.getElementById('txtSubCategoryName').focus();
	  
		      return false;}  
		      
		      if(ddlvalidate('ddlQuerySubGroup')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Sub Group is Mandatory";
		    document.getElementById('ddlQuerySubGroup').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlCategoryName')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Category Name is Mandatory";
		    document.getElementById('ddlCategoryName').focus();
		    return false;}	
		    
		  	
		   /* 
		      if(ddlvalidate('ddlContactType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Contact Type is Mandatory";
		    document.getElementById('ddlContactType').focus();
		    return false;}
		    
		      if(ddlvalidate('ddlQueryStatus')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Status is Mandatory";
		    document.getElementById('ddlQueryStatus').focus();
		    return false;}
		    	
		    
		     if(ddlvalidate('ddlQueryPriority')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Priority is Mandatory";
		    document.getElementById('ddlQueryPriority').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlTeam')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Team is Mandatory";
		    document.getElementById('ddlTeam').focus();
		    return false;}	
	*/	    
		    
}

function QuerySubGroupPage()
{
  if(textbox('txtSubgroupName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Query Sub Group is Mandatory";
	        document.getElementById('txtSubgroupName').focus();
	  
		      return false;} 
}

function SearchCallLogPage()
{
if(textbox('txtQueryOpenedDateFrom')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Opened Date From is Mandatory";
	        	  
		      return false;} 
		      if(textbox('txtQueryOpenedDateTo')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Opened Date To is Mandatory";
	        	  
		      return false;} 
}


function ManageCallLogPage()
{
 
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
		      
//		      if(textbox('txtCallerName')==true )
//    {        }
//	else{ document.getElementById("lblError").innerText="Caller Name is Mandatory";	 
//	document.getElementById("txtCallerName").focus();      	  
//		      return false;} 

         if (document.getElementById('DlstCallerName')!=null)
            {
                 var CallerName=document.getElementById('DlstCallerName').value;
                 //alert(CallerName);
                 if (CallerName=='')
                   {
                     // alert(CallerName);
                       document.getElementById("lblError").innerHTML="Caller Name is Mandatory";
                       document.getElementById("DlstCallerName").focus();  
                       return false;    	  
                   }
                   else 
                   { 
                       if ( CallerName.split("-").length==2)
                       {
                           if (CallerName.split("-")[1]=='')
                           {
                               document.getElementById("lblError").innerHTML="Update SignIn for CallerName";
                               document.getElementById("DlstCallerName").focus(); 
                               return false;     	  
                           }
                       } 
                   }               
              
            }



		      
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
		    
           //	Start of Code Addedd by 
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
		     //	End of Code Addedd by 
		       
		    
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
		      
		      
		      //	Start of Code Addedd by 
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
		     //	End of Code Addedd by 
		      
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
		      
//		      if(textbox('txtCallerName')==true )
//    {        }
//	else{ document.getElementById("lblError").innerText="Caller Name is Mandatory";	 
//	document.getElementById("txtCallerName").focus();      	  
//		      return false;} 

        if (document.getElementById('DlstCallerName')!=null)
            {
                 var CallerName=document.getElementById('DlstCallerName').value;
                 //alert(CallerName);
                 if (CallerName=='')
                   {
                     // alert(CallerName);
                       document.getElementById("lblError").innerHTML="Caller Name is Mandatory";
                       document.getElementById("DlstCallerName").focus();  
                       return false;    	  
                   }
                   else 
                   { 
                        if ( CallerName.split("-").length==2)
                       {
                           if (CallerName.split("-")[1]=='')
                           {
                               document.getElementById("lblError").innerHTML="Update SignIn for CallerName";
                               document.getElementById("DlstCallerName").focus(); 
                               return false;     	  
                           }
                      } 
                   }               
              
            }


		      
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
		    
		    
		      //	Start of Code Addedd by 
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
		     //	End of Code Addedd by 
		    
		    
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
		      
		      
		     //	Start of Code Addedd by 
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
		     //	End of Code Addedd by 
		      
		      
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


/* Inventory challan Page */

var st;
   function fillSerial1InventoryChallan(s,strProductID)
   { 
      document.getElementById('hdSerial').value = document.getElementById(s).value;
      document.getElementById('hdType').value="1";            
      var id=document.getElementById(s).value + "|" + "1" + "|" + s + "|" + s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtVenderSerial" + "|" + strProductID + "|" + document.getElementById("ddlChallanCategory").value + "|" + document.getElementById("ddlChallanType").value + "|" + document.getElementById("hdChallanLCode").value + "|" + document.getElementById("ddlGodown").value + "|" + document.getElementById("hdChallanGodownId").value;
      document.getElementById(s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtVenderSerial").value="";
      if (document.getElementById(s).value != "")
      {
      CallServer(id,"This is context from client");
      return false;
      }
      else
      {
      document.getElementById(s).value="";
      document.getElementById(s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtVenderSerial").value="";
      }
   }
   
  // ----------------------------old method comment by comment by Neeraj--------------------------------------------------
  /*
   function fillSerial2InventoryChallan(s,strProductID)
   {
    document.getElementById('hdSerial').value = document.getElementById(s).value;
      document.getElementById('hdType').value ="2"
     var id=document.getElementById(s).value + "|" + "2" + "|" + s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtAmadeusSerial" + "|" + s + "|" + strProductID + "|" + document.getElementById("ddlChallanCategory").value + "|" + document.getElementById("ddlChallanType").value + "|" + document.getElementById("hdChallanLCode").value + "|" + document.getElementById("ddlGodown").value + "|" + document.getElementById("hdChallanGodownId").value;
     document.getElementById(s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtAmadeusSerial").value="";
     if (document.getElementById(s).value != "")
      {
      CallServer(id,"This is context from client");
      return false;
      }
      else
      {
      document.getElementById(s).value="";
      document.getElementById(s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtAmadeusSerial").value="";
      }
   }
   */
   //-----------------------------------end old method here-------------------------------------------------------------------
   

   //-------------------------------------- New method written by Neeraj---------------------------------------------

   function fillSerial2InventoryChallan(s,strProductID,strApplicable)
   {
   //debugger;
    
    var x;
    document.getElementById('hdSerial').value = document.getElementById(s).value;
    document.getElementById('hdType').value ="2";
    var id=document.getElementById(s).value + "|" + "2" + "|" + s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtAmadeusSerial" + "|" + s + "|" + strProductID + "|" + document.getElementById("ddlChallanCategory").value + "|" + document.getElementById("ddlChallanType").value + "|" + document.getElementById("hdChallanLCode").value + "|" + document.getElementById("ddlGodown").value + "|" + document.getElementById("hdChallanGodownId").value;
   
    if   (strApplicable=="Applicable") 
    {
        x="0";    
    }
        
   
   if (x!="0")
   {       
    document.getElementById(s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtAmadeusSerial").value="";    
     
     if (document.getElementById(s).value != "")
      {
      CallServer(id,"This is context from client");
      return false;
      }
      
      else 
      {
      document.getElementById(s).value="";
      document.getElementById(s.split('_')[0] + "_" + s.split('_')[1] + "_" + "txtAmadeusSerial").value="";
      }
   }
   
   
   } 
//--------------------------------------method end-----------------------------------------------------------------------

   
   
   // ------------------------------------------------old method comment by Neeraj-----------------------------------------
   /*
    function ReceiveServerDataInventoryChallan(args, context)
    {        
   
            document.getElementById(args.split("|")[2]).value=args.split("|")[0];
            document.getElementById(args.split("|")[3]).value=args.split("|")[1];
            
            if ((args.split("|")[0]=="") || args.split("|")[1]=="")
            {
            document.getElementById("lblError").innerHTML=args.split("|")[4];
            }
            else
            {
            document.getElementById("lblError").innerHTML="";
            }
            try
            {
            document.getElementById("btnSave").focus();
            }
            catch(err)
            {
            }
        
			
    }
   */
   //---------------------------------old method comment here----------------------------------------------------------------- 
    
   //-------------------------------------- New method written by Neeraj---------------------------------------------
    function ReceiveServerDataInventoryChallan(args, context)
    {       
     
           //debugger;
           
           if ((args.split("|")[0])!="Applicable")
           {
            document.getElementById(args.split("|")[2]).value=args.split("|")[0];
           }
           if((args.split("|")[1])!="Applicable")
           {
            document.getElementById(args.split("|")[3]).value=args.split("|")[1];
           }
            
            if ((args.split("|")[0]=="") || args.split("|")[1]=="")
            {
            document.getElementById("lblError").innerHTML=args.split("|")[4];
            }
            else
            {
            document.getElementById("lblError").innerHTML="";
            }
            try
            {
            document.getElementById("btnSave").focus();
            }
            catch(err)
            {
            }
        
			
    }
//--------------------------------------new method end-----------------------------------------------------------------------
    

    
    
    
  
    function getOrderQuantityInventoryChallan(strVal)
    {
        
        if (document.getElementById("chkOrderAmadeusIndia").checked==false)
        {
            document.getElementById("hdOrderQuantity").value=strVal.defaultValue;
        }
        else
        {
            try
            {//code to unchecked radio button 
                document.getElementById(strVal.previousSibling.previousSibling.id).checked=false;
            }
            catch(err){}
        }
                
    }
 
    function AddSelectProductPageInventoryChallan()
    {
    var prodID=document.getElementById("ddlProduct").value;
    var godownID=document.getElementById("ddlGodown").value;
    var challanType=document.getElementById("ddlChallanType").value;
    var LCode=document.getElementById("hdEnChallanLCode").value;
  /*  if(textbox('txtIssueChallanNo')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Issue Challan No is Mandatory";	 
	                  
		      return false;} */
	var issueChallanNo=document.getElementById("txtIssueChallanNo").value;   
	if(document.getElementById("ddlChallanCategory").value=="4" && document.getElementById("ddlChallanType").value=="2")
{
//godownID=document.getElementById("hdChallanGodownId").value;
if (issueChallanNo=="")
{
document.getElementById("lblError").innerText="Issue Challan No is Mandatory";	 
     return false;
}
}
else
{
issueChallanNo="";
}  
    if (prodID != "")
    {
    prodID=prodID.split("|")[0];
    }
    
    var type = "INVSR_Products.aspx?prodID=" + prodID + "&godownID=" + godownID + "&challanType=" + challanType + "&LCode=" + LCode + "&IssChallanNo=" +issueChallanNo;
   	window.open(type,"aaChallanProductListPopup","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	               	    
    return false;
    }
    
function f1()
{

ifBarCode.focus();
ifBarCode.print();
return false;
}

/*function PrintLabel()
{ if(confirm("Do You Want to print Serial Number on Labels")==true)
{

}
else
{
document.getElementById("hdPrintLabel").value="0";
}
} */

function DisableQuantityInventoryChallan()
{
var str=document.getElementById("ddlProduct").value;
var astr=str.split("|")
var strMaintainSerial=astr[2];
var strMaintainQty=astr[1];

if ( (strMaintainQty=="False") && (strMaintainSerial=="False"))
{
document.getElementById("txtQuantity").readOnly=true;
}

if ( (strMaintainQty=="True") && (strMaintainSerial=="True"))
{
document.getElementById("txtQuantity").readOnly=false;
}

if (( strMaintainQty=="True") && (strMaintainSerial=="False"))
{
document.getElementById("txtQuantity").readOnly=false;
}

if (( strMaintainQty=="False") && (strMaintainSerial=="True"))
{
document.getElementById("txtQuantity").readOnly=false;
}


}


function ColorMethodInventoryChallan(id,total)
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
            document.getElementById(Hcontrol).className="headingtabactive";
        }
       
       document.getElementById(id).className="headingtab";     
      
    //   document.getElementById('lblPanelClick').value =id; 

       if (id == (ctextFront +  "00" + ctextBack))
       {   
            document.getElementById("pnlDetails").style.display="block";
            document.getElementById("pnlProductList").style.display="none";
            document.getElementById("pnlNotes").style.display="none";            
            document.getElementById('hdTabType').value='0';        
            
            if ( document.getElementById("chkReplacementChallan").checked==true)
		    {if(document.getElementById("ddlChallanType").value=="1"){		            
		           document.getElementById("txtRplIssueChallanNo").style.display="none";}}           
            
            return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
       
        document.getElementById("pnlDetails").style.display="block";
            document.getElementById("pnlProductList").style.display="none";
            document.getElementById("pnlNotes").style.display="none"; 
            
             document.getElementById("theTabStrip_ctl00_Button1").className="headingtab" 
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive" 
            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive" 
if(ddlvalidate('ddlChallanType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Challan Type  is Mandatory";
		    document.getElementById('ddlChallanType').focus();
		    return false;}	
		    
		    var ChallanType=document.getElementById("ddlChallanCategory").value;
		    switch(ChallanType)
{
    case "1":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="block";   
            document.getElementById("pnlReplacement").style.display="none";   
            
            
            if(textbox('txtAgencyName')==true )
            {        }
	        else
	        {
	             document.getElementById("lblError").innerText="Agency Name is Mandatory";	 
	             document.getElementById("txtAgencyName").focus();    	  
		         return false;
		    }  
		    
		    if ( document.getElementById("chkReplacementChallan").checked==true)
		    {
		        if(document.getElementById("ddlChallanType").value=="2")
		        {
		            if (document.getElementById("txtRplIssueChallanNo").value=="")
		            {
		                document.getElementById("lblError").innerText="Issued challan number is Mandatory";
	                    //document.getElementById("txtRplIssueChallanNo").focus();    	  
		                return false;
		            }		            
		        }
//		        else
//		        {
//		            document.getElementById("lblError").innerText="Please select Receive challan.";
//		            return false;
//		        }
		    }                
             if (document.getElementById("chkReplacementChallan").checked==true && document.getElementById("ddlChallanType").value=="2" && document.getElementById("ddlChallanCategory").value=="1" )             
             {
                var obj = new ActiveXObject("MsXml2.DOMDocument");
                if (document.getElementById("hdChallanDetails").value.trim() != "")
                {
                    obj.loadXML(document.getElementById("hdChallanDetails").value);
                    var dsRoot=obj.documentElement;                     
                    if (dsRoot !=null)
                        {
                            var Agencyname = dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("AgencyName");
                            if (Agencyname!=null)
                            {
                                Agencyname = Agencyname.trim();
                                if (Agencyname != document.getElementById('txtAgencyName').value.trim())
                                {
                                document.getElementById("lblError").innerHTML="Challan number is not valid for this Agency.";
                                return false;
                                }
                                if (dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("ChallanType") != "2" && dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("ChallanCategory") != "1")
                                {
                                document.getElementById("lblError").innerHTML="Challan number is not valid issued replacement challan.";
                                return false;
                                }                                
                            }
                        }
                }    
                else
                {
                    document.getElementById("lblError").innerHTML="Invalid Challan number.";
                    return false;
                }
             }
		      
		   //   if ( document.getElementById("chkMiscChallan").checked==true || document.getElementById("chkOrderAmadeusIndia").checked==true )
		   
		   
		   
		      if (document.getElementById("chkReplacementChallan").checked==true || document.getElementById("chkMiscChallan").checked==true || document.getElementById("chkOrderAmadeusIndia").checked==true )
		   
		      {    
		      }
	          else
	          {
	              if(document.getElementById('gvOrder')!=null)
                  {
                    var cn=document.getElementById('gvOrder').rows.length;
                        if (cn<=1)
                        {
                            document.getElementById("lblError").innerHTML="There is no order against this agency";
                            document.getElementById("pnlDetails").style.display="block";
                            document.getElementById("pnlProductList").style.display="none";
                            document.getElementById("pnlNotes").style.display="none";            
                            document.getElementById('hdTabType').value='0';  
                            return false;
                        }
                  }
                  else
                  {
                    document.getElementById("lblError").innerHTML="There is no order against this agency";
                    document.getElementById("pnlDetails").style.display="block";
                    document.getElementById("pnlProductList").style.display="none";
                    document.getElementById("pnlNotes").style.display="none";            
                    document.getElementById('hdTabType').value='0';  
                    return false;
                  }
                  	      
                  if (document.getElementById("hdOrderQuantity").value =="")
                  {
                    document.getElementById("lblError").innerHTML="Select atleast one order number";
                    document.getElementById("pnlDetails").style.display="block";
                    document.getElementById("pnlProductList").style.display="none";
                    document.getElementById("pnlNotes").style.display="none";            
                    document.getElementById('hdTabType').value='0';  
                    return false;
                  }
	        }
                    		      
		                    /*       var cnddlOrderNo=document.getElementById('ddlOrderNo').options.length;
		                        if(cnddlOrderNo>1)
		                        {
                                if (document.getElementById("hdMandatoryOrder").value=="0")
                                {
                                if(ddlvalidate('ddlOrderNo')==true )
                        {       }
	                    else{ document.getElementById("lblError").innerText="Order No  is Mandatory";
		                        document.getElementById('ddlOrderNo').focus();
		                        return false;}	
                                }
                                }*/
                                
           break;
    case "2":
            document.getElementById("pnlPurchaseOrder").style.display="block";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="none"; 
            document.getElementById("pnlReplacement").style.display="none";    
            if(textbox('txtPurchaseOrder')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Purchase Order is Mandatory";	 
	
	                   document.getElementById("txtPurchaseOrder").focus();    	  
		      return false;} 
            break;
    case "3":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="none";
            document.getElementById("pnlReplacement").style.display="block";     
            if(textbox('txtSupplierName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Supplier Name is Mandatory";	 
	
	                   document.getElementById("txtSupplierName").focus();    	  
		      return false;} 
            break;
   case "4":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="block";
            document.getElementById("pnlCustomer").style.display="none"; 
            document.getElementById("pnlReplacement").style.display="none";  
            if(textbox('txtChallanGodownName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Godown is Mandatory";	 
	
	                   document.getElementById("txtChallanGodownName").focus();    	  
		      return false;}   
		      if (document.getElementById("hdChallanGodownId").value==document.getElementById("ddlGodown").value)
		      {
		      document.getElementById("lblError").innerText="Issue Godown can't be same as Receive Godown";		
	          document.getElementById("ddlGodown").focus();
	          return false;
		      }
		      		      if(document.getElementById("ddlChallanType").value=="2")
{document.getElementById("txtIssueChallanNo").className="textbox";
if(textbox('txtIssueChallanNo')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Issue Challan No is Mandatory";	 
	
	                   document.getElementById("txtIssueChallanNo").focus();    	  
		      return false;}  

}
            break;
   
}	    
		    
		    
		    if(ddlvalidate('ddlGodown')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Godown is Mandatory";
		    document.getElementById('ddlGodown').focus();
		    return false;}
		    
var ChallanDate=document.getElementById("txtChallanDate").value;
var RequestedDate=document.getElementById("txtRequestedDate").value;
var ApprovedDate=document.getElementById("txtApprovedDate").value;

 if(textbox('txtChallanDate')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Challan Date is Mandatory";	 
	
	                   document.getElementById("txtChallanDate").focus();    	  
		      return false;} 
if (isDate(ChallanDate.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("txtChallanDate").focus();
return false;

}
if(RequestedDate != "")
{
    if (isDate(RequestedDate.trim(),"dd/MM/yyyy")==false)
    {
    document.getElementById("lblError").innerText="Invalid date format";
    document.getElementById("txtRequestedDate").focus();
    return false;
    }
}
if(ApprovedDate != "")
{
    if (isDate(ApprovedDate.trim(),"dd/MM/yyyy")==false)
    {
    document.getElementById("lblError").innerText="Invalid date format";
    document.getElementById("txtApprovedDate").focus();
    return false;
    }
}


 if (textbox('txtChallanReceivedBy')==false)
		      {
		      document.getElementById("lblError").innerText = "Received By is Mandatory ";
		      document.getElementById("txtChallanReceivedBy").focus();
		      return false;
		      }

            document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive" 
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtab" 
            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive" 
            document.getElementById("lblError").innerText="";
            document.getElementById("pnlDetails").style.display="none";
            document.getElementById("pnlProductList").style.display="block";
            document.getElementById("pnlNotes").style.display="none";            
            document.getElementById('hdTabType').value='1';        
            return false;     
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
           document.getElementById("pnlDetails").style.display="none";
            document.getElementById("pnlProductList").style.display="none";
            document.getElementById("pnlNotes").style.display="block";            
           document.getElementById('hdTabType').value='2';        
           return false;
       }
}


function PopupPageInventoryChallan(id)
         {
         var type;
         
         if(ddlvalidate('ddlChallanType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Challan Type  is Mandatory";
		    document.getElementById('ddlChallanType').focus();
		    return false;}
         
          if (id=="1")
         {
             type = "INVSR_PurchaseOrder.aspx?Popup=T" ;
   	         window.open(type,"aaChallanPurchaseOrder","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }
         if (id=="2")
         {
                var strAgencyName=document.getElementById("txtAgencyName").value;
               type = "../TravelAgency/TASR_Agency.aspx?Popup=T&AgencyName="+strAgencyName;
   	            window.open(type,"aaChallanAgencyName","height=600,width=900,top=30,left=20,scrollbars=1,status=1");                  
          }
        
    
         if (id=="3")
         {
                document.getElementById("hdSelectEmployeeType").value="1";
                 var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                if (strEmployeePageName!="")
                {
                type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
                //type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	            window.open(type,"aaChallanEmployeeRequestedBy","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	            }
   	               	             	                    
          }
          if (id=="4")
         {
              document.getElementById("hdSelectEmployeeType").value="2";
              var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
              if (strEmployeePageName!="")
               {
                    type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
                  //type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	              window.open(type,"aaChallanEmployeeApprovedBy","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     
   	          }
          }
        if (id=="5")
         {
                document.getElementById("hdSelectEmployeeType").value="3";
                var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                if (strEmployeePageName!="")
                {
                type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
               //type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	            window.open(type,"aaChallanEmployeeReceivedBy","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     
   	            }
          }
          if (id=="6")
         {
                type = "INVSR_Supplier.aspx?Popup=T";
   	            window.open(type,"aaChallanSupplier","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     
          }
          if (id=="7")
         {
                type = "INVSR_Godown.aspx?Popup=T";
   	            window.open(type,"aaChallanGodown","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     
          }
     }


function changeChallanCategoryInventoryChallan()
{
var id=document.getElementById("ddlChallanCategory").value;
switch(id)
{
    case "1":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="block";   
            document.getElementById("pnlReplacement").style.display="none"; 
            if (document.getElementById("img5") != null)
            {
             document.getElementById("img5").className="displayNone";  
             }
            document.getElementById("txtChallanReceivedBy").className="textbox";
            document.getElementById("txtChallanReceivedBy").readOnly=false;
            break;
    case "2":
            document.getElementById("pnlPurchaseOrder").style.display="block";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="none"; 
            document.getElementById("pnlReplacement").style.display="none"; 
            if (document.getElementById("img5") != null)
            {  
            document.getElementById("img5").className="displayNone";  
            }
            document.getElementById("txtChallanReceivedBy").className="textbox";
            document.getElementById("txtChallanReceivedBy").readOnly=false; 
            break;
    case "3":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="none";
            document.getElementById("pnlReplacement").style.display="block";    
            if (document.getElementById("img5") != null)
            { 
            document.getElementById("img5").className="displayNone";  
            }
            document.getElementById("txtChallanReceivedBy").className="textbox";
            document.getElementById("txtChallanReceivedBy").readOnly=false;
            break;
   case "4":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="block";
            document.getElementById("pnlCustomer").style.display="none"; 
            document.getElementById("pnlReplacement").style.display="none";  
            if (document.getElementById("img5") != null)
            {
            document.getElementById("img5").className="";
            }
            document.getElementById("txtChallanReceivedBy").className="textboxgrey";
            document.getElementById("txtChallanReceivedBy").readOnly=true;
            
            break;
   
}
}

function changeChallanTypeInventoryChallan()
{
if(document.getElementById("ddlChallanCategory").value=="4" && document.getElementById("ddlChallanType").value=="2")
{
document.getElementById("txtIssueChallanNo").className="textbox"
document.getElementById("tdIssueChallanNo").className="textbold"
}
else
{
{
document.getElementById("txtIssueChallanNo").className="textbox displayNone"
document.getElementById("tdIssueChallanNo").className="textbold displayNone"
}
}
}

 function HideShowChallanInventoryChallan()
    {var strTabtype=document.getElementById("hdTabType").value;
    switch(strTabtype)
    {
    case "0":
           document.getElementById("pnlDetails").style.display="block";
            document.getElementById("pnlProductList").style.display="none";
            document.getElementById("pnlNotes").style.display="none";  
            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab" 
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive" 
            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive" 
             break;
    case "1":
            document.getElementById("pnlDetails").style.display="none";
            document.getElementById("pnlProductList").style.display="block";
            document.getElementById("pnlNotes").style.display="none";   
            document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive" 
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtab" 
            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive" 
             break;
     case "2":
            document.getElementById("pnlDetails").style.display="none";
            document.getElementById("pnlProductList").style.display="none";
            document.getElementById("pnlNotes").style.display="block";   
            document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive" 
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive" 
            document.getElementById("theTabStrip_ctl02_Button1").className="headingtab" 
             break;
            
    }
    
    changeChallanCategoryInventoryChallan();
    changeChallanTypeInventoryChallan();
    
    }
   
   
   function hideClearInventoryChallan(id)
   {
  
    if (id=="1")
    {
        if (document.getElementById("chkReplacementChallan").checked==true)
        {
            document.getElementById("tdReplacementChallan").innerText="Replacement Challan";
            document.getElementById("tdMiscChallan").innerText="";
            document.getElementById("chkMiscChallan").style.display="none";
            document.getElementById("chkMiscChallan").checked=false;
            document.getElementById("chkReplacementChallan").style.display="block";
            document.getElementById("trInstalledPC").style.display="block";
            if(document.getElementById("ddlChallanType").value=="2")
		    {		            
		        
		        document.getElementById("txtRplIssueChallanNo").style.display="block";
		        document.getElementById("trInstalledPC").style.display="none";		        
            }            
        }
        else
        {
            document.getElementById("tdReplacementChallan").innerText="Replacement Challan";
            document.getElementById("tdMiscChallan").innerText="Misc H/W Challan";
            document.getElementById("chkMiscChallan").style.display="block";    
            document.getElementById("chkReplacementChallan").style.display="block";
            document.getElementById("trInstalledPC").style.display="none";
            document.getElementById("txtRplIssueChallanNo").value=""
            document.getElementById("txtRplIssueChallanNo").style.display="none";
        }
    }
    else if(id=="2")
    {
    document.getElementById("tdMiscChallan").innerText="";
    if (document.getElementById("chkMiscChallan").checked==true)
        {
            document.getElementById("tdReplacementChallan").innerText="";
            document.getElementById("tdMiscChallan").innerText="Misc H/W Challan";
            document.getElementById("chkReplacementChallan").style.display="none";
            document.getElementById("chkReplacementChallan").checked=false;
             document.getElementById("chkMiscChallan").style.display="block";
             document.getElementById("trInstalledPC").style.display="none";
             document.getElementById("txtRplIssueChallanNo").style.display="none";
        }
        else
        {
            document.getElementById("tdReplacementChallan").innerText="Replacement Challan";
            document.getElementById("tdMiscChallan").innerText="Misc H/W Challan";
            document.getElementById("chkMiscChallan").style.display="block";   
            document.getElementById("chkReplacementChallan").style.display="block";                 
            document.getElementById("trInstalledPC").style.display="none";
        }
    }
    
   }
   

function InventoryChallanPage()
{
var DateFrom=document.getElementById("txtChallanDateFrom").value;
var DateTo=document.getElementById("txtChallanDateTo").value;

if(ddlvalidate('ddlChallanCategory')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Challan Category  is Mandatory";
		    document.getElementById('ddlChallanCategory').focus();
		    return false;}	

if(textbox('txtChallanDateFrom')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Date from is Mandatory";	 
	
	                   document.getElementById("txtChallanDateFrom").focus();    	  
		      return false;} 


if (isDate(DateFrom.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("txtChallanDateFrom").focus();
return false;
}


if(textbox('txtChallanDateTo')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Date to is Mandatory";	 
	
	                   document.getElementById("txtChallanDateTo").focus();    	  
		      return false;} 
if (isDate(DateTo.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("txtChallanDateTo").focus();
return false;
}

if (compareDates(DateFrom,"dd/MM/yyyy",DateTo,"dd/MM/yyyy")==1)
       {
            document.getElementById("lblError").innerText ='Date to should be greater than or equal to Date from.';
            document.getElementById("txtChallanDateFrom").focus();
            return false;
       }


}


function AddProductPage()
{

if(ddlvalidate('ddlProduct')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Product  is Mandatory";
		    document.getElementById('ddlProduct').focus();
		    return false;}	
		    
		    
 var str=document.getElementById("ddlProduct").value;
var astr=str.split("|")
var strMaintainSerial=astr[2];
var strMaintainQty=astr[1];

if ( (strMaintainQty=="False") && (strMaintainSerial=="False"))
{
document.getElementById("txtQuantity").readOnly=true;
}

if ( (strMaintainQty=="True") && (strMaintainSerial=="True"))
{
if(textbox('txtQuantity')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Quantity is Mandatory";	 
	
	                   document.getElementById("txtQuantity").focus();    	  
		      return false;} 
}

if (( strMaintainQty=="True") && (strMaintainSerial=="False"))
{
if(textbox('txtQuantity')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Quantity is Mandatory";	 
	
	                   document.getElementById("txtQuantity").focus();    	  
		      return false;} 
}

if (( strMaintainQty=="False") && (strMaintainSerial=="True"))
{
if(textbox('txtQuantity')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Quantity is Mandatory";	 
	
	                   document.getElementById("txtQuantity").focus();    	  
		      return false;} 
}
		    
		    
		    if (_isInteger(document.getElementById("txtQuantity").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed ";
		      document.getElementById("txtQuantity").focus();
		      return false;
		      }
		      else
		      {
		      var strQuantity=parseInt(document.getElementById("hdQuantity").value);
		       var strQuantity1=parseInt(document.getElementById("txtQuantity").value);
		       //Commented on 18 Sep 08
            //previously quantity check disabled for MAINTAIN_BALANCE_BY="FALSE" and MAINTAIN_BALANCE="FALSE"
            //Now quantity check enaled only when MAINTAIN_BALANCE_BY="FALSE" and MAINTAIN_BALANCE="TRUE"
		      // if( ( (strMaintainQty=="True") && (strMaintainSerial=="True")) || (( strMaintainQty=="True") && (strMaintainSerial=="False"))|| (( strMaintainQty=="False") && (strMaintainSerial=="True")))
		       if(  (strMaintainQty=="False") && (strMaintainSerial=="True"))
		       {
		       
		      // Challan Category 2 for Purchase Order.
		       //Commented on 18 Sep 08
		       //if (document.getElementById("ddlChallanCategory").value != "2")
		       //{
		           if (strQuantity1>strQuantity)
		           {
		           // code modified by Neeraj
		            document.getElementById("lblError").innerText ="Maximum quantity allowed is " + document.getElementById("hdQuantity").value;
		          document.getElementById("txtQuantity").focus();
		          return false;
		           }
		       //}
		       if (strQuantity1==0)
		       {
		        document.getElementById("lblError").innerText ="Quantity must be greater than zero";
		      document.getElementById("txtQuantity").focus();
		      return false;
		       }
		       }
		      }
}

// ----------------------------------------------Old method comment by Neeraj-----------------------------------------------------
/*
function ManageChallanPage()
{

            document.getElementById("pnlDetails").style.display="block";
            document.getElementById("pnlProductList").style.display="none";
            document.getElementById("pnlNotes").style.display="none"; 
if(ddlvalidate('ddlChallanType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Challan Type  is Mandatory";
		    document.getElementById('ddlChallanType').focus();
		    return false;}	
		    
		    var ChallanType=document.getElementById("ddlChallanCategory").value;
		    switch(ChallanType)
{
    case "1":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="block";   
            document.getElementById("pnlReplacement").style.display="none";   
            if(textbox('txtAgencyName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Agency Name is Mandatory";	 
	
	                   document.getElementById("txtAgencyName").focus();    	  
		      return false;} 		      
		    
		    if ( document.getElementById("chkReplacementChallan").checked==true)
		    {
		        if(document.getElementById("ddlChallanType").value=="2")
		        {
		            if (document.getElementById("txtRplIssueChallanNo").value=="")
		            {
		                document.getElementById("lblError").innerText="Issued challan number is Mandatory";
	                    //document.getElementById("txtRplIssueChallanNo").focus();    	  
		                return false;
		            }		    
		        }
//		        else
//		        {
//		            document.getElementById("lblError").innerText="Please select Receive challan.";
//		            return false;
//		        }
		    }
		    
		    if (document.getElementById("chkReplacementChallan").checked==true && document.getElementById("ddlChallanType").value=="2" && document.getElementById("ddlChallanCategory").value=="1" )             
             {
                var obj = new ActiveXObject("MsXml2.DOMDocument");
                if (document.getElementById("hdChallanDetails").value.trim() != "")
                {
                    obj.loadXML(document.getElementById("hdChallanDetails").value);
                    var dsRoot=obj.documentElement;                     
                    if (dsRoot !=null)
                        {
                            var Agencyname = dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("AgencyName");
                            if (Agencyname !=null)
                            {
                                Agencyname = Agencyname.trim();
                                if (Agencyname != document.getElementById('txtAgencyName').value.trim())
                                {
                                document.getElementById("lblError").innerHTML="Challan number is not valid for this Agency.";
                                return false;
                                }
                                if (dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("ChallanType") != "2" && dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("ChallanCategory") != "1")
                                {
                                document.getElementById("lblError").innerHTML="Challan number is not valid issued replacement challan.";
                                return false;
                                }                                
                            }
                        }
                }    
                else
                {
                document.getElementById("lblError").innerHTML="Invalid Challan number."
                return false;
                }
             }
		    
		      
		    /*   var cnddlOrderNo=document.getElementById('ddlOrderNo').options.length;  // this was old comment
		    if(cnddlOrderNo>1)
		    {
            if (document.getElementById("hdMandatoryOrder").value=="0")
            {
            if(ddlvalidate('ddlOrderNo')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Order No  is Mandatory";
		    document.getElementById('ddlOrderNo').focus();
		    return false;}	
            }
            }*/    // old comment
            
 /*  //new comment          
            		  if (document.getElementById("chkReplacementChallan").checked==true || document.getElementById("chkMiscChallan").checked==true || document.getElementById("chkOrderAmadeusIndia").checked==true )
		      {
	          }
	          else
	          {
	          if(document.getElementById('gvOrder')!=null)
{
var cn=document.getElementById('gvOrder').rows.length;
if (cn<=1)
{
document.getElementById("lblError").innerHTML="There is no order against this agency";
document.getElementById("pnlDetails").style.display="block";
document.getElementById("pnlProductList").style.display="none";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='0';  
return false;
}
}
else
{
document.getElementById("lblError").innerHTML="There is no order against this agency";
document.getElementById("pnlDetails").style.display="block";
document.getElementById("pnlProductList").style.display="none";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='0';  
return false;
}	      
if (document.getElementById("hdOrderQuantity").value =="")
{
document.getElementById("lblError").innerHTML="Select atleast one order number";
document.getElementById("pnlDetails").style.display="block";
document.getElementById("pnlProductList").style.display="none";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='0';  
return false;
}
	          }
            
            break;
    case "2":
            document.getElementById("pnlPurchaseOrder").style.display="block";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="none"; 
            document.getElementById("pnlReplacement").style.display="none";    
            if(textbox('txtPurchaseOrder')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Purchase Order is Mandatory";	 
	
	                   document.getElementById("txtPurchaseOrder").focus();    	  
		      return false;} 
            break;
    case "3":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="none";
            document.getElementById("pnlReplacement").style.display="block";     
            if(textbox('txtSupplierName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Supplier Name is Mandatory";	 
	
	                   document.getElementById("txtSupplierName").focus();    	  
		      return false;} 
            break;
   case "4":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="block";
            document.getElementById("pnlCustomer").style.display="none"; 
            document.getElementById("pnlReplacement").style.display="none";  
            if(textbox('txtChallanGodownName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Godown is Mandatory";	 
	
	                   document.getElementById("txtChallanGodownName").focus();    	  
		      return false;}   
		      if (document.getElementById("hdChallanGodownId").value==document.getElementById("ddlGodown").value)
		      {
		      document.getElementById("lblError").innerText="Issue Godown can't be same as Receive Godown";		
	          document.getElementById("ddlGodown").focus();
	          return false;
		      }
		      
		      if(document.getElementById("ddlChallanType").value=="2")
{document.getElementById("txtIssueChallanNo").className="textbox";
if(textbox('txtIssueChallanNo')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Issue Challan No is Mandatory";	 
	
	                   document.getElementById("txtIssueChallanNo").focus();    	  
		      return false;}  

}
		      
		      
            break;
   
}
		    
		    if(ddlvalidate('ddlGodown')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Godown is Mandatory";
		    document.getElementById('ddlGodown').focus();
		    return false;}
		    
var ChallanDate=document.getElementById("txtChallanDate").value;
var RequestedDate=document.getElementById("txtRequestedDate").value;
var ApprovedDate=document.getElementById("txtApprovedDate").value;

 if(textbox('txtChallanDate')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Challan Date is Mandatory";	 
	
	                   document.getElementById("txtChallanDate").focus();    	  
		      return false;} 
if (isDate(ChallanDate.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("txtChallanDate").focus();
return false;

}
if(RequestedDate != "")
{
if (isDate(RequestedDate.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("txtRequestedDate").focus();
return false;
}
}
if(ApprovedDate != "")
{
if (isDate(ApprovedDate.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("ApprovedDate").focus();
return false;
}
}


 if (textbox('txtChallanReceivedBy')==false)
		      {
		      document.getElementById("lblError").innerText = "Received By is Mandatory ";
		      document.getElementById("txtChallanReceivedBy").focus();
		      return false;
		      }


if(document.getElementById('gvProduct')!=null)
{
var cn=document.getElementById('gvProduct').rows.length;
if (cn<=1)
{
document.getElementById("lblError").innerHTML="Select atleast one product";
document.getElementById("pnlDetails").style.display="none";
document.getElementById("pnlProductList").style.display="block";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='1';  
document.getElementById('ddlProduct').focus();
return false;

}
}
else
{
document.getElementById("lblError").innerHTML="Select atleast one product";
document.getElementById("pnlDetails").style.display="none";
document.getElementById("pnlProductList").style.display="block";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='1';  
return false;
}
   
        for(intcnt=1;intcnt<=document.getElementById('gvProduct').rows.length-1;intcnt++)
    {   
    ///////////////////
    if (document.getElementById('gvProduct').rows[intcnt].cells[3].children.length == "1")
            {    
            if (document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].type=="text")
            { 
               if(document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].value.trim()=='')
               {
                    document.getElementById("pnlDetails").style.display="none";
                    document.getElementById("pnlProductList").style.display="block";
                    document.getElementById("pnlNotes").style.display="none";            
                    document.getElementById('hdTabType').value='1';
                    document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].focus();
                    document.getElementById("lblError").innerHTML="Quantity is Mandatory";
                    return false;
               }
               if(_isInteger(document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].value.trim())==false)
                   {
                        document.getElementById("pnlDetails").style.display="none";
                        document.getElementById("pnlProductList").style.display="block";
                        document.getElementById("pnlNotes").style.display="none";            
                        document.getElementById('hdTabType').value='1';
                        document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].focus();
                        document.getElementById("lblError").innerHTML="Only Numbers Allowed";
                        return false;
                   }
                   else
                   {
                        if(document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].value.trim()=="0")
                        {
                            document.getElementById("pnlDetails").style.display="none";
                            document.getElementById("pnlProductList").style.display="block";
                            document.getElementById("pnlNotes").style.display="none";            
                            document.getElementById('hdTabType').value='1';
                            document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].focus();
                            document.getElementById("lblError").innerHTML="Quantity must be greater than zero";
                            return false;
                         }
                   }
            }
            }
    //////////////////////////////
          if (document.getElementById('gvProduct').rows[intcnt].cells[4].children.length == "1")
            {    
            if (document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].type=="text")
            { 
               if(document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].value.trim()=='')
               {
               document.getElementById("pnlDetails").style.display="none";
        document.getElementById("pnlProductList").style.display="block";
        document.getElementById("pnlNotes").style.display="none";            
        document.getElementById('hdTabType').value='1';
               document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].focus();
                document.getElementById("lblError").innerHTML="Amadeus Serial is Mandatory";
                return false;
               }
            }
            }
            
            
            if (document.getElementById('gvProduct').rows[intcnt].cells[5].children.length == "1")
            {
            if (document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].type=="text")
            {
               if(document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].value.trim()=='')
               {
               document.getElementById("pnlDetails").style.display="none";
        document.getElementById("pnlProductList").style.display="block";
        document.getElementById("pnlNotes").style.display="none";            
        document.getElementById('hdTabType').value='1';
               document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].focus();
                document.getElementById("lblError").innerHTML="Vender Serial is Mandatory";
                return false;
               }
              } 
              }
   }
   
   for(intexcnt=1;intexcnt<=document.getElementById('gvProduct').rows.length-1;intexcnt++)
    { 
   for(intcnt=intexcnt+1;intcnt<=document.getElementById('gvProduct').rows.length-1;intcnt++)
    {    
         if (document.getElementById('gvProduct').rows[intcnt].cells[4].children.length == "1" && document.getElementById('gvProduct').rows[intexcnt].cells[4].children.length == "1" )
         {
       if (document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].type=="text")
        { 
           if( (document.getElementById('gvProduct').rows[intexcnt].cells[4].children[0].value.trim()==document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].value.trim()) && (document.getElementById('gvProduct').rows[intexcnt].cells[6].children[1].value.trim()==document.getElementById('gvProduct').rows[intcnt].cells[6].children[1].value.trim()) )
           {
           document.getElementById("pnlDetails").style.display="none";
    document.getElementById("pnlProductList").style.display="block";
    document.getElementById("pnlNotes").style.display="none";            
    document.getElementById('hdTabType').value='1';
           document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].focus();
            document.getElementById("lblError").innerHTML="Amadeus Serial number can't be duplicate";
            return false;
           }
        }
        }
        
        
        if (document.getElementById('gvProduct').rows[intcnt].cells[5].children.length == "1" && document.getElementById('gvProduct').rows[intexcnt].cells[5].children.length == "1" )
        {
        if (document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].type=="text")
        { 
           if( (document.getElementById('gvProduct').rows[intexcnt].cells[5].children[0].value.trim()==document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].value.trim()) && (document.getElementById('gvProduct').rows[intexcnt].cells[6].children[1].value.trim()==document.getElementById('gvProduct').rows[intcnt].cells[6].children[1].value.trim()))
           {
           document.getElementById("pnlDetails").style.display="none";
    document.getElementById("pnlProductList").style.display="block";
    document.getElementById("pnlNotes").style.display="none";            
    document.getElementById('hdTabType').value='1';
           document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].focus();
            document.getElementById("lblError").innerHTML="Vender Serial number can't be duplicate";
            return false;
           }
        }
        }
    }
   }
 
}
*/
//----------------------------------------------------------old method end here-------------------------------------------


//-------------------------------------- New method written by Neeraj---------------------------------------------

function ManageChallanPage()
{
            debugger;
            document.getElementById("pnlDetails").style.display="block";
            document.getElementById("pnlProductList").style.display="none";
            document.getElementById("pnlNotes").style.display="none"; 
if(ddlvalidate('ddlChallanType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Challan Type  is Mandatory";
		    document.getElementById('ddlChallanType').focus();
		    return false;}	
		    
		    var ChallanType=document.getElementById("ddlChallanCategory").value;
		    switch(ChallanType)
{
    case "1":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="block";   
            document.getElementById("pnlReplacement").style.display="none";   
            if(textbox('txtAgencyName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Agency Name is Mandatory";	 
	
	                   document.getElementById("txtAgencyName").focus();    	  
		      return false;} 		      
		    
		    if ( document.getElementById("chkReplacementChallan").checked==true)
		    {
		        if(document.getElementById("ddlChallanType").value=="2")
		        {
		            if (document.getElementById("txtRplIssueChallanNo").value=="")
		            {
		                document.getElementById("lblError").innerText="Issued challan number is Mandatory";
	                    //document.getElementById("txtRplIssueChallanNo").focus();    	  
		                return false;
		            }		    
		        }
//		        else
//		        {
//		            document.getElementById("lblError").innerText="Please select Receive challan.";
//		            return false;
//		        }
		    }
		    
		    if (document.getElementById("chkReplacementChallan").checked==true && document.getElementById("ddlChallanType").value=="2" && document.getElementById("ddlChallanCategory").value=="1" )             
             {
                var obj = new ActiveXObject("MsXml2.DOMDocument");
                if (document.getElementById("hdChallanDetails").value.trim() != "")
                {
                    obj.loadXML(document.getElementById("hdChallanDetails").value);
                    var dsRoot=obj.documentElement;                     
                    if (dsRoot !=null)
                        {
                            var Agencyname = dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("AgencyName");
                            if (Agencyname !=null)
                            {
                                Agencyname = Agencyname.trim();
                                if (Agencyname != document.getElementById('txtAgencyName').value.trim())
                                {
                                document.getElementById("lblError").innerHTML="Challan number is not valid for this Agency.";
                                return false;
                                }
                                if (dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("ChallanType") != "2" && dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("ChallanCategory") != "1")
                                {
                                document.getElementById("lblError").innerHTML="Challan number is not valid issued replacement challan.";
                                return false;
                                }                                
                            }
                        }
                }    
                else
                {
                document.getElementById("lblError").innerHTML="Invalid Challan number."
                return false;
                }
             }
		    
		      
		    /*   var cnddlOrderNo=document.getElementById('ddlOrderNo').options.length;
		    if(cnddlOrderNo>1)
		    {
            if (document.getElementById("hdMandatoryOrder").value=="0")
            {
            if(ddlvalidate('ddlOrderNo')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Order No  is Mandatory";
		    document.getElementById('ddlOrderNo').focus();
		    return false;}	
            }
            }*/
            
            		  if (document.getElementById("chkReplacementChallan").checked==true || document.getElementById("chkMiscChallan").checked==true || document.getElementById("chkOrderAmadeusIndia").checked==true )
		      {
	          }
	          else
	          {
	          if(document.getElementById('gvOrder')!=null)
{
var cn=document.getElementById('gvOrder').rows.length;
if (cn<=1)
{
document.getElementById("lblError").innerHTML="There is no order against this agency";
document.getElementById("pnlDetails").style.display="block";
document.getElementById("pnlProductList").style.display="none";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='0';  
return false;
}
}
else
{
document.getElementById("lblError").innerHTML="There is no order against this agency";
document.getElementById("pnlDetails").style.display="block";
document.getElementById("pnlProductList").style.display="none";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='0';  
return false;
}	      
if (document.getElementById("hdOrderQuantity").value =="")
{
document.getElementById("lblError").innerHTML="Select atleast one order number";
document.getElementById("pnlDetails").style.display="block";
document.getElementById("pnlProductList").style.display="none";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='0';  
return false;
}
	          }
            
            break;
    case "2":
            document.getElementById("pnlPurchaseOrder").style.display="block";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="none"; 
            document.getElementById("pnlReplacement").style.display="none";    
            if(textbox('txtPurchaseOrder')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Purchase Order is Mandatory";	 
	
	                   document.getElementById("txtPurchaseOrder").focus();    	  
		      return false;} 
            break;
    case "3":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="none";
            document.getElementById("pnlCustomer").style.display="none";
            document.getElementById("pnlReplacement").style.display="block";     
            if(textbox('txtSupplierName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Supplier Name is Mandatory";	 
	
	                   document.getElementById("txtSupplierName").focus();    	  
		      return false;} 
            break;
   case "4":
            document.getElementById("pnlPurchaseOrder").style.display="none";
            document.getElementById("pnlStockTransfer").style.display="block";
            document.getElementById("pnlCustomer").style.display="none"; 
            document.getElementById("pnlReplacement").style.display="none";  
            if(textbox('txtChallanGodownName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Godown is Mandatory";	 
	
	                   document.getElementById("txtChallanGodownName").focus();    	  
		      return false;}   
		      if (document.getElementById("hdChallanGodownId").value==document.getElementById("ddlGodown").value)
		      {
		      document.getElementById("lblError").innerText="Issue Godown can't be same as Receive Godown";		
	          document.getElementById("ddlGodown").focus();
	          return false;
		      }
		      
		      if(document.getElementById("ddlChallanType").value=="2")
{document.getElementById("txtIssueChallanNo").className="textbox";
if(textbox('txtIssueChallanNo')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Issue Challan No is Mandatory";	 
	
	                   document.getElementById("txtIssueChallanNo").focus();    	  
		      return false;}  

}
		      
		      
            break;
   
}
	    
		    
		    if(ddlvalidate('ddlGodown')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Godown is Mandatory";
		    document.getElementById('ddlGodown').focus();
		    return false;}
		    
var ChallanDate=document.getElementById("txtChallanDate").value;
var RequestedDate=document.getElementById("txtRequestedDate").value;
var ApprovedDate=document.getElementById("txtApprovedDate").value;

 if(textbox('txtChallanDate')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Challan Date is Mandatory";	 
	
	                   document.getElementById("txtChallanDate").focus();    	  
		      return false;} 
if (isDate(ChallanDate.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("txtChallanDate").focus();
return false;

}
if(RequestedDate != "")
{
if (isDate(RequestedDate.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("txtRequestedDate").focus();
return false;
}
}
if(ApprovedDate != "")
{
if (isDate(ApprovedDate.trim(),"dd/MM/yyyy")==false)
{
document.getElementById("lblError").innerText="Invalid date format";
document.getElementById("ApprovedDate").focus();
return false;
}
}


 if (textbox('txtChallanReceivedBy')==false)
		      {
		      document.getElementById("lblError").innerText = "Received By is Mandatory ";
		      document.getElementById("txtChallanReceivedBy").focus();
		      return false;
		      }


if(document.getElementById('gvProduct')!=null)
{
var cn=document.getElementById('gvProduct').rows.length;
if (cn<=1)
{
document.getElementById("lblError").innerHTML="Select atleast one product";
document.getElementById("pnlDetails").style.display="none";
document.getElementById("pnlProductList").style.display="block";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='1';  
document.getElementById('ddlProduct').focus();
return false;

}
}
else
{
document.getElementById("lblError").innerHTML="Select atleast one product";
document.getElementById("pnlDetails").style.display="none";
document.getElementById("pnlProductList").style.display="block";
document.getElementById("pnlNotes").style.display="none";            
document.getElementById('hdTabType').value='1';  
return false;
}
   
        for(intcnt=1;intcnt<=document.getElementById('gvProduct').rows.length-1;intcnt++)
    {   
    ///////////////////
    if (document.getElementById('gvProduct').rows[intcnt].cells[3].children.length == "1")
            {    
            if (document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].type=="text")
            { 
               if(document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].value.trim()=='')
               {
                    document.getElementById("pnlDetails").style.display="none";
                    document.getElementById("pnlProductList").style.display="block";
                    document.getElementById("pnlNotes").style.display="none";            
                    document.getElementById('hdTabType').value='1';
                    document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].focus();
                    document.getElementById("lblError").innerHTML="Quantity is Mandatory";
                    return false;
               }
               if(_isInteger(document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].value.trim())==false)
                   {
                        document.getElementById("pnlDetails").style.display="none";
                        document.getElementById("pnlProductList").style.display="block";
                        document.getElementById("pnlNotes").style.display="none";            
                        document.getElementById('hdTabType').value='1';
                        document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].focus();
                        document.getElementById("lblError").innerHTML="Only Numbers Allowed";
                        return false;
                   }
                   else
                   {
                        if(document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].value.trim()=="0")
                        {
                            document.getElementById("pnlDetails").style.display="none";
                            document.getElementById("pnlProductList").style.display="block";
                            document.getElementById("pnlNotes").style.display="none";            
                            document.getElementById('hdTabType').value='1';
                            document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].focus();
                            document.getElementById("lblError").innerHTML="Quantity must be greater than zero";
                            return false;
                         }
                   }
            }
            }
    //////////////////////////////
    
    
    
   //Neeraj on date 14-01-2011 for serial number
    if  (((document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "False" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="False") || (document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "True" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="False")) || (document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "True" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="True"))
     {               
      }
   //end here    
   else if (document.getElementById('gvProduct').rows[intcnt].cells[4].children.length == "1")
            {    
            if (document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].type=="text")
                { 
               if(document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].value.trim()=='')
                    {
                     document.getElementById("pnlDetails").style.display="none";
                     document.getElementById("pnlProductList").style.display="block";
                     document.getElementById("pnlNotes").style.display="none";            
                     document.getElementById('hdTabType').value='1';
                     document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].focus();
                     //alert(document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim());  // maintain by
                     document.getElementById("lblError").innerHTML="Amadeus Serial is Mandatory";
                     return false;
                    }
                }
            }
            
           
    
    //Neeraj on date 14-01-2011 for vender serial number
    
    if (((document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "False" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="False") || (document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "True" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="False")) || (document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "True" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="True"))             
       {
        // return true;
       }
    //end here    
    else if (document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "True" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim()=="False") 
                        {
                                if  (document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "True" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim()=="True")
                                {
                                   if (document.getElementById('gvProduct').rows[intcnt].cells[5].children.length == "1")
                                           {
                                             if (document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].type=="text")
                                                {
                                                 if(document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].value.trim()=='')
                                                     {
                                                            document.getElementById("pnlDetails").style.display="none";
                                                            document.getElementById("pnlProductList").style.display="block";
                                                            document.getElementById("pnlNotes").style.display="none";            
                                                            document.getElementById('hdTabType').value='1';
                                                            document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].focus();
                                                            document.getElementById("lblError").innerHTML="Vender Serial is Mandatory";
                                                            return false;
                                                      }
                                                  } 
                                            }
         
                            }
                        } 
  
   }  // for loop end
   
   for(intexcnt=1;intexcnt<=document.getElementById('gvProduct').rows.length-1;intexcnt++)
    { 
   for(intcnt=intexcnt+1;intcnt<=document.getElementById('gvProduct').rows.length-1;intcnt++)
    {       
    
    
      if    	(((document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "False" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="False") || (document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "True" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="False")) || (document.getElementById('gvProduct').rows[intcnt].cells[6].children[2].value.trim() == "True" &&  document.getElementById('gvProduct').rows[intcnt].cells[6].children[3].value.trim()=="True"))
             
               {
                // return true;
               }
    //end here
     
        
       else if (document.getElementById('gvProduct').rows[intcnt].cells[4].children.length == "1" && document.getElementById('gvProduct').rows[intexcnt].cells[4].children.length == "1" )
            {
              if (document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].type=="text")
               { 
                 if ((document.getElementById('gvProduct').rows[intexcnt].cells[4].children[0].value.trim()==document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].value.trim()) && (document.getElementById('gvProduct').rows[intexcnt].cells[6].children[1].value.trim()==document.getElementById('gvProduct').rows[intcnt].cells[6].children[1].value.trim()) )
                     {
                        document.getElementById("pnlDetails").style.display="none";
                        document.getElementById("pnlProductList").style.display="block";
                        document.getElementById("pnlNotes").style.display="none";            
                        document.getElementById('hdTabType').value='1';
                        document.getElementById('gvProduct').rows[intcnt].cells[4].children[0].focus();
                        document.getElementById("lblError").innerHTML="Amadeus Serial number can't be duplicate";
                        return false;
                       }
                }
             }
                                 
        
       
     else if    (document.getElementById('gvProduct').rows[intcnt].cells[5].children.length == "1" && document.getElementById('gvProduct').rows[intexcnt].cells[5].children.length == "1" )
        {
        if (document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].type=="text")
            { 
                if((document.getElementById('gvProduct').rows[intexcnt].cells[5].children[0].value.trim()==document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].value.trim()) && (document.getElementById('gvProduct').rows[intexcnt].cells[6].children[1].value.trim()==document.getElementById('gvProduct').rows[intcnt].cells[6].children[1].value.trim()))
                    {
                    document.getElementById("pnlDetails").style.display="none";
                    document.getElementById("pnlProductList").style.display="block";
                    document.getElementById("pnlNotes").style.display="none";            
                    document.getElementById('hdTabType').value='1';
                    document.getElementById('gvProduct').rows[intcnt].cells[5].children[0].focus();
                    document.getElementById("lblError").innerHTML="Vender Serial number can't be duplicate";
                    return false;
                    }
            }
         }
        
    
        
    } //for loop end
   }  //for loop end
 
}  //method end


//--------------------------------------method end-----------------------------------------------------------------------
 

function  challanExecuteButtonPage()
{
if(ddlvalidate('ddlGodown')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Godown is Mandatory";
		    document.getElementById('ddlGodown').focus();
		    return false;}
		   if( confirm('Challan once executed cannot be modified. Continue ?') ==true)
		   {
		   return true;
		   }
		   else
		   {
		   return false;
		   }
}

function TrainingRoomUpdatePage()
{

if(ddlvalidate('ddlAOffice')==true )
    {       }
	else{ document.getElementById("lblError").innerText="AOffice is Mandatory";
		    document.getElementById('ddlAOffice').focus();
		    return false;}
		    
		    if(ddlvalidate('ddlCity')==true )
    {       }
	else{ 
		    try
		    {
		    document.getElementById('ddlCity').focus();
		    document.getElementById("lblError").innerText="City is Mandatory";
		    return false;
		    }
		    catch(err){}
		   }

if (textbox('txtTrainingRoom')==false)
		      {
		      document.getElementById("lblError").innerText = "Training Room is Mandatory ";
		      document.getElementById("txtTrainingRoom").focus();
		      return false;
		      }
}

function TrainingRoomCourseUpdatePage()
{
if(ddlvalidate('ddlCourse')==true )
    {       }
	else{ document.getElementById("lblError").innerText="ddlCourse is Mandatory";
		    document.getElementById('ddlCourse').focus();
		    return false;}
}

function ManageCourseSessionPage()
{

if (ddlvalidate('ddlCourseTitle')==false)
		      {
		      document.getElementById("lblError").innerText = "Course Title is Mandatory ";
		      document.getElementById('ddlCourseTitle').focus();
		      return false;
		      }

if (textbox('txtTrainingRoom')==false)
		      {
		      document.getElementById("lblError").innerText = "Training Room is Mandatory ";
		      
		      return false;
		      }
		      
		   if(ddlvalidate('ddlTrainer1')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Trainer1 is Mandatory";
		    document.getElementById('ddlTrainer1').focus();
		    return false;}   
		    
		    if (textbox('txtStartDate')==false)
		      {
		      document.getElementById("lblError").innerText = "Start Date is Mandatory ";
		     document.getElementById('txtStartDate').focus();
		      return false;
		      }
		    if (isDate(document.getElementById('txtStartDate').value,"d/M/yyyy") == false)	
            {
                document.getElementById('lblError').innerText = "Start date is not valid.";			
	            document.getElementById('txtStartDate').focus();
	            return(false);  
            }
		      
		       if (textbox('txtStartTimeHH')==false)
		      {
		      document.getElementById("lblError").innerText = "Start Time is Mandatory ";
		     document.getElementById('txtStartTimeHH').focus();
		      return false;
		      }
		      
		      if (textbox('txtStartTimeMM')==false)
		      {
		      document.getElementById("lblError").innerText = "Start Time is Mandatory ";
		     document.getElementById('txtStartTimeMM').focus();
		      return false;
		      }
		      
		      if (_isInteger(document.getElementById("txtStartTimeHH").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 23";
		      document.getElementById("txtStartTimeHH").focus();
		      return false;
		      }

              if (_isInteger(document.getElementById("txtStartTimeMM").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 59";
		      document.getElementById("txtStartTimeMM").focus();
		      return false;
		      }
		      
		     var intStartTimeHH=parseInt(document.getElementById("txtStartTimeHH").value,10);
		      
		        if((intStartTimeHH > 23)  || (intStartTimeHH < 0))
		        {
		        document.getElementById("lblError").innerText = "Hours must be in between 0 to 23";
		        document.getElementById("txtStartTimeHH").focus();
		        return false;
		        }
		    
            var intStartTimeMM=parseInt(document.getElementById("txtStartTimeMM").value,10);
		      
		        if((intStartTimeHH > 59)  || (intStartTimeHH < 0))
		        {
		        document.getElementById("lblError").innerText = "Minutes must be in between 0 to 59";
		        document.getElementById("txtStartTimeMM").focus();
		        return false;
		        }
		     
		     
		      
		      if (textbox('txtEndDate')==false)
		      {
		      document.getElementById("lblError").innerText = "End Date time is Mandatory ";
		     document.getElementById('txtEndDate').focus();
		      return false;
		      }
		      if (isDate(document.getElementById('txtEndDate').value,"d/M/yyyy") == false)	
            {
                document.getElementById('lblError').innerText = "End date is not valid.";			
	            document.getElementById('txtEndDate').focus();
	            return(false);  
            }
            
              if (textbox('txtEndTimeHH')==false)
		      {
		      document.getElementById("lblError").innerText = "End time is Mandatory ";
		     document.getElementById('txtEndTimeHH').focus();
		      return false;
		      }
            
            if (textbox('txtEndTimeMM')==false)
		      {
		      document.getElementById("lblError").innerText = "End time is Mandatory ";
		     document.getElementById('txtEndTimeMM').focus();
		      return false;
		      }
		      
		        if (_isInteger(document.getElementById("txtEndTimeHH").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 23";
		      document.getElementById("txtEndTimeHH").focus();
		      return false;
		      }

              if (_isInteger(document.getElementById("txtEndTimeMM").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 59";
		      document.getElementById("txtEndTimeMM").focus();
		      return false;
		      }
		      
		     var intEndTimeHH=parseInt(document.getElementById("txtEndTimeHH").value,10);
		      
		        if((intEndTimeHH> 23)  || (intEndTimeHH< 0))
		        {
		        document.getElementById("lblError").innerText = "Hours must be in between 0 to 23";
		        document.getElementById("txtEndTimeHH").focus();
		        return false;
		        }
		    
            var intEndTimeMM=parseInt(document.getElementById("txtEndTimeMM").value,10);
		      
		        if((intEndTimeMM > 59)  || (intEndTimeMM < 0))
		        {
		        document.getElementById("lblError").innerText = "Minutes must be in between 0 to 59";
		        document.getElementById("txtEndTimeMM").focus();
		        return false;
		        }
		    
            
		      //var dateFrom =document.getElementById("txtStartDate").value.split(' ')[0];
		     //var dateTo =document.getElementById("txtEndDate").value.split(' ')[0];
		     
		      var dateFrom =document.getElementById("txtStartDate").value;
		     var dateTo =document.getElementById("txtEndDate").value;
		     
		       if (compareDates(dateFrom,"d/M/yyyy",dateTo,"d/M/yyyy")==1)
         {
            document.getElementById('lblError').innerText ='End date time  should be greater than Start date time .';
            document.getElementById('txtStartDate').focus();
            return false;
         }
       
       if (dateFrom==dateTo)
       {
          // var startTimeMinSec=document.getElementById("txtStartDate").value.split(' ')[1];
         //  var endTimeMinSec=document.getElementById("txtEndDate").value.split(' ')[1];
          
          var startTimeMM=parseInt(document.getElementById("txtStartTimeMM").value,10);
           var endTimeMM=parseInt(document.getElementById("txtEndTimeMM").value,10);
           
            var startTimeHH=parseInt(document.getElementById("txtStartTimeHH").value,10);
           var endTimeHH=parseInt(document.getElementById("txtEndTimeHH").value,10);
            //var startTimeMin=parseInt(startTimeMinSec.split(':')[0],10);
            //var endTimeMin=parseInt(endTimeMinSec.split(':')[0],10);
           
            if (startTimeHH>endTimeHH)
            {
             document.getElementById('lblError').innerText ='End time should be greater than Start time .';
                return false;
            }
             if (startTimeHH==endTimeHH)
            {
            
              /*  var startTimeSec=parseInt(startTimeMinSec.split(':')[1],10);
                var endTimeSec=endTimeMinSec.split(':')[1];
                endTimeSec=parseInt(endTimeSec,10);*/
            
                if (startTimeMM>=endTimeMM)
                {
                     document.getElementById('lblError').innerText ='End time should be greater than Start time .';
                    return false;
                }
		     }
		 }
		
		   var parseDateFrom=parseDate(dateFrom.split('/')[1] + "/" + dateFrom.split('/')[0] + "/" + dateFrom.split('/')[2]);
		   var  parseDateTo=parseDate(dateTo.split('/')[1] + "/" + dateTo.split('/')[0] + "/" + dateTo.split('/')[2]);
		      var days=days_between(parseDateFrom, parseDateTo) 
		      var duration =parseInt(Math.ceil(document.getElementById("hdDuration").value));
		      
		       if ((parseInt(days)+1) !=duration)
		       {
		           if (confirm('Difference between Start Date and End Date does not match with Course duration. Save ?')==false)
		           {
		            return false;
		           }
		       }
		      
}

function ValidateParticipantBasketPage()
{
if (textbox('txtAgencyName')==false)
		      {
		      document.getElementById("lblError").innerText = "Agency Name is Mandatory ";
		      
		      return false;
		      }
		      
		      if (textbox('txtAgencyStaff')==false)
		      {
		      document.getElementById("lblError").innerText = "Agency Staff is Mandatory ";
		      
		      return false;
		      }
		   /*   if (textbox('txtDate')==false)
		      {
		      document.getElementById("lblError").innerText = "Date is Mandatory ";
		      document.getElementById("txtDate").focus();
		      return false;
		      }
		      var strDate=document.getElementById("txtDate").value;
		      if (isDate(strDate.trim(),"dd/MM/yyyy")==false)
                {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtDate").focus();
                    return false;

                }
                */
               /* if (textbox('txtPreferredDate')==false)
		      {
		      document.getElementById("lblError").innerText = "Preferred Date is Mandatory ";
		      document.getElementById("txtPreferredDate").focus();
		      return false;
		      }*/
                
                var strPreferredDate=document.getElementById("txtPreferredDate").value;
                if (strPreferredDate.trim() != "")
                {
		      if (isDate(strPreferredDate.trim(),"dd/MM/yyyy")==false)
                {
                    document.getElementById("lblError").innerText="Invalid Preferred date format";
                    document.getElementById("txtPreferredDate").focus();
                    return false;

                }
                }
                
                if(ddlvalidate('ddlAOffice')==true )
    {       }
	else{ document.getElementById("lblError").innerText="AOffice is Mandatory";
		    document.getElementById('ddlAOffice').focus();
		    return false;}   
		 /*    if(ddlvalidate('ddlStatus')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Status is Mandatory";
		    document.getElementById('ddlStatus').focus();
		    return false;}   */
		    
		   if(ddlvalidate('ddlCourse')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Course is Mandatory";
		    document.getElementById('ddlCourse').focus();
		    return false;}   
		    
		   
}

 
function ValidateRegisterPage()
{

    var strPagePracTotalMarks=document.getElementById('hdTotalPracticalMarks').value;
    var strTotalMarks="";
    if(document.getElementById('gvRegisterTab')!=null)
    {
        var cn=document.getElementById('gvRegisterTab').rows.length;
        if (cn<=1)
        {
        document.getElementById("lblError").innerHTML="Register atleast one candidate";
        return false;
        }
    }
    else
    { 
        document.getElementById("lblError").innerHTML="Register atleast one candidate";
         
        return false;
    }
  
        for(intcnt=1;intcnt<=document.getElementById('gvRegisterTab').rows.length-1;intcnt++)
    {   
     if (document.getElementById('gvRegisterTab').rows[intcnt].cells[5].children.length == "1")
    {    
    if (document.getElementById('gvRegisterTab').rows[intcnt].cells[5].children[0].type=="text")
    { 
   
   var strStatus = document.getElementById('gvRegisterTab').rows[intcnt].cells[4].children[0].id;
   var item=document.getElementById(strStatus).selectedIndex;
var text=document.getElementById(strStatus).options[item].text;
 var strValue = document.getElementById('gvRegisterTab').rows[intcnt].cells[5].children[0].value.trim();
 var strValue1 = document.getElementById('gvRegisterTab').rows[intcnt].cells[6].children[0].value.trim();
 var strTotalMarks = document.getElementById('gvRegisterTab').rows[intcnt].cells[6].children[1].value.trim();
 text=text.toUpperCase();
if (text=="certified" || text=="not certified" || text=="CERTIFIED" || text=="NOT CERTIFIED" || text=="Certified" || text=="Not Certified")
{
    if (strValue=='')
    {
    //document.getElementById('lblError').innerText ='Result is mandatory.' 
    //document.getElementById(document.getElementById('gvRegisterTab').rows[intcnt].cells[5].children[0].id).focus();            
      //              return false;
    }
}
else
{
    document.getElementById('gvRegisterTab').rows[intcnt].cells[6].children[0].value="";
}
   
    // var strValue = document.getElementById('gvRegisterTab').rows[intcnt].cells[5].children[0].value.trim();
          if (strValue1 != '')
          {
            reg = new RegExp("^[0-9.]+$"); 
            if(reg.test(strValue1) == false) 
            {
                document.getElementById('lblError').innerText ='Only Number allowed.'
              
                return false;

             }
             else
                 {
                    document.getElementById('lblError').innerText ='';
                 }
                 
                 if (strTotalMarks.trim()!="")
                 {
                     if (parseFloat(strValue1)>parseFloat(strTotalMarks))
                     {
                        document.getElementById('lblError').innerText ='P Marks can not be greater than Practical Marks ';
                        
                        return false;
                     
                     }
                     else
                     {
                            document.getElementById('lblError').innerText ='';
                     }
                 }
                 else
                 {
                     if (parseFloat(strValue1)>parseFloat(strPagePracTotalMarks))
                     {
                        document.getElementById('lblError').innerText ='P Marks can not be greater than Practical Marks ';
                        
                        return false;
                     
                     }
                     else
                     {
                            document.getElementById('lblError').innerText ='';
                     }
                 }
                 
             /*if (strValue1 != '')
             {
                var tempTotal =strValue+strValue1
                if (tempTotal>strTotalMarks)
                {
                document.getElementById('lblError').innerText ='Result can not be greater than Total Marks';
                return false;             
                }  
             }*/
           }
      
    }
    }
  
   }
}



function ValidateAgencyTrainingPage()
{

            if (textbox('txtUserName')==false)
		      {
		      document.getElementById("lblError").innerText = "User Name is Mandatory ";
		      document.getElementById("txtUserName").focus();
		      return false;
		      }
		      
		      
		      if (textbox('txtPassword')==false)
		      {
		      document.getElementById("lblError").innerText = "Password is Mandatory ";
		      document.getElementById("txtPassword").focus();
		      return false;
		      }
}


function ManageTrainingLetter(buttonText)
{
    
    if(document.getElementById('gvParticipant')==null)
    { 
        document.getElementById("lblError").innerHTML="No Participant for the operation";
        return false;
    }
     var cn=0;
    
        for(intcnt=1;intcnt<=document.getElementById('gvParticipant').rows.length-1;intcnt++)
        {   //length == 3 because it contains checkbox and 2 hidden fields
         if (document.getElementById('gvParticipant').rows[intcnt].cells[0].children.length == 3)
        {    
            if (document.getElementById('gvParticipant').rows[intcnt].cells[0].children[0].type=="checkbox" && document.getElementById('gvParticipant').rows[intcnt].cells[0].children[0].checked==true)
            { 
                cn=1;
                if (buttonText!='Print All')
                {
                 var strEmailText=document.getElementById('gvParticipant').rows[intcnt].cells[2].children[0].value
                            var arstrEmailText=strEmailText.split(",")
                            for(i=0;i<arstrEmailText.length;i++)
                            {
                                if(checkEmail(arstrEmailText[i])==false)
                                 {  
                                    try
                                    {
                                   
                                    document.getElementById('hdTabType').value='1';
                                    document.getElementById("pnlTemplate").style.display="none";
                                    document.getElementById("pnlList").style.display="block";
                                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                                     switch("1")
                                    {
                                    case "0":
                                           document.getElementById("theTabStrip_ctl00_Button1").className="headingtab";
                                             break;
                                    case "1":
                                           document.getElementById("theTabStrip_ctl01_Button1").className="headingtab";
                                             break;
                                    }
                                    document.getElementById("lblError").innerHTML='Enter valid email Id.';
                                    document.getElementById(document.getElementById('gvParticipant').rows[intcnt].cells[2].children[0].id).focus();
                                    return false;
                                    }
                                    catch(err)
                                    { return false;
                                    }
                                 }
                            }
                  }  
            }
        }
  
    
  }
        if (cn==0)
        {
                        try
                        {
                            document.getElementById('hdTabType').value='1';
                            document.getElementById("pnlTemplate").style.display="none";
                            document.getElementById("pnlList").style.display="block";
                            document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                             switch("1")
                            {
                            case "0":
                                   document.getElementById("theTabStrip_ctl00_Button1").className="headingtab";
                                     break;
                            case "1":
                                   document.getElementById("theTabStrip_ctl01_Button1").className="headingtab";
                                     break;
                            }
                            document.getElementById("lblError").innerHTML='You have not selected any participant';
                            return false;
                        }
                        catch(err)
                        { return false;
                        }
        }
        
         document.getElementById('hdnmsg').value=NewsBody_rich.document.body.innerHTML;
}

/*Search Call Log*/

    function ValidateFormCallLog()
    {
    
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
                
        function EditFunctionCallLog(LCode,HD_RE_ID,strStatus)
        {           
          window.location.href="HDUP_CallLog.aspx?Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus;               
          return false;
        }
        
        
         function SelectFunctionCallLog(HD_RE_ID,strStatus,OfficeID,AgencyName,HD_RE_OPEN_DATE,LoggedBy,HD_QUERY_GROUP_NAME,HD_STATUS_NAME)
        {           
      
         if (window.opener.document.forms['form1']['hdPageHD_RE_ID']!=null)
        { 
        window.opener.document.forms['form1']['hdCHD_RE_ID'].value=HD_RE_ID;
        //window.opener.document.forms['form1']['hdPageStatus'].value=strStatus;
         window.opener.document.forms['form1']['hdOfficeID'].value=OfficeID;
        window.opener.document.forms['form1']['hdAgencyName'].value=AgencyName;
         window.opener.document.forms['form1']['hdHD_RE_OPEN_DATE'].value=HD_RE_OPEN_DATE;
        window.opener.document.forms['form1']['hdLoggedBy'].value=LoggedBy;
         window.opener.document.forms['form1']['hdHD_QUERY_GROUP_NAME'].value=HD_QUERY_GROUP_NAME;
        window.opener.document.forms['form1']['hdHD_STATUS_NAME'].value=HD_STATUS_NAME;
        
        
      //  var Lcodes=document.getElementById("hdLcodeMuk").value;
     //   var HLtrCodes=document.getElementById("hdReIDMuk").value;
        
        window.opener.document.forms['form1'].submit();
        
      //  window.location.href="HDUP_LinkedLTR.aspx?Action=PP&Reid="+ HD_RE_ID + "&LcodesMuk=" + Lcodes + "&HLtrCodesMuk=" + HLtrCodes ;
        window.close();
      
        //window.opener.document.submit();
        
      
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
 function PopupPageCallLog(id)
         {
         var type;
         if (id=="1")
         {
                
             var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
            if (strEmployeePageName!="")
            {
               type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
              //  type = "../Setup/MSSR_Employee.aspx?Popup=T";
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
    
    
    
     function SelectAllProduct() 
    {
       CheckAllDataGridCheckBoxesChallan(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxesChallan(value) 
    {
   
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
            try
            {
                var elm = document.forms[0].elements[i]; 
                if(elm.type == 'checkbox') 
                {
                    var chkId=elm.id;
                    if (chkId.split("_")[0]=="gvProduct")
                    {
                        if (chkId.split("_")[2]=="chkSelect")
                        {
                            elm.checked = value
                        }
                    }
                }
              }
              catch(err){}
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
/* End of Search Call */
/* For TEchnical Call Plz donot repeat these function names*/
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

function PopupPage(id)
         {
      
         var type;
          if (id=="1")
         {
             type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	         window.open(type,"Taa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }
         if (id=="2")
         {
                  var Loc_code;
                  Loc_code="";
                   var CallerNames;
                   CallerNames=''
                   
                    if (document.getElementById("DlstCallerName") !=null)
                 {
                     CallerNames= document.getElementById("DlstCallerName").value;
                 } 
                  
                 if (document.getElementById("hdEnCallAgencyName_LCODE") !=null)
                 {
                     Loc_code= document.getElementById("hdEnCallAgencyName_LCODE").value;
                 } 
               
                var strAgencyName=document.getElementById("txtAgencyName").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                if (strAgencyName!="")
                {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&Source=HD&Lcode=" + Loc_code   +  "&AgencyName="+strAgencyName + "&CallerName=" + CallerNames;
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
                type = "../HelpDesk/HDSR_Ptr.aspx?Popup=T";
   	            window.open(type,"TaaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
          }
          if (id=="4")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="../Popup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
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


//*************************************Start Technical Call Log Methods*****************************************

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
 //*************************************End Technical Call Log Methods*****************************************

//*************************************Start Fucnctional Call Log Methods*****************************************


 function YesNoFunctional()
   {
       try
       {
            var strReturn;
            var msg =document.getElementById("hdMsg").value;
            if (msg!=null)
            {
            var type = "../popup/confirmwindow.htm?Msg=" + msg;
            strReturn = showPopWin(type, 350, 120, returnRefreshFunctional,null); 
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
function returnRefreshFunctional(returnVal) 
    {
       
         if (returnVal != null)
         {    
            if (returnVal=="1")
            {   
               document.getElementById("hdReSave").value="1";
              // __doPostBack('btnSave','');          
              document.forms['form1'].submit();
            }
            else
            {
            document.getElementById("ddlQuerySubGroup").selectedIndex=0;
            }
            
	     }  
	     
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


function fnCallLogID()
    {   
    if (document.getElementById("hdPageHD_RE_ID").value != "")
    {
         if (window.opener.document.forms['form1']['hdEHD_RE_ID']!=null)
        { 
    /*     var ind =document.getElementById("ddlQueryStatus").selectedIndex;
        if (ind ==0)
        {
        document.getElementById("lblError").innerText="Query Status is Mandatory";
        return false;
        }
        else
        {
        var text1=document.getElementById("ddlQueryStatus").options[ind].text;*/
        window.opener.document.forms['form1']['hdEHD_RE_ID'].value=document.getElementById("hdPageHD_RE_ID").value;
        window.opener.document.forms['form1']['hdPopupStatus'].value=document.getElementById("hdQueryStatus").value;
        window.opener.document.forms['form1'].submit();
        window.close();
        return false;
      //  }
        }    
    }
    else
    {
    window.close();
        return false;
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
      
            window.location.href="HDUP_helpDeskFeedBack.aspx?Action=U&strStatus=" + strStatus + "&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;   
            return false;
      
            
           
       }
       
  
     //  }                               
       
}


function PopupPageFunctional(id)
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
       
          if (id=="1")
         {
             type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	         window.open(type,"aa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }
         if (id=="2")
         {
                   var Loc_code;
                   Loc_code=''
                   var CallerNames;
                   CallerNames=''
                   
                    if (document.getElementById("DlstCallerName") !=null)
                 {
                     CallerNames= document.getElementById("DlstCallerName").value;
                 } 
                   
                 if (document.getElementById("hdCallAgencyName") !=null)
                 {
                     Loc_code= document.getElementById("hdCallAgencyName").value.split("|")[0];
                 } 
                 //alert(Loc_code);
                 var strAgencyName=document.getElementById("txtAgencyName").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                if (strAgencyName!="")
                {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&Source=HD&Lcode=" + Loc_code   +  "&AgencyName="+strAgencyName +"&CallerName=" + CallerNames;
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
                            type = "../HelpDesk/HDUP_Ptr.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode; //+ "&MultiHD_RE_ID=" + strMultiHD_RE_ID ;
   	                        window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                    }
   	                    else
   	                    {
   	                        //var strAction="U|" + document.getElementById("hdEnPTRNo").value ;
   	                        var strAction="U|" + document.getElementById("ddlPTRNo").value ;   	                        
   	                        type = "../HelpDesk/HDUP_Ptr.aspx?Popup=T&Action=" + strAction + "&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;//+ "&MultiHD_RE_ID=" + strMultiHD_RE_ID;
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
                                 type = "../HelpDesk/HDUP_BDR.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode +"&requestType=" + text1;//'document.getElementById("ddlQueryCategory").value;
   	                            window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                        }
   	                        else
   	                        {
   	                            document.getElementById("lblError").innerText="First Select query Category";
   	                        }
   	                    }
   	                    else
   	                     {
                             type = "../HelpDesk/HDUP_BDR.aspx?Popup=T&Action=U&ReqID="+strHD_RE_ID + "&LCode="+strLCode + "&HD_RE_BDR_ID=" + document.getElementById("ddlBDRLetterID").value;
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
                        type = "../HelpDesk/HDUP_WorkOrder.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;
   	                    window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                    }
           	            
   	                    else
   	                    {
                        type = "../HelpDesk/HDUP_WorkOrder.aspx?Popup=T&Action=U&ReqID="+strHD_RE_ID + "&LCode="+strLCode + "&OrderID=" + document.getElementById("ddlWorkOrderNo").value ;
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
                type ="../Popup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
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
     
     
     function FillProductDetails()
     {
        var strChallanNo;
        strChallanNo = document.getElementById('txtRplIssueChallanNo').value;        
        
        if (strChallanNo != "")
        {
        var AgencyName = document.getElementById('txtAgencyName').value;
        AgencyName = AgencyName.trim();
        if ( AgencyName =="" )
        {
            document.getElementById("lblError").innerHTML="Please select Agency.";
            return false;
        }
        else{
                document.getElementById('txtRplIssueChallanNo').value = "Searching.."
                CallServerDataChallan(strChallanNo+"|CH","This is context from client");
                document.getElementById('txtRplIssueChallanNo').value =strChallanNo;            
            }
        }        
     }
     
     function ReceiveServerDataChallanNumber(args, context)
     {
     
        var obj = new ActiveXObject("MsXml2.DOMDocument");
        if (args!="")
        {
            var parts = args;
            obj.loadXML(parts);
            var dsRoot=obj.documentElement; 
            
            if (dsRoot !=null)
            {
                document.getElementById('hdChallanDetails').value = dsRoot.xml;
                var Agencyname = dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("AgencyName");
                Agencyname = Agencyname.trim();
                if (Agencyname != document.getElementById('txtAgencyName').value.trim())
                {
                    document.getElementById("lblError").innerHTML="Challan number is not valid for this Agency.";
                    return false;
                }
                if (dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("ChallanType") != "2" && dsRoot.getElementsByTagName("CHALLAN")(0).getAttribute("ChallanCategory") != "1")
                {
                    document.getElementById("lblError").innerHTML="Challan number is not valid issued replacement challan.";
                    return false;
                }
                else
                {document.forms['form1'].submit();}
            }
            else
            {
                document.getElementById('hdChallanDetails').value = "";
                document.forms['form1'].submit();
            }
            //alert(dsRoot.xml);
            
            
            
        }

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
			     
			     if  (document.getElementById('DlstCallerName') !=null)
			     {
			         FillCallerName("");
			     }
			  
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
			           
			           
			           //@ Added By 
			            try
			            { 		
			           // alert(document.getElementById('hdLoggedDatetime').value);	  
			            if ( document.getElementById('HdTechnicalLOgDateTime')  ==null)
			            {
			            // document.getElementById('txtLoggedDate').value=document.getElementById('hdLoggedDatetime').value;
			              document.getElementById('txtLoggedDate').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NEWLOGDATETIME");
			            }           
			             
			              //Added on 3rd Sep 2011 By 
			              if  (document.getElementById('DlstCallerName') !=null)
			                {
			                FillCallerName(parts[3]);
			               } 
			                 //alert("ab");
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

function ReceiveServerDataSubCategory(args, context)
{
        document.getElementById('lblError').innerText ="";
         var obj = new ActiveXObject("MsXml2.DOMDocument");
            
            
            if (args =="")
            {
                
            }
            else
            {
             obj.loadXML(args);
             var dsRoot=obj.documentElement; 
            
			    if (dsRoot !=null)
			    {
			         
			         if ( document.getElementById("HdDescWriitenByUser").value!="1")
			          {
			            var StrDescByUser ="";
			            var StrDescByCallSubCateg ="";
			            StrDescByUser=document.getElementById('txtDescription').value;
			            StrDescByCallSubCateg=dsRoot.getElementsByTagName("CALL_SUB_CATEGORY")[0].getAttribute("DESC");
			           // if ((StrDescByUser.indexOf(StrDescByCallSubCateg)==-1))
			             // { 
			              document.getElementById('txtDescription').value=dsRoot.getElementsByTagName("CALL_SUB_CATEGORY")[0].getAttribute("DESC");
			              document.getElementById("HdDescWriitenByUser").value="";
			            //  }	
			         }   
			    }            
            }
            
}
function ReceiveServerDataSubCategoryTech(args, context)
{
        document.getElementById('lblError').innerText ="";
         var obj = new ActiveXObject("MsXml2.DOMDocument");
            
            
            if (args =="")
            {
                
            }
            else
            {
             obj.loadXML(args);
             var dsRoot=obj.documentElement; 
            
			    if (dsRoot !=null)
			    {
			         //if (document.getElementById('txtDescription').value=='')
			        // {
			        //  document.getElementById('txtDescription').value=dsRoot.getElementsByTagName("CALL_SUB_CATEGORY")[0].getAttribute("DESC");
			        // }
			          //@ Added By  For Default Valu on the basis of Employee Setting
                   try
                       {           
                         if ( document.getElementById("HdTechDefaultTeamAssignedTo").value!="")
                           {
                                 document.getElementById("ddlTeamAssignedTo").value=document.getElementById("HdTechDefaultTeamAssignedTo").value;
                           }
                            if ( document.getElementById("HdTechDefaultContactType").value!="")
                           {
                             document.getElementById("ddlContactType").value= document.getElementById("HdTechDefaultContactType").value;
                           }
                           
                           
                       }
                        catch(err){}		        
			        		       
			    }
            
            }
            
}

var st;

   


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


function ReceiveServerDataCallerName(args, context)
{
        
        document.getElementById('lblError').innerText ="";
         var obj = new ActiveXObject("MsXml2.DOMDocument");
         var strTable="";
            
            if (args =="")
            {
                
            }
            else
            {
            
            strTable="<table border='1px' ><tr><td><br/></td></tr><tr><td id='tdruntime1' onclick='populatedata(tdruntime1)' >abc</td</tr>"
			        strTable=strTable + "<tr><td id='tdruntime2' onclick='populatedata(tdruntime2)' >abcd</td</tr>"
			        strTable=strTable + "<tr><td id='tdruntime3' onclick='populatedata(tdruntime3)' >abcde</td</tr>"
			        strTable=strTable + "<tr><td id='tdruntime4' onclick='populatedata(tdruntime4)' >abcdf</td</tr></table>"
			        
			        document.getElementById("dvAutoPopulate").innerHTML=strTable
            
             obj.loadXML(args);
             var dsRoot=obj.documentElement; 
             if (dsRoot !=null)
			    {
			        
			        		       
			    }
            
            }
}




function populatedata(id)
{
    document.getElementById("txtCallerName").value=id.innerText;
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
			
			//@ Code Added By  In case of Query Sub Group =Offline Work then  Query Status should be SOLVED OFFLINE by default   Date :----  20/12/2008
			    

			
			//@ End of Code Added By  In case of Query Sub Group =Offline Work then  Query Status should be SOLVED OFFLINE by default
			
			
			
   }
   
      function fillSubCategoryDefaultValues()
{

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
           
           //@ Added By  For Default Valu on the basis of Employee Setting
           
           
             
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


     
//*************************************End Fucnctional Call Log Methods*****************************************

//*************************************Start Travel Agency Methods**********************************************
//Pop up page



     function SelectFunctionTravelAgency(str3,strAdd,strAdd2,StrLoggedDateTime,CompVertical)
    {    
    
        var pos=str3.split('|'); 
        var passOutCode="0";
         
          if (window.opener.document.forms['form1']['hDCompanyVertical']!=null)
            {  
                var CVer=''; 
               if (CompVertical =='Amadeus' )
               {
                 CVer=1;
               }
                if (CompVertical =='ResBird' )
               {
                CVer=2;
               }
                  if (CompVertical =='Non1A' )
               {
                CVer=3;
               }
               
               window.opener.document.forms['form1']['hDCompanyVertical'].value=CVer;
              
            }
        
          if (window.opener.document.forms['form1']['TxtGroupOfficeID']!=null)
            {    
               window.opener.document.forms['form1']['TxtGroupOfficeID'].value=pos[5]
               window.close();
            }

       
         

     if (window.opener.document.forms['form1']['ChkGrpProductivity']!=null)
            {    
             window.opener.document.forms['form1']['ChkGrpProductivity'].disabled=false;
            }

    // used in course session
    if (window.opener.document.forms['form1']['hdCourseLCode']!=null)
    {
    window.opener.document.forms['form1']['hdCourseLCode'].value=pos[0];
    window.opener.document.forms['form1']['txtAgency'].value=pos[1];
    window.close();
    }
  
    if (window.opener.document.forms['form1']['hdtxtAgencyName']!=null)
    {
        window.opener.document.forms['form1']['hdtxtAgencyName'].value=pos[1];        
        if(window.opener.document.forms['form1']['hdtxtAgencyName'].value.trim()!="")
        {
            if ( window.opener.document.forms['form1']['chbWholeGroup']!=null)
            {
                window.opener.document.forms['form1']['chbWholeGroup'].disabled=false;            
            }    
        }
    }   
        
        // Code For Training Module
        
         if (window.opener.document.forms['form1']['hdTrainingLCode']!=null)
        { 
        window.opener.document.forms['form1']['hdTrainingLCode'].value=pos[0];
        window.opener.document.forms['form1']['txtAgency'].value=pos[1];
        window.close();
        }
        
        //For Update
        if (window.opener.document.forms['form1']['hdAgencyNameParticipantBasket']!=null)
        { 
        window.opener.document.forms['form1']['hdAgencyNameParticipantBasket'].value=str3 + "\n" +strAdd;
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.opener.document.forms['form1']['txtAddress'].value=strAdd + '\n ' + strAdd2;//pos[2];;
        window.opener.document.forms['form1']['txtCountry'].value=pos[3];
        window.opener.document.forms['form1']['txtCity'].value=pos[2];
        window.opener.document.forms['form1']['txtPhone'].value=pos[4];
        window.opener.document.forms['form1']['txtFax'].value=pos[6];
        window.opener.document.forms['form1']['txtOnlineStatus'].value=pos[7];
        //window.opener.document.forms['form1']['txtPriority'].value=pos[5];//pos[7];
       
         window.close();
         return false;
        }
        //


         if (window.opener.document.forms['form1']['hdAgencyStaffAgencyName']!=null)
        { 
        window.opener.document.forms['form1']['hdAgencyStaffAgencyName'].value=str3;
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.close();
        }
      if (window.opener.document.forms['form1']['hdCallAgencyName']!=null)
        { 
        
        window.opener.document.forms['form1']['hdCallAgencyName'].value=str3;
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.opener.document.forms['form1']['txtAddress'].value=strAdd + '\n ' + strAdd2;//pos[2];
        window.opener.document.forms['form1']['txtCountry'].value=pos[3];//pos[5];
        window.opener.document.forms['form1']['txtCity'].value=pos[2];//pos[4];
        window.opener.document.forms['form1']['txtPhone'].value=pos[4];//pos[6];
        window.opener.document.forms['form1']['txtFax'].value=pos[6];//pos[8];
        window.opener.document.forms['form1']['txtOnlineStatus'].value=pos[7];//pos[9];
        window.opener.document.forms['form1']['txtOfficeId'].value=pos[5];//pos[7];
        window.opener.document.forms['form1']['hdAOffice'].value=pos[8];//pos[10];
        
        window.opener.document.forms['form1']['txtPincode'].value=pos[9];//pos[7];
        window.opener.document.forms['form1']['txtEmail'].value=pos[10];//pos[10];
       
       
                 //@ Added By 
			            try
			            { 
			            
			            if ( window.opener.document.forms['form1']['HdRequireCallerName']  !=null)
			             {
			              window.opener.document.forms['form1']['HdRequireCallerName'].value=pos[0];
			             }
			            
			            
			             if ( window.opener.document.forms['form1']['TxtCompVertical']  !=null)
			             {
			              window.opener.document.forms['form1']['TxtCompVertical'].value=CompVertical;
			             }
			            		
			             // alert(  window.opener.document.forms['form1']['hdLoggedDatetime'].value);	
			             if ( window.opener.document.forms['form1']['HdTechnicalLOgDateTime']  ==null)
			            {
			           // window.opener.document.forms['form1']['txtLoggedDate'].value=window.opener.document.forms['form1']['hdLoggedDatetime'].value;
			             window.opener.document.forms['form1']['txtLoggedDate'].value=StrLoggedDateTime;//window.opener.document.forms['form1']['hdLoggedDatetime'].value;
			            }
			                           
			            
			            }
                        catch(err){}
                      //@ Added By 
			            try			            
			            { 		
			                if(window.opener.document.forms['form1']['BtnChangeContext'] !=null)			              
			               {	                
			                window.opener.document.forms['form1']['BtnChangeContext'].click();
			               }
			            }
                        catch(err){}
       
       
          window.close();
           return false;
        }
         if (window.opener.document.forms['form1']['hdChallanLCode']!=null)
        { 
        window.opener.document.forms['form1']['hdChallanLCodeTemp'].value=pos[0];
        window.opener.document.forms['form1']['hdChallanLCode'].value=pos[0];
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.opener.document.forms['form1']['txtAddress'].value=strAdd + '\n ' + strAdd2;//pos[2];
        window.opener.document.forms['form1']['txtCountry'].value=pos[3];//pos[5];
        window.opener.document.forms['form1']['txtCity'].value=pos[2];//pos[4];
        window.opener.document.forms['form1']['txtPhone'].value=pos[4];//pos[6];
        window.opener.document.forms['form1']['txtFax'].value=pos[6];//pos[8];
        window.opener.document.forms['form1']['txtOfficeId'].value=pos[5];;//pos[7];
           window.opener.document.forms['form1'].submit();    
           passOutCode="1";
            window.close();
        }
        if (passOutCode=="0")
        {
        if (window.opener.document.forms['form1']['hidLcode']!=null)
        {
        window.opener.document.forms['form1']['hidLcode'].value=pos[0];
        }
       // hdlcode.Value
        if (window.opener.document.forms['form1']['hdLcode']!=null)
        {
        window.opener.document.forms['form1']['hdLcode'].value=pos[0];
        }
        if (window.opener.document.forms['form1']['hdAgencyNameId']!=null)
        {
        window.opener.document.forms['form1']['hdAgencyNameId'].value=pos[0];
        }
         if (window.opener.document.forms['form1']['hdAgencyName']!=null)
        {
        window.opener.document.forms['form1']['hdAgencyName'].value=pos[0];
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        
        }
          if (window.opener.document.forms['form1']['hdOfficeID']!=null)
        {
        window.opener.document.forms['form1']['hdOfficeID'].value=pos[5];//pos[7];   
        
        }
        
        
        if  (window.opener.document.forms['form1']['hdAgency']!=null)
        {
	    window.opener.document.forms['form1']['hdAgency'].value=pos[1];
	    }
        if  (window.opener.document.forms['form1']['txtAgencyName']!=null)
        {
	    window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
	    }
	     if  (window.opener.document.forms['form1']['txtAgencyAddress']!=null)
        {
	    window.opener.document.forms['form1']['txtAgencyAddress'].value=strAdd + ' ' + strAdd2 ;//pos[2] + ' ' + pos[3];
	    }
	    if  (window.opener.document.forms['form1']['hdAddress']!=null)
        {
	    window.opener.document.forms['form1']['hdAddress'].value=strAdd + ' ' + strAdd2;//pos[2] ;
	    }
	    if  (window.opener.document.forms['form1']['txtAddress']!=null)
        {
	    window.opener.document.forms['form1']['txtAddress'].value=strAdd + ' ' + strAdd2;//pos[2] ;
	    }
	     if  (window.opener.document.forms['form1']['hdCity']!=null)
        {
	   window.opener.document.forms['form1']['hdCity'].value=pos[2];//pos[4] ;
	    
	    }
	    
	    
	    
	    if  (window.opener.document.forms['form1']['txtCity']!=null)
        {
	    window.opener.document.forms['form1']['txtCity'].value=pos[2];//pos[4] ;
	    }
	    
	    if  (window.opener.document.forms['form1']['hdCountry']!=null)
        {
	    window.opener.document.forms['form1']['hdCountry'].value=pos[3];//pos[5];
	    }
	    if  (window.opener.document.forms['form1']['txtCountry']!=null)
        {
	   window.opener.document.forms['form1']['txtCountry'].value=pos[3];//pos[5];
	    }
	    
	     if  (window.opener.document.forms['form1']['hdPhone']!=null)
        {
	    window.opener.document.forms['form1']['hdPhone'].value=pos[4];//pos[6];
	    }
	    
	    if  (window.opener.document.forms['form1']['txtPhone']!=null)
        {
	    window.opener.document.forms['form1']['txtPhone'].value=pos[4];//pos[6];
	    }
	     if  (window.opener.document.forms['form1']['hdOffice']!=null)
        {
	    window.opener.document.forms['form1']['hdOffice'].value=pos[5];//pos[7];
	    }
	    
	    if  (window.opener.document.forms['form1']['txtOfficeId']!=null)
        {
	    window.opener.document.forms['form1']['txtOfficeId'].value=pos[5];//pos[7];
	    }
	    if  (window.opener.document.forms['form1']['txtOfficeID1']!=null)
        {
	    window.opener.document.forms['form1']['txtOfficeID1'].value=pos[5];//pos[7];
	    }
	      if  (window.opener.document.forms['form1']['hdFax']!=null)
        {
	   window.opener.document.forms['form1']['hdFax'].value=pos[6];//pos[8];
	    }
	    if  (window.opener.document.forms['form1']['txtFax']!=null)
        {
	    window.opener.document.forms['form1']['txtFax'].value=pos[6];//pos[8];
	    }
	    
	   if (window.opener.document.forms['form1']['txtAgencyName']!=null)
	   {
	       if(window.opener.document.forms['form1']['txtAgencyName'].value.trim()!="")
	       {
	          if ( window.opener.document.forms['form1']['chbWholeGroup']!=null)
	          {
               window.opener.document.forms['form1']['chbWholeGroup'].disabled=false;
              }
                if ( window.opener.document.forms['form1']['chkGroupProductivity']!=null)
	          {
               window.opener.document.forms['form1']['chkGroupProductivity'].disabled=false;
              }              
              
              
           }
	   }
	    if (window.opener.document.forms['form1']['drpCity'] !=null )
	    {		    
	         for (i=0;i<window.opener.document.forms['form1']['drpCity'].length;i++)
           {
                if (pos[2] == window.opener.document.forms['form1']['drpCity'].options[i].text) // pos[4]
               {  
                      
               window.opener.document.forms['form1']['drpCity'].options[i].selected = true;
                //document.forms(0).submit();
                break;
               }
           }
       }
        if (window.opener.document.forms['form1']['drpCountry'] !=null )
	    {		    
	         for (i=0;i<window.opener.document.forms['form1']['drpCountry'].length;i++)
           {
                if (pos[3] == window.opener.document.forms['form1']['drpCountry'].options[i].text) //pos[5]
               {  
                      
               window.opener.document.forms['form1']['drpCountry'].options[i].selected = true;
                //document.forms(0).submit();
                break;
               }
           }
       }
        if (window.opener.document.forms['form1']['drpAoffice'] !=null )
	    {		    
	         for (i=0;i<window.opener.document.forms['form1']['drpAoffice'].length;i++)
           {
                if (pos[8] == window.opener.document.forms['form1']['drpAoffice'].options[i].text)//pos[10]
               {  
                      
               window.opener.document.forms['form1']['drpAoffice'].options[i].selected = true;
                //document.forms(0).submit();
                break;
               }
           }
       }
       if (window.opener.document.forms['form1']['drpOnlineStatus'] !=null )
	    {		    
	         for (i=0;i<window.opener.document.forms['form1']['drpOnlineStatus'].length;i++)
           {
                if (pos[7] == window.opener.document.forms['form1']['drpOnlineStatus'].options[i].text)//pos[9]
               {  
                      
               window.opener.document.forms['form1']['drpOnlineStatus'].options[i].selected = true;
                //document.forms(0).submit();
                break;
               }
           }
       }
	   // Neeraj
	  if  (window.opener.document.forms['form1']['txtAgencyName']!=null)
        {
	    window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
	   // window.opener.document.forms['form1']['hdAgencyID'].value=pos[0];
	   
	    //Added by Neeraj nath for Lcode Disable
            if ( window.opener.document.forms['form1']['txtLcode']!=null)
            {
                window.opener.document.forms['form1']['txtLcode'].value='';
                window.opener.document.forms['form1']['txtLcode'].disabled=true;            
                window.opener.document.forms['form1']['txtLcode'].className="textboxgrey";
            }
           if ( window.opener.document.forms['form1']['txtChainCode']!=null)
            {
                window.opener.document.forms['form1']['txtChainCode'].value='';
                window.opener.document.forms['form1']['txtChainCode'].disabled=true;      
                window.opener.document.forms['form1']['txtChainCode'].className="textboxgrey";      
            }
            
         //Added by Neeraj nath for Lcode Disable
         
	    }
	     if  (window.opener.document.forms['form1']['hdAgencyID']!=null)
        {
	   window.opener.document.forms['form1']['hdAgencyID'].value=pos[0];
	    }
	    
	    // Code for filling Agency details
	    
	    if (window.opener.document.forms['form1']['hdLCode']!=null)
        { 
        
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.opener.document.forms['form1']['txtAddress'].value=strAdd + ' ' + strAdd2;//pos[2];
        window.opener.document.forms['form1']['txtCountry'].value=pos[3];//pos[5];
        window.opener.document.forms['form1']['txtCity'].value=pos[2];//pos[4];
        window.opener.document.forms['form1']['txtPhone'].value=pos[4];//pos[6];
        window.opener.document.forms['form1']['txtFax'].value=pos[6];//pos[8];
        window.opener.document.forms['form1']['txtOnlineStatus'].value=pos[7];//pos[9];
        window.opener.document.forms['form1']['txtOfficeID'].value=pos[5];//pos[7];
        //window.opener.document.forms['form1']['hdAOffice'].value=pos[10];
          window.opener.document.forms['form1']['hdLCode'].value=pos[0];   
        }
         
             //Added By Neeraj Nath for Agency Target 14/09/2011
         if (window.opener.document.forms['form1']['hdTargetLcode']!=null)
         {
           window.opener.document.forms['form1']['hdTargetLcode'].value=pos[0];
            if ( window.opener.document.forms['form1']['txtLcode']!=null)
                {
                 window.opener.document.forms['form1']['txtLcode'].disabled=false;            
                 window.opener.document.forms['form1']['txtLcode'].className="textbox";

//                 window.opener.document.forms['form1']['txtLcode'].disabled=true;            
//                 window.opener.document.forms['form1']['txtLcode'].className="textboxgrey";
                 window.opener.document.forms['form1']['txtLcode'].value=pos[0];                  
                }
//            if ( window.opener.document.forms['form1']['txtOfficeID']!=null)
//                {
//                window.opener.document.forms['form1']['txtOfficeID'].value=pos[5];
//                }
            if ( window.opener.document.forms['form1']['chkGroupProductivity']!=null)
                {
                window.opener.document.forms['form1']['chkGroupProductivity'].disabled=false;            
                }    
         }
       //Added By Neeraj Nath for Agency Target 14/09/2011
       
        
 //Code Used In ISP Feasibility Request.

        if (window.opener.document.forms['form1']['hdISPRequestPage']!=null)
        { 
            window.opener.document.forms['form1']['hdlcode'].value=pos[0];
             window.opener.document.forms['form1']['hdAgency'].value=pos[1];
           // window.opener.document.forms['form1']['hdAddress'].value=strAdd;
            window.opener.document.forms['form1']['hdAddress'].value=strAdd + '\n ' + strAdd2;
            window.opener.document.forms['form1']['hdOffice'].value=pos[5];
            window.opener.document.forms['form1']['hdFax'].value=pos[6];
             window.opener.document.forms['form1']['hdPhone'].value=pos[4];
             window.opener.document.forms['form1']['hdPin'].value=pos[9];
               window.opener.document.forms['form1']['hdCountry'].value=pos[14];
                 window.opener.document.forms['form1']['hdCity'].value=pos[13];
                 window.opener.document.forms['form1']['hdConcernPerson'].value=pos[11];
            
            
            
            window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
            window.opener.document.forms['form1']['txtAddress'].value=strAdd + '\n ' + strAdd2;
            window.opener.document.forms['form1']['txtOfficeId'].value=pos[5];
            window.opener.document.forms['form1']['txtFax'].value=pos[6];
             window.opener.document.forms['form1']['txtPhone'].value=pos[4];
             window.opener.document.forms['form1']['txtPinCode'].value=pos[9];
             
             window.opener.document.forms['form1']['txtConcernPerson'].value=pos[12];
             
            window.opener.document.forms['form1']['txtCountry'].value=pos[3];
           window.opener.document.forms['form1']['txtCity'].value=pos[2];
             
         
        }
	    }
	  	window.close();
        }
             
             
             
     function fillBackUpTravelAgency()
         {
           var officeId;
           //officeId=  document.getElementById('txtOfficeId').value;
           officeId="1"
           CallServer(officeId,"This is context from client");
           return false;
           }

function ReceiveServerDataTravelAgency(args, context)
        {
        
            AdvanceSearchTravelAgency();
            var obj = new ActiveXObject("MsXml2.DOMDocument");
            obj.loadXML(args);
			var dsRoot=obj.documentElement;
			
			
			var ddlOrders = document.getElementById('drpBackupOnlineStatus');
		
			for (var count = ddlOrders.options.length-1; count >-1; count--)
			{
				ddlOrders.options[count] = null;
				
				
			}
			
			var orders = dsRoot.getElementsByTagName('Status');
				var codes='';
			var names="-- All --";
			var text; 
			
			var listItem;
			listItem = new Option(names, codes,  false, false);
			ddlOrders.options[ddlOrders.length] = listItem;
			
			
			for (var count = 0; count < orders.length; count++)
			{
				codes= orders[count].getAttribute("StatusCode"); 
			    names=orders[count].getAttribute("StatusCode"); 
				listItem = new Option(names, codes,  false, false);
				ddlOrders.options[ddlOrders.length] = listItem;
				
			}
			
			// code for online status start
			var ddlOnlineStatus = document.getElementById('drpOnlineStatus');
			for (var count = ddlOnlineStatus.options.length-1; count >-1; count--)
			{
				ddlOnlineStatus.options[count] = null;
				
				
			}
			
			var orders = dsRoot.getElementsByTagName('Status');
				var codes='';
			var names="-- All --";
			var text; 
			
			var listItem;
			listItem = new Option(names, codes,  false, false);
			ddlOnlineStatus.options[ddlOnlineStatus.length] = listItem;
			
			
			for (var count = 0; count < orders.length; count++)
			{
				codes= orders[count].getAttribute("StatusCode"); 
			    names=orders[count].getAttribute("StatusCode"); 
				listItem = new Option(names, codes,  false, false);
				ddlOnlineStatus.options[ddlOnlineStatus.length] = listItem;
				
			}
			// code for online status end
			
        }
         
      function CheckValidationTravelAgency()
    {
        if (document.getElementById("txtFielNumber").value!="")
         {
           if(IsDataValid(document.getElementById("txtFielNumber").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="File no is not valid.";
            document.getElementById("txtFielNumber").focus();
            return false;
            } 
             if(parseInt(document.getElementById("txtFielNumber").value)>32767)
            {
           // alert("abhi");
            document.getElementById("lblError").innerHTML="File no is not valid.";
            document.getElementById("txtFielNumber").focus();
            return false;
            } 
         }  
        if(document.getElementById('txtDateOfflineF').value != '')
        {
        if (isDate(document.getElementById('txtDateOfflineF').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Date of offline from is not valid.";			
	       document.getElementById('txtDateOfflineF').focus();
	       return(false);  
        }
         }  
          if(document.getElementById('txtDateOfflineT').value != '')
        {
        if (isDate(document.getElementById('txtDateOfflineT').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Date of offline to is not valid.";			
	       document.getElementById('txtDateOfflineT').focus();
	       return(false);  
        }
         }  
         
            if(document.getElementById('txtDateOnlineF').value != '')
        {
        if (isDate(document.getElementById('txtDateOnlineF').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Date of online from is not valid.";			
	       document.getElementById('txtDateOnlineF').focus();
	       return(false);  
        }
         }   
               if(document.getElementById('txtDateOnlineT').value != '')
        {
        if (isDate(document.getElementById('txtDateOnlineT').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Date of online to is not valid.";			
	       document.getElementById('txtDateOnlineT').focus();
	       return(false);  
        }
         }   
         
          if (  document.getElementById("txtLcode").value!="")
         {
           if(IsDataValid(document.getElementById("txtLcode").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Lcode is not valid.";
            document.getElementById("txtLcode").focus();
            return false;
            } 
            } 
                 if (  document.getElementById("txtChainCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtChainCode").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Chain code is not valid.";
            document.getElementById("txtChainCode").focus();
            return false;
            } 
            } 
     }
     
     
     function OnloadAdvanceSearchTravelAgency()
    {            
       if(document.getElementById('hdAdvanceSearch').value=="1")
       {   
           document.getElementById('btnUp').src='../images/up.jpg';  
           document.getElementById('pnlAdvanceSearch').style.display ='block'            
       }
       else
       {
          document.getElementById('btnUp').src="../images/down.jpg";
           document.getElementById('pnlAdvanceSearch').style.display ='none'
       }
     }
     
     function AdvanceSearchTravelAgency()
    {           
        if(document.getElementById('hdAdvanceSearch').value=="1")
        {
            document.getElementById('btnUp').src="../images/down.jpg";            
            document.getElementById('pnlAdvanceSearch').style.display ='none'
            document.getElementById('hdAdvanceSearch').value='0';
        }
        else
        {
            document.getElementById('btnUp').src='../images/up.jpg';           
            document.getElementById('pnlAdvanceSearch').style.display ='block'
            document.getElementById('hdAdvanceSearch').value='1';
        }        
    }
    
    
    function EditFunctionTravelAgency(CheckBoxObj)
    {   
        window.location.href="TAUP_Agency.aspx?Action=U|"+CheckBoxObj;       
        return false;
    }
    function DeleteFunctionTravelAgency(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {        
            document.getElementById("hdDeleteFlag").value= CheckBoxObj ;
           // window.location.href="TASR_Agency.aspx?Action=D|"+CheckBoxObj;
            
        }
        else
        {
         document.getElementById("hdDeleteFlag").value="";
         return false;
        }
    }
    
    
     function NewFunctionTravelAgency()
    {   
        window.location.href="TAUP_Agency.aspx?Action=I";       
        return false;
    }  
    
     function EmployeePageTravelAgency()
        {
        
                    var type;     
                   var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                    if (strEmployeePageName!="")
                    {
                        type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
   	                    window.open(type,"aa3","height=600,width=910,top=30,left=20,scrollbars=1,status=1");	
                        return false;
                    }
        }
        
        
        
        /*  Js for Isp Pending Cancellation         */
        
          function CheckValidationIspPendingCancellation()
            {       
                if(document.getElementById('txtOrderDateFrom').value != '')
                {
                if (isDate(document.getElementById('txtOrderDateFrom').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('lblError').innerText = "Order Date From is not valid.";			
	               document.getElementById('txtOrderDateFrom').focus();
	               return(false);  
                }
                 } 
                 
                  if(document.getElementById('txtOrderDateTo').value != '')
                {
                if (isDate(document.getElementById('txtOrderDateTo').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('lblError').innerText = "Order Date To is not valid.";			
	               document.getElementById('txtOrderDateTo').focus();
	               return(false);  
                }
                 }   
                    if(document.getElementById('txtOnlineDateFrom').value != '')
                {
                if (isDate(document.getElementById('txtOnlineDateFrom').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('lblError').innerText = "Date of online From is not valid.";			
	               document.getElementById('txtOnlineDateFrom').focus();
	               return(false);  
                }
                 }  
                      if(document.getElementById('txtOnlineDateTo').value != '')
                {
                if (isDate(document.getElementById('txtOnlineDateTo').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('lblError').innerText = "Date of online To is not valid.";			
	               document.getElementById('txtOnlineDateTo').focus();
	               return(false);  
                }
                 }    
                    if(document.getElementById('txtOrderDateTo').value != '' && document.getElementById('txtOrderDateFrom').value != '')
                {
                if (compareDates(document.getElementById('txtOrderDateFrom').value,"d/M/yyyy",document.getElementById('txtOrderDateTo').value,"d/M/yyyy")=='1')	
                {
                   document.getElementById('lblError').innerText = "Order Date To is shorter than Order Date From.";			
                   document.getElementById('txtOrderDateTo').focus();
                   return(false);  
                }
               }   
               
                 if(document.getElementById('txtOnlineDateTo').value != '' && document.getElementById('txtOnlineDateFrom').value != '')
                {
                if (compareDates(document.getElementById('txtOnlineDateFrom').value,"d/M/yyyy",document.getElementById('txtOnlineDateTo').value,"d/M/yyyy")=='1')	
                {
                   document.getElementById('lblError').innerText = "Online Date To is shorter than Online Date From.";			
                   document.getElementById('txtOnlineDateTo').focus();
                   return(false);  
                }
               } 
                    
           }
           
           
           function PopupIspPendingCancellation()
           { 
                var type;          
                type = "../ISP/MSSR_ISPPlan.aspx?Popup=T" ;   	     
   	            window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                return false;
           }
 
 function AdvanceSearchIspPendingCancellation()
    {           
        if(document.getElementById('hdAdvanceSearch').value=="1")
        {
            document.getElementById('btnUp').src="../images/down.jpg";            
            document.getElementById('pnlAdvanceSearch').style.display ='none'
            document.getElementById('hdAdvanceSearch').value='0';
        }
        else
        {
            document.getElementById('btnUp').src='../images/up.jpg';           
            document.getElementById('pnlAdvanceSearch').style.display ='block'
            document.getElementById('hdAdvanceSearch').value='1';
        }        
    }
    function OnloadAdvanceSearchIspPendingCancellation()
    {            
       if(document.getElementById('hdAdvanceSearch').value=="1")
       {   
           document.getElementById('btnUp').src='../images/up.jpg';  
           document.getElementById('pnlAdvanceSearch').style.display ='block'            
       }
       else
       {
          document.getElementById('btnUp').src="../images/down.jpg";
           document.getElementById('pnlAdvanceSearch').style.display ='none'
       }
     }
       function PopupAgencyPageIspPendingCancellation()
        {

            var type;
          // type = "../Popup/PUSR_Agency.aspx" ;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
        
        
        /* Code For Employee Search */        
                
                    function SelectFunctionEmployeeData(str3)
                   {   
                    //alert(str3);
                    var pos=str3.split('|'); 
                    // used in Approval Level Start
                      if (window.opener.document.forms['form1']['txtLevel1']!=null && window.opener.document.getElementById("hdCtrlId").value=="txtLevel1" )
                    {
                    window.opener.document.forms['form1']['txtLevel1'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel1'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      if (window.opener.document.forms['form1']['txtLevel2']!=null  && window.opener.document.getElementById("hdCtrlId").value=="txtLevel2")
                    {
                    window.opener.document.forms['form1']['txtLevel2'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel2'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      if (window.opener.document.forms['form1']['txtLevel3']!=null  && window.opener.document.getElementById("hdCtrlId").value=="txtLevel3")
                    {
                    window.opener.document.forms['form1']['txtLevel3'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel3'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      if (window.opener.document.forms['form1']['txtLevel4']!=null  && window.opener.document.getElementById("hdCtrlId").value=="txtLevel4")
                    {
                    window.opener.document.forms['form1']['txtLevel4'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel4'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      if (window.opener.document.forms['form1']['txtLevel5']!=null  && window.opener.document.getElementById("hdCtrlId").value=="txtLevel5")
                    {
                    window.opener.document.forms['form1']['txtLevel5'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel5'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      if (window.opener.document.forms['form1']['txtLevel6']!=null  && window.opener.document.getElementById("hdCtrlId").value=="txtLevel6")
                    {
                    window.opener.document.forms['form1']['txtLevel6'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel6'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      if (window.opener.document.forms['form1']['txtLevel7']!=null  && window.opener.document.getElementById("hdCtrlId").value=="txtLevel7")
                    {
                    window.opener.document.forms['form1']['txtLevel7'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel7'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      if (window.opener.document.forms['form1']['txtLevel8']!=null  && window.opener.document.getElementById("hdCtrlId").value=="txtLevel8")
                    {
                    window.opener.document.forms['form1']['txtLevel8'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel8'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      if (window.opener.document.forms['form1']['txtLevel9']!=null && window.opener.document.getElementById("hdCtrlId").value=="txtLevel9")
                    {
                    window.opener.document.forms['form1']['txtLevel9'].value=pos[1];
                    window.opener.document.forms['form1']['hdLevel9'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                    //End
                    
                    
                    // Used In Sales -> Visit Details 
     
                     if (window.opener.document.forms['form1']['txtAssignedTo']!=null && window.opener.document.forms['form1']['hdAssingedTo']!=null )
                    {
                    window.opener.document.forms['form1']['txtAssignedTo'].value=pos[1];
                    window.opener.document.forms['form1']['hdAssingedTo'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                    // Used In Agency Group
     
                     if (window.opener.document.forms['form1']['txtAccountsManager']!=null && window.opener.document.forms['form1']['hdAccountsManagerID']!=null )
                    {
                    window.opener.document.forms['form1']['txtAccountsManager'].value=pos[1];
                    window.opener.document.forms['form1']['hdAccountsManagerID'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                    //used for training in participant basket
                    if (window.opener.document.forms['form1']['txtTrainer1']!=null && document.getElementById("hdCtrlId").value=="txtTrainer1")
                    {
                    window.opener.document.forms['form1']['txtTrainer1'].value=pos[1];
                    window.close();
                    return false;
                    }
                    
                    if (window.opener.document.forms['form1']['txtTrainer2']!=null && document.getElementById("hdCtrlId").value=="txtTrainer2")
                    {
                    window.opener.document.forms['form1']['txtTrainer2'].value=pos[1];
                    window.close();
                    return false;
                    }
                    
                    if (window.opener.document.forms['form1']['hdTrainingEmployeeID']!=null)
                    {
                    window.opener.document.forms['form1']['txtEmployee'].value=pos[1];
                    window.opener.document.forms['form1']['hdTrainingEmployeeID'].value=pos[0];
                    }
                    
                     //Used for Scrap in Inventory.
                    if(window.opener.document.forms['form1']['hdScrap']!=null)
                    {
                     window.opener.document.forms['form1']['txtLoggedBy'].value=pos[1];
                    window.opener.document.forms['form1']['hdEmployeeID'].value=str3;
                    window.close();
                    return false;
                    }

                    //Used for FeedBack in HelpDEsk
                    if(window.opener.document.forms['form1']['hdCallAssignedTo']!=null)
                    {
                     window.opener.document.forms['form1']['txtAssignedTo'].value=pos[1];
                    window.opener.document.forms['form1']['hdCallAssignedTo'].value=str3;
                    window.close();
                    return false;
                    }
                    if(window.opener.document.forms['form1']['hdLoggedBy']!=null)
                    {
                     window.opener.document.forms['form1']['txtLoggedBy'].value=pos[1];
                    window.opener.document.forms['form1']['hdLoggedBy'].value=str3;
                    window.close();
                    return false;
                    }
                    
                    if (window.opener.document.forms['form1']['hdEmployeeID']!=null)
                    {
                     window.opener.document.forms['form1']['hdEmployeeID'].value=pos[0];
                     window.opener.document.forms['form1']['txtEmployeeName'].value=pos[1];
                     window.close();
                     return false;
                    }

                 if (window.opener.document.forms['form1']['hdCourseSessionEmployeePopup']!=null)
                { 
                window.opener.document.forms['form1']['hdCourseSessionEmployeePopup'].value='E' + "|" + pos[0] + "|" + pos[2] + "|" + pos[1] + "|" + pos[3];
                window.opener.document.forms['form1'].submit();
                window.close();
                return false;                    
                }

                    if (window.opener.document.forms['form1']['hdRespId']!=null)
                    {
                    window.opener.document.forms['form1']['hdRespId'].value=pos[0];
                    }
                       if (window.opener.document.forms['form1']['hdApprovedBy']!=null)
                    {
                    window.opener.document.forms['form1']['hdApprovedBy'].value=pos[0];
                    }
                      if (window.opener.document.forms['form1']['hdEmployeeId']!=null)
                    {
                    window.opener.document.forms['form1']['hdEmployeeId'].value=pos[0];
                    }
                         
                    if (window.opener.document.forms['form1']['hdChallanReceivedBy']!=null )
                    {
                    if (window.opener.document.forms['form1']['hdSelectEmployeeType'].value == "3")
                    {
                    window.opener.document.forms['form1']['hdChallanReceivedBy'].value=pos[0];
                     window.opener.document.forms['form1']['txtChallanReceivedBy'].value=pos[1];
                    }
                    }
                        if (window.opener.document.forms['form1']['hdChallanRequestedBy']!=null )
                    {
                    if (window.opener.document.forms['form1']['hdSelectEmployeeType'].value == "1")
                    {
                    window.opener.document.forms['form1']['hdChallanRequestedBy'].value=pos[0];
                     window.opener.document.forms['form1']['txtChallanRequestedBy'].value=pos[1];
                    }
                    }
                      if (window.opener.document.forms['form1']['hdChallanApprovedBy']!=null)
                    {
                    if (window.opener.document.forms['form1']['hdSelectEmployeeType'].value == "2")
                    {
                    window.opener.document.forms['form1']['hdChallanApprovedBy'].value=pos[0];
                     window.opener.document.forms['form1']['txtChallanApprovedBy'].value=pos[1];
                    }
                    }                
                  
                     if (window.opener.document.forms['form1']['hdPendingWithId']!=null)
                    {
                    window.opener.document.forms['form1']['hdPendingWithId'].value=pos[0];
                    }
                    
                    if (window.opener.document.forms['form1']['txtBdrSentBy']!=null)
                    {
                    window.opener.document.forms['form1']['txtBdrSentBy'].value=pos[1];
                    }
                        if (window.opener.document.forms['form1']['txtApprovedBy']!=null)
                    {
                    window.opener.document.forms['form1']['txtApprovedBy'].value=pos[1];
                    }     
                      if (window.opener.document.forms['form1']['txtPendingWith']!=null)
                    {
                    window.opener.document.forms['form1']['txtPendingWith'].value=pos[1];
                    }            
                     if (window.opener.document.forms['form1']['txtAResponsibility']!=null)
                    {
                    window.opener.document.forms['form1']['txtAResponsibility'].value=pos[1];
                    }
                    if (window.opener.document.forms['form1']['hdEmployeeName']!=null)
                    {
                    window.opener.document.forms['form1']['hdEmployeeName'].value=pos[1];
                    }
                    
                     if ( window.opener.document.forms['form1']['txtEmpName']!=null)
                    {
                    window.opener.document.forms['form1']['txtEmpName'].value=pos[1];
                    window.opener.document.forms['form1']['hdEmpName'].value=pos[1];
                    window.opener.document.forms['form1']['hdEmpID'].value=pos[0];
                    window.opener.document.forms['form1']['drpAoffice'].disabled=true
                            
                    }
                    
                      if ( window.opener.document.forms['form1']['txtLogedByName']!=null)
                    {
                    window.opener.document.forms['form1']['txtLogedByName'].value=pos[1];
                    window.opener.document.forms['form1']['hdLogedByName'].value=pos[1];
                    window.opener.document.forms['form1']['hdEmpID'].value=pos[0];
                   }
                   
                   // Used in Inventory --> Search Scrap INVSR_Scrap.aspx
                    if ( window.opener.document.forms['form1']['txtLoggedBy']!=null)
                    {
                    window.opener.document.forms['form1']['txtLoggedBy'].value=pos[1];
                    window.opener.document.forms['form1']['hdEmployeeID'].value=pos[0];
                    }  
                    
                    if (window.opener.document.forms['form1']['txtResFrom']!=null && window.opener.document.getElementById("hdCtrlId").value=="txtResFrom" )
                    {
                    window.opener.document.forms['form1']['txtResFrom'].value=pos[1];
                    window.opener.document.forms['form1']['hdResFrom'].value=pos[0];
                    window.close();
                    return false;
                    }
                    if (window.opener.document.forms['form1']['txtResTo']!=null && window.opener.document.getElementById("hdCtrlId").value=="txtResTo" )
                    {
                    window.opener.document.forms['form1']['txtResTo'].value=pos[1];
                    window.opener.document.forms['form1']['hdResTo'].value=pos[0];
                    window.close();
                    return false;
                    }
                    
                      
                    window.close();
               }
               function ConfirmDeleteEmplyeeData()
               {   
                    if (confirm("Are you sure you want to delete?")==false)
                    { 
                        return false;
                    }
                }
        
        /* code for Employee Search*/
        
        
        /*Code  for Travel Agency Main Order  */
  
    
        function ActDeActOrderMain()
 {
        var AgencyName = document.getElementById("txtAgencyName").value;
        AgencyName = AgencyName.trim();
	    if (AgencyName == "")
	    {document.getElementById("chbWholeGroup").disabled=true;
	    document.getElementById("chbWholeGroup").checked=false;      }
	    else
	    {document.getElementById("chbWholeGroup").disabled=false;}
	    
 }
 
    function EditFunctionOrderMain(CheckBoxObj)
    {           
          window.location.href="MSUP_Order.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
    
    function DeleteFunctionOrderMain(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
          
           document.getElementById('hdOrderID').value = CheckBoxObj;
           document.forms['form1'].submit();   
        }
        else
        {
            return false;
        }
    }
       
       
       function AdvanceSearchOrderMain()
    { 
           
        if(document.getElementById('hdAdvanceSearch').value=='')
        {
            document.getElementById('btnUp').src="../images/up.jpg";
            document.getElementById('pnlAdvanceSearch').style.display ='block'
            document.getElementById('hdAdvanceSearch').value='1';
        }
        else
        {
            document.getElementById('btnUp').src="../images/down.jpg";
            document.getElementById('pnlAdvanceSearch').style.display ='none'
            document.getElementById('hdAdvanceSearch').value='';
        }
    }
    
    
     function PopupAgencyPageOrderMain()
{

    var type;
    type = "TASR_Agency.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;
 
} 




function OrderPage2MainOrder()
{
 //debugger;
       if ((document.getElementById ('hDCompanyVertical').value=='3') && ( document.getElementById ('hDCompanyVerticalSelectByUser').value=='3' ||  document.getElementById ('hDCompanyVerticalSelectByUser').value=='') )
         {
             MandForCompanyVertical();
            
         }  



if(ddlvalidate('ddlOrderStatus')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Order Status is Mandatory";
		    document.getElementById('ddlOrderStatus').focus();
		    return false;}	
 if(ddlvalidate('ddlOrderType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Order Type is Mandatory";
		    document.getElementById('ddlOrderType').focus();
		    return false;}	
		    
		 if(textbox('txtAgencyName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Agency Name is Mandatory";
	  
		      return false;}  
		      if(textbox('txtDateReceived')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Received date is Mandatory";
	document.getElementById('txtDateReceived').focus();
		      return false;}  
		      if(CheckValidOptionalDate('txtDateApproval',"Please enter correct Approval Date")==true && CheckValidOptionalDate('txtDateApplied',"Please enter correct Applied Date")==true && CheckValidOptionalDate('txtDateMessage',"Please enter correct Message sent Date")==true && CheckValidOptionalDate('txtDateExp',"Please enter correct Exp. Installation Date")==true && CheckValidOptionalDate('txtDateSentBack',"Please enter correct Message Sent back  Date")==true && CheckValidOptionalDate('txtDateMdReceivingNeeraj',"Please enter correct Receiving Date")==true && CheckValidOptionalDate('txtDateMdResending',"Please enter correct Resending Date")==true  )		      
    {       }
    else
    {
    return false;
    }
    
    
    
    if(document.getElementById("ddlOrderStatus").value==document.getElementById("hdOrderApproved").value)
{
    if(textbox('txtDateApproval')==false )
    {   
        document.getElementById("lblError").innerText="Approval Date is Mandatory";
        document.getElementById('txtDateApproval').focus();
        return false;
     }  
     
if(document.getElementById("txtOfficeID1").value=="")
    {
        document.getElementById("lblError").innerText="OfficeID is Mandatory";
        document.getElementById('txtOfficeID1').focus();
        return false;
    }
}

          
                      if(document.getElementById('txtAgencyPcReq').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtAgencyPcReq").value,3)==false)
                {
                    document.getElementById('lblError').innerHTML='Agency Pc Required is not valid.';             
                    document.getElementById("txtAgencyPcReq").focus();
                    return false;
                  }
                     if(parseInt(document.getElementById("txtAgencyPcReq").value)>32767)
                    {                   
                    document.getElementById("lblError").innerHTML='Agency Pc Required is not valid.';             
                    document.getElementById("txtAgencyPcReq").focus();
                    return false;
                    } 
                }
                     if(document.getElementById('txtAmadeusPcReq').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtAmadeusPcReq").value,3)==false)
                {
                    document.getElementById('lblError').innerHTML='Amadeus Pc Required is not valid.';             
                    document.getElementById("txtAmadeusPcReq").focus();
                    return false;
                  }
                    if(parseInt(document.getElementById("txtAmadeusPcReq").value)>32767)
                    {                   
                    document.getElementById("lblError").innerHTML='Amadeus Pc Required is not valid.';           
                    document.getElementById("txtAmadeusPcReq").focus();
                    return false;
                    } 
                }
                     if(document.getElementById('txtAmadeusPrinterReq').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtAmadeusPrinterReq").value,3)==false)
                {
                    document.getElementById('lblError').innerHTML='Amadeus Printer Required is not valid.';             
                    document.getElementById("txtAmadeusPrinterReq").focus();
                    return false;
                  }
                    if(parseInt(document.getElementById("txtAmadeusPrinterReq").value)>32767)
                    {                   
                    document.getElementById("lblError").innerHTML='Amadeus Printer Required is not valid.';           
                    document.getElementById("txtAmadeusPrinterReq").focus();
                    return false;
                    } 
                }
                 if (document.getElementById("txtremarks").value.trim().length>1000)
            {
                 document.getElementById("lblError").innerHTML="Rmarks cann't be more than 1000 characters."
                 document.getElementById("txtremarks").focus();
                 return false;
            } 
             
	
		return true;         
}

        
        
        
        
        function ResetNPIdMainOrder()
  {
      document.getElementById('txtPlainId').value='';  
      document.getElementById('hdNPID').value='';  
  }
  
 function ChkApprovalDateMainOrder()
  {
  if(document.getElementById('txtDateApproval').value=="")
  {
  alert("Enter Approval Date","WAY");
  document.getElementById("txtDateApproval").focus();
  return false;
  }
  }
  
  
  
function PopupPagePendingWithMainOrder()
{
 
           var type;      
            var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
            if (strEmployeePageName!="")
            {
                type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
           
           // type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
            
            }
}


function PopupHistoryPageMainOrder()
{
 var orderValue=document.getElementById('hdOrderID').value;

 var type="../Popup/PUSR_OrderHistory.aspx?OrderID="+orderValue;
 
 
            if (window.showModalDialog) 
            {
                  strReturn=window.showModalDialog(type,orderValue,'dialogWidth:880px;dialogHeight:600px;help:no;');       
            }
            else
            {
             strReturn=window.open(type,orderValue,'height=600,width=880,top=30,left=20,scrollbars=1'); 
            }

}

  function PopupAgencyPageMainOrder()
{

     var type;
            type = "TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
   
}

    
        // Modified by  on 04-01-11
   function ViewOrderDocMainOrder()
    {   
        var lCode=document.getElementById('hdAgencyNameId').value;  
        var Fileno=document.getElementById('hdFileNo').value;
        var AName=document.getElementById('txtAgencyName').value;
        var OrderNo=document.getElementById('txtOrderNumber').value;
        var AOrderId=document.getElementById('hdOrderID').value;
        
          var type;
        type = "TASR_ViewOrderDoc.aspx?LCode=" + lCode +"&FileNo=" + Fileno + "&AgencyName=" + AName +"&OrderNo="+OrderNo + "&AOrderId=" + AOrderId ; 
        if(Fileno==null || Fileno=="")
        {
        document.getElementById('lblError').value="File Number Mandatory";
        return false;
        }    
          if(OrderNo==null || OrderNo=="")
        {
        document.getElementById('lblError').value="Order Number Mandatory";
        return false;
        }
        
        if(lCode==null || lCode=="")
        {
        document.getElementById('lblError').value="LCode Mandatory";
        return false;
        }  
          
              window.open(type,'parent','height=600,width=880,top=30,left=20,scrollbars=1,resizable=1');       
              return false;
      
    }
    
    


function PopupAgencyGroupOfficeIDMainOrder()
{
  var type;
  var city=document.getElementById('hdCity').value;
  var lcode=document.getElementById('hdAgencyNameId').value;
     

    if(lcode=="")
    {
        document.getElementById('lblError').innerHTML='Select Agency'; 
        return false;
    }
    else
    {
                    var type;
                    type = "../TravelAgency/MSSR_AgencyOfficeID.aspx?Popup=T"+"&City="+ city+ "&Lcode=" + lcode; 
   	                window.open(type,"aa2","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                    return false;
    }

}


//written by Neeraj

 function  PopupISPPlanIdMainOrder()
        {
          var Lcode= document.getElementById('hdAgencyNameId').value;
           document.getElementById('lblError').innerHTML=''
           
         if(document.getElementById('drpIspList').value=="")
              {
                  document.getElementById('lblError').innerHTML='ISP name is mandatory.';             
                  document.getElementById("drpIspList").focus();
                  return false;
              }
          var w = document.getElementById('drpIspList').selectedIndex;
          var IspName = document.getElementById('drpIspList').options[w].text;

          var IspId= document.getElementById('drpIspList').value;
           var varProviderID=  document.getElementById('hdIspProviderID').value;
            var type;      
            type = "../ISP/MSSR_ISPPlan.aspx?Popup=T&IspName=" + IspName + "&IspId=" + IspId + "&Lcode=" + Lcode + "&IspProviderID=" + varProviderID ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
        }
        
        
        /*Code  for Travel Agency Main Order */   
        
        
        
            /*Code for Manage Agency Page */


        function onlyDeleteAllowedManageAgency()
    {
 
         if ((event.keyCode!=46) && (event.keyCode!=8) && (event.keyCode!=9))
         {
            return false;
         }

    }
    
    
    function PopupIPDefinitionManageAgency()
 {
          var LCode=document.getElementById("hdLcode").value;

                 var type="../ISP/ISPSR_IPPool.aspx?Popup=T&Lcode="+LCode;         
   	             window.open(type,"aa12","height=600,width=905,top=30,left=20,scrollbars=1,status=1");	
                 return false;
       
 }
      
      
      var st;
function SendCustomerIDManageAgency(s)
{
   var id
   st=s;
   if(s=='drpCity')
   {
      id=document.getElementById('drpCity').value;
   }
   id=s+'|'+id;           
   CallServer(id,"This is context from client");
   return false;
}
  
function ReceiveServerDataManageAgency(args, context)
{
var objvar=args;
var Var_array=objvar.split("|");

    //document.getElementById("txtCountry").value=args;
    document.getElementById('txtCountry').value=Var_array[0];
    document.getElementById('txtAoffice').value=Var_array[1];
}

  
   function PopupAgencyGroupManageAgency()
{
            var type;
            type = "../Setup/MSSR_ManageAgencyGroup.aspx?Popup=T" ;
   	        window.open(type,"aa9","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;

}     
    
    function PopupEmployeeManageAgency()
{
         
           var type;    
           


        var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
        if (strEmployeePageName!="")
        {
              type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;  
           // type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa11","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
       }
}


function GroupCaseManageAgency()
{
             var LCode=document.getElementById("hdLcode").value;
             if (LCode!='' )
             {            
                 var type="../TravelAgency/TASR_GroupCase.aspx?Popup=T&Lcode="+LCode;         
   	             window.open(type,"aa","height=600,width=905,top=30,left=20,scrollbars=1,status=1");	
                 return false;
              }
}


function OrderFeasibilityManageAgency()
{

    var LCode=document.getElementById("hdEnLcode").value;
     if ( LCode!='' )
             {     
                var type="../Popup/PUSR_OrderFeasibility.aspx?Popup=T&Lcode="+LCode;
                window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");
     
      }

}


function ShowAllCRSProductivityManageAgency()
{
      var Agency=document.getElementById("txtName").value;
      var Add=document.getElementById("txtAddress1").value;
      var dropdown=document.getElementById("drpCity");
      var City=dropdown.options[dropdown.selectedIndex].text;
      var LimAoff=document.getElementById("hdLimAoff").value;
      var LimReg=document.getElementById("hdLimReg").value;
      var LimOwnOff=document.getElementById("hdLimOwnOff").value;
      var LCode=document.getElementById("hdEnLcode").value;
    //  var Aoff=document.getElementById("hdEnAoffice").value
      var Aoff=document.getElementById("txtAoffice").value;
      var Country=document.getElementById("txtCountry").value;
      var parameter="&Aoff=" + Aoff + "&LCode=" + LCode + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff;
      var type="../Productivity/PRD_BIDT_CRSDetails.aspx?Popup2=T"+parameter;
             if ( LCode!='' )
             {  
               window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");
              }
}


function Show1AProductivityManageAgency()
{
     var Agency=document.getElementById("txtName").value;
     var Add=document.getElementById("txtAddress1").value;
     var dropdown=document.getElementById("drpCity");
     var City=dropdown.options[dropdown.selectedIndex].text;
     var LCode=document.getElementById("hdEnLcode").value;
     var Country=document.getElementById("txtCountry").value;
     var LimAoff=document.getElementById("hdLimAoff").value;
   // var Aoff=document.getElementById("hdEnAoffice").value
   
    var Aoff=document.getElementById("txtAoffice").value;
   
     
     var LimReg=document.getElementById("hdLimReg").value;
     var LimOwnOff=document.getElementById("hdLimOwnOff").value;
     var parameter="&Aoff=" + Aoff + "&LCode=" + LCode + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff;
     var type="../Productivity/PRD_BIDT_Details.aspx?Popup2=T"+parameter;
     var strReturn;
       if ( LCode!='' )
             {  
              window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");
            }
 }
 
 
 function ShowDailyBookingsManageAgency()
{

    var LCode=document.getElementById("hdEnLcode").value;
      if ( LCode!='' )
             {
           var type="../Popup/PUSR_DailyBooking.aspx?Popup=T&Lcode="+LCode;
            window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");
           }
   
}

function PopHistoryManageAgency()
{
     var LCode=document.getElementById("hdEnLcode").value;
      if ( LCode!='' )
      {
      var type;
      type = "../Popup/PUUP_AgencyHistory.aspx" 
         
      if (window.showModalDialog)
      {
          window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
      }
      else
      {
          window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1,status=1');       
      }	 
    } 
}


function PopConnectivityHistoryManageAgency()
{

  var LCode=document.getElementById("hdEnLcode").value;
     if ( LCode!='' )
     {
          var type;
          type = "../Popup/PUUP_ConnectivityHistory.aspx"              
          if (window.showModalDialog)
          {
              window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
          }
          else
          {
              window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
          }	  
     }
}



function ValidateAgencyManageAgency()
{
    if(document.getElementById("txtAgencyGroup").value=='')
    {
        document.getElementById("lblError").innerHTML='Agency Group is Mandatory.';
        document.getElementById("txtAgencyGroup").focus();
        return false;
    }

    if(document.getElementById("txtName").value.trim()=='')
    {
        document.getElementById("lblError").innerHTML='Name is Mandatory.';
        document.getElementById("txtName").focus();
        return false;
    }

    if(document.getElementById("txtAddress1").value.trim()=='')
    {
        document.getElementById("lblError").innerHTML='Address1 is Mandatory.';
        document.getElementById("txtAddress1").focus();
        return false;
    }

    if(document.getElementById("drpCity").selectedIndex==0)
    {
        document.getElementById("lblError").innerHTML='City is Mandatory.';
        document.getElementById("drpCity").focus();
        return false;
    }  
    if(document.getElementById("txtEmail").value.trim()!='')
    {              
        if(CheckEmailForAgency(document.getElementById("txtEmail").value)==false)
        {
            document.getElementById("lblError").innerHTML='Enter valid email Id.';
            document.getElementById("txtEmail").focus();
            return false;
        }
    }       
     if(document.getElementById('txtDateOnline').value != '')
     {
        if (isDate(document.getElementById('txtDateOnline').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Enter valid online date.";			
	       document.getElementById('txtDateOnline').focus();
	       return(false);  
        }
     }
     if(document.getElementById('txtDateOffline').value != '')
     {
        if (isDate(document.getElementById('txtDateOffline').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Enter valid offline date.";			
	       document.getElementById('txtDateOffline').focus();
	       return(false);  
        }
     }
   
     if(document.getElementById('txtPrimaryDate').value != '')
     {
        if (isDate(document.getElementById('txtPrimaryDate').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Enter valid primary date.";			
	       document.getElementById('txtPrimaryDate').focus();
	       return(false);  
        }
     }
      if(document.getElementById('txtBackupDate').value != '')
     {
        if (isDate(document.getElementById('txtBackupDate').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Enter valid Backup Date.";			
	       document.getElementById('txtBackupDate').focus();
	       return(false);  
        }
     }
       
   
}

  function URLEncode (clearString) 
        {
          var output = '';
          var x = 0;
          clearString = clearString.toString();
          var regex = /(^[a-zA-Z0-9_.]*)/;
          while (x < clearString.length) {
            var match = regex.exec(clearString.substr(x));
            if (match != null && match.length > 1 && match[1] != '') {
    	        output += match[1];
              x += match[1].length;
            } else {
              if (clearString[x] == ' ')
                output += '+';
              else {
                var charCode = clearString.charCodeAt(x);
                var hexVal = charCode.toString(16);
                output += '%' + ( hexVal.length < 2 ? '0' : '' ) + hexVal.toUpperCase();
              }
              x++;
            }
          }
          return output;
        }

function ViewMiscDocManageAgency()
    {     
       var LCode=document.getElementById("hdLcode").value;
           
             if ( LCode!='' )
             {  
                var Fileno=document.getElementById('txtFileNo').value
                var AName=document.getElementById('txtName').value
                   if (AName!=null)
                   {
                     if (AName!='')
                     {
                      AName=URLEncode(AName);
                      }       
                   } 
                   
               // var EnLcode=document.getElementById("hdEnLcode").value;
     
                 var type;
                 type = "TASR_ViewOrderDoc.aspx?TYPE=M&LCode="  + LCode + "&FileNo=" + Fileno + "&AgencyName=" + AName ;
                 window.open(type,'parent','height=600,width=880,top=30,left=20,scrollbars=1,status=1,resizable=1'); 

            }
    }
    

function NewFunctionManageAgency()
 {   
 window.location.href="TAUP_Agency.aspx?Action=I";       
 return false;
  }
  
  
  function PopupAgencyGroupOfficeIDManageAgency()
  {
 
  var type;
  var city=document.getElementById('hdCity').value;
  type = "../Popup/PUSR_OfficeId.aspx?Action=U|"+city; 
   var strReturn;   
  if (window.showModalDialog)
  {     
      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
   }
  else
  {     
     strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
  }	   
  if (strReturn != null)
  {
     var sPos = strReturn; 
     document.getElementById('txtAoffice').value=strReturn;
   }
}  
/*Code for Manage CRS   ForCRS*/


 function PopuporWriteTesxtForCRS()
       {
           var Index;
           Index=document.getElementById('drpCRS').selectedIndex;
           if(document.getElementById('drpCRS').options[Index].value!="1A")
            {            
                document.getElementById('txtOfficeId').readOnly =false;
                document.getElementById('txtOfficeId').className ="textbox";
                document.getElementById('ImgOfficeId').style.display  ="none";
              
                return false;
            }
            else
            {
                document.getElementById('txtOfficeId').readOnly =true; 

                document.getElementById('txtOfficeId').className ="textboxgrey";
                document.getElementById('ImgOfficeId').style.display ="block";
                //document.getElementById('txtOfficeId').value='';
                return false;
            
            }
       }
    
     function DeleteFunctionForCRS()
          {   
            if (confirm("Are you sure you want to delete?")==true)
            {        
                return true;
            }
            return false;
        }
    function ValidationForCRS()
    {
        if(document.getElementById('drpCRS').selectedIndex==0)
        {            
            document.getElementById('lblError').innerHTML='CRS is mandatory.';
            document.getElementById('drpCRS').focus();
            return false;
        }
         if(document.getElementById('txtOfficeId').value.trim()=="")
        {            
            document.getElementById('lblError').innerHTML='OfficeId is mandatory.';
            document.getElementById('txtOfficeId').focus();
            return false;
        }
    }
    function PopupCRSHistoryPageForCRS()
    {   
         var LcodeValue=document.getElementById('hdLcode').value;
         var type="../Popup/PUSR_AgencyCRSHistory.aspx?Lcode="+ LcodeValue;   
        if (window.showModalDialog) 
        {
              strReturn=window.showModalDialog(type,LcodeValue,'dialogWidth:880px;dialogHeight:600px;help:no;');       
        }
        else
        {
         strReturn=window.open(type,LcodeValue,'height=600,width=880,top=30,left=20,scrollbars=1'); 
        }
        
        
      }
 function PopupAgencyGroupOfficeIDForCRS()
   {
   
  var type;
  var lcode=document.getElementById('hdLcode').value;
  var city=document.getElementById('hdCity').value;

  var type;
         type = "../TravelAgency/MSSR_AgencyOfficeID.aspx?Popup=T"+"&City="+ city+ "&Lcode=" + lcode; 
   	        window.open(type,"aa2","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
}

/*Code for Manage CRS */

/*Code for Manage Agency Competetion */

 function DeleteFunctionManageComp()
          {   
            if (confirm("Are you sure you want to delete?")==true)
            {        
                return true;
            }
            return false;
        }
    function ValidationCompetitionManageComp()
    {
        if(document.getElementById('drpCRSCode').selectedIndex==0)
        {            
            document.getElementById('lblError').innerHTML='CRS is mandatory.';
            document.getElementById('drpCRSCode').focus();
            return false;
        }
        if(document.getElementById('txtDateStart').value != '')
        {
            if (isDate(document.getElementById('txtDateStart').value,"d/M/yyyy") == false)	
            {
                document.getElementById('lblError').innerText = "Enter valid Start date.";			
	            document.getElementById('txtDateStart').focus();
	            return(false);  
             }
        }
        if(document.getElementById('txtDateEnd').value != '')
        {
            if (isDate(document.getElementById('txtDateEnd').value,"d/M/yyyy") == false)	
            {
                document.getElementById('lblError').innerText = "Enter valid End date.";			
	            document.getElementById('txtDateEnd').focus();
	            return(false);  
            }
        }
           if (compareDates(document.getElementById('txtDateStart').value,"d/M/yyyy",document.getElementById('txtDateEnd').value,"d/M/yyyy")==1)
        {
           document.getElementById('lblError').innerText = "Start date can't be greater than end date.";			
	       document.getElementById('txtDateStart').focus();
	       return(false);  
        }
         if(document.getElementById('txtPCCount').value != '')
        {
           if(IsDataValid(document.getElementById("txtPCCount").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="PC Count is not valid.";
            document.getElementById("txtPCCount").focus();
            return false;
            } 
        }
          if(document.getElementById('txtPrinterCount').value != '')
        {
           if(IsDataValid(document.getElementById("txtPrinterCount").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Printer Count is not valid.";
            document.getElementById("txtPrinterCount").focus();
            return false;
            } 
        }
    }


/*Code for Manage Agency Competetion */

/*Code for Manage Staff */


 function  TAUPAgencyStaffForStaff()
       {     
           document.getElementById('lblError').innerText=""  ; 
           window.location="TAUP_AgencyStaff.aspx?Id=3&Action=I";
           return false;
       }
        function DeleteFunctionForStaff(CheckBoxObj)
          {   
            document.getElementById('lblError').innerText="";
            if (confirm("Are you sure you want to delete?")==true)
            {    
                window.location.href="TAUP_AgencyStaff.aspx?Id=3&Action=D|"+CheckBoxObj ;
                return false;
            }
        }
      function EditFunctionForStaff(CheckBoxObj)
    {         
              document.getElementById('lblError').innerText =""; 
             window.location ="TAUP_AgencyStaff.aspx?Id=3&Action=U&AGENCYSTAFFID=" + CheckBoxObj;        
              return false;
    }  
   
   function AgencyStaffMandatoryForStaff()
    {
        if (document.getElementById("txtName").value.trim()=="")
         {          
            document.getElementById("lblError").innerHTML="Name is mandatory.";
            document.getElementById("txtName").focus();
            return false;
          
         }
        if (document.getElementById("txtName").value.trim()!="")
         {
           if(IsDataValid(document.getElementById("txtName").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Name is not valid.";
            document.getElementById("txtName").focus();
            return false;
            } 
         }
        
          if (document.getElementById("txtDesig").value.trim()!="")
         {
           if(IsDataValid(document.getElementById("txtDesig").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Designation is not valid.";
            document.getElementById("txtDesig").focus();
            return false;
            } 
         }
       
           if(document.getElementById('txtDob').value.trim() != '')
        {
        if (isDate(document.getElementById('txtDob').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Date of birth is not valid.";			
	       document.getElementById('txtDob').focus();
	       return(false);  
        }
         } 
             if(document.getElementById('txtDow').value.trim() != '')
        {
        if (isDate(document.getElementById('txtDow').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Date of wedding is not valid.";			
	       document.getElementById('txtDow').focus();
	       return(false);  
        }
         } 
        if (compareDates(document.getElementById('txtDob').value,"d/M/yyyy",document.getElementById('txtDow').value,"d/M/yyyy")==1)
        {
           document.getElementById('lblError').innerText = "Date of birth can't be greater than wedding date.";			
	       document.getElementById('txtDob').focus();
	       return(false);  
        }
      
         if(document.getElementById("txtEmail").value.trim()!='')
         {              
        if(checkEmail(document.getElementById("txtEmail").value)==false)
        {
            document.getElementById("lblError").innerHTML='Email is not valid.';
            document.getElementById("txtEmail").focus();
            return false;
        }
        } 
       if (document.getElementById("txtNotes").value.trim().length>300)
        {
             document.getElementById("lblError").innerHTML="Notes can't be greater than 300 characters."
             document.getElementById("txtNotes").focus();
             return false;
        }  
         return true;
     }
     
/*Code for Manage Staff */


/*Code for Manage PC */

function NewAgencyPCInstall()
       {
             // document.getElementById('<%=lblError.ClientId%>').innerText ="";  
            var type;
            type = "../TravelAgency/TAUP_NewAgencyPCInstall.aspx?&Action=I&Popup=T" ;
   	        window.open(type,"AgencyPC","height=360,width=650,top=150,left=170,scrollbars=1,Status=1");	
            return false;           
           
       }
       function New1AAgencyPCInstall()
       {
             // document.getElementById('<%=lblError.ClientId%>').innerText ="";             
            var type;
            type = "../TravelAgency/TAUP_New1APCInstall.aspx?&Action=I&Popup=T" ;
   	        window.open(type,"1APC","height=580,width=645,top=120,left=170,scrollbars=1,Status=1");	
            return false;      
       }
      
       function DeleteFunctionPCInstall(CheckBoxObj)
          {   
            document.getElementById('lblError').innerText="";
            if (confirm("Are you sure you want to delete?")==true)
            {    
                document.getElementById("hdRowId").value=CheckBoxObj; 
            }
            else
            {
             document.getElementById("hdRowId").value="";
             return false;
            }
        }
      function EditFunctionPCInstall(CheckBoxObj,CheckBoxObj2)
    {  
            var type;
            type = "../TravelAgency/TAUP_Agencyor1APCInstall.aspx?&Action=U&Popup=T&ROWID=" + CheckBoxObj + "&PCTYPE=" + CheckBoxObj2 ;        
   	        window.open(type,"1APC","height=610,width=835,top=85,left=170,scrollbars=1,Status=1");	
            return false;   
    }  
    
      function ReplaceFunctionPCInstall(CheckBoxObj,CheckBoxObj2)
    {         

            var type;
            type = "../TravelAgency/TAUP_Agencyor1APCInstall.aspx?&Action=R&Popup=T&ROWID=" + CheckBoxObj + "&PCTYPE=" + CheckBoxObj2 ;   
   	        window.open(type,"1APC","height=610,width=835,top=85,left=170,scrollbars=1,Status=1");	
            return false;   
    }  
    
      function PCInstallHistoryPCInstall(CheckBoxObj)
       { 
            var type;
            type = "../Popup/PUSR_PCInstallationHistory.aspx?&Action=I&Popup=T&ROWID=" + CheckBoxObj;  
   	        window.open(type,"AgencyPC","height=360,width=930,top=150,left=30,scrollbars=1,Status=1");	
            return false;           
           
       }
         function PCDeInstallPCInstall(CheckBoxObj,CheckBoxObj2)
       {   
            var type;
            type = "../TravelAgency/TAUP_AgencyPCDeInstall.aspx?&Action=D&Popup=T&ROWID=" + CheckBoxObj + "&PCTYPE=" + CheckBoxObj2 ;   
   	        window.open(type,"PCDeInstall","height=360,width=650,top=150,left=170,scrollbars=1,Status=1");	
            return false;           
           
       }
/*Code for Manage PC */
/*Code for Manage Product */


function EditValuesAgencyProduct(EDRowValue)
    {           
       window.location.href="TAUP_AgencyProducts.aspx?Action=U&EditCode=" +EDRowValue     
        return false;
    }
    function DeleteFunctionAgencyProduct(EDRowValue)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
            window.location.href="TAUP_AgencyProducts.aspx?Action=D&DeleteCode=" +EDRowValue    
            return false;
        }
    }
    
    function ChkEmptyProductNameAgencyProduct()
    {
    if(document.getElementById('drpProductList').selectedIndex==0)
    {
    document.getElementById("lblError").innerText="Product is Mandatory";
    document.getElementById("drpProductList").focus();
    return false;
    }
    
    
    if(document.getElementById("txtInstallDate").value=="")
    {
    document.getElementById("lblError").innerText="Installation Date is Mandatory";
    document.getElementById("txtInstallDate").focus();
    return false;
    }
    else
    {
        if (isDate(document.getElementById('txtInstallDate').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Installation date is not valid.";			
	       document.getElementById('txtInstallDate').focus();
	       return(false);  
        }
        
    
    }
    
    if(document.getElementById("txtTerminalOnline").value!="")
    {
    if(IsDataValid(document.getElementById("txtTerminalOnline").value,3)==false)
    {
    document.getElementById("lblError").innerText="Number of Terminals is Numeric";
    document.getElementById("txtTerminalOnline").focus();
      return false;
    }
    if((parseInt(document.getElementById("txtTerminalOnline").value)>0) && ((parseInt(document.getElementById("txtTerminalOnline").value))<255))
    {
    }
    else
    {
    document.getElementById("lblError").innerText="Number of Terminals is Invalid or Limit Exceed";
    document.getElementById("txtTerminalOnline").focus();
      return false; 
    }
   // return false;     
    }
    
    
        }
/*Code for Manage Product */


/*Code for Misc Installation */
 function DeleteFunctionAgencyMis(CheckBoxObj)
          {   
            document.getElementById('lblError').innerText="";
            if (confirm("Are you sure you want to delete?")==true)
            {    
                document.getElementById('hdDel').value=CheckBoxObj
                
            }
        }
        
        
      function EditFunctionAgencyMis(CheckBoxObj)
    {         
              document.getElementById('lblError').innerText =""; 
             // alert(""); 
             var type ="TAUP_ModifyMiscInstall.aspx?Action=U&ROWID=" + CheckBoxObj;   
   	        window.open(type,"aa","height=360px,width=860px,top=230,left=120,scrollbars=1,status=1");	
            return false;
    } 
    
       function ReplaceAgencyMis(CheckBoxObj)
    {         
              document.getElementById('lblError').innerText =""; 
             // alert(""); 
             var type ="TAUP_ModifyMiscInstall.aspx?Action=R&ROWID=" + CheckBoxObj;   
   	        window.open(type,"aa","height=360px,width=860px,top=230,left=120,scrollbars=1,status=1");	
            return false;
    } 
    
    
    
    
    function DeinstallAgencyMis(rowID)
    {
                document.getElementById('lblError').innerText =""; 
                var type ="TAUP_DeinstallMiscInstall.aspx?RowId=" + rowID; 
   	        window.open(type,"aa","height=210px,width=570px,top=230,left=120,scrollbars=0,status=1");	
            return false;
    } 
    
    
    function ShowHistoryAgencyMis(rowID)
    {
  
                document.getElementById('lblError').innerText =""; 
                var type ="../Popup/PUSR_MiscHardware.aspx?RowID=" + rowID; 
   	        window.open(type,"aa","height=400px,width=850px,top=130,left=80,scrollbars=1,status=1");	
            return false;
  
    }
    
        function InstallNewHardwareAgencyMis()
        {
            var type;
            type ="TAUP_NewMiscInstall.aspx";
   	        window.open(type,"aa","height=400,width=600,top=230,left=220,scrollbars=1,status=1");	
   	         return false;
         }

/*End of Code for Misc Installation */



/*Code for Agency Ordert Aorder */


function ResetNPIdAorder()
  {
      //document.getElementById('txtPlainId').value='';  
      document.getElementById('hdNPID').value='';  
  }
 
  function DeleteFunctionAorder()
          {   
            if (confirm("Are you sure you want to delete?")==true)
            {        
                return true;
            }
            return false;
        }
  function ValidationAgencyOrderAorder()
    {
     // debugger;
        if ((document.getElementById ('hDCompanyVertical').value=='' || document.getElementById ('hDCompanyVertical').value=='3') && ( document.getElementById ('hDCompanyVerticalSelectByUser').value=='' || document.getElementById ('hDCompanyVerticalSelectByUser').value=='3' ||  document.getElementById ('hDCompanyVerticalSelectByUser').value=='') )
         {
             MandForCompanyVertical();
            
         }  
        if(document.getElementById('ddlOrderType').selectedIndex==0)
        {            
            document.getElementById('lblError').innerHTML='Order type is mandatory.';
            document.getElementById('ddlOrderType').focus();
            return false;
        }
         if(document.getElementById('ddlOrderStatus').selectedIndex==0)
        {            
            document.getElementById('lblError').innerHTML='Order status is mandatory.';
            document.getElementById('ddlOrderStatus').focus();
            return false;
        }
           if(document.getElementById('txtAgencyName').value.trim()=="")
        {            
            document.getElementById('lblError').innerHTML='Agency name is mandatory.';
            document.getElementById('txtAgencyName').focus();
            return false;
        }
          if (document.getElementById('ddlOrderStatus').value ==document.getElementById('hdOrderStatus').value )
          {  
                    if(document.getElementById('txtDateApproval').value == '')
                {       
                   document.getElementById('lblError').innerText = "Approval Date is mandatory.";			
                   document.getElementById('txtDateApproval').focus();
                   return(false);  
               
                 }
          
          }
          if(document.getElementById('txtDateReceived').value == '')
        {       
           document.getElementById('lblError').innerText = "Received Date is mandatory.";			
           document.getElementById('txtDateReceived').focus();
           return(false);  
       
         }
        
           if(document.getElementById('txtDateProcessed').value != '')
        {
        if (isDate(document.getElementById('txtDateProcessed').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Processed Date is not valid.";			
           document.getElementById('txtDateProcessed').focus();
           return(false);  
        }
         }
         
              if(document.getElementById('txtDateApproval').value != '')
        {
        if (isDate(document.getElementById('txtDateApproval').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Approval Date is not valid.";			
           document.getElementById('txtDateApproval').focus();
           return(false);  
        }
         }
        if(document.getElementById('txtDateApplied').value != '')
        {
        if (isDate(document.getElementById('txtDateApplied').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Applied Date is not valid.";			
           document.getElementById('txtDateApplied').focus();
           return(false);  
        }
         }
         if(document.getElementById('txtDateMessage').value != '')
        {
        if (isDate(document.getElementById('txtDateMessage').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Message Date is not valid.";			
           document.getElementById('txtDateMessage').focus();
           return(false);  
        }
         }
         if(document.getElementById('txtDateReceived').value != '')
        {
        if (isDate(document.getElementById('txtDateReceived').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Received Date is not valid.";			
           document.getElementById('txtDateReceived').focus();
           return(false);  
        }
         }
      
               if(document.getElementById('txtDateExp').value != '')
        {
        if (isDate(document.getElementById('txtDateExp').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Exp. Installation Date is not valid.";			
           document.getElementById('txtDateExp').focus();
           return(false);  
        }
         }
               if(document.getElementById('txtDateSentBack').value != '')
        {
        if (isDate(document.getElementById('txtDateSentBack').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Sent Back Date is not valid.";			
           document.getElementById('txtDateSentBack').focus();
           return(false);  
        }
         }
               if(document.getElementById('txtDateMdReceiving').value != '')
            {
            if (isDate(document.getElementById('txtDateMdReceiving').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerText = "Receiving Date is not valid.";			
               document.getElementById('txtDateMdReceiving').focus();
               return(false);  
            }
            }
              if(document.getElementById('txtDateMdResending').value != '')
            {
            if (isDate(document.getElementById('txtDateMdResending').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerText = "Resending Date is not valid.";			
               document.getElementById('txtDateMdResending').focus();
               return(false);  
            }
             }
             

                      if(document.getElementById('txtAgencyPcReq').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtAgencyPcReq").value,3)==false)
                {
                    document.getElementById('lblError').innerHTML='Agency Pc Required is not valid.';             
                    document.getElementById("txtAgencyPcReq").focus();
                    return false;
                  }
                     if(parseInt(document.getElementById("txtAgencyPcReq").value)>32767)
                    {                   
                    document.getElementById("lblError").innerHTML='Agency Pc Required is not valid.';             
                    document.getElementById("txtAgencyPcReq").focus();
                    return false;
                    } 
                }
                     if(document.getElementById('txtAmadeusPcReq').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtAmadeusPcReq").value,3)==false)
                {
                    document.getElementById('lblError').innerHTML='Amadeus Pc Required is not valid.';             
                    document.getElementById("txtAmadeusPcReq").focus();
                    return false;
                  }
                    if(parseInt(document.getElementById("txtAmadeusPcReq").value)>32767)
                    {                   
                    document.getElementById("lblError").innerHTML='Amadeus Pc Required is not valid.';           
                    document.getElementById("txtAmadeusPcReq").focus();
                    return false;
                    } 
                }
                     if(document.getElementById('txtAmadeusPrinterReq').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtAmadeusPrinterReq").value,3)==false)
                {
                    document.getElementById('lblError').innerHTML='Amadeus Printer Required is not valid.';             
                    document.getElementById("txtAmadeusPrinterReq").focus();
                    return false;
                  }
                    if(parseInt(document.getElementById("txtAmadeusPrinterReq").value)>32767)
                    {                   
                    document.getElementById("lblError").innerHTML='Amadeus Printer Required is not valid.';           
                    document.getElementById("txtAmadeusPrinterReq").focus();
                    return false;
                    } 
                }
                if (document.getElementById("txtremarks").value.trim().length>1000)
            {
                 document.getElementById("lblError").innerHTML="Rmarks can't be greater than 1000 characters."
                 document.getElementById("txtremarks").focus();
                 return false;
            } 
             
            // alert(document.getElementById('ddlOrderStatus').value =='7');
          if (document.getElementById('ddlOrderStatus').value ==document.getElementById('hdOrderStatus').value )
          {  
               if(document.getElementById('txtOfficeID1').value.trim()=="")
            {            
                document.getElementById('lblError').innerHTML='Office ID is mandatory.';
                document.getElementById('txtOfficeID1').focus();
                return false;
            }   
          } 
                
    }


function FillOrderTypeAorder()
   {
        var args;
        var rdlNewCancelId=document.getElementById('rdlNewCancel_0').checked;
        if(rdlNewCancelId==true)
        {
         args=document.getElementById('hdOrderTypeNew').value;
        }
        else
        {
        args=document.getElementById('hdOrderTypeCancel').value;
        }
        //alert(args);
          var obj = new ActiveXObject("MsXml2.DOMDocument");
          obj.loadXML(args);
   	   

           var ddlOrders = document.getElementById('ddlOrderType');
			for (var count = ddlOrders.options.length-1; count >-1; count--)
			{
				ddlOrders.options[count] = null;
			}
		    var orders = obj.getElementsByTagName('ORDER_TYPE');
		    var codes='';
			var names="-- Select One --";
			var text; 
		
			var listItem;
			listItem = new Option(names, codes);
			ddlOrders.options[ddlOrders.length] = listItem;
			for (var count = 0; count < orders.length; count++)
			{
				codes= orders[count].getAttribute("ORDERTYPEID"); 
			    names=orders[count].getAttribute("ORDER_TYPE_NAME"); 
				listItem = new Option(names, codes);
				ddlOrders.options[ddlOrders.length] = listItem;
			}
			
}
   
   
   
function PopupPagePendingWithAorder()
{

           var type;               

         var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
        if (strEmployeePageName!="")
        {
           type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;  
        //    type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
       }

}
function PopupPageProcessedByAorder()
{
    var type="../Popup/PUSR_Employee.aspx"
     var strReturn;
     
     if (window.showModalDialog) {
      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
        }
    else
    {     strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       }	     
          
      if (strReturn != null)
      {
          var sPos = strReturn.split('|'); 
          document.getElementById('hdProcessedById').value=sPos[0];
          document.getElementById('txtProcessedBy').value=sPos[1];
      }  
}
function PopupAgencyTypeAorder()
{
    var type="../Popup/PUSR_Agency.aspx"
     var strReturn;
     
     if (window.showModalDialog) {
      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
        }
    else
    {     strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       }	     
          
      if (strReturn != null)
      {
          var sPos = strReturn.split('|'); 
          document.getElementById('hdAgencyNameId').value=sPos[0];
          document.getElementById('txtAgencyName').value=sPos[1];
          document.getElementById('txtAgencyAddress').value=sPos[2];
      }  
}
function ChkApprovalDateAorder()
  {
  if(document.getElementById('txtDateApproval').value=="")
  {
     document.getElementById('lblError').innerHTML='Approval Date is mandatory.';
 
  document.getElementById("txtDateApproval").focus();
  return false;
  }
  }
    // Written for showing History Page
    function PopupHistoryPageAorder(objOrderId)
    { 
        //alert("abhi");
         var orderValue=objOrderId;
         var type="../Popup/PUSR_OrderHistory.aspx?OrderId=" + orderValue; 
        if (window.showModalDialog) 
        {
              strReturn=window.showModalDialog(type,orderValue,'dialogWidth:880px;dialogHeight:600px;help:no;');       
        }
        else
        {
         strReturn=window.open(type,orderValue,'height=600,width=880,top=30,left=20,scrollbars=1'); 
        }
    }
    function ViewDocAorder(args,Lcode,OrderId)
    {       
        var objvar=args;
        //alert(args);
        var  Var_array=objvar.split("|");
        var type="TASR_ViewOrderDoc.aspx?OrderNo=" + Var_array[0] + "&Fileno=" + Var_array[1] + "&AgencyName=" +  Var_array[2] + "&LCode=" + Lcode + "&AOrderId=" + OrderId   ;
        //window.location.href="TASR_ViewOrderDoc.aspx?OrderNo=" + Var_array[0] + "&Fileno=" + Var_array[1] + "&AgencyName=" +  Var_array[2]  ; 
        window.open(type,'parent','height=600,width=880,top=30,left=20,scrollbars=1,resizable=1');       
        return false;
    }
    
    function ViewPtypeReport(args,args1)
    {       

       var objvar=args;
        var objvar1=args1;
        
        var  Var_array=objvar.split("|");
        var  Var_array1=objvar1.split("|");
        
        //var type="../RPSR_ReportShow.aspx?Case=PTypeChallan&OrderNo=" + Var_array[0] + "&Fileno=" + Var_array[1] + "&AgencyName=" +  Var_array[2] + "&OrderType=" + Var_array1[0] + "&OrderQty=" +Var_array1[1] + "&OrderRemark="  + Var_array1[3] +"&IsOrderType" +Var_array1[4];
          var type="../RPSR_ReportShow.aspx?Case=PTypeChallan&OrderNo=" + Var_array[0] + "&Fileno=" + Var_array[1] + "&AgencyName=" +  Var_array[2] + "&OrderType=" + Var_array1[0] + "&OrderQty=" +Var_array1[1] + "&OrderQtyAPC="  + Var_array1[2] + "&OrderRemark="  + Var_array1[3] +"&IsOrderType=" +Var_array1[4];

       
        window.open(type,'parent','height=600,width=880,top=30,left=20,scrollbars=1,resizable=1');       
        return false;
    }

    
     function NewFunctionAorder()
    {       
         window.location.href="TAUP_AgencyOrder.aspx?Id=7"
            return false;    
     
    }
    
         function  PopupISPPlanIdAorder()
        {
            var Lcode= document.getElementById('hdLcode').value;
            document.getElementById('lblError').innerHTML=''           
             if(document.getElementById('drpIspName').value=="")
                  {
                      document.getElementById('lblError').innerHTML='ISP name is mandatory.';             
                      document.getElementById("drpIspName").focus();
                      return false;
                  }
              var w = document.getElementById('drpIspName').selectedIndex;
              var IspName = document.getElementById('drpIspName').options[w].text;

              var IspId= document.getElementById('drpIspName').value;


           var varProviderID=  document.getElementById('hdIspProviderID').value;
           var type;      
            type = "../ISP/MSSR_ISPPlan.aspx?Popup=T&IspName=" + IspName + "&IspId=" + IspId + "&Lcode=" + Lcode + "&IspProviderID=" + varProviderID ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;


              
        }
 function PopupAgencyGroupOfficeIDAorder()
   {
   
//  var type;
   var city=document.getElementById('hdCity').value;
   var lcode=document.getElementById('hdLcode').value;
     var type;
            type = "../TravelAgency/MSSR_AgencyOfficeID.aspx?Popup=T"+"&City="+ city+ "&Lcode=" + lcode; 
   	        window.open(type,"aa2","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
}       
     /*  Code for Manage Employee Update Page*/
            
          
            function PageLoadMethodForEmployee()
            {
                    //CheckRestrictEmployeeData();
                    return false; 
            }
            function ValidateEmployeeDataOnSave()
            {
            
            
             if(document.getElementById("txtContactPerson").value=='')
                {
                    document.getElementById("lblError").innerHTML='Contact Person Name is Mandatory.';
                    document.getElementById("txtContactPerson").focus();
                    return false;
                }
            
               
                if(document.getElementById("txtEmployeeName").value=='')
                {
                    document.getElementById("lblError").innerHTML='Employee Name is Mandatory.';
                    document.getElementById("txtEmployeeName").focus();
                    return false;
                }
                
                if(IsDataValid(document.getElementById("txtEmployeeName").value,2)==false)
                {
                     document.getElementById("lblError").innerHTML="Employee Name is not valid.";
                     document.getElementById("txtEmployeeName").focus();
                     return false;
                }
                
                if(document.getElementById("drpDesignation").selectedIndex==0)
                {
                    document.getElementById("lblError").innerHTML='Designation is Mandatory.';
                    document.getElementById("drpDesignation").focus();
                    return false;
                }
                
                 
                if(document.getElementById("txtMobileNumber").value=='')
                {
                    document.getElementById("lblError").innerHTML='Mobile Number is Mandatory.';
                    document.getElementById("txtMobileNumber").focus();
                    return false;
                }
                
                if(IsDataValid(document.getElementById("txtMobileNumber").value,4)==false)
                {
                     document.getElementById("lblError").innerHTML="Mobile No. Format is Invalid.";
                     document.getElementById("txtMobileNumber").focus();
                     return false;
                }
                
                if(document.getElementById("txtEmail").value=='')
                {
                    document.getElementById("lblError").innerHTML='Email ID is Mandatory.';
                    document.getElementById("txtEmail").focus();
                    return false;
                }
                
                if(document.getElementById("txtEmail").value!='')
                {              
                    if(checkEmail(document.getElementById("txtEmail").value)==false)
                    {
                        document.getElementById("lblError").innerHTML='Enter valid email Id.';
                        document.getElementById("txtEmail").focus();
                        return false;
                    }
                
                                
                if (document.getElementById("chkLoginRequired").checked==true)
                {                
                    if(document.getElementById("txtLogin").value=='')
                    {
                        document.getElementById("lblError").innerHTML='Login Id is Mandatory.';
                        document.getElementById("txtLogin").focus();
                        return false;
                    } 
                
                    if(document.getElementById("txtPassword").value=='')
                    {
                        document.getElementById("lblError").innerHTML='Password is Mandatory.';
                        document.getElementById("txtPassword").focus();
                        return false;
                    }
                   
                      if(document.getElementById("txtRetypePassword").value=='')
                    {
                        document.getElementById("lblError").innerHTML='Retype Password is Mandatory.';
                        document.getElementById("txtRetypePassword").focus();
                        return false;
                    }
                
                    if(document.getElementById("txtPassword").value!=document.getElementById("txtRetypePassword").value)
                       {
                          document.getElementById("lblError").innerHTML="Enter password and retype password same.";
                          document.getElementById("txtPassword").focus();
                          return false;           
                       } 
                       
                    if(document.getElementById("txtIPAddress").value=='')
                        {
                            document.getElementById("lblError").innerHTML='IPAddress can not be blank.';
                            document.getElementById("txtIPAddress").focus();
                            return false;
                        }                       
                    if (document.getElementById("chkAgmntSigned").checked==false)
                        {
                            document.getElementById("lblError").innerHTML='Agreement singed should be checked.';
                            document.getElementById("chkAgmntSigned").focus();
                            return false;
                        }                       
                        
                    }   
              }    
                      /*    commented on 27 mar 09
                     if(document.getElementById("txtDOJ").value=='')
                {
                    document.getElementById("lblError").innerHTML='Date of Joining is Mandatory.';
                    document.getElementById("txtDOJ").focus();
                    return false;
                }
                
                   
                   if(document.getElementById('txtDOJ').value != '')
                 {
                    if (isDate(document.getElementById('txtDOJ').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Enter valid date of joining.";			
	                   document.getElementById('txtDOJ').focus();
	                   return(false);  
                    }
                 } */
                 if(document.getElementById('txtDOL').value != '')
                 {
                    if (isDate(document.getElementById('txtDOL').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Enter valid date of leaving.";			
	                   document.getElementById('txtDOL').focus();
	                   return(false);  
                    }
                 } 
                 /*
                  if(document.getElementById('txtDOL').value != '')
                 {
                 
                   if (compareDates(document.getElementById('txtDOJ').value,"d/M/yyyy",document.getElementById('txtDOL').value,"d/M/yyyy")==1)
                    {
                       document.getElementById('lblError').innerText = "Date of Leaving can't be less than Joining date.";			
	                   document.getElementById('txtDOL').focus();
	                   return(false);  
                    }
                 } 
                 */
                  if(document.getElementById("drpDepartment").selectedIndex==0)
                {
                    document.getElementById("lblError").innerHTML='Department is Mandatory.';
                    document.getElementById("drpDepartment").focus();
                    return false;
                }
                if(document.getElementById("drpAoffice").selectedIndex==0)
                {
                    document.getElementById("lblError").innerHTML='Aoffice is Mandatory.';
                    document.getElementById("drpAoffice").focus();
                    return false;
                } 
                 
                 if(document.getElementById("drpHOD").selectedIndex==0)
                {
                    document.getElementById("lblError").innerHTML='Head of Department is Mandatory.';
                    document.getElementById("drpHOD").focus();
                    return false;
                }
                   
                    if(document.getElementById("drpImmediateSuperVisor").selectedIndex==0)
                {
                    document.getElementById("lblError").innerHTML='Immediate Supervisor is Mandatory.';
                    document.getElementById("drpImmediateSuperVisor").focus();
                    return false;
                }
                 
         
                     if(document.getElementById("drpCity").selectedIndex==0)
                {
                    document.getElementById("lblError").innerHTML='City is Mandatory.';
                    document.getElementById("drpCity").focus();
                    return false;
                } 
                
                if (document.getElementById("drpRestrict").value=="4")
                {
                
                  if(document.getElementById("drpRegionList").selectedIndex==0)
                    {
                        document.getElementById("lblError").innerHTML='Region List is Mandatory.';
                        document.getElementById("drpRegionList").focus();
                        return false;
                    }
                }    
            }

            function CheckRestrictEmployeeData()
            {
                
//                if(document.getElementById("drpRestrict").selectedIndex==3)
//                {
//                    document.getElementById("drpRegionList").disabled=false;        
//                    return false;
//                }
//                else    
//                {
//                    document.getElementById("drpRegionList").disabled=true;
//                     document.getElementById("drpRegionList").selectedIndex=0;
//                    
//                    return false;
//                }
            }
     
     /* End of Code for Manage Employee Update Page*/      
     
     
     /* Manage Employee IP Pool*/
      function ValidateEmployeeIPPool()
        {
            if(document.getElementById("txtIP").value=='')
            {
                document.getElementById("lblError").innerHTML='IP Address Mandatory.';
                document.getElementById("txtIP").focus();
                return false;
            }
            if(isValidIPAddress(document.getElementById("txtIP").value)==false)
            {
                document.getElementById("lblError").innerHTML='Enter valid IP Address.';
                document.getElementById("txtIP").focus();
                return false;
            }    
        }
     
     /* end of Manage Emplyee IP Pool */ 
     
     /* Code for Search Isp Order*/
                 function PopupISPplanForIspOrder()
             { 
                         var type;          
                        type = "../ISP/MSSR_ISPPlan.aspx?Popup=T" ;   	     
   	                    window.open(type,"IspOrder","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                        return false;
             }
                 function CheckValidationIspOrder()
                {       
                    if(document.getElementById('txtOrderDateFrom').value != '')
                    {
                    if (isDate(document.getElementById('txtOrderDateFrom').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Order Date From is not valid.";			
	                   document.getElementById('txtOrderDateFrom').focus();
	                   return(false);  
                    }
                     } 
                     
                      if(document.getElementById('txtOrderDateTo').value != '')
                    {
                    if (isDate(document.getElementById('txtOrderDateTo').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Order Date To is not valid.";			
	                   document.getElementById('txtOrderDateTo').focus();
	                   return(false);  
                    }
                     }   
                        if(document.getElementById('txtOnlineDateFrom').value != '')
                    {
                    if (isDate(document.getElementById('txtOnlineDateFrom').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Date of online From is not valid.";			
	                   document.getElementById('txtOnlineDateFrom').focus();
	                   return(false);  
                    }
                     }  
                          if(document.getElementById('txtOnlineDateTo').value != '')
                    {
                    if (isDate(document.getElementById('txtOnlineDateTo').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Date of online To is not valid.";			
	                   document.getElementById('txtOnlineDateTo').focus();
	                   return(false);  
                    }
                     }    
                     if(document.getElementById('txtExpOnlineDateFrom').value != '')
                    {
                    if (isDate(document.getElementById('txtExpOnlineDateFrom').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Expected Online Date From is not valid.";			
	                   document.getElementById('txtExpOnlineDateFrom').focus();
	                   return(false);  
                    }
                     }   
                        if(document.getElementById('txtExpOnlineDateTo').value != '')
                    {
                    if (isDate(document.getElementById('txtExpOnlineDateTo').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Expected Online Date To is not valid.";			
	                   document.getElementById('txtExpOnlineDateTo').focus();
	                   return(false);  
                    }
                     }                  
                        if(document.getElementById('txtOrderDateTo').value != '' && document.getElementById('txtOrderDateFrom').value != '')
                    {
                    if (compareDates(document.getElementById('txtOrderDateFrom').value,"d/M/yyyy",document.getElementById('txtOrderDateTo').value,"d/M/yyyy")=='1')	
                    {
                       document.getElementById('lblError').innerText = "Order Date To is shorter than Order Date From.";			
                       document.getElementById('txtOrderDateTo').focus();
                       return(false);  
                    }
                   }   
               
                     if(document.getElementById('txtOnlineDateTo').value != '' && document.getElementById('txtOnlineDateFrom').value != '')
                    {
                    if (compareDates(document.getElementById('txtOnlineDateFrom').value,"d/M/yyyy",document.getElementById('txtOnlineDateTo').value,"d/M/yyyy")=='1')	
                    {
                       document.getElementById('lblError').innerText = "Online Date To is shorter than Online Date From.";			
                       document.getElementById('txtOnlineDateTo').focus();
                       return(false);  
                    }
                   } 
                   
                      if(document.getElementById('txtExpOnlineDateTo').value != '' && document.getElementById('txtExpOnlineDateFrom').value != '')
                    {
                    if (compareDates(document.getElementById('txtExpOnlineDateFrom').value,"d/M/yyyy",document.getElementById('txtExpOnlineDateTo').value,"d/M/yyyy")=='1')	
                    {
                       document.getElementById('lblError').innerText = "Expected Online Date To is shorter than Expected Online Date From.";			
                       document.getElementById('txtExpOnlineDateTo').focus();
                       return(false);  
                    }
                   } 
                     
                        
               }
 
                    function NewFunctionIspOrder()
                {       
                     window.location.href="ISPUP_Order.aspx?Action=New";
                        return false;    
                 
                }
     
                 function EditFunctionIspOrder(CheckBoxObj,CheckBoxObj2)
                    {           
                          window.location.href="ISPUP_Order.aspx?IspOrderId=" + CheckBoxObj + "&Lcode=" + CheckBoxObj2 ;               
                          return false;
                    }
                function DeleteFunctionIspOrder(CheckBoxObj)
                {       
                           if (confirm("Are you sure you want to delete?")==true)
                              {        
                                document.getElementById("hdDeleteISPOrderID").value= CheckBoxObj ;   
                              }
                            else
                            {
                             document.getElementById("hdDeleteISPOrderID").value="";
                             return false;
                            }
                }
     
 
             function AdvanceSearchForIspOrder()
            {           
                if(document.getElementById('hdAdvanceSearch').value=="1")
                {
                    document.getElementById('btnUp').src="../images/down.jpg";            
                    document.getElementById('pnlAdvanceSearch').style.display ='none'
                    document.getElementById('hdAdvanceSearch').value='0';
                }
                else
                {
                    document.getElementById('btnUp').src='../images/up.jpg';           
                    document.getElementById('pnlAdvanceSearch').style.display ='block'
                    document.getElementById('hdAdvanceSearch').value='1';
                }        
            }
            function OnloadAdvanceSearchForIspOrder()
            {            
               if(document.getElementById('hdAdvanceSearch').value=="1")
               {   
                   document.getElementById('btnUp').src='../images/up.jpg';  
                   document.getElementById('pnlAdvanceSearch').style.display ='block'            
               }
               else
               {
                  document.getElementById('btnUp').src="../images/down.jpg";
                   document.getElementById('pnlAdvanceSearch').style.display ='none'
               }
             }
               function PopupAgencyPageISPOrder()
                {
                    var type;         
                    type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	                window.open(type,"IspOrder","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                    return false;
                }
     /* End of Code for Search Isp Order*/
     
     
     /* Code for Search Isp Order Report */
           function  CheckValidationForISPOrderReport()
    {
                  if(parseInt(document.getElementById("drpYearFrom").value)>parseInt(document.getElementById("drpYearTo").value))
                    {         
                         document.getElementById("lblError").innerHTML='Range is not valid.';           
                         document.getElementById("drpYearTo").focus();
                          return false;
                        
                    } 
                  if(parseInt(document.getElementById("drpYearFrom").value)==parseInt(document.getElementById("drpYearTo").value))
                    {                   
                      if (parseInt(document.getElementById("drpMonthFrom").value ) >   parseInt(document.getElementById("drpMonthTo").value)) 
                      {         
                         document.getElementById("lblError").innerHTML='Range is not valid.';           
                         document.getElementById("drpMonthFrom").focus();
                          return false;
                        }
                    } 
    }
     function PopupAgencyPageISPOrderReport()
        {
            var type;       
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
     /* End of Code for Search Isp Order Report */
     
     /* Code for Isp Pending Cancellation */
     
     
         function ValidationISPPendingCancellation()
    {
                  if(document.getElementById('txtNPID').value == '')
                {            
                    document.getElementById('lblError').innerHTML='NPID is mandatory.';
                    document.getElementById('txtNPID').focus();
                    return false;
                }
              if(document.getElementById('drpIspOrderStatus').selectedIndex==0)
                {            
                    document.getElementById('lblError').innerHTML='ISP Order Status is mandatory.';
                    document.getElementById('drpIspOrderStatus').focus();
                    return false;
                }
                    if(document.getElementById('drpNewOrderNoForCan').value=='')
                {            
                    document.getElementById('lblError').innerHTML='Order No. is mandatory.';
                    document.getElementById('drpNewOrderNoForCan').focus();
                    return false;
                }
          
    
                  if(document.getElementById('txtOrderDate').value != '')
                    {
                    if (isDate(document.getElementById('txtOrderDate').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Order Date is not valid.";			
	                   document.getElementById('txtOrderDate').focus();
	                   return(false);  
                    }
                     }             
           
           
                  if(document.getElementById('txtCanDate').value != '')
                {
                if (isDate(document.getElementById('txtCanDate').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('lblError').innerText = "Cancellation Date is not valid.";			
	               document.getElementById('txtCanDate').focus();
	               return(false);  
                }
                 }  
                     
                            if(document.getElementById('drpIspOrderStatus').value=='2')
                             {    
                       if(document.getElementById('txtCanDate').value =='')
                                {
                                document.getElementById('lblError').innerHTML='Cancellation date is mandatory.';
                                 document.getElementById('txtCanDate').focus();
                                return false;
                                 }    
                                         if(document.getElementById('txtCanReason').value =='')
                                        {
                                        document.getElementById('lblError').innerHTML='Cancellation Reason is mandatory.';
                                         document.getElementById('txtCanReason').focus();
                                        return false;
                                         }  
                             }  
                             
                         if (document.getElementById("txtCanReason").value.trim().length>300)
                    {
                         document.getElementById("lblError").innerHTML="Cancellation Reason  cann't be more than 300 characters."
                         document.getElementById("txtCanReason").focus();
                         return false;
                    }         
            }
       
          function PopupAgencyPageIspPendingCancellation()
        {
                  var type;
                    type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	                window.open(type,"ISPPenCan","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                    return false;
                                     
        }
          
            function PopupEmployeeForISPCan()
        {
                   var type;      
                   var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                   if (strEmployeePageName!="")
                   {
                        type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
                    // type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	                 window.open(type,"ISPPenCan","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                     return false;
                     
                   }
        }

     
     /* end of  Code for Isp Pending Cancellation */
     
     
     /*  cOURSE sESSION*/ 
     
      function ValidateFormCourseSession()
    {
      document.getElementById('lblError').innerText=''
     
        //      Checking txtOpenDateFrom .
        if(document.getElementById('txtStartDateFrom').value != '')
        {
        if (isDate(document.getElementById('txtStartDateFrom').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Date from is not valid.";			
	       document.getElementById('txtStartDateFrom').focus();
	       return(false);  
        }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('txtStartDateTo').value != '')
        {
        if (isDate(document.getElementById('txtStartDateTo').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "Date to is not valid.";			
	       document.getElementById('txtStartDateTo').focus();
	       return(false);  
        }
        } 
        /*
         //      Checking txtCloseDateFrom .
        if(document.getElementById('txtEndDateFrom').value != '')
        {
        if (isDate(document.getElementById('txtEndDateFrom').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "End date from is not valid.";			
	       document.getElementById('txtEndDateFrom').focus();
	       return(false);  
        }
        } 
         //      Checking txtCloseDateTo .
        if(document.getElementById('txtEndDateTo').value != '')
        {
        if (isDate(document.getElementById('txtEndDateTo').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "End date to is not valid.";			
	       document.getElementById('txtEndDateTo').focus();
	       return(false);  
        }
        } */
        
   if (compareDates(document.getElementById('txtStartDateFrom').value,"d/M/yyyy",document.getElementById('txtStartDateTo').value,"d/M/yyyy")==1)
       { 
            document.getElementById('lblError').innerText ='Date to should be greater than or equal to date from.'
            return false;
       }
       /*
        if (compareDates(document.getElementById('txtEndDateFrom').value,"d/M/yyyy",document.getElementById('txtEndDateTo').value,"d/M/yyyy")==1)
       {
            document.getElementById('lblError').innerText ='End date to should be greater than or equal to End date from.'
            return false;
       }
       */
       return true; 
        
    }
    
 function SelectFunctionCourseSession(strID,course,trainingRoom,startDate,maxParticipant,TRAINER1,CourseLevel,CourseSessionID)
        {           
        
         if (window.opener.document.forms['form1']['hdCourseSessionFeedBack']!=null)
        { 
        window.opener.document.forms['form1']['hdCourseSessionFeedBack'].value=CourseSessionID
        window.opener.document.forms['form1']['txtCourseTitleFeedBack'].value=course
        window.opener.document.forms['form1']['txtTrainingRoomFeedBack'].value=trainingRoom
        window.opener.document.forms['form1']['txtStartDateFeedBack'].value=startDate
        window.opener.document.forms['form1']['txtMaxNoParticipantFeedBack'].value=maxParticipant
        window.opener.document.forms['form1']['txtNMCTrainersFeedBack'].value=TRAINER1
        window.opener.document.forms['form1']['txtCourseLevelFeedBack'].value=CourseLevel
        
        window.opener.document.forms['form1']['txtCourse'].selectedIndex=0;
        window.opener.document.forms['form1']['txtCourse'].disabled=true;
        window.opener.document.forms['form1']['txtCourse'].className="textboxgrey";
        window.close();
        return false;
            
        }
        
              
        }
        
        function PopupPageTrainerForTraining(id,ctrlid)
        {
             if (id=="1")
             {
                
                var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                if (strEmployeePageName!="")
                {
                    type = "../Setup/" + strEmployeePageName+ "?Popup=T&Dept=Training&ctrlId="+ctrlid;
                 
                   //type = "../Setup/MSSR_Employee.aspx?Popup=T&Dept=Training&ctrlId="+ctrlid;
   	                window.open(type,"aaTrainerForTrainingSearchEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	
   	                return false;
   	            }
             }
        }
        
     function PopupPageCourseSession(id)
         {
         var type;
         if (id=="1")
         {
              type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aaTrainingAgency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	    
          }
         if (id=="2")
         {
          var strAgencyName=document.getElementById("txtAgency").value;
               type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName ;
   	            window.open(type,"aaTrainingStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	 
         }
    
         if (id=="3")
         {
               
               type = "../Training/TRSR_TrainingRooms.aspx?Popup=T";
   	            window.open(type,"aaTrainingRoom","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
          }
          
           if (id=="4")
         {
               
               type = "../Training/TRSR_ParticipantBasket.aspx?Popup=T";
   	            window.open(type,"aaParticipantBasket","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
          }
           if (id=="5")
         {
             
               type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	            window.open(type,"aaCourseSessionSearchEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	
   	            return false;
         }
     }
    
    /*Update Course Seesion*/
    
function FillLevelManageCourseSession()
{
try
{
document.getElementById("txtCourseLevel").value=document.getElementById("ddlCourseTitle").value.split("|")[0]
document.getElementById("hdDuration").value=document.getElementById("ddlCourseTitle").value.split("|")[2]
document.getElementById("txtDuration").value=document.getElementById("ddlCourseTitle").value.split("|")[2]
document.getElementById("hdNoOfTest").value=document.getElementById("ddlCourseTitle").value.split("|")[3]


}
catch(err)
{
document.getElementById("txtCourseLevel").value=""
document.getElementById("hdDuration").value="0"
document.getElementById("txtDuration").value=""
document.getElementById("hdNoOfTest").value="0"
}
}

function ColorMethodManageCourseSession(id,total)
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
       
       
 
       if (id == (ctextFront +  "00" + ctextBack))
       {   
       
       document.getElementById("hdTabType").value="0";
     
       
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
       
     
             if (document.getElementById("hdPageCourseSessionID").value != "")
             {
            window.location.href="TRUP_Register.aspx?Action=U&CourseSessionID=" + document.getElementById("hdEnPageCourseSessionID").value + "&Duration=" + document.getElementById("txtDuration").value + "&NoOfTest=" + document.getElementById("hdNoOfTest").value ;
            }
           return false;         
       }
       
       else if (id == (ctextFront +  "02" + ctextBack))
       {   
       
       document.getElementById("hdTabType").value="2";
       
            window.location.href="MSUP_ActiveDayWiseTest.aspx?Action=U&CourseSessionID="+document.getElementById("hdEnPageCourseSessionID").value + "&Duration=" + document.getElementById("txtDuration").value + "&NoOfTest=" + document.getElementById("hdNoOfTest").value ;
       
           return false;
       }   
}


function PopupPageManageCourseSession(id)
         {
         var type;
       
          if (id=="1")
         {
             type = "TRSR_TrainingRooms.aspx?Popup=T" ;
   	         window.open(type,"aaCourseSessionTrainingRooms","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }
        
        if (id=="2")
         {
           
               type = "TRSR_Course.aspx?Popup=T";
   	            window.open(type,"aaCourseSessionSearchCourse","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
         }
         
          if (id=="3")
         {
             
               type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	            window.open(type,"aaCourseSessionEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	
   	            return false;
         }
        
        if (id=="4")
         {
           
                
               type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T" ;
   	            window.open(type,"aaCourseSessionStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
   	              return false;
         }
         
         
          if (id=="5")
         {
             type = "TRSR_ParticipantBasket.aspx?Popup=T" ;
   	         window.open(type,"aaCourseSessionTrainingRooms","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	        return false;
         }
        
     }
     

   function HideShowManageCourseSession()
    {
   
        if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
        {
             document.getElementById("theTabStrip_ctl00_Button1").className="headingtab";
        }
        if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
        {
           document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive";
        }
    }
    
    
    //******************************* Start JavaScript Code For Training - TRUP_Register.aspx ********************
    function validateResultRegister(id,id1)
    {

////////////


    var strPagePracTotalMarks=document.getElementById('hdTotalPracticalMarks').value;
    

      for(intcnt=1;intcnt<=document.getElementById('gvRegisterTab').rows.length-1;intcnt++)
    {    
                 if (document.getElementById('gvRegisterTab').rows[intcnt].cells[5].children.length == "1")
                {    
                        if (document.getElementById('gvRegisterTab').rows[intcnt].cells[5].children[0].type=="text")
                            { 
                                    if (document.getElementById('gvRegisterTab').rows[intcnt].cells[6].children[0].id==id)
                                    {
                                            var strStatus = document.getElementById('gvRegisterTab').rows[intcnt].cells[4].children[0].id;
                                            var item=document.getElementById(strStatus).selectedIndex;
                                            var text=document.getElementById(strStatus).options[item].text;
                                            var strValue1 = document.getElementById('gvRegisterTab').rows[intcnt].cells[6].children[0].value.trim();
                                            text=text.toUpperCase();
                                            if (text=="certified" || text=="not certified" || text=="CERTIFIED" || text=="NOT CERTIFIED" || text=="Certified" || text=="Not Certified")
                                            {
                                                event.returnValue = true;     
                                            }
                                            else
                                            {
                                                event.returnValue = false;     
                                                document.getElementById('gvRegisterTab').rows[intcnt].cells[6].children[0].value="";
                                            }
                                               
                                     }                                         
                            }
                }
  
   }
        if(document.getElementById(id).value =='')
            {
                
            }
        else
        {
            var strValue = document.getElementById(id).value
            var strTotalMarks=document.getElementById(id1).value;
                reg = new RegExp("^[0-9.]+$"); 
                if(reg.test(strValue) == false) 
                {
                    document.getElementById('lblError').innerText ='Only Number allowed.'
                     document.getElementById(id).focus();
                    return false;

                 }
                 else
                 {
                    document.getElementById('lblError').innerText ='';
                 }
                 
                 if (strTotalMarks.trim()!="")
                 {
                     if (parseFloat(strValue)>parseFloat(strTotalMarks))
                     {
                        document.getElementById('lblError').innerText ='P Marks can not be greater than Practical Marks';
                        return false;
                     
                     }
                     else
                     {
                        document.getElementById('lblError').innerText ='';
                     }
                 }
                 else
                 {
                    if (parseFloat(strValue)>parseFloat(strPagePracTotalMarks))
                     {
                        document.getElementById('lblError').innerText ='P Marks can not be greater than Practical Marks';
                        return false;
                     
                     }
                     else
                     {
                        document.getElementById('lblError').innerText ='';
                     }
                 
                 }
        } 
}

    
    function ColorMethodRegister(id,total)
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
      
  
       if (id == (ctextFront +  "00" + ctextBack))
       {   
       
       document.getElementById("hdTabType").value="0";
       
            window.location.href="TRUP_CourseSession.aspx?Action=U&CourseSessionID="+document.getElementById("hdEnPageCourseSessionID").value + "&Duration=" + document.getElementById("hdDuration").value + "&NoOfTest=" + document.getElementById("hdNoOfTest").value ;
       
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
        
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {   
       
       document.getElementById("hdTabType").value="2";
       
            window.location.href="MSUP_ActiveDayWiseTest.aspx?Action=U&CourseSessionID="+document.getElementById("hdEnPageCourseSessionID").value + "&Duration=" + document.getElementById("hdDuration").value + "&NoOfTest=" + document.getElementById("hdNoOfTest").value ;
       
           return false;
       } 
}

    function PopupPageRegister(id)
         {
         var type;
       
          if (id=="1")
         {
             type = "TRSR_TrainingRooms.aspx?Popup=T" ;
   	         window.open(type,"aaCourseSessionTrainingRooms","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	          return false;
         }
        
        if (id=="2")
         {
                
               type = "TRSR_Course.aspx?Popup=T";
   	            window.open(type,"aaCourseSessionSearchCourse","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	               return false;
         }
         
          if (id=="3")
         {
                if (document.getElementById("hdCheckQuestion").value=="True")
                {
                     var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                    if (strEmployeePageName!="")
                    {
                        type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
                        //type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	                    window.open(type,"aaCourseSessionEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	
   	                    return false;
   	                }
   	            }
   	            else
   	            {
   	                document.getElementById('lblError').innerText = "Question set not defined properly for this course please define set. "
   	                return false;
   	            }
         }
        
        if (id=="4")
         {
           
                if (document.getElementById("hdCheckQuestion").value=="True")
                {
                   type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T" ;
   	                window.open(type,"aaCourseSessionStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1,status=1");
   	                  return false;
   	            }
   	            else
   	            {
   	                document.getElementById('lblError').innerText = "Question set not defined properly for this course please define set. "
   	                return false;
   	            }
         }
         
         
          if (id=="5")
         {
            if (document.getElementById("hdCheckQuestion").value=="True")
            {
                 type = "TRSR_ParticipantBasket.aspx?Popup=T&PageFrom=Register&CourseId=" + document.getElementById("hdEnCourseID").value ;
   	             window.open(type,"aaCourseSessionTrainingRooms","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	            return false;
   	         }
   	            else
   	            {
   	                document.getElementById('lblError').innerText = "Question set not defined properly for this course please define set. "
   	                return false;
   	            }
         }
        
          if (id=="6")
         {
             type = "TRUP_AllLetterOperation.aspx?Popup=T&Operation=Email&CourseSessionId=" + document.getElementById("hdEnPageCourseSessionID").value + "&Aoffice=" + document.getElementById("hdEnPageAoffice").value ;
   	         window.open(type,"aaCourseSessionRegisterEmail","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	        return false;
         }
        
        if (id=="7")
         {
             type = "TRUP_AllLetterOperation.aspx?Popup=T&Operation=Print&CourseSessionId=" + document.getElementById("hdEnPageCourseSessionID").value + "&Aoffice=" + document.getElementById("hdEnPageAoffice").value ;
   	         window.open(type,"aaCourseSessionRegisterPrint","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	        return false;
         }
     }


function HideShowRegister()
    {
    /*var strTabtype=document.getElementById("hdTabType").value;
    switch(strTabtype)
    {
    case "0":
            document.getElementById("pnlSession").style.display="block";
            document.getElementById("pnlRegister").style.display="none";
             document.getElementById("btnEmployee").className =" button topMargin displayNone";
            document.getElementById("btnPeople").className =" button topMargin displayNone";
             document.getElementById("btnBasket").className =" button topMargin displayNone";
            break;
    case "1":
            document.getElementById("pnlSession").style.display="none";
            document.getElementById("pnlRegister").style.display="block";
            document.getElementById("btnEmployee").className ="button topMargin";
            document.getElementById("btnPeople").className ="button topMargin";
             document.getElementById("btnBasket").className ="button topMargin";
            break;
    
         
    }
    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
     switch(strTabtype)
    {
    case "0":
           document.getElementById("theTabStrip_ctl00_Button1").className="headingtab";
             break;
    case "1":
           document.getElementById("theTabStrip_ctl01_Button1").className="headingtab";
             break;
      
    }*/
       
    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
    {
         document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive";
    }
    if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
    {
       document.getElementById("theTabStrip_ctl01_Button1").className="headingtab";
    }
    
    
    }
    
    //******************************* End JavaScript Code For Training - TRUP_Register.aspx **********************
   
   //******************************* Start JavaScript Code For Training - MSUP_ActiveDayWiseTest.aspx*************
   function ColorMethodDayWiseTest(id,total)
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
       
       
 
       if (id == (ctextFront +  "00" + ctextBack))
       {   
       
       document.getElementById("hdTabType").value="0";
       
            window.location.href="TRUP_CourseSession.aspx?Action=U&CourseSessionID="+document.getElementById("hdEnPageCourseSessionID").value + "&Duration=" + document.getElementById("hdDuration").value + "&NoOfTest=" + document.getElementById("hdNoOfTest").value ;
       
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            if (document.getElementById("hdPageCourseSessionID").value != "")
             {
            window.location.href="TRUP_Register.aspx?Action=U&CourseSessionID=" + document.getElementById("hdEnPageCourseSessionID").value + "&Duration=" + document.getElementById("hdDuration").value + "&NoOfTest=" + document.getElementById("hdNoOfTest").value ;
            }
           
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {   
     
       
           return false;
       }  
}   
   
function PopupPageDayWiseTest(id,P_ID,ResultDay)
         {
         var type;
       
          if (id=="1")
         {
             type = "PUSR_ParticipantResult.aspx?TR_COURSEP_ID="+ P_ID + "&DAYS=" + ResultDay ;
   	         window.open(type,"aaParticipantResult","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }      
          if (id=="2")
         {
             type = "TRUP_FeedBackDetails.aspx?TR_COURSEP_ID="+ P_ID;
   	         window.open(type,"aaParticipantFeedback","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }     
       return false;
     }
     

   function HideShowDayWiseTest()
    {
   
   if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
    {
         document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive";
    }
    
    if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
    {
       document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive";
    }
    
    if (document.getElementById('theTabStrip_ctl02_Button1').className != "displayNone")
    {
         document.getElementById("theTabStrip_ctl02_Button1").className="headingtab";
    }
      
    
    }
      
   //******************************* End JavaScript Code For Training - MSUP_ActiveDayWiseTest.aspx*************
   
   
   //******************************* Start JavaScript Code For Training - TRSR_Letter.aspx**********************
   
   function TransferTextTrainingLetter()
{
    document.getElementById('hdRptLetter').value=NewsBody_rich.document.body.innerText;
    document.getElementById('hdnmsg').value=NewsBody_rich.document.body.innerHTML;
}


 function TextContentTrainingLetter()
    {   
            
            NewsBody_rich.document.body.innerHTML=document.getElementById("txtLetter").value;
            document.getElementById('hdRptLetter').value=NewsBody_rich.document.body.innerText;
            document.getElementById('hdnmsg').value=NewsBody_rich.document.body.innerHTML;
			self.focus();
    }

         
         //code for closing and returning value
    function fnPTRIDTrainingLetter()
    {
        window.close();
        return false;
    }
    
    function ValidateFormTrainingLetter()
    {

 document.getElementById('hdRptLetter').value=NewsBody_rich.document.body.innerText;
       document.getElementById('hdnmsg').value=NewsBody_rich.document.body.innerHTML;
      document.getElementById('lblError').innerText=''
              if(document.getElementById('txtEmail').value == '')
             {
             document.getElementById('lblError').innerText = "Email is mandatory.";
             document.getElementById('txtEmail').focus();
             return false;
             }
            else
            {
                var strEmailText=document.getElementById("txtEmail").value;
                var arstrEmailText=strEmailText.split(",")
                for(i=0;i<arstrEmailText.length;i++)
                {
                    if(checkEmail(arstrEmailText[i])==false)
                     {
                        document.getElementById("lblError").innerHTML='Enter valid email Id.';
                        document.getElementById("txtEmail").focus();
                        return false;
                     }
                }
            }
              if(document.getElementById('txtLetter').value == '')
             {
             document.getElementById('lblError').innerText = "Letter text is mandatory.";
             document.getElementById('txtLetter').focus();
             return false;
             }
       
       return true; 
        
    }
    
     function ValidateSaveTrainingLetter()
    {
  
    document.getElementById('hdRptLetter').value=NewsBody_rich.document.body.innerText;
       document.getElementById('hdnmsg').value=NewsBody_rich.document.body.innerHTML;
      document.getElementById('lblError').innerText=''
              
              if(document.getElementById('txtLetter').value == '')
             {
             document.getElementById('lblError').innerText = "Letter text is mandatory.";
             document.getElementById('txtLetter').focus();
             return false;
             }
       
       return true; 
        
    }
    
   //******************************* End JavaScript Code For Training - TRSR_Letter.aspx************************
   
   //******************************* Start JavaScript Code For Training - TRUP_Course.aspx**********************
   
   function ColorMethodCourse(id,total)

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

Hcontrol = ctextFront + HFlush + ctextBack;

document.getElementById(Hcontrol).className="headingtabactive";

}


document.getElementById(id).className="headingtab"; 


document.getElementById('lblPanelClick').value =id; 




if (id == (ctextFront + "00" + ctextBack))

{ 
// setting value for tab
document.getElementById("hdTabType").value = "0"
// end
document.getElementById("pnlCall").style.display="block";

//document.getElementById("pnlDesc").style.display="none";



return false;

} 

}

function PopupPageCourse(id)

{

var type;



if (id=="1")

{

type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;

window.open(type,"aa","height=600,width=900,top=30,left=20,scrollbars=1"); 


}
return false;
}

function HideShowCourse()

{var strTabtype=document.getElementById("hdTabType").value;

switch(strTabtype)

{

case "0":

document.getElementById("pnlCall").style.display="block";
break;
}

switch(strTabtype)

{

case "0":

document.getElementById("theTabStrip_ctl00_Button1").className="headingtab";

break;
}

}
function EnableNoOfTest(id)
{
var TestRequired=document.getElementById(id).checked;
if( TestRequired==true)
{
    document.getElementById("txtNoOfTest").className="textbox";
    document.getElementById("txtNoOfTest").readOnly=false;
    document.getElementById("txtNoOfTest").value='';
    document.getElementById("txtNoOfTest").focus();
}
else
{
    document.getElementById("txtNoOfTest").className="textboxgrey";
    document.getElementById("txtNoOfTest").readOnly=true;
    document.getElementById("txtNoOfTest").value='';
    document.getElementById("txtCourseDescription").focus();
    
}
for (i=1;i<=9;i++)
    {
       document.getElementById("trDay" + i).style.display="none";
    }
}

function HideShowDay(id)
{
var days=document.getElementById(id).value;
if (days==0)
{
document.getElementById(id).value='';
document.getElementById(id).focus();
}
var CeilDays=Math.ceil(days);
if (CeilDays>9)
{
document.getElementById("lblError").innerText="No of Test Exceeds Maximum no of Test";
document.getElementById(id).value='';
document.getElementById(id).focus();
return false;
}
    for (i=1;i<=9;i++)
    {
        if (i<=CeilDays)
        {
            document.getElementById("trDay" + i).style.display="block";   
        }
        else
        {
            document.getElementById("trDay" + i).style.display="none";
        }
    }
    try
    {
    document.getElementById("trDay1").focus();
    }
    catch(err)
    {
    }
}


 function SelectAllCourse() 
    {
   
       CheckAllDataGridCheckBoxesCourse(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxesCourse(value) 
    {
    
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
           
               if (elm.name !='chkInternalCourse' && elm.name != 'chlShowOnWeb' && elm.name != 'chkRequired')
                {
                  elm.checked = value
                }
            }
        }
    }
    
    
    function ValidateFormCourse()
    {
      document.getElementById('lblError').innerText=''
     
       //*********** Validating Course title *****************************
   
   if(document.getElementById('txtCourse').value =='')
        {
            document.getElementById('lblError').innerText='Course title is mandatory.'
            document.getElementById('txtCourse').focus();
            return false;
        }
        
        //*********** Validating Level  *****************************
    var cboGroup=document.getElementById('ddlLevel');
   if(cboGroup.selectedIndex ==0)
        {
         document.getElementById('lblError').innerText ='Level is mandatory.'
         cboGroup.focus();
         return false;
            
        }
        
      
        
         //*********** Validating Duration  *****************************
   
   if(document.getElementById('txtDuration').value =='')
        {
            document.getElementById('lblError').innerText='Duration is mandatory.'
            document.getElementById('txtDuration').focus();
            return false;
        }
        else
        {
          if(IsDataValid(document.getElementById("txtDuration").value,4)==false)
          {
            document.getElementById('lblError').innerText='Only numbers allowed.'
            document.getElementById('txtDuration').focus();
            return false;
          }
       var strValue = document.getElementById('txtDuration').value
       try
       {    
            var digits="1234567890";
            var cn=0;
	        for (var i=0; i < strValue.length; i++) 
	        {
		    if (digits.indexOf(strValue.charAt(i))==-1)
		     {  }
		     else
		     {cn=1;		     }
		 
		    }
		    
            if (cn==0)
            {
                document.getElementById('lblError').innerText='Duration Should be greater than Zero.'
                document.getElementById('txtDuration').focus();
                return false;
            }
           if (parseFloat(strValue)<=0)
           {
                document.getElementById('lblError').innerText='Duration Should be greater than Zero.'
                document.getElementById('txtDuration').focus();
                return false;
           }
       }
      catch(err)
       {
                 document.getElementById('lblError').innerText='Duration Should be greater than Zero.'
                document.getElementById('txtDuration').focus();
                return false;
       }
         /*    reg = new RegExp("^[0-9]+$"); 
            if(reg.test(strValue) == false) 
            {
                document.getElementById('lblError').innerText ='Invalid Duration.'
                 document.getElementById('txtDuration').focus();
                return false;

             }
             if (strValue>9)
             {
                document.getElementById('lblError').innerText ='Duration can not be greater than 9';
                 document.getElementById('txtDuration').focus();
                return false;
             
             }*/
        
        }
  
       /*if(document.getElementById('txtPractMarks').value !='')
         {
            var strValue = document.getElementById('txtPractMarks').value
            reg = new RegExp("^[0-9]+$"); 
            if(reg.test(strValue) == false) 
            {
                document.getElementById('lblError').innerText ='Practical Marks should contain only digits.'
                return false;

             }
        }*/
        
        // Code For Checking Test Wise Total Marks.
        
         if(document.getElementById('txtPractMarks').value =='')
        {    }
        else
        {
          if(IsDataValid(document.getElementById("txtPractMarks").value,4)==false)
          {
            document.getElementById('lblError').innerText='Only numbers allowed.'
            document.getElementById('txtPractMarks').focus();
            return false;
          }
        }
        
        var TestRequired=document.getElementById("chkRequired").checked;
        if( TestRequired==true)
        {
            var strNoOfTest = document.getElementById('txtNoOfTest').value
            if (strNoOfTest=='' || strNoOfTest=='0')
            {
             document.getElementById('lblError').innerText ='No Of Test must be greater than Zero'
             document.getElementById('txtNoOfTest').focus();
                    return false;
            }
            for (i=1;i<=strNoOfTest;i++)
            {
                if (document.getElementById("txtDay" + i).value=='')
                {
                  document.getElementById('lblError').innerText ='Total Marks Test ' + i + ' is mandatory';
                  document.getElementById("txtDay" + i).focus();
                  return false;
                }
            }   
        }
        else
        {
            document.getElementById('txtNoOfTest').value='';
        }
       
        // End Code For Checking Test Wise Total Marks.
        
		}		


   //******************************* End JavaScript Code For Training - TRUP_Course.aspx************************
   
   
   //******************************* Start JavaScript Code For Productivity - PRDSR_DailyBookings.aspx ************************
   //Added by Neeraj Nath
   function EnableDisableGroupProductivity()
    {
            //DailyBookings
            if ( document.forms['form1']['chkGroupProductivity']!=null) 
                {
                   if ( (document.getElementById("txtLcode").value.trim()!='') || (document.getElementById("txtChainCode").value.trim()!='') || (document.getElementById("hdAgencyName").value.trim()!=''))
                    {
                        //document.getElementById("chkGroupProductivity").checked=false;
                        document.getElementById("chkGroupProductivity").disabled =false;                        
                    }
                   else
                   {        
                        document.getElementById("chkGroupProductivity").checked=false;
                        document.getElementById("chkGroupProductivity").disabled =true;                        
                   }    
                }   
               
             //BIDT
             
            if ( document.forms['form1']['ChkGrpProductivity']!=null)
                {
                                        
                   if ( (document.getElementById("txtLcode").value.trim()!='') || (document.getElementById("txtChainCode").value.trim()!='') || (document.getElementById("hdAgencyName").value.trim()!=''))
                    {
                        //document.getElementById("ChkGrpProductivity").checked=false;
                        document.getElementById("ChkGrpProductivity").disabled =false;                        
                    }
                   else
                   {  
                        document.getElementById("ChkGrpProductivity").checked=false; 
                        document.getElementById("ChkGrpProductivity").disabled =true;
                   }                                      
                        
                }    
                             
            //MIDT && Travel Assistance Bkng
            if ( document.forms['form1']['chbWholeGroup']!=null)
                {
                   if ( (document.getElementById("txtLcode").value.trim()!='') || (document.getElementById("txtChainCode").value.trim()!='') || (document.getElementById("hdAgencyName").value.trim()!=''))
                    {
                       // document.getElementById("chbWholeGroup").checked=false;
                        document.getElementById("chbWholeGroup").disabled =false;                        
                    }
                   else
                   {   
                        document.getElementById("chbWholeGroup").checked=false;     
                        document.getElementById("chbWholeGroup").disabled =true;
                   }   
                 }                   
               
               
        return false;
    }
    
 
  
     
//for others (BIDT/MIDT)
    function ActDecLcodeChainCode()    
    {   
        if (document.getElementById("hdAgencyName").value.trim()=='')
            {
            document.getElementById("txtChainCode").disabled=false;
            document.getElementById("txtLcode").disabled=false;

            document.getElementById("txtChainCode").className="textbox";
            document.getElementById("txtLcode").className="textbox";
                if ((document.getElementById("txtLcode").value.trim()!='') || (document.getElementById("txtChainCode").value.trim()!=''))
                {
                    if ( document.forms['form1']['ChkGrpProductivity']!=null)
                        {
                         document.getElementById("ChkGrpProductivity").disabled =false;
                        }
                       if ( document.forms['form1']['chbWholeGroup']!=null) //This is for MIDT && Travel Assistance Bkng
                        {
                         document.getElementById("chbWholeGroup").disabled =false;
                        }                           
                       
                }
            }
         else
            {
            document.getElementById("txtChainCode").disabled=true;
            document.getElementById("txtLcode").disabled=true;

            document.getElementById("txtChainCode").className="textboxgrey";
            document.getElementById("txtLcode").className="textboxgrey";
            }   
        return false;
    }    
    
    
      //for DailyBookings
    function EnableDisableLcodeChainCode()    
    {    
    if (document.getElementById("hdAgencyName").value.trim()=='')
            {
            document.getElementById("txtChainCode").disabled=false;
            document.getElementById("txtLcode").disabled=false;

            document.getElementById("txtChainCode").className="textbox";
            document.getElementById("txtLcode").className="textbox";
                if ((document.getElementById("txtLcode").value.trim()!='') || (document.getElementById("txtChainCode").value.trim()!=''))
                {
                    document.getElementById("chkGroupProductivity").disabled =false;
                }
            }
         else
            {
            document.getElementById("txtChainCode").disabled=true;
            document.getElementById("txtLcode").disabled=true;

            document.getElementById("txtChainCode").className="textboxgrey";
            document.getElementById("txtLcode").className="textboxgrey";
            }
        return false;
       }       
    //for Group Productivity
    
    function EnableDisableChainCode()    
    {    
        if (document.getElementById("hdAgencyName").value.trim()=='')
            {
            document.getElementById("txtChaincode").disabled=false;
            document.getElementById("txtChaincode").className="textbox";

            }
         else
            {
            document.getElementById("txtChaincode").disabled=true;
            document.getElementById("txtChaincode").className="textboxgrey";
            }
        return false;
    }       
       
     function EnableDisableBIDTGroupProductivity()
    {
    
          if ( (document.getElementById("txtLcode").value.trim()!='') || (document.getElementById("txtChainCode").value.trim()!='') ||   ( document.getElementById("hdAgencyName").value.trim() !='') )
            {            
                //document.getElementById("ChkGrpProductivity").checked=false;
                document.getElementById("ChkGrpProductivity").disabled =false;
            }
           else
           {        
            document.getElementById("ChkGrpProductivity").checked=false; 
            document.getElementById("ChkGrpProductivity").disabled =true;       
           }        
        return false;
    }            
  
   //Added by Neeraj Nath--Ends here
   
   
   
    function gotopPerDAILYB()
    {
         if (event.keyCode==46 )
         {
            document.getElementById('drpPerformence').selectedIndex=0;
         }
     validatePerformencFromToDAILYB();
    }
    
    
    function gotopProdDAILYB()
    {
         if (event.keyCode==46 )
         {
            document.getElementById('drpProductivity').selectedIndex=0;
         }
     validateProductivityFromToDAILYB();
     
    }
    
    
    function showDetailsDAILYB(lcode)
{
try
{
        if(document.getElementById("chkAir")!=null)
        {
                        var stAir=document.getElementById("chkAir").checked;
        }
        if(document.getElementById("chkCar")!=null)
        {
                        var stCar=document.getElementById("chkCar").checked;
        }
        if(document.getElementById("chkHotel")!=null)
        {
                        var stHotel=document.getElementById("chkHotel").checked;

        }
        if(document.getElementById("chkAirBreakUp")!=null)
        {
                        var stAirBreak=document.getElementById("chkAirBreakUp").checked;
        }
            
            if(document.getElementById("chkOriginalBk")!=null)
            {
                var orgnlBooking=document.getElementById("chkOriginalBk").checked;
            } 
               
                var dtMonth=document.getElementById("drpMonth").selectedIndex;
                var dtYear=document.getElementById("drpYear").options[document.getElementById("drpYear").selectedIndex].value;
            
    var type;
    type = "../Popup/PUSR_DailyBooking.aspx?Lcode="+lcode +"&Air="+stAir+"&AirBreak="+stAirBreak+"&Car="+stCar+"&Hotel="+stHotel+"&Months="+dtMonth+"&Years="+dtYear+"&OriginalBookings="+orgnlBooking;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=yes");	
    return false;
  }
  catch(err)
  {
  
  }
  
}

    
    function LcodeResetDAILYB()
    {
    
    document.getElementById("hdAgencyNameId").value=''
    document.getElementById("hdChkGroupProductivity").value='1'
    }
    
    
    function GroupProductivityChkDAILYB()
    {
        //Commentd by Neeraj Nath on 21/Mar/2011            
        if(document.getElementById("hdAgencyName").value.trim()=='')
        {
            //document.getElementById("chkGroupProductivity").checked=false;
            //document.getElementById("chkGroupProductivity").disabled =true;

        }
        else
        {   
            if(document.getElementById("txtAgencyName").value.trim()==document.getElementById("hdAgencyName").value.trim())
            {            
            document.getElementById("chkGroupProductivity").disabled =false;
            }
            else
            {
            //document.getElementById("chkGroupProductivity").checked=false;
            //document.getElementById("chkGroupProductivity").disabled =true;
            document.getElementById("hdAgencyNameId").value='';
            }
        }    
        
        EnableDisableLcodeChainCode();
        validateProductivityFromToDAILYB(); 
        validatePerformencFromToDAILYB();
    }
    
    function ValidateSearchDAILYB()
    {
    
    
                document.getElementById("hdButtonClick").value="1";
                
                             
             var stAirBreak=document.getElementById("chkAirBreakUp").checked;
            if(stAirBreak==true)
            {
                if((document.getElementById("txtAgencyName").value.trim()=='')&&(document.getElementById("drpAirLineName").selectedIndex=='0')&&(document.getElementById("drpCitys").selectedIndex=='0'))
                {
                //document.getElementById("lblError").innerHTML="Please Select Agency,City or Airline Name Data will be send soon"
               // document.getElementById("lblError").innerHTML="Data will be send soon"
                
               // return false;
                }
            
            } 
            
              //Code for 
               if(document.getElementById("txtProductivityFrm").className=='textbox')
        {
            if(document.getElementById("txtProductivityFrm").value.trim()=='')
            {
            document.getElementById("txtProductivityFrm").focus();
            document.getElementById("lblError").innerHTML="Productivity From cann't be blank";
            return false;
            }
        }
        
          if(document.getElementById("txtProductivityTo").className=='textbox')
        {
            if(document.getElementById("txtProductivityTo").value.trim()=='')
            {
            document.getElementById("txtProductivityTo").focus();
            document.getElementById("lblError").innerHTML="Productivity To cann't be blank";
            return false;
            }
        }
      
       if(document.getElementById("txtPerformenceFrm").className=='textbox')
        {
            if(document.getElementById("txtPerformenceFrm").value.trim()=='')
            {
            document.getElementById("txtPerformenceFrm").focus();
            document.getElementById("lblError").innerHTML="Performance From cann't be blank";
            return false;
            }
        }  
        
       if(document.getElementById("txtPerformenceTo").className=='textbox')
        {
            if(document.getElementById("txtPerformenceTo").value.trim()=='')
            {
            document.getElementById("txtPerformenceTo").focus();
            document.getElementById("lblError").innerHTML="Performance To cann't be blank";
            return false;
            }
        }
        
       
        //Added by Neeraj Nath
         var values=document.getElementById("txtLcode").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Invalid Lcode.";
                    document.getElementById("txtLcode").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }

        var values=document.getElementById("txtChainCode").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Invalid Chain Code.";
                    document.getElementById("txtChainCode").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }

        //Added by Neeraj Nath
        
       
         var values=document.getElementById("txtProductivityFrm").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Productivity From is Numeric";
                    document.getElementById("txtProductivityFrm").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }

 values=document.getElementById("txtProductivityTo").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Productivity To is Numeric";
                    document.getElementById("txtProductivityTo").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }
        
         values=document.getElementById("txtPerformenceFrm").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Performance From is Numeric";
                    document.getElementById("txtPerformenceFrm").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }
        
        
         values=document.getElementById("txtPerformenceTo").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Performance To is Numeric";
                    document.getElementById("txtPerformenceTo").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }    
    }
    
    
    
    function validateAirBreakUpDAILYB()
    {
    
    if(document.getElementById("chkAir")!=null)
    {
                var stAir=document.getElementById("chkAir").checked;
    }
    
    if(document.getElementById("chkCar")!=null)
    {
                var stCar=document.getElementById("chkCar").checked;
    }
    
    if(document.getElementById("chkHotel")!=null)
    {
                var stHotel=document.getElementById("chkHotel").checked;
    
    }
    
    if(document.getElementById("chkAirBreakUp")!=null)
    {
                var stAirBreak=document.getElementById("chkAirBreakUp").checked;
    }
          
            if(stAirBreak==true)
            {
            
            document.getElementById("chkCar").checked=false;
            document.getElementById("chkHotel").checked=false;
            
                 document.getElementById("dv1").style.display='block';
                 document.getElementById("dv2").style.display='block';
            
           
            //return false;
            } 
            else
            {
                 document.getElementById("dv1").style.display='none';
                 document.getElementById("dv2").style.display='none';
            }
           
      }
   
   
   function ValidateCarHotelDAILYB() 
   {
   
                if(document.getElementById("chkAir")!=null)
    {
                var stAir=document.getElementById("chkAir").checked;
    }
    
    if(document.getElementById("chkCar")!=null)
    {
                var stCar=document.getElementById("chkCar").checked;
    }
    
    if(document.getElementById("chkHotel")!=null)
    {
                var stHotel=document.getElementById("chkHotel").checked;
    
    }
    
    if(document.getElementById("chkAirBreakUp")!=null)
    {
                var stAirBreak=document.getElementById("chkAirBreakUp").checked;
    }
                
                if(stCar==true || stHotel==true)
                {
                document.getElementById("chkAirBreakUp").checked=false;
                 document.getElementById("dv1").style.display='none';
            document.getElementById("dv2").style.display='none';
               // return false;
                } 
               
   }
   
 function PopupAgencyPageDAILYB()
 {
 
    var type;
    type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
    window.document.getElementById("txtAgencyName").focus();
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
   	return false;
 }

function validateProductivityToDAILYB()
{
 
var values=document.getElementById("txtProductivityFrm").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Productivity From is Numeric";
                    document.getElementById("txtProductivityFrm").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }

 values=document.getElementById("txtProductivityTo").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Productivity To is Numeric";
                    document.getElementById("txtProductivityTo").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }
        
         values=document.getElementById("txtPerformenceFrm").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Performance From is Numeric";
                    document.getElementById("txtPerformenceFrm").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }
        
        
         values=document.getElementById("txtPerformenceTo").value;
        if(values.trim()!='')
        {
                if(IsDataValid(values,12)=='0')
                {
                    document.getElementById("lblError").innerHTML="Performance To is Numeric";
                    document.getElementById("txtPerformenceTo").focus();
                    return false;
                }
                else
                {
                    document.getElementById("lblError").innerHTML="";
                }
        }
}



function validatePerformencFromToDAILYB()
{
        if(document.getElementById("drpPerformence").selectedIndex=='0')
        {
            document.getElementById("txtPerformenceTo").disabled=true;
            document.getElementById("txtPerformenceFrm").disabled=true;
             document.getElementById("txtPerformenceTo").className='textboxgrey'
            document.getElementById("txtPerformenceFrm").className='textboxgrey'
             document.getElementById("txtPerformenceTo").value=''
            document.getElementById("txtPerformenceFrm").value=''
        }
        else
        {
            document.getElementById("txtPerformenceTo").disabled=false;
            document.getElementById("txtPerformenceFrm").disabled=false;
             document.getElementById("txtPerformenceTo").className='textbox'
            document.getElementById("txtPerformenceFrm").className='textbox'
           
            
        }
        
        
        if(document.getElementById("drpPerformence").selectedIndex!='7')
        {
         document.getElementById("txtPerformenceTo").disabled=true;
         document.getElementById("txtPerformenceTo").className='textboxgrey'
         document.getElementById("txtPerformenceTo").value='';
        }
        else
        {
            document.getElementById("txtPerformenceTo").disabled=false;
            document.getElementById("txtPerformenceTo").className='textbox'
            
        }
}


function validateProductivityFromToDAILYB()
{

        if(document.getElementById("drpProductivity").selectedIndex=='0')
        {
            document.getElementById("txtProductivityTo").disabled=true;
            document.getElementById("txtProductivityFrm").disabled=true;
             document.getElementById("txtProductivityTo").className='textboxgrey'
            document.getElementById("txtProductivityFrm").className='textboxgrey'
            
             document.getElementById("txtProductivityTo").value=''
            document.getElementById("txtProductivityFrm").value=''
            
        }
        else
        {
            document.getElementById("txtProductivityTo").disabled=false;
            document.getElementById("txtProductivityFrm").disabled=false;
           document.getElementById("txtProductivityTo").className='textbox'
            document.getElementById("txtProductivityFrm").className='textbox'
        }
        
        
        if(document.getElementById("drpProductivity").selectedIndex!='7')
        {
         document.getElementById("txtProductivityTo").disabled=true;
         document.getElementById("txtProductivityTo").className='textboxgrey'
         document.getElementById("txtProductivityTo").value='';
        }
        else
        {
            document.getElementById("txtProductivityTo").disabled=false;
         document.getElementById("txtProductivityTo").className='textbox'
        }
        
}


function ResetDailBookingDAILYB()
{
 
document.getElementById("drpCountrys").selectedIndex=0; 
document.getElementById("drpCitys").selectedIndex=0; 
document.getElementById("drpAgencyStatus").selectedIndex=0; 
document.getElementById("drpResponsibleStaff").selectedIndex=0; 
document.getElementById("drpAgencyType").selectedIndex=0; 
document.getElementById("ddlOnlineStatus").selectedIndex=0; 
document.getElementById("drpOneAOffice").selectedIndex=0; 
document.getElementById("drpProductivity").selectedIndex=0; 
document.getElementById("drpPerformence").selectedIndex=0; 
document.getElementById("drpRegion").selectedIndex=0; 
document.getElementById("txtAgencyName").value='';
document.getElementById("txtProductivityFrm").value='';
document.getElementById("txtProductivityTo").value='';
document.getElementById("txtPerformenceFrm").value='';
document.getElementById("txtPerformenceTo").value='';
document.getElementById("chkAir").checked=false;
document.getElementById("chkCar").checked=false;
document.getElementById("chkHotel").checked=false;
document.getElementById("chkAirBreakUp").checked=false;
document.getElementById("lblTotRec").style.display='none'
document.getElementById("txtRecordCount").style.display='none'
document.getElementById("lblError").innerHTML=""

   
try
    {
document.getElementById("grdvDailyBookingsAll").style.display='none'
    }
catch(err)
    {

    }
    
    try
    {
document.getElementById("grdvAirWithAirBr").style.display='none'
    }
catch(err)
    { }
    
    try
    {
document.getElementById("grdvCar").style.display='none'
    }
catch(err)
    {  }
    
    try
    {
document.getElementById("grdvHotel").style.display='none'
    }
catch(err)
    {   }
    
      
    
    try
    {
document.getElementById("grdvNoChk").style.display='none'
    }
catch(err)
    {   }
    
    try
    {
document.getElementById("grdvCarHotel").style.display='none'
    }
catch(err)
    {  }
    
    
    try
    {
document.getElementById("grdvAirCar").style.display='none'
    }
catch(err)
    {    }
    
  
return false;
}

function CallPrintDAILYB( strid )
{

var prtContent = document.getElementById( strid );
prtContent.border = 0; //set no border here
var strOldOne=prtContent.innerHTML;
var WinPrint = window.open('','','letf=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
WinPrint.document.write(prtContent.outerHTML);
WinPrint.document.close();
WinPrint.focus();
WinPrint.print();
WinPrint.close();

}
   
   
   //******************************* End JavaScript Code For Productivity - PRDSR_DailyBookings.aspx ************************
   
   //******************************* Start JavaScript Code For HelpDesk - HDUP_HelpDeskFeedBack.aspx*******************
   
   
   function ColorMethodHelpDeskFeedBack(id,total,itemIndex)
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
       var EnStatus=document.getElementById('hdEnPageStatus').value;
       var FeedBackId=document.getElementById("hdEnFeedBackId").value;
       var HD_RE_ID =document.getElementById('hdEnPageHD_RE_ID').value;
       var strHD_RE_ID=document.getElementById('hdEnPageHD_RE_ID').value;
       var LCode=document.getElementById('hdEnPageLCode').value;
       var strStatus=document.getElementById('hdPageStatus').value;
       var AOFFICE=document.getElementById("hdEnAOffice").value;
       document.getElementById("hdTabType").value=itemIndex
   
       if (strStatus != "Technical")
       {
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action=U&TabType=0&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus + "&FeedBackId="+ FeedBackId;               
          return false;
           }
          return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action=U&TabType=1&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus + "&FeedBackId="+ FeedBackId;                
          return false;
           }
           
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
           
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action=U&TabType=2&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus + "&FeedBackId="+ FeedBackId;               
          return false;
           }
                    
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {
            
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action=U&TabType=3&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus + "&FeedBackId="+ FeedBackId;                
          return false;
           }
          return false;
           
       }
       
        else if (id == (ctextFront +  "04" + ctextBack))
       {
            document.getElementById('hdTabType').value='4';
                  
            window.location.href="HDUP_LinkedLTR.aspx?Action=U&TabType=4&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus+ "&AOFFICE="+ AOFFICE + "&FeedBackId="+ FeedBackId;  
         return false;
           
       }
        else if (id == (ctextFront +  "05" + ctextBack))
       {
            
         return false;
           
       }
       }
       else
       {
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_CallLog.aspx?Action=U&TabType=0&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus + "&FeedBackId="+ FeedBackId;                 
          return false;
           }
          return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_CallLog.aspx?Action=U&TabType=1&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus+ "&FeedBackId="+ FeedBackId;               
          return false;
           }
           
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
           
            if(strHD_RE_ID!="")
           {
           window.location.href="TCUP_CallLog.aspx?Action=U&TabType=2&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus+ "&FeedBackId="+ FeedBackId;               
          return false;
           }
                    
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {
           document.getElementById('hdTabType').value='4';
            window.location.href="HDUP_LinkedLTR.aspx?Action=U&TabType=4&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus + "&AOFFICE="+ AOFFICE +  "&FeedBackId="+ FeedBackId;   
         return false;
           
           
           
       }
        else if (id == (ctextFront +  "04" + ctextBack))
       {
            
         return false;
           
       }
       
       }
}

   
   //******************************* End JavaScript Code For HelpDesk - HDUP_HelpDeskFeedBack.aspx*******************
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


/**********************************************javascript for birdresHelpdesk**********************************************/

//Start of New Added Code
  
  function fillBRBackUpTravelAgency()
         {
           var officeId;
           //officeId=  document.getElementById('txtOfficeId').value;
           officeId="1"
           CallServer(officeId,"This is context from client");
           return false;
           }
           
  function ReceiveBRServerDataTravelAgency(args, context)
        {
        
              AdvanceSearchTravelAgency();
//            var obj = new ActiveXObject("MsXml2.DOMDocument");
//            obj.loadXML(args);
//			  var dsRoot=obj.documentElement;
                 if (window.DOMParser)
                  {
                  parser=new DOMParser();
                  obj=parser.parseFromString(args,"text/xml");
                  }
                else // Internet Explorer
                  {
                  obj=new ActiveXObject("Microsoft.XMLDOM");
                  obj.async="false";
                  obj.loadXML(args);
                  } 
                  var dsRoot=obj.documentElement;
			
			var ddlOrders = document.getElementById('drpBackupOnlineStatus');
			
			
			
			//alert(h.value)
			for (var count = ddlOrders.options.length-1; count >-1; count--)
			{
				ddlOrders.options[count] = null;
				
				
			}
			
			var orders = dsRoot.getElementsByTagName('Status');
				var codes='';
			var names="-- All --";
			var text; 
			
			var listItem;
			listItem = new Option(names, codes,  false, false);
			ddlOrders.options[ddlOrders.length] = listItem;
			
			
			for (var count = 0; count < orders.length; count++)
			{
				codes= orders[count].getAttribute("StatusCode"); 
			    names=orders[count].getAttribute("StatusCode"); 
				listItem = new Option(names, codes,  false, false);
				ddlOrders.options[ddlOrders.length] = listItem;
				
			}
			
			// code for online status start
			var ddlOnlineStatus = document.getElementById('drpOnlineStatus');
			for (var count = ddlOnlineStatus.options.length-1; count >-1; count--)
			{
				ddlOnlineStatus.options[count] = null;
				
				
			}
			
			var orders = dsRoot.getElementsByTagName('Status');
				var codes='';
			var names="-- All --";
			var text; 
			
			var listItem;
			listItem = new Option(names, codes,  false, false);
			ddlOnlineStatus.options[ddlOnlineStatus.length] = listItem;
			
			
			for (var count = 0; count < orders.length; count++)
			{
				codes= orders[count].getAttribute("StatusCode"); 
			    names=orders[count].getAttribute("StatusCode"); 
				listItem = new Option(names, codes,  false, false);
				ddlOnlineStatus.options[ddlOnlineStatus.length] = listItem;
				
			}
			// code for online status end
			
        }
  
  function fillBRCategoryTechnical()
   {
      document.getElementById('ddlQueryCategory').options.length=0;
      document.getElementById('ddlQuerySubCategory').options.length=0;
      id=document.getElementById('ddlQuerySubGroup').value;
      //var obj = new ActiveXObject("MsXml2.DOMDocument");
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
                 var obj;
                 if (window.DOMParser)
                  {
                  parser=new DOMParser();
                  obj=parser.parseFromString(document.getElementById("hdCategory").value,"text/xml");
                  
                       var dsRoot=obj.documentElement; 
			            if (dsRoot !=null)
			            {   
			                 var item=document.getElementById("ddlQuerySubGroup").selectedIndex;
			                 var text1=document.getElementById("ddlQuerySubGroup").options[item].text;		
			                 //<HD_CC><CC CCI='' CCN='' CSGN='' />
			                //var orders = dsRoot.getElementsByTagName("CALL_CATEGORY[@CALL_SUB_GROUP_NAME='" + text1 + "']");
			               // var orders = dsRoot.getElementsByTagName("CC[@CSGN='" + text1 + "']");
			                var listItem;
			                listItem = new Option(names, codes);
			                ddlQueryCategory.options[0] = listItem;
//			                for (var count = 0; count < orders.length; count++)
//			                {
//            			        
//				                codes= orders[count].getAttribute("CCI"); 
//			                    names=orders[count].getAttribute("CCN"); 
//				                listItem = new Option(names, codes);
//				                ddlQueryCategory.options[ddlQueryCategory.length] = listItem;
//			                }
                                  for (var count = 0; count < dsRoot.getElementsByTagName("CC").length; count++)
			                    {
            //			               alert(dsRoot.getElementsByTagName("CC")[count].attributes.getNamedItem("CSGI").nodeValue);
            //			               alert(id);
			                           if (dsRoot.getElementsByTagName("CC")[count].attributes.getNamedItem("CSGI").nodeValue==id)
			                           {
			                                    codes= dsRoot.getElementsByTagName("CC")[count].attributes.getNamedItem("CCI").nodeValue//orders[count].getAttribute("CCI"); 
			                                    names=dsRoot.getElementsByTagName("CC")[count].attributes.getNamedItem("CCN").nodeValue//orders[count].getAttribute("CCN"); 
				                                listItem = new Option(names, codes);
				                                ddlQueryCategory.options[ddlQueryCategory.length] = listItem;
			                           }
        			              
			                    }   

			            }
			            else
			            {
			                listItem = new Option(names, codes );
                            ddlQueryCategory.options[0] = listItem;
			            }
                  
                  }
                else // Internet Explorer
                  {
                  obj=new ActiveXObject("Microsoft.XMLDOM");
                  obj.async="false";
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
                
               // obj.loadXML(document.getElementById("hdCategory").value);
			  
			}
   }
   
    function fillBRSubCategoryTechnical()
   {
      document.getElementById('ddlQuerySubCategory').options.length=0;
      id=document.getElementById('ddlQueryCategory').value;
      //var obj = new ActiveXObject("MsXml2.DOMDocument");
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
                
                //obj.loadXML(document.getElementById("hdSubCategory").value);
                 var obj;
                 if (window.DOMParser)
                  {
                  parser=new DOMParser();
                  obj=parser.parseFromString(document.getElementById("hdSubCategory").value,"text/xml");
                  
                    var dsRoot=obj.documentElement; 
			            if (dsRoot !=null)
			            {   
			             var item=document.getElementById("ddlQueryCategory").selectedIndex;
			             var text1=document.getElementById("ddlQueryCategory").options[item].text;		
			          //  var orders = dsRoot.getElementsByTagName("CALL_SUB_CATEGORY[@CALL_CATEGORY_NAME='" + text1 + "']");
			           //var orders = dsRoot.getElementsByTagName("CSC[@CCN='" + text1 + "']");
        			   
			          //<HD_CSC> <CSC CSCI='' CSCN='' CCN='' CSGN='' /> 
			            var listItem;
			            listItem = new Option(names, codes);
			            ddlQuerySubCategory.options[0] = listItem;
//			            for (var count = 0; count < orders.length; count++)
//			            {
//				            codes= orders[count].getAttribute("CSCI"); 
//			                names=orders[count].getAttribute("CSCN"); 
//				            listItem = new Option(names, codes);
//				            ddlQuerySubCategory.options[ddlQuerySubCategory.length] = listItem;
//			            }
                                 for (var count = 0; count < dsRoot.getElementsByTagName("CSC").length; count++)
			                    {
            //			               alert(dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("CCI").nodeValue);
            //			               alert(id);
			                           if (dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("CCI").nodeValue==id)
			                           {
			                                    codes= dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("CSCI").nodeValue//orders[count].getAttribute("CSCI"); 
			                                    names=dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("CSCN").nodeValue//orders[count].getAttribute("CSCN"); 
				                                listItem = new Option(names, codes);
				                                ddlQuerySubCategory.options[ddlQuerySubCategory.length] = listItem;
			                           }
        			              
			                    }   



			            }
			            else
			            {
			                listItem = new Option(names, codes );
                            ddlQuerySubCategory.options[0] = listItem;
			            }
                       
                  }
                else // Internet Explorer
                  {
                  obj=new ActiveXObject("Microsoft.XMLDOM");
                  obj.async="false";
                  obj.loadXML(document.getElementById("hdSubCategory").value);
                   var dsRoot=obj.documentElement; 
			            if (dsRoot !=null)
			            {   
			             var item=document.getElementById("ddlQueryCategory").selectedIndex;
			             var text1=document.getElementById("ddlQueryCategory").options[item].text;		
			          //  var orders = dsRoot.getElementsByTagName("CALL_SUB_CATEGORY[@CALL_CATEGORY_NAME='" + text1 + "']");
			           // var orders = dsRoot.getElementsByTagName("CSC[@CCN='" + text1 + "']");
			             var orders = dsRoot.getElementsByTagName("CSC[@CCI='" + id + "']");
        			   
        			   
        			   
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
   }
    function fillBRSubCategoryDefaultValues()
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
            
        }
    }
    catch(err){}
    
}
   function fillBRSubCategoryFunctional()
   {
   //{debugger}
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
     // var obj = new ActiveXObject("MsXml2.DOMDocument");
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
                var obj ;
                //obj.loadXML(document.getElementById("hdSubCategory").value);
                  if (window.DOMParser)
                  {
                  parser=new DOMParser();
                  obj=parser.parseFromString(document.getElementById("hdSubCategory").value,"text/xml");
                  
                var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			    		
			   // var orders = dsRoot.getElementsByTagName("CALL_SUB_CATEGORY[@CALL_CATEGORY_NAME='" + text1 + "']");
			     //var orders = dsRoot.getElementsByTagName("CSC[@CCI='" + id + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQuerySubCategory.options[0] = listItem;
//			        for (var count = 0; count < orders.length; count++)
//			        {
//			            codes= orders[count].getAttribute("CSCI")+ "," + orders[count].getAttribute("HEI") + "," + orders[count].getAttribute("HSI")  + "," + orders[count].getAttribute("TI")  + "," + orders[count].getAttribute("CTI")+ "," + orders[count].getAttribute("TE") ; 
//			            names=orders[count].getAttribute("CSCN"); 
//				        listItem = new Option(names, codes);
//				        ddlQuerySubCategory.options[ddlQuerySubCategory.length] = listItem;
//			        }
                    for (var count = 0; count < dsRoot.getElementsByTagName("CSC").length; count++)
			                {
			                       if (dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("CCI").nodeValue==id)
			                       {
			                                codes= dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("CSCI").nodeValue+ "," +  dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("HEI").nodeValue + "," +  dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("HSI").nodeValue  + "," +  dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("TI").nodeValue  + "," +  dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("CTI").nodeValue+ "," +  dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("TE").nodeValue ; 
			                                names=dsRoot.getElementsByTagName("CSC")[count].attributes.getNamedItem("CSCN").nodeValue//orders[count].getAttribute("CCN"); 
				                           // alert(codes);
				                           // alert(names);
				                            
				                            listItem = new Option(names, codes);
				                            ddlQuerySubCategory.options[ddlQuerySubCategory.length] = listItem;
			                       }
    			              
			                }
			        
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQuerySubCategory.options[0] = listItem;
			    }
                  
                  
                  }
                else // Internet Explorer
                  {
                  obj=new ActiveXObject("Microsoft.XMLDOM");
                  obj.async="false";
                  obj.loadXML(document.getElementById("hdSubCategory").value);
                  
                    var dsRoot=obj.documentElement; 
			        if (dsRoot !=null)
			        {   
    			    		
			       // var orders = dsRoot.getElementsByTagName("CALL_SUB_CATEGORY[@CALL_CATEGORY_NAME='" + text1 + "']");
			         var orders = dsRoot.getElementsByTagName("CSC[@CCI='" + id + "']");
			        var listItem;
			        listItem = new Option(names, codes);
			        ddlQuerySubCategory.options[0] = listItem;
			        for (var count = 0; count < orders.length; count++)
			        {
			            codes= orders[count].getAttribute("CSCI")+ "," + orders[count].getAttribute("HEI") + "," + orders[count].getAttribute("HSI")  + "," + orders[count].getAttribute("TI")  + "," + orders[count].getAttribute("CTI")+ "," + orders[count].getAttribute("TE") ; 
			            names=orders[count].getAttribute("CSCN"); 
			            // alert(codes);
				        // alert(names);
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
			
   }
  function fillBRCategoryFunctional()
   {
   //{debugger;}
      document.getElementById('ddlQueryCategory').options.length=0;
      document.getElementById('ddlQuerySubCategory').options.length=0;
      var  id=document.getElementById('ddlQuerySubGroup').value;
      //var obj = new ActiveXObject("MsXml2.DOMDocument");
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
                //obj.loadXML(document.getElementById("hdCategory").value);
                 var obj;
                 if (window.DOMParser)
                  {
                  parser=new DOMParser();
                  obj=parser.parseFromString(document.getElementById("hdCategory").value,"text/xml");
                  var dsRoot=obj.documentElement;                   
			            if (dsRoot !=null)
			            {   
			             var item=document.getElementById("ddlQuerySubGroup").selectedIndex;
			             var text1=document.getElementById("ddlQuerySubGroup").options[item].text;		
			            // var orders = dsRoot.getElementsByTagName("CALL_CATEGORY[@CALL_SUB_GROUP_NAME='" + text1 + "']");
			            //var orders = dsRoot.getElementsByTagName("CC[@CSGI='" + id + "']");
			            var listItem;
			            listItem = new Option(names, codes);
			            ddlQueryCategory.options[0] = listItem;
			              for (var count = 0; count < dsRoot.getElementsByTagName("CC").length; count++)
			                {
        //			               alert(dsRoot.getElementsByTagName("CC")[count].attributes.getNamedItem("CSGI").nodeValue);
        //			               alert(id);
			                       if (dsRoot.getElementsByTagName("CC")[count].attributes.getNamedItem("CSGI").nodeValue==id)
			                       {
			                                codes= dsRoot.getElementsByTagName("CC")[count].attributes.getNamedItem("CCI").nodeValue//orders[count].getAttribute("CCI"); 
			                                names=dsRoot.getElementsByTagName("CC")[count].attributes.getNamedItem("CCN").nodeValue//orders[count].getAttribute("CCN"); 
				                            listItem = new Option(names, codes);
				                            ddlQueryCategory.options[ddlQueryCategory.length] = listItem;
			                       }
    			              
			                }
			            }
			            else
			            {
			                listItem = new Option(names, codes );
                            ddlQueryCategory.options[0] = listItem;
			            }
                  	        
			       
                  }
                else // Internet Explorer
                  {
                  obj=new ActiveXObject("Microsoft.XMLDOM");
                  obj.async="false";
                  obj.loadXML(document.getElementById("hdCategory").value);
                  
                       var dsRoot=obj.documentElement; 
			            if (dsRoot !=null)
			            {   
			             var item=document.getElementById("ddlQuerySubGroup").selectedIndex;
			             var text1=document.getElementById("ddlQuerySubGroup").options[item].text;		
			            // var orders = dsRoot.getElementsByTagName("CALL_CATEGORY[@CALL_SUB_GROUP_NAME='" + text1 + "']");
			            var orders = dsRoot.getElementsByTagName("CC[@CSGI='" + id + "']");
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
   }

function FillBRAgencyDetailsTech()
         {
        
           var officeId;
           officeId=  document.getElementById('txtOfficeId').value;
           var officeIdClassName=document.getElementById('txtOfficeId').className;
         
           if (officeId != ""  && officeIdClassName !="textboxgrey")
           {
                document.getElementById('txtAgencyName').value="Searching...";
                BRCallServerTech(officeId,"This is context from client");
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
			    */
           }
           return false;
           }
function BRReceiveServerDataTech(args, context)
        {
    
        document.getElementById('lblError').innerText ="";
         //var obj = new ActiveXObject("MsXml2.DOMDocument");
            var obj;
            
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
			          
            }
            else
            {
             var parts = args.split("$");
              if (window.DOMParser)
                  {
                  parser=new DOMParser();
                  obj=parser.parseFromString(parts[0],"text/xml");
                  }
                else // Internet Explorer
                  {
                  obj=new ActiveXObject("Microsoft.XMLDOM");
                  obj.async="false";
                  obj.loadXML(parts[0]);
                  } 
                // obj.loadXML(parts[0]);
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
			           
			    }
            
            }
            
        }

 function FillBRAgencyDetailsFunctional()
         {
        
           var officeId;
           officeId=  document.getElementById('txtOfficeId').value;
           var officeIdClassName=document.getElementById('txtOfficeId').className;
         
           if (officeId != ""  && officeIdClassName !="textboxgrey")
           {
                document.getElementById('txtAgencyName').value="Searching...";
                BRCallServerFunctional(officeId,"This is context from client");
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
			    */
           }
           return false;
           }
function BRReceiveServerDataFunctional(args, context)
        {
         document.getElementById('lblError').innerText ="";
        // var obj = new ActiveXObject("MsXml2.DOMDocument");
         var obj;
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
			          
            }
            else
            {
             var parts = args.split("$");
             
                 if (window.DOMParser)
                  {
                  parser=new DOMParser();
                  obj=parser.parseFromString(parts[0],"text/xml");
                  }
                else // Internet Explorer
                  {
                  obj=new ActiveXObject("Microsoft.XMLDOM");
                  obj.async="false";
                  obj.loadXML(parts[0]);
                  } 
                // obj.loadXML(parts[0]);
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
			    }
            
            }
        }
        
        //End of New Added Code


function PopupPageBRCallLog(id)
             {
             var type;
             if (id=="1")
             {
                    
                 var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                if (strEmployeePageName!="")
                {
                   type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
                  //  type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	                window.open(type,"aaCallLogEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1");         
   	             }   
              }
             if (id=="2")
             {
                    type = "../TravelAgency/TASR_BRAgency.aspx?Popup=T&HelpDeskType=BR" ;
   	                window.open(type,"aaCallLogAgency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
             }
        
             if (id=="3")
             {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T";
   	                window.open(type,"aaCallLogAgencyStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
              }
     }
     
     
     
     /*Search Call Log*/

    function ValidateFormBRCallLog()
    {
    
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
    
    function ReturnDataBRCallLog()
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
    
    
       function SelectAllBRCallLog() 
    {
       CheckAllDataGridCheckBoxesBRCallLog(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxesBRCallLog(value) 
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
    
    function PopupPageBRFunctional(id)
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
             type = "../TravelAgency/TASR_BRAgency.aspx?Popup=T&HelpDeskType=BR" ;
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
             
                //if (strHD_RE_ID != "" && strLCode != "" && textQueryCategory=="PTR" )
                if (strHD_RE_ID != "" && strLCode != "" )
                {
                    //if(strWorkOrderNo=="" && strBDRLetterID=="")
                   // {
                    
                        if(document.getElementById("hdPTRNo").value == "")
                        {
                            type = "../BirdresHelpDesk/HDUP_Ptr.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;
   	                        window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                    }
   	                    else
   	                    {
   	                        var strAction="U|" + document.getElementById("hdEnPTRNo").value ;
   	                        type = "../BirdresHelpDesk/HDUP_Ptr.aspx?Popup=T&Action=" + strAction + "&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;
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
                        if(document.getElementById("hdBDRLetterID").value == "")
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
                                 type = "../BirdresHelpDesk/HDUP_BDR.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode +"&requestType=" + text1;//'document.getElementById("ddlQueryCategory").value;
   	                            window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                        }
   	                        else
   	                        {
   	                            document.getElementById("lblError").innerText="First Select query Category";
   	                        }
   	                    }
   	                    else
   	                     {
                             type = "../BirdresHelpDesk/HDUP_BDR.aspx?Popup=T&Action=U&ReqID="+strHD_RE_ID + "&LCode="+strLCode + "&HD_RE_BDR_ID=" + document.getElementById("hdEnBDRLetterID").value;
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
                        if(document.getElementById("hdEnWorkOrderNo").value == "")
                        {
                        type = "../BirdresHelpDesk/HDUP_WorkOrder.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;
   	                    window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                    }
           	            
   	                    else
   	                    {
                        type = "../BirdresHelpDesk/HDUP_WorkOrder.aspx?Popup=T&Action=U&ReqID="+strHD_RE_ID + "&LCode="+strLCode + "&OrderID=" + document.getElementById("hdEnWorkOrderNo").value ;
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
                type ="../BirdresHelpDeskPopup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
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
    
    //   Code For Birdres
    
    function BRPopupPage(id)
         {
      
         var type;
          if (id=="1")
         {
             type = "../TravelAgency/TASR_BRAgency.aspx?Popup=T&HelpDeskType=BR" ;
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
                type = "../BirdresHelpDesk/HDSR_Ptr.aspx?Popup=T";
   	            window.open(type,"TaaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
          }
          if (id=="4")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="../BirdresHelpDeskPopup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
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


    
    
    function ManageTBRCallLogPage()
{
//{debugger;}
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
		    
		    
		    if(textbox('txtDescription')==true )
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
		      
		      if(textbox('txtSolution')==true )
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



function ManageBRCallLogPage()
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
		    
		    if(document.getElementById("txtCallDuration").value.trim() != "" )
		     {
		        if (_isInteger(document.getElementById("txtCallDuration").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 24";
		      document.getElementById("txtCallDuration").focus();
		      return false;
		      }
		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value,10);
		      
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
		    
		    if(textbox('txtDescription')==true )
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
		      
		      if(textbox('txtSolution')==true )
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


function PopupPageBR(id)
         {
      
         var type;
          if (id=="1")
         {
             type = "../TravelAgency/TASR_BRAgency.aspx?Popup=T&HelpDeskType=BR";
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
                type = "../HelpDesk/HDSR_Ptr.aspx?Popup=T";
   	            window.open(type,"TaaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
          }
          if (id=="4")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="../Popup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
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
    
/* End of Search Call */
/* Start of Business case */
 function calcFixedIncetiveITITNew()
    {
    
    try
    {
    if((document.getElementById("lblFixIncTotalCost").innerHTML!='') && (document.getElementById("lblSegLessICITValNew").innerHTML!=''))
        {
        var IncTotal=parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML);
        var IncSegLess=parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML);
       
        
            if((parseFloat(IncSegLess)>0)&&(parseFloat(IncTotal)>0))
            {
            document.getElementById("lblFixIncICIT").innerHTML=(parseFloat(IncTotal)/parseFloat(IncSegLess)).toFixed(intRoundOff);
            
            }
            else
            {
             document.getElementById("lblFixIncICIT").innerHTML='0';
              document.getElementById("lblCPSMultiRateICITNew").innerHTML='0';
              
            }
        } 
        else
        {
             document.getElementById("lblFixIncICIT").innerHTML='0';
              document.getElementById("lblCPSMultiRateICITNew").innerHTML='0';
             
        
        }
        
        
        
        if((document.getElementById("lblFixIncTotalCost").innerHTML!='') && (document.getElementById("lblSegLessICValNew").innerHTML!=''))
        {
        var IncTotal=parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML);
        var IncSegLessIC=parseFloat(document.getElementById("lblSegLessICValNew").innerHTML);
       // var IncSegLessIC=parseFloat(document.getElementById("lblSegLessICValNew").innerHTML);
        
        
            if((parseFloat(IncSegLessIC)>0)&&(parseFloat(IncTotal)>0))
            {
            document.getElementById("lblFixIncIC").innerHTML=(parseFloat(IncTotal)/parseFloat(IncSegLessIC)).toFixed(intRoundOff);
            }
            else
            {
             document.getElementById("lblFixIncIC").innerHTML='0';
              document.getElementById("lblCPSMultiRateICNew").innerHTML='0';
              
            }
        } 
        else
        {
             document.getElementById("lblFixIncIC").innerHTML='0';
              document.getElementById("lblCPSMultiRateICNew").innerHTML='0';
             
        
        } 
        
          
        //Code Section for Segment exc IC IT footer
         if((document.getElementById("lblTotalCostNew").innerHTML!='') && (document.getElementById("lblSegLessICITValNew").innerHTML!=''))
        {
            var IncTotalF=parseFloat(document.getElementById("lblTotalCostNew").innerHTML);
            var IncSegLessF=parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML);
            if((parseFloat(IncSegLessF)>0)&&(parseFloat(IncTotalF)))
            {
            document.getElementById("lblCPSMultiRateICITNew").innerHTML=(parseFloat(IncTotalF)/parseFloat(IncSegLessF)).toFixed(intRoundOff);
            }
            else
            {
             document.getElementById("lblCPSMultiRateICITNew").innerHTML='0';
            }
        }
        else
        {
        document.getElementById("lblCPSMultiRateICITNew").innerHTML='0';
        }
        
        
         //Code Section for Segment exc IC footer
         if((document.getElementById("lblTotalCostNew").innerHTML!='') && (document.getElementById("lblSegLessICValNew").innerHTML!=''))
        {
            var IncTotalF1=parseFloat(document.getElementById("lblTotalCostNew").innerHTML);
            var IncSegLessF1=parseFloat(document.getElementById("lblSegLessICValNew").innerHTML);
            if((parseFloat(IncSegLessF1)>0)&&(parseFloat(IncTotalF1)))
            {
            document.getElementById("lblCPSMultiRateICNew").innerHTML=(parseFloat(IncTotalF1)/parseFloat(IncSegLessF1)).toFixed(intRoundOff);
            }
            else
            {
             document.getElementById("lblCPSMultiRateICNew").innerHTML='0';
            }
        }
        else
        {
        document.getElementById("lblCPSMultiRateICNew").innerHTML='0';
        }
         if((document.getElementById("lblFixIncPerMonth").innerHTML!='') && (document.getElementById("lblSegLessICValNew").innerHTML!=''))
        {
            var IncTotalFW=parseFloat(document.getElementById("lblSegLessICValNew").innerHTML);
            var IncSegLessFW=parseFloat(document.getElementById("lblFixIncPerMonth").innerHTML);
            
    
            document.getElementById("lblIncRateWOIC").innerHTML=(parseFloat(IncSegLessFW)/parseFloat(IncTotalFW)).toFixed(intRoundOff);
            
        }
        else
        {
        document.getElementById("lblIncRateWOIC").innerHTML='0';
        }
        }
        catch(err)
        {}
        
        CalculateFixIncCPS();
    }
    
    
     function CalculateExcICITFooter()
    {
    var totalCost=0;
    var segLessICIT=0;
    var segLessIC=0;
    
        
        try
        {
        if(document.getElementById("lblTotalCostNew").innerHTML!='')
        {
        totalCost=parseFloat(document.getElementById("lblTotalCostNew").innerHTML);
        }
    
    
    
        if(document.getElementById("lblSegLessICITValNew").innerHTML!='')
        {
        segLessICIT=parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML);
        
        }
        
         if(document.getElementById("lblSegLessICValNew").innerHTML!='')
        {
        segLessIC=parseFloat(document.getElementById("lblSegLessICValNew").innerHTML);
        
        }
    }
    catch(err)
    {
    }
    
        if(parseFloat(segLessICIT)>0)
        {
        document.getElementById("lblCPSMultiRateICITNew").innerHTML=(totalCost/segLessICIT).toFixed(intRoundOff);
        }
        else
        {
        document.getElementById("lblCPSMultiRateICITNew").innerHTML='0';
        }
        
        
         if(parseFloat(segLessIC)>0)
        {
        document.getElementById("lblCPSMultiRateICNew").innerHTML=(totalCost/segLessIC).toFixed(intRoundOff);
        }
        else
        {
        document.getElementById("lblCPSMultiRateICNew").innerHTML='0';
        }
        
        
        CalculateBreakFooter();
        
    }
    
    
    function CalculateBreakUp()
    {debugger
          var obj = new ActiveXObject("MsXml2.DOMDocument");
          var intConversionPer =document.getElementById("txtConversionPer").value;
          if(document.getElementById("txtConversionPer").value=='')
                        {
                        intConversionPer='100';
                        }
          
          var intTOTAL,intMIDT_CONV_PER,intLESS ,intRowAmt,intRowRate,intGrandTotal;
          intGrandTotal=0;
          if (document.getElementById("hdCalculateBreakUp").value.trim() != "" && intConversionPer.trim() != "")
            {
                obj.loadXML(document.getElementById("hdCalculateBreakUp").value);
                var dsRoot=obj.documentElement;
                                 
                if (dsRoot !=null)
                    {
                        if(document.getElementById('grdvBreakUpLast')!=null)
                        {
                            var cn=document.getElementById('grdvBreakUpLast').rows.length;
                                if (cn<=1)
                                {
                                    return false;
                                }

                        }
                        for(intcnt=0;intcnt<cn-1;intcnt++)
                        {
                            intTOTAL=document.getElementById('grdvBreakUpLast').rows[intcnt].cells[1].innerText
                            intMIDT_CONV_PER=(( intTOTAL / 100 ) * intConversionPer ).toFixed(intRoundOff)
                            intLESS=(intMIDT_CONV_PER * .9).toFixed(intRoundOff)                           
                            document.getElementById('grdvBreakUpLast').rows[intcnt].cells[2].innerText=intMIDT_CONV_PER
                            document.getElementById('grdvBreakUpLast').rows[intcnt].cells[4].innerText= intLESS
                            
                            if (document.getElementById('grdvBreakUpLast').rows[intcnt].cells[5].children.length==1)
                            {
                                intRowRate=document.getElementById('grdvBreakUpLast').rows[intcnt].cells[5].children[0].value
                                
                                if (intRowRate != 0 && intLESS != 0 && intRowAmt != 'NaN' && intLESS != 'NaN' )
                                {
                                    document.getElementById('grdvBreakUpLast').rows[intcnt].cells[6].children[0].value = (intRowRate * intLESS).toFixed(intRoundOff)
                                }
                                else
                                {
                                    document.getElementById('grdvBreakUpLast').rows[intcnt].cells[6].children[0].value = 0;
                                }
                                
                                if (document.getElementById('grdvBreakUpLast').rows[intcnt].cells[0].innerText.trim()=="IC")
                                {
                                    document.getElementById("hdICvalue").value=intLESS;
                                }
                                
                                if (document.getElementById('grdvBreakUpLast').rows[intcnt].cells[0].innerText.trim()=="IT")
                                {
                                    document.getElementById("hdITvalue").value=intLESS;
                                }
                                
                                
                                intGrandTotal =parseFloat(intGrandTotal) + parseFloat(document.getElementById('grdvBreakUpLast').rows[intcnt].cells[6].children[0].value)
                             }
                             else
                             {
                                intGrandTotal =parseFloat(intGrandTotal) + parseFloat(document.getElementById('grdvBreakUpLast').rows[intcnt].cells[6].children[0].value)
                             
                                if (document.getElementById('grdvBreakUpLast').rows[intcnt].cells[0].innerText.trim()=="DOM")
                                {
                                    document.getElementById('grdvBreakUpLast').rows[intcnt].cells[6].children[0].value=intGrandTotal.toFixed(intRoundOff);
                                }
                                
                             }
                            
                        }
                    }
                if (intGrandTotal >0)
                {
                    document.getElementById('grdvBreakUpLast').rows[cn-1].cells[6].innerText=intGrandTotal.toFixed(intRoundOff);
                }
            }     
            
                  
    }
    
    
     function calculateSegmentLessICITvalue()
    {

        var valTotalSegAsPerConver=0;
        var totalMIDT=0;
        var valTotalSergAsPerConverValue=0;
        
       // CalculateBreakUp();
              
        
       
        if(document.getElementById("hdTotalMIDT").value.trim()!='')  
        {
        totalMIDT=parseFloat(document.getElementById("hdTotalMIDT").value);
        }
        else
        {
        totalMIDT='0.00';
        }
      
       
        
         if(document.getElementById("txtConversionPer").value.trim()!='')  
        {
            if(parseFloat(document.getElementById("txtConversionPer").value)>0)
            {
          //  valTotalSegAsPerConver=parseFloat(totalMIDT)/(parseFloat(document.getElementById("txtConversionPer").value)/100);
            valTotalSegAsPerConver=parseFloat(totalMIDT)*(parseFloat(document.getElementById("txtConversionPer").value)/100);
            }
            else
            {
            valTotalSegAsPerConver='0.00';
            }
        }  
        
       
        valTotalSergAsPerConverValue=GetTotalOfLessTenOfINTLAndDOM(); // Added By  (parseFloat(valTotalSegAsPerConver)*.9)
        
         var icvalu=0;
        
        
        
        if(document.getElementById("hdICvalue").value.trim()!='')
        {
            icvalu=parseFloat(document.getElementById("hdICvalue").value);
        }
        else
        {
        icvalu=0;
        }
        
        
        var itvalue=0;
        if(document.getElementById("hdITvalue").value.trim()!='')
        {
       itvalue=parseFloat(document.getElementById("hdITvalue").value);
        }
        else
        {
        itvalue=0;
        }
        
      
               
        document.getElementById("lblSegLessICITValNew").innerHTML=(valTotalSergAsPerConverValue - (parseFloat(icvalu) + parseFloat(itvalue))).toFixed(intRoundOff)
    
            
            if(parseFloat(totalMIDT)>0)
            {
                //if((document.getElementById("lblSegLessICITValNew").innerHTML!='') &&(parseFloat(totalMIDT)>0))
                if((valTotalSergAsPerConverValue > 0) &&(parseFloat(totalMIDT)>0))
                {
                document.getElementById("lblSegLessICITPerNew").innerHTML = (parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML)*100 / valTotalSergAsPerConverValue).toFixed(intRoundOff);
                }
                else
                {
                 document.getElementById("lblSegLessICITPerNew").innerHTML ="0";
                }
            }
            else
            {
            document.getElementById("lblSegLessICITPerNew").innerHTML ="0";
            }
            
            
            
            //Code for IC Value***************
            
            //End of fCode for IC Value*********
            
            
           if(document.getElementById("lblTotalCostNew").innerHTML!='')
            {
            var a =parseFloat(document.getElementById("lblTotalCostNew").innerHTML);
           
            if(parseFloat(valTotalSergAsPerConverValue)>0)
            {
            document.getElementById("lblGrossNew").innerHTML=(a/valTotalSergAsPerConverValue).toFixed(intRoundOff);
            }
            else
            {
            document.getElementById("lblGrossNew").innerHTML='0';
            }
            }
            else
            {
            document.getElementById("lblGrossNew").innerHTML='0';
            }
            
            if(document.getElementById("txtMinMonthSegNew").value!='')
            {
            var b=parseFloat(document.getElementById("txtMinMonthSegNew").value);
            var c=document.getElementById("lblTotalCostNew").innerHTML;
                     if(parseFloat(b)>0)
                     {
                     document.getElementById("lblNetNew").innerHTML=(c/b).toFixed(intRoundOff);
                     }
                     else
                     {
                     document.getElementById("lblNetNew").innerHTML='0';
                     }
            }
            
        
        //Useless call   
     CalculateExcICITFooter();
        //Useless call   
     CalculateFixIncCPS();
     calcFixedIncetiveITITNew()
     CalculateBreakFooter();
     
    }
    
  
     function CalcTotalCostCPSMulti()
    {
    
var totalCostCPSMulti=0;
    
 var grd1=document.getElementById("grdvBreakUpLast");
var grd2=document.getElementById("grdConnectivityN");
var grd3=document.getElementById("grdvHardwareN");


        if(grd1.rows[grd1.rows.length-1].cells[3].innerText!='')
        {
        totalCostCPSMulti+=parseFloat(grd1.rows[grd1.rows.length-1].cells[5].innerText);
        }
        else
        {
        totalCostCPSMulti+=0;
        }
        
        
         if(grd2.rows[grd2.rows.length-1].cells[4].innerText!='')
        {
        totalCostCPSMulti+=parseFloat(grd2.rows[grd2.rows.length-1].cells[4].innerText);
        }
        else
        {
        totalCostCPSMulti+=0;
        }
        
         if(grd3.rows[grd3.rows.length-1].cells[4].innerText!='')
        {
        totalCostCPSMulti+=parseFloat(grd3.rows[grd3.rows.length-1].cells[4].innerText);
        }
        else
        {
        totalCostCPSMulti+=0;
        }
        
        
       document.getElementById("lblTotalCostNew").innerHTML=totalCostCPSMulti.toFixed(intRoundOff); 

calculateSegmentLessICITvalue();

    }
 
    
    function CalculateMinMonthSegNew()
    {
    
    if(document.getElementById("txtFixIncMinMonthSeg").value!='')
    {
    document.getElementById("txtMinMonthSegNew").value=document.getElementById("txtFixIncMinMonthSeg").value;
    }
    
     if(document.getElementById("txtMinMonthSegNew").value.trim()!='')
           {
             if(IsDataValid(document.getElementById("txtMinMonthSegNew").value,4)==false)
             {
             document.getElementById("lblError").innerHTML="Minimum Monthly Segment is Numeric";
             document.getElementById("txtMinMonthSegNew").focus();
                        return false;
                        } 
           }
           else
           {
           document.getElementById("lblNetNew").innerHTML='';
           }
           
            if(document.getElementById("txtMinMonthSegNew").value!='')
            {
            var b=parseFloat(document.getElementById("txtMinMonthSegNew").value);
            var c=document.getElementById("lblTotalCostNew").innerHTML;
            //alert("abhi");
                      if(parseFloat(b)>0)
                      {
                     document.getElementById("lblNetNew").innerHTML=(c/b).toFixed(intRoundOff);
                     }
                     else
                     {
                     document.getElementById("lblNetNew").innerHTML='0';
                     }
            }
            else
            {
            
            }
    }
    
    function GetTotalOfLessTenOfINTLAndDOM()
    {
   // debugger;
                     var Total=0;
                     try
                     {     
                          var grd=document.getElementById("grdvBreakUpLast");    
                          if (grd !=null)
                          {
                              var TenPerLess=0 ;    
                              for(var i=0;i<grd.rows.length-1;i++)
                                {
                                                   
                                                if (grd.rows[i].cells[3].innerText.trim()!='')
                                                {
                                                TenPerLess =parseFloat(grd.rows[i].cells[3].innerText.trim()).toFixed(intRoundOff);
                                                }
                                                else
                                                {
                                                TenPerLess=0;
                                                }
                                               if (grd.rows[i].cells[0].innerText.trim()=='DOM')
                                               {
                                                  Total =parseFloat(Total) +parseFloat( TenPerLess);
                                               }
                                               
                                            if (grd.rows[i].cells[0].innerText.trim()=='INTL')
                                               {
                                                    Total =parseFloat(Total) +parseFloat( TenPerLess);
                                               }   
                                }
                         }                           
                        
                      return  Total;
                       
                     }
                    catch(err)
                    {
                      return 0;
                    }
          alert(Total);
    
    }   
    function CalculateBreakFooter()
    {
 
     var converPer='0';
     
         if((document.getElementById("hdTotalMIDT").value.trim()!='') &&(document.getElementById("hdICvalue").value.trim()!=''))
        {
                        var totMidt=parseFloat(document.getElementById("hdTotalMIDT").value);
                        var conversionPer=document.getElementById("txtConversionPer").value.trim();
                            if(document.getElementById("txtConversionPer").value=='')
                            {
                                converPer='0';
                                converPer=100;
                            }
                        
                                if(conversionPer!='0' && conversionPer!='')
                                {
                                    if(parseFloat(totMidt)>0)
                                    {
                                    totMidt=converPer=GetTotalOfLessTenOfINTLAndDOM();     // Added By    (parseFloat(totMidt)*(parseFloat(conversionPer)/100))*0.9;
                                    converPer=totMidt
                                    }
                                    else
                                    {
                                        converPer='0';
                                    }
                                }
                         
                                
                         converPer=GetTotalOfLessTenOfINTLAndDOM();     // Added By    
                                
                                
                        
                        var ICValue='0';
                        if(parseFloat(document.getElementById("hdICvalue").value)>0)
                        ICValue=(parseFloat(converPer)-parseFloat(document.getElementById("hdICvalue").value)).toFixed(intRoundOff);
                        else
                        {
                        ICValue=parseFloat(converPer).toFixed(intRoundOff)
                        }
                        document.getElementById("lblSegLessICValNew").innerHTML=ICValue;
                        
                        
            if(parseFloat(document.getElementById("hdTotalMIDT").value.trim())>0 && parseFloat(converPer)>0)
            {
            document.getElementById("lblSegLessICPerNew").innerHTML=((parseFloat(ICValue)/parseFloat(converPer))*100).toFixed(intRoundOff);
            }
            else
            {
            document.getElementById("lblSegLessICPerNew").innerHTML='0';
            }
            
                        if(document.getElementById("lblSegLessICValNew").innerHTML=='NaN')
                        {
                        document.getElementById("lblSegLessICValNew").innerHTML='0'
                        }
                        
            
            
        }
        
         if((document.getElementById("hdTotalMIDT").value.trim()!='') &&(document.getElementById("hdITvalue").value.trim()!='')  && parseFloat(converPer)>0)
        {
        
                        var ICITValue=parseFloat(converPer)-parseFloat(document.getElementById("hdITvalue").value);
                        
                        if(document.getElementById("hdICvalue").value.trim()!='')
                        {
                        ICITValue=parseFloat(ICITValue)-parseFloat(document.getElementById("hdICvalue").value);
                        }
                        
                        document.getElementById("lblSegLessICITValNew").innerHTML=parseFloat(ICITValue).toFixed(intRoundOff);
                        
                        if(parseFloat(document.getElementById("hdTotalMIDT").value.trim())>0)
                        {
                        document.getElementById("lblSegLessICITPerNew").innerHTML=((parseFloat(ICITValue)/parseFloat(converPer))*100).toFixed(intRoundOff);
                        }
                        else
                        {
                        document.getElementById("lblSegLessICITPerNew").innerHTML='0';
                        }
        }
        
         if((document.getElementById("txtFixIncentive").value.trim()!='') &&(document.getElementById("txtFixIncMonth").value!='') && parseFloat(converPer)>0)
        {
         
            document.getElementById("lblIncRateGrossVal").innerHTML=(parseFloat(document.getElementById("txtFixIncentive").value)/parseFloat(document.getElementById("txtFixIncMonth").value)/parseFloat(converPer)).toFixed(intRoundOff);
        }
        else
        {
         document.getElementById("lblIncRateGrossVal").innerHTML='0';
        }
        
        
        //lblIncRateWOIC
            //if((document.getElementById("lblSegLessICValNew").innerHTML!='') && (document.getElementById("lblFixIncTotalCost").innerHTML!=''))
         if((document.getElementById("txtFixIncentive").value.trim()!='') &&(document.getElementById("txtFixIncMonth").value!='') && document.getElementById("lblSegLessICValNew").innerHTML!='' && parseFloat(document.getElementById("lblSegLessICValNew").innerHTML)!='0')
            
            {
           // document.getElementById("lblIncRateWOIC").innerHTML=(parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML)/parseFloat(document.getElementById("lblSegLessICValNew").innerHTML)).toFixed(intRoundOff);
            document.getElementById("lblIncRateWOIC").innerHTML=(parseFloat(document.getElementById("txtFixIncentive").value)/parseFloat(document.getElementById("txtFixIncMonth").value)/parseFloat(document.getElementById("lblSegLessICValNew").innerHTML)).toFixed(intRoundOff);
            
            }
            else
            {
            document.getElementById("lblIncRateWOIC").innerHTML='0';
            }
            
            
        //    if((document.getElementById("lblSegLessICITValNew").innerHTML!='') && (document.getElementById("lblFixIncTotalCost").innerHTML!=''))
         if((document.getElementById("txtFixIncentive").value.trim()!='') &&(document.getElementById("txtFixIncMonth").value!='') && document.getElementById("lblSegLessICITValNew").innerHTML!='' && parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML)!='0')
        
            {
           // document.getElementById("lblIncRateWOICIT").innerHTML=(parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML)/parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML)).toFixed(2);
            document.getElementById("lblIncRateWOICIT").innerHTML=(parseFloat(document.getElementById("txtFixIncentive").value)/parseFloat(document.getElementById("txtFixIncMonth").value)/parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML)).toFixed(intRoundOff);
            
            }
            else
            {
            document.getElementById("lblIncRateWOICIT").innerHTML='0';
            }
            
            
            
            //lblIncRateWOICIT
            
            
            if(document.getElementById("lblIncRateWOIC").innerHTML=='NaN')
            {
            document.getElementById("lblIncRateWOIC").innerHTML='0';
            }
            
            if(document.getElementById("lblIncRateWOICIT").innerHTML=='NaN')
            {
            document.getElementById("lblIncRateWOICIT").innerHTML='0';
            }
            
            
            if((document.getElementById("txtFixIncMinMonthSeg").value!='') && (document.getElementById("lblFixIncTotalCost").innerHTML!=''))
            {
           // document.getElementById("lblIncRateNet").innerHTML=(parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML)/parseFloat(document.getElementById("txtFixIncMinMonthSeg").value)).toFixed(2);
            document.getElementById("lblIncRateNet").innerHTML=(parseFloat(document.getElementById("txtFixIncentive").value)/parseFloat(document.getElementById("txtFixIncMonth").value)/parseFloat(document.getElementById("txtFixIncMinMonthSeg").value)).toFixed(intRoundOff);
            
            }
            else
            {
            document.getElementById("lblIncRateNet").innerHTML='0';
            }
            
            
        
        
    }
    
    
    
    function CalculateFixIncCPS()
    {
    if(document.getElementById("txtFixIncMinMonthSeg").value!='')
    {
    document.getElementById("txtMinMonthSegNew").value=document.getElementById("txtFixIncMinMonthSeg").value;
    }
    if(document.getElementById("txtFixIncMinMonthSeg").value.trim()!='')
           {
             if(IsDataValid(document.getElementById("txtFixIncMinMonthSeg").value,4)==false)
             {
             document.getElementById("lblError").innerHTML="Fix Incentive Min. Month is Numeric";
             document.getElementById("txtFixIncMinMonthSeg").focus();
                        return false;
                        } 
           }
           
                    if((document.getElementById("txtFixIncMinMonthSeg").value.trim()!='')&&(document.getElementById("lblFixIncTotalCost").innerHTML!=''))
                   {
                        var totalCost=0;
                       if (document.getElementById("txtFixIncMinMonthSeg").value.trim()!='0')
                       {
                             totalCost=parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML)/parseFloat(document.getElementById("txtFixIncMinMonthSeg").value.trim());
                       }                 
                         document.getElementById("lblFixIncNet").innerHTML=parseFloat(totalCost).toFixed(intRoundOff);
                         document.getElementById("lblFixIncCPS").innerHTML=parseFloat(totalCost).toFixed(intRoundOff);
                   
                   }
                   else
                   {
                       document.getElementById("lblFixIncNet").innerHTML=parseFloat(0).toFixed(intRoundOff);
                         document.getElementById("lblFixIncCPS").innerHTML=parseFloat(0).toFixed(intRoundOff);
                   
                   }
                   
                   
                     if((document.getElementById("lblFixIncTotalCost").innerHTML!='') && (document.getElementById("hdTotalMIDT").value!=''))
                    {
                        var totMidt=parseFloat(document.getElementById("hdTotalMIDT").value);
                        
                        var conversionPer=document.getElementById("txtConversionPer").value
                        if(document.getElementById("txtConversionPer").value=='')
                        {
                        conversionPer='100';
                        }
                        
                        if(conversionPer!='0')
                        {
                            if(parseFloat(totMidt)>0)
                            {
                            totMidt=   GetTotalOfLessTenOfINTLAndDOM();     // Added By        //  (parseFloat(totMidt)*(parseFloat(conversionPer)/100))*0.9;
                            var totalcost=parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML);
                            document.getElementById("lblFixIncGross").innerHTML=(parseFloat(totalcost)/(parseFloat(totMidt))).toFixed(intRoundOff);
                            }
                        }
                        else
                        {
                        document.getElementById("lblFixIncGross").innerHTML='0';
                        }
                    }
                    else
                    {
                    document.getElementById("lblFixIncGross").innerHTML='0';
                    }
        calculateIncRateNet();
        CalculateBreakFooter();  
        
        //      Start of   Added By 
        try
        {
             if(document.getElementById("txtMinMonthSegNew").value!='')
            {
                    var ab=parseFloat(document.getElementById("txtMinMonthSegNew").value);
                    var cd=document.getElementById("lblTotalCostNew").innerHTML;
                      if(parseFloat(ab)>0)
                      {
                     document.getElementById("lblNetNew").innerHTML=(cd/ab).toFixed(intRoundOff);
                     }
                     else
                     {
                     document.getElementById("lblNetNew").innerHTML='0';
                     }
            }
          }  
        catch(err)
            {            
            }
        //      End of   Added By    
        
  
    }
    
    function calculateIncRateNet()
    {
   
        if((document.getElementById("txtFixIncMonth").value!='')&&(document.getElementById("txtFixIncMinMonthSeg").value!=''))
        {
            var a=document.getElementById("txtFixIncentive").value;
            var b=document.getElementById("txtFixIncMonth").value;
            var c=document.getElementById("txtFixIncMinMonthSeg").value;
            
            if((parseFloat(b)>0) && (parseFloat(c)>0))
            {
            document.getElementById("lblIncRateNet").innerHTML=(parseFloat(parseFloat(a)/parseFloat(b))/parseFloat(c)).toFixed(intRoundOff);
            }
            else
            {
            document.getElementById("lblIncRateNet").innerHTML="0";
            }
        }
        else
        {
        document.getElementById("lblIncRateNet").innerHTML="0";
        }
    }
    
    function CalculateFixIncentiveICIT()
    {
    
    try
    {
     calcFixedIncetiveITITNew();
     CalculateBreakFooter();
     
    }
    catch(err)
    {
    
    }
    
    try
    {
         if(document.getElementById("txtFixIncentive").value.trim()!='')
           {
             if(IsDataValid(document.getElementById("txtFixIncentive").value,4)==false)
             {
             document.getElementById("lblError").innerHTML="Fix Incentive is Numeric";
             document.getElementById("txtFixIncentive").focus();
                        return false;
                        } 
           }
           
           if(document.getElementById("txtFixIncMonth").value.trim()!='')
           {
             if(IsDataValid(document.getElementById("txtFixIncMonth").value,4)==false)
             {
             document.getElementById("lblError").innerHTML="Fix Incentive Month is Numeric";
             document.getElementById("txtFixIncMonth").focus();
                        return false;
                        } 
           }
           
           
           var grd=document.getElementById("grdConnectivityN");
        var valTotalCon=grd.rows[grd.rows.length-1].cells[4].innerText;
        
         var grdH=document.getElementById("grdvHardwareN");
        var valTotalHw='0';
        try
        {
       valTotalHw=grdH.rows[grdH.rows.length-1].cells[4].innerText;
        }
        catch(err)
        {
        }
        
                    if((document.getElementById("txtFixIncentive").value.trim()!='')&&(document.getElementById("txtFixIncMonth").value.trim()!=''))
                   {
                   
                          var totalCost=0;
                          if (document.getElementById("txtFixIncMonth").value.trim()!="0" )
                         {
                            totalCost=parseFloat(document.getElementById("txtFixIncentive").value.trim())/parseFloat(document.getElementById("txtFixIncMonth").value.trim());
                         }
                   
                        
                         document.getElementById("lblFixIncPerMonth").innerHTML=parseFloat(totalCost).toFixed(intRoundOff);
                         document.getElementById("lblFixIncTotalCost").innerHTML=(parseFloat(totalCost)+parseFloat(valTotalCon)+parseFloat(valTotalHw)).toFixed(intRoundOff);
                          if(parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML)>0)
                           {
                           document.getElementById("lblFixIncICIT").innerHTML=(parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML)/parseFloat(document.getElementById("lblSegLessICITValNew").innerHTML)).toFixed(intRoundOff);
                           }
                           else
                           {
                           document.getElementById("lblFixIncICIT").innerHTML='0';
                           }
                         if((document.getElementById("lblFixIncTotalCost").innerHTML!='') && (document.getElementById("lblSegLessICValNew").innerHTML!=''))
                          {
                                 var IncTotal=parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML);
                                var IncSegLessIC=parseFloat(document.getElementById("lblSegLessICValNew").innerHTML);
                                if((parseFloat(IncSegLessIC)>0)&&(parseFloat(IncTotal)>0))
                                {
                                document.getElementById("lblFixIncIC").innerHTML=(parseFloat(IncTotal)/parseFloat(IncSegLessIC)).toFixed(intRoundOff);
                                }
                                else
                                {
                                 document.getElementById("lblFixIncIC").innerHTML='0';
                                  
                                }
                        } 
                        else
                        {
                             document.getElementById("lblFixIncIC").innerHTML='0';                           
                        } 
                   }
                   else
                   {
                       // alert("Abhi");
                            var totalCost=parseFloat(0);
                            document.getElementById("lblFixIncPerMonth").innerHTML=parseFloat(totalCost).toFixed(intRoundOff);
                            document.getElementById("lblFixIncTotalCost").innerHTML=(parseFloat(totalCost)+parseFloat(valTotalCon)+parseFloat(valTotalHw)).toFixed(intRoundOff);
                   
                   }
           
           }
           catch(err)
           {}
           
        
        calculateIncRateNet(); 
        
        
        
         if((document.getElementById("lblFixIncTotalCost").innerHTML!='') && (document.getElementById("hdTotalMIDT").value!=''))
                    {
                        var totMidt=parseFloat(document.getElementById("hdTotalMIDT").value);
                        
                        var conversionPer=document.getElementById("txtConversionPer").value
                        if(document.getElementById("txtConversionPer").value=='')
                        {
                        conversionPer='100';
                        }
                        
                        if(conversionPer!='0')
                        {
                            if(parseFloat(totMidt)>0)
                            {
                            totMidt=converPer=GetTotalOfLessTenOfINTLAndDOM();     // Added By    (parseFloat(totMidt)*(parseFloat(conversionPer)/100))*0.9;
                            var totalcost=parseFloat(document.getElementById("lblFixIncTotalCost").innerHTML);
                            document.getElementById("lblFixIncGross").innerHTML=(parseFloat(totalcost)/(parseFloat(totMidt))).toFixed(intRoundOff);
                            }
                        }
                        else
                        {
                        document.getElementById("lblFixIncGross").innerHTML='0';
                        }
                    }
                    else
                    {
                    document.getElementById("lblFixIncGross").innerHTML='0';
                    }  
    
         CalculateFixIncCPS();
    }
    
   function calculateFixedIncentive()
   {
   if(document.getElementById("txtIncRateN").value.trim()!='')
   {
            if(IsDataValid(document.getElementById("txtIncRateN").value,4)==false)
                {
                document.getElementById("lblError").innerHTML="Incentive Rate is Numeric";
                document.getElementById("txtIncRateN").focus();
                return false;
                } 
   }
   
   if(document.getElementById("txtIncExpectedSegN").value.trim()!='')
   {
            if(IsDataValid(document.getElementById("txtIncExpectedSegN").value,4)==false)
                {
                document.getElementById("lblError").innerHTML="Expected Segment is Numeric";
                document.getElementById("txtIncExpectedSegN").focus();
                return false;
                } 
   }
   
   
        var grd=document.getElementById("grdConnectivityN");
        var valTotalCon=grd.rows[grd.rows.length-1].cells[4].innerText;
        
         var grdH=document.getElementById("grdvHardwareN");
        var valTotalHw=grdH.rows[grdH.rows.length-1].cells[4].innerText;
        

   
       if((document.getElementById("txtIncRateN").value.trim()!='')&&(document.getElementById("txtIncExpectedSegN").value.trim()!='') )
       {
           var totalCost=parseFloat(document.getElementById("txtIncRateN").value.trim())*parseFloat(document.getElementById("txtIncExpectedSegN").value.trim());
           document.getElementById("lblIncTotalCostN").innerHTML=parseFloat(totalCost).toFixed(intRoundOff);
           var totalCostN=parseFloat(totalCost)+parseFloat(valTotalCon)+parseFloat(valTotalHw);
           document.getElementById("lblTSegTotalCostN").innerHTML=(parseFloat(totalCost)+parseFloat(valTotalCon)+parseFloat(valTotalHw)).toFixed(intRoundOff);
           document.getElementById("lblTotalSeg").innerHTML=document.getElementById("txtIncExpectedSegN").value;
               if(document.getElementById("txtIncExpectedSegN").value.trim()!='')
               {
                       if(parseFloat(document.getElementById("lblTotalSeg").innerHTML)>0)
                       {
                       document.getElementById("lblIncCpsTotalN").innerHTML=(parseFloat(totalCostN)/parseFloat(document.getElementById("lblTotalSeg").innerHTML)).toFixed(intRoundOff);
                       }
                       else
                       {
                       document.getElementById("lblIncCpsTotalN").innerHTML='0';
                       }
              }
           //lblTotalSeg lblIncCpsTotalN
        }
      else
      {
       //document.getElementById("lblIncTotalCostN").innerHTML='0.0';
       
       document.getElementById("lblTotalSeg").innerHTML=document.getElementById("txtIncExpectedSegN").value;
       var totalCostN=parseFloat(valTotalCon)+parseFloat(valTotalHw);       
       document.getElementById("lblIncTotalCostN").innerHTML='0';
       document.getElementById("lblTSegTotalCostN").innerHTML=totalCostN;
                  if(parseFloat(document.getElementById("lblTotalSeg").innerHTML)>0)
                       {
                       document.getElementById("lblIncCpsTotalN").innerHTML=(parseFloat(totalCostN)/parseFloat(document.getElementById("lblTotalSeg").innerHTML)).toFixed(intRoundOff);
                       }
                       else
                       {
                       document.getElementById("lblIncCpsTotalN").innerHTML='0';
                       }
       
      }
       
        return false;
   }
   
   
   //Code for calculating Last Three Month Breakup
    function calculateBreak(uncost,tot,no)
{
 
try
{
        var grd=document.getElementById("grdvBreakUpLast");
        var footerrow=grd.rows.length-1;  
        var totalConnCost=0; 
            if(document.getElementById(no).value.trim()=="")
            {
            //return false;
            }
            
            if(document.getElementById(no).value!="")
            {
                if(IsDataValid(document.getElementById(no).value,4)==false)
                {
                document.getElementById("lblError").innerHTML="Break up Rate is Numeric";
                document.getElementById(no).focus();
                return false;
                } 
            }
        try
        {
//            if(document.getElementById(uncost).innerHTML!='')
//            {
//            document.getElementById(tot).innerHTML=(parseFloat(document.getElementById(no).value)*parseFloat(document.getElementById(uncost).innerHTML)).toFixed(intRoundOff);
//            }

            if  ( (document.getElementById(no).value!='') &&(document.getElementById(uncost).innerHTML!=''))
               {
                      document.getElementById(tot).innerHTML=(parseFloat(document.getElementById(no).value)*parseFloat(document.getElementById(uncost).innerHTML)).toFixed(intRoundOff)
               }
               else
               {
                   document.getElementById(tot).innerHTML="0";
               }


        }
        catch(err)
        {
        document.getElementById(tot).innerHTML='0';
        }

 //Calculation Starts for Total 

        var unitCost=0;
        var No=0;
        var Total=0;
        var GrandTotal=0;
                      for(var i=0;i<grd.rows.length-1;i++)
                        {
                        try
                        {
                       
                                                 
                                    if (grd.rows[i].cells[3].innerText.trim()!='')
                                    {
                                    unitCost =parseFloat(grd.rows[i].cells[3].innerText.trim()).toFixed(intRoundOff);
                                    }
                                    else
                                    {
                                    unitCost=0;
                                    }
                            
                            
                            if (grd.rows[i].cells[4].children[0].type=="text")
                                    {                 
                                       if(grd.rows[i].cells[4].children[0].value.trim()!='')
                                       {
                                         No =parseFloat(grd.rows[i].cells[4].children[0].value.trim());
                                        
                                       }
                                       else
                                       {
                                       No=0
                                       }
                                    } 
                                 
                                    
                                    
                                    Total=unitCost*No;
                                   
                                    GrandTotal =GrandTotal +Total;
                                
                                   if (grd.rows[i].cells[0].innerText.trim()=='DOM')
                                   {
                                   grd.rows[i].cells[5].innerText=GrandTotal.toFixed(intRoundOff);
                                   }
                                   
                                   
                                   if (grd.rows[i].cells[0].innerText.trim()=='IC')
                                   {
                                   document.getElementById("hdICvalue").value=grd.rows[i].cells[3].innerText;
                                   }
                                   
                                   if (grd.rows[i].cells[0].innerText.trim()=='IT')
                                   {
                                  document.getElementById("hdITvalue").value=grd.rows[i].cells[3].innerText;
                                   }
                                   
                                   
                                    
                            }
                            catch(err)
                            {
                            GrandTotal=0;
                            }
                        }
                       
    grd.rows[grd.rows.length-1].cells[5].innerText=GrandTotal.toFixed(intRoundOff);
//grandTotalConn=GrandTotal;
calculateFixedIncentive();
CalcTotalCostCPSMulti();
//Calculation Starts for Total


} catch(err){}
     
}

//End of Code for Calculating last three month breakup

  function ConnectivityCalculation(tot,no,uncost)
{
 

try
{
        var grd=document.getElementById("grdConnectivityN");
        var footerrow=grd.rows.length-1;  
        var totalConnCost=0; 
            if(document.getElementById(no).value.trim()=="")
            {
            //return false;
            }
            
            if(document.getElementById(no).value!="")
            {
                if(IsDataValid(document.getElementById(no).value,4)==false)
                {
                document.getElementById("lblError").innerHTML="Connectivity No. is Numeric";
                document.getElementById(no).focus();
                return false;
                } 
            }
        try
        {
           //alert(document.getElementById(no).value);
             //alert(document.getElementById(uncost).innerHTML);
           if  ( (document.getElementById(no).value!='') &&(document.getElementById(uncost).innerHTML!=''))
               {
                      document.getElementById(tot).innerHTML=(parseFloat(document.getElementById(no).value)*parseFloat(document.getElementById(uncost).innerHTML)).toFixed(intRoundOff)
               }
               else
               {
                   document.getElementById(tot).innerHTML="0";
               }
     
        }
        catch(err)
        {
       // document.getElementById(tot).innerHTML='0';
        }



 //Calculation Starts for Total 

        var unitCost=0;
        var No=0;
        var Total=0;
        var GrandTotal=0;
                      for(var i=0;i<grd.rows.length-1;i++)
                        {
                        try
                        {
                       
                                                 
                                    if (grd.rows[i].cells[1].innerText.trim()!='')
                                    {
                                    unitCost =parseFloat(grd.rows[i].cells[1].innerText.trim())
                                    }
                                    else
                                    {
                                    unitCost=0;
                                    }
                            
                            
                            if (grd.rows[i].cells[3].children[0].type=="text")
                                    {                 
                                       if(grd.rows[i].cells[3].children[0].value.trim()!='')
                                       {
                                         No =parseFloat(grd.rows[i].cells[3].children[0].value.trim());
                                        
                                       }
                                       else
                                       {
                                       No=0
                                       }
                                    } 
                                 
                                    
                                    
                                    Total=unitCost*No;
                                   
                                    GrandTotal =GrandTotal +Total;
                                   
                                    
                            }
                            catch(err)
                            {
                            GrandTotal=0;
                            }
                        }
                        
    grd.rows[grd.rows.length-1].cells[4].innerText=GrandTotal.toFixed(intRoundOff);
grandTotalConn=GrandTotal.toFixed(intRoundOff);
calculateFixedIncentive();
CalcTotalCostCPSMulti();
CalculateFixIncentiveICIT();
CalculateFixIncCPS();
//Calculation Starts for Total

} catch(err){}
}


function HardwareNCalculation(tot,no,uncost)
{
 
try
{
        var grd=document.getElementById("grdvHardwareN");
        var footerrow=grd.rows.length-1;  
        var totalConnCost=0; 
            if(document.getElementById(no).value.trim()=="")
            {
            //return false;
            }
        if(document.getElementById(no).value!="")
            {
                if(IsDataValid(document.getElementById(no).value,4)==false)
                {
                document.getElementById("lblError").innerHTML="Hardware Quantity is Numeric";
                document.getElementById(no).focus();
                return false;
                } 
                else
                {
                document.getElementById("lblError").innerHTML="";
                }
            }
             
        try
        {
            
          //  document.getElementById(tot).innerHTML=(parseFloat(document.getElementById(no).value)*parseFloat(document.getElementById(uncost).innerHTML)).toFixed(intRoundOff);
           if  ( (document.getElementById(no).value!='') &&(document.getElementById(uncost).innerHTML!=''))
               {
                      document.getElementById(tot).innerHTML=(parseFloat(document.getElementById(no).value)*parseFloat(document.getElementById(uncost).innerHTML)).toFixed(intRoundOff)
               }
               else
               {
                   document.getElementById(tot).innerHTML="0";
               }
          
        }
        catch(err)
        {
        document.getElementById(tot).innerHTML='0';
        }
         //Calculation Starts for Total 

        var unitCost=0;
        var No=0;
        var Total=0;
        var GrandTotal=0;
                      for(var i=0;i<grd.rows.length-1;i++)
                        {
                        try
                        {
                       
                                                 
                                    if (grd.rows[i].cells[1].innerText.trim()!='')
                                    {
                                    unitCost =parseFloat(grd.rows[i].cells[1].innerText.trim())
                                    }
                                    else
                                    {
                                    unitCost=0;
                                    }
                            
                            
                            if (grd.rows[i].cells[3].children[0].type=="text")
                                    {                 
                                       if(grd.rows[i].cells[3].children[0].value.trim()!='')
                                       {
                                         No =parseFloat(grd.rows[i].cells[3].children[0].value.trim()).toFixed(intRoundOff);
                                        
                                       }
                                       else
                                       {
                                       No=0
                                       }
                                    } 
                                 
                                    
                                    
                                    Total=unitCost*No;
                                   
                                    GrandTotal =GrandTotal +Total;
                                   
                                    
                            }
                            catch(err)
                            {
                            GrandTotal=0;
                            }
                        }
                        
    grd.rows[grd.rows.length-1].cells[4].innerText=GrandTotal.toFixed(intRoundOff);
grandTotalHw=GrandTotal;
calculateFixedIncentive();
CalcTotalCostCPSMulti();
CalculateFixIncentiveICIT();
CalculateFixIncCPS();
//Calculation Starts for Total

} catch(err){}
}


    function validateBusinessCase()
    {
    
//         if(document.getElementById("txtConversionPer").value=='')
//        {
//                document.getElementById("lblError").innerHTML="Conversion Percentage is Mandatory";
//                document.getElementById("txtConversionPer").focus();
//                return false;
//        }
        
    
        if(document.getElementById("txtPeriodFrom").value.trim()=='')
        {
                document.getElementById("lblError").innerHTML="Contract Period From is Mandatory";
                BC_TAB_Error()
                return false;
        }
         else if (isDate(document.getElementById("txtPeriodFrom").value,"dd/MM/yyyy")==false)
            {
            document.getElementById("lblError").innerText="Invalid date format";
           BC_TAB_Error()
            return false;

            }
        
        
        if(document.getElementById("txtPeriodTo").value.trim()=='')
        {
                document.getElementById("lblError").innerHTML="Contract Period To is Mandatory";
               BC_TAB_Error()
                return false;
        }
        else if (isDate(document.getElementById("txtPeriodTo").value,"dd/MM/yyyy")==false)
            {
            document.getElementById("lblError").innerText="Invalid date format";
            BC_TAB_Error()
            return false;

            }
                try
                {
                    var eleCounter=0;
                    var intLength=0;
                    var intChecked=0;
                    var chkBoxList = document.getElementById("chkSLABQUALIFICATION");
                    var chkBoxCount= chkBoxList.getElementsByTagName("input");
                    for(var i=0;i<chkBoxCount.length;i++)
                    {
                        intLength=intLength + 1;
                    }
                        
                    for(eleCounter=0;eleCounter<=intLength-1;eleCounter++)
                    {
                        var elementID='chkSLABQUALIFICATION_'+ eleCounter;
                       if (document.getElementById(elementID).checked==true)
                        {
                                  intChecked=1;                    
                        }
                    }
                    
                    if (intChecked==0)
                    {
                         document.getElementById("lblError").innerText="Slab Qualification is mandatory.";
                         BC_TAB_Error()
                         return false;
                    }
        
                    
                }
                catch(err){}
        
    }
    
    
    function BC_TAB_Error()
    {
    try{
                document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayBlock';        
                document.getElementById('pnlLocation').className='displayNone'; 
                
                
                
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
                 }
    catch(err)
    {
    }
    }
    
    function validateConnectivity()
    {
        if(document.getElementById("drpConnectivity").selectedIndex==0)
        {
        document.getElementById("lblError").innerHTML="Please Select Connectivity Type";
        return false;
        }
            if(document.getElementById("txtConnectivityNo").value=='')
            {
                document.getElementById("lblError").innerHTML="Connectivity No. is Mandatory";
                document.getElementById("txtConnectivityNo").focus();
                return false;
            }           
        else
        {
            if(IsDataValid(document.getElementById("txtConnectivityNo").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="Connectivity No. is Numeric";
            document.getElementById("txtConnectivityNo").focus();
            
                return false;
            }
        }
    }
    
    
    
     function validateHardware()
    {
        if(document.getElementById("drpHardware").selectedIndex==0)
        {
        document.getElementById("lblError").innerHTML="Please Select Hardware Name";
        return false;
        }
            if(document.getElementById("txtHWNo").value=='')
            {
                document.getElementById("lblError").innerHTML="Hardware Qty.  is Mandatory";
                document.getElementById("txtHWNo").focus();
                return false;
            }           
        else
        {
            if(IsDataValid(document.getElementById("txtHWNo").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="Hardware Qty. is Numeric";
            document.getElementById("txtHWNo").focus();
            return false;
            }
        }
    }
    
    
    function validateSlabs()
    {
        if(document.getElementById("txtRangeFromN").value=='')
        {
        document.getElementById("lblError").innerHTML="Range From is Mandatory";
        document.getElementById("lblInnerError").innerHTML="Range From is Mandatory";
        return false;
       }
       else
       {
            if(IsDataValid(document.getElementById("txtRangeFromN").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="Range From is Numeric";
            document.getElementById("lblInnerError").innerHTML="Range From is Numeric";
            return false;
            }
       }
        
        if(document.getElementById("txtSlabAmountN").value=='')
        {
        document.getElementById("lblError").innerHTML="Slab is Mandatory";
        document.getElementById("lblInnerError").innerHTML="Slab is Mandatory";
        return false;
        }
        else
        {
            if(IsDataValid(document.getElementById("txtSlabAmountN").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="Amount is Numeric";
            document.getElementById("lblInnerError").innerHTML="Amount is Numeric";
            return false;
            }
        }
       
            if(document.getElementById("txtRangeToN").value!='')
            {
                if(IsDataValid(document.getElementById("txtRangeToN").value,5)==false)
                {
                document.getElementById("lblError").innerHTML="Range To is Numeric";
               document.getElementById("lblInnerError").innerHTML="Range To is Numeric";
                return false;
            }
        }
        
    }
    
    
    function validatePLBSlabs()
    {
        if(document.getElementById("txtPlbSlabFromN").value=='')
        {
        document.getElementById("lblError").innerHTML="Range From is Mandatory";
        document.getElementById("lblInnerError").innerHTML="Range From is Mandatory";
        
        return false;
       }
       else
       {
            if(IsDataValid(document.getElementById("txtPlbSlabFromN").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="Range From is Numeric";
            document.getElementById("lblInnerError").innerHTML="Range From is Numeric";
            
            return false;
            }
       }
        
        if(document.getElementById("txtPlbSlabAmountN").value=='')
        {
        document.getElementById("lblError").innerHTML="Slab is Mandatory";
        document.getElementById("lblInnerError").innerHTML="Slab is Mandatory";
        
        return false;
        }
        else
        {
            if(IsDataValid(document.getElementById("txtPlbSlabAmountN").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="Amount is Numeric";
            document.getElementById("lblInnerError").innerHTML="Amount is Numeric";
            
            return false;
            }
        }
       
            if(document.getElementById("txtPlbSlabToN").value!='')
            {
                if(IsDataValid(document.getElementById("txtPlbSlabToN").value,5)==false)
                {
                document.getElementById("lblError").innerHTML="Range To is Numeric";
                document.getElementById("lblInnerError").innerHTML="Range To is Numeric";
               
                return false;
            }
        }
        
    }
    
    
    function validatePlan()
    {
    
    
            if(document.getElementById("hdFlagCreatePlan").value=='0')
            {
            document.getElementById("hdFlagCreatePlan").value="1";
            }
            else if(document.getElementById("hdFlagCreatePlan").value=='1')
            {
            document.getElementById("hdFlagCreatePlan").value="0";
            }
            
           if(document.getElementById("txtCaseName").value=='')
           {
               document.getElementById("lblError").innerHTML="Case Name is Mandatory";
               document.getElementById("lblInnerError").innerHTML="Case Name is Mandatory";
               return false;
           }

        
    }


function ConnectivitySelection()
{
            var type;       
            type = "../Popup/PUSR_ConnectionCategory.aspx"; 
   	        window.open(type,"aa","height=600,width=950,top=30,left=20,scrollbars=1,status=1;");	       
             return false;
}



function validateNo(clnID)
{

var a= document.getElementById(clnID).value;
 
        if(a=='')
        {
        document.getElementById("lblError").innerHTML="Invalid No";
        document.getElementById(clnID).focus();
        return false;
        }
        else
        {
            if(!IsDataValid(a,5))
            {
            document.getElementById("lblError").innerHTML="Invalid No";
            document.getElementById(clnID).focus();
            
                return false;
            }
        }
        document.getElementById("lblError").innerHTML="";
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


/* End of Business case  */

/* Start of Sales Module Validation  */
 // validating visit details
        function ValidateVisitDetails()
        {
            var strValue=""
            document.getElementById('lblError').innerText="";
            document.getElementById('lblErrorVisitDetails').innerText="";
   
            if (document.getElementById('txtPersonMet').value=="")
            {
             document.getElementById('lblError').innerText="Person Met is mandatory.";
             document.getElementById('lblErrorVisitDetails').innerText="Person Met is mandatory.";
             document.getElementById('txtPersonMet').focus();
             return false;
            }
             if (document.getElementById('txtDesignation').value=="")
            {
             document.getElementById('lblError').innerText="Designation is mandatory.";
             document.getElementById('lblErrorVisitDetails').innerText="Designation is mandatory.";
             document.getElementById('txtDesignation').focus();
             return false;
            }
            
             //*********** Validating In-Time *****************************
              if(document.getElementById('txtInTime').value =='')
                 {
                    document.getElementById('lblError').innerText = "In time is mandatory.";	
                    document.getElementById('lblErrorVisitDetails').innerText = "In time is mandatory.";			
                    document.getElementById('txtInTime').focus();
                    return false;                   
                 }
                 else
                 {
                    strValue = document.getElementById('txtInTime').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='In time should contain only digits.'
                        document.getElementById('lblErrorVisitDetails').innerText ='In time should contain only digits.'
                        document.getElementById('txtInTime').focus();
                        return false;
                     }
                     
                     if (strValue.length!=4)
                     {
                        document.getElementById('lblError').innerText ='Invalid In time format.'
                        document.getElementById('lblErrorVisitDetails').innerText ='Invalid In time format.'
                        document.getElementById('txtInTime').focus();
                        return false;
                     }
                     if (parseInt(strValue,10)>2400)
                     {
                        document.getElementById('lblError').innerText ='Invalid In time format.'
                        document.getElementById('lblErrorVisitDetails').innerText ='Invalid In time format.'
                        document.getElementById('txtInTime').focus();
                        return false;
                     }
                     
                 }  
                 //*********** Validating Out-Time *****************************
              if(document.getElementById('txtOutTime').value =='')
                 {
                    document.getElementById('lblError').innerText = "Out time is mandatory.";	
                    document.getElementById('lblErrorVisitDetails').innerText = "Out time is mandatory.";			
                    document.getElementById('txtOutTime').focus();
                    return false;                   
                 }
                 else
                 {
                    var strOutValue = document.getElementById('txtOutTime').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strOutValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='Out time should contain only digits.'
                        document.getElementById('lblErrorVisitDetails').innerText ='Out time should contain only digits.'
                        document.getElementById('txtOutTime').focus();
                        return false;
                     }
                     if (strOutValue.length!=4)
                     {
                        document.getElementById('lblError').innerText ='Invalid Out time format.'
                        document.getElementById('lblErrorVisitDetails').innerText ='Invalid Out time format.'
                        document.getElementById('txtOutTime').focus();
                        return false;
                     }
                     if (parseInt(strOutValue,10)>2400)
                     {
                        document.getElementById('lblError').innerText ='Invalid Out time format.'
                        document.getElementById('lblErrorVisitDetails').innerText ='Invalid Out time format.'
                        document.getElementById('txtOutTime').focus();
                        return false;
                     }
                     if (parseInt(strValue,10)>parseInt(strOutValue,10))
                     {
                        document.getElementById('lblError').innerText ="In time can't be greater than Out time."
                        document.getElementById('lblErrorVisitDetails').innerText ="In time can't be greater than Out time."
                        document.getElementById('txtOutTime').focus();
                        return false;
                     }
                     
                       if (document.getElementById("TxtJointByCall").value.trim().length>1000)
                        {
                             document.getElementById("lblErrorVisitDetails").innerHTML="Joint call remarks can't be greater than 1000 characters."
                             document.getElementById("TxtJointByCall").focus();
                             return false;
                        } 
                                     
                     
                     
                     
                     
                 }                
                ShowPopupVisitDetails('modVisitDetails','pnlProgressVisitDetails');
        }   
        
        //cancel/Edit visit details
        function CancelEditVisitDetails()
        {
                ShowPopupVisitDetails('modVisitDetails','pnlProgressVisitDetails');
            
        }
        function HideShowServiceStrategicCall()
        {
                ShowPopupVisitDetails('modVisitDetails','pnlProgressVisitDetails');
            
        }
        function HideShowStrategicVisits()
        {
                ShowPopupVisitDetails('modStrategicVisits','pnlProgressStrategicVisits');
            
        }
        
        
        
        
          // validating service call
          function ValidateServiceCall()
          {
          
         
                document.getElementById('lblError').innerText="";
                document.getElementById('lblErrorServiceCall').innerText="";
              //*********** Validating Department  *****************************
                var cboDepartment=document.getElementById('ddlDepartment');
                if(cboDepartment.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Department is mandatory.'
                    document.getElementById('lblErrorServiceCall').innerText ='Department is mandatory.'
                    cboDepartment.focus();
                    return false;
                }
                
                if(document.getElementById('txtDetailedDiscussion').value =='')
                 {
                    document.getElementById('lblError').innerText = "Detailed Disc/Issue Reported is mandatory.";	
                    document.getElementById('lblErrorServiceCall').innerText = "Detailed Disc/Issue Reported is mandatory.";			
                    document.getElementById('txtDetailedDiscussion').focus();
                    return false;                   
                 }
                
                //*********** Validating Service Satus  *************************
                var cboServiceStatus=document.getElementById('ddlServiceStatus');
                var strStatus=document.getElementById('ddlServiceStatus').value;
                if(cboServiceStatus.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Service staus is mandatory.'
                    document.getElementById('lblErrorServiceCall').innerText ='Service staus is mandatory.'
                    cboServiceStatus.focus();
                    return false;
                }
                
                   //*********** Validating  Closer Date  *****************              
                 
                 
                 if (strStatus.split("|")[1]=="1")
                 {
                     if(document.getElementById('txtCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('lblErrorServiceCall').innerText ="Closure date is mandatory.";
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                                document.getElementById('lblErrorServiceCall').innerText = "Closure date is not valid.";
                               return false;  
                            }  
                    }   
                 }
                 else
                 {
                 
                     if (document.getElementById('dlstAssignedTo').value == '')
                      {
                        document.getElementById('lblError').innerText = "Assigned To is mandatory.";
                        document.getElementById('lblErrorServiceCall').innerText = "Assigned To is mandatory.";
                        return false;
                        
                      }
                 
                               
                        
                 }
                    //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('lblErrorServiceCall').innerText = "Target Closure date is mandatory.";
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('lblErrorServiceCall').innerText = "Target Closure date is not valid.";
                               return false;  
                            }  
                      
                      }   
//                       if (document.getElementById('txtFollowUpRemarks').value == '')
//                      {
//                        document.getElementById('lblError').innerText = "Follow Up Remarks is mandatory.";
//                        document.getElementById('lblErrorServiceCall').innerText = "Follow Up Remarks is mandatory.";
//                        return false;
                        
//                      }
                             
                 
                ShowPopupVisitDetails('modServiceCall','pnlProgressServiceCall');
          }
          
           //cancel/Edit Service Call
        function CancelEditServiceCall()
        {
                ShowPopupVisitDetails('modServiceCall','pnlProgressServiceCall');
            
        }
        
          
          // validating retention 
          function ValidateRetention()
          {
            document.getElementById('lblError').innerText="";
            document.getElementById('lblErrRetention').innerText="";

              //*********** Validating Existing Deal  *****************************
             /*
              if(document.getElementById('txtExistingDeal').value =='')
                 {
                    document.getElementById('lblError').innerText = "Existing deal is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "Existing deal is mandatory.";			
                    document.getElementById('txtExistingDeal').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtExistingDeal').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='Existing deal should contain only digits.'
                        document.getElementById('lblErrRetention').innerText ='Existing deal should contain only digits.'
                        document.getElementById('txtExistingDeal').focus();
                        return false;
                     }
                 }
                */ 
                  //*********** Validating CPS *****************************
              if(document.getElementById('txtCPS').value =='')
                 {
                    document.getElementById('lblError').innerText = "CPS is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "CPS is mandatory.";			
                    document.getElementById('txtCPS').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtCPS').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='CPS should contain only digits.'
                        document.getElementById('lblErrRetention').innerText ='CPS should contain only digits.'
                        document.getElementById('txtCPS').focus();
                        return false;
                     }
                 }
                //*********** Validating Retention Reason *************************
                var cboRetentionReason=document.getElementById('ddlRetentionReason');
                if(cboRetentionReason.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Retention reason is mandatory.'
                    document.getElementById('lblErrRetention').innerText ='Retention reason is mandatory.'
                    cboRetentionReason.focus();
                    return false;
                }
                
                //*********** Validating Retention Status *************************
                var cboRetentionStatus=document.getElementById('ddlRetentionStatus');
               
                if(cboRetentionStatus.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Retention status is mandatory.'
                    document.getElementById('lblErrRetention').innerText ='Retention status is mandatory.'
                    cboRetentionStatus.focus();
                    return false;
                }
                
                 //*********** Validating New CPS *****************************
              if(document.getElementById('txtNewCPS').value =='')
                 {
                    document.getElementById('lblError').innerText = "New CPS is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "New CPS is mandatory.";			
                    document.getElementById('txtNewCPS').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtNewCPS').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='New CPS should contain only digits.'
                        document.getElementById('lblErrRetention').innerText ='New CPS should contain only digits.'
                        document.getElementById('txtNewCPS').focus();
                        return false;
                     }
                 }
                 
                 //*********** Validating 1A Approved New Deal *****************************
              if(document.getElementById('txt1AApprovedNewDeal').value =='')
                 {
                    document.getElementById('lblError').innerText = "1A approved new deal is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "1A approved new deal is mandatory.";			
                    document.getElementById('txt1AApprovedNewDeal').focus();
                    return false;                   
                 }
//                 else
//                 {
//                    var strValue = document.getElementById('txt1AApprovedNewDeal').value
//                    reg = new RegExp("^[0-9]+$"); 
//                    if(reg.test(strValue) == false) 
//                    {
//                        document.getElementById('lblError').innerText ='1A approved new deal should contain only digits.'
//                        document.getElementById('lblErrRetention').innerText ='1A approved new deal should contain only digits.'
//                        document.getElementById('txt1AApprovedNewDeal').focus();
//                        return false;
//                     }
//                 }
           
              //*********** Validating Detailed Disc / Issue Reported *****************************
//              if(document.getElementById('txtRetentionDetailedDiscussion').value =='')
//                 {
//                    document.getElementById('lblError').innerText = "Detailed Disc / Issue Reported is mandatory.";	
//                    document.getElementById('lblErrRetention').innerText = "Detailed Disc / Issue Reported is mandatory.";			
//                    document.getElementById('txtRetentionDetailedDiscussion').focus();
//                    return false;                   
//                 }
               
                 //var cboRetentionStatusValue=document.getElementById('ddlRetentionStatus').value;
               
                var cboRetStatus=document.getElementById('ddlRetentionStatus').value;
                var cboRetStatusStatus1=cboRetStatus.split("|")[0];               
                var cboRetentionStatusValue=cboRetStatusStatus1;
               
                if (cboRetentionStatusValue == document.getElementById('hdRetentionChk').value)
                {
                  if(document.getElementById('txtRetentionSignedOn').value == '')
                    {
                       document.getElementById('lblError').innerText = "Signed on is mandatory.";
                       document.getElementById('lblErrRetention').innerText = "Signed on is mandatory.";			
                       document.getElementById('txtRetentionSignedOn').focus();
                       return false;  
                    }

                }
                 
                  //*********** Validating Signed On Date  *****************
                if(document.getElementById('txtRetentionSignedOn').value != '')
                {
                    if (isDate(document.getElementById('txtRetentionSignedOn').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Signed on is not valid.";
                       document.getElementById('lblErrRetention').innerText = "Signed on is not valid.";			
                       document.getElementById('txtRetentionSignedOn').focus();
                       return false;  
                    }  
                }
            
                 //*********** Validating Target- Segs / % of Business *****************************
              if(document.getElementById('txtRetentionTargetSegs').value =='')
                 {
                    document.getElementById('lblError').innerText = "Target-Segs / % of Business is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "Target-Segs / % of Business is mandatory.";			
                    document.getElementById('txtRetentionTargetSegs').focus();
                    return false;                   
                 }
//                 else
//                 {
//                    var strValue = document.getElementById('txtRetentionTargetSegs').value
//                    reg = new RegExp("^[0-9]+$"); 
//                    if(reg.test(strValue) == false) 
//                    {
//                        document.getElementById('lblError').innerText ='Target-Segs / % of Business should contain only digits.'
//                        document.getElementById('lblErrRetention').innerText ='Target-Segs / % of Business should contain only digits.'
//                        document.getElementById('txtRetentionTargetSegs').focus();
//                        return false;
//                     }
//                 }


//--------------------- Start Added On 13 th July by ------------

    if(document.getElementById('DrpRetDept').value == '')
     {
        document.getElementById('lblError').innerText = "Department is mandatory.";
        document.getElementById('lblErrRetention').innerText ="Department is mandatory.";
        return false;  
     }
        if(document.getElementById('DrpRetAssignedTo').value == '')
     {
        document.getElementById('lblError').innerText = "Assigned to is mandatory.";
        document.getElementById('lblErrRetention').innerText ="Assigned to is mandatory.";
        return false;  
     }
       
     var cboRetStatusStatus2=cboRetStatus.split("|")[1];    
     if (cboRetStatusStatus2=="1")
                 {
                     if(document.getElementById('txtRetCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('lblErrRetention').innerText ="Closure date is mandatory.";
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtRetCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtRetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                               document.getElementById('lblErrRetention').innerText = "Closure date is not valid.";
                               return false;  
                            }  
                    }   
                 }
                 else
                 {             
                        
                 }
                    //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtRetTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('lblErrRetention').innerText = "Target Closure date is mandatory.";
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtRetTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('lblErrRetention').innerText = "Target Closure date is not valid.";
                               return false;  
                            }  
                      
                      }
                    
                         
                       if (document.getElementById("TxtRetFollowup").value.trim().length>1000)
                        {
                             document.getElementById("lblError").innerHTML="Follow ups greater than 1000 characters."
                             document.getElementById('lblErrRetention').innerText ="Follow ups greater than 1000 characters."
                             document.getElementById("TxtRetFollowup").focus();
                             return false;
                        }  

//--------------------- End Added On 13 th July by ------------





          ShowPopupVisitDetails('modRetention','pnlProgressRetention');
          }
          
           //cancel/Edit retention
        function CancelEditRetention()
        {
                ShowPopupVisitDetails('modRetention','pnlProgressRetention');
            
        }
          
          // validating target
          /*
           
           
            Target- Segs / % of Business txtTargetTargetSegs
          
          */
          
          function ValidateTarget()
          {
            document.getElementById('lblError').innerText="";
            document.getElementById('lblErrTarget').innerText="";
             //*********** Validating 1A Approved New Deal *****************************
              if(document.getElementById('txtTarget1AApprovedNewDeal').value =='')
                 {
                    document.getElementById('lblError').innerText = "1A approved new deal is mandatory.";	
                    document.getElementById('lblErrTarget').innerText = "1A approved new deal is mandatory.";			
                    document.getElementById('txtTarget1AApprovedNewDeal').focus();
                    return false;                   
                 }
//                 else
//                 {
//                    var strValue = document.getElementById('txtTarget1AApprovedNewDeal').value
//                    reg = new RegExp("^[0-9]+$"); 
//                    if(reg.test(strValue) == false) 
//                    {
//                        document.getElementById('lblError').innerText ='1A approved new deal should contain only digits.'
//                        document.getElementById('lblErrTarget').innerText ='1A approved new deal should contain only digits.'
//                        document.getElementById('txtTarget1AApprovedNewDeal').focus();
//                        return false;
//                     }
//                 }
                 
                   //*********** Validating CPS *****************************
              if(document.getElementById('txtTargetCPS').value =='')
                 {
                    document.getElementById('lblError').innerText = "CPS is mandatory.";	
                    document.getElementById('lblErrTarget').innerText = "CPS is mandatory.";			
                    document.getElementById('txtTargetCPS').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtTargetCPS').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='CPS should contain only digits.'
                        document.getElementById('lblErrTarget').innerText ='CPS should contain only digits.'
                        document.getElementById('txtTargetCPS').focus();
                        return false;
                     }
                 }
                 
                 //*********** Validating Target Status *************************
                var cboTargetStatus=document.getElementById('ddlTargetStatus');
                if(cboTargetStatus.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Target status is mandatory.'
                    document.getElementById('lblErrTarget').innerText ='Target status is mandatory.'
                    cboTargetStatus.focus();
                    return false;
                }
                
                 //*********** Validating Detailed Disc / Issue Reported *****************************
//              if(document.getElementById('txtTargetDetailedDiscussion').value =='')
//                 {
//                    document.getElementById('lblError').innerText = "Detailed Disc / Issue Reported is mandatory.";	
//                    document.getElementById('lblErrTarget').innerText = "Detailed Disc / Issue Reported is mandatory.";			
//                    document.getElementById('txtTargetDetailedDiscussion').focus();
//                    return false;                   
//                 }
                  //*********** Validating Signed On Date  *****************
                  
                 // var cboTargetStatusValue=document.getElementById('ddlTargetStatus').value;
                  
                    var cboTarStatus=document.getElementById('ddlTargetStatus').value;
                    var cboTarStatus1=cboTarStatus.split("|")[0];               
                    var cboTargetStatusValue=cboTarStatus1;
                  
                  
                if (cboTargetStatusValue == document.getElementById('hdTargetChk').value)
                {
                  if(document.getElementById('txtTargetSignedOn').value == '')
                    {
                       document.getElementById('lblError').innerText = "Signed on is mandatory.";
                       document.getElementById('lblErrTarget').innerText = "Signed on is mandatory.";			
                       document.getElementById('txtTargetSignedOn').focus();
                       return false;  
                    }

                }
                  
                if(document.getElementById('txtTargetSignedOn').value != '')
                {
                    if (isDate(document.getElementById('txtTargetSignedOn').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Signed on is not valid.";
                       document.getElementById('lblErrTarget').innerText = "Signed on is not valid.";			
                       document.getElementById('txtTargetSignedOn').focus();
                       return false;  
                    }  
                }
            
                 //*********** Validating Target- Segs / % of Business *****************************
              if(document.getElementById('txtTargetTargetSegs').value =='')
                 {
                    document.getElementById('lblError').innerText = "Target-Segs / % of Business is mandatory.";	
                    document.getElementById('lblErrTarget').innerText = "Target-Segs / % of Business is mandatory.";			
                    document.getElementById('txtTargetTargetSegs').focus();
                    return false;                   
                 }
//                 else
//                 {
//                    var strValue = document.getElementById('txtTargetTargetSegs').value
//                    reg = new RegExp("^[0-9]+$"); 
//                    if(reg.test(strValue) == false) 
//                    {
//                        document.getElementById('lblError').innerText ='Target-Segs / % of Business should contain only digits.'
//                        document.getElementById('lblErrTarget').innerText ='Target-Segs / % of Business should contain only digits.'
//                        document.getElementById('txtTargetTargetSegs').focus();
//                        return false;
//                     }
//                 }


//--------------------- Start Added On 13 th July by ------------
     if(document.getElementById('DrpTargetDept').value == '')
     {
        document.getElementById('lblError').innerText = "Department is mandatory.";
        document.getElementById('lblErrTarget').innerText ="Department is mandatory.";
        return false;  
     }
        if(document.getElementById('DrpTargetAssignedTo').value == '')
     {
        document.getElementById('lblError').innerText = "Assigned to is mandatory.";
        document.getElementById('lblErrTarget').innerText ="Assigned to is mandatory.";
        return false;  
     }
       
     var cboTarStatus2=cboTarStatus.split("|")[1];    
     if (cboTarStatus2=="1")
                 {
                     if(document.getElementById('txtTarCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('lblErrTarget').innerText ="Closure date is mandatory.";
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtTarCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtTarCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                                document.getElementById('lblErrTarget').innerText = "Closure date is not valid.";
                               return false;  
                            }  
                    }   
                 }
                 else
                 {             
                        
                 }
                    //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtTarTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('lblErrTarget').innerText = "Target Closure date is mandatory.";
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtTarTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('lblErrTarget').innerText = "Target Closure date is not valid.";
                               return false;  
                            }  
                      
                      } 
                      
                  
                      
                        
                       if (document.getElementById("TxtTarFollowup").value.trim().length>1000)
                        {
                             document.getElementById("lblError").innerHTML="Follow ups greater than 1000 characters."
                             document.getElementById('lblErrTarget').innerText ="Follow ups greater than 1000 characters."
                             document.getElementById("TxtTarFollowup").focus();
                             return false;
                        }  

//--------------------- End Added On 13 th July by ------------










            
          ShowPopupVisitDetails('modTarget','pnlProgressTarget');
          }
          
           //cancel/Edit Target
        function CancelEditTarget()
        {
                ShowPopupVisitDetails('modTarget','pnlProgressTarget');
            
        }
          
          // validating air non-air
          function ValidateAirNonAir()
          {
         
            document.getElementById('lblError').innerText="";
            document.getElementById('lblErrAirNonAirProduct').innerText="";
            
              //*********** Validating Product Name *************************
                var cboProductName=document.getElementById('ddlProductName');
                if(cboProductName.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Product name is mandatory.'
                    document.getElementById('lblErrAirNonAirProduct').innerText ='Product name is mandatory.'
                    cboProductName.focus();
                    return false;
                }
            
             //*********** Validating Revenue *****************************
              if(document.getElementById('txtRevenue').value =='')
                 {
                    document.getElementById('lblError').innerText = "Revenue is mandatory.";	
                    document.getElementById('lblErrAirNonAirProduct').innerText = "Revenue is mandatory.";			
                    document.getElementById('txtRevenue').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtRevenue').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='Revenue should contain only digits.'
                        document.getElementById('lblErrAirNonAirProduct').innerText ='Revenue should contain only digits.'
                        document.getElementById('txtRevenue').focus();
                        return false;
                     }
                 }
                 
                  //*********** Validating Status *************************
                var cboAirNonAirStatus=document.getElementById('ddlAirNonAirStatus');
                if(cboAirNonAirStatus.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Status is mandatory.'
                    document.getElementById('lblErrAirNonAirProduct').innerText ='Status is mandatory.'
                    cboAirNonAirStatus.focus();
                    return false;
                }
                
                //*********** Validating Signed On Date  *****************
                
                //var cboAirNonAirStatusValue=document.getElementById('ddlAirNonAirStatus').value;
                var cboAirNonAirStatus=document.getElementById('ddlAirNonAirStatus').value;
                var cboAirNonAirStatus1=cboAirNonAirStatus.split("|")[0];               
                var cboAirNonAirStatusValue=cboAirNonAirStatus1;//document.getElementById('cboAirNonAirStatus').value; 
               
                
                if (cboAirNonAirStatusValue == document.getElementById('hdAirNonAirChk').value)
                {
                  if(document.getElementById('txtAirNonAirSignedOn').value == '')
                    {
                       document.getElementById('lblError').innerText = "Instalation Date is mandatory.";
                       document.getElementById('lblErrAirNonAirProduct').innerText = "Instalation Date is mandatory.";			
                       document.getElementById('txtAirNonAirSignedOn').focus();
                       return false;  
                    }

                }
                
                
                if(document.getElementById('txtAirNonAirSignedOn').value != '')
                {
                    if (isDate(document.getElementById('txtAirNonAirSignedOn').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Instalation Date is not valid.";
                       document.getElementById('lblErrAirNonAirProduct').innerText = "Instalation Date is not valid.";			
                       document.getElementById('txtAirNonAirSignedOn').focus();
                       return false;  
                    }  
                }
                
                  //*********** Validating Detailed Disc / Issue Reported *****************************
//              if(document.getElementById('txtAirNonAirDetailedDiscussion').value =='')
//                 {
//                    document.getElementById('lblError').innerText = "Detailed Disc / Issue Reported is mandatory.";	
//                    document.getElementById('lblErrAirNonAirProduct').innerText = "Detailed Disc / Issue Reported is mandatory.";			
//                    document.getElementById('txtAirNonAirDetailedDiscussion').focus();
//                    return false;                   
//                 }



//--------------------- Start Added On 13 th July by ------------
     if(document.getElementById('DrpAirNonAirDept').value == '')
     {
        document.getElementById('lblError').innerText = "Department is mandatory.";
        document.getElementById('lblErrAirNonAirProduct').innerText ="Department is mandatory.";
        return false;  
     }
        if(document.getElementById('DrpAirNonAirAssignedTo').value == '')
     {
        document.getElementById('lblError').innerText = "Assigned to is mandatory.";
        document.getElementById('lblErrAirNonAirProduct').innerText ="Assigned to is mandatory.";
        return false;  
     }
       
     var cboAirNonAirStatus2=cboAirNonAirStatus.split("|")[1];    
     if (cboAirNonAirStatus2=="1")
                 {
                     if(document.getElementById('txtAirCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('lblErrAirNonAirProduct').innerText ="Closure date is mandatory.";
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtAirCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtAirCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                               document.getElementById('lblErrAirNonAirProduct').innerText = "Closure date is not valid.";
                               return false;  
                            }  
                    }   
                 }
                 else
                 {             
                        
                 }
                    //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtAirTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('lblErrAirNonAirProduct').innerText = "Target Closure date is mandatory.";
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtAirTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('lblErrAirNonAirProduct').innerText = "Target Closure date is not valid.";
                               return false;  
                            }  
                      
                      }   
                      
                    
                      
                      
                       if (document.getElementById("TxtAirNonAirFollowup").value.trim().length>1000)
                        {
                             document.getElementById("lblError").innerHTML="Follow ups greater than 1000 characters."
                             document.getElementById('lblErrAirNonAirProduct').innerText ="Follow ups greater than 1000 characters."
                             document.getElementById("TxtAirNonAirFollowup").focus();
                             return false;
                        }  

//--------------------- End Added On 13 th July by ------------


                 
         ShowPopupVisitDetails('modAirNonAir','pnlProgressAirNonAir');
          }
          
           //cancel/Edit AirNonAir
        function CancelEditAirNonAir()
        {
                ShowPopupVisitDetails('modAirNonAir','pnlProgressAirNonAir');
            
        }
         
          function ShowPopupVisitDetails(modalName,ProgressName)
        {
            try
            {
                var modal = $find(modalName); 
                if (modal !=null)
                {
                    document.getElementById(ProgressName).style.height='150px';
                    if (document.getElementById ('imgLoad')!=null)
                     {
                        document.getElementById('imgLoad').style.display ="none";
                     }
                     
                    modal.show(); 
                 }   
            }
             catch(err){alert(err)}
         
        } 
          
 function ValidatePageVisitDetails()
 {
    var grdVisitDetails=document.getElementById("gvVisitDetails");
    if (grdVisitDetails != null)
    {
        if (grdVisitDetails.rows.length <=1)
        {
            document.getElementById("lblError").innerText="Please add visit details.";
            document.getElementById('lblErrorVisitDetails').innerText="Please add visit details.";
            return false;
        }
     }
     else
     {
            document.getElementById("lblError").innerText="Please add visit details.";
            document.getElementById('lblErrorVisitDetails').innerText="Please add visit details.";
            return false;
     }
 
     if (document.getElementById("chkServiceCall").checked==false && document.getElementById("chkStrategicCall").checked==false)
     {
            document.getElementById("lblError").innerText="Visit sub type is mandatory.";
            return false;
     }
 }


function ValidateOpenItemsServiceCall()
{
     //*********** Validating Department  *****************************
                document.getElementById('lblError').innerText='';
                if(document.getElementById('txtDepartment').value == '')
                {
                    document.getElementById('lblError').innerText ='Department is mandatory.'
                    return false;
                }
                
                if(document.getElementById('txtDetailedDiscussion').value == '')
                {
                    document.getElementById('lblError').innerText ='Detailed Disc Issue Reported is mandatory.'
                    return false;
                }
                
                               
                 //*********** Validating  Closer Date  *****************
                 var strStatus=document.getElementById('ddlServiceStatus').value;
                 if(strStatus == '')
                 {
                    document.getElementById('lblError').innerText = "Status is mandatory.";
                    document.getElementById('ddlServiceStatus').focus();
                    return false;  
                 }
                 
                 if (strStatus.split("|")[1]=="1")
                 {
                     if(document.getElementById('txtCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('txtCloserDate').focus();
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                               document.getElementById('txtCloserDate').focus();
                               return false;  
                            }  
                    }   
                 }
                 else
                 {
                      
                      
                      //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('txtTargetCloserDate').focus();
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('txtTargetCloserDate').focus();                               
                               return false;  
                            }  
                      
                      }        
                      
                 }
                  if(document.getElementById('txtFollowUpRemarks').value == '')
                {
                    document.getElementById('lblError').innerText ='Follow-Up Remarksis mandatory.'
                    document.getElementById('txtFollowUpRemarks').focus();
                    return false;
                }
                 
                 
    ShowPopupVisitDetails('modOpenIemsServiceCall','pnlProgressOpenItemsServiceCall');
    
}

function CancelEditOpenItemsServiceCall()
{
               ShowPopupVisitDetails('modOpenIemsServiceCall','pnlProgressOpenItemsServiceCall');
    
}
/* End of Sales Module Validation  */