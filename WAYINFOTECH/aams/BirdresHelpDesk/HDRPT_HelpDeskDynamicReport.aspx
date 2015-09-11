<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDRPT_HelpDeskDynamicReport.aspx.vb" Inherits="BirdresHelpDesk_HDRPT_HelpDeskDynamicReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <script id="script1" type ="text/javascript" >
      function ActDeAct()
     {
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9)
        {
        document.getElementById("hdAgencyName").value="";
     
        }
    	
     }
       function fillDate()
        {
        if(document.getElementById("chkDisplayLastCall").checked)
        {
         document.getElementById("txtQueryOpenedDateFrom").value="";
         document.getElementById("txtQueryOpenedDateTo").value="";
        }
        else
        {
         document.getElementById("txtQueryOpenedDateFrom").value=document.getElementById("hdFromTime").value;
         document.getElementById("txtQueryOpenedDateTo").value=document.getElementById("hdToTime").value;
        }
        }
      function  ResultConfigure()
       {   
          var CurrentSet= document.getElementById("drpSelectSet").value
         type="../BirdresHelpDeskPopup/PUSR_HelpDeskResultConfigure.aspx?Action=U&Popup=T&CurrentSet=" + CurrentSet;
         window.open(type,"ff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	     

           return false;
       } 
         function  SearchConfigure()
       {   
         var CurrentSet= document.getElementById("drpSelectSet").value
         type="../BirdresHelpDeskPopup/PUSR_HelpDeskSearchConfigure.aspx?Action=U&Popup=T&CurrentSet=" + CurrentSet;
         window.open(type,"ff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	     

           return false;
       } 
    function PopupPage(id)
         {
         var type;
         if (id=="1")
         {
                

             var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
            if (strEmployeePageName!="")
            {
               type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
              //  type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	            window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");            
   	         }
          }
         if (id=="2")
         {
                type = "../TravelAgency/TASR_BRAgency.aspx?Popup=T&HelpDeskType=BR" ;
   	            window.open(type,"aa","height=600,width=900,top=30,left=20,scrollbars=1");	
         }
    
         if (id=="3")
         {
                type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T";
   	            window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");            
          }
    
           
     }   
     function CkeckValidateForm()
     
     {
     //alert("abhi");
         if (document.getElementById("txtLTRNo").value!="")
         {
           if(IsDataValid(document.getElementById("txtLTRNo").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="LTR No is not valid.";
            document.getElementById("txtLTRNo").focus();
            return false;
            } 
          }
        if (document.getElementById("txtPTRNo").value!="")
         {
           if(IsDataValid(document.getElementById("txtPTRNo").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="PTR No is not valid.";
            document.getElementById("txtPTRNo").focus();
            return false;
            } 
         }
         
          if (document.getElementById("txtQueryOpenedDateFrom")!=null)
         {    
            if (document.getElementById('txtQueryOpenedDateFrom').value.trim().length>0)
            {                 
               if (isDate(document.getElementById('txtQueryOpenedDateFrom').value.trim(),"dd/MM/yyyy HH:mm") == false)	
                {
                    document.getElementById('lblError').innerText ='Open date from is not valid.';
                    document.getElementById("txtQueryOpenedDateFrom").focus();
                    return false;   
                }         
            }
         }
         
                if (document.getElementById("txtQueryOpenedDateTo")!=null)
         {    
             if (document.getElementById('txtQueryOpenedDateTo').value.trim().length>0)
            {             
               if (isDate(document.getElementById('txtQueryOpenedDateTo').value.trim(),"dd/MM/yyyy HH:mm") == false)	
                {
                    document.getElementById('lblError').innerText ='Open date to is not valid.';
                    document.getElementById("txtQueryOpenedDateTo").focus();
                    return false;   
                }         
             }
        }
         
    
          if (document.getElementById("txtCloseDateFrom")!=null)
         {       
              if (document.getElementById('txtCloseDateFrom').value.trim().length>0)
            {       
               if (isDate(document.getElementById('txtCloseDateFrom').value.trim(),"dd/MM/yyyy HH:mm") == false)	
                {
                    document.getElementById('lblError').innerText ='Close date from is not valid.';
                    document.getElementById("txtCloseDateFrom").focus();
                    return false;   
                }         
            }
         }
           
           if (document.getElementById("txtCloseDateTo")!=null)
         {     
                if (document.getElementById('txtCloseDateTo').value.trim().length>0)
               {           
                   if (isDate(document.getElementById('txtCloseDateTo').value.trim(),"dd/MM/yyyy HH:mm") == false)	
                    {
                        document.getElementById('lblError').innerText ='Close date to is not valid.';
                        document.getElementById("txtCloseDateTo").focus();
                        return false;   
                    }         
                 }
         }
         
             if (document.getElementById('txtQueryOpenedDateFrom').value.trim().length>0 &&  document.getElementById('txtQueryOpenedDateTo').value.trim().length>0)
              {
                  if (compareDates(document.getElementById('txtQueryOpenedDateFrom').value,"dd/MM/yyyy HH:mm",document.getElementById('txtQueryOpenedDateTo').value,"dd/MM/yyyy HH:mm")=='1')
                {
                    document.getElementById('lblError').innerText ='Open date to should be greater than or equal to open date from.';
                    document.getElementById("txtQueryOpenedDateTo").focus();
                    return false;            
                }
             }
            
              if (document.getElementById('txtCloseDateFrom').value.trim().length>0 &&  document.getElementById('txtCloseDateTo').value.trim().length>0)
               {           
                 var dtFrom=document.getElementById("txtCloseDateFrom").value;
                 var dtTo=document.getElementById("txtCloseDateTo").value;
                 dtFrom=dtFrom.trim();
                 dtTo=dtTo.trim();
                   if (compareDates(dtFrom,"dd/MM/yyyy HH:mm",dtTo,"dd/MM/yyyy HH:mm")=='1')
                   {
                        document.getElementById('lblError').innerText ='Close date to should be greater than or equal to close date from.';
                         document.getElementById("txtCloseDateTo").focus();
                        return false;
                   }               
               }
         
              if(document.getElementById('txtDateAssigned')!=null)
             {         
                 if(document.getElementById('txtDateAssigned').value.trim().length>0)
                    {
                        if (isDate(document.getElementById('txtDateAssigned').value,"d/M/yyyy") == false)	
                        {
                           document.getElementById('lblError').innerText = "Assigned date is not valid.";			
	                       document.getElementById('txtDateAssigned').focus();
	                       return false;  
                        }
                  }
            }
         
         if (document.getElementById("txtWorkOrderNo")!=null)
         {
            if (document.getElementById("txtWorkOrderNo").value!="")
             {
               if(IsDataValid(document.getElementById("txtWorkOrderNo").value,3)==false)
                {
                document.getElementById("lblError").innerHTML="Work Order No is not valid.";
                document.getElementById("txtWorkOrderNo").focus();
                return false;
                } 
             }
          }       
     } 
    </script>
</head>
<body>
    <form id="form1" runat="server"  defaultbutton ="btnSearch" defaultfocus ="txtAgencyName" >        
        
       <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Birdres HelpDesk-&gt;</span><span class="sub_menu">HelpDesk Report</span>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                HelpDesk Report</td>
                        </tr>
                        <tr>
                            <td valign="top" >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder"> 
                                                          <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td style="width:3%" >&nbsp;
                                                                            </td>
                                                                            <td style="width:15%" >
                                                                            </td>
                                                                            <td colspan="4" class="center gap"> <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                </td>
                                                                            <td class="textbold"> Agency Name</td>                                                                               
                                                                            <td colspan="3">
                                                                            <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="30" TabIndex="1" Width="493px"></asp:TextBox>
                                                                            <img id="Img2" runat="server" alt="" tabIndex="2" onclick="PopupPage(2)"  src="../Images/lookup.gif" /></td>
                                                                            <td style="width: 12%;" class="center">
                                                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Display" TabIndex="38" Width="114px" AccessKey="D" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                </td>
                                                                            <td class="textbold">
                                                                                LTR No.</td>
                                                                            <td style="width: 27%">
                                                                                <asp:TextBox ID="txtLTRNo" runat="server" CssClass="textbox" Width="170px" TabIndex="3"  MaxLength="9"></asp:TextBox></td>
                                                                            <td class="textbold" style="width: 15%">
                                                                                PTR No.</td>
                                                                            <td style="width: 26%">
                                                                                <asp:TextBox ID="txtPTRNo" runat="server" CssClass="textbox" Width="138px" TabIndex="4"  MaxLength="9"></asp:TextBox></td>
                                                                            <td class="center" style="width: 13%">
                                                                                <asp:Button id="btnSearchConfigure" tabIndex=39 runat="server" CssClass="button" Width="114px" Text="Configure Search" AccessKey="C"></asp:Button></td>
                                                                        </tr>                                     
                                                                             <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Opened Date From <%--<span class="Mandatory">*</span>--%></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtQueryOpenedDateFrom" runat="server" CssClass="textbox" Width="170px" TabIndex="5"></asp:TextBox>
                                                                                <img  tabIndex="6"  id="imgOpenedDateFrom" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                                <script type="text/javascript">
                                                                                  Calendar.setup({
                                                                                  inputField     :    '<%=txtQueryOpenedDateFrom.clientId%>',
                                                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                                                  button         :    "imgOpenedDateFrom",
                                                                                 //align          :    "Tl",
                                                                                  singleClick    :    true,
                                                                                  showsTime      : true
                                                                                               });
                                                                            </script></td>
                                                                            <td class="textbold">
                                                                                Opened Date To <%--<span class="Mandatory">*</span>--%></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtQueryOpenedDateTo" runat="server" CssClass="textbox" Width="138px" TabIndex="7"></asp:TextBox>
                                                                                <img tabIndex="8"  id="imgOpenedDateTo" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                                <script type="text/javascript">
                                                                                  Calendar.setup({
                                                                                  inputField     :    '<%=txtQueryOpenedDateTo.clientId%>',
                                                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                                                  button         :    "imgOpenedDateTo",
                                                                                 //align          :    "Tl",
                                                                                  singleClick    :    true,
                                                                                  showsTime      : true
                                                                                               });
                                                                            </script>
                                                                                </td>
                                                                            <td>
                                                                               
                                                                                <asp:Button ID="btnResultConfig" CssClass="button" runat="server" Text="Configure Result " TabIndex="40" Width="114px" AccessKey="F" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Close Date From</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCloseDateFrom" runat="server" CssClass="textbox" Width="170px" TabIndex="9"></asp:TextBox>
                                                                                <img tabIndex="10"  id="imgCloseDateFrom" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                                <script type="text/javascript">
                                                                                  Calendar.setup({
                                                                                  inputField     :    '<%=txtCloseDateFrom.clientId%>',
                                                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                                                  button         :    "imgCloseDateFrom",
                                                                                 //align          :    "Tl",
                                                                                  singleClick    :    true,
                                                                                  showsTime      : true
                                                                                               });
                                                                            </script></td>
                                                                            <td class="textbold">
                                                                                Close Date To</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCloseDateTo" runat="server" CssClass="textbox" Width="137px" TabIndex="11"></asp:TextBox>
                                                                                <img tabIndex="12"  id="imgCloseDateTo" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                                <script type="text/javascript">
                                                                                  Calendar.setup({
                                                                                  inputField     :    '<%=txtCloseDateTo.clientId%>',
                                                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                                                  button         :    "imgCloseDateTo",
                                                                                 //align          :    "Tl",
                                                                                  singleClick    :    true,
                                                                                  showsTime      : true
                                                                                               });
                                                                            </script>
                                                                                </td>
                                                                            <td>
                                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="41" Width="114px" AccessKey="R" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                        <td></td>
                                                                          <td  colspan ="2" align ="left" style ="width:50%"><div id="dv1Left" style="height :336px;">
                                                                                           <div id="DivOfficeId" runat ="server" style ="empty-cells:hide;height:28px; "  >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            OfficeID</td>
                                                                                                        <td style="width:64%">             <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textbox" MaxLength="9"
                                                                                                                TabIndex="12" Width="170px"></asp:TextBox></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                             <div id="DivLoggedBy" runat ="server"   style ="empty-cells:hide;height:28px;" >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            Logged By</td>
                                                                                                        <td style="width:64%">             
                                                                                                            <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textbox" MaxLength="30" TabIndex="14" Width="170px"></asp:TextBox>
                                                                                                            <img  tabIndex="15"  id="img1" runat="server" alt="Select & Add Logged By" onclick="PopupPage(1)"
                                                                                                                src="../Images/lookup.gif" /></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                            <div id="DivQueryGroup" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            Query Group</td>
                                                                                                        <td style="width:64%" class="textbold">   
                                                                                                            <asp:DropDownList ID="ddlQueryGroup" runat="server" AutoPostBack="True" CssClass="dropdownlist" onkeyup="gotop(this.id)" 
                                                                                                                Width="176px" TabIndex="18">
                                                                                                                <asp:ListItem Selected="True" Value ="">--All--</asp:ListItem>
                                                                                                                <asp:ListItem Value="1">Functional</asp:ListItem>
                                                                                                                <asp:ListItem Value="2">Technical</asp:ListItem>
                                                                                                            </asp:DropDownList></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                            <div id="DivQueryCateg" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            Query Category</td>
                                                                                                        <td style="width:64%" class="textbold">             
                                                                                                            <asp:DropDownList ID="ddlQueryCategory" runat="server" AutoPostBack="True" CssClass="dropdownlist" onkeyup="gotop(this.id)" 
                                                                                                                Width="176px" TabIndex="20">
                                                                                                                <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                            <div id="DivQueryStatus" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            Query Status</td>
                                                                                                        <td style="width:64%" class="textbold">             <asp:DropDownList ID="ddlQueryStatus" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)" 
                                                                                                                Width="176px" TabIndex="22">
                                                                                                                <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                                                                                            </asp:DropDownList></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                            <div id="DivCord1" runat ="server"   style ="empty-cells:hide;height:28px;">
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            Coordinator1</td>
                                                                                                        <td style="width:64%" class="textbold">             <asp:DropDownList ID="ddlCoordinator1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)" 
                                                                                                                Width="176px" TabIndex="24">
                                                                                                            </asp:DropDownList></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                             <div id="DivDateAssign" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            Date Assigned</td>
                                                                                                        <td style="width:64%" class="textbold"><asp:TextBox ID="txtDateAssigned" runat="server" CssClass="textbox" Width="170px" TabIndex="26"></asp:TextBox>
                                                                                                             <img tabIndex="27"  id="imgDateAssigned" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />                                                
                                                                                                             <script type="text/javascript">
                                                                                                            Calendar.setup({
                                                                                                            inputField     :    '<%=txtDateAssigned.clientId%>',
                                                                                                            ifFormat       :    "%d/%m/%Y",
                                                                                                            button         :    "imgDateAssigned",
                                                                                                            //align          :    "Tl",
                                                                                                            singleClick    :    true
                                                                                                               });
                                                                                                          </script></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                            <div id="Div1AOffice" runat ="server"  style ="empty-cells:hide;height:28px;"  >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr valign="top">
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            1A Office</td>
                                                                                                        <td style="width:64%" class="textbold"><asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="176px" onkeyup="gotop(this.id)"  TabIndex="29">
                                                                                                            </asp:DropDownList></td> 
                                                                                                    </tr>
                                                                                                </table>                                                                                                                                                   
                                                                                           </div>
                                                                                             <div id="DivWorkOrderNo" runat ="server"   style ="empty-cells:hide;height:28px;" >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            Work Order No.</td>
                                                                                                        <td style="width:64%" class="textbold">             <asp:TextBox ID="txtWorkOrderNo" runat="server" CssClass="textbox" Width="170px"  TabIndex="31" MaxLength="9"></asp:TextBox></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                             <div id="DivAddress" runat ="server"  style ="empty-cells:hide;height:28px;"  >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%">
                                                                                                            Address</td>
                                                                                                        <td style="width:64%" class="textbold">             <asp:TextBox ID="txtAddresses" runat="server" CssClass="textbox" Width="170px" TabIndex="33" MaxLength="100"></asp:TextBox></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                             <div id="DivDispLastCall" runat ="server"   style ="empty-cells:hide;height:28px;" >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%; height: 20px;">
                                                                                                            Display Last Call</td>
                                                                                                        <td style="width:64%; height: 20px;" class="textbold">             
                                                                                                       <asp:CheckBox ID="chkDisplayLastCall" runat="server" CssClass="textbold" onclick="fillDate()" Checked="True" TabIndex="35" /></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>
                                                                                              <div id="DivQueryCategTitle" runat ="server"   style ="empty-cells:hide;height:28px;" >
                                                                                                <table width="100%" border="0" align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                                     <tr>
                                                                                                        <td class="textbold" style="width:36%; height: 20px;">
                                                                                                            Title</td>
                                                                                                        <td style="width:64%; height: 20px;" class="textbold">             
                                                                                                            <asp:TextBox ID="txtTit" runat="server" CssClass="textbox" MaxLength="100" TabIndex="37"
                                                                                                                Width="170px"></asp:TextBox></td> 
                                                                                                    </tr>
                                                                                                </table>
                                                                                           </div>                                                                                           
                                                                                      </div> </td>
                                                                                    <td colspan ="2"  align ="left" style ="width:50%">
                                                                                     <div id="dv1Reight" style="height :336px;"> 
                                                                                           <div id="DivCustCateg" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                             <table  width="100%" border="0"  align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                             <tr>       
                                                                                                <td class="textbold" style="width:36%"> Customer Category
                                                                                                </td>
                                                                                                   <td style="width:64%" >
                                                                                                       &nbsp;<asp:DropDownList ID="ddlCustomerCategory" runat="server" CssClass="dropdownlist"
                                                                                                        onkeyup="gotop(this.id)"    Width="145px" TabIndex="13">
                                                                                                       </asp:DropDownList>
                                                                                                  </td> 
                                                                                              </tr>
                                                                                            </table> 
                                                                                         </div>
                                                                                          <div id="DivCallName" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                         <table  width="100%" border="0"  align="left" cellpadding="0"  cellspacing="1" >                                                            
                                                                                             <tr>       
                                                                                                <td class="textbold" style="width:36%"> Caller Name </td>
                                                                                                   <td style="width:64%" >
                                                                                                       &nbsp;<asp:TextBox id="txtCallerName" tabIndex=16 runat="server" CssClass="textbox" Width="139px" MaxLength="30"></asp:TextBox><IMG tabIndex="17"  id="Img4" onclick="PopupPage(3);" alt="Select & Add Caller Name" src="../Images/lookup.gif" runat="server" style ="display :none;" />                                
                                                                                                  </td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                       <div id="DivQuerySubGroup" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold">  Query Sub Group
                                                                                                  </td>
                                                                                                   <td style="width:64%" class="textbold"> &nbsp;<asp:DropDownList ID="ddlQuerySubGroup" runat="server" AutoPostBack="True" CssClass="dropdownlist" onkeyup="gotop(this.id)" 
                                                                                                           Width="145px" TabIndex="19">
                                                                                                           <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                                       </asp:DropDownList>&nbsp;</td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                       <div id="DivQuerySubCategory" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold"> Query Sub Category
                                                                                                  </td>
                                                                                                   <td style="width:64%" class="textbold"> &nbsp;<asp:DropDownList ID="ddlQuerySubCategory" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)" 
                                                                                                           Width="145px" TabIndex="21">
                                                                                                           <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                                       </asp:DropDownList>&nbsp;</td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                       <div id="DivSevirity" runat ="server"  style ="empty-cells:hide;height:28px;">
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold"> Sevirity</td>
                                                                                                   <td style="width:64%" class="textbold"> &nbsp;<asp:DropDownList ID="ddlQueryPriority" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"  Width="145px" TabIndex="23">
                                                                                                           <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                                       </asp:DropDownList>&nbsp;</td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                       <div id="DivCord2" runat ="server"  style ="empty-cells:hide;height:28px;">
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold"> Coordinator2</td>
                                                                                                   <td style="width:64%" class="textbold"> &nbsp;<asp:DropDownList ID="ddlCoordinator2" runat="server" CssClass="dropdownlist" Width="145px" onkeyup="gotop(this.id)"  TabIndex="25">
                                                                                                       </asp:DropDownList>&nbsp;</td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                      <div id="DivDispos" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold"> Disposition</td>
                                                                                                   <td style="width:64%" class="textbold"> &nbsp;<asp:DropDownList ID="ddlDisposition" runat="server" CssClass="dropdownlist" Width="145px" onkeyup="gotop(this.id)"  TabIndex="28">
                                                                                                       </asp:DropDownList>&nbsp;</td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                        <div id="DivAgency1AOffice" runat ="server"   style ="empty-cells:hide;height:28px;">
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr valign="top">       
                                                                                                <td style="width:36%" class="textbold"> Agency 1A Office</td>
                                                                                                   <td style="width:64%" class="textbold"> &nbsp;<asp:DropDownList ID="ddlAgencyAOffice" runat="server" CssClass="dropdownlist" Width="145px" onkeyup="gotop(this.id)"  TabIndex="30">
                                                                                                       </asp:DropDownList>&nbsp;</td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                       <div id="DivFollowUp" runat ="server"  style ="empty-cells:hide;height:28px;" >
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold"> 
                                                                                                            Follow up</td>
                                                                                                   <td style="width:64%" class="textbold"> &nbsp;<asp:DropDownList ID="drpFollowup" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"  Width="145px" TabIndex="32">
                                                                                                   </asp:DropDownList></td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                        <div id="DivassigneTo" runat ="server"   style ="empty-cells:hide;height:28px;">
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold"> Assigned To</td>
                                                                                                   <td style="width:64%" class="textbold"> &nbsp;<asp:DropDownList ID="drpAssignedTo" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"  Width="145px" TabIndex="34">
                                                                                                   </asp:DropDownList></td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                        <div id="DivConType" runat ="server"   style ="empty-cells:hide;height:28px;">
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold"> Contact Type</td>
                                                                                                   <td style="width:64%" class="textbold">&nbsp;<asp:DropDownList id="ddlContactType" tabIndex=36 onkeyup="gotop(this.id)" runat="server" CssClass="textbold" Width="145px">
                                                                                                     </asp:DropDownList></td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>
                                                                                       <div id="Div2" runat ="server"   style ="empty-cells:hide;height:28px;">
                                                                                         <table  width="100%" border="0" cellpadding="0"  cellspacing="1"   align="left">                                                            
                                                                                             <tr>       
                                                                                                <td style="width:36%" class="textbold">&nbsp;</td>
                                                                                                   <td style="width:64%" class="textbold">&nbsp;<asp:DropDownList id="DropDownList1" tabIndex=38 onkeyup="gotop(this.id)" runat="server" CssClass="textbold" Width="145px" Visible="False">
                                                                                                   </asp:DropDownList></td> 
                                                                                              </tr>
                                                                                         </table> 
                                                                                      </div>                                                                                      
                                                                                 </div></td>
                                                                        <td></td>
                                                                          
                                                                          </tr>  
                                                                                <tr>
                                                                                    <td class="center" colspan="6" style="height: 8px">
                                                                                        <input id="hdFromTime" runat="server" enableviewstate="true" style="width: 1px" type="hidden" /><input
                                                                                            id="hdToTime" runat="server" enableviewstate="true" style="width: 1px" type="hidden" /><input
                                                                                                id="hdAgencyName" runat="server" style="width: 1px" type="hidden" /><input id="hdLoggedBy"
                                                                                                    runat="server" style="width: 1px" type="hidden" /><input id="hdCallCallerName" runat="server"
                                                                                                        style="width: 1px" type="hidden" />
                                                                                                        <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />&nbsp;</td>
                                                                                </tr>
                                                                                
                                                                                
                                                                                  <tr>
                                                                            <td>
                                                                                </td>
                                                                            <td class="textbold">
                                                                                Select Set</td>                                                                               
                                                                            <td colspan="3"><asp:DropDownList ID="drpSelectSet" runat="server" CssClass="dropdownlist" Width="70px" TabIndex="37" AutoPostBack="True">
                                                                             
                                                                            </asp:DropDownList></td>
                                                                            <td style="width: 12%;" class="center">
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
</body>
<script language="javascript" type="text/javascript">
   
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''            
   if (compareDates(document.getElementById('<%=txtQueryOpenedDateFrom.ClientId%>').value,"dd/MM/yyyy HH:mm",document.getElementById('<%=txtQueryOpenedDateTo.ClientId%>').value,"dd/MM/yyyy HH:mm")==1)
       {
            document.getElementById('<%=lblError.ClientId%>').innerText ='Open date to should be greater than or equal to open date from.';
            return false;
       }
       var dtFrom=document.getElementById("txtCloseDateFrom").value;
       var dtTo=document.getElementById("txtCloseDateTo").value;
        dtFrom=dtFrom.trim();
        dtTo=dtTo.trim();
       if (compareDates(dtFrom,"dd/MM/yyyy HH:mm",dtTo,"dd/MM/yyyy HH:mm")==1)
       {
            document.getElementById('<%=lblError.ClientId%>').innerText ='Close date to should be greater than or equal to close date from.';
            return false;
       }
           
       
            
       return true; 
        
    }
    
   
    

    </script>
</html>
