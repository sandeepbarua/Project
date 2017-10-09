<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrCurrentEmployerDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrCurrentEmployerDetail" %>

<h3>Current Employer Details</h3>

<table id="tbl" runat="server" class="table" style="width: 960px;">
    <tr>
        <th>Employer Name</th>

        <th>Street Address</th>
        <th>City</th>
        <th>State</th>
        <th>Zip</th>
        <th>Employment Start Date</th>
     
    </tr>
</table>


<br />
<div style="text-align:right">
<asp:Button ID="btnAddNewRow1" runat="server" Text="Add Rows" OnClick="btnAddNewRow1_Click" />
<asp:Button ID="btnRemoveRow1" runat="server" Text="Remove Rows" OnClick="btnRemoveRow1_Click" />
    </div>
