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
    <asp:GridView CssClass="" ID="gvGenres" AutoGenerateColumns="false" OnRowDataBound="gvGenres_RowDataBound" runat="server">
        <Columns>
            <asp:BoundField ReadOnly="true" Visible="false" DataField="Id"/>
            <asp:hyperlinkfield datatextfield="Title" datanavigateurlfields="Id" datanavigateurlformatstring="genre.aspx?g={0}"  />
            <asp:BoundField DataField="Description"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <tr>
                        <td colspan="100%">
<%--                            <div id="div<%# Eval("Id") %>" style="display: none; position: relative; left: 15px; overflow: auto">--%>
                                <asp:GridView ID="gvTopMovies" runat="server" AutoGenerateColumns="false" BorderStyle="Double"  BorderColor="#df5015" GridLines="None" Width="250px">
                                <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                <RowStyle BackColor="#E1E1E1" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" HeaderStyle-HorizontalAlign="Left" />
                                </Columns>
                                </asp:GridView>
<%--                            </div>--%>
                        </td>
                    </tr>
                </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
