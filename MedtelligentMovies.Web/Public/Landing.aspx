<%@ Page Title="Home Page" Language="C#" MasterPageFile="Public.Master" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="MedtelligentMovies.Web.Public.Default" %>

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
    <asp:DataList ID="gvGenres" OnItemDataBound="gvGenres_ItemDataBound" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table"  AutoGenerateColumns="false" runat="server">
                <ItemTemplate>
                    <table>
                    <tr>
                        <td>
                            <asp:HyperLink runat="server" ID="lnkGenre" Text='<%# Eval("Title") %>' NavigateUrl='<%# "genre.aspx?g=" + Eval("Id") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
<%--                            <div id="div<%# Eval("Id") %>" style="display: none; position: relative; left: 15px; overflow: auto">--%>
                                <asp:GridView ID="gvTopMovies" CssClass="Grid" runat="server" AutoGenerateColumns="false" GridLines="None" >
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                                </Columns>
                                </asp:GridView>
<%--                            </div>--%>
                        </td>
                    </tr>
                        </table>
                </ItemTemplate>
    </asp:DataList>
</asp:Content>
