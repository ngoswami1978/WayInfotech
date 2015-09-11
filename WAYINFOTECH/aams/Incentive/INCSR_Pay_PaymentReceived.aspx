<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_Pay_PaymentReceived.aspx.vb" Inherits="Incentive_INCSR_Pay_PaymentReceived" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Incentive Cheque Creation</title>
    <script type ="text/javascript" language ="javascript" >
        function EditFunction(PayID, ChainCode,objPeriod,objPLB,objMonth,objYear)               
        {          
               var type;       
                type = "../Incentive/INCUP_Pay_PaymentReceived.aspx?Action=U&Chain_Code=" + ChainCode + "&PayID=" + PayID +   "&Month=" + objMonth + "&Year=" + objYear + "&PLB=" + objPLB + "&Period=" + objPeriod  ;
   	            //window.open(type,"IncUp","height=900,width=1224,top=30,left=20,scrollbars=1,status=1");	                    
               window.location =type;
                return false;
        }   
            function ValidateForm()
           {   
              document.getElementById('lblError').innerHTML=''
              if  (document.getElementById('drpMonths').value !='' )
              {
                if  (document.getElementById('drpYears').value =='' )
                {
                    document.getElementById('lblError').innerHTML="Year is mandatory for month selection."
                    document.getElementById('drpYears').focus();
                    return false;
                }
              }
              
          
           }
    
    </script>
    
</head>
<body>
    <form id="form1" runat="server"  defaultbutton ="BtnSearch">
        <table style="width: 845px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 860px;">
                    <table width="100%">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Incentive-&gt;</span><span class="sub_menu"> Incentive Cheque Creation</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px; width:850px;">
                                Incentive Cheque Creation</td>
                        </tr>
                        <tr>
                            <td valign="top" align="LEFT">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left" class="redborder" style="width: 860px">
                                            <table style="width: 850px" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="7" align="center" style="height: 25px; width:840px;" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                       <td colspan ="6" style="width: 840px">
                                                           <table style="width: 840px" border="0" cellpadding="0" cellspacing="0">
                                                                       <tr>
                                                    <td style="width: 15%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 6%;" class="textbold">
                                                        <span class="textbold">Month</span></td>
                                                    <td style="width: 22%;" class="textbold">
                                                        <asp:DropDownList ID="drpMonths" runat="server" CssClass="dropdownlist" Width="158px"
                                                            TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 6%;">
                                                        <span class="textbold">Year</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:DropDownList ID="drpYears" runat="server" CssClass="dropdownlist" Width="158px"
                                                            TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 5%;" class="textbold">
                                                    </td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="BtnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3"
                                                            AccessKey="S" /></td>
                                                            
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%; height: 19px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td  class="textbold" nowrap="nowrap" colspan ="2">
                                                        <asp:CheckBox ID="ChkPenChequeCreation" runat="server" Text ="Pending cheque creation" /></td>
                                                    
                                                    <td style="width: 6%; height: 19px;">
                                                        <span class="textbold"></span>
                                                    </td>
                                                    <td style="width: 20%; height: 19px;" class="textbold">
                                                    </td>
                                                    <td style="width: 5%; height: 19px;" class="textbold">
                                                    </td>
                                                    <td style="width: 20%; height: 19px;">
                                                        <asp:Button ID="BtnExport" CssClass="button" runat="server" Text="Export" TabIndex="17"
                                                            AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 15%; height: 19px">
                                                    </td>
                                                    <td class="textbold" nowrap="nowrap" style="width: 6%; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="width: 22%; height: 19px">
                                                    </td>
                                                    <td style="width: 6%; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="width: 20%; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="width: 5%; height: 19px">
                                                    </td>
                                                    <td style="width: 20%; height: 19px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="17"
                                                            AccessKey="r" /></td>
                                                </tr>
                                                <tr >
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px" class="textbold" colspan="7" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="7" align="center" valign="TOP" style="height: 10px;">
                                                        &nbsp;</td>
                                                        
                                                </tr>
                                                           </table>
                                                       </td>
                                                       <td></td>
                                                 </tr> 
                                                <tr>
                                                    <td align="center" valign="top" style="width: 1000px;" colspan="7">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px">
                                                            <tr>
                                                                <td style="width: 1000px;" valign="top">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px">
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" colspan="6" style="width: 900px; " valign="top">
                                                                          
                                                                                <asp:GridView ID="GvIncPayReceived" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                  ShowHeader="true"  Width="900px" EnableViewState="true" AllowSorting="True" ShowFooter="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="BCaseId" ItemStyle-Width="70px" DataField ="BC_ID" SortExpression ="BC_ID" HeaderStyle-Width="60px"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Payment Id " ItemStyle-Width="70px" DataField ="PAYMENT_ID" SortExpression ="PAYMENT_ID" HeaderStyle-Width="70px"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Chain Code " DataField="CHAIN_CODE" SortExpression="CHAIN_CODE" HeaderStyle-Width="65px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Group Name" DataField="CHAIN_NAME" SortExpression="CHAIN_NAME" HeaderStyle-Width="140px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Aoffice" DataField="AOFFICE" SortExpression="AOFFICE" HeaderStyle-Width="80px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Account Manager" DataField="ACCOUNTSMANAGER" SortExpression="ACCOUNTSMANAGER" HeaderStyle-Width="120px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Cheque No." DataField="CHQ_NO" SortExpression="CHQ_NO" HeaderStyle-Width="100px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Cheque Amount" DataField="CHQ_AMOUNT" SortExpression="CHQ_AMOUNT" HeaderStyle-Width="100px"  ItemStyle-HorizontalAlign="Right" 
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Cheque Delivered To" DataField="CHQ_DELIVERED_NAME" SortExpression="CHQ_DELIVERED_NAME" HeaderStyle-Width="100px"
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>  
                                                                                          <asp:TemplateField  >
                                                                                        <HeaderTemplate  >
                                                                                            <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate  >    
                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server" visible ="true" >Edit</a>&nbsp;&nbsp;
                                                                                            <asp:HiddenField ID="hdPayID" runat="server" Value='<%#Eval("PAYMENT_ID" )%>' />                                                                                       
                                                                                            <asp:HiddenField ID="hdChainCode" runat="server" Value='<%#Eval("CHAIN_CODE" )%>' />
                                                                                            <asp:HiddenField ID="HDPLBCYCLE" value='<%# DataBinder.Eval(Container.DataItem, "PLBCYCLE")  %>' runat ="server" />
                                                                                            <asp:HiddenField ID="HDPLBPERIODFROM" value='<%# DataBinder.Eval(Container.DataItem, "PLBPERIODFROM")  %>' runat ="server" />
                                                                                            <asp:HiddenField ID="HDPLBPERIODTO" value='<%# DataBinder.Eval(Container.DataItem, "PLBPERIODTO")  %>' runat ="server" /> 
                                                                                             <asp:HiddenField ID="HDMONTH" value='<%# DataBinder.Eval(Container.DataItem, "MONTH")  %>' runat ="server" />
                                                                                            <asp:HiddenField ID="HDYEAR" value='<%# DataBinder.Eval(Container.DataItem, "YEAR")  %>' runat ="server" />                               
                                                                                            
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="50px" CssClass="ItemColor" HorizontalAlign="Center"  />
                                                                                        <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                    </asp:TemplateField>
                                                                                                           
                                                                                            
                                                                                                                                                                            
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="left" />
                                                                                    <RowStyle CssClass="textbold" HorizontalAlign="left" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="left" />
                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" width="860px;">
                                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr class="paddingtop paddingbottom">
                                                                                <td style="width: 30%" class="left">
                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                        ReadOnly="True"></asp:TextBox></td>
                                                                                <td style="width: 25%" class="right">
                                                                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                <td style="width: 20%" class="center">
                                                                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                                        Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                    </asp:DropDownList></td>
                                                                                <td style="width: 25%" class="left">
                                                                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textbold" align="center" valign="TOP" style="height: 10px;">
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
</body>
</html>
