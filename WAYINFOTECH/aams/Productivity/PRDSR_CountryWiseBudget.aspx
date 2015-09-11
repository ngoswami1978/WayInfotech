<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRDSR_CountryWiseBudget.aspx.vb" Inherits="Productivity_PRDSR_CountryWiseBudget" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Country Wise Budget</title>
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<script type="text/javascript" language="javascript">
function chkSearch()
{
    if(document.getElementById("drpCountry").selectedIndex=='0')
    {
    document.getElementById("lblError").innerHTML="Select a country";
    return false;
    }

}


function ChangeControlStatus(id,mon)
    {
    var d =new Date();
    var curr_year=d.getFullYear();
    var curr_month=d.getMonth();
     var Year=document.getElementById("drpYear").value.trim();
     if (Year<curr_year)
     {
       document.getElementById(id).readOnly=true;
        document.getElementById(id).className='textboxgrey';
            document.getElementById("btnSave").focus();
        
     }
     else
     {
        document.getElementById(id).readOnly=false;
        document.getElementById(id).className='textbox';
        document.getElementById(id).focus();
        if (Year==curr_year && mon<curr_month+1)
        {
            document.getElementById(id).readOnly=true;
            document.getElementById(id).className='textboxgrey';
            document.getElementById("btnSave").focus();
        }
     }
   
        return false;
    }
    
    
function ShowHistory()
{
   
  
    if(document.getElementById("drpCountry").selectedIndex=='0')
    {
    document.getElementById("lblError").innerHTML="Select a country";
    return false;
    }
    
var hdC=document.getElementById("drpCountry").options[document.getElementById("drpCountry").selectedIndex].value;
var hdY=document.getElementById("drpYear").options[document.getElementById("drpYear").selectedIndex].text;

   var type="../Popup/PUSR_CountryBudgetHistory.aspx?COUNTRYIDS="+hdC+"&YEAR="+hdY
      
      if (window.showModalDialog) 
        {
        window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
        }
         else
            {  
            window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');     
            }
            return false;

}

function validateTarget()
{

var counter=0;
var dotcount=0;
for(intcnt=1;intcnt<=document.getElementById('<%=grdvCountryWiseBudget.ClientID%>').rows.length-1;intcnt++)
    {    
    if(IsDataValid(document.getElementById('<%=grdvCountryWiseBudget.ClientID%>').rows[intcnt].cells[2].children[0].value,3)==false)
       {
       document.getElementById('<%=grdvCountryWiseBudget.ClientID%>').rows[intcnt].cells[2].children[0].focus();
        document.getElementById("lblError").innerHTML="Target is Numeric";
        return false;
       }
       else
       {
                   if(document.getElementById('<%=grdvCountryWiseBudget.ClientID%>').rows[intcnt].cells[2].children[0].value!='')
                   {
                   counter=counter+1;
                   }
                   
                   
                   
    }
     
      var targetval=document.getElementById('<%=grdvCountryWiseBudget.ClientID%>').rows[intcnt].cells[2].children[0].value;
        
        for (var i=0; i < targetval.length; i++) 
	        {
		        if (targetval.charAt(i)=='.')
		         { 
		         dotcount=dotcount+1;
		          }
		   }
            if(dotcount>1)
            {
            document.getElementById('<%=grdvCountryWiseBudget.ClientID%>').rows[intcnt].cells[2].children[0].focus();
                document.getElementById("lblError").innerHTML="Target Invalid";
                return false;
            }
            
     dotcount=0; 
   }
   
   if(document.getElementById("drpCountry").selectedIndex=='0')
    {
    document.getElementById("lblError").innerHTML="Select a country";
    return false;
    }
    else
    {
     
        var yval=document.getElementById("drpYear").options[document.getElementById("drpYear").selectedIndex].text;
         var d =new Date();
        var curr_year=d.getFullYear();
        
        if(yval<curr_year)
        {
        document.getElementById("lblError").innerHTML="Target Cannot be set for Previous Years";
        return false;
        }
    }
    
    if(counter=='0')
    {
    document.getElementById("lblError").innerHTML="Set At least one Month Target ";
        return false;
    }
    
    
    
}
</script>
</head>
<body >
    <form id="frmCountryWiseBudget" runat="server">
       <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left" class="textbold">
                                            <span class="menu">Productivity-&gt;</span>Country Wise Target</td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            &nbsp;Countrywise Budget</td>
                                    </tr>
                                </table>                              
                            </td>
                        </tr>                       
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                         
                                      <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">                                              
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" colspan="7">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                                <tr height="3px"></tr>
                                                
                                                <tr>
                                                    
                                                     <tr height="3px"></tr>
                                                              
                                                                <tr height="3px"></tr>
                                                                          <tr height="3px"></tr>
                                                                <tr valign="top" >
                                                                    <td class="textbold" style="width: 64px; height: 17px;">
                                                                    </td>
                                                                    <td style="width: 151px; height: 17px;" class="textbold">
                                                                        Country<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 174px; height: 17px;">
                                                                            <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                                Width="137px">
                                                                            </asp:DropDownList></td>
                                                                        <td style="height: 17px; width: 114px;" class="textbold" >
                                                                            Year</td>
                                                                        <td style="height: 17px; width: 184px;" >
                                                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                                Width="104px">
                                                                            </asp:DropDownList></td>
                                                                    <td style="width: 187px; height: 17px;">
                                                                        <asp:Button ID="btnSearch" runat="server" TabIndex="2" CssClass="button" Text="Search" Width="88px" AccessKey="A" /></td>
                                                                  </tr>
                                                            
                                                            
                                                            
                                                              <tr height="3px"></tr>
                                                              
                                                              
                                                                <tr>
                                                                    <td colspan="5" style="height: 11px">
                                                                    </td>
                                                                    <td style="width: 187px; height: 11px;"><asp:Button ID="btnSave" runat="server" TabIndex="2" CssClass="button" Text="Save" Width="88px" AccessKey="S" /></td>
                                                                </tr>
                                                                
                                                                
                                                                  <tr height="3px"></tr>
                                                                  
                                                                <tr>
                                                                   <td colspan="5" class="subheading" style="height: 10px"> &nbsp; &nbsp;Country Wise Budget
                                                    </td>
                                                                           <td style="height: 21px; width: 187px;" ><asp:Button ID="btnHistory" runat="server" TabIndex="2" CssClass="button" Text="History" Width="88px" /></td>  
                                                                </tr>
                                                                
                                                                 
                                                                  
                                                               
                                                                
                                                    <tr>
                                                    <td colspan="5">
                                                     <asp:GridView ID="grdvCountryWiseBudget" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="61%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" TabIndex="1">
                                                            <Columns>
                                                            
                                                            <asp:TemplateField HeaderText="Month">
                                                            <ItemTemplate>
                                                            <asp:Label  ID="lblMonthName" runat="server" Text='<%# MonthName(Eval("MONTH")) %>' />
                                                            <asp:HiddenField ID="hdMonthID" runat="server" Value='<%# Eval("MONTH") %>' />
                                                            <asp:HiddenField ID="hdCountryID" runat="server" Value='<%# Eval("COUNTRYID") %>' />
                                                            <asp:HiddenField ID="hdUserID" runat="server" Value='<%# Eval("USERID") %>' />
                                                            </ItemTemplate>
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Year" >
                                                            <ItemTemplate>
                                                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                                <ItemStyle Width="600px" HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Target">
                                                            <ItemTemplate>
                                                            <asp:TextBox ID="txtTarget" MaxLength="9" ReadOnly="true"  CssClass="textboxgrey" runat="server" Text='<%# Eval("TARGET") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="Action" >
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" CommandArgument='<%# Eval("MONTH") %>' Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>
                                                           </ItemTemplate>
                                                                                        <ItemStyle Width="7%" HorizontalAlign="Center" Wrap="False" />
                                                           </asp:TemplateField> 
                                                           
                                                            
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                   </asp:GridView>
                              
                                   <asp:HiddenField ID="hdCountry" runat="server" />
                                   <asp:HiddenField ID="hdYear" runat="server" />
                                   
                                                      </td>
                                                    <td style="width: 187px; padding-top:3px;" valign="top" >
                                                                             <asp:Button ID="btnReset" runat="server" TabIndex="2" CssClass="button" Text="Reset" Width="88px"  AccessKey="R"/></td>
                                                    
                                                    </tr>              
                                                                  <tr height="3px"></tr>
                                                                
                                                                <tr height="3px"></tr>
                                                                
                                                                <tr height="3px"></tr>
                                                                
                                                                <tr height="3px"></tr>
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="height: 11px; width: 64px;">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg" style="height: 11px" valign="bottom">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td style="height: 11px; width: 187px;">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                            &nbsp;
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                &nbsp; &nbsp; &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table> 
                </td>
              
            </tr>
        </table>
    </form>
</body>
</html>
