<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin2.Master" CodeBehind="EditRule.aspx.cs" Inherits="PresentationLayer.Admin.EditRule" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMainBody" runat="server">

        <div class="main-panel">
            <br />
            <div align="center">
                Edit Audit Rules
            </div>
            <br />
            <br />
            <div align="center">
                <table width="70%">
                   
                    <tr>
                        <td>
                            <label>Select Function Type</label>
                        </td>
                        <td>
                            <asp:RadioButton ID="rb_Logic" runat="server" Text="Logical" AutoPostBack="true" GroupName="QuestionType" OnCheckedChanged="rb_Logic_CheckedChanged"/>
                        
                            <asp:RadioButton ID="rb_calculate" runat="server" Text="Calculation" AutoPostBack="true" GroupName="QuestionType" OnCheckedChanged="rb_calculate_CheckedChanged"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Audit Question</label>
                        </td>
                        <td style="padding-top:20px">
                            <asp:Label ID="lblAuditQuestion" runat="server" class="form-control"></asp:Label>
                            </td>
                    </tr>
                        <td>
                            <label>Select Rule</label>
                        </td>
                        <td style="padding-top:20px">
                            <asp:DropDownList ID="ddlRuleType" AutoPostBack="true"  class="form-control" runat="server" OnSelectedIndexChanged="ddlRuleType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added" ControlToValidate="ddlRuleType" runat="server" InitialValue="0" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <label>Select Data Field</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDataField" AutoPostBack="true"  class="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="added" ControlToValidate="ddlDataField" runat="server" InitialValue="0" ErrorMessage="Please Select Document Type" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <label>Rule Expression</label>
                        </td>
                        <td>
                            <asp:TextBox class="form-control" ID="txtEditAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                </table>

            </div>

            <div class="modal-footer">
                <asp:Button ID="btnEditRule" runat="server" Text="Save Changes" ValidationGroup="added" class="btn btn-primary" OnClick="btnEditRule_Click"/>
                <asp:Button ID="btnCacel" runat="server" Text="Cacel" class="btn btn-primary"  OnClick="btnCacel_Click"/>

            </div>


        </div>



    </asp:Panel>
</asp:Content>
