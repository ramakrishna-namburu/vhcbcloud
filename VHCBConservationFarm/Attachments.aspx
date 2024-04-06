<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Attachments.aspx.cs" Inherits="VHCBConservationFarm.Attachments" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>


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

        .auto-style7 {
            text-decoration: underline;
        }
    </style>
    <div class="jumbotron">
        <p class="lead">6.  Attachments</p>
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
                            <td style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td style="height: 10px">Please attach the following in the order shown and send as one PDF: 
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="height: 10px"> &nbsp;&nbsp;&nbsp;&nbsp; <a href="#" target="_blank" runat="server" id="UploadLink"><strong><span style="font-size: large">Use this link to upload your PDF</span></strong></a>
                            </td>
                        </tr>
                        
                         <tr>
                            <td style="height: 10px"><span class="auto-style7"><strong>Required</strong></span></td>
                        </tr>
                         <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="height: 10px; margin-left: 50px;">&nbsp;&nbsp;&nbsp; <strong>Fully Executed Purchase and Sale Agreement </strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp; <a href="https://www.vhcb.org/sites/default/files/programs/conservation/app/VHCB%20Farm%20Application%20Budget%20-%202022.xlsx" target="_blank" runat="server" id="A1">Budget (please use budget form found here)</a>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp; <strong>Ecological Assessment Report</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp;  Maps:
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>Locator Map</strong> (showing property location, road names, and proximity to other  conserved land)
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>Orthophoto Map</strong> (showing location and acreage of property, and acreages of main  buildings, excluded land, and special protection zone(s))
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>Agricultural Soils Map</strong> (including labels of soil types, and a table breaking down  prime and statewide soil totals) 
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>Surface Water Planning Map </strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>USGS Topographic Map</strong> (show confirmed locations of rare, threatened and  
endangered species and/or significant natural communities, important wetlands, and  deeryards, if possible.) 

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>Floodplain map</strong> (if applicable) 
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>Photographs</strong> (showing land and buildings. maximum 8; with captions)
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;<span class="auto-style7"><strong>Optional:</strong></span>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp; <strong>Cover sheet</strong> (including project name, photo, and 3-5 highlights)
                           
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp; <strong>Tax Parcel Map</strong>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp; <strong>Farm Business Plan
                           
                            </strong>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp; <strong>Supplemental Narrative
                           
                            </strong>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp; <strong>Letters of support</strong> (town Selectboard, business planner, etc.) 
                           
                            </td>
                        </tr>
                        <%--<tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;&nbsp;&nbsp; <strong>Other:</strong> __________________ 
                           
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="margin-left: 50px;">&nbsp;<strong>If any of the required documents are not attached, please explain why, and when  VHCB can expect them: </strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                         <tr>
                            <td style="margin-left: 10px">&nbsp;&nbsp;<asp:TextBox ID="txtMissingDocs" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="3" runat="server" Width="971px" />
                            </td>
                        </tr>
                         <tr>
                            <td style="height: 10px"></td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                       <tr>
                            <td colspan="2"><span class="labelClass">Signature</span>
                                <asp:TextBox ID="txtSignature" CssClass="clsTextBoxBlueSm" Width="531px" Height="28px" runat="server"></asp:TextBox>
                                <span class="labelClass">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date </span>
                                <asp:TextBox ID="txtSig_Date" CssClass="clsTextBoxBlueSm" Width="202px" Height="28px" runat="server"></asp:TextBox>
                                 <ajaxToolkit:CalendarExtender runat="server" ID="aceTransDate" TargetControlID="txtSig_Date"></ajaxToolkit:CalendarExtender>
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
                                   <asp:Button ID="btnSaveExit" runat="server" Text="Save/Exit" class="btn btn-info" OnClick="btnSaveExit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnNext" runat="server" Text="Submit" class="btn btn-info" OnClick="btnNext_Click"/>
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


        });

    </script>
</asp:Content>







