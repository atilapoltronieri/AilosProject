using AutoFixture;
using Moq;
using Questao5.Domain.Entities.Business;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.Repository;
using Questao5.Infrastructure.Services.Controllers;
using Questao5.Infrastructure.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao5.Tests.Base
{
    public class BaseFixture : Fixture
    {
        private Mock<IMovimentoRepository> mockMovimentoRepository;
        private Mock<IContaCorrenteRepository> mockContaCorrenteRepository;

        private IMovimentoService movimentoService;
        private IContaCorrenteService contaCorrenteService;

        public MovimentoController movimentoController;

        public ContaAtiva contaAtiva;
        public ContaAtiva contaNaoAtiva;
        public ContaSaldo contaSaldoAtiva;
        public ContaSaldo contaSaldoNaoAtiva;
        public MovimentoDto movimentoContaAtiva;
        public MovimentoDto movimentoContaNaoAtiva;
        public MovimentoDto movimentoContaTipoMovimentoA;
        public MovimentoDto movimentoContaValorNegativo;

        public string contaAtivaId;
        public string contaNaoAtivaId;
        public Guid newGuid;

        public BaseFixture()
        {
            CreateBaseObjects();

            SetupMock();

            ConfigureServices();

            ConfigureControllers();
        }

        private void CreateBaseObjects()
        {
            contaAtivaId = "1";
            contaNaoAtivaId = "2";

            contaAtiva = new ContaAtiva(true);
            contaNaoAtiva = new ContaAtiva(false);

            contaSaldoAtiva = new ContaSaldo(1, "Ab", 60);
            contaSaldoNaoAtiva = null;

            newGuid = Guid.NewGuid();

            movimentoContaAtiva = new MovimentoDto() { IdConta = contaAtivaId };
            movimentoContaNaoAtiva = new MovimentoDto() { IdConta = contaNaoAtivaId };
            movimentoContaTipoMovimentoA = new MovimentoDto() { IdConta = contaAtivaId, TipoMovimentacao = "A" };
            movimentoContaValorNegativo = new MovimentoDto() { IdConta = contaAtivaId, Valor = -60 };
        }

        private void SetupMock()
        {
            mockContaCorrenteRepository = new Mock<IContaCorrenteRepository>();
            mockContaCorrenteRepository.Setup(service => service.GetContaAtiva(contaAtivaId)).ReturnsAsync(contaAtiva);
            mockContaCorrenteRepository.Setup(service => service.GetContaAtiva(contaNaoAtivaId)).ReturnsAsync(contaNaoAtiva);
            mockContaCorrenteRepository.Setup(service => service.GetContaSaldo(contaAtivaId)).ReturnsAsync(contaSaldoAtiva);
            mockContaCorrenteRepository.Setup(service => service.GetContaSaldo(contaNaoAtivaId)).ReturnsAsync(contaSaldoNaoAtiva);

            mockMovimentoRepository = new Mock<IMovimentoRepository>();
            mockMovimentoRepository.Setup(service => service.InserirMovimento(movimentoContaAtiva)).ReturnsAsync(newGuid);
            mockMovimentoRepository.Setup(service => service.SaldoConta(contaAtivaId)).ReturnsAsync(contaSaldoAtiva.Saldo);
        }

        private void ConfigureServices()
        {
            contaCorrenteService = new ContaCorrenteService(mockContaCorrenteRepository.Object);
            movimentoService = new MovimentoService(mockMovimentoRepository.Object, contaCorrenteService);
        }

        private void ConfigureControllers()
        {
            movimentoController = new MovimentoController(movimentoService);
        }
    }
}
