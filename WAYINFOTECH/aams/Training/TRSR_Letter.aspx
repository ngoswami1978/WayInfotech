<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRSR_Letter.aspx.vb" Inherits="Training_TRSR_Letter" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js" ></script>
    <script language="javascript" type="text/javascript" src="../JavaScript/TextEditor.js"></script>
    <title>AAMS::Training::Send Letter</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                         <tr>
                            <td>
                            <table width="100%">
                            <tr>
                            <td align="left" ><span class="menu"> Training -&gt;</span><span class="sub_menu">Letter</span></td>
                            <td align="right"><asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnPTRIDTrainingLetter()" >Close</asp:LinkButton></td>
                            </tr>
                            </table>
                            
                            </td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Send Letter</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                        
                                        <tr>
                                            <td class="textbold" colspan="4">
                                            <asp:Panel ID="pnlAgency" runat="server" Width="100%" >
                                            
                                            <table style="width:100%" border="0" cellspacing="1" cellpadding="1">
                                            <tr>
                                            <td class="subheading" colspan="4">Agency Details</td>                                                                               
                                            
                                        </tr>
                                      
                                            <tr>
                                            <td class="textbold"> Agency Name</td>                                                                               
                                            <td colspan="3">
                                            <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="2" Width="534px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                      
                                              <tr>
                                            <td class="textbold" style="width:15%">
                                                Address</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Height="50px"
                                                    ReadOnly="True" Rows="5" TabIndex="20" TextMode="MultiLine" Width="534px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Country</td>
                                            <td style="width: 27%">
                                                <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 15%">
                                                City</td>
                                            <td style="width: 26%">
                                                <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" ReadOnly="True" TabIndex="20"
                                                    Width="170px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Phone</td>
                                            <td style="width: 27%">
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 15%">
                                                Fax</td>
                                            <td style="width: 26%">
                                                <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" ReadOnly="True" TabIndex="20"
                                                    Width="170px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Online Status</td>
                                            <td style="width: 27%">
                                                <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 15%">
                                                Priority</td>
                                            <td style="width: 26%">
                                                <asp:TextBox ID="txtPriority" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                        </tr>
                                       
                                            </table>
                                            
                                            </asp:Panel>
                                            
                                            </td>
                                            <td class="center top" rowspan="10" style="width: 12%">
                                            <asp:Button ID="btnSave" CssClass="button topMargin" runat="server" Text="Save" TabIndex="3" AccessKey="s"  /><br />
                                                <asp:Button ID="btnPrint" CssClass="button topMargin" runat="server" Text="Print" TabIndex="3" AccessKey="p"  /><br />
                                                
                                                <asp:Button ID="btnEmail" CssClass="button topMargin" runat="server" Text="Email" TabIndex="3" AccessKey="m" /><br />
                                                <asp:Button ID="btnReset" CssClass="button topMargin" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Contact Person</td>
                                            <td style="width: 27%">
                                                <asp:TextBox ID="txtAgencyStaff" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                            <td style="width: 15%">
                                            </td>
                                            <td style="width: 26%">
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Date Send</td>
                                            <td style="width: 27%">
                                                <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textboxgrey" Width="170px" TabIndex="2" ReadOnly="True"></asp:TextBox>
                                                
                                               </td>
                                            <td class="textbold" style="width: 15%; visibility: hidden;">
                                                Date Re-Send</td>
                                            <td style="width: 26%">
                                             <asp:TextBox ID="txtReSendDate" runat="server" CssClass="textboxgrey displayNone" Width="170px" TabIndex="2" ReadOnly="True"></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Email(Multiple email ids separated by comma)<span class="Mandatory">*</span></td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Height="30px" 
                                                    Rows="2" TabIndex="20" TextMode="MultiLine" Width="534px"></asp:TextBox></td>
                                            <td class="center" style="width: 13%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%" valign="top">
                                                &nbsp;Letter</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtLetter" runat="server" CssClass="textboxgrey" Height="50px" ReadOnly="True"
                                                    Rows="5" TabIndex="20" TextMode="MultiLine" Width="534px" Wrap="False"></asp:TextBox></td>
                                            <td class="center" style="width: 13%">
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td colspan="4">
                                            
                                            
                                                 <asp:Panel ID="pnlContentMgmt" runat="server" Width="100%">
                                                                    <table style="width: 100%; border-collapse: collapse; height: 77px" bordercolor="black"
                                                                        cellspacing="0" cellpadding="0" width="500" align="left" bgcolor="buttonface"
                                                                        border="1">
                                                                        <tr>
                                                                            <td height="20">
                                                                                <table height="18" cellspacing="1" cellpadding="0" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                              <img height="21" src="../Images/VertLine2.jpg" width="10" alt="" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('createLink')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Link" hspace="2" src="../Images/Link.gif" width="20" align="absMiddle"
                                                                                                    vspace="1" />
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine2.jpg" width="10" alt="" />
                                                                                            </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('Unlink')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Unlink" hspace="2" src="../Images/unlink.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine.jpg" width="10">
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('bold')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Bold" hspace="1" src="../Images/Bold.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('italic')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Italic" hspace="1" src="../Images/Italic.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('underline')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Underline" hspace="1" src="../Images/Under.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine.jpg" width="10"></td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('RemoveFormat')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Remove Format" hspace="2" src="../Images/UnSelect.gif" width="20"
                                                                                                    align="absMiddle" vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="foreColor()" onmouseout="button_out(this);">
                                                                                                <img id="BtnColor" height="19" alt="Forecolor" hspace="2" src="../Images/fgcolor.gif" width="20"
                                                                                                    align="absMiddle" vspace="1"></div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine.jpg" width="10"></td>
                                                                                        <td>
                                                                                            <select style="width: 80px" onchange="cmdExec('fontname',this[this.selectedIndex].value);"
                                                                                                name="selfontname">
                                                                                                <option selected>Font</option>
                                                                                                <option value="Arial">Arial</option>
                                                                                                <option value="Arial Black">Arial Black</option>
                                                                                                <option value="Arial Narrow">Arial Narrow</option>
                                                                                                <option value="Comic Sans MS">Comic Sans MS</option>
                                                                                                <option value="Courier New">Courier New</option>
                                                                                                <option value="System">System</option>
                                                                                                <option value="Tahoma">Tahoma</option>
                                                                                                <option value="Times">Times</option>
                                                                                                <option value="Verdana">Verdana</option>
                                                                                                <option value="Wingdings">Wingdings</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select onchange="cmdExec('fontsize',this[this.selectedIndex].value);" name="selfontsize">
                                                                                                <option selected>Size</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                                <option value="5">5</option>
                                                                                                <option value="6">6</option>
                                                                                                <option value="7">7</option>
                                                                                                <option value="8">8</option>
                                                                                                <option value="9">9</option>
                                                                                                <option value="10">10</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine2.jpg" width="10"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td id="mam" height="380">
                                                                                <iframe id="NewsBody_rich" src="../Popup/MsgBody.htm" width="100%" height="100%" font-name="Verdana" onload="TextContentTrainingLetter()">
                                                                                </iframe>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                            </td>
                                            <td class="center" style="width: 13%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold">
                                                Authorised Signatory</td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlAuth" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                    Width="176px">
                                                    
                                                </asp:DropDownList></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold">
                                            <asp:HiddenField ID="hdPageBasketID" runat="server" />
                                            </td>
                                            <td colspan="3">
                                                <input id="hdCourseName" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdStartDate" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdEndDate" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdStartTime" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdEndTime" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdTrainingLocation" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdStaffId" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdResult" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdLetterId" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdStaffName" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdEmpAddress" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdEmpCity" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdAgency" runat="server" style="width: 1px" type="hidden" />

                                                <input id="hdTotalMarks" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdTotalPracticalMarks" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdTotalTheoryMarks" runat="server" style="width: 1px" type="hidden" />

                                                <input id="hdnmsg" runat="server" style="width: 1px" type="hidden" />

                                                <input id="hdRptLetter" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdCourseParID" runat="server" style="width: 1px" type="hidden" />
                                                
                                                <input id="hdCountryCode" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdRegister_online_Site" runat="server" style="width: 1px" type="hidden" />
                                                
                                                
                                               
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ErrorMsg" colspan="4">
                                                Field Marked * are Mandatory</td>
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
    </div>
    </form>
     
</body>
</html>
