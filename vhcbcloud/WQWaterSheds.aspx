<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WQWaterSheds.aspx.cs" Inherits="vhcbcloud.WQWaterSheds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <p class="lead">WaterSheds</p>

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
                    </table>
                </div>
                <div class="panel-heading">
                    <div id="dvMessage" runat="server">
                        <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg"></asp:Label></p>
                    </div>
                    <table>
                        <tr>
                            <td><span class="labelClass">Watershed:</span></td>
                            <td>
                                <asp:DropDownList ID="ddlWatershed" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWatershed_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass">SubWatershed:</span></td>
                            <td>
                                <asp:TextBox ID="txtSubWatershed" CssClass="clsTextBoxBlue1" runat="server" Width="350px" MaxLength="75"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass">HUC12:</span></td>
                            <td>
                                <asp:TextBox ID="txtHUC12" CssClass="clsTextBoxBlue1" runat="server" Width="150px" MaxLength="15"></asp:TextBox>
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
                            <asp:GridView ID="gvWQSubwatershed" runat="server" AutoGenerateColumns="False" DataKeyNames="WQSubWatershedID"
                                Width="90%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                GridLines="None" EnableTheming="True" AllowPaging="false" OnRowCancelingEdit="gvWQSubwatershed_RowCancelingEdit" AllowSorting="false"
                                OnRowEditing="gvWQSubwatershed_RowEditing" OnRowUpdating="gvWQSubwatershed_RowUpdating" OnRowDataBound="gvWQSubwatershed_RowDataBound">
                                <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                <HeaderStyle CssClass="headerStyle" />
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                <RowStyle CssClass="rowStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Watershed">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWatershed" runat="Server" Text='<%# Eval("Watershed") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Watershed">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubWatershed" runat="Server" Text='<%# Eval("SubWaterhed") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSubWaterhed" runat="Server" CssClass="clsTextBoxBlueSMDL" Width="250px" MaxLength="250" Text='<%# Eval("SubWaterhed") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="HUC12">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHUC12" runat="Server" Text='<%# Eval("HUC12") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtHUC12" runat="Server" CssClass="clsTextBoxBlueSMDL" Width="250px" MaxLength="250" Text='<%# Eval("HUC12") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WQSubWatershedID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWQSubWatershedID" runat="Server" Text='<%# Eval("WQSubWatershedID") %>' />
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
                                    <asp:BoundField DataField="WQSubWatershedID" HeaderText="WQSubWatershedID" ReadOnly="True" Visible="false" />
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
