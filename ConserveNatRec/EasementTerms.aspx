<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EasementTerms.aspx.cs" Inherits="ConserveNatRec.EasementTerms" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" runat="server" media="screen" href="~/Content/StyleSheet.css" />
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/CurrencyController.js"></script>
    <style type="text/css">
        .FormatRadioButtonList label {
            margin-left: 5px;
        }

        .cblPlanCommisionsInformedStyle td {
            margin-right: 10px;
            padding-right: 20px;
        }
    </style>
    <div class="jumbotron">
        <p class="lead">4. EASEMENT TERMS</p>
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

                     <div class="panel-width" runat="server" id="dvNewAttribute">
                    <div class="panel panel-default" style="margin-bottom: 2px;">
                        <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Easement Attributes</h3>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="cbAddAttribute" runat="server" Text="Add New Easement Attribute" />
                                       <%-- <asp:ImageButton ID="ImgConservationAttributes" ImageUrl="~/Images/print.png" ToolTip="Conservation Attributes Report"
                                            Style="border: none; vertical-align: middle;" runat="server" OnClick="" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="panel-body" style="padding: 10px 15px 0px 15px" runat="server" id="dvAttributeForm">
                            <asp:Panel runat="server" ID="Panel8">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 140px"><span class="labelClass">Attribute</span></td>
                                        <td style="width: 215px">
                                            <asp:DropDownList ID="ddlAttribute" CssClass="clsDropDownLong" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 180px">
                                            <asp:Button ID="AddAttribute" runat="server" Text="Add" class="btn btn-info" OnClick="AddAttribute_Click" />
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

                        <div class="panel-body" style="padding: 10px 10px 10px 10px" id="dvAttributeGrid" runat="server">
                            <asp:Panel runat="server" ID="Panel9" Width="100%" Height="100px" ScrollBars="Vertical">
                                <asp:GridView ID="gvAttribute" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CssClass="gridView" PageSize="50" PagerSettings-Mode="NextPreviousFirstLast"
                                    GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true"
                                    OnRowEditing="gvAttribute_RowEditing" OnRowCancelingEdit="gvAttribute_RowCancelingEdit"
                                    OnRowUpdating="gvAttribute_RowUpdating">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;" LastPageText="&amp;gt;" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Conserve Attrib ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConserveAttribID" runat="Server" Text='<%# Eval("ConserveAttribID") %>' />
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
                                        GridLines="None" EnableTheming="True" AllowPaging="false" AllowSorting="true" ShowFooter="false"
                                        OnRowEditing="gvAcreage_RowEditing1" OnRowCancelingEdit="gvAcreage_RowCancelingEdit"
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
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">
                                <span class="labelClass" style="margin-left: 10px">Reserved right for a subdivision: describe reason for the subdivision and show location of subdivision on attached Ortho map.  If this project will be using ACEP-ALE funds,please confirm that both subdivided units are independently eligible for ACEP-ALE 
                                </span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtSubDivisionReason" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">
                                <span class="labelClass" style="margin-left: 10px">Describe whether there is any historic significance of infrastructure on the property, and whether it warrants any special protection.</span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtHistoricSignificance" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">
                                <span class="labelClass" style="margin-left: 10px">*** Other Easement Terms, or further description of terms above:</span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtEasementTermsOther" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
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

                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfProjectId" runat="server" />
        <asp:HiddenField ID="hfConserveId" runat="server" />
    </div>

    <script language="javascript">

        $(document).ready(function () {
            $('#<%= dvNewAcreageForm.ClientID%>').toggle($('#<%= cbAddAcreage.ClientID%>').is(':checked'));

            $('#<%= cbAddAcreage.ClientID%>').click(function () {
                $('#<%= dvNewAcreageForm.ClientID%>').toggle(this.checked);
            }).change();
            

            $('#<%= dvAttributeForm.ClientID%>').toggle($('#<%= cbAddAttribute.ClientID%>').is(':checked'));

            $('#<%= cbAddAttribute.ClientID%>').click(function () {
                $('#<%= dvAttributeForm.ClientID%>').toggle(this.checked);
            }).change();
        });

    </script>
</asp:Content>





