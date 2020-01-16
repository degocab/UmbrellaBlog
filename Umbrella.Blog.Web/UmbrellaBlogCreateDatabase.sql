USE master
GO

--dropping database if it exists
IF EXISTS(select * from sys.databases where name='UmbrellaBlog')
DROP DATABASE UmbrellaBlog
GO
--creating the database
CREATE DATABASE UmbrellaBlog
GO