using PeraInvest.API.Batch.Models;
using PeraInvest.API.Queries;
using PeraInvest.Infrastructure.Repositories;
using System.Diagnostics;

namespace PeraInvest.API.Services {

    interface ICalculoService {
        Task ExecutaRotinaCalculoValorizacao();
    }

    public sealed class CalculoService : ICalculoService {
        private readonly IBatchRepository batchRepository;
        private readonly IOperacoesCarteiraQuery carteiraQuery;
        private readonly ILogger<CalculoService> logger;
        private static readonly int TAMANHO_BLOCO = 1000;
        private static readonly string ROTINA_ID = "8e442a42-748f-11ef-9c76-0242ac120002";

        public CalculoService(IBatchRepository batchRepository, IOperacoesCarteiraQuery carteiraQuery, ILogger<CalculoService> logger) {
            this.batchRepository = batchRepository;
            this.carteiraQuery = carteiraQuery;
            this.logger = logger;
        }

        public async Task ExecutaRotinaCalculoValorizacao() {
            Rotina rotina = await batchRepository.ConsultaRotina(ROTINA_ID);

            if (rotina.DataInicioProcessamento.Date.Equals(DateTime.Today) && rotina.EstadoExecucao == Rotina.Estado.FINALIZADO) return;

            if (rotina.EstadoExecucao == Rotina.Estado.NAO_INICIADO) {
                rotina.DataInicioProcessamento = DateTime.Now;
                rotina.EstadoExecucao = Rotina.Estado.EM_ANDAMENTO;
                await AtualizaRotina(rotina);
                await CriaBlocosProcessamento();
                await ProcessaBlocosDisponiveis();
            }
            else if (rotina.EstadoExecucao == Rotina.Estado.EM_ANDAMENTO) {
                await ProcessaBlocosDisponiveis();
            }
            else if (rotina.EstadoExecucao == Rotina.Estado.FINALIZADO) {
                await ExpurgoBlocosProcessamento();
                return;
            }
            else if (rotina.EstadoExecucao == Rotina.Estado.FINALIZADO_COM_ERRO) {
                await ReprocessaBlocosComErro();
            }

            await ExpurgoBlocosProcessamento();
            rotina.EstadoExecucao = Rotina.Estado.FINALIZADO;
            rotina.DataFimProcessamento = DateTime.Now;
            await AtualizaRotina(rotina);
        }

        private async Task<List<BlocoProcessamento>> CriaBlocosProcessamento() {
            int TotalOperacoes = await carteiraQuery.ConsultaTotalOperacoes();
            List<BlocoProcessamento> blocos = [];
            int TamanhoBlocoParcial = TotalOperacoes % TAMANHO_BLOCO;
            int offsetAtual = 1;


            if (TamanhoBlocoParcial > 1) {
                BlocoProcessamento blocoParcial = new(
                    id: 1,
                    rotinaId: ROTINA_ID,
                    estado: Rotina.Estado.NAO_INICIADO,
                    tamanhoBloco: TamanhoBlocoParcial,
                    offset: offsetAtual
                );
                blocos.Add(blocoParcial);
                TotalOperacoes -= TamanhoBlocoParcial;
                offsetAtual = TamanhoBlocoParcial;
            }

            for (int i = 1; i <= TotalOperacoes / TAMANHO_BLOCO; i++) {
                var blocoId = (i == 1 && blocos.Count == 1) ? i + 1 : i;

                BlocoProcessamento bloco = new(
                    id: blocoId,
                    rotinaId: ROTINA_ID,
                    estado: Rotina.Estado.NAO_INICIADO,
                    tamanhoBloco: TAMANHO_BLOCO,
                    offset: offsetAtual
                );
                blocos.Add(bloco);
                offsetAtual += TAMANHO_BLOCO;
            }

            try {
                await batchRepository.InsereBlocos(blocos);
                logger.LogInformation("Sucesso ao criar blocos processamento");
            }
            catch (Exception ex) {
                logger.LogError("Erro ao inserir blocos processamento!");
            }

            return await batchRepository.ConsultaBlocosDisponiveisRotina(ROTINA_ID);
        }

        private async Task ProcessaBlocosDisponiveis() {
            logger.LogInformation($"Tempo inicio={DateTime.Now}");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var listaBlocosDisponiveis = await batchRepository.ConsultaBlocosDisponiveisRotina(ROTINA_ID);

            if (listaBlocosDisponiveis.Count == 0) throw new Exception($"Blocos de processamento não disponíveis para execução da rotina={ROTINA_ID}");


            foreach (var bloco in listaBlocosDisponiveis) {
                logger.LogInformation($"Processando bloco={bloco.Id} rotina={bloco.RotinaId} offset={bloco.Offset} tamanhoBloco={bloco.TamanhoBloco}");

                var blocoOperacoes = await carteiraQuery.ObterBlocoOperacoes(size: bloco.TamanhoBloco, offset: bloco.Offset);

                Parallel.ForEach(blocoOperacoes, op => {
                    logger.LogInformation($"Processando operação={op.Id} carteira={op.CarteiraId} valorInvestido={op.ValorInvestido} dataValorizacao={op.DataValorizacao}");
                });

                await FinalizaBlocoOperacao(bloco.Id);

                logger.LogInformation("Bloco de operacoes valorizado!");
            }

            stopwatch.Stop();

            logger.LogInformation($"Tempo fim={DateTime.Now}");
            logger.LogInformation($"Duração execução={stopwatch.Elapsed}");
        }

        private async Task ReprocessaBlocosComErro() {

        }

        private async Task FinalizaBlocoOperacao(int blocoId) {
            try {
                await batchRepository.AlteraEstadoBloco(blocoId, Rotina.Estado.FINALIZADO);
            }
            catch (Exception ex) {
                logger.LogError($"Nao foi possivel atualizar BlocoId={blocoId} Rotina={ROTINA_ID} para finalizado");
            }
        }

        private async Task ExpurgoBlocosProcessamento() {
            try {
                await batchRepository.ExpurgoBlocos(ROTINA_ID);
            }
            catch (Exception ex) {
                logger.LogError($"Nao foi possivel realizar o expurgo dos blocos processados Rotina={ROTINA_ID}");
            }
        }

        private async Task AtualizaRotina(Rotina rotina) {
            try {
                await batchRepository.AtualizaRotina(rotina);
            }
            catch (Exception ex) {
                logger.LogError($"Nao foi possivel Rotina={ROTINA_ID} para finalizado");
            }
        }
    }
}
