<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FarmManagement.aspx.cs" Inherits="VHCBConservationFarm.Page5" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--    <link rel="stylesheet" runat="server" media="screen" href="~/Content/StyleSheet.css" />--%>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/CurrencyController.js"></script>
    <style type="text/css">
        .FormatRadioButtonList label {
            margin-left: 5px;
        }

        .auto-style7 {
            width: 202px;
        }
    </style>

    <div class="jumbotron">
        <p class="lead">FARM OPERATION, MANAGEMENT AND INFRASTRUCTURE</p>
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
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">1. How does the Vermont Agency of Agriculture classify the farm?</span></td>
                            <td>
                                <asp:DropDownList ID="ddlFarmSize" CssClass="clsDropDown" runat="server" Height="23px" Width="195px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">2. Is the farm in compliance with VT Required Agricultural Practices (RAPs)?</span> </td>
                            <td>
                                <asp:RadioButtonList ID="rdbtnRAPCompliance" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">3. Acres of land rented from others that is part of the same operation and relates to the land being conserved</span></td>
                            <td>
                                <asp:TextBox ID="txtRentedLand" CssClass="clsTextBoxBlue1" runat="server" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px">4. Total employees (including family members + self)</span></td>
                            <td class="auto-style7">
                                <span class="labelClass" style="margin-left: 10px">Full-Time Year-Round</span>&nbsp; &nbsp;
                                <asp:TextBox ID="txtFullTime" CssClass="clsTextBoxBlue1" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                <span class="labelClass" style="margin-left: 10px">Part-Time Year-Round</span>&nbsp; &nbsp;
                                <asp:TextBox ID="txtPartTime" CssClass="clsTextBoxBlue1" runat="server" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td><span class="labelClass" style="margin-left: 10px"></span></td>
                            <td class="auto-style7">
                                <span class="labelClass" style="margin-left: 10px">Full-Time Seasonal</span>&nbsp; &nbsp;&nbsp; &nbsp;
                                <asp:TextBox ID="txtFullTimeSeasonal" CssClass="clsTextBoxBlue1" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                <span class="labelClass" style="margin-left: 10px">Part-Time Seasonal</span>&nbsp; &nbsp;&nbsp; &nbsp;
                                <asp:TextBox ID="txtPartTimeSeasonal" CssClass="clsTextBoxBlue1" runat="server" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">5. Annual gross income from farming </span></td>
                            <td>
                                <asp:TextBox ID="txtGrossIncome" CssClass="clsTextBoxBlue1" runat="server" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">Is this gross farm income for the current farmland owner, or for a farm operator who is a prospective farm buyer or tenant?</span></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px">
                                <asp:TextBox ID="txtGrossIncomeDescription" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="971px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">6. Where are farm products sold?</span></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px">
                                <asp:TextBox ID="txtProductsSold" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="971px" />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">7. Has the farmer worked with the VHCB Farm and Forest Viability Program on business planning or other support services?</span></td>
                            <td>&nbsp;<asp:RadioButtonList ID="rdbtWorkedWithVHCB" runat="server" CellPadding="2" CellSpacing="4" OnSelectedIndexChanged="rdbtWorkedWithVHCB_SelectedIndexChanged" AutoPostBack="true"
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                <asp:ListItem> No &nbsp;</asp:ListItem>
                            </asp:RadioButtonList>

                            </td>
                        </tr>
                      
                      <%--  <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>--%>
                        <tr>
                            <td colspan="3"><span class="labelClass" style="margin-left: 10px" runat="server" visible="false" id="spanVHCBDesc">If yes, describe how the farmer has worked with Viability?</span></td>
                        </tr>
                          <tr>
                            <td colspan="3" style="height: 10px">
                                <asp:TextBox ID="txtVHCBWorkDescription" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="971px" Visible="false"/>
                            </td>
                        </tr>
                        <%--<tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>--%>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">8. If the parcel is rented, is there a written lease?</span></td>
                            <td>
                                <asp:RadioButtonList ID="rdbtWrittenLease" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                  

                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">9. Does the farm have Highly Erodible Land (HEL), as defined by NRCS? </span></td>
                            <td>
                                 <asp:RadioButtonList ID="rdbtnHEL" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>

                             

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">10. Does the farm have a nutrient management plan up to NRCS standards? </span></td>
                            <td>
                                <asp:RadioButtonList ID="rdbtnNutrientPlan" runat="server" CellPadding="2" CellSpacing="4"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>

                               
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">11. Are there are any dumps/significant trash piles on the property? </span></td>
                            <td>
                                <asp:DropDownList ID="ddlDumps" CssClass="clsDropDown" runat="server" Height="23px" Width="95px">
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Unsure</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
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
                    <table>
                        
                       <%-- <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">Other</span></td>
                            <td>
                                <asp:TextBox ID="txtOtherConservationMeasures" CssClass="clsTextBoxBlue1" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td colspan="3"><span class="labelClass" style="margin-left: 10px">13. Describe any aspects of the farm's operation that help to mitigate and/or adapt to climate change (i.e. new crops/rotations 
                                to manage risk of extreme weather events, renewable energy projects, hoop houses, etc.)</span></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px">
                                <asp:TextBox ID="txtMitigateClimate" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="971px" />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span class="labelClass" style="margin-left: 10px">14. Is there existing farm infrastructure (which could include housing) within the project area? </span></td>
                            <td>
                                <asp:RadioButtonList ID="rdbExistingInfrastructure" runat="server" CellPadding="2" CellSpacing="4" OnSelectedIndexChanged="rdbExistingInfrastructure_SelectedIndexChanged" AutoPostBack="true"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Yes &nbsp;</asp:ListItem>
                                    <asp:ListItem> No &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="3"><span class="labelClass" style="margin-left: 10px" runat="server" visible="false" id="spanExistingInfra">If yes, please describe</span></td>

                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px">
                                <asp:TextBox ID="txtExistingInfrastuctureDescription" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="971px" Visible="false" />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td colspan="3"><span class="labelClass" style="margin-left: 10px">15. Describe anything else about the farm operation and its management that you have not addressed elsewhere:</span></td>

                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px">
                                <asp:TextBox ID="txtFarmOperation" TextMode="multiline" CssClass="clsTextBoxBlue1" Columns="50" Rows="2" runat="server" Width="971px" />

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
                       <asp:HiddenField ID="hfConserveId" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script language="javascript">

        $(document).ready(function () {
            toCurrencyControl($('#<%= txtGrossIncome.ClientID%>').val(), $('#<%= txtGrossIncome.ClientID%>'));

            $('#<%= txtGrossIncome.ClientID%>').keyup(function () {
                toCurrencyControl($('#<%= txtGrossIncome.ClientID%>').val(), $('#<%= txtGrossIncome.ClientID%>'));
            });

            $('#<%= dvCMForm.ClientID%>').toggle($('#<%= cbAddCM.ClientID%>').is(':checked'));
            $('#<%= cbAddCM.ClientID%>').click(function () {
                $('#<%= dvCMForm.ClientID%>').toggle(this.checked);
            }).change();
        });

    </script>
</asp:Content>




