<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ColorQuiz.aspx.cs" Inherits="AgileMind.Website.Games.ColorQuiz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--    <asp:LoginView ID="uxLoginView" runat="server">
        <AnonymousTemplate>
            <h1>I'm sorry, you must be logged in to play this game.</h1>
        </AnonymousTemplate>
        <LoggedInTemplate>
--%>            <h1>Color Game</h1>
            <asp:UpdatePanel ID="GameUpdate" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td><asp:Label ID="uxLeftScreen" runat="server"></asp:Label></td>
                            <td><asp:Label ID="uxRightScreen" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Button ID="uxLeftChoice" runat="server" onclick="uxNoMatchChoice_Click" Text="No Match" /></td>
                            <td><asp:Button ID="uxRightChoice" runat="server" onclick="uxMatchChoice_Click" Text="Match" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
<%--        </LoggedInTemplate>
    </asp:LoginView>
--%>
</asp:Content>
