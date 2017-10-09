<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrDriverLicenseStatusDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrDriverLicenseStatus" %>

<h3>Driving License Status </h3>

<p>
    Denied, Suspended/Revoked
    &nbsp;&nbsp;<%-- <asp:CheckBox ID="chk1" runat="server" />--%>

</p>



<table id="tbl" runat="server" class="table">
    <tr>
        <th>DriverLicenseStatusStatement</th>
       
    </tr>

</table>

<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>
