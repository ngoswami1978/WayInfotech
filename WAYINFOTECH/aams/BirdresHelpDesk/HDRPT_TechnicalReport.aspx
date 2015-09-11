<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDRPT_TechnicalReport.aspx.vb" Inherits="BirdresHelpDesk_HDRPT_TechnicalReport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::HelpDesk::Technical Report</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"      title="win2k-cold-1" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
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
     
     
     function ValidateForm()
     {
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
         
    
          if (document.getElementById("txtCloseDateFrom1")!=null)
         {       
              if (document.getElementById('txtCloseDateFrom1').value.trim().length>0)
            {       
               if (isDate(document.getElementById('txtCloseDateFrom1').value.trim(),"dd/MM/yyyy HH:mm") == false)	
                {
                    document.getElementById('lblError').innerText ='Close date from is not valid.';
                    document.getElementById("txtCloseDateFrom1").focus();
                    return false;   
                }         
            }
         }
           
           if (document.getElementById("txtCloseDateTo1")!=null)
         {     
                if (document.getElementById('txtCloseDateTo1').value.trim().length>0)
               {           
                   if (isDate(document.getElementById('txtCloseDateTo1').value.trim(),"dd/MM/yyyy HH:mm") == false)	
                    {
                        document.getElementById('lblError').innerText ='Close date to is not valid.';
                        document.getElementById("txtCloseDateTo1").focus();
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
            
              if (document.getElementById('txtCloseDateFrom1').value.trim().length>0 &&  document.getElementById('txtCloseDateTo1').value.trim().length>0)
               {           
                 var dtFrom=document.getElementById("txtCloseDateFrom1").value;
                 var dtTo=document.getElementById("txtCloseDateTo1").value;
                 dtFrom=dtFrom.trim();
                 dtTo=dtTo.trim();
                   if (compareDates(dtFrom,"dd/MM/yyyy HH:mm",dtTo,"dd/MM/yyyy HH:mm")=='1')
                   {
                        document.getElementById('lblError').innerText ='Close date to should be greater than or equal to close date from.';
                         document.getElementById("txtCloseDateTo1").focus();
                        return false;
                   }               
               }
         
          
     }
     
     
      </script>
      
   
    </head>
<body>
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnExtract">
        <div>
            <table>
                <tr>
                    <td>
                        <table width="860px" class="border_rightred left">
                            <tr>
                                <td class="top">
                                    <table width="100%" class="left">
                                        <tr>
                                            <td>
                                                <span class="menu">Birdres HelpDesk -&gt;</span><span class="sub_menu">Technical Report</span></td>
                                        </tr>
                                        <tr>
                                            <td class="heading center">
                                                Technical Report</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td class="redborder center">
                                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                <tr>
                                                                    <td class="center" colspan="6">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold">Agency Name</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox TextTitleCase" MaxLength="50"
                                                                            TabIndex="2" Width="528px"></asp:TextBox>
                                                                        <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPageBRCallLog(2)" tabindex="2"
                                                                            src="../Images/lookup.gif" style="cursor: pointer;" /></td>
                                                                    <td style="width: 12%;" class="center">
                                                                        <asp:Button ID="BtnPrint" CssClass="button" runat="server" Text="Display" TabIndex="28"
                                                                            AccessKey="p" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 3%"></td>
                                                                    <td class="textbold" style="width: 15%">LTR No</td>
                                                                    <td style="width: 27%"><asp:TextBox ID="txtLTRNo" runat="server" CssClass="textbox" Width="170px" MaxLength="9"
                                                                            onkeyup="checknumeric(this.id)" TabIndex="3"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 15%">
                                                                        PTR No</td>
                                                                    <td style="width: 26%">
                                                                        <asp:TextBox ID="txtPTRNo" runat="server" CssClass="textbox" Width="170px" MaxLength="9"
                                                                            onkeyup="checknumeric(this.id)" TabIndex="4"></asp:TextBox></td>
                                                                    <td class="center" style="width: 13%">
                                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="29"
                                                                            AccessKey="r" /></td>
                                                                </tr>
                                                                  <tr>
                                                                    <td></td>
                                                                    <td class="textbold">
                                                                        Opened Date From
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQueryOpenedDateFrom" runat="server" CssClass="textbox" Width="170px" TabIndex="5"></asp:TextBox>
                                                                        <img id="imgOpenedDateFrom" alt="" tabindex="6" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

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
                                                                        Opened Date To
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQueryOpenedDateTo" runat="server" CssClass="textbox" Width="170px" TabIndex="7"></asp:TextBox>
                                                                        <img id="imgOpenedDateTo" alt="" tabindex="8" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

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
                                                                    <td align="center"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">Close Date From</td>
                                                                    <td><asp:TextBox ID="txtCloseDateFrom1" runat="server" CssClass="textbox" Width="170px" TabIndex="9"></asp:TextBox>
                                                                        <img id="imgCloseDateFrom" alt="" tabindex="10" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtCloseDateFrom1.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                  button         :    "imgCloseDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true,
                                                  showsTime      : true
                                                               });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">Close Date To</td>
                                                                    <td><asp:TextBox ID="txtCloseDateTo1" runat="server" CssClass="textbox" Width="170px" TabIndex="11"></asp:TextBox>
                                                                        <img id="imgCloseDateTo" alt="" tabindex="12" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtCloseDateTo1.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                  button         :    "imgCloseDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true,
                                                  showsTime      : true
                                                               });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                        <input id="hdQueryCategory" runat="server" style="width: 1px" type="hidden" />
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                    <td></td>
                                                                    <td class="textbold">Query Group</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQueryGroup" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" AutoPostBack="True" TabIndex="13" Enabled="False">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Query Sub Group</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQuerySubGroup" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" AutoPostBack="True" TabIndex="14">
                                                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td class="center"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><input id="hdCallCallerName" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold">Query Category</td>
                                                                    <td><asp:DropDownList ID="ddlQueryCategory" runat="server" CssClass="dropdownlist" Width="176px"
                                                                            AutoPostBack="True" TabIndex="15" onkeyup="gotop(this.id)">
                                                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Query Sub Category</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQuerySubCategory" runat="server"
                                                                            CssClass="dropdownlist" Width="176px" TabIndex="16">
                                                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">Query Status</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQueryStatus" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="17">
                                                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="textbold">Query Priority</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQueryPriority" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="18">
                                                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input id="hdLoggedBy" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold">Logged By</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textbox" MaxLength="50" TabIndex="19"
                                                                            Width="170px"></asp:TextBox>
                                                                        <img id="img1" runat="server" alt="Select & Add Logged By" onclick="PopupPageBRCallLog(1)" tabindex="20"
                                                                            src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                    <td class="textbold">
                                                                        Call Assigned To</td>
                                                                    <td><asp:DropDownList ID="drpAssignedTo" runat="server" CssClass="dropdownlist"
                                                                            onkeyup="gotop(this.id)" TabIndex="24" Width="176px">
                                                                        </asp:DropDownList></td>
                                                                    <td class="center"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">1A Office</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlAOffice" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="25">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Agency 1A Office</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlAgencyAOffice" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="26">
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                              
                                                            
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td><asp:CheckBox ID="chkDisplayLastCall" runat="server" CssClass="textbold" onClick="fillDateCallLog(1)"
                                                                            TabIndex="27" Visible="False" /></td>
                                                                    <td class="textbold"></td>
                                                                    <td></td>
                                                                    <td>
                                                                        <asp:Button ID="btnExtract" CssClass="button" runat="server" Text="Extract" TabIndex="31"
                                                                            AccessKey="E" Visible="False" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td></td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input id="hdRowID" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td>
                                                                    <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                    <td colspan="4">
                                                                        <input id="hdFromTime" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <input id="hdToTime" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <input id="hdPendingTime" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <input id="hdData" runat="server" style="width: 1px" type="hidden" />
                                                                        <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                        <asp:HiddenField ID="hdLcodeMuk" runat="server" />
                                                                        <asp:HiddenField ID="hdReIDMuk" runat="server" />
                                                                        <asp:HiddenField ID="hdStatusMuk" runat="server" />
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
               
            </table>
        </div>
    </form>
</body>
</html>
