<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SARPT_SALESDynamicReport.aspx.vb" Inherits="Sales_SARPT_SALESDynamicReport" %>

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
         type="../Popup/PUSR_SalesResultConfigure.aspx?Action=U&Popup=T&CurrentSet=" + CurrentSet;
         window.open(type,"ff","height=600,width=900,top=30,left=20,scrollbars=1,status=1,resizable =1");	     

           return false;
       } 


         function  SearchConfigure()
       {   
        //debugger;
         var CurrentSet= document.getElementById("drpSelectSet").value
         type="../Popup/PUSR_SalesSearchConfigure.aspx?Action=U&Popup=T&CurrentSet=" + CurrentSet;
         window.open(type,"ff","height=600,width=900,top=30,left=20,scrollbars=1,status=1,resizable = 1");	     

           return false;
       } 
   


    function PopupPage(id)
         {
         //debugger;
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
                type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
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
                    document.getElementById('lblError').innerHTML ='Open date from is not valid.';
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
                    document.getElementById('lblError').innerHTML ='Open date to is not valid.';
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
                    document.getElementById('lblError').innerHTML ='Close date from is not valid.';
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
                        document.getElementById('lblError').innerHTML ='Close date to is not valid.';
                        document.getElementById("txtCloseDateTo").focus();
                        return false;   
                    }         
                 }
         }
         
             if (document.getElementById('txtQueryOpenedDateFrom').value.trim().length>0 &&  document.getElementById('txtQueryOpenedDateTo').value.trim().length>0)
              {
                  if (compareDates(document.getElementById('txtQueryOpenedDateFrom').value,"dd/MM/yyyy HH:mm",document.getElementById('txtQueryOpenedDateTo').value,"dd/MM/yyyy HH:mm")=='1')
                {
                    document.getElementById('lblError').innerHTML ='Open date to should be greater than or equal to open date from.';
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
                        document.getElementById('lblError').innerHTML ='Close date to should be greater than or equal to close date from.';
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
                           document.getElementById('lblError').innerHTML = "Assigned date is not valid.";			
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
                            <span class="menu">Sales-&gt;</span><span class="sub_menu">Sales Report</span>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Sales &nbsp;Report</td>
                        </tr>
                        <tr>
                            <td valign="top" >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder" style="width: 849px"> 
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
                                                                            <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="30" TabIndex="1" Width="575px"></asp:TextBox>
                                                                            <img id="Img2" runat="server" alt="" tabIndex="2" onclick="PopupPage(2)"  src="../Images/lookup.gif" /></td>
                                                                            <td style="width: 12%;" class="center"><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Display" TabIndex="46" Width="125px" AccessKey="D" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                </td>
                                                                            <td class="textbold">
                                                                                Lcode</td>
                                                                            <td style="width: 224px">
                                                                                <asp:TextBox ID="txtLcode1" runat="server" CssClass="textbox" Width="170px" TabIndex="3"  MaxLength="9"></asp:TextBox></td>
                                                                            <td class="textbold" style="width: 15%">
                                                                                Group Code</td>
                                                                            <td style="width: 26%">
                                                                                <asp:TextBox ID="txtChainCode1" runat="server" CssClass="textbox" Width="136px" TabIndex="4"  MaxLength="9"></asp:TextBox></td>
                                                                            <td class="center" style="width: 13%"><asp:Button id="btnSearchConfigure" tabIndex=47 runat="server" CssClass="button" Width="125px" Text="Configure Search" AccessKey="C"></asp:Button></td>
                                                                        </tr>                                     
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Date From </td>
                                                                            <td style="width: 224px">
                                                                                <asp:TextBox ID="txtQueryOpenedDateFrom" runat="server" CssClass="textbox" TabIndex="5"
                                                                                    Width="170px"></asp:TextBox><img id="imgOpenedDateFrom" alt="" src="../Images/calender.gif"
                                                                                        style="cursor: pointer" tabindex="6" title="Date selector" />
                                                                                <script type="text/javascript">
                                                                                  Calendar.setup({
                                                                                  inputField     :    '<%=txtQueryOpenedDateFrom.clientId%>',
                                                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                                                  button         :    "imgOpenedDateFrom",
                                                                                 //align          :    "Tl",
                                                                                  singleClick    :    true,
                                                                                  showsTime      : true
                                                                                               });
                                                                            </script>
                                                                                </td>
                                                                            <td class="textbold">
                                                                                Date To <%--<span class="Mandatory">*</span>--%>
                                                                  </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtQueryOpenedDateTo" runat="server" CssClass="textbox" TabIndex="7"
                                                                                    Width="136px"></asp:TextBox><img id="imgOpenedDateTo" alt="" src="../Images/calender.gif"
                                                                                        style="cursor: pointer" tabindex="8" title="Date selector" />
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
                                                                            <td><asp:Button ID="btnResultConfig" CssClass="button" runat="server" Text="Configure Result " TabIndex="48" Width="125px" AccessKey="F" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                  <td>
                                                                  </td>
                                                                  <td class="textbold">
                                                                                <%--<span class="Mandatory">*</span>--%>
                                                                  </td>
                                                                  <td style="width: 224px">
                                                                      &nbsp;</td>
                                                                  <td class="textbold">
                                                                  </td>
                                                                  <td>
                                                                      &nbsp;</td>
                                                                  <td>
                                                                      <asp:Button ID="btmReset" CssClass="button" runat="server" Text="Reset" TabIndex="48" Width="125px" AccessKey="F" /></td>
                                                              </tr>
                                                                        <tr>
                                                                        
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
                                                                            <td colspan="3"><asp:DropDownList ID="drpSelectSet" runat="server" CssClass="dropdownlist" Width="70px" TabIndex="45" AutoPostBack="True">
                                                                             
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
<%--<script language="javascript" type="text/javascript">
   
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerHTML=''            
   if (compareDates(document.getElementById('<%=txtQueryOpenedDateFrom.ClientId%>').value,"dd/MM/yyyy HH:mm",document.getElementById('<%=txtQueryOpenedDateTo.ClientId%>').value,"dd/MM/yyyy HH:mm")==1)
       {
            document.getElementById('<%=lblError.ClientId%>').innerHTML ='Open date to should be greater than or equal to open date from.';
            return false;
       }
       var dtFrom=document.getElementById("txtCloseDateFrom").value;
       var dtTo=document.getElementById("txtCloseDateTo").value;
        dtFrom=dtFrom.trim();
        dtTo=dtTo.trim();
       if (compareDates(dtFrom,"dd/MM/yyyy HH:mm",dtTo,"dd/MM/yyyy HH:mm")==1)
       {
            document.getElementById('<%=lblError.ClientId%>').innerHTML ='Close date to should be greater than or equal to close date from.';
            return false;
       }
           
       
            
       return true; 
        
    }
    
   
    

    </script>--%>
</html>
