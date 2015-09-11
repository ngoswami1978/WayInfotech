<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_Department.aspx.vb"
    Inherits="Setup_MSSR_Department" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Department</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
//     function DepartmentReset()
//    {
//        document.getElementById("txtDepartmentName").value="";
//        document.getElementById("txtManagerName").value="";
//        document.getElementById("lblError").innerHTML= "";
//        document.getElementById("grdDepartment").style.display = "none";
//        return false;
//    }
    
    // function EditFunction(Obj)
   // {
   //     window.location ="MSUP_Department.aspx?Action=U&DepartmentId=" + Obj.parentElement.parentElement.cells(0).innerText;        
   //     return false;
   // }
     
     
     function EditFunction(CheckBoxObj)
    {           
        window.location.href="MSUP_Department.aspx?Action=U|"+CheckBoxObj;       
        return false;
    }
     
    
    
   
   
    function DeleteFunction()
    {   
   
        if (confirm("Are you sure you want to delete?")==false)
        {   
          //  window.location.href="MSSR_Department.aspx?Action=D|"+ CheckBoxObj +"|"+ document.getElementById("<%=txtDepartmentName.ClientID%>").value +"|"+ document.getElementById("<%=txtManagerName.ClientID%>").value;                   
           return false;
//               document.getElementById('hdDepID').value = CheckBoxObj;
//               document.forms['frmDepartment'].submit(); 
        }
    }
    
    
    
    function NewFunction()
    {   
        window.location.href="MSUP_Department.aspx?Action=I";       
        return false;
    }
    

    </script>

</head>
<body >
    <form id="frmDepartment" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Department</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Department
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 346px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" class="textbold">
                                                        &nbsp;</td>
                                                    <td width="20%" class="textbold">
                                                        Department Name</td>
                                                    <td width="30%">
                                                        <asp:TextBox ID="txtDepartmentName" CssClass="textbox" runat="server" MaxLength="50"
                                                            TabIndex="1"></asp:TextBox></td>
                                                    <td width="20%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                </tr>
                                            <%--    <tr>
                                                    <td class="textbold" style="height: 3px">
                                                    </td>
                                                    <td class="textbold" style="height: 3px">
                                                    </td>
                                                    <td style="height: 3px">
                                                    </td>
                                                    <td style="height: 3px">
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 8px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px">
                                                        Manager Name</td>
                                                    <td style="height: 22px">
                                                        <asp:TextBox ID="txtManagerName" CssClass="textbox" runat="server" MaxLength="40"
                                                            TabIndex="2"></asp:TextBox></td>
                                                    <td style="height: 22px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4"  AccessKey="N"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 8px">
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td style="height: 8px">
                                                    </td>
                                                    <td class="textbold" style="height: 8px">
                                                    </td>
                                                    <td style="height: 8px">
                                                    </td>
                                                    <td style="height: 8px">
                                                    <asp:Button ID="btnExport" runat="server"
                                                            CssClass="button"  TabIndex="21" Text="Export"  AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 8px">
                                                    </td>
                                                    <td class="textbold" style="height: 8px">
                                                    </td>
                                                    <td style="height: 8px">
                                                    </td>
                                                    <td style="height: 8px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 8px">
                                                    </td>
                                                    <td class="textbold" style="height: 8px">
                                                     <asp:HiddenField ID="hdDepID" runat="server" />
                                                    </td>
                                                    <td style="height: 8px">
                                                    </td>
                                                    <td style="height: 8px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" AccessKey="R" Text="Reset" TabIndex="5"  /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
                                                        &nbsp;<asp:GridView ID="grdDepartment" BorderWidth="1" BorderColor="#d4d0c8" runat="server"
                                                            AutoGenerateColumns="False" Width="100%" EnableViewState="true" AllowSorting="True"
                                                            HeaderStyle-ForeColor="white">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Department Name" SortExpression="Department_Name">
                                                                    <ItemTemplate>
                                                                        <%#Eval("Department_Name")%>
                                                                        <asp:HiddenField ID="hdDepartmentId" runat="server" Value='<%#Eval("DepartmentId")%>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ManagerName" HeaderText="Manager Name" HeaderStyle-HorizontalAlign="left"
                                                                    ItemStyle-HorizontalAlign="left"  SortExpression="ManagerName"/>
                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center">
                                                                    <ItemStyle HorizontalAlign="center" />
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp;
                                                                        <asp:LinkButton ID="linkDelete" CssClass="LinkButtons" Text="Delete" runat="server" CommandName="DeleteX" CommandArgument='<%#Eval("DepartmentId")%>'></asp:LinkButton>
                                                                        <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="left" />
                                                            <RowStyle CssClass="textbold" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" valign="top">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                    <td style="width: 30%" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey" ReadOnly="true"></asp:TextBox></td>
                                                                    <td style="width: 25%" class="right">
                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                    <td style="width: 20%" class="center">
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                            ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 25%" class="left">
                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                                                            Visible="false"></asp:TextBox>
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
    </form>
</body>
</html>
