<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_CorporateCode.aspx.vb" Inherits="Order_MSSR_CorporateCode" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Corporate Code Search</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >
    function CorporateCodeReset()
    {        
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtCorporateCode").value=""; 
         document.getElementById("txtCorporateQualifier").value=""; 
        if (document.getElementById("gvCorporateCode")!=null) 
        document.getElementById("gvCorporateCode").style.display ="none"; 
        document.getElementById("txtCorporateCode").focus(); 
                
        return false;
    }
     function CorporateCodeMandatory()
    {
       if (  document.getElementById("txtCorporateQualifier").value!="")
         {
           if(IsDataValid(document.getElementById("txtCorporateQualifier").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Corporate qualifier is not valid.";
            document.getElementById("txtCorporateQualifier").focus();
            return false;
            } 
         } 
          if (  document.getElementById("txtCorporateCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtCorporateCode").value,6)==false)
            {
            document.getElementById("lblError").innerHTML="Corporate code is not valid.";
            document.getElementById("txtCorporateCode").focus();
            return false;
            } 
         } 
          return true;
     }
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff" >
    <form id="frmCorCode" runat="server"  defaultfocus="txtCorporateCode">
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Order-></span><span class="sub_menu">Corporate Code</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                               Corporate Code
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width:130px; height: 22px;">
                                                                    Corporate Code</td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtCorporateCode" runat ="server" CssClass ="textbox" Width="208px" MaxLength="2" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td width="15%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" AccessKey="A" runat="server" Text="Search" TabIndex="4" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width:130px; height: 22px;">
                                                                    Corporate Qualifier</td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtCorporateQualifier" runat ="server" CssClass ="textbox" Width="208px" MaxLength="1" TabIndex="2"></asp:TextBox>
                                                                    </td>
                                                                <td width="15%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="5" AccessKey="N" /></td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 130px; height: 22px;">
                                                                    Description</td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtDescription" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30" TabIndex="3"></asp:TextBox>
                                                                    </td>
                                                                <td width="21%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6"  AccessKey="R"/></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 130px; height: 22px;">
                                                              
                                                                    </td>
                                                                <td class="textbold" style="width: 192px; height: 22px;">
                                                                
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    </td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td style="width: 130px">
                                                                    &nbsp;</td>
                                                                <td style="width: 192px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    </td>
                                                            </tr>                                                           
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td style="width: 192px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    <asp:GridView ID="gvCorporateCode" runat="server"  AutoGenerateColumns="False" TabIndex="7" Width="100%"    >
                                                                     <Columns>
                                                                   
                                                                     <asp:BoundField DataField="CCode" HeaderText="Corporate Code"   />
                                                                     <asp:BoundField DataField="CQualifier" HeaderText="Corporate Qualifier " />
                                                                     <asp:BoundField DataField="Description"  HeaderText="Description" />                                                                     
                                                                                <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                               <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="EditX" CommandArgument='<%#Eval("ChainID")%>'
                                                                                                CssClass="LinkButtons">
                                                                                                Edit 
                                                                                            </asp:LinkButton>
                                                                                            &nbsp;
                                                                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="DeleteX" CommandArgument='<%#Eval("ChainID")%>' 
                                                                                                CssClass="LinkButtons">
                                                                                                Delete
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                                        <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                    </asp:TemplateField>
                                                                                         </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <RowStyle CssClass="textbold" />
                                                                        <HeaderStyle CssClass="Gridheading" />
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <!-- Code by Abhishek -->
    
  
    </form>
</body>
</html>
