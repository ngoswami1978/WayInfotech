<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_CallLog.aspx.vb" Inherits="BirdresHelpDesk_HDUP_CallLog" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::HelpDesk::Manage Call Log</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" src="../JavaScript/subModal.js"></script>
<link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
<link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />

</head>
<body onload="HideShowFunctional()"  onunload="fnCallLogIDCloseFunctional();">
    <form id="form1" runat="server" defaultfocus="rdFunctional" defaultbutton="btnSave"  >
        <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width:80%">
                                            <span class="menu">Birdres HelpDesk-></span><span class="sub_menu">Call Log</span>
                                        </td>
                                         <td class="right" style="width:20%">
                                        <img alt="Back"  src="../Images/back.gif" onclick="javascript:history.back();" style="cursor:pointer" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width:100%">
                                            Manage Call Log</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px;width:80%">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />
                                                   
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width:20%">
                                        <asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnCallLogID()" >Close</asp:LinkButton> &nbsp; &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="redborder top" colspan="2" style="width:100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP"   >
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width:100%;">
                                                  
                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="width:90%" >
                                                                    <asp:Panel ID="pnlCall" runat="server" Width="100%">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 2%"></td>
                                                                            <td class="gap" colspan="4"></td>
                                                                        </tr>
                                                                  <tr>
                                                                    <td ></td>
                                                                    <td class="subheading" colspan="4">
                                                                        Agency Details</td>
                                                                </tr>
                                                                   <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" style="width: 136px">Query Group</td>
                                                                    <td><asp:RadioButton ID="rdFunctional" runat="server" CssClass="textbold" Text="Functional" GroupName="r1" Width="171px" Checked="True" TabIndex="2" /></td>
                                                                    <td ></td>
                                                                    <td><asp:RadioButton ID="rdTechanical" runat="server" CssClass="textbold" Text="Technical" GroupName="r1"     Width="171px" AutoPostBack="True" TabIndex="2" /></td>
                                                                   
                                                                </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="textbold" >Office Id</td>
                                                                            <td><asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" Width="170px" ReadOnly="false" TabIndex="2" MaxLength ="20"></asp:TextBox></td>
                                                                            <td ></td>
                                                                            <td></td>
                                                                        </tr>
                                                                      
                                                                <tr>
                                                                    <td >
                                                                        <input id="hdCallAgencyName" style="width: 2px" type="hidden" runat="server" />
                                                                        <input id="hdEnCallAgencyName_LCODE" style="width: 2px" type="hidden" runat="server" />
                                                                        <input id="hdAOffice" style="width: 2px" type="hidden" runat="server" />
                                                                        <input id="hdEnAOffice" style="width: 2px" type="hidden" runat="server" /></td>
                                                                    <td class="textbold" style="width: 136px">
                                                                        Agency Name <span class="Mandatory">*</span> 
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey TextTitleCase"  Width="532px" ReadOnly="True" TabIndex="20"  ></asp:TextBox>
                                                                        <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPageBRFunctional(1)"
                                                                            src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >Address</td>
                                                                    <td colspan="3"><asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Width="532px" ReadOnly="True" Height="50px" Rows="5" TextMode="MultiLine" TabIndex="20"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >Country</td>
                                                                    <td><asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                    <td class="textbold">City</td>
                                                                    <td><asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >Phone</td>
                                                                    <td><asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 17%">Fax</td>
                                                                    <td><asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >Online Status</td>
                                                                    <td><asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                    <td class="textbold">Email</td>
                                                                    <td><asp:TextBox ID="txtEmail" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="textbold" >Pincode</td>
                                                                            <td><asp:TextBox ID="txtPincode" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            <td ></td>
                                                                            <td></td>
                                                                        </tr>
                                                                <tr>
                                                                    <td class="gap"></td>
                                                                    <td ></td>
                                                                    <td></td>
                                                                    <td ></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="subheading" colspan="4">Call Details</td>
                                                                </tr>
                                                               
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >LTR No</td>
                                                                    <td><asp:TextBox ID="txtLTRNo" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                    <td class="textbold">Caller Name <span class="Mandatory">*</span></td>
                                                                    <td><asp:TextBox ID="txtCallerName" runat="server" Width="168px" MaxLength="50" TabIndex="2" CssClass="TextTitleCase" onkeypress="allTextWithSpace();"></asp:TextBox>
                                                                        <img id="Img1" runat="server" TabIndex="2" alt="Select & Add Caller Name" onclick="PopupPageBRFunctional(2)"
                                                                            src="../Images/lookup.gif" style="cursor: pointer" visible="false" />
                                                                        <input id="hdCallCallerName" runat="server" style="width: 1px" type="hidden" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold">Query Sub Group <span class="Mandatory">*</span> 
                                                                    </td>
                                                                    <td><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlQuerySubGroup" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Query Category <span class="Mandatory">*</span></td>
                                                                    <td><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlQueryCategory" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                        <asp:ListItem Selected="True" Value="">--Select One--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                        <input id="hdCategory" runat="server" style="width: 1px" type="hidden" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ><input id="hdSubCategory" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold" >  Query Sub Category <span class="Mandatory">*</span> 
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlQuerySubCategory" runat="server" TabIndex="2" Width="174px" CssClass="textbold" >
                                                                        <asp:ListItem Selected="True" Value="">--Select One--</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Call Duration(HH:MM)</td>
                                                                    <td><asp:TextBox ID="txtCallDuration" runat="server" Width="76px" MaxLength="2"  TabIndex="2"></asp:TextBox>
                                                                        :<asp:TextBox ID="txtCallDuration1" runat="server" Width="76px" MaxLength="2"  TabIndex="2"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >Priority &nbsp;<span class="Mandatory">*</span></td>
                                                                    <td><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPriority" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold">Query Status <span class="Mandatory">*</span> 
                                                                    </td>
                                                                    <td><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlQueryStatus" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                        </asp:DropDownList>
                                                                        <input id="hdQueryStatus" runat="server" style="width: 1px" type="hidden" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold">Assigned To<span class="Mandatory" >*</span></td>
                                                                    <td><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlTeamAssignedTo" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Assigned Date Time</td>
                                                                    <td><asp:TextBox ID="txtDateAssigned" runat="server" CssClass="textboxgrey" MaxLength="10" ReadOnly="True"
                                                                            Width="170px" TabIndex="20"></asp:TextBox>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td ><input id="hdCoordinator" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold" >Coordinator1 <span class="Mandatory">   *</span></td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlCoordinator1" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                        <asp:ListItem Selected="True" Value="">--Select One--</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <input id="hdCoordinator1" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold">Coordinator2</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlCoordinator2" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <input id="hdCoordinator2" runat="server" style="width: 1px" type="hidden" />
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td ><input style="WIDTH: 1px" id="hdPTRNo" type="hidden" runat="server" /><input style="WIDTH: 1px" id="hdEnPTRNo" type="hidden" runat="server" /></td>
                                                                    <td class="textbold" >PTR No</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPTRNo" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                        <img id="Img5" TabIndex="2" runat="server" alt="Select & Add PTR No" onclick="PopupPageBRFunctional(3)"
                                                                            src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                    <td class="textbold">BDR Letter ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBDRLetterID" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                        <img id="Img3" TabIndex="2" runat="server" alt="Select & Add BDR Letter ID" onclick="PopupPageBRFunctional(4)"
                                                                            src="../Images/lookup.gif" style="cursor: pointer" />
                                                                        <input style="WIDTH: 1px" id="hdBDRLetterID" type="hidden" runat="server" />
                                                                        <input style="WIDTH: 1px" id="hdEnBDRLetterID" type="hidden" runat="server" />
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td ><input id="hdWorkOrderNo" runat="server" style="width: 1px" type="hidden" /><input style="WIDTH: 1px" id="hdEnWorkOrderNo" type="hidden" runat="server" /></td>
                                                                    <td class="textbold" >Work Order No</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWorkOrderNo" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                        <img id="Img4" runat="server" TabIndex="2" alt="Select & Add Work Order No" onclick="PopupPageBRFunctional(5)"
                                                                            src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                    <td class="textbold">Contact Type<span class="Mandatory">*</span></td>
                                                                    <td><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlContactType" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                    </asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >Disposition</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlDisposition" runat="server" TabIndex="2" Width="174px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Close Date Time</td>
                                                                    <td><asp:TextBox ID="txtCloseDateTime" runat="server" CssClass="textboxgrey" MaxLength="10" ReadOnly="True"
                                                                            Width="170px" TabIndex="20"></asp:TextBox>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >&nbsp;</td>
                                                                    <td class="textbold" >Logged By </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                    <td class="textbold">Logged Date Time</td>
                                                                    <td><asp:TextBox ID="txtLoggedDate" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                        </td>
                                                                </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Title</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="100" TabIndex="2" Width="168px"></asp:TextBox></td>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td ></td>
                                                                    <td></td>
                                                                    <td ></td>
                                                                    <td>
                                                                       <input id="hdPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdQueryString" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdFeedBackPresence" runat="server" style="width: 1px" type="hidden" value="1" />
                                                                        <input id="hdFeedBackId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdSaveRights" runat="server" style="width: 1px" type="hidden" value="0" />
                                                                        <input id="hdMsg" runat="server" style="width: 1px" type="hidden"  />
                                                                        <input id="hdReSave" runat="server" style="width: 1px" type="hidden" value="0" />
                                                                        <input id="hdQueryCategory" runat="server" style="width: 1px" type="hidden"  />
                                                                        <input id="hdQuerySubCategory" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdSubCategoryMandatory" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdEnPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdEnPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdEnFeedBackId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdEnFunctional" runat="server" style="width: 1px" type="hidden" />
                                                                       </td>
                                                                </tr>
                                                                    </table>
                                                                       
                                                                       </asp:Panel>
                                                                    <asp:Panel ID="pnlDesc" runat="server" Width="100%" CssClass="displayNone">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 2%"></td>
                                                                            <td class="gap" colspan="4"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td colspan="4"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td  colspan="4"><asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" Height="170px" Rows="5"
                                                                            TextMode="MultiLine" Width="622px" TabIndex="2"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="gap" colspan="4"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td  colspan="4">
                                                                             <asp:GridView EnableViewState="False" ID="gvDescription" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                                                                <Columns>
                                                                                     <asp:BoundField HeaderText="User" DataField="EmployeeName" HeaderStyle-Width="20%" />
                                                                                    <asp:BoundField HeaderText="DateTime" DataField="DATETIME"  HeaderStyle-Width="20%"/>
                                                                                    <asp:TemplateField HeaderStyle-Width="60%">
                                                                                    <ItemTemplate>
                                                                                            <asp:TextBox ID="txtDesc" runat="server" CssClass="textbox" Height="150px" BorderStyle="none" TextMode="MultiLine" Wrap="true" Width="100%" ReadOnly="True" BorderColor="white" BorderWidth="0px" Text='<%#Eval("ACTION_TAKEN") %>' ></asp:TextBox>
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
                                                                            <td style="width: 2%"></td>
                                                                            <td class="gap" colspan="4"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                           <td colspan="4"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td colspan="4">
                                                                            <asp:TextBox ID="txtSolution" runat="server" CssClass="textbox" Height="170px" Rows="5"
                                                                            TextMode="MultiLine" Width="622px" TabIndex="2"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="gap" colspan="4"><input style="width: 1px" id="hdSol" type="hidden" runat="server" value="1" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td  colspan="4">
                                                                              <asp:GridView EnableViewState="False" ID="gvSolution" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                                                                <Columns>
                                                                                     <asp:BoundField HeaderText="User" DataField="EmployeeName" HeaderStyle-Width="20%" />
                                                                                     <asp:BoundField HeaderText="DateTime" DataField="DATETIME" HeaderStyle-Width="20%" />
                                                                                      <asp:TemplateField HeaderStyle-Width="60%">
                                                                                    <ItemTemplate>
                                                                                            <asp:TextBox ID="txtSoln" runat="server" CssClass="textbox" Height="150px" BorderStyle="none" TextMode="MultiLine" Wrap="true" Width="100%" ReadOnly="True" BorderColor="white" BorderWidth="0px" Text='<%#Eval("ACTION_TAKEN") %>' ></asp:TextBox>
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
                                                                    <asp:Panel ID="pnlFollowUp" runat="server" Width="100%" CssClass="displayNone">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 15%"></td>
                                                                            <td class="gap" colspan="2"></td>
                                                                        </tr>
                                                                        <tr>
                                                                        <td >&nbsp;</td>
                                                                    <td class="textbold" style="width: 136px">Mode <span class="Mandatory"></span> </td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlMode" runat="server" CssClass="dropdownlist" Width="269px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    </tr>
                                                                     <tr>
                                                                            <td ></td>
                                                                            <td class="textbold" style="width: 136px">
                                                                                Description <span class="Mandatory"></span></td>
                                                                            <td colspan="1">
                                                                            <asp:TextBox ID="txtFollowDesc" runat="server" CssClass="textbox" Height="70px" Rows="5"
                                                                            TextMode="MultiLine" Width="264px" TabIndex="2"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="textbold" style="width: 136px">
                                                                        Next Follow up Date <span class="Mandatory"></span></td>
                                                                            <td>
                                                                          <asp:TextBox ID="txtNxtFollowupDate" runat="server" CssClass="textboxgrey"
                                                                            Width="266px" ReadOnly="True" TabIndex="2"></asp:TextBox>
                                                                        <img id="imgDateAssigned" alt="" src="../Images/calender.gif" style="cursor: pointer"   title="Date selector" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtNxtFollowupDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y %H:%M",
                                                                                                    button         :    "imgDateAssigned",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true,
                                                                                                     showsTime      : true
                                                                                                    });                                                                                                  
                                                                                                  </script>
                                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="textbold" >Total No. of Follow Up
                                                                            </td>
                                                                            <td><asp:TextBox ID="txtNoOfFollowup" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                            Width="266px" TabIndex="2"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                       
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td ></td>
                                                                            <td colspan="1"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="textbold" colspan="2">
                                                                                  <asp:GridView EnableViewState="False" ID="gvFollowUp" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="99%">
                                                                                <Columns>
                                                                                     <asp:BoundField HeaderText="User" DataField="EmployeeName" HeaderStyle-Width="20%" />
                                                                                    <asp:BoundField HeaderText="DateTime" DataField="DATETIME" HeaderStyle-Width="20%" />
                                                                                    <asp:BoundField HeaderText="Mode" DataField="CONTACT_TYPE_Name" HeaderStyle-Width="20%" />
                                                                                    <asp:BoundField HeaderText="Description" DataField="FOLLOWUP_DESC" HeaderStyle-Width="20%" />
                                                                                    </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue center" />
                                                    <RowStyle CssClass="textbold center" />
                                                    <HeaderStyle CssClass="Gridheading center" />                                                    
                                                 </asp:GridView></td>
                                                                        </tr>
                                                                        </table>
                                                                        </asp:Panel>
                                                                       
                                                                    </td>
                                                                    <td class="center top " colspan="2" rowspan="1">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin" Text="Save" Width="100px" OnClientClick="return ManageBRCallLogPage()" AccessKey="s" /><br />
                                                                        &nbsp;<asp:Button ID="btnNew" runat="server" TabIndex="3" CssClass="button topMargin" Text="New" Width="100px" AccessKey="n" />&nbsp;<br />
                                                                         <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin" Text="Reset" Width="100px" AccessKey="r" /><br />
                                                                        <asp:Button ID="btnHistory" runat="server" TabIndex="3" CssClass="button topMargin" Text="History" Width="100px" OnClientClick="return PopupPageBRFunctional(7)"  /><br />
                                                                         <asp:Button ID="btnAssigneeHistory" runat="server" TabIndex="3" CssClass="button topMargin" Text="Assignee History" Width="100px" OnClientClick="return PopupPageBRFunctional(6)" /><br />
                                                                        &nbsp;<asp:Button ID="btnFeedBack" runat="server" TabIndex="3" CssClass="button topMargin" Text="FeedBack" Width="100px" OnClientClick="return PopupPageBRFunctional(8)" />&nbsp;
                                                                       
                                                                        </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td  class="ErrorMsg" style="width:10%">Field Marked * are Mandatory</td>
                                                                    <td>       </td>
                                                                    <td></td>
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
                    <asp:HiddenField ID="hdTimeSpan" runat="server" />
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
               //document.getElementById("ddlQuerySubGroup").selectedIndex=0;
               document.getElementById("hdReSave").value="0";

    }

}


</script>
</html>
