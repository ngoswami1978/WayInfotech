<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_ChallanStatus.aspx.vb" Inherits="Inventory_INVUP_ChallanStatus" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Modify Challan Status</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
  <script language="javascript" type="text/javascript">
          function CheckMandatoty()
      {
        if (document.getElementById("drpChallanStatus").selectedIndex==0)
        {          
             document.getElementById("lblError").innerHTML="Challan Status is mandatory.";
             document.getElementById("drpChallanStatus").focus();
             return false;
        } 
         if (document.getElementById("txtChallanDate").value.trim()=='')
        {          
             document.getElementById("lblError").innerHTML="Challan Date is mandatory.";
             document.getElementById("txtChallanDate").focus();
             return false;
        }      
           
      
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="drpChallanStatus"  >  
    <table width="600px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Inventory -&gt;</span><span class="sub_menu">Modify Challan Status</span></td>
                        </tr>
                         <tr>
                                    <td align ="right" ><a href="#" class="LinkButtons" onclick="window.close(); window.opener.document.forms['form1']['btnSearch'].click();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                                  </tr>
                        <tr>
                            <td class="heading center" >
                                &nbsp;Modify Challan Status</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">                                 
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        
                                        <tr>
                                            <td class="center" colspan="5"  height="25px" >
                                              <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                        </tr>
                                            <tr>
                                            <td style="width:20%">
                                            </td>
                                            <td class="textbold" style="width:20%">
                                                Challan Status<span class="Mandatory" >*</span></td>
                                            <td style="width: 30%">
                                                <asp:DropDownList ID="drpChallanStatus" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                    Width="140px">
                                                </asp:DropDownList></td>                                           
                                            <td style="width: 10%">
                                                &nbsp;</td>
                                            <td class="center" style="width: 20%">
                                               <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="4" /></td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="width:20%">
                                            </td>
                                            <td class="textbold" style="width:20%">
                                                Challan Date<span class="Mandatory" >*</span></td>
                                            <td style="width: 30%">
                                                <asp:TextBox ID="txtChallanDate" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                    ReadOnly="True" TabIndex="12" Width="112px"></asp:TextBox>
                                                <img id="f_DateChallan" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                    tabindex="13" title="Date selector" />

                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtChallanDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_DateChallan",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                </script>

                                            </td>
                                           
                                            <td style="width: 10%">
                                                </td>
                                            <td class="center" style="width: 20%"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="4" Visible="False" /></td>
                                        </tr>
                                        <tr>
                                            <td  style="width:20%"></td>
                                            <td class="textbold" style="width:25%" >
                                                </td>
                                            <td style="width:25%">             </td>
                                             
                                            <td style="width:10%">
                                                &nbsp;</td>
                                            <td style="width:20%" class="center"> </td>
                                        </tr>
                                         <tr>
                                            <td  style="width:20%"></td>
                                            <td class="textbold" style="width:25%" >
                                              </td>
                                            <td style="width:25%">             </td>
                                            <td style="width:10%">
                                                &nbsp;</td>
                                            <td style="width:20%" class="center"> </td>
                                        </tr>
                                         <tr>
                                              <td >
                                            </td>
                                            <td colspan="4" class="ErrorMsg">
                                                <input id="hdChallanDate" runat="server" style="width: 5px" type="hidden" />
                                                <input id="hdPCDate" runat="server" style="width: 5px" type="hidden" /></td>
                                          
                                        </tr>    
                                        <tr>
                                              <td >
                                            </td>
                                            <td colspan="4" class="ErrorMsg">
                                                Field Marked * are Mandatory</td>
                                          
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