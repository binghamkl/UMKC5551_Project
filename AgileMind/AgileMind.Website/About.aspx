<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="AgileMind.Website.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
        Studies are beginning to show that mental exercise as well as physical exercise can prevent or reduce the symptoms of brain disorders such as alzheimer's.
    </p>
    <p>
        Keep your mind active is a big part of helping with symptoms of people who are currently afflicted with these diseases.
    </p>
    <div id="authorsdiv">
        <h2>Creators</h2>
        <div>
            <h3>Vidya Sridhar</h3>
            <div class="controls controls-row">
                <span class="span1"></span>
                <span class="span10">
			        <p>I am currently in my final semester at UMKC, doing my masters in computer science. I completed several courses of my undergrad back in India, and decided to study in the University of Missouri, Kansas City to pursue my further studies. 
			        </p>
        		</span>
            </div>
        </div>
        <div>
            <h3>Kendall Bingham</h3>
            <div class="controls controls-row">
                <span class="span1">
                    <img src="img/kbingham.jpg" class="img-polaroid" />
                </span>
                <span class="span10">
                    <p>I've worked for IBS Software for since 1995, building enterprise applications for in house use as well as for external customers.  I graduated with my BS in Computer Science form UMKC and have recently returned as a graduate student.</p>
                </span>
            </div>
        </div>
    </div>
</asp:Content>
