using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;


namespace SCE.DAO
{
    // responsavel por conteudo dos dropdown list  e pesquisas entre dropdown list
    public class DropDownListDados
    {
        // dados de configuração
        private static string CS = ConfigurationManager.AppSettings["BancoProducao"];

        #region Busca De dados 

        // busca de dados SEMPRE VAI FECHAR A CONEXÃO 
        // Não vejo nescessidade de colcoar um log aqui 
        public DataSet BuscaDadosDropDown(string sql)
        {
            DataSet result = new DataSet();
            using (NpgsqlConnection conn = new NpgsqlConnection(CS))
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
            result.Dispose();
            return result;
        }

        #endregion

        #region Metodos De todos os DropDown com SQL
        public DataSet ListPort() //comb_port
        {
            string sql = "SELECT  tbl_portos.lporto , lcode FROM tbl_portos ORDER BY tbl_portos.lporto; ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListYear()
        {
            string sql = "SELECT year FROM tbl_year order by year desc ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListDirection() //comb_cod 
        {
            string sql = "SELECT  (tbl_movimentos.cod  || ' | ' || tbl_movimentos.direction ) as text , tbl_movimentos.cod , direction FROM tbl_movimentos;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListTipoCliente() //list_tipo  -Cliente
        {
            string sql = "SELECT  ( tbl_tipo_cliente.tipo || ' | ' || tbl_tipo_cliente.remarks ) as text , tbl_tipo_cliente.tipo FROM tbl_tipo_cliente;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet List_carregamento() // list_mode tipo de carregamento 
        {
            string sql = "SELECT  ( tbl_mode.mode || ' | ' ||  tbl_mode.remarks ) as text , tbl_mode.mode FROM tbl_mode;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListTipoContainer() //list_container  tipo de container
        {
            string sql = "SELECT ( tbl_containers.tcontainer || ' | ' || tbl_containers.dcontainer) as text , tbl_containers.tcontainer FROM tbl_containers GROUP BY tbl_containers.tcontainer, tbl_containers.dcontainer; ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListRestricao() //list_container  tipo de container
        {
            string sql = "SELECT ( tbl_problems.problem_code || ' | ' || tbl_problems.problem_description) as text , tbl_problems.problem_code FROM tbl_problems; ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListRotas()  //comb_serv1 service /rotes    segunda aba 
        {
            string sql = "SELECT ( tbl_services.service || ' | ' ||   tbl_services.dservice) as text , tbl_services.service FROM tbl_services ORDER BY tbl_services.service;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }
        public DataSet ListAreas() // list_filtro_1     aeras  segunda aba
        {
            string sql = "SELECT ( vw_destino.fk_area || ' | ' ||  vw_destino.darea ) as text , vw_destino.fk_area , vw_destino.fk_service  FROM vw_destino  GROUP BY vw_destino.fk_area, vw_destino.darea, vw_destino.fk_service ORDER BY vw_destino.fk_service;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListRegiao()  // lista de região  segunda aba
        {
            string sql = "SELECT ( vw_destino.fk_region || ' | ' ||  vw_destino.dregion ) as text ,  vw_destino.fk_region  FROM vw_destino  GROUP BY vw_destino.fk_region, vw_destino.dregion, vw_destino.fk_area  ORDER BY vw_destino.fk_area; ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListPais()  // List_coutry   segunda aba
        {
            string sql = "SELECT (vw_destino.fk_country || ' | ' || vw_destino.fk_region) as text , vw_destino.fk_country FROM vw_destino GROUP BY vw_destino.fk_country, vw_destino.fk_region ORDER BY vw_destino.fk_country; ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListPortsDePaises()  // List_coutry   segunda aba
        {
            string sql = " SELECT ( vw_destino.odporto || ' | ' ||  vw_destino.fk_country )as text , vw_destino.odporto "
               + " FROM vw_destino "
               + " GROUP BY vw_destino.odporto, vw_destino.fk_country, vw_destino.fk_region "
               + " ORDER BY vw_destino.odporto LIMIT 100;  ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListTransportadora()  // list_carrier_1   segunda aba
        {
            string sql = "SELECT   tbl_carriers.carrier  FROM tbl_carriers  ORDER BY tbl_carriers.carrier;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListMercadoria()  // list_commodity_1   segunda aba
        {
            string sql = "SELECT tbl_commodity.commodity FROM tbl_commodity;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListVendedores()  // list_srep_1   segunda aba
        {
            string sql = "SELECT (tbl_vendedores.srep || ' | ' ||  tbl_vendedores.vendedor) as text , tbl_vendedores.srep FROM tbl_vendedores ORDER BY tbl_vendedores.srep;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListCliente()  // list_srep_1   segunda aba
        {
            string sql = "SELECT cliente FROM tbl_clientes LIMIT 100;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }


        public DataSet ListClienteM()  // Form Manifesto
        {
            string sql = "SELECT DISTINCT cliente FROM tbl_actual order by cliente asc  LIMIT 100;";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListOwners()  //Form Manifesto    1 aba
        {
            string sql = "SELECT(code || ' | ' || armador) as text , code FROM tbl_armador";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }



        public DataSet ListCommodityM()  //Form Manifesto Commodity   1 aba
        {
            string sql = "SELECT(tcommodity  || ' | ' || d_commoditity) as text , tcommodity FROM tbl_type_commodity";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListSling()  //Form Manifesto Commodity   1 aba
        {
            string sql = "SELECT  (sling  || ' | ' || fk_service )as text , sling  FROM tbl_sling  ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListClientPesquisaM(string v)  // list_srep_1   segunda aba
        {
            string sql = "SELECT DISTINCT cliente FROM tbl_actual WHERE cliente Like '%" + v + "%' LIMIT 100";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListVendedoresPesquisa(string v)  // list_srep_1   segunda aba
        {
            string sql = "SELECT cliente FROM tbl_clientes WHERE cliente Like '%" + v + "%' LIMIT 100";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }


        public DataSet PesquisaPaisList(string v)  // list_srep_1   segunda aba
        {
            string sql = "SELECT  (vw_destino.fk_country || ' | ' || vw_destino.fk_region) as text , vw_destino.fk_country FROM vw_destino WHERE fk_country Like '%" + v + "%' GROUP BY vw_destino.fk_country, vw_destino.fk_region ORDER BY vw_destino.fk_country limit 100";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet PesquisaPaisPort(string v)  // list_srep_1   segunda aba
        {
            string sql = "SELECT (odporto || ' | ' || fk_region ) as text , odporto FROM  tbl_destino WHERE odporto Like '%" + v + "%' LIMIT 100 ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet PesquisaCommodity(string v)  // list_srep_1   segunda aba
        {
            string sql = " SELECT commodity , tbl_commodity.hs4 FROM tbl_commodity WHERE commodity Like '%" + v + "%' ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListAreasDinamic(string v)
        {
            string sql = " SELECT (vw_destino.fk_area || ' | ' ||  vw_destino.darea ) as text , vw_destino.fk_area , vw_destino.fk_service FROM vw_destino  WHERE vw_destino.fk_service in (" + v + ") " +
                        "  GROUP BY vw_destino.fk_area, vw_destino.darea, vw_destino.fk_service " +
                        "  ORDER BY vw_destino.fk_area; ";
            DataSet dados = BuscaDadosDropDown(sql);
            if (dados.Tables[0].Rows.Count == 0)
            {
                return ListAreas();
            }
            else
            {
                return dados;
            }
        }

        public DataSet ListSlingDinamic(string v)
        {
            string sql = " SELECT(sling || ' | ' || fk_service) as text,  sling  FROM tbl_sling where fk_service in(" + v + ") ";
            DataSet dados = BuscaDadosDropDown(sql);
            return dados;
        }

        public DataSet ListARegiaoDinamic(string v)
        {
            string sql = "SELECT ( vw_destino.fk_region || ' | ' || vw_destino.dregion ) as text , vw_destino.fk_region, vw_destino.fk_area " +
                          " FROM vw_destino " +
                          " WHERE vw_destino.fk_area in (" + v + ")" +
                          " GROUP BY vw_destino.fk_region, vw_destino.dregion, vw_destino.fk_area" +
                          " ORDER BY vw_destino.fk_region;";
            DataSet dados = BuscaDadosDropDown(sql);
            if (dados.Tables[0].Rows.Count == 0)
            {
                return ListRegiao();
            }
            else
            {
                return dados;
            }
        }

        public DataSet ListPaisDinamic(string v)
        {
            string sql = "SELECT ( vw_destino.fk_country || ' | ' || vw_destino.fk_region ) as text , vw_destino.fk_country   " +
                         " FROM vw_destino " +
                         " WHERE vw_destino.fk_region in (" + v + ")" +
                         " GROUP BY vw_destino.fk_country, vw_destino.fk_region" +
                         " ORDER BY vw_destino.fk_country;";
            DataSet dados = BuscaDadosDropDown(sql);
            if (dados.Tables[0].Rows.Count == 0)
            {
                return ListPais();
            }
            else
            {
                return dados;
            }
        }

        public DataSet ListPaisPortDinamic(string v)
        {
            string sql = " SELECT ( vw_destino.odporto || ' | ' || vw_destino.fk_country) AS text , vw_destino.odporto , vw_destino.fk_region " +
                         " FROM vw_destino " +
                         " WHERE vw_destino.fk_country in (" + v + ") " +
                         " GROUP BY vw_destino.odporto, vw_destino.fk_country, vw_destino.fk_region " +
                         " ORDER BY vw_destino.odporto;";
            DataSet dados = BuscaDadosDropDown(sql);
            if (dados.Tables[0].Rows.Count == 0)
            {
                return ListPortsDePaises();
            }
            else
            {
                return dados;
            }
        }
        #endregion

    }
}