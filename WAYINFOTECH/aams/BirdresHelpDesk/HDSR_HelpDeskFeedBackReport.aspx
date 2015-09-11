<%@ Page Language="VB"  EnableEventValidation ="false"  AutoEventWireup="false" CodeFile="HDSR_HelpDeskFeedBackReport.aspx.vb" Inherits="BirdresHelpDesk_HDSR_HelpDeskFeedBackReport" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>AAMS::HelpDesk::Search FeedBack</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<script language ="javascript" type ="text/javascript">

  
function PopupAgencyPage()
{
          var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T&HelpDeskType=BR";
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
}



			
	
</script>
    </head>
<body>
    <form id="form1"  defaultfocus="txtAgencyName" runat="server" defaultbutton="btnSearch">
    <table>
    <tr>
    <td>
   
           <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                     <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Birdres HelpDesk -&gt;</span><span class="sub_menu">FeedBack Report Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search FeedBack Report</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" /></td>
                                            <td class="textbold"> Agency Name</td>                                                                               
                                            <td colspan="3">
                                            <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2" Width="528px"></asp:TextBox>
                                            <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPageBR(1)"  src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 12%;" class="center">
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width:3%">
                                            </td>
                                            <td class="textbold" style="width:15%">
                                                OfficeID</td>
                                            <td style="width: 27%">
                                                <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textbox" MaxLength="25" TabIndex="2" Width="170px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 15%">
                                                FeedBack Id</td>
                                            <td style="width: 26%">
                                                <asp:TextBox ID="txtFeedBackID" runat="server" CssClass="textbox" MaxLength="9" TabIndex="2"
                                                    Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                            <td class="center" style="width: 13%">
                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" /></td>
                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="textbold" >
                                                                                LTR No.</td>
                                                                            <td style="width:27%">             
                                                                                             <asp:TextBox ID="txtLTRNo" runat="server" CssClass="textbox" Width="170px" MaxLength="9" onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox></td>
                                                                            <td class="textbold" style="width:15%">     Logged By</td>
                                                                            <td style="width:26%">
                                                                                <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                                                    Width="170px"></asp:TextBox></td>
                                                                            <td style="width:13%" class="center"> 
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" /></td>
                                                                        </tr>
                                        <tr>
                                            <td><input id="hdCallCallerName" runat="server" style="width: 1px" type="hidden" /></td>
                                            <td class="textbold">
                                                Caller Name</td>
                                            <td colspan="3">
                                                                       
                                                <asp:TextBox ID="txtExecutiveName" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2" Width="528px"></asp:TextBox></td>
                                            <td class="center" style="width: 13%">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td><input id="hdCallAssignedTo" runat="server" style="width: 1px" type="hidden" /></td>
                                            <td class="textbold">
                                                FeedBack Status</td>
                                            <td style="width: 27%">
                                                <asp:DropDownList ID="drpFeedBackStatus" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                    Width="174px">
                                                </asp:DropDownList></td>
                                            <td class="textbold" style="width: 15%">
                                                Assigned To</td>
                                            <td style="width: 26%">
                                                <asp:TextBox ID="txtAssignedTo" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                    Width="170px"></asp:TextBox>
                                                <img id="Img1" runat="server" alt="Select & Add Employee" onclick="PopupPage(2)"  src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td class="center" style="width: 13%">
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>  </td> <td class="textbold" >   Date From<span class="Mandatory">*</span> </td>    <td>
                                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox"                                                     TabIndex="20" Width="170px"></asp:TextBox>
                                                <img id="imgDateFrom" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                                                                                                                      });
                                                                                                  </script></td><td class="textbold" > Date To <span class="Mandatory">*</span> </td><td>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox" 
                                TabIndex="20" Width="170px"></asp:TextBox>
                            <img id="imgDateTo" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                                                                                                                      });
                                                                                                  </script></td>
                                            <td>
                                            </td>
                                        </tr>   
                                          <tr>
                                                                <td >
                                                                </td>
                                                                <td class="textbold" >
                                                                    HelpDesk Status</td>
                                                                <td >
                                                                    <asp:CheckBox ID="chkHelpDeskStatus" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                                                <td class="textbold" >
                                                                    Technical Status</td>
                                                                <td >
                                                                    <asp:CheckBox ID="chkTechnicalStatus" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                                                <td >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                </td>
                                                                <td class="textbold" >
                                                                    Sales status</td>
                                                                <td >
                                                                    <asp:CheckBox ID="chkSalesstatus" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                                                <td class="textbold" >
                                                                    Training Status</td>
                                                                <td >
                                                                    <asp:CheckBox ID="chkTrainingStatus" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                </td>
                                                                <td class="textbold" >
                                                                    Product Status</td>
                                                                <td >
                                                                    <asp:CheckBox ID="chkProductStatus" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                                                <td class="textbold" >Critical</td>
                                                                <td ><asp:CheckBox ID="chkCritical" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                                                <td >
                                                                </td>
                                                            </tr>                        
                                        <tr>
                                            <td ></td>
                                            <td >       
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>    <td  colspan="4">
                                                <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true"/>
                                                <input id="hdQuestionSet10" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet2" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet3" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet4" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet5" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet6" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet7" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet8" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet9" runat="server" style="width: 1px" type="hidden"/>
                                                <input id="hdQuestionSet1" runat="server" style="width: 1px" type="hidden"/>
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
    <tr><td class="top" >
<table width="100%">

<tr>
<td class="redborder top">
 <asp:GridView ID="gvOrder" runat="server" Width="3500px"  AutoGenerateColumns="False" TabIndex="19"  EnableViewState="False"  AllowSorting="True" >
           <Columns>
           
              
              <asp:BoundField DataField="QUESTION1" SortExpression="QUESTION1" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION2" SortExpression="QUESTION2" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION3" SortExpression="QUESTION3" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION4" SortExpression="QUESTION4" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION5" SortExpression="QUESTION5" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION6" SortExpression="QUESTION6" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION7" SortExpression="QUESTION7" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION8" SortExpression="QUESTION8" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION9" SortExpression="QUESTION9" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField DataField="QUESTION10" SortExpression="QUESTION10" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              
              
              <asp:BoundField HeaderText="Suggestion For Helpdesk" DataField="SUGGESTION_HELPDESK" SortExpression="SUGGESTION_HELPDESK" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Suggestion For Technical" DataField="SUGGESTION_TECHNICAL" SortExpression="SUGGESTION_TECHNICAL" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Suggestion For Sales" DataField="SUGGESTION_SALES" SortExpression="SUGGESTION_SALES" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Suggestion For Training" DataField="SUGGESTION_TRAINING" SortExpression="SUGGESTION_TRAINING" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Suggestion For Products" DataField="SUGGESTION_PRODUCT" SortExpression="SUGGESTION_PRODUCT" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Station" DataField="STATION" SortExpression="STATION" >
                  <ItemStyle Width="2%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Surveyor" DataField="SURVEYOR" SortExpression="SURVEYOR" >
                  <ItemStyle Width="2%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="LTR No." DataField="LTRNo" SortExpression="LTRNo" >
                  <ItemStyle Width="2%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Date" DataField="DATE" SortExpression="DATE" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Caller Name" DataField="EXECUTIVENAME" SortExpression="EXECUTIVENAME" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
                 <asp:BoundField HeaderText="Call Assigned To" DataField="CALLASSIGNEDTO" SortExpression="CALLASSIGNEDTO" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
                         
              <asp:BoundField HeaderText="Travel Agency" DataField="TRAVELAGENCY" SortExpression="TRAVELAGENCY" >
                  <ItemStyle Width="4%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="OfficeId" DataField="OFFICEID" SortExpression="OFFICEID" >
                  <ItemStyle Width="2%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Forwarded Date(HOD)" DataField="INFOTOHOD" SortExpression="INFOTOHOD" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="HelpDesk Action Taken" DataField="ACTIONTAKEN_HELPDESK" SortExpression="ACTIONTAKEN_HELPDESK" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Technical Action Taken" DataField="ACTIONTAKEN_TECHNICAL" SortExpression="ACTIONTAKEN_TECHNICAL" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Sales Action Taken" DataField="ACTIONTAKEN_SALES" SortExpression="ACTIONTAKEN_SALES" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Training Action Taken" DataField="ACTIONTAKEN_TRAINING" SortExpression="ACTIONTAKEN_TRAINING" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Product Action Taken" DataField="ACTIONTAKEN_PRODUCT" SortExpression="ACTIONTAKEN_PRODUCT" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Helpdesk Status" DataField="STATUS_HELPDESK" SortExpression="STATUS_HELPDESK" >
                  <ItemStyle Width="2%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Technical Status" DataField="STATUS_TECHNICAL" SortExpression="STATUS_TECHNICAL" >
                  <ItemStyle Width="2%" />
              </asp:BoundField>
               
             <asp:BoundField HeaderText="Sales Status" DataField="STATUS_SALES" SortExpression="STATUS_SALES" >
                 <ItemStyle Width="2%" />
             </asp:BoundField>
              <asp:BoundField HeaderText="Training Status" DataField="STATUS_TRAINING" SortExpression="STATUS_TRAINING" >
                  <ItemStyle Width="2%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Product Status" DataField="STATUS_PRODUCT" SortExpression="STATUS_PRODUCT" >
                  <ItemStyle Width="2%" />
              </asp:BoundField>
              
              <asp:BoundField HeaderText="Assigned To(Helpdesk)" DataField="ASSIGNEDTO_HELPDESK" SortExpression="ASSIGNEDTO_HELPDESK" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Assigned To(Technical)" DataField="ASSIGNEDTO_TECHNICAL" SortExpression="ASSIGNEDTO_TECHNICAL" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
               
             <asp:BoundField HeaderText="Assigned To(Sales)" DataField="ASSIGNEDTO_SALES" SortExpression="ASSIGNEDTO_SALES" >
                 <ItemStyle Width="3%" />
             </asp:BoundField>
              <asp:BoundField HeaderText="Assigned To(Training)" DataField="ASSIGNEDTO_TRAINING" SortExpression="ASSIGNEDTO_TRAINING" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Assigned To(Product)" DataField="ASSIGNEDTO_PRODUCT" SortExpression="ASSIGNEDTO_PRODUCT" >
                  <ItemStyle Width="3%" />
              </asp:BoundField>
                           

            </Columns>
            <AlternatingRowStyle CssClass="lightblue" Wrap="False" />
            <RowStyle CssClass="textbold" HorizontalAlign="Left" />
            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left"  ForeColor="White" />
        </asp:GridView>

  <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="20%">
  <table border="0" cellpadding="0" cellspacing="0" width="100%">
                  <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                      <td style="width: 35%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                      <td style="width: 20%" class="right">                                                                             
                          <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                      <td style="width: 20%" class="center">
                          <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                          </asp:DropDownList></td>
                      <td style="width: 25%" class="left">
                          <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                  </tr>
              </table></asp:Panel>
  </td></tr>
</table>
</td></tr>
</table>
    </form>
   <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
   
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtLTRNo.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtLTRNo.ClientId%>').value
            reg = new RegExp("^[0-9]+$"); 

            if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=txtLTRNo.ClientId%>').focus();
                document.getElementById('<%=lblError.ClientId%>').innerText ='LTR number should contain only digits.'
                return false;

             }
        }
        if(document.getElementById('<%=txtFeedBackID.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtFeedBackID.ClientId%>').value
            reg = new RegExp("^[0-9]+$"); 

            if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=txtFeedBackID.ClientId%>').focus();
                document.getElementById('<%=lblError.ClientId%>').innerText ='FeedBack Id should contain only digits.'
                return false;

             }
        }
        //      Checking txtOpenDateFrom .
        if(document.getElementById('<%=txtDateFrom.ClientId%>').value != "" )
        {
            if (isDate(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date from is not valid.";			
	           document.getElementById('<%=txtDateFrom.ClientId%>').focus();
	           return(false);  
            }
       }
       else
       
       {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date from is Mandatory.";			
	           document.getElementById('<%=txtDateFrom.ClientId%>').focus();
	           return(false);  
       
       }
         //      Checking txtOpenDateTo .
        if(document.getElementById('<%=txtDateTo.ClientId%>').value!='')
        {
        
            if (isDate(document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date to is not valid.";			
	           document.getElementById('<%=txtDateTo.ClientId%>').focus();
	           return(false);  
            }
       }
       else
       {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date to is Mandatory.";			
	           document.getElementById('<%=txtDateTo.ClientId%>').focus();
	           return(false);  
       
       }
      
        
      
            if (compareDates(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy")==1)
           {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Date to should be greater than or equal to Date from.'
                return false;
           }
          
        var strDateFrom=document.getElementById('txtDateFrom').value;
        var strDateTo=document.getElementById('txtDateTo').value;
        var strFromYr=strDateFrom.split("/")[2]
        var strToYr=strDateTo.split("/")[2]
        var strFromMon=strDateFrom.split("/")[1]
        var strToMon=strDateTo.split("/")[1]
        if ((strFromYr!=strToYr) || (strFromMon!=strToMon))
        {
            document.getElementById('lblError').innerText = "Month and Year should be same";			
	        document.getElementById('txtDateTo').focus();
	        return(false);  
        }
        
        
    }
    
   

    </script>
</body>

</html>
