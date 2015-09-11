<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_IRType.aspx.vb" Inherits="HelpDesk_HDSR_IRType" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
      <title>IR Type</title>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     <script type="text/javascript" language ="javascript" > 
    
 
     function DeleteFunction(CheckBoxObj)
      {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
        document.getElementById("hdDelete").value=CheckBoxObj
        }
        else
        {
         document.getElementById("hdDelete").value="";
         return false;
        }
    }
      function EditFunction(CheckBoxObj)
    {                
          window.location ="HDUP_IRType.aspx?Action=U&HD_IR_TYPE_ID=" + CheckBoxObj; 
          return false;
    } 
    function  IRTypeMandatory()
    {
    return true;
    }  
    </script>
    
</head>
<body>
    <form id="form1" runat="server" defaultbutton ="btnSearch" defaultfocus ="txtType">
      <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" ><span class="menu">Help Desk-></span><span class="sub_menu">IR Type</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >Search IR Type</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  align ="left" class="redborder">                                 
                                                        <table width="100%" border="0"  cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height: 15px"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:20%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:10%;" class="textbold" ></td>
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">IR Type</span></td>
                                                                <td style="width:20%;" class="textbold"><asp:TextBox ID="txtType" runat ="server" CssClass ="textbox" Width="161px" MaxLength="30" TabIndex="1" ></asp:TextBox></td>
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:20%;" ><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height: 10px" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:20%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:10%;" class="textbold" ></td>
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">Category</span></td>
                                                                <td style="width:20%;" class="textbold"><asp:DropDownList ID="drpCatName" runat ="server" CssClass ="dropdownlist" Width="167px" TabIndex="2"  >
                                                                </asp:DropDownList></td>
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:20%;" ><asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="n" /></td>
                                                            </tr>
                                                                <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height: 10px" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:20%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:10%;" class="textbold" ></td>
                                                                <td style="width:36%;" colspan="2" class="ErrorMsg" ></td>
                                                               
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:20%;" ><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="r" /></td>
                                                            </tr>
                                                           
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td class="textbold" style="width: 20%">
                                                                </td>
                                                                <td class="textbold" style="width: 10%">
                                                                </td>
                                                                <td class="ErrorMsg" colspan="2" style="width: 36%">
                                                                </td>
                                                                <td style="width: 10%">
                                                                </td>
                                                                <td style="width: 20%"><asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="5" AccessKey="e" /></td>
                                                            </tr>
                                                            
                                                               <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height: 10px">                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                              <td colspan="6"><asp:GridView ID="gvIRType" runat="server" AllowSorting="true" HeaderStyle-ForeColor="white"  AutoGenerateColumns="False" TabIndex="6" width="78%" EnableViewState="False" >
                                                                                 <Columns>
                                                                                   <asp:BoundField DataField="HD_IR_TYCAT_NAME" SortExpression="HD_IR_TYCAT_NAME"  HeaderText="Category" >
                                                                                       <ItemStyle Width="30%" />
                                                                                   </asp:BoundField>
                                                                                 <asp:TemplateField HeaderText="IR Type" SortExpression="HD_IR_TYPE_NAME" >
                                                                                                <itemtemplate>
                                                                                                    <%#Eval("HD_IR_TYPE_NAME")%>
                                                                                                    <asp:HiddenField ID="HDHDIRTYPEID" runat="server" Value='<%#Eval("HD_IR_TYPE_ID")%>' />
                                                                                                </itemtemplate>
                                                                                     <ItemStyle Width="50%" />
                                                                                        </asp:TemplateField>                                                                                        
                                                                                      
                                                                                        <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                       <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp;
                                                                                                       <asp:LinkButton ID="linkDelete" runat="server" CssClass="LinkButtons" Text="Delete"></asp:LinkButton>
                                                                                                       
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                                                <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                       </asp:TemplateField>
                                                                                 </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" />
                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                        
                                                                  </asp:GridView></td>
                                                            </tr>
                                                           
                                            <tr>    
                                                                                       
                                                    <td valign ="top" colspan="6"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 200px" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                
            
            
                                                        </table>  
                                        </td>
                                    </tr>
                                       <tr>
                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">  
                                            <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server" ></asp:TextBox>
                                            <asp:HiddenField ID="hdDelete" runat="server" />                                                              
                                            </td>
                                        </tr>
                                </table>
                            </td>
                        </tr>
                     
                    </table>
                </td>
            </tr>
        </table>
    <!-- Code by Dev Abhishek -->
    </form>
</body>
</html>
