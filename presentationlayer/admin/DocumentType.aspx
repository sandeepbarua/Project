<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DocumentType.aspx.cs" Inherits="PresentationLayer.Admin.DocumentType1" %>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
        

            <asp:Panel ID="pnlMainBody" runat="server">
                <div class="box-header">
                    <div class="row" align="right">
                         <Center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </Center>   
                        
                        <asp:LinkButton ID="btnadd" Visible="false" runat="server" ToolTip="Add" class="btn btn-sm" data-toggle="modal" data-target="#myModelAdd" OnClick="btnadd_Click">
                           
                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/Add.png" Width="20" Height="20"/>
                        </asp:LinkButton>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="main-panel">
                    <div class="modal-body">

                        <div class="row">
                            <table  width="40%">
                                <tr>
             
               <td width="25%">Company Name:&nbsp&nbsp&nbsp&nbsp          </td>
               <td style="width:50%; justify-content:flex-end; margin-bottom:10px"><asp:DropDownList ID="ddlCompanyName" runat="server" class="form-control" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
               
               </td>
               </tr>
              
           
       </table>
                       </div>
                   
                    </div>
                </div>
                <div class="template">
        
        <table id="Table1" style="margin-bottom:2px" align="center" class="table  table-bordered table-striped display">
          
                        <thead>
                            <tr style="background-color:#F6A41C">
                                <th class="col" style="width:57px 53px; color:white; text-align:center">S.No</th>
                                
                                <th class="col" style="width:150px; color:white; text-align:center">Document Type </th>
                                <th class="col" style="width:150px; color:white; text-align:center">Company Name</th>
                                <th class="col"style="width:100px; color:white; text-align:center">Document Description</th>
                                <th class="col" style="width:100px; color:white; text-align:center">Added By:</th>
                                <th class="col" style="color:white; text-align:center">Actions</th>
                                
                            </tr>
                        </thead>
           
                     <tbody>
                           
                           <asp:Repeater ID="ReptUse" runat="server" OnItemDataBound="ReptUse_ItemDataBound">
                                <ItemTemplate>
                                    <tr id = "trID" runat="server">
                                        <td id="serial" style="width:57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                     <td class="classDocumentTypeID" style="display:none" ><%# DataBinder.Eval(Container.DataItem, "DocumentTypeID")%></td>
                                        <td id="DocumentTypeName" style="width:150px" class="classDocumentTypeName"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%></td>
                                       <td id="CompanyName" style="width:100px" class="classCompanyName"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></td>
                                        <td id="DocumentDescription" style="width:150px" class="classDocumentDescription"><%# DataBinder.Eval(Container.DataItem, "DocumentDescription")%></td>
                                        <td id="UserName" style="width:150px" class="classUserName"><%# DataBinder.Eval(Container.DataItem, "UserName")%></td>
                                         <td id="CustomerID" style="width:100px;display:none" class="classCustomerID"><%# DataBinder.Eval(Container.DataItem, "CustomerID")%></td>
                                        <td class="classUserDetailsID" style="display:none"><%# DataBinder.Eval(Container.DataItem, "UserID")%></td>
                                       
                                        <td id="actions" style="width:150px">

                                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID imgEdit btn-sm" data-toggle="modal" data-target="#myModal">
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>
                                      
                                      
                                                  <asp:ImageButton ID="OnDelete" runat="server" ImageUrl="Images/Delete.png" Height="18px" OnClick="OnDelete_Click"  OnClientClick = "return confirm('Do you want to Delete data?')" />
                                                  
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
                <%--Edit Account--%>
                <div class="modal fade" id="myModal">
                    <div class="modal-dialog">

                        <div class="modal-content">

                            <div class="modal-header bg-aqua-active">

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                                    <span aria-hidden="true">&times;</span></button>

                                            <h4 class="modal-title">Edit  Document Type</h4>

                            </div>

                            <div class="modal-body">

                                <div class="row">
                                    <asp:ScriptManager ID="ScriptManager1" runat="Server"></asp:ScriptManager>
                                 <div class="col-md-6">
                                        <label>Enter Document Type *</label>
                                        <asp:TextBox class="form-control" ID="txtEditDocumentTypeName" runat="server"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="addEdit"  ControlToValidate="txtEditDocumentTypeName" runat="server" ErrorMessage="Enter Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                   
                                        <input type="hidden" class="form-control" id="hiddenDocumentTypeID" name="hiddenDocumentTypeID" />
                                 
                                    </div>
                                    <div class="col-md-6">

                                        <label>Select Company *</label>
                                          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="ddlEditSelectCompany" class="form-control" runat="server">

                                              </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="addEdit"  ControlToValidate="ddlEditSelectCompany" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                                                </div>

                                     <div class="col-md-6">
                                        <label>Enter Document Description </label>
                                        <asp:TextBox class="form-control" ID="txtEditDocumentDescription" TextMode="MultiLine" runat="server"></asp:TextBox>
                                       
                                    </div>
                                
                                <div class="modal-footer">
                                   
                                    <asp:Button ID="btnUpdate" runat="server" ValidationGroup="addEdit" Text="Save Changes" class="btn btn-primary" OnClick="btnUpdate_Click"/>
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

                                <h4 class="modal-title">Add Document Type</h4>

                            </div>

                            <div class="modal-body">

                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Enter Document Type *</label>
                                        <asp:TextBox class="form-control" ID="txtDocumentTypeName" autocomplete="off" runat="server"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="add"  ControlToValidate="txtDocumentTypeName" runat="server" ErrorMessage="Enter Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                   
                                        <input type="hidden" class="form-control" id="ddlAccountId1" name="ddlAccountId1" />
                                 
                                    </div>
                                    <div class="col-md-6">

                                        <label>Select Company *</label>
                                          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="ddlAddCompany" class="form-control" runat="server">
                                              </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="add"  ControlToValidate="ddlAddCompany" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                                                </div>

                                     <div class="col-md-6">
                                        <label>Enter Document Description*</label>
                                        <asp:TextBox class="form-control" ID="txtDocumentDescrition" TextMode="MultiLine" runat="server"></asp:TextBox>
                                       
                                    </div>
                                    
                                    <div class="modal-footer">

                                        <%-- <button type="button" class="btn btn-default" id="close" runat="server" data-dismiss="modal">Close</button>--%>
                                     
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
</asp:Content>
