using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatFactHerokuWebClientApp.Web.Domain
{
    public class UserUpvotes
    {
        public string UserName { get; set; }
        public int Upvotes { get; set; }
    }
}
