using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities.Business;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<Guid> InserirMovimento(MovimentoDto movimento)
        {
            var idMovimento = Guid.NewGuid();

            using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO movimento VALUES (@idMovimento, @idContaCorrente, @dataMovimento, @tipoMovimento, @valor)";
            command.Parameters.AddWithValue("idMovimento", idMovimento);
            command.Parameters.AddWithValue("idContaCorrente", movimento.IdConta);
            command.Parameters.AddWithValue("dataMovimento", DateTime.Now.ToString());
            command.Parameters.AddWithValue("tipoMovimento", movimento.TipoMovimentacao);
            command.Parameters.AddWithValue("valor", movimento.Valor);

            await command.ExecuteNonQueryAsync();

            return idMovimento;
        }

        public async Task<double> SaldoConta(string idConta)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT 
                                    SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE 0 END) -
                                    SUM(CASE WHEN tipomovimento = 'D' THEN valor ELSE 0 END) AS saldo
                                FROM 
                                    movimento
                                WHERE 
                                    idcontacorrente = @idConta
                                GROUP BY 
                                    idcontacorrente";
            command.Parameters.AddWithValue("idConta", idConta);

            var result = await command.ExecuteScalarAsync();
            return result == DBNull.Value ? 0.0 : Convert.ToDouble(result);
        }
    }
}
