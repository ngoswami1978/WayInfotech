<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TARPT_Agency.aspx.vb" Inherits="TravelAgency_MSSR_Agency" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
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
    
      function CheckValidation()
    {
        if (document.getElementById("txtFielNumber").value!="")
         {
           if(IsDataValid(document.getElementById("txtFielNumber").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="File no is not valid.";
            document.getElementById("txtFielNumber").focus();
            return false;
            } 
         }  
        if(document.getElementById('<%=txtDateOffline.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDateOffline.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of offline is not valid.";			
	       document.getElementById('<%=txtDateOffline.ClientId%>').focus();
	       return(false);  
        }
         }  
            if(document.getElementById('<%=txtDateOnline.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDateOnline.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of online is not valid.";			
	       document.getElementById('<%=txtDateOnline.ClientId%>').focus();
	       return(false);  
        }
         }    
     }
    function AdvanceSearch()
    {           
        if(document.getElementById('<%=hdAdvanceSearch.ClientID%>').value=="1")
        {
            document.getElementById('btnUp').src="../images/down.jpg";            
            document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='none'
            document.getElementById('<%=hdAdvanceSearch.ClientID%>').value='0';
        }
        else
        {
            document.getElementById('btnUp').src='../images/up.jpg';           
            document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='block'
            document.getElementById('<%=hdAdvanceSearch.ClientID%>').value='1';
        }        
    }
    function OnloadAdvanceSearch()
    {            
       if(document.getElementById('<%=hdAdvanceSearch.ClientID%>').value=="1")
       {   
           document.getElementById('btnUp').src='../images/up.jpg';  
           document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='block'            
       }
       else
       {
          document.getElementById('btnUp').src="../images/down.jpg";
           document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='none'
       }
     }
     function AgencyReset()
    {
        document.getElementById("txtAgencyName").value=""; 
         document.getElementById("lblError").value=""; 
        document.getElementById("txtShortName").value="";       
        document.getElementById("txtOfficeId").value="";       
        document.getElementById("txtAddress").value="";       
        document.getElementById("txtDateOffline").value="";       
        document.getElementById("txtFax").value="";       
        document.getElementById("txtIATAId").value="";       
        document.getElementById("txtEmail").value="";       
        document.getElementById("txtDateOnline").value="";       
        document.getElementById("txtFielNumber").value="";  
        document.getElementById("drpOnlineStatus").selectedIndex=0; 
        document.getElementById("drpCity").selectedIndex=0;
        document.getElementById("drpCountry").selectedIndex=0;
        document.getElementById("drpAoffice").selectedIndex=0;
        document.getElementById("drpCRS").selectedIndex=0; 
        document.getElementById("drpAgencyStatus").selectedIndex=0;
        document.getElementById("drpAgencyType").selectedIndex=0;
        return false;
    }
    function EditFunction(CheckBoxObj)
    {          
        window.location.href="TAUP_Agency.aspx?Action=U|"+CheckBoxObj;       
        return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {          
            window.location.href="TASR_Agency.aspx?Action=D|"+CheckBoxObj;
            return false;
        }
    }
    function NewFunction()
    {   
        window.location.href="TAUP_Agency.aspx?Action=I";       
        return false;
    }   
    </script>
<body onload="javascript:OnloadAdvanceSearch();"  >
    <form id="form1" runat="server">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="860px" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <strong><span style="font-size: 9pt; font-family: Trebuchet MS">Travel Agency</span></strong><span class="menu">-&gt;</span><span class="sub_menu">Agency</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Agency
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table  border="0" width="860px" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td class="textbold" style="width: 20px" >
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 150px">
                                                      </td>
                                                    <td style="width: 200px" >
                                                      </td>
                                                    <td style="width: 120px" >
                                                      </td>
                                                    <td style="width: 180px" >
                                                        </td>
                                                    <td style="width: 210px">
                                                      </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 25px">
                                                        Agency
                                                        Name</td>
                                                    <td colspan="3" style="height: 25px">
                                                        <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server" Width="446px" TabIndex="1"></asp:TextBox><span class="textbold"></span></td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnPrint" CssClass="button" runat="server" Text="Display" TabIndex="10" AccessKey="D" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 25px">
                                                        Short Name</td>
                                                    <td style="height: 25px">
                                                        <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 25px">
                                                        City</td>
                                                    <td style="height: 25px">
                                                        <asp:DropDownList ID="drpCity" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="6">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        Office ID</td>
                                                    <td>
                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" MaxLength="40" TabIndex="3"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        Country</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpCountry" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="7">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" >
                                                        Online Status</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpOnlineStatus" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="4">
                                                        </asp:DropDownList></td>
                                                    <td  class="textbold">
                                                        Aoffice</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpAoffice" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="8">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold">
                                                        CRS</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpCRS" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="5">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        &nbsp;</td>
                                                    <td colspan="4" class="subheading"> 
                                                        <img src="../Images/down.jpg" style="cursor:pointer" id="btnUp" onclick="Javascript:return AdvanceSearch();" />&nbsp;&nbsp;<a href="#" onclick="Javascript:return AdvanceSearch();" class="menu">Advance Search</a>&nbsp;
                                                    </td>
                                                    
                                                    <td >
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        
                                                            <table ID="pnlAdvanceSearch" runat="server" style="width: 860px" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 20px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 150px">
                                                                        Address</td>
                                                                    <td style="width: 200px">
                                                                        <asp:TextBox ID="txtAddress" CssClass="textbox" MaxLength="40" runat="server" TabIndex="12"></asp:TextBox></td>
                                                                    <td style="width: 120px" >
                                                                        Agency Status</td>
                                                                    <td style="width: 180px"><asp:DropDownList ID="drpAgencyStatus" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="17">
                                                                    </asp:DropDownList></td>
                                                                    <td style="width: 210px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Agency Type</td>
                                                                    <td ><asp:DropDownList ID="drpAgencyType" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="13">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Email</td>
                                                                    <td >
                                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="40" TabIndex="18"></asp:TextBox></td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Date Offline</td>
                                                                    <td >
                                                                        <asp:TextBox ID="txtDateOffline" runat="server" CssClass="textbox" MaxLength="40" TabIndex="14"></asp:TextBox>
                                                                        <img id="imgDateOffline" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOffline.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOffline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td  class="textbold">
                                                                        Date Online</td>
                                                                    <td >
                                                                        <asp:TextBox ID="txtDateOnline" runat="server" CssClass="textbox" MaxLength="40" TabIndex="19"></asp:TextBox>
                                                                        <img id="imgDateOnline" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOnline.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px">
                                                                        Fax</td>
                                                                    <td style="height: 25px">
                                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="textbox" MaxLength="40" TabIndex="15"></asp:TextBox></td>
                                                                    <td class="textbold" style="height: 25px" >
                                                                        File Number</td>
                                                                    <td style="height: 25px">
                                                                        <asp:TextBox ID="txtFielNumber" runat="server" CssClass="textbox" MaxLength="4" TabIndex="20"></asp:TextBox></td>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        IATA ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIATAId" runat="server" CssClass="textbox" MaxLength="20" TabIndex="16"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td >
                                                                        </td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        
                                                    <asp:HiddenField ID="hdAdvanceSearch" EnableViewState="true" runat="server" />
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
    </form>
</body>
</html>
