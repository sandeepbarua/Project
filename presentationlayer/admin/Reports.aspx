<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="Reports.aspx.cs" Inherits="PresentationLayer.Admin.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
         
     <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }
    </style>

     <link href="script/Scripts/style.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
                      <div id="maindiv" style="display: block; background-color: #ECEFF1;" runat="server">
        
            <asp:Panel ID="pnlMainBody" runat="server">
                <div class="box-header">
                    <div class="row" align="right">
                         <Center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </Center>   
                        
                    </div>
                </div>
                <!-- /.box-header -->
                 <div class="main-panel">
       
           
<div class="template">
        
        <table id="Table1" style="margin-bottom:2px" class="table  table-bordered table-striped display">
          
                      <tr>

                          <td>

    <div class="row">
        <div class="col-md-4 ">
          <div class="box box-solid feature">
            
            <!-- /.box-header -->
            <div class="box-body">
              <a href="LoginAudit.aspx"> <i class="glyphicon glyphicon-th"></i>
            <h3>Login Audit</h3>
            <div class="title_border"></div>
            <p> Click this box to Check Login Details </p></a>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
        <!-- ./col -->
        <div class="col-md-4">
          <div class="box box-solid feature">
            
            <!-- /.box-header -->
            <div class="box-body">
             <a href="Report_RobotProcess.aspx">  <i class="glyphicon glyphicon-file"></i>
            <h3>Robot Details</h3>
            <div class="title_border"></div>
            <p>Click this box to Check Robot Details</p></a>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
        <!-- ./col -->
          <div class="col-md-4">
          <div class="box box-solid feature">
            
            <!-- /.box-header -->
            <div class="box-body">
             <a href="Roboactivityshow.aspx">  <i class="glyphicon glyphicon-file"></i>
            <h3>Data pull report from CP Screen</h3>
            <div class="title_border"></div>
            <p>Click this box to Check Robot Details</p></a>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
        <!-- ./col -->
      </div>
                              <div class="row">
                              <div class="col-md-4">
          <div class="box box-solid feature">
            
            <!-- /.box-header -->
            <div class="box-body">
             <a href="UserLoginDetail.aspx">  <i class="glyphicon glyphicon-file"></i>
            <h3>Login Event</h3>
            <div class="title_border"></div>
            <p>Click this box to Check Login Event</p></a>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
                                  <div class="col-md-4">
          <div class="box box-solid feature">
            
            <!-- /.box-header -->
            <div class="box-body">
             <a href="CPScreenData.aspx">  <i class="glyphicon glyphicon-file"></i>
            <h3>CP Screen Data</h3>
            <div class="title_border"></div>
            <p>Click this box to CP Screen Data</p></a>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
                                   </div>
        <!-- ./col -->





                          </td>

                      </tr>
              
                    </table>
            <div id="bottom_anchor"></div>
                </div>
        
    </div>
               
            </asp:Panel>
        
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
