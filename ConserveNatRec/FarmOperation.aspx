﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmOperation.aspx.cs" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" Inherits="ConserveNatRec.FarmOperation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" runat="server" media="screen" href="~/Content/StyleSheet.css" />
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/CurrencyController.js"></script>
    <style type="text/css">
        .checkboxlist_nowrap label {
            display: inline;
            margin-left: 5px;
            font-weight: normal !important;
        }

        .FormatRadioButtonList label {
            margin-left: 5px;
        }

        .auto-style7 {
            width: 274px;
        }

        .auto-style9 {
            width: 216px;
        }

        .auto-style10 {
            width: 271px;
        }

        .auto-style12 {
            height: 10px;
            width: 416px;
        }

        .auto-style13 {
            width: 416px;
        }

        .auto-style16 {
            width: 492px;
        }

        .auto-style17 {
            width: 365px;
        }
    </style>
    <div class="jumbotron">
        <p class="lead">Farm Operation, Management and Infrastructure</p>
        <div class="container">
            <div class="panel panel-default">
                <div id="dvEntityRole" runat="server">
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <div id="dvMessage" runat="server" visible="false">
                        <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg" ForeColor="Red"></asp:Label></p>
                    </div>
                    <table>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">How does the Vermont Agency of Agriculture classify the farm?</span></td>
                            <td>&nbsp;&nbsp;
                                 <asp:DropDownList ID="ddlFarmClassification" CssClass="clsDropDown" runat="server" Height="23px" Width="185px">
                                 </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">Is the farm in compliance with VT Required Agricultural Practices - (RAPs)?</span></td>
                            <td>&nbsp;&nbsp;
                               <asp:RadioButtonList ID="RadioButtonList1" runat="server"
                                   RepeatDirection="Horizontal">
                                   <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                   <asp:ListItem> No &nbsp;</asp:ListItem>
                               </asp:RadioButtonList>
                            </td>
                            <td></td>
                        </tr>
                        

                        <tr>
                            <td colspan="3" style="height: 10px">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">Acres of land rented from others that is part of the same operation and relates to the conserved land:</span></td>
                            <td>&nbsp;&nbsp;
                                <asp:TextBox ID="txtRentedLand" CssClass="clsTextBoxBlue1" runat="server" Width="200px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                         <tr>
                            <td><span class="labelClass" style="margin-left: 10px">Total employees (including family members + self)</span></td>
                            <td class="auto-style7">
                                <span class="labelClass" style="margin-left: 10px">Full-Time Year-Round</span>&nbsp; &nbsp;
                                <asp:TextBox ID="txtFullTime" CssClass="clsTextBoxBlue1" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                <span class="labelClass" style="margin-left: 10px">Part-Time Year-Round</span>&nbsp; &nbsp;
                                <asp:TextBox ID="txtPartTime" CssClass="clsTextBoxBlue1" runat="server" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <%-- <tr>
                            <td class="auto-style7"><span class="labelClass" style="margin-left: 10px">Project Name</span></td>
                            <td colspan="2">
                                <span class="labelClass" runat="server" id="spnProjectName" visible="true"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td class="auto-style7"><span class="labelClass" style="margin-left: 10px">Date Submitted</span></td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDateSubmitted" CssClass="clsTextBoxBlue1" runat="server" MaxLength="20"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender runat="server" ID="ce_txtDateSubmitted" TargetControlID="txtDateSubmitted">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td class="auto-style7"><span class="labelClass" style="margin-left: 10px">Board Meeting Date</span></td>
                            <td colspan="2"></td>
                        </tr>--%>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td class="auto-style17"><span class="labelClass" style="margin-left: 10px">Total Number of Acres to be Conserved</span></td>
                            <td colspan="2" class="auto-style16">
                                <asp:TextBox ID="txtConservedAcres" CssClass="clsTextBoxBlue1" runat="server" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td class="auto-style17"><span class="labelClass" style="margin-left: 10px">Total Funds Requested from VHCB (VHCB and NRCS funds)</span></td>
                            <td colspan="2" class="auto-style16">
                                <asp:TextBox ID="txtFundsRequested" CssClass="clsTextBoxBlue1" runat="server" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td class="auto-style17"><span class="labelClass" style="margin-left: 10px">Total Project Expenses (from budget )</span></td>
                            <td colspan="2" class="auto-style16">
                                <asp:TextBox ID="txtTotalExpenses" CssClass="clsTextBoxBlue1" runat="server" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="height: 10px">
                                <div>
                                    <span class="labelClass" style="margin-left: 10px">Because VHCB awards are a matter of public record, VHCB may describe this project in a press release following the Board's decision.  Should we notify you in advance before publicizing this project?
                                    </span>
                                    <asp:RadioButtonList ID="rdbtNotify" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                        <asp:ListItem> No &nbsp;</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 5px">
                                <hr />
                            </td>
                        </tr>
                    </table>

                    <table>
                        <tr>
                            <td colspan="2" style="text-decoration: underline;" class="auto-style7"><strong>Applicant Contact Information</strong></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">Project Director Name</span></td>
                            <td colspan="2">
                                <asp:TextBox ID="txtProjectManager" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">Email</span></td>
                            <td colspan="2">
                                <asp:TextBox ID="txtAppEmail" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">Work Phone</span></td>
                            <td colspan="2">
                                <asp:TextBox ID="txtAppPhone" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtAppPhone"
                                    Mask="(999)-999-9999" MessageValidatorTip="true" ErrorTooltipEnabled="True">
                                </asp:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">Cell Phone</span></td>
                            <td colspan="2">
                                <asp:TextBox ID="txtAppCellPhone" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtAppCellPhone"
                                    Mask="(999)-999-9999" MessageValidatorTip="true" ErrorTooltipEnabled="True">
                                </asp:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 10px">
                                <hr />
                            </td>
                        </tr>
                    </table>



                    <div class="panel-width" runat="server" id="dvNew">
                        <table>
                            <tr>
                                <td colspan="2" style="text-decoration: underline;" class="auto-style7"><strong>Landowner/Farmer</strong></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td><span class="labelClass" style="margin-left: 10px">Name</span></td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtLONames" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                <tr>
                                    <td colspan="3" style="height: 10px"></td>
                                </tr>
                        </table>
                        <div class="panel panel-default" style="margin-bottom: 2px;">
                            <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h3 class="panel-title">Mailing Address</h3>
                                        </td>
                                        <td style="text-align: right"></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="panel-body" runat="server" id="dvProjectAddressForm">
                                <asp:Panel runat="server" ID="Panel2">

                                    <div id="dvAddress" runat="server">

                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 150px"><span class="labelClass">Street #</span></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtloStreetNo" CssClass="clsTextBoxBlue1" runat="server" MaxLength="12"></asp:TextBox>
                                                </td>

                                                <td style="width: 100px"><span class="labelClass">Address1:</span></td>
                                                <td style="width: 270px">
                                                    <asp:TextBox ID="txtLoAddress1" CssClass="clsTextBoxBlue1" runat="server" MaxLength="60"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">Address2</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLoAddress2" CssClass="clsTextBoxBlue1" runat="server" MaxLength="60"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 170px"><span class="labelClass">Town</span></td>
                                                <td class="auto-style9">
                                                    <asp:DropDownList ID="ddlLoTown" CssClass="clsDropDown" runat="server" Height="23px" Width="185px" AutoPostBack="true" OnSelectedIndexChanged="ddlLoTown_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </td>
                                                <td style="width: 150px"><span class="labelClass">Zip Code</span></td>
                                                <td style="width: 250px">
                                                    <asp:TextBox ID="txtLOZipCode" CssClass="clsTextBoxBlue1" runat="server" MaxLength="10"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">County</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:Label runat="server" ID="lblLoCounty" class="labelClass"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px"><span class="labelClass" runat="server" id="spnVillage" visible="true">Email</span></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtLOEmail" CssClass="clsTextBoxBlue1" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px">
                                                    <span class="labelClass">Home phone</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:TextBox ID="txtLOHomephone" CssClass="clsTextBoxBlue1" runat="server" MaxLength="10"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtLOHomephone"
                                                        Mask="(999)-999-9999" MessageValidatorTip="true" ErrorTooltipEnabled="True">
                                                    </asp:MaskedEditExtender>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">Cell phone</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLoCellPhone" CssClass="clsTextBoxBlue1" runat="server" MaxLength="10"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtLoCellPhone"
                                                        Mask="(999)-999-9999" MessageValidatorTip="true" ErrorTooltipEnabled="True">
                                                    </asp:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <%-- <table>

                            <tr>
                                <td colspan="3" style="text-decoration: underline;"><strong>Farmer/Landowner Mailing Address & Contact Information</strong></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td colspan="3" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td><span class="labelClass" style="margin-left: 10px">Name      </span></td>
                                <td colspan="2" style="margin-left: 30px">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtFarmerName" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                <tr>
                                    <td colspan="3" style="height: 10px"></td>
                                </tr>
                        </table>--%>
                        <%--<div class="panel panel-default" style="margin-bottom: 2px;">
                             <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h3 class="panel-title">Mailing Address</h3>
                                        </td>
                                        <td style="text-align: right"></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="panel-body" runat="server" id="Div1">
                                <asp:Panel runat="server" ID="Panel1">

                                    <div id="Div2" runat="server">

                                        <table style="width: 100%">
                                            <%-- <tr>
                                                <td style="width: 150px"><span class="labelClass">Street #</span></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtFarmerStreet" CssClass="clsTextBoxBlue1" runat="server" MaxLength="12"></asp:TextBox>
                                                </td>

                                                <td style="width: 100px"><span class="labelClass">Address1:</span></td>
                                                <td style="width: 270px">
                                                    <asp:TextBox ID="txtFarmerAdd1" CssClass="clsTextBoxBlue1" runat="server" MaxLength="60"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">Address2</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFarmerAdd2" CssClass="clsTextBoxBlue1" runat="server" MaxLength="60"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 170px"><span class="labelClass">Town</span></td>
                                                <td class="auto-style9">
                                                   
                                                      <asp:DropDownList ID="ddlFarmerTown" CssClass="clsDropDown" runat="server" Height="23px" Width="185px" AutoPostBack="true" OnSelectedIndexChanged="ddlFarmerTown_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 150px"><span class="labelClass">Zip Code</span></td>
                                                <td style="width: 250px">
                                                    <asp:TextBox ID="txtFarmerZip" CssClass="clsTextBoxBlue1" runat="server" MaxLength="10"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">
                                                   <span class="labelClass">County</span>
                                                </td>
                                                <td style="width: 270px">
                                                     <asp:Label runat="server" ID="lblFarmerCounty" class="labelClass"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80px"><span class="labelClass" runat="server" id="Span1" visible="true">Email</span></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtFarmerEmail" CssClass="clsTextBoxBlue1" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px">
                                                    <span class="labelClass">Home phone</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:TextBox ID="txtFarmerHomePhone" CssClass="clsTextBoxBlue1" runat="server" MaxLength="10"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtFarmerHomePhone"
                                                        Mask="(999)-999-9999" MessageValidatorTip="true" ErrorTooltipEnabled="True">
                                                    </asp:MaskedEditExtender>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">Cell phone</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFarmerCell" CssClass="clsTextBoxBlue1" runat="server" MaxLength="10"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtFarmerCell"
                                                        Mask="(999)-999-9999" MessageValidatorTip="true" ErrorTooltipEnabled="True">
                                                    </asp:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>


                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>--%>

                        <table>
                            <tr>
                                <td colspan="6" style="height: 15px"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-decoration: underline;" class="auto-style13"><strong>PROPERTY TO BE CONSERVED (if different from above) </strong></td>
                            </tr>
                            <tr>
                                <td colspan="3" class="auto-style12"></td>
                            </tr>
                            <%-- <tr>
                                <td><span class="labelClass" style="margin-left: 10px">Name(s)</span></td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtPropertyName" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                <tr>
                                    <td colspan="3" style="height: 10px"></td>
                                </tr>--%>
                        </table>
                        <div class="panel panel-default" style="margin-bottom: 2px;">
                            <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h3 class="panel-title">Address</h3>
                                        </td>
                                        <td style="text-align: right"></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="panel-body" runat="server" id="Div3">
                                <asp:Panel runat="server" ID="Panel3">

                                    <div id="Div4" runat="server">

                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 150px"><span class="labelClass">Street #</span></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtPropertyStreet" CssClass="clsTextBoxBlue1" runat="server" MaxLength="12"></asp:TextBox>
                                                </td>

                                                <td style="width: 100px"><span class="labelClass">Address1:</span></td>
                                                <td style="width: 270px">
                                                    <asp:TextBox ID="txtPropertyAdd1" CssClass="clsTextBoxBlue1" runat="server" MaxLength="60"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">Address2</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPropertyAdd2" CssClass="clsTextBoxBlue1" runat="server" MaxLength="60"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 170px"><span class="labelClass">Town</span></td>
                                                <td class="auto-style9">
                                                    <asp:DropDownList ID="ddlPropertyTown" CssClass="clsDropDown" runat="server" Height="23px" Width="185px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPropertyTown_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <%-- <td style="width: 150px"><span class="labelClass">Other Town(s)</span></td>
                                                <td style="width: 250px">
                                                    <asp:TextBox ID="txtPropertyOtherTown" CssClass="clsTextBoxBlue1" runat="server" MaxLength="10"></asp:TextBox>
                                                </td>--%>
                                                <td class="auto-style10">
                                                    <span class="labelClass">Zip</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:TextBox ID="txtPropertyZip" CssClass="clsTextBoxBlue1" runat="server" MaxLength="20"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">County</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:Label runat="server" ID="lblPropertyCounty" class="labelClass"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <%-- <td class="auto-style10">
                                                    <span class="labelClass">County</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:Label runat="server" ID="lblPropertyCounty" class="labelClass"></asp:Label>
                                                </td>--%>
                                                <td style="width: 100px">
                                                    <span class="labelClass" runat="server" id="Span2" visible="true"></span>
                                                </td>
                                                <td style="width: 270px"></td>
                                                <td class="auto-style10"><span class="labelClass"></span>

                                                </td>
                                                <td></td>
                                            </tr>



                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <table>
                            <tr>
                                <td colspan="6" style="height: 15px"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-decoration: underline;" class="auto-style13"><strong>Contact of proposed new landowner or leasing farmer</strong></td>
                            </tr>
                            <tr>
                                <td colspan="3" class="auto-style12"></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td colspan="4" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td><span class="labelClass" style="margin-left: 10px">Primary Contact’s Name</span></td>
                                <td style="margin-left: 30px">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtProposedContact" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                </td>
                                <td><span class="labelClass" style="margin-left: 10px">Email</span></td>
                                <td style="margin-left: 30px">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtProposedEmail" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td><span class="labelClass" style="margin-left: 10px">Home Phone</span></td>
                                <td style="margin-left: 30px">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtProposedHomePhone" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtProposedHomePhone"
                                        Mask="(999)-999-9999" MessageValidatorTip="true" ErrorTooltipEnabled="True">
                                    </asp:MaskedEditExtender>
                                </td>
                                <td><span class="labelClass" style="margin-left: 10px">Cell Phone</span></td>
                                <td style="margin-left: 30px">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtProposedCellPhone" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="txtProposedCellPhone"
                                        Mask="(999)-999-9999" MessageValidatorTip="true" ErrorTooltipEnabled="True">
                                    </asp:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td><span class="labelClass" style="margin-left: 10px">Relationship to project (i.e., farmer, new owner)</span></td>
                                <td style="margin-left: 30px">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtProposedRelation" CssClass="clsTextBoxBlue1" runat="server" Width="253px"></asp:TextBox>
                                </td>
                                <td><span class="labelClass" style="margin-left: 10px"></span></td>
                                <td style="margin-left: 30px">&nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 10px"></td>
                            </tr>
                        </table>
                        <div class="panel panel-default" style="margin-bottom: 2px;">
                            <div class="panel-heading" style="padding: 5px 5px 1px 5px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h3 class="panel-title">Address</h3>
                                        </td>
                                        <td style="text-align: right"></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="panel-body" runat="server" id="Div5">
                                <asp:Panel runat="server" ID="Panel4">

                                    <div id="Div6" runat="server">

                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 150px"><span class="labelClass">Street #</span></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtProposedStreet" CssClass="clsTextBoxBlue1" runat="server" MaxLength="12"></asp:TextBox>
                                                </td>

                                                <td style="width: 100px"><span class="labelClass">Address1:</span></td>
                                                <td style="width: 270px">
                                                    <asp:TextBox ID="txtProposedAdd1" CssClass="clsTextBoxBlue1" runat="server" MaxLength="60"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">Address2</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProposedAdd2" CssClass="clsTextBoxBlue1" runat="server" MaxLength="60"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 170px"><span class="labelClass">Town</span></td>
                                                <td class="auto-style9">
                                                    <asp:DropDownList ID="ddlProposedTown" CssClass="clsDropDown" runat="server" Height="23px" Width="185px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlProposedTown_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <%-- <td style="width: 150px"><span class="labelClass">Other Town(s)</span></td>
                                                <td style="width: 250px">
                                                    <asp:TextBox ID="txtPropertyOtherTown" CssClass="clsTextBoxBlue1" runat="server" MaxLength="10"></asp:TextBox>
                                                </td>--%>
                                                <td class="auto-style10">
                                                    <span class="labelClass">Zip</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:TextBox ID="txtProposedZIP" CssClass="clsTextBoxBlue1" runat="server" MaxLength="20"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">
                                                    <span class="labelClass">County</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:Label runat="server" ID="lblProposedCounty" class="labelClass"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <%-- <td class="auto-style10">
                                                    <span class="labelClass">County</span>
                                                </td>
                                                <td style="width: 270px">
                                                    <asp:Label runat="server" ID="lblPropertyCounty" class="labelClass"></asp:Label>
                                                </td>--%>
                                                <td style="width: 100px">
                                                    <span class="labelClass" runat="server" id="Span3" visible="true"></span>
                                                </td>
                                                <td style="width: 270px"></td>
                                                <td class="auto-style10"><span class="labelClass"></span>

                                                </td>
                                                <td></td>
                                            </tr>

                                            <tr>
                                                <td colspan="6" style="height: 5px">
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print Application PDF" class="btn btn-info" OnClick="btnPrint_Click" />
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

                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script language="javascript">

        $(document).ready(function () {
            toCurrencyControl($('#<%= txtFundsRequested.ClientID%>').val(), $('#<%= txtFundsRequested.ClientID%>'));
            toCurrencyControl($('#<%= txtTotalExpenses.ClientID%>').val(), $('#<%= txtTotalExpenses.ClientID%>'));

            $('#<%= txtFundsRequested.ClientID%>').keyup(function () {
                toCurrencyControl($('#<%= txtFundsRequested.ClientID%>').val(), $('#<%= txtFundsRequested.ClientID%>'));
            });

            $('#<%= txtTotalExpenses.ClientID%>').keyup(function () {
                toCurrencyControl($('#<%= txtTotalExpenses.ClientID%>').val(), $('#<%= txtTotalExpenses.ClientID%>'));
            });

        });

    </script>
</asp:Content>


