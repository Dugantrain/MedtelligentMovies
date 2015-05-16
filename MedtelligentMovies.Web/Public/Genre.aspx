<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Genre.aspx.cs" Inherits="MedtelligentMovies.Web.Public.Genre" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView CssClass="" ID="gvMovies" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
            <asp:BoundField DataField="Title" HeaderText="Title"/>
            <asp:BoundField DataField="Description" HeaderText="Description"/>
            <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date"/>
        </Columns>
    </asp:GridView>
</asp:Content>
