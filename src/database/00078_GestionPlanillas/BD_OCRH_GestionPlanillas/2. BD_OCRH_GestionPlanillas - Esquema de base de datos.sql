USE [BD_OCRH_GestionPlanillas]
GO


/*	-----------------------  Autenticacion	-----------------------  */


CREATE TABLE TC_Usuario
( 
	UserId               int IDENTITY ( 1,1 ) ,
	UserName             varchar(20)  NOT NULL ,
	I_UsuarioCrea        int  NULL ,
	D_FecActualiza       datetime  NULL ,
	B_CambiaPassword     bit  NOT NULL ,
	B_Habilitado         bit  NOT NULL ,
	B_Eliminado          bit  NOT NULL ,
	I_UsuarioCre         int  NULL ,
	D_FecCre             datetime  NULL ,
	I_UsuarioMod         int  NULL ,
	D_FecMod             datetime  NULL ,
	I_DependenciaID      int  NULL ,
	CONSTRAINT PK_Usuario PRIMARY KEY  NONCLUSTERED (UserId ASC),
	CONSTRAINT FK_Usuario_Usuario_UsuarioCrea FOREIGN KEY (I_UsuarioCrea) REFERENCES TC_Usuario(UserId)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
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
	D_FecRegistro        datetime  NULL ,
	D_FecActualiza       datetime  NULL ,
	B_Habilitado         bit  NOT NULL ,
	B_Eliminado          bit  NOT NULL ,
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
	B_Habilitado         bit  NOT NULL ,
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

CREATE TABLE TC_TipoConcepto
(
	I_TipoConceptoID INT IDENTITY(1,1),
	T_TipoConceptoDesc VARCHAR(250) NOT NULL,
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
	M_Valor DECIMAL(15,2),
	B_EsFijo BIT,
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
	I_ClasePlanillaID INT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_PlantillaPlanilla PRIMARY KEY (I_PlantillaPlanillaID),
	CONSTRAINT FK_ClasePlanilla_PlantillaPlanilla FOREIGN KEY (I_ClasePlanillaID) REFERENCES TC_ClasePlanilla(I_ClasePlanillaID)
)

CREATE TABLE TI_PlantillaPlanilla_Concepto
(
	I_PlantillaPlanillaConceptoID INT IDENTITY(1,1),
	I_PlantillaPlanillaID INT NOT NULL,
	I_ConceptoID INT NOT NULL,
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

CREATE TABLE TC_TipoDocumento
(
	I_TipoDocumentoID INT IDENTITY(1,1),
	T_TipoDocumentoDesc VARCHAR(250),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_TipoDocumento PRIMARY KEY (I_TipoDocumentoID)
)

CREATE TABLE TC_Persona
(
	I_PersonaID INT IDENTITY(1,1),
	I_TipoDocumentoID INT,
	C_NumDocumento VARCHAR(20),
	T_Nombre VARCHAR(250) NOT NULL,
	T_ApellidoPaterno VARCHAR(250),
	T_ApellidoMaterno VARCHAR(250),
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Persona PRIMARY KEY (I_PersonaID),
	CONSTRAINT FK_TipoDocumento_Persona FOREIGN KEY (I_TipoDocumentoID) REFERENCES TC_TipoDocumento(I_TipoDocumentoID)
)

CREATE TABLE TC_Vinculo
(
	I_VinculoID INT IDENTITY(1,1),
	T_VinculoDesc VARCHAR(250) NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Vinculo PRIMARY KEY (I_VinculoID)
)

CREATE TABLE TC_Trabajador
(
	I_TrabajadorID INT IDENTITY(1,1),
	I_PersonaID INT NOT NULL,
	D_FechaIngreso DATE,
	X_Regimen VARCHAR(250),
	X_Dependencia VARCHAR(250),
	I_ClasePlanillaID INT NOT NULL,
	I_VinculoID INT NOT NULL,
	B_Habilitado BIT NOT NULL,
	B_Eliminado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Trabajador PRIMARY KEY (I_TrabajadorID),
	CONSTRAINT FK_Persona_Trabajador FOREIGN KEY (I_PersonaID) REFERENCES TC_Persona(I_PersonaID),
	CONSTRAINT FK_ClasePlanilla_Trabajador FOREIGN KEY (I_ClasePlanillaID) REFERENCES TC_ClasePlanilla(I_ClasePlanillaID),
	CONSTRAINT FK_Vinculo_Trabajador FOREIGN KEY (I_VinculoID) REFERENCES TC_Vinculo(I_VinculoID),
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
	CONSTRAINT PK_Periodo PRIMARY KEY (I_PeriodoID)
)

CREATE TABLE TR_Planilla
(
	I_PlanillaID INT IDENTITY(1,1),
	I_PeriodoID INT NOT NULL,
	I_ClasePlanillaID INT NOT NULL,
	I_Correlativo INT NOT NULL,
	I_CantRegistros INT NOT NULL,
	I_TotalRemuneracion DECIMAL(15,2) NOT NULL,
	I_TotalDescuento DECIMAL(15,2) NOT NULL,
	I_TotalReintegro DECIMAL(15,2) NOT NULL,
	I_TotalDeduccion DECIMAL(15,2) NOT NULL,
	I_TotalSueldo DECIMAL(15,2) NOT NULL,
	B_Anulado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_Planilla PRIMARY KEY (I_PlanillaID),
	CONSTRAINT FK_Periodo_Planilla FOREIGN KEY (I_PeriodoID) REFERENCES TR_Periodo(I_PeriodoID),
	CONSTRAINT FK_ClasePlanilla_Planilla FOREIGN KEY (I_ClasePlanillaID) REFERENCES TC_ClasePlanilla(I_ClasePlanillaID)
)

CREATE TABLE TR_TrabajadorPlanilla
(
	I_TrabajadorPlanillaID INT IDENTITY(1,1),
	I_PlanillaID INT NOT NULL,
	I_TrabajadorID INT NOT NULL,
	I_TotalRemuneracion DECIMAL(15,2) NOT NULL,
	I_TotalDescuento DECIMAL(15,2) NOT NULL,
	I_TotalReintegro DECIMAL(15,2) NOT NULL,
	I_TotalDeduccion DECIMAL(15,2) NOT NULL,
	I_TotalSueldo DECIMAL(15,2) NOT NULL,
	B_Anulado BIT NOT NULL,
	I_UsuarioCre INT,
	D_FecCre DATETIME,
	I_UsuarioMod INT,
	D_FecMod DATETIME,
	CONSTRAINT PK_TrabajadorPlanilla PRIMARY KEY (I_TrabajadorPlanillaID),
	CONSTRAINT FK_Planilla_TrabajadorPlanilla FOREIGN KEY (I_PlanillaID) REFERENCES TR_Planilla(I_PlanillaID),
	CONSTRAINT FK_Trabajador_TrabajadorPlanilla FOREIGN KEY (I_TrabajadorID) REFERENCES TC_Trabajador(I_TrabajadorID)
)

CREATE TABLE TR_Concepto_TrabajadorPlanilla
(
	I_ConceptoTrabajadorPlanilla INT IDENTITY(1,1),
	I_TrabajadorPlanillaID INT NOT NULL,
	I_ConceptoID INT NOT NULL,
	C_ConceptoCod VARCHAR(20) NOT NULL,
	T_ConceptoDesc VARCHAR(250) NOT NULL,
	M_Monto DECIMAL(15,2) NOT NULL,
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