USE [master]
GO

/*  SCRIPT PARA LA CREACI�N DE BASE DE DATOS */

CREATE DATABASE [BD_OCRH_GestionPlanillas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_OCRH_GestionPlanillas', FILENAME = N'F:\Microsoft SQL Server\DATA\BD_OCRH_GestionPlanillas.mdf' , SIZE = 10240KB , FILEGROWTH = 1024KB )
--( NAME = N'BD_OCRH_GestionPlanillas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQL_2016E\MSSQL\DATA\BD_OCRH_GestionPlanillas.mdf' , SIZE = 10240KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BD_OCRH_GestionPlanillas_log', FILENAME = N'F:\Microsoft SQL Server\DATA\BD_OCRH_GestionPlanillas_log.ldf' , SIZE = 6144KB , FILEGROWTH = 10%)
--( NAME = N'BD_OCRH_GestionPlanillas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQL_2016E\MSSQL\DATA\BD_OCRH_GestionPlanillas_log.ldf' , SIZE = 6144KB , FILEGROWTH = 10%)
 COLLATE Modern_Spanish_CI_AS
GO

ALTER DATABASE [BD_OCRH_GestionPlanillas] SET COMPATIBILITY_LEVEL = 120
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET ARITHABORT OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET DISABLE_BROKER 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET READ_WRITE 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET MULTI_USER 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BD_OCRH_GestionPlanillas] SET DELAYED_DURABILITY = DISABLED 
GO

USE [BD_OCRH_GestionPlanillas]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [BD_OCRH_GestionPlanillas] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO


/*	CREACI�N DE LOGIN PARA LA BASE DE DATOS	*/

USE [master]
GO

IF NOT EXISTS (SELECT principal_id FROM sys.sql_logins WHERE name = 'UserOCRH')
BEGIN
	CREATE LOGIN [UserOCRH] WITH PASSWORD=N'123@abc', DEFAULT_DATABASE=[BD_OCRH_GestionPlanillas], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
END
ELSE
BEGIN
	PRINT ('Ya existe el LOGIN UserOCRH en master.  Se omiti� la creaci�n')
END
GO


/*	CREACI�N DE USUARIO DE BASE DE DATOS	*/

USE [BD_OCRH_GestionPlanillas]
GO

IF NOT EXISTS (SELECT uid FROM sys.sysusers WHERE name = 'UserOCRH' AND issqluser = 1)
BEGIN

	CREATE USER [UserOCRH] FOR LOGIN [UserOCRH] WITH DEFAULT_SCHEMA=[dbo]

	ALTER ROLE [db_owner] ADD MEMBER [UserOCRH]

	ALTER ROLE [db_datareader] ADD MEMBER [UserOCRH]

	ALTER ROLE [db_datawriter] ADD MEMBER [UserOCRH]

END
ELSE
BEGIN
	PRINT ('Ya existe el usuario UserOCRH en BD_OCRH_GestionPlanillas. Se omiti� la creaci�n')
END
GO