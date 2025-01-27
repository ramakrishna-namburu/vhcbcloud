<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewLookup.aspx.cs" Inherits="vhcbcloud.NewLookup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <p class="lead">New Lookup</p>
        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <p>
                        <span class="labelClass">View Name:</span>
                        <asp:TextBox ID="txtViewName" CssClass="clsTextBoxBlueSMDL" runat="server" Height="17px" Width="172px"></asp:TextBox>
                        &nbsp; &nbsp;
                        
                        <asp:CheckBox ID="cbOrdered" runat="server" Text="Ordered" />
                        &nbsp; &nbsp;
                        
                        <asp:CheckBox ID="cbTiered" runat="server" Text="Tiered" />
                        &nbsp; &nbsp;
                        
                        <asp:LinkButton ID="btnSubmit" runat="server" class="btn btn-info" Text="Add New Lookup" TabIndex="3" OnClick="btnSubmit_Click" />

                        <br />
                        <asp:Label runat="server" class="lblErrMsg" ID="lblErrorMsg"></asp:Label>
                        <br />

                    </p>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

