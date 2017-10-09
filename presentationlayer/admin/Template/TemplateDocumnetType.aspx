<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="TemplateDocumnetType.aspx.cs" Inherits="PresentationLayer.Template.TemplateDocumnetType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<!-- Custom styles for this template -->

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
    </script>    
    
      <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />  
        <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>  
        <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.0/js/jquery.dataTables.js"></script>  
    <script>  
            $(document).ready(function() {  
                $('#Table1').dataTable();
            });  
        </script>
    <asp:Panel ID="pnlMainBody" runat="server">
    
                    <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">TEMPLATE ENGINE</h4>
                      
                        <asp:LinkButton ID="btnadd" runat="server" ToolTip="Add Template" class="mq-btn btn btn-primary nxt_btn" OnClick="btnadd_Click" Text="Add Template"> </asp:LinkButton>                       
                        
           
                       
    </div>
         
            <table id="Table1" style="margin-bottom: 2px; margin-right:10px" align="center" class="table  table-bordered table-striped display">

                <thead>
                    <tr>
                        <th style="width:10px">S.No</th>
                        <th style="display: none">Document Type ID</th>
                        <th style= "text-align: center; width:25%">Template</th>                       
                        <th style=" text-align: center">Template Description</th>
                        <th style="width:12%; text-align: center">Created Date</th>
                        <th style="width:12%; text-align: center">Modified Date</th>
                        <%--<th style=" text-align: center">Added By</th>--%>
                        <th  style="width:14%; text-align: center">Actions</th>
                    </tr>
                </thead>

                <tbody>

                    <asp:Repeater ID="ReptUse" runat="server" OnItemDataBound="ReptUse_ItemDataBound">
                        <ItemTemplate>
                            <tr id="trID" runat="server">
                                <td id="serial" ><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                <td class="classDocumentTypeID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeID")%></td>
                                <td id="TemplateName"  class="classDocumentName" style="width:25%"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%></td>
                                <td id="DocumentDescription"  class="classDocumentDescription"><%# DataBinder.Eval(Container.DataItem, "DisplayDescription")%></td>
                                <td id="CustomersID" style="width:12%"><%# DataBinder.Eval(Container.DataItem, "DateDisplayCreation")%></td>
                                <td id="CustomerID" style="width:12%"><%# DataBinder.Eval(Container.DataItem, "DateDisplayModify")%></td>
                                <%--<td id="UserName" class="classUserName"><%# DataBinder.Eval(Container.DataItem, "UserName")%></td>--%>

                                <%--<td class="classUserDetailsID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "UserID")%></td>--%>

                                <td id="actions" style="width: 14%">
                                    <asp:LinkButton ID="lbAddDataPoint" runat="server" ToolTip="Add Data Point" OnClick="lbAddDataPoint_Click" class="btn  btn-outline-dark">
                                         <i class="fa fa-id-card-o"></i>                                     
                                    </asp:LinkButton>
                                  <%--  <asp:LinkButton ID="lbView" runat="server" ToolTip="View" OnClick="lbDocument_Click" class="btn  btn-outline-secondary">
                                   <i class="fa fa-bars"></i>  
                                    </asp:LinkButton>--%>
                                 
                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID btn btn-outline-info" data-toggle="modal" data-target="#myModal">
                                             <i class="fa fa-pencil"></i>  
                                    </asp:LinkButton>
                                  <%--  <asp:ImageButton ID="OnDelete" class="btn-outline-danger" runat="server" ImageUrl="../Admin/Images/Delete.png" Height="18px" OnClick="OnDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')" />--%>
                                    
                                     <asp:LinkButton ID="OnDelete" runat="server" class="btn  btn-outline-danger" ToolTip="Delete" OnClick="OnDelete_Click"  OnClientClick = "return confirm('Do you want to Delete data?')"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                     
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("DocumentTypeID")%>' />
                                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("DocumentTypeName")%>' />
                                    <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("DocumentDescription")%>' />


                                </td>

                            </tr>


                        </ItemTemplate>

                    </asp:Repeater>


                </tbody>

            </table>
            <div class="modal fade" id="myModal">
            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Edit Template</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <asp:ScriptManager ID="ScriptManager1" runat="Server"></asp:ScriptManager>
                            <div class="col-md-6">
                                <label>Enter Template *</label>
                                <asp:TextBox class="form-control" ID="txtEditDocumentTypeName" runat="server" ValidationGroup="addEdit"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="addEdit" Display="Dynamic" ControlToValidate="txtEditDocumentTypeName" runat="server" ErrorMessage="Enter template name" ForeColor="Red"></asp:RequiredFieldValidator>

                                <input type="hidden" class="form-control" id="hiddenDocumentTypeID" name="hiddenDocumentTypeID" />

                            </div>                            

                            <div class="col-md-6">
                                <label>Enter Template Description</label>
                                <asp:TextBox class="form-control" ID="txtEditDocumentDescription" TextMode="MultiLine" runat="server"></asp:TextBox>

                            </div>
                            <div class="row"></div>
                            <div class="modal-footer">

                                <asp:Button ID="btnUpdate" runat="server" ValidationGroup="addEdit" Display="Dynamic" Text="Save Changes" class="btn btn-primary" OnClick="btnUpdate_Click" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      

        </asp:Panel>

        <%--------------------------------Create New Tempalte Pop-Up----------------------------%>

        <div class="modal fade" id="myModelAdd">

            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Add Template</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-4"><label>Template Name</label></div>
                                <div class="col-lg-8">
                                <asp:TextBox class="form-control" ID="txtDocumentTypeName" autocomplete="off" runat="server" Width="310px" ValidationGroup="add"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="add" ControlToValidate="txtDocumentTypeName" runat="server" ErrorMessage="Enter Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                <input type="hidden" class="form-control" id="hd_txtDocumentTypeName" name="ddlAccountId1" />
                                    </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="col-lg-4"><label>Description</label></div>
                                <div class="col-lg-8">
                                <asp:TextBox class="form-control" ID="txtDocumentDescrition" autocomplete="off" runat="server" Width="310px" Height="80px" TextMode="MultiLine" ValidationGroup="add"></asp:TextBox><br />
                                
                                <input type="hidden" class="form-control" id="hd_txtDocumentDescrition" name="ddlAccountId1" />
                                    </div>
                            </div>

<%--                            <div class="col-md-6">
                                <label>Enter Label *</label>
                                <asp:TextBox class="form-control" ID="txtAddLabelName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="abc" ControlToValidate="txtAddLabelName" runat="server" ErrorMessage="Please Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                <input type="hidden" class="form-control" id="hd_txtAddLabelName" name="ddlAccountId1" />
                            </div>--%>
                            <%--<div class="col-md-6">
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
                            </div>--%>
                            <%--<div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>

                                        <asp:Label ID="lblDropDownValue" runat="server" Visible="false">Enter Select Value</asp:Label>

                                        <asp:TextBox class="form-control" ID="txtDropDownValue" runat="server" Visible="false"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" ValidationGroup="abc" ControlToValidate="txtDropDownValue" runat="server" ErrorMessage="Please Enter Value" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>--%>
                            <%--<div class="col-md-6">

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
                            </div>--%>

                            <div class="modal-footer">                                

                                <asp:Button ID="BtnCreateuser" runat="server" Text="Save" OnClick="BtnCreateuser_Click"  class="btn btn-primary" />
                                <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />

                            </div>


                        </div>

                    </div>

                </div>

            </div>
        </div>
      <script>
        $(function () {
            $(".editID").click(function () {
                $("[id*=hiddenDocumentTypeID]").val($.trim($(".classDocumentTypeID", $(this).closest("tr")).html()));
                $("[id*=txtEditDocumentTypeName]").val($.trim($(".classDocumentTypeName", $(this).closest("tr")).html()));
                $("[id*=ddlEditSelectCompany]").val($.trim($(".classCustomerID", $(this).closest("tr")).html()));
                $("[id*=txtEditDocumentDescription]").val($.trim($(".classDocumentDescription", $(this).closest("tr")).html()));
                $("[id*=txtEditDocumentTypeName]").val($.trim($(".classDocumentName", $(this).closest("tr")).html()));
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
