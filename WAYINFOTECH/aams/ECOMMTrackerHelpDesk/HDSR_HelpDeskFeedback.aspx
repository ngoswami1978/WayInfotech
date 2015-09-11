<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_HelpDeskFeedback.aspx.vb" Inherits="ETHelpDesk_HDSR_HelpDeskFeedback" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::ETrackers HelpDesk::Search FeedBack</title>
    <script src="../JavaScript/ETracker.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
   <script language="javascript" type="text/javascript">

        function PopupPage(id)
{
var type;
    if (id=="1")
    {
        type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
        window.open(type,"aaFeedBackAgency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
    }
    if (id=="2")
    {
         var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
        if (strEmployeePageName!="")
        {
            type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
        //    type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	        window.open(type,"aaFeedBackEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1"); 
   	   }
    }
}

                
        function EditFunction(FeedbackID,HD_RE_ID,HD_QUERY_GROUP_ID,LCode,AOFFICE,Status)
        {         
          var strStatus="";
          if (HD_QUERY_GROUP_ID=="1")
          {
          strStatus=Status;
          window.location.href="HDUP_CallLog.aspx?Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus + "&FeedBackId=" + FeedbackID + "&AOFFICE="+ AOFFICE + "&QueryGroup=" +strStatus ;               
          }
          else
          {strStatus=Status;      
          window.location.href="TCUP_CallLog.aspx?Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus + "&FeedBackId=" + FeedbackID + "&AOFFICE="+ AOFFICE + "&QueryGroup=" +strStatus ;               
          }
          return false;
        }
                     
        
        function DeleteFunction(hdFeedbackID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteId").value=hdFeedbackID;       
           
           }
           else
           {
                document.getElementById("hdDeleteId").value="";
                 return false;
           }
        }
 
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
        if (document.getElementById('<%=txtDateFrom.ClientId%>').value!="")
        {
            if (isDate(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date from is not valid.";			
	           document.getElementById('<%=txtDateFrom.ClientId%>').focus();
	           return(false);  
            }
       }
         //      Checking txtOpenDateTo .
        if (document.getElementById('<%=txtDateTo.ClientId%>').value!="")
        {
            if (isDate(document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date to is not valid.";			
	           document.getElementById('<%=txtDateTo.ClientId%>').focus();
	           return(false);  
            }
       }
      
        
      
            if (compareDates(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy")==1)
           {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Date to should be greater than or equal to Date from.'
                return false;
           }
          
       
        
        
    }
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
   
     <div>
     <table><tr> <td>
    <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> ETrackers HelpDesk-&gt;<span class="sub_menu"></span></span><span class="sub_menu">FeedBack Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search FeedBack</td>
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
                                            <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"  src="../Images/lookup.gif" style="cursor: pointer" /></td>
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
                                                <asp:DropDownList ID="drpFeedBackStatus" runat="server" CssClass="dropdownlist" TabIndex="2" onkeyup="gotop(this.id)"
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
                                            <td>  </td> <td class="textbold" >   Date From</td>    <td>
                                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox"                                                     TabIndex="2" Width="170px"></asp:TextBox>
                                                <img id="imgDateFrom" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                                                                                                                      });
                                                                                                  </script></td><td class="textbold" > Date To</td><td>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox" 
                                TabIndex="2" Width="170px"></asp:TextBox>
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
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Region</td>
                                            <td>
                                                <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" TabIndex="2" onkeyup="gotop(this.id)"
                                                    Width="174px">
                                                </asp:DropDownList></td>
                                            <td class="textbold">
                                                Feedback Dept.</td>
                                            <td><asp:DropDownList ID="drpFeedbackDept" runat="server" CssClass="dropdownlist" TabIndex="2" onkeyup="gotop(this.id)"
                                                    Width="174px">
                                            </asp:DropDownList></td>
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
                                                                <td class="textbold" >
                                                                    Corp. Comm. Status</td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkCorporateCommunication" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                                                <td >
                                                                </td>
                                                            </tr>                        
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Critical</td>
                                            <td>
                                                <asp:CheckBox ID="chkCritical" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                            <td class="textbold">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td ></td>
                                            <td >       
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>    <td  colspan="4">
                                                <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true"/>
                                                <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                               
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
     <tr><td class="redborder">
     <asp:GridView  ID="gvFeedBack" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="1050px" EnableViewState="False" AllowSorting="True">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="FeedBack ID" DataField="FEEDBACK_ID" SortExpression="FEEDBACK_ID" >
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="LTR No" DataField="HD_RE_ID" SortExpression="HD_RE_ID">
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Agency Name" DataField="AGENCYNAME" SortExpression="AGENCYNAME">
                                                                                        <ItemStyle Width="15%" Wrap="True" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Office Id" DataField="OFFICEID" SortExpression="OFFICEID">
                                                                                        <ItemStyle Width="6%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Aoffice" DataField="AOFFICE" SortExpression="AOFFICE">
                                                                                        <ItemStyle Width="7%" />
                                                                                    </asp:BoundField>
                                                                                    
                                                                                     <asp:BoundField HeaderText="Region" DataField="REGION" SortExpression="REGION">
                                                                                        <ItemStyle Width="7%" />
                                                                                    </asp:BoundField>
                                                                                    
                                                                                    <asp:BoundField HeaderText="Date" DataField="DATETIME" SortExpression="DATETIME">
                                                                                        <ItemStyle Width="10%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Helpdesk Status" DataField="STATUS_HELPDESK" SortExpression="STATUS_HELPDESK">
                                                                                        <ItemStyle Width="7%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Technical Status" DataField="STATUS_TECHNICAL" SortExpression="STATUS_TECHNICAL">
                                                                                        <ItemStyle Width="7%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Sales Status" DataField="STATUS_SALES" SortExpression="STATUS_SALES">
                                                                                        <ItemStyle Width="7%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Training Status" DataField="STATUS_TRAINING" SortExpression="STATUS_TRAINING">
                                                                                        <ItemStyle Width="7%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Product Status" DataField="STATUS_PRODUCT" SortExpression="STATUS_PRODUCT">
                                                                                        <ItemStyle Width="7%" />
                                                                                    </asp:BoundField>

                                                                                   <asp:BoundField  HeaderText="Corporate comm." DataField="STATUS_CCOMMUNICATION" SortExpression="STATUS_CCOMMUNICATION" >
                                                                                    <ItemStyle Width="7%" />
                                                                                   </asp:BoundField>                                                                                                                                                                      
                                                                                                                            
                                                                                    <asp:TemplateField HeaderText="Action" >
                                                                                    <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                                                          
                                                                                       <asp:HiddenField ID="hdFeedbackID" runat="server" Value='<%#Eval("FEEDBACK_ID")%>' />   
                                                                                     </ItemTemplate>
                                                                                        <HeaderStyle Width="7%" />
                                                                                   </asp:TemplateField>                                                  
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                      
                                                    
                                                 </asp:GridView>
                                                         <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="860px">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
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
    </div>
   
    </form>
   </body>
</html>
