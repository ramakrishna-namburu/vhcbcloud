﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fundingsource.aspx.cs" Inherits="vhcbcloud.Fundingsource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <p class="lead">Funding Source</p>
        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <p>

                        <span class="labelClass">Name :</span>
                        <asp:TextBox ID="txtFName" CssClass="clsTextBoxBlue1" runat="server"></asp:TextBox>

                        <br />
                        <%--<asp:RequiredFieldValidator ID="rfvFname" runat="server" ErrorMessage="funding name required" CssClass="lblErrMsg" ControlToValidate="txtFName"></asp:RequiredFieldValidator>--%>

                        <br />
                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Images/BtnSubmit.gif" TabIndex="3" OnClick="btnSubmit_Click" />
                    </p>
                    <p class="lblErrMsg">
                        <asp:Label runat="server" ID="lblErrorMsg"></asp:Label>
                    </p>
                </div>
                <div class="panel-body">
                    <p>
                        <asp:Panel runat="server" ID="Panel1" Width="100%" Height="350px" ScrollBars="Vertical">
                            <asp:GridView ID="gvFSource" runat="server" AutoGenerateColumns="False" DataKeyNames="fundid"
                                Width="90%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                GridLines="None" EnableTheming="True" AllowPaging="false" OnRowCancelingEdit="gvFSource_RowCancelingEdit" AllowSorting="true"
                                OnRowEditing="gvFSource_RowEditing" OnRowUpdating="gvFSource_RowUpdating"
                                OnPageIndexChanging="gvFSource_PageIndexChanging" OnRowDataBound="gvFSource_RowDataBound" OnSorting="gvFSource_Sorting">
                                <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                <HeaderStyle CssClass="headerStyle" />
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                <RowStyle CssClass="rowStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Funding Source Name" SortExpression="name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="Server" Text='<%# Eval("name") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtName" runat="Server" CssClass="clsTextBoxBlueSMDL" Text='<%# Eval("name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Fund Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFundId" runat="Server" Text='<%# Eval("fundid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
