<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_VisitDetails.aspx.vb"
    Inherits="Sales_SASR_VisitDetails" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS::Sales::DSR Details</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Sales.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" src="../JavaScript/subModal.js"></script>

    <link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
    <link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />
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

    <script language="javascript" type="text/javascript">   
   
      function ShowPopupTabChange()
        {
        try
        {
            var modal = $find('ModalLoading'); 
            document.getElementById('PnlPrrogress').style.height='150px';
            if (document.getElementById ('img6')!=null)
             {
                document.getElementById('img6').style.display ="block";
             }
            modal.show(); 
        }
         catch(err){}
         
        }  
        
    function ValidateIataStatus()
    {      
        ShowPopupTabChange(); 
      
    }
  function ValidateIata()
  {
    document.getElementById("lblError").innerHTML="";
   if (document.getElementById('DlstIataStatus') != null)
    {
       if (document.getElementById('DlstIataStatus').value.trim()!='')
       {
               if (document.getElementById('DlstIataStatus').value.trim().split("|")[1]!='N')
           {
                    if (document.getElementById('TxtIataID') != null)
                {
                   if (document.getElementById('TxtIataID').value.trim()=='')
                   {
                         document.getElementById("LblUpdateIataError").innerHTML="Please update Iata ID.";
                         document.getElementById('TxtIataID').focus();
                         return false;
                   }
                    // alert(parseInt(document.getElementById('TxtIataID').value.trim(),10));                      
                     if (document.getElementById('TxtIataID').value.trim().length <8)
                   {
                         document.getElementById("LblUpdateIataError").innerHTML="Iata ID is not valid.";
                         document.getElementById('TxtIataID').focus();
                         return false;
                   }
                }           
           }                      
       }
    }   
    
     if (document.getElementById('DlstIataStatus') != null)
    {
       if (document.getElementById('DlstIataStatus').value.trim()=='')
       {
            document.getElementById("LblUpdateIataError").innerHTML="Please update Iata Status.";
            document.getElementById('DlstIataStatus').focus();
            return false;
       }
    }
     if (document.getElementById('DlstIataStatus') != null)
    {
       if (document.getElementById('DlstIataStatus').value.trim()!='')
       {
               if (document.getElementById('DlstIataStatus').value.trim().split("|")[1]=='S')
           {
                    if (document.getElementById('TxtIataOfficeID') != null)
                {
                   if (document.getElementById('TxtIataOfficeID').value.trim()=='')
                   {
                         document.getElementById("LblUpdateIataError").innerHTML="IATA Office ID mandatory.";
                         document.getElementById('TxtIataOfficeID').focus();
                         return false;
                   }
                }           
           }                      
       }
    }  
    ShowPopupTabChange(); 
   return true;
    
  }
  
   function ValidatePageVisitDetails2()
 {
      document.getElementById("lblError").innerHTML="";
   if (document.getElementById('DlstIataStatus') != null)
    {
       if (document.getElementById('DlstIataStatus').value.trim()!='')
       {
               if (document.getElementById('DlstIataStatus').value.trim().split("|")[1]!='N')
           {
                    if (document.getElementById('TxtIataID') != null)
                {
                   if (document.getElementById('TxtIataID').value.trim()=='')
                   {
                         document.getElementById("lblError").innerHTML="Please update Iata ID.";
                         document.getElementById('TxtIataID').focus();
                         return false;
                   }  
                   
                     if (document.getElementById('TxtIataID').value.trim().length < 8)
                   {
                         document.getElementById("LblUpdateIataError").innerHTML="Iata ID is not valid.";
                         document.getElementById('TxtIataID').focus();
                         return false;
                   }
                }           
           }                      
       }
    }   
    
     if (document.getElementById('DlstIataStatus') != null)
    {
       if (document.getElementById('DlstIataStatus').value.trim()=='')
       {
            document.getElementById("lblError").innerHTML="Please update Iata Status.";
            document.getElementById('DlstIataStatus').focus();
            return false;
       }
    }
     if (document.getElementById('DlstIataStatus') != null)
    {
       if (document.getElementById('DlstIataStatus').value.trim()!='')
       {
               if (document.getElementById('DlstIataStatus').value.trim().split("|")[1]=='S')
           {
                    if (document.getElementById('TxtIataOfficeID') != null)
                {
                   if (document.getElementById('TxtIataOfficeID').value.trim()=='')
                   {
                         document.getElementById("lblError").innerHTML="Iata Office ID is mandatory.";
                         document.getElementById('TxtIataOfficeID').focus();
                         return false;
                   }
                   
                }           
           }                      
       }
    }   
    
    
    
     if (document.getElementById('HdCompetionCount') != null)
    {        
        if (document.getElementById('HdCompetionCount').value.trim()=='0' || document.getElementById('HdCompetionCount').value.trim()=='')
        {            
             var Lcode=document.getElementById('hdLCode').value;
             var param  = "../TravelAgency/TAUP_AgencyCompetition.aspx?Id=2&SalesVisit='V'&Action=U|" + Lcode + "&Lcode=" + Lcode;
             window.document.getElementById('iframeID').src='';
             window.document.getElementById('iframeID').src = param;            
             var modal = $find('mdlPopUpCalender'); 
             modal.show();   
             document.getElementById('PnlCalenderImagePnl').className   ="displayBlock";
             document.getElementById('CalenderImage').className  ="displayBlock";         
             return false;
        }          
    }
 
    var grdVisitDetails=document.getElementById("gvVisitDetails");
    if (grdVisitDetails != null)
    {
        if (grdVisitDetails.rows.length <=1)
        {
            document.getElementById("lblError").innerHTML="Please add visit details.";
            document.getElementById('lblErrorVisitDetails').innerHTML="Please add visit details.";
            return false;
        }
     }
     else
     {
            document.getElementById("lblError").innerText="Please add visit details.";
            document.getElementById('lblErrorVisitDetails').innerHTML="Please add visit details.";
            return false;
     }
 
     if (document.getElementById("chkServiceCall").checked==false && document.getElementById("chkStrategicCall").checked==false)
     {
            document.getElementById("lblError").innerHTML="Visit sub type is mandatory.";
            return false;
     }
 }

  
   function CloseCalander()
    {
       //  window.close();
        window.document.getElementById('iframeID').src='';
        window.document.getElementById('BtnCancel').click();        
    }
  
  function FollowupCallPlan()
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;   	           	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID   + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE  ;               
            
               type = "SAUP_FollowupPlanDayCalender.aspx?" + parameter;
               window.open(type,"SAUP_FollowupPlanDayCalender","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false; 
  
  }
  function EditServiceCallFolowupRem(DSR_SC_DETAIL_ID,STATUSID)
  {
    
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;   	           	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID   + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE  + "&SCDETAILID=" + DSR_SC_DETAIL_ID + "&STATUSID=" + STATUSID;
               
              // alert(parameter);
               type = "SASR_SCFollowupRem.aspx?" + parameter;
               window.open(type,"SASR_SCFollowupRem","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false; 
  }
  
  function ShowSCMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowTarMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowRetMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowAirNonAirMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowTarIssueReported(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_IssueReported.aspx?" + parameter;
               window.open(type,"SASR_SCMarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowRetIssueReported(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_IssueReported.aspx?" + parameter;
               window.open(type,"SASR_SCMarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
  
   function ShowAirNonAirIssueReported(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_IssueReported.aspx?" + parameter;
               window.open(type,"SASR_SCMarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
  
    function SignedOnDateByTargetStatus()
    {
              // var cboddlTargetStatusValue=document.getElementById('ddlTargetStatus').value;    
                var cboTarStatus=document.getElementById('ddlTargetStatus').value;
                //alert(cboTarStatus);
                var cboTarStatus1=cboTarStatus.split("|")[0];               
                var cboddlTargetStatusValue=cboTarStatus1;
              
               if (cboddlTargetStatusValue == document.getElementById('hdTargetChk').value)
                 {                    
                     document.getElementById('txtTargetSignedOn').readOnly=false;                       
                     document.getElementById('txtTargetSignedOn').className ="textbox";
                     document.getElementById('imgTargetSignedOn').style.display ="block";
                 }
                 else
                 {
                     document.getElementById('txtTargetSignedOn').readOnly=true;                   
                     document.getElementById('txtTargetSignedOn').className ="textboxgrey";
                     document.getElementById('imgTargetSignedOn').style.display ="none";                     
                     document.getElementById('txtTargetSignedOn').value="";
                  
                 }   
                   var cboTarStatus2=cboTarStatus.split("|")[1];
              
                 if (cboTarStatus2=="0")  
                 {
                  // document.getElementById('DrpTarAssignedTo').disabled=false;
                   document.getElementById('txtTarCloserDate').readOnly=true; 
                   document.getElementById('txtTarCloserDate').value='';
                   document.getElementById('imgTarCloserDate').style.display ="none";
                   document.getElementById('txtTarCloserDate').className ="textboxgrey";                 
                 }
                 else
                 {
                   //document.getElementById('DrpTarAssignedTo').disabled=true;
                   document.getElementById('txtTarCloserDate').readOnly=false;                   
                   document.getElementById('imgTarCloserDate').style.display ="block";
                   document.getElementById('txtTarCloserDate').className ="textbox";
                 }               
                 
                 
                  return false;
    
    
    }
  
   function SignedOnDateByTargetStatusPrev()
    {
      var cboddlTargetStatusValue=document.getElementById('ddlTargetStatus').value;    
               if (cboddlTargetStatusValue == document.getElementById('hdTargetChk').value)
                 {                    
                      if(document.getElementById('btnTargetAdd').value== "Update")
                       {
                               if (document.getElementById('hdTargetSaveData').value=='')  // ' Unsave Data
                               {
                                         document.getElementById('txtTargetSignedOn').readOnly=false;                       
                                         document.getElementById('txtTargetSignedOn').className ="textbox";
                                         document.getElementById('imgTargetSignedOn').style.display ="block";
                               }          
                              
                               else  //// ' Save Data
                               {
                                    if (document.getElementById('txtTargetSignedOn').value=='') 
                                     {
                                         document.getElementById('txtTargetSignedOn').readOnly=false;                       
                                         document.getElementById('txtTargetSignedOn').className ="textbox";
                                         document.getElementById('imgTargetSignedOn').style.display ="block";
                                     }
                                     else
                                     {
                                        document.getElementById('txtTargetSignedOn').readOnly=true;                   
                                        document.getElementById('txtTargetSignedOn').className ="textboxgrey";
                                        document.getElementById('imgTargetSignedOn').style.display ="none";
                                     }
                                 
                               }            
                         }     
                       else
                       {
                           document.getElementById('txtTargetSignedOn').readOnly=false;                       
                           document.getElementById('txtTargetSignedOn').className ="textbox";
                           document.getElementById('imgTargetSignedOn').style.display ="block";
                       }
                 }
                 else
                 {
                    document.getElementById('txtTargetSignedOn').readOnly=true;                   
                    document.getElementById('txtTargetSignedOn').className ="textboxgrey";
                    document.getElementById('imgTargetSignedOn').style.display ="none"; 
                    
                     if(document.getElementById('btnTargetAdd').value== "Update")
                       {
                          if (document.getElementById('hdTargetSaveData').value=='')  // ' Unsave Data
                           {
                                  document.getElementById('txtTargetSignedOn').value="";
                           }   
                       }
                       else
                       {
                        document.getElementById('txtTargetSignedOn').value="";
                       }
                  
                 }   
                  return false;
    
    
    }
    
    
      
    function SignedOnDateByRetStatus()
    {
              // var cboddlRetentionStatusValue=document.getElementById('ddlRetentionStatus').value;    
              
                var cboRetStatus=document.getElementById('ddlRetentionStatus').value;
                var cboRetStatusStatus1=cboRetStatus.split("|")[0];               
                var cboddlRetentionStatusValue=cboRetStatusStatus1;
              
              
               if (cboddlRetentionStatusValue == document.getElementById('hdRetentionChk').value)
                 {         
                       document.getElementById('txtRetentionSignedOn').readOnly=false;                       
                       document.getElementById('txtRetentionSignedOn').className ="textbox";
                       document.getElementById('imgRetentionSignedOn').style.display ="block";
                 }
                 else
                 {
                    document.getElementById('txtRetentionSignedOn').readOnly=true;                   
                    document.getElementById('txtRetentionSignedOn').className ="textboxgrey";
                    document.getElementById('imgRetentionSignedOn').style.display ="none"; 
                     document.getElementById('txtRetentionSignedOn').value="";
                 } 
                 
                 var cboRetStatusStatus2=cboRetStatus.split("|")[1];
              
                 if (cboRetStatusStatus2=="0")  
                 {
                  // document.getElementById('DrpRetAssignedTo').disabled=false;
                   document.getElementById('txtRetCloserDate').readOnly=true; 
                   document.getElementById('txtRetCloserDate').value='';
                   document.getElementById('imgRetCloserDate').style.display ="none";
                   document.getElementById('txtRetCloserDate').className ="textboxgrey";                 
                 }
                 else
                 {
                   // document.getElementById('DrpRetAssignedTo').disabled=true;
                   document.getElementById('txtRetCloserDate').readOnly=false;                   
                   document.getElementById('imgRetCloserDate').style.display ="block";
                   document.getElementById('txtRetCloserDate').className ="textbox";
                 }               
                 
                 
                 
                 
                   
                  return false;
    
    }
    
    function SignedOnDateByRetStatusPrev()
    {
               var cboddlRetentionStatusValue=document.getElementById('ddlRetentionStatus').value;    
               if (cboddlRetentionStatusValue == document.getElementById('hdRetentionChk').value)
                 {                    
                      if(document.getElementById('btnAddRetention').value== "Update")
                       {
                               if (document.getElementById('hdRetentionSavedData').value=='')  // ' Unsave Data
                               {
                                         document.getElementById('txtRetentionSignedOn').readOnly=false;                       
                                         document.getElementById('txtRetentionSignedOn').className ="textbox";
                                         document.getElementById('imgRetentionSignedOn').style.display ="block";
                               }          
                              
                               else  //// ' Save Data
                               {
                                    if (document.getElementById('txtRetentionSignedOn').value=='') 
                                     {
                                         document.getElementById('txtRetentionSignedOn').readOnly=false;                       
                                         document.getElementById('txtRetentionSignedOn').className ="textbox";
                                         document.getElementById('imgRetentionSignedOn').style.display ="block";
                                     }
                                     else
                                     {
                                        document.getElementById('txtRetentionSignedOn').readOnly=true;                   
                                        document.getElementById('txtRetentionSignedOn').className ="textboxgrey";
                                        document.getElementById('imgRetentionSignedOn').style.display ="none";
                                     }
                                 
                               }            
                         }     
                       else
                       {
                           document.getElementById('txtRetentionSignedOn').readOnly=false;                       
                           document.getElementById('txtRetentionSignedOn').className ="textbox";
                           document.getElementById('imgRetentionSignedOn').style.display ="block";
                       }
                 }
                 else
                 {
                    document.getElementById('txtRetentionSignedOn').readOnly=true;                   
                    document.getElementById('txtRetentionSignedOn').className ="textboxgrey";
                    document.getElementById('imgRetentionSignedOn').style.display ="none"; 
                    
                     if(document.getElementById('btnAddRetention').value== "Update")
                       {
                          if (document.getElementById('hdRetentionSavedData').value=='')  // ' Unsave Data
                           {
                                  document.getElementById('txtRetentionSignedOn').value="";
                           }   
                       }
                       else
                       {
                        document.getElementById('txtRetentionSignedOn').value="";
                       }
                  
                 }   
                  return false;
    
    }
     
      function CallAirNonAirForsignDate()
    {   
    
               // var cboAirNonAirStatusValue=document.getElementById('ddlAirNonAirStatus').value;    
               
                var cboAirNonAirStatus=document.getElementById('ddlAirNonAirStatus').value;
                var cboAirNonAirStatus1=cboAirNonAirStatus.split("|")[0];               
                var cboAirNonAirStatusValue=cboAirNonAirStatus1;//document.getElementById('cboAirNonAirStatus').value; 
              // alert(cboAirNonAirStatus);
               
               if (cboAirNonAirStatusValue == document.getElementById('hdAirNonAirChk').value)
                 {             
                     document.getElementById('txtAirNonAirSignedOn').readOnly=false;                       
                     document.getElementById('txtAirNonAirSignedOn').className ="textbox";
                     document.getElementById('imgAirNonAirSignedOn').style.display ="block";
                 }
                 else
                 {
                    document.getElementById('txtAirNonAirSignedOn').readOnly=true;                   
                    document.getElementById('txtAirNonAirSignedOn').className ="textboxgrey";
                    document.getElementById('imgAirNonAirSignedOn').style.display ="none"; 
                    document.getElementById('txtAirNonAirSignedOn').value="";                   
                  
                 } 
                 
                  var cboAirNonAirStatus2=cboAirNonAirStatus.split("|")[1];
              
                 if (cboAirNonAirStatus2=="0")  
                 {
                  // document.getElementById('dlstAssignedTo').disabled=false;
                   document.getElementById('txtAirCloserDate').readOnly=true; 
                   document.getElementById('txtAirCloserDate').value='';
                   document.getElementById('imgAirCloserDate').style.display ="none";
                   document.getElementById('txtAirCloserDate').className ="textboxgrey";                 
                 }
                 else
                 {
                   document.getElementById('txtAirCloserDate').readOnly=false;                   
                   document.getElementById('imgAirCloserDate').style.display ="block";
                   document.getElementById('txtAirCloserDate').className ="textbox";
                 }               
                 
                 
                   
                  return false;
             
    }
    
     
     
    function CallAirNonAirForsignDatePrev()
    {   
    
                var cboAirNonAirStatusValue=document.getElementById('ddlAirNonAirStatus').value;    
               if (cboAirNonAirStatusValue == document.getElementById('hdAirNonAirChk').value)
                 {                    
                      if(document.getElementById('btnAirNonAirAdd').value== "Update")
                       {
                               if (document.getElementById('hdAirNonAirSaveData').value=='')  // ' Unsave Data
                               {
                                         document.getElementById('txtAirNonAirSignedOn').readOnly=false;                       
                                         document.getElementById('txtAirNonAirSignedOn').className ="textbox";
                                         document.getElementById('imgAirNonAirSignedOn').style.display ="block";
                               }          
                              
                               else  //// ' Save Data
                               {
                                    if (document.getElementById('txtAirNonAirSignedOn').value=='') 
                                     {
                                         document.getElementById('txtAirNonAirSignedOn').readOnly=false;                       
                                         document.getElementById('txtAirNonAirSignedOn').className ="textbox";
                                         document.getElementById('imgAirNonAirSignedOn').style.display ="block";
                                     }
                                     else
                                     {
                                        document.getElementById('txtAirNonAirSignedOn').readOnly=true;                   
                                        document.getElementById('txtAirNonAirSignedOn').className ="textboxgrey";
                                        document.getElementById('imgAirNonAirSignedOn').style.display ="none";
                                     }
                                 
                               }            
                         }     
                       else
                       {
                           document.getElementById('txtAirNonAirSignedOn').readOnly=false;                       
                           document.getElementById('txtAirNonAirSignedOn').className ="textbox";
                           document.getElementById('imgAirNonAirSignedOn').style.display ="block";
                       }
                 }
                 else
                 {
                    document.getElementById('txtAirNonAirSignedOn').readOnly=true;                   
                    document.getElementById('txtAirNonAirSignedOn').className ="textboxgrey";
                    document.getElementById('imgAirNonAirSignedOn').style.display ="none"; 
                    
                     if(document.getElementById('btnAirNonAirAdd').value== "Update")
                       {
                          if (document.getElementById('hdAirNonAirSaveData').value=='')  // ' Unsave Data
                           {
                                  document.getElementById('txtAirNonAirSignedOn').value="";
                           }   
                       }
                       else
                       {
                        document.getElementById('txtAirNonAirSignedOn').value="";
                       }
                  
                 }   
                  return false;
                  
                  
    
    
             
    }
    
   function   CallAssignedOnOrOff()
    {            
               //  alert(document.getElementById('hdDSR_SC_DETAIL_ID').value);    
                var cboServiceStatus=document.getElementById('ddlServiceStatus');
                var strStatus=document.getElementById('ddlServiceStatus').value;  
               // alert(strStatus);
                 if (strStatus.split("|")[1]=="0")  
                 {
                   document.getElementById('dlstAssignedTo').disabled=false;
                   document.getElementById('txtCloserDate').readOnly=true; 
                   document.getElementById('imgCloserDate').style.display ="none";
                   document.getElementById('txtCloserDate').className ="textboxgrey";
                                
                    if(document.getElementById('btnAddServiceCall').value== "Update")
                   {                     
                       if (document.getElementById('hdDSR_SC_DETAIL_ID').value =='') // UnSaved Data In Modify Case
                       {
                         document.getElementById('txtCloserDate').value ='';
                       }
                       else
                       {
                       
                       } 
                   }
                   else
                   {
                       document.getElementById('txtCloserDate').value ='';
                   }
                 
                 }
                 else
                 {
                   if(document.getElementById('btnAddServiceCall').value== "Update")
                   {
                       if (document.getElementById('hdDSR_SC_DETAIL_ID').value =='') // UnSaved Data In Modify Case
                       {
                           document.getElementById('dlstAssignedTo').disabled=true; 
                           document.getElementById('txtCloserDate').readOnly=false;                       
                           document.getElementById('txtCloserDate').className ="textbox";
                           document.getElementById('imgCloserDate').style.display ="block";
                       }
                       else  // In Saved Data In Modify Case
                       {
                                   document.getElementById('dlstAssignedTo').disabled=true; 
                                   if (document.getElementById('txtCloserDate').value=='') 
                                     {
                                         document.getElementById('txtCloserDate').readOnly=false;                       
                                         document.getElementById('txtCloserDate').className ="textbox";
                                         document.getElementById('imgCloserDate').style.display ="block";
                                     }
                                     else
                                     {
                                        document.getElementById('txtCloserDate').readOnly=true;                   
                                        document.getElementById('txtCloserDate').className ="textboxgrey";
                                        document.getElementById('imgCloserDate').style.display ="none";
                                     }
//                           document.getElementById('txtCloserDate').readOnly=true;                       
//                           document.getElementById('txtCloserDate').className ="textboxgrey";
//                           document.getElementById('imgCloserDate').style.display ="none";
                       }
                       
                   }
                   else
                   {
                       document.getElementById('dlstAssignedTo').disabled=false;
                       document.getElementById('txtCloserDate').readOnly=false;                       
                       document.getElementById('txtCloserDate').className ="textbox";
                       document.getElementById('imgCloserDate').style.display ="block";
                   
                   }
                     
                 }
                 return false;
    
    }
    
     function AgencyStaff()
         {
              
                 var strAgencyName=document.getElementById("txtAgencyName").value;
                 var LCode=document.getElementById("hdLCode").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                if (strAgencyName!="")
                {
                  //  type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName + "&LCode=" + LCode;
                   type = "../TravelAgency/TASR_TravelAgencySalesStaff.aspx?Popup=T&AgencyName="+strAgencyName + "&LCode=" + LCode;
                  
                  
                    window.open(type,"Sales","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
                }                  
              
              
          }
    
    
     function DetailsFunction()
{
var BCaseID =document.getElementById("hdBCID").value
var ChainCode=document.getElementById("hdChainCode").value
var type;
//type = "../Incentive/INCSR_BacseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID;
type = "../Incentive/INCUP_BacseDetails.aspx?Action=U&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID;

window.open(type,"IncDetails","height=630,width=1000,top=30,left=20,scrollbars=1,status=1,resizable=1");
// window.location ="MSSR_BcaseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID;
return false;
}
        function EmployeePageVisitDetails()
        {
            var type;
            type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");
   	        return false;
         }   
         
     function OpenVisitedItems()
    {
            
   	   var DSR_VISIT_ID=document.getElementById("hdID").value;
   	   var VisitDate=document.getElementById("hdVisitDATE").value;
   	   var LCode=document.getElementById("hdLCode").value;
   	      	   
   	   var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&VisitDate=" + VisitDate  +  "&LCODE=" + LCode;
       type = "SAUP_PREV_VisitedItems.aspx?" + parameter;
       window.open(type,"SAUP_PREV_VisitedItems","height=600,width=980,top=30,left=20,scrollbars=1,status=1,resizable=0");            
       return false;  
   	        
    }
         
         function OpenPrevRemarks(id)
         {
            
               var DSR_VISIT_ID=document.getElementById("hdID").value;
               var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_Previous_Remarks.aspx?" + parameter;
               window.open(type,"SASR_Previous_Remarks","height=600;width=880,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;  
         }
         
        // Closing Form
         function fnCloseForm()
         {
            window.close();
            return false;
         }    
         function ShowCalender(varControlID,varButtonID)
         {
       //  alert(varControlID +' '+varButtonID)
                  Calendar.setup({
                        inputField     :    varControlID,
                        ifFormat       :    "%d/%m/%Y",
                        button         :    varButtonID,
                        //align          :    "Tl",
                        singleClick    :    true
                       
                        });                            
         
         }          
    </script>

    <script type="text/javascript">
     function ShowCal()
     {
         try
         {
            Calendar.setup({
            inputField     :    '<%=txtRetentionSignedOn.ClientId%>',
            ifFormat       :     "%d/%m/%Y",
            button         :    "imgRetentionSignedOn",
            //align          :    "Tl",
            singleClick    :    true
          
            });
            
           
        }
        catch(err)
        {
        alert(err)
        
        }
        
    }
    
    
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <table width="93%" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top left" style="width: 80%">
                                <span class="menu">Sales -> DSR Logging -> </span><span class="sub_menu">DSR Details</span>
                            </td>
                            <td class="right" style="width: 20%">
                                <asp:LinkButton ID="lnkClose" CssClass="LinkButtons" runat="server" OnClientClick="return fnCloseForm()">Close</asp:LinkButton>
                                &nbsp; &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="heading center" colspan="2" style="width: 100%">
                                <asp:Label ID="lblHeading" runat="server" Text="DSR Details"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 100%">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder top" colspan="2" style="width: 100%">
                                            <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td class="center TOP">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" rowspan="6" style="width: 2%">
                                                                </td>
                                                                <td class="textbold" style="width: 19%">
                                                                    Agency Name</td>
                                                                <td colspan="3" style="width: 71%">
                                                                    <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey TextTitleCase"
                                                                        Width="95%" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                <td style="width: 8%">
                                                                    <asp:Button ID="btnSave" runat="server" TabIndex="13" CssClass="button topMargin"
                                                                        Text="Save" Width="110px" OnClientClick="return ValidatePageVisitDetails2()"
                                                                        AccessKey="s" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Address</td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey TextTitleCase"
                                                                        ReadOnly="True" Rows="3" TabIndex="20" TextMode="MultiLine" Width="95%"></asp:TextBox></td>
                                                                <td class="center top">
                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="13" CssClass="button topMargin"
                                                                        Text="Reset" Width="110px" AccessKey="r" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    City</td>
                                                                <td style="width: 28%">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="87%"></asp:TextBox></td>
                                                                <td style="width: 19%" class="textbold">
                                                                    Country</td>
                                                                <td style="width: 28%">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Chain Code
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox>
                                                                </td>
                                                                <td class="textbold">
                                                                    Lcode
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLcode" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Office ID
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox>
                                                                </td>
                                                                <td class="textbold">
                                                                    On Contract</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOnContract" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Past Month Daily Motive</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPastMonthDailyMotive" runat="server" CssClass="textboxgrey right"
                                                                        ReadOnly="True" Width="87%"></asp:TextBox></td>
                                                                <td class="textbold">
                                                                    Current Month Daily Motive</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCurrentMonthDailyMotive" runat="server" CssClass="textboxgrey right"
                                                                        ReadOnly="True" Width="87%"></asp:TextBox></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td class="textbold">
                                                                    Business commit %/Segs</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBusinessCommit" runat="server" CssClass="textboxgrey right" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox></td>
                                                                <td class="textbold">
                                                                    Latest Month 1A %/Segs</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLatestMonth1A" runat="server" CssClass="textboxgrey right" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td class="textbold">
                                                                    DSR Code</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDSRCode" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox></td>
                                                                <td class="textbold">
                                                                    Category
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtCtg" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="87%"></asp:TextBox></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td class="textbold">
                                                                    Total Sales Objective Visit</td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtTotSalesObj" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox></td>
                                                                <td class="textbold">
                                                                    Segment Target
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtSegTarget" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td class="textbold">
                                                                    Visit targets
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtVisitTarget" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox></td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="left">
                                                                    <asp:UpdatePanel ID="UpdateIata" runat="server">
                                                                        <ContentTemplate>
                                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0" style="border: solid 1px black;">
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td class="textbold" colspan="4" align="center">
                                                                                        <asp:Label ID="LblUpdateIataError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 8%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 18%">
                                                                                        IATA ID<span class="Mandatory">*</span></td>
                                                                                    <td style="width: 28%">
                                                                                        <asp:TextBox ID="TxtIataID" runat="server" CssClass="textbox" ReadOnly="false" MaxLength="8"
                                                                                            onkeyup="checknumeric(this.id);" Width="86%"></asp:TextBox></td>
                                                                                    <td class="textbold" style="width: 19%">
                                                                                        IATA Status<span class="Mandatory">*</span>
                                                                                    </td>
                                                                                    <td style="width: 28%">
                                                                                        <asp:DropDownList ID="DlstIataStatus" runat="server" CssClass="dropdownlist" Width="89%"
                                                                                            AutoPostBack="true">
                                                                                        </asp:DropDownList></td>
                                                                                    <td style="width: 8%">
                                                                                        <asp:Button ID="BtnResetIata" runat="server" TabIndex="13" CssClass="button topMargin"
                                                                                            Text="Reset IATA" Width="110px" AccessKey="s" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 18%">
                                                                                        IATA Office ID</td>
                                                                                    <td style="width: 28%">
                                                                                        <asp:TextBox ID="TxtIataOfficeID" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                            Width="86%"></asp:TextBox></td>
                                                                                    <td class="textbold" style="width: 19%">
                                                                                    </td>
                                                                                    <td style="width: 28%">
                                                                                    </td>
                                                                                    <td style="width: 8%">
                                                                                        <asp:Button ID="BtnUpdateIata" runat="server" TabIndex="13" CssClass="button topMargin"
                                                                                            OnClientClick="return ValidateIata();" Text="Update IATA" Width="110px" AccessKey="s" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6">
                                                                                        <ajax:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="PnlPrrogress"
                                                                                            TargetControlID="PnlPrrogress" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                            ID="ModalLoading" runat="server">
                                                                                        </ajax:ModalPopupExtender>
                                                                                        <asp:Panel ID="PnlPrrogress" runat="server" CssClass="overPanel_Test" Height="0px"
                                                                                            Width="150px" BackColor="white" Style="display: none;">
                                                                                            <table style="width: 150px; height: 150px;">
                                                                                                <tr>
                                                                                                    <td valign="middle" align="center">
                                                                                                        <img src="../Images/er.gif" id="img6" runat="server" alt="" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
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
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                            <tr>
                                                                <td class="textbold" style="width: 45%">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tabMIDT">
                                                                        <tr>
                                                                            <td colspan="6" style="width: 100%" class="center">
                                                                                Average Last Three Month MIDT</td>
                                                                        </tr>
                                                                        <tr class="center bold">
                                                                            <td style="width: 25%">
                                                                                1A</td>
                                                                            <td style="width: 25%">
                                                                                1B</td>
                                                                            <td style="width: 12%">
                                                                                1G</td>
                                                                            <td style="width: 13%">
                                                                                1P</td>
                                                                            <td style="width: 12%">
                                                                                1W</td>
                                                                            <td style="width: 13%">
                                                                                Total</td>
                                                                        </tr>
                                                                        <tr class="right" style="height: 16px">
                                                                            <td>
                                                                                <asp:Literal ID="lit1A" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="lit1B" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="lit1G" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="lit1P" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="lit1W" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="textbold top" style="width: 38%">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tabMIDT">
                                                                        <tr>
                                                                            <td colspan="3" style="width: 100%" class="center">
                                                                                Last Three Month BIDT</td>
                                                                        </tr>
                                                                        <tr class="center bold" style="height: 16px">
                                                                            <td style="width: 33%">
                                                                                <asp:Literal ID="litMonthName1" runat="server" Text="Month-1"></asp:Literal>
                                                                            </td>
                                                                            <td style="width: 33%">
                                                                                <asp:Literal ID="litMonthName2" runat="server" Text="Month-2"></asp:Literal>
                                                                            </td>
                                                                            <td style="width: 34%">
                                                                                <asp:Literal ID="litMonthName3" runat="server" Text="Month-3"></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="right" style="height: 16px">
                                                                            <td>
                                                                                <asp:Literal ID="litMonth1" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="litMonth2" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="litMonth3" runat="server"></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 12%">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <asp:UpdatePanel ID="updPnlVisitDetails" runat="server">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                                    <tr>
                                                                        <td class="top" style="width: 100%;">
                                                                            <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                <tr>
                                                                                    <td style="width: 15%;">
                                                                                    </td>
                                                                                    <td style="width: 25%;">
                                                                                    </td>
                                                                                    <td style="width: 20%;">
                                                                                    </td>
                                                                                    <td style="width: 28%;">
                                                                                    </td>
                                                                                    <td style="width: 12%;">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" style="width: 100%;" class="heading">
                                                                                        Visit Details
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" style="width: 100%;">
                                                                                        <asp:Label ID="lblErrorVisitDetails" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr runat="server" id="tblTRLoggedByManager">
                                                                                    <td>
                                                                                        Logged by Manager
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_logbyManager" runat="server" Text="Label"></asp:Label></td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td class="center">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Acco. By Manager
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlManager" runat="server" CssClass="dropdownlist" Width="87%"
                                                                                            TabIndex="1">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        Acco. By Reporting Manager
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlReportingManager" runat="server" CssClass="dropdownlist"
                                                                                            Width="86%" TabIndex="1">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="center">
                                                                                        <asp:Button ID="btnAddVisitDetails" Text="Add" runat="Server" CssClass="button" Width="110px"
                                                                                            OnClientClick="javascript:return ValidateVisitDetails();" TabIndex="2" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Person Met <span class="Mandatory">*</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtPersonMet" runat="server" ReadOnly="true" CssClass="textboxgrey"
                                                                                            Width="84%" TabIndex="1"></asp:TextBox><img style="cursor: pointer" tabindex="4"
                                                                                                onclick="javascript:return AgencyStaff();" alt="Person Met" src="../Images/lookup.gif"
                                                                                                runat="server" id="ImgPersonMet" />
                                                                                    </td>
                                                                                    <td>
                                                                                        Designation <span class="Mandatory">*</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="textboxgrey" ReadOnly="true"
                                                                                            Width="84%" TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="center">
                                                                                        <asp:Button ID="btnCancelVisitDetails" Text="Cancel" runat="Server" CssClass="button"
                                                                                            Width="110px" TabIndex="2" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        In Time (HHMM)<span class="Mandatory">*</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtInTime" runat="server" CssClass="textbox" Width="84%" MaxLength="4"
                                                                                            TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        Out Time (HHMM)<span class="Mandatory">*</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtOutTime" runat="server" CssClass="textbox" Width="84%" MaxLength="4"
                                                                                            TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="center" style="vertical-align: top;">
                                                                                        <asp:Button ID="BtnFollowupCallPlan" Text="Followup Call Plan" runat="server" CssClass="button"
                                                                                            Width="110px" TabIndex="2" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        Acco. By colleague
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <asp:DropDownList ID="DrpAccByCollegeus" runat="server" CssClass="dropdownlist" Width="86%"
                                                                                            TabIndex="1">
                                                                                        </asp:DropDownList></td>
                                                                                    <td valign="top">
                                                                                        Joint call remarks</td>
                                                                                    <td valign="top">
                                                                                        <asp:TextBox ID="TxtJointByCall" runat="server" CssClass="textbox" Height="40px"
                                                                                            Rows="4" TabIndex="4" TextMode="MultiLine" Width="84%"></asp:TextBox></td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4" style="width: 100%;">
                                                                                        <asp:GridView ID="gvVisitDetails" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                            Width="96%">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                                                <asp:BoundField DataField="MANAGER_NAME" HeaderText="Manager" />
                                                                                                <asp:BoundField DataField="IMMEDIATE_MANAGERNAME" HeaderText="Reporting Manager" />
                                                                                                <asp:BoundField DataField="CONTACT_NAME" HeaderText="Person Met" />
                                                                                                <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" />
                                                                                                <asp:BoundField DataField="INTIME" HeaderText="In Time" />
                                                                                                <asp:BoundField DataField="OUTTIME" HeaderText="Out Time" />
                                                                                                <asp:TemplateField HeaderText="Action">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                            OnClientClick="javascript:return CancelEditVisitDetails();" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") %>'></asp:LinkButton>&nbsp;
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                            <RowStyle CssClass="textbold" />
                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Visit Sub Type
                                                                                    </td>
                                                                                    <td style="width: 50%" colspan="2">
                                                                                        <asp:CheckBox ID="chkServiceCall" runat="server" Text="Service Call" AutoPostBack="true"
                                                                                            onclick="HideShowServiceStrategicCall();" TabIndex="3" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                        <asp:CheckBox ID="chkStrategicCall" runat="server" Text="Strategic Call" AutoPostBack="true"
                                                                                            onclick="HideShowServiceStrategicCall();" TabIndex="3" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="hidden" id="hdVisitDetails" runat="server" style="width: 1px" />
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100%" colspan="5">
                                                                                        <ajax:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlProgressVisitDetails"
                                                                                            TargetControlID="pnlProgressVisitDetails" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                            ID="modVisitDetails" runat="server">
                                                                                        </ajax:ModalPopupExtender>
                                                                                        <asp:Panel ID="pnlProgressVisitDetails" runat="server" CssClass="overPanel_Test"
                                                                                            Height="0px" Width="150px" BackColor="white">
                                                                                            <table style="width: 150px; height: 150px;">
                                                                                                <tr>
                                                                                                    <td valign="middle" align="center">
                                                                                                        <img src="../Images/er.gif" id="img" runat="server" alt="" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="top" style="width: 100%;">
                                                                            <asp:Panel ID="pnlServiceCallCollapseManage" runat="server">
                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr class="heading">
                                                                                        <td style="width: 80%; padding-left: 5px; height: 20px;">
                                                                                            Service Call
                                                                                        </td>
                                                                                        <td style="width: 20%; padding-right: 5px; height: 20px;" class="right">
                                                                                            <asp:Image ImageUrl="~/Images/hide_arrow.gif" ID="imgServiceCall" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="pnlServiceCall" runat="server">
                                                                                <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                    <tr>
                                                                                        <td style="width: 15%;">
                                                                                        </td>
                                                                                        <td style="width: 25%;">
                                                                                        </td>
                                                                                        <td style="width: 20%;">
                                                                                        </td>
                                                                                        <td style="width: 28%;">
                                                                                        </td>
                                                                                        <td style="width: 12%;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5" style="width: 100%;">
                                                                                            <asp:Label ID="lblErrorServiceCall" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr class="top">
                                                                                        <td>
                                                                                            Department<span class="Mandatory">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="dropdownlist" Width="87%"
                                                                                                AutoPostBack="true" TabIndex="4">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            Deptt Specific
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtDepttSpecific" runat="server" Width="84%" TabIndex="4"></asp:TextBox>
                                                                                            <asp:DropDownList ID="ddlDepttSpecific" runat="server" CssClass="dropdownlist" Width="86%"
                                                                                                TabIndex="4">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="center">
                                                                                            <asp:Button ID="btnAddServiceCall" Text="Add" runat="Server" CssClass="button" Width="110px"
                                                                                                OnClientClick="javascript:return ValidateServiceCall();" TabIndex="5" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr class="top">
                                                                                        <td style="vertical-align: top;">
                                                                                            Detailed Disc /<br />
                                                                                            Issue Reported<span class="Mandatory">*</span>
                                                                                        </td>
                                                                                        <td style="width: 80%" colspan="3">
                                                                                            <asp:TextBox ID="txtDetailedDiscussion" runat="server" CssClass="textbox" Width="94%"
                                                                                                TextMode="MultiLine" Rows="4" Height="40px" TabIndex="4"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="center" style="vertical-align: top;" rowspan="2">
                                                                                            <asp:Button ID="btnCancelServiceCall" Text="Cancel" runat="Server" CssClass="button"
                                                                                                Width="110px" OnClientClick="javascript:return CancelEditServiceCall();" TabIndex="5" /><br />
                                                                                            <br />
                                                                                            <input type="button" onclick="javascript:OpenVisitedItems();" style="width: 110px;"
                                                                                                id="Button1" value="Prev Visit Items" runat="server" class="button topMargin"
                                                                                                tabindex="5" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr class="top">
                                                                                        <td>
                                                                                            Status<span class="Mandatory">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlServiceStatus" runat="server" CssClass="dropdownlist" Width="87%"
                                                                                                TabIndex="4">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            Assigned to
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="dlstAssignedTo" runat="server" CssClass="dropdownlist" Width="87%">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Closure Date<span class="Mandatory">*</span>
                                                                                        </td>
                                                                                        <td nowrap="nowrap">
                                                                                            <asp:TextBox ID="txtCloserDate" runat="server" CssClass="textboxgrey" Width="84%"
                                                                                                TabIndex="4" ReadOnly="true"></asp:TextBox><img id="imgCloserDate" alt="" src="../Images/calender.gif"
                                                                                                    style="cursor: pointer; display: none;" runat="server" title="Date selector"
                                                                                                    onclick="ShowCalender('txtCloserDate','imgCloserDate');" tabindex="4" />

                                                                                            <script type="text/javascript">
                                                                                                Calendar.setup({
                                                                                                inputField     :    '<%=txtCloserDate.ClientId%>',
                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                button         :    "imgCloserDate",
                                                                                                //align          :    "Tl",
                                                                                                singleClick    :    true
                                                                                                });
                                                                                            </script>

                                                                                        </td>
                                                                                        <td>
                                                                                            Target Closure Date<span class="Mandatory">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtTargetCloserDate" runat="server" CssClass="textbox" Width="84%"
                                                                                                TabIndex="4"></asp:TextBox>
                                                                                            <img id="imgTargetCloserDate" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                                                runat="server" title="Date selector" onclick="ShowCalender('txtTargetCloserDate','imgTargetCloserDate');"
                                                                                                tabindex="4" />

                                                                                            <script type="text/javascript">
                                                                                                Calendar.setup({
                                                                                                inputField     :    '<%=txtTargetCloserDate.ClientId%>',
                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                button         :    "imgTargetCloserDate",
                                                                                                //align          :    "Tl",
                                                                                                singleClick    :    true
                                                                                                });
                                                                                            </script>

                                                                                        </td>
                                                                                        <td class="center" style="vertical-align: top;">
                                                                                            <input type="button" onclick="javascript:ShowSCMarketInfo('SC');" style="width: 110px;"
                                                                                                id="BtnSCMarketInfo" value="Comp/Mkt info " runat="server" class="button" tabindex="5" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td style="width: 80%" colspan="3">
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td style="width: 71%" colspan="3">
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" style="width: 100%;">
                                                                                            <asp:GridView ID="gvServiceCall" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                Width="96%">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" HeaderStyle-Width="3%" />
                                                                                                    <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Deptt" HeaderStyle-Width="8%" />
                                                                                                    <asp:BoundField DataField="DEPARTMENT_SPECIFIC" HeaderText="Deptt Specific" HeaderStyle-Width="10%" />
                                                                                                    <asp:TemplateField HeaderText="Detailed Disc /Issue Reported" HeaderStyle-Width="22%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:TextBox ID="txtSCIssueReported" runat="server" CssClass="textbox" Height="30px"
                                                                                                                BorderStyle="none" TextMode="MultiLine" Wrap="true" Width="100%" ReadOnly="True"
                                                                                                                BorderColor="white" BorderWidth="0px" Text='<%#Eval("SC_DISCUSSIONISSUE_REMARKS") %>'> </asp:TextBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="SC_STATUSID_NAME" HeaderText="Status" HeaderStyle-Width="8%" />
                                                                                                    <asp:BoundField DataField="ASSIGNTO_EMPLOYEE_NAME" HeaderText="Assigned to" HeaderStyle-Width="10%" />
                                                                                                    <asp:BoundField DataField="CLOSER_DATETIME" HeaderText="Closure Date" HeaderStyle-Width="8%" />
                                                                                                    <asp:BoundField DataField="TARGET_CLOSER_DATETIME" HeaderText="Target Closure Date"
                                                                                                        HeaderStyle-Width="10%" />
                                                                                                    <asp:BoundField DataField="LOGDATE" HeaderText="Logged Date" HeaderStyle-Width="7%" />
                                                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="110px" ItemStyle-Wrap="false"
                                                                                                        ItemStyle-Width="110px">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HiddenField ID="HdSC_STATUSID" runat="server" Value='<%# Eval("SC_STATUSID") %>' />
                                                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                                OnClientClick="javascript:return CancelEditServiceCall();" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO")  %>'></asp:LinkButton>&nbsp;&nbsp;
                                                                                                            <asp:LinkButton ID="LnkSCFRem" runat="server" CommandName="FupRemX" Text="Followup"
                                                                                                                CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_SC_DETAIL_ID")  %>'></asp:LinkButton>&nbsp;
                                                                                                            <asp:LinkButton ID="LnkSCDel" runat="server" CommandName="SCDelX" Text="Delete" CssClass="LinkButtons"
                                                                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") %>'></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" Wrap="true" />
                                                                                                <RowStyle CssClass="textbold" Wrap="true" />
                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="white" Wrap="true" />
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                        <td>
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5" style="width: 100%;">
                                                                                            <input type="hidden" runat="server" id="hdServiceCall" style="width: 1px" />
                                                                                            <input type="hidden" runat="server" id="hdAssingedTo" style="width: 1px" />
                                                                                            <input type="hidden" runat="server" id="hdDSR_SC_DETAIL_ID" style="width: 1px" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%" colspan="5">
                                                                                            <ajax:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlProgressServiceCall"
                                                                                                TargetControlID="pnlProgressServiceCall" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                                ID="modServiceCall" runat="server">
                                                                                            </ajax:ModalPopupExtender>
                                                                                            <asp:Panel ID="pnlProgressServiceCall" runat="server" CssClass="overPanel_Test" Height="0px"
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
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <ajax:CollapsiblePanelExtender ID="collapsePanelServiceCall" TargetControlID="pnlServiceCall"
                                                                                Collapsed="false" ExpandControlID="pnlServiceCallCollapseManage" SuppressPostBack="true"
                                                                                CollapseControlID="pnlServiceCallCollapseManage" CollapsedImage="../Images/hide_arrow.gif"
                                                                                ExpandedImage="../Images/show_arrow.gif" runat="Server" ImageControlID="imgServiceCall" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="top" style="width: 100%;">
                                                                            <asp:Panel ID="pnlStrategicVisits" runat="server" Width="100%">
                                                                                <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                    <tr>
                                                                                        <td style="width: 15%;">
                                                                                        </td>
                                                                                        <td style="width: 15%;">
                                                                                        </td>
                                                                                        <td style="width: 15%;">
                                                                                        </td>
                                                                                        <td style="width: 55%;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" style="width: 100%;" class="heading">
                                                                                            Strategic Visits
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" style="width: 100%;">
                                                                                            <asp:Label ID="lblStrategicVisits" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr class="top">
                                                                                        <td>
                                                                                            Strategic Visits<span class="Mandatory">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="rbRetention" runat="server" Text="Retention" GroupName="GrpStrategicVisits"
                                                                                                AutoPostBack="true" TabIndex="6" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="rbTarget" runat="server" Text="Target" GroupName="GrpStrategicVisits"
                                                                                                AutoPostBack="true" TabIndex="6" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="rbNone" runat="server" Text="None" GroupName="GrpStrategicVisits"
                                                                                                AutoPostBack="true" TabIndex="6" /></td>
                                                                                    </tr>
                                                                                    <tr class="top">
                                                                                        <td>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <asp:CheckBox ID="chkAirNonAir" runat="server" Text="Air & Non Air Product & Others"
                                                                                                AutoPostBack="true" onclick="HideShowStrategicVisits();" TabIndex="6" /></td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%" colspan="4">
                                                                                            <ajax:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlProgressStrategicVisits"
                                                                                                TargetControlID="pnlProgressStrategicVisits" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                                ID="modStrategicVisits" runat="server">
                                                                                            </ajax:ModalPopupExtender>
                                                                                            <asp:Panel ID="pnlProgressStrategicVisits" runat="server" CssClass="overPanel_Test"
                                                                                                Height="0px" Width="150px" BackColor="white">
                                                                                                <table style="width: 150px; height: 150px;">
                                                                                                    <tr>
                                                                                                        <td valign="middle" align="center">
                                                                                                            <img src="../Images/er.gif" id="img5" runat="server" alt="" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                                                    <tr class="top">
                                                                                        <td style="width: 100%;">
                                                                                            <asp:Panel ID="pnlRetentionCollapseManage" runat="server">
                                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                    <tr class="heading">
                                                                                                        <td style="width: 80%; padding-left: 5px">
                                                                                                            Retention
                                                                                                        </td>
                                                                                                        <td style="width: 20%; padding-right: 5px" class="right">
                                                                                                            <asp:Image ImageUrl="~/Images/hide_arrow.gif" ID="imgRetention" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="pnlRetention" runat="server" Width="100%">
                                                                                                <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                                    <tr>
                                                                                                        <td style="width: 15%;">
                                                                                                        </td>
                                                                                                        <td style="width: 25%;">
                                                                                                        </td>
                                                                                                        <td style="width: 20%;">
                                                                                                        </td>
                                                                                                        <td style="width: 28%;">
                                                                                                        </td>
                                                                                                        <td style="width: 12%;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="5" style="width: 100%;">
                                                                                                            <asp:Label ID="lblErrRetention" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="top">
                                                                                                        <td>
                                                                                                            Existing&nbsp;Deal<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="lnkExistingDeals" runat="server" CssClass="LinkButtonsWithoutUnderline"
                                                                                                                OnClientClick="return DetailsFunction()"></asp:LinkButton>
                                                                                                            <input type="hidden" id="hdBCID" runat='server' style="width: 1px" />
                                                                                                            <input type="hidden" id="hdChainCode" runat='server' style="width: 1px" />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            CPS <span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtCPS" runat="server" CssClass="textbox" Width="84%" TabIndex="7"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td class="center">
                                                                                                            <asp:Button ID="btnAddRetention" Text="Add" runat="Server" CssClass="button" Width="110px"
                                                                                                                OnClientClick="javascript:return ValidateRetention();" TabIndex="8" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="top">
                                                                                                        <td>
                                                                                                            Retention Reason <span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlRetentionReason" runat="server" CssClass="dropdownlist"
                                                                                                                Width="87%" TabIndex="7">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Retention Status <span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlRetentionStatus" runat="server" CssClass="dropdownlist"
                                                                                                                Width="87%" TabIndex="7">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td class="center">
                                                                                                            <asp:Button ID="btnCancelRetention" Text="Cancel" runat="Server" CssClass="button"
                                                                                                                Width="110px" OnClientClick="javascript:return CancelEditRetention();" TabIndex="8" /></td>
                                                                                                    </tr>
                                                                                                    <tr class="top">
                                                                                                        <td>
                                                                                                            New CPS<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtNewCPS" runat="server" CssClass="textbox" Width="84%" TabIndex="7"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            1A Approved
                                                                                                            <br />
                                                                                                            New Deal<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txt1AApprovedNewDeal" runat="server" CssClass="textbox" Width="84%"
                                                                                                                TabIndex="7" MaxLength="500"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td class="center" valign="top">
                                                                                                            <input id="BtnRetMarketInfo" runat="server" class="button topMargin" onclick="javascript:ShowRetMarketInfo('Retention');"
                                                                                                                style="width: 110px" tabindex="5" type="button" value="Comp/Mkt info " /></td>
                                                                                                    </tr>
                                                                                                    <tr class="Top">
                                                                                                        <td>
                                                                                                            Signed On /<br />
                                                                                                            Conversion On (date)
                                                                                                        </td>
                                                                                                        <td class="Top">
                                                                                                            <table width="97%" cellpadding="0" cellspacing="0" border="0">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtRetentionSignedOn" runat="server" CssClass="textboxgrey" Width="94%"
                                                                                                                            ReadOnly="true" TabIndex="7"></asp:TextBox></td>
                                                                                                                    <td style="width: 6%">
                                                                                                                        <img id="imgRetentionSignedOn" alt="" src="../Images/calender.gif" style="cursor: pointer;
                                                                                                                            display: none;" runat="server" title="Date selector" onclick="ShowCal();" tabindex="7" /></td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Target- Segs / % of Business<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td class="Top">
                                                                                                            <asp:TextBox ID="txtRetentionTargetSegs" runat="server" CssClass="textbox" Width="84%"
                                                                                                                TabIndex="7" MaxLength="500"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td valign="top" class="center">
                                                                                                            <input id="BtnRetIssueReported" runat="server" class="button topMargin" onclick="javascript:ShowRetIssueReported('Retention');"
                                                                                                                style="width: 110px" tabindex="5" type="button" value="Discussed items" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="Top">
                                                                                                        <td>
                                                                                                            Department<span class="Mandatory">*</span></td>
                                                                                                        <td class="Top">
                                                                                                            <asp:DropDownList ID="DrpRetDept" runat="server" CssClass="dropdownlist" Width="87%"
                                                                                                                AutoPostBack="true" TabIndex="7">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td class="Top">
                                                                                                            Assigned To<span class="Mandatory">*</span></td>
                                                                                                        <td class="Top">
                                                                                                            <asp:DropDownList ID="DrpRetAssignedTo" TabIndex="7" runat="server" CssClass="dropdownlist"
                                                                                                                Width="87%">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td class="center" valign="top">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            Closure Date<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td nowrap="nowrap">
                                                                                                            <asp:TextBox ID="txtRetCloserDate" runat="server" CssClass="textboxgrey" Width="84%"
                                                                                                                TabIndex="7" ReadOnly="true"></asp:TextBox><img id="imgRetCloserDate" alt="" src="../Images/calender.gif"
                                                                                                                    style="cursor: pointer; display: none;" runat="server" title="Date selector"
                                                                                                                    onclick="ShowCalender('txtRetCloserDate','imgRetCloserDate');" tabindex="7" />

                                                                                                            <script type="text/javascript">
                                                                                                Calendar.setup({
                                                                                                inputField     :    '<%=txtRetCloserDate.ClientId%>',
                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                button         :    "imgRetCloserDate",
                                                                                                //align          :    "Tl",
                                                                                                singleClick    :    true
                                                                                                });
                                                                                                            </script>

                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Target Closure Date<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtRetTargetCloserDate" runat="server" CssClass="textbox" Width="84%"
                                                                                                                TabIndex="7"></asp:TextBox>
                                                                                                            <img id="imgRetTargetCloserDate" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                                                                runat="server" title="Date selector" onclick="ShowCalender('txtRetTargetCloserDate','imgRetTargetCloserDate');"
                                                                                                                tabindex="7" />

                                                                                                            <script type="text/javascript">
                                                                                                Calendar.setup({
                                                                                                inputField     :    '<%=txtRetTargetCloserDate.ClientId%>',
                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                button         :    "imgRetTargetCloserDate",
                                                                                                //align          :    "Tl",
                                                                                                singleClick    :    true
                                                                                                });
                                                                                                            </script>

                                                                                                        </td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="Top" style="display: none; empty-cells: hide;">
                                                                                                        <td>
                                                                                                            Follow-ups</td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                            <asp:TextBox ID="TxtRetFollowup" runat="server" CssClass="textbox" Height="40px"
                                                                                                                Rows="4" TabIndex="7" TextMode="MultiLine" Width="94%"></asp:TextBox></td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="Top">
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                        </td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4">
                                                                                                            <asp:CheckBox ID="ChkRetShowAll" runat="server" Text="Show All" TextAlign="Right"
                                                                                                                AutoPostBack="true" Visible="false" /></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4" style="width: 100%;">
                                                                                                            <table style="width: 100%;">
                                                                                                                <tr>
                                                                                                                    <td style="width: 100%;">
                                                                                                                        <asp:GridView ID="gvRetention" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                            Width="96%">
                                                                                                                            <Columns>
                                                                                                                                <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                                                                                <asp:BoundField DataField="CPS" HeaderText="CPS" />
                                                                                                                                <asp:BoundField DataField="SVR_REASON_NAME" HeaderText="Retention Reason" />
                                                                                                                                <asp:BoundField DataField="SVR_STATUS_NAME" HeaderText="Status/ Retention Phase" />
                                                                                                                                <asp:BoundField DataField="A1APPROVED_NEW_DEAL" HeaderText="1A Approved New Deal"
                                                                                                                                    HeaderStyle-Wrap="true" />
                                                                                                                                <asp:BoundField DataField="NEWCPS" HeaderText="New CPS" />
                                                                                                                                <asp:BoundField DataField="STR_SIGNON_DATE" HeaderText="Signed On/Conv On" HeaderStyle-Wrap="true" />
                                                                                                                                <asp:BoundField DataField="STR_TARGET_SEG" HeaderText="Target- Segs/% of Business"
                                                                                                                                    HeaderStyle-Wrap="true" />
                                                                                                                                <%-- <asp:BoundField DataField="ASSIGNTO_EMPLOYEE_NAME" HeaderText="Assigned to" HeaderStyle-Width="10%" />--%>
                                                                                                                                <asp:BoundField DataField="CLOSER_DATETIME" HeaderText="Closure Date" HeaderStyle-Width="8%" />
                                                                                                                                <asp:BoundField DataField="TARGET_CLOSER_DATETIME" HeaderText="Target Closure Date"
                                                                                                                                    HeaderStyle-Width="10%" />
                                                                                                                                <asp:BoundField DataField="LOGDATE" HeaderText="Logged Date" HeaderStyle-Width="7%" />
                                                                                                                                <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:HiddenField ID="HdSVR_STATUSID" runat="server" Value='<%# Eval("SVR_STATUSID") %>' />
                                                                                                                                        <asp:HiddenField ID="HdDSR_STR_DETAIL_ID" runat="server" Value='<%# Eval("DSR_STR_DETAIL_ID") %>' />
                                                                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                                                            OnClientClick="javascript:return CancelEditRetention();" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") %>'></asp:LinkButton>&nbsp;
                                                                                                                                        <asp:LinkButton ID="LnkRetDel" runat="server" CommandName="RetDelX" Text="Delete"
                                                                                                                                            CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") %>'></asp:LinkButton>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                            </Columns>
                                                                                                                            <AlternatingRowStyle CssClass="lightblue" Wrap="true" />
                                                                                                                            <RowStyle CssClass="textbold" Wrap="true" />
                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" Wrap="true" Height="20px" />
                                                                                                                        </asp:GridView>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="5" style="width: 100%;">
                                                                                                            <input type="hidden" runat="server" id="hdRetention" style="width: 1px" />
                                                                                                            <input type="hidden" runat="server" id="hdRetentionSavedData" style="width: 1px" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 100%" colspan="5">
                                                                                                            <ajax:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlProgressRetention"
                                                                                                                TargetControlID="pnlProgressRetention" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                                                ID="modRetention" runat="server">
                                                                                                            </ajax:ModalPopupExtender>
                                                                                                            <asp:Panel ID="pnlProgressRetention" runat="server" CssClass="overPanel_Test" Height="0px"
                                                                                                                Width="150px" BackColor="white">
                                                                                                                <table style="width: 150px; height: 150px;">
                                                                                                                    <tr>
                                                                                                                        <td valign="middle" align="center">
                                                                                                                            <img src="../Images/er.gif" id="img2" runat="server" alt="" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <ajax:CollapsiblePanelExtender ID="CollapsiblePanelRetention" TargetControlID="pnlRetention"
                                                                                                Collapsed="false" ExpandControlID="pnlRetentionCollapseManage" SuppressPostBack="true"
                                                                                                CollapseControlID="pnlRetentionCollapseManage" CollapsedImage="../Images/hide_arrow.gif"
                                                                                                ExpandedImage="../Images/show_arrow.gif" runat="Server" ImageControlID="imgRetention" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr class="top">
                                                                                        <td style="width: 100%;">
                                                                                            <asp:Panel ID="pnlTargetCollapseManage" runat="server">
                                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                    <tr class="heading">
                                                                                                        <td style="width: 80%; padding-left: 5px">
                                                                                                            Target
                                                                                                        </td>
                                                                                                        <td style="width: 20%; padding-right: 5px" class="right">
                                                                                                            <asp:Image ImageUrl="~/Images/hide_arrow.gif" ID="imgTarget" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="pnlTarget" runat="server">
                                                                                                <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                                    <tr>
                                                                                                        <td style="width: 17%;">
                                                                                                        </td>
                                                                                                        <td style="width: 27%;">
                                                                                                        </td>
                                                                                                        <td style="width: 17%;">
                                                                                                        </td>
                                                                                                        <td style="width: 27%;">
                                                                                                        </td>
                                                                                                        <td style="width: 12%;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="6" style="width: 100%;">
                                                                                                            <asp:Label ID="lblErrTarget" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="top">
                                                                                                        <td>
                                                                                                            1A Approved New Deal<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtTarget1AApprovedNewDeal" runat="server" CssClass="textbox" Width="80%"
                                                                                                                TabIndex="9" MaxLength="500"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            CPS <span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtTargetCPS" runat="server" CssClass="textbox" Width="81%" TabIndex="9"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td class="center">
                                                                                                            <asp:Button ID="btnTargetAdd" Text="Add" runat="Server" CssClass="button" Width="110px"
                                                                                                                OnClientClick="javascript:return ValidateTarget();" TabIndex="10" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="top">
                                                                                                        <td>
                                                                                                            Status<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlTargetStatus" runat="server" CssClass="dropdownlist" Width="83%"
                                                                                                                TabIndex="9">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td class="center">
                                                                                                            <asp:Button ID="btnbtnTargetCancel" Text="Cancel" runat="Server" CssClass="button"
                                                                                                                Width="110px" OnClientClick="javascript:return CancelEditTarget();" TabIndex="10" /></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td valign="top">
                                                                                                            Signed On /</br> Conversion On (date)
                                                                                                        </td>
                                                                                                        <td valign="top" align="left">
                                                                                                            <table width="98%" cellpadding="0" cellspacing="0" border="0">
                                                                                                                <tr>
                                                                                                                    <td valign="top" align="left">
                                                                                                                        <asp:TextBox ID="txtTargetSignedOn" runat="server" CssClass="textboxgrey" Width="88%"
                                                                                                                            ReadOnly="true" TabIndex="9"></asp:TextBox></td>
                                                                                                                    <td valign="top" style="width: 7%">
                                                                                                                        <img id="imgTargetSignedOn" alt="" src="../Images/calender.gif" style="cursor: pointer;
                                                                                                                            display: none;" runat="Server" title="Date selector" onclick="ShowCalender('txtTargetSignedOn','imgTargetSignedOn');"
                                                                                                                            tabindex="9" />

                                                                                                                        <script type="text/javascript">
                                                                                                                Calendar.setup({
                                                                                                                inputField     :    '<%=txtTargetSignedOn.ClientId%>',
                                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                                button         :    "imgTargetSignedOn",
                                                                                                                //align          :    "Tl",
                                                                                                                singleClick    :    true
                                                                                                                });
                                                                                                                        </script>

                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td valign="top">
                                                                                                            Target- Segs /<br />
                                                                                                            % of Business<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td valign="top">
                                                                                                            <asp:TextBox ID="txtTargetTargetSegs" runat="server" CssClass="textbox" Width="81%"
                                                                                                                TabIndex="9" MaxLength="500"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                            <input id="BtnTargetMarketInfo" runat="server" class="button topMargin" onclick="javascript:ShowTarMarketInfo('Target');"
                                                                                                                style="width: 110px" tabindex="5" type="button" value="Comp/Mkt info " />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td valign="top">
                                                                                                            Department<span class="Mandatory">*</span></td>
                                                                                                        <td valign="top">
                                                                                                            <asp:DropDownList ID="DrpTargetDept" runat="server" CssClass="dropdownlist" Width="83%"
                                                                                                                AutoPostBack="true" TabIndex="9">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td valign="top">
                                                                                                            Assigned To<span class="Mandatory">*</span></td>
                                                                                                        <td valign="top">
                                                                                                            <asp:DropDownList ID="DrpTargetAssignedTo" TabIndex="9" runat="server" CssClass="dropdownlist"
                                                                                                                Width="83%">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td class="center" style="vertical-align: top">
                                                                                                            <input id="BtnTargetIssueReported" runat="server" class="button topMargin" onclick="javascript:ShowTarIssueReported('Target');"
                                                                                                                style="width: 110px" tabindex="5" type="button" value="Discussed items" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            Closure Date<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td nowrap="nowrap">
                                                                                                            <asp:TextBox ID="txtTarCloserDate" runat="server" CssClass="textboxgrey" Width="81%"
                                                                                                                TabIndex="5" ReadOnly="true"></asp:TextBox><img id="imgTarCloserDate" alt="" src="../Images/calender.gif"
                                                                                                                    style="cursor: pointer; display: none;" runat="server" title="Date selector"
                                                                                                                    onclick="ShowCalender('txtTarCloserDate','imgTarCloserDate');" tabindex="5" />

                                                                                                            <script type="text/javascript">
                                                                                                Calendar.setup({
                                                                                                inputField     :    '<%=txtTarCloserDate.ClientId%>',
                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                button         :    "imgTarCloserDate",
                                                                                                //align          :    "Tl",
                                                                                                singleClick    :    true
                                                                                                });
                                                                                                            </script>

                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Target Closure Date<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtTarTargetCloserDate" runat="server" CssClass="textbox" Width="81%"
                                                                                                                TabIndex="5"></asp:TextBox>
                                                                                                            <img id="imgTarTargetCloserDate" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                                                                runat="server" title="Date selector" onclick="ShowCalender('txtTarTargetCloserDate','imgTarTargetCloserDate');"
                                                                                                                tabindex="5" />

                                                                                                            <script type="text/javascript">
                                                                                                Calendar.setup({
                                                                                                inputField     :    '<%=txtTarTargetCloserDate.ClientId%>',
                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                button         :    "imgTarTargetCloserDate",
                                                                                                //align          :    "Tl",
                                                                                                singleClick    :    true
                                                                                                });
                                                                                                            </script>

                                                                                                        </td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="Top" style="display: none; empty-cells: hide;">
                                                                                                        <td>
                                                                                                            Follow-ups</td>
                                                                                                        <td colspan="3" style="width: 71%">
                                                                                                            <asp:TextBox ID="TxtTarFollowup" runat="server" CssClass="textbox" Height="40px"
                                                                                                                Rows="4" TabIndex="9" TextMode="MultiLine" Width="93%"></asp:TextBox></td>
                                                                                                        <td class="center">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="Top">
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td colspan="3" style="width: 71%">
                                                                                                        </td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4">
                                                                                                            <asp:CheckBox ID="ChkTargetShowAll" runat="server" Text="Show All" TextAlign="Right"
                                                                                                                AutoPostBack="true" Visible="false" /></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4" style="width: 100%;">
                                                                                                            <asp:GridView ID="gvTarget" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                Width="96%">
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                                                                    <asp:BoundField DataField="A1APPROVED_NEW_DEAL" HeaderText="1A Approved New Deal"
                                                                                                                        HeaderStyle-Wrap="true" />
                                                                                                                    <asp:BoundField DataField="CPS" HeaderText="CPS" />
                                                                                                                    <asp:BoundField DataField="SVT_STATUS_NAME" HeaderText="Status/ Target Phase" />
                                                                                                                    <asp:BoundField DataField="STT_SIGNON_DATE" HeaderText="Signed On/Conversion On"
                                                                                                                        HeaderStyle-Wrap="true" />
                                                                                                                    <asp:BoundField DataField="STT_TARGET_SEG" HeaderText="Target- Segs/% of Business"
                                                                                                                        HeaderStyle-Wrap="true" />
                                                                                                                    <%-- <asp:BoundField DataField="ASSIGNTO_EMPLOYEE_NAME" HeaderText="Assigned to" HeaderStyle-Width="10%" />--%>
                                                                                                                    <asp:BoundField DataField="CLOSER_DATETIME" HeaderText="Closure Date" HeaderStyle-Width="8%" />
                                                                                                                    <asp:BoundField DataField="TARGET_CLOSER_DATETIME" HeaderText="Target Closure Date"
                                                                                                                        HeaderStyle-Width="10%" />
                                                                                                                    <asp:BoundField DataField="LOGDATE" HeaderText="Logged Date" HeaderStyle-Width="7%" />
                                                                                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:HiddenField ID="HdSVT_STATUSID" runat="server" Value='<%# Eval("SVT_STATUSID") %>' />
                                                                                                                            <asp:HiddenField ID="DSR_STT_DETAIL_ID" runat="server" Value='<%# Eval("DSR_STT_DETAIL_ID") %>' />
                                                                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                                                OnClientClick="javascript:return CancelEditTarget();" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") %>'></asp:LinkButton>&nbsp;
                                                                                                                            <asp:LinkButton ID="LnkTarDel" runat="server" CommandName="TarDelX" Text="Delete"
                                                                                                                                CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") %>'></asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                <RowStyle CssClass="textbold" />
                                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                                            </asp:GridView>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="5" style="width: 100%;">
                                                                                                            <input type="hidden" runat="server" id="hdTarget" style="width: 1px" />
                                                                                                            <input type="hidden" runat="server" id="hdTargetSaveData" style="width: 1px" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 100%" colspan="5">
                                                                                                            <ajax:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlProgressTarget"
                                                                                                                TargetControlID="pnlProgressTarget" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                                                ID="modTarget" runat="server">
                                                                                                            </ajax:ModalPopupExtender>
                                                                                                            <asp:Panel ID="pnlProgressTarget" runat="server" CssClass="overPanel_Test" Height="0px"
                                                                                                                Width="150px" BackColor="white">
                                                                                                                <table style="width: 150px; height: 150px;">
                                                                                                                    <tr>
                                                                                                                        <td valign="middle" align="center">
                                                                                                                            <img src="../Images/er.gif" id="img3" runat="server" alt="" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <ajax:CollapsiblePanelExtender ID="CollapsiblePanelTarget" TargetControlID="pnlTarget"
                                                                                                Collapsed="false" ExpandControlID="pnlTargetCollapseManage" SuppressPostBack="true"
                                                                                                CollapseControlID="pnlTargetCollapseManage" CollapsedImage="../Images/hide_arrow.gif"
                                                                                                ExpandedImage="../Images/show_arrow.gif" runat="Server" ImageControlID="imgTarget" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr class="top">
                                                                                        <td style="width: 100%;">
                                                                                            <asp:Panel ID="pnlAirNonAirCollapseManage" runat="server">
                                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                    <tr class="heading">
                                                                                                        <td style="width: 80%; padding-left: 5px">
                                                                                                            Air & Non Air Product & Others
                                                                                                        </td>
                                                                                                        <td style="width: 20%; padding-right: 5px" class="right">
                                                                                                            <asp:Image ImageUrl="~/Images/hide_arrow.gif" ID="imgAirNonAir" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="pnlAirNonAir" runat="server">
                                                                                                <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                                    <tr>
                                                                                                        <td style="width: 17%;">
                                                                                                        </td>
                                                                                                        <td style="width: 27%;">
                                                                                                        </td>
                                                                                                        <td style="width: 17%;">
                                                                                                        </td>
                                                                                                        <td style="width: 27%;">
                                                                                                        </td>
                                                                                                        <td style="width: 12%;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="5" style="width: 100%;">
                                                                                                            <asp:Label ID="lblErrAirNonAirProduct" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="top">
                                                                                                        <td>
                                                                                                            Product Name<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlProductName" runat="server" CssClass="dropdownlist" Width="83%"
                                                                                                                TabIndex="11">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Revenue <span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtRevenue" runat="server" CssClass="textbox" Width="80%" TabIndex="11"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td class="center">
                                                                                                            <asp:Button ID="btnAirNonAirAdd" Text="Add" runat="Server" CssClass="button" Width="110px"
                                                                                                                OnClientClick="javascript:return ValidateAirNonAir();" TabIndex="12" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="top">
                                                                                                        <td>
                                                                                                            Status<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlAirNonAirStatus" runat="server" CssClass="dropdownlist"
                                                                                                                Width="83%" TabIndex="11">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Instalation Date
                                                                                                        </td>
                                                                                                        <td valign="top">
                                                                                                            <table width="84%">
                                                                                                                <tr>
                                                                                                                    <td valign="top">
                                                                                                                        <asp:TextBox ID="txtAirNonAirSignedOn" runat="server" CssClass="textboxgrey" Width="100%"
                                                                                                                            ReadOnly="true" TabIndex="11"></asp:TextBox></td>
                                                                                                                    <td valign="top">
                                                                                                                        <img id="imgAirNonAirSignedOn" alt="" src="../Images/calender.gif" style="cursor: pointer;
                                                                                                                            display: none;" runat="server" title="Date selector" onclick="ShowCalender('txtAirNonAirSignedOn','imgAirNonAirSignedOn');"
                                                                                                                            tabindex="11" />

                                                                                                                        <script type="text/javascript">
                                                                                                                Calendar.setup({
                                                                                                                inputField     :    '<%=txtAirNonAirSignedOn.ClientId%>',
                                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                                button         :    "imgAirNonAirSignedOn",
                                                                                                                //align          :    "Tl",
                                                                                                                singleClick    :    true
                                                                                                                });
                                                                                                                        </script>

                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td class="center">
                                                                                                            <asp:Button ID="btnAirNonAirCancel" Text="Cancel" runat="Server" CssClass="button"
                                                                                                                Width="110px" OnClientClick="javascript:return CancelEditAirNonAir();" TabIndex="12" /></td>
                                                                                                    </tr>
                                                                                                    <tr class="top">
                                                                                                        <td>
                                                                                                            Department<span class="Mandatory">*</span></td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="DrpAirNonAirDept" runat="server" CssClass="dropdownlist" Width="83%"
                                                                                                                AutoPostBack="true" TabIndex="11">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td class="Top">
                                                                                                            Assigned To<span class="Mandatory">*</span></td>
                                                                                                        <td valign="top">
                                                                                                            <asp:DropDownList ID="DrpAirNonAirAssignedTo" runat="server" CssClass="dropdownlist"
                                                                                                                Width="83%" TabIndex="11">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td class="center">
                                                                                                            <input id="BtnAirNonAirMarketInfo" runat="server" class="button topMargin" onclick="javascript:ShowAirNonAirMarketInfo('AirNonAir');"
                                                                                                                style="width: 110px" tabindex="11" type="button" value="Comp/Mkt info " /></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            Closure Date<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td nowrap="nowrap">
                                                                                                            <asp:TextBox ID="txtAirCloserDate" runat="server" CssClass="textboxgrey" Width="80%"
                                                                                                                TabIndex="11" ReadOnly="true"></asp:TextBox><img id="imgAirCloserDate" alt="" src="../Images/calender.gif"
                                                                                                                    style="cursor: pointer; display: none;" runat="server" title="Date selector"
                                                                                                                    onclick="ShowCalender('txtAirCloserDate','imgAirCloserDate');" tabindex="11" />

                                                                                                            <script type="text/javascript">
                                                                                                Calendar.setup({
                                                                                                inputField     :    '<%=txtAirCloserDate.ClientId%>',
                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                button         :    "imgAirCloserDate",
                                                                                                //align          :    "Tl",
                                                                                                singleClick    :    true
                                                                                                });
                                                                                                            </script>

                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Target Closure Date<span class="Mandatory">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtAirTargetCloserDate" runat="server" CssClass="textbox" Width="80%"
                                                                                                                TabIndex="11"></asp:TextBox>
                                                                                                            <img id="imgAirTargetCloserDate" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                                                                runat="server" title="Date selector" onclick="ShowCalender('txtAirTargetCloserDate','imgAirTargetCloserDate');"
                                                                                                                tabindex="11" />

                                                                                                            <script type="text/javascript">
                                                                                                Calendar.setup({
                                                                                                inputField     :    '<%=txtAirTargetCloserDate.ClientId%>',
                                                                                                ifFormat       :     "%d/%m/%Y",
                                                                                                button         :    "imgAirTargetCloserDate",
                                                                                                //align          :    "Tl",
                                                                                                singleClick    :    true
                                                                                                });
                                                                                                            </script>

                                                                                                        </td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                            <input id="BtnAirNonoAirIssueReported" runat="server" class="button topMargin" onclick="javascript:ShowAirNonAirIssueReported('AirNonAir');"
                                                                                                                style="width: 110px" tabindex="5" type="button" value=" Discussed items" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="Top" style="display: none; empty-cells: hide;">
                                                                                                        <td>
                                                                                                            Follow-ups</td>
                                                                                                        <td colspan="3" style="width: 71%">
                                                                                                            <asp:TextBox ID="TxtAirNonAirFollowup" runat="server" CssClass="textbox" Height="40px"
                                                                                                                Rows="4" TabIndex="11" TextMode="MultiLine" Width="93%"></asp:TextBox></td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="Top">
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td colspan="3" style="width: 71%">
                                                                                                        </td>
                                                                                                        <td class="center" style="vertical-align: top;">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4">
                                                                                                            <asp:CheckBox ID="ChkAirNonAirShowAll" runat="server" Text="Show All" TextAlign="Right"
                                                                                                                AutoPostBack="true" Visible="false" /></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4" style="width: 100%;">
                                                                                                            <asp:GridView ID="gvAirNonAir" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                Width="96%">
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                                                                    <asp:BoundField DataField="PRODUCT_NAME" HeaderText="Product Name" />
                                                                                                                    <asp:BoundField DataField="REVENUE" HeaderText="Revenue" />
                                                                                                                    <asp:BoundField DataField="SV_STATUS_NAME" HeaderText="Status" />
                                                                                                                    <asp:BoundField DataField="STA_SIGNON_DATE" HeaderText="Instalation Date" HeaderStyle-Wrap="true" />
                                                                                                                    <%-- <asp:BoundField DataField="ASSIGNTO_EMPLOYEE_NAME" HeaderText="Assigned to" HeaderStyle-Width="10%" />--%>
                                                                                                                    <asp:BoundField DataField="CLOSER_DATETIME" HeaderText="Closure Date" HeaderStyle-Width="8%" />
                                                                                                                    <asp:BoundField DataField="TARGET_CLOSER_DATETIME" HeaderText="Target Closure Date" />
                                                                                                                    <asp:BoundField DataField="LOGDATE" HeaderText="Logged Date" HeaderStyle-Width="7%" />
                                                                                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:HiddenField ID="HdSV_STATUSID" runat="server" Value='<%# Eval("SV_STATUSID") %>' />
                                                                                                                            <asp:HiddenField ID="HdDSR_STA_DETAIL_ID" runat="server" Value='<%# Eval("DSR_STA_DETAIL_ID") %>' />
                                                                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                                                OnClientClick="javascript:return CancelEditAirNonAir();" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") %>'></asp:LinkButton>&nbsp;
                                                                                                                            <asp:LinkButton ID="LnkAirNonairDel" runat="server" CommandName="AirNonairDelX" Text="Delete"
                                                                                                                                CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") %>'></asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                <RowStyle CssClass="textbold" />
                                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                                            </asp:GridView>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="5" style="width: 100%;">
                                                                                                            <input type="hidden" runat="server" id="hdAirNonAir" style="width: 1px" />
                                                                                                            <input type="hidden" runat="server" id="hdAirNonAirSaveData" style="width: 1px" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 100%" colspan="5">
                                                                                                            <ajax:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlProgressAirNonAir"
                                                                                                                TargetControlID="pnlProgressAirNonAir" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                                                ID="modAirNonAir" runat="server">
                                                                                                            </ajax:ModalPopupExtender>
                                                                                                            <asp:Panel ID="pnlProgressAirNonAir" runat="server" CssClass="overPanel_Test" Height="0px"
                                                                                                                Width="150px" BackColor="white">
                                                                                                                <table style="width: 150px; height: 150px;">
                                                                                                                    <tr>
                                                                                                                        <td valign="middle" align="center">
                                                                                                                            <img src="../Images/er.gif" id="img4" runat="server" alt="" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <ajax:CollapsiblePanelExtender ID="CollapsiblePanelAirNonAir" TargetControlID="pnlAirNonAir"
                                                                                                Collapsed="false" ExpandControlID="pnlAirNonAirCollapseManage" SuppressPostBack="true"
                                                                                                CollapseControlID="pnlAirNonAirCollapseManage" CollapsedImage="../Images/hide_arrow.gif"
                                                                                                ExpandedImage="../Images/show_arrow.gif" runat="Server" ImageControlID="imgAirNonAir" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <input type="hidden" runat="server" id="hdRetentionChk" style="width: 1px" value="5" />
                                                                            <input type="hidden" runat="server" id="hdTargetChk" style="width: 1px" value="1" />
                                                                            <input type="hidden" runat="server" id="hdAirNonAirChk" style="width: 1px" />
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
                    <input type="hidden" runat="server" id="hdID" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdLCode" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdVisitDATE" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdIsManager" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdBackDateAllow" style="width: 1px" />
                    <asp:HiddenField ID="hdPersonMetStaff" runat="server" />
                    <asp:HiddenField ID="hdRes1A" runat="server" />
                    <asp:HiddenField ID="HdCompetionCount" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Button ID="BtnConfirm" Text="" runat="server" CssClass="displayNone" />
                    <ajax:ModalPopupExtender ID="mdlPopUpCalender" runat="server" TargetControlID="BtnConfirm"
                        BackgroundCssClass="confirmationBackground" PopupControlID="pnlPopup" CancelControlID="BtnCancel">
                    </ajax:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" Width="875px" Height="530px" Style="display: none"
                        HorizontalAlign="Center" CssClass="confirmationPopup">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td valign="top" align="right" style="padding-right: 10px; padding-top: 10px;">
                                    <input type="button" class="modalCloseButton" runat="server" value="Close" id="BtnColose"
                                        onclick="CloseCalander();" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="center">
                                    <asp:Panel ID="PnlCalenderImagePnl" runat="server" CssClass="displayNone">
                                        <img src="../Images/er.gif" id="CalenderImage" runat="server" alt="" class="displayNone" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" valign="top">
                                    <iframe width="875px" scrolling="yes" runat="server" height="500px" src="" id="iframeID"
                                        frameborder="0"></iframe>
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
        </table>
    </form>
</body>
</html>
