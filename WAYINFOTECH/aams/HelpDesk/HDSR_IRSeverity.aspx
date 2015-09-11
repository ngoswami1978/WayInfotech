<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_IRSeverity.aspx.vb" Inherits="HelpDesk_HDSR_IRSeverity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Seach IR Severity </title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<script language="javascript" type="text/javascript">
    
   
      function EditFunction(IRSeverityID)
    {    
        window.location.href="HDUP_IRSeverity.aspx?Action=U&IRSeverityID="+IRSeverityID;      
        return false;
     }
    function DeleteFunction(IRSeverityID)
    {  
        if (confirm("Are you sure you want to delete?")==true)
        {     
        document.getElementById("hdDeleteFlag").value=IRSeverityID;
        }
        else
        {
        document.getElementById("hdDeleteFlag").value="";
        return false;
        }
    }
</script>
<body>
    <form id="form1" runat="server" defaultfocus="txtIRSeverity" defaultbutton="btnSearch">
        
        
     
     
     <table width="860px" align="left" height="486px" class="border_rightred">
                <tr>
                    <td valign="top">
                        <table width="100%" align="left">
                            <tr>
                                <td valign="top" align="left">
                                    <span class="menu">Help Desk-><span class="sub_menu">IR Severity</span><span style="font-size: 12pt"><span
                                        style="font-family: Times New Roman; width: 50%;"> <span> </span></span></span></span>
                                </td>
                            </tr>
                            
                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                <td class="heading" align="center" valign="top" style="height: 10px; width: 100%;">
                                Search IR <span style="font-family: Microsoft Sans Serif">Severity</span><span style="font-size: 12pt"><span
                                        style="font-family: Times New Roman; width: 50%;"></span></span></td>
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
                                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 10%">
                                                                    </td>
                                                                    <td align="left" style="width: 10%">
                                                                        IR Severity</td>
                                                                    <td style="width: 10%;">
                                                                    <asp:TextBox ID="txtIRSeverity" runat="server" CssClass="textbold" TabIndex="1"
                                                                        Width="224px" MaxLength="25"></asp:TextBox></td>
                                                                    <td style="width: 40%;" class="center">
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="3" AccessKey="s" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 24px">
                                                                    </td>
                                                                    <td style="width: 10%; height: 24px;" align="left">
                                                                        </td>
                                                                    <td class="textbold" style="height: 24px; width: 10%;">
                                                                        </td>
                                                                    <td style="height: 24px" class="center">
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="4" AccessKey="n" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td style="width: 10%;" align="left">
                                                                        </td>
                                                                    <td style="height: 22px; width: 10%;">
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
                                                                    <td style="width: 10%">
                                                                        &nbsp;</td>
                                                                    <td class="center">
                                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" TabIndex="6" AccessKey="e" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="height: 4px">
                                                                    </td>
                                                                </tr>
                                                                <tr >
                                                                <td></td>
                                                                    <td colspan="3" style="width:100%">
                                                                       <asp:GridView ID="grdvIRSeverity" AllowSorting="true" HeaderStyle-ForeColor="white" BorderWidth="1" BorderColor="#d4d0c8" runat="server"
                                                                        AutoGenerateColumns="False" Width="58%" EnableViewState="false">
                                                                        <Columns>
                                                                            <asp:TemplateField SortExpression="HD_IR_SEV_NAME" HeaderText="IR Severity">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("HD_IR_SEV_NAME")%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp; 
                                                                                    <asp:LinkButton ID="linkDelete" runat="server" Text="Delete" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                                                    <asp:HiddenField ID="hdField" runat="server" Value='<%#Eval("HD_IR_SEV_ID") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                                        <RowStyle CssClass="textbold" />
                                                                    </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" height="12">
                                                                    </td>
                                                                </tr>
                                                                
                                                                  
                                            <tr>    
                                                                                     
                                                    <td valign ="top" colspan="4"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 200px" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                

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
