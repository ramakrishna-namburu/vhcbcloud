﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectLeadBuildings.aspx.cs" MasterPageFile="~/Site.Master"
    Inherits="vhcbcloud.Lead.ProjectLeadBuildings" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="EventContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .scroll_checkboxes {
            height: 150px;
            padding: 5px;
            overflow: auto;
            border: 1px solid #ccc;
        }


        .auto-style1 {
            width: 94px;
        }

        .auto-style2 {
            width: 404px;
        }


        .auto-style3 {
            width: 150px;
            height: 26px;
        }

        .auto-style4 {
            width: 250px;
            height: 26px;
        }

        .auto-style5 {
            width: 104px;
            height: 52px;
        }

        .auto-style6 {
            width: 70px;
            height: 52px;
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
                            <td style="width: 171px"><span class="labelClass">Project #:</span></td>
                            <td style="width: 192px"><span class="labelClass" id="ProjectNum" runat="server"></span></td>
                            <td><span class="labelClass">Name:</span></td>
                            <td style="text-align: left"><span class="labelClass" id="ProjName" runat="server"></span></td>
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

                <div class="panel-width" runat="server" id="dvNewBldgInfo">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Building Info</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddBldgInfo" runat="server" Text="Add New Building Info" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvBldgInfoForm">
                            <asp:Panel runat="server" ID="Panel2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Bldg #:</span></td>
                                        <td style="width: 250px">
                                            <asp:TextBox ID="txtBldgnumber" CssClass="clsTextBoxBlueSm" runat="server" MaxLength="2"></asp:TextBox>
                                        </td>
                                        <td style="width: 185px">
                                            <span class="labelClass">Address:</span>
                                        </td>
                                        <td style="width: 270px">
                                            <asp:DropDownList ID="ddlAddress" CssClass="clsDropDownLong" runat="server" Style="margin-left: 1"></asp:DropDownList>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Age:</span></td>
                                        <td>
                                            <%--<asp:TextBox ID="txtAge" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlBldgAge" CssClass="clsDropDownLong" runat="server" Style="margin-left: 1"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Type:</span></td>
                                        <td style="width: 250px">
                                            <asp:DropDownList ID="ddlType" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 185px">
                                            <span class="labelClass">Enrolled LHC Units:</span>
                                        </td>
                                        <td style="width: 270px">
                                            <asp:TextBox ID="txtLHCUnits" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Flood Hazard Area:</span></td>
                                        <td>
                                            <asp:CheckBox ID="cbFloodHazardArea" CssClass="ChkBox" runat="server" Text="Yes" Checked="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Flood Insurance:</span></td>
                                        <td style="width: 250px">
                                            <asp:CheckBox ID="cbFloodInsurance" CssClass="ChkBox" runat="server" Text="Yes" Checked="false" />
                                        </td>
                                        <td style="width: 185px">
                                            <span class="labelClass">Verified by:</span>
                                        </td>
                                        <td style="width: 270px">
                                            <asp:DropDownList ID="ddlverifiedBy" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Insured by:</span></td>
                                        <td>
                                            <asp:TextBox ID="txtInsuredby" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Historic Status:</span></td>
                                        <td style="width: 250px">
                                            <asp:DropDownList ID="ddlHistoricStatus" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 185px"><span class="labelClass">Appendix A Determination:</span></td>
                                        <td style="width: 250px">
                                            <asp:DropDownList ID="ddlAppendixA" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                        </td>
                                        <td><span class="labelClass">Active:</span></td>
                                        <td>
                                            <asp:CheckBox ID="chkBldgActive" Enabled="false" runat="server" Checked="true" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                            <asp:Button ID="btnAddBldgInfoSubmit" runat="server" Text="Submit" class="btn btn-info" OnClick="btnAddBldgInfoSubmit_Click" />
                                        </td>
                                        <td style="width: 250px"></td>
                                        <td style="width: 185px"></td>
                                        <td style="width: 250px"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvBldgInfoGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel3" Width="100%" Height="200px" ScrollBars="Vertical">
                                <asp:GridView ID="gvBldgInfo" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false"
                                    OnRowEditing="gvBldgInfo_RowEditing" OnRowCancelingEdit="gvBldgInfo_RowCancelingEdit" OnRowDataBound="gvBldgInfo_RowDataBound"
                                    OnSelectedIndexChanged="gvBldgInfo_SelectedIndexChanged">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="LeadBldgID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadBldgID" runat="Server" Text='<%# Eval("LeadBldgID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdBtnSelectBldgInfo" runat="server" AutoPostBack="true" onclick="RadioCheck(this);"
                                                    OnCheckedChanged="rdBtnSelectBldgInfo_CheckedChanged" />
                                                <asp:HiddenField ID="HiddenLeadBldgID" runat="server" Value='<%#Eval("LeadBldgID")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Building">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuilding" runat="Server" Text='<%# Eval("Building") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="Server" Text='<%# Eval("Address") %>' />
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

                <div class="panel-width" runat="server" id="dvNewUnitInfo" visible="false">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Unit Info</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddUnitInfo" runat="server" Text="Add New unit Info" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvUnitInfoForm">
                            <asp:Panel runat="server" ID="Panel1">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Bldg #:</span></td>
                                        <td style="width: 250px">
                                            <span class="labelClass" id="BldgNumber" runat="server"><%= hfSelectedBuilding.Value %></span>
                                        </td>
                                        <td style="width: 185px">
                                            <span class="labelClass">Unit #:</span>
                                        </td>
                                        <td style="width: 270px">
                                            <asp:TextBox ID="txtUnitNumber" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">EBL Status:</span></td>
                                        <td>
                                            <asp:DropDownList ID="ddlEBlStatus" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Household Count:</span></td>
                                        <td style="width: 250px">
                                            <asp:TextBox ID="txtHouseholdCount" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 185px">
                                            <span class="labelClass"># Rooms:</span>
                                        </td>
                                        <td style="width: 270px">
                                            <asp:TextBox ID="txtRooms" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Household Income:</span></td>
                                        <td>
                                            <asp:TextBox ID="txtHouseholdIncome" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Matching Funds:</span>
                                        </td>
                                        <td style="width: 250px">
                                            <asp:TextBox ID="txtMatchingFund" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 185px">
                                            <span class="labelClass">3rd Party Verified:</span></td>
                                        <td style="width: 270px">
                                            <asp:CheckBox ID="cbThirdPartyVerified" CssClass="ChkBox" runat="server" Text="Yes" Checked="false" />
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Income Status:</span></td>
                                        <td>
                                            <asp:DropDownList ID="ddlIncomeStatus" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Certified By:</span></td>
                                        <td style="width: 250px">
                                            <asp:TextBox ID="txtCertifiedBy" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="ce_txtCertifiedBy" TargetControlID="txtCertifiedBy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td style="width: 185px"><span class="labelClass">6 Month Recertify:</span></td>
                                        <td style="width: 270px"><span class="labelClass" id="labelRectDate" runat="server"></span>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Clearance Date:</span></td>
                                        <td>
                                            <asp:TextBox ID="txtClearanceDate" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="ce_txtClearanceDate" TargetControlID="txtClearanceDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Relocation Amt</span></td>
                                        <td style="width: 250px">
                                            <asp:TextBox ID="txtRelocAmt" CssClass="clsTextBoxBlueSm" Width="100px" Height="22px" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 185px"><span class="labelClass">Start Date</span></td>
                                        <td style="width: 270px">
                                            <asp:TextBox ID="txtStartDate" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtStartDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td style="width: 170px"><span class="labelClass">Active</span></td>
                                        <td>
                                            <asp:CheckBox ID="chkUnitActive" Enabled="false" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"><span class="labelClass">Note: If unit # = 0 is “Common Area” and unit # =99 is “Exterior”</span></td>
                                    </tr>
                                     <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                            <asp:Button ID="btnAddUnitInfo" runat="server" Text="Submit" class="btn btn-info" OnClick="btnAddUnitInfo_Click" />
                                        </td>
                                        <td style="width: 250px"></td>
                                        <td style="width: 185px"></td>
                                        <td style="width: 250px"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvUnitInfoGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel4" Width="100%" Height="250px" ScrollBars="Vertical">
                                <asp:GridView ID="gvUnitInfo" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false"
                                    OnRowEditing="gvUnitInfo_RowEditing" OnRowCancelingEdit="gvUnitInfo_RowCancelingEdit" OnRowDataBound="gvUnitInfo_RowDataBound">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="LeadUnitID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadUnitID" runat="Server" Text='<%# Eval("LeadUnitID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdBtnSelectUnitInfo" runat="server" AutoPostBack="true"
                                                    onclick="RadioCheckUnitInfo(this);"
                                                    OnCheckedChanged="rdBtnSelectUnitInfo_CheckedChanged" />
                                                <asp:HiddenField ID="HiddenLeadUnitID" runat="server" Value='<%#Eval("LeadUnitID")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Building">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuilding" runat="Server" Text='<%# Eval("Building") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" runat="Server" Text='<%# Eval("Unit") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="# Rooms">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRooms" runat="Server" Text='<%# Eval("Rooms") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Clearance Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblClearDate" runat="Server" Text='<%# Eval("ClearDate") %>' />
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

                <div class="panel-width" runat="server" id="dvNewWorkLocation" visible="false">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Work Location</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddWorkLocation" runat="server" Text="Add New Work Location" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvWorkLocationForm">
                            <asp:Panel runat="server" ID="Panel7">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 150px"><span class="labelClass">Work Location</span></td>
                                        <td style="width: 250px">
                                            <asp:DropDownList ID="ddlWorkLocation" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                        </td>
                                         <td style="width: 150px"><span class="labelClass">Order#</span></td>
                                        <td style="width: 250px">
                                           <asp:TextBox ID="txtOrder" runat="Server" CssClass="clsTextBoxBlueSm"  Visible="true" MaxLength="2"></asp:TextBox>
                                        </td>
                                        <td style="width: 104px">
                                            <span class="labelClass">Active</span>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:CheckBox ID="cbWorkLocationActive" Enabled="false" runat="server" Checked="true" />
                                        </td>
                                        <td style="width: 170px"><span class="labelClass"></span></td>
                                        <td>
                                            <asp:Button ID="btnAddWorkLocation" runat="server" Text="Submit" class="btn btn-info" OnClick="btnAddWorkLocation_Click" Style="margin-left: 0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvWorkLocationGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel8" Width="100%" Height="250px" ScrollBars="Vertical">
                                <asp:GridView ID="gvWorkLocationGrid" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false"
                                    OnRowCancelingEdit="gvWorkLocationGrid_RowCancelingEdit"
                                    OnRowEditing="gvWorkLocationGrid_RowEditing"
                                    OnRowUpdating="gvWorkLocationGrid_RowUpdating"
                                    OnRowDataBound="gvWorkLocationGrid_RowDataBound">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="WorkLocationID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorkLocationID" runat="Server" Text='<%# Eval("WorkLocationID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdBtnSelectWorkLocation" runat="server" AutoPostBack="true"
                                                    onclick="RadioCheckWorkLocation(this);"
                                                    OnCheckedChanged="rdBtnSelectWorkLocation_CheckedChanged" />
                                                <asp:HiddenField ID="HiddenWorkLocationID" runat="server" Value='<%#Eval("WorkLocationID")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrdernum" runat="Server" Text='<%# Eval("Ordernum") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>  
                                                <asp:TextBox ID="txtOrdernum" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("Ordernum") %>' Visible="true"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorkLocation" runat="Server" Text='<%# Eval("WorkLocation") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlWorkLocation" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtLocationID" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("LocationID") %>' Visible="false"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Location Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocationDescription" runat="Server" Text='<%# Eval("LocationDesc") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>  
                                                <asp:TextBox ID="txtLocationDesc" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("LocationDesc") %>' Visible="true"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
 
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActiveEditWorkLocation" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" Visible="true"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvNewLeadTypeofWork" visible="false">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Location Specs</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddTypeOfWork" runat="server" Text="Add New Location Specs" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" runat="server" id="dvTypeOfWorkForm">
                            <asp:Panel runat="server" ID="Panel5">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="auto-style3"><span class="labelClass">Category</span></td>
                                        <td class="auto-style4">
                                            <asp:DropDownList ID="ddlCategory" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><span class="labelClass">Specs</span></td>
                                        <td class="auto-style2">
                                            <div class="scroll_checkboxes">
                                                <asp:CheckBoxList Width="700px" ID="cblSpec" runat="server" RepeatDirection="Vertical" RepeatColumns="1" BorderWidth="0"
                                                    Datafield="description" DataValueField="value" CssClass="checkboxlist_nowrap">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <span class="labelClass">Active</span>
                                        </td>
                                        <td class="auto-style6">
                                            <asp:CheckBox ID="chkTypeOfWorkActive" Enabled="false" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px"><span class="labelClass"></span></td>
                                        <td>
                                            <asp:Button ID="btnAddSpec" runat="server" Text="Submit" class="btn btn-info" OnClick="btnAddSpec_Click" Style="margin-left: 0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" runat="server" id="dvSpecDetails" visible="false">
                            <asp:Panel runat="server" ID="Panel9">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="auto-style3"><span class="labelClass">Spec Details</span></td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="txtSpecDetails" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="600px" Height="80px" />
                                        </td>
                                    </tr>
                                      <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><span class="labelClass">Spec Notes</span></td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtSpecNotes" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="600px" Height="80px" />
                                        </td>
                                    </tr>
                                      <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><span class="labelClass">Units</span></td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtUnits" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><span class="labelClass">Unit Cost</span></td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtUnitCost" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><span class="labelClass">Order#</span></td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtSpecOrder" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <span class="labelClass">Active</span>
                                        </td>
                                        <td class="auto-style6">
                                            <asp:CheckBox ID="cbSpecActive" Enabled="true" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                      <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px"><span class="labelClass"></span></td>
                                        <td>
                                            <asp:Button ID="btnUpdateSpecDetails" runat="server" Text="Update" class="btn btn-info" OnClick="btnUpdateSpecDetails_Click" Style="margin-left: 0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" id="dvTypeOfWorkGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel6" Width="100%" Height="300px" ScrollBars="Vertical">
                                <asp:GridView ID="gvTypeOfWork" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false"
                                    OnRowCancelingEdit="gvTypeOfWork_RowCancelingEdit"
                                    OnRowEditing="gvTypeOfWork_RowEditing"
                                    OnRowUpdating="gvTypeOfWork_RowUpdating"
                                    OnRowDataBound="gvTypeOfWork_RowDataBound">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProjectLeadSpecID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectLeadSpecID" runat="Server" Text='<%# Eval("ProjectLeadSpecID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrdernum" runat="Server" Text='<%# Eval("Ordernum") %>' />
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>  
                                                <asp:TextBox ID="txtOrdernum" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("Ordernum") %>' Visible="true"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spec Detail">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecDetail" runat="Server" Text='<%# Eval("Spec_Detail") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spec Note">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecNote" runat="Server" Text='<%# Eval("Spec_Note") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Items">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnits" runat="Server" Text='<%# Eval("Units") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Per Item Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitCost" runat="Server" Text='<%# Eval("UnitCost", "{0:C2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <%--<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>--%>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" Visible="true"></asp:LinkButton>
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
    <asp:HiddenField ID="hfLeadBldgID" runat="server" />
    <asp:HiddenField ID="hfLeadUnitID" runat="server" />
    <asp:HiddenField ID="hfWorkLocationID" runat="server" />
    <asp:HiddenField ID="hfSelectedBuilding" runat="server" />
    <asp:HiddenField ID="hfIsVisibleBasedOnRole" runat="server" />
    <asp:HiddenField ID="hfProjectLeadSpecID" runat="server" />
    <script language="javascript">
        $(document).ready(function () {
            if ($('#<%= txtRelocAmt.ClientID%>').val() >= 0) {
                toCurrencyControl($('#<%= txtRelocAmt.ClientID%>').val(), $('#<%= txtRelocAmt.ClientID%>'));
            }
            $('#<%= txtRelocAmt.ClientID%>').keyup(function () {
                toCurrencyControl($('#<%= txtRelocAmt.ClientID%>').val(), $('#<%= txtRelocAmt.ClientID%>'));
            });

            $('#<%= dvBldgInfoForm.ClientID%>').toggle($('#<%= cbAddBldgInfo.ClientID%>').is(':checked'));
            $('#<%= cbAddBldgInfo.ClientID%>').click(function () {
                $('#<%= dvBldgInfoForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvUnitInfoForm.ClientID%>').toggle($('#<%= cbAddUnitInfo.ClientID%>').is(':checked'));
            $('#<%= cbAddUnitInfo.ClientID%>').click(function () {
                $('#<%= dvUnitInfoForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvTypeOfWorkForm.ClientID%>').toggle($('#<%= cbAddTypeOfWork.ClientID%>').is(':checked'));
            $('#<%= cbAddTypeOfWork.ClientID%>').click(function () {
                $('#<%= dvTypeOfWorkForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvWorkLocationForm.ClientID%>').toggle($('#<%= cbAddWorkLocation.ClientID%>').is(':checked'));
            $('#<%= cbAddWorkLocation.ClientID%>').click(function () {
                $('#<%= dvWorkLocationForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= txtCertifiedBy.ClientID%>').blur(function () {
                GetRecertifyDate();
               <%-- var dt = new Date($('#<%= txtCertifiedBy.ClientID%>').val());
                console.log(dt);
                dt.setMonth(dt.getMonth() + 6);
                console.log(dt.getMonth());
                var month;
                if (dt.getMonth() == 0)
                    month = 12;
                else
                    month = dt.getMonth();

                $('#<%=labelRectDate.ClientID%>').html(month + '/' + dt.getDate() + '/' + dt.getFullYear());--%>
            });

            function GetRecertifyDate() {
                $.ajax({
                    type: "POST",
                    url: "ProjectLeadBuildings.aspx/GetRecertifyDate",
                    data: '{CertifiedDate: "' + $("#<%= txtCertifiedBy.ClientID%>").val() + '" }',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var RecertifyDate = JSON.stringify(data.d);
                        console.log('RecertifyDate :' + RecertifyDate);
                        $('#<%=labelRectDate.ClientID%>').html(RecertifyDate.replace('"', '').replace('"', ''));

                    },
                    error: function (data) {
                        alert("error found");
                    }
                });
            }

            toCurrencyControl($('#<%= txtUnitCost.ClientID%>').val(), $('#<%= txtUnitCost.ClientID%>'));
            $('#<%= txtUnitCost.ClientID%>').keyup(function () {
                toCurrencyControl($('#<%= txtUnitCost.ClientID%>').val(), $('#<%= txtUnitCost.ClientID%>'));
            });

        });

        function PopupAwardSummary() {
            window.open('../awardsummary.aspx?projectid=' + $('#<%=hfProjectId.ClientID%>').val());
        };

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=gvBldgInfo.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }

        function RadioCheckUnitInfo(rb) {
            var gv = document.getElementById("<%=gvUnitInfo.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }


        function RadioCheckWorkLocation(rb) {
            var gv = document.getElementById("<%=gvWorkLocationGrid.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }

    </script>
</asp:Content>
