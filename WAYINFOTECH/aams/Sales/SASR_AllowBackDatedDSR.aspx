<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_AllowBackDatedDSR.aspx.vb"
    Inherits="Sales_SASR_AllowBackDatedDSR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Sales::Allow Back Dated DSR</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <!-- import the language module -->
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <!-- import the calendar setup module -->
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript">
    function LoadHistory()
    {
               type = "SASR_AllowBackDatedDSRHistory.aspx" ;
               window.open(type,"SASR_AllowBackDatedDSRHistory","height=600,width=800,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false; 
    
    }
    
    function ValidateData()
    {
             if(document.getElementById('drpSalesPerson').value == '')
         {
            document.getElementById('lblError').innerHTML = "Assigned to is mandatory.";
          
            return false;  
         }
         if(document.getElementById('txtDSRDate').value == '')
             {
                document.getElementById('lblError').innerHTML = "Date is mandatory.";                      
                return false;  
             }                     
             
            if(document.getElementById('txtDSRDate').value != '')
            {
                if (isDate(document.getElementById('txtDSRDate').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerHTML = "Date is not valid.";
                       return false; 
                    }  
            }   
    
    }
       function SelectDate(textBoxid,imgId)
    {   
                 Calendar.setup
                 (
                     {
                           inputField : textBoxid,
                           ifFormat :"%d/%m/%Y",
                           button :imgId,
                           onmousedown :true
                     }
                 )                                      
    }
    </script>
</head>
<body>
    <form id="form1" runat="server" >
        <asp:ScriptManager ID="Sc1" runat="server" AsyncPostBackTimeout="800">
        </asp:ScriptManager>
        <div>
            <table>
                <tr>
                    <td>
                        <table width="860px" class="border_rightred left">
                            <tr>
                                <td class="top">
                                    <table width="100%" class="left">
                                        <tr>
                                            <td>
                                                <span class="menu">Sales -&gt;</span><span class="sub_menu">Allow Back Dated DSR</span></td>
                                        </tr>
                                        <tr>
                                            <td class="heading center">
                                                <table>
                                                    <tr>
                                                        <td style="width: 840px;" align="center">
                                                            Manage Allow Back Dated DSR</td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td class="redborder center">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 840px">
                                                                        <table border="0" cellpadding="2" cellspacing="1" style="width: 840px" class="left">
                                                                            <tr>
                                                                                <td class="center" colspan="6" style="height: 17px">
                                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 3%">
                                                                                </td>
                                                                                <td class="textbold" style="width: 15%">
                                                                                    Assigned To</td>
                                                                                <td style="width: 27%">
                                                                                    <asp:DropDownList ID="drpSalesPerson" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                        onkeyup="gotop(this.id)" Width="174px">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold" style="width: 15%">
                                                                                    DSR Date</td>
                                                                                <td style="width: 26%">
                                                                                    <asp:TextBox ID="txtDSRDate" runat="server" CssClass="textbox" TabIndex="2" Width="170px"></asp:TextBox>
                                                                                    &nbsp;<img id="imgDSRDate" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                                        runat="server" style="cursor: pointer" /></td>
                                                                                <td class="center" style="width: 13%">
                                                                                    <asp:Button ID="BtnSave" runat="server" CssClass="button" TabIndex="3" Text="Save" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td style="width: 27%">
                                                                                </td>
                                                                                <td class="textbold" style="width: 15%">
                                                                                </td>
                                                                                <td style="width: 26%">
                                                                                     <asp:Button ID="BtnAdd" runat="server" CssClass="button" TabIndex="3" Text="Add" />&nbsp;&nbsp;<asp:Button ID="BtnCancel" runat="server" CssClass="button" TabIndex="3" Text="Cancel"  Visible ="true" /></td>
                                                                                <td style="width: 13%" class="center">
                                                                                    <asp:Button ID="BtnHistory" runat="server" CssClass="button" TabIndex="3" Text="History" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td style="width: 27%">
                                                                                </td>
                                                                                <td class="textbold" style="width: 15%">
                                                                                </td>
                                                                                <td style="width: 26%">
                                                                                </td>
                                                                                <td class="center" style="width: 13%">
                                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="center" style="height: 12px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="100%">
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 100%">
                                                                                    <asp:GridView ID="gvDSRBackDated" runat="server" AutoGenerateColumns="False" HorizontalAlign="left"
                                                                                        Width="100%" ShowFooter="true" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                                        AlternatingRowStyle-CssClass="lightblue" RowStyle-VerticalAlign="top" HeaderStyle-ForeColor="white"
                                                                                        AllowPaging="True" PageSize="25" AllowSorting="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField HeaderText="DSR Date" DataField="DSRDATE" SortExpression="DSRDATE"
                                                                                                HeaderStyle-HorizontalAlign="Left">
                                                                                                <HeaderStyle Width="100px" Wrap="False" />
                                                                                                <ItemStyle Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField HeaderText="Assigned To" DataField="RESP_NAME" SortExpression="RESP_NAME"
                                                                                                HeaderStyle-HorizontalAlign="Left">
                                                                                                <HeaderStyle Width="240px" Wrap="False" />
                                                                                                <ItemStyle Width="240px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField HeaderText="Logged By" DataField="EMPLOYEENAME" SortExpression="EMPLOYEENAME"
                                                                                                HeaderStyle-HorizontalAlign="Left">
                                                                                                <ItemStyle Width="240px" />
                                                                                                <HeaderStyle Width="240px" Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField HeaderText="Logged Date" DataField="LOGDATE" SortExpression="LOGDATE"
                                                                                                Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                                <HeaderStyle Width="100px" Wrap="False" />
                                                                                                <ItemStyle Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField HeaderText="DSR Code" DataField="DSRCODE" SortExpression="DSRCODE"
                                                                                                Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                                <HeaderStyle Width="70px" Wrap="False" />
                                                                                                <ItemStyle Width="70px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField HeaderText="Action">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ROWID")  %>'  ></asp:LinkButton>&nbsp;
                                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete"
                                                                                                        CssClass="LinkButtons"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ROWID")  %>' ></asp:LinkButton>
                                                                                                    <asp:HiddenField ID="hdDSRCODE" runat="server" Value='<%# Eval("DSRCODE")%>' />
                                                                                                    <asp:HiddenField ID="hdDSRDate" runat="server" Value='<%# Eval("DSRDATE")%>' />
                                                                                                    <asp:HiddenField ID="HdRowId" runat="server" Value='<%# Eval("ROWID")%>' />
                                                                                                    <asp:HiddenField ID="hdLoggedDate" runat="server" Value='<%# Eval("LOGDATE")%>' />
                                                                                                    <asp:HiddenField ID="hdEmplyeeId" runat="server" Value='<%# Eval("EMPLOYEEID")%>' />
                                                                                                    <asp:HiddenField ID="hdResId" runat="server" Value='<%# Eval("RESP_1A")%>' />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="80px" />
                                                                                                <ItemStyle Width="80px" Wrap="false" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" />
                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                        <PagerTemplate>
                                                                                        </PagerTemplate>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                     <asp:HiddenField ID="HdRowId" runat="server" Value='' />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign="top" style="width: 850px; height: 58px;">
                                                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="850px">
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="850px">
                                                                                            <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                                                <td style="width: 28%" class="left">
                                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                                        ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3"
                                                                                                        Width="100px" ReadOnly="True" Text="0" Visible="True"></asp:TextBox></td>
                                                                                                <td style="width: 33%" class="right">
                                                                                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                                <td style="width: 20%" class="center">
                                                                                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                                                        ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                                    </asp:DropDownList></td>
                                                                                                <td style="width: 25%" class="left">
                                                                                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next ></asp:LinkButton></td>
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
