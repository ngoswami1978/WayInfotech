<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_CorporateCode.aspx.vb" Inherits="Popup_PUSR_CorporateCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Corporate Code Search</title>
      <base target="_self"/>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >
    function CorporateCodeReset()
    {        
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtCorporateCode").value=""; 
         document.getElementById("txtCorporateQualifier").value=""; 
         document.getElementById("txtDescription").value=""; 
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
                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Corporate Code</span>
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
                                                                <td style="width:20%" class="textbold" >
                                                                    &nbsp;</td>
                                                                <td class="textbold"  style="width:5%" >
                                                                    </td>
                                                                <td class="textbold" style="width:15%">
                                                                    <span class="textbold">Corporate Code</span></td>
                                                                <td style="width:20%" >
                                                                   <asp:TextBox ID="txtCorporateCode" runat ="server" CssClass ="textbox" Width="208px" MaxLength="2" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td style="width:5%" >
                                                                    </td>
                                                                <td style="width:35%" >
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="4" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td  style="width:10%" class="textbold">
                                                                   </td>
                                                                <td class="textbold"  style="width:5%">
                                                                    </td>
                                                                <td class="textbold" style="width:15%">
                                                                     <span class="textbold">Corporate Qualifier</span ></td>
                                                                <td  style="width:20%" >
                                                                   <asp:TextBox ID="txtCorporateQualifier" runat ="server" CssClass ="textbox"  Width="208px" MaxLength="1" TabIndex="2"></asp:TextBox>
                                                                    </td>
                                                                <td  style="width:5%">
                                                                    </td>
                                                                <td  style="width:45%">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" /></td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td  style="width:10%">
                                                                    &nbsp;</td>
                                                                <td  style="width:5%">
                                                                    </td>
                                                                <td class="textbold"  style="width:15%">
                                                                   <span class="textbold" > Description</span></td>
                                                                <td  style="width:20%" >
                                                                   <asp:TextBox ID="txtDescription" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30" TabIndex="3"></asp:TextBox>
                                                                    </td>
                                                                <td  style="width:5%">
                                                                    </td>
                                                                <td  style="width:45%">
                                                                    </td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                          
                                                            
                                                            <tr>
                                                                <td colspan="6" >
                                                                    <asp:GridView ID="gvCorporateCode" runat="server"  AutoGenerateColumns="False" TabIndex="7" Width="100%"    >
                                                                      <Columns>                                               
                                                                         <asp:TemplateField HeaderText="Corporate Code">
                                                                                 <ItemTemplate>
                                                                                 <asp:HiddenField ID="rowIDHidden" runat="server" Value='<%#Eval("RowID")%>' />
                                                                                <%#Eval("Code")%>
                                                                                </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Qualifier">
                                                                         <ItemTemplate>
                                                                         <%#Eval("Qualifier")%>
                                                                         </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                                                                                                  
                                                                         <asp:TemplateField HeaderText="Description">
                                                                         <ItemTemplate>
                                                                         <%#Eval("Description")%>
                                                                         </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action">                                                                        
                                                                            <ItemTemplate>
                                                                            <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") + "|" + DataBinder.Eval(Container.DataItem, "Qualifier") %>'>Select</asp:LinkButton>
                                                                            </ItemTemplate>                                                                       
                                                                            </asp:TemplateField>  
                                                                                                                                                     
                                                                                                                                                                                                                                                  
                                                                                        
                                                                                 
                                                                         </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" />
                                                                     </asp:GridView>                                                                    
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 12px">
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
             <tr>
              <td> <asp:Literal ID="litCorporateCode" runat="server"></asp:Literal></td>
            </tr>
        </table>
    <!-- Code by Abhishek -->
    
  
    </form>
</body>
</html>