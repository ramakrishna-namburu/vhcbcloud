<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnusedOnlineProjects.aspx.cs" Inherits="vhcbcloud.UnusedOnlineProjects" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <p class="lead">New Inactive project</p>

        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="panel-body">




                        <table style="width: 60%;">
                            <tr>
                                <td style="height: 30px">
                                    <span class="labelClass">Program</span>
                                </td>
                                <td style="height: 30px">
                                    <asp:DropDownList ID="ddlProgram" CssClass="clsDropDown" runat="server" Style="margin-left: 0">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 30px"></td>
                                <td style="height: 30px"></td>
                            </tr>
 
                             <tr>
                                <td style="height: 30px">
                                    <span class="labelClass">Application ID</span>
                                </td>
                                <td style="height: 30px">
                                    <asp:DropDownList ID="ddlApplication" CssClass="clsDropDown" runat="server" Style="margin-left: 0">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 30px"></td>
                                <td style="height: 30px"></td>
                            </tr>
                           
                            <tr>
                                <td colspan="4" style="height: 50px">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-info" OnClick="btnSubmit_Click" />
                                    &nbsp;&nbsp;
                                  <%--  <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-info" OnClick="btnClear_Click" />--%>

                                </td>
                            </tr>
                        </table>

                        &nbsp;&nbsp;&nbsp;
                        <div id="dvMessage" runat="server">
                            <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg"></asp:Label></p>
                        </div>
                         <div class="panel-body">




                    <p>
                        <asp:Panel runat="server" ID="Panel1" Width="100%" Height="350px" ScrollBars="Vertical">
                            <asp:GridView ID="gvProjGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="TempUserID"
                                Width="90%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                GridLines="None" EnableTheming="True" AllowPaging="false" OnRowCancelingEdit="gvProjGrid_RowCancelingEdit" AllowSorting="true"
                                OnRowEditing="gvProjGrid_RowEditing" OnRowUpdating="gvProjGrid_RowUpdating" OnRowDataBound="gvProjGrid_RowDataBound">
                                <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                <HeaderStyle CssClass="headerStyle" />
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                <RowStyle CssClass="rowStyle" />
                                <Columns>
                                   
                                    <asp:TemplateField HeaderText="Project Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMeettype" runat="Server" Text='<%# Eval("Projnum") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Login Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoginName" runat="Server" Text='<%# Eval("LoginName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Portfolio">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPortfolio" runat="Server" Text='<%# Eval("PortfolioDesc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="Server" Text='<%# Eval("Year") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Application">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplication" runat="Server" Text='<%# Eval("ApplicationName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Program">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgram" runat="Server" Text='<%# Eval("Program") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="TempUserID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTempUserID" runat="Server" Text='<%# Eval("TempUserID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="TempUserID" HeaderText="type ID" ReadOnly="True" Visible="false" />
                                    <asp:CommandField ShowEditButton="True" />
                                </Columns>
                                <FooterStyle CssClass="footerStyle" />
                            </asp:GridView>
                        </asp:Panel>
                    </p>
                </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
      <script language="javascript">
         
      </script>
</asp:Content>

