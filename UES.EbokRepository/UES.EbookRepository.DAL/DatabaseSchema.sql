CREATE DATABASE ebook;

USE ebook;

CREATE TABLE categories (
  CategoryId INT NOT NULL AUTO_INCREMENT,
  Name VARCHAR(30) NOT NULL,
  PRIMARY KEY (CategoryId)
);

CREATE TABLE languages (
  LanguageId INT NOT NULL AUTO_INCREMENT,
  Name VARCHAR(30) NOT NULL,
  PRIMARY KEY (LanguageId)
);

CREATE TABLE users (
  UserId INT NOT NULL AUTO_INCREMENT,
  Firstname VARCHAR(30) NOT NULL,
  Lastname VARCHAR(30) NOT NULL,
  Username VARCHAR(10) NOT NULL,
  Password LONGTEXT NOT NULL,
  Type varchar(30) NOT NULL,
  CategoryId INT NOT NULL,
  PRIMARY KEY (UserId), 
  FOREIGN KEY (CategoryId) REFERENCES categories (CategoryId) ON DELETE CASCADE
);


CREATE TABLE ebooks (
  EbookId INT NOT NULL AUTO_INCREMENT,
  Title VARCHAR(80) NOT NULL,
  Author VARCHAR(120) DEFAULT NULL,
  Keywords VARCHAR(120) DEFAULT NULL,
  PublicationYear INT NOT NULL,
  Filename VARCHAR(200) NOT NULL,
  MIME VARCHAR(100) DEFAULT NULL,
  UserId INT  NOT NULL,
  CategoryId INT  NOT NULL,
  LanguageId INT NOT NULL,
  PRIMARY KEY (EbookId),
  FOREIGN KEY (CategoryId) REFERENCES categories (CategoryId) ON DELETE CASCADE,
  FOREIGN KEY (LanguageId) REFERENCES languages (LanguageId) ON DELETE CASCADE,
  FOREIGN KEY (UserId) REFERENCES users (UserId) ON DELETE CASCADE
);

INSERT INTO languages(name) VALUES('Serbian');
INSERT INTO languages(name) VALUES('English');
INSERT INTO languages(name) VALUES('Croatian');

INSERT INTO categories(name) VALUES('None');
INSERT INTO categories(name) VALUES('Comedy');
INSERT INTO categories(name) VALUES('Business');

INSERT INTO users(Firstname,Lastname,Username,Password,Type,CategoryId) VALUES ('Marko','Markovic','marko','AQAAAAEAACcQAAAAEHzn1jsKZ+C5fkIlE5cAUDJhtxOVj1be+4qmCNu0w2LIbC+EJY1enxhHHZgG56Na1Q==','Admin',1);
INSERT INTO users(Firstname,Lastname,Username,Password,Type,CategoryId) VALUES ('Petar','Petrovic','pera','AQAAAAEAACcQAAAAEHzn1jsKZ+C5fkIlE5cAUDJhtxOVj1be+4qmCNu0w2LIbC+EJY1enxhHHZgG56Na1Q==','Admin',2);
