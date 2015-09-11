<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_IR.aspx.vb" Inherits="HelpDesk_HDUP_IR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HelpDesk: IR</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <!-- import the calendar script -->
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <!-- import the language module -->
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <!-- import the calendar setup module -->
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
     <script language="javascript" type="text/javascript">
  function IRValidate()
  {
           
            if(document.getElementById("txtAgencyName").value.trim()=='')
                 {
                     document.getElementById("lblError").innerHTML='Agency Name is Mandatory.';
                     document.getElementById("txtAgencyName").focus();
                    return false;
                 }
              
                 
                  if(document.getElementById("txtIRNo").value.trim()=='')
                 {
                     document.getElementById("lblError").innerHTML='IR Number is Mandatory.';
                     document.getElementById("txtIRNo").focus();
                    return false;
                 } 
                     if(document.getElementById('drpSeverity').value=='')
                    {  
                     document.getElementById("lblError").innerHTML='Severity  is Mandatory.';
                     document.getElementById("drpSeverity").focus();
                    return false;
                    }
                     if(document.getElementById('drpFollowup').value=='')
                    {  
                     document.getElementById("lblError").innerHTML='FollowUp  is Mandatory.';
                     document.getElementById("drpFollowup").focus();
                    return false;
                    }
                   if(document.getElementById("drpStatus").selectedIndex=="0")
                    {
                        document.getElementById("lblError").innerHTML='Status  is Mandatory.';
                        document.getElementById("drpStatus").focus();
                        return false;
                    }   
                  
                 
                     if(document.getElementById('drpType').value=='')
                    {  
                     document.getElementById("lblError").innerHTML='Type  is Mandatory.';
                     document.getElementById("drpType").focus();
                    return false;
                    }
                  
                        if(document.getElementById("txtIRTitle").value.trim()=='')
                 {
                     document.getElementById("lblError").innerHTML='IR Title is Mandatory.';
                     document.getElementById("txtIRTitle").focus();
                    return false;
                 }     
                    if(document.getElementById('drpAssignedTo').value=='')
                    {  
                     document.getElementById("lblError").innerHTML='Assigned To  is Mandatory.';
                     document.getElementById("drpAssignedTo").focus();
                    return false;
                    }   
       }
  
  function  ShowAssigneeHis()
  {
      var hdIRsID=document.getElementById("hdEnIRID").value.trim();
      if (hdIRsID !="")
      {
          
           var type="../Popup/PUSR_IRAssigneeHistory.aspx?AssigneeID="+hdIRsID
              
              if (window.showModalDialog) 
                {
                window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
                }
                 else
                    {  
                    window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1,status=1');     
                    }
       }
   }
     

     
      function  ShowIRHistory()
  {
      var IRsID=document.getElementById("hdEnIRID").value.trim();
      if (IRsID !="")
      {
           var type="../Popup/PUSR_IRHistory.aspx?IRID="+IRsID
              
              if (window.showModalDialog) 
                {
                window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
                }
                 else
                    {  
                    window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1,status=1');     
                    }
         }
    }
  
    function PopupAgencyPage()
{
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	     //   window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
  }
  
  
  function AssignCurrentDate()
  {  
     if(document.getElementById("drpStatus").selectedIndex==0)
     {
     document.getElementById("txtCloseDt").value=document.getElementById("hdCurrentVal").value.trim();
     }
     else
     {
     document.getElementById("txtCloseDt").value="";
    }     
  }

//code for closing and returning value
    function fnIRID()
    {
   // debugger;
    if (document.getElementById("hdRIRId").value != "")
    {
         if (window.opener.document.forms['form1']['txtIRNumber']!=null)
        { 
        window.opener.document.forms['form1']['txtIRNumber'].value=document.getElementById("hdRIRNO").value;
        window.opener.document.forms['form1']['hdIRNo'].value=document.getElementById("hdRIRId").value;
        window.opener.document.forms['form1']['hdEnIRNo'].value=document.getElementById("hdEnRIRId").value;
       // window.opener.document.forms['form1']['ddlQueryStatus'].value=document.getElementById("drpStatus").value; 
                
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
            
                      window.opener.document.forms['form1']['hdMultiIR'].value=strData;
	                 
                    window.close();
                     window.opener.fnMultiIRddl(document.getElementById("ddlPageNumber").value.split("|")[0]);
                    return false;
        }    
    }
    else
    {
    window.close();
        return false;
    }
    }
    
    function fnIRIDClose()
    {
    try
    {
        if (document.getElementById("hdRIRId").value != "")
        {
             if (window.opener.document.forms['form1']['txtIRNo']!=null)
            { 
            window.opener.document.forms['form1']['txtIRNo'].value=document.getElementById("hdRIRNO").value;
            window.opener.document.forms['form1']['hdIRNo'].value=document.getElementById("hdRIRId").value;
            window.opener.document.forms['form1']['hdEnIRNo'].value=document.getElementById("hdEnRIRId").value;
           // window.opener.document.forms['form1']['ddlQueryStatus'].value=document.getElementById("drpStatus").value; 
            return false;
            }    
        }
      
     }
        catch(err){}
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">HelpDesk-></span><span class="sub_menu">IR</span>
                                        </td>
                                        <td class="right">
                                            <asp:LinkButton ID="lnkClose" CssClass="LinkButtons" runat="server" OnClientClick="return fnIRID()">Close</asp:LinkButton>
                                            &nbsp; &nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center" colspan="2">
                                            Manage IR
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP"  colspan="6"  height="20px">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 86px" colspan="6">
                                                    </td>
                                                </tr>
                                                <tr height="20px">
                                                    <td colspan="5" class="subheading" style="height: 10px">
                                                        &nbsp;&nbsp; Agency Details
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr height="3px">
                                                  <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px">
                                                    </td>
                                                    <td class="textbold" style="width: 151px">
                                                        Agency<span class="Mandatory">*</span></td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtAgencyName" CssClass="textboxgrey" runat="server" Width="528px"
                                                            ReadOnly="True" TabIndex="1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSave" runat="server" TabIndex="19" CssClass="button" Text="Save"
                                                            Width="104px" AccessKey="s" /></td>
                                                </tr>
                                               <tr height="3px">
                                                  <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 82px;">
                                                    </td>
                                                    <td class="textbold" style="width: 151px; height: 82px;" valign="top">
                                                        Address</td>
                                                    <td colspan="3" style="height: 82px" valign="top">
                                                        <asp:TextBox ID="txtAgencyAddress" runat="server" CssClass="textboxgrey" TabIndex="2"
                                                            TextMode="MultiLine" Height="90px" ReadOnly="True" Rows="5" Width="528px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 187px; height: 82px;" valign="top">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" runat="server" TabIndex="20" CssClass="button" Text="New"
                                                                        Width="104px" AccessKey="n" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="20" CssClass="button" Text="Reset"
                                                                        Width="104px" AccessKey="r" />
                                                                </td>
                                                            </tr>
                                                            <tr height="3px">
                                                             <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 22px">
                                                                    <input type="button" id="btnHistory" runat="server" tabindex="21" class="button"
                                                                        value="History" style="width: 104px" onclick="javascript:return ShowIRHistory();"
                                                                        accesskey="h" />
                                                                </td>
                                                            </tr>
                                                            <tr height="3px">
                                                             <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 22px">
                                                                    <input type="button" id="btnAssignHistory" runat="server" tabindex="25" class="button"
                                                                        value="Assignee  History" style="width: 104px" onclick="javascript:return ShowAssigneeHis();" />
                                                                </td>
                                                            </tr>
                                                            <tr height="3px">
                                                             <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnLtr" runat="server" TabIndex="22" CssClass="button" Text="LTR"
                                                                        Width="104px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"></td>
                                                </tr>
                                                <tr valign="top">
                                                    <td class="textbold" style="width: 86px">
                                                    </td>
                                                    <td style="width: 151px" class="textbold">
                                                        City</td>
                                                    <td style="width: 257px">
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="40" ReadOnly="True"
                                                            Width="178px" TabIndex="3"></asp:TextBox></td>
                                                    <td style="height: 26px; width: 178px;" class="textbold">
                                                        Country</td>
                                                    <td style="height: 26px; width: 245px;">
                                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                            Width="160px" ReadOnly="true" TabIndex="4"></asp:TextBox></td>
                                                    <td style="width: 187px; height: 26px;">
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 23px;">
                                                    </td>
                                                    <td class="textbold" style="width: 151px; height: 23px;">
                                                        Phone</td>
                                                    <td style="width: 257px; height: 23px;">
                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" MaxLength="40" Width="178px"
                                                            ReadOnly="True" TabIndex="5"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 178px; height: 23px;">
                                                        Office ID
                                                    </td>
                                                    <td style="width: 245px; height: 23px;">
                                                        <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textboxgrey" MaxLength="20"
                                                            ReadOnly="true" TabIndex="6" Width="160px"></asp:TextBox></td>
                                                    <td style="width: 187px; height: 23px;">
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 21px;">
                                                    </td>
                                                    <td class="textbold" style="width: 151px; height: 21px;">
                                                        Fax</td>
                                                    <td style="width: 257px; height: 21px;">
                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" MaxLength="40" Width="178px"
                                                            ReadOnly="True" TabIndex="7"></asp:TextBox></td>
                                                    <td style="width: 178px; height: 21px;" class="textbold">
                                                        Online Status</td>
                                                    <td style="height: 21px; width: 245px;">
                                                        <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                            Width="160px" TabIndex="8"></asp:TextBox></td>
                                                    <td style="height: 21px">
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"> </td>
                                                </tr>
                                                <tr height="20px">
                                                    <td colspan="5" class="subheading" style="height: 10px">
                                                        &nbsp; &nbsp;IR Details
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr height="3px" colspan="6">
                                                 <td colspan ="6" ></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 21px;">
                                                    </td>
                                                    <td class="textbold" style="width: 151px; height: 21px;">
                                                        IR Number<span class="Mandatory">*</span></td>
                                                    <td style="width: 257px; height: 21px;">
                                                        <asp:TextBox ID="txtIRNo" runat="server" CssClass="textbox" MaxLength="15" Width="178px"
                                                            TabIndex="9" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 178px; height: 21px;">
                                                        Severity<span class="Mandatory">*</span></td>
                                                    <td style="height: 21px; width: 245px;">
                                                        <asp:DropDownList ID="drpSeverity" runat="server" TabIndex="10" Width="163px" CssClass="dropdownlist">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="height: 21px">
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 22px;">
                                                    </td>
                                                    <td class="textbold" style="width: 151px; height: 22px;">
                                                        Follow Up<span class="Mandatory">*</span></td>
                                                    <td style="width: 257px; height: 22px;">
                                                        <asp:DropDownList ID="drpFollowup" runat="server" TabIndex="11" Width="182px" CssClass="dropdownlist">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="textbold" style="width: 178px; height: 22px;">
                                                        Status<span class="Mandatory">*</span>
                                                    </td>
                                                    <td style="height: 22px; width: 245px;">
                                                        <asp:DropDownList ID="drpStatus" runat="server" TabIndex="12" Width="163px" CssClass="dropdownlist">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 22px">
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 21px;">
                                                    </td>
                                                    <td class="textbold" style="width: 151px; height: 21px;">
                                                        Open Date</td>
                                                    <td style="width: 257px; height: 21px;">
                                                        <asp:TextBox ID="txtOpenDt" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                            Width="178px" TabIndex="13" ReadOnly="True"></asp:TextBox>
                                                       
                                                    </td>
                                                    <td class="textbold" style="width: 178px; height: 21px;">
                                                        Close Date</td>
                                                    <td style="height: 21px; width: 245px;">
                                                        <asp:TextBox ID="txtCloseDt" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                            Width="160px" TabIndex="14" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 187px; height: 21px;">
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 86px">
                                                    </td>
                                                    <td style="width: 151px" class="textbold">
                                                        Type<span class="Mandatory">*</span></td>
                                                    <td style="width: 257px">
                                                        <asp:DropDownList ID="drpType" runat="server" TabIndex="15" Width="182px" CssClass="dropdownlist">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 178px">
                                                    </td>
                                                    <td style="width: 245px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px">
                                                    </td>
                                                    <td class="textbold" style="width: 151px" valign="top">
                                                        IR Title<span class="Mandatory">*</span></td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtIRTitle" runat="server" CssClass="textbox" MaxLength="300" Width="528px"
                                                            Height="88px" TextMode="MultiLine" TabIndex="16"></asp:TextBox></td>
                                                    <td style="height: 23px">
                                                    </td>
                                                </tr>
                                                <tr height="3px">
                                                 <td colspan ="6"></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 21px;">
                                                    </td>
                                                    <td class="textbold" style="width: 151px; height: 21px;">
                                                        Assigned To<span class="Mandatory">*</span></td>
                                                    <td style="width: 257px; height: 21px;">
                                                        <asp:DropDownList ID="drpAssignedTo" runat="server" TabIndex="17" Width="184px" CssClass="dropdownlist">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="height: 21px; width: 178px;">
                                                        Assigned Date</td>
                                                    <td style="height: 21px; width: 245px;">
                                                        <asp:TextBox ID="txtAssignedDt" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                            Width="160px" TabIndex="18" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 21px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 21px">
                                                    </td>
                                                    <td class="textbold" style="width: 151px; height: 21px">
                                                    </td>
                                                    <td style="width: 257px; height: 21px">
                                                    </td>
                                                    <td class="textbold" style="width: 178px; height: 21px">
                                                    </td>
                                                    <td style="width: 245px; height: 21px">
                                                    </td>
                                                    <td style="height: 21px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 86px; height: 21px">
                                                    </td>
                                                    <td colspan="4" style="height: 21px">
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
                                                    <td style="height: 21px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 11px; width: 86px;">
                                                        &nbsp;</td>
                                                    <td colspan="4" class="ErrorMsg" style="height: 11px">
                                                        Field Marked * are Mandatory</td>
                                                    <td style="height: 11px; width: 187px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdAddress" runat="server" />
                                <asp:HiddenField ID="hdLcode" runat="server" />
                                <asp:HiddenField ID="hdEnLcode" runat="server" />
                                <asp:HiddenField ID="hdAssigneeID" runat="server" />
                                <asp:HiddenField ID="hdIRID" runat="server" />
                                <asp:HiddenField ID="hdEnIRID" runat="server" />
                                <asp:HiddenField ID="hdRequestID" runat="server" EnableViewState="False" />
                                <asp:HiddenField ID="hdEnRequestID" runat="server" />
                                <input type="hidden" runat="server" id="hdRIRId" style="width: 1px" />
                                <input type="hidden" runat="server" id="hdEnRIRId" style="width: 1px" />
                                <input type="hidden" runat="server" id="hdRIRNO" style="width: 1px" />
                                <input type="hidden" runat="server" id="hdTempMultiIRData" style="width: 1px" />
                                <input type="hidden" runat="server" id="hdTotalNoOfRecords" style="width: 1px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
