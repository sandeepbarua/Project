<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrPreviousEmploymentDetail.ascx.cs" Inherits="PresentationLayer.UserControls.UsrPreviousEmploymentDetail" %>


<h3>Previous Employeement Details</h3>
<table id="tbl" runat="server"   class="table">
      <tr>
        <th >Employer Name</th>
        <th>Employment Start Date</th>
         <th>Employment End Date</th>
    </tr>  
</table>


<br />
<div style="text-align:right">
<asp:Button ID="btnAddRows" runat="server" Text="Add Rows" OnClick="btnAddRows_Click" />
<asp:Button ID="btnRemoveRows" runat="server" Text="Remove Rows" OnClick="btnRemoveRows_Click" />
    </div>