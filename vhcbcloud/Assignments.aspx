﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Assignments.aspx.cs" Inherits="vhcbcloud.Assignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="jumbotron clearfix" id="vhcb">

                <p class="lead">Board Assignment</p>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:RadioButtonList ID="rdBtnFinancial" runat="server" AutoPostBack="true" CellPadding="2" CellSpacing="4" onclick="needToConfirm = true;"
                                RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rdBtnFinancial_SelectedIndexChanged">
                                <asp:ListItem> Commitment &nbsp;</asp:ListItem>
                                <asp:ListItem> DeCommitment &nbsp;</asp:ListItem>
                                <asp:ListItem> Reallocation &nbsp;</asp:ListItem>
                                <asp:ListItem Selected="true"> Assignments &nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlReallocations" runat="server" Visible="true">
                    <div class="container">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rdBtnSelection" Visible="true" runat="server" Width="150px" AutoPostBack="True" RepeatDirection="Horizontal" onclick="needToConfirm = true;"
                                                OnSelectedIndexChanged="rdBtnSelection_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">New    </asp:ListItem>
                                                <asp:ListItem>Existing</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgSearch" ImageUrl="~/Images/search.png" ToolTip="Project Search" Style="border: none;" runat="server" Text="Project Search" Visible="true"
                                                OnClientClick="PopupProjectSearch(); return false;"></asp:ImageButton>
                                            &nbsp;
                                       
                                            <asp:ImageButton ID="imgNewAwardSummary" ImageUrl="~/Images/$$.png" ToolTip="Award summary" Style="border: none;" runat="server" Text="Award Summary" Visible="true"
                                                OnClientClick="PopupNewAwardSummary(); return false;"></asp:ImageButton>
                                            <asp:ImageButton ID="imgExistingAwardSummary" ImageUrl="~/Images/$$.png" ToolTip="Award summary" Style="border: none;" runat="server" Text="Award Summary" Visible="false"
                                                OnClientClick="PopupExistingAwardSummary(); return false;"></asp:ImageButton>
                                            &nbsp;
                               
                                            <asp:ImageButton ID="btnProjectNotes" ImageUrl="~/Images/notes.png" ToolTip="Notes" runat="server" Text="Project Notes" Style="border: none;"></asp:ImageButton>
                                            &nbsp;
                               
                                            <asp:CheckBox ID="cbActiveOnly" runat="server" Text="Active Only" Checked="true" AutoPostBack="true" OnCheckedChanged="cbActiveOnly_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <ajaxToolkit:ModalPopupExtender ID="mpExtender" runat="server" PopupControlID="pnlProjectNotes" TargetControlID="btnProjectNotes" CancelControlID="btnClose"
                                BackgroundCssClass="MEBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="pnlProjectNotes" runat="server" CssClass="MEPopup" align="center" Style="display: none">
                                <iframe style="width: 750px; height: 600px;" id="ifProjectNotes" src="ProjectNotes.aspx" runat="server"></iframe>
                                <br />
                                <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-info" />

                            </asp:Panel>

                            <div class="panel-heading">Assign from</div>
                            <div class="panel-body" id="pnlReallocateFrom" runat="server">
                                <table style="width: 100%" class="">
                                    <tr>
                                        <td style="width: 10%; float: left"><span class="labelClass">Project # :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:DropDownList ID="ddlRFromProj" CssClass="clsDropDown" AutoPostBack="true" runat="server" Visible="false" onclick="needToConfirm = false;" OnSelectedIndexChanged="ddlRFromProj_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="txtFromProjNum" runat="server" Visible="true" CssClass="clsTextBoxBlueSm" Width="120px" TabIndex="1"></asp:TextBox>

                                            <ajaxToolkit:AutoCompleteExtender ID="aaceProjName" runat="server" TargetControlID="txtFromProjNum" MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="1"
                                                OnClientItemSelected="OnContactSelected" CompletionInterval="100" ServiceMethod="GetCommittedPendingProjectslistByFilter">
                                            </ajaxToolkit:AutoCompleteExtender>--%>

                                            <asp:TextBox ID="txtFromProjNum" runat="server" Visible="true" CssClass="clsTextBoxBlueSm" Width="120px" TabIndex="1"></asp:TextBox>
                                            <%-- <ajaxToolkit:MaskedEditExtender ID="ameProjNum" runat="server" ClearMaskOnLostFocus="false" Mask="9999-999-999" MaskType="Number" TargetControlID="txtProjNum">
                                            </ajaxToolkit:MaskedEditExtender>--%>
                                            <ajaxToolkit:AutoCompleteExtender ID="aaceProjName" runat="server" TargetControlID="txtFromProjNum" MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="1"
                                                OnClientItemSelected="OnContactSelected" CompletionInterval="100" ServiceMethod="GetProjectsByFilter">
                                            </ajaxToolkit:AutoCompleteExtender>

                                            <asp:TextBox ID="txtFromCommitedProjNum" runat="server" Visible="false" CssClass="clsTextBoxBlueSm" Width="120px"></asp:TextBox>
                                            <%--<ajaxToolkit:MaskedEditExtender ID="ameCommitExt" runat="server" ClearMaskOnLostFocus="false" Mask="9999-999-999" MaskType="Number" TargetControlID="txtCommitedProjNum">
                                            </ajaxToolkit:MaskedEditExtender>--%>
                                            <ajaxToolkit:AutoCompleteExtender ID="aceCommitAuto" runat="server" TargetControlID="txtFromCommitedProjNum" MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="1"
                                                OnClientItemSelected="OnCommittedProjectSelected" CompletionInterval="100" ServiceMethod="GetCommittedPendingProjectslistByFilter">
                                            </ajaxToolkit:AutoCompleteExtender>

                                        </td>
                                        <td style="width: 10%; float: left"><span class="labelClass">Date :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:TextBox ID="txtRfromDate" runat="server" CssClass="clsTextBoxBlue1"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRfromDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td style="width: 40%; float: left" colspan="2">
                                            <asp:Label ID="lblProjName1" runat="server" class="labelClass" Text=" "></asp:Label>
                                        </td>
                                       <%-- <td style="width: 30%; float: left">
                                            
                                            <asp:TextBox ID="txtToProjNum" runat="server" Visible="true" CssClass="clsTextBoxBlueSm" Width="120px" TabIndex="1"></asp:TextBox>

                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtToProjNum" MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="1"
                                                OnClientItemSelected="OnContactSelected" CompletionInterval="100" ServiceMethod="GetCommittedPendingProjectslistByFilter">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </td>
--%>
                                    </tr>
                                     <tr>
                                        <td style="height: 4px" colspan="6" />
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; float: left"><span class="labelClass">Fund # :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:DropDownList ID="ddlAccountFrom" CssClass="clsDropDown" runat="server" onclick="needToConfirm = false;" OnSelectedIndexChanged="ddlAccountFrom_SelectedIndexChanged" AutoPostBack="True" TabIndex="8">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtAcctNumFrom" runat="server" Visible="false" CssClass="clsTextBoxBlueSm" Width="120px" TabIndex="1"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtAcctNumFrom" MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="1"
                                                OnClientItemSelected="OnContactSelected" CompletionInterval="100" ServiceMethod="GetFundAccountsByFilter">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </td>
                                        <td style="width: 10%; float: left"><span class="labelClass">Fund :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:DropDownList ID="ddlRFromFund" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRFromFund_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40%; float: left" colspan="2">
                                        </td>
                                       <%-- <td style="width: 20%; float: left"></td>--%>
                                    </tr>
                                    <tr>
                                        <td style="height: 4px" colspan="6" />
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; float: left"><span class="labelClass">Type :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:DropDownList ID="ddlRFromFundType" CssClass="clsDropDown" AutoPostBack="true" runat="server" onclick="needToConfirm = false;" OnSelectedIndexChanged="ddlRFromFundType_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td style="width: 10%; float: left"><span class="labelClass">Amount :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:TextBox ID="txtRfromAmt" runat="server" onkeyup='toRFromAmtFormatter(value)' CssClass="clsTextBoxMoney"></asp:TextBox>
                                        </td>
                                        <td style="width: 20%; float: left"><span class="labelClass">Available Funds $:</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:Label ID="lblAvailFund" runat="server" class="labelClass" Text="" Visible="false">
                                            </asp:Label>
                                            <asp:Label ID="lblAvailVisibleFund" runat="server" class="labelClass" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="height: 4px" colspan="6" />
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; float: left">
                                            <asp:Label ID="lblUsePermit" class="labelClass" runat="server" Visible="false" Text="Use Permit:"></asp:Label>
                                        </td>
                                        <td style="width: 20%; float: left">
                                            <asp:DropDownList ID="ddlUsePermit" CssClass="clsDropDown" runat="server" Visible="false" TabIndex="10" 
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlUsePermit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <p class="lblErrMsg">
                                    <asp:Label runat="server" ID="lblRErrorMsg" Font-Size="Small"></asp:Label>
                                </p>

                            </div>
                        </div>
                    </div>

                    <div class="container" id="divReallocateTo" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading">Assign To</div>
                            <div class="panel-body">
                                <table style="width: 100%" runat="server" id="tblReallocateTo">
                                    <tr>
                                        <td style="width: 10%; float: left"><span class="labelClass">Project# :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:DropDownList ID="ddlRToProj" CssClass="clsDropDown" AutoPostBack="true" Visible="false" runat="server" onclick="needToConfirm = false;"
                                                OnSelectedIndexChanged="ddlRToProj_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtToProjNum" runat="server" Visible="true" CssClass="clsTextBoxBlueSm" Width="120px" TabIndex="1" onkeyup="SetContextKey()"></asp:TextBox>
                                            <%-- <ajaxToolkit:MaskedEditExtender ID="ameProjNum" runat="server" ClearMaskOnLostFocus="false" Mask="9999-999-999" MaskType="Number" TargetControlID="txtProjNum">
                                            </ajaxToolkit:MaskedEditExtender>--%>
                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtToProjNum" MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="1"
                                                UseContextKey="true" OnClientItemSelected="OnToProjectSelected" CompletionInterval="100" ServiceMethod="GetAssignmentProjectslistByFilter">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </td>
                                        <td style="width: 10%; float: left"><span class="labelClass">Fund :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:DropDownList ID="ddlRToFund" CssClass="clsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRToFund_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td style="width: 40%; float: left" colspan="2">
                                            <asp:Label ID="lblProjName2" runat="server" class="labelClass" Text=" "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 4px" colspan="6" />
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; float: left"><span class="labelClass">Type :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:DropDownList ID="ddlRtoFundType" CssClass="clsDropDown" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10%; float: left"><span class="labelClass">Amount :</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:TextBox ID="txtRToAmt" CssClass="clsTextBoxMoney" onkeyup='toRToAmtFormatter(value)' runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 20%; float: left"><span class="labelClass">Available Funds $:</span></td>
                                        <td style="width: 20%; float: left">
                                            <asp:Label ID="lblAvailFundTo" runat="server" class="labelClass" Text="" Visible="false">
                                            </asp:Label>
                                            <asp:Label ID="lblAvailVisibleFundTo" runat="server" class="labelClass" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="height: 4px" colspan="6" />
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; float: left">
                                            <span class="labelClass">Target Year</span>
                                        </td>
                                        <td style="width: 20%; float: left">
                                           <asp:DropDownList ID="ddlTargetYear" Enabled="true" CssClass="clsDropDown" runat="server" TabIndex="5">
                                                        </asp:DropDownList>
                                        </td>
                                         <td style="width: 10%; float: left">
                                             <asp:Label ID="lblUsePermitTo" class="labelClass" runat="server" Visible="false" Text="Use Permit:"></asp:Label>
                                         </td>
                                        <td style="width: 20%; float: left">
                                             <asp:DropDownList ID="ddlUsePermitTo" CssClass="clsDropDown" runat="server" Visible="false" TabIndex="10" 
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlUsePermitTo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Button ID="btnReallocateSubmit" runat="server" Enabled="true" Text="Submit" class="btn btn-info" OnClientClick="needToConfirm = false;" OnClick="btnReallocateSubmit_Click" />
                                <br />
                                <br />

                                <asp:GridView ID="gvReallocate" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" CssClass="gridView" EnableTheming="True"
                                    GridLines="None"
                                    PagerSettings-Mode="NextPreviousFirstLast" ShowFooter="True" Width="90%" OnRowCancelingEdit="gvReallocate_RowCancelingEdit"
                                    OnRowEditing="gvReallocate_RowEditing" OnRowDeleting="gvReallocate_RowDeleting" OnRowDataBound="gvReallocate_RowDataBound">
                                    <AlternatingRowStyle CssClass="alternativeRowStyle" />
                                    <PagerStyle CssClass="pagerStyle" ForeColor="#F78B0E" />
                                    <HeaderStyle CssClass="headerStyle" />
                                    <PagerSettings FirstPageText="&amp;lt;" LastPageText="&amp;gt;" Mode="NumericFirstLast" PageButtonCount="5" />
                                    <RowStyle CssClass="rowStyle" />
                                    <FooterStyle CssClass="footerStyleTotals" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("transid")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project #" SortExpression="proj_num">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRProjNum" runat="Server" Text='<%# Eval("proj_num") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account Number" SortExpression="Account">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcctNum" runat="Server" Text='<%# Eval("Account") %>' />
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtAcctNum" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("Account") %>'></asp:TextBox>
                                </EditItemTemplate>--%>
                                            <FooterTemplate>
                                                Running Total :
                                           
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fund Name" SortExpression="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFundName" runat="Server" Text='<%# Eval("Name") %>' />
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                    <asp:TextBox ID="txtFundName" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("Name") %>'></asp:TextBox>
                                </EditItemTemplate>--%>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFooterAmount" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Type" SortExpression="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransType" runat="Server" Text='<%# Eval("Description") %>' />
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                        <asp:DropDownList ID="ddlTransType" runat="server" CssClass="clsDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtTransType" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("lktranstype") %>' Visible="false"></asp:TextBox>
                                    </EditItemTemplate>--%>
                                            <FooterTemplate>
                                                Balance Amount :
                                           
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" SortExpression="Amount" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmt" runat="Server" Text='<%# Eval("Amount", "{0:C2}") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="Server" CssClass="clsTextBoxBlueSm" Text='<%# Eval("Amount", "{0:0.00}") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFooterBalance" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                            <FooterStyle Width="200px" />
                                            <HeaderStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Guid" SortExpression="projguid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjGuid" runat="Server" Text='<%# Eval("projguid") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fund Id" SortExpression="FundID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFundId" runat="Server" Text='<%# Eval("FundID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Detail Id" SortExpression="detailid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDetId" runat="Server" Text='<%# Eval("detailid") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="false" ShowDeleteButton="true" />
                                    </Columns>
                                    <FooterStyle CssClass="footerStyle" />
                                </asp:GridView>

                                <br />
                                <asp:Button ID="btnNewTransaction" runat="server" class="btn btn-info" Enabled="true" OnClick="btnNewTransaction_Click" TabIndex="11" Text="Add New Assignment" Visible="False" />
                                <br />

                                <br />
                            </div>
                        </div>
                    </div>

                    <asp:HiddenField ID="hfTransAmt" runat="server" Value="0" />
                    <asp:HiddenField ID="hfBalAmt" runat="server" Value="0" />
                    <asp:HiddenField ID="hfTransId" runat="server" />
                    <asp:HiddenField ID="hfRFromTransId" runat="server" />
                    <asp:HiddenField ID="hfProjId" runat="server" />
                    <asp:HiddenField ID="hfToProjId" runat="server" />
                    <asp:HiddenField ID="hfReallocateGuid" runat="server" />
                    <asp:HiddenField ID="hdnValue" OnValueChanged="hdnValue_ValueChanged" runat="server" />
                    <asp:HiddenField ID="hdnCommitedProjValue" OnValueChanged="hdnCommitedProjValue_ValueChanged" runat="server" />
                    <asp:HiddenField ID="hdnToValue" OnValueChanged="hdnToValue_ValueChanged" runat="server" />


                </asp:Panel>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        window.onbeforeunload = confirmExit;
        function confirmExit() {
            var balAmt = document.getElementById("<%=hfBalAmt.ClientID%>").value;
            //var traAmt = document.getElementById("<%=hfTransAmt.ClientID%>").value;

            if (needToConfirm && balAmt != 0)
                return "You have attempted to leave this page.  Please make sure balance amount is 0 for each transaction, otherwise the transaction can't be used for board financial transactions.  Are you sure you want to exit this page?";
        }

        function PopupNewAwardSummary() {
            window.open('./awardsummary.aspx?projectid=' + $("#<%= hfProjId.ClientID%>").val())
        };

        function PopupExistingAwardSummary() {
            window.open('./awardsummary.aspx?projectid=' + $("#<%= hfProjId.ClientID%>").val())
        };

        function PopupProjectSearch() {
            window.open('./projectsearch.aspx')
        };

        function OnCommittedProjectSelected(source, eventArgs) {

            var hdnCommitedProjValueID = "<%= hdnCommitedProjValue.ClientID %>";

            document.getElementById(hdnCommitedProjValueID).value = eventArgs.get_value();
            __doPostBack(hdnCommitedProjValueID, "");
            $('#totMoney').focus();
        }


        function OnFundAcctSelected(source, eventArgs) {

            var hdnfundAcct = "<%= hdnCommitedProjValue.ClientID %>";

            document.getElementById(hdnCommitedProjValueID).value = eventArgs.get_value();
            __doPostBack(hdnCommitedProjValueID, "");
            $('#totMoney').focus();
        }

        function OnContactSelected(source, eventArgs) {

            var hdnValueID = "<%= hdnValue.ClientID %>";

            document.getElementById(hdnValueID).value = eventArgs.get_value();
            __doPostBack(hdnValueID, "");
        }


        function OnToProjectSelected(source, eventArgs) {

            var hdnValueID = "<%= hdnToValue.ClientID %>";

            document.getElementById(hdnValueID).value = eventArgs.get_value();
            __doPostBack(hdnValueID, "");
        }

        //Currency formatter code starts below
        var formatter = new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD',
            minimumFractionDigits: 2,
        });
        toRFromAmtFormatter = value => {
            const digits = this.getDigitsFromValue(value);
            const digitsWithPadding = this.padDigits(digits);

            let result = this.addDecimalToNumber(digitsWithPadding);

            var inputElement = document.getElementById("txtRfromAmt");

            //inputElement.value = formatter.format(result);
            $('#<%= txtRfromAmt.ClientID%>').val(formatter.format(result));
        };

        toRToAmtFormatter = value => {
            const digits = this.getDigitsFromValue(value);
            const digitsWithPadding = this.padDigits(digits);

            let result = this.addDecimalToNumber(digitsWithPadding);

            var inputElement = document.getElementById("txtRToAmt");

            //inputElement.value = "$" + result;
            $('#<%= txtRToAmt.ClientID%>').val(formatter.format(result));
        };
        getDigitsFromValue = (value) => {
            return value.toString().replace(/\D/g, '');
        };

        padDigits = digits => {
            const desiredLength = 3;
            const actualLength = digits.length;

            if (actualLength >= desiredLength) {
                return digits;
            }

            const amountToAdd = desiredLength - actualLength;
            const padding = '0'.repeat(amountToAdd);

            return padding + digits;
        };
        addDecimalToNumber = number => {
            const centsStartingPosition = number.length - 2;
            const dollars = this.removeLeadingZeros(number.substring(0, centsStartingPosition));
            const cents = number.substring(centsStartingPosition);
            return `${dollars}.${cents}`;
        };
        removeLeadingZeros = number => number.replace(/^0+([0-9]+)/, '$1');

        function SetContextKey() {            
            $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($('#<%= txtFromProjNum.ClientID%>').val());
        }
    </script>
</asp:Content>
