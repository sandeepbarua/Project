<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="UserLoginDetail.aspx.cs" Inherits="PresentationLayer.Admin.UserLoginDetail" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>--%>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     
    
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />
    <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="JavaScript/1.js"></script>
    <script>
        $(document).ready(function () {
            $('#test1').dataTable();
        });
    </script>
    <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }
    </style>

    <asp:Panel ID="pnlMainBody" runat="server">

        <!-- /.box-header -->
        <div class="main-panel">
             <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">USER DETAILS</h4>
            </div>
            <div class="White_box ">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="row">
                            <div class="col-lg-3">
                                <label class="form-control-label">Name:</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblname" runat="server" Text="" CssClass="form-control-label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Email:</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblemail" runat="server" Text="" CssClass="form-control-label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Role:</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblrole" runat="server" Text="" CssClass="form-control-label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Status:</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblstatus" runat="server" Text="" CssClass="form-control-label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label class="form-control-label">Account Creation On:</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lbldatecreation" runat="server" Text="" CssClass="form-control-label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Last Modified Date</label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lbldatemodification" runat="server" Text="" CssClass="form-control-label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div id="test_wrapper" class="dataTables_wrapper no-footer" runat="server" visible="false">
            <div class="modal-header bg-aqua-active">
                <h5 class="modal-title">DATA ENTRY REPORT</h5>
            </div>

            
            <table id="test2" class="table table-striped table-bordered" >

                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Fax ID</th>
                        <th>Client Name</th>
                        <th>Document Name</th>
                        <th>No Of Pages</th>
                        <th>Date</th>
                        <th>StartTime</th>
                        <th>EndTime</th>
                        <th>MisMatch Count</th>
                    </tr>
                </thead>

                <tbody>

                    <asp:Repeater ID="reptdataentry" runat="server">
                        <ItemTemplate>
                            <tr id="trID" runat="server">
                                <td><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "FaxId")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "ClientName")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "DocumentName")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "TotalNumberOfPages")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "DateDisplayText")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "StartTime")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "EndTime")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "TotalError")%></td>
                            </tr>


                        </ItemTemplate>

                    </asp:Repeater>




                </tbody>

            </table>
                </div>

             <div id="DetailsDiv" class="dataTables_wrapper no-footer" runat="server" visible="false">
                
            <div class="modal-header bg-aqua-active">
                <h5 class="modal-title">LOGIN DETAILS</h5>
            </div>

            <table id="test1" class="table table-striped table-bordered">

                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Login Time</th>

                        <th>Logout Time</th>

                        <th>IP Address</th>

                    </tr>
                </thead>

                <tbody>

                    <asp:Repeater ID="ReptUse" runat="server">
                        <ItemTemplate>
                            <tr id="trID" runat="server">
                                <td id="serial"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                <td id="email" class="classEmail"><%# DataBinder.Eval(Container.DataItem, "DateDisplayLogin")%></td>
                                <td id="emaild" class="classEmail"><%# DataBinder.Eval(Container.DataItem, "DateDisplayLogout")%></td>
                                <td id="RoleName" class="classRoleName"><%# DataBinder.Eval(Container.DataItem, "IPAddress")%></td>

                            </tr>


                        </ItemTemplate>

                    </asp:Repeater>




                </tbody>

            </table>
                 </div>

        

    </asp:Panel>

    <%-- <script>
        $('#test1').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 4}], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
 </script>
    <script>
        $('#test2').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 3}], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
 </script>--%>
</asp:Content>
