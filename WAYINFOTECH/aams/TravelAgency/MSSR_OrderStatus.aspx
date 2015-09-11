<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_OrderStatus.aspx.vb" Inherits="Order_MSSR_OrderStatus" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Order Status Search</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >
    function OrderStatusReset()
    {        
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtOrderStatus").value=""; 
      
        if (document.getElementById("gvOrderStatus")!=null) 
        document.getElementById("gvOrderStatus").style.display ="none"; 
        document.getElementById("txtOrderStatus").focus(); 
                
        return false;
    }
    
     function EditFunction(OrderStatusID)
    {   
                  
        window.location.href="MSUP_OrderStatus.aspx?Action=U&OrderStID="+OrderStatusID;       
        return false;
           }
    function DeleteFunction(OrderStatusID)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {          
            window.location.href="MSSR_OrderStatus.aspx?Action=D|"+OrderStatusID;                   
            return false;
        }
    }
    function NewFunction()
    {   
    
        window.location.href="MSUP_OrderStatus.aspx?Action=I";       
        return false;
    }
    
     function OrderStatusMandatory()
    {
       if (  document.getElementById("txtOrderStatus").value!="")
         {
           if(IsDataValid(document.getElementById("txtOrderStatus").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Order Status is not valid.";
            document.getElementById("txtOrderStatus").focus();
            return false;
            } 
         } 
       
          return true;
     }
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff" >
    <form id="frmOrderStatus" runat="server" defaultbutton="btnSearch"  defaultfocus ="txtOrderStatus" >
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Order-&gt;</span><span class="sub_menu">Order Status</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Order Status</td>
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
                                                                <td class="textbold" style="width: 100px; height: 22px;">
                                                                    Order Status </td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtOrderStatus" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30" TabIndex="1"></asp:TextBox>
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
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="N" /></td>
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
                                                                <td style="width: 90px; height: 22px;">
                                                                    &nbsp;</td>
                                                                <td style="width: 192px; height: 22px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="4" Text="Export" AccessKey="E" /></td>
                                                            </tr>                                                           
                                                            <tr>
                                                               <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                                <td style="width: 192px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                                <td style="width: 192px">
                                                                    <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    <asp:GridView ID="gvOrderStatus" runat="server"  EnableViewState ="false" AutoGenerateColumns="False" TabIndex="5" Width="100%" AllowSorting="True">
                                                                         <Columns>                                               
                                                                         <asp:TemplateField HeaderText="Order Status" SortExpression="ORDER_STATUS_NAME">
                                                                                 <ItemTemplate>
                                                                                 <%#Eval("ORDER_STATUS_NAME")%>
                                                                                 <asp:HiddenField ID="HiddenorderStatus" runat="server"  Value='<%#Eval("ORDERSTATUSID")%>' />
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                              
                                                                                                                                                     
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp;
                                                                                            <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                                                                                                                                                                                                          
                                                                                        
                                                                                 
                                                                         </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                         </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
                                                                 <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey"  ReadOnly ="true" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
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
