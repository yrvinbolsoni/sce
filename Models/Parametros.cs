using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCE.Models
{
    public class Parametros
    {
        public  string year { get; set; }
        public  string StartMonth { get; set; }
        public  string FinalMonth { get; set; }

        public  string listport { get; set; }
        public  string Direction { get; set; }
        public  string listClient { get; set; }
        public  string ListCarregamento { get; set; }
        public  string ListContainer { get; set; }
        public  string ListRestricoes { get; set; }

        public  string ListRotas { get; set; }
        public  string ListAreas { get; set; }
        public  string ListRegion { get; set; }
        public  string ListPais { get; set; }
        public  string ListPortsPais { get; set; }
        public  string ListCarrier { get; set; }

        public  string ListCommodity { get; set; }
        public  string ListSalesRep { get; set; }
        public  string ListClients { get; set; }
    }
}