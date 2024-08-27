using DapperConsole.Services.Implementations;
using Microsoft.Extensions.Configuration;

namespace DapperConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Uso de appsettings.json
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.dev.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            LecturaPaises(configuration);

            Console.ReadKey();
        }

        public static void LecturaPaises(IConfiguration configuration)
        {
            var PaisService = new PaisService(configuration);

            foreach (var item in PaisService.Estados("Ch"))
            {
                Console.WriteLine($"{item.Id}, {item.Estado}, {item.Pais}");
            }
        }
    }
}
