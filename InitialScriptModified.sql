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

--CREATE TABLE Sports (
--	Id INT PRIMARY KEY IDENTITY(1,1),
--	Name NVARCHAR(100) NOT NULL,
--)

--CREATE TABLE SponsorshipLevels (
--	Id INT PRIMARY KEY IDENTITY(1,1),
--	Level NVARCHAR(50) NOT NULL,
--)

--CREATE TABLE EventTypes (
--	Id INT PRIMARY KEY IDENTITY(1,1),
--	Type NVARCHAR(100) NOT NULL,
--)

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
--    SportId INT NOT NULL,
--	UserId INT NOT NULL,
--	CONSTRAINT FK_Athlete_UserId FOREIGN KEY (UserId) REFERENCES Users (Id),
--    CONSTRAINT FK_Athlete_Sport FOREIGN KEY (SportId) REFERENCES Sports(Id),
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
--	Created DATETIME NOT NULL,
--	LevelId INT NOT NULL,
--	Amount MONEY NOT NULL,
--	AthleteId INT NOT NULL,
--	SponsorId INT NOT NULL,
--	CONSTRAINT FK_AthleteId_Sponsorship FOREIGN KEY (AthleteId) REFERENCES Users (Id),
--	CONSTRAINT FK_SponsorId_Sponsorship FOREIGN KEY (SponsorId) REFERENCES Users (Id),
--	CONSTRAINT FK_SponsorshipLevel FOREIGN KEY (LevelId) REFERENCES SponsorshipLevels(Id),
--	CONSTRAINT PK_Sponsorships PRIMARY KEY (AthleteId, SponsorId),
--)

--CREATE TABLE SportEvents (
--	Id INT PRIMARY KEY,
--	Name NVARCHAR(200) NOT NULL,
--	Country NVARCHAR(100) NOT NULL,
--	EventDate DATETIME NOT NULL,
--    EventTypeId INT NOT NULL,
--    SportId INT NOT NULL,
--    Finished BIT NOT NULL,
--    CONSTRAINT FK_EventType FOREIGN KEY (EventTypeId) REFERENCES EventTypes(Id),
--    CONSTRAINT FK_Sport FOREIGN KEY (SportId) REFERENCES Sports(Id),
--)

--CREATE TABLE Achievements (
--	Created DATETIME NOT NULL,
--	PlaceFinished SMALLINT NULL,
--	SportEventId INT NOT NULL,
--	AthleteId INT NOT NULL,
--	CONSTRAINT FK_Athlete_Achievement FOREIGN KEY (AthleteId) REFERENCES Users (Id),
--    CONSTRAINT FK_SportEvent_Achievement FOREIGN KEY (SportEventId) REFERENCES SportEvents (Id),
--	CONSTRAINT PK_Achievements PRIMARY KEY (AthleteId, SportEventId),
--)

--CREATE TABLE Goals (
--	Created DATETIME NOT NULL,
--    SportEventId INT NOT NULL,
--	AmountNeeded MONEY NOT NULL,
--	AthleteId INT NOT NULL,
--	CONSTRAINT FK_Athlete_Goal FOREIGN KEY (AthleteId) REFERENCES Users (Id),
--	CONSTRAINT FK_SportEvent_Goal FOREIGN KEY (SportEventId) REFERENCES SportEvents (Id),
--	CONSTRAINT PK_Goals PRIMARY KEY (AthleteId, SportEventId),
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
--)