<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Picadely.UI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="site-wrap" id="home-section">

        <div class="site-mobile-menu site-navbar-target">
            <div class="site-mobile-menu-header">
                <div class="site-mobile-menu-close mt-3">
                    <span class="icon-close2 js-menu-toggle"></span>
                </div>
            </div>
            <div class="site-mobile-menu-body"></div>
        </div>



        <header class="site-navbar site-navbar-target" role="banner">

            <div class="container">
                <div class="row align-items-center position-relative">

                    <div class="col-3">
                        <div class="site-logo">
                            <a href="index.html" class="font-weight-bold">Picadelli</a>
                        </div>
                    </div>


                </div>
            </div>

        </header>


        <div class="site-section-cover">
            <div class="container">
                <div class="row align-items-center text-left justify-content-center">
                    <div class="col-lg-7 mr-auto">
                        <h1 class="text-black mb-4">¿Picadelli, quiénes somos?</h1>
                        <p class="lead text-muted">
                            Picadelli es una empresa dedicada a la confección de picadas para distintos tipos de necesidades.

Ofrecemos un servicio diferente a todos los demás. Nos distinguimos por nuestra propuesta dinámica y a medida. La idea fundamental de nuestro servicio es adecuarnos a tus necesidades y gustos.
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <div class="site-section-cover image-about"  style="background-image: url('images/hero_2.jpg');" data-stellar-background-ratio="0.5">
            <div class="container">
                <div class="row align-items-center text-left justify-content-center">
                </div>
            </div>
        </div>
</asp:Content>
