<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin2.Master" EnableEventValidation="false" CodeBehind="CreateAuditQusRule.aspx.cs" Inherits="PresentationLayer.Admin.CreateAuditQusRule" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />
    <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.0/js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#Table1').dataTable();
        });
    </script>

    <asp:Panel ID="pnlMainBody" runat="server">
        <div class="box-header">
            <div class="row" align="right">
                <center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </center>
                <div class="col col-12 text-right">
                    <asp:LinkButton ID="btnadd" runat="server" class="mq-btn btn btn-primary nxt_btn" ToolTip="Add New Rule" OnClick="btnadd_Click" Text="Add Rule">

                    </asp:LinkButton>
                </div>
            </div>
        </div>



        <table id="Table1" style="margin-bottom: 2px" class="table  table-bordered table-striped display">

            <thead>
                <tr>
                    <th class="col" style="width: 20px; text-align: center">S.No</th>

                    <%--<th class="col" style="width: 150px; color: white; text-align: center">Document Type Name</th>--%>
                    <th class="col" style="width: 200px; text-align: center">Audit Question</th>
                    <th class="col" style="width: 100px; display: none; text-align: center">Dynamic Control ID</th>
                    <th class="col" style="width: 50px; text-align: center">Label Name</th>
                    <th class="col" style="width: 100px; text-align: center">Rule Expression</th>
                    <th class="col" style="width: 80px; text-align: center">RuleType</th>
                    <th class="col" style="width: 20px; text-align: center">Actions</th>

                </tr>
            </thead>

            <tbody>

                <asp:Repeater ID="Reptrule" runat="server">
                    <ItemTemplate>
                        <tr id="trID" runat="server">
                            <td id="serial"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                            <td class="hiddenDynamicControlID"><%# DataBinder.Eval(Container.DataItem, "AuditQuestion")%>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("AuditRuleClassification")%>' />
                            </td>
                            <td id="DocumentTypeName" class="DynContrId" style="width: 150px; display: none"><%# DataBinder.Eval(Container.DataItem, "DynamicControlId")%></td>
                            <td id="CompanyName" class="labelname" style="width: 150px"><%# DataBinder.Eval(Container.DataItem, "labelName")%></td>
                            <td id="UserName" style="width: 150px" class="RuleExpression"><%# DataBinder.Eval(Container.DataItem, "RuleExpression")%></td>
                            <td id="CustomerID" style="width: 100px;" class="RuleType"><%# DataBinder.Eval(Container.DataItem, "RuleType")%></td>

                            <td id="actions">

                                <%--<asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID imgEdit btn-sm" data-toggle="modal" data-target="#myModal">
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>--%>
                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("AuditRuleClassification")%>' />
                                <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("RuleExpression")%>' />
                                <asp:HiddenField ID="HiddenField4" runat="server" Value='<%#Eval("DynamicControlId")%>' />
                                <asp:HiddenField ID="HiddenField5" runat="server" Value='<%#Eval("AuditQuestion")%>' />



                                <%--<asp:LinkButton ID="btn_Edit" runat="server" OnClick="btn_Edit_Click" ToolTip="Edit" >
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>--%>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ToolTip="Edit" class="editID btn btn-outline-info" data-toggle="modal" data-target="#myModal">
                                              <i class="fa fa-pencil"></i> 
                                </asp:LinkButton>
                                <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><img src="Images/edit.png" /></asp:LinkButton>--%>

                                <%-- <asp:ImageButton ID="OnEdit" runat="server" ImageUrl="Images/edit.png" Height="18px" OnClick="btnEdit_Click"  />--%>

                                <asp:LinkButton ID="OnDelete" runat="server" class="btn  btn-outline-danger" ToolTip="Delete" OnClick="OnDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                <%--<asp:ImageButton ID="OnDelete" runat="server" ImageUrl="Images/Delete.png" Height="18px" OnClick="OnDelete_Click"  OnClientClick="return confirm('Do you want to Delete data?')" />--%>

                                           
                            </td>

                        </tr>


                    </ItemTemplate>

                </asp:Repeater>


            </tbody>

        </table>


    </asp:Panel>
    <script>
        $('#Table1').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 4 }], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
    </script>
</asp:Content>
