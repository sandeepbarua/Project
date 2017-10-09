<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrAccidentRecordDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrAccidentRecordDetail" %>

<h3>Accident Record Details</h3>

<p>
    If Accident Record For Past 3 Years
    &nbsp;&nbsp; <%--<asp:CheckBox ID="chk1" runat="server" />--%>

</p>

<table id="tbl" runat="server" class="table">
    <tr>
        <th>Nature Of Accident</th>
        <th>Date Of Accident</th>
        <th>Fatalities</th>
        <th>Injuries</th>
       

    </tr>

</table>

<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>