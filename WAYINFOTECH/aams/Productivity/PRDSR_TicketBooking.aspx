<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRDSR_TicketBooking.aspx.vb"
    Inherits="Productivity_PRDSR_TicketBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Productivity::Search Ticket Bookings</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">
           function CheckedUnchecked1AProd()
         {
                   var objChkProductivity =document.getElementById('Chk1AProd');                   
                   var objchkNIDT=document.getElementById('chkNIDT');
                   
                   var objChkAirBreakup =document.getElementById('ChkAirBreackup');
                       
                   if (objChkAirBreakup.checked==true) 
                   {
                      objChkProductivity.disabled=true;
                      objChkProductivity.checked=false;
                      
                        objchkNIDT.disabled=true;
                        objchkNIDT.checked=false;
                        
                      document.getElementById('Div1').className ="displayBlock";
                      document.getElementById('Div2').className ="displayBlock";                  
                   } 
                   else
                   {
                        //check rights of NIDT Display    
                        objchkNIDT.disabled=false;
                      //  objchkNIDT.checked=false;      
                    
                      document.getElementById('Div1').className ="displayNone";
                      document.getElementById('Div2').className ="displayNone";
                      
                      //check rights of BIDT Display
                      if (document.getElementById('Hd1AprodRight').value=="1" )
                      {
                        objChkProductivity.disabled=false;
                       // objChkProductivity.checked=false;                        
                      }
                      else
                      {
                      objChkProductivity.disabled=true;
                      objChkProductivity.checked=false;  
                      
                      }
                   }
              
   	          //  return false;
       }
      function ActDeAct()
     {
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9 &&  whichASC !=13 && whichASC!=18)
        {
        document.getElementById("hdAgencyName").value="";
        document.getElementById("ChkGrpProductivity").disabled=true;
        document.getElementById("ChkGrpProductivity").checked=false;
        }
    	
     }
       function PopupPage(id)
         {
         var type;        
         if (id=="2")
         {
                type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	
         } 
     }
   
             function DetailsFunction(FMonth,TMonth,FYear,TYear,Lcode,GroupData,TicketData,DailyBookData,AProdData,CRSData,AirBreakData,MBreakup,AirCode,NIDTData)
     {
                
                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&GroupData=" + GroupData +  "&TicketData=" + TicketData +  "&DailyBookData=" + DailyBookData  +  "&AProdData=" + AProdData  +  "&CRSData=" + CRSData  +  "&AirBreakData=" + AirBreakData  + "&MBreakup=" + MBreakup + "&AirCode=" + AirCode + "&NIDT=" + NIDTData;                
                
               
                type = "PRD_TicketDetails.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=1050,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
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

     function SelectProdutivity()
     {
      
       var objdrpProductivity =document.getElementById('drpProductivity').value;
       var objtxtFrom =document.getElementById('txtFrom');
       var objtxtTo =document.getElementById('txtTo');       
       if (objdrpProductivity=='') 
       {
          objtxtFrom.disabled=true;
          objtxtTo.disabled=true;
          objtxtFrom.value='0';
          objtxtTo.value='0';
          objtxtFrom.className='textboxgrey';
          objtxtTo.className='textboxgrey';
       } 
    else if (objdrpProductivity=='7') 
       {
         
          objtxtFrom.disabled=false;
          objtxtTo.disabled=false;
          objtxtFrom.value='0';
          objtxtTo.value='0';
          objtxtFrom.className='textbox';
          objtxtTo.className='textbox';  
       }   
       else 
       {
          objtxtFrom.disabled=false;
          objtxtTo.disabled=true;
          objtxtFrom.value='0';
          objtxtTo.value='';  
          objtxtFrom.className='textbox';
          objtxtTo.className='textboxgrey';   
       }
       
     }
    
    
     function CheckValidation()
     {

            if(document.getElementById('<%=txtLcode.ClientID%>').value.trim()!="")
                  {
                    if(IsDataValid(document.getElementById("txtLcode").value,12)==false)
                    {
                        document.getElementById('lblError').innerHTML='Invalid Lcode.';             
                        document.getElementById("txtLcode").focus();
                        return false;
                     }                  
                  }
              
              if(document.getElementById('<%=txtChainCode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtChainCode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Chain code.';             
                    document.getElementById("txtChainCode").focus();
                    return false;
                 }                  
              }
              
           if(document.getElementById('<%=txtFrom.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtFrom").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtFrom").focus();
                    return false;
                 }                  
              }
               if(document.getElementById('<%=txtTo.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtTo").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtTo").focus();
                    return false;
                 }                  
              }
                 if(parseInt(document.getElementById("txtFrom").value) >parseInt(document.getElementById("txtTo").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Productivity range is not valid.';          
                    document.getElementById("txtFrom").focus();
                    return false;
                    } 
                    
                      if(parseInt(document.getElementById("drpYearFrom").value) >parseInt(document.getElementById("drpYearTo").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
                   
                   if(parseInt(document.getElementById("drpYearTo").value)- parseInt(document.getElementById("drpYearFrom").value)>4)
                    {                   
                    document.getElementById("lblError").innerHTML='Maximum number of years should be 4 years.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
                    
                   var objChkAirBreakup =document.getElementById('ChkAirBreackup');
                       
                   if (objChkAirBreakup.checked==true) 
                   {
                          if(document.getElementById('<%=DlstAirline.ClientID%>').value.trim()=="")
                       {
                            if(document.getElementById('<%=txtAgencyName.ClientID%>').value.trim()=="")
                            {
                                   if(document.getElementById('<%=dlstCity.ClientID%>').value.trim()=="")
                                {
                                      document.getElementById("lblError").innerHTML='Either airline or agency or city is mandatory.';          
                                      document.getElementById('<%=DlstAirline.ClientID%>').focus();                          
                                      return false;
                                }  
                            }                         
                       }                       
                   } 
                    document.getElementById("lblError").innerHTML="";
              
     }
          function gotops(ddlname)
         {    
             if (event.keyCode==46 )
             {
                document.getElementById(ddlname).selectedIndex=0;
                SelectProdutivity();
             }
         }     
    </script>

</head>
<body onload="ResetSelectedValueAfterSearch();">
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
        <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 860px;">
                    <table width="100%" class="left">
                        <tr>
                            <td>
                                <span class="menu">Productivity -&gt;</span><span class="sub_menu">Ticket Bookings Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center">
                                Search Ticket Bookings</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 860px;" class="redborder" valign="top">
                                                        <table border="0" cellpadding="2" cellspacing="1" style="width: 845px;" class="left">
                                                            <tr>
                                                                <td class="center" colspan="8">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%">
                                                                </td>
                                                                <td class="textbold" style="width: 17%">
                                                                    Country</td>
                                                                <td style="width: 20%">
                                                                    <asp:DropDownList ID="dlstCountry" runat="server" CssClass="dropdownlist" Width="165px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 2%">
                                                                </td>
                                                                <td class="textbold" style="width: 20%">
                                                                    City</td>
                                                                <td class="textbold" style="width: 23%">
                                                                    <asp:DropDownList ID="dlstCity" runat="server" CssClass="dropdownlist" Width="155px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 6%">
                                                                </td>
                                                                <td class="left" style="width: 15%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"
                                                                        AccessKey="A" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%">
                                                                    <input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" /></td>
                                                                <td class="textbold" style="width: 17%">
                                                                    Agency Name</td>
                                                                <td colspan="4" style="width: 63%">
                                                                    <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="50"
                                                                        TabIndex="1" Width="507px"></asp:TextBox>
                                                                    <img id="Img2" runat="server" tabindex="1" alt="" onclick="PopupPage(2)" src="../Images/lookup.gif"
                                                                        style="cursor: pointer;" /></td>
                                                                <td style="width: 6%">
                                                                </td>
                                                                <td style="width: 15%;" class="left">
                                                                    <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="2"
                                                                        AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">  <input id="Hd1AprodRight" runat="server" style="width: 1px" type="hidden" value ="1" />
                                                                </td>
                                                                <td class="textbold" style="width: 17%;">
                                                                    Group Productivity</td>
                                                                <td style="width: 20%;">
                                                                    <asp:CheckBox ID="ChkGrpProductivity" runat="server" CssClass="textbox" TabIndex="1" /></td>
                                                                <td class="textbold" style="width: 2%;">
                                                                </td>
                                                                <td class="textbold" style="width: 20%;">
                                                                    </td>
                                                                <td class="textbold" style="width: 23%;">
                                                                    </td>
                                                                <td style="width: 6%;">
                                                                </td>
                                                                <td class="left" style="width: 15%;">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                        AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%; height: 23px;">
                                                                </td>
                                                                <td class="textbold" style="width: 17%; height: 23px;">
                                                                    Agency Status</td>
                                                                <td style="width: 20%; height: 23px;">
                                                                    <asp:DropDownList ID="drpAgencyStatus" runat="server" CssClass="dropdownlist" Width="165px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 2%; height: 23px;">
                                                                </td>
                                                                <td class="textbold" style="width: 20%; height: 23px;">
                                                                    Responsible Staff</td>
                                                                <td class="textbold" style="width: 23%; height: 23px;">
                                                                    <asp:DropDownList ID="drpResStaff" runat="server" CssClass="dropdownlist" Width="155px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 6%; height: 23px;">
                                                                </td>
                                                                <td class="center" style="width: 15%; height: 23px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%">
                                                                </td>
                                                                <td class="textbold" style="width: 17%" valign="top">
                                                                    Lcode</td>
                                                                <td style="width: 20%">
                                                                    <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1" Width="160px"></asp:TextBox></td>
                                                                <td style="width: 2%" valign="top">
                                                                </td>
                                                                <td style="width: 20%" valign="top" class="textbold" >
                                                                    Chain Code</td>
                                                                <td style="width: 23%" valign="top">
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1" Width="150px"></asp:TextBox></td>
                                                                <td colspan="2" style="width: 21%" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%">
                                                                </td>
                                                                <td class="textbold" style="width: 17%" valign="top">
                                                                    Agency Group Category</td>
                                                                <td style="width: 20%">
                                                                    <asp:DropDownList ID="drpAgencyType" runat="server" CssClass="dropdownlist" Width="165px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 2%" valign="top">
                                                                </td>
                                                                <td class="textbold" style="width: 20%" valign="top">
                                                                    Agency Category</td>
                                                                <td style="width: 23%" valign="top">
                                                                    <asp:DropDownList ID="drpGroupAgencyType" runat="server" CssClass="dropdownlist" Width="155px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td colspan="2" style="width: 21%" valign="top">
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="width: 3%">
                                                                </td>
                                                                <td class="textbold" style="width: 17%" valign ="top" >Productivity</td>
                                                                <td style="width: 20%"><asp:DropDownList ID="drpProductivity" runat="server" CssClass="dropdownlist" Width="165px"
                                                                        TabIndex="1"><asp:ListItem Selected="True">--All--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                        <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                        <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="7">Between</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 2%" valign ="top" >
                                                                </td>
                                                                <td style="width: 20%" valign ="top" ><asp:TextBox ID="txtFrom" runat="server" CssClass="textboxgrey" MaxLength="9" TabIndex="1"
                                                                        Width="150px" Enabled="False">0</asp:TextBox></td>
                                                                <td style="width: 23%" valign ="top" ><asp:TextBox ID="txtTo" runat="server" CssClass="textboxgrey" MaxLength="9" TabIndex="1"
                                                                        Width="150px" Enabled="False">0</asp:TextBox></td>
                                                                <td style="width: 21%" colspan="2" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%" valign="top">
                                                                </td>
                                                                <td class="textbold" style="width: 17%" valign="top">
                                                                    1 A Office</td>
                                                                <td style="width: 20%" valign="top">
                                                                    <asp:DropDownList ID="drp1AOffice" runat="server" CssClass="dropdownlist" Width="165px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 2%">
                                                                </td>
                                                                <td class="textbold" style="width: 20%" valign="middle"  rowspan="6">&nbsp;
                                                                <asp:CheckBoxList ID="ChkListStatus" runat="server" CssClass="textbox" Width="170px"
                                                                        TabIndex="1">
                                                                        <asp:ListItem>Show Online Status</asp:ListItem>
                                                                        <asp:ListItem>Show Address</asp:ListItem>
                                                                        <asp:ListItem>Show Country</asp:ListItem>
                                                                        <asp:ListItem>Show Chain Code</asp:ListItem>
                                                                        <asp:ListItem>Show agency category</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                    </td>
                                                                <td class="textbold" style="width: 23%" rowspan="6" valign="top">
                                                                   <div id="divlbl" runat="server">
                                                                        <table border="0" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <span style="font-size: 9pt; font-family: Arial"><strong>Ticket Details</strong></span></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="ChkTicket" TabIndex="1" runat="server" Text="Ticket" CssClass="textbox" Checked ="true" >
                                                                                    </asp:CheckBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="ChkDailyBook" TabIndex="1" runat="server" Text="Daily Booking"
                                                                                        CssClass="textbox"></asp:CheckBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="Chk1AProd" TabIndex="1" runat="server" Text="1A Productivity" CssClass="textbox">
                                                                                    </asp:CheckBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="ChkAllCRS" TabIndex="1" runat="server" Text="All CRS MIDT" CssClass="textbox">
                                                                                    </asp:CheckBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkNIDT" runat="server" CssClass="textbox" TabIndex="1" Text="CODD & HX" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                                <td style="width: 21%;" rowspan="6" colspan="2" valign="top">
                                                                 
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%">&nbsp;
                                                                </td>
                                                                <td class="textbold" style="width: 17%" valign="top">
                                                                    Region</td>
                                                                <td style="width: 20%" valign="top">
                                                                    <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="165px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                </td>                                                                
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%" valign="top">&nbsp;
                                                                </td>
                                                                <td class="textbold" style="width: 17%" valign="top">
                                                                    Group Type</td>
                                                                <td class="textbold" style="width: 20%" valign="top">
                                                                    <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdownlist" Width="165px"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                </td>                                                               
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 3%;">&nbsp;
                                                                </td>
                                                                <td class="textbold" style="width: 17%;">
                                                                    Company Vertical</td>
                                                                <td class="textbold" style="width: 20%;"><asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" Width="165px"
                                                                        TabIndex="1">
                                                                </asp:DropDownList></td>
                                                                <td>&nbsp;
                                                                </td>
                                                               
                                                            </tr>
                                                                <tr>
                                                                <td style="width: 3%;">&nbsp;
                                                                </td>
                                                                <td class="textbold" style="width: 17%;">&nbsp;
                                                                   </td>
                                                                <td style="width: 20%;">
                                                                    <asp:RadioButton ID="RdShowMon" runat="server" CssClass="textbox" Text="Show Monthly Breakup"
                                                                        Width="156px" GroupName="Avg" Checked="True" TabIndex="1" /></td>
                                                                <td class="textbold" style="width: 2%;">&nbsp;
                                                                </td>  
                                                            </tr> 
                                                             <tr>
                                                                <td style="width: 3%;">&nbsp;
                                                                </td>
                                                                <td class="textbold" style="width: 17%;">&nbsp;
                                                                   </td>
                                                                <td style="width: 20%;"><asp:RadioButton ID="RdShowAvg" runat="server" CssClass="textbox" Text="Show Average"
                                                                            GroupName="Avg" TabIndex="1" /></td>
                                                                <td class="textbold" style="width: 2%;">&nbsp;
                                                                </td>
                                                               
                                                            </tr>                                                    
                                                            <tr>
                                                                <td class="textbold" style="width: 3%">
                                                                </td>
                                                                <td class="textbold" style="width: 17%" valign="top">
                                                                </td>
                                                                <td class="textbold" style="width: 20%" valign="top">
                                                                                    <asp:CheckBox ID="ChkAirBreackup" TabIndex="1" runat="server" Text="Air Breakup"
                                                                                        CssClass="textbox"></asp:CheckBox></td>
                                                                <td style="width: 2%">
                                                                </td>
                                                                <td class="textbold" style="width: 20%" valign="top"><div id="Div1" runat="server">Airline</div>
                                                                </td>
                                                                <td class="textbold" valign="top" colspan="3"><div id="Div2" runat="server"><asp:DropDownList ID="DlstAirline" runat="server" CssClass="dropdownlist" TabIndex="1"></asp:DropDownList></div>
                                                                </td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td class="textbold" style="width: 3%">&nbsp;
                                                                </td>
                                                                <td class="textbold" style="width: 17%" valign="top">
                                                                    Period From
                                                                </td>
                                                                <td class="textbold" style="width: 20%" valign="top">
                                                                    <asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="88px" Style="position: relative">
                                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="drpYearFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="56px" Style="left: 16px; position: relative; top: 0px">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 2%">
                                                                </td>
                                                                <td class="textbold" style="width: 20%" valign="top">
                                                                    Period To</td>
                                                                <td class="textbold" style="width: 23%" valign="top">
                                                                    <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                                    Width="88px">
                                                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                                                </asp:DropDownList>&nbsp;<asp:DropDownList ID="drpYearTo" runat="server" CssClass="dropdownlist"
                                                                                    TabIndex="1" Width="56px" Style="left: 8px; position: relative; top: 0px">
                                                                                </asp:DropDownList><span class="textbold"></span></td>
                                                                        </tr>
                                                                    </table>
                                                                    <span class="textbold"></span>
                                                                </td>
                                                                <td class="textbold" style="width: 19%" colspan="2" valign="top">
                                                                    &nbsp; &nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" style="height: 10px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" class="textbold">
                                                                    <asp:Label ID="lblFound" runat="server" Text="No. of records found " Font-Bold="True"
                                                                        Width="142px" Visible="False"></asp:Label>
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
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" style="padding-left: 4px;">
                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder">
                                <asp:GridView EnableViewState="false" ID="grdvMonthlyBReak" runat="server" AutoGenerateColumns="False"
                                    AllowSorting="true" HeaderStyle-ForeColor="white" Width="2000px" HeaderStyle-CssClass="Gridheading"
                                    RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" ShowFooter="true" TabIndex="3">
                                    <Columns>
                                        <asp:BoundField DataField="LCODE" HeaderText="Lcode" SortExpression="LCODE">
                                            <ItemStyle Wrap="False" Width="55px" />
                                            <HeaderStyle Wrap="False" Width="55px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code " SortExpression="CHAIN_CODE">
                                            <ItemStyle Wrap="False" Width="55px" />
                                            <HeaderStyle Wrap="False" Width="55px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AGENCY_NAME" HeaderText="Agency Name " SortExpression="AGENCY_NAME">
                                            <ItemStyle Width="230px" Wrap="False" />
                                            <HeaderStyle Wrap="False" Width="230px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS" HeaderText="Address " SortExpression="ADDRESS">
                                            <ItemStyle Width="280px" Wrap="False" />
                                            <HeaderStyle Wrap="False" Width="280px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Sales Executive " SortExpression="EMPLOYEE_NAME">
                                            <ItemStyle Width="130px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression="COMP_VERTICAL">
                                            <ItemStyle Width="100px" Wrap="false" />
                                            <HeaderStyle Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CITY" HeaderText="City " SortExpression="CITY">
                                            <ItemStyle Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COUNTRY" HeaderText="Country " SortExpression="COUNTRY">
                                            <ItemStyle Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status " SortExpression="ONLINE_STATUS">
                                            <ItemStyle Width="88px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GROUPTYPENAME" HeaderText="Agency Category " SortExpression="GROUPTYPENAME">
                                            <ItemStyle Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OFFICEID" HeaderText="OfficeId " SortExpression="OFFICEID">
                                            <ItemStyle Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AOFFICE" HeaderText="Aoffice " SortExpression="AOFFICE">
                                            <ItemStyle Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MONTH" HeaderText="Month " SortExpression="MONTH">
                                            <ItemStyle Width="60px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="YEAR" HeaderText="Year " SortExpression="YEAR">
                                            <ItemStyle Width="50px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AIRLINE_NAME" HeaderText="Airline " SortExpression="AIRLINE_NAME">
                                            <ItemStyle Width="180px" Wrap="False" />
                                            <HeaderStyle Wrap="False" Width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TKT_NETBOOKINGS" HeaderText="Tickets " SortExpression="TKT_NETBOOKINGS">
                                            <ItemStyle Width="50px" Wrap="False" HorizontalAlign="Right" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DB_NETBOOKINGS" HeaderText="Daily Productivity" SortExpression="DB_NETBOOKINGS">
                                            <ItemStyle Width="110px" Wrap="False" HorizontalAlign="Right" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="A_NETBOOKINGS" HeaderText="1A Productivity " SortExpression="A_NETBOOKINGS">
                                            <ItemStyle Width="100px" Wrap="False" HorizontalAlign="Right" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="MT_NETBOOKINGS" HeaderText="ALL CRS MIDT " SortExpression="MT_NETBOOKINGS">
                                            <ItemStyle Width="95px" Wrap="False" HorizontalAlign="Right" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="CODD" HeaderText="CODD" SortExpression="CODD">
                                            <ItemStyle Width="80px" Wrap="False" HorizontalAlign="Right" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="HX_BOOKINGS" HeaderText="HX BOOKINGS" SortExpression="HX_BOOKINGS">
                                            <ItemStyle Width="90px" Wrap="False" HorizontalAlign="Right" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a>
                                                <asp:HiddenField ID="hdLcode" runat="server" Value='<%# Eval("LCODE") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" Width="80px" HorizontalAlign ="Center"  />
                                            <HeaderStyle HorizontalAlign ="Center" Width="80px"  />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White" />
                                    <RowStyle CssClass="textbold" />
                                    <FooterStyle CssClass="Gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" valign="top" style="width: 860px; height: 74px;">
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                            <td style="width: 30%" class="left">
                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                    ID="txtRecordCount" runat="server" Width="105px" CssClass="textboxgrey" ReadOnly="true"></asp:TextBox></td>
                                            <td style="width: 25%" class="right">
                                                <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                            <td style="width: 20%" class="center">
                                                <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                    Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                </asp:DropDownList></td>
                                            <td style="width: 25%" class="left">
                                                <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                        Visible="false"></asp:TextBox></td>
            </tr>
        </table>
    </form>

    <script type="text/javascript" language="javascript">
       ActDecLcodeChainCode();
       function ResetSelectedValueAfterSearch()
       {
       }
          CheckedUnchecked1AProd();
          if (document.getElementById("hdAgencyName").value=="")
              {
                  document.getElementById("ChkGrpProductivity").disabled=true;
                  document.getElementById("ChkGrpProductivity").checked==false;        
              }     
        
    </script>

</body>
</html>
