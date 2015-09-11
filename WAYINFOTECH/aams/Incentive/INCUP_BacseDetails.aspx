<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCUP_BacseDetails.aspx.vb"
    ValidateRequest="false" Inherits="Incentive_INCSR_BacseDetails" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Incentive::Manage Bussiness Case</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
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
.displayNone1 
{
	display:none;
}


.modalPopupConv {
	background-color:#ffffdd;
	border:8px solid #3a3434;
	padding:3px;
	width:250px;
	background-color:#ffffff;



</style>

    <script type="text/javascript" language="javascript">
    function MandatoryForQual()
    {
     try{
          //debugger;              
               var chkboxSQMore =document.getElementById("chkLstSQMore");
                 if( chkboxSQMore !=null  )
                    {
                       var inputArrSQMore = chkboxSQMore.getElementsByTagName('input');
                       var intLengthSQMore=0;
                       intLengthSQMore =inputArrSQMore.length;
                       var intTotalSelection ;
                       intTotalSelection=0
                       for(Counter=0;Counter<intLengthSQMore;Counter++)
                                {
                                    var elementID='chkLstSQMore_'+ Counter;                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                         intTotalSelection+=1;   
                                     }
                                 }                         
                       
                       if (intTotalSelection==0 )
                       {
                         document.getElementById('LBSQMsg').innerHTML="Select Qualification."                          
                         return false;
                       }                       
                    }
        }  
            catch(err){alert(err)}                 
                    
    }
    
     function MandatoryForMS()
    {
     try{
        //  debugger;              
               var chkboxMSMore =document.getElementById("chkLstMSMore");
                 if( chkboxMSMore !=null  )
                    {
                       var inputArrMSMore = chkboxMSMore.getElementsByTagName('input');
                       var intLengthMSMore=0;
                       intLengthMSMore =inputArrMSMore.length;
                       var intTotalSelection ;
                       intTotalSelection=0
                       for(Counter=0;Counter<intLengthMSMore;Counter++)
                                {
                                    var elementID='chkLstMSMore_'+ Counter;                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                         intTotalSelection+=1;   
                                     }
                                 }                         
                       
                       if (intTotalSelection==0 )
                       {
                         document.getElementById('LblMSMsg').innerHTML="Select Minimum Segment"                          
                         return false;
                       }                       
                    }
        }  
            catch(err){alert(err)}                 
                    
    }
    
       
    function   SelectItemFromMSToModalList()
    {
       try{
        //  debugger;
               var chkboxMS =document.getElementById("ChkMinimunCriteriaNew");
               var chkboxMSMore =document.getElementById("chkLstMSMore");
                 if( chkboxMS !=null && chkboxMSMore!=null )
                    {
                        var inputArrMS = chkboxMS.getElementsByTagName('input');
                        var labelArrMS = chkboxMS.getElementsByTagName('label');
                        
                         var inputArrMSMore = chkboxMSMore.getElementsByTagName('input');
                         var labelArrMSMore = chkboxMSMore.getElementsByTagName('label');
                         var intLengthMS=0;
                         var intRowsMS=0;
                         var intLengthMSMore=0;
                         var intRowsMSMore=0;
                         intLengthMSMore =inputArrMSMore.length;
                         intLengthMS=inputArrMS.length;
                           for(CounterMSMore=0;CounterMSMore<intLengthMSMore;CounterMSMore++)
                                 {
                                      for(CounterMS=0;CounterMS<intLengthMS;CounterMS++)
                                     {
                                         var elementIDMSMore='chkLstMSMore_'+ CounterMSMore;
                                         var elementIDMS='ChkMinimunCriteriaNew_'+ CounterMS;
                                          if(labelArrMS[CounterMS].innerHTML.toUpperCase()== labelArrMSMore[CounterMSMore].innerHTML.toUpperCase())
                                          {
                                               if(document.getElementById(elementIDMS).checked==true)
                                               {  
                                                 document.getElementById(elementIDMSMore).checked=true;  
                                               }  
                                               else
                                               {
                                                  document.getElementById(elementIDMSMore).checked=false;
                                               } 
                                          }
                                     }
                                 }
                         
                         
                   }
               
         }    
            catch(err){alert(err)}
      
    }
    
    function   SelectItemFromQualToModalList()
    {
       try{
         // debugger;
               var chkboxSQ =document.getElementById("chkSLABQUALIFICATION");
               var chkboxSQMore =document.getElementById("chkLstSQMore");
                 if( chkboxSQ !=null && chkboxSQMore!=null )
                    {
                        var inputArrSQ = chkboxSQ.getElementsByTagName('input');
                        var labelArrSQ = chkboxSQ.getElementsByTagName('label');
                        
                         var inputArrSQMore = chkboxSQMore.getElementsByTagName('input');
                         var labelArrSQMore = chkboxSQMore.getElementsByTagName('label');
                         var intLengthSQ=0;
                         var intRowsSQ=0;
                         var intLengthSQMore=0;
                         var intRowsSQMore=0;
                         intLengthSQMore =inputArrSQMore.length;
                         intLengthSQ=inputArrSQ.length;
                           for(CounterSQMore=0;CounterSQMore<intLengthSQMore;CounterSQMore++)
                                 {
                                      for(CounterSQ=0;CounterSQ<intLengthSQ;CounterSQ++)
                                     {
                                         var elementIDSQMore='chkLstSQMore_'+ CounterSQMore;
                                         var elementIDSQ='chkSLABQUALIFICATION_'+ CounterSQ;
                                          if(labelArrSQ[CounterSQ].innerHTML.toUpperCase()== labelArrSQMore[CounterSQMore].innerHTML.toUpperCase())
                                          {
                                               if(document.getElementById(elementIDSQ).checked==true)
                                               {  
                                                 document.getElementById(elementIDSQMore).checked=true;  
                                               }
                                               else
                                               {
                                                 document.getElementById(elementIDSQMore).checked=false; 
                                               }   
                                          }
                                     }
                                 }
                         
                         
                   }
               
         }    
            catch(err){alert(err)}
      
    }
    
      function CheckOrUnckeckItemFromQlaificationModal()
            {
                try
                {
               
                    var Counter=0;
                    var strProductivity="0";
                    var intLength=0;
                    var intRows=0;
                    var intTotalSelection=0;
                    var chkbox =document.getElementById("chkLstSQMore");
                    if( chkbox !=null)
                    {
                        var inputArr = chkbox.getElementsByTagName('input');
                        var labelArr = chkbox.getElementsByTagName('label');
                        if( inputArr !=null)
                            {
                                 intLength=inputArr.length;   
                                 for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID='chkLstSQMore_'+ Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                         intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                            {
                                                  strProductivity="1" ;                                
                                            }
                                     }
                                 }   
                                         
                                if (strProductivity=="1")
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                           var elementID='chkLstSQMore_'+ Counter;   
                                            if (labelArr[Counter].innerHTML.toUpperCase()!="PRODUCTIVITY")
                                            {
                                                  document.getElementById(elementID).disabled=true;
                                                  document.getElementById(elementID).checked=false;
                                            }
                                            else
                                            {
                                             document.getElementById(elementID).disabled=false;
                                            }                       
                                    }
                                }
                                else
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                        var elementID='chkLstSQMore_'+ Counter;                                       
                                       
                                        if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                            {
                                                  document.getElementById(elementID).disabled=true;
                                                  document.getElementById(elementID).checked=false;                                
                                            }
                                        else
                                            {
                                                  document.getElementById(elementID).disabled=false;   
                                            }
                                    }
                                } 
                                                                  
                                if(intTotalSelection==0)
                                {                      
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                           var elementID='chkLstSQMore_'+ Counter;                               
                                           document.getElementById(elementID).disabled=false;
                                     }
                                }                
                        }                                                                           
                    }
               
                 }    
            catch(err){alert(err)}
         }
    
    
      function CheckOrUnckeckItemFromMinimunCriteria(ID)
            {
                try
                {
                  //alert("abh");
                    var Counter=0;
                    var strProductivity="0";
                    var strHL="0";
                    var strROI="0";
                    var str9W=0;
                    var intLength=0;
                    var intRows=0;
                    var intTotalSelection=0;
                    var chkbox =document.getElementById(ID);
                    if( chkbox !=null)
                    {
                        var inputArr = chkbox.getElementsByTagName('input');
                        var labelArr = chkbox.getElementsByTagName('label');
                        if( inputArr !=null)
                            {
                                 intLength=inputArr.length;   
                                 for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID=ID + "_" + Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                         intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                            {
                                                  strProductivity="1" ;                                
                                            }
                                     }
                                 }  
                                 
                                   for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID=ID  + "_"  + Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                        // intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="HL")
                                            {
                                                  strHL="1" ;                                
                                            }
                                     }
                                 }   
                                     
                                     
                                       for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID=ID  + "_"  + Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                        // intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="ROI")
                                            {
                                                  strROI="1" ;                                
                                            }
                                     }
                                 }   
                                     
                                     
                                       for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID=ID  + "_"  + Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                        // intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="9W DOM")
                                            {
                                                  str9W="1" ;                                
                                            }
                                     }
                                 }   
                                      
                                         
                                if (strProductivity=="1" )
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                           var elementID=ID  + "_"  + Counter;   
                                            if (labelArr[Counter].innerHTML.toUpperCase()!="PRODUCTIVITY"  && labelArr[Counter].innerHTML.toUpperCase()!="HL")
                                            {
                                                  document.getElementById(elementID).disabled=true;
                                                  document.getElementById(elementID).checked=false;
                                            }
                                          
                                            else
                                            {
                                             document.getElementById(elementID).disabled=false;
                                            }                       
                                    }
                                }
                                
                                else if (strHL=="1" )
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                           var elementID=ID  + "_"  + Counter;   
                                            if (labelArr[Counter].innerHTML.toUpperCase()!="PRODUCTIVITY"  && labelArr[Counter].innerHTML.toUpperCase()!="HL")
                                            {
                                                    if (strProductivity=="1" )
                                                     {
                                                          document.getElementById(elementID).disabled=true;
                                                          document.getElementById(elementID).checked=false;
                                                     }
                                                     else
                                                     {
                                                     // document.getElementById(elementID).disabled=false;
                                                     
                                                       if (labelArr[Counter].innerHTML.toUpperCase()=="ROI")
                                                        {
                                                               if (str9W=="1" )
                                                                   {
                                                                    document.getElementById(elementID).disabled=false;   
                                                                   }
                                                                   else
                                                                   {
                                                                      document.getElementById(elementID).disabled=true;
                                                                      document.getElementById(elementID).checked=false;       
                                                                   }
                                                        }
                                                        else
                                                        {
                                                          document.getElementById(elementID).disabled=false;   
                                                        }                                                      
                                                     
                                                     }
                                            }
                                          
                                            else
                                            {
                                                     if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                                     {
                                                              if (strProductivity=="1" )
                                                             {
                                                                  document.getElementById(elementID).disabled=false; 
                                                              }
                                                             else
                                                             {
                                                                 if(intTotalSelection>1)
                                                                 {
                                                                       document.getElementById(elementID).disabled=true;
                                                                       document.getElementById(elementID).checked=false; 
                                                                 }
                                                                 else
                                                                 {
                                                                   document.getElementById(elementID).disabled=false;
                                                                 }
                                                             }                                                     
                                                     }
                                                     else
                                                     {
                                                       document.getElementById(elementID).disabled=false;
                                                     }
                                               
                                            }                       
                                    }
                                }
                                
                                else
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                        var elementID=ID  + "_"  + Counter;                                       
                                      
                                        if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                            {
                                                  document.getElementById(elementID).disabled=true;
                                                  document.getElementById(elementID).checked=false;                                
                                            }
                                        else
                                            {                                              
                                                if (labelArr[Counter].innerHTML.toUpperCase()=="ROI")
                                                {
                                                       if (str9W=="1" )
                                                           {
                                                           
                                                            document.getElementById(elementID).disabled=false;   
                                                           }
                                                           else
                                                           { 
                                                              document.getElementById(elementID).disabled=true;
                                                              document.getElementById(elementID).checked=false;       
                                                           }
                                                }
                                                else
                                                {
                                                  document.getElementById(elementID).disabled=false;   
                                                }  
                                            }
                                    }
                                } 
                                                                  
                                if(intTotalSelection==0)
                                {                      
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                             var elementID=ID  + "_"  +  Counter; 
                                           
                                             if (labelArr[Counter].innerHTML.toUpperCase()=="ROI")
                                                 { document.getElementById(elementID).disabled=true;
                                                    document.getElementById(elementID).checked=false; 
                                                  }      
                                              
                                                else
                                                {
                                                  document.getElementById(elementID).disabled=false;   
                                                }                                                                          
                                          
                                     }
                                }                
                        }                                                                           
                    }
               
                 }    
            catch(err){alert(err)}
         }
         
         
         
      function CheckOrUnckeckItemFromMinimunCriteriaPrev()
            {
                try
                {
                  //alert("abh");
                    var Counter=0;
                    var strProductivity="0";
                    var strHL="0";
                    var strROI="0";
                    var str9W=0;
                    var intLength=0;
                    var intRows=0;
                    var intTotalSelection=0;
                    var chkbox =document.getElementById("ChkMinimunCriteriaNew");
                    if( chkbox !=null)
                    {
                        var inputArr = chkbox.getElementsByTagName('input');
                        var labelArr = chkbox.getElementsByTagName('label');
                        if( inputArr !=null)
                            {
                                 intLength=inputArr.length;   
                                 for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID='ChkMinimunCriteriaNew_'+ Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                         intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                            {
                                                  strProductivity="1" ;                                
                                            }
                                     }
                                 }  
                                 
                                   for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID='ChkMinimunCriteriaNew_'+ Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                        // intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="HL")
                                            {
                                                  strHL="1" ;                                
                                            }
                                     }
                                 }   
                                     
                                     
                                       for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID='ChkMinimunCriteriaNew_'+ Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                        // intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="ROI")
                                            {
                                                  strROI="1" ;                                
                                            }
                                     }
                                 }   
                                     
                                     
                                       for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID='ChkMinimunCriteriaNew_'+ Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                        // intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="9W DOM")
                                            {
                                                  str9W="1" ;                                
                                            }
                                     }
                                 }   
                                      
                                         
                                if (strProductivity=="1" )
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                           var elementID='ChkMinimunCriteriaNew_'+ Counter;   
                                            if (labelArr[Counter].innerHTML.toUpperCase()!="PRODUCTIVITY"  && labelArr[Counter].innerHTML.toUpperCase()!="HL")
                                            {
                                                  document.getElementById(elementID).disabled=true;
                                                  document.getElementById(elementID).checked=false;
                                            }
                                          
                                            else
                                            {
                                             document.getElementById(elementID).disabled=false;
                                            }                       
                                    }
                                }
                                
                                else if (strHL=="1" )
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                           var elementID='ChkMinimunCriteriaNew_'+ Counter;   
                                            if (labelArr[Counter].innerHTML.toUpperCase()!="PRODUCTIVITY"  && labelArr[Counter].innerHTML.toUpperCase()!="HL")
                                            {
                                                    if (strProductivity=="1" )
                                                     {
                                                          document.getElementById(elementID).disabled=true;
                                                          document.getElementById(elementID).checked=false;
                                                     }
                                                     else
                                                     {
                                                     // document.getElementById(elementID).disabled=false;
                                                     
                                                       if (labelArr[Counter].innerHTML.toUpperCase()=="ROI")
                                                        {
                                                               if (str9W=="1" )
                                                                   {
                                                                    document.getElementById(elementID).disabled=false;   
                                                                   }
                                                                   else
                                                                   {
                                                                      document.getElementById(elementID).disabled=true;
                                                                      document.getElementById(elementID).checked=false;       
                                                                   }
                                                        }
                                                        else
                                                        {
                                                          document.getElementById(elementID).disabled=false;   
                                                        }                                                      
                                                     
                                                     }
                                            }
                                          
                                            else
                                            {
                                                     if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                                     {
                                                              if (strProductivity=="1" )
                                                             {
                                                                  document.getElementById(elementID).disabled=false; 
                                                              }
                                                             else
                                                             {
                                                                 if(intTotalSelection>1)
                                                                 {
                                                                       document.getElementById(elementID).disabled=true;
                                                                       document.getElementById(elementID).checked=false; 
                                                                 }
                                                                 else
                                                                 {
                                                                   document.getElementById(elementID).disabled=false;
                                                                 }
                                                             }                                                     
                                                     }
                                                     else
                                                     {
                                                       document.getElementById(elementID).disabled=false;
                                                     }
                                               
                                            }                       
                                    }
                                }
                                
                                else
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                        var elementID='ChkMinimunCriteriaNew_'+ Counter;                                       
                                      
                                        if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                            {
                                                  document.getElementById(elementID).disabled=true;
                                                  document.getElementById(elementID).checked=false;                                
                                            }
                                        else
                                            {                                              
                                                if (labelArr[Counter].innerHTML.toUpperCase()=="ROI")
                                                {
                                                       if (str9W=="1" )
                                                           {
                                                           
                                                            document.getElementById(elementID).disabled=false;   
                                                           }
                                                           else
                                                           { 
                                                              document.getElementById(elementID).disabled=true;
                                                              document.getElementById(elementID).checked=false;       
                                                           }
                                                }
                                                else
                                                {
                                                  document.getElementById(elementID).disabled=false;   
                                                }  
                                            }
                                    }
                                } 
                                                                  
                                if(intTotalSelection==0)
                                {                      
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                             var elementID='ChkMinimunCriteriaNew_'+ Counter; 
                                           
                                             if (labelArr[Counter].innerHTML.toUpperCase()=="ROI")
                                                 { document.getElementById(elementID).disabled=true;
                                                    document.getElementById(elementID).checked=false; 
                                                  }      
                                              
                                                else
                                                {
                                                  document.getElementById(elementID).disabled=false;   
                                                }                                                                          
                                          
                                     }
                                }                
                        }                                                                           
                    }
               
                 }    
            catch(err){alert(err)}
         }
    
    
    
    
    
//Script Section for New Change in December
function makeSelection()
{

try
{
var eleCounter=0;
var strProductivity="0";
var intLength=0;
var intRows=0;
var intTotalSelection=0;
// hdEnableDisableCase



intRows=document.getElementById("chklstCriteria").rows.length;

if (intRows>0)
{
    for(i=0;i<intRows;i++)
    {
        intLength=intLength +  document.getElementById("chklstCriteria").rows(i).cells.length;
    }
}


    for(eleCounter=0;eleCounter<intLength-1;eleCounter++)
    {
        var elementID='chklstCriteria_'+ eleCounter;
           
        if(document.getElementById(elementID).checked==true)
        {
        
            intTotalSelection+=1;
        
            if (document.getElementById(elementID).parentElement.innerText.toUpperCase()=="PRODUCTIVITY")
            {
                  strProductivity="1" 
                    
            }
        }
    }
    
   

    
    
    if (strProductivity=="1")
    {
        for(eleCounter=0;eleCounter<intLength-1;eleCounter++)
        {
            var elementID='chklstCriteria_'+ eleCounter;
               
               if (document.getElementById(elementID).parentElement.innerText.toUpperCase()!="PRODUCTIVITY")
                {
                      document.getElementById(elementID).disabled=true;
                      document.getElementById(elementID).checked=false;
                        
                }
           
        }
    }
    else
    {
        for(eleCounter=0;eleCounter<intLength-1;eleCounter++)
    {
        var elementID='chklstCriteria_'+ eleCounter;
           
             if (document.getElementById(elementID).parentElement.innerText.toUpperCase()=="PRODUCTIVITY")
            {
                  document.getElementById(elementID).disabled=true;
                  document.getElementById(elementID).checked=false;
                    
            }
            else
            {
                  document.getElementById(elementID).disabled=false;
                
                
            }
        
    }
    }
    
     //For Enabling all Checkboxes
    if(intTotalSelection==0)
    {
        for(eleCounter=0;eleCounter<intLength-1;eleCounter++)
        {
            var elementID='chklstCriteria_'+ eleCounter;
               
               document.getElementById(elementID).disabled=false;
         }
    }
    //For Enabling all Checkboxes
    }
    catch(err){}
    
    
}
//End of  Script Section for New Change in December

//Script Section for New Change in December
function makeSelectionSLABQUALIFICATION()
{

try
{
var eleCounter=0;
var strProductivity="0";
var intLength=0;
var intRows=0;
var intTotalSelection=0;
// hdEnableDisableCase




      
        

//intRows=document.getElementById("chkSLABQUALIFICATION").rows.length;

//if (intRows>0)
//{
//    for(i=0;i<intRows;i++)
//    {
//        intLength=intLength +  document.getElementById("chkSLABQUALIFICATION").rows(i).cells.length;
//    }
//}
intLength=0;
var chkBoxList = document.getElementById("chkSLABQUALIFICATION");
        var chkBoxCount= chkBoxList.getElementsByTagName("input");
        for(var i=0;i<chkBoxCount.length;i++)
        {
            intLength=intLength + 1;
        }
       

    for(eleCounter=0;eleCounter<=intLength-1;eleCounter++)
    {
        var elementID='chkSLABQUALIFICATION_'+ eleCounter;
           
        if(document.getElementById(elementID).checked==true)
        {
        
            intTotalSelection+=1;
        
            if (document.getElementById(elementID).parentElement.innerText.toUpperCase()=="PRODUCTIVITY")
            {
                  strProductivity="1" 
                    
            }
        }
    }
    
   

    
    
    if (strProductivity=="1")
    {
        for(eleCounter=0;eleCounter<=intLength-1;eleCounter++)
        {
            var elementID='chkSLABQUALIFICATION_'+ eleCounter;
               
               if (document.getElementById(elementID).parentElement.innerText.toUpperCase()!="PRODUCTIVITY")
                {
                      document.getElementById(elementID).disabled=true;
                      document.getElementById(elementID).checked=false;
                        
                }
           
        }
    }
    else
    {
        for(eleCounter=0;eleCounter<=intLength-1;eleCounter++)
    {
        var elementID='chkSLABQUALIFICATION_'+ eleCounter;
           
             if (document.getElementById(elementID).parentElement.innerText.toUpperCase()=="PRODUCTIVITY")
            {
                  document.getElementById(elementID).disabled=true;
                  document.getElementById(elementID).checked=false;
                    
            }
            else
            {
                  document.getElementById(elementID).disabled=false;
                
                
            }
        
    }
    }
    
     //For Enabling all Checkboxes
    if(intTotalSelection==0)
    {
        for(eleCounter=0;eleCounter<=intLength-1;eleCounter++)
        {
            var elementID='chkSLABQUALIFICATION_'+ eleCounter;
               
               document.getElementById(elementID).disabled=false;
         }
    }
    //For Enabling all Checkboxes
    }
    catch(err){}
    
    
}

//End of  Script Section for New Change in December



//**************************************************
//Script Section for New Change in December
function makeSelection1()
{
//{debugger;}
try
{
var eleCounter=0;
var strProductivity="0";
var intLength=0;
var intRows=0;
var intTotalSelection=0;
// hdEnableDisableCase



intRows=document.getElementById("chkLstGvShowMIDT").rows.length;

if (intRows>0)
{
    for(i=0;i<intRows;i++)
    {
        intLength=intLength +  document.getElementById("chkLstGvShowMIDT").rows(i).cells.length;
    }
}


    for(eleCounter=0;eleCounter<intLength-1;eleCounter++)
    {
        var elementID='chkLstGvShowMIDT_'+ eleCounter;
           
        if(document.getElementById(elementID).checked==true)
        {
        
            intTotalSelection+=1;
        
            if (document.getElementById(elementID).parentElement.innerText.toUpperCase()=="PRODUCTIVITY")
            {
                  strProductivity="1" 
                    
            }
        }
    }
    
   

    
    
    if (strProductivity=="1")
    {
        for(eleCounter=0;eleCounter<intLength-1;eleCounter++)
        {
            var elementID='chkLstGvShowMIDT_'+ eleCounter;
               
               if (document.getElementById(elementID).parentElement.innerText.toUpperCase()!="PRODUCTIVITY")
                {
                      document.getElementById(elementID).disabled=true;
                      document.getElementById(elementID).checked=false;
                        
                }
           
        }
    }
    else
    {
        for(eleCounter=0;eleCounter<intLength-1;eleCounter++)
    {
        var elementID='chkLstGvShowMIDT_'+ eleCounter;
           
             if (document.getElementById(elementID).parentElement.innerText.toUpperCase()=="PRODUCTIVITY")
            {
                  document.getElementById(elementID).disabled=true;
                  document.getElementById(elementID).checked=false;
                    
            }
            else
            {
                  document.getElementById(elementID).disabled=false;
                
                
            }
        
    }
    }
    
     //For Enabling all Checkboxes
    if(intTotalSelection==0)
    {
        for(eleCounter=0;eleCounter<intLength-1;eleCounter++)
        {
            var elementID='chkLstGvShowMIDT_'+ eleCounter;
               
               document.getElementById(elementID).disabled=false;
         }
    }
    //For Enabling all Checkboxes
    
    }
    catch(err){}
    
}
//End of  Script Section for New Change in December
//**************************************************


function showPreview()
{

    var type = "INC_POP_Airline_Preview.aspx";
   	window.open(type,"aaChallanProductListPopup","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	               	    
    return false;

}

function history()
{
    var type = "INC_BC_History.aspx?BC_ID=" + document.getElementById("hdBcID").value;
   	window.open(type,"INC_BC_History","height=600,width=650,top=30,left=20,scrollbars=1,status=1");     	               	    
    return false;
}

function ShoImageRotaion(img2)
 
 {
      document.getElementById(img2).className ="displayBlock"  ; 
 }
 
 
 
    function incentiveType()
    {

        var incTypeVal=GetCheckedVal('rdbIncentiveType');  // document.getElementById("rdbIncentiveType").options[document.getElementById("rdbIncentiveType").selectedIndex].value;
         var incTypeVal_Upfront=GetCheckedVal('rdbUpfrontTypeName'); 
         document.getElementById("trForThePeriodOf").className="displayNone";
         document.getElementById("trNoOfPayments").className="displayNone";
         
         document.getElementById("TRMinimumSegmentCriteriaNew").className="displayNone";
         document.getElementById("TblMinimumSegmentCriteriaNew").className="displayNone";
         
        if (incTypeVal=="1")
        {
            document.getElementById("trUpfrontTypeName").className="displayBlock";
            document.getElementById("trUpfrontAmount").className="displayBlock";
            
            document.getElementById("TRMinimumSegmentCriteriaNew").className="displayNone";
            document.getElementById("TblMinimumSegmentCriteriaNew").className="displayNone";  
            
            
            if (incTypeVal_Upfront=="1")
            {
                document.getElementById("trForThePeriodOf").className="displayBlock";
                            
            }
            if (incTypeVal_Upfront=="3")
            {
               document.getElementById("trNoOfPayments").className="displayBlock";
                
            }
        }
        else
        {
            document.getElementById("trUpfrontTypeName").className="displayNone";
            document.getElementById("trUpfrontAmount").className="displayNone";   
            
            document.getElementById("TRMinimumSegmentCriteriaNew").className="displayBlock";
            document.getElementById("TblMinimumSegmentCriteriaNew").className="redborder displayBlock";         
        }
        
    }
    
    
    function UpfrontTypeName()
    {

        var incTypeVal=GetCheckedVal('rdbIncentiveType');  
         var incTypeVal_Upfront=GetCheckedVal('rdbUpfrontTypeName'); 
         document.getElementById("trForThePeriodOf").className="displayNone";
         document.getElementById("trNoOfPayments").className="displayNone";
        if (incTypeVal=="1")
        {
            if (incTypeVal_Upfront=="1")
            {
                document.getElementById("trForThePeriodOf").className="displayBlock";
                            
            }
            if (incTypeVal_Upfront=="3")
            {
               document.getElementById("trNoOfPayments").className="displayBlock";
                
            }
        }
        

    }
    

    function bonusCheck()
    {
        
        if (document.getElementById("chkPlb").checked==true)
        {
            document.getElementById("trPlbTypeName").className="displayBlock"
            document.getElementById("trSlabPlb").className="displayBlock"
        }
        else
        {
            document.getElementById("trPlbTypeName").className="displayNone"
            document.getElementById("trSlabPlb").className="displayNone"
        }
        
        plb_paymenttypeFixed_Bonus();
        
        
        
    }
    
    function plb_paymenttypeFixed_Bonus()
    {
 //   {debugger;}
        if (document.getElementById("chkPlb").checked==true)
        {
        var plbTypeVal=GetCheckedVal('rdbPlbTypeName'); //document.getElementById("rdbPlbTypeName").options[document.getElementById("rdbPlbTypeName").selectedIndex].value;
        if(plbTypeVal=='2')
        {
        document.getElementById("trSlabPlb").className="displayBlock"
        document.getElementById("trPlbAmount").className="displayNone"
        
        }
        else
        {
            document.getElementById("trSlabPlb").className="displayNone"
            document.getElementById("trPlbAmount").className="displayBlock"
            
            
            
        }
      }
      else
      {
            document.getElementById("trSlabPlb").className="displayNone"
            document.getElementById("trPlbAmount").className="displayNone"
      }
    
    }
    
    function displayFirstTime()
    {
    //Code for maintaing PLB 
    plb_paymenttypeFixed_Bonus();
    //End of Code for maintaing PLB 
    
    }



// Code for getting Checked Value


function GetCheckedVal(radioID)
{
//{debugger;}
var a = null;
var f = document.forms[0];
var e = f.elements[radioID];
if (e != null)
    {
        for (var i=0; i < e.length; i++)
        {
            if (e[i].checked)
            {
                a = e[i].value;
                break;
            }
        }
        return a;
    }
}

function PaymentType_Rate_Fixed()
{
 var incTypeVal=GetCheckedVal('rdbPaymentType');  

    if(incTypeVal=='1')
    {
        document.getElementById("spnAdjAmount").innerText="Rate";
         if(document.getElementById('grdvSlabN')!=null)
         {
            document.getElementById('grdvSlabN').rows[0].cells[2].children[0].innerText="Rate";
         }
         if(document.getElementById('grdvIncentivePlan')!=null)
         {
         
            for(intcnt=1;intcnt<=document.getElementById('grdvIncentivePlan').rows.length;intcnt++)
            {   
                try
                {   
                    document.getElementById('grdvIncentivePlan_ctl0' + (intcnt+1) +'_GvIncSlabsNested_ctl01_spnAdjAmount').innerText="Rate";
                }
                catch(err){}
            }
         }
    }
    else
    {
        document.getElementById("spnAdjAmount").innerText="Amount";
        if(document.getElementById('grdvSlabN')!=null)
         {
            document.getElementById('grdvSlabN').rows[0].cells[2].children[0].innerText="Amount";
         }
         if(document.getElementById('grdvIncentivePlan')!=null)
         {
         
            for(intcnt=1;intcnt<=document.getElementById('grdvIncentivePlan').rows.length;intcnt++)
            {   
                try
                {   
                    document.getElementById('grdvIncentivePlan_ctl0' + (intcnt+1) +'_GvIncSlabsNested_ctl01_spnAdjAmount').innerText="Amount";
                }
                catch(err){}
            }
         }
    }
}
// Code for getting Checked Value



//*******Old Deal Display****************
function incentiveTypeOld()
    {

        var incTypeVal=GetCheckedVal('rdbIncentiveTypeOld');  // document.getElementById("rdbIncentiveType").options[document.getElementById("rdbIncentiveType").selectedIndex].value;
        
        if (incTypeVal=="1")
        {
            document.getElementById("trUpfrontTypeNameOld").className="displayBlock";
            document.getElementById("trUpfrontAmountOld").className="displayBlock";
        }
        else
        {
            document.getElementById("trUpfrontTypeNameOld").className="displayNone";
            document.getElementById("trUpfrontAmountOld").className="displayNone";
        }

    }

    function bonusCheckOld()
    {
        
        if (document.getElementById("chkPlbOld").checked==true)
        {
            document.getElementById("trPlbTypeNameOld").className="displayBlock"
            document.getElementById("trSlabPlbOld").className="displayBlock"
        }
        else
        {
            document.getElementById("trPlbTypeNameOld").className="displayNone"
            document.getElementById("trSlabPlbOld").className="displayNone"
        }
        
        plb_paymenttypeFixed_BonusOld();
        
        
        
    }
    
    function plb_paymenttypeFixed_BonusOld()
    {
   // {debugger;}
        if (document.getElementById("chkPlbOld").checked==true)
        {
        var plbTypeVal=GetCheckedVal('rdbPlbTypeNameOld'); //document.getElementById("rdbPlbTypeName").options[document.getElementById("rdbPlbTypeName").selectedIndex].value;
        if(plbTypeVal=='2')
        {
        document.getElementById("trSlabPlbOld").className="displayBlock"
        document.getElementById("trPlbAmountOld").className="displayNone"
        
        }
        else
        {
            document.getElementById("trSlabPlbOld").className="displayNone"
            document.getElementById("trPlbAmountOld").className="displayBlock"
            
            
            
        }
      }
      else
      {
            document.getElementById("trSlabPlbOld").className="displayNone"
            document.getElementById("trPlbAmountOld").className="displayNone"
      }
    
    }
   
//End of *********Old Deal Display*********

//End of Script Section for New Change in December
    </script>

    <script type="text/javascript" language="javascript"> 
    var grandTotalConn='0';
    var grandTotalHw='0';
    
    function getCalender()
    {
     Calendar.setup({
     inputField     :    '<%=txtPeriodFrom.ClientId%>',
     ifFormat       :     "%d/%m/%Y",
     button         :    "Img1",
     //align          :    "Tl",
     singleClick    :    true
     });
    }
    
    function getCalenderTo()
    {
       Calendar.setup({
      inputField     :    '<%=txtPeriodTo.ClientId%>',
      ifFormat       :     "%d/%m/%Y",
      button         :    "Img2",
      //align          :    "Tl",
      singleClick    :    true
      });
    }
    
     function ShowModPopExtForSQ(ID)
    {
         try
         {
                SelectItemFromQualToModalList();
                document.getElementById('LBSQMsg').innerHTML="";
         } catch(err){alert(err)} 
        var modal = $find('ModPExtForQual');  
        modal.show();
        return false;
        
           
    }
      function ShowModPopExtForMS(ID)
    {
          try
         {
                SelectItemFromMSToModalList();
                document.getElementById('LblMSMsg').innerHTML="";
         } catch(err){alert(err)} 
        var modal = $find('ModPExtForMS');  
        modal.show();
        return false;
    }
    
    
  function ShowModalPopupExtenderForTrainRoute(ID)
        {
      //  {debugger;}
                var modal = $find('ModalPopupExtender2');  
                modal.show();
                return false;
       }
       
       function ShowModalPopupExtenderForTrainRoute1(ID)
        {
                var modal = $find('ModalForGvSelection');  
                modal.show();
        }
        
        function HideModalPopupExtenderForGridviewModalpopup()
            {
              var modal = $find('ModalForGvSelection');
              modal.hide();
                return false;
              
            }
            
            
       
        function HideModalPopupExtenderForTrainRoutePopup()
            {
              var modal = $find('ModalPopupExtender2');
              modal.hide();
                return false;
              
            }
            
       function showModalPopupExtenderPreview()
        {
        
                var modal = $find('ModalPopupExtender3');  
                modal.show();
                return false;
       }
       
              
        function HideModalPopupExtenderPreview()
            {
              var modal = $find('ModalPopupExtender3');
              modal.hide();
                return false;
              
            }
       
       
       function loaddata() 

{

   Sys.WebForms.PageRequestManager.getInstance().add_endRequest(tabSelection);
  // Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CheckOrUnckeckItemFromMinimunCriteria);

}


   
    </script>

</head>
<body onload="return loaddata();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <table width="1000px" class="border_rightred left">
            <tr>
                <td class="top" width="1000px">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width: 80%">
                                            <span class="menu">Incentive-></span><span class="sub_menu"> Bussiness Case </span>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width: 100%">
                                            Manage Bussiness Case
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" onload='tabSelection();' cellpadding="0">
                                    <tr>
                                        <td align="right">
                                            <table cellpadding="0" cellspacing="1" border="0" style="vertical-align: top; width: 30%;">
                                                <tr align="right">
                                                    <td>
                                                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/btn_Save.jpg" AccessKey="N"
                                                            TabIndex="3" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="btnSendforApproval" ImageUrl="~/Images/Btn_SendForApprovel.jpg"
                                                            runat="server" TabIndex="3" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="btnRefresh" ImageUrl="~/Images/btn_Refresh.jpg" runat="server"
                                                            TabIndex="3" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="btnReset" ImageUrl="~/Images/btn_Reset.jpg" runat="server" TabIndex="3" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="BtnApproved" ImageUrl="~/Images/Btn_Approved.jpg" runat="server"
                                                            TabIndex="3" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="BtnReject" runat="server" ImageUrl="~/Images/btn_Reject.jpg"
                                                            TabIndex="3" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="BtnFinnallyApproved" runat="server" AccessKey="r" ImageUrl="~/Images/btn_FinallyApproved.jpg" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="btnHistory" runat="server" ImageUrl="~/Images/btn_History.jpg" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="top">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="lblSubPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px; width: 100%" colspan="2">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" Width="100px" CssClass="headingtabactive" runat="server"
                                                        Text="<%# Container.DataItem %>" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top" style="width: 830px; padding-left: 7px; padding-bottom: 7px;">
                                                        <asp:UpdatePanel ID="updtIncentivePlan" runat="server">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td valign="top" style="height: 5px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="top" style="height: 22px; width: 100%" colspan="2">
                                                                            <asp:Repeater ID="theTabSubStrip" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="Button2" Width="100px" CssClass="headingtabactive" runat="server"
                                                                                        CommandName="Tab" Text="<%# Container.DataItem %>" />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                            &nbsp; &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="redborder top" style="width: 100%;">
                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="center TOP" style="width: 830px;">
                                                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="top" style="width: 100%;">
                                                                                        <table width="1000" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                            <tr>
                                                                                                <td style="width: 1186px">
                                                                                                    <asp:Panel ID="pnlDetails" runat="server" CssClass="displayNone">
                                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Panel ID="PnlAgencyDetails" runat="server" Width="100%" Visible="true">
                                                                                                                        <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                                                                                                            <tr>
                                                                                                                                <td class="subheading" colspan="6">
                                                                                                                                    Group Details</td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="textbold">
                                                                                                                                    Group Name</td>
                                                                                                                                <td colspan="4">
                                                                                                                                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                        TabIndex="3" Width="631px" ReadOnly="True"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td style="width: 118px;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="textbold">
                                                                                                                                    Region</td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtRegion" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                        ReadOnly="True" TabIndex="3" Width="206px"></asp:TextBox></td>
                                                                                                                                <td>
                                                                                                                                </td>
                                                                                                                                <td class="textbold">
                                                                                                                                    Chain Code</td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                        ReadOnly="True" TabIndex="3" Width="198px"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="textbold" style="height: 25px">
                                                                                                                                    Account Manager</td>
                                                                                                                                <td style="height: 25px">
                                                                                                                                    <asp:TextBox ID="txtActManager" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                        ReadOnly="true" TabIndex="3" Width="203px"></asp:TextBox></td>
                                                                                                                                <td style="height: 25px">
                                                                                                                                </td>
                                                                                                                                <td style="height: 25px">
                                                                                                                                    Billing Cycle</td>
                                                                                                                                <td class="textbold" style="height: 25px">
                                                                                                                                    <asp:DropDownList ID="drpBillingCycle" runat="server" CssClass="textbold" onkeyup="gotop(this.id)"
                                                                                                                                        TabIndex="1" Width="201px">
                                                                                                                                    </asp:DropDownList></td>
                                                                                                                                <td valign="top" style="height: 25px">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td colspan="5" style="height: 17px">
                                                                                                                                </td>
                                                                                                                                <td style="height: 17px">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </asp:Panel>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="subheading" align="center">
                                                                                                                    Group MIDT(Average)</td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height: 13px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <table border="0" cellspacing="0" cellpadding="0" width="1000">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="1000">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <table border="0" cellspacing="0" cellpadding="1" width="1000">
                                                                                                                                                <tr class="Gridheading">
                                                                                                                                                    <td width="10%">
                                                                                                                                                        Last Available</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1A</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1B</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1G</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1P</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1W</td>
                                                                                                                                                    <td width="10%">
                                                                                                                                                        Total</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1A%</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1B%</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1G%</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1P%</td>
                                                                                                                                                    <td width="8%">
                                                                                                                                                        1W%</td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <asp:Panel ID="pnlAmadeus" runat="server" Width="1000px">
                                                                                                                                                <asp:GridView ID="GvBGroupMIDT" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                    ShowHeader="false" Width="1000px" EnableViewState="true" AllowSorting="false">
                                                                                                                                                    <Columns>
                                                                                                                                                        <asp:BoundField HeaderText="Last Available" DataField="LASTAVAIL" SortExpression="LASTAVAIL"
                                                                                                                                                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-CssClass="left"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1A" DataField="A" SortExpression="A" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1B" DataField="B" SortExpression="B" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1G" DataField="G" SortExpression="G" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1P" DataField="P" SortExpression="P" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1W" DataField="W" SortExpression="W" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="Total" DataField="TOTAL" SortExpression="TOTAL" HeaderStyle-Width="10%"
                                                                                                                                                            ItemStyle-Width="10%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1A %" DataField="A_PER" SortExpression="APER" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1B %" DataField="B_PER" SortExpression="BPER" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1G %" DataField="G_PER" SortExpression="GPER" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1P %" DataField="P_PER" SortExpression="PPER" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1W %" DataField="W_PER" SortExpression="WPER" HeaderStyle-Width="8%"
                                                                                                                                                            ItemStyle-Width="8%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                    </Columns>
                                                                                                                                                    <AlternatingRowStyle CssClass="lightblue right" />
                                                                                                                                                    <RowStyle CssClass="textbold right" />
                                                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                                                    <FooterStyle CssClass="Gridheading" />
                                                                                                                                                </asp:GridView>
                                                                                                                                            </asp:Panel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="height: 10pt;">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td class="subheading" align="center">
                                                                                                                                Agency MIDT(Average)</td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="height: 13px">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="1000">
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 996px">
                                                                                                                                            <asp:Panel ID="PnlAgencyMIDT" runat="server" Width="1000px">
                                                                                                                                                <asp:GridView ID="GvBAgencyMIDT" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                    ShowHeader="true" Width="1000px" EnableViewState="true" AllowSorting="True" ShowFooter="true"
                                                                                                                                                    OnSorting="GvBAgencyMIDT_Sorting">
                                                                                                                                                    <Columns>
                                                                                                                                                        <asp:TemplateField HeaderText="S.No.">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Literal ID="lblSNo" runat="server"></asp:Literal>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                            <HeaderStyle Width="5%" />
                                                                                                                                                            <ItemStyle Width="5%" />
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:BoundField HeaderText="Agency Name" DataField="Name" SortExpression="Name">
                                                                                                                                                            <HeaderStyle Width="15%" />
                                                                                                                                                            <ItemStyle Width="15%" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="Office Id" DataField="OfficeID" SortExpression="OfficeID">
                                                                                                                                                            <HeaderStyle Width="9%" />
                                                                                                                                                            <ItemStyle Wrap="True" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="Address" DataField="Address" SortExpression="Address">
                                                                                                                                                            <ItemStyle Width="30%" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1A" DataField="A" SortExpression="A">
                                                                                                                                                            <HeaderStyle Width="6%" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1B" DataField="B" SortExpression="B">
                                                                                                                                                            <HeaderStyle Width="6%" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1G" DataField="G" SortExpression="G">
                                                                                                                                                            <HeaderStyle Width="6%" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1P" DataField="P" SortExpression="P">
                                                                                                                                                            <HeaderStyle Width="6%" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="1W" DataField="W" SortExpression="W">
                                                                                                                                                            <HeaderStyle Width="6%" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                        <asp:BoundField HeaderText="TTP" DataField="TTP" SortExpression="TTP">
                                                                                                                                                            <HeaderStyle Width="6%" />
                                                                                                                                                        </asp:BoundField>
                                                                                                                                                    </Columns>
                                                                                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                    <RowStyle CssClass="textbold" />
                                                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                </asp:GridView>
                                                                                                                                            </asp:Panel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 996px">
                                                                                                                                            <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%">
                                                                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                                    <tr class="paddingtop paddingbottom">
                                                                                                                                                        <td style="width: 38%" class="left">
                                                                                                                                                            <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                                                                                                ID="txtTotalRecordCount" runat="server" Width="70px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                                                                                                                                        <td style="width: 20%" class="right">
                                                                                                                                                            <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                                                                                        <td style="width: 25%" class="center">
                                                                                                                                                            <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                                                                                                                Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                                                                                            </asp:DropDownList></td>
                                                                                                                                                        <td style="width: 25%" class="left">
                                                                                                                                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </asp:Panel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <!-- BIDT Part Begins Here--->
                                                                                                                                    <tr>
                                                                                                                                        <td id="tdBidt" runat="server" style="width: 996px">
                                                                                                                                            <asp:ImageButton ID="lnkShowBidtGroup" CssClass="CollapseHeader" runat="server" ImageUrl="~/Images/show-BIDT_TEMP.jpg"
                                                                                                                                                OnClick="lnkShowBidtGroup_Click"></asp:ImageButton>
                                                                                                                                            <asp:Panel CssClass="displayNone" ID="imgAvl" runat="server">
                                                                                                                                                <asp:Label ID="lblLoding" runat="server" CssClass="txtcolor" Text="&nbsp;&nbsp;&nbsp;Loading...&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                                                                                            </asp:Panel>
                                                                                                                                            <asp:UpdatePanel ID="pnlMidtDetailsCollapsOld" runat="server">
                                                                                                                                                <ContentTemplate>
                                                                                                                                                    <asp:Image ImageUrl="~/Images/Hide-BIDT_TEMP.jpg" ID="imgBIDT_Panel" runat="server" />
                                                                                                                                                    <br />
                                                                                                                                                    <asp:Panel ID="pnlBidtGroup" runat="server" Visible="true">
                                                                                                                                                        <asp:Label ID="lblHeaderBIDTGroup" runat="server" CssClass="CollapseHeader subheading center"
                                                                                                                                                            Width="100%" Visible="false" Height="18px"></asp:Label>
                                                                                                                                                        <br />
                                                                                                                                                        <asp:GridView ID="grdvBIDTGrop" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                            Width="100%" EnableViewState="true" AllowSorting="True" ShowFooter="false">
                                                                                                                                                            <Columns>
                                                                                                                                                                <asp:BoundField DataField="LASTAVAIL" HeaderText="Last Available" />
                                                                                                                                                                <asp:BoundField DataField="PRODUCTIVITY" HeaderText="PRODUCTIVITY" Visible="false" />
                                                                                                                                                                <asp:BoundField DataField="AIRNET" HeaderText="AIRNET" ItemStyle-CssClass="right" />
                                                                                                                                                                <asp:BoundField DataField="HOTEL" HeaderText="HOTEL" Visible="false" />
                                                                                                                                                                <asp:BoundField DataField="CAR" HeaderText="CAR" Visible="false" />
                                                                                                                                                                <asp:BoundField DataField="INSURANCE" HeaderText="INSURANCE" Visible="false" />
                                                                                                                                                                <asp:BoundField DataField="NBS" HeaderText="NBS" ItemStyle-CssClass="right" />
                                                                                                                                                                <asp:BoundField DataField="WITHPASSIVE" HeaderText="WITHPASSIVE" Visible="false" />
                                                                                                                                                            </Columns>
                                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                            <RowStyle CssClass="textbold" />
                                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                                                            <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                        </asp:GridView>
                                                                                                                                                        <br />
                                                                                                                                                        <asp:Label ID="llbHeaderBIDTAgencyDetails" runat="server" CssClass="CollapseHeader subheading center"
                                                                                                                                                            Width="100%" Visible="false" Height="18px"></asp:Label>
                                                                                                                                                        <br />
                                                                                                                                                        <asp:GridView ID="grdvBidtAgency" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                            ShowHeader="true" Width="100%" EnableViewState="true" AllowSorting="True" ShowFooter="true"
                                                                                                                                                            OnSorting="grdvBidtAgency_Sorting" OnSelectedIndexChanged="grdvBidtAgency_SelectedIndexChanged"
                                                                                                                                                            OnRowDataBound="grdvBidtAgency_RowDataBound">
                                                                                                                                                            <Columns>
                                                                                                                                                                <asp:BoundField DataField="LCODE" HeaderText="LCODE" SortExpression="LCODE" />
                                                                                                                                                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                                                                                                                                                <asp:BoundField DataField="OfficeID" HeaderText="OfficeID" SortExpression="OfficeID" />
                                                                                                                                                                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                                                                                                                                                                <asp:BoundField DataField="PRODUCTIVITY" HeaderText="PRODUCTIVITY" SortExpression="PRODUCTIVITY"
                                                                                                                                                                    Visible="false" />
                                                                                                                                                                <asp:BoundField DataField="AIRNET" HeaderText="AIRNET" SortExpression="AIRNET" ItemStyle-CssClass="right" />
                                                                                                                                                                <asp:BoundField DataField="HOTEL" HeaderText="HOTEL" SortExpression="HOTEL" Visible="false" />
                                                                                                                                                                <asp:BoundField DataField="CAR" HeaderText="CAR" SortExpression="CAR" Visible="false" />
                                                                                                                                                                <asp:BoundField DataField="INSURANCE" HeaderText="INSURANCE" SortExpression="INSURANCE"
                                                                                                                                                                    Visible="false" />
                                                                                                                                                                <asp:BoundField DataField="NBS" HeaderText="NBS" SortExpression="NBS" ItemStyle-CssClass="right" />
                                                                                                                                                                <asp:BoundField DataField="WITHPASSIVE" HeaderText="WITHPASSIVE" SortExpression="WITHPASSIVE"
                                                                                                                                                                    Visible="false" />
                                                                                                                                                            </Columns>
                                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                            <RowStyle CssClass="textbold" />
                                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                                                            <FooterStyle CssClass="Gridheading right" ForeColor="White" />
                                                                                                                                                        </asp:GridView>
                                                                                                                                                        <%--Paging Section of BIDT Agency Details--%>
                                                                                                                                                        <br />
                                                                                                                                                        <asp:Panel ID="pnlPagingBidtAgency" Visible="false" runat="server" Width="80%">
                                                                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                                                <tr class="paddingtop paddingbottom">
                                                                                                                                                                    <td style="width: 38%" class="left">
                                                                                                                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                                                                                                            ID="txtTotalRecordCountBidt" runat="server" Width="70px" CssClass="textboxgrey"
                                                                                                                                                                            ReadOnly="True"></asp:TextBox></td>
                                                                                                                                                                    <td style="width: 20%" class="right">
                                                                                                                                                                        <asp:LinkButton ID="lnkPervBidt" CssClass="LinkButtons" runat="server" CommandName="Prev"
                                                                                                                                                                            OnClick="lnkPervBidt_Click"><< Prev</asp:LinkButton></td>
                                                                                                                                                                    <td style="width: 25%" class="center">
                                                                                                                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumberBidt"
                                                                                                                                                                            Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageNumberBidt_SelectedIndexChanged">
                                                                                                                                                                        </asp:DropDownList></td>
                                                                                                                                                                    <td style="width: 25%" class="left">
                                                                                                                                                                        <asp:LinkButton ID="lnkBidtNext" runat="server" CssClass="LinkButtons" CommandName="Next"
                                                                                                                                                                            OnClick="lnkBidtNext_Click">Next >></asp:LinkButton></td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                        </asp:Panel>
                                                                                                                                                    </asp:Panel>
                                                                                                                                                    <ajax:CollapsiblePanelExtender ID="collapsePanelBidt" TextLabelID="lblMIDTDetails"
                                                                                                                                                        CollapsedImage="../Images/show-BIDT_TEMP.jpg" ExpandedImage="../Images/Hide-BIDT_TEMP.jpg"
                                                                                                                                                        runat="Server" TargetControlID="pnlBidtGroup" ImageControlID="imgBIDT_Panel"
                                                                                                                                                        ExpandControlID="imgBIDT_Panel" SuppressPostBack="true" CollapseControlID="imgBIDT_Panel"
                                                                                                                                                        Collapsed="false" />
                                                                                                                                                </ContentTemplate>
                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <!-- BIDT Part Begins Here--->
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <!--End of Business Case -->
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 1186px">
                                                                                                    <!-- Business case starts -->
                                                                                                    <asp:Panel ID="pnlBusinessCase" runat="server" Width="100%" CssClass="displayBlock">
                                                                                                        <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                            <tr>
                                                                                                                <td class="bcHeading center">
                                                                                                                    Existing Deal</td>
                                                                                                                <td style="width: 5px; background-color: White;">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                                <td class="bcHeading center">
                                                                                                                    New Deal
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="3" class="gap">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr style="width: 100%">
                                                                                                                <!--Existing Deal -->
                                                                                                                <td style="width: 50%" align="left" valign="top">
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                        <tr>
                                                                                                                            <td valign="top">
                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td width="100%" valign="top">
                                                                                                                                            <table border="0" cellspacing="0" class="redborder" cellpadding="1" width="98%">
                                                                                                                                                <tr>
                                                                                                                                                    <td align="center" style="height: 10px; width: 689px;">
                                                                                                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                                                                            <tr>
                                                                                                                                                                <td align="center" class="subheading" colspan="4" style="height: 14px">
                                                                                                                                                                    Contract Period</td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td style="width: 25%; height: 14px" class="textbox" align="left">
                                                                                                                                                                    Valid from<strong><span style="color: #de2418">*</span></strong></td>
                                                                                                                                                                <td style="width: 25%; height: 14px" align="left">
                                                                                                                                                                    <asp:TextBox ID="txtValidfromOld" CssClass="textboxgrey" ReadOnly="true" runat="server"
                                                                                                                                                                        Width="85px"></asp:TextBox></td>
                                                                                                                                                                <td style="width: 25%; height: 14px" align="left" class="textbox">
                                                                                                                                                                    Valid Till<strong><span style="color: #de2418">*</span></strong></td>
                                                                                                                                                                <td style="width: 25%; height: 14px" align="left">
                                                                                                                                                                    <asp:TextBox ID="txtValidToOld" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                                                                                        Width="85px"></asp:TextBox></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td style="height: 7px">
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="subheading" align="center" style="height: 10px;">
                                                                                                                                                        Connectivity
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr class="gap">
                                                                                                                                                    <td>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 689px;"  valign ="top">
                                                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                            <tr>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Panel ID="pnlConnectivity" runat="server" Width="100%">
                                                                                                                                                                        <asp:GridView ID="GvConnectivity" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                            Width="100%" EnableViewState="true" AllowSorting="false" ShowFooter="true">
                                                                                                                                                                            <Columns>
                                                                                                                                                                                <asp:BoundField HeaderText="Connectivity" Visible="false" DataField="BC_ONLINE_CATG_ID"
                                                                                                                                                                                    ItemStyle-Width="0%" HeaderStyle-Width="0%"></asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="Connectivity" ItemStyle-Wrap="true" DataField="BC_ONLINE_CATG_NAME"
                                                                                                                                                                                    ItemStyle-Width="20%" HeaderStyle-Width="20%"></asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="Unit Cost" DataField="BC_ONLINE_CATG_COST" ItemStyle-Width="25%"
                                                                                                                                                                                    HeaderStyle-Width="25%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="No." DataField="CONN_COUNT" ItemStyle-Width="25%" HeaderStyle-Width="25%"
                                                                                                                                                                                    ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="Total Cost" DataField="TOTAL" ItemStyle-Width="30%" HeaderStyle-Width="25%"
                                                                                                                                                                                    ItemStyle-HorizontalAlign="right"></asp:BoundField>
                                                                                                                                                                                <asp:TemplateField HeaderStyle-Width="0px" HeaderStyle-CssClass="displayNone" ItemStyle-CssClass="displayNone"
                                                                                                                                                                                    FooterStyle-CssClass="displayNone">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:HiddenField ID="hdUnitCost" runat="server" Value='<%#Eval("BC_ONLINE_CATG_COST") %>' />
                                                                                                                                                                                        <asp:HiddenField ID="hdConCount" runat="server" Value='<%#Eval("CONN_COUNT") %>' />
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                            </Columns>
                                                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                            <RowStyle Height="21px" />
                                                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="center" />
                                                                                                                                                                            <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                        </asp:GridView>
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
                                                                                                                                        <td style="width: 689px;" valign="top" >
                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                <tr>
                                                                                                                                                    <td valign ="top" >
                                                                                                                                                        <table border="0" cellspacing="0" class="redborder" cellpadding="0" width="98%">
                                                                                                                                                            <tr>
                                                                                                                                                                <td class="subheading" align="center">
                                                                                                                                                                    Hardware
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr class="gap">
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Panel ID="pnlHardware" runat="server" Width="100%">
                                                                                                                                                                        <asp:GridView ID="GvHardware" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                            Width="100%" EnableViewState="true" AllowSorting="false" ShowFooter="true">
                                                                                                                                                                            <Columns>
                                                                                                                                                                                <asp:BoundField HeaderText="" Visible="false" DataField="BC_EQP_CATG_ID" ItemStyle-Width="0%"
                                                                                                                                                                                    HeaderStyle-Width="0%"></asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="Hardware" DataField="BC_EQP_CATG_TYPE" ItemStyle-Width="20%"
                                                                                                                                                                                    HeaderStyle-Width="20%"></asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="Unit Cost" DataField="BC_EQP_CATG_COST" ItemStyle-Width="25%"
                                                                                                                                                                                    HeaderStyle-Width="25%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="No." DataField="PRODUCT_COUNT" ItemStyle-Width="25%"
                                                                                                                                                                                    HeaderStyle-Width="25%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="Total Cost" DataField="TOTAL" ItemStyle-Width="30%" HeaderStyle-Width="30%"
                                                                                                                                                                                    ItemStyle-HorizontalAlign="right"></asp:BoundField>
                                                                                                                                                                            </Columns>
                                                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                            <RowStyle Height="21px" />
                                                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="center" />
                                                                                                                                                                            <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                    </asp:Panel>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td  valign ="top" >
                                                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                            <tr class="gap">
                                                                                                                                                                <td style="width: 689px; height: 8pt;">
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td  valign ="top" >
                                                                                                                                                                    <table border="0" class="redborder" cellspacing="0" cellpadding="0" width="98%">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4" align="center" class="subheading">
                                                                                                                                                                                Slab Qualification
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4">
                                                                                                                                                                                <asp:CheckBoxList ID="chkSLABQUALIFICATIONOld" RepeatDirection="Horizontal" Width="100%"
                                                                                                                                                                                    RepeatColumns="4" runat="server">
                                                                                                                                                                                </asp:CheckBoxList>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr class="gap">
                                                                                                                                                                <td style="width: 689px">
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <table border="0" cellspacing="0" cellpadding="0" class="redborder" style="width: 98%">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4" class="subheading" align="center" style="height: 10px">
                                                                                                                                                                                Incentive
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="gap">
                                                                                                                                                                            <td style="width: 689px;">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="Gridheading">
                                                                                                                                                                            <td width="25%">
                                                                                                                                                                                Incentive</td>
                                                                                                                                                                            <td width="25%">
                                                                                                                                                                                Rate</td>
                                                                                                                                                                            <td width="30%">
                                                                                                                                                                                Expected Segments</td>
                                                                                                                                                                            <td width="20%">
                                                                                                                                                                                Total Costs</td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="right">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:TextBox ID="txtIncRateO" runat="server" MaxLength="6" CssClass="textboxgrey right"
                                                                                                                                                                                    ReadOnly="True" Width="84px"></asp:TextBox></td>
                                                                                                                                                                            <td>
                                                                                                                                                                                &nbsp;
                                                                                                                                                                                <asp:TextBox ID="txtIncExpectedSegO" MaxLength="6" runat="server" CssClass="textboxgrey right"
                                                                                                                                                                                    ReadOnly="True" Width="87px"></asp:TextBox></td>
                                                                                                                                                                            <td align="right">
                                                                                                                                                                                <asp:Label ID="lblIncTotalCostO" runat="server" CssClass="textboxgrey right" Width="87px"></asp:Label></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="lightblue">
                                                                                                                                                                                Total Cost</td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="right">
                                                                                                                                                                                <asp:Label ID="lblTSegTotalCostO" runat="server" CssClass="right" />
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td style="height: 18px" class="lightblue">
                                                                                                                                                                                Total Segment</td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblIncTSegRateO" runat="server" CssClass="right"></asp:Label></td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblTSegExpSegmentO" runat="server" CssClass="right"></asp:Label></td>
                                                                                                                                                                            <td align="right">
                                                                                                                                                                                <asp:Label ID="lblTotalSegmentO" runat="server" CssClass="right" />
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="lightblue" style="height: 10px">
                                                                                                                                                                                CPS</td>
                                                                                                                                                                            <td style="height: 10px">
                                                                                                                                                                                <asp:Label ID="lblIncCpsRateO" runat="server" CssClass="right"></asp:Label></td>
                                                                                                                                                                            <td style="height: 10px">
                                                                                                                                                                                <asp:Label ID="lblIncCpsExpSegO" runat="server" CssClass="right"></asp:Label></td>
                                                                                                                                                                            <td align="right" style="height: 10px">
                                                                                                                                                                                <asp:Label ID="lblIncCpsTotalO" runat="server" CssClass="right" /></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr class="gap">
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <table border="0" class="redborder" cellspacing="0" cellpadding="0" style="border-collapse: collapse;
                                                                                                                                                                        width: 98%;">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4" class="subheading" align="center">
                                                                                                                                                                                Fixed Incentive
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="gap">
                                                                                                                                                                            <td colspan="4">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="Gridheading">
                                                                                                                                                                            <td width="25%">
                                                                                                                                                                                Fixed Incentive</td>
                                                                                                                                                                            <td width="20%">
                                                                                                                                                                                Months</td>
                                                                                                                                                                            <td width="55%" colspan="2" class="center">
                                                                                                                                                                                Incentive Per Month</td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td style="height: 14px" width="80%">
                                                                                                                                                                                <asp:TextBox ID="txtFixIncOld" runat="server" MaxLength="6" CssClass="textboxgrey right"
                                                                                                                                                                                    Width="97px" ReadOnly="True"></asp:TextBox></td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:TextBox ID="txtFixIncMonthOld" runat="server" CssClass="textboxgrey right" Width="84px"
                                                                                                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="height: 14px" align="right">
                                                                                                                                                                                <asp:Label ID="lblFixIncPerMonthOld" runat="server" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td width="80%" style="height: 14px" class="lightblue">
                                                                                                                                                                                Total Cost</td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="height: 14px" align="right">
                                                                                                                                                                                <asp:Label ID="lblFixIncTotalCostOld" runat="server" CssClass="textbox right" Width="62px"></asp:Label></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="lightblue">
                                                                                                                                                                                Min Monthly Segment</td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="right">
                                                                                                                                                                                <asp:Label ID="lblFixIncMinMSegOld" runat="server" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="lightblue" style="height: 14px">
                                                                                                                                                                                CPS</td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="height: 14px" align="right">
                                                                                                                                                                                <asp:Label ID="lblFixIncCPSOld" runat="server" Style="display: none;" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td width="25%" style="height: 14px" class="lightblue">
                                                                                                                                                                                Gross</td>
                                                                                                                                                                            <td width="20%" style="height: 14px" class="lightblue">
                                                                                                                                                                                Excluding IC/IT</td>
                                                                                                                                                                            <td width="35%" style="height: 14px" class="lightblue">
                                                                                                                                                                                Net</td>
                                                                                                                                                                            <td width="20%">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td style="height: 13px">
                                                                                                                                                                                <asp:Label ID="lblFixIncGrossOld" runat="server" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                            <td style="height: 13px">
                                                                                                                                                                                <asp:Label ID="lblFixIncICITOld" runat="server" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                            <td style="height: 13px">
                                                                                                                                                                                <asp:Label ID="lblFixIncNetOld" runat="server" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                            <td style="height: 13px">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="gap">
                                                                                                                                                                            <td colspan="4" style="height: 8pt">
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
                                                                                                                        <tr class="gap">
                                                                                                                            <td colspan="3">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr id="trOldCase" runat="server">
                                                                                                                            <td colspan="3" class="BCTopBorder">
                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td colspan="2">
                                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tlbRadioOLD" runat="server">
                                                                                                                                                <tr class="">
                                                                                                                                                    <td style="width: 30%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 62%">
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <!-- Raido Option for Old -->
                                                                                                                                                <tr>
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Payment Type
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 67%;" colspan="3">
                                                                                                                                                        <asp:RadioButtonList ID="rdbIncentiveTypeOld" Enabled="false" CellSpacing="5" runat="server"
                                                                                                                                                            RepeatDirection="Horizontal">
                                                                                                                                                            <asp:ListItem Selected="True" Value="1">Upfront Payment</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Post Payment</asp:ListItem>
                                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 1px; background-color: Maroon;">
                                                                                                                                                    <td colspan="5" style="height: 1px; background-color: Maroon;">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trUpfrontTypeNameOld" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Payment Term
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 67%" colspan="3">
                                                                                                                                                        <asp:RadioButtonList ID="rdbUpfrontTypeNameOld" Enabled="false" runat="server" RepeatDirection="Horizontal"
                                                                                                                                                            Width="100%" CellSpacing="3">
                                                                                                                                                            <asp:ListItem Selected="True" Value="1">One time</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Replinishable</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="3">Fixed</asp:ListItem>
                                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 1px; background-color: Maroon;">
                                                                                                                                                    <td colspan="5" style="height: 1px; background-color: Maroon;">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="TRMinimumSegmentCriteriaOld" runat="server">
                                                                                                                                                    <td colspan="5">
                                                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                            <tr>
                                                                                                                                                                <td>
                                                                                                                                                                    <table border="0" class="displayNone" cellspacing="0" cellpadding="0" width="98%"
                                                                                                                                                                        id="TblMinimumSegmentCriteriaOld" runat="server">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4" align="center" class="subheading">
                                                                                                                                                                                Minimum Segment Criteria
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4">
                                                                                                                                                                                &nbsp;&nbsp;
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                                &nbsp;</td>
                                                                                                                                                                            <td style="width: 160px;">
                                                                                                                                                                                Minimum Segment
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                &nbsp;</td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:TextBox ID="TxtMinSegCriteriaValueOld" runat="server" Enabled="false"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4">
                                                                                                                                                                                <asp:CheckBoxList ID="ChkMinimunCriteriaOld" RepeatDirection="Horizontal" Width="100%"
                                                                                                                                                                                    Enabled="false" RepeatColumns="4" runat="server">
                                                                                                                                                                                </asp:CheckBoxList>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Sign Up Amount</td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:TextBox ID="txtSignUpAmountOld" runat="server" ReadOnly="true" CssClass="textboxgrey right"
                                                                                                                                                            MaxLength="10" onkeyup="checknumericWithDot(this.id)" Width="131px"></asp:TextBox>
                                                                                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                                        Adjustable
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%" class="left">
                                                                                                                                                        <asp:CheckBox ID="chkAdjustableOld" runat="server" Text="" Enabled="false" />
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trUpfrontAmountOld" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Upfront Amount<span class="Mandatory">* </span>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:TextBox ID="txtUpfrontAmountOld" ReadOnly="true" runat="server" CssClass="textboxgrey right"
                                                                                                                                                            MaxLength="10" Width="131px"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%;">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr runat="server" id="trNoOfPaymentsOld" class="displayNone">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        No Of Payments</td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:TextBox ID="txtNoOfPaymentsOld" runat="server" ReadOnly="true" CssClass="textboxgrey right"
                                                                                                                                                            MaxLength="3" Width="131px"></asp:TextBox></td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr runat="server" id="trForThePeriodOfOld" class="displayNone">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        For the period of
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:TextBox ID="txtForThePeriodOfOld" runat="server" ReadOnly="true" CssClass="textboxgrey right"
                                                                                                                                                            MaxLength="3" Width="131px"></asp:TextBox></td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trPaymentTypeOld" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Adjustment
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 67%" colspan="3">
                                                                                                                                                        <asp:RadioButtonList ID="rdbPaymentTypeOld" Enabled="false" runat="server" RepeatDirection="Horizontal"
                                                                                                                                                            CellSpacing="10">
                                                                                                                                                            <asp:ListItem Selected="True" Value="1">Rate</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Fixed</asp:ListItem>
                                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <!-- Raido Option for Old -->
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td colspan="2">
                                                                                                                                            <asp:Panel ID="pnlIncentivePlan" runat="server" Width="100%">
                                                                                                                                                <asp:GridView ID="grdvIncentivePlanOld" ShowHeader="False" runat="server" AutoGenerateColumns="False"
                                                                                                                                                    TabIndex="6" Width="100%" EnableViewState="true" AllowSorting="false" OnRowDataBound="grdvIncentivePlanOld_RowDataBound">
                                                                                                                                                    <RowStyle CssClass="textbold" />
                                                                                                                                                    <Columns>
                                                                                                                                                        <asp:TemplateField>
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                                    <tr class="smallgap">
                                                                                                                                                                        <td colspan="3">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td style="height: 21px" class="LinkButtons">
                                                                                                                                                                            Case Name :
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="height: 21px;">
                                                                                                                                                                            <asp:TextBox ID="txtPlanName" Width="150px" ReadOnly="true" CssClass="textboxgrey"
                                                                                                                                                                                runat="server" Text='<%#Eval("INC_PLAN_NAME")%>'></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                                <br />
                                                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td style="width: 125px; text-align: left">
                                                                                                                                                                            <asp:UpdatePanel ID="pnlMidtDetailsCollapsOld" runat="server">
                                                                                                                                                                                <ContentTemplate>
                                                                                                                                                                                    <asp:Panel ID="ExpandHeaderOld" runat="server" Style="cursor: pointer">
                                                                                                                                                                                        <asp:Label ID="lblMIDTDetailsOld" runat="server" CssClass="LinkButtons" Text="Case Details"></asp:Label>
                                                                                                                                                                                    </asp:Panel>
                                                                                                                                                                                    <asp:Panel ID="pnlShowCaseDetailOld" Height="0px" runat="server" Style="overflow: hidden;">
                                                                                                                                                                                        <asp:CheckBoxList CssClass="chkboxlist" ID="chklstCriteriaOld" RepeatDirection="Horizontal"
                                                                                                                                                                                            runat="server" Width="470px" RepeatColumns="5">
                                                                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                                                                    </asp:Panel>
                                                                                                                                                                                    <ajax:CollapsiblePanelExtender ID="ClsPnlMIDTDetailsOld" TextLabelID="lblMIDTDetailsOld"
                                                                                                                                                                                        runat="Server" TargetControlID="pnlShowCaseDetailOld" ExpandControlID="ExpandHeaderOld"
                                                                                                                                                                                        ExpandedText="Hide Airline" CollapsedText="Show Airline" SuppressPostBack="true"
                                                                                                                                                                                        CollapseControlID="ExpandHeaderOld" Collapsed="false" />
                                                                                                                                                                                </ContentTemplate>
                                                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                                <table cellpadding="0" cellspacing="0" class="displayNone" border="0" style="width: 100%;">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 109px">
                                                                                                                                                                            Range From<span class="Mandatory">* </span>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 100px">
                                                                                                                                                                            <asp:TextBox ID="txtRangeFrom" MaxLength="6" CssClass="right" runat="server" Width="79px"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                            &nbsp;&nbsp;
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 50px">
                                                                                                                                                                            To
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 100px">
                                                                                                                                                                            <asp:TextBox ID="txtRangeTo" MaxLength="6" CssClass="right" runat="server" Width="79px"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 88px">
                                                                                                                                                                            Amount <span class="Mandatory">* </span>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 255px">
                                                                                                                                                                            <asp:TextBox ID="txtSlabAmount" MaxLength="6" CssClass="right" runat="server" Width="79px"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                            <asp:Button ID="btnAddSlabs" runat="server" Text="Add" CssClass="button" CommandName="AddSlabNested"
                                                                                                                                                                                CommandArgument='<%#Eval("Case_Id")%>' />
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 10px;">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                                <br />
                                                                                                                                                                <asp:GridView ID="GvIncSlabsNested" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                    OnRowCommand="GvIncSlabsNested_RowCommand" ShowHeader="true" Width="100%" EnableViewState="true"
                                                                                                                                                                    AllowSorting="false" ShowFooter="false">
                                                                                                                                                                    <Columns>
                                                                                                                                                                        <asp:BoundField HeaderText="Start" DataField="SLABS_START">
                                                                                                                                                                            <HeaderStyle Width="35%" />
                                                                                                                                                                            <ItemStyle Width="35%" CssClass="right" />
                                                                                                                                                                        </asp:BoundField>
                                                                                                                                                                        <asp:BoundField HeaderText="End" DataField="SLABS_END">
                                                                                                                                                                            <HeaderStyle Width="35%" />
                                                                                                                                                                            <ItemStyle Width="35%" Wrap="True" CssClass="right" />
                                                                                                                                                                        </asp:BoundField>
                                                                                                                                                                        <asp:BoundField HeaderText="Amount" DataField="SLABS_RATE">
                                                                                                                                                                            <HeaderStyle Width="30%" />
                                                                                                                                                                            <ItemStyle Width="30%" CssClass="right" />
                                                                                                                                                                        </asp:BoundField>
                                                                                                                                                                    </Columns>
                                                                                                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                    <RowStyle CssClass="textbold" />
                                                                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                </asp:GridView>
                                                                                                                                                                <asp:HiddenField ID="hdCaseId" runat="server" Value='<%#Eval("Case_Id") %>' />
                                                                                                                                                                <asp:HiddenField ID="hdNestedUpdateFlag" EnableViewState="true" runat="server" />
                                                                                                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                                                                                                    <tr class="smallgap">
                                                                                                                                                                        <td colspan="2">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                    </Columns>
                                                                                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                </asp:GridView>
                                                                                                                                            </asp:Panel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr class="gap">
                                                                                                                                        <td>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr >
                                                                                                                                        <td class="redborder" style="width: 100%;">
                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                <tr id="trPlbOld" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Is PLB Applicable
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:CheckBox ID="chkPlbOld" Enabled="false" runat="server" Text="" Width="51px" />
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trPlbTypeNameOld" class="displayNone" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        PLB Type
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:RadioButtonList ID="rdbPlbTypeNameOld" Enabled="false" runat="server" RepeatDirection="Horizontal">
                                                                                                                                                            <asp:ListItem Selected="True" Value="1">Fixed</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Slab Based</asp:ListItem>
                                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trSlabPlbOld" runat="server">
                                                                                                                                                    <td colspan="5" style="width: 100%">
                                                                                                                                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                                                                                                                            <tr>
                                                                                                                                                                <td style="width: 101px; height: 3px;">
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td colspan="3">
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td colspan="3">
                                                                                                                                                                    <asp:GridView ID="grdvPlbSlabNOld" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                        ShowHeader="true" Width="100%" EnableViewState="true" AllowSorting="false" ShowFooter="false">
                                                                                                                                                                        <Columns>
                                                                                                                                                                            <asp:BoundField HeaderText="Start" DataField="SLABS_START">
                                                                                                                                                                                <HeaderStyle Width="35%" />
                                                                                                                                                                                <ItemStyle Width="35%" CssClass="right" />
                                                                                                                                                                            </asp:BoundField>
                                                                                                                                                                            <asp:BoundField HeaderText="End" DataField="SLABS_END">
                                                                                                                                                                                <HeaderStyle Width="35%" />
                                                                                                                                                                                <ItemStyle Width="35%" Wrap="True" CssClass="right" />
                                                                                                                                                                            </asp:BoundField>
                                                                                                                                                                            <asp:BoundField HeaderText="Amount" DataField="SLABS_RATE">
                                                                                                                                                                                <HeaderStyle Width="30%" />
                                                                                                                                                                                <ItemStyle Width="30%" CssClass="right" />
                                                                                                                                                                            </asp:BoundField>
                                                                                                                                                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:LinkButton ID="lnkEdit" Enabled="false" runat="server" Text="Edit" CssClass="LinkButtons"
                                                                                                                                                                                        CommandName="EditX">
                                                                                                                                                                                    </asp:LinkButton>
                                                                                                                                                                                    &nbsp;
                                                                                                                                                                                    <asp:LinkButton ID="lnkDelete" Enabled="false" runat="server" Text="Delete" CssClass="LinkButtons"
                                                                                                                                                                                        CommandName="DelX">
                                                                                                                                                                                    </asp:LinkButton>
                                                                                                                                                                                    <asp:HiddenField ID="hdTEMP_SLAB_ID" runat="server" />
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                        </Columns>
                                                                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                        <RowStyle CssClass="textbold" />
                                                                                                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                        <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                    </asp:GridView>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trPlbAmountOld" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        PLB Bonus Amount
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:TextBox ID="txtPlbBonusOld" Enabled="false" runat="server" CssClass="textboxgrey right"
                                                                                                                                                            MaxLength="6" Width="131px"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%;">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr class="smallgap">
                                                                                                                            <td>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="width: 100%; border-bottom-color: Red; border-left-color: Red;">
                                                                                                                                <asp:Panel ID="pnlCreatePlanN" runat="server">
                                                                                                                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="3">
                                                                                                                                                <table border="0" class="displayNone" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                    <tr>
                                                                                                                                                        <td style="width: 26%; text-align: left">
                                                                                                                                                        </td>
                                                                                                                                                        <td colspan="2" style="width: 74%; text-align: left">
                                                                                                                                                            <asp:ImageButton ID="imgCriteriaSelection" runat="server" ImageAlign="Bottom" ImageUrl="~/Images/lookup.gif" />
                                                                                                                                                            <asp:Button ID="btnFalse" runat="server" CssClass="displayNone" />
                                                                                                                                                            <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                                                                                                                                DropShadow="false" PopupControlID="pnlMidt" OkControlID="btnok" CancelControlID="btnCancelCriteria"
                                                                                                                                                                TargetControlID="btnFalse" RepositionMode="RepositionOnWindowResizeAndScroll">
                                                                                                                                                            </ajax:ModalPopupExtender>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr class="smallgap">
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="2" style="width: 100%;">
                                                                                                                                                <asp:Panel ID="pnlMidt" runat="server" Width="100%" BackColor="white" CssClass="displayNone">
                                                                                                                                                    <table>
                                                                                                                                                        <tr style="height: 1pt">
                                                                                                                                                            <td>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                    </table>
                                                                                                                                                    <table cellpadding="0" cellspacing="0" border="0" width="590px" class="redborder">
                                                                                                                                                        <tr>
                                                                                                                                                            <td>
                                                                                                                                                            </td>
                                                                                                                                                            <td colspan="2" align="center">
                                                                                                                                                                <asp:Label ID="lblChkMsg" CssClass="ErrorMsg" runat="server" Visible="false" EnableViewState="False"></asp:Label>
                                                                                                                                                            </td>
                                                                                                                                                            <td>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td>
                                                                                                                                                            </td>
                                                                                                                                                            <td colspan="2" align="left">
                                                                                                                                                                <br />
                                                                                                                                                            </td>
                                                                                                                                                            <td>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td>
                                                                                                                                                            </td>
                                                                                                                                                            <td colspan="2" align="center">
                                                                                                                                                                <asp:Button ID="btnok" runat="server" Text="Ok" Style="display: none" />
                                                                                                                                                            </td>
                                                                                                                                                            <td>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr class="smallgap">
                                                                                                                                                            <td colspan="3">
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                    </table>
                                                                                                                                                </asp:Panel>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </asp:Panel>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <!-- Section for Gridview Criteria Selection -->
                                                                                                                        <tr>
                                                                                                                            <td colspan="3" style="height: 10pt;">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <!-- End of Section for Gridview Criteria Selection -->
                                                                                                                        <tr>
                                                                                                                            <td colspan="3" style="height: 10pt;">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                                <!-- End of Existing Deal -->
                                                                                                                <td style="width: 5px;">
                                                                                                                </td>
                                                                                                                <!-- New Deal Starts --->
                                                                                                                <td style="width: 50%" valign="top">
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                        <tr>
                                                                                                                            <td valign="top">
                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td width="100%" valign="top">
                                                                                                                                            <table border="0" cellspacing="0" class="redborder" cellpadding="1" width="98%">
                                                                                                                                                <tr>
                                                                                                                                                    <td align="center" style="height: 10px;width: 689px;">
                                                                                                                                                        <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                            <tr>
                                                                                                                                                                <td align="center" class="subheading" colspan="4" nowrap="nowrap">
                                                                                                                                                                    Contract Period</td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td class="textbox" nowrap="nowrap" style="width: 25%" align="left">
                                                                                                                                                                    Valid From <span class="Mandatory">* </span>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="width: 25%" nowrap="nowrap">
                                                                                                                                                                    <asp:TextBox ID="txtPeriodFrom" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                                                                                                                                                    <img id="Img1" alt="" src="../Images/calender.gif" runat="server" title="Date selector"
                                                                                                                                                                        onclick="return getCalender();" style="cursor: pointer" />
                                                                                                                                                                    <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtPeriodFrom.ClientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                                                                                    </script>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="width: 25%" nowrap="nowrap" class="textbox">
                                                                                                                                                                    Valid Till <span class="Mandatory">* </span>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="width: 25%" nowrap="nowrap">
                                                                                                                                                                    <asp:TextBox ID="txtPeriodTo" MaxLength="10" runat="server" Width="80px"></asp:TextBox>
                                                                                                                                                                    <img id="Img2" alt="" src="../Images/calender.gif" onclick="return getCalenderTo();"
                                                                                                                                                                        title="Date selector" style="cursor: pointer" /> 
                                                                                                                                                                   <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtPeriodTo.ClientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "Img2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                                                                                    </script>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                           <tr>
                                                                                                                                                                <td style="height: 7px">
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="subheading" align="center" style="height: 10px;">
                                                                                                                                                        Connectivity
                                                                                                                                                    </td>
                                                                                                                                                </tr> 
                                                                                                                                                <tr class="gap">
                                                                                                                                                    <td>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 100%;" valign ="top" >
                                                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                            <tr>
                                                                                                                                                                <td valign ="top" >
                                                                                                                                                                    <asp:GridView ID="grdConnectivityN" runat="server" ShowFooter="true" ShowHeader="true"
                                                                                                                                                                        AutoGenerateColumns="false" OnRowDataBound="grdConnectivityN_RowDataBound">
                                                                                                                                                                        <Columns>
                                                                                                                                                                            <asp:TemplateField HeaderText="Connectivity" ItemStyle-Width="21%">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:Label ID="lblConnName" runat="server" Text='<%#Eval("BC_ONLINE_CATG_NAME") %>'></asp:Label>
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                            <asp:TemplateField HeaderText="Unit Cost" ItemStyle-Width="20%">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:Label ID="lblConnCatCost" CssClass="right" runat="server" Text='<%#Eval("BC_ONLINE_CATG_COST") %>'></asp:Label>
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                            <asp:TemplateField HeaderText="Existing No." ItemStyle-Width="24%">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:Label ID="lblEConnCount" CssClass="right" runat="server" Text='<%#Eval("E_CONN_COUNT") %>'></asp:Label>
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                            <asp:TemplateField HeaderText="Number" ItemStyle-Width="15%">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:TextBox ID="txtConnNo" CssClass="right" MaxLength="6" runat="server" Text='<%#Eval("CONN_COUNT") %>'></asp:TextBox>
                                                                                                                                                                                    <asp:HiddenField ID="hdOnlineCatID" runat="server" Value='<%#Eval("BC_ONLINE_CATG_ID") %>' />
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Total Cost">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:Label ID="lblConnTotal" CssClass="right" runat="server"></asp:Label>
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                        </Columns>
                                                                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                        <RowStyle Height="21px" />
                                                                                                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="center" />
                                                                                                                                                                        <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                    </asp:GridView>
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
                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                <tr>
                                                                                                                                                    <td valign ="top">
                                                                                                                                                        <table border="0" cellspacing="0" class="redborder" cellpadding="0" width="98%">
                                                                                                                                                            <tr>
                                                                                                                                                                <td class="subheading" align="center">
                                                                                                                                                                    Hardware
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr class="gap">
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>                                                                                                                                                          
                                                                                                                                                            <tr>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Panel ID="pnlHardwareN" runat="server" Width="100%">
                                                                                                                                                                        <asp:GridView ID="grdvHardwareN" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                            ShowHeader="true" Width="100%" EnableViewState="true" AllowSorting="false" ShowFooter="true"
                                                                                                                                                                            OnRowDataBound="grdvHardwareN_RowDataBound">
                                                                                                                                                                            <Columns>
                                                                                                                                                                                <asp:TemplateField HeaderText="Hardware" ItemStyle-Width="21%">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lblHwNameN" runat="server" Text='<%#Eval("BC_EQP_CATG_TYPE") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Unit Cost" ItemStyle-Width="20%">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lblHwCostN" runat="server" Text='<%#Eval("BC_EQP_CATG_COST") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Existing No." ItemStyle-Width="24%">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lblHwNoNEcount" runat="server" Text='<%#Eval("E_PRODUCT_COUNT") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Number" ItemStyle-Width="15%">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:TextBox ID="txtHwNoN" MaxLength="6" CssClass="right" runat="server" Text='<%#Eval("PRODUCT_COUNT") %>'></asp:TextBox>
                                                                                                                                                                                        <asp:HiddenField ID="hdHwIDN" runat="server" Value='<%#Eval("BC_EQP_CATG_ID") %>' />
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Total Cost">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lblHwTotCostN" runat="server"></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                            </Columns>
                                                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                           <RowStyle Height="21px" />
                                                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                            <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                    </asp:Panel>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td valign ="top" >
                                                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                            <tr class="gap">
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td>
                                                                                                                                                                    <table border="0" class="redborder" cellspacing="0" cellpadding="0" width="98%">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4" align="center" class="subheading">
                                                                                                                                                                                <span class="LinkButtons">Slab Qualification</span> <span class="Mandatory">* </span>
                                                                                                                                                                                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LnkSlabQualMore" CssClass="LinkButtons" runat="server"
                                                                                                                                                                                    Text="More..."></asp:LinkButton>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4">
                                                                                                                                                                                <asp:CheckBoxList ID="chkSLABQUALIFICATION" RepeatDirection="Horizontal" Width="100%"
                                                                                                                                                                                    RepeatColumns="4" runat="server">
                                                                                                                                                                                </asp:CheckBoxList>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <asp:Button ID="BtnFakeSQ" runat="server" CssClass="displayNone" />
                                                                                                                                                                        <ajax:ModalPopupExtender ID="ModPExtForQual" runat="server" BackgroundCssClass="modalBackground"
                                                                                                                                                                            DropShadow="false" PopupControlID="pnlSQMore" OkControlID="btnSQOK" CancelControlID="btnSQCAN"
                                                                                                                                                                            TargetControlID="BtnFakeSQ" RepositionMode="RepositionOnWindowResizeAndScroll">
                                                                                                                                                                        </ajax:ModalPopupExtender>
                                                                                                                                                                    </table>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr class="gap">
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <table border="0" class="redborder" cellspacing="0" cellpadding="0" width="98%">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4" class="subheading" align="center">
                                                                                                                                                                                Incentive
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="gap">
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="Gridheading">
                                                                                                                                                                            <td width="25%">
                                                                                                                                                                                Incentive</td>
                                                                                                                                                                            <td width="25%">
                                                                                                                                                                                Rate</td>
                                                                                                                                                                            <td width="30%">
                                                                                                                                                                                Expected Segments</td>
                                                                                                                                                                            <td width="20%">
                                                                                                                                                                                Total Costs</td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td style="height: 21px">
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="height: 21px">
                                                                                                                                                                                <asp:TextBox ID="txtIncRateN" MaxLength="6" runat="server" CssClass="textbox right"
                                                                                                                                                                                    Width="125px"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="height: 21px">
                                                                                                                                                                                &nbsp;
                                                                                                                                                                                <asp:TextBox ID="txtIncExpectedSegN" MaxLength="6" runat="server" CssClass="textbox right"
                                                                                                                                                                                    Width="87px"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="height: 21px; padding-right: 2px;" align="right">
                                                                                                                                                                                <asp:Label ID="lblIncTotalCostN" runat="server" CssClass="textbox right" Width="87px"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="lightblue">
                                                                                                                                                                                Total Cost</td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="right" style="padding-right: 2px;">
                                                                                                                                                                                <asp:Label ID="lblTSegTotalCostN" runat="server" Text="" CssClass="right" />
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="lightblue">
                                                                                                                                                                                Total Segment</td>
                                                                                                                                                                            <td align="right">
                                                                                                                                                                                <asp:Label ID="lblIncTSegRateN" runat="server" Text="" CssClass="right"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="right">
                                                                                                                                                                                <asp:Label ID="lblTSegExpSegmentN" runat="server" Text="" CssClass="right"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="right" style="padding-right: 2px;">
                                                                                                                                                                                <asp:Label ID="lblTotalSeg" runat="server" Text="" CssClass="right"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td style="height: 13px" class="lightblue">
                                                                                                                                                                                CPS</td>
                                                                                                                                                                            <td style="height: 13px" align="right">
                                                                                                                                                                                <asp:Label ID="lblIncCpsRateN" runat="server" Text="" CssClass="right"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="height: 13px">
                                                                                                                                                                                <asp:Label ID="lblIncCpsExpSegN" runat="server" Text="" CssClass="right"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="height: 13px; padding-right: 2px;" align="right">
                                                                                                                                                                                <asp:Label ID="lblIncCpsTotalN" runat="server" Text="" CssClass="right" />
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr class="gap">
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <table border="0" cellspacing="0" class="redborder" cellpadding="0" width="98%">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="5" class="subheading" align="center">
                                                                                                                                                                                Fixed Incentive
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="gap">
                                                                                                                                                                            <td colspan="5">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="Gridheading">
                                                                                                                                                                            <td width="25%">
                                                                                                                                                                                Fixed Incentive</td>
                                                                                                                                                                            <td width="20%">
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="width: 154px">
                                                                                                                                                                                Months</td>
                                                                                                                                                                            <td width="55%" colspan="2" class="center">
                                                                                                                                                                                Incentive Per Month</td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:TextBox ID="txtFixIncentive" MaxLength="10" runat="server" CssClass="textbox right"
                                                                                                                                                                                    Width="113px"></asp:TextBox></td>
                                                                                                                                                                            <td align="right" colspan="2" style="padding-right: 6px">
                                                                                                                                                                                <asp:TextBox ID="txtFixIncMonth" MaxLength="6" runat="server" CssClass="textbox right"
                                                                                                                                                                                    Width="69px"></asp:TextBox></td>
                                                                                                                                                                            <td>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="right" style="padding-right: 2px;">
                                                                                                                                                                                <asp:Label ID="lblFixIncPerMonth" runat="server" CssClass="textbox right" Width="60px"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td align="left" class="lightblue" colspan="3">
                                                                                                                                                                                Total Cost</td>
                                                                                                                                                                            <td align="center" width="35%">
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="20%" align="right" style="padding-right: 2px;">
                                                                                                                                                                                <asp:Label ID="lblFixIncTotalCost" runat="server" CssClass="textbox right" Width="60px"></asp:Label></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td align="left" class="lightblue" colspan="3">
                                                                                                                                                                                Min Monthly Segment</td>
                                                                                                                                                                            <td align="center" width="35%">
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="20%" align="right" style="padding-right: 2px;">
                                                                                                                                                                                <asp:TextBox ID="txtFixIncMinMonthSeg" MaxLength="10" runat="server" CssClass="textbox right"
                                                                                                                                                                                    Width="69px"></asp:TextBox></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td align="left" class="lightblue" colspan="3">
                                                                                                                                                                                CPS</td>
                                                                                                                                                                            <td align="center" width="35%">
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="20%" align="right" style="padding-right: 2px;">
                                                                                                                                                                                <asp:Label ID="lblFixIncCPS" runat="server" Style="display: none;" CssClass="displayNone"
                                                                                                                                                                                    Width="60px"></asp:Label></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td width="25%" align="center" class="lightblue">
                                                                                                                                                                                Gross</td>
                                                                                                                                                                            <td align="left" class="lightblue" width="20%">
                                                                                                                                                                                Excluding IC</td>
                                                                                                                                                                            <td align="center" class="lightblue" style="width: 154px">
                                                                                                                                                                                Excluding IC/IT</td>
                                                                                                                                                                            <td width="35%" align="center" class="lightblue">
                                                                                                                                                                                Net</td>
                                                                                                                                                                            <td width="20%">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td align="center" style="height: 13px">
                                                                                                                                                                                <asp:Label ID="lblFixIncGross" runat="server" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                            <td align="left" style="height: 13px">
                                                                                                                                                                                <asp:Label ID="lblFixIncIC" runat="server" CssClass="textbox" Width="114px"></asp:Label></td>
                                                                                                                                                                            <td align="center" style="width: 154px; height: 13px;">
                                                                                                                                                                                <asp:Label ID="lblFixIncICIT" runat="server" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                            <td align="center" style="height: 13px">
                                                                                                                                                                                <asp:Label ID="lblFixIncNet" runat="server" CssClass="textbox right" Width="50px"></asp:Label></td>
                                                                                                                                                                            <td style="height: 13px">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr class="gap">
                                                                                                                                                                            <td colspan="5">
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
                                                                                                                        <tr class="gap">
                                                                                                                            <td colspan="3">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td colspan="3" class="BCTopBorder">
                                                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td colspan="2" style="width: 100%;">
                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                <tr class="gap">
                                                                                                                                                    <td colspan="5" class="center" style="height: 8pt">
                                                                                                                                                        <asp:Label ID="lblInnerError" runat="server" CssClass="ErrorMsg" EnableViewState="false"></asp:Label>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Payment Type
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 67%" colspan="3">
                                                                                                                                                        <asp:RadioButtonList ID="rdbIncentiveType" CellSpacing="5" runat="server" RepeatDirection="Horizontal">
                                                                                                                                                            <asp:ListItem Selected="True" Value="1">Upfront Payment</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Post Payment</asp:ListItem>
                                                                                                                                                        </asp:RadioButtonList></td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                        <asp:Button ID="btnAddPlan" runat="server" CssClass="button" Text="Create Plan" Width="100%"
                                                                                                                                                            OnClick="btnAddPlan_Click" />
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 1px; background-color: Maroon;">
                                                                                                                                                    <td colspan="5" style="height: 1px; background-color: Maroon;">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trUpfrontTypeName" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Payment Term
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 67%" colspan="3">
                                                                                                                                                        <asp:RadioButtonList ID="rdbUpfrontTypeName" runat="server" RepeatDirection="Horizontal"
                                                                                                                                                            Width="100%" CellSpacing="3">
                                                                                                                                                            <asp:ListItem Selected="True" Value="1">One time</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Replinishable</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="3">Fixed</asp:ListItem>
                                                                                                                                                        </asp:RadioButtonList></td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 1px; background-color: Maroon;">
                                                                                                                                                    <td colspan="5" style="height: 1px; background-color: Maroon;">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td colspan="5" class="smallgap">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="TRMinimumSegmentCriteriaNew" runat="server">
                                                                                                                                                    <td colspan="5" style="background-color: White;">
                                                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                            <tr>
                                                                                                                                                                <td>
                                                                                                                                                                    <table border="0" class="displayNone" cellspacing="0" cellpadding="0" width="98%"
                                                                                                                                                                        id="TblMinimumSegmentCriteriaNew" runat="server">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4" align="center" class="subheading">
                                                                                                                                                                                Minimum Segment Criteria <span class="Mandatory">*</span>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                                                                                                                                                                    ID="LnkMinSeg" CssClass="LinkButtons" runat="server" Text="More..."></asp:LinkButton>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4">
                                                                                                                                                                                &nbsp;&nbsp;
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                                &nbsp;&nbsp;</td>
                                                                                                                                                                            <td style="width: 160px;">
                                                                                                                                                                                Minimum Segment
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                &nbsp;</td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:TextBox ID="TxtMinSegCriteriaValueNew" runat="server" CssClass="textbox right"
                                                                                                                                                                                    MaxLength="10" onkeyup="checknumericWithDot(this.id)" Width="131px"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                                &nbsp;</td>
                                                                                                                                                                            <td colspan="3">
                                                                                                                                                                                <asp:CheckBoxList ID="ChkMinimunCriteriaNew" RepeatDirection="Horizontal" Width="100%"
                                                                                                                                                                                    RepeatColumns="4" runat="server">
                                                                                                                                                                                </asp:CheckBoxList>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td colspan="4">
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td align="left" style="width: 26%; height: 13px">
                                                                                                                                                        Sign Up Amount</td>
                                                                                                                                                    <td style="width: 40%; height: 13px">
                                                                                                                                                        <asp:TextBox ID="txtSignUpAmount" runat="server" CssClass="textbox right" MaxLength="10"
                                                                                                                                                            onkeyup="checknumericWithDot(this.id)" Width="131px"></asp:TextBox></td>
                                                                                                                                                    <td style="width: 7%; height: 13px; text-align: right">
                                                                                                                                                        Adjustable
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%; height: 13px; text-align: left">
                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                        <asp:CheckBox ID="chkAdjustable" runat="server" Text="" /></td>
                                                                                                                                                    <td style="width: 7%; height: 13px">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trUpfrontAmount" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Upfront Amount<span class="Mandatory">* </span>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:TextBox ID="txtUpfrontAmount" runat="server" CssClass="textbox right" MaxLength="10"
                                                                                                                                                            Width="131px" onkeyup="checknumericWithDot(this.id)"></asp:TextBox></td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%; text-align: left">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <!--Case and Ariline Data 5 columns-->
                                                                                                                                                <tr id="trNoOfPayments" runat="server" class="displayNone">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        No Of Payments
                                                                                                                                                    </td>
                                                                                                                                                    <td align="left" colspan="3">
                                                                                                                                                        <asp:TextBox ID="txtNoOfPayments" runat="server" CssClass="textbox right" MaxLength="3"
                                                                                                                                                            onkeyup="checknumeric(this.id)" Width="131px"></asp:TextBox></td>
                                                                                                                                                    <td align="left" colspan="1">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trForThePeriodOf" runat="server" class="displayNone">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        For the period of
                                                                                                                                                    </td>
                                                                                                                                                    <td align="left" colspan="3">
                                                                                                                                                        <asp:TextBox ID="txtForThePeriodOf" runat="server" CssClass="textbox right" MaxLength="3"
                                                                                                                                                            onkeyup="checknumeric(this.id)" Width="131px"></asp:TextBox></td>
                                                                                                                                                    <td align="left" colspan="1">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 26%;" align="left">
                                                                                                                                                        Case Name<span class="Mandatory">*</span></td>
                                                                                                                                                    <td align="left" colspan="3">
                                                                                                                                                        <asp:TextBox ID="txtCaseName" MaxLength="60" Width="80%" runat="server"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td align="left" colspan="1">
                                                                                                                                                        <asp:LinkButton ID="lnkPreview" runat="server" CssClass="LinkButtons" OnClick="lnkPreview_Click">Preview</asp:LinkButton></td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trPaymentType">
                                                                                                                                                    <td align="left" style="width: 26%;">
                                                                                                                                                        Adjustment</td>
                                                                                                                                                    <td align="left" colspan="3">
                                                                                                                                                        <asp:RadioButtonList ID="rdbPaymentType" runat="server" RepeatDirection="Horizontal"
                                                                                                                                                            CellSpacing="0" CellPadding="0">
                                                                                                                                                            <asp:ListItem Selected="True" Value="1">Rate</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Fixed</asp:ListItem>
                                                                                                                                                        </asp:RadioButtonList></td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="text-align: left;" nowrap="nowrap" colspan="5">
                                                                                                                                                        <asp:Panel ID="pnlMIDT_Criteria" runat="server" Width="26%">
                                                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td nowrap="nowrap" style="height: 13px">
                                                                                                                                                                        <span class="LinkButtons">Airline Data</span> <span class="Mandatory">* </span>&nbsp;&nbsp;&nbsp;
                                                                                                                                                                        <asp:LinkButton ID="lnkCriteriaSelectionN" CssClass="LinkButtons" runat="server"
                                                                                                                                                                            Text="More..."></asp:LinkButton>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:CheckBoxList ID="chkSelectedCriteria" RepeatDirection="Horizontal" Width="470px"
                                                                                                                                                                            RepeatColumns="4" runat="server">
                                                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                        </asp:Panel>
                                                                                                                                                        <asp:Button ID="btnFalseN" runat="server" CssClass="displayNone" />
                                                                                                                                                        <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                                                                                                                                            DropShadow="false" PopupControlID="pnlMIDT_New" OkControlID="btnOkN" CancelControlID="btnCancelCriteria"
                                                                                                                                                            TargetControlID="btnFalseN" RepositionMode="RepositionOnWindowResizeAndScroll">
                                                                                                                                                        </ajax:ModalPopupExtender>
                                                                                                                                                        <ajax:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                                                                                                                                            DropShadow="false" PopupControlID="pnlAirlineDataPreview" OkControlID="btnOkN"
                                                                                                                                                            CancelControlID="btnClose" TargetControlID="btnFalseN" RepositionMode="RepositionOnWindowResizeAndScroll">
                                                                                                                                                        </ajax:ModalPopupExtender>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td colspan="5" class="smallgap">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <!-- Case and Ariline Data 
                                                                                                                                                <tr style="height: 1px; background-color: Silver;">
                                                                                                                                                    <td colspan="5" style="height: 1px; background-color: Silver;">
                                                                                                                                                    </td>
                                                                                                                                                </tr>-->
                                                                                                                                                <!--  <tr id="trPaymentType" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Adjustment</td>
                                                                                                                                                    <td style="width: 67%" colspan="3">
                                                                                                                                                        </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>-->
                                                                                                                                                <tr id="trSlab" runat="server">
                                                                                                                                                    <td colspan="5" style="width: 100%">
                                                                                                                                                        <asp:Panel ID="pnlSlabsN" runat="server" Width="100%">
                                                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td style="width: 100%;">
                                                                                                                                                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td align="left" style="width: 20%">
                                                                                                                                                                                    Range From<span class="Mandatory">*</span></td>
                                                                                                                                                                                <td style="width: 15%">
                                                                                                                                                                                    <asp:TextBox ID="txtRangeFromN" MaxLength="6" CssClass="right" runat="server" Width="40px"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td align="left" style="width: 8%;">
                                                                                                                                                                                    To
                                                                                                                                                                                </td>
                                                                                                                                                                                <td style="width: 13%;">
                                                                                                                                                                                    <asp:TextBox ID="txtRangeToN" MaxLength="6" runat="server" Width="40px" CssClass="right"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td align="left">
                                                                                                                                                                                    <asp:Label ID="spnAdjAmount" runat="server" Text="Rate"></asp:Label>
                                                                                                                                                                                    <span class="Mandatory">* </span>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td style="width: 12%;">
                                                                                                                                                                                    <asp:TextBox ID="txtSlabAmountN" MaxLength="6" runat="server" Width="40px" CssClass="right"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td style="width: 20%;">
                                                                                                                                                                                    <asp:Button ID="btnAddSlabN" runat="server" Text="Add" CssClass="button" Width="61%"
                                                                                                                                                                                        OnClick="btnAddSlabN_Click" />
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                        </table>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td style="width: 101px; height: 3px;">
                                                                                                                                                                    </td>
                                                                                                                                                                    <td nowrap="nowrap">
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td colspan="3">
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td colspan="3">
                                                                                                                                                                        <!--   <asp:BoundField HeaderText="Amount" DataField="SLABS_RATE">
                                                                                                                                                                                    <HeaderStyle Width="30%" />
                                                                                                                                                                                    <ItemStyle Width="30%" CssClass="right" />
                                                                                                                                                                                </asp:BoundField>-->
                                                                                                                                                                        <asp:GridView ID="grdvSlabN" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                            ShowHeader="true" Width="100%" EnableViewState="true" AllowSorting="false" ShowFooter="false"
                                                                                                                                                                            OnRowCommand="grdvSlabN_RowCommand" OnRowDataBound="grdvSlabN_RowDataBound">
                                                                                                                                                                            <Columns>
                                                                                                                                                                                <asp:BoundField HeaderText="Start" DataField="SLABS_START">
                                                                                                                                                                                    <HeaderStyle Width="35%" />
                                                                                                                                                                                    <ItemStyle Width="35%" CssClass="right" />
                                                                                                                                                                                </asp:BoundField>
                                                                                                                                                                                <asp:BoundField HeaderText="End" DataField="SLABS_END">
                                                                                                                                                                                    <HeaderStyle Width="35%" />
                                                                                                                                                                                    <ItemStyle Width="35%" Wrap="True" CssClass="right" />
                                                                                                                                                                                </asp:BoundField>
                                                                                                                                                                                <asp:TemplateField ItemStyle-Wrap="false">
                                                                                                                                                                                    <HeaderTemplate>
                                                                                                                                                                                        <asp:Label ID="spnAdjAmount" runat="server" Text="Rate"></asp:Label>
                                                                                                                                                                                    </HeaderTemplate>
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:TextBox ID="txtAmount" onkeyup="checknumericWithDot(this.id)" runat="server"
                                                                                                                                                                                            CssClass="textbox right" Text='<%#Eval("SLABS_RATE") %>'></asp:TextBox>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField ItemStyle-Wrap="false">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="LinkButtons" CommandName="EditX"
                                                                                                                                                                                            CommandArgument='<%#Eval("TEMP_SLAB_ID") %>'></asp:LinkButton>
                                                                                                                                                                                        &nbsp;
                                                                                                                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="LinkButtons"
                                                                                                                                                                                            CommandName="DelX" CommandArgument='<%#Eval("TEMP_SLAB_ID") %>'></asp:LinkButton>
                                                                                                                                                                                        <asp:HiddenField ID="hdTEMP_SLAB_ID" runat="server" Value='<%#Eval("TEMP_SLAB_ID") %>' />
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                            </Columns>
                                                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                            <RowStyle CssClass="textbold" />
                                                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                            <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                        </asp:Panel>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 1px; background-color: Maroon;">
                                                                                                                                                    <td colspan="5" style="height: 1px; background-color: Maroon;">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td colspan="5" style="height: 2pt">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <!--PLB Is Here -->
                                                                                                                                                <tr>
                                                                                                                                                    <td colspan="5" style="width: 100%">
                                                                                                                                                        <asp:Panel ID="Panel19" runat="server">
                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td colspan="3">
                                                                                                                                                                        <table border="0" id="tlbCaseNameNew" runat="server" cellspacing="0" cellpadding="0"
                                                                                                                                                                            width="100%">
                                                                                                                                                                        </table>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr class="smallgap">
                                                                                                                                                                    <td colspan="3">
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td colspan="3">
                                                                                                                                                                        <table border="0" id="tlbCriteria" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                                        </table>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr class="smallgap">
                                                                                                                                                                    <td colspan="3">
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td colspan="0" style="width: 100%;">
                                                                                                                                                                        <asp:Panel ID="pnlMIDT_New" runat="server" Width="610px" BackColor="white" CssClass="center">
                                                                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="600px" class="redborder">
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td style="width: 6px; height: 13px;">
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td colspan="2" align="center" style="height: 13px">
                                                                                                                                                                                        <asp:Label ID="lblPopupError" CssClass="ErrorMsg" runat="server" Visible="false"
                                                                                                                                                                                            EnableViewState="False"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td style="height: 13px">
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="4" align="left">
                                                                                                                                                                                        <asp:CheckBoxList ID="chklstCriteria" CssClass="chkboxlist" RepeatDirection="Horizontal"
                                                                                                                                                                                            runat="server" Width="600px" RepeatColumns="5">
                                                                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                                                                        <br />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="4" align="center" style="height: 22px">
                                                                                                                                                                                        <asp:Button ID="btnOkN" runat="server" Text="Ok" Style="display: none" />
                                                                                                                                                                                        <asp:Button ID="btnCriteriaSelection" runat="server" Text="Select Case" CssClass="button" />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Button ID="btnCancelCriteria" runat="server" Text="Cancel" CssClass="button"
                                                                                                                                                                                            OnClick="btnCancelCriteria_Click" />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr class="smallgap">
                                                                                                                                                                                    <td colspan="4">
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                        </asp:Panel>
                                                                                                                                                                        <asp:Panel ID="pnlAirlineDataPreview" runat="Server" Width="100%" BackColor="white"
                                                                                                                                                                            CssClass="center">
                                                                                                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td>
                                                                                                                                                                                        <asp:GridView ID="grdvAirlinePrv" runat="server" BorderWidth="1px" BorderColor="#D4D0C8"
                                                                                                                                                                                            AutoGenerateColumns="true" Width="99%" TabIndex="9" AllowSorting="True">
                                                                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                                            <RowStyle CssClass="ItemColor" HorizontalAlign="right" />
                                                                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="right" />
                                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td>
                                                                                                                                                                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClick="btnClose_Click" />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                        </asp:Panel>
                                                                                                                                                                        <asp:Panel ID="pnlSQMore" runat="server" Width="610px" BackColor="white" CssClass="center">
                                                                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="600px" class="redborder">
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td style="width: 6px; height: 13px;">
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td colspan="2" align="center" style="height: 13px">
                                                                                                                                                                                        <asp:Label ID="LBSQMsg" CssClass="ErrorMsg" runat="server" Visible="true" EnableViewState="False"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td style="height: 13px">
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="4" align="left">
                                                                                                                                                                                        <asp:CheckBoxList ID="chkLstSQMore" CssClass="chkboxlist" RepeatDirection="Horizontal"
                                                                                                                                                                                            runat="server" Width="600px" RepeatColumns="5">
                                                                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                                                                        <br />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="4" align="center" style="height: 22px">
                                                                                                                                                                                        <asp:Button ID="btnSQOK" runat="server" Text="Ok" Style="display: none" />
                                                                                                                                                                                        <asp:Button ID="btnSelectSQ" runat="server" Text="Select Qualification" CssClass="button"
                                                                                                                                                                                            OnClientClick="return MandatoryForQual()" Width="120px" />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Button ID="btnSQCAN" runat="server" Text="Cancel" CssClass="button" />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr class="smallgap">
                                                                                                                                                                                    <td colspan="4">
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                        </asp:Panel>
                                                                                                                                                                        <asp:Panel ID="pnlMSMore" runat="server" Width="610px" BackColor="white" CssClass="center">
                                                                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="600px" class="redborder">
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td style="width: 6px; height: 13px;">
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td colspan="2" align="center" style="height: 13px">
                                                                                                                                                                                        <asp:Label ID="LblMSMsg" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td style="height: 13px">
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="4" align="left">
                                                                                                                                                                                        <asp:CheckBoxList ID="chkLstMSMore" CssClass="chkboxlist" RepeatDirection="Horizontal"
                                                                                                                                                                                            runat="server" Width="600px" RepeatColumns="5">
                                                                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                                                                        <br />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="4" align="center" style="height: 22px">
                                                                                                                                                                                        <asp:Button ID="btnMSOK" runat="server" Text="Ok" Style="display: none" />
                                                                                                                                                                                        <asp:Button ID="btnSelectMS" runat="server" Text="Select Min. Segment" CssClass="button"
                                                                                                                                                                                            Width="125px" OnClientClick="return MandatoryForMS()" />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Button ID="btnMSCAN" runat="server" Text="Cancel" CssClass="button" />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr class="smallgap">
                                                                                                                                                                                    <td colspan="4">
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                        </asp:Panel>
                                                                                                                                                                        <asp:Button ID="BtnFakeMS" runat="server" CssClass="displayNone" />
                                                                                                                                                                        <ajax:ModalPopupExtender ID="ModPExtForMS" runat="server" BackgroundCssClass="modalBackground"
                                                                                                                                                                            DropShadow="false" PopupControlID="pnlMSMore" OkControlID="btnMSOK" CancelControlID="btnMSCAN"
                                                                                                                                                                            TargetControlID="BtnFakeMS" RepositionMode="RepositionOnWindowResizeAndScroll">
                                                                                                                                                                        </ajax:ModalPopupExtender>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                        </asp:Panel>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr class="smallgap">
                                                                                                                                                    <td colspan="5">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                        <td style="height: 21px">
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr class="smallgap">
                                                                                                                                        <td>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td colspan="3">
                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                                                <tr class="">
                                                                                                                                                    <td style="width: 30%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 62%">
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td class="redborder">
                                                                                                                                            <asp:Panel ID="pnlIncentivePlanNew" runat="server" Width="100%">
                                                                                                                                                <asp:GridView ID="grdvIncentivePlan" ShowHeader="False" runat="server" AutoGenerateColumns="False"
                                                                                                                                                    TabIndex="6" Width="99%" EnableViewState="true" AllowSorting="false" CellPadding="0">
                                                                                                                                                    <RowStyle CssClass="textbold" />
                                                                                                                                                    <Columns>
                                                                                                                                                        <asp:TemplateField>
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                                    <tr class="smallgap">
                                                                                                                                                                        <td colspan="3">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td style="width: 16%; height: 21px">
                                                                                                                                                                            <asp:Label ID="lblPlanIfor" runat="server" Text="Case Name  :" CssClass="LinkButtons"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 63%; height: 21px;">
                                                                                                                                                                            <asp:TextBox ID="txtPlanName" Width="150px" MaxLength="60" runat="server" Text='<%#Eval("INC_PLAN_NAME")%>'></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 11%; height: 21px;">
                                                                                                                                                                            <asp:LinkButton ID="lnkDeletePlan" Width="100px" runat="server" Text="Delete Case"
                                                                                                                                                                                CssClass="LinkButtons" CommandName="DelXPlan" CommandArgument='<%# Eval("Case_Id") %>'></asp:LinkButton>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="3">
                                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td style="width: 17%; text-align: left">
                                                                                                                                                                                        <asp:Panel ID="pnlMIDTLabel" CssClass="LinkButtons" runat="server" Width="100%">
                                                                                                                                                                                        </asp:Panel>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td colspan="2" style="width: 83%; text-align: left">
                                                                                                                                                                                        <asp:Button ID="btnFalse" runat="server" CssClass="displayNone" />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td style="text-align: left;" nowrap="nowrap">
                                                                                                                                                                                        <asp:UpdatePanel ID="pnlMidtDetailsCollaps" runat="server">
                                                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                                                <asp:Image ID="imgC" runat="server" ImageUrl="~/Images/Hide_TEMP.jpg" />
                                                                                                                                                                                                <asp:Panel ID="pnlShowCaseDetail" Height="0px" runat="server" Style="overflow: hidden;">
                                                                                                                                                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                                                                                                        <tr>
                                                                                                                                                                                                            <td align="left">
                                                                                                                                                                                                                <asp:CheckBoxList CssClass="chkboxlist" ID="chklstCriteriaNew" RepeatDirection="Horizontal"
                                                                                                                                                                                                                    runat="server" Width="470px" RepeatColumns="5">
                                                                                                                                                                                                                </asp:CheckBoxList>
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                        </tr>
                                                                                                                                                                                                        <tr>
                                                                                                                                                                                                            <td align="right">
                                                                                                                                                                                                                <asp:LinkButton ID="lnkCriteriaSelection" CommandName="MODALPOPUP" CssClass="LinkButtons"
                                                                                                                                                                                                                    CommandArgument='<%#Eval("Case_Id") %>' runat="server" Text="More..."></asp:LinkButton>
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                        </tr>
                                                                                                                                                                                                    </table>
                                                                                                                                                                                                </asp:Panel>
                                                                                                                                                                                                <ajax:CollapsiblePanelExtender ID="ClsPnlMIDTDetails" TextLabelID="lblMIDTDetails"
                                                                                                                                                                                                    CollapsedImage="../Images/show_TEMP.jpg" ExpandedImage="../Images/Hide_TEMP.jpg"
                                                                                                                                                                                                    runat="Server" TargetControlID="pnlShowCaseDetail" ImageControlID="imgC" ExpandControlID="imgC"
                                                                                                                                                                                                    SuppressPostBack="true" CollapseControlID="imgC" Collapsed="false" />
                                                                                                                                                                                            </ContentTemplate>
                                                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="3">
                                                                                                                                                                            <asp:GridView ID="GvIncSlabsNested" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                                OnRowCommand="GvIncSlabsNested_RowCommand" ShowHeader="true" Width="96%" EnableViewState="true"
                                                                                                                                                                                AllowSorting="false" ShowFooter="false">
                                                                                                                                                                                <Columns>
                                                                                                                                                                                    <asp:BoundField HeaderText="Start" DataField="SLABS_START">
                                                                                                                                                                                        <HeaderStyle Width="35%" />
                                                                                                                                                                                        <ItemStyle Width="35%" CssClass="right" />
                                                                                                                                                                                    </asp:BoundField>
                                                                                                                                                                                    <asp:BoundField HeaderText="End" DataField="SLABS_END">
                                                                                                                                                                                        <HeaderStyle Width="35%" />
                                                                                                                                                                                        <ItemStyle Width="35%" Wrap="True" CssClass="right" />
                                                                                                                                                                                    </asp:BoundField>
                                                                                                                                                                                    <%--<asp:BoundField HeaderText="Amount" DataField="SLABS_RATE">
                                                                                                                                                                            <HeaderStyle Width="30%" />
                                                                                                                                                                            <ItemStyle Width="30%" CssClass="right" />
                                                                                                                                                                        </asp:BoundField>--%>
                                                                                                                                                                                    <asp:TemplateField ItemStyle-Wrap="false">
                                                                                                                                                                                        <HeaderTemplate>
                                                                                                                                                                                            <asp:Label ID="spnAdjAmount" runat="server" Text="Rate"></asp:Label>
                                                                                                                                                                                        </HeaderTemplate>
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("SLABS_RATE") %>' onkeyup="checknumericWithDot(this.id)"
                                                                                                                                                                                                CssClass="right"></asp:TextBox>
                                                                                                                                                                                            <asp:HiddenField ID="hdTempID" runat="server" Value='<%#Eval("TEMP_SLAB_ID") %>' />
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <%--<asp:TemplateField ItemStyle-Wrap="false">
                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="LinkButtons" CommandName="EditXNested"
                                                                                                                                                                                    CommandArgument='<%# Convert.ToString(Eval("TEMP_SLAB_ID"))+ "|" + Convert.ToString(Eval("Case_Id")) %>'></asp:LinkButton>
                                                                                                                                                                                &nbsp;
                                                                                                                                                                                <asp:LinkButton ID="lnkDeleteNested" runat="server" Text="Delete" CssClass="LinkButtons"
                                                                                                                                                                                    CommandName="DelXNested" CommandArgument='<%# Convert.ToString(Eval("TEMP_SLAB_ID"))+ "|" + Convert.ToString(Eval("Case_Id")) %>'></asp:LinkButton>
                                                                                                                                                                                <asp:HiddenField ID="hdTEMP_SLAB_ID" runat="server" Value='<%#Eval("TEMP_SLAB_ID") %>' />
                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                        </asp:TemplateField>--%>
                                                                                                                                                                                </Columns>
                                                                                                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                                <RowStyle CssClass="textbold" />
                                                                                                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                                <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                                <asp:HiddenField ID="hdCaseId" runat="server" Value='<%#Eval("Case_Id") %>' />
                                                                                                                                                                <asp:HiddenField ID="hdNestedUpdateFlag" EnableViewState="true" runat="server" />
                                                                                                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                                                                                                    <tr class="smallgap">
                                                                                                                                                                        <td colspan="2">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                    </Columns>
                                                                                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                </asp:GridView>
                                                                                                                                            </asp:Panel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr class="gap">
                                                                                                                                        <td>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr   style ="background-color:#dee7f7;">
                                                                                                                                        <td class="redborder">
                                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; padding-left:2px;">
                                                                                                                                                <tr id="trPlb" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        Is PLB Applicable
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:CheckBox ID="chkPlb" runat="server" Text="" Width="51px" /></td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%; text-align: left">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trPlbTypeName" class="displayNone" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%">
                                                                                                                                                        PLB Type
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%">
                                                                                                                                                        <asp:RadioButtonList ID="rdbPlbTypeName" runat="server" RepeatDirection="Horizontal">
                                                                                                                                                            <asp:ListItem Selected="True" Value="1">Fixed</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Slab Based</asp:ListItem>
                                                                                                                                                        </asp:RadioButtonList></td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%; text-align: left">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trSlabPlb" runat="server">
                                                                                                                                                    <td colspan="5" style="width: 100%">
                                                                                                                                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                                                                                                                            <tr>
                                                                                                                                                                <td style="width: 100%;">
                                                                                                                                                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td align="left" style="width: 20%">
                                                                                                                                                                                Range From<span class="Mandatory">*</span></td>
                                                                                                                                                                            <td style="width: 15%">
                                                                                                                                                                                <asp:TextBox ID="txtPlbSlabFromN" MaxLength="6" CssClass="right" runat="server" Width="40px"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="left" style="width: 8%;">
                                                                                                                                                                                To
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="width: 13%;">
                                                                                                                                                                                <asp:TextBox ID="txtPlbSlabToN" MaxLength="6" runat="server" Width="40px" CssClass="right"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td align="left">
                                                                                                                                                                                Amount<span class="Mandatory">* </span>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="width: 12%;">
                                                                                                                                                                                <asp:TextBox ID="txtPlbSlabAmountN" MaxLength="6" runat="server" Width="40px" CssClass="right"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td style="width: 20%;">
                                                                                                                                                                                <asp:Button ID="btnPlbSlabN" runat="server" Text="Add" CssClass="button" Width="61%"
                                                                                                                                                                                    OnClick="btnPlbSlabN_Click" />
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td style="width: 101px; height: 2pt">
                                                                                                                                                                </td>
                                                                                                                                                                <td nowrap="nowrap">
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td colspan="3">
                                                                                                                                                                    <asp:GridView ID="grdvPlbSlabN" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                        OnRowCommand="grdvPlbSlabN_RowCommand" ShowHeader="true" Width="100%" EnableViewState="true"
                                                                                                                                                                        AllowSorting="false" ShowFooter="false">
                                                                                                                                                                        <Columns>
                                                                                                                                                                            <asp:BoundField HeaderText="Start" DataField="SLABS_START">
                                                                                                                                                                                <HeaderStyle Width="35%" />
                                                                                                                                                                                <ItemStyle Width="35%" CssClass="right" />
                                                                                                                                                                            </asp:BoundField>
                                                                                                                                                                            <asp:BoundField HeaderText="End" DataField="SLABS_END">
                                                                                                                                                                                <HeaderStyle Width="35%" />
                                                                                                                                                                                <ItemStyle Width="35%" Wrap="True" CssClass="right" />
                                                                                                                                                                            </asp:BoundField>
                                                                                                                                                                            <asp:BoundField HeaderText="Amount" DataField="SLABS_RATE">
                                                                                                                                                                                <HeaderStyle Width="30%" />
                                                                                                                                                                                <ItemStyle Width="30%" CssClass="right" />
                                                                                                                                                                            </asp:BoundField>
                                                                                                                                                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="LinkButtons" CommandName="EditX"
                                                                                                                                                                                        CommandArgument='<%#Eval("PLBTYPEID_TEMP") %>'></asp:LinkButton>
                                                                                                                                                                                    &nbsp;
                                                                                                                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="LinkButtons"
                                                                                                                                                                                        CommandName="DelX" CommandArgument='<%#Eval("PLBTYPEID_TEMP") %>'></asp:LinkButton>
                                                                                                                                                                                    <asp:HiddenField ID="hdTEMP_SLAB_ID" runat="server" Value='<%#Eval("PLBTYPEID_TEMP") %>' />
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                        </Columns>
                                                                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                        <RowStyle CssClass="textbold" />
                                                                                                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                        <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                    </asp:GridView>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr id="trPlbAmount" runat="server">
                                                                                                                                                    <td align="left" style="width: 26%; height: 24px;">
                                                                                                                                                        PLB Bonus Amount
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 40%; height: 24px;">
                                                                                                                                                        <asp:TextBox ID="txtPlbBonus" runat="server" CssClass="textbox right" MaxLength="6"
                                                                                                                                                            Width="131px" onkeyup="checknumericWithDot(this.id)"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%; height: 24px;">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 20%; text-align: left; height: 24px;">
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 7%; height: 24px;">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr class="smallgap">
                                                                                                                                                    <td colspan="5">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr class="gap">
                                                                                                                            <td>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td colspan="0" style="width: 100%;" align="left" valign="top">
                                                                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 100%;" id="tdUpdtConversionPer" runat="server">
                                                                                                                                            <asp:UpdatePanel ID="UpdtConversionPer" runat="server">
                                                                                                                                                <ContentTemplate>
                                                                                                                                                    <table width="98%" class="redborder">
                                                                                                                                                        <tr>
                                                                                                                                                            <td>
                                                                                                                                                                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td style="width: 25%">
                                                                                                                                                                            <strong>Commitment</strong> <span class="Mandatory displayNone">* </span>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 20%">
                                                                                                                                                                            <asp:TextBox ID="txtConversionPer" MaxLength="3" runat="server" CssClass="textbox right"
                                                                                                                                                                                onblur="ShowWait('1');" AutoPostBack="true" TabIndex="6" Width="80px"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 30%" class="center">
                                                                                                                                                                            <asp:Button ID="btnReCalulate" runat="server" CssClass="button" OnClick="btnReCalculate_Click"
                                                                                                                                                                                Width="100px" OnClientClick="ShowWait('2');" TabIndex="6" Text="Recalculate" />
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 25%">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="3">
                                                                                                                                                                            <asp:HiddenField ID="HdConversionPer" runat="server" Value="" />
                                                                                                                                                                            <asp:Panel CssClass="displayNone" ID="PnlConversionMsg" runat="server" HorizontalAlign="Center">
                                                                                                                                                                                <asp:Label ID="LblConversionMsg" runat="server" CssClass="txtcolor" Text="...Recalculating..."></asp:Label>
                                                                                                                                                                            </asp:Panel>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td>
                                                                                                                                                                <table border="1" cellspacing="0" cellpadding="2" width="100%">
                                                                                                                                                                    <tr class="Gridheading">
                                                                                                                                                                        <td style="width: 58%" colspan="3">
                                                                                                                                                                            BREAKUP OF LAST 3 MONTHS &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;%
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 12%">
                                                                                                                                                                            10% less</td>
                                                                                                                                                                        <td style="width: 12%" class="center">
                                                                                                                                                                            RATE</td>
                                                                                                                                                                        <td style="width: 18%">
                                                                                                                                                                            AMOUNT</td>
                                                                                                                                                                        <td style="width: 0%" class="displayNone">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="7" style="width: 100%;">
                                                                                                                                                                            <asp:GridView ID="grdvBreakUpLast" ShowHeader="false" ShowFooter="true" AutoGenerateColumns="false"
                                                                                                                                                                                TabIndex="7" runat="server" Width="100%" OnRowDataBound="grdvBreakUpLast_RowDataBound">
                                                                                                                                                                                <Columns>
                                                                                                                                                                                    <asp:TemplateField HeaderText="BREAKUP ON THE BASIS OF LAST 3 MONTHS" HeaderStyle-Width="25%"
                                                                                                                                                                                        ItemStyle-Width="25%">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <%#Eval("TYPE")%>
                                                                                                                                                                                            <asp:HiddenField ID="hdType" runat="server" Value='<%#Eval("TYPE")%>' />
                                                                                                                                                                                            <asp:HiddenField ID="hdbreakID" runat="server" Value='<%#Eval("BR_ID") %>' />
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:BoundField DataField="MIDT_CONV_PER" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                                                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <%#Eval("PER")%>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                        <HeaderStyle Width="13%" />
                                                                                                                                                                                        <ItemStyle Width="13%" />
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderText="10% less">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="lbltenPerLess" runat="server" Text='<%#Eval("LESS")%>'></asp:Label>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                        <HeaderStyle Width="12%" />
                                                                                                                                                                                        <ItemStyle Width="12%" />
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderText="Rate">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:TextBox ID="txtBreakupRate" Width="50px" CssClass="right" MaxLength="5" runat="server"
                                                                                                                                                                                                Text='<%#Eval("RATE")%>'></asp:TextBox>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                        <HeaderStyle Width="12%" />
                                                                                                                                                                                        <ItemStyle Width="12%" />
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderText="Amount">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="lblbrAmount" runat="server" Text='<%#Eval("AMT") %>'></asp:Label>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                        <HeaderStyle Width="18%" />
                                                                                                                                                                                        <ItemStyle Width="18%" />
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:BoundField DataField="TOTAL" ItemStyle-Width="0%" ItemStyle-CssClass="displayNone"
                                                                                                                                                                                        HeaderStyle-CssClass="displayNone" FooterStyle-CssClass="displayNone" ControlStyle-CssClass="displayNone" />
                                                                                                                                                                                </Columns>
                                                                                                                                                                                <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                                <table border="1" cellspacing="0" cellpadding="3" width="100%" style="border-collapse: collapse;">
                                                                                                                                                                    <tr class="gap">
                                                                                                                                                                        <td style="width: 58%">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 24%">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 22%">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr class="Gridheading gap">
                                                                                                                                                                        <td style="width: 58%">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 24%">
                                                                                                                                                                            Segment
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 22%">
                                                                                                                                                                            %age
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr class="gap">
                                                                                                                                                                        <td style="width: 58%">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 24%">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 22%">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td style="width: 58%">
                                                                                                                                                                            <b>SEGMENTS LESS IC</b></td>
                                                                                                                                                                        <td align="right" style="width: 24%">
                                                                                                                                                                            <asp:Label ID="lblSegLessICValNew" runat="server" Text=""></asp:Label></td>
                                                                                                                                                                        <td align="right" style="width: 22%">
                                                                                                                                                                            <asp:Label ID="lblSegLessICPerNew" runat="server" Text=""></asp:Label></td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                            <strong>SEGMENTS LESS IT/IC</strong></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                            <asp:Label ID="lblSegLessICITValNew" runat="server"></asp:Label></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                            <asp:Label ID="lblSegLessICITPerNew" runat="server"></asp:Label></td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr class="Gridheading gap">
                                                                                                                                                                        <td style="width: 58%">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 24%">
                                                                                                                                                                            Rate
                                                                                                                                                                        </td>
                                                                                                                                                                        <td style="width: 22%">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                            <strong>INCENTIVE RATE (GROSS)</strong></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                            <asp:Label ID="lblIncRateGrossVal" runat="server"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                            <asp:Label ID="lblIncRateGrossPer" runat="server" Width="36px" Visible="false"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                            <b>INCENTIVE RATE (WO IC)</b></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                            <asp:Label ID="lblIncRateWOIC" runat="server"></asp:Label></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                            <strong>INCENTIVE RATE (WO IC IT)</strong></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                            <asp:Label ID="lblIncRateWOICIT" runat="server"></asp:Label></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                            <b>INCENTIVE RATE (NET)</b></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                            <asp:Label ID="lblIncRateNet" runat="server"></asp:Label></td>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td style="height: 126px; width: 100%;" colspan="3">
                                                                                                                                                                            <table border="1" cellspacing="0" cellpadding="3" width="100%" style="border-collapse: collapse;">
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td style="width: 40%" colspan="2">
                                                                                                                                                                                        <b>TOTAL COST</b></td>
                                                                                                                                                                                    <td style="width: 30%" align="left">
                                                                                                                                                                                        <b>
                                                                                                                                                                                            <asp:Label ID="lblTotalCostNew" runat="server"></asp:Label></b></td>
                                                                                                                                                                                    <td style="width: 30%">
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="2">
                                                                                                                                                                                        <b>MIN MONTHLY SEGMENT</b></td>
                                                                                                                                                                                    <td align="left">
                                                                                                                                                                                        <asp:TextBox ID="txtMinMonthSegNew" MaxLength="6" runat="server" CssClass="right"
                                                                                                                                                                                            Width="50px"></asp:TextBox></td>
                                                                                                                                                                                    <td>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr class="Gridheading">
                                                                                                                                                                                    <td colspan="4">
                                                                                                                                                                                        <b>CPS FOR MULTIPLE RATES</b></td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td align="center">
                                                                                                                                                                                        <b>GROSS</b></td>
                                                                                                                                                                                    <td align="center">
                                                                                                                                                                                        <strong>EXCL. IC</strong></td>
                                                                                                                                                                                    <td align="center">
                                                                                                                                                                                        <b>EXCL. IC/IT</b></td>
                                                                                                                                                                                    <td align="center">
                                                                                                                                                                                        <b>NET</b></td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr align="right">
                                                                                                                                                                                    <td align="center">
                                                                                                                                                                                        <asp:Label ID="lblGrossNew" runat="server"></asp:Label></td>
                                                                                                                                                                                    <td align="center">
                                                                                                                                                                                        <asp:Label ID="lblCPSMultiRateICNew" runat="server" CssClass="textboxright"></asp:Label></td>
                                                                                                                                                                                    <td align="center">
                                                                                                                                                                                        <asp:Label ID="lblCPSMultiRateICITNew" runat="server"></asp:Label></td>
                                                                                                                                                                                    <td align="center">
                                                                                                                                                                                        <asp:Label ID="lblNetNew" runat="server"></asp:Label></td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                        </tr>
                                                                                                                                                    </table>
                                                                                                                                                    </td> </tr> </table>
                                                                                                                                                </ContentTemplate>
                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                            <ajax:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
                                                                                                                                                TargetControlID="UpdtConversionPer">
                                                                                                                                                <Animations>
                                                                                                                                                    <OnUpdating>
                                                                                                                                                        <Sequence>
                                                                                                                                                            <%-- Store the original height of the panel --%>
                                                                                                                                                            <ScriptAction Script="var b = $find('animation'); b._originalHeight = b._element.offsetHeight;" />
                                                                                                                                                            
                                                                                                                                                            <%-- Disable all the controls --%>
                                                                                                                                                           <Parallel duration="0">
                                                                                                                                                            <EnableAction AnimationTarget="btnReCalulate" Enabled="false" />
                                                                                                                                                            <EnableAction AnimationTarget="txtConversionPer" Enabled="false" />
                                                                                                                                                           
                                                                                                                                                           </Parallel>  
                                                                                                                                                           <StyleAction Attribute="overflow" Value="hidden" />
                                                                                                                                                    
                                                                                                                                                        <%-- Do each of the selected effects --%>
                                                                                                                                                        <Parallel duration=".25" Fps="30">
                                                                                                                                                               <%-- <FadeOut AnimationTarget="tdUpdtConversionPer" minimumOpacity=".2" />  --%>
                                                                                                                                                                <Color AnimationTarget="up_container" PropertyKey="backgroundColor"
                                                                                                                                                                        EndValue="#dde7f2" StartValue="#d3d3d3" />
                                                                                                                                                                 <Condition ConditionScript="document.getElementById ('HdConversionPer').value!= document.getElementById ('txtConversionPer').value">
                                                                                                                                                                   <Color AnimationTarget="up_container" PropertyKey="backgroundColor"
                                                                                                                                                                        EndValue="#dde7f2" StartValue="#d3d3d3" />
                                                                                                                                                                </Condition>                                                                                                                                                                                                                                         
                                                                                                                                                        </Parallel>
                                                                                                                                                </Sequence>
                                                                                                                                            </OnUpdating>
                                                                                                                                            <OnUpdated>
                                                                                                                                                <Sequence>
                                                                                                                                                    <%-- Do each of the selected effects --%>
                                                                                                                                                    <Parallel duration=".25" Fps="30"> 
                                                                                                                                                         <%--<FadeIn AnimationTarget="tdUpdtConversionPer" minimumOpacity=".2" />     --%>   
                                                                                                                                                          <Color AnimationTarget="tdUpdtConversionPer" PropertyKey="backgroundColor"
                                                                                                                                                                        StartValue="#dde7f2" EndValue="#d3d3d3" />                                                                         
                                                                                                                                                             <Condition ConditionScript="document.getElementById ('PnlConversionMsg').className=='displayBlock modalPopupConv'">
                                                                                                                                                                 <Color AnimationTarget="tdUpdtConversionPer" PropertyKey="backgroundColor"
                                                                                                                                                                        StartValue="#dde7f2" EndValue="#d3d3d3" />                                                                                                                                                                       
                                                                                                                                                            </Condition>
                                                                                                                                                         <%--    <FadeIn AnimationTarget="tdUpdtConversionPer" minimumOpacity=".2" /> --%>
                                                                                                                                                    </Parallel>                                                                                                                                                    
                                                                                                                                                     <%-- Enable all the controls --%>
                                                                                                                                                    <Parallel duration="0">
                                                                                                                                                        <EnableAction AnimationTarget="btnReCalulate" Enabled="true" />
                                                                                                                                                        <EnableAction AnimationTarget="txtConversionPer" Enabled="true" />
                                                                                                                                                       
                                                                                                                                                    </Parallel>  
                                                                                                                                                </Sequence>
                                                                                                                                            </OnUpdated>
                                                                                                                                                </Animations>
                                                                                                                                            </ajax:UpdatePanelAnimationExtender>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <!-- Section for Gridview Criteria Selection -->
                                                                                                                        <tr>
                                                                                                                            <td colspan="3" style="height: 10pt;">
                                                                                                                                <table border="0" cellspacing="0" id="tlbGvMidtSelection" runat="server" cellpadding="0"
                                                                                                                                    width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 125px">
                                                                                                                                        </td>
                                                                                                                                        <td colspan="2" align="left">
                                                                                                                                            <asp:Button ID="btngvFake1" runat="server" CssClass="displayNone" />
                                                                                                                                            <ajax:ModalPopupExtender ID="ModalForGvSelection" runat="server" BackgroundCssClass="modalBackground"
                                                                                                                                                DropShadow="false" PopupControlID="PnlGvMIDT1" OkControlID="btnFakesGvShow" CancelControlID="btnGvCancel1"
                                                                                                                                                TargetControlID="btngvFake1" RepositionMode="RepositionOnWindowResizeAndScroll">
                                                                                                                                            </ajax:ModalPopupExtender>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td colspan="0" style="width: 100%;" align="left">
                                                                                                                                            <asp:Panel ID="PnlGvMIDT1" runat="server" Width="610px" BackColor="white">
                                                                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="600px" class="redborder">
                                                                                                                                                    <tr>
                                                                                                                                                        <td>
                                                                                                                                                        </td>
                                                                                                                                                        <td colspan="2" align="center">
                                                                                                                                                            <asp:Label ID="lblErrorGrdvCriteria" runat="server" CssClass="Error"></asp:Label>
                                                                                                                                                        </td>
                                                                                                                                                        <td>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr>
                                                                                                                                                        <td colspan="4" align="left">
                                                                                                                                                            <asp:CheckBoxList ID="chkLstGvShowMIDT" CssClass="chkboxlist" RepeatDirection="Horizontal"
                                                                                                                                                                runat="server" Width="600px" RepeatColumns="5">
                                                                                                                                                            </asp:CheckBoxList>
                                                                                                                                                            <br />
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr>
                                                                                                                                                        <td colspan="4" align="center" style="height: 22px">
                                                                                                                                                            <asp:Button ID="btnFakesGvShow" runat="server" Text="Ok" Style="display: none" />
                                                                                                                                                            <asp:Button ID="btnGvSelection" runat="server" Text="Select Case" CssClass="button" />
                                                                                                                                                            &nbsp;&nbsp;&nbsp;
                                                                                                                                                            <asp:Button ID="btnGvCancel1" runat="server" OnClientClick="return HideModalPopupExtenderForGridviewModalpopup();"
                                                                                                                                                                Text="Cancel" CssClass="button" />
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr class="smallgap">
                                                                                                                                                        <td colspan="3">
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </asp:Panel>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <!-- End of Section for Gridview Criteria Selection -->
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 1186px">
                                                                                                    <!-- Business case starts -->
                                                                                                    <asp:Panel ID="pnlLocation" runat="server" Width="100%" CssClass="displayNone">
                                                                                                        <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                            <tr>
                                                                                                                <td colspan="3" style="width: 100%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="3" style="width: 100%">
                                                                                                                    <asp:GridView ID="gvAgencyLoc" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                        ShowHeader="true" Width="1000px" EnableViewState="true" AllowSorting="True" ShowFooter="true">
                                                                                                                        <Columns>
                                                                                                                            <asp:BoundField HeaderText="LCode" DataField="LCODE" SortExpression="LCODE">
                                                                                                                                <HeaderStyle Width="15%" />
                                                                                                                                <ItemStyle Width="15%" />
                                                                                                                            </asp:BoundField>
                                                                                                                            <asp:BoundField HeaderText="Agency Name" DataField="NAME" SortExpression="NAME">
                                                                                                                                <HeaderStyle Width="15%" />
                                                                                                                                <ItemStyle Width="15%" />
                                                                                                                            </asp:BoundField>
                                                                                                                            <asp:BoundField HeaderText="Office Id" DataField="OFFICEID" SortExpression="OFFICEID">
                                                                                                                                <HeaderStyle Width="9%" />
                                                                                                                                <ItemStyle Wrap="True" />
                                                                                                                            </asp:BoundField>
                                                                                                                            <asp:BoundField HeaderText="Address" DataField="ADDRESS" SortExpression="ADDRESS">
                                                                                                                                <ItemStyle Width="30%" />
                                                                                                                            </asp:BoundField>
                                                                                                                            <asp:BoundField HeaderText="Country" DataField="COUNTRY" SortExpression="COUNTRY">
                                                                                                                                <HeaderStyle Width="6%" />
                                                                                                                            </asp:BoundField>
                                                                                                                            <asp:BoundField HeaderText="City" DataField="CITY" SortExpression="CITY">
                                                                                                                                <HeaderStyle Width="6%" />
                                                                                                                            </asp:BoundField>
                                                                                                                        </Columns>
                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                        <RowStyle CssClass="textbold" />
                                                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                        <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                    </asp:GridView>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="3" style="width: 100%">
                                                                                                                    <asp:Panel ID="pnlPagingLoc" runat="server" Visible="false" Width="80%">
                                                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                            <tr class="paddingtop paddingbottom">
                                                                                                                                <td style="width: 38%" class="left">
                                                                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                                                                        ID="txtLocTotalRecordCount" runat="server" Width="70px" CssClass="textboxgrey"
                                                                                                                                        ReadOnly="True"></asp:TextBox></td>
                                                                                                                                <td style="width: 20%" class="right">
                                                                                                                                    <asp:LinkButton ID="lnkLocPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"
                                                                                                                                        OnClick="lnkLocPrev_Click"><< Prev</asp:LinkButton></td>
                                                                                                                                <td style="width: 25%" class="center">
                                                                                                                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlLocPageNumber"
                                                                                                                                        Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLocPageNumber_SelectedIndexChanged">
                                                                                                                                    </asp:DropDownList></td>
                                                                                                                                <td style="width: 25%" class="left">
                                                                                                                                    <asp:LinkButton ID="lnkLocNext" runat="server" CssClass="LinkButtons" CommandName="Next"
                                                                                                                                        OnClick="lnkLocNext_Click">Next >></asp:LinkButton></td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </asp:Panel>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3" class="gap" style="height: 8pt; width: 1186px;">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3" style="height: 10pt;">
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <!-- Business case Ends -->
                                                                                <tr>
                                                                                    <td>
                                                                                        <input type="hidden" runat="server" id="hdCalculateBreakUp" style="width: 2px" />
                                                                                        <input type="hidden" runat="server" id="hdRefreshAction" style="width: 2px" />
                                                                                        <input type="hidden" runat="server" id="hdEnChainCode" style="width: 21px" />
                                                                                        <input type="hidden" runat="server" id="hdChainCode" style="width: 21px" />
                                                                                        <asp:HiddenField ID="hdBcID" runat="server" />
                                                                                        <asp:HiddenField ID="hdConIDCollection" runat="server" />
                                                                                        <asp:HiddenField ID="hdHwIDCollection" runat="server" />
                                                                                        <asp:HiddenField ID="hdCreatePlanFlag" runat="server" />
                                                                                        <asp:TextBox ID="hdTabID" runat="server"></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdIncentiveType" runat="server" />
                                                                                        <asp:HiddenField ID="hdAdvanceSearch" EnableViewState="true" runat="server" />
                                                                                        <asp:HiddenField ID="hdFlagCreatePlan" EnableViewState="true" Value="0" runat="server" />
                                                                                        <asp:HiddenField ID="hdMidtGvClick" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdTotalMIDT" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdITvalue" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdICvalue" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdBreakupFromDt" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdBreakupToDt" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdRecordOnCurrentPage" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdRecordOnCurrentPageBidt" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdAction" runat="server" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hdEnableDisableCase" runat="server" EnableViewState="true" />
                                                                                        <asp:TextBox ID="txtHiddenBidtFlag" runat="server"></asp:TextBox>
                                                                                         <asp:HiddenField ID="HdIsFinalApproval" runat="server" EnableViewState="true" />
                                                                                           <asp:HiddenField ID="HdIsApproval" runat="server" EnableViewState="true" />
                                                                                    </td>
                                                                                </tr>
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
                </td>
            </tr>
        </table>

        <script src="../JavaScript/AAMS_BC.js" type="text/javascript"></script>

    </form>
</body>

<script type="text/javascript">
function ShowWait(Type)
{
     
   try
     {         
         if (document.getElementById ('PnlConversionMsg') !=null)
         {
         
           if (document.getElementById ('HdConversionPer') !=null)
           {
                if (document.getElementById ('txtConversionPer') !=null)
                {
                      if (Type=="1")
                      {
                         if (document.getElementById ('HdConversionPer').value!= document.getElementById ('txtConversionPer').value)
                           {
                           document.getElementById ('PnlConversionMsg').className ="displayBlock modalPopupConv";
                           }
                      }
                      else
                      {
                           document.getElementById ('PnlConversionMsg').className ="displayBlock modalPopupConv";
                      }                  
                }
           }
         } 
     }
      catch(err){alert(err)}  
}
    CheckOrUnckeckItemFromMinimunCriteria('ChkMinimunCriteriaNew');
    CheckOrUnckeckItemFromMinimunCriteria('chkLstMSMore');
    CheckOrUnckeckItemFromQlaificationModal();
</script>

</html>
