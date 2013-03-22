<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfileQuiz.aspx.cs" Inherits="AgileMind.Website.Games.UserProfileQuiz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

            <h1>Profile Quiz Game</h1>
            <asp:UpdatePanel ID="GameUpdate" runat="server">
                <ContentTemplate>
                    <p><asp:Label ID="uxQuestion" runat="server"></asp:Label></p>
                    <div>
                        <asp:TextBox ID="uxAnswer" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Button ID="uxNext" runat="server" Text="Next" CssClass="btn btn-primary" 
                            onclick="uxNext_Click" />
                    </div>
                    <div>
                        <asp:Label ID="uxQuestionBegin" runat="server"></asp:Label>
                            &nbsp out of &nbsp
                        <asp:Label ID="uxQuestionCount" runat="server"></asp:Label>
                    </div>

                    <div class="progress progress-striped active">
                        <asp:Literal ID="uxBar" runat="server"></asp:Literal>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

</asp:Content>
