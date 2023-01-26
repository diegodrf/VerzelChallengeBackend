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
insert into Brands values ('CHEVROLET');
insert into Brands values ('VOLKSWAGEN');
insert into Brands values ('FIAT');
insert into Brands values ('MERCEDES-BENZ');
insert into Brands values ('CITROEN');
insert into Brands values ('CHANA');
insert into Brands values ('HONDA');
insert into Brands values ('SUBARU');
insert into Brands values ('FERRARI');
insert into Brands values ('BUGATTI');
insert into Brands values ('LAMBORGHINI');
insert into Brands values ('FORD');
insert into Brands values ('HYUNDAI');
insert into Brands values ('JAC');
insert into Brands values ('KIA');
insert into Brands values ('GURGEL');
insert into Brands values ('DODGE');
insert into Brands values ('CHRYSLER');
insert into Brands values ('BENTLEY');
insert into Brands values ('SSANGYONG');
insert into Brands values ('PEUGEOT');
insert into Brands values ('TOYOTA');
insert into Brands values ('RENAULT');
insert into Brands values ('ACURA');
insert into Brands values ('ADAMO');
insert into Brands values ('AGRALE');
insert into Brands values ('ALFA ROMEO');
insert into Brands values ('AMERICAR');
insert into Brands values ('ASTON MARTIN');
insert into Brands values ('AUDI');
insert into Brands values ('BEACH');
insert into Brands values ('BIANCO');
insert into Brands values ('BMW');
insert into Brands values ('BORGWARD');
insert into Brands values ('BRILLIANCE');
insert into Brands values ('BUICK');
insert into Brands values ('CBT');
insert into Brands values ('NISSAN');
insert into Brands values ('CHAMONIX');
insert into Brands values ('CHEDA');
insert into Brands values ('CHERY');
insert into Brands values ('CORD');
insert into Brands values ('COYOTE');
insert into Brands values ('CROSS LANDER');
insert into Brands values ('DAEWOO');
insert into Brands values ('DAIHATSU');
insert into Brands values ('VOLVO');
insert into Brands values ('DE SOTO');
insert into Brands values ('DETOMAZO');
insert into Brands values ('DELOREAN');
insert into Brands values ('DKW-VEMAG');
insert into Brands values ('SUZUKI');
insert into Brands values ('EAGLE');
insert into Brands values ('EFFA');
insert into Brands values ('ENGESA');
insert into Brands values ('ENVEMO');
insert into Brands values ('FARUS');
insert into Brands values ('FERCAR');
insert into Brands values ('FNM');
insert into Brands values ('PONTIAC');
insert into Brands values ('PORSCHE');
insert into Brands values ('GEO');
insert into Brands values ('GRANCAR');
insert into Brands values ('GREAT WALL');
insert into Brands values ('HAFEI');
insert into Brands values ('HOFSTETTER');
insert into Brands values ('HUDSON');
insert into Brands values ('HUMMER');
insert into Brands values ('INFINITI');
insert into Brands values ('INTERNATIONAL');
insert into Brands values ('JAGUAR');
insert into Brands values ('JEEP');
insert into Brands values ('JINBEI');
insert into Brands values ('JPX');
insert into Brands values ('KAISER');
insert into Brands values ('KOENIGSEGG');
insert into Brands values ('LAUTOMOBILE');
insert into Brands values ('LAUTOCRAFT');
insert into Brands values ('LADA');
insert into Brands values ('LANCIA');
insert into Brands values ('LAND ROVER');
insert into Brands values ('LEXUS');
insert into Brands values ('LIFAN');
insert into Brands values ('LINCOLN');
insert into Brands values ('LOBINI');
insert into Brands values ('LOTUS');
insert into Brands values ('MAHINDRA');
insert into Brands values ('MASERATI');
insert into Brands values ('MATRA');
insert into Brands values ('MAYBACH');
insert into Brands values ('MAZDA');
insert into Brands values ('MENON');
insert into Brands values ('MERCURY');
insert into Brands values ('MITSUBISHI');
insert into Brands values ('MG');
insert into Brands values ('MINI');
insert into Brands values ('MIURA');
insert into Brands values ('MORRIS');
insert into Brands values ('MP LAFER');
insert into Brands values ('MPLM');
insert into Brands values ('NEWTRACK');
insert into Brands values ('NISSIN');
insert into Brands values ('OLDSMOBILE');
insert into Brands values ('PAG');
insert into Brands values ('PAGANI');
insert into Brands values ('PLYMOUTH');
insert into Brands values ('PUMA');
insert into Brands values ('RENO');
insert into Brands values ('REVA-I');
insert into Brands values ('ROLLS-ROYCE');
insert into Brands values ('ROMI');
insert into Brands values ('SEAT');
insert into Brands values ('UTILITARIOS AGRICOLAS');
insert into Brands values ('SHINERAY');
insert into Brands values ('SAAB');
insert into Brands values ('SHORT');
insert into Brands values ('SIMCA');
insert into Brands values ('SMART');
insert into Brands values ('SPYKER');
insert into Brands values ('STANDARD');
insert into Brands values ('STUDEBAKER');
insert into Brands values ('TAC');
insert into Brands values ('TANGER');
insert into Brands values ('TRIUMPH');
insert into Brands values ('TROLLER');
insert into Brands values ('UNIMOG');
insert into Brands values ('WIESMANN');
insert into Brands values ('CADILLAC');
insert into Brands values ('AM GEN');
insert into Brands values ('BUGGY');
insert into Brands values ('WILLYS OVERLAND');
insert into Brands values ('KASEA');
insert into Brands values ('SATURN');
insert into Brands values ('SWELL MINI');
insert into Brands values ('SKODA');
insert into Brands values ('KARMANN GHIA');
insert into Brands values ('KART');
insert into Brands values ('HANOMAG');
insert into Brands values ('OUTROS');
insert into Brands values ('HILLMAN');
insert into Brands values ('HRG');
insert into Brands values ('GAIOLA');
insert into Brands values ('TATA');
insert into Brands values ('DITALLY');
insert into Brands values ('RELY');
insert into Brands values ('MCLAREN');
insert into Brands values ('GEELY');

-- Populate Vehicles
insert into Vehicles values ('Tracker', 'SUV', 8000000, 1);
insert into Vehicles values ('Gol', 'Hatch', 4000000, 2);
insert into Vehicles values ('Palio', 'Fiat', 2850000, 3);
insert into Vehicles values ('Civic', 'Sedan', 9735295, 7);

-- Populate Images
insert into Images values ('163d96556e34464ea61b9cd9b66db044.jfif', 1);
insert into Images values ('764fef22c3c84eb39802b2dc19a29670.jpg', 2);
insert into Images values ('598c9385ef79471aa633dddb0a060157.jpg', 3);
insert into Images values ('2f97f3570b40459785274aec1457c9a3.jpg', 4);

-- Populate Users
insert into Users values ('admin', 'admin');
-- End Populate Database