<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageMovies.aspx.cs" Inherits="MedtelligentMovies.Web.Admin.ManageMovies" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Movie Management</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView CssClass="" ID="gvMovies" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
            <asp:BoundField DataField="Title"/>
            <asp:BoundField DataField="Description"/>
        </Columns>
    </asp:GridView>
</asp:Content>
