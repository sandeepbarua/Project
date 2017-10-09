<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="CreateRule.aspx.cs" EnableEventValidation="false"  Inherits="PresentationLayer.Admin.CreateRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
  
    <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }
    </style>
   <script>
        function valid() {
           var v_type = document.getElementById("<%=txtAuditQuestion.ClientID%>").value;
            var customer_val = document.getElementById("<%=ddlAddauditrule.ClientID%>").value;
             var document_val = document.getElementById("<%=ddlruletype.ClientID%>").value;
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

   </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
     <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:Panel ID="pnlMainBody" runat="server">
                <div class="box-header">
                    <div class="row" align="right">
                        <center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </center>
                                            
                        <asp:LinkButton ID="btnadd" runat="server" ToolTip="Add New Control" class="btn btn-sm" data-toggle="modal" data-target="#myModelAdd">

                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/Add.png" Width="20" Height="20" />
                        </asp:LinkButton>
                    </div>
                </div>
         <div class="template">

                    <table id="Table1" style="margin-bottom: 2px" class="table  table-bordered table-striped display">

                        <thead>
                            <tr style="background-color: #F6A41C">
                                <th class="col" style="width: 57px 53px; color: white; text-align: center">S.No</th>

                                <%--<th class="col" style="width: 150px; color: white; text-align: center">Document Type Name</th>--%>
                                <th class="col" style="width: 150px; color: white; text-align: center">Audit Question</th>
                                <th class="col" style="width: 150px; color: white; text-align: center">Dynamic Control ID</th>
                                <th class="col" style="width: 150px; color: white; text-align: center">Label Name</th>
                                <th class="col" style="width: 100px; color: white; text-align: center">Rule Expression</th>
                                <th class="col" style="width: 100px; color: white; text-align: center">RuleType</th>
                                <th class="col" style="color: white; text-align: center">Actions</th>

                            </tr>
                        </thead>

                        <tbody>

                            <asp:Repeater ID="Reptrule" runat="server">
                                <ItemTemplate>
                                    <tr id="trID" runat="server">
                                        <td id="serial" style="width: 57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                        <td class="hiddenDynamicControlID" ><%# DataBinder.Eval(Container.DataItem, "AuditQuestion")%>  <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("AuditRuleClassification")%>' /></td>
                                        <td id="DocumentTypeName" class="DynContrId" style="width: 150px"><%# DataBinder.Eval(Container.DataItem, "DynamicControlID")%></td>
                                        <td id="CompanyName" class="labelname" style="width:150px"><%# DataBinder.Eval(Container.DataItem, "labelname")%></td>
                                        <td id="UserName" style="width:150px" class="RuleExpression"><%# DataBinder.Eval(Container.DataItem, "RuleExpression")%></td>
                                        <td id="CustomerID" style="width:100px;" class="RuleType"><%# DataBinder.Eval(Container.DataItem, "RuleType")%></td>
                                      
                                        <td id="actions" style="width: 150px">

                                            <%--<asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID imgEdit btn-sm" data-toggle="modal" data-target="#myModal">
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>--%>

                                            <%--<asp:LinkButton ID="btn_Edit" runat="server" OnClick="btn_Edit_Click" ToolTip="Edit" >
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkEdit"   runat="server" OnClick="lnkEdit_Click1">Edit</asp:LinkButton>

                                            
                                           <%-- <asp:ImageButton ID="OnEdit" runat="server" ImageUrl="Images/edit.png" Height="18px" OnClick="btnEdit_Click"  />--%>



                                            <asp:ImageButton ID="OnDelete" runat="server" ImageUrl="Images/Delete.png" Height="18px" OnClick="OnDelete_Click"  OnClientClick="return confirm('Do you want to Delete data?')" />

                                           
                                        </td>

                                    </tr>


                                </ItemTemplate>

                            </asp:Repeater>


                        </tbody>

                    </table>
                    <div id="bottom_anchor"></div>
                </div>
            <%--create new Account--%>

        <div class="modal fade" id="myModelAdd">

            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Add Audit Rules</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            

                            <div class="col-md-6">
                                 <label>Select Control Name *</label>
                                          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="ddlAddauditrule" class="form-control" AutoPostBack="True" runat="server">
                                              </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="add"  ControlToValidate="ddlAddauditrule" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                            </div>


                            <div class="col-md-6">
                                <label>Select Rule Type *</label>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                       

                                        <asp:DropDownList ID="ddlruletype"  class="form-control" runat="server" Width="250px" Height="34px"  AutoPostBack="True" >
                                        </asp:DropDownList>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="abc"  ControlToValidate="ddlruletype" InitialValue="0" runat="server" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                               
                            </div>

                            <div class="col-md-6">

                                <label>Rule Expression </label>
                             <asp:TextBox class="form-control" ID="txtAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="add"  ControlToValidate="txtAuditQuestion" runat="server" ErrorMessage="Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>
                                   
                            </div>


                            <div class="modal-footer">
                                 <button type="button"  id="close" runat="server" data-dismiss="modal" class="btn btn-primary">Close</button>
                               
                                <asp:Button ID="BtnCreateRule" runat="server" Text="Submit" OnClick="BtnCreateRule_Click" OnClientClick="return valid()" ValidationGroup="abc" class="btn btn-primary" />
                               <%--  <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />--%>

                            </div>


                        </div>

                    </div>

                </div>

            </div>
        </div>
        <%--edit--%>
         <div class="modal fade" id="myModal">
            <div class="modal-dialog">

                <div class="modal-content">

                    <div class="modal-header bg-aqua-active">

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Edit Audit Rules</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                          
                           <div class="col-md-6">

                                        <label>Select Control Name *</label>
                                          <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="ddlEditSelectCompany"  AutoPostBack="True" class="form-control" runat="server">

                                              </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="addEdit"   ControlToValidate="ddlEditSelectCompany" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                                                </div> 


                            <div class="col-md-6">

                                <label>Select Rule Type *</label>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEditDocumentType" class="form-control" runat="server">
                                        </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added"  ControlToValidate="ddlEditDocumentType" runat="server" InitialValue="0" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-6">
                                <label>Enter Rule Expression *</label>
                                <asp:TextBox class="form-control" ID="txtEditAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <asp:TextBox class="form-control" Visible="false" ID="txtDynamicCtrID"  runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="EditValida"  ControlToValidate="txtEditAuditQuestion" runat="server"  ErrorMessage="Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                     
                                <input type="hidden" class="form-control" id="hiddenDynamicControlID" name="hiddenDynamicControlID" />

                            </div>
                            


                            <div class="modal-footer">
                             
                                <asp:Button ID="btnUpdate" ValidationGroup="EditValida" runat="server" Text="Save Changes" OnClick="btnUpdate_Click" class="btn btn-primary" onclientclick="return validedit();"   />
                                   <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

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
                $("[id*=txtDynamicCtrID]").val($.trim($(".hiddenDynamicControlID", $(this).closest("tr")).html()));
                $("[id*=ddlEditSelectCompany]").val($.trim($(".DynContrId", $(this).closest("tr")).html()));
                $("[id*=txtEditAuditQuestion]").val($.trim($(".RuleExpression", $(this).closest("tr")).html()));
                $("[id*=ddlEditDocumentType]").val($.trim($(".RuleType", $(this).closest("tr")).html()));
                });
        });
    </script>

    
    </asp:Content>
