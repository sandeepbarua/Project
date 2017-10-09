<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dynamicUserCtrl.aspx.cs" Inherits="PresentationLayer.dynamicUserCtrl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
            <ItemTemplate>
                <asp:HiddenField ID="hfDocumentTypeId" Value='<%# Eval("DocumentTypeId") %>' runat="server" />
                <asp:Label ID="lblDocumentName" Text='<%#Eval("DocumentName")%>' runat="server"></asp:Label>
                <asp:Panel ID="pnl" runat="server"></asp:Panel>
            </ItemTemplate>
            <SeparatorTemplate>
                <br />
            </SeparatorTemplate>
        </asp:Repeater>

        <p>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </p>

        <p>
        <asp:Label ID="lbl1" runat="server" ></asp:Label>
        </p>
    
        <p>
        <asp:Label ID="lbl2" runat="server" ></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
