﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Page8.aspx.cs" Inherits="vhcbExternalApp.Page8" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" runat="server" media="screen" href="~/Content/StyleSheet.css" />
    <div class="jumbotron">
        <p class="lead"></p>
        <div class="container">
            <div class="panel panel-default">
                <div id="dvEntityRole" runat="server">
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                     <div id="dvMessage" runat="server">
                        <p class="lblErrMsg">&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblErrorMsg" ForeColor="Red"></asp:Label></p>
                    </div>
                    <table style="width=100%">

                        <tr>
                            <td colspan="2"><span class="labelClass">4.	Please list all farm owners and managers, and their areas of responsibility. (100 words) <em><strong>(Required)</strong></em>
                            </span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">
                                <asp:TextBox ID="txtFarmOwners4" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="879px" Height="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass">5.	Please provide a brief narrative about surface waters or wetlands on the farm, if any. Indicate if there is a wetland, ditch, or river near the proposed project area. (100 words) <em><strong>(Required)</strong></em></span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">
                                <asp:TextBox ID="txtSurfaceWaters5" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="879px" Height="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass">6.	Please describe your major goals for the farm, both including short-term and long-term, and the actions you plan to take to maintain, improve, or grow your business. (250 words)<em><strong>(Required)</strong></em></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">
                                <asp:TextBox ID="txtMajorGoals6" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="879px" Height="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px"></td>
                        </tr>

                        <tr>
                            <td colspan="2" style="height: 10px">&nbsp;&nbsp;&nbsp;&nbsp;
                                                 <asp:Button ID="previousButton" runat="server" Text="Previous Page/Save" class="btn btn-info" OnClick="previousButton_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnNext" runat="server" Text="Next Page/Save" class="btn btn-info" OnClick="btnNext_Click" />
                                 &nbsp;&nbsp;<asp:Label runat="server" ID="Label1" class="labelClass" Text="Go To"></asp:Label>
                               <asp:DropDownList ID="ddlGoto" CssClass="clsDropDown" runat="server" Height="23px" Width="185px" AutoPostBack="true" OnSelectedIndexChanged="ddlGoto_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Applicant Information" Value="ProjectDetails.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Farm Business Information" Value="FarmBusinessInformation.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Farm Business Information - continued" Value="WaterQualityGrants.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Grant Request" Value="WaterQualityGrantsProgramPage4.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Budget Tables & Narrative" Value="BudgetNarrativeTables.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Narrative Questions" Value="Page7.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Narrative Questions - continued" Value="Page8.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Project Questions" Value="Page9.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Attachments" Value="Page11.aspx"></asp:ListItem>
                                            <asp:ListItem Text="Confidentiality/Submit" Value="Page10.aspx"></asp:ListItem>
                                        </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

