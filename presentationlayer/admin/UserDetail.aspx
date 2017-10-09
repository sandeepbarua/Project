<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="PresentationLayer.Admin.UserDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   
   
   
    <asp:Panel ID="pnlMainBody" runat="server">
                        <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">MANAGE USERS</h4>
                     
                        <asp:LinkButton ID="btnadd" runat="server" ToolTip="Add User" class="mq-btn btn btn-primary nxt_btn" Text="ADD USER" OnClick="btnadd_Click">
                           
                          
                        </asp:LinkButton>
                        
            </div>

                
                <!-- /.box-header -->
                 <div class="main-panel">
                             <Center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                            
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </Center>   
                                              <%-- </div>
                </div>--%>
                <!-- /.box-header -->
                
<%--<div class="template">--%>
                    
        <table id="test" class="table table-striped table-bordered">
          
                        <thead>
                            <tr>
                                <th>S.No</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                 <th>Email</th>
                                <th>Role</th>
                                <th>Status</th>
                             <th style="width:8%">Registration Date</th>
                                <th style="width:17%">Actions</th>
                                
                                
                            </tr>
                        </thead>
           
                     <tbody>
                           
                           <asp:Repeater ID="ReptUse" runat="server" OnItemDataBound="ReptUse_ItemDataBound">
                                <ItemTemplate>
                                    <tr id = "trID" runat="server">
                                        <td id="serial"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                    <%-- <td class="ddlEditEmployeeMasterID" style="display:none"   ><%# DataBinder.Eval(Container.DataItem, "UserDetailsID")%></td>--%>
                                       <%-- <td id="UserFirstNameEdit" style=" display:none" class="classUserFirstName"><%# DataBinder.Eval(Container.DataItem, "UserFirstName")%></td>--%>
                                         <td  id="UserFirstName"  class="txtUserId"><%# DataBinder.Eval(Container.DataItem, "UserFirstName")%></td>
                                        <td id="UserLastName"  class="classUserLastName"><%# DataBinder.Eval(Container.DataItem, "UserLastName")%></td>
                                        <td id="email" class="classEmail" ><%# DataBinder.Eval(Container.DataItem, "EmailId")%></td>
                                      <%--  <td id="FADVUserID" style="width:100px" class="classFADVUserID"><%# DataBinder.Eval(Container.DataItem, "FADVUserID")%></td>--%>
                                         <td id="RoleName" class="classRoleName"><%# DataBinder.Eval(Container.DataItem, "RoleName")%></td>
                                         <td class="Flagsa" > <asp:Label ID="la" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LocktheAccount")%>'> </asp:Label></td>
                                      
                                       <%-- <td class="classRoleId" style="display:none"><%# DataBinder.Eval(Container.DataItem, "RoleId")%></td>
                                        <td id="LoginAttempt" style="display:none" class="classLoginAttempt" ><%# DataBinder.Eval(Container.DataItem, "LoginAttempt")%>
                                            
                                        </td>--%>
                                        <td id="DateOfCreation" class="classLoginAttempt" style="width:8%"><%# DataBinder.Eval(Container.DataItem, "DateOfCreation")%>
                                            <asp:HiddenField ID="hndUserID" runat="server" Value='<%#Eval("LocktheAccount")%>'/>
                                        </td>
                                         
                                        
                                        <td id="actions" style="width:17%">

                                               <asp:ImageButton ID="onLocked" runat="server"  Height="18px" OnClick="onLocked_Click"  OnClientClick = "return confirm('Do you want to Perform this Action?')" />                                            
                                        <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID btn btn-outline-info" data-toggle="modal" data-target="#myModal">
                                              <i class="fa fa-pencil"></i>                                            </asp:LinkButton>
                                      
                                      <asp:LinkButton ID="OnDelete" runat="server" class="btn  btn-outline-danger" ToolTip="Delete" OnClick="OnDelete_Click"  OnClientClick = "return confirm('Do you want to Delete data?')"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                  
                                             <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("UserDetailsID")%>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("UserFirstName")%>' />
                                            <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("UserLastName")%>' />
                                            <asp:HiddenField ID="HiddenField4" runat="server" Value='<%#Eval("EmailId")%>' />
                                            <asp:HiddenField ID="HiddenField5" runat="server" Value='<%#Eval("RoleName")%>' />

                                      <a href="/Admin/UserLoginDetail.aspx?id=<%# DataBinder.Eval(Container.DataItem,"UserDetailsID")%>" title="View" class="btn  btn-outline-secondary"> <i class="fa fa-bars"></i> </a>
                                                  </td>
                                      
                                        </tr>
                                        
                                         
                                </ItemTemplate>
                               
                            </asp:Repeater>
                                                                

                        </tbody>
              
                    </table>
                        
                             <asp:ScriptManager ID="ScriptManager2" runat="server" />
                
                <%--Edit Account--%>

            
        <%--        <div class="modal fade" id="myModal">
                    <div class="modal-dialog">

                        <div class="modal-content">

                            <div class="modal-header bg-aqua-active">

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                                    <span aria-hidden="true">&times;</span></button>

                                            <h4 class="modal-title">Edit Users <center><asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" /></center>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                                <h4></h4>
                                                
                                </h4>

                            </div>

                           
                        </div>
                    </div>
                </div>--%>

                     </div>
            </asp:Panel>
           
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnadd"
            PopupControlID="Panel1" OkControlID="close" />
 
    <script>

        $(".modal").on("hidden.bs.modal", function () {
            $(".modal-body1").html("");
        });

        $(function () {
            $(".editID").click(function () {
                $("[id*=hfUserId]").val($.trim($(".ddlEditEmployeeMasterID", $(this).closest("tr")).html()));
                $("[id*=txtEditFirstName]").val($.trim($(".classUserFirstName", $(this).closest("tr")).html()));
                $("[id*=txtEditLastName]").val($.trim($(".classUserLastName", $(this).closest("tr")).html()));
                $("[id*=txtEditFadvID]").val($.trim($(".classFADVUserID", $(this).closest("tr")).html()));
                $("[id*=ddlEditRole]").val($.trim($(".classRoleId", $(this).closest("tr")).html()));
                $("[id*=txtEditLoginAttempt]").val($.trim($(".classLoginAttempt", $(this).closest("tr")).html()));
                $("[id*=txtEditEmail]").val($.trim($(".classEmail", $(this).closest("tr")).html()));
              

            });
        });

    </script>
    <script>
        $(document).ready(function () {
            $('#myModelAdd').bootstrapValidator({
                container: '#messages',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    fullName: {
                        validators: {
                            notEmpty: {
                                message: 'The full name is required and cannot be empty'
                            }
                        }
                    },
                    email: {
                        validators: {
                            notEmpty: {
                                message: 'The email address is required and cannot be empty'
                            },
                            emailAddress: {
                                message: 'The email address is not valid'
                            }
                        }
                    },
                    title: {
                        validators: {
                            notEmpty: {
                                message: 'The title is required and cannot be empty'
                            },
                            stringLength: {
                                max: 100,
                                message: 'The title must be less than 100 characters long'
                            }
                        }
                    },
                    content: {
                        validators: {
                            notEmpty: {
                                message: 'The content is required and cannot be empty'
                            },
                            stringLength: {
                                max: 500,
                                message: 'The content must be less than 500 characters long'
                            }
                        }
                    }
                }
            });
        });
    </script>
      <script type="text/javascript">
    function openModal() {
        $('#myModal').modal('show');
    }
</script>

    <script type="text/javascript">
        function focusRed() {
        }
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
                 //  document.getElementById("=lblMessageError.ClientID %>").style.display = "none";
        }, seconds * 1000);
             };
         function HideLabelErrorDelete() {
             var seconds = 5;
             setTimeout(function () {
                 document.getElementById("<%=lblMessageErrorDelete.ClientID %>").style.display = "none";
                 }, seconds * 1000);
             };
      </script>

   <script>
       $('#test').DataTable({
            'columnDefs': [{ 'orderable': false, 'targets': 4}], // hide sort icon on header of first column
            'aaSorting': [[0, 'asc']] // start to sort data in second column
        });
        $(".textarea").wysihtml5
 </script>
    
    <script>  
            $(document).ready(function() {  
                $('#test').dataTable();
            });  
        </script>  

        
</asp:Content>
