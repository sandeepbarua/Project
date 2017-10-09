<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicControlWebForm.aspx.cs" Inherits="PresentationLayer.DynamicControlWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    



        <p>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

        </p>
       
              <div id="dvRptDynamicTextBox" runat="server">

              </div>

        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            
        <asp:Label ID="lbl" runat="server"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
    </div>
    </form>
</body>
</html>
