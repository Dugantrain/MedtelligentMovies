<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageUsers" %>
<%@ Import Namespace="System.Diagnostics.Eventing.Reader" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>User Management</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">
        <table>
           <tr>
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="UserUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvUsers" OnRowDataBound="gvUsers_RowDataBound" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
                                    <asp:BoundField DataField="UserName"/>
                                    <asp:BoundField DataField="Email"/>
                                    <asp:BoundField DataField="FirstName"/>
                                    <asp:BoundField DataField="LastName"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnUpdate" CommandArgument='<%# Eval("Id") %>' OnClick="PopulateFieldsForUpdate" Text="Update" CommandName="Update"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnDelete" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteUser" Text="Delete" CommandName="Delete"/>
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
                    <asp:UpdatePanel ID="InsertUserUpdatePanel"  runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                          <table>
                              <tr style="display: none">
                                  <td>
                                      <asp:HiddenField ID="hdnId" runat="server"/>
                                  </td>
                              </tr>
                             <tr>
                              <td><asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" 
                                             Text="First Name" /></td>
                              <td><asp:TextBox runat="server" MaxLength="50" ID="txtFirstName" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName" ValidationGroup="saveValidation" CssClass="field-validation-error" ErrorMessage="First Name is required." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" 
                                             Text="Last Name" /></td>
                              <td><asp:TextBox MaxLength="50" runat="server" ID="txtLastName" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName" ValidationGroup="saveValidation" CssClass="field-validation-error" ErrorMessage="Last Name is required." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" 
                                             Text="User Name" />
                              </td>
                              <td><asp:TextBox MaxLength="20" runat="server" ID="txtUserName" /></td>
                              <td>
                                <asp:RequiredFieldValidator Display="Dynamic" runat="server" ValidationGroup="saveValidation" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="User Name is required." />
                                <asp:CustomValidator runat="server" Display="Dynamic" ValidationGroup="saveValidation" SetFocusOnError="True" OnServerValidate="UsernameUniqueValidation" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="User Name already exists." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" 
                                             Text="Password" /></td>
                              <td><asp:TextBox runat="server" ID="txtPassword" TextMode="Password" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ValidationGroup="saveValidation" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="Password is required." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" 
                                             Text="Email" /></td>
                              <td><asp:TextBox runat="server" ID="txtEmail" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="saveValidation" ControlToValidate="txtEmail" CssClass="field-validation-error" ErrorMessage="Email is required." />
                                  <asp:RegularExpressionValidator ID="regexEmailValid" ValidationGroup="saveValidation" Display="Dynamic" runat="server" CssClass="field-validation-error" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format."></asp:RegularExpressionValidator>
                                  <asp:CustomValidator runat="server" Display="Dynamic" ValidationGroup="saveValidation" SetFocusOnError="True" OnServerValidate="EmailUniqueValidation" ControlToValidate="txtEmail" CssClass="field-validation-error" ErrorMessage="Email already exists." />
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
