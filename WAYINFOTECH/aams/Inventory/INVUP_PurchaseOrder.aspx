<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_PurchaseOrder.aspx.vb" Inherits="Inventory_INVUP_PurchaseOrder" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::HelpDesk::Manage Call Log</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script language="javascript" type="text/javascript">


function ColorMethod(id,total)
{   
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
            document.getElementById(Hcontrol).className="headingtabactive";
        }
        
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblPanelClick').value =id; 
       var pgStatus=document.getElementById('hdPageStatus').value;
       
    //   if(pgStatus=='U')
    //   {    
       
       if (id == (ctextFront +  "00" + ctextBack))
       {   
        
            document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById('hdTabType').value='0';
            return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
           document.getElementById('hdTabType').value='1';
           document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
           return false;         
       }
       
    }


    function HideShow()
    {var strTabtype=document.getElementById("hdTabType").value;
    switch(strTabtype)
    {
    case "0":
            document.getElementById("pnlCall").style.display="block";
            document.getElementById("pnlDesc").style.display="none";
            document.getElementById("theTabStrip_ctl00_Button1").className="headingtab";     
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtabactive";     
            break;
    case "1":
            document.getElementById("pnlCall").style.display="none";
            document.getElementById("pnlDesc").style.display="block";
            document.getElementById("theTabStrip_ctl00_Button1").className="headingtabactive";     
            document.getElementById("theTabStrip_ctl01_Button1").className="headingtab";     
            
           break;
       }
    }
    
    function SupplierName()
    {
     var type;
        type = "INVSR_Supplier.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
    }
    
    function EmployeeName()
    {
            var type;      
             var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
            if (strEmployeePageName!="")
            {
                type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
                //type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	            window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                return false;
            }
    }
    
     function DeleteFunction(TempRowID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteFlag").value=TempRowID;
           return true;
           }
           else
           {
                document.getElementById("hdDeleteFlag").value="";
                 return false;
           }
        }
        
        function EditFunction(TempRowID)
        {               
           document.getElementById("hdEditFlag").value=TempRowID;
        }
      
      function ProductValidation()
      {
          if(document.getElementById("drpProductName").selectedIndex=='0')
          {
              document.getElementById("lblError").innerHTML="Product Name Cann't be blank";
              document.getElementById("drpProductName").focus();
              return false;
          }
     
   
       if(document.getElementById("hdReadonlyFlag").value=='1')
       {
              if(document.getElementById("txtQty").value=='')
              {
                  document.getElementById("lblError").innerHTML="Qunatity Cann't be blank";
                  document.getElementById("txtQty").focus();
                  return false;
              }
              if(document.getElementById("txtQty").value!='')
              {
                 var txtVal=document.getElementById("txtQty").value;
                  if(IsDataValid(txtVal,3)==false)
                  {
                      document.getElementById("lblError").innerHTML="Qunatity is Numeric";
                      document.getElementById("txtQty").focus();
                      return false;
                  }
              }
         }
  
   
       if(document.getElementById("txtTax").value=='')
        {
            document.getElementById("lblError").innerHTML="Tax Cann't be blank";
            document.getElementById("txtTax").focus();
            return false;
         }
      
      if(document.getElementById("txtTax").value!='')
      {
          var txtVal=document.getElementById("txtTax").value;
          if(IsDataValid(txtVal,5)==false)
          {
              document.getElementById("lblError").innerHTML="Tax is Numeric";
              document.getElementById("txtTax").focus();
              return false;
          }
      
      }
      
      
      
       if(document.getElementById("txtRates").value=='')
      {
          document.getElementById("lblError").innerHTML="Rate Cann't be blank";
          document.getElementById("txtRates").focus();
          return false;
         }
      
      if(document.getElementById("txtRates").value!='')
      {
          var txtVal=document.getElementById("txtRates").value;
          if(IsDataValid(txtVal,5)==false)
          {
              document.getElementById("lblError").innerHTML="Rate is Numeric";
              document.getElementById("txtRates").focus();
              return false;
          }
      
      }
      

      
       if(document.getElementById("txtDeliveryDt").value=='')
      {
      document.getElementById("lblError").innerHTML="Deliver Date Cann't be blank";
      document.getElementById("txtDeliveryDt").focus();
      return false;
      }
      
      if(document.getElementById('<%=txtDeliveryDt.ClientId%>').value != '')
        {
            if (isDate(document.getElementById('<%=txtDeliveryDt.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Deliver Date Format is Invalid.";			
	           document.getElementById('<%=txtDeliveryDt.ClientId%>').focus();
	           return false;  
            }
       }
       
        if(document.getElementById("drpGodown").selectedIndex=='0')
          {
              document.getElementById("lblError").innerHTML="Godown Name Cann't be blank";
              document.getElementById("drpGodown").focus();
              return false;
          }
      }  
      
      
      
      
      function ValidateSupplier()
      {
      
           if(document.getElementById("hdSupplierID").value=='')
          {
              document.getElementById("lblError").innerHTML="Supplier Name Cann't be blank";
              document.getElementById("txtSupplierName").focus();
              return false;
          }
          
        
          if(document.getElementById("txtOrderDt").value=='')
          {
              document.getElementById("lblError").innerHTML="Order Date Cann't be blank";
              document.getElementById("txtOrderDt").focus();
              return false;
          }
          
              
              if(document.getElementById('<%=txtOrderDt.ClientId%>').value != '')
            {
                if (isDate(document.getElementById('<%=txtOrderDt.ClientId%>').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "Order Date Format is Invalid.";			
	               document.getElementById('<%=txtOrderDt.ClientId%>').focus();
	               return(false);  
                }
           }
         
      
      }
      
       function validateProductQty()
       {
   
        var item=document.getElementById("drpProductName").selectedIndex;
        var textProduct=document.getElementById("drpProductName").selectedvalue;
			     var text1=document.getElementById("drpProductName").options[item].value;
			     var varname = new Array(3);
			     varname=text1.split("|");
			     document.getElementById("txtQty").disabled=false;
			     
                 document.getElementById("txtQty").className="textbox";
               document.getElementById("txtQty").setAttribute("readonly", "false");
               document.getElementById("txtQty").readonly=false;
               document.getElementById("hdReadonlyFlag").value='1';

//			          alert(text1);
//			          alert(varname);

//                alert(varname[0]);
//                alert(varname[1]);
//                alert(varname[2]);
               if((varname[1]=='False') && (varname[2]=='False'))
               {
//               alert("test Success");
            document.getElementById("txtQty").value='';
               //document.getElementById("txtQty").readonly= true;
               //document.getElementById("txtQty").isTextEdit=false;
                document.getElementById("txtQty").disabled=true;
                 document.getElementById("txtQty").className="textboxgrey";
               document.getElementById("hdReadonlyFlag").value='-1';
                 //hdProductName
               document.getElementById("hdProductName").value='-1';
                 
               //document.getElementById("txtQty").setAttribute("readonly", "true");
               }
//               else
//               {
//               alert("else part");
//                document.getElementById("txtQty").disabled=false;
//                 document.getElementById("txtQty").className="textbox";
//               document.getElementById("txtQty").setAttribute("readonly", "false");
//               document.getElementById("txtQty").isTextEdit=true;
//               
//               }
			     
//			        var ss = "testing-delimiter-split-function";
//                    var result = ss.split("-");

	
			     
      // var test=document.getElementById("drpProductName").selectedvalue[document.getElementById("drpProductName").selectedIndex];
  
       }
       
        
   </script>
</head>
<body onload="HideShow()" >
    <form id="form1" runat="server">
     <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="860px">
                        <tr>
                            <td class="top">
                                <table width="860px" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Inventory-&gt;</span><span class="sub_menu">Purchase Order</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="heading center">
                                            Manage Purchase Order</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px;width:80%">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" Width="100px" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width:20%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="redborder top" colspan="2" style="width:100%">
                                            <table width="860px" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP"   >
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width:860px;">
                                                  
                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="width:90%" >
                                                                    <asp:Panel ID="pnlCall" runat="server" Width="100%">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="gap" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                  <tr>
                                                                    <td style="width: 2%">
                                                                    </td>
                                                                    <td class="subheading" colspan="4">
                                                                        Order Details</td>
                                                                </tr>
                                                                        <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="textbold" style="width: 136px">
                                                                                Purchase Order No.</td>
                                                                            <td class="textbold" style="width: 227px" >
                                                                                <asp:TextBox ID="txtPurchaseOrderNo" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="1" Width="176px" MaxLength="15"></asp:TextBox></td>
                                                                                    <td class="textbold">
                                                                                        Supplier Invoice</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtSupplierInv" runat="server" CssClass="textbox" TabIndex="1"
                                                                                            Width="160px" MaxLength="15"></asp:TextBox></td>
                                                                                    
                                                                            <td class="top" colspan="2" rowspan="1">
                                                                            </td>
                                                                        </tr>
                                                                      
                                                                <tr>
                                                                    <td style="width:2%">
                                                                       </td>
                                                                    <td class="textbold" style="width: 136px">
                                                                        Supplier <span class="Mandatory">*</span> 
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textboxgrey"  Width="520px" ReadOnly="True" TabIndex="1" MaxLength="100"  ></asp:TextBox>
                                                                        <img id="Img2" runat="server" alt="" onclick="return SupplierName();" style="cursor:pointer;"
                                                                            src="../Images/lookup.gif" tabindex="1" />
                                                                            </td>
                                                                     <td class="top" colspan="2" rowspan="12">
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 136px">
                                                                        Address</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Width="520px" ReadOnly="True" Height="50px" Rows="5" TextMode="MultiLine" TabIndex="1" MaxLength="200"></asp:TextBox>
                                                                   </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 136px">
                                                                        Order Date <span class="Mandatory">*</span> </td>
                                                                    <td style="width: 227px">
                                                                        <asp:TextBox ID="txtOrderDt" runat="server" CssClass="textbox" Width="170px" TabIndex="1" MaxLength="10"></asp:TextBox>
                                                                        <img id="imgOrderDt" alt="" src="../Images/calender.gif"  title="" style="cursor: pointer" tabindex="1" />
                                                                          <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOrderDt.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgOrderDt",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script> 
                                                                    </td>
                                                                    <td class="textbold" style="width: 122px">
                                                                        Creation Date</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCreationDt" runat="server" CssClass="textboxgrey" Width="163px" TabIndex="1" ReadOnly="True" MaxLength="20"></asp:TextBox>
                                                                       </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Last Delivery Penalty</td>
                                                                    <td class="textbox" colspan="3">
                                                                        <asp:TextBox ID="txtLastDeliveryPenaly" runat="server" CssClass="textbox" Width="520px" TabIndex="1" MaxLength="100"></asp:TextBox></td>
                                                                </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" style="width: 136px">
                                                                                Last Payment Penalty</td>
                                                                            <td class="textbold" colspan="3">
                                                                        <asp:TextBox ID="txtLastPaymentPenalty" runat="server" CssClass="textbox" Width="520px" TabIndex="1" MaxLength="100"></asp:TextBox>
                                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" style="width: 136px">
                                                                        Payment Terms</td>
                                                                            <td colspan="3" class="textbold">
                                                                        <asp:TextBox ID="txtPaymentTerms" runat="server" CssClass="textbox" Width="520px" TabIndex="1" MaxLength="100"></asp:TextBox>
                                                                        </td>
                                                                        </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 136px">
                                                                        Description</td>
                                                                    <td colspan="3" class="textbold">
                                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" Width="520px" TabIndex="1"></asp:TextBox></td>
                                                                </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" style="width: 136px">
                                                                                Notes</td>
                                                                            <td class="textbold" colspan="3">
                                                                                 <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox" Width="520px" Height="50px" Rows="5" TextMode="MultiLine" TabIndex="1" MaxLength="200"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="gap">
                                                                            </td>
                                                                            <td class="textbold" style="width: 136px">
                                                                                Created By</td>
                                                                            <td style="width: 227px">
                                                                                <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="1" Width="170px" MaxLength="10"></asp:TextBox></td>
                                                                            <td class="textbold" style="width: 122px">
                                                                                Approved By</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLogedByName" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="1" Width="163px" MaxLength="100"></asp:TextBox>
                                                                                     <img id="ImageApprovedBy" runat="server" alt="" onclick="return EmployeeName();" style="cursor:pointer;"
                                                                            src="../Images/lookup.gif" tabindex="1" />
                                                                                    </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="gap" style="height: 19px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 136px; height: 19px">
                                                                                Approval Date</td>
                                                                            <td style="height: 19px; width: 227px;">
                                                                            <asp:TextBox ID="txtAppovalDt" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True" TabIndex="1" MaxLength="10"></asp:TextBox>
                                                                        <img id="imgApprovalDt" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" tabindex="1" />
                                                                          <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtAppovalDt.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgApprovalDt",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script> 
                                                                            </td>
                                                                            <td class="textbold" style="height: 19px; width: 122px;">
                                                                            </td>
                                                                            <td style="height: 19px">
                                                                            </td>
                                                                        </tr>
                                                                
                                                                
                                                                
                                                                
                                                                <tr>
                                                                    <td class="gap">
                                                                    </td>
                                                                    <td class="textbold" style="width: 136px">
                                                                    </td>
                                                                    <td style="width: 227px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 122px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 56px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 136px; height: 56px;">
                                                                        </td>
                                                                    <td style="width: 227px; height: 56px;">
                                                                        </td>
                                                                    <td class="textbold" style="width: 122px; height: 56px;">
                                                                        </td>
                                                                    <td style="height: 56px">
                                                                       <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        <asp:HiddenField ID="hdCreatedby" runat="server" /><asp:HiddenField ID="hdSupplierID" runat="server" />
                                                                        <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                    </td>
                                                                </tr>
                                                                    </table>
                                                                       
                                                                       </asp:Panel>
                                                                       
                                                                    <asp:Panel ID="pnlDesc" runat="server" Width="800px" CssClass="displayNone">
                                                                 <table width="800px" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                       
                                                                
                                                                
                                                                
                                                                
                                                                
                                                                <tr>
                                                                    <td style="width: 100px" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4">
                                                                        Product Details</td>
                                                                    <td class="subheading" colspan="1">
                                                                    </td>
                                                                </tr>
                                                                      
                                                                <tr>
                                                                    <td class="textbold" style="width: 100px">
                                                                    </td>
                                                                    <td class="textbold" nowrap="nowrap">
                                                                        Product Name<span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpProductName" runat="server" TabIndex="2" Width="386px" CssClass="textbold">
                                                                       </asp:DropDownList>
                                                                       </td>
                                                                    <td colspan="1">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 100px;">
                                                                    
                                                                    </td>
                                                                    <td class="textbold" style="width: 130px; height: 24px;">
                                                                        Quantity <span class="Mandatory">*</span> 
                                                                    </td>
                                                                    <td style="width: 80px; height: 24px;">
                                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="textbox"
                                                                            TabIndex="2" Width="122px" MaxLength="5"></asp:TextBox></td>
                                                                    <td class="textbold" style="height: 24px">
                                                                        Rates <span class="Mandatory">*</span></td>
                                                                    <td style="width: 330px">
                                                                        <asp:TextBox ID="txtRates" runat="server" CssClass="textbox"
                                                                            TabIndex="2" Width="107px" MaxLength="10"></asp:TextBox></td>
                                                                    <td style="width: 200px; height: 24px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 100px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Tax &nbsp;<span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTax" runat="server" CssClass="textbox"
                                                                            TabIndex="2" Width="119px" MaxLength="10"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 80px">
                                                                        Warranty  
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWarranty" runat="server" CssClass="textbox"
                                                                            TabIndex="2" Width="107px" MaxLength="3"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 100px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        W/On Site</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOnsite" runat="server" TabIndex="2" /></td>
                                                                    <td class="textbold" nowrap="nowrap" >
                                                                        Delivery Date <span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDeliveryDt" runat="server" CssClass="textboxgrey" MaxLength="10" ReadOnly="True"
                                                                            Width="110px" TabIndex="2"></asp:TextBox>
                                                                             <img id="imgDeliveryDt" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" tabindex="2" />
                                                                          <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDeliveryDt.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDeliveryDt",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script> 
                                                                         
                                                                        </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 100px">
                                                                    
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Godown <span class="Mandatory">   *</span></td>
                                                                    <td colspan="3" nowrap="nowrap">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpGodown" runat="server" TabIndex="2" Width="386px" CssClass="textbold">
                                                                       </asp:DropDownList>
                                                                        <asp:Button ID="btnAdd" runat="server" TabIndex="2" CssClass="button" Text="Add" Width="64px" /></td>
                                                                    <td colspan="1" nowrap="nowrap">
                                                                    </td>
                                                                </tr>
                                                                <tr height="20px" ></tr>
                                                                     <tr>
                                                                         
                                                                         <td colspan="6" nowrap="nowrap" style="width: 100px">
                                                                            <asp:GridView ID="grdvProduct" AllowSorting="true" HeaderStyle-ForeColor="white" runat="server" AutoGenerateColumns="False" HorizontalAlign="Left"
                                                            HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" Width="725px" TabIndex="2">
                                                              <Columns>
                                                    
                                                <asp:TemplateField HeaderText="Product Name" SortExpression="PRODUCTNAME">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("PRODUCTNAME") %>' Width="200px"></asp:Label>
                                                <asp:HiddenField ID="hdMaintain" runat="server" Value='<%# Eval("MAINTAIN_BALANCE") %>' />
                                                 <asp:HiddenField ID="hdMaintainby" runat="server" Value='<%# Eval("MAINTAIN_BALANCE_BY") %>' />
                                                 
                                                 </ItemTemplate>
                                                      <ItemStyle Width="120px" Wrap="True" />
                                                 </asp:TemplateField>
                                                 
                                                  <asp:TemplateField HeaderText="Quantity" SortExpression="Qty" HeaderStyle-Wrap="false" >
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' ></asp:Label>
                                                 </ItemTemplate>
                                                      <HeaderStyle Wrap="False" />
                                                 </asp:TemplateField>
                                                 
                                                  <asp:TemplateField HeaderText="Rate" SortExpression="Rate" HeaderStyle-Wrap="false" >
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' ></asp:Label>
                                                 </ItemTemplate>
                                                      <HeaderStyle Wrap="False" />
                                                 </asp:TemplateField>
                                                 
                                                 <asp:TemplateField HeaderText="Tax" SortExpression="Tax" HeaderStyle-Wrap="false" >
                                                 <ItemTemplate>
                                                 <asp:Label ID="bllTax" runat="server" Text='<%# Eval("Tax") %>'></asp:Label>
                                                 </ItemTemplate>
                                                     <ItemStyle  Wrap="False" />
                                                 </asp:TemplateField>
                                                 
                                                 <asp:TemplateField HeaderText="Total">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                 </ItemTemplate>
                                                     <ItemStyle  Wrap="False" />
                                                 </asp:TemplateField>
                                                 
                                                
                                                
                                                <asp:TemplateField HeaderText="Warrnaty" SortExpression="Warranty" HeaderStyle-Wrap="false" >
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblWarrenty" runat="server" Text='<%# Eval("Warranty") %>'></asp:Label>
                                                 </ItemTemplate>
                                                     <ItemStyle Wrap="False" Width="60px" />
                                                 </asp:TemplateField>
                                                  
                                                  
                                                 <asp:TemplateField HeaderText="O/Warranty" SortExpression="WarrantyOnSite" HeaderStyle-Wrap="false" >
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblWarrentyOnsite" runat="server" Text='<%# Eval("WarrantyOnSite") %>'></asp:Label>
                                                 </ItemTemplate>
                                                     <ItemStyle Wrap=False />
                                                 </asp:TemplateField>
                                                 
                                                  <asp:TemplateField HeaderText="Delivery Date" SortExpression="DeliveryDate">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblDeliveryDt" runat="server" Text='<%# Eval("DeliveryDate") %>'></asp:Label>
                                                 </ItemTemplate>
                                                     <ItemStyle  Wrap=False  Width="60px" />
                                                 </asp:TemplateField>
                                                 
                                                 
                                                 <asp:TemplateField HeaderText="Godown Name" SortExpression="GodownName" HeaderStyle-Wrap="false"  >
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblGodownName" runat="server" Text='<%# Eval("GodownName") %>'></asp:Label>
                                                 </ItemTemplate>
                                                     <ItemStyle Width="100px" Wrap="True" />
                                                 </asp:TemplateField>
                                                 
                                                 
                                                 <asp:TemplateField HeaderText="Action" >
                                                                                         <ItemTemplate>
                                                                                            <asp:LinkButton ID="linkEdit" runat="server" CssClass="LinkButtons" Text="Edit"></asp:LinkButton>&nbsp;
                                                                                            <asp:LinkButton ID="linkDelete" runat="server" CssClass="LinkButtons" Text="Delete"></asp:LinkButton>
                                                                                            <asp:HiddenField ID="hdProductID" runat="server" Value='<%#Eval("PRODUCTID")%>' /> 
                                                                                            <asp:HiddenField ID="hdGodownID" runat="server" Value='<%#Eval("GodownID")%>' /> 
                                                                                            <asp:HiddenField ID="hdRowID" runat="server" Value='<%#Eval("TempRow")%>' />
                                                                                            
                                                                                            
                                                                                           </ItemTemplate>
                                                     <ItemStyle Wrap="False" />
                                                                                </asp:TemplateField>   
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                                </asp:GridView>
                                                                         </td>
                                                                         <td colspan="1" nowrap="nowrap">
                                                                         </td>
                                                                     </tr>
                                                                
                                                               
                                                                
                                                                
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="width: 100px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 136px">
                                                                        </td>
                                                                    <td style="width: 208px">
                                                                        </td>
                                                                    <td class="textbold" style="width: 92px">
                                                                        </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdPurchaseID" runat="server" /><asp:HiddenField ID="hdLogedByName" runat="server" />
                                                                        <asp:HiddenField ID="hdEmpID" runat="server" /><asp:HiddenField ID="hdDeleteFlag" runat="server" />
                                                                        <asp:HiddenField ID="hdTempXml" runat="server" />
                                                                        <asp:HiddenField ID="hdEditFlag" runat="server" /><asp:HiddenField ID="hdReadonlyFlag" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hdProductName" runat="server" />
                                                                        <asp:HiddenField ID="hdProductUniqe" runat="server" />
                                                                        <asp:HiddenField ID="hdGodownUnique" runat="server" />
                                                                        
                                                                        </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                     <tr>
                                                                         <td class="textbold" style="width: 100px">
                                                                         </td>
                                                                         <td class="textbold" style="width: 136px">
                                                                         </td>
                                                                         <td style="width: 180px">
                                                                         </td>
                                                                         <td class="textbold" style="width: 92px">
                                                                             Total Cost</td>
                                                                         <td>
                                                                             <asp:TextBox ID="txtTotalCost" runat="server" CssClass="textboxgrey" MaxLength="10" ReadOnly="True"
                                                                                 TabIndex="2" Width="160px"></asp:TextBox></td>
                                                                         <td>
                                                                         </td>
                                                                     </tr>
                                                                    </table>
                                                                        </asp:Panel>
                                                                    
                                                                       
                                                                    </td>
                                                                    <td class="center top " colspan="2" rowspan="1">
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server"  CssClass="button" Text="Save" Width="100px" TabIndex="3"  AccessKey="S"/>
                                                                    </td>
                                                                    </tr>
                                                                    <tr height="3px"></tr>
                                                                    
                                                                    <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnNew" runat="server"  CssClass="button" Text="New" Width="100px" TabIndex="3" AccessKey="N" />
                                                                    </td>
                                                                    </tr>
                                                                    <tr height="3px"></tr>
                                                                    
                                                                    <tr>
                                                                    <td style="height: 22px">
                                                                         <asp:Button ID="btnReset" runat="server"  CssClass="button" Text="Reset" Width="100px" TabIndex="3" AccessKey="R" />
                                                                    
                                                                    </td>
                                                                    </tr>
                                                                    </table>
                                                                       
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="center" colspan="2" rowspan="1">
                                                                    </td>
                                                                </tr>
                                                             
                                                              
                                                                
                                                                
                                                                <tr>
                                                                    <td  class="ErrorMsg" style="width:10%">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>                     &nbsp; &nbsp;                     </td>
                                                                    <td>
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
                    <asp:HiddenField ID="hdTabType" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
