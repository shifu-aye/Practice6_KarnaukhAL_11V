USE [master]
GO
/****** Object:  Database [UchPracticeDB]    Script Date: 06.11.2022 22:53:58 ******/
CREATE DATABASE [UchPracticeDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UchPracticeDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\UchPracticeDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'UchPracticeDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\UchPracticeDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [UchPracticeDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UchPracticeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UchPracticeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UchPracticeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UchPracticeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UchPracticeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UchPracticeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [UchPracticeDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UchPracticeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UchPracticeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UchPracticeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UchPracticeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UchPracticeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UchPracticeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UchPracticeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UchPracticeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UchPracticeDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UchPracticeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UchPracticeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UchPracticeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UchPracticeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UchPracticeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UchPracticeDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UchPracticeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UchPracticeDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UchPracticeDB] SET  MULTI_USER 
GO
ALTER DATABASE [UchPracticeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UchPracticeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UchPracticeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UchPracticeDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UchPracticeDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [UchPracticeDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [UchPracticeDB] SET QUERY_STORE = OFF
GO
USE [UchPracticeDB]
GO
/****** Object:  Table [dbo].[Correspondents]    Script Date: 06.11.2022 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Correspondents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SurnameNameLastname] [nvarchar](50) NOT NULL,
	[Position] [nvarchar](50) NOT NULL,
	[Division] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Correspondents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocTable]    Script Date: 06.11.2022 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentName] [nvarchar](50) NOT NULL,
	[FK_Orders] [int] NOT NULL,
	[FK_Correspondent] [int] NOT NULL,
 CONSTRAINT [PK_DocTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 06.11.2022 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [date] NOT NULL,
	[OrderSource] [nvarchar](50) NOT NULL,
	[OrderEvent] [nvarchar](50) NOT NULL,
	[FK_Correspondent] [int] NOT NULL,
	[OrderCompletedDate] [date] NULL,
	[OrderIsCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DocTable]  WITH CHECK ADD  CONSTRAINT [FK_DocTable_Correspondents] FOREIGN KEY([FK_Correspondent])
REFERENCES [dbo].[Correspondents] ([Id])
GO
ALTER TABLE [dbo].[DocTable] CHECK CONSTRAINT [FK_DocTable_Correspondents]
GO
ALTER TABLE [dbo].[DocTable]  WITH CHECK ADD  CONSTRAINT [FK_DocTable_Orders] FOREIGN KEY([FK_Orders])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[DocTable] CHECK CONSTRAINT [FK_DocTable_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Correspondents] FOREIGN KEY([FK_Correspondent])
REFERENCES [dbo].[Correspondents] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Correspondents]
GO
USE [master]
GO
ALTER DATABASE [UchPracticeDB] SET  READ_WRITE 
GO
