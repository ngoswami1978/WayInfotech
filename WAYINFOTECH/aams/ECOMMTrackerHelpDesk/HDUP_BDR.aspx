<%@ Page Language="VB" ValidateRequest="false" AutoEventWireup="false" CodeFile="HDUP_BDR.aspx.vb"
    Inherits="ETHelpDesk_HDUP_BDR"  EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BDR Letter</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />

    <script src="../JavaScript/ETracker.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript"> 
     function PopupAgencyPage()
        {
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"Agency","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
        function AirLineOfficePage()
        {
            var type;
            type = "../Setup/MSSR_AirLineOffice.aspx?Popup=T" ;
   	        window.open(type,"AirLineOffice","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
        
     function  NewHDUPBDR()
       {    
           var type;
          // window.location="HDUP_BDR.aspx?Popup=T&Action=I&ReqID=123&LCode=779";
           type = "../ECOMMTrackerHelpDesk/HDUP_BDR.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode +"&requestType=" + text1;//'document.getElementById("ddlQueryCategory").value;
   	       window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
           return false;
       }  
   
     function BDRMandatory()
    {
        
            if(document.getElementById("txtBDRTicket").value.trim()=="")
            {
            document.getElementById("lblError").innerHTML="BDR Ticket No is mandatory.";
            document.getElementById("txtBDRTicket").focus();
            return false;
            }
             if(document.getElementById("txtAirLineoffice").value.trim()=="")
            {
            document.getElementById("lblError").innerHTML="Airline Office is mandatory.";
            document.getElementById("txtAirLineoffice").focus();
            return false;
            }
          if (document.getElementById("txtBDRBDRSendDate").value.trim()=="")
             {
                 document.getElementById("lblError").innerHTML="BDR Send date is mandatory.";
                 document.getElementById("txtBDRBDRSendDate").focus();
                 return false;              
             }
           if(document.getElementById('<%=txtBDRBDRSendDate.ClientId%>').value!= '')
            {
                if (isDate(document.getElementById('<%=txtBDRBDRSendDate.ClientId%>').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "BDR Send date is not valid.";			
	               document.getElementById('<%=txtBDRBDRSendDate.ClientId%>').focus();
	               return(false);  
                }
            }
            if(document.getElementById("txtBdrLetter").value.trim()=="")
            {
            document.getElementById("lblError").innerHTML="Bdr letter is mandatory.";
            document.getElementById("txtBdrLetter").focus();
            return false;
            }    
   
       
         return true;
     }
  function EmployeePage()
{
            var type;      
            type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=910,top=30,left=20,scrollbars=1,status=1");	
            return false;
}
  // Written for showing History Page
    function PopupHistoryPage(objBdrId)
    { 
        //alert("abhi");
        // var BDRValue=objBdrId;
         BDRValue= document.getElementById('hdbdrLetterId').value;
         var type="../ECOMMTrackerHelpDeskPopup/PUSR_BDRHistory.aspx?BDRId=" + BDRValue; 
       
   	        window.open(type,"BDRValue","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	
            return false;
    }  
    
    //code for closing and returning value
    function fnBDRID(id)
    {debugger;
        var strData =     ""
        var intLen,intCn;
    try
    {
        if (document.getElementById("hdbdrLetterId").value != "")
        {
             if (window.opener.document.forms['form1']['txtBDRLetterID']!=null)
            { 
            window.opener.document.forms['form1']['txtBDRLetterID'].value=document.getElementById("hdbdrLetterId").value;
            window.opener.document.forms['form1']['hdBDRLetterID'].value=document.getElementById("hdbdrLetterId").value;
            window.opener.document.forms['form1']['hdEnBDRLetterID'].value=document.getElementById("hdEnbdrLetterId").value;
            
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
            
            window.opener.document.forms['form1']['hdMultiBDRID'].value=strData;
            if (id=="1")
            {
                window.close();
            }
            window.opener.fnMultiBDRddl(document.getElementById("ddlPageNumber").value);
            return false;
            }    
        }
        else
        {
        window.close();
            return false;
        }
    }
    catch(err){}
    }
    
//    function fnBDRIDClose()
//    {
//        try
//        {
//            if (document.getElementById("hdbdrLetterId").value != "")
//            {
//                 if (window.opener.document.forms['form1']['txtBDRLetterID']!=null)
//                { 
//                window.opener.document.forms['form1']['txtBDRLetterID'].value=document.getElementById("hdbdrLetterId").value;
//                window.opener.document.forms['form1']['hdBDRLetterID'].value=document.getElementById("hdbdrLetterId").value;
//                window.opener.document.forms['form1']['hdEnBDRLetterID'].value=document.getElementById("hdEnbdrLetterId").value;
//               return false;
//               
//                }    
//            }          
//        }
//        catch(err){}
//    }
//    
    </script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <!-- import the calendar script -->

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

</head>
<body style="font-size: 12pt; font-family: Times New Roman" >
    <form id="form1" runat="server" defaultfocus="txtAgencyName">
        <table width="840px" class="border_rightred" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ETrackers HelpDesk-&gt;</span><span class="sub_menu">BDR</span></td>
                            <td class="right">
                                <asp:LinkButton ID="lnkClose" CssClass="LinkButtons" runat="server" OnClientClick="return fnBDRID('1')">Close</asp:LinkButton>
                                &nbsp; &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" colspan="2">
                                Manage BDR</td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100%; height: 25px;" class="textbold" colspan="8" align="center"
                                                        valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%; height: 20px;" class="textbold">
                                                    </td>
                                                    <td style="width: 76%;" class="subheading" colspan="5">
                                                        Agency Details</td>
                                                    <td colspan="2" "width:22%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="8" align="center" style="height: 5px;" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">Agency</span></td>
                                                    <td colspan="4" style="width: 58%;" class="textbold">
                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" Width="484px"
                                                            MaxLength="30" TabIndex="1" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="20"
                                                            AccessKey="S" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">Address</span></td>
                                                    <td colspan="4" style="width: 58%;" class="textbold">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Width="485px"
                                                            MaxLength="30" TabIndex="2" Height="58px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;" valign="top">
                                                        <asp:Button ID="btnNewBdr" CssClass="button" runat="server" Text="New" TabIndex="21"
                                                            AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">City&nbsp;</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="3"
                                                            Width="162px" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 1%;" class="textbold">
                                                    </td>
                                                    <td style="width: 17%;">
                                                        <span class="textbold">Country</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="30"
                                                            TabIndex="4" Width="162px" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="21"
                                                            AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">Phone&nbsp;</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="5"
                                                            Width="162px" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 1%;" class="textbold">
                                                    </td>
                                                    <td style="width: 17%;">
                                                        <span class="textbold">Fax</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="6"
                                                            Width="162px" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                       <asp:Button ID="btnPrint" CssClass="button" runat="server" Text="Print" TabIndex="22"
                                                            AccessKey="P" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%; height: 22px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%; height: 22px;" class="textbold">
                                                        <span class="textbold">Office Id</span></td>
                                                    <td style="width: 20%; height: 22px;" class="textbold">
                                                        <asp:TextBox ID="txtOfficeId" TabIndex="7" runat="server" CssClass="textboxgrey"
                                                            ReadOnly="True" MaxLength="30" Width="162px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 1%; height: 22px;" class="textbold">
                                                    </td>
                                                    <td style="width: 17%; height: 22px;">
                                                        <span class="textbold">Online Status</span></td>
                                                    <td style="width: 20%; height: 22px;" class="textbold">
                                                        <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" MaxLength="30"
                                                            TabIndex="8" Width="162px" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 4%; height: 22px;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%; height: 22px;">
                                                        <input type="button" class="button" tabindex="23" value="History" style="width: 75px"
                                                            id="btnHistory" runat="server" accesskey="H" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 2%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 18%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 20%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 1%; height: 22px">
                                                    </td>
                                                    <td style="width: 17%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 20%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 4%; height: 22px">
                                                    </td>
                                                    <td style="width: 18%; height: 22px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 2%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 18%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 20%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 1%; height: 22px">
                                                    </td>
                                                    <td style="width: 17%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 20%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 4%; height: 22px">
                                                    </td>
                                                    <td style="width: 18%; height: 22px">
                                                        <asp:Button ID="btnLTR" runat="server" CssClass="button" TabIndex="4" Text="LTR"
                                                            Width="74px" AccessKey="L" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%; height: 20px;" class="textbold">
                                                    </td>
                                                    <td style="width: 76%;" class="subheading" colspan="5">
                                                        BDR Details</td>
                                                    <td colspan="2" style="width: 22%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">BDR Letter Id</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtBDrId" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="9"
                                                            Width="162px" ReadOnly="True"></asp:TextBox>&nbsp;</td>
                                                    <td style="width: 1%;" class="textbold">
                                                    </td>
                                                    <td style="width: 17%;">
                                                        <span class="textbold">LTR No</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtLtrNo" TabIndex="10" runat="server" CssClass="textboxgrey" MaxLength="30"
                                                            Width="162px" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 3px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">BDR Sent by </span>
                                                    </td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtBdrSentBy" runat="server" CssClass="textboxgrey" MaxLength="100"
                                                            ReadOnly="True" TabIndex="11" Width="162px"></asp:TextBox>&nbsp;<%--<img alt="" onclick="javascript:return EmployeePage();" src="../Images/lookup.gif" />--%></td>
                                                    <td style="width: 1%;" class="textbold">
                                                    </td>
                                                    <td style="width: 17%;">
                                                        <span class="textbold">BDR Ticket</span><span class="Mandatory">*</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtBDRTicket" runat="server" CssClass="textbox" MaxLength="100"
                                                            TabIndex="12" Width="158px"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">BDR Send Date</span><span class="Mandatory">*</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:TextBox ID="txtBDRBDRSendDate" runat="server" MaxLength="10" TabIndex="13" CssClass="textboxgrey"
                                                            Width="139px" ReadOnly="True"></asp:TextBox>&nbsp;<img id="ImgBDRSendDate" alt=""
                                                                src="../Images/calender.gif" tabindex="14" title="Date selector" style="cursor: pointer"
                                                                runat="server" />

                                                        <script type="text/javascript">
                                                                                        Calendar.setup({
                                                                                        inputField     :    '<%=txtBDRBDRSendDate.clientId%>',
                                                                                        ifFormat       :    "%d/%m/%Y",
                                                                                        button         :    "ImgBDRSendDate",
                                                                                        //align          :    "Tl",
                                                                                        singleClick    :    true
                                                                                        });
                                                        </script>

                                                    </td>
                                                    <td style="width: 1%;" class="textbold">
                                                    </td>
                                                    <td style="width: 17%;">
                                                        <span class="textbold"></span>
                                                    </td>
                                                    <td style="width: 20%;" class="textbold">
                                                    </td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 3px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">Airlines Office&nbsp;</span><span class="Mandatory">*</span></td>
                                                    <td colspan="4" style="width: 58%;" class="textbold">
                                                        <asp:TextBox ID="txtAirLineoffice" runat="server" CssClass="textboxgrey" MaxLength="30"
                                                            TabIndex="16" Width="485px" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                        <img tabindex="16" alt="" src="../Images/lookup.gif" onclick="javascript:return AirLineOfficePage();"
                                                            id="img1A" runat="server" style="cursor: pointer;" /></td>
                                                    <td style="width: 18%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 3px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">Airline Office Address&nbsp;</span></td>
                                                    <td colspan="4" style="width: 58%;" class="textbold">
                                                        <asp:TextBox ID="txtAirLineOfficeAdd" runat="server" CssClass="textboxgrey" MaxLength="30"
                                                            TabIndex="18" Width="485px" ReadOnly="True" Height="74px" TextMode="MultiLine"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">BDR Letter </span>
                                                    </td>
                                                    <td colspan="4" style="width: 58%;" class="textbold">
                                                        <asp:TextBox ID="txtBdrLetter" runat="server" CssClass="textboxgrey" MaxLength="30"
                                                            TabIndex="19" Width="485px" ReadOnly="True" Height="200px" TextMode="MultiLine"></asp:TextBox></td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="18"
                                                            Visible="False" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px;" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                        <span class="textbold">Authorized Signature</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:DropDownList ID="drpAuthSig" runat="server" CssClass="dropdownlist" Width="150px"
                                                            TabIndex="7">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 1%;" class="textbold">
                                                    </td>
                                                    <td style="width: 17%;">
                                                        <span class="textbold"></span>
                                                    </td>
                                                    <td style="width: 20%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px" class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 840px;" colspan ="8">
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
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 18%;" class="textbold">
                                                    </td>
                                                    <td style="width: 38%;" colspan="3" class="ErrorMsg">
                                                        Field Marked * are Mandatory</td>
                                                    <td style="width: 20%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 4%;" class="textbold">
                                                    </td>
                                                    <td style="width: 18%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="8" align="center" valign="TOP" style="height: 10px;">
                                                        <input type="hidden" id="hdAirLineCode" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="HdAROFID" runat="server" value="" style="width: 5px" />
                                                        <input type="Hidden" id="hdAilLineName" runat="server" value="" style="width: 5px" />
                                                        <input type="Hidden" id="hdEmployeeId" runat="server" value="" style="width: 5px" />
                                                        <input type="Hidden" id="hdCallCategoryName" runat="server" value="" style="width: 5px" />
                                                        <input type="Hidden" id="hdAoffice" runat="server" value="" style="width: 5px" />
                                                        <input type="Hidden" id="hdTemplateVersion" runat="server" value="" style="width: 5px" />
                                                        <input type="Hidden" id="hdReqId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdbdrLetterId" runat="server" style="width: 1px" />
                                                        <input type="hidden" id="hdLcode" runat="server" style="width: 1px" />
                                                        <input type="hidden" id="hdRequestID" runat="server" style="width: 1px" />
                                                        <input type="Hidden" id="hdEnReqId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdEnbdrLetterId" runat="server" style="width: 1px" />
                                                        <input type="hidden" id="hdEnLcode" runat="server" style="width: 1px" />
                                                        <input type="hidden" id="hdEnRequestID" runat="server" style="width: 1px" />
                                                        <input style="WIDTH: 5px" id="HdBdrIdXml" type="hidden" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="8" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="8" align="center" valign="TOP" style="height: 10px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textbold" align="center" valign="TOP" style="height: 14px;">
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
