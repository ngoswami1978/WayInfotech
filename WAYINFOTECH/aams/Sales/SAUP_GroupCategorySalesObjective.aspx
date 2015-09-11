<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SAUP_GroupCategorySalesObjective.aspx.vb" Inherits="Sales_SAUP_GroupCategorySalesObjective" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Sales::Manage  Agency Category Sales Objective </title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
      <script type="text/javascript" src="../JavaScript/AAMS.js">
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSave">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Sales-&gt;</span><span class="sub_menu">Manage  Agency Category Sales Objective</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Agency Category Sales Objective</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                <tr>
                                                    <td colspan="6" class="center gap">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                </tr>
                                            <tr>
                                                    <td width="10%" class="textbold" style="height: 25px">
                                                        &nbsp;</td>
                                                    <td width="15%" class="textbold" style="height: 25px">
                                                        Aoffice<span class="Mandatory">*</span></td>
                                                    <td style="width: 15%; height: 25px;" class="textbold">
                                                        <asp:DropDownList ID="drpAoffice" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                            position: relative; top: 0px" TabIndex="1" Width="160px" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 15%; height: 25px;" class="textbold">
                                                        <span class="textbold"> Agency Category</span><span class="Mandatory">*</span></td>
                                                    <td width="15%" style="height: 25px" class="textbold">
                                                            <asp:DropDownList ID="drpGroupCategory" runat="server" CssClass="dropdownlist" Style="left: 0px;position: relative; top: 0px" TabIndex="1" Width="160px" onkeyup="gotop(this.id)">
                                                    </asp:DropDownList></td>
                                                    <td width="30%" style="height: 25px; text-align: center;"  class="textbold">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="2" AccessKey="a" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 28px" width="6%">
                                                    </td>
                                                    <td class="textbold" style="height: 28px" width="15%">
                                                        Visit Count<span class="Mandatory">*</span></td>
                                                    <td style="width: 235px; height: 28px"  class="textbold">
                                                        <asp:TextBox ID="txtVisitCount" runat="server" CssClass="textbox"   Width="155px"  ></asp:TextBox></td>
                                                    <td style="width: 147px; height: 28px">
                                                    </td>
                                                    <td style="height: 28px" width="21%">
                                                    </td>
                                                    <td style="height: 28px; text-align: center;" width="18%">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="a" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 28px" width="6%">
                                                    </td>
                                                    <td class="textbold" style="height: 28px" width="15%">
                                                    </td>
                                                    <td style="width: 235px; height: 28px">
                                                    </td>
                                                    <td style="width: 147px; height: 28px">
                                                    </td>
                                                    <td style="height: 28px" width="21%">
                                                    </td>
                                                    <td style="height: 28px; text-align: center;" width="18%">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="a" /></td>
                                                </tr>
                                        
  
                                                <tr>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="text-align: center" class="ErrorMsg">
                                                        Field Marked * are Mandatory</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                                        <asp:HiddenField ID="hdID" runat="server" />
                                                        <asp:HiddenField ID="hdAOffice" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>

    <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
        
        if (document.getElementById("drpAoffice").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Aoffice is mandatory.";
          document.getElementById("drpAoffice").focus();
          return false;
        }        
        
        if (document.getElementById("drpGroupCategory").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="AgencyCategory is mandatory.";
          document.getElementById("drpGroupCategory").focus();
          return false;
        }        
        
        
        if(document.getElementById('<%=TxtVisitCount.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerHTML='Visit Count is mandatory.'
            document.getElementById('<%=TxtVisitCount.ClientId%>').focus();
            return false;
        }
        if ( document.getElementById('<%=TxtVisitCount.ClientId%>').value.trim()!="")
         {
             if(IsDataValid(document.getElementById('<%=TxtVisitCount.ClientId%>').value,3)==false)
             {
               document.getElementById("lblError").innerHTML="Visit count is not valid.";
               document.getElementById('<%=TxtVisitCount.ClientId%>').focus();
               return false;
             }  
        }  
        if ( document.getElementById('<%=TxtVisitCount.ClientId%>').value.trim()!="")
         {
                if ( parseInt(document.getElementById('<%=TxtVisitCount.ClientId%>').value,10) >31) 
                 { 
                    document.getElementById("lblError").innerHTML ="Visit count can't be greater than 31.";
                    document.getElementById('<%=TxtVisitCount.ClientId%>').focus();                  
                    return false;
                 }
        }         
        
       return true;         
    }      
    </script>
</body>
</html>
