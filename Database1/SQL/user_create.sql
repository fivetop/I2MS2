USE [i2ms2]
GO
/****** Object:  User [i2ms]    Script Date: 2015-01-22 오후 1:56:32 ******/
CREATE USER [i2ms] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [i2ms2]    Script Date: 2015-01-22 오후 1:56:32 ******/
CREATE USER [i2ms2] FOR LOGIN [i2ms2] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [i2ms2]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [i2ms2]
GO
