<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAuditQusRule.aspx.cs" MasterPageFile="~/Admin/Admin2.Master" Inherits="PresentationLayer.Admin.AddAuditQusRule" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />
    <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.0/js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#Table1').dataTable();
        });
    </script>
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:Panel ID="pnlMainBody" runat="server">
       
        

        <div>

            <div>

                <div>

                    <div class="modal-header bg-aqua-active">



                        <h4 class="modal-title">Add Audit Rules</h4>

                    </div>
                       
                    <div class="modal-body">

                        <div class="row">

                            <table>
                                <tr>
                                    <td>
                                        <label>Select Control Name *</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAddauditrule" class="form-control" AutoPostBack="True" runat="server" Width="500" Height="40">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="abc" ControlToValidate="ddlAddauditrule" runat="server" ErrorMessage="Please Data Field" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>

                                </tr>

                                <tr>


                                    <td>
                                        <label>Select Rule Type *</label>
                                    </td>
                                    <td style="padding-top:10px">
                                        <asp:DropDownList ID="ddlruletype" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlruletype_SelectedIndexChanged" Width="500" Height="40">
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="abc" ControlToValidate="ddlruletype" InitialValue="0" runat="server" ErrorMessage="Please Select Rule" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top:50px">
                                        <label>Rule Expression </label>
                                        </td>
                                    <td>
                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtAuditQuestion" runat="server" Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="padding-top:30px">

                                        
                                <asp:Button ID="BtnCreateRule" runat="server" Text="Submit" OnClick="BtnCreateRule_Click" OnClientClick="return valid()" ValidationGroup="abc" class="btn btn-primary" />
                                <asp:Button ID="btnBack" ValidationGroup="EditValida" runat="server" Text="Back" OnClick="btnBack_Click" class="btn btn-primary" />
                                    </td>
                                </tr>
                            </table>
                      
                            <div class="modal-footer">
                                <%--<button type="button"  id="close" runat="server" data-dismiss="modal" class="btn btn-primary">Close</button>--%>

                                <%--  <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />--%>
                            </div>


                        </div>

                    </div>
                       
                </div>

            </div>
        </div>

    </asp:Panel>
    <script>
        $('#Table1').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 4 }], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
    </script>
</asp:Content>
