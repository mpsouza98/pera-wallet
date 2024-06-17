CREATE DATABASE perainvest;

CREATE TABLE perainvest.usuarios(
	id	INT NOT NULL,
	email	VARCHAR(40) NOT NULL,
	senha	VARCHAR(80) NOT NULL,
	PRIMARY KEY (id)
);