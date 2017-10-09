<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" CodeBehind="CreateAuditQusRule.aspx.cs" Inherits="PresentationLayer.Admin.CreateAuditQusRule" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
   <%-- <script type="text/javascript">
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
    <script type="text/javascript">
    
        function valid() {
           var v_type = document.getElementById("<%=txtAuditQuestion.ClientID%>").value;
            var customer_val = document.getElementById("<%=ddlAddCompany.ClientID%>").value;
             var document_val = document.getElementById("<%=ddlAddDocumentType.ClientID%>").value;
           //var sales = document.getElementById("editor1").value;
            if (v_type == "" || customer_val == "" || document_val == "" || customer_val == "Select Customer" || customer_val == "0" || document_val == "Select Document Type" || document_val == "0")
           {
               alert("Fill all information");
               return false;
           }
           else {
              
                   return true;              
 
           }        
        }

        function validedit() {
           var v_type = document.getElementById("<%=txtEditAuditQuestion.ClientID%>").value;
           var customer_val = document.getElementById("<%=ddlEditSelectCompany.ClientID%>").value;
             var document_val = document.getElementById("<%=ddlEditDocumentType.ClientID%>").value;
          
           //var sales = document.getElementById("editor1").value;
            if (v_type == "" || customer_val == "" || document_val == "" || customer_val == "Select Customer" || document_val == "Select Document Type" || customer_val == "0" || document_val == "0")
           {
               alert("Fill all information");
               return false;
           }
           else {
              
                   return true;              
 
           }        
        }



</script>

    <style>
        .template {
            height: 400px;
            overflow-y: scroll;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
   
  
            <asp:Panel ID="pnlMainBody" runat="server">
                <div class="box-header">
                    <div class="row" align="right">
                        <center>                                  <div style="align-content:center">
                                 <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Text="Added Successfully." runat="server" Visible="false" />
                                 <asp:Label ID="lblMessageError" ForeColor="Red" Font-Bold="true" Text="Already Exist!!" runat="server" Visible="false" />
                                    <asp:Label ID="lblMessageErrorDelete" ForeColor="Red" Font-Bold="true" Text="Deleted Successfully." runat="server" Visible="false" />
                            </div>
            </center>

                        <asp:LinkButton ID="btnadd"  runat="server" ToolTip="Add New Control" OnClick="btnadd_Click" >

                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/Add.png" Width="20" Height="20" />
                        </asp:LinkButton>
                        </div>
                    </div>

                <div class="template">

                    <table id="Table1" style="margin-bottom: 2px" class="table  table-bordered table-striped display">

                        <thead>
                            <tr style="background-color: #F6A41C">
                                <th class="col" style="width: 57px 53px; color: white; text-align: center">S.No</th>

                                <%--<th class="col" style="width: 150px; color: white; text-align: center">Document Type Name</th>--%>
                                <th class="col" style="width: 150px; color: white; text-align: center">Audit Question</th>
                                <th class="col" style="width: 150px; color: white; text-align: center">Dynamic Control ID</th>
                                <th class="col" style="width: 150px; color: white; text-align: center">Label Name</th>
                                <th class="col" style="width: 100px; color: white; text-align: center">Rule Expression</th>
                                <th class="col" style="width: 100px; color: white; text-align: center">RuleType</th>
                                <th class="col" style="color: white; text-align: center">Actions</th>

                            </tr>
                        </thead>

                        <tbody>

                            <asp:Repeater ID="Reptrule" runat="server">
                                <ItemTemplate>
                                    <tr id="trID" runat="server">
                                        <td id="serial" style="width: 57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                        <td class="hiddenDynamicControlID" ><%# DataBinder.Eval(Container.DataItem, "AuditQuestion")%>  <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("AuditRuleClassification")%>' /></td>
                                        <td id="DocumentTypeName" class="DynContrId" style="width: 150px"><%# DataBinder.Eval(Container.DataItem, "DynamicControlID")%></td>
                                        <td id="CompanyName" class="labelname" style="width:150px"><%# DataBinder.Eval(Container.DataItem, "labelname")%></td>
                                        <td id="UserName" style="width:150px" class="RuleExpression"><%# DataBinder.Eval(Container.DataItem, "RuleExpression")%></td>
                                        <td id="CustomerID" style="width:100px;" class="RuleType"><%# DataBinder.Eval(Container.DataItem, "RuleType")%></td>
                                      
                                        <td id="actions" style="width: 150px">

                                            <%--<asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ToolTip="Edit" class="editID imgEdit btn-sm" data-toggle="modal" data-target="#myModal">
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>--%>

                                            <%--<asp:LinkButton ID="btn_Edit" runat="server" OnClick="btn_Edit_Click" ToolTip="Edit" >
                                               <img src="Images/edit.png" />
                                            </asp:LinkButton>--%>
                                            
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><img src="Images/edit.png" /></asp:LinkButton>
                                            
                                           <%-- <asp:ImageButton ID="OnEdit" runat="server" ImageUrl="Images/edit.png" Height="18px" OnClick="btnEdit_Click"  />--%>



                                            <asp:ImageButton ID="OnDelete" runat="server" ImageUrl="Images/Delete.png" Height="18px" OnClick="OnDelete_Click"  OnClientClick="return confirm('Do you want to Delete data?')" />

                                           
                                        </td>

                                    </tr>


                                </ItemTemplate>

                            </asp:Repeater>


                        </tbody>

                    </table>
                    <div id="bottom_anchor"></div>
                </div>
                
                        </asp:Panel>
                        </asp:Content>
