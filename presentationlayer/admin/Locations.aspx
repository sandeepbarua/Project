<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Locations.aspx.cs" Inherits="PresentationLayer.Admin.Locations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
 
         <%--       <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
                    
                    <ol class="breadcrumb page-breadcrumb pull-right">
                        <li><i class="fa fa-home"></i>&nbsp;<a href="/datatable.aspx">Home</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
                        <li class="hidden"><a href="#">UI Elements</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
                        <li class="active">Location</li>
                    </ol>
                    <div class="clearfix">
                    </div>
                </div>--%>
 
            <asp:Panel ID="pnlMainBody" runat="server">
                <div class="box-header">
                    
                                        
                    <div class="row" align="right">
                             
                        

                         <Center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </Center> 
                         
                        <asp:LinkButton ID="btnadd" align="right" runat="server" ToolTip="Add" Visible="false" class="btn btn-sm" data-toggle="modal" data-target="#myModelAdd" OnClick="btnadd_Click1">
                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/Add.png" Width="20" Height="20"/>
                        </asp:LinkButton>
                          
                    </div>
                  
                </div>
                <!-- /.box-header -->
                   <div class="main-panel">
                    <div class="modal-body">

                        <div class="row">
                            <table  width="40%">
                                
                           <tr>
               <td width="25%">Company Name:&nbsp&nbsp&nbsp&nbsp          </td>
               <td style="width:50%; justify-content:flex-end; margin-bottom:10px"><asp:DropDownList ID="ddlCompanyName" runat="server" class="form-control" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
               
               </td>

              
           </tr>
       </table>
                                </div>
            </div>
            </div>        
           
<div class="template">
        
        <table id="Table1" style="margin-bottom:2px" align="center" class="table  table-bordered table-striped display">
          
                        <thead>
                            <tr style="background-color:#F6A41C">
                                <th class="col" style="width:57px 53px; color:white; text-align:center">S.No</th>
                                   <th class="col" style="width:150px; color:white; text-align:center">Company Name</th>
                                <th class="col" style="width:150px; color:white; text-align:center">Location Code</th>
                                                            
                                 <th class="col" style="width:150px; color:white; text-align:center">Location</th>

                                <th class="col" style="color:white; text-align:center">Actions</th>
                                
                            </tr>
                        </thead>
           
                     <tbody>
                           
                           <asp:Repeater ID="ReptUse" runat="server">
                                <ItemTemplate>
                                    <tr id = "trID" runat="server">
                                        <td id="serial" style="width:57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                     <td class="classLocationID" style="display:none" ><%# DataBinder.Eval(Container.DataItem, "LocationID")%></td>
                                         <td id="iCompanyName" style="width:100px" class="classCompanyName"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></td>
                                        <td id="DocumentTypeName" style="width:150px" class="classFadv_LocationID"><%# DataBinder.Eval(Container.DataItem, "Fadv_LocationID")%></td>
                                                              
                                       <td id="CompanyName" style="width:100px" class="classLocationName"><%# DataBinder.Eval(Container.DataItem, "LocationName")%></td>
                                                                         <td id="CompanyNames" style="width:100px; display:none" class="classfadvCustomerId"><%# DataBinder.Eval(Container.DataItem, "FADV_CustomerID")%></td>
                                       <td id="Comid" style="width:100px; display:none" class="claCompanyID"><%# DataBinder.Eval(Container.DataItem, "CompanyID")%></td>
                                       
                                          <td id="actions" style="width:150px">

                                                <asp:LinkButton ID="btnEdit" runat="server"  OnClick="btnEdit_Click" ToolTip="Edit" class="editID imgEdit btn-sm" data-toggle="modal" data-target="#myModal">
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>

                                                  <asp:ImageButton ID="OnDelete" runat="server" ImageUrl="Images/Delete.png" Height="18px" OnClick="OnDelete_Click"  OnClientClick = "return confirm('Do you want to Delete data?')" />
                                          
                                                 <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("LocationID")%>' />
                                                  </td>
                                      
                                        </tr>
                                        
                                </ItemTemplate>
                               
                            </asp:Repeater>
                                        
                        </tbody>
              
                    </table>
            <div id="bottom_anchor"></div>
                </div>
        
    </div>
                <%--Edit Account--%>
                <div class="modal fade" id="myModal">
                    <div class="modal-dialog">

                        <div class="modal-content">

                            <div class="modal-header bg-aqua-active">

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                                    <span aria-hidden="true">&times;</span></button>

                                            <h4 class="modal-title">Edit Location</h4>

                            </div>

                            <div class="modal-body">

                                <div class="row">
                                    <asp:ScriptManager ID="ScriptManager1" runat="Server"></asp:ScriptManager>
                                    <div class="col-md-6">
                                        <label>Company Name *</label>
                                        <asp:DropDownList id="ddlEditCompany"  class="form-control" runat="server"></asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added"  ControlToValidate="ddlAddCompanyName" runat="server" ErrorMessage="Select the Company Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    </div>
                                   <div class="col-md-6">
                                     <label>Enter Location Code *</label>
                                        <asp:TextBox class="form-control" ID="txtEditLocationFadvID" runat="server"></asp:TextBox>
                                        <input type="hidden" class="form-control" id="hiddenLocationID" name="hiddenLocationID" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="edited"  ControlToValidate="txtEditLocationFadvID" runat="server" ErrorMessage="Please Enter Location Code" ForeColor="Red"></asp:RequiredFieldValidator>
                                  
                                    </div>
                                    <div class="col-md-6">
                                        <label>Enter Location Name *</label>
                                        <asp:TextBox class="form-control" ID="txtEditLocationName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="edited"  ControlToValidate="txtEditLocationName" runat="server" ErrorMessage="Please Enter Location Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    </div>

                                <div class="modal-footer">
                                      <asp:Button ID="btnUpdate" runat="server" Text="Save Changes" class="btn btn-primary" ValidationGroup="edited" OnClick="btnUpdate_Click"/>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                  
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

                                <h4 class="modal-title">Add Location</h4>

                            </div>

                            <div class="modal-body">

                                <div class="row">
                                    
                                 <div class="col-md-6">
                                        <label>Company Name *</label>
                                        <asp:DropDownList id="ddlAddCompanyName"  class="form-control" runat="server"></asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="added"  ControlToValidate="ddlAddCompanyName" runat="server" ErrorMessage="Please Enter Company Namee" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    </div>
                                 <div class="col-md-6">
                                        <label>Enter Location Code *</label>
                                        <asp:TextBox class="form-control" ID="txtAddFadvLocationID" runat="server"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="added"  ControlToValidate="txtAddFadvLocationID" runat="server" ErrorMessage="Please Enter Location Code" ForeColor="Red"></asp:RequiredFieldValidator>
                                 
                                    </div>
                                    <div class="col-md-6">
                                        <label>Enter Location Name *</label>
                                        <asp:TextBox class="form-control" ID="txtAddLocation" runat="server"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="added"  ControlToValidate="txtAddLocation" runat="server" ErrorMessage="Please Enter Location Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="modal-footer">
                                        <%-- <button type="button" class="btn btn-default" id="close" runat="server" data-dismiss="modal">Close</button>--%>
                                          <asp:Button ID="BtnCreateuser" runat="server" Text="Save" OnClick="BtnCreateuser_Click" ValidationGroup="added" class="btn btn-primary" />
                                         <asp:Button ID="close" runat="server" Text="Close" class="btn btn-default" />
                                     
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
                $("[id*=hiddenLocationID]").val($.trim($(".classLocationID", $(this).closest("tr")).html()));
                $("[id*=txtEditLocationFadvID]").val($.trim($(".classFadv_LocationID", $(this).closest("tr")).html()));
                $("[id*=txtEditLocationName]").val($.trim($(".classLocationName", $(this).closest("tr")).html()));
                $("[id*=ddlEditCompany]").val($.trim($(".claCompanyID", $(this).closest("tr")).html()));
               });
        });
    </script>
</asp:Content>
