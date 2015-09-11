<%@ Page MaintainScrollPositionOnPostback ="true"  Language="VB" EnableEventValidation = "false" AutoEventWireup="false" CodeFile="PRDSR_BIDT.aspx.vb" Inherits="Productivity_PRDSR_BIDT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Productivity::Search 1 A Productivity</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript">      
      function ActDeAct()
     {
        {debugger;}
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9 &&  whichASC !=13 && whichASC!=18)
        {
        document.getElementById("hdAgencyName").value="";
        document.getElementById("ChkGrpProductivity").disabled=true;
        document.getElementById("ChkGrpProductivity").checked=false;
        }
    	
     }
       function PopupPage(id)
         {
         var type;        
         if (id=="2")
         {
                type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	
         } 
     }
             function SelectCrsExcAvgFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PRD_BIDT_CRSDetails.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
             function SelectCrsExcMonFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PRD_BIDT_CRSDetails.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
             function SelectCrsAvgFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PRD_BIDT_CRSDetails.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
             function SelectMonCrsFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PRD_BIDT_CRSDetails.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
     
     
     
     
          function SelectMonBreakFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PDSR_BIDTBreakUp.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
           function SelectBreakAvgFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PDSR_BIDTBreakUp.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
           function SelectBreakExcMonFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PDSR_BIDTBreakUp.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
           function SelectBreakExcAvgFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PDSR_BIDTBreakUp.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
     
     
     
     
      function SelectDetailsExcMonFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {

                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PRD_BIDT_Details.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
      function URLEncode (clearString) 
        {
          var output = '';
          var x = 0;
          clearString = clearString.toString();
          var regex = /(^[a-zA-Z0-9_.]*)/;
          while (x < clearString.length) {
            var match = regex.exec(clearString.substr(x));
            if (match != null && match.length > 1 && match[1] != '') {
    	        output += match[1];
              x += match[1].length;
            } else {
              if (clearString[x] == ' ')
                output += '+';
              else {
                var charCode = clearString.charCodeAt(x);
                var hexVal = charCode.toString(16);
                output += '%' + ( hexVal.length < 2 ? '0' : '' ) + hexVal.toUpperCase();
              }
              x++;
            }
          }
          return output;
        }

      function SelectDetailsExcAvgFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {
//                 alert(FMonth);               
//                 alert(TMonth);
//                 alert(FYear);
//                 alert(TYear);
//                 alert(Lcode);
//                 alert(Aoff);
//                 alert(GrData);
//                 alert(UseOrig);
//                 alert(ResId);
//                 alert(Air);
//                 alert(Car);
//                 alert(Hotel);               
//                 alert(Insurance);
//                 alert(LimAoff);
//                 alert(LimReg);
//                 alert(LimOwnOff);
//                 alert(Agency);
//                 
//                     alert(Add);
//                       alert(City);
//                       alert(Country);
                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PRD_BIDT_Details.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
     
     
     function SelectMonDetailsFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {
//                 alert(FMonth);               
//                 alert(TMonth);
//                 alert(FYear);
//                 alert(TYear);
//                 alert(Lcode);
//                 alert(Aoff);
//                 alert(GrData);
//                 alert(UseOrig);
//                 alert(ResId);
//                 alert(Air);
//                 alert(Car);
//                 alert(Hotel);               
//                 alert(Insurance);
//                 alert(LimAoff);
//                 alert(LimReg);
//                 alert(LimOwnOff);
                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PRD_BIDT_Details.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
      function SelectDetailsAvgFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {
//                 alert(FMonth);               
//                 alert(TMonth);
//                 alert(FYear);
//                 alert(TYear);
//                 alert(Lcode);
//                 alert(Aoff);
//                 alert(GrData);
//                 alert(UseOrig);
//                 alert(ResId);
//                 alert(Air);
//                 alert(Car);
//                 alert(Hotel);               
//                 alert(Insurance);
//                 alert(LimAoff);
//                 alert(LimReg);
//                 alert(LimOwnOff);
                  var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PRD_BIDT_Details.aspx?Popup=T&" + parameter;
              
   	            window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
     function SelectProdutivity()
     {
      
       var objdrpProductivity =document.getElementById('drpProductivity').value;
       var objtxtFrom =document.getElementById('txtFrom');
       var objtxtTo =document.getElementById('txtTo');       
       if (objdrpProductivity=='') 
       {
          objtxtFrom.disabled=true;
          objtxtTo.disabled=true;
          objtxtFrom.value='0';
          objtxtTo.value='0';
          objtxtFrom.className='textboxgrey';
          objtxtTo.className='textboxgrey';
       } 
    else if (objdrpProductivity=='7') 
       {
         
          objtxtFrom.disabled=false;
          objtxtTo.disabled=false;
          objtxtFrom.value='0';
          objtxtTo.value='0';
          objtxtFrom.className='textbox';
          objtxtTo.className='textbox';  
       }   
       else 
       {
          objtxtFrom.disabled=false;
          objtxtTo.disabled=true;
          objtxtFrom.value='0';
          objtxtTo.value='';  
          objtxtFrom.className='textbox';
          objtxtTo.className='textboxgrey';   
       }
       
     }
     function ShowAvg()
     {
       var objChkNewFormat =document.getElementById('ChkNewFormat');
     // var objChkExTrans =document.getElementById('ChkExTrans');
       var objDivShowAvg =document.getElementById('DivShowAvg');  
       var objRdShowAvg =document.getElementById('RdShowAvg');  
       var objdivlbl =document.getElementById('divlbl');
       var objDivABookDetails =document.getElementById('DivABookDetails');
       var objRdShowMon =document.getElementById('RdShowMon');       
       
       if (objChkNewFormat.checked==true) 
       {        
          objDivShowAvg.className="displayNone";
          objRdShowAvg.checked=false;
          //objChkExTrans.checked=false;
       }
       else
       {        
         objDivShowAvg.className="textbox";
         objRdShowAvg.checked=false;
       }
        objdivlbl.className="textbox";
        objDivABookDetails.className="textbox";
        objRdShowMon.checked=true;
      
            
     }
//     function Show1ABooking()
//     {      
//       var objChkExTrans =document.getElementById('ChkExTrans');
//       var objChkNewFormat =document.getElementById('ChkNewFormat');
//       var objdivlbl =document.getElementById('divlbl');
//       var objDivABookDetails =document.getElementById('DivABookDetails');
//       var objDivShowAvg =document.getElementById('DivShowAvg');  
//        if (objChkExTrans.checked==true) 
//       {     
//        objChkNewFormat.checked=false;
//        objdivlbl.className="displayNone";
//        objDivABookDetails.className="displayNone";      
//       }
//       else
//       {
//         objdivlbl.className="textbox";
//         objDivABookDetails.className="textbox";  
//         objChkNewFormat.checked=true;
//       }
//       objDivShowAvg.className="textbox";
//      
//     }
     function ResetSelectedValueAfterSearch()     
     {
     //ActDeAct();
       //SelectProdutivity();
     //  ShowAvg();
      // Show1ABooking();
     
     }
     function CheckValidation()
     {
//            if (document.getElementById("hdAgencyName").value=="")
//              {
//                 if (document.getElementById("ChkGrpProductivity").checked==true)
//                   { 
//                     document.getElementById('lblError').innerHTML="Group Productivity Can't be Checked Without Location Code.";             
//                     return false;
//                   }              
//              }
  
  
           if(document.getElementById('<%=txtLcode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtLcode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Lcode.';             
                    document.getElementById("txtLcode").focus();
                    return false;
                 }                  
              }
              
              if(document.getElementById('<%=txtChainCode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtChainCode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Chain Code.';             
                    document.getElementById("txtChainCode").focus();
                    return false;
                 }                  
              }
              
              
           if(document.getElementById('<%=txtFrom.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtFrom").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtFrom").focus();
                    return false;
                 }                  
              }
               if(document.getElementById('<%=txtTo.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtTo").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtTo").focus();
                    return false;
                 }                  
              }
                 if(parseInt(document.getElementById("txtFrom").value) >parseInt(document.getElementById("txtTo").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Productivity range is not valid.';          
                    document.getElementById("txtFrom").focus();
                    return false;
                    } 
                    
                      if(parseInt(document.getElementById("drpYearFrom").value) >parseInt(document.getElementById("drpYearTo").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
                   
                   if(parseInt(document.getElementById("drpYearTo").value)- parseInt(document.getElementById("drpYearFrom").value)>4)
                    {                   
                    document.getElementById("lblError").innerHTML='Maximum number of years should be 4 years.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
              
     }
          function gotops(ddlname)
         {    
             if (event.keyCode==46 )
             {
                document.getElementById(ddlname).selectedIndex=0;
                SelectProdutivity();
             }
         }     
    </script>
</head>
<body onload  ="ResetSelectedValueAfterSearch();"  >
    <form id="form1"  runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch"  >    
     <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Productivity -&gt;</span><span class="sub_menu">1A Productivity Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                              <table>
                                <tr>
                                  <td style ="width:840px;" >1A Productivity
                                  </td>
                                  <td></td>
                                </tr>
                              </table>
                                </td>
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
                                                        <td style="width:3%">
                                                        </td>
                                                        <td class="textbold" style="width:13%">
                                                            Country</td>
                                                        <td style="width: 20%">
                                                           <asp:DropDownList ID="dlstCountry" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="1"></asp:DropDownList></td>
                                                         <td class="textbold" style="width: 2%">
                                                            </td>
                                                             <td class="textbold" style="width: 20%">
                                                            City</td>                                                      
                                                        <td class="textbold" style="width: 23%">
                                                        <asp:DropDownList ID="dlstCity"  runat="server" CssClass="dropdownlist" Width="155px" TabIndex="1"></asp:DropDownList></td>
                                                        <td style="width: 6%">
                                                        </td>
                                                        <td class="left" style="width: 15%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"  AccessKey="A"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:3%">
                                                        <input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" /></td>
                                                        <td class="textbold" style="width:13%"> Agency Name</td>                                                                               
                                                        <td colspan="4" style="width:63%"><asp:TextBox ID="txtAgencyName"    runat="server" CssClass="textbox" MaxLength="50" TabIndex="1" Width="507px"></asp:TextBox>
                                                            <img id="Img2" runat="server"   tabindex="1" alt="" onclick="PopupPage(2)"  src="../Images/lookup.gif" style="cursor:pointer;"  /></td>
                                                        <td style="width:6%">
                                                            </td>
                                                        <td style="width: 15%;" class="left"><asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="2"  AccessKey="E"/></td>
                                                    </tr>  
                                                     <tr>
                                                        <td style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:13%;">
                                                            Group Productivity</td>
                                                        <td style="width: 20%; ">
                                                            <asp:CheckBox ID="ChkGrpProductivity" runat="server" CssClass="textbox" TabIndex="1"    /></td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 20%; ">
                                                            </td>    
                                                        <td class="textbold" style="width: 23%; ">    </td>
                                                        <td style="width: 6%; ">
                                                        </td>
                                                        <td class="left" style="width: 15%; "><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="R" /></td>
                                                    </tr>
                                                     <tr>
                                                        <td style="width:3%">
                                                        </td>
                                                        <td class="textbold" style="width:13%">
                                                            Agency Status</td>
                                                        <td style="width: 20%">
                                                           <asp:DropDownList ID="drpAgencyStatus" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="1"></asp:DropDownList></td>
                                                          <td class="textbold" style="width: 2%">
                                                            </td>
                                                             <td class="textbold" style="width: 20%">
                                                            Responsible Staff</td>    
                                                        <td class="textbold" style="width: 23%">
                                                        <asp:DropDownList ID="drpResStaff" runat="server" CssClass="dropdownlist" Width="155px" TabIndex="1"></asp:DropDownList></td>
                                                        <td style="width: 6%">
                                                        </td>
                                                        <td class="center" style="width: 15%">
                                                            </td>
                                                    </tr>
                                                                     <tr>
                                                                         <td style="width: 3%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 13%">
                                                                             LCode</td>
                                                                         <td style="width: 20%">
                                                                             <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1"
                                                                                 Width="158px"></asp:TextBox></td>
                                                                         <td class="textbold" style="width: 2%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 20%">
                                                                             Chain Code</td>
                                                                         <td class="textbold" style="width: 23%">
                                                                             <asp:TextBox ID="txtChainCode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1"
                                                                                 Width="150px"></asp:TextBox></td>
                                                                         <td style="width: 6%">
                                                                         </td>
                                                                         <td class="center" style="width: 15%">
                                                                         </td>
                                                                     </tr>
                                                     <tr>
                                                        <td style="width:3%">
                                                        </td>
                                                        <td class="textbold" style="width:13%">
                                                            Productivity</td>
                                                        <td style="width: 20%">
                                                            <asp:DropDownList ID="drpProductivity" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="1">
                                                                <asp:ListItem Selected="True">--All--</asp:ListItem>
                                                                <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                <asp:ListItem Value="7">Between</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;</td>
                                                         <td  style="width: 2%"></td>       
                                                         <td  style="width: 20%"><asp:TextBox ID="txtFrom" runat="server" CssClass="textboxgrey" MaxLength="9" TabIndex="1"
                                                                Width="150px" Enabled="False">0</asp:TextBox></td>   
                                                          <td  style="width: 23%"> <asp:TextBox ID="txtTo" runat="server" CssClass="textboxgrey" MaxLength="9" TabIndex="1"
                                                                Width="150px" Enabled="False">0</asp:TextBox></td>        
                                                        <td style="width: 21%"    colspan ="2" valign="top">
                                                           </td>
                                                     
                                                    </tr>
                                                     <tr>
                                                        <td style="width:3%">
                                                        </td>
                                                        <td class="textbold" style="width:13%">
                                                            1 A Office</td>
                                                        <td style="width: 20%">
                                                           <asp:DropDownList ID="drp1AOffice" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="1"></asp:DropDownList></td>
                                                             <td  style="width: 2%"  ></td>
                                                            <td class="textbold" style="width: 20%"  valign ="top" >
                                                                <asp:RadioButton ID="RdShowMon" runat="server" CssClass="textbox" Text="Show Monthly Breakup" Width="156px" GroupName="Avg" Checked="True" TabIndex="1" /></td>                                                          
                                                          <td class="textbold" style="width: 23%" rowspan="4" valign ="top">
                                                          &nbsp;<asp:CheckBox ID="ChkOrigBook" runat ="server" Text="NBS " Checked="False" CssClass="textbox" TabIndex="1"   />                                                            
                                                          <asp:CheckBoxList id="ChkListStatus" runat="server" CssClass="textbox" Width="143px" TabIndex="1">
                                                                    <asp:ListItem>Show Online Status</asp:ListItem>
                                                                    <asp:ListItem>Show Address</asp:ListItem>
                                                                    <asp:ListItem>Show Country</asp:ListItem>
                                                                </asp:CheckBoxList>&nbsp;<asp:CheckBox ID="ChkChainCode" runat ="server" Text="Show Chain Code" CssClass="textbox" TabIndex="1"   /></td>                                                        
                                                        <td style="width: 21%;" rowspan ="7"  colspan ="2" valign ="top"><div id="divlbl" runat ="server" ><span  class="textbold">1 A Booking Details</span></div>
                                                        <div id="DivABookDetails" runat ="server" >
                                                                <asp:CheckBoxList ID="ChkABooking" runat="server" CssClass="textbox" TabIndex="1">
                                                                    <asp:ListItem Selected="True">Air</asp:ListItem>
                                                                    <asp:ListItem>Car</asp:ListItem>
                                                                    <asp:ListItem>Hotel</asp:ListItem>
                                                                    <asp:ListItem>Insurance</asp:ListItem>
                                                                </asp:CheckBoxList></div> 
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                    <td style="width:3%"></td>
                                                     <td  class="textbold" style="width:13%">
                                                         Region</td>
                                                      <td style="width:20%"><asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="1">
                                                      </asp:DropDownList></td>
                                                      <td ></td>
                                                       <td ><div id ="DivShowAvg" runat ="server" ><asp:RadioButton ID="RdShowAvg" runat="server" CssClass="textbox" Text="Show Average" GroupName="Avg" TabIndex="1" /></div></td>                                      
                                                       
                                                    </tr>
                                                     <tr>
                                                    <td  style="width:3%"></td>
                                                     <td  class="textbold" style="width:13%">
                                                         Group Type</td>
                                                      <td  class="textbold" style="width:20%"><asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="1">
                                                      </asp:DropDownList></td>
                                                       <td></td>
                                                       <td>
                                                           <asp:CheckBox ID="ChkNewFormat" runat="server" CssClass="textbox" TabIndex="1" Text="Cross-Tab" /></td>                                                     
                                                       
                                                    </tr>
                                                     <tr>
                                                    <td  class="textbold" style="width:3%; "></td>
                                                     <td  class="textbold" style="width:15%; ">
                                                         Agency Group Category</td>
                                                      <td  class="textbold" style="width:20%; ">
                                                        <asp:DropDownList ID="drpAgencyType" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="1"></asp:DropDownList></td>
                                                       <td ></td>
                                                       <td ><asp:CheckBox ID="ChkExTrans" runat="server" CssClass="textbox" Text="Excessive Transaction" Width="151px" TabIndex="1" Visible="False" />
                                                            </td>                                                                                                                                                                                                                 
                                                    </tr>
                                                                     <tr>
                                                                         <td class="textbold" style="width: 3%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 15%">
                                                                             Agency Category</td>
                                                                         <td class="textbold" style="width: 20%">
                                                                         <asp:DropDownList ID="drpLstGroupClassType" runat="server" CssClass="dropdownlist" Width="165px" TabIndex="1" >
                                                      </asp:DropDownList></td>
                                                                         <td>
                                                                         </td>
                                                                         <td>&nbsp;</td>
                                                                         <td class="textbold" rowspan="1" style="width: 23%" valign="top">
                                                                             &nbsp;<asp:CheckBox ID="ChkGroupClass" runat="server" CssClass="textbox" Text="Show Agency Category" TabIndex="1" /></td>
                                                                     </tr>
                                                                     <tr>
                                                                         <td class="textbold" style="width: 3%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 15%">
                                                                             Company Vertical</td>
                                                                         <td class="textbold" style="width: 20%">
                                                                             <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                 TabIndex="1" Width="165px">
                                                                             </asp:DropDownList></td>
                                                                         <td>
                                                                         </td>
                                                                         <td>
                                                                         </td>
                                                                         <td class="textbold" rowspan="1" style="width: 23%" valign="top">
                                                                         </td>
                                                                     </tr>
                                                       <tr>
                                                    <td colspan="8" style="height: 7px"></td> 
                                                    </tr>   
                                                       <tr>
                                                    <td  class="textbold" style="width:3%"></td>
                                                     <td  class="textbold" style="width:15%" valign ="top">
                                                         Period From
                                                     </td>
                                                      <td  class="textbold" style="width:20%" valign ="top">
                                                         <asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist"
                                                              TabIndex="20" Width="88px" style="position: relative">
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
                                                          </asp:DropDownList>
                                                               <asp:DropDownList ID="drpYearFrom" runat="server" CssClass="dropdownlist" TabIndex="21"
                                                               Width="56px" style="left: 16px; position: relative; top: 0px"></asp:DropDownList></td>
                                                       <td  style="width:2%"></td>
                                                       <td  class="textbold" style="width:20%"valign ="top">
                                                           Period To</td>
                                                        <td  class="textbold" style="width:23%" valign ="top" >
                                                         <table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                            <tr>
                                                               <td colspan="2">
                                                                <asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist"
                                                              TabIndex="22" Width="88px">
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
                                                          </asp:DropDownList>&nbsp;<asp:DropDownList ID="drpYearTo" runat="server" CssClass="dropdownlist" TabIndex="23"
                                                                     Width="56px" style="left: 8px; position: relative; top: 0px">
                                                                 </asp:DropDownList><span  class="textbold" ></span></td>
                                                            </tr>
                                                         </table>
                                                            <span  class="textbold"></span></td>
                                                             <td  class="textbold" style="width:19%" colspan="2" valign ="top">
                                                                 &nbsp; &nbsp;&nbsp;
                                                             </td>
                                                    </tr>
                                                      <tr>
                                                    <td colspan="8" style ="height:10px;"></td> 
                                                    </tr> 
                                                     <%-- <tr>
                                                      <td></td>
                                                      <td ><span  class="textbold"><b>No. of records found</b></span></td>
                                                    <td colspan="6"> <asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="24" Width="100px" ReadOnly="True" Text ="0"></asp:TextBox></td> 
                                                    </tr> --%>
                                                       <tr>
                                                    <td colspan="8" class="textbold" ><asp:Label id="lblFound" runat ="server" Text ="No. of records found " Font-Bold="True" Width="142px" Visible="False" ></asp:Label>
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
            <tr>
                <td valign="top" style="padding-left:4px;" >
                   <table  border="0" align="left" cellpadding="0" cellspacing="0"> 
                        <tr>  
                        
                             <td class="redborder" >
                                    <asp:GridView EnableViewState ="false" ID="grdvMonthlyBReak" runat="server" AutoGenerateColumns="False"  AllowSorting ="true" HeaderStyle-ForeColor="white" 
                                                            width="1800px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" ShowFooter ="true" >
                                                              <Columns>
                                                                     <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code "  SortExpression ="CHAIN_CODE" >
                                                                         <ItemStyle  Wrap="False" Width="55px" />
                                                                         <HeaderStyle Wrap="False" Width="55px" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="LOCATION_CODE" HeaderText="Lcode" SortExpression ="LOCATION_CODE" >
                                                                         <ItemStyle  Wrap="False"  Width="55px" />
                                                                         <HeaderStyle Wrap="False" Width="55px"/>
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name " SortExpression ="AGENCYNAME"  >
                                                                         <ItemStyle Width="230px" Wrap="false" />
                                                                         <HeaderStyle Wrap="False" Width="230px" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="ADDRESS" HeaderText="Address " SortExpression ="ADDRESS"  >
                                                                         <ItemStyle Width="280px" Wrap="false" />
                                                                         <HeaderStyle Wrap="False" Width="280px" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Sales Executive " SortExpression ="EMPLOYEE_NAME"  >
                                                                         <ItemStyle Width="130px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>     
                                                                        <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="100px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>                                                                   
                                                                     <asp:BoundField  DataField="CITY" HeaderText="City " SortExpression ="CITY"   >
                                                                         <ItemStyle Width="80px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="COUNTRY" HeaderText="Country " SortExpression ="COUNTRY"  >
                                                                          <ItemStyle Width="80px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField> 
                                                                     <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status "   SortExpression ="ONLINE_STATUS"  >
                                                                          <ItemStyle Width="88px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                      <asp:BoundField DataField="GROUP_CLASSIFICATION_NAME" HeaderText="Type "   SortExpression ="GROUP_CLASSIFICATION_NAME"  >
                                                                          <ItemStyle Width="80px" Wrap="False" />
                                                                         <HeaderStyle Wrap="True" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="OFFICEID" HeaderText="OfficeId "  SortExpression ="OFFICEID"  >
                                                                             <ItemStyle Width="80px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="MONTH" HeaderText="Month "  SortExpression ="MONTH"  >
                                                                          <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="YEAR" HeaderText="Year "  SortExpression ="YEAR"  >
                                                                           <ItemStyle Width="50px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>                                                                                                                                 
                                                                     <asp:TemplateField HeaderText="Air "    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"   SortExpression ="AIR_NETBOOKINGS"  >
                                                                        <ItemTemplate>
                                                                                <%# Eval("AIR_NETBOOKINGS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotAir" runat="server"  ></asp:label>
                                                                        </FooterTemplate>   
                                                                          <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="60px" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Car "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="CAR_NETBOOKINGS"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("CAR_NETBOOKINGS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotCar" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="60px" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Hotel "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="HOTEL_NETBOOKINGS"   >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("HOTEL_NETBOOKINGS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotHotel" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="60px" />
                                                                          <HeaderStyle Wrap="False" />
                                                                          <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Insurance "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"   SortExpression ="INSURANCE_NETBOOKINGS"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("INSURANCE_NETBOOKINGS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotIns" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                          <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="65px" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Total "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="TOTAL"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TOTAL")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotSum" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                          <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="65px" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NBS "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="PASSIVE"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("PASSIVE")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotPassive" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                          <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="80px" />
                                                                         <HeaderStyle Wrap="true" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Air Booking "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="WITHPASSIVE"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("WITHPASSIVE")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotWithPassive" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                           <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="80px" />
                                                                         <HeaderStyle Wrap="true" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Action" HeaderStyle-Width="200px">
                                                                         <ItemTemplate>
                                                                           <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a>&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkCRs" runat="server">CRS Details</a>&nbsp; <a href="#" class="LinkButtons" id="linkABreakUp" runat="server">1 A BreakUp</a>                                                                          
                                                                           <asp:HiddenField ID="hdFMonth" runat="server"  />   
                                                                           <asp:HiddenField ID="hdTMonth" runat="server"  />  
                                                                           <asp:HiddenField ID="hdFYear" runat="server"  />   
                                                                           <asp:HiddenField ID="hdTYear" runat="server"  />   
                                                                           <asp:HiddenField ID="hdAdd" runat="server"  Value ='<%# Eval("ADDRESS") %>' /> 
                                                                           <asp:HiddenField ID="hdCountry" runat="server"   Value ='<%# Eval("COUNTRY") %>' />  
                                                                        </ItemTemplate>
                                                                         <ItemStyle Wrap="False" Width ="200px" />
                                                                         <HeaderStyle />
                                                                    </asp:TemplateField>  
                                                                     
                                                            </Columns>
                                                            
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                        <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>    
                                                            <asp:GridView EnableViewState ="false" ID="grdvShowAvg" runat="server" AutoGenerateColumns="False"  ShowFooter ="true" 
                                                            width="1800px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"  HeaderStyle-ForeColor="white" 
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True">
                                                              <Columns>
                                                                     <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code "  SortExpression ="CHAIN_CODE"  >
                                                                         <ItemStyle Width="55px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="LOCATION_CODE" HeaderText="Lcode "  SortExpression ="LOCATION_CODE"   >
                                                                         <ItemStyle Width="55px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name " SortExpression ="AGENCYNAME" >
                                                                         <ItemStyle Width="230px" Wrap="false" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="ADDRESS" HeaderText="Address " SortExpression ="ADDRESS" >
                                                                         <ItemStyle Width="280px" Wrap="false" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Sales Executive "  SortExpression ="EMPLOYEE_NAME" >
                                                                       <ItemStyle  Wrap="False" Width="130px" />
                                                                         <HeaderStyle Wrap="false" />
                                                                     </asp:BoundField>  
                                                                        <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="100px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>                                                                      
                                                                     <asp:BoundField  DataField="CITY" HeaderText="City "  SortExpression ="CITY" >
                                                                        <ItemStyle  Wrap="False" Width="80px" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="COUNTRY" HeaderText="Country "  SortExpression ="COUNTRY" >
                                                                         <ItemStyle  Wrap="False" Width="80px" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField> 
                                                                     <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status " SortExpression ="ONLINE_STATUS"  >
                                                                          <ItemStyle  Wrap="False" Width="88px" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>
                                                                       <asp:BoundField DataField="GROUP_CLASSIFICATION_NAME" HeaderText="Type"   SortExpression ="GROUP_CLASSIFICATION_NAME"  >
                                                                         <ItemStyle Width="80px" Wrap="False" />
                                                                         <HeaderStyle Wrap="True" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="OFFICEID" HeaderText="OfficeId " SortExpression ="OFFICEID"  >
                                                                      <ItemStyle  Wrap="False" Width="80px" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField> 
                                                                     <asp:TemplateField HeaderText="Air " SortExpression ="AIR_NETBOOKINGS" >
                                                                        <ItemTemplate  >
                                                                                <%# Eval("AIR_NETBOOKINGS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotAir" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="60px" />
                                                                         <HeaderStyle Wrap="true" HorizontalAlign="Right" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Car "  SortExpression ="CAR_NETBOOKINGS" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("CAR_NETBOOKINGS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotCar" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="60px" />
                                                                         <HeaderStyle Wrap="true" HorizontalAlign="Right" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Hotel " SortExpression ="HOTEL_NETBOOKINGS" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("HOTEL_NETBOOKINGS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotHotel" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                       <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="60px" />
                                                                          <HeaderStyle Wrap="true" HorizontalAlign="Right" />
                                                                          <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Insurance " SortExpression ="INSURANCE_NETBOOKINGS" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("INSURANCE_NETBOOKINGS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotIns" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                        <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="65px" />
                                                                         <HeaderStyle Wrap="true" HorizontalAlign="Right" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Total " SortExpression ="AVERAGE">
                                                                        <ItemTemplate  >
                                                                                <%#Eval("AVERAGE")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotAvg" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="70px" />
                                                                         <HeaderStyle Wrap="true" HorizontalAlign="Right" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>       
                                                                         <asp:TemplateField HeaderText="NBS "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="PASSIVE"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("PASSIVE")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotPassive" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="80px" />
                                                                         <HeaderStyle Wrap="true" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Air Booking "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="WITHPASSIVE"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("WITHPASSIVE")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotWithPassive" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                      <ItemStyle HorizontalAlign="Right"  Wrap="False" Width="80px" />
                                                                         <HeaderStyle Wrap="true" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>                                                                                 
                                                                     <asp:TemplateField HeaderText="Action">
                                                                         <ItemTemplate>
                                                                          <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a>&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkCRs" runat="server">CRS Details</a>&nbsp; <a href="#" class="LinkButtons" id="linkABreakUp" runat="server">1 A BreakUp</a>                                                                          
                                                                           <asp:HiddenField ID="hdFMonth" runat="server"  />   
                                                                           <asp:HiddenField ID="hdTMonth" runat="server"  />  
                                                                              <asp:HiddenField ID="hdFYear" runat="server"  />   
                                                                           <asp:HiddenField ID="hdTYear" runat="server"  />  
                                                                              <asp:HiddenField ID="hdAdd" runat="server"  Value ='<%# Eval("ADDRESS") %>' /> 
                                                                             <asp:HiddenField ID="hdCountry" runat="server"   Value ='<%# Eval("COUNTRY") %>' />    
                                                                        </ItemTemplate>
                                                                         <ItemStyle Wrap="false" Width ="200px"/>
                                                                         <HeaderStyle Wrap ="false"  />
                                                                    </asp:TemplateField>   
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="true"  />
                                                            <RowStyle CssClass="textbold" />
                                       <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>  
                                    <asp:GridView EnableViewState ="false" ID="GrdExcessMonthBreak" runat="server" AutoGenerateColumns="False"  ShowFooter ="true"  HeaderStyle-ForeColor="white" 
                                                            Width="1800px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True">
                                                              <Columns>
                                                                     <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code " SortExpression ="CHAIN_CODE"  >
                                                                         <ItemStyle Width="70px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="LOCATION_CODE" HeaderText="Lcode "  SortExpression ="LOCATION_CODE"  >
                                                                         <ItemStyle Width="70px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name "  SortExpression ="AGENCYNAME"  >
                                                                         <ItemStyle Width="280px" Wrap="True" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="ADDRESS" HeaderText="Address " SortExpression ="ADDRESS"  >
                                                                         <ItemStyle Width="310px" Wrap="True" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Sales Executive " SortExpression ="EMPLOYEE_NAME"  >
                                                                         <ItemStyle Wrap="False" />
                                                                     </asp:BoundField>  
                                                                        <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="100px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>                                                                      
                                                                     <asp:BoundField  DataField="CITY" HeaderText="City "  SortExpression ="CITY"  >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="COUNTRY" HeaderText="Country "  SortExpression ="COUNTRY"  >
                                                                         <ItemStyle Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField> 
                                                                     <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status " SortExpression ="ONLINE_STATUS"   >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="OFFICEID" HeaderText="OfficeId "  SortExpression ="OFFICEID"  >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="MONTH" HeaderText="Month "  SortExpression ="MONTH"  >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>                                                                    
                                                                       <asp:TemplateField HeaderText="Total " SortExpression ="TOTAL"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TOTAL")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotSum" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                           <ItemStyle HorizontalAlign="Right"  Wrap="False" />
                                                                           <HeaderStyle Wrap="False" HorizontalAlign="Right" />
                                                                           <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="Transaction " SortExpression ="TRANSACTIONS"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TRANSACTIONS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotTrans" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                           <ItemStyle HorizontalAlign="Right"  Wrap="False" />
                                                                           <HeaderStyle Wrap="False" HorizontalAlign="Right" />
                                                                           <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Trans. Per Segment " SortExpression ="TRANPERSEG"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TRANPERSEG")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotTransPerSeg" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right"  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" HorizontalAlign="Right" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Action" >
                                                                         <ItemTemplate>
                                                                           <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a>&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkCRs" runat="server">CRS Details</a>&nbsp; <a href="#" class="LinkButtons" id="linkABreakUp" runat="server">1 A BreakUp</a>   
                                                                          
                                                                           <asp:HiddenField ID="hdFMonth" runat="server"  />   
                                                                           <asp:HiddenField ID="hdTMonth" runat="server"  />  
                                                                              <asp:HiddenField ID="hdFYear" runat="server"  />   
                                                                           <asp:HiddenField ID="hdTYear" runat="server"  />   
                                                                            <asp:HiddenField ID="hdAdd" runat="server"  Value ='<%# Eval("ADDRESS") %>' /> 
                                                                             <asp:HiddenField ID="hdCountry" runat="server"   Value ='<%# Eval("COUNTRY") %>' />  
                                                                        </ItemTemplate>
                                                                         <ItemStyle Wrap="False" Width="200px" />
                                                                         <HeaderStyle Width="200px" />
                                                                    </asp:TemplateField>   
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                        <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>    
                                      <asp:GridView EnableViewState ="false" ID="GrdExcessAvg" runat="server" AutoGenerateColumns="False"  ShowFooter ="true" 
                                                            Width="1800px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor" HeaderStyle-ForeColor ="white" 
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True">
                                                              <Columns>
                                                                     <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code " SortExpression ="CHAIN_CODE"  >
                                                                         <ItemStyle Width="70px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="LOCATION_CODE" HeaderText="Lcode " SortExpression ="LOCATION_CODE"  >
                                                                         <ItemStyle Width="70px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name " SortExpression ="AGENCYNAME"  >
                                                                         <ItemStyle Width="280px" Wrap="True" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="ADDRESS" HeaderText="Address " SortExpression ="ADDRESS"  >
                                                                         <ItemStyle Width="310px" Wrap="True" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Sales Executive " SortExpression ="EMPLOYEE_NAME"  >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>   
                                                                        <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="100px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>                                                                     
                                                                     <asp:BoundField  DataField="CITY" HeaderText="City "  SortExpression ="CITY"  >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="COUNTRY" HeaderText="Country " SortExpression ="COUNTRY"   >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField> 
                                                                     <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status "  SortExpression ="ONLINE_STATUS"  >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>
                                                                     <asp:BoundField DataField="OFFICEID" HeaderText="OfficeId " SortExpression ="OFFICEID"  >
                                                                         <ItemStyle  Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                     </asp:BoundField>                                                                
                                                                       <asp:TemplateField HeaderText="Average " SortExpression ="AVERAGE"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("AVERAGE")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotAvg" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                           <ItemStyle HorizontalAlign="Right"  Wrap="False" />
                                                                           <HeaderStyle Wrap="False" HorizontalAlign="Right" />
                                                                           <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField> 
    
                                                                       <asp:TemplateField HeaderText="Transaction "  SortExpression ="TRANSACTIONS" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TRANSACTIONS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotTrans" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                           <ItemStyle HorizontalAlign="Right"  Wrap="False" />
                                                                           <HeaderStyle Wrap="False" HorizontalAlign="Right" />
                                                                           <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Trans. Per Segment " SortExpression ="TRANPERSEG"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TRANPERSEG")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotTransPerSeg" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" HorizontalAlign="Right" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     
                                                                     <asp:TemplateField HeaderText="Action">
                                                                         <ItemTemplate>
                                                                          <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a>&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkCRs" runat="server">CRS Details</a>&nbsp; <a href="#" class="LinkButtons" id="linkABreakUp" runat="server">1 A BreakUp</a>   
                                                                          
                                                                           <asp:HiddenField ID="hdFMonth" runat="server"  />   
                                                                           <asp:HiddenField ID="hdTMonth" runat="server"  />  
                                                                              <asp:HiddenField ID="hdFYear" runat="server"  />   
                                                                           <asp:HiddenField ID="hdTYear" runat="server"  />  
                                                                              <asp:HiddenField ID="hdAdd" runat="server"  Value ='<%# Eval("ADDRESS") %>' /> 
                                                                             <asp:HiddenField ID="hdCountry" runat="server"   Value ='<%# Eval("COUNTRY") %>' />    
                                                                        </ItemTemplate>
                                                                         <ItemStyle Wrap="False" Width="200px" />
                                                                         <HeaderStyle Width="200px" />
                                                                    </asp:TemplateField>   
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                          <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>   
                                   <asp:GridView EnableViewState ="false"  ID="grdNewFormat" runat="server" AutoGenerateColumns="true"  ShowFooter ="true" 
                                                            Width="1800px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor" HeaderStyle-ForeColor ="white" 
                                                            AlternatingRowStyle-CssClass="lightblue"  AllowSorting="True">
                                                            <Columns>
                                                              <asp:TemplateField HeaderText="Action" >
                                                                         <ItemTemplate>
                                                                          <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a>&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkCRs" runat="server">CRS Details</a>&nbsp; <a href="#" class="LinkButtons" id="linkABreakUp" runat="server">1 A BreakUp</a>   
                                                                          

                                                                              <asp:HiddenField ID="hdAdd" runat="server"  Value ='<%# Eval("ADDRESS") %>' /> 
                                                                             <asp:HiddenField ID="hdCountry" runat="server"   Value ='<%# Eval("COUNTRY") %>' />
                                                                        </ItemTemplate>
                                                                         <ItemStyle Wrap="False" />
                                                                    </asp:TemplateField>   
                                                            
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                          <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>                 
                             </td>
                        </tr> 
                        <tr>                                                   
                                                    <td colspan="6" valign ="top" style ="width:860px;"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" style ="width:860px;" >
                                                      <table border="0" cellpadding="0" cellspacing="0" style ="width:860px;" >
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey"  ReadOnly="true"></asp:TextBox></td>
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
                                                    <td colspan="6" ></td> 
                                                </tr> 
                     </table> 
                </td> 
            </tr> 
             <tr>
              <td><asp:TextBox ID="txtRecordOnCurrentPage" runat ="server"  Width="73px" CssClass="textboxgrey" Visible ="false"  ></asp:TextBox></td>
            </tr>
        </table>     
    </form>
    <script  type ="text/javascript" language ="javascript" >
        ActDecLcodeChainCode();
       // Show1ABooking();        
       var objChkNewFormat =document.getElementById('ChkNewFormat');       
       var objDivShowAvg =document.getElementById('DivShowAvg');              
       
       if (objChkNewFormat.checked==true) 
       {        
          objDivShowAvg.className="displayNone";
         
       }
         if (document.getElementById("hdAgencyName").value=="")
              {
                  document.getElementById("ChkGrpProductivity").disabled=true;
                  document.getElementById("ChkGrpProductivity").checked==false;        
              }
      
        
    </script>
</body>

</html>
