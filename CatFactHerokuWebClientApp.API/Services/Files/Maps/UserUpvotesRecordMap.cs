using CatFactHerokuWebClientApp.Web.Domain;
using CsvHelper.Configuration;
using System.Globalization;

namespace CatFactHerokuApp.Infrastructure.Files.Maps
{
    public class UserUpvotesRecordMap : ClassMap<UserUpvotes>
    {
        public UserUpvotesRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.UserName).Name("User Name");
            Map(m => m.Upvotes).Name("Total Upvotes");
        }
    }
}
