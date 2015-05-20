<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ManageMovies.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageMovies" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Movie Management</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">
    <table>
            <tr>
                <td class="admin-editpanel" >
                    <asp:UpdatePanel ID="InsertUpdateMoviePanel"  runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                          <table>
                              <tr style="display: none">
                                  <td>
                                      <asp:HiddenField ID="hdnId" runat="server"/>
                                  </td>
                              </tr>
                              <tr>
                              <td><asp:Label ID="lblGenre" runat="server" AssociatedControlID="ddlGenre" 
                                             Text="Genre" /></td>
                              <td><asp:DropDownList runat="server" ID="ddlGenre" /></td>
                                <td>
                                    <asp:CustomValidator runat="server" Display="Dynamic" ValidationGroup="saveValidation" SetFocusOnError="True" OnServerValidate="DropDownGenreValidation" ControlToValidate="ddlGenre" CssClass="field-validation-error" ErrorMessage="Please select a Genre." />
                              </td>
                            </tr>
                             <tr>
                              <td><asp:Label ID="lblTitle" runat="server" AssociatedControlID="txtTitle" 
                                             Text="Title" /></td>
                              <td><asp:TextBox runat="server" MaxLength="50" ID="txtTitle" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle" ValidationGroup="saveValidation" CssClass="field-validation-error" ErrorMessage="Title is required." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" 
                                             Text="Description" /></td>
                              <td><asp:TextBox MaxLength="200" TextMode="MultiLine" runat="server" ID="txtDescription" /></td>
                                <td>
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                <asp:Button ID="SaveButton" runat="server" ValidationGroup="saveValidation"  Text="Save" OnClick="SaveButton_Click"/>
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click"/>
                              </td>
                            </tr>
                          </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
           <tr>
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="MovieUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID ="hdnSelectedMovieId" runat="server"/>
                            <asp:GridView ID="gvMovies" GridLines="None" RowStyle-CssClass="row" AlternatingRowStyle-CssClass="alt-row" OnRowDataBound="gvMovies_RowDataBound" 
                                CellPadding="4" CssClass="Grid" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
                                    <asp:BoundField DataField="Title" HeaderText="Title"/>
                                    <asp:BoundField DataField="Description" HeaderText="Description"/>
                                    <%--Directly binding sub-objects like below yields inconsistent results.  Using RowDataBound event in the code-behind--%>
                                    <asp:BoundField DataField="Genre.Title" HeaderText="Genre"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnUpdate" CommandArgument='<%# Eval("Id") %>' OnClick="PopulateFieldsForUpdate" Text="Update" CommandName="Update"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnDelete" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteMovie" Text="Delete" CommandName="Delete"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SaveButton" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    <script type="text/javascript">
        function PreserveClickedRowStyleOnMouseOut(gridViewId, rowId) {
            var gridViewControl = document.getElementById(gridViewId);
            if (gridViewControl != null) {
                var gridViewRows = gridViewControl.rows;
                if (gridViewRows != null) {
                    var selectedRowId = $("#" + '<%= hdnSelectedMovieId.ClientID %>').val();
                    if (selectedRowId == rowId)return;
                    for (var i = 1; i < gridViewRows.length; i++) {
                        var row = gridViewRows[i];
                        row.style.backgroundColor = "";
                        }
                    }
                }
            }

        function ChangeMouseOverRowColor(gridViewId, rowId) {
            var gridViewControl = document.getElementById(gridViewId);
            if (gridViewControl != null) {
                var gridViewRows = gridViewControl.rows;
                if (gridViewRows != null) {
                    var selectedRowId = $("#" + '<%= hdnSelectedMovieId.ClientID %>').val();
                    for (var i = 1; i < gridViewRows.length; i++) {
                        var row = gridViewRows[i];
                        if (i == selectedRowId) {
                            continue;
                        } else if (i == rowId && rowId != selectedRowId) {
                            row.style.backgroundColor = "green";
                            $("#" + '<%= hdnSelectedMovieId.ClientID %>').val(selectedRowId);
                        } else {
                            row.style.backgroundColor = "";
                        }
                    }
                }
            }
        }

        function ChangeSelectedRowColorOnClick(gridViewId, selectedRowId) {
            var gridViewControl = document.getElementById(gridViewId);
            if (gridViewControl != null) {
                var gridViewRows = gridViewControl.rows;
                if (gridViewRows != null) {
                    var selectedRow = gridViewRows[selectedRowId];
                    for (var i = 1; i < gridViewRows.length; i++) {
                        var row = gridViewRows[i];
                        if (row == selectedRow) {
                            row.style.backgroundColor = "yellow";
                            $("#" + '<%= hdnSelectedMovieId.ClientID %>').val(selectedRowId);
                        }
                        else {
                            row.style.backgroundColor = "";
                        }
                    }
                }
            }

        }
    </script>
</asp:Content>
