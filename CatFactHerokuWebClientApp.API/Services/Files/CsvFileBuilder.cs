using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CatFactHerokuWebClientApp.Web.Domain;
using CsvHelper;

namespace CatFactHerokuWebClientApp.Web.Services.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildUserUpvotesFile(IEnumerable<UserUpvotes> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteHeader<UserUpvotes>();
                csvWriter.NextRecord();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
