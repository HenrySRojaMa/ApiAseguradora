USE [master]
GO
/****** Object:  Database [Seguros]    Script Date: 26/10/2024 20:07:31 ******/
CREATE DATABASE [Seguros]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Seguros', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Seguros.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Seguros_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Seguros_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Seguros] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Seguros].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Seguros] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Seguros] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Seguros] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Seguros] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Seguros] SET ARITHABORT OFF 
GO
ALTER DATABASE [Seguros] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Seguros] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Seguros] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Seguros] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Seguros] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Seguros] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Seguros] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Seguros] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Seguros] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Seguros] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Seguros] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Seguros] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Seguros] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Seguros] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Seguros] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Seguros] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Seguros] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Seguros] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Seguros] SET  MULTI_USER 
GO
ALTER DATABASE [Seguros] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Seguros] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Seguros] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Seguros] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Seguros] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Seguros] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Seguros] SET QUERY_STORE = OFF
GO
USE [Seguros]
GO
/****** Object:  Table [dbo].[Aseguradora]    Script Date: 26/10/2024 20:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aseguradora](
	[IdSeguro] [int] NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Cobertura] [decimal](18, 2) NULL,
	[Prima] [decimal](18, 2) NULL,
	[Estado] [varchar](1) NULL,
 CONSTRAINT [PK_Aseguradora] PRIMARY KEY CLUSTERED 
(
	[IdSeguro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 26/10/2024 20:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [int] NOT NULL,
	[Cedula] [varchar](10) NULL,
	[Nombre] [varchar](100) NULL,
	[Telefono] [varchar](50) NULL,
	[edad] [int] NULL,
	[Estado] [varchar](1) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contrato]    Script Date: 26/10/2024 20:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contrato](
	[IdContrato] [int] NOT NULL,
	[IdCliente] [int] NULL,
	[IdSeguro] [int] NULL,
	[Estado] [varchar](1) NULL,
 CONSTRAINT [PK_Contrato] PRIMARY KEY CLUSTERED 
(
	[IdContrato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contrato]  WITH CHECK ADD  CONSTRAINT [FK_Contrato_Aseguradora] FOREIGN KEY([IdSeguro])
REFERENCES [dbo].[Aseguradora] ([IdSeguro])
GO
ALTER TABLE [dbo].[Contrato] CHECK CONSTRAINT [FK_Contrato_Aseguradora]
GO
ALTER TABLE [dbo].[Contrato]  WITH CHECK ADD  CONSTRAINT [FK_Contrato_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
ALTER TABLE [dbo].[Contrato] CHECK CONSTRAINT [FK_Contrato_Cliente]
GO
USE [master]
GO
ALTER DATABASE [Seguros] SET  READ_WRITE 
GO
