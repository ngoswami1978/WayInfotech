 //Start of New Added Code
  
  function fillBRCategoryTechnical()
   {
      document.getElementById('ddlQueryCategory').options.length=0;
      document.getElementById('ddlQuerySubCategory').options.length=0;
      id=document.getElementById('ddlQuerySubGroup').value;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="--Select One--";
	  document.getElementById('ddlQuerySubCategory').options[0]=new Option(names, codes );
	  var ddlQueryCategory = document.getElementById('ddlQueryCategory');
	  if (document.getElementById("hdCategory").value=="") 
            {
             listItem = new Option(names, codes );
             ddlQueryCategory.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(document.getElementById("hdCategory").value);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			     var item=document.getElementById("ddlQuerySubGroup").selectedIndex;
			     var text1=document.getElementById("ddlQuerySubGroup").options[item].text;		
			     //<HD_CC><CC CCI='' CCN='' CSGN='' />
			    //var orders = dsRoot.getElementsByTagName("CALL_CATEGORY[@CALL_SUB_GROUP_NAME='" + text1 + "']");
			    var orders = dsRoot.getElementsByTagName("CC[@CSGN='" + text1 + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQueryCategory.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
			        
				    codes= orders[count].getAttribute("CCI"); 
			        names=orders[count].getAttribute("CCN"); 
				    listItem = new Option(names, codes);
				    ddlQueryCategory.options[ddlQueryCategory.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQueryCategory.options[0] = listItem;
			    }
			}
   }
   
    function fillBRSubCategoryTechnical()
   {
      document.getElementById('ddlQuerySubCategory').options.length=0;
      id=document.getElementById('ddlQueryCategory').value;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="--Select One--";
	  var ddlQuerySubCategory = document.getElementById('ddlQuerySubCategory');
	  if (document.getElementById("hdSubCategory").value=="") 
            {
             listItem = new Option(names, codes );
             ddlQuerySubCategory.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(document.getElementById("hdSubCategory").value);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			     var item=document.getElementById("ddlQueryCategory").selectedIndex;
			     var text1=document.getElementById("ddlQueryCategory").options[item].text;		
			  //  var orders = dsRoot.getElementsByTagName("CALL_SUB_CATEGORY[@CALL_CATEGORY_NAME='" + text1 + "']");
			   var orders = dsRoot.getElementsByTagName("CSC[@CCN='" + text1 + "']");
			  //<HD_CSC> <CSC CSCI='' CSCN='' CCN='' CSGN='' /> 
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQuerySubCategory.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
				    codes= orders[count].getAttribute("CSCI"); 
			        names=orders[count].getAttribute("CSCN"); 
				    listItem = new Option(names, codes);
				    ddlQuerySubCategory.options[ddlQuerySubCategory.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQuerySubCategory.options[0] = listItem;
			    }
			}
   }
    function fillBRSubCategoryDefaultValues()
{
//debugger;
    var valQuerySubCategory =document.getElementById("ddlQuerySubCategory").value;
    valQuerySubCategory=valQuerySubCategory.trim();
    try
    {
        if ( valQuerySubCategory != "")
        {
            document.getElementById("ddlPriority").value=valQuerySubCategory.split(',')[1];
            document.getElementById("ddlQueryStatus").value=valQuerySubCategory.split(',')[2];
            document.getElementById("ddlTeamAssignedTo").value=valQuerySubCategory.split(',')[3];
            document.getElementById("ddlContactType").value=valQuerySubCategory.split(',')[4];
            document.getElementById("txtTitle").value=valQuerySubCategory.split(',')[5];
            
        }
    }
    catch(err){}
    
}
   function fillBRSubCategoryFunctional()
   {
   {debugger}
      document.getElementById('ddlQuerySubCategory').options.length=0;
      var id=document.getElementById('ddlQueryCategory').value;
      var item=document.getElementById("ddlQueryCategory").selectedIndex;
	  var text1=document.getElementById("ddlQueryCategory").options[item].text ;
	   var text2=text1;
	   text2=text2.toUpperCase();
	/*  if (text2=="PTR")
	  {
	      var intTeamLength= document.getElementById("ddlTeamAssignedTo").options.length;
	      for (var intCount=0 ;intCount<intTeamLength;intCount++)
	      {
    	     var textTeam=document.getElementById("ddlTeamAssignedTo").options[intCount].text ;
    	    textTeam=textTeam.toUpperCase();
	         if (textTeam=="OFFLINE")
	         {
	           document.getElementById("ddlTeamAssignedTo").value=document.getElementById("ddlTeamAssignedTo").options[intCount].value ;
	         }
	      }
	      var intStatusLength= document.getElementById("ddlQueryStatus").options.length;
	      for (var intCount=0 ;intCount<intStatusLength;intCount++)
	      {
    	     var textStatus=document.getElementById("ddlQueryStatus").options[intCount].text ;
    	     textStatus=textStatus.toUpperCase();
	         if (textStatus=="PENDING - OFFLINE")
	         {
	           document.getElementById("ddlQueryStatus").value=document.getElementById("ddlQueryStatus").options[intCount].value ;
	         }
	      }
	  }*/
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="--Select One--";
	  var ddlQuerySubCategory = document.getElementById('ddlQuerySubCategory');
	  if (document.getElementById("hdSubCategory").value=="") 
            {
             listItem = new Option(names, codes );
             ddlQuerySubCategory.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(document.getElementById("hdSubCategory").value);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			    		
			   // var orders = dsRoot.getElementsByTagName("CALL_SUB_CATEGORY[@CALL_CATEGORY_NAME='" + text1 + "']");
			     var orders = dsRoot.getElementsByTagName("CSC[@CCI='" + id + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQuerySubCategory.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
			        codes= orders[count].getAttribute("CSCI")+ "," + orders[count].getAttribute("HEI") + "," + orders[count].getAttribute("HSI")  + "," + orders[count].getAttribute("TI")  + "," + orders[count].getAttribute("CTI")+ "," + orders[count].getAttribute("TE") ; 
			        names=orders[count].getAttribute("CSCN"); 
				    listItem = new Option(names, codes);
				    ddlQuerySubCategory.options[ddlQuerySubCategory.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQuerySubCategory.options[0] = listItem;
			    }
			}
			
   }
  function fillBRCategoryFunctional()
   {
   {debugger;}
      document.getElementById('ddlQueryCategory').options.length=0;
      document.getElementById('ddlQuerySubCategory').options.length=0;
      var  id=document.getElementById('ddlQuerySubGroup').value;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="--Select One--";
	  document.getElementById('ddlQuerySubCategory').options[0]=new Option(names, codes );
	  var ddlQueryCategory = document.getElementById('ddlQueryCategory');
	  if (document.getElementById("hdCategory").value=="") 
            {
             listItem = new Option(names, codes );
             ddlQueryCategory.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(document.getElementById("hdCategory").value);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			     var item=document.getElementById("ddlQuerySubGroup").selectedIndex;
			     var text1=document.getElementById("ddlQuerySubGroup").options[item].text;		
			   // var orders = dsRoot.getElementsByTagName("CALL_CATEGORY[@CALL_SUB_GROUP_NAME='" + text1 + "']");
			     var orders = dsRoot.getElementsByTagName("CC[@CSGI='" + id + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlQueryCategory.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
			        codes= orders[count].getAttribute("CCI"); 
			        names=orders[count].getAttribute("CCN"); 
				    listItem = new Option(names, codes);
				    ddlQueryCategory.options[ddlQueryCategory.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlQueryCategory.options[0] = listItem;
			    }
			}
			
	
			
   }

function FillBRAgencyDetailsTech()
         {
        
           var officeId;
           officeId=  document.getElementById('txtOfficeId').value;
           var officeIdClassName=document.getElementById('txtOfficeId').className;
         
           if (officeId != ""  && officeIdClassName !="textboxgrey")
           {
                document.getElementById('txtAgencyName').value="Searching...";
                BRCallServerTech(officeId,"This is context from client");
           }
           else
           {
           /*
             document.getElementById('lblError').innerText ="";
                document.getElementById('hdEnAOffice').value="";
			    document.getElementById('hdAOffice').value="";
			    document.getElementById('hdCallAgencyName').value="";
			    document.getElementById('hdEnCallAgencyName_LCODE').value="";
			    document.getElementById('txtAgencyName').value="";
			    document.getElementById('txtAddress').value="";
			    document.getElementById('txtCity').value="";
			    document.getElementById('txtCountry').value="";
			    document.getElementById('txtPhone').value="";
			    document.getElementById('txtFax').value="";
			    document.getElementById('txtOnlineStatus').value="";
			    document.getElementById('txtPincode').value="";
			    document.getElementById('txtEmail').value="";
			    */
           }
           return false;
           }
function BRReceiveServerDataTech(args, context)
        {
    
        document.getElementById('lblError').innerText ="";
         var obj = new ActiveXObject("MsXml2.DOMDocument");
            
            
            if (args =="")
            {
                            
                document.getElementById('hdEnAOffice').value="";
			    document.getElementById('hdAOffice').value="";
			    document.getElementById('hdCallAgencyName').value="";
			    document.getElementById('hdEnCallAgencyName_LCODE').value="";
			    document.getElementById('txtAgencyName').value="";
			    document.getElementById('txtAddress').value="";
			    document.getElementById('txtCity').value="";
			    document.getElementById('txtCountry').value="";
			    document.getElementById('txtPhone').value="";
			    document.getElementById('txtFax').value="";
			    document.getElementById('txtOnlineStatus').value="";
			    document.getElementById('txtPincode').value="";
			    document.getElementById('txtEmail').value="";
			    document.getElementById('txtAgencyName').value="";
			          
            }
            else
            {
             var parts = args.split("$");
                 obj.loadXML(parts[0]);
             var dsRoot=obj.documentElement; 
            
			    if (dsRoot !=null)
			    {
			        
			            document.getElementById('hdEnAOffice').value=parts[1];
			            document.getElementById('hdAOffice').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Aoffice");
			            document.getElementById('hdCallAgencyName').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("LOCATION_CODE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NAME") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS") + "|" + "" + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CITY") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COUNTRY")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PHONE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("OfficeID")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("FAX") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ONLINE_STATUS") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Aoffice")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PINCODE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Email")
			            document.getElementById('hdEnCallAgencyName_LCODE').value=parts[2];
			            document.getElementById('txtAgencyName').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NAME")
			            document.getElementById('txtAddress').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS") + '\n ' + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS1")
			            document.getElementById('txtCity').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CITY")
			            document.getElementById('txtCountry').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COUNTRY")
			            document.getElementById('txtPhone').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PHONE")
			            document.getElementById('txtFax').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("FAX")
			            document.getElementById('txtOnlineStatus').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ONLINE_STATUS")
			            document.getElementById('txtPincode').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PINCODE")
			            document.getElementById('txtEmail').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Email")
			           
			           
			       
			    }
            
            }
            
           
            
        }

 function FillBRAgencyDetailsFunctional()
         {
        
           var officeId;
           officeId=  document.getElementById('txtOfficeId').value;
           var officeIdClassName=document.getElementById('txtOfficeId').className;
         
           if (officeId != ""  && officeIdClassName !="textboxgrey")
           {
                document.getElementById('txtAgencyName').value="Searching...";
                BRCallServerFunctional(officeId,"This is context from client");
           }
           else
           {
           /*
             document.getElementById('lblError').innerText ="";
                document.getElementById('hdEnAOffice').value="";
			    document.getElementById('hdAOffice').value="";
			    document.getElementById('hdCallAgencyName').value="";
			    document.getElementById('hdEnCallAgencyName_LCODE').value="";
			    document.getElementById('txtAgencyName').value="";
			    document.getElementById('txtAddress').value="";
			    document.getElementById('txtCity').value="";
			    document.getElementById('txtCountry').value="";
			    document.getElementById('txtPhone').value="";
			    document.getElementById('txtFax').value="";
			    document.getElementById('txtOnlineStatus').value="";
			    document.getElementById('txtPincode').value="";
			    document.getElementById('txtEmail').value="";
			    */
           }
           return false;
           }
function BRReceiveServerDataFunctional(args, context)
        {
         document.getElementById('lblError').innerText ="";
         var obj = new ActiveXObject("MsXml2.DOMDocument");
            if (args =="")
            {
                            
                document.getElementById('hdEnAOffice').value="";
			    document.getElementById('hdAOffice').value="";
			    document.getElementById('hdCallAgencyName').value="";
			    document.getElementById('hdEnCallAgencyName_LCODE').value="";
			    document.getElementById('txtAgencyName').value="";
			    document.getElementById('txtAddress').value="";
			    document.getElementById('txtCity').value="";
			    document.getElementById('txtCountry').value="";
			    document.getElementById('txtPhone').value="";
			    document.getElementById('txtFax').value="";
			    document.getElementById('txtOnlineStatus').value="";
			    document.getElementById('txtPincode').value="";
			    document.getElementById('txtEmail').value="";
			    document.getElementById('txtAgencyName').value="";
			          
            }
            else
            {
             var parts = args.split("$");
                 obj.loadXML(parts[0]);
             var dsRoot=obj.documentElement; 
            
			    if (dsRoot !=null)
			    {			        
			            document.getElementById('hdEnAOffice').value=parts[1];
			            document.getElementById('hdAOffice').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Aoffice");
			            document.getElementById('hdCallAgencyName').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("LOCATION_CODE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NAME") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS") + "|" + "" + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CITY") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COUNTRY")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PHONE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("OfficeID")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("FAX") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ONLINE_STATUS") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Aoffice")+ "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PINCODE") + "|" + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Email")
			            document.getElementById('hdEnCallAgencyName_LCODE').value=parts[2];
			            document.getElementById('txtAgencyName').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NAME")
			            document.getElementById('txtAddress').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS") + '\n ' + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS1")
			            document.getElementById('txtCity').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CITY")
			            document.getElementById('txtCountry').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COUNTRY")
			            document.getElementById('txtPhone').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PHONE")
			            document.getElementById('txtFax').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("FAX")
			            document.getElementById('txtOnlineStatus').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ONLINE_STATUS")
			            document.getElementById('txtPincode').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PINCODE")
			            document.getElementById('txtEmail').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("Email")
			    }
            
            }
        }
        
        //End of New Added Code   
        
        /**********************************************javascript for birdresHelpdesk**********************************************/
function PopupPageBRCallLog(id)
             {
             
            
             var type;
             if (id=="1")
             {
                    
                 var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                if (strEmployeePageName!="")
                {
                   type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
                  //  type = "../Setup/MSSR_Employee.aspx?Popup=T";
   	                window.open(type,"aaCallLogEmployee","height=600,width=900,top=30,left=20,scrollbars=1,status=1");         
   	             }   
              }
             if (id=="2")
             {
                    type = "../TravelAgency/TASR_Agency.aspx?Popup=T&HelpDeskType=BR" ;
   	                window.open(type,"aaCallLogAgency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
             }
        
             if (id=="3")
             {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T";
   	                window.open(type,"aaCallLogAgencyStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
              }
        
           
     }
     
     
     
     /*Search Call Log*/

    function ValidateFormBRCallLog()
    {
    
    var strAgencyName=document.getElementById("txtAgencyName").value.trim();
    var strOfficeID=document.getElementById("txtOfficeID").value.trim();
    var strCUSTOMER_CATEGORY_ID=document.getElementById("ddlCustomerCategory").selectedIndex;
    var strLoggedBy=document.getElementById("txtLoggedBy").value.trim();
    var strCALLER_NAME=document.getElementById("txtCallerName").value.trim();
    var strHD_QUERY_GROUP_ID=document.getElementById("ddlQueryGroup").selectedIndex;
    var strCALL_SUB_GROUP_ID=document.getElementById("ddlQuerySubGroup").selectedIndex;
    var strCALL_CATEGORY_ID=document.getElementById("ddlQueryCategory").selectedIndex;
    var strCALL_SUB_CATEGORY_ID=document.getElementById("ddlQuerySubCategory").selectedIndex;
    var strHD_STATUS_ID=document.getElementById("ddlQueryStatus").selectedIndex;
    var strHD_SEVERITY_ID=document.getElementById("ddlQueryPriority").selectedIndex;
    var strCOORDINATOR1=document.getElementById("ddlCoordinator1").selectedIndex;
    var strCOORDINATOR2=document.getElementById("ddlCoordinator2").selectedIndex;
    var strAssignedDatetime=document.getElementById("txtDateAssigned").value.trim();
    var strDISPOSITION_ID=document.getElementById("ddlDisposition").selectedIndex;
    var strQueryOpenedDateFrom=document.getElementById("txtQueryOpenedDateFrom").value.trim();
    var strQueryOpenedDateTo=document.getElementById("txtQueryOpenedDateTo").value.trim();
    var strCloseDateFrom1=document.getElementById("txtCloseDateFrom1").value.trim();
    var strCloseDateTo1=document.getElementById("txtCloseDateTo1").value.trim();
    var strAOffice=document.getElementById("ddlAOffice").selectedIndex;
    var strAgencyAOffice=document.getElementById("ddlAgencyAOffice").selectedIndex;
    var strLTRNo=document.getElementById("txtLTRNo").value.trim();
    var strPTRNo=document.getElementById("txtPTRNo").value.trim();
    var strDisplayLastCall=document.getElementById("chkDisplayLastCall").checked;
    var strWorkOrderNo=document.getElementById("txtWorkOrderNo").value.trim();
    var strCity=document.getElementById("ddlCity").selectedIndex;
    var strState=document.getElementById("ddlState").selectedIndex;
    var strISIATA=document.getElementById("chkISIATA").checked;
    var strIATA=document.getElementById("txtIATA").value.trim();
    var strISPending=document.getElementById("chkISPending").checked;
    var strRegion=document.getElementById("ddlRegion").selectedIndex;
    var strAssignedTo=document.getElementById("txtAssignedTo").value.trim();
    var strContactType=document.getElementById("ddlContactType").selectedIndex;
    var strTitle=document.getElementById("txtTitle").value.trim();
   var strError=false;
   
   if (strDisplayLastCall==false && strISPending ==false)
   {
           if (strAgencyName=="" && strOfficeID=="" && strCUSTOMER_CATEGORY_ID=="0" && strLoggedBy=="" && strCALLER_NAME==""&& strHD_QUERY_GROUP_ID=="0" && strCALL_SUB_GROUP_ID=="0" && strCALL_CATEGORY_ID=="0")
           {
            if ( strCALL_SUB_CATEGORY_ID=="0" && strHD_STATUS_ID=="0" && strHD_SEVERITY_ID=="0" && strCOORDINATOR1=="0" &&  strCOORDINATOR2=="0" && strAssignedDatetime=="" && strDISPOSITION_ID=="0" && strQueryOpenedDateFrom=="" )
                { 
                    if (strQueryOpenedDateTo=="" && strCloseDateFrom1=="" && strCloseDateTo1=="" && strAOffice=="0" && strAgencyAOffice=="0"  && strLTRNo=="" && strPTRNo=="" && strWorkOrderNo=="" && strCity=="0" && strState=="0")
                     {
                        if( strIATA=="" &&  strRegion=="0" &&  strAssignedTo=="" && strContactType=="0" && strTitle=="")
                        {
                            strError=true;
                        }
                    }
                 }   
           }      
    }
    
    if (strError==true)
    {
                    document.getElementById("lblError").innerText="Opened Date From is mandatory";
                    document.getElementById("txtQueryOpenedDateFrom").focus();
                    return false;
    }
        var dtDateAssigned=document.getElementById("txtDateAssigned").value;
        
        if (dtDateAssigned.trim()!="")
        {
            if (isDate(dtDateAssigned.trim(),"dd/MM/yyyy")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtDateAssigned").focus();
                    return false;

            }
        }
        
        var dtOpenFrom=document.getElementById("txtQueryOpenedDateFrom").value;
        var dtOpenTo=document.getElementById("txtQueryOpenedDateTo").value;
    
    
     if (dtOpenFrom.trim()!="")
        {
            if (isDate(dtOpenFrom.trim(),"dd/MM/yyyy HH:mm")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtQueryOpenedDateFrom").focus();
                    return false;

            }
        }
        
         if (dtOpenTo.trim()!="")
        {
            if (isDate(dtOpenTo.trim(),"dd/MM/yyyy HH:mm")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtQueryOpenedDateTo").focus();
                    return false;
            }
      }
    
      document.getElementById('lblError').innerText=''            
   if (compareDates(document.getElementById('txtQueryOpenedDateFrom').value,"dd/MM/yyyy HH:mm",document.getElementById('txtQueryOpenedDateTo').value,"dd/MM/yyyy HH:mm")==1)
       {
            document.getElementById('lblError').innerText ='Open date to should be greater than or equal to open date from.';
            return false;
       }
       var dtFrom=document.getElementById("txtCloseDateFrom1").value;
       var dtTo=document.getElementById("txtCloseDateTo1").value;
        dtFrom=dtFrom.trim();
        dtTo=dtTo.trim();
        
         if (dtFrom.trim()!="")
        {
            if (isDate(dtFrom.trim(),"dd/MM/yyyy HH:mm")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtCloseDateFrom1").focus();
                    return false;

            }
        }
        
         if (dtTo.trim()!="")
        {
            if (isDate(dtTo.trim(),"dd/MM/yyyy HH:mm")==false)
            {
                    document.getElementById("lblError").innerText="Invalid date format";
                    document.getElementById("txtCloseDateTo1").focus();
                    return false;
            }
        }
       if (compareDates(dtFrom,"dd/MM/yyyy HH:mm",dtTo,"dd/MM/yyyy HH:mm")==1)
       {
            document.getElementById('lblError').innerText ='Close date to should be greater than or equal to close date from.';
            return false;
       }
                     
       return true; 
        
    }
    
    function ReturnDataBRCallLog()
       {
     

           for(i=0;i<document.forms[0].elements.length;i++) 
            {
            var elm = document.forms[0].elements[i]; 
                    if(elm.type == 'checkbox') 
                    {
                         var chkId=elm.id;
                         if (elm.checked == true && chkId.split("_")[2]=="chkSelect")
                         {                           
                            var gvname=chkId.split("_")[0];
                            var ctrlidname=chkId.split("_")[1];
                             if (document.getElementById("hdData").value == "")
                             {
                                document.getElementById("hdData").value =document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                             }
                             else
                             {
                                document.getElementById("hdData").value = document.getElementById("hdData").value + "," + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                             }                         
                         }                      
                    }
            }
        
            var data= document.getElementById("hdData").value;
            if(data=="")
            {
                document.getElementById("lblError").innerText="Checked atleast one checkbox";
                return false;            
            }
           else
           {
                 if (window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage']!=null)
                 {
                    if (window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage'].value=="")
                    {
                        window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage'].value=data;
                    }
                    else
                    {
                        window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage'].value=window.opener.document.forms['form1']['hdCHD_RE_IDListPopupPage'].value + "," + data;
                    }
                    window.opener.document.forms(0).submit();
                    window.close();
                    return false;
                 }
           }
       } 
    
    
       function SelectAllBRCallLog() 
    {
       CheckAllDataGridCheckBoxesBRCallLog(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxesBRCallLog(value) 
    {
   
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
                var chkId=elm.id;
                if (chkId.split("_")[2]=="chkSelect")
                {
                    elm.checked = value
                }
            }
        }
    }
    
    
    
    function PopupPageBRFunctional(id)
         {
  
         var type;
         //var stLCode=document.getElementById("hdCallAgencyName").value; commented on 2 aug 07
         var strLCode=document.getElementById("hdEnCallAgencyName_LCODE").value; //Added on 2 aug 08 
         var itemQueryCategory=""
         var strPTRNo=document.getElementById("txtPTRNo").value;
	     var strWorkOrderNo=document.getElementById("txtWorkOrderNo").value;
	     var strBDRLetterID=document.getElementById("txtBDRLetterID").value;
         try
         {
            var itemQueryCategory=document.getElementById("ddlQueryCategory").selectedIndex;
	        var textQueryCategory=document.getElementById("ddlQueryCategory").options[itemQueryCategory].text ;
	        textQueryCategory=textQueryCategory.toUpperCase();
	    }
	    catch(err){}
	    //code for this added above on 2 Aug 08
       /*  if (stLCode != "")
         {
              //strLCode=stLCode.split("|")[0]; 
         }*/
          if (id=="1")
         {
             type = "../TravelAgency/TASR_Agency.aspx?Popup=T&HelpDeskType=BR" ;
   	         window.open(type,"aa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }
         if (id=="2")
         {
                var strAgencyName=document.getElementById("txtAgencyName").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                if (strAgencyName!="")
                {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName;
   	                window.open(type,"aaHelpDesk","height=600,width=900,top=30,left=20,scrollbars=1,status=1");                  
   	            }
   	            else
   	            {
   	                document.getElementById("lblError").innerHTML='Agency name is mandatory';
                    return false;
   	            }
          }
        
    
         if (id=="3")
         {
                document.getElementById("lblError").innerHTML='';
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
             
                //if (strHD_RE_ID != "" && strLCode != "" && textQueryCategory=="PTR" )
                if (strHD_RE_ID != "" && strLCode != "" )
                {
                    //if(strWorkOrderNo=="" && strBDRLetterID=="")
                   // {
                    
                        if(document.getElementById("hdPTRNo").value == "")
                        {
                            type = "../BirdresHelpDesk/HDUP_Ptr.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;
   	                        window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                    }
   	                    else
   	                    {
   	                        var strAction="U|" + document.getElementById("hdEnPTRNo").value ;
   	                        type = "../BirdresHelpDesk/HDUP_Ptr.aspx?Popup=T&Action=" + strAction + "&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;
   	                        window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
   	                    }
   	                //}
   	            } 
   	            else
   	            {
   	               // document.getElementById("lblError").innerHTML='Query Category must be PTR';
                   // return false;
   	            }
   	             	                    
          }
          if (id=="4")
         {
                document.getElementById("lblError").innerHTML='';
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                if (strHD_RE_ID != "" && strLCode != "" && textQueryCategory=="BDR LETTER")
                {
                   // if( strPTRNo=="" &&  strWorkOrderNo=="")
                    //{
                        if(document.getElementById("hdBDRLetterID").value == "")
                        {
                            if (document.getElementById("ddlQueryCategory").value != "")
                            {
                                 var item=document.getElementById("ddlQuerySubCategory").selectedIndex;
                                 if (item==0 )                 
                                 {
                                     text1="";
                                 }
                                 else
                                 {
			                     var text1=document.getElementById("ddlQuerySubCategory").options[item].text;	
                                 }
                                 type = "../BirdresHelpDesk/HDUP_BDR.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode +"&requestType=" + text1;//'document.getElementById("ddlQueryCategory").value;
   	                            window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                        }
   	                        else
   	                        {
   	                            document.getElementById("lblError").innerText="First Select query Category";
   	                        }
   	                    }
   	                    else
   	                     {
                             type = "../BirdresHelpDesk/HDUP_BDR.aspx?Popup=T&Action=U&ReqID="+strHD_RE_ID + "&LCode="+strLCode + "&HD_RE_BDR_ID=" + document.getElementById("hdEnBDRLetterID").value;
   	                         window.open(type,"HelpDesks","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	                    }
       	            //}
   	                
   	            }
   	             else
   	            {
   	                document.getElementById("lblError").innerHTML='Query Category must be BDR LETTER';
                    return false;
   	            }
          }
        if (id=="5")
         {      document.getElementById("lblError").innerHTML='';
                 var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
             //   if (strHD_RE_ID != "" && strLCode != "" && textQueryCategory=="WORK ORDER")
                if (strHD_RE_ID != "" && strLCode != "" )
                {
                   // if(strPTRNo=="" && strBDRLetterID=="")
                    //{
                        if(document.getElementById("hdEnWorkOrderNo").value == "")
                        {
                        type = "../BirdresHelpDesk/HDUP_WorkOrder.aspx?Popup=T&Action=I&ReqID="+strHD_RE_ID + "&LCode="+strLCode ;
   	                    window.open(type,"aaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   	                    }
           	            
   	                    else
   	                    {
                        type = "../BirdresHelpDesk/HDUP_WorkOrder.aspx?Popup=T&Action=U&ReqID="+strHD_RE_ID + "&LCode="+strLCode + "&OrderID=" + document.getElementById("hdEnWorkOrderNo").value ;
   	                    window.open(type,"HelpDesks1","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	                    }
   	                 //}
   	            }
   	            else
   	            {
   	                //document.getElementById("lblError").innerHTML='Query Category must be WORK ORDER';
                   // return false;
   	            }
          }
          if (id=="6")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="HDUP_Transaction.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"HelpDesksTransaction","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	            return false;
          }
           if (id=="7")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="../BirdresHelpDeskPopup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"HelpDesksCallLogHistory","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	            return false;
          }
          if (id=="8")
         {
                 var strStatus=document.getElementById("hdEnFunctional").value;
                 var LCode=document.getElementById("hdEnPageLCode").value;
                var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
               var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
               var AOFFICE=document.getElementById("hdEnAOffice").value;              
      
              type="HDUP_helpDeskFeedBack.aspx?Popup=T&Action=U&strStatus=" + strStatus + "&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;   
              window.open(type,"HelpDesksCallFeedBack","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	          return false;
          }
     }
     
   
    
    //   Code For Birdres
    
    function BRPopupPage(id)
         {
      
         var type;
          if (id=="1")
         {
             type = "../TravelAgency/TASR_Agency.aspx?Popup=T&HelpDeskType=BR" ;
   	         window.open(type,"Taa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }
         if (id=="2")
         {
                var strAgencyName=document.getElementById("txtAgencyName").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                if (strAgencyName!="")
                {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName;
                    window.open(type,"TaaHelpDesk","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
                }                  
               else
   	            {
   	                document.getElementById("lblError").innerHTML='Agency name is mandatory';
                    return false;
   	            }
          }
        
    
         if (id=="3")
         {
                type = "../BirdresHelpDesk/HDSR_Ptr.aspx?Popup=T";
   	            window.open(type,"TaaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
          }
          if (id=="4")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="../BirdresHelpDeskPopup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"THelpDesksTransaction","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	            return false;          
          }
        if (id=="5")
         {
                 var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="TCUP_Transaction.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"THelpDesksTransaction","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	            return false;         
          }
         if (id=="6")
         {
              var strStatus=document.getElementById('hdEnTechnical').value;  
              var LCode=document.getElementById("hdEnPageLCode").value;
              var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
              var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
              var AOFFICE=document.getElementById("hdEnAOffice").value;
              type="HDUP_helpDeskFeedBack.aspx?Popup=T&Action=U&strStatus="+strStatus+"&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;  
              window.open(type,"THelpDesksFeedBack","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	            return false;   
                
          
         }
     }


    
    
    function ManageTBRCallLogPage()
{

 var ClosedStatus="";
 var strClosedStatus="";
 document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
if(textbox('txtAgencyName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Agency Name is Mandatory";	        	  
		      return false;} 
		      
		      if(textbox('txtCallerName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Caller Name is Mandatory";	 
	document.getElementById("txtCallerName").focus();      	  
		      return false;} 
		      
		      if(ddlvalidate('ddlQuerySubGroup')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Sub Group is Mandatory";
		    document.getElementById('ddlQuerySubGroup').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlQueryCategory')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Category is Mandatory";
		    document.getElementById('ddlQueryCategory').focus();
		    return false;}	
		      
		      var cnddlQuerySubCategory=document.getElementById('ddlQuerySubCategory').options.length;
		    if(cnddlQuerySubCategory>1)
		    {       document.getElementById("hdSubCategoryMandatory").value="1";
		             if(ddlvalidate('ddlQuerySubCategory')==true )
                    { }
	                 else
	                 { 
	                     document.getElementById("lblError").innerText="Query Sub Category is Mandatory";
	                     document.getElementById('ddlQuerySubCategory').focus();
		                 return false;
		             }	
		     } 
		     else
		     {
		        document.getElementById("hdSubCategoryMandatory").value="0";
		     }
		      
		    
		     if(document.getElementById("txtCallDuration").value.trim() != "" )
		     {
		        if (_isInteger(document.getElementById("txtCallDuration").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 24";
		      document.getElementById("txtCallDuration").focus();
		      return false;
		      }
		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value);
		      
		        if((strCallDur > 24)  || (strCallDur < 0))
		        {
		        document.getElementById("lblError").innerText = "Hours must be in between 0 to 24";
		        document.getElementById("txtCallDuration").focus();
		        return false;
		        }
		     } 
		    
		      if(document.getElementById("txtCallDuration1").value != "")
		     {
		     if (_isInteger(document.getElementById("txtCallDuration1").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 60";
		      document.getElementById("txtCallDuration1").focus();
		      return false;
		      }
		      var strCallDur1=parseInt(document.getElementById("txtCallDuration1").value);
		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value);
		        if((strCallDur1 > 60)  || (strCallDur1 < 0))
		        {
		        document.getElementById("lblError").innerText = "Minutes must be in between 0 to 60";
		        document.getElementById("txtCallDuration1").focus();
		        return false;
		        }
		        if(strCallDur==24)
		        {
		             if(strCallDur1>0)
		             {
		             document.getElementById("lblError").innerText = "Minutes must be 0";
		             document.getElementById("txtCallDuration1").focus();
		             return false;
		             }
		        }
		       
		     } 
		     
		      
		      if(ddlvalidate('ddlPriority')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Priority is Mandatory";
		    document.getElementById('ddlPriority').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlQueryStatus')==true )
    {   
     ClosedStatus=document.getElementById('ddlQueryStatus').value; 
        strClosedStatus= ClosedStatus.split("|")[1];
        }
	else{ document.getElementById("lblError").innerText="Query Status is Mandatory";
		    document.getElementById('ddlQueryStatus').focus();
		    return false;}	
		      
		    	    
		     if(ddlvalidate('ddlTeamAssignedTo')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Assigned to  is Mandatory";
		    document.getElementById('ddlTeamAssignedTo').focus();
		    return false;}	
		    /*
		    if(ddlvalidate('ddlCoordinator1')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Coordinator1 is Mandatory";
		    document.getElementById('ddlCoordinator1').focus();
		    return false;}	*/
		    
		    if(ddlvalidate('ddlContactType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Contact Type is Mandatory";
		    document.getElementById('ddlContactType').focus();
		    return false;}	
		    
		    
		    if(textbox('txtDescription')==true )
    {        }
	else{ 
	if (document.getElementById("hdPageHD_RE_ID").value == "")
	    {
	        document.getElementById("lblError").innerText="Description is Mandatory";	 
	
	        document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("txtDescription").focus();   
            try
            {
                
                if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtab"
                }
            }
            catch(err){} 	  
		      return false;} 
		 
		}
		     
		      if(strClosedStatus == "1")
		      {
		      
		      if(textbox('txtSolution')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Solution is Mandatory";	 
	 
	        document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="block";
             document.getElementById("txtSolution").focus();  
             try
            {
                
                if (document.getElementById('theTabStrip_ctl02_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                     document.getElementById("theTabStrip_ctl02_Button1").className="headingtab"
                }
            }
            catch(err){}
              	  
		      return false;} 
		      }
		    
}



function ManageBRCallLogPage()
{

 var ClosedStatus="";
 var strClosedStatus="";
            document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";  
if(textbox('txtAgencyName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Agency Name is Mandatory";	        	  
		      return false;} 
		      
		      if(textbox('txtCallerName')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Caller Name is Mandatory";	 
	document.getElementById("txtCallerName").focus();      	  
		      return false;} 
		      
		      if(ddlvalidate('ddlQuerySubGroup')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Sub Group is Mandatory";
		    document.getElementById('ddlQuerySubGroup').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlQueryCategory')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Query Category is Mandatory";
		    document.getElementById('ddlQueryCategory').focus();
		    return false;}	
		      
		    var cnddlQuerySubCategory=document.getElementById('ddlQuerySubCategory').options.length;
		    if(cnddlQuerySubCategory>1)
		    {       document.getElementById("hdSubCategoryMandatory").value="1";
		             if(ddlvalidate('ddlQuerySubCategory')==true )
                    { }
	                 else
	                 { 
	                     document.getElementById("lblError").innerText="Query Sub Category is Mandatory";
	                     document.getElementById('ddlQuerySubCategory').focus();
		                 return false;
		             }	
		     } 
		     else
		     {
		        document.getElementById("hdSubCategoryMandatory").value="0";
		     }
		    
		    if(document.getElementById("txtCallDuration").value.trim() != "" )
		     {
		        if (_isInteger(document.getElementById("txtCallDuration").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 24";
		      document.getElementById("txtCallDuration").focus();
		      return false;
		      }
		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value,10);
		      
		        if((strCallDur > 24)  || (strCallDur < 0))
		        {
		        document.getElementById("lblError").innerText = "Hours must be in between 0 to 24";
		        document.getElementById("txtCallDuration").focus();
		        return false;
		        }
		     } 
		    
		      if(document.getElementById("txtCallDuration1").value != "")
		     {
		     if (_isInteger(document.getElementById("txtCallDuration1").value)==false)
		      {
		      document.getElementById("lblError").innerText = "Only Numbers allowed between 0 to 60";
		      document.getElementById("txtCallDuration1").focus();
		      return false;
		      }
		      var strCallDur1=parseInt(document.getElementById("txtCallDuration1").value,10);
		      var strCallDur=parseInt(document.getElementById("txtCallDuration").value,10);
		        if((strCallDur1 > 60)  || (strCallDur1 < 0))
		        {
		        document.getElementById("lblError").innerText = "Minutes must be in between 0 to 60";
		        document.getElementById("txtCallDuration1").focus();
		        return false;
		        }
		       if(strCallDur==24)
		        {
		             if(strCallDur1>0)
		             {
		             document.getElementById("lblError").innerText = "Minutes must be 0";
		             document.getElementById("txtCallDuration1").focus();
		             return false;
		             }
		        }
		     }   
		      
		      if(ddlvalidate('ddlPriority')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Priority is Mandatory";
		    document.getElementById('ddlPriority').focus();
		    return false;}	
		    
		     if(ddlvalidate('ddlQueryStatus')==true )
    {   
        ClosedStatus=document.getElementById('ddlQueryStatus').value; 
        strClosedStatus= ClosedStatus.split("|")[1];
        document.getElementById("hdSol").value=strClosedStatus;
    }
	else{ document.getElementById("lblError").innerText="Query Status is Mandatory";
		    document.getElementById('ddlQueryStatus').focus();
		    return false;}	
		      
		    	    
		     if(ddlvalidate('ddlTeamAssignedTo')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Assigned To is Mandatory";
		    document.getElementById('ddlTeamAssignedTo').focus();
		    return false;}	
		    
		    if(ddlvalidate('ddlCoordinator1')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Coordinator1 is Mandatory";
		    document.getElementById('ddlCoordinator1').focus();
		    return false;}	
		    
		    if(ddlvalidate('ddlContactType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Contact Type is Mandatory";
		    document.getElementById('ddlContactType').focus();
		    return false;}	
		    
		    if(textbox('txtDescription')==true )
    {        }
	else{ 
	
	    if (document.getElementById("hdPageHD_RE_ID").value == "" || document.getElementById("hdSaveRights").value == "1")
	    {
	        document.getElementById("lblError").innerText="Description is Mandatory";	 
	
	        document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="none";  
            document.getElementById("txtDescription").focus(); 
            try
            {
                
                if (document.getElementById('theTabStrip_ctl01_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtab"
                }
            }
            catch(err){}
               	  
		      return false;} 
		 }
		
		     
		      if(strClosedStatus == "1")
		      {
		      
		      if(textbox('txtSolution')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Solution is Mandatory";	 
	 
	        document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="block";
            document.getElementById("pnlFollowUp").style.display="none";  
            document.getElementById("txtSolution").focus();   	 
            try
            {
                if (document.getElementById('theTabStrip_ctl02_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl02_Button1").className="headingtab"
                }
            }
            catch(err){} 
		      return false;} 
		      }
		    
		    if (document.getElementById("ddlMode").selectedIndex > 0 && document.getElementById("txtFollowDesc").value != "" && document.getElementById("txtNxtFollowupDate").value != "" )
		    {
		    
            }
            else
            {
            if (document.getElementById("ddlMode").selectedIndex == 0 && document.getElementById("txtFollowDesc").value == "" && document.getElementById("txtNxtFollowupDate").value == "" )
            {
            }
            else
            {
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("pnlSol").style.display="none";
            document.getElementById("pnlFollowUp").style.display="block"; 
             
             if(ddlvalidate('ddlMode')==true )
            {       }
	        else{ document.getElementById("lblError").innerText="Mode is Mandatory";
		    document.getElementById('ddlMode').focus();
		   
		    try
            {
                if (document.getElementById('theTabStrip_ctl03_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl03_Button1").className="headingtab"
                }
            }
            catch(err){} 
             return false;
		    }	
             
             if(textbox('txtFollowDesc')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Description is Mandatory";	 
	document.getElementById("txtFollowDesc").focus();      	  
		      try
            {
                if (document.getElementById('theTabStrip_ctl03_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl03_Button1").className="headingtab"
                }
            }
            catch(err){} 
             return false;
             } 
		      
		      if(textbox('txtNxtFollowupDate')==true )
    {        }
	else{ document.getElementById("lblError").innerText="Next Follow up Date is Mandatory";	 
	document.getElementById("txtNxtFollowupDate").focus();      	  
		       try
            {
                if (document.getElementById('theTabStrip_ctl03_Button1').className != "displayNone")
                {
                    document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                    document.getElementById("theTabStrip_ctl03_Button1").className="headingtab"
                }
            }
            catch(err){} 
             return false;
             } 
		              
           
            }
            }
            
            try
            {
                 var itemTeam=document.getElementById("ddlTeamAssignedTo").selectedIndex;
                 var textTeam=document.getElementById("ddlTeamAssignedTo").options[itemTeam].text ;
    	        textTeam=textTeam.toUpperCase();
    	         var itemStatus=document.getElementById("ddlQueryStatus").selectedIndex;
    	        var textStatus=document.getElementById("ddlQueryStatus").options[itemStatus].text ;
    	         textStatus=textStatus.toUpperCase();
	             if (textTeam=="ONLINE" && (textStatus=="SOLVED - OFFLINE" || textStatus=="PENDING - OFFLINE"))
	             {
	              document.getElementById("lblError").innerText="Call Could not be saved because AssignedTo is Online and Query Status is " + textStatus;	 
	              document.getElementById("ddlTeamAssignedTo").focus();
	                try
                 {
                    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                    {
                        document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                        document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                    }
                }
            catch(err){} 
             return false;      	  
		        
	             }
	        }
	        catch(err){}
	     
	     var strPTRNo=document.getElementById("txtPTRNo").value;
	     var strWorkOrderNo=document.getElementById("txtWorkOrderNo").value;
	     var strBDRLetterID=document.getElementById("txtBDRLetterID").value;
	    /* if ( strPTRNo != "" && (strWorkOrderNo != "" || strBDRLetterID != "")) 
	     {
	              document.getElementById("lblError").innerText= "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
	               try
                 {
                    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                    {
                        document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                        document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                    }
                }
            catch(err){} 
	              return false;	      
	     }
	     
	     if ( strWorkOrderNo != "" && (strPTRNo != "" || strBDRLetterID != "")) 
	     {
	              document.getElementById("lblError").innerText= "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
	               try
                 {
                    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                    {
                        document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                        document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                    }
                }
            catch(err){} 
	              return false;	      
	     }
	     
	     if ( strBDRLetterID != "" && (strWorkOrderNo != "" || strPTRNo != "")) 
	     {
	              document.getElementById("lblError").innerText= "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
	               try
                 {
                    if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                    {
                        document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                        document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                        document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                    }
                }
            catch(err){} 
	              return false;	      
	     }
              */
                
		  var itemQueryCategory=""
         try
         {
            var itemQueryCategory=document.getElementById("ddlQueryCategory").selectedIndex;
	        var textQueryCategory=document.getElementById("ddlQueryCategory").options[itemQueryCategory].text ;
	        textQueryCategory=textQueryCategory.toUpperCase();
	        //For PTR CASE
	        /*if (strPTRNo!="")
	        {
	            if (textQueryCategory != "PTR" )
	            {
    	                document.getElementById("lblError").innerText= "Invalid Query Category";
    	                document.getElementById("ddlQueryCategory").focus();
	                   try
                     {
                        if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                        {
                            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                        }
                    }
                     catch(err){} 
	                  return false;	 
	            }
	        }
	         //For WorkOrder CASE
	        if (strWorkOrderNo!="")
	        {
	            if (textQueryCategory != "WORK ORDER" )
	            {
    	                document.getElementById("lblError").innerText= "Invalid Query Category";
    	                document.getElementById("ddlQueryCategory").focus();
	                   try
                     {
                        if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                        {
                            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                        }
                    }
                     catch(err){} 
	                  return false;	 
	            }
	        }
	        */
	         //For BDRLetter CASE   
	        if (strBDRLetterID!="")
	        {
	            if (textQueryCategory != "BDR LETTER" )
	            {
    	                document.getElementById("lblError").innerText= "Invalid Query Category";
    	                document.getElementById("ddlQueryCategory").focus();
	                   try
                     {
                        if (document.getElementById('theTabStrip_ctl00_Button1').className != "displayNone")
                        {
                            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab"
                            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl02_Button1").className="headingtabactive"
                            document.getElementById("theTabStrip_ctl03_Button1").className="headingtabactive"
                        }
                    }
                     catch(err){} 
	                  return false;	 
	            }
	        }
	    }
	    catch(err){}
}




 


function PopupPageBR(id)
         {
      
         var type;
          if (id=="1")
         {
             type = "../TravelAgency/TASR_Agency.aspx?Popup=T&HelpDeskType=BR";
   	         window.open(type,"Taa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
   	       
         }
         if (id=="2")
         {
                var strAgencyName=document.getElementById("txtAgencyName").value;
                 strAgencyName=strAgencyName.replace("&","%26")
                if (strAgencyName!="")
                {
                    type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName;
                    window.open(type,"TaaHelpDesk","height=600,width=900,top=30,left=20,scrollbars=1,status=1");
                }                  
               else
   	            {
   	                document.getElementById("lblError").innerHTML='Agency name is mandatory';
                    return false;
   	            }
          }
        
    
         if (id=="3")
         {
                type = "../HelpDesk/HDSR_Ptr.aspx?Popup=T";
   	            window.open(type,"TaaHelpDesks","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
          }
          if (id=="4")
         {
                var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="../Popup/PUSR_CallLogHistory.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"THelpDesksTransaction","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	            return false;          
          }
        if (id=="5")
         {
                 var strHD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
                type ="TCUP_Transaction.aspx?HD_RE_ID="+strHD_RE_ID ;
   	            window.open(type,"THelpDesksTransaction","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	            return false;         
          }
         if (id=="6")
         {
              var strStatus=document.getElementById('hdEnTechnical').value;  
              var LCode=document.getElementById("hdEnPageLCode").value;
              var strFeedBackId=document.getElementById("hdEnFeedBackId").value;
              var HD_RE_ID=document.getElementById("hdEnPageHD_RE_ID").value;
              var AOFFICE=document.getElementById("hdEnAOffice").value;
              type="HDUP_helpDeskFeedBack.aspx?Popup=T&Action=U&strStatus="+strStatus+"&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId + "&AOFFICE="+ AOFFICE ;  
              window.open(type,"THelpDesksFeedBack","height=600,width=920,top=30,left=20,scrollbars=1,status=1");           
   	            return false;   
                
          
         }
     }

    
    
/* End of Search Call */