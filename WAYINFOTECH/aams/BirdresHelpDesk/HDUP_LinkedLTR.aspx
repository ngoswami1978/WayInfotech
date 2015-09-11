<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_LinkedLTR.aspx.vb" Inherits="BirdresHelpDesk_HDUP_LinkedLTR" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Linked LTR</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
     function DeleteFunction(HD_RE_ID)
        {
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteId").value=HD_RE_ID; 
           }
           else
           {
                document.getElementById("hdDeleteId").value="";
                return false;
           }
        }
        
        function EditFunction(LCode,HD_RE_ID,strStatus)
        {     
          var type="HDUP_CallLog.aspx?Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus + "&Popup=T";               
          window.open(type,"aaHelpDesksManageCallLogPopup","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
          return false;
        }
 	
	function PopupLTR()
	{
	 var LCode=document.getElementById("hdEnPageLCode").value;
     var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
    type ="HDSR_CallLog.aspx?Popup=T&strStatus="+ document.getElementById("hdEnFunctional").value+"&LCodeMuk="+ LCode + "&HD_RE_IDMuk=" + HD_RE_ID  ;
   	 window.open(type,"HelpDesksSearchCallLog","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	 return false;
         
	}
			
    </script>
   
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSave">
     <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width:80%">
                                            <span class="menu">Birdres HelpDesk-></span><span class="sub_menu">Linked LTR</span>
                                        </td>
                                        <td class="right" style="width:20%">
                                            <img alt="Back"  src="../Images/back.gif" onclick="javascript:history.back();" style="cursor:pointer" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width:100%">
                                            Manage Linked LTR</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="redborder top" >
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP"   >
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width:100%;">
                                                  
                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="width:90%" >
                                                                    <asp:Panel ID="pnlCall" runat="server" Width="100%">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td colspan="5">
                                                                              <asp:GridView  ID="gvCallLog" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" AllowSorting="True">
                                                                                <Columns>
                                                                                    
                                                                                    <asp:BoundField HeaderText="LTR No" DataField="HD_RE_ID" SortExpression="HD_RE_ID" />
                                                                                    <asp:BoundField HeaderText="OfficeID" DataField="OfficeID" SortExpression="OfficeID" />
                                                                                    <asp:BoundField HeaderText="Agency Name" DataField="AgencyName" SortExpression="AgencyName"/>
                                                                                    <asp:BoundField HeaderText="Opened Date" DataField="HD_RE_OPEN_DATE" SortExpression="HD_RE_OPEN_DATE"/>
                                                                                    <asp:BoundField HeaderText="Logged By" DataField="LoggedBy" SortExpression="LoggedBy"/>
                                                                                    <asp:BoundField HeaderText="Group" DataField="HD_QUERY_GROUP_NAME" SortExpression="HD_QUERY_GROUP_NAME"/>
                                                                                    <asp:BoundField HeaderText="Status" DataField="HD_STATUS_NAME" SortExpression="HD_STATUS_NAME"/>
                                                                                    <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                            
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                               <asp:HiddenField ID="hdHD_RE_ID" runat="server" Value='<%#Eval("HD_RE_ID")%>' />   
                                                                 <asp:HiddenField ID="hdLCode" runat="server" Value='<%#Eval("LCODE")%>' />   
                                                             </ItemTemplate>
                                                           </asp:TemplateField>                                                  
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />                                                    
                                                 </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                 
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 136px">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        <input id="hdPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdQueryString" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdCHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdInputXml" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPopupStatus" runat="server" style="width: 1px" type="hidden" />  
                                                                        <input id="hdEHD_RE_ID" runat="server" style="width: 1px" type="hidden" />  
                                                                     
                                                                                                                                              
                                                                        <input id="hdOfficeID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdHD_RE_OPEN_DATE" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdLoggedBy" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdHD_QUERY_GROUP_NAME" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdHD_STATUS_NAME" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdError" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdCHD_RE_IDList" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdCHD_RE_IDListPopupPage" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdAdd" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdSaveStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdButton" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdTabType" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdFeedBackId" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdAOffice" runat="server" style="width: 1px" type="hidden" />
                                                                       
                                                                       <input id="hdEnPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdEnPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdEnFunctional" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdEnFeedBackId" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdEnAOffice" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdEnPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        
                                                                        <asp:Button ID="Button1" runat="server" TabIndex="30" CssClass="displayNone" Text="Temp" Width="1px"  />
                                                                        
                                                                        </td>
                                                                </tr>
                                                                    </table>
                                                                       
                                                                       </asp:Panel>
                                                                   
                                                                       
                                                                    </td>
                                                                    <td class="center top " colspan="2" rowspan="1">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin" Text="Save" Width="100px" OnClientClick="return LinkedLTRPage()" AccessKey="s"  /><br/>
                                                                        <asp:Button ID="btnAdd" runat="server" TabIndex="3" CssClass="button topMargin" Text="Add LTR" Width="100px" OnClientClick="return PopupLTR()" />
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="center" colspan="2" rowspan="1">
                                                                    </td>
                                                                </tr>
                                                             
                                                              
                                                                
                                                                
                                                                <tr>
                                                                    <td  class="ErrorMsg" style="width:10%">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>                     &nbsp; &nbsp;                     </td>
                                                                    <td>
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
                    
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
