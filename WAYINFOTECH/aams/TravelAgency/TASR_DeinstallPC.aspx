<%@ Page Language="VB" AutoEventWireup="false" MaintainScrollPositionOnPostback="true"  CodeFile="TASR_DeinstallPC.aspx.vb" Inherits="TravelAgency_TASR_DeinstallPC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Agency PC DeInstallation</title>
     <script type="text/javascript" language ="javascript" >  
     
     
    
   </script>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
       
       
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body  >
    <form id="form1" runat="server" >
   <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left" style="height: 20px">
                                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Agency</span>
                                        </td>
                                        <td style="height: 20px"></td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center"  style="width:1000px;">
                                            &nbsp;Agency PC Deinstallation</td>
                                            <td bgcolor="#1A61A9"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td align="right" style ="width:860px;">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr> 
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td valign="top" class="redborder"  width="100%" >
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width:860px;height: 22px" class="textbold" align="center" valign="TOP" rowspan="0" >
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>                                              
                                                
                                               <tr>
                                                   <td>
                                                       <table width="860px" border="0" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                               <td align="right" class="textbold">
                                                                   <asp:Button id="btnExport" tabIndex=1 runat="server" CssClass="button" Text="Export" Width="82px" AccessKey="E"></asp:Button>
                                                                   </td>
                                                                </tr>
                                                               <%--  <tr>
                                                                        <td class="textbold" style="height: 25px">
                                                                        </td>
                                                                        <td class="subheading" colspan="4" style="height: 25px">
                                                                            Agency &nbsp;Details</td>
                                                                            <td></td>
                                                                </tr>                                                                
                                                                <tr>
                                                                    <td width="6%" class="textbold" height="25px">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="width: 288px">
                                                                        Agency Name
                                                                        </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyName" CssClass="textboxgrey" MaxLength="50" runat="server" Width="97%" ReadOnly="True" TabIndex="1"></asp:TextBox> </td>
                                                                    <td style="width: 176px">
                                                                        <asp:Button id="btnExport" tabIndex=3 runat="server" CssClass="button" Text="Export" Width="82px"></asp:Button></td>
                                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" height="25" width="6%">
                                                                  </td>
                                                                  <td class="textbold" style="width: 288px" valign="top" >
                                                                      Address</td>
                                                                  <td colspan="3">
                                                                      <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Height="48px"
                                                                          MaxLength="40" ReadOnly="True" Width="97%" TabIndex="2"></asp:TextBox></td>
                                                                  <td style="width: 176px" valign="top" >
                                                                 </td>
                                                              </tr>
                                                            <tr>
                                                                <td class="textbold" width="6%" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 288px;">
                                                                    City</td>
                                                                <td style="height: 25px; width: 616px;">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="20" ReadOnly="True"
                                                                        Width="142px" TabIndex="3"></asp:TextBox></td>
                                                                <td width="18%" class="textbold" style="height: 25px">
                                                                    Country</td>
                                                                <td style="height: 25px; width: 184px;">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="20"
                                                                        ReadOnly="True" Width="143px" TabIndex="4"></asp:TextBox></td>
                                                                <td style="height: 25px; width: 176px;">
                                                                </td>
                                                            </tr>
                                                           <tr>
                                                               <td class="textbold" style="height: 25px" width="6%">
                                                               </td>
                                                               <td class="textbold" style="width: 288px; height: 25px">
                                                                   Online Status</td>
                                                               <td style="width: 616px; height: 25px">
                                                                   <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" MaxLength="20" ReadOnly="True"
                                                                       TabIndex="3" Width="142px"></asp:TextBox></td>
                                                               <td class="textbold" style="height: 25px" width="18%">
                                                                   Online Date</td>
                                                               <td style="width: 184px; height: 25px">
                                                                   <asp:TextBox ID="txtOnlineDate" runat="server" CssClass="textboxgrey" MaxLength="20" ReadOnly="True"
                                                                       TabIndex="3" Width="142px"></asp:TextBox></td>
                                                               <td style="width: 176px; height: 25px">
                                                               </td>
                                                           </tr>--%>
                                                            <tr>
                                                                <td  colspan ="6" align="right"  class="textbold" >
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" Width="82px" /></td>
                                                            </tr>                                                           
                                                       </table>
                                                   </td>
                                                </tr>
                                                <tr>
                                                   <td>
                                                   </td>
                                                </tr>
                                                <tr>
                                                    <td  valign="top"  width="2000px">
                                                       <asp:Panel ID="pnlPcDeinstall" runat="server" Width="2000px">
                                                           <table width="2000px" border="0" align="left" cellpadding="0" cellspacing="0">                                                                                                                                                                            
                                                                <tr>
                                                                <td width="2000px">   
                                                                    <asp:GridView ID="gvPcDeInstallation" runat="server"  EnableViewState ="true" AutoGenerateColumns="False" TabIndex="5" width="1900px" AllowSorting="true"  HeaderStyle-ForeColor="white"  HeaderStyle-HorizontalAlign ="Left"  RowStyle-HorizontalAlign ="left"  >
                                                                                         <Columns>   
                                                                                                          <asp:TemplateField HeaderText="Installaltion Date" ItemStyle-Wrap="false" SortExpression ="INSTALLATIONDATE" >                                                                                
                                                                                                            <itemtemplate>
                                                                                                                <%#Eval("INSTALLATIONDATE")%>
                                                                                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("ROWID")%>' />
                                                                                                                 <asp:HiddenField ID="hdPcType" runat="server"  />
                                                                                                                <asp:HiddenField ID="hdCpuType" runat="server" Value='<%#Eval("CPUTYPE")%>' />
                                                                                                                 <asp:HiddenField ID="hdCpuNo" runat="server" Value='<%#Eval("CPUNO")%>' />
                                                                                                                  <asp:HiddenField ID="hdMonType" runat="server" Value='<%#Eval("MONTYPE")%>' />
                                                                                                            </itemtemplate>
                                                                                                           </asp:TemplateField> 
                                                                                                             <asp:BoundField DataField="DEINSTALLATIONDATE" HeaderText="DeInstallation Date"  SortExpression ="DEINSTALLATIONDATE"   >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>        
                                                                                                             <asp:BoundField DataField="CPUTYPE" HeaderText="Cpu Type"  SortExpression ="CPUTYPE"   >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>                                                                                                                                                                     
                                                                                                             <asp:BoundField DataField="CPUNO" HeaderText="Cpu No." SortExpression ="CPUNO" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>                                                                                                                                                                     
                                                                                                             <asp:BoundField DataField="MONTYPE" HeaderText="Monitor Type"  SortExpression ="MONTYPE" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MONNO" HeaderText="Monitor No."  SortExpression ="MONNO" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="KBDTYPE" HeaderText="Kbd Type"  SortExpression ="KBDTYPE"  >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="KBDNO" HeaderText="Kbd No."  SortExpression ="KBDNO" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MSETYPE" HeaderText="Mse Type"  SortExpression ="MSETYPE"  >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MSENO" HeaderText="Mse No." SortExpression ="MSENO"  >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField> 
                                                                                                              <asp:BoundField DataField="CDRNO" HeaderText="Cdr No."   SortExpression ="CDRNO" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>    
                                                                                                             <asp:BoundField DataField="INSORDERNUMBER" HeaderText="Install Order No." SortExpression ="INSORDERNUMBER"  >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>
                                                                                                               <asp:BoundField DataField="DEINSORDERNUMBER" HeaderText="DeInstall Order No." SortExpression ="DEINSORDERNUMBER"  >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>          
                                                                                                             <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression ="REMARKS" >
                                                                                                                 <ItemStyle Wrap="True"  Width ="200px"  />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="CHALLANNUMBER" HeaderText="Challan No." SortExpression ="CHALLANNUMBER" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField> 
                                                                                                             <asp:BoundField DataField="LOGGEDBY" HeaderText="Logged By" SortExpression ="LOGGEDBY" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="LOGGEDDATETIME" HeaderText="Logged Date Time"  SortExpression ="LOGGEDDATETIME" >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField> 
                                                                                                         </Columns>
                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                        <RowStyle CssClass="textbold"  />
                                                                                                        <HeaderStyle CssClass="Gridheading" Wrap ="False" />   
                                                                                                        
                                                                                         </asp:GridView>
                                                                </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                      <td style="height: 91px">
                                                             <table width ="500px;">
                                                                <tr>
                                                                    <td  class="textbold" ><b>Total PC Online</b></td><td></td>
                                                                </tr>
                                                                  <tr>
                                                                    <td  class="textbold" ><b>1A</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt1ATotal" runat="server" CssClass ="textboxgrey" ReadOnly ="true"  Width="105px" ></asp:TextBox></td><td><b>Agency</b>&nbsp;&nbsp;<asp:TextBox ID="txtAgencyTotal" runat="server" CssClass ="textboxgrey" ReadOnly ="true"  Width="105px" ></asp:TextBox></td>
                                                                </tr>
                                                              <tr>
                                                                    <td></td><td></td>
                                                                </tr>
                                                             </table>
                                                       
                                                      </td>
                                               </tr>
                                                  <tr>
                                                                <td  style="width:850px;" >
                                                                 <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                                          <td style="width: 50%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly ="true"  ></asp:TextBox></td>
                                                                                          <td style="width: 15%" class="right">                                                                             
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
