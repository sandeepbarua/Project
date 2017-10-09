<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="TamplateAuditQuestion.aspx.cs" Inherits="PresentationLayer.Admin_New.TamplateAuditQuestion" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Delete data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        <%--function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };--%>

        <%--function HideLabelError() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessageError.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };--%>
        <%--function HideLabelErrorDelete() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessageErrorDelete.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };--%>
    </script>
    <script type="text/javascript">

        function valid() {
            var v_type = document.getElementById("<%=txtAuditQuestion.ClientID%>").value;
            <%--var customer_val = document.getElementById("<%=ddlAddCompany.ClientID%>").value;--%>
            var document_val = document.getElementById("<%=ddlAddDocumentType.ClientID%>").value;
            //var sales = document.getElementById("editor1").value;
            if (v_type == "" || customer_val == "" || document_val == "" || customer_val == "Select Customer" || customer_val == "0" || document_val == "Select Document Type" || document_val == "0") {
                alert("Fill all information");
                return false;
            }
            else {

                return true;

            }
        }

        function validedit() {
            var v_type = document.getElementById("<%=txtEditAuditQuestion.ClientID%>").value;
            //var customer_val = document.getElementById("<%--<%=ddlEditSelectCompany.ClientID%>--%>").value;
            var document_val = document.getElementById("<%=ddlEditDocumentType.ClientID%>").value;

            //var sales = document.getElementById("editor1").value;
            if (v_type == "" || customer_val == "" || document_val == "" || customer_val == "Select Customer" || document_val == "Select Document Type" || customer_val == "0" || document_val == "0") {
                alert("Fill all information");
                return false;
            }
            else {

                return true;
            }
        }
    </script>

    <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }

        .auto-style1 {
            width: 52%;
        }
    </style>

    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />
    <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.0/js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#Table1').dataTable();
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="Server"></asp:ScriptManager>
    <asp:Panel ID="pnlMainBody" runat="server">
        <%-- <div class="box-header">
                    <div class="row" align="right">
                        <center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </center>
                                            
                        <asp:LinkButton ID="btnadd" runat="server" ToolTip="Add New Control" class="btn btn-sm" data-toggle="modal" data-target="#myModelAdd" OnClick="btnadd_Click">

                            <asp:Image ID="Image1" runat="server" ImageUrl="../Admin/Images/Add.png" Width="20" Height="20" />
                        </asp:LinkButton>
                    </div>
                </div>--%>
        <!-- /.box-header -->
        <div class="main-panel">
           

                <div class="modal-header bg-aqua-active">
                    <h4 class="modal-title">AUDIT ENGINE</h4>
                </div>

                <div class="White_box " style="padding-bottom:0px">
                    <div class="row" style="padding-bottom:0px">
                        <div class="col-lg-8" style="padding-bottom:0px">
                            <div class="row">
                                <div class="col-lg-3">
                                    <label>Template</label>
                                </div>
                                <div class="col-lg-5">
                                    <asp:DropDownList ID="ddlDocumentType" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="added" Display="Dynamic" ControlToValidate="ddlDocumentType" runat="server" InitialValue="0" ErrorMessage="Please Select Template" ForeColor="Red"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <label>Audit Question</label>
                                </div>
                                <div class="col-lg-5">
                                    <asp:TextBox ID="txtNewAuditQuestion" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="added" Display="Dynamic" ControlToValidate="txtNewAuditQuestion" runat="server" InitialValue="0" ErrorMessage="Please Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <label>Rule</label>
                                </div>
                                <div class="col-lg-5">
                                    <asp:RadioButton ID="Rb_Logical" runat="server" Text="Logical" AutoPostBack="true" GroupName="QuestionType" OnCheckedChanged="Rb_Logical_CheckedChanged"  />

                                    <asp:RadioButton ID="Rb_Calculation" runat="server" Text="Calculation" GroupName="QuestionType" AutoPostBack="true" OnCheckedChanged="Rb_Calculation_CheckedChanged" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom:0px">
                                <div class="col-lg-3" style="padding-bottom:0px">
                                    <label>Rule Type</label>
                                </div>
                                <div class="col-lg-5" style="padding-bottom:0px">
                                    <asp:DropDownList ID="ddlSelectFunction" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectFunction_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="added" Display="Dynamic" ControlToValidate="ddlSelectFunction" runat="server" InitialValue="0" ErrorMessage="Please Select function" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="White_box " runat="server" id="DivShowHide" visible="false" style="padding-top:0px">
                    <div class="row">
                        <div class="col-lg-8">
                            <asp:Repeater ID="rptDynamicControl" runat="server" OnItemDataBound="rptDynamicControl_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row">

                                        <div class="col-lg-3">
                                            <label><%# DataBinder.Eval(Container.DataItem, "ParameterName")%></label>
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div id="dvDropDown" runat="server">
                                                        <asp:DropDownList ID="ddl_Parameters" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="myDropdown_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredField" Display="Dynamic" ValidationGroup="added" ControlToValidate="ddl_Parameters" runat="server" InitialValue="0" ErrorMessage="Please Select Data field" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_Parameters" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="row">
                                <div class="col-lg-3">
                                    <label>Expression</label>
                                </div>
                                <div class="col-lg-5">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox class="form-control" ID="txtExpression"  TextMode="MultiLine" runat="server"></asp:TextBox></td>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-5" align="right">
                                    <asp:Button ID="BtnSubmitExpression" runat="server" ValidationGroup="added" Text="Submit" class="mq-btn btn btn-primary nxt_btn" OnClick="BtnSubmitExpression_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Close" class="mq-btn btn btn-primary nxt_btn" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>










                <%--                <div class="row" style=" padding-left: 30px;">
                
                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <table align="left" class="auto-style1">                            
                            <asp:Repeater ID="rptDynamicControl" runat="server" OnItemDataBound="rptDynamicControl_ItemDataBound">
                                <ItemTemplate>
                                    <tr >
                                        <td width="30%"><%# DataBinder.Eval(Container.DataItem, "ParameterName")%></td>
                                        <td  style="padding-left:26%">

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div id="dvDropDown" runat="server">
                                                        <asp:DropDownList ID="ddl_Parameters" Width="100%" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="myDropdown_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredField" Display="Dynamic" ValidationGroup="added" ControlToValidate="ddl_Parameters" runat="server" InitialValue="0" ErrorMessage="Please Select Data field" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_Parameters" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>                            
                            <tr>
                                <td>Expression</td>
                                <td style="padding-left:90px; padding-top:10px">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                    <asp:TextBox class="form-control" ID="txtExpression" Width="190px" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </td>
                            </tr>
                            <tr>
                                
                                <td></td>
                                <td style="padding-top:30px; padding-left:100px">
                                    <asp:Button ID="BtnSubmitExpression" runat="server" ValidationGroup="added" Text="Submit" class="mq-btn btn btn-primary nxt_btn" OnClick="BtnSubmitExpression_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Close" class="mq-btn btn btn-primary nxt_btn" OnClick="btnCancel_Click"/>
                                </td>

                            </tr>

                        </table>
                    </asp:Panel>
                          
               </div>--%>
         
        </div>




        <table id="Table1" style="margin-bottom: 2px" class="table  table-bordered table-striped display">

                <thead>
                    <tr>
                        <th class="col" style="width: 5%; text-align: center">S.No</th>

                        <%--<th class="col" style="width: 150px; color: white; text-align: center">Document Type Name</th>--%>
                        <th class="col" style="width: 45%; text-align: center">Audit Question</th>
       <%--                 <th class="col" style="width:  60px; text-align: center">Template</th>--%>
                        <%--<th class="col" style="width: 150px; color: white; text-align: center">Company Name</th>--%>
                        <%--<th class="col" style="width: 100px; color: white; text-align: center">Added By</th>--%>
                        <th class="col" style="width: 35%; text-align: center">Rule</th>
                        <th class="col" style="display: none">Edit Rule</th>
                        <th class="col" style="display: none">Edit Rule</th>
                        <th class="col" style="display: none">Edit Rule</th>
                        <th class="col" style="display: none">Edit Rule</th>

                        <th class="col" style=" width: 10%; text-align: center">Actions</th>

                    </tr>
                </thead>

                <tbody>

                    <asp:Repeater ID="ReptUse" runat="server" OnItemDataBound="ReptUse_ItemDataBound">
                        <ItemTemplate>
                            <tr id="trID" runat="server">
                                <td id="serial" style="width: 57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                <td class="classAuditQuestion"><%# DataBinder.Eval(Container.DataItem, "AuditQuestion")%></td>
                             <%--   <td id="DocumentTypeName" style="width: 150px" class="classDocumentTypeName"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%></td>--%>
                                <%--<td id="CompanyName" style="width:150px" class="classCompanyName"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></td>--%>
                                <%--<td id="UserName" style="width:150px" class="classUserName"><%# DataBinder.Eval(Container.DataItem, "UserName")%></td>--%>
                                <td id="UserName" style="width: 150px" class="classUserName">
                                    <asp:Repeater ID="Reptrule" runat="server">

                                        <HeaderTemplate>
                                            <table class="table  table-bordered table-striped display">
                                                <thead>
                                                    <tr style="background-color: #D6D6D6">
                                                        <th class="col" style="width: 50%; color: dimgray; text-align: center">Label Name</th>
                                                        <th class="col" style="width: 50%; color: dimgray; text-align: center">Function</th>
                                                    </tr>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td id="123"><%# DataBinder.Eval(Container.DataItem, "labelName")%></td>
                                                <td id="432"><%# DataBinder.Eval(Container.DataItem, "RuleType")%></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>




                                    </asp:Repeater>
                                </td>

                                <td id="CustomerID" style="width: 100px; display: none" class="classCustomerID"><%# DataBinder.Eval(Container.DataItem, "CustomerID")%></td>
                                <td class="classUserDetailsID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "UserID")%></td>
                                <td class="classDynamicControlID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "AuditQuestionsID")%></td>
                                <td class="classDocumentTypeID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "DocumentID")%></td>

                                <td id="actions" style="width: 10%">

                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID btn btn-outline-info" data-toggle="modal" data-target="#myModal">
                                              <i class="fa fa-pencil"></i> 
                                    </asp:LinkButton>

                                    <%-- <asp:ImageButton ID="OnEdit" runat="server" ImageUrl="Images/edit.png" Height="18px" OnClick="btnEdit_Click"  />--%>

                                    <asp:LinkButton ID="OnDelete" runat="server" class="btn  btn-outline-danger" ToolTip="Delete" OnClick="OnDelete_Click"  OnClientClick = "return confirm('Do you want to Delete data?')"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                    <%--<asp:ImageButton ID="OnDelete" runat="server" ImageUrl="../Admin/Images/Delete.png" Height="18px" OnClick="OnDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')" />--%>

                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("AuditQuestionsID")%>' />
                                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("DocumentID")%>' />
                                    <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("CustomerID")%>' />
                                    <asp:HiddenField ID="HiddenField4" runat="server" Value='<%#Eval("AuditQuestion")%>' />

                                    <%--<asp:LinkButton ID="lnkBtnCreateRule" runat="server" OnClick="lnkBtnCreateRule_Click" class="btn  btn-outline-success"  ToolTip="Create Rule">
                                               <i class="fa fa-clipboard"></i> 
                                    </asp:LinkButton>--%>
                                </td>

                            </tr>


                        </ItemTemplate>

                    </asp:Repeater>


                </tbody>

            </table>
            <div id="bottom_anchor"></div>
        

        <%--Edit Account--%>
        <div class="modal fade" id="myModal">
            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Edit Audit</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                          



                            <%-- <div class="col-md-6">

                                        <label>Select Company *</label>
                                          <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="ddlEditSelectCompany" OnSelectedIndexChanged="ddlEditSelectCompany_SelectedIndexChanged" AutoPostBack="True" class="form-control" runat="server">

                                              </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="addEdit"   ControlToValidate="ddlEditSelectCompany" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                                                </div> --%>


                            <div class="col-md-6">

                                <label>Select Document Type *</label>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEditDocumentType" class="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added" ControlToValidate="ddlEditDocumentType" runat="server" InitialValue="0" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>


                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-6">
                                <label>Enter Audit Question *</label>
                                <asp:TextBox class="form-control" ID="txtEditAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="EditValida" ControlToValidate="txtEditAuditQuestion" runat="server" ErrorMessage="Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>

                                <input type="hidden" class="form-control" id="hiddenDynamicControlID" name="hiddenDynamicControlID" />

                            </div>



                            <div class="modal-footer">

                                <asp:Button ID="btnUpdate" ValidationGroup="EditValida" runat="server" Text="Save Changes" class="btn btn-primary" OnClientClick="return validedit();" OnClick="btnUpdate_Click" />
                                <button type="iCancel" class="btn btn-primary" data-dismiss="modal">Cancel</button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <%--create new Account--%>

        <div class="modal fade" id="myModelAdd">

            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Add Audit Question</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">

                            <%--//----------------------------------%>
                            <%--<div class="col-md-6">
                                 <label>Select Company *</label>
                                          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="ddlAddCompany" class="form-control" OnSelectedIndexChanged="ddlAddCompany_SelectedIndexChanged" AutoPostBack="True" runat="server">
                                              </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="add"  ControlToValidate="ddlAddCompany" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                            </div>--%>


                            <div class="col-md-6">
                                <label>Select Document Type *</label>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <asp:DropDownList ID="ddlAddDocumentType" class="form-control" runat="server" Width="240px" Height="34px" AutoPostBack="True" Enabled="false">
                                        </asp:DropDownList>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="abc" ControlToValidate="ddlAddDocumentType" InitialValue="0" runat="server" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>


                            </div>

                            <div class="col-md-6">

                                <label>Audit Question :  </label>
                                <asp:TextBox class="form-control" ID="txtAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="add" ControlToValidate="txtAuditQuestion" runat="server" ErrorMessage="Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>





                            <div class="modal-footer">
                                <%-- <button type="button" class="btn btn-default" id="close" runat="server" data-dismiss="modal">Close</button>--%>

                                <asp:Button ID="BtnCreateuser" runat="server" Text="Save" OnClick="BtnCreateuser_Click" OnClientClick="return valid()" ValidationGroup="abc" class="btn btn-primary" />
                                <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />

                            </div>


                        </div>

                    </div>

                </div>

            </div>
        </div>
    </asp:Panel>

    <script>
        $(function () {
            $(".editID").click(function () {
                $("[id*=hiddenDynamicControlID]").val($.trim($(".classDynamicControlID", $(this).closest("tr")).html()));
                $("[id*=txtEditAuditQuestion]").val($.trim($(".classAuditQuestion", $(this).closest("tr")).html()));
                $("[id*=ddlEditDocumentType]").val($.trim($(".classDocumentTypeID", $(this).closest("tr")).html()));
                $("[id*=ddlEditSelectCompany]").val($.trim($(".classCustomerID", $(this).closest("tr")).html()));
            });
        });
    </script>
    <script>
        function OpenModalWindow() {
            $('#myModelWindow').modal('show');
        }
    </script>


    <script>
        $('#Table1').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 4 }], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
    </script>
</asp:Content>

