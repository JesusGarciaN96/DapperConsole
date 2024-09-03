using DapperConsole.Services;
using DapperConsole.Services.Implementations;
using Microsoft.Extensions.Configuration;
using DapperConsole.Entities;

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

            var PaisService = new PaisService(configuration);

            MenuAcciones(PaisService);

            Console.ReadKey();
        }

        public static void MenuAcciones(IPaisService paisService)
        {
            Console.WriteLine("Eliga una opción: ");
            Console.WriteLine("Agregar Pais (A): ");
            Console.WriteLine("Listar Paises (B): ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "A":
                case "a":
                    AgregarPais(paisService);
                    break;
                case "B":
                case "b":
                    LecturaPaises(paisService);
                    break;
                default:
                    Console.WriteLine("La opción no es valida");
                    break;
            }
        }

        public static void LecturaPaises(IPaisService paisService)
        {
            foreach (var item in paisService.Estados("Ch"))
            {
                Console.WriteLine($"{item.Id}, {item.Estado}, {item.Pais}");
            }
        }

        public static void AgregarPais(IPaisService paisService)
        {
            Console.Write("Escribe el nombre del país: ");
            string pais = Console.ReadLine();

            if (pais == null)
            {
                throw new FileNotFoundException("El nombre del país no puede ser null");
            }

            var Pais = new AgregarPaisDto
            {
                Nombre = pais
            };

            string response = paisService.AgregarPais(Pais);

            Console.WriteLine(response);
        }
    }
}
