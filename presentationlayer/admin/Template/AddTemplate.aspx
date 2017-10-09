<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="AddTemplate.aspx.cs" Inherits="PresentationLayer.Admin.Template.AddTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMainBody" runat="server">
            
                 <div class="main-panel">
                        <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">ADD TEMPLATE</h4>
            </div>

               <div class="White_box ">
        <div class="row">
          <div class="col-lg-8">
               <div class="row">
              <div class="col-lg-3">
                 
              </div>
              <div class="col-lg-4">
                   <asp:ValidationSummary 
            ID="ValidationSummary1" 
            runat="server" 
            HeaderText="Following error occurs....." 
            ShowMessageBox="false" 
            DisplayMode="BulletList" 
            ShowSummary="true"
            BackColor="Snow"
           ValidationGroup="added"
            Width="400"
            ForeColor="Red"
            Font-Size="Small"
          
            Font-Italic="true" />
              </div>
             </div>
               <div class="row">
              <div class="col-lg-3">
                 <label>Template Name <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added"  ControlToValidate="txtTemplateName"  runat="server" ErrorMessage="Please Enter Template Name" ForeColor="Red" Text="*"></asp:RequiredFieldValidator></label>
              </div>
              <div class="col-lg-5">
                  <asp:TextBox  ID="txtTemplateName" CssClass="form-control" name="fullName" runat="server"></asp:TextBox>
                   <asp:Label ID="lbExistmsg" runat="server" ForeColor="Red" Text="Label" Visible="false">Template name already exist</asp:Label>
                                     
              </div>
             </div>
            <div class="row">
              <div class="col-lg-3">
               <label>Description</label>
              </div>
              <div class="col-lg-5">
                <asp:TextBox class="form-control" ID="txtDocumentDescrition" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
              </div>             
            </div>
              <div class="row">
             <br />
                  </div>
              <div class="row">
              <div class="col-lg-3"></div>
            <div class="col-lg-5" align="right" >    
                 <asp:Button ID="BtnAddTemplate" runat="server" Text="Save"  ValidationGroup="added" class="mq-btn btn btn-primary nxt_btn"  OnClick="BtnAddTemplate_Click" />
                                        <asp:Button ID="btnCacel" runat="server" Text="Cancel" class="mq-btn btn btn-primary nxt_btn"  OnClick="Btncancel_Click"/>
          </div>
                   </div>
              </div>
            </div>
                   </div>
                   
          
                        
                                



                     </asp:Panel>
</asp:Content>
