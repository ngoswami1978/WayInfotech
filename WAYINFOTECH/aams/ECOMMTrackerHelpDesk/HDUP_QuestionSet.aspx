<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_QuestionSet.aspx.vb"
    Inherits="ETHelpDesk_HDUP_QuestionSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::HelpDesk::Manage QuestionSet</title>

    <script src="../JavaScript/ETracker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
// function chkSave()
//{
//    if(document.getElementById("drpMonth").selectedIndex==0)
//    {
//    document.getElementById("lblError").innerHTML="Select a valid Month.";
//    return false;
//    }

//}
  function ChangeControlStatus(id)
    {
      document.getElementById(id).readOnly=false;
      document.getElementById(id).className='textbox';
      return false;
     }
    function validateGrid()
    {
    
    if(document.getElementById("drpMonth").selectedIndex==0)
    {
    document.getElementById("lblError").innerHTML="Select a valid Month.";
    return false;
    }
    
   
     var gridID,colDrp,ColTxt;
     gridID=document.getElementById('<%=gvQuestions.ClientID%>');
     ColTxt=1;
       for(intcnt=1;intcnt<=gridID.rows.length-1;intcnt++)
       {
          var txtVal=gridID.rows[intcnt].cells[ColTxt].children[0].value;
          if(txtVal=='')
          {
            document.getElementById("lblError").innerHTML ="Question cannot be blank";
            //gridID.rows[intcnt].cells[1].children[0].focus();
            return false;
          }
       }
    }
    </script>

</head>
<body style="font-size: 12pt; font-family: Times New Roman">
    <form id="form1" defaultbutton ="btnSave" runat="server" >
        <div>
            <table width="840px" class="border_rightred left">
                <tr>
                    <td class="top">
                        <table width="100%" class="left">
                            <tr>
                                <td>
                                    <span class="menu">ETrackers HelpDesk-&gt;</span><span class="sub_menu">QuestionSet</span></td>
                            </tr>
                            <tr>
                                <td class="heading center" style="height: 20px">
                                    Manage Question Set</td>
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
                                                        <td style="width: 10%">
                                                        </td>
                                                        <td class="textbold" style="width: 10%">
                                                            Month<span class="Mandatory">* </span>
                                                        </td>
                                                        <td class="textbold" colspan="2">
                                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                TabIndex="1" Width="137px">
                                                                 <asp:ListItem Value="0">--Select One--</asp:ListItem>
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
                                                        <td class="textbold" style="width: 10%">
                                                            Year<span class="Mandatory">* </span>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                                position: relative; top: 0px" TabIndex="2" Width="72px">
                                                            </asp:DropDownList></td>
                                                        <td class="center" style="width: 15%">
                                                            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" style="position: relative" AccessKey="s" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold">
                                                        </td>
                                                        <td class="textbold" colspan="2">
                                                            &nbsp;</td>
                                                        <td class="textbold" style="width: 8%">
                                                        </td>
                                                        <td style="width: 22%">
                                                            &nbsp;</td>
                                                        <td class="center">
                                                            <asp:Button ID="btnNew" runat="server" CssClass="button" Style="position: relative"
                                                                TabIndex="4" Text="New" AccessKey="n" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 26px">
                                                        </td>
                                                        <td class="textbold" style="height: 26px">
                                                        </td>
                                                        <td class="ErrorMsg" colspan="2" style="height: 26px">
                                                            Field Marked * are Mandatory
                                                        </td>
                                                        <td class="textbold" style="width: 8%; height: 26px;">
                                                        </td>
                                                        <td style="height: 26px">
                                                        </td>
                                                        <td class="center" style="height: 26px">
                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5"
                                                                Style="position: relative" AccessKey="r" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold">
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td class="textbold" style="width: 4%">
                                                        </td>
                                                        <td class="textbold" style="width: 8%">
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="center">
                                                            &nbsp;<%-- <asp:Button ID="btnHistory" CssClass="button" runat="server" Text="History" TabIndex="5" style="position: relative" />--%></td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td colspan="5">
                                                            <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                Width="100%">
                                                                <Columns>
                                                               
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Question No">
                                                                        <ItemTemplate>
                                                                            <asp:Label  runat="server" ID="txtQuestionNo"  Text='<%#DataBinder.Eval(Container.DataItem,"QUESTION_NO")%>'  CssClass="textbox"></asp:Label>
                                                                             <asp:HiddenField ID="hdId" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Question">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox Width="450px" Height="30px" ID="txtQuestion"    ReadOnly="true" TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem,"QUESTION_TITLE")%>'    MaxLength="200" runat="server" CssClass="textboxgrey"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle CssClass="lightblue center" />
                                                                <RowStyle CssClass="textbold center" />
                                                                <HeaderStyle CssClass="Gridheading center" />
                                                            </asp:GridView>
                                                        </td>
                                                        <td class="center">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="width: 100%; height: 71px;" valign="top">
                                                            <asp:HiddenField ID="hdRegion" runat="server" />
                                                            <asp:HiddenField ID="hdYear" runat="server" />
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
