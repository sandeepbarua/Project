<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrTrafficConvictionsDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrTrafficConvictionsDetail" %>

<h3>Traffic Conviction Details</h3>
<p>
    Traffic Convictions And Forfeitures For Past 3 Years
    &nbsp;&nbsp;<%-- <asp:CheckBox ID="chk1" runat="server" />--%>

</p>
<table id="tbl" runat="server" class="table">
    <tr>
        <th>Location</th>
        <th>Vehicle Type</th>
        <th>Date Of Conviction</th>
        <th>Charge</th>
        <th>Penalty</th>

    </tr>

</table>

<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>