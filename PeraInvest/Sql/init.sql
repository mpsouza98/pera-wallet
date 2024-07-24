CREATE DATABASE perainvest;

CREATE TABLE perainvest.usuarios(
	id BINARY(16) NOT NULL,
	email VARCHAR(40) NOT NULL,
	senha VARCHAR(80) NOT NULL,
	PRIMARY KEY (id)
);

CREATE TABLE ativos_financeiro (
    id BINARY(16) PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    descricao TEXT,
    codigo_negociacao VARCHAR(40) NOT NULL,
    cotacao_atual DECIMAL(10, 2),
    data_atualizacao_cotacao DATETIME,
    classe_ativo VARCHAR(255) NOT NULL,
    data_vencimento DATETIME,
    data_emissao DATETIME,
    emissor VARCHAR(255),
    status BIT
);
