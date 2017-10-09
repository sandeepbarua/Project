<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QCDataEntry.aspx.cs" Inherits="PresentationLayer.Admin.QCDataEntry" %>
<%@ Register Src="~/UserControls/UsrConvictionDetails.ascx" TagName="TgConvictionDetails" TagPrefix="UCConvictionDetails" %>
<%@ Register Src="~/UserControls/UsrDriverLicenceDetail.ascx" TagName="TgDriverLicence" TagPrefix="UCDriverLicence" %>
<%@ Register Src="~/UserControls/UsrPreviousEmploymentDetail.ascx" TagName ="TgPreviousEmployment" TagPrefix="UCPreviousEmployment" %>


<%@ Register Src="~/UserControls/UsrCurrentResidenceDetail.ascx" TagName ="TgCurrentResidence" TagPrefix="UCCurrentResidence" %>
<%@ Register Src="~/UserControls/UsrPreviousResidenceDetail.ascx" TagName ="TgPreviousResidence" TagPrefix="UCPreviousResidence" %>


<%@ Register Src="~/UserControls/UsrTrafficConvictionsDetail.ascx" TagName ="TgTrafficConvictions" TagPrefix="UCTrafficConvictions" %>

<%@ Register Src="~/UserControls/UsrPreviousEmployerDetail.ascx" TagName ="TgPreviousEmployer" TagPrefix="UCPreviousEmployer" %>

<%@ Register Src="~/UserControls/UsrCEDPreviousEmployerDetail.ascx" TagName ="TgCEDPreviousEmployer" TagPrefix="UCCEDPreviousEmployer" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="/images/icons/favicon.ico" />
    <link rel="apple-touch-icon" href="/images/icons/favicon.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="/images/icons/favicon-72x72.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="/images/icons/favicon-114x114.png" />
    <!--Loading bootstrap css-->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <script src="../JavaScript/fusioncharts.charts.js" type="text/javascript"></script>
    <script src="../JavaScript/fusioncharts.js" type="text/javascript"></script>
    <script src="../JavaScript/plugins/jQuery/jquery-2.2.3.min.js" type="text/javascript"></script>
    <script src="../JavaScript/bootstrap.js" type="text/javascript"></script>
    <script src="../JavaScript/plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../JavaScript/plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScript/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../JavaScript/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.js" type="text/javascript"></script>
    <script src="../JavaScript/app.min.js" type="text/javascript"></script>

    <script src="../tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script src="../news/jcarousellite_1.0.1c4.js" type="text/javascript"></script>

    <style type="text/css">
        @font-face {
            font-family: "The Example Font";
            src: url("fonts/Roboto-Regular.ttf">fonts/Roboto-Regular.ttf");
        }

        body.ExampleFont {
            font-family: "The Example Font", Verdana;
        }
    </style>
    <style>
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
    width: 40%;
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

    <script src="../JavaScript/plugins/morris/morris.min.js" type="text/javascript"></script>
    <script src="../JavaScript/plugins/morris/vector.js" type="text/javascript"></script>




    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../Admin/styles/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
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
    
    <style>
.table {  
    color: #333; /* Lighten up font color */
    font-family: Helvetica, Arial, sans-serif; /* Nicer font */
    width: 100%; 
    border-collapse: 
    collapse; border-spacing: 0; 
}

.table td, th { border: 1px solid #CCC; height: 30px; } /* Make cells a bit taller */

.table th {  
    background: #F3F3F3; /* Light grey background */
    font-weight: bold; /* Make sure they're bold */
}

.tableth {  
    background: #F3F3F3; /* Light grey background */
    font-weight: bold; /* Make sure they're bold */
}

.table td {  
    background: #FAFAFA; /* Lighter grey background */
    text-align: center; /* Center our text */
}
    </style>
 <script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();
        }

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
     <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    
    <script>

        $(function () {
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
            "Incomplete", "Missing", "Illegible"
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
       
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table border="0" width="100%">
            <tr>
                <td>
                    <h2>


                        <img src="../Admin/Images/logo.png" />
                    </h2>
                </td>
                <td>
                    
                    <div style="padding-top:20px; width: 98%" align="right">
                        <div class="menu">

        <ul>

           

            <li><a href="#">Welcome, <asp:Label ID="lblUserName" runat="server"></asp:Label></a>

                <ul>

                   <li><asp:LinkButton ID="lnkbtn_changepassword" runat="server" Text="Change Password" data-toggle="modal" data-target="#myModelAdd"></asp:LinkButton>
                    </li>
                    <li><asp:LinkButton ID="btnSignOut" runat="server" Text="Sign Out" OnClick="btnSignOut_Click"></asp:LinkButton>
                        </li>

               </ul>

          </li>          

        </ul>

    </div>

                                
                        &nbsp; &nbsp;	&nbsp;      
                        <br />
                        </div>

                </td>
            </tr>
        </table>
        <nav id="sidebar" role="navigation" data-step="2" data-intro="Template has &lt;b&gt;many navigation styles&lt;/b&gt;"
                    data-position="right" class="navbar-default navbar-static-side">
         <div class="sidebar-collapse menu-scroll" id="div_top_hypers" >
                        <ul id="ul_top_hypers" runat="server" style="background-color:#263238; height:25px; color:White;">
                    
                       
                                        <li id="AccessUser" runat="server" class=""><a href="UserDetail.aspx"><span class="menu-title"><font color="White"><b>Manage Users</b></font></span></a>
                </li>
                             
                           
                            <li id="AccessCustomers" runat="server" class="active"><a href="Customers.aspx"><span class="menu-title"><font color="White"><b>Manage Customers</b></font></span></a>
                          
                    </li>
                              <li id="AccessLocations" runat="server" class=""><a href="Locations.aspx"><span class="menu-title"><font color="White"><b>Manage Customer Location</b></font></span></a>
                      
                    </li>
                          <li id="AccessDocumentType" runat="server" class=""><a href="DocumentType.aspx"><span class="menu-title"><font color="White"><b>Document Creation</b></font></span></a>
                       
                    </li>
                  
            
           
                    <li id="AccessDynamicControl" runat="server" class=""><a href="DynamicControl.aspx"><span class="menu-title"><font color="White"><b>Data Entry Form Creation</b></font></span></a>
                       
                    </li>
               
                    

                    
                     <li id="AccessAuditQuestion" runat="server" class=""><a href="AuditQuestion.aspx"><span class="menu-title"><font color="White"><b>Manage Audit Question</b></font></span></a>
                     
                    </li>
                    <li id="AccessExcepionData" runat="server" class=""><a href="ExceptionData.aspx"><span class="menu-title"><font color="White"><b>Manage Exception Data</b></font></span></a>
                     
                    </li>
                         
          <li id="AccessReport" runat="server" class=""><a href="Reports.aspx"><span class="menu-title"><font color="White"><b>Reports</b></font></span></a>
                       
                    </li>


                </ul>
            </div>
        </nav>
  



        <div class="row">
            <div class="col-md-5 " style="text-align: left;">

                <div class="">
                </div>
            </div>


        </div>



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
         <asp:Label ID="lblSessionTime" runat="server" Text="Label" ></asp:Label>
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
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-11">

                    <label class="text" for="lName" style="margin-bottom: 9px">
                        FAX ID :
                    </label>

                    <asp:Label ID="lblFaxId" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfTempCPScreenDataID" runat="server" />
                    <asp:HiddenField ID="hfTempAssignmentId" runat="server" />
                    <asp:HiddenField ID="hfTaskOperationId" Value="0" runat="server" />


                </div>

                <div class="main-panel">
                    <div class="modal-body">

                        <div class="row">
                            <table width="70%" align="Center">
                                 <tr>

                                    <td width="25%" class="auto-style4">
                                        <label>
                                            User Name
                                        </label>
                                    </td>
                                    <td width="50%" class="auto-style4">
                                        <asp:DropDownList ID="ddlUsers" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </td>
                                    <td width="25%" class="auto-style4">
                                        <label>
                                           
                                        </label>
                                    </td>
                                    <td class="auto-style5">
                                      
                                    </td>
                                </tr>
                                <tr>

                                    <td width="25%" class="auto-style4">
                                        <label>
                                            Customer Name
                                        </label>
                                    </td>
                                    <td width="50%" class="auto-style4">
                                        <asp:DropDownList ID="ddlCustomer" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="25%" class="auto-style4">
                                        <label>
                                            Document Type
                                        </label>
                                    </td>
                                    <td class="auto-style5">
                                        <asp:DropDownList ID="ddlDocumentType" runat="server" class="form-control" Enabled="false" Width="400px" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:HiddenField ID="hfDbOperation" runat="server" Value="Add" />
                                        <asp:HiddenField ID="hfIsReviewed" runat="server" Value="0" />
                                        
                                        <asp:HiddenField ID="hfNewDriver" Value="0" runat="server" />
                                    </td>
                                </tr>
                                <tr>

                                    <td width="25%">
                                        <label>
                                            Location
                                        </label>
                                    </td>
                                    <td width="50%">
                                        <asp:DropDownList ID="ddlLocation" Enabled="false" Width="220" Height="30" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:HiddenField ID="hfLocationId" runat="server" />
                                        <asp:Label ID="lblLocatoinError" runat="server" Visible="false" Text="Not Matched" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td width="25%">

                                        <label>
                                            Start Page&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtStartPage" runat="server" Width="51px" Text="1" Enabled="false"></asp:TextBox>
                                        </label>
                                        <asp:HiddenField ID="hfEndPageId" runat="server" />
                                        <asp:HiddenField ID="hfCountForFaxId" runat="server" Value="1" />

<%--                                        <label>
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtStartPage" Display="Dynamic"
                                                ErrorMessage="Enter Start Page" ForeColor="Red" ValidationGroup="grp1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="txtEndPage" Display="Dynamic"
                                                ErrorMessage="Only Numeric" ForeColor="Red" ValidationGroup="grp1" ValidationExpression="\d+"></asp:RegularExpressionValidator>

                                        </label>--%>
                                        <td style="justify-content: flex-end; margin-bottom: 10px" class="auto-style3">
                                            <label>
                                                &nbsp;&nbsp; End Page&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtEndPage" runat="server" Width="51px"></asp:TextBox>
                                            </label>

                                            <asp:HiddenField ID="hfStartPageId" runat="server" />
                                       <%--     &nbsp;<label><asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtEndPage" Display="Dynamic"
                                                ErrorMessage="Enter End Page" ForeColor="Red" ValidationGroup="grp1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev3" runat="server" ControlToValidate="txtEndPage" Display="Dynamic"
                                                    ErrorMessage="Only Numeric" ForeColor="Red" ValidationGroup="grp1" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                <asp:CompareValidator  Display="Dynamic" runat="server" id="cmpNumbers" controltovalidate="txtStartPage" controltocompare="txtEndPage"  ForeColor="Red" ValidationGroup="grp1" operator="LessThanEqual" type="Integer" errormessage="The Start page should be smaller than equal to the End page" />
                                            
                                                 </label>--%>
                                        </td>
                                    </td>

                                    <td class="auto-style2">&nbsp;<td style="width: 10%; justify-content: flex-end; margin-bottom: 10px">&nbsp;</td>
                                    </td>





                                </tr>
                            </table>
                        </div>

                    </div>
                </div>






                <div id="wrapper">

                    <div id="page-wrapper">
                        <!--BEGIN TITLE & BREADCRUMB PAGE-->

                        <div class="row">
                            <div class="small-12 medium-11 medium-centered large-9 large-centered columns">
                                <div class="row forms">
                                    <div id="maindiv" style="display: block; background-color: #ECEFF1;" runat="server">
                                        <div class="box box-primary">

                                            <div class="box box-primary">





                                                <div class="col-md-1 "></div>





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
                                                <div class="container" style="width: 960px; padding-bottom: 10px;">
                                                    <asp:HiddenField ID="hfRepeaterBound" runat="server" Value="0" />
                                                    <table>
                                                        <tr>
                                                            <td>
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
                                                                                    <asp:DropDownList class="form-control" ID="tone" runat="server" onchange="ManageControl(this.id , this.value)">
                                                                                    </asp:DropDownList>
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

                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="margin-top: 20px;">
                                                    

                             <div>
                               
                               
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
                                      
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                              

                            </div>


                                                    </div>

                                                    <div style="margin-top: 20px;">

                                                        <div class="col-md-7">
                                                        </div>
                                                        <div class="col-md-11" align="right">

                                                            <asp:Button ID="btnNext" Text="Next" runat="server" OnClick="btnNext_Click" Visible="false" ValidationGroup="grp1" class="btn btn-primary"  OnClientClick="return  validateNextButton();" />


                                                        </div>


                                                    </div>
                                                    <div class="row" style="margin-top: 80px;">
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
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
                               
                                <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />

                            </div>


                        </div>

                    </div>

                </div>

            </div>
        </div>

    </form>
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
    <div class="footer">Powered by <a href="http://www.gridinfocom.com/" rel="noopener noreferrer" target="_blank"><strong>@GridInfocom.com</strong></a>.</div>

</body>
</html>
