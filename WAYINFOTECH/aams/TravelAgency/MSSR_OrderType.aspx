<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_OrderType.aspx.vb" Inherits="Setup_MSSR_OrderType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Order Type</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript"> 
      function EditFunction(CheckBoxObj)
    {           
          window.location.href="MSUP_OrderType.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
          window.location.href="MSSR_OrderType.aspx?Action=D|"+ CheckBoxObj +"|"+ document.getElementById("<%=txtOrderTypeCat.ClientID%>").value  +"|"+ document.getElementById("<%=txtOrderType.ClientID%>").value ;                   
          return false;
        }
    } 
    </script>
    </head>
<body >
    <form id="form1" runat="server" defaultfocus="txtOrderTypeCat" defaultbutton="btnSearch">
       <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Order Type</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Order Type 
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 309px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                           <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg "></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 10%">
                                                                            </td>
                                                                            <td class="textbold">
                                                        Order Type Category</td>
                                                                            <td>
                                                        <asp:TextBox ID="txtOrderTypeCat" CssClass="textbox" runat="server" MaxLength="50" Width="224px" Wrap="False" TabIndex="2"></asp:TextBox></td>
                                                                            <td>
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Order Type</td>
                                                                            <td>
                                                    <asp:TextBox ID="txtOrderType" CssClass="textbox" runat="server" MaxLength="50" Width="224px" Wrap="False" TabIndex="2"></asp:TextBox></td>
                                                                            <td>
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="N" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                             </td>
                                                                            <td>
                                                                    </td>
                                                                            <td>
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="5" Text="Export" AccessKey="E" /></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                            <td>
                                            </td>
                                            <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                               <asp:GridView ID="grdOrderType" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" EnableViewState="False" TabIndex="7" AllowSorting="True">
                                                            <Columns>
                                                          
                                                               <asp:BoundField DataField="OrderTypeCategoryName" HeaderText="Order Type Category" SortExpression="OrderTypeCategoryName"   />
                                                  <asp:BoundField DataField="ORDER_TYPE_NAME" HeaderText="Order Type" SortExpression="ORDER_TYPE_NAME"   />
                                                 <asp:BoundField DataField="TimeRequired" HeaderText="Time Required" SortExpression="TimeRequired"   />
                                                              <asp:TemplateField HeaderText="Action">
                                                                                         <ItemTemplate>
                                                                                         <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a> 
                                                                                          <asp:HiddenField ID="hdORDERTYPE" runat="server" Value='<%#Eval("ORDERTYPEID")%>' />   
                                                                                        </ItemTemplate>
                                                                                </asp:TemplateField>   
                                                              
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue"  />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                            <RowStyle CssClass="textbold" />
                                                        </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                               <tr>
                                                   <td colspan="4">
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
