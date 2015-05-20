<%@ Page Title="Home Page" Language="C#" MasterPageFile="Public.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="MedtelligentMovies.Web.Public.Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Top 5 Movies by Genre</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="genreUpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <asp:DataList ID="dlGenres" ItemStyle-VerticalAlign="Top" OnItemDataBound="dlGenres_ItemDataBound" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table"  AutoGenerateColumns="false" runat="server">
                        <ItemTemplate>
                            <table>
                            <tr>
                                <td>
                                    <h2>
                                    <asp:HyperLink runat="server" ID="lnkGenre" Text='<%# Eval("Title") %>' NavigateUrl='<%# "genre.aspx?g=" + Eval("Id") %>' />
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="MovieUpdatePanel" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                        <asp:HiddenField ID ="hdnSelectedMovieId" runat="server"/>
                                        <asp:GridView ID="gvTopMovies" CssClass="Grid" OnSelectedIndexChanged="OnSelectedIndexChanged"
                                            OnRowDataBound="gvMovies_RowDataBound" RowStyle-CssClass="row" 
                                            AlternatingRowStyle-CssClass="alt-row" runat="server" AutoGenerateColumns="false" 
                                            GridLines="None" >
                                        <Columns>
                                            <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="CreatedDate" dataformatstring="{0:MM/dd/yyyy}" HeaderText="Created On" HeaderStyle-HorizontalAlign="Left" />

                                        </Columns>
                                        </asp:GridView>
                                       </ContentTemplate>
                                        </asp:UpdatePanel>
                                </td>
                            </tr>
                                </table>
                        </ItemTemplate>
                    </asp:DataList>
                    </ContentTemplate>
               </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
</asp:Content>
