<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_Bcase.aspx.vb" Inherits="Incentive_INCSR_Bcase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Travel Agency::Manage Business Case</title>
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

    <script type="text/javascript" language="javascript"> 
    
 function gotops(ddlname,Id)
         {    
             if (event.keyCode==46 )
             {
                document.getElementById(ddlname).selectedIndex=0;
                if(Id=="1" )
                { 
                    PayType();
                }
                if(Id=="2" )
                  {
                     PLBType();
                  }
             }
         }         
function GetCheckedVal(radioID)
{
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

function PLBType()
{
       var incPLBTypeVal= document.getElementById("DlstPLBApplicable").value ;
//        if (document.getElementById("ChkPLB").checked ==true)
  if (incPLBTypeVal=="1")
        {
            document.getElementById("TRPLBType").className="displayBlock";               
            document.getElementById("DsltPLBType").value="";
        }
        else
        {
            document.getElementById("TRPLBType").className="displayNone";
         
        }      
}
      function PayType()
    {

      //  var incPayTypeVal=GetCheckedVal('DlstPayType');  
        var incPayTypeVal= document.getElementById("DlstPayType").value ;
        if (incPayTypeVal=="1")
        {
            document.getElementById("TrPaymentTerm").className="displayBlock"; 
            document.getElementById("DlstPayTerm").value="";              
         
        }
        else
        {
            document.getElementById("TrPaymentTerm").className="displayNone";
         
        }        
    }
    
    
    
     function  NewMSUPManageAgencyGroupFromIncentive()
   {    
      // window.location="MSUP_ManageAgencyGroup.aspx?Action=I";
       window.location="../Setup/MSUP_AgencyGroup.aspx?Action=I&OpenFromInc=I";
       return false;
   } 
    
  function PopupAgencyGroupForIncentive()
{
            var type;
            type = "../Setup/MSSR_ManageAgencyGroup.aspx?Popup=T" ;
   	        window.open(type,"IncSBC","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;

}       
  
    
         function ValidateForm()
    {
    
      document.getElementById('lblError').innerText=''
      
      
        if(document.getElementById('<%=txtBCID.ClientId%>').value !='')
        {         
           var strValue = document.getElementById('<%=txtBCID.ClientId%>').value
           reg = new RegExp("^[0-9]+$"); 
           if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=lblError.ClientId%>').innerText ='BC ID should contain only digits.'
                document.getElementById('<%=txtBCID.ClientId%>').focus()
                return false;
           }
           else
           {
                if(document.getElementById('<%=txtBCID.ClientId%>').value =='0') 
                {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='BC ID  should be greater than zero.'
                    document.getElementById('<%=txtBCID.ClientId%>').focus()
                    return false;
               }             
           
           }
        }
      
     
      
       if(document.getElementById('<%=txtChainCode.ClientId%>').value !='')
        {         
           var strValue = document.getElementById('<%=txtChainCode.ClientId%>').value
           reg = new RegExp("^[0-9]+$"); 
           if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Chain code should contain only digits.'
                document.getElementById('<%=txtChainCode.ClientId%>').focus()
                return false;
           }
           else
           {
                if(document.getElementById('<%=txtChainCode.ClientId%>').value =='0') 
                {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='Chain code should be greater than zero.'
                    document.getElementById('<%=txtChainCode.ClientId%>').focus()
                    return false;
               }             
           
           }
        }
      
     
        //      Checking txtOpenDateFrom .
        if(document.getElementById('txtStartDateFrom').value != '')
        {
            if (isDate(document.getElementById('txtStartDateFrom').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerText = "Effective from is not valid.";			
	           document.getElementById('txtStartDateFrom').focus();
	           return(false);  
            }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('txtStartDateTo').value != '')
        {
            if (isDate(document.getElementById('txtStartDateTo').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerText = "Valid till date is not valid.";			
	           document.getElementById('txtStartDateTo').focus();
	           return(false);  
            }
        }   
        
     
   if (compareDates(document.getElementById('txtStartDateFrom').value,"d/M/yyyy",document.getElementById('txtStartDateTo').value,"d/M/yyyy")==1)
       { 
            document.getElementById('lblError').innerText ='Valid till date should be greater than or equal to Effective from.'
            return false;
       }
      
       return true; 
        
    }
    
    
       function EditFunction(BCaseID, ChainCode)               
        {          
               var type;       
                type = "../Incentive/INCUP_BacseDetails.aspx?Action=U&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
   	            //window.open(type,"IncUp","height=900,width=1224,top=30,left=20,scrollbars=1,status=1");	                    
               window.location =type;
                return false;
        }   
        
                  function NewFunction(ChainCode)
            {
            var type;
            type = "../Incentive/INCUP_BacseDetails.aspx?Action=N&Chain_Code=" + ChainCode;

           window.open(type,"aa","height=630,width=1000,top=30,left=20,scrollbars=1,status=1,resizable=1;");
           // window.location =type;
            return false;
            }

        
             function DetailsFunction(BCaseID, ChainCode)
        {          
              var type;       
             type = "../Incentive/INCUP_BacseDetails.aspx?Action=U&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
   	        
   	       // type = "../Incentive/INCSR_BacseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
   	            window.open(type,"IncDetails","height=630,width=1000,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
             // window.location ="MSSR_BcaseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
              return false;
        }   
        
             function ViewDocFunction(BCaseID, ChainCode)
        {        
        
            var type;       
           // type = "MSUP_BcaseDetails.aspx?Action=N&Chain_Code=" + ChainCode; 
   	          //  window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	         
            //  window.location ="MSUP_City.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
              return false;
        }   

    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtBCID" defaultbutton="btnSearch">
        <table width="845px" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width: 80%; height: 18px;">
                                            <span class="menu">Incentive-&gt;</span><span class="sub_menu">Business Case</span>
                                        </td>
                                        <td class="right" style="width: 20%; height: 18px;">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width: 100%">
                                            Search Business Case</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="6">
                                                        <table width="840px" cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td class="textbold" colspan="5" align="center" height="20px" valign="TOP" style="width: 839px;">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold" style="height: 22px">
                                                                    BC ID</td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:TextBox ID="txtBCID" runat="server" CssClass="textfield" TabIndex="1" MaxLength="6"
                                                                        EnableViewState="False"></asp:TextBox></td>
                                                                <td width="12%" class="textbold" style="height: 22px">
                                                                    Chain Code</td>
                                                                <td width="21%" style="height: 22px">
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textfield" EnableViewState="False"
                                                                        MaxLength="6" TabIndex="1"></asp:TextBox><img src="../Images/lookup.gif" onclick="javascript:return PopupAgencyGroupForIncentive();"
                                                                            id="ImgAGroup" style="cursor: pointer;" alt="" runat="server" /></td>
                                                                <td width="18%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"
                                                                        AccessKey="S" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Effective From</td>
                                                                <td style="height: 22px">
                                                                    <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                                        TabIndex="1" Width="132px"></asp:TextBox><img id="imgStartDateFrom" alt="" src="../Images/calender.gif"
                                                                            style="cursor: pointer" tabindex="1" title="Date selector" />

                                                                    <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtStartDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                                    </script>

                                                                </td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Valid Till Date</td>
                                                                <td style="height: 22px">
                                                                    <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" MaxLength="10"
                                                                        TabIndex="1" Width="132px"></asp:TextBox><img id="imgStartDateTo" alt="" src="../Images/calender.gif"
                                                                            style="cursor: pointer" tabindex="1" title="Date selector" />

                                                                    <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtStartDateTo.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                                    </script>

                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" runat="server" AccessKey="N" CssClass="button" TabIndex="2"
                                                                        Text="New" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Signup Adjustable</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList ID="DsltSignUpAdjstable" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="138px">
                                                                        <asp:ListItem Text="--All--" Selected="True" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No " Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left" class="textbold" valign="middle">
                                                                    Adjustment
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DlstAdjustment" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="138px">
                                                                        <asp:ListItem Text="--All--" Value="" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Rate" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Fixed" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td width="18%" valign="top">
                                                                    <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="2"
                                                                        AccessKey="E" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Payment Type</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList ID="DlstPayType" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotops(this.id,1)"
                                                                        Width="138px">
                                                                        <asp:ListItem Text="--All--" Selected="True" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Upfront" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Post " Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left" valign="middle" colspan="2" width="100%">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr id="TrPaymentTerm" runat="server" class="displayNone">
                                                                            <td width="100px" class="textbold" valign="middle">
                                                                                Payment Term
                                                                            </td>
                                                                            <td valign="middle">
                                                                                <asp:DropDownList ID="DlstPayTerm" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                    Width="138px">
                                                                                    <asp:ListItem Text="--All--" Value="" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="One Time" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Replinishable" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="Fixed" Value="3"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td width="18%" valign="top">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                        AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    PLB Applicable</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList ID="DlstPLBApplicable" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotops(this.id,2)"
                                                                        Width="138px">
                                                                        <asp:ListItem Text="--All--" Value="" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left" valign="middle" colspan="2" width="100%">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr id="TRPLBType" runat="server" class="displayNone">
                                                                            <td width="100px" class="textbold" valign="middle">
                                                                                PLB Type
                                                                            </td>
                                                                            <td valign="middle">
                                                                                <asp:DropDownList ID="DsltPLBType" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                    Width="138px">
                                                                                    <asp:ListItem Text="--All--" Value="" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Slab" Value="2"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td width="18%" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr height="20">
                                                                <td class="textbold">
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr height="20">
                                                                <td class="textbold">
                                                                </td>
                                                                <td colspan="4">
                                                                    <div id="DivFAColor" runat="server" visible="false">
                                                                        <b>Note: &nbsp;<asp:Label ID="lblFAColor" runat="server" BackColor="LightSeaGreen"
                                                                            Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                            Denotes currently running business deal.</b></div>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                        <asp:GridView ID="GvBCaseDeals" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                            ShowFooter="false" ShowHeader="true" Width="915px" EnableViewState="true" AllowSorting="true">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="BCaseId" ItemStyle-Width="50px" DataField="BC_ID" SortExpression="BC_ID"
                                                                    HeaderStyle-Width="50px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Chain Code" DataField="CHAIN_CODE" ItemStyle-Width="60px"
                                                                    SortExpression="CHAIN_CODE"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Group Name" DataField="GROUP_NAME" ItemStyle-Width="130px"
                                                                    SortExpression="GROUP_NAME"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Deal Type" DataField="INC_TYPE_NAME" ItemStyle-Width="80px"
                                                                    SortExpression="INC_TYPE_NAME"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Payment Term" DataField="UPFRONTTYPENAME" HeaderStyle-Width="80px"
                                                                    SortExpression="UPFRONTTYPENAME" ItemStyle-Width="80px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Payment Cycle" DataField="PAYMENT_CYCLE_NAME" HeaderStyle-Width="60px"
                                                                    SortExpression="PAYMENT_CYCLE_NAME" ItemStyle-Width="60px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Adjustment" DataField="PAYMENTTYPENAME" HeaderStyle-Width="70px"
                                                                    SortExpression="PAYMENTTYPENAME" ItemStyle-Width="70px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="PLB Type" DataField="PLBTYPENAME" HeaderStyle-Width="40px"
                                                                    SortExpression="PLBTYPENAME" ItemStyle-Width="40px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Signup Adjust" DataField="ADJUSTABLE" HeaderStyle-Width="45px"
                                                                    SortExpression="ADJUSTABLE" ItemStyle-Width="45px"></asp:BoundField>
                                                                      <asp:BoundField HeaderText="Valid From" DataField="BC_EFFECTIVE_FROM" HeaderStyle-Width="75px"
                                                                    SortExpression="BC_EFFECTIVE_FROM" ItemStyle-Width="80px" ItemStyle-Wrap="true">
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Valid Till" DataField="BC_VALID_TILL" HeaderStyle-Width="75px"
                                                                    SortExpression="BC_VALID_TILL" ItemStyle-Width="80px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Status" DataField="APPROVAL_STATUS_NAME" HeaderStyle-Width="60px"
                                                                    SortExpression="APPROVAL_STATUS_NAME" ItemStyle-Width="60px"></asp:BoundField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a>&nbsp;&nbsp;
                                                                        <a href="#" class="LinkButtons" id="linkViewDoc" visible="false" runat="server">ViewDoc</a>&nbsp;&nbsp;
                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server" visible="false">Edit</a>&nbsp;&nbsp;
                                                                        <asp:HiddenField ID="hdBCaseID" runat="server" Value='<%#Eval("BC_ID" )%>' />
                                                                        <asp:HiddenField ID="hdActive" runat="server" Value='<%#Eval("Active" )%>' />
                                                                        <asp:HiddenField ID="hdChainCode" runat="server" Value='<%#Eval("CHAIN_CODE" )%>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="50px" CssClass="ItemColor" HorizontalAlign ="center"  />
                                                                    <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                            <FooterStyle CssClass="Gridheading" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" valign="top">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                    <td style="width: 30%" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                                            TabIndex="3" Width="100px" ReadOnly="True" Text="0" Visible="True"></asp:TextBox></td>
                                                                    <td style="width: 25%" class="right">
                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                    <td style="width: 20%" class="center">
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                            ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 25%" class="left">
                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 44px">
                                                        <asp:HiddenField ID="hdID" runat="server" />
                                                        &nbsp;
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
