create database AlifAcademyLessons

go

use AlifAcademyLessons;

create table Clients
(
	Id int primary key identity not null,
	LastName nvarchar(200) not null,
	FirstName nvarchar (200) not null,
	MiddleName nvarchar(200) not null,
	BirthDate DateTime not null,
	Created_At DateTime not null
);

create table Currencies
(
	Id int primary key identity not null,
	Currency nvarchar(200) not null
);

create table Accounts 
(
	Id int primary key identity not null,
	Client_Id int not null,
	Account_Number nvarchar(200) not null,
	Currency_Id int not null,
	Created_At datetime  not null,
	Updated_At datetime not null,
	foreign key (Client_Id) references Clients(Id),
	foreign key (Currency_Id) references Currencies(Id)
);