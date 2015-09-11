<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_HelpDeskResultConfigure.aspx.vb" Inherits="Popup_PUSR_HelpDeskResultConfigure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HelpDesk:Configure Result</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
  
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body  oncontextmenu=" return false;" >
    <form id="frmConfigRes" runat="server" >
        <table width="860px" align="left" height="486px" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">  
                      <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.opener.location.href ='../HelpDesk/HDRPT_HelpDeskDynamicReport.aspx?Reload=T';window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>                
                        <tr>
                            <td class="heading" align="center" valign="top">
                                &nbsp;Configure Result</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                           
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr> 
                                                                <tr>
                                                                            <td>
                                                                                </td>
                                                                            <td class="textbold">
                                                                                &nbsp; &nbsp; &nbsp;&nbsp;
                                                                                Select Set &nbsp; &nbsp;
                                                                                <asp:DropDownList ID="drpSelectSet" runat="server" CssClass="dropdownlist" Width="70px" TabIndex="29" AutoPostBack="True">
                                                                             
                                                                            </asp:DropDownList></td>                                                                               
                                                                            <td colspan="3"></td>
                                                                            <td style="width: 12%;" class="center">
                                                                                                  </td>
                                                                             </tr>                                                           
                                                               <%-- <tr>
                                                                 <td  class="textbold"   style ="width:70%;"  colspan ="5"  valign="TOP">&nbsp;                                                             
                                                                </td>
                                                                
                                                                <td  class="textbold" align="left"   style ="width:30%"  valign="TOP">
                                                                    &nbsp;</td>
                                                            </tr>
                                                               <tr>
                                                                 <td  class="textbold"   style ="width:70%;"  colspan ="5"  valign="TOP">&nbsp;                                                             
                                                                </td>
                                                                
                                                                <td  class="textbold" align="left"   style ="width:30%;height:25px"  valign="TOP"></td> 
                                                                   
                                                            </tr>--%>
                                                               <tr>
                                                                 <td  class="textbold"   style ="width:60%;"  colspan ="5"  valign="TOP">&nbsp;                                                             
                                                                </td>
                                                                
                                                                <td  class="textbold" align="left"   style ="width:40%;height:25px"  valign="TOP">
                                                                    <asp:Button ID="btnNewSet" runat="server" CssClass="button" Text="New Set" />
                                                                <asp:Button ID="btnSave" CssClass="button" runat ="server" Text ="Save" />
                                                                <asp:Button ID="btnSelectAll" CssClass="button" runat ="server" Text ="Select All" />&nbsp;
                                                                    <asp:Button ID="btnDeSelectAll" CssClass="button" runat ="server" Text ="DeSelect All" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4" align="center">
                                                                    <asp:GridView ID="gvHelpDeskResCofig" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False"
                                                                        Width="95%" TabIndex="9">
                                                                        <Columns>
                                                                        <asp:BoundField DataField="DM_FIELD_NAME" HeaderText="Field Name" ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left" />
                                                                       <asp:TemplateField  HeaderText ="Select/Deselect"  ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left" >
                                                                           <ItemTemplate >
                                                                                <asp:CheckBox ID="chkSelected" runat ="server" Checked ='<%# GetEnableorDisableOrChecked(Eval("DM_RESULT_FIELD"),Eval("DM_RESULT_DEFAULT")) %>' Enabled ='<%# GetEnableorDisable(Eval("DM_RESULT_DEFAULT")) %>' />
                                                                                <asp:HiddenField ID="DM_FIELD_ID" Value ='<% # Eval("DM_FIELD_ID") %>'  runat ="server" />
                                                                            <asp:HiddenField ID="DM_RESULT_DEFAULT" Value ='<% # Eval("DM_RESULT_DEFAULT") %>'  runat ="server" />
                                                                           </ItemTemplate>
                                                                       </asp:TemplateField>
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td colspan="6" height="12"> <asp:HiddenField ID="hdAction" Value ="" runat ="server"  />
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

