<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_ISP.aspx.vb" Inherits="ISP_MSUP_ISP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Update ISP</title>
    
    
     <script src="../JavaScript/AAMS.js" type="text/javascript" ></script>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
      <link href="../JavaScript/AAMS.js" type="text/javascript" language="javascript" />
      <link href="../JavaScript/AAMS.js" type="text/javascript" />
      
      <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

      
    <script type="text/javascript" language ="javascript" >   
       
    function validateISP()
    {
 
    if(document.getElementById("txtISPName").value=='')
    {
        document.getElementById("lblError").innerHTML='ISP Name is Mandatory.';
        document.getElementById("txtISPName").focus();
        return false;
    }
         if(document.getElementById("drpIspProvider").value=='')
    {
        document.getElementById("lblError").innerHTML='Provider Name is Mandatory.';
        document.getElementById("drpIspProvider").focus();
        return false;
    }
     if(document.getElementById("drpCityName").selectedIndex==0)
    {
        document.getElementById("lblError").innerHTML='City Name is Mandatory.';
        document.getElementById("drpCityName").focus();
        return false;
    }
    
    if(document.getElementById("txtCtcPerson").value.trim()=='')
    {
        document.getElementById("lblError").innerHTML='Contact Person is Mandatory.';
        document.getElementById("txtCtcPerson").focus();
        return false;
    }
    
    if(document.getElementById("txtAddress").value=='')
    {
        document.getElementById("lblError").innerHTML='Address is Mandatory.';
        document.getElementById("txtAddress").focus();
        return false;
    } 
    
    
   
    
      if(document.getElementById("txtPinCode").value.trim()=='')
    {
        document.getElementById("lblError").innerHTML='PIN Code is Mandatory.';
        document.getElementById("txtPinCode").focus();
        return false;
    }
    
    
    
     if(document.getElementById("txtPhoneNo").value.trim()=='')
    {
        document.getElementById("lblError").innerHTML='Phone Number is Mandatory.';
        document.getElementById("txtPhoneNo").focus();
        return false;
    }
    
     if(document.getElementById('<%=txtEmail.ClientID%>').value!='')
    {        
        if(checkEmail(document.getElementById('<%=txtEmail.ClientID%>').value)==false)
        {
            document.getElementById("lblError").innerHTML='Email id is not valid.';
            document.getElementById('<%=txtEmail.ClientID%>').focus();
            return false;
        }
    }    
   
    
      
    }
  
    </script>
   
</head>
<body >
    <form id="form1" runat="server">
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage <span style="font-family: Microsoft Sans Serif">ISP </span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">   <table style="width: 100%" border="0" cellpadding="1" cellspacing="2" class="left textbold">
                                            <tr>
                                                <td style="width: 16%">
                                                </td>
                                                <td class="textbold" style="width: 18%">
                                                </td>
                                                <td colspan="4" class="gap">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                <td >
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                            ISP Name<span class="Mandatory" >*</span></td>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtISPName" runat="server" CssClass="textbox" MaxLength="45" TabIndex="2" Width="97%"></asp:TextBox></td>
                                        <td>
                                                                    </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="10" AccessKey="S" /></td>
                                    </tr>
                                            <tr>
                                                <td style="width: 16%">
                                                </td>
                                                <td class="textbold" style="width: 18%">
                                            Provider Name<span class="Mandatory" >*</span></td>
                                                <td colspan="4">
                                            <asp:DropDownList ID="drpIspProvider" runat="server" CssClass="dropdownlist" Width="100%" TabIndex="2">
                                        </asp:DropDownList></td>
                                                <td>
                                                </td>
                                                <td style="width: 34%">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="11" AccessKey="N" /></td>
                                            </tr>
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                            City Name &nbsp;<span class="Mandatory" >*</span></td>
                                        <td colspan="4" class="textbold">
                                                                    <asp:DropDownList ID="drpCityName" runat="server" CssClass="dropdownlist" Width="100%" TabIndex="3">
                                                                    </asp:DropDownList></td>
                                        <td>
                                                                    </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="12" AccessKey="R" /></td>
                                    </tr>
                                    
                                    
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                           Contact Person <span class="Mandatory" >*</span></td>
                                        <td style="width: 25%" class="textbold" colspan="4" >
                                         <asp:TextBox ID="txtCtcPerson" runat="server" CssClass="textbox" MaxLength="40"  TabIndex="4" Width="98%"></asp:TextBox></td>
                                        
                                        <td>
                                                                    </td>
                                                                    <td> </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%; height: 21px;">
                                        </td>
                                        <td style="width: 18%; height: 21px;" class="textbold">
                                            Address<span class="Mandatory" >*</span></td>
                                        <td colspan="4" style="height: 21px">
                                                                   <asp:TextBox ID="txtAddress" runat ="server" CssClass ="textbox" Width="98%" MaxLength="255" TextMode="MultiLine" Height="48px" TabIndex="5"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td style="width: 34%; height: 21px;">
                                                                    </td>
                                    </tr>
                                    
                                        <tr>
                                        <td style="width: 16%; height: 21px;">
                                        </td>
                                        <td style="width: 18%; height: 21px;" class="textbold">
                                            PIN Code <span class="Mandatory" >*</span></td>
                                        <td colspan="4" style="height: 21px">
                                         <asp:TextBox ID="txtPinCode" runat="server" CssClass="textbox" MaxLength="10" Width="98%" TabIndex="6"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td style="width: 34%; height: 21px;">
                                                                    </td>
                                    </tr>    
                                            
                                        <tr>                                        
                                        <td style="width: 16%; height: 21px;">
                                        </td>
                                        <td style="width: 18%; height: 21px;" class="textbold">
                                           Phone No.<span class="Mandatory" >*</span></td>
                                        <td colspan="4" style="height: 21px">
                                         <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox" MaxLength="30" Width="98%" TabIndex="7"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td style="width: 34%; height: 21px;">
                                                                    </td>
                                    </tr>       
                                           
                                             
                                        <tr>                                        
                                        <td style="width: 16%; height: 21px;">
                                        </td>
                                        <td style="width: 18%; height: 21px;" class="textbold">
                                          Fax No.</td>
                                        <td colspan="4" style="height: 21px">
                                         <asp:TextBox ID="txtFaxNo" runat="server" CssClass="textbox" MaxLength="30" Width="98%" TabIndex="8"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td style="width: 34%; height: 21px;">
                                                                    </td>
                                    </tr>          
                                           <tr>                                        
                                        <td style="width: 16%; height: 21px;">
                                        </td>
                                        <td style="width: 18%; height: 21px;" class="textbold">
                                          Email ID</td>
                                        <td colspan="4" style="height: 21px">
                                         <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="100" Width="98%" TabIndex="9"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td style="width: 34%; height: 21px;">
                                                                    </td>
                                    </tr>      
                                           
                                            
                                       
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td class="ErrorMsg" colspan="5">
                                            Field Marked * are Mandatory</td>
                                        <td>
                                        </td>
                                        <td style="width: 34%">
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
