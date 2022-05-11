DROP TABLE IF EXISTS [dbo].[Authorizations];
CREATE TABLE [dbo].[Authorizations](
	[ID] [int] Primary Key IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](max) DEFAULT 'admin',
	[Password] [nvarchar](max) DEFAULT 'admin',
	[Access] [nvarchar](max) DEFAULT 'Полный',
	[IdDoctor] [int] DEFAULT 1);
	GO

INSERT INTO [dbo].[Authorizations]
           ([Login]
           ,[Password]
           ,[Access]
           ,[IdDoctor])
     VALUES
           ('admin'
           ,'admin'
           ,'Полный'
           ,1);

GO
DROP TABLE IF EXISTS [dbo].[Doctor];
GO
CREATE TABLE [dbo].[Doctor](
	[ID] [int] Primary Key IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Patronymic] [nvarchar](max) NULL,
	[Phone] [nvarchar](11) NULL,
	[img] [nvarchar](max) DEFAULT 'user.jpg',
	[ChartWork] [nvarchar](max) NULL,
	[Specialization] [nvarchar](max) null);
	GO
	DROP TABLE IF EXISTS [dbo].[Patient];
GO
CREATE TABLE [dbo].[Patient](
	[ID] [int] Primary Key IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Patronymic] [nvarchar](max) NULL,
	[Passport] [int] NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](11) NULL,
	[Policy_number] [int] NULL,
	[Card_number] [int] NULL,
	[Sex] [nvarchar](10) NULL,
	[DateBirth] [nvarchar](max) NOT NULL);
	GO
	DROP TABLE IF EXISTS [dbo].[Diseases];
GO
	CREATE TABLE [dbo].[Diseases](
	[ID] [int] Primary Key IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL);
	GO
	
	DROP TABLE IF EXISTS [dbo].[Record];
GO
	CREATE TABLE [dbo].[Record](
	[ID] [int] Primary Key IDENTITY(1,1) NOT NULL,
	[ID_Doctor] [int] NOT NULL,
	[ID_Patient] [int] NOT NULL,
	[ID_Diseases] [int] NOT NULL,
	[Date_Time] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL);
GO
	