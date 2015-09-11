<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_ChallanStatus.aspx.vb"
    Inherits="Inventory_INVSR_ChallanStatus" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Search Challan Status</title>
<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">
   
   function ActDeAct()
     {
    
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
      
        if(whichASC!=9 && whichASC!=18 && whichASC!=13)
        {
     
          document.getElementById("hdAgencyNameId").value="";
      
        }
    	
     }
   
    function DateValidatin(strDt)
    {
    
    var v=document.getElementById(strDt).value;
        if(v!='')
        {
            if(isDate(v,"dd/MM/yyyy")==false)
            {
            document.getElementById("lblError").innerHTML ="Invalid Date(dd/mm/yyyy) format";
            document.getElementById(strDt).focus();
            return false;
            }
        }
        else
        {
       // document.getElementById("lblError").innerHTML ="";
        }
    }
    
   
    function validateChallanStatus(strDrp,strDate)
    {
   
       var drp=document.getElementById(strDrp).selectedIndex;
        if(drp=='0')
        {
        //document.getElementById(strDate).value='';
        return false;
        }
    
    }
    
    
    function validateGrid()
    {
   
    
    var gridID,colDrp,ColTxt;
                    
    
    if(document.getElementById("gvChallanStatusPI") !=null)
    {
        colDrp=11;
        ColTxt=12;
         gridID=document.getElementById('<%=gvChallanStatusPI.ClientID%>');
    }
    else if (document.getElementById("gvChallanStatusPR") !=null)
    {
        colDrp=12;
        ColTxt=13;
   
    gridID=document.getElementById('<%=gvChallanStatusPR.ClientID%>');
    }
    else if (document.getElementById("gvChallanStatusMI") !=null)
    {
        colDrp=6;
        ColTxt=7;
   
    gridID=document.getElementById('<%=gvChallanStatusMI.ClientID%>');
    
    }
    else if (document.getElementById("gvChallanStatusMD") !=null)
    {
        colDrp=6;
        ColTxt=7;
    gridID=document.getElementById('<%=gvChallanStatusMD.ClientID%>');
    
    }
    else if (document.getElementById("gvChallanStatusMR") !=null)
    {
        colDrp=6;
        ColTxt=7;
    gridID=document.getElementById('<%=gvChallanStatusMR.ClientID%>');
    }
    else if (document.getElementById("gvChallanStatusPD") !=null)
    {
         colDrp=12;
        ColTxt=13;
       gridID=document.getElementById('<%=gvChallanStatusPD.ClientID%>');
    }
    
    
    
    
    
             for(intcnt=1;intcnt<=gridID.rows.length-1;intcnt++)
            {        
              var drIndex=gridID.rows[intcnt].cells[colDrp].children[0].selectedIndex;
              var txtVal=gridID.rows[intcnt].cells[ColTxt].children[0].value;
                  if(drIndex=='0' && txtVal!='')
                  {
                        document.getElementById("lblError").innerHTML ="Challan Status cannot be blank";
                        gridID.rows[intcnt].cells[colDrp].children[0].focus();
                        return false;
                        
                  }
                  
                  if(drIndex!='0' && txtVal=='')
                  {
                        document.getElementById("lblError").innerHTML ="Challan Date cannot be blank";
                       gridID.rows[intcnt].cells[ColTxt].children[0].focus();
                        return false;
                        
                  }
                
            }
           
    
    }
    
    
    function SelectDate(textBoxid,imgId)
    {   
                 Calendar.setup
                 (
                     {
                           inputField : textBoxid,
                           ifFormat :"%d/%m/%Y",
                           button :imgId,
                           onmousedown :true
                     }
                 )                            
                                                        
    }
      function PopupAgencyPage()
        {
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"Agency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
            return false;
        } 
                
        function EditFunction(Args,ChallanType,ChallanDate,PCDate)
        {           
           // alert(Args);   
            var pos=Args.split('|');  
            var type;        
            type = "../Inventory/INVUP_ChallanStatus.aspx?Popup=T&RowId=" +  pos[0] + "&ChStatus=" +  pos[1] + "&ChDate=" + ChallanDate + "&ChType=" + ChallanType + "&PCDate=" + PCDate  ;      
   	        window.open(type,"ab",'height=260px,width=620px,top=10,left=30,scrollbars=0,status=1');	
            return false;          
          
        } 
        function HistoryFunctionMR(Args)
        {
         // alert(Args);   
            var pos=Args;  
            var type;        
            type = "../Popup/PUSR_ChallanStatusMRHistory.aspx?Popup=T&RowId=" + pos;
   	        window.open(type,"ab",'height=600px,width=900px,top=10,left=30,scrollbars=1,status=1');	
            return false;    
        }
        
         function HistoryFunctionPR(Args)
        {
         // alert(Args);   
            var pos=Args;  
            var type;        
            type = "../Popup/PUSR_ChallanStatusPRHistory.aspx?Popup=T&RowId=" + pos;
   	        window.open(type,"ab",'height=600px,width=900px,top=10,left=30,scrollbars=1,status=1');	
            return false;    
        }
        
        function ChallanStatusMandatory()
        
        {
        
        }
        function ChallanStatusReset()
        {
            if (document.getElementById("pnlPaging")!=null) 
            document.getElementById("pnlPaging").style.display ="none"; 
           document.getElementById("txtAgencyName").value=""; 
           document.getElementById("hdAgencyNameId").value="";           
           document.getElementById("txtAddress").value="";
           document.getElementById("txtDateFrom").value=""; 
           document.getElementById("txtDateTo").value="";
           document.getElementById("lblError").innerHTML="";
           document.getElementById("drpCity").selectedIndex=0;  
           document.getElementById("drpCountry").selectedIndex=0;  
           document.getElementById("drpAOffice").selectedIndex=0;
           document.getElementById("drpEquipGroup").innerHTML=""; 
           document.getElementById("drpEquipCode").selectedIndex=0;  
           document.getElementById("drpOrderType").selectedIndex=0; 
           document.getElementById("drpOrderStatus").selectedIndex=0;  
           document.getElementById("drpChallanStatus").selectedIndex=0;
           document.getElementById("drpLoggedBy").selectedIndex=0;
           document.getElementById("drpDateSearchOn").selectedIndex=0;
           document.getElementById('rdHardWareType_0').checked=true;        
           document.getElementById('rdChallanType_0').checked=true;  
           ClearItemFromDate();
           document.getElementById("drpOrderType").disabled=false;
           document.getElementById("drpOrderStatus").disabled=false; 
                
           if (document.getElementById("gvChallanStatusPI")!=null) 
           document.getElementById("gvChallanStatusPI").style.display ="none"; 
             if (document.getElementById("gvChallanStatusPD")!=null) 
           document.getElementById("gvChallanStatusPD").style.display ="none"; 
             if (document.getElementById("gvChallanStatusPR")!=null) 
           document.getElementById("gvChallanStatusPR").style.display ="none"; 
             if (document.getElementById("gvChallanStatusMI")!=null) 
           document.getElementById("gvChallanStatusMI").style.display ="none"; 
             if (document.getElementById("gvChallanStatusMD")!=null) 
           document.getElementById("gvChallanStatusMD").style.display ="none"; 
             if (document.getElementById("gvChallanStatusMR")!=null) 
           document.getElementById("gvChallanStatusMR").style.display ="none"; 
           document.getElementById("txtAgencyName").focus(); 
           return false;
        }
    
  function DisableOrderStatus()
  {   
        
          var rdHardWareType=document.getElementById('rdHardWareType_1').checked;        
          if(rdHardWareType==true)
          {
            document.getElementById("drpOrderType").selectedIndex=0; 
            document.getElementById("drpOrderStatus").selectedIndex=0; 
            document.getElementById("drpOrderType").disabled=true;
            document.getElementById("drpOrderStatus").disabled=true;
          }
          else
          {
            document.getElementById("drpOrderType").disabled=false;
            document.getElementById("drpOrderStatus").disabled=false;
          }
  }
    function ClearItemFromDate()
      { 
            var rdChallanType1=document.getElementById('rdChallanType_0').checked;
            var rdChallanType2=document.getElementById('rdChallanType_1').checked; 
            var rdChallanType3=document.getElementById('rdChallanType_2').checked;
            var drpDateSearchOn = document.getElementById('<%=drpDateSearchOn.ClientId%>');
            for (var count = drpDateSearchOn.options.length-1; count >-1; count--)
			{
				drpDateSearchOn.options[count] = null;
			}
            
            if(rdChallanType1==true)
            {
            // alert('rdChallanType1');
            listItem = new Option('', '');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Installation Date', '1');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Challan Date', '2');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Login Date', '5');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            }
             if(rdChallanType2==true)
            {
             // alert('rdChallanType2');
            listItem = new Option('', '');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Installation Date', '1');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Challan Date', '2');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Login Date', '5');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('DeInstallation Date', '3');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            }
             if(rdChallanType3==true)
            {
             // alert('rdChallanType3');
            listItem = new Option('', '');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Installation Date', '1');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Challan Date', '2');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Login Date', '5');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            listItem = new Option('Replacement Date', '4');
            drpDateSearchOn.options[drpDateSearchOn.length] = listItem;
            }
      }
  function rtclickcheck(keyp)
	{ if (navigator.appName == "Netscape" && keyp.which == 3)
	{ 	 return false; } 
	
	if (navigator.appVersion.indexOf("MSIE") != -1 && event.button == 2)
	 { 	 	return false; } } 
	document.onmousedown = rtclickcheck;
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
        <table width="840px" align="left" class="border_rightred">
            <tr>
                <td class="top">
                    <table width="840px" class="left">
                        <tr>
                            <td>
                                <span class="menu">Inventory -&gt;</span><span class="sub_menu">Challan Status Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" style="width: 840px">
                                Search Challan Status</td>
                        </tr>
                        <tr>
                            <td>
                                <table width="840px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center" style="width: 840px">
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 800px" class="left">
                                                <tr>
                                                    <td class="center" colspan="7" >
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
                                                        
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px">
                                                    </td>
                                                    <td class="textbold" style="width: 150px">
                                                        Agency Name</td>
                                                    <td style="nowarp: trure;" colspan="4">
                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="50"
                                                            TabIndex="1" Width="504px"></asp:TextBox>
                                                        <img onclick="javascript:return PopupAgencyPage();" src="../Images/lookup.gif" tabindex="2" /></td>
                                                    <td class="center" style="width: 90px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="20" AccessKey="a" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px">
                                                     
                                                    </td>
                                                    <td class="textbold" style="width: 150px">
                                                        Address</td>
                                                    <td style="width: 180px">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" MaxLength="50" TabIndex="3"
                                                            Width="163px"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 20px">
                                                    </td>
                                                    <td class="textbold" style="width: 150px">
                                                        City</td>
                                                    <td style="width: 300px">
                                                        <asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="4" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="center" style="width: 90px">
                                                        <asp:Button ID="btn_Save" runat="server" CssClass="button" TabIndex="21" Text="Save"
                                                            Style="position: relative" AccessKey="s" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px">
                                                    </td>
                                                    <td class="textbold" style="width: 150px" valign="top">
                                                        Country</td>
                                                    <td style="width: 180px" valign="top">
                                                        <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="5" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 20px">
                                                    </td>
                                                    <td class="textbold" style="width: 200px" valign="top">
                                                        1AOffice</td>
                                                    <td style="width: 180px" valign="top">
                                                        <asp:DropDownList ID="drpAOffice" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="6" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 90px" class="center">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative"
                                                            TabIndex="22" Text="Export" AccessKey="e" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px">
                                                    </td>
                                                    <td class="textbold" style="width: 150px" valign="top">
                                                        Equipment Group
                                                    </td>
                                                    <td style="width: 180px" valign="top">
                                                        <asp:DropDownList ID="drpEquipGroup" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="7" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 20px">
                                                    </td>
                                                    <td class="textbold" style="width: 180px" valign="top">
                                                        Equipment Code</td>
                                                    <td style="width: 180px" valign="top">
                                                        <asp:DropDownList ID="drpEquipCode" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="8" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 90px" class="center">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="23"
                                                            Style="position: relative" AccessKey="r" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px">
                                                    </td>
                                                    <td class="textbold" style="width: 150px">
                                                        Order Type</td>
                                                    <td style="width: 180px">
                                                        <asp:DropDownList ID="drpOrderType" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="9" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 20px">
                                                    </td>
                                                    <td class="textbold" style="width: 200px">
                                                        Order Status</td>
                                                    <td style="width: 180px">
                                                        <asp:DropDownList ID="drpOrderStatus" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="10" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="center" style="width: 90px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px;">
                                                    </td>
                                                    <td class="textbold" style="width: 150px;" valign="top">
                                                        Challan Status</td>
                                                    <td style="width: 180px;" valign="top">
                                                        <asp:DropDownList ID="drpChallanStatus" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="11" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 20px;">
                                                    </td>
                                                    <td class="textbold" style="width: 200px;" valign="top">
                                                        Logged By</td>
                                                    <td style="width: 180px;" valign="top">
                                                        <asp:DropDownList ID="drpLoggedBy" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="12" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 90px;" class="center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px;">
                                                    </td>
                                                    <td class="textbold" style="width: 150px;" valign="middle">
                                                        Challan Type</td>
                                                    <td colspan="3" valign="middle">
                                                        <asp:RadioButtonList CssClass="textbox" ID="rdChallanType" RepeatDirection="Horizontal"
                                                            runat="server" Width="30px" TabIndex="13">
                                                            <asp:ListItem Value="I" Selected="True">Installation</asp:ListItem>
                                                            <asp:ListItem Value="D">DeInstallation</asp:ListItem>
                                                            <asp:ListItem Value="R">Replacement</asp:ListItem>
                                                        </asp:RadioButtonList></td>
                                                    <td style="width: 180px;" valign="top">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" valign="middle">
                                                                    Hardware Type
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;</td>
                                                                <td class="textbold" valign="middle">
                                                                    <asp:RadioButtonList ID="rdHardWareType" runat="server" RepeatDirection="Horizontal"
                                                                        Width="65px" CssClass="textbox" TabIndex="14">
                                                                        <asp:ListItem Value="P" Selected="True">PC</asp:ListItem>
                                                                        <asp:ListItem Value="M">Misc.</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 90px;" class="center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px;">
                                                    </td>
                                                    <td class="textbold" style="width: 150px;" valign="top">
                                                        Date</td>
                                                    <td style="width: 180px;" colspan="2" valign="top">
                                                        <asp:DropDownList ID="drpDateSearchOn" runat="server" CssClass="dropdownlist" Width="170px"
                                                            TabIndex="15">
                                                            <asp:ListItem Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="1">Installation Date</asp:ListItem>
                                                            <asp:ListItem Value="2">Challan Date</asp:ListItem>
                                                            <asp:ListItem Value="5">Login Date</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td colspan="2" class="textbold" style="width: 300px; nowarp: true;" valign="top">
                                                        <asp:TextBox ID="txtDateFrom" runat="server" MaxLength="10" TabIndex="16" CssClass="textboxgrey"
                                                            Width="89px" ReadOnly="True"></asp:TextBox>&nbsp;&nbsp;<img id="f_DateFrom" alt=""
                                                                src="../Images/calender.gif" tabindex="17" title="Date selector" style="cursor: pointer" /><script
                                                                    type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_DateFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                </script>&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtDateTo" runat="server" MaxLength="10" TabIndex="18" CssClass="textboxgrey"
                                                            Width="89px" ReadOnly="True"></asp:TextBox>&nbsp;&nbsp;<img id="f_DateTo" alt=""
                                                                src="../Images/calender.gif"   style="cursor: pointer" tabindex="19" title="Date selector" />
                                                        &nbsp;

                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_DateTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script>

                                                    </td>
                                                    <td style="width: 90px;" class="center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" style="width: 50px">
                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdEmployeeId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdEmployeeName" runat="server" value="" style="width: 5px" />
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
                <td colspan="7" style="width: 1200px; padding-left: 4px;" valign="top">
                    <table cellpadding="0" cellspacing="0" style="width: 1200px">
                        <tr>
                            <td style="width: 1200px" class="redborder center">
                                <asp:GridView ID="gvChallanStatusPI" runat="server" AutoGenerateColumns="false" TabIndex="22"
                                    Style="width: 1200px;" HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left"
                                    AllowSorting="true" HeaderStyle-ForeColor="white">
                                    <Columns>
                                        <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name">
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Address " SortExpression="Address" DataField="Address">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="OfficeId" DataField="OFFICEID" SortExpression="OFFICEID">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date" DataField="DATE" SortExpression="DATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="CPu Type" DataField="CPUTYPE" SortExpression="CPUTYPE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cpu No." DataField="CPUNO" SortExpression="CPUNO">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mon Type" DataField="MONTYPE" SortExpression="MONTYPE">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mon No." DataField="MonNo" SortExpression="MonNo">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="KeyBoard No." DataField="KBDNO" SortExpression="KBDNO">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mouse No." DataField="MSENO" SortExpression="MSENO">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Order Number" DataField="OrderNumber" SortExpression="OrderNumber">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Challan Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpChallan" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Received" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan Date" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChallanDate" runat="server" MaxLength="10" TabIndex="16" CssClass="textboxgrey"
                                                    Width="112px" ></asp:TextBox>&nbsp;&nbsp;
                                                <img id="Img_ChallanDate"   alt="" runat="server" src="../Images/calender.gif" tabindex="17"
                                                    title="Date selector" style="cursor: pointer" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Order Type" DataField="OrderType" SortExpression="OrderType">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Amadeus PC" DataField="AmadeusPC" SortExpression="AmadeusPC">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Agency PC" DataField="AgencyPC" SortExpression="AgencyPC">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged By" DataField="LOGGEDBY" SortExpression="LOGGEDBY">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged Date" DataField="LOGGEDDATE" SortExpression="LOGGEDDATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModifyChallanStatus" runat="server" CssClass="LinkButtons"
                                                    CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "RowId") + "|" + DataBinder.Eval(Container.DataItem, "ChallanStatus")+  "|" + DataBinder.Eval(Container.DataItem, "CHALLANDATE") %>'>Edit</asp:LinkButton>
                                                <asp:HiddenField ID="hdLCode" runat="server" Value='<%#Eval("LCode")%>' />
                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")%>' />
                                                <asp:HiddenField ID="hdChallanType" runat="server" />
                                                <asp:HiddenField ID="hdChallanDate" runat="server" Value='<%#Eval("CHALLANDATE")%>' />
                                                <asp:HiddenField ID="hdPCDate" runat="server" Value='<%#Eval("DATE")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                    <AlternatingRowStyle CssClass="lightblue" />
                                </asp:GridView>
                                <asp:GridView ID="gvChallanStatusPD" runat="server" AutoGenerateColumns="false" TabIndex="22"
                                    Style="width: 1200px;" HeaderStyle-HorizontalAlign="Left"
                                    RowStyle-HorizontalAlign="Left" AllowSorting="true" HeaderStyle-ForeColor="white">
                                    <Columns>
                                        <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name">
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Address " DataField="Address" SortExpression="Address">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="OfficeId" DataField="OFFICEID" SortExpression="OFFICEID">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date" DataField="DATE" SortExpression="DATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date_DeInstall" DataField="DATE_Deinst" SortExpression="DATE_Deinst">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="CPu Type" DataField="CPUTYPE" SortExpression="CPUTYPE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cpu No." DataField="CPUNO" SortExpression="CPUNO">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mon Type" DataField="MONTYPE" SortExpression="MONTYPE">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mon No." DataField="MonNo" SortExpression="MonNo">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="KeyBoard No." DataField="KBDNO" SortExpression="KBDNO">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mouse No." DataField="MSENO" SortExpression="MSENO">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="DOrder Number" DataField="OrderNumber" SortExpression="OrderNumber">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Challan Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpChallan" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Received" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan Date" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChallanDate" runat="server" MaxLength="10" TabIndex="16" CssClass="textboxgrey"
                                                    Width="112px"></asp:TextBox>&nbsp;&nbsp;
                                                <img id="Img_ChallanDate" alt="" runat="server" src="../Images/calender.gif" tabindex="17"
                                                    title="Date selector" style="cursor: pointer" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Order Type" DataField="OrderType" SortExpression="OrderType">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Amadeus PC" DataField="AmadeusPC" SortExpression="AmadeusPC">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Agency PC" DataField="AgencyPC" SortExpression="AgencyPC">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged By" DataField="LOGGEDBY" SortExpression="LOGGEDBY">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged Date" DataField="LOGGEDDATE" SortExpression="LOGGEDDATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModifyChallanStatus" runat="server" CssClass="LinkButtons"
                                                    CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "RowId") + "|" + DataBinder.Eval(Container.DataItem, "ChallanStatus")+  "|" + DataBinder.Eval(Container.DataItem, "CHALLANDATE") %>'>Edit</asp:LinkButton>
                                                <asp:HiddenField ID="hdLCode" runat="server" Value='<%#Eval("LCode")%>' />
                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")%>' />
                                                <asp:HiddenField ID="hdChallanType" runat="server" />
                                                <asp:HiddenField ID="hdChallanDate" runat="server" Value='<%#Eval("CHALLANDATE")%>' />
                                                <asp:HiddenField ID="hdPCDate" runat="server" Value='<%#Eval("DATE_Deinst")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                    <AlternatingRowStyle CssClass="lightblue" />
                                </asp:GridView>
                                <asp:GridView ID="gvChallanStatusPR" runat="server" AutoGenerateColumns="false" TabIndex="22"
                                    Style="width: 1200px;" AllowSorting="true" HeaderStyle-HorizontalAlign="Left"
                                    RowStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                    <Columns>
                                        <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name">
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Address " DataField="Address" SortExpression="Address">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="OfficeId" DataField="OFFICEID" SortExpression="OFFICEID">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date" DataField="DATE" SortExpression="DATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date Repl." DataField="DATERepl" SortExpression="DATERepl">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="CPu Type" DataField="CPUTYPE" SortExpression="CPUTYPE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cpu No." DataField="CPUNO" SortExpression="CPUNO">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mon Type" DataField="MONTYPE" SortExpression="MONTYPE">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mon No." DataField="MonNo" SortExpression="MonNo">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="KeyBoard No." DataField="KBDNO" SortExpression="KBDNO">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mouse No." DataField="MSENO" SortExpression="MSENO">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="IOrder Number" DataField="OrderNumber" SortExpression="OrderNumber">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Challan Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpChallan" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Received" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan Date" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChallanDate" runat="server" MaxLength="10" TabIndex="16" CssClass="textboxgrey"
                                                    Width="112px"></asp:TextBox>&nbsp;&nbsp;
                                                <img id="Img_ChallanDate" alt="" runat="server" src="../Images/calender.gif" tabindex="17"
                                                    title="Date selector" style="cursor: pointer" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Order Type" DataField="OrderType" SortExpression="OrderType">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Amadeus PC" DataField="AmadeusPC" SortExpression="AmadeusPC">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Agency PC" DataField="AgencyPC" SortExpression="AgencyPC">
                                            <ItemStyle Width="10%" Wrap="True" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged By" DataField="LOGGEDBY" SortExpression="LOGGEDBY">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged Date" DataField="LOGGEDDATE" SortExpression="LOGGEDDATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModifyChallanStatus" runat="server" CssClass="LinkButtons"
                                                    CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "RowId") + "|" + DataBinder.Eval(Container.DataItem, "ChallanStatus")+  "|" + DataBinder.Eval(Container.DataItem, "CHALLANDATE") %>'>Edit</asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkHistory" runat="server" CssClass="LinkButtons" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "RowId")  %>'>History</asp:LinkButton>
                                                <asp:HiddenField ID="hdLCode" runat="server" Value='<%#Eval("LCode")%>' />
                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")%>' />
                                                <asp:HiddenField ID="hdChallanType" runat="server" />
                                                <asp:HiddenField ID="hdChallanDate" runat="server" Value='<%#Eval("CHALLANDATE")%>' />
                                                <asp:HiddenField ID="hdPCDate" runat="server" Value='<%#Eval("DATERepl")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                    <AlternatingRowStyle CssClass="lightblue" />
                                </asp:GridView>
                                <asp:GridView ID="gvChallanStatusMI" runat="server" AutoGenerateColumns="false" TabIndex="22"
                                    Style="width: 1200px;" HeaderStyle-HorizontalAlign="Left"
                                    RowStyle-HorizontalAlign="Left" AllowSorting="true" HeaderStyle-ForeColor="white">
                                    <Columns>
                                        <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name">
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Address " DataField="Address" SortExpression="Address">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="OfficeId" DataField="OFFICEID" SortExpression="OFFICEID">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date" DataField="DATE" SortExpression="DATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Equip Type" DataField="CPUTYPE" SortExpression="CPUTYPE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Equip No." DataField="CPUNO" SortExpression="CPUNO">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Challan Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpChallan" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Received" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan Date" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChallanDate" runat="server" MaxLength="10" TabIndex="16" CssClass="textboxgrey"
                                                    Width="112px"></asp:TextBox>&nbsp;&nbsp;
                                                <img id="Img_ChallanDate" alt="" runat="server" src="../Images/calender.gif" tabindex="17"
                                                    title="Date selector" style="cursor: pointer" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Logged By" DataField="LOGGEDBY" SortExpression="LOGGEDBY">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged Date" DataField="LOGGEDDATE" SortExpression="LOGGEDDATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModifyChallanStatus" runat="server" CssClass="LinkButtons"
                                                    CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "RowId") + "|" + DataBinder.Eval(Container.DataItem, "ChallanStatus")+  "|" + DataBinder.Eval(Container.DataItem, "CHALLANDATE") %>'>Edit</asp:LinkButton>
                                                <asp:HiddenField ID="hdLCode" runat="server" Value='<%#Eval("LCode")%>' />
                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")%>' />
                                                <asp:HiddenField ID="hdChallanType" runat="server" />
                                                <asp:HiddenField ID="hdChallanDate" runat="server" Value='<%#Eval("CHALLANDATE")%>' />
                                                <asp:HiddenField ID="hdPCDate" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                    <AlternatingRowStyle CssClass="lightblue" />
                                </asp:GridView>
                                <asp:GridView ID="gvChallanStatusMD" runat="server" AutoGenerateColumns="false" TabIndex="22"
                                    Style="width: 1200px;" HeaderStyle-HorizontalAlign="Left"
                                    RowStyle-HorizontalAlign="Left" AllowSorting="true" HeaderStyle-ForeColor="white">
                                    <Columns>
                                        <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name">
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Address" DataField="Address" SortExpression="Address">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="OfficeId" DataField="OFFICEID" SortExpression="OFFICEID">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date DeInstall" DataField="DATE_Deinst" SortExpression="DATE_Deinst">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Equip Type" DataField="CPUTYPE" SortExpression="CPUTYPE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Equip No." DataField="CPUNO" SortExpression="CPUNO">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Challan Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpChallan" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Received" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan Date" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChallanDate" runat="server" MaxLength="10" TabIndex="16" CssClass="textboxgrey"
                                                    Width="112px"></asp:TextBox>&nbsp;&nbsp;
                                                <img id="Img_ChallanDate" alt="" runat="server" src="../Images/calender.gif" tabindex="17"
                                                    title="Date selector" style="cursor: pointer" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Logged By" DataField="LOGGEDBY" SortExpression="LOGGEDBY">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged Date" DataField="LOGGEDDATE" SortExpression="LOGGEDDATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModifyChallanStatus" runat="server" CssClass="LinkButtons"
                                                    CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "RowId") + "|" + DataBinder.Eval(Container.DataItem, "ChallanStatus")+  "|" + DataBinder.Eval(Container.DataItem, "CHALLANDATE") %>'>Edit</asp:LinkButton>
                                                <asp:HiddenField ID="hdLCode" runat="server" Value='<%#Eval("LCode")%>' />
                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")%>' />
                                                <asp:HiddenField ID="hdChallanType" runat="server" />
                                                <asp:HiddenField ID="hdChallanDate" runat="server" Value='<%#Eval("CHALLANDATE")%>' />
                                                <asp:HiddenField ID="hdPCDate" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                    <AlternatingRowStyle CssClass="lightblue" />
                                </asp:GridView>
                                <asp:GridView ID="gvChallanStatusMR" runat="server" AutoGenerateColumns="false" TabIndex="22"
                                    Style="width: 1200px;" HeaderStyle-HorizontalAlign="Left"
                                    RowStyle-HorizontalAlign="Left" AllowSorting="true" HeaderStyle-ForeColor="white">
                                    <Columns>
                                        <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name">
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Address " DataField="Address" SortExpression="Address">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="OfficeId" DataField="OFFICEID" SortExpression="OFFICEID">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date Repl." DataField="DATERepl" SortExpression="DATERepl">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Equip Type" DataField="CPUTYPE" SortExpression="CPUTYPE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Equip No." DataField="CPUNO" SortExpression="CPUNO">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Challan Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpChallan" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Received" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan Date" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChallanDate" runat="server" MaxLength="10" TabIndex="16" CssClass="textboxgrey"
                                                    Width="112px"></asp:TextBox>&nbsp;&nbsp;
                                                <img id="Img_ChallanDate" alt="" runat="server" src="../Images/calender.gif" tabindex="17"
                                                    title="Date selector" style="cursor: pointer" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Logged By" DataField="LOGGEDBY" SortExpression="LOGGEDBY">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Logged Date" DataField="LOGGEDDATE" SortExpression="LOGGEDDATE">
                                            <ItemStyle Width="20%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModifyChallanStatus" runat="server" CssClass="LinkButtons"
                                                    CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "RowId") + "|" + DataBinder.Eval(Container.DataItem, "ChallanStatus")+  "|" + DataBinder.Eval(Container.DataItem, "CHALLANDATE") %>'>Edit</asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkHistory" runat="server" CssClass="LinkButtons" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "RowId")  %>'>History</asp:LinkButton>
                                                <asp:HiddenField ID="hdLCode" runat="server" Value='<%#Eval("LCode")%>' />
                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")%>' />
                                                <asp:HiddenField ID="hdChallanType" runat="server" />
                                                <asp:HiddenField ID="hdChallanDate" runat="server" Value='<%#Eval("CHALLANDATE")%>' />
                                                <asp:HiddenField ID="hdPCDate" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                    <AlternatingRowStyle CssClass="lightblue" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" valign="top"  style ="width:840px;">
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                            <td style="width: 30%" class="left">
                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                    ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                            <td style="width: 25%" class="right">
                                                <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                            <td style="width: 20%" class="center">
                                                <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                    ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                </asp:DropDownList></td>
                                            <td style="width: 25%" class="left">
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
    </form>
</body>
</html>
