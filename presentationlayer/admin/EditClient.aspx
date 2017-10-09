<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin2.Master" CodeBehind="EditClient.aspx.cs" Inherits="PresentationLayer.Admin.EditClient" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 15%;
        }

        .auto-style5 {
            width: 292px;
        }

        .auto-style6 {
            width: 320px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=chkHeader]").bind("click", function () {
                var chkHeader = $(this);

                //Find and reference the GridView.
                var grid = $(this).closest("table");

                //Loop through the CheckBoxes in each Row.
                $("td", grid).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.
                    if (chkHeader.is(":checked")) {
                        $(this).attr("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");


                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).attr("disabled", "disabled");


                    }
                });

            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkRow]").bind("click", function () {

                //Find and reference the GridView.
                var grid = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var chkHeader = $("[id*=chkHeader]", grid);

                //If the CheckBox is Checked then enable the TextBoxes in thr Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                } else {
                    chkHeader.removeAttr("checked");
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=rfvPrice]").each(function () {
                ValidatorEnable(this, false);
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var val = $("[id*=rfvPrice]", $(this).closest("tr"))[0];
            ValidatorEnable(val, $(this).is(":checked"));
        });

    </script>
    <script type="text/javascript">
        $('input[type=file]').change(function () {
            document.getElementById('txtLocation').value = $(this).value;

        })
    </script>
    <style>
        input[type="text"] {
            border: 1px solid #ccc;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            height: 30px;
            width: 250px;
        }
    </style>
    <asp:Panel ID="pnlMainBody" runat="server">

        <!-- /.box-header -->
        <div class="main-panel">
            <div class="modal-header bg-aqua-active">
                <h4 class="modal-title">EDIT CLIENT</h4>
            </div>

            <div class="White_box" style="padding-bottom=0px">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Client Name</label>
                            </div>
                            <div class="col-lg-5">
                                <asp:TextBox ID="txtCustomerName" runat="server" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPrice" ControlToValidate="txtCustomerName" runat="server" ErrorMessage="*Customer name required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Client ID</label>
                            </div>
                            <div class="col-lg-5">
                                <asp:TextBox ID="txtCustomerID" runat="server" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomerID" runat="server" ErrorMessage="*Company ID required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        </div>
                </div>
            </div>
        </div>
        <div class="White_box" style="padding-top:0px">
                <div class="row">
                    <div class="col-lg-9">
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Location</label>
                            </div>
                            <div class="col-lg-3" align="left">
                             
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                            <div class="col-lg-3" align="right">
                              <asp:Button ID="BtnDownload" runat="server" Text="Download"  OnClick="BtnDownload_Click" class="mq-btn btn btn-primary nxt_btn"/>
                            </div>
                           
                        </div>
                          <div class="row">
                            <div class="col-lg-3">
                              
                            </div>
                            <div class="col-lg-5">
                                
                            </div>
                           
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <label></label>
                            </div>
                             <div class="col-lg-5" align="left">
                                  <asp:Button Width="100px" ID="BtnSubmit" runat="server" Text="Submit" ValidationGroup="abc" OnClick="BtnSubmit_Click" class="mq-btn btn btn-primary nxt_btn" />&nbsp;&nbsp;&nbsp; 
               <asp:Button ID="btnCancel" Width="100px" ValidationGroup="EditValida" runat="server" Text="Cancel" class="mq-btn btn btn-primary nxt_btn" OnClick="btnCancel_Click" />
                                 </div>                           

                    </div>
            </div>
        </div>

            </div>
        <asp:HiddenField ID="CustomerID" runat="server"/>
         <table id="Table1" style="margin-bottom: 2px; margin-right:10px" align="center" class="table  table-bordered table-striped display">

                <thead>
                    <tr>
                        <th style="width:10px">S.No</th>
                        <th style="width:10px">Document Code</th>
                        <th style= "text-align: center; width:25%">Document</th>                       
                        <th style=" text-align: center">Document Description</th>
                        <th style=" text-align: center">Source Type</th>
                        <th style="width:12%; text-align: center">Created Date</th>
                        <th style="width:12%; text-align: center">Modified Date</th>
                        <%--<th style=" text-align: center">Added By</th>--%>
                        <th  style="width:14%; text-align: center">Actions</th>
                    </tr>
                </thead>

                <tbody>

                    <asp:Repeater ID="ReptUse" runat="server" OnItemDataBound="ReptUse_ItemDataBound">
                        <ItemTemplate>
                            <tr id="trID" runat="server">
                                <td id="serial" ><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                <td class="classDocumentTypeID" style="width:10%"><%# DataBinder.Eval(Container.DataItem, "DocumentCode")%></td>
                                <td id="TemplateName"  class="classDocumentName" style="width:25%"><%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%></td>
                                <td id="DocumentDescription"  class="classDocumentDescription"><%# DataBinder.Eval(Container.DataItem, "DisplayDescription")%></td>
                                <td id="SourceType"  class="classDocumentDescription"><%# DataBinder.Eval(Container.DataItem, "SourceType")%></td>
                                <td id="CustomersID" style="width:12%"><%# DataBinder.Eval(Container.DataItem, "DateDisplayCreation")%></td>
                                <td id="CustomerID" style="width:12%"><%# DataBinder.Eval(Container.DataItem, "DateDisplayModify")%></td>
                                <%--<td id="UserName" class="classUserName"><%# DataBinder.Eval(Container.DataItem, "UserName")%></td>--%>

                                <%--<td class="classUserDetailsID" style="display: none"><%# DataBinder.Eval(Container.DataItem, "UserID")%></td>--%>

                                <td id="actions" style="width: 14%">
                                    <asp:LinkButton ID="lbAddDataPoint" runat="server" ToolTip="Customize Document" OnClick="lbAddDataPoint_Click" class="btn  btn-outline-dark">
                                         <i class="fa fa-id-card-o"></i>                                     
                                    </asp:LinkButton>
                                  <%--  <asp:LinkButton ID="lbView" runat="server" ToolTip="View" OnClick="lbDocument_Click" class="btn  btn-outline-secondary">
                                   <i class="fa fa-bars"></i>  
                                    </asp:LinkButton>--%>
                                 
                                    <asp:LinkButton ID="btnCustomizeRule" runat="server" OnClick="btnCustomizeRule_Click" ToolTip="Customize Rule" class="editID btn btn-outline-info" data-toggle="modal" data-target="#myModal">
                                             <i class="fa fa-id-card-o"></i> 
                                    </asp:LinkButton>
                                  <%--  <asp:ImageButton ID="OnDelete" class="btn-outline-danger" runat="server" ImageUrl="../Admin/Images/Delete.png" Height="18px" OnClick="OnDelete_Click" OnClientClick="return confirm('Do you want to Delete data?')" />--%>
                                    
                                     <asp:LinkButton ID="OnDelete" runat="server" class="btn  btn-outline-danger" ToolTip="Delete" OnClick="OnDelete_Click"  OnClientClick = "return confirm('Do you want to Delete data?')"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                     
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("DocumentTypeID")%>' />
                                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("DocumentTypeName")%>' />
                                    <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("DocumentDescription")%>' />


                                </td>

                            </tr>


                        </ItemTemplate>

                    </asp:Repeater>


                </tbody>

            </table>



       <%-- <div class="modal-body" id="dvDropDown">
            <asp:GridView ID="EditClientGrid" runat="server" AutoGenerateColumns="false" class="table  table-bordered table-striped display" onrowcommand="EditClientGrid_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="Black">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkHeader" runat="server" Text="Template" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRow" runat="server" Text='<% # Eval("DocumentTypeAlias") %>' />
                            <asp:HiddenField ID="DocID" runat="server" Value='<%#Eval("DocumentTypeID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Document Name" HeaderStyle-ForeColor="Black">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDocumentName" runat="server" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Document Code" HeaderStyle-ForeColor="Black">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDocumentCode" runat="server" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Source Type" HeaderStyle-ForeColor="Black">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlSourceType" runat="server" Width="250px" class="form-control">
                                <asp:ListItem Value="NA">--Select Source---</asp:ListItem>
                                <asp:ListItem>FaxID</asp:ListItem>
                                <asp:ListItem>Email</asp:ListItem>
                                <asp:ListItem>PDF</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Data Point" HeaderStyle-ForeColor="Black">
                        <ItemTemplate>
                            <%--<asp:Button Width="100px" ID="BtnDataPoint" runat="server" Text="Customize Data Point"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %> " CommandName="DataPoint"  class="mq-btn btn btn-primary nxt_btn" />&nbsp;&nbsp;&nbsp;
                             <asp:Button Width="100px" ID="BtnAuditRule" runat="server" Text="Costomize Audit Rule" CommandArgument="<%# ((GridViewRow) Container).RowIndex %> " CommandName="AuditRule"  class="mq-btn btn btn-primary nxt_btn" />
                            <asp:LinkButton ID="BtnDataPoint" runat="server"  ToolTip="Customize Data Point" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DataPoint" class="btn btn-outline-info">
                                               <i class="fa fa-pencil"></i>                           
                                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Audit Rule" HeaderStyle-ForeColor="Black">
                        <ItemTemplate>                           
                            <asp:LinkButton ID="BtnAuditRule" runat="server" ToolTip="Customize Audit Rule"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AuditRule" class="btn btn-outline-info">
                                               <i class="fa fa-pencil"></i>                           
                                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </div>--%>

        <div align="right" style="padding-right: 10px">
           


        </div>

    </asp:Panel>


</asp:Content>
