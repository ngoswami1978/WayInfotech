<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOHDSR_QuestionSet.aspx.vb"
    Inherits="BOHelpDesk_HDSR_QuestionSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Back Office HelpDesk::Search Question Set</title>
    <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
 
      function MandatoryFunction()
    {
     if(document.getElementById("drpRegion").value=='')
        {
        document.getElementById("drpRegion").focus();
        document.getElementById("lblError").innerHTML ="Region Name is Mandatory";
        return false;
        }
    }

    function EditFunction(CheckBoxObj)
    {           
        window.location.href="BOHDUP_QuestionSet.aspx?Action=U&SetID="+CheckBoxObj;       
        return false;
    }
    
     function DeleteFunction(ID)
    {   
    
   
        if (confirm("Are you sure you want to delete?")==true)
        {   
         document.getElementById('<%=hdDeleteID.ClientId%>').value = ID
               return true; 
        }
        else
        {
            return false;
        }
    }
    function NewFunction()
    {   
        window.location.href="BOHDUP_QuestionSet.aspx?Action=I";       
        return false;
    }
     
    

    
    
    </script>

</head>
<body>
    <form id="form1" defaultbutton ="btnSearch"   runat="server">
        <div>
            <table width="840px" class="border_rightred left">
                <tr>
                    <td class="top" style="width: 857px">
                        <table width="100%" class="left">
                            <tr>
                                <td>
                                    <span class="menu">Back Office HelpDesk -&gt;</span><span class="sub_menu">QuestionSet</span></td>
                            </tr>
                            <tr>
                                <td class="heading center" style="height: 20px">
                                    Search Question Set</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="redborder center">
                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                    <tr>
                                                        <td class="center" colspan="7" style="height: 25px">
                                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 74px">
                                                        </td>
                                                        <td class="textbold" style="width: 78px">
                                                            Month<span class="Mandatory"> </span>
                                                        </td>
                                                        <td class="textbold" colspan="2">
                                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                TabIndex="1" Width="120px">
                                                                  <asp:ListItem Value="">--All--</asp:ListItem>
                                                                 <asp:ListItem Value="1">January</asp:ListItem>
                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">september</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td class="textbold" style="width: 11%">
                                                            Year<span class="Mandatory"> </span>
                                                        </td>
                                                        <td style="width: 54px">
                                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                                position: relative; top: 0px" TabIndex="2" Width="120px">
                                                            </asp:DropDownList></td>
                                                        <td class="center" style="width: 122px">
                                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 26px; width: 74px;">
                                                        </td>
                                                        <td class="textbold" style="height: 26px; width: 78px;">
                                                        </td>
                                                        <td class="textbold" colspan="2" style="height: 26px">
                                                            &nbsp;</td>
                                                        <td class="textbold" style="width: 11%; height: 26px;">
                                                        </td>
                                                        <td style="width: 54px; height: 26px;">
                                                            &nbsp;</td>
                                                        <td class="center" style="height: 26px; width: 122px;">
                                                            <asp:Button ID="btnNew" runat="server" CssClass="button" Style="left: 0px; position: relative;
                                                                top: 0px" TabIndex="4" Text="New" AccessKey="n" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 22px; width: 74px;">
                                                        </td>
                                                        <td class="textbold" style="height: 22px; width: 78px;">
                                                        </td>
                                                        <td   colspan="2" style="height: 22px">
                                                            
                                                        </td>
                                                        <td class="textbold" style="width: 11%; height: 22px;">
                                                        </td>
                                                        <td style="height: 22px; width: 54px;">
                                                        </td>
                                                        <td class="center" style="height: 22px; width: 122px;">
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative"
                                                                TabIndex="5" Text="Export" AccessKey="e" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px; width: 74px;">
                                                        </td>
                                                        <td class="textbold" style="height: 23px; width: 78px;">
                                                        </td>
                                                        <td style="width: 20%; height: 23px;">
                                                        </td>
                                                        <td class="textbold" style="width: 6%; height: 23px;">
                                                        </td>
                                                        <td class="textbold" style="width: 11%; height: 23px;">
                                                        </td>
                                                        <td style="height: 23px; width: 54px;">
                                                        </td>
                                                        <td class="center" style="height: 23px; width: 122px;">
                                                            &nbsp;<asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="r"
                                                                 />&nbsp;<%-- <asp:Button ID="btnHistory" CssClass="button" runat="server" Text="History" TabIndex="5" style="position: relative" />--%></td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                    <tr>
                                                       <td style="width: 75px"></td>
                                                        <td colspan="5"   align="center" >
                                                            <asp:GridView ID="gvQuestionSet" runat="server" AutoGenerateColumns="False" TabIndex="6" HeaderStyle-ForeColor="white" Width="500px" AllowSorting="True" EnableViewState="False">
                                                                
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SetId" SortExpression="ID">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblSetId" Text='<%# Eval("ID")%>' CssClass="textbox"></asp:Label>
                                                                            <asp:HiddenField ID="hdSetId" runat="server" Value='<%#Eval("ID")%>' />
                                                                              <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Month" SortExpression="Month">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="txtmonth" Text='<%# MonthName(Eval("Month"))%>' CssClass="textbox"></asp:Label>
                                                                            <asp:HiddenField ID="hdMonth" runat="server" Value='<%#Eval("Month")%>' />
                                                                        </ItemTemplate>
                                                                          <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Year" DataField="Year" SortExpression="Year">
                                                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                             <asp:HiddenField ID="hdQID" runat="server" Value='<%#Eval("ID")%>' /> 
                                                                            <asp:LinkButton ID="lnkEdit" runat="server"  Text="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" CommandArgument='<%#Eval("ID")%>'
                                                                                Text="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                                                  <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle CssClass="lightblue center" />
                                                                <RowStyle CssClass="textbold center" />
                                                                <HeaderStyle CssClass="Gridheading center" ForeColor="White" />
                                                            </asp:GridView>
                                                        </td>
                                                        <td >
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" valign="top" class="center">
                                                            <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                    <td style="width: 5%"></td>
                                                                        <td style="width: 15%" class="left" align="right">
                                                                            <span class="textbold" ><b>&nbsp; &nbsp;&nbsp; No. of records found</b></span>&nbsp;</td>
                                                                        <td style="width: 25%" class="left">
                                                                            <asp:TextBox
                                                                                ID="txtTotalRecordCount" runat="server" Width="64px" CssClass="textboxgrey" ReadOnly="True"  ></asp:TextBox>
                                                                            <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                        <td style="width: 20%" class="center">
                                                                            <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                                ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                            </asp:DropDownList></td>
                                                                        <td style="width: 25%" class="left">
                                                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                                                                Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="width: 100%" valign="top">
                                                            <asp:HiddenField ID="hdRegion" runat="server" />
                                                            <asp:HiddenField ID="hdYear" runat="server" />
                                                             <asp:HiddenField ID="hdDeleteID" runat="server" />
                                                            &nbsp;</td>
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
