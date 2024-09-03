using Dapper;
using DapperConsole.Entities;
using DapperConsole.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DapperConsole.Services.Implementations
{
    public class PaisService : IPaisService
    {
        private readonly SqlConnection _connectionUniversidad;

        public PaisService(IConfiguration configuration)
        {
            _connectionUniversidad = new SqlConnection(configuration.GetConnectionString("Default_Connection"));
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

        public string AgregarPais(AgregarPaisDto pais)
        {
            try
            {
                string queryInsert = @"
                    INSERT INTO 
                        [dbo].[Pais] (Nombre) 
                    OUTPUT INSERTED.Nombre
                    VALUES(@pais);
                ";

                string queryFind = @"
                    SELECT TOP 1 [Nombre] 
                    FROM [dbo].[Pais] 
                    WHERE UPPER([Nombre]) = UPPER(@pais)
                ";

                if (String.IsNullOrEmpty(pais.Nombre))
                {
                    return "El país no es valido.";
                }

                var parametros = new { pais = pais.Nombre };
                var paisEncontrado = _connectionUniversidad.Query<string>(queryFind, parametros);

                if (paisEncontrado.Any())
                {
                    return "El país ya está registrado.";
                }

                var response = _connectionUniversidad.Query<string>(queryInsert, parametros);
                return response.Any() ? response.First() : "";
            }
            catch
            {
                throw new Exception("Ocurrió un error al insertar el país.");
            }

        }
    }
}

