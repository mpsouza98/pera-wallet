CREATE DATABASE perainvest;

CREATE TABLE perainvest.usuarios (
    id MEDIUMINT NOT NULL AUTO_INCREMENT,
    code varchar(25) UNIQUE,
    pos_x double(7,2) NOT NULL,
    pos_y double(7,2) NOT NULL,
    PRIMARY KEY (id)
);