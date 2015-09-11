<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_WorkOrder.aspx.vb" Inherits="ETHelpDesk_HDUP_WorkOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::ETrackers HelpDesk::Manage Work Order</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type ="text/javascript" src="../JavaScript/ETracker.js"></script>
<script language ="javascript" type ="text/javascript">
function  ShowAssigneeHis()
  {
//  var hdPtrsID=document.getElementById("hdOrderID").value.trim();
   var hdPtrsID=document.getElementById("hdHistoryOrderID").value.trim();
   var type="../ECOMMTrackerHelpDeskPopup/PUSR_AssigneeHistory.aspx?WO_Number="+hdPtrsID
      
    /*  if (window.showModalDialog) 
        {
        window.showModalDialog(type,null,'dialogWidth:870px;dialogHeight:600px;help:no;');       
        }
         else
            {  */
                     
            window.open(type,"aa",'height=600,width=900,top=30,left=20,scrollbars=1,status=1');   
             return false;  
          //  }
     }
     

     
      function  ShowWorkOrderHistory()
  {
  //var ptrsID=document.getElementById("hdOrderID").value.trim();
  
  var ptrsID=document.getElementById("hdHistoryOrderID").value.trim();

   var type="../ECOMMTrackerHelpDeskPopup/PUSR_EmployeeHistory.aspx?WO_Number="+ptrsID
      
  /*    if (window.showModalDialog) 
        {
        window.showModalDialog(type,null,'dialogWidth:870px;dialogHeight:600px;help:no;');       
        }
         else
            {  */
            window.open(type,"aaWOHistory",'height=600,width=900,top=30,left=20,scrollbars=1,status=1');     
             return false;
         //   }
     }
     
     function PopupAgencyPage()
{
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
  }
     
     
     
//code for closing and returning value
    function fnWOID()
    {debugger;
    try
    {
            if (document.getElementById("hdRWOId").value != "")
            {
                     if (window.opener.document.forms['form1']['txtWorkOrderNo']!=null)
                    { 
                    window.opener.document.forms['form1']['txtWorkOrderNo'].value=document.getElementById("hdRWONO").value;
                    window.opener.document.forms['form1']['hdWorkOrderNo'].value=document.getElementById("hdRWOId").value;
                     window.opener.document.forms['form1']['hdEnWorkOrderNo'].value=document.getElementById("hdEnRWOId").value;
                    var strStatus=document.getElementById("drpStatus").value;
                    var intStatusLength= window.opener.document.forms['form1']["ddlQueryStatus"].options.length;
	                  for (var intCount=0 ;intCount<intStatusLength;intCount++)
	                  {
    	                 var textStatus=window.opener.document.forms['form1']["ddlQueryStatus"].options[intCount].value ;
    	                 if (textStatus.split("|")[0]==strStatus)
	                     {
	                       window.opener.document.forms['form1']["ddlQueryStatus"].value=textStatus;
	                     }
	                 }
	                 
	                 
	                 
	                 
	                intLen=document.getElementById("ddlPageNumber").options.length;
                    for (intCn=0;intCn<intLen;intCn++)
                    {
                        if (intCn==0)
                        {
                            strData=document.getElementById("ddlPageNumber").options[intCn].value;    
                        }
                        else
                        {
                            strData=strData + "," + document.getElementById("ddlPageNumber").options[intCn].value;    
                        }
                    }
            
                      window.opener.document.forms['form1']['hdMultiWO'].value=strData;
	                 
                    window.close();
                     window.opener.fnMultiWOddl(document.getElementById("ddlPageNumber").value.split("|")[0]);
                    return false;
                    }    
            }
            else
            {
            window.close();
                return false;
            }
      }
        catch(err){}
    }
    
     function fnWOIDClose()
    {
    try
    {
            if (document.getElementById("hdRWOId").value != "")
            {
                 if (window.opener.document.forms['form1']['txtWorkOrderNo']!=null)
                { 
                window.opener.document.forms['form1']['txtWorkOrderNo'].value=document.getElementById("hdRWONO").value;
                window.opener.document.forms['form1']['hdWorkOrderNo'].value=document.getElementById("hdRWOId").value;
                 window.opener.document.forms['form1']['hdEnWorkOrderNo'].value=document.getElementById("hdEnRWOId").value;
                var strStatus=document.getElementById("drpStatus").value;
                var intStatusLength= window.opener.document.forms['form1']["ddlQueryStatus"].options.length;
	              for (var intCount=0 ;intCount<intStatusLength;intCount++)
	              {
    	             var textStatus=window.opener.document.forms['form1']["ddlQueryStatus"].options[intCount].value ;
    	             if (textStatus.split("|")[0]==strStatus)
	                 {
	                   window.opener.document.forms['form1']["ddlQueryStatus"].value=textStatus;
	                 }
	             }
              
                return false;
                }    
            }
    }
        catch(err){}
    }
    
</script>
</head>
<body style="font-size: 12pt; font-family: Times New Roman" >
    <form id="form1"  runat="server" defaultbutton="btnSave" >
        <table width="860px" align="left" class="border_rightred" style="height:486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">ETrackers HelpDesk-&gt;</span><span class="sub_menu">Manage Work Order</span></td>
                            <td class="right"><asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnWOID('1')" >Close</asp:LinkButton>
                                &nbsp; &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" colspan="2">
                                Manage Work Order&nbsp;
                            </td>
                            
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td class="textbold" style="width: 40px">
                                                                </td>
                                                                <td colspan="4" style="text-align: center">
                                                          <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False" ></asp:Label></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px;">
                                                                </td>
                                                                <td colspan="4"  class="subheading"> Agency Details
                                                                </td>
                                                                <td >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Agency Name</td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtAgencyName" CssClass="textboxgrey" MaxLength="50" runat="server" Width="464px" ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                    </td>
                                                                <td style="height: 25px">
                                                                    &nbsp;
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" TabIndex="4" Text="Save" Width="106px" AccessKey="s" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Address</td>
                                                                <td colspan="3" rowspan="2" valign="middle">
                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Height="37px" MaxLength="300"
                                                                        ReadOnly="True"  TextMode="MultiLine" Width="464px" TabIndex="20"></asp:TextBox><br />
                                                                </td>
                                                                <td style="height: 25px">
                                                                    &nbsp;
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" TabIndex="4" Text="New" Width="106px" AccessKey="s" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                </td>
                                                                <td style="height: 25px">
                                                                    &nbsp;
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="4" Width="106px" AccessKey="r" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    City</td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="40" ReadOnly="True" TabIndex="20"
                                                                        ></asp:TextBox></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Country</td>
                                                                <td style="width: 200px; height: 25px">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="40" ReadOnly="True" TabIndex="20"
                                                                        ></asp:TextBox></td>
                                                                <td style="height: 25px">
                                                                    &nbsp;
                                                                    <asp:Button ID="btnHistory" CssClass="button" runat="server" Text="History" TabIndex="4" Width="106px" OnClientClick ="return ShowWorkOrderHistory();" AccessKey="h" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Phone</td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" MaxLength="40" ReadOnly="True" TabIndex="20"
                                                                        ></asp:TextBox></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Fax</td>
                                                                <td style="width: 200px; height: 25px">
                                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" MaxLength="40"  ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                <td style="height: 25px">
                                                                    &nbsp;
                                                                    <asp:Button ID="btnAssigneeHistory" CssClass="button" runat="server" Text="Assignee History" TabIndex="4" Width="106px" OnClientClick ="return ShowAssigneeHis();" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    OfficeID</td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textboxgrey" MaxLength="40"  ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Online Status</td>
                                                                <td style="width: 200px; height: 25px">
                                                                    <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" MaxLength="40"  ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                <td style="height: 25px" >  &nbsp;
                                                                    <asp:Button ID="btnLTR" CssClass="button" runat="server" Text="LTR" TabIndex="4" Width="106px" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="gap">
                                                                </td>
                                                                <td >
                                                                </td>
                                                                <td >
                                                                </td>
                                                                <td >
                                                                </td>
                                                                <td style="width: 200px" >
                                                                </td>
                                                                <td >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 19px;">
                                                                </td>
                                                                <td colspan="4"  class="subheading" style="height: 19px">
                                                                    Work Order Details</td>
                                                                <td style="height: 19px" >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Work
                                                                    Order Number<span class="Mandatory">*</span></td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textbox" MaxLength="9" TabIndex="2"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Order Type<span class="Mandatory">*</span></td>
                                                                <td style="width: 200px; height: 25px">
                                                                    <asp:DropDownList ID="drpOrderType" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Severity<span class="Mandatory">*</span></td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:DropDownList ID="drpSeverity" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Follow Up<span class="Mandatory">*</span></td>
                                                                <td style="width: 200px; height: 25px">
                                                                    <asp:DropDownList ID="drpFollowUp" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Status<span class="Mandatory">*</span></td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                </td>
                                                                <td style="width: 200px; height: 25px">
                                                                </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 13px; width: 40px;">
                                                                </td>
                                                                <td class="textbold" style="height: 13px; width: 119px;">
                                                                    Open Date </td>
                                                                <td style="height: 13px; width: 214px;">
                                                                    <asp:TextBox ID="txtOpenDate"   runat="server" CssClass="textboxgrey" MaxLength="40" TabIndex="20" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                <td class="textbold" style="height: 13px; width: 116px;">
                                                                    Close Date
                                                                </td>
                                                                <td style="height: 13px; width: 200px;">
                                                                    <asp:TextBox ID="txtCloseDate" runat="server" CssClass="textboxgrey" MaxLength="40"  ReadOnly="True" TabIndex="20"></asp:TextBox>&nbsp;
                                                                         
                                                                        </td>
                                                                <td style="height: 13px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 2pt">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                    Order Title</td>
                                                                <td colspan="3" style="height: 13px">
                                                                    <asp:TextBox ID="txtOrderTitle" runat="server" CssClass="textbox" MaxLength="300"
                                                                        TabIndex="3" Width="462px" Height="66px" TextMode="MultiLine"></asp:TextBox></td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" style="height: 2pt">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                    Assigned To<span class="Mandatory">*</span></td>
                                                                <td style="width: 214px; height: 13px"><asp:DropDownList ID="drpAssigned" runat="server" CssClass="dropdownlist" TabIndex="3" 
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                </asp:DropDownList>
                                                                    
                                                                    </td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                    Assigned Date&nbsp;</td>
                                                                <td style="width: 200px; height: 13px">
                                                                    <asp:TextBox ID="txtAssignedDate" runat="server" CssClass="textboxgrey" MaxLength="40"  ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                        
                                                                        </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                    </td>
                                                                <td style="width: 214px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                </td>
                                                                <td style="width: 200px; height: 13px">
                                                                </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" colspan="4" style="height: 13px">
                                                                  <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                    <td style="width: 30%" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="19"
                                                                            Width="64px" ReadOnly="True" Text="0" Visible="True"></asp:TextBox></td>
                                                                    <td style="width: 25%" class="right">
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
                                                                </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                </td>
                                                                <td style="width: 214px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                </td>
                                                                <td style="width: 200px; height: 13px">
                                                                </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                <asp:HiddenField ID="hdOrderID" runat="server" />
                                                                <asp:HiddenField ID="hdEnRWOId" runat="server" />
                                                                
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 214px; height: 13px"><asp:HiddenField ID="hdAssignedTo" runat="server" />
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 13px"><asp:HiddenField ID="hdReqID" runat="server" />
                                                                <asp:HiddenField ID="hdEnReqID" runat="server" />
                                                                </td>
                                                                <td style="width: 200px; height: 13px"><asp:HiddenField ID="hdAssignedDate" runat="server" />
                                                                </td>
                                                                <td style="height: 13px">
                                                                <input type="hidden" runat="server" id="hdRWOId" style="width: 1px" />
                                                                 <input type="hidden" runat="server" id="hdRWONO" style="width: 1px" />
                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4"><asp:HiddenField ID="hdLCode" runat="server" />
                                                                <asp:HiddenField ID="hdHistoryOrderID" runat="server" />
                                                                 <asp:HiddenField ID="hdTotalNoOfRecords" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
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
      <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
    
      document.getElementById('<%=lblError.ClientId%>').innerText=''
    
    if(document.getElementById('<%=txtOrderNo.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtOrderNo.ClientId%>').value
            reg = new RegExp("^[0-9]+$"); 
            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='Work order number should contain only digits.'
                return false;

             }
        }
    else
    {
    document.getElementById('<%=lblError.ClientId%>').innerText ='Work order number is mandatory';
    return false;
    }
    
      //*********** Validating drpOrderType  *****************************
        var cboOrderType=document.getElementById('<%=drpOrderType.ClientId%>');
         if(cboOrderType.selectedIndex ==0)
        {
         document.getElementById('<%=lblError.ClientId%>').innerText ='Order type is mandatory.'
         return false;
            
        }
    
           //*********** Validating Severity  *****************************
        var cboSeverity=document.getElementById('<%=drpSeverity.ClientId%>');
         if(cboSeverity.selectedIndex ==0)
        {
         document.getElementById('<%=lblError.ClientId%>').innerText ='Severity is mandatory.'
         return false;
            
        }
        
          //*********** Validating drpFollowUp  *****************************
        var cboFollow=document.getElementById('<%=drpFollowUp.ClientId%>');
         if(cboFollow.selectedIndex ==0)
        {
         document.getElementById('<%=lblError.ClientId%>').innerText ='Follow Up is mandatory.'
         return false;
            
        }
        
        
          //*********** Validating drpStatus  *****************************
        var cboStatus=document.getElementById('<%=drpStatus.ClientId%>');
         if(cboStatus.selectedIndex ==0)
        {
         document.getElementById('<%=lblError.ClientId%>').innerText ='Status is mandatory.'
         return false;
            
        }
       
      
        //*********** Validating drpAssigned  *****************************
        var cboAssignee=document.getElementById('<%=drpAssigned.ClientId%>');
         if(cboAssignee.selectedIndex ==0)
        {
         document.getElementById('<%=lblError.ClientId%>').innerText ='Assignee is mandatory.'
         return false;
            
        }
       
        
//          //*********** Validating drpStatus  *****************************
//        var cboStatus=document.getElementById('<%=drpStatus.ClientId%>');
//         if(cboStatus.selectedIndex !=0)
//        {
//        
//        
//            if (cboStatus.options[cboStatus.selectedIndex].text == "Closed")
//            {
//                if (document.getElementById('<%=txtCloseDate.ClientId%>').value == "")
//                {
//                    document.getElementById('<%=lblError.ClientId%>').innerText ='Close date is mandatory.'
//                    return false;
//                }
//            }
//         
//            
//        }
       


         // End function
       return true; 
        
    }
    function ClearAssigneeDate()
    {
       if (document.getElementById('<%=drpAssigned.ClientId%>').value == document.getElementById('<%=hdAssignedTo.ClientId%>').value)
       {
       document.getElementById('<%=txtAssignedDate.ClientId%>').value = document.getElementById('<%=hdAssignedDate.ClientId%>').value
       }
       else
       {
        document.getElementById('<%=txtAssignedDate.ClientId%>').value =''
        return false;
       }
    
    }

    </script>
</body>
</html>
