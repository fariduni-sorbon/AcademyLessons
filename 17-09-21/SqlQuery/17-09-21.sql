use AcademySummer;

create table Account 
(
	Id int identity primary key not null,
	Account_Name nvarchar(100)not null,
	Is_Active int not null, 
	Balance int not null,
	Created_At datetime not null,
	Updated_At datetime  null
)


create table Transactions
(
	Id int identity primary key  not null,
	Account_Id int references Account(Id),
	Amount decimal(18,2) not null,
	Created_At datetime not null
)