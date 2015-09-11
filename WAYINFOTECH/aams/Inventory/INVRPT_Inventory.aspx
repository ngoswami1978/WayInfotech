<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVRPT_Inventory.aspx.vb" Inherits="Inventory_INVRPT_Pc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory PC Report </title>
   <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     </head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" language="javascript">
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
        
        function InventoryReset()
        {
        document.getElementById('<%=chbWholeGroup.ClientID%>').checked=false;
        document.getElementById("INV_Report_0").checked=true;
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtAgencyName").value="";   
        document.getElementById("drpCity").selectedIndex=0;
        document.getElementById("drpCountry").selectedIndex=0;      
        document.getElementById("drpAoffice").selectedIndex=0; 
        document.getElementById("drpRegion").selectedIndex=0; 
        document.getElementById("drpOnlineStatus").selectedIndex=0;
        document.getElementById("drpEquipmentGroup").selectedIndex=0;
        document.getElementById("txtEquipmentType").value="";
        document.getElementById("txtInstallationFrom").value="";
        document.getElementById("txtInstallationTo").value="";
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
        if(document.getElementById('<%=txtInstallationFrom.ClientId%>').value != '' && document.getElementById('<%=txtInstallationTo.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtInstallationFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtInstallationTo.ClientId%>').value,"d/M/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation From should be lesser than Date of Installation To .";			
	       document.getElementById('<%=txtInstallationTo.ClientId%>').focus();
	       return(false);  
        }
       }  
//         if(document.getElementById('<%=txtInstallationFrom.ClientId%>').value>document.getElementById('<%=txtInstallationTo.ClientId%>').value)
//         {
//         document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Installation From should be lesser than Date of Installation To .";			
//	       document.getElementById('<%=txtInstallationFrom.ClientId%>').focus();
//	       return(false);  
//         }
  }
  
  function disableChkbox()
  {
   	        document.getElementById("chbWholeGroup").disabled = true;
   	     
  }
    </script>
<body onload="return disableChkbox();">
    <form id="form1" runat="server" defaultbutton="btnDisplay" defaultfocus="txtAgencyName">
        <table width="860px"  align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="860px" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Inventory-&gt;</span><span class="sub_menu">Inventory Reports</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                <span>Inventory</span><span> Report</span></td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center" >
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 860px" class="left">
                                                <tr>
                                                    <td colspan="7" align="center" style="height: 25px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                </tr>
                                              
                                                <tr>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 150px; height: 18px;" class="textbold">
                                                        Agency Name
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txtAgencyName" runat="server" MaxLength="40" Width="512px" TabIndex="1"
                                                            CssClass="textbox"></asp:TextBox>
                                                            <img tabindex="2" src="../Images/lookup.gif"
                                                                onclick="javascript:return PopupAgencyPage();" style="position: relative;" /></td>
                                                    <td style="width: 110px;">
                                                        <asp:Button ID="btnDisplay" CssClass="button" runat="server" Text="Print" TabIndex="16" AccessKey="p" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 20px">
                                                        &nbsp;</td>
                                                    <td style="width: 150px;" class="textbold">
                                                        Whole Group</td>
                                                    <td class="textbold" style="width: 190px">
                                                       <asp:CheckBox  ID="chbWholeGroup" runat="server"  TextAlign="Left" Width="125px" TabIndex="2" /></td>
                                                    <td style="width: 10px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 130px">
                                                        Country
                                                    </td>
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="drpCountry" runat="server" Width="176px" TabIndex="3" CssClass="dropdownlist" onkeyup="gotop(this.id)" >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="17" AccessKey="r" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                    </td>
                                                    <td class="textbold" style="width: 150px; height: 18px;">
                                                        City
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="drpCity" runat="server" Width="158px" TabIndex="4" CssClass="dropdownlist" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td class="textbold">
                                                        Region</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="5" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                    </td>
                                                    <td class="textbold" style="width: 150px; height: 18px">
                                                        1a Office</td>
                                                    <td class="textbold">
                                                         <asp:DropDownList ID="drpAoffice" runat="server" CssClass="dropdownlist" TabIndex="6" Width="158px" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td class="textbold">
                                                        Online Status</td>
                                                    <td><asp:DropDownList ID="drpOnlineStatus" runat="server" Width="176px" TabIndex="7" CssClass="dropdownlist" onkeyup="gotop(this.id)">
                                                    </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 150px; height: 18px;" class="textbold">
                                                       Equipment Group</td>
                                                    <td>
                                                    <asp:DropDownList ID="drpEquipmentGroup" runat="server" CssClass="dropdownlist" 
                                                            TabIndex="8" Width="157px" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        Equipment Type</td>
                                                    <td>
                                                        <asp:TextBox ID="txtEquipmentType" runat="server" CssClass="textbox" MaxLength="40" ReadOnly="false"
                                                             TabIndex="9" Width="168px"></asp:TextBox>
                                                        <img tabindex="10" src="../Images/lookup.gif" onclick="javascript:return PopupEquipment();"
                                                            style="position: relative" />
                                                            </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 150px; height: 18px;" class="textbold">
                                                        From</td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtInstallationFrom" runat="server" CssClass="textbox" MaxLength="40" TabIndex="11" Width="151px"></asp:TextBox>
                                                        <img id="Img1" alt="" src="../Images/calender.gif" style="cursor: pointer;
                                                            position: relative" tabindex="12" title="Date selector" />
                                                             <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtInstallationFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        To</td>
                                                    <td>
                                                        <asp:TextBox ID="txtInstallationTo" runat="server" CssClass="textbox" MaxLength="40"
                                                            Style="left: 1px; position: relative; top: 0px;" TabIndex="13" Width="169px"></asp:TextBox>
                                                        <img id="Img2" alt="" src="../Images/calender.gif"  tabindex="14" title="Date selector" />
                                                             <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtInstallationTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="subheading" colspan="7" align="center" style="height: 15px">
                                                       Reports
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td style="height: 8px;" class="textbold" colspan="7" width="860px">
                                                        <asp:RadioButtonList ID="INV_Report" RepeatDirection="Horizontal" RepeatColumns="3"  runat="server"   Width="800px" TabIndex="15"   >
                                                        <asp:ListItem Selected="True">PC Report </asp:ListItem>
                                                        <asp:ListItem>Misc Hardware Report</asp:ListItem>
                                                        <asp:ListItem>PC Summary Report</asp:ListItem>
                                                        <asp:ListItem>PC Deinstallation Report</asp:ListItem>
                                                        <asp:ListItem>Misc Deinstallation Report</asp:ListItem>
                                                         <asp:ListItem>Misc Summary Report</asp:ListItem>
                                                        </asp:RadioButtonList>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="hidLcode" runat="server" />
                                            &nbsp;
                                          
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
