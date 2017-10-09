<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrPreviousEmployerDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrPreviousEmployerDetail" %>

<h3>Employment Details</h3>

<table id="tbl" runat="server" class="table" style="width: 960px;">
    <tr>
        <th>Employer Name</th>
        <th>Street Address</th>
        <th>City</th>
        <th>State</th>
        <th>Zip</th>
        <th>Employment Start Date</th>
        <th>Employment End Date</th>
        <th>Reason For Leaving</th>
    </tr>
</table>


<br />
<div style="text-align:right">
<asp:Button ID="btnAddRow2" runat="server" Text="Add Rows" OnClick="btnAddRow2_Click" />
<asp:Button ID="btnRemoveRow2" runat="server" Text="Remove Rows" OnClick="btnRemoveRow2_Click" />
    </div>
