--USE MULTIBRANDS_ATL
--USE MULTIBRANDS_BR
--USE MULTIBRANDS_GAP
--USE MULTIBRANDS_ON
USE ALAMEDADC       
GO
If Not Exists (Select Name From sys.Schemas Where Name = 'BTOB')
Begin
	EXEC ('Create Schema BTOB')
End
GO

DROP Table If Exists BTOB.ColaVentas, BTOB.ColaArticulos, BTOB.ColaAlmacenes, BTOB.ColaStock, BTOB.Errores, BTOB.ColaTiendas, BTOB.ColaCajas, BTOB.ColaMonedas, BTOB.ColaFormasPago
GO


go
/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Tabla para almacenar los errores que ocurran en el proceso
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.Errores
(
	IDError		Int Identity(1, 1)	Not Null,
	Fecha		DateTime			Not Null Default (GETDATE()),
	Ubicacion	Varchar(200)		COLLATE Latin1_General_CS_AI Not Null,
	Informacion	Varchar(MAX)		COLLATE Latin1_General_CS_AI Not Null

	Constraint	PK_BTOB_Errores Primary Key Clustered (	IDError ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Cola de los almacenes pendientes de transmitir a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.ColaAlmacenes
(
	IDAlmacen		NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IdEmpresa		NVarchar(20)	COLLATE Latin1_General_CS_AI Not Null,
	CodAlmacen		NVarchar(4)		COLLATE Latin1_General_CS_AI Not Null,
	AlmacenDeFront	Bit				Not Null,
	Nombre			NVarchar(100)	COLLATE Latin1_General_CS_AI Not Null,
	Sincronizado	Bit				Not Null Default 0

	Constraint PK_Catalogos_Almacenes Primary Key Clustered ( IDAlmacen ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Cola de las ventas pendientes de transmitir a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.ColaVentas
(
	IDEncabezado	NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IdEmpresa		NVarchar(20)	COLLATE Latin1_General_CS_AI Not Null,
	NumSerie		NVarchar(4)		COLLATE Latin1_General_CS_AI Not Null,
	NumAlbaran		Int				Not Null,
	N				NChar(1)		COLLATE Latin1_General_CS_AI Not Null,
	NumSerieFac		NVarchar(4)		COLLATE Latin1_General_CS_AI Not Null,
	NumFac			Int				Not Null,
	NFac			NChar(1)		COLLATE Latin1_General_CS_AI Not Null,
	Fecha			DateTime		Not Null,
	TotalBruto		Decimal(18,3)	Not Null,
	TotalImpuesto	Decimal(18,3)	Not Null,
	DTOComercial	Decimal(18, 4)	Not Null,
	TotDTOComercial	Decimal(18, 4)	Not Null,
	TotalNeto		Decimal(18,3)	Not Null,
	CodCliente		Int				Not Null,
	CodVendedor		Int				Not Null,
	IDCaja			NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDMoneda		NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDVendedor		NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDCliente		NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDTarjeta		Int				Null,
	FactorMoneda	Float			Not Null,
	Sincronizado	Bit				Not Null Default 0

	Constraint PK_ColaVentas Primary Key Clustered ( IDEncabezado ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Cola de los articulos pendientes de transmitir a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.ColaArticulos
(
	IDArticulo		nVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDEmpresa		nVarchar(20)	COLLATE Latin1_General_CS_AI Not Null,
	CodArticulo		Int				Not Null,	
	Referencia		NVarchar(15)	COLLATE Latin1_General_CS_AI Not Null,
	Talla			NVarchar(10)	COLLATE Latin1_General_CS_AI Not Null,
	Color			NVarchar(10)	COLLATE Latin1_General_CS_AI Not Null,
	CodBarras		NVarchar(50)	COLLATE Latin1_General_CS_AI Not Null,	
	Descripcion		nVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	Departamento	nVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	Seccion			nVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	Familia			nVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	SubFamilia		nVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	CodBarras2		NVarchar(50)	COLLATE Latin1_General_CS_AI Null,
	CodBarras3		NVarchar(50)	COLLATE Latin1_General_CS_AI Null,
	CostoStock		Decimal(18,4)	Not Null,
	FechaModificado Datetime		Not Null,
	EsNuevo			Bit				Not Null Default 1,
	Sincronizado	Bit				Not Null Default 0

	Constraint PK_ColaArticulos Primary Key Clustered ( IDArticulo ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Cola del stock de los articulos pendientes de transmitir a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.ColaStock
(
	IDStock			Int Identity(1, 1)	Not Null,
	IDAlmacen		NVarchar(200)		COLLATE Latin1_General_CS_AI Not Null,
	IDArticulo		NVarchar(200)		COLLATE Latin1_General_CS_AI Not Null,
	Stock			Decimal(18,3)		Not Null,
	EnTransito		Decimal(18,3)		Not Null,
	FechaModificado	DateTime			Not Null,
	EsNuevo			Bit					Not Null Default 1,
	Sincronizado	Bit					Not Null Default 0

	Constraint PK_ColaStock Primary Key Clustered ( IDStock ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Cola de las Tiendas pendientes de transmitir a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.ColaTiendas
(
	IDTienda		NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDEmpresa		NVarchar(40)	COLLATE Latin1_General_CS_AI Not Null,
	Nombre			NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	CodigoICG		Int				Not Null,
	TiendaECOMM		Bit				Not Null,
	Sincronizado	Bit				Not Null Default 0

	Constraint PK_ColaTiendas Primary Key Clustered ( IDTienda ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Cola de las cajas pendientes de transmitir a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.ColaCajas
(
	IDCaja			NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDTienda		NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	Nombre			NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	CodigoICG		Int				Not Null,
	Serie			nVarchar(4)		COLLATE Latin1_General_CS_AI Not Null,
	Sincronizado	Bit				Not Null Default 0

	Constraint PK_ColaCajas Primary Key Clustered ( IDCaja ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Cola de las monedas pendientes de transmitir a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.ColaMonedas
(
	IDMoneda				NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDEmpresa				NVarchar(40)	COLLATE Latin1_General_CS_AI Not Null,
	CodigoICG				Int				Not Null,
	CodigoISO				NChar(3)		COLLATE Latin1_General_CS_AI Not Null,
	Descripcion				NVarchar(100)	COLLATE Latin1_General_CS_AI Not Null,
	EsMonedaPrincipal		Bit				Not Null,
	UsaTipoCambioDefecto	Bit				Not Null,
	TipoCambioDefecto		Decimal(18,4)	Not Null,
	Sincronizado			Bit				Not Null Default 0

	Constraint PK_ColaMonedas Primary Key Clustered ( IDMoneda ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Cola de las Formas de pago pendientes de transmitir a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Table	BTOB.ColaFormasPago
(
	IDFormaPago		NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	IDEmpresa		NVarchar(40)	COLLATE Latin1_General_CS_AI Not Null,
	Descripcion		NVarchar(100)	COLLATE Latin1_General_CS_AI Not Null,
	IDMoneda		NVarchar(200)	COLLATE Latin1_General_CS_AI Not Null,
	CodigoICG		NVarchar(6)		COLLATE Latin1_General_CS_AI Not Null,
	Sincronizado	Bit				Not Null Default 0

	Constraint PK_ColaFormasPago Primary Key Clustered ( IDFormaPago ASC ) ON [PRIMARY]
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Vista que consulta la informaci�n de la marca a Sincronizar con BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter View BTOB.VW_InfoEmpresa
As
(
	Select		TOP 1(Select Top 1 FE_CedulaEmisor From SeriesCamposLibres Where Nullif(FE_CedulaEmisor, '') Is Not Null) as IDEmpresa
				, E.Titulo as Nombre, Coalesce(E.NombreComercial, 'N-D') as NombreComercial, E.Pais As ISOPais
	From		General.dbo.Empresas	As	E
	Where		UPPER(PATHBD) like '%' + DB_NAME() + '%'
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Vista con la informaci�n del detalle de las facturas pendientes de transmitir a BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	2022-05-03
Responsable				:	Javier Castellon
Descripcion del ajuste	:	1. Cambio en los join para quitar el uso del Collation Database_Default  						
Confluence				:	https://ar-holdings.atlassian.net/l/c/sN7k7iBQ
================================================================================
*/

Create Or Alter View BTOB.VW_VentasDetalle
As	
(
	Select		
				CV.IDEncabezado, AVL.NUMLIN, CA.IDArticulo, CAL.IDAlmacen, AVL.UnidadesTotal as Unidades,  AVL.Precio, AVL.DTO as PorcDescuento, AVL.TOTAL , AVL.IVA as PorcentajeIVA, AVL.REQ As PorcentajeREQ
				, AVL.ABONODE_NUMSERIE as SerieAbono, AVL.ABONODE_NUMALBARAN as NumeroAbono, AVL.ABONODE_N as NAbono
	From		BTOB.ColaVentas		As	CV
	Inner Join	DBO.AlbVentaLin		As	AVL
		On		CV.NumSerie = AVL.NumSerie Collate Database_Default and CV.NumAlbaran = AVL.NumAlbaran and CV.N  Collate Database_Default = AVL.N
	Inner Join	BTOB.ColaArticulos	AS	CA
		On		AVL.CodArticulo = CA.CodArticulo and AVL.Talla  = CA.Talla and AVL.Color   = CA.Color
	Inner Join	BTOB.ColaAlmacenes	As	CAL
		On		AVL.CodAlmacen = CAL.CodAlmacen 
	Where		CV.Sincronizado = 0
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Vista con la informaci�n de los clientes en las facturas pendientes de transmitir a BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter View BTOB.VW_VentasClientes
As
(
	Select		Distinct
				CV.IDCliente, E.ISOPais, Coalesce(C.NIF20, 'N-D') As Identificacion, Coalesce(C.CIF, 'N-D') as IdentificacionSecundaria, C.NOMBRECLIENTE as NombreCompleto, C.DIRECCION1 as Direccion, C.Provincia, C.Poblacion As Canton, C.DIRECCION2 as Distrito, convert(datetime,coalesce( TRY_CONVERT(datetime, CL.LU_FechaNacimiento),getdate())) as FechaNacimiento, Coalesce(C.SEXO, '1') as Sexo
	From		BTOB.ColaVentas			As	CV
	Inner Join	Clientes				As	C
		On		CV.CodCliente = C.CodCliente
	Inner Join	ClientesCamposLibres	As	CL
		On		CL.CodCliente = CV.CodCliente
	Inner Join	BTOB.VW_InfoEmpresa		As	E
		On		CV.IdEmpresa = E.IDEmpresa Collate Database_Default
	Where		CV.Sincronizado = 0 
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Vista con la informaci�n de los vendedores en las facturas pendientes de transmitir a BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter View BTOB.VW_VentasVendedores
As
(
	Select		Distinct
				CV.IDVendedor, CV.IdEmpresa, Coalesce(V.NOMVENDEDOR, 'N-D') as NombreVendedor, V.NUMSSOCIAL as Identificacion
	From		BTOB.ColaVentas	As	CV
	Left Join	Vendedores		As	V
		On		CV.CodVendedor = V.CODVENDEDOR
	Where		CV.Sincronizado = 0
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Vista con la informaci�n de las formas de pago relacionadas a las facturas pendientes de transmitir a BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter View BTOB.VW_VentasFormasPago
As
(
	Select		Distinct
				CV.IDEncabezado, T.POSICION, T.IMPORTE, CF.IDFormaPago, T.FactorMoneda
	From		BTOB.ColaVentas	As	CV
	Inner Join	Tesoreria		As	T
		On		CV.NumSerieFac = T.SERIE Collate Database_Default and CV.NumFac = T.NUMERO
	Inner Join	BTOB.ColaFormasPago	As	CF
		On		T.CodFormaPago = CF.CodigoICG Collate Database_Default
	Where		CV.Sincronizado = 0
)
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que valida la configuraci�n de la Empresa que se va a sincronizar con la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Empresa_Sincronizar
(
	@EmpresaSincronizada BIT OUTPUT
)
As
Begin
	Begin Try
		Begin Transaction
			IF Exists (Select IDEmpresa From BTOB.Marca.Empresas Where  IDEmpresa = (Select Top 1 FE_CedulaEmisor From SeriesCamposLibres Where Nullif(FE_CedulaEmisor, '') Is Not Null)Collate Database_Default)
			Begin
				Set @EmpresaSincronizada = 1
			End
			Else
			Begin
				Insert Into BTOB.Marca.Empresas (IdEmpresa, Nombre, NombreComercial, ISOPais)
				Select IdEmpresa, Nombre, NombreComercial, ISOPais From BTOB.VW_InfoEmpresa
				
				IF @@ROWCOUNT > 0
				Begin
					Set @EmpresaSincronizada = 1
				End
				Else
				Begin
					; THROW 50010, 'No fue posible insertar la informaci�n de la empresa en la BASE BTOB', 16;
				End
			End
		Commit Transaction
	End Try
	Begin Catch
		If @@TRANCOUNT > 0
			Rollback Transaction;
		
		Insert Into BTOB.Errores (Ubicacion, Informacion) Values ('BTOB.SP_Empresa_Sincronizar', ERROR_MESSAGE())
		Set @EmpresaSincronizada = 0
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Carga la informaci�n de las tiendas en la Tabla BTOB.ColaTiendas para luego ser transmitida a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Tiendas_CargaCola
As
Begin
	Begin Try
		Begin Transaction
		Declare @IDEmpresa nVarchar(40) = (Select top 1 FE_CedulaEmisor From SeriesCamposLibres Where Nullif(FE_CEDULAEMISOR, '') Is Not Null) 

		Insert Into		BTOB.ColaTiendas
						(IdTienda, IDEmpresa, Nombre, CodigoICG, Sincronizado, TiendaECOMM)
		Select			Concat(@IDEmpresa, '-', IDFRONT) as IDTienda, @IDEmpresa as IDEmpresa, Titulo as Nombre, IDFRONT as CodigoICG, 0 as Sincronizado, Coalesce(AE.EsEcom, 0) as TiendaECOMM
		From			REM_Fronts			As	RF
		Left Join		BTOB.ColaTiendas	As	CT
			On			RF.IDFRONT = CT.CodigoICG
		Left Join		(
							Select		Distinct
										RCF.IDFRONT as IDFrontECOM, 1 As EsECOM
							From		REM_CAJASFRONTSERIES	As RCF
							Inner Join	SERIESCAMPOSLIBRES		As SCL
								On		RCF.SERIE = SCL.SERIE Collate Database_Default--and SCL.USARENSHOPIFY = 'T'
						) As AE
			On			RF.IDFRONT = AE.IDFrontECOM
		Where			CT.CodigoICG is null

		Commit Transaction
	End Try
	Begin Catch
		IF	@@TRANCOUNT > 0
			Rollback Transaction;

		THROW;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Carga la informaci�n de las Cajas por Tienda en la Tabla BTOB.ColaCajas para luego ser transmitida a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Cajas_CargaCola
As
Begin
	Begin Try
		Begin Transaction
			Declare @IDEmpresa nVarchar(40) = (Select top 1 FE_CedulaEmisor From SeriesCamposLibres Where Nullif(FE_CEDULAEMISOR, '') Is Not Null) 

			Insert Into		BTOB.ColaCajas
							(IdTienda, IDCaja, Nombre, CodigoICG, Serie, Sincronizado)
			Select			Concat('@IDEmpresa', '-', IDFRONT) as IDTienda, Concat('@IDEmpresa', '-', IDFRONT, '-', CAJAFRONT) as IDCaja, Coalesce(Descripcion, 'N-A') as Nombre, CajaFront As CodigoICG, RF.CAJAMANAGER as Serie, 0 As Sincronizado
			From			Rem_CajasFront		As	RF
			Left Join		BTOB.ColaCajas	As	CT
				On			RF.CAJAFRONT = CT.CodigoICG
			Where			CT.CodigoICG is null
		Commit Transaction
	End Try
	Begin Catch
		IF	@@TRANCOUNT > 0
			Rollback Transaction;

		THROW;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Carga la informaci�n de las Monedas configuradas en ICG en la Tabla BTOB.ColaMonedas para luego ser transmitidas a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Monedas_CargaCola
As
Begin
	Begin Try
		Begin Transaction
			Declare @IDEmpresa nVarchar(40) = (Select top 1 FE_CedulaEmisor From SeriesCamposLibres Where Nullif(FE_CEDULAEMISOR, '') Is Not Null) 

			Insert Into		BTOB.ColaMonedas
							(IDMoneda, IDEmpresa, CodigoICG, CodigoISO, Descripcion, EsMonedaPrincipal, UsaTipoCambioDefecto, TipoCambioDefecto, Sincronizado)
			Select			Concat(@IDEmpresa, '-', CODMONEDA) as IDMoneda, @IDEmpresa as IDEmpresa, CODMONEDA, Coalesce(M.CODIGOISO, 'N-D') as CODIGOISO, Coalesce(M.Descripcion, 'N-D')As Descripcion, IIF(PRINCIPAL = 'F', 0, 1) As EsMonedaPrincipal, IIF(AplicarCOTDEF = 'F', 0, 1) as UsaTipoCambioDefecto, Coalesce(COTDEF, 0) as TipoDeCambioDefecto, 0 as Sincronizado
			From			Monedas				As	M
			Left Join		BTOB.ColaMonedas	As	CM
				On			M.CODMONEDA = CM.CodigoICG
			Where			CM.CodigoICG is null
		Commit Transaction
	End Try
	Begin Catch
		IF	@@TRANCOUNT > 0
			Rollback Transaction;

		THROW;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Carga la informacion de las Cajas por Tienda en la Tabla BTOB.ColaCajas para luego ser transmitida a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	2022-05-03
Responsable				:	Javier Castellon
Descripcion del ajuste	:	1. Cambio en los join para quitar el uso del Collation Database_Default  						
Confluence				:	https://ar-holdings.atlassian.net/l/c/sN7k7iBQ
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Ventas_CargaCola
AS
Begin
	Begin Try
		Begin Transaction
			Insert Into BTOB.ColaVentas
						(IDEncabezado, IdEmpresa, NumSerie, NumAlbaran, N, NumSerieFac, NumFac, NFaC, Fecha, TotalBruto, TotalImpuesto, DTOComercial, TotDTOComercial, TotalNeto, CodCliente, CodVendedor, IDCaja, IDMoneda, IDVendedor, IDCliente, FactorMoneda, IDTarjeta, Sincronizado )

			Select		Concat(SC.FE_CEDULAEMISOR, '-', AC.NumSerie, '-', AC.NumAlbaran, '-', AC.N) Collate Database_Default as IDEncabezado, SC.FE_CEDULAEMISOR  as IDEmpresa , AC.NumSerie Collate Database_Default, AC.NumAlbaran, AC.N Collate Database_Default
						, AC.NumSerieFac Collate Database_Default, AC.NumFac, AC.NFac Collate Database_Default, Concat(CONVERT(CHAR(10), AC.Fecha, 120),'T', CONVERT(CHAR(8), Ac.HORA, 108)) as Fecha, AC.TotalBruto, AC.TotalImpuestos, AC.DTOCOMERCIAL, AC.TOTDTOCOMERCIAL, AC.TotalNeto
						, AC.CODCLIENTE,coalesce(AC.CODVENDEDOR,1) as CODVENDEDOR , coalesce(nullif(CC.IDCaja,''),1) IDCaja, CM.IDMoneda, Concat(SC.FE_CEDULAEMISOR, '-', coalesce(AC.CodVendedor,1)) as IDVendedor, Concat(SC.FE_CEDULAEMISOR, '-', AC.CODCLIENTE) as IDCliente, AC.FACTORMONEDA, AC.IdTarjeta, 0 as Sincronizado
			From		AlbVentaCab			As	AC
			Inner Join	Monedas				As	M
				On		M.CodMoneda = AC.CodMoneda
			Inner Join	BTOB.ColaMonedas	As	CM
				On		M.CODMONEDA = CM.CodigoICG
			left Join	BTOB.ColaCajas		As	CC -- porque no tiene remcajasfront
				On		CC.Serie = AC.CAJA Collate Database_Default 
			Inner Join	SeriesCamposLibres	As	SC 
				On		AC.NumSerie = SC.Serie  and Nullif(SC.FE_CEDULAEMISOR, '') is not null
			Left Join	BTOB.ColaVentas as CV 
				On		AC.NumSerie = CV.NumSerie  Collate Database_Default and AC.NumAlbaran = CV.NumAlbaran and AC.N = CV.N  Collate Database_Default
			Where		CV.NumSerie Is Null
		Commit Transaction
	End Try
	Begin Catch
		IF	@@TRANCOUNT > 0
			Rollback Transaction;

		THROW;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Carga la informacion de los articulos en la Tabla BTOB.ColaArticulos para luego ser transmitida a la base de datos BTOB
							Este metodo valida los articulos nuevo y los que se han modificado, en ambos casos se establecen con el Estado de Sincronizado en False
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	2022-05-03
Responsable				:	Javier Castellon
Descripcion del ajuste	:	1. Cambio en los join para quitar el uso del Collation Database_Default  						
Confluence				:	https://ar-holdings.atlassian.net/l/c/sN7k7iBQ
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Articulos_CargaCola
AS
Begin
	Declare @IDEmpresa nVarchar(20) = (Select Top 1 FE_CedulaEmisor From SeriesCamposLibres Where Nullif(FE_CedulaEmisor, '') is not null)

	-- Inserta Articulos Nuevos en la Tabla Intermedia	
	Insert Into BTOB.ColaArticulos
				(IDArticulo, IDEmpresa, CodArticulo, Referencia, Talla, Color, CodBarras, CodBarras2, CodBarras3, FechaModificado, Descripcion, Departamento, Seccion, Familia, SubFamilia, CostoStock )

	Select		Concat(@IDEmpresa, '-', A.CodArticulo, '-', AL.Talla, '-', AL.Color) as IDArticulo, @IDEmpresa as IDEmpresa,
	A.CodArticulo, A.RefProveedor as Referenca, AL.Talla, AL.Color
				,	AL.CodBarras, AL.CodBarras2, AL.CodBarras3, A.FechaModificado,  Coalesce(A.Descripcion, 'N-A') as Descripcion, Coalesce(D.Descripcion, 'N-A') as Departamento, Coalesce(S.Descripcion, 'N-A') as Seccion
				,	Coalesce(F.Descripcion, 'N-A') as Familia,  Coalesce(SF.Descripcion, 'N-A') as SubFamilia, Coalesce(AL.COSTESTOCK, 0) as CosteStock--, A.TEMPORADA as temporada
	From		Articulos		As	A
	Inner Join	ARTICULOSLIN	As	AL 
		On		A.CodArticulo = AL.CodArticulo
	Left Join	Departamento As D
		On		D.NumDPTO	= A.Dpto
	Left Join	Secciones	As	S
		On		S.NumDPTO = A.DPTO  and S.NumSeccion = A.Seccion
	Left Join	Familias	As	F
		On		F.NumDPTO = A.DPTO and F.NumSeccion = A.Seccion and F.NumFamilia = A.Familia
	Left Join	SubFamilias as SF
		On		SF.NumDPTO = A.DPTO and SF.NumSeccion = A.Seccion and SF.NumFamilia = A.Familia and SF.NumSubFamilia = A.SubFamilia
	Left Join	BTOB.ColaArticulos	As CA 
		On		CA.CodArticulo = AL.CodArticulo and CA.Talla = AL.Talla  and CA.Color = AL.Color  
	Where		CA.IDArticulo is null and A.TIPOARTICULO = 'A' and al.CODBARRAS is not null and a.REFPROVEEDOR is not null AND a.DESCATALOGADO='F'

	-- Actualiza articulos Existentes en la Tabla Intermedia
	Update		CA
	Set			CA.FechaModificado = A.FechaModificado, CA.Sincronizado = 0, EsNuevo = 0, CostoStock = AL.COSTESTOCK, Descripcion = Coalesce(A.Descripcion, 'N-A')
	From		Articulos			As A
	Inner Join	ArticulosLin		As AL 
		On		A.CodArticulo = AL.CodArticulo
	Inner Join	BTOB.ColaArticulos	As CA 
		On		CA.CodArticulo = AL.CodArticulo and CA.Talla = AL.Talla  and CA.Color = AL.Color   
	Where		A.TIPOARTICULO = 'A' and A.FechaModificado <> CA.FechaModificado
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Carga la informacion de los almacenes en la Tabla BTOB.ColaAlmacenes para luego ser transmitida a la base de datos BTOB
							Este metodo valida los almacenes para determinar si es un Almacen de Front o no, establaciendo el valor true o false para la columna AlmacenDeFront
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Almacenes_CargaCola
AS
Begin
	Begin Try
		Begin Transaction
			Declare @IDEmpresa nVarchar(20) = (Select Top 1 FE_CedulaEmisor From SeriesCamposLibres Where Nullif(FE_CedulaEmisor, '') is not null)

			Insert Into BTOB.ColaAlmacenes
						(IDAlmacen, IDEmpresa, CodAlmacen, Nombre, AlmacenDeFront )

			Select		Concat(@IDEmpresa, '-', A.CodAlmacen) as IDAlmacen, @IDEmpresa as IdEmpresa, A.CodAlmacen, A.NombreAlmacen
						, IIF( (Select Count(Coalesce(CodAlmVentas, '')) From Rem_CajasFront Where CodAlmVentas Collate Database_Default = A.CodAlmacen ) > 0, 1 , 0 ) as AlmacenDeFront
			From		Almacen				As A
			Left Join	BTOB.ColaAlmacenes	As C
				On		C.CodAlmacen = A.CodAlmacen Collate Database_Default
			Where		C.CodAlmacen is null
		Commit Transaction
	End Try
	Begin Catch
		IF	@@TRANCOUNT > 0
			Rollback Transaction;

		THROW;
	End Catch
End
GO


/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Carga la informacion del STOCK de los articulos por almacen en la Tabla BTOB.ColaStock para luego ser transmitida a la base de datos BTOB
							Este metodo valida  el stock nuevo y el que se ha modificado, en ambos casos se establecen con el Estado de Sincronizado en False
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	2022-05-03
Responsable				:	Javier Castellon
Descripcion del ajuste	:	1. Cambio en los join para quitar el uso del Collation Database_Default 						
Confluence				:	https://ar-holdings.atlassian.net/l/c/sN7k7iBQ
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Stock_CargaCola
AS
Begin
	Begin Try
		Begin Transaction
			Insert Into BTOB.ColaStock
						(IDArticulo, IDAlmacen, Stock, EnTransito, FechaModificado )

			Select		CA.IDArticulo, A.IDAlmacen, S.Stock, S.EnTransito, S.FechaModificado
			From		Stocks	As	S
			Inner Join	BTOB.ColaArticulos	As	CA
				On		CA.CodArticulo = S.CodArticulo and CA.Talla = S.Talla and CA.Color = S.Color  
			Inner Join	BTOB.ColaAlmacenes	As	A
				On		A.CodAlmacen = S.CodAlmacen 
			Left Join	BTOB.ColaStock	As	CS
				On		CS.IDArticulo = CA.IDArticulo and CS.IDAlmacen = A.IDAlmacen
			Where		CS.IDAlmacen is null

			Update		CS
			Set			CS.FechaModificado = S.FechaModificado, CS.Sincronizado = 0, CS.EsNuevo = 0
			From		Stocks	As	S
			Inner Join	BTOB.ColaArticulos	As	CA
				On		CA.CodArticulo = S.CodArticulo and CA.Talla = S.Talla and CA.Color = S.Color 
			Inner Join	BTOB.ColaAlmacenes	As	A
				On		A.CodAlmacen = S.CodAlmacen  
			Inner Join	BTOB.ColaStock	As	CS
				On		CS.IDArticulo = CA.IDArticulo and CS.IDAlmacen = A.IDAlmacen
			Where		CS.FechaModificado <> S.FechaModificado 
		Commit Transaction
	End Try
	Begin Catch
		IF	@@TRANCOUNT > 0
			Rollback Transaction;

		THROW;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Carga la informaci�n de las formas de pago en la Tabla BTOB.ColaFormasPago para luego ser transmitida a la base de datos BTOB
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_FormasPago_CargaCola
As
Begin
	Begin Try
		Begin Transaction
			Declare @IDEmpresa nVarchar(40) = (Select top 1 FE_CedulaEmisor From SeriesCamposLibres Where Nullif(FE_CEDULAEMISOR, '') Is Not Null) 

			Insert Into		BTOB.ColaFormasPago
							(IDFormaPago, IDEmpresa, Descripcion, IDMoneda, CodigoICG, Sincronizado)
			Select			Concat(@IDEmpresa, '-', FP.CodFormaPago) as IDFormaPago, @IDEmpresa as IDEmpresa, FP.DESCRIPCION, Concat(@IDEmpresa, '-', FP.CodMoneda) as IDMoneda, FP.CODFORMAPAGO, 0 as Sincronizado
			From			FormasPago			As	FP
			Left Join		BTOB.ColaFormasPago	As	CF
				On			CF.CodigoICG = FP.CodFormaPago Collate Database_Default
			Where			CF.CodigoICG is null
		Commit Transaction
	End Try
	Begin Catch
		IF	@@TRANCOUNT > 0
			Rollback Transaction;

		THROW;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que transmite la informacion de los articulos a la Base BTOB en la tabla BTOB.Catalogos.Articulos
							, posterior al traspaso, actualiza el estado de los articulos en la Tabla BTOB.ColaArticulos
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	2022-05-03
Responsable				:	Javier Castellon
Descripcion del ajuste	:	1. Cambio en los join para quitar el uso del Collation Database_Default  						
Confluence				:	https://ar-holdings.atlassian.net/l/c/sN7k7iBQ
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Articulos_CargaBDCentral
As
Begin
	Begin Try
		Begin Transaction
			Drop Table IF Exists #InfoArticulosNuevos, #InfoArticulosActualizar

			Select	IDArticulo, IDEmpresa, CodArticulo, Referencia, Talla, Color, CodBarras, CodBarras2, CodBarras3, Descripcion, Departamento, Seccion, Familia, SubFamilia, CostoStock
			Into	#InfoArticulosNuevos
			From	BTOB.ColaArticulos
			Where	Sincronizado = 0 and EsNuevo = 1

			Select	IDArticulo, IDEmpresa, CodArticulo, Referencia, Talla, Color, CodBarras, CodBarras2, CodBarras3, Descripcion, Departamento, Seccion, Familia, SubFamilia, CostoStock
			Into	#InfoArticulosActualizar
			From	BTOB.ColaArticulos
			Where	Sincronizado = 0 and EsNuevo = 0

			Insert Into BTOB.Catalogos.Articulos
					(IDArticulo, IDEmpresa, CodArticulo, Referencia, Talla, Color, CodBarras, CodBarras2, CodBarras3, Descripcion, Departamento, Seccion, Familia, SubFamilia, CostoStock)
			Select	IDArticulo, IDEmpresa, CodArticulo, Referencia, Talla, Color, CodBarras, CodBarras2, CodBarras3, Descripcion, Departamento, Seccion, Familia, SubFamilia, CostoStock
			From	#InfoArticulosNuevos

			-- Modificar para actualizar seg�n sea requerido
			Update		A
			Set			A.CodBarras2 = IA.CodBarras2, A.CodBarras3 = IA.CodBarras3
			From		BTOB.Catalogos.Articulos	As	A
			Inner Join	#InfoArticulosActualizar	As	IA
				On		A.IDArticulo = IA.IDArticulo 

			Update		CA
			Set			CA.Sincronizado = 1
			From		BTOB.ColaArticulos		AS CA
			Inner Join	#InfoArticulosNuevos	As IA 
				On		CA.IDArticulo = IA.IDArticulo  COllate Database_Default

			Update		CA
			Set			CA.Sincronizado = 1
			From		BTOB.ColaArticulos		AS CA
			Inner Join	#InfoArticulosActualizar	AS IAA
				On		IAA.IDArticulo = CA.IDArticulo  COllate Database_Default

		Commit  Transaction
	End Try
	Begin Catch
		DECLARE @ErrorMessage NVARCHAR(4000) = Concat (ERROR_NUMBER (), ' | ', ERROR_MESSAGE() )

		IF @@TRANCOUNT > 0
			Rollback Transaction;

		THROW 50000, @ErrorMessage, 1;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que transmite la informaci�n de los articulos a la Base BTOB en la tabla BTOB.Catalogos.Articulos
							, posterior al traspaso, actualiza el estado de los articulos en la Tabla BTOB.ColaArticulos
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	2022-05-03
Responsable				:	Javier Castellon
Descripcion del ajuste	:	1. Cambio en los join para quitar el uso del Collation Database_Default 
												
Confluence				:	https://ar-holdings.atlassian.net/l/c/sN7k7iBQ
================================================================================
*/


Create Or Alter Procedure BTOB.SP_Ventas_CargaBDCentral
As
Begin
	-- En caso de Error se hace Rollback inmediatamente.
	SET XACT_ABORT ON

	Begin Try
		Begin Transaction
			
			
			Drop table If Exists #InfoVentas

			Select	IDEncabezado, IdEmpresa, NumSerie, NumAlbaran, N, NumSerieFac, NumFac, NFac, Fecha, TotalBruto, TotalImpuesto, DTOComercial, TotDTOComercial, TotalNeto, CodCliente, CodVendedor, IDVendedor, IDCaja, IDMoneda, IDCliente, IDTarjeta, FactorMoneda
			Into	#InfoVentas
			From	BTOB.ColaVentas
			Where	Sincronizado = 0

		
			Insert Into BTOB.Catalogos.Clientes (IDCliente,ISOPais,Identificacion,IdentificacionSecundaria,NombreCompleto,Direccion,Provincia,Canton,Distrito,FechaDeNacimiento,IdSexo)
			Select	C.IDCliente, C.ISOPais, C.Identificacion, C.IdentificacionSecundaria, C.NombreCompleto, C.Direccion, C.Provincia, C.Canton, C.Distrito,C.FechaNacimiento, C.Sexo
			From		BTOB.VW_VentasClientes	As	C
			Left Join	BTOB.Catalogos.Clientes	AS	BC
				On		C.IDCliente = BC.IDCliente Collate Database_Default
			Where		BC.IDCliente is null 

			Insert Into BTOB.Catalogos.Vendedores
			Select		V.IDVendedor, V.IdEmpresa, V.NombreVendedor, V.Identificacion
			From		BTOB.VW_VentasVendedores	As	V
			Left Join	BTOB.Catalogos.Vendedores	As	BV
				On		V.IDVendedor = BV.IDVendedor Collate Database_Default and V.IdEmpresa = BV.IDEmpresa Collate Database_Default
			Where		BV.IDVendedor is null

		
			Insert Into BTOB.Ventas.Encabezados (IDEncabezado, IdEmpresa, NumSerie, NumAlbaran, N, NumSerieFac, NumFac, NFac, Fecha, TotalBruto, TotalImpuesto, TotalNeto,  DTOComercial, TotDTOComercial, IDVendedor, IDCaja, IDMoneda, IDCliente, IDTarjeta, FactorMoneda)
			Select	IDEncabezado, IdEmpresa, NumSerie, NumAlbaran, N, NumSerieFac, NumFac, NFac,  Fecha, TotalBruto, TotalImpuesto, TotalNeto,  DTOComercial, TotDTOComercial, IDVendedor, IDCaja, IDMoneda, IDCliente, IDTarjeta, FactorMoneda
			From	#InfoVentas

	
			Insert Into BTOB.Ventas.Detalles
			Select	IDEncabezado, NumLin, IdArticulo, IDAlmacen, Unidades, Precio, PorcDescuento, Total, PorcentajeIVA, PorcentajeREQ, SerieAbono, NumeroAbono, NAbono
			From	BTOB.VW_VentasDetalle

			Insert Into	BTOB.Ventas.FormasPago
			Select		IDEncabezado, Posicion, Importe, IDFormaPago, FactorMoneda
			From		BTOB.VW_VentasFormasPago	As	VF

			Update		V
			Set			V.Sincronizado = 1
			From		BTOB.ColaVentas As	V
			Inner Join	#InfoVentas		As	IV
				On		V.IDEncabezado = IV.IDEncabezado

		Commit Transaction
	End Try
	Begin Catch
		DECLARE @ErrorMessage NVARCHAR(4000) = Concat (ERROR_NUMBER (), ' | ', ERROR_MESSAGE() )

		IF @@TRANCOUNT > 0
			Rollback Transaction;

		THROW 50000, @ErrorMessage, 1;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que transmite la informaci�n de los almacenes a la Base BTOB en la tabla BTOB.Catalogos.Almacenes
							, posterior al traspaso, actualiza el estado de los articulos en la Tabla BTOB.ColaAlmacenes
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Almacenes_CargaBDCentral
As
Begin
	Begin Try
		Begin Transaction
			Drop Table IF Exists #InfoAlmacenes

			Select	IDAlmacen, IDEmpresa, CodAlmacen, Nombre, AlmacenDeFront
			Into	#InfoAlmacenes
			From	BTOB.ColaAlmacenes
			Where	Sincronizado = 0 

			Insert Into BTOB.Catalogos.Almacenes
					(IDAlmacen, IDEmpresa, CodAlmacen, Nombre, AlmacenDeFront)
			Select	IDAlmacen, IDEmpresa, CodAlmacen, Nombre, AlmacenDeFront
			From	#InfoAlmacenes

			Update		CA
			Set			CA.Sincronizado = 1
			From		BTOB.ColaAlmacenes	AS CA
			Inner Join	#InfoAlmacenes		As IA 
				On		CA.IdAlmacen = IA.IdAlmacen

		Commit Transaction
	End Try
	Begin Catch
		DECLARE @ErrorMessage NVARCHAR(4000) = Concat (ERROR_NUMBER (), ' | ', ERROR_MESSAGE() )

		IF @@TRANCOUNT > 0
			Rollback Transaction;

		THROW 50000, @ErrorMessage, 1;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que transmite la informaci�n del Stock a la Base BTOB en la tabla BTOB.Catalogos.Stock
							, posterior al traspaso, actualiza el estado del Stock en la Tabla BTOB.ColaAlmacenes
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	2022-05-03
Responsable				:	Javier Castellon
Descripcion del ajuste	:	1. Cambio en los join para quitar el uso del Collation Database_Default  						
Confluence				:	https://ar-holdings.atlassian.net/l/c/sN7k7iBQ
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Stock_CargaBDCentral
As
Begin
	Begin Try
		Begin Transaction
			Drop Table IF Exists #InfoStokNuevo, #InfoStockActualizar

			Select	IDArticulo, IDAlmacen, Stock, EnTransito, FechaModificado
			Into	#InfoStokNuevo
			From	BTOB.ColaStock
			Where	Sincronizado = 0 and EsNuevo = 1

			Select	IDArticulo, IDAlmacen, Stock, EnTransito, FechaModificado
			Into	#InfoStockActualizar
			From	BTOB.ColaStock
			Where	Sincronizado = 0 and EsNuevo = 0

			Insert Into BTOB.Catalogos.Stock
					(IDArticulo, IDAlmacen, Stock, EnTransito, FechaModificado)
			Select	IDArticulo, IDAlmacen, Stock, EnTransito, FechaModificado
			From	#InfoStokNuevo
			
			Update		A
			Set			A.Stock = IA.Stock, A.EnTransito = IA.EnTransito, A.FechaModificado = IA.FechaModificado
			From		BTOB.Catalogos.Stock	As	A
			Inner Join	#InfoStockActualizar	As	IA
				On		A.IDArticulo = IA.IDArticulo COllate Database_Default and A.IDAlmacen = IA.IDAlmacen COllate Database_Default

			Update		CA
			Set			CA.Sincronizado = 1
			From		BTOB.ColaStock	AS CA
			Inner Join	#InfoStokNuevo	As IA 
				On		CA.IDArticulo = IA.IDArticulo COllate Database_Default and CA.IDAlmacen = IA.IDAlmacen COllate Database_Default

			Update		CA
			Set			CA.Sincronizado = 1
			From		BTOB.ColaStock			AS CA
			Inner Join	#InfoStockActualizar	AS ISA
				On		ISA.IDArticulo = CA.IDArticulo COllate Database_Default and CA.IDAlmacen = ISA.IDAlmacen COllate Database_Default

		Commit Transaction
	End Try
	Begin Catch
		DECLARE @ErrorMessage NVARCHAR(4000) = Concat (ERROR_NUMBER (), ' | ', ERROR_MESSAGE() )

		IF @@TRANCOUNT > 0
			Rollback Transaction;

		THROW 50000, @ErrorMessage, 1;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que transmite la informaci�n de los almacenes a la Base BTOB en la tabla BTOB.Marca.Tiendas
							, posterior al traspaso, actualiza el estado de las tiendas en la Tabla BTOB.ColaTiendas
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Tiendas_CargaBDCentral
As
Begin
	Begin Try
		Begin Transaction
			Drop Table IF Exists #InfoTiendas

			Select	IDTienda, IDEmpresa, Nombre, CodigoICG,TiendaECOMM
			Into	#InfoTiendas
			From	BTOB.ColaTiendas
			Where	Sincronizado = 0 

			Insert Into BTOB.Marca.Tiendas
					(IDTienda, IDEmpresa, Nombre, CodigoICG, TiendaECOMM)
			Select	IDTienda, IDEmpresa, Nombre, CodigoICG,TiendaECOMM
			From	#InfoTiendas

			Update		CT
			Set			CT.Sincronizado = 1
			From		BTOB.ColaTiendas	AS CT
			Inner Join	#InfoTiendas		As IA 
				On		CT.IDTienda = IA.IDTienda

		Commit Transaction
	End Try
	Begin Catch
		DECLARE @ErrorMessage NVARCHAR(4000) = Concat (ERROR_NUMBER (), ' | ', ERROR_MESSAGE() )

		IF @@TRANCOUNT > 0
			Rollback Transaction;

		THROW 50000, @ErrorMessage, 1;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que transmite la informaci�n de los almacenes a la Base BTOB en la tabla BTOB.Marca.Tiendas
							, posterior al traspaso, actualiza el estado de las Cajas en la Tabla BTOB.ColaCajas
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Cajas_CargaBDCentral
As
Begin
	Begin Try
		Begin Transaction
			Drop Table IF Exists #InfoCajas

			Select	IDCaja, IDTienda, Nombre, CodigoICG, Serie
			Into	#InfoCajas
			From	BTOB.ColaCajas
			Where	Sincronizado = 0 

			Insert Into BTOB.Marca.Cajas
					(IDCaja, IDTienda, Nombre, CodigoICG, Serie)
			Select	IDCaja, IDTienda, Nombre, CodigoICG, Serie
			From	#InfoCajas

			Update		CA
			Set			CA.Sincronizado = 1
			From		BTOB.ColaCajas	AS CA
			Inner Join	#InfoCajas		As IA 
				On		CA.IDCaja = IA.IDCaja

		Commit Transaction
	End Try
	Begin Catch
		DECLARE @ErrorMessage NVARCHAR(4000) = Concat (ERROR_NUMBER (), ' | ', ERROR_MESSAGE() )

		IF @@TRANCOUNT > 0
			Rollback Transaction;

		THROW 50000, @ErrorMessage, 1;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que transmite la informaci�n de las monedas a la Base BTOB en la tabla BTOB.Catalogos.Monedas
							, posterior al traspaso, actualiza el estado de las tiendas en la Tabla BTOB.ColaMonedas
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Monedas_CargaBDCentral
As
Begin
	Begin Try
		Begin Transaction
			Drop Table IF Exists #InfoMonedas

			Select	IDMoneda, IDEmpresa, CodigoICG, CodigoISO, Descripcion, EsMonedaPrincipal, UsaTipoCambioDefecto, TipoCambioDefecto
			Into	#InfoMonedas
			From	BTOB.ColaMonedas
			Where	Sincronizado = 0 

			Insert Into BTOB.Catalogos.Monedas
					(IDMoneda, IDEmpresa, CodigoICG, CodigoISO, Descripcion, EsMonedaPrincipal, UsaTipoCambioDefecto, TipoCambioDefecto)
			Select	IDMoneda, IDEmpresa, CodigoICG, CodigoISO, Descripcion, EsMonedaPrincipal, UsaTipoCambioDefecto, TipoCambioDefecto
			From	#InfoMonedas

			Update		CA
			Set			CA.Sincronizado = 1
			From		BTOB.ColaMonedas	AS CA
			Inner Join	#InfoMonedas		As IA 
				On		CA.IDMoneda = IA.IDMoneda

		Commit Transaction
	End Try
	Begin Catch
		DECLARE @ErrorMessage NVARCHAR(4000) = Concat (ERROR_NUMBER (), ' | ', ERROR_MESSAGE() )

		IF @@TRANCOUNT > 0
			Rollback Transaction;

		THROW 50000, @ErrorMessage, 1;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que transmite la informaci�n de las Formas de Pago a la Base BTOB en la tabla BTOB.Catalogos.FormasPago
							, posterior al traspaso, actualiza el estado de las Formas de pago en la Tabla BTOB.ColaFormasPago
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_FormasPago_CargaBDCentral
As
Begin
	Begin Try
		Begin Transaction
			Drop Table IF Exists #InfoFormasPago

			Select	IDFormaPago, IDEmpresa, Descripcion, IDMoneda, CodigoICG
			Into	#InfoFormasPago
			From	BTOB.ColaFormasPago
			Where	Sincronizado = 0 

			Insert Into BTOB.Catalogos.FormasPago
					(IDFormaPago, IDEmpresa, Descripcion, IDMoneda, CodigoICG)
			Select	IDFormaPago, IDEmpresa, Descripcion, IDMoneda, CodigoICG
			From	#InfoFormasPago

			Update		CA
			Set			CA.Sincronizado = 1
			From		BTOB.ColaFormasPago	AS CA
			Inner Join	#InfoFormasPago		As IA 
				On		CA.IDFormaPago = IA.IDFormaPago

		Commit Transaction
	End Try
	Begin Catch
		DECLARE @ErrorMessage NVARCHAR(4000) = Concat (ERROR_NUMBER (), ' | ', ERROR_MESSAGE() )

		IF @@TRANCOUNT > 0
			Rollback Transaction;

		THROW 50000, @ErrorMessage, 1;
	End Catch
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Ejecuta todos los Procedimientos de Carga de Colas en las bases de Gesti�n ICG Manager, Se crea para facilitar la configuraci�n del Job
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create OR Alter Procedure BTOB.SP_Carga_Colas
As
Begin
	Set NOCOUNT On;
	EXEC BTOB.SP_Tiendas_CargaCola
	EXEC BTOB.SP_Cajas_CargaCola
	EXEC BTOB.SP_Monedas_CargaCola
	EXEC BTOB.SP_Articulos_CargaCola
	EXEC BTOB.SP_Almacenes_CargaCola
	EXEC BTOB.SP_Stock_CargaCola
	EXEC BTOB.SP_FormasPago_CargaCola
	EXEC BTOB.SP_Ventas_CargaCola
End
GO

/*
================================================================================
Autor					:	Javier Castellon
Fecha de Creación		:	2022-02-24
Descripción				:	Procedimiento almacenado que Ejecuta todos los Procedimientos de traslado de Informaci�n a la base de datos BTOB, Se crea para facilitar la configuraci�n del Job
Confluence				:	https://ar-holdings.atlassian.net/l/c/0vLdPjDV
================================================================================
Fecha de modificación	:	[FECHA]
Responsable				:	[Nombre del Responsable]
Descripcion del ajuste	:	1. 
							2. 						
Confluence				:	[PAGINA_CONFLUENCE]
================================================================================
*/

Create Or Alter Procedure BTOB.SP_Carga_BDCentral
As
Begin
	Set NOCOUNT On;
	Declare @EmpresaSincronizada BIT = 0
	EXEC BTOB.SP_Empresa_Sincronizar @EmpresaSincronizada OutPut

	IF @EmpresaSincronizada = 1
	Begin
		EXEC BTOB.SP_Tiendas_CargaBDCentral
		EXEC BTOB.SP_Cajas_CargaBDCentral
		EXEC BTOB.SP_Monedas_CargaBDCentral
		EXEC BTOB.SP_FormasPago_CargaBDCentral
		EXEC BTOB.SP_Almacenes_CargaBDCentral
		EXEC BTOB.SP_Articulos_CargaBDCentral
		EXEC BTOB.SP_Stock_CargaBDCentral
		EXEC BTOB.SP_Ventas_CargaBDCentral
	End
End
GO

-- ========================================
-- Indices
-- ========================================
GO

Create NonClustered Index IX_ColaVentas_Sincronizado_IDEmpresaVendedor 
ON BTOB.ColaVentas 
(
	Sincronizado
)
INCLUDE (IdEmpresa, CodVendedor, IDVendedor)
GO

Create NonClustered Index IX_ColaArticulos_CodArticuloTallaColor 
On BTOB.ColaArticulos
(
	CodArticulo, Talla, Color
)
INCLUDE (IDArticulo)
GO

Create NonClustered Index IX_ColaStock_IDAlmacen_StockArticuloFecha
ON BTOB.ColaStock
(
	IDAlmacen
)
Include(IDStock, IDArticulo, FechaModificado)
GO

CREATE NOnClustered Index IX_ColaStock_SincronizadoEsnuevo
ON [BTOB].[ColaStock]
(
Sincronizado ASC, EsNuevo ASC
)
INCLUDE(IDArticulo, IDAlmacen, Stock, EnTransito, FechaModificado)
GO
-- ejecutar en b2b
--Alter table BTOB.Catalogos.Clientes add IdEmpresa nvarchar(40)