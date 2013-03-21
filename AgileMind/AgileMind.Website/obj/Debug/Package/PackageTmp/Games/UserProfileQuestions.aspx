<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfileQuestions.aspx.cs" Inherits="AgileMind.Website.Games.UserProfileQuestions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="width:300px">
    <form>
        <div>
            <fieldset>
                <div>
                    <asp:Label ID="uxQuestion" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="uxAnswer" placeholder="answer" runat="server"></asp:TextBox>
                </div>
            </fieldset>
        </div>
            <asp:Button ID="uxBack" runat="server" Text="Back" CssClass="btn" 
                onclick="uxBack_Click" />
            <span style="float:right">
            <asp:Button ID="uxNextFinish" runat="server" Text="Next" 
                CssClass="btn btn-primary" onclick="uxNextFinish_Click" />
            </span>
            <asp:HiddenField ID="uxQuestionNumber" runat="server" />
            <asp:HiddenField ID="uxQuestionList" runat="server" />
            <div>
                <asp:Label ID="uxQuestionBegin" runat="server"></asp:Label>
                &nbsp out of &nbsp
                <asp:Label ID="uxQuestionCount" runat="server"></asp:Label>
            </div>
            <div class="progress progress-striped active">
                <asp:Literal ID="uxBar" runat="server"></asp:Literal>
            </div>
    </form>
    </div>

</asp:Content>
