<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_UpdateAResHistory.aspx.vb" Inherits="Popup_PUSR_UpdateAResHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>1A Responsibilty Update History</title>
        <base target="_self"/>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="heading">
                    1A Responsibilty Update History</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
            </tr>
            <tr>
                <td>
                     <asp:GridView ID="grd1AResUpdateHistory" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False" AllowSorting ="true" HeaderStyle-ForeColor ="white" 
                                                                        Width="100%">
                                                                           <Columns>
                                                                            <asp:TemplateField HeaderText="Employee Name" SortExpression ="EMPNAME" >
                                                                                <ItemTemplate>
                                                                                    <%#Eval("EMPNAME")%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date Time" SortExpression ="DATE"  >
                                                                                <ItemTemplate>
                                                                                    <%#Eval("DATE")%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" Width="20%"/>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Responsible Name"  SortExpression ="RESP_NAME"  >
                                                                                <ItemTemplate>
                                                                                    <%#Eval("RESP_NAME")%>
                                                                                </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="60%"/>
                                                                                </asp:TemplateField>                                                  
                                                                            </Columns>
                                                                                                                                                                                                     
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                    
                </td>
            </tr>
               <tr>
                    <td  >
                         <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                              <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="82px" CssClass="textboxgrey" ReadOnly ="true"  ></asp:TextBox></td>
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
    </form>
</body>
</html>
