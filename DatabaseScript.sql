create database VerzelVehicles;
use VerzelVehicles;

-- Begin Create tables
create table Brands (
	[Id] int identity(1,1),
	[Name] varchar(100) not null,
	
	primary key ([Id])
);

create table Vehicles (
	[Id] int identity(1,1),
	[Name] varchar(255) not null,
	[Model] varchar(255) not null,
	[Price] int not null,
	[BrandId] int not null,

	primary key ([Id]),
	foreign key ([BrandId]) references Brands([Id])
);


create table Images (
	[Id] int identity(1,1),
	[Filename] varchar(255) not null unique,
	[VehicleId] int not null,

	primary key ([Id]),
	foreign key (VehicleId) references Vehicles([Id])
);

create table Users (
	[Id] int identity(1,1),
	[Username] varchar(255) not null,
	[PasswordHash] varchar(255) not null,

	primary key ([Id])
);
-- End Create tables

-- Begin Create constraints
create unique index idx_users_username on Users ([Username]);
-- End Create constraints



-- Begin Populate Database
-- Populate Brands
insert into Brands values ('Honda');
insert into Brands values ('Ford');

-- Populate Vehicles
insert into Vehicles values ('Civic', 'Sedan', 8000000, 1);
insert into Vehicles values ('Ka', 'Sedan', 4000000, 2);

-- Populate Images
insert into Images values ('xpto.jpg', 1);
insert into Images values ('xpto1.jpg', 1);
insert into Images values ('xpto2.jpg', 1);
insert into Images values ('xpto3.jpg', 1);

-- Populate Users
insert into Users values ('Admin', 'Admin');
-- End Populate Database