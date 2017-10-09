<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="NewAuditQuesRules.aspx.cs" Inherits="PresentationLayer.AdminNew.NewAuditQuesRules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="main-panel">
        <div class="modal-body">

            <div class="row">
                <table width="70%" align="Center">
                    <tr>

                        <td width="25%" class="auto-style4">Select Document
                            
                        </td>
                        <td width="50%" class="auto-style4">
                            <asp:DropDownList ID="ddlDocumentType" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>

                        <td width="25%">Select Audit Question
                            
                        </td>
                        <td width="50%">
                            <asp:DropDownList ID="ddlAuditQuestion" class="form-control" runat="server">
                                <%--OnSelectedIndexChanged="ddlAuditQuestion_SelectedIndexChanged"--%>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtNewAuditQuestion" class="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Select Type Of Question
                            
                        </td>
                        <td>
                            
                                    <asp:RadioButton ID="Rb_Logical" runat="server" Text="Logical" AutoPostBack="true" GroupName="QuestionType" OnCheckedChanged="Rb_Logical_CheckedChanged" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                               
                            <asp:RadioButton ID="Rb_Calculation" runat="server" Text="Calculation" GroupName="QuestionType" AutoPostBack="true" OnCheckedChanged="Rb_Calculation_CheckedChanged" />
                                

                        </td>
                    </tr>
                    <tr>
                        <td>
                           
                                Select Function
                            
                        </td>
                        <td>

                            <asp:DropDownList ID="ddlSelectFunction" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectFunction_SelectedIndexChanged">
                                <%-- OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged"--%>
                            </asp:DropDownList>
                        </td>
                    </tr>

                </table>
            </div>

        </div>
    </div>
    <%--------------Pop-Up-------Dialog---------------------------%>
    <script>
        //$(function () {
        //    $(document).on("change", "#ContentBody_ddlSelectFunction", function () {
        //        debugger
        //        $('#myModelAdd').modal('show');
        //    });
        //});
        function OpenModalWindow() {
            $('#myModelWindow').modal('show');
        }
    </script>
    <div class="modal fade" id="myModelWindow">

        <div class="modal-dialog">

            <div class="modal-content">

                <div class="modal-header bg-aqua-active">

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                        <span aria-hidden="true">&times;</span></button>

                    <h4 class="modal-title">Add Audit Expression</h4>

                </div>

                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">

                            <asp:Repeater ID="rptDynamicControl" runat="server" OnItemDataBound="rptDynamicControl_ItemDataBound">
                                <HeaderTemplate>
                                    <table style="border-spacing: 10px;">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="padding: 6px;">
                                            <asp:Label Text='<%#Eval("ParameterName")%>' runat="server" />
                                        </td>
                                        <td style="padding: 6px;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div id="dvDropDown" runat="server">
                                                        <asp:DropDownList ID="ddl_Parameters" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="myDropdown_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_Parameters" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>



                                        </td>

                                    </tr>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Expression</label>
                        </div>
                        <div class="col-md-8">
                            <asp:UpdatePanel runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlSelect">
                                        <asp:TextBox class="form-control" ID="txtAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="modal-footer">

                        <%-- <button type="button" class="btn btn-default" id="close" runat="server" data-dismiss="modal">Close</button>--%>

                        <asp:Button ID="BtnCreateRule" runat="server" Text="Submit" ValidationGroup="add" class="btn btn-primary" OnClick="BtnCreateRule_Click" />
                        <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />
                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
