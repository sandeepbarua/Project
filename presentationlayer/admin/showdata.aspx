<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="showdata.aspx.cs" Inherits="PresentationLayer.showdata" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    
    <asp:Repeater ID="rptCustomers" runat="server" OnItemCommand="rptCustomers_ItemCommand">
    <HeaderTemplate>
        <table cellspacing="0" rules="all" border="1">
            <tr>
                <th scope="col" style="width: 80px">
                    CpScreenDataId
                </th>
                <th scope="col" style="width: 120px">
                    FaxId
                </th>
                <th scope="col" style="width: 100px">
                    ReceiveDate
                </th>
                <th scope="col" style="width: 100px">
                    CustomerId
                </th>
                <th scope="col" style="width: 100px">
                    CompanyName
                </th>
                <th scope="col" style="width: 100px">
                    SourceFile
                </th>
                <th scope="col" style="width: 100px">
                    DateOfCreation
                </th>
                <th scope="col" style="width: 100px">
                    Comment
                </th>
                <th scope="col" style="width: 100px">
                    CMS_CPScreenDocumentTypeId
                </th>
                <th scope="col" style="width: 100px">
                    Labelling
                </th>
                <th scope="col" style="width: 100px">
                   TotalDays
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <asp:Label ID="lblCpscreendataid" runat="server" Text='<%# Eval("CPScreenDataID") %>' />
            </td>
            <td>
                <asp:Label ID="lblFaxID" runat="server" Text='<%# Eval("FaxID") %>' />
            </td>
            <td>
                <asp:Label ID="lblReceivedate" runat="server" Text='<%# Eval("ReceiveDate") %>' />
            </td>
            <td>
                <asp:Label ID="lblcustomerid" runat="server" Text='<%# Eval("CustomerID") %>' />
            </td>
     
            <td>
                <asp:Label ID="lblCompanyname" runat="server" Text='<%# Eval("CompanyName") %>' />
            </td>
            <td>
                <asp:Label ID="lblsourcefile" runat="server" Text='<%# Eval("SourceFile") %>' />
            </td>
            <td>
                <asp:Label ID="lbldateofcreation" runat="server" Text='<%# Eval("DateofCreation") %>' />
            </td>
            <td>
                <asp:Label ID="lblcomment" runat="server" Text='<%# Eval("Comment") %>' />
            </td>
            <td>
                <asp:Label ID="lblcms_cpscreendocumenttype" runat="server" Text='<%# Eval("CMS_CPScreenDocumentdTypeId") %>' />
            </td>
            <td>
                <asp:Label ID="lbllabelling" runat="server" Text='<%# Eval("Labelling") %>' />
            </td>
            <td>
                <asp:Label ID="lbldiffdate" runat="server" Text='<%# Eval("DiffDate") %>' />
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
    
</asp:Content>
