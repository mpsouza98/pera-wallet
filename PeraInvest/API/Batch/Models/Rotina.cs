﻿using PeraInvest.Domain.CarteiraAggregate;
using System;

namespace PeraInvest.API.Batch.Models {
    public class Rotina {
        public string Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicioProcessamento { get; set; }
        public DateTime DataFimProcessamento { get; set; }
        public Estado EstadoExecucao { get; set; }
        public enum Estado { NAO_INICIADO, EM_ANDAMENTO, FINALIZADO, FINALIZADO_COM_ERRO }

        public Rotina(string id, string nome, DateTime dataInicioProcessamento, DateTime dataFimProcessamento, Estado estadoExecucao) {
            Id = id;
            Nome = nome;
            DataInicioProcessamento = dataInicioProcessamento;
            DataFimProcessamento = dataFimProcessamento;
            EstadoExecucao = estadoExecucao;
        }
    }
}
