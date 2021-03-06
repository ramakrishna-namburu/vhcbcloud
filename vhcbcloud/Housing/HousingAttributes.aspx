﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HousingAttributes.aspx.cs" MasterPageFile="~/Site.Master"
    Inherits="vhcbcloud.Housing.HousingAttributes" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="EventContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
.scroll_checkboxes {
    height: 100px;
    
    padding: 5px;
    overflow: auto;
    border: 1px solid #ccc;
}

.FormText {
    FONT-SIZE: 11px;
    FONT-FAMILY: tahoma,sans-serif;
}
.checkboxlist_nowrap label
{
     white-space:nowrap;
     display:inline-block;
}
      
    </style>

    <div class="jumbotron" id="vhcb">
        <!-- Tabs   -->
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
                            <td style="width: 171px"><span class="labelClass">Project #</span></td>
                            <td style="width: 192px">
                                <span class="labelClass" id="ProjectNum" runat="server"></span>
                            </td>
                            <td><span class="labelClass">Name:</span></td>
                            <td style="text-align: left">
                                <span class="labelClass" id="ProjName" runat="server"></span>
                            </td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="imgSearch" ImageUrl="~/Images/search.png" ToolTip="Project Search" 
                                    Style="border: none; vertical-align: middle;" runat="server" Text="Project Search"
                                    OnClientClick="window.location.href='../ProjectSearch.aspx'; return false;"></asp:ImageButton>
                                <asp:ImageButton ID="ibAwardSummary" runat="server" ImageUrl="~/Images/$$.png" Text="Award Summary" 
                                    Style="border: none; vertical-align: middle;"
                                    OnClientClick="PopupAwardSummary(); return false;"></asp:ImageButton>
                                <asp:ImageButton ID="btnProjectNotes" runat="server" ImageUrl="~/Images/notes.png" Text="Project Notes" 
                                    Style="border: none; vertical-align: middle;" />
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

                <div class="panel-width" runat="server" id="dvNewAttribute">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Consolidated Plan Priorities</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddCPP" runat="server" Text="Add New Consolidated Plan Priorities" />
                                        <asp:ImageButton ID="ImgConsolidated" ImageUrl="~/Images/print.png" ToolTip="Housing Consolidated Plan Priorities"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgConsolidated_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvCPPForm">
                            <asp:Panel runat="server" ID="Panel8">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 100px"><span class="labelClass">Priorities</span></td>
                                        <td colspan="3">
                                           <%-- <asp:DropDownList ID="ddlPriorities" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>--%>
                                             <div class="scroll_checkboxes">
                                                <asp:CheckBoxList Width="180px" ID="cblPriorities" runat="server" RepeatDirection="Vertical" RepeatColumns="1" BorderWidth="0" 
                                                    Datafield="description" DataValueField="value"  CssClass="checkboxlist_nowrap">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="width: 60px">&nbsp;<asp:Button ID="AddPriorities" runat="server" Text="Add" class="btn btn-info" OnClick="AddPriorities_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvCPPFormGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel9" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvCPPForm" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvCPPForm_RowEditing" OnRowCancelingEdit="gvCPPForm_RowCancelingEdit"
                                    OnRowUpdating="gvCPPForm_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProjectConPlanPrioritiesID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectConPlanPrioritiesID" runat="Server" Text='<%# Eval("ProjectConPlanPrioritiesID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priorities">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriorities" runat="Server" Text='<%# Eval("Priorities") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                            <ItemStyle Width="350px" />
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

                <div class="panel-width" runat="server" id="dvNewInterAgency">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Interagency Priorities</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddInterAgency" runat="server" Text="Add New Interagency Priorities" />
                                        <asp:ImageButton ID="ImgInteragency" ImageUrl="~/Images/print.png" ToolTip="Housing Interagency Priorities"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgInteragency_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvInterAgencyForm">
                            <asp:Panel runat="server" ID="Panel1">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Priorities</span></td>
                                        <td style="width: 215px">
                                            <%--<asp:DropDownList ID="ddlInterAgencyPriorities" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>--%>
                                             <div class="scroll_checkboxes">
                                                <asp:CheckBoxList Width="180px" ID="cblInterAgencyPriorities" runat="server" RepeatDirection="Vertical" RepeatColumns="1" BorderWidth="0" 
                                                    Datafield="description" DataValueField="value"  CssClass="checkboxlist_nowrap">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="btnAddInterAgency" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddInterAgency_Click" />
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

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvInterAgencyGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel2" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvInterAgency" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvInterAgency_RowEditing" OnRowCancelingEdit="gvInterAgency_RowCancelingEdit"
                                    OnRowUpdating="gvInterAgency_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProjectInteragencyID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectInteragencyID" runat="Server" Text='<%# Eval("ProjectInteragencyID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priorities">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriorities" runat="Server" Text='<%# Eval("Priorities") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                            <ItemStyle Width="350px" />
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

                <div class="panel-width" runat="server" id="dvNewVHCB">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">VHCB Priorities/Outcomes</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddVHCB" runat="server" Text="Add New VHCB Priorities/Outcomes" />
                                        
                                        <asp:ImageButton ID="ImgVHCBpriorities" ImageUrl="~/Images/print.png" ToolTip="Housing VHCB Priorities/Outcomes"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgVHCBpriorities_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvVHCBForm">
                            <asp:Panel runat="server" ID="Panel3">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Priorities/Outcomes</span></td>
                                        <td class="auto-style1">
                                           <%-- <asp:DropDownList ID="ddlVHCBPriorities" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>--%>
                                             <div class="scroll_checkboxes">
                                                <asp:CheckBoxList Width="180px" ID="cblVHCBPriorities" runat="server" RepeatDirection="Vertical" RepeatColumns="1" BorderWidth="0" 
                                                    Datafield="description" DataValueField="value"  CssClass="checkboxlist_nowrap">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 100px">
                                            <asp:Button ID="btnAddVHCB" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddVHCB_Click" />
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

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvVHCBGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel4" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvVHCB" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvVHCB_RowEditing" OnRowCancelingEdit="gvVHCB_RowCancelingEdit"
                                    OnRowUpdating="gvVHCB_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProjectVHCBPrioritiesID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectVHCBPrioritiesID" runat="Server" Text='<%# Eval("ProjectVHCBPrioritiesID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priorities">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriorities" runat="Server" Text='<%# Eval("Priorities") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                            <ItemStyle Width="350px" />
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

               <%-- <div class="panel-width" runat="server" id="dvNewOther">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Other Outcomes</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddOther" runat="server" Text="Add New Other Outcomes" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvOtherForm">
                            <asp:Panel runat="server" ID="Panel5">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Outcomes</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlOutcomes" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="btnAddOutcomes" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddOutcomes_Click" />
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

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvOtherGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel6" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvOther" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvOther_RowEditing" OnRowCancelingEdit="gvOther_RowCancelingEdit" 
                                    OnRowUpdating="gvOther_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProjectOtherOutcomesID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectOtherOutcomesID" runat="Server" Text='<%# Eval("ProjectOtherOutcomesID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priorities">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriorities" runat="Server" Text='<%# Eval("Priorities") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                            <ItemStyle Width="350px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfProjectId" runat="server" />
    <asp:HiddenField ID="hfIsVisibleBasedOnRole" runat="server" />

    <script language="javascript">
        $(document).ready(function () {
            $('#<%= dvCPPForm.ClientID%>').toggle($('#<%= cbAddCPP.ClientID%>').is(':checked'));
            $('#<%= cbAddCPP.ClientID%>').click(function () {
                $('#<%= dvCPPForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvInterAgencyForm.ClientID%>').toggle($('#<%= cbAddInterAgency.ClientID%>').is(':checked'));
            $('#<%= cbAddInterAgency.ClientID%>').click(function () {
                $('#<%= dvInterAgencyForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvVHCBForm.ClientID%>').toggle($('#<%= cbAddVHCB.ClientID%>').is(':checked'));
            $('#<%= cbAddVHCB.ClientID%>').click(function () {
                $('#<%= dvVHCBForm.ClientID%>').toggle(this.checked);
            }).change();

           <%-- $('#<%= dvOtherForm.ClientID%>').toggle($('#<%= cbAddOther.ClientID%>').is(':checked'));
            $('#<%= cbAddOther.ClientID%>').click(function () {
                $('#<%= dvOtherForm.ClientID%>').toggle(this.checked);
            }).change();--%>
            
        });
        function PopupAwardSummary() {
            window.open('../awardsummary.aspx?projectid=' + $('#<%=hfProjectId.ClientID%>').val());
        };
    </script>
</asp:Content>
