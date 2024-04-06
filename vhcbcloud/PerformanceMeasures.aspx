<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerformanceMeasures.aspx.cs" Inherits="vhcbcloud.PerformanceMeasures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <p class="lead">Performance Measures</p>

        <div class="container">
            <div class="panel panel-default">
                   <div class="panel-heading">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 171px"></td>
                            <td style="width: 192px"></td>
                            <td></td>
                            <td style="text-align: left"></td>
                            <td style="text-align: right">
                              
                                <asp:CheckBox ID="cbActiveOnly" runat="server" Text="Active Only" Checked="true" AutoPostBack="true"
                                    OnCheckedChanged="cbActiveOnly_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="height: 5px"></td>
                        </tr>
                    </table>
                </div>
                <div class="panel-heading">
                    <div id="dvMessage" runat="server">
                        <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg"></asp:Label></p>
                    </div>
                    <table>
                        <tr>
                            <td><span class="labelClass">Project Type:</span></td>
                            <td>
                                <asp:DropDownList ID="ddlProjectType" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            </tr>
                          <tr>
                            <td colspan="2" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass">Performance Measure:</span></td>
                            <td>
                                <asp:TextBox ID="txtPerformanceMeasure" CssClass="clsTextBoxBlue1" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-info" OnClick="btnSubmit_Click" />

                         </td>
     
                        </tr>

                    </table>

                </div>
                <div class="panel-body">




                    <p>
                        <asp:Panel runat="server" ID="Panel1" Width="100%" Height="350px" ScrollBars="Vertical">
                            <asp:GridView ID="gvWQPerformanceMeasures" runat="server" AutoGenerateColumns="False" DataKeyNames="WQPerformanceTypeID"
                                Width="90%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                GridLines="None" EnableTheming="True" AllowPaging="false" OnRowCancelingEdit="gvWQPerformanceMeasures_RowCancelingEdit" AllowSorting="false"
                                OnRowEditing="gvWQPerformanceMeasures_RowEditing" OnRowUpdating="gvWQPerformanceMeasures_RowUpdating" OnRowDataBound="gvWQPerformanceMeasures_RowDataBound"
                               >
                                <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                <HeaderStyle CssClass="headerStyle" />
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                <RowStyle CssClass="rowStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Project Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProjectType" runat="Server" Text='<%# Eval("ProjectType") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Performance Measure">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPerformanceMeasure" runat="Server" Text='<%# Eval("PerformanceMeasure") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPerformanceMeasure" runat="Server" CssClass="clsTextBoxBlueSMDL" Width="250px" MaxLength="250" Text='<%# Eval("PerformanceMeasure") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WQPerformanceTypeID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWQPerformanceTypeID" runat="Server" Text='<%# Eval("WQPerformanceTypeID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="WQPerformanceTypeID" HeaderText="WQPerformanceTypeID" ReadOnly="True" Visible="false" />
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
</asp:Content>

