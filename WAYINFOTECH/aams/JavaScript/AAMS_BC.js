//End of For Old Plan Type





function TabMethodAgencySubGroup(id,total)
{  


try
{
        var ctextSubFront;
        var ctextSubBack;
        var HSubcontrol;
        var HSubFlush;
        var strRefreshAction="";
        
        try
        {
            strRefreshAction=document.getElementById("hdRefreshAction").value;
        }
        catch(err){}
        
        ctextSubFront = id.substring(0,18);        
        ctextSubBack = id.substring(20,29);   
       
        for(var i=0;i<total;i++)
        {
            HSubFlush = "0" + i;
            HSubcontrol = ctextSubFront +  HSubFlush + ctextSubBack;
            if (document.getElementById(HSubcontrol).className != "displayNone")
            {
                document.getElementById(HSubcontrol).className="headingtabactive";
            }
           
        }
       
       var strChain_Code="";
       var strBC_ID="";
       var strACTION="";
       
        strChain_Code = document.getElementById('hdEnChainCode').value;
        strBC_ID=document.getElementById("hdBcID").value;
        strACTION=document.getElementById("hdAction").value;
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblSubPanelClick').value =id; 
       
    
      
        if (id == (ctextSubFront +  "00" + ctextSubBack))
       {   
            //hdTabID  pnlBusinessCase
            document.getElementById('hdTabID').value='0';
            document.getElementById('pnlDetails').className='displayNone';    
            document.getElementById('pnlBusinessCase').className='displayBlock'; 
            document.getElementById('pnlLocation').className='displayNone'; 
            
            return false;
       } 
       
       else if (id == (ctextSubFront +  "01" + ctextSubBack))
       {   
            document.getElementById('hdTabID').value='1';
            document.getElementById('pnlDetails').className='displayBlock';    
            document.getElementById('pnlBusinessCase').className='displayNone'; 
            document.getElementById('pnlLocation').className='displayNone'; 
            return false;                      
       }      
        else if (id == (ctextSubFront +  "02" + ctextSubBack))
       {   
            document.getElementById('hdTabID').value='2';
            document.getElementById('pnlDetails').className='displayNone';    
            document.getElementById('pnlBusinessCase').className='displayNone'; 
            document.getElementById('pnlLocation').className='displayBlock'; 
            document.getElementById("lblError").innerHTML="Loading...";
          
            
          //  return false;                      
       }         
       else if (id == (ctextSubFront +  "03" + ctextSubBack))
       {   
           if (strBC_ID.trim()!="")
           {
                document.getElementById('hdTabID').value='3';
                location.href="INCUP_BacseDetails_Remarks.aspx?TabID=0&Chain_Code=" + strChain_Code + "&BCaseID="+strBC_ID + "&Action=" +strACTION + "&RefreshAction=" +  strRefreshAction ; 
            }
                return false;                      
       }
       
        else if (id == (ctextSubFront +  "04" + ctextSubBack))
       {
           if (strBC_ID.trim()!="")
           {
          
             document.getElementById('hdTabID').value='4';
             location.href="INCUP_BacseDetails_Remarks.aspx?TabID=1&Chain_Code=" + strChain_Code + "&BCaseID="+strBC_ID  + "&Action=" +strACTION + "&RefreshAction=" +  strRefreshAction ;  
           }
             return false;                      
       }
       
       
        else if (id == (ctextSubFront +  "05" + ctextSubBack))
       {   
           if (strBC_ID.trim()!="")
           {
       
               document.getElementById('hdTabID').value='5';
               location.href="INCUP_BacseDetails_Remarks.aspx?TabID=2&Chain_Code=" + strChain_Code + "&BCaseID="+strBC_ID + "&Action=" +strACTION + "&RefreshAction=" +  strRefreshAction ;  
            }
               return false;                    
       }
       
       
     }  catch(err)
    			{
    			}  
      
}

function tabSelection()
{
try
{


// Code Modified on December 28

// Code Modified on December 28
    CheckOrUnckeckItemFromMinimunCriteria('ChkMinimunCriteriaNew');
    CheckOrUnckeckItemFromMinimunCriteria('chkLstMSMore');
 CheckOrUnckeckItemFromQlaificationModal();
document.getElementById("hdFlagCreatePlan").value="0";

        if(document.getElementById('hdTabID').value=='0')
        {
               document.getElementById('pnlDetails').className='displayNone';    
               document.getElementById('pnlBusinessCase').className='displayBlock';  
                document.getElementById('pnlLocation').className='displayNone';                  
                 document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
        }
        else if(document.getElementById('hdTabID').value=='1')
        {
                document.getElementById('pnlDetails').className='displayBlock';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayNone'; 
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
                 
        }
         else if(document.getElementById('hdTabID').value=='2')
        {       
                document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                  document.getElementById('pnlLocation').className='displayBlock'; 
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtab"                
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
        }
         else if(document.getElementById('hdTabID').value=='3')
        {       
                 document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayNone'; 
               
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
         }
        
         else if(document.getElementById('hdTabID').value=='4')
        {
                               
                 document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayNone'; 
                
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
        }
        
         else if(document.getElementById('hdTabID').value=='5')
        {
                               
                 document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayNone'; 
                
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtab"
                 
        }
        
    
}
    catch(err)
    {
    }
}



            
            
 

try
{


//startcode for slab qualifiucation

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
                        
                        //end code for salb qualification


                            var incTypeVal=GetCheckedVal('rdbIncentiveType');  
                             var incTypeVal_Upfront=GetCheckedVal('rdbUpfrontTypeName'); 
                             
                             var incTypeValOld=GetCheckedVal('rdbIncentiveTypeOld');  
                             var incTypeVal_UpfrontOld=GetCheckedVal('rdbUpfrontTypeNameOld'); 
                             
                             document.getElementById("trForThePeriodOf").className="displayNone";
                             document.getElementById("trNoOfPayments").className="displayNone";
                             try
                             {
                                document.getElementById("trForThePeriodOfOld").className="displayNone";
                                document.getElementById("trNoOfPaymentsOld").className="displayNone";
                             }
                             catch(err){}
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
                            
                            if (incTypeValOld=="1")
                            {
                                if (incTypeVal_UpfrontOld=="1")
                                {
                                    document.getElementById("trForThePeriodOfOld").className="displayBlock";
                                                
                                }
                                if (incTypeVal_UpfrontOld=="3")
                                {
                                   document.getElementById("trNoOfPaymentsOld").className="displayBlock";
                                    
                                }
                            }
                            
                            
                            
                            



        if(document.getElementById('hdTabID').value=='0')
        {
               document.getElementById('pnlDetails').className='displayNone';    
               document.getElementById('pnlBusinessCase').className='displayBlock';        
                 document.getElementById('pnlLocation').className='displayNone'; 
                          
                 document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
                 
                 
        }
        else if(document.getElementById('hdTabID').value=='1')
        {
                document.getElementById('pnlDetails').className='displayBlock';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayNone'; 
                
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
                 
        }
         else if(document.getElementById('hdTabID').value=='2')
        {       
                document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayBlock'; 
                 
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
        }
         else if(document.getElementById('hdTabID').value=='3')
        {       
                 document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayNone'; 
                
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
         }
        
         else if(document.getElementById('hdTabID').value=='4')
        {
                               
                 document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayNone'; 
                
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
        }
        
        else if(document.getElementById('hdTabID').value=='5')
        {
                               
                 document.getElementById('pnlDetails').className='displayNone';    
                document.getElementById('pnlBusinessCase').className='displayNone';
                 document.getElementById('pnlLocation').className='displayNone'; 
                
                document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtab"
                 
        }
        
        
                                document.getElementById('pnlLabelUpfront').className="displayNone";
    			                document.getElementById('pnlUpfront').className="displayNone";
    			                
    			                
    			                
    	
    	
    	
    	                              
    			                
    			
    	}
    			
    			
    			
    			
    			
    			
    			
    			
    			
    	
    			
    			
    			 catch(err)
    			{
    			}
    			
    			
    		
      
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


        
        
        
       
        
    
      