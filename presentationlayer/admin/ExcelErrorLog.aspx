<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ExcelErrorLog.aspx.cs" Inherits="PresentationLayer.Admin.ExcelErrorLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">


    <table>  
            <tr>  
                <td>  
                    Select File  
                </td>  
                <td>  
                    <asp:FileUpload ID="FileUpload1" runat="server" ></asp:FileUpload> 
                </td>  
               
               
            </tr>  
        <tr>
            <td></td>
            <td style="text-align:right;">
                  <asp:Button ID="btnUpload" runat="server" Text="Upload"  OnClick="btnUpload_Click"/> 
            </td>
        </tr>
        </table>  
  <asp:Label ID="lblmsg" runat="server" ></asp:Label>
</asp:Content>
