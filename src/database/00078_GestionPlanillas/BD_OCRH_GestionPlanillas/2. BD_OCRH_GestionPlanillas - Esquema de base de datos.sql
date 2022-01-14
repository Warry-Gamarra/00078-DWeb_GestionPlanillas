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

