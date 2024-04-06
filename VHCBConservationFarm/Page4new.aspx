<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Page4new.aspx.cs" Inherits="VHCBConservationFarm.Page4new" MaintainScrollPositionOnPostback="true" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
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

    <%--    <link rel="stylesheet" runat="server" media="screen" href="~/Content/StyleSheet.css" />--%>



    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/CurrencyController.js"></script>
    <style type="text/css">
        .FormatRadioButtonList label {
            margin-left: 5px;
        }

        .auto-style7 {
            width: 274px;
        }

        .auto-style8 {
            width: 301px;
        }

        .auto-style11 {
            width: 196px;
        }

        .auto-style12 {
            width: 175px;
        }

        .auto-style13 {
            width: 169px;
        }

        .auto-style14 {
            width: 195px;
        }

        .auto-style16 {
            width: 154px;
        }

        .auto-style17 {
            width: 162px;
        }

        .auto-style18 {
            width: 144px;
        }

        .auto-style19 {
            width: 643px;
        }
    </style>
    <div class="jumbotron" id="vhcb">
        <p class="lead">LAND & WATER RESOURCES</p>
        <div class="container">
            <div class="panel panel-default">
                <div id="dvEntityRole" runat="server">
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <div id="dvMessage" runat="server" visible="false">
                        <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg" ForeColor="Red"></asp:Label></p>
                    </div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right;">
                                <asp:CheckBox ID="cbActiveOnly" runat="server" Text="Active Only" Checked="true" AutoPostBack="true" OnCheckedChanged="cbActiveOnly_CheckedChanged" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style7">
                                <span class="labelClass" style="margin-left: 10px">What tactical basin does this project reside in?</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTacticalBasin" CssClass="clsDropDown" runat="server" Height="23px" Width="185px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px" class="auto-style7"><strong>a) Surface Water and Watershed</strong>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style7">
                                <span class="labelClass" style="margin-left: 10px">Does this project area contain surface waters?</span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdBtnSurfaceWaters" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rdBtnSurfaceWaters_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <%-- <tr>

                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtSurfaceWaterDesc" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="500px" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>--%>
                    </table>


                    <div class="panel-width" runat="server" id="dvNewSurfaceWaters" visible="false">
                        <div class="panel panel-default ">
                            <div class="panel-heading ">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h3 class="panel-title">Surface Waters</h3>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:CheckBox ID="cbAddSurfaceWaters" runat="server" Text="Add New Surface Waters" />
                                            <%--<asp:ImageButton ID="ImgSurfaceWaters" ImageUrl="~/Images/print.png" ToolTip="Surface Waters Report"
                                                Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgSurfaceWaters_Click" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="panel-body" runat="server" id="dvNewSurfaceWatersForm">
                                <asp:Panel runat="server" ID="Panel3">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 140px"><span class="labelClass">Water Body</span></td>
                                            <td style="width: 215px">
                                                <asp:DropDownList ID="ddlWaterBody" CssClass="clsDropDown" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 166px">
                                                <span class="labelClass">Frontage (ft):</span>
                                            </td>
                                            <td style="width: 180px">
                                                <asp:TextBox ID="txtFrontageFeet" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="width: 170px"><span class="labelClass">Other Stream/Pond Name</span></td>
                                            <td>
                                                <asp:TextBox ID="txtOtherStream" CssClass="clsTextBoxBlue" runat="server"></asp:TextBox>
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
                                            <asp:TemplateField HeaderText="Stream/Pond Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOtherWater" runat="Server" Text='<%# Eval("OtherWater") %>' />
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


                    <div runat="server" id="divPrimary">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="6" style="height: 5px"></td>
                            </tr>
                            <tr>
                                <td><span class="labelClass" style="margin-left: 10px">Primary Watershed</span></td>
                                <td>
                                    <asp:DropDownList ID="ddlWaterShedNew" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWaterShedNew_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <span class="labelClass" style="margin-left: 10px">Primary Sub-Watershed (HUC8)</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubWatershedNew" CssClass="clsDropDown" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <span class="labelClass" style="margin-left: 10px">Primary HUC-12</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHUC12Primary" CssClass="clsDropDown" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                       
                  
                        <tr>
                            <td colspan="6" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">Secondary Watershed</span></td>
                            <td>
                                <asp:DropDownList ID="ddlWaterShedNewSec" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWaterShedNewSec_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span class="labelClass" style="margin-left: 10px">Secondary Sub-Watershed (HUC8)</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubWatershedNewSec" CssClass="clsDropDown" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span class="labelClass" style="margin-left: 10px">Secondary HUC-12</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlHUC12Secondary" CssClass="clsDropDown" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 5px"></td>
                        </tr>
                    </table>

                        </div>
                    <table>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style8">
                                <span class="labelClass" style="margin-left: 10px">How many acres of wetlands are on the property?</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWetlands" CssClass="clsTextBoxBlue1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style8">
                                <span class="labelClass" style="margin-left: 10px">How many acres of floodplain are on the property?</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFloodPlain" CssClass="clsTextBoxBlue1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <table>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px" class="auto-style7"><strong>b) Water Quality</strong>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">
                                <span class="labelClass" style="margin-left: 10px">1. Briefly describe any known water quality or other resource-related concerns on the property, including any known current or past water quality violations with the State of Vermont. 
                                </span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;
                                <asp:TextBox ID="txtWaterQualityIssues" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />

                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">
                                <span class="labelClass" style="margin-left: 10px">2. Describe any management practices used to protect water quality, if applicable, and whether or not these practices (or infrastructure) have been implemented with financial assistance from state or federal programs (i.e., NRCS Environmental Quality Incentives Program) (EQIP):
                                </span></td>
                        </tr>
                    </table>
                    <table>
                        <tr>

                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtWQManagepractices" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style19">
                                <span class="labelClass" style="margin-left: 10px">3. Are there drainage ditches installed on the property?
                                </span></td>
                            <td>
                                <asp:RadioButtonList ID="rdbtnDrainageDitches" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style19">
                                <span class="labelClass" style="margin-left: 10px">4. Are there drainage tiles installed on the property?
                                </span></td>
                            <td>
                                <asp:RadioButtonList ID="rdbtnDrainageTiles" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style19">
                                <span class="labelClass" style="margin-left: 10px">5. Any other types of waste or water management infrastructure to note, including animal manure pits? 
                                </span></td>
                            <td>
                                <asp:RadioButtonList ID="rdbtnWastewatermanage" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbtnWastewatermanage_SelectedIndexChanged">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style19">&nbsp;&nbsp;
                                 <span class="labelClass" style="margin-left: 10px" runat="server" visible="false" id="spnInfrastuctureDescription">Please describe: </span>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style19">&nbsp;&nbsp;
                                <asp:TextBox ID="txtInfrastuctureDescription" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="margin-left: 10px" class="auto-style19">
                                <span class="labelClass" style="margin-left: 10px">6. Are livestock excluded from all surface waters? 
                                </span></td>
                            <td>
                                <asp:RadioButtonList ID="rdbtnLivestockExcluded" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>
                    </table>
                    <hr />

                    <table style="width: 70%">

                        <tr>
                            <td colspan="6" style="margin-left: 10px" class="auto-style7"><strong>c. Acreage</strong>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass">Tillable:</span></td>
                            <td style="width: 142px">
                                <asp:TextBox ID="txtTillable" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                            <td class="auto-style17"><span class="labelClass">UnManaged:</span></td>
                            <td class="auto-style16">
                                <asp:TextBox ID="txtUnManaged" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                            <td class="auto-style18"><span class="labelClass">Sugarbush</span></td>
                            <td>
                                <asp:TextBox ID="txtSugarbush" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass">Pasture:</span></td>
                            <td style="width: 142px">
                                <asp:TextBox ID="txtPasture" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                            <td class="auto-style17"><span class="labelClass">Farmstead/Residential:</span></td>
                            <td class="auto-style16">
                                <asp:TextBox ID="txtFarmResident" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                            <td class="auto-style18"><span class="labelClass">Hay</span></td>
                            <td>
                                <asp:TextBox ID="txtHay" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass">Wooded:</span></td>
                            <td style="width: 142px">
                                <asp:TextBox ID="txtWooded" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                            <td class="auto-style17"><span class="labelClass">Natural/Rec</span></td>
                            <td class="auto-style16">
                                <asp:TextBox ID="txtNaturalRec" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox></td>
                            <td class="auto-style18"><span class="labelClass"># of taps</span></td>
                            <td>
                                <asp:TextBox ID="txtTaps" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 5px"></td>
                        </tr>
                    </table>

                </div>
                <table>
                    <tr>
                        <td colspan="2" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td style="margin-left: 10px">
                            <span class="labelClass" style="margin-left: 10px"><span>Is there an existing forest management plan?</span>
                            </span></td>
                        <td>
                            <asp:RadioButtonList ID="rdbtnManagedTimber" runat="server" CellPadding="2" CellSpacing="4"
                                RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbtnManagedTimber_SelectedIndexChanged">
                                <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                <asp:ListItem> No &nbsp;</asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td style="margin-left: 10px">
                            <span class="labelClass" style="margin-left: 10px" id="spnForestMgtPlan" runat="server" visible="false">What is the date of the forest management plan?
                            </span></td>
                        <td>
                            <asp:TextBox ID="txtForestplandate" CssClass="clsTextBoxBlue1" runat="server" MaxLength="20" Visible="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtForestplandate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td colspan="7" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td colspan="7" style="margin-left: 10px" class="auto-style7"><strong>d. Soils</strong>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="height: 10px"></td>
                    </tr>

                </table>
                <table>
                    <thead>
                        <tr>
                            <th class="auto-style11" style="margin-left: 10px">&nbsp;&nbsp;Prime</th>
                            <th class="auto-style11">Acres</th>
                            <th class="auto-style14">%</th>
                            <th></th>
                            <th class="auto-style11">Statewide</th>
                            <th class="auto-style12">Acres</th>
                            <th class="auto-style13">%</th>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            <span class="labelClass" style="margin-left: 10px">Footnoted
                            </span>
                        </td>
                        <td class="auto-style11">
                            <asp:TextBox ID="txtPrimefoot" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style14">

                            <asp:TextBox ID="txtPrimefootPC" CssClass="clsTextBoxBlueSm" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <span class="labelClass" style="margin-left: 10px">Footnoted
                            </span>
                        </td>
                        <td class="auto-style12">
                            <asp:TextBox ID="txtStatewideFoot" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style13">
                            <asp:TextBox ID="txtStatewideFootPC" CssClass="clsTextBoxBlueSm" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td>
                            <span class="labelClass" style="margin-left: 10px">Non-Footnoted
                            </span>
                        </td>
                        <td class="auto-style11">
                            <asp:TextBox ID="txtPrimeNonFoot" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="txtPrimeNonFootPC" CssClass="clsTextBoxBlueSm" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <span class="labelClass" style="margin-left: 10px">Non-Footnoted
                            </span>
                        </td>
                        <td class="auto-style12">
                            <asp:TextBox ID="txtStatewideNonFoot" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style13">
                            <asp:TextBox ID="txtStatewideNonFootPC" CssClass="clsTextBoxBlueSm" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="height: 10px"></td>

                    </tr>
                    <tr>
                        <td>
                            <span class="labelClass" style="margin-left: 10px">Total
                            </span>
                        </td>
                        <td class="auto-style11"><span class="labelClass" runat="server" id="spnPrimaryfootTotal"></span>

                        </td>
                        <td class="auto-style14">&nbsp;</td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <span class="labelClass" style="margin-left: 10px">Total
                            </span>
                        </td>
                        <td class="auto-style12"><span class="labelClass" runat="server" id="spnStatewidefootTotal"></span></td>
                        <td class="auto-style13">&nbsp;</td>
                    </tr>

                </table>
                <table>
                    <tr>
                        <td style="height: 10px"></td>

                    </tr>
                    <tr>
                        <td>
                            <span class="labelClass" style="margin-left: 10px">For more information on soils data, please see <a href="https://websoilsurvey.nrcs.usda.gov/app/" target="_blank">USDA’s Web Soil Survey</a> or contact your local  Natural Resources Conservation Service (NRCS) office. 
                            </span>
                        </td>
                    </tr>
                </table>
                <hr />
                <div class="panel-width" runat="server" id="dvFarmProducts">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">e. Products</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddFarmProducts" runat="server" Text="Add New Products" />

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
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="dvNewProjectAttribute">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">h. Project Attributes</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddProjectAttribute" runat="server" Text="Add New Project Attribute" />

                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvProjectAttributeForm">
                            <asp:Panel runat="server" ID="Panel7">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Attribute</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlProjectAttribute" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="AddProjectAttribute" runat="server" Text="Add" class="btn btn-info" OnClick="AddProjectAttribute_Click" />
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

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvProjectAttributeGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel10" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvProjAttribute" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvProjAttribute_RowEditing" OnRowCancelingEdit="gvProjAttribute_RowCancelingEdit"
                                    OnRowUpdating="gvProjAttribute_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Conserve Attrib ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConserveAttribID" runat="Server" Text='<%# Eval("ConserveAttrib2ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Attribute">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttribute" runat="Server" Text='<%# Eval("Attribute") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                            <%--<EditItemTemplate>
                                                <asp:DropDownList ID="ddlAttributeE" CssClass="clsDropDown" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtLkConsAttrib" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("LkConsAttrib") %>' Visible="false">
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

                <div class="panel-width" runat="server" id="dvNewPA">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">i. Type of Public Access</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddPA" runat="server" Text="Add New Type of Public Access" />

                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvPAForm">
                            <asp:Panel runat="server" ID="Panel1">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Type of Public Access:</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlPA" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="btnAddPA" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddPA_Click" />
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

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvPAGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel2" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvPA" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvPA_RowEditing" OnRowCancelingEdit="gvPA_RowCancelingEdit" OnRowUpdating="gvPA_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ConservePAcessID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConservePAcessID" runat="Server" Text='<%# Eval("ConservePAcessID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type of Public Access">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPublicAccess" runat="Server" Text='<%# Eval("PublicAccess") %>' />
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

                <div class="panel-width" runat="server" id="dvNewAltEnergy">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">j. Alternative Energy</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddAltEnergy" runat="server" Text="Add New Alternative Energy" />

                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvAltEnergyForm">
                            <asp:Panel runat="server" ID="Panel5">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Alternative Energy:</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlAltEnergy" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="btnAddAltEnergy" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddAltEnergy_Click" />

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

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvAltEnergyGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel6" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvAltEnergy" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvAltEnergy_RowEditing" OnRowCancelingEdit="gvAltEnergy_RowCancelingEdit" OnRowUpdating="gvAltEnergy_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ConsserveAltEnergyID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConsserveAltEnergyID" runat="Server" Text='<%# Eval("ConsserveAltEnergyID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Alternative Energy">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAlternativeEnergy" runat="Server" Text='<%# Eval("AlternativeEnergy") %>' />
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


                <table>

                    <tr>
                        <td colspan="6" style="margin-left: 10px">&nbsp; &nbsp;<asp:Button ID="btnPrevious" runat="server" Text="Previous Page/Save" class="btn btn-info" OnClick="btnPrevious_Click" />
                            &nbsp; &nbsp;
                                <asp:Button ID="btnNext" runat="server" Text="Next Page/Save" class="btn btn-info" OnClick="btnNext_Click" />
                            &nbsp; &nbsp; 
                          <asp:Label runat="server" ID="Label1" class="labelClass" Text="Go To"></asp:Label>
                            <asp:DropDownList ID="ddlGoto" CssClass="clsDropDown" runat="server" Height="23px" Width="185px" AutoPostBack="true" OnSelectedIndexChanged="ddlGoto_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Applicant Information" Value="FirstPage.aspx"></asp:ListItem>
                                <asp:ListItem Text="Executive Summary and Farm Transfer" Value="ThirdPage.aspx"></asp:ListItem>
                                <asp:ListItem Text="Land & Water Resources" Value="Page4New.aspx"></asp:ListItem>
                                <asp:ListItem Text="Farm Management" Value="FarmManagement.aspx"></asp:ListItem>
                                <asp:ListItem Text="Easement Terms" Value="EasementTerms.aspx"></asp:ListItem>
                                <asp:ListItem Text="Town Planning" Value="TownPlaning.aspx"></asp:ListItem>
                                <asp:ListItem Text="Additional Info" Value="Additionalinfo.aspx"></asp:ListItem>
                                <asp:ListItem Text="Attachments" Value="Attachments.aspx"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 10px"></td>
                    </tr>
                </table>

                <asp:HiddenField ID="hfProjectId" runat="server" />
                <asp:HiddenField ID="hfConserveId" runat="server" />
                <asp:HiddenField ID="hfSurfaceWatersId" runat="server" />
                 <asp:HiddenField ID="hfTotaAcres" runat="server" />
            </div>
        </div>
    </div>


    <script language="javascript">

        $(document).ready(function () {
            $('#<%= dvNewSurfaceWatersForm.ClientID%>').toggle($('#<%= cbAddSurfaceWaters.ClientID%>').is(':checked'));

            $('#<%= cbAddSurfaceWaters.ClientID%>').click(function () {
                $('#<%= dvNewSurfaceWatersForm.ClientID%>').toggle(this.checked);
            }).change();
            $('#<%= dvFarmProductsForm.ClientID%>').toggle($('#<%= cbAddFarmProducts.ClientID%>').is(':checked'));
            $('#<%= cbAddFarmProducts.ClientID%>').click(function () {
                $('#<%= dvFarmProductsForm.ClientID%>').toggle(this.checked);
            }).change();
            $('#<%= dvProjectAttributeForm.ClientID%>').toggle($('#<%= cbAddProjectAttribute.ClientID%>').is(':checked'));
            $('#<%= cbAddProjectAttribute.ClientID%>').click(function () {
                $('#<%= dvProjectAttributeForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvPAForm.ClientID%>').toggle($('#<%= cbAddPA.ClientID%>').is(':checked'));
            $('#<%= cbAddPA.ClientID%>').click(function () {
                $('#<%= dvPAForm.ClientID%>').toggle(this.checked);
            }).change();
            $('#<%= dvAltEnergyForm.ClientID%>').toggle($('#<%= cbAddAltEnergy.ClientID%>').is(':checked'));
            $('#<%= cbAddAltEnergy.ClientID%>').click(function () {
                $('#<%= dvAltEnergyForm.ClientID%>').toggle(this.checked);
            }).change();

            var txtboxs = $('#<%= txtPrimefoot.ClientID%>,#<%= txtPrimeNonFoot.ClientID%>,#<%= txtStatewideFoot.ClientID%>,#<%= txtStatewideNonFoot.ClientID%>, #<%= txtTillable.ClientID%>, #<%= txtUnManaged.ClientID%>, #<%= txtSugarbush.ClientID%>, #<%= txtPasture.ClientID%>, #<%= txtFarmResident.ClientID%>, #<%= txtHay.ClientID%>,  #<%= txtWooded.ClientID%>, #<%= txtNaturalRec.ClientID%>');
            $.each(txtboxs, function () {
                $(this).blur(function () {

                    CalculatePercentages();
                });
            });

            function CalculatePercentages() {

                var totTillable = (isNaN(parseFloat($('#<%=txtTillable.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtTillable.ClientID%>').val(), 10));
                var totUnmanaged = (isNaN(parseFloat($('#<%=txtUnManaged.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtUnManaged.ClientID%>').val(), 10));
                var totSugar = (isNaN(parseFloat($('#<%=txtSugarbush.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtSugarbush.ClientID%>').val(), 10));
                var totPasture = (isNaN(parseFloat($('#<%=txtPasture.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtPasture.ClientID%>').val(), 10));
                var totFarmResident = (isNaN(parseFloat($('#<%=txtFarmResident.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtFarmResident.ClientID%>').val(), 10));
                var totHay = (isNaN(parseFloat($('#<%=txtHay.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtHay.ClientID%>').val(), 10));
                var totWooded = (isNaN(parseFloat($('#<%=txtWooded.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtWooded.ClientID%>').val(), 10));
                var totNatural = (isNaN(parseFloat($('#<%=txtNaturalRec.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtNaturalRec.ClientID%>').val(), 10));

                //var totAcres = totTillable + totUnmanaged + totSugar + totPasture + totFarmResident + totHay + totWooded + totNatural;
                var totAcres = (isNaN(parseFloat($('#<%=hfTotaAcres.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=hfTotaAcres.ClientID%>').val(), 10));
                console.log(totAcres);

                var totPrimeFoot = (isNaN(parseFloat($('#<%=txtPrimefoot.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtPrimefoot.ClientID%>').val(), 10));
                var totPrimeNonFoot = (isNaN(parseFloat($('#<%=txtPrimeNonFoot.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtPrimeNonFoot.ClientID%>').val(), 10));
                var totPrime = totPrimeFoot + totPrimeNonFoot;

                var pfc = 0;
                var pnfc = 0;

                if (totAcres > 0) {
                    pfc = (totPrimeFoot * 100 / totAcres).toFixed(2);
                    pnfc = (totPrimeNonFoot * 100 / totAcres).toFixed(2);
                }

                $('#<%=txtPrimefootPC.ClientID%>').val(pfc);
                $('#<%=txtPrimeNonFootPC.ClientID%>').val(pnfc);
                $('#<%=spnPrimaryfootTotal.ClientID%>').text(totPrime);

                var totSFoot = (isNaN(parseFloat($('#<%=txtStatewideFoot.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtStatewideFoot.ClientID%>').val(), 10));
                var totSNonFoot = (isNaN(parseFloat($('#<%=txtStatewideNonFoot.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtStatewideNonFoot.ClientID%>').val(), 10));
                var totS = totSFoot + totSNonFoot;

                var psfc = 0;
                var psnfc = 0;

                if (totAcres > 0) {
                    psfc = (totSFoot * 100 / totAcres).toFixed(2);
                    psnfc = (totSNonFoot * 100 / totAcres).toFixed(2);
                }
                $('#<%=txtStatewideFootPC.ClientID%>').val(psfc);
                $('#<%=txtStatewideNonFootPC.ClientID%>').val(psnfc);
                $('#<%=spnStatewidefootTotal.ClientID%>').text(totS);

                console.log(totSFoot);
                console.log(totAcres);
                console.log(psfc);
            };
        });

    </script>
</asp:Content>



