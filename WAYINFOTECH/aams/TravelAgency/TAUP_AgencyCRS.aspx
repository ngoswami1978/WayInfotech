<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyCRS.aspx.vb" Inherits="TravelAgency_TAUP_AgencyCRS" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />   
   <script language="javascript" type="text/javascript" src="../JavaScript/AAMS.js"></script>
  </head>

<body onload= "PopuporWriteTesxtForCRS2();" >
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
                                            <span class="menu">Travel Agency-></span><span class="sub_menu">Agency</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Agency CRS</td>
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
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" style="height:25px">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                            <td colspan="6" ></td>
                                                            </tr>
                                                            <tr>
                                                                    <td style="width: 5%">
                                                                    </td>
                                                                    <td style="width: 17%">
                                                                        <span class="Mandatory"></span></td>
                                                                    <td style="width: 18%" class="textbold">
                                                                        CRS Code <strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">
                                                                            *</span></strong></td>
                                                                    <td class="textbold" style="width: 18%">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCRS" TabIndex="1" CssClass="dropdown" runat="server">
                                                                        </asp:DropDownList></td>
                                                                <td style="width: 3%">
                                                                </td>
                                                                    <td style="width: 20%">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="5" CssClass="button" Text="Save" AccessKey="S" /></td>
                                                                    <td style="width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td  class="textbold">
                                                                        Office Id<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">
                                                                            *</span></strong></td>
                                                                    <td class="textbold"  >
                                                                        <asp:TextBox ID="txtOfficeId" TabIndex="2" CssClass="textboxgrey"  runat="server" MaxLength="15" Width="131px"></asp:TextBox>&nbsp;</td>
                                                                    <td class="textbold">
                                                                        <img alt="Select & Add Office Id1." onclick="javascript:return PopupAgencyGroupOfficeIDForCRS();"
                                                                          runat="server"  id ="ImgOfficeId"  src="../Images/lookup.gif" tabindex="32" /></td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="6" CssClass="button" Text="Reset" AccessKey="R" /></td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Current Id</td>
                                                                    <td class="textbold">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCurrentID" TabIndex="3" CssClass="dropdown" runat="server">
                                                                            <asp:ListItem Value='N'>False</asp:ListItem>
                                                                            <asp:ListItem Value='Y'>True</asp:ListItem>                                                                            
                                                                        </asp:DropDownList>&nbsp;
                                                                        <asp:HiddenField ID="hdCity" runat="server" />
                                                                    </td>
                                                                    <td class="textbold">
                                                                        <asp:Button ID="btnAdd" TabIndex="4" runat="server" CssClass="button" Text="Add" AccessKey="N" /></td>
                                                                    <td>
                                                                     <input type="button" Class="button" TabIndex="22"  value="History" onclick="javascript:PopupCRSHistoryPageForCRS();" id="btnHistory" accesskey="H" />
                                                                       <%-- <asp:Button ID="btnHistory" runat="server" TabIndex="22" CssClass="button" Text="History" />--%><asp:HiddenField ID="hdLcode" runat="server" Value="220" /></td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    
                                                                    <td class="textbold" colspan="7">
                                                                          <asp:GridView  ID="grdCRSAgency" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" EnableViewState="true"  AllowSorting ="true"  HeaderStyle-ForeColor="white" > 
                                                                      <Columns>
                                                                                 <asp:TemplateField HeaderText="CRS Code" SortExpression ="CRS">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLocationCode" runat="server" Text='<%#Eval("CRS")%>'></asp:Label>
                                                                                        <asp:HiddenField ID="hdRN" runat="server" value='<%#Eval("RN")%>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Office ID"  SortExpression ="OFFICEID">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("OFFICEID")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Current ID"  SortExpression ="CURRENTID">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CURRENTID")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>   
                                                                                
                                                                                   <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkEdit" CssClass="LinkButtons" runat="server" CommandName="EditX" CommandArgument='<%#Eval("TRN")%>' Text="Edit"></asp:LinkButton>&nbsp;
                                                                                        <asp:LinkButton ID="lnkDelete" CssClass="LinkButtons" runat="server" CommandName="DeleteX" CommandArgument='<%#Eval("TRN")%>' Text="Delete"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>                                                                          
                                                                               
                                                                             
                                                                           </Columns>                                                                       
                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                    <RowStyle CssClass="textbold" />
                                                                    <HeaderStyle CssClass="Gridheading" />
                                                                    <pagersettings  
                                                                      pagebuttoncount="5"/>
                                                                             
                                                   
                                                    
                                                 </asp:GridView>
                                                                      </td>
                                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td colspan="5">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td colspan="5" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>
                                                                        &nbsp;</td>
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
<script  type ="text/javascript" language ="javascript" >
function PopuporWriteTesxtForCRS2()
       {
           var Index;
           Index=document.getElementById('drpCRS').selectedIndex;
           if(document.getElementById('drpCRS').options[Index].value!="1A")
            {            
                document.getElementById('txtOfficeId').readOnly =false;
                document.getElementById('txtOfficeId').className ="textbox";
                document.getElementById('ImgOfficeId').style.display  ="none";
              
                return false;
            }
            else
            {
                document.getElementById('txtOfficeId').readOnly =true; 

                document.getElementById('txtOfficeId').className ="textboxgrey";
                document.getElementById('ImgOfficeId').style.display ="block";
                return false;
            
            }
       }
    
   PopuporWriteTesxtForCRS2();
</script>