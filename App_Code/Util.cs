using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace SCE.DAO
{
    public class Util
    // regras sql add XML consuntas administrativa 
    {
        private static string CS = ConfigurationManager.AppSettings["BancoProducao"];
        public static NpgsqlConnection conn { get; set; }
        public DataSet Consultabanco(string p) // Consunta banco ADM
        {
            conn = new NpgsqlConnection(CS); ;
            conn.Open();

            string sql = p;
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            ds.Reset();
            da.Fill(ds);

            conn.Close();
            return ds;
        }
        public string MarketStatiscsParametersQuery(string YearMs , string StartMonth, string FinalMonth, string listport, string Direction, string listClient, string ListCarregamento,
                                        string ListContainer, string ListRotas, string ListAreas, string ListRegion, string ListPais, string ListPortsPais,
                                        string ListCarrier, string ListCommodity, string ListClients, string ListSalesRep, string ListRestricoes)
        {
            string rulesSql = " WHERE year = " + YearMs + " ";

            if (StartMonth == "" || FinalMonth == "")
            {
            }
            else
            { rulesSql += "AND(fk_month BETWEEN " + StartMonth + " AND " + FinalMonth + ") "; }
            //=============================================================================================

            if (listport == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND fk_lporto In (" + listport + ")"; }
            //=============================================================================================
            if (Direction == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  cod In(" + Direction + ") "; }
            //=============================================================================================
            if (listClient == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_tipo In(" + listClient + ") "; }
            //=============================================================================================
            if (ListCarregamento == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_mode In(" + ListCarregamento + ") "; }
            //=============================================================================================
            if (ListContainer == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_tcontainer In(" + ListContainer + ")"; }


            //ABA 2 
            //=============================================================================================
            if (ListRotas == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_service In(" + ListRotas + ") "; }
            //=============================================================================================
            if (ListAreas == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_area In(" + ListAreas + ") "; }
            //=============================================================================================
            if (ListRegion == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_region In(" + ListRegion + ") "; }
            //=============================================================================================
            if (ListPais == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_country In (" + ListPais + ") "; }
            //=============================================================================================
            if (ListPortsPais == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND  fk_odporto In(" + ListPortsPais + ") "; }
            //============================================================================================

            // ABA 3 
            if (ListCarrier == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND  fk_carrier In(" + ListCarrier + ") "; }
            //============================================================================================
            if (ListCommodity == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND commodity In(" + ListCommodity + ")"; }

            //============================================================================================
            if (ListClients == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND fk_cliente In(" + ListClients + ")"; }
            //============================================================================================
            if (ListSalesRep == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_srep In(" + ListSalesRep + ")"; }
            //============================================================================================
            if (ListRestricoes == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND fk_problem In(" + ListRestricoes + ")"; }

            if (rulesSql == " ")
            {
                rulesSql = "#Erro";
                return rulesSql;

            }
            else
            {
                return rulesSql;
            }
        }

        public string MarketStatiscsParametersQuery(string YearMs, string StartMonth, string FinalMonth, string listport, string Direction, string listClient, string ListCarregamento,
                                      string ListContainer, string ListRotas, string ListAreas, string ListRegion, string ListPais, string ListPortsPais,
                                      string ListCarrier, string ListCommodity, string ListClients, string ListSalesRep, string ListRestricoes, string nameReport)
        {
            string rulesSql = " WHERE a.year = " + YearMs + " ";

            if (StartMonth == "" || FinalMonth == "")
            {
            }
            else
            {
                rulesSql += "AND( a.fk_month BETWEEN " + StartMonth + " AND " + FinalMonth + ") ";
            }
            //=============================================================================================

            if (listport == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += "AND a.fk_lporto In (" + listport + ")";
            }
            //=============================================================================================
            if (Direction == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.cod In(" + Direction + ") ";
            }
            //=============================================================================================
            if (listClient == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.fk_tipo In(" + listClient + ") ";
            }
            //=============================================================================================
            if (ListCarregamento == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.fk_mode In(" + ListCarregamento + ") ";
            }
            //=============================================================================================
            if (ListContainer == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.fk_tcontainer In(" + ListContainer + ")";
            }


            //ABA 2 
            //=============================================================================================
            if (ListRotas == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.fk_service In(" + ListRotas + ") ";
            }
            //=============================================================================================
            if (ListAreas == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.fk_area In(" + ListAreas + ") ";
            }
            //=============================================================================================
            if (ListRegion == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.fk_region In(" + ListRegion + ") ";
            }
            //=============================================================================================
            if (ListPais == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.fk_country In (" + ListPais + ") ";
            }
            //=============================================================================================
            if (ListPortsPais == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += "AND  a.fk_odporto In(" + ListPortsPais + ") ";
            }
            //============================================================================================

            // ABA 3 
            if (ListCarrier == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += "AND  a.fk_carrier In(" + ListCarrier + ") ";
            }
            //============================================================================================
            if (ListCommodity == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND a.commodity In(" + ListCommodity + ")";
            }

            //============================================================================================
            if (ListClients == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND a.fk_cliente In(" + ListClients + ")";
            }
            //============================================================================================
            if (ListSalesRep == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND  a.fk_srep In(" + ListSalesRep + ")";
            }
            //============================================================================================
            if (ListRestricoes == "''" || listport == null)
            {
            }
            else
            {
                rulesSql += " AND a.fk_problem In(" + ListRestricoes + ")";
            }

            if (rulesSql == " ")
            {
                rulesSql = "#Erro";
                return rulesSql;

            }
            else
            {
                return rulesSql;
            }
        }


        public string ParametersQuery(string StartMonth , string FinalMonth , string listport , string Direction  , string listClient , string ListCarregamento ,
                                        string ListContainer  , string ListRotas , string ListAreas , string ListRegion , string ListPais , string ListPortsPais ,
                                        string ListCarrier , string ListCommodity , string ListClients , string ListSalesRep , string ListRestricoes)
         {
            string rulesSql =" ";

            if (StartMonth == "" || FinalMonth == "")
            {
            }
            else
            { rulesSql += "AND(fk_month BETWEEN " + StartMonth + " AND " + FinalMonth + ") "; }
            //=============================================================================================

            if (listport == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND fk_lporto In (" + listport + ")"; }
            //=============================================================================================
            if (Direction == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  cod In(" + Direction + ") "; }
            //=============================================================================================
            if (listClient == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_tipo In(" + listClient + ") "; }
            //=============================================================================================
            if (ListCarregamento == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_mode In(" + ListCarregamento + ") "; }
            //=============================================================================================
            if (ListContainer == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_tcontainer In(" + ListContainer + ")"; }


            //ABA 2 
            //=============================================================================================
            if (ListRotas == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_service In(" + ListRotas + ") "; }
            //=============================================================================================
            if (ListAreas == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_area In(" + ListAreas + ") "; }
            //=============================================================================================
            if (ListRegion == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_region In(" + ListRegion + ") "; }
            //=============================================================================================
            if (ListPais == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_country In (" + ListPais + ") "; }
            //=============================================================================================
            if (ListPortsPais == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND  fk_odporto In(" + ListPortsPais + ") "; }
            //============================================================================================

            // ABA 3 
            if (ListCarrier == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND  fk_carrier In(" + ListCarrier + ") "; }
            //============================================================================================
            if (ListCommodity == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND commodity In(" + ListCommodity + ")"; }

            //============================================================================================
            if (ListClients == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND fk_cliente In(" + ListClients + ")"; }
            //============================================================================================
            if (ListSalesRep == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_srep In(" + ListSalesRep + ")"; }
            //============================================================================================
            if (ListRestricoes == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND fk_problem In(" + ListRestricoes + ")"; }

            if (rulesSql == " ")
            {
                rulesSql = "#Erro";
                return rulesSql;

            }
            else
            {
                return rulesSql;
            }
        }

        public string ParametersQueryManifest(string YearM , string StartMonthM, string FinalMonthM, string listportM, string DirectionM ,
                                               string ListRotasM, string ListAreasM, string ListSlingM, string ListCommodityM, string ListOwnersM)
        {

            string rulesSql = " WHERE year = " + YearM + " ";

            

            if (StartMonthM == "" || FinalMonthM == "")
            {
            }
            else
            { rulesSql += "AND(fk_month BETWEEN " + StartMonthM + " AND " + FinalMonthM + ") "; }
            //=============================================================================================

            if (listportM == "''" || listportM == null)
            {
            }
            else
            { rulesSql += "AND fk_bporto In (" + listportM + ")"; }
            //=============================================================================================
            if (DirectionM == "''" || listportM == null)
            {
            }
            else
            { rulesSql += " AND  direction In(" + DirectionM.ToUpper() + ") "; }
            //=============================================================================================
            

            //ABA 2 
            //=============================================================================================
            if (ListRotasM == "''" || listportM == null)
            {
            }
            else
            { rulesSql += " AND  fk_service In(" + ListRotasM + ") "; }
            //=============================================================================================
            if (ListAreasM == "''" || listportM == null)
            {
            }
            else
            { rulesSql += " AND  fk_area In(" + ListAreasM + ") "; }
            //=============================================================================================
            if (ListSlingM == "''" || listportM == null)
            {
            }
            else
            { rulesSql += " AND  fk_sling In(" + ListSlingM + ") "; }
            //=============================================================================================
            if (ListCommodityM == "''" || listportM == null)
            {
            }
            else
            { rulesSql += " AND  fk_tcommodity In (" + ListCommodityM + ") "; }
            //=============================================================================================
            if (ListOwnersM == "''" || listportM == null)
            {
            }
            else
            { rulesSql += "AND  owner In(" + ListOwnersM + ") "; }
            //============================================================================================

           
                return rulesSql;
           
        }


        public string GetIPAddress()
        {
            HttpContext context = HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        //log de movimentação
        public void addLog(string nomeUser, string NomeRelatorio, string sql , string sXMLFile)
        {
            string data = DateTime.Now.ToShortDateString();
            string Hora = DateTime.Now.ToShortTimeString();
            string ip = GetIPAddress();


            DataSet ds = new DataSet();
            ds.ReadXml(sXMLFile);
            if (ds.Tables.Count == 0)
            {
                DataTable dt = new DataTable("log");
                dt.Columns.Add("Usuario");
                dt.Columns.Add("NomeRelatorio");
                dt.Columns.Add("Data");
                dt.Columns.Add("Hora");
                dt.Columns.Add("Ip");
                dt.Columns.Add("Sql");
                ds.Tables.Add(dt);
            }
            DataRow dRow = ds.Tables[0].NewRow();
            dRow["Usuario"] = nomeUser;
            dRow["NomeRelatorio"] = NomeRelatorio;
            dRow["Data"] = data;
            dRow["Hora"] = Hora;
            dRow["IP"] = ip;
            dRow["Sql"] = sql;

            ds.Tables["log"].Rows.Add(dRow);
            ds.WriteXml(sXMLFile);
        }

        // log erro
        public void addLog(string nomeUser, string NomeRelatorio, string sql , string error, string sXMLFile)
        {
            string data = DateTime.Now.ToShortDateString();
            string Hora = DateTime.Now.ToShortTimeString();
            string ip = GetIPAddress();

            DataSet ds = new DataSet();
            ds.ReadXml(sXMLFile);
            if (ds.Tables.Count == 0)
            {
                DataTable dt = new DataTable("log");
                dt.Columns.Add("Usuario");
                dt.Columns.Add("NomeRelatorio");
                dt.Columns.Add("Data");
                dt.Columns.Add("Hora");
                dt.Columns.Add("Ip");
                dt.Columns.Add("Erro");
                ds.Tables.Add(dt);
            }
            DataRow dRow = ds.Tables[0].NewRow();
            dRow["Usuario"] = nomeUser;
            dRow["NomeRelatorio"] = NomeRelatorio;
            dRow["Data"] = data;
            dRow["Hora"] = Hora;
            dRow["IP"] = ip;
            dRow["Erro"] = error;
            ds.Tables["log"].Rows.Add(dRow);
            ds.WriteXml(sXMLFile);
        }

        public string MessageError()
        {
            string Message = "<div class='alert alert-dismissible alert-danger' style='background-image:linear-gradient(to bottom,#ff0000 0,#dc4949  100%)'>" +
                    "<button type = 'button' class='close' data-dismiss='alert' style='color: #000000;' >&times;</button>" +
                   "<p style = 'color: #ffffff;' > Nada encontrado , tente novamente com mais parametros</p>" +
                   "</div>";

            return Message;
        }

        public string MessageErrorAdm(string p)
        {
            string Message = "<div class='alert alert-dismissible alert-danger' style='background-image:linear-gradient(to bottom,#ff0000 0,#dc4949  100%)'>" +
                    "<button type = 'button' class='close' data-dismiss='alert' style='color: #000000;' >&times;</button>" +
                   "<p style = 'color: #ffffff;' >"+p+"</p>" +
                   "</div>";

            return Message;
        }

        public static string GraphicMarketStatiscsParametersQuery(string YearMs, string StartMonth, string FinalMonth, string listport, string Direction, string listClient, string ListCarregamento,
                                    string ListContainer, string ListRotas, string ListAreas, string ListRegion, string ListPais, string ListPortsPais,
                                    string ListCarrier, string ListCommodity, string ListClients, string ListSalesRep, string ListRestricoes)
        {
            string rulesSql = " WHERE year = " + YearMs + " ";

            if (StartMonth == "" || FinalMonth == "")
            {
            }
            else
            { rulesSql += "AND(fk_month BETWEEN " + StartMonth + " AND " + FinalMonth + ") "; }
            //=============================================================================================

            if (listport == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND fk_lporto In (" + listport + ")"; }
            //=============================================================================================
            if (Direction == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  cod In(" + Direction + ") "; }
            //=============================================================================================
            if (listClient == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_tipo In(" + listClient + ") "; }
            //=============================================================================================
            if (ListCarregamento == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_mode In(" + ListCarregamento + ") "; }
            //=============================================================================================
            if (ListContainer == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_tcontainer In(" + ListContainer + ")"; }


            //ABA 2 
            //=============================================================================================
            if (ListRotas == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_service In(" + ListRotas + ") "; }
            //=============================================================================================
            if (ListAreas == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_area In(" + ListAreas + ") "; }
            //=============================================================================================
            if (ListRegion == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_region In(" + ListRegion + ") "; }
            //=============================================================================================
            if (ListPais == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_country In (" + ListPais + ") "; }
            //=============================================================================================
            if (ListPortsPais == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND  fk_odporto In(" + ListPortsPais + ") "; }
            //============================================================================================

            // ABA 3 
            if (ListCarrier == "''" || listport == null)
            {
            }
            else
            { rulesSql += "AND  fk_carrier In(" + ListCarrier + ") "; }
            //============================================================================================
            if (ListCommodity == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND commodity In(" + ListCommodity + ")"; }

            //============================================================================================
            if (ListClients == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND fk_cliente In(" + ListClients + ")"; }
            //============================================================================================
            if (ListSalesRep == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND  fk_srep In(" + ListSalesRep + ")"; }
            //============================================================================================
            if (ListRestricoes == "''" || listport == null)
            {
            }
            else
            { rulesSql += " AND fk_problem In(" + ListRestricoes + ")"; }

            if (rulesSql == " ")
            {
                rulesSql = "#Erro";
                return rulesSql;

            }
            else
            {
                return rulesSql;
            }
        }

    }
}