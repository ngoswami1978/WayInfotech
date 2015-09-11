<%@ Page Language="VB" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" CodeFile="BOHDSR_BDR.aspx.vb" Inherits="BOHelpDesk_HDSR_BDR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
      <title>Back Office Helpdesk BDR Letter</title>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
     <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
     <script type="text/javascript" language ="javascript" > 
    function ActDeAct()
     {
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
          if(whichASC!=9 && whichASC!=18 && whichASC!=13)
        {
        document.getElementById("hdAgencyNameId").value="";
     
        }
    	
     }
     function SelectFunction(str3)
        {   
          //  alert(str3);
//           // var pos=str3.split('|'); 
            if (window.opener.document.forms['form1']['txtBDRLetterID']!=null)
            {
            window.opener.document.forms['form1']['txtBDRLetterID'].value=str3;
            }

           window.close();
        }
  function EmployeePage()
{
            var type;      
            

         var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
        if (strEmployeePageName!="")
        {
           type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
            //type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=910,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
}
     function PopupAgencyPage()
        {
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"Agency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
     function  NewHDUPBDR()
       {    
           window.location="BOHDUP_BDR.aspx?Action=I";
           return false;
       }  
    function  BDRReset()
    {       
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtAgencyName").value="";  
        document.getElementById("drpReqType").value=""; 
        document.getElementById("txtBDrId").value=""; 
        document.getElementById("txtBDRTicket").value=""; 
        document.getElementById("txtLtrNo").value=""; 
       // document.getElementById("drpBdrSentBy").value=""; 
       document.getElementById("txtBdrSentBy").value=""; 
       
        document.getElementById("drpAirLine").value=""; 
        document.getElementById("drp1Aoffice").value=""; 
        document.getElementById("txtAirLineoffice").value=""; 
        document.getElementById("txtBDRLoggedDateFrom").value=""; 
        document.getElementById("txtBDRLoggedDateTo").value=""; 
        document.getElementById("hdAgencyNameId").value=""; 
        document.getElementById("hdEmployeeId").value="";
        document.getElementById("hdEmployeeName").value="";  
        
        
//        document.getElementById("chkWholeGroup").checked=false;
         if (document.getElementById("gvBdr")!=null) 
        document.getElementById("gvBdr").style.display ="none"; 
      
        return false;
    }
     function BDRMandatory()
    {
    
     if (document.getElementById("txtBDrId").value!="")
         {
           if(IsDataValid(document.getElementById("txtBDrId").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="BDR Id  is not valid.";
            document.getElementById("txtBDrId").focus();
            return false;
            } 
//             if(parseInt(document.getElementById("txtBDrId").value)>32767)
//            {
//           // alert("abhi");
//            document.getElementById("lblError").innerHTML="BDR Id is not valid.";
//            document.getElementById("txtBDrId").focus();
//            return false;
//            } 
         }  
     if (document.getElementById("txtLtrNo").value!="")
         {
           if(IsDataValid(document.getElementById("txtLtrNo").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="LTR No is not valid.";
            document.getElementById("txtLtrNo").focus();
            return false;
            } 
//             if(parseInt(document.getElementById("txtLtrNo").value)>32767)
//            {
//           // alert("abhi");
//            document.getElementById("lblError").innerHTML="LTR No is not valid.";
//            document.getElementById("txtLtrNo").focus();
//            return false;
//            } 
         }  
      if(document.getElementById('<%=txtBDRLoggedDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtBDRLoggedDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "BDR Logged Date From is not valid.";			
	       document.getElementById('<%=txtBDRLoggedDateFrom.ClientId%>').focus();
	       return(false);  
        }
       }  
        if(document.getElementById('<%=txtBDRLoggedDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtBDRLoggedDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "BDR Logged Date To is not valid.";			
	       document.getElementById('<%=txtBDRLoggedDateTo.ClientId%>').focus();
	       return(false);  
        }
       }
         if(document.getElementById('<%=txtBDRLoggedDateTo.ClientId%>').value != '' && document.getElementById('<%=txtBDRLoggedDateFrom.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtBDRLoggedDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtBDRLoggedDateTo.ClientId%>').value,"d/M/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "BDR Logged Date To is shorter than BDR Logged Date From.";			
	       document.getElementById('<%=txtBDRLoggedDateTo.ClientId%>').focus();
	       return(false);  
        }
       }  
       
        
         return true;
     }
     function DeleteFunction(CheckBoxObj)
      {   
//        if (confirm("Are you sure you want to delete?")==true)
//        {   
//            window.location.href="HDSR_BDR.aspx?Action=D|"+CheckBoxObj + "|" +   document.getElementById("hdEmployeeName").value;
//            return false;
//        }
              if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteBDRID").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteBDRID").value="";
                 return false;
                }
    }
      function DeleteFunction2(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {    
            window.location.href="HDSR_BDR.aspx?PopUp=T&Action=D|"+CheckBoxObj + "|" +   document.getElementById("hdEmployeeName").value;      
          
            return false;
        }
    }
    
      function EditFunction(CheckBoxObj)
    {                
          window.location ="BOHDUP_BDR.aspx?Action=U&HD_RE_BDR_ID=" + CheckBoxObj; 
          return false;
    } 
     // Written for showing History Page
    function PopupHistoryPage(objBdrId)
    { 
        //alert("abhi");
         var BDRValue=objBdrId;
         var type="../BirdresHelpDeskPopup/PUSR_BDRHistory.aspx?BDRId=" + BDRValue; 
        if (window.showModalDialog) 
        {
              strReturn=window.showModalDialog(type,'BdrValue','dialogWidth:920px;dialogHeight:600px;help:no;');       
        }
        else
        {
         strReturn=window.open(type,'BdrValue','height=600,width=920,top=30,left=20,scrollbars=1'); 
        }
    }  
    </script>
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <!-- import the calendar script -->
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <!-- import the language module -->
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <!-- import the calendar setup module -->
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton ="btnSearch" defaultfocus ="txtAgencyName">
      <table style="width:845px" align="left" class="border_rightred" >
            <tr>
                <td valign="top"  style="width:860px;">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" ><span class="menu">Back Office Help Desk-></span><span class="sub_menu">BDR </span></td>
                        </tr>
                        <tr>
                            <td  class="heading" align="center" valign="top" >Search BDR </td>
                        </tr>
                        <tr>
                            <td valign="top" align="LEFT">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  align ="left" class="redborder" style="width:860px" >                                 
                                                        <table    border="0"  cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="9" align="center" style="height:25px;" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  class="textbold" >&nbsp;</td>
                                                                <td   class="textbold" ><span class="textbold">Agency</span></td>
                                                                <td colspan="5" class="textbold"><asp:TextBox ID="txtAgencyName" runat ="server" CssClass ="textbox" Width="510px" MaxLength="50" TabIndex="1"   ></asp:TextBox></td>                                                                
                                                                <td class="textbold" style="width: 13px" ><img tabIndex="2" src="../Images/lookup.gif"  onclick="javascript:return PopupAgencyPage();" style="cursor:pointer;"  /></td>
                                                                <td align="center"  ><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="16" AccessKey="A" /></td>
                                                            </tr>                                                      
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="9" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" >&nbsp;</td>
                                                                <td  class="textbold" ><span class ="textbold">Query Type </span></td>
                                                                <td  class="textbold" ><asp:DropDownList ID="drpReqType" runat ="server" CssClass ="dropdownlist" Width="165px" TabIndex="3"></asp:DropDownList></td>
                                                                <td  class="textbold"></td>
                                                                <td class="textbold" ></td>
                                                                <td ><span class="textbold">BDR ID</span></td> 
                                                                <td  class="textbold" ><asp:TextBox ID="txtBDrId" runat ="server" CssClass ="textbox"  MaxLength="9" TabIndex="4" Width="158px" ></asp:TextBox></td>
                                                                <td  class="textbold" style="width: 13px" ></td>
                                                                <td align="center" >
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="17" Text="Export" AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="9" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" >&nbsp;</td>
                                                                <td  class="textbold" ><span class="textbold">BDR Ticket</span></td>
                                                                <td  class="textbold" ><asp:TextBox ID="txtBDRTicket" runat ="server" CssClass ="textbox"  MaxLength="100" TabIndex="5" Width="158px" ></asp:TextBox></td>
                                                                <td  class="textbold"></td>
                                                                <td class="textbold" ></td>
                                                                <td  ><span class="textbold">LTR No</span></td> 
                                                                <td class="textbold" ><asp:TextBox id="txtLtrNo" tabIndex=6 runat="server" CssClass="textbox" MaxLength="9" Width="158px"></asp:TextBox></td>
                                                                <td  class="textbold" style="width: 13px" ></td>
                                                                <td align="center" ><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="18" AccessKey="R" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td style="height:4px;" class="textbold" colspan="9" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" >&nbsp;</td>
                                                                <td class="textbold" ><span class="textbold">BDR Sent by </span></td>
                                                                <td  class="textbold" > <asp:TextBox ID="txtBdrSentBy" runat="server" CssClass="textbox" MaxLength="40" TabIndex="7" Width="159px"></asp:TextBox>&nbsp;<%--<asp:DropDownList id="drpBdrSentBy" runat="server" CssClass="dropdownlist" Width="150px" TabIndex="8"></asp:DropDownList>--%></td>
                                                                <td class="textbold"><img alt="" onclick="javascript:return EmployeePage();" src="../Images/lookup.gif" / tabIndex="8" style="cursor:pointer;"  ></td>
                                                                  <td  class="textbold" ></td>
                                                                <td  ><span class="textbold">Airlines</span></td> 
                                                                <td  class="textbold" ><asp:DropDownList id="drpAirLine" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="9"></asp:DropDownList></td>
                                                                <td  class="textbold" style="width: 13px" ></td>
                                                                <td  ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="height:4px;" class="textbold" colspan="9" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  class="textbold" >&nbsp;</td>
                                                                <td  class="textbold" ><span class="textbold">1a Office </span></td>
                                                                <td  class="textbold" ><asp:DropDownList id="drp1Aoffice" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="10"></asp:DropDownList></td>
                                                                <td  class="textbold"></td>
                                                                <td class="textbold" ></td>
                                                                <td  ><span class="textbold">Airlines Office</span></td> 
                                                                <td  class="textbold" ><asp:TextBox ID="txtAirLineoffice" runat ="server" CssClass ="textbox"  MaxLength="30" TabIndex="11" Width="158px" ></asp:TextBox></td>
                                                                <td  class="textbold" style="width: 13px" ></td>
                                                                <td  ></td>
                                                            </tr>                                                          
                                                             <tr>
                                                                <td  style="height:4px;" class="textbold" colspan="9" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td  class="textbold"  style="width:50px; " ></td>
                                                                <td  class="textbold"  style="width:150px; "><span class="textbold">BDR Logged Date From</span></td>
                                                                <td  class="textbold" style="width:170px; " ><asp:TextBox ID="txtBDRLoggedDateFrom" runat="server" MaxLength="10" TabIndex="12" CssClass="textboxgrey" Width="138px" ReadOnly="True"></asp:TextBox>&nbsp;<img id="f_BDRLoggedDateFrom" alt="" src="../Images/calender.gif" TabIndex="13" title="Date selector" style="cursor: pointer" /></td>
                                                                <td  class="textbold" style="width:30px; ">
                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtBDRLoggedDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_BDRLoggedDateFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                                <td  class="textbold"  ></td>
                                                                <td  style ="width:150px" ><span class="textbold">BDR Logged Date To</span></td> 
                                                                <td  class="textbold" style="width:170px;" ><asp:TextBox ID="txtBDRLoggedDateTo" runat="server" MaxLength="10" TabIndex="14" CssClass="textboxgrey" Width="138px" ReadOnly="True"></asp:TextBox>&nbsp;<img id="f_BDRLoggedDateTo" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                        tabindex="15" title="Date selector" />

                                                                    <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtBDRLoggedDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_BDRLoggedDateTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                    </script></td>
                                                                <td  class="textbold" style="width: 23px" ></td>
                                                                <td   style ="width:120px" ></td>
                                                            </tr>                                                           
                                                             <tr>
                                                                <td  style="height: 15px" class="textbold" colspan="9" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td class="textbold" >&nbsp;</td>
                                                                <td  class="textbold" ></td>
                                                                <td  colspan="4" class="ErrorMsg" ></td>
                                                                <td class="textbold" >&nbsp;</td>
                                                                <td  class="textbold" style="width: 13px" ></td>
                                                                <td ></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="9" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                               <tr>
                                                                <td  class="textbold" colspan="9" align="center" valign="TOP"> <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" /> 
                                                         <input type="hidden" id="hdEmployeeId" runat="server" value="" style="width: 5px" />
                                                          <input type="hidden" id="hdEmployeeName" runat="server" value="" style="width: 5px" />
                                                          
<input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                                                                       
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="9" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                        </table>  
                                        </td>
                                    </tr>
                                       <tr>
                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">                                                                
                                            </td>
                                        </tr>
                                </table>
                            </td>
                        </tr>
                     
                    </table>
                </td>
            </tr>
            <tr>
            <td valign ="top" style="padding-left:4px;">
            <table width="1200px" border="0" cellspacing="0" cellpadding="0" align="left" > 
                                                          <tr>    
                                                               <td  style="width:1200px;" class="redborder" valign ="top" ><asp:GridView ID="gvBdr" runat="server"  AutoGenerateColumns="False" TabIndex="18" width="1200px" EnableViewState="False"  RowStyle-VerticalAlign ="Top" AllowSorting ="true" HeaderStyle-ForeColor ="white"  >
                                                                                 <Columns>
                                                                                 <asp:TemplateField HeaderText="LTR No"  HeaderStyle-Wrap ="false" ItemStyle-Wrap="false"   SortExpression ="LTRNO" >
                                                                                                <itemtemplate  >
                                                                                                    <%#Eval("LTRNO")%>
                                                                                                    <asp:HiddenField ID="HDHDREBDRID" runat="server" Value='<%#Eval("HD_RE_BDR_ID")%>' />
                                                                                                </itemtemplate>
                                                                                     <ItemStyle Wrap="False" />
                                                                                        </asp:TemplateField>                                                                                        
                                                                                        <asp:BoundField DataField="AGENCYNAME"  HeaderText="Agency Name"  HeaderStyle-Wrap ="false"  ItemStyle-Width="100px" SortExpression ="AGENCYNAME" />
                                                                                        <asp:BoundField DataField="ADDRESS"  HeaderText="Agency Address" HeaderStyle-Wrap ="false" SortExpression ="ADDRESS" />
                                                                                        <asp:BoundField DataField="QUERYTYPE"  HeaderText="Requested Type"  HeaderStyle-Wrap ="false" SortExpression ="QUERYTYPE"  >
                                                                                            <ItemStyle Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="HD_RE_BDR_TICKETS"  HeaderText="BDR Tickets" HeaderStyle-Wrap ="false"  SortExpression ="HD_RE_BDR_TICKETS" >
                                                                                            <ItemStyle Wrap="False" />
                                                                                            <HeaderStyle Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="HD_RE_BDR_DATESEND"  HeaderText="Date Sent" HeaderStyle-Wrap ="false" SortExpression ="HD_RE_BDR_DATESEND" >
                                                                                            <ItemStyle Wrap="False" />
                                                                                            <HeaderStyle Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="STATUS"  HeaderText="Status" SortExpression ="STATUS"  >
                                                                                            <ItemStyle Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="AIRLINE"  HeaderText="AirLine" SortExpression ="AIRLINE"  />                                                                                                                                                                         
                                                                                        <asp:BoundField DataField="AIRLINEOFFICEADDRESS"  HeaderText="Airline Aoffice Address" SortExpression ="AIRLINEOFFICEADDRESS" />
                                                                                        <asp:BoundField DataField="HD_RE_BDR_SENDBY"  HeaderText="Send By" SortExpression ="HD_RE_BDR_SENDBY"   />
                                                                                        <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>  <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="SelectX" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "HD_RE_BDR_ID") %>'></asp:LinkButton>&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton> <%--&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>&nbsp;&nbsp;<%--<a href="#" class="LinkButtons" id="linkHistory" runat="server">History</a>--%>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle  CssClass="ItemColor" Wrap="False" />
                                                                                                <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                       </asp:TemplateField>
                                                                                 </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" VerticalAlign="Top" />
                                                                                        <HeaderStyle CssClass="Gridheading" />
                                                                                        
                                                                  </asp:GridView></td>
                                                         </tr>
                                                        <tr>
                                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">                                                                
                                                            </td>
                                                        </tr>
                                                      </table></td>
            </tr>
               <tr>                                                   
                                                    <td valign ="top"  style ="width:840px;" >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="19" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="height: 53px">
                                                        <asp:HiddenField ID="hdDeleteBDRID" runat="server" />
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                            Width="73px"></asp:TextBox>
                                                    </td> 
                                                </tr> 
        </table>
    <!-- Code by Dev Abhishek -->
    </form>
</body>
</html>
