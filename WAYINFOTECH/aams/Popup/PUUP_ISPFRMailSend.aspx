<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUUP_ISPFRMailSend.aspx.vb" Inherits="Popup_PUUP_ISPFRMailSend"  ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Send EMail</title>
    <base target="_self"/>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>     
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script  id="Js" language ="javascript" type="text/javascript">
     
     function MailMandatory()
     {
           if(document.getElementById("txtEmailFrom").value.trim()=="")
            { 
                    document.getElementById("lblError").innerHTML='From email Id is mandatory.';
                    document.getElementById("txtEmailFrom").focus();
                    return false;           
            }
              if(document.getElementById("txtEmailFrom").value!='')
            {              
                if(checkEmail(document.getElementById("txtEmailFrom").value)==false)
                {
                    document.getElementById("lblError").innerHTML='Enter valid email Id.';
                    document.getElementById("txtEmailFrom").focus();
                    return false;
                }
            }  
            if(document.getElementById("txtEmailTo").value.trim()=="")
            { 
                    document.getElementById("lblError").innerHTML='To email Id is mandatory.';
                    document.getElementById("txtEmailTo").focus();
                    return false;           
            } 
            
              if(document.getElementById("txtEmailTo").value!='')
            {      
                var strEmail =document.getElementById("txtEmailTo").value 
                          
                 var EmailArr = strEmail.split(','); 
                
                 var blnWrongEmail=false;
                 
                for (i=0; i<EmailArr.length;i++)
                {
                  
                    if(checkEmail(EmailArr[i])==false)
                    {                      
                        blnWrongEmail= true;
                        break;                       
                    }
                }
                if (blnWrongEmail==true)
                {
                        document.getElementById("lblError").innerHTML='Enter valid email Id by seperating ; ';
                        document.getElementById("txtEmailTo").focus();
                         return false;     
                }
            }  
            
             if(document.getElementById("txtSub").value.trim()=="")
            { 
                    document.getElementById("lblError").innerHTML='Subject is mandatory.';
                    document.getElementById("txtSub").focus();
                    return false;           
            } 
               if(document.getElementById("txtbody").value.trim()=="")
            { 
                    document.getElementById("lblError").innerHTML='Body is mandatory.';
                    document.getElementById("txtbody").focus();
                    return false;           
            } 
     }
     
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table style="width:100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top" style="height: 498px">
                    <table width="600" align="left">
                        <tr>
                            <td valign="top" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Send Mail&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>   
                        <tr>
                            <td valign="top" >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                       <td align="LEFT" class="redborder">                                 
                                                <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="7" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  class="textbold"></td>                                                               
                                                                <td class="textbold" style="width: 75px" >
                                                                    &nbsp;<span class ="textbold"><b>From</b></span></td>
                                                                <td style="width: 451px" ><asp:TextBox ID="txtEmailFrom" runat="server" MaxLength="100" Width="435px" TabIndex="2"></asp:TextBox></td>
                                                                <td class="textbold" style="width:50px;"></td>
                                                                 <td class="textbold" style="width: 4px" ></td>
                                                                  <td class="textbold" style="width: 4px" ></td>
                                                                <td class="textbold" ></td>
                                                            </tr>                                                           
                                                             <tr>
                                                                <td  class="textbold" colspan="7" align="center" valign="TOP" style="height: 15px">   </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" ></td>                                                              
                                                                <td class="textbold" style="width: 75px"  >
                                                                    &nbsp;<b>To</b></td>
                                                                 <td class="textbold" style="width: 451px"   >
                                                                     <asp:TextBox ID="txtEmailTo" runat="server" MaxLength="1000" TabIndex="2" TextMode="MultiLine"
                                                                         Width="436px"></asp:TextBox></td>
                                                                 <td class="textbold" ></td>
                                                                  <td class="textbold" style="width: 4px">
                                                                      &nbsp;</td>
                                                                  <td class="textbold"  ></td>
                                                                  <td class="textbold" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="7" align="center" valign="TOP" style="height: 15px">   </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" ></td>                                                              
                                                                <td class="textbold" style="width: 75px"  >
                                                                    &nbsp;<b>Subject</b></td>
                                                                 <td class="textbold" style="width: 451px"   >
                                                                     <asp:TextBox ID="txtSub" runat="server" MaxLength="200" TabIndex="2"
                                                                         Width="436px"></asp:TextBox></td>
                                                                 <td class="textbold" ></td>
                                                                  <td class="textbold" style="width: 4px">
                                                                      &nbsp;</td>
                                                                  <td class="textbold"  ></td>
                                                                  <td class="textbold" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">   </td>
                                                            </tr>
                                                             <tr>                                                              
                                                                <td class="textbold"   valign ="Top"></td>
                                                                <td  valign ="Top" class="textbold" colspan ="2" >
                                                                    <asp:TextBox ID="txtbody" runat="server" Height="315px" MaxLength="8000" TextMode="MultiLine" Width="608px" EnableViewState="False"></asp:TextBox></td>
                                                                <td  colspan ="2" style="height:4;Width:35px;" valign="top">
                                                                  
                                                            </td>    
                                                             <td  align ="left" valign="bottom">
                                                                 </td>                                    
                                                             <td><asp:Button  CssClass ="button" ID="btnSendMail" runat="server" Text="Send Mail" /></td>
                                                              <td  align ="left" style="width: 4px">&nbsp;&nbsp;</td>   
                                                            </tr>
                                               </table> 
                                      </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
