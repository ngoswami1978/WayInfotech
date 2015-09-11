<%@ Page Language="VB" MaintainScrollPositionOnPostback ="true"  AutoEventWireup="false" CodeFile="PUSR_PCInstallationHistory.aspx.vb" Inherits="Popup_PUSR_PCInstallationHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PC installation History</title>
   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right"><%--<a href="#" class="LinkButtons" onclick="PrintHistory();">Print</a>&nbsp;&nbsp;--%>&nbsp;
                    <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="heading" style="width:860px;">
                    PC Installation History</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
            </tr>
            <tr>
                <td>
                  <asp:GridView ID="grdPCInstallHistory" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False" AllowSorting ="true"  HeaderStyle-ForeColor ="white"
                                                                       Width="1200px" TabIndex="9">
                     <Columns>
                            <asp:TemplateField HeaderText="Date Time" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" SortExpression ="DATETIME"  >
                                <ItemTemplate>
                                    <%#Eval("DATETIME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CPU TYPE" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  SortExpression ="CPUTYPE" >
                                <ItemTemplate>
                                    <%#Eval("CPUTYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CPU NO" SortExpression ="CPUNO" >
                                <ItemTemplate>
                                    <%#Eval("CPUNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>   
                             <asp:TemplateField HeaderText="MON TYPE"   SortExpression ="MONTYPE"  >
                                <ItemTemplate>
                                    <%#Eval("MONTYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  
                              <asp:TemplateField HeaderText="MON NO"  SortExpression ="MONNO" >
                                <ItemTemplate>
                                    <%#Eval("MONNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  
                             <asp:TemplateField HeaderText="KBD TYPE"  SortExpression ="KBDTYPE" >
                                <ItemTemplate>
                                    <%#Eval("KBDTYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField> 
                               <asp:TemplateField HeaderText="KBD NO"  SortExpression ="KBDNO" >
                                <ItemTemplate>
                                    <%#Eval("KBDNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField> 
                               <asp:TemplateField HeaderText="MSET YPE" SortExpression ="MSETYPE"   >
                                <ItemTemplate>
                                    <%#Eval("MSETYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="MSENO"  SortExpression ="MSENO" >
                                <ItemTemplate>
                                    <%#Eval("MSENO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="CDR NO" SortExpression ="CDRNO" >
                                <ItemTemplate>
                                    <%#Eval("CDRNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>   
                             <asp:TemplateField HeaderText="IORDER NUMBER" SortExpression ="IOrderNumber"  >
                                <ItemTemplate>
                                    <%#Eval("IOrderNumber")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="DORDER NUMBER " SortExpression ="DOrderNumber"  >
                                <ItemTemplate>
                                    <%#Eval("DOrderNumber")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField> 
                              <asp:TemplateField HeaderText="REMARKS" SortExpression ="REMARKS"  >
                                <ItemTemplate>
                                    <%#Eval("REMARKS")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  
                               <asp:TemplateField HeaderText="CHALLAN NO" SortExpression ="CHALLANNUMBER"  >
                                <ItemTemplate>
                                    <%#Eval("CHALLANNUMBER")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>   
                             <asp:TemplateField HeaderText="ACTION"  SortExpression ="ActionTaken"  >
                                <ItemTemplate>
                                    <%#Eval("ActionTaken")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField> 
                              <asp:TemplateField HeaderText="USER NAME"  SortExpression ="UserName"  >
                                <ItemTemplate>
                                    <%#Eval("UserName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>    
                              <asp:TemplateField HeaderText="LOGGED DATETIME" SortExpression ="LoggedDateTime"  >
                                <ItemTemplate>
                                    <%#Eval("LoggedDateTime")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                          
                        </Columns>
                                                                                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                 
                </td>
            </tr>
             <tr>
                                                                <td colspan="6" >
                                                                     <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly ="true"  ></asp:TextBox></td>
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
            
            <td>
            
            <iframe id="ifrmPrnt" runat="server"  frameborder="0" src="BDRHistoryPrint.aspx" height="0" width="0"   ></iframe></td></tr>
             
        </table>
    </form>
</body>
</html>
