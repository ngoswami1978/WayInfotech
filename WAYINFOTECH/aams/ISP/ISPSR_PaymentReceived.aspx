<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPSR_PaymentReceived.aspx.vb" Inherits="ISP_ISPSR_PaymentReceived" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script type ="text/javascript" language ="javascript">
    
       function CheckValidation()       
       {
                   if(document.getElementById('txtPANO').value.trim()!="")
                    {
                     if(IsDataValid(document.getElementById("txtPANO").value,3)==false)
                      {
                        document.getElementById('lblError').innerHTML='PA No. is not valid.';             
                        document.getElementById("txtPANO").focus();
                       return false;
                      }
                    }       
       }
       function DetailsFunction(CheckBoxObj,CheckBoxObj2,CheckBoxObj3)
          {   
            document.getElementById('lblError').innerText="";   
              var type;
             // type = "ISPSR_PaymentProceed.aspx?Popup='p'&PaymentDetailsofPANumber=" + CheckBoxObj + "&PMonth="  +  CheckBoxObj2 + "&PYear=" + CheckBoxObj3; 
             type = "../Popup/PUSRISPPaymentDetails.aspx?Popup='p'&PaymentDetailsofPANumber=" + CheckBoxObj + "&PMonth="  +  CheckBoxObj2 + "&PYear=" + CheckBoxObj3;                 
             
           // window.location.href="ISPSR_PaymentProceed.aspx?PaymentDetailsofPANumber=" + CheckBoxObj; 
          
            window.open(type,'ISPPayDetails','height=600px,width=920px,top=100,left=100,scrollbars=1,status=1');           
            return false;
           
        }
          function EditFunction(CheckBoxObj)
        {         
                  document.getElementById('lblError').innerText =""; 
                  var type;
                  type = "ISPUP_PaymentReceived.aspx?Action=U&PANumber=" + CheckBoxObj;         
                  var strReturn; 
                  strReturn=window.open(type,'ISPPayEdit','height=400px,width=830px,top=150,left=150,scrollbars=0,status=1');                      
                  return false;
        }  
    </script>
</head>
<body>
    <form id="form1" runat="server"  defaultfocus="txtPANO" defaultbutton ="btnSearch">
     <table width="860px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP&nbsp; Serach Payment Received</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="width:860px">
                                <span >ISP</span> <span >&nbsp;Payment Received</span></td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 117px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder">
                                    <table border="0" cellpadding="2" cellspacing="1"  width="100%">
                                                                       <tr>
                                                                            <td colspan="5" class="center gap"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>                                                                        
                                                                        </tr>
                                                                         <tr>
                                                                            <td colspan="5" style="height:5px;" ></td> 
                                                                        </tr>                                                                      
                                                                        <tr>
                                                                            <td style="width:25%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold">
                                                                                <span class="textbold">PA No.</span>&nbsp;</td>
                                                                            <td style="width:15%">
                                                                                 <asp:TextBox ID="txtPANO" runat="server" CssClass="textbox" TabIndex="1" Width="147px"></asp:TextBox></td>
                                                                             <td style="width:10%" class="textbold" >
                                                                                 &nbsp;</td>                                                                           
                                                                            <td  style="width:45%"><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="5" AccessKey="A" /></td>                                                                                                                                                  
                                                                        </tr>
                                                                         <tr>
                                                                            <td style="width:25%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold"><span class="textbold">Cheque No.</span>
                                                                            </td>
                                                                            <td style="width:15%"> <asp:TextBox ID="txtChqNo" runat="server" CssClass="textbox" TabIndex="2" Width="147px"></asp:TextBox>
                                                                                </td>
                                                                             <td style="width:10%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:45%" class="textbold"><span class="textbold"><asp:Button ID="btnPrint" CssClass="button" runat="server" Text="Print" TabIndex="6" AccessKey="P" /></span></td>  
                                                                        </tr>                                                                        
                                                                          <tr>
                                                                            <td style="width:25%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold">
                                                                              
                                                                            <span class="textbold">Month</span>&nbsp;</td>
                                                                            <td style="width:15%"><asp:DropDownList ID="drpMonthFrom" runat="server" Width="154px" TabIndex="3" CssClass="dropdownlist" >
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
                                                                                </asp:DropDownList></td>
                                                                             <td style="width:10%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:45%" class="textbold"><span class="textbold">
                                                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="7" Text="Export" AccessKey="E" /></span></td>
                                                                          
                                                                        </tr>                                                               
                                                                          <tr>
                                                                            <td style="width:25%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold">
                                                                                <span class="textbold">Year</span></td>
                                                                            <td style="width:15%">
                                                                                <asp:DropDownList ID="drpYearFrom" runat="server" Width="154px" TabIndex="4" CssClass="dropdownlist" >
                                                                                </asp:DropDownList></td>
                                                                             <td style="width:10%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:45%" align="left" class="textbold">
                                                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="8" AccessKey="R" /></td>
                                                                          
                                                                        </tr> 
                                                                        <tr>
                                                                            <td class="textbold" style="height: 23px">
                                                                                &nbsp;<input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                           <td  >
                                                                                </td>
                                                                            <td  colspan="2" class="ErrorMsg" style="height: 23px">
                                                                                </td>
                                                                            <td style="height: 23px">
                                                                                &nbsp;</td>
                                                                            
                                                                        </tr>
                                                                    </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                          <td valign ="top"  >
                               <table border="0" cellpadding="2" cellspacing="1" >
                                   
                                    <tr>
                                                    <td colspan="8" valign ="top" >
                                                            <asp:GridView ID="gvIspPaymentRec" runat="server"  AutoGenerateColumns="False" TabIndex="9" Width="100%" AllowSorting="True"    >
                                                                     <Columns>
                                                                                      
                                                                                               <asp:TemplateField HeaderText="PA Month"  SortExpression="PAMonth">
                                                                                                <ItemTemplate  >
                                                                                                   <asp:Label ID="PAMonth" runat ="server" Text='<%# Eval("PAMonth") %>' ></asp:Label>
                                                                                                 </ItemTemplate>
                                                                                                   <ItemStyle Wrap="False" />
                                                                                                   <HeaderStyle Wrap="False" />
                                                                                               </asp:TemplateField>
                                                                                                 <asp:BoundField DataField="PAYear"  HeaderText="PA Year" SortExpression="PAYear" >
                                                                                                     <HeaderStyle Wrap="False" />
                                                                                                 </asp:BoundField>
                                                                                                 <asp:BoundField DataField="DTPASentToAccount" HeaderText="PA Sent To Account" SortExpression="DTPASentToAccount">
                                                                                                     <HeaderStyle Wrap="False" />
                                                                                                 </asp:BoundField>
                                                                                                 <asp:BoundField DataField="DTPAReceivedInAccount" HeaderText="PA Received In Account" SortExpression="DTPAReceivedInAccount">
                                                                                                     <HeaderStyle Wrap="False" />
                                                                                                 </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ChequeNumber"  HeaderText="Cheque Number" SortExpression="ChequeNumber"  >
                                                                                                     <HeaderStyle Wrap="False" />
                                                                                                 </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ChequeDate" HeaderText="Cheque Date"  SortExpression="ChequeDate">
                                                                                                     <HeaderStyle Wrap="False" />
                                                                                                 </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ChequeAmount" HeaderText="Cheque Amount" SortExpression="ChequeAmount" ItemStyle-HorizontalAlign ="right" >
                                                                                                     <HeaderStyle Wrap="False"  />
                                                                                                 </asp:BoundField>
                                                                                                 <asp:BoundField DataField="ChequeSentTo"  HeaderText="Cheque Sent To" SortExpression="ChequeSentTo"  HeaderStyle-Wrap="false"  />
                                                                                                 <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" >
                                                                                                     <HeaderStyle Wrap="False" />
                                                                                                 </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Action" >
                                                                                                <ItemTemplate  >
                                                                                                    <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                                                    <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a> 
                                                                                                     <asp:HiddenField ID="hdPANumber" runat="server" Value='<%#Eval("PANumber")%>' />
                                                                                                 </ItemTemplate>
                                                                                                    <ItemStyle Wrap="False" />
                                                                                                    <HeaderStyle Wrap="False" />
                                                                                               </asp:TemplateField>
                                                                                                                     
                                                                     </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" Wrap="false"  />                                                    
                                                         </asp:GridView></td>
                                                </tr>
                                   <tr>
                                       <td colspan="8" valign="top">
                                         <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                       </td>
                                   </tr>
                                                   <tr>
                                                    <td colspan ="8" align ="right">
                                                       </td>
                                     </tr>
                                                <tr><td align ="left" colspan ="8">&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
                                                  
                                </table>
                          </td>
                        </tr>
                    </table>
                </td>
            </tr>
                     
        </table>
    </form>
</body>
</html>
