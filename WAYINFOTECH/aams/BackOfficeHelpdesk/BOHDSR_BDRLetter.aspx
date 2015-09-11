<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOHDSR_BDRLetter.aspx.vb" Inherits="BOHelpDesk_HDSR_BDRLetter" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
      <title>Back Office Helpdesk BDR Letter</title>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
     <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
     <script type="text/javascript" language ="javascript" > 
     function PopupAgencyPage()
        {
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"Agency","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
        }
//     function  NewHDUPBDRLetter()
//       {    
//           window.location="HDUP_BDRLetter.aspx?Action=I";
//           return false;
//       }  
    function  BDRLetterReset()
    {       
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtAgencyName").value="";  
        document.getElementById("drpReqType").value=""; 
        document.getElementById("txtBDrId").value=""; 
        document.getElementById("txtBDRTicket").value=""; 
        document.getElementById("txtLtrNo").value=""; 
        document.getElementById("drpBdrSentBy").value=""; 
        document.getElementById("drpAirLine").value=""; 
        document.getElementById("drp1Aoffice").value=""; 
        document.getElementById("txtAirLineoffice").value=""; 
        document.getElementById("txtBDRLoggedDateFrom").value=""; 
        document.getElementById("txtBDRLoggedDateTo").value=""; 
        document.getElementById("chkWholeGroup").checked=false;
        return false;
    }
     function BDRLetterMandatory()
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
         return true;
     }
     function DeleteFunction(CheckBoxObj)
      {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
            window.location.href="BOHDSR_BDRLetter.aspx?Action=D|"+CheckBoxObj + "|";
            return false;
        }
    }
      function EditFunction(CheckBoxObj)
    {                
          window.location ="BOHDUP_BDRLetter.aspx?Action=U&HD_RE_BDR_ID=" + CheckBoxObj; 
          return false;
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
      <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" ><span class="menu">Back Office Help Desk-></span><span class="sub_menu">BDR Letter</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >Search BDR Letter</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  align ="left" class="redborder" >                                 
                                                        <table width="100%" border="0"  cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="8" align="center" style="height:25px;" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td  style="width:18%;" class="textbold" ><span class="textbold">Agency</span></td>
                                                                <td colspan="4" style="width:50%;" class="textbold"><asp:TextBox ID="txtAgencyName" runat ="server" CssClass ="textbox" Width="437px" MaxLength="50" TabIndex="1" ></asp:TextBox></td>                                                                
                                                                <td style="width:8%;" class="textbold" ><img src="../Images/lookup.gif"  onclick="javascript:return PopupAgencyPage();" /></td>
                                                                <td style="width:22%;" ><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="18" AccessKey="a" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class ="textbold">Request Type </span></td>
                                                                <td style="width:17%;" class="textbold" ><asp:DropDownList ID="drpReqType" runat ="server" CssClass ="dropdownlist" Width="150px" TabIndex="3"></asp:DropDownList></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;"><span class="textbold">BDR ID</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtBDrId" runat ="server" CssClass ="textbox"  MaxLength="9" TabIndex="4" Width="143px" ></asp:TextBox></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="20" AccessKey="r" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class="textbold">BDR Ticket</span></td>
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtBDRTicket" runat ="server" CssClass ="textbox"  MaxLength="30" TabIndex="5" Width="143px" ></asp:TextBox></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;" ><span class="textbold">LTR No</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox id="txtLtrNo" tabIndex=6 runat="server" CssClass="textbox" MaxLength="9" Width="143px"></asp:TextBox></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class="textbold">BDR Sent by </span></td>
                                                                <td style="width:17%;" class="textbold" ><asp:DropDownList id="drpBdrSentBy" runat="server" CssClass="dropdownlist" Width="150px" TabIndex="7"></asp:DropDownList></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;" ><span class="textbold">Airlines</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:DropDownList id="drpAirLine" runat="server" CssClass="dropdownlist" Width="150px" TabIndex="8"></asp:DropDownList></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%; " class="textbold" ><span class="textbold">1a Office </span></td>
                                                                <td style="width:17%;" class="textbold" ><asp:DropDownList id="drp1Aoffice" runat="server" CssClass="dropdownlist" Width="150px" TabIndex="9"></asp:DropDownList></td>
                                                                <td style="width:1%; " class="textbold"></td>
                                                                <td style="width:15%;" ><span class="textbold">Airlines Office</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtAirLineoffice" runat ="server" CssClass ="textbox"  MaxLength="30" TabIndex="10" Width="143px" ></asp:TextBox></td>
                                                                <td style="width:8%; " class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>                                                          
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td style="width:6%; height: 21px;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%; height: 21px;" class="textbold" ><span class="textbold">BDR Logged Date From</span></td>
                                                                <td style="width:17%; height: 21px;" class="textbold" ><asp:TextBox ID="txtBDRLoggedDateFrom" runat="server" MaxLength="10" TabIndex="13" CssClass="textboxgrey" Width="118px"></asp:TextBox>
                                                                         <img id="f_BDRLoggedDateFrom" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtBDRLoggedDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_BDRLoggedDateFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                                <td style="width:1%; height: 21px;" class="textbold"></td>
                                                                <td style="width:15%; height: 21px;" ><span class="textbold">BDR Logged Date To</span></td> 
                                                                <td style="width:17%; height: 21px;" class="textbold" ><asp:TextBox ID="txtBDRLoggedDateTo" runat="server" MaxLength="10" TabIndex="15" CssClass="textboxgrey" Width="118px"></asp:TextBox>
                                                                         <img id="f_BDRLoggedDateTo" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtBDRLoggedDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_BDRLoggedDateTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                                <td style="width:8%; height: 21px;" class="textbold" ></td>
                                                                <td style="width:22%; height: 21px;" ></td>
                                                            </tr>
                                                                 <tr>
                                                                <td style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class ="textbold">Whole Group</span></td>
                                                                <td style="width:17%;" class="textbold" ><asp:CheckBox ID="chkWholeGroup" runat ="server" TabIndex="17" /></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;"><span class="textbold"></span></td> 
                                                                <td style="width:17%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td  style="height: 15px" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ></td>
                                                                <td style="width:33%;" colspan="3" class="ErrorMsg" >Field Marked * are Mandatory</td>
                                                                <td style="width:17%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="8" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                               <tr>
                                                                <td  class="textbold" colspan="8" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="8" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="830px" border="0" cellspacing="0" cellpadding="0"> 
                                                          <tr>
                                                                                                                                 
					                                                
                                                               <td><asp:GridView ID="gvBDRLetter" runat="server"  AutoGenerateColumns="False" TabIndex="7" width="830px" EnableViewState="False" >
                                                                                 <Columns>
                                                                                 <asp:TemplateField HeaderText="LTR No">
                                                                                                <itemtemplate>
                                                                                                    <%#Eval("LTRNO")%>
                                                                                                    <asp:HiddenField ID="HDHDREBDRID" runat="server" Value='<%#Eval("HD_RE_BDR_ID")%>' />
                                                                                                </itemtemplate>
                                                                                        </asp:TemplateField>                                                                                        
                                                                                        <asp:BoundField DataField="AGENCYNAME"  HeaderText="Agency Name" />
                                                                                        <asp:BoundField DataField="ADDRESS"  HeaderText="Agency Address" />
                                                                                        <asp:BoundField DataField="REQUESTTYPE"  HeaderText="Requested Type" />
                                                                                        <asp:BoundField DataField="HD_RE_BDR_TICKETS"  HeaderText="BDR Letter" />
                                                                                        <asp:BoundField DataField="HD_RE_BDR_DATESEND"  HeaderText="Date Sent" />
                                                                                        <asp:BoundField DataField="STATUS"  HeaderText="Status" />
                                                                                        <asp:BoundField DataField="AIRLINE"  HeaderText="AirLine" />                                                                                                                                                                         
                                                                                        <asp:BoundField DataField="AIRLINEOFFICEADDRESS"  HeaderText="Airline Aoffice Address" />
                                                                                        <asp:BoundField DataField="HD_RE_BDR_SENDBY"  HeaderText="Send By" />
                                                                                        <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                       <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#" class="LinkButtons" id="linkDelete" runat="server">
                                                                                                                    Delete</a>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                                                <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                       </asp:TemplateField>
                                                                                 </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" />
                                                                                        <HeaderStyle CssClass="Gridheading" />
                                                                                        
                                                                  </asp:GridView></td>
                                                         </tr>
                                                        <tr>
                                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">                                                                
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
        </table>
    <!-- Code by Dev Abhishek -->
    </form>
</body>
</html>
