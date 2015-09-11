<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_WorkOrderSeverity.aspx.vb" Inherits="ETHelpDesk_HDSR_WorkOrderSeverity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>AAMS::HelpDesk::Search Work Order Severity</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    
    function Edit(SeverityID)
			{
				 window.location.href="HDUP_WorkOrderSeverity.aspx?Action=U&SeverityID=" +SeverityID
				 return false;
			}
			
	function Delete(SeverityID)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
//                var SeverityName = document.getElementById('<%=txtSeverity.ClientId%>').value      
//                window.location.href="HDSR_WorkOrderSeverity.aspx?Action=D&SeverityID=" +SeverityID+"&SeverityName="+SeverityName
		       document.getElementById('<%=hdID.ClientId%>').value  =SeverityID
                return true;
            }
            return false;
	}
			
    </script>
</head>
<body style="font-size: 12pt; font-family: Times New Roman">
    <form id="form1" runat="server" defaultbutton="btnSearch">
      <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ETrackers HelpDesk-&gt;</span><span class="sub_menu">Search Work Order Severity</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Order Severity</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td style="width: 176px">
                                            </td>
                                            <td colspan="2" style="text-align: center" class="gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Severity</td>
                                                                            <td style="width: 308px">
                                                                    <asp:TextBox ID="txtSeverity" runat="server" CssClass="textbox" Width="208px" MaxLength="25" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                            <td>
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                </td>
                                                                            <td style="width: 308px">
                                                                    </td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="n" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px">
                                                             </td>
                                                                            <td style="width: 308px; height: 26px;">
                                                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                                                <asp:HiddenField ID="hdID" runat="server" />
                                                                                </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="r" /></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px">
                                            </td>
                                            <td style="width: 308px; height: 26px">
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px">
                                            </td>
                                            <td style="width: 308px; height: 26px">
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 26px">
                                                                                <asp:GridView EnableViewState="false" ID="gvSeverity" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" AllowSorting="True">
                                                                                <Columns>
                                                                                   <asp:BoundField DataField="WO_SEVERITY_NAME" HeaderText="Severity" SortExpression="WO_SEVERITY_NAME" />
                                                                                                              
                                                            <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass ="LinkButtons"></asp:LinkButton>&nbsp;
                                                               <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass ="LinkButtons"></asp:LinkButton> 
                                                                
                                                                <asp:HiddenField ID="hdSeverityID" runat="server" Value='<%#Eval("WO_SEVERITY_ID")%>' />   
                                                             </ItemTemplate>
                                                           </asp:TemplateField>
                                                                                                        
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                    
                                                 </asp:GridView>
                                                 <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 20%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 10%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 15%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
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
    </form>
     <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtSeverity.ClientId%>').value !='')
        {
            var strValue = document.getElementById('<%=txtSeverity.ClientId%>').value
            reg = new RegExp("^[a-zA-Z ]+$"); 

            if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Severity should contain only alphabets.'
                return false;

            }
        }
       
       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtSeverity").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
    </script>
</body>

</html>
