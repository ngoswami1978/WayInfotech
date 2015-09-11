<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_Feedback.aspx.vb" Inherits="BirdresHelpDesk_HDUP_Feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" /> 
<script type="text/javascript" language="javascript">
function InsertFeedback()
{
window.location.href="HDUP_Feedback.aspx?Action=I|"
}

function  PopupAgencyPage()
{
 type = "../TravelAgency/TASR_BRAgency.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
    return false;
}
function ValidatFeedback()
{
        if(document.getElementById("txtAgencyName").value =='') 
        {
        document.getElementById("txtAgencyName").focus();
        document.getElementById("lblError").innerHTML="Agency Name is Mandatory";
        return false;
        }
        
        if(document.getElementById("txtLogedByName").value =='') 
        {
        document.getElementById("txtLogedByName").focus();
        document.getElementById("lblError").innerHTML="LoggedBy Name is Mandatory";
        return false;
        }
    
    for(intcnt=1;intcnt<=document.getElementById('<%=grdvFeedback.ClientID%>').rows.length-1;intcnt++)
    {        
       if(document.getElementById('<%=grdvFeedback.ClientID%>').rows[intcnt].cells[2].children[0].selectedIndex=='0')
       {
       document.getElementById('<%=grdvFeedback.ClientID%>').rows[intcnt].cells[2].children[0].focus();
        document.getElementById("lblError").innerHTML="Questions Status is Mandatory";
        return false;
       }
   }
   
}

  function PopupLoggedBy()
    {
            var type;      
         var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
        if (strEmployeePageName!="")
        {
           type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
         //   type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
        }
    
    }
</script>  
</head>
<body>
    <form id="form1" runat="server">
       <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Birdres HelpDesk-></span><span class="sub_menu">Feedback</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Feedback
                                        </td>
                                    </tr>
                                </table>                              
                            </td>
                        </tr>                       
                        <tr>
                            <td valign="top" style="height: 660px">
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
                                                    <tr height="20px" >
                                                    <td colspan="5" class="subheading" style="height: 10px"> &nbsp;&nbsp;
                                                       Agency Details
                                                    </td>
                                                    </tr>
                                                    
                                                     <tr height="3px"></tr>
                                                     
                                                        <tr>
                                                                    <td class="textbold" style="width: 86px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 151px">
                                                                        Agency<span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyName" CssClass="textboxgrey" runat="server" Width="528px" ReadOnly="True" TabIndex="1" MaxLength="50"></asp:TextBox>
                                                                            <img src="../Images/lookup.gif" alt="" onclick="javascript:return PopupAgencyPage();"  id="imgAgency" runat="server" />
                                                                        </td>
                                                                    <td style="width: 187px">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="13" CssClass="button" Text="Save" Width="88px" /></td>
                                                                </tr>
                                                              
                                                                <tr height="3px"></tr>
                                                                
                                                              
                                                                <tr>
                                                                    <td class="textbold" style="width: 86px; height: 82px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 151px; height: 82px;" valign="top" >
                                                                        Address</td>
                                                                    <td colspan="3" style="height: 82px" valign="top" >
                                                                    <asp:TextBox ID="txtAgencyAddress" runat="server" CssClass="textboxgrey" TabIndex="2" TextMode="MultiLine"  Height="62px" ReadOnly="True" Rows="5" Width="528px" MaxLength="300"></asp:TextBox>
                                                                     </td>
                                                                    <td style="width: 187px; height: 82px;" valign="top"  >
                                                                    <table cellpadding="0" cellspacing="0" >
                                                                    <tr>
                                                                            <td style="height: 22px"> 
                                                                             <asp:Button ID="btnNew" runat="server" TabIndex="14" CssClass="button" Text="New" Width="88px" />
                                                                               </td>
                                                                        </tr>
                                                                        <tr height="3px"></tr>
                                                                        <tr>
                                                                            <td style="height: 22px"> 
                                                                             <asp:Button ID="btnReset" runat="server" TabIndex="15" CssClass="button" Text="Reset" Width="88px" />
                                                                               </td>
                                                                        </tr>
                                                                    </table>
                                                                    </td>
                                                                </tr>
                                                                          <tr height="3px"></tr>
                                                                <tr valign="top" >
                                                                    <td class="textbold" style="width: 86px">
                                                                    </td>
                                                                    <td style="width: 151px" class="textbold">
                                                                        City</td>
                                                                        <td style="width: 257px">
                                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="40" ReadOnly="True"
                                                                                Width="178px" TabIndex="3"></asp:TextBox></td>
                                                                        <td style="height: 26px; width: 178px;" class="textbold" >
                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                            Country</td>
                                                                        <td style="height: 26px; width: 245px;" >
                                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey"
                                                                                MaxLength="40" Width="161px" ReadOnly="true" TabIndex="4" ></asp:TextBox></td>
                                                                    <td style="width: 187px; height: 26px;">
                                                                    
                                                                    </td>
                                                                  </tr>
                                                            
                                                            
                                                            
                                                              <tr height="3px"></tr>
                                                              
                                                              
                                                                <tr>
                                                                    <td class="textbold" style="width: 86px; height: 23px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 151px; height: 23px;">
                                                                        Phone</td>
                                                                    <td style="width: 257px; height: 23px;">
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" MaxLength="40" Width="178px" ReadOnly="True" TabIndex="5"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 178px; height: 23px;">
                                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                        Office ID
                                                                    </td>
                                                                    <td style="width: 245px; height: 23px;">
                                                                        <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textboxgrey" MaxLength="20"  ReadOnly="true" TabIndex="6" Width="161px"></asp:TextBox></td>
                                                                    <td style="width: 187px; height: 23px;">
                                                                        </td>
                                                                </tr>
                                                                
                                                                
                                                                  <tr height="3px"></tr>
                                                                  
                                                                <tr>
                                                                    <td class="textbold" style="width: 86px; height: 21px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 151px; height: 21px;">
                                                                      Fax</td>
                                                                    <td style="width: 257px; height: 21px;" >
                                                                       <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                                            Width="178px" ReadOnly="True" TabIndex="7"></asp:TextBox></td>
                                                                    <td style="width: 178px; height: 21px;" class="textbold">
                                                                        </td>
                                                                         <td style="height: 21px; width: 245px;">
                                                                             </td>
                                                                           <td style="height: 21px; width: 187px;" >
                                                                        </td>  
                                                                </tr>
                                                                
                                                                  <tr height="15px"></tr>
                                                                  
                                                               
                                                                
                                                    <tr height="20px" >
                                                    <td colspan="5" class="subheading" style="height: 10px"> &nbsp; &nbsp;Question Details
                                                    </td>
                                                    </tr>
                                                    <tr height="3px"></tr>
                                                    <tr>
                                                    
                                                    <td colspan="5" style="height: 161px">
                                                     <asp:GridView ID="grdvFeedback" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="99%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" TabIndex="8">
                                                            <Columns>
                                                            
                                                            <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                            <asp:Label  ID="lblQuestionID" runat="server" Text='<%# Eval("QUESTION_ID") %>' />
                                                            </ItemTemplate>
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Questions" >
                                                            <ItemTemplate>
                                                            <asp:Label ID="lblQuest" runat="server" Text='<%# Eval("QUESTION_TITLE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                                <ItemStyle Width="600px" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                            <asp:DropDownList ID="drpQuestStatus" runat="server" ></asp:DropDownList>
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            
                                                            
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                   </asp:GridView>
                                                        &nbsp;</td>
                                                    <td style="width: 187px; height: 161px"></td>
                                                    
                                                    </tr>              
                                                               
                                                <tr height="20px" >
                                                    <td colspan="5" class="subheading" style="height: 10px"> &nbsp; &nbsp;Other Details
                                                    </td>
                                                    </tr>
                                                                  <tr height="3px"></tr>
                                                    
                                                                  <tr>
                                                                    <td class="textbold" style="width: 86px; height: 21px;" >
                                                                    </td>
                                                                    <td class="textbold" style="width: 151px; height: 21px;">
                                                                        Feedback No.</td>
                                                                    <td style="width: 257px; height: 21px;">
                                                                     <asp:TextBox ID="txtFeedBackNo" runat="server" CssClass="textboxgrey" MaxLength="40" Width="178px" TabIndex="9" ReadOnly="True"></asp:TextBox></td>
                                                                    <td class="textbold" style="height: 21px; width: 178px;">
                                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                        Date</td>
                                                                    <td style="height: 21px; width: 245px;">
                                                                        &nbsp;<asp:TextBox ID="txtFeedbkDt" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                                            TabIndex="10" Width="161px" ReadOnly="True"></asp:TextBox></td>
                                                                    <td style="height: 21px; width: 187px;">
                                                                        <asp:HiddenField ID="hdEmpID" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr height="3px"></tr>
                                                                  <tr>
                                                                    <td class="textbold" style="width: 86px; height: 21px;" >
                                                                    </td>
                                                                    <td class="textbold" style="width: 151px; height: 21px;">
                                                                        Logged By <span class="Mandatory">*</span></td>
                                                                    <td style="width: 257px; height: 21px;">
                                                                        <asp:TextBox ID="txtLogedByName" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                                            TabIndex="11" Width="178px" ReadOnly="True"></asp:TextBox>
                                                                            <img alt=" " onclick="javascript:return PopupLoggedBy();" id="imgLogged" runat="server"   src="../Images/lookup.gif" tabindex="9" /></td>
                                                                    <td class="textbold" style="height: 21px; width: 178px;">
                                                                       </td>
                                                                    <td style="height: 21px; width: 245px;">
                                                                        &nbsp;</td>
                                                                    <td style="height: 21px; width: 187px;">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                
                                                                <tr height="3px"></tr>
                                                                
                                                                  
                                                                  <tr>
                                                                    <td class="textbold" style="width: 86px; height: 21px;" >
                                                                    </td>
                                                                    <td class="textbold" style="height: 21px; width: 151px;" valign="top" >
                                                                     Remarks
                                                                    </td>
                                                                    <td colspan="4" class="textbold" style="height: 21px">
                                                                     <asp:TextBox ID="txtRemarks" TextMode="MultiLine"  runat="server" CssClass="textbox" Height="62px" Width="74%" TabIndex="12" MaxLength="300"></asp:TextBox>                                                                   
                                                                     </td>
                                                                    
                                                                    <td style="height: 21px">
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr height="3px"></tr>
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="height: 11px; width: 86px;">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg" style="height: 11px" valign="bottom">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td style="height: 11px; width: 187px;">
                                                                        <asp:HiddenField ID="hdAddress" runat="server" />
                                                                        </td>
                                                                </tr>
                                                            </table>
                                            <asp:HiddenField ID="hdLcode" runat="server" />
                                            <asp:HiddenField ID="hdLogedByName" runat="server" />
                                            
<input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                        
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
