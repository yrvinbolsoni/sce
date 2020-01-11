using SCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static SCE.TI.LogM;

namespace SCE.TI
{
    public partial class GraficosLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlParaObjeto();
        }

        public static List<XmlLogMovimentacao> XmlParaObjeto()
        {
            List<XmlLogMovimentacao> LogMov = new List<XmlLogMovimentacao>();

            XDocument xmlDoc = XDocument.Load(HostingEnvironment.MapPath("~/Ti/LogMovi.xml"));
            var user = from users in xmlDoc.Descendants("log")
                       select new
                       {
                           usuario = users.Element("Usuario").Value,
                           NomeRelatorio = users.Element("NomeRelatorio").Value,
                           Data = users.Element("Data").Value,
                           Hora = users.Element("Hora").Value,
                           IP = users.Element("IP").Value,
                           Sql = users.Element("Sql").Value,
                       };

            foreach (var u in user)
            {
                LogMov.Add(new XmlLogMovimentacao
                {
                    usuario = u.usuario,
                    NomeRelatorio = u.NomeRelatorio,
                    Data = u.Data,
                    Hora = u.Hora,
                    IP = u.IP,
                    Sql = u.Sql,
                });
            };

            return LogMov.ToList();
        }


        private static object[] BuildJasonUser()
        {
            List<GoogleChartData> GraphicJason = new List<GoogleChartData>();

            List<XmlLogMovimentacao> DadosGraphic = XmlParaObjeto();


            //select * count from tabela group by usuario
            var User = (from l in DadosGraphic
                        group l by l.usuario into g
                        select new
                        {
                            desc = g.Key,
                            sum = g.Count()
                        });

            var ListaOrdenada = User.OrderByDescending(x => x.sum).Take(10);



            foreach (var dado in ListaOrdenada)
            {
                GraphicJason.Add(new GoogleChartData
                {
                    Descricao = dado.desc,
                    Valor = dado.sum
                });

            }

            GraphicJason = GraphicJason.ToList();

            var chartData = new object[GraphicJason.Count + 1];
            chartData[0] = new object[]{
                "Total",
                "Total Relatorio",
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
        public static object[] UserRelatorio()
        {
            // retorna um  objeto[] anonimo 
            return BuildJasonUser();
        }

        private static object[] RelatorioMaisUsado()
        {
            List<GoogleChartData> GraphicJason = new List<GoogleChartData>();

            List<XmlLogMovimentacao> DadosGraphic = XmlParaObjeto();


            //select * count from tabela group by usuario
            var User = (from l in DadosGraphic
                        group l by l.NomeRelatorio into g
                        select new
                        {
                            desc = g.Key,
                            sum = g.Count()
                        }
                        );

            var ListaOrdenada = User.OrderByDescending(x => x.sum).Take(10);




            foreach (var dado in ListaOrdenada)
            {
                GraphicJason.Add(new GoogleChartData
                {
                    Descricao = dado.desc,
                    Valor = dado.sum
                });

            }

            GraphicJason = GraphicJason.ToList();

            var chartData = new object[GraphicJason.Count + 1];
            chartData[0] = new object[]{
                "Total",
                "Total Relatorio",
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
        public static object[] Relatorio()
        {
            // retorna um  objeto[] anonimo 
            return RelatorioMaisUsado();
        }

    }
}