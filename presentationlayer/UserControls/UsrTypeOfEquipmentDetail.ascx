<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrTypeOfEquipmentDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrTypeOfEquipmentDetail" %>

<h3>Type Of Equipment Details</h3>

<table id="tbl" runat="server" class="table" style="width: 960px;">
    <tr>
       
        <th>Type Of Equipment</th>
        <th>Miles</th>
        <th>Driving From</th>
        <th>Driving To</th>
      
    </tr>
</table>


<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>