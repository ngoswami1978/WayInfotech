<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation ="false" CodeFile="TRSR_ParticipantBasket.aspx.vb" Inherits="Training_TRSR_ParticipantBasket" MaintainScrollPositionOnPostBack="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <title>AAMS::Training::Search Participant Basket</title>
      <script language="javascript" type="text/javascript">
      
      {debugger;}
      
       function ActDeAct()
     {
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
          if(whichASC!=9 && whichASC!=18 && whichASC!=13)
        {
        document.getElementById("hdCourseLCode").value="";
     
        }
    	
     }
     
     
   function Edit(BasketID,Status)
			{
				 window.location.href="TRUP_ParticipantBasket.aspx?Action=U&BasketID=" +BasketID + "&Status=" + Status;
				 return false;
			}
			
	function Delete(BasketID)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('<%=hdParBasketID.ClientId%>').value = BasketID
               return true;        
            }
            return false;
	}
	
	function SelectFunction(ContactID,NAME,STAFFNAME,LCODE,Request_ID)
        {           
        
         if (window.opener.document.forms['form1']['hdCourseSessionBasketPopup']!=null)
        { 
        window.opener.document.forms['form1']['hdCourseSessionBasketPopup'].value='B' + "|" + ContactID + "|" + NAME + "|" + STAFFNAME + "|" + LCODE + "|" + Request_ID  ;
        window.opener.document.forms['form1'].submit();
        window.close();
        return false;
            
        }
        
         if (window.opener.document.forms['form1']['hdParticipant']!=null)
        { 
            window.opener.document.forms['form1']['hdParticipant'].value = ContactID;
            window.opener.document.forms['form1']['txtParticipant'].value = STAFFNAME;
            window.close();
            return false;
            
        }
        }
        
    function SelectAll() 
    {
       CheckAllDataGridCheckBoxes(document.forms[0].chkAllSelect.checked)
    }
    
    function CheckAllDataGridCheckBoxes(value) 
    {
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
              elm.checked = value
            }
        }
    }
    
	
	function ReturnData()
       {
       {debugger;}
           for(i=0;i<document.forms[0].elements.length;i++) 
            {
            var elm = document.forms[0].elements[i]; 
                    if(elm.type == 'checkbox') 
                    {
                         if (elm.checked == true && elm.id != "chkAllSelect")
                         {
                            var chkname=elm.id;
                            var gvname=chkname.split("_")[0];
                            var ctrlidname=chkname.split("_")[1];
                            //alert (chkname);
                             if (document.getElementById("hdData").value == "")
                             {
                                document.getElementById("hdData").value ='B' + "|" + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value.replace(","," ");
                             }
                             else
                             {
                                document.getElementById("hdData").value = document.getElementById("hdData").value + "," + 'B' + "|" + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value.replace(","," ");
                             }
                         }
                    }
                }
        
            var data= document.getElementById("hdData").value;
           // alert(data);
            if(data=="")
            {
                document.getElementById("lblError").innerText="Checked atleast one checkbox";
                return false;            
            }
            else
           {
                 if (window.opener.document.forms['form1']['hdBasketListPopUpPage']!=null)
                 {
                    if (window.opener.document.forms['form1']['hdBasketListPopUpPage'].value=="")
                    {
                        window.opener.document.forms['form1']['hdBasketListPopUpPage'].value=data;
                    }
                    else
                    {
                        window.opener.document.forms['form1']['hdBasketListPopUpPage'].value=window.opener.document.forms['form1']['hdBasketListPopUpPage'].value + "," + data;
                    }
                    window.opener.document.forms(0).submit();
                    window.close();
                    return false;
                 }
           }
          }
	
     function PopupPage(id)
         {
         var type;
        
         if (id=="1")
         {
              type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aaTrainingAgency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	    
          }
            
         if (id=="2")
         {
                var strAgencyName=document.getElementById("txtAgency").value;
                  strAgencyName=strAgencyName.replace("&","%26")
               type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName;
   	            window.open(type,"aaTrainingAgencyStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
          }
          
           if (id=="3")
         {
        
                
                var strAgencyName=document.getElementById("txtEmployee").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                 var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                if (strEmployeePageName!="")
                {   
                    type = "../Setup/" + strEmployeePageName+ "?Popup=T&EmployeeName="+strAgencyName;
                //    type = "../Setup/MSSR_Employee.aspx?Popup=T&EmployeeName="+strAgencyName;
   	                window.open(type,"aaTrainingEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	            }
          }
    
           
     }
    
  
    
    
      </script>
</head>
<body >
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtAgency">
    <div>
     <table>
    <tr>
    <td>
    <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                <asp:Panel ID="pnlExport" runat="server">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Participant Basket Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Participant Basket</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center gap" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Agency</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAgency" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                    Width="530px"></asp:TextBox>
                                                <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td class="left" style="width: 12%">
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                </td>
                                            <td class="textbold"> 
                                                Agency Staff</td>                                                                               
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAgencyStaff" runat="server" CssClass="textbox" 
                                                    TabIndex="2" Width="530px" MaxLength="50"></asp:TextBox>
                                                <img id="Img3" runat="server" alt="Select & Add Agency Staff" onclick="PopupPage(2)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td style="width: 12%;" class="left">
                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Course</td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="536px" TabIndex="2" onkeyup="gotop(this.id)">
                                                    
                                                                                                  
                                                </asp:DropDownList>&nbsp;</td>
                                            <td class="left">
                                               <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="3" AccessKey="e" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Employee</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtEmployee" runat="server" CssClass="textboxgrey" TabIndex="2" Width="530px" ReadOnly="True"></asp:TextBox>
                                                <img id="Img1" runat="server" alt="Select & Add Employee " onclick="PopupPage(3)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td class="left">
                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" />
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                AOffice</td>
                                            <td>
                                                <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                   
                                                   
                                                </asp:DropDownList></td>
                                            <td class="textbold">
                                                Office ID</td>
                                            <td>
                                                <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textbox" TabIndex="2" Width="170px" MaxLength ="9"></asp:TextBox></td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Request ID</td>
                                            <td>
                                                <asp:TextBox ID="txtRequestID" runat="server" CssClass="textbox" TabIndex="2" Width="170px" MaxLength="15"></asp:TextBox></td>
                                            <td class="textbold">
                                                Status</td>
                                            <td>
                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlStatus" runat="server" CssClass="dropdownlist" Width="176px" ></asp:DropDownList></td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Req Date From </td>
                                            <td>
                                                <asp:TextBox ID="txtReqDateFrom" runat="server" CssClass="textbox" Width="170px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgReqDateFrom" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtReqDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgReqDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                            </script></td>
                                            <td class="textbold">
                                                Req Date To </td>
                                            <td>
                                                <asp:TextBox ID="txtreqDateTo" runat="server" CssClass="textbox" Width="170px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgReqDateTo" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtreqDateTo.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgReqDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                
                                                               });
                                            </script>
                                                </td>
                                            <td class="left">
                                               </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                </td>
                                            <td>
                                                &nbsp;<asp:RadioButtonList ID="rdlrequest" runat="server" CellPadding="2" CellSpacing="2"
                                                    CssClass="textbold" RepeatDirection="Horizontal" TabIndex="2">
                                                    <asp:ListItem Value="1">Web Request</asp:ListItem>
                                                    <asp:ListItem Value="2">Client</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="3">Both</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                            <td class="textbold">
                                            </td>
                                            <td>
                                                </td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>  </td> <td class="textbold" >   </td>    <td colspan="3">
                                                </td>
                                            <td>
                                            </td>
                                        </tr>                           
                                        <tr>
                                            <td ></td>
                                            <td >       </td>    <td  colspan="4">
                                            
                                                <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true"/>
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                <asp:HiddenField ID="hdTrainingLCode" runat="server" /><asp:HiddenField ID="hdTrainingStaffID" runat="server" />
                                                <asp:HiddenField ID="hdTrainingEmployeeID" runat="server" />
                                                 <asp:HiddenField ID="hdParBasketID" runat="server" />
                                                 <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                <asp:HiddenField ID="hdCourseLCode" runat="server" />
                                                
                                                </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" style="height: 15px" >
                                                                               
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
                                                   </td>
    </tr>
    <tr><td class="top border_rightred">
<table width="100%">
<tr>
<td colspan="6" >
    <asp:Button ID="btnSelect" runat="server" CssClass="button" Text="Select" Visible ="false"  OnClientClick="return ReturnData()" />
</td>
</tr>
<tr>
<td class="redborder top">
 <asp:GridView   ID="gvParticipantBasket" runat="server"  AutoGenerateColumns="False" Width="1400px" TabIndex="6" 
 EnableViewState="False" AllowSorting="True" HeaderStyle-ForeColor="white">
    <Columns>
   <asp:TemplateField  >
   
   <HeaderTemplate >
   <input type="checkbox" id="chkAllSelect" name = "chkAllSelect" onclick="SelectAll();"/>
   </HeaderTemplate>
   <ItemTemplate >
        <input type="checkbox" id="chkSelect" name ="chkSelect" runat ="server" />
        <asp:HiddenField  ID ="hdDataID" runat ="server" Value ='<% #Container.DataItem("AGENCYSTAFFID") + "|" + Container.DataItem("NAME") + "|" + Container.DataItem("STAFFNAME") + "|" + Container.DataItem("LCODE") + "|" + Container.DataItem("TR_BASKET_REQUEST_ID")  %>' /> 
   </ItemTemplate>
   
   </asp:TemplateField>
        <asp:BoundField HeaderText="Req Id" DataField="TR_BASKET_REQUEST_ID" SortExpression="TR_BASKET_REQUEST_ID" >
         <ItemStyle Width="3%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Date" DataField="TR_BASKET_DATE" SortExpression="TR_BASKET_DATE" >
        <ItemStyle Width="8%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Agency" DataField="NAME" SortExpression="NAME" >
        <ItemStyle Width="10%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Address" DataField="ADDRESS" SortExpression="ADDRESS" >
        <ItemStyle Width="10%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="OfficeId" DataField="OFFICEID" SortExpression="OFFICEID" >
        <ItemStyle Width="5%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Course" DataField="TR_COURSE_NAME" SortExpression="TR_COURSE_NAME" >
        <ItemStyle Width="11%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Staff Name" DataField="STAFFNAME" SortExpression="STAFFNAME" >
        <ItemStyle Width="7%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Employee Name" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" >
        <ItemStyle Width="7%" />
        </asp:BoundField>
         <asp:BoundField HeaderText="Status" DataField="STATUS" SortExpression="STATUS">
        <ItemStyle Width="5%" />
        </asp:BoundField>
         <asp:BoundField HeaderText="Session Date" DataField="SESSION_DATE" SortExpression="SESSION_DATE">
        <ItemStyle Width="6%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Preferred Date" DataField="PREFERREDDATE" SortExpression="PREFERREDDATE" >
        <ItemStyle Width="7%" />
        </asp:BoundField>
        
        <asp:BoundField HeaderText="AOffice" DataField="AOFFICE" SortExpression="AOFFICE" >
        <ItemStyle Width="4%" />
        </asp:BoundField>
        
        <asp:BoundField HeaderText="Email" DataField="EMAIL" SortExpression="EMAIL" >
        <ItemStyle Width="9%" />
        </asp:BoundField>
       <asp:BoundField HeaderText="Remarks" DataField="TR_BASKET_REMARKS" SortExpression="TR_BASKET_REMARKS" >
        <ItemStyle Width="7%" />
        </asp:BoundField>
        
        <asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="lnkSelect" runat="server" CommandName ="SelectX" Text ="Select" Visible="false"  CssClass="LinkButtons"></asp:LinkButton>&nbsp;
<asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
<asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
<asp:HiddenField ID="hdBasketID" runat="server" Value='<%#Eval("TR_BASKET_REQUEST_ID")%>' />   

</ItemTemplate>
            <ItemStyle Width="7%" />
</asp:TemplateField>                                                  

</Columns>
<AlternatingRowStyle CssClass="lightblue" />
<RowStyle CssClass="textbold" />
<HeaderStyle CssClass="Gridheading" />                                                    
</asp:GridView>
<asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%" CssClass="left">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table>
    <input id="hdData" style="width: 1px" type="hidden" runat="server" />
    </asp:Panel>
</td></tr></table>
</td></tr>

</table>
    </div>
    </form>
     <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
   
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtRequestID.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtRequestID.ClientId%>').value
            reg = new RegExp("^[0-9]+$"); 

            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='RequestID should contain only digits.'
                return false;

             }
        }
        // Checking OFFICEID for Alpha Numeric.
        if(document.getElementById('<%=txtOfficeID.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtOfficeID.ClientId%>').value
            reg = new RegExp("^[a-zA-Z0-9]+$"); 

            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='OfficeID should contain only alpha numeric.'
                return false;

             }
        }
        
        
        //      Checking txtOpenDateFrom .
        if(document.getElementById('<%=txtReqDateFrom.ClientId%>').value != '')
        {
            if (isDate(document.getElementById('<%=txtReqDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Request date from is not valid.";			
	           document.getElementById('<%=txtReqDateFrom.ClientId%>').focus();
	           return(false);  
            }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('<%=txtreqDateTo.ClientId%>').value != '')
        {
            if (isDate(document.getElementById('<%=txtreqDateTo.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Request date to is not valid.";			
	           document.getElementById('<%=txtreqDateTo.ClientId%>').focus();
	           return(false);  
            }
        } 
          
        // End function
         //****************************************************************
    //      Checking txtOpenDateFrom .
      
         //****************************************************************
  
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
        
   
        if (compareDates(document.getElementById('<%=txtReqDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtreqDateTo.ClientId%>').value,"d/M/yyyy")==1)
           {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Request date to should be greater than or equal to Request date from.'
                return false;
           }
       return true; 
        
    }
    
   
    

    </script>
</body>
</html>
