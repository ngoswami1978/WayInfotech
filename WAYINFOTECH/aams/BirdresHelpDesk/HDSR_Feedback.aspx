<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_Feedback.aspx.vb" Inherits="BirdresHelpDesk_HDSR_Feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
  <script type="text/javascript" src="../Calender/calendar.js"></script>
  <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
  <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
  <link rel="stylesheet" href="../CSS/AAMS.css" type="text/css" />
  <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
  <script type="text/javascript" language="javascript">
  function PopupAgencyPage()
    {
    type = "../TravelAgency/TASR_BRAgency.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;
    } 
  
     function EditFunction(CheckBoxObj)
   {           
          window.location.href="HDUP_Feedback.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
    
    
//    function DeleteFunction(FeedbackID)
//    {   
//        if (confirm("Are you sure you want to delete?")==true)
//        {   
//        window.location.href="HDSR_Feedback.aspx?Action=D|"+ FeedbackID;                   
//          return false;
//        }
//    }
    
        function DeleteFunction(FeedbackID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteFlag").value=FeedbackID;
           return true;
           }
           else
           {
                document.getElementById("hdDeleteFlag").value="";
                 return false;
           }
        }
    
     function PopupLoggedBy()
    {
    var type;      
        
         var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
        if (strEmployeePageName!="")
        {
           type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
        //    type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
    
    }
    
    function ValidateDates()
    {
    
        if ((document.getElementById("txtFeedBackFromDt").value !='') && (document.getElementById("txtFeedbackDtTo").value !='')) 
        {
        if (isDate(document.getElementById('<%=txtFeedBackFromDt.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Invalid Date-From Format";			
	       document.getElementById('<%=txtFeedBackFromDt.ClientId%>').focus();
	       return(false);  
        }
        if (isDate(document.getElementById('<%=txtFeedbackDtTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Invalid Date-To Format";			
	       document.getElementById('<%=txtFeedbackDtTo.ClientId%>').focus();
	       return(false);  
        }
        if (compareDates(document.getElementById('<%=txtFeedBackFromDt.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtFeedbackDtTo.ClientId%>').value,"d/M/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date To is Less than Date From.";			
	       document.getElementById('<%=txtFeedbackDtTo.ClientId%>').focus();
	       return(false);  
        }        
        }
        else if ((document.getElementById("txtFeedBackFromDt").value =='') && (document.getElementById("txtFeedbackDtTo").value ==''))
        {
        }
        else if(document.getElementById("txtFeedBackFromDt").value =='')
        {
          document.getElementById("txtFeedBackFromDt").focus();
        document.getElementById("lblError").innerHTML="From Date Cannot be blank";
        return false;   
        }
        else if(document.getElementById("txtFeedbackDtTo").value =='')
        {
          document.getElementById("txtFeedbackDtTo").focus();
        document.getElementById("lblError").innerHTML="To Date Cannot be blank";
        return false;   
        }
    
    }
    
  </script>
  
</head>
<body>
    <form id="form1" runat="server" defaultbutton ="btnSearch">
     <table style="width:845px" align="left" class="border_rightred" >
            <tr>
                <td valign="top"  style="width:860px;">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" ><span class="menu">Birdres HelpDesk-></span><span class="sub_menu">Feedback</span></td>
                        </tr>
                        <tr>
                            <td  class="heading" align="center" valign="top" >Search Feedback </td>
                        </tr>
                        <tr>
                            <td valign="top" align="LEFT">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  align ="left" class="redborder" style="width:860px" >                                 
                                                        <table  style="width:100%" border="0"  cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="9" align="center" style="height:25px;" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td  style="width:12%;" class="textbold" ><span class="textbold">Agency</span></td>
                                                                <td colspan="5" style="width:51%;" class="textbold">
                                                                <asp:TextBox ID="txtAgencyName" runat ="server" CssClass ="textbox" Width="503px" MaxLength="50" TabIndex="1"   ></asp:TextBox>
                                                                    <img tabIndex="2" src="../Images/lookup.gif"  onclick="javascript:return PopupAgencyPage();" /></td>                                                                
                                                                <td style="width:5%;" class="textbold" ></td>
                                                                <td style="width:20%;" ><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="7" /></td>
                                                            </tr>                                                      
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="9" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:12%;" class="textbold" ><span class ="textbold">1a Office</span></td>
                                                                <td style="width:26%;" class="textbold" >
                                                                <asp:DropDownList id="drp1Aoffice" runat="server" CssClass="dropdownlist" Width="158px" TabIndex="2"></asp:DropDownList></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:6%;" class="textbold" ></td>
                                                                <td style="width:11%;"><span class="textbold">LTR &nbsp;Number</span></td> 
                                                                <td style="width:20%;" class="textbold" >
                                                                <asp:TextBox ID="txtFeedBackNo" runat ="server" CssClass ="textbox"  MaxLength="9" TabIndex="3" Width="144px" ></asp:TextBox></td>
                                                                <td style="width:5%;" class="textbold" ></td>
                                                                <td style="width:20%;" >
                                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="8" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="9" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                           <tr>
                                                                <td style="width:6%; height: 19px;" class="textbold" >&nbsp;</td>
                                                                <td style="width:12%; height: 19px;" class="textbold" ><span class="textbold"> Date From</span></td>
                                                                <td style="width:26%; height: 19px;" class="textbold" >
                                                                <asp:TextBox ID="txtFeedBackFromDt" CssClass="textbox"  runat="server" Width="152px" TabIndex="4" MaxLength="10"></asp:TextBox>&nbsp;
                                                                <img id="FeedbackImgDT" alt="" src="../Images/calender.gif" TabIndex="13" title="" style="cursor: pointer" />
                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtFeedBackFromDt.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "FeedbackImgDT",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                </td>
                                                                <td style="width:1%; height: 19px;" class="textbold">
                                                                </td>
                                                                <td style="width:6%; height: 19px;" class="textbold" ></td>
                                                                <td style="width:11%; height: 19px;" ><span class="textbold"> Date To</span></td> 
                                                                <td style="width:20%; height: 19px;" class="textbold" >
                                                                <asp:TextBox ID="txtFeedbackDtTo" runat="server" CssClass="textbox" Width="144px" TabIndex="5" MaxLength="10"></asp:TextBox>&nbsp;
                                                                 <img id="Img1" alt="" src="../Images/calender.gif" style="cursor: pointer" tabindex="15" title="" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtFeedbackDtTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                    </script>
                                                               </td>
                                                                <td style="width:5%; height: 19px;" class="textbold" >
                                                                </td>
                                                                <td style="width:20%; height: 19px;" ><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="8" /></td>
                                                            </tr> 
                                                           <tr height="3px"></tr>
                                                           
                                                            <tr valign="top" >
                                                                <td style="width:6%; height: 31px;" class="textbold" >&nbsp;</td>
                                                                <td style="width:12%; height: 31px;" class="textbold" ><span class="textbold">Logged By</span></td>
                                                                <td style="width:26%; height: 31px;" class="textbold" >
                                                                <asp:TextBox ID="txtPendingWith" CssClass="textbox" MaxLength="40" runat="server" Width="152px" TabIndex="6"></asp:TextBox>&nbsp;
                                                        <img src="../Images/lookup.gif" alt=" " onclick="javascript:return PopupLoggedBy();" tabindex="9" /></td>
                                                                <td style="width:1%; height: 31px;" class="textbold">
                                                        </td>
                                                                <td style="width:6%; height: 31px;" class="textbold" ></td>
                                                                <td style="width:11%; height: 31px;" class="textbold" ></td> 
                                                                <td style="width:20%; height: 31px;" class="textbold" >
                                                                  
</td>
                                                                <td style="width:5%; height: 31px;" class="textbold" ></td>
                                                                <td style="width:20%; height: 31px;" >
                                                                    <span style="font-size: 9pt; font-family: Arial"></span>
                                                                </td>
                                                            </tr>                                                           
                                                             <tr>
                                                                <td  style="height: 15px" class="textbold" colspan="9" align="center" valign="TOP" > 
                                                                                                                               
                                                                </td>
                                                            </tr>
                                                             
                                                            
                                                               <tr>
                                                                <td  class="textbold" colspan="9" align="center" valign="TOP"> 
                                                               <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                               <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' /> 
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="9" align="center" valign="TOP" style="height:10px;">                                                                
                                                                 <asp:GridView ID="grdvFeedback" runat="server"  AutoGenerateColumns="False" TabIndex="9" EnableViewState="False"  RowStyle-VerticalAlign ="Top" Width="860px">
                                                                                 <Columns>
                                                                         <asp:TemplateField HeaderText="LTR No." ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" >
                                                                         <ItemTemplate>
                                                                         <%#Eval("FEEDBACK_ID")%>
                                                                         </ItemTemplate>
                                                                             <ItemStyle Width="100px" />
                                                                         </asp:TemplateField>
                                                                         
                                                                         <asp:TemplateField HeaderText="Agency Name"  >
                                                                         <ItemTemplate>
                                                                        <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AGENCYNAME") %>'></asp:Label>
                                                                         </ItemTemplate>
                                                                             <ItemStyle HorizontalAlign="Left" Width="300px" Wrap="True" />
                                                                             <HeaderStyle HorizontalAlign="Left" />
                                                                         </asp:TemplateField>
                                                                            <asp:BoundField DataField="AOFFICE" HeaderText="1a Office"  >
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                         <asp:BoundField DataField="DATETIME" HeaderText="Feedback Date" >
                                                                             <ItemStyle HorizontalAlign="Left" />
                                                                             <HeaderStyle HorizontalAlign="Left" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="LOGGEDBY" HeaderText="Logged By" >
                                                                             <ItemStyle HorizontalAlign="Left" />
                                                                             <HeaderStyle HorizontalAlign="Left" />
                                                                         </asp:BoundField>
                                                                                     
                                                                                     <asp:TemplateField HeaderText="Action" >
                                                                                         <ItemTemplate>
                                                                                             <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                                            <%-- <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                                             <asp:LinkButton ID="linkDelete" CssClass="LinkButtons" runat="server" Text="Delete"></asp:LinkButton>
                                                                                             <asp:HiddenField ID="hdFeedbackID" runat="server" Value='<%#Eval("FEEDBACK_ID")%>' />  
                                                                                           </ItemTemplate>
                                                                                         <ItemStyle Wrap="False" />
                                                                                    </asp:TemplateField>  
                                                                         
                                                                                 </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" VerticalAlign="Top" />
                                                                                        <HeaderStyle CssClass="Gridheading" />
                                                                                        
                                                                  </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>  
                                        </td>
                                    </tr>
                                       <tr>
                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">                                                                
                                            </td>
                                        </tr>
                                </table>
                                <asp:HiddenField ID="hdDeleteFlag" runat="server" />
                            </td>
                        </tr>
                     
                    </table>
                </td>
            </tr>
          
        </table>
    </form>
</body>
</html>
