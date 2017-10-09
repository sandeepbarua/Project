<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CPScreenData.aspx.cs" Inherits="PresentationLayer.Admin.CPScreenData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
      <asp:Panel ID="pnlMainBody" runat="server">
                
                <!-- /.box-header -->
                 <div class="main-panel">
       
           
<div class="template" style="margin-bottom:50px" >
        
        <table id="Table1" style="margin-bottom:2px" class="table  table-bordered table-striped display">
          
                        <thead>
                            <tr style="background-color:#F6A41C">
                                <th>S.No</th>
                                
                                <th>FaxID</th>
                                <th>Process</th>
                                <th>Customer Name</th>
                             
                                <th>Total Number of Pages</th>
                                <th>Date of Creation</th>
                              
                              <%--  <th>Robot Activity</th>--%>
                               
                                <th>Labelling</th>
                                 <th>Processing Started</th>
                                <th>Processing Ended</th> 
                                <th>Total Processing Time(hr:min:sec)</th>
                            </tr>
                        </thead>
           
                     <tbody>
                           
                           <asp:Repeater ID="ReptUse" runat="server">
                                <ItemTemplate>
                                    <tr id = "trID" runat="server">
                                        <td id="serial"><%#(((RepeaterItem)Container).ItemIndex+1).ToString()%></td>
                                     <td class="ddlEditEmployeeMasterID" style="display:none"   ><%# DataBinder.Eval(Container.DataItem, "CPScreenDataID")%></td>
                                        <td id="UserFirstName" class="classUserFirstName"><%# DataBinder.Eval(Container.DataItem, "FaxID")%></td>
                                           <td id="Processing" class="classUserFirstName"><%# DataBinder.Eval(Container.DataItem, "Processed")%></td>
                                        <td id="UserLastName"  class="classUserLastName"><%# DataBinder.Eval(Container.DataItem, "CustomerName")%></td>
                                         <td id="RoleName" style="width:100px" class="classRoleName"><%# DataBinder.Eval(Container.DataItem, "TotalNumberOfPages")%></td>
                                            <td id="LoginAttempts" class="classLoginAttempt"><%# DataBinder.Eval(Container.DataItem, "DateofCreation")%></td>
                                      <%--  <td class="txtEditEmail"><a href="/Admin/RobotActivityById.aspx?id=<%# DataBinder.Eval(Container.DataItem,"RoboActivityID")%>"><%# DataBinder.Eval(Container.DataItem, "RoboActivityID")%></a></td>
                                      --%>
                                       <%-- <td id="email" class="classEmail"><%# DataBinder.Eval(Container.DataItem, "RoboActivityID")%></td>--%>
                                         <td id="emaild" class="classEmail"><%# DataBinder.Eval(Container.DataItem, "Labelling")%></td>
                                             <td id="emaildds" class="classEmail"><%# DataBinder.Eval(Container.DataItem, "stringProcessingStarted")%></td>
                                        <td id="ema" class="classEmail"><%# DataBinder.Eval(Container.DataItem, "stringProcessingEnd")%></td>

                                         <td id="emak" class="classEmail"><%# DataBinder.Eval(Container.DataItem, "TotalProcessingStarted")%></td>
                                       
                                       
                                       
                                       
                                        
                                      
                                        </tr>
                                        
                                         
                                </ItemTemplate>
                               
                            </asp:Repeater>
                                                                

                        </tbody>
              
                    </table>
            <div id="bottom_anchor"></div>
                </div>
        
    </div>

                            </asp:Panel>
</asp:Content>
