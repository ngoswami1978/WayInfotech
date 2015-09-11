<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyMiscInstallation.aspx.vb" Inherits="TravelAgency_TAUP_AgencyMiscInstallation" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
       <title>Agency Miscellineous Installation</title>
    <script type="text/javascript" language ="javascript" >  
       function DeinstallMisc()
       {
            var type;      
            type = "TASR_DeinstallMisc.aspx?Popup=T" ;
            window.open(type,"POPDeinstallMisc","height=600,width=930,top=30,left=20,scrollbars=1,status=1");	
            return false;
                            
       }
          
    
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

<body>
    <form id="form1" runat="server">
    <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Agency</span>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center" style="width:1000px;">
                                            Manage Agency Misc. Installation</td>
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
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="left" valign="TOP" rowspan="0" style="height: 23px;padding-left:300px;">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                                    <tr> 
                                                        <td align="right" valign="top" style="padding-right:200px;">
                                                        <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" Width="114px" />
                                                        </td>
                                                    </tr>
                                                      <tr> 
                                                        <td align="right" valign="top" style="padding-right:200px;">
                                                         <input type="button" id="btnDeinstallMisc" runat="server" tabIndex="3"  class="button" value="Deinstall Misc." style="width: 114px" onclick="javascript:return DeinstallMisc();" accesskey="D"/>
                                                        </td>
                                                    </tr>
                                                  
                                                    <tr height="10px" ></tr>
                                                
                                                <tr>
                                                    <td width="1400px" valign="top">
                                                       <asp:Panel ID="pnlEmployee" runat="server" Width="1200px">
                                                            <table width="1400px" border="0" align="left" cellpadding="2" cellspacing="0">                                                           
                                                                <tr>                                                               
                                                                <td >                                                                       
                                                                    <asp:Panel runat ="server" ID="pnlPcInstallation" width="1200px" >
                                                                       <asp:GridView ID="gvMiscInstallation" AllowSorting="true"  runat="server" HeaderStyle-ForeColor="White"  EnableViewState ="false" AutoGenerateColumns="False" TabIndex="5" Width="1200px" HeaderStyle-HorizontalAlign="left" RowStyle-HorizontalAlign="left">
                                                                         <Columns>                                               
                                                                           <asp:TemplateField HeaderText="Date Inst." SortExpression="DATE" ItemStyle-Wrap="false">                                                                                
                                                                                            <itemtemplate>
                                                                                                <%#Eval("DATE")%>
                                                                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("ROWID")%>' />
                                                                                            </itemtemplate>
                                                                                           </asp:TemplateField>     
                                                                                             <asp:BoundField SortExpression="EQUIPMENT_TYPE" DataField="EQUIPMENT_TYPE" HeaderText="Equipment Type"  >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField>                                                                                                                                                                     
                                                                                             <asp:BoundField SortExpression="EQUIPMENT_NO" DataField="EQUIPMENT_NO" HeaderText="Equipment No."  >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField>                                                                                                                                                                     
                                                                                             <asp:BoundField SortExpression="QTY" DataField="QTY" HeaderText="Equipment Qty."   />  
                                                                                            <%-- <asp:BoundField DataField="OrderNUmber" HeaderText="Order No."  >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField> --%> 
                                                                                             
                                                                                             <asp:BoundField SortExpression="ChallanNumber" DataField="ChallanNumber" HeaderText="Challan No." >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField>  
                                                                                             
                                                                                            <%-- <asp:BoundField DataField="CHALLANDATE" HeaderText="Challan Date"  >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField>  --%>
                                                                                             
                                                                                             <asp:BoundField SortExpression="LoggedBy" DataField="LoggedBy" HeaderText="Logged By" >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField> 
                                                                                              
                                                                                             <%--<asp:BoundField DataField="Employee_Name" HeaderText="Employee Name"  >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField>  --%>   
                                                                                             <asp:BoundField SortExpression="LoggedDateTime" DataField="LoggedDateTime" HeaderText="Logged Date Time"  >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField>     
                                                                                             <%--   
                                                                                             <asp:BoundField DataField="CHALLANDATE" HeaderText="Challan Date" >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField>     
                                                                                             <asp:BoundField DataField="CHALLANNUMBER" HeaderText="Challan No."  >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField> 
                                                                                             <asp:BoundField DataField="LoggedBy" HeaderText="Logged By" >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField>   --%> 
                                                                                             <%--<asp:BoundField DataField="CHALLANSTATUS" HeaderText="Challan Status"  >
                                                                                                 <ItemStyle Wrap="False" />
                                                                                                 <HeaderStyle Wrap="False" />
                                                                                             </asp:BoundField> --%>                                                                                                                                     
                                                                                            <asp:TemplateField >
                                                                                            <HeaderTemplate >
                                                                                                <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate   >
                                                                                            <asp:LinkButton ID="linkEdit" runat="server" Text="Edit" CssClass="LinkButtons"></asp:LinkButton>
                                                                                            
                                                                                                   &nbsp;&nbsp;
                                                                                            <asp:LinkButton ID="linkDelete" runat="server" Text="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                                                                  &nbsp;&nbsp;
                                                                                            <asp:LinkButton ID="linkDeinstall" runat="server" Text="DeInstall" CssClass="LinkButtons"></asp:LinkButton>
                                                                                                  
                                                                                                  &nbsp;&nbsp;
                                                                                            <asp:LinkButton ID="linkReplace" runat="server" Text="Replace" CssClass="LinkButtons"></asp:LinkButton>
                                                                                                     &nbsp;&nbsp; 
                                                                                            <asp:LinkButton ID="linkHistory" runat="server" Text="History" CssClass="LinkButtons"></asp:LinkButton>
                                                                                               
                                                                                                 
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%" CssClass="ItemColor" Wrap="False" />
                                                                                            <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                      </asp:TemplateField>
                                                                                         </Columns>
                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                <RowStyle CssClass="textbold" />
                                                                                <HeaderStyle CssClass="Gridheading" />   
                                                                      </asp:GridView>
                                                                    </asp:Panel> 
                                                                </td>
                                                                </tr> 
                                                                <tr>
                                                                
                                                                <td class="textbold" nowrap="nowrap" >Total Misc. H/W &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtTotalMis" runat="server" CssClass="textboxgrey" ReadOnly="true" Width="105px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                
                                                                </td>
                                                                <td colspan ="5"></td>
                                                                </tr> 
                                                                
                                                                
                                                                <tr>
                                                                <td colspan ="7"></td>
                                                                </tr> 
                                                                
                                                                 <!-- code for paging----->
                                            <tr>                                                   
                                                    <td valign ="top" colspan ="7"  >
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
                                                        </asp:Panel>
                                                        <asp:HiddenField ID="hdDel" runat="server" /><asp:HiddenField ID="hdAdd" runat="server" />
                                                        <asp:HiddenField ID="hdUpReplace" runat="server" />
                                                        &nbsp;
                                                        <asp:HiddenField ID="hdDeinstalled" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <input type="submit" id="submitPage" runat="server" style="display:none" />
                                            <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server" ></asp:TextBox>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                             
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
