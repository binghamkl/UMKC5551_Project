<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Identify.aspx.cs" Inherits="AgileMind.Website.Games.Identify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="uxupdateQuiz" runat="server">
        <ContentTemplate>

            <h2>Questions</h2>
            <h3><asp:Image ID="uxImage" runat="server" /></h3>
            <div>
                <asp:RadioButtonList ID="uxAnswers" runat="server" DataTextField="Answer" 
                    DataValueField="Answer" Height="23px"  
                    Width="270px"></asp:RadioButtonList>
            </div>
            <div>
                <asp:Button ID="uxNextQuestion" runat="server" Text="Next"
                    CssClass="btn btn-primary" onclick="uxNextQuestion_Click"  />
            </div>

            <div>
                <asp:Label ID="uxQuestionBegin" runat="server"></asp:Label>
                    &nbsp out of &nbsp
                <asp:Label ID="uxQuestionCount" runat="server"></asp:Label>
            </div>

            <div class="progress progress-striped active">
                <asp:Literal ID="uxBar" runat="server"></asp:Literal>
            </div>

            <asp:HiddenField ID="uxQuestionNumber" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
