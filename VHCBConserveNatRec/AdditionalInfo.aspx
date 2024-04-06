<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdditionalInfo.aspx.cs" Inherits="ConserveNatRec.AdditionalInfo" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>


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
        <p class="lead">6.  ADDITIONAL INFORMATION</p>
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
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">1. Whenever possible, VHCB endeavors to support dual goal projects - projects that support both affordable housing and conservation. If applicable, describe how this project supports affordable housing</span></td>

                        </tr>

                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtDualGoals" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="8" runat="server" Width="971px" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px"></td>
                        </tr>


                        <tr>
                            <td colspan="2" style="margin-left: 10px;">
                                <span class="labelClass" style="margin-left: 10px">2. Extra space – if needed, use this space to add anything you wish to clarify about your proposed project:</span></td>

                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtAdditionalInfo" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
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
                                                        <%--<asp:ListItem Text="Farm Management" Value="FarmManagement.aspx"></asp:ListItem>--%>
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






