<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyGenOfficeId.aspx.vb"
    Inherits="TravelAgency_MSGen_OfficeId" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate Office Id</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
    
</head>

<script language="javascript" type="text/javascript">
 function PopupCorporateCode() 
    {
   
   
//   var type;
//    type = "../Popup/PUSR_CorporateCode.aspx"; 
//      var strReturn;   
//  if (window.showModalDialog)
//{     
//    strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
//   }
// else
//  {     
//     strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
//  }	   
// if (strReturn != null)
//  {
//     var sPos = strReturn.split('|'); 
//     document.getElementById('<%=ddlCorporateCode.ClientID%>').value=sPos[0];
//   } 
   type = "MSSR_CorporateCode.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;  
}  
  function ChkMandatory()
      {
      if(document.getElementById('<%=drpCity.ClientID%>').value=="")
      {
       document.getElementById("lblError").innerHTML="Enter City";
      document.getElementById("drpCity").focus();
      return false;
      }
      if(document.getElementById('<%=ddlCorporateCode.ClientID%>').value=="")
      {
       document.getElementById("lblError").innerHTML="Enter Corporate Code";
      document.getElementById("ddlCorporateCode").focus();
      return false;
        }
         if(document.getElementById('<%=txtCorporateQualifier.ClientID%>').value=="")
      {
      document.getElementById("lblError").innerHTML="Enter Corporate Qualifier.";
      document.getElementById("txtCorporateQualifier").focus();
      return false;
        }
        if(document.getElementById('<%=ddlOfficeType.ClientID%>').value=="")
      {
      document.getElementById("lblError").innerHTML="Enter Office Type.";
      document.getElementById("ddlOfficeType").focus();
      return false;
        }
        
       
        if(IsDataValid(document.getElementById("txtCorporateQualifier").value,10)==false)
    {
         document.getElementById("lblError").innerHTML="Coprporate Qualifier is not valid.";
         document.getElementById("txtCorporateQualifier").focus();
         return false;
    }
        
        }
       
</script>


<body >
    <form id="form1"  defaultfocus="drpCity" defaultbutton="btnGenerate" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top" style="width: 857px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Office Id</span>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Generate Office Id</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" class="textbold" style="height: 23px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 57px; height: 23px;">
                                                    </td>
                                                    <td class="textbold" style="width: 150px; height: 23px;">
                                                        Office Id<span class="Mandatory">*</span></td>
                                                    <td style="width: 192px; height: 23px">
                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textboxgrey" Width="160px"
                                                            MaxLength="50" TabIndex="1" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td width="30%" style="height: 23px">
                                                        <asp:Button ID="btnGenerate" CssClass="button" runat="server" Text="Generate" TabIndex="6" AccessKey="G" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 15px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" class="textbold" style="height: 23px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 57px; height: 23px;">
                                                    </td>
                                                    <td class="textbold" style="width: 150px; height: 23px;">
                                                        City<span class="Mandatory">*</span></td>
                                                    <td style="width: 192px; height: 23px">
                                                        <asp:DropDownList ID="drpCity" TabIndex="2" CssClass="dropdownlist" Width="168px"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="30%" style="height: 23px">
                                                        <asp:Button ID="btn_Reset" CssClass="button" runat="server" Text="Reset" TabIndex="7" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 15px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 23px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 57px; height: 23px;">
                                                    </td>
                                                    <td class="textbold" style="width: 150px; height: 23px;">
                                                        Corporate Code<span class="Mandatory">*</span>
                                                    </td>
                                                    <td class="textbold" style="width: 192px; height: 23px;">
                                                        <asp:TextBox ID="ddlCorporateCode" runat="server"    CssClass="textboxgrey" MaxLength="1"
                                                            TabIndex="4" Width="160px"></asp:TextBox>
                                                        <img src="../Images/lookup.gif"  alt=""  onclick="javascript:return PopupCorporateCode();" style="cursor:pointer;" />
                                                                                                               
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 15px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 23px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 57px; height: 23px;">
                                                    </td>
                                                    <td class="textbold" style="width: 150px; height: 23px;">
                                                        Corporate Qualifier<span class="Mandatory">*</span></td>
                                                    <td class="textbold" style="width: 192px; height: 23px;">
                                                        <asp:TextBox ID="txtCorporateQualifier" runat="server"    CssClass="textboxgrey"   Width="160px" 
                                                            MaxLength="1" TabIndex="4"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 15px" >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" class="textbold" style="height: 23px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 57px; height: 23px;">
                                                    </td>
                                                    <td class="textbold" style="width: 150px; height: 23px;">
                                                        Office Type<span class="Mandatory">*</span></td>
                                                    <td style="width: 192px; height: 23px">
                                                        <asp:DropDownList ID="ddlOfficeType" TabIndex="5" CssClass="dropdownlist" Width="168px"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 15px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 57px">
                                                    </td>
                                                    <td colspan="2" class="ErrorMsg" style="height: 23px">
                                                        Field Marked * are Mandatory</td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                         
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
