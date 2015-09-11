<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_CitySearch.aspx.vb" Inherits="AMS_City_CRS_AAMS_CitySearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>City Search</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >      
    
     function  NewMSUPCity()
       {    
           window.location="MSUP_City.aspx?Action=I";
           return false;
       }  
    function CityReset()
    {
        
        document.getElementById("lblError").innerHTML="";    
        document.getElementById("txtCtyName").value=""; 
        if (document.getElementById("gvCity")!=null) 
        document.getElementById("gvCity").style.display ="none"; 
        document.getElementById("txtCtyName").focus(); 
                
        return false;
    }
     function CityMandatory()
    {
        if (  document.getElementById("txtCtyName").value!="")
         {
           if(IsDataValid(document.getElementById("txtCtyName").value,2)==false)
            {
            document.getElementById("lblError").innerHTML="City is not valid.";
            document.getElementById("txtCtyName").focus();
            return false;
            } 
         }     
       
        
         return true;
     }
     
         function DeleteFunction(CheckBoxObj)
          {   
//            if (confirm("Are you sure you want to delete?")==true)
//            {       
//                 
//                window.location.href="MSSR_CitySearch.aspx?Action=D|"+CheckBoxObj + "|"+ document.getElementById("<%=txtCtyName.ClientID%>").value;
//                return false;
//            }
                 if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteCityId").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteCityId").value="";
                 return false;
                }

        }
          function EditFunction(CheckBoxObj)
        {                
              window.location ="MSUP_City.aspx?Action=U&CityID=" + CheckBoxObj; 
              return false;
        }   
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff"  >
    <form id="frmCity" runat="server"  defaultbutton ="btnSearch" defaultfocus="txtCtyName">
    
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
                                Search City
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
                                                                <td class="textbold" style="width: 90px; height: 22px;">
                                                                    City Name</td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtCtyName" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30" EnableViewState="False" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td width="21%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="A" /></td>
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
                                                                <td class="textbold" style="width: 90px; height: 22px;">
                                                              
                                                                    </td>
                                                                <td class="textbold" style="width: 192px; height: 22px;">
                                                                
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3"  AccessKey="N"/></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td style="width: 90px">
                                                                </td>
                                                                <td style="width: 192px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="4" AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td style="width: 90px">
                                                                </td>
                                                                <td style="width: 192px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td style="width: 90px">
                                                                    &nbsp;</td>
                                                                <td style="width: 192px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5"  AccessKey="R"/></td>
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
                                                                    <asp:GridView ID="gvCity" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" EnableViewState="False"  AllowSorting ="true" HeaderStyle-ForeColor ="white"   >
                                                 <Columns>
                                                <asp:TemplateField HeaderText="City Name" SortExpression ="City_Name">
                                                                            <itemtemplate>
                                                                                <%#Eval("City_Name")%>
                                                                                <asp:HiddenField ID="hdCityId" runat="server" Value='<%#Eval("CityID")%>' />
                                                                            </itemtemplate>
                                                                </asp:TemplateField>     
                                                 <asp:BoundField DataField="Aoffice" HeaderText="Aoffice"  SortExpression ="Aoffice" />
                                                 <asp:BoundField DataField="State"  HeaderText="State" SortExpression ="State" />
                                                 <asp:BoundField DataField="Country" HeaderText="Country" SortExpression ="Country" />
                                                            <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                           <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton> <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                    <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                </asp:TemplateField>
                                                 
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                    
                                                 </asp:GridView>
                                                                    
                                                </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" height="12">
                                                         </td>
                                                    </tr>
                                                    <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td colspan="6" >
                                                        <asp:HiddenField ID="hdDeleteCityId" runat="server" />
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                            Width="73px"></asp:TextBox>
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
    <!-- Code by Dev -->
    
  
    </form>
</body>
</html>
