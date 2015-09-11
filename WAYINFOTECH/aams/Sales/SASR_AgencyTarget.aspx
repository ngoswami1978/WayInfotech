<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_AgencyTarget.aspx.vb"
    Inherits="Sales_SASR_AgencyTarget" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency Target</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
 .confirmationBackground {
	background-color:#434040;
	filter:alpha(opacity=20);
	opacity:0.7;
}
.confirmationPopup 
{
	
	background-color:#ffffdd;
	border:3px solid #0457b7;
	padding:px;
	width:250px;
	background-color:#ffffff;
	border-top-width:3px;

}
 .modalCloseButton	{
	    background-image:url(../Images/strip_tab.jpg);
	    background-repeat:repeat-x;
	    background-color:#f9f9f9;	
	    font-family:Verdana;
	    font-size:10px;
	    color:#0457b7;
	    border-top:1px solid #0457b7;
	    border-bottom:2px solid #0457b7;
	    border-left:1px solid #0457b7;
	    border-right:1px solid #0457b7;
	    text-align:center;
	    vertical-align:middle;
	    text-decoration:none;
	    cursor:pointer ;
    }
</style>

    <script type="text/javascript" src="../JavaScript/AAMS.js">
    </script>

    <script type="text/javascript">
 
    </script>

    <script type="text/javascript" language="javascript">
    
    
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
        
        
         function ShowPopupCalender()
        {
        try
        {
        var modal = $find('mdlPopUpCalender'); 
       // document.getElementById('Panel1').style.height='150px';
          window.document.getElementById('iframeID').height="270px";
          modal.show(); 
          document.getElementById('PnlCalenderImagePnl').className   ="displayBlock";
          document.getElementById('CalenderImage').className  ="displayBlock";
          
        }
         catch(err){}
         
        } 
      
        function CloseCalander()
        {
        try
        {
           var modal = $find('mdlPopUpCalender'); 
           window.document.getElementById('iframeID').src=''; 
           modal.hide(); 
        }
         catch(err){}
         
        }   
    
    function OpenPlanVisitCalender (Month, Year,MaxVisit,UserDefVisit,UserDefVisitDays ,Lcode,SalesId,VT)
    {    
       if (UserDefVisit=='' )
       {
         UserDefVisit=0;
       }
         if (MaxVisit=='' )
       {
         MaxVisit=0;
       }
       
       var parameter="MaxVisit=" + MaxVisit  + "&UserDefVisit=" + UserDefVisit  +  "&UserDefVisitDays=" + UserDefVisitDays    + "&Lcode=" + Lcode  + "&Year=" + Year + "&Month=" + Month +  "&SalesPersonId=" + SalesId + "&VT=" + VT;
       type = "SAUP_PlanDayCalender.aspx?" + parameter;
      //  window.open(type,"Sa","height=510,width=650,top=30,left=20,scrollbars=1,status=1");   
       document.getElementById("iframeID").src = type;     
       ShowPopupCalender();  
         
       return false;    
    }
    
      function OpenHistoryFunction (Month, Year,MaxVisit,UserDefVisit,UserDefVisitDays ,Lcode,SalesId)
    {    
       var parameter="MaxVisit=" + MaxVisit  + "&UserDefVisit=" + UserDefVisit  +  "&UserDefVisitDays=" + UserDefVisitDays    + "&Lcode=" + Lcode  + "&Year=" + Year + "&Month=" + Month +  "&SalesPersonId=" + SalesId;
       type = "SASR_AgencyTargetHistory.aspx?" + parameter;
       window.open(type,"Sa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");            
       return false;    
    }
    
      function MandatoryFunction()
      {
       
        if(document.getElementById("drpTarCity").value=='' &&   document.getElementById("drpTarAoffice").value=='' &&   document.getElementById("drpSalesPerson").value=='')
        {     
           if(document.getElementById("drpTarAoffice").value=='')
          {
          document.getElementById("drpTarAoffice").focus();
          }
            if(document.getElementById("drpTarCity").value=='')
          {
          document.getElementById("drpTarCity").focus();
          }   
          
             if(document.getElementById("drpSalesPerson").value=='')
          {
          document.getElementById("drpSalesPerson").focus();
          }   
          
        document.getElementById("lblError").innerHTML ="Either city or 1A Responsible or Aoffice is  mandatory";
        return false;
        }
       
       
    ShowPopupTabChange();
           
            
    }
    
     function PostData()
    {
        if (document.getElementById('grdAgencyTarget') !=null)
        {
            // return true;
            
              $get("<%=BtnAuoPostback.ClientID %>").click(); 
        }
        else
        {
          return true;
        }
    }
    
function radionewexisting() 
{   
 
    var rdoIncrease = document.getElementById('<%=rdSummaryOption1.ClientID%>'); 
    var rdoDecrease = document.getElementById('<%=rdSummaryOption2.ClientID%>'); 
    var text= document.getElementById('<%=btnIncrease.ClientID%>'); 
    if (rdoIncrease.checked)     
    { 
    document.getElementById('<%=btnIncrease.ClientID%>').value="Increase";
    return;
    }
    else
    {
     document.getElementById('<%=btnIncrease.ClientID%>').value="Decrease";
     return;
    }   
   }
   
    function disableall()
  {
      document.getElementById("drpPreviousYear").disabled = true;
      document.getElementById("drpPreviousMonth").disabled = true;
      document.getElementById("btn_Select").disabled = true;
  }
 function Enableall()
 {
   var objchbPrevious =document.getElementById('chbPrevious');  
  if (objchbPrevious.checked==true) 
  {
      document.getElementById("drpPreviousYear").disabled = false;
      document.getElementById("drpPreviousMonth").disabled = false;
       document.getElementById("btn_Select").disabled = false;
      }
      else
      {
      document.getElementById("drpPreviousYear").disabled = true;
      document.getElementById("drpPreviousMonth").disabled = true;
       document.getElementById("btn_Select").disabled = true;
       }
  } 
  
  
    
    function disableVall()
  {
      document.getElementById("drpVPreviousYear").disabled = true;
      document.getElementById("drpVPreviousMonth").disabled = true;
      document.getElementById("btn_VSelect").disabled = true;
  }
  function EnableVall()
  {
         var objChkPrevVisPlan =document.getElementById('ChkPrevVisPlan');
         if (objChkPrevVisPlan.checked==true) 
        {
         document.getElementById("drpVPreviousYear").disabled = false;
         document.getElementById("drpVPreviousMonth").disabled = false;
         document.getElementById("btn_VSelect").disabled = false;
        }
      else
        {
         document.getElementById("drpVPreviousYear").disabled = true;
         document.getElementById("drpVPreviousMonth").disabled = true;
         document.getElementById("btn_VSelect").disabled = true;
       }
  } 
  function EnablOrDisableControls()
  {
      if (document.getElementById ('img1')!=null)
         {
            document.getElementById('img1').style.display ="none";
         }
     Enableall();
     EnableVall();  
     Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequest) 
   //  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);    
     Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CheckOrUnckeckControls);  
  }
  
  function BeginRequest(sender,args)
   {
     var elem = args.get_postBackElement();
     //alert(elem.id);
     //alert(elem.value);
  //  
      if (elem.id!="btnNew"   &&  elem.id!="btnSearch" && elem.id!="BtnSave" && elem.id!="BtnExport" &&  elem.id!="BtnReset"  &&   elem.id!="LnkCalender" &&   elem.id!="lnkHistory" &&   elem.id!="drpTarCity"  )
       {
       ShowPopupTabChange();
      }
   }
  
  function CheckOrUnckeckControls()
   {
   Enableall();
   EnableVall();
  // EndRequest();  
      
        if (document.getElementById("hdTargetLcode").value=="")
              { 
                  document.getElementById("chkGroupProductivity").disabled=true;
                  document.getElementById("chkGroupProductivity").checked==false;        
              }   
              
   }
   
  

   function ValidateRegisterPage()
{
//try
//{

// var dotcount=0;
//     for(intcnt=1;intcnt<=document.getElementById('grdAgencyTarget').rows.length-1;intcnt++)
//    {  
//         if (document.getElementById('grdAgencyTarget').rows[intcnt].cells[9].children[0] !=null )
//            {
//                      if (document.getElementById('grdAgencyTarget').rows[intcnt].cells[9].children[0].type=="text")
//                { 
//                var strValue = document.getElementById('grdAgencyTarget').rows[intcnt].cells[9].children[0].value.trim();
//	            if (strValue != '')
//                      {
//                        reg = new RegExp("^[0-9.]+$"); 
//                        if(reg.test(strValue) == false) 
//                        {
//                          document.getElementById('lblError').innerText ='Only Number allowed.';       
//                          return false;
//                         }
//                      }
//                        for (var i=0; i < strValue.length; i++) 
//	                    {
//		                    if (strValue.charAt(i)=='.')
//		                     { 
//		                     dotcount=dotcount+1;
//		                      }
//		               }
//                        if(dotcount>1)
//                        {
//                            document.getElementById('<%=grdAgencyTarget.ClientID%>').rows[intcnt].cells[9].children[0].focus();
//                            document.getElementById("lblError").innerHTML="Target Invalid";
//                            return false;
//                        }                        
//                 dotcount=0; 
//                 }            
//            }
            ShowPopupTabChange();
//            
//            
//  }
//  }catch(err){}

}
  
  function CheckValidation()
 {

  var dotcount=0;
   if(document.getElementById('<%=txtTarget.ClientID%>').value.trim()!="")
     {
    if(IsDataValid(document.getElementById("txtTarget").value,5)==false)
     {
      document.getElementById('lblError').innerHTML='Percentage is not valid.';             
      document.getElementById("txtTarget").focus();
      return false;
     }    
      var strValue = document.getElementById('<%=txtTarget.ClientID%>').value.trim();
	if (strValue != '')
           
            for (var i=0; i < strValue.length; i++) 
	        {
		        if (strValue.charAt(i)=='.')
		         { 
		         dotcount=dotcount+1;
		          }
		   }
            if(dotcount>1)
            {
                document.getElementById('<%=txtTarget.ClientID%>').focus();
                document.getElementById("lblError").innerHTML="Target Invalid";
                return false;
            }
            
     dotcount=0;               
    }
    
     ShowPopupTabChange();

 } 
 
 function ExportData()
 {
  
//       if(document.getElementById("drpSalesPerson").value=='')
//            {  
//              document.getElementById("lblError").innerHTML ="1A Responsiblily person is  mandatory";
//              return false;
//            }
        $get("<%=Btnexp.ClientID %>").click(); 
      
 
 }
 
 
 //Added by Tapan Nath
   function ActDeAct()
     {
        {debugger;}
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9 &&  whichASC !=13 && whichASC!=18)
        {
        
        if  (document.getElementById("txtLcode").value=='' )
            {
            document.getElementById("hdTargetLcode").value="";
            document.getElementById("chkGroupProductivity").disabled=true;
            document.getElementById("chkGroupProductivity").checked=false;
            }
        else
            {
                document.getElementById("hdTargetLcode").value=document.getElementById("txtLcode").value;
                document.getElementById("chkGroupProductivity").disabled=false;                    
            }
          }  
    	
     }
     
    function EnableDisableTargetGroupProductivity()
    {
            //alert(document.getElementById("hdTargetLcode").value.trim());
          if ( (document.getElementById("txtLcode").value.trim()!='') || ( document.getElementById("hdTargetLcode").value.trim() !='') )
            {   
                document.getElementById("chkGroupProductivity").disabled =false;                
            }
           else
           {        
            document.getElementById("chkGroupProductivity").checked=false; 
            document.getElementById("chkGroupProductivity").disabled =true;       
           }        
        return false;
    }            


    function ActDecTargetLcode()    
    {   
        //alert(document.getElementById("hdTargetLcode").value.trim());
        if (document.getElementById("hdTargetLcode").value.trim()=='')
            {            
            document.getElementById("txtLcode").disabled=false;
            document.getElementById("txtLcode").className="textbox";
                if (document.getElementById("txtLcode").value.trim()!='') 
                {
                    if ( document.forms['form1']['chkGroupProductivity']!=null)
                        {
                         document.getElementById("chkGroupProductivity").disabled =false;
                        }                       
                }
            }
         else
            {
                document.getElementById("txtLcode").disabled=true;
                document.getElementById("txtLcode").className="textboxgrey";
            }

        return false;
    }    
     function CheckValidation()
     {
              if(document.getElementById('<%=txtLcode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtLcode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Lcode.';             
                    document.getElementById("txtLcode").focus();
                    return false;
                 }                  
              }
     }

       function PopupPage(id)
         {
         var type;        
         if (id=="2")
         {
                type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	
         } 
        }
    </script>

</head>
<body onload="EnablOrDisableControls();">
    <form id="form1" runat="server">
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
                                            <span class="menu">Sales -&gt;</span><span class="sub_menu">Agency Target</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="heading" style="width: 1500;">
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        Search Agency Target</td>
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
                                                                        <td style="width: 1500px; text-align: left;">
                                                                            <table border="0" cellpadding="0" cellspacing="1" style="width: 860px" class="left">
                                                                                <tr>
                                                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                
                                                                                 <tr>
                                                                                    <td style="width: 3%">
                                                                                        <input id="hdTargetLcode" runat="server" style="width: 1px" type="hidden" />
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Agency</td>
                                                                                    <td colspan="3">
                                                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="50"
                                                                                            TabIndex="1" Width="92%"></asp:TextBox>
                                                                                        <img alt="" onclick="PopupPage(2);" src="../Images/lookup.gif"
                                                                                            style="cursor: pointer" /></td>
                                                                                    <td class="center" style="width: 18%">
                                                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" Width="120px" AccessKey="A" />
										                                            </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" colspan="2">
                                                                                        Group Productivity
                                                                                        <asp:CheckBox ID="chkGroupProductivity" runat="server" CssClass="textbold" TabIndex="1" TextAlign="Left" Width="144px" /></td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                        </td>
                                                                                    <td style="width: 26%">
                                                                                        </td>
                                                                                    <td class="center" style="width: 18%">
                                                                                    <asp:Button ID="BtnExport" CssClass="button" runat="server" Text="Export" TabIndex="2" Width="120px" AccessKey="E" />
										                                            </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Lcode</td>
                                                                                    <td style="width: 32%">
                                                                                    <asp:TextBox ID="txtLcode" runat="server" onkeyup="checknumeric(this.id);ActDeAct();"  CssClass="textbox" Width="178px" TabIndex="1"></asp:TextBox>
                                                                                        
                                                                                     </td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                       OfficeID</td>
                                                                                    <td style="width: 26%">
                                                                                        <asp:TextBox ID="txtOfficeID" runat="server"  CssClass="textbox" Width="178px" TabIndex="1"></asp:TextBox>
                                                                                     </td>
                                                                                    <td class="center" style="width: 18%">
                                                                                    <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" TabIndex="2" OnClientClick="return ValidateRegisterPage()" Width="120px" AccessKey="s" />
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        City<span class="Mandatory">*</span></td>
                                                                                    <td style="width: 32%">
                                                                                        <asp:UpdatePanel ID="UpdateCity" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="drpTarCity" runat="server" AutoPostBack="true" CssClass="dropdownlist"
                                                                                                    onkeyup="gotops(this.id)" Style="position: relative" TabIndex="1" Width="184px"
                                                                                                    OnSelectedIndexChanged="drpTarCity_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="drpTarCity" EventName="SelectedIndexChanged" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                        1A Responsibility</td>
                                                                                    <td style="width: 26%"  >
                                                                                        <asp:UpdatePanel ID="UpdtSalesPerson" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:UpdateProgress ID="UpDateProgress1" AssociatedUpdatePanelID="UpdateCity" DisplayAfter="10" runat="server">
                                                                                                    <ProgressTemplate>
                                                                                                        <asp:Label ID="lblLoading" Text ="Loading" runat ="server" CssClass="textbox" ></asp:Label>
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>
                                                                                                <asp:DropDownList ID="drpSalesPerson" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                                    TabIndex="1" Width="184px">
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="drpTarCity" EventName="SelectedIndexChanged" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td class="center" style="width: 18%">                                                                                        
                                                                                        <asp:Button ID="BtnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" Width="120px" AccessKey="R" />
                                                                                     </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%; height: 23px">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%; height: 23px">
                                                                                        Aoffice</td>
                                                                                    <td style="width: 32%; height: 23px">
                                                                                        <asp:DropDownList ID="drpTarAoffice" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="184px">
                                                                                        </asp:DropDownList></td>
                                                                                    <td class="textbold" style="width: 15%; height: 23px">
                                                                                        Region</td>
                                                                                    <td style="width: 26%; height: 23px">
                                                                                        <asp:DropDownList ID="drpTarRegion" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="184px">
                                                                                        </asp:DropDownList></td>
                                                                                    <td class="center" style="width: 18%; height: 23px">                                                                                        
                                                                                        
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Country</td>
                                                                                    <td style="width: 32%" valign="top">
                                                                                        <asp:DropDownList ID="drpTarCountry" runat="server" CssClass="dropdownlist" Width="184px"  onkeyup="gotop(this.id)" TabIndex="1" >
                                                                                        </asp:DropDownList></td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                        Agency Category</td>
                                                                                    <td style="width: 26%"  class="textbold" valign ="top" >
                                                                                      <asp:CheckBoxList ID="ChkLstAgencyCategory" runat="server" RepeatColumns ="8" RepeatDirection="Horizontal" CssClass ="textbox" TabIndex="1">
                                                                                        </asp:CheckBoxList>
                                                                                    
                                                                                        </td>
                                                                                    <td class="center" style="width: 18%"> 
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Year</td>
                                                                                    <td style="width: 32%">
                                                                                        <asp:DropDownList ID="drpYear" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                                            TabIndex="1" Width="184px">
                                                                                        </asp:DropDownList></td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                        Month</td>
                                                                                    <td style="width: 26%">
                                                                                        <asp:DropDownList ID="drpMonth" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                                                            position: relative; top: 0px" TabIndex="1" Width="184px">
                                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td class="center" style="width: 18%">                                                                                        
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Visit</td>
                                                                                    <td colspan="3"  class="textbold">
                                                                                        <asp:CheckBoxList ID="ChkLstVisitPlanType" runat="server" RepeatColumns ="4" RepeatDirection="Horizontal" CssClass ="textbox" TabIndex="1" Width="640px" >
                                                                                        </asp:CheckBoxList></td>
                                                                                    <td class="center" style="width: 18%">
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 42%" colspan="2">
                                                                                        Select Targets from previous month<asp:CheckBox ID="chbPrevious" runat="server" CssClass="textbold"
                                                                                            Style="left: 8px; position: relative; top: 0px" TabIndex="1" TextAlign="Left"
                                                                                            Width="120px" />
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 43%" colspan="2">
                                                                                    </td>
                                                                                    <td class="center" style="width: 18%">
                                                                                       </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Year</td>
                                                                                    <td style="width: 32%">
                                                                                        <asp:DropDownList ID="drpPreviousYear" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                                            TabIndex="1" Width="184px">
                                                                                        </asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btn_Select" runat="server" CssClass="button"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="2" Text="Select" Width="56px" /></td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                        <asp:RadioButton ID="rdSummaryOption1" runat="server" Checked="True" CssClass="textbold"
                                                                                            GroupName="Type" Text="Increase Target" Width="112px" TabIndex="1" /></td>
                                                                                    <td rowspan="2" style="width: 26%" align="center" valign="middle">
                                                                                        <span class="textbold">&nbsp; &nbsp; By(%)</span>&nbsp;<asp:TextBox ID="txtTarget"
                                                                                            onkeyup="checknumeric(this.id)" runat="server" CssClass="textbox right" MaxLength="2"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="56px" Wrap="False"
                                                                                            EnableViewState="true">0</asp:TextBox>&nbsp;
                                                                                        <asp:Button ID="btnIncrease" TabIndex="2" CssClass="button" runat="server" Text="Increase"
                                                                                            Style="left: 0px; position: relative; top: 0px" Width="71px" />
                                                                                    </td>
                                                                                    <td class="center" style="width: 18%">
                                                                                        </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Month</td>
                                                                                    <td style="width: 32%">
                                                                                        <asp:DropDownList ID="drpPreviousMonth" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                                                            position: relative; top: 0px" TabIndex="7" Width="184px">
                                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                        <asp:RadioButton ID="rdSummaryOption2" runat="server" CssClass="textbold" GroupName="Type"
                                                                                            Text="Decrease Target" Width="112px" /></td>
                                                                                    <td class="center" style="width: 18%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%; height: 24px;">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 42%; height: 24px;" colspan="2">
                                                                                        Select visit plan from previous month<asp:CheckBox ID="ChkPrevVisPlan" runat="server"
                                                                                            CssClass="textbold" Style="left: 8px; position: relative; top: 0px" TabIndex="1"
                                                                                            TextAlign="Left" Width="120px" />
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 43%; height: 24px;" colspan="2">
                                                                                    </td>
                                                                                    <td class="center" style="width: 18%; height: 24px;">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Year</td>
                                                                                    <td style="width: 32%">
                                                                                        <asp:DropDownList ID="drpVPreviousYear" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                                            TabIndex="1" Width="184px">
                                                                                        </asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btn_VSelect" runat="server" CssClass="button"
                                                                                            Style="left: 0px; position: relative; top: 0px" TabIndex="2" Text="Select" Width="56px" /></td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                    </td>
                                                                                    <td rowspan="2" style="width: 26%" align="center" valign="middle">
                                                                                        <span class="textbold">&nbsp; &nbsp; &nbsp;&nbsp;</span></td>
                                                                                    <td class="center" style="width: 18%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 10%">
                                                                                        Month</td>
                                                                                    <td style="width: 32%">
                                                                                        <asp:DropDownList ID="drpVPreviousMonth" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                                                            position: relative; top: 0px" TabIndex="1" Width="184px">
                                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td class="textbold" style="width: 15%">
                                                                                    </td>
                                                                                    <td class="center" style="width: 18%">
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                

                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td  colspan="4">
                                                                                       
                                                                                     </td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td style="width: 3%">
                                                                                    </td>
                                                                                    <td colspan="4" style="width: 100%">
                                                                                        
                                                                                    <asp:UpdatePanel ID="UpdatePnlMain" runat="server">
                                                                                            <ContentTemplate>
                                                                                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td colspan="4" style="width: 100%" class="subheading" >
                                                                                                      <asp:Panel ID="pHeader" runat="server" >
                                                                                                            <img alt="" src="../Images/down.jpg" style="cursor:pointer" id="btnUp"  runat="server" tabindex="1" />&nbsp;&nbsp;                                                                                                
                                                                                                        <asp:Label ID="lblText" runat="server" CssClass ="subheading" Style="cursor: pointer"  />
                                                                                                    </asp:Panel>                                                                                          
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table> 
                                                                                            
            
                                                                                             <asp:Panel ID="PnlShowUnhideColumns" Visible="true" runat="server" Style="width: 100%" Width="100%">
                                                                                            <table style="width: 100%" cellpadding="0" cellspacing="0">                                                                                             
                                                                                                <tr>
                                                                                                    <td style="width: 15%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkLcode" TabIndex="1" Text="LCode" runat="server" CssClass="textbold"
                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="chkOfficeID" runat="server" Text="Office ID" Width="90px" CssClass="textbold" /></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkCateg" TabIndex="1" Text="Category" runat="server" CssClass="textbold"
                                                                                                            Width="100px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkPastMotive" TabIndex="1" Text="Last Month Motives" runat="server"
                                                                                                            CssClass="textbold" Width="170px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 25%; height: 23px; text-align: left;">
                                                                                                        <asp:CheckBox ID="chkCalender" runat="server" Text="Calender" Width="170px" CssClass="textbold" /></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 15%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkChaincode" TabIndex="1" Text="Chain Code" runat="server" CssClass="textbold"
                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="chkAddress" runat="server" Text="Address" Width="90px" CssClass="textbold" /></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkMinSeg" TabIndex="1" Text="Min Seg" runat="server" CssClass="textbold"
                                                                                                            Width="100px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkMIDT" TabIndex="1" Text="Three Months Avg Potential" runat="server"
                                                                                                            CssClass="textbold" Width="170px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 25%; height: 23px;">
                                                                                                        <asp:CheckBox ID="chkPVisit" runat="server" Text="Planned Visit Done" Width="170px" CssClass="textbold" /></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 15%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkCity" TabIndex="1" Text="City" runat="server" CssClass="textbold"
                                                                                                            Width="90px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="chkResp1a" runat="server" Text="Sales Executive" Width="120px" CssClass="textbold" /></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkBCommit" TabIndex="1" Text="Business commit" runat="server"
                                                                                                            CssClass="textbold" Width="120px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 20%; height: 23px;">
                                                                                                        <asp:CheckBox ID="ChkBIDT" TabIndex="1" Text="Three Months Avg BIDT" runat="server"
                                                                                                            CssClass="textbold" Width="170px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td style="width: 25%; height: 23px;">
                                                                                                        <asp:CheckBox ID="chkUPVisit" runat="server" Text="Unplanned Visit Done" Width="170px"  CssClass="textbold"/></td>
                                                                                                </tr>                                                                                           
                                                                                            </table>
                                                                                        </asp:Panel>                                                                                                   
                                                                                                                                                                               
                                                                                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"  
                                                                                                TargetControlID="PnlShowUnhideColumns"
                                                                                                CollapseControlID="pHeader"
                                                                                                ExpandControlID="pHeader" 
                                                                                                Collapsed="true" 
                                                                                                TextLabelID="lblText"
                                                                                                CollapsedText="Show Columns" 
                                                                                                ExpandedText="Hide Columns" 
                                                                                                
                                                                                                CollapsedImage="../Images/down.jpg" 
                                                                                                ExpandedImage="../Images/up.jpg"
                                                                                                ImageControlID="btnUp"

                                                                                                
                                                                                                SuppressPostBack="true" />
                                                                                        </ContentTemplate>
                                                                                     </asp:UpdatePanel>
                                                                                     
                                                                                        
                                                                                        
                                                                                        

                                                                                    </td>
                                                                                    <td class="center" style="width: 18%">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <table width="100%" border="1" cellspacing="0" cellpadding="0">
                                                                                <td>
                                                                                    <asp:Label ID="LbltdLeft" runat="server"> </asp:Label></td>
                                                                                <td class="textbold">
                                                                                    <asp:Label ID="LblPlannedDays" Visible="false" runat="server" Text="Planned Days"> </asp:Label></td>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100%" align="left">
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:GridView TabIndex="1" ID="grdAgencyTarget" runat="server" AutoGenerateColumns="False"
                                                                                            HorizontalAlign="left" Width="100%" ShowFooter="true" HeaderStyle-CssClass="Gridheading"
                                                                                            RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" RowStyle-VerticalAlign="top"
                                                                                            HeaderStyle-ForeColor="white" AllowPaging="True" PageSize="25" AllowSorting="True" >
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Action">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="LnkCalender" runat="server" CommandName="Calenderx" Text="Calendar"
                                                                                                                CssClass="LinkButtons" Width="50px"></asp:LinkButton>&nbsp;<asp:LinkButton ID="lnkHistory"
                                                                                                                    runat="server" CommandName="HistoryX" Text="History" CssClass="LinkButtons" Width="50px"></asp:LinkButton>
                                                                                                            <asp:HiddenField ID="hdlcode" runat="server" Value='<% #Container.DataItem("LCODE") %>' />
                                                                                                            <asp:HiddenField ID="hdSalesId" runat="server" Value='<% #Container.DataItem("RESP1A_ID") %>' />
                                                                                                            <asp:HiddenField ID="hdResp_1A" runat="server" Value='<% #Container.DataItem("EMPLOYEEID") %>' />
                                                                                                            <asp:HiddenField ID="hdMonth" runat="server" Value='<% #Container.DataItem("MONTH") %>' />
                                                                                                            <asp:HiddenField ID="hdYear" runat="server" Value='<% #Container.DataItem("YEAR") %>' />
                                                                                                            <asp:HiddenField ID="HdUserDefinedVisit" runat="server" Value='<% #Container.DataItem("VISITTARGET") %>' />
                                                                                                            <asp:HiddenField ID="HdMaxVisit" runat="server" Value='<% #Container.DataItem("VISITCOUNT") %>' />
                                                                                                            <asp:HiddenField ID="HdUserDefinedVisitDays" runat="server" Value='<% #Container.DataItem("PLANNED_DAYS") %>' />
                                                                                                            <asp:HiddenField ID="hdchkVisitTarget" runat="server" Value='<% #Container.DataItem("M_CHK_VT") %>' />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle HorizontalAlign="center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="  " HeaderImageUrl="~/Images/empty-flg.gif" SortExpression="COLORCODE">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Image ImageUrl="" runat="server" ID="ImgColorCode" />
                                                                                                        <asp:HiddenField ID="hdColorCode" runat="server" Value='<%#Eval("COLORCODE")%>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle Width="30px" />
                                                                                                    <ItemStyle Width="30px" Wrap="false" HorizontalAlign="center" />
                                                                                                </asp:TemplateField>
                                                                                                
                                                                                                <asp:BoundField DataField="LCODE" HeaderText="LCode" SortExpression="LCODE">
                                                                                                    <ItemStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="CHAINCODE" HeaderText="Chain Code" SortExpression="CHAINCODE">
                                                                                                    <ItemStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" SortExpression="AGENCYNAME">
                                                                                                    <ItemStyle Wrap="True" Width="160px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="160px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="OFFICEID" HeaderText="Office ID" SortExpression="OFFICEID">
                                                                                                    <ItemStyle Wrap="false" Width="70px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="70px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="ADDRESS" HeaderText="Address" SortExpression="ADDRESS">
                                                                                                    <ItemStyle Wrap="True" Width="200px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="200px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="120px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="GROUP_CATG" HeaderText="Category" SortExpression="GROUP_CATG">
                                                                                                    <ItemStyle Wrap="True" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="RESP1A_NAME" HeaderText="Sales Executive" SortExpression="RESP1A_NAME">
                                                                                                    <ItemStyle Wrap="True" Width="120px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="120px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Segs Target " SortExpression="SEGSTARGET" ItemStyle-HorizontalAlign="Right">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtTarget" Text='<% #DataBinder.Eval(Container.DataItem, "SEGSTARGET") %>'
                                                                                                            onkeyup="checknumeric(this.id)" Width="86%" runat="server" CssClass="textbox right"
                                                                                                            MaxLength="6" Wrap="false" TabIndex="1"></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="left" Width="120px" Wrap ="false"   />
                                                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="top" Width="120px" />
                                                                                                    <FooterStyle Wrap="false" Width="120px" HorizontalAlign="right" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="VISITCOUNT" HeaderText="Sales Objective Visit" SortExpression="VISITCOUNT">
                                                                                                    <ItemStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="center" Width="100px" />
                                                                                                     <FooterStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="VISITTARGET" HeaderText="Visit Target" SortExpression="VISITTARGET">
                                                                                                    <ItemStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" />
                                                                                                    <FooterStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PVISITDONE" HeaderText="Planned Visit Done" SortExpression="PVISITDONE">
                                                                                                    <ItemStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" Width="100px" />
                                                                                                     <FooterStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="UVISITDONE" HeaderText="UnPlanned Visit Done" SortExpression="UVISITDONE">
                                                                                                    <ItemStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" Width="100px" />
                                                                                                     <FooterStyle Wrap="false" Width="100px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="AVGMIDT" HeaderText="Potential" SortExpression="AVGMIDT">
                                                                                                    <ItemStyle Wrap="false" Width="60px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" />
                                                                                                     <FooterStyle Wrap="false" Width="90px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="AVGBIDT" HeaderText="BIDT" SortExpression="AVGBIDT">
                                                                                                    <ItemStyle Wrap="false" Width="60px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" />
                                                                                                     <FooterStyle Wrap="false" Width="90px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                
                                                                                                <asp:BoundField DataField="PASTMOTIVE" HeaderText="Last Motive" SortExpression="PASTMOTIVE">
                                                                                                    <ItemStyle Wrap="false" Width="90px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" Width="90px" />
                                                                                                     <FooterStyle Wrap="false" Width="90px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                                
                                                                                                <asp:BoundField DataField="MINIUMSEGS" HeaderText="Min Segment" SortExpression="MINIUMSEGS">
                                                                                                    <ItemStyle Wrap="false" Width="90px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="true" Width="90px" />
                                                                                                     <FooterStyle Wrap="false" Width="90px" HorizontalAlign="right" />
                                                                                                </asp:BoundField>
                                                                                               
                                                                                                 <asp:BoundField DataField="BUSINESSCCOMMIT" HeaderText="Commitment" SortExpression="BUSINESSCCOMMIT">
                                                                                                    <ItemStyle Wrap="false" Width="90px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="90px" />
                                                                                                  
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="1"  SortExpression ="D1">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate1" Width="20px" Text='<%# Eval("D1") %>' runat="server"></asp:Label>
                                                                                                        <br />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="2" SortExpression ="D2">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate2" Text='<%# Eval("D2") %>' runat="server" Width="100%" Height="100%"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="3" SortExpression ="D3">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate3" Text='<%# Eval("D3") %>' runat="server" Width="100%" Height="100%"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="4" SortExpression ="D4">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate4" Width="20px" Text='<%# Eval("D4") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="5" SortExpression ="D5">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate5" Width="20px" Text='<%# Eval("D5") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="6" SortExpression ="D6">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate6" Width="20px" Text='<%# Eval("D6") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="7" SortExpression ="D7">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate7" Width="20px" Text='<%# Eval("D7") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="8" SortExpression ="D8">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate8" Width="20px" Text='<%# Eval("D8") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="9" SortExpression ="D9">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate9" Width="20px" Text='<%# Eval("D9") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="10" SortExpression ="D10">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate10" Width="20px" Text='<%# Eval("D10") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="11" SortExpression ="D11">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate11" Width="20px" Text='<%# Eval("D11") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="12" SortExpression ="D12">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate12" Width="20px" Text='<%# Eval("D12") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="13" SortExpression ="D13">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate13" Width="20px" Text='<%# Eval("D13") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="14" SortExpression ="D13">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate14" Width="20px" Text='<%# Eval("D14") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="15" SortExpression ="D15">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate15" Width="20px" Text='<%# Eval("D15") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="16" SortExpression ="D16">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate16" Width="20px" Text='<%# Eval("D16") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="17" SortExpression ="D17">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate17" Width="20px" Text='<%# Eval("D17") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="18" SortExpression ="D18">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate18" Width="20px" Text='<%# Eval("D18") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="19" SortExpression ="D19">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate19" Width="20px" Text='<%# Eval("D19") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="20" SortExpression ="D20">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate20" Width="20px" Text='<%# Eval("D20") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="21" SortExpression ="D21">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate21" Width="20px" Text='<%# Eval("D21") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="22" SortExpression ="D22">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate22" Width="20px" Text='<%# Eval("D22") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="23" SortExpression ="D23">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate23" Width="20px" Text='<%# Eval("D23") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="24" SortExpression ="D24">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate24" Width="20px" Text='<%# Eval("D24") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="25" SortExpression ="D25">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate25" Width="20px" Text='<%# Eval("D25") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="26" SortExpression ="D25">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate26" Width="20px" Text='<%# Eval("D26") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="27" SortExpression ="D27">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate27" Width="20px" Text='<%# Eval("D27") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="28" SortExpression ="D28">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate28" Width="20px" Text='<%# Eval("D28") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="29" SortExpression ="D29">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate29" Width="20px" Text='<%# Eval("D29") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="30" SortExpression ="D30">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate30" Width="20px" Text='<%# Eval("D30") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="31" SortExpression ="D31">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblDate31" Width="20px" Text='<%# Eval("D31") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                
                                                                                            </Columns>
                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="true" ForeColor="White" />
                                                                                            <RowStyle CssClass="textbold" />
                                                                                            <FooterStyle CssClass="Gridheading " />
                                                                                            <PagerTemplate>
                                                                                            </PagerTemplate>
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; padding-top: 10px;">
                                                                            <asp:Panel ID="PnlLegand" runat="server">
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="LblLeftLegand" runat="server" Text=" " Width="600px"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <table border="1" cellpadding="0" cellspacing="0" width="900px">
                                                                                                <tr>
                                                                                                    <td class="textbold">
                                                                                                    </td>
                                                                                                    <td class="textbold">
                                                                                                        <asp:Label ID="LblPlanDay" runat="server" BackColor="Blue" Text="&nbsp;&nbsp;&nbsp;"
                                                                                                            Width="15px" Height="15px"></asp:Label>
                                                                                                        &nbsp;&nbsp; Planned (P)</td>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                    <td class="textbold">
                                                                                                        <asp:Label ID="LblPlanDayVisited" runat="server" BackColor="Green" Text="&nbsp;&nbsp;&nbsp;"
                                                                                                            Width="15px" Height="15px"></asp:Label>
                                                                                                        &nbsp;Planned & Visited (PV)</td>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                    <td class="textbold" style="width: 239px">
                                                                                                        <asp:Label ID="Label1" runat="server" BackColor="Yellow" Text="&nbsp;&nbsp;&nbsp;"
                                                                                                            Width="15px" Height="15px"></asp:Label>
                                                                                                        &nbsp;Unplanned Visited (UV)</td>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                    <td class="textbold">
                                                                                                        <asp:Label ID="LblPlanDaynOTVisited" runat="server" BackColor="Red" Text="&nbsp;&nbsp;&nbsp;"
                                                                                                            Width="15px" Height="15px"></asp:Label>
                                                                                                        &nbsp;Not Visited (NV)</td>
                                                                                                   
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="textbold" style="height: 19px">
                                                                                                    </td>
                                                                                                    <td class="textbold" style="height: 19px">
                                                                                                        <asp:Label ID="Label2" runat="server" BackColor="Purple" Text="&nbsp;&nbsp;&nbsp;"
                                                                                                            Width="15px" Height="15px"></asp:Label>&nbsp;&nbsp;Backdated DSR Logged</td>
                                                                                                    <td style="height: 19px">
                                                                                                    </td>
                                                                                                    <td class="textbold" style="height: 19px">
                                                                                                        <asp:Label ID="Label3" runat="server" BackColor="DarkSalmon" Text="&nbsp;&nbsp;&nbsp;"
                                                                                                            Width="15px" Height="15px"></asp:Label>
                                                                                                        &nbsp;&nbsp;Planned Call By Manager</td>
                                                                                                    <td style="height: 19px">
                                                                                                    </td>
                                                                                                    <td class="textbold" colspan="2" style="width: 239px; height: 19px;">
                                                                                                        <asp:Label ID="Label4" runat="server" BackColor="Fuchsia" Text="&nbsp;&nbsp;&nbsp;"
                                                                                                            Width="15px" Height="15px"></asp:Label>
                                                                                                        &nbsp;&nbsp;Planned Visit NotLogged after 5 days
                                                                                                    </td>
                                                                                                </tr>
                                                                                                
                                                                                            
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <input id="hdSales" runat="server" style="width: 1px" type="hidden" />
                                                                            <input id="hdSearch" runat="server" style="width: 1px" type="hidden" />
                                                                            <asp:HiddenField ID="hdTotal" runat="server" />
                                                                            <asp:HiddenField ID="HdPrevVisitMonth" runat="server" />
                                                                            <asp:HiddenField ID="HdPrevVisitYear" runat="server" />
                                                                            <input id="hdTargetList" runat="server" enableviewstate="true" style="width: 1px"
                                                                                type="hidden" />
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
                                                                                            <span class="textbold"><b>&nbsp;Total Target</b></span>&nbsp;&nbsp;
                                                                                            <asp:TextBox ID="txtTotalTarget" runat="server" Width="112px" CssClass="textboxgrey"
                                                                                                Style="position: relative; left: -5px;"></asp:TextBox>
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
                                                            <td valign ="top" >
                                                                <asp:Button ID="BtnConfirm" Text="" runat="server" CssClass="displayNone" />
                                                                <cc1:ModalPopupExtender ID="mdlPopUpCalender" runat="server" TargetControlID="BtnConfirm"
                                                                    BackgroundCssClass="confirmationBackground" PopupControlID="pnlPopup" CancelControlID="BtnCancel">
                                                                </cc1:ModalPopupExtender>
                                                                <asp:Panel ID="pnlPopup" runat="server" Width="655px" Height="480px" Style="display: none"
                                                                    HorizontalAlign="Center" CssClass="confirmationPopup">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        
                                                                         <tr>
                                                                         <td valign="middle" align ="center"  >     
                                                                            <asp:Panel ID="PnlCalenderImagePnl" runat ="server"    CssClass ="displayNone">
                                                                                    <img src="../Images/er.gif" id="CalenderImage" runat="server" alt="" class ="displayNone" />
                                                                            </asp:Panel>
                                                                         </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="2" valign="top">
                                                                                <iframe width="655px" scrolling="yes" height="270px" src="" id="iframeID" frameborder="0">                                                                               
                                                                                </iframe>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:Button ID="BtnRefreshGrid" Text="" runat="server" CssClass="displayNone" />
                                                                                <asp:Button ID="BtnCancel" Text="" runat="server" CssClass="displayNone" />
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
       function gotops(ddlname)
     {
    
     if (event.keyCode==46 )
     {
        document.getElementById(ddlname).selectedIndex=0;
        setTimeout('__doPostBack(\'drpTarCity\',\'\')', 0)
        //EnableCarrierType3();
     }
     }   

         if (document.getElementById("hdTargetLcode").value=="")
              { 
                  document.getElementById("chkGroupProductivity").disabled=true;
                  document.getElementById("chkGroupProductivity").checked==false;        
              }   
   
</script>

</html>
