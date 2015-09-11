<%@ Page Language="VB"   AutoEventWireup="false" CodeFile="MSUP_ManageAgencyGroup.aspx.vb" EnableEventValidation="false"
    Inherits="Setup_MSUP_ManageAgencyGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS:Manage Agency Group</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
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
   
   function  NewMSUPManageAgencyGroup()
   {    
       window.location="MSUP_ManageAgencyGroup.aspx?Action=I";
       return false;
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
         
         
         
          return true;
    }
   // PopupAccountsManager
            function PopupCorporateCode()
        {
          var type;
//          type = "../Popup/PUSR_CorporateCode.aspx" 
//          var strReturn;   
//          if (window.showModalDialog)
//          {
//              strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
//          }
//          else
//          {
//              strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
//          }	  
//          if (strReturn != null)
//          {
//              var sPos = strReturn.split('|');       
//              document.getElementById('<%=hdCCodeId.ClientID%>').value=sPos[0];
//              document.getElementById('<%=txtCorporateCode.ClientID%>').value=sPos[0];
////              document.getElementById('<%=hdQualifier.ClientID%>').value=sPos[1];
////              document.getElementById('<%=txtCorporateQualifier.ClientID%>').value=sPos[1];
//          }
        type = "../TravelAgency/MSSR_CorporateCode.aspx?Popup=T" ;
   	window.open(type,"aa4","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;    
}

 function PopupAccountsManager()
        {
          var type;
        type = "MSSR_Employee.aspx?Popup=T" ;
   	    window.open(type,"aa10","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
        return false;    
        }
    </script>
</head>
<body >
    <form id="form1" runat="server" defaultfocus ="txtGroupName">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-></span><span class="sub_menu">Agency Group</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Manage Agency Group
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder" >
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
                                                    <td width="30%" style="height: 22px">
                                                        <asp:TextBox ID="txtChainCode" CssClass="textboxgrey" ReadOnly="true" Text="1001" runat="server" TabIndex="1"></asp:TextBox></td>
                                                    <td width="12%" style="height: 22px">
                                                        <span class="textbold"></span></td>
                                                    <td width="21%" style="height: 22px">
                                                       </td>
                                                    <td width="18%" style="height: 22px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="11" AccessKey="S" /></td>
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
                                                         <asp:TextBox ID="txtGroupName" CssClass="textfield"  runat="server" TabIndex="2" MaxLength="40" Width ="485" EnableViewState="False"></asp:TextBox></td>  
                                                    <td width="18%" style="height: 22px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="12" AccessKey="N" /></td>
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
                                                    <td style="height: 22px">
                                                        <asp:CheckBox ID="ChkMainGroup" runat="server" TabIndex="3" /></td>
                                                    <td class="textbold" style="height: 22px">
                                                        </td>
                                                    <td style="height: 22px">
                                                        </td>
                                                    <td style="height: 22px"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset"  AccessKey="R" TabIndex="13" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP"    style="height:25;">
                                                    </td>
                                                </tr> 
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px" align ="left" ><span class="textbold">Address<span class="Mandatory" id="Add" style="display:none;">*</span></td>
                                                    <td class="textbold" style="height: 22px"  colspan ="3"><asp:TextBox ID="txtMainGroupAddress" CssClass="textfield"  runat="server" TabIndex="4" MaxLength="100" TextMode="MultiLine" Wrap="true" Width ="485" ></asp:TextBox></td>
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
                                                    <td><asp:DropDownList ID="drpLstAoffice" runat="server" CssClass="dropdown" TabIndex="7">
                                                    </asp:DropDownList></td>
                                                    <td class="textbold">
                                                        City<span class="Mandatory">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="drpLstCity" runat="server" CssClass="dropdownlist" TabIndex="6" Width="133px">
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
                                                        Priority<span class="Mandatory">*</span></td>
                                                    <td style="height: 22px">
                                                        <asp:DropDownList ID="drpLstPriority" runat="server" CssClass="dropdown" TabIndex="7">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="height: 22px">
                                                        Corporate Code</td>
                                                    <td style="height: 22px">
                                                        <asp:TextBox ID="txtCorporateCode" runat="server" CssClass="textboxgrey" TabIndex="8" MaxLength="2" ReadOnly="True"></asp:TextBox>
                                                        <img id="IMG1" onclick="javascript:return PopupCorporateCode();" src="../Images/lookup.gif"  style="cursor:pointer;"  /></td>
                                                    <td style="height: 22px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
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
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                        Corporate Qualifier</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCorporateQualifier" runat="server" CssClass="textfield" Font-Bold="false"
                                                            TabIndex="9" MaxLength="1" EnableViewState="False"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        Group Type <span class="Mandatory">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdown" TabIndex="10">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
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
                                                        Group Classification</td>
                                                    <td style="height: 19px">
                                                        <asp:DropDownList ID="drpGroupClassification" runat="server" CssClass="dropdown" TabIndex="11">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="height: 19px">
                                                        Account Manager</td>
                                                    <td style="height: 19px">
                                                        <asp:TextBox ID="txtAccountsManager" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                            TabIndex="8"></asp:TextBox>
                                                        <img id="imgAccountManager" onclick="javascript:return PopupAccountsManager();" src="../Images/lookup.gif"  style="cursor:pointer;" alt="Select Accounts Manager" /></td>
                                                    <td style="height: 19px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 50px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
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
                                                    <td style="height: 19px">
                                                        <asp:TextBox ID="txtRegion" runat="server" CssClass="textboxgrey" MaxLength="20" ReadOnly="True"
                                                            TabIndex="8"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 19px">
                                                    </td>
                                                    <td style="height: 19px">
                                                    </td>
                                                    <td style="height: 19px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 23px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td colspan="2" style="height: 23px"><asp:HiddenField ID="hdCCodeId" runat ="server" />
                                                    <asp:HiddenField ID="hdQualifier" runat ="server" />
                                                    <asp:HiddenField ID="hdAccountsManagerID" runat ="server" />
                                                        </td>
                                                    <td style="height: 23px">
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
                                                    <td>
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
