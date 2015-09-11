<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TASR_GroupCase.aspx.vb" Inherits="TravelAgency_TASR_GroupCase" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Productivity::1 A Productivity Details</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript">      
           function ChangeView()
        {              
                     var USEORIGINAL ="N";
                     if (document.getElementById("ChkOrignalBook")!= null )
                     {
                         if (document.getElementById("ChkOrignalBook").checked==true)
                         {
                            USEORIGINAL ="Y";
                         }
                         else
                         {
                            USEORIGINAL ="N";
                         }
                     }
                     var MonDisp=document.getElementById("hdMonDisp").value;
                     var Year =document.getElementById("drpYear").value;
                     var LCode=document.getElementById("hdLCode").value;
                     var type="../TravelAgency/TASR_GroupCaseChangeView.aspx?Popup=T&Lcode=" + LCode + "&Year=" +  Year+ "&USEORIGINAL=" +  USEORIGINAL  + "&MonDisp=" +  MonDisp 
   	                 window.open(type,"aa2","height=600,width=905,top=30,left=20,scrollbars=1,status=1");	
                     return false;
        }
    </script>
    <style id="TextAlign" type="text/css" >
        .TextAlignRight
        {
                font-size: 11px;
                font-family: verdana, Arial;
                color: Black;
                background-color: #d3d3d3;
                border-right: #7f9db9 1px solid;
                border-top: #7f9db9 1px solid;
                border-left: #7f9db9 1px solid;
                border-bottom: #7f9db9 1px solid;
                text-align: right;
	        
        }
     
    </style>
</head>
<body   >
    <form id="form1"  runat="server" >    
     <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Agency -&gt;</span><span class="sub_menu">Group Case</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Group Case</td>
                        </tr>
                         <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                       <td style="width: 860px;" class="redborder" valign="top" >
                                                                 <table border="0" cellpadding="2" cellspacing="1" style="width: 845px;" class="left">
                                                                    <tr>
                                                                        <td class="center" colspan="8"  >
                                                                          <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                          <tr>
                                                                             <td colspan ="8">
                                                                                 <asp:Panel ID="pnlYear" runat="server"  width="100%" >
                                                                                         <table width="100%" border="0" cellspacing="2" cellpadding="2" >
                                                                                                    <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="subheading" colspan="5">
                                                                                                            Select Year</td>
                                                                                                        <td  ></td> 
                                                                                                        <td  style="width:10%">
                                                                                                        </td>
                                                                                                      </tr>
                                                                                                     <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="textbold"  colspan ="4" align="Right" >
                                                                                                            Assume Current Year&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="drpYear" runat ="server" CssClass="dropdown" Width="100px" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                                                                                        </td>
                                                                                                        <td class="left" style="width: 10%"><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="24" />
                                                                                                        </td>
                                                                                                         <td>
                                                                                                        </td>
                                                                                                          <td>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                      <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="textbold"  colspan ="4" align="Right" >
                                                                                                            &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                                                                                        </td>
                                                                                                        <td class="left" style="width: 10%"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="24" />
                                                                                                        </td>
                                                                                                         <td>
                                                                                                        </td>
                                                                                                          <td>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                         </table>
                                                                                 </asp:Panel>
                                                                             </td>
                                                                          </tr>
                                                                          <tr>                                                                          
                                                                             <td colspan ="8">
                                                                                 
                                                                                         <table border="0" cellspacing="0" id="PnlAgencyDetails"   visible ="false" runat="server"  cellpadding="2" >
                                                                                                    <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="subheading" colspan="5">
                                                                                                            Agency Details</td>
                                                                                                        <td  ></td> 
                                                                                                      </tr>
                                                                                                     <tr>
                                                                                                        <td >
                                                                                                        <input id="hdLCode" runat="server" style="width: 1px" type="hidden" /></td>
                                                                                                        <td class="textbold" >Agency Name</td>
                                                                                                         <td colspan="4" ><asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="3" Width="486px" ReadOnly="True"></asp:TextBox>
                                                                                                            </td>                                                       
                                                                                                        <td class="left" >
                                                                                                            <input type="button" class="button" tabIndex="39"  value="Change View" onclick="javascript:ChangeView();" style="width:86px" id="btnChangeView" runat="server"   /></td> 
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="textbold" > Address</td>                                                                               
                                                                                                        <td colspan="4" >
                                                                                                            <asp:TextBox ID="txtAdd" runat="server" CssClass="textboxgrey" Height="41px" MaxLength="50"
                                                                                                                ReadOnly="True" TabIndex="3" TextMode="MultiLine" Width="487px"></asp:TextBox></td>                                                      
                                                                                                          <td class="left" ></td> 
                                                                                                    </tr>  
                                                                                                     <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="textbold" >
                                                                                                            City</td>
                                                                                                        <td >
                                                                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                                TabIndex="3" Width="241px"></asp:TextBox></td>
                                                                                                         <td class="textbold" >
                                                                                                            </td>
                                                                                                             <td class="textbold" >
                                                                                                            Country</td>    
                                                                                                        <td class="textbold" >    
                                                                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                                TabIndex="3" Width="181px"></asp:TextBox></td>                                                      
                                                                                                        <td class="left" ></td> 
                                                                                                    </tr> 
                                                                                                    <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="textbold" >
                                                                                                            IATA</td>
                                                                                                        <td >
                                                                                                            <asp:TextBox ID="txtIATA" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                                TabIndex="3" Width="241px"></asp:TextBox></td>
                                                                                                         <td class="textbold" >
                                                                                                            </td>
                                                                                                             <td class="textbold" >
                                                                                                            </td>    
                                                                                                        <td class="textbold" >    </td>                                                      
                                                                                                        <td class="left" ></td> 
                                                                                                    </tr>  
                                                                                                    <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="textbold" >
                                                                                                            Online</td>
                                                                                                        <td >
                                                                                                            <asp:TextBox ID="txtOnline" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                                TabIndex="3" Width="241px"></asp:TextBox></td>
                                                                                                         <td class="textbold" >
                                                                                                            </td>
                                                                                                             <td class="textbold" >
                                                                                                                 OfficeId</td>    
                                                                                                        <td class="textbold" >    
                                                                                                            <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                                TabIndex="3" Width="181px"></asp:TextBox></td>                                                      
                                                                                                        <td class="left" ></td> 
                                                                                                    </tr>   
                                                                                                  
                                                                                                     <tr>
                                                                                                        <td >
                                                                                                        </td>
                                                                                                        <td class="textbold" >
                                                                                                            </td>
                                                                                                        <td >
                                                                                                            <asp:CheckBox ID="ChkOrignalBook" runat="server" CssClass="textbox" TabIndex="17" Text="NBS" Width="224px" AutoPostBack="True"  Height ="25px"/></td>
                                                                                                         <td class="textbold" >
                                                                                                            </td>
                                                                                                             <td class="textbold" >
                                                                                                            </td>    
                                                                                                        <td class="textbold" >    </td>                                                      
                                                                                                        <td class="left" ></td> 
                                                                                                    </tr>   
                                                                                                     <tr>
                                                                                                     <td></td>
                                                                                                         <td class="subheading" colspan="2">
                                                                                                           Amadeus Productivity</td>
                                                                                                         <td class="subheading" colspan="4">
                                                                                                            Summary</td>
                                                                                                   </tr> 
                                                                                                    <tr>
                                                                                                         <td ><input id="hdMonDisp" runat="server" style="width: 1px" type="hidden" /></td>
                                                                                                         <td colspan ="2" valign ="top" >
                                                                                                              <asp:Panel ID="pnlAmadeus" runat ="server"  ScrollBars="Vertical"  Width="95%" Height ="100px">
                                                                                                                 <asp:GridView EnableViewState="false" ID="grdvBidtDetails" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                                                        Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                                                                        AlternatingRowStyle-CssClass="lightblue"  >
                                                                                                                          <Columns>                                                                   
                                                                                                                                 <asp:BoundField DataField="MONTH" HeaderText="Month"   />  
                                                                                                                                 <asp:BoundField DataField="PRODUCTIVITY" HeaderText="Productivity"   />                                                                                                                              
                                                                                                                        </Columns>
                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                                                                                        <RowStyle CssClass="textbold" />
                                                                                                                  </asp:GridView>
                                                                                                               </asp:Panel>                       
                                                                                                         </td >
                                                                                                         <td colspan ="4"  rowspan ="3" valign ="top"  style ="height :230px;">
                                                                                                                <table  width="100%" border="0" cellspacing="2" cellpadding="2" >
                                                                                                                         <tr>
                                                                                                                            <td  class="textbold">
                                                                                                                                Efficiency (%) as on : <asp:Literal  ID="LitEffCurr" runat ="server"></asp:Literal> </td><td>
                                                                                                                                <asp:TextBox ID="txtEff" runat="server" CssClass="TextAlignRight"  MaxLength="50" ReadOnly="True"
                                                                                                                                    TabIndex="3" Width="86px"></asp:TextBox></td>
                                                                                                                         </tr>                                                                                                                        
                                                                                                                           <tr>
                                                                                                                            <td  class="textbold">
                                                                                                                                Deficit&nbsp; as on : <asp:Literal  ID="LitDef" runat ="server"></asp:Literal> </td><td>
                                                                                                                                <asp:TextBox ID="txtDef" runat="server" CssClass="TextAlignRight" MaxLength="50" ReadOnly="True"
                                                                                                                                    TabIndex="3" Width="86px"></asp:TextBox></td>
                                                                                                                         </tr>                                                                                                                        
                                                                                                                           <tr>
                                                                                                                            <td  class="textbold">
                                                                                                                                Deficit (%) as on : <asp:Literal  ID="LitDefPer" runat ="server"></asp:Literal> </td><td>
                                                                                                                                <asp:TextBox ID="txtDefPer" runat="server" CssClass="TextAlignRight" MaxLength="50" ReadOnly="True"
                                                                                                                                    TabIndex="3" Width="86px"></asp:TextBox></td>
                                                                                                                         </tr>                                                                                                                        
                                                                                                                                                                                                                                              
                                                                                                                           <tr>
                                                                                                                            <td  class="textbold"> Amadeus Avg. for the year : &nbsp;<asp:Literal  ID="LitAP" runat ="server"></asp:Literal>  </td><td>
                                                                                                                                <asp:TextBox ID="txtAmadAvgPrev" runat="server" CssClass="TextAlignRight" MaxLength="50" ReadOnly="True"
                                                                                                                                    TabIndex="3" Width="86px"></asp:TextBox></td>                                                                                                                                    
                                                                                                                         </tr>                                                                                                                       
                                                                                                                           <tr>
                                                                                                                            <td  class="textbold">
                                                                                                                                Amadeus Avg. for the year : &nbsp;<asp:Literal  ID="LitAN" runat ="server"></asp:Literal> </td><td>
                                                                                                                                <asp:TextBox ID="txtAmadAvgCurr" runat="server" CssClass="TextAlignRight" MaxLength="50" ReadOnly="True"
                                                                                                                                    TabIndex="3" Width="86px"></asp:TextBox></td>                                                                                                                                    
                                                                                                                         </tr>
                                                                                                                           <tr>
                                                                                                                            <td  class="textbold">
                                                                                                                                Total Potential for the year :  <asp:Literal  ID="LitPP" runat ="server"></asp:Literal> </td><td>
                                                                                                                                <asp:TextBox ID="txtPotPrev" runat="server" CssClass="TextAlignRight" MaxLength="50" ReadOnly="True"
                                                                                                                                    TabIndex="3" Width="86px"></asp:TextBox></td>                                                                                                                                    
                                                                                                                         </tr>
                                                                                                                           <tr>
                                                                                                                            <td  class="textbold">
                                                                                                                                Total Potential for the year :  <asp:Literal  ID="LitPN" runat ="server"></asp:Literal> </td><td>
                                                                                                                                <asp:TextBox ID="txtPotCur" runat="server" CssClass="TextAlignRight" MaxLength="50" ReadOnly="True"
                                                                                                                                    TabIndex="3" Width="86px"></asp:TextBox></td>                                                                                                                                    
                                                                                                                         </tr>
                                                                                                                </table>
                                                                                                         </td>
                                                                                                   </tr> 
                                                                                                     <tr>
                                                                                                          <td></td>
                                                                                                         <td class="subheading" colspan="2">
                                                                                                           All CRS Average Productivity</td>
                                                                                                        <%-- <td colspan="4">
                                                                                                            </td>--%>
                                                                                                   </tr> 
                                                                                                    <tr>
                                                                                                         <td></td>
                                                                                                         <td colspan ="2">
                                                                                                          <asp:Panel ID="Panel1" runat ="server"  ScrollBars="Vertical"  Width="95%" Height ="100px">
                                                                                                                 <asp:GridView EnableViewState="false" ID="GvAvgCrs" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                                                        Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                                                                        AlternatingRowStyle-CssClass="lightblue" >
                                                                                                                          <Columns>                                                                   
                                                                                                                                 <asp:BoundField DataField="YEAR" HeaderText="Year"   />  
                                                                                                                                 <asp:BoundField DataField="CRS" HeaderText="CRS"   /> 
                                                                                                                                 <asp:BoundField DataField="PRODUCTIVITY_AVG" HeaderText="Avg. Productivity"   />                                                                                                                              
                                                                                                                        </Columns>
                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                                                                                        <RowStyle CssClass="textbold" />
                                                                                                                  </asp:GridView>  
                                                                                                                 </asp:Panel>                   
                                                                                                         </td >
                                                                                                        <%-- <td colspan ="4"></td>--%>
                                                                                                   </tr>                                                                                          
                                                                                         </table>                                                                                 
                                                                                 
                                                                             </td>
                                                                          </tr>                                                                     
                                                                          <tr>
                                                                             <td colspan ="8"></td>
                                                                       </tr> 
                                                                       <tr>
                                                                          <td  colspan ="8">
                                                                            <asp:GridView ID="GVNav" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                                                 AllowPaging="true"        Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor" ShowHeader="false" 
                                                                                                                        AlternatingRowStyle-CssClass="lightblue" PageSize="1" >
                                                                                                                          <Columns> 
                                                                                                                          <asp:TemplateField HeaderText ="Lcode" Visible ="false"  >
                                                                                                                                 <ItemTemplate >
                                                                                                                                 <%# Eval("Lcode") %>
                                                                                                                                 </ItemTemplate>
                                                                                                                          </asp:TemplateField>      
                                                                                                                          <asp:TemplateField >
                                                                                                                                 <ItemTemplate >
                                                                                                                                 <asp:Label ID="lblGetData" runat="server" Text='<%# GetData(Eval("Lcode")) %>' />                                                                                                                                
                                                                                                                                 </ItemTemplate>
                                                                                                                          </asp:TemplateField>                                                               
                                                                                                                                   
                                                                                                                        </Columns>
                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                                                                                        <RowStyle CssClass="textbold" />
                                                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast"
                                                                                    NextPageText="Next" PreviousPageText="Prev" />
                                                                                <PagerStyle Wrap="False" />
                                                                                                                  </asp:GridView>  
                                                                          </td>
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
                </td>
            </tr>           
        </table>     
    </form>
</body>
</html>
