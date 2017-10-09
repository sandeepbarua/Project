<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="EditAuditQuestion.aspx.cs" Inherits="PresentationLayer.Admin.AuditEngine.EditAuditQuestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMainBody" runat="server">
           
                 <div class="main-panel">
                     <br />
                     
                     <div class="modal-header bg-aqua-active">



                        <h4 class="modal-title">Edit Audit Question</h4>

                    </div>
                     <br /><br />
                     <div align="center">
                         <table width="50%">
                             <tr>
                                 
                                 <td>
                                    
                                        <label>Select Template</label>
                                       
                                 </td>
                                 <td>
                                     <asp:Label ID="lblDocumentType" runat="server" class="form-control"></asp:Label>                                     
                                 
                                    </td>
                             </tr>
                             <tr>
                             <td style="padding-top:5px"> 
                                        <label>Enter Audit Question</label>
                                       </td>
                             <td style="padding-top:10px">
                                        <asp:TextBox class="form-control" ID="txtEditAuditQuestion" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="EditValida" ControlToValidate="txtEditAuditQuestion" runat="server" ErrorMessage="Enter Audit Question" ForeColor="Red"></asp:RequiredFieldValidator>

                                <input type="hidden" class="form-control" id="hiddenDynamicControlID" name="hiddenDynamicControlID" />
                               
                                   </td>
                             </tr>
                             
                         </table>
                            
                         </div>
                        
                                    <div class="modal-footer">                                      
                                      
                                       <asp:Button ID="btnUpdate" ValidationGroup="EditValida" runat="server" Text="Save Changes" class="btn btn-primary" OnClientClick="return validedit();" OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btnClose" runat="server" class="btn btn-primary" data-dismiss="modal" OnClick="btnClose_Click" Text="Close" />
                                    </div>
                      

                                </div>



                     </asp:Panel>
</asp:Content>
