<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin2.Master" CodeBehind="CopyCompany.aspx.cs" Inherits="PresentationLayer.Admin.CopyCompany" %>

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
                <h4 class="modal-title">CONFIGURE CLIENT</h4>
            </div>

            <div class="White_box ">
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
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Location</label>
                            </div>
                            <div class="col-lg-5">
                                <input type="file" id="selectedFile" style="display: none;" onchange="document.getElementById('txtLocation').value = this.value" />
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>




        <div class="modal-body" id="dvDropDown">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table  table-bordered table-striped display">
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
                </Columns>
            </asp:GridView>


        </div>

        <div align="right" style="padding-right: 10px">
            <asp:Button Width="100px" ID="BtnSubmit" runat="server" Text="Submit" ValidationGroup="abc" OnClick="BtnSubmit_Click" class="mq-btn btn btn-primary nxt_btn" />&nbsp;&nbsp;&nbsp; 
               <asp:Button ID="btnReset" Width="100px" ValidationGroup="EditValida" runat="server" Text="Reset" class="mq-btn btn btn-primary nxt_btn" OnClick="btnReset_Click" />


        </div>

    </asp:Panel>


</asp:Content>
