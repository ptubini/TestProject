using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using TestProject.Application.Contracts;

namespace TestProject.Application.Services
{
    public class CsvService : ICsvService
    {
        public IEnumerable<T> ReadCSV<T>(string fileLocation)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
            };

            StreamReader reader = new StreamReader(fileLocation);
            CsvReader csv = new CsvReader(reader, config);

            return csv.GetRecords<T>();
        }
    }
}
