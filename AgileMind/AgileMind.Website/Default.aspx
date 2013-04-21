<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AgileMind.Website._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type='text/javascript'>
        google.load('visualization', '1', { packages: ['gauge'] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
          ['Label', 'Value'],
          ['MemoryScore', <%= UserTotal %>]
        ]);

            var options = {
                width: 400, height: 120,
                redFrom: -3, redTo: -1,
                greenFrom: 1, greenTo: 3,
                minorTicks: .1,
                min: -3,
                max: 3
            };

            var chart = new google.visualization.Gauge(document.getElementById('userscore_div'));
            chart.draw(data, options);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Agile Mind
    </h2>
    <p>
        We are building tools and information to help you increase your mental elasticity.
    </p>
    <p>
        There are many games and quizes to help you.. Start building today.
    </p>
    <asp:Panel ID="uxLoggedIn" runat="server" Visible="false">
        <div id='userscore_div'></div>
        <asp:Repeater ID="uxGamesRepeater" runat="server" 
            onitemdatabound="uxGamesRepeater_ItemDataBound">
            <ItemTemplate>
                <div style="width:100%">
                    <asp:Chart ID="uxIndividualCharge" runat="server" Width="800px">
                        <Series >
                            <asp:Series Name="GameScores"  ChartType="Column" ChartArea="MainChartArea">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="MainChartArea">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
    </asp:Panel>
</asp:Content>
