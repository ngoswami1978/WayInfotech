<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOHDSR_PtrFollowUp.aspx.vb" Inherits="BOHelpDesk_HDSR_PtrFollowUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Search PTR Follow up</title>
    <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
    
    function EditFunction(CheckBoxObj)
   {           
          window.location.href="BOHDUP_PtrFollowUp.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
        function InsertPtrAssignee()
        {
       window.location.href="BOHDUP_PtrFollowUp.aspx?Action=I|";
        return false;
        }
        
//    function DeleteFunction(PtrID)
//    {   
//        if (confirm("Are you sure you want to delete?")==true)
//        {   
//        
//          window.location.href="HDSR_PtrFollowUp.aspx?Action=D|" + PtrID + "|"+ document.getElementById("<%=txtFollowup.ClientID%>").value;                         
//          return false;
//        }
//    }
    
    
        function DeleteFunction(FollowUpID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteFlag").value=FollowUpID;
           return true;
           }
           else
           {
                document.getElementById("hdDeleteFlag").value="";
                 return false;
           }
        }
        
    
//       function Reset()
//    {
//        
//        document.getElementById("lblError").innerHTML="";    
//        document.getElementById("txtFollowup").value=""; 
//        if (document.getElementById("grdvPtrFollowUp")!=null) 
//        document.getElementById("grdvPtrFollowUp").style.display ="none"; 
//        document.getElementById("txtFollowup").focus(); 
//                
//        return false;
//    }
   
    
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
           
        <table width="860px" align="left" height="486px" class="border_rightred">
                <tr>
                    <td valign="top">
                        <table width="100%" align="left">
                            <tr>
                                <td valign="top" align="left">
                                    <span class="menu">Back Office Help Desk-><span class="sub_menu">PTR Follow Up</span><span style="font-size: 12pt;
                                        font-family: Times New Roman"> </span></span>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt; font-family: Times New Roman">
                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                <td class="heading" align="center" valign="top" style="height: 10px">
                                    PTR Follow Up</td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="100%" class="redborder">
                                                <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="textbold" style="height: 28px; width: 100%" colspan="4" valign="top">
                                                            <table width="100%" border="0" align="left" cellpadding="1" cellspacing="1">
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 10%">
                                                                    </td>
                                                                    <td align="left" style="width: 20%">
                                                                        Follow Up</td>
                                                                    <td style="width: 20%;">
                                                                       
                                                                            <asp:TextBox ID="txtFollowup" runat="server" CssClass="textfield" TabIndex="1"
                                                                                Width="177px" MaxLength="2"></asp:TextBox></td>
                                                                    <td style="width: 40%;" class="center">
                                                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td style="height: 22px" align="left">
                                                                        </td>
                                                                    <td class="textbold" style="height: 22px; width: 108px;">
                                                                        </td>
                                                                    <td style="height: 22px" class="center">
                                                                            <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="4" AccessKey="n" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td style="height: 22px" align="left">
                                                                        </td>
                                                                    <td style="height: 22px; width: 108px;">
                                                                        </td>
                                                                    <td style="height: 22px" class="center">
                                                                            <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="5" AccessKey="r" /></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td colspan="2">
                                                                    </td>
                                                                    <td style="width: 108px">
                                                                        &nbsp;</td>
                                                                    <td class="center">
                                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" TabIndex="6" AccessKey="e" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="height: 4px">
                                                                    </td>
                                                                </tr>
                                                                <tr >
                                                                    <td colspan="4" style="width:100%">
                                                                        &nbsp;<asp:GridView ID="grdvPtrFollowUp" BorderWidth="1" AllowSorting="true" HeaderStyle-ForeColor="white" BorderColor="#d4d0c8" runat="server"
                                                                                AutoGenerateColumns="False" Width="100%" EnableViewState="false">
                                                                              <Columns>
                                                                                    <asp:TemplateField SortExpression="HD_PTR_FOLLOWUP_NAME" HeaderText="Follow Up Name">
                                                                                        <itemtemplate>
                                                                                           <%#Eval("HD_PTR_FOLLOWUP_NAME")%> 
                                                                                    </itemtemplate>
                                                                                        <itemstyle horizontalalign="Left" />
                                                                                        <headerstyle horizontalalign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                                            <asp:LinkButton ID="linkDelete" runat="server" Text="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                                                             <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">
                                                                                                Delete</a>--%>
                                                                                                <asp:HiddenField ID="hdField" runat="server" Value='<%#Eval("HD_PTR_FOLLOWUP_ID") %>' />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" />
                                                                                <RowStyle CssClass="textbold" />
                                                                            </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" height="12">
                                                                    </td>
                                                                </tr>
                                                                
                                                                  <!-- code for paging----->
                                            <tr>   
                                                    <td valign ="top" colspan="4"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px; height: 29px;" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 200px; height: 29px;" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px; height: 29px;" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px; height: 29px;" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                    
                                                </tr>
                                                
            <!-- code for paging----->
                                                            </table>
                                                            <br />
                                                        </td>
                                                        <td width="18%" rowspan="1" valign="top">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="textbold" colspan="5">
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                                        <td colspan="6" style="height: 6px">
                                                            &nbsp;<asp:HiddenField ID="hdDeleteFlag" runat="server" />
                                                            <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server" ></asp:TextBox>
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
                <tr>
                    <td valign="top" style="height: 6px">
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>