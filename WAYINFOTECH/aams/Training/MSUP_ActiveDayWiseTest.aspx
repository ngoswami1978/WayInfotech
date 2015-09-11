<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_ActiveDayWiseTest.aspx.vb" Inherits="Training_MSUP_ActiveDayWiseTest" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Training::Manage Course Session</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<script language ="javascript" type="text/javascript">
function SelectAllFeedBack()
{
CheckAllDataGridCheckBoxesChallan(document.forms[0].chkAllSelect.checked)
}

function SelectAllTest(day)
{
var id="chkAllTest" +day;
CheckAllDataGridCheckBoxesTest(document.getElementById(id).checked,day);
}



function CheckAllDataGridCheckBoxesTest(value,day)
{

    for(i=0;i<document.forms[0].elements.length;i++)
    {
        try
        {
            var elm = document.forms[0].elements[i];
            if(elm.type == 'checkbox')
            {
                var chkId=elm.id;
                if (chkId.split("_")[0]=="gvParticipantTab")
                {
                    if (chkId.split("_")[2]==("chkDay" + day))
                    {
                    elm.checked = value
                    }
                }
            }
        }
        catch(err){}
    }
}



function CheckAllDataGridCheckBoxesChallan(value)
{

for(i=0;i<document.forms[0].elements.length;i++)
{
try
{
var elm = document.forms[0].elements[i];
if(elm.type == 'checkbox')
{
var chkId=elm.id;
if (chkId.split("_")[0]=="gvParticipantTab")
{
if (chkId.split("_")[2]=="chkFeedBack")
{
elm.checked = value
}
}
}
}
catch(err){}
}
}


    
</script>
</head>
<body onload="HideShowDayWiseTest()" >
    <form id="form1" runat="server" defaultbutton="btnSave" >
        <table   class="left">
            <tr>
                <td class="top">
                    <table  width="860px">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Training-></span><span class="sub_menu">Activate Test</span></td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">Manage Activate Test</td>
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
                                        <td class="right" style="width:20%">&nbsp; &nbsp; &nbsp;&nbsp;</td>
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
                                                                           <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 2%"></td>
                                                                            <td class="gap" colspan="7"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" colspan="8">
                                                                             <asp:GridView ID="gvParticipantTab" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                                                                <Columns>
                                                                                 <asp:BoundField HeaderText="Participant Name" DataField="PARTICIPANTNAME" HeaderStyle-Width="20%"  />
                                                                                    <asp:TemplateField >
                                                                                    <HeaderTemplate>Test1<br />
                                                                                       <input type="checkbox"  id="chkAllTest1" name="chkAllTest1" onclick="SelectAllTest('1');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay1" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns1" runat="server" Text ="Ans" CssClass="LinkButtons"  ></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField >
                                                                                     <HeaderTemplate>Test2<br />
                                                                                       <input type="checkbox"  id="chkAllTest2" name="chkAllTest2" onclick="SelectAllTest('2');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay2" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns2" runat="server" Text ="Ans" CssClass="LinkButtons"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField >
                                                                                     <HeaderTemplate>Test3<br />
                                                                                       <input type="checkbox"  id="chkAllTest3" name="chkAllTest3" onclick="SelectAllTest('3');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay3" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns3" runat="server" Text ="Ans" CssClass="LinkButtons"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField  >
                                                                                     <HeaderTemplate>Test4<br />
                                                                                       <input type="checkbox"  id="chkAllTest4" name="chkAllTest4" onclick="SelectAllTest('4');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay4" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns4" runat="server" Text ="Ans" CssClass="LinkButtons"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField >
                                                                                     <HeaderTemplate>Test5<br />
                                                                                       <input type="checkbox"  id="chkAllTest5" name="chkAllTest5" onclick="SelectAllTest('5');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay5" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns5" runat="server" Text ="Ans" CssClass="LinkButtons"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField >
                                                                                     <HeaderTemplate>Test6<br />
                                                                                       <input type="checkbox"  id="chkAllTest6" name="chkAllTest6" onclick="SelectAllTest('6');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay6" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns6" runat="server" Text ="Ans" CssClass="LinkButtons"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField >
                                                                                     <HeaderTemplate>Test7<br />
                                                                                       <input type="checkbox"  id="chkAllTest7" name="chkAllTest7" onclick="SelectAllTest('7');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay7" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns7" runat="server" Text ="Ans" CssClass="LinkButtons"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField >
                                                                                     <HeaderTemplate>Test8<br />
                                                                                       <input type="checkbox"  id="chkAllTest8" name="chkAllTest8" onclick="SelectAllTest('8');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay8" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns8" runat="server" Text ="Ans" CssClass="LinkButtons"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                     <asp:TemplateField  >
                                                                                     <HeaderTemplate>Test9<br />
                                                                                       <input type="checkbox"  id="chkAllTest9" name="chkAllTest9" onclick="SelectAllTest('9');" />                                                                                       
                                                                                     </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDay9" runat ="server" /><br />
                                                                                    <asp:LinkButton ID="lnkAns9" runat="server" Text ="Ans" CssClass="LinkButtons"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                     <asp:TemplateField>
                                                                                     <HeaderTemplate>FeedBack<br />
                                                                                       <input type="checkbox"  id="chkAllSelect" name="chkAllSelect" onclick="SelectAllFeedBack();" />
                                                                                       
                                                                                     </HeaderTemplate>
                                                                                     
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkFeedBack" runat ="server" /><br />
                                                                                   <asp:LinkButton ID="lnkFeedDetails" runat="server" Text ="Details" CssClass="LinkButtons"></asp:LinkButton>
                                                                                   <asp:HiddenField ID="hdTR_COURSEP_ID" runat="server" Value='<%#Eval("TR_COURSEP_ID")%>' />   
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
                                                                            <td class="gap" colspan="8">
                                                    <input id="hdData" runat="server" style="width: 1px" type="hidden" />
                                                   <input id="hdCourseID" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdPageCourseSessionID" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdDayConfig_ID" runat="server" style="width: 1px" type="hidden" />
                                                 <input id="hdDuration" runat="server" style="width: 1px" type="hidden" />
                                                  <input id="hdNoOfTest" runat="server" style="width: 1px" type="hidden" />
                                                   <asp:HiddenField ID="hdTabType" runat="server" Value="0" />
                                                    <input id="hdEnPageCourseSessionID" runat="server" style="width: 1px" type="hidden" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 2%"></td>
                                                                            <td class="gap" colspan="7">&nbsp;</td>
                                                                        </tr>
                                                                        </table>
                                                                    </td> 
                                                                    <td class="center top " colspan="2" rowspan="1">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin" Text="Save" Width="100px" AccessKey="a" /><br />
                                                                         <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin" Text="Reset" Width="100px" AccessKey="r" />
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td ></td>
                                                                    <td></td>
                                                                    <td  colspan="2" ></td>
                                                                </tr>
                                                                <tr>
                                                                    <td  class="ErrorMsg" style="width:10%">Field Marked * are Mandatory</td>
                                                                    <td>                     &nbsp; &nbsp;                     </td>
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
