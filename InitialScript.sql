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


USE InternshipProject;

--CREATE TABLE Athletes (
--	Name NVARCHAR(200) NOT NULL,
--	LastName NVARCHAR(200) NOT NULL,
--	Email NVARCHAR(100) NOT NULL,
--	Password NVARCHAR(200) NOT NULL,
--	Country NVARCHAR(100) NOT NULL,
--	Birthdate DATETIME NOT NULL,
--	PhoneNumber NVARCHAR(30) NOT NULL,
--	Created DATETIME NOT NULL,
--	Sport NVARCHAR(200) NOT NULL,
--	PictureOrLogo IMAGE NULL,
--	Website NVARCHAR(200) NULL,
--	FacebookLink NVARCHAR(200) NULL,
--	InstagramLink NVARCHAR(200) NULL,
--	TwitterLink NVARCHAR(200) NULL,
--	StravaLink NVARCHAR(200) NULL,
--)

--CREATE TABLE SponsorCompanies (
--	Name NVARCHAR(200) NOT NULL,
--	Email NVARCHAR(100) NOT NULL,
--	Password NVARCHAR(200) NOT NULL,
--	Country NVARCHAR(100) NOT NULL,
--	PhoneNumber NVARCHAR(30) NOT NULL,
--	Created DATETIME NOT NULL,
--	PictureOrLogo IMAGE NULL,
--	Iban NVARCHAR(100) NOT NULL,
--	Website NVARCHAR(200) NULL,
--	FacebookLink NVARCHAR(200) NULL,
--	InstagramLink NVARCHAR(200) NULL,
--	TwitterLink NVARCHAR(200) NULL,
--	StravaLink NVARCHAR(200) NULL,
--)

--CREATE TABLE SponsorIndividuals (
--	Name NVARCHAR(200) NOT NULL,
--	LastName NVARCHAR(200) NOT NULL,
--	Email NVARCHAR(100) NOT NULL,
--	Password NVARCHAR(200) NOT NULL,
--	Country NVARCHAR(100) NOT NULL,
--	Birthdate DATETIME NOT NULL,
--	PhoneNumber NVARCHAR(30) NULL,
--	Created DATETIME NOT NULL,
--	PictureOrLogo IMAGE NULL,
--	Website NVARCHAR(200) NULL,
--	FacebookLink NVARCHAR(200) NULL,
--	InstagramLink NVARCHAR(200) NULL,
--	TwitterLink NVARCHAR(200) NULL,
--	StravaLink NVARCHAR(200) NULL,
--)

--CREATE TABLE Sponsorships (
--	AthleteId INT NOT NULL,
--	SponsorId INT NOT NULL,
--	Created DATETIME NOT NULL,
--	Amount MONEY NOT NULL,
--	Level TINYINT NOT NULL,
--)

--CREATE TABLE Achievements (
--	AthleteId INT NOT NULL,
--	Sport NVARCHAR(200) NOT NULL,
--	Created DATETIME NOT NULL,
--	EventType NVARCHAR(200) NOT NULL,
--	Place SMALLINT NOT NULL,
--)

--CREATE TABLE Goals (
--	Date DATETIME NOT NULL,
--	EventType NVARCHAR(200) NOT NULL,
--	Sport NVARCHAR(200) NOT NULL,
--	AmountNeeded MONEY NOT NULL,
--)

--CREATE TABLE BlogPosts (
--	Date DATETIME NOT NULL,
--	Content TEXT NOT NULL,
--	AuthorId INT NOT NULL,
--	Picture IMAGE NULL,
--)