<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPSR_FeasibilityStatus.aspx.vb"
    Inherits="ISP_ISPSR_FeasibilityStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript"></script>

</head>

<script language="javascript" type="text/javascript">
    function FeasibilityReset()
    {
        document.getElementById("txtFeasibilityStatus").value=""; 
        if (document.getElementById("grdFeasibilityStatus")!=null) 
        document.getElementById("grdFeasibilityStatus").style.display ="none"; 
        return false;
    }
    function NewFunction()
    {  
        window.location.href="ISPUP_FeasibilityStatus.aspx?Action=I";      
        return false;
    }  
      function EditFunction(FeasibleStatusID)
    {    
        window.location.href="ISPUP_FeasibilityStatus.aspx?Action=U&FeasibleStatusID="+FeasibleStatusID;      
        return false;
     }
    function DeleteFunction(FeasibleStatusID)
    {  
        if (confirm("Are you sure you want to delete?")==true)
        {          
            window.location.href="ISPSR_FeasibilityStatus.aspx?Action=D&FeasibleStatusID="+FeasibleStatusID;                  return false;
        }
    }
</script>

<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtFeasibilityStatus">
        <table width="860px" align="left" style="height: 486px;" class="border_rightred">
            <tr>
                <td valign="top" style="height: 191px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ISP-></span><span class="sub_menu">Feasibility Status</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Feasibility Status
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" class="textbold" style="height: 25px;">
                                                        &nbsp;</td>
                                                    <td width="10%" nowrap="nowrap"  class="textbold">
                                                        Feasibility Status</td>
                                                    <td width="30%">
                                                        <asp:TextBox ID="txtFeasibilityStatus" MaxLength="15" CssClass="textbox" runat="server"
                                                            TabIndex="1" Width="264px"></asp:TextBox></td>
                                                    <td width="20%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px;">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" OnClientClick="javascript:NewFunction();"
                                                            TabIndex="3" AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px;">
                                                        &nbsp;</td>
                                                    <td style="height: 25px">
                                                        &nbsp;</td>
                                                    <td style="height: 25px">
                                                        &nbsp;</td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="4" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 19px;">
                                                    </td>
                                                    <td style="height: 19px">
                                                    </td>
                                                    <td style="height: 19px">
                                                    </td>
                                                    <td style="height: 19px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <asp:GridView ID="grdFeasibilityStatus" runat="server" AutoGenerateColumns="False"
                                                            Width="100%" RowStyle-HorizontalAlign="Left" HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            TabIndex="7" EnableViewState="False" AllowSorting="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="FeasibilityStatus" SortExpression="Name">
                                                                    <ItemTemplate>
                                                                     <asp:HiddenField ID="rowHidden" runat="server" Value='<%#Eval("FeasibleStatusID")%>' />
                                                                        <%#Eval("Name")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="15%" Wrap="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#"
                                                                            class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="12%" Wrap="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="white" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
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
