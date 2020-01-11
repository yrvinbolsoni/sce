<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Form.aspx.cs" Inherits="SCE.Form" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
       
    <script>
        var chartData; // globar variable for hold chart data
        google.load("visualization", "1", { packages: ["corechart"] });

        function Graphics_Click(rota ,NomeRelatorio) {
            $.ajax({
                url: "Form.aspx/"+rota,
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
                drawChart(NomeRelatorio);
            });
        }
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

        function drawChart(NomeRelatorio) {
            var data = google.visualization.arrayToDataTable(chartData);

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
                title:"Top TEN "+ NomeRelatorio,
                'height': 500,
                'width': 1000,
                'is3D': true,
                'legend': 'labeled', fontSize: 12,  // Como vai mostrar a legenda em Top ,Bottom , left , right ou labeled que vai fazer uma linha indicando no grafico
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
            ///////////////// fim PIE////////////////////////////////////////////////////////////////////////////////////////////////////

             

            /////////////////////////////////////////////////////// INICIO COLUNAS /////////////////////////////////////////////////////
            //pegando dados 
            var dataColumn = google.visualization.arrayToDataTable(chartData);
            var dados = []

            // colocando os dados em um array para ser usado no dataTable
            for (var i = 0; i < dataColumn.getNumberOfRows() ; i++) {
                dados[i] = [dataColumn.getValue(i, 0), dataColumn.getValue(i, 1), dataColumn.getValue(i, 1), myColors[dataColumn.getValue(i, 0)]]
            }

            // criando data table para a coluna 
            var tabela = new google.visualization.DataTable();
            tabela.addColumn('string', 'Armador');
            tabela.addColumn('number', 'Teus');
            tabela.addColumn({ type: 'number', role: 'annotation' }); // cria uma anotação para mostrar o numero em cima da coluna
            tabela.addColumn({ type: 'string', role: 'style' });

            // Associando valores 
            tabela.addRows(dados);

      
            var optionsColunm = {
                title: 'Top TEN ' +NomeRelatorio,
                height: 500,
                width: 1000,
                vAxis: { gridlines: { count: 0 }, textPosition: 'none' }, // retira o valor da coluna a direita
                legend: 'none' , // retira o valor da legenda
            };

            var columnChart = new google.visualization.ColumnChart(document.getElementById('chart_Col'));
            columnChart.draw(tabela, optionsColunm);
            /////////////////////////////////////////////////////// fim chart colunas  COLORIDAS BASEADO EM VALORES /////////////////////////////////////////////
        }
    


 
    </script>
 
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="ListYear" runat="server" ErrorMessage="* Year is a required field." ForeColor="Red"></asp:RequiredFieldValidator>
            <div style="font-family: Arial">
                <div class="stepwizard">
                    <div class="stepwizard-row">
                        <div class="stepwizard-step col-lg-2 ">
                            <asp:Button ID="ButtonYear" class="btn-circle" runat="server" Text="1"   OnClick="ButtonYear_Click" />
                            <p>Year/Direction/Brz Ports/Types</p>
                        </div>
                        <div class="stepwizard-step col-lg-3 ">
                            <asp:Button ID="ButtonRoutes" class="btn-circle" runat="server" Text="2"   OnClick="ButtonRoutes_Click" />
                            <p>Routes/Areas/Foringn Ports</p>
                        </div>
                        <div class="stepwizard-step col-lg-3 ">
                            <asp:Button ID="ButtonCarrier" class="btn-circle" runat="server" Text="3"   OnClick="ButtonCarrier_Click" />
                            <p>Carrier/Commodity/Client</p>
                        </div>
                        <div class="stepwizard-step col-lg-2 ">
                            <asp:Button ID="ButtonReports" class="btn-circle" runat="server" Text="4"   OnClick="ButtonReports_Click" />
                            <p>Choose the Report/</p>
                        </div>
                        <div class="stepwizard-step col-lg-2 ">
                            <asp:Button ID="ButtonViwerReport" class="btn-circle" runat="server" Text="5"   OnClick="ButtonViwerReport_Click" CssClass="btn-circle" Enabled="False" />
                            <p>Report/ Viewer</p>
                        </div>

                    </div>
                </div>

                <div id="Erros" runat="server"></div>
                <asp:MultiView ID="MultiView" runat="server">
                    <asp:View ID="viewPersonalDetails" runat="server">
                        <h4>Year/Direction/Brz Ports/Types</h4>
                        <div class="Row">
                            <div class="col-lg-2 col-md-2">
                                <h4>Year/</h4>
                                <asp:ListBox ID="ListYear" ValidationGroup="ListYear" runat="server" Rows="6"></asp:ListBox>

                            </div>
                            <div class="col-lg-2 col-md-2">
                                <h4>Start month </h4>
                                <asp:ListBox ID="StartMonth" runat="server" Rows="6">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:ListBox>
                            </div>

                            <div class="col-lg-2 col-md-2 ">
                                <h4>Final month</h4>
                                <asp:ListBox ID="FinalMonth" runat="server" Rows="6">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:ListBox>
                            </div>

                            <div class="col-lg-3 col-md-3 ">
                                <h4>Brazilian Ports</h4>
                                <asp:ListBox ID="ListPort" runat="server" SelectionMode="Multiple" Rows="8"></asp:ListBox>
                            </div>
                            <div class="col-lg-2 col-md-2">
                                <h4>Direction</h4>
                                <asp:ListBox ID="ListDirecao" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>

                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <%--  Proxima linha --%>

                            <div class="Row">
                                <div class="col-lg-2 col-md-2" >
                                    <h4>Type of container</h4>
                                    <asp:ListBox ID="ListContainer" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4" >
                                <h4>Type of Client</h4>
                                <asp:ListBox ID="ListCliente" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-lg-3 col-md-3">
                                <h4>Loading type</h4>
                                <asp:ListBox ID="ListCarregamento" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-lg-3 col-md-3 " >
                                <h4>Restrictions</h4>
                                <asp:ListBox ID="ListRestricao" runat="server" SelectionMode="Multiple" Width="210px"></asp:ListBox>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </asp:View>
                    <asp:View ID="viewContactDetails" runat="server">
                        <h4>Routes/Areas/Foring Ports</h4>
                        <div class="Row">
                            <div class="col-lg-4 col-md-4">
                                <h4>Service/Rotes</h4>

                                <asp:ListBox ID="ListRotas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListRotas_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-lg-4 col-md-4">
                                <h4>Areas</h4>
                                <asp:ListBox ID="ListAreas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListAreas_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>

                            </div>
                            <div class="col-lg-4 col-md-4">
                                <h4>Regions</h4>
                                <asp:ListBox ID="ListRegiao" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListRegiao_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                        <%--  Proxima linha --%>
                        <div class="Row">
                            <div class="col-lg-4 col-md-4">
                                <h4>Select to CONTRIES</h4>
                                <asp:TextBox ID="pPais" Style="text-transform: uppercase;" runat="server"></asp:TextBox>
                                <asp:Button ID="PesquisaPais" runat="server" Text="Search" OnClick="SelectPaisPesquisa_Click" />
                                <h4>Select below the countries of your choice</h4>
                                <div runat="server" id="ErroPais" style="color: red;"></div>
                                <asp:ListBox ID="ListPais" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListPais_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-lg-4 col-md-4">
                                <h4>Select to PODs</h4>
                                <asp:TextBox ID="PortPesquisa" Style="text-transform: uppercase;" runat="server"></asp:TextBox>
                                <asp:Button ID="SelectPaisPortPesquisa" runat="server" Text="Search" OnClick="SelectPaisPortPesquisa_Click" />
                                <div runat="server" id="ErroPortPais" style="color: red;"></div>
                                <h4>Select below the ports of your choice</h4>
                                <asp:ListBox ID="ListPortsPais" runat="server" SelectionMode="Multiple"></asp:ListBox>

                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />

                    </asp:View>
                    <asp:View ID="viewSummary" runat="server">

                        <h4>Carrier/Commodity/Client</h4>
                        <div class="Row">
                            <div class="col-lg-6 col-md-6">
                                <h4>Carrier</h4>
                                <asp:ListBox ID="ListTranpostadora" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-lg-6 col-md-6">
                                <h4>Commodity</h4>
                                <h4>Search Commodity</h4>
                                <asp:TextBox ID="Pmercadoria" Style="text-transform: uppercase;" runat="server"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Search" OnClick="PesquisaMercadoria_Click" />
                                <div runat="server" id="ErroMercadoria" style="color: red;"></div>
                                <br />
                                <asp:ListBox ID="ListMercadoria" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <%--  Proxima linha --%>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div class="Row">

                            <div class="col-lg-6 col-md-6">
                                <h4>Sales Rep</h4>
                                <asp:ListBox ID="ListVendedor" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>

                            <div class="col-lg-6 col-md-6">
                                <h4>Search CLIENTS</h4>
                                <asp:TextBox ID="Pcliente" Style="text-transform: uppercase;" runat="server"></asp:TextBox>
                                <asp:Button ID="SelectCliente" runat="server" Text="Search" OnClick="SelectCliente_Click" />
                                <div runat="server" id="ErroCliente" style="color: red;"></div>
                                <h4>Select below the clientes of your choice</h4>
                                <asp:ListBox ID="ListClientes" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ListClientes_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>

                               <%-- <asp:ListBox ID="NovaListaCliente" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="NovaListaCliente_SelectedIndexChanged" SelectionMode="Multiple" Width="206px"></asp:ListBox>--%>
                            </div>
                             
                             
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />

                    </asp:View>


                    <asp:View ID="Resultado" runat="server">

                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="Content/Img/loading2.gif" width="80px" height="80px" class="loader" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <h4>Reports/</h4>
                        
                                 <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th scope="col"></th>
                                    <th scope="col"></th>
                                    <th scope="col"></th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr class="table-active">
                                    <td>
                                        <asp:Button ID="TotalMovementsByPortButton" runat="server" Text="Total Movements by Port" Width="200px" OnClick="TotalMovementsByPortButton_Click" />
                                        <asp:ImageButton ID="TotalMovementsByPortExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="TotalMovementsByPortExcelButton_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="SummaryPerDischargePortButton" runat="server" Text="Summary per Discharge Port" Width="200px" OnClick="SummaryPerDischargePortButton_Click" />
                                        <asp:ImageButton ID="SummaryPerDischargePortExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryPerDischargePortExcelButton_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="ShipperCneeByPortPairsButton" runat="server" Text="Shipper - Cnee by port pair" Width="200px" OnClick="ShipperCneeByPortPairsButton_Click" />
                                        <asp:ImageButton ID="ShipperCneeByPortPairsExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="ShipperCneeByPortPairsExcelButton_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="SumarryServiceButton" runat="server" Text="Summary - Service" Width="200px" OnClick="SumarryServiceButton_Click" OnClientClick="progress()" />
                                        <asp:ImageButton ID="SumarryServiceExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SumarryServiceExcelButton_Click" />
                                        <asp:ImageButton ID="SummaryServiceGraphics" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('SummaryServiceGraphic', 'Summary Service')" OnClick="SummaryServiceGraphics_Click" />

                                    </td>

                                    <td>
                                        <asp:Button ID="SummaryCarrierButton" runat="server" Text="Summary - Carrier" Width="200px" OnClick="SummaryCarrierButton_Click" />
                                        <asp:ImageButton ID="SummaryCarrierExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryCarrierExcelButton_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="CneeShipperByPortPairButton" runat="server" Text="Cnee Shipper by port pair" Width="200px" OnClick="CneeShipperByPortPairButton_Click" />
                                        <asp:ImageButton ID="CneeShipperByPortPairExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="CneeShipperByPortPairExcelButton_Click" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button ID="SummaryAreaButton" runat="server" Text="Summary - Area" Width="200px" OnClick="SummaryAreaButton_Click" />
                                        <asp:ImageButton ID="SummaryAreaExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryAreaExcelButton_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="DetailedPerContainerButton" runat="server" Text="Detailed per Container" Width="200px" OnClick="DetailedPerContainerButton_Click" />
                                        <asp:ImageButton ID="DetailedPerContainerExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="DetailedPerContainerExcelButton_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="ClientCneeButton" runat="server" Text="Client - Cnee" Width="200px" OnClick="ClientCneeButton_Click" />
                                        <asp:ImageButton ID="ClientCneeExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="ClientCneeExcelButton_Click1" />

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button ID="SummaryRakingButton" runat="server" Text="Summary - Ranking" Width="200px" OnClick="SummaryRakingButton_Click" />
                                        <asp:ImageButton ID="SummaryRakingExecelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryRakingExecelButton_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="DetailedPerClientButton" runat="server" Text="Detailed per Client" Width="200px" OnClick="DetailedPerClientButton_Click" />
                                        <asp:ImageButton ID="DetailedPerExcelClientButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="DetailedPerExcelClientButton_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="CneeClientButton" runat="server" Text="Cnee Client" Width="200px" OnClick="CneeClientButton_Click" />
                                        <asp:ImageButton ID="CneeClientExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="CneeClientExcelButton_Click" />

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button ID="StatsPerContainerAreaButton" runat="server" Text="Stats per Container / Area" Width="200px" OnClick="StatsPerContainerAreaButton_Click" />
                                        <asp:ImageButton ID="StatsPerContainerAreaExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="StatsPerContainerAreaExcelButton_Click" />
                                        <asp:ImageButton ID="StatsPerContainerAreaGraphic" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('StatsPerContainerAreaGraphics', 'StatsPer Container Area ')" OnClick="StatsPerContainerAreaGraphic_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="TypeContainerForeignPortButton" runat="server" Text="Type Container - Foreign Port" Width="200px" OnClick="TypeContainerForeignPortButton_Click" />
                                        <asp:ImageButton ID="TypeContainerForeignPortExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="TypeContainerForeignPortExcelButton_Click" />
                                        <asp:ImageButton ID="TypeContainerForeignPortGraphic" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('TypeContainerForeignPortGraphics', 'Type Container ForeignPort')" OnClick="TypeContainerForeignPortGraphic_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="ClientAreaButton" runat="server" Text="Client Area" Width="200px" OnClick="ClientAreaButton_Click" />
                                        <asp:ImageButton ID="ClientAreaExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="ClientAreaExcelButton_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="StatsPerContainerCarrierButton" runat="server" Text="Stats Per Container / Carrier" Width="200px" OnClick="StatsPerContainerCarrierButton_Click" />
                                        <asp:ImageButton ID="StatsPerContainerExcelCarrierButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="StatsPerContainerExcelCarrierButton_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="CarrierClientButton" runat="server" Text="Carrier - Client" Width="200px" OnClick="CarrierClientButton_Click" />
                                        <asp:ImageButton ID="CarrierClientExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="CarrierClientExcelButton_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="ClientSrepButton" runat="server" Text="Client - Srep" Width="200px" OnClick="ClientSrepButton_Click" />
                                        <asp:ImageButton ID="ClientSrepExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="ClientSrepExcelButton_Click" />
                                        <asp:ImageButton ID="ClientSrepGraphic" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('ClientSrepGraphics', 'Client Srep ')" OnClick="ClientSrepGraphic_Click" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button ID="RankingServiceButton" runat="server" Text="Ranking - Service" Width="200px" OnClick="RankingServiceButton_Click" />
                                        <asp:ImageButton ID="RankingServiceExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="RankingServiceExcelButton_Click" />
                                        <asp:ImageButton ID="RankingServiceGraphic" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('RankingServiceGraphics', 'Ranking Service')" OnClick="RankingServiceGraphic_Click" />
                                    </td>

                                    <td>
                                        <asp:Button ID="CarrierCommodityButton" runat="server" Text="Carrier - Commodity" Width="200px" OnClick="CarrierCommodityButton_Click" />
                                        <asp:ImageButton ID="CarrierCommoditExcelyButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="CarrierCommoditExcelyButton_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="FollowUpReportButton" runat="server" Text="Follow up - Clientes" Width="200px" OnClick="FollowUpReportButton_Click" />
                                        <asp:ImageButton ID="FollowUpReportExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="FollowUpReportExcelButton_Click" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="RankingServiceByAreaButton" runat="server" Text="Ranking - Service By Area" Width="200px" OnClick="RankingServiceByAreaButton_Click" />
                                        <asp:ImageButton ID="RankingServiceByExcelAreaButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="RankingServiceByExcelAreaButton_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="CarrierVasselButton" runat="server" Text="Carrier - Vassel" Width="200px" OnClick="CarrierVasselButton_Click" />
                                        <asp:ImageButton ID="CarrierVasselExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="CarrierVasselExcelButton_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="SummaryPortsButton" runat="server" Text="Summary - port" Width="200px" OnClick="SummaryPortsButton_Click" />
                                        <asp:ImageButton ID="SummaryPortsExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryPortsExcelButton_Click" />
                                        <asp:ImageButton ID="SummaryPortsGraphic" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('SummaryPortsGraphics', 'Summary Ports')" OnClick="SummaryPortsGraphic_Click" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button ID="SummaryBrazilButton" runat="server" Text="Summary - Brazil" Width="200px" OnClick="SummaryBrazilButton_Click" />
                                        <asp:ImageButton ID="SummaryBrazilExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryBrazilExcelButton_Click" />
                                        <asp:ImageButton ID="SummaryBrazilExcelGraphic" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('SummaryBrazilExcelGraphics', 'Summary Brazil')" OnClick="SummaryBrazilExcelGraphic_Click" />

                                    </td>

                                    <td>
                                        <asp:Button ID="CommodityAreaButton" runat="server" Text="Commodity - Area" Width="200px" OnClick="CommodityAreaButton_Click" />
                                        <asp:ImageButton ID="CommodityAreaExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="CommodityAreaExcelButton_Click" />

                                    </td>

                                    <td>
                                        <asp:Button ID="SummaryCountriesButton" runat="server" Text="Summary Countries" Width="200px" OnClick="SummaryCountriesButton_Click" />
                                        <asp:ImageButton ID="SummaryCountriesExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryCountriesExcelButton_Click" />
                                        <asp:ImageButton ID="SummaryCountriesGraphic" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('SummaryCountriesGraphics', 'Summary Countries ')" OnClick="SummaryCountriesGraphic_Click" />

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button ID="SummaryClientButton" runat="server" Text="Summary- Client" Width="200px" OnClick="SummaryClientButton_Click" />
                                        <asp:ImageButton ID="SummaryClientExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryClientExcelButton_Click" />

                                    </td>

                                    <td>
                                        <asp:Button ID="ForeignPortClientButton" runat="server" Text="Foreign Port - Client" Width="200px" OnClick="ForeignPortClientButton_Click" />
                                        <asp:ImageButton ID="ForeignPortClientExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="ForeignPortClientExcelButton_Click" />

                                    </td>

                                    <td>
                                        <asp:Button ID="SummaryClientMonthButton" runat="server" Text="Summary Client Month" Width="200px" OnClick="SummaryClientMonthButton_Click" />
                                        <asp:ImageButton ID="SummaryClientMonthExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="SummaryClientMonthExcelButton_Click" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button ID="RankingCommodityButton" runat="server" Text="Ranking - Commodity" Width="200px" OnClick="RankingCommodityButton_Click" />
                                        <asp:ImageButton ID="RankingCommodityExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="RankingCommodityExcelButton_Click" />
                                        <asp:ImageButton ID="RankingCommodityGraphic" runat="server" Height="25" ImageUrl="Content/Img/IconPie.png" OnClientClick="Graphics_Click('RankingCommodityGraphics', 'Ranking Commodit')" OnClick="RankingCommodityGraphic_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="ForeignPortCarrierButton" runat="server" Text="Foreign Port - Carrier" Width="200px" OnClick="ForeignPortCarrierButton_Click" />
                                        <asp:ImageButton ID="ForeignPortCarrierExcelButton" runat="server" Height="25" ImageUrl="Content/Img/iconsExcel.png" OnClick="ForeignPortCarrierExcelButton_Click" />

                                    </td>
                                    <td> 
                                        <asp:Button ID="GraphicButton" runat="server" Text="Graphic" Width="200px" OnClick="GraphicButton_Click" Visible="false" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>

            </asp:View>
             <asp:View ID="View1" runat="server">
                 <rsweb:ReportViewer ID="ReportViewer1" BackColor="#DAEBFF" Height="800px" Width="100%" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" AsyncRendering="False">
                 </rsweb:ReportViewer>
 
             </asp:View>
            </asp:MultiView>
                 <div>

               <br />
            <br />
        </div>
      </div>
 
        </ContentTemplate>
             
    </asp:UpdatePanel>
            <%--    // Graficos--%>

      <div id="Graphics" >
        <div id="chart_Pie" >
        </div>
            <hr /> 
        <div id="chart_Col" >
        </div>
    </div> 

</asp:Content>
