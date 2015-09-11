<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TCUP_CallLog.aspx.vb" Inherits="ETHelpDesk_TCUP_CallLog"
    EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::HelpDesk::Manage Call Log</title>
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script src="../JavaScript/ETracker.js" type="text/javascript"></script>

    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" src="../JavaScript/subModal.js"></script>

    <link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
    <link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />
    
    <script language="javascript" type="text/javascript">
function CallLogDescription(fr,hd_re_id,rowid)
    {
    //alert(fr);
    //alert(hd_re_id);
    //alert(rowid);    
    var type;    
    type ="../ECOMMTrackerHelpDeskPopup/PUUP_HDDescSolution.aspx?fr="+ fr +"&hd_re_id="+hd_re_id + "&ID="+rowid;	
    window.open(type,"aa","height=870,width=900,top=30,left=20,scrollbars=1,status=1");	
    
    }
    
    </script>
    <style type ="text/css" >
 

.searchBox 
{
	background-image:url('../Images/loading.gif');   
	background-repeat:no-repeat;
	background-position:right;

}
.textbox_search
{
	font-size:11px;
	font-family:verdana, Arial;
	height:16px;
	width:149px;	
	
}	
	
.textbox_search_list
{
	font-size:11px;
	font-family:verdana, Arial;
	padding-left:5px;
	margin-left:10px;
	border:2px;
	line-height:20px;
	overflow-x:hidden; 
	overflow-y:scroll;
	height:200px;
	
	
	
}
	

</style>
</head>
<body onload="HideShowTechnical()">
    <form id="form1" runat="server" defaultfocus="rdTechanical" defaultbutton="btnSave">
       <asp:ScriptManager id="ScriptManager1" runat="server" AsyncPostBackTimeout ="660">
       </asp:ScriptManager>
        <table width="860px" class="border_rightred left">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width: 80%">
                                            <span class="menu">ETrackers Technical-></span><span class="sub_menu">Call Log</span>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            <img alt="Back" src="../Images/back.gif" onclick="javascript:history.back();" style="cursor: pointer" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width: 100%">
                                            Manage Call Log</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px; width: 80%">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            <asp:LinkButton ID="lnkClose" CssClass="LinkButtons" runat="server" OnClientClick="return fnCallLogID()">Close</asp:LinkButton>
                                            &nbsp; &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="redborder top" style="width: 100%" colspan="2">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%">
                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 90%">
                                                                    <asp:Panel ID="pnlCall" runat="server" Width="100%">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="gap" colspan="4">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="subheading" colspan="4">
                                                                                    Agency Details</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Query Group</td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="rdFunctional" runat="server" CssClass="textbold" Text="Functional"
                                                                                        GroupName="r1" Width="171px" TabIndex="2" AutoPostBack="True" /></td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="rdTechanical" runat="server" CssClass="textbold" Text="Technical"
                                                                                        GroupName="r1" Width="171px" Checked="True" TabIndex="2" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Office Id</td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" Width="170px" ReadOnly="false"
                                                                                        TabIndex="2" MaxLength="20"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="hdCallAgencyName" style="width: 1px" type="hidden" runat="server" />
                                                                                    <input id="hdAOffice" style="width: 1px" type="hidden" runat="server" />
                                                                                    <input id="hdEnAOffice" style="width: 1px" type="hidden" runat="server" />
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Agency Name <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey TextTitleCase"
                                                                                        Width="532px" ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                                    <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="ET_PopupPage(1)"
                                                                                        src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Address</td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Width="532px"
                                                                                        ReadOnly="True" Height="50px" Rows="5" TextMode="MultiLine" TabIndex="20"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Country</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" Width="170px"
                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                                <td class="textbold">
                                                                                    City</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True"
                                                                                        TabIndex="20"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold" style="width: 17%">
                                                                                    Phone</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True"
                                                                                        TabIndex="20"></asp:TextBox></td>
                                                                                <td class="textbold" style="width: 17%">
                                                                                    Fax</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True"
                                                                                        TabIndex="20"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Online Status</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" Width="170px"
                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                                <td class="textbold">
                                                                                    Email</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Pincode</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPincode" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                                <td> Company Vertical
                                                                                </td>
                                                                                <td><asp:TextBox ID="TxtCompVertical" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="gap">
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
                                                                                <td class="subheading" colspan="4">
                                                                                    Call Details</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    LTR No</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtLTRNo" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True"
                                                                                        TabIndex="20"></asp:TextBox></td>
                                                                                <td class="textbold">
                                                                                    Caller Name<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCallerName" runat="server" Width="168px" MaxLength="50" TabIndex="2"
                                                                                        CssClass="TextTitleCase" onkeypress="allTextWithSpace();"></asp:TextBox>
                                                                                    <img id="Img1" runat="server" alt="Select & Add Caller Name" onclick="ET_PopupPage(2)"
                                                                                        src="../Images/lookup.gif" tabindex="2" style="cursor: pointer" />
                                                                                    <input id="hdCallCallerName" runat="server" style="width: 1px" type="hidden" />
                                                                                    <div id="AutoCompleteTo" />
                                                                                   <cc1:AutoCompleteExtender ID="AutoCompleteExtenderForCaller" runat="server" TargetControlID="txtCallerName"
                                                                                   EnableCaching="false" MinimumPrefixLength="1"  ContextKey =" | " UseContextKey="true"  BehaviorID ="BhAutoCompleteExtenderForCaller"
                                                                                   ServiceMethod="GetCallerNameForTechnical" CompletionSetCount="10"  CompletionListElementID="AutoCompleteTo" CompletionInterval="100" 
                                                                                   OnClientPopulating="loadingFromCaller"
                                                                                   OnClientPopulated="loadedFromCaller">
                                                                                   </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Query Sub Group <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQuerySubGroup" runat="server" TabIndex="2"
                                                                                        Width="174px" CssClass="textbold">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">
                                                                                    Query Category <span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQueryCategory" runat="server" TabIndex="2"
                                                                                        Width="174px" CssClass="textbold">
                                                                                        <asp:ListItem Selected="True">--Select One--</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <input id="hdCategory" runat="server" style="width: 1px" type="hidden" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="hdSubCategory" runat="server" style="width: 1px" type="hidden" />
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Query Sub Category <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQuerySubCategory" runat="server"
                                                                                        TabIndex="2" Width="174px" CssClass="textbold">
                                                                                        <asp:ListItem Selected="True">--Select One--</asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">
                                                                                    Call Duration(HH:MM)</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCallDuration" runat="server" Width="76px" MaxLength="5" TabIndex="2"></asp:TextBox>
                                                                                    :
                                                                                    <asp:TextBox ID="txtCallDuration1" runat="server" Width="76px" MaxLength="2" TabIndex="2"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Priority &nbsp;<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlPriority" runat="server" TabIndex="2"
                                                                                        Width="174px" CssClass="textbold">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">
                                                                                    Query Status <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQueryStatus" runat="server" TabIndex="2"
                                                                                        Width="174px" CssClass="textbold">
                                                                                    </asp:DropDownList>
                                                                                    <input id="hdQueryStatus" runat="server" style="width: 1px" type="hidden" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Assigned To <span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlTeamAssignedTo" runat="server"
                                                                                        TabIndex="2" Width="174px" CssClass="textbold">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">
                                                                                    Coordinator1
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCoordinator1" runat="server" TabIndex="2"
                                                                                        Width="174px" CssClass="textbold">
                                                                                    </asp:DropDownList>
                                                                                    <input id="hdCoordinator1" runat="server" style="width: 1px" type="hidden" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Assigned Date Time</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDateAssigned" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                        ReadOnly="True" Width="170px" TabIndex="20"></asp:TextBox></td>
                                                                                <td class="textbold">
                                                                                    Left Date Time</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtLeftDateTime" runat="server" Width="168px" CssClass="textboxgrey"
                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                                    <img id="imgLeftDateTime" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                                        style="cursor: pointer" />

                                                                                    <script type="text/javascript">
                                                                           
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtLeftDateTime.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                  button         :    "imgLeftDateTime",
                                                 //align          :    "Tl",
                                                  singleClick    :    true,
                                                  showsTime      : true
                                                               });
                                                                                    </script>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    TT Number</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtTTNo" runat="server" CssClass="textbox" Width="170px" MaxLength="50"
                                                                                        TabIndex="2"></asp:TextBox>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Contact Type<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlContactType" runat="server" TabIndex="2"
                                                                                        Width="174px" CssClass="textbold">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Close Date Time</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCloseDateTime" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                        ReadOnly="True" Width="170px" TabIndex="20"></asp:TextBox>&nbsp;
                                                                                    <img id="imgCloseDateTime" runat="server" alt="" tabindex="2" src="../Images/calender.gif"
                                                                                        title="Date selector" style="cursor: pointer" visible="false" />

                                                                                    <script type="text/javascript">
                                                                           if( document.getElementById("imgCloseDateTime")!=null)
                                                                           {
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtCloseDateTime.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                  button         :    "imgCloseDateTime",
                                                 //align          :    "Tl",
                                                  singleClick    :    true,
                                                  showsTime      : true
                                                               });}
                                                                                    </script>

                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Logged By
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textboxgrey" Width="170px"
                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Logged Date Time</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtLoggedDate" runat="server" CssClass="textboxgrey" Width="170px"
                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                                <td>
                                                                                    Airline Name</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpAirLineName" TabIndex="2" onkeyup="gotop(this.id)" runat="server"
                                                                                        CssClass="textbold" Width="174px">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                             <tr>
                                                                <td class="ErrorMsg" style="width: 100%" colspan="5">
                                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr class="paddingtop paddingbottom">
                                                                                <td style="width: 30%" class="left">
                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                        ID="txtTotalRecordCount" runat="server" Width="75px" CssClass="textboxgrey"
                                                                                        ReadOnly="True"></asp:TextBox></td>
                                                                                <td style="width: 25%" class="right">
                                                                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"
                                                                                        OnClientClick="return ManageTCallLogPageForNextAndPrev()"><< Prev</asp:LinkButton></td>
                                                                                <td style="width: 20%" class="center">
                                                                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList 
                                                                                               ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True"
                                                                                        >
                                                                                    </asp:DropDownList></td>
                                                                                <td style="width: 25%" class="left">
                                                                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next"
                                                                                        OnClientClick="return ManageTCallLogPageForNextAndPrev()">Next >></asp:LinkButton></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                                            
                                                                            
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;<input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdQueryString" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdFeedBackPresence" runat="server" style="width: 1px" type="hidden" value="1" />
                                                                                    <input id="hdFeedBackId" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdSaveRights" runat="server" style="width: 1px" type="hidden" value="0" />
                                                                                    <input id="hdMsg" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdReSave" runat="server" style="width: 1px" type="hidden" value="0" />
                                                                                    <input id="hdQueryCategory" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdQuerySubCategory" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdSubCategoryMandatory" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdEnPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdEnPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdEnFeedBackId" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdEnTechnical" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdEnCallAgencyName_LCODE" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdLimited_To_Aoffice" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdLimited_To_Region" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdLimited_To_OwnAgency" runat="server" style="width: 1px" type="hidden" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlDesc" runat="server" Width="100%" CssClass="displayNone">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="gap" colspan="4">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold" colspan="4">
                                                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" Height="170px"
                                                                                        Rows="5" TextMode="MultiLine" Width="622px" TabIndex="2"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                              <td>
                                                                                </td>
                                                                                <td width="100px">Attach Desc. file
                                                                                </td>
                                                                                <td colspan="3">
                                                                                    <asp:FileUpload ID="FileUploadDesc" runat="server" Width="400px" />
                                                                                    <asp:Button ID="btnDUpload" runat="server" CssClass="button topMargin" TabIndex="3"
                                                                                        Text="Upload" Width="100px" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="gap" colspan="4">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td colspan="4">
                                                                                       <asp:GridView ID="gvDescription" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <asp:BoundField HeaderText="User" DataField="EmployeeName" HeaderStyle-Width="10%" />
                                                                                            <asp:BoundField HeaderText="DateTime" DataField="DATETIME" HeaderStyle-Width="10%" />
                                                                                            <asp:TemplateField HeaderStyle-Width="60%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="textbox" Height="150px" BorderStyle="none"
                                                                                                        TextMode="MultiLine" Wrap="true" Width="100%" ReadOnly="True" BorderColor="white"
                                                                                                        BorderWidth="0px" Text='<%#Eval("ACTION_TAKEN") %>' Style="cursor: pointer"> </asp:TextBox>
                                                                                                    <asp:HiddenField ID="HdRowId" runat="server" Value='<%#Eval("ID") %>'></asp:HiddenField>
                                                                                                <asp:HiddenField ID="hdRowType" runat="server" Value='<%#Eval("RTYPE") %>'></asp:HiddenField>
                                                                                                   <asp:HiddenField ID="hdRowNo" runat="server" Value='<%#Eval("ROWNUMBER") %>'></asp:HiddenField>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                              <asp:TemplateField HeaderText="File" HeaderStyle-Width="10%" ItemStyle-CssClass="breakLongLine">
                                                                                                <ItemTemplate >
                                                                                                    <asp:LinkButton ID="lnkView" runat="server" CommandName="ViewX" Text='<% #DataBinder.Eval(Container.DataItem, "Attach") %>' CssClass="LinkButtons"
                                                                                                        CommandArgument='<% # DataBinder.Eval(Container.DataItem, "Attach")  %>'></asp:LinkButton>
                                                                                                
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            
                                                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                   <%-- <asp:LinkButton ID="lnkView" runat="server" CommandName="ViewX" Text="View" CssClass="LinkButtons"
                                                                                                        CommandArgument='<% #DataBinder.Eval(Container.DataItem, "Attach") %>'></asp:LinkButton>--%>
                                                                                                        <asp:LinkButton ID="LnkDelete" runat="server" CommandName="DelX" Text="Delete" CssClass="LinkButtons"
                                                                                                        CommandArgument='<% #DataBinder.Eval(Container.DataItem, "ROWNUMBER") + "|" + DataBinder.Eval(Container.DataItem, "Attach")  %>'></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue center" />
                                                                                        <RowStyle CssClass="textbold center" />
                                                                                        <HeaderStyle CssClass="Gridheading center" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlSol" runat="server" Width="100%" CssClass="displayNone">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="gap" colspan="4">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold" colspan="4">
                                                                                    <asp:TextBox ID="txtSolution" runat="server" CssClass="textbox" Height="170px" Rows="5"
                                                                                        TextMode="MultiLine" Width="622px" TabIndex="2"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                               <td>
                                                                                </td>
                                                                                <td>Attach solution file
                                                                                </td>
                                                                                <td colspan="3"><asp:FileUpload ID="FileUploadSol" runat="server" Width="400px" />
                                                                                    <asp:Button ID="btnSUpload" runat="server" CssClass="button topMargin" TabIndex="3" OnClientClick="return SetActivePage();"
                                                                                        Text="Upload" Width="100px" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="gap" colspan="4">
                                                                                    <input style="width: 1px" id="hdSol" type="hidden" runat="server" value="1" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td colspan="4">
                                                                                       <asp:GridView ID="gvSolution" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <asp:BoundField HeaderText="User" DataField="EmployeeName" HeaderStyle-Width="10%" />
                                                                                            <asp:BoundField HeaderText="DateTime" DataField="DATETIME" HeaderStyle-Width="10%" />
                                                                                            <asp:TemplateField HeaderStyle-Width="60%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtSoln" runat="server" CssClass="textbox" Height="150px" BorderStyle="none"
                                                                                                        TextMode="MultiLine" Wrap="true" Width="100%" ReadOnly="True" BorderColor="white"
                                                                                                        BorderWidth="0px" Text='<%#Eval("ACTION_TAKEN") %>' Style="cursor: pointer"></asp:TextBox>
                                                                                                    <asp:HiddenField ID="HdSLRowId" runat="server" Value='<%#Eval("ID") %>'></asp:HiddenField>
                                                                                                     <asp:HiddenField ID="hdRowType" runat="server" Value='<%#Eval("RTYPE") %>'></asp:HiddenField>
                                                                                                   <asp:HiddenField ID="hdRowNo" runat="server" Value='<%#Eval("ROWNUMBER") %>'></asp:HiddenField>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                          
                                                                                            <asp:TemplateField HeaderText="File" HeaderStyle-Width="10%" ItemStyle-CssClass="breakLongLine">
                                                                                                <ItemTemplate >
                                                                                                    <asp:LinkButton ID="lnkView" runat="server" CommandName="ViewX" Text='<% #DataBinder.Eval(Container.DataItem, "Attach") %>' CssClass="LinkButtons"
                                                                                                        CommandArgument='<% # DataBinder.Eval(Container.DataItem, "Attach")  %>'></asp:LinkButton>
                                                                                                
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            
                                                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                   
                                                                                                        <asp:LinkButton ID="LnkDelete" runat="server" CommandName="DelX" Text="Delete" CssClass="LinkButtons"
                                                                                                         CommandArgument='<% #DataBinder.Eval(Container.DataItem, "ROWNUMBER") + "|" + DataBinder.Eval(Container.DataItem, "Attach")  %>'></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue center" />
                                                                                        <RowStyle CssClass="textbold center" />
                                                                                        <HeaderStyle CssClass="Gridheading center" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td class="top">
                                                                    <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Save" Width="100px" OnClientClick="return ManageTCallLogPage()" AccessKey="s" /><br />
                                                                    <asp:Button ID="btnNew" runat="server" TabIndex="3" CssClass="button topMargin" Text="New"
                                                                        Width="100px" AccessKey="n" /><br />
                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Reset" Width="100px" AccessKey="r" /><br />
                                                                   
                                                                    <asp:Button ID="btnHistory" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="History" Width="100px" OnClientClick="return ET_PopupPage(4)" /><br />
                                                                    <asp:Button ID="btnAssigneeHistory" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Assignee History" Width="100px" OnClientClick="return ET_PopupPage(5)" /><br />
                                                                    <asp:Button ID="btnFeedBack" runat="server" CssClass="button topMargin" OnClientClick="return ET_PopupPage(6)"
                                                                        TabIndex="3" Text="FeedBack" Width="100px" /><br />
                                                                   
                                                                    <asp:Button ID="btnSaveAll" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                       Visible="false"    Text="Save All" Width="100px" OnClientClick="return ManageTCallLogPage()" />
                                                                </td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td class="ErrorMsg" style="width: 10%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ErrorMsg" style="width: 90%">
                                                                    Field Marked * are Mandatory</td>
                                                                <td>
                                                                    &nbsp; &nbsp;
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
                    <asp:HiddenField ID="hdTabType" runat="server" />
                    <asp:HiddenField ID="hdDurationDate" runat="server" />
                    <asp:HiddenField ID="hdstartTimeSpan" runat="server" />
                    <asp:HiddenField ID="hdCallPreviousState" runat="server" />
                    <asp:HiddenField ID="hdCallSatrtDateTimeInitialy" runat="server" />
                    <asp:HiddenField ID="hdCallCloseDateTime" runat="server" />
                    <asp:HiddenField ID="hdMultiLTR" runat="server" />
                     <asp:HiddenField ID="hdLoggedDatetime" runat="server" />
                       <asp:HiddenField ID="hdPageNo" runat="server" Value="1"  />
                       <asp:Button ID="BtnChangeContext" runat ="server" style="display:none;" OnClientClick ="return ChangeContextKeyForTech();" />
                       <asp:HiddenField ID="HdTechDefaultTeamAssignedTo" runat="server" Value=""  />
                    <asp:HiddenField ID="HdTechDefaultContactType" runat="server" Value=""  />
                     <asp:HiddenField ID="HdTechManForDecAndSoln" runat="server" Value=""  />
                      <asp:HiddenField ID="HdTechManForDecAndSolnForNav" runat="server" Value=""  />
                     <asp:HiddenField ID="hdMULTIHD_RE_ID" runat="server" Value=""  />
                       <asp:HiddenField ID="HdTechnicalLOgDateTime" runat="server" Value=""  />
                    
                </td>
            </tr>
        </table>
    </form>
</body>

<script language="javascript" type="text/javascript">

if( document.getElementById("hdMsg").value !="")

{

    if (confirm(document.getElementById("hdMsg").value )==true)

    {

        if( document.getElementById("hdReSave").value !="1")

        {

            document.getElementById("hdReSave").value="1";

            document.getElementById("hdMsg").value ="";

            document.forms['form1'].submit();

        }

    }

    else

    {
               // fillCategoryTechnical()
                
               //document.getElementById("ddlQuerySubGroup").selectedIndex=0;
               document.getElementById("hdReSave").value="0";

    }

}

function ManageTCallLogPagePageno()
{
 document.getElementById("HdTechManForDecAndSolnForNav").value="N";
if (ManageTCallLogPage()==false)
{
   document.getElementById("HdTechManForDecAndSolnForNav").value="";
   document.getElementById("ddlPageNumber").value= document.getElementById("hdPageNo").value;
  
  return false;
 
}
else
{
   document.getElementById("HdTechManForDecAndSolnForNav").value="";
return true;
}

}
function SetActivePage()
{
    document.getElementById("hdTabType").value="2"
    return true;
}


   function ChangeContextKeyForTech()
         {
              var s = $find("BhAutoCompleteExtenderForCaller");  
              var Lcode;
              Lcode=document.getElementById ('hdCallAgencyName').value.split("|")[0];
              s._contextKey=Lcode + "|" + document.getElementById ('txtOfficeId').value;   
              return false;
             // alert( s._contextKey);       
         }
 
      
  function loadingFromCaller() 
{

   document.getElementById('<%=txtCallerName.ClientId %>').className = "searchBox"; 
     
  
}

function loadedFromCaller() 
{
    document.getElementById('<%=txtCallerName.ClientId %>').className = "textbox_search";
    document.getElementById('AutoCompleteTo').className = "textbox_search_list"; 
  }

function ManageTCallLogPageForNextAndPrev()
 {
     document.getElementById("HdTechManForDecAndSolnForNav").value="N";
    if (ManageTCallLogPage()==false)
    {
        document.getElementById("HdTechManForDecAndSolnForNav").value="";
       return false;
    }
    else
    {
        document.getElementById("HdTechManForDecAndSolnForNav").value="";
        return true;
    }
   }

</script>

</html>
