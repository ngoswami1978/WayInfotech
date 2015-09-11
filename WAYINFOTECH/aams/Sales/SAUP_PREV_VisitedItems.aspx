<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SAUP_PREV_VisitedItems.aspx.vb"
    Inherits="Sales_SAUP_PREV_VisitedItems" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS::Sales::Previous Visit Details</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Sales.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" src="../JavaScript/subModal.js"></script>

    <link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
    <link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />

    <script language="javascript" type="text/javascript">
   function EditServiceCallFolowupRem(DSR_SC_DETAIL_ID,DSRVisitedId,STATUSID)
  {
    
               var DSR_VISIT_ID=DSRVisitedId;//'document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;   	           	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID   + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE  + "&SCDETAILID=" + DSR_SC_DETAIL_ID + "&STATUSID=" + STATUSID;
               
              // alert(parameter);
               type = "SASR_SCFollowupRem.aspx?" + parameter;
               window.open(type,"SASR_SCFollowupRem","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false; 
  }
  
  function ShowSCMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
  
   function OpenPrevRemarks(DSR_VISIT_ID)
         {
                           	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=1";
               type = "SASR_Previous_Remarks.aspx?" + parameter;
               window.open(type,"SASR_Previous_Remarks","height=600,width=920,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;  
         }
          // Closing Form
         function fnCloseForm()
         {
            window.close();
            return false;
         } 
         function ValidateServiceStatus (strStatusID)
         {
            var strId = document.getElementById(strStatusID).value;
            //alert(strId);
            var itemIndex=document.getElementById("ddlServiceStatus").selectedIndex;
	        var strServiceStatusName;
	        strServiceStatusName=  document.getElementById("ddlServiceStatus").options[itemIndex].text;
            var id = strId.split("|")[1];
            //alert(id);
            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            
            if (id == "1")
            {                
                document.getElementById("txtCloserDate").value = day + "/" + month  + "/" + year;
                document.getElementById("hdCloserDate").value = day + "/" + month  + "/" + year;
            }
            else if(id=="0")
            {
                document.getElementById("txtCloserDate").value = "";
            }
            if ( strServiceStatusName == "Closed Non-ReSolution" )
            {                
                document.getElementById("txtCloserDate").value = day + "/" + month  + "/" + year;
                document.getElementById("hdCloserDate").value = day + "/" + month  + "/" + year;
            }            
         }
         function ValidatePrevitemsItems()
         {
            if(document.getElementById('txtCompetitionInfo').value == '')
             {
                document.getElementById('lblError').innerText = "Comp Info / Mkt info remarks is mandatory.";
                document.getElementById('txtCompetitionInfo').focus();
                return false;  
             }
             if(document.getElementById('txtFollowUpRemarks').value == '')
             {
                document.getElementById('lblError').innerText = "Follow-up remarks is mandatory.";
                document.getElementById('txtFollowUpRemarks').focus();
                return false;  
             }
                 CancelEditOpenItemsServiceCall();
         }   
         
         function ShowCalender(varControlID,varButtonID)
         {
       //  alert(varControlID +' '+varButtonID)
                  Calendar.setup({
                        inputField     :    varControlID,
                        ifFormat       :    "%d/%m/%Y",
                        button         :    varButtonID,
                        //align          :    "Tl",
                        singleClick    :    true
                       
                        });                            
         
         }     
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <div>
            <table width="860px" class="border_rightred left">
                <tr>
                    <td class="top">
                        <table width="100%">
                            <tr>
                                <td class="top left" style="width: 80%">
                                    <span class="menu">Sales-> DSR -> </span><span class="sub_menu">Previous visit items
                                        / Previous visit open items</span>
                                </td>
                                <td class="right" style="width: 20%">
                                    <asp:LinkButton ID="lnkClose" CssClass="LinkButtons" runat="server" OnClientClick="return fnCloseForm()">Close</asp:LinkButton>
                                    &nbsp; &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="heading center" colspan="2" style="width: 100%">
                                    Previous visit items / Previous visit open items</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="redborder top" style="width: 100%">
                                                <asp:UpdatePanel ID="updPnlVisitDetails" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlServiceCall" runat="server">
                                                            <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                                <tr>
                                                                    <td class="center TOP">
                                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                        <input type="hidden" runat="server" id="hdID" style="width: 1px" />
                                                                        <input type="hidden" runat="server" id="hdLCode" style="width: 1px" />
                                                                        <input type="hidden" runat="server" id="hdVisitDATE" style="width: 1px" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="top">
                                                                    <td>
                                                                        <asp:RadioButton ID="rbPrevVisitItems" runat="server" Text="Previous visit items"
                                                                            GroupName="r1" AutoPostBack="true" onclick="javascript:CancelEditOpenItemsServiceCall();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:RadioButton ID="rbPrevVisitOpenItems" runat="server" Text="Previous visit open items"
                                                                            GroupName="r1" AutoPostBack="true" onclick="javascript:CancelEditOpenItemsServiceCall();" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp; &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="top" style="width: 100%;">
                                                                        <asp:Panel ID="pnlServiceCallDetails" runat="server">
                                                                            <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                                                <tr>
                                                                                    <td style="width: 15%;">
                                                                                    </td>
                                                                                    <td style="width: 25%;">
                                                                                    </td>
                                                                                    <td style="width: 20%;">
                                                                                    </td>
                                                                                    <td style="width: 28%;">
                                                                                    </td>
                                                                                    <td style="width: 12%;">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="top">
                                                                                    <td>
                                                                                        Department
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="textboxgrey" Width="84%"
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        Deptt Specific
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtDepttSpecific" runat="server" CssClass="textboxgrey" Width="84%"
                                                                                            ReadOnly="True"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="center">
                                                                                        <asp:Button ID="btnSave" Text="Save" runat="Server" CssClass="button" Width="100px"
                                                                                            OnClientClick="javascript:return ValidateOpenItemsServiceCall();" /></td>
                                                                                </tr>
                                                                                <tr class="top">
                                                                                    <td style="vertical-align: top;">
                                                                                        Detailed Disc /<br />
                                                                                        Issue Reported
                                                                                    </td>
                                                                                    <td style="width: 80%" colspan="3">
                                                                                        <asp:TextBox ID="txtDetailedDiscussion" runat="server" CssClass="textboxgrey" Width="94%"
                                                                                            TextMode="MultiLine" Rows="4" Height="60px" ReadOnly="True"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="center" style="vertical-align: top;">
                                                                                        <asp:Button ID="btnReset" Text="Reset" runat="server" CssClass="button" Width="100px"
                                                                                            OnClientClick="javascript:return CancelEditOpenItemsServiceCall();" OnClick="btnReset_Click" /></br><br /><input id="BtnSCMarketInfo" runat="server" class="button" onclick="javascript:ShowSCMarketInfo('SC');"
                                                                                            style="width: 100px" tabindex="5" type="button" value="Comp/Mkt info " /></td>
                                                                                </tr>
                                                                                <tr class="top">
                                                                                    <td>
                                                                                        Status
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlServiceStatus" runat="server" CssClass="dropdownlist" Width="98%">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        Assigned to
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtAssignedTo" runat="server" CssClass="textboxgrey" Width="84%"
                                                                                            ReadOnly="True"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Closer Date
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtCloserDate" runat="server" CssClass="textboxgrey" Width="76%"
                                                                                            ReadOnly="True"></asp:TextBox>
                                                                                        <input type="hidden" runat="server" id="hdCloserDate" style="width: 1px" value="5" />
                                                                                    </td>
                                                                                    <td>
                                                                                        Target Closer Date
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtTargetCloserDate" runat="server" CssClass="textboxgrey" Width="84%"
                                                                                            ReadOnly="True"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="center" style="vertical-align: top;">
                                                                                       </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 80%" colspan="3">
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                        <%--<asp:Button ID="btnAddServiceCall" Text="Update" runat="server" CssClass="button"
                                                                                            Width="75px" OnClientClick="return ValidatePrevitemsItems();" Enabled="False" Visible="false" /><br />
                                                                                        <asp:Button ID="btnCancelServiceCall" Text="Cancel" runat="Server" CssClass="button"
                                                                                            Width="75px" OnClientClick="javascript:return CancelEditOpenItemsServiceCall();"
                                                                                            Enabled="False" Visible="false"/>
                                                                                            --%>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 71%" colspan="3">
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" style="width: 100%;">
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" style="width: 100%;">
                                                                                        <asp:GridView ID="gvVisitItemsServiceCall" runat="server" AutoGenerateColumns="False"
                                                                                            TabIndex="6" Width="100%">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="" HeaderText="S.No" HeaderStyle-Width="3%" />
                                                                                                <asp:BoundField DataField="LOGDATE" HeaderText="Logged Date" HeaderStyle-Width="6%" />
                                                                                                <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Deptt" HeaderStyle-Width="10%" />
                                                                                                <asp:BoundField DataField="DEPARTMENT_SPECIFIC" HeaderText="Deptt Specific" HeaderStyle-Width="15%" />
                                                                                                <asp:TemplateField HeaderText="Detailed Disc /Issue Reported" ItemStyle-Width="25%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtSCIssueReported" runat="server" CssClass="textbox" Height="30px"
                                                                                                            BorderStyle="none" TextMode="MultiLine" Wrap="true" Width="100%" ReadOnly="True"
                                                                                                            BorderColor="white" BorderWidth="0px" Text='<%#Eval("SC_DISCUSSIONISSUE_REMARKS") %>'> </asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="SC_STATUSID_NAME" HeaderText="Status" HeaderStyle-Width="6%" />
                                                                                                <asp:BoundField DataField="ASSIGNTO_EMPLOYEE_NAME" HeaderText="Assigned to" HeaderStyle-Width="15%" />
                                                                                                <asp:BoundField DataField="CLOSER_DATETIME" HeaderText="Closer Date" HeaderStyle-Width="10%" />
                                                                                                <asp:BoundField DataField="TARGET_CLOSER_DATETIME" HeaderText="Target Closer Date"
                                                                                                    HeaderStyle-Width="10%" />
                                                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="8%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:HiddenField ID ="HdDSR_VISIT_ID" runat ="server" Value ='<%# Eval("DSR_VISIT_ID") %>' />
                                                                                                         <asp:HiddenField ID ="HdSC_STATUSID" runat ="server" Value ='<%# Eval("SC_STATUSID") %>' />
                                                                                                        
                                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                            OnClientClick="javascript:return CancelEditOpenItemsServiceCall();" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_SC_DETAIL_ID")  %>'></asp:LinkButton>&nbsp;<asp:LinkButton
                                                                                                                ID="LnkSCFRem" runat="server" CommandName="FupRemX" Text="FollowupRem" CssClass="LinkButtons"
                                                                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_SC_DETAIL_ID")  %>'></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <AlternatingRowStyle CssClass="lightblue" Wrap="true" />
                                                                                            <RowStyle CssClass="textbold" Wrap="true" />
                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" Wrap="true" />
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" style="width: 100%;">
                                                                                        <input type="hidden" id="hdDSR_SC_DETAIL_ID" runat="server" />
                                                                                        <input type="hidden" id="hdDSR_DETAIL_ID" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlRemarks" runat="server">
                                                                            <asp:GridView ID="gvRemarks" AllowSorting="true" HeaderStyle-ForeColor="white" runat="server"
                                                                                BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False" Width="95%"
                                                                                TabIndex="9">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="DATE" HeaderText="Date" HeaderStyle-HorizontalAlign="left"
                                                                                        ItemStyle-Width="15%" />
                                                                                    <asp:BoundField DataField="CHANGE_DATA" HeaderText="Remarks" HeaderStyle-HorizontalAlign="left"
                                                                                        ItemStyle-Wrap="true" Visible="false" />
                                                                                    <asp:BoundField DataField="COMPETITION" HeaderText="Comp Info/Mkt info Remarks "
                                                                                        HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="true" />
                                                                                    <asp:BoundField DataField="DISCUSSIONISSUE" HeaderText="Follow-up Remarks" HeaderStyle-HorizontalAlign="left"
                                                                                        ItemStyle-Wrap="true" />
                                                                                </Columns>
                                                                                <HeaderStyle CssClass="Gridheading" />
                                                                                <RowStyle CssClass="ItemColor" HorizontalAlign="Left" />
                                                                                <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="Left" />
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <ajax:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlProgressOpenItemsServiceCall"
                                                                            TargetControlID="pnlProgressOpenItemsServiceCall" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                            ID="modOpenIemsServiceCall" runat="server">
                                                                        </ajax:ModalPopupExtender>
                                                                        <asp:Panel ID="pnlProgressOpenItemsServiceCall" runat="server" CssClass="overPanel_Test"
                                                                            Height="0px" Width="150px" BackColor="white">
                                                                            <table style="width: 150px; height: 150px;">
                                                                                <tr>
                                                                                    <td valign="middle" align="center">
                                                                                        <img src="../Images/er.gif" id="img1" runat="server" alt="" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
