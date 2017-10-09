<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrConvictionDetails.ascx.cs" Inherits="PresentationLayer.UserControls.UsrConvictionDetails" %>

<H3>Conviction Details</H3>
 <table id="tbl" runat="server" class="table" >
        <tr>
        <th >Date Of Conviction</th>
        <th>Offense</th>
        <th >Location</th>
        <th >Type Of Vehicle Operated</th>
    </tr> 

 </table>

<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>