<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSHT_Permission.aspx.vb" Inherits="Setup_MSHT_Permission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SetUp:Permission History</title>
    <script src="../JavaScript/WAY.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/WAY.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
     
     function fnWOID()
    {
   
    window.close();
        return false;
   
    }
     </script>
</head>
<body  >
    <form id="frmPtrHistory" runat="server" >
        <table width="860px" align="left" height="486px" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">  
                         <tr>
                            <td align="right">
                                <asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnWOID()" >Close</asp:LinkButton>&nbsp;&nbsp;&nbsp;</td>
                        </tr>                      
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Employee Permission History</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4" align="center">
                                                                    <asp:GridView ID="gvEmployeeHistory" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False"
                                                                        Width="99%" TabIndex="9" AllowSorting="true"  HeaderStyle-ForeColor ="white">
                                                                        <Columns>
                                                                        <asp:BoundField DataField="USER" HeaderText="Employee Name" SortExpression="USER" ItemStyle-Width="15%" HeaderStyle-Width="15%" />
                                                                        <asp:BoundField DataField="LOGDATE" HeaderText="Date Time" SortExpression="LOGDATE" ItemStyle-Width="15%" HeaderStyle-Width="15%" >
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Sec_Group" HeaderText="Category" SortExpression="Sec_Group" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                                                        <asp:BoundField DataField="SecurityOptionSubName" HeaderText="Sub Category" SortExpression="SecurityOptionSubName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                                                            
                                                                        <asp:BoundField   DataField="CHANGEDDATA" HeaderText="Change Data" SortExpression="CHANGEDDATA" ItemStyle-Width="40%" HeaderStyle-Width="40%" />                                                                                                                                                      
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
                                                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 35%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="70px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 20%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 25%" class="center">
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
                                
                                <asp:Literal ID="litAgencyGroup" runat="server" ></asp:Literal>
                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                        </tr>
                    </table>
                    
                                
               
            </tr>
        </table>
    </form>
</body>
</html>
