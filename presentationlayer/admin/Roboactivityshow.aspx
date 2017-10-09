<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Roboactivityshow.aspx.cs" Inherits="PresentationLayer.Roboactivityshow" %>
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
                    RoboActivityID
                </th>
                <th scope="col" style="width: 120px">
                    StartTime
                </th>
                <th scope="col" style="width: 100px">
                    EndTime
                </th>
                <th scope="col" style="width: 100px">
                    Activity
                </th>
                 <th scope="col" style="width: 100px">
                    TotalDays
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <asp:Label ID="lblRoboActivityId" runat="server" Text='<%# Eval("RoboActivitiesID") %>' />
            </td>
            <td>
                <asp:Label ID="lblstarttime" runat="server" Text='<%# Eval("StartTime") %>' />
            </td>
            <td>
                <asp:Label ID="lblendtime" runat="server" Text='<%# Eval("EndTime") %>' />
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Activity") %>' />
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("DiffDate") %>' />
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
</asp:Content>
