<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_OrderTypeCategory.aspx.vb" Inherits="Setup_MSSR_OrderTypeCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Order Type Category</title>
      <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript"> 
        
        function OrderTypeCategoryReset()
    {        
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtOrderTypeCat").value=""; 
        document.getElementById("txtOrderTypeCat").focus();                        
        return false;
    }
      
     function EditFunction(CatTypeID)
    {   
                  
        window.location.href="MSUP_OrderTypeCategory.aspx?Action=U&CategoryID="+CatTypeID;       
        return false;
           }
    function DeleteFunction(CatTypeID)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {          
            window.location.href="MSSR_OrderTypeCategory.aspx?Action=D&CategoryID="+CatTypeID;                   
            return false;
        }
    }
    function NewFunction()
    {   
    
        window.location.href="MSUP_OrderTypeCategory.aspx?Action=I";       
        return false;
    }
    
     
    </script>

</head>
<body>
    <form id="OrderTypeCat" runat="server" defaultfocus="txtOrderTypeCat" defaultbutton="btnSearch">
   <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-></span><span class="sub_menu">Order Type Category</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Order Type Category
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 21px; width: 131px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 21px; width: 123px;">
                                                        Order Type Category</td>
                                                    <td  style="height: 21px; width: 380px;">
                                                        <asp:TextBox ID="txtOrderTypeCat" CssClass="textbox" runat="server" MaxLength="50" Width="272px" Wrap="False" TabIndex="1"></asp:TextBox></td>
                                                    <td  style="height: 21px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 131px">
                                                    </td>
                                                    <td class="textbold" style="width: 123px">
                                                    </td>
                                                    <td style="width: 380px">
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 131px">
                                                        &nbsp;</td>
                                                    <td style="width: 123px">
                                                        &nbsp;</td>
                                                    <td style="width: 380px">
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="2" Text="Export" AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 131px">
                                                    </td>
                                                    <td style="width: 123px">
                                                    </td>
                                                    <td style="width: 380px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 131px">
                                                    </td>
                                                    <td style="width: 123px">
                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                    <td style="width: 380px">
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td height="20px" style="width: 131px">
                                                    </td>
                                                    <td style="width: 123px">
                                                    </td>
                                                    <td style="width: 380px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:GridView ID="grdOrderTypeCat" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" EnableViewState="False" AllowSorting="True">
                                                           <Columns>                                               
                                                                         <asp:TemplateField HeaderText="Order Type Category" SortExpression="OrderTypeCategoryName">
                                                                                 <ItemTemplate>
                                                                                 <asp:HiddenField ID="catIDHidden" runat="server" Value='<%#Eval("OrderTypeCategoryID")%>' />
                                                                                <%#Eval("OrderTypeCategoryName")%>
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
                                                    <td colspan="4">
                                                     <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
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
                                                    <td colspan="4" height="12">
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
