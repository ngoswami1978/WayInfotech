<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_EquipmentCategory.aspx.vb" Inherits="Incentive_INCSR_EquipmentCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>AAMS: Equipment Category</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">   
          
       function  NewMSUPSecurityRegion()
       {    
           window.location="INVUP_EquipmentCategory.aspx?Action=I";
           return false;
       }    
    
   
         function DeleteFunction(CheckBoxObj)
          {   
               if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteEqipCateg").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteEqipCateg").value="";
                 return false;
                }
        }
      function EditFunction(CheckBoxObj)
    {
            
              window.location ="INVUP_EquipmentCategory.aspx?Action=U&EquipMentCategID=" + CheckBoxObj;  
      
          return false;
    }   
    </script>
</head>
<body >
    <form id="frmManageState" runat="server" defaultfocus ="txtEquipCategName"  defaultbutton="btnSearch" >
    <div>
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                <table width="100%" align="left" >
                          <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Inventory-&gt; </span>Equipment Category</td>
                        </tr>      
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Search Equipment Category</td>
                        </tr>
                        <tr>
                            <td  >
                                <table border="0" cellpadding="0" cellspacing="0"  width="100%">                                   
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">                                              
                                                <tr>                                                    
                                                    <td class="textbold" style="height: 264px;width:100%" colspan="4" valign="top" >
                                                          <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <%--<tr>
                                                                <td width="20px"  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                    &nbsp;</td>
                                                            </tr>--%>
                                                              <tr>
                                                                  <td align="center" class="textbold" colspan="6" height="20px" valign="top"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                  </td>
                                                              </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 148px;">
                                                                    </td>
                                                                <td style="height: 22px; width: 156px;" align="left" nowrap="true" >
                                                                    Country</td>
                                                                <td style="height: 22px; width: 176px;" >
                                                                    <span class="textbold">
                                                                        <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                            TabIndex="1" Width="184px">
                                                                        </asp:DropDownList></span></td>
                                                                <td style="height: 22px; width: 32px;">
                                                                    </td>
                                                                <td width="18%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="button"  AccessKey="A" Text="Search" TabIndex="2" /></td>
                                                            </tr>                                                          
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 148px;">
                                                                    </td>
                                                                <td style="height: 22px; width: 156px;" align=left>
                                                                    Equipment Category Name</td>
                                                                <td class="textbold" style="height: 22px; width: 176px;">
                                                                    <asp:TextBox ID="txtEquipCategName" runat="server" CssClass="textfield" TabIndex="1" Width="177px" MaxLength="100" EnableViewState="False"></asp:TextBox></td>
                                                                <td style="height: 22px; width: 32px;">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New"  AccessKey="N" TabIndex="3" /></td>
                                                            </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 22px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 148px; height: 22px">
                                                                  </td>
                                                                  <td align="left" style="height: 22px; width: 156px;">
                                                                  </td>
                                                                  <td class="textbold" style="width: 176px; height: 22px">
                                                                  </td>
                                                                  <td style="height: 22px; width: 32px;">
                                                                  </td>
                                                                  <td style="height: 22px">
                                                                      <asp:Button ID="btnExport" runat="server" CssClass="button" AccessKey="E" Text="Export" TabIndex="4" /></td>
                                                              </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 22px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 148px; height: 22px">
                                                                  </td>
                                                                  <td align="left" style="height: 22px; width: 156px;">
                                                                  </td>
                                                                  <td class="textbold" style="width: 176px; height: 22px">
                                                                  </td>
                                                                  <td style="height: 22px; width: 32px;">
                                                                  </td>
                                                                  <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="5"  AccessKey="R"/></td>
                                                              </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 148px;">
                                                                </td>
                                                                <td style="height: 22px; width: 156px;" align=left>
                                                                    </td>
                                                                <td style="height: 22px; width: 176px;">
                                                                    </td>
                                                                <td style="height: 22px; width: 32px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 22px">
                                                                    </td>
                                                            </tr>
                                                            <tr height="20px">
                                                            <td></td>
                                                            <td style="width: 148px"></td>
                                                                <td colspan="3" height="4">
                                                                     <asp:GridView ID="grdEquipCat" runat="server" AutoGenerateColumns="False" Width="90%" TabIndex="6" EnableViewState="False" AllowSorting ="true" HeaderStyle-ForeColor="white" >
                                                            <Columns>
                                                        
                                                             <asp:TemplateField HeaderText="Eqipment Category" SortExpression ="BC_EQP_CATG_TYPE" ItemStyle-HorizontalAlign="left" >
                                                                            <itemtemplate>
                                                                                <%#Eval("BC_EQP_CATG_TYPE")%>
                                                                                <asp:HiddenField ID="hdCategId" runat="server" Value='<%#Eval("BC_EQP_CATG_ID")%>' />
                                                                            </itemtemplate>
                                                                </asp:TemplateField>      
                                                                 <asp:BoundField  HeaderText ="Country"  DataField ="COUNTRYNAME" SortExpression ="COUNTRYNAME" ItemStyle-HorizontalAlign ="left" HeaderStyle-HorizontalAlign ="left" ItemStyle-Width ="100px" />                                                                        
                                                                  <asp:BoundField  HeaderText ="Unit Cost"  DataField ="BC_EQP_CATG_COST" SortExpression ="BC_EQP_CATG_COST" ItemStyle-HorizontalAlign ="right" HeaderStyle-HorizontalAlign ="left" ItemStyle-Width ="100px" />                                                           
                                                            <asp:TemplateField HeaderText="Action" >                                                            
                                                                <ItemStyle HorizontalAlign="Left"/>   
                                                                <ItemTemplate>
                                                                       <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton> <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                </ItemTemplate>                                                                                                                                
                                                            </asp:TemplateField>                                                              
                                                            </Columns> 
                                                            <AlternatingRowStyle  CssClass="lightblue"/>
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="left" ForeColor="White"/>
                                                            <RowStyle CssClass="textbold"/>
                                                            </asp:GridView></td>
                                                            <td></td>
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
                                                        <asp:HiddenField ID="hdDeleteEqipCateg" runat="server" />
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                            Width="73px"></asp:TextBox>
                                                    </td> 
                                                </tr> 
                                                        </table>
                                                        <br />
                                                           
                                                     </td>
                                                    <td width="18%" rowspan="1" valign="top" style="height: 264px" >
                                                        </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td class="textbold" colspan="5">                                                        
                                                          
                                                        </td>
                                                </tr>
                                                <tr style="font-size: 12pt; font-family: Times New Roman">
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
            <tr>
                <td  valign="top" style="height: 23px">
                
                  
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
