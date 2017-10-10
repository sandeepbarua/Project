<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="EditControl.aspx.cs" Inherits="PresentationLayer.Admin.Template.EditControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:panel id="pnlMainBody" runat="server">
            
                 <div class="main-panel">
                        <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">EDIT CONTROL</h4>
            </div>
                      <div class="White_box ">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="row" >
                            <div class="col-lg-3" >
                               <label>Template</label>
                            </div>
                            <div class="col-lg-5">
                              <asp:Label ID="ddlDocumentType" runat="server" class="form-control"></asp:Label>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValida" ValidationGroup="added"  Enabled="false" ControlToValidate="txtEditLabelName" runat="server" ErrorMessage="Enter data field" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                         <div class="row" >
                            <div class="col-lg-3" >
                               <label>Field Name</label>
                            </div>
                            <div class="col-lg-5">
                               <asp:TextBox class="form-control" ID="txtEditLabelName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="added" ControlToValidate="txtEditLabelName" runat="server" ErrorMessage="Enter data field" ForeColor="Red"></asp:RequiredFieldValidator>

                                <input type="hidden" class="form-control" id="hiddenDynamicControlID" name="hiddenDynamicControlID" />
                            </div>

                        </div>
                        <div class="row" >
                            <div class="col-lg-3" >
                               <label>Drop Down Values</label>
                            </div>
                            <div class="col-lg-5">
                               <asp:TextBox class="form-control" ID="txtEditDropDown" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="added" ControlToValidate="txtEditDropDown" runat="server" ErrorMessage="Enter Label Name" ForeColor="Red"></asp:RequiredFieldValidator>
                               
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-5" align="right">
                                 <asp:Button ID="btnUpdate" ValidationGroup="EditValida" runat="server" Text="Save Changes"  class="mq-btn btn btn-primary nxt_btn" OnClick="btnUpdate_Click" />
                                       <asp:Button ID="btnCancel" ValidationGroup="EditValida" runat="server" Text="Cancel"  class="mq-btn btn btn-primary nxt_btn" OnClick="btnCancel_Click" />
                            </div>

                        </div>

              



                     </asp:panel>
</asp:Content>
