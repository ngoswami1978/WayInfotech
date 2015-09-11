<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_Godown.aspx.vb" Inherits="Inventory_InvUP_Godown" %>

 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Godown Name</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
 
</head>
<script language="javascript" type="text/javascript">
var st;
function City(s)
{

   var id;
   st=s;
   if(s=='drpCity')
   {
      id=document.getElementById('<%=drpCity.ClientId%>').value;
   }
   id=s+'|'+id;           
   CallServer(id,"This is context from client");
   return false;
}
  
function ReceiveServerData(args, context)
{
var objvar=args;
var Var_array=objvar.split("|");

    //document.getElementById("txtCountry").value=args;
   
    document.getElementById('<%=txtAoffice.ClientId%>').value=Var_array[1];
}
 function NewFunction()
    {   
        window.location.href="INVUP_Godown.aspx?Action=I";       
        return false;
    }
function ValidateGodown()
     {
    if(document.getElementById("txtGodownName").value.trim()=='')
    {
        document.getElementById("lblError").innerHTML='Godown Name is Mandatory.';
        document.getElementById("txtGodownName").focus();
        return false;
    }
     if(document.getElementById('<%=txtAddress.ClientId%>').value=="")
    {
        document.getElementById("lblError").innerHTML="Address is Mandatory.";
        document.getElementById("txtAddress").focus();
        return false;
    }
  if(document.getElementById('<%=txtPhone.ClientId%>').value.trim()=='')
    {
        document.getElementById("lblError").innerHTML='Phone is Mandatory.';
        document.getElementById("txtPhone").focus();
        return false;
    }
     if(document.getElementById('<%=txtPostalCode.ClientId%>').value.trim()=='')
    {
        document.getElementById("lblError").innerHTML='Postal Code is Mandatory.';
        document.getElementById("txtPostalCode").focus();
        return false;
    }
     if(document.getElementById("drpCity").selectedIndex==0)
    {
        document.getElementById("lblError").innerHTML='City is Mandatory.';
        document.getElementById("drpCity").focus();
        return false;
    }  
     }
    function gotop1(ddlname)
     {
    
     if (event.keyCode==46 )
     {
   
        document.getElementById(ddlname).selectedIndex=0;
         document.getElementById('<%=txtAoffice.ClientId%>').value="";
     }
     } 
</script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body>
    <form  defaultbutton="btnSave"  defaultfocus="txtGodownName" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top" style="height: 482px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Inventory-&gt;</span><span class="sub_menu">Godown</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Godown Name</td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 231px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder" style="height: 193px">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" style="height: 208px">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 25px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 11%; height: 27px;">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold" style="height: 27px">
                                                        Godown Name<span class="Mandatory">*</span></td>
                                                    <td colspan="2" style="width: 213px; height: 27px;">
                                                        <asp:TextBox ID="txtGodownName"  CssClass="textbox" TabIndex="1"
                                                            MaxLength="40" runat="server" Width="392px"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%" style="height: 27px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="7" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 11%; height: 66px">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold" style="height: 66px">
                                                        Address<span class="Mandatory">*</span></td>
                                                    <td colspan="2" style="width: 213px; height: 66px;">
                                                        <asp:TextBox ID="txtAddress"  CssClass="textbox" TabIndex="2"
                                                            MaxLength="40" runat="server" Width="392px" Height="40px" TextMode="MultiLine"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%"   style="height: 66px">
                                                        <asp:Button  ID="btn_New" CssClass="button" runat="server"  Text="New"    TabIndex="8" style="position: relative; left: 0px; top: -14px;" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 11%; height: 27px;">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold" style="height: 27px">
                                                        Phone<span class="Mandatory">*</span></td>
                                                    <td colspan="2" style="width: 213px; height: 27px;">
                                                        <asp:TextBox ID="txtPhone" TabIndex="3" CssClass="textbox" MaxLength="30" runat="server"
                                                            Width="160px"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%" style="height: 27px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset"  TabIndex="9" style="left: 0px; position: relative; top: -30px;" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 11%; height: 27px;">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold" style="height: 27px">
                                                        Postal Code<span class="Mandatory">*</span></td>
                                                    <td colspan="2" style="width: 213px; height: 27px;">
                                                      <span class="textbold"></span>
                                                      
                                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="textbox" MaxLength="6"
                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="4" Width="160px"></asp:TextBox></td>
                                                    <td width="18%" style="height: 27px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 11%; height: 27px;">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold" style="height: 27px">
                                                        City<span class="Mandatory">*</span></td>
                                                    <td colspan="2" style="width: 213px; height: 27px;">
                                                        <span class="textbold"><asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist"
                                                            Style="left: 0px; position: relative; top: 0px" Width="168px" AutoPostBack="True" TabIndex="5" onkeyup="gotop1(this.id)">
                                                        </asp:DropDownList>
                                                        </span></td>
                                                    <td width="18%" style="height: 27px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 27px; width: 11%;">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold"  style="height: 27px">
                                                        AOffice</td>
                                                    <td colspan="2" style="height: 27px; width: 213px;">
                                                        <span class="textbold">
                                                            <asp:TextBox ID="txtAoffice" runat="server"  ReadOnly="true" CssClass="textboxgrey"  MaxLength="40" Style="position: relative"
                                                                TabIndex="6" Width="160px"></asp:TextBox></span></td>
                                                    <td width="18%" style="height: 27px">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdID" runat="server" />
                            </td>
                        </tr>
                    </table>
                   
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
