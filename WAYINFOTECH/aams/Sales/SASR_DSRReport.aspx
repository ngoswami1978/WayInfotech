<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_DSRReport.aspx.vb"
    Inherits="Sales_SASR_DSRReport" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Account Management</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

</style>

    <script type="text/javascript" src="../JavaScript/AAMS.js">
    </script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript">   
    
     function EnableDisableGroupProductivity()
    {
    
            if  (document.getElementById("TxtLCode").value=='' )
                   {
                        document.getElementById("hdLcode").value="";
                        document.getElementById("chkGroupProductivity").disabled=true;
                        document.getElementById("chkGroupProductivity").checked=false;
                   } 
                   else
                   {
                      document.getElementById("hdLcode").value=document.getElementById("TxtLCode").value;
                      document.getElementById("chkGroupProductivity").disabled=false;                      
                   }
        
    }       
      function ActDeAct()
     {       
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9 &&  whichASC !=13 && whichASC!=18)
        {
                    if  (document.getElementById("TxtLCode").value=='' )
                   {
                        document.getElementById("hdLcode").value="";
                        document.getElementById("chkGroupProductivity").disabled=true;
                        document.getElementById("chkGroupProductivity").checked=false;
                   } 
                   else
                   {
                      document.getElementById("hdLcode").value=document.getElementById("TxtLCode").value;
                      document.getElementById("chkGroupProductivity").disabled=false;                      
                   }
    	}
     } 
function gotops(id)
{
   try
   {
         if (event.keyCode==46 )
         {
            document.getElementById(id).selectedIndex=0;
            OpenStrategicCallType();
            setTimeout('__doPostBack(\'drpStrategicCallType\',\'\')', 0);           
         }
   }
     catch(err){}   
}
function gotops2(id)
{
   try
   {
         if (event.keyCode==46 )
         {
            document.getElementById(id).selectedIndex=0;
            OpenStrategicCallType();
            setTimeout('__doPostBack(\'drpStrategicCallType\',\'\')', 0);           
         }
   }
     catch(err){}   
}
  
    
function OpenStrategicCallType()
{
  
   if ( document.getElementById ('DlstVisitSubType').value=="2" )
   {
      document.getElementById ('drpStrategicCallType').disabled=false;
      document.getElementById ('DlstRetReason').disabled=false;
   }
   else if ( document.getElementById ('DlstVisitSubType').value=="" )
   {
   document.getElementById ('drpStrategicCallType').disabled=true;
   document.getElementById ('drpStrategicCallType').value="";
   document.getElementById ('DlstRetReason').disabled=false; 
   }
   else
   {
    document.getElementById ('drpStrategicCallType').disabled=true;
    document.getElementById ('drpStrategicCallType').value="";
    document.getElementById ('DlstRetReason').disabled=true;
    document.getElementById ('DlstRetReason').value="";
   }

}
    
     function PopupAgencyPage()
        {
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"Agency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
        
    function CallValidation()
     {
          document.getElementById("lblError").innerHTML="";
       if  ( window.document.getElementById ('txtDateOfDSRF').value=='')
       {
           document.getElementById("lblError").innerHTML ="From date is mandatory.";
           document.getElementById('txtDateOfDSRF').focus();
           return false;
       }
       
          if(document.getElementById('txtDateOfDSRF').value != '')
        {
        if (isDate(document.getElementById('txtDateOfDSRF').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "From date is not valid.";			
	       document.getElementById('txtDateOfDSRF').focus();
	       return(false);  
        }
         }  
         
         if  ( window.document.getElementById ('txtDateOfDSRT').value=='')
       {
           document.getElementById("lblError").innerHTML ="To date is mandatory.";
            document.getElementById('txtDateOfDSRT').focus();
           return false;
       }
       
          if(document.getElementById('txtDateOfDSRT').value != '')
        {
        if (isDate(document.getElementById('txtDateOfDSRT').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "To date is not valid.";			
	       document.getElementById('txtDateOfDSRT').focus();
	       return(false);  
        }
         } 
         
         if (compareDates(document.getElementById('txtDateOfDSRF').value,"dd/MM/yyyy",document.getElementById('txtDateOfDSRT').value,"dd/MM/yyyy")==1)
       {
            document.getElementById('lblError').innerText ='To date should be greater than or equal to from date.';
            document.getElementById('txtDateOfDSRT').focus();
            return false;
       }
          
         
       ShowPopupTabChange();
       
       
     }
    

      function OpenVistDetails(RowId,DSRVistedId,UnplannedVisit,ResId,Lcode,ChainCode,VisitDate,strIsManager)
    {
    
       var parameter="RowId=" + RowId  + "&DSRVistedId=" + DSRVistedId  +  "&UnplannedVisit=" + UnplannedVisit    + "&ResId=" + ResId + "&Lcode=" + Lcode + "&ChainCode=" + ChainCode  + "&VisitDate=" + VisitDate  + "&IsManager=" + strIsManager;
       type = "SASR_VisitDetails.aspx?" + parameter;
       window.open(type,"Sa","height=600;width=1080,top=30,left=20,scrollbars=1,status=1,resizable=1");            
       return false;    
    
    
    }
    
    
    function OpenUnplannedVisit()
    {
       document.getElementById("lblError").innerHTML="";
       if  ( window.document.getElementById ('txtDateOfDSR').value=='')
       {
           document.getElementById("lblError").innerHTML ="Previous date is mandatory.";
           return false;
       }
       var PREDATE=window.document.getElementById ('txtDateOfDSR').value;
       var parameter="&PREDATE=" + PREDATE  ;
       type = "SASR_Agency.aspx?" + parameter;
       window.open(type,"Sa","height=600;width=1080,top=30,left=20,scrollbars=1,status=1,resizable=1");          
       return false; 
    }
    
           function SelectDate(textBoxid,imgId)
    {   
                 Calendar.setup
                 (
                     {
                           inputField : textBoxid,
                           ifFormat :"%d/%m/%Y",
                           button :imgId,
                           onmousedown :true
                     }
                 )                                      
    }
      
      
   function Defaultfunction()
  {
            if (document.getElementById("hdLcode").value=="")
              {
                  document.getElementById("chkGroupProductivity").disabled=true;
                  document.getElementById("chkGroupProductivity").checked==false;        
              }
      if (document.getElementById ('img1')!=null)
         {
            document.getElementById('img1').style.display ="none";
         }
     Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequest) 
     Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequests);  
  }
  
  function BeginRequest(sender,args)
   {
      var elem = args.get_postBackElement();
      if (elem.id!="btnNew"   &&  elem.id!="btnSearch" && elem.id!="BtnSave" && elem.id!="BtnExport" &&  elem.id!="BtnReset"  &&   elem.id!="LnkCalender" &&   elem.id!="BtnCalender"   && elem.id!="btnUp"  && elem.id!="lnkAdvance"  && elem.id!="DlstVisitSubType"   && elem.id!="drpStrategicCallType"   )
       {
       ShowPopupTabChange();
      }
   }
  
  function EndRequests(sender,args)
   { 
  // EndRequest();
              if (document.getElementById("hdLcode").value=="")
              {
                  document.getElementById("chkGroupProductivity").disabled=true;
                  document.getElementById("chkGroupProductivity").checked==false;        
              }
   }        
       
         function PostData()
    {
        if (document.getElementById('grdDSRReport') !=null)
        {
            // return true;
            
              $get("<%=BtnAuoPostback.ClientID %>").click(); 
        }
        else
        {
          return true;
        }
    }   
    function ShowPopupTabChange()
        {
        try
        {
        var modal = $find('ModalLoading'); 
        document.getElementById('PnlPrrogress').style.height='150px';
        if (document.getElementById ('img1')!=null)
         {
            document.getElementById('img1').style.display ="block";
         }
        modal.show(); 
        }
         catch(err){}
         
        }     
         
         
         function ExportData()
 {
 
 
     document.getElementById("lblError").innerHTML="";
       if  ( window.document.getElementById ('txtDateOfDSRF').value=='')
       {
           document.getElementById("lblError").innerHTML ="From date is mandatory.";
           document.getElementById('txtDateOfDSRF').focus();
           return false;
       }
       
          if(document.getElementById('txtDateOfDSRF').value != '')
        {
        if (isDate(document.getElementById('txtDateOfDSRF').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "From date is not valid.";			
	       document.getElementById('txtDateOfDSRF').focus();
	       return(false);  
        }
         }  
         
         if  ( window.document.getElementById ('txtDateOfDSRT').value=='')
       {
           document.getElementById("lblError").innerHTML ="To date is mandatory.";
            document.getElementById('txtDateOfDSRT').focus();
           return false;
       }
       
          if(document.getElementById('txtDateOfDSRT').value != '')
        {
        if (isDate(document.getElementById('txtDateOfDSRT').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "To date is not valid.";			
	       document.getElementById('txtDateOfDSRT').focus();
	       return(false);  
        }
         }
         
              if (compareDates(document.getElementById('txtDateOfDSRF').value,"dd/MM/yyyy",document.getElementById('txtDateOfDSRT').value,"dd/MM/yyyy")==1)
       {
            document.getElementById('lblError').innerText ='To date should be greater than or equal to from date.';
            document.getElementById('txtDateOfDSRT').focus();
            return false;
       }
       
        $get("<%=Btnexp.ClientID %>").click(); 
 
     
 }
                                                         
    </script>

</head>
<body onload="Defaultfunction();">
    <form id="form1" runat="server" defaultbutton="btnSearch">
        <asp:ScriptManager ID="Sc1" runat="server" AsyncPostBackTimeout="800">
        </asp:ScriptManager>
        <table>
            <tr>
                <td>
                    <table width="860px" class="border_rightred left">
                        <tr>
                            <td class="top">
                                <table width="100%" class="left">
                                    <tr>
                                        <td>
                                            <span class="menu">Sales -&gt;</span><span class="sub_menu">Account Management</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="heading center" style="width: 860px;">
                                                        Search Account Management</td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdPnl" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td class="redborder center">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 870px" valign="top">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 870px" class="left">
                                                                                <tr>
                                                                                    <td class="textbold" colspan="9" align="center" style="height: 15px">
                                                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 140px">
                                                                                        Date from <span class="Mandatory">*</span></td>
                                                                                    <td class="textbold" style="width: 180px">
                                                                                        <asp:TextBox ID="txtDateOfDSRF" runat="server" CssClass="textbox" MaxLength="40"
                                                                                            TabIndex="1" Width="172px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 30px">
                                                                                        <img id="Img_DSRDateF" alt="" runat="server" src="../Images/calender.gif" tabindex="1"
                                                                                            title="Date selector" style="cursor: pointer" /></td>
                                                                                    <td style="width: 10px;">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 160px">
                                                                                        Date To <span class="Mandatory">*</span></td>
                                                                                    <td style="width: 180px">
                                                                                        <asp:TextBox ID="txtDateOfDSRT" runat="server" CssClass="textbox" MaxLength="40"
                                                                                            TabIndex="1" Width="172px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 30px;">
                                                                                        <img id="Img_DSRDateT" alt="" runat="server" src="../Images/calender.gif" tabindex="1"
                                                                                            title="Date selector" style="cursor: pointer" /></td>
                                                                                    <td class="left" style="width: 125px">
                                                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"
                                                                                            Width="120px" AccessKey="A" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px;">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 140px">
                                                                                        Agency
                                                                                    </td>
                                                                                    <td class="textbold" colspan="4" style="height: 22px">
                                                                                        <asp:HiddenField ID="hdLcode" runat="server" Value="" />
                                                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox TextTitleCase" MaxLength="50"
                                                                                            TabIndex="1" Width="90%"></asp:TextBox>&nbsp;<img id="Img2" runat="server" alt="Select & Add Agency Name"
                                                                                                onclick="PopupAgencyPage()" tabindex="1" src="../Images/lookup.gif" style="cursor: pointer;" />
                                                                                    </td>
                                                                                    <td colspan="2" style="height: 22px">
                                                                                        <asp:CheckBox ID="chkGroupProductivity" runat="server" CssClass="textbold" TabIndex="1"
                                                                                            Text="Group Productivity" TextAlign="right" Width="144px" /></td>
                                                                                    <td class="left" style="width: 125px; height: 22px;">
                                                                                        <asp:Button ID="BtnExport" Width="120px" CssClass="button" runat="server" Text="Export"
                                                                                            TabIndex="2" AccessKey="E" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 140px" valign="top">
                                                                                        Lcode</td>
                                                                                    <td class="textbold" style="width: 180px" valign="top">
                                                                                        <asp:TextBox ID="TxtLCode" onkeyup="checknumeric(this.id);ActDeAct();" runat="server"
                                                                                            CssClass="textbox" MaxLength="40" TabIndex="1" Width="172px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 10px;">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 160px" valign="top">
                                                                                        Chain Code
                                                                                    </td>
                                                                                    <td style="width: 180px" valign="top">
                                                                                        <asp:TextBox ID="TxtChainCode" runat="server" CssClass="textbox" MaxLength="10" TabIndex="1"
                                                                                            onkeyup="checknumeric(this.id)" Width="172px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 30px;">
                                                                                    </td>
                                                                                    <td class="left" style="width: 125px" valign="top">
                                                                                        <asp:Button ID="BtnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                                            AccessKey="R" Width="120px" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 140px" valign="top">
                                                                                        City</td>
                                                                                    <td class="textbold" style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="DlstCity" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="178px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 10px;">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 160px" valign="top">
                                                                                        Country
                                                                                    </td>
                                                                                    <td style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="DlstCountry" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="178px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 30px;">
                                                                                    </td>
                                                                                    <td class="left" style="width: 125px" valign="top">
                                                                                        <asp:Button ID="btnManagerVisitLog" CssClass="button" runat="server" Text="Manager Visit Log"
                                                                                            TabIndex="2" AccessKey="R" Width="120px" OnClick="btnManagerVisitLog_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 140px" valign="top">
                                                                                        Region
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="178px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 10px;">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 160px" valign="top">
                                                                                        OfficeID
                                                                                    </td>
                                                                                    <td style="width: 180px" valign="top">
                                                                                        <asp:TextBox ID="TxtOfficeID" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1"
                                                                                            Width="172px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 30px;">
                                                                                    </td>
                                                                                    <td class="left" style="width: 125px" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 140px" valign="top">
                                                                                        Aoffice</td>
                                                                                    <td class="textbold" style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="DlstAoffice" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="178px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 10px;">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 160px" valign="top">
                                                                                        1A Responsible
                                                                                    </td>
                                                                                    <td style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="Drp1ARes" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="178px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 30px;">
                                                                                    </td>
                                                                                    <td class="left" style="width: 125px" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 140px" valign="top">
                                                                                        Visit Sub Type
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="DlstVisitSubType" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                                            Width="178px" AutoPostBack="True">
                                                                                            <asp:ListItem Text="--All--" Value="" Selected="True"></asp:ListItem>
                                                                                            <asp:ListItem Text="Service Call" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Strategic Call" Value="2"></asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 10px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 160px" valign="top">
                                                                                        Strategic Call Type
                                                                                    </td>
                                                                                    <td style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="drpStrategicCallType" runat="server" CssClass="dropdownlist"
                                                                                            TabIndex="2" onkeyup="gotops2(this.id)" Width="178px" AutoPostBack="True">
                                                                                            <asp:ListItem Text="--All--" Selected="True" Value=""></asp:ListItem>
                                                                                            <asp:ListItem Text="Target Call" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Retention Call" Value="2"></asp:ListItem>
                                                                                            <asp:ListItem Text="Air Non Air Call" Value="3"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 30px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="center" style="width: 125px" valign="top">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px;">
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td class="textbold">
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 30px;">
                                                                                    </td>
                                                                                    <td class="center" style="width: 30px;" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="padding-top: 5px;">
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 140px" valign="top">
                                                                                        Status
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                            onkeyup="gotop(this.id)" Width="178px">
                                                                                        </asp:DropDownList></td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 10px">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 160px" valign="top">
                                                                                        Retention Reason
                                                                                    </td>
                                                                                    <td style="width: 180px" valign="top">
                                                                                        <asp:DropDownList ID="DlstRetReason" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                            onkeyup="gotop(this.id)" Width="178px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 30px">
                                                                                    </td>
                                                                                    <td class="center" style="width: 125px" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="padding-top: 5px;">
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td valign="top" class="textbold" style="width: 140px">
                                                                                        Agency Category
                                                                                    </td>
                                                                                    <td class="textbold" valign="top" colspan="3">
                                                                                        <asp:CheckBoxList ID="ChkLstAgencyCategory" TabIndex="1" runat="server" TextAlign="Right"
                                                                                            RepeatDirection="horizontal" CssClass="textbox">
                                                                                        </asp:CheckBoxList>
                                                                                    </td>
                                                                                    <td class="textbold" valign="top">
                                                                                    </td>
                                                                                    <td class="textbold" rowspan="3" valign="top">
                                                                                    </td>
                                                                                    <td style="width: 20px">
                                                                                    </td>
                                                                                    <td class="center" style="width: 125px" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="padding-top: 5px;">
                                                                                    <td style="width: 20px">
                                                                                    </td>
                                                                                    <td class="textbold" valign="top" style="width: 140px">
                                                                                     Visited
                                                                                        <asp:CheckBox ID="ChkY" TabIndex="1" Text="Y" Checked="false" CssClass="textbold"
                                                                                            TextAlign="Right" AutoPostBack="false" runat="server" Font-Underline="True" OnCheckedChanged="ChkY_CheckedChanged"
                                                                                            Width="32px" /><asp:CheckBox ID="ChkN" TabIndex="1" Text="N" Checked="false" CssClass="textbold"
                                                                                                TextAlign="Right" AutoPostBack="false" Font-Underline="True" runat="server" Width="32px" />
                                                                                        
                                                                                    </td>
                                                                                    <td class="textbold" valign="top">
                                                                                       <asp:CheckBox ID="ChkPenIssue" Text="Pending Issue" TextAlign="left" runat="server"
                                                                                            Width="100px" TabIndex="1" />
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td class="textbold">
                                                                                        <asp:CheckBox ID="ChkManagerCallLog" Text="Manager Calls" TextAlign="left" runat="server"
                                                                                            Width="100px" TabIndex="1" />
                                                                                    </td>
                                                                                    <td style="width: 30px">
                                                                                    </td>
                                                                                    <td class="center" style="width: 125px" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px;">
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td class="textbold">
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 30px;">
                                                                                    </td>
                                                                                    <td class="center" style="width: 30px;" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 130px" valign="top">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 180px" valign="top">
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 10px">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 160px" valign="top">
                                                                                    </td>
                                                                                    <td style="width: 180px" valign="top">
                                                                                    </td>
                                                                                    <td style="width: 30px">
                                                                                    </td>
                                                                                    <td class="center" style="width: 125px" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="subheading" colspan="7">
                                                                                        <asp:ImageButton ID="btnUp" runat="server" ImageUrl="../Images/down.jpg" OnClick="btnUp_Click" />
                                                                                        &nbsp;&nbsp;
                                                                                        <asp:LinkButton ID="lnkAdvance" CssClass="menu" Text=" Show/hide Columns" runat="server"
                                                                                            TabIndex="1" OnClick="lnkAdvance_Click"></asp:LinkButton>
                                                                                    </td>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px;">
                                                                                    </td>
                                                                                    <td colspan="9" style="width: 850px;" valign="top">
                                                                                        <asp:Panel ID="PnlShowUnhideColumns" Visible="true" runat="server" Style="width: 100%">
                                                                                           <table width="100%" cellpadding="2" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td class="textbold" style="width: 125px;" align ="center" >
                                                                                                        Agency Details</td>
                                                                                                    <td class="textbold" style="width: 145px; padding-left: 5px;" align ="center" >
                                                                                                        Agency Segs Details</td>
                                                                                                    <td class="textbold" style="padding-left: 5px;">
                                                                                                        Agency Visit Details</td>
                                                                                                </tr>
                                                                                                <tr >
                                                                                                    <td valign="top" style="width: 125px;">
                                                                                                        <asp:Panel ID="PnlADetails" runat="server" ScrollBars="Vertical" Height="100px" BorderWidth="1px" width="100%">
                                                                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                                                                <tr >
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkLcode" TabIndex="1" Text="LCode" runat="server" CssClass="textbold"
                                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkChaincode" TabIndex="1" Text="Chain Code" runat="server" CssClass="textbold"
                                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkCity" TabIndex="1" Text="City" runat="server" CssClass="textbold"
                                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkOfficeID" TabIndex="1" Text="OfficeID" runat="server" CssClass="textbold"
                                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkSalesExec" TabIndex="1" Text="Sales Executive" runat="server"
                                                                                                                            CssClass="textbold" Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkAddress" TabIndex="1" Text="Address" runat="server" CssClass="textbold"
                                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkCateg" TabIndex="1" Text="Category" runat="server" CssClass="textbold"
                                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                    <td valign="top" style="width: 145px; padding-left: 5px;">
                                                                                                        <asp:Panel ID="PnlASegDetails" runat="server" ScrollBars="Vertical" Height="100px"
                                                                                                            BorderWidth="1px">
                                                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkPastMotive" TabIndex="1" Text="1A Daily Motives" runat="server"
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkBIDTLatest" TabIndex="1" Text="BIDT (Latest)" runat="server"  Visible="true" 
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkMIDTLatest" TabIndex="1" Text="Potential (Latest)" runat="server"  Visible="true"  
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkMIDT" TabIndex="1" Text="Potential" runat="server" CssClass="textbold"
                                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkBIDT" TabIndex="1" Text="BIDT" runat="server" CssClass="textbold"
                                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkBCommit" TabIndex="1" Text="Business commit" runat="server"
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkMinSeg" TabIndex="1" Text="Min Seg" runat="server" CssClass="textbold"
                                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="ChkSegstarget" TabIndex="1" Text="Segs target" runat="server" CssClass="textbold"
                                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                    <td valign="top" style="padding-left: 5px;">
                                                                                                        <asp:Panel ID="PnlAVDetails" runat="server" ScrollBars="both" Height="100px" Width="550px"
                                                                                                            BorderWidth="1px">
                                                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkSalesObjVisit" TabIndex="1" Text="Sales Obj. Visit" runat="server"
                                                                                                                            CssClass="textbold" Width="115px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkAcBYManger" TabIndex="1" Text="Acco By Manager" runat="server"
                                                                                                                            Width="125px" CssClass="textbold" TextAlign="Right" AutoPostBack="False" Visible="true">
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkMarketInfo" TabIndex="1" Text="Competition/Mkt Info Remarks"
                                                                                                                            runat="server" CssClass="textbold" Width="130px" TextAlign="Right" AutoPostBack="False">
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkTarCloserDate" TabIndex="1" Text=" Target Closer Date" runat="server" Visible="true"  
                                                                                                                            CssClass="textbold" Width="130px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkTargerSegPer" TabIndex="1" Text="Target Segs % of Business" Visible="true"  
                                                                                                                            runat="server" CssClass="textbold" Width="130px" TextAlign="Right" AutoPostBack="False">
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkVisittargets" TabIndex="1" Text="Visit targets" runat="server"
                                                                                                                            CssClass="textbold" Width="115px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkAcBYRepManger" TabIndex="1" Text="Acco By Reporting Manager"
                                                                                                                            runat="server" CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"
                                                                                                                            Visible="true"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkFollowupRem" TabIndex="1" Text="Followup Remarks" runat="server"
                                                                                                                            CssClass="textbold" Width="130px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top">
                                                                                                                        <asp:CheckBox ID="ChkCloserDate" TabIndex="1" Text="Closer Date" runat="server" CssClass="textbold" Visible="true"  
                                                                                                                            Width="130px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkProdName" TabIndex="1" Text="Product Name" runat="server" CssClass="textbold" Visible="true"  
                                                                                                                            Width="130px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkVisitSubType" TabIndex="1" Text="Visit SubType" runat="server"
                                                                                                                            Visible="true" CssClass="textbold" Width="115px" TextAlign="Right" AutoPostBack="False">
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkAcBycolleague" TabIndex="1" Text="Acco. By colleague " runat="server"
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkPrevRem1" TabIndex="1" Text="Prev Remark I" runat="server" CssClass="textbold"
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkRetReason" TabIndex="1" Text="Retention Reason" runat="server" Visible="true"  
                                                                                                                            CssClass="textbold" Width="120px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top">
                                                                                                                        <asp:CheckBox ID="ChkRevenue" TabIndex="1" Text="Revenue" runat="server" CssClass="textbold" Visible="true"  
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkPlanVisitDone" TabIndex="1" Text="Plan Visit Done" runat="server"
                                                                                                                            CssClass="textbold" Width="115px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkJointCallremarks" TabIndex="1" Text="Joint Call remarks " runat="server"
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkPrevRem2" TabIndex="1" Text="Prev Remark II" runat="server"
                                                                                                                            CssClass="textbold" Width="105px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkNewCPS" TabIndex="1" Text="New CPS" runat="server" CssClass="textbold" Visible="true"  
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkInsDate" TabIndex="1" Text="Installation Date" runat="server" Visible="true"  
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkUnPlanVisitDone" TabIndex="1" Text="UnPlan Visit Done" runat="server"
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkLoggedByManager" TabIndex="1" Text="Logged by Manager" runat="server"
                                                                                                                            Checked="false" CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"
                                                                                                                            Visible="true"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkDepartment" TabIndex="1" Text="Department" runat="server" CssClass="textbold" Visible ="true" 
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="Chk1aAppNewDeal" TabIndex="1" Text="1A Approval New Deal" runat="server" Visible="true"  
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkDSRCode" TabIndex="1" Text="DSR Code" runat="server" CssClass="textbold" 
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkVisitYesNo" TabIndex="1" Text="Visited(Yes/No)" runat="server"
                                                                                                                            Width="115px" CssClass="textbold" TextAlign="Right" AutoPostBack="False" Visible="true">
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkPersonMet" TabIndex="1" Text="Person Met" runat="server" CssClass="textbold"
                                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False" Visible="true"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkAssignedTo" TabIndex="1" Text="Assigned To" runat="server" Checked="false"
                                                                                                                            CssClass="textbold" Width="105px" TextAlign="Right" AutoPostBack="false" Visible="true">
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkCPS" TabIndex="1" Text="CPS" runat="server" CssClass="textbold" Visible="true"  
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkInOutTime" TabIndex="1" Text="In/out Time" runat="server" CssClass="textbold"
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkReasonRemNotVisited" TabIndex="1" Text="Reason for no visit as per Planned Visit"
                                                                                                                            runat="server" CssClass="textbold" Width="115px" TextAlign="Right" AutoPostBack="False">
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkDesig" TabIndex="1" Text="Designation" runat="server" CssClass="textbold"
                                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False" Visible="true"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkDeptSpecific" TabIndex="1" Text="Department Specific" runat="server"  Visible ="true" 
                                                                                                                            CssClass="textbold" Width="130px" TextAlign="Right" AutoPostBack="False" >
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkSignOnConvDate" TabIndex="1" Text="SignOn/Conv. Date" runat="server" Visible="true"  
                                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False" >
                                                                                                                        </asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top" style ="empty-cells :hide;">
                                                                                                                        <asp:CheckBox ID="ChkVisitDate" TabIndex="1" Text="VisitDate" runat="server" CssClass="textbold"
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False" Visible="true"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style ="empty-cells :hide;">
                                                                                                                    <td valign="top">
                                                                                                                        &nbsp;
                                                                                                                    </td>
                                                                                                                    <td valign="top">
                                                                                                                        &nbsp;
                                                                                                                    </td>
                                                                                                                    <td valign="top">
                                                                                                                        &nbsp;
                                                                                                                    </td>
                                                                                                                    <td valign="top">
                                                                                                                        <asp:CheckBox ID="ChkPendingIssue" TabIndex="1" Text="Pending Issue" runat="server"
                                                                                                            Checked="true" CssClass="textbold" Width="110px" TextAlign="Right" AutoPostBack="False"
                                                                                                            Visible="false"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                    <td valign="top">
                                                                                                                        <asp:CheckBox ID="ChkLogDate" TabIndex="1" Text="Logged Date" runat="server" CssClass="textbold"
                                                                                                                            Width="105px" TextAlign="Right" AutoPostBack="False" Visible="true"></asp:CheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                        <asp:HiddenField ID="hdAdvanceSearch" EnableViewState="true" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100%;padding-top:10px;" >
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:GridView TabIndex="1" ID="grdDSRReport" runat="server" AutoGenerateColumns="False"
                                                                                            HorizontalAlign="Left" ShowFooter="true" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                                            AlternatingRowStyle-CssClass="lightblue" RowStyle-VerticalAlign="top" HeaderStyle-ForeColor="white"
                                                                                            AllowPaging="false" AllowSorting="true">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Action">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="LnkVisitDetail" runat="server" CommandName="VisitDetailsx" Text="Visit Details"
                                                                                                            CssClass="LinkButtons" Width="70px"></asp:LinkButton>
                                                                                                        <asp:HiddenField ID="hdDSRVistedId" runat="server" Value='<% #Container.DataItem("DSR_VISIT_ID") %>' />
                                                                                                        <asp:HiddenField ID="hdRowId" runat="server" Value='<% #Container.DataItem("ROWID") %>' />
                                                                                                        <asp:HiddenField ID="HdResID" runat="server" Value='<% #Container.DataItem("RESP_1A") %>' />
                                                                                                        <asp:HiddenField ID="HdUnpllanedVisit" runat="server" Value='<% #Container.DataItem("UNPLANVISIT") %>' />
                                                                                                        <asp:HiddenField ID="HdLcode" runat="server" Value='<% #Container.DataItem("LCODE") %>' />
                                                                                                        <asp:HiddenField ID="HdChainCode" runat="server" Value='<% #Container.DataItem("CHAIN_CODE") %>' />
                                                                                                        <asp:HiddenField ID="HdPreDate" runat="server" Value='<% #Container.DataItem("DATE") %>' />
                                                                                                        <asp:HiddenField ID="HdPlannedVisitDone" runat="server" Value='<% #Container.DataItem("PLAN_VISIT_DONE") %>' />
                                                                                                        <asp:HiddenField ID="HdUnPlannedVisitDone" runat="server" Value='<% #Container.DataItem("UNPLAN_VISIT_DONE") %>' />
                                                                                                        <asp:HiddenField ID="HdVisitYesNo" runat="server" Value='<% #Container.DataItem("VISITED") %>' />
                                                                                                        <asp:HiddenField ID="HdIsManager" runat="server" Value='<% #Container.DataItem("MANAGER") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="left" Width="75px" />
                                                                                                    <HeaderStyle HorizontalAlign="left" Width="75px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="  " HeaderImageUrl="~/Images/empty-flg.gif" SortExpression="COLORCODE">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Image ImageUrl="" runat="server" ID="ImgColorCode" />
                                                                                                        <asp:HiddenField ID="hdColorCode" runat="server" Value='<%#Eval("COLORCODE")%>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle Width="30px" />
                                                                                                    <ItemStyle Width="30px" Wrap="false" HorizontalAlign="center" />
                                                                                                </asp:TemplateField>
                                                                                                 <asp:BoundField DataField="DSR_VISIT_ID" HeaderText="DSR CODE" SortExpression="DSR_VISIT_ID">
                                                                                                    <ItemStyle Wrap="False" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="LCODE" HeaderText="LCode" SortExpression="LCODE">
                                                                                                    <ItemStyle Wrap="False" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code" SortExpression="CHAIN_CODE">
                                                                                                    <ItemStyle Wrap="False" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="AGENCY_NAME" HeaderText="Agency Name" SortExpression="AGENCY_NAME">
                                                                                                    <ItemStyle Wrap="True" Width="135px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="135px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="ADDRESS" HeaderText="Address" SortExpression="ADDRESS">
                                                                                                    <ItemStyle Wrap="True" Width="140px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="140px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="CITY_NAME" HeaderText="City" SortExpression="CITY_NAME">
                                                                                                    <ItemStyle Wrap="True" HorizontalAlign="left" Width="100px" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="GROUP_CATG_NAME" HeaderText="Category" SortExpression="GROUP_CATG_NAME">
                                                                                                    <ItemStyle Wrap="true" HorizontalAlign="left" Width="50px" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" Width="50px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="OFFICEID" HeaderText="OfficeID" SortExpression="OFFICEID">
                                                                                                    <ItemStyle Wrap="false" HorizontalAlign="left" Width="70px" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" Width="70px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="RESP_NAME" HeaderText="Sales Executive" Visible="true"
                                                                                                    SortExpression="RESP_NAME">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Visited (Y/N)  " ItemStyle-HorizontalAlign="Left"
                                                                                                    SortExpression="VISITED">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblVisitYesNo" runat="server" Text='<% #Container.DataItem("VISITED") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" Width="70px" />
                                                                                                    <ItemStyle HorizontalAlign="center" VerticalAlign="top" Width="70px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="REASON_REMARKS" HeaderText="Reason for no visit as per Planned Visit"
                                                                                                    SortExpression="REASON_REMARKS">
                                                                                                    <ItemStyle Wrap="True" Width="200px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" Width="200px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="SEGSTARGET" HeaderText="Segs Target" SortExpression="SEGSTARGET">
                                                                                                    <ItemStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" />
                                                                                                    <FooterStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="OBJ_VISITCOUNT" HeaderText="Sales Objective visit" SortExpression="OBJ_VISITCOUNT">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" Width="100px" />
                                                                                                    <FooterStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="VISITTARGET" HeaderText="Visit Target" SortExpression="VISITTARGET">
                                                                                                    <ItemStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" />
                                                                                                    <FooterStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PLAN_VISIT_DONE" HeaderText="Planned Visit Done" SortExpression="PLAN_VISIT_DONE">
                                                                                                    <ItemStyle Wrap="True" Width="80px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" Width="80px" />
                                                                                                    <FooterStyle Wrap="false" Width="80px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="UNPLAN_VISIT_DONE" HeaderText="UnPlanned Visit Done" SortExpression="UNPLAN_VISIT_DONE">
                                                                                                    <ItemStyle Wrap="True" Width="80px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" Width="80px" />
                                                                                                    <FooterStyle Wrap="false" Width="80px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="MIDT" HeaderText="Potential" SortExpression="MIDT">
                                                                                                    <ItemStyle Wrap="False" Width="50px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="50px" HorizontalAlign="left" />
                                                                                                    <FooterStyle Wrap="false" Width="50px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="MIDT_LATEST" HeaderText="Potential Latest" SortExpression="MIDT_LATEST">
                                                                                                    <ItemStyle Wrap="False" Width="50px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="50px" HorizontalAlign="left" />
                                                                                                    <FooterStyle Wrap="false" Width="50px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="BIDT" HeaderText="BIDT" SortExpression="BIDT">
                                                                                                    <ItemStyle Wrap="false" Width="50px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="50px" HorizontalAlign="left" />
                                                                                                    <FooterStyle Wrap="false" Width="50px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="BIDT_LATEST" HeaderText="BIDT LATEST" SortExpression="BIDT_LATEST">
                                                                                                    <ItemStyle Wrap="false" Width="50px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="50px" HorizontalAlign="left" />
                                                                                                    <FooterStyle Wrap="false" Width="50px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="A1DAILYMOTIVES" HeaderText="1A Daily Motive" SortExpression="A1DAILYMOTIVES">
                                                                                                    <ItemStyle Wrap="false" Width="70px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="70px" HorizontalAlign="left" />
                                                                                                    <FooterStyle Wrap="false" Width="70px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="BCMINSEGMENT" HeaderText="Min Segment" SortExpression="BCMINSEGMENT">
                                                                                                    <ItemStyle Wrap="True" Width="60px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" Width="60px" />
                                                                                                    <FooterStyle Wrap="false" Width="60px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="BCCOMMIT" HeaderText="Commitment" SortExpression="BCCOMMIT">
                                                                                                    <ItemStyle Wrap="True" Width="60px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="VISIT_TYPE_NAME" HeaderText="Visit Sub Type" SortExpression="VISIT_TYPE_NAME"
                                                                                                    Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="DISCUSSION_ISSUE_REMARKS" HeaderText="Detailed Discussion/ Issue Reported"
                                                                                                    SortExpression="DISCUSSION_ISSUE_REMARKS" Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="200px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="200px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="COMPETITION_MKT_INFO_REMARKS" HeaderText="Competition  Info/Mkt info Remarks"
                                                                                                    SortExpression="COMPETITION_MKT_INFO_REMARKS" Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="200px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="200px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="STATUS_NAME" HeaderText=" Status" SortExpression="STATUS_NAME"
                                                                                                    Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="FOLLOWUP_REMARKS" HeaderText=" Followup Remarks" SortExpression="FOLLOWUP_REMARKS"
                                                                                                    Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="200px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="200px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PREV_REMARKS1" HeaderText=" Previous Remarks-I" SortExpression="PREV_REMARKS1"
                                                                                                    Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="200px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="200px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PREV_REMARKS2" HeaderText=" Previous Remarks-II" SortExpression="PREV_REMARKS2"
                                                                                                    Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="200px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="200px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="False" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PERSONMET" HeaderText="Person Met" SortExpression="PERSONMET">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="LOGDATE" HeaderText="Logged Date" SortExpression="LOGDATE">
                                                                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" Width="100px" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="Left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="INTIME" HeaderText="In Time" SortExpression="INTIME" Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="60px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="OUTTIME" HeaderText="Out Time" SortExpression="OUTTIME"
                                                                                                    Visible="true">
                                                                                                    <ItemStyle Wrap="True" Width="60px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="HODNAME" HeaderText="Accompanied By Manager" SortExpression="HODNAME">
                                                                                                    <ItemStyle Wrap="True" Width="90px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" Width="90px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="MANAGERNAME" HeaderText="Accompanied By Reporting Manager"
                                                                                                    SortExpression="MANAGERNAME">
                                                                                                    <ItemStyle Wrap="True" Width="90px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" Width="90px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="STAFFNAME" HeaderText="Acco. By colleague" SortExpression="STAFFNAME">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="JOINTCALLREMARKS" HeaderText="Joint call remarks" SortExpression="JOINTCALLREMARKS">
                                                                                                    <ItemStyle Wrap="True" Width="200px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" Width="200px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="SR_ASSIGNTO" HeaderText="Service Call Assigned To" SortExpression="SR_ASSIGNTO"
                                                                                                    HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="ST_ASSIGNTO" HeaderText="Strategic Call Assigned To" SortExpression="ST_ASSIGNTO"
                                                                                                    HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="LOGMANAGERNAME" HeaderText="Logged By Manager" SortExpression="LOGMANAGERNAME"
                                                                                                    HeaderStyle-Width="120px">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="DATE" HeaderText="Visit Date" SortExpression="DATE" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="80px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                                                                                                </asp:BoundField>
                                                                                                
                                                                                                
                                                                                                
                                                                                                 <asp:BoundField DataField="ST_DEPARTMENT" HeaderText="Department" SortExpression="ST_DEPARTMENT" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="SC_DEPARTMENT_SPECIFIC" HeaderText="Department Specific" SortExpression="SC_DEPARTMENT_SPECIFIC" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_TARGET_CLOSERDATE" HeaderText="Target Closer Date" SortExpression="ST_TARGET_CLOSERDATE" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_CLOSERDATE" HeaderText="Closer Date" SortExpression="ST_CLOSERDATE" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="80px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_RETENTION_REASON" HeaderText="Retention Reason" SortExpression="ST_RETENTION_REASON" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_CPS" HeaderText="CPS" SortExpression="ST_CPS" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="80px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_NEWCPS" HeaderText="New CPS" SortExpression="ST_NEWCPS" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="80px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_A1APPROVED_NEW_DEAL" HeaderText="1A Approval New Deal" SortExpression="ST_A1APPROVED_NEW_DEAL" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_STR_SIGNON_DATE" HeaderText="Signon / Conversion Date" SortExpression="ST_STR_SIGNON_DATE" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_STR_INSTALLATION_DATE" HeaderText="Installation Date" SortExpression="ST_STR_INSTALLATION_DATE" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_STR_PRODUCT" HeaderText="Product" SortExpression="ST_STR_PRODUCT" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_STR_REVENUE" HeaderText="Revenue" SortExpression="ST_STR_REVENUE" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ST_STR_TARGET_SEG" HeaderText="Target Segs % of Business" SortExpression="ST_STR_TARGET_SEG" HeaderStyle-Width="200px">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="true" ForeColor="White" />
                                                                                            <RowStyle CssClass="textbold" Wrap="true" />
                                                                                            <FooterStyle CssClass="Gridheading" />
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" style="width: 850px">
                                                                            <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="850px">
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="850px">
                                                                                    <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                                        <td style="width: 28%" class="left">
                                                                                            <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                                ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3"
                                                                                                Width="100px" ReadOnly="True" Text="0" Visible="True"></asp:TextBox></td>
                                                                                        <td style="width: 33%" class="right">
                                                                                            <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                        <td style="width: 20%" class="center">
                                                                                            <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                                                ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                            </asp:DropDownList></td>
                                                                                        <td style="width: 25%" class="left">
                                                                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next ></asp:LinkButton></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <cc1:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="PnlPrrogress"
                                                                    TargetControlID="PnlPrrogress" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                    ID="ModalLoading" runat="server">
                                                                </cc1:ModalPopupExtender>
                                                                <asp:Panel ID="PnlPrrogress" runat="server" CssClass="overPanel_Test" Height="0px"
                                                                    Width="150px" BackColor="white">
                                                                    <table style="width: 150px; height: 150px;">
                                                                        <tr>
                                                                            <td valign="middle" align="center">
                                                                                <img src="../Images/er.gif" id="img1" runat="server" alt="" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnAuoPostback" CssClass="button" runat="server" Text="exp" Style="display: none;"
                                                                    TabIndex="17" AccessKey="r" Width="115px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <asp:Button ID="Btnexp" CssClass="button" runat="server" Text="exp" Style="display: none;"
                TabIndex="17" AccessKey="r" Width="115px" /></div>
    </form>
</body>

<script type="text/javascript">
   

</script>

</html>
