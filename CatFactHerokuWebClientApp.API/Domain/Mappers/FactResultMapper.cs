using System.Collections.Generic;
using System.Linq;
using CatFactHerokuWebClientApp.Web.External.Models;

namespace CatFactHerokuWebClientApp.Web.Domain.Mappers
{
    public class FactResultMapper : IMapper<FactResult, All>
    {
        public FactResult ToEntity(All factApiResult)
        {
            return new FactResult
            {
                Id = factApiResult.Id,
                Text = factApiResult.Text,
                Type = factApiResult.Type,
                User = new UserData
                {
                    Id = (factApiResult.User?.Id)??string.Empty,
                    FirstName = (factApiResult.User?.Name?.First)??string.Empty,
                    LastName = (factApiResult.User?.Name?.Last)??string.Empty,
                },
                Upvotes = factApiResult.Upvotes,
                UserUpvoted = factApiResult.UserUpvoted

            };
        }

        public All ToDto(FactResult factResult)
        {
            throw new System.NotImplementedException();

        }

        public void ToEntity(All factApiResult, FactResult factResult)
        {

            throw new System.NotImplementedException();

        }

        public IEnumerable<All> ToDto(IEnumerable<FactResult> factResults)
        {
            throw new System.NotImplementedException();

        }

        public IEnumerable<FactResult> ToEntity(IEnumerable<All> dtos)
        {
            return dtos?.Select(ToEntity)?.ToList();
        }
    }
}
