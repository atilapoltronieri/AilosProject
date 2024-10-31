namespace Questao5.Infrastructure.Database.CommandStore.Responses
{
    public class ContaSaldo
    { 
        public int Numero { get; set; }
        public string Titular { get; set; } 
        public DateTime DataConsulta { get; set; }
        public double Saldo { get; set; }

        public ContaSaldo()
        {
            
        }

        public ContaSaldo(int numero, string titular, double saldo)
        {
            Numero = numero;
            Titular = titular;
            Saldo  = saldo;
            DataConsulta = DateTime.Now;
        }
    }
}
