<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageGenres.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageGenres" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView CssClass="" ID="gvGenres" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
            <asp:BoundField DataField="Title"/>
            <asp:BoundField DataField="Description"/>
            <asp:BoundField DataField="NumMovies"/>
        </Columns>
    </asp:GridView>
</asp:Content>
