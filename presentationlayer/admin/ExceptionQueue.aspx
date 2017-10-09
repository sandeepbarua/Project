<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="ExceptionQueue.aspx.cs" Inherits="PresentationLayer.Admin.ExceptionQueue" %>
<%@ Register Src="~/UserControls/UsrConvictionDetails.ascx" TagName="TgConvictionDetails" TagPrefix="UCConvictionDetails" %>
<%@ Register Src="~/UserControls/UsrDriverLicenceDetail.ascx" TagName="TgDriverLicence" TagPrefix="UCDriverLicence" %>
<%@ Register Src="~/UserControls/UsrPreviousEmploymentDetail.ascx" TagName ="TgPreviousEmployment" TagPrefix="UCPreviousEmployment" %>


<%@ Register Src="~/UserControls/UsrCurrentResidenceDetail.ascx" TagName ="TgCurrentResidence" TagPrefix="UCCurrentResidence" %>
<%@ Register Src="~/UserControls/UsrPreviousResidenceDetail.ascx" TagName ="TgPreviousResidence" TagPrefix="UCPreviousResidence" %>


<%@ Register Src="~/UserControls/UsrTrafficConvictionsDetail.ascx" TagName ="TgTrafficConvictions" TagPrefix="UCTrafficConvictions" %>

<%@ Register Src="~/UserControls/UsrPreviousEmployerDetail.ascx" TagName ="TgPreviousEmployer" TagPrefix="UCPreviousEmployer" %>

<%@ Register Src="~/UserControls/UsrCEDPreviousEmployerDetail.ascx" TagName ="TgCEDPreviousEmployer" TagPrefix="UCCEDPreviousEmployer" %>
<%@ Register Src="~/UserControls/UsrViolationDetails.ascx" TagName ="TgViolationDetails" TagPrefix="UCUsrViolationDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    
         <div class="container">
             <asp:ScriptManager runat="server"></asp:ScriptManager>
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
   <div class="col-lg-10 page-content-wrapper"> 
        
       <div class="top_content">
      <h3>FAX ID:   <asp:Label ID="lblFaxId" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfTempCPScreenDataID" runat="server" />
                    <asp:HiddenField ID="hfTempAssignmentId" runat="server" />
                    <asp:HiddenField ID="hfTaskOperationId" Value="0" runat="server" /> </h3>
      <div class="row">
        <div class="col-lg-4">
          <div class="row">
            <div class="col-lg-6">
              <label>User Name</label>
            </div>
            <div class="col-lg-6">
                        <asp:DropDownList ID="ddlUsers" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged" >
                                        </asp:DropDownList>
                            
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6">
              <label>Customer Name</label>
            </div>
            <div class="col-lg-6">
                 <asp:DropDownList ID="ddlCustomer" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                        </asp:DropDownList>
                                  
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6">
              <label>Location</label>
            </div>
            <div class="col-lg-6">
                    <asp:DropDownList ID="ddlLocation" Enabled="false" Width="220" Height="30" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:HiddenField ID="hfLocationId" runat="server" />
                                        <asp:Label ID="lblLocatoinError" runat="server" Visible="false" Text="Not Matched" ForeColor="Red"></asp:Label>
                             
            </div>
          </div>
        </div>
        <div class="col-lg-8">
          <div class="row">
            <div class="col-lg-6">
              <label>&nbsp;</label>
            </div>
            <div class="col-lg-6"></div>
          </div>
          <div class="row">
            <div class="col-lg-2">
              <label>Document Type</label>
            </div>
            <div class="col-lg-10">
               <asp:DropDownList ID="ddlDocumentType" runat="server" class="form-control" Enabled="false" Width="400px" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:HiddenField ID="hfDbOperation" runat="server" Value="Add" />
                                        <asp:HiddenField ID="hfIsReviewed" runat="server" Value="0" />
                                        
                                        <asp:HiddenField ID="hfNewDriver" Value="0" runat="server" />
            </div>
          </div>
          <div class="row">
            <div class="col-lg-2">
              <label>Start Page</label>
            </div>
            <div class="col-lg-2">
               <asp:TextBox ID="txtStartPage" runat="server" Width="51px" Text="1" Enabled="false"></asp:TextBox>
                <asp:HiddenField ID="hfEndPageId" runat="server" />
                                        <asp:HiddenField ID="hfCountForFaxId" runat="server" Value="1" />

            </div>
            <div class="col-lg-2">
              <label>End Page</label>
            </div>
            <div class="col-lg-2">
               <asp:TextBox ID="txtEndPage" runat="server" Width="51px"></asp:TextBox>
            </div>
              <asp:HiddenField ID="hfStartPageId" runat="server" />
          </div>
        </div>
      </div>
    </div>
        <div class="grey_box">
        <div runat="server" id="dvMainContainer">
        <div class="row">
          <div class="col-lg-8">
            <div class="row">   
              <div class="container">
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
                                        <UCUsrViolationDetails:TgViolationDetails ID="TgViolationDetails1" runat="server" Visible="false" />
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
</asp:Content>
