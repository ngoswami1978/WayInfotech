<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_DomainTopics.aspx.vb" Inherits="Training_TRUP_DomainTopics" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>AAMS::Training::Manage Topic</title>
	<link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
	
	<script language="javascript" type="text/javascript" src="../JavaScript/AAMS.js"></script>
	<script language="javascript" type="text/javascript">
		
		 function Delete()
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
              
               return true;        
            }
            else
            {
                return false;
            }
            
	}
		function PopupPage(id)
         {
			var type;
			if (id=="2")
			{
				type = "TRSR_Course.aspx?Popup=T";
   	            window.open(type,"aaCourseSessionSearchCourse","height=600,width=900,top=30,left=20,scrollbars=1");  
			}
			if (id=="3")
			{
			    
				if (window.document.getElementById('ddlCourseTitle').value != '')
				{
				    try
   				    {
			        window.document.getElementById('<%=hdCourseID.ClientId %>').value =window.document.getElementById('ddlCourseTitle').value;
			        }
   				    catch(err){}
					type = "TRSR_Course.aspx?Popup=T";
   					window.open(type,"aaCourseSessionSearchCourse123","height=600,width=900,top=30,left=20,scrollbars=1");  
   					 return false;
   	            }
   	            else
   	            {
   					document.getElementById("lblError").innerText = "Please Select the Course Name.";
   					try
   					{
   					window.document.getElementById('ddlCourseTitle').focus();
   					}
   					catch(err){}
   					 return false;
   	            }
   	            return false;
			}
	     }
	     function GetData(CourseId)
	     {
			 window.document.getElementById('<%=hdnCopyCourseId.ClientId %>').value = CourseId;
			 if (CourseId != '')
			 {
				document.forms['form1'].submit();
			 }
	     }
	     function GetDataMain(CourseId,CourseName)
	     {
	     
			 window.document.getElementById('<%=hdCourseID.ClientId %>').value = CourseId;
			
	     }
	</script>
</head>
<body >
	<form id="form1" runat="server" defaultbutton="btnSave" defaultfocus="ddlDomain" >
		<table width="860px" align="left" height="486px" class="border_rightred">
			<tr>
				<td valign="top">
					<table width="100%" align="left">
						<tr>
							<td valign="top" align="left">
								<span class="menu">Training-&gt;</span><span class="sub_menu">Manage FeedBack Topic</span></td>
						</tr>
						<tr>
							<td class="heading" align="center" valign="top">
								Manage FeedBack Topic	  
							</td>
						</tr>
						<tr>
							<td valign="top">
								<table width="100%" border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td class="redborder center">
											<table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
												<tr>
													<td style="width: 100px">
													</td>
													<td class="gap" colspan="3" style="text-align: center">
														<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
													<td>
													</td>
												</tr>
												<tr>
													<td style="width: 100px">   &nbsp;&nbsp;
													</td>
													<td class="textbold" style="width: 10%">
															Course<span class="Mandatory">*</span></td>
														 <td>
                                                             <asp:DropDownList  ID="ddlCourseTitle" runat="server" CssClass="textbold"                                                                  onkeyup="gotop(this.id)" TabIndex="2" Width="536px">
                                                             </asp:DropDownList></td>
                                                                     <td class="top" ></td>
													<td rowspan="4" class="top" align="center">  <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="9" AccessKey="s" /><br />
                                                        <asp:Button ID="btnNew" CssClass="button topMargin" runat="server" Text="New" TabIndex="10" AccessKey="n" /><br />
														<asp:Button ID="btnReset" CssClass="button topMargin" runat="server" Text="Reset" TabIndex="11" AccessKey="r" /><br />
                                                        <asp:Button ID="btnCopy" CssClass="button topMargin" runat="server" Text="Copy Topics" TabIndex="11" onClientclick="return PopupPage(3);" /><br />
														<asp:Button ID="btnPrintTopic" CssClass="button topMargin" runat="server" Text="Print Topics" TabIndex="11" AccessKey="p" />
                                                        
                                                        </td>
												</tr>
                                                <tr>
                                                    <td style="width: 100px">
                                                    </td>
                                                    <td class="textbold">
														Domain<span class="Mandatory">*</span></td>
                                                    <td style="width: 308px">
																<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlDomain" runat="server" CssClass="dropdownlist" Width="534px" TabIndex="2">
																	
															 </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px">
                                                    </td>
                                                    <td class="textbold">
															Topic<span class="Mandatory">*</span></td>
                                                    <td style="width: 308px">
															<asp:TextBox ID="txtTopics" runat="server" CssClass="textbox" 
                                                                            TabIndex="2" Width="530px" Height="50px" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
												<tr>
													<td style="width: 100px">
													</td>
													<td class="textbold">
                                                        Topic Order <span class="Mandatory">*</span></td>
													<td style="width: 308px">
                                                        <asp:TextBox ID="txtTopicNo" onkeyup="checknumeric(this.id)" MaxLength="2" runat="server" CssClass="textbox" TabIndex="2" Width="174px"></asp:TextBox>
                                                        <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add" TabIndex="10" /></td>
													<td></td>
												</tr>
												<tr>
													<td style="width: 100px; height: 26px">
													</td>
													<td colspan="2" style="height: 26px" class="ErrorMsg">
														&nbsp;Field Marked * are Mandatory</td>
													<td></td>
													<td style="height: 26px">
													</td>
												</tr>
												<tr>
													<td style="width: 100px; height: 26px">
													
													</td>
													<td class="ErrorMsg" colspan="2">
													</td>
													<td></td>
													<td>
													</td>
												</tr>
												<tr>
													<td colspan="5">
														<asp:GridView ID="gvTopic" runat="server" AutoGenerateColumns="False" TabIndex="6"
																Width="100%" EnableViewState="true" AllowSorting ="true" >
																<Columns>
																<asp:BoundField HeaderText="Domain No" DataField="TR_DOMAIN_ORDER" ItemStyle-CssClass="displayNone" HeaderStyle-CssClass="displayNone"  />
																	<asp:BoundField HeaderText="Domain" DataField="TR_VALTOPICDOM_NAME"  SortExpression="TR_VALTOPICDOM_NAME" />
																	<asp:BoundField HeaderText="Topic No" DataField="TR_CVALTOPICS_ORDER" ItemStyle-CssClass="displayNone" HeaderStyle-CssClass="displayNone"/>
																	<asp:BoundField HeaderText="Topic" DataField="TR_TOPICS" SortExpression="TR_TOPICS" />
																	<asp:TemplateField HeaderText="Action">
																		<ItemTemplate>
																			<asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons" CommandArgument='<%#Eval("TR_CVALTOPIC_ID")%>'></asp:LinkButton>&nbsp;
																			<asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete"
																				CssClass="LinkButtons" CommandArgument='<%#Eval("TempRowID")%>'></asp:LinkButton>
																			<asp:HiddenField ID="hdDomain" runat="server" Value='<%#Eval("TempRowID")%>' />
																		</ItemTemplate>
																		<ItemStyle Width="15%" />
																	</asp:TemplateField>
																</Columns>
																<AlternatingRowStyle CssClass="lightblue" />
																<RowStyle CssClass="textbold" />
																<HeaderStyle CssClass="Gridheading" ForeColor="white" />
																<PagerSettings PageButtonCount="5" />
															</asp:GridView>
													</td>
												</tr>
												<tr>
													<td colspan="5" style="text-align: center">
														<asp:HiddenField ID="hdID" runat="server" />
														<asp:HiddenField ID="hdCourseID" runat="server" />
														<asp:HiddenField ID="hdnCopyCourseId" runat="server" />
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

	<script language="javascript" type="text/javascript">
    function ValidateForm()
    {
     document.getElementById('<%=lblError.ClientId%>').innerText=''
     
       //*********** Validating Course title *****************************
   
   if(document.getElementById('<%=ddlCourseTitle.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Course is mandatory.'
            document.getElementById('<%=ddlCourseTitle.ClientId%>').focus();
            return false;
        }
        
        if(document.getElementById('gvTopic')!=null)
{
var cn=document.getElementById('gvTopic').rows.length;
if (cn<=1)
{
document.getElementById("lblError").innerHTML="Please add topics.";
return false;
}
}
else
{
document.getElementById("lblError").innerHTML="Please add topics.";
return false;
}

      
        
       return true; 
        
    }
    function ValidateTopics()
    {
    document.getElementById('<%=lblError.ClientId%>').innerText=''
    
    if(document.getElementById("ddlCourseTitle").value=='')
    {
    document.getElementById('<%=lblError.ClientId%>').innerText="Course  is mandatory"
    document.getElementById("ddlCourseTitle").focus();
    return false;
    }
    
    
       //*********** Validating domain  *****************************
    var cboGroup=document.getElementById('<%=ddlDomain.ClientId%>');
   if(cboGroup.selectedIndex ==0)
        {
         document.getElementById('<%=lblError.ClientId%>').innerText ='Domain is mandatory.'
         cboGroup.focus();
         return false;
            
        }
     
       //*********** Validating Course title *****************************
   
   if(document.getElementById('<%=txtTopics.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Topics is mandatory.'
            document.getElementById('<%=txtTopics.ClientId%>').focus();
            return false;
        }
        
        if(document.getElementById('<%=txtTopicNo.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Topic Order  is mandatory.'
            document.getElementById('<%=txtTopicNo.ClientId%>').focus();
            return false;
        }
        
    
    }
    
    function ClearControls()
    {
        document.getElementById("ddlCourseTitle").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
	</script>

</body>
</html>
