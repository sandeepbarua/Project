<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="Report_RobotProcess.aspx.cs" Inherits="PresentationLayer.Admin.Report_RobotProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

         .btn-facebook {
 color: #fff;
 background-color: #263238;
 border-color: rgba(0,0,0,0.2);
 margin-top:20px;

}
    </style>
    

   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
         
   
    <div id="maindiv" style="display: block; background-color: #ECEFF1;" runat="server">
        <div class="box box-primary">
            <asp:Panel ID="pnlMainBody" runat="server">
                <div class="box-header">
                    <div class="row" align="right">
                        <center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </center>
                        
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="main-panel">
                    <div class="modal-body">

                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label1" runat="server" Visible="false" Text="Label"></asp:Label>
                               
                                  <div class="form-group"> 
            <table width="100%">
            <tr>
            
             <td>
             <div class="col-md-8">
             <label for="exampleInputFile">From Date</label>
               <div class="input-group date">
                  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:TextBox ID="txtStartTime" runat="server" class="form-control pull-right" Width="200px" ></asp:TextBox>
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
    <asp:ImageButton ID="ImageButton1" ImageUrl="Images/calendar.png" ImageAlign="right"
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
                                
                               
                            </div>
                        </div>
                   
                    </div>
                </div>



                <div class="template">

                  <div class="box-body">
   <table id="test1" class="table table-bordered table-striped">
                <thead>
                <tr>
                     <th>Activity</th>
                     <th>Total NO Of Pages</th>
                  <th>Robo Start Time</th>
                  <th>Robo End Time</th>
                  <th>Comments</th>
                    <th>Labelling</th>

                </tr>
                </thead>
                 <tbody>
                     <asp:Repeater ID="rptUser" runat="server">
                     <ItemTemplate>
                     <tr>
                 <td> <%# DataBinder.Eval(Container.DataItem, "Activity")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "TotalNumberOfPages")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "StartTime")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "EndTime")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "Comment")%></td> 
                 <td> <%# DataBinder.Eval(Container.DataItem, "Labelling")%></td> 
                
                </tr>
                     </ItemTemplate>
                     </asp:Repeater>
            
                 </tbody>
                 </table>
                 </div>
                </div>
       
       


        <%--create new Account--%>

         </asp:Panel>
    </div>
   </div>

    <script>
        $(function () {
            $(".editID").click(function () {
                $("[id*=hiddenDynamicControlID]").val($.trim($(".classDynamicControlID", $(this).closest("tr")).html()));
                $("[id*=txtEditAuditQuestion]").val($.trim($(".classAuditQuestion", $(this).closest("tr")).html()));
                $("[id*=ddlEditDocumentType]").val($.trim($(".classDocumentTypeID", $(this).closest("tr")).html()));
                $("[id*=ddlEditSelectCompany]").val($.trim($(".classCustomerID", $(this).closest("tr")).html()));
                });
        });
    </script>


</asp:Content>
