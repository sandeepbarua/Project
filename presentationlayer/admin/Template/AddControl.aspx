<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="AddControl.aspx.cs" Inherits="PresentationLayer.Admin.Template.AddControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:scriptmanager runat="server"></asp:scriptmanager>
    <asp:panel id="pnlMainBody" runat="server">
            
                 <div class="main-panel">
                     <br />
                     <div align="center">
                         ADD CONTROL
                     </div>
                     <br /><br />
                     <div align="center">
                         <table width="70%">
                                   <tr><td colspan="2">   <asp:ValidationSummary 
            ID="ValidationSummary1" 
            runat="server" 
            HeaderText="Following error occurs....." 
            ShowMessageBox="false" 
            DisplayMode="BulletList" 
            ShowSummary="true"
            BackColor="Snow"
           ValidationGroup="added"
            Width="400"
            ForeColor="Red"
            Font-Size="Small"
          
            Font-Italic="true"
           
            /></td></tr>
                             <tr>
                                 <td >
                                    
                                        <label>Template    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="added" ControlToValidate="ddlAddControlType" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red" Enabled="false" Text="*"></asp:RequiredFieldValidator></label>
                                       
                                 </td>
                                 <td>
                                        <asp:Label ID="lblDocumentType" runat="server" class="form-control"></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="added" ControlToValidate="ddlAddControlType" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red" Enabled="false" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                             </tr>
                             <tr>
                             <td> 
                                        <label>Enter Data Field   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="added" ControlToValidate="txtAddLabelName" runat="server" ErrorMessage="Please Enter Data Field" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                    </label>
                                       </td>
                             <td>
                                        <asp:TextBox class="form-control" ID="txtAddLabelName" runat="server"></asp:TextBox>
                                                    </td>
                             </tr>
                              <tr>
                                        <td> 
                                        <label>Select Control Type</label>      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="added" ControlToValidate="ddlAddControlType" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                
                                       </td>
                                        <td>
                                        <asp:DropDownList ID="ddlAddControlType" class="form-control"  AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAddControlType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select Control" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="TextBox" Value="1"></asp:ListItem>

                                            <asp:ListItem Text="Calender" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="RadioButton" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                                     </td>                                        
                             </tr>
                              </table>
                             
                                  
<%--                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlTextBox" runat="server">--%>
                                        <table id="tbl" runat="server" width="70%" visible="false">
                                        <tr>
                                        <td> 
                                      <asp:Label ID="lblDropDownValue" runat="server" >Enter Select Value</asp:Label>
                                            </td>
                                             <td >
                                        <asp:TextBox class="form-control" ID="txtDropDownValue" runat="server" visible="false"></asp:TextBox>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="added" ControlToValidate="txtDropDownValue" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                   </td>
                                            </tr>
                                            </table>
                                            <%-- </asp:Panel>
                                        </ContentTemplate>--%>
                                          <%-- <Triggers> 
                                        <asp:AsyncPostBackTrigger ControlID="ddlAddControlType" EventName="SelectedIndexChanged" /> 
                                        </Triggers>
                                          </asp:UpdatePanel>--%>
                         
<%--                              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlRadioBtn" runat="server">--%>
                                           
                                        <table id="tbl1"  runat="server" width="70%" visible="false">
                                        <tr >
                                            <td > 
                                            <asp:Label ID="lbltxtAddChoiceOne" runat="server">Enter Choice One</asp:Label>
                                           </td>
                                        <td >
                                        <asp:TextBox class="form-control" ID="txtAddChoiceOne" runat="server" ></asp:TextBox>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="added" ControlToValidate="txtAddChoiceOne" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                      </td>                                        
                                    </tr>
                                            <tr>
                                            <td> 
                                            <asp:Label ID="lblAddChoiceTwo" runat="server" >Enter Choice Two</asp:Label>
                                           </td>
                                        <td >
                                        <asp:TextBox class="form-control" ID="txtAddChoiceTwo" runat="server" ></asp:TextBox>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="added" ControlToValidate="txtAddChoiceTwo" InitialValue="0" runat="server" ErrorMessage="Please Select Control Type" ForeColor="Red"></asp:RequiredFieldValidator>
                                      </td>                                        
                                    </tr>
                                            </table>
                                            <%--</asp:Panel>
                                            </ContentTemplate>
                                  <Triggers> 
                                        <asp:AsyncPostBackTrigger ControlID="ddlAddControlType" EventName="SelectedIndexChanged" /> 
                                        </Triggers>
                                </asp:UpdatePanel>--%>
                        
                            
                         </div>
                        
                                    <div class="modal-footer">                                      
                                      
                                       <asp:Button ID="BtnAddTemplate" runat="server" Text="Save" OnClick="BtnAddControl_Click"  ValidationGroup="added" class="btn btn-primary"  />
                                        <asp:Button ID="btnCacel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCacel_Click"/>
                                        
                                    </div>
                      

                                </div>



                     </asp:panel>

</asp:Content>
