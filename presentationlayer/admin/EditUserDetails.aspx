<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="EditUserDetails.aspx.cs" Inherits="PresentationLayer.Admin.EditUserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMainBody" runat="server">


        <div class="main-panel">
            <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">EDIT USER</h4>
            </div>
            <br />
            <br />
            <div align="center">

                <asp:ValidationSummary
                    ID="ValidationSummary1"
                    runat="server"
                    HeaderText="Following error occurs....."
                    ShowMessageBox="false"
                    DisplayMode="BulletList"
                    ShowSummary="true"
                    BackColor="Snow"
                    ValidationGroup="edited"
                    Width="400"
                    ForeColor="Red"
                    Font-Size="Small"
                    Font-Italic="true" />
            </div>
            <div class="White_box ">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Enter First Name * </label>
                            </div>
                            <div class="col-lg-5">
                                <asp:TextBox class="form-control" ID="txtEditFirstName" runat="server" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="edited" Display="Dynamic" ControlToValidate="txtEditFirstName" runat="server" ErrorMessage="Please Enter First Name" ForeColor="white"></asp:RequiredFieldValidator>
                                <input type="hidden" class="form-control" id="HiddenID" name="ddlAccountId1" />
                                <asp:HiddenField ID="hfUserId" runat="server" />
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Enter Last Name * </label>
                            </div>
                            <div class="col-lg-5">
                                <asp:TextBox class="form-control" ID="txtEditLastName" runat="server" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="edited" Display="Dynamic" ControlToValidate="txtEditLastName" runat="server" ErrorMessage="Please Enter Last Name" ForeColor="white"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Select Role *</label>
                            </div>
                            <div class="col-lg-5">
                                <asp:DropDownList ID="ddlEditRole" class="form-control" runat="server">
                                    <asp:ListItem Text="Select Role " Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Data Entry Operator" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="QC Manager" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Template Manager" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Audit Manager" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Client Manager" Value="6"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="edited" Display="Dynamic" ControlToValidate="ddlEditRole" runat="server" ErrorMessage="Please Select the Role" InitialValue="0" ForeColor="white"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Email *</label>
                            </div>
                            <div class="col-lg-5">
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationGroup="edited" Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEditEmail" ErrorMessage="Invalid Email Format" ForeColor="Red"></asp:RegularExpressionValidator>
                                <asp:TextBox class="form-control" ID="txtEditEmail" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="edited" ControlToValidate="txtEditEmail" runat="server" ErrorMessage="Enter Email address" ForeColor="white"></asp:RequiredFieldValidator>
                            </div>

                        </div>


                        <div class="row">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-5" align="right">
                                <asp:Button ID="btnUpdate" runat="server" Text="Save Changes" ValidationGroup="edited" class="mq-btn btn btn-primary nxt_btn" OnClick="btnUpdate_Click" />

                                <asp:Button ID="btncancal" runat="server" Text="Cancel" class="mq-btn btn btn-primary nxt_btn" OnClick="btncancal_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



    </asp:Panel>
</asp:Content>
