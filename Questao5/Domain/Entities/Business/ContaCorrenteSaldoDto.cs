namespace Questao5.Domain.Entities.Business
{
    public class ContaCorrenteSaldoDto
    {
        public string Numero { get; set; }
        public string Titular { get; set; }
        public DateTime DataConsulta { get; set; }
        public double Saldo { get; set; }

        public ContaCorrenteSaldoDto(string numero, string titular, double saldo)
        {
            Numero = numero;
            Titular = titular;
            Saldo = saldo;
            DataConsulta = DateTime.Now;
        }

    }
}
