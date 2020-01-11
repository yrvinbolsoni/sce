
// carrengamento central para os chat (CORAÇÂO)
google.charts.load('current', { 'packages': ['geochart'], 'mapsApiKey': 'AIzaSyCDF_9PaPS9M_5yQZg3api7kRRJhOoc_oE' });
google.load("visualization", "1", { packages: ["corechart"] });


// ordem de carregamento 
CarregarCards();
PieArmardorimp();
PieArmardorExp();
ColunaPorMes();
CommoditGraphics();
Graphics_Maps();

// definindo plaheta de cores para os armadores 
var myColors = {
    'EVERGREEN': '#008000',
    'MAERSK LINE': '#4682B4',
    'MSC': '#FFD700',
    'HAPAG LLOYD': '#800080',
    'CMA-CGM': '#FF8C00',
    'HAMBURG-SUD': '#FF0000',
    'ONE': '#FF69B4',
    'COSCO': '#8B4513',
    'PIL': '#90EE90',
    'SAFMARINE': '#87CEFA'
}


// formata o valor para conter as mascara de pontos ex 100 = 100.00
function formatarValor(valor) {
    return valor.toLocaleString('pt-BR');
}

// Esconde o loading
function LoadPageEsconde(valor) {
    document.getElementById("circle").style.visibility = "Hidden";
};

// Esconde o Mostra o loading
function LoadPage(valor) {
    document.getElementById("circle").style.visibility = "Show";
};

//  //////////////////////////////////////////////////// Cards 
function CarregarCards() {
    $.ajax({
        url: "Default.aspx/LoadCards",
        data: "",
        dataType: "json",
        type: "POST",
        contentType: "application/json; chartset=utf-8",
        success: function (data) {
            CardData = data.d;
        },
        error: function () {
            alert("Error loading data! Please try again.");
        }
    }).done(function () {
        google.setOnLoadCallback(drawCard);

    });

    function drawCard() {
        //pegando dados 
        var card = google.visualization.arrayToDataTable(CardData);
        var dadosCard = []

        // colocando os dados em um array para ser usado no dataTable
        for (var i = 0; i < card.getNumberOfRows() ; i++) {
            dadosCard[card.getValue(i, 0)] = [card.getValue(i, 1)]
        }
        document.getElementById("evergreenvalue").innerHTML = formatarValor(dadosCard['EVERGREEN']);
        document.getElementById("esaValue").innerHTML = formatarValor(dadosCard['ESA']);
        document.getElementById("snaValue").innerHTML = formatarValor(dadosCard['SNA']);
        document.getElementById("eusaValue").innerHTML = formatarValor(dadosCard['EUSA']);
    }
};
////////////////////////////////// Fim cards/////////////////////////////////////////////


//////////////////////////////////////// começo MAPA///////////////////////////////////////////////
function Graphics_Maps() {
    LoadPage();
    $.ajax({
        url: "Default.aspx/MapCountriesGraphic",
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
        google.setOnLoadCallback(drawRegionsMap);
    });
}


function drawRegionsMap() {
    var data = google.visualization.arrayToDataTable(chartData)

    var options = {
        colorAxis: { colors: ['#0cc764', '#038741', '#125c35'] },
    };

    var chart = new google.visualization.GeoChart(document.getElementById('Mapa_chart'));
    chart.draw(data, options);
    LoadPageEsconde();

    //////////////////////////////////Fim mapa////////////////////////////////////////////////////////////
}


////////////////////////////////////// inicio pie exportação ///////////////////////////
function PieArmardorExp() {
    LoadPage();
    $.ajax({
        url: "Default.aspx/ArmadorExpGraphic",
        data: "",
        dataType: "json",
        type: "POST",
        contentType: "application/json; chartset=utf-8",
        success: function (data) {
            CharArmardorExp = data.d;
        },
        error: function () {
            alert("Error loading data! Please try again.");
        }
    }).done(function () {
        google.setOnLoadCallback(drawChartPieExp);
    });
}

function drawChartPieExp() {
    var data = google.visualization.arrayToDataTable(CharArmardorExp);

    //criando array de cores basiado em valores 
    var slicesColor = {};
    // setando valores para o array de cores
    for (var i = 0; i < data.getNumberOfRows() ; i++) {
        slicesColor[i] = {
            offset: (i == 0 || data.getValue(i, 0) == 'EVERGREEN' ? "0.4" : "0.0"),
            color: myColors[data.getValue(i, 0)]
        };
    }

    var options = {
        title: "Top FIVE exportação ",
        'is3D': true,
        'legend': 'labeled', fontSize: 10,
        'pieSliceText': 'none',
        legend: { position: 'labeled', labeledValueText: 'value' },
        tooltip: {
            text: 'value'
        },
        slices: slicesColor,
        pointSize: 5
    };

    var columnChartEXP = new google.visualization.PieChart(document.getElementById('PieArmadorExportacao'));
    columnChartEXP.draw(data, options);

    LoadPageEsconde();
    ///////////////// fim PIE exportação////////////////////////////////////////////////////////////////////////////////////////////////////
}

///////////////////////////////////////// Começo Pie IMPORTAÇÂO //////////////////////////////////////////////////////////
function PieArmardorimp() {
    LoadPage();
    $.ajax({
        url: "Default.aspx/ArmadorImpGraphic",
        data: "",
        dataType: "json",
        type: "POST",
        contentType: "application/json; chartset=utf-8",
        success: function (data) {
            DadosCharArmardorIMP = data.d;
        },
        error: function () {
            alert("Error loading data! Please try again.");
        }
    }).done(function () {
        google.setOnLoadCallback(drawChartPieIMP);
    });
}

function drawChartPieIMP() {
    var data = google.visualization.arrayToDataTable(DadosCharArmardorIMP);

    //criando array de cores basiado em valores 
    var slicesColor = {};
    // setando valores para o array de cores
    for (var i = 0; i < data.getNumberOfRows() ; i++) {
        slicesColor[i] = {
            offset: (i == 0 || data.getValue(i, 0) == 'EVERGREEN' ? "0.4" : "0.0"),
            color: myColors[data.getValue(i, 0)]
        };
    }


    var options = {
        title: "Top FIVE importação  ",
        'is3D': true,
        'legend': 'labeled', fontSize: 10,
        'pieSliceText': 'none',
        legend: { position: 'labeled', labeledValueText: 'value' },
        tooltip: {
            text: 'value'
        },
        slices: slicesColor,
        pointSize: 5
    };

    var columnChartImp = new google.visualization.PieChart(document.getElementById('PieArmadorImportacao'));
    columnChartImp.draw(data, options);

    LoadPageEsconde();

    ///////////////// fim PIE importação ////////////////////////////////////////////////////////////////////////////////////////////////////
}

/////////////////////////////////////////////////////// começo comddity //////////////////////////////////////////////////////////////////////////////
function CommoditGraphics() {
    LoadPage();
    $.ajax({
        url: "Default.aspx/CommoditGraphic",
        data: "",
        dataType: "json",
        type: "POST",
        contentType: "application/json; chartset=utf-8",
        success: function (data) {
            ChartDataComiddity = data.d;
        },
        error: function () {
            alert("Error loading data! Please try again.");
        }
    }).done(function () {
        google.setOnLoadCallback(drawCommoditGraphics);
    });
}

function drawCommoditGraphics() {


    //pegando dados 
    var dataComodity = google.visualization.arrayToDataTable(ChartDataComiddity);
    var dadosC = []

    // colocando os dados em um array para ser usado no dataTable
    for (var i = 0; i < dataComodity.getNumberOfRows() ; i++) {
        dadosC[i] = [dataComodity.getValue(i, 0), dataComodity.getValue(i, 1), dataComodity.getValue(i, 1)]
    }

    var tabelaC = new google.visualization.DataTable();
    tabelaC.addColumn('string', 'Commodity');
    tabelaC.addColumn('number', 'Teus');
    tabelaC.addColumn({ type: 'number', role: 'annotation' });
    // cria uma anotação para mostrar o numero em cima da coluna

    // Associando valores 
    tabelaC.addRows(dadosC);

    console.log(dadosC);

    var optionsC = {
        title: 'Top FIVE commodity',
        vAxis: { textPosition: 'none' },
        legend: 'none',
    };

    var columnChartComodidty = new google.visualization.ColumnChart(document.getElementById('Comoddty_chart'));
    columnChartComodidty.draw(tabelaC, optionsC);

    LoadPageEsconde();

}
///////////////////////////////// Fim colunas de comodity //////////////////////////////////////////////////////////////////////////////////


////////////////////////// Começo da Histotico mensal /////////////////////////////////////////////////////////////////////////////////////////////
function ColunaPorMes() {
    LoadPage();
    $.ajax({
        url: "Default.aspx/ColunaPorMesGraphic",
        data: "",
        dataType: "json",
        type: "POST",
        contentType: "application/json; chartset=utf-8",
        success: function (data) {
            DatahartColumn = data.d;
        },
        error: function () {
            alert("Error loading data! Please try again.");
        }
    }).done(function () {
        google.setOnLoadCallback(DrawColunaPorMes);
    });
}

function DrawColunaPorMes() {

    //pegando dados 
    var dataColumn = google.visualization.arrayToDataTable(DatahartColumn);
    var dados = []

    // colocando os dados em um array para ser usado no dataTable
    for (var i = 0; i < dataColumn.getNumberOfRows() ; i++) {
        dados[i] = [dataColumn.getValue(i, 0), dataColumn.getValue(i, 1), dataColumn.getValue(i, 1)]
    }

    // criando data table para a coluna 
    var tabela = new google.visualization.DataTable();
    tabela.addColumn('string', 'Mês');
    tabela.addColumn('number', 'Teus');
    tabela.addColumn({ type: 'number', role: 'annotation' }); // cria uma anotação para mostrar o numero em cima da coluna

    // Associando valores 
    tabela.addRows(dados);

    var optionsColunm = {
        title: 'Historico mensal',
        vAxis: { textPosition: 'none' },
        legend: 'none',
    };

    var grafico = new google.visualization.ColumnChart(document.getElementById('ColPorMes'));
    grafico.draw(tabela, optionsColunm);

    ////////////////////////////////////////////// Fim estorico mensal ///////////////////////////////////////////


    ///////////////////////////////// quando chegar ao fim feche o load
    LoadPageEsconde();

}