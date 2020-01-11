using Microsoft.Reporting.WebForms;
using Npgsql;
using SCE.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCE
{
    public partial class FormEmc : System.Web.UI.Page
    {
        private DropDownListDados dados = new DropDownListDados();
        private Util utl = new Util();
        private string cs = ConfigurationManager.AppSettings["BancoProducao"];


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // progress.Visible = false;
                ButtonYear.BackColor = Color.DeepSkyBlue;
                ButtonYear.BorderColor = Color.ForestGreen;

                ButtonViwerReport.BackColor = Color.Silver;
                ButtonViwerReport.BorderColor = Color.Silver;

                multiViewFormEmc.ActiveViewIndex = 0;

                try
                {
                    LoadDropDownList();
                }
                catch (Exception ex)
                {
                    utl.addLog("System", "Dropdown", "", ex.ToString(), MapPath("LOG.xml"));
                    Response.Redirect("~/");
                }
            }
        }

        private void LoadDropDownList()
        {
            ListYear.DataSource = dados.ListYear();
            ListYear.DataTextField = "year";
            ListYear.DataValueField = "year";
            ListYear.DataBind();

            ListDirecao.DataSource = dados.ListDirection();
            ListDirecao.DataTextField = "text";
            ListDirecao.DataValueField = "direction";
            ListDirecao.DataBind();

            ListPort.DataSource = dados.ListPort();
            ListPort.DataTextField = "lporto";
            ListPort.DataValueField = "lcode";
            ListPort.DataBind();

            ListOwners.DataSource = dados.ListOwners();
            ListOwners.DataTextField = "text";
            ListOwners.DataValueField = "code";
            ListOwners.DataBind();

            ListCommodity.DataSource = dados.ListCommodityM();
            ListCommodity.DataTextField = "text";
            ListCommodity.DataValueField = "tcommodity";
            ListCommodity.DataBind();

            /// regiao aba 2 

            ListRotas.DataSource = dados.ListRotas();
            ListRotas.DataTextField = "text";
            ListRotas.DataValueField = "service";
            ListRotas.DataBind();

            ListSling.DataSource = dados.ListSling();
            ListSling.DataTextField = "text";
            ListSling.DataValueField = "sling";
            ListSling.DataBind();

            ListArea.DataSource = dados.ListAreas();
            ListArea.DataTextField = "text";
            ListArea.DataValueField = "fk_area";
            ListArea.DataBind();

            ListClientes.DataSource = dados.ListClienteM();
            ListClientes.DataTextField = "cliente";
            ListClientes.DataValueField = "cliente";
            ListClientes.DataBind();

            /// regiao aba 3
        }

        protected void SelectCliente_Click(object sender, EventArgs e)
        {

            if (dados.ListClientPesquisaM(Pcliente.Text.ToUpper()).Tables["tbl_actual"].Rows.Count != 0)
            {
                ListClientes.DataSource = dados.ListClientPesquisaM(Pcliente.Text.ToUpper());
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

        protected void ButtonYear_Click(object sender, EventArgs e)
        {
            ButtonYear.BackColor = Color.DeepSkyBlue;
            ButtonYear.BorderColor = Color.ForestGreen;

            ButtonReport.BackColor = Color.Silver;
            ButtonReport.BorderColor = Color.Silver;

            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;


            ButtonViwerReport.BackColor = Color.Silver;
            ButtonViwerReport.BorderColor = Color.Silver;

            multiViewFormEmc.ActiveViewIndex = 0;
             


        }

        protected void ButtonViwerReport_Click(object sender, EventArgs e)
        {
            ButtonViwerReport.BackColor = Color.DeepSkyBlue;
            ButtonViwerReport.BorderColor = Color.ForestGreen;

            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;

            ButtonReport.BackColor = Color.Silver;
            ButtonReport.BorderColor = Color.Silver;

            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;

            multiViewFormEmc.ActiveViewIndex = 3;
        }

        protected void ButtonReport_Click(object sender, EventArgs e)
        {
            ButtonReport.BackColor = Color.DeepSkyBlue;
            ButtonReport.BorderColor = Color.ForestGreen;

            ButtonViwerReport.BackColor = Color.Silver;
            ButtonViwerReport.BorderColor = Color.Silver;

            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;

            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;

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

            string DadosListOwners = "";
            foreach (ListItem li in ListOwners.Items)
            {
                if (li.Selected)
                {

                    DadosListOwners += "'" + li.Value + "'" + ',';
                }
            }

            DadosListOwners += "''";
            //=====================================================================================================================

            string DadosListCommodity = "";
            foreach (ListItem li in ListCommodity.Items)
            {
                if (li.Selected)
                {

                    DadosListCommodity += "'" + li.Value + "'" + ',';
                }
            }

            DadosListCommodity += "''";
            //=====================================================================================================================


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
            string DadosListSling = "";
            foreach (ListItem li in ListSling.Items)
            {
                if (li.Selected)
                {

                    DadosListSling += "'" + li.Value + "'" + ',';
                }
            }
            DadosListSling += "''";
            //=====================================================================================================================
            string DadosListAreas = "";
            foreach (ListItem li in ListArea.Items)
            {
                if (li.Selected)
                {
                    DadosListAreas += "'" + li.Value + "'" + ',';
                }
            }
            DadosListAreas += "''";
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

            //===================================================================================================================== terceira aba

            //  Primeira aba 
            Session["ParameterYear"] = Dadoyear; // 2012
            Session["ParameterStartMonth"] = StartMonth.SelectedValue; //mes inicial 
            Session["ParameterFinalMonth"] = FinalMonth.SelectedValue; // mes final 
            Session["ParameterListDirecao"] = DadosListDirecao; // imp exp 
            Session["ParameterListPort"] = DadosListPort; // portos 
            Session["ParameterListCommodity"] = DadosListCommodity; // portos 
            Session["ParameterListOwners"] = DadosListOwners; // portos 
            Session["ParameterListClients"] = DadosListClientes;

            // Segunda aba 

            Session["ParameterListRotas"] = DadosListRotas; // EAF
            Session["ParameterListAreas"] = DadosListAreas; // EAF 
            Session["ParameterListSling"] = DadosListSling; // EAF 

            // Terceira aba 

            multiViewFormEmc.ActiveViewIndex = 2;
        }



        protected void ButtonRoutes_Click(object sender, EventArgs e)
        {
            ButtonRoutes.BackColor = Color.DeepSkyBlue;
            ButtonRoutes.BorderColor = Color.ForestGreen;

            ButtonReport.BackColor = Color.Silver;
            ButtonReport.BorderColor = Color.Silver;

            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;

            multiViewFormEmc.ActiveViewIndex = 1;

        }


        protected void ListRotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string busca = "";
            foreach (ListItem li in ListRotas.Items)
            {
                if (li.Selected)
                {
                    busca += "'" + li.Value + "'" + ',';
                }
            }
            busca += "'" + "'";
            ListSling.DataSource = dados.ListSlingDinamic(busca);
            ListSling.DataTextField = "text";
            ListSling.DataValueField = "sling";
            ListSling.DataBind();


            ListArea.DataSource = dados.ListAreasDinamic(busca);
            ListArea.DataTextField = "text";
            ListArea.DataValueField = "fk_area";
            ListArea.DataBind();

        }

       


        protected void ResumoServiceButton_Click(object sender, EventArgs e)
        {

            string Sql = " SELECT owner, fk_month, dmonth, year, fk_bporto, fk_service, direction, currency, SUM(somadec20) AS c20, SUM(vlr20)AS vlr20, " +
                                 " SUM(somadec40) AS c40, SUM(vlr40) AS vlr40, SUM(teus) AS teus, SUM(totalrev) AS totalrev  " +
                                 " FROM vw_summary_per_month ";

            string GrupSql = "GROUP BY owner, fk_month, dmonth, year, fk_bporto, fk_service , direction, currency ";

            BuildReport(Sql , GrupSql , "ResumoService");

        }

        protected void ResumoNavioCommodityButton_Click(object sender, EventArgs e)
        {

            string Sql = " SELECT owner, cliente, vessel, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency, Sum(somadec20) AS c20, " +
                                  " Sum(somadec40) AS c40, Sum(teus)AS teus, Sum(totalrev) AS totalrev " +
                                  " FROM vw_summary_container ";

            string GrupSql = " GROUP BY owner, cliente, vessel, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency";

            BuildReport(Sql, GrupSql, "ResumoNavioCommodity");
           
        }

        protected void ResumoNavioTosButton_Click(object sender, EventArgs e)

        {
            
            string Sql = " SELECT tos, direction, currency, year, fk_month, owner, fk_service, dmonth, vessel, cliente, Sum(teus)AS teus, Sum(total_freight) AS TotalFreight " +
                          " FROM  vw_summary_tos ";

            string GrupSql = " GROUP BY tos , direction, currency ,year, fk_month, owner, fk_service, dmonth, vessel, cliente ";

            BuildReport(Sql, GrupSql, "ResumoNavioTos");
        }

        protected void PerformanceClienteButton_Click(object sender, EventArgs e)
        {
             
          string Sql = " SELECT owner, cliente, fk_srep, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency, Sum(somadec20) AS c20, " +
                                  " Sum(somadec40) AS c40, Sum(teus) AS teus , Sum(totalrev) AS totalrev " +
                                  " FROM vw_summary_performance  ";

            string GrupSql = "GROUP BY owner, cliente, fk_srep, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency  "; 

            BuildReport(Sql, GrupSql, "PerformanceCliente");

        }

        protected void ResumoClienteButton_Click(object sender, EventArgs e) // ResumoCliente
        {
            var  ClienteM = Session["ParameterListClients"];
            string GrupSql = "";

            string Sql = " SELECT owner, cliente, fk_area , darea, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency , " +
                                   " Sum(somadec20) AS c20, Sum(vlr20)AS vlr20, Sum(somadec40) AS c40, Sum(vlr40) AS vlr40, Sum(teus) AS teus, Sum(totalrev) AS totalrev " +
                                   " FROM vw_summary_cliente ";

            if (ClienteM.ToString() == "''" || ClienteM == null)
            {
            }
            else
            {
                  GrupSql = " AND cliente In(" + ClienteM + ")";
            }

               GrupSql += "GROUP BY owner, cliente, fk_area, darea, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency";

            BuildReport(Sql, GrupSql, "ResumoCliente");
           
        }

        protected void SummaryNavioButton_Click(object sender, EventArgs e)
        {
            
          string Sql = " SELECT owner,direction,fk_bporto,fk_service,dservice,fk_area,darea,dporto,vessel,fk_month,dmonth,year,currency, " +
                                  " Sum(somadec20) AS c20, Sum(vlr20)AS vlr20, Sum(somadec40) AS c40, Sum(vlr40) AS vlr40, Sum(teus) AS teus, Sum(totalrev) AS totalrev " +
                                  " FROM vw_summary_por_navio ";

            string GrupSql = " GROUP BY owner, direction,fk_bporto ,fk_service, dservice,fk_area,darea ,dporto,vessel,fk_month,dmonth,year,currency  ";

            BuildReport(Sql, GrupSql, "SummaryNavio");

        }

        protected void ResumoCommodityNavioButton_Click(object sender, EventArgs e)
        {
             
          string Sql = " SELECT owner, vessel, direction, fk_service, dservice, fk_tcommodity , d_commoditity, fk_month, dmonth, year, currency , " +
                                  " Sum(somadec20) AS c20, Sum(somadec40)AS c40, Sum(teus) AS teus, Sum(totalrev) AS totalrev " +
                                  " FROM vw_summary_container " ;

            string GrupSql = "GROUP BY owner, vessel, direction, fk_service, dservice, fk_tcommodity , d_commoditity, fk_month, dmonth, year, currency; ";

            BuildReport(Sql, GrupSql, "ResumoCommodityNavio");
            
        }

        protected void ResumoTosButton_Click(object sender, EventArgs e)
        {
            
                   string Sql = " SELECT tos , direction, currency, year, owner, fk_service, cliente, Sum(teus) AS teus, " +
                                  " Sum(total_freight)AS total, Sum(total_freight) / Sum(teus) AS Avg " +
                                  " FROM vw_summary_tos";

            string GrupSql = "GROUP BY tos , direction, currency ,year, owner, fk_service, cliente";

            BuildReport(Sql, GrupSql, "ResumoTos");

        }

        protected void PerformaceTotalClienteButton_Click(object sender, EventArgs e)
        {
            
            string Sql = " SELECT owner, cliente, fk_srep, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency, Sum(somadec20) AS c20, " +
                                  " Sum(somadec40) AS c40, Sum(teus) AS teus , Sum(totalrev) AS totalrev " +
                                  " FROM vw_summary_performance  ";

            string GrupSql = "GROUP BY owner, cliente, fk_srep, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency  ";

            BuildReport(Sql, GrupSql, "PerformaceTotalCliente");
             
        }

        protected void ControleClientesButton_Click(object sender, EventArgs e)
        {
             
            string Sql = " SELECT fk_month , dmonth , owner, cliente, fk_srep, direction, fk_service, dservice, fk_bporto, year, " +
                                  "  Sum(teus) AS Totalteus  " +
                                  "  FROM vw_summary_cliente ";

            string GrupSql = "GROUP BY  fk_month , dmonth , owner, cliente, fk_srep, direction, fk_service, dservice, fk_bporto, year ";

            BuildReport(Sql, GrupSql, "ControleClientes");

        }

        protected void ResumoMensalButton_Click(object sender, EventArgs e)
        {
            
            string Sql = " SELECT owner, direction, fk_service, dservice, fk_area, darea, fk_region ,fk_bporto, dporto, fk_month, dmonth, year, currency , " +
                                  " Sum(somadec20) AS c20, Sum(somadec40)AS c40, Sum(teus) AS teus, Sum(totalrev) AS totalrev " +
                                  " FROM vw_summary_mensal";

            string GrupSql = " GROUP BY owner, direction, fk_service, dservice, fk_area, darea, fk_region ,fk_bporto, dporto, fk_month, dmonth, year, currency";

            BuildReport(Sql, GrupSql, "ResumoMensal");
          
        }

        protected void ResumoClienteNavioButton_Click(object sender, EventArgs e)
        {
             
            var ClienteM = Session["ParameterListClients"];
            string GrupSql = "";

          
            string Sql = " SELECT owner, cliente, vessel, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency, Sum(somadec20) AS c20, " +
                                  " Sum(somadec40) AS c40, Sum(teus)AS teus, Sum(totalrev) AS totalrev " +
                                  " FROM vw_summary_container ";

            if (ClienteM.ToString() == "''" || ClienteM == null)
            {
            }
            else
            {
                GrupSql = " AND cliente In(" + ClienteM + ")";
            }

              GrupSql += "GROUP BY owner, cliente, vessel, direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency";

            BuildReport(Sql, GrupSql, "ResumoClienteNavio");

        }

        protected void ResumoContainerNavioButton_Click(object sender, EventArgs e)
        {
             
                  string Sql = " SELECT owner, vessel, direction, fk_service, dservice, tcontainer, fk_month, dmonth, year, currency, " +
                                  " Sum(somadec20) AS c20, Sum(somadec40)AS c40, Sum(teus) AS teus, Sum(totalrev) AS totalrev " +
                                  " FROM vw_summary_container ";

            string GrupSql = " GROUP BY owner, vessel, direction, fk_service, dservice, tcontainer, fk_month, dmonth, year, currency  ";

            BuildReport(Sql, GrupSql, "ResumoContainerNavio");
            
        }

        protected void ResumoArmadorButton_Click(object sender, EventArgs e)
        {
             
            string Sql = " SELECT owner, cliente, dporto , direction, fk_service, dservice, fk_bporto, fk_month, dmonth, " +
                                   " year, currency , Sum(somadec20) AS c20, Sum(vlr20) AS vlr20, " +
                                   " Sum(somadec40) AS c40, Sum(vlr40)AS vlr40, Sum(teus) AS teus, Sum(totalrev) AS totalrev " +
                                   " FROM vw_summary_performance ";

            string GrupSql = " GROUP BY owner, cliente, dporto , direction, fk_service, dservice, fk_bporto, fk_month, dmonth, year, currency ";

            BuildReport(Sql, GrupSql, "ResumoArmador");

        }

        protected void ResumoCommodityButton_Click(object sender, EventArgs e)
        {
            string Sql = " SELECT owner, cliente , direction , fk_service, dservice, fk_area, darea, fk_bporto, fk_tcommodity, d_commoditity, fk_month, " +
                                  " dmonth, year, currency , Sum(somadec20) AS c20, Sum(somadec40) AS c40, Sum(teus) AS teus , Sum(totalrev) AS totalrev  " +
                                  "  FROM vw_summary_commodity ";

            string GrupSql = "GROUP BY owner, cliente , direction , fk_service, dservice, fk_area, darea, fk_bporto, fk_tcommodity, d_commoditity, fk_month, dmonth, year, currency";

            BuildReport(Sql, GrupSql, "ResumoCommodity");
        }

        public void BuildReport(string Sql , string GrupSql , string NameReport)
        {
            multiViewFormEmc.ActiveViewIndex = 3;
            ButtonViwerReport.Enabled = true;
            ReportViewer1.Visible = true;
            Erros.Visible = false;

            ButtonViwerReport.BackColor = Color.DeepSkyBlue;
            ButtonViwerReport.BorderColor = Color.ForestGreen;

            ButtonRoutes.BackColor = Color.Silver;
            ButtonRoutes.BorderColor = Color.Silver;

            ButtonReport.BackColor = Color.Silver;
            ButtonReport.BorderColor = Color.Silver;

            ButtonYear.BackColor = Color.Silver;
            ButtonYear.BorderColor = Color.Silver;

            // aba 1 
            var yearM = Session["ParameterYear"];
            var StartMonthM = Session["ParameterStartMonth"];
            var FinalMonthM = Session["ParameterFinalMonth"];
            var listportM = Session["ParameterListPort"];
            var DirectionM = Session["ParameterListDirecao"];
            var ListCommodityM = Session["ParameterListCommodity"];
            var ListOwnersM = Session["ParameterListOwners"];

            //aba 2
            var ListRotasM = Session["ParameterListRotas"];
            var ListSlingM = Session["ParameterListSling"];
            var ListAreasM = Session["ParameterListAreas"];

            try
            {

                Sql += utl.ParametersQueryManifest(yearM.ToString(), StartMonthM.ToString(), FinalMonthM.ToString(), listportM.ToString(),
                                                  DirectionM.ToString(), ListRotasM.ToString(), ListAreasM.ToString(), ListSlingM.ToString(),
                                                  ListCommodityM.ToString(), ListOwnersM.ToString())+ GrupSql; 

                ReportParameterCollection parameter = new ReportParameterCollection();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportViewer/Manifest/"+NameReport+".rdlc");

                parameter.Add(new ReportParameter("RStartMonth", Session["ParameterStartMonth"].ToString()));
                parameter.Add(new ReportParameter("RFinalMonth", Session["ParameterFinalMonth"].ToString()));

                parameter.Add(new ReportParameter("RPorts", Session["ParameterListPort"].ToString()));
                parameter.Add(new ReportParameter("RSling", Session["ParameterListSling"].ToString()));
                parameter.Add(new ReportParameter("RArea", Session["ParameterListAreas"].ToString()));
                parameter.Add(new ReportParameter("Ryear", Session["ParameterYear"].ToString()));

                parameter.Add(new ReportParameter("RCommodity", Session["ParameterListCommodity"].ToString().Trim()));
                ReportViewer1.LocalReport.SetParameters(parameter);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(Sql, cs);
                DataTable ds = new DataTable();
                ds.Reset();
                da.Fill(ds);

                if (ds.Rows.Count > 0)
                {

                }
                else
                {
                    Erros.InnerHtml = utl.MessageError();
                    ReportViewer1.Visible = false;
                    Erros.Visible = true;
                }
                if (Page.IsPostBack)
                {
                    utl.addLog(Request.LogonUserIdentity.Name.ToString(), NameReport, Sql, MapPath("~/Ti/LogMovi.xml"));
                    ReportDataSource rds = new ReportDataSource(NameReport, ds);
                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(rds);
                    this.ReportViewer1.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {
                Erros.InnerHtml = utl.MessageError();
                ReportViewer1.Visible = false;
                Erros.Visible = true;
                utl.addLog(Request.LogonUserIdentity.Name.ToString(), NameReport , "",ex.ToString(), MapPath("LOG.xml"));
            }
        }
    }
}