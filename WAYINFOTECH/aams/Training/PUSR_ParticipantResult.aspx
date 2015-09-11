<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_ParticipantResult.aspx.vb" Inherits="Training_PUSR_ParticipantResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Training:Participant Result</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
     
     function fnWOID()
    {
   
    window.close();
        return false;
   
    }
     </script>
</head>
<body  >
    <form id="frmPtrHistory" runat="server" >
        <table width="860px" align="left" height="486px" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">  
                         <tr>
                            <td align="right">
                                <asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnWOID()" >Close</asp:LinkButton>&nbsp;&nbsp;&nbsp;</td>
                        </tr>                      
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Participant Result</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="1" cellspacing="2" >
                                                            <tr>
                                                                <td  class="textbold" colspan="5" align="center" >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                                <td align="center" class="textbold" colspan="1">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="5" >
                                                                </td>
                                                                <td align="center" colspan="1">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                 <td style="width: 2%">   </td>
                                                                 <td class="textbold" style="width: 23%"> Course Title</td>
                                                                <td class="textbold" colspan="3" valign="top">
                                                                    <asp:TextBox ID="txtCourseTitle" runat="server" CssClass="textboxgrey"
                                                                        ReadOnly="True" TabIndex="20" Width="475px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                <td class="textbold" colspan="1" valign="top">
                                                                <asp:Button ID="btnExport" runat="server" AccessKey="e" CssClass="button" TabIndex="3"
                                                                        Text="Export" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 2%">
                                                                </td>
                                                                <td class="textbold" style="width: 23%">
                                                                    Course Level</td>
                                                                <td class="textbold" style="width: 25%">
                                                                    <asp:TextBox ID="txtCourseLevel" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 15%">
                                                                    Test Day</td>
                                                                <td class="textbold" style="width: 35%"> 
                                                                    <asp:TextBox ID="txtTestDay" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 35%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 2%">
                                                                </td>
                                                                <td class="textbold" >
                                                                    Total Marks</td>
                                                                <td class="textbold" >
                                                                    <asp:TextBox ID="txtTotalMarks" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                <td class="textbold" >
                                                                    Marks Obtained</td>
                                                                <td class="textbold" >
                                                                    <asp:TextBox ID="txtMarksObtained" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                <td class="textbold">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 2%">
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="center">
                                                                    <asp:GridView ID="gvParticipantResult" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False"
                                                                        Width="99%" TabIndex="9">
                                                                        <Columns>
                                                                         
                                                                        <asp:BoundField DataField="QS_ID" HeaderText="S.No." />
                                                                        <asp:BoundField DataField="QS_TEXT" HeaderText="Question" ItemStyle-Width="150px" ItemStyle-Wrap="true"  />
                                                                        <asp:BoundField DataField="QS_OPTION1" HeaderText="Ans1" />
                                                                        <asp:BoundField DataField="QS_OPTION2" HeaderText="Ans2" />
                                                                        <asp:BoundField DataField="QS_OPTION3" HeaderText="Ans3" />
                                                                        <asp:BoundField DataField="QS_OPTION4" HeaderText="Ans4" />
                                                                        <asp:BoundField DataField="QS_RIGHT_OPTION" HeaderText="Right Ans" />
                                                                        <asp:BoundField DataField="ANSWER_GIVEN" HeaderText="Ans Given" />
                                                                        <asp:BoundField DataField="ANSWER_GIVEN" HeaderText="Correct" />
                                                                        
                                                                        
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                    </td>
                                                              
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" >
                                                                </td>
                                                                <td colspan="1">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                  
                                        </td>
                                    </tr>
                                </table>
                                
                                <asp:Literal ID="litAgencyGroup" runat="server" ></asp:Literal></td>
                        </tr>
                    </table>
                    
                   </td>             
               
            </tr>
        </table>
    </form>
</body>
</html>
