<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_City.aspx.vb" Inherits="AMS_City_CRS_AS_CityUP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title> Add/Modify City </title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet"/>
      <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
     function  NewMSUPCity()
       {    
           window.location="MSUP_City.aspx?Action=I";
           return false;
       }  
    
     function CheckMandatoty()
    {
        if (document.getElementById("txtCtyCode").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="City code is mandatory.";
             document.getElementById("txtCtyCode").focus();
             return false;
        } 
           if (document.getElementById("txtCtyCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtCtyCode").value,1)==false)
            {
            document.getElementById("lblError").innerHTML="City code is not valid.";
            document.getElementById("txtCtyCode").focus();
            return false;
            } 
         }    
       
        if (document.getElementById("txtCityName").value.trim()=="")
        {           
          document.getElementById("lblError").innerHTML="City name is mandatory.";
          document.getElementById("txtCityName").focus();
          return false;
        }   
         if (  document.getElementById("txtCityName").value!="")
         {
           if(IsDataValid(document.getElementById("txtCityName").value,2)==false)
            {
            document.getElementById("lblError").innerHTML="City is not valid.";
            document.getElementById("txtCityName").focus();
            return false;
            } 
         }        
           
        if (document.getElementById("drpLstAOff").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Aoffice is mandatory.";
          document.getElementById("drpLstAOff").focus();
          return false;
        }    
        if (document.getElementById("drpLstState").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="State is mandatory."
          document.getElementById("drpLstState").focus();
          return false;
        }    
        if (document.getElementById("drpLstCountry").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Country is mandatory.";
          document.getElementById("drpLstCountry").focus();
          return false;
        }    
        
    }
    </script>
</head>
<body bgcolor="ffffff" >
    <form id="frmAddEditCity" runat="server" defaultfocus="txtCtyCode">
    
    <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Setup-></span><span class="sub_menu">City</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Manage City
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 293px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >                                                          
                                                            <tr> 
                                                                <td  class="textbold" colspan="6" align="center" height="22px" valign="middle">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr> 
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 147px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 98px; height: 22px;">
                                                                    City Code<span class ="Mandatory">*</span> </td>
                                                                <td width="15%" style="height: 22px" >
                                                                    
                                                                   <asp:TextBox ID="txtCtyCode" runat ="server" CssClass ="textbox" Width="175px" TabIndex="1" MaxLength="3"></asp:TextBox>
                                                                    </td>
                                                                <td width="10%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" TabIndex="6" Text="Save" AccessKey="A" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 147px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 98px; height: 22px;">
                                                                    City Name <span class ="Mandatory">*</span> </td>
                                                                <td width="15%" style="height: 22px" >
                                                                    
                                                                   <asp:TextBox ID="txtCityName" runat ="server" CssClass ="textbox" Width="175px" TabIndex="2" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                <td width="10%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="7"  AccessKey="N"/></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">  
                                                              </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 147px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 98px; height: 22px;">
                                                                    Aoffice<span class ="Mandatory">*</span>
                                                                </td>
                                                                <td class="textbold" style="height: 22px">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpLstAOff" runat="server" Width="180px" CssClass="dropdownlist" TabIndex="3">
                                                                    </asp:DropDownList></td>
                                                                <td  width="10%" style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="8" AccessKey="R" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">  
                                                              </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" height="22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 147px">
                                                                </td>
                                                                <td class="textbold" style="width: 98px">
                                                                    State<span class ="Mandatory">*</span> </td>
                                                                <td>
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpLstState" runat="server" Width="180px"  CssClass="dropdownlist" TabIndex="4">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" height="22px">
                                                                </td>
                                                                <td class="textbold" style="width: 147px">
                                                                </td>
                                                                <td class="textbold" style="width: 98px">
                                                                    Country<span class ="Mandatory">*</span> </td>
                                                                <td>
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpLstCountry" runat="server" Width="180px" CssClass="dropdownlist" TabIndex="5">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr height="5px">
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 147px">
                                                                </td>
                                                                <td style="width: 98px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                    &nbsp;</td>
                                                               <td  >
                                                                    </td>
                                                                <td  colspan="2" class="ErrorMsg" style="height: 23px">
                                                                    Field Marked * are Mandatory</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 4px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                  
                                                                    
                                                                    </td>
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
    </form>
</body>
</html>
