<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TASR_AgencyTarget.aspx.vb"
    Inherits="TravelAgency_MSSR_AgencyTarget" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Productivity</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js">
    </script>

    <script type="text/javascript" language="javascript">
    function validateResult(id)
{


       if(document.getElementById(id).value =='')
        {
            
        }
        else
        {
        var strValue = document.getElementById(id).value;
        
            reg = new RegExp("^[0-9.]+$"); 
            if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Only Number allowed.';
                 document.getElementById(id).focus();
                return false;
             }
            
        
        }
        
}
function ValidateRegisterPage()
{
try
{

 var dotcount=0;
     for(intcnt=1;intcnt<=document.getElementById('grdAgencyTarget').rows.length-1;intcnt++)
    {   
//     if (document.getElementById('grdAgencyTarget').rows[intcnt].cells[4].children.length == "1")
//    {    
            if (document.getElementById('grdAgencyTarget').rows[intcnt].cells[4].children[0] !=null )
            {
                      if (document.getElementById('grdAgencyTarget').rows[intcnt].cells[4].children[0].type=="text")
                { 
                var strValue = document.getElementById('grdAgencyTarget').rows[intcnt].cells[4].children[0].value.trim();
	            if (strValue != '')
                      {
                        reg = new RegExp("^[0-9.]+$"); 
                        if(reg.test(strValue) == false) 
                        {
                          document.getElementById('lblError').innerText ='Only Number allowed.';       
                          return false;
                         }
                      }
                        for (var i=0; i < strValue.length; i++) 
	                    {
		                    if (strValue.charAt(i)=='.')
		                     { 
		                     dotcount=dotcount+1;
		                      }
		               }
                        if(dotcount>1)
                        {
                            document.getElementById('<%=grdAgencyTarget.ClientID%>').rows[intcnt].cells[4].children[0].focus();
                            document.getElementById("lblError").innerHTML="Target Invalid";
                            return false;
                        }
                        
                 dotcount=0; 
                 }
            
            }
              
     
    //}
  }
  }catch(err){}

}
 var st;
   function fillCategoryName(s)
   {
      document.getElementById('<%=drpSalesPerson.ClientId%>').options.length=0;
       
      document.getElementById('<%=drpSalesPerson.ClientId%>').options[0]=new Option("Loading...","0");  
      id=document.getElementById('<%=drpCity.ClientId%>').value;
      CallServer(id,"This is context from client");
      return false;
   }
     function MandatoryFunction()
    {
     if(document.getElementById("drpCity").value=='' &&   document.getElementById("drpSalesPerson").value=='')
        {     
           if(document.getElementById("drpSalesPerson").value=='')
          {
          document.getElementById("drpSalesPerson").focus();
          }
            if(document.getElementById("drpCity").value=='')
          {
          document.getElementById("drpCity").focus();
          }   
        document.getElementById("lblError").innerHTML ="Either city or sales person is  mandatory";
        return false;
        }
    }
   
    function ReceiveServerData(args, context)
    {        
    
            var obj = new ActiveXObject("MsXml2.DOMDocument");
         	var codes='';
//			var names="-- Select One --";
            var names="-- All --";
			var drpSalesPerson = document.getElementById('<%=drpSalesPerson.ClientId%>');
			//ddlCategoryName.disabled=false;
			if (args=="") 
            {
             listItem = new Option(names, codes );
             drpSalesPerson.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(args);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   			
			    var orders = dsRoot.getElementsByTagName('TARGET');
			    var text;     			
			    var listItem;
			    listItem = new Option(names, codes);
			    drpSalesPerson.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
				    codes= orders[count].getAttribute("SalesManId"); 
			        names=orders[count].getAttribute("SalesManName"); 
				    listItem = new Option(names, codes);
				    drpSalesPerson.options[drpSalesPerson.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    drpSalesPerson.options[0] = listItem;
			    }
			}
			
    }

//  function change()
//  {
//   var objChkNewFormat=document.getElementById("btnIncrease")
//    var objdivlbl2 =document.getElementById(' rdSummaryOption');
//if objdivlbl2.options(0).selected=true
//{

//}
//else
//{
//}
//  }
function radionewexisting() 
{   
 
    var rdoIncrease = document.getElementById('<%=rdSummaryOption1.ClientID%>'); 
    var rdoDecrease = document.getElementById('<%=rdSummaryOption2.ClientID%>'); 
   var text= document.getElementById('<%=btnIncrease.ClientID%>'); 
    if (rdoIncrease.checked)     
    { 
    document.getElementById('<%=btnIncrease.ClientID%>').value="Increase";
    return;
    }
    else
    {
     document.getElementById('<%=btnIncrease.ClientID%>').value="Decrease";
     return;
    }
   
   }
   
    function disableall()
  {
      document.getElementById("drpPreviousYear").disabled = true;
      document.getElementById("drpPreviousMonth").disabled = true;
      document.getElementById("btn_Select").disabled = true;
  }
 function Enableall()
 {
   var objChkNewFormat =document.getElementById('chbPrevious');
  var objdivlbl1 =document.getElementById('DivMonth');
  var objdivlbl2=document.getElementById('DivYear');
  if (objChkNewFormat.checked==true) 
  {
      document.getElementById("drpPreviousYear").disabled = false;
      document.getElementById("drpPreviousMonth").disabled = false;
       document.getElementById("btn_Select").disabled = false;
      }
      else
      {
      document.getElementById("drpPreviousYear").disabled = true;
      document.getElementById("drpPreviousMonth").disabled = true;
       document.getElementById("btn_Select").disabled = true;
       }
  }
 function HistoryFunction(Lcode,SalesPersonId)
 {
     var objChkNewFormat =document.getElementById('chbPrevious');
     var Month;
     var Year
     Month=document.getElementById("drpMonth").value.trim();
     Year=document.getElementById("drpYear").value.trim();
//  if (objChkNewFormat.checked==true) 
//  {
//     Month=document.getElementById("drpPreviousMonth").value.trim();
//     Year=document.getElementById("drpPreviousYear").value.trim();
//  }
//  else
//  {
//     Month=document.getElementById("drpMonth").value.trim();
//     Year=document.getElementById("drpYear").value.trim();
//  }
   
   var parameter="Lcode=" + Lcode  + "&Year=" + Year + "&Month=" + Month +  "&SalesPersonId=" + SalesPersonId;
   type = "../Popup/PUSR_AgencyTarget.aspx?" + parameter;
   window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   return false;
 }
 function CheckValidation()
 {

  var dotcount=0;
   if(document.getElementById('<%=txtTarget.ClientID%>').value.trim()!="")
     {
    if(IsDataValid(document.getElementById("txtTarget").value,5)==false)
     {
      document.getElementById('lblError').innerHTML='Percentage is not valid.';             
      document.getElementById("txtTarget").focus();
      return false;
     }    
      var strValue = document.getElementById('<%=txtTarget.ClientID%>').value.trim();
	if (strValue != '')
           
            for (var i=0; i < strValue.length; i++) 
	        {
		        if (strValue.charAt(i)=='.')
		         { 
		         dotcount=dotcount+1;
		          }
		   }
            if(dotcount>1)
            {
                document.getElementById('<%=txtTarget.ClientID%>').focus();
                document.getElementById("lblError").innerHTML="Target Invalid";
                return false;
            }
            
     dotcount=0;               
    }
 }
 
    </script>

</head>
<body onload="Enableall();"  >
    <form id="form1" runat="server">
        <table width="800px" align="left" class="border_rightred" id="TABLE1" language="javascript">
            <tr>
                <td valign="top" style="width: 800px;">
                    <table align="left" style="width: 102%">
                        <tr>
                            <td valign="top" align="left" style="width: 863px; height: 20px;">
                                <span class="menu">Agency-></span><span class="sub_menu">Agency Target</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px; width: 863px;">
                                Search Agency Target</td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 863px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" style="width: 864px; height: 209px;">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 800px;" class="redborder" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                    &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 59px; height: 27px">
                                                                </td>
                                                                <td class="textbold" style="width: 89px; height: 27px;">
                                                                    City<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana"></span></strong></td>
                                                                <td style="width: 307px; height: 27px;">
                                                                    <asp:DropDownList ID="drpCity" runat="server" TabIndex="1" onkeyup="gotop(this.id)"
                                                                        CssClass="dropdownlist" Width="184px" Style="position: relative" AutoPostBack="True">
                                                                    </asp:DropDownList>&nbsp; </td>
                                                                <td class="textbold" style="height: 27px; width: 118px;">
                                                                    Sales Person</td>
                                                                <td style="width: 223px; height: 27px;">
                                                                    <asp:DropDownList ID="drpSalesPerson" runat="server" TabIndex="2" onkeyup="gotop(this.id)"
                                                                        CssClass="dropdownlist" Width="184px" Style="position: relative">
                                                                         <asp:ListItem Text ="---All---" Selected="True" Value =""></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 100px; height: 27px;">
                                                                    <asp:Button ID="btnSearch" TabIndex="12" CssClass="button" runat="server" Text="Search" AccessKey ="A" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 27px; width: 59px;">
                                                                </td>
                                                                <td class="textbold" style="width: 89px; height: 27px">
                                                                    Year</td>
                                                                <td style="width: 307px; height: 27px">
                                                                    <asp:DropDownList ID="drpYear" TabIndex="3" runat="server" CssClass="dropdownlist"
                                                                        Width="184px" Style="position: relative">
                                                                    </asp:DropDownList>&nbsp;
                                                                </td>
                                                                <td class="textbold" style="height: 27px; width: 118px;">
                                                                    Month</td>
                                                                <td style="height: 27px; width: 223px;">
                                                                    <asp:DropDownList ID="drpMonth" runat="server" TabIndex="4" CssClass="dropdownlist"
                                                                        Width="184px" Style="position: relative; left: 0px; top: 0px;">
                                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">september</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 27px; width: 100px;">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Style="position: relative"
                                                                        TabIndex="13" Text="Save" OnClientClick="return ValidateRegisterPage()"   AccessKey ="S"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 27px; width: 59px;">
                                                                </td>
                                                                <td class="textbold" colspan="2" style="height: 27px">
                                                                    Select Targets from previous months<asp:CheckBox ID="chbPrevious" runat="server"
                                                                        CssClass="textbold" Style="left: 8px; position: relative; top: 0px" TabIndex="5"
                                                                        TextAlign="Left" Width="120px" AutoPostBack="True" />
                                                                        <%--OnCheckedChanged="chbPrevious_CheckedChanged" />--%>
                                                                </td>
                                                                <td class="textbold" style="height: 27px; width: 118px;">
                                                                    &nbsp;</td>
                                                                <td style="width: 223px; height: 27px">
                                                                </td>
                                                                <td style="height: 27px; width: 100px;">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Style="position: relative"  AccessKey ="R"
                                                                        TabIndex="14" Text="Reset" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 27px; width: 59px;">
                                                                </td>
                                                                <td id="Year1" class="textbold" style="height: 27px; width: 89px;">
                                                                    Year</td>
                                                                <td style="height: 27px; width: 307px;">
                                                                    <div id="DivYear" runat="server">
                                                                        <asp:DropDownList ID="drpPreviousYear" TabIndex="6" runat="server" CssClass="dropdownlist"
                                                                            Width="184px" Style="position: relative">
                                                                        </asp:DropDownList>
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="btn_Select" TabIndex="8" CssClass="button" runat="server" Text="Select"
                                                                        Style="left: 0px; position: relative; top: 0px" Width="56px" /></div>
                                                                </td>
                                                                <td class="textbold" rowspan="2" style="width: 118px">
                                                                
                                                                 <%--    <asp:RadioButtonList RepeatDirection="Vertical" ID="rdSummaryOption" runat="server"
                                                                        CssClass="textbold" Width="128px" Style="position: relative" Height="48px"  TabIndex="9" AutoPostBack="True">
                                                                        <asp:ListItem Value="1" Selected="True">Increase Target</asp:ListItem>
                                                                        <asp:ListItem Value="2">Decrease Target</asp:ListItem>
                                                                    </asp:RadioButtonList> --%>
                                                                    <asp:RadioButton ID="rdSummaryOption1" runat="server" Checked="True"  
                                                            GroupName="Type" Text="Increase Target" Width="112px"  CssClass="textbold"/>
                                                        <asp:RadioButton ID="rdSummaryOption2" runat="server"   GroupName="Type"
                                                            Text="Decrease Target" Width="112px" CssClass="textbold" /> 
                                                                    </td>
                                                                <td style="width: 223px;" rowspan="2">
                                                                    <span class="textbold">&nbsp; &nbsp; &nbsp;&nbsp; By(%)</span>
                                                                    <asp:TextBox ID="txtTarget" runat="server" CssClass="textbox" MaxLength="4" Style="left: 0px;
                                                                        position: relative; top: 0px" TabIndex="10" Width="56px" Wrap="False" EnableViewState="False">0</asp:TextBox>
                                                                    <asp:Button ID="btnIncrease" TabIndex="11" CssClass="button" runat="server" Text="Increase" OnClientClick="return ValidateRegisterPage()" 
                                                                        Style="left: 0px; position: relative; top: 0px" Width="71px" /></td>
                                                                <td style="height: 27px; width: 100px;">
                                                                    <asp:Button ID="btnDisplay" runat="server" CssClass="button" 
                                                                        Style="position: relative" TabIndex="15" Text="Display"   AccessKey ="D"/></td>
                                                                <td style="height: 27px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 27px; width: 59px;">
                                                                </td>
                                                                <td id="Month1" class="textbold" style="width: 89px; height: 27px">
                                                                    Month</td>
                                                                <td style="width: 307px; height: 27px">
                                                                    <div id="DivMonth" runat="server">
                                                                        <asp:DropDownList ID="drpPreviousMonth" runat="server" TabIndex="7" CssClass="dropdownlist"
                                                                            Width="184px" Style="position: relative; left: 0px; top: 0px;">
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList></div>
                                                                </td>
                                                                <td style="height: 27px; width: 100px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 59px; height: 27px">
                                                                </td>
                                                                <td class="textbold" style="width: 89px; height: 27px">
                                                                </td>
                                                                <td style="width: 307px; height: 27px" class="ErrorMsg">
                                                                 Field Marked * are Mandatory
                                                                </td>
                                                                <td class="textbold" rowspan="1" style="width: 118px">
                                                                </td>
                                                                <td rowspan="1" style="width: 223px">
                                                                </td>
                                                                <td style="width: 100px; height: 27px">
                                                                </td>
                                                            </tr>
                                                            <%-- <asp:Panel ID="pnlCount" runat="server" Visible ="false">  
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    <strong>
                                                                    Number of Records Found</strong></td>
                                                                <td style="width: 249px; height: 25px">
                                                                    <asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                                        ReadOnly="True" Style="position: relative" TabIndex="18" Text="0" Width="176px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                </td>
                                                                <td style="width: 223px; height: 25px">
                                                                </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            </asp:panel> --%>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 864px">
                                        </td>
                                    </tr>
                                </table>
                                <input id="hdSales" runat="server" style="width: 1px" type="hidden" />
                                <input id="hdSearch" runat="server" style="width: 1px" type="hidden" />
                                  <asp:HiddenField ID="hdTotal" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" class="top border_rightred" style="width: 883px">
                    <table width="900px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder">
                                <%-- <asp:GridView TabIndex="20" ID="grdMIDT" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AlternatingRowStyle-CssClass="lightblue" ShowFooter="true">--%>
                                <asp:GridView TabIndex="20" ID="grdAgencyTarget" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" ShowFooter="false" HeaderStyle-CssClass="Gridheading"
                                    RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" RowStyle-VerticalAlign="top" 
                                    HeaderStyle-ForeColor="white" AllowPaging="True" PageSize="25" AllowSorting="True">
                                    <Columns>
                                        <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" SortExpression="AgencyName">
                                            <ItemStyle Wrap="True" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OfficeId" HeaderText="Office ID" SortExpression="OfficeId">
                                            <ItemStyle Wrap="False" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address">
                                            <ItemStyle  Wrap="True" Width="25%" />
                                            <HeaderStyle Wrap="True" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="SalesManName" HeaderText="Sales Executive" SortExpression="SalesManName">
                                            <ItemStyle Wrap="False" Width="20%" />
                                        </asp:BoundField> 
                                        <asp:TemplateField HeaderText="Target  " SortExpression="TARGET"  ItemStyle-HorizontalAlign="Right" >
                                            <ItemTemplate >
                                                <asp:TextBox  ID="txtTarget" Text='<% #DataBinder.Eval(Container.DataItem, "TARGET") %>'
                                              Width ="82%"       runat="server" CssClass="textbox right" MaxLength="6"   Wrap ="false"   ></asp:TextBox>
                                                <asp:HiddenField ID="hdlcode" runat="server" Value='<% #Container.DataItem("LCode") %>' /> 
                                                 <asp:HiddenField ID="hdSalesId" runat="server" Value='<% #Container.DataItem("SalesPersonId") %>' />
                                                  <asp:HiddenField ID="hdResp_1A" runat="server" Value='<% #Container.DataItem("LoginId") %>' />   
                                                   <asp:HiddenField ID="hdMonth" runat="server"  Value='<% #Container.DataItem("Month") %>' />
                                                  <asp:HiddenField ID="hdYear" runat="server"  Value='<% #Container.DataItem("Year") %>' />                                                  
                                              <%-- <HeaderStyle wrap="False" />--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="right"  VerticalAlign ="top"   Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" >
                                            <ItemTemplate><asp:LinkButton ID="lnkHistory" runat="server" CommandName="HistoryX" Text="History"
                                                    CssClass="LinkButtons" Width="10%"></asp:LinkButton>                                                                                        
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White" />
                                    <RowStyle CssClass="textbold" />
                                    <FooterStyle CssClass="Gridheading" />
                                      <PagerTemplate></PagerTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td colspan="6" valign="top">
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                            <td style="width: 22%" class="left">
                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;</td>
                                            <td style="width: 26%" class="right">
                                                <strong><span  style="font-size: 9pt; font-family: Arial"><asp:TextBox
                                                    ID="txtTotalRecordCount" runat="server" Width="88px" CssClass="textboxgrey" style="left: -78px; position: relative; top: 0px"></asp:TextBox>Total Target&nbsp;</span></strong>
                                               </td>
                                            <td style="width: 23%" class="center">
                                               
                                               <asp:TextBox
                                                    ID="txtTotalTarget" runat="server" Width="105px" CssClass="textboxgrey" style="position: relative"></asp:TextBox></td>
                                            <td style="width: 25%" class="left">
                                                </td>
                                        </tr>
                                        <tr style="padding-bottom: 4pt; padding-top: 4pt">
                                            <td class="left" style="width: 22%">
                                             <span class="textbold"><b>&nbsp;</b></span>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp;
                                            </td>
                                            <td class="right" style="width: 26%">
                                            </td>
                                            <td class="center" style="width: 23%">
                                            </td>
                                            <td class="left" style="width: 25%">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                                    Visible="false"></asp:TextBox>
                            </td>
                        </tr>--%>
                            <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 28%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"  ></asp:TextBox></td>
                                                                          <td style="width: 33%" class="right"><span  class="textbold"><b>&nbsp;Total Target</b></span>&nbsp;&nbsp;
                                                                                          <asp:TextBox
                                                    ID="txtTotalTarget" runat="server" Width="112px" CssClass="textboxgrey" style="position: relative; left: -5px;"></asp:TextBox>                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr> 
                    </table>
                   
                </td>
            </tr>
            <tr>
               <td> <input id="hdTargetList" runat="server" enableviewstate="true" style="width: 1px"
                        type="hidden" /></td>
            </tr>
            
        </table>
    </form>
</body>
</html>
