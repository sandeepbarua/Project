<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrViolationDetails.ascx.cs" Inherits="PresentationLayer.UserControls.UsrViolationDetails" %>
<H3>Violation Details</H3>
 <table id="tbl" runat="server" class="table" >
        <tr>
        <th >Type of violations</th>
        <th>Date</th>
        <th >Location</th>
        <th >Type Of Vehicle</th>
    </tr> 

 </table>

<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>