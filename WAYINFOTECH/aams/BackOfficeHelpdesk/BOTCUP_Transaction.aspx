<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOTCUP_Transaction.aspx.vb" Inherits="BOHelpDesk_TCUP_Transaction" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Back Office Technical::Assignee History</title>
    <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
function fnClose()
{
        window.close();
        return false;
}
</script>
</head>
<body>
     <form id="form1" runat="server"  >
        <table width="860px" style="height:486px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                  
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Back Office Technical-></span><span class="sub_menu">Assignee</span></td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">
                                            Assignee</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top" style="height: 456px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                   <tr>
                                        <td class="top" style="width:80%">
                                            &nbsp;</td>
                                        <td style="width:20%" class="center"><asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnClose()" >Close</asp:LinkButton> &nbsp; &nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="gap" style="width: 80%">
                                        </td>
                                        <td style="width: 20%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="redborder top" colspan="2">                                
                                    
                                     
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold center top"   rowspan="0">
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" class="top">                                                       
                                                        <asp:Panel ID="pnlTransaction" runat="server" Width="100%">
                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                
                                                                <tr>
                                                                    <td colspan="4" class="center" >
                                                                        <asp:GridView EnableViewState="False" ID="gvTransaction" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="99%" AllowSorting="True">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="DateTime" DataField="Datetime" SortExpression="Datetime" >
                                                                                        <ItemStyle Width="15%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="LeftDateTime" DataField="LeftDateTime" SortExpression="LeftDateTime" >
                                                                                        <ItemStyle Width="15%" />
                                                                                    </asp:BoundField>
                                                                                    
                                                                                    <asp:BoundField HeaderText="Assigned By" DataField="AssignedBy" SortExpression="AssignedBy" >
                                                                                        <ItemStyle Width="12%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Assigned To" DataField="AssignedTo" SortExpression="AssignedTo" >
                                                                                        <ItemStyle Width="12%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Reason" DataField="Reason" SortExpression="Reason" >
                                                                                        <ItemStyle Width="46%" CssClass="displayNone" />
                                                                                        <HeaderStyle CssClass="displayNone" />
                                                                                    </asp:BoundField>
                                                                                                                               
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading " ForeColor="White" />                                                    
                                                 </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  colspan="4">
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
                                                                    <td class="gap" colspan="4">
                                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>                           
                                                       
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