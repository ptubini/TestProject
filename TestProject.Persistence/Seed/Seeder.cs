using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestProject.Application.Contracts;
using TestProject.Domain;

namespace TestProject.Persistence.Seed
{
    public static class Seeder
    {
        public static void SeedLocations(DbContext context, ICsvService csvService)
        {
            IEnumerable<Location> employees = csvService.ReadCSV<Location>(Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "Seed", "seed.csv"));

            context.AddRange(employees);
            context.SaveChanges();
        }
    }
}
