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

create table Location
(
id int primary key identity(1, 1),
City varchar(30),
StreenName varchar(30),
StreenNr varchar(5),
);

create table Specialization
(
id int primary key identity(1, 1), 
Description varchar(100),
);

create table MedicalService
(
id int primary key identity(1, 1),
UserId int,
LocationId int,
SpecializationId int,
Name varchar(50),
Description varchar(50),
DayStartTime time, 
DayEndTime time,
AppDefaultMinutes time,

FOREIGN KEY (UserId) REFERENCES Users(id),
FOREIGN KEY (LocationId) REFERENCES Location(id),
FOREIGN KEY (SpecializationId) REFERENCES Specialization(id)
);

create table Review
(
id int primary key identity(1, 1),
UserId int,
MedicalServiceId int,
Comment varchar(50),
Rating int

FOREIGN KEY (userId) REFERENCES Users(id),
FOREIGN KEY (MedicalServiceId) REFERENCES MedicalService(id)
);

create table Appointment
(
id int primary key identity(1, 1), 
PacientId int,
MedicalServiceId int,
TimeOfAppointment datetime, 
AppointmentDuration time,
Description varchar(50)

FOREIGN KEY (PacientId) REFERENCES Users(id),
FOREIGN KEY (MedicalServiceId) REFERENCES MedicalService(id)
);


