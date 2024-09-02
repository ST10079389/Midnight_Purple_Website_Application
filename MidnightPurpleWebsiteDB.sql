create database Midnight_Purple_WebsiteDB;
use Midnight_Purple_WebsiteDB;

create table Users(
UserID INT IDENTITY (1,1) PRIMARY KEY,
[Name] VARCHAR(30),
[Password] VARCHAR(50)
);

create table SemesterDates(
SemesterDatesID INT IDENTITY(1,1) PRIMARY KEY,
Semester_Start DATE,
NumberOfWeeks INT,
Semester_End DATE,
UserID INT FOREIGN KEY REFERENCES Users(UserID)
);

create table ModuleInformation(
ModuleID INT IDENTITY(1,1) PRIMARY KEY,
Module_Name VARCHAR(25),
Module_Code VARCHAR(25),
NumberOfCredits INT,
ClassHours INT,
SelfStudyHoursPerWeek INT,
UserID INT FOREIGN KEY REFERENCES Users(UserID),
);
create table [Notification](
NotificationID INT IDENTITY(1,1) PRIMARY KEY,
NotificationDate DATE,
UserID INT FOREIGN KEY REFERENCES Users(UserID),
ModuleID INT FOREIGN KEY REFERENCES ModuleInformation(ModuleID)
);
create table StudyRecord(
StudyRecordID INT IDENTITY(1,1) PRIMARY KEY,
StudyDates DATE,
HoursStudied INT,
UserID INT FOREIGN KEY REFERENCES Users(UserID),
ModuleID INT FOREIGN KEY REFERENCES ModuleInformation(ModuleID)
);

SELECT * FROM ModuleInformation

SELECT * FROM SemesterDates

SELECT * FROM Users

SELECT * FROM StudyRecord

SELECT * FROM Notification

DELETE FROM Users
WHERE UserID = 27;

DBCC CHECKIDENT ('Notification', RESEED, 15);