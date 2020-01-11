<%@ Page Title="" Language="C#" MasterPageFile="~/TI/Ti.Master" AutoEventWireup="true" CodeBehind="GraficosLog.aspx.cs" Inherits="SCE.TI.GraficosLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script type="text/javascript" src="https://www.google.com/jsapi"></script>
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
     <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCDF_9PaPS9M_5yQZg3api7kRRJhOoc_oE&callback=initMap" type="text/javascript"></script>
    <script>

        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        
        window.onload = Graphics_Click;

        function Graphics_Click() {
            $.ajax({
                url: "/Ti/GraficosLog.aspx/UserRelatorio",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; chartset=utf-8",
                success: function (data) {
                    chartData = data.d;
                },
                error: function () {
                    alert("Error loading data! Please try again.");
                }
            }).done(function () {
                // after complete loading data
                google.setOnLoadCallback(drawChart);
                drawChart(chartData);
            });
        }
        

        function drawChart(chartData) {
            var data = google.visualization.arrayToDataTable(chartData);

            //criando array de cores basiado em valores 
            var slicesColor = {};
            // setando valores para o array de cores
            for (var i = 0; i < data.getNumberOfRows() ; i++) {
                slicesColor[i] = {
                    offset: (i == 0 || data.getValue(i, 0) == 'EVERGREEN' ? "0.4" : "0.0"),
                };
            }


            var options = {
                title: "Top 10 Usuarios  " ,
                'height': 500,
                'width': 1000,
                'is3D': true,
                'legend': 'labeled', fontSize: 12,  // Como vai mostrar a legenda em Top ,Bott peraai om , left , right ou labeled que vai fazer uma linha indicando no grafico
                'pieSliceText': 'none', // * mostrar o valor  ou texto = (label) que vai mostrar na fatia da pizza
                legend: { position: 'labeled', labeledValueText: 'value' },
                tooltip: {
                    text: 'value'
                },
                slices: slicesColor,
                pointSize: 5
            };


            var columnChart = new google.visualization.PieChart(document.getElementById('chart_Pie'));
            columnChart.draw(data, options);
            ColunaGraphic();
        }
            ///////////////// fim PIE////////////////////////////////////////////////////////////////////////////////////////////////////



            /////////////////////////////////////////////////////// INICIO COLUNAS /////////////////////////////////////////////////////
            function ColunaGraphic() {
                $.ajax({
                    url: "/Ti/GraficosLog.aspx/Relatorio",
                    data: "",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; chartset=utf-8",
                    success: function (data) {
                        chartDataColun = data.d;
                    },
                    error: function () {
                        alert("Error loading data! Please try again.");
                    }
                }).done(function () {
                    // after complete loading data
                    google.setOnLoadCallback(chartDataColun);
                    drawChartColumn(chartDataColun);
                });
            }

            function drawChartColumn(chartDataColun) {
                //pegando dados 
                var dataColumn = google.visualization.arrayToDataTable(chartDataColun);
                var dados = []

                // colocando os dados em um array para ser usado no dataTable
                for (var i = 0; i < dataColumn.getNumberOfRows() ; i++) {
                    dados[i] = [dataColumn.getValue(i, 0), dataColumn.getValue(i, 1), dataColumn.getValue(i, 1)]
                }

                // criando data table para a coluna 
                var tabela = new google.visualization.DataTable();
                tabela.addColumn('string', 'Armador');
                tabela.addColumn('number', 'Feitos');
                tabela.addColumn({ type: 'number', role: 'annotation' }); // cria uma anotação para mostrar o numero em cima da coluna

                // Associando valores 
                tabela.addRows(dados);


                var optionsColunm = {
                    title: 'Top 10 relatorio ',
                    height: 500,
                    width: 1000,
                    vAxis: { gridlines: { count: 0 }, textPosition: 'none' }, // retira o valor da coluna a direita
                    legend: 'none', // retira o valor da legenda
                };

                var columnChart = new google.visualization.ColumnChart(document.getElementById('chart_Col'));
                columnChart.draw(tabela, optionsColunm);
            }

           
        

    </script>

  <br />

         <div id="Graphics" >
        <div id="chart_Pie" >
        </div>
            <hr /> 
        <div id="chart_Col" >
        </div>
    </div> 

       


</asp:Content>
