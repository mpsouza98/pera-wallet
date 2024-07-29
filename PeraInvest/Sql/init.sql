CREATE DATABASE perainvest;

CREATE TABLE perainvest.usuarios(
	id BINARY(16) NOT NULL,
	email VARCHAR(40) NOT NULL,
	senha VARCHAR(80) NOT NULL,
	PRIMARY KEY (id)
);

CREATE TABLE ativos_financeiro (
    id VARCHAR(36) PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    descricao TEXT,
    codigo_negociacao VARCHAR(40) NOT NULL,
    idx DECIMAL(10, 2),
    classe_ativo INT NOT NULL,
    data_vencimento DATETIME,
    data_emissao DATETIME,
    emissor VARCHAR(255),
    status TINYINT(1)
);
