<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUUP_AgencyGroup.aspx.vb" Inherits="Popup_PUUP_AgencyGroup" EnableEventValidation="FALSE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS:Manage Agency Group</title>
     <base target="_self">
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
			}
			
    }
   
   function  NewMSUPManageAgencyGroup()
   {    
       window.location="PUUP_AgencyGroup.aspx?Action=I";
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
          return true;
    }
    
            function PopupCorporateCode()
        {
          var type;
          type = "../Popup/PUSR_CorporateCode.aspx" 
          var strReturn;   
          if (window.showModalDialog)
          {
              strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
          }
          else
          {
              strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
          }	  
          if (strReturn != null)
          {
              var sPos = strReturn.split('|');       
              document.getElementById('<%=hdCCodeId.ClientID%>').value=sPos[0];
              document.getElementById('<%=txtCorporateCode.ClientID%>').value=sPos[0];
//              document.getElementById('<%=hdQualifier.ClientID%>').value=sPos[1];
//              document.getElementById('<%=txtCorporateQualifier.ClientID%>').value=sPos[1];
          }
}
    </script>
</head>
<body>
    <form id="frmAgency" runat="server" defaultfocus ="txtGroupName">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Agency Group</span>
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
                                        <td align="LEFT" class="redborder">
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
                                                        <asp:TextBox ID="txtChainCode" CssClass="textboxgrey" ReadOnly="true" runat="server" TabIndex="1"></asp:TextBox></td>
                                                    <td width="12%" style="height: 22px">
                                                        <span class="textbold"></span></td>
                                                    <td width="21%" style="height: 22px">
                                                       </td>
                                                    <td width="18%" style="height: 22px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="11" /></td>
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
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="12" /></td>
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
                                                    <td style="height: 22px"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="13" /></td>
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
                                                    <td class="textbold" style="height: 22px; width: 50px;"></td>
                                                    <td class="textbold" style="height: 22px">
                                                        Aoffice<span class="Mandatory">*</span></td>
                                                    <td style="height: 22px">
                                                        <asp:DropDownList ID="drpLstAoffice" CssClass="dropdown"  runat="server" TabIndex="5" Height="30px" >
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="height: 22px">
                                                        City<span class="Mandatory">*</span></td>
                                                    <td style="height: 22px">
                                                        <asp:DropDownList ID="drpLstCity" runat="server" CssClass="dropdown" TabIndex="6" Height="30px">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 22px">
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
                                                        <img id="IMG1" onclick="javascript:return PopupCorporateCode();" src="../Images/lookup.gif" /></td>
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
                                                        Group Type</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdown" TabIndex="10">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 23px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td colspan="2" style="height: 23px"><asp:HiddenField ID="hdCCodeId" runat ="server" /><asp:HiddenField ID="hdQualifier" runat ="server" />
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
                                                        Field Marked * are Mandoatry
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

