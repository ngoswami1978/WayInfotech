<%@ Page Language="VB" AutoEventWireup="false" EnableViewState="true"  CodeFile="MSSR_Aoffice.aspx.vb" Inherits="Setup_MSSR_1Aoffice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Aoffice</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function AofficeReset()
    {
        document.getElementById("txtAoffice").value="";       
        //return false;
    }
    function EditFunction(CheckBoxObj)
    {           
        window.location.href="MSUP_Aoffice.aspx?Action=U|"+CheckBoxObj;       
        return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {          
//            window.location.href="MSSR_Aoffice.aspx?Action=D|"+CheckBoxObj+"|"+document.getElementById("txtAoffice").value;       
//            return false;

        document.getElementById('hdAOfficeID').value = CheckBoxObj;
               document.forms['frmAoffice'].submit();     
        }
        else
        {
            return false;
        }
    }
    function NewFunction()
    {   
        window.location.href="MSUP_Aoffice.aspx?Action=I|";       
        return false;
    }
    </script>

</head>
<body >
    <form id="frmAoffice" runat="server" defaultbutton ="btnSearch" defaultfocus="txtAoffice">
        <table width="860px" align="left" style="height: 486px;" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Aoffice</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Aoffice
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
                                                    <td width="20%" class="textbold" style="height: 25px;">
                                                        &nbsp;</td>
                                                    <td width="10%" class="textbold">
                                                        Aoffice</td>
                                                    <td width="30%">
                                                        <asp:TextBox ID="txtAoffice" MaxLength="3" CssClass="textbox" runat="server"></asp:TextBox></td>
                                                    <td width="20%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" AccessKey="A" /></td>
                                                </tr>
                                                 <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                 </tr>
                                                                
                                                <tr>
                                                    <td style="height: 25px;">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" OnClientClick="javascript:NewFunction();" AccessKey="N" /></td>
                                                </tr>
                                                 <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                
                                                <tr>
                                                    <td style="height: 25px;">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                    <td>
                                                    <asp:HiddenField ID="hdAOfficeID" runat="server" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" height="4" align="center">
                               
                                                      <asp:GridView  ID="grdAoffice" BorderWidth="1" HeaderStyle-ForeColor="white" AllowSorting="true"  BorderColor="#d4d0c8" runat="server"
                                                            AutoGenerateColumns="False"  ItemStyle-CssClass="ItemColor" AlternatingItemStyle-CssClass="lightblue"
                                                            HeaderStyle-CssClass="Gridheading" Width="68%" EnableViewState="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Aoffice" SortExpression="Aoffice">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAoffice" runat="server" Text='<%#Eval("Aoffice")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="City" SortExpression="City_Name">
                                                                                                              
                                                                     <ItemTemplate>
                                                                        <%# Eval("City_Name")%>
                                                                   </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Country" SortExpression="Country_Name">
                                                                   
                                                                    <ItemTemplate>
                                                                        <%#Eval("Country_Name")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="btnEdit" runat="server">Edit</a> <a href="#" class="LinkButtons" id="btnDelete" runat="server">
                                                                                                Delete</a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                            <HeaderStyle CssClass="Gridheading" />
                                                            <RowStyle CssClass="ItemColor" />
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                        </asp:GridView>
                                                      
                                                        </td>
                                                </tr>
                                                
                                               
                                            <tr>   
                                                    <td valign ="top" colspan="4"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px; height: 29px;" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey"  ReadOnly="true"></asp:TextBox></td>
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
