namespace TestProject.Application.Contracts
{
    public interface ICsvService
    {
        public IEnumerable<T> ReadCSV<T>(string fileLocation);
    }
}
