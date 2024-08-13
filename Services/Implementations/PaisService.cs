using Dapper;
using DapperConsole.Models;
using DapperConsole.Settings;
using System.Data.SqlClient;

namespace DapperConsole.Services.Implementations
{
    public class PaisService : IPaisService
    {
        private readonly string _connection = Connection.connectionString;
        private readonly SqlConnection _connectionUniversidad;

        public PaisService()
        {
            _connectionUniversidad = new SqlConnection(_connection);
        }

        public IEnumerable<PaisDto> Paises()
        {
            string query = @"
                SELECT 
	                [ET].[Id], 
	                [ET].[Nombre] AS 'Estado', 
	                [PS].[Nombre] AS 'Pais' 
                FROM [dbo].[Estado] ET 
                INNER JOIN 
	                [dbo].[Pais] PS 
                ON PS.Id = ET.PaisId
                WHERE [PS].[Nombre] != 'México';
            ";

            var paisesList = _connectionUniversidad.Query<PaisDto>(query);

            return paisesList.ToList();
        }
    }
}

