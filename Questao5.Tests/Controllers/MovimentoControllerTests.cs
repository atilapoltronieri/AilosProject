using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Language;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Exceptions;
using Questao5.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao5.Tests.Controllers
{
    public class MovimentoControllerTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture fixture;

        public MovimentoControllerTests(BaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task MovimentacaoDeConta_ContaAtivaRetornaNovoId()
        {
            // Act
            var result = await fixture.movimentoController.MovimentacaoDeConta(fixture.movimentoContaAtiva);

            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.NotNull(okObjectResult);

            var guid = (Guid)okObjectResult.Value;
            Assert.Equal(guid, fixture.newGuid);

        }

        [Fact]
        public async Task MovimentacaoDeConta_ContaNaoAtivaThrowsBusinessErrorMessage()
        {
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () => await fixture.movimentoController.MovimentacaoDeConta(fixture.movimentoContaNaoAtiva));

            // Assert
            Assert.IsType<BusinessException>(result);
            Assert.Equal(result.Message, BusinessErrorMessages.InvalidAccount);
        }

        [Fact]
        public async Task Saldo_ContaAtivaRetornaSaldo()
        {
            // Act
            var result = await fixture.movimentoController.SaldoConta(fixture.contaAtivaId);

            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.NotNull(okObjectResult);

            var contaSaldo = (ContaSaldo)okObjectResult.Value;
            Assert.Equal(60, contaSaldo.Saldo);
        }


        [Fact]
        public async Task Saldo_ContaNaoAtivaThrowsBusinessErrorMessage()
        {
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () => await fixture.movimentoController.SaldoConta(fixture.contaNaoAtivaId));

            // Assert
            Assert.IsType<BusinessException>(result);
            Assert.Equal(result.Message, BusinessErrorMessages.InvalidAccount);
        }
    }
}
