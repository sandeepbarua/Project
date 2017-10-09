<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrCEDPreviousEmployerDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrCEDPreviousEmployerDetail" %>

<h3>Employment Gap Details</h3>

<table id="tbl" runat="server" class="table" style="width: 960px;">
    <tr>
       
        <th>Employment Gap From</th>
        <th>Employment Gap To</th>
        <th>Reason For Employment Gap</th>
    </tr>
</table>


<br />
<div style="text-align:right">
<asp:Button ID="btnAddRow2" runat="server" Text="Add Rows" OnClick="btnAddRow2_Click" />
<asp:Button ID="btnRemoveRow2" runat="server" Text="Remove Rows" OnClick="btnRemoveRow2_Click" />
    </div>
