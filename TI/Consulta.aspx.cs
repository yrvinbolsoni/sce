using Npgsql;
using SCE.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCE.TI
{
    public partial class Consulta : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Error.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Util utl = new Util();
            try
            {
                Error.Visible = false;
                TableQuerry.DataSource = utl.Consultabanco(Parametro.Text);
                TableQuerry.DataBind();
            }
            catch (Exception ex)
            {
                Error.Visible = true;
                Error.InnerHtml = utl.MessageErrorAdm(ex.ToString());
            }
        }
    }
}