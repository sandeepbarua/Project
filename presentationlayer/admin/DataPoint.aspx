<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="DataPoint.aspx.cs" Inherits="PresentationLayer.Template.DataPoint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            if (confirm("Do you want to Delete data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>
    <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }

        .auto-style1 {
            width: 99%;
        }
    </style>
    <script>
        function CheckNumeric(e) {
            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;
                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;
                }
            }
        }

    </script>

    <%--          <link href="../Admin/NewCss/bootstrap.min.css" rel="stylesheet" />  
        <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>  
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>  
  <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/jquery.bootstrapvalidator/0.5.3/js/bootstrapValidator.min.js"> </script>--%>

    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />
    <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.0/js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#Table1').dataTable();
        });
    </script>
    <script type="text/javascript">
        function OpenModalWindow() {
            $('#myModelAdd').modal('show');
        }
    </script>
    <asp:Panel ID="pnlMainBody" runat="server">
        <div class="main-panel">
            <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">DESIGN DOCUMENT</h4>
            </div>

            <div class="White_box ">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-4">

                                <asp:ValidationSummary
                                    ID="ValidationSummary1"
                                    runat="server"
                                    HeaderText="Following error occurs....."
                                    ShowMessageBox="false"
                                    DisplayMode="BulletList"
                                    ShowSummary="true"
                                    BackColor="Snow"
                                    ValidationGroup="added"
                                    Width="400"
                                    ForeColor="Red"
                                    Font-Size="Small"
                                    Font-Italic="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Document Name :</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:DropDownList ID="ddlDocumentType" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ValidationGroup="added" ControlToValidate="ddlDocumentType" InitialValue="0" runat="server" ErrorMessage="Please select template name" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label id="lblDropDownValue" runat="server">Data Point</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ValidationGroup="added" ControlToValidate="txtAddLabelName" runat="server" ErrorMessage="Please Enter Data Field" ForeColor="Red"></asp:RequiredFieldValidator>

                                <asp:TextBox class="form-control" ID="txtAddLabelName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Input Type</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" ValidationGroup="added" ControlToValidate="ddlAddControlType" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>

                                <asp:DropDownList ID="ddlAddControlType" class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAddControlType_SelectedIndexChanged">
                                    <asp:ListItem Text="Select Control" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="TextBox" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Calender" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="RadioButton" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" id="DropDownDiv" runat="server" visible="false" style="padding-top:0px">
                            <div class="col-lg-3"  style="padding-top:25px">
                              
                                <asp:Label ID="lblSelectValue" runat="server">Drop Down Values</asp:Label>
                            </div>
                            <div class="col-lg-4">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12"  ValidationGroup="added" ControlToValidate="txtDropDownValue" InitialValue="0" runat="server" Display="Dynamic" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:RegularExpressionValidator ID="rev" runat="server" Display="Dynamic" ControlToValidate="txtDropDownValue" ValidationGroup="added" ForeColor="Red"
                                    ErrorMessage="Spaces are not allowed!" ValidationExpression="[^\s]+" />
                                <asp:TextBox class="form-control" ID="txtDropDownValue" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row"  id="RadioDiv1" runat="server" visible="false" style="padding-bottom:0px; height:42px">
                            <div class="col-lg-3" style="padding-top:5px">
                                <asp:Label ID="lbltxtAddChoiceOne" runat="server">First Option</asp:Label>
                            </div>
                            <div class="col-lg-4" style="padding-bottom:0px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" ValidationGroup="added" ControlToValidate="txtAddChoiceOne" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>

                                <asp:TextBox class="form-control" ID="txtAddChoiceOne" runat="server"></asp:TextBox><br />
                            </div>
                        </div>
                        <div class="row"   id="RadioDiv2" runat="server" visible="false" style="padding-top:0px">
                            <div class="col-lg-3" style="padding-top:5px">
                                <asp:Label ID="lblAddChoiceTwo" runat="server">Second Option</asp:Label>
                            </div>
                            <div class="col-lg-4">
                              
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" ValidationGroup="added" ControlToValidate="txtAddChoiceTwo" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>

                                <asp:TextBox class="form-control" ID="txtAddChoiceTwo" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-4" align="right">
                                  <br />
                                <asp:Button ID="btmCreate" runat="server" class="mq-btn btn btn-primary nxt_btn" Text="Save" OnClick="btmCreate_Click" />
                                <asp:Button ID="btnCacel" runat="server" Text="Cancel" class="mq-btn btn btn-primary nxt_btn" OnClick="btnCacel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="CustomerID" runat="server" />






            <table id="Table1" style="margin-bottom: 2px" class="table  table-bordered table-striped display">

                <thead>
                    <tr>
                        <th style="width: 10px; text-align: center">S.No</th>

                        <th style="display: none">Document Type Name</th>
                        <th style="display: none">Document Type Name</th>
                        <th style="width: 150px; text-align: center">Label Name</th>
                        <%-- <th class="col" style="width: 100px; color: white; text-align: center">Control Name</th>--%>
                        <th style="width: 100px; text-align: center">Control Type</th>
                        <th style="width: 100px; text-align: center">Drop Down Values</th>
                        <th style="width: 100px; text-align: center">Order No</th>
                        <th style="width: 100px; text-align: center">Actions</th>

                    </tr>
                </thead>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <tbody>

                    <asp:Repeater ID="ReptUse" runat="server" OnItemDataBound="ReptUse_ItemDataBound">
                        <ItemTemplate>
                            <tr id="trID" runat="server">
                                <td id="serial" style="width: 57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                <%--<td class="classDynamicControlIDs" style="display: none">   <asp:HiddenField ID="hndorderno" runat="server" Value='<%#Eval("DynamicControlID")%>' /> <%# DataBinder.Eval(Container.DataItem, "DynamicControlID")%></td>--%>
                                <td class="classDynamicControlID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "DynamicControlID")%></td>

                                <%--<td id="DocumentTypeName" style="width: 150px" class="classDocumentTypeName"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%></td>--%>
                                <td id="DocumentTypeIDss" style="width: 100px; display: none" class="classDocumentTypeID"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeID")%></td>
                                <td id="UserName" style="width: 150px" class="classlabelName"><%# DataBinder.Eval(Container.DataItem, "labelName")%></td>
                                <%--                     <td id="CustomerID" style="width: 100px" class="classControlName"><%# DataBinder.Eval(Container.DataItem, "ControlName")%></td>--%>
                                <td class="classControlType"><%# DataBinder.Eval(Container.DataItem, "ControlType")%></td>
                                <td class="classDropDownValue"><%# DataBinder.Eval(Container.DataItem, "DropDownValue")%></td>
                                <%--<td class="classControlTypeID" style="display:none"><%# DataBinder.Eval(Container.DataItem, "ControlTypeID")%></td>--%>
                                <td style="width: 150px">
                                    <asp:TextBox ID="txtOrderNo" runat="server" onkeypress="CheckNumeric(event);" Width="40px" Text='<%#Eval("OrderBy") %>'></asp:TextBox></td>
                                <td id="actions" style="width: 150px">

                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID btn btn-outline-info">
                                              <i class="fa fa-pencil"></i> 
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbDelete" runat="server" ToolTip="Delete" class="btn  btn-outline-danger" OnClick="lbDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')">
                                              <i class="fa fa-trash-o"></i>
                                    </asp:LinkButton>
                                    <%--<asp:ImageButton ID="OnDelete" class="btn  btn-outline-danger" runat="server" ImageUrl="../Admin/Images/Delete.png" Height="18px" OnClick="OnDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')" />--%>

                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("DynamicControlID")%>' />
                                    <asp:HiddenField ID="HfLabelName" runat="server" Value='<%#Eval("labelName")%>' />
                                    <asp:HiddenField ID="hfDropDownValue" runat="server" Value='<%#Eval("DropDownValue")%>' />


                                </td>

                            </tr>

                        </ItemTemplate>

                    </asp:Repeater>



                    <caption>
                        <br />
                        <br />
                    </caption>

                </tbody>

            </table>
            <%--Edit Account--%>
            <div class="modal fade" id="myModal">
                <div class="modal-dialog">

                    <div class="modal-content">

                        <div class="modal-header bg-aqua-active">

                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                                <span aria-hidden="true">&times;</span></button>

                            <h4 class="modal-title">Edit Control</h4>

                        </div>

                        <div class="modal-body">

                            <div class="row">
                                <asp:ScriptManager ID="ScriptManager1" runat="Server"></asp:ScriptManager>
                                <div class="col-md-6">

                                    <label>Select Document Type *</label>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlEditDocumentType" Enabled="false" Width="220" Height="30" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added" ControlToValidate="ddlEditDocumentType" runat="server" InitialValue="0" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-md-6">
                                    <label>Label Name*</label>
                                    <asp:TextBox class="form-control" ID="txtEditLabelName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="added" ControlToValidate="txtEditLabelName" runat="server" ErrorMessage="Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>


                                    <input type="hidden" class="form-control" id="hiddenDynamicControlID" name="hiddenDynamicControlID" />

                                </div>
                                <div class="col-md-6">
                                    <label>Drop Down Values *</label>
                                    <asp:TextBox class="form-control" ID="txtEditDropDown" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="added" ControlToValidate="txtEditDropDown" runat="server" ErrorMessage="Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      
                            <div class="modal-footer">
                                <%--<asp:Button ID="BtnCreateuser" ValidationGroup="EditValida" runat="server" Text="Save Changes" class="btn btn-primary"  OnClick="BtnCreateuser_Click" />--%>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnorderno" runat="server" Visible="false" class="mq-btn btn btn-primary nxt_btn" OnClick="btnorderno_Click" Text="Submit Order No" />
            </div>
        </div>

    </asp:Panel>
    <%--create new Account--%>

    <%--        <div class="modal fade" id="myModelAdd">

            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Add Control</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-md-6">

                                <label>Template :  </label>
                                <asp:Label ID="lblDocumentType" runat="server" class="form-control"></asp:Label>
                            </div>

                            <div class="col-md-6">
                                <label>Enter Data Field</label>
                                <asp:TextBox class="form-control" ID="txtAddLabelName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="abc" ControlToValidate="txtAddLabelName" runat="server" ErrorMessage="Please Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                <input type="hidden" class="form-control" id="ddlAccountId1" name="ddlAccountId1" />

                            </div>
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <label>Select Control Type</label>

                                        <asp:DropDownList ID="ddlAddControlType" class="form-control" Height="30px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAddControlType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select Control" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="TextBox" Value="1"></asp:ListItem>

                                            <asp:ListItem Text="Calender" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="RadioButton" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="abc" ControlToValidate="ddlAddControlType" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server"> 
                                    <ContentTemplate>

                                        <asp:Label ID="lblDropDownValue" runat="server" Visible="false">Enter Select Value</asp:Label>

                                        <asp:TextBox class="form-control" ID="txtDropDownValue" runat="server" Visible="false"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" ValidationGroup="abc" ControlToValidate="txtDropDownValue" runat="server" ErrorMessage="Please Enter Value" ForeColor="Red"></asp:RequiredFieldValidator>
                                         
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-6">

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbltxtAddChoiceOne" runat="server" Visible="false">Enter Choice One</asp:Label>
                                        <asp:TextBox class="form-control" ID="txtAddChoiceOne" runat="server" Visible="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="abc" ControlToValidate="txtAddChoiceOne" runat="server" ErrorMessage="Enter Choice One" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-6" style="margin-top: 10px">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblAddChoiceTwo" runat="server" Visible="false">Enter Choice Two</asp:Label>

                                        <asp:TextBox class="form-control" ID="txtAddChoiceTwo" runat="server" Visible="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="abc" Display="Dynamic" ControlToValidate="txtAddChoiceTwo" runat="server" ErrorMessage="Enter Choice Two" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="row"></div>
                            <div class="modal-footer" style="margin-top: 10px">
                                <%-- <button type="button" class="btn btn-default" id="close" runat="server" data-dismiss="modal">Close</button>--%>

    <%-- <asp:Button ID="BtnCreateuser" runat="server" Text="Save" OnClick="BtnCreateuser_Click" ValidationGroup="abc" class="btn btn-primary" />
                                <asp:Button ID="close" OnClick="close_Click" runat="server" Text="Close" class="btn btn-default" />--%>

                            </div>


                        </div>

                    </div>

                </div>

            </div>
        </div>
        <script>
            $(function () {

                $(".editID").click(function () {
                    $("[id*=hiddenDynamicControlID]").val($.trim($(".classDynamicControlID", $(this).closest("tr")).html()));
                    $("[id*=ddlEditDocumentType]").val($.trim($(".classDocumentTypeID", $(this).closest("tr")).html()));
                    $("[id*=txtEditLabelName]").val($.trim($(".classlabelName", $(this).closest("tr")).html()));
                    $("[id*=ddlEditControlType]").val($.trim($(".classControlTypeID", $(this).closest("tr")).html()));
                    $("[id*=txtEditDocumentDescription]").val($.trim($(".classDocumentDescription", $(this).closest("tr")).html()));
                    $("[id*=txtEditDropDown]").val($.trim($(".classDropDownValue", $(this).closest("tr")).html()));
                });
            });

        </script>
    <script>
        $('#Table1').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 4 }], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
    </script>
</asp:Content>
