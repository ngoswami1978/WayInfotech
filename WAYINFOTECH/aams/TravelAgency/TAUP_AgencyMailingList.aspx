<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyMailingList.aspx.vb" Inherits="TravelAgency_TAUP_AgencyMailingList" %>
<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>     
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script  id="Js" language ="javascript" type="text/javascript">
       function Validate()
       {
            if(document.getElementById("txtEmail").value.trim()=="")
            {     
                    document.getElementById("lblError").innerHTML='Email id is mandatory.';
                    document.getElementById("txtEmail").focus();
                    return false;
            }
             if(document.getElementById("txtEmail").value!='')
            {              
                if(checkEmail(document.getElementById("txtEmail").value)==false)
                {
                    document.getElementById("lblError").innerHTML='Email id is not valid.';
                    document.getElementById("txtEmail").focus();
                    return false;
                }
            } 
       }
       function PopSendMail()
        {
          var ddlMailListTo = document.getElementById('<%=lstEmail.ClientId%>');
	      var count = ddlMailListTo.options.length;
	      if (count==0 )
	      {
	                document.getElementById("lblError").innerHTML='There is no any e-mail for sending.';
                    document.getElementById("lstEmail").focus();
                    return false;
	      }
	      var hdFileno=document.getElementById('<%=hdFileno.ClientId%>').value;
	     // alert(hdFileno);
	         if (hdFileno=="" || hdFileno=="0" ) 
	         {
	              //  document.getElementById("lblError").innerHTML='There is no file number exist for this order.';                                  
                  //  return false;
	         }
	     
          var hdLcode=document.getElementById('<%=hdLcode.ClientId%>').value;
          var hdOrderId=document.getElementById('<%=hdOrderId.ClientId%>').value;
          var type;
          type = "../Popup/PUUP_ProductMailSend.aspx?Lcode=" + hdLcode + "&OrderId="  + hdOrderId ;
             
//          if (window.showModalDialog)
//          {
//              window.showModalDialog(type,null,'dialogWidth:785px;dialogHeight:600px;status:yes;help:no;');       
//          }
//          else
//          {
//              window.open(type,null,'height=600px,width="785px",top=100,left=100,status=1,scrollbars=0');       
//          }	  
             
          window.open(type,"aa","height=600px,width=800px,top=30,left=20,scrollbars=1,status=1");
   	           return false;	  
        
        }
       function SelectEmailGroup()
       {
            var type;
            type = "../TravelAgency/MSSR_MailGroup.aspx?PopUp=P" ;
            window.open(type,"aa","height=600px,width=920px,top=30,left=20,scrollbars=1,status=1");
   	        return false;	  
       }
       
    </script>

  
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMailingList" runat="server"  defaultfocus ="txtEmail">
        <table width="860px" align="left" style="height: 486px;" class="border_rightred">
            <tr>
                <td valign="top" style="height: 516px">
                    <table width="100%" align="left">
                          <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">TravelAgency -&nbsp; </span><span class="sub_menu">
                      Send Mail</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Send Mail&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <uc1:MenuControl ID="MenuControl1" runat="server" />
                                        </td>
                                    </tr>                                   
                                     <tr>
                                      <td valign="top" class="redborder">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                              <tr>
                                                    <td valign="top"  style="width:845px;padding-left:7px;padding-bottom:7px; padding-right:7px;">                                                                                
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                           <tr>
                                                               <td valign="top" >                                                              
                                                                  <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                        <td valign="top" style ="height:5PX"></td>
                                                                       </tr>   
                                                                      <tr>
                                                                         <td valign="middle" style="height: 22px"><asp:Button ID="btnOrder" runat="server" Text="Order Details" CssClass="headingtab" Width="72px" />&nbsp;<asp:Button ID="btnMail" runat="server" Text="Mailing List" CssClass="headingtab" Enabled="True" Width="72px" /></td>
                                                                      </tr>
                                                                  </table> 
                                                                   </td>
                                                            </tr>  
                                                                                                                      
                                                             <tr>
                                                                   <td align="center" class="redborder">                                 
                                                                            <table width="60%" border="0"  cellpadding="0" cellspacing="0" >
                                                                                        <tr> 
                                                                                           <td>
                                                                                               <table> 
                                                                                                           <tr>
                                                                                                                <td  class="textbold" colspan="7" align="center" height="25px" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                                                            </tr>
                                                                                                              <tr>
                                                                                                                <td  class="textbold" colspan="7" align="Right" height="25px" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  class="textbold"></td>                                                               
                                                                                                                <td class="textbold" ></td>
                                                                                                                <td style="width:50px"  class="textbold">E-mail</td>
                                                                                                                <td class="textbold" style="width:50px;"><asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="295px" TabIndex="2"></asp:TextBox></td>
                                                                                                                 <td class="textbold" ></td>
                                                                                                                  <td class="textbold" ></td>
                                                                                                                <td class="textbold" ><asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add " Width="98px" TabIndex="3" /></td>
                                                                                                            </tr>                                                           
                                                                                                             <tr>
                                                                                                                <td  class="textbold" colspan="7" align="Right" valign="TOP" style="height: 15px"><asp:Button ID="BtnEmailGroup" CssClass="button" runat="server" Text="Email Group " Width="98px" TabIndex="3" /></td>
                                                                                                            </tr>
                                                                                                              <tr>
                                                                                                                <td  class="textbold" ></td>                                                              
                                                                                                                <td class="textbold"  ></td>
                                                                                                                 <td class="textbold"   ></td>
                                                                                                                 <td class="textbold"  rowspan ="2">
                                                                                                                 <asp:ListBox ID="lstEmail" runat="server" Height="300px" SelectionMode="Multiple"
                                                                                                                     TabIndex="7" Width="300px" ToolTip="You can select more than on email form list ( By Cltrl / Shift key) and remove it"></asp:ListBox></td>
                                                                                                              <td class="textbold">
                                                                                                                  </td>
                                                                                                              <td class="textbold"  ></td>
                                                                                                              <td class="textbold" valign ="top" > <asp:Button ID="btnRemove_Click" CssClass="button" runat="server" Text="Remove" Width="98px" TabIndex="3" /></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                   <td  colspan ="3"></td><td> </td> <td colspan ="3"  valign ="bottom"> <input  type="button" class="button"  tabindex="8"  value="Send Mail" onclick="javascript:PopSendMail();" style="width:98px" id="btnSendMail" size="" runat="server"  /></td>
                                                                                                                </tr>
                                                                                                                 <tr>
                                                                                                                    <td  class="textbold" colspan="6" align="center" valign="TOP">   </td><td> </td>
                                                                                                                </tr>
                                                                                                                 <tr>                                                              
                                                                                                                    <td class="textbold"   valign ="Top"></td>
                                                                                                                    <td  valign ="Top" class="textbold" colspan ="2" ></td>
                                                                                                                    <td   align="center"colspan ="3" valign="top"></td>    
                                                                                                                 <td valign ="middle"> </td>                                    
                                                                                                                 <td></td>
                                                                                                                </tr>
                                                                                                                 <tr>                                                              
                                                                                                                    <td class="textbold"   valign ="Top" style="height: 22px"></td>
                                                                                                                    <td  valign ="Top" class="textbold" colspan ="2" style="height: 22px" ></td>
                                                                                                                    <td  colspan ="3" style="height:22px;Width:35px;" valign="top"></td>    
                                                                                                                 <td valign ="middle" style="height: 22px"> </td>                                    
                                                                                                                 <td style="height: 22px"></td>
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
                                    </td >
                                    </tr> 
                                </table>
                                <asp:HiddenField ID="hdLcode" runat="server" /><asp:HiddenField ID="hdOrderId" runat="server" />
                                <asp:HiddenField ID="hdFileno" runat="server" Value="" /><asp:HiddenField ID="hdGroupId"   Value ="0" runat="server" />
                                <br />
                                <asp:HiddenField ID="hdViewEmailDetailsByDept" Value ="0" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
