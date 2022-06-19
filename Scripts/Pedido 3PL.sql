drop table Catalogos.PedCompraLin
drop table Catalogos.PedCompraCab

Create table Catalogos.PedCompraCab(
IdEmpresa varchar(40), 
Documento varchar(15), 
Tipo varchar(15),
Fecha_Entrada varchar(15)
) 
go
Create table Catalogos.PedCompraLin(
IdEmpresa varchar(40), 
Documento varchar(15), 
Linea_Numero int , 
Producto varchar(200), 
Bodega varchar(3), 
Cantidad decimal(18,2), 
Costo_Unitario decimal(18,2) ) 

go
INSERT INTO Catalogos.PedCompraLin (IdEmpresa,Documento,Linea_Numero, Producto,Bodega, Cantidad, Costo_Unitario)
Select	(Select Top 1 FE_CedulaEmisor From GAP_2.DBO.SeriesCamposLibres Where Nullif(FE_CedulaEmisor, '') Is Not Null) as empresa,
		PC.SuPedido as documento, PL.NumLinea as linea_numero, Convert(varchar(50), Concat(PL.CodArticulo,  '-', UPPER(PL.Color), '-', UPPER(PL.Talla))) as producto, PL.CodAlmacen as Bodega, 
				PL.UnidadesTotal as cantidad, PL.Precio as costo_unitario
		From MULTIBRANDS_GAP.DBO.PedCompraLin as PL
		Join MULTIBRANDS_GAP.DBO.PedCompraCab as PC on PL.NumSerie = PC.NumSerie and PL.NumPedido = PC.NumPedido and PL.N = PC.N

go
INSERT INTO Catalogos.PedCompraCab (IdEmpresa,Documento,Tipo, Fecha_Entrada)
	Select (Select Top 1 FE_CedulaEmisor From GAP_2.DBO.SeriesCamposLibres Where Nullif(FE_CedulaEmisor, '') Is Not Null) as empresa,	
		SuPedido as documento, Coalesce(PCL.WMS_Tipo, 'Regular') as tipo, GetDate() as transmitido
		From	MULTIBRANDS_GAP.DBO.PedCompraCab as PC
		Join	MULTIBRANDS_GAP.DBO.PedCompraCamposLibres as PCL on PC.NumSerie = PCL.NumSerie and PC.NumPedido = PCL.NumPedido and PC.N = PCL.N

-- agregar  campo libre WMS_Tipo
Select * from Catalogos.PedCompraCab where Documento='21068173-EITU'
Select * from Catalogos.PedCompraLin where Documento='21068173-EITU'



SELECT 	
	encabezado.IdEmpresa ,
	encabezado.Documento,
	encabezado.Tipo ,
	encabezado.Fecha_Entrada ,
	GETDATE() fecha_transmision ,
	detalle.Linea_Numero ,
	detalle.Producto ,
	detalle.Bodega ,
	detalle.Cantidad
FROM Catalogos.PedCompraCab encabezado
LEFT JOIN Catalogos.PedCompraLin detalle
	ON encabezado.Documento=detalle.Documento and encabezado.IdEmpresa=detalle.IdEmpresa
	where encabezado.Documento='21014887-EITU'





