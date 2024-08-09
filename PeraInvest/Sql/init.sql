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
    codigo_negociacao VARCHAR(40) NOT NULL UNIQUE,
    idx DECIMAL(10, 2),
    classe_ativo INT NOT NULL,
    data_vencimento DATETIME,
    data_emissao DATETIME,
    emissor VARCHAR(255),
    status TINYINT(1)
);

CREATE TABLE operacao_carteira (
    id BINARY(16) PRIMARY KEY,
    ativo_id BINARY(16) NOT NULL,
    valor_investido DECIMAL(10,2) NOT NULL,
    valor_acumulado DECIMAL(10,2) NOT NULL,
    data_valorizacao DATETIME NOT NULL,
    data_compra DATETIME NOT NULL,

    FOREIGN KEY (ativo_id) REFERENCES ativos_financeiro(id)
);