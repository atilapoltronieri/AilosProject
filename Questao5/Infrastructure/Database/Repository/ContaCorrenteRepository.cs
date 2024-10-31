using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repository
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<ContaAtiva> GetContaAtiva(string idConta)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ativo, numero, nome FROM contacorrente WHERE idcontacorrente = @idConta;";
            command.Parameters.AddWithValue("idConta", idConta);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var contaCorrente = new ContaAtiva
                {
                    Ativo = reader.GetBoolean(0),
                };

                return contaCorrente;
            }

            return null;
        }

        public async Task<ContaSaldo> GetContaSaldo(string idConta)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ativo, numero, nome FROM contacorrente WHERE idcontacorrente = @idConta;";
            command.Parameters.AddWithValue("idConta", idConta);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                if (reader.GetBoolean(0) == false)
                    return null;

                var contaSaldo = new ContaSaldo
                {
                    Numero = reader.GetInt32(1),
                    Titular = reader.GetString(2),
                };

                return contaSaldo;
            }

            return null;
        }
    }
}
