<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="EditTemplate.aspx.cs" Inherits="PresentationLayer.Admin.Template.EditTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMainBody" runat="server">

        <div class="main-panel">
            <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">EDIT TEMPLATE</h4>
            </div>
            <div class="White_box ">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="row" style="padding-bottom:0px">
                            <div class="col-lg-3" style="padding-bottom:0px">
                                <label>Enter Template</label>
                            </div>
                            <div class="col-lg-5" style="padding-bottom:0px">
                                <asp:TextBox ID="txtTemplateName" CssClass="form-control" name="fullName" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ValidationGroup="added" ControlToValidate="txtTemplateName" runat="server" ErrorMessage="Please Enter Template Name" ForeColor="Red"></asp:RequiredFieldValidator>
                               <asp:Label ID="lbExistmsg" runat="server" ForeColor="Red" Text="Label" Visible="false">Template name already exist</asp:Label>
                            </div>

                        </div>
                        <div class="row" style="padding-top:0px">
                            <div class="col-lg-3">
                                <label>Enter Description</label>
                            </div>
                            <div class="col-lg-5">
                                <asp:TextBox class="form-control" ID="txtDocumentDescrition" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row" style="padding-top:10px">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-5" align="right">
                                <asp:Button ID="BtnAddTemplate" runat="server" Text="Save" ValidationGroup="added" class="mq-btn btn btn-primary nxt_btn" OnClick="OnEdit_Click" />
                                <asp:Button ID="btnCacel" runat="server" Text="Cacel" class="mq-btn btn btn-primary nxt_btn" OnClick="OnCancel_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>




    </asp:Panel>
</asp:Content>
