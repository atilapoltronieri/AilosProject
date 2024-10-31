using System;
using System.Globalization;
using System.Text;

namespace Questao1
{
    internal class ContaBancaria : BaseClass {       

        public int Numero { get; private set; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int numero, string titular) : this(numero, titular, 0) { }

        public ContaBancaria(int numero, string titular, double saldo) : base()
        {
            Numero=numero;
            Titular=titular;
            Saldo=saldo;
        }

        public void Deposito(double valor)
        {
            Saldo += valor;
        }

        public void Saque(double valor)
        {
            Saldo -= valor;
        }

        public override string ToString()
        {
            var contaString = new StringBuilder();
            contaString.Append($"Conta: {Numero}, ");
            contaString.Append($"Titular: {Titular}, ");
            contaString.AppendLine($"Saldo: $ {Saldo.ToString("0.00", CultureInfo.InvariantCulture)}");

            return contaString.ToString();
        }
    }
}
