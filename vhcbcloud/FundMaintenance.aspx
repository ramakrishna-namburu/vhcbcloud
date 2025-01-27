<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FundMaintenance.aspx.cs" Inherits="vhcbcloud.FundMaintenance"
    MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>

<asp:Content ID="EventContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" id="vhcb">
        <p class="lead">Fund Maintenance </p>

        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <h3 class="panel-title">Fund Search</h3>
                            </td>
                            <td style="text-align: right;">
                                <asp:CheckBox ID="cbActiveOnly" runat="server" Text="Active Only" Checked="true" AutoPostBack="true" OnCheckedChanged="cbActiveOnly_CheckedChanged" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div id="dvMessage" runat="server">
                    <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg"></asp:Label></p>
                </div>

                <div id="dvFund" runat="server">
                    <table>
                        <tr>
                            <td colspan="6" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="width: 10px"></td>
                            <td style="width: 100px"><span class="labelClass">Fund Name</span></td>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddlFundName" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFundName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 90px"><span class="labelClass">Fund #</span></td>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddlAcctNum" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcctNum_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 50px"><span class="labelClass">Fund ID</span></td>
                            <td>
                              <span class="labelClass" runat="server" id="spnFundId"></span>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 10px"></td>
                        </tr>
                    </table>
                </div>
                <div id="dvfundf" runat="server">
                    <div class="panel-width" runat="server" id="dvNewFund">
                        <div class="panel panel-default ">
                            <div class="panel-heading ">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h3 class="panel-title">Fund</h3>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:CheckBox ID="cbAddFund" runat="server" Text="Add New Fund" />
                                            <asp:ImageButton ID="ImgFundDataReport" ImageUrl="~/Images/print.png" ToolTip="Fund Data Report"
                                                Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgFundDataReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="panel-body" runat="server" id="dvFundForm">
                        <asp:Panel runat="server" ID="Panel10">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 224px"><span class="labelClass">Name</span></td>
                                    <td style="width: 250px">
                                        <asp:TextBox ID="txtFundName" CssClass="clsTextBoxBlueSm" Width="100px" Height="22px" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 140px">
                                        <span class="labelClass">Abbrev</span>
                                    </td>
                                    <td style="width: 237px">
                                        <asp:TextBox ID="txtAbbrev" CssClass="clsTextBoxBlueSm" Width="100px" Height="22px" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 101px"><span class="labelClass">Type</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlFundType" CssClass="clsDropDown" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 224px"><span class="labelClass">Fund #</span></td>
                                    <td style="width: 250px">
                                        <asp:TextBox ID="txtFundNum" CssClass="clsTextBoxBlueSm" Width="100px" Height="22px" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 140px">
                                        <span class="labelClass">SOV Fund Code </span>
                                    </td>
                                    <td style="width: 237px">
                                        <asp:DropDownList ID="ddlSOVFundCode" CssClass="clsDropDown" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 101px"><span class="labelClass">MIP Fund #</span></td>
                                    <td>
                                        <asp:TextBox ID="txtMIPFundNo" CssClass="clsTextBoxBlueSm" Width="100px" Height="22px" runat="server" MaxLength="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 224px"><span class="labelClass">SOV Dept. ID</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlSOVDeptId" CssClass="clsDropDown" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td><span class="labelClass">Mitigation Fund</span></td>
                                    <td>
                                        <asp:CheckBox ID="cbMitFund" runat="server" Checked="false" />
                                    </td>
                                    <td>
                                        <span class="labelClass">Active:</span>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbFundActive" Enabled="false" runat="server" Checked="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 224px"><span class="labelClass">Require 2nd Program Approval</span></td>
                                    <td>
                                        <asp:CheckBox ID="cbSecondapproval" runat="server" Checked="false" />
                                    </td>
                                    <td><span class="labelClass">Beginning Amount $</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBegAmount" CssClass="clsTextBoxBlueSm" Width="150px" Height="22px" runat="server" MaxLength="16"></asp:TextBox>
                                    </td>
                                    <td><span class="labelClass">As of Date</span></td>
                                    <td>
                                        <asp:TextBox ID="txtAsofDate" CssClass="clsTextBoxBlue1" runat="server" Width="100px" Height="22px" AutoPostBack="true" OnTextChanged="txtAsofDate_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" ID="ce_txtAsofDate" TargetControlID="txtAsofDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 224px"></td>
                                    <td></td>
                                    <td><span class="labelClass">Committed $ Amount</span></td>
                                    <td>
                                        <span class="labelClass" id="spnCalculatedAmt" runat="server"></span>
                                    </td>
                                    <td><span class="labelClass">$ Balance</span></td>
                                    <td><span class="labelClass" id="spnBalance" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                 <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 224px"></td>
                                    <td></td>
                                    <td><span class="labelClass">Commitment Method</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCommitmentType" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCommitmentType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td><span class="labelClass">Method</span></td>
                                    <td><span class="labelClass" id="spnMethod" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="height: 5px">
                                        <asp:Button ID="btnSubmitFund" runat="server" Text="Add" class="btn btn-info" OnClick="btnSubmitFund_Click" />
                                        &nbsp; &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-info" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px"></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>

                    <div class="panel-width" runat="server" id="dvNewAttachedGrants" visible="false">
                        <div class="panel panel-default ">
                            <div class="panel-heading ">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h3 class="panel-title">Attached Grants</h3>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:CheckBox ID="cbAddAttachedGrants" runat="server" Text="Add to Grant" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="panel-body" runat="server" id="dvAttachedGrantsForm">
                                <asp:Panel runat="server" ID="Panel1">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 150px"><span class="labelClass">VHCB Grant Name</span></td>
                                            <td style="width: 250px">
                                                <asp:DropDownList ID="ddlGrantName" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFundName_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                            <td style="width: 63px">&nbsp;</td>
                                            <td style="width: 167px"></td>
                                            <td style="width: 170px">
                                                <asp:Button ID="btnAddAttachGrant" runat="server" Text="Add" class="btn btn-info"
                                                    OnClick="btnAddAttachGrant_Click" Style="margin-left: 0" /></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="height: 5px"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <div class="panel-body" id="dvAttachedGrantsGrid" runat="server">
                                <asp:Panel runat="server" ID="Panel4" Width="100%" Height="200px" ScrollBars="Vertical">
                                    <%--<asp:GridView ID="gvAttachedGrants" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" ShowFooter="false"
                                    OnRowEditing="gvAttachedGrants_RowEditing" OnRowCancelingEdit="gvAttachedGrants_RowCancelingEdit"
                                    OnRowUpdating="gvAttachedGrants_RowUpdating" OnRowDataBound="gvAttachedGrants_RowDataBound">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <FooterStyle CssClass="footerStyleTotals" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="FundGrantinfoID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFundGrantinfoID" runat="Server" Text='<%# Eval("FundGrantinfoID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fund Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFundName" runat="Server" Text='<%# Eval("FundName") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fund ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFundID" runat="Server" Text='<%# Eval("FundID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>--%>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <div class="panel-width" runat="server" id="dvNewNotes">
                        <div class="panel panel-default" style="margin-bottom: 2px;">
                            <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h3 class="panel-title">Notes</h3>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:CheckBox ID="cbAddnotes" runat="server" Text="Add New Notes" />
                                            <asp:ImageButton ID="ImgNotesReport" ImageUrl="~/Images/print.png" ToolTip="Notes Report" Visible="false"
                                                Style="border: none; vertical-align: middle;" runat="server" OnClick="ImgNotesReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvNotesForm">
                                <asp:Panel runat="server" ID="Panel13">
                                    <table style="width: 100%">
                                        <tr>
                                            <td><span class="labelClass">Notes</span></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtEntityNotes" TextMode="multiline" CssClass="clsTextBoxBlue1"
                                                    Columns="50" Rows="2" runat="server" Width="100%" Height="120px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="labelClass">Active</span>

                                                <asp:CheckBox ID="cbNotesActive" CssClass="ChkBox" runat="server" Enabled="false" Checked="true" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="height: 5px"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px">
                                                <asp:Button ID="btnAddNotes" runat="server" Text="Submit" class="btn btn-info" OnClick="btnAddNotes_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvNotesGrid" runat="server">
                                <asp:Panel runat="server" ID="Panel14" Width="100%" Height="200px" ScrollBars="Vertical">
                                    <asp:GridView ID="gvNotes" runat="server" AutoGenerateColumns="False"
                                        Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                        GridLines="None" EnableTheming="True" AllowPaging="false"
                                        OnRowCancelingEdit="gvNotes_RowCancelingEdit"
                                        OnRowEditing="gvNotes_RowEditing"
                                        OnRowUpdating="gvNotes_RowUpdating"
                                        OnRowDataBound="gvNotes_RowDataBound">
                                        <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                        <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                        <HeaderStyle CssClass="headerStyle" />
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                        <RowStyle CssClass="rowStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Project Notes ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFundNoteID" runat="Server" Text='<%# Eval("FundNoteID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("Date") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserName" runat="Server" Text='<%# Eval("username") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Notes" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNotes" runat="Server" Text='<%# Eval("FullNotes") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Active" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    &nbsp; &nbsp;<asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%# Eval("RowIsActive") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit" Visible='<%# GetIsVisibleBasedOnRole() %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%-- <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Update" Text="Update" />--%>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Cancel" Text="Cancel" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <asp:HiddenField ID="hfEntityNotesId" runat="server" />
                            </div>
                            <asp:HiddenField ID="hfFundNoteID" runat="server" />
                             <asp:HiddenField ID="hfLKMethod" runat="server" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfIsVisibleBasedOnRole" runat="server" />
    </div>
    <script language="javascript">
        $(document).ready(function () {
            toCurrencyControl($('#<%= txtBegAmount.ClientID%>').val(), $('#<%= txtBegAmount.ClientID%>'));

            $('#<%= txtBegAmount.ClientID%>').keyup(function () {
                toCurrencyControl($('#<%= txtBegAmount.ClientID%>').val(), $('#<%= txtBegAmount.ClientID%>'));
            });

            $('#<%= dvFundForm.ClientID%>').toggle($('#<%= cbAddFund.ClientID%>').is(':checked'));

            $('#<%= cbAddFund.ClientID%>').click(function () {
                $('#<%= dvFundForm.ClientID%>').toggle(this.checked);
            }).change();

            $('#<%= dvNotesForm.ClientID%>').toggle($('#<%= cbAddnotes.ClientID%>').is(':checked'));
            $('#<%= cbAddnotes.ClientID%>').click(function () {
                $('#<%= dvNotesForm.ClientID%>').toggle(this.checked);
            }).change();
        });
    </script>
</asp:Content>
