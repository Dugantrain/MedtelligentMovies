<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <table>
            <tr>
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="InsertUserUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                          <table>
                             <tr>
                              <td><asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" 
                                             Text="First Name" /></td>
                              <td><asp:TextBox runat="server" ID="txtFirstName" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName" CssClass="field-validation-error" ErrorMessage="First Name is required." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" 
                                             Text="Last Name" /></td>
                              <td><asp:TextBox runat="server" ID="txtLastName" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName" CssClass="field-validation-error" ErrorMessage="Last Name is required." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" 
                                             Text="User Name" />
                              </td>
                              <td><asp:TextBox runat="server" ID="txtUserName" /></td>
                              <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="User Name is required." />
                                  <asp:CustomValidator runat="server" OnServerValidate="UsernameValidation" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="User Name already exists." />

                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" 
                                             Text="Password" /></td>
                              <td><asp:TextBox runat="server" ID="txtPassword" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="Password is required." />
                              </td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" 
                                             Text="Email" /></td>
                              <td><asp:TextBox runat="server" ID="txtEmail" /></td>
                                <td>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" CssClass="field-validation-error" ErrorMessage="Email is required." />
                                  <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" CssClass="field-validation-error" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format."></asp:RegularExpressionValidator>
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                <asp:Button ID="InsertButton" runat="server" Text="Add" OnClick="InsertButton_Click"/>
                                <asp:Button ID="Cancelbutton" runat="server" Text="Cancel" OnClick="CancelButton_Click"/>
                              </td>
                            </tr>
                          </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="UserUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
                                    <asp:BoundField DataField="UserName"/>
                                    <asp:BoundField DataField="Email"/>
                                    <asp:BoundField DataField="FirstName"/>
                                    <asp:BoundField DataField="LastName"/>
                                </Columns>
                                <PagerSettings PageButtonCount="5" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="InsertButton" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
</asp:Content>
