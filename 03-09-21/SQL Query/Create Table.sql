create database DemoDB

go

use DemoDB;

create table Users
(
	id int identity primary key,
	FirstName nvarchar(100) not null,
	LastName nvarchar (100) not null,
	Age int not null
)