<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_ApprovalQueue.aspx.vb" Inherits="Incentive_INCSR_ApprovalQueue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Incentive::Business Case Approval Queue</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    
    <script language="javascript" type="text/javascript">
    
      function Edit(parBC_ID,parPREV_BC_ID)
    {           
          window.location ="INCUP_ApprovalQueue.aspx?Action=U&BC_ID=" + parBC_ID+"&PREV_BC_ID="+parPREV_BC_ID;       
          return false;
    }   
    
         
     function  History(parBC_ID,parPREV_BC_ID)
  {
     
   var type="INCHR_ApprovalQueue.aspx?BC_ID=" + parBC_ID + "&PREV_BC_ID=" + parPREV_BC_ID
    window.open(type,"aaHistory",'height=600,width=900,top=30,left=20,scrollbars=1,status=1');     
    return false;
  }
  
     function DetailsFunction(BCaseID, ChainCode)
{
var type;
//type = "../Incentive/INCSR_BacseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID;
type = "../Incentive/INCUP_BacseDetails.aspx?Action=U&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID;

window.open(type,"IncDetails","height=630,width=1000,top=30,left=20,scrollbars=1,status=1,resizable=1");
// window.location ="MSSR_BcaseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID;
return false;
}


  function PopupAgencyGroupForIncentive()
{
            var type;
            type = "../Setup/MSSR_ManageAgencyGroup.aspx?Popup=T" ;
   	        window.open(type,"IncAppQ","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;

}       
  
    </script>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server"  defaultfocus ="txtChainCode" >
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Incentive-></span><span class="sub_menu">Business Case Approval Queue</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Business Case Approval Queue</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold" style="height: 22px">
                                                                    Group Name</td>
                                                                <td width="63%" colspan ="3" style="height: 22px" >
                                                                     <asp:TextBox ID="txtGroupName" runat="server" CssClass="textfield" TabIndex="2" MaxLength="40"  Width="485px" EnableViewState="False"></asp:TextBox><img src="../Images/lookup.gif" onclick="javascript:return PopupAgencyGroupForIncentive();" id="ImgAGroup" style="cursor:pointer;"  alt="" runat ="server"  /></td>
                                                              
                                                                <td width="18%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="7"  AccessKey="A"/></td>
                                                            </tr>
                                                            
                                                           
                                                                                                                      
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                          <tr>
                                                                <td width="6%" class="textbold">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Chain Code</td>
                                                                <td width="30%" >
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textfield" TabIndex="1" MaxLength="6" EnableViewState="False"></asp:TextBox></td>
                                                                <td width="12%" class="textbold" >
                                                                    Aoffice</td>
                                                                <td width="21%"><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlAoffice" runat="server" CssClass="dropdown" TabIndex="5">
                                                                </asp:DropDownList></td>
                                                                <td width="18%">
                                                                     <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="9"  AccessKey="E"/></td>
                                                            </tr>  
                                                            
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Expected Date</td>
                                                                <td style="height: 22px">
                                                                    <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                                        TabIndex="2" Width="132px"></asp:TextBox><img id="imgStartDateFrom" alt="" src="../Images/calender.gif"
                                                                            style="cursor: pointer" tabindex="2" title="Date selector" />
                                                                             <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtStartDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                            </script>
                                                                            </td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Valid Till Date</td>
                                                                <td style="height: 22px">
                                                                    <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" MaxLength="10"
                                                                        TabIndex="2" Width="132px"></asp:TextBox><img id="imgStartDateTo" alt="" src="../Images/calender.gif"
                                                                            style="cursor: pointer" tabindex="2" title="Date selector" />
                                                                            
                                                                             <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtStartDateTo.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                            </script>
                                                                            </td>
                                                                <td style="height: 22px">
                                                                   <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="10" AccessKey="R" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Status</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlApprovalStatus" runat="server" CssClass="dropdown" TabIndex="5">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Received Date&nbsp;</td>
                                                                <td style="height: 22px">
                                                                    <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="textbox" MaxLength="10" TabIndex="2"
                                                                        Width="132px"></asp:TextBox><img id="imgReceivedDate" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                            tabindex="2" title="Date selector" />
                                                                            <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtReceivedDate.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgReceivedDate",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                            </script>
                                                                            </td>
                                                                <td style="height: 22px">
                                                                   </td>
                                                            </tr>
                                                            <tr height="20px" >
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                       
                                                                <td colspan="6" height="4">
                                                                    <asp:GridView ID="gvApprovalQueue" runat="server" AutoGenerateColumns="False" 
                                                                     AllowSorting ="true"  HeaderStyle-ForeColor="White"     Width="100%" TabIndex="10" EnableViewState="False">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code" SortExpression="CHAIN_CODE" ItemStyle-Width="25px" />
                                                                            <asp:BoundField DataField="GROUPNAME" HeaderText="Group Name" SortExpression="GROUPNAME" ItemStyle-Width="100px"/>
                                                                            <asp:BoundField DataField="AOFFICE" HeaderText="Aoffice" SortExpression="AOFFICE" ItemStyle-Width="25px" />
                                                                            <asp:BoundField DataField="STARTDATE" HeaderText="Expected Date" SortExpression="STARTDATE" ItemStyle-Width="70px" />
                                                                            <asp:BoundField DataField="ENDDATE" HeaderText="Valid Date" SortExpression="ENDDATE" ItemStyle-Width="70px" />
                                                                            <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" ItemStyle-Width="70px" />
                                                                            <asp:BoundField DataField="REC_DATE" HeaderText="Received Date" SortExpression="REC_DATE" ItemStyle-Width="70px" />
                                                                            <asp:BoundField DataField="INC_TYPE_NAME" HeaderText="Incentive Type" SortExpression="INC_TYPE_NAME" ItemStyle-Width="100px" />
                                                                                  
                                                                            <asp:TemplateField HeaderText="Action">                                                                                
                                                                                <ItemTemplate>            
                                                                                <asp:LinkButton ID="lnkBusinessCase" runat="server" CommandName ="BCaseX" Text ="BusinessCase" CssClass="LinkButtons"></asp:LinkButton>&nbsp;                                                                      
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons" Visible ="false" ></asp:LinkButton>&nbsp;
                                                                            <asp:LinkButton ID="lnkHistory" runat="server" CommandName ="HistoryX" Text ="History" CssClass="LinkButtons" Visible ="true"  ></asp:LinkButton>&nbsp;
                                                                            <asp:HiddenField ID="hdBCID" runat="server" Value='<%#Eval("BC_ID")%>' />   
                                                                            <asp:HiddenField ID="hdPrevBCID" runat="server" Value='<%#Eval("PREVIOUS_BC_ID")%>' />   
                                                                            <asp:HiddenField ID="hdChainCode" runat="server" Value='<%#Eval("CHAIN_CODE")%>' />   
                                                                            
                                                                                  </ItemTemplate>
                                                                                  <ItemStyle Width="100px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" />
                                                                    </asp:GridView>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
                                                                </td>
                                                            </tr>
                                                               <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                </tr>
                                                 <tr>
                                                    <td colspan="6" style="height: 44px" >
                                                        <asp:HiddenField ID="hdID" runat="server" />
                                                        &nbsp;
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
      document.getElementById('lblError').innerText=''
      
       if(document.getElementById('<%=txtChainCode.ClientId%>').value !='')
        {         
           var strValue = document.getElementById('<%=txtChainCode.ClientId%>').value
           reg = new RegExp("^[0-9]+$"); 
           if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Chain code should contain only digits.'
                document.getElementById('<%=txtChainCode.ClientId%>').focus()
                return false;
           }
           else
           {
                if(document.getElementById('<%=txtChainCode.ClientId%>').value =='0') 
                {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='Chain code should be greater than zero.'
                    document.getElementById('<%=txtChainCode.ClientId%>').focus()
                    return false;
               }             
           
           }
        }
      
     
        //      Checking txtOpenDateFrom .
        if(document.getElementById('txtStartDateFrom').value != '')
        {
            if (isDate(document.getElementById('txtStartDateFrom').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerText = "Expected date is not valid.";			
	           document.getElementById('txtStartDateFrom').focus();
	           return(false);  
            }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('txtStartDateTo').value != '')
        {
            if (isDate(document.getElementById('txtStartDateTo').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerText = "Valid till date is not valid.";			
	           document.getElementById('txtStartDateTo').focus();
	           return(false);  
            }
        }   
        
         if(document.getElementById('txtReceivedDate').value != '')
        {
            if (isDate(document.getElementById('txtReceivedDate').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerText = "Received date is not valid.";			
	           document.getElementById('txtReceivedDate').focus();
	           return(false);  
            }
        }           
        
   if (compareDates(document.getElementById('txtStartDateFrom').value,"d/M/yyyy",document.getElementById('txtStartDateTo').value,"d/M/yyyy")==1)
       { 
            document.getElementById('lblError').innerText ='Valid till date should be greater than or equal to expected date.'
            return false;
       }
      
       return true; 
        
    }
   
    </script>
</body>
</html>
