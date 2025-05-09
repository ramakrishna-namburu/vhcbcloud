﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConservationSummary.aspx.cs" MasterPageFile="~/Site.Master"
    Inherits="vhcbcloud.Conservation.ConservationSummary" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="EventContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .scroll_checkboxes {
            height: 100px;
            padding: 1px;
            overflow: auto;
            border: 1px solid #ccc;
        }

        .FormText {
            FONT-SIZE: 11px;
            FONT-FAMILY: tahoma,sans-serif;
        }

        .checkboxlist_nowrap label {
            white-space: nowrap;
            display: inline-block;
        }
    </style>
    <div class="jumbotron" id="vhcb">
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

                <div class="panel-heading">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 171px"></td>
                            <td style="width: 192px"></td>
                            <td></td>
                            <td style="text-align: left"></td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="imgSearch" ImageUrl="~/Images/search.png" ToolTip="Project Search"
                                    Style="border: none; vertical-align: middle;" runat="server" Text="Project Search"
                                    OnClientClick="window.location.href='../ProjectSearch.aspx'; return false;"></asp:ImageButton>
                                <asp:ImageButton ID="ibAwardSummary" runat="server" ImageUrl="~/Images/$$.png" Text="Award Summary" Style="border: none; vertical-align: middle;"
                                    OnClientClick="PopupAwardSummary(); return false;"></asp:ImageButton>
                                <asp:ImageButton ID="btnProjectNotes" runat="server" ImageUrl="~/Images/notes.png" Text="Project Notes" Style="border: none; vertical-align: middle;" />
                                <asp:CheckBox ID="cbActiveOnly" runat="server" Text="Active Only" Checked="true" AutoPostBack="true"
                                    OnCheckedChanged="cbActiveOnly_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="height: 5px"></td>
                        </tr>
                    </table>
                </div>

                <ajaxToolkit:ModalPopupExtender ID="mpExtender" runat="server" PopupControlID="pnlProjectNotes" TargetControlID="btnProjectNotes" CancelControlID="btnClose"
                    BackgroundCssClass="MEBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlProjectNotes" runat="server" CssClass="MEPopup" align="center" Style="display: none">
                    <iframe style="width: 750px; height: 600px;" id="ifProjectNotes" src="../ProjectNotes.aspx" runat="server"></iframe>
                    <br />
                    <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-info" />
                </asp:Panel>

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
                                    <td><span class="labelClass">Conservation Track:</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlConservationTrack" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>

                                    <td><span class="labelClass"># of Easements:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtEasements" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                    <%--<td><span class="labelClass">Primary Steward Organization:</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlPSO" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                    </td>--%>
                                    <td><span class="labelClass">Geographic Significance:</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlGeoSignificance" CssClass="clsDropDown" runat="server"></asp:DropDownList></td>
                                    <%-- <td><span class="labelClass">Total Project Acres:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtTotProjAcres" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>--%>
                                    <td><span class="labelClass">Transfer Type:</span></td>
                                    <td>
                                        <asp:ListBox runat="server" SelectionMode="Multiple" Height="42px" Width="150px" ID="lbTransferType" CssClass="clsTextBoxBlue1"></asp:ListBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 5px"><span class="labelClass">Tactical Basin:</span></td>
                                    <td style="height: 5px">
                                        <asp:DropDownList ID="ddlTacticalBasin" CssClass="clsDropDown" runat="server" Height="23px" Width="185px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 5px"><span class="labelClass" runat="server" id="spnFTE" visible="false">FTEs Hired:</span></td>
                                    <td style="height: 5px"><asp:TextBox ID="txtFTEHired" CssClass="clsTextBoxBlueSm" runat="server" Visible="false"></asp:TextBox></td>
                                    <td style="height: 5px"></td>
                                    <td style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <%-- <tr>
                                    <td><span class="labelClass">% Wooded:</span></td>
                                    <td>  
                                    </td>
                                    <td><span class="labelClass">% Prime:</span></td>
                                    <td>
                                        <span class="labelClass" id="pctPrime" runat="server"></span>
                                    </td>
                                    <td><span class="labelClass">% StateWide:</span></td>
                                    <td>
                                        <span class="labelClass" id="pctState" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Other Acres:</span></td>
                                    <td><span class="labelClass" id="otherAcres" runat="server"></span></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>--%>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="Div1">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <h3 class="panel-title">Acres</h3>
                        </div>

                        <div class="panel-body">
                            <table style="width: 100%">
                                <tr>
                                    <td><span class="labelClass">Tillable:</span></td>
                                    <td style="width: 142px">
                                        <asp:TextBox ID="txtTillable" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                    <td><span class="labelClass">UnManaged:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtUnManaged" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                    <td><span class="labelClass">Prime:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPrime" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Pasture:</span></td>
                                    <td style="width: 142px">
                                        <asp:TextBox ID="txtPasture" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                    <td><span class="labelClass">Farmstead/Residential:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtFarmResident" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                    <td><span class="labelClass">StateWide:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtStateWide" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Wooded:</span></td>
                                    <td style="width: 142px">
                                        <asp:TextBox ID="txtWooded" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                    <td><span class="labelClass">% Wooded:</span></td>
                                    <td><span class="labelClass" id="pctWooded" runat="server"></span></td>
                                    <td><span class="labelClass">% Prime + Statewide:</span></td>
                                    <td><span class="labelClass" id="pctPrimeStateWide" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Natural/Rec</span></td>
                                    <td>
                                        <asp:TextBox ID="txtNaturalRec" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                    <td><span class="labelClass">Total Project Acres:</span></td>
                                    <td><span class="labelClass" id="spnTotalProject" runat="server"></span></td>
                                    <td><span class="labelClass"></span></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Sugarbush</span></td>
                                    <td>
                                        <asp:TextBox ID="txtSugarbush" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                    <td><span class="labelClass">% Sugarbush:</span></td>
                                    <td><span class="labelClass" id="pctSugarBush" runat="server"></span></td>

                                    <td><span class="labelClass"></span></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Hay</span></td>
                                    <td>
                                        <asp:TextBox ID="txtHay" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                    <td><span class="labelClass"># of taps</span></td>
                                    <td>
                                        <asp:TextBox ID="txtTaps" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>

                                    <td><span class="labelClass"></span></td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-info" OnClick="btnSubmit_Click" />
                                    </td>
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
                                        <asp:ImageButton ID="ImgEasementHoldersReport" ImageUrl="~/Images/print.png" ToolTip="Conservation Easement Holders Report"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgEasementHoldersReport_Click" />
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
                                            <asp:CheckBox ID="cbPrimarySteward" CssClass="ChkBox" runat="server" Text="Primary Steward" Checked="false" Enabled="true" /></td>
                                        <td style="width: 170px">
                                            <asp:Button ID="AddEasementHolder" runat="server" class="btn btn-info" OnClick="AddEasementHolder_Click" Text="Add" />
                                        </td>
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
                                        <asp:TemplateField HeaderText="Applicant Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApplicantName" runat="Server" Text='<%# Eval("ApplicantName") %>' />
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEasementHolderE" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtApplicantId" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("ApplicantId") %>' Visible="false">
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Primary Steward">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPrimarySteward" Enabled="false" runat="server" Checked='<%# Eval("PrimeStew") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkPrimarySteward" runat="server" Checked='<%# Eval("PrimeStew") %>' />
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
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" Visible='<%# GetIsVisibleBasedOnRole() %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                        <asp:ImageButton ID="ImgAcreage" ImageUrl="~/Images/print.png" ToolTip="Acreage Report"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgAcreage_Click" />
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
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true" ShowFooter="false"
                                    OnRowEditing="gvAcreage_RowEditing" OnRowCancelingEdit="gvAcreage_RowCancelingEdit"
                                    OnRowUpdating="gvAcreage_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <FooterStyle CssClass="footerStyleTotals" />
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
                                            <FooterTemplate>
                                                Grand Total :
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acres">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcres" runat="Server" Text='<%# Eval("Acres") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAcres" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("Acres") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblFooterTotal" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" Visible='<%# GetIsVisibleBasedOnRole() %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvTrailMileage">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Trail Mileage</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddTrailMileage" runat="server" Text="Add New Trail Mileage" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvNewTrailMileageForm">
                            <asp:Panel runat="server" ID="Panel7">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Trail</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlTrail" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px">
                                            <span class="labelClass">Mileage</span>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:TextBox ID="txtMileage" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 100px">
                                            <span class="labelClass"></span>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:CheckBox ID="cbProtected" CssClass="ChkBox" runat="server" Text="Protected" Checked="false" />
                                        </td>
                                        <td style="width: 170px"></td>
                                        <td>
                                            <asp:Button ID="btnAddTrailMileage" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddTrailMileage_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvTrailMileageGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel10" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvTrailMileage" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true" ShowFooter="false"
                                    OnRowEditing="gvTrailMileage_RowEditing"
                                    OnRowCancelingEdit="gvTrailMileage_RowCancelingEdit"
                                    OnRowUpdating="gvTrailMileage_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <FooterStyle CssClass="footerStyleTotals" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Conserve Trails" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConserveTrailsID" runat="Server" Text='<%# Eval("ConserveTrailsID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trail">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrailDescription" runat="Server" Text='<%# Eval("Description") %>' />
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:DropDownList ID="ddlTrail" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtLKTrail" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("LKTrail") %>' Visible="false">
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Miles">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMiles" runat="Server" Text='<%# Eval("Miles") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtMiles" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("Miles") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Protected">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkProtected" Enabled="false" runat="server" Checked='<%# Eval("Protected") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkProtected" runat="server" Checked='<%# Eval("Protected") %>' />
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
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" Visible='<%# GetIsVisibleBasedOnRole() %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvAllowedSpecialUses">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Allowed Special Uses</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAllowedSpecialUses" runat="server" Text="Add New Allowed Special Uses" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvNewAllowedSpecialUsesForm">
                            <asp:Panel runat="server" ID="Panel11">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Allowed Special Uses</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlAllowedSpecialUses" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px">
                                            <span class="labelClass"></span>
                                        </td>
                                        <td style="width: 180px"></td>
                                        <td style="width: 100px">
                                            <span class="labelClass"></span>
                                        </td>
                                        <td style="width: 180px"></td>
                                        <td style="width: 170px"></td>
                                        <td>
                                            <asp:Button ID="btnAddAllowedSpecialUses" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddAllowedSpecialUses_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvAllowedSpecialUsesGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel12" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvAllowedSpecialUses" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true" ShowFooter="false"
                                    OnRowEditing="gvAllowedSpecialUses_RowEditing"
                                    OnRowCancelingEdit="gvAllowedSpecialUses_RowCancelingEdit"
                                    OnRowUpdating="gvAllowedSpecialUses_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <FooterStyle CssClass="footerStyleTotals" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ConserveTrailsUsageId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConserveTrailsUsageId" runat="Server" Text='<%# Eval("ConserveTrailsUsageId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Allowed Special Uses">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAllowedSpecialUses" runat="Server" Text='<%# Eval("Description") %>' />
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
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" Visible='<%# GetIsVisibleBasedOnRole() %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                        <asp:ImageButton ID="ImgSurfaceWaters" ImageUrl="~/Images/print.png" ToolTip="Surface Waters Report"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgSurfaceWaters_Click" />
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
                                            <asp:DropDownList ID="ddlWatershed" CssClass="clsDropDown" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 166px">
                                            <span class="labelClass">Subwatershed</span>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:DropDownList ID="ddlSubwatershed" CssClass="clsDropDown" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Water Body</span></td>
                                        <td>
                                            <asp:DropDownList ID="ddlWaterBody" CssClass="clsDropDown" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Frontage (ft):</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="txtFrontageFeet" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                                        <td style="width: 166px"><span class="labelClass">Other Stream/Pond Name</span></td>
                                        <td style="width: 180px">
                                            <asp:TextBox ID="txtOtherStream" CssClass="clsTextBoxBlue" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 170px">
                                            <span class="labelClass">Riparian Buffer (length in ft.):</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRiparianBuffer" CssClass="clsTextBoxBlue" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 140px">
                                            <asp:CheckBox ID="cbActive" CssClass="ChkBox" runat="server" Text="Active" Checked="true" Enabled="false" />
                                        </td>
                                        <td style="width: 215px">
                                            <asp:Button ID="btnAddSurfaceWaters" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddSurfaceWaters_Click" />
                                        </td>
                                        <td style="width: 166px"></td>
                                        <td style="width: 180px"></td>
                                        <td style="width: 170px"></td>
                                        <td></td>
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
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvNewWatershed">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Watershed</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddWatershed" runat="server" Text="Add New Watershed" />
                                        <asp:ImageButton ID="ImgWatershed" ImageUrl="~/Images/print.png" ToolTip="Watershed Report"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgWatershed_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvNewWatershedForm">
                            <asp:Panel runat="server" ID="Panel5">
                                <table style="width: 100%">
                                    <tr>
                                        <td><span class="labelClass">Watershed</span></td>
                                        <td>
                                            <asp:DropDownList ID="ddlWaterShedNew" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWaterShedNew_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <span class="labelClass">Sub-Watershed (HUC8)</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubWatershedNew" CssClass="clsDropDown" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="cbWatershedActive" CssClass="ChkBox" runat="server" Text="Active" Checked="true" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="labelClass">HUC-12</span>
                                        </td>
                                        <td>
                                            <div class="scroll_checkboxes">
                                                <asp:CheckBoxList Width="180px" ID="cblHUC12" runat="server" RepeatDirection="Vertical" RepeatColumns="1" BorderWidth="0"
                                                    Datafield="description" DataValueField="value" CssClass="checkboxlist_nowrap">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td><span class="labelClass">Sort By</span></td>
                                        <td>
                                            <asp:RadioButtonList ID="rdHUC12order" runat="server" Width="150px" AutoPostBack="True" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rdHUC12order_SelectedIndexChanged">
                                                <asp:ListItem>HUC12</asp:ListItem>
                                                <asp:ListItem>Name</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnWatershed" runat="server" Text="Add" class="btn btn-info" OnClick="btnWatershed_Click" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvWatershedGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel6" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvWatershed" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvWatershed_RowEditing"
                                    OnRowUpdating="gvWatershed_RowUpdating"
                                    OnRowCancelingEdit="gvWatershed_RowCancelingEdit">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ConserveWatershedID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConserveWatershedID" runat="Server" Text='<%# Eval("ConserveWatershedID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="conserveHUCId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblconserveHUCId" runat="Server" Text='<%# Eval("conserveHUCId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Watershed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwatershed" runat="Server" Text='<%# Eval("WatershedDesc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Watershed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubWatershed" runat="Server" Text='<%# Eval("WatershedSubDesc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HUC-12">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHUC12" runat="Server" Text='<%# Eval("HUC12Name") %>' />
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
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>


                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvFarmProducts">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Farm Products</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddFarmProducts" runat="server" Text="Add New Farm Products" />
                                        <asp:ImageButton ID="ImgFarmProducts" ImageUrl="~/Images/print.png" ToolTip="Farm Products Report"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgFarmProducts_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="panel-body" runat="server" id="dvFarmProductsForm">
                            <asp:Panel runat="server" ID="Panel14">
                                <table style="width: 100%">
                                   <tr>
                                        <td style="width: 140px"><span class="labelClass">Products</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlFormProducts" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px">
                                            <span class="labelClass">Acres</span>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:TextBox ID="txtProductAcres" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 100px">
                                            <span class="labelClass"></span>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:CheckBox ID="cbOrganic" CssClass="ChkBox" runat="server" Text="Organic" Checked="false" />
                                        </td>
                                        <td style="width: 170px"></td>
                                        <td>
                                            <asp:Button ID="btnFarmProducts" runat="server" Text="Add" class="btn btn-info" OnClick="btnFarmProducts_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvFarmProductsGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel13" Width="100%" Height="100px" ScrollBars="Vertical">
                               <asp:GridView ID="gvFarmProducts" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true" ShowFooter="false"
                                    OnRowEditing="gvFarmProducts_RowEditing"
                                    OnRowCancelingEdit="gvFarmProducts_RowCancelingEdit"
                                    OnRowUpdating="gvFarmProducts_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <FooterStyle CssClass="footerStyleTotals" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Conserve Products" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConserveProductID" runat="Server" Text='<%# Eval("ConserveProductID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductDescription" runat="Server" Text='<%# Eval("Description") %>' />
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:DropDownList ID="ddlTrail" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtLKTrail" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("LKTrail") %>' Visible="false">
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
                                        <asp:TemplateField HeaderText="Organic">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkOrganic" Enabled="false" runat="server" Checked='<%# Eval("Organic") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkOrganic" runat="server" Checked='<%# Eval("Organic") %>' />
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
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" Visible='<%# GetIsVisibleBasedOnRole() %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
    <asp:HiddenField ID="hfIsVisibleBasedOnRole" runat="server" />
    <asp:HiddenField ID="hfConserveWatershedID" runat="server" />

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

            $('#<%= dvNewTrailMileageForm.ClientID%>').toggle($('#<%= cbAddTrailMileage.ClientID%>').is(':checked'));

            $('#<%= cbAddTrailMileage.ClientID%>').click(function () {
                $('#<%= dvNewTrailMileageForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvNewAllowedSpecialUsesForm.ClientID%>').toggle($('#<%= cbAllowedSpecialUses.ClientID%>').is(':checked'));

            $('#<%= cbAllowedSpecialUses.ClientID%>').click(function () {
                $('#<%= dvNewAllowedSpecialUsesForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvNewSurfaceWatersForm.ClientID%>').toggle($('#<%= cbAddSurfaceWaters.ClientID%>').is(':checked'));

            $('#<%= cbAddSurfaceWaters.ClientID%>').click(function () {
                $('#<%= dvNewSurfaceWatersForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvNewWatershedForm.ClientID%>').toggle($('#<%= cbAddWatershed.ClientID%>').is(':checked'));
            $('#<%= cbAddWatershed.ClientID%>').click(function () {
                $('#<%= dvNewWatershedForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvFarmProductsForm.ClientID%>').toggle($('#<%= cbAddFarmProducts.ClientID%>').is(':checked'));
            $('#<%= cbAddFarmProducts.ClientID%>').click(function () {
                $('#<%= dvFarmProductsForm.ClientID%>').toggle(this.checked);
            }).change();


<%--             $('#<%= txtTillable.ClientID%>').keyup(function () {
                $('#<%=txtTillable.ClientID%>').val($('#<%=txtTillable.ClientID%>').getNum());
             });--%>

            var txtboxs = $('#<%= txtTillable.ClientID%>,#<%= txtPasture.ClientID%>,#<%= txtWooded.ClientID%>,#<%= txtUnManaged.ClientID%>,#<%= txtFarmResident.ClientID%>,#<%= txtPrime.ClientID%>,#<%= txtStateWide.ClientID%>, #<%= txtNaturalRec.ClientID%>, #<%= txtSugarbush.ClientID%>');
            $.each(txtboxs, function () {
                $(this).blur(function () {
                    $('#<%=txtTillable.ClientID%>').val($('#<%=txtTillable.ClientID%>').getNum());
                    $('#<%=txtPasture.ClientID%>').val($('#<%=txtPasture.ClientID%>').getNum());
                    $('#<%=txtPrime.ClientID%>').val($('#<%=txtPrime.ClientID%>').getNum());
                    $('#<%=txtPasture.ClientID%>').val($('#<%=txtPasture.ClientID%>').getNum());
                    $('#<%=txtFarmResident.ClientID%>').val($('#<%=txtFarmResident.ClientID%>').getNum());
                    $('#<%=txtStateWide.ClientID%>').val($('#<%=txtStateWide.ClientID%>').getNum());
                    $('#<%=txtNaturalRec.ClientID%>').val($('#<%=txtNaturalRec.ClientID%>').getNum());
                    $('#<%=txtWooded.ClientID%>').val($('#<%=txtWooded.ClientID%>').getNum());
                    $('#<%=txtSugarbush.ClientID%>').val($('#<%=txtSugarbush.ClientID%>').getNum());
                    $('#<%=txtUnManaged.ClientID%>').val($('#<%=txtUnManaged.ClientID%>').getNum());
                    CalculatePercentages();
                });
            });

            function CalculatePercentages() {

                var totTillable = (isNaN(parseFloat($('#<%=txtTillable.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtTillable.ClientID%>').val(), 10));
                var totPasture = (isNaN(parseFloat($('#<%=txtPasture.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtPasture.ClientID%>').val(), 10));
                var totWooded = (isNaN(parseFloat($('#<%=txtWooded.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtWooded.ClientID%>').val(), 10));
                var totUnManaged = (isNaN(parseFloat($('#<%=txtUnManaged.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtUnManaged.ClientID%>').val(), 10));
                var totFarmResident = (isNaN(parseFloat($('#<%=txtFarmResident.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtFarmResident.ClientID%>').val(), 10));
                var totNaturalRec = (isNaN(parseFloat($('#<%=txtNaturalRec.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtNaturalRec.ClientID%>').val(), 10));
                var totSugarbush = (isNaN(parseFloat($('#<%=txtSugarbush.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtSugarbush.ClientID%>').val(), 10));

                var Total = totTillable + totPasture + totWooded + totUnManaged + totFarmResident + totNaturalRec;
                console.log('Total:' + Total.toFixed(2));


                $('#<%=spnTotalProject.ClientID%>').text(Total.toFixed(2));

                $('#<%=pctPrimeStateWide.ClientID%>').text('-');

                if (Total != 0) {
                    var totPrime = (isNaN(parseFloat($('#<%=txtPrime.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtPrime.ClientID%>').val(), 10));
                    var totStateWide = (isNaN(parseFloat($('#<%=txtStateWide.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtStateWide.ClientID%>').val(), 10));

                    var totPS = totPrime + totStateWide;
                    var pctPS = Math.round(totPS * 100 / Total);
                  
                    $('#<%=pctPrimeStateWide.ClientID%>').text(pctPS + ' %');

                    //console.log('Total is: '  + Total);
                    
        
                    var pctWooded = Math.round($('#<%=txtWooded.ClientID%>').val() * 100 / Total);
                    //console.log('pctWooded: ' + pctWooded);
                    //console.log(pctWooded.toPrecision(2) + ' %');
                   $('#<%=pctWooded.ClientID%>').text(pctWooded.toPrecision(2) + ' %');
                   // $('#<%=pctWooded.ClientID%>').text(pctWooded + ' %');
                }


                console.log(totWooded);
                console.log(totSugarbush);

                if (totWooded != 0) {
                    var pctSugarBush = Math.round($('#<%=txtSugarbush.ClientID%>').val() * 100 / totWooded);
                    $('#<%=pctSugarBush.ClientID%>').text(pctSugarBush + ' %');
                } else {
                    $('#<%=pctSugarBush.ClientID%>').text(' 0.0 %');
                }
            };
            <%--$('#<%= txtTotProjAcres.ClientID%>').blur(function () {
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


                //var pctWooded = parseFloat(parseInt($('#<%=txtWooded.ClientID%>').val(), 10) * 100) / parseInt($('#<%=txtTotProjAcres.ClientID%>').val(), 10);
                var pctWooded = Math.round($('#<%=txtWooded.ClientID%>').val() * 100 / $('#<%=txtTotProjAcres.ClientID%>').val());
                var pctPrime = Math.round($('#<%=txtPrime.ClientID%>').val() * 100 / $('#<%=txtTotProjAcres.ClientID%>').val());
                var pctState = Math.round($('#<%=txtStateWide.ClientID%>').val() * 100 / $('#<%=txtTotProjAcres.ClientID%>').val());

                if ($('#<%=txtTotProjAcres.ClientID%>').val() != 0) {
                    //$('#<%=pctWooded.ClientID%>').text(pctWooded.toPrecision(4));
                    $('#<%=pctWooded.ClientID%>').text(pctWooded.toPrecision(2));
                    $('#<%=pctPrime.ClientID%>').text(pctPrime.toPrecision(2));
                    $('#<%=pctState.ClientID%>').text(pctState.toPrecision(2));

                    var Other = txttot - (txtwooded + txtprime + txtstate);
                    $('#<%=otherAcres.ClientID%>').text(Other);
                }
                else {
                    $('#<%=txtWooded.ClientID%>').val(0)
                    $('#<%=txtPrime.ClientID%>').val(0)
                    $('#<%=txtStateWide.ClientID%>').val(0)

                    $('#<%=pctWooded.ClientID%>').text("-");
                    $('#<%=pctPrime.ClientID%>').text("-");
                    $('#<%=pctState.ClientID%>').text("-");
                    $('#<%=otherAcres.ClientID%>').text("-");
                }
            };--%>
        });

        function PopupAwardSummary() {
            window.open('../awardsummary.aspx?projectid=' + $('#<%=hfProjectId.ClientID%>').val());
        };

        jQuery.fn.getNum = function () {
            var val = $.trim($(this).val());
            if (val.indexOf(',') > -1) {
                val = val.replace(',', '.');
            }
            var num = parseFloat(val);
            var num = num.toFixed(2);
            if (isNaN(num)) {
                num = '';
            }
            return num;
        }
    </script>
</asp:Content>
