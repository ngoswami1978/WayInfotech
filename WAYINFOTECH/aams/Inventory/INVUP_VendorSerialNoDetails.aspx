<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_VendorSerialNoDetails.aspx.vb" Inherits="Inventory_INVUP_VendorSerialNoDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Modify Vendor Serail No</title>
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
        if (document.getElementById("txtNewVenSNo").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="New Vendor Serial No. is mandatory.";
             document.getElementById("txtNewVenSNo").focus();
             return false;
        }      
      
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtNewVenSNo"  >  
    <table width="600px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Inventory -&gt;</span><span class="sub_menu">Modify Vendor Seraial
                                No</span></td>
                        </tr>
                         <tr>
                                    <td align ="right" >   <a href="#" class="LinkButtons" onclick="window.close(); window.opener.document.forms['form1']['btnSearch'].click();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                                  </tr>
                        <tr>
                            <td class="heading center" >
                                &nbsp;Modify Vendor Serail No.</td>
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
                                            <td class="textbold" style="width:25%">
                                               Product Name</td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtPrdName" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="1"
                                                    Width="135px" ReadOnly="True"></asp:TextBox></td>                                           
                                            <td style="width: 10%">
                                                &nbsp;</td>
                                            <td class="center" style="width: 20%">
                                               <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="4"  AccessKey="A"/></td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="width:20%">
                                            </td>
                                            <td class="textbold" style="width:25%">
                                                Serial No.</td>
                                            <td style="width: 25%">
                                            <asp:TextBox ID="txtSNo" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="1" Width="135px" ReadOnly="True"></asp:TextBox></td>
                                           
                                            <td style="width: 10%">
                                                </td>
                                            <td class="center" style="width: 20%">
                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                        </tr>
                                        <tr>
                                            <td  style="width:20%"></td>
                                            <td class="textbold" style="width:25%" >
                                                Vendor Serial No.</td>
                                            <td style="width:25%">             
                                                <asp:TextBox ID="txtVenSNo" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="1"
                                                    Width="135px" ReadOnly="True"></asp:TextBox></td>
                                             
                                            <td style="width:10%">
                                                &nbsp;</td>
                                            <td style="width:20%" class="center"> </td>
                                        </tr>
                                         <tr>
                                            <td  style="width:20%"></td>
                                            <td class="textbold" style="width:25%" >
                                                New Vendor Serial No.<span class="Mandatory" >*</span></td>
                                            <td style="width:25%">             
                                                <asp:TextBox ID="txtNewVenSNo" runat="server" CssClass="textbox" MaxLength="50" TabIndex="1"
                                                    Width="135px"></asp:TextBox></td>
                                            <td style="width:10%">
                                                &nbsp;</td>
                                            <td style="width:20%" class="center"> </td>
                                        </tr>
                                         <tr>
                                              <td >
                                            </td>
                                            <td colspan="4" class="ErrorMsg">
                                               </td>
                                          
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
                        <asp:HiddenField ID="hdProductId" runat="server" />
                                <asp:HiddenField ID="hdSerialno" runat="server" />
                                  <asp:HiddenField ID="hdVendorSerialNo" runat="server" />
                                <asp:HiddenField ID="hdProductName" runat="server" />
                                
                </td>
            </tr>
        </table>
    </form>
</body>
</html>