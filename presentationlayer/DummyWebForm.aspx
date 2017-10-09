<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DummyWebForm.aspx.cs" Inherits="PresentationLayer.DummyWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
               <script type="text/javascript">
                             function ManageControl(id, selectedValue) {
                                 alert(id);
                                 alert(selectedValue);
                                 var text = document.getElementById(id.replace("ddlTest", "hfControlName"));

                                 if (selectedValue == "1") {
                                     text.style.display = "";
                                 } else {
                                     text.style.display = "none";
                                 }
                             }
    </script>
            <asp:DropDownList ID="dd" runat="server" onchange="ManageControl(this.id,this.value)">
         <asp:ListItem Text="a" Value="b"></asp:ListItem>
         <asp:ListItem Text="c" Value="c"></asp:ListItem>
         <asp:ListItem Text="d" Value="d"></asp:ListItem>
     </asp:DropDownList>

            <p>
                <asp:RadioButtonList ID="rb" runat="server">
                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                </asp:RadioButtonList>

            </p>
            <p>
                <asp:Button ID="btn" runat="server" Text="Click " OnClick="btn_Click" />
            </p>

            <p>
                <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>
                        <div id="dvDynamciTextBox" runat="server"></div>
                        <div >
                              <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" />

                        </div>
                      
                    </ContentTemplate>
                    
                </asp:UpdatePanel>
                <asp:Button runat="server" Text="Next" ID="btnNext" OnClick="btnNext_Click" />
            </p>
            <asp:Label ID="lbl1" runat="server"></asp:Label>
            
            <p>
                Repeater Data
            </p>
            
            <p>
            
                <asp:Repeater ID="rpt" runat="server" OnItemCommand="rpt_ItemCommand">
                    <ItemTemplate>
                        <div id="dvRptDynamicTextBox" runat="server">
                            <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
                             </div>
                            <br />
                            <asp:Button ID="btnRptAdd" runat="server" Text="Add" CommandName="Add" />
                            <asp:Button ID="btnRptRemove" runat="server" Text="Remove" CommandName="Remove" />

                       
                    </ItemTemplate>
                    <SeparatorTemplate>
                        <br />
                    </SeparatorTemplate>
                </asp:Repeater>

            </p>
        </div>
    </form>
</body>
</html>
