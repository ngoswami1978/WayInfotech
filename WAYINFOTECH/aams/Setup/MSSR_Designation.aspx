
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_Designation.aspx.vb"
    Inherits="Setup_MSSR_Designation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS:Manage Designation</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
 
    
   function EditValues(DesignationID)
    {           
       window.location.href="MSUP_Designation.aspx?Action=U&DesignationId=" +DesignationID     
        return false;
    }
    function DeleteFunction(DesignationID)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
            window.location.href="MSSR_Designation.aspx?Action=D&DesignationId=" +DesignationID    
            return false;
        }
    }
    function NewFunction()
    {   
        window.location.href="MSUP_Designation.aspx?Action=I";       
        return false;
    }
  
          
    function DesignationReset()
    {
        document.getElementById("lblError").innerHTML="";
        document.getElementById("txtDesignation").value="";
        //document.getElementById("dbgrdDesignation").style.display = "none";
        document.getElementById("txtDesignation").focus();
        return false;
    }
    
         
    </script>

</head>
<body >
    <form id="frmDesignation" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Designation</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Designation
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
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
                                                    <td width="20%" class="textbold" style="height: 21px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 21px; width: 128px;">
                                                        Designation Name</td>
                                                    <td width="30%" style="height: 21px">
                                                        <asp:TextBox ID="txtDesignation" CssClass="textbox" runat="server" MaxLength="100"></asp:TextBox></td>
                                                    <td width="20%" style="height: 21px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold" style="width: 128px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New"  AccessKey="N"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td style="width: 128px">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" /></td>
                                                </tr>
                                                 <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td style="width: 128px; height: 22px;">
                                                    </td>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td style="height: 22px">
                                                        <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export"  AccessKey="E"/></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:GridView ID="dbgrdDesignation" AllowSorting="true" HeaderStyle-ForeColor="white" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="50%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" EnableViewState="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Designation Name" SortExpression="Designation">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("Designation")%>
                                                                        <asp:HiddenField ID="hdDesigID" runat="server" Value='<%#Eval("DesignationID")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center">
                                                                                        <ItemStyle HorizontalAlign="center" />
                                                                                        <ItemTemplate>
                                                                                           <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                                          <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" />
                                                            <RowStyle CssClass="textbold" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                
                                            <tr>   
                                                    <td valign ="top" colspan="4"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px; height: 29px;" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="true" ></asp:TextBox></td>
                                                                          <td style="width: 200px; height: 29px;" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px; height: 29px;" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px; height: 29px;" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
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
