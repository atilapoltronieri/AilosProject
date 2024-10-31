namespace Questao5.Infrastructure.Database.CommandStore.Responses
{
    public class ContaAtiva
    {
        public bool Ativo { get; set; }

        public ContaAtiva()
        {
            
        }

        public ContaAtiva(bool ativo)
        {
            Ativo = ativo;
        }
    }
}
