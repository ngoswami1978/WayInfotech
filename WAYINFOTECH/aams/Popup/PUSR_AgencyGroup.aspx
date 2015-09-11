<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_AgencyGroup.aspx.vb" Inherits="Popup_PUSR_AgencyGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS:Manage Agency Group</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <base target="_self">
    <script language="javascript" type="text/javascript">
    function AGroupReset()
    {
        document.getElementById("txtGroupName").value="";
        document.getElementById("txtChainCode").value="";
        document.getElementById("drpCity").selectedIndex=0;        
        document.getElementById("drpLstGroupType").selectedIndex=0;
        document.getElementById("drpLstAoffice").selectedIndex=0;
        document.getElementById("chkMainGroup").checked=false
        document.getElementById("lblError").innerHTML="";    
        if ( document.getElementById("gvManageAgencyGroup")!=null)       
        document.getElementById("gvManageAgencyGroup").style.display ="none"; 
        document.getElementById("txtChainCode").focus();  
        return false;
    }
   function AGroupMandatory()
    {
        if (  document.getElementById("txtChainCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtChainCode").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Chain code is not valid.";
            document.getElementById("txtChainCode").focus();
            return false;
            } 
         }
          if (  document.getElementById("txtGroupName").value!="")
         {
           if(IsDataValid(document.getElementById("txtGroupName").value,9)==false)
            {
            document.getElementById("lblError").innerHTML="Group name is not valid.";
            document.getElementById("txtGroupName").focus();
            return false;
            } 
         }
         
         return true;
     }
    </script>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmAgency" runat="server"  defaultfocus ="txtChainCode">
        <table width="860px" align="left" height="486px" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">                        
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Agency Group</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height:25px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Chain Code</td>
                                                                <td width="30%" >
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textfield" TabIndex="1" MaxLength="6"></asp:TextBox></td>
                                                                <td width="12%" ></td>
                                                                <td width="21%"></td>
                                                                <td width="18%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="7" /></td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height:25px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Group Name</td>
                                                                <td width="63%" colspan ="3" >
                                                                     <asp:TextBox ID="txtGroupName" runat="server" CssClass="textfield" TabIndex="2" MaxLength="40"  Width="485px"></asp:TextBox></td>
                                                              
                                                                <td width="18%">
                                                                   <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="8" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height:25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" >
                                                                    City</td>
                                                                <td >
                                                                    <%--<asp:TextBox ID="txtCity" runat="server" CssClass="textfield" TabIndex="3" MaxLength="30"></asp:TextBox>--%><asp:DropDownList id="drpCity" tabIndex="3" runat="server" CssClass="dropdown"></asp:DropDownList></td>
                                                                <td class="textbold" >
                                                                    Group Type</td>
                                                                <td >
                                                                    <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdown" TabIndex="4">
                                                                    </asp:DropDownList></td>
                                                                <td >
                                                                     <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="9" Visible="False" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height:25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    Aoffice</td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpLstAoffice" runat="server" CssClass="dropdown" TabIndex="5">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" >
                                                                    Main Group&nbsp;</td>
                                                                <td class="textbold" >
                                                                    &nbsp;<asp:CheckBox ID="chkMainGroup" runat="server" TabIndex="6" /></td>
                                                                <td>
                                                                    </td>
                                                            </tr>
                                                            <tr >
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
                                                                <td colspan="6" height="4" align="center">
                                                                    <asp:GridView ID="gvManageAgencyGroup" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False"
                                                                        Width="95%" TabIndex="10">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Chain Code">                                                                                
                                                                                <ItemTemplate>
                                                                                   <asp:Label ID="lblChainCode" runat="server" Text='<%#Eval("Chain_Code")%>'></asp:Label>
                                                                                </ItemTemplate>                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Group Name">                                                                                
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblChainName" runat="server" Text='<%#Eval("Chain_Name")%>'></asp:Label>
                                                                                </ItemTemplate>                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="City">                                                                                
                                                                                <ItemTemplate>
                                                                                <%#Eval("City_Name")%>
                                                                                </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Group Type">                                                                                
                                                                                <ItemTemplate>
                                                                                <%#Eval("GroupTypeName")%>
                                                                                </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Aoffice">                                                                                
                                                                                <ItemTemplate>
                                                                                <%#Eval("Aoffice")%>
                                                                                </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action"  >                                                                                
                                                                                <ItemTemplate>                                                                                   
                                                                                    <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Chain_Code") + "|" + DataBinder.Eval(Container.DataItem, "Chain_Name") %>'>Select</asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                    </asp:GridView>
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
                                <asp:Literal ID="litAgencyGroup" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
