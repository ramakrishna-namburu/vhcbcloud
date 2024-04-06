<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TownPlaning.aspx.cs" Inherits="ConserveNatRec.TownPlaning" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>


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
        <p class="lead">5. TOWN PLANNING, ZONING & SUPPORT</p>
        <div class="container">
            <div class="panel panel-default">
                <div id="dvEntityRole" runat="server">
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <div id="dvMessage" runat="server" visible="false">
                        <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg" ForeColor="Red"></asp:Label></p>
                    </div>

                    <table style="width: 100%">
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px; width: 50%">
                                <span class="labelClass" style="margin-left: 10px">Within what zoning district(s) is the farm located?</span></td>
                            <td style="width: 50%">
                                <asp:TextBox ID="txtZoningDistrict" CssClass="clsTextBoxBlue1" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px; width: 50%">
                                <span class="labelClass" style="margin-left: 10px">What are the allowed minimum lot sizes?</span></td>
                            <td style="width: 50%">
                                <asp:TextBox ID="txtMinLotSize" CssClass="clsTextBoxBlue1" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px; width: 50%">
                                <span class="labelClass" style="margin-left: 10px">How many feet of public road frontage is along the area to be conserved? (Note: if property straddles a road, count frontage on both sides.)</span></td>
                            <td style="width: 50%">
                                <asp:TextBox ID="txtFrontageFeet" CssClass="clsTextBoxBlue1" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 10px; width: 50%">
                                <span class="labelClass" style="margin-left: 10px">Is there a public water supply on the property?</span></td>
                            <td style="width: 50%">
                                <asp:RadioButtonList ID="rdbtnPublicWater" runat="server" CellPadding="2" CellSpacing="4"
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
                            <td style="margin-left: 10px; width: 50%">
                                <span class="labelClass" style="margin-left: 10px">Is the farm on a public sewer system?</span></td>
                            <td style="width: 50%">
                                <asp:RadioButtonList ID="rdbtnPublicSewer" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                       <%-- <tr>
                            <td style="margin-left: 10px; width: 50%">
                                <span class="labelClass" style="margin-left: 10px">6. Is the farm enrolled in Vermont's use-value appraisal (UVA) program?</span></td>
                            <td style="width: 50%">
                                <asp:RadioButtonList ID="rdbtnEnrolledUseValue" runat="server" CellPadding="2" CellSpacing="4"
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
                            <td style="margin-left: 10px; width: 50%">
                                <span class="labelClass" style="margin-left: 10px">7. How is the conserved acreage derived?</span></td>
                            <td style="width: 50%">
                                <asp:TextBox ID="txtAcresDerived" CssClass="clsTextBoxBlue1" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td style="margin-left: 10px; width: 50%">
                                <span class="labelClass" style="margin-left: 10px">8.  Total acreage excluded from conserved land:</span></td>
                            <td style="width: 50%">
                                <asp:TextBox ID="txtAcresExcluded" CssClass="clsTextBoxBlue1" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">If land is to be excluded, please describe why the land will not be conserved:</span></td>

                        </tr>

                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtExcludedLand" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>--%>
                        <tr>
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">Does the deed match the mapped acreage?</span></td>

                        </tr>

                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtDeedMatch" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <%--<tr>
                            <td style="margin-left: 10px; width: 80%">
                                <span class="labelClass" style="margin-left: 10px">10. Will a survey be required by NRCS*? *if yes, VHCB can pay up to $3,000 towards a survey.  Please include in budget.</span></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList ID="rdbtnSurveyRequired" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>--%>
                        <tr>
                            <td style="margin-left: 10px; width: 80%">
                                <span class="labelClass" style="margin-left: 10px">To the best knowledge of the landowner, are there any deed restrictions, such as restrictive covenants or mineral rights, which encumber use of the property?</span></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList ID="rdbtnRestrictiveCovenants" runat="server" CellPadding="2" CellSpacing="4"
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
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">If yes, please describe.  A full title search will be required prior to disbursement of VHCB funds.</span></td>

                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtDeedRestrictions" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">Describe how this project is in conformance with adopted or proposed  local and regional plans and zoning.</span></td>

                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtConformancePlans" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">Whenever possible, VHCB encourages applicants to bring leverages (i.e., match) to the table in the formof landowner bargain sales and/or local fundraising (i.e., town conservation fund, land trust fundraising).  If this project does not include any leverage, please describe why it is not possible and what fundraising possibilities (if any) were explored.</span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtNoLeverage" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">VHCB requires applicants to inform towns and Regional Planning Commissions in writing about proposed conservation projects prior to submitting an application to VHCB.  Please confirm to whom a letter was sent.</span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 30px">
                                  <asp:CheckBoxList ID="cblPlanCommisionsInformed" runat="server" RepeatDirection="Horizontal" CssClass="cblPlanCommisionsInformedStyle">
                                      <asp:ListItem Value="Municipal officials">Municipal officials</asp:ListItem>
                                      <asp:ListItem Value="Regional Planning Commission">Regional Planning Commission</asp:ListItem>
                                      <asp:ListItem Value="Other">Other</asp:ListItem>
                                  </asp:CheckBoxList>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">Describe any endorsements or other indications of community support for this project, if applicable</span></td>

                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtEndorsements" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
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
                                                      <%--  <asp:ListItem Text="Farm Management" Value="FarmManagement.aspx"></asp:ListItem>--%>
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


        });

    </script>
</asp:Content>





