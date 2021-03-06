USE [BTOB]
GO
select * from [Catalogos].[Articulos]
/****** Object:  StoredProcedure [Catalogos].[SP_Articulos_Obtener]    Script Date: 02/06/2022 10:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure Catalogos.SP_Articulos_Obtener AS
BEGIN 
select top 100  
	   e.idempresa as empresa,
       IDArticulo as producto,
       CodBarras as barcode,
       Descripcion as descripcion_1,
       Departamento as categoria,
       Seccion as subcategoria,
       Convert(varchar(50), 'unidad') as uom ,
       Referencia as referencia,
       'NA' as estilo,
       Color as color,
       Talla as talla,
       e.Nombre as marca,
       'NA' as temporada,
       Familia as familia
	from [Catalogos].[Articulos] a
	 inner join Marca.Empresas e on a.IDEmpresa=e.IDEmpresa
	  where  A.IDEmpresa IN ('155687558220201','155687558220202','155687558220203','155687558220204','' )
END 
go
CREATE procedure Catalogos.SP_Codigo_Articulos_Obtener AS
BEGIN 
select top 100  
	   e.idempresa as empresa,
       IDArticulo as producto,
       CodBarras as codigo,
       GETDATE() as fecha_transmision
	from [Catalogos].[Articulos] a
	 inner join Marca.Empresas e on a.IDEmpresa=e.IDEmpresa
	  where  A.IDEmpresa IN ('155687558220201','155687558220202','155687558220203','155687558220204','15559434322015' )
END 
go
CREATE or alter procedure Catalogos.SP_Clientes_Obtener AS
BEGIN 
	select top 100  
	   substring(IDCliente, 1,Charindex('-',IDCliente)-1) as empresa,
       c.identificacion as numero,
       'N-A' as bodega,
	   coalesce(c.direccion, 'N/D') as dir1,
	   c.nombrecompleto as nombre,
	   c.isoPais as pais,
       GETDATE() as fecha_transmision
	from Catalogos.Clientes c
	 where  substring(IDCliente, 1,Charindex('-',IDCliente)-1) IN ('155687558220201','155687558220202','155687558220203','155687558220204','15559434322015' )
END 

select * from  Catalogos.Clientes  where  IDEmpresa IN ('155687558220201','155687558220202','155687558220203','155687558220204','15559434322015' )
go
CREATE or alter procedure Catalogos.SP_Precios_Obtener AS
BEGIN 
	select top 100  
	   e.idempresa as empresa,
       p.IDArticulo  as producto,
	   p.PrecioNeto as precio,
	   'CRC' as moneda,
       'CR' as pais,
       GETDATE() as fecha_transmision
	from Catalogos.Precios p
		 inner join Marca.Empresas e on p.IDEmpresa=e.IDEmpresa collate DATABASE_DEFAULT
	  where  p.IDEmpresa IN ('155687558220201','155687558220202','155687558220203','155687558220204','15559434322015' )
END 
go
create or alter procedure Catalogos.SP_PedidoEncabezado_Obtener AS
BEGIN 


	SELECT 	top 1
		encabezado.IdEmpresa as Empresa ,
		encabezado.Documento as Documento ,
		encabezado.Tipo as Tipo ,
		encabezado.Fecha_Entrada as Fecha_Entrada,
		getdate() as Fecha_transmision
	FROM Catalogos.PedCompraCab encabezado
  FOR XML AUTO

END 

go
CREATE or alter procedure Catalogos.SP_PedidoDetalle_Obtener 
(
	@Pedido varchar(200)
)
AS
BEGIN 
	SELECT 	
	encabezado.IdEmpresa as Empresa,
	encabezado.Documento as Documento,
	encabezado.Tipo as Tipo ,
	encabezado.Fecha_Entrada as Fecha_Entrada,
	GETDATE() as Fecha_transmision,
	linea.Linea_Numero as Linea_Numero,
	linea.Producto as Producto,
	linea.Bodega as Bodega,
	linea.Cantidad as Cantidad
FROM Catalogos.PedCompraCab encabezado
LEFT JOIN Catalogos.PedCompraLin linea
	ON encabezado.Documento=linea.Documento and encabezado.IdEmpresa=linea.IdEmpresa
	where encabezado.Documento=@Pedido
	 FOR XML auto;
END 



