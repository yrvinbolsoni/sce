<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SCE._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/style.css" rel="stylesheet" />
    <div class="row">
        <div class="col-lg-3">
            <div class="metroblock commentsblock left ">
                <span class="icon entypo-trophy left"></span>
                <h1 id="evergreenvalue">0,00</h1>
                <div class="clear"></div>
                <h2>EVERGREEN</h2>
            </div>
        </div>

        <div class="col-lg-3">
            <div class="metroblock buysblock left ">
                <span class="icon entypo-note-beamed left"></span>
                <h1 id="esaValue">0,00</h1>
                <div class="clear"></div>
                <h2>ESA</h2>
            </div>
        </div>

        <div class="col-lg-3">
            <div class="metroblock buysblock left ">
                <span class="icon entypo-chart-line  left"></span>
                <h1 id="snaValue">0,00</h1>
                <div class="clear"></div>
                <h2>SNA</h2>
            </div>
        </div>

        <div class="col-lg-3">
            <div class="metroblock buysblock left ">
                <span class="icon entypo-chart-area left"></span>
                <h1 id="eusaValue">0,00</h1>
                <div class="clear"></div>
                <h2>EUSA</h2>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-8">
            <div id="Mapa_chart"></div>
        </div>
        <div class="col-lg-4">
            <div id="PieArmadorExportacao">
            </div>
            <div id="PieArmadorImportacao">
           </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-6">
            <div id="Comoddty_chart"></div>
        </div>
        <div class="col-lg-6">
            <div id="ColPorMes">
           </div>
        </div>
    </div>

<%--Loading--%> 
<div id="circle">
  <div class="loaderG">
  </div>
      <h3 >Carregando...</h3>
    <p id="status"></p>
</div> 

     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
     <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCDF_9PaPS9M_5yQZg3api7kRRJhOoc_oE&callback=initMap" type="text/javascript"></script>
    <%--carrega mapas--%> 
    <script type="text/javascript" src="Content/Js/DashBoard.js"></script>
</asp:Content>

