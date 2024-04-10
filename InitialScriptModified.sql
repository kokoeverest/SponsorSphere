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

--CREATE TABLE Users (
--	Id INT NOT NULL IDENTITY(1,1),
--	Name NVARCHAR(200) NOT NULL,
--	Email NVARCHAR(100) NOT NULL,
--	Password NVARCHAR(200) NOT NULL,
--	Country NVARCHAR(100) NOT NULL,
--	PhoneNumber NVARCHAR(30) NOT NULL,
--	Created DATETIME NOT NULL,
--	PictureOrLogo IMAGE NULL,
--	Website NVARCHAR(200) NULL,
--	FacebookLink NVARCHAR(200) NULL,
--	InstagramLink NVARCHAR(200) NULL,
--	TwitterLink NVARCHAR(200) NULL,
--	StravaLink NVARCHAR(200) NULL,
--	CONSTRAINT PK_User PRIMARY KEY (Id),
--)

--CREATE TABLE Athletes (
--	LastName NVARCHAR(200) NOT NULL,
--	Birthdate DATETIME NOT NULL,
--	Sport NVARCHAR(200) NOT NULL,
--	UserId INT NOT NULL,
--	CONSTRAINT FK_Athlete_UserId FOREIGN KEY (UserId) REFERENCES Users (Id),
--)

--CREATE TABLE SponsorCompanies (
--	Iban NVARCHAR(100) NOT NULL,
--	UserId INT NOT NULL,
--	CONSTRAINT FK_SponsorCompany_UserId FOREIGN KEY (UserId) REFERENCES Users (Id),
--)

--CREATE TABLE SponsorIndividuals (
--	LastName NVARCHAR(200) NOT NULL,
--	Birthdate DATETIME NOT NULL,
--	UserId INT NOT NULL,
--	CONSTRAINT FK_SponsorIndividual_UserId FOREIGN KEY (UserId) REFERENCES Users (Id),
--)

--CREATE TABLE Sponsorships (
--	Id INT PRIMARY KEY,
--	Created DATETIME NOT NULL,
--	Level TINYINT NOT NULL,
--	Amount MONEY NOT NULL,
--	AthleteId INT NOT NULL,
--	SponsorId INT NOT NULL,
--	CONSTRAINT FK_AthleteId_Sponsorship FOREIGN KEY (AthleteId) REFERENCES Users (Id),
--	CONSTRAINT FK_SponsorId_Sponsorship FOREIGN KEY (SponsorId) REFERENCES Users (Id),
--)

--CREATE TABLE Achievements (
--	Id INT PRIMARY KEY,
--	Created DATETIME NOT NULL,
--	Sport NVARCHAR(200) NOT NULL,
--	EventType NVARCHAR(200) NOT NULL,
--	PlaceFinished SMALLINT NULL,
--	AthleteId INT NOT NULL,
--	CONSTRAINT FK_Athlete_Achievement FOREIGN KEY (AthleteId) REFERENCES Users (Id),
--)

--CREATE TABLE Goals (
--	Id INT PRIMARY KEY,
--	Created DATETIME NOT NULL,
--	Date DATETIME NOT NULL,
--	EventType NVARCHAR(200) NOT NULL,
--	Sport NVARCHAR(200) NOT NULL,
--	AmountNeeded MONEY NOT NULL,
--	AthleteId INT NOT NULL,
--	CONSTRAINT FK_Athlete_Goal FOREIGN KEY (AthleteId) REFERENCES Users (Id),
--)

--CREATE TABLE BlogPosts (
--	Id INT PRIMARY KEY IDENTITY(1,1),
--	Created DATETIME NOT NULL,
--	Content TEXT NOT NULL,
--	UserId INT NOT NULL,
--	CONSTRAINT FK_User_BlogPosts FOREIGN KEY (UserId) REFERENCES Users (Id),
--	)

--CREATE TABLE Athletes_Sponsors (
--	AthleteId INT NOT NULL,
--	SponsorId INT NOT NULL,
--	CONSTRAINT FK_Athlete FOREIGN KEY ( AthleteId) REFERENCES Users (Id),
--	CONSTRAINT FK_Sponsor FOREIGN KEY ( SponsorId) REFERENCES Users (Id),
--	)