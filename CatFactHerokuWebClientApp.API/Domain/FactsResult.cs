using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatFactHerokuWebClientApp.Web.Domain
{
    public class FactsResult
    {
        public IEnumerable<FactResult> FactResultList { get; set; }
    }
}
