<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_Equipment.aspx.vb" Inherits="Inventory_INVUP_Equipment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title> Add/Modify Equipment </title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet"/>
      <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    
    
    function IsValidDataValid(datavalue,type)
{
/*
type
1 Alphabets without space
2 Alphabets with space
3 Numeric without sign and dot
4 Numeric with sign and dot
5 Numeric with dot
6 Alphabets & Numeric
7 Alphabets & Numeric with Space
8 Phone Number 
9 Alphabete with ( ),-,& and Space 
13 Alphabets & Numeric with Space  and dot
*/
if(datavalue!='')
{
    datalength = datavalue.length;
    x = datavalue;
    flag=0;
    for(p=0;p < datalength;p++)
    {
        vAscii = x.charCodeAt(p) 
        if (type == 1)
        {
        if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122))
            {flag=1;}
        else
            {flag=0;break;}
        }

        if (type == 2)
        {
            if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) || (vAscii == 32))
                {flag=1;}
            else
                {flag=0;break;}
        }

        if (type == 3)
        {
            if((vAscii >= 48 && vAscii <= 57))
                {flag=1;}
            else
                {flag=0;break;}
        }

        if (type == 4)
        {
            if((vAscii >= 48 && vAscii <= 57) || (vAscii >= 45 && vAscii <= 46))
                {flag=1;}
            else
                {flag=0;break;}
        }

        if (type == 5)
        {
            if((vAscii >= 48 && vAscii <= 57) || (vAscii == 46))
                {flag=1;}
            else
                {flag=0;break;}
        }
        if (type == 6)
        {
            if((vAscii >= 48 && vAscii <= 57) || (vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122))
                {flag=1;}
            else
                {flag=0;break;}
        }
        if(type==7)
        {
            if((vAscii >= 48 && vAscii <= 57) || (vAscii >= 45 && vAscii <= 46) || (vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) || (vAscii == 32))
                {flag=1;}
            else
                {flag=0; break;}
        }
        if(type==8)
       {
      
            if((vAscii >= 48 && vAscii <= 57 ) ||vAscii == 43 || vAscii == 45)
               
                {flag=1;}
            else
                {flag=0;break;}
        }
       if(type==9)
       {
      
            if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) || (vAscii == 32) || (vAscii == 40) || (vAscii == 41) || (vAscii == 38) || (vAscii == 45)|| (vAscii >= 48 && vAscii <= 57))
                {flag=1;}
            else
                {flag=0;break;}
        }
        if (type == 10)
        {
            if((vAscii >= 50 && vAscii <= 57))
                {flag=1;}
            else
                {flag=0;break;}
        }
        if (type == 11)
        {
        if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) ||vAscii == 48)
            {flag=1;}
        else
            {flag=0;break;}
        }
        
        if (type == 12)     // Numeric Value with -ve 
        {
           if(vAscii >= 48 && vAscii <= 57)
                {flag=1;}
           else if (p==0 && x.charAt(p) == '-' && datalength > 1)
            {
               flag=1;
            }
            else
                {flag=0;break;}
        }
        
        
         if (type == 13)     //Alphabets & Numeric with Space  and dot
        {
           if((vAscii >= 48 && vAscii <= 57) || (vAscii >= 45 && vAscii <= 46) || (vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122) || (vAscii == 32))
                {flag=1;}
            else
                {flag=0; break;}
        }
        
    }
}
else
{
    flag=1;
}
if(flag==0)
{return false;}
else
{return true;}
}
     function  NewFunction()
       {    
           window.location="INVUP_Equipment.aspx?Action=I";
           return false;
       }  
    
     function ValidateEquipment()
    {
        // Validation of Equipment Category       
        var objEqpCatg,CountEqpCatg;
        CountEqpCatg=0;    


        var checkBoxList = document.getElementById("DlstEquipMentCategory");   
        //Get the number of checkboxes in the checkboxlist   
        var numCheckBoxItems = checkBoxList.cells.length;    
        
        //alert(numCheckBoxItems);
        //var list = document.getElementById('<%=DlstEquipMentCategory.ClientID%>').childNodes(0);
        var child ="";        
        
        for(i = 0; i < parseInt(numCheckBoxItems)-1 ; i++) 
        {
            //checkbox list id is 'CheckBoxList1' replace according id of checkboxlist 
            child =document.getElementById('DlstEquipMentCategory$'+ i) 
            if(child.checked) 
            {                     
              CountEqpCatg=1;
            }         
        }
        
        
        var strEGroup= document.getElementById("drpEquipmentGroup").value
        if (strEGroup =="CPU"  || strEGroup =="TFT"  || strEGroup =="LAP"  || strEGroup =="MON"  || strEGroup =="DPR"  || strEGroup =="TPR")
        {
            if (CountEqpCatg==0)
            {         
             document.getElementById("lblError").innerHTML="Equipment Category is mandatory.";
             document.getElementById("DlstEquipMentCategory").focus();
             return false;
            }
        }


       //end Validation of Equipment Category
       
       
        if (document.getElementById("txtEquipmentType").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="Equipment Code is mandatory.";
             document.getElementById("txtEquipmentType").focus();
             return false;
        }        
        
        if (document.getElementById("drpEquipmentGroup").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Equipment Group is mandatory.";
          document.getElementById("drpEquipmentGroup").focus();
          return false;
        }
         
        if (document.getElementById("txtDescription").value.trim()=="")
        {         
          document.getElementById("lblError").innerHTML="Description is mandatory.";
          document.getElementById("txtDescription").focus();
          return false;
        }
          if (document.getElementById("txtEquipmentType").value.trim().length>3)
        {
             document.getElementById("lblError").innerHTML="Please Enter valid Equipment Code."
             document.getElementById("txtEquipmentType").focus();
             return false;
        } 
        
                if (document.getElementById("txtConfiguration").value.trim().length>100)
        {
             document.getElementById("lblError").innerHTML="Configuration can't be greater than 100 characters."
             document.getElementById("txtConfiguration").focus();
             return false;
        }  
      
         
       if(document.getElementById("txtCpuSpeed").value!='')
      {
          var txtVal=document.getElementById("txtCpuSpeed").value;
          if(IsDataValid(txtVal,7)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid Cpu Speed";
              document.getElementById("txtCpuSpeed").focus();
              return false;
          }    
          
//            if(parseInt(txtVal.trim())>32767)
//            {
//               document.getElementById("lblError").innerHTML="Please Enter valid Cpu Speed";
//               document.getElementById("txtCpuSpeed").focus();
//                return false;            
//            }
              
      
      }
//       if (document.getElementById("txtCpuSpeed").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid CpuSpeed."
//             document.getElementById("txtCpuSpeed").focus();
//             return false;
//        }  
              if(document.getElementById("txtRAM").value!='')
      {
          var txtVal=document.getElementById("txtRAM").value;
          if(IsDataValid(txtVal,7)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid RAM.";
              document.getElementById("txtRAM").focus();
              return false;
          }

//         if(parseInt(txtVal.trim())>32767)
//            {
//             document.getElementById("lblError").innerHTML="Please Enter valid RAM."
//             document.getElementById("txtRAM").focus();
//             return false;
//            
//            }

      
      }
//       if (document.getElementById("txtRAM").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid RAM."
//             document.getElementById("txtRAM").focus();
//             return false;
//        }  

            if(document.getElementById("txtVRAM").value!='')
      {
          var txtVal=document.getElementById("txtVRAM").value;
          if(IsDataValid(txtVal,7)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid VRAM.";
              document.getElementById("txtVRAM").focus();
              return false;
          }
          
//          if(parseInt(txtVal.trim())>32767)
//            {
//             document.getElementById("lblError").innerHTML="Please Enter valid VRAM."
//            document.getElementById("txtVRAM").focus();
//             return false;
//            
//            }
//      
      }
     
//       if (document.getElementById("txtVRAM").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid VRAM."
//             document.getElementById("txtVRAM").focus();
//             return false;
//        }  

  if(document.getElementById("txtHDD").value!='')
      {
          var txtVal=document.getElementById("txtHDD").value;
          if(IsDataValid(txtVal,7)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid HDD.";
              document.getElementById("txtHDD").focus();
              return false;
          }
          
//            if(parseInt(txtVal.trim())>32767)
//            {
//              document.getElementById("lblError").innerHTML="Please Enter valid HDD."
//              document.getElementById("txtHDD").focus();
//             return false;
//            
//            }
      
      }
//       if (document.getElementById("txtHDD").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid HDD."
//             document.getElementById("txtHDD").focus();
//             return false;
//        }  
        
            if(document.getElementById("txtPrinterSpeed").value!='')
      {
          var txtVal=document.getElementById("txtPrinterSpeed").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid Printer Speed.";
              document.getElementById("txtPrinterSpeed").focus();
              return false;
          }
             if(parseInt(txtVal.trim())>32767)
            {
               document.getElementById("lblError").innerHTML="Please Enter valid Printer Speed."
               document.getElementById("txtPrinterSpeed").focus();
             return false;
            
            }
      
      
      }
//      if (document.getElementById("txtPrinterSpeed").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid PrinterSpeed."
//             document.getElementById("txtPrinterSpeed").focus();
//             return false;
//        }  
       
         if(document.getElementById("txtPSpeedMeasure").value!='')
      {
          var txtVal=document.getElementById("txtPSpeedMeasure").value;
//          if(IsDataValid(txtVal,11)==false)
//          {
//              document.getElementById("lblError").innerHTML="Please Enter valid PSpeed Measure.";
//              document.getElementById("txtPSpeedMeasure").focus();
//              return false;
//          }
      
      }
//      if (document.getElementById("txtPSpeedMeasure").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid PSpeed Measure."
//             document.getElementById("txtPSpeedMeasure").focus();
//             return false;
//        } 
         
       
       
          if(document.getElementById("txtMonitorSize").value!='')
      {
          var txtVal=document.getElementById("txtMonitorSize").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid Monitor Size.";
              document.getElementById("txtMonitorSize").focus();
              return false;
          }
          
           if(parseInt(txtVal.trim())>32767)
            {
               document.getElementById("lblError").innerHTML="Please Enter valid Monitor Size."
            document.getElementById("txtMonitorSize").focus();
             return false;
            
            }
      
      }
//      if (document.getElementById("txtMonitorSize").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid MonitorSize."
//             document.getElementById("txtMonitorSize").focus();
//             return false;
//        }  
//        
        
     
     
      
      
              if(document.getElementById("txtModemSPeed").value!='')
      {
          var txtVal=document.getElementById("txtModemSPeed").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid Modem SPeed.";
              document.getElementById("txtModemSPeed").focus();
              return false;
          }
          
             if(parseInt(txtVal.trim())>32767)
            {
             document.getElementById("lblError").innerHTML="Please Enter valid Modem SPeed."
             document.getElementById("txtModemSPeed").focus();
             return false;
            
            }
      
      }
      
       
//       if (document.getElementById("txtModemSPeed").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid ModemSPeed."
//             document.getElementById("txtModemSPeed").focus();
//             return false;
//        }  
//     
       
           if(document.getElementById("txtLanCardType").value!='')
      {
          var txtVal=document.getElementById("txtLanCardType").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid LanCard Type.";
              document.getElementById("txtLanCardType").focus();
              return false;
          }
              if(parseInt(txtVal.trim(),10)>32767  || parseInt(txtVal.trim(),10)<0)
            {
               document.getElementById("lblError").innerHTML="Please Enter valid LanCard Type."
             document.getElementById("txtLanCardType").focus();
             return false;
            
            }
      
      }
      
//       if (document.getElementById("txtLanCardType").value.trim().length>1)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid LanCard Type."
//             document.getElementById("txtLanCardType").focus();
//             return false;
//        }  
      
        if(document.getElementById("txtLanCardSpeed").value!='')
      {
          var txtVal=document.getElementById("txtLanCardSpeed").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid LanCard Speed.";
              document.getElementById("txtLanCardSpeed").focus();
              return false;
          }
          
              if(parseInt(txtVal.trim(),10)>32767  || parseInt(txtVal.trim(),10)<0)
            {
              document.getElementById("lblError").innerHTML="Please Enter valid LanCard Speed."
               document.getElementById("txtLanCardSpeed").focus();
             return false;
            
            }
      
      }
     
//        if (document.getElementById("txtLanCardSpeed").value.trim().length>1)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid LanCard Speed."
//             document.getElementById("txtLanCardSpeed").focus();
//             return false;
//        }  
          if(document.getElementById("txtWanCardType").value!='')
      {
          var txtVal=document.getElementById("txtWanCardType").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid WanCard Type.";
              document.getElementById("txtWanCardType").focus();
              return false;
          }
          
               if(parseInt(txtVal.trim(),10)>32767  || parseInt(txtVal.trim(),10)<0)
            {
               document.getElementById("lblError").innerHTML="Please Enter valid WanCard Type."
               document.getElementById("txtWanCardType").focus();
             return false;
            
            }
      
      }
      
      
      
//        if (document.getElementById("txtWanCardType").value.trim().length>1)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid WanCard Type."
//             document.getElementById("txtWanCardType").focus();
//             return false;
//        }  
//        
         if(document.getElementById("txtCDRSpeed").value!='')
      {
          var txtVal=document.getElementById("txtCDRSpeed").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid CDR Speed.";
              document.getElementById("txtCDRSpeed").focus();
              return false;
          }
          
               if(parseInt(txtVal.trim(),10)>32767  || parseInt(txtVal.trim(),10)<0)
            {
                document.getElementById("lblError").innerHTML="Please Enter valid CDR Speed."
               document.getElementById("txtCDRSpeed").focus();
               return false;
          
            }
      
      
      }
          
      
//            if (document.getElementById("txtCDRSpeed").value.trim().length>2)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid CDR Speed."
//             document.getElementById("txtCDRSpeed").focus();
//             return false;
//        }  
        
              if(document.getElementById("txtPCISlots").value!='')
      {
          var txtVal=document.getElementById("txtPCISlots").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid PCISlots.";
              document.getElementById("txtPCISlots").focus();
              return false;
          }
          
              if(parseInt(txtVal.trim(),10)>32767  || parseInt(txtVal.trim(),10)<0)
            {
                 document.getElementById("lblError").innerHTML="Please Enter valid PCISlots."
                document.getElementById("txtPCISlots").focus();
               return false;
          
            }
      
      }
      
//       if (document.getElementById("txtPCISlots").value.trim().length>1)
//        {
//             document.getElementById("lblError").innerHTML="Please Enter valid PCISlots."
//             document.getElementById("txtPCISlots").focus();
//             return false;
//        }  
       
       if(document.getElementById("txtISASlots").value!='')
      {
          var txtVal=document.getElementById("txtISASlots").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid ISA Slots.";
              document.getElementById("txtISASlots").focus();
              return false;
          }
              if(parseInt(txtVal.trim(),10)>32767  || parseInt(txtVal.trim(),10)<0)
            {
                 document.getElementById("lblError").innerHTML="Please Enter valid ISA Slots."
                 document.getElementById("txtISASlots").focus();
               return false;
          
            }
      
      
      }
      
      
       if(document.getElementById("txtSegExpected").value!='')
      {
          var txtVal=document.getElementById("txtSegExpected").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid Segment Expected.";
              document.getElementById("txtSegExpected").focus();
              return false;
          }
              if(parseInt(txtVal.trim(),10)>32767  || parseInt(txtVal.trim(),10)<0)
            {
                 document.getElementById("lblError").innerHTML="Please Enter valid Segment Expected."
                 document.getElementById("txtSegExpected").focus();
               return false;
          
            }     
      
      }
        if (  document.getElementById("txtUnitCost").value!="")
         {
           if(IsDataValid(document.getElementById("txtUnitCost").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="Unit Cost is Numeric";
            document.getElementById("txtUnitCost").focus();
            return false;
            } 
         }
         
          
          
          
          
             if (document.getElementById("txtRemarks").value.trim().length>300)
        {
             document.getElementById("lblError").innerHTML="Remarks can't be greater than 300 characters."
             document.getElementById("txtRemarks").focus();
             return false;
        }  
      
          
    
    }
 
    </script>
</head>
<body >
    <form id="form1" runat="server" defaultfocus="txtEquipmentType" defaultbutton="btnSave">
    
    <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Inventory-&gt;</span><span class="sub_menu">Equipment</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Manage Equipment</td>
                        </tr>
                          <tr>
                            <td valign="top" style="height: 366px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                           
                                            <td class="gap" colspan="7" style="text-align: center; height: 18pt;">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                           </tr> 
                                        <tr>
                                            <td style="width: 18%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px">
                                                Category Name<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">*</span></strong></td>
                                            <td  colspan ="4" >
                                                <%--<asp:DropDownList ID="DlstEquipMentCategory" runat="server" CssClass="dropdownlist" TabIndex="1" Width="428px">
                                                </asp:DropDownList></td>--%>                                                
                                                <%--<div id="DivEquipmentCatg" runat ="server"  style="height:75px;width:430px;overflow:scroll;border:1px">--%>
                                                <asp:Panel id="DivEquipmentCatg" runat ="server"   BorderStyle="Solid" BorderColor="ActiveBorder"     Width="420px" Height="75px" ScrollBars="vertical">
                                                    <asp:CheckBoxList ID="DlstEquipMentCategory" CssClass="dropdownlist"   RepeatDirection="Horizontal" RepeatColumns="2"   runat="server" TabIndex="1" Width="400px">
                                                    </asp:CheckBoxList> 
                                                </asp:Panel>
                                                <%--</div>--%>
                                           
                                            <td style="width: 27%; height: 18px">
                                            </td>
                                        </tr>
                                             <tr>
                                                <td style="width:18%; height: 18px;">
                                                </td>
                                                <td class="textbold" style="width:16%; height: 18px;">
                                                    Equipment Code<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">*</span></strong></td>
                                                <td style="width: 15%; height: 18px;">
                                                    <asp:TextBox ID="txtEquipmentType" runat="server" CssClass="textbox" MaxLength="3"
                                                        ReadOnly="false" Style="left: 0px; position: relative; top: 0px" TabIndex="1"
                                                        Width="152px"></asp:TextBox></td>
                                                 <td style="width: 1%; height: 18px;">                                                                             
                                                </td>                                                                            
                                                 <td style="width: 14%; height: 18px;" class="textbold">
                                                     Equipment Group&nbsp;<span style="font-size: 8pt; color: #de2418; font-family: Verdana"><strong>*</strong></span></td>
                                                 <td style="width: 17%; height: 18px;">
                                                     <asp:DropDownList ID="drpEquipmentGroup" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                         position: relative; top: 0px" TabIndex="1" Width="128px">
                                                     </asp:DropDownList></td>    
                                                <td style="width: 27%; height: 18px;">
                                                      <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save"  TabIndex="2"   /></td>
                                            </tr>              
                                            <tr>
                                                <td style="width:18%; height: 18px;">
                                                </td>
                                                <td class="textbold" style="width:16%; height: 18px;" >
                                                    Description<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">*</span></strong></td>
                                                <td style="width: 43%; height: 18px;" colspan="4">
                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" MaxLength="50" TabIndex="1" Width="424px"></asp:TextBox></td>   
                                                <td style="width: 27%; height: 18px;"  ><asp:Button ID="btn_New" CssClass="button" runat="server" Text="New" TabIndex="2"  /></td>
                                            </tr>
                                              <tr>
                                                <td style="width:18%" valign ="top">
                                                </td>
                                                <td class="textbold" style="width:16%"  valign ="top">
                                                    Configuration</td>
                                                <td style="width: 43%"  colspan="4" valign ="top" >
                                                    <asp:TextBox id="txtConfiguration" tabIndex=1 runat="server" CssClass="textbox" MaxLength="25" Width="424px" Height="42px" TextMode="MultiLine"></asp:TextBox></td> 
                                               
                                                <td style="width: 27%"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" style="position: relative; left: 0px; top: -14px;" /></td>
                                            </tr>
                                                <tr>
                                                <td style="width:18%; height: 18px;">
                                                </td>
                                                <td class="textbold" style="width:16%; height: 18px;">
                                                    CPU Speed</td>
                                                <td style="width: 15%; height: 18px;">
                                                    <asp:TextBox ID="txtCpuSpeed" runat="server" CssClass="textbox" MaxLength="6" Style="position: relative"
                                                        TabIndex="1" Width="122px"></asp:TextBox></td>
                                                 <td style="width: 1%; height: 18px;">                                                                             
                                                </td>                                                                            
                                                 <td style="width: 14%; height: 18px;" class="textbold">
                                                     RAM&nbsp;</td>
                                                 <td style="width: 17%; height: 18px;">
                                                     <asp:TextBox ID="txtRAM" runat="server" CssClass="textbox" MaxLength="6" TabIndex="1"
                                                         Width="122px"></asp:TextBox>&nbsp;</td>    
                                                <td style="width: 27%; height: 18px;">
                                                      </td>
                                            </tr>
                                                <tr>
                                                <td style="width:18%; height: 18px;">
                                                </td>
                                                <td class="textbold" style="width:16%; height: 18px;" >
                                                    VRAM</td>
                                                <td style="width: 15%; height: 18px;">
                                                    <asp:TextBox ID="txtVRAM" runat="server" CssClass="textbox" MaxLength="6" Style="position: relative"
                                                        TabIndex="1" Width="122px"></asp:TextBox></td>
                                                 <td style="width: 1%; height: 18px;">                                                                             
                                                </td>                                                                            
                                                 <td style="width: 14%; height: 18px;" class="textbold">
                                                     HDD</td>
                                                 <td style="width: 17%; height: 18px;">
                                                    <asp:TextBox ID="txtHDD" runat="server" CssClass="textbox" MaxLength="6" TabIndex="1" Width="122px"></asp:TextBox></td>    
                                                <td style="width: 27%; height: 18px;">
                                                    </td>
                                            </tr>
                                             <tr>
                                                <td style="width:18%; height: 18px;">
                                                </td>
                                                <td class="textbold" style="width:16%; height: 18px;">
                                                    Printer Speed</td>
                                                <td style="width: 15%; height: 18px;">
                                                    <asp:TextBox ID="txtPrinterSpeed" runat="server" CssClass="textbox" MaxLength="6" TabIndex="1" Width="122px"></asp:TextBox></td>
                                                 <td style="width: 1%; height: 18px;">                                                                             
                                                </td>                                                                            
                                                 <td style="width: 14%; height: 18px;" class="textbold">
                                                     PSpeedMeasure</td>
                                                 <td style="width: 17%; height: 18px;">
                                                  <asp:TextBox id="txtPSpeedMeasure" tabIndex=1 runat="server" CssClass="textbox" MaxLength="100" Width="122px"></asp:TextBox>
                                                </td>    
                                                <td style="width: 27%; height: 18px;">
                                                      </td>
                                            </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px;">
                                                Monitor Size</td>
                                            <td style="width: 15%; height: 18px;">
                                                <asp:TextBox ID="txtMonitorSize" runat="server" CssClass="textbox" MaxLength="6"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px;">
                                                Modem Speed</td>
                                            <td style="width: 17%; height: 18px;">
                                                <asp:TextBox ID="txtModemSPeed" runat="server" CssClass="textbox" MaxLength="6"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 27%; height: 18px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px;">
                                                Lan Card Type</td>
                                            <td style="width: 15%; height: 18px;">
                                                <asp:TextBox ID="txtLanCardType" runat="server" CssClass="textbox" MaxLength="30"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px;">
                                                Lan Card Speed</td>
                                            <td style="width: 17%; height: 18px;">
                                                <asp:TextBox ID="txtLanCardSpeed" runat="server" CssClass="textbox" MaxLength="30"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 27%; height: 18px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px;">
                                                Wan Card Type</td>
                                            <td style="width: 15%; height: 18px;">
                                                <asp:TextBox ID="txtWanCardType" runat="server" CssClass="textbox" MaxLength="30"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px;">
                                                CDR Speed</td>
                                            <td style="width: 17%; height: 18px;">
                                                <asp:TextBox ID="txtCDRSpeed" runat="server" CssClass="textbox" MaxLength="6" Style="position: relative"
                                                    TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 27%; height: 18px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px;">
                                                ISA Slots</td>
                                            <td style="width: 15%; height: 18px;">
                                                <asp:TextBox ID="txtISASlots" runat="server" CssClass="textbox" MaxLength="6" Style="position: relative"
                                                    TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px;">
                                                Maintain Balance By Qty</td>
                                            <td style="width: 17%; height: 18px;">
                                                <asp:CheckBox  TabIndex=1 ID="chkMaintainBy" runat="server" Style="position: relative" /></td>
                                            <td style="width: 27%; height: 18px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px;">
                                                PCI Slots</td>
                                            <td style="width: 15%; height: 18px;">
                                                <asp:TextBox ID="txtPCISlots" runat="server" CssClass="textbox" MaxLength="30" Style="position: relative"
                                                    TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px;">
                                                Maintain Balance</td>
                                            <td style="width: 17%; height: 18px;">
                                                <asp:CheckBox ID="chkMaintainBal" TabIndex=1 runat="server" Style="position: relative" /></td>
                                            <td style="width: 27%; height: 18px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px">
                                                Segment Expected</td>
                                            <td style="width: 15%; height: 18px">
                                                <asp:TextBox ID="txtSegExpected" runat="server" CssClass="textbox" MaxLength="5" Style="position: relative"
                                                    TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px">
                                                Unit Cost (IN)</td>
                                            <td style="width: 17%; height: 18px">
                                                <asp:TextBox ID="txtUnitCost" runat="server" CssClass="textboxgrey" MaxLength="6" Style="position: relative"
                                                    TabIndex="1" Width="122px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="width: 27%; height: 18px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px">
                                                Unit Cost&nbsp; (NP)</td>
                                            <td style="width: 15%; height: 18px">
                                                <asp:TextBox ID="txtNPUnitCost" runat="server" CssClass="textboxgrey" MaxLength="6" ReadOnly="True"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px">
                                                Unit Cost&nbsp; (LK)</td>
                                            <td style="width: 17%; height: 18px">
                                                <asp:TextBox ID="txtLKUnitCost" runat="server" CssClass="textboxgrey" MaxLength="6" ReadOnly="True"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 27%; height: 18px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px">
                                                Unit Cost&nbsp; (BD)</td>
                                            <td style="width: 15%; height: 18px">
                                                <asp:TextBox ID="txtBDUnitCost" runat="server" CssClass="textboxgrey" MaxLength="6" ReadOnly="True"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px">
                                                Unit Cost&nbsp; (BT)</td>
                                            <td style="width: 17%; height: 18px">
                                                <asp:TextBox ID="txtBTUnitCost" runat="server" CssClass="textboxgrey" MaxLength="6" ReadOnly="True"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 27%; height: 18px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 16%; height: 18px">
                                                Unit Cost&nbsp; (ML)</td>
                                            <td style="width: 15%; height: 18px">
                                                <asp:TextBox ID="txtMLUnitCost" runat="server" CssClass="textboxgrey" MaxLength="6" ReadOnly="True"
                                                    Style="position: relative" TabIndex="1" Width="122px"></asp:TextBox></td>
                                            <td style="width: 1%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px">
                                            </td>
                                            <td style="width: 17%; height: 18px">
                                            </td>
                                            <td style="width: 27%; height: 18px">
                                            </td>
                                        </tr>
                                             <tr>
                                                <td style="width:18%" valign ="top">
                                                </td>
                                                <td class="textbold" style="width:16%"  valign ="top">
                                                    Remarks</td>
                                                <td style="width: 43%"  colspan="4" valign ="top" >
                                                    <asp:TextBox id="txtRemarks" tabIndex=1 runat="server" CssClass="textbox" MaxLength="25" Width="424px" Height="42px" TextMode="MultiLine"></asp:TextBox></td> 
                                               
                                                <td style="width: 27%">
                                                     </td>
                                            </tr>
                                        <tr>
                                            <td>
                                            <asp:HiddenField ID="hdID" runat="server" />
                                            </td>
                                            <td colspan="5" class="ErrorMsg">
                                                Field Marked * are Mandatory</td>
                                            <td >
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

