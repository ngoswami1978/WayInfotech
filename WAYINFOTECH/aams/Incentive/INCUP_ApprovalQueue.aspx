<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCUP_ApprovalQueue.aspx.vb"
    Inherits="Incentive_INCUP_ApprovalQueue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Incentive::Manage Approval Queue</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSave">
        <div>
            <table width="860px" align="left" height="486px" class="border_rightred">
                <tr>
                    <td valign="top">
                        <table width="100%" align="left">
                            <tr>
                                <td valign="top" align="left">
                                    <span class="menu">Incentive-&gt;</span><span class="sub_menu">Manage BC &nbsp;Approval Queue</span></td>
                            </tr>
                            <tr>
                                <td class="heading" align="center" valign="top">
                                    Manage BC Approval Queue</td>
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
                                                        <td class="textbold" style="width: 222px; text-align: center;">
                                                            Status<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana;">*</span></strong></td>
                                                        <td style="width: 308px">
                                                            <asp:DropDownList ID="ddlIncentiveStatus" runat="server" CssClass="dropdownlist"
                                                                 Width="175px" TabIndex="1">
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="6"
                                                                AccessKey="a" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 176px">
                                                        </td>
                                                        <td class="textbold" style="width: 222px">
                                                        </td>
                                                        <td style="width: 308px">
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6"
                                                                AccessKey="r" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 176px; height: 26px">
                                                        </td>
                                                        <td colspan="2" style="height: 26px" class="ErrorMsg">
                                                            &nbsp; Field Marked * are Mandatory</td>
                                                        <td style="height: 26px">
                                                            <asp:Button ID="btnSideLetter" CssClass="button" runat="server" Text="Side Letter"
                                                                TabIndex="6" AccessKey="r" Visible ="false"  /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 176px; height: 26px">
                                                        </td>
                                                        <td colspan="3" style="height: 26px">
                                                            <asp:GridView EnableViewState="False" ID="gvContactType" runat="server" AutoGenerateColumns="False"
                                                                TabIndex="7" Width="80%">
                                                                <Columns>
                                                                    <asp:BoundField DataField="BCLEVEL" HeaderText="Level" />
                                                                    <asp:BoundField DataField="EMPLOYEENAME" HeaderText="Employee" />
                                                                    <asp:BoundField DataField="APPROVAL_STATUS_NAME" HeaderText="Status" />
                                                                </Columns>
                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                <RowStyle CssClass="textbold" />
                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align: center">
                                                            <input id="hdID" runat="server" style="width: 6px" type="hidden" />
                                                            <input id="hdOldStatusID" runat="server" style="width: 6px" type="hidden" />
                                                            &nbsp;&nbsp;<input id="hdPrevBCID" runat="server" style="width: 6px" type="hidden" /></td>
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

    <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
 
      document.getElementById('<%=lblError.ClientId%>').innerText=''
      
        var cboGroup=document.getElementById('ddlIncentiveStatus');
   if(cboGroup.selectedIndex ==0)
        {
         document.getElementById('lblError').innerText ='Status is mandatory.'
         cboGroup.focus();
         return false;
            
        }
      
      
    
       return true; 
        
    }
    /*
    function PopupPageApprovalLevel(id,ctrlid)
        {
        
             if (id=="1")
             {
                
                var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                document.getElementById("hdCtrlId").value = ctrlid
                if (strEmployeePageName!="")
                {
                    type = "../Setup/" + strEmployeePageName+ "?Popup=T&ctrlId="+ctrlid;
                 
                   //type = "../Setup/MSSR_Employee.aspx?Popup=T&Dept=Training&ctrlId="+ctrlid;
   	                window.open(type,"aaApprovalLevelEmp","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	
   	                return false;
   	            }
             }
        }
        
*/
    
  
    </script>

</body>
</html>
