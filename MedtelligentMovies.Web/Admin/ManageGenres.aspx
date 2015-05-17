<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageGenres.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageGenres" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Genre Management</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView CssClass="" ID="gvGenres" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
            <asp:BoundField DataField="Title"/>
            <asp:BoundField DataField="Description"/>
            <asp:BoundField DataField="NumMovies"/>
        </Columns>
    </asp:GridView>
</asp:Content>
