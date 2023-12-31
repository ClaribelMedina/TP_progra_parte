USE [Cordoba_Cine_GRUPO_N9]
GO
/****** Object:  StoredProcedure [dbo].[SP_PROXIMO_DETALLE_COMPROBANTE]    Script Date: 20/11/2023 17:13:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_PROXIMO_DETALLE_COMPROBANTE]
@proxDetalle int OUTPUT
AS
BEGIN
	SET @proxDetalle = (SELECT MAX(id_detalle_comprobante)+1  FROM DETALLES_COMPROBANTES);
END

create procedure [dbo].[SP_INSERTAR_COMPROBANTE]
@fecha datetime,
@idCliente int,
@idSucursal int ,
@idFormaPago int,
@idVendedor int
as
begin
      insert into COMPROBANTES (fecha_compra, id_cliente,id_sucursal, id_forma_pago, id_vendedor)
	  values( @fecha, @idCliente, @idSucursal, @idFormaPago, @idVendedor)
end

create procedure [dbo].[SP_INSERTAR_DETALLE_COMPROBANTE]
@idDetalle int,
@idFuncion int,
@idComprobante int,
@idPromo int
as
begin
      insert into DETALLES_COMPROBANTES(id_detalle_comprobante, id_funcion,id_comprobante,id_promo) 
	  values (@idDetalle, @idFuncion, @idComprobante,@idPromo)
end

create PROCEDURE [dbo].[SP_CLIENTES]
AS
SELECT * 
FROM CLIENTES

create PROCEDURE [dbo].[SP_F_PAGOS]
AS
SELECT * 
FROM FORMAS_PAGO

create PROCEDURE  [dbo].[SP_FUNCIONES_FILTRADAS]
@IdPeli int

as
begin
  select id_funcion, nombre_pelicula, format(fecha,'dd-MM-yyyy') Fecha , inicio , final, s.id_sala, ts.tipo, f.subtitulo, h.id_horario,
          ts.id_tipo_sala ,p.id_pelicula,ts.precio
  from funciones f join peliculas p on f.id_pelicula=p.id_pelicula 
                   join salas s on s.id_sala=f.id_sala
				   join tipos_salas ts on ts.id_tipo_sala=s.id_tipo_sala
				   join horarios h on h.id_horario = f.id_horario
  where p.id_pelicula = @IdPeli
end


create PROCEDURE [dbo].[SP_PELICULAS]
AS
SELECT * 
FROM peliculas

create PROCEDURE [dbo].[SP_PROMOS]
AS
SELECT nombre_promo, id_promo, descuento
FROM promos	

create PROCEDURE [dbo].[SP_PROXIMO_COMPROBANTE]
@next int OUTPUT
AS
BEGIN
	SET @next = (SELECT MAX(id_comprobante)+1  FROM COMPROBANTES);
END

create procedure [dbo].[SP_EXISTE_DOCUMENTO]
@numDoc int ,
@siExiste bit output
as
begin
     if (exists (select id_cliente from clientes where nro_doc= @numDoc ))
	   set @siExiste = 1;  
	 else
	   set @siExiste =0;
	 

end

create PROCEDURE [dbo].[SP_SALAS_FILTRADAS]
@IdTipoSala int
as
begin
      select id_sala, ts.id_tipo_sala, tipo,precio
	  from salas s join tipos_salas ts on s.id_tipo_sala=ts.id_tipo_sala
	  where ts.id_tipo_sala= @IdTipoSala
end

create PROCEDURE [dbo].[SP_SUCURSALES]
AS
SELECT * 
FROM sucursales


create procedure [dbo].[SP_SALA_FILT_POR_FUNCION]
@IdFuncion int
as
begin
      select  tipo, ts.id_tipo_sala, ts.precio
      from funciones f join peliculas p on f.id_pelicula=p.id_pelicula 
                   join salas s on s.id_sala=f.id_sala
				   join tipos_salas ts on ts.id_tipo_sala=s.id_tipo_sala
	  where id_funcion= @IdFuncion;
end


create PROCEDURE [dbo].[SP_VENDEDORES]
AS
SELECT * 
FROM VENDEDORES


create PROCEDURE [dbo].[SP_CLIENTE_POR_DOCUMENTO]
@numDoc int 
as
begin
     select nombre , apellido
	 from clientes
	 where nro_doc = @numDoc
end


create procedure [dbo].[SP_CONSULTAR_DETALLES]
@id_comprobante int
as
begin
      select * from DETALLES_COMPROBANTES
	  where id_comprobante= @id_comprobante
end


