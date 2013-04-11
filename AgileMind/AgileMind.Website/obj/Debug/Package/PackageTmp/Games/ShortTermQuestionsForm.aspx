<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShortTermQuestionsForm.aspx.cs" Inherits="AgileMind.Website.Games.ShortTermQuestionsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanel" runat="server">
        
        <ContentTemplate>
            <asp:Panel ID="uxCountDownPanel" runat="server" Visible="true">
                <h2>Please wait for the timer to expire, then you can take the quiz questions</h2>
                <asp:Timer ID="uxCountdown" runat="server" Interval="1000" 
                    ontick="uxCountdown_Tick"></asp:Timer>
                <div>
                    <h3>Wait time left <asp:Label ID="uxTimeLeft" runat="server"></asp:Label></h3>
                </div>
            </asp:Panel>
            <asp:Panel ID="uxQuestionsPanel" runat="server" Visible="false">
                <h2>Questions</h2>
                <h3><asp:Label ID="uxQuestion" runat="server"></asp:Label></h3>
                <div>
                <asp:RadioButtonList ID="uxAnswers" runat="server" DataTextField="Answer" 
                    DataValueField="Answer" Height="23px"  
                    Width="270px"></asp:RadioButtonList>
                </div>
                <div>
                    <asp:Button ID="uxNextQuestion" runat="server" Text="Next"
                        CssClass="btn btn-primary" onclick="uxNextQuestion_Click" />
                </div>
                <asp:HiddenField ID="uxQuestionNumber" runat="server" />
            </asp:Panel>
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>
