<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SAUP_DSRAction.aspx.vb"
    Inherits="Sales_SAUP_DSRAction" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales DSR Action</title>
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
   <script type ="text/javascript" >
   
     // validating air non-air
          function ValidateActionAirNonAir()
          {
         
            document.getElementById('lblError').innerText="";
            document.getElementById('lblErrAirNonAirProduct').innerText="";
            
              //*********** Validating Product Name *************************
                var cboProductName=document.getElementById('ddlProductName');
                if(cboProductName.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Product name is mandatory.'
                    document.getElementById('lblErrAirNonAirProduct').innerText ='Product name is mandatory.'
                    cboProductName.focus();
                    return false;
                }
            
             //*********** Validating Revenue *****************************
              if(document.getElementById('txtRevenue').value =='')
                 {
                    document.getElementById('lblError').innerText = "Revenue is mandatory.";	
                    document.getElementById('lblErrAirNonAirProduct').innerText = "Revenue is mandatory.";			
                    document.getElementById('txtRevenue').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtRevenue').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='Revenue should contain only digits.'
                        document.getElementById('lblErrAirNonAirProduct').innerText ='Revenue should contain only digits.'
                        document.getElementById('txtRevenue').focus();
                        return false;
                     }
                 }
                 
                  //*********** Validating Status *************************
                var cboAirNonAirStatus=document.getElementById('ddlAirNonAirStatus');
                if(cboAirNonAirStatus.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Status is mandatory.'
                    document.getElementById('lblErrAirNonAirProduct').innerText ='Status is mandatory.'
                    cboAirNonAirStatus.focus();
                    return false;
                }
                
                //*********** Validating Signed On Date  *****************
                
                //var cboAirNonAirStatusValue=document.getElementById('ddlAirNonAirStatus').value;
                var cboAirNonAirStatus=document.getElementById('ddlAirNonAirStatus').value;
                var cboAirNonAirStatus1=cboAirNonAirStatus.split("|")[0];               
                var cboAirNonAirStatusValue=cboAirNonAirStatus1;//document.getElementById('cboAirNonAirStatus').value; 
               
                
                if (cboAirNonAirStatusValue == document.getElementById('hdAirNonAirChk').value)
                {
                  if(document.getElementById('txtAirNonAirSignedOn').value == '')
                    {
                       document.getElementById('lblError').innerText = "Instalation Date is mandatory.";
                       document.getElementById('lblErrAirNonAirProduct').innerText = "Instalation Date is mandatory.";			
                       document.getElementById('txtAirNonAirSignedOn').focus();
                       return false;  
                    }

                }
                
                
                if(document.getElementById('txtAirNonAirSignedOn').value != '')
                {
                    if (isDate(document.getElementById('txtAirNonAirSignedOn').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Instalation Date is not valid.";
                       document.getElementById('lblErrAirNonAirProduct').innerText = "Instalation Date is not valid.";			
                       document.getElementById('txtAirNonAirSignedOn').focus();
                       return false;  
                    }  
                }
                
     

//--------------------- Start Added On 13 th July by abhishek------------
     if(document.getElementById('DrpAirNonAirDept').value == '')
     {
        document.getElementById('lblError').innerText = "Department is mandatory.";
        document.getElementById('lblErrAirNonAirProduct').innerText ="Department is mandatory.";
        return false;  
     }
        if(document.getElementById('DrpAirNonAirAssignedTo').value == '')
     {
        document.getElementById('lblError').innerText = "Assigned to is mandatory.";
        document.getElementById('lblErrAirNonAirProduct').innerText ="Assigned to is mandatory.";
        return false;  
     }
       
     var cboAirNonAirStatus2=cboAirNonAirStatus.split("|")[1];    
     if (cboAirNonAirStatus2=="1")
                 {
                     if(document.getElementById('txtAirCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('lblErrAirNonAirProduct').innerText ="Closure date is mandatory.";
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtAirCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtAirCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                               document.getElementById('lblErrAirNonAirProduct').innerText = "Closure date is not valid.";
                               return false;  
                            }  
                    }   
                 }
                 else
                 {             
                        
                 }
                    //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtAirTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('lblErrAirNonAirProduct').innerText = "Target Closure date is mandatory.";
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtAirTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('lblErrAirNonAirProduct').innerText = "Target Closure date is not valid.";
                               return false;  
                            }  
                      
                      }   
                      
                    
                      
                      
                       if (document.getElementById("TxtAirNonAirFollowup").value.trim().length>1000)
                        {
                             document.getElementById("lblError").innerHTML="Follow ups greater than 1000 characters."
                             document.getElementById('lblErrAirNonAirProduct').innerText ="Follow ups greater than 1000 characters."
                             document.getElementById("TxtAirNonAirFollowup").focus();
                             return false;
                        }  

//--------------------- End Added On 13 th July by abhishek------------


   
          }
    function ValidateActionTarget()
          {
            document.getElementById('lblError').innerText="";
            document.getElementById('lblErrTarget').innerText="";
             //*********** Validating 1A Approved New Deal *****************************
              if(document.getElementById('txtTarget1AApprovedNewDeal').value =='')
                 {
                    document.getElementById('lblError').innerText = "1A approved new deal is mandatory.";	
                    document.getElementById('lblErrTarget').innerText = "1A approved new deal is mandatory.";			
                    document.getElementById('txtTarget1AApprovedNewDeal').focus();
                    return false;                   
                 }
//                
                 
                   //*********** Validating CPS *****************************
              if(document.getElementById('txtTargetCPS').value =='')
                 {
                    document.getElementById('lblError').innerText = "CPS is mandatory.";	
                    document.getElementById('lblErrTarget').innerText = "CPS is mandatory.";			
                    document.getElementById('txtTargetCPS').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtTargetCPS').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='CPS should contain only digits.'
                        document.getElementById('lblErrTarget').innerText ='CPS should contain only digits.'
                        document.getElementById('txtTargetCPS').focus();
                        return false;
                     }
                 }
                 
                 //*********** Validating Target Status *************************
                var cboTargetStatus=document.getElementById('ddlTargetStatus');
                if(cboTargetStatus.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Target status is mandatory.'
                    document.getElementById('lblErrTarget').innerText ='Target status is mandatory.'
                    cboTargetStatus.focus();
                    return false;
                }
                
             
                  
                    var cboTarStatus=document.getElementById('ddlTargetStatus').value;
                    var cboTarStatus1=cboTarStatus.split("|")[0];               
                    var cboTargetStatusValue=cboTarStatus1;
                  
                  
                if (cboTargetStatusValue == document.getElementById('hdTargetChk').value)
                {
                  if(document.getElementById('txtTargetSignedOn').value == '')
                    {
                       document.getElementById('lblError').innerText = "Signed on is mandatory.";
                       document.getElementById('lblErrTarget').innerText = "Signed on is mandatory.";			
                       document.getElementById('txtTargetSignedOn').focus();
                       return false;  
                    }

                }
                  
                if(document.getElementById('txtTargetSignedOn').value != '')
                {
                    if (isDate(document.getElementById('txtTargetSignedOn').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Signed on is not valid.";
                       document.getElementById('lblErrTarget').innerText = "Signed on is not valid.";			
                       document.getElementById('txtTargetSignedOn').focus();
                       return false;  
                    }  
                }
            
                 //*********** Validating Target- Segs / % of Business *****************************
              if(document.getElementById('txtTargetTargetSegs').value =='')
                 {
                    document.getElementById('lblError').innerText = "Target-Segs / % of Business is mandatory.";	
                    document.getElementById('lblErrTarget').innerText = "Target-Segs / % of Business is mandatory.";			
                    document.getElementById('txtTargetTargetSegs').focus();
                    return false;                   
                 }


//--------------------- Start Added On 13 th July by abhishek------------
     if(document.getElementById('DrpTargetDept').value == '')
     {
        document.getElementById('lblError').innerText = "Department is mandatory.";
        document.getElementById('lblErrTarget').innerText ="Department is mandatory.";
        return false;  
     }
        if(document.getElementById('DrpTargetAssignedTo').value == '')
     {
        document.getElementById('lblError').innerText = "Assigned to is mandatory.";
        document.getElementById('lblErrTarget').innerText ="Assigned to is mandatory.";
        return false;  
     }
       
     var cboTarStatus2=cboTarStatus.split("|")[1];    
     if (cboTarStatus2=="1")
                 {
                     if(document.getElementById('txtTarCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('lblErrTarget').innerText ="Closure date is mandatory.";
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtTarCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtTarCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                                document.getElementById('lblErrTarget').innerText = "Closure date is not valid.";
                               return false;  
                            }  
                    }   
                 }
                 else
                 {             
                        
                 }
                    //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtTarTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('lblErrTarget').innerText = "Target Closure date is mandatory.";
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtTarTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('lblErrTarget').innerText = "Target Closure date is not valid.";
                               return false;  
                            }  
                      
                      } 
                      
                  
                      
                        
                       if (document.getElementById("TxtTarFollowup").value.trim().length>1000)
                        {
                             document.getElementById("lblError").innerHTML="Follow ups greater than 1000 characters."
                             document.getElementById('lblErrTarget').innerText ="Follow ups greater than 1000 characters."
                             document.getElementById("TxtTarFollowup").focus();
                             return false;
                        }  

//--------------------- End Added On 13 th July by abhishek------------
          }
   
     function ValidateActionRetention()
          {
            document.getElementById('lblError').innerText="";
            document.getElementById('lblErrRetention').innerText="";

           
              if(document.getElementById('txtCPS').value =='')
                 {
                    document.getElementById('lblError').innerText = "CPS is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "CPS is mandatory.";			
                    document.getElementById('txtCPS').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtCPS').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='CPS should contain only digits.'
                        document.getElementById('lblErrRetention').innerText ='CPS should contain only digits.'
                        document.getElementById('txtCPS').focus();
                        return false;
                     }
                 }
                //*********** Validating Retention Reason *************************
                var cboRetentionReason=document.getElementById('ddlRetentionReason');
                if(cboRetentionReason.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Retention reason is mandatory.'
                    document.getElementById('lblErrRetention').innerText ='Retention reason is mandatory.'
                    cboRetentionReason.focus();
                    return false;
                }
                
                //*********** Validating Retention Status *************************
                var cboRetentionStatus=document.getElementById('ddlRetentionStatus');
               
                if(cboRetentionStatus.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Retention status is mandatory.'
                    document.getElementById('lblErrRetention').innerText ='Retention status is mandatory.'
                    cboRetentionStatus.focus();
                    return false;
                }
                
                 //*********** Validating New CPS *****************************
              if(document.getElementById('txtNewCPS').value =='')
                 {
                    document.getElementById('lblError').innerText = "New CPS is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "New CPS is mandatory.";			
                    document.getElementById('txtNewCPS').focus();
                    return false;                   
                 }
                 else
                 {
                    var strValue = document.getElementById('txtNewCPS').value
                    reg = new RegExp("^[0-9]+$"); 
                    if(reg.test(strValue) == false) 
                    {
                        document.getElementById('lblError').innerText ='New CPS should contain only digits.'
                        document.getElementById('lblErrRetention').innerText ='New CPS should contain only digits.'
                        document.getElementById('txtNewCPS').focus();
                        return false;
                     }
                 }
                 
                 //*********** Validating 1A Approved New Deal *****************************
              if(document.getElementById('txt1AApprovedNewDeal').value =='')
                 {
                    document.getElementById('lblError').innerText = "1A approved new deal is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "1A approved new deal is mandatory.";			
                    document.getElementById('txt1AApprovedNewDeal').focus();
                    return false;                   
                 }

               
                var cboRetStatus=document.getElementById('ddlRetentionStatus').value;
                var cboRetStatusStatus1=cboRetStatus.split("|")[0];               
                var cboRetentionStatusValue=cboRetStatusStatus1;
               
                if (cboRetentionStatusValue == document.getElementById('hdRetentionChk').value)
                {
                  if(document.getElementById('txtRetentionSignedOn').value == '')
                    {
                       document.getElementById('lblError').innerText = "Signed on is mandatory.";
                       document.getElementById('lblErrRetention').innerText = "Signed on is mandatory.";			
                       document.getElementById('txtRetentionSignedOn').focus();
                       return false;  
                    }

                }
                 
                  //*********** Validating Signed On Date  *****************
                if(document.getElementById('txtRetentionSignedOn').value != '')
                {
                    if (isDate(document.getElementById('txtRetentionSignedOn').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerText = "Signed on is not valid.";
                       document.getElementById('lblErrRetention').innerText = "Signed on is not valid.";			
                       document.getElementById('txtRetentionSignedOn').focus();
                       return false;  
                    }  
                }
            
                 //*********** Validating Target- Segs / % of Business *****************************
              if(document.getElementById('txtRetentionTargetSegs').value =='')
                 {
                    document.getElementById('lblError').innerText = "Target-Segs / % of Business is mandatory.";	
                    document.getElementById('lblErrRetention').innerText = "Target-Segs / % of Business is mandatory.";			
                    document.getElementById('txtRetentionTargetSegs').focus();
                    return false;                   
                 }



//--------------------- Start Added On 13 th July by abhishek------------

    if(document.getElementById('DrpRetDept').value == '')
     {
        document.getElementById('lblError').innerText = "Department is mandatory.";
        document.getElementById('lblErrRetention').innerText ="Department is mandatory.";
        return false;  
     }
        if(document.getElementById('DrpRetAssignedTo').value == '')
     {
        document.getElementById('lblError').innerText = "Assigned to is mandatory.";
        document.getElementById('lblErrRetention').innerText ="Assigned to is mandatory.";
        return false;  
     }
       
     var cboRetStatusStatus2=cboRetStatus.split("|")[1];    
     if (cboRetStatusStatus2=="1")
                 {
                     if(document.getElementById('txtRetCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('lblErrRetention').innerText ="Closure date is mandatory.";
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtRetCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtRetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                               document.getElementById('lblErrRetention').innerText = "Closure date is not valid.";
                               return false;  
                            }  
                    }   
                 }
                 else
                 {             
                        
                 }
                    //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtRetTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('lblErrRetention').innerText = "Target Closure date is mandatory.";
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtRetTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('lblErrRetention').innerText = "Target Closure date is not valid.";
                               return false;  
                            }  
                      
                      }
                    
                         
                       if (document.getElementById("TxtRetFollowup").value.trim().length>1000)
                        {
                             document.getElementById("lblError").innerHTML="Follow ups greater than 1000 characters."
                             document.getElementById('lblErrRetention').innerText ="Follow ups greater than 1000 characters."
                             document.getElementById("TxtRetFollowup").focus();
                             return false;
                        }  

//--------------------- End Added On 13 th July by abhishek------------





        
          }
   
     // validating service call
          function ValidateActionServiceCall()
          {
                document.getElementById('lblError').innerText="";
                document.getElementById('lblErrorServiceCall').innerText="";
              //*********** Validating Department  *****************************
                var cboDepartment=document.getElementById('ddlDepartment');
                if(cboDepartment.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Department is mandatory.'
                    document.getElementById('lblErrorServiceCall').innerText ='Department is mandatory.'
                    cboDepartment.focus();
                    return false;
                }
                
                if(document.getElementById('txtDetailedDiscussion').value =='')
                 {
                    document.getElementById('lblError').innerText = "Detailed Disc/Issue Reported is mandatory.";	
                    document.getElementById('lblErrorServiceCall').innerText = "Detailed Disc/Issue Reported is mandatory.";			
                    document.getElementById('txtDetailedDiscussion').focus();
                    return false;                   
                 }
                
                //*********** Validating Service Satus  *************************
                var cboServiceStatus=document.getElementById('ddlServiceStatus');
                var strStatus=document.getElementById('ddlServiceStatus').value;
                if(cboServiceStatus.selectedIndex ==0)
                {
                    document.getElementById('lblError').innerText ='Service staus is mandatory.'
                    document.getElementById('lblErrorServiceCall').innerText ='Service staus is mandatory.'
                    cboServiceStatus.focus();
                    return false;
                }
                
                   //*********** Validating  Closer Date  *****************              
                 
                 
                 if (strStatus.split("|")[1]=="1")
                 {
                     if(document.getElementById('txtCloserDate').value == '')
                     {
                        document.getElementById('lblError').innerText = "Closure date is mandatory.";
                        document.getElementById('lblErrorServiceCall').innerText ="Closure date is mandatory.";
                        return false;  
                     }
                     
                     
                    if(document.getElementById('txtCloserDate').value != '')
                    {
                        if (isDate(document.getElementById('txtCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Closure date is not valid.";
                                document.getElementById('lblErrorServiceCall').innerText = "Closure date is not valid.";
                               return false;  
                            }  
                    }   
                 }
                 else
                 {
                 
                     if (document.getElementById('dlstAssignedTo').value == '')
                      {
                        document.getElementById('lblError').innerText = "Assigned To is mandatory.";
                        document.getElementById('lblErrorServiceCall').innerText = "Assigned To is mandatory.";
                        return false;
                        
                      }
                 
                               
                        
                 }
                    //*********** Validating Target Closer Date  *****************
                      if (document.getElementById('txtTargetCloserDate').value == '')
                      {
                        document.getElementById('lblError').innerText = "Target Closure date is mandatory.";
                        document.getElementById('lblErrorServiceCall').innerText = "Target Closure date is mandatory.";
                        return false;
                        
                      }
                      else
                      {
                          if (isDate(document.getElementById('txtTargetCloserDate').value,"d/M/yyyy") == false)	
                            {
                               document.getElementById('lblError').innerText = "Target Closure date is not valid.";
                               document.getElementById('lblErrorServiceCall').innerText = "Target Closure date is not valid.";
                               return false;  
                            }  
                      
                      }   
        }
   
         function EditServiceCallFolowupRem(DSR_SC_DETAIL_ID,DSRCODE,VisitDATE,STATUSID)
  {    
              // var DSR_VISIT_ID=document.getElementById("DSRCODE").value;
              // var VisitDATE=document.getElementById("hdVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;   	           	      	   
   	           var parameter="DSR_VISIT_ID=" + DSRCODE   + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE  + "&SCDETAILID=" + DSR_SC_DETAIL_ID + "&STATUSID=" + STATUSID;;
               
              // alert(parameter);
               type = "SASR_SCFollowupRem.aspx?" + parameter;
               window.open(type,"SASR_SCFollowupRem","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false; 
  }
  
  function ShowSCMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdSCDSRCODE").value;
               var VisitDATE=document.getElementById("hdSCVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowTarMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdTarDSRCODE").value;
               var VisitDATE=document.getElementById("hdTarVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowRetMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdRetDSRCODE").value;
               var VisitDATE=document.getElementById("hdRetVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowAirNonAirMarketInfo(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdAirDSRCODE").value;
               var VisitDATE=document.getElementById("hdAirVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_MarketInfo.aspx?" + parameter;
               window.open(type,"SASR_MarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowTarIssueReported(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdTarDSRCODE").value;
               var VisitDATE=document.getElementById("hdTarVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_IssueReported.aspx?" + parameter;
               window.open(type,"SASR_SCMarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
   function ShowRetIssueReported(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdRetDSRCODE").value;
               var VisitDATE=document.getElementById("hdRetVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_IssueReported.aspx?" + parameter;
               window.open(type,"SASR_SCMarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
  
   function ShowAirNonAirIssueReported(id)
  {
               var DSR_VISIT_ID=document.getElementById("hdAirDSRCODE").value;
               var VisitDATE=document.getElementById("hdAirVisitDATE").value;
               var LCode=document.getElementById("hdLCode").value;
   	                      	      	   
   	           var parameter="DSR_VISIT_ID=" + DSR_VISIT_ID  + "&REMARKS_TYPE=" + id + "&Lcode=" + LCode + "&VisitDate=" + VisitDATE ;
               type = "SASR_IssueReported.aspx?" + parameter;
               window.open(type,"SASR_SCMarketInfo","height=600,width=710,top=30,left=20,scrollbars=1,status=1,resizable=1");            
               return false;     
  
  }
    function CallAssignedOnOrOff (strStatusID)
         {
            var strId = document.getElementById(strStatusID).value;
          //  alert(strId);
            var itemIndex=document.getElementById("ddlServiceStatus").selectedIndex;
	        var strServiceStatusName;
	        strServiceStatusName=  document.getElementById("ddlServiceStatus").options[itemIndex].text;
            var id = strId.split("|")[1];
            //alert(id);
            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            
            if (id == "1")
            {                
                document.getElementById("txtCloserDate").value = day + "/" + month  + "/" + year;
              
            }
            else if(id=="0")
            {
                document.getElementById("txtCloserDate").value = "";
            }           
            else
            {
              document.getElementById("txtCloserDate").value = "";
            }          
         }
     function SignedOnDateByRetStatus (strStatusID)
         {
            var strId = document.getElementById(strStatusID).value;
          //  alert(strId);
            var itemIndex=document.getElementById("ddlRetentionStatus").selectedIndex;
	        var strServiceStatusName;
	        strServiceStatusName=  document.getElementById("ddlRetentionStatus").options[itemIndex].text;
            var id = strId.split("|")[1];
            //alert(id);
            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            
                    document.getElementById('txtRetCloserDate').readOnly=true;                   
                    document.getElementById('imgRetCloserDate').style.display ="none";
                    document.getElementById('txtRetCloserDate').className ="textboxgrey";
                    
            if (id == "1")
            {                
                document.getElementById("txtRetCloserDate").value = day + "/" + month  + "/" + year;
              
                    document.getElementById('txtRetCloserDate').readOnly=false;                   
                    document.getElementById('imgRetCloserDate').style.display ="block";
                    document.getElementById('txtRetCloserDate').className ="textbox";
            }
            else if(id=="0")
            {
                document.getElementById("txtRetCloserDate").value = "";
            }           
            else
            {
              document.getElementById("txtRetCloserDate").value = "";
            }          
         }
         
          function SignedOnDateByTargetStatus (strStatusID)
         {
            var strId = document.getElementById(strStatusID).value;
          //  alert(strId);
            var itemIndex=document.getElementById("ddlTargetStatus").selectedIndex;
	        var strServiceStatusName;
	        strServiceStatusName=  document.getElementById("ddlTargetStatus").options[itemIndex].text;
            var id = strId.split("|")[1];
            //alert(id);
            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            
                    document.getElementById('txtTarCloserDate').readOnly=true;                   
                    document.getElementById('imgTarCloserDate').style.display ="none";
                    document.getElementById('txtTarCloserDate').className ="textboxgrey";
            
            if (id == "1")
            {                
                    document.getElementById("txtTarCloserDate").value = day + "/" + month  + "/" + year;
                  
                    document.getElementById('txtTarCloserDate').readOnly=false;                   
                    document.getElementById('imgTarCloserDate').style.display ="block";
                    document.getElementById('txtTarCloserDate').className ="textbox";
                
              
            }
            else if(id=="0")
            {
                document.getElementById("txtTarCloserDate").value = "";
            }           
            else
            {
              document.getElementById("txtTarCloserDate").value = "";
            }          
         }
         
             function CallAirNonAirForsignDate (strStatusID)
         {
            var strId = document.getElementById(strStatusID).value;
           // alert(strId);
            var itemIndex=document.getElementById("ddlAirNonAirStatus").selectedIndex;
	        var strServiceStatusName;
	        strServiceStatusName=  document.getElementById("ddlAirNonAirStatus").options[itemIndex].text;
            var id = strId.split("|")[1];
            //alert(id);
            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            
                    document.getElementById('txtAirCloserDate').readOnly=true;                   
                    document.getElementById('imgAirCloserDate').style.display ="none";
                    document.getElementById('txtAirCloserDate').className ="textboxgrey";
            
            if (id == "1")
            {                
                    document.getElementById("txtAirCloserDate").value = day + "/" + month  + "/" + year;
                    document.getElementById('txtAirCloserDate').readOnly=false;                   
                    document.getElementById('imgAirCloserDate').style.display ="block";
                    document.getElementById('txtAirCloserDate').className ="textbox";
              
            }
            else if(id=="0")
            {
                document.getElementById("txtAirCloserDate").value = "";
            }           
            else
            {
              document.getElementById("txtAirCloserDate").value = "";
            }          
         }
   
   
   </script> 
    
</head>
<body>
    <form id="form1" runat="server">
        <table width="99%" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top left" style="width: 80%">
                                <span class="menu">Sales -> DSR -> </span><span class="sub_menu">DSR Action</span>
                            </td>
                            <td class="right" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="heading center" colspan="2" style="width: 100%">
                                <asp:Label ID="lblHeading" runat="server" Text="DSR Action Details"></asp:Label>
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
                                                    <td class="top" style="width: 100%; display :none; empty-cells:hide;" >
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
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Address</td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey TextTitleCase"
                                                                        ReadOnly="True" Rows="3" TabIndex="20" TextMode="MultiLine" Width="95%"></asp:TextBox></td>
                                                                <td class="center top">
                                                                </td>
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
                                                                    DSR Code
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDSRCode" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="87%"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                            <tr>
                                                                <td class="top" style="width: 100%;">
                                                                    <asp:Panel ID="pnlServiceCall" runat="server"  width="100%">
                                                                        <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                             <tr>
                                                                                <td style="width: 100%;" class="heading" colspan ="5">
                                                                                    Service Call
                                                                                </td>
                                                                            </tr>
                                                                            
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
                                                                                    <asp:Button ID="btnUpdateServiceCall" Text="Update" runat="Server" CssClass="button"
                                                                                        Width="110px" OnClientClick="javascript:return ValidateActionServiceCall();" TabIndex="5" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="top">
                                                                                <td style="vertical-align: top;">
                                                                                    Detailed Disc /<br />
                                                                                    Issue Reported<span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td style="width: 80%" colspan="3">
                                                                                    <asp:TextBox ID="txtDetailedDiscussion" runat="server" CssClass="textboxgrey" Width="94%" ReadOnly ="true" 
                                                                                        TextMode="MultiLine" Rows="4" Height="40px" TabIndex="4"></asp:TextBox>
                                                                                </td>
                                                                                <td class="center" style="vertical-align: top;" rowspan="2">
                                                                                    <asp:Button ID="btnCancelServiceCall" Text="Cancel" runat="Server" CssClass="button"
                                                                                        Width="110px"  TabIndex="5" /><br />
                                                                                    <br />
                                                                                    <input type="button" onclick="javascript:ShowSCMarketInfo('SC');" style="width: 110px;"
                                                                                        id="BtnSCMarketInfo" value="Comp/Mkt info " runat="server" class="button" tabindex="5" />&nbsp;</td>
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
                                                                                    <asp:TextBox ID="txtTargetCloserDate" runat="server" CssClass="textboxgrey" Width="84%"
                                                                                      ReadOnly ="true"    TabIndex="4"></asp:TextBox>
                                                                                  

                                                                                </td>
                                                                                <td class="center" style="vertical-align: top;"><asp:Button ID="BtnSCReset" Text="Reset" runat="server" CssClass="button"
                                                                                        Width="110px"  TabIndex="5" /></td>
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
                                                                                        Width="98%">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" HeaderStyle-Width="3%" />
                                                                                            <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Deptt" HeaderStyle-Width="10%" />
                                                                                            <asp:BoundField DataField="DEPARTMENT_SPECIFIC" HeaderText="Deptt Specific" HeaderStyle-Width="10%" />
                                                                                            <asp:TemplateField HeaderText="Detailed Disc /Issue Reported" HeaderStyle-Width="27%">
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
                                                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="110px" ItemStyle-Wrap="false"
                                                                                                ItemStyle-Width="110px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:HiddenField ID="HdSC_STATUSID" runat="server" Value='<%# Eval("SC_STATUSID") %>' />
                                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_SC_DETAIL_ID") +"|" + DataBinder.Eval(Container.DataItem, "DSRCODE")  +"|" + DataBinder.Eval(Container.DataItem, "PREDATE")   %>'></asp:LinkButton>&nbsp;&nbsp;
                                                                                                    <asp:LinkButton ID="LnkSCFRem" runat="server" CommandName="FupRemX" Text="Followup"
                                                                                                        CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_SC_DETAIL_ID") +"|" + DataBinder.Eval(Container.DataItem, "DSRCODE")  +"|" + DataBinder.Eval(Container.DataItem, "PREDATE")   %>'></asp:LinkButton>&nbsp;
                                                                                                   
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
                                                                                            <input type="hidden" runat="server" id="hdSCDSRCODE" style="width: 1px" />
                                                                                            <input type="hidden" runat="server" id="hdSCVisitDATE" style="width: 1px" />
                                                                                        </td>
                                                                                    </tr>
                                                                          
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="top" style="width: 100%;">
                                                                    <asp:Panel ID="pnlStrategicVisits" runat="server" Width="100%">
                                                                        <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                                            <tr class="top">
                                                                                <td style="width: 100%;">
                                                                                    <asp:Panel ID="pnlRetention" runat="server" Width="100%">
                                                                                        <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                                <tr>
                                                                                                <td style="width: 100%;" class="heading" colspan ="5">
                                                                                                    Retention Strategic Call
                                                                                                </td>
                                                                                            </tr>
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
                                                                                                    <asp:TextBox ID="txtCPS" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="84%" TabIndex="7"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="center">
                                                                                                    <asp:Button ID="btnUpdateRetention" Text="Update" runat="Server" CssClass="button" Width="110px"
                                                                                                        OnClientClick="javascript:return ValidateActionRetention();" TabIndex="8" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr class="top">
                                                                                                <td>
                                                                                                    Retention Reason <span class="Mandatory">*</span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="ddlRetentionReason" Enabled ="false"  runat="server" CssClass="dropdownlist"
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
                                                                                                        Width="110px"  TabIndex="8" /></td>
                                                                                            </tr>
                                                                                            <tr class="top">
                                                                                                <td>
                                                                                                    New CPS<span class="Mandatory">*</span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtNewCPS" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="84%" TabIndex="7"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    1A Approved
                                                                                                    <br />
                                                                                                    New Deal<span class="Mandatory">*</span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt1AApprovedNewDeal" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="84%"
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
                                                                                                                </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td>
                                                                                                    Target- Segs / % of Business<span class="Mandatory">*</span>
                                                                                                </td>
                                                                                                <td class="Top">
                                                                                                    <asp:TextBox ID="txtRetentionTargetSegs" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="84%"
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
                                                                                                <td class="center" valign="top"><asp:Button ID="BtnRetReset" Text="Reset" runat="server" CssClass="button"
                                                                                        Width="110px"  TabIndex="5" BorderStyle="None" /></td>
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
                                                                                                    <asp:TextBox ID="txtRetTargetCloserDate" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="84%"
                                                                                                        TabIndex="7"></asp:TextBox>
                                                                                                   

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
                                                                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:HiddenField ID="HdSVR_STATUSID" runat="server" Value='<%# Eval("SVR_STATUSID") %>' />
                                                                                                                                <asp:HiddenField ID="HdDSR_STR_DETAIL_ID" runat="server" Value='<%# Eval("DSR_STR_DETAIL_ID") %>' />
                                                                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_STR_DETAIL_ID") +"|" + DataBinder.Eval(Container.DataItem, "DSRCODE") +"|" + DataBinder.Eval(Container.DataItem, "PREDATE") %>'></asp:LinkButton>&nbsp;
                                                                                                                                <asp:LinkButton ID="LnkRetDel" runat="server" CommandName="RetDelX" Text="Delete"
                                                                                                                                    CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_STR_DETAIL_ID") +"|" + DataBinder.Eval(Container.DataItem, "DSRCODE") +"|" + DataBinder.Eval(Container.DataItem, "PREDATE") %>'></asp:LinkButton>
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
                                                                                                    <input type="hidden" runat="server" id="hdRetAssingedTo" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdDSR_STR_DETAIL_ID" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdRetDSRCODE" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdRetVisitDATE" style="width: 1px" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="top">
                                                                                <td style="width: 100%;">
                                                                                    <asp:Panel ID="pnlTarget" runat="server"  width="100%">
                                                                                        <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                               <tr>
                                                                                                <td style="width: 100%;" class="heading" colspan ="5">
                                                                                                    Target Strategic Call
                                                                                                </td>
                                                                                            </tr>
                                                                                            
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
                                                                                                    <asp:TextBox ID="txtTarget1AApprovedNewDeal" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="80%"
                                                                                                        TabIndex="9" MaxLength="500"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    CPS <span class="Mandatory">*</span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtTargetCPS" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="81%" TabIndex="9"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="center">
                                                                                                    <asp:Button ID="btnUpdateTarget" Text="Update" runat="Server" CssClass="button" Width="110px"
                                                                                                        OnClientClick="javascript:return ValidateActionTarget();" TabIndex="10" />
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
                                                                                                    <asp:Button ID="btnCancelTarget" Text="Cancel" runat="Server" CssClass="button"
                                                                                                        Width="110px" TabIndex="10" /></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    Signed On /<br />
                                                                                                    Conversion On (date)
                                                                                                </td>
                                                                                                <td valign="top" align="left">
                                                                                                    <table width="98%" cellpadding="0" cellspacing="0" border="0">
                                                                                                        <tr>
                                                                                                            <td valign="top" align="left">
                                                                                                                <asp:TextBox ID="txtTargetSignedOn" runat="server" CssClass="textboxgrey" Width="88%"
                                                                                                                    ReadOnly="true" TabIndex="9"></asp:TextBox></td>
                                                                                                            <td valign="top" style="width: 7%">
                                                                                                              
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td valign="top">
                                                                                                    Target- Segs /<br />
                                                                                                    % of Business<span class="Mandatory">*</span>
                                                                                                </td>
                                                                                                <td valign="top">
                                                                                                    <asp:TextBox ID="txtTargetTargetSegs" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="81%"
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
                                                                                                    <asp:TextBox ID="txtTarTargetCloserDate" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="81%"
                                                                                                        TabIndex="5"></asp:TextBox>
                                                                                                  
                                                                                                </td>
                                                                                                <td class="center" style="vertical-align: top;"><asp:Button ID="BtnTarReset" Text="Reset" runat="server" CssClass="button"
                                                                                        Width="110px"  TabIndex="5" /></td>
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
                                                                                                            <asp:BoundField DataField="CLOSER_DATETIME" HeaderText="Closure Date" HeaderStyle-Width="8%" />
                                                                                                            <asp:BoundField DataField="TARGET_CLOSER_DATETIME" HeaderText="Target Closure Date"
                                                                                                                HeaderStyle-Width="10%" />
                                                                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="HdSVT_STATUSID" runat="server" Value='<%# Eval("SVT_STATUSID") %>' />
                                                                                                                    <asp:HiddenField ID="DSR_STT_DETAIL_ID" runat="server" Value='<%# Eval("DSR_STT_DETAIL_ID") %>' />
                                                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_STT_DETAIL_ID") +"|" + DataBinder.Eval(Container.DataItem, "DSRCODE") +"|" + DataBinder.Eval(Container.DataItem, "PREDATE") %>'></asp:LinkButton>&nbsp;
                                                                                                                    <asp:LinkButton ID="LnkTarDel" runat="server" CommandName="TarDelX" Text="Delete"
                                                                                                                        CssClass="LinkButtons"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_STT_DETAIL_ID") +"|" + DataBinder.Eval(Container.DataItem, "DSRCODE") +"|" + DataBinder.Eval(Container.DataItem, "PREDATE") %>'></asp:LinkButton>
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
                                                                                                    <input type="hidden" runat="server" id="hdTarAssingedTo" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdDSR_STT_DETAIL_ID" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdTarDSRCODE" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdTarVisitDATE" style="width: 1px" />
                                                                                                </td>
                                                                                            </tr>
                                                                                           
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="top">
                                                                                <td style="width: 100%;">
                                                                                    <asp:Panel ID="pnlAirNonAir" runat="server"  width="100%">
                                                                                        <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                                               <tr>
                                                                                                <td style="width: 100%;" class="heading" colspan ="5">
                                                                                                    Air Non Air Strategic Call
                                                                                                </td>
                                                                                            </tr>
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
                                                                                                    <asp:DropDownList ID="ddlProductName" Enabled ="false"  runat="server" CssClass="dropdownlist" Width="83%"
                                                                                                        TabIndex="11">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td>
                                                                                                    Revenue <span class="Mandatory">*</span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtRevenue" runat="server" CssClass="textboxgrey" ReadOnly="True"  Width="80%" TabIndex="11"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="center">
                                                                                                    <asp:Button ID="btnUpdateAirNonAir" Text="Update" runat="Server" CssClass="button" Width="110px"
                                                                                                        OnClientClick="javascript:return ValidateActionAirNonAir();" TabIndex="12" />
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
                                                                                                    <table width="84%" cellpadding ="0" cellspacing ="0">
                                                                                                        <tr>
                                                                                                            <td valign="top" style="height: 19px"  Width="92%">
                                                                                                                <asp:TextBox ID="txtAirNonAirSignedOn" runat="server" CssClass="textboxgrey" Width="100%"
                                                                                                                    ReadOnly="true" TabIndex="11"></asp:TextBox></td>
                                                                                                            <td valign="top" style="height: 19px">
                                                                                                              

                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td class="center">
                                                                                                    <asp:Button ID="btnCancelAirNonAir" Text="Cancel" runat="Server" CssClass="button"
                                                                                                        Width="110px"  TabIndex="12" /></td>
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
                                                                                                    <asp:TextBox ID="txtAirTargetCloserDate" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="80%"
                                                                                                        TabIndex="11"></asp:TextBox>
                                                                                                  
                                                                                                </td>
                                                                                                <td class="center" style="vertical-align: top;">
                                                                                                    <input id="BtnAirNonoAirIssueReported" runat="server" class="button topMargin" onclick="javascript:ShowAirNonAirIssueReported('AirNonAir');"
                                                                                                        style="width: 110px" tabindex="5" type="button" value=" Discussed items" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td nowrap="nowrap">
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td class="center" style="vertical-align: top">
                                                                                                    <asp:Button ID="BtnAirReset" Text="Reset" runat="server" CssClass="button"
                                                                                        Width="110px"  TabIndex="5" /></td>
                                                                                            </tr>
                                                                                            <tr class="Top" style="display: none; empty-cells: hide;">
                                                                                                <td>
                                                                                                    Follow-ups</td>
                                                                                                <td colspan="3" style="width: 71%">
                                                                                                    <asp:TextBox ID="TxtAirNonAirFollowup" runat="server" CssClass="textbox" Height="40px"
                                                                                                        Rows="4" TabIndex="11" TextMode="MultiLine" Width="93%"></asp:TextBox></td>
                                                                                                <td class="center" style="vertical-align: top;"></td>
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
                                                                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="HdSV_STATUSID" runat="server" Value='<%# Eval("SV_STATUSID") %>' />
                                                                                                                    <asp:HiddenField ID="HdDSR_STA_DETAIL_ID" runat="server" Value='<%# Eval("DSR_STA_DETAIL_ID") %>' />
                                                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                                          CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_STA_DETAIL_ID") +"|" + DataBinder.Eval(Container.DataItem, "DSRCODE") +"|" + DataBinder.Eval(Container.DataItem, "PREDATE") %>' ></asp:LinkButton>&nbsp;
                                                                                                                    <asp:LinkButton ID="LnkAirNonairDel" runat="server" CommandName="AirNonairDelX" Text="Delete"
                                                                                                                        CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DSR_STA_DETAIL_ID") +"|" + DataBinder.Eval(Container.DataItem, "DSRCODE") +"|" + DataBinder.Eval(Container.DataItem, "PREDATE") %>' ></asp:LinkButton>
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
                                                                                                    <input type="hidden" runat="server" id="hdAirNonAir" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdAirAssingedTo" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdDSR_STA_DETAIL_ID" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdAirDSRCODE" style="width: 1px" />
                                                                                                    <input type="hidden" runat="server" id="hdAirVisitDATE" style="width: 1px" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
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
                                </table>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" runat="server" id="hdDSRCODE" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdDSR_DETAIL_ID" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdLCode" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdVisitDATE" style="width: 1px" />                     
                    <input type="hidden" runat="server" id="HdSTRATEGICTYPE" style="width: 1px" />                 
                    <input type="hidden" runat="server" id="HdVISITSUBTYPE" style="width: 1px" />
                     <input type="hidden" runat="server" id="hdRetentionChk" style="width: 1px" />
                      <input type="hidden" runat="server" id="hdAirNonAirChk" style="width: 1px" />
                       <input type="hidden" runat="server" id="hdTargetChk" style="width: 1px" />
                    
                    
                    
                </td>
                
            </tr>
        </table>
    </form>
</body>
</html>
