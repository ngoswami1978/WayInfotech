<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_ChallanStatusPRHistory.aspx.vb" Inherits="Popup_PUSR_ChallanStatusPRHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Challan Status History</title>
   
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
                    Challan Status PC History</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
            </tr>
            <tr>
                <td> 
                    <asp:DataGrid ID="grdChallanStatusPRHistory" runat="server" Width="1200px" EnableViewState="false" HeaderStyle-CssClass="Gridheading"
                        AlternatingItemStyle-CssClass="lightblue" ItemStyle-CssClass="ItemColor" AutoGenerateColumns="False"
                        BorderColor="#d4d0c8" BorderWidth="1">
                         <Columns>
                            <asp:TemplateColumn HeaderText="DATE" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  >
                                <ItemTemplate>
                                    <%#Eval("DATE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left"  />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CPU TYPE" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <ItemTemplate>
                                    <%#Eval("CPUTYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CPU NO" >
                                <ItemTemplate>
                                    <%#Eval("CPUNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>   
                             <asp:TemplateColumn HeaderText="MON TYPE" >
                                <ItemTemplate>
                                    <%#Eval("MONTYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>  
                              <asp:TemplateColumn HeaderText="MON NO" >
                                <ItemTemplate>
                                    <%#Eval("MONNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>  
                             <asp:TemplateColumn HeaderText="KBD TYPE" >
                                <ItemTemplate>
                                    <%#Eval("KBDTYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn> 
                               <asp:TemplateColumn HeaderText="KBD NO" >
                                <ItemTemplate>
                                    <%#Eval("KBDNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn> 
                               <asp:TemplateColumn HeaderText="MSET YPE" >
                                <ItemTemplate>
                                    <%#Eval("MSETYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                              <asp:TemplateColumn HeaderText="MSENO" >
                                <ItemTemplate>
                                    <%#Eval("MSENO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                               <asp:TemplateColumn HeaderText="CDR NO" >
                                <ItemTemplate>
                                    <%#Eval("CDRNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>   
                             <asp:TemplateColumn HeaderText="IORDER NUMBER" >
                                <ItemTemplate>
                                    <%#Eval("IORDERNUMBER")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            
                             <asp:TemplateColumn HeaderText="DORDER NUMBER " >
                                <ItemTemplate>
                                    <%#Eval("DORDERNUMBER")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn> 
                              <asp:TemplateColumn HeaderText="REMARKS" >
                                <ItemTemplate>
                                    <%#Eval("REMARKS")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>  
                               <asp:TemplateColumn HeaderText="CHALLAN NO" >
                                <ItemTemplate>
                                    <%#Eval("CHALLANNO")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>   
                             <asp:TemplateColumn HeaderText="ACTION" >
                                <ItemTemplate>
                                    <%#Eval("ACTION")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn> 
                              <asp:TemplateColumn HeaderText="USER NAME" >
                                <ItemTemplate>
                                    <%#Eval("USERNAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>    
                              <asp:TemplateColumn HeaderText="LOGGED DATETIME" >
                                <ItemTemplate>
                                    <%#Eval("LOGGEDDATETIME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>                          
                        </Columns>
                        <AlternatingItemStyle CssClass="lightblue" />
                        <ItemStyle CssClass="textbold" />
                        <HeaderStyle CssClass="Gridheading" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
            <td>
            
            <iframe id="ifrmPrnt" runat="server"  frameborder="0" src="BDRHistoryPrint.aspx" height="0" width="0"   ></iframe></td></tr>
             
        </table>
    </form>
</body>
</html>
