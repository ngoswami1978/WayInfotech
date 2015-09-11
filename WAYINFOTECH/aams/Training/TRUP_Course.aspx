<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_Course.aspx.vb" Inherits="Training_TRUP_Course" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Training::Manage Course</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
</head>
<body onload="HideShowCourse()" >
    <form id="form1" runat="server" defaultbutton="btnSave" defaultfocus="txtCourse">
        <table width="860px" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Training-></span><span class="sub_menu">Manage Course</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">Manage Course</td>
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
                                        <td class="top" style="height: 22px; width: 80%">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width: 20%">&nbsp; &nbsp; &nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="redborder top" colspan="2" style="width: 100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" style="width: 85%" valign="top">
                                                                    <asp:Panel ID="pnlCall" runat="server" Width="100%">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 2%"></td>
                                                                                <td class="gap subheading" colspan="4">Course Detail</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold" style="width: 136px">Title<span class="Mandatory">*</span></td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="txtCourse" runat="server" CssClass="textbox" TabIndex="2" Width="475px"
                                                                                        MaxLength="100"></asp:TextBox></td>
                                                                                <td ></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold" >Level<span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td >
                                                                                    <asp:DropDownList ID="ddlLevel" runat="server" TabIndex="2" Width="174px" CssClass="dropdown"
                                                                                        onkeyup="gotop(this.id)">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold" >Duration (In Days)<span class="Mandatory">*</span></td>
                                                                                <td >
                                                                                    <asp:TextBox ID="txtDuration" runat="server" CssClass="textbox" TabIndex="2" Width="125px"
                                                                                        MaxLength="3" ></asp:TextBox></td>
                                                                                <td rowspan="8"><br />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                                                                            </tr>
                                                                            <tr id="Tr1" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Pract Marks</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPractMarks" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="168px" ></asp:TextBox></td>
                                                                                <td class="textbold">Test Required</td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkRequired" runat="server" onclick="EnableNoOfTest(this.id)" TabIndex="2" /></td>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >No Of Test</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtNoOfTest" runat="server" CssClass="textboxgrey" MaxLength="1"
                                                                                        onblur="HideShowDay(this.id)" onkeyup="checknumeric(this.id)" TabIndex="2"
                                                                                        Width="168px" ReadOnly="True"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr id="trDay1" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 1<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay1" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr id="trDay2" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 2<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay2" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr id="trDay3" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 3<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay3" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td >
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trDay4" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 4<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay4" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr id="trDay5" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 5<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay5" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr id="trDay6" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 6<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay6" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr id="trDay7" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 7<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay7" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr id="trDay8" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 8<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay8" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr id="trDay9" runat="server">
                                                                                <td ></td>
                                                                                <td class="textbold" >Total Marks Test 9<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDay9" runat="server" CssClass="textbox" MaxLength="3" TabIndex="2"
                                                                                        Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold" style="width: 136px">
                                                                                    Description</td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="txtCourseDescription" runat="server" CssClass="textbox" Height="45px"
                                                                                        TabIndex="2" TextMode="MultiLine" Width="475px" MaxLength="300"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold" >Document</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlDocument" runat="server" TabIndex="2" Width="174px" CssClass="dropdown"
                                                                                        onkeyup="gotop(this.id)">
                                                                                    </asp:DropDownList></td>
                                                                                <td ></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold" >Internal Course</td>
                                                                                <td><asp:CheckBox ID="chkInternalCourse" runat="server" TabIndex="2" /></td>
                                                                                <td class="textbold">Show On Web</td>
                                                                                <td><asp:CheckBox ID="chlShowOnWeb" runat="server" TabIndex="2" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td  style="width: 136px"></td>
                                                                                <td></td>
                                                                                <td ></td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td colspan="4" class="subheading">Manuals</td>
                                                                                <td ></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td colspan="4" style="height: 19px">
                                                                                    <asp:GridView ID="gvManuals" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                        Width="93%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectAllCourse();" />
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <input type="checkbox" id="chkSelect" name="chkSelect" runat="server" />
                                                                                                    <asp:HiddenField ID="hdDataID" runat="server" Value='<% #Container.DataItem("TR_MANUAL_ID") %>' />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="5%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="TR_MANUAL_NAME" HeaderText="Existing Manuals" />
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" />
                                                                                        <HeaderStyle CssClass="Gridheading" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                                <td ></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                  
                                                                </td>
                                                                <td class="center top " style="width: 15%" colspan="2" rowspan="1">
                                                                    <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Save" Width="100px" AccessKey="s" /><br />
                                                                    <asp:Button ID="btnNew" runat="server" TabIndex="3" CssClass="button topMargin" Text="New"
                                                                        Width="100px" AccessKey="n" /><br />
                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Reset" Width="100px" AccessKey="r" /><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td  colspan="2" ></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ErrorMsg" style="width: 10%">
                                                                    Field Marked * are Mandatory</td>
                                                                <td>
                                                                    &nbsp; &nbsp;
                                                                </td>
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
                    <asp:HiddenField ID="hdTabType" runat="server" Value="0" />
                    <asp:HiddenField ID="hdCourseId" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
