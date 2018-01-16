/*
	DATABASE NOTAS 
*/

CREATE DATABASE NOTAS_DB
ON PRIMARY(
	NAME = 'notas_db.mdf',
	FILENAME = 'C:\dbs\notas_db.mdf'
)

LOG ON(
	NAME = 'notas_db.ldf',
	FILENAME = 'C:\dbs\notas_db.ldf'
)

GO

USE NOTAS_DB
GO

CREATE TABLE notas(
	id_nota int not null identity(1,1) primary key,
	titulo varchar(50),
	contenido varchar(1000)
)
GO


-- consultas

CREATE PROC insertarNota
@title as varchar,@content as varchar
as
	--set nocount on
	insert into notas(titulo,contenido)values(@title,@content);
	--print 'INSERTADO'
GO

alter PROC insertarNota
@title as varchar,@content as varchar
as
	set nocount on --no muestra mensajes automaticos por parte del query que se ejecute DML
	insert into notas(titulo,contenido)values(@title,@content);
	print 'INSERTADO'
GO



SELECT * FROM notas
GO

exec insertarNota 'musica','muse'
GO


alter function primir (@msg1 as varchar(100),@msg2 as varchar(100))
returns varchar(100)
as
begin
	declare @cadena as varchar
	set @cadena = (@msg1 + @msg2)
	return @cadena
end;
go

print dbo.primir('hello','world')