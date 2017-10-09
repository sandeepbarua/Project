<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="AddUserDeatils.aspx.cs" Inherits="PresentationLayer.Admin.AddUserDeatils" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMainBody" runat="server">

        <div class="main-panel">

            <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">REGISTER USER</h4>
            </div>
                  

                    <div class="White_box ">
        <div class="row">
          <div class="col-lg-8">
               <div class="row">
              <div class="col-lg-3">
       <label>First Name *</label>
              </div>
              <div class="col-lg-5">
                 <asp:TextBox ID="txtFirstName" CssClass="form-control" name="fullName" MaxLength="50" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added" Display="Dynamic" ControlToValidate="txtFirstName" runat="server" ErrorMessage="Please Enter First Name" ForeColor="white"></asp:RequiredFieldValidator>
                     <input type="hidden" class="form-control" id="ddlAccountId1" name="ddlAccountId1" /> 
              </div>
            
            </div>
            <div class="row">
              <div class="col-lg-3">
                <label>Last Name *</label>
              </div>
              <div class="col-lg-5">
                <asp:TextBox class="form-control" ID="txtLastName" MaxLength="50" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="added" Display="Dynamic" ControlToValidate="txtLastName" runat="server" ErrorMessage="Please Enter Last Name" ForeColor="white"></asp:RequiredFieldValidator>

              </div>
             
            </div>
            <div class="row">
              <div class="col-lg-3">
                <label>Role *</label>
              </div>
              <div class="col-lg-5">
                <asp:DropDownList ID="dllSelectRole" class="form-control" runat="server">
                            <asp:ListItem Text="Select Role " Value="0"></asp:ListItem>
                            <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Data Entry Operator" Value="2"></asp:ListItem>
                            <asp:ListItem Text="QC Manager" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Template Manager" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Audit Manager" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Client Manager" Value="6"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="added" Display="Dynamic" ControlToValidate="dllSelectRole" runat="server" ErrorMessage="Please Select the Role" InitialValue="0" ForeColor="white"></asp:RequiredFieldValidator>

              </div>
             
            </div>
            <div class="row">
              <div class="col-lg-3">
                <label>Email *</label>
              </div>
              <div class="col-lg-5">
                
                        <asp:TextBox class="form-control" ID="txtEmailId" runat="server" MaxLength="120"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="added" ControlToValidate="txtEmailId" runat="server" ErrorMessage="Please Enter Email id" ForeColor="white"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="added" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailId" ErrorMessage="Invalid Email Format" ForeColor="white"></asp:RegularExpressionValidator>

              </div>
            
            </div>
              <div class="row">
              <div class="col-lg-3"></div>
            <div class="col-lg-5" align="right">    
                <asp:Button ID="BtnCreateuser" runat="server" Text="Save" OnClick="BtnCreateuser_Click" ValidationGroup="added" class="mq-btn btn btn-primary nxt_btn" />
            <asp:Button ID="btncanal" runat="server" Text="Cancel" OnClick="Btnbtncanal_Click" class="mq-btn btn btn-primary nxt_btn" /></div>
          </div>
          
        </div>
      </div>
              
           
           
      

    


        </div>



    </asp:Panel>
</asp:Content>
