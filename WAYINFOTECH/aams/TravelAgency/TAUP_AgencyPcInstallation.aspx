<%@ Page Language="VB" AutoEventWireup="false"  MaintainScrollPositionOnPostback ="true"  CodeFile="TAUP_AgencyPcInstallation.aspx.vb" Inherits="TravelAgency_TAUP_AgencyPcInstallation" %>
<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Agency PC Installation</title>
     <script type="text/javascript" language ="javascript" >  
       function DeinstallPC()
       {
            var type;      
            type = "TASR_DeinstallPC.aspx?Popup=T" ;
            window.open(type,"POPDeinstallPC","height=600,width=930,top=30,left=20,scrollbars=1,status=1");	
            return false;
                            
       }
          
    
   </script>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
       
       
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
                                            Manage Agency PC Installation</td>
                                            <td bgcolor="#1A61A9"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <uc1:MenuControl ID="MenuControl1" runat="server" />
                                        </td>
                                    </tr>
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
                                                                <td  align="right"  class="textbold" ><asp:Button id="btnNewAgency" tabIndex=3 runat="server" CssClass="button" Text="New Agency PC" Width="129px"></asp:Button></td>
                                                            </tr>
                                                             <tr>
                                                                <td  align="right"   class="textbold"><asp:Button id="btnNew1APc" tabIndex=3 runat="server" CssClass="button" Text="New 1A PC" Width="129px"></asp:Button></td>
                                                            </tr>
                                                           <tr>
                                                               <td align="right" class="textbold">
                                                                   <input type="button" id="btnDeinstallPC" runat="server" tabIndex="3"  class="button" value="Deinstall PC" style="width: 129px" onclick="javascript:return DeinstallPC();" accesskey="D"/>
                                                                   </td>
                                                           </tr>
                                                            <tr>
                                                                <td  align="right"   class="textbold">
                                                                    <asp:HiddenField ID="hdRowId" runat="server" />
                                                                </td>
                                                            </tr>
                                                            
                                                       </table>
                                                   </td>
                                                </tr>
                                                <tr>
                                                    <td  valign="top"  width="2000px">
                                                       <asp:Panel ID="pnlEmployee" runat="server" Width="2000px">
                                                           <table width="2000px" border="0" align="left" cellpadding="0" cellspacing="0">                                                                                                                                                                            
                                                                <tr>
                                                                <td width="2000px">   
                                                                    <asp:GridView ID="gvPcInstallation" runat="server"  EnableViewState ="true" AutoGenerateColumns="False" TabIndex="5" width="1900px" AllowSorting="true"  HeaderStyle-ForeColor="white"  HeaderStyle-HorizontalAlign="left"  RowStyle-HorizontalAlign="left" >
                                                                                         <Columns>                                               
                                                                                                          <asp:TemplateField HeaderText="Date" ItemStyle-Wrap="false" SortExpression ="DATE" >                                                                                
                                                                                                            <itemtemplate>
                                                                                                                <%#Eval("DATE")%>
                                                                                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("ROWID")%>' />
                                                                                                                 <asp:HiddenField ID="hdPcType" runat="server"  />
                                                                                                                <asp:HiddenField ID="hdCpuType" runat="server" Value='<%#Eval("CPUTYPE")%>' />
                                                                                                                 <asp:HiddenField ID="hdCpuNo" runat="server" Value='<%#Eval("CPUNO")%>' />
                                                                                                                  <asp:HiddenField ID="hdMonType" runat="server" Value='<%#Eval("MONTYPE")%>' />
                                                                                                                  <asp:HiddenField ID="hdMonNo" runat="server" Value='<%#Eval("MONNO")%>' />
                                                                                                                  
                                                                                                            </itemtemplate>
                                                                                                           </asp:TemplateField>     
                                                                                                             <asp:BoundField DataField="CPUTYPE" HeaderText="Cpu Type"  SortExpression ="CPUTYPE"   >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>                                                                                                                                                                     
                                                                                                             <asp:BoundField DataField="CPUNO" HeaderText="Cpu No." SortExpression ="CPUNO" >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>                                                                                                                                                                     
                                                                                                             <asp:BoundField DataField="MONTYPE" HeaderText="Monitor Type"  SortExpression ="MONTYPE" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MONNO" HeaderText="Monitor No."  SortExpression ="MONNO" >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="KBDTYPE" HeaderText="Kbd Type"  SortExpression ="KBDTYPE"  >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="KBDNO" HeaderText="Kbd No."  SortExpression ="KBDNO" >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MSETYPE" HeaderText="Mse Type"  SortExpression ="MSETYPE"  >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MSENO" HeaderText="Mse No." SortExpression ="MSENO"  >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="OrderNumber" HeaderText="Order No." SortExpression ="OrderNumber"  >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression ="REMARKS" >
                                                                                                                 <ItemStyle Wrap="True"  Width ="200px"  />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="CHALLANDATE" HeaderText="Challan Date" SortExpression ="CHALLANDATE"  >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="True" />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="CHALLANNUMBER" HeaderText="Challan No." SortExpression ="CHALLANNUMBER" >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField> 
                                                                                                           <%--  <asp:BoundField DataField="LoggedBy" HeaderText="Logged By" SortExpression ="LoggedBy" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>   --%> 
                                                                                                             <asp:BoundField DataField="Employee_Name" HeaderText="Logged By" SortExpression ="Employee_Name" >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="LoggedDateTime" HeaderText="Logged Date Time"  SortExpression ="LoggedDateTime" >
                                                                                                                 <ItemStyle Wrap="false" />
                                                                                                                 <HeaderStyle Wrap="True" />
                                                                                                             </asp:BoundField>      
                                                                                                             <asp:BoundField DataField="CHALLANSTATUS" HeaderText="Challan Status" SortExpression ="CHALLANSTATUS" >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="true" />
                                                                                                             </asp:BoundField>   
                                                                                                             <asp:BoundField DataField="CDRNO" HeaderText="Cdr No."   SortExpression ="CDRNO" >
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>
                                                                                                             <asp:BoundField DataField="LastModifiedDate" HeaderText="Last Modified Date"   SortExpression ="LastModifiedDate" >
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="True" />
                                                                                                             </asp:BoundField>                                                                                                                  
                                                                                                             <asp:TemplateField HeaderStyle-Wrap="false"  ItemStyle-Wrap="true"  ItemStyle-Width="400px"  >
                                                                                                                    <HeaderTemplate    >
                                                                                                                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                                                    </HeaderTemplate>
                                                                                                                    <ItemTemplate  ><a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;&nbsp; <asp:LinkButton ID="linkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>&nbsp;&nbsp; <a href="#" class="LinkButtons" id="linkHistory" runat="server">History</a>&nbsp;&nbsp; <a href="#" class="LinkButtons" id="linkDeinstall" runat="server">DeInstall</a>&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkReplace" runat="server">Replace</a>
                                                                                                                    </ItemTemplate>                                                                                                                   
                                                                                                              </asp:TemplateField>
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
                                                                    <td  class="textbold" ><b>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 1A</b>&nbsp;<asp:TextBox ID="txt1ATotal" runat="server" CssClass ="textboxgrey" ReadOnly ="true"  Width="105px" ></asp:TextBox></td><td><b>Agency</b>&nbsp;<asp:TextBox ID="txtAgencyTotal" runat="server" CssClass ="textboxgrey" ReadOnly ="true"  Width="105px" ></asp:TextBox></td>
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
