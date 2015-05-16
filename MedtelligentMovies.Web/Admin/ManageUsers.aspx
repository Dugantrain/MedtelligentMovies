<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView CssClass="" ID="gvUsers" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
            <asp:BoundField DataField="UserName"/>
            <asp:BoundField DataField="FirstName"/>
            <asp:BoundField DataField="LastName"/>
            <asp:BoundField DataField="Email"/>
        </Columns>
    </asp:GridView>
</asp:Content>
