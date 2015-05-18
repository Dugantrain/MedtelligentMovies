<%@ Page Title="" Language="C#" MasterPageFile="Public.Master" AutoEventWireup="true" CodeBehind="Genre.aspx.cs" Inherits="MedtelligentMovies.Web.Public.Genre" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView CssClass="" ID="gvMovies" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
            <asp:BoundField DataField="Title" HeaderText="Title"/>
            <asp:BoundField DataField="Description" HeaderText="Description"/>
        </Columns>
    </asp:GridView>
</asp:Content>
