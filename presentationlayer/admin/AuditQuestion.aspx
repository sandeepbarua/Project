<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="AuditQuestion.aspx.cs" Inherits="PresentationLayer.Admin.AuditQuestion" %>


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
    <script type="text/javascript">
    
        function valid() {
           var v_type = document.getElementById("<%=txtAuditQuestion.ClientID%>").value;
            var customer_val = document.getElementById("<%=ddlAddCompany.ClientID%>").value;
             var document_val = document.getElementById("<%=ddlAddDocumentType.ClientID%>").value;
           //var sales = document.getElementById("editor1").value;
            if (v_type == "" || customer_val == "" || document_val == "" || customer_val == "Select Customer" || customer_val == "0" || document_val == "Select Document Type" || document_val == "0")
           {
               alert("Fill all information");
               return false;
           }
           else {
              
                   return true;              
 
           }        
        }

        function validedit() {
           var v_type = document.getElementById("<%=txtEditAuditQuestion.ClientID%>").value;
           var customer_val = document.getElementById("<%=ddlEditSelectCompany.ClientID%>").value;
             var document_val = document.getElementById("<%=ddlEditDocumentType.ClientID%>").value;
          
           //var sales = document.getElementById("editor1").value;
            if (v_type == "" || customer_val == "" || document_val == "" || customer_val == "Select Customer" || document_val == "Select Document Type" || customer_val == "0" || document_val == "0")
           {
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
    </style>
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
                                            
                        <asp:LinkButton ID="btnadd" runat="server" ToolTip="Add New Control" class="btn btn-sm" data-toggle="modal" data-target="#myModelAdd" OnClick="btnadd_Click">

                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/Add.png" Width="20" Height="20" />
                        </asp:LinkButton>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="main-panel">
                    <div class="modal-body">

                        <div class="row">
                             <table   width="75%"
                                  <tr>
                                    </tr> 
                                        <tr>
                                            <td width="25%">
                                                <label>
                                                Customer Name :
                                                </label>
                                            </td>
                                            <td width="50%">
                                                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="25%">
                                                <label>
                                                Document Type :</label> </td>
                                            <td width="50%">
                                                <asp:DropDownList ID="ddlDocumentType" runat="server" AutoPostBack="True" class="form-control" Enabled="false" Height="34px" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged" Width="250px">
                                                </asp:DropDownList>
                                            </td>
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

                                <%--<th class="col" style="width: 150px; color: white; text-align: center">Document Type Name</th>--%>
                                <th class="col" style="width: 150px; color: white; text-align: center">Audit Question</th>
                                <th class="col" style="width: 150px; color: white; text-align: center">Document Type</th>
                                <th class="col" style="width: 150px; color: white; text-align: center">Company Name</th>
                                <th class="col" style="width: 100px; color: white; text-align: center">Added By</th>

                                <th class="col" style="color: white; text-align: center">Actions</th>

                            </tr>
                        </thead>

                        <tbody>

                            <asp:Repeater ID="ReptUse" runat="server">
                                <ItemTemplate>
                                    <tr id="trID" runat="server">
                                        <td id="serial" style="width: 57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                        <td class="classAuditQuestion" ><%# DataBinder.Eval(Container.DataItem, "AuditQuestion")%></td>
                                        <td id="DocumentTypeName" style="width: 150px" class="classDocumentTypeName"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%></td>
                                        <td id="CompanyName" style="width:150px" class="classCompanyName"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></td>
                                        <td id="UserName" style="width:150px" class="classUserName"><%# DataBinder.Eval(Container.DataItem, "UserName")%></td>
                                        <td id="CustomerID" style="width:100px;display:none" class="classCustomerID"><%# DataBinder.Eval(Container.DataItem, "CustomerID")%></td>
                                        <td class="classUserDetailsID" style="display:none"><%# DataBinder.Eval(Container.DataItem, "UserDetailsID")%></td>
                                       <td class="classDynamicControlID" style="display:none"><%# DataBinder.Eval(Container.DataItem, "AuditQuestionID")%></td>
                                        <td class="classDocumentTypeID" style="display:none"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeID")%></td>

                                        <td id="actions" style="width: 150px">

                                            <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID imgEdit btn-sm" data-toggle="modal" data-target="#myModal">
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>

                                           <%-- <asp:ImageButton ID="OnEdit" runat="server" ImageUrl="Images/edit.png" Height="18px" OnClick="btnEdit_Click"  />--%>



                                            <asp:ImageButton ID="OnDelete" runat="server" ImageUrl="Images/Delete.png" Height="18px" OnClick="OnDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')" />

                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("AuditQuestionID")%>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("DocumentTypeID")%>' />
                                             <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("CustomerID")%>' />
                                            <asp:LinkButton ID="lnkBtnCreateRule" runat="server" OnClick="lnkBtnCreateRule_Click" ToolTip="Create Rule" >
                                               Create Rule
                                            </asp:LinkButton>
                                        </td>

                                    </tr>


                                </ItemTemplate>

                            </asp:Repeater>


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

                        <h4 class="modal-title">Edit Audit</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <asp:ScriptManager ID="ScriptManager1" runat="Server"></asp:ScriptManager>
                            
                            

                           <div class="col-md-6">

                                        <label>Select Company *</label>
                                          <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="ddlEditSelectCompany" OnSelectedIndexChanged="ddlEditSelectCompany_SelectedIndexChanged" AutoPostBack="True" class="form-control" runat="server">

                                              </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="addEdit"   ControlToValidate="ddlEditSelectCompany" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                                                </div> 


                            <div class="col-md-6">

                                <label>Select Document Type *</label>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEditDocumentType" class="form-control" runat="server">
                                        </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added"  ControlToValidate="ddlEditDocumentType" runat="server" InitialValue="0" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-6">
                                <label>Enter Audit Question *</label>
                                <asp:TextBox class="form-control" ID="txtEditAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="EditValida"  ControlToValidate="txtEditAuditQuestion" runat="server"  ErrorMessage="Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                     
                                <input type="hidden" class="form-control" id="hiddenDynamicControlID" name="hiddenDynamicControlID" />

                            </div>
                            


                            <div class="modal-footer">
                             
                                <asp:Button ID="btnUpdate" ValidationGroup="EditValida" runat="server" Text="Save Changes" class="btn btn-primary" onclientclick="return validedit();"  OnClick="btnUpdate_Click" />
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

                        <h4 class="modal-title">Add Audit Question</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            

                            <div class="col-md-6">
                                 <label>Select Company *</label>
                                          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="ddlAddCompany" class="form-control" OnSelectedIndexChanged="ddlAddCompany_SelectedIndexChanged" AutoPostBack="True" runat="server">
                                              </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="add"  ControlToValidate="ddlAddCompany" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                            </div>


                            <div class="col-md-6">
                                <label>Select Document Type *</label>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                       

                                        <asp:DropDownList ID="ddlAddDocumentType"  class="form-control" runat="server" Width="250px" Height="34px"  AutoPostBack="True"  Enabled="false">
                                        </asp:DropDownList>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="abc"  ControlToValidate="ddlAddDocumentType" InitialValue="0" runat="server" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                               
                            </div>

                            <div class="col-md-6">

                                <label>Audit Question :  </label>
                             <asp:TextBox class="form-control" ID="txtAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="add"  ControlToValidate="txtAuditQuestion" runat="server" ErrorMessage="Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>
                                   
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


</asp:Content>
