<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_ModifyMiscInstall.aspx.vb" Inherits="TravelAgency_TAUP_ModifyMiscInstall" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Add Misc. Hardware </title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
      <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
        <script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<script type="text/javascript" language="javascript">

var st;


        function showhide()
        {
     
    var value=document.getElementById("hdShowHide").value;
            if(value=='111')
            {
            if(document.getElementById("drpModifyEqup") !=null)
            {
            document.getElementById("drpModifyEqup").className="displayNone";
            }
            if(document.getElementById("txtModEquipNo")!=null)
            {
            document.getElementById("txtModEquipNo").className="textbox";
             }
             
            document.getElementById("hdShowHide").value=""
            }   
            else
            {
            if(document.getElementById("drpModifyEqup")!=null)
            {
            document.getElementById("drpModifyEqup").className="dropdownlist";
            }
            if(document.getElementById("txtModEquipNo")!=null)
            {
            document.getElementById("txtModEquipNo").className="displayNone";
            }
            document.getElementById("hdShowHide").value=""
            }
    
     
     }
     
     
  function fillEquipNo()
  {
  
  
var data=  document.getElementById("hdComboData").value;
     
     try
     {
     //code to show and hide dropdown and textbox based on class
//     var strddlEquipNoClass= document.getElementById("drpModifyEqup").className;
//     var strtxtEquipNoClass= document.getElementById("txtModEquipNo").className;
//      if (strtxtEquipNoClass=="displayNone")
//     {
//     document.getElementById("txtModEquipNo").value="";
//     }
//     //end
//     if (strddlEquipNoClass=="displayNone")
//     {
//     document.getElementById("drpModifyEqup").options.length=0;
//     }
//     else
//     {
            
               //Code to fill dropdown list and showing drop down according to EquipType
     
                    
      document.getElementById('drpModifyEqup').options.length=0;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="-- Select One --";
	  document.getElementById('drpModifyEqup').options[0]=new Option(names, codes );
	  var ddlEquipNo = document.getElementById('drpModifyEqup');
	  if (data=="") 
            {
             listItem = new Option(names, codes );
             ddlEquipNo.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(data);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			var strEQUIPMENT_CODE=document.getElementById("drpModEquipType").value;
			    var orders =dsRoot.getElementsByTagName("DETAILS[@EGROUP_CODE='" + strEQUIPMENT_CODE + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlEquipNo.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
			        
				    codes= orders[count].getAttribute("VENDORSR_NUMBER");
			        names=orders[count].getAttribute("VENDORSR_NUMBER"); 
				    listItem = new Option(names, codes);
				    ddlEquipNo.options[ddlEquipNo.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlEquipNo.options[0] = listItem;
			    }
			}
			}
			catch(err)
			{ 
			
			}
            //End code
    }
  //}
 
  function RefreshMisc()
             {
             window.opener.document.getElementById("hdUpReplace").value="1"
                 window.opener.document.forms['form1'].submit();
            }
             
             
             
             
             
             
             function ValidateEdit()
        {
                
              document.getElementById("hdValidate").value="0"; 
                
               
                 try{
                 
                 if(document.getElementById("drpModEquipType").selectedIndex==0)
                 {
                     document.getElementById("lblError").innerHTML="Equipment Type cann't be blank";
                    try
                    {
                        document.getElementById("drpModEquipType").focus();
                    }
                    catch(err){}
                    return false;
                 }
                 
                 if(document.getElementById("txtModEquipNo").className !="displayNone")
                 {
                if(document.getElementById("txtModEquipNo").value =='')
                {
                document.getElementById("lblError").innerHTML="Equipment No. cann't be blank";
                document.getElementById("txtModEquipNo").focus();
                return false;
                }
                }
                }
                catch(err)
                {}
     //   }
        
       /* if(document.getElementById("ddlEquipNo").className !="displayNone")
        {
                    document.getElementById("txtEquipNo").value="";*/
                    try{
                    if(document.getElementById("drpModifyEqup").className !="displayNone")
                    {
                    if(document.getElementById("drpModifyEqup").selectedIndex =='0')
                    {
                    document.getElementById("lblError").innerHTML="Equipment No. is Mandatory";
                    document.getElementById("drpModifyEqup").focus();
                    return false;
                     }  
                     else
                     {
                     document.getElementById("hdModifyEquipNo").value=document.getElementById("drpModifyEqup").value;
                     }
                     }
                      }
                catch(err)
                {
                
                }
             
            
                 
        } 
        
        

    function ReceiveServerData(args, context)
    {      
   
      
            var result=args.split("|");
            if (result[0]=="0")
            {
            document.getElementById("drpModifyEqup").className="displayNone";
            document.getElementById("txtModEquipNo").className="textbox";
                if(confirm('Given challan number does not exist .Do you want to Continue?')==true)
                {
                    
                  if (document.getElementById("hdOverRide").value == "0" || document.getElementById("hdOverRide").value == "")
                 {
                     document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a ChallanNo";
                    document.getElementById("txtModEquipNo").focus();
                     return false;
                 }                   
                else
                 {
                 try
                 {
                  document.getElementById("drpModifyEqup").focus();
                 
                 }
                 catch(err)
                 {
                 
                 }
                   return false;
                }
                
                }
                else
                {
                    try
                    {
                    document.getElementById("txtModEquipNo").focus();
                    }
                    catch(err)
                    {
                    }
                    return false;
                }
            }
            
     
            
            if (result[0]=="1")
            {
            //storing Equipment number list
            document.getElementById("hdComboData").value=result[4];
            
               //Code to fill dropdown list and showing drop down
                
                    document.getElementById("drpModifyEqup").className="dropdownlist";
                    document.getElementById("txtModEquipNo").className="displayNone";
      document.getElementById('drpModifyEqup').options.length=0;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="-- Select One --";
	  document.getElementById('drpModifyEqup').options[0]=new Option(names, codes );
	  var ddlEquipNo = document.getElementById('drpModifyEqup');
	  if (result[4]=="") 
            {
             listItem = new Option(names, codes );
             ddlEquipNo.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(result[4]);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
			var strEQUIPMENT_CODE=document.getElementById("drpModEquipType").value;
		    
		    var orders = dsRoot.getElementsByTagName("DETAILS[@EQUIPMENT_CODE='" + strEQUIPMENT_CODE + "']");
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlEquipNo.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
			        
				    codes= orders[count].getAttribute("EQUIPMENT_CODE");
			        names=orders[count].getAttribute("VENDORSR_NUMBER"); 
				    listItem = new Option(names, codes);
				    ddlEquipNo.options[ddlEquipNo.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlEquipNo.options[0] = listItem;
			    }
			}
            //End code
            
            
                if(confirm('Given challan No. ' + result[1] + ' is for ' + result[2] + ' OfficeID ' + result[3]  +  ' Want to reuse it for this Agency also ?')==true)
                {
                  if (document.getElementById("hdOverRide").value == "0" || document.getElementById("hdOverRide").value == "")
                 {
                     document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a ChallanNo1";
                    // document.getElementById("txtChallanNo").focus();
                     return false;
                 }  
                 else
                 {
                 try{
                  document.getElementById("drpModEquipType").focus();
                  }
                  catch(err)
                  {
                  }
                                   
                  
                   return false;
                }
                }
                else
                {                    
                    document.getElementById("txtModChallNo").focus();
                }
                
             
                
                
            }
            
            if (result[0]=="-1")
            {
                
                 var strddlEquipNoClass= document.getElementById("drpModifyEqup").className;
             var strtxtEquipNoClass= document.getElementById("txtModEquipNo").className;
               document.getElementById("drpModifyEqup").className=strddlEquipNoClass;
               document.getElementById("txtModEquipNo").className=strtxtEquipNoClass;
                
                document.getElementById("lblError").innerHTML=result[1];
                //document.getElementById("txtChallanNo").focus();
                return false;
            }
            
            if (result[0]=="11")
            {
               
                var strddlEquipNoClass= document.getElementById("drpModifyEqup").className;
             var strtxtEquipNoClass= document.getElementById("txtModEquipNo").className;
               document.getElementById("drpModifyEqup").className=strddlEquipNoClass;
               document.getElementById("txtModEquipNo").className=strtxtEquipNoClass;
               
                var type = "../Popup/PUSR_MiscEquipList.aspx?VENDERSERIALNO=" + result[1] + "&EQUIPMENTTYPE=" + result[2] ;

             window.open(type,"EquipmentList","height=300,width=600,top=30,left=20,scrollbars=1");     	               	    
                return false;
    
            }
            if (result[0]=="12")
            {
               var strddlEquipNoClass= document.getElementById("drpModifyEqup").className;
             var strtxtEquipNoClass= document.getElementById("txtModEquipNo").className;
               document.getElementById("drpModifyEqup").className=strddlEquipNoClass;
               document.getElementById("txtModEquipNo").className=strtxtEquipNoClass;
            }
            
                       
            try
            {
            document.getElementById("btnSave").focus();
            }
            catch(err)
            {
            }
			
    }


    function  openPopup()
     {  
   
     
             var strEquipId=document.getElementById("drpModifyEqup").value;
             strEquipId=strEquipId.trim();
          if( strEquipId!="")
         {
             var item=document.getElementById("drpModifyEqup").selectedIndex;
             var strEquipNo=document.getElementById("drpModifyEqup").options[item].text;
             strEquipNo=strEquipNo.trim();
              CallServer(strEquipNo + "|2|" + document.getElementById("drpModEquipType").value,"This is context from client");
              return false;
         }  
     
     }
     
     
 function validateEquipNo()
   { 
  
      var strEquipNo= document.getElementById("txtModEquipNo").value;
     strEquipNo=strEquipNo.trim();
 /*     if(ddlvalidate('drpEquipType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Equipment Type is Mandatory";
		    document.getElementById('drpEquipType').focus();
		    return false;}*/
     if( strEquipNo!="")
     {
      CallServer(strEquipNo + "|2|" + document.getElementById("drpEquipType").value,"This is context from client");
      return false;
     }  
   }
   



 function validateUpdateReplaceChallan()
    {
    
     var strChallanNo= document.getElementById("txtModChallNo").value;
     strChallanNo=strChallanNo.trim();
     if( strChallanNo=="")
     {
         if (confirm('Challan Number is blank. Want to continue ?'))
         {
         document.getElementById("drpModifyEqup").className="displayNone";
         document.getElementById("txtModEquipNo").className="textbox";
         document.getElementById("drpModifyEqup").options.length=0;
         document.getElementById("txtModEquipNo").value="";
         if (document.getElementById("hdOverRide").value == "0" || document.getElementById("hdOverRide").value == "")
         {
         document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a ChallanNo";
         document.getElementById("txtModEquipNo").focus();
         return false;
         }
         }
     }
          
        
        
        if(document.getElementById("txtModEquipNo").className !="displayNone")
        {
                if(document.getElementById("txtModEquipNo").value.trim() =='')
                {
                document.getElementById("lblError").innerHTML="Equipment No. is Mandatory";
                document.getElementById("txtModEquipNo").focus();
                return false;
                }
        }
        
        if(document.getElementById("drpModifyEqup").className !="displayNone")
        {
                    document.getElementById("txtModEquipNo").value="";
                    if(document.getElementById("drpModifyEqup").selectedIndex =='0')
                    {
                    document.getElementById("lblError").innerHTML="Equipment No. is Mandatory";
                    document.getElementById("drpModifyEqup").focus();
                    return false;
                     }  
                      var item=document.getElementById("drpModifyEqup").selectedIndex;
                      var strEquipNo=document.getElementById("drpModifyEqup").options[item].text;  
                     document.getElementById("hdEuipText").value=strEquipNo;       
        }
        
        if (document.getElementById("hdFlagMiscInstall").value=="0")
        {
               return false;
        }
        
        
        if (document.getElementById("hdFlagMiscInstall").value=="")
        {
        document.getElementById("lblError").innerHTML="";
        if (confirm("This Hardware is already installed .Would you like to install it at this Location also?")==true)
        {
            if (document.getElementById("hdOverRideSerialNo").value == "0" || document.getElementById("hdOverRideSerialNo").value == "")
        {
        document.getElementById("lblError").innerHTML="You don't have enough rights to proceed.";        
        return false;
        }
        }
        else
        {return false;
        }
        
        }
        
        
    }
    

 
           
    
    
        



// function closeWindow()
//     {
//    window.close()
//     }
//      
//      
//        
//     
      function validateChallanNo()
   { 
      var strChallanNo= document.getElementById("txtModChallNo").value;
     strChallanNo=strChallanNo.trim();
         if( strChallanNo=="")
     {
     if (confirm('Challan Number is blank. Want to continue ?'))
     {
     document.getElementById("ddlEquipNo").className="displayNone";
     document.getElementById("txtEquipNo").className="textbox";
     document.getElementById("ddlEquipNo").options.length=0;
     document.getElementById("txtEquipNo").value="";
     if (document.getElementById("hdOverRide").value == "0" || document.getElementById("hdOverRide").value == "")
     {
     document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a ChallanNo";
     return false;
     }
     }
     }
     else
     {
      //CallServer(strChallanNo,"This is context from client");
      
      CallServer(strChallanNo + "|1|" + document.getElementById("drpModEquipType").value,"This is context from client");
      return false;
     }    
     
   }
   
    
    function f1()
   {
  
   if (document.getElementById("hdfocustemp").value=="0")
   {
   document.getElementById("lblError").innerHTML="You don't have enough rights to proceed.";
   document.getElementById("hdfocustemp").value="";
   }
   }
     
     
     
      function HdBtnGridNoClickFunction()
   {
   document.getElementById("HdBtnGridNoClick").value="1"
   }
   
     
     
</script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="860px" align="left" height="360px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Misc. Hardware </span></td>
                        </tr>
                        
                        
                        
                        
                        <tr>
                                    <td valign="top">
                              <asp:Panel ID="pnlErroMsg" Visible="false"  runat="server" Width="100%"  >
                                 <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                 <tr>
                                     <td valign="top" >
                                     <asp:Label ID="lblValidationMsg" CssClass="button" runat="server" Height="20px" Width="712px" EnableViewState="False" ></asp:Label></td>
                                     <td colspan="2">
                                     <table cellpadding="0" cellspacing="0">
                                     <tr valign="top" >
                                     <td>
                                      <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="button" />
                                     </td>
                                     <td>
                                     <asp:Button ID="btnNo" runat="server" Text="No" CssClass="button" />
                                     
                                     </td>
                                     
                                     </tr>
                                     </table>
                                    
                                     
                                     </td>
                                 </tr>
                                 <tr>
                                 <td colspan="3"> 
                                </td>
                                 
                                 </tr>
                                 </table>
                                 </asp:Panel>
                                                                             <asp:Panel ID="pnlGrid" Visible="false"  runat="server" Width="100%"  >
                                                                             <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                 <tr>
                                     <td valign="top" >
                                     <asp:Label ID="lblGrid" CssClass="button" runat="server" Width="712px" Height="20px" EnableViewState="False" ></asp:Label>
                                     </td>
                                     <td colspan="2">
                                     <table cellpadding="0" cellspacing="0" >
                                     <tr valign="top" >
                                     <td>
                                      <asp:Button ID="btnYesGrid" runat="server" Text="Yes" CssClass="button" />
                                     </td>
                                     <td>
                                     <asp:Button ID="btnNoGrid" runat="server" Text="No" CssClass="button" />
                                     
                                     </td>
                                     
                                     </tr>
                                     </table>
                                    
                                     
                                     </td>
                                 </tr>
                                 <tr>
                                 <td colspan="3">    <asp:GridView  ID="gvInstall" runat="server"  AutoGenerateColumns="False"  TabIndex="6" Width="81%">
                                                                                <Columns>
                                                                            
                                                                   <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" />
                                                                   <asp:BoundField DataField="OFFICEID" HeaderText="Office ID"  />
                                                                   <asp:BoundField DataField="DATEINSTALLED" HeaderText="Date" />
                                                   
                                                                
                                                     
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />                                                    
                                                 </asp:GridView></td>
                                 
                                 </tr>
                                 </table>
                                                                             </asp:Panel>
                            </td>
                                    
                                    </tr>
                        
                        
                        
                        
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                <span style="font-family: Microsoft Sans Serif"> Manage Misc. Hardware </span></td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 246px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                        <asp:Panel ID="pnlDataValidation" runat="server" Width="100%" >
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center gap" colspan="1">
                                                                            </td>
                                                                            <td colspan="6" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="false"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td  class="subheading" nowrap="nowrap"  colspan="2" valign="middle" align="left"  style="width: 124px">
                                            Installation Details
                                            </td>
                                           
                                            <td style="width: 124px" nowrap="nowrap" class="subheading" colspan="2">
                                          <asp:Label ID="lblHeading" runat="server" Text="Modification Details"></asp:Label>
                                            </td>
                                            <td  nowrap="nowrap">
                                            </td>
                                            
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px;">
                                                                            </td>
                                                                            <td class="textbold"  style="width: 117px; height: 26px;">
                                                                                Date of Installation</td>
                                                                            <td style="width: 239px; height: 26px;" >
                                                                                <asp:TextBox ID="txtDtInstall" runat="server" CssClass="textboxgrey" MaxLength="10" Width="152px" ReadOnly="True"></asp:TextBox>
                                                                                <img id="imgDateInstall" alt=""  src="../Images/calender.gif" title="Date selector"
                                                                                                                       TabIndex="16" style="cursor: pointer" />
                                                                                                                      
                                                                                </td>
                                                                            <td style="width: 129px; height: 26px;" class="textbold">
                                                                                Date of Modification
                                                                                </td>
                                                                            <td style="width: 219px; height: 26px;" nowrap="nowrap" >
                                                                             <asp:TextBox ID="txtDtModInstall" runat="server" CssClass="textboxgrey" MaxLength="10" Width="144px" ReadOnly="True"></asp:TextBox>
                                                                              <img id="img1" alt="" src="../Images/calender.gif" runat="server"  title="Date selector"
                                                                                                                       TabIndex="16" style="cursor: pointer" />
                                                                                                                       <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    'txtDtModInstall',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "img1",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                                                                                    </script>
                                                                            </td>
                                                                            <td nowrap="nowrap" style="height: 26px">
                                                                            </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold" style="width: 117px">
                                                                                Challan No.</td>
                                                                            <td style="width: 239px">
                                                                   <asp:TextBox ID="txtChallanNo" runat ="server" CssClass ="textboxgrey" Width="176px" MaxLength="30" ReadOnly="True"></asp:TextBox></td>
                                                                   
                                                                            <td style="width: 129px" class="textbold">
                                                                                Challan No.</td>
                                                                            <td style="width: 219px">
                                                                                <asp:TextBox ID="txtModChallNo" runat="server" CssClass="textbox" MaxLength="30" Width="144px"></asp:TextBox>
                                                                                <asp:Button ID="btnValidate" runat="server" CssClass="button" TabIndex="8" Text="Validate"
                                                                                    Width="64px" /></td>
                                                                            <td>
                                                                            </td>
                                                                            <td><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold" style="width: 117px">
                                                                                Equipment Type</td>
                                                                            <td style="width: 239px" >
                                                                                <asp:TextBox ID="txtQuipType" runat="server" CssClass="textboxgrey" MaxLength="30" ReadOnly="True"
                                                                                    Width="176px"></asp:TextBox></td>
                                                                            <td style="width: 129px" class="textbold">
                                                                                Equipment Type
                                                                                 <strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">
                                                                            *</span></strong>
                                                                                </td>
                                                                            <td style="width: 219px"><asp:DropDownList ID="drpModEquipType" runat="server" CssClass="dropdownlist" Width="216px">
                                                                            </asp:DropDownList></td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                    </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 23px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 23px; width: 117px;">
                                                                                Equipment No.
                                                                                
                                                                                </td>
                                                                            <td style="width: 239px">
                                                                                <asp:TextBox ID="txtEquipNo" runat="server" CssClass="textboxgrey" MaxLength="30" Width="176px" ReadOnly="True"></asp:TextBox></td>
                                                                            <td style="width: 129px; height: 23px" class="textbold">
                                                                                Equipment No.
                                                                                <strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">
                                                                            *</span></strong>
                                                                                </td>
                                                                            <td style="width: 219px; height: 23px">
                                                                                <asp:TextBox ID="txtModEquipNo" runat="server" CssClass="textbox" MaxLength="30" Width="208px"></asp:TextBox>
                                                                                <asp:DropDownList ID="drpModifyEqup" runat="server" CssClass="dropdownlist" Width="216px">
                                                                                </asp:DropDownList></td>
                                                                            <td style="height: 23px">
                                                                            </td>
                                                                            <td style="height: 23px">
                                                                            </td>
                                                                        </tr>
                                        <tr>
                                            <td style="height: 27px;">
                                            </td>
                                            <td class="textbold" style="height: 27px; width: 117px;">
                                                <%--Qty. of Equipment--%></td>
                                            <td style="width: 239px; height: 27px;">
                                               <%-- <asp:TextBox ID="txtQtyEquip" runat="server" CssClass="textboxgrey" MaxLength="30" Width="176px" ReadOnly="True"></asp:TextBox>--%></td>
                                            <td style="width: 129px; height: 27px" class="textbold">
                                                <%--Qty. of Equipment--%></td>
                                            <td style="width: 219px; height: 27px">
                                                <%--<asp:TextBox ID="txtModQtyEquip" runat="server" CssClass="textboxgrey" MaxLength="30" Width="168px" ReadOnly="True"></asp:TextBox>--%></td>
                                            <td style="height: 27px">
                                            </td>
                                            <td style="height: 27px">
                                                </td>
                                        </tr>
                                     
                                                                      
                                                                    </table>
                                                                       </asp:Panel> 
                                                                      <input id="hdOverRide" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdEquipCode" runat="server" style="width: 1px" type="hidden" />
                                                 <input id="hdFlagMiscInstall" runat="server" style="width: 1px" type="hidden" />
                                                  <input id="hdOverRideBackDate" runat="server" style="width: 1px" type="hidden" />
                                                 <input id="hdOverRideSerialNo" runat="server" style="width: 1px" type="hidden" />
                                                    <input id="hdfocustemp" runat="server" style="width: 1px" type="hidden" />
                                                    <input id="hdComboData" runat="server" style="width: 1px" type="hidden" />
                                                    <input id="hdEuipText" runat="server" style="width: 1px" type="hidden" />
                                                    <input id="hdchecktype" runat="server" type="hidden" style="width: 1px" />
                                                    <input id="hdchecktype1" runat="server" type="hidden" style="width: 1px" />
                                                    <input type="hidden" id="hdValidate" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="hdAction" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="hdShowHide" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="hdUpReplace" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="hdGridYes" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="hdModifyEquipNo" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="hdChkSerialNo" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="hdChallanRights" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="hdSerialRights" runat="server" style="width: 1px" />
                                                    <input type="hidden" id="HdBtnGridNoClick" runat="server" style="width: 1px" />
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
