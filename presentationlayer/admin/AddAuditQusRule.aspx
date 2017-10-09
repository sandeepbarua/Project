<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAuditQusRule.aspx.cs" MasterPageFile="~/Admin/Admin.Master" Inherits="PresentationLayer.Admin.AddAuditQusRule" %>

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

        <div>

            <div>

                <div >

                    <div class="modal-header bg-aqua-active">

                        

                        <h4 class="modal-title">Add Audit Rules</h4>

                    </div>

                    <div class="modal-body">

                        <div class="row">
                            

                            <div class="col-md-6">
                                 <label>Select Control Name *</label>
                                          
                                        <asp:DropDownList ID="ddlAddauditrule" class="form-control" AutoPostBack="True" runat="server">
                                              </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="add"  ControlToValidate="ddlAddauditrule" runat="server" ErrorMessage="Please Select Company" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                       
                                    
                            </div>


                            <div class="col-md-6">
                                <label>Select Rule Type *</label>
                               
                                       

                                        

                                        <asp:DropDownList ID="ddlruletype"   class="form-control" runat="server" Width="250px" Height="34px"  AutoPostBack="True" OnSelectedIndexChanged="ddlruletype_SelectedIndexChanged" >
                                        </asp:DropDownList>

                                    
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="abc"  ControlToValidate="ddlruletype" InitialValue="0" runat="server" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                               
                            </div>

                            <div class="col-md-6">

                                <label>Rule Expression </label>
                             <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtAuditQuestion"  runat="server"></asp:TextBox>
                                         
                                   
                            </div>


                            <div class="modal-footer">
                                 <%--<button type="button"  id="close" runat="server" data-dismiss="modal" class="btn btn-primary">Close</button>--%>
                               
                                <asp:Button ID="BtnCreateRule" runat="server" Text="Submit" OnClick="BtnCreateRule_Click" OnClientClick="return valid()" ValidationGroup="abc" class="btn btn-primary" />
                               <asp:Button ID="btnBack" ValidationGroup="EditValida" runat="server" Text="Back" OnClick="btnBack_Click" class="btn btn-primary" />
                                <%--  <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />--%>

                            </div>


                        </div>

                    </div>

                </div>

            </div>
        </div>
        
        </asp:Panel>
    </asp:Content>