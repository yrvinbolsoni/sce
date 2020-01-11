using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCE
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NomeUser.Text = Request.LogonUserIdentity.Name.ToString().Replace(@"VMWEB\", " ");  
        }
    }
}