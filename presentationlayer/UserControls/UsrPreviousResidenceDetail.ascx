<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrPreviousResidenceDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrPreviousResidenceDetail" %>
<h3>Previous Residence Details</h3>

<table id="tbl" runat="server" class="table" style="width: 960px;">
    <tr>
       
        <th>Street Address</th>
        <th>City</th>
        <th>State</th>
        <th>Zip</th>
      <th>Duration</th>
    </tr>
</table>


<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>