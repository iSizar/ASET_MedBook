use MedBookDB;

insert Location(City,StreenName,StreenNr) values('Iasi', 'Codrescu', '10');
insert Location(City,StreenName,StreenNr) values('Bucuresti', 'Bl. Mircea Voda', '50');
insert Location(City,StreenName,StreenNr) values('Cluj Napoca', 'Piezisei', '1');
insert Location(City,StreenName,StreenNr) values('Timisoara', 'Bl. Independentei', '3');
insert Location(City,StreenName,StreenNr) values('Iasi', 'Td. Vladimirescu', '20');

insert into MedicalService(UserId, LocationId, Name, Description, TargetBodySystem) values(1, 1, 'Recumed', 'Servicii medicale private', 'Nervous system');
insert into MedicalService(UserId, LocationId, Name, Description, TargetBodySystem) values(1, 1, 'Recumed', 'Servicii medicale private', 'The locomotor system');
insert into MedicalService(UserId, LocationId, Name, Description, TargetBodySystem) values(1, 1, 'Recumed', 'Servicii medicale private','Digestive system');
insert into MedicalService(UserId, LocationId, Name, Description, TargetBodySystem) values(1, 1, 'Recumed', 'Servicii medicale private', 'Respiratory system');
insert into MedicalService(UserId, LocationId, Name, Description, TargetBodySystem) values(1, 1, 'Recumed', 'Servicii medicale private', 'Circulatory system');
insert into MedicalService(UserId, LocationId, Name, Description, TargetBodySystem) values(1, 1, 'Recumed', 'Servicii medicale private', 'The excretory system');
insert into MedicalService(UserId, LocationId, Name, Description, TargetBodySystem) values(1, 1, 'Recumed', 'Servicii medicale private', 'The reproductive system');
insert into MedicalService(UserId, LocationId, Name, Description, TargetBodySystem) values(1, 1, 'Recumed', 'Servicii medicale private', 'The Analyzers');