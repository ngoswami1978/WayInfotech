<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_MiscHardware.aspx.vb" Inherits="Popup_PUSR_MiscHardware" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Misc. Hardware History</title>
    <base target="_self"></base>
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
                <td align="center" class="heading">
                    Misc. Hardware &nbsp;History</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
            </tr>
            <tr>
                <td> 
                 <%-- ="ItemColor"--%>
               <%-- DATE  MODIREPLDATE  EQUIPMENT_NO  QTY CHALLANNO LOGGEDBY  LOGGEDDATETIME--%>
                    <asp:GridView  ID="grdMiscHistory" HeaderStyle-ForeColor="white" runat="server" AllowSorting="true"  Width="100%" EnableViewState="false" HeaderStyle-CssClass="Gridheading"
                        AlternatingRowStyle-CssClass ="lightblue"  AutoGenerateColumns="False"
                        BorderColor="#d4d0c8" BorderWidth="1">
                        <Columns>
                            <asp:TemplateField  SortExpression ="DATE" HeaderText="Date" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  >
                                <ItemTemplate>
                                    <%#Eval("DATE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left"  />
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression ="MODIREPLDATE" HeaderText="Modi./Repl. Date" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <ItemTemplate>
                                    <%#Eval("MODIREPLDATE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression ="EQUIPMENT_NO" HeaderText="Equipment No." >
                                <ItemTemplate>
                                    <%#Eval("EQUIPMENT_NO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField> 
                            <asp:TemplateField SortExpression ="QTY" HeaderText="Quantity" >
                                <ItemTemplate>
                                    <%#Eval("QTY")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  
                            <asp:TemplateField SortExpression ="CHALLANNO" HeaderText="Challan No." >
                                <ItemTemplate>
                                    <%#Eval("CHALLANNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  
                            <asp:TemplateField SortExpression ="LOGGEDBY" HeaderText="Logged By" >
                                <ItemTemplate>
                                    <%#Eval("LOGGEDBY")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  
                            <asp:TemplateField SortExpression ="LOGGEDDATETIME" HeaderText="LoggedBy Date Time" >
                                <ItemTemplate>
                                    <%#Eval("LOGGEDDATETIME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                                  
                        </Columns>
                             <AlternatingRowStyle CssClass="lightblue" />
                            <RowStyle CssClass="textbold" />
                            <HeaderStyle CssClass="Gridheading" />  
                    </asp:GridView>
                </td>
            </tr>
            <tr>
            <td>
            
            <iframe id="ifrmPrnt" runat="server"  frameborder="0" src="BDRHistoryPrint.aspx" height="0" width="0"   ></iframe></td></tr>
            
              <!-- code for paging----->
                                            <tr>                                                   
                                                    <td valign ="top">
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  ReadOnly="true" Width="105px" CssClass="textboxgrey" TabIndex="5" ></asp:TextBox></td>
                                                                          <td style="width: 200px" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="5"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="5" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next" TabIndex="5">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                
            <!-- code for paging----->
             
        </table>
    </form>
</body>
</html>
