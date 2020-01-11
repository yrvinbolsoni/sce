using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SCE.DAL
{
    public class ReportsDB
    {


        public DataTable BuscaRelatorio(string sql , string cs)
        {
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
                   //log
                }
                finally
                {
                    conn.Close();
                }
            }

            return result;
        }
    }
}