using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SCE.DAO
{
    public class ResumoBasil
    {
        public DataSet Destino()
        {
            string CS = "Server=172.16.7.5;Port=5432;User Id=postgres;Password=cooler01;Database=sce;";
            NpgsqlConnection conn = new NpgsqlConnection(CS);
            conn.Open();
            string sql = "SELECT odporto as Porto , fk_country as Estrangeira , fk_area as Area , fk_service as Servico   from vw_destino LIMIT 21";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            ds.Reset();
            da.Fill(ds);
            return ds;

             
        }
    }

    
    
} 