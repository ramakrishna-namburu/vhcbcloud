<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WQSummary.aspx.cs" MasterPageFile="~/Site.Master" Inherits="vhcbcloud.WaterQuality.WQSummary" MaintainScrollPositionOnPostback="true" %>


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
                                    <td><span class="labelClass">Tactical Basin:</span>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlTacticalBasin" CssClass="clsDropDown" runat="server" Height="23px" Width="185px">
                                        </asp:DropDownList>

                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Project Type:</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlProjectType" CssClass="clsDropDown" runat="server" Height="23px" Width="185px" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td><span class="labelClass">Step/Phase:</span></td>
                                    <td>
                                        <span class="labelClass" id="spnStepPhase" runat="server"></span>
                                    </td>
                                    <td><span class="labelClass">Total Acres:</span></td>
                                    <td>
                                        <asp:TextBox ID="txtAcres" CssClass="clsTextBoxBlue1" runat="server" Width="49px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td><span class="labelClass">Phosphorus Reduction (kg):</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPhosphorus" CssClass="clsTextBoxBlue1" runat="server" Width="54px"></asp:TextBox>

                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-info" OnClick="btnSubmit_Click" />
                                    </td>
                                </tr>

                            </table>
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
                                            <span class="labelClass">Lake Memphremagog</span>
                                        </td>
                                        <td>
                                            <span class="labelClass">Sub-Watershed (HUC8)</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubWatershedNew" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubWatershedNew_SelectedIndexChanged">
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
                                             <asp:DropDownList ID="ddlHUC12" CssClass="clsDropDown" runat="server" Visible="false">
                                            </asp:DropDownList>
                                             <span class="labelClass" runat="server" id="spnHUC12" visible="false"></span>
                                        </td>
                                        <td></td>
                                        <td>
                                          
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
                                        <asp:TemplateField HeaderText="WQWatershedID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWQWatershedID" runat="Server" Text='<%# Eval("WQWatershedID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="conserveHUCId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblconserveHUCId" runat="Server" Text='<%# Eval("conserveHUCId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                      <%--  <asp:TemplateField HeaderText="Watershed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwatershed" runat="Server" Text='<%# Eval("WatershedDesc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Sub Watershed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLKSubWatershed" runat="Server" Text='<%# Eval("LKSubWatershed") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HUC-12">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHUC12" runat="Server" Text='<%# Eval("HUCID") %>' />
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

                <div class="panel-width" runat="server" id="dvPerformance">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Performance Measures</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddPerformance" runat="server" Text="Add New Performance Measure" />
                                        <%-- <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/print.png" ToolTip="Watershed Report"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgWatershed_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvPerformanceForm">
                            <asp:Panel runat="server" ID="Panel1">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Performance Measure:</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlPerformance" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                          <td style="width: 100px"><span class="labelClass">Description:</span></td>
                                        <td style="width: 100px">

                                            <asp:TextBox ID="txtDetail" CssClass="clsTextBoxBlue1" runat="server"></asp:TextBox>
                                           

                                        </td>
                                         <td style="width: 170px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="btnAddPerform" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddPerform_Click" />
                                        </td>
                                       
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvPerformanceGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel3" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvPerformance" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvPerformance_RowEditing" OnRowCancelingEdit="gvPerformance_RowCancelingEdit" OnRowUpdating="gvPerformance_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="WQProjectPerformID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWQProjectPerformID" runat="Server" Text='<%# Eval("WQProjectPerformID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Performance Measure">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPerformanceMeasure" runat="Server" Text='<%# Eval("PerformanceMeasure") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDetail" runat="Server" Text='<%# Eval("Detail") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDetail" runat="Server" CssClass="clsTextBoxBlueSMDL" Text='<%# Eval("Detail") %>'></asp:TextBox>
                                            </EditItemTemplate>
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
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvMilestone">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Program Milestones</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddMilestone" runat="server" Text="Add New Milestone" />
                                        <%--               <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/print.png" ToolTip="Watershed Report"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgWatershed_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvMilestoneForm">
                            <asp:Panel runat="server" ID="Panel4">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Milestone:</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlMilestone" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"><span class="labelClass">Date:</span></td>
                                        <td style="width: 100px">

                                            <asp:TextBox ID="txtDate" CssClass="clsTextBoxBlue1" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="aceBoardDate" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>

                                        </td>
                                        <td style="width: 180px">
                                            <asp:Button ID="btnAddMilestone" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddMilestone_Click" />
                                        </td>
                                        <td style="width: 170px"></td>

                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvMilestoneGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel7" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvMilestone" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvMilestone_RowEditing" OnRowCancelingEdit="gvMilestone_RowCancelingEdit" OnRowUpdating="gvMilestone_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="WQProjectMilestoneID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWQProjectMilestoneID" runat="Server" Text='<%# Eval("WQProjectMilestoneID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Milestone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilestone" runat="Server" Text='<%# Eval("Milestone") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("Date", "{0:MM/dd/yyyy}") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDate" runat="Server" CssClass="clsTextBoxBlueSMDL" Text='<%# Eval("Date", "{0:MM-dd-yyyy}") %>'></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender runat="server" ID="acebdt" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                                            </EditItemTemplate>
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
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvNewOT">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Owner Types</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddOT" runat="server" Text="Add New Owner Type" />
                                        <%--<asp:ImageButton ID="ImgConservationownerType" ImageUrl="~/Images/print.png" ToolTip="Conservation Owner TypesReport"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgConservationownerType_Click" />--%>

                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvOTForm">
                            <asp:Panel runat="server" ID="Panel11">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Owner Type:</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlOT" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="btnAddOT" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddOT_Click" />
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

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvOTGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel12" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvOT" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvOT_RowEditing" OnRowCancelingEdit="gvOT_RowCancelingEdit" OnRowUpdating="gvOT_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="WQOTypeID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWQOTypeID" runat="Server" Text='<%# Eval("WQOTypeID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Owner Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOwnerType" runat="Server" Text='<%# Eval("OwnerTypeDesc") %>' />
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
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvDeliverables">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Deliverables</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddDeliverables" runat="server" Text="Add New Deliverables" />
                                        <%--       <asp:ImageButton ID="ImageButton4" ImageUrl="~/Images/print.png" ToolTip="Watershed Report"
                                        Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgWatershed_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvDeliverablesForm">
                            <asp:Panel runat="server" ID="Panel2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Deliverable:</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlDeliverables" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"><span class="labelClass">Date:</span></td>
                                        <td style="width: 100px">

                                            <asp:TextBox ID="txtDateDel" CssClass="clsTextBoxBlue1" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateDel"></ajaxToolkit:CalendarExtender>

                                        </td>
                                        <td style="width: 170px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="btnAddDeliverables" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddDeliverables_Click" />
                                        </td>
                                        
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 5px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                         <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvDeliverablesGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel8" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvDeliverables" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvDeliverables_RowEditing" OnRowCancelingEdit="gvDeliverables_RowCancelingEdit" OnRowUpdating="gvDeliverables_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="WQProjectDeliverablesID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWQProjectDeliverablesID" runat="Server" Text='<%# Eval("WQProjectDeliverablesID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deliverable">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeliverable" runat="Server" Text='<%# Eval("Deliverable") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("Date", "{0:MM/dd/yyyy}") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDate" runat="Server" CssClass="clsTextBoxBlueSMDL" Text='<%# Eval("Date", "{0:MM-dd-yyyy}") %>'></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender runat="server" ID="acebdt" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                                            </EditItemTemplate>
                                             <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                            <ItemStyle Width="50px" />
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

            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfProjectId" runat="server" />
    <asp:HiddenField ID="hfConserveId" runat="server" />
    <asp:HiddenField ID="hfSurfaceWatersId" runat="server" />
    <asp:HiddenField ID="hfIsVisibleBasedOnRole" runat="server" />
    <asp:HiddenField ID="hfConserveWatershedID" runat="server" />
    <asp:HiddenField ID="hfProjectType" runat="server" />
    
    <script language="javascript">

        $('#<%= dvNewWatershedForm.ClientID%>').toggle($('#<%= cbAddWatershed.ClientID%>').is(':checked'));
        $('#<%= cbAddWatershed.ClientID%>').click(function () {
            $('#<%= dvNewWatershedForm.ClientID%>').toggle(this.checked);
        }).change();

        $('#<%= dvOTForm.ClientID%>').toggle($('#<%= cbAddOT.ClientID%>').is(':checked'));
        $('#<%= cbAddOT.ClientID%>').click(function () {
            $('#<%= dvOTForm.ClientID%>').toggle(this.checked);
        }).change();

        $('#<%= dvPerformanceForm.ClientID%>').toggle($('#<%= cbAddPerformance.ClientID%>').is(':checked'));
        $('#<%= cbAddPerformance.ClientID%>').click(function () {
            $('#<%= dvPerformanceForm.ClientID%>').toggle(this.checked);
        }).change();

        $('#<%= dvDeliverablesForm.ClientID%>').toggle($('#<%= cbAddDeliverables.ClientID%>').is(':checked'));
        $('#<%= cbAddDeliverables.ClientID%>').click(function () {
            $('#<%= dvDeliverablesForm.ClientID%>').toggle(this.checked);
        }).change();

        $('#<%= dvMilestoneForm.ClientID%>').toggle($('#<%= cbAddMilestone.ClientID%>').is(':checked'));
        $('#<%= cbAddMilestone.ClientID%>').click(function () {
            $('#<%= dvMilestoneForm.ClientID%>').toggle(this.checked);
        }).change();

    </script>
</asp:Content>
