<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="TemplateDocumnetType.aspx.cs" Inherits="PresentationLayer.Admin_New.TemplateDocumnetType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        function HideLabelError() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessageError.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
        function HideLabelErrorDelete() {

            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessageErrorDelete.ClientID %>").style.display = "none";
             }, seconds * 1000);
         };
    </script>
    <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />  
        <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>  
        <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.0/js/jquery.dataTables.js"></script>  
    <script>  
            $(document).ready(function() {  
                $('#Table1').dataTable();
            });  
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">


    <asp:Panel ID="pnlMainBody" runat="server">
        <div class="box-header">
            <div class="row" align="right">
                <center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </center>

                <asp:LinkButton ID="btnadd" runat="server" ToolTip="Add" class="btn btn-sm" data-toggle="modal" data-target="#myModelAdd" OnClick="btnadd_Click">

                    <asp:Image ID="Image1" runat="server" ImageUrl="../Admin/Images/Add.png" Width="20" Height="20" />
                </asp:LinkButton>
            </div>
        </div>
       
        <div class="template">

            <table id="Table1" style="margin-bottom: 2px" align="center" class="table  table-bordered table-striped display">

                <thead>
                    <tr style="background-color: #F6A41C">
                        <th class="col" style="width: 57px 53px; color: white; text-align: center">S.No</th>
                        <th class="col" style="display: none">Document Type ID</th>
                        <th class="col" style="width: 150px; color: white; text-align: center">Template</th>                       
                        <th class="col" style="width: 100px; color: white; text-align: center">Template Description</th>
                        <th class="col" style="width: 130px; color: white; text-align: center">Creation Date</th>
                        <th class="col" style="width: 130px; color: white; text-align: center">Modify Date</th>
                        <th class="col" style="width: 80px; color: white; text-align: center">Added By</th>
                        <th class="col" style="color: white; text-align: center">Actions</th>
                    </tr>
                </thead>

                <tbody>

                    <asp:Repeater ID="ReptUse" runat="server" OnItemDataBound="ReptUse_ItemDataBound">
                        <ItemTemplate>
                            <tr id="trID" runat="server">
                                <td id="serial" style="width: 57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                <td class="classDocumentTypeID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeID")%></td>
                                <td>
                                    <asp:LinkButton ID="lblocation" runat="server" OnClick="lbDocument_Click"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%></asp:LinkButton></td>
                                <td id="DocumentDescription" style="width: 150px" class="classDocumentDescription"><%# DataBinder.Eval(Container.DataItem, "DocumentDescription")%></td>
                                <td id="CustomersID" style="width: 130px;"><%# DataBinder.Eval(Container.DataItem, "DateOfCreation")%></td>
                                <td id="CustomerID" style="width: 130px;"><%# DataBinder.Eval(Container.DataItem, "DateOfModification")%></td>
                                <td id="UserName" style="width: 80px" class="classUserName"><%# DataBinder.Eval(Container.DataItem, "UserName")%></td>

                                <%--<td class="classUserDetailsID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "UserID")%></td>--%>

                                <td id="actions" style="width: 150px">
                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID imgEdit btn-sm" data-toggle="modal" data-target="#myModal">
                                               <img src="../Admin/Images/edit.png" />
                                    </asp:LinkButton>
                                    <asp:ImageButton ID="OnDelete" runat="server" ImageUrl="../Admin/Images/Delete.png" Height="18px" OnClick="OnDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("DocumentTypeID")%>' />
                                </td>

                            </tr>


                        </ItemTemplate>

                    </asp:Repeater>


                </tbody>

            </table>
            <div id="bottom_anchor"></div>
        </div>

        </div>
                <%----%>
        <div class="modal fade" id="myModal">
            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Edit  Template</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <asp:ScriptManager ID="ScriptManager1" runat="Server"></asp:ScriptManager>
                            <div class="col-md-6">
                                <label>Enter Template *</label>
                                <asp:TextBox class="form-control" ID="txtEditDocumentTypeName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="addEdit" ControlToValidate="txtEditDocumentTypeName" runat="server" ErrorMessage="Enter Template" ForeColor="Red"></asp:RequiredFieldValidator>

                                <input type="hidden" class="form-control" id="hiddenDocumentTypeID" name="hiddenDocumentTypeID" />

                            </div>                            

                            <div class="col-md-6">
                                <label>Enter Template Description</label>
                                <asp:TextBox class="form-control" ID="txtEditDocumentDescription" TextMode="MultiLine" runat="server"></asp:TextBox>

                            </div>
                            <div class="row"></div>
                            <div class="modal-footer">

                                <asp:Button ID="btnUpdate" runat="server" ValidationGroup="addEdit" Text="Save Changes" class="btn btn-primary" OnClick="btnUpdate_Click" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <%--------------------------------Create New Tempalte Pop-Up----------------------------%>

        <div class="modal fade" id="myModelAdd">

            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Add New Template</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-md-6">
                                <label>Enter Template Name *</label>
                                <asp:TextBox class="form-control" ID="txtDocumentTypeName" autocomplete="off" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="add" ControlToValidate="txtDocumentTypeName" runat="server" ErrorMessage="Enter Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                <input type="hidden" class="form-control" id="hd_txtDocumentTypeName" name="ddlAccountId1" />
                            </div>
                            <div class="col-md-6">
                                <label>Enter Description</label>
                                <asp:TextBox class="form-control" ID="txtDocumentDescrition" autocomplete="off" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="add" ControlToValidate="txtDocumentTypeName" runat="server" ErrorMessage="Enter Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                <input type="hidden" class="form-control" id="hd_txtDocumentDescrition" name="ddlAccountId1" />
                            </div>

                            <div class="col-md-6">
                                <label>Enter Label *</label>
                                <asp:TextBox class="form-control" ID="txtAddLabelName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="abc" ControlToValidate="txtAddLabelName" runat="server" ErrorMessage="Please Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                <input type="hidden" class="form-control" id="hd_txtAddLabelName" name="ddlAccountId1" />
                            </div>
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <label>Select Control Type *</label>

                                        <asp:DropDownList ID="ddlAddControlType" class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAddControlType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select Control" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="TextBox" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Calender" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="RadioButton" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="abc" ControlToValidate="ddlAddControlType" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>
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

                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div class="modal-footer">                                

                                <asp:Button ID="BtnCreateuser" runat="server" Text="Save" OnClick="BtnCreateuser_Click" ValidationGroup="add" class="btn btn-primary" />
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
                $("[id*=hiddenDocumentTypeID]").val($.trim($(".classDocumentTypeID", $(this).closest("tr")).html()));
                $("[id*=txtEditDocumentTypeName]").val($.trim($(".classDocumentTypeName", $(this).closest("tr")).html()));
                $("[id*=ddlEditSelectCompany]").val($.trim($(".classCustomerID", $(this).closest("tr")).html()));
                $("[id*=txtEditDocumentDescription]").val($.trim($(".classDocumentDescription", $(this).closest("tr")).html()));
            });
        });
    </script>

    <script>
        $('#Table1').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 4}], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
 </script>
</asp:Content>
