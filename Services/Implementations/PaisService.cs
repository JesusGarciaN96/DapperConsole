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

        public IEnumerable<PaisDto> Estados()
        {
            string query = @"
                SELECT 
	                [ET].[Id], 
	                [ET].[Nombre] AS 'Estado', 
	                [PS].[Nombre] AS 'Pais' 
                FROM [dbo].[Estado] ET 
                INNER JOIN 
	                [dbo].[Pais] PS 
                ON PS.Id = ET.PaisId;
            ";

            var estados = _connectionUniversidad.Query<PaisDto>(query);

            return estados.ToList();
        }

        public IEnumerable<PaisDto> Estados(string pais)
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
                WHERE [PS].[Nombre] LIKE @pais;
            ";

            var parametros = new { pais = $"%{pais}%" };

            var estados = _connectionUniversidad.Query<PaisDto>(query, parametros);

            return estados.ToList();
        }
    }
}

