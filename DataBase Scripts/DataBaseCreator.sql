create database MedBookDB 
use MedBookDB

create table Users
(
id int primary key identity(1, 1),
FirstName varchar(50),
LastName varchar(50),
Email varchar(50),
Password varchar(200),
UserType int
);

create table MedicalService
(
id int primary key identity(1, 1),
UserId int,
Name varchar(50),
Description varchar(50),
DayStartTime time, 
DayEndTime time,
AppDefaultMinutes time,

FOREIGN KEY (UserId) REFERENCES Users(id)
);

create table Location
(
id int primary key identity(1, 1),
MedicalServiceId int,
City varchar(30),
StreenName varchar(30),
StreenNr varchar(5),
FOREIGN KEY (MedicalServiceId) REFERENCES MedicalService(id)
);

create table Review
(
id int primary key identity(1, 1),
MedicalServiceId int,
Comment varchar(50),
Rating int

FOREIGN KEY (MedicalServiceId) REFERENCES MedicalService(id)
);

create table Appointment
(
id int primary key identity(1, 1), 
PacientId int,
MedicalServiceId int,
Description varchar(50),
TimeOfAppointment datetime, 
AppointmentDuration time

FOREIGN KEY (PacientId) REFERENCES Users(id),
FOREIGN KEY (MedicalServiceId) REFERENCES MedicalService(id)
);