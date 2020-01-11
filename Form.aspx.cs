using System;
using System.Web.UI.WebControls;
using SCE.DAO;
using System.Drawing;
using System.Web.UI;
using Npgsql;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Linq;
using SCE.DAL;
using SCE.Models;

namespace SCE
{
    public partial class Form :  Page
    {
        // configurações
        private string cs = ConfigurationManager.AppSettings["BancoProducao"];
        private Util utl = new Util();
        private  ReportsDB ReportsDal = new ReportsDB();
        public static Parametros parametros = new Parametros();
        private DropDownListDados dados = new DropDownListDados();

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonPost();

            if (!IsPostBack)
            {
                EstiloStepWizard();

                try
                {
                    LoadDropdonwList();
                }
                catch (Exception ex)
                {
                    utl.addLog("System", "Dropdown", "",  ex.ToString(), MapPath("LOG.xml"));
                }
            }
        }

        #region DropDonwList 
        private void LoadDropdonwList()
        {
            ListYear.DataSource = dados.ListYear();
            ListYear.DataTextField = "year";
            ListYear.DataValueField = "year";
            ListYear.DataBind();

            ListPort.DataSource = dados.ListPort();
            ListPort.DataTextField = "lporto";
            ListPort.DataValueField = "lporto";
            ListPort.DataBind();

            ListDirecao.DataSource = dados.ListDirection();
            ListDirecao.DataTextField = "text";
            ListDirecao.DataValueField = "cod";
            ListDirecao.DataBind();

            ListContainer.DataSource = dados.ListTipoContainer();
            ListContainer.DataTextField = "text";
            ListContainer.DataValueField = "tcontainer";
            ListContainer.DataBind();

            ListCliente.DataSource = dados.ListTipoCliente();
            ListCliente.DataTextField = "text";
            ListCliente.DataValueField = "tipo";
            ListCliente.DataBind();

            ListCarregamento.DataSource = dados.List_carregamento();
            ListCarregamento.DataTextField = "text";
            ListCarregamento.DataValueField = "mode";
            ListCarregamento.DataBind();

            ListRestricao.DataSource = dados.ListRestricao();
            ListRestricao.DataTextField = "text";
            ListRestricao.DataValueField = "problem_code";
            ListRestricao.DataBind();

            /// regiao aba 2 

            ListRotas.DataSource = dados.ListRotas();
            ListRotas.DataTextField = "text";
            ListRotas.DataValueField = "service";
            ListRotas.DataBind();

            ListAreas.DataSource = dados.ListAreas();
            ListAreas.DataTextField = "text";
            ListAreas.DataValueField = "fk_area";
            ListAreas.DataBind();

            ListRegiao.DataSource = dados.ListRegiao();
            ListRegiao.DataTextField = "text";
            ListRegiao.DataValueField = "fk_region";
            ListRegiao.DataBind();

            ListPais.DataSource = dados.ListPais();
            ListPais.DataTextField = "text";
            ListPais.DataValueField = "fk_country";
            ListPais.DataBind();
            /// regiao aba 3

            ListTranpostadora.DataSource = dados.ListTransportadora();
            ListTranpostadora.DataTextField = "carrier";
            ListTranpostadora.DataValueField = "carrier";
            ListTranpostadora.DataBind();

            ListMercadoria.DataSource = dados.ListMercadoria();
            ListMercadoria.DataTextField = "commodity";
            ListMercadoria.DataValueField = "commodity";
            ListMercadoria.DataBind();

            ListVendedor.DataSource = dados.ListVendedores();
            ListVendedor.DataTextField = "text";
            ListVendedor.DataValueField = "srep";
            ListVendedor.DataBind();

            ListPortsPais.DataSource = dados.ListPortsDePaises();
            ListPortsPais.DataTextField = "text";
            ListPortsPais.DataValueField = "odporto";
            ListPortsPais.DataBind();

            ListClientes.DataSource = dados.ListCliente();
            ListClientes.DataTextField = "cliente";
            ListClientes.DataValueField = "cliente";
            ListClientes.DataBind();
        }

        protected void SelectCliente_Click(object sender, EventArgs e)
        {
            DropDownListDados dados = new DropDownListDados();

            if (dados.ListVendedoresPesquisa(Pcliente.Text.ToUpper()).Tables[0].Rows.Count != 0)
            {
                ListClientes.DataSource = dados.ListVendedoresPesquisa(Pcliente.Text.ToUpper());
                ListClientes.DataTextField = "cliente";
                ListClientes.DataValueField = "cliente";
                ErroCliente.InnerHtml = " ";
                ListClientes.DataBind();
                ErroCliente.Visible = false;
            }
            else
            {
                ErroCliente.Visible = true;
                ErroCliente.InnerHtml = "nada foi encontrado";
            }

        }

        protected void SelectPaisPesquisa_Click(object sender, EventArgs e)
        {
            DropDownListDados dados = new DropDownListDados();

            if (dados.PesquisaPaisList(pPais.Text.ToUpper()).Tables[0].Rows.Count != 0)
            {
                ListPais.DataSource = dados.PesquisaPaisList(pPais.Text.ToUpper());
                ListPais.DataTextField = "text";
                ListPais.DataValueField = "fk_country";
                ErroPais.InnerHtml = " ";
                ListPais.DataBind();
                ErroPais.Visible = false;
            }
            else
            {
                ErroPais.Visible = true;
                ErroPais.InnerHtml = "nada foi encontrado";
            }
        }

        protected void SelectPaisPortPesquisa_Click(object sender, EventArgs e)
        {
            DropDownListDados dados = new DropDownListDados();

            if (dados.PesquisaPaisPort(PortPesquisa.Text.ToUpper()).Tables[0].Rows.Count != 0)
            {
                ListPortsPais.DataSource = dados.PesquisaPaisPort(PortPesquisa.Text.ToUpper());
                ListPortsPais.DataTextField = "text";
                ListPortsPais.DataValueField = "odporto";
                ErroPortPais.InnerHtml = " ";
                ListPortsPais.DataBind();
                ErroPortPais.Visible = false;

            }
            else
            {
                ErroPortPais.Visible = true;
                ErroPortPais.InnerHtml = "nada foi encontrado";
            }
        }

        protected void PesquisaMercadoria_Click(object sender, EventArgs e)
        {
            DropDownListDados dados = new DropDownListDados();

            if (dados.PesquisaCommodity(Pmercadoria.Text.ToUpper()).Tables[0].Rows.Count != 0)
            {
                ListMercadoria.DataSource = dados.PesquisaCommodity(Pmercadoria.Text.ToUpper());
                ListMercadoria.DataTextField = "commodity";
                ListMercadoria.DataValueField = "commodity";
                ErroMercadoria.InnerHtml = " ";
                ListMercadoria.DataBind();
                ErroMercadoria.Visible = false;
            }
            else
            {
                ErroMercadoria.Visible = true;
                ErroMercadoria.InnerHtml = "nada foi encontrado";
            }
        }

        protected void ListRotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListDados dados = new DropDownListDados();
            string busca = "";
            foreach (ListItem li in ListRotas.Items)
            {
                if (li.Selected)
                {
                    busca += "'" + li.Value + "'" + ',';
                }
            }
            busca += "'" + "'";
            ListAreas.DataSource = dados.ListAreasDinamic(busca);
            ListAreas.DataTextField = "text";
            ListAreas.DataValueField = "fk_area";
            ListAreas.DataBind();

        }

        protected void ListAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListDados dados = new DropDownListDados();
            string busca = "";
            foreach (ListItem li in ListAreas.Items)
            {
                if (li.Selected)
                {

                    busca += "'" + li.Value + "'" + ',';
                }
            }
            busca += "'" + "'";
            ListRegiao.DataSource = dados.ListARegiaoDinamic(busca);
            ListRegiao.DataTextField = "text";
            ListRegiao.DataValueField = "fk_region";
            ListRegiao.DataBind();
        }


        protected void ListRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListDados dados = new DropDownListDados();
            string busca = "";
            foreach (ListItem li in ListRegiao.Items)
            {
                if (li.Selected)
                {

                    busca += "'" + li.Value + "'" + ',';
                }
            }
            busca += "'" + "'";
            ListPais.DataSource = dados.ListPaisDinamic(busca);
            ListPais.DataTextField = "text";
            ListPais.DataValueField = "fk_country";
            ListPais.DataBind();
        }

        protected void ListPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListDados dados = new DropDownListDados();
            string busca = "";
            foreach (ListItem li in ListPais.Items)
            {
                if (li.Selected)
                {

                    busca += "'" + li.Value + "'" + ',';
                }
            }
            busca += "'" + "'";
            ListPortsPais.DataSource = dados.ListPaisPortDinamic(busca);
            ListPortsPais.DataTextField = "text";
            ListPortsPais.DataValueField = "odporto";
            ListPortsPais.DataBind();

        }

        #endregion

        #region Componente ASP:MultiView  e funções de estilos
        // css => stepwizard
        // registrar o botao para que possa fazer um POST

        public void EstiloStepWizard()
        {
            // progress.Visible = false;
            ButtonYear.BackColor = Color.DeepSkyBlue;
            ButtonYear.BorderColor = Color.ForestGreen;

            ButtonViwerReport.BackColor = Color.Silver;
            ButtonViwerReport.BorderColor = Color.Silver;

            MultiView.ActiveViewIndex = 0;
            Erros.Visible = false;
        }

        private void ButtonPost()
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(TotalMovementsByPortExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryPerDischargePortExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(ShipperCneeByPortPairsExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SumarryServiceExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryCarrierExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(CneeShipperByPortPairExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryAreaExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(DetailedPerContainerExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(ClientCneeExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryRakingExecelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(DetailedPerExcelClientButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(CneeClientExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(StatsPerContainerAreaExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(TypeContainerForeignPortExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(ClientAreaExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(StatsPerContainerExcelCarrierButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(CarrierClientExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(ClientSrepExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(RankingServiceExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(CarrierCommoditExcelyButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(FollowUpReportExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(RankingServiceByExcelAreaButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(CarrierVasselExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryPortsExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryBrazilExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(CommodityAreaExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryCountriesExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryClientExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(ForeignPortClientExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(SummaryClientMonthExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(RankingCommodityExcelButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(ForeignPortCarrierExcelButton);
        }

        protected void ButtonYear_Click(object sender, EventArgs e)
        {
            ButtonYear.BackColor = Color.DeepSkyBlue;
            ButtonYear.BorderColor = Color.ForestGreen;

            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;

            ButtonCarrier.BackColor = Color.Silver;
            ButtonCarrier.BorderColor = Color.Silver;

            ButtonReports.BackColor = Color.Silver;
            ButtonReports.BorderColor = Color.Silver;

            ButtonViwerReport.BackColor = Color.Silver;
            ButtonViwerReport.BorderColor = Color.Silver;
            Erros.Visible = false;


            MultiView.ActiveViewIndex = 0;


        }

        protected void ButtonRoutes_Click(object sender, EventArgs e)
        {
            ButtonRoutes.BackColor = Color.DeepSkyBlue;
            ButtonRoutes.BorderColor = Color.ForestGreen;

            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;

            ButtonCarrier.BackColor = Color.Silver;
            ButtonCarrier.BorderColor = Color.Silver;

            ButtonReports.BackColor = Color.Silver;
            ButtonReports.BorderColor = Color.Silver;

            ButtonViwerReport.BackColor = Color.Silver;
            ButtonViwerReport.BorderColor = Color.Silver;
            Erros.Visible = false;


            MultiView.ActiveViewIndex = 1;


        }

        protected void ButtonCarrier_Click(object sender, EventArgs e)
        {
            ButtonCarrier.BackColor = Color.DeepSkyBlue;
            ButtonCarrier.BorderColor = Color.ForestGreen;

            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;

            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;

            ButtonReports.BackColor = Color.Silver;
            ButtonReports.BorderColor = Color.Silver;

            ButtonViwerReport.BackColor = Color.Silver;
            ButtonViwerReport.BorderColor = Color.Silver;
            Erros.Visible = false;


            MultiView.ActiveViewIndex = 2;

        }

        protected void ButtonViwerReport_Click(object sender, EventArgs e)
        {
            ButtonViwerReport.BackColor = Color.DeepSkyBlue;
            ButtonViwerReport.BorderColor = Color.ForestGreen;

            ButtonCarrier.BackColor = Color.Silver;
            ButtonCarrier.BorderColor = Color.Silver;

            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;

            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;

            ButtonReports.BackColor = Color.Silver;
            ButtonReports.BorderColor = Color.Silver;
            Erros.Visible = false;

            MultiView.ActiveViewIndex = 4;

        }

        private void GraphicStyleNavBar()
        {
            // estivlos da navegação
            ButtonViwerReport.Enabled = true;
            MultiView.ActiveViewIndex = 4;
            ReportViewer1.Visible = false;
            Erros.Visible = false;

            // estilos 
            ButtonViwerReport.BackColor = Color.DeepSkyBlue;
            ButtonViwerReport.BorderColor = Color.ForestGreen;
            ButtonReports.BackColor = Color.Silver;
            ButtonReports.BorderColor = Color.Silver;
            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;
            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;
            ButtonCarrier.BackColor = Color.Silver;
            ButtonCarrier.BorderColor = Color.Silver;
        }

        // quando clica em  relatorio vai fazer todo o trambite de preparar o sql e jogar na sessão [REPORT/ VIEWER]
        protected void ButtonReports_Click(object sender, EventArgs e)
        {
            ButtonReports.BackColor = Color.DeepSkyBlue;
            ButtonReports.BorderColor = Color.ForestGreen;

            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;

            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;

            ButtonCarrier.BackColor = Color.Silver;
            ButtonCarrier.BorderColor = Color.Silver;

            ButtonViwerReport.BackColor = Color.Silver;
            ButtonViwerReport.BorderColor = Color.Silver;
            Erros.Visible = false;


            string Dadoyear = "";
            foreach (ListItem li in ListYear.Items)
            {
                if (li.Selected)
                {
                    Dadoyear = "'" + li.Value + "'";
                }
            }
            //=====================================================================================================================

            string DadosListPort = "";
            foreach (ListItem li in ListPort.Items)
            {
                if (li.Selected)
                {

                    DadosListPort += "'" + li.Value + "'" + ',';
                }
            }
            DadosListPort += "''";
            //=====================================================================================================================
            string DadosListDirecao = "";
            foreach (ListItem li in ListDirecao.Items)
            {
                if (li.Selected)
                {

                    DadosListDirecao += "'" + li.Value + "'" + ',';
                }
            }

            DadosListDirecao += "''";
            //=====================================================================================================================
            string DadosListContainer = "";
            foreach (ListItem li in ListContainer.Items)
            {
                if (li.Selected)
                {

                    DadosListContainer += "'" + li.Value + "'" + ',';
                }
            }
            DadosListContainer += "''";
            //=====================================================================================================================
            string DadosListCliente = "";
            foreach (ListItem li in ListCliente.Items)
            {
                if (li.Selected)
                {

                    DadosListCliente += "'" + li.Value + "'" + ',';
                }
            }
            DadosListCliente += "''";
            //=====================================================================================================================
            string DadosListCarregamento = "";
            foreach (ListItem li in ListCarregamento.Items)
            {
                if (li.Selected)
                {

                    DadosListCarregamento += "'" + li.Value + "'" + ',';
                }
            }
            DadosListCarregamento += "''";
            //=====================================================================================================================
            string DadosListRestricao = "";
            foreach (ListItem li in ListRestricao.Items)
            {
                if (li.Selected)
                {

                    DadosListRestricao += "'" + li.Value + "'" + ',';
                }
            }
            DadosListRestricao += "''";
            //===================================================================================================================== segunda aba
            string DadosListRotas = "";
            foreach (ListItem li in ListRotas.Items)
            {
                if (li.Selected)
                {

                    DadosListRotas += "'" + li.Value + "'" + ',';
                }
            }
            DadosListRotas += "''";
            //=====================================================================================================================
            string DadosListAreas = "";
            foreach (ListItem li in ListAreas.Items)
            {
                if (li.Selected)
                {

                    DadosListAreas += "'" + li.Value + "'" + ',';
                }
            }
            DadosListAreas += "''";
            //=====================================================================================================================
            string DadosListRegiao = "";
            foreach (ListItem li in ListRegiao.Items)
            {
                if (li.Selected)
                {
                    DadosListRegiao += "'" + li.Value + "'" + ',';
                }
            }
            DadosListRegiao += "''";
            //=====================================================================================================================
            string DadosListPais = "";
            foreach (ListItem li in ListPais.Items)
            {
                if (li.Selected)
                {
                    DadosListPais += "'" + li.Value + "'" + ',';
                }
            }
            DadosListPais += "''";
            //=====================================================================================================================
            string DadosListPortsPais = "";
            foreach (ListItem li in ListPortsPais.Items)
            {
                if (li.Selected)
                {

                    DadosListPortsPais += "'" + li.Value + "'" + ',';
                }
            }
            DadosListPortsPais += "''";
            //===================================================================================================================== terceira aba
            string DadosListTranpostadora = "";
            foreach (ListItem li in ListTranpostadora.Items)
            {
                if (li.Selected)
                {

                    DadosListTranpostadora += "'" + li.Value + "'" + ',';
                }
            }
            DadosListTranpostadora += "''";
            //=====================================================================================================================
            string DadosListMercadoria = "";
            foreach (ListItem li in ListMercadoria.Items)
            {
                if (li.Selected)
                {

                    DadosListMercadoria += "'" + li.Value + "'" + ',';
                }
            }
            DadosListMercadoria += "''";
            //=====================================================================================================================
            string DadosListVendedor = "";
            foreach (ListItem li in ListVendedor.Items)
            {
                if (li.Selected)
                {

                    DadosListVendedor += "'" + li.Value + "'" + ',';
                }
            }
            DadosListVendedor += "''";
            //=====================================================================================================================


            string DadosListClientes = "";
            foreach (ListItem li in ListClientes.Items)
            {
                if (li.Selected)
                {
                    DadosListClientes += "'" + li.Value + "'" + ',';
                }
            }
            DadosListClientes += "''";

            // setandos parametros 
            //  Primeira aba 
            Session["ParameterYear"] = Dadoyear; // 2012
            Session["ParameterStartMonth"] = StartMonth.SelectedValue; //mes inicial 
            Session["ParameterFinalMonth"] = FinalMonth.SelectedValue; // mes final 
            Session["ParameterListPort"] = DadosListPort; // portos 
            Session["ParameterListDirecao"] = DadosListDirecao; // imp exp 
            Session["ParameterListCliente"] = DadosListCliente; //  Direct
            Session["ParameterListContainer"] = DadosListContainer; // R
            Session["ParameterListCarregamento"] = DadosListCarregamento; // FCL
            Session["ParameterListRestricao"] = DadosListRestricao;

            // Segunda aba 

            Session["ParameterListRotas"] = DadosListRotas; // EAF
            Session["ParameterListAreas"] = DadosListAreas; // EAF 
            Session["ParameterListRegiao"] = DadosListRegiao; // EAF 
            Session["ParameterListPais"] = DadosListPais;
            Session["ParameterListPortsPais"] = DadosListPortsPais;

            // Terceira aba 

            Session["ParameterListTranpostadora"] = DadosListTranpostadora;
            Session["ParameterListCommodity"] = DadosListMercadoria;
            Session["ParameterListVendedor"] = DadosListVendedor;
            Session["ParameterListClients"] = DadosListClientes;



            // prenchendo propriedades staticas para  incovar o metodo jason para usar nos graficos
            parametros.year = Dadoyear;
            parametros.StartMonth = StartMonth.SelectedValue;
            parametros.FinalMonth = FinalMonth.SelectedValue;
            parametros.listport = DadosListPort;
            parametros.Direction = DadosListDirecao;
            parametros.listClient = DadosListCliente;
            parametros.ListContainer = DadosListContainer;
            parametros.ListCarregamento = DadosListCarregamento;
            parametros.ListRestricoes = DadosListRestricao;

            parametros.ListRotas = DadosListRotas;
            parametros.ListAreas = DadosListAreas;
            parametros.ListRegion = DadosListRegiao;
            parametros.ListPais = DadosListPais;
            parametros.ListPortsPais = DadosListPortsPais;

            parametros.ListCarrier = DadosListTranpostadora;
            parametros.ListCommodity = DadosListMercadoria;
            parametros.ListSalesRep = DadosListVendedor;
            parametros.ListClients = DadosListClientes;

            MultiView.ActiveViewIndex = 3;
        }


        #endregion

        #region Relatorios no Reportview
        // Relatorios que vão aparecer na view com opção de transformar em pdf ou apenas vizualizar
        protected void TotalMovementsByPortButton_Click(object sender, EventArgs e)
        {

            string Sql = " SELECT        fk_direction, fk_cliente, fk_srep, fk_tipo, year, fk_service, lcode, cod, SUM(teu) AS teu, " +
                              " (CASE WHEN fk_carrier = 'EVERGREEN' THEN SUM(teu) WHEN fk_carrier = 'LLOYD TRI' THEN SUM(teu) WHEN fk_carrier = 'HATSU MARINE' THEN SUM(teu) WHEN " +
                              "  fk_carrier = 'ITALIA MARITTIMA' THEN SUM(teu) END) AS grieg, " +
                              " (CASE WHEN fk_service = 'ESA' THEN 1 WHEN fk_service = 'SNA' THEN 2 WHEN fk_service = 'EUSA' THEN 3 WHEN fk_service = 'MED' THEN 4 WHEN " +
                              " fk_service = 'SA' THEN 5 WHEN fk_service = 'EAF' THEN 6 WHEN fk_service " +
                              " = 'WAF' THEN 7 WHEN fk_service = 'UNK' THEN 8 END) AS fk_service_NUM " +
                              " FROM vw_outros_resumo ";


            string GrupSql = " GROUP BY fk_direction, fk_cliente, fk_srep, fk_tipo, year, fk_service, lcode, cod, fk_carrier , fk_service_NUM ";

            BuildReport(Sql, GrupSql, "TotalMovementsByPort");

        }
        protected void SumarryServiceButton_Click(object sender, EventArgs e)
        {
            string Sql = "SELECT fk_service, dservice, cod, fk_direction, fk_carrier, year, SUM(teu)AS TOTAL, dmonth, fk_month " +
                         " FROM vw_outros_resumo ";


            string GrupSql = "GROUP BY year , fk_service, dservice, cod, fk_direction, fk_carrier,  dmonth, fk_month ORDER BY total desc ";

            BuildReport(Sql, GrupSql, "SummaryService");

        }

        protected void SummaryAreaButton_Click(object sender, EventArgs e)
        {

            string Sql = "SELECT fk_service , dservice, fk_area, darea, lcode, fk_lporto, cod, fk_direction, fk_carrier, year, Sum(teu) AS Total , dmonth , fk_month " +
                          " FROM vw_outros_resumo ";


            string GrupSql = "GROUP BY fk_service, dservice, fk_area , darea, lcode, fk_lporto, cod, fk_direction, fk_carrier, dmonth, fk_month ,year";

            BuildReport(Sql, GrupSql, "SummaryArea");


        }

        protected void SummaryRakingButton_Click(object sender, EventArgs e)
        {
            string Sql = "SELECT lcode, fk_lporto, fk_service, dservice, fk_area, darea, cod, fk_direction, year, SUM(teu) AS total, code , " +
                            " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4  " +
                            " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8  " +
                            " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10 WHEN code = 'MOL' THEN 11 WHEN code = 'KLINE' THEN 12  " +
                            " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16  " +
                            " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM  " +
                            " FROM  vw_ranking  ";


            string GrupSql = "GROUP BY  lcode, fk_lporto, fk_service, dservice, fk_area, darea, cod, fk_direction, year, code  ORDER BY COD_NUM ";

            BuildReport(Sql, GrupSql, "SummaryRanking");

        }

        protected void StatsPerContainerAreaButton_Click(object sender, EventArgs e)
        {
            string Sql = "  SELECT  year,  lcode,  fk_lporto,  cod,   fk_direction,  fk_service,  dservice,  fk_area ,  " +
                         "  darea, Sum(c20) AS c20, Sum(c40) AS c40, Sum(teu) AS Total , Sum(wtmt) AS wtmt    " +
                         " FROM  vw_container  ";

            string GrupSql = " GROUP BY  year,  lcode,  fk_lporto,  cod,  fk_direction,  fk_service,  dservice,  fk_area, darea";

            BuildReport(Sql, GrupSql, "StatsPerContainerArea");

        }

        protected void StatsPerContainerCarrierButton_Click(object sender, EventArgs e)
        {
            string Sql = "SELECT  year,  lcode,  fk_lporto,  cod,  fk_direction,  fk_service,  dservice,  fk_carrier, " +
                          " Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS total, Sum(wtmt) AS wtmt   " +
                          " FROM  vw_container  ";


            string GrupSql = " GROUP BY  year,  lcode,  fk_lporto,  cod,  fk_direction,  fk_service,  dservice,  fk_carrier";

            BuildReport(Sql, GrupSql, "StatsPerContainerCarrier");

        }

        protected void RankingServiceButton_Click(object sender, EventArgs e)
        {
            string Sql = "SELECT  fk_service,  dservice,  fk_direction,  fk_cliente,  fk_srep,  year, Sum(teu) AS teu , code , " +
                     " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4  " +
                     " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8  " +
                     " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12  " +
                     " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16  " +
                     " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM  " +
                     " FROM  vw_ranking  ";


            string GrupSql = " GROUP BY  fk_service,  dservice, fk_direction,  fk_cliente,  fk_srep,  year, code  ORDER BY COD_NUM ";

            BuildReport(Sql, GrupSql, "RankingService");

        }

        protected void RankingServiceByAreaButton_Click(object sender, EventArgs e)
        {

            string Sql = " SELECT lcode,  fk_lporto,  fk_service,  dservice,  fk_area,  darea,  fk_direction,  fk_cliente,  fk_srep,  year, Sum(teu) AS teu , code , " +
                    " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4  " +
                    " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8  " +
                    " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12  " +
                    " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16  " +
                    " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM  " +
                    " FROM  vw_ranking  ";


            string GrupSql = "GROUP BY  lcode,  fk_lporto,  fk_service,  dservice,  fk_area,  darea,  fk_direction , fk_cliente,  fk_srep, code , year ORDER BY COD_NUM ";

            BuildReport(Sql, GrupSql, "RankingServiceByArea");

        }

        protected void SummaryBrazilButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT  fk_service,  dservice,  fk_area,  darea,  fk_direction,  fk_carrier,  year, Sum(teu) AS teu , lcode , " +
                      " (CASE WHEN lcode = 'STO' THEN 1 WHEN lcode = 'RGR' THEN 2 WHEN lcode = 'ITJ' THEN 3 WHEN lcode = 'NVT' THEN 4 " +
                        " WHEN lcode = 'IOA' THEN 5 WHEN lcode = 'PNP' THEN 6 WHEN lcode = 'RIO' THEN 7 WHEN lcode = 'SEP' THEN 8 " +
                        " WHEN lcode = 'PCM' THEN 9 WHEN lcode = 'SQI' THEN 10  WHEN lcode = 'VCD' THEN 11  WHEN lcode = 'VTO' THEN 12 " +
                        " WHEN lcode = 'MSC' THEN 13 WHEN lcode = 'MSK' THEN 14 WHEN lcode = 'NYK' THEN 15 WHEN lcode = 'PIL' THEN 16 " +
                        " WHEN lcode = 'SFS' THEN 17 WHEN lcode = 'SVD' THEN 18 WHEN lcode = 'MAO' THEN 19 WHEN lcode = 'IMB' THEN 20 WHEN lcode = 'OTH' THEN 21 END) AS COD_NUM " +
                        " FROM vw_brasil_resumo ";

            string GrupSql = " GROUP BY fk_service, dservice, fk_area, darea, fk_direction, fk_carrier, year, lcode  ORDER BY COD_NUM ";

            BuildReport(Sql, GrupSql, "SummaryBrazil");

        }

        protected void SummaryClientButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT lcode , fk_lporto ,fk_service, dservice, fk_area, darea, fk_direction, fk_carrier, year, " +
                         " SUM(teu) AS teu, dmonth, fk_month, fk_cliente, fk_srep " +
                         " FROM vw_brasil_resumo ";

            string GrupSql = " GROUP BY lcode , fk_lporto ,fk_service, dservice, fk_area, darea, fk_direction, fk_carrier, year, dmonth, fk_month, fk_cliente, fk_srep ";

            BuildReport(Sql, GrupSql, "SummaryClient");

        }

        protected void RankingCommodityButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_service, dservice, fk_direction, commodity, year, Sum(teu) AS teu , code , " +
                     " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4  " +
                     " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8  " +
                     " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12  " +
                     " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16  " +
                     " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM  " +
                     " FROM  vw_ranking  ";

            string GrupSql = " GROUP BY fk_service, dservice, fk_direction, commodity, year , code order by  COD_NUM ";

            BuildReport(Sql, GrupSql, "RankingCommodity");
        }

        protected void SummaryPerDischargePortButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT lcode, fk_lporto, fk_area, darea, fk_service, dservice, fk_direction, " +
                               " fk_odporto, year, Sum(teu) AS Total , dmonth ,fk_month" +
                                " FROM vw_brasil_resumo ";

            string GrupSql = "  GROUP BY lcode, fk_lporto, fk_area, darea, fk_service, dservice, fk_direction, fk_odporto, year , dmonth ,fk_month ";

            BuildReport(Sql, GrupSql, "SummaryPerDischargePort");

        }

        protected void SummaryCarrierButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT lcode, fk_lporto, fk_area, darea, fk_service, dservice, fk_direction, fk_cliente, fk_srep, dmonth ,fk_month , " +
                       " fk_carrier, year, Sum(teu) AS Total " +
                       " FROM vw_brasil_resumo  ";

            string GrupSql = " GROUP BY lcode, fk_lporto, fk_area, darea, fk_service, dservice, fk_direction, fk_cliente, fk_srep, fk_carrier, year , dmonth ,fk_month";

            BuildReport(Sql, GrupSql, "SummaryCarrier");
        }

        protected void DetailedPerContainerButton_Click(object sender, EventArgs e)
        {
            string Sql = "  SELECT fk_lporto, lcode, fk_service, dservice, fk_area, darea, fk_odporto, fk_cliente, fk_srep, fk_tcontainer, dcontainer, fk_direction, " +
                     "  cod, year, Sum(teu) AS teu, Sum(c40)AS Totalc40, Sum(c20) AS Totalc20 , code , " +
                    " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'HSUD' THEN 3 WHEN code = 'MSK' THEN 4 " +
                   " WHEN code = 'SAF' THEN 5 WHEN code = 'MSC' THEN 6 WHEN code = 'COSC' THEN 7 WHEN code = 'ZIM' THEN 8 " +
                   " WHEN code = 'CGM' THEN 9 WHEN code = 'LIB' THEN 10  WHEN code = 'CASAV' THEN 11  WHEN code = 'KLINE' THEN 12 " +
                   " WHEN code = 'MOL' THEN 13 WHEN code = 'HPG' THEN 14 WHEN code = 'PIL' THEN 15 WHEN code = 'NYK' THEN 16 " +
                   " WHEN code = 'HJS' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM " +
                   " FROM  vw_ranking_soma  ";

            string GrupSql = " GROUP BY fk_lporto, code , COD_NUM , lcode, fk_service, dservice, fk_area, darea, fk_odporto, fk_cliente, fk_srep, fk_tcontainer, dcontainer, fk_direction, cod, year ";

            BuildReport(Sql, GrupSql, "DetailedPerContainer");
        }

        protected void DetailedPerClientButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, lcode, fk_service, dservice, fk_area, darea, fk_odporto, fk_cliente, fk_srep, fk_tcontainer, " +
                       "  dcontainer, fk_direction, cod, year, Sum(teu) AS teu, Sum(c40)AS totalc40, Sum(c20) AS totalC20, code," +
                       "  (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4 " +
                       "  WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8 " +
                       "  WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10 WHEN code = 'MOL' THEN 11 WHEN code = 'KLINE' THEN 12 " +
                       "  WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'PIL' THEN 15 WHEN code = 'NYK' THEN 16 " +
                       "  WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM " +
                       " FROM vw_ranking_soma ";

            string GrupSql = "  GROUP BY fk_lporto, lcode, fk_service, dservice, fk_area, darea, fk_odporto, fk_cliente, fk_srep, fk_tcontainer, " +
                         " dcontainer, fk_direction, cod, year , code , cod_num ";

            BuildReport(Sql, GrupSql, "DetailedPerClient");
        }

        protected void TypeContainerForeignPortButton_Click(object sender, EventArgs e)
        {

            string Sql = "	SELECT year, lcode, fk_lporto, cod, fk_direction, fk_service, dservice, fk_odporto, fk_tcontainer, dcontainer , " +
                       " Sum(c20) AS c20, Sum(c40) AS c40, Sum(teu) AS Total, Sum(wtmt) AS wtmt " +
                       " FROM vw_tipo_container ";

            string GrupSql = " GROUP BY year, lcode, fk_lporto, cod, fk_direction, fk_service, dservice, fk_odporto, fk_tcontainer, dcontainer ";

            BuildReport(Sql, GrupSql, "TypeContainerForeignPort");

        }

        protected void CarrierClientButton_Click(object sender, EventArgs e)
        {
            string Sql = "	SELECT fk_lporto, fk_cliente, darea, fk_carrier, fk_odporto, fk_direction, year, Sum(c20) AS c20," +
                                " Sum(c40) AS c40, Sum(teu)AS teu, Sum(wtmt) AS wtmt " +
                                " FROM vw_especifico_carrier_area ";

            string GrupSql = "GROUP BY fk_lporto, fk_cliente, darea, fk_carrier, fk_odporto, fk_direction, year";

            BuildReport(Sql, GrupSql, "CarrierClient");

        }

        protected void CarrierCommodityButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, commodity, darea, fk_carrier, fk_odporto, fk_direction, year, Sum(c20) AS c20, " +
                         " Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                         " FROM  vw_especifico_carrier_area ";

            string GrupSql = "GROUP BY fk_lporto, commodity, darea, fk_carrier, fk_odporto, fk_direction, year ";

            BuildReport(Sql, GrupSql, "CarrierCommodity");

        }

        protected void CarrierVasselButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_srep, fk_odporto, dservice, darea, year, fk_direction, fk_carrier, vessel, " +
                       " fk_month, dmonth, Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                       " FROM  vw_especifico_vessel ";

            string GrupSql = " GROUP BY fk_lporto, fk_cliente, fk_srep, fk_odporto, dservice, darea, year, fk_direction, fk_carrier, vessel, fk_month, dmonth ";

            BuildReport(Sql, GrupSql, "CarrierVassel");

        }

        protected void CommodityAreaButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, darea, fk_carrier, commodity, cod, fk_direction, year, Sum(c20)AS c20, " +
                       " Sum(c40)AS c40, Sum(teu) AS teu " +
                       " FROM vw_especifico_cliente_area ";

            string GrupSql = " GROUP BY fk_lporto, fk_cliente, darea, fk_carrier, commodity, cod, fk_direction, year;  ";

            BuildReport(Sql, GrupSql, "CommodityArea");

        }

        protected void ForeignPortClientButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_odporto,fport, dservice, darea, year, fk_direction, " +
                      " Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                      " FROM vw_especifico_vessel ";

            string GrupSql = " GROUP BY fk_lporto, fk_cliente, fk_odporto,fport, dservice, darea, year, fk_direction;";

            BuildReport(Sql, GrupSql, "ForeignPortClient");

        }

        protected void ForeignPortCarrierButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_carrier, fk_odporto, fport, dservice, darea, year, fk_direction, " +
                      " Sum(c20) AS c20, Sum(c40) AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                      " FROM vw_especifico_vessel ";

            string GrupSql = " GROUP BY fk_lporto, fk_carrier, fk_odporto, fport, dservice, darea, year, fk_direction; ";

            BuildReport(Sql, GrupSql, "ForeignPortCarrier");
        }

        protected void ShipperCneeByPortPairsButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT year, fk_lporto, shcnee, fk_cliente, dregion, fk_odporto, fk_direction, Sum(teu) AS Total , code , " +
                                " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ZIM' THEN 2 WHEN code = 'ALI' THEN 3 WHEN code = 'HJS' THEN 4 " +
                                " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HSUD' THEN 8 " +
                                " WHEN code = 'LIB' THEN 9 WHEN code = 'HPG' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12 " +
                                " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16 " +
                                " WHEN code = 'SAF' THEN 17 WHEN code = 'HPG' THEN 18 WHEN code = 'CHS' THEN 19 WHEN code = 'OTHS' THEN 20 END) " +
                                " AS COD_NUM " +
                             " FROM vw_especifico_shipper_cnee ";

            string GrupSql = " GROUP BY year, fk_lporto, shcnee, fk_cliente, dregion, fk_odporto, fk_direction , code , COD_NUM ";

            BuildReport(Sql, GrupSql, "ShipperCneeByPortPairs");
        }

        protected void CneeShipperByPortPairButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT year , fk_lporto, shcnee, fk_cliente, dregion, fk_odporto, fk_direction, Sum(teu) AS Total  , code , " +
                      " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ZIM' THEN 2 WHEN code = 'ALI' THEN 3 WHEN code = 'HJS' THEN 4 " +
                      " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HSUD' THEN 8 " +
                      " WHEN code = 'LIB' THEN 9 WHEN code = 'HPG' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12 " +
                      " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16 " +
                      " WHEN code = 'SAF' THEN 17 WHEN code = 'HPG' THEN 18 WHEN code = 'CHS' THEN 19 WHEN code = 'OTHS' THEN 20 END) " +
                      " AS COD_NUM " +
                     " FROM vw_especifico_shipper_cnee ";

            string GrupSql = " GROUP BY year, fk_lporto, shcnee, fk_cliente, dregion, fk_odporto, fk_direction , code , COD_NUM  ";

            BuildReport(Sql, GrupSql, "CneeShipperByPortPair");

        }

        protected void ClientCneeButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_srep, darea, fk_carrier, fk_odporto, cod, fk_direction, " +
                        " year, shcnee, Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                        " FROM vw_especifico_cnee ";

            string GrupSql = " GROUP BY fk_lporto, fk_cliente, fk_srep, darea, fk_carrier, fk_odporto, cod, fk_direction, year, shcnee ";

            BuildReport(Sql, GrupSql, "ClientCnee");

        }

        protected void CneeClientButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_srep, darea, fk_carrier, fk_odporto, cod, " +
                       " fk_direction, year, shcnee, Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                       " FROM vw_especifico_cnee ";

            string GrupSql = "GROUP BY fk_lporto, fk_cliente, fk_srep, darea, fk_carrier, fk_odporto, cod, fk_direction, year, shcnee";

            BuildReport(Sql, GrupSql, "CneeClient");

        }

        protected void ClientAreaButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_srep, darea, dregion, fk_carrier, commodity, cod, fk_direction, year, " +
                              " Sum(c20) AS c20, Sum(c40) AS c40, Sum(teu) AS teu " +
                              " FROM vw_especifico_cliente_area ";

            string GrupSql = "GROUP BY  fk_lporto, fk_cliente, fk_srep, darea, dregion, fk_carrier, commodity, cod, fk_direction, year";

            BuildReport(Sql, GrupSql, "ClientArea");

        }

        protected void ClientSrepButton_Click(object sender, EventArgs e)
        {
            string Sql = "SELECT fk_lporto, lcode, fk_service, dservice, fk_cliente, fk_srep, fk_direction, year, Sum(c20)AS c20, Sum(c40)AS c40, Sum(teu) AS teu, " +
                         " (CASE WHEN fk_carrier = 'EVERGREEN' THEN sum(teu) WHEN fk_carrier = 'HATSU MARINE' THEN sum(teu) WHEN fk_carrier = 'ITALIA MARITTIMA' THEN sum(teu) END) " +
                         " as grieg FROM vw_ranking_soma ";


            string GrupSql = "GROUP BY fk_lporto, lcode, fk_service, dservice, fk_cliente, fk_srep, fk_direction, year ,fk_carrier ";

            BuildReport(Sql, GrupSql, "ClientSrep");

        }


        protected void FollowUpReportButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT a.fk_service, a.dservice, a.fk_direction, a.fk_cliente, a.fk_srep, a.year, Sum(a.teu) AS teu, to_char(b.data, 'MONTH/YYYY')|| ' ' || b.description as remarks,a.code , " +
              " (CASE WHEN code = 'EMC' THEN SUM(teu) END) AS EMC " +
              " FROM vw_ranking a " +
              " LEFT OUTER JOIN tbl_contato b " +
              " ON a.fk_cliente = b.fk_cliente AND a.year = b.year ";


            string GrupSql = " GROUP BY a.fk_service, a.dservice, a.fk_direction, a.fk_cliente, a.fk_srep, a.year,a.code,a.fk_carrier,a.teu,remarks ";


            BuildReport(Sql, GrupSql, "FollowUpReport");

        }

        protected void SummaryPortsButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_service, dservice, fk_area, darea, fk_odporto ,cod, fk_direction, year, Sum(teu) AS teu ,code , " +
                          " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4 " +
                          " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8 " +
                          " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10 WHEN code = 'MOL' THEN 11 WHEN code = 'KLINE' THEN 12 " +
                          " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16 " +
                          " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM " +
                          " FROM vw_ranking ";

            string GrupSql = " GROUP BY  fk_service, dservice, fk_area, darea, fk_odporto ,cod, fk_direction, year ,cod_num , code  , cod_num   ";

            BuildReport(Sql, GrupSql, "SummaryPorts");
        }

        protected void GraphicButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_carrier As Armador, fk_lporto As Porto, fk_direction As Sentido, fk_service As Service, Sum(teu) AS teus  FROM vw_ranking ";

            string GrupSql = " GROUP BY  fk_service, dservice, fk_area, darea, fk_odporto ,cod, fk_direction, year ,cod_num , code  , cod_num   ";

            BuildReport(Sql, GrupSql, "Graphic");

        }


        protected void SummaryCountriesButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT fk_service, dservice, fk_area, darea, fk_country ,cod, fk_direction, year, Sum(teu) AS teu , code , " +
                        " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4 " +
                        " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8 " +
                       " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10 WHEN code = 'MOL' THEN 11 WHEN code = 'KLINE' THEN 12 " +
                       " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16 " +
                       " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM " +
                       " FROM vw_ranking  ";

            string GrupSql = "GROUP BY fk_service, dservice, fk_area, darea, fk_country ,cod, fk_direction, year , code  , cod_num   ";

            BuildReport(Sql, GrupSql, "SummaryCountries");
        }

        protected void SummaryClientMonthButton_Click(object sender, EventArgs e)
        {

            string Sql = " SELECT lcode, fk_lporto, fk_service, fk_direction, fk_cliente, fk_srep, year, Sum(teu) AS Total  , dmonth, fk_month" +
                         " FROM vw_brasil_resumo";

            string GrupSql = "GROUP BY lcode, fk_lporto, fk_service, fk_direction, fk_cliente, fk_srep, year , dmonth, fk_month ";

            BuildReport(Sql, GrupSql, "SummaryClientMonth");

        }


        public void BuildReport(string Sql, string GrupSql, string NameReport)
        {
            ButtonViwerReport.Enabled = true;
            MultiView.ActiveViewIndex = 4;
            ReportViewer1.Visible = true;
            Erros.Visible = false;

            // estilos 
            ButtonViwerReport.BackColor = Color.DeepSkyBlue;
            ButtonViwerReport.BorderColor = Color.ForestGreen;
            ButtonReports.BackColor = Color.Silver;
            ButtonReports.BorderColor = Color.Silver;
            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;
            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;
            ButtonCarrier.BackColor = Color.Silver;
            ButtonCarrier.BorderColor = Color.Silver;

            // aba 1 
            var year = Session["ParameterYear"];
            var StartMonth = Session["ParameterStartMonth"];
            var FinalMonth = Session["ParameterFinalMonth"];
            var listport = Session["ParameterListPort"];
            var Direction = Session["ParameterListDirecao"];
            var listClient = Session["ParameterListCliente"];
            var ListCarregamento = Session["ParameterListCarregamento"];
            var ListContainer = Session["ParameterListContainer"];
            var ListRestricoes = Session["ParameterListRestricao"];

            //aba 2
            var ListRotas = Session["ParameterListRotas"];
            var ListAreas = Session["ParameterListAreas"];
            var ListRegion = Session["ParameterListRegiao"];
            var ListPais = Session["ParameterListPais"];
            var ListPortsPais = Session["ParameterListPortsPais"];

            //  aba 3
            var ListCarrier = Session["ParameterListTranpostadora"];
            var ListCommodity = Session["ParameterListCommodity"];
            var ListSalesRep = Session["ParameterListVendedor"];
            var ListClients = Session["ParameterListClients"];


            try
            {
                if (NameReport == "FollowUpReport")
                {
                    Sql += utl.MarketStatiscsParametersQuery(year.ToString(), StartMonth.ToString(), FinalMonth.ToString(), listport.ToString(), Direction.ToString(), listClient.ToString(), ListCarregamento.ToString(),
                            ListContainer.ToString(), ListRotas.ToString(), ListAreas.ToString(), ListRegion.ToString(), ListPais.ToString(), ListPortsPais.ToString(),
                            ListCarrier.ToString(), ListCommodity.ToString(), ListClients.ToString(), ListSalesRep.ToString(), ListRestricoes.ToString(), NameReport) + GrupSql;
                }
                else
                {
                    Sql += utl.MarketStatiscsParametersQuery(year.ToString(), StartMonth.ToString(), FinalMonth.ToString(), listport.ToString(), Direction.ToString(), listClient.ToString(), ListCarregamento.ToString(),
                                   ListContainer.ToString(), ListRotas.ToString(), ListAreas.ToString(), ListRegion.ToString(), ListPais.ToString(), ListPortsPais.ToString(),
                                   ListCarrier.ToString(), ListCommodity.ToString(), ListClients.ToString(), ListSalesRep.ToString(), ListRestricoes.ToString()) + GrupSql;
                }

                ReportParameterCollection parameter = new ReportParameterCollection();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportViewer/MarketStatistics/" + NameReport + ".rdlc");

                parameter.Add(new ReportParameter("RStartMonth", StartMonth.ToString()));
                parameter.Add(new ReportParameter("RFinalMonth", FinalMonth.ToString()));

                parameter.Add(new ReportParameter("RPorts", Session["ParameterListPort"].ToString()));
                parameter.Add(new ReportParameter("RService", Session["ParameterListRotas"].ToString()));
                parameter.Add(new ReportParameter("RArea", Session["ParameterListAreas"].ToString()));
                parameter.Add(new ReportParameter("RRegion", Session["ParameterListRegiao"].ToString()));
                parameter.Add(new ReportParameter("Ryear", Session["ParameterYear"].ToString()));
                parameter.Add(new ReportParameter("Rloading", Session["ParameterListCliente"].ToString()));
                parameter.Add(new ReportParameter("RType", Session["ParameterListCarregamento"].ToString()));
                parameter.Add(new ReportParameter("RContainer", Session["ParameterListContainer"].ToString()));

                parameter.Add(new ReportParameter("RCarrier", Session["ParameterListTranpostadora"].ToString()));
                parameter.Add(new ReportParameter("RCountry", Session["ParameterListPais"].ToString()));
                parameter.Add(new ReportParameter("RPod", Session["ParameterListPortsPais"].ToString()));
                parameter.Add(new ReportParameter("RCommodity", Session["ParameterListCommodity"].ToString().Trim()));
                parameter.Add(new ReportParameter("RRest", Session["ParameterListRestricao"].ToString()));
                parameter.Add(new ReportParameter("RSrep", Session["ParameterListVendedor"].ToString()));
                parameter.Add(new ReportParameter("RClient", Session["ParameterListClients"].ToString()));
                ReportViewer1.LocalReport.SetParameters(parameter);


                DataTable DADOS = ReportsDal.BuscaRelatorio(Sql, cs);

                if (DADOS.Rows.Count > 1)
                {
                    utl.addLog(Request.LogonUserIdentity.Name.ToString(), NameReport, Sql, MapPath("~/Ti/LogMovi.xml"));
                    ReportViewer1.Visible = true;
                    ReportDataSource rds = new ReportDataSource(NameReport, DADOS);
                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(rds);
                    this.ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    Erros.Visible = true;
                    ReportViewer1.Visible = false;
                    Erros.InnerHtml = utl.MessageError();
                }
            }
            catch (Exception ex)
            {
                ReportViewer1.Visible = false;
                Erros.Visible = true;
                Erros.InnerHtml = utl.MessageError();
                utl.addLog(Request.LogonUserIdentity.Name.ToString(), NameReport, "", ex.ToString(), MapPath("LOG.xml"));
            }

        }

        #endregion

        #region Relatorios em EXCEL


        private void BuildExcelToReport(string Sql, string GrupSql, string NameReport)
        {


            ButtonViwerReport.Enabled = true;
            MultiView.ActiveViewIndex = 4;
            ReportViewer1.Visible = true;
            Erros.Visible = false;

            // estilos 
            ButtonViwerReport.BackColor = Color.DeepSkyBlue;
            ButtonViwerReport.BorderColor = Color.ForestGreen;
            ButtonReports.BackColor = Color.Silver;
            ButtonReports.BorderColor = Color.Silver;
            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;
            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;
            ButtonCarrier.BackColor = Color.Silver;
            ButtonCarrier.BorderColor = Color.Silver;

            // aba 1 
            var year = Session["ParameterYear"];
            var StartMonth = Session["ParameterStartMonth"];
            var FinalMonth = Session["ParameterFinalMonth"];
            var listport = Session["ParameterListPort"];
            var Direction = Session["ParameterListDirecao"];
            var listClient = Session["ParameterListCliente"];
            var ListCarregamento = Session["ParameterListCarregamento"];
            var ListContainer = Session["ParameterListContainer"];
            var ListRestricoes = Session["ParameterListRestricao"];

            //aba 2
            var ListRotas = Session["ParameterListRotas"];
            var ListAreas = Session["ParameterListAreas"];
            var ListRegion = Session["ParameterListRegiao"];
            var ListPais = Session["ParameterListPais"];
            var ListPortsPais = Session["ParameterListPortsPais"];

            //  aba 3
            var ListCarrier = Session["ParameterListTranpostadora"];
            var ListCommodity = Session["ParameterListCommodity"];
            var ListSalesRep = Session["ParameterListVendedor"];
            var ListClients = Session["ParameterListClients"];


            try
            {
                if (NameReport == "FollowUpReport")
                {
                    Sql += utl.MarketStatiscsParametersQuery(year.ToString(), StartMonth.ToString(), FinalMonth.ToString(), listport.ToString(), Direction.ToString(), listClient.ToString(), ListCarregamento.ToString(),
                            ListContainer.ToString(), ListRotas.ToString(), ListAreas.ToString(), ListRegion.ToString(), ListPais.ToString(), ListPortsPais.ToString(),
                            ListCarrier.ToString(), ListCommodity.ToString(), ListClients.ToString(), ListSalesRep.ToString(), ListRestricoes.ToString(), NameReport) + GrupSql;
                }
                else
                {
                    Sql += utl.MarketStatiscsParametersQuery(year.ToString(), StartMonth.ToString(), FinalMonth.ToString(), listport.ToString(), Direction.ToString(), listClient.ToString(), ListCarregamento.ToString(),
                                   ListContainer.ToString(), ListRotas.ToString(), ListAreas.ToString(), ListRegion.ToString(), ListPais.ToString(), ListPortsPais.ToString(),
                                   ListCarrier.ToString(), ListCommodity.ToString(), ListClients.ToString(), ListSalesRep.ToString(), ListRestricoes.ToString()) + GrupSql;
                }

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportViewer/MarketStatistics/Excel/" + NameReport + ".rdlc");

                DataTable DADOS = ReportsDal.BuscaRelatorio(Sql, cs);

                if (DADOS.Rows.Count > 1)
                {
                    // força download sem vizualização 
                    ReportViewer1.Visible = true;
                    ReportDataSource datasource = new ReportDataSource(NameReport, DADOS);

                    Warning[] warnings;
                    string[] streams;
                    string MIMETYPE = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    ReportViewer rptviewer = new ReportViewer();
                    rptviewer.ProcessingMode = ProcessingMode.Local; // preocessa localmente  
                    rptviewer.LocalReport.ReportPath = Server.MapPath("ReportViewer/MarketStatistics/Excel/" + NameReport + ".rdlc");
                    rptviewer.LocalReport.DataSources.Add(datasource);
                    byte[] bytes = rptviewer.LocalReport.Render("Excel", null, out MIMETYPE, out encoding, out extension, out streams, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = MIMETYPE;
                    Response.AddHeader("content-disposition", "attachment; filename=" + NameReport + "." + extension);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    utl.addLog(Request.LogonUserIdentity.Name.ToString(), NameReport + "Excel", Sql, MapPath("~/Ti/LogMovi.xml"));

                }
                else
                {
                    Erros.Visible = true;
                    ReportViewer1.Visible = false;
                    Erros.InnerHtml = utl.MessageError();
                }
            }
            catch (Exception ex)
            {
                ReportViewer1.Visible = false;
                Erros.Visible = true;
                Erros.InnerHtml = utl.MessageError();
                utl.addLog(Request.LogonUserIdentity.Name.ToString(), NameReport + "Excel", "", ex.ToString(), MapPath("LOG.xml"));
            }
        }


        protected void SumarryServiceExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "SELECT fk_service, dservice, cod, fk_direction, fk_carrier, year, SUM(teu)AS TOTAL, dmonth, fk_month " +
                      " FROM vw_outros_resumo ";


            string GrupSql = "GROUP BY year , fk_service, dservice, cod, fk_direction, fk_carrier,  dmonth, fk_month ORDER BY total desc ";

            BuildExcelToReport(Sql, GrupSql, "SummaryService");

        }

        protected void SummaryAreaExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "SELECT fk_service , dservice, fk_area, darea, lcode, fk_lporto, cod, fk_direction, fk_carrier, year, Sum(teu) AS Total , dmonth , fk_month " +
                        " FROM vw_outros_resumo ";


            string GrupSql = "GROUP BY fk_service, dservice, fk_area , darea, lcode, fk_lporto, cod, fk_direction, fk_carrier, dmonth, fk_month ,year";

            BuildExcelToReport(Sql, GrupSql, "SummaryArea");

        }

        protected void SummaryRakingExecelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "SELECT lcode, fk_lporto, fk_service, dservice, fk_area, darea, cod, fk_direction, year, SUM(teu) AS total, code , " +
                               " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4  " +
                               " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8  " +
                               " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10 WHEN code = 'MOL' THEN 11 WHEN code = 'KLINE' THEN 12  " +
                               " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16  " +
                               " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM  " +
                               " FROM  vw_ranking  ";


            string GrupSql = "GROUP BY  lcode, fk_lporto, fk_service, dservice, fk_area, darea, cod, fk_direction, year, code  ORDER BY COD_NUM ";

            BuildExcelToReport(Sql, GrupSql, "SummaryRanking");

        }

        protected void StatsPerContainerAreaExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "  SELECT  year,  lcode,  fk_lporto,  cod,   fk_direction,  fk_service,  dservice,  fk_area ,  " +
                             "  darea, Sum(c20) AS c20, Sum(c40) AS c40, Sum(teu) AS Total , Sum(wtmt) AS wtmt    " +
                             " FROM  vw_container  ";

            string GrupSql = " GROUP BY  year,  lcode,  fk_lporto,  cod,  fk_direction,  fk_service,  dservice,  fk_area, darea";

            BuildExcelToReport(Sql, GrupSql, "StatsPerContainerArea");
        }

        protected void StatsPerContainerExcelCarrierButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "SELECT  year,  lcode,  fk_lporto,  cod,  fk_direction,  fk_service,  dservice,  fk_carrier, " +
                             " Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS total, Sum(wtmt) AS wtmt   " +
                             " FROM  vw_container  ";


            string GrupSql = " GROUP BY  year,  lcode,  fk_lporto,  cod,  fk_direction,  fk_service,  dservice,  fk_carrier";

            BuildExcelToReport(Sql, GrupSql, "StatsPerContainerCarrier");
        }

        protected void RankingServiceExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "SELECT  fk_service,  dservice,  fk_direction,  fk_cliente,  fk_srep,  year, Sum(teu) AS teu , code , " +
                           " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4  " +
                           " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8  " +
                           " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12  " +
                           " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16  " +
                           " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM  " +
                           " FROM  vw_ranking  ";


            string GrupSql = " GROUP BY  fk_service,  dservice, fk_direction,  fk_cliente,  fk_srep,  year, code  ORDER BY COD_NUM ";

            BuildExcelToReport(Sql, GrupSql, "RankingService");
        }

        protected void RankingServiceByExcelAreaButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT lcode,  fk_lporto,  fk_service,  dservice,  fk_area,  darea,  fk_direction,  fk_cliente,  fk_srep,  year, Sum(teu) AS teu , code , " +
                           " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4  " +
                           " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8  " +
                           " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12  " +
                           " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16  " +
                           " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM  " +
                           " FROM  vw_ranking  ";


            string GrupSql = "GROUP BY  lcode,  fk_lporto,  fk_service,  dservice,  fk_area,  darea,  fk_direction , fk_cliente,  fk_srep, code , year ORDER BY COD_NUM ";

            BuildExcelToReport(Sql, GrupSql, "RankingServiceByArea");
        }

        protected void SummaryBrazilExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT  fk_service,  dservice,  fk_area,  darea,  fk_direction,  fk_carrier,  year, Sum(teu) AS teu , lcode , " +
                           " (CASE WHEN lcode = 'STO' THEN 1 WHEN lcode = 'RGR' THEN 2 WHEN lcode = 'ITJ' THEN 3 WHEN lcode = 'NVT' THEN 4 " +
                             " WHEN lcode = 'IOA' THEN 5 WHEN lcode = 'PNP' THEN 6 WHEN lcode = 'RIO' THEN 7 WHEN lcode = 'SEP' THEN 8 " +
                             " WHEN lcode = 'PCM' THEN 9 WHEN lcode = 'SQI' THEN 10  WHEN lcode = 'VCD' THEN 11  WHEN lcode = 'VTO' THEN 12 " +
                             " WHEN lcode = 'MSC' THEN 13 WHEN lcode = 'MSK' THEN 14 WHEN lcode = 'NYK' THEN 15 WHEN lcode = 'PIL' THEN 16 " +
                             " WHEN lcode = 'SFS' THEN 17 WHEN lcode = 'SVD' THEN 18 WHEN lcode = 'MAO' THEN 19 WHEN lcode = 'IMB' THEN 20 WHEN lcode = 'OTH' THEN 21 END) AS COD_NUM " +
                             " FROM vw_brasil_resumo ";

            string GrupSql = " GROUP BY fk_service, dservice, fk_area, darea, fk_direction, fk_carrier, year, lcode  ORDER BY COD_NUM ";

            BuildExcelToReport(Sql, GrupSql, "SummaryBrazil");
        }

        protected void SummaryClientExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT lcode , fk_lporto ,fk_service, dservice, fk_area, darea, fk_direction, fk_carrier, year, " +
                             " SUM(teu) AS teu, dmonth, fk_month, fk_cliente, fk_srep " +
                             " FROM vw_brasil_resumo ";

            string GrupSql = " GROUP BY lcode , fk_lporto ,fk_service, dservice, fk_area, darea, fk_direction, fk_carrier, year, dmonth, fk_month, fk_cliente, fk_srep ";

            BuildExcelToReport(Sql, GrupSql, "SummaryClient");
        }

        protected void RankingCommodityExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_service, dservice, fk_direction, commodity, year, Sum(teu) AS teu , code , " +
                        " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4  " +
                        " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8  " +
                        " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12  " +
                        " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16  " +
                        " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM  " +
                        " FROM  vw_ranking  ";

            string GrupSql = " GROUP BY fk_service, dservice, fk_direction, commodity, year , code order by  COD_NUM ";

            BuildExcelToReport(Sql, GrupSql, "RankingCommodity");
        }

        protected void SummaryPerDischargePortExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT lcode, fk_lporto, fk_area, darea, fk_service, dservice, fk_direction, " +
                             " fk_odporto, year, Sum(teu) AS Total , dmonth ,fk_month" +
                              " FROM vw_brasil_resumo ";

            string GrupSql = "  GROUP BY lcode, fk_lporto, fk_area, darea, fk_service, dservice, fk_direction, fk_odporto, year , dmonth ,fk_month ";

            BuildExcelToReport(Sql, GrupSql, "SummaryPerDischargePort");
        }

        protected void SummaryCarrierExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT lcode, fk_lporto, fk_area, darea, fk_service, dservice, fk_direction, fk_cliente, fk_srep, dmonth ,fk_month , " +
                          " fk_carrier, year, Sum(teu) AS Total " +
                          " FROM vw_brasil_resumo  ";

            string GrupSql = " GROUP BY lcode, fk_lporto, fk_area, darea, fk_service, dservice, fk_direction, fk_cliente, fk_srep, fk_carrier, year , dmonth ,fk_month";

            BuildExcelToReport(Sql, GrupSql, "SummaryCarrier");
        }

        protected void DetailedPerContainerExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "  SELECT fk_lporto, lcode, fk_service, dservice, fk_area, darea, fk_odporto, fk_cliente, fk_srep, fk_tcontainer, dcontainer, fk_direction, " +
                             "  cod, year, Sum(teu) AS teu, Sum(c40)AS Totalc40, Sum(c20) AS Totalc20 , code , " +
                            " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'HSUD' THEN 3 WHEN code = 'MSK' THEN 4 " +
                           " WHEN code = 'SAF' THEN 5 WHEN code = 'MSC' THEN 6 WHEN code = 'COSC' THEN 7 WHEN code = 'ZIM' THEN 8 " +
                           " WHEN code = 'CGM' THEN 9 WHEN code = 'LIB' THEN 10  WHEN code = 'CASAV' THEN 11  WHEN code = 'KLINE' THEN 12 " +
                           " WHEN code = 'MOL' THEN 13 WHEN code = 'HPG' THEN 14 WHEN code = 'PIL' THEN 15 WHEN code = 'NYK' THEN 16 " +
                           " WHEN code = 'HJS' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM " +
                           " FROM  vw_ranking_soma  ";

            string GrupSql = " GROUP BY fk_lporto, code , COD_NUM , lcode, fk_service, dservice, fk_area, darea, fk_odporto, fk_cliente, fk_srep, fk_tcontainer, dcontainer, fk_direction, cod, year ";

            BuildExcelToReport(Sql, GrupSql, "DetailedPerContainer");
        }

        protected void DetailedPerExcelClientButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, lcode, fk_service, dservice, fk_area, darea, fk_odporto, fk_cliente, fk_srep, fk_tcontainer, " +
                            "  dcontainer, fk_direction, cod, year, Sum(teu) AS teu, Sum(c40)AS totalc40, Sum(c20) AS totalC20, code," +
                            "  (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4 " +
                            "  WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8 " +
                            "  WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10 WHEN code = 'MOL' THEN 11 WHEN code = 'KLINE' THEN 12 " +
                            "  WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'PIL' THEN 15 WHEN code = 'NYK' THEN 16 " +
                            "  WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM " +
                            " FROM vw_ranking_soma ";

            string GrupSql = "  GROUP BY fk_lporto, lcode, fk_service, dservice, fk_area, darea, fk_odporto, fk_cliente, fk_srep, fk_tcontainer, " +
                              " dcontainer, fk_direction, cod, year , code , cod_num ";

            BuildExcelToReport(Sql, GrupSql, "DetailedPerClient");
        }

        protected void TypeContainerForeignPortExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "	SELECT year, lcode, fk_lporto, cod, fk_direction, fk_service, dservice, fk_odporto, fk_tcontainer, dcontainer , " +
                            " Sum(c20) AS c20, Sum(c40) AS c40, Sum(teu) AS Total, Sum(wtmt) AS wtmt " +
                            " FROM vw_tipo_container ";

            string GrupSql = " GROUP BY year, lcode, fk_lporto, cod, fk_direction, fk_service, dservice, fk_odporto, fk_tcontainer, dcontainer ";

            BuildExcelToReport(Sql, GrupSql, "TypeContainerForeignPort");
        }

        protected void CarrierClientExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "	SELECT fk_lporto, fk_cliente, darea, fk_carrier, fk_odporto, fk_direction, year, Sum(c20) AS c20," +
                    " Sum(c40) AS c40, Sum(teu)AS teu, Sum(wtmt) AS wtmt " +
                    " FROM vw_especifico_carrier_area ";

            string GrupSql = "GROUP BY fk_lporto, fk_cliente, darea, fk_carrier, fk_odporto, fk_direction, year";

            BuildExcelToReport(Sql, GrupSql, "CarrierClient");
        }

        protected void CarrierCommoditExcelyButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, commodity, darea, fk_carrier, fk_odporto, fk_direction, year, Sum(c20) AS c20, " +
                           " Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                           " FROM  vw_especifico_carrier_area ";

            string GrupSql = "GROUP BY fk_lporto, commodity, darea, fk_carrier, fk_odporto, fk_direction, year ";

            BuildExcelToReport(Sql, GrupSql, "CarrierCommodity");
        }

        protected void CarrierVasselExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_srep, fk_odporto, dservice, darea, year, fk_direction, fk_carrier, vessel, " +
                    " fk_month, dmonth, Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                    " FROM  vw_especifico_vessel ";

            string GrupSql = " GROUP BY fk_lporto, fk_cliente, fk_srep, fk_odporto, dservice, darea, year, fk_direction, fk_carrier, vessel, fk_month, dmonth ";

            BuildExcelToReport(Sql, GrupSql, "CarrierVassel");
        }

        protected void CommodityAreaExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, darea, fk_carrier, commodity, cod, fk_direction, year, Sum(c20)AS c20, " +
                           " Sum(c40)AS c40, Sum(teu) AS teu " +
                           " FROM vw_especifico_cliente_area ";

            string GrupSql = " GROUP BY fk_lporto, fk_cliente, darea, fk_carrier, commodity, cod, fk_direction, year;  ";

            BuildExcelToReport(Sql, GrupSql, "CommodityArea");
        }

        protected void ForeignPortClientExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_odporto,fport, dservice, darea, year, fk_direction, " +
                        " Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                        " FROM vw_especifico_vessel ";

            string GrupSql = " GROUP BY fk_lporto, fk_cliente, fk_odporto,fport, dservice, darea, year, fk_direction;";

            BuildExcelToReport(Sql, GrupSql, "ForeignPortClient");
        }

        protected void ForeignPortCarrierExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_carrier, fk_odporto, fport, dservice, darea, year, fk_direction, " +
                            " Sum(c20) AS c20, Sum(c40) AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                            " FROM vw_especifico_vessel ";

            string GrupSql = " GROUP BY fk_lporto, fk_carrier, fk_odporto, fport, dservice, darea, year, fk_direction; ";

            BuildExcelToReport(Sql, GrupSql, "ForeignPortCarrier");
        }

        protected void ShipperCneeByPortPairsExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT year, fk_lporto, shcnee, fk_cliente, dregion, fk_odporto, fk_direction, Sum(teu) AS Total , code , " +
                              " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ZIM' THEN 2 WHEN code = 'ALI' THEN 3 WHEN code = 'HJS' THEN 4 " +
                              " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HSUD' THEN 8 " +
                              " WHEN code = 'LIB' THEN 9 WHEN code = 'HPG' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12 " +
                              " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16 " +
                              " WHEN code = 'SAF' THEN 17 WHEN code = 'HPG' THEN 18 WHEN code = 'CHS' THEN 19 WHEN code = 'OTHS' THEN 20 END) " +
                              " AS COD_NUM " +
                           " FROM vw_especifico_shipper_cnee ";

            string GrupSql = " GROUP BY year, fk_lporto, shcnee, fk_cliente, dregion, fk_odporto, fk_direction , code , COD_NUM ";

            BuildExcelToReport(Sql, GrupSql, "ShipperCneeByPortPairs");
        }

        protected void CneeShipperByPortPairExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT year , fk_lporto, shcnee, fk_cliente, dregion, fk_odporto, fk_direction, Sum(teu) AS Total  , code , " +
                           " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ZIM' THEN 2 WHEN code = 'ALI' THEN 3 WHEN code = 'HJS' THEN 4 " +
                           " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HSUD' THEN 8 " +
                           " WHEN code = 'LIB' THEN 9 WHEN code = 'HPG' THEN 10  WHEN code = 'MOL' THEN 11  WHEN code = 'KLINE' THEN 12 " +
                           " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16 " +
                           " WHEN code = 'SAF' THEN 17 WHEN code = 'HPG' THEN 18 WHEN code = 'CHS' THEN 19 WHEN code = 'OTHS' THEN 20 END) " +
                           " AS COD_NUM " +
                          " FROM vw_especifico_shipper_cnee ";

            string GrupSql = " GROUP BY year, fk_lporto, shcnee, fk_cliente, dregion, fk_odporto, fk_direction , code , COD_NUM  ";

            BuildExcelToReport(Sql, GrupSql, "CneeShipperByPortPair");
        }

        protected void ClientCneeExcelButton_Click1(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_srep, darea, fk_carrier, fk_odporto, cod, fk_direction, " +
                         " year, shcnee, Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                         " FROM vw_especifico_cnee ";

            string GrupSql = " GROUP BY fk_lporto, fk_cliente, fk_srep, darea, fk_carrier, fk_odporto, cod, fk_direction, year, shcnee ";

            BuildExcelToReport(Sql, GrupSql, "ClientCnee");
        }

        protected void CneeClientExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_srep, darea, fk_carrier, fk_odporto, cod, " +
                          " fk_direction, year, shcnee, Sum(c20) AS c20, Sum(c40)AS c40, Sum(teu) AS teu, Sum(wtmt) AS wtmt " +
                          " FROM vw_especifico_cnee ";

            string GrupSql = "GROUP BY fk_lporto, fk_cliente, fk_srep, darea, fk_carrier, fk_odporto, cod, fk_direction, year, shcnee";

            BuildExcelToReport(Sql, GrupSql, "CneeClient");
        }

        protected void ClientAreaExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_lporto, fk_cliente, fk_srep, darea, dregion, fk_carrier, commodity, cod, fk_direction, year, " +
                          " Sum(c20) AS c20, Sum(c40) AS c40, Sum(teu) AS teu " +
                          " FROM vw_especifico_cliente_area ";

            string GrupSql = "GROUP BY  fk_lporto, fk_cliente, fk_srep, darea, dregion, fk_carrier, commodity, cod, fk_direction, year";

            BuildExcelToReport(Sql, GrupSql, "ClientArea");
        }

        protected void ClientSrepExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = "SELECT fk_lporto, lcode, fk_service, dservice, fk_cliente, fk_srep, fk_direction, year, Sum(c20)AS c20, Sum(c40)AS c40, Sum(teu) AS teu, " +
                  " (CASE WHEN fk_carrier = 'EVERGREEN' THEN sum(teu) WHEN fk_carrier = 'HATSU MARINE' THEN sum(teu) WHEN fk_carrier = 'ITALIA MARITTIMA' THEN sum(teu) END) " +
                  " as grieg FROM vw_ranking_soma ";


            string GrupSql = "GROUP BY fk_lporto, lcode, fk_service, dservice, fk_cliente, fk_srep, fk_direction, year ,fk_carrier ";

            BuildExcelToReport(Sql, GrupSql, "ClientSrep");
        }

        protected void FollowUpReportExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT a.fk_service, a.dservice, a.fk_direction, a.fk_cliente, a.fk_srep, a.year, Sum(a.teu) AS teu, to_char(b.data, 'MONTH/YYYY')|| ' ' || b.description as remarks,a.code , " +
            " (CASE WHEN code = 'EMC' THEN SUM(teu) END) AS EMC " +
            " FROM vw_ranking a " +
            " LEFT OUTER JOIN tbl_contato b " +
            " ON a.fk_cliente = b.fk_cliente AND a.year = b.year ";


            string GrupSql = " GROUP BY a.fk_service, a.dservice, a.fk_direction, a.fk_cliente, a.fk_srep, a.year,a.code,a.fk_carrier,a.teu,remarks ";


            BuildExcelToReport(Sql, GrupSql, "FollowUpReport");
        }

        protected void SummaryPortsExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_service, dservice, fk_area, darea, fk_odporto ,cod, fk_direction, year, Sum(teu) AS teu ,code , " +
                              " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4 " +
                              " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8 " +
                              " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10 WHEN code = 'MOL' THEN 11 WHEN code = 'KLINE' THEN 12 " +
                              " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16 " +
                              " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM " +
                              " FROM vw_ranking ";

            string GrupSql = " GROUP BY  fk_service, dservice, fk_area, darea, fk_odporto ,cod, fk_direction, year ,cod_num , code  , cod_num   ";

            BuildExcelToReport(Sql, GrupSql, "SummaryPorts");
        }

        protected void SummaryCountriesExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT fk_service, dservice, fk_area, darea, fk_country ,cod, fk_direction, year, Sum(teu) AS teu , code , " +
                       " (CASE WHEN code = 'EMC' THEN 1 WHEN code = 'ALI' THEN 2 WHEN code = 'ZIM' THEN 3 WHEN code = 'HJS' THEN 4 " +
                       " WHEN code = 'CGM' THEN 5 WHEN code = 'COSC' THEN 6 WHEN code = 'CSAV' THEN 7 WHEN code = 'HPG' THEN 8 " +
                      " WHEN code = 'HSUD' THEN 9 WHEN code = 'LIB' THEN 10 WHEN code = 'MOL' THEN 11 WHEN code = 'KLINE' THEN 12 " +
                      " WHEN code = 'MSC' THEN 13 WHEN code = 'MSK' THEN 14 WHEN code = 'NYK' THEN 15 WHEN code = 'PIL' THEN 16 " +
                      " WHEN code = 'SAF' THEN 17 WHEN code = 'CHS' THEN 18 WHEN code = 'OTHS' THEN 19 END) AS COD_NUM " +
                      " FROM vw_ranking  ";

            string GrupSql = "GROUP BY fk_service, dservice, fk_area, darea, fk_country ,cod, fk_direction, year , code  , cod_num   ";

            BuildExcelToReport(Sql, GrupSql, "SummaryCountries");
        }

        protected void SummaryClientMonthExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT lcode, fk_lporto, fk_service, fk_direction, fk_cliente, fk_srep, year, Sum(teu) AS Total  , dmonth, fk_month" +
                         " FROM vw_brasil_resumo";

            string GrupSql = "GROUP BY lcode, fk_lporto, fk_service, fk_direction, fk_cliente, fk_srep, year , dmonth, fk_month ";

            BuildExcelToReport(Sql, GrupSql, "SummaryClientMonth");

        }

        protected void TotalMovementsByPortExcelButton_Click(object sender, ImageClickEventArgs e)
        {
            string Sql = " SELECT        fk_direction, fk_cliente, fk_srep, fk_tipo, year, fk_service, lcode, cod, SUM(teu) AS teu, " +
                           " (CASE WHEN fk_carrier = 'EVERGREEN' THEN SUM(teu) WHEN fk_carrier = 'LLOYD TRI' THEN SUM(teu) WHEN fk_carrier = 'HATSU MARINE' THEN SUM(teu) WHEN " +
                           "  fk_carrier = 'ITALIA MARITTIMA' THEN SUM(teu) END) AS grieg, " +
                           " (CASE WHEN fk_service = 'ESA' THEN 1 WHEN fk_service = 'SNA' THEN 2 WHEN fk_service = 'EUSA' THEN 3 WHEN fk_service = 'MED' THEN 4 WHEN " +
                           " fk_service = 'SA' THEN 5 WHEN fk_service = 'EAF' THEN 6 WHEN fk_service " +
                           " = 'WAF' THEN 7 WHEN fk_service = 'UNK' THEN 8 END) AS fk_service_NUM " +
                           " FROM vw_outros_resumo ";


            string GrupSql = " GROUP BY fk_direction, fk_cliente, fk_srep, fk_tipo, year, fk_service, lcode, cod, fk_carrier , fk_service_NUM ";

            BuildExcelToReport(Sql, GrupSql, "TotalMovementsByPort");
        }

        //public void BuildExcelReport(string Sql, string GrupSql, string NameReport)
        //************************************* MOSTRAR O EXCEL NO REPORTVIWER 
        //{
        //    string cs = ConfigurationManager.AppSettings["BancoProducao"];

        //    ButtonViwerReport.Enabled = true;
        //    MultiView.ActiveViewIndex = 4;
        //    ReportViewer1.Visible = true;
        //    Erros.Visible = false;

        //    // estilos 
        //    ButtonViwerReport.BackColor = Color.DeepSkyBlue;
        //    ButtonViwerReport.BorderColor = Color.ForestGreen;
        //    ButtonReports.BackColor = Color.Silver;
        //    ButtonReports.BorderColor = Color.Silver;
        //    ButtonRoutes.BackColor = Color.Silver;
        //    ButtonRoutes.BorderColor = Color.Silver;
        //    ButtonYear.BackColor = Color.Silver;
        //    ButtonYear.BorderColor = Color.Silver;
        //    ButtonCarrier.BackColor = Color.Silver;
        //    ButtonCarrier.BorderColor = Color.Silver;

        //    // aba 1 
        //    var year = Session["ParameterYear"];
        //    var StartMonth = Session["ParameterStartMonth"];
        //    var FinalMonth = Session["ParameterFinalMonth"];
        //    var listport = Session["ParameterListPort"];
        //    var Direction = Session["ParameterListDirecao"];
        //    var listClient = Session["ParameterListCliente"];
        //    var ListCarregamento = Session["ParameterListCarregamento"];
        //    var ListContainer = Session["ParameterListContainer"];
        //    var ListRestricoes = Session["ParameterListRestricao"];

        //    //aba 2
        //    var ListRotas = Session["ParameterListRotas"];
        //    var ListAreas = Session["ParameterListAreas"];
        //    var ListRegion = Session["ParameterListRegiao"];
        //    var ListPais = Session["ParameterListPais"];
        //    var ListPortsPais = Session["ParameterListPortsPais"];

        //    //  aba 3
        //    var ListCarrier = Session["ParameterListTranpostadora"];
        //    var ListCommodity = Session["ParameterListCommodity"];
        //    var ListSalesRep = Session["ParameterListVendedor"];
        //    var ListClients = Session["ParameterListClients"];


        //    try
        //    {
        //        if (NameReport == "FollowUpReport")
        //        {
        //            Sql += utl.MarketStatiscsParametersQuery(year.ToString(), StartMonth.ToString(), FinalMonth.ToString(), listport.ToString(), Direction.ToString(), listClient.ToString(), ListCarregamento.ToString(),
        //                    ListContainer.ToString(), ListRotas.ToString(), ListAreas.ToString(), ListRegion.ToString(), ListPais.ToString(), ListPortsPais.ToString(),
        //                    ListCarrier.ToString(), ListCommodity.ToString(), ListClients.ToString(), ListSalesRep.ToString(), ListRestricoes.ToString(), NameReport) + GrupSql;
        //        }
        //        else
        //        {
        //            Sql += utl.MarketStatiscsParametersQuery(year.ToString(), StartMonth.ToString(), FinalMonth.ToString(), listport.ToString(), Direction.ToString(), listClient.ToString(), ListCarregamento.ToString(),
        //                           ListContainer.ToString(), ListRotas.ToString(), ListAreas.ToString(), ListRegion.ToString(), ListPais.ToString(), ListPortsPais.ToString(),
        //                           ListCarrier.ToString(), ListCommodity.ToString(), ListClients.ToString(), ListSalesRep.ToString(), ListRestricoes.ToString()) + GrupSql;
        //        }

        //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportViewer/MarketStatistics/Excel/" + NameReport + ".rdlc");

        //        NpgsqlDataAdapter da = new NpgsqlDataAdapter(Sql, cs);
        //        DataTable ds = new DataTable();
        //        ds.Reset();
        //        da.Fill(ds);

        //        if (ds.Rows.Count > 1)
        //        {
        //            utl.addLog(Request.LogonUserIdentity.Name.ToString(), NameReport, MapPath("~/Ti/LogMovi.xml"));
        //            ReportViewer1.Visible = true;
        //            ReportDataSource rds = new ReportDataSource(NameReport, ds);
        //            this.ReportViewer1.LocalReport.DataSources.Clear();
        //            this.ReportViewer1.LocalReport.DataSources.Add(rds);
        //            this.ReportViewer1.LocalReport.Refresh();
        //        }
        //        else
        //        {
        //            Erros.Visible = true;
        //            ReportViewer1.Visible = false;
        //            Erros.InnerHtml = utl.MessageError();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ReportViewer1.Visible = false;
        //        Erros.Visible = true;
        //        Erros.InnerHtml = utl.MessageError();
        //        utl.addLog(Request.LogonUserIdentity.Name.ToString(), NameReport, ex.ToString(), MapPath("LOG.xml"));
        //    }
        //}  


        #endregion

        #region Graficos GOOGLE CHARTS

        // busca os dados para o grafico
        public static DataTable BuildGraphic(string sql, string GrupSql)
        {
            string cs = ConfigurationManager.AppSettings["BancoProducao"];

            sql += Util.GraphicMarketStatiscsParametersQuery(parametros.year.ToString(), parametros.StartMonth.ToString(), parametros.FinalMonth.ToString(), parametros.listport.ToString(),
                      parametros.Direction.ToString(), parametros.listClient.ToString(), parametros.ListCarregamento.ToString(),
                      parametros.ListContainer.ToString(), parametros.ListRotas.ToString(), parametros.ListAreas.ToString(), parametros.ListRegion.ToString(), parametros.ListPais.ToString(),
                      parametros.ListPortsPais.ToString(), parametros.ListCarrier.ToString(), parametros.ListCommodity.ToString(), parametros.ListClients.ToString(),
                      parametros.ListSalesRep.ToString(), parametros.ListRestricoes.ToString()) + GrupSql;


            DataTable result = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn))
                    {
                        da.Fill(result);
                    }
                }
                catch (Exception ex)
                {
                    // log
                }
                finally
                {
                    conn.Close();
                } 
            }

            return result;
        }

        private static object[] BuildJason(DataTable DadosGraphic, string Descicao, string valor)
        {
            List<GoogleChartData> GraphicJason = new List<GoogleChartData>();

            foreach (DataRow row in DadosGraphic.Rows)
            {
                GraphicJason.Add(new GoogleChartData
                {
                    Descricao = Convert.ToString(row[Descicao]),
                    Valor = Convert.ToInt32(row[valor]),
                });

            }

            GraphicJason = GraphicJason.ToList();

            var chartData = new object[GraphicJason.Count + 1];
            chartData[0] = new object[]{
                "Total",
                "Total Teus",
            };

            int j = 0;
            foreach (var i in GraphicJason)
            {
                j++;
                chartData[j] = new object[] { i.Descricao, i.Valor };
            }
            return chartData;
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] SummaryServiceGraphic()
        {
            string sql = " SELECT  fk_carrier,   SUM(teu)AS total FROM vw_outros_resumo ";
            string GrupSql = " GROUP BY fk_carrier  ORDER BY total desc  limit 10";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_carrier", "total");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] RankingServiceGraphics()
        {
            string sql = "SELECT fk_cliente,  Sum(teu)AS teu FROM vw_ranking   ";
            string GrupSql = "GROUP BY fk_cliente ORDER BY teu desc limit 10";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_cliente", "teu");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] SummaryBrazilExcelGraphics()
        {
            string sql = " SELECT Sum(teu) AS teu , lcode FROM vw_brasil_resumo    ";
            string GrupSql = "GROUP BY lcode  ORDER BY teu desc limit 10";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "lcode", "teu");
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] RankingCommodityGraphics()
        {
            string sql = "  SELECT commodity, Sum(teu) AS teu FROM  vw_ranking     ";
            string GrupSql = " GROUP BY   commodity   order by  teu desc limit 10";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "commodity", "teu");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] TypeContainerForeignPortGraphics()
        {
            string sql = "SELECT  dcontainer , Sum(teu) AS Total FROM vw_tipo_container ";
            string GrupSql = "GROUP BY dcontainer order by total desc";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "dcontainer", "total");
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] StatsPerContainerAreaGraphics()
        {
            string sql = " SELECT darea, Sum(teu) AS total  FROM  vw_container ";
            string GrupSql = "  GROUP BY darea order by total desc limit 10";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "darea", "total");
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] ClientSrepGraphics()
        {
            string sql = "SELECT  fk_srep,  Sum(teu) AS teu FROM vw_ranking_soma ";
            string GrupSql = " GROUP BY fk_srep  order by teu desc limit 10 ";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_srep", "teu");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] SummaryPortsGraphics()
        {
            string sql = " SELECT  fk_odporto ,  Sum(teu) AS teu  FROM vw_ranking  ";
            string GrupSql = " GROUP BY   fk_odporto order by teu desc limit 10 ";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_odporto", "teu");
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] SummaryCountriesGraphics()
        {
            string sql = "  SELECT  fk_country , Sum(teu) AS teu   FROM vw_ranking    ";
            string GrupSql = " GROUP BY fk_country  order by teu desc limit 10 ";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphic(sql, GrupSql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_country", "teu");
        }

        // BOTOES PARA FAZER O ESTILO DA NAV-BAR
        protected void SummaryServiceGraphics_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();
        }


        protected void RankingServiceGraphic_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();
        }

        protected void SummaryBrazilExcelGraphic_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();

        }

        protected void RankingCommodityGraphic_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();
        }

        protected void TypeContainerForeignPortGraphic_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();

        }

        protected void StatsPerContainerAreaGraphic_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();
        }

        protected void ClientSrepGraphic_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();
        }

        protected void SummaryPortsGraphic_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();

        }

        protected void SummaryCountriesGraphic_Click(object sender, ImageClickEventArgs e)
        {
            GraphicStyleNavBar();
        }



        #endregion


        // implentar função de tirar de dropdowns
        protected void ListClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string busca = "";
            //foreach (ListItem li in ListClientes.Items)
            //{
            //    if (li.Selected)
            //    {

            //        busca += "'" + li.Value + "'" + ',';
            //    }
            //}

            //NovaListaCliente.DataSource = dataset;
            //NovaListaCliente.DataTextField = "Descricao";
            //NovaListaCliente.DataValueField = "Codigo";
            //NovaListaCliente.DataBind();
        }

        protected void NovaListaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}