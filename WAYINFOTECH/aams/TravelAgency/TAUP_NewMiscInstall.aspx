<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_NewMiscInstall.aspx.vb"
    Inherits="TravelAgency_TAUP_NewMiscInstall" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modify Misc. Hardware </title>

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

        function CloseWindow()
       {
       
                    window.opener.document.getElementById('hdAdd').value='1';
                    
                    window.opener.document.forms['form1'].submit();
                    window.close();
                    
                   return false;
       }
       
       
    var st;
  function fillEquipNo()
  {
   
   try
   {
   
     var data=  document.getElementById("hdComboData").value;
     //code to show and hide dropdown and textbox based on class
     var strddlEquipNoClass= document.getElementById("ddlEquipNo").className;            
    //Code to fill dropdown list and showing drop down according to EquipType
                         
      document.getElementById('ddlEquipNo').options.length=0;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="-- Select One --";
	  document.getElementById('ddlEquipNo').options[0]=new Option(names, codes );
	  var ddlEquipNo = document.getElementById('ddlEquipNo');
	  if (data=="") 
            {
             listItem = new Option(names, codes );
             ddlEquipNo.options[0] = listItem;
            }
            else
            {
          //debugger;
                obj.loadXML(data);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   
	     		var strEQUIPMENT_CODE=document.getElementById("drpEquipType").value;
	     		var strEGroupCode = document.getElementById('<%=drpEquipType.ClientId%>').options[document.getElementById('<%=drpEquipType.ClientId%>').selectedIndex].text;
	     		
			    var orders = dsRoot.getElementsByTagName("DETAILS[@EGROUP_CODE='" + strEQUIPMENT_CODE + "' and @EQUIPMENT_CODE='"+ strEGroupCode +"']");
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
			catch(err){}
            //End code
    }
 // }
  
  
  
   function validateChallanNo()
   { 
      var strChallanNo= document.getElementById("txtChallanNo").value;
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
      CallServer(strChallanNo + "|1|" + document.getElementById("drpEquipType").value,"This is context from client");
      return false;
     }    
     
   }
   
   
    function validateEquipNo()
   { 
     var strEquipNo= document.getElementById("txtEquipNo").value;
     strEquipNo=strEquipNo.trim();
     
    /*if(ddlvalidate('drpEquipType')==true )
    {       }
	else{ document.getElementById("lblError").innerText="Equipment Type is Mandatory";
		    document.getElementById('drpEquipType').focus();
		    return false;}
    */
    
     if( strEquipNo!="")
     {
      CallServer(strEquipNo + "|2|" + document.getElementById("drpEquipType").value,"This is context from client");
      return false;
     }  
   }
   
    function ReceiveServerData(args, context)
    {   
    
    //debugger     
            var result=args.split("|");
            
            if (result[0]=="0")
            {
            document.getElementById("ddlEquipNo").className="displayNone";
            document.getElementById("txtEquipNo").className="textbox";
                if(confirm('Given challan number does not exist .Do you want to Continue?')==true)
                {
                    
                  if (document.getElementById("hdOverRide").value == "0" || document.getElementById("hdOverRide").value == "")
                 {
                     document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a ChallanNo";
                     document.getElementById("txtChallanNo").focus();
                     return false;
                 }                   
                else
                 {
                  document.getElementById("drpEquipType").focus();
                   return false;
                }
                
                }
                else
                {
                    document.getElementById("txtChallanNo").focus();
                    return false;
                }
            }
            
     
            
      if (result[0]=="1")
      {
      //storing Equipment number list
      document.getElementById("hdComboData").value=result[4];
            
      //Code to fill dropdown list and showing drop down
                
      document.getElementById("ddlEquipNo").className="dropdownlist";
      document.getElementById("txtEquipNo").className="displayNone";
      document.getElementById('ddlEquipNo').options.length=0;
      var obj = new ActiveXObject("MsXml2.DOMDocument");
      var codes='';
	  var names="-- Select One --";
	  document.getElementById('ddlEquipNo').options[0]=new Option(names, codes );
	  var ddlEquipNo = document.getElementById('ddlEquipNo');
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
			var strEQUIPMENT_CODE=document.getElementById("drpEquipType").value;
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
                     document.getElementById("txtChallanNo").focus();
                     return false;
                 }  
                 else
                 {
                  document.getElementById("drpEquipType").focus();
                   return false;
                }
                }
                else
                {                    
                    document.getElementById("txtChallanNo").focus();
                }
            }
            
            if (result[0]=="-1")
            {
                
                 var strddlEquipNoClass= document.getElementById("ddlEquipNo").className;
             var strtxtEquipNoClass= document.getElementById("txtEquipNo").className;
               document.getElementById("ddlEquipNo").className=strddlEquipNoClass;
               document.getElementById("txtEquipNo").className=strtxtEquipNoClass;
                
                document.getElementById("lblError").innerHTML=result[1];
                document.getElementById("txtChallanNo").focus();
                return false;
            }
            
            if (result[0]=="11")
            {
               
                var strddlEquipNoClass= document.getElementById("ddlEquipNo").className;
             var strtxtEquipNoClass= document.getElementById("txtEquipNo").className;
               document.getElementById("ddlEquipNo").className=strddlEquipNoClass;
               document.getElementById("txtEquipNo").className=strtxtEquipNoClass;
               
                var type = "../Popup/PUSR_MiscEquipList.aspx?VENDERSERIALNO=" + result[1] + "&EQUIPMENTTYPE=" + result[2] ;

             window.open(type,"EquipmentList","height=300,width=630,top=230,left=220,scrollbars=1");     	               	    
                return false;
    


   	            
            }
            if (result[0]=="12")
            {
               var strddlEquipNoClass= document.getElementById("ddlEquipNo").className;
             var strtxtEquipNoClass= document.getElementById("txtEquipNo").className;
               document.getElementById("ddlEquipNo").className=strddlEquipNoClass;
               document.getElementById("txtEquipNo").className=strtxtEquipNoClass;
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
     
     var strEquipId=document.getElementById("ddlEquipNo").value;
     strEquipId=strEquipId.trim();
      if( strEquipId!="")
     {
     var item=document.getElementById("ddlEquipNo").selectedIndex;
     var strEquipNo=document.getElementById("ddlEquipNo").options[item].text;
     strEquipNo=strEquipNo.trim();
      CallServer(strEquipNo + "|2|" + document.getElementById("drpEquipType").value,"This is context from client");
      return false;
     }  
     
     }
       
         
    
    
    function validateNewChallan()
    {
   
        if(document.getElementById("txtDtInstall").value =='')
        {
        document.getElementById("lblError").innerHTML="Installation Date is Mandatory";
        document.getElementById("txtDtInstall").focus();
        return false;
        }
        
        
    /*  var strChallanNo= document.getElementById("txtChallanNo").value;
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
         document.getElementById("txtChallanNo").focus();
         return false;
         }
         }
     }
        */  
        
        
    /*    if(document.getElementById("txtEquipNo").className !="displayNone")
        {*/
         try{
                if(document.getElementById("txtEquipNo").value.trim() =='')
                {
                document.getElementById("lblError").innerHTML="Equipment No. is Mandatory";
                document.getElementById("txtEquipNo").focus();
                return false;
                }
                }
                catch(err)
                {}
     //   }
        
       /* if(document.getElementById("ddlEquipNo").className !="displayNone")
        {
                    document.getElementById("txtEquipNo").value="";*/
                    try{
                    if(document.getElementById("ddlEquipNo").selectedIndex =='0')
                    {
                    document.getElementById("lblError").innerHTML="Equipment No. is Mandatory";
                    document.getElementById("ddlEquipNo").focus();
                    return false;
                     }  
                      }
                catch(err)
                {}
                  /*    var item=document.getElementById("ddlEquipNo").selectedIndex;
                      var strEquipNo=document.getElementById("ddlEquipNo").options[item].text;  
                     document.getElementById("hdEuipText").value=strEquipNo;       
        }*/
        
        
        try
        {
            if(document.getElementById("txtQtyEquip").value.trim() =='')
            {
            document.getElementById("lblError").innerHTML="Equipment Quantity is Mandatory";
            document.getElementById("txtQtyEquip").focus();
            return false;
            }
            
             if(document.getElementById("txtQtyEquip").value.trim() != "" )
		         {
		            if (_isInteger(document.getElementById("txtQtyEquip").value)==false)
		          {
		          document.getElementById("lblError").innerText = "Only Numbers allowed ";
		          document.getElementById("txtQtyEquip").focus();
		          return false;
		          }
		          }
        }
        catch(err){}
       /* if (document.getElementById("hdFlagMiscInstall").value=="0")
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
        
        }*/
        if (document.getElementById("hdValidate").value=="1")
        {
        document.getElementById("hdValidate").value="0";
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
        <table width="560px" align="left" height="300px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Add Misc. Hardware
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                <asp:LinkButton ID="btnClose" runat="server" Text="Close" CssClass="LinkButtons" /></td>
                        </tr>
                        <tr>
                            <td valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="pnlErroMsg" Visible="false" runat="server" Width="100%">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="lblValidationMsg" CssClass="button" runat="server" Width="420px" EnableViewState="False"
                                                    Height="20px"></asp:Label>
                                            </td>
                                            <td colspan="2" valign="top">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
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
                                            <td valign="top">
                                            </td>
                                            <td colspan="2" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlGrid" Visible="false" runat="server" Width="100%">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="lblGrid" CssClass="button" runat="server" Width="420px" EnableViewState="False"
                                                    Height="20px"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
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
                                            <td colspan="3">
                                                <asp:GridView ID="gvInstall" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" />
                                                        <asp:BoundField DataField="OFFICEID" HeaderText="Office ID" />
                                                        <asp:BoundField DataField="DATEINSTALLED" HeaderText="Date" />
                                                    </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                <span style="font-family: Microsoft Sans Serif">Manage Misc. Hardware </span>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                            <asp:Panel ID="pnlDataValidation" runat="server">
                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                    <tr>
                                                        <td colspan="4" class="center gap">
                                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%">
                                                        </td>
                                                        <td class="textbold">
                                                            Date of Installation <span class="Mandatory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDtInstall" runat="server" CssClass="textboxgrey" ReadOnly="true"
                                                                MaxLength="30" Width="160px" TabIndex="1"></asp:TextBox>
                                                            <img id="imgDateApproval" runat="server" alt="" src="../Images/calender.gif" title="Date selector"
                                                                style="cursor: pointer" tabindex="2" />

                                                            <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDtInstall.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateApproval",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                            </script>

                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="7" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold">
                                                            Challan No.</td>
                                                        <td>
                                                            <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textbox" Width="110px" MaxLength="17"
                                                                TabIndex="3"></asp:TextBox>
                                                            <asp:Button ID="btnValidate" CssClass="button" runat="server" Text="Validate" TabIndex="8"
                                                                Width="64px" /></td>
                                                        <td>
                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="8" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold">
                                                            Equipment Type<span class="Mandatory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drpEquipType" runat="server" CssClass="dropdownlist" Width="190px"
                                                                TabIndex="4">
                                                            </asp:DropDownList></td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px">
                                                        </td>
                                                        <td class="textbold" style="height: 23px">
                                                            Equipment No. <span class="Mandatory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEquipNo" runat="server" Visible="false" CssClass="dropdownlist"
                                                                Width="190px" TabIndex="4">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtEquipNo" runat="server" CssClass="textbox" MaxLength="25" Width="184px"
                                                                TabIndex="5"></asp:TextBox></td>
                                                        <td style="height: 23px">
                                                            <asp:HiddenField ID="hdSecCheck" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px">
                                                        </td>
                                                        <td class="textbold" style="height: 23px">
                                                            <%--  Qty. of Equipment
                                                <span class="Mandatory">*</span>--%>
                                                        </td>
                                                        <td style="height: 23px">
                                                            <%--<asp:TextBox ID="txtQtyEquip" runat="server" CssClass="textbox" MaxLength="3" Width="184px" TabIndex="6"></asp:TextBox>--%>
                                                        </td>
                                                        <td style="height: 23px">
                                                            <asp:HiddenField ID="hdChallanNo" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px">
                                                        </td>
                                                        <td class="textbold" style="height: 23px">
                                                        </td>
                                                        <td style="height: 23px">
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
                                                            <input type="hidden" id="hdSaveClicked" runat="server" style="width: 1px" />
                                                            <input type="hidden" id="hdChallanRights" runat="server" style="width: 1px" />
                                                            <input type="hidden" id="hdSerialRights" runat="server" style="width: 1px" />
                                                            <input type="hidden" id="HdBtnGridNoClick" runat="server" style="width: 1px" />
                                                            <input type="hidden" id="hdTextGridShow" runat="server" style="width: 1px" />
                                                        </td>
                                                        <td style="height: 23px">
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
    </form>
</body>
</html>
