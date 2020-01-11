using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Collections.Generic;
using Npgsql;
using System.Configuration;
using System.Linq;

namespace SCE
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //GetChartData();
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] LoadCards()
        {
            string sql = "select * from mtv_load_cards_2019";


            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphics(sql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_service", "Total");
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] CommoditGraphic()
        {
            string sql = "select * from mtv_commodity_2019";

            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphics(sql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "commodity", "teu");
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] ColunaPorMesGraphic()
        {
            string sql = " Select * from mtv_historico_mensal_2019 ";


            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphics(sql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "dmonth", "TOTAL");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] MapCountriesGraphic()
        {
            string sql = "select * from mtv_mapa_2019 ";



            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphics(sql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_country", "teu");
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] ArmadorExpGraphic()
        {
            string sql = " select * from mtv_exportacao_2019 ";


            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphics(sql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_carrier", "total");
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] ArmadorImpGraphic()
        {
            string sql = " select * from mtv_importacao_2019 ";


            // dados da consuta SQL retornando um DataTable
            DataTable DadosGraphic = BuildGraphics(sql);
            // retorna um  objeto[] anonimo 
            return BuildJason(DadosGraphic, "fk_carrier", "total");
        }


        // cria o jason
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
                "Country",
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


        public static DataTable BuildGraphics(string sql)
        {
            string cs = ConfigurationManager.AppSettings["BancoProducao"];

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, cs);
            DataTable ds = new DataTable();
            ds.Reset();
            da.Fill(ds);

            return ds;
        }

        class GoogleChartData
        {
            public string Descricao { get; set; }
            public int Valor { get; set; }
            public string Cor { get; set; }
        }

    }

}

//www.codigomaster.com.br/desenvolvimento/como-exportar-html-para-pdf-em-c-e-itextsharp 