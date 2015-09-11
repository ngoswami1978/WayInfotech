<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyCompetition.aspx.vb" Inherits="TravelAgency_TAUP_AgencyCompetition" %>
<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />        
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />      
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
   </head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type ="text/javascript" >
 function HideParentImage()
    {
       try
       {
         window.parent.document.getElementById('PnlCalenderImagePnl').className   ="displayNone";
         window.parent.document.getElementById('CalenderImage').className  ="displayNone";
       //  window.parent.document.getElementById('iframeID').height="480px";
       }catch(e) {}
        
    }
    
     function ValidationCompetitionManageComp2()
    {
        if(document.getElementById('drpCRSCode').selectedIndex==0)
        {            
            document.getElementById('lblError').innerHTML='CRS is mandatory.';
            document.getElementById('drpCRSCode').focus();
            return false;
        }
        if(document.getElementById('txtDateStart').value != '')
        {
            if (isDate(document.getElementById('txtDateStart').value,"d/M/yyyy") == false)	
            {
                document.getElementById('lblError').innerText = "Enter valid Start date.";			
	            document.getElementById('txtDateStart').focus();
	            return(false);  
             }
        }
        if(document.getElementById('txtDateEnd').value != '')
        {
            if (isDate(document.getElementById('txtDateEnd').value,"d/M/yyyy") == false)	
            {
                document.getElementById('lblError').innerText = "Enter valid End date.";			
	            document.getElementById('txtDateEnd').focus();
	            return(false);  
            }
        }
           if (compareDates(document.getElementById('txtDateStart').value,"d/M/yyyy",document.getElementById('txtDateEnd').value,"d/M/yyyy")==1)
        {
           document.getElementById('lblError').innerText = "Start date can't be greater than end date.";			
	       document.getElementById('txtDateStart').focus();
	       return(false);  
        }
         if(document.getElementById('txtPCCount').value != '')
        {
           if(IsDataValid(document.getElementById("txtPCCount").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="PC Count is not valid.";
            document.getElementById("txtPCCount").focus();
            return false;
            } 
        }
          if(document.getElementById('txtPrinterCount').value != '')
        {
           if(IsDataValid(document.getElementById("txtPrinterCount").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Printer Count is not valid.";
            document.getElementById("txtPrinterCount").focus();
            return false;
            } 
        }
          var strValue = document.getElementById('TxtCommercialDetail').value.trim();
          if (strValue == '')
         {      
//            document.getElementById("lblError").innerHTML="Commercial Detail is mandatory.";
//            document.getElementById("TxtCommercialDetail").focus();
//            return false;                                                       
          }  
           if (strValue !='')
           {
               if (strValue.length>1000)
                { 
                      document.getElementById("lblError").innerHTML="Commercial Detail can't be greater than 1000 characters."
                      document.getElementById("TxtCommercialDetail").focus();
                     return false;
                }   
           }
    }
    
</script>
<body onload ="HideParentImage()" >
    <form id="form1" runat="server">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top" style="height: 548px">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Agency</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Agency Competition
                                        </td>
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
                                                    <td class="textbold" align="center" valign="TOP"  style="height:25px">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                            <td colspan="5" ></td>
                                                            </tr>
                                                             <tr>
                                                                    <td style="width: 6%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 12%">
                                                                        CRS Code<span class="Mandatory">*</span></td>
                                                                    <td style="width: 20%"><asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCRSCode" TabIndex="1" CssClass="dropdown" runat="server" >
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 12%">
                                                                        Sole User<span class="Mandatory">*</span></td>
                                                                    <td style="width: 20%">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpSoleUser" CssClass="dropdownlist" TabIndex="1" runat="server">
                                                                        <asp:ListItem>False</asp:ListItem>
                                                                            <asp:ListItem>True</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 16%">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button" Text="Save" AccessKey="S" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Connectivity&nbsp; Status</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpOnlineStatus" TabIndex="1" CssClass="dropdown" runat="server">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Backup Connectivity</td>
                                                                    <td style="width: 20%">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpDialBackup" CssClass="dropdown" TabIndex="1" runat="server">
                                                                        <asp:ListItem>False</asp:ListItem>
                                                                            <asp:ListItem>True</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button" Text="Reset" AccessKey="R" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px">
                                                                        PC Count</td>
                                                                    <td style="height: 25px">
                                                                        <asp:TextBox ID="txtPCCount" runat="server" CssClass="textbox" MaxLength="4" TabIndex="1"></asp:TextBox></td>
                                                                    <td class="textbold" style="height: 25px">
                                                                        Printer Count</td>
                                                                    <td style="width: 20%; height: 25px">
                                                                        <asp:TextBox ID="txtPrinterCount" CssClass="textbox" TabIndex="1" runat="server" MaxLength="4"></asp:TextBox></td>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Start</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateStart" CssClass="textbox" TabIndex="1" runat="server" MaxLength="10"></asp:TextBox>
                                                                        <img id="imgDateStart" alt="" src="../Images/calender.gif" TabIndex="1" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateStart.ClientID%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateStart",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                                                  </td>
                                                                    <td class="textbold">
                                                                        Date End</td>
                                                                    <td style="width: 20%">
                                                                        <asp:TextBox ID="txtDateEnd" CssClass="textbox" TabIndex="1" runat="server" MaxLength="10"></asp:TextBox>
                                                                        <img id="imgtxtDateEnd" alt="" src="../Images/calender.gif" TabIndex="1" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateEnd.ClientID%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgtxtDateEnd",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Commercial Details</td>
                                                                    <td class="textbold" colspan="2">
                                                                        <asp:TextBox ID="TxtCommercialDetail" runat="server" CssClass="textbox" Height="50px" MaxLength="4"
                                                                            TabIndex="1" TextMode="MultiLine" Width="297px"></asp:TextBox></td>
                                                                    <td style="width: 20%">
                                                                        <asp:Button ID="btnAdd" runat="server" TabIndex="2" CssClass="button" Text="Add" AccessKey="N" /></td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td style="width: 20%">
                                                                        &nbsp;</td>
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
                                                                    <td style="width: 20%">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    
                                                                     <td colspan="6">
                                                                     
                                                                       <asp:GridView  ID="grdComptitionAgency" runat="server"  AutoGenerateColumns="False" TabIndex="4" Width="100%" EnableViewState="true"  AllowSorting ="true"  HeaderStyle-ForeColor="white" > 
                                                                      <Columns>                                                                              
                                                                                    <asp:TemplateField HeaderText="CRS Code" SortExpression ="CRSID">
                                                                                     <ItemTemplate>
                                                                                         <asp:Label ID="lblCRSID" runat="server" Text='<%#Eval("CRSID")%>'></asp:Label>
                                                                                         <asp:HiddenField ID="hdComptID" runat="server" Value='<%#Eval("ComptID")%>' />
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Connectivity  Status"  SortExpression ="ONLINESTATUSID">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("ONLINESTATUSID")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Date Start" SortExpression ="DATE_START">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("DATE_START")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateField>
                                                                                  <asp:TemplateField HeaderText="Date End" SortExpression ="DATE_END">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("DATE_END")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Backup Connectivity" SortExpression ="DIAL_BACKUP">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("DIAL_BACKUP")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateField>
                                                                                  <asp:TemplateField HeaderText="Sole User" SortExpression ="SOLE_USER">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("SOLE_USER")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="PC Count" SortExpression ="PC_COUNT">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("PC_COUNT")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateField>
                                                                                  <asp:TemplateField HeaderText="Printer Count" SortExpression ="PRINTER_COUNT">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("PRINTER_COUNT")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Action">
                                                                                     <ItemTemplate>
                                                                                         <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ComptID")%>' CommandName="EditX"
                                                                                             CssClass="LinkButtons" Text="Edit"></asp:LinkButton>&nbsp;
                                                                                         <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ComptID")%>' CommandName="DeleteX"
                                                                                             CssClass="LinkButtons" Text="Delete"></asp:LinkButton>
                                                                                     </ItemTemplate>
                                                                                 </asp:TemplateField>
                                                                             
                                                                           </Columns>                                                                       
                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                    <RowStyle CssClass="textbold" />
                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                    <pagersettings  
                                                                      pagebuttoncount="5"/>                                                    
                                                                      </asp:GridView>
                                                                   </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                        &nbsp;</td>
                                                                    <td >
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td >
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
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
