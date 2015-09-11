<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPRPT_Feasibility.aspx.vb" Inherits="ISP_ISPRPT_Feasibility" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search ISP Feasibility Request</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
 <script language="javascript" type="text/javascript">
   function PopupAgencyPage()
{
//    var type;
//  type = "../Popup/PUSR_Agency.aspx" 
//  var strReturn;   
//  if (window.showModalDialog)
//  {
//      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
//  }
//  else
//  {
//      strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
//  }	   
//  if (strReturn != null)
//  {
//        var sPos = strReturn.split('|'); 
//        document.getElementById('<%=hidLcode.ClientID%>').value=sPos[0];        
//        document.getElementById('<%=txtAgencyName.ClientID%>').value=sPos[1];        
//        document.getElementById('<%=txtAgencyName.ClientID%>').focus();
//  }
          var type;
         // type = "../Popup/PUSR_Agency.aspx" ;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
}
function DummyFeasiblity()
{
if(document.getElementById('<%=chkDummyFeasiblity.ClientID%>').checked==true)
{
document.getElementById("txtAgencyName").disabled=true;
document.getElementById("txtAgencyName").className='textboxgrey';
document.getElementById('<%=hdSpan.ClientID%>').style.display='none';

}
else
{
document.getElementById("txtAgencyName").disabled=false;
document.getElementById("txtAgencyName").className='textbox';
document.getElementById('<%=hdSpan.ClientID%>').style.display='block';

}
}
function FeasibilityReset()
    {
    
        document.getElementById('<%=chkDummyFeasiblity.ClientID%>').checked=false;
        document.getElementById("txtAgencyName").value="";   
        document.getElementById("txtRequestId").value="";
        document.getElementById("ddlApprovedBy").selectedIndex=0;      
        document.getElementById("txtDateTo").value="";
        document.getElementById("txtDateFrom").value=""; 
        document.getElementById("ddlFeasibleStatus").selectedIndex=0;
        document.getElementById("txtIspName").value="";
        if(document.getElementById("gvISPFeasibilityRequest")!=null)  
        document.getElementById("gvISPFeasibilityRequest").style.display ="none";
        document.getElementById("hidIspId").value="";
        document.getElementById("hidLcode").value="";
        //document.getElementById("txtAgencyName").focus();
       
    }
 function PopupIspPage()
  {
 
//    var type;
//  type = "../Popup/PUSR_IspName.aspx" 
//  var strReturn;   
//  if (window.showModalDialog)
//  {
//      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
//  }
//  else
//  {
//      strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
//  }	   
//  if (strReturn != null)
//  {
//        var sPos = strReturn.split('|'); 
//        document.getElementById('<%=hidIspId.ClientID%>').value=sPos[0];        
//        document.getElementById('<%=txtIspName.ClientID%>').value=sPos[1];        
//        document.getElementById('<%=txtIspName.ClientID%>').focus();
//  }
//  
            type = "../ISP/MSSR_ISP.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
  }
</script>
<body onload ="DummyFeasiblity();" >
    <form id="form1" runat="server" defaultbutton="btnReportPrint" defaultfocus="txtRequestId">
    <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">ISP</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                ISP Feasibility Request Report
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                                    <td class="textbold" style="width:40px; height: 19px;">
                                                                        </td>
                                                                    <td class="textbold" style="height: 19px;" colspan="5">
                                                                        </td>
                                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 40px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 128px; height: 25px">
                                                        Agency Name</td>
                                                    <td colspan="3" style="height: 25px" nowrap="nowrap" id="td1" runat="server"  >
                                                     <table cellpadding="0" cellspacing="0" border="0">
                                                      <tr>
                                                       <td > <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="40"
                                                            TabIndex="1" Width="443px"></asp:TextBox></td>
                                                            <td >
                                                             <img id="hdSpan" runat="server" alt="" onclick="javascript:return PopupAgencyPage();"
                                                            src="../Images/lookup.gif" tabindex="2" /></td>
                                                              </tr>
                                                                </table>
                                                                </td> 
                                                               <td width="18%" style="height: 16px">  
                                                        <asp:Button ID="btnReportPrint" runat="server" CssClass="button" Text="Display" TabIndex="13" AccessKey="D" /></td>
                                                    <td style="height: 25px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 40px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 25px; width: 128px;">
                                                        Request ID</td>
                                                    <td style="width: 194px; height: 25px;">
                                                        <asp:TextBox ID="txtRequestId" runat="server" TabIndex="3" CssClass="textbox" ReadOnly="false" MaxLength="40"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 25px; width: 109px;" align="left">
                                                        Logged By</td>
                                                    <td style="height: 25px; width: 199px;">
                                                        <asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="dropdownlist" TabIndex="4"
                                                            Width="137px">
                                                        </asp:DropDownList>
                                                        </td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="14" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 40px;">
                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 128px;">
                                                        Request Date From</td>
                                                    <td style="height: 25px; width: 194px;">
                                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" MaxLength="40" TabIndex="5"></asp:TextBox>
                                                        <img id="Img1" tabindex="6" alt="" src="../Images/calender.gif" style="cursor: pointer" title="Date selector" /><script
                                                            type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField    :    '<%=txtDateFrom.clientId%>',
                                                                                                    ifFormat      :    "%d/%m/%Y",
                                                                                                    button        :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script></td>
                                                    <td class="textbold" style="height: 25px; width: 109px;" align="left">
                                                        Request Date To</td>
                                                    <td style="height: 25px; width: 199px;">
                                                        <asp:TextBox ID="txtDateTo" TabIndex="7" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox>
                                                        <img id="Img2"  tabindex="8" alt="" src="../Images/calender.gif" style="cursor: pointer" title="Date selector" /><script
                                                            type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField    :    '<%=txtDateTo.clientId%>',
                                                                                                    ifFormat      :    "%d/%m/%Y",
                                                                                                    button        :    "Img2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script></td>
                                                    <td style="height: 25px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 40px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 128px; height: 25px">
                                                        ISP Name</td>
                                                    <td style="width: 194px; height: 25px">
                                                        <asp:TextBox ID="txtIspName" runat="server" CssClass="textbox" MaxLength="40" TabIndex="9"></asp:TextBox>
                                                        <img id="Img3" runat="server" alt="" onclick="javascript:return PopupIspPage();"
                                                            src="../Images/lookup.gif"  tabindex="10"/></td>
                                                    <td align="left" class="textbold" style="width: 109px; height: 25px">
                                                        Feasibility Status</td>
                                                    <td style="width: 199px; height: 25px">
                                                        <asp:DropDownList ID="ddlFeasibleStatus" runat="server" CssClass="dropdownlist" TabIndex="11"
                                                            Width="137px">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 25px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 40px; height: 30px">
                                                    </td>
                                                    <td class="textbold" colspan="2" style="height: 30px">
                                                        <asp:CheckBox ID="chkDummyFeasiblity" runat="server" Text="Dummy Location " TextAlign="Left"
                                                            Width="248px"  TabIndex="12"/>
                                                        <asp:HiddenField ID="hidIspId" runat="server" />
                                                    </td>
                                                    <td align="left" class="textbold" style="width: 109px; height: 30px">
                                                    </td>
                                                    <td style="width: 199px; height: 30px">
                                                        <asp:HiddenField ID="hidLcode" runat="server" />
                                                        <asp:HiddenField ID="hdCountry" runat="server" />
                                                    </td>
                                                    <td style="height: 30px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 4px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
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
        
        <br />
    </form>
</body>
</html>
