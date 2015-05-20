<%@ Page Title="" Language="C#" MasterPageFile="Public.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Genre.aspx.cs" Inherits="MedtelligentMovies.Web.Public.Genre" %>
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
    <table>
        <tr>
            <td style="height: 206px" valign="top">
                <asp:UpdatePanel ID="MovieUpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <asp:HiddenField ID ="hdnSelectedMovieId" runat="server"/>
                    <asp:GridView CssClass="Grid" ID="gvMovies" GridLines="None" OnSelectedIndexChanged="OnSelectedIndexChanged" OnRowDataBound="gvMovies_RowDataBound" RowStyle-CssClass="row" AlternatingRowStyle-CssClass="alt-row"   AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
                            <asp:BoundField DataField="Title" HeaderText="Title"/>
                            <asp:BoundField DataField="Description" HeaderText="Description"/>
                        </Columns>
                    </asp:GridView>
                        </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
</asp:Content>
