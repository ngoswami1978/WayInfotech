<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManageTest.aspx.vb" Inherits="ManageTest"    %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Training::Manage Test</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

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
			
			if (id=="3")
			{
				//if (window.document.getElementById('<%=hdCourseID.ClientId %>').value != '')
			//	{
					type = "TRSR_Course.aspx?Popup=T";
   					//window.open(type,"aaCourseSessionSearchCourse","height=600,width=900,top=30,left=20,scrollbars=1");  
   					window.open(type,"aaTestCopy","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   					//document.forms(0).submit();
   					return false;
   	            //}
   	           // else
   	          //  {
   				//	document.getElementById("lblError").innerText = "Please Select the Course Name.";
   	          //  }
   	           // return false;
			}
	     }
	     
	 /*    function GetData(CourseId)
	     {
			 window.document.getElementById('<%=hdCourseID.ClientId %>').value = CourseId;
			 if (CourseId != '')
			 {
				document.forms['form1'].submit();
			 }
	     }*/
	     
   function ValidateTest()
     {
          if (document.getElementById('<%=txtTestName.ClientId%>').value == '')
               {
                document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Test Name ";
                document.getElementById('<%=txtTestName.ClientId%>').focus();
                return false;
               }   
         if (document.getElementById('<%=txtTestTime.ClientId%>').value == '')
               {
                document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Test Time.";
                document.getElementById('<%=txtTestTime.ClientId%>').focus();
                return false;
               }
               
         if (isNaN(document.getElementById('<%=txtTestTime.ClientId%>').value))
           {
            document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Integer Test Time ";
            document.getElementById('<%=txtTestTime.ClientId%>').focus();
            return false;
           } 
           if (document.getElementById('<%=txtTestTime.ClientId%>').value <= 0)
           {
            document.getElementById('<%=lblError.ClientId%>').innerText = "Test Time must  be greater than Zero ";
            document.getElementById('<%=txtTestTime.ClientId%>').focus();
            return false;
           } 
      /*     
          if (document.getElementById('txtNoOfQuest').value == '')
           {
            document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter No of Question.";
            document.getElementById('<%=txtNoOfQuest.ClientId%>').focus();
            return false;
           }  
            
           
             if (isNaN(document.getElementById('<%=txtNoOfQuest.ClientId%>').value))
           {
            document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter No of question In Integer";
            document.getElementById('<%=txtNoOfQuest.ClientId%>').focus();
            return false;
           } 
                
           if (document.getElementById('<%=txtNoOfQuest.ClientId%>').value <= 0)
           {
            document.getElementById('<%=lblError.ClientId%>').innerText = "No of Question must  be greater than Zero ";
            document.getElementById('<%=txtNoOfQuest.ClientId%>').focus();
            return false;
           } 
             */ 
          if (document.getElementById('<%=txtTMarks.ClientId%>').value == '')
               {
                document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Total Marks.";
                document.getElementById('<%=txtTMarks.ClientId%>').focus();
                return false;
               }
        
       
         if (isNaN(document.getElementById('<%=txtTMarks.ClientId%>').value))
           {
            document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Total marks In Integer";
            document.getElementById('<%=txtTMarks.ClientId%>').focus();
            return false;
           } 
           
           // Checking Max No. Of Questions and current questions.
           // Start 
           /*
           var TotalNoOfQuest = document.getElementById('<%=txtNoOfQuest.ClientId%>').value
           if (document.getElementById('GrdTestDetails').rows.length > 0)
           {
               var CurrentNoOfQuest= document.getElementById('GrdTestDetails').rows.length-1
               if (TotalNoOfQuest < CurrentNoOfQuest)
               {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Questions should be less than or equal to number of questions.";
               return false;
               }
               else
               {
                return true;
               }
           }
           
           */
           
           // End 
       
       }
      
  function ValidateQuestion()
  {
       if (document.getElementById('<%=txtQuest.ClientId%>').value == '')
               {
                document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Question ";
                document.getElementById('<%=txtQuest.ClientId%>').focus();
                return false;
               }   
      if (document.getElementById('<%=txtAns1.ClientId%>').value == '')
               {
                document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Answer No. 1";
                document.getElementById('<%=txtAns1.ClientId%>').focus();
                return false;
               }
        if (document.getElementById('<%=txtAns2.ClientId%>').value == '')
       {
        document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Answer No. 2";
        document.getElementById('<%=txtAns2.ClientId%>').focus();
        return false;
       }  
        var strAns3=document.getElementById("txtAns3").value;
        var strAns4=document.getElementById("txtAns4").value;
        var strRAns=document.getElementById("drpRAns").value;
        if ((strAns3 =="") && (strAns4 !=""))
        {
            document.getElementById('<%=lblError.ClientId%>').innerText = "Please Enter Answer No. 3";
            document.getElementById('<%=txtAns3.ClientId%>').focus();
            return false;
        }
           
                         
      if (document.getElementById('<%=drpRAns.ClientId%>').value == '--Select One--')
       {
        document.getElementById('<%=lblError.ClientId%>').innerText = "Please Select Right Answer";
        document.getElementById('<%=drpRAns.ClientId%>').focus();
        return false;
       } 
       
      strRAns=parseInt(strRAns,10);
      
      if ((strAns3 =="") && (strRAns>2))
      {
        document.getElementById('<%=lblError.ClientId%>').innerText = "Invalid Right Answer";
        document.getElementById('<%=drpRAns.ClientId%>').focus();
        return false;
      } 
      
      if ((strAns4 =="") && (strRAns>3))
      {
        document.getElementById('<%=lblError.ClientId%>').innerText = "Invalid Right Answer";
        document.getElementById('<%=drpRAns.ClientId%>').focus();
        return false;
      } 

 
   }
   

    </script>

</head>
<body>
    <form id="form1" runat="server">
    
    
 <table cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td>
    <!-- Code for Search Criteria -->
    <table width="860px" class="left">
                <tr>
                    <td>
                        <span class="menu">Training -&gt;</span><span class="sub_menu">Manage Test</span></td>
                </tr>
                <tr>
                    <td class="heading center">
                        Manage Test</td>
                </tr>
                <tr>
                    <td valign="top" colspan="2" class="redborder">
                        <table align="center" id="tb1" cellspacing="0" cellpadding="1" border="0" bgcolor="white"
                            style="width: 98%">
                            <tr>
                                <td colspan="5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="5" class="ErrorMsg">
                                    <asp:Label ID="lblError" runat="server" EnableViewState="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbold" >
                                    Course Name<span class="Mandatory">*</span></td>
                                <td colspan="3" >
                                    <asp:TextBox ID="txtTestName" runat="server" CssClass="textboxgrey" ReadOnly="true"
                                        Width="548px" TabIndex="2"></asp:TextBox></td>
                                <td >
                                    <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" Width="100px" TabIndex="3" AccessKey="a" />
                                </td>
                            </tr>
                            <tr>
                                <td class="textbold">
                                    Course Description</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtTestDesc" runat="server" CssClass="textboxgrey" TextMode="MultiLine"
                                        Width="550px" TabIndex="2" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnreset" runat="server" Text="Reset" CssClass="button" Width="100px" TabIndex="3" AccessKey="r" /></td>
                            </tr>
                            <tr>
                                <td  class="textbold">
                                    Total Marks</td>
                                <td>
                                    <asp:TextBox ID="txtTMarks" runat="server" CssClass="textboxgrey" TabIndex="2" ReadOnly="true" Width="200px"></asp:TextBox></td>
                                <td class="textbold">
                                    </td>
                                <td >
                                    <asp:TextBox ID="txtNoOfQuest" runat="server" CssClass="textboxgrey displayNone" TabIndex="2" Width="194px" ReadOnly="True"></asp:TextBox></td>
                                    <td>
                                    <asp:Button ID="btnQuestionSet" runat="server" Text="Print Question" CssClass="button" Width="100px" TabIndex="3" AccessKey="p" /></td>
                            </tr>
                            <tr>
                                <td  class="textbold">
                                    Test Time(MM)<span class="Mandatory">*</span>
                                </td>
                                <td class="textbold">
                                    <asp:TextBox ID="txtTestTime" onkeyup="checknumeric(this.id)" runat="server" CssClass="textfield" Width="200px" TabIndex="2" MaxLength="3"></asp:TextBox>&nbsp;&nbsp;
                                    &nbsp;&nbsp; &nbsp;
                                </td>
                                <td  class="textbold">
                                    Test<span class="Mandatory">*</span></td>
                                <td >
                                    <asp:DropDownList ID="ddlDays" runat="server" CssClass="dropdown" onkeyup="gotop(this.id)"
                                        TabIndex="2" Width="200px" AutoPostBack="True">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 15%">
                                    
                                </td>
                                <td style="width: 36%">
                                   
                                </td>
                                <td style="width: 12%">
                                   
                                </td>
                                <td style="width: 42%" >
                                   
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4" class="subheading" style="height: 16px">
                                    Question</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td >
                                   
                                </td>
                            </tr>
                            <tr>
                                <td  class="textbold">
                                    Question<span class="Mandatory">*</span></td>
                                <td colspan="3" >
                                    <asp:TextBox ID="txtQuest" runat="server" TextMode="MultiLine" Width="547px" TabIndex="2"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td  class="textbold">
                                    Ans1.<span class="Mandatory">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtAns1" runat="server" CssClass="textfield" TabIndex="2" Width="200px" Height="30px" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                <td  class="textbold">
                                    Ans2.<span class="Mandatory">*</span></td>
                                <td >
                                    <asp:TextBox ID="txtAns2" runat="server" CssClass="textfield" TabIndex="2" Width="202px" Height="30px" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="textbold" >
                                    Ans3.</td>
                                <td>
                                    <asp:TextBox ID="txtAns3" runat="server" CssClass="textfield" TabIndex="2" Width="200px" Height="30px" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                <td class="textbold" >
                                    Ans4.</td>
                                <td >
                                    <asp:TextBox ID="txtAns4" runat="server" CssClass="textfield" TabIndex="2" Width="204px" Height="30px" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                            <!--tr>
            <td class="textbold">
                Marks for Right Ans<span class="Mandatory">*</span></td>
            <td style="width: 173px">
                <asp:TextBox ID="txtMforRAns" runat="server" CssClass="textfield" TabIndex="14" Text="1"></asp:TextBox></td>
            <td class="textbold">
                Marks for Wrong Ans.<span class="Mandatory">*</span></td>
            <td style="width: 215px">
                <asp:TextBox ID="txtMforWAns" runat="server" CssClass="textfield" TabIndex="15" Text="0"></asp:TextBox></td>
        </tr-->
                            <tr>
                                <td  class="textbold">
                                    Right Ans<span class="Mandatory">*</span></td>
                                <td>
                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpRAns" runat="server" TabIndex="2" CssClass="dropdownlist" Width="200px">
                                        <asp:ListItem Value="--Select One--">--Select One--</asp:ListItem>
                                        <asp:ListItem Value="1">Ans1</asp:ListItem>
                                        <asp:ListItem Value="2">Ans2</asp:ListItem>
                                        <asp:ListItem Value="3">Ans3</asp:ListItem>
                                        <asp:ListItem Value="4">Ans4</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td  class="textbold" visible="false">
                                    &nbsp;</td>
                                <td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnAddToGrid" runat="server" Text="Add" CssClass="button" Width="91px"
                                        TabIndex="2" />
                                    </td>
                                <td>
                                    &nbsp;<asp:Button ID="btnCopy" runat="server" CssClass="button" OnClientClick="return PopupPage(3);"
                                        TabIndex="3" Text="Copy Questions" Width="100px" /></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td >
                                </td>
                            </tr>
                            <tr >
                                <td>
                                </td>
                                <td class="ErrorMsg" colspan="3">
                                    Fields marked * are mandatory.
                                </td>
                                <td>
                                </td>
                            </tr>
                          
                            <tr >
                                <td>
                                <asp:HiddenField id="hdCourseID" runat="server" />
                                <asp:HiddenField id="hdTestQuestion" runat="server" />
                                
                                </td>
                                <td >
                                </td>
                                <td>
                                </td>
                                <td >
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
            </table>
    </td>
    <td></td>
    
    </tr>
    
    <tr>
    <td colspan="2">
    <!-- Code for Search Result Gridview & Paging -->
    <table cellpadding="0" width="100%" cellspacing="0" border="0">
      <tr>
                                <td colspan="5" align="center">
                                <asp:HiddenField ID="hdDays" runat="server" />
                                <asp:HiddenField ID="hdCourseDuration" runat="server" />
                                    <asp:HiddenField ID="Qs_id" runat="server" />
                                    <asp:HiddenField ID="Test_id" runat="server" />
                                    <asp:GridView ID="GrdTestDetails" runat="server" AutoGenerateColumns="False"   CellPadding="1" Width="100%" AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField HeaderText="S.No.">
                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" Width="4%" />
                                                <ItemStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Question" DataField="QS_TEXT" SortExpression="QS_TEXT">
                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" Width="32%" />
                                                <ItemStyle HorizontalAlign="Left"  Width="32%" Wrap="True"/>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Ans1" DataField="QS_OPTION1" SortExpression="QS_OPTION1">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Left" Width="13%"/>
                                                <ItemStyle HorizontalAlign="Left" Wrap="True"  />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Ans2" DataField="QS_OPTION2" SortExpression="QS_OPTION2">
                                                <HeaderStyle  HorizontalAlign="Left" Wrap="True" Width="13%"/>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Ans3" DataField="QS_OPTION3" SortExpression="QS_OPTION3">
                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" Width="13%"/>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Ans4" DataField="QS_OPTION4" SortExpression="QS_OPTION4">
                                                <HeaderStyle Wrap="True" HorizontalAlign="Left"  Width="13%"/>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="R.Ans" DataField="QS_RIGHT_OPTION" SortExpression="QS_RIGHT_OPTION">
                                                <HeaderStyle Wrap="True" HorizontalAlign="Left"  Width="4%"/>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                <asp:LinkButton ID="BtnUpdate" runat="server" CssClass="LinkButtons" CommandName="EditU" CommandArgument='<%#Container.DataItem("QS_TEXT")+"~"+Container.DataItem("QS_OPTION1")+"~"+Container.DataItem("QS_OPTION2")+"~"+Container.DataItem("QS_RIGHT_OPTION")%>'>Edit</asp:LinkButton>&nbsp;
                                                    <asp:LinkButton ID="BtnDelete" runat="server" CssClass="LinkButtons" CommandName="EditX" CommandArgument='<%#Container.DataItem("QS_TEXT")+"~"+Container.DataItem("QS_OPTION1")+"~"+Container.DataItem("QS_OPTION2")+"~"+Container.DataItem("QS_RIGHT_OPTION")%>'>Delete</asp:LinkButton>
                                                    
                                              
                                                </ItemTemplate>
                                                 <ItemStyle Width ="8%" HorizontalAlign="Center" />
                                                <HeaderStyle Width="15%" />
                                            </asp:TemplateField>
                                           </Columns>
                                        <AlternatingRowStyle CssClass="lightblue" />
                                        <RowStyle CssClass="textbold" />
                                        <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                    </asp:GridView>
                                </td>
                            </tr>
    </table> 
    </td>
    </tr>
    
    </table>
    
        
            
           
    
    </form>
</body>
</html>
 