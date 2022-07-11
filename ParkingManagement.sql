
CREATE DATABASE [ParkingManagement]
GO
USE [ParkingManagement]
GO

CREATE TABLE [User](
	CCID			NVARCHAR(50)		PRIMARY KEY,
	Password		NVARCHAR(50)		NOT NULL,
	Fullname		NVARCHAR(50)		NOT NULL,
	Status			BIT					NOT NULL,
	IsResident		BIT					NOT NULL
)

CREATE TABLE [Manager](
	Username		NVARCHAR(50)		PRIMARY KEY,
	Password		NVARCHAR(50)		NOT NULL,
	Status			BIT					NOT NULL,
	Type			NVARCHAR(50)		NOT NULL
)


CREATE TABLE [Vehicle](
	VehicleID		NVARCHAR(50)		PRIMARY KEY,		
	Type			NVARCHAR(50)		NOT NULL,
	CCID			NVARCHAR(50)		REFERENCES [User](CCID),
)

CREATE TABLE [Pricing](
	PricingID		INT					PRIMARY KEY IDENTITY(1,1),
	DurationType	NVARCHAR(50)		NOT NULL,
	Value			DECIMAL				NOT NULL,
	VehicleType		NVARCHAR(50)		NOT NULL
)

CREATE TABLE [Slot](
	SlotID			INT					PRIMARY KEY IDENTITY(1,1),
	Floor			INT					NOT NULL,
	Position		NVARCHAR(20)		NOT NULL,
	IsOccupied		BIT					NOT NULL,
	IsBook			BIT					NOT NULL,
	IsActive		BIT					NOT NULL
)

CREATE TABLE [ParkingInfo](
	ParkingInfoID	INT					PRIMARY KEY IDENTITY,
	CCID			NVARCHAR(50)		REFERENCES [User](CCID),
	VehicleID		NVARCHAR(50)		REFERENCES [Vehicle](VehicleID),
	SlotID			INT					REFERENCES [Slot](SlotID),
	PricingID		INT					REFERENCES [Pricing](PricingID),
	CheckInTime		DATETIME			NOT NULL,
	CheckOutTime	DATETIME			,
	IsMonthly		BIT					NOT NULL,
	HaveVehicle		BIT					NOT NULL,
	Status			NVARCHAR(10)		NOT NULL,
	TotalPrice		DECIMAL				NOT NULL
)

GO
INSERT INTO [User] VALUES ('1234567890', '123', 'ABC', 1, 1);
INSERT INTO [User] VALUES ('9876543210', '123', 'DEF', 1, 1);

GO
INSERT INTO [Manager] VALUES ('Test', '123', 1, 'Admin');
INSERT INTO [Manager] VALUES ('Manager01', '123', 1, 'Staff');

GO
INSERT INTO [Vehicle] VALUES ('30V-12345', 'Car', '1234567890');
INSERT INTO [Vehicle] VALUES ('15-Z2 56987', 'Bike', '1234567890');
INSERT INTO [Vehicle] VALUES ('59-V1 12345', 'Bike', '9876543210');
INSERT INTO [Vehicle] VALUES ('75-Z1 11345', 'Bike', '9876543210');
INSERT INTO [Vehicle] VALUES ('69B-65432', 'Car', '9876543210');

GO
INSERT INTO [Pricing] (DurationType, Value, VehicleType) VALUES ('Temporary', 1000, 'Bike')
INSERT INTO [Pricing] (DurationType, Value, VehicleType) VALUES ('Monthly', 400000, 'Bike')
INSERT INTO [Pricing] (DurationType, Value, VehicleType) VALUES ('Temporary', 10000, 'Car')
INSERT INTO [Pricing] (DurationType, Value, VehicleType) VALUES ('Monthly', 2000000, 'Car')

GO
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A1', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A2', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A3', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A4', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A5', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A6', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A7', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A8', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A9', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('1', 'A10', 0, 0, 1)

INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B1', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B2', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B3', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B4', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B5', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B6', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B7', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B8', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B9', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('2', 'B10', 0, 0, 1)

INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C1', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C2', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C3', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C4', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C5', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C6', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C7', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C8', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C9', 0, 0, 1)
INSERT INTO [Slot] (Floor, Position, IsOccupied, IsBook, IsActive) VALUES ('3', 'C10', 0, 0, 1)
