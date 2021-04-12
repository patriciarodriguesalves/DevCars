IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [tb_Car] (
    [Id] int NOT NULL IDENTITY,
    [VinCode] nvarchar(max) NULL,
    [Marca] VARCHAR(100) NOT NULL DEFAULT 'Genérico',
    [Model] nvarchar(max) NULL,
    [Year] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Color] nvarchar(max) NULL,
    [ProductionDate] datetime2 NOT NULL DEFAULT (getDate()),
    [Status] int NOT NULL,
    [RegisteredAt] datetime2 NOT NULL,
    CONSTRAINT [PK_tb_Car] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [tb_Customer] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(max) NULL,
    [Document] nvarchar(max) NULL,
    [BirthDate] datetime2 NOT NULL,
    CONSTRAINT [PK_tb_Customer] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [tb_Order] (
    [Id] int NOT NULL IDENTITY,
    [IdCar] int NOT NULL,
    [IdCustomer] int NOT NULL,
    [TotalCost] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_tb_Order] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_tb_Order_tb_Car_IdCar] FOREIGN KEY ([IdCar]) REFERENCES [tb_Car] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_tb_Order_tb_Customer_IdCustomer] FOREIGN KEY ([IdCustomer]) REFERENCES [tb_Customer] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [tb_ExtraOrderItem] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [IdOrder] int NOT NULL,
    CONSTRAINT [PK_tb_ExtraOrderItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_tb_ExtraOrderItem_tb_Order_IdOrder] FOREIGN KEY ([IdOrder]) REFERENCES [tb_Order] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_tb_ExtraOrderItem_IdOrder] ON [tb_ExtraOrderItem] ([IdOrder]);
GO

CREATE UNIQUE INDEX [IX_tb_Order_IdCar] ON [tb_Order] ([IdCar]);
GO

CREATE INDEX [IX_tb_Order_IdCustomer] ON [tb_Order] ([IdCustomer]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210409203247_InitialMigration', N'5.0.5');
GO

COMMIT;
GO

