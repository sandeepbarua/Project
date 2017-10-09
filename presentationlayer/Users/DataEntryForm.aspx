<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataEntryForm.aspx.cs" Inherits="PresentationLayer.Users.DataEntryForm" %>
<%@ Register Src="~/UserControls/UsrConvictionDetails.ascx" TagName="TgConvictionDetails" TagPrefix="UCConvictionDetails" %>
<%@ Register Src="~/UserControls/UsrDriverLicenceDetail.ascx" TagName="TgDriverLicence" TagPrefix="UCDriverLicence" %>
<%@ Register Src="~/UserControls/UsrPreviousEmploymentDetail.ascx" TagName ="TgPreviousEmployment" TagPrefix="UCPreviousEmployment" %>
<%@ Register Src="~/UserControls/UsrCurrentResidenceDetail.ascx" TagName ="TgCurrentResidence" TagPrefix="UCCurrentResidence" %>
<%@ Register Src="~/UserControls/UsrPreviousResidenceDetail.ascx" TagName ="TgPreviousResidence" TagPrefix="UCPreviousResidence" %>
<%@ Register Src="~/UserControls/UsrTrafficConvictionsDetail.ascx" TagName ="TgTrafficConvictions" TagPrefix="UCTrafficConvictions" %>
<%@ Register Src="~/UserControls/UsrPreviousEmployerDetail.ascx" TagName ="TgPreviousEmployer" TagPrefix="UCPreviousEmployer" %>
<%@ Register Src="~/UserControls/UsrCEDPreviousEmployerDetail.ascx" TagName ="TgCEDPreviousEmployer" TagPrefix="UCCEDPreviousEmployer" %>
<%@ Register Src="~/UserControls/UsrViolationDetails.ascx" TagName ="TgViolationDetails" TagPrefix="UCUsrViolationDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
<link href="../vendor/bootstrap/css/bootstrap-select.min.css" rel="stylesheet"/>
<link href="../vendor/fa/css/font-awesome.min.css" rel="stylesheet" />
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <style>
      
         .spaced input[type="radio"]
{
   margin-left: 20px;
   margin-right: 2px;
}
        .menu{

    border:none;
   
    border:0px;

    margin:0px;

    padding:0px;

    font: 67.5% "Lucida Sans Unicode", "Bitstream Vera Sans", "Trebuchet Unicode MS", "Lucida Grande", Verdana, Helvetica, sans-serif;

    font-size:12px;

    font-weight:bold;

    }

.menu ul{
    list-style:none;
    border: none;
    padding:10px;
    
    text-align: center;
    text-decoration: none;
   
    font-size: 12px;
    cursor: pointer;
    -webkit-transition-duration: 0.4s; /* Safari */
    transition-duration: 0.4s;

    }
	.menu ul {
       
    }
	

    .menu ul:hover {
          }
    
	

    .menu li{

        float:left;

        padding:0px;
		

        }

    .menu li a{
      
      
        display:block;

        font-weight:normal;

  	color: black;
        margin:0px;

        padding:0px 25px;

        text-align:center;

        text-decoration:none;

        }

        .menu li a:hover, .menu ul li:hover a{

            background: #309bcf url("images/hover.png") bottom center no-repeat;

            color:#FFFFFF;

            text-decoration:none;

            }

   .menu li ul{

        background:#333333;

        display:none;

        height:auto;

        padding:0px;

        margin:0px;

        border:0px;

        position:absolute;

        width:225px;

        z-index:200;

        /*top:1em;

        /*left:0;*/

        }

    .menu li:hover ul{
        display:block;
        }

    .menu li li {

        background:url('images/sub_sep.gif') bottom left no-repeat;

        display:block;

        float:none;

        margin:0px;

        padding:0px;

        width:225px;

        }

    .menu li:hover li a{

        background:none;

         

        }

    .menu li ul a{

        display:block;

        height:25px;

        font-size:12px;
		color: white;
        font-style:normal;

        margin:0px;

        padding:0px 10px 0px 15px;

        text-align:left;

        }

        .menu li ul a:hover, .menu li ul li:hover a{

            background:#309bcf url('images/hover_sub.png') center left no-repeat;

            border:0px;

            color:#ffffff;

            text-decoration:none;

            }

    .menu p{

        clear:left;

        }
    .footer {
  position: fixed;
  right: 0;
  bottom: 0;
  left: 0;
  padding: 1rem;
  background-color: #efefef;
  text-align: center;
}

    </style>
     <style>
        .input-validation-error {
            background: #FEF1EC;
            border: 1px solid #CD0A0A;
        }

        #div_top_hypers {
            background-color: #eeeeee;
            display: inline;
        }

        #ul_top_hypers {
            display: flex;
            justify-content: space-around;
            list-style-type: none;
        }

        .auto-style2 {
            width: 31%;
        }

        .auto-style3 {
            width: 41%;
        }

        .auto-style4 {
            height: 47px;
        }

        .auto-style5 {
            width: 41%;
            height: 47px;
        }
    </style>
     <script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();
        }

       

        $(document).on('focus', "input.myclass", function () {
            console.log('onfocus is calling');
            if ($(this).val() == 'MM/DD/YYYY') {
                $(this).val('');
            }
           

        }).on('blur', "input.myclass", function () {
            console.log('blur is calling')
            var txt = $(this).val();
            if (!txt) {
                $(this).val('MM/DD/YYYY');
            }
        });

        $(document).ready(function () {
            console.log("ready!");
            if ($('[id^=ConvictionDetails1_txt]')) {
                var txt = $('[id^=ConvictionDetails1_txt]').val();
                console.log(txt);
                if(txt=='MM/DD/YYYY')
                {
                   // $('[id^=ConvictionDetails1_txt]').val('');
                }
            }
        });


         function validateNextButton() {
             var txt1 = parseInt( document.getElementById('<%= txtStartPage.ClientID %>').value);
             var txt2 = parseInt(document.getElementById('<%= txtEndPage.ClientID %>').value);
            var isError = false;
            if (isNaN(txt2)) {
                isError = true;
            }
             var txt3=document.getElementById('<%= txtEndPage.ClientID %>').value;
             if (!(/^\d+$/.test(txt3))) {
                isError = true;
            }
            if (txt2 < txt1) {
                isError = true;
            }

            if (isError) {             
                $("[id*=txtEndPage]").addClass(" input-validation-error ");
                window.scrollTo(0, 0);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
      <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    
    <script type="text/javascript">
                                                    function ManageControl(id, selectedValue) {

                                                     
                                                        document.getElementById(id.replace("tone", "hfControlValue")).value = selectedValue;
                                                        if (selectedValue != "-1") {
                                                            var text = document.getElementById(id.replace("tone", "hfControlName"));
                                                           // var res = text.value.substring(0, 3);
                                                            var strNew = text.value.substring(text.value.indexOf('rb_'));
                                                            var res = "";
                                                            if (strNew)
                                                            {
                                                                 res = strNew.substring(0, 3);
                                                            }
                                                           
                                                            if (res == "rb_") {
                                                                var allrbcntrls = document.getElementsByTagName("input");
                                                                for (var loop = 0; loop < allrbcntrls.length; loop++) {
                                                                    var tt = 'rptDynamicControl_' + text.value;

                                                                    if (allrbcntrls[loop].type == 'radio' && allrbcntrls[loop].id.indexOf(tt) >= 0) {
                                                                        console.log(allrbcntrls[loop].id.indexOf(tt));
                                                                        allrbcntrls[loop].disabled = true;
                                                                        allrbcntrls[loop].checked = false;
                                                                    }
                                                                }

                                                            }
                                                            else {
                                                                document.getElementById(id.replace("tone", text.value)).disabled = true;
                                                                document.getElementById(id.replace("tone", text.value)).value = "";
                                                            }

                                                        }
                                                        else {
                                                            var text = document.getElementById(id.replace("tone", "hfControlName"));

                                                           // var res = text.value.substring(0, 3);
                                                            var strNew = text.value.substring(text.value.indexOf('rb_'));
                                                            var res = "";
                                                            if (strNew) {
                                                                res = strNew.substring(0, 3);
                                                            }
                                                            if (res == "rb_") {
                                                                var allrbcntrls = document.getElementsByTagName("input");
                                                                for (var loop = 0; loop < allrbcntrls.length; loop++) {
                                                                    var tt = 'rptDynamicControl_' + text.value;
                                                                    if (allrbcntrls[loop].type == 'radio' && allrbcntrls[loop].id.indexOf(tt) >= 0) {
                                                                        console.log(allrbcntrls[loop].id.indexOf(tt));
                                                                        allrbcntrls[loop].disabled = false;
                                                                    }
                                                                }

                                                            }
                                                            else {

                                                                document.getElementById(id.replace("tone", text.value)).disabled = false;
                                                            }
                                                        }
                                                    }



                                                </script>
    <script>
        $(function () {
            console.log('document ready');

        var availableTags = [
            "Incomplete", "Missing", "Illegible"
        ];

        var availableTagsForPreviousEmployer = [
       "Incomplete", "Missing", "Illegible", "Till Date"
        ];

        

        $('[id^=ConvictionDetails1_txt]').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });
        })

        $('[id^=TgCurrentEmployer1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })


        $('#TgPreviousEmployer1_tbl :text').each(function () {
            $(this).autocomplete({
                source: availableTagsForPreviousEmployer
            });
        })

        $('[id^=TgCurrentResidence1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })


        $('[id^=TgPreviousResidence1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })


        $('[id^=TgTypeOfEquipment1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })

        $('[id^=TgAccidentRecord1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })

        $('[id^=TgTrafficConvictions1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })

        $('[id^=TgDriverLicenseStatus1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })

        $('[id^=TgPreviousEmployment1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })

        $('[id^=TgDriverLicence1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })

        $('[id^=TgCEDPreviousEmployer1_txt]').each(function () {
            $(this).autocomplete({
                source: availableTags
            });
        })

                    
        //$('#ConvictionDetails1_tbl :text').live("focus.autocomplete", null, function () {
        //    $(this).autocomplete({
        //        source: availableTags,
        //        minLength: 0,
        //        delay: 0
        //    });

        //    $(this).autocomplete("search");
        //});

    })();


        function ConfirmConvictionDetails() {
        var availableTags = [
        "Incomplete", "Missing", "Illegible"
        ];
        $('#ConvictionDetails1_tbl :text').each(function () {
            $(this).autocomplete({
                source: availableTags
            });

        })
    };

        
        function ConfirmDriverApplication() {
            var availableTags = [
            "Incomplete", "Missing", "Illegible"
            ];


            var availableTagsForPreviousEmployer = [
           "Incomplete", "Missing", "Illegible", "Till Date"
            ];

            $('#TgCurrentEmployer1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            });

            $('#TgPreviousEmployer1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTagsForPreviousEmployer
                });

            });

            $('#TgCurrentResidence1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            });

            $('#TgPreviousResidence1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            });

            $('#TgTypeOfEquipment1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            });

            $('#TgAccidentRecord1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            });

            $('#TgTrafficConvictions1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            });

            $('#TgDriverLicenseStatus1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            })

        };

        function ConfirmPreviousEmploymentDetails() {
            var availableTags = [
            "Incomplete", "Missing", "Illegible"
            ];
            $('#TgPreviousEmployment1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            })
        };

        function ConfirmDriverLicenceDetails() {
            var availableTags = [
            "Incomplete", "Missing", "Illegible","NA"
            ];
            $('#TgDriverLicence1_tbl :text').each(function () {
                $(this).autocomplete({
                    source: availableTags
                });

            })
        };

      

        </script>
    <script type="text/javascript">
        var sessionTimeout = "<%= Session.Timeout %>";
        function DisplaySessionTimeout()
        {
            //assigning minutes left to session timeout to Label
            document.getElementById("<%= lblSessionTime.ClientID %>").innerText = sessionTimeout;
            sessionTimeout = sessionTimeout - 1;
            
            //if session is not less than 0
            if (sessionTimeout >= 0)
                //call the function again after 1 minute delay
                window.setTimeout("DisplaySessionTimeout()", 60000);
            else
            {
                //show message box
                alert("Your current Session is over.");
            }
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
 
       <header>
 <div class="row">
	 <div class="col-lg-2"><div class="logoimg"><img src="images/logo_header.png" width="" height="" alt="" class="img-fluid"/></div></div>
	 <div class="col-lg-10"><nav class="float-right top_nav">
        <asp:LinkButton ID="lnkbtn_changepassword" class="fa fa-cog" runat="server" data-toggle="modal" data-target="#myModelAdd"></asp:LinkButton>
      </nav>
	 </div>
	</div>

<link href="../css/main.css" rel="stylesheet"/>
</header>
 
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container" id="dvSubmit" runat="server" visible="false" style="width: 960px; padding-bottom: 10px;">
            &nbsp;<div class="row">
                <div>
                    <div class="row">
                        <div class="small-12 medium-12 columns">
                            <div style="padding-left: 180px">
                                <div class="menu-title" style="font-size: 15px;">
                                    You have completed all the pages. Please click on Submit button to confirm it or click on Back button to Review the entry 
                                </div>

                            </div>
                        </div>
                    </div>
                     <div  runat ="server" style="display:none">
         <asp:Label ID="Label1" runat="server" Text="Label" ></asp:Label>
        </div>
                    <div class="row">
                        <div class="small-12 medium-12 columns">
                            <div style="padding-left: 250px; margin-top: 15px;">
                                <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" ValidationGroup="grp2" class="btn btn-primary" />
                                &nbsp;&nbsp;&nbsp;     
                                 <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" ValidationGroup="grp2" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    <div runat="server" id="dvMainContainer">
    <div id="wrapper"> 
	<div class="row no-gutters">
		<div class="col-lg-2 sidebar-wrapper nomargin">
            <div class="user_sidebar_text text-center"> 
                 <a>Welcome <asp:Label ID="lblUserName" runat="server"></asp:Label></a>
            | <asp:LinkButton ID="btnSignOut" runat="server" Text="LogOut" OnClick="btnSignOut_Click"></asp:LinkButton><br />
                 <h3>FAX ID:   <asp:Label ID="lblFaxId" runat="server"></asp:Label> </h3>
                <asp:HiddenField ID="hfTempCPScreenDataID" runat="server" />
                    <asp:HiddenField ID="hfTempAssignmentId" runat="server" />
                    <asp:HiddenField ID="hfTaskOperationId" Value="0" runat="server" /> 

                </div>

           
            <div class="menu" style="padding-left:30px">
        <ul>
            <li>
                <ul>
                   <li>
                    </li>
                    <li>
                        </li>

               </ul>

          </li>
        </ul>
    </div>
		</div>
		<div class="col-lg-10 page-content-wrapper"> <div class="top_content">
      <div class="row">
        <div class="col-lg-4">
          <div class="row">
            <div class="col-lg-6">
              <label></label>
            </div>
            <div class="col-lg-6">
  
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6">
              <label>Customer Name</label>
            </div>
            <div class="col-lg-6">
                    <asp:DropDownList ID="ddlCustomer" class="form-control" Width="177" Height="38" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                        </asp:DropDownList>
            </div>
          </div>
                      <div class="row">
            <div class="col-lg-6">
              <label></label>
            </div>
            <div class="col-lg-6">
  
            </div>
          </div>
              <div class="row">
            <div class="col-lg-6">
              <label>Location</label>
            </div>
            <div class="col-lg-6">
              <asp:DropDownList ID="ddlLocation" Enabled="false" Width="177" Height="38"  runat="server" class="form-control"></asp:DropDownList>
                                        <asp:HiddenField ID="hfLocationId" runat="server" />
                                        <asp:Label ID="lblLocatoinError" runat="server" Visible="false" Text="Not Matched" ForeColor="Red"></asp:Label>
            </div>
          </div>
        </div>
        <div class="col-lg-8">
          <div class="row">
            <div class="col-lg-6">
              <label></label>
            </div>
            <div class="col-lg-6">


            </div>
          </div>
          <div class="row">
            <div class="col-lg-2">
              <label>Document Type</label>
            </div>
            <div class="col-lg-10">
              <asp:DropDownList ID="ddlDocumentType" runat="server" Width="305" Height="38" class="form-control" Enabled="false"  AutoPostBack="True" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:HiddenField ID="hfDbOperation" runat="server" Value="Add" />
                                        <asp:HiddenField ID="hfIsReviewed" runat="server" Value="0" />
                                        <asp:HiddenField ID="hfNewDriver" Value="0" runat="server" />
            </div>
          </div>
          <div class="row" style="margin-top:15">
            <div class="col-lg-2">
              <label>Start Page</label>
            </div>
            <div class="col-lg-2">
                 <asp:TextBox ID="txtStartPage" class="form-control smallinput" Width="40" Height="34" Text="1" Enabled="false" runat="server"></asp:TextBox>
            </div>
              
                                        <asp:HiddenField ID="hfEndPageId" runat="server" />
                                        <asp:HiddenField ID="hfCountForFaxId" runat="server" Value="1" />
            <div class="col-lg-2">
              <label>End Page</label>
            </div>
            <div class="col-lg-2">
                 <asp:TextBox ID="txtEndPage" class="form-control smallinput" runat="server"></asp:TextBox>
            </div>
              
                                            <asp:HiddenField ID="hfStartPageId" runat="server" />
          </div>
        </div>
      </div>
    </div>
    <div class="grey_box">
        <div class="row">
          <div class="col-lg-8">

                  <asp:HiddenField ID="hfRepeaterBound" runat="server" Value="0" />
         
                      <asp:Repeater ID="rptDynamicControl" runat="server" OnItemDataBound="rptDynamicControl_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <table style="border-spacing: 10px;">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td style="padding: 6px;">


                                                                                <asp:HiddenField ID="hfDropDownValue" runat="server" Value='<%# Eval("DropDownValue") %>' />
                                                                                <asp:HiddenField ID="hfDynamicControlValueText" runat="server" Value='<%# Eval("DynamicControlValueText") %>' />
                                                                                <asp:HiddenField ID="hfDynamicControlID" runat="server" Value='<%# Eval("DynamicControlID") %>' />
                                                                                <asp:HiddenField ID="hfControlName" runat="server" Value='<%# Eval("ControlName") %>' />
                                                                                <asp:HiddenField ID="hfControlType" runat="server" Value='<%# Eval("ControlType") %>' />
                                                                                <asp:HiddenField ID="hfDyanamicControlValueID" runat="server" Value='<%# Eval("DyanamicControlValueID") %>' />
                                                                                <asp:Label ID="lblLabelName" runat="server" Text='<%#Eval("labelName") %>'></asp:Label>

                                                                            </td>

                                                                            <td style="padding: 6px;">

                                                                                <div id="dvControl" runat="server">
                                                                                </div>
                                                                            </td>
                                                                            <td style="padding: 6px;">
                                                                                <div id="dvDropDown" runat="server">
                                                                                    <div class="col-lg-4" style="width:30px">
                                                                                    <asp:DropDownList class="form-control" Width="200px" Height="35px" ID="tone" runat="server" onchange="ManageControl(this.id , this.value)">
                                                                                    </asp:DropDownList>
                                                                                        </div>
                                                                                </div>
                                                                                <asp:HiddenField ID="hfTextBoxId" runat="server" />
                                                                                <asp:HiddenField ID="hfControlValue" runat="server" />
                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>

                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
              
              <div style="margin-top:20px"></div>
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            
                                    <ContentTemplate>
                                         <UCConvictionDetails:TgConvictionDetails ID="ConvictionDetails1" runat="server" Visible="false" />
                                    <UCDriverLicence:TgDriverLicence ID="TgDriverLicence1" runat="server" Visible="false" />
                                        <UCPreviousEmployment:TgPreviousEmployment ID="TgPreviousEmployment1" runat="server" Visible="false" />
                                     
                                       
                                          <UCPreviousEmployer:TgPreviousEmployer ID="TgPreviousEmployer1" runat="server" Visible="false" />
                                          <UCCEDPreviousEmployer:TgCEDPreviousEmployer ID="TgCEDPreviousEmployer1" runat="server" Visible="false" />
                                       <UCCurrentResidence:TgCurrentResidence ID="TgCurrentResidence1" runat="server" Visible="false" />
                                        <UCPreviousResidence:TgPreviousResidence ID="TgPreviousResidence1" runat="server" Visible="false" />
                                      
                                        <UCTrafficConvictions:TgTrafficConvictions ID="TgTrafficConvictions1" runat="server" Visible="false" />
                                     
                                        <UCUsrViolationDetails:TgViolationDetails ID="TgViolationDetails1" runat="server" Visible="false" />
                                    </ContentTemplate>

                                  
                                </asp:UpdatePanel>

              <div class="row">
              <div class="col-lg-4"></div>
              <div class="col-lg-4"> </div>
                  
            
              <div class="col-lg-4" style="margin-top:10px">

                  <asp:Button ID="btnNext" Text="Next"  class="btn btn-success btn-full nxt_btn" runat="server" OnClick="btnNext_Click" Visible="false" ValidationGroup="grp1" OnClientClick="return  validateNextButton();" />
              </div>
            </div>
          </div>
          <div class="col-lg-4"></div>
            
         <asp:Label ID="lblSessionTime" runat="server" Visible="false" Text="Label" ></asp:Label>
        </div>
      </div></div>
	</div>
        


</div>
	<footer style="background: #fffff; "><div class="row no-gutters">
		<div class="col-lg-2"></div>
	 <div class="col-lg-10"><div class="row topmargin30">
		<div class="col-lg-6"><img src="../images/fadv.png" width="" height="" alt="" class="img-fluid"/></div>
	 <div class="col-lg-6 float-right">Developed by: <img src="../images/gic.png" width="" height="" alt="" class="img-fluid"/></div>
	 
	
	</div></div>
	</div></footer>

    </div>
                 <div class="container">
            <div class="row">
                <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                    data-toggle="modal" data-target="#myModal">
                    Launch demo modal</button>
                <div class="modal fade" id="myModal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">  Data Entry for   <asp:Literal id="litFaxId1" runat="server"></asp:Literal></h4>
                            </div>
                            <div class="modal-body">
                               You have completed all the pages for Fax Id  <asp:Literal id="litFaxId2" runat="server"></asp:Literal>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                               
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
             </div>
        
        <div class="modal fade" id="myModelAdd">

            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Change Password</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            

                            <div class="col-md-6">
                                 <label>Enter New Password</label>
                                          <asp:TextBox class="form-control" ID="txtnewpass" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="abc"  ControlToValidate="txtnewpass" ErrorMessage="Please Enter New Password"></asp:RequiredFieldValidator>
                            </div>


                            <div class="col-md-6">
                                <label> Re-Enter New Password</label>
                                 <asp:TextBox class="form-control" ID="txtretypepass" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="abc"  ControlToValidate="txtretypepass" ErrorMessage="Please Re-Enter New Password"></asp:RequiredFieldValidator>
                          
                                 </div>
                            <br />

                            <div class="modal-footer">
                                <%-- <button type="button" class="btn btn-default" id="close" runat="server" data-dismiss="modal">Close</button>--%>
                               
                                <asp:Button ID="BtnCreatpass" runat="server" OnClick="BtnCreatpass_Click" Text="Save"  OnClientClick="return valid(),StringCompare()" ValidationGroup="abc" class="btn btn-primary" />
                                 <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />

                            </div>


                        </div>

                    </div>

                </div>

            </div>
        </div>
    </form>
    <script src="../vendor/jquery/jquery.js"></script> 
<script src="../vendor/bootstrap/js/bootstrap.min.js"></script> 
<script src="../vendor/bootstrap/js/bootstrap-select.min.js"></script> 
    <script src="script/jquery-migrate-1.2.1.min.js"></script>
    <script src="script/jquery.menu.js"></script>
     <script>
        var acc = document.getElementsByClassName("accordion");
        var i;

        for (i = 0; i < acc.length; i++) {
            acc[i].onclick = function () {
                this.classList.toggle("active");
                var panel = this.nextElementSibling;
                if (panel.style.maxHeight) {
                    panel.style.maxHeight = null;
                } else {
                    panel.style.maxHeight = panel.scrollHeight + "px";
                }
            }
        }
        
 function StringCompare() {
           
             var pass = document.getElementById("<%=txtnewpass.ClientID%>").value;
            var repass = document.getElementById("<%=txtretypepass.ClientID%>").value;
            if (pass == repass) {
                //alert("Both values are same.");
            }
            else {
                alert("Password Not Match");
                return false;
            }
            
        }

         function valid() {
           var pass = document.getElementById("<%=txtnewpass.ClientID%>").value;
            var repass = document.getElementById("<%=txtretypepass.ClientID%>").value;
             
            if (pass == "" || repass == "")
           {
               alert("Fill all information");
               return false;
           }
           else {
              
                   return true;              
 
           }        
        }
    </script>
</body>
</html>
