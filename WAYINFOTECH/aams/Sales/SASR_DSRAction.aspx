<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_DSRAction.aspx.vb"
    Inherits="Sales_SASR_DSRAction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Sales::DSR Action</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">
    
   
    
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
   if ( document.getElementById ('drpVisitSubType').value=="2" )
   {
     document.getElementById ('drpStrategicCallType').disabled=false;
   }
   else
   {
   document.getElementById ('drpStrategicCallType').disabled=true;
   document.getElementById ('drpStrategicCallType').value="";
   }

}

function PopupPage(id)
{
   // debugger;
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

 function PostData()
    {
        if (document.getElementById('gvDSRAction') !=null)
        {
            // return true;
            
              $get("<%=BtnAuoPostback.ClientID %>").click(); 
        }
        else
        {
          return true;
        }
    }
    
function ValidateForm()
{
        //Checking txtOpenDateFrom .

        if (document.getElementById('<%=txtDateFrom.ClientId%>').value!="")
        {
            if (isDate(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date from is not valid.";			
	           document.getElementById('<%=txtDateFrom.ClientId%>').focus();
	           return(false);  
            }
       }
         //Checking txtOpenDateTo .
        if (document.getElementById('<%=txtDateTo.ClientId%>').value!="")
        {
            if (isDate(document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date to is not valid.";			
	           document.getElementById('<%=txtDateTo.ClientId%>').focus();
	           return(false);  
            }
       }
      
         if (document.getElementById('<%=txtDateTo.ClientId%>').value!=""  && document.getElementById('<%=txtDateFrom.ClientId%>').value!="" ) 
      {
            if (compareDates(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy")==1)
           {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Date to should be greater than or equal to Date from.'
                return false;
           }
        }
         // ShowPopupTabChange();
          
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
      
  function OpenDetails(DSRCODE,DSR_DETAILID,VISITSUBTYPE,STRATEGICTYPE,Lcode,VisitDate )
    {
    
       var parameter="DSRCODE=" + DSRCODE  + "&DSR_DETAILID=" + DSR_DETAILID  +  "&VISITSUBTYPE=" + VISITSUBTYPE    + "&STRATEGICTYPE=" + STRATEGICTYPE + "&Lcode=" + Lcode   + "&VisitDate=" + VisitDate ;
      // alert(parameter);
       type = "SAUP_DSRAction.aspx?" + parameter;
       window.open(type,"Sa","height=550,width=880,top=30,left=20,scrollbars=1,status=1,resizable=1");            
       return false;   
    }
      function DeleteDetails(DSRCODE,DSR_DETAILID)
    {
    
            if (confirm("Are you sure you want to delete?")==true)
            {    
                 document.getElementById('<%=hdDSR_DETAILID.ClientId%>').value  =DSR_DETAILID
                  document.getElementById('<%=HdDSRCODE.ClientId%>').value  =DSRCODE
                 
                 return true;	        
            }
            return false;
    }


    
         function ExportData()
 {
 
 
          document.getElementById("lblError").innerHTML="";
            if (document.getElementById('<%=txtDateFrom.ClientId%>').value!="")
        {
            if (isDate(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date from is not valid.";			
	           document.getElementById('<%=txtDateFrom.ClientId%>').focus();
	           return(false);  
            }
       }
         //Checking txtOpenDateTo .
        if (document.getElementById('<%=txtDateTo.ClientId%>').value!="")
        {
            if (isDate(document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date to is not valid.";			
	           document.getElementById('<%=txtDateTo.ClientId%>').focus();
	           return(false);  
            }
       }
      
         if (document.getElementById('<%=txtDateTo.ClientId%>').value!=""  && document.getElementById('<%=txtDateFrom.ClientId%>').value!="" ) 
      {
            if (compareDates(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy")==1)
           {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Date to should be greater than or equal to Date from.'
                return false;
           }
        }
       
        $get("<%=Btnexp.ClientID %>").click(); 
 
     
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


    
   function Defaultfunction()
  {
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
      if (elem.id!="btnSearch"  && elem.id!="BtnExport" &&  elem.id!="BtnReset"  &&   elem.id!="lnkEdit" &&   elem.id!="lnkDelete"   &&   elem.id!="drpVisitSubType" &&   elem.id!="drpStrategicCallType" && elem.id!="btnUp"  && elem.id!="lnkAdvance"   )
       {
       ShowPopupTabChange();
      }
   }
  
  function EndRequests(sender,args)
   { 
  // EndRequest();
   }        
     

    </script>

</head>
<body onload="Defaultfunction();">
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
        <asp:ScriptManager ID="Sc1" runat="server" AsyncPostBackTimeout="800">
        </asp:ScriptManager>
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
                                                <span class="menu">Sales -&gt;</span><span class="sub_menu">DSR Action</span></td>
                                        </tr>
                                        <tr>
                                            <td class="heading center">
                                                <table>
                                                    <tr>
                                                        <td style="width: 840px;">
                                                            Search DSR Action</td>
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
                                                                            <td style="width: 840px">
                                                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 840px" class="left">
                                                                                    <tr>
                                                                                        <td class="center" colspan="6" style="height: 17px">
                                                                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td class="textbold">
                                                                                            Agency Name</td>
                                                                                        <td colspan="3">
                                                                                            <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="50"
                                                                                                TabIndex="2" Width="528px"></asp:TextBox>
                                                                                            <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"
                                                                                                src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                                        <td style="width: 12%;" class="center">
                                                                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 3%">
                                                                                        </td>
                                                                                        <td class="textbold" style="width: 15%">
                                                                                            OfficeID</td>
                                                                                        <td style="width: 27%">
                                                                                            <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textbox" MaxLength="25" TabIndex="2"
                                                                                                Width="170px"></asp:TextBox></td>
                                                                                        <td class="textbold" style="width: 15%">
                                                                                            DSR Code</td>
                                                                                        <td style="width: 26%">
                                                                                            <asp:TextBox ID="txtDRSCode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="2"
                                                                                                Width="170px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
                                                                                        <td class="center" style="width: 13%">
                                                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td class="textbold">
                                                                                            Lcode</td>
                                                                                        <td style="width: 27%">
                                                                                            <asp:TextBox ID="txtLcode1" runat="server" CssClass="textbox" MaxLength="25" TabIndex="2"
                                                                                                Width="170px"></asp:TextBox></td>
                                                                                        <td class="textbold" style="width: 15%">
                                                                                            Assigned By</td>
                                                                                        <td style="width: 26%">
                                                                                            <asp:DropDownList ID="DlstAssignedBy" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                                onkeyup="gotop(this.id)" Width="174px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 13%" class="center">
                                                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <input id="hdCallAssignedTo" runat="server" style="width: 1px" type="hidden" /></td>
                                                                                        <td class="textbold">
                                                                                            Visit Sub Type</td>
                                                                                        <td style="width: 27%">
                                                                                            <asp:DropDownList ID="drpVisitSubType" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                                onkeyup="gotops(this.id)" Width="174px" AutoPostBack="True">
                                                                                                <asp:ListItem Text="--All--" Selected="True" Value=""></asp:ListItem>
                                                                                                <asp:ListItem Text="Service Call" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Strategic Call" Value="2"></asp:ListItem>
                                                                                            </asp:DropDownList></td>
                                                                                        <td class="textbold" style="width: 15%">
                                                                                            Strategic Call Type</td>
                                                                                        <td style="width: 26%">
                                                                                            <asp:DropDownList ID="drpStrategicCallType" runat="server" CssClass="dropdownlist"
                                                                                                TabIndex="2" onkeyup="gotops2(this.id)" Width="174px" AutoPostBack="True">
                                                                                                <asp:ListItem Text="--All--" Selected="True" Value=""></asp:ListItem>
                                                                                                <asp:ListItem Text="Target Call" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Retention Call" Value="2"></asp:ListItem>
                                                                                                <asp:ListItem Text="Air Non Air Call" Value="3"></asp:ListItem>
                                                                                            </asp:DropDownList></td>
                                                                                        <td class="center" style="width: 13%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 25px">
                                                                                            <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                            Status</td>
                                                                                        <td style="height: 25px">
                                                                                            <asp:DropDownList ID="drpStatus" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                                onkeyup="gotop(this.id)" Width="174px">
                                                                                            </asp:DropDownList></td>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                            Assigned To</td>
                                                                                        <td style="height: 25px">
                                                                                            <asp:DropDownList ID="DlstAssignedTo" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                                onkeyup="gotop(this.id)" Width="174px">
                                                                                            </asp:DropDownList></td>
                                                                                        <td style="height: 25px">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true" /></td>
                                                                                        <td class="textbold">
                                                                                            Date From</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" TabIndex="2" Width="170px"></asp:TextBox>
                                                                                            <img id="imgDateFrom" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                                                runat="server" style="cursor: pointer" /></td>
                                                                                        <td class="textbold">
                                                                                            Date To</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox" TabIndex="2" Width="170px"></asp:TextBox>
                                                                                            <img id="imgDateTo" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                                                runat="server" style="cursor: pointer" /></td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <input type="hidden" id="hdEmployeePageName" style="width: 1px" runat='server' />
                                                                                        </td>
                                                                                        <td class="textbold" valign="top">
                                                                                            Region</td>
                                                                                        <td valign="top">
                                                                                            <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                                onkeyup="gotop(this.id)" Width="174px">
                                                                                            </asp:DropDownList></td>
                                                                                        <td class="textbold" valign="top">
                                                                                            Department</td>
                                                                                        <td valign="top">
                                                                                            <asp:Panel ID="PnlDepartment" runat="server" ScrollBars="Vertical" Height="100px"
                                                                                                BorderWidth="1px">
                                                                                                <asp:CheckBoxList ID="ChkLstDepartment" runat="server" CssClass="textbox" TextAlign="Right"
                                                                                                    RepeatDirection="Vertical">
                                                                                                </asp:CheckBoxList>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    <td class="subheading" colspan="6">
                                                                                    <asp:ImageButton ID="btnUp" runat="server" ImageUrl="../Images/down.jpg" OnClick="btnUp_Click" />
                                                                                     &nbsp;&nbsp;
                                                                                     <asp:LinkButton ID="lnkAdvance" CssClass="menu" Text=" Show/hide Columns"  runat="server" TabIndex="1" OnClick="lnkAdvance_Click" ></asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td colspan="4" valign="top">
                                                                                            <asp:Panel ID="PnlShowUnhideColumns" Visible="true" runat="server" Style="width: 100%">
                                                                                                <table width="100" cellpadding="0" cellspacing="0">
                                                                                                    
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:CheckBox ID="ChkLcode" TabIndex="1" Text="LCode" runat="server" CssClass="textbold"
                                                                                                                Width="90px" TextAlign="Right" AutoPostBack="false"></asp:CheckBox></td>
                                                                                                        <td>
                                                                                                            <asp:CheckBox ID="ChkAssignedBy" TabIndex="1" Text="Assigned By" runat="server" Checked="false"
                                                                                                                CssClass="textbold" Width="100px" TextAlign="Right" AutoPostBack="false" Visible="true" />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:CheckBox ID="ChkVisitSubType" TabIndex="1" Text="Visit SubType" runat="server"
                                                                                                                Visible="true" CssClass="textbold" Width="110px" TextAlign="Right" AutoPostBack="false">
                                                                                                            </asp:CheckBox></td>
                                                                                                        <td>
                                                                                                            <asp:CheckBox ID="ChkAssignedTo" TabIndex="1" Text="Assigned To" runat="server" Checked="false"
                                                                                                                CssClass="textbold" Width="100px" TextAlign="Right" AutoPostBack="false" Visible="true" />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:CheckBox ID="ChkRegion" TabIndex="1" Text="Region" runat="server" CssClass="textbold"
                                                                                                                Width="100px" TextAlign="Right" AutoPostBack="false"></asp:CheckBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:CheckBox ID="ChkDepartment" TabIndex="1" Text="Department" runat="server" CssClass="textbold"
                                                                                                                Width="100px" TextAlign="Right" AutoPostBack="false"></asp:CheckBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                        <td class="center"><asp:HiddenField ID="hdAdvanceSearch" EnableViewState="true" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center" style="height: 12px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" width="100%">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 100%">
                                                                                            <asp:GridView ID="gvDSRAction" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                Width="1430px" EnableViewState="true" AllowSorting="True">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="  "  HeaderImageUrl ="~/Images/empty-flg.gif"  SortExpression="COLORCODE">
                                                                                                      
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Image ImageUrl="" runat ="server"  ID ="ImgColorCode" />
                                                                                                            <asp:HiddenField ID="hdColorCode" runat="server" Value='<%#Eval("COLORCODE")%>' />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="30px" />
                                                                                                        <ItemStyle Width="30px" Wrap="false" HorizontalAlign ="center" />
                                                                                                    </asp:TemplateField>
                                                                                                    
                                                                                                    <asp:BoundField HeaderText="DSR Code" DataField="DSRCODE" SortExpression="DSRCODE"
                                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                                        <HeaderStyle Width="70px" Wrap="False" />
                                                                                                        <ItemStyle Width="70px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="LCODE" DataField="LCODE" SortExpression="LCODE" HeaderStyle-HorizontalAlign="Left">
                                                                                                        <HeaderStyle Width="70px" Wrap="False" />
                                                                                                        <ItemStyle Width="70px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Agency Name" DataField="NAME" SortExpression="NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                                        <HeaderStyle Width="150px" Wrap="False" />
                                                                                                        <ItemStyle Width="150px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Office Id" DataField="OFFICEID" SortExpression="OFFICEID"
                                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                                        <ItemStyle Width="70px" />
                                                                                                        <HeaderStyle Width="70px" Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Aoffice" DataField="AOFFICE" SortExpression="AOFFICE"
                                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                                        <ItemStyle Width="70px" />
                                                                                                        <HeaderStyle Width="70px" Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Target Closer Date" DataField="TARGET_CLOSER_DATETIME"
                                                                                                        SortExpression="TARGET_CLOSER_DATETIME" HeaderStyle-HorizontalAlign="Left">
                                                                                                        <ItemStyle Width="100px" />
                                                                                                        <HeaderStyle Width="100px" Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="DSR Date" DataField="DSR_DATETIME" SortExpression="DSR_DATETIME"
                                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                                        <ItemStyle Width="100px" />
                                                                                                        <HeaderStyle Width="100px" Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Status" DataField="STATUS" SortExpression="STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                                        <ItemStyle Width="100px" />
                                                                                                        <HeaderStyle Width="100px" Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Assigned By" DataField="ASSIGNEDBYNAME" SortExpression="ASSIGNEDBYNAME"
                                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                                        <HeaderStyle Width="120px" Wrap="False" />
                                                                                                        <ItemStyle Width="120px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Department" DataField="DEPARTMENT" SortExpression="DEPARTMENT"
                                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                                        <HeaderStyle Width="120px" Wrap="False" />
                                                                                                        <ItemStyle Width="120px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Assigned To" DataField="ASSIGNEDTONAME" SortExpression="ASSIGNEDTONAME"
                                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                                        <HeaderStyle Width="120px" Wrap="False" />
                                                                                                        <ItemStyle Width="120px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Visit Sub Type" DataField="VISITSUBTYPE" SortExpression="VISITSUBTYPE"
                                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                                        <HeaderStyle Width="120px" Wrap="False" />
                                                                                                        <ItemStyle Width="120px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Region" DataField="REGION" SortExpression="REGION" HeaderStyle-HorizontalAlign="Left">
                                                                                                        <HeaderStyle Width="120px" Wrap="False" />
                                                                                                        <ItemStyle Width="120px" />
                                                                                                    </asp:BoundField>
                                                                                                 
                                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete"
                                                                                                                CssClass="LinkButtons"></asp:LinkButton>
                                                                                                            <asp:HiddenField ID="hdDSRID" runat="server" Value='<%#Eval("DSRCODE")%>' />
                                                                                                            <asp:HiddenField ID="hdDSR_DETAILID" runat="server" Value='<%#Eval("DSR_DETAIL_ID")%>' />
                                                                                                            <asp:HiddenField ID="HdLcode" runat="server" Value='<%#Eval("LCODE")%>' />
                                                                                                            <asp:HiddenField ID="HdStatus" runat="server" Value='<%#Eval("STATUS")%>' />
                                                                                                            <asp:HiddenField ID="HdVISITSUBTYPE" runat="server" Value='<%#Eval("VISITSUBTYPE")%>' />
                                                                                                            <asp:HiddenField ID="HdSTRATEGICTYPE" runat="server" Value='<%#Eval("STRATEGICTYPE")%>' />
                                                                                                            <asp:HiddenField ID="hdPreDate" runat="server" Value='<%#Eval("DSR_DATETIME")%>' />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="100px" />
                                                                                                        <ItemStyle Width="100px" Wrap="false"  />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                <RowStyle CssClass="textbold" />
                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" />
                                                                                            <asp:HiddenField ID="hdDSR_DETAILID" runat="server" Value='' />
                                                                                            <asp:HiddenField ID="HdDSRCODE" runat="server" Value='' />
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
            <asp:Button ID="Btnexp" CssClass="button" runat="server" Text="exp" Style="display: none;"
                TabIndex="17" AccessKey="r" Width="115px" />
        </div>
    </form>
</body>
</html>
