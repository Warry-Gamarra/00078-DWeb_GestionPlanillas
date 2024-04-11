USE [BD_OCRH_GestionPlanillas]
GO


/*	-----------------------  Autenticacion	-----------------------  */


CREATE TABLE TC_Usuario
( 
	UserId               int IDENTITY ( 1,1 ) ,
	UserName             varchar(20)  NOT NULL ,
	D_FecActualizaPassword datetime  NULL ,
	B_CambiaPassword     bit  NOT NULL ,
	B_Habilitado         bit  NOT NULL ,
	B_Eliminado          bit  NOT NULL ,
	I_UsuarioCre         int  NULL ,
	D_FecCre             datetime  NULL ,
	I_UsuarioMod         int  NULL ,
	D_FecMod             datetime  NULL ,
	I_DependenciaID      int  NULL ,
	CONSTRAINT PK_Usuario PRIMARY KEY  NONCLUSTERED (UserId ASC)
)
go



CREATE TABLE webpages_Roles
( 
	RoleId               int IDENTITY ( 1,1 ) ,
	RoleName             varchar(50)  NOT NULL ,
	CONSTRAINT PK_Roles PRIMARY KEY  NONCLUSTERED (RoleId ASC)
)
go



CREATE TABLE webpages_UsersInRoles
( 
	UserId               int  NOT NULL ,
	RoleId               int  NOT NULL ,
	CONSTRAINT PK_UsersInRoles PRIMARY KEY  NONCLUSTERED (UserId ASC,RoleId ASC),
	CONSTRAINT FK_Roles_UserInRoles FOREIGN KEY (RoleId) REFERENCES webpages_Roles(RoleId)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
CONSTRAINT FK_Usuario_UsersInRoles FOREIGN KEY (UserId) REFERENCES TC_Usuario(UserId)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go



CREATE TABLE webpages_Membership
( 
	UserId               int  NOT NULL ,
	CreateDate           datetime  NULL ,
	ConfirmationToken    nvarchar(max)  NULL ,
	IsConfirmed          bit  NULL ,
	LastPasswordFailureDate datetime  NULL ,
	PasswordFailuresSinceLastSuccess int  NULL ,
	Password             nvarchar(max)  NULL ,
	PasswordChangedDate  datetime  NULL ,
	PasswordSalt         varchar(max)  NULL ,
	PasswordVerificationToken nvarchar(max)  NULL ,
	PasswordVerificationTokenExpirationDate datetime  NULL ,
	CONSTRAINT PK_Membership PRIMARY KEY  NONCLUSTERED (UserId ASC),
	CONSTRAINT FK_Usuario_Membership FOREIGN KEY (UserId) REFERENCES TC_Usuario(UserId)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go



CREATE TABLE TC_DatosUsuario
( 
	I_DatosUsuarioID     int IDENTITY ( 1,1 ) ,
	N_NumDoc             varchar(15)  NULL ,
	T_NomPersona         varchar(250)  NULL ,
	T_CorreoUsuario      varchar(100)  NULL ,
	I_UsuarioCre         int  NULL ,
	D_FecCre             datetime  NULL ,
	I_UsuarioMod         int  NULL ,
	D_FecMod             datetime  NULL ,
	CONSTRAINT PK_DatosUsuario PRIMARY KEY  NONCLUSTERED (I_DatosUsuarioID ASC)
)
go



CREATE TABLE TI_UsuarioDatosUsuario
( 
	UserId               int  NOT NULL ,
	I_DatosUsuarioID     int  NOT NULL ,
	D_FecAlta            datetime  NOT NULL ,
	D_FecBaja            datetime  NULL ,
	CONSTRAINT PK_UsuarioDatosUsuario PRIMARY KEY  NONCLUSTERED (UserId ASC,I_DatosUsuarioID ASC),
	CONSTRAINT FK_Usuario_UsuarioDatosUsuario FOREIGN KEY (UserId) REFERENCES TC_Usuario(UserId)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT FK_DatosUsuario_UsuarioDatosUsuario FOREIGN KEY (I_DatosUsuarioID) REFERENCES TC_DatosUsuario(I_DatosUsuarioID)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go


/*	-----------------------  Negocio	-----------------------  */


CREATE TABLE TC_TipoPlanilla
(
	I_TipoPlanillaID INT IDENTITY(1,1),
	C_TipoPlanillaCod VARCHAR(20) NOT NULL,
	T_TipoPlanillaDesc VARCHAR(250) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_TipoPlanilla PRIMARY KEY (I_TipoPlanillaID)
)

CREATE TABLE TC_ClasePlanilla
(
	I_ClasePlanillaID INT IDENTITY(1,1),
	I_TipoPlanillaID INT NOT NULL,
	C_ClasePlanillaCod VARCHAR(20) NOT NULL,
	T_ClasePlanillaDesc VARCHAR(250) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_ClasePlanilla PRIMARY KEY (I_ClasePlanillaID),
	CONSTRAINT FK_TipoPlanilla_ClasePlanilla FOREIGN KEY (I_TipoPlanillaID) REFERENCES TC_TipoPlanilla(I_TipoPlanillaID)
)

CREATE TABLE TC_CategoriaPlanilla
(
	I_CategoriaPlanillaID INT IDENTITY(1,1),
	I_ClasePlanillaID INT NOT NULL,
	T_CategoriaPlanillaDesc VARCHAR(250) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_CategoriaPlanilla PRIMARY KEY (I_CategoriaPlanillaID),
	CONSTRAINT FK_ClasePlanilla_CategoriaPlanilla FOREIGN KEY (I_ClasePlanillaID) REFERENCES TC_ClasePlanilla(I_ClasePlanillaID)
)

CREATE TABLE TC_TipoConcepto
(
	I_TipoConceptoID INT IDENTITY(1,1),
	T_TipoConceptoDesc VARCHAR(250) NOT NULL,
	B_EsAdicion BIT NOT NULL,
	B_IncluirEnTotalBruto BIT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_TipoConcepto PRIMARY KEY (I_TipoConceptoID)
)

CREATE TABLE TC_Concepto
(
	I_ConceptoID INT IDENTITY(1,1),
	I_TipoConceptoID INT NOT NULL,
	C_ConceptoCod VARCHAR(20) NOT NULL,
	T_ConceptoDesc VARCHAR(250) NOT NULL,
	T_ConceptoAbrv VARCHAR(250),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Concepto PRIMARY KEY (I_ConceptoID),
	CONSTRAINT FK_TipoConcepto_Concepto FOREIGN KEY (I_TipoConceptoID) REFERENCES TC_TipoConcepto(I_TipoConceptoID)
)

CREATE TABLE TI_PlantillaPlanilla
(
	I_PlantillaPlanillaID INT IDENTITY(1,1),
	I_CategoriaPlanillaID INT NOT NULL,
	T_PlantillaPlanillaDesc VARCHAR(250),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_PlantillaPlanilla PRIMARY KEY (I_PlantillaPlanillaID),
	CONSTRAINT FK_CategoriaPlanilla_PlantillaPlanilla FOREIGN KEY (I_CategoriaPlanillaID) REFERENCES TC_CategoriaPlanilla(I_CategoriaPlanillaID)
)

CREATE TABLE TI_PlantillaPlanilla_Concepto
(
	I_PlantillaPlanillaConceptoID INT IDENTITY(1,1),
	I_PlantillaPlanillaID INT NOT NULL,
	I_ConceptoID INT NOT NULL,
	B_EsValorFijo BIT NOT NULL,
	B_ValorEsExterno BIT NOT NULL,
	M_ValorConcepto DECIMAL(15,2),
	B_AplicarFiltro1 BIT NOT NULL,
	I_Filtro1 INT,
	B_AplicarFiltro2 BIT NOT NULL,
	I_Filtro2 INT,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_PlantillaPlanillaConcepto PRIMARY KEY (I_PlantillaPlanillaConceptoID),
	CONSTRAINT FK_PlantillaPlanilla_PlantillaPlanillaConcepto FOREIGN KEY (I_PlantillaPlanillaID) REFERENCES TI_PlantillaPlanilla(I_PlantillaPlanillaID),
	CONSTRAINT FK_Concepto_PlantillaPlanillaConcepto FOREIGN KEY (I_ConceptoID) REFERENCES TC_Concepto(I_ConceptoID)
)

CREATE TABLE TI_PlantillaPlanilla_Concepto_Referencia
(
	I_ID INT IDENTITY(1, 1),
	I_PlantillaPlanillaConceptoID INT NOT NULL,
	I_ConceptoReferenciaID INT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_PlantillaPlanillaConceptoReferencia PRIMARY KEY (I_ID),
	CONSTRAINT FK_PlantillaPlanillaConceptoReferencia_PlantillaPlanillaConcepto FOREIGN KEY (I_PlantillaPlanillaConceptoID) REFERENCES TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaConceptoID),
	CONSTRAINT FK_PlantillaPlanillaConceptoReferencia_Concepto FOREIGN KEY (I_ConceptoReferenciaID) REFERENCES TC_Concepto(I_ConceptoID),
)

CREATE TABLE TC_TipoDocumento
(
	I_TipoDocumentoID INT IDENTITY(1,1),
	T_TipoDocumentoCod VARCHAR(20),
	T_TipoDocumentoDesc VARCHAR(250),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_TipoDocumento PRIMARY KEY (I_TipoDocumentoID)
)

CREATE TABLE TC_Sexo
(
	I_SexoID INT IDENTITY(1,1),
	T_SexoCod VARCHAR(20),
	T_SexoDesc VARCHAR(250),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Sexo PRIMARY KEY (I_SexoID)
)

CREATE TABLE TC_Persona
(
	I_PersonaID INT IDENTITY(1,1),
	I_TipoDocumentoID INT,
	C_NumDocumento VARCHAR(20),
	T_Nombre VARCHAR(250) NOT NULL,
	T_ApellidoPaterno VARCHAR(250),
	T_ApellidoMaterno VARCHAR(250),
	I_SexoID INT NOT NULL,
	D_FecNac DATE,
	C_Cui varchar(20),
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Persona PRIMARY KEY (I_PersonaID),
	CONSTRAINT FK_TipoDocumento_Persona FOREIGN KEY (I_TipoDocumentoID) REFERENCES TC_TipoDocumento(I_TipoDocumentoID),
	CONSTRAINT FK_Sexo_Persona FOREIGN KEY (I_SexoID) REFERENCES TC_Sexo(I_SexoID)
)

CREATE TABLE TC_Vinculo
(
	I_VinculoID INT IDENTITY(1,1),
	T_VinculoDesc VARCHAR(250) NOT NULL,
	C_VinculoCod VARCHAR(20),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Vinculo PRIMARY KEY (I_VinculoID)
)

CREATE TABLE TC_Estado
(
	I_EstadoID INT IDENTITY(1,1),
	T_EstadoDesc VARCHAR(250) NOT NULL,
	C_EstadoCod VARCHAR(20),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Estado PRIMARY KEY (I_EstadoID)
)

CREATE TABLE TC_Regimen
(
	I_RegimenID INT IDENTITY(1,1),
	T_RegimenDesc VARCHAR(250) NOT NULL,
	C_RegimenCod VARCHAR(20) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Regimen PRIMARY KEY (I_RegimenID)
)

CREATE TABLE TC_Afp
(
	I_AfpID INT IDENTITY(1,1),
	T_AfpDesc VARCHAR(250) NOT NULL,
	C_AfpCod VARCHAR(20),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Afp PRIMARY KEY (I_AfpID)
)

CREATE TABLE TC_Trabajador
(
	I_TrabajadorID INT IDENTITY(1,1),
	I_PersonaID INT NOT NULL,
	C_TrabajadorCod VARCHAR(20) NOT NULL,
	C_CodigoPlaza VARCHAR(20),
	D_FechaIngreso DATE,
	I_EstadoID INT NOT NULL,
	I_VinculoID INT NOT NULL,
	I_RegimenID INT,
	I_AfpID INT,
	T_Cuspp VARCHAR(20),
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Trabajador PRIMARY KEY (I_TrabajadorID),
	CONSTRAINT FK_Persona_Trabajador FOREIGN KEY (I_PersonaID) REFERENCES TC_Persona(I_PersonaID),
	CONSTRAINT FK_Regimen_Trabajador FOREIGN KEY (I_RegimenID) REFERENCES TC_Regimen(I_RegimenID),
	CONSTRAINT FK_Estado_Trabajador FOREIGN KEY (I_EstadoID) REFERENCES TC_Estado(I_EstadoID),
	CONSTRAINT FK_Vinculo_Trabajador FOREIGN KEY (I_VinculoID) REFERENCES TC_Vinculo(I_VinculoID),
	CONSTRAINT FK_Afp_Trabajador FOREIGN KEY (I_AfpID) REFERENCES TC_Afp(I_AfpID)
)

CREATE TABLE TC_Banco
(
	I_BancoID INT IDENTITY(1,1),
	T_BancoDesc VARCHAR(250) NOT NULL,
	T_BancoAbrv VARCHAR(20),
	C_BancoCod VARCHAR(20) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Banco PRIMARY KEY (I_BancoID)
)

CREATE TABLE TC_TipoCuentaBancaria
(
	I_TipoCuentaBancariaID INT IDENTITY(1,1),
	T_TipoCuentaBancariaDesc VARCHAR(250) NOT NULL,
	C_TipoCuentaBancariaCod VARCHAR(20) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_TipoCuentaBancariaID PRIMARY KEY (I_TipoCuentaBancariaID)
)

CREATE TABLE TC_CuentaBancaria
(
	I_CuentaBancariaID INT IDENTITY(1,1),
	I_TrabajadorID INT NOT NULL,
	I_BancoID INT NOT NULL,
	T_NroCuentaBancaria VARCHAR(250) NOT NULL,
	I_TipoCuentaBancariaID INT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_CuentaBancaria PRIMARY KEY (I_CuentaBancariaID),
	CONSTRAINT PK_Trabajador_CuentaBancaria FOREIGN KEY (I_TrabajadorID) REFERENCES TC_Trabajador(I_TrabajadorID),
	CONSTRAINT PK_Banco_CuentaBancaria FOREIGN KEY (I_BancoID) REFERENCES TC_Banco(I_BancoID),
	CONSTRAINT PK_TipoCuentaBancaria_CuentaBancaria FOREIGN KEY (I_TipoCuentaBancariaID) REFERENCES TC_TipoCuentaBancaria(I_TipoCuentaBancariaID)
)

CREATE TABLE TC_Dependencia
(
	I_DependenciaID INT IDENTITY(1,1),
	T_DependenciaDesc VARCHAR(250) NOT NULL,
	C_DependenciaCod VARCHAR(20) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Dependencia PRIMARY KEY (I_DependenciaID)
)

CREATE TABLE TC_GrupoTrabajo
(
	I_GrupoTrabajoID INT IDENTITY(1,1),
	C_GrupoTrabajoCod VARCHAR(20),
	T_GrupoTrabajoDesc VARCHAR(250),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_GrupoTrabajo PRIMARY KEY (I_GrupoTrabajoID)
)

CREATE TABLE TC_Trabajador_CategoriaPlanilla
(
	I_TrabajadorCategoriaPlanillaID INT IDENTITY(1,1),
	I_TrabajadorID INT NOT NULL,
	I_CategoriaPlanillaID INT NOT NULL,
	B_CategoriaPrincipal BIT NOT NULL,
	B_EsJefe BIT NOT NULL,
	I_DependenciaID INT NOT NULL,
	I_GrupoTrabajoID INT,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_TrabajadorCategoriaPlanilla PRIMARY KEY (I_TrabajadorCategoriaPlanillaID),
	CONSTRAINT FK_Trabajador_TrabajadorCategoriaPlanilla FOREIGN KEY (I_TrabajadorID) REFERENCES TC_Trabajador(I_TrabajadorID),
	CONSTRAINT FK_CategoriaPlanilla_TrabajadorCategoriaPlanilla FOREIGN KEY (I_CategoriaPlanillaID) REFERENCES TC_CategoriaPlanilla(I_CategoriaPlanillaID),
	CONSTRAINT FK_Dependencia_TrabajadorCategoriaPlanilla FOREIGN KEY (I_DependenciaID) REFERENCES TC_Dependencia(I_DependenciaID),
	CONSTRAINT FK_GrupoTrabajo_TrabajadorCategoriaPlanilla FOREIGN KEY (I_GrupoTrabajoID) REFERENCES TC_GrupoTrabajo(I_GrupoTrabajoID)
)

CREATE TABLE TC_Anio
(
	I_Anio INT NOT NULL,
	CONSTRAINT PK_Anio PRIMARY KEY (I_Anio)
)

CREATE TABLE TR_Periodo
(
	I_PeriodoID INT IDENTITY(1,1),
	I_Anio INT NOT NULL,
	I_Mes INT NOT NULL,
	T_MesDesc VARCHAR(20) NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Periodo PRIMARY KEY (I_PeriodoID),
	CONSTRAINT FK_Anio_Periodo FOREIGN KEY (I_Anio) REFERENCES TC_Anio(I_Anio),
	CONSTRAINT UQ_Periodo UNIQUE(I_Anio, I_Mes)
)

CREATE TABLE TR_Planilla
(
	I_PlanillaID INT IDENTITY(1,1),
	I_PeriodoID INT NOT NULL,
	I_CategoriaPlanillaID INT NOT NULL,
	I_Correlativo INT NOT NULL,
	I_CantRegistros INT NOT NULL,
	I_TotalRemuneracion DECIMAL(15,2) NOT NULL,
	I_TotalReintegro DECIMAL(15,2) NOT NULL,
	I_TotalDeduccion DECIMAL(15,2) NOT NULL,
	I_TotalBruto DECIMAL(15,2) NOT NULL,
	I_TotalDescuento DECIMAL(15,2) NOT NULL,
	I_TotalSueldo DECIMAL(15,2) NOT NULL,
	B_Anulado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Planilla PRIMARY KEY (I_PlanillaID),
	CONSTRAINT FK_Periodo_Planilla FOREIGN KEY (I_PeriodoID) REFERENCES TR_Periodo(I_PeriodoID),
	CONSTRAINT FK_CategoriaPlanilla_Planilla FOREIGN KEY (I_CategoriaPlanillaID) REFERENCES TC_CategoriaPlanilla(I_CategoriaPlanillaID)
)

CREATE TABLE TR_TrabajadorPlanilla
(
	I_TrabajadorPlanillaID INT IDENTITY(1,1),
	I_PlanillaID INT NOT NULL,
	I_TrabajadorCategoriaPlanillaID INT NOT NULL,
	B_CategoriaPrincipal BIT NOT NULL,
	B_EsJefe BIT NOT NULL,
	I_DependenciaID INT NOT NULL,
	I_GrupoTrabajoID INT,
	I_VinculoID INT NOT NULL,
	I_TotalRemuneracion DECIMAL(15,2) NOT NULL,
	I_TotalReintegro DECIMAL(15,2) NOT NULL,
	I_TotalDeduccion DECIMAL(15,2) NOT NULL,
	I_TotalBruto DECIMAL(15,2) NOT NULL,
	I_TotalDescuento DECIMAL(15,2) NOT NULL,
	I_TotalSueldo DECIMAL(15,2) NOT NULL,
	B_Anulado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_TrabajadorPlanilla PRIMARY KEY (I_TrabajadorPlanillaID),
	CONSTRAINT FK_Planilla_TrabajadorPlanilla FOREIGN KEY (I_PlanillaID) REFERENCES TR_Planilla(I_PlanillaID),
	CONSTRAINT FK_TrabajadorCategoriaPlanilla_TrabajadorPlanilla FOREIGN KEY (I_TrabajadorCategoriaPlanillaID) REFERENCES TC_Trabajador_CategoriaPlanilla(I_TrabajadorCategoriaPlanillaID),
	CONSTRAINT FK_Dependencia_TrabajadorPlanilla FOREIGN KEY (I_DependenciaID) REFERENCES TC_Dependencia(I_DependenciaID),
	CONSTRAINT FK_Vinculo_TrabajadorPlanilla FOREIGN KEY (I_VinculoID) REFERENCES TC_Vinculo(I_VinculoID),
	CONSTRAINT FK_GrupoTrabajo_TrabajadorPlanilla FOREIGN KEY (I_GrupoTrabajoID) REFERENCES TC_GrupoTrabajo(I_GrupoTrabajoID)
)

CREATE TABLE TR_Concepto_TrabajadorPlanilla
(
	I_ConceptoTrabajadorPlanilla INT IDENTITY(1,1),
	I_TrabajadorPlanillaID INT NOT NULL,
	I_ConceptoID INT NOT NULL,
	C_ConceptoCod VARCHAR(20) NOT NULL,
	T_ConceptoDesc VARCHAR(250) NOT NULL,
	T_ConceptoAbrv VARCHAR(250),
	M_Monto DECIMAL(15,2) NOT NULL,
	I_Orden TINYINT NOT NULL,
	B_Anulado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_ConceptoTrabajadorPlanilla PRIMARY KEY (I_ConceptoTrabajadorPlanilla),
	CONSTRAINT FK_Planilla_ConceptoTrabajadorPlanilla FOREIGN KEY (I_TrabajadorPlanillaID) REFERENCES TR_TrabajadorPlanilla(I_TrabajadorPlanillaID),
	CONSTRAINT FK_Concepto_ConceptoTrabajadorPlanilla FOREIGN KEY (I_ConceptoID) REFERENCES TC_Concepto(I_ConceptoID)
)

CREATE TABLE TC_CategoriaDocente
(
	I_CategoriaDocenteID INT IDENTITY(1,1),
	C_CategoriaDocenteCod VARCHAR(20) NOT NULL,
	T_CategoriaDocenteDesc VARCHAR(250) NOT NULL,
	B_ParaDocenteOrdinario BIT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_CategoriaDocente PRIMARY KEY (I_CategoriaDocenteID)
)

CREATE TABLE TC_DedicacionDocente
(
	I_DedicacionDocenteID INT IDENTITY(1,1),
	C_DedicacionDocenteCod VARCHAR(20) NOT NULL,
	T_DedicacionDocenteDesc VARCHAR(250) NOT NULL,
	B_ParaDocenteOrdinario BIT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_DedicacionDocente PRIMARY KEY (I_DedicacionDocenteID)
)

CREATE TABLE TC_HorasDocente
(
	I_HorasDocenteID INT IDENTITY(1,1),
	I_DedicacionDocenteID INT NOT NULL,
	I_Horas INT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_HorasDocente PRIMARY KEY (I_HorasDocenteID)
)

CREATE TABLE TC_Docente
(
	I_DocenteID INT IDENTITY(1,1),
	I_TrabajadorID INT NOT NULL,
	I_CategoriaDocenteID INT NOT NULL,
	I_HorasDocenteID INT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Docente PRIMARY KEY (I_DocenteID),
	CONSTRAINT FK_Trabajador_Docente FOREIGN KEY (I_TrabajadorID) REFERENCES TC_Trabajador(I_TrabajadorID),
	CONSTRAINT FK_CategoriaDocente_Docente FOREIGN KEY (I_CategoriaDocenteID) REFERENCES TC_CategoriaDocente(I_CategoriaDocenteID),
	CONSTRAINT FK_HorasDocente_Docente FOREIGN KEY (I_HorasDocenteID) REFERENCES TC_HorasDocente(I_HorasDocenteID)
)

CREATE TABLE TC_GrupoOcupacional
(
	I_GrupoOcupacionalID INT IDENTITY(1,1),
	C_GrupoOcupacionalCod VARCHAR(20) NOT NULL,
	T_GrupoOcupacionalDesc VARCHAR(250) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_GrupoOcupacional PRIMARY KEY (I_GrupoOcupacionalID)
)

CREATE TABLE TC_NivelRemunerativo
(
	I_NivelRemunerativoID INT IDENTITY(1,1),
	C_NivelRemunerativoCod VARCHAR(20) NOT NULL,
	T_NivelRemunerativoDesc VARCHAR(250) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_NivelRemunerativo PRIMARY KEY (I_NivelRemunerativoID)
)

CREATE TABLE TC_Administrativo
(
	I_AdministrativoID INT IDENTITY(1,1),
	I_TrabajadorID INT NOT NULL,
	I_GrupoOcupacionalID INT NOT NULL,
	I_NivelRemunerativoID INT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Administrativo PRIMARY KEY (I_AdministrativoID),
	CONSTRAINT FK_GrupoOcupacional_Administrativo FOREIGN KEY (I_GrupoOcupacionalID) REFERENCES TC_GrupoOcupacional(I_GrupoOcupacionalID),
	CONSTRAINT FK_NivelRemunerativo_Administrativo FOREIGN KEY (I_NivelRemunerativoID) REFERENCES TC_NivelRemunerativo(I_NivelRemunerativoID)
)

CREATE TABLE TI_ValorExternoPeriodo
(
	I_ValorExternoPeriodoID INT IDENTITY(1,1),
	I_TrabajadorCategoriaPlanillaID INT NOT NULL,
	I_PeriodoID INT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_ValorExternoPeriodo PRIMARY KEY (I_ValorExternoPeriodoID),
	CONSTRAINT FK_TrabajadorCategoriaPlanilla_ValorExternoPeriodo FOREIGN KEY (I_TrabajadorCategoriaPlanillaID) REFERENCES TC_Trabajador_CategoriaPlanilla(I_TrabajadorCategoriaPlanillaID),
	CONSTRAINT FK_Periodo_ValorExternoPeriodo FOREIGN KEY (I_PeriodoID) REFERENCES TR_Periodo(I_PeriodoID)
)

CREATE TABLE TC_Proveedor
(
	I_ProveedorID INT IDENTITY(1,1),
	T_ProveedorDesc VARCHAR(250) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Proveedor PRIMARY KEY (I_ProveedorID)
)

CREATE TABLE TI_ValorExternoConcepto
(
	I_ConceptoExternoValorID INT IDENTITY(1,1),
	I_ValorExternoPeriodoID INT,
	I_ConceptoID INT NOT NULL,
	M_ValorConcepto DECIMAL(15,2) NOT NULL,
	I_ProveedorID INT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_ValorExternoConcepto PRIMARY KEY (I_ConceptoExternoValorID),
	CONSTRAINT FK_ValorExternoPeriodo_ValorExternoConcepto FOREIGN KEY (I_ValorExternoPeriodoID) REFERENCES TI_ValorExternoPeriodo(I_ValorExternoPeriodoID),
	CONSTRAINT FK_Concepto_ValorExternoConcepto FOREIGN KEY (I_ConceptoID) REFERENCES TC_Concepto(I_ConceptoID),
	CONSTRAINT FK_Proveedor_ValorExternoConcepto FOREIGN KEY (I_ProveedorID) REFERENCES TC_Proveedor(I_ProveedorID)
)

CREATE TABLE TI_AsistenciaTrabajador
(
	I_AsistenciaTrabajadorID INT IDENTITY(1,1),
	I_TrabajadorID INT NOT NULL,
	I_PeriodoID INT NOT NULL,
	B_TipoMedicion BIT NOT NULL,
	I_Valor INT NOT NULL,
	CONSTRAINT PK_AsistenciaTrabajador PRIMARY KEY(I_AsistenciaTrabajadorID),
	CONSTRAINT FK_Trabajador_AsistenciaTrabajador FOREIGN KEY (I_TrabajadorID) REFERENCES TC_Trabajador(I_TrabajadorID),
	CONSTRAINT FK_Periodo_AsistenciaTrabajador FOREIGN KEY (I_PeriodoID) REFERENCES TR_Periodo(I_PeriodoID)
)

CREATE TABLE TC_Actividad
(
	I_ActividadID INT IDENTITY(1,1),
	T_ActividadDesc VARCHAR(250),
	C_ActividadCod VARCHAR(20),
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Actividad PRIMARY KEY (I_ActividadID)
)

CREATE TABLE TC_Meta
(
	I_MetaID INT IDENTITY(1,1),
	T_MetaDesc VARCHAR(250),
	C_MetaCod VARCHAR(20),
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Meta PRIMARY KEY (I_MetaID)
)

CREATE TABLE TC_CategoriaPresupuestal
(
	I_CategoriaPresupuestalID INT IDENTITY(1,1),
	T_CategoriaPresupDesc VARCHAR(250),
	T_CategoriaPresupCod VARCHAR(250),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_CategoriaPresupuestal PRIMARY KEY (I_CategoriaPresupuestalID)
)

CREATE TABLE TC_DepActividadMeta
(
	I_DepActividadMetaID INT IDENTITY(1,1),
	I_Anio INT NOT NULL,
	I_CategoriaPlanillaID INT NOT NULL,
	I_DependenciaID INT NOT NULL,
	T_Descripcion VARCHAR(250),
	I_ActividadID INT NOT NULL,
	I_MetaID INT NOT NULL,
	I_CategoriaPresupuestalID INT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT FK_Anio_DepActividadMeta FOREIGN KEY (I_Anio) REFERENCES TC_Anio(I_Anio),
	CONSTRAINT PK_DepActividadMeta PRIMARY KEY(I_DepActividadMetaID),
	CONSTRAINT FK_CategoriaPlanilla_DepActividadMeta FOREIGN KEY(I_CategoriaPlanillaID) REFERENCES TC_CategoriaPlanilla(I_CategoriaPlanillaID),
	CONSTRAINT FK_Dependencia_DepActividadMeta FOREIGN KEY(I_DependenciaID) REFERENCES TC_Dependencia(I_DependenciaID),
	CONSTRAINT FK_Actividad_DepActividadMeta FOREIGN KEY(I_ActividadID) REFERENCES TC_Actividad(I_ActividadID),
	CONSTRAINT FK_Meta_DepActividadMeta FOREIGN KEY(I_MetaID) REFERENCES TC_Meta(I_MetaID),
	CONSTRAINT FK_CategoriaPresupuestal_DepActividadMeta FOREIGN KEY(I_CategoriaPresupuestalID) REFERENCES TC_CategoriaPresupuestal(I_CategoriaPresupuestalID)
)