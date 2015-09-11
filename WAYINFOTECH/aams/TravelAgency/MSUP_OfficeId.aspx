<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_OfficeId.aspx.vb" Inherits="TravelAgency_MSUP_OfficeId" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS:Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript"> 
          
//        function NewFunction()
//         {   
//    
//        window.location.href="MSUP_OfficeId.aspx?Action=I";       
//        return false;
//           }
    function GenerateFunction()
    {
    window.location.href="MSUP_AgencyGenOfficeId.aspx?Action=I";
    return false;
    }
     function CheckValidation()
  {
  if(document.getElementById('<%=txtDOP.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDOP.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of Date Of Processing is not valid.";			
	       document.getElementById('<%=txtDOP.ClientId%>').focus();
	       return(false);  
        }
        }  
  
  }
  
  function dopjs()
  {
  
  if(document.getElementById('<%=txtDOP.ClientId%>').value=="")
  {
 document.getElementById("imgDateOffline").style.visibility='visible';
  document.getElementById("imgDateOffline").disabled=false;
 }
 if(document.getElementById('<%=txtDOP.ClientId%>').value!= "")
   {
   //document.getElementById("imgDateOffline").style.visibility='hidden';
    document.getElementById("imgDateOffline").disabled=true;
    }
    }
</script>

</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body  onload="dopjs()" >
    <form id="UPOfficeId" defaultbutton="btnSave" defaultfocus="txtCID" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Office Id</span>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Update Office Id</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder" style="height: 209px">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" style="height: 208px">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 11%; height: 25px;">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold" style="height: 25px">
                                                        Office Id</td>
                                                    <td colspan="2" style="width: 213px; height: 25px;">
                                                        <asp:TextBox ID="txtOfficeId" ReadOnly="true" CssClass="textboxgrey" TabIndex="1"
                                                            MaxLength="40" runat="server" Width="160px"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%" style="height: 25px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" AccessKey="S" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 11%; height: 28px">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold" style="height: 28px">
                                                        Agency Name</td>
                                                    <td colspan="2" style="width: 213px; height: 28px;">
                                                        <asp:TextBox ID="txtAgencyName" ReadOnly="true" CssClass="textboxgrey" TabIndex="2"
                                                            MaxLength="40" runat="server" Width="392px" Height="16px"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%" style="height: 28px">
                                                        <asp:Button ID="btn_New" CssClass="button" runat="server" Text="New" AccessKey="N" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px" style="width: 11%">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold">
                                                        CID</td>
                                                    <td colspan="2" style="width: 213px">
                                                        <asp:TextBox ID="txtCID" TabIndex="3" CssClass="textbox" MaxLength="40" runat="server"
                                                            Width="160px"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px" style="width: 11%">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold">
                                                        Date Of Processing</td>
                                                    <td colspan="2" style="width: 213px">
                                                        <asp:TextBox ID="txtDOP" TabIndex="4" CssClass="textbox" MaxLength="40" runat="server"
                                                            Width="160px"></asp:TextBox>
                                                        <span class="textbold"></span><img  id="imgDateOffline"
                                                                alt="" src="../Images/calender.gif" title="Date selector" style="cursor: pointer" />

                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDOP.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOffline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script>

                                                    </td>
                                                    <td width="18%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px" style="width: 11%">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold">
                                                        Terminal ID</td>
                                                    <td colspan="2" style="width: 213px">
                                                        <asp:TextBox ID="txtTerminalId" TabIndex="5" CssClass="textbox" MaxLength="40" runat="server"
                                                            Width="160px"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 44px; width: 11%;">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold" style="height: 44px">
                                                        Remarks</td>
                                                    <td colspan="2" style="height: 44px; width: 213px;">
                                                        <asp:TextBox ID="txtRemarks" TabIndex="6" CssClass="textfield" MaxLength="40" runat="server"
                                                            Width="392px" TextMode="MultiLine" Height="40px"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%" style="height: 44px">
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
