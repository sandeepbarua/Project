<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="PresentationLayer.Admin.Customers" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                       <script type="text/javascript">
        function Redirect()
        {location.href="Locations.aspx"}

    </script>
           <script type="text/javascript">
         function Confirm() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Do you want to Delete data?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
         function HideLabel() {
             var seconds = 5;
             setTimeout(function () {
                 document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
                 }, seconds * 1000);
             };

             function HideLabelError() {
                 var seconds = 5;
                 setTimeout(function () {
                     document.getElementById("<%=lblMessageError.ClientID %>").style.display = "none";
        }, seconds * 1000);
             };
         function HideLabelErrorDelete() {
             var seconds = 5;
             setTimeout(function () {
                 document.getElementById("<%=lblMessageErrorDelete.ClientID %>").style.display = "none";
                 }, seconds * 1000);
             };
      </script>
     <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }
    </style>
    <asp:Panel ID="Panel1" runat="server">
                 <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">CONFIGURE CLIENT</h4>
                     <asp:LinkButton ID="btnadd" runat="server" ToolTip="Add" Text="Add New Client" class="mq-btn btn btn-primary nxt_btn"  OnClick="btnadd_Click"><%--data-toggle="modal" data-target="#myModelAdd"--%>
                          
                        </asp:LinkButton>
            </div>
                <!-- /.box-header -->
                
               
                        <div class="col col-12 text-right">
                   
                         <Center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </Center>  

                        
                    </div>
                <br />
                <!-- /.box-header -->
               
        
        <table id="test" class="table table-striped table-bordered">
          
                        <thead>
                            <tr >
                                <th style="width:5%">S.No</th>
                                
                                <th>Client ID</th>

                                      <th >Client Name</th>
                                      <th style="width:13%">Onboarded On</th>

                                <th style="width:15%">Actions</th>
                                
                            </tr>
                        </thead>
           
                     <tbody>
                           
                           <asp:Repeater ID="ReptUse" runat="server">
                                <ItemTemplate>
                                    <tr id = "trID" runat="server" ><%--onclick="Redirect()" onmouseover="this.style.cursor='pointer'"--%>
                                        <td style="width:5%"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>

                                        </td>
                                   <%--  <td class="classCompanyID" style="display:none" ><%# DataBinder.Eval(Container.DataItem, "CompanyID")%>--%>

                                    <%-- </td>--%>
                                        <td style="width:15%"><%# DataBinder.Eval(Container.DataItem, "FADV_CustomerID")%></td>
                                       <td> 
                                               <%# DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                           

                                       </td>
                                        <td style="width:13%"> 
                                               <%# DataBinder.Eval(Container.DataItem, "DateofCreation")%>
                                           

                                       </td>
                                       <%-- <td id="CompanyNames" style="width:100px" class="classCompanyName"></td>--%>
                                        <td  style="width:15%">

                                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="btn btn-outline-info">
                                               <i class="fa fa-pencil"></i>                           
                                            </asp:LinkButton>
                                             <asp:LinkButton ID="OnDelete" runat="server" class="btn  btn-outline-danger" ToolTip="Delete" OnClick="OnDelete_Click"  OnClientClick = "return confirm('Do you want to Delete data?')"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                  
                                          
                                                 <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("CompanyID")%>' />
                                                 <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("CompanyName")%>' />
                                                 <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("FADV_CustomerID")%>' />
                                            <asp:LinkButton ID="lbView" runat="server" class="btn  btn-outline-secondary" ToolTip="View Documents" OnClick="lbView_Click"  ><i class="fa fa-bars"></i> </asp:LinkButton>
                                         
                                                  </td>
                                      
                                        </tr>
                                        
                                </ItemTemplate>
                               
                            </asp:Repeater>
                                        
                        </tbody>
              
                    </table>
    <%--<script language="javascript">
 
        var tbl = document.getElementById("Table1");
        if (tbl != null) {
            for (var i = 0; i < tbl.rows.length; i++) {
                for (var j = 0; j < tbl.rows[i].cells.length; j++)
                    tbl.rows[i].cells[j].onclick = function () { getval(this); };
            }
        }
 
        function getval(cel) {
            alert(cel.innerHTML);
        }
    </script>--%>
      
                <%--Edit Account--%>
                <div class="modal fade" id="myModal">
                    <div class="modal-dialog">

                        <div class="modal-content">

                            <div class="modal-header bg-aqua-active">

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                                    <span aria-hidden="true">&times;</span></button>

                                            <h4 class="modal-title">Edit Customer</h4>

                            </div>

                            <div class="modal-body">

                                <div class="row">
                                    <asp:ScriptManager ID="ScriptManager1" runat="Server"></asp:ScriptManager>
                                   <div class="col-md-6">
                                        <label>Enter Customer ID *</label>
                                        <asp:TextBox class="form-control" ID="txtEditLocationFadvID" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="EditRoleNameValidator" ValidationGroup="addedit"  ControlToValidate="txtEditLocationFadvID" runat="server" ErrorMessage="Please Enter Customer ID" ForeColor="Red"></asp:RequiredFieldValidator>
                                   
                                        <input type="hidden" class="form-control" id="hiddenCompanyID" name="hiddenCompanyID" />
                                 
                                    </div>
                                    <div class="col-md-6">
                                        <label>Enter Company Name *</label>
                                        <asp:TextBox class="form-control" ID="txtEditCompanyName" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="addedit"  ControlToValidate="txtEditCompanyName" runat="server" ErrorMessage="Please Enter Company Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                   
                                    </div>
                                    

                                
                                <div class="modal-footer">
                               
                                    <asp:Button ID="btnUpdate" runat="server" ValidationGroup="addedit" Text="Save Changes" class="btn btn-primary" OnClick="btnUpdate_Click" /></button>
                                         <button type="button"  class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>



                <%--create new Account--%>

                <div class="modal fade" id="myModelAdd">

                    <div class="modal-dialog">

                        <div class="modal-content">

                            <div class="modal-header bg-aqua-active">

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                                    <span aria-hidden="true">&times;</span></button>

                                <h4 class="modal-title">Add Customer</h4>

                            </div>

                            <div class="modal-body">

                                <div class="row">
                                      <div class="col-md-6">
                                        <label>Enter Customer ID *</label>
                                        <asp:TextBox class="form-control" ID="txtAddFadvLocationId" autocomplete="off" runat="server"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="added"  ControlToValidate="txtAddFadvLocationId" runat="server" ErrorMessage="Please Enter Customer ID" ForeColor="Red"></asp:RequiredFieldValidator>
                                   
                                        <input type="hidden" class="form-control" id="hiddenLocationIDs" name="hiddenLocationID" />
                                 
                                    </div>
                                    <div class="col-md-6">
                                        <label>Enter Company Name *</label>
                                        <asp:TextBox class="form-control" ID="txtAddCompanyName" autocomplete="off" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="added"  ControlToValidate="txtAddCompanyName" runat="server" ErrorMessage="Please Enter Company Name " ForeColor="Red"></asp:RequiredFieldValidator>
                                   
                                    </div>
                                    

                                    <div class="modal-footer">
                                        <%-- <button type="button" class="btn btn-default" id="close" runat="server" data-dismiss="modal">Close</button>--%>
                                  
                                        <asp:Button ID="BtnCreateuser" runat="server" Text="Save" OnClick="BtnCreateuser_Click" ValidationGroup="added" class="btn btn-primary" />
                                              <asp:Button ID="close" OnClick="close_Click" runat="server" Text="Close" class="btn btn-default" />
                                    </div>


                                </div>

                            </div>

                        </div>

                    </div>
                </div>
            </asp:Panel>
 
   
    <script>
        $(function () {
            $(".editID").click(function () {
                $("[id*=hiddenCompanyID]").val($.trim($(".classCompanyID", $(this).closest("tr")).html()));
                $("[id*=txtEditLocationFadvID]").val($.trim($(".classFADV_CustomerID", $(this).closest("tr")).html()));
            
                $("[id*=ddlEditLocation]").val($.trim($(".classLocationID", $(this).closest("tr")).html()));
                $("[id*=txtEditCompanyName]").val($.trim($(".classCompanyName", $(this).closest("tr")).html()));
               });
        });
    </script>
     <script>
       $('#test').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 3}], // hide sort icon on header of first column
            'aaSorting': [[1, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
 </script>
    
</asp:Content>
