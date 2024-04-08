--Requirements
--1. Start from your domain models in your internship project

--2. Create the necessary tables to model the database for your application

--3. Save all the SQL commands you execute in a file

--4. Create a table diagram in SQL Server Management Studio

--5. Pay attention on how you model relationships and data normalization. 

--NOTE: You might need more tables than the number of your classes in your domain model. 

 

--Grading
--1. Submit this assignment with the answer "Ready" or anthing else

--2. Get in touch with your mentor and discuss the generated table diagram

--3. Show the file with the commands you rand to arrive at the table structure you present

--4. The mentor will then grade your assignment

--CREATE DATABASE SponsorSphere;

USE SponsorSphere;

CREATE TABLE Athletes (
	UserType INT NOT NULL DEFAULT 1,
	Id INT NOT NULL IDENTITY(1,1),
	CONSTRAINT PK_Athlete PRIMARY KEY (UserType, Id),
	Name NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Password NVARCHAR(200) NOT NULL,
	Country NVARCHAR(100) NOT NULL,
	Birthdate DATETIME NOT NULL,
	PhoneNumber NVARCHAR(30) NOT NULL,
	Created DATETIME NOT NULL,
	Sport NVARCHAR(200) NOT NULL,
	PictureOrLogo IMAGE NULL,
	Website NVARCHAR(200) NULL,
	FacebookLink NVARCHAR(200) NULL,
	InstagramLink NVARCHAR(200) NULL,
	TwitterLink NVARCHAR(200) NULL,
	StravaLink NVARCHAR(200) NULL,
)

CREATE TABLE SponsorCompanies (
	UserType INT NOT NULL DEFAULT 2,
	Id INT NOT NULL IDENTITY(1,1),
	CONSTRAINT PK_SponsorCompanie PRIMARY KEY (UserType, Id),
	Name NVARCHAR(200) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Password NVARCHAR(200) NOT NULL,
	Country NVARCHAR(100) NOT NULL,
	PhoneNumber NVARCHAR(30) NULL,
	Created DATETIME NOT NULL,
	PictureOrLogo IMAGE NULL,
	Iban NVARCHAR(100) NOT NULL,
	Website NVARCHAR(200) NULL,
	FacebookLink NVARCHAR(200) NULL,
	InstagramLink NVARCHAR(200) NULL,
	TwitterLink NVARCHAR(200) NULL,
	StravaLink NVARCHAR(200) NULL,
)

CREATE TABLE SponsorIndividuals (
	UserType INT NOT NULL DEFAULT 3,
	Id INT NOT NULL IDENTITY(1,1),
	CONSTRAINT PK_SponsorIndividual PRIMARY KEY (UserType, Id),
	Name NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Password NVARCHAR(200) NOT NULL,
	Country NVARCHAR(100) NOT NULL,
	Birthdate DATETIME NOT NULL,
	PhoneNumber NVARCHAR(30) NULL,
	Created DATETIME NOT NULL,
	PictureOrLogo IMAGE NULL,
	Website NVARCHAR(200) NULL,
	FacebookLink NVARCHAR(200) NULL,
	InstagramLink NVARCHAR(200) NULL,
	TwitterLink NVARCHAR(200) NULL,
	StravaLink NVARCHAR(200) NULL,
)

CREATE TABLE Sponsorships (
	Id INT PRIMARY KEY,
	Created DATETIME NOT NULL,
	Level TINYINT NOT NULL,
	Amount MONEY NOT NULL,
	AthleteType INT NOT NULL DEFAULT 1,
	AthleteId INT NOT NULL,
	SponsorType INT NOT NULL,
	SponsorId INT NOT NULL,
	CONSTRAINT FK_Athlete_Sponsorhip FOREIGN KEY (AthleteType, AthleteId) REFERENCES Athletes (UserType, Id),
	CONSTRAINT FK_SponsorCompany_Sponsorhip FOREIGN KEY (SponsorType, SponsorId) REFERENCES SponsorCompanies (UserType, Id),
	CONSTRAINT FK_SponsorIndividual_Sponsorhip FOREIGN KEY (SponsorType, SponsorId) REFERENCES SponsorIndividuals (UserType, Id)
)

CREATE TABLE Achievements (
	Id INT PRIMARY KEY,
	Created DATETIME NOT NULL,
	Sport NVARCHAR(200) NOT NULL,
	EventType NVARCHAR(200) NOT NULL,
	PlaceFinished SMALLINT NULL,
	AthleteType INT NOT NULL DEFAULT 1,
	AthleteId INT NOT NULL,
	CONSTRAINT FK_Athlete_Achievement FOREIGN KEY (AthleteType, AthleteId) REFERENCES Athletes (UserType, Id)
)

CREATE TABLE Goals (
	Id INT PRIMARY KEY,
	AthleteType INT NOT NULL DEFAULT 1,
	AthleteId INT NOT NULL,
	Created DATETIME NOT NULL,
	Date DATETIME NOT NULL,
	EventType NVARCHAR(200) NOT NULL,
	Sport NVARCHAR(200) NOT NULL,
	AmountNeeded MONEY NOT NULL,
	CONSTRAINT FK_Athlete_Goal FOREIGN KEY (AthleteType, AthleteId) REFERENCES Athletes (UserType, Id)
)

CREATE TABLE BlogPosts (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Created DATETIME NOT NULL,
	Content TEXT NOT NULL,
	UserType INT NOT NULL,
	AuthorId INT NOT NULL,
	CONSTRAINT FK_Athlete_BlogPosts FOREIGN KEY (UserType, AuthorId) REFERENCES Athletes (UserType, Id),
	CONSTRAINT FK_SponsorCompany_BlogPosts FOREIGN KEY (UserType, AuthorId) REFERENCES SponsorCompanies (UserType, Id),
	CONSTRAINT FK_SponsorIndividual_BlogPosts FOREIGN KEY (UserType, AuthorId) REFERENCES SponsorIndividuals (UserType, Id)
)

CREATE TABLE Athletes_Sponsors (
	AthleteType INT NOT NULL DEFAULT 1,
	AthleteId INT NOT NULL,
	SponsorType INT NOT NULL,
	SponsorId INT NOT NULL,
	CONSTRAINT FK_Athlete FOREIGN KEY (AthleteType, AthleteId) REFERENCES Athletes (UserType, Id),
	CONSTRAINT FK_SponsorCompany FOREIGN KEY (SponsorType, SponsorId) REFERENCES SponsorCompanies (UserType, Id),
	CONSTRAINT FK_SponsorIndividual FOREIGN KEY (SponsorType, SponsorId) REFERENCES SponsorIndividuals (UserType, Id)
)