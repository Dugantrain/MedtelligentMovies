﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ManageMovies.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageMovies" %>
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
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="MovieUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvMovies" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
                                    <asp:BoundField DataField="Title"/>
                                    <asp:BoundField DataField="Description"/>
                                    <asp:BoundField DataField="Genre.Title"/>
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
            <tr>
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="InsertUpdateMoviePanel"  runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                          <table>
                              <tr style="display: none">
                                  <td>
                                      <asp:HiddenField ID="hdnId" runat="server"/>
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
                              <td><asp:TextBox MaxLength="50" runat="server" ID="txtDescription" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription" ValidationGroup="saveValidation" CssClass="field-validation-error" ErrorMessage="Description is required." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblGenre" runat="server" AssociatedControlID="ddlGenre" 
                                             Text="Genre" /></td>
                              <td><asp:DropDownList runat="server" ID="ddlGenre" /></td>
                                <td>
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                <asp:Button ID="SaveButton" runat="server" ValidationGroup="saveValidation"  Text="Save" OnClick="SaveButton_Click"/>
                                <asp:Button ID="Cancelbutton"  runat="server" Text="Cancel" OnClick="CancelButton_Click"/>
                              </td>
                            </tr>
                          </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
</asp:Content>
