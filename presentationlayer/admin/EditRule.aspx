<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="EditRule.aspx.cs" Inherits="PresentationLayer.Admin.EditRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
  
    <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }
    </style>
   <%--<script>
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

   </script>--%>
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
                        </div>
                    </div>
        <div >
            <div >

                <div >

                    <div class="modal-header bg-aqua-active">

                        <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span></button>--%>

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
                                
                                        <asp:DropDownList ID="ddlEditDocumentType" AutoPostBack="true" OnSelectedIndexChanged="ddlEditDocumentType_SelectedIndexChanged" class="form-control" runat="server">
                                        </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added"  ControlToValidate="ddlEditDocumentType" runat="server" InitialValue="0" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    
                                    
                            </div>

                            <div class="col-md-6">
                                <label>Enter Rule Expression *</label>
                                <asp:TextBox class="form-control" ID="txtEditAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <asp:TextBox class="form-control" Visible="false" ID="txtDynamicCtrID"  runat="server"></asp:TextBox>
                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="EditValida"  ControlToValidate="txtEditAuditQuestion" runat="server"  ErrorMessage="Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                     
                                <input type="hidden" class="form-control" id="hiddenDynamicControlID" name="hiddenDynamicControlID" />

                            </div>
                            


                            <div class="modal-footer">
                             
                                <asp:Button ID="btnUpdate" ValidationGroup="EditValida" runat="server" Text="Save Changes" OnClick="btnUpdate_Click" class="btn btn-primary" onclientclick="return validedit();"   />
                                <asp:Button ID="btnBack" ValidationGroup="EditValida" runat="server" Text="Back" OnClick="btnBack_Click" class="btn btn-primary" />
                                   <%--<button  type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
                        </asp:Panel>
                        </asp:Content>
