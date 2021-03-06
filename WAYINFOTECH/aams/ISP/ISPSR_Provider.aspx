<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPSR_Provider.aspx.vb" Inherits="ISP_ISP_Provider" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ISP:Provider Search</title>
     <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
     <script type="text/javascript" language="javascript">
      function EditFunction(CheckBoxObj)
    {           
          window.location.href="ISPUP_Provider.aspx?Action=U&ProviderId="+CheckBoxObj;               
          return false;
    }
        function NewMSUPProvider()
        {
        window.location.href="ISPUP_Provider.aspx?Action=I|";
        return false;
        }
        
    function DeleteFunction(CheckBoxObj)
    {   
         if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteProviderID").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteProviderID").value="";
                 return false;
                }
    } 
     </script>
</head>
<body>
  <form   defaultbutton="btnSearch" defaultfocus="txtISPProvider" id="form1" runat="server">
    <table width="860px" align="left"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">IP Provider Search </span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search <span style="font-family: Microsoft Sans Serif">IP Provider</span></td>
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
                                                                                        <td style="width: 100px">
                                                                                        </td>
                                                                                        <td class="textbold" style="width: 100px">
                                                                                            IP Provider</td>
                                                                                        <td style="width: 408px">
                                                                                              <asp:TextBox ID="txtISPProvider" runat="server" CssClass="textbold" Width="208px" MaxLength="100" TabIndex="1"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                          <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="A" /></td>
                                                                                     </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100px">
                                                                                        </td>
                                                                                        <td class="textbold">
                                                                                            Reserved</td>
                                                                                        <td style="width: 308px">
                                                                                        <asp:CheckBox ID="chkReserved" runat="server" TabIndex="1" />
                                                                                        </td>
                                                                                        <td>
                                                                                       <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="N" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100px; height: 26px;">
                                                                                        </td>
                                                                                        <td class="textbold" style="height: 26px">
                                                                                        </td>
                                                                                        <td style="width: 308px; height: 26px;">
                                                                                         </td>
                                                                                        <td style="height: 26px">
                                                                                         <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="2" Text="Export" AccessKey="E" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100px; height: 26px">
                                                                                        </td>
                                                                                        <td class="textbold" style="height: 26px">
                                                                                        </td>
                                                                                        <td style="width: 308px; height: 26px">
                                                                                            <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                                        <td style="height: 26px">
                                                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="R" /></td>
                                                                                    </tr>
                                                                                                                    
                                                                                    <tr>
                                                                                        
                                                                                    </tr>
                                                                                     <tr>
                                                                                     <td></td>
                                                                                        <td colspan="3">
                                                                                           <asp:GridView EnableViewState="false" ID="grdvISPProvider" runat="server"  AutoGenerateColumns="False" TabIndex="3" Width="80%" AllowSorting="True"    >
                                                                                             <Columns>
                                                                                           
                                                                                                        <asp:BoundField DataField="ProviderName" HeaderText="Provider Name"  SortExpression="ProviderName"  >
                                                                                                            <ItemStyle Wrap="True" Width="220px" />
                                                                                                        </asp:BoundField>
                                                                                                    
                                                                                                        <asp:TemplateField HeaderText="Action" >
                                                                                                        <ItemTemplate>
                                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                                                           <asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton> <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                                                            <asp:HiddenField ID="hdProviderID" runat="server" Value='<%#Eval("ProviderID")%>' />   
                                                                                                         </ItemTemplate>
                                                                                                            <ItemStyle Wrap="False" />
                                                                                                            <HeaderStyle Width="130px" />
                                                                                                       </asp:TemplateField>
                                                                                                                                                    
                                                                                             
                                                                                             </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                <RowStyle CssClass="textbold" />
                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                
                                                                                             </asp:GridView>
                                                                                                                        </td>
                                                                                    
                                                                                     </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                                                  <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                  <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                                                                      <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" TabIndex="4" ></asp:TextBox></td>
                                                                                                                      <td style="width: 25%" class="right">                                                                             
                                                                                                                          <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="4"><< Prev</asp:LinkButton></td>
                                                                                                                      <td style="width: 20%" class="center">
                                                                                                                          <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="4" >
                                                                                                                          </asp:DropDownList></td>
                                                                                                                      <td style="width: 25%" class="left">
                                                                                                                          <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next" TabIndex="4">Next >></asp:LinkButton></td>
                                                                                                                  </tr>
                                                                                                              </table></asp:Panel>
                                                                                            <asp:HiddenField ID="hdDeleteProviderID" runat="server" />
                                                                                            <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                                                                Width="73px"></asp:TextBox></td>
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
