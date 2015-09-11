<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="PRDSR_AllCRS(MIDT).aspx.vb"
    Inherits="Productivity_PRDSR_AllCRS_MIDT_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Productivity</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">
 
     function AgencyValidation()
     {
           {debugger;}
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9 &&  whichASC !=13 && whichASC!=18)
        {
        document.getElementById("hdAgencyName").value="";
        document.getElementById("chbWholeGroup").disabled=true;
        document.getElementById("chbWholeGroup").checked=false;
    	}
     }
   
 function PopupAgencyPage()
 {
    var type;
    type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;
 }
 
  function New()
  {
      window.location.href="PRDSR_ProductivityDetailsAllCRS.aspx";      
      return false;
  }
 
      function DetailFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country,MncNonMnc)
     {
          var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear + "&Lcode=" + Lcode +  "&Aoff=" + Aoff + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country  + "&MNC=" + MncNonMnc ;
         type="PRD_BIDT_CRSDetails.aspx?Popup=T&" + parameter;   
         window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");     
         return false;        
                                           
     }
     function SelectCrsExcAvgFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {
     
                var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
                type = "PRD_BIDT_Details.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
 
    function validateNumeric()
        {
       var objdrpProductivity =document.getElementById('drpProductivity').value;
       var objtxtNumeric1 =document.getElementById('txtNumeric1');
       var objtxtNumeric2 =document.getElementById('txtNumeric2');       
       if (objdrpProductivity=='') 
       {
          objtxtNumeric1.disabled=true;
          objtxtNumeric2.disabled=true;
          objtxtNumeric1.value='0';
          objtxtNumeric2.value='0';
          objtxtNumeric1.className='textboxgrey';
          objtxtNumeric2.className='textboxgrey';
           document.getElementById("drpAirlineCode").selectedIndex=0;
           document.getElementById("drpAirlineCode").disabled=true;
       } 
    else if (objdrpProductivity=='7') 
       {
          objtxtNumeric1.disabled=false;
          objtxtNumeric2.disabled=false;
          objtxtNumeric1.value='0';
          objtxtNumeric2.value='0';
          objtxtNumeric1.className='textbox';
          objtxtNumeric2.className='textbox';  
           document.getElementById("drpAirlineCode").disabled=false;
       }   
       else 
       {
          objtxtNumeric1.disabled=false;
          objtxtNumeric2.disabled=true;
          objtxtNumeric1.value='0';
          objtxtNumeric2.value='';  
          objtxtNumeric1.className='textbox';
          objtxtNumeric2.className='textboxgrey';  
          document.getElementById("drpAirlineCode").disabled=false;
       }
       
//        if(document.getElementById("drpProductivity").selectedIndex=='0')
//        {
//            document.getElementById("txtNumeric1").disabled=true;
//            document.getElementById("txtNumeric2").disabled=true;
//            document.getElementById("drpAirlineCode").disabled=true;
//        }
//        else
//        {
//            document.getElementById("txtNumeric1").disabled=false;
//            document.getElementById("txtNumeric2").disabled=false;
//             document.getElementById("drpAirlineCode").disabled=false;
//        }
//        
//        
//        if(document.getElementById("drpProductivity").selectedIndex!='7')
//        {
//         document.getElementById("txtNumeric2").disabled=true;
//                 }
//        else
//        {
//            document.getElementById("txtNumeric2").disabled=false;
//        }

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
              
               
           if(document.getElementById('<%=txtNumeric1.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtNumeric1").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtNumeric1").focus();
                    return false;
                 }                  
              }
               if(document.getElementById('<%=txtNumeric2.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtNumeric2").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtNumeric2").focus();
                    return false;
                 }                  
              }
                 if(parseInt(document.getElementById("txtNumeric1").value) >parseInt(document.getElementById("txtNumeric2").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Productivity range is not valid.';          
                    document.getElementById("txtNumeric1").focus();
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
                    
              
     }
       
   

    </script>

</head>
<body onload="return ActDecLcodeChainCode();">
    <form id="form1" defaultbutton="btnSearch" defaultfocus="txtAgencyName" runat="server" >
    
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    <table width="860px" align="left" class="border_rightred" id="TABLE1" language="javascript">
            <tr>
                <td valign="top" style="width: 850px;">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Productivity-></span><span class="sub_menu">All CRS(MIDT)</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                Search All CRS Productivity</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 851px;" class="redborder" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                    &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" width="6%" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 194px; height: 25px;">
                                                                    Country</td>
                                                                <td style="width: 249px; height: 25px;">
                                                                    <asp:DropDownList ID="drpCountry1" TabIndex="1" runat="server"  onkeyup="gotop(this.id)" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px;">
                                                                    City</td>
                                                                <td style="width: 223px; height: 25px;">
                                                                    <asp:DropDownList ID="drpCity1" runat="server" TabIndex="1"  onkeyup="gotop(this.id)" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px; width: 176px;">
                                                                    <asp:Button ID="btnSearch" TabIndex="21" CssClass="button" runat="server" Text="Search" AccessKey="A" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" height="25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 194px">
                                                                    Agency Name
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server"
                                                                        TabIndex="1" Width="94%"></asp:TextBox>&nbsp;
                                                                    <img src="../Images/lookup.gif" alt="" tabindex="1" onclick="javascript:return PopupAgencyPage();" style="cursor:pointer;"  /></td>
                                                                <td style="width: 176px">
                                                                    <asp:Button ID="btnGraph" runat="server" CssClass="button" TabIndex="22" Text="Graph"  AccessKey="G" Height="24px"/></td>
                                                            </tr>
                                                            <tr valign="top" >
                                                                <td class="textbold" width="6%" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 194px;">
                                                                    Whole Group</td>
                                                                <td style="height: 25px; width: 249px;">
                                                                    <asp:CheckBox ID="chbWholeGroup" runat="server" TabIndex="1" CssClass="textbold"
                                                                        Width="144px" Style="position: relative" TextAlign="Left" /></td>
                                                                <td class="textbold" style="height: 25px; width: 132px;">
                                                                    Responsible Staff</td>
                                                                <td style="height: 25px; width: 223px;">
                                                                    <asp:DropDownList ID="drpResponsibleStaff" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px; width: 176px;">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="23" Text="Export"  AccessKey="E"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 194px;">
                                                                    Agency Status</td>
                                                                <td style="height: 25px; width: 249px;">
                                                                    <asp:DropDownList TabIndex="1" ID="drpAgencyStatus"  onkeyup="gotop(this.id)" runat="server" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="height: 25px; width: 132px;">
                                                                    Group Type</td>
                                                                <td style="height: 25px; width: 223px;">
                                                                    <asp:DropDownList ID="drpLstGroupType" runat="server" onkeyup="gotop(this.id)" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="186px">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 176px; height: 25px; vertical-align:top;" >
                                                                    <asp:Button ID="btnReset" TabIndex="24" CssClass="button" runat="server" Text="Reset"
                                                                         AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 194px; height: 25px">
                                                                    LCode</td>
                                                                <td style="width: 249px; height: 25px">
                                                                    <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1" Width="176px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                    Chain Code</td>
                                                                <td style="width: 223px; height: 25px">
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1" Width="178px"></asp:TextBox></td>
                                                                <td style="vertical-align: top; width: 176px; height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 194px; height: 25px;">
                                                                    Avg productivity</td>
                                                                <td style="height: 25px; width: 249px;">
                                                                    <asp:DropDownList TabIndex="1" ID="drpProductivity" runat="server" CssClass="dropdownlist"  
                                                                        Width="184px">
                                                                        <asp:ListItem>--All--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                        <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                        <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="7">Between</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox TabIndex="1" ID="txtNumeric1" runat="server" CssClass="textboxgrey"
                                                                        MaxLength="9" Width="60px" Enabled="False">0</asp:TextBox>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                                                                    <asp:TextBox ID="txtNumeric2" TabIndex="1" runat="server" CssClass="textboxgrey"
                                                                        MaxLength="9" Width="60px" Style="position: relative; top: 0px"
                                                                        Enabled="False">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="drpAirlineCode"
                                                                            Enabled="false" runat="server" CssClass="dropdownlist" Width="80px" Style="
                                                                            position: relative; top: 0px" TabIndex="1" >
                                                                            <asp:ListItem Value="1">Total</asp:ListItem>
                                                                            <asp:ListItem Value="2">1A</asp:ListItem>
                                                                            <asp:ListItem Value="3">1B</asp:ListItem>
                                                                            <asp:ListItem Value="4">1G</asp:ListItem>
                                                                            <asp:ListItem Value="5">1P</asp:ListItem>
                                                                            <asp:ListItem Value="6">1W</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 194px;">
                                                                    1 AOffice</td>
                                                                <td style="height: 25px; width: 249px;">
                                                                    <asp:DropDownList TabIndex="1" ID="drpAOffice" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="184px">
                                                                        <asp:ListItem>--All--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                        <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                        <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="7">Between</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 132px;">
                                                                    Region</td>
                                                                <td style="height: 25px; width: 223px;">
                                                                    <asp:DropDownList ID="drpRegion" runat="server" onkeyup="gotop(this.id)" TabIndex="1" CssClass="dropdownlist"
                                                                        Width="184px" Style="position: relative">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 194px; height: 25px">
                                                                    Agency Group Category</td>
                                                                <td style="width: 249px; height: 25px">
                                                                    <asp:DropDownList ID="drpAgencyType" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                    Agency Category</td>
                                                                <td style="width: 223px; height: 25px">
                                                                    <asp:DropDownList ID="drpGroupAgencyType" runat="server" onkeyup="gotop(this.id)" CssClass="dropdownlist"
                                                                        TabIndex="1" Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 194px; height: 25px">Company Vertical
                                                                </td>
                                                                <td style="width: 249px; height: 25px"><asp:DropDownList id="DlstCompVertical" tabIndex=1 onkeyup="gotop(this.id)" runat="server" CssClass="dropdownlist" Width="184px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td colspan="2" style="height: 25px">
                                                                    <asp:CheckBox ID="chkShowGroupClassification" runat="server" CssClass="textbold"
                                                                        TabIndex="1" Text="Show Agency Category" Width="152px" /></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 194px; height: 25px">
                                                                    From&nbsp;
                                                                </td>
                                                                <td style="width: 249px; height: 25px">
                                                                    <asp:DropDownList ID="drpMonthFrom" TabIndex="1" runat="server" CssClass="dropdownlist" 
                                                                        Width="104px" Style="position: relative">
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
                                                                    </asp:DropDownList>&nbsp;&nbsp;
                                                                    <asp:DropDownList TabIndex="1" ID="drpYearFrom" runat="server" CssClass="dropdownlist"
                                                                        Width="64px" Style="position: relative; left: 8px; top: 0px;" >
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                    To</td>
                                                                <td style="height: 25px; width: 223px;">
                                                                    <asp:DropDownList ID="drpMonthTo" runat="server" TabIndex="1" CssClass="dropdownlist" 
                                                                        Width="104px" Style="position: relative">
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
                                                                    </asp:DropDownList>&nbsp;&nbsp;
                                                                    <asp:DropDownList ID="drpYearTo" runat="server" TabIndex="1" CssClass="dropdownlist" 
                                                                        Width="72px" Style="position: relative; left: 0px; top: 0px;">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="height: 25px" colspan="2">
                                                                </td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                </td>
                                                                <td style="width: 223px; height: 25px">
                                                                </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                         <%-- <asp:Panel ID="pnlCount" runat="server" Visible ="false">  
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    <strong>
                                                                    Number of Records Found</strong></td>
                                                                <td style="width: 249px; height: 25px">
                                                                    <asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                                        ReadOnly="True" Style="position: relative" TabIndex="18" Text="0" Width="176px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                </td>
                                                                <td style="width: 223px; height: 25px">
                                                                </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            </asp:panel> --%>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                <asp:HiddenField ID="hd1A" runat="server" />
                                <asp:HiddenField ID="hd1B" runat="server" />
                                <asp:HiddenField ID="hd1G" runat="server" />
                                <asp:HiddenField ID="hd1P" runat="server" />
                                <asp:HiddenField ID="hd1W" runat="server" />
                                <asp:HiddenField ID="hdTotal" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
            
            
        </table>
    </td>
    <td>
    
    </td>
    </tr>
    
    
     <tr>
                <td valign="top" colspan="2" class="top border_rightred">
                    <table width="1200px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder">
                                <%-- <asp:GridView TabIndex="20" ID="grdMIDT" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AlternatingRowStyle-CssClass="lightblue" ShowFooter="true">--%>
                                <asp:GridView EnableViewState ="false" TabIndex="20" ID="grdMIDT" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" ShowFooter="true" HeaderStyle-CssClass="Gridheading"
                                    RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" AllowSorting="True"   HeaderStyle-ForeColor="white" >
                                    <Columns>
                                        <asp:BoundField DataField="LCODE" HeaderText="Location Code" SortExpression="LCODE">
                                            <ItemStyle Wrap="False" Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHAINCODE" HeaderText="Chain Code"  SortExpression="CHAINCODE">
                                            <ItemStyle Wrap="False" Width="5%" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="Group_Classification_Name" HeaderText="Type"  SortExpression="Group_Classification_Name" HeaderStyle-Wrap ="true">
                                            <ItemStyle Wrap="False" Width="5%" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="NAME" HeaderText="Agency Name" SortExpression="NAME">
                                            <ItemStyle Wrap="True" Width="13%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS" HeaderText="Address" SortExpression="ADDRESS">
                                            <ItemStyle Wrap="True" Width="13%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SALESEXECUTIVE" HeaderText="Sales Executive"  SortExpression="SALESEXECUTIVE">
                                            <ItemStyle Wrap="False" Width="9%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OFFICEID" HeaderText="Office ID" SortExpression="OFFICEID" >
                                            <ItemStyle Wrap="False" Width="8%" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="COMP_VERTICAL" HeaderText="Company Vertical" SortExpression="COMP_VERTICAL" >
                                            <ItemStyle Wrap="False" Width="3%" />
                                            <HeaderStyle wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY" >
                                            <ItemStyle Wrap="False" Width="7%" />
                                        </asp:BoundField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Country" SortExpression="COUNTRY">
                                            <ItemTemplate>
                                                <%#Eval("COUNTRY")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Tot" Text="Totals" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="1A" SortExpression="A">
                                            <ItemTemplate>
                                                <asp:Label ID="lblA" runat="server" Text='<%# Eval("A") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHA" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="1B" SortExpression="B">
                                            <ItemTemplate>
                                                <asp:Label ID="lblB" runat="server" Text='<%# Eval("B") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHB" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="1G" SortExpression="G">
                                            <ItemTemplate>
                                                <asp:Label ID="lblG" runat="server" Text='<%# Eval("G") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHG" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="1P" SortExpression="P">
                                            <ItemTemplate>
                                                <asp:Label ID="lblP" runat="server" Text='<%# Eval("P") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHP" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                       
                                      
                                        <asp:TemplateField HeaderText="1W" SortExpression="W">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW" runat="server" Text='<%# Eval("W") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHW" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" SortExpression="TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTOTAL" runat="server" Text='<%# Eval("TOTAL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHTOTAL" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>&nbsp;<a href="#"
                                                    class="LinkButtons" id="link1AActual" runat="server">1A Actual</a>
                                                <asp:HiddenField ID="hdAdd" runat="server" Value='<%# Eval("ADDRESS") %>' />
                                                <asp:HiddenField ID="hdCountry" runat="server" Value='<%# Eval("COUNTRY") %>' />
                                                <asp:HiddenField ID="hdLcode" runat="server" Value='<%#Eval("LCODE")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" Width="14%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <RowStyle CssClass="textbold" />
                                    <FooterStyle   CssClass="Gridheading"  />
                                </asp:GridView>
                            </td>
                        </tr>
                          <tr>                                                   
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
                                                     
                                                </tr>  
                                         
                    </table>
                 
                </td>
            </tr>
    
    </table>
    
        
    </form>
      <script  type ="text/javascript" language ="javascript" >
     
        if (document.getElementById("hdAgencyName").value=="")
              {
                  document.getElementById("chbWholeGroup").disabled=true;
                  document.getElementById("chbWholeGroup").checked==false;        
              }	 
        </script>          
</body>
</html>
