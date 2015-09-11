<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_Aoffice.aspx.vb" Inherits="Setup_MSUP_Aoffice" EnableEventValidation="false"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Manage Aoffice</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
     /*********************************************************************
                        Code for Call Back Information
    *********************************************************************/
   var st;
   function SendCustomerID(s)
   {
      var id
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
        document.getElementById("txtCountry").value=args;
    }
    /*********************************************************************
                        Method for Reset
    *********************************************************************/
    function AofficeReset()
    {
        document.getElementById("txtAofficeCode").value="";
        document.getElementById("txtAofficeAddress").value="";
        document.getElementById("drpCity").selectedIndex=0;
        document.getElementById("drpCountry").selectedIndex=0;
        document.getElementById("txtPhone").value="";
        document.getElementById("txtfax").value="";
        document.getElementById("txtPinCode").value="";        
        document.getElementById("drpRegionHQ").selectedIndex=0;
        document.getElementById("drpRegion").selectedIndex=0;
        document.getElementById("txtBDR").value="";
        return false;
    }
    /*********************************************************************
                        Method for check Mandatory Field
    *********************************************************************/
    function AofficeMandatory()
    {
        if(document.getElementById("txtAofficeCode").value=="")
        {
            document.getElementById("lblError").innerHTML="Please enter Aofficecode.";
            document.getElementById("txtAofficeCode").focus();
            return false;
        }
        if(IsDataValid(document.getElementById("txtAofficeCode").value,1)==false)
        {
            document.getElementById("lblError").innerHTML="Please enter valid Aofficecode.";
            document.getElementById("txtAofficeCode").focus();
            return false;
        }
        
        if(document.getElementById("drpCity").selectedIndex==0)
        {
            document.getElementById("lblError").innerHTML="Please select City.";
            document.getElementById("drpCity").focus();
            return false;
        }
//        if(IsDataValid(document.getElementById("txtPinCode").value,3)==false)
//        {
//            document.getElementById("lblError").innerHTML="Please enter valid pin code.";
//            document.getElementById("txtPinCode").focus();
//            return false;
//        } 
//        if(IsDataValid(document.getElementById("txtPhone").value,3)==false)
//        {
//            document.getElementById("lblError").innerHTML="Please enter valid phone number.";
//            document.getElementById("txtPhone").focus();
//            return false;
//        }              
//        if(IsDataValid(document.getElementById("txtfax").value,3)==false)
//        {
//            document.getElementById("lblError").innerHTML="Please enter valid fax number.";
//            document.getElementById("txtfax").focus();
//            return false;
//        }    
        if(document.getElementById("drpRegion").selectedIndex==false)
        {
            document.getElementById("lblError").innerHTML="Please select region.";
            document.getElementById("drpRegion").focus();
            return false;
        }
        if(IsDataValid(document.getElementById("txtBDR").value,3)==false)
        {
            document.getElementById("lblError").innerHTML="Please enter valid BDR Letter.";
            document.getElementById("txtfax").focus();
            return false;
        }
            
        return true;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="860px" align="left" style="height:486px" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Aoffice </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Aoffice
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                        </td>
                                        <td class="redborder" valign="top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="100%">
                                                        <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="4%" class="textbold" style="height: 28px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold" style="height: 28px">
                                                                    Aoffice Code <span class="Mandatory">*</span>
                                                                </td>
                                                                <td colspan="3" style="height: 28px">
                                                                    <span class="textbold">
                                                                        <asp:TextBox ID="txtAofficeCode" MaxLength="3" runat="server" CssClass="textbold" TabIndex="1"
                                                                            Width="62px" CausesValidation="True"></asp:TextBox></span></td>
                                                                <td rowspan="3" valign="top">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 19%">
                                                                        <tr>
                                                                            <td style="width: 148px; height: 25px; text-align: left" align="left">
                                                                                <asp:Button ID="btnSave" runat="server" TabIndex="11" CssClass="button" Text="Save" AccessKey="A" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 148px; height: 25px; text-align: left">
                                                                                <asp:Button ID="btnNew" runat="server" Text="New" TabIndex="12" CssClass="button" AccessKey="N" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 148px; height: 25px; text-align: left">
                                                                                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="13"  AccessKey="R"/></td>
                                                                        </tr>
                                                                    </table>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" rowspan="5">
                                                                    &nbsp; &nbsp; &nbsp;</td>
                                                                <td class="textbold" style="height: 23px">
                                                                    Address</td>
                                                                <td colspan="3" style="height: 23px">
                                                                    <asp:TextBox ID="txtAofficeAddress" MaxLength="255" runat="server" CssClass="textfield" Height="70px"
                                                                        TabIndex="2" Width="323px" TextMode="MultiLine"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    City <span class="Mandatory">*</span></td>
                                                                <td style="width: 255px" id="cboregionhead">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCity" runat="server" CssClass="dropdown" TabIndex="3">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 144px" class="textbold">
                                                                    Pin Code</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPinCode" runat="server" MaxLength="10" CssClass="textfield" TabIndex="4"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                    Country</td>
                                                                <td style="height: 23px; width: 255px;">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" ReadOnly="true" TabIndex="5"></asp:TextBox></td>
                                                                <td class="textbold">
                                                                    Phone</td>
                                                                <td style="height: 23px">
                                                                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="30" CssClass="textfield" TabIndex="6"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Fax</td>
                                                                <td >
                                                                    <asp:TextBox ID="txtfax" runat="server" MaxLength="30" CssClass="textfield" TabIndex="7"></asp:TextBox></td>
                                                                <td style="height: 23px; width: 144px;" class="textbold">
                                                                    Region HQ</td>
                                                                <td style="height: 23px">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpRegionHQ" EnableViewState="false" runat="server" CssClass="dropdown" TabIndex="8">
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="BOM" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="CCU" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="DEL" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                    Region<span class="Mandatory">*</span></td>
                                                                <td style="height: 23px; width: 255px;">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpRegion" runat="server" CssClass="dropdown" TabIndex="9">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 23px; width: 144px;" class="textbold">
                                                                    Max. no. of BDR Letter</td>
                                                                <td colspan="2" style="height: 23px" class="textfield">
                                                                    <asp:TextBox ID="txtBDR" runat="server" MaxLength="10" CssClass="textfield" TabIndex="10"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" class="ErrorMsg">
                                                                    Field Marked * are Mandatory</td>
                                                                <td style="width: 144px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
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
