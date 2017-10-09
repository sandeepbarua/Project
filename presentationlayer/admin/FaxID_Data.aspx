<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FaxID_Data.aspx.cs" Inherits="PresentationLayer.Admin.FaxID_Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .template {
            height: 450px;
            overflow-y: scroll;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
       <div class="main-panel">
       <%-- <div class="template">--%>
            <table>
                 <tr>
            <td>
                <asp:LinkButton ID="lbtnBack" class="btn btn-success btn-sm" Text="Back" runat="server" OnClick="lbtnBack_Click"></asp:LinkButton>
            </td>
        </tr>
       
            </table><br />
            <table width="100%">
                <tr>
                   
                    <td width="50%">
                       &nbsp;&nbsp;<b > User Name:-</b>  <asp:Label ID="txtusername1" runat="server" Text="Label"></asp:Label>
                    </td>
                     <td width="50%">
                       <b > User Name:-</b> <asp:Label ID="txtusername2" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>

            </table>
            <table border="0" width="100%">
       
        <tr>
            <td>

 <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <HeaderTemplate>
                <table width="100%" style="margin-bottom:2px" class="table  table-bordered table-striped display">
                    <%--<tr>
                        <td colspan="2" align="center" class="title">
                            Student's Report
                        </td>
                    </tr>--%>
            </HeaderTemplate>
            <ItemTemplate>
                
                <tr>
                    <td colspan="2"><br /><br />
                      &nbsp;&nbsp;  <b> Document Type:</b>

                         <%# DataBinder.Eval(Container.DataItem, "DocumentTypeName")%>
                       <asp:HiddenField ID="hdndocid" runat="server" Value='<%#Eval("DocumentTypeID")%>'/>                       <%-- <asp:TextBox ID="txtdoctype" Width="400px" Enabled="false" Text='<%#Eval("DocumentTypeName") %>' runat="server"
                            ></asp:TextBox>--%>
                        <br /><br />
                    </td>
                   
                </tr>
            <tr>
                <td>
<table width="100%" style="margin-bottom:2px" class="table  table-bordered table-striped display">

    <tr>
                    <td colspan="2" width="50%">
                        <table id="test1" class="table table-bordered table-striped" >
                <thead>
                <tr style="background-color:#F6A41C">
                 
                <th width="25%">Data Field</th>
                 <th width="25%">Value</th>
                </tr>
                </thead>
                 <tbody>
                        <asp:Repeater ID="innerRepeater" runat="server" OnItemDataBound="innerRepeater_ItemDataBound">
                            <HeaderTemplate>
                                <table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                       <td width="25%">
                                            <asp:Label ID="DynamicControlID" runat="server" Visible="false" Text='<%#Eval("DynamicControlID") %>'></asp:Label>
                                           <asp:Label ID="CPScreenDataID" runat="server" Visible="false" Text='<%#Eval("CPScreenDataID") %>'></asp:Label>
                                            <asp:Label ID="CountForFaxId" runat="server" Visible="false" Text='<%#Eval("CountForFaxId") %>'></asp:Label>
                                        <%# DataBinder.Eval(Container.DataItem, "labelName")%>
                                    </td>
                                
                                     <td width="25%">
                                       <asp:TextBox ID="txtvalue" class="form-control" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DynamicControlValueText") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                </ItemTemplate>
                          
                        </asp:Repeater>
                     </tbody>
                            </table>
                    </td>
                    <td>
                         <table id="test2" class="table table-bordered table-striped" >
                <thead>
                <tr style="background-color:#F6A41C">
                 
                <th width="25%">Data Field</th>
                 <th width="25%">Entries</th>
                </tr>
                </thead>
                 <tbody>
                          <asp:Repeater ID="innerRepeater2" runat="server" OnItemDataBound="innerRepeater2_ItemDataBound">
                            <HeaderTemplate>
                                <table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    
                                    <td width="25%">
                                         <asp:Label ID="CPScreenDataID" runat="server" Visible="false" Text='<%#Eval("CPScreenDataID") %>'></asp:Label>
                                        <asp:Label ID="DynamicControlID2" runat="server" Visible="false" Text='<%#Eval("DynamicControlID") %>'></asp:Label>
                                        <asp:Label ID="CountForFaxId" runat="server" Visible="false" Text='<%#Eval("CountForFaxId") %>'></asp:Label>
                                        <%# DataBinder.Eval(Container.DataItem, "labelName")%>
                                    </td>
                                
                                    <td width="25%">
                                       <asp:TextBox ID="txtvalue" class="form-control" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DynamicControlValueText") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                </ItemTemplate>
                          
                        </asp:Repeater>
                     </tbody>
                             </table>

                    </td>
                </tr>
     
</table>

                </td>

            </tr>
               
            </ItemTemplate>
           
        </asp:Repeater>

                    </td>
             
        </tr>
    </table>

            
           <%-- </div>--%>
           <br />
           <table width="100%">
                 <tr>
                 <td colspan="2" width="30%"></td><td><asp:Button ID="Button1" runat="server" class="btn btn-success btn-sm" Text="Submit Data" onclick="Button1_Click"  /></td>
                <td colspan="2" width="30%"></td><td><asp:Button ID="Button2" runat="server" class="btn btn-success btn-sm" Text="Submit Data" onclick="Button2_Click"  /></td>
                      </tr>
                 </table>
           <br /><br />
           </div>
</asp:Content>