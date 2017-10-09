<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master"  CodeBehind="LoginAudit.aspx.cs" Inherits="PresentationLayer.Admin.LoginAudit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
         
     <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }
        .btn-facebook {
 color: #fff;
 background-color: #263238;
 border-color: rgba(0,0,0,0.2);
  margin-top:20px;
}

        .btn-social.btn-sm {
 padding-left: 38px
}
    </style>
   
    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
                  
    <div id="maindiv" style="display: block; background-color: #ECEFF1;" runat="server">
          <div class="box box-primary	">
    <div class="box-header">
   <div class="row">
         <div class="col-lg-6"> 
            <%--<h3 class="box-title" style="color:#FF5722 ;"><i class="fa fa-tag"></i> Login Audit</h3>--%>

            <div class="form-group"> 
            <table width="100%">
            <tr>
            <td>
             <div class="col-md-8">
             <label for="exampleInputFile">From Date</label>
               <div class="input-group date">
                  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:TextBox ID="txtStartTime" runat="server" class="form-control pull-right" Width="200px"></asp:TextBox>
    <asp:ImageButton ID="imgPopup" ImageUrl="Images/calendar.png" ImageAlign="TextTop"
        runat="server" />
    <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtStartTime"
        Format="MM/dd/yyyy">
    </cc1:CalendarExtender>
                 
                  </div>
                </div>

            </td>
            <td>
              <div class="col-md-8">
                  <label for="exampleInputFile">To Date</label>
                 <div class="input-group date">
                 
    <asp:TextBox ID="txtEndTime" runat="server" class="form-control pull-right" Width="200px" ></asp:TextBox>
    <asp:ImageButton ID="ImageButton1" ImageUrl="Images/calendar.png" ImageAlign="TextTop"
        runat="server" />
    <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtEndTime"
        Format="MM/dd/yyyy">
    </cc1:CalendarExtender>
                  
                 
                </div>
                  
                </div>
            </td>
            <td>
             <div class="col-md-6">
                <a id="A1" href="#" type="button"  runat="server"  class="btn btn-facebook btn-sm" OnServerClick="btnGetData_Click"  ><%--OnServerClick="btnGetData_Click"--%>
               <i></i> Search</a>
               </div>
            </td>
            <td>
            <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Export to Excel" 
                    class="btn imgExportToExcel btn-sm" /><%--onclick="LinkButton1_Click"--%>
            </td>
            </tr>
            </table>
           
               </div>
                <div></div>
                
              </div>

         </div> 
         

         </div>
   <div class="box-body">
   <table id="test1" class="table table-bordered table-striped">
                <thead>
                <tr>
                  <th>User First Name</th>
                  <th>User Last Name</th>
                  <th>Email ID</th>
                  <th>Date Of Login</th>
                  <th>Date Of LogOut</th>

                </tr>
                </thead>
                 <tbody>
                     <asp:Repeater ID="rptUser" runat="server">
                     <ItemTemplate>
                     <tr>
                 <td> <%# DataBinder.Eval(Container.DataItem, "UserFirstName")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "UserLastName")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "EmailId")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "DateOfLogin")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "DateOfLogOut")%></td> 
                
                </tr>
                     </ItemTemplate>
                     </asp:Repeater>
            
                 </tbody>
                 </table>
                 </div>
    </div>
    </div>
   
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

</asp:Content>
