<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmOperationTownPlanning.aspx.cs" MasterPageFile="~/Site.Master"
    Inherits="vhcbcloud.Conservation.FarmOperationTownPlanning" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="EventContent" ContentPlaceHolderID="MainContent" runat="server">

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
                           <td style="width: 123px"><span class="labelClass">Project #</span></td>
                            <td style="width: 258px">
                              
                                <span class="labelClass" id="ProjectNum" runat="server"></span>
                            </td>
                            <td><span class="labelClass">Name</span></td>
                            <td style="text-align: left">
                               
                                <span class="labelClass" id="ProjName" runat="server"></span>
                            </td>
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
                            <table style="width: 55%">
                                
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Farm Classification :</span></td>
                                    <td style="width: 25%">
                                        <asp:DropDownList ID="ddlFarmClassification" CssClass="clsDropDown" runat="server" Width="200px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Compliant with VT Required Agricultural Practices (RAPs):</span>
                                    <td style="width: 25%">
                                        <asp:RadioButtonList ID="rdbtnRAPCompliance" runat="server" CellPadding="2" CellSpacing="4"  CssClass="clsRadioButton"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                            <asp:ListItem> No &nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Acres of rental land that is related to the conserved land:</span>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtRentedLand" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Total Employees (including Family & Self):</span>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtTotalEmployees" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Full-Time Year Round:</span>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtFTyear" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Part_Time Year Round:</span>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPTyear" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Full_Time Seasonal:</span>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtFTSeasonal" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Part-Time Seasonal:</span>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPTSeasonal" CssClass="clsTextBoxBlueSm" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                              </table>
                            <table  style="width: 100%">
                                <tr>
                                    <td><span class="labelClass">Where products are sold:</span>
                                   </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                     <td style="width: 25%">
                                        <asp:TextBox ID="txtProductsSold" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                                    </td>
                                    </tr>
                                 <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                </table>
                             <table style="width: 50%">
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Farmer worked with FFVP?</span>
                                    <td style="width: 25%">
                                        <asp:RadioButtonList ID="rdbtnWorkedWithVHCB" runat="server" CellPadding="2" CellSpacing="4" CssClass="clsRadioButton"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                            <asp:ListItem> No &nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                            </table>
                            <div class="panel-width" runat="server" id="dvNewCM">
                                <div class="panel panel-default" style="margin-bottom: 2px;">
                                    <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <h3 class="panel-title">Conservation Measures</h3>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbAddCM" runat="server" Text="Add New Conservation Measures" />

                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvCMForm">
                                        <asp:Panel runat="server" ID="Panel1">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 140px"><span class="labelClass">Conservation Measures:</span></td>
                                                    <td style="width: 215px">
                                                        <asp:DropDownList ID="ddlCM" CssClass="clsDropDownLong" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 100px"></td>
                                                    <td style="width: 180px">
                                                        <asp:Button ID="btnAddCM" runat="server" Text="Add" class="btn btn-info" OnClick="btnAddCM_Click" />
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
                                            <asp:GridView ID="gvCM" runat="server" AutoGenerateColumns="False"
                                                Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                                GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                                OnRowEditing="gvCM_RowEditing" OnRowCancelingEdit="gvCM_RowCancelingEdit" OnRowUpdating="gvCM_RowUpdating">
                                                <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                                <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                                <HeaderStyle CssClass="headerStyle" />
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                                <RowStyle CssClass="rowStyle" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ConserveMeasuresID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblConserveMeasuresID" runat="Server" Text='<%# Eval("ConserveMeasuresID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Conservation Measures">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblConservationMeasures" runat="Server" Text='<%# Eval("ConserveMeasuresDesc") %>' />
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

                             <table  style="width: 100%">
                                <tr>
                                    <td><span class="labelClass">Farm’s operation that mitigate or adapt to climate change:</span>
                                   </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                 </table>
                            <table>
                                <tr>
                                     <td style="width: 25%">
                                        <asp:TextBox ID="txtMitigateClimate" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                                    </td>
                                    </tr>
                                </table>

                                <table>
                                 <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                  <tr>
                                    <td><span class="labelClass">Existing Farm Infrastructure:</span>
                                   </td>
                                      <td>
                                          <asp:RadioButtonList ID="rdbtnExistingInfastructure" runat="server" CellPadding="2" CellSpacing="4" CssClass="clsRadioButton"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                            <asp:ListItem> No &nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                      </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                               
                                
                                 <tr>
                                    <td><span class="labelClass">Describe this Infrastructure:</span>
                                   </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                    </table>
                            <table>
                                <tr>
                                     <td style="width: 25%">
                                        <asp:TextBox ID="txtInfrastuctureDescription" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                                    </td>
                                    </tr>
                                 <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                </table>

                        </div>
                    </div>
                </div>

                <div class="panel-width" runat="server" id="Div1">
                    <div class="panel panel-default ">
                        <div class="panel-heading ">
                            <h3 class="panel-title">TOWN PLANNING, ZONING, & SUPPORT</h3>
                        </div>

                        <div class="panel-body">
                            <table style="width: 50%">
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Zoning District:</span></td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtZoningDistrict" CssClass="clsTextBoxBlueSm" runat="server" Width="150px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                 <tr>
                                    <td style="width: 75%"><span class="labelClass">Minimum Lot Sizes:</span></td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtMinLotSize" CssClass="clsTextBoxBlueSm" runat="server" Width="150px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 75%"><span class="labelClass">Enrolled in Vermont’s use-value appraisal (UVA) program?</span></td>
                                    <td style="width: 25%">
                                        <asp:RadioButtonList ID="rdbtnEnrolledUseValue" runat="server" CellPadding="2" CellSpacing="4"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                            <asp:ListItem> No &nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>

                                 <tr>
                                    <td colspan="6" style="height: 5px">
                                         <asp:Button ID="btnSubmit" runat="server" Text="Update" class="btn btn-info" OnClick="btnSubmit_Click" />
                                    </td>
                                </tr>
                                

                            </table>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfProjectId" runat="server" />
    <asp:HiddenField ID="hfConserveId" runat="server" />

    <script language="javascript">

        $(document).ready(function () {


            $('#<%= dvCMForm.ClientID%>').toggle($('#<%= cbAddCM.ClientID%>').is(':checked'));
            $('#<%= cbAddCM.ClientID%>').click(function () {
                $('#<%= dvCMForm.ClientID%>').toggle(this.checked);
            }).change();

            var txtboxs = $('#<%= txtFTyear.ClientID%>,#<%= txtPTyear.ClientID%>,#<%= txtFTSeasonal.ClientID%>,#<%= txtPTSeasonal.ClientID%>');
            $.each(txtboxs, function () {
                $(this).blur(function () {

                    CalculatePercentages();
                });
            });

            function CalculatePercentages() {

                var totFTyear = (isNaN(parseFloat($('#<%=txtFTyear.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtFTyear.ClientID%>').val(), 10));
                var totPTyear = (isNaN(parseFloat($('#<%=txtPTyear.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtPTyear.ClientID%>').val(), 10));
                var totFTSeasonal = (isNaN(parseFloat($('#<%=txtFTSeasonal.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtFTSeasonal.ClientID%>').val(), 10));
                var totPTSeasonal = (isNaN(parseFloat($('#<%=txtPTSeasonal.ClientID%>').val(), 10)) ? 0 : parseFloat($('#<%=txtPTSeasonal.ClientID%>').val(), 10));


                var totAcres = parseFloat(totFTyear + totPTyear + totFTSeasonal + totPTSeasonal, 4);

                $('#<%=txtTotalEmployees.ClientID%>').val(totAcres);
                console.log(totAcres);

            };
        });
    </script>
</asp:Content>
