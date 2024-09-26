using Microsoft.Extensions.Logging;
using Moq;
using PeraInvest.API.Batch.Models;
using PeraInvest.API.Queries;
using PeraInvest.API.Services;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Infrastructure.Repositories;

namespace PeraInvestTest.Services {

    [TestClass]
    public class CalculadoraServiceTest {
        private Mock<IBatchRepository> _batchRepositoryMock;
        private Mock<IOperacoesCarteiraQuery> _carteiraQueryMock;
        private Mock<ILogger<CalculoService>> _loggerMock;
        private CalculoService _calculoService;
        private Rotina rotina;

        [TestInitialize]
        public void Setup() {
            _batchRepositoryMock = new Mock<IBatchRepository>();
            _carteiraQueryMock = new Mock<IOperacoesCarteiraQuery>();
            _loggerMock = new Mock<ILogger<CalculoService>>();

            _calculoService = new CalculoService(
                _batchRepositoryMock.Object,
                _carteiraQueryMock.Object,
                _loggerMock.Object
            );
            rotina = new Rotina(
                id: Guid.NewGuid().ToString(),
                nome: "Rotina Calculo Valorizacao Test"
             );
        }

        [TestMethod]
        public async Task ExecutaRotinaCalculoValorizacao_NaoDeveProcessar_QuandoJaFinalizadoHoje() {
            // Arrange
            rotina.EstadoExecucao = Rotina.Estado.FINALIZADO;

            _batchRepositoryMock.Setup(r => r.ConsultaRotina(It.IsAny<string>()))
                .ReturnsAsync(rotina);

            // Act
            await _calculoService.ExecutaRotinaCalculoValorizacao();

            // Assert
            _batchRepositoryMock.Verify(r => r.AtualizaRotina(It.IsAny<Rotina>()), Times.Never);
            _batchRepositoryMock.Verify(r => r.ConsultaBlocosDisponiveisRotina(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task ExecutaRotinaCalculoValorizacao_DeveProcessarBlocos_QuandoEstadoNaoIniciado() {
            // Arrange
            _batchRepositoryMock.Setup(r => r.ConsultaRotina(It.IsAny<string>()))
                .ReturnsAsync(rotina);
            _batchRepositoryMock.Setup(r => r.ConsultaBlocosDisponiveisRotina(It.IsAny<string>()))
                .ReturnsAsync(new List<BlocoProcessamento>());

            // Act
            await _calculoService.ExecutaRotinaCalculoValorizacao();

            // Assert
            _batchRepositoryMock.Verify(r => r.AtualizaRotina(It.IsAny<Rotina>()), Times.AtLeastOnce);
            _batchRepositoryMock.Verify(r => r.ConsultaBlocosDisponiveisRotina(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task ExecutaRotinaCalculoValorizacao_DeveExpurgarBlocos_QuandoEstadoFinalizado() {
            // Arrange
            rotina.EstadoExecucao = Rotina.Estado.FINALIZADO;

            _batchRepositoryMock.Setup(r => r.ConsultaRotina(It.IsAny<string>()))
                .ReturnsAsync(rotina);

            // Act
            await _calculoService.ExecutaRotinaCalculoValorizacao();

            // Assert
            _batchRepositoryMock.Verify(r => r.ExpurgoBlocos(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task ExecutaRotinaCalculoValorizacao_DeveReprocessarBlocosComErro_QuandoEstadoFinalizadoComErro() {
            // Arrange
            rotina.EstadoExecucao = Rotina.Estado.FINALIZADO_COM_ERRO;

            _batchRepositoryMock.Setup(r => r.ConsultaRotina(It.IsAny<string>()))
                .ReturnsAsync(rotina);

            // Act
            await _calculoService.ExecutaRotinaCalculoValorizacao();

            // Assert
            _batchRepositoryMock.Verify(r => r.ExpurgoBlocos(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task ExecutaRotinaCalculoValorizacao_DeveProcessarBlocosPendentes_QuandoEstadoEmAndamento() {
            // Arrange
            rotina.EstadoExecucao = Rotina.Estado.EM_ANDAMENTO;

            var bloco = new BlocoProcessamento(
                id: 1,
                rotinaId: "8e442a42-748f-11ef-9c76-0242ac120002",
                estado: Rotina.Estado.NAO_INICIADO,
                tamanhoBloco: 1000,
                offset: 1
            );
            List<BlocoProcessamento> blocosDisponiveis = [bloco];

            _batchRepositoryMock.Setup(r => r.ConsultaRotina(It.IsAny<string>()))
                .ReturnsAsync(rotina);

            _batchRepositoryMock.Setup(r => r.ConsultaBlocosDisponiveisRotina(It.IsAny<string>()))
                .ReturnsAsync(blocosDisponiveis);

            var operacaoAtivoCarteira = new OperacaoAtivoCarteira(
                ativoId: Guid.NewGuid().ToByteArray(),
                carteiraId: Guid.NewGuid().ToByteArray(),
                valorInvestido: 1000,
                valorAcumulado: 1000,
                dataValorizacao: DateTime.Now,
                dataCompra: DateTime.Now
            );
            List<OperacaoAtivoCarteira> listaOperacoes = [operacaoAtivoCarteira];

            _carteiraQueryMock.Setup(q => q.ObterBlocoOperacoes(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(listaOperacoes);

            // Act
            await _calculoService.ExecutaRotinaCalculoValorizacao();

            // Assert
            _batchRepositoryMock.Verify(r => r.ConsultaRotina(It.IsAny<string>()), Times.Once);
            _batchRepositoryMock.Verify(r => r.ConsultaBlocosDisponiveisRotina(It.IsAny<string>()), Times.Once);
            _carteiraQueryMock.Verify(q => q.ObterBlocoOperacoes(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce);
            _batchRepositoryMock.Verify(r => r.AlteraEstadoBloco(It.IsAny<int>(), Rotina.Estado.FINALIZADO), Times.Once);
        }

    }
}
