<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ColorGameResults.aspx.cs" Inherits="AgileMind.Website.Games.ColorGameResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    You got <asp:Label ID="uxCorrect" runat="server"></asp:Label> out of <asp:Label ID="uxTotal" runat="server"></asp:Label> Questions
    in <asp:Label ID="uxTimeSpan" runat="server"></asp:Label> seconds
</asp:Content>
