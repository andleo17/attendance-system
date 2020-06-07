USE MASTER
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name='DBAttendance')
	DROP DATABASE DBAttendance
GO

CREATE DATABASE DBAttendance
GO

USE DBAttendance
GO

CREATE TABLE "Employee" (
	"CardId"	CHAR(8)		NOT NULL PRIMARY KEY,
	"Name"		VARCHAR(30)	NOT NULL,
	"Lastname"	VARCHAR(60)	NOT NULL,
	"Genre"		CHAR(1)		NOT NULL,
	"BirthDate"	DATE		NOT NULL,
	"Address"	VARCHAR(50)	NOT NULL,
	"Phone"		VARCHAR(13)	NOT NULL,
	"Email"		VARCHAR(50)	NULL,
	"State"		BIT			NOT NULL DEFAULT TRUE
)
GO

CREATE TABLE "Contract" (
	"Id"				INTEGER 		NOT NULL IDENTITY PRIMARY KEY,
	"StartDate"			DATE			NOT NULL,
	"FinishDate" 		DATE			NOT NULL,
	"Mount"				DECIMAL(8, 2)	NOT NULL,
	"State"				BIT				NOT NULL DEFAULT TRUE,
	"ExtraHours"		BIT				NOT NULL,
	"EmployeeCardId"	CHAR(8)			NOT NULL REFERENCES "Employee"
)
GO

CREATE TABLE "Schedule" (
	"Id" 				INTEGER		NOT NULL IDENTITY PRIMARY KEY,
	"StartDate"			DATE		NOT NULL,
	"FinishDate"		DATE		NULL,
	"State"				BIT			NOT NULL DEFAULT TRUE,
	"EmployeeCardId"	CHAR(8)		NOT NULL REFERENCES "Employee"
)
GO

CREATE TABLE "ScheduleDetail" (
	"Id"			INTEGER		NOT NULL IDENTITY PRIMARY KEY,
	"ScheduleId"	INTEGER		NOT NULL REFERENCES "Schedule",
	"Day"			TINYINT		NOT NULL,
	"InHour"		TIME		NOT NULL,
	"OutHour"		TIME		NOT NULL
)
GO

CREATE TABLE "Attendance" (
	"Id"				INTEGER		NOT NULL IDENTITY PRIMARY KEY,
	"Date"				DATE		NOT NULL,
	"InHour"			TIME		NOT NULL,
	"OutHour"			TIME		NULL,
	"EmployeeCardId"	CHAR(8)		NOT NULL REFERENCES "Employee",
	CONSTRAINT Key_Attendance_Date_EmployeeCardId
		UNIQUE ("Date", "EmployeeCardId")
)
GO

CREATE TABLE "Justification" (
	"Id"			INTEGER		NOT NULL IDENTITY PRIMARY KEY,
	"Date"			DATE		NOT NULL,
	"Motive"		TEXT		NOT NULL,
	"State"			BIT			NOT NULL DEFAULT TRUE,
	"AttendanceId"	INTEGER		NOT NULL REFERENCES "Attendance"
)
GO

CREATE TABLE "Permission" (
	"Id"				INTEGER			NOT NULL IDENTITY PRIMARY KEY,
	"PresentationDate"	DATE			NOT NULL,
	"Date"				DATE			NOT NULL,
	"Motive"			VARCHAR(100)	NOT NULL,
	"State"				BIT				NOT NULL DEFAULT FALSE,
	"EmployeeCardId"	CHAR(8)			NOT NULL REFERENCES "Employee"
)
GO

CREATE TABLE "LicenseType" (
	"Id"			TINYINT		NOT NULL IDENTITY PRIMARY KEY,
	"Description"	VARCHAR(50)	NOT NULL,
	"MaximumDays"	SMALLINT	NOT NULL
)
GO

CREATE TABLE "License" (
	"Id"				INTEGER		NOT NULL IDENTITY PRIMARY KEY,
	"PresentationDate"	DATETIME	NOT NULL,
	"StartDate"			DATE		NOT NULL,
	"FinishDate"		DATE		NOT NULL,
	"State"				BIT			NOT NULL DEFAULT FALSE,
	"Document"			VARCHAR(50)	NOT NULL,
	"EmployeeCardId"	CHAR(8)		NOT NULL REFERENCES "Employee",
	"LicenseTypeId"		TINYINT		NOT NULL REFERENCES "LicenseType"
)
GO

CREATE TABLE "User" (
	"Id"				SMALLINT	NOT NULL IDENTITY PRIMARY KEY,
	"Name"				VARCHAR(30)	NOT NULL,
	"Password"			VARCHAR(30)	NOT NULL,
	"State"				BIT			NOT NULL DEFAULT 1,
	"EmployeeCardId"	CHAR(8)		NOT NULL REFERENCES "Employee"
)
GO

INSERT INTO "Employee" VALUES
	('12345678', 'Panchito', 'Panchitus Pachas', 'M', '2000-07-01', 'Panamericana 123', '976544354', 'pancho@domain.com', 1)
GO

INSERT INTO "User" VALUES
	('admin', 'admin', 1, '12345678')
GO

CREATE TRIGGER StateChangeContractTrigger ON "Contract"
	AFTER INSERT AS
	DECLARE @id INT;
	DECLARE @cardid CHAR(8);
	DECLARE @iid INT;
BEGIN
	SELECT @iid = "Id", @cardid = "EmployeeCardId" FROM inserted;
	SELECT TOP 1 @id = "Id" FROM "Contract" WHERE "State" = 1 AND "EmployeeCardId" = @cardid AND "Id" <> @iid ORDER BY "Id";
	IF @id IS NOT NULL
		UPDATE "Contract" SET "State" = 0 WHERE "Id" = @id;
END
GO

CREATE TRIGGER StateChangeUserTrigger ON "User"
	AFTER INSERT AS
	DECLARE @id INT;
	DECLARE @cardid CHAR(8);
	DECLARE @iid INT;
BEGIN
	SELECT @iid = "Id", @cardid = "EmployeeCardId" FROM inserted;
	SELECT TOP 1 @id = "Id" FROM "User" WHERE "State" = 1 AND "EmployeeCardId" = @cardid AND "Id" <> @iid ORDER BY "Id";
	IF @id IS NOT NULL
		UPDATE "User" SET "State" = 0 WHERE "Id" = @id;
END
GO

CREATE TRIGGER StateChangeScheduleTrigger ON "Schedule"
	AFTER INSERT AS
	DECLARE @id INT;
	DECLARE @cardid CHAR(8);
	DECLARE @iid INT;
BEGIN
	SELECT @iid = "Id", @cardid = "EmployeeCardId" FROM inserted;
	SELECT TOP 1 @id = "Id" FROM "Schedule" WHERE "State" = 1 AND "EmployeeCardId" = @cardid AND "Id" <> @iid ORDER BY "Id";
	IF @id IS NOT NULL
		UPDATE "Schedule" SET "State" = 0 WHERE "Id" = @id;
END
GO