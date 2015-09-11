<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyGroup.aspx.vb" Inherits="Setup_MSUP_AgencyGroup" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>AAMS::Travel Agency::Manage Agency Group</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" src="../JavaScript/subModal.js"></script>
<link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
<link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />
 <script language="javascript" type="text/javascript">
     /*********************************************************************
                        Code for Call Back Information
    *********************************************************************/
   var st;
      function PopupAgency()
         {
            var type;  
             var strChain_Code="";
             strChain_Code = document.getElementById('hdEnChainCode').value;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T&EnCodedChainCode=" + strChain_Code ;
   	        window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	     
     } 
   
   
   function SendAoffice(s)
   {
      var id
      st=s;
      if(s=='drpLstAoffice')
      {
         id=document.getElementById('<%=drpLstAoffice.ClientId%>').value;
      }
      id=s+'|'+id;           
      CallServer(id,"This is context from client");
      return false;
   }
   
    function ReceiveServerData(args, context)
    {        
  
           
            var obj = new ActiveXObject("MsXml2.DOMDocument");
         	var codes='';
			var names="-- Select One --";
			var ddlOrders = document.getElementById('<%=drpLstCity.ClientId%>');
			    //alert(h.value)
			    for (var count = ddlOrders.options.length-1; count >-1; count--)
			    {
				    ddlOrders.options[count] = null;
			    }
            if (args=="") 
            {
             listItem = new Option(names, codes,  false, false);
             ddlOrders.options[ddlOrders.length] = listItem;
            }
            else
            {
                obj.loadXML(args);
			    var dsRoot=obj.documentElement;
			    var orders = dsRoot.getElementsByTagName('AOFFICECITY');
			    var text;     			
			    var listItem;
			    listItem = new Option(names, codes,  false, false);
			    ddlOrders.options[ddlOrders.length] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
				    codes= orders[count].getAttribute("CityID"); 
			        names=orders[count].getAttribute("City_Name"); 
				    listItem = new Option(names, codes,  false, false);
				    ddlOrders.options[ddlOrders.length] = listItem;
			    }
			    // Displaying Region on the basis of AOFFICE.
			    var Region = dsRoot.getElementsByTagName('AOFFICECITY')[0].getAttribute("Region");
			    
			    document.getElementById('txtRegion').value=Region;
			}
			
    }
   
  
   
   // PopupCorporateCode
            function PopupCorporateCode()
        {
          var type;

        type = "../TravelAgency/MSSR_CorporateCode.aspx?Popup=T" ;
   	window.open(type,"aa4","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;    
}
 // PopupAccountsManager
 function PopupAccountsManager()
        {
          var type;
          
        type = "MSSR_Employee.aspx?Popup=T" ;
   	    window.open(type,"aa10","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
        return false;    
        }
    </script>

<script type="text/javascript" language="javascript"> 
function TabMethodAgencyGroup(id,total)
{   
//{debugger;}
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
            if (document.getElementById(Hcontrol).className != "displayNone")
            {
                document.getElementById(Hcontrol).className="headingtabactive";
            }
           
        }
       
       var strChain_Code="";
       strChain_Code = document.getElementById('hdEnChainCode').value;
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblPanelClick').value =id; 
       
    
      
       if (id == (ctextFront +  "00" + ctextBack))
       {   
        return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            window.location.href="MSUP_AG_CRSDetails.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;                      
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
           window.location.href="MSUP_AG_Competition.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {    
            window.location.href="MSUP_AG_Staff.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
        else if (id == (ctextFront +  "04" + ctextBack))
       {
             window.location.href="MSUP_AG_PC.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
       else if (id == (ctextFront +  "05" + ctextBack))
       {
             window.location.href="MSUP_AG_Contract.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextFront +  "06" + ctextBack))
       {
             window.location.href="MSUP_AG_BusinessCase.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
}

/*********************************************************************
                        Code for Call Back Information
    *********************************************************************/
   var st;
   function SendAoffice(s)
   {
      var id
      st=s;
      if(s=='drpLstAoffice')
      {
         id=document.getElementById('<%=drpLstAoffice.ClientId%>').value;
      }
      id=s+'|'+id;           
      CallServer(id,"This is context from client");
      return false;
   }
   
    function ReceiveServerData(args, context)
    {        
  
           
            var obj = new ActiveXObject("MsXml2.DOMDocument");
         	var codes='';
			var names="-- Select One --";
			var ddlOrders = document.getElementById('<%=drpLstCity.ClientId%>');
			    //alert(h.value)
			    for (var count = ddlOrders.options.length-1; count >-1; count--)
			    {
				    ddlOrders.options[count] = null;
			    }
            if (args=="") 
            {
             listItem = new Option(names, codes,  false, false);
             ddlOrders.options[ddlOrders.length] = listItem;
            }
            else
            {
                obj.loadXML(args);
			    var dsRoot=obj.documentElement;
			    var orders = dsRoot.getElementsByTagName('AOFFICECITY');
			    var text;     			
			    var listItem;
			    listItem = new Option(names, codes,  false, false);
			    ddlOrders.options[ddlOrders.length] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
				    codes= orders[count].getAttribute("CityID"); 
			        names=orders[count].getAttribute("City_Name"); 
				    listItem = new Option(names, codes,  false, false);
				    ddlOrders.options[ddlOrders.length] = listItem;
			    }
			    // Displaying Region on the basis of AOFFICE.
			    var Region = dsRoot.getElementsByTagName('AOFFICECITY')[0].getAttribute("Region");
			    
			    document.getElementById('txtRegion').value=Region;
			}
			
    }
   
   function CheckMandatoty()
    {
        if (document.getElementById("txtGroupName").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="Group name is mandatory.";
             document.getElementById("txtGroupName").focus();
             return false;
        }
//         if (  document.getElementById("txtGroupName").value!="")
//         {
//           if(IsDataValid(document.getElementById("txtGroupName").value,7)==false)
//            {
//            document.getElementById("lblError").innerHTML="Group name is not valid.";
//            document.getElementById("txtGroupName").focus();
//            return false;
//            } 
//         }
         
         
          if (document.getElementById("ChkMainGroup").checked==true)
              {   
                 if (document.getElementById("txtMainGroupAddress").value.trim()=="")                  
                 {
                     document.getElementById("lblError").innerHTML="Address is mandatory.";
                     document.getElementById("txtMainGroupAddress").focus();
                     return false;
                 }              
              }
               if (document.getElementById("txtMainGroupAddress").value.trim().length>100)                  
                 {
                     document.getElementById("lblError").innerHTML="Address is exceed from 100 characters.";
                     document.getElementById("txtMainGroupAddress").focus();
                     return false;
                 }      
        if (document.getElementById("drpLstAoffice").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Aoffice is mandatory.";
          document.getElementById("drpLstAoffice").focus();
          return false;
        }        
        if (document.getElementById("drpLstCity").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="City is mandatory.";
          document.getElementById("drpLstCity").focus();
          return false;
        }    
        if (document.getElementById("drpLstPriority").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Priority is mandatory.";
          document.getElementById("drpLstPriority").focus();
          return false;
        }    
       if (  document.getElementById("txtCorporateQualifier").value!="")
         {
           if(IsDataValid(document.getElementById("txtCorporateQualifier").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Corporate qualifier is not valid.";
            document.getElementById("txtCorporateQualifier").focus();
            return false;
            } 
         } 
          if (  document.getElementById("txtCorporateCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtCorporateCode").value,6)==false)
            {
            document.getElementById("lblError").innerHTML="Corporate code is not valid.";
            document.getElementById("txtCorporateCode").focus();
            return false;
            } 
         } 
         
          if (document.getElementById("drpLstGroupType").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Group type is mandatory.";
          document.getElementById("drpLstGroupType").focus();
          return false;
        }  
        
         if (document.getElementById("txtPanNo").value.trim()=='')
        {           
        //  document.getElementById("lblError").innerHTML="PAN No. is mandatory.";
        //  document.getElementById("txtPanNo").focus();
        //  return false;
        }   
        //alert(document.getElementById("rdbCompVertical").selectedIndex)        
        /*
        var isrdbCVChecked='False';
        var rdbCV = document.getElementsByName("rdbCompVertical");
        for (var j = 0; j < rdbCV.length; j++)
        {
            if (rdbCV[j].checked)
                {
                isrdbCVChecked='True';
                }                    
        }
        //alert(isrdbCVChecked);
         if (isrdbCVChecked == 'False')
         {
            document.getElementById("lblError").innerHTML="Company Vertical is mandatory.";
            return false;
         }
         */
          return true;
    }



 
   function PopupAgencyGroupManageAgencyFromInc()
{
            document.getElementById("hdFromIncentive").value=1;
            var type;
            type = "../Setup/MSSR_ManageAgencyGroup.aspx?Popup=T" ;
   	        window.open(type,"aInc","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;

}     
</script>

</head>
<body >
    <form id="form1" runat="server"  defaultbutton="btnSave" defaultfocus ="txtGroupName"  >
        <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width:80%">
                                            <span class="menu">Travel Agency-></span><span class="sub_menu">Agency Group</span>
                                        </td>
                                         <td class="right" style="width:20%">
                                             &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width:100%">
                                            Manage Agency Group</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px;width:100%" colspan="2"> 
                                            <asp:Repeater ID="theTabStrip" runat="server" >
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" width="100px" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />
                                                   
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td  class="redborder top" colspan="2" style="width:100%">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center"   style="height:25;" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td width="15%" class="textbold" style="height: 22px">
                                                        Chain Code</td>
                                                    <td style="height: 22px; width: 237px;">
                                                        <asp:TextBox ID="txtChainCode" CssClass="textboxgrey" ReadOnly="true" Text="" runat="server" TabIndex="1" Width="133px"></asp:TextBox>
                                                        <img id="ImgAgroup" runat="server" alt="" onclick="javascript:return PopupAgencyGroupManageAgencyFromInc();"
                                                         tabindex="1"    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                    <td style="height: 22px; width: 111px;">
                                                        <span class="textbold"></span></td>
                                                    <td width="21%" style="height: 22px">
                                                       </td>
                                                    <td width="18%" style="height: 22px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="2" AccessKey="S" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP"    style="height:25;">
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td class="textbold" style="height: 22px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td width="15%" class="textbold" style="height: 22px">
                                                       Group Name<span class="Mandatory">*</span></td>
                                                    <td width="63%" style="height: 22px" colspan ="3">
                                                         <asp:TextBox ID="txtGroupName" CssClass="textfield"  runat="server" TabIndex="1" MaxLength="40" Width ="487px" EnableViewState="False"></asp:TextBox></td>  
                                                    <td width="18%" style="height: 22px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP"    style="height:25;">
                                                    </td>
                                                </tr>                                                
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px" >
                                                        Main Group</td>
                                                    <td style="height: 22px; width: 237px;">
                                                        <asp:CheckBox ID="ChkMainGroup" runat="server" TabIndex="1" /></td>
                                                    <td class="textbold" style="height: 22px; width: 111px;">
                                                        </td>
                                                    <td style="height: 22px">
                                                        </td>
                                                    <td style="height: 22px"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset"  AccessKey="R" TabIndex="2" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP"    style="height:25;">
                                                    </td>
                                                </tr> 
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px" align ="left" ><span class="textbold">Address<span class="Mandatory" id="Add" style="display:none;">*</span></td>
                                                    <td class="textbold" style="height: 22px"  colspan ="3"><asp:TextBox ID="txtMainGroupAddress" CssClass="textfield"  runat="server" TabIndex="1" MaxLength="100" TextMode="MultiLine" Wrap="true" Width ="487px" ></asp:TextBox></td>
                                                    <td style="height: 22px"></td> 
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP"  style="height:25;">
                                                    </td>
                                                </tr>                                                       
                                                <tr>
                                                    <td class="textbold" style="width: 50px;"></td>
                                                    <td class="textbold">
                                                        Aoffice<span class="Mandatory">*</span></td>
                                                    <td style="width: 237px">
                                                        <asp:DropDownList ID="drpLstAoffice" CssClass="dropdownlist"  runat="server" TabIndex="1" Width="137px" >
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 111px">
                                                        City<span class="Mandatory">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="drpLstCity" runat="server" CssClass="dropdownlist" TabIndex="1" Width="137px">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP"   style="height:25;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px">
                                                        Priority<span class="Mandatory"></span></td>
                                                    <td style="height: 22px; width: 237px;">
                                                        <asp:DropDownList ID="drpLstPriority" runat="server" CssClass="dropdownlist" TabIndex="1" Width="137px">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="height: 22px; width: 111px;">
                                                        Corporate Code</td>
                                                    <td style="height: 22px">
                                                        <asp:TextBox ID="txtCorporateCode" runat="server" CssClass="textboxgrey" TabIndex="1" MaxLength="2" ReadOnly="True" Width="133px"></asp:TextBox>
                                                        <img id="IMG1" onclick="javascript:return PopupCorporateCode();" src="../Images/lookup.gif"  style="cursor:pointer;"  tabindex="1"   /></td>
                                                    <td style="height: 22px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td style="width: 237px">
                                                    </td>
                                                    <td style="width: 111px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                        Corporate Qualifier</td>
                                                    <td style="width: 237px">
                                                        <asp:TextBox ID="txtCorporateQualifier" runat="server" CssClass="textfield" Font-Bold="false"
                                                            TabIndex="1" MaxLength="1" EnableViewState="False"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 111px">
                                                        Group Type <span class="Mandatory">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdownlist" TabIndex="1" Width="137px">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td style="width: 237px">
                                                    </td>
                                                    <td class="textbold" style="width: 111px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="height: 19px">
                                                        Type</td>
                                                    <td style="height: 19px; width: 237px;">
                                                        <asp:TextBox ID="txtType" runat="server" CssClass="textboxgrey" MaxLength="2" ReadOnly="True"
                                                            TabIndex="1" Width="133px"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 19px; width: 111px;">
                                                        Account Manager</td>
                                                    <td style="height: 19px">
                                                        <asp:TextBox ID="txtAccountsManager" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                            TabIndex="1" Width="133px"></asp:TextBox>
                                                        <img id="imgAccountManager" onclick="javascript:return PopupAccountsManager();" src="../Images/lookup.gif"  style="cursor:pointer;" alt="Select Accounts Manager"   tabindex="1"  /></td>
                                                    <td style="height: 19px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td style="width: 237px">
                                                    </td>
                                                    <td class="textbold" style="width: 111px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="height: 19px">
                                                        Region</td>
                                                    <td style="height: 19px; width: 237px;">
                                                        <asp:TextBox ID="txtRegion" runat="server" CssClass="textboxgrey" MaxLength="20" ReadOnly="True"
                                                            TabIndex="1" Width="133px"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 19px; width: 111px;">
                                                        PAN No.<span class="Mandatory">*</span></td>
                                                    <td style="height: 19px">
                                                        <asp:TextBox ID="txtPanNo" runat="server" CssClass="textbox" MaxLength="15" TabIndex="1"></asp:TextBox></td>
                                                    <td style="height: 19px">
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td style="width: 237px">
                                                    </td>
                                                    <td class="textbold" style="width: 111px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="height: 19px" valign="top"  >
                                                       Group Main OfficeID</td>
                                                    <td style="height: 19px; width: 237px;" valign="top"  >
                                                     <asp:TextBox ID="TxtGroupOfficeID" runat="server" CssClass="textboxgrey" MaxLength="9" ReadOnly="True"
                                                            TabIndex="1" Width="133px"></asp:TextBox>
                                                       
                                                        <img  style="CURSOR: pointer" id="ImgAgency" tabIndex="1"  runat ="server" onclick="javascript:return PopupAgency();" alt="" src="../Images/lookup.gif"  /></td>
                                                    <td class="textbold" style="height: 19px; width: 111px;" valign="top"  CellSpacing="3">
                                                        Company Vertical<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana"></span></strong></td>
                                                    <td style="height: 19px; font-family: Verdana;">
                                                          <asp:RadioButtonList ID="rdbCompVertical" runat="server" RepeatDirection="Vertical" Width="100%" >
                                                            <asp:ListItem Value="1">Amadeus</asp:ListItem>
                                                            <asp:ListItem Value="2">ResBird</asp:ListItem>
                                                            <asp:ListItem Value="3">Non 1A</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                     </td>
                                                    <td style="height: 19px; font-family: Verdana;">
                                                    </td>
                                                </tr>
                                                <tr style="font-family: Verdana">
                                                    <td class="textbold" style="height: 23px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td colspan="2" style="height: 23px"><asp:HiddenField ID="hdCCodeId" runat ="server" />
                                                    <asp:HiddenField ID="hdQualifier" runat ="server" />
                                                    <asp:HiddenField ID="hdAccountsManagerID" runat ="server" />
                                                    <asp:HiddenField ID="hdChainCode" runat ="server" />
                                                    <asp:HiddenField ID="hdEnChainCode" runat ="server" />
                                                    <asp:HiddenField ID="hdFromIncentive" runat ="server" Value="0" />
                                                    <asp:HiddenField ID="hdChainCodeFromInc" runat ="server" Value="" />
                                                        </td>
                                                    <td style="height: 23px; width: 111px;">
                                                        &nbsp;</td>
                                                    <td style="height: 23px">
                                                        &nbsp;</td>
                                                    <td style="height: 23px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px">
                                                        &nbsp;</td>
                                                    <td colspan="2" class="ErrorMsg">
                                                        Field Marked * are Mandatory
                                                        </td>
                                                    <td style="width: 111px">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
