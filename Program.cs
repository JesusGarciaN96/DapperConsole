using DapperConsole.Services.Implementations;

namespace DapperConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var PaisService = new PaisService();

            foreach (var item in PaisService.Paises())
            {
                Console.WriteLine($"{item.Id}, {item.Estado}, {item.Pais}");
            }
        }
    }
}
