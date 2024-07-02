CREATE DATABASE perainvest;

CREATE TABLE perainvest.usuarios(
	id VARCHAR(26) NOT NULL,
	email VARCHAR(40) NOT NULL,
	senha VARCHAR(80) NOT NULL,
	PRIMARY KEY (id)
);