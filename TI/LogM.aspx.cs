using SCE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SCE.TI
{
    public partial class LogM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cabecalho.Visible = false;
        }

        public class XmlLogMovimentacao
        {
            public string usuario { get; set; }
            public string NomeRelatorio { get; set; }
            public string Data { get; set; }
            public string Hora { get; set; }
            public string IP { get; set; }
            public string Sql { get; set; }
        }

     

        protected void butGetXML_Click(object sender, EventArgs e)
        {

            XDocument xmlDoc = XDocument.Load(Server.MapPath("~/Ti/LogMovi.xml"));
            string corpo = "";
            var clientes = from cliente in xmlDoc.Descendants("log")
                           select new
                           {
                               usuario = cliente.Element("Usuario").Value,
                               compania = cliente.Element("NomeRelatorio").Value,
                               departamento = cliente.Element("Data").Value,
                               nomeArquivo = cliente.Element("Hora").Value,
                               Data = cliente.Element("IP").Value,
                               Sql = cliente.Element("Sql").Value,

                           };

            litXMLDados.Text = "";
            foreach (var cliente in clientes)
            {
                corpo += "<tr> " +
                                              "<td>" + cliente.usuario + "</td>" +
                                              "<td> " + cliente.compania + "  </td>" +
                                              "<td> " + cliente.departamento + "  </td>" +
                                              "<td> " + cliente.nomeArquivo + "  </td>" +
                                              "<td> " + cliente.Data + "  </td>" +
                                              "<td> " + cliente.Sql + "  </td>" +
                                         "</tr>";
            }
            Cabecalho.Visible = true;
            CorpoTabela.InnerHtml = corpo;

            if (corpo == "")
                litXMLDados.Text = "Nada encontrado.";
        }

        protected void butFiltraXML_Click(object sender, EventArgs e)
        {
            try
            {

                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/Ti/LogMovi.xml"));
                string data = Convert.ToDateTime(DataForm.Value).ToString("dd/MM/yyy");
                string corpoPesquisa = "";

                var clientes = from cliente in xmlDoc.Descendants("log")
                               where cliente.Element("Data").Value == data
                               select new
                               {
                                   usuario = cliente.Element("Usuario").Value,
                                   compania = cliente.Element("NomeRelatorio").Value,
                                   departamento = cliente.Element("Data").Value,
                                   nomeArquivo = cliente.Element("Hora").Value,
                                   Data = cliente.Element("IP").Value,
                                   Sql = cliente.Element("Sql").Value,


                               };

                litXMLDados.Text = "";

                foreach (var cliente in clientes)
                {
                    corpoPesquisa += "<tr> " +
                                              "<td>" + cliente.usuario + "</td>" +
                                              "<td> " + cliente.compania + "  </td>" +
                                              "<td> " + cliente.departamento + "  </td>" +
                                              "<td> " + cliente.nomeArquivo + "  </td>" +
                                              "<td> " + cliente.Data + "  </td>" +
                                              "<td> " + cliente.Sql + "  </td>" +

                                         "</tr>";
                }

                Cabecalho.Visible = true;
                CorpoTabela.InnerHtml = corpoPesquisa;

                if (corpoPesquisa == "")
                {
                    Cabecalho.Visible = false;

                    litXMLDados.Text = "Nada encontrado.";
                }
            }
            catch (Exception ex)
            {
                litXMLDados.Text = ex.ToString() + " insira uma data";
            }
        }
    }
}