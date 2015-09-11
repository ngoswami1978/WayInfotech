<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_HardwareInstallation.aspx.vb"
    MaintainScrollPositionOnPostback="true" Inherits="Inventory_INVSR_Installations" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script language="javascript" type="text/javascript">

//   function AgencyValidation()
//     {
//        document.getElementById("hidLcode").value="";
//         document.getElementById("chbWholeGroup").disabled=true;
//    	document.getElementById("chbWholeGroup").checked=false;
//     }


function SetValueForExport()
{
     document.getElementById("hdExport").value="E";
     return true;
}

function setOrderTypeValue()
{    
    //alert(itemOrder);
    var itemOrder=document.getElementById("drpOrderType").selectedIndex;
    var textOrder=document.getElementById("drpOrderType").options[itemOrder].text ;
    document.getElementById("hdOrderTypeSelectedIndex").value =itemOrder;
    //alert(itemOrder);
}
function gotop1(ddlname)
     {
    
     if (event.keyCode==46 )
     {
        document.getElementById(ddlname).selectedIndex=0;
         document.getElementById("hdOrderTypeSelectedIndex").value=0;
     }
     }

 function ActDeAct()
     {
    
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
      
        if(whichASC!=9 && whichASC!=18 && whichASC!=13)
        {
        document.getElementById("hidLcode").value="";
        document.getElementById("hdAgencyName").value="";
        document.getElementById("ChkGrpProductivity").disabled=true;
        document.getElementById("ChkGrpProductivity").checked=false;
        }
    	
     }
   
 function CheckValidation()
      {
       
  if(document.getElementById('<%=txtInstallationFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtInstallationFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation From is not valid.";			
	       document.getElementById('<%=txtInstallationFrom.ClientId%>').focus();
	       return(false);  
        }
        }  
        



if(document.getElementById('<%=txtInstallationTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtInstallationTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation To is not valid.";			
	       document.getElementById('<%=txtInstallationTo.ClientId%>').focus();
	       return(false);  
        }
        }  
        
        
        if(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value,"dd/MM/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Purchase From is not valid.";			
	       document.getElementById('<%=txtPurchaseFrom.ClientId%>').focus();
	       return(false);  
        }
        }  
        if(document.getElementById('<%=txtPurchaseTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtPurchaseTo.ClientId%>').value,"dd/MM/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Purchase To is not valid.";			
	       document.getElementById('<%=txtPurchaseTo.ClientId%>').focus();
	       return(false);  
        }
        }  
        
         if(document.getElementById('<%=txtInstallationFrom.ClientId%>').value != '' && document.getElementById('<%=txtInstallationTo.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtInstallationFrom.ClientId%>').value,"dd/MM/yyyy",document.getElementById('<%=txtInstallationTo.ClientId%>').value,"dd/MM/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation From should be lesser than Date of Installation To.";			
	       document.getElementById('<%=txtInstallationTo.ClientId%>').focus();
	       return(false);  
        }
       }  
        if(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value != '' && document.getElementById('<%=txtPurchaseTo.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value,"dd/MM/yyyy",document.getElementById('<%=txtPurchaseTo.ClientId%>').value,"dd/MM/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Purchase From should be lesser than Date of Purchase To.";			
	       document.getElementById('<%=txtPurchaseTo.ClientId%>').focus();
	       return(false);  
        }
       }  
        
//         if(document.getElementById('<%=txtInstallationFrom.ClientId%>').value>document.getElementById('<%=txtInstallationTo.ClientId%>').value)
//         {
//         document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation From should be lesser than Date of Installation To .";			
//	       document.getElementById('<%=txtInstallationFrom.ClientId%>').focus();
//	       return(false);  
//         }
         
          
//         if(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value>document.getElementById('<%=txtPurchaseTo.ClientId%>').value)
//         {
//         document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Purchase From should be lesser than Date of Purchase To .";			
//	       document.getElementById('<%=txtPurchaseFrom.ClientId%>').focus();
//	       return(false);  
//         }
 
  }


function CheckValidationFroExport()
      {
       
  if(document.getElementById('<%=txtInstallationFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtInstallationFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation From is not valid.";			
	       document.getElementById('<%=txtInstallationFrom.ClientId%>').focus();
	       return(false);  
        }
        }  
        



if(document.getElementById('<%=txtInstallationTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtInstallationTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation To is not valid.";			
	       document.getElementById('<%=txtInstallationTo.ClientId%>').focus();
	       return(false);  
        }
        }  
        
        
        if(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value,"dd/MM/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Purchase From is not valid.";			
	       document.getElementById('<%=txtPurchaseFrom.ClientId%>').focus();
	       return(false);  
        }
        }  
        if(document.getElementById('<%=txtPurchaseTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtPurchaseTo.ClientId%>').value,"dd/MM/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Purchase To is not valid.";			
	       document.getElementById('<%=txtPurchaseTo.ClientId%>').focus();
	       return(false);  
        }
        }  
        
         if(document.getElementById('<%=txtInstallationFrom.ClientId%>').value != '' && document.getElementById('<%=txtInstallationTo.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtInstallationFrom.ClientId%>').value,"dd/MM/yyyy",document.getElementById('<%=txtInstallationTo.ClientId%>').value,"dd/MM/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation From should be lesser than Date of Installation To.";			
	       document.getElementById('<%=txtInstallationTo.ClientId%>').focus();
	       return(false);  
        }
       }  
        if(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value != '' && document.getElementById('<%=txtPurchaseTo.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value,"dd/MM/yyyy",document.getElementById('<%=txtPurchaseTo.ClientId%>').value,"dd/MM/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Purchase From should be lesser than Date of Purchase To.";			
	       document.getElementById('<%=txtPurchaseTo.ClientId%>').focus();
	       return(false);  
        }
       }  
        
//         if(document.getElementById('<%=txtInstallationFrom.ClientId%>').value>document.getElementById('<%=txtInstallationTo.ClientId%>').value)
//         {
//         document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation From should be lesser than Date of Installation To .";			
//	       document.getElementById('<%=txtInstallationFrom.ClientId%>').focus();
//	       return(false);  
//         }
         
          
//         if(document.getElementById('<%=txtPurchaseFrom.ClientId%>').value>document.getElementById('<%=txtPurchaseTo.ClientId%>').value)
//         {
//         document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Purchase From should be lesser than Date of Purchase To .";			
//	       document.getElementById('<%=txtPurchaseFrom.ClientId%>').focus();
//	       return(false);  
//         }
 
  }


  function PopupAgencyPage()
        {

         var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
   	        //chkWholeGroup
   	        //document.getElementById("chbWholeGroup").disabled = false;
            return false;
        }
        function PopupEquipment()        
        {
       
         var type;
         type = "../Popup/PUSR_InvEquipment.aspx?Popup=T" ;
         var strReturn; 
         if (window.showModalDialog)
         {     
         strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
         }
         else
         {     
         strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1,status=1');       
         }	   
         if (strReturn != null)
         {
         var sPos = strReturn.split('|'); 
         document.getElementById('<%=txtEquipmentType.ClientID%>').value=sPos[0];
          }  
   	      return false;
        }
         function InstallationReset()
        {
        document.getElementById('hdTabType').value='0';
        document.getElementById('<%=ChkGrpProductivity.ClientID%>').checked=false;
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtAgencyName").value=""; 
        document.getElementById("txtEquipmentType").value="";
        document.getElementById("txtInstallationFrom").value="";
        document.getElementById("txtInstallationTo").value="";
        document.getElementById("txtPurchaseFrom").value="";
        document.getElementById("txtPurchaseTo").value="";
        document.getElementById("txtVendorSerialNo").value="";
        document.getElementById("drpCity").selectedIndex=0;
        document.getElementById("drpCountry").selectedIndex=0;      
        document.getElementById("drpAoffice").selectedIndex=0; 
        document.getElementById("ddlRegion").selectedIndex=0; 
        document.getElementById("drpEquipmentGroup").selectedIndex=0;
        document.getElementById("drpOnlineStatus").selectedIndex=0;
        document.getElementById("txtAgencyName").focus(); 
        document.getElementById("hidLcode").value="";
        document.getElementById("hdAgencyName").value="";
        }
        
function ColorMethod(id,total)
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
      
       document.getElementById('lblPanelClick').value =id; 
       
    
//       var pgStatus=document.getElementById('hdPageStatus').value;
       
    //   if(pgStatus=='U')
    //   {    
       
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            document.getElementById("hdExport").value="";
            document.getElementById("pnlPCInstallation").style.display="block";
            document.getElementById("pnlmiscInstallation").style.display="none";
            document.getElementById('hdTabType').value='1';
              if (CheckValidation()==false)
              {
              return false;
              }
            document.forms['form1'].submit();
            '<%ViewState("PrevSearchingMisc")=nothing%>';  
           
//           '<%ViewState("SortNameMISC")=nothing%>';
         
         
            return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            document.getElementById("hdExport").value="";
            document.getElementById('hdTabType').value='2';
            document.getElementById("pnlPCInstallation").style.display="none";
            document.getElementById("pnlmiscInstallation").style.display="block";
              if (CheckValidation()==false)
              {
              return false;
              }
            
           document.forms['form1'].submit();
//          '<%ViewState("SortName")=nothing%>';
          '<%ViewState("PrevSearching")=nothing%>';
          
           return false;         
       }
       
    }


    function HideShow()
    {
    
    var strTabtype=document.getElementById("hdTabType").value;
    switch(strTabtype)
    {
    case "1":
            document.getElementById("pnlPCInstallation").style.display="block";
            document.getElementById("pnlmiscInstallation").style.display="none";
            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab";     
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive";     
            break;
    case "2":
            document.getElementById("pnlPCInstallation").style.display="none";
            document.getElementById("pnlmiscInstallation").style.display="block";
            document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive";     
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtab";     
                       break;
       }
    }
    
     
</script>

<body onload="HideShow()">
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
    
 <table cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td>
    <!-- Code for Search Criteria -->
     <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 850px;">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Inventory-></span><span class="sub_menu">Hardware Installation</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Hardware Installation</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" style="height: 230px" colspan="7">
                                            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                                                <tr>
                                                    <td style="width: 1800px; height: 222px;" class="redborder" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center">
                                                                    &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px">
                                                                    &nbsp;</td>
                                                                <td width="18%" class="textbold">
                                                                    Agency Name
                                                                    <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                                    <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server"
                                                                        TabIndex="1" Width="89%"></asp:TextBox><img src="../Images/lookup.gif" alt="" tabindex="1" onclick="javascript:return PopupAgencyPage();"
                                                                        style="cursor: pointer;" id="IMG1" /></td>
                                                                <td style="width: 32px">
                                                                    <asp:Button ID="btnSearch" TabIndex="2" CssClass="button" runat="server" Text="Search"
                                                                        AccessKey="a" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px;">
                                                                </td>
                                                                <td class="textbold" width="18%" >
                                                                    Whole Group</td>
                                                                <td style="width: 198px;">
                                                                    <asp:CheckBox ID="ChkGrpProductivity" runat="server" TabIndex="1" CssClass="textbold" /></td>
                                                                <td class="textbold" style="width: 112px;" valign ="top">
                                                                    Country</td>
                                                                <td style="width: 170px;" valign ="top">
                                                                    <asp:DropDownList ID="drpCountry1" runat="server" TabIndex="1" CssClass="dropdownlist"
                                                                        Width="144px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 32px;" valign ="top">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" TabIndex="2" Text="Reset"
                                                                        Style="position: relative" AccessKey="r" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px;" valign ="top">
                                                                    </td>
                                                                <td class="textbold" valign ="top">
                                                                    City</td>
                                                                <td style="width: 198px;" valign ="top">
                                                                    <asp:DropDownList ID="drpCity1" runat="server" TabIndex="1" CssClass="dropdownlist"
                                                                        Width="144px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 112px;" valign ="top">
                                                                    Region</td>
                                                                <td style="width: 170px;" valign ="top"> 
                                                                    <asp:DropDownList TabIndex="1" ID="ddlRegion" runat="server" CssClass="dropdownlist"
                                                                        Width="144px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 32px" valign ="top">
                                                                    <asp:Button ID="btnDisplay" CssClass="button" runat="server" TabIndex="2" Text="Display"
                                                                        Style="position: relative" AccessKey="d" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px;" valign ="top">
                                                                </td>
                                                                <td class="textbold" valign ="top">
                                                                    1A Office</td>
                                                                <td style="width: 198px;" valign ="top">
                                                                    <asp:DropDownList ID="drpAoffice1" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                        TabIndex="1" Width="144px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 112px;" valign ="top">
                                                                    Vendor Serial No</td>
                                                                <td style="width: 170px;" valign ="top">
                                                                    <asp:TextBox ID="txtVendorSerialNo" runat="server" CssClass="textbox" MaxLength="40"
                                                                        ReadOnly="false" Style="position: relative" TabIndex="1" Width="136px"></asp:TextBox></td>
                                                                <td style="width: 32px" valign ="top">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative"
                                                                        TabIndex="2" Text="Export" AccessKey="e" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px;" valign ="top">
                                                                </td>
                                                                <td class="textbold"  valign ="top">
                                                                    Equipment Group</td>
                                                                <td style="width: 198px;" valign ="top">
                                                                    <asp:DropDownList ID="drpEquipmentGroup" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="144px" Style="position: relative" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 112px;" valign ="top">
                                                                    Equipment Type</td>
                                                                <td style="width: 170px;" valign ="top">
                                                                    <asp:TextBox ID="txtEquipmentType" runat="server" CssClass="textbox" MaxLength="40"
                                                                        ReadOnly="false" TabIndex="1" Width="136px" Style="position: relative"></asp:TextBox><img onclick="javascript:return PopupEquipment();" src="../Images/lookup.gif" style="cursor: pointer;"   /></td>
                                                                <td style="width: 32px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px" valign ="top">
                                                                </td>
                                                                <td class="textbold" valign="top">
                                                                    1A Responsibility</td>
                                                                <td style="width: 198px" valign ="top">                                                                    
                                                                    <asp:TextBox ID="txtAResponsibility" CssClass="textbox" runat="server" Style="left: 0px; position: relative; top: 0px" Width="136px" TabIndex="1"></asp:TextBox><img onclick="javascript:return EmployeePageTravelAgency();" src="../Images/lookup.gif" tabindex="1" style="cursor: pointer;"/>
                                                                </td>
                                                                <td class="textbold" style="width: 112px" valign ="top">
                                                                    Order Type</td>
                                                                <td style="width: 170px" valign ="top">
                                                                    

                                                                    <asp:DropDownList ID="drpOrderType" runat="server" CssClass="dropdownlist"
                                                                        Width="144px" Style="position: relative; left: 0px;" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 32px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px; height: 21px;" valign ="top">
                                                                </td>
                                                                <td class="textbold" valign="top" style="height: 21px">
                                                                    Installation Date From</td>
                                                                <td style="width: 198px; height: 21px;" valign="top">
                                                                    <asp:TextBox ID="txtInstallationFrom" TabIndex="1" CssClass="textbox" MaxLength="40"
                                                                        runat="server" Style="position: relative" Width="136px"></asp:TextBox>
                                                                        <img id="ImgInstallationFrom" alt="" src="../Images/calender.gif" title="Date selector" tabindex="1" style="cursor: pointer; position: relative; left: 0px; top: 0px;" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtInstallationFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgInstallationFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                    </script>
                                                                        </td>
                                                                <td class="textbold" style="width: 112px; height: 21px;" valign="top">
                                                                    Installation Date To</td>
                                                                <td style="width: 170px; height: 21px;" valign="top">

                                                                    

                                                                    <asp:TextBox ID="txtInstallationTo" runat="server" CssClass="textbox" MaxLength="40"
                                                                        TabIndex="1" Style="position: relative; left: 0px; top: 0px;" Width="136px"></asp:TextBox>
                                                                        <img id="imgInstallationTo" alt="" src="../Images/calender.gif" title="Date selector" tabindex="1" style="cursor: pointer; position: relative; left: 0px; top: 0px;" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtInstallationTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgInstallationTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                    </script>
                                                                        </td>
                                                                <td style="width: 32px; height: 21px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px; height: 21px;" valign ="top">
                                                                </td>
                                                                <td class="textbold" valign ="top" style="height: 21px">
                                                                    Purchase Date From</td>
                                                                <td style="width: 198px; height: 21px;" valign ="top">
                                                                    <asp:TextBox ID="txtPurchaseFrom" TabIndex="1" runat="server" CssClass="textbox"
                                                                        MaxLength="40" Style="position: relative" Width="136px"></asp:TextBox>
                                                                        <img id="ImgPurchaseFrom" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector" style="cursor: pointer;  position: relative; left: 0px; top: 0px;" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtPurchaseFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgPurchaseFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                    </script>
                                                                        </td>
                                                                <td class="textbold" style="width: 112px; height: 21px;" valign ="top">
                                                                    Purchase DateTo</td>
                                                                <td style="width: 170px; height: 21px;" valign ="top">
                                                                    <asp:TextBox ID="txtPurchaseTo" TabIndex="1" runat="server" CssClass="textbox" MaxLength="40"
                                                                        Style="left: 0px; position: relative; top: 0px" Width="136px"></asp:TextBox>
                                                                        <img id="ImgPurchaseTo" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector" style="cursor: pointer; position: relative; left: 0px; top: 0px;" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtPurchaseTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgPurchaseTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                    </script>
                                                                        </td>
                                                                <td style="width: 32px; height: 21px;">
                                                                <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px; height: 21px" valign="top">
                                                                </td>
                                                                <td class="textbold" valign ="top" style="height: 21px">
                                                                Lcode</td>
                                                                <td style="width: 170px; height: 21px;" valign ="top">
                                                                    <asp:TextBox ID="txtLcode" TabIndex="1" CssClass="textbox" MaxLength="40" runat="server" Width="136px"></asp:TextBox></td>
                                                                <td class="textbold" valign ="top" style="height: 21px">
                                                                Chain Code</td>
                                                                <td style="width: 170px; height: 21px;" valign ="top">
                                                                    <asp:TextBox ID="txtChainCode" TabIndex="1" CssClass="textbox" MaxLength="40" runat="server" Width="136px"></asp:TextBox></td>
                                                                <td style="width: 32px; height: 21px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 30px; height: 21px" valign="top">
                                                                </td>
                                                                <td class="textbold" style="height: 21px" valign="top">
                                                                    Company Vertical</td>
                                                                <td style="width: 170px; height: 21px" valign="top">
                                                                    <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        TabIndex="1" Width="144px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="height: 21px" valign="top">
                                                                </td>
                                                                <td style="width: 170px; height: 21px" valign="top">
                                                                </td>
                                                                <td style="width: 32px; height: 21px">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="hidLcode" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px; width: 80%">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" Width="100px" CssClass="headingtabactive" runat="server"
                                                        Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width: 20%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           
            
        </table>
    </td>
    <td></td>
    
    </tr>
    
    <tr>
    <td colspan="2">
    <!-- Code for Search Result Gridview & Paging -->
    <table cellpadding="0" cellspacing="0" border="0">
    <tr>
                <td class="top border_rightred">
                    <table width="2100px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td id="pnlPCInstallation" class="redborder top">
                                <%--<asp:Panel ID="pnlPCInstallation" runat="server" Width="100%" >--%>
                                <asp:GridView ID="gvPCInstallation" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                    Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AllowSorting="true" AlternatingRowStyle-CssClass="lightblue" EnableViewState="False"
                                    HeaderStyle-ForeColor="white" TabIndex="3">
                                    <Columns>
                                        <asp:BoundField DataField="LCODE" HeaderText="Lcode" SortExpression="LCODE" />
                                        <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code" SortExpression="CHAIN_CODE" />
                                        <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" SortExpression="AGENCYNAME" />
                                        <asp:BoundField DataField="OFFICEID" HeaderText="OfficeID" SortExpression="OFFICEID" />
                                        <asp:BoundField DataField="COMP_VERTICAL" HeaderText="Company Vertical" SortExpression="COMP_VERTICAL" />
                                        <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY" />
                                        <asp:BoundField DataField="COUNTRY" HeaderText="Country" SortExpression="COUNTRY" />
                                        <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status" SortExpression="ONLINE_STATUS" />
                                        <asp:BoundField DataField="DATEINSTALLED" HeaderText="Date" SortExpression="DATEINSTALLED" />
                                        <asp:BoundField DataField="CPUTYPE" HeaderText="CPU Type" SortExpression="CPUTYPE" />
                                        <asp:BoundField DataField="CPUNO" HeaderText="CPU Number" SortExpression="CPUNO" />
                                        <asp:BoundField DataField="MONTYPE" HeaderText="Monitor Type" SortExpression="MONTYPE" />
                                        <asp:BoundField DataField="MONNO" HeaderText="Monitor No" SortExpression="MONNO" />
                                        <asp:BoundField DataField="KBDTYPE" HeaderText="Keyboard Type" SortExpression="KBDTYPE" />
                                        <asp:BoundField DataField="KBDNO" HeaderText="Keyboard No" SortExpression="KBDNO" />
                                        <asp:BoundField DataField="MSETYPE" HeaderText="Mouse Type" SortExpression="MSETYPE" />
                                        <asp:BoundField DataField="MSENO" HeaderText="Mouse No" SortExpression="MSENO" />
                                        <asp:BoundField DataField="CDRNO" HeaderText="CDR No" SortExpression="CDRNO" />
                                        <asp:BoundField DataField="CHALLANNUMBER" HeaderText="Challan Number" SortExpression="CHALLANNUMBER" />
                                        <asp:BoundField DataField="WARRANTY" HeaderText="Warranty" SortExpression="WARRANTY" />
                                        <asp:BoundField DataField="PURCHASEDATE" HeaderText="Purchase Date" SortExpression="PURCHASEDATE" />
                                        <asp:BoundField DataField="SUPPLIERNAME" HeaderText="Vendor Name" SortExpression="SUPPLIERNAME" />
                                        
                                        
                                        <asp:BoundField DataField="OrderType" HeaderText="Order Type" SortExpression="OrderType" />
                                        <asp:BoundField DataField="RESPNAME" HeaderText="1A Responsible" SortExpression="RESPNAME" />
                                        
                                        
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White" />
                                    <RowStyle CssClass="textbold" />
                                </asp:GridView>
                                <%-- </asp:Panel>--%>
                            </td>
                        </tr>
                        <%--    <tr>
                                    <td colspan="6" valign="top">
                                        <asp:Panel ID="pnlPaging1" runat="server" Visible="false" Width="100%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                    <td style="width: 30%" class="left">
                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                            ID="TextBox1" runat="server" Width="105px" CssClass="textboxgrey"></asp:TextBox></td>
                                                    <td style="width: 25%" class="right">
                                                        <asp:LinkButton ID="lnkPrev1" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                    <td style="width: 20%" class="center">
                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                            ID="ddlPageNumber1" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 25%" class="left">
                                                        <asp:LinkButton ID="lnkNext1" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="73px" CssClass="textboxgrey" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>--%>
                        <tr>
                            <td id="pnlmiscInstallation" class="redborder top">
                                <%-- <asp:Panel ID="pnlmiscInstallation" runat="server" Width="100%">--%>
                                <asp:GridView ID="miscInstallation" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                    Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AlternatingRowStyle-CssClass="lightblue" EnableViewState="False" AllowSorting="true"
                                    HeaderStyle-ForeColor="white" ShowHeader="true" TabIndex="4">
                                    <Columns>
                                        <asp:BoundField DataField="LCODE" HeaderText="Lcode" SortExpression="LCODE" />
                                        <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code" SortExpression="CHAIN_CODE" />
                                        <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" SortExpression="AGENCYNAME" />
                                        <asp:BoundField DataField="OFFICEID" HeaderText="OfficeID" SortExpression="OFFICEID" />
                                         <asp:BoundField DataField="COMP_VERTICAL" HeaderText="Company Vertical" SortExpression="COMP_VERTICAL" />
                                        <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY" />
                                        <asp:BoundField DataField="COUNTRY" HeaderText="Country" SortExpression="COUNTRY" />
                                        <asp:BoundField DataField="DATEINSTALLED" HeaderText="Date" SortExpression="DATEINSTALLED" />
                                        <asp:BoundField DataField="EQUIPMENTTYPE" HeaderText="Equipment Type" SortExpression="EQUIPMENTTYPE" />
                                        <asp:BoundField DataField="EQUIPMENTNO" HeaderText="Equipment Number" SortExpression="EQUIPMENTNO" />
                                        <asp:BoundField DataField="QTY" HeaderText="Quantity" SortExpression="QTY" />
                                        <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status" SortExpression="ONLINE_STATUS" />
                                        <asp:BoundField DataField="CHALLANNUMBER" HeaderText="Challan Number" SortExpression="CHALLANNUMBER" />
                                        <asp:BoundField DataField="WARRANTY" HeaderText="Warranty" SortExpression="WARRANTY" />
                                        <asp:BoundField DataField="PURCHASEDATE" HeaderText="Purchase Date" SortExpression="PURCHASEDATE" />
                                        <asp:BoundField DataField="VENDORNAME" HeaderText="Vendor Name" SortExpression="VENDORNAME" />
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White" />
                                    <RowStyle CssClass="textbold" />
                                </asp:GridView>
                                <%-- </asp:Panel>--%>
                            </td>
                        </tr>
                        <%--      <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                         <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                   <asp:TextBox ID="txtRecordOnCurrentPage" runat ="server"  Width="73px" CssClass="textboxgrey" Visible ="false"  ></asp:TextBox>      
                                                    </td>
                                                     
                                                </tr>  --%>
                        <asp:HiddenField ID="hdTabType" runat="server" />
                        <asp:HiddenField ID="hdDefaultPC" runat="server" />
                        <asp:HiddenField ID="hdDefaultMisc" runat="server" />
                        <asp:HiddenField ID="hdExport" runat="server" Value=""/>
                        <asp:HiddenField ID="hdOrderTypeSelectedIndex" runat="server"/>                        
                        
                    </table>
                </td>
            </tr>
            <tr  runat ="server" id="naviGate">
                <td colspan="6" valign="top" style="height: 28px">
                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="70%">
                            <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                <td style="width: 20%" class="left">
                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                        ReadOnly="True"></asp:TextBox></td>
                                <td style="width: 20%" class="right">
                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                <td style="width: 20%" class="center">
                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                        ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td style="width: 20%" class="left">
                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                        Visible="false"></asp:TextBox>
                </td>
            </tr>
    </table> 
    </td>
    </tr>
    
    </table>
<script type="text/javascript" language="javascript">
 if (document.getElementById("hdAgencyName").value=="")
    {
        document.getElementById("ChkGrpProductivity").disabled=true;
        document.getElementById("ChkGrpProductivity").checked==false;
    }
</script>
</form>
</body>
</html>
