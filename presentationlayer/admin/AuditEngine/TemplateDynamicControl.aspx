<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="TemplateDynamicControl.aspx.cs" Inherits="PresentationLayer.Admin_New.TemplateDynamicControl" %>
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
                                                 <asp:LinkButton ID="btnadd" runat="server" Visible="false" ToolTip="Add New Control" class="btn btn-sm" data-toggle="modal" data-target="#myModelAdd" OnClick="btnadd_Click">

                            <asp:Image ID="Image1" runat="server" ImageUrl="../Admin/Images/Add.png" Width="20" Height="20" />
                        </asp:LinkButton>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="main-panel">
                    <div class="modal-body">

                        <div class="row">
                            <table  width="75%">
                                    <tr>
                                        <td  width="40%"><label>Template Name :</label></td>
                                        <td width="50%"><asp:DropDownList ID="ddlDocumentType"  class="form-control" runat="server" Width="250px" Height="34px"  AutoPostBack="True" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged" Enabled="false"></asp:DropDownList></td>
                                        <td width="25%" "><%--<label >Customer Name : </label>--%></td>
                                        <td width="30%" ><asp:DropDownList ID="ddlCustomer"  class="form-control" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" Visible="false"></asp:DropDownList></td>
                                    <%--<tr style="margin-top:10px"></tr>--%>
                                        <%--<td  width="25%"><label>Document Type :</label></td>
                                        <td width="50%"><asp:DropDownList ID="ddlDocumentType"  class="form-control" runat="server" Width="250px" Height="34px"  AutoPostBack="True" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged" Enabled="false"></asp:DropDownList></td>--%>
                                    </tr>
                                </table>
                        </div>
                   
                    </div>
                </div>

                <div class="template">

                    <table id="Table1" style="margin-bottom: 2px" class="table  table-bordered table-striped display">

                        <thead>
                            <tr style="background-color: #F6A41C">
                                <th class="col" style="width: 57px 53px; color: white; text-align: center">S.No</th>

                                <th class="col" style="display: none">Document Type Name</th>
                                <th class="col" style="display: none">Document Type Name</th>

                                <th class="col" style="width: 150px; color: white; text-align: center">Label Name</th>
                               <%-- <th class="col" style="width: 100px; color: white; text-align: center">Control Name</th>--%>
                                <th class="col" style="width: 100px; color: white; text-align: center">Control Type</th>
                                 <th class="col" style="width: 100px; color: white; text-align: center">Drop Down Values</th>
                                 <th class="col" style="width: 100px; color: white; text-align: center">Order No</th>
                                <th class="col" style="color: white; text-align: center">Actions</th>

                            </tr>
                        </thead>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <tbody>

                            <asp:Repeater ID="ReptUse" runat="server" OnItemDataBound="ReptUse_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="trID" runat="server">
                                        <td id="serial" style="width: 57px">   <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                        <%--<td class="classDynamicControlIDs" style="display: none">   <asp:HiddenField ID="hndorderno" runat="server" Value='<%#Eval("DynamicControlID")%>' /> <%# DataBinder.Eval(Container.DataItem, "DynamicControlID")%></td>--%>
                                         <td class="classDynamicControlID" style="display: none">  <%# DataBinder.Eval(Container.DataItem, "DynamicControlID")%></td>
                                      
                                         <%--<td id="DocumentTypeName" style="width: 150px" class="classDocumentTypeName"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%></td>--%>
                                        <td id="DocumentTypeIDss" style="width: 100px; display:none"  class="classDocumentTypeID"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeID")%></td>
                                        <td id="UserName" style="width: 150px" class="classlabelName"><%# DataBinder.Eval(Container.DataItem, "labelName")%></td>
                   <%--                     <td id="CustomerID" style="width: 100px" class="classControlName"><%# DataBinder.Eval(Container.DataItem, "ControlName")%></td>--%>
                                        <td class="classControlType"><%# DataBinder.Eval(Container.DataItem, "ControlType")%></td>
                                        <td class="classDropDownValue"><%# DataBinder.Eval(Container.DataItem, "DropDown")%></td>
                                        <%--<td class="classControlTypeID" style="display:none"><%# DataBinder.Eval(Container.DataItem, "ControlTypeID")%></td>--%>
                                         <td style="width: 150px"><asp:TextBox ID="txtOrderNo" runat="server" onkeypress="CheckNumeric(event);" Width="40px" Text='<%#Eval("Order_No") %>'></asp:TextBox></td>
                                        <td id="actions" style="width: 150px">
                                           
                                            <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID imgEdit btn-sm" data-toggle="modal" data-target="#myModal">
                                               <img src="../Admin/Images/edit.png" />
                                            </asp:LinkButton>

                                            <asp:ImageButton ID="OnDelete" runat="server" ImageUrl="../Admin/Images/Delete.png" Height="18px" OnClick="OnDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')" />

                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("DynamicControlID")%>' />
                                            <asp:HiddenField ID="HfLabelName" runat="server" Value='<%#Eval("labelName")%>' />

                                        </td>

                                    </tr>

                                </ItemTemplate>

                            </asp:Repeater>
                            
                                
                                    <table class="auto-style1">
                                        <tr>
                                            <td colspan="2" width="30%"></td>
                                            <td></td>
                                            <td colspan="2" width="50%"></td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnorderno" runat="server" Visible="false" class="btn btn-success btn-sm" OnClick="btnorderno_Click" Text="Submit Order No" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                
                        </tbody>

                    </table>
                    <div id="bottom_anchor"></div>
                </div>
       
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
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added"  ControlToValidate="ddlEditDocumentType" runat="server" InitialValue="0" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-6">
                                <label>Label Name*</label>
                                <asp:TextBox class="form-control" ID="txtEditLabelName" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="added"  ControlToValidate="txtEditLabelName" runat="server"  ErrorMessage="Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    
                                <input type="hidden" class="form-control" id="hiddenDynamicControlID" name="hiddenDynamicControlID" />

                            </div>
                            <div class="col-md-6">
                                <label>Drop Down Values *</label>
                                <asp:TextBox class="form-control" ID="txtEditDropDown" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="added"  ControlToValidate="txtEditDropDown" runat="server"  ErrorMessage="Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>
                               </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <%-- Unused Text Box--%>
                            <%--<div>
                             <div class="col-md-6">
        
                                <asp:TextBox class="form-control" ID="TextBox1" Visible="false" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtEditDropDown" runat="server"  ErrorMessage="" ForeColor="Red"></asp:RequiredFieldValidator>
                                 </div>
                                   <div class="col-md-6">
        
                                <asp:TextBox class="form-control" ID="TextBox2" Visible="false" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtEditDropDown" runat="server"  ErrorMessage="" ForeColor="Red"></asp:RequiredFieldValidator>
                                 </div>
                                 <div class="col-md-6">
        
                                <asp:TextBox class="form-control" ID="TextBox3" Visible="false" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtEditDropDown" runat="server"  ErrorMessage="" ForeColor="Red"></asp:RequiredFieldValidator>
                                 </div>
                                 <div class="col-md-6">
        
                                <asp:TextBox class="form-control" ID="TextBox4" Visible="false" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtEditDropDown" runat="server"  ErrorMessage="" ForeColor="Red"></asp:RequiredFieldValidator>
                                 </div>
                                </div>
                            --%> <%-- End of Unused Text Box--%>
                       
                           <%-- Unused Edit--%>
                         <%--   <div>
                            <div class="col-md-6">
                                <label>Select Control Type *</label>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEditControlType" class="form-control" runat="server" OnSelectedIndexChanged="ddlEditControlType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select Control " Value="0"></asp:ListItem>
                                            <asp:ListItem Text="TextBox" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="CheckBox" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Calender" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="RadioButton" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="EditValida"  ControlToValidate="ddlEditControlType" runat="server" InitialValue="0" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>
                              
                            </div>
                           <div class="col-md-6">
                                <label>Enter Choice One *</label>
0                                <asp:TextBox class="form-control" ID="txtChoiceOne" runat="server" Visible="false"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="EditValida"  ControlToValidate="txtChoiceOne" runat="server"  ErrorMessage="Enter Choice One" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label>Enter Choice Two *</label>
                                <asp:TextBox class="form-control" ID="txtChoiceTwo" runat="server" Visible="false"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="EditValida"  ControlToValidate="txtChoiceTwo" runat="server"  ErrorMessage="Enter Choice Two" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                                </div>--%>
                             <%-- End of Unused Edit--%>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdate" ValidationGroup="EditValida" runat="server" Text="Save Changes" class="btn btn-primary" OnClick="btnUpdate_Click" />
                                   <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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

                        <h4 class="modal-title">Add Control</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-md-6">

                                <label>Template :  </label>
                              <asp:Label ID="lblDocumentType" runat="server"  class="form-control" ></asp:Label>
                            </div>

                            <div class="col-md-6">
                                <label>Enter Label Name</label>
                                <asp:TextBox class="form-control" ID="txtAddLabelName" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="abc"  ControlToValidate="txtAddLabelName" runat="server" ErrorMessage="Please Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                <input type="hidden" class="form-control" id="ddlAccountId1" name="ddlAccountId1" />

                            </div>
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                <label>Select Control Type</label>
                                
                                        <asp:DropDownList ID="ddlAddControlType" class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAddControlType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select Control" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="TextBox" Value="1"></asp:ListItem>
                                            
                                            <asp:ListItem Text="Calender" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="RadioButton" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="abc"  ControlToValidate="ddlAddControlType" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                               <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                
                                  <asp:Label ID="lblDropDownValue" runat="server" Visible="false">Enter Select Value</asp:Label>
                                
                                <asp:TextBox class="form-control" ID="txtDropDownValue" runat="server" Visible="false"></asp:TextBox>

                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10"  Display="Dynamic"  ValidationGroup="abc"  ControlToValidate="txtDropDownValue" runat="server"  ErrorMessage="Please Enter Value" ForeColor="Red"></asp:RequiredFieldValidator>
           
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                        </div>
                             
                        <div class="col-md-6">
                            
                                <asp:UpdatePanel ID="UpdatePanel3"  runat="server">
                                    <ContentTemplate>
                                  <asp:Label ID="lbltxtAddChoiceOne" runat="server" Visible="false">Enter Choice One</asp:Label>
                                <asp:TextBox class="form-control" ID="txtAddChoiceOne" runat="server" Visible="false"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="abc"  ControlToValidate="txtAddChoiceOne" runat="server"  ErrorMessage="Enter Choice One" ForeColor="Red"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                       
                            <div class="col-md-6" style="margin-top:10px">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                 <asp:Label ID="lblAddChoiceTwo" runat="server" Visible="false">Enter Choice Two</asp:Label>
                                
                                <asp:TextBox class="form-control" ID="txtAddChoiceTwo" runat="server" Visible="false"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="abc"  Display="Dynamic"  ControlToValidate="txtAddChoiceTwo" runat="server"  ErrorMessage="Enter Choice Two" ForeColor="Red"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                                </asp:UpdatePanel>
                                        </div>
                            <div class="row"></div>
                            <div class="modal-footer" style="margin-top:10px">
                                <%-- <button type="button" class="btn btn-default" id="close" runat="server" data-dismiss="modal">Close</button>--%>
                               
                                <asp:Button ID="BtnCreateuser" runat="server" Text="Save" OnClick="BtnCreateuser_Click" ValidationGroup="abc" class="btn btn-primary" />
                                 <asp:Button ID="close" OnClick="close_Click" runat="server" Text="Close" class="btn btn-default" />

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
            'columnDefs': [{ 'orderable': false, 'targets': 4}], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
 </script>

</asp:Content>
