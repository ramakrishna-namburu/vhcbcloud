﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConservationSummary.aspx.cs" MasterPageFile="~/Site.Master"
    Inherits="vhcbcloud.Conservation.ConservationSummary" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="EventContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <!-- Tabs -->
        <div id="dvTabs" runat="server">
            <div id="page-inner">
                <div id="VehicleDetail">
                    <ul class="vdp-tabs" runat="server" id="Tabs"></ul>
                </div>
            </div>
        </div>
        <!-- Tabs -->
        <div class="container">
            <div class="panel panel-default">

                <div class="panel-heading" style="padding: 10px 5px 0px 5px">
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

                <div id="dvMessage" runat="server">
                    <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg"></asp:Label></p>
                </div>

                <div class="panel-width">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <table style="width: 100%">
                                <tr>
                                    <td><span class="labelClass">Project #:</span></td>
                                    <td>
                                        <span class="labelClass" id="ProjectNum" runat="server"></span>
                                    </td>
                                    <td><span class="labelClass">Name:</span></td>
                                    <td>
                                        <span class="labelClass" id="ProjName" runat="server"></span>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Conservation Track:</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlConservationTrack" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                    </td>
                                    <td><span class="labelClass"># of Easements:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtEasements" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                    <td><span class="labelClass">Primary Steward Organization:</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlPSO" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Total Project Acres:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtTotProjAcres" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                    <td><span class="labelClass">Wooded:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtWooded" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                    <td><span class="labelClass">% Wooded:</span></td>
                                    <td>
                                        <span id="pctWooded" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Prime:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPrime" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                    <td><span class="labelClass">% Prime:</span></td>
                                    <td>
                                        <span id="pctPrime" runat="server"></span>
                                    </td>
                                    <td><span class="labelClass">StateWide</span></td>
                                    <td>
                                        <asp:TextBox ID="txtStateWide" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">% StateWide:</span></td>
                                    <td>
                                        <span id="pctState" runat="server"></span>
                                    </td>
                                    <td><span class="labelClass">Other Acres:</span></td>
                                    <td><span id="otherAcres" runat="server"></span></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-info" OnClick="btnSubmit_Click" /></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvNewEasementHolder">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Easement Holders</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddEasementHolder" runat="server" Text="Add New Easement Holder" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvEasementHolderForm">
                            <asp:Panel runat="server" ID="Panel8">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Easement Holder</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlEasementHolder" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="AddEasementHolder" runat="server" Text="Add" class="btn btn-info" OnClick="AddEasementHolder_Click" />
                                        </td>
                                        <td style="width: 170px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvEasementHolderGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel9" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvEasementHolder" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvEasementHolder_RowEditing" OnRowCancelingEdit="gvEasementHolder_RowCancelingEdit"
                                    OnRowUpdating="gvEasementHolder_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Conserve Eholder ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConserveEholderID" runat="Server" Text='<%# Eval("ConserveEholderID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ApplicantName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApplicantName" runat="Server" Text='<%# Eval("ApplicantName") %>' />
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEasementHolderE" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtApplicantId" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("ApplicantId") %>' Visible="false">
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvNewAcreage">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Acreage</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddAcreage" runat="server" Text="Add New Acreage" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvNewAcreageForm">
                            <asp:Panel runat="server" ID="Panel1">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Description</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlAcreageDescription" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px">
                                            <span class="labelClass">Acres</span>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:TextBox ID="txtAcres" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 170px"></td>
                                        <td>
                                            <asp:Button ID="btnAddAcreage" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddAcreage_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvAcreageGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel2" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvAcreage" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvAcreage_RowEditing" OnRowCancelingEdit="gvAcreage_RowCancelingEdit"
                                    OnRowUpdating="gvAcreage_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Conserve Acres ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConserveAcresID" runat="Server" Text='<%# Eval("ConserveAcresID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="Server" Text='<%# Eval("Description") %>' />
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEasementHolderE" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtApplicantId" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("ApplicantId") %>' Visible="false">
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acres">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcres" runat="Server" Text='<%# Eval("Acres") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAcres" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("Acres") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvNewSurfaceWaters">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Surface Waters</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddSurfaceWaters" runat="server" Text="Add New Surface Waters" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvNewSurfaceWatersForm">
                            <asp:Panel runat="server" ID="Panel3">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Watershed</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlWatershed" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 166px">
                                            <span class="labelClass">Subwatershed</span>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:TextBox ID="txtSubwatershed" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Water Body</span></td>
                                        <td>
                                            <asp:DropDownList ID="ddlWaterBody" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Frontage (ft):</span></td>
                                        <td style="width: 215px"><asp:TextBox ID="txtFrontageFeet" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                        <td style="width: 166px"><span class="labelClass">Other Stream/Pond Name</span></td>
                                        <td style="width: 180px">
                                            <asp:TextBox ID="txtOtherStream" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 170px"><asp:CheckBox ID="cbActive" CssClass="ChkBox" runat="server" Text="Active" Checked="true" Enabled="false" /></td>
                                        <td>
                                            <asp:Button ID="btnAddSurfaceWaters" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddSurfaceWaters_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvSurfaceWatersGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel4" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvSurfaceWaters" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true" OnRowEditing="gvSurfaceWaters_RowEditing" OnRowCancelingEdit="gvSurfaceWaters_RowCancelingEdit" 
                                    OnRowDataBound="gvSurfaceWaters_RowDataBound">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Surface Waters ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurfaceWatersID" runat="Server" Text='<%# Eval("SurfaceWatersID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Watershed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwatershed" runat="Server" Text='<%# Eval("watershed") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Waterbody">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwaterbody" runat="Server" Text='<%# Eval("waterbody") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frontage">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFrontageFeet" runat="Server" Text='<%# Eval("FrontageFeet") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfProjectId" runat="server" />
    <asp:HiddenField ID="hfConserveId" runat="server" />
    <asp:HiddenField ID="hfSurfaceWatersId" runat="server" />
    
    <script language="javascript">
        $(document).ready(function () {

            $('#<%= dvEasementHolderForm.ClientID%>').toggle($('#<%= cbAddEasementHolder.ClientID%>').is(':checked'));

            $('#<%= cbAddEasementHolder.ClientID%>').click(function () {
                $('#<%= dvEasementHolderForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvNewAcreageForm.ClientID%>').toggle($('#<%= cbAddAcreage.ClientID%>').is(':checked'));

            $('#<%= cbAddAcreage.ClientID%>').click(function () {
                $('#<%= dvNewAcreageForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvNewSurfaceWatersForm.ClientID%>').toggle($('#<%= cbAddSurfaceWaters.ClientID%>').is(':checked'));

            $('#<%= cbAddSurfaceWaters.ClientID%>').click(function () {
                $('#<%= dvNewSurfaceWatersForm.ClientID%>').toggle(this.checked);
            }).change();
            

            $('#<%= txtTotProjAcres.ClientID%>').blur(function () {
                CalculatePercentages();
            });
            $('#<%= txtWooded.ClientID%>').blur(function () {
                CalculatePercentages();
            });
            $('#<%= txtPrime.ClientID%>').blur(function () {
                CalculatePercentages();
            });
            $('#<%= txtStateWide.ClientID%>').blur(function () {
                CalculatePercentages();
            });
            function CalculatePercentages() {
                //console.log(87.93456.toPrecision(4))
                var txttot = parseInt($('#<%=txtTotProjAcres.ClientID%>').val(), 10);
                var txtwooded = parseInt($('#<%=txtWooded.ClientID%>').val(), 10);
                var txtprime = parseInt($('#<%=txtPrime.ClientID%>').val(), 10);
                var txtstate = parseInt($('#<%=txtStateWide.ClientID%>').val(), 10);

                var Other = txttot - (txtwooded + txtprime + txtstate);
                $('#<%=otherAcres.ClientID%>').text(Other);
                var pctWooded = parseFloat(parseInt($('#<%=txtWooded.ClientID%>').val(), 10) * 100) / parseInt($('#<%=txtTotProjAcres.ClientID%>').val(), 10);
                var pctPrime = parseFloat(parseInt($('#<%=txtPrime.ClientID%>').val(), 10) * 100) / parseInt($('#<%=txtTotProjAcres.ClientID%>').val(), 10);
                var pctState = parseFloat(parseInt($('#<%=txtStateWide.ClientID%>').val(), 10) * 100) / parseInt($('#<%=txtTotProjAcres.ClientID%>').val(), 10);

                $('#<%=pctWooded.ClientID%>').text(pctWooded.toPrecision(4));
                $('#<%=pctPrime.ClientID%>').text(pctPrime.toPrecision(4));
                $('#<%=pctState.ClientID%>').text(pctState.toPrecision(4));
            };
        });
    </script>
</asp:Content>