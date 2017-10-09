<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrDriverLicenceDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrDriverLicenceDetail" %>



<H3>Driver Licence Details</H3>



<table id="tbl" runat="server"  class="table">
       <tr>
        <th >Restriction</th>
        <th>Endorsement</th>
       
    </tr>  
</table>


<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>