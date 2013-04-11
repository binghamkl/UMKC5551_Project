<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShortTerm.aspx.cs" Inherits="AgileMind.Website.Games.ShortTerm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Please read the following and try to remember as many details as possible.</h1>
        <p>
            <asp:Label ID="uxStory" runat="server"></asp:Label>
        </p>
        <div>
            <asp:Button ID="uxContinue" runat="server" Text="Begin Quiz" 
                CssClass="btn btn-primary" onclick="uxContinue_Click" />
        </div>
    </div>
</asp:Content>
