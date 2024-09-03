using DapperConsole.Entities;
using DapperConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperConsole.Services
{
    public interface IPaisService
    {
        IEnumerable<PaisDto> Estados();

        IEnumerable<PaisDto> Estados(string pais);

        string AgregarPais(AgregarPaisDto pais);
    }
}
