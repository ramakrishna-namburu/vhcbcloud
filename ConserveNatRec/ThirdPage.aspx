﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThirdPage.aspx.cs" Inherits="ConserveNatRec.ThirdPage" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" runat="server" media="screen" href="~/Content/StyleSheet.css" />
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/CurrencyController.js"></script>
    <style type="text/css">
        .FormatRadioButtonList label {
            margin-left: 5px;
        }

        .auto-style7 {
            width: 274px;
        }
        .auto-style9 {
            width: 567px;
        }
    </style>
    <div class="jumbotron">
        <p class="lead">Farm Conservation Application</p>
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
                            <td colspan="2" style="text-decoration: underline;" class="auto-style7"><strong>Executive Summary</strong>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="3"><span class="labelClass" style="margin-left: 10px">
                                a. Provide an overview of the project and its goals. Briefly describe the farm operation, farm history, primary conservation values, and other critical aspects of the project with a focus on how this project aligns with VHCB’s Funding of Agricultural Land policy. This summary should give the reader an overview of project highlights, leaving the details to be more fully described later in the application. Approximately 2-4 paragraphs. If more space is needed to explain particular aspects of the project, please see Page 10.</span></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="margin-left: 10px">
                                &nbsp;&nbsp;<asp:TextBox ID="txtExecSummary" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="971px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-decoration: underline;" class="auto-style7"><strong>FARM TRANSFER</strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td class="auto-style9"><span class="labelClass" style="margin-left: 10px">b. Are there plans to sell or convey all or part of the farm to new landowners?  </span></td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rdBtnSellorConvey" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rdBtnSellorConvey_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="margin-left: 10px">&nbsp;&nbsp;
                                <asp:TextBox ID="txtSellorPlans" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="974px" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                    </table>
                    <table runat="server" id="part2" visible="true">
                       <%-- <tr>
                            <td class="auto-style8"><span class="labelClass" style="margin-left: 10px">2. Is this farm transfer a: [check all that apply]: family transfer, new buyer, farm access program</span></td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="cblFarmerTransfer" runat="server">
                                    <asp:ListItem Value="Family Transfer">Family Transfer</asp:ListItem>
                                    <asp:ListItem Value="New Buyer">New Buyer</asp:ListItem>
                                    <asp:ListItem Value="Farm access program">Farm access program</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>--%>
                        <tr>
                            <td colspan="3"><span class="labelClass" style="margin-left: 10px">c. Incoming farmer(s) plans:  Briefly describe the incoming farmer(s)’ operation and their plans for this property.  Summarize the incoming farmer(s)’ experience with farming and any support services, such as business planning support, they are receiving. (Specifically note if the farmers are enrolled in VHCB’s Farm and Forest Viability program.) How will acquiring this parcel impact their farm operation?</span></td>

                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="margin-left: 10px">&nbsp;&nbsp;
                                <asp:TextBox ID="txtFarmerPlans" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="6" runat="server" Width="974px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
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
                                                    <asp:Label runat="server" ID="Label1" class="labelClass" Text ="Go To"></asp:Label>
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
    </div>

    <script language="javascript">

        $(document).ready(function () {
            $("#<%=rdBtnSellorConvey.ClientID%>").change(function(){
                var rbvalue = $("input[name='<%=rdBtnSellorConvey.UniqueID%>']:radio:checked").val();
                //alert(rbvalue);
                if (rbvalue === "Yes")
                  //  alert("Yes");
                    //alert($("#<%=part2.ClientID%>"));
            });
        });

    </script>
</asp:Content>


