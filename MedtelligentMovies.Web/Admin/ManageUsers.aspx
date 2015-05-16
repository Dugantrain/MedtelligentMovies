<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <table>
            <tr>
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="InsertUserUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                          <table cellpadding="2" border="0" style="background-color:#7C6F57">
                            <tr>
                              <td><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserNameTextBox" 
                                             Text="UserName" ForeColor="White" /></td>
                              <td><asp:TextBox runat="server" ID="UserNameTextBox" /></td>
                            </tr>
                              <tr>
                              <td><asp:Label ID="EmailLabel" runat="server" AssociatedControlID="EmailTextBox" 
                                             Text="Email" ForeColor="White" /></td>
                              <td><asp:TextBox runat="server" ID="EmailTextBox" /></td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstNameTextBox" 
                                             Text="First Name" ForeColor="White" /></td>
                              <td><asp:TextBox runat="server" ID="FirstNameTextBox" /></td>
                            </tr>
                            <tr>
                              <td><asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastNameTextBox" 
                                             Text="Last Name" ForeColor="White" /></td>
                              <td><asp:TextBox runat="server" ID="LastNameTextBox" /></td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                <asp:LinkButton ID="InsertButton" runat="server" Text="Insert" OnClick="InsertButton_Click" ForeColor="White" />
                                <asp:LinkButton ID="Cancelbutton" runat="server" Text="Cancel" OnClick="CancelButton_Click" ForeColor="White" />
                              </td>
                            </tr>
                          </table>
                          <asp:Label runat="server" ID="InputTimeLabel"><%=DateTime.Now %></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="UserUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvUsers" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                                BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" AutoGenerateColumns="False">
                                <FooterStyle BackColor="Tan" />
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                <Columns>
                                    <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
                                    <asp:BoundField DataField="UserName"/>
                                    <asp:BoundField DataField="Email"/>
                                    <asp:BoundField DataField="FirstName"/>
                                    <asp:BoundField DataField="LastName"/>
                                </Columns>
                                <PagerSettings PageButtonCount="5" />
                            </asp:GridView>
                            <asp:Label runat="server" ID="ListTimeLabel"><%=DateTime.Now %></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="InsertButton" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
</asp:Content>
