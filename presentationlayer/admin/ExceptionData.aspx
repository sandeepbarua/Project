<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.Master" AutoEventWireup="true" CodeBehind="ExceptionData.aspx.cs" Inherits="PresentationLayer.Admin.ExceptionData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style>
        .template {
            height: 600px;
            /*overflow-y: scroll;*/
        }
    </style>
     <script type="text/javascript">
        function ExpandCollapse(Ctrl) {
            var ImgCtrl = Ctrl.id;
            var TrId = ImgCtrl.replace("imgExpCol", "TrProductSales");
            if (document.getElementById(TrId).style.display == 'none') {
                document.getElementById(TrId).style.display = '';
                document.getElementById(ImgCtrl).src = '/Admin/Images/minus.gif';
            }
            else {
                document.getElementById(TrId).style.display = 'none';
                document.getElementById(ImgCtrl).src = '/Admin/Images/plus.gif';
            }
            return false;
        }
    </script>

    <style type="text/css">
        .LinkButton
        {
            color: orange;
            font-family: Calibri;
            font-weight: bold;
            font-size: 15px;
        }
    </style>
    
    <table id="Table1" style="margin-bottom:5px" class="table  table-bordered table-striped display">
          
                        <thead>
                            <tr >
                               <%-- <th class="col" style="width:60px; color:white; text-align:center">S.No</th>--%>
                                 <th class="col" style="width:60px;  text-align:center">Fax ID</th>
                                
                                 <th class="col" style="width:100px;  text-align:center">Receive Date</th>
                                <th class="col" style="width:100px;  text-align:center">Pending Since</th>
                                 <th class="col" style="width:100px;  text-align:center">Pending for Review</th>
                                      <th class="col" style="width:100px;  text-align:center">Total Number Of Pages</th>
                                <th class="col" style="width:100px; text-align:center">View</th>
                                
                            </tr>
                       </thead>
         
                     <tbody>

                           <asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_OnItemDatabound">
                               
                                <ItemTemplate>
                                    <tr id = "trID" runat="server" style="margin-bottom:5px; border-collapse:separate; border-spacing:10px 0px;">
                                        <%-- <td style="width:60px">&nbsp;&nbsp;<%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
        
                                 
                                        </td>--%>
                                        <td style="width:60px" >
                                          &nbsp;&nbsp;<asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FaxID") %>'></asp:Label>
                                            <asp:HiddenField ID="hdndocid" runat="server" Value='<%#Eval("CMS_CPScreenDocumentdTypeId")%>' />
                                             <asp:HiddenField ID="hdnuserid" runat="server" Value='<%#Eval("UserId")%>'/>
                                        </td>
                                       <td style="width:100px">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "ReceiveDate")%></td>
                                         <td style="width:100px">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "DateofCreation")%></td>
                                        <td style="width:100px">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "HrsMM")%></td>
                                         <td style="width:100px">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "TotalNumberOfPages")%></td>
                                        
                                        <td style="width:100px">
                                           &nbsp;&nbsp; <asp:ImageButton ID="lbtnView" runat="server" ImageUrl="~/Admin/Images/View.png" Height="18px" onclick="lbtnView_Click" />
                                         
                                        </td>

                                       
                                    </tr>
                                    <tr id="TrProductSales" runat="server" style="display: none;">
                                        <td colspan="15" align="center">
                                            <asp:DataList ID="dlProductSales"  Width="100%" runat="server"
                                                RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Table" OnItemDataBound="dlProductSales_OnItemDataBound">
                                                <HeaderTemplate>
                                                    <table  width="100%">
                                                        <tr style="background-color:#F6A41C">
                                                       <%-- <td style="width: 15%;">
                                                Fax ID
                                            </td>     --%>
                                          
                                            <td style="display:none">
                                               &nbsp;&nbsp;&nbsp;&nbsp; User ID
                                            </td>
                                            <td style="display:none" >
                                                User Name
                                            </td>
                                            <td style="display:none">
                                                Dynamic Type
                                            </td>
                                           </tr>
                                                         </HeaderTemplate>
                                            <ItemTemplate>
                                                    <tr>
                                                        <td style="display:none">
                                                         &nbsp;&nbsp;&nbsp;&nbsp;   <asp:Label ID="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UserDetailsID") %>'></asp:Label>
                                                        </td>
                                                       <td  style="display:none">
                                                            <%# DataBinder.Eval(Container.DataItem,"UserFirstName") %>   <%# DataBinder.Eval(Container.DataItem,"UserLastName") %>
                                                        </td>
                                                        <td style="display:none" >
                                                            <%# DataBinder.Eval(Container.DataItem,"DocumentTypeName") %>
                                                        </td>

                                                        <td style="width: 100px;">
                                                            <asp:LinkButton ID="lbtnOrderView" Visible="false" CssClass="LinkButton" runat="server" Text="View"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                      
                                                        <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                               
                                              
                                                    
                                                
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                             <%--   <FooterTemplate>

                                       
                                    <td style="width: 10%;">
                                         <%# DataBinder.Eval(Container.DataItem,"UserId") %>
                                        </td>
                                        <td style="width: 10%;">
                                            <%# DataBinder.Eval(Container.DataItem,"DynamicControlID") %>
                                        </td>
                                        <td style="width: 10%;">
                                            <%# DataBinder.Eval(Container.DataItem,"DynamicControlValueText") %>
                                        </td>
                                     <td style="width: 10%;">
                                                            <asp:LinkButton ID="lbtnOrderView" CssClass="LinkButton" runat="server" Text="View"></asp:LinkButton>
                                                        </td>

                                    </table>
                                </FooterTemplate>--%>
                          </asp:Repeater>
<%--                           <asp:Repeater ID="ReptUse" runat="server">
                                <ItemTemplate>
                                    <tr id = "trID" runat="server">
                                        <td id="serial" style="width:57px"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                         <td class="classCompanyID" ><%# DataBinder.Eval(Container.DataItem, "UserId")%></td>
                                     

                                          <td class="classCompanyID" ><%# DataBinder.Eval(Container.DataItem, "FaxID")%></td>
                                        <td id="DocumentTypeName"  class="classFADV_CustomerID"><%# DataBinder.Eval(Container.DataItem, "DynamicControlID")%></td>

                                        <td id="CompanyNames" class="classCompanyName"><%# DataBinder.Eval(Container.DataItem, "DynamicControlValueText")%></td>
                                       
                                      
                                        </tr>
                                        
                                </ItemTemplate>
                               
                            </asp:Repeater>--%>
                                        
                        </tbody>
              
                    </table>
</asp:Content>
