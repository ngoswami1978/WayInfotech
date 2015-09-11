<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_AllLetterOperation.aspx.vb" Inherits="Training_TRUP_AllLetterOperation" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Training::Manage Letter</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
 <script language="javascript" type="text/javascript" src="../JavaScript/TextEditor.js"></script>

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script language="javascript" type="text/javascript">
function TextContent()
    {   
         
            NewsBody_rich.document.body.innerHTML=document.getElementById("hdnmsg").value;
            self.focus();
    }
function ColorMethod(id,total)
{   
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
            document.getElementById(Hcontrol).className="headingtabactive";
        }
        if (document.getElementById("hdTabType").value=="2")
       {
            document.getElementById("pnlTemplate").style.display="none";
            document.getElementById("pnlList").style.display="none";
            return false;
       }
       document.getElementById(id).className="headingtab";     
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            document.getElementById('hdTabType').value='0';
            document.getElementById("pnlTemplate").style.display="block";
            document.getElementById("pnlList").style.display="none";
            return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
           document.getElementById('hdTabType').value='1';
           document.getElementById("pnlTemplate").style.display="none";
           document.getElementById("pnlList").style.display="block";
           return false;         
       }
       
}

     

    function HideShow()
    {
    // Calling function for Letter Format.
   // FillLetterText()
    
    var strTabtype=document.getElementById("hdTabType").value;
    switch(strTabtype)
    {
    case "0":
            document.getElementById("pnlTemplate").style.display="block";
            document.getElementById("pnlList").style.display="none";
            break;
    case "1":
            document.getElementById("pnlTemplate").style.display="none";
            document.getElementById("pnlList").style.display="block";
            break;
    case "2":
            document.getElementById("pnlTemplate").style.display="none";
            document.getElementById("pnlList").style.display="none";
            break;
            
     }
    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
     switch(strTabtype)
    {
    case "0":
           document.getElementById("theTabStrip_ctl00_Button1").className="headingtab";
             break;
    case "1":
           document.getElementById("theTabStrip_ctl01_Button1").className="headingtab";
             break;
    }
    }
    function SelectAll() 
    {
       CheckAllDataGridCheckBoxes(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxes(value) 
    {
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
              elm.checked = value
            }
        }
    }
    function FillLetterText()
{
    //document.getElementById('dvLetter').innerHTML=document.getElementById('<%=txtTemplate.ClientID %>').value;
}
    
</script>
</head>
<body onload="HideShow()"  >
    <form id="form1" runat="server" defaultfocus="rdFunctional" defaultbutton="btnSave"  >
        <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Training-&gt; </span><span class="sub_menu">Manage Letter</span></td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">
                                            Manage Letter</td>
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
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width:20%">
                                        <asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="window.close();return false;" >Close</asp:LinkButton> &nbsp; &nbsp;&nbsp;
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
                                                                    <asp:Panel ID="pnlTemplate" runat="server" Width="100%">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                   <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 130px">
                                                                        Letter Type</td>
                                                                    <td colspan="2">
                                                                        <asp:RadioButton ID="rdInvitation" runat="server" CssClass="textbold" Text="Invitation" GroupName="r1" Width="171px" Checked="True" TabIndex="2" AutoPostBack="True" />
                                                                        <asp:RadioButton ID="rdDistinction" runat="server" CssClass="textbold" Text="Distinction" GroupName="r1"     Width="171px" AutoPostBack="True" TabIndex="2" /></td>
                                                                    <td>
                                                                        </td>
                                                                     <td class="top" colspan="2" rowspan="4">
                                                                        <br />
                                                                         &nbsp; &nbsp; &nbsp;
                                                                         &nbsp; &nbsp;</td>
                                                                </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" style="width: 130px">
                                                                                Authorised Signatory</td>
                                                                            <td colspan="2">
                                                                                <asp:DropDownList ID="ddlAuth" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                    Width="176px">
                                                                                  
                                                                                </asp:DropDownList><input type="hidden" runat="server" id="hdPageAoffice" style="width:1px" /></td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Template</td>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" colspan="4">
                                                                        <asp:TextBox ID="txtTemplate" runat="server" CssClass="textbox" Width="532px" ReadOnly="True" Height="250px" Rows="5" TextMode="MultiLine" TabIndex="2"></asp:TextBox></td>
                                                                </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" colspan="4">
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
                                                                            <td id="mam" height="180">
                                                                                <iframe id="NewsBody_rich" src="../Popup/MsgBody.htm" width="100%" height="400px" font-name="Verdana" onload="TextContent()">
                                                                                </iframe>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                            </td>
                                                                            <td class="top" colspan="2" rowspan="1">
                                                                            </td>
                                                                        </tr>
                                                                 <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" colspan="4">
                                                                    <div id="dvLetter" runat="server"></div>
                                                                    </td>
                                                                </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="ErrorMsg" colspan="4">
                                                                              Please Do Not Modify Content Within [[ ]] Brackets.
                                                                            </td>
                                                                            <td class="top" colspan="2" rowspan="1">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                       
                                                                       </asp:Panel>
                                                                    <asp:Panel ID="pnlList" runat="server" Width="100%" CssClass="displayNone">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="gap" colspan="4">
                                                                            
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="gap" colspan="4">
                                                                           

                                                                             <asp:GridView  ID="gvParticipant" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                                                                <Columns>
                                                                                 <asp:TemplateField HeaderStyle-CssClass="left" HeaderText="" >
                                                                                 <HeaderTemplate  >
                                                                           <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectAll();" /> 
                                                                           
                                                                        </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                 
                                                                                    <asp:CheckBox ID="chkSelect" runat ="server" />
                                                                                   <asp:HiddenField ID="hdTR_COURSEP_ID" runat="server" Value='<%#Eval("TR_COURSEP_ID")%>' />
                                                                                   <asp:HiddenField ID="hdTR_CLETTER_ID" runat="server" Value='<%#Eval("TR_CLETTER_ID")%>' />
                                                                                   
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:BoundField HeaderText="Participant Name" DataField="NAME" >
                                                                                         <ItemStyle Width="48%" />
                                                                                     </asp:BoundField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Email Id" >
                                                                                    <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEmail" runat ="server" Text='<%#Eval("EMAILID") %>' />
                                                                                   
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                 </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading center" />                                                    
                                                                                 </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="gap" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        </table>
                                                                        </asp:Panel>
                                                                  
                                                                       
                                                                    </td>
                                                                    <td class="center top " colspan="2" rowspan="1">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin" Text="Email All" Width="100px" OnClientClick="return ManageTrainingLetter(this.value)" AccessKey="m" /><br />
                                                                         <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin" Text="Reset" Width="100px" AccessKey="r" />
                                                                       
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                        &nbsp;</td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="center" colspan="2" rowspan="1">
                                                                    </td>
                                                                </tr>
                                                             
                                                              
                                                                
                                                                
                                                                <tr>
                                                                    <td   style="width:10%">
                                                                      </td>
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
                     <asp:HiddenField ID="hdTemplateShowIndicator" runat="server" Value="0" />
                    <asp:HiddenField ID="hdTabType" runat="server" Value="0" />
                      <asp:HiddenField ID="hdnmsg" runat="server" />
                    <input type="hidden" runat="server" id="hdPageCourseSessionID" style="width: 8px" />
                    <asp:Literal id="ltrPrint" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
            

    </form>
</body>
</html>
