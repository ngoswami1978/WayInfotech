<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_Pay_ApprovalLevel.aspx.vb" Inherits="Incentive_INCSR_Pay_ApprovalLevel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Incentive::Search Approval Level</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
    <script language="javascript" type="text/javascript">
    
    function Edit(ID)
			{
				 window.location.href="INCUP_Pay_ApprovalLevel.aspx?Action=U&ID=" +ID
				 return false;
			}
			
	function Delete(ID)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
              document.getElementById('<%=hdID.ClientId%>').value  =ID
              return true;
		        
            }
            return false;
	}
	
	function History(ID)
			{
			
			 type = "../Popup/PUSR_Payment_ApprovalLevelHistory.aspx?Action=U&Aoffice=" +ID
			 window.open(type,"PayApprovalLevelHistory","height=600,width=800,top=30,left=20,scrollbars=1,status=1");                  
		    return false;
			}
			
    </script>
</head>
<body>
    <form id="frmApprovalLevel" runat="server"  defaultbutton="btnSearch">
    <div>
    <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Incentive-&gt;</span><span class="sub_menu">Payment&nbsp; Approval Level Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Payment Approval Level</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td style="width: 10%">
                                            </td>
                                            <td class="gap" colspan="2" style="text-align: center">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 222px">
                                                                                Aoffice</td>
                                                                            <td style="width: 308px">
                                                                                <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                    Width="175px" TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="6" AccessKey="a" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 222px">
                                                                                </td>
                                                                            <td style="width: 308px">
                                                                    </td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="6" AccessKey="n" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px; width: 222px;">
                                                             </td>
                                                                            <td style="width: 308px; height: 26px;">
                                                                    </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="r" /></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 222px;">
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                
                                                <asp:HiddenField ID="hdID" runat="server" />
                                                </td>
                                            <td style="width: 308px; height: 26px">
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="6" Text="Export" AccessKey="e" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td colspan="3" style="height: 26px">
                                                                                <asp:GridView EnableViewState="False" ID="gvContactType" runat="server"  AutoGenerateColumns="False" TabIndex="7" Width="80%" AllowSorting="True">
                                                                                <Columns>
                                                                                   <asp:BoundField DataField="AOFFICE" HeaderText="Aoffice" SortExpression="AOFFICE" />
                                                                                   <asp:BoundField DataField="LEVEL" HeaderText="Level" SortExpression="LEVEL" />
                                                                                                              
                                                            <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass ="LinkButtons"></asp:LinkButton>&nbsp;
                                                            &nbsp;   <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass ="LinkButtons"></asp:LinkButton> 
                                                                  &nbsp;   <asp:LinkButton ID="LnkHistory" runat="server" CommandName ="HistoryX" Text ="History" CssClass ="LinkButtons"></asp:LinkButton> 
                                                            
                                                                <asp:HiddenField ID="hdID" runat="server" Value='<%#Eval("AOFFICE")%>' />   
                                                             </ItemTemplate>
                                                           </asp:TemplateField>
                                                                                                        
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White"/>
                                                    
                                                 </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                            </td>
                                            <td colspan="3" >
                                             <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 35%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="70px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 20%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="7"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 25%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="7" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next" TabIndex="7">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                            </td>
                                        </tr>
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" style="text-align: center">
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
