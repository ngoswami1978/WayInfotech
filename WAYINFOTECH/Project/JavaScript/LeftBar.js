// JScript File

var XmlHttp;
var AjaxServerPageName;
AjaxServerPageName = "GetServerResponse.aspx";
var CountRow;
var strID;
function CreateXmlHttp()
{
	try
	{
		XmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
	}
	catch(e)
	{
		try
		{
			XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
		} 
		catch(oc)
		{
			XmlHttp = null;
		}
	}
	if(!XmlHttp && typeof XMLHttpRequest != "undefined") 
	{
		XmlHttp = new XMLHttpRequest();
	}
}

function Change() 
{
	var requestUrl = AjaxServerPageName + "?ID=" + encodeURIComponent(strID);
	CreateXmlHttp();
	if(XmlHttp)
	{
		XmlHttp.onreadystatechange = HandleResponse;
		XmlHttp.open("GET", requestUrl,  true);
		XmlHttp.send(null);		
	}
}

function HandleResponse()
{
   	if(XmlHttp.readyState == 4)
	{
	   	if(XmlHttp.status == 200 && XmlHttp.statusText == "OK" )
		{	
        
		   	ClearAndSetMenuItems(XmlHttp.responseXML.documentElement);
		   	if (document.getElementById('NavDiv' + strID).innerHTML=="")
		   	{
		   	    ClearAndSetMenuItems(XmlHttp.responseXML.documentElement);
		   	}
		}
		else
		{
			alert("There was a problem retrieving data from the server." );
		}
	}
}

function ClearAndSetMenuItems(XNode)
{
    try
    {
       
        if (XNode != null)
        {
         /*   var stateNodes = XNode.getElementsByTagName('Item');
            _String = "<table border='0' cellpadding='0' cellspacing='0' style='width: 159px; class='sub_menu'>";
	        for (var count = 0; count < stateNodes.length; count++)
	        {
   		        var Name;
   		        var Link;
   		        Name = stateNodes[count].getAttribute("NAME");
   		        Link = stateNodes[count].getAttribute("LINK");
   		        _String += "<td align='right' style='width: 10px; valign='middle' class='sub_menu'></td><td align='right' style='width: 10px; valign='bottom' class='sub_menu'><img src='Images/ArrowNew.gif' /></td><td align='left' style='width: 170px;  valign='top' class='sub_menu'><a class='sub_menu'  href='"+ Link +"' target='mainframe'>"+Name+" </a></td></tr>";
   	        }
   	        _String +="</table>"; */
   	        
   	        var stateNodes = XNode.getElementsByTagName('Item');
            _String = "<ul>";
            
	        for (var count = 0; count < stateNodes.length; count++)
	        {
   		        var Name;
   		        var Link;
   		        Name = stateNodes[count].getAttribute("NAME");
   		        Link = stateNodes[count].getAttribute("LINK");
   		        /*_String += "<td align='right' style='width: 10px; valign='middle' class='sub_menu'></td><td align='right' style='width: 10px; valign='bottom' class='sub_menu'><img src='Images/ArrowNew.gif' /></td><td align='left' style='width: 170px;  valign='top' class='sub_menu'><a class='sub_menu'  href='"+ Link +"' target='mainframe'>"+Name+" </a></td></tr>";*/
   		        _String += "<li><a class='sub_menu'  href='"+ Link +"' target='mainframe'>"+Name+" </a></li>";
   	        }
   	        _String +="</ul>";
   	        
   	        
	        var str = _String; 
	        document.getElementById('NavDiv' + strID).innerHTML = str;
	    }
	}
	catch(err){}
	//ShowLoading(strID , "Hide");
}

function GetInnerText (node)
{
	 return (node.textContent || node.innerText || node.text) ;
}


function GetData(strID1)
{
    strID = strID1;
    ColapseAll(strID);
    try
    {
        if(document.getElementById('NavDiv' + strID).style.display == "none")
        {
            SetDefaultImage_Src();
            //document.getElementById('NavImg' + strID ).src = "Images/up.jpg";
            document.getElementById('NavImg' + strID ).className = "moduleDivup";
           
            document.getElementById('NavDiv' + strID).style.display = "block";
            if (document.getElementById('NavDiv' + strID).innerHTML == "" )
            {
                //ShowLoading(strID , "Show");
                Change();
            }
        }
        else if(document.getElementById('NavDiv' + strID).style.display == "block")
        {
            document.getElementById('NavDiv' + strID).style.display = "none" ;
            SetDefaultImage_Src();
        }
    }
     catch(err)  {     }  
}
function SetDefaultImage_Src()
{
    for (var i=1 ; i<= CountRow ; i++)
    {
        try
        {
             //document.getElementById('NavImg' + i ).src = "Images/down.jpg";
             document.getElementById('NavImg' + i).className = "moduleDivdown";
        }
        catch(err)  {     }  
    }
}

        
function ShowLoading(ThisRow , State)
{
    try
    {
    
        var This='none' , All='none';
        if (State == "Show")
        {
            This = "block"
        }

        for (var i=1 ; i<= CountRow ; i++)
        { 
             try
            {
                if (i != ThisRow)
                    document.getElementById('Loading' + i).style.display = All ;
                else
                {
                    document.getElementById('Loading' + i).style.display = This ;
                }
            }
            catch(err)  {     }  
        }
   }
   catch(err)   {  }
     
}
function ColapseAll(ThisRow)
{
    
    for (var i=1 ; i<= CountRow ; i++)
    { 
        if (i != ThisRow)
        {
            try
            {
            document.getElementById('NavDiv' + i).style.display = "none";
            }
            catch(err)  {     }
        }
    }
} 