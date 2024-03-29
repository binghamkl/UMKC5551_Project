﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ColorQuiz.aspx.cs" Inherits="AgileMind.Website.Games.ColorQuiz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

            <h1>Color Game</h1>
            <asp:UpdatePanel ID="GameUpdate" runat="server">
                <ContentTemplate>
                    <table width="200px">
                        <tr>
                            <td><asp:Label ID="uxLeftScreen" runat="server"></asp:Label></td>
                            <td><asp:Label ID="uxRightScreen" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Button ID="uxLeftChoice" runat="server" onclick="uxNoMatchChoice_Click" Text="No Match" CssClass="btn btn-danger" /></td>
                            <td><asp:Button ID="uxRightChoice" runat="server" onclick="uxMatchChoice_Click" Text="Match" CssClass="btn btn-success" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

</asp:Content>
