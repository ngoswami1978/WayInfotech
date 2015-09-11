<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_DSRLogging.aspx.vb"
    Inherits="Sales_SASR_DSRLogging" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: DSR </title>
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
    function CallValidationForSave()
    {
    //try
//{

//12 is checkbox
//13 is textbox for remark
//alert(document.getElementById('grdDSRReport').rows[1].cells.length);

// var dotcount=0;
//     for(intcnt=1;intcnt<=document.getElementById('grdDSRReport').rows.length-1;intcnt++)
//    {  
//         if (document.getElementById('grdDSRReport').rows[intcnt].cells[9].children[0] !=null )
//            {
//                      if (document.getElementById('grdDSRReport').rows[intcnt].cells[9].children[0].type=="text")
//                { 
//                var strValue = document.getElementById('grdDSRReport').rows[intcnt].cells[9].children[0].value.trim();
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
//                            document.getElementById('<%=grdDSRReport.ClientID%>').rows[intcnt].cells[9].children[0].focus();
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
     function CallValidation()
     {
          document.getElementById("lblError").innerHTML="";
       if  ( window.document.getElementById ('txtDateOfDSR').value=='')
       {
           document.getElementById("lblError").innerHTML ="DSR date is mandatory.";
           document.getElementById('txtDateOfDSR').focus();
           return false;
       }
       
        if(document.getElementById('txtDateOfDSR').value != '')
        {
        if (isDate(document.getElementById('txtDateOfDSR').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "DSR date is not valid.";			
	       document.getElementById('txtDateOfDSR').focus();
	       return false ;  
        }
         }  
         ShowPopupTabChange();
      
     }
    
      function OpenVisitYESNO(RowId,DSRVistedId,UnplannedVisit,Lcode,txtReasonNotVisitPlan,ChkVisitYesNo,HdPlannedVisitDone,HdUnPlannedVisitDone)
    {
       //alert(document.getElementById(ChkVisitYesNo).checked);
      if (document.getElementById(ChkVisitYesNo).checked==true )
      {
       document.getElementById(txtReasonNotVisitPlan).readOnly=true;
       document.getElementById(txtReasonNotVisitPlan).className='textboxgrey';
       document.getElementById(HdPlannedVisitDone).value="1";
//      // document.getElementById(ChkVisitYesNo).checked=true
//      
//      if (UnplannedVisit=="TRUE")
//         {
//           document.getElementById(HdPlannedVisitDone).value="0";  
//           document.getElementById(HdUnPlannedVisitDone).value="1"             
//         }
      
      }
      else
      {
       document.getElementById(txtReasonNotVisitPlan).readOnly=false;
       document.getElementById(txtReasonNotVisitPlan).className='textbox';
      // document.getElementById(ChkVisitYesNo).checked=true
       
     //  document.getElementById(ChkVisitYesNo).checked=true
//       document.getElementById(HdPlannedVisitDone).value="0"
       document.getElementById(HdUnPlannedVisitDone).value="0"
     }
//     
//     alert( document.getElementById(HdUnPlannedVisitDone).value);
//     alert( document.getElementById(HdPlannedVisitDone).value);
      return true;
    
    }
    function OpenVistDetails(RowId,DSRVistedId,UnplannedVisit,ResId,Lcode,ChainCode,VisitDate,strIsManager)
    {
    
       var parameter="RowId=" + RowId  + "&DSRVistedId=" + DSRVistedId  +  "&UnplannedVisit=" + UnplannedVisit    + "&ResId=" + ResId + "&Lcode=" + Lcode + "&ChainCode=" + ChainCode  + "&VisitDate=" + VisitDate  +"&FrmType=DSRLOG" + "&IsManager=" + strIsManager ;       
       //alert(parameter);
       
       type = "SASR_VisitDetails.aspx?" + parameter;
       window.open(type,"Sa","height=600;width=1080,top=30,left=20,scrollbars=1,status=1,resizable=1");            
       $get("<%=btnUpdateRemark.ClientID %>").click();  
       return false;    
    
    
    }
    function OpenUnplannedVisit()
    {
       debugger;
       document.getElementById("lblError").innerHTML="";
       if  ( window.document.getElementById ('txtDateOfDSR').value=='')
       {
           document.getElementById("lblError").innerHTML ="DSR date is mandatory.";
             document.getElementById('txtDateOfDSR').focus();
           return false;
       }
         if(document.getElementById('txtDateOfDSR').value != '')
        {
        if (isDate(document.getElementById('txtDateOfDSR').value,"d/M/yyyy") == false)	
        {
           document.getElementById('lblError').innerText = "DSR date is not valid.";			
	       document.getElementById('txtDateOfDSR').focus();
	       return(false);  
        }
         }  
       var PREDATE=window.document.getElementById ('txtDateOfDSR').value;
       var parameter="&PREDATE=" + PREDATE  ;
       var IsManager;
       
       if (document.getElementById('hdIsManager').value=="1")
            {            
                IsManager="&IsManager=" + "True";
            }
        else
            {
                IsManager="&IsManager=" + "False";
            }      
              
       type = "SASR_Agency.aspx?" + parameter + IsManager ;
       //alert (type);
       window.open(type,"Sa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
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
     //alert(elem.id);
     //alert(elem.value);
  //  
      if (elem.id!="btnNew"   &&  elem.id!="btnSearch" && elem.id!="BtnSave" && elem.id!="BtnExport" &&  elem.id!="BtnReset"  &&   elem.id!="LnkCalender" &&   elem.id!="BtnCalender" && elem.id!="btnUp"  && elem.id!="lnkAdvance"   )
       {
       ShowPopupTabChange();
      }
   }
  
  function EndRequests(sender,args)
   { 
  // EndRequest();
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
    
     
     
                                                                    
    </script>

</head>
<body onload="Defaultfunction();">
    <form id="form1" runat="server" defaultbutton ="btnSearch">
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
                                            <span class="menu">Sales -&gt;</span><span class="sub_menu">DSR </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="heading center" style="width: 860px;">
                                                        File DSR</td>
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
                                                                        <td style="width: 860px">
                                                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 860px" class="left">
                                                                                <tr>
                                                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;&nbsp;</td>
                                                                                    <td class="textbold" style="width: 100px">
                                                                                        DSR Date<span class="Mandatory">*</span></td>
                                                                                    <td class="textbold" style="width: 200px">
                                                                                        <asp:TextBox ID="txtDateOfDSR" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox>
                                                                                        <img id="Img_DSRDate" alt="" runat="server" src="../Images/calender.gif" tabindex="1"
                                                                                            title="Date selector" style="cursor: pointer" />
                                                                                    </td>
                                                                                    <td style="width: 200px">
                                                                                        &nbsp;
                                                                                    <asp:Button ID="btnUpdateRemark" runat="server" Style="display: none;" Width="2px" /></td>
                                                                                    <td class="center" style="width: 200px">
                                                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"
                                                                                            AccessKey="A" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" colspan="3">
                                                                                        <asp:Label ID="lblPlannedUnplanned" runat="server" Text="Label"></asp:Label>&nbsp;
                                                                                        &nbsp;<a href="#" onclick="javascript:OpenUnplannedVisit();"
                                                                                            style="width: 125px" id="BtnSelectAgency" runat="server">Select Agency</a>
                                                                                    </td>
                                                                                    <td class="center" style="width: 200px">
                                                                                        <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" TabIndex="2"
                                                                                            AccessKey="A" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                        &nbsp;</td>
                                                                                    <td class="textbold" style="width: 100px">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 200px">
                                                                                    </td>
                                                                                    <td style="width: 200px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="center" style="width: 200px">
                                                                                        <asp:Button ID="BtnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                                            AccessKey="A" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="subheading" colspan="6">
                                                                                    <asp:ImageButton ID="btnUp" runat="server" ImageUrl="../Images/down.jpg" OnClick="btnUp_Click"  />
                                                                                     &nbsp;&nbsp;
                                                                                     <asp:LinkButton ID="lnkAdvance" CssClass="menu" Text=" Show/hide Columns"  runat="server" TabIndex="1" OnClick="lnkAdvance_Click"  ></asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                <tr>
                                                                                    <td style="width: 20px">
                                                                                    </td>
                                                                                    <td colspan="4" style="width: 100%">
                                                                                        <asp:Panel ID="PnlShowUnhideColumns" Visible="true" runat="server" Style="width: 100%">
                                                                                            <table width="100" cellpadding="1" cellspacing="1">                                                                                                
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkLcode" TabIndex="1" Text="LCode" runat="server" CssClass="textbold"
                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkAddress" TabIndex="1" Text="Address" runat="server" CssClass="textbold"
                                                                                                            Width="170px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkMIDT" TabIndex="1" Text="Potential" runat="server" CssClass="textbold"
                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkBCommit" TabIndex="1" Text="Business commit" runat="server" CssClass="textbold"
                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkSalesObj" TabIndex="1" Text="Sales Obj. Visit" runat="server" CssClass="textbold"
                                                                                                            Width="110px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkChaincode" TabIndex="1" Text="Chain Code" runat="server"
                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkCateg" TabIndex="1" Text="Category" runat="server"
                                                                                                            CssClass="textbold" Width="170px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkBIDT" TabIndex="1" Text="BIDT" runat="server" CssClass="textbold"
                                                                                                            Width="110px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkPlanVisitDone" TabIndex="1" Text="Plan Visit Done" runat="server" CssClass="textbold"
                                                                                                            Width="110px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                            
                                                                                                            </td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkLogDate" TabIndex="1" Text="Logged Date" runat="server" CssClass="textbold"
                                                                                                            Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkCity" TabIndex="1" Text="City" runat="server"
                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkPastMotive" TabIndex="1" Text="1A Daily Motives" runat="server"
                                                                                                            CssClass="textbold" Width="170px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkMinSeg" TabIndex="1" Text="Min Seg" runat="server"
                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkUnPlanVisitDone" TabIndex="1" Text="UnPlan Visit Done" runat="server"
                                                                                                            CssClass="textbold" Width="125px" TextAlign="Right" AutoPostBack="False"></asp:CheckBox>
                                                                                                       </td>
                                                                                                    <td>
                                                                                                     <asp:CheckBox ID="ChkDesig" TabIndex="1" Text="Designation" runat="server" CssClass="textbold"
                                                                                                            Width="110px" TextAlign="Right" AutoPostBack="False" Visible ="false" ></asp:CheckBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkPersonMet" TabIndex="1" Text="Person Met" runat="server" CssClass="textbold"
                                                                                                            Width="110px" TextAlign="Right" AutoPostBack="False"  Visible ="false"></asp:CheckBox></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="ChkAcBYManger" TabIndex="1" Text="Accompanied By Manager" runat="server"
                                                                                                          Width="170px"    CssClass="textbold"  TextAlign="Right" AutoPostBack="False"  Visible ="false"></asp:CheckBox></td>
                                                                                                    <td colspan="3">
                                                                                                        <asp:CheckBox ID="ChkAcBYRepManger" TabIndex="1" Text="Accompanied By Reporting Manager"
                                                                                                            runat="server" CssClass="textbold" Width="220px" TextAlign="Right" AutoPostBack="False"  Visible ="false">
                                                                                                        </asp:CheckBox>
                                                                                                        </td>
                                                                                                        
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                     <asp:HiddenField ID="hdAdvanceSearch" EnableViewState="true" runat="server" />
                                                                                                        &nbsp;</td>
                                                                                                    <td >
                                                                                                        &nbsp;</td>
                                                                                                    <td colspan="3">
                                                                                                        &nbsp;</td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td class="center" style="width: 200px">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100%">
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:GridView TabIndex="1" ID="grdDSRReport" runat="server" AutoGenerateColumns="False"
                                                                                            HorizontalAlign="Left" Width="100%" ShowFooter="False" HeaderStyle-CssClass="Gridheading"
                                                                                            RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" RowStyle-VerticalAlign="top"
                                                                                            HeaderStyle-ForeColor="white" AllowPaging="false" PageSize="25" AllowSorting="false">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="  "  HeaderImageUrl ="~/Images/empty-flg.gif"  SortExpression="COLORCODE">                                                                  
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Image ImageUrl="" runat ="server"  ID ="ImgColorCode" />
                                                                                                        <asp:HiddenField ID="hdColorCode" runat="server" Value='<%#Eval("COLORCODE")%>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle Width="30px" />
                                                                                                    <ItemStyle Width="30px" Wrap="false" HorizontalAlign ="center" />
                                                                                                 </asp:TemplateField>
                                                                                                <asp:BoundField DataField="LCODE" HeaderText="LCode" SortExpression="LCODE">
                                                                                                    <ItemStyle Wrap="False" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code" SortExpression="CHAIN_CODE">
                                                                                                    <ItemStyle Wrap="False" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="AGENCY_NAME" HeaderText="Agency Name" SortExpression="AGENCY_NAME">
                                                                                                    <ItemStyle Wrap="True" Width="180px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="180px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="ADDRESS" HeaderText="Address" SortExpression="ADDRESS">
                                                                                                    <ItemStyle Wrap="True" Width="190px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" Width="190px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="CITY_NAME" HeaderText="City" SortExpression="CITY_NAME">
                                                                                                    <ItemStyle Wrap="True" Width="100px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" Width="100px"/>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="GROUP_CATG_NAME" HeaderText="Category" SortExpression="GROUP_CATG_NAME">
                                                                                                    <ItemStyle Wrap="True" Width="60px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left"  Width="60px"/>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="OFFICEID" HeaderText="OfficeID" SortExpression="OFFICEID">
                                                                                                    <ItemStyle Wrap="True" Width="80px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" Width="80px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="MIDT" HeaderText="Potential" SortExpression="MIDT">
                                                                                                    <ItemStyle Wrap="False" Width="60px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="BIDT" HeaderText="BIDT" SortExpression="BIDT">
                                                                                                    <ItemStyle Wrap="false" Width="60px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="BCCOMMIT" HeaderText="Commitment" SortExpression="BCCOMMIT">
                                                                                                    <ItemStyle Wrap="True" Width="90px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="BCMINSEGMENT" HeaderText="Min Segment" SortExpression="BCMINSEGMENT">
                                                                                                    <ItemStyle Wrap="True" Width="90px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="A1DAILYMOTIVES" HeaderText="1A Daily Motive" SortExpression="A1DAILYMOTIVES">
                                                                                                    <ItemStyle Wrap="True" Width="90px" HorizontalAlign="right" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Visited (Y/N)  " ItemStyle-HorizontalAlign="Left">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="ChkVisitYesNo" runat="server" OnCheckedChanged="ChkVisitYesNo_CheckedChanged"
                                                                                                            AutoPostBack="true" />
                                                                                                        <asp:HiddenField ID="HdVisitYesNo" runat="server" Value='<% #Container.DataItem("VISITED") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="left" Width="100px" />
                                                                                                    <ItemStyle HorizontalAlign="center" VerticalAlign="top" Width="100px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Reason for no visit as per Planned Visit">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtReasonNotVisitPlan" Width="205px" Rows="3" Height="30px" TextMode="MultiLine"
                                                                                                            TabIndex="1" runat="server" Text='<%# Eval("REASON_REMARKS") %>' CssClass="textboxgrey"
                                                                                                            ReadOnly="true" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Left" Width="210px" VerticalAlign="top" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="OBJ_VISITCOUNT" HeaderText="Sales Obj visit" SortExpression="OBJ_VISITCOUNT">
                                                                                                    <ItemStyle Wrap="True" Width="100px" />
                                                                                                    <HeaderStyle Wrap="true" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Planned Visit Done" ItemStyle-HorizontalAlign="Left">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblPlannedVisitDone" runat="server" Text='<% #Container.DataItem("PLAN_VISIT_DONE") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="center" Width="120px" />
                                                                                                    <ItemStyle HorizontalAlign="center" VerticalAlign="top" Width="120px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="UnPlanned Visit Done" ItemStyle-HorizontalAlign="Left">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblUnPlannedVisitDone" runat="server" Text='<% #Container.DataItem("UNPLAN_VISIT_DONE") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="center" Width="120px" />
                                                                                                    <ItemStyle HorizontalAlign="center" VerticalAlign="top" Width="120px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="VISIT_TYPE_NAME" HeaderText="Visit Sub Type" SortExpression="VISIT_TYPE_NAME" Visible ="false" >
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="DISCUSSION_ISSUE_REMARKS" HeaderText="Detailed Discussion/ Issue Reported"
                                                                                                    SortExpression="DISCUSSION_ISSUE_REMARKS" Visible ="false" >
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="COMPETITION_MKT_INFO_REMARKS" HeaderText="Competition  Info/Mkt info Remarks"
                                                                                                    SortExpression="COMPETITION_MKT_INFO_REMARKS" Visible ="false">
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="STATUS_NAME" HeaderText=" Status" SortExpression="STATUS_NAME" Visible ="false" >
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="FOLLOWUP_REMARKS" HeaderText=" Followup Remarks" SortExpression="FOLLOWUP_REMARKS" Visible ="false">
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PREV_REMARKS1" HeaderText=" Previous remarks-I DD/MM/YY"
                                                                                                    SortExpression="PREV_REMARKS1" Visible ="false">
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="140px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PREV_REMARKS2" HeaderText=" Previous Remarks-II DD/MM/YY"
                                                                                                    SortExpression="PREV_REMARKS2" Visible ="false">
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="true" Width="140px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION" Visible ="false"  >
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="False" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PERSONMET" HeaderText="Person Met" SortExpression="PERSONMET" Visible ="false"  >
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false"  Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="LOGDATE" HeaderText="Logged Date" SortExpression="LOGDATE">
                                                                                                    <ItemStyle Wrap="True" Width="80px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false" HorizontalAlign="left" Width="80px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="INTIME" HeaderText="In Time" SortExpression="INTIME" Visible ="false">
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="OUTTIME" HeaderText="Out Time" SortExpression="OUTTIME" Visible ="false">
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle Wrap="false" Width="60px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="MANAGERNAME" HeaderText="Accompanied By Manager" SortExpression="MANAGERNAME" Visible ="false">
                                                                                                    <ItemStyle Wrap="True" Width="90px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" Width="90px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="HODNAME" HeaderText="Accompanied By Reporting Manager"
                                                                                                    SortExpression="HODNAME" Visible ="false">
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true"   Width="90px"/>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="RESP_NAME" HeaderText="Sales Executive" Visible ="false" 
                                                                                                    SortExpression="RESP_NAME">
                                                                                                    <ItemStyle Wrap="True" Width="0px" HorizontalAlign="left" />
                                                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left"  Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Action">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="LnkVisitDetail" runat="server" CommandName="VisitDetailsx" Text="Visit Details"
                                                                                                            CssClass="LinkButtons" Width="70px"></asp:LinkButton>&nbsp;<asp:LinkButton ID="LnkDelete"
                                                                                                                runat="server" CommandName="Deletex" Text="Delete" CommandArgument='<% #Container.DataItem("ROWID") %>'
                                                                                                                CssClass="LinkButtons" Width="40px"></asp:LinkButton>
                                                                                                        <asp:HiddenField ID="hdDSRVistedId" runat="server" Value='<% #Container.DataItem("DSR_VISIT_ID") %>' />
                                                                                                        <asp:HiddenField ID="hdRowId" runat="server" Value='<% #Container.DataItem("ROWID") %>' />
                                                                                                        <asp:HiddenField ID="HdResID" runat="server" Value='<% #Container.DataItem("RESP_1A") %>' />
                                                                                                        <asp:HiddenField ID="HdUnpllanedVisit" runat="server" Value='<% #Container.DataItem("UNPLANVISIT") %>' />
                                                                                                        <asp:HiddenField ID="HdLcode" runat="server" Value='<% #Container.DataItem("LCODE") %>' />
                                                                                                        <asp:HiddenField ID="HdChainCode" runat="server" Value='<% #Container.DataItem("CHAIN_CODE") %>' />
                                                                                                        <asp:HiddenField ID="HdPreDate" runat="server" Value='<% #Container.DataItem("PREDATE") %>' />
                                                                                                        <asp:HiddenField ID="HdPlannedVisitDone" runat="server" Value='<% #Container.DataItem("PLAN_VISIT_DONE") %>' />
                                                                                                        <asp:HiddenField ID="HdUnPlannedVisitDone" runat="server" Value='<% #Container.DataItem("UNPLAN_VISIT_DONE") %>' />
                                                                                                        <asp:HiddenField ID="HdObjVisitCount" runat="server" Value='<% #Container.DataItem("OBJ_VISITCOUNT") %>' />
                                                                                                        
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Wrap="False" HorizontalAlign="left" />
                                                                                                    <HeaderStyle HorizontalAlign="left" Width="125px" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="true" ForeColor="White" />
                                                                                            <RowStyle CssClass="textbold" Wrap="true" />
                                                                                            <FooterStyle CssClass="Gridheading" />
                                                                                            <PagerTemplate>
                                                                                            </PagerTemplate>
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
                                                    <asp:Button ID="BtnAppendUnplannedData" CssClass="button" runat="server" Text=""
                                                        TabIndex="2" Style="display: none;" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    
                                </table>
                                <asp:HiddenField ID="hdIsManager" runat="server" />
                                       <input type="hidden" runat="server" id="hdBackDateAllow" style="width: 1px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>

<script type="text/javascript">
   

</script>

</html>
