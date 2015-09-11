<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_Product.aspx.vb" Inherits="Order_MSSR_Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Product</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
       
    function EditFunction(CheckBoxObj)
    {           
          window.location.href="MSUP_Product.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
        //  window.location.href="MSSR_Product.aspx?Action=D|"+ CheckBoxObj +"|"+ document.getElementById("<%=ddlGroupName.ClientID%>").selectedIndex  +"|"+ document.getElementById("<%=txtProductName.ClientID%>").value +"|"+ document.getElementById("<%=ddlCrs.ClientID%>").selectedIndex +"|"+ document.getElementById("<%=ddlOS.ClientID%>").selectedIndex;                   
        //  return false;
         // hdProductID
           document.getElementById('hdProductID').value = CheckBoxObj;
           document.forms['form1'].submit();   
        }
        else
        {
            return false;
        }
    }
   
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff" >
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="ddlGroupName"  >
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Products</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search <span style="font-family: Microsoft Sans Serif">Product</span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 10%">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Product Group Name</td>
                                                                            <td>
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlGroupName" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Product Name</td>
                                                                            <td>
                                                                   <asp:TextBox ID="txtProductName" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30" TabIndex="2"></asp:TextBox></td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="N" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                             Provider CRS</td>
                                                                            <td>
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCrs" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                                            <td>
                                                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="E" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 23px">
                                                                            </td>
                                                                            <td class="textbold" style="height: 23px">
                                                                    O.S</td>
                                                                            <td style="height: 23px">
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlOS" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                                            <td style="height: 23px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="R" /></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                <input id="hdProductID" runat="server" style="width: 6px" type="hidden" />
                                                </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:GridView EnableViewState="False" ID="gvProduct" runat="server"  AutoGenerateColumns="False" TabIndex="4" Width="100%" AllowSorting="True"    >
                                                 <Columns>
                                               
                                                 <asp:BoundField DataField="productGroupName" HeaderText="Group Name" SortExpression="productGroupName"   />
                                                  <asp:BoundField DataField="PRODUCTNAME" HeaderText="Product Name" SortExpression="PRODUCTNAME"   />
                                                 <asp:BoundField DataField="VERSION" HeaderText="Version" SortExpression="VERSION"   />
                                                           
                                                            <asp:TemplateField HeaderText="Action">
                                                                                         <ItemTemplate>
                                                                                         <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a> 
                                                                                          <asp:HiddenField ID="hdProductName" runat="server" Value='<%#Eval("PRODUCTID")%>' />   
                                                                                        </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                                        
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                    
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
    <!-- Code by Rakesh -->
    
  
    </form>
</body>
</html>
