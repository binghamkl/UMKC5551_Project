<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GameResults.aspx.cs" Inherits="AgileMind.Website.Games.GameResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="controls controls-row">
        <span class="span1">
            <div class="ratingimage">
                <asp:Image ID="uxResultImage" runat="server" ImageUrl="~/img/Star-small.png" />
            </div>
        </span>
        <span class="span10">
			<h2>You got <asp:Label ID="uxCorrect" runat="server"></asp:Label> out of <asp:Label ID="uxTotal" runat="server"></asp:Label> Questions in <asp:Label ID="uxTimeSpan" runat="server"></asp:Label> seconds
            </h2>
        </span>
    </div>
</asp:Content>
