using System;

namespace CatFactHerokuWebClientApp.Web.Domain
{
    public class FactResult
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public UserData User { get; set; }
        public int Upvotes { get; set; }
        public string UserUpvoted { get; set; }
    }
    
}
