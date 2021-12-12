using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma3.Objects
{
    public class HomePageModel
    {
        public List<SecuritiesModel> MostActive { get; set; } = new List<SecuritiesModel>();
        public List<SecuritiesModel> TopGainers { get; set; } = new List<SecuritiesModel>();
        public List<SecuritiesModel> TopLosers { get; set; } = new List<SecuritiesModel>();
        public List<SecuritiesModel> Crypto { get; set; } = new List<SecuritiesModel>();

        public HomePageModel(List<SecuritiesModel> a, List<SecuritiesModel> g, List<SecuritiesModel> l, List<SecuritiesModel> c)
        {
            Crypto = c;
            MostActive = a;
            TopGainers = g;
            TopLosers = l;
        }
    }
}
