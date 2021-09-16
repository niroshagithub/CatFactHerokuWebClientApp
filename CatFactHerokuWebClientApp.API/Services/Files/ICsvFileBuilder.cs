using System.Collections.Generic;
using CatFactHerokuWebClientApp.Web.Domain;

namespace CatFactHerokuWebClientApp.Web.Services.Files
{
    public interface ICsvFileBuilder
    {
        byte[] BuildUserUpvotesFile(IEnumerable<UserUpvotes> records);
    }
}