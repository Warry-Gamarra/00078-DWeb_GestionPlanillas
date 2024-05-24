USE BD_OCRH_GestionPlanillas
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarTrabajador')
	DROP PROCEDURE [dbo].[USP_I_RegistrarTrabajador]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarTrabajador]
@C_TrabajadorCod VARCHAR(20),
@C_CodigoPlaza VARCHAR(20) = NULL,
@I_PersonaID INT = NULL,
@T_ApellidoPaterno VARCHAR(250),
@T_ApellidoMaterno VARCHAR(250),
@T_Nombre VARCHAR(250),
@I_TipoDocumentoID INT,
@C_NumDocumento VARCHAR(20),
@I_SexoID INT,
@D_FechaIngreso DATETIME = NULL,
@I_RegimenID INT,
@I_EstadoID INT,
@I_VinculoID INT,
@I_BancoID INT = NULL,
@T_NroCuentaBancaria VARCHAR(250) = NULL,
@I_TipoCuentaBancariaID INT = NULL,
@I_DependenciaID INT,
@I_AfpID INT = NULL,
@T_Cuspp VARCHAR(20) = NULL,
@I_CategoriaDocenteID INT = NULL,
@I_HorasDocenteID INT = NULL,
@I_GrupoOcupacionalID INT = NULL,
@I_NivelRemunerativoID INT = NULL,
@I_CategoriaPlanillaID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @I_TrabajadorID INT,
			@C_VinculoCod VARCHAR(20),
			@D_FecCre DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE();

		IF (@I_PersonaID IS NULL OR @I_PersonaID = 0) BEGIN

			INSERT dbo.TC_Persona(T_ApellidoPaterno, T_ApellidoMaterno, T_Nombre, I_TipoDocumentoID, C_NumDocumento, I_SexoID, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@T_ApellidoPaterno, @T_ApellidoMaterno, @T_Nombre, @I_TipoDocumentoID, @C_NumDocumento, @I_SexoID, 0, @I_UserID, @D_FecCre);

			SET @I_PersonaID = SCOPE_IDENTITY();
		END

		INSERT dbo.TC_Trabajador(I_PersonaID, C_TrabajadorCod, C_CodigoPlaza, D_FechaIngreso, I_EstadoID, I_VinculoID, I_RegimenID, I_AfpID, T_Cuspp, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_PersonaID, @C_TrabajadorCod, @C_CodigoPlaza, @D_FechaIngreso, @I_EstadoID, @I_VinculoID, @I_RegimenID, @I_AfpID, @T_Cuspp, 0, @I_UserID, @D_FecCre);

		SET @I_TrabajadorID = SCOPE_IDENTITY();
		
		SET @C_VinculoCod = (SELECT v.C_VinculoCod FROM dbo.TC_Vinculo v WHERE v.I_VinculoID = @I_VinculoID);

		INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_CategoriaPrincipal, B_EsJefe, I_DependenciaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_TrabajadorID, @I_CategoriaPlanillaID, 1, 0, @I_DependenciaID, 1, 0, @I_UserID, @D_FecCre);

		IF (@I_BancoID IS NOT NULL AND @T_NroCuentaBancaria IS NOT NULL AND LEN(@T_NroCuentaBancaria) > 0 AND @I_TipoCuentaBancariaID IS NOT NULL) BEGIN
			INSERT dbo.TC_CuentaBancaria(I_TrabajadorID, I_BancoID, T_NroCuentaBancaria, I_TipoCuentaBancariaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@I_TrabajadorID, @I_BancoID, @T_NroCuentaBancaria, @I_TipoCuentaBancariaID, 1, 0, @I_UserID, @D_FecCre);
		END

		IF (@C_VinculoCod IN ('AP','AC')) BEGIN
			INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@I_TrabajadorID, @I_GrupoOcupacionalID, @I_NivelRemunerativoID, 1, 0, @I_UserID, @D_FecCre);
		END

		IF (@C_VinculoCod IN ('DP','DC')) BEGIN
			INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@I_TrabajadorID, @I_CategoriaDocenteID, @I_HorasDocenteID, 1, 0, @I_UserID, @D_FecCre);
		END

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarTrabajador')
	DROP PROCEDURE [dbo].[USP_U_ActualizarTrabajador]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarTrabajador]
@I_TrabajadorID INT,
@C_TrabajadorCod VARCHAR(20),
@C_CodigoPlaza VARCHAR(20) = NULL,
@T_ApellidoPaterno VARCHAR(250),
@T_ApellidoMaterno VARCHAR(250),
@T_Nombre VARCHAR(250),
@I_TipoDocumentoID INT,
@C_NumDocumento VARCHAR(20),
@I_SexoID INT,
@D_FechaIngreso DATETIME = NULL,
@I_RegimenID INT,
@I_EstadoID INT,
@I_VinculoID INT,
@I_BancoID INT = NULL,
@T_NroCuentaBancaria VARCHAR(250) = NULL,
@I_TipoCuentaBancariaID INT = NULL,
@I_DependenciaID INT,
@I_AfpID INT = NULL,
@T_Cuspp VARCHAR(20) = NULL,
@I_CategoriaDocenteID INT = NULL,
@I_HorasDocenteID INT = NULL,
@I_GrupoOcupacionalID INT = NULL,
@I_NivelRemunerativoID INT = NULL,
@I_CategoriaPlanillaID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @I_ADMINISTRATIVOID INT = 1,
			@I_DOCENTEID INT = 2;

	DECLARE @C_VinculoCod VARCHAR(20),
			@D_FecMod DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE();

		--Actualizar Persona
		UPDATE per SET 
			per.T_ApellidoPaterno = @T_ApellidoPaterno,
			per.T_ApellidoMaterno = @T_ApellidoMaterno,
			per.T_Nombre = @T_Nombre,
			per.I_TipoDocumentoID = @I_TipoDocumentoID,
			per.C_NumDocumento = @C_NumDocumento,
			per.I_SexoID = @I_SexoID,
			per.I_UsuarioMod = @I_UserID,
			per.D_FecMod = @D_FecMod
		FROM dbo.TC_Persona per
		INNER JOIN dbo.TC_Trabajador trab ON trab.I_PersonaID = per.I_PersonaID
		WHERE trab.I_TrabajadorID = @I_TrabajadorID;

		--Actualizar Trabajador
		UPDATE dbo.TC_Trabajador SET
			C_TrabajadorCod = @C_TrabajadorCod,
			C_CodigoPlaza = @C_CodigoPlaza,
			D_FechaIngreso = @D_FechaIngreso,
			I_EstadoID = @I_EstadoID,
			I_VinculoID = @I_VinculoID,
			I_RegimenID = @I_RegimenID,
			I_AfpID = @I_AfpID,
			T_Cuspp = @T_Cuspp,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_TrabajadorID = @I_TrabajadorID;

		--Actualizar CategoriaPlanilla de Trabajador
		UPDATE dbo.TC_Trabajador_CategoriaPlanilla SET
			I_DependenciaID = @I_DependenciaID,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_TrabajadorID = @I_TrabajadorID AND I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND B_CategoriaPrincipal = 1 AND B_Habilitado = 1 AND B_Eliminado = 0;

		--Actualizar Cuentas Bancarias
		IF (@I_BancoID IS NULL) BEGIN

			UPDATE dbo.TC_CuentaBancaria SET
				B_Habilitado = 0,
				I_UsuarioMod = @I_UserID,
				D_FecMod = @D_FecMod
			WHERE I_TrabajadorID = @I_TrabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0;

		END ELSE BEGIN

			IF EXISTS(SELECT I_TrabajadorID FROM dbo.TC_CuentaBancaria 
				WHERE I_TrabajadorID = @I_TrabajadorID AND I_BancoID = @I_BancoID AND B_Eliminado = 0)
			BEGIN
				IF EXISTS(SELECT I_TrabajadorID FROM dbo.TC_CuentaBancaria 
					WHERE I_TrabajadorID = @I_TrabajadorID AND I_BancoID = @I_BancoID AND B_Habilitado = 1 AND B_Eliminado = 0)
				BEGIN
					UPDATE dbo.TC_CuentaBancaria SET
						T_NroCuentaBancaria = @T_NroCuentaBancaria,
						I_TipoCuentaBancariaID = @I_TipoCuentaBancariaID,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecMod
					WHERE I_TrabajadorID = @I_TrabajadorID AND I_BancoID = @I_BancoID AND B_Habilitado = 1 AND B_Eliminado = 0
				END ELSE BEGIN
					UPDATE dbo.TC_CuentaBancaria SET
						B_Habilitado = 0,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecMod
					WHERE I_TrabajadorID = @I_TrabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

					UPDATE dbo.TC_CuentaBancaria SET
						T_NroCuentaBancaria = @T_NroCuentaBancaria,
						I_TipoCuentaBancariaID = @I_TipoCuentaBancariaID,
						B_Habilitado = 1,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecMod
					WHERE I_TrabajadorID = @I_TrabajadorID AND I_BancoID = @I_BancoID AND B_Eliminado = 0
				END
			END ELSE BEGIN
				UPDATE dbo.TC_CuentaBancaria SET
					B_Habilitado = 0,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_TrabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

				INSERT dbo.TC_CuentaBancaria(I_TrabajadorID, I_BancoID, T_NroCuentaBancaria, I_TipoCuentaBancariaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorID, @I_BancoID, @T_NroCuentaBancaria, @I_TipoCuentaBancariaID, 1, 0, @I_UserID, @D_FecMod)
			END

		END

		SET @C_VinculoCod = (SELECT v.C_VinculoCod FROM dbo.TC_Vinculo v WHERE v.I_VinculoID = @I_VinculoID);

		IF (@C_VinculoCod IN ('AP','AC')) BEGIN
			IF EXISTS(SELECT doc.I_DocenteID FROM dbo.TC_Docente doc 
				WHERE doc.I_TrabajadorID = @I_TrabajadorID AND doc.B_Habilitado = 1 AND doc.B_Eliminado = 0) 
			BEGIN
				UPDATE dbo.TC_Docente SET 
					B_Habilitado = 0,
					B_Eliminado = 1,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_TrabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0
				
				UPDATE dbo.TC_Trabajador_CategoriaPlanilla SET
					B_Habilitado = 0,
					B_Eliminado = 1,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_TrabajadorID AND I_CategoriaPlanillaID = @I_DOCENTEID AND B_Habilitado = 1 AND B_Eliminado = 0

				INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorID, @I_GrupoOcupacionalID, @I_NivelRemunerativoID, 1, 0, @I_UserID, @D_FecMod)

				INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorID, @I_ADMINISTRATIVOID, 1, 0, @I_UserID, @D_FecMod)
			END
			ELSE BEGIN
				UPDATE dbo.TC_Administrativo SET 
					I_GrupoOcupacionalID = @I_GrupoOcupacionalID,
					I_NivelRemunerativoID = @I_NivelRemunerativoID,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_TrabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0
			END
		END
		
		IF (@C_VinculoCod IN ('DP','DC')) BEGIN
			IF EXISTS(SELECT adm.I_AdministrativoID FROM dbo.TC_Administrativo adm 
				WHERE adm.I_TrabajadorID = @I_trabajadorID AND adm.B_Habilitado = 1 AND adm.B_Eliminado = 0)
			BEGIN
				UPDATE dbo.TC_Administrativo SET 
					B_Habilitado = 0,
					B_Eliminado = 1,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_TrabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

				UPDATE dbo.TC_Trabajador_CategoriaPlanilla SET
					B_Habilitado = 0,
					B_Eliminado = 1,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_TrabajadorID AND I_CategoriaPlanillaID = @I_ADMINISTRATIVOID AND B_Habilitado = 1 AND B_Eliminado = 0
				
				INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorID, @I_CategoriaDocenteID, @I_HorasDocenteID, 1, 0, @I_UserID, @D_FecMod)

				INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorID, @I_DOCENTEID, 1, 0, @I_UserID, @D_FecMod)
			END
			ELSE BEGIN
				UPDATE dbo.TC_Docente SET 
					I_CategoriaDocenteID = @I_CategoriaDocenteID,
					I_HorasDocenteID = @I_HorasDocenteID,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_TrabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0
			END
		END
		
		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_S_ObtenerParametros_PlantillaPlanilla')
	DROP PROCEDURE [dbo].[USP_S_ObtenerParametros_PlantillaPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_S_ObtenerParametros_PlantillaPlanilla]
@I_PlantillaPlanillaID INT,
@I_ConceptoID INT,
@I_Filtro1 INT,
@I_Filtro2 INT,
@I_PlantillaPlanillaConceptoID INT OUTPUT,
@B_EsValorFijo BIT OUTPUT,
@B_ValorEsExterno BIT OUTPUT,
@M_ValorConcepto DECIMAL(15,2) OUTPUT,
@B_ConceptoObtenido BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF ((SELECT COUNT(ppc.I_PlantillaPlanillaConceptoID) FROM dbo.TI_PlantillaPlanilla_Concepto ppc
		WHERE ppc.I_PlantillaPlanillaID = @I_PlantillaPlanillaID AND ppc.I_ConceptoID = @I_ConceptoID AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND
			ppc.B_AplicarFiltro1 = 1 AND ppc.I_Filtro1 = @I_Filtro1 AND ppc.B_AplicarFiltro2 = 1 AND ppc.I_Filtro2 = @I_Filtro2) = 1)
	BEGIN
		SELECT
			@I_PlantillaPlanillaConceptoID = ppc.I_PlantillaPlanillaConceptoID,
			@B_EsValorFijo = B_EsValorFijo,
			@B_ValorEsExterno = B_ValorEsExterno,
			@M_ValorConcepto = M_ValorConcepto,
			@B_ConceptoObtenido = 1
		FROM dbo.TI_PlantillaPlanilla_Concepto ppc
		WHERE ppc.I_PlantillaPlanillaID = @I_PlantillaPlanillaID AND ppc.I_ConceptoID = @I_ConceptoID AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND
			ppc.B_AplicarFiltro1 = 1 AND ppc.I_Filtro1 = @I_Filtro1 AND ppc.B_AplicarFiltro2 = 1 AND ppc.I_Filtro2 = @I_Filtro2;
	END
	ELSE
	BEGIN
		IF ((SELECT COUNT(ppc.I_PlantillaPlanillaConceptoID) FROM dbo.TI_PlantillaPlanilla_Concepto ppc
			WHERE ppc.I_PlantillaPlanillaID = @I_PlantillaPlanillaID AND ppc.I_ConceptoID = @I_ConceptoID AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND
				ppc.B_AplicarFiltro1 = 1 AND ppc.I_Filtro1 = @I_Filtro1 AND ppc.B_AplicarFiltro2 = 0) = 1)
		BEGIN
			SELECT
				@I_PlantillaPlanillaConceptoID = ppc.I_PlantillaPlanillaConceptoID,
				@B_EsValorFijo = B_EsValorFijo,
				@B_ValorEsExterno = B_ValorEsExterno,
				@M_ValorConcepto = M_ValorConcepto,
				@B_ConceptoObtenido = 1
			FROM dbo.TI_PlantillaPlanilla_Concepto ppc
			WHERE ppc.I_PlantillaPlanillaID = @I_PlantillaPlanillaID AND ppc.I_ConceptoID = @I_ConceptoID AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND
				ppc.B_AplicarFiltro1 = 1 AND ppc.I_Filtro1 = @I_Filtro1 AND ppc.B_AplicarFiltro2 = 0;
		END
		ELSE BEGIN
			IF ((SELECT COUNT(ppc.I_PlantillaPlanillaConceptoID) FROM dbo.TI_PlantillaPlanilla_Concepto ppc
				WHERE ppc.I_PlantillaPlanillaID = @I_PlantillaPlanillaID AND ppc.I_ConceptoID = @I_ConceptoID AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND
					ppc.B_AplicarFiltro1 = 0 AND ppc.B_AplicarFiltro2 = 1 AND ppc.I_Filtro2 = @I_Filtro2) = 1)
			BEGIN
				SELECT
					@I_PlantillaPlanillaConceptoID = ppc.I_PlantillaPlanillaConceptoID,
					@B_EsValorFijo = B_EsValorFijo,
					@B_ValorEsExterno = B_ValorEsExterno,
					@M_ValorConcepto = M_ValorConcepto,
					@B_ConceptoObtenido = 1
				FROM dbo.TI_PlantillaPlanilla_Concepto ppc
				WHERE ppc.I_PlantillaPlanillaID = @I_PlantillaPlanillaID AND ppc.I_ConceptoID = @I_ConceptoID AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND
					ppc.B_AplicarFiltro1 = 0 AND ppc.B_AplicarFiltro2 = 1 AND ppc.I_Filtro2 = @I_Filtro2;
			END
			ELSE BEGIN
				IF ((SELECT COUNT(ppc.I_PlantillaPlanillaConceptoID) FROM dbo.TI_PlantillaPlanilla_Concepto ppc
					WHERE ppc.I_PlantillaPlanillaID = @I_PlantillaPlanillaID AND ppc.I_ConceptoID = @I_ConceptoID AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND
						ppc.B_AplicarFiltro1 = 0 AND ppc.B_AplicarFiltro2 = 0) = 1)
				BEGIN
					SELECT
						@I_PlantillaPlanillaConceptoID = ppc.I_PlantillaPlanillaConceptoID,
						@B_EsValorFijo = B_EsValorFijo,
						@B_ValorEsExterno = B_ValorEsExterno,
						@M_ValorConcepto = M_ValorConcepto,
						@B_ConceptoObtenido = 1
					FROM dbo.TI_PlantillaPlanilla_Concepto ppc
					WHERE ppc.I_PlantillaPlanillaID = @I_PlantillaPlanillaID AND ppc.I_ConceptoID = @I_ConceptoID AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND
						ppc.B_AplicarFiltro1 = 0 AND ppc.B_AplicarFiltro2 = 0;
				END
			END
		END
	END
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.DOMAINS WHERE DOMAIN_NAME = 'type_dataIdentifiers') BEGIN
	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_GenerarPlanilla_Docente_Administrativo') BEGIN
		DROP PROCEDURE [dbo].[USP_I_GenerarPlanilla_Docente_Administrativo]
	END
	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarPlantillaPlanillaConcepto') BEGIN
		DROP PROCEDURE [dbo].[USP_I_RegistrarPlantillaPlanillaConcepto]
	END
	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarPlantillaPlanillaConcepto') BEGIN
		DROP PROCEDURE [dbo].[USP_U_ActualizarPlantillaPlanillaConcepto]
	END

	DROP TYPE [dbo].[type_dataIdentifiers]
END
GO

CREATE TYPE [dbo].[type_dataIdentifiers] AS TABLE(
	I_ID int
)
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_GenerarPlanilla_Docente_Administrativo')
	DROP PROCEDURE [dbo].[USP_I_GenerarPlanilla_Docente_Administrativo]
GO

CREATE PROCEDURE [dbo].[USP_I_GenerarPlanilla_Docente_Administrativo]
@Tbl_Trabajador [dbo].[type_dataIdentifiers] READONLY,
@I_Anio INT,
@I_Mes INT,
@I_CategoriaPlanillaID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF (@I_Anio IS NULL) BEGIN
		SET @B_Result = 0
		SET @T_Message = 'El año es obligatorio.'
		RETURN 0;
	END

	IF (@I_Mes IS NULL) BEGIN
		SET @B_Result = 0
		SET @T_Message = 'El mes es obligatorio.'
		RETURN 0;
	END

	IF (@I_categoriaPlanillaID IS NULL) BEGIN
		SET @B_Result = 0
		SET @T_Message = 'La categoría de planilla es obligatorio.'
		RETURN 0;
	END

	IF (@I_UserID IS NULL) BEGIN
		SET @B_Result = 0
		SET @T_Message = 'No se ha especificado el usuario que está realizando la acción.'
		RETURN 0;
	END

	IF NOT EXISTS (SELECT pr.I_PeriodoID FROM dbo.TR_Periodo pr WHERE pr.I_Anio = @I_Anio AND pr.I_Mes = @I_Mes) BEGIN
		SET @B_Result = 0
		SET @T_Message = 'No existe el periodo.'
		RETURN 0;
	END
		
	DECLARE --Cabecera plantilla
			@I_PeriodoID INT,
			@I_CorrelativoPlanilla INT,
			@I_Indicador INT,
			@I_CantRegistros INT,
			@I_PlantillaID INT,
			--Resumen cabecera planilla
			@I_TotalRemuneracion DECIMAL(15,2) = 0,
			@I_TotalReintegro DECIMAL(15,2) = 0,
			@I_TotalDeduccion DECIMAL(15,2) = 0,
			@I_TotalBruto DECIMAL(15,2) = 0,
			@I_TotalDescuento DECIMAL(15,2) = 0,
			@I_TotalSueldo DECIMAL(15,2) = 0,
			@I_CantPlanillasGeneradas INT = 0,
			--Resumen por trabajador
			@I_TrabajadorCategoriaPlanillaID INT,
			@B_CategoriaPrincipal BIT,
			@B_EsJefe BIT,
			@I_DependenciaID INT,
			@I_GrupoTrabajoID INT,
			@I_VinculoID INT,
			@I_Filtro1 INT,-->Grupo Ocupacional | Categoria
			@I_Filtro2 INT,-->Nivel Remunerativo | Dedicacion y horas
			@I_TrabajadorPlanillaID INT,
			@I_TotalRemuneracionTrabajador DECIMAL(15,2),
			@I_TotalReintegroTrabajador DECIMAL(15,2),
			@I_TotalDeduccionTrabajador DECIMAL(15,2),
			@I_TotalBrutoTrabajador DECIMAL(15,2),
			@I_TotalDescuentoTrabajador DECIMAL(15,2),
			@I_TotalSueldoTrabajador DECIMAL(15,2),
			--Detalle conceptos por trabajador
			@I_NroOrden INT,
			@I_PlantillaPlanillaID INT,
			@I_CantConceptos INT,
			@B_ConceptoObtenido BIT,
			@I_ConceptoID INT,
			@C_ConceptoCod VARCHAR(20),
			@T_ConceptoDesc VARCHAR(250),
			@T_ConceptoAbrv VARCHAR(250),
			@I_PlantillaPlanillaConceptoID INT,
			@B_EsValorFijo BIT,
			@B_ValorEsExterno BIT,
			@M_ValorConcepto DECIMAL(15,2),
			@M_SumConceptos DECIMAL(15,2),
			--Variables generales
			@I_ADMINISTRATIVOID INT = 1,
			@I_DOCENTEID INT = 2,
			@D_FecRegistro DATETIME  = GETDATE(),
			@I_Remunerativo INT = 1,
			@I_Descuento INT = 2,
			@I_Reintegro INT = 4,
			@I_Deduccion INT = 10

	CREATE TABLE #tmp_trabajador
	(
		I_NroOrden INT IDENTITY(1,1),
		I_TrabajadorCategoriaPlanillaID INT NOT NULL,
		B_CategoriaPrincipal BIT NOT NULL,
		B_EsJefe BIT NOT NULL,
		I_DependenciaID INT NOT NULL,
		I_GrupoTrabajoID INT,
		I_VinculoID INT NOT NULL,
		I_Filtro1 INT,
		I_Filtro2 INT
	);

	DECLARE @tmp_remuneracion TABLE(
		I_NroOrden INT,
		I_PlantillaPlanillaID INT,
		I_ConceptoID INT,
		C_ConceptoCod VARCHAR(20),
		T_ConceptoDesc VARCHAR(250),
		T_ConceptoAbrv VARCHAR(250));

	DECLARE @tmp_descuento TABLE(
		I_NroOrden INT,
		I_PlantillaPlanillaID INT,
		I_ConceptoID INT,
		C_ConceptoCod VARCHAR(20),
		T_ConceptoDesc VARCHAR(250),
		T_ConceptoAbrv VARCHAR(250));

	DECLARE @tmp_reintegro TABLE(
		I_NroOrden INT,
		I_PlantillaPlanillaID INT,
		I_ConceptoID INT,
		C_ConceptoCod VARCHAR(20),
		T_ConceptoDesc VARCHAR(250),
		T_ConceptoAbrv VARCHAR(250));

	DECLARE @tmp_deduccion TABLE(
		I_NroOrden INT,
		I_PlantillaPlanillaID INT,
		I_ConceptoID INT,
		C_ConceptoCod VARCHAR(20),
		T_ConceptoDesc VARCHAR(250),
		T_ConceptoAbrv VARCHAR(250));

	--1. Obtener valores para la cabecera de la planilla
	SET @I_PeriodoID = (SELECT pr.I_PeriodoID FROM dbo.TR_Periodo pr WHERE pr.I_Anio = @I_Anio AND pr.I_Mes = @I_Mes);

	SET @I_CorrelativoPlanilla = ISNULL((SELECT MAX(pl.I_Correlativo) FROM dbo.TR_Planilla pl 
		WHERE pl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID), 0) + 1;

	--2. Obtener la lista de trabajadores
	IF (@I_CategoriaPlanillaID = @I_ADMINISTRATIVOID) BEGIN
		INSERT #tmp_trabajador(I_TrabajadorCategoriaPlanillaID, I_DependenciaID, I_VinculoID, I_Filtro1, I_Filtro2, B_CategoriaPrincipal, B_EsJefe, I_GrupoTrabajoID)
		SELECT tca.I_TrabajadorCategoriaPlanillaID, tca.I_DependenciaID, trab.I_VinculoID, adm.I_GrupoOcupacionalID, adm.I_NivelRemunerativoID,
			tca.B_CategoriaPrincipal, tca.B_EsJefe, tca.I_GrupoTrabajoID
		FROM dbo.TC_Administrativo adm
		INNER JOIN dbo.TC_Trabajador trab ON trab.I_TrabajadorID = adm.I_TrabajadorID
		INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla tca ON tca.I_TrabajadorID = trab.I_TrabajadorID 
		INNER JOIN dbo.TC_GrupoOcupacional gr ON gr.I_GrupoOcupacionalID = adm.I_GrupoOcupacionalID
		INNER JOIN dbo.TC_NivelRemunerativo nivrem ON nivrem.I_NivelRemunerativoID = adm.I_NivelRemunerativoID
		INNER JOIN @Tbl_Trabajador tmp ON tmp.I_ID = tca.I_TrabajadorCategoriaPlanillaID
		WHERE trab.B_Eliminado = 0 AND 
			adm.B_Habilitado = 1 AND adm.B_Eliminado = 0 AND 
			tca.B_Habilitado = 1 AND tca.B_Eliminado = 0 AND tca.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND
			NOT EXISTS(SELECT pl.I_PlanillaID FROM dbo.TR_Planilla pl 
				INNER JOIN dbo.TR_TrabajadorPlanilla tpl ON tpl.I_PlanillaID = pl.I_PlanillaID 
				WHERE pl.B_Anulado = 0 AND tpl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND 
					tpl.I_TrabajadorCategoriaPlanillaID = tca.I_TrabajadorCategoriaPlanillaID);
	END

	IF (@I_CategoriaPlanillaID = @I_DOCENTEID) BEGIN
		INSERT #tmp_trabajador(I_TrabajadorCategoriaPlanillaID, I_DependenciaID, I_VinculoID, I_Filtro1, I_Filtro2, B_CategoriaPrincipal, B_EsJefe, I_GrupoTrabajoID)
		SELECT tca.I_TrabajadorCategoriaPlanillaID, tca.I_DependenciaID, trab.I_VinculoID, doc.I_CategoriaDocenteID, doc.I_HorasDocenteID,
			tca.B_CategoriaPrincipal, tca.B_EsJefe, tca.I_GrupoTrabajoID
		FROM dbo.TC_Docente doc
		INNER JOIN dbo.TC_Trabajador trab ON trab.I_TrabajadorID = doc.I_TrabajadorID
		INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla tca ON tca.I_TrabajadorID = trab.I_TrabajadorID 
		INNER JOIN dbo.TC_CategoriaDocente cd ON cd.I_CategoriaDocenteID = doc.I_CategoriaDocenteID
		INNER JOIN dbo.TC_HorasDocente hd ON hd.I_HorasDocenteID = doc.I_HorasDocenteID
		INNER JOIN @Tbl_Trabajador tmp ON tmp.I_ID = tca.I_TrabajadorCategoriaPlanillaID
		WHERE trab.B_Eliminado = 0 AND 
			doc.B_Habilitado = 1 AND doc.B_Eliminado = 0 AND 
			tca.B_Habilitado = 1 AND tca.B_Eliminado = 0 AND tca.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND
			NOT EXISTS(SELECT pl.I_PlanillaID FROM dbo.TR_Planilla pl 
				INNER JOIN dbo.TR_TrabajadorPlanilla tpl ON tpl.I_PlanillaID = pl.I_PlanillaID 
				WHERE pl.B_Anulado = 0 AND tpl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND 
					tpl.I_TrabajadorCategoriaPlanillaID = tca.I_TrabajadorCategoriaPlanillaID);
	END

	IF (@I_CategoriaPlanillaID NOT IN (@I_ADMINISTRATIVOID, @I_DOCENTEID)) BEGIN
		INSERT #tmp_trabajador(I_TrabajadorCategoriaPlanillaID, I_DependenciaID, I_VinculoID, B_CategoriaPrincipal, B_EsJefe, I_GrupoTrabajoID)
		SELECT tca.I_TrabajadorCategoriaPlanillaID, tca.I_DependenciaID, trab.I_VinculoID,
			tca.B_CategoriaPrincipal, tca.B_EsJefe, tca.I_GrupoTrabajoID
		FROM dbo.TC_Trabajador trab
		INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla tca ON tca.I_TrabajadorID = trab.I_TrabajadorID 
		INNER JOIN @Tbl_Trabajador tmp ON tmp.I_ID = tca.I_TrabajadorCategoriaPlanillaID
		WHERE trab.B_Eliminado = 0 AND 
			tca.B_Habilitado = 1 AND tca.B_Eliminado = 0 AND tca.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND
			NOT EXISTS(SELECT pl.I_PlanillaID FROM dbo.TR_Planilla pl 
				INNER JOIN dbo.TR_TrabajadorPlanilla tpl ON tpl.I_PlanillaID = pl.I_PlanillaID 
				WHERE pl.B_Anulado = 0 AND tpl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND 
					tpl.I_TrabajadorCategoriaPlanillaID = tca.I_TrabajadorCategoriaPlanillaID);
	END

	SET @I_Indicador = 1;

	SET @I_CantRegistros = (SELECT COUNT(*) FROM #tmp_trabajador);

	IF (@I_CantRegistros = 0) BEGIN
		SET @B_Result = 0
		SET @T_Message = 'No existen trabajadores para generar una planilla.'
		RETURN 0;
	END

	BEGIN TRY
	BEGIN TRAN
		--3. Crear la cabecera de la planilla
		INSERT dbo.TR_Planilla(I_PeriodoID, I_CategoriaPlanillaID, I_Correlativo, I_CantRegistros, 
			I_TotalRemuneracion, I_TotalReintegro, I_TotalDeduccion, I_TotalBruto, I_TotalDescuento, I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
		VALUES(@I_PeriodoID, @I_CategoriaPlanillaID, @I_CorrelativoPlanilla, 0, 0, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

		SET @I_PlantillaID = SCOPE_IDENTITY();

		--4. Recorrer cada trabajador
		WHILE (@I_Indicador <= @I_CantRegistros) BEGIN
		
			SELECT	@I_TrabajadorCategoriaPlanillaID = tmp.I_TrabajadorCategoriaPlanillaID,
					@B_CategoriaPrincipal = tmp.B_CategoriaPrincipal,
					@B_EsJefe = tmp.B_EsJefe,
					@I_DependenciaID = tmp.I_DependenciaID,
					@I_GrupoTrabajoID = tmp.I_GrupoTrabajoID,
					@I_VinculoID = tmp.I_VinculoID,
					@I_Filtro1 = tmp.I_Filtro1,
					@I_Filtro2 = tmp.I_Filtro2,
					@I_TrabajadorPlanillaID = 0
			FROM #tmp_trabajador tmp
			WHERE tmp.I_NroOrden = @I_Indicador;

			--5. Obtener REMUNERACIÓN
			SET @I_TotalRemuneracionTrabajador = 0;

			DELETE @tmp_remuneracion;
			
			WITH tmp_remuneracion(I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, 
				B_IncluirEnTotalBruto, B_EsAdicion, B_EsValorFijo)
			AS
			(
				SELECT DISTINCT pp.I_PlantillaPlanillaID, c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, c.T_ConceptoAbrv, 
					tc.B_IncluirEnTotalBruto, tc.B_EsAdicion, ppc.B_EsValorFijo
				FROM dbo.TI_PlantillaPlanilla pp
				INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
				INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
				INNER JOIN dbo.TC_TipoConcepto tc ON tc.I_TipoConceptoID = c.I_TipoConceptoID
				WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
					pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Remunerativo
			)
			INSERT @tmp_remuneracion(I_NroOrden, I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv)
			SELECT 
				ROW_NUMBER() OVER(ORDER BY B_EsValorFijo DESC, B_IncluirEnTotalBruto DESC, B_EsAdicion DESC), 
				I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv
			FROM tmp_remuneracion;

			SET @I_NroOrden = 1;

			SET @I_CantConceptos = (SELECT COUNT(*) FROM @tmp_remuneracion);

			WHILE (@I_NroOrden <= @I_CantConceptos)
			BEGIN
				SELECT	@I_PlantillaPlanillaID = I_PlantillaPlanillaID,
						@I_ConceptoID = I_ConceptoID,
						@C_ConceptoCod = C_ConceptoCod,
						@T_ConceptoDesc = T_ConceptoDesc,
						@T_ConceptoAbrv = T_ConceptoAbrv,
						@B_ConceptoObtenido = 0
				FROM @tmp_remuneracion
				WHERE I_NroOrden = @I_NroOrden;

				EXEC dbo.USP_S_ObtenerParametros_PlantillaPlanilla
					@I_PlantillaPlanillaID = @I_PlantillaPlanillaID,
					@I_ConceptoID = @I_ConceptoID,
					@I_Filtro1 = @I_Filtro1,
					@I_Filtro2 = @I_Filtro2,
					@I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID OUTPUT,
					@B_EsValorFijo = @B_EsValorFijo OUTPUT,
					@B_ValorEsExterno = @B_ValorEsExterno OUTPUT,
					@M_ValorConcepto = @M_ValorConcepto OUTPUT,
					@B_ConceptoObtenido = @B_ConceptoObtenido OUTPUT;

				IF (@B_ConceptoObtenido = 1) BEGIN
					IF (@B_ValorEsExterno = 1) BEGIN
						SET @M_ValorConcepto = ISNULL((SELECT conc.M_ValorConcepto FROM dbo.TI_ValorExternoPeriodo per
							INNER JOIN dbo.TI_ValorExternoConcepto conc ON conc.I_ValorExternoPeriodoID = per.I_ValorExternoPeriodoID 
							WHERE per.B_Eliminado = 0 AND conc.B_Eliminado = 0 AND
								per.I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID AND per.I_PeriodoID = @I_PeriodoID AND conc.I_ConceptoID = @I_ConceptoID), 0);
					END

					IF (@M_ValorConcepto IS NOT NULL AND @M_ValorConcepto > 0) BEGIN
						IF (@B_EsValorFijo = 0 AND @I_TrabajadorPlanillaID > 0) BEGIN
							SET @M_SumConceptos = (SELECT SUM(p.M_Monto) FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia r
								INNER JOIN dbo.TR_Concepto_TrabajadorPlanilla p ON p.I_ConceptoID = r.I_ConceptoReferenciaID
								WHERE	r.B_Eliminado = 0 AND r.I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID AND
										p.B_Anulado = 0 AND p.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID);

							SET @M_ValorConcepto = (@M_ValorConcepto/100) * @M_SumConceptos;
						END
						
						IF (@I_TrabajadorPlanillaID = 0) BEGIN
							INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorCategoriaPlanillaID, B_CategoriaPrincipal, B_EsJefe, I_DependenciaID, I_GrupoTrabajoID, I_VinculoID, 
								I_TotalRemuneracion, I_TotalReintegro, I_TotalDeduccion, I_TotalBruto, I_TotalDescuento,
								I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
							VALUES(@I_PlantillaID, @I_TrabajadorCategoriaPlanillaID, @B_CategoriaPrincipal, @B_EsJefe, @I_DependenciaID, @I_GrupoTrabajoID, @I_VinculoID, 0, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

							SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();
						END
					
						INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, M_Monto, I_Orden, B_Anulado, I_UsuarioCre, D_FecCre)
						VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, @M_ValorConcepto, 1, 0, @I_UserID, @D_FecRegistro);
					
						SET @I_TotalRemuneracionTrabajador = @I_TotalRemuneracionTrabajador + @M_ValorConcepto;
					END
				END

				SET @I_NroOrden = @I_NroOrden + 1;
			END

			--6. Obtener DESCUENTOS
			SET @I_TotalDescuentoTrabajador = 0;

			DELETE @tmp_descuento;

			WITH tmp_descuento(I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, 
				B_IncluirEnTotalBruto, B_EsAdicion, B_EsValorFijo)
			AS
			(
				SELECT DISTINCT pp.I_PlantillaPlanillaID, c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, c.T_ConceptoAbrv,
					tc.B_IncluirEnTotalBruto, tc.B_EsAdicion, ppc.B_EsValorFijo
				FROM dbo.TI_PlantillaPlanilla pp
				INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
				INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
				INNER JOIN dbo.TC_TipoConcepto tc ON tc.I_TipoConceptoID = c.I_TipoConceptoID
				WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
					pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Descuento
			)
			INSERT @tmp_descuento(I_NroOrden, I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv)
			SELECT ROW_NUMBER() OVER(ORDER BY B_EsValorFijo DESC, B_IncluirEnTotalBruto DESC, B_EsAdicion DESC),
				I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv
			FROM tmp_descuento;

			SET @I_NroOrden = 1;

			SET @I_CantConceptos = (SELECT COUNT(*) FROM @tmp_descuento);

			WHILE (@I_NroOrden <= @I_CantConceptos)
			BEGIN
				SELECT	@I_PlantillaPlanillaID = I_PlantillaPlanillaID,
						@I_ConceptoID = I_ConceptoID,
						@C_ConceptoCod = C_ConceptoCod,
						@T_ConceptoDesc = T_ConceptoDesc,
						@T_ConceptoAbrv = T_ConceptoAbrv,
						@B_ConceptoObtenido = 0
				FROM @tmp_descuento
				WHERE I_NroOrden = @I_NroOrden;

				EXEC dbo.USP_S_ObtenerParametros_PlantillaPlanilla
					@I_PlantillaPlanillaID = @I_PlantillaPlanillaID,
					@I_ConceptoID = @I_ConceptoID,
					@I_Filtro1 = @I_Filtro1,
					@I_Filtro2 = @I_Filtro2,
					@I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID OUTPUT,
					@B_EsValorFijo = @B_EsValorFijo OUTPUT,
					@B_ValorEsExterno = @B_ValorEsExterno OUTPUT,
					@M_ValorConcepto = @M_ValorConcepto OUTPUT,
					@B_ConceptoObtenido = @B_ConceptoObtenido OUTPUT;

				IF (@B_ConceptoObtenido = 1) BEGIN
					IF (@B_ValorEsExterno = 1) BEGIN
						SET @M_ValorConcepto = ISNULL((SELECT conc.M_ValorConcepto FROM dbo.TI_ValorExternoPeriodo per
							INNER JOIN dbo.TI_ValorExternoConcepto conc ON conc.I_ValorExternoPeriodoID = per.I_ValorExternoPeriodoID 
							WHERE per.B_Eliminado = 0 AND conc.B_Eliminado = 0 AND
								per.I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID AND per.I_PeriodoID = @I_PeriodoID AND conc.I_ConceptoID = @I_ConceptoID), 0);
					END

					IF (@M_ValorConcepto IS NOT NULL AND @M_ValorConcepto > 0) BEGIN
						IF (@B_EsValorFijo = 0 AND @I_TrabajadorPlanillaID > 0) BEGIN
							SET @M_SumConceptos = (SELECT SUM(p.M_Monto) FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia r
								INNER JOIN dbo.TR_Concepto_TrabajadorPlanilla p ON p.I_ConceptoID = r.I_ConceptoReferenciaID
								WHERE	r.B_Eliminado = 0 AND r.I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID AND
										p.B_Anulado = 0 AND p.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID);

							SET @M_ValorConcepto = (@M_ValorConcepto/100) * @M_SumConceptos;
						END

						IF (@I_TrabajadorPlanillaID = 0) BEGIN
							INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorCategoriaPlanillaID, B_CategoriaPrincipal, B_EsJefe, I_DependenciaID, I_GrupoTrabajoID, I_VinculoID, 
								I_TotalRemuneracion, I_TotalReintegro, I_TotalDeduccion, I_TotalBruto, I_TotalDescuento,
								I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
							VALUES(@I_PlantillaID, @I_TrabajadorCategoriaPlanillaID, @B_CategoriaPrincipal, @B_EsJefe, @I_DependenciaID, @I_GrupoTrabajoID, @I_VinculoID, 0, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

							SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();
						END

						INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, M_Monto, I_Orden, B_Anulado, I_UsuarioCre, D_FecCre)
						VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, @M_ValorConcepto, 4, 0, @I_UserID, @D_FecRegistro);

						SET @I_TotalDescuentoTrabajador = @I_TotalDescuentoTrabajador + @M_ValorConcepto;
					END
				END

				SET @I_NroOrden = @I_NroOrden + 1;
			END

			--7. Obtener REINTEGRO
			SET @I_TotalReintegroTrabajador = 0;

			DELETE @tmp_reintegro;

			WITH tmp_reintegro(I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, 
				B_IncluirEnTotalBruto, B_EsAdicion, B_EsValorFijo)
			AS
			(
				SELECT DISTINCT pp.I_PlantillaPlanillaID, c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, c.T_ConceptoAbrv, 
					tc.B_IncluirEnTotalBruto, tc.B_EsAdicion, ppc.B_EsValorFijo
				FROM dbo.TI_PlantillaPlanilla pp
				INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
				INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
				INNER JOIN dbo.TC_TipoConcepto tc ON tc.I_TipoConceptoID = c.I_TipoConceptoID
				WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
					pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Reintegro
			)
			INSERT @tmp_reintegro(I_NroOrden, I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv)
			SELECT ROW_NUMBER() OVER(ORDER BY B_EsValorFijo DESC, B_IncluirEnTotalBruto DESC, B_EsAdicion DESC), 
				I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv
			FROM tmp_reintegro;

			SET @I_NroOrden = 1;

			SET @I_CantConceptos = (SELECT COUNT(*) FROM @tmp_reintegro);

			WHILE (@I_NroOrden <= @I_CantConceptos)
			BEGIN
				SELECT	@I_PlantillaPlanillaID = I_PlantillaPlanillaID,
						@I_ConceptoID = I_ConceptoID,
						@C_ConceptoCod = C_ConceptoCod,
						@T_ConceptoDesc = T_ConceptoDesc,
						@T_ConceptoAbrv = T_ConceptoAbrv,
						@B_ConceptoObtenido = 0
				FROM @tmp_reintegro
				WHERE I_NroOrden = @I_NroOrden;

				EXEC dbo.USP_S_ObtenerParametros_PlantillaPlanilla
					@I_PlantillaPlanillaID = @I_PlantillaPlanillaID,
					@I_ConceptoID = @I_ConceptoID,
					@I_Filtro1 = @I_Filtro1,
					@I_Filtro2 = @I_Filtro2,
					@I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID OUTPUT,
					@B_EsValorFijo = @B_EsValorFijo OUTPUT,
					@B_ValorEsExterno = @B_ValorEsExterno OUTPUT,
					@M_ValorConcepto = @M_ValorConcepto OUTPUT,
					@B_ConceptoObtenido = @B_ConceptoObtenido OUTPUT;

				IF (@B_ConceptoObtenido = 1) BEGIN
					IF (@B_ValorEsExterno = 1) BEGIN
						SET @M_ValorConcepto = ISNULL((SELECT conc.M_ValorConcepto FROM dbo.TI_ValorExternoPeriodo per
							INNER JOIN dbo.TI_ValorExternoConcepto conc ON conc.I_ValorExternoPeriodoID = per.I_ValorExternoPeriodoID 
							WHERE per.B_Eliminado = 0 AND conc.B_Eliminado = 0 AND
								per.I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID AND per.I_PeriodoID = @I_PeriodoID AND conc.I_ConceptoID = @I_ConceptoID), 0);
					END

					IF (@M_ValorConcepto IS NOT NULL AND @M_ValorConcepto > 0) BEGIN
						IF (@B_EsValorFijo = 0 AND @I_TrabajadorPlanillaID > 0) BEGIN
							SET @M_SumConceptos = (SELECT SUM(p.M_Monto) FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia r
								INNER JOIN dbo.TR_Concepto_TrabajadorPlanilla p ON p.I_ConceptoID = r.I_ConceptoReferenciaID
								WHERE	r.B_Eliminado = 0 AND r.I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID AND
										p.B_Anulado = 0 AND p.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID);

							SET @M_ValorConcepto = (@M_ValorConcepto/100) * @M_SumConceptos;
						END

						IF (@I_TrabajadorPlanillaID = 0) BEGIN
							INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorCategoriaPlanillaID, B_CategoriaPrincipal, B_EsJefe, I_DependenciaID, I_GrupoTrabajoID, I_VinculoID, 
								I_TotalRemuneracion, I_TotalReintegro, I_TotalDeduccion, I_TotalBruto, I_TotalDescuento,
								I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
							VALUES(@I_PlantillaID, @I_TrabajadorCategoriaPlanillaID, @B_CategoriaPrincipal, @B_EsJefe, @I_DependenciaID, @I_GrupoTrabajoID, @I_VinculoID, 0, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

							SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();
						END

						INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, M_Monto, I_Orden, B_Anulado, I_UsuarioCre, D_FecCre)
						VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, @M_ValorConcepto, 2, 0, @I_UserID, @D_FecRegistro);

						SET @I_TotalReintegroTrabajador = @I_TotalReintegroTrabajador + @M_ValorConcepto;
					END
				END

				SET @I_NroOrden = @I_NroOrden + 1;
			END

			--8. Obtener DEDUCCIONES
			SET @I_TotalDeduccionTrabajador = 0;

			DELETE @tmp_deduccion;

			WITH tmp_deduccion(I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, 
				B_IncluirEnTotalBruto, B_EsAdicion, B_EsValorFijo)
			AS
			(
				SELECT DISTINCT pp.I_PlantillaPlanillaID, c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, c.T_ConceptoAbrv, 
					tc.B_IncluirEnTotalBruto, tc.B_EsAdicion, ppc.B_EsValorFijo
				FROM dbo.TI_PlantillaPlanilla pp
				INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
				INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
				INNER JOIN dbo.TC_TipoConcepto tc ON tc.I_TipoConceptoID = c.I_TipoConceptoID
				WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
					pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Deduccion
			)
			INSERT @tmp_deduccion(I_NroOrden, I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv)
			SELECT ROW_NUMBER() OVER(ORDER BY B_EsValorFijo DESC, B_IncluirEnTotalBruto DESC, B_EsAdicion DESC), 
				I_PlantillaPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv
			FROM tmp_deduccion;

			SET @I_NroOrden = 1;

			SET @I_CantConceptos = (SELECT COUNT(*) FROM @tmp_deduccion);

			WHILE (@I_NroOrden <= @I_CantConceptos)
			BEGIN
				SELECT	@I_PlantillaPlanillaID = I_PlantillaPlanillaID,
						@I_ConceptoID = I_ConceptoID,
						@C_ConceptoCod = C_ConceptoCod,
						@T_ConceptoDesc = T_ConceptoDesc,
						@T_ConceptoAbrv = T_ConceptoAbrv,
						@B_ConceptoObtenido = 0
				FROM @tmp_deduccion
				WHERE I_NroOrden = @I_NroOrden;

				EXEC dbo.USP_S_ObtenerParametros_PlantillaPlanilla
					@I_PlantillaPlanillaID = @I_PlantillaPlanillaID,
					@I_ConceptoID = @I_ConceptoID,
					@I_Filtro1 = @I_Filtro1,
					@I_Filtro2 = @I_Filtro2,
					@I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID OUTPUT,
					@B_EsValorFijo = @B_EsValorFijo OUTPUT,
					@B_ValorEsExterno = @B_ValorEsExterno OUTPUT,
					@M_ValorConcepto = @M_ValorConcepto OUTPUT,
					@B_ConceptoObtenido = @B_ConceptoObtenido OUTPUT;

				IF (@B_ConceptoObtenido = 1) BEGIN
					IF (@B_ValorEsExterno = 1) BEGIN
						SET @M_ValorConcepto = ISNULL((SELECT conc.M_ValorConcepto FROM dbo.TI_ValorExternoPeriodo per
							INNER JOIN dbo.TI_ValorExternoConcepto conc ON conc.I_ValorExternoPeriodoID = per.I_ValorExternoPeriodoID 
							WHERE per.B_Eliminado = 0 AND conc.B_Eliminado = 0 AND
								per.I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID AND per.I_PeriodoID = @I_PeriodoID AND conc.I_ConceptoID = @I_ConceptoID), 0);
					END

					IF (@M_ValorConcepto IS NOT NULL AND @M_ValorConcepto > 0) BEGIN
						IF (@B_EsValorFijo = 0 AND @I_TrabajadorPlanillaID > 0) BEGIN
							SET @M_SumConceptos = (SELECT SUM(p.M_Monto) FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia r
								INNER JOIN dbo.TR_Concepto_TrabajadorPlanilla p ON p.I_ConceptoID = r.I_ConceptoReferenciaID
								WHERE	r.B_Eliminado = 0 AND r.I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID AND
										p.B_Anulado = 0 AND p.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID);

							SET @M_ValorConcepto = (@M_ValorConcepto/100) * @M_SumConceptos;
						END

						IF (@I_TrabajadorPlanillaID = 0) BEGIN
							INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorCategoriaPlanillaID, B_CategoriaPrincipal, B_EsJefe, I_DependenciaID, I_GrupoTrabajoID, I_VinculoID, 
								I_TotalRemuneracion, I_TotalReintegro, I_TotalDeduccion, I_TotalBruto, I_TotalDescuento,
								I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
							VALUES(@I_PlantillaID, @I_TrabajadorCategoriaPlanillaID, @B_CategoriaPrincipal, @B_EsJefe, @I_DependenciaID, @I_GrupoTrabajoID, @I_VinculoID, 0, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

							SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();
						END

						INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, M_Monto, I_Orden, B_Anulado, I_UsuarioCre, D_FecCre)
						VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, @M_ValorConcepto, 3, 0, @I_UserID, @D_FecRegistro);

						SET @I_TotalDeduccionTrabajador = @I_TotalDeduccionTrabajador + @M_ValorConcepto;
					END
				END

				SET @I_NroOrden = @I_NroOrden + 1;
			END

			IF (@I_TrabajadorPlanillaID > 0) BEGIN
				--9. Actualizar totales por trabajador
				SET @I_TotalBrutoTrabajador = @I_TotalRemuneracionTrabajador + @I_TotalReintegroTrabajador - @I_TotalDeduccionTrabajador;

				SET @I_TotalSueldoTrabajador = @I_TotalBrutoTrabajador - @I_TotalDescuentoTrabajador;

				IF (@I_TotalSueldoTrabajador > 0 ) BEGIN

					UPDATE  dbo.TR_TrabajadorPlanilla SET 
						I_TotalRemuneracion = @I_TotalRemuneracionTrabajador,
						I_TotalReintegro = @I_TotalReintegroTrabajador,
						I_TotalDeduccion = @I_TotalDeduccionTrabajador,
						I_TotalDescuento = @I_TotalDescuentoTrabajador,
						I_TotalBruto = @I_TotalBrutoTrabajador,
						I_TotalSueldo = @I_TotalSueldoTrabajador
					WHERE I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID;

					--10. Acumular para el resumen total de la planilla
					SET @I_TotalRemuneracion = @I_TotalRemuneracion + @I_TotalRemuneracionTrabajador;

					SET @I_TotalReintegro = @I_TotalReintegro + @I_TotalReintegroTrabajador;

					SET @I_TotalDeduccion = @I_TotalDeduccion + @I_TotalDeduccionTrabajador;

					SET @I_TotalBruto = @I_TotalBruto + @I_TotalBrutoTrabajador;

					SET @I_TotalDescuento = @I_TotalDescuento + @I_TotalDescuentoTrabajador;

					SET @I_TotalSueldo = @I_TotalSueldo + @I_TotalSueldoTrabajador;

					SET @I_CantPlanillasGeneradas = @I_CantPlanillasGeneradas + 1;

				END ELSE BEGIN
					UPDATE  dbo.TR_TrabajadorPlanilla SET 
						B_Anulado = 1,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecRegistro
					WHERE I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID;
				END
			END

			SET @I_Indicador = @I_Indicador + 1;
		END

		IF (@I_CantPlanillasGeneradas > 0) BEGIN

			--11. Actualizar totales de planilla
			UPDATE dbo.TR_Planilla SET 
				I_TotalRemuneracion = @I_TotalRemuneracion, 
				I_TotalReintegro = @I_TotalReintegro, 
				I_TotalDeduccion = @I_TotalDeduccion,
				I_TotalBruto = @I_TotalBruto,
				I_TotalDescuento = @I_TotalDescuento,
				I_TotalSueldo = @I_TotalSueldo,
				I_CantRegistros = @I_CantPlanillasGeneradas
			WHERE I_PlanillaID = @I_PlantillaID;

			COMMIT TRAN
			
			SET @B_Result = 1;
			SET @T_Message = 'Planilla generada correctamente.';

		END ELSE BEGIN
			ROLLBACK TRAN

			SET @B_Result = 0;
			SET @T_Message = 'La generación de planilla no generó ningún registro, por lo que se canceló el proceso.';
		END
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarConcepto')
	DROP PROCEDURE [dbo].[USP_I_RegistrarConcepto]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarConcepto]
@I_TipoConceptoID INT,
@C_ConceptoCod VARCHAR(20),
@T_ConceptoDesc VARCHAR(250),
@T_ConceptoAbrv VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		INSERT dbo.TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_TipoConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, 1, 0, @I_UserID, GETDATE())

		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Registro correcto.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarConcepto')
	DROP PROCEDURE [dbo].[USP_U_ActualizarConcepto]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarConcepto]
@I_ConceptoID INT,
@I_TipoConceptoID INT,
@C_ConceptoCod VARCHAR(20),
@T_ConceptoDesc VARCHAR(250),
@T_ConceptoAbrv VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TC_Concepto SET
			I_TipoConceptoID = @I_TipoConceptoID,
			C_ConceptoCod = @C_ConceptoCod,
			T_ConceptoDesc = @T_ConceptoDesc,
			T_ConceptoAbrv = @T_ConceptoAbrv,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_ConceptoID = @I_ConceptoID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoConcepto')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoConcepto]
GO

CREATE PROCEDURE [dbo].[USP_U_CambiarEstadoConcepto]
@I_ConceptoID INT,
@B_Habilitado BIT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TC_Concepto SET
			B_Habilitado = @B_Habilitado,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_ConceptoID = @I_ConceptoID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarPlantillaPlanilla')
	DROP PROCEDURE [dbo].[USP_I_RegistrarPlantillaPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarPlantillaPlanilla]
@I_CategoriaPlanillaID INT,
@T_PlantillaPlanillaDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		INSERT dbo.TI_PlantillaPlanilla(I_CategoriaPlanillaID, T_PlantillaPlanillaDesc, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_CategoriaPlanillaID, @T_PlantillaPlanillaDesc, 1, 0, @I_UserID, GETDATE());

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarPlantillaPlanilla')
	DROP PROCEDURE [dbo].[USP_U_ActualizarPlantillaPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarPlantillaPlanilla]
@I_PlantillaPlanillaID INT,
@I_CategoriaPlanillaID INT,
@T_PlantillaPlanillaDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TI_PlantillaPlanilla SET
			I_CategoriaPlanillaID = @I_CategoriaPlanillaID,
			T_PlantillaPlanillaDesc = @T_PlantillaPlanillaDesc,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_PlantillaPlanillaID = @I_PlantillaPlanillaID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoPlantillaPlanilla')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoPlantillaPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_U_CambiarEstadoPlantillaPlanilla]
@I_PlantillaPlanillaID INT,
@B_Habilitado BIT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TI_PlantillaPlanilla SET
			B_Habilitado = @B_Habilitado,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_PlantillaPlanillaID = @I_PlantillaPlanillaID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarPlantillaPlanillaConcepto')
	DROP PROCEDURE [dbo].[USP_I_RegistrarPlantillaPlanillaConcepto]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarPlantillaPlanillaConcepto]
@Tbl_ConceptoReferencia [dbo].[type_dataIdentifiers] READONLY,
@I_PlantillaPlanillaID INT,
@I_ConceptoID INT,
@B_EsValorFijo BIT,
@B_ValorEsExterno BIT,
@M_ValorConcepto DECIMAL(15,2) = NULL,
@B_AplicarFiltro1 BIT,
@I_Filtro1 INT = NULL,
@B_AplicarFiltro2 BIT,
@I_Filtro2 INT = NULL,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @I_PlantillaPlanillaConceptoID INT
	DECLARE @D_FecCre DATETIME = GETDATE()

	BEGIN TRAN
	BEGIN TRY
		INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsValorFijo, B_ValorEsExterno, M_ValorConcepto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_PlantillaPlanillaID, @I_ConceptoID, @B_EsValorFijo, @B_ValorEsExterno, @M_ValorConcepto, @B_AplicarFiltro1, @I_Filtro1, @B_AplicarFiltro2, @I_Filtro2, 1, 0, @I_UserID, @D_FecCre)

		SET @I_PlantillaPlanillaConceptoID = SCOPE_IDENTITY();
		
		INSERT dbo.TI_PlantillaPlanilla_Concepto_Referencia(I_PlantillaPlanillaConceptoID, I_ConceptoReferenciaID, B_Eliminado, I_UsuarioCre, D_FecCre)
		SELECT @I_PlantillaPlanillaConceptoID, I_ID, 0, @I_UserID, @D_FecCre FROM @Tbl_ConceptoReferencia

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarPlantillaPlanillaConcepto')
	DROP PROCEDURE [dbo].[USP_U_ActualizarPlantillaPlanillaConcepto]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarPlantillaPlanillaConcepto]
@Tbl_ConceptoReferencia [dbo].[type_dataIdentifiers] READONLY,
@I_PlantillaPlanillaConceptoID INT,
@I_PlantillaPlanillaID INT,
@I_ConceptoID INT,
@B_EsValorFijo BIT,
@B_ValorEsExterno BIT,
@M_ValorConcepto DECIMAL(15,2) = NULL,
@B_AplicarFiltro1 BIT,
@I_Filtro1 INT = NULL,
@B_AplicarFiltro2 BIT,
@I_Filtro2 INT = NULL,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @D_FechaActual DATETIME = GETDATE();

	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TI_PlantillaPlanilla_Concepto SET
			I_PlantillaPlanillaID = @I_PlantillaPlanillaID,
			I_ConceptoID = @I_ConceptoID,
			B_EsValorFijo = @B_EsValorFijo,
			B_ValorEsExterno = @B_ValorEsExterno,
			M_ValorConcepto = @M_ValorConcepto,
			B_AplicarFiltro1 = @B_AplicarFiltro1,
			I_Filtro1 = @I_Filtro1,
			B_AplicarFiltro2 = @B_AplicarFiltro2,
			I_Filtro2 = @I_Filtro2,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FechaActual
		WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID;

		UPDATE c SET
			B_Eliminado = 1, 
			I_UsuarioMod = @I_UserID, 
			D_FecMod = @D_FechaActual
		FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia c
		LEFT JOIN @Tbl_ConceptoReferencia r ON r.I_ID = c.I_ConceptoReferenciaID
		WHERE 
			c.I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID AND 
			c.B_Eliminado = 0 AND 
			r.I_ID IS NULL;

		INSERT dbo.TI_PlantillaPlanilla_Concepto_Referencia(I_PlantillaPlanillaConceptoID, I_ConceptoReferenciaID, B_Eliminado, I_UsuarioCre, D_FecCre)
		SELECT @I_PlantillaPlanillaConceptoID, I_ID, 0, @I_UserID, @D_FechaActual 
		FROM @Tbl_ConceptoReferencia r
		WHERE NOT EXISTS(SELECT c.I_ID FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia c 
			WHERE c.B_Eliminado = 0 AND c.I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID AND c.I_ConceptoReferenciaID = r.I_ID);

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoPlantillaPlanillaConcepto')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoPlantillaPlanillaConcepto]
GO

CREATE PROCEDURE [dbo].[USP_U_CambiarEstadoPlantillaPlanillaConcepto]
@I_PlantillaPlanillaConceptoID INT,
@B_Habilitado BIT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TI_PlantillaPlanilla_Concepto SET
			B_Habilitado = @B_Habilitado,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_EliminarPlantillaPlanillaConcepto')
	DROP PROCEDURE [dbo].[USP_U_EliminarPlantillaPlanillaConcepto]
GO

CREATE PROCEDURE [dbo].[USP_U_EliminarPlantillaPlanillaConcepto]
@I_PlantillaPlanillaConceptoID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TI_PlantillaPlanilla_Concepto_Referencia SET
			B_Eliminado = 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID;

		UPDATE dbo.TI_PlantillaPlanilla_Concepto SET
			B_Habilitado = 0,
			B_Eliminado = 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID;
		
		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.DOMAINS WHERE DOMAIN_NAME = 'type_dataValorExterno') BEGIN
	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_IU_GrabarValorExterno') BEGIN
		DROP PROCEDURE [dbo].[USP_IU_GrabarValorExterno]
	END

	DROP TYPE [dbo].[type_dataValorExterno]
END
GO

CREATE TYPE [dbo].[type_dataValorExterno] AS TABLE(
	I_ID INT NOT NULL,
	I_PeriodoID INT NOT NULL,
	I_TrabajadorCategoriaPlanillaID INT NOT NULL,
	I_ConceptoID INT NOT NULL,
	M_ValorConcepto DECIMAL(15,2) NOT NULL,
	I_ProveedorID INT NOT NULL
)
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_IU_GrabarValorExterno')
	DROP PROCEDURE [dbo].[USP_IU_GrabarValorExterno]
GO

CREATE PROCEDURE [dbo].[USP_IU_GrabarValorExterno]
@Tbl_ValorExterno [dbo].[type_dataValorExterno] READONLY,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @I_Indicador INT,
			@I_CantRegistros INT,
			@I_PeriodoID INT,
			@I_TrabajadorCategoriaPlanillaID INT,
			@I_ConceptoID INT,
			@M_ValorConcepto DECIMAL(15,2),
			@I_ProveedorID INT,
			@D_FecActual DATETIME,
			@I_ValorExternoPeriodoID INT

	SET @I_Indicador = 1;

	SET @I_CantRegistros = (SELECT COUNT(*) FROM @Tbl_ValorExterno);

	SET @D_FecActual = GETDATE();

	BEGIN TRAN
	BEGIN TRY
		WHILE (@I_Indicador <= @I_CantRegistros) BEGIN

			SELECT	@I_PeriodoID = I_PeriodoID,
					@I_TrabajadorCategoriaPlanillaID = I_TrabajadorCategoriaPlanillaID,
					@I_ConceptoID = I_ConceptoID,
					@M_ValorConcepto = M_ValorConcepto,
					@I_ProveedorID = I_ProveedorID
			FROM @Tbl_ValorExterno
			WHERE I_ID = @I_Indicador;
			
			IF NOT EXISTS(SELECT I_ValorExternoPeriodoID FROM dbo.TI_ValorExternoPeriodo WHERE I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID AND I_PeriodoID = @I_PeriodoID AND B_Eliminado = 0)
			BEGIN
				INSERT dbo.TI_ValorExternoPeriodo(I_TrabajadorCategoriaPlanillaID, I_PeriodoID, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorCategoriaPlanillaID, @I_PeriodoID, 0, @I_UserID, @D_FecActual);

				SET @I_ValorExternoPeriodoID = SCOPE_IDENTITY();
			END ELSE
			BEGIN
				SET @I_ValorExternoPeriodoID = (SELECT I_ValorExternoPeriodoID FROM dbo.TI_ValorExternoPeriodo 
					WHERE I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID AND I_PeriodoID = @I_PeriodoID AND B_Eliminado = 0);
			END

			IF NOT EXISTS(SELECT I_ConceptoExternoValorID FROM dbo.TI_ValorExternoConcepto 
				WHERE I_ValorExternoPeriodoID = @I_ValorExternoPeriodoID AND I_ConceptoID = @I_ConceptoID AND B_Eliminado = 0) 
			BEGIN
				INSERT dbo.TI_ValorExternoConcepto(I_ValorExternoPeriodoID, I_ConceptoID, M_ValorConcepto, I_ProveedorID, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_ValorExternoPeriodoID, @I_ConceptoID, @M_ValorConcepto, @I_ProveedorID, 0, @I_UserID, @D_FecActual);
			END ELSE
			BEGIN
				UPDATE dbo.TI_ValorExternoConcepto SET 
					M_ValorConcepto = @M_ValorConcepto,
					I_ProveedorID = @I_ProveedorID,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecActual
				WHERE I_ValorExternoPeriodoID = @I_ValorExternoPeriodoID AND I_ConceptoID = @I_ConceptoID AND B_Eliminado = 0
			END

			SET @I_Indicador = @I_Indicador + 1;
		END

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Grabación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarValorExterno')
	DROP PROCEDURE [dbo].[USP_U_ActualizarValorExterno]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarValorExterno]
@I_ConceptoExternoValorID INT,
@M_ValorConcepto DECIMAL(15,2),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TI_ValorExternoConcepto SET
			M_ValorConcepto = @M_ValorConcepto,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_ConceptoExternoValorID = @I_ConceptoExternoValorID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualziación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_EliminarValorExternoConcepto')
	DROP PROCEDURE [dbo].[USP_U_EliminarValorExternoConcepto]
GO

CREATE PROCEDURE [dbo].[USP_U_EliminarValorExternoConcepto]
@I_ConceptoExternoValorID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TI_ValorExternoConcepto SET
			B_Eliminado = 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_ConceptoExternoValorID = @I_ConceptoExternoValorID;
		
		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarDatosUsuario')
	DROP PROCEDURE [dbo].[USP_I_RegistrarDatosUsuario]
GO
  
CREATE PROCEDURE [dbo].[USP_I_RegistrarDatosUsuario] 
@UserId INT,
@N_NumDoc VARCHAR(150),
@T_NomPersona VARCHAR(250),
@T_CorreoUsuario VARCHAR(100),
@CurrentUserId INT,
@D_FecRegistro DATETIME,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(4000) OUTPUT
AS  
BEGIN  
	SET NOCOUNT ON;
  
	BEGIN TRANSACTION  
	BEGIN TRY  
		DECLARE @I_DatosUsuarioID INT
				
		INSERT INTO TC_DatosUsuario(N_NumDoc, T_NomPersona, T_CorreoUsuario, I_UsuarioCre, D_FecCre)
		VALUES(@N_NumDoc, @T_NomPersona, @T_CorreoUsuario, @CurrentUserId, @D_FecRegistro);
  
		SET @I_DatosUsuarioID = SCOPE_IDENTITY();
  
		INSERT INTO TI_UsuarioDatosUsuario(UserId, I_DatosUsuarioID, D_FecAlta)
		VALUES(@UserId, @I_DatosUsuarioID, @D_FecRegistro);
  
		COMMIT TRANSACTION
		SET @B_Result = 1;
		SET @T_Message = 'El usuario se registró con éxito.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarDatosUsuario')
    DROP PROCEDURE [dbo].[USP_U_ActualizarDatosUsuario]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarDatosUsuario]
@UserId INT,
@I_DatosUsuarioID INT,
@N_NumDoc VARCHAR(15),
@T_NomPersona VARCHAR(250),
@T_CorreoUsuario VARCHAR(100),
@I_DependenciaID INT,
@CurrentUserId  INT,
@D_FecMod DATETIME,
@B_Result BIT OUTPUT,
@T_Message NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION
    BEGIN TRY
    
		UPDATE  TC_Usuario SET
			I_DependenciaID = @I_DependenciaID,
			I_UsuarioMod = @CurrentUserId,
			D_FecMod = @D_FecMod
		WHERE UserId = @UserId;

		UPDATE  TC_DatosUsuario SET
			N_NumDoc = @N_NumDoc,
			T_NomPersona = @T_NomPersona,
			T_CorreoUsuario = @T_CorreoUsuario,
			I_UsuarioMod = @CurrentUserId,
			D_FecMod = @D_FecMod
		WHERE I_DatosUsuarioID = @I_DatosUsuarioID;
   
		COMMIT TRANSACTION
		SET @B_Result = 1;
		SET @T_Message = 'La operación se realizó con éxito.';
    END TRY
    BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
    END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoUsuario')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoUsuario]
GO
  
CREATE PROCEDURE [dbo].[USP_U_CambiarEstadoUsuario]
@UserId INT,
@B_Habilitado BIT,
@CurrentUserId INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(4000) OUTPUT
AS  
BEGIN  
	SET NOCOUNT ON;  
	
	BEGIN TRANSACTION
	BEGIN TRY
		DECLARE @D_FecMod DATETIME = GETDATE();

		UPDATE TC_Usuario SET 
			B_Habilitado = @B_Habilitado,  
			I_UsuarioMod = @CurrentUserId,
			D_FecMod = @D_FecMod
		WHERE UserId = @UserId;

		IF (@B_Habilitado = 0)
		BEGIN
			UPDATE udu SET udu.D_FecBaja = @D_FecMod
			FROM dbo.TC_Usuario u
			INNER JOIN dbo.TI_UsuarioDatosUsuario udu ON udu.UserId = u.UserId
			WHERE u.UserId = @UserId;
		END
		ELSE BEGIN
			UPDATE udu SET udu.D_FecBaja = NULL
			FROM dbo.TC_Usuario u
			INNER JOIN dbo.TI_UsuarioDatosUsuario udu ON udu.UserId = u.UserId
			WHERE u.UserId = @UserId;
		END
		
		COMMIT TRANSACTION

		SET @B_Result = 1;
		SET @T_Message = 'Actualización de datos correcta.';
	END TRY  
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END  
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarPeriodo')
	DROP PROCEDURE [dbo].[USP_I_RegistrarPeriodo]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarPeriodo]
@I_Anio INT,
@I_Mes INT,
@T_MesDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecCre DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE();
		
		INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc, I_UsuarioCre, D_FecCre)
		VALUES(@I_Anio, @I_Mes, @T_MesDesc, @I_UserID, @D_FecCre);

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarPeriodo')
	DROP PROCEDURE [dbo].[USP_U_ActualizarPeriodo]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarPeriodo]
@I_PeriodoID INT,
@I_Anio INT,
@I_Mes INT,
@T_MesDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecMod DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE();
		
		UPDATE dbo.TR_Periodo SET
			I_Anio = @I_Anio,
			I_Mes = @I_Mes,
			T_MesDesc = @T_MesDesc,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_PeriodoID = @I_PeriodoID;

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_D_EliminarPeriodo')
	DROP PROCEDURE [dbo].[USP_D_EliminarPeriodo]
GO

CREATE PROCEDURE [dbo].[USP_D_EliminarPeriodo]
@I_PeriodoID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN
	BEGIN TRY
	
		DELETE TR_Periodo WHERE I_PeriodoID = @I_PeriodoID;

		COMMIT TRAN;

		SET @B_Result = 1;

		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarDependencia')
	DROP PROCEDURE [dbo].[USP_I_RegistrarDependencia]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarDependencia]
@C_DependenciaCod VARCHAR(20),
@T_DependenciaDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecCre DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE();

		INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@T_DependenciaDesc, @C_DependenciaCod, 1, 0, @I_UserID, @D_FecCre);

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarDependencia')
	DROP PROCEDURE [dbo].[USP_U_ActualizarDependencia]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarDependencia]
@I_DependenciaID INT,
@C_DependenciaCod VARCHAR(20),
@T_DependenciaDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecMod DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE();

		UPDATE dbo.TC_Dependencia SET
			C_DependenciaCod = @C_DependenciaCod,
			T_DependenciaDesc = @T_DependenciaDesc,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_DependenciaID = @I_DependenciaID;

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoDependencia')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoDependencia]
GO
  
CREATE PROCEDURE [dbo].[USP_U_CambiarEstadoDependencia]
@I_DependenciaID INT,
@B_Habilitado BIT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(4000) OUTPUT
AS  
BEGIN  
	SET NOCOUNT ON;  
	
	BEGIN TRY
		DECLARE @D_FecMod DATETIME = GETDATE()

		UPDATE dbo.TC_Dependencia SET 
			B_Habilitado = @B_Habilitado,  
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_DependenciaID = @I_DependenciaID;

		SET @B_Result = 1;
		SET @T_Message = 'Actualización de estado correcta.';
	END TRY  
	BEGIN CATCH
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END  
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_EliminarDependencia')
	DROP PROCEDURE [dbo].[USP_U_EliminarDependencia]
GO

CREATE PROCEDURE [dbo].[USP_U_EliminarDependencia]
@I_DependenciaID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN
	BEGIN TRY
	
		UPDATE dbo.TC_Dependencia SET 
			B_Eliminado= 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_DependenciaID = @I_DependenciaID

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarActividad')
	DROP PROCEDURE [dbo].[USP_I_RegistrarActividad]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarActividad]
@C_ActividadCod VARCHAR(20),
@T_ActividadDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecCre DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE();

		INSERT dbo.TC_Actividad(T_ActividadDesc, C_ActividadCod, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@T_ActividadDesc, @C_ActividadCod, 0, @I_UserID, @D_FecCre);

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarActividad')
	DROP PROCEDURE [dbo].[USP_U_ActualizarActividad]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarActividad]
@I_ActividadID INT,
@C_ActividadCod VARCHAR(20),
@T_ActividadDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecMod DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE();

		UPDATE dbo.TC_Actividad SET
			C_ActividadCod = @C_ActividadCod,
			T_ActividadDesc = @T_ActividadDesc,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_ActividadID = @I_ActividadID;

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_EliminarActividad')
	DROP PROCEDURE [dbo].[USP_U_EliminarActividad]
GO

CREATE PROCEDURE [dbo].[USP_U_EliminarActividad]
@I_ActividadID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN
	BEGIN TRY
	
		UPDATE dbo.TC_Actividad SET 
			B_Eliminado= 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_ActividadID = @I_ActividadID

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarMeta')
	DROP PROCEDURE [dbo].[USP_I_RegistrarMeta]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarMeta]
@C_MetaCod VARCHAR(20),
@T_MetaDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecCre DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE();

		INSERT dbo.TC_Meta(T_MetaDesc, C_MetaCod, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@T_MetaDesc, @C_MetaCod, 0, @I_UserID, @D_FecCre);

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarMeta')
	DROP PROCEDURE [dbo].[USP_U_ActualizarMeta]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarMeta]
@I_MetaID INT,
@C_MetaCod VARCHAR(20),
@T_MetaDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecMod DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE();

		UPDATE dbo.TC_Meta SET
			C_MetaCod = @C_MetaCod,
			T_MetaDesc = @T_MetaDesc,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_MetaID = @I_MetaID;

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_EliminarMeta')
	DROP PROCEDURE [dbo].[USP_U_EliminarMeta]
GO

CREATE PROCEDURE [dbo].[USP_U_EliminarMeta]
@I_MetaID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN
	BEGIN TRY
	
		UPDATE dbo.TC_Meta SET 
			B_Eliminado= 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_MetaID = @I_MetaID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarDepActividadMeta')
	DROP PROCEDURE [dbo].[USP_I_RegistrarDepActividadMeta]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarDepActividadMeta]
@I_Anio INT,
@I_CategoriaPlanillaID INT,
@I_DependenciaID INT,
@T_Descripcion VARCHAR(250),
@I_ActividadID INT,
@I_MetaID INT,
@I_CategoriaPresupuestalID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecCre DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE();

		INSERT dbo.TC_DepActividadMeta(I_Anio, I_CategoriaPlanillaID, I_DependenciaID, T_Descripcion, I_ActividadID, I_MetaID, I_CategoriaPresupuestalID, I_UsuarioCre, D_FecCre)
		VALUES(@I_Anio, @I_CategoriaPlanillaID, @I_DependenciaID, @T_Descripcion, @I_ActividadID, @I_MetaID, @I_CategoriaPresupuestalID, @I_UserID, @D_FecCre);

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarDepActividadMeta')
	DROP PROCEDURE [dbo].[USP_U_ActualizarDepActividadMeta]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarDepActividadMeta]
@I_DepActividadMetaID INT,
@I_Anio INT,
@I_CategoriaPlanillaID INT,
@I_DependenciaID INT,
@T_Descripcion VARCHAR(250),
@I_ActividadID INT,
@I_MetaID INT,
@I_CategoriaPresupuestalID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecMod DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE();

		UPDATE dbo.TC_DepActividadMeta SET
			I_Anio = @I_Anio,
			I_CategoriaPlanillaID = @I_CategoriaPlanillaID,
			I_DependenciaID = @I_DependenciaID,
			T_Descripcion = @T_Descripcion,
			I_ActividadID = @I_ActividadID,
			I_MetaID = @I_MetaID,
			I_CategoriaPresupuestalID = @I_CategoriaPresupuestalID,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_DepActividadMetaID = @I_DepActividadMetaID;

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_D_EliminarDepActividadMeta')
	DROP PROCEDURE [dbo].[USP_D_EliminarDepActividadMeta]
GO

CREATE PROCEDURE [dbo].[USP_D_EliminarDepActividadMeta]
@I_DepActividadMetaID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN
	BEGIN TRY
	
		DELETE FROM dbo.TC_DepActividadMeta WHERE I_DepActividadMetaID = @I_DepActividadMetaID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarAnio')
	DROP PROCEDURE [dbo].[USP_I_RegistrarAnio]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarAnio]
@I_Anio INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN
	BEGIN TRY
	
		INSERT dbo.TC_Anio(I_Anio) VALUES(@I_Anio);

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_S_ListarResumenPlanillaTrabajador')
	DROP PROCEDURE [dbo].[USP_S_ListarResumenPlanillaTrabajador]
GO

CREATE PROCEDURE [dbo].[USP_S_ListarResumenPlanillaTrabajador]
@I_Anio INT,
@I_Mes INT,
@I_CategoriaPlanillaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		trab.I_TrabajadorID, trab.C_TrabajadorCod, trab.T_Nombre, trab.T_ApellidoPaterno, trab.T_ApellidoMaterno, trab.I_TipoDocumentoID, trab.T_TipoDocumentoDesc, trab.C_NumDocumento,
		trab.I_RegimenID, trab.T_RegimenDesc, trab.I_EstadoID, trab.T_EstadoDesc, trab.I_VinculoID, trab.T_VinculoDesc,
		trabpla.I_TrabajadorPlanillaID, trabpla.I_TotalRemuneracion, trabpla.I_TotalReintegro, trabpla.I_TotalDeduccion, trabpla.I_TotalBruto, trabpla.I_TotalDescuento, trabpla.I_TotalSueldo,
		pla.I_PlanillaID, pla.I_PeriodoID, per.I_Anio, per.I_Mes, per.T_MesDesc, pla.I_CategoriaPlanillaID, catpla.T_CategoriaPlanillaDesc, trabpla.I_TrabajadorCategoriaPlanillaID
	FROM dbo.VW_TrabajadoresCategoriaPlanilla AS trab INNER JOIN
		dbo.TR_TrabajadorPlanilla AS trabpla ON trabpla.I_TrabajadorCategoriaPlanillaID = trab.I_TrabajadorCategoriaPlanillaID INNER JOIN
		dbo.TR_Planilla AS pla ON pla.I_PlanillaID = trabpla.I_PlanillaID INNER JOIN
		dbo.TR_Periodo AS per ON per.I_PeriodoID = pla.I_PeriodoID INNER JOIN
		dbo.TC_CategoriaPlanilla AS catpla ON catpla.I_CategoriaPlanillaID = pla.I_CategoriaPlanillaID
	WHERE trabpla.B_Anulado = 0 AND pla.B_Anulado = 0 AND per.I_Anio = @I_Anio AND per.I_Mes = @I_Mes AND pla.I_CategoriaPlanillaID = @I_CategoriaPlanillaID
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_S_ListarResumenPorActividadYDependencia')
	DROP PROCEDURE [dbo].[USP_S_ListarResumenPorActividadYDependencia]
GO

CREATE PROCEDURE [dbo].[USP_S_ListarResumenPorActividadYDependencia]
@I_Anio INT,
@I_Mes INT,
@I_CategoriaPlanillaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		dam.C_ActividadCod,
		dam.C_DependenciaCod,
		dam.T_DependenciaDesc,
		SUM(trabpla.I_TotalRemuneracion) AS I_TotalRemuneracion, 
		SUM(trabpla.I_TotalReintegro) AS I_TotalReintegro,
		SUM(trabpla.I_TotalDeduccion) AS I_TotalDeduccion,
		SUM(trabpla.I_TotalBruto) AS I_TotalBruto,
		SUM(trabpla.I_TotalDescuento) AS I_TotalDescuento,
		SUM(trabpla.I_TotalSueldo) AS I_TotalSueldo
	FROM dbo.TR_Planilla AS pla INNER JOIN
		dbo.TR_Periodo AS per ON per.I_PeriodoID = pla.I_PeriodoID INNER JOIN
		dbo.TR_TrabajadorPlanilla AS trabpla ON trabpla.I_PlanillaID = pla.I_PlanillaID INNER JOIN
		dbo.VW_DepActividadMeta AS dam ON dam.I_DependenciaID = trabpla.I_DependenciaID AND dam.I_Anio = per.I_Anio AND dam.I_CategoriaPlanillaID = pla.I_CategoriaPlanillaID
	WHERE trabpla.B_Anulado = 0 AND pla.B_Anulado = 0 AND per.I_Anio = @I_Anio AND per.I_Mes = @I_Mes AND pla.I_CategoriaPlanillaID = @I_CategoriaPlanillaID
	GROUP BY dam.C_ActividadCod, dam.C_DependenciaCod, dam.T_DependenciaDesc
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_S_ListarResumenSIAF')
	DROP PROCEDURE [dbo].[USP_S_ListarResumenSIAF]
GO

CREATE PROCEDURE [dbo].[USP_S_ListarResumenSIAF]
@I_Anio INT,
@I_Mes INT,
@I_CategoriaPlanillaID INT,
@I_VinculoID INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @I_PeriodoID INT,
			--Construcción de consulta
			@SQLString NVARCHAR(2000),
			@ParmDefinition NVARCHAR(1000),
			@Columns VARCHAR(1000),
			@ColumnNames VARCHAR(1000);
    
	SET @I_PeriodoID = (SELECT per.I_PeriodoID FROM dbo.TR_Periodo per WHERE per.I_Anio = @I_Anio AND per.I_Mes = @I_Mes);

	SELECT DISTINCT ctp.T_ConceptoDesc, ctp.I_Orden FROM dbo.TR_Concepto_TrabajadorPlanilla ctp
	INNER JOIN dbo.TR_TrabajadorPlanilla trab ON trab.I_TrabajadorPlanillaID = ctp.I_TrabajadorPlanillaID
	INNER JOIN dbo.TR_Planilla pla ON pla.I_PlanillaID = trab.I_PlanillaID
	WHERE trab.B_Anulado = 0 AND ctp.B_Anulado = 0 AND pla.I_PeriodoID = @I_PeriodoID AND pla.I_CategoriaPlanillaID = @I_CategoriaPlanillaID
	ORDER BY ctp.I_Orden;

	WITH Tmp_Conceptos(T_ConceptoDesc)
	AS
	(
		SELECT DISTINCT ctp.T_ConceptoDesc
		FROM dbo.TR_Concepto_TrabajadorPlanilla ctp
		INNER JOIN dbo.TR_TrabajadorPlanilla trab ON trab.I_TrabajadorPlanillaID = ctp.I_TrabajadorPlanillaID
		INNER JOIN dbo.TR_Planilla pla ON pla.I_PlanillaID = trab.I_PlanillaID
		WHERE trab.B_Anulado = 0 AND ctp.B_Anulado = 0 AND pla.I_PeriodoID = @I_PeriodoID AND pla.I_CategoriaPlanillaID = @I_CategoriaPlanillaID
	)
	SELECT 
		@Columns = STRING_AGG('[' + T_ConceptoDesc + ']', ',')
	FROM Tmp_Conceptos;

	SET @SQLString = N'SELECT C_ActividadCod AS Actividad, C_MetaCod AS Meta, ' + @Columns + ' FROM
		(SELECT dam.C_ActividadCod, dam.C_MetaCod, ctp.T_ConceptoDesc, ctp.M_Monto FROM dbo.TR_Concepto_TrabajadorPlanilla ctp
		INNER JOIN dbo.TR_TrabajadorPlanilla trabpla ON trabpla.I_TrabajadorPlanillaID = ctp.I_TrabajadorPlanillaID
		INNER JOIN dbo.TR_Planilla pla ON pla.I_PlanillaID = trabpla.I_PlanillaID
		INNER JOIN dbo.TR_Periodo per ON per.I_PeriodoID = pla.I_PeriodoID
		INNER JOIN dbo.VW_DepActividadMeta AS dam ON dam.I_DependenciaID = trabpla.I_DependenciaID AND dam.I_Anio = per.I_Anio AND dam.I_CategoriaPlanillaID = pla.I_CategoriaPlanillaID
		WHERE trabpla.B_Anulado = 0 AND ctp.B_Anulado = 0 AND per.I_PeriodoID = @I_PeriodoID AND pla.I_CategoriaPlanillaID = @I_CategoriaPlanillaID
		' + CASE WHEN @I_VinculoID IS NULL THEN '' ELSE ' AND trabpla.I_VinculoID = @I_VinculoID' END + ') AS SourceTable
	PIVOT (
		SUM(M_Monto) FOR T_ConceptoDesc IN (' + @Columns + ')
	) AS PivottABLE';

	SET @ParmDefinition = N'@I_PeriodoID INT, @I_CategoriaPlanillaID INT, @I_VinculoID INT';

	--PRINT @SQLString

	EXECUTE SP_EXECUTESQL @SQLString, @ParmDefinition,
	  @I_PeriodoID = @I_PeriodoID,
	  @I_CategoriaPlanillaID = @I_CategoriaPlanillaID,
	  @I_VinculoID = @I_VinculoID;
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarTrabajadorCategoriaPlanilla')
	DROP PROCEDURE [dbo].[USP_I_RegistrarTrabajadorCategoriaPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarTrabajadorCategoriaPlanilla]
@I_TrabajadorID INT,
@I_CategoriaPlanillaID INT,
@I_DependenciaID INT,
@I_GrupoTrabajoID INT = NULL,
@B_EsJefe BIT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecCre DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE();

		INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_CategoriaPrincipal, I_DependenciaID, I_GrupoTrabajoID,
			B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre, B_EsJefe)
		VALUES(@I_TrabajadorID, @I_CategoriaPlanillaID, 0, @I_DependenciaID, @I_GrupoTrabajoID, 1, 0, @I_UserID, @D_FecCre, @B_EsJefe)

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarTrabajadorCategoriaPlanilla')
	DROP PROCEDURE [dbo].[USP_U_ActualizarTrabajadorCategoriaPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarTrabajadorCategoriaPlanilla]
@I_TrabajadorCategoriaPlanillaID INT,
@I_CategoriaPlanillaID INT,
@I_DependenciaID INT,
@I_GrupoTrabajoID INT = NULL,
@B_EsJefe BIT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecMod DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE();

		UPDATE dbo.TC_Trabajador_CategoriaPlanilla SET
			I_CategoriaPlanillaID = @I_CategoriaPlanillaID,
			I_DependenciaID = @I_DependenciaID,
			I_GrupoTrabajoID = @I_GrupoTrabajoID,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod,
			B_EsJefe = @B_EsJefe
		WHERE I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoTrabajadorCategoriaPlanilla')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoTrabajadorCategoriaPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_U_CambiarEstadoTrabajadorCategoriaPlanilla]
@I_TrabajadorCategoriaPlanillaID INT,
@B_Habilitado BIT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TC_Trabajador_CategoriaPlanilla SET
			B_Habilitado = @B_Habilitado,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_EliminarTrabajadorCategoriaPlanilla')
	DROP PROCEDURE [dbo].[USP_U_EliminarTrabajadorCategoriaPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_U_EliminarTrabajadorCategoriaPlanilla]
@I_TrabajadorCategoriaPlanillaID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TC_Trabajador_CategoriaPlanilla SET
			B_Eliminado = 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_S_ListarTrabajadoresConPlanilla')
	DROP PROCEDURE [dbo].[USP_S_ListarTrabajadoresConPlanilla]
GO

CREATE PROCEDURE [dbo].[USP_S_ListarTrabajadoresConPlanilla]
@I_Anio INT,
@I_Mes INT,
@I_CategoriaPlanillaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		trab.I_TrabajadorID, trab.C_TrabajadorCod, per.T_Nombre, per.T_ApellidoPaterno, per.T_ApellidoMaterno, 
		tipdoc.T_TipoDocumentoDesc, per.C_NumDocumento, est.T_EstadoDesc, vin.T_VinculoDesc,
		@I_Anio AS I_Anio, @I_Mes AS I_Mes
	FROM 
		dbo.TC_Persona AS per INNER JOIN
		dbo.TC_Trabajador AS trab ON trab.I_PersonaID = per.I_PersonaID INNER JOIN
		dbo.TC_TipoDocumento AS tipdoc ON tipdoc.I_TipoDocumentoID = per.I_TipoDocumentoID INNER JOIN
		dbo.TC_Estado AS est ON est.I_EstadoID = trab.I_EstadoID INNER JOIN 
		dbo.TC_Vinculo AS vin ON vin.I_VinculoID = trab.I_VinculoID INNER JOIN 
		dbo.TC_Trabajador_CategoriaPlanilla cat ON cat.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
		dbo.TR_TrabajadorPlanilla AS tp ON tp.I_TrabajadorCategoriaPlanillaID = cat.I_TrabajadorCategoriaPlanillaID INNER JOIN 
		dbo.TR_Planilla AS pl ON pl.I_PlanillaID = tp.I_PlanillaID INNER JOIN
		dbo.TR_Periodo AS pr ON pr.I_PeriodoID = pl.I_PeriodoID
	WHERE	per.B_Eliminado = 0 AND 
			trab.B_Eliminado = 0 AND 
			cat.B_Eliminado = 0 AND
			tp.B_Anulado = 0 AND
			pl.B_Anulado = 0 AND
			pr.I_Anio = @I_Anio AND
			pr.I_Mes = @I_Mes AND
			cat.I_CategoriaPlanillaID = @I_CategoriaPlanillaID;
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador')
	DROP PROCEDURE [dbo].[USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador]
GO

CREATE PROCEDURE [dbo].[USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador]
@I_TrabajadorID INT,
@I_Anio INT,
@I_Mes INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT pl.I_PlanillaID, trabPla.I_TrabajadorPlanillaID, pr.I_Anio, pr.I_Mes, pr.T_MesDesc, 
		dep.T_DependenciaDesc, cp.I_CategoriaPlanillaID, cp.T_CategoriaPlanillaDesc, cl.T_ClasePlanillaDesc, tp.T_TipoPlanillaDesc, gp.T_GrupoTrabajoDesc,
		trabPla.I_TotalRemuneracion, trabPla.I_TotalReintegro, trabPla.I_TotalDeduccion, trabPla.I_TotalBruto, trabPla.I_TotalDescuento, trabPla.I_TotalSueldo
	FROM dbo.TR_TrabajadorPlanilla AS trabPla
	INNER  JOIN dbo.TR_Planilla AS pl ON pl.I_PlanillaID = trabPla.I_PlanillaID
	INNER JOIN dbo.TR_Periodo AS pr ON pr.I_PeriodoID = pl.I_PeriodoID
	INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla AS tc ON tc.I_TrabajadorCategoriaPlanillaID = trabpla.I_TrabajadorCategoriaPlanillaID
	LEFT JOIN dbo.TC_GrupoTrabajo AS gp ON gp.I_GrupoTrabajoID = trabpla.I_GrupoTrabajoID
	INNER JOIN dbo.TC_Dependencia AS dep ON dep.I_DependenciaID = trabPla.I_DependenciaID
	INNER JOIN dbo.TC_CategoriaPlanilla AS cp ON cp.I_CategoriaPlanillaID = pl.I_CategoriaPlanillaID
	INNER JOIN dbo.TC_ClasePlanilla AS cl ON cl.I_ClasePlanillaID = cp.I_ClasePlanillaID
	INNER JOIN dbo.TC_TipoPlanilla AS tp ON tp.I_TipoPlanillaID = cl.I_TipoPlanillaID
	WHERE trabPla.B_Anulado = 0 AND pl.B_Anulado = 0 AND 
		pr.I_Anio = @I_Anio AND pr.I_Mes = @I_Mes AND tc.I_TrabajadorID = @I_TrabajadorID
	ORDER BY trabPla.B_CategoriaPrincipal DESC;
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_S_ListarConceptosGeneradosPorategoriaYTrabajador')
	DROP PROCEDURE [dbo].[USP_S_ListarConceptosGeneradosPorategoriaYTrabajador]
GO

CREATE PROCEDURE [dbo].[USP_S_ListarConceptosGeneradosPorategoriaYTrabajador]
@I_TrabajadorPlanillaID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT cp.I_TrabajadorPlanillaID, t.I_TipoConceptoID, t.T_TipoConceptoDesc, cp.C_ConceptoCod, cp.T_ConceptoDesc, cp.T_ConceptoAbrv, cp.M_Monto 
	FROM dbo.TR_Concepto_TrabajadorPlanilla cp
	INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = cp.I_ConceptoID
	INNER JOIN dbo.TC_TipoConcepto t ON t.I_TipoConceptoID = c.I_TipoConceptoID
	WHERE cp.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID AND cp.B_Anulado = 0
	ORDER BY cp.I_Orden
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarGrupoTrabajo')
	DROP PROCEDURE [dbo].[USP_I_RegistrarGrupoTrabajo]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarGrupoTrabajo]
@C_GrupoTrabajoCod VARCHAR(20),
@T_GrupoTrabajoDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecCre DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE();

		INSERT dbo.TC_GrupoTrabajo(C_GrupoTrabajoCod, T_GrupoTrabajoDesc, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@C_GrupoTrabajoCod, @T_GrupoTrabajoDesc, 1, 0, @I_UserID, @D_FecCre)

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Registro correcto.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarGrupoTrabajo')
	DROP PROCEDURE [dbo].[USP_U_ActualizarGrupoTrabajo]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarGrupoTrabajo]
@I_GrupoTrabajoID INT,
@C_GrupoTrabajoCod VARCHAR(20),
@T_GrupoTrabajoDesc VARCHAR(250),
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecMod DATETIME;

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE();

		UPDATE dbo.TC_GrupoTrabajo SET
			C_GrupoTrabajoCod = @C_GrupoTrabajoCod,
			T_GrupoTrabajoDesc = @T_GrupoTrabajoDesc,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_GrupoTrabajoID = @I_GrupoTrabajoID

		COMMIT TRAN

		SET @B_Result = 1;

		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;

		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoGrupoTrabajo')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoGrupoTrabajo]
GO

CREATE PROCEDURE [dbo].[USP_U_CambiarEstadoGrupoTrabajo]
@I_GrupoTrabajoID INT,
@B_Habilitado BIT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TC_GrupoTrabajo SET
			B_Habilitado = @B_Habilitado,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_GrupoTrabajoID = @I_GrupoTrabajoID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Actualización correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_EliminarGrupoTrabajo')
	DROP PROCEDURE [dbo].[USP_U_EliminarGrupoTrabajo]
GO

CREATE PROCEDURE [dbo].[USP_U_EliminarGrupoTrabajo]
@I_GrupoTrabajoID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TC_GrupoTrabajo SET
			B_Eliminado = 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_GrupoTrabajoID = @I_GrupoTrabajoID;

		COMMIT TRAN
		SET @B_Result = 1;
		SET @T_Message = 'Eliminación correcta.';
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0;
		SET @T_Message = ERROR_MESSAGE();
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_S_ListarDetallePlanillaTrabajador')
	DROP PROCEDURE [dbo].[USP_S_ListarDetallePlanillaTrabajador]
GO

CREATE PROCEDURE [dbo].[USP_S_ListarDetallePlanillaTrabajador]
@I_Anio INT,
@I_Mes INT,
@I_CategoriaPlanillaID INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @I_PeriodoID INT,
			--Construcción de consulta
			@SQLString NVARCHAR(2000),
			@ParmDefinition NVARCHAR(1000),
			@Columns VARCHAR(1000),
			@ColumnNames VARCHAR(1000);
    
	SET @I_PeriodoID = (SELECT per.I_PeriodoID FROM dbo.TR_Periodo per WHERE per.I_Anio = @I_Anio AND per.I_Mes = @I_Mes);

	SELECT DISTINCT ctp.T_ConceptoDesc, ctp.I_Orden FROM dbo.TR_Concepto_TrabajadorPlanilla ctp
	INNER JOIN dbo.TR_TrabajadorPlanilla trab ON trab.I_TrabajadorPlanillaID = ctp.I_TrabajadorPlanillaID
	INNER JOIN dbo.TR_Planilla pla ON pla.I_PlanillaID = trab.I_PlanillaID
	WHERE pla.B_Anulado = 0 AND trab.B_Anulado = 0 AND ctp.B_Anulado = 0 AND pla.I_PeriodoID = @I_PeriodoID AND pla.I_CategoriaPlanillaID = @I_CategoriaPlanillaID
	ORDER BY ctp.I_Orden;

	WITH Tmp_Conceptos(T_ConceptoDesc, I_Orden)
	AS
	(
		SELECT DISTINCT ctp.T_ConceptoDesc, ctp.I_Orden
		FROM dbo.TR_Concepto_TrabajadorPlanilla ctp
		INNER JOIN dbo.TR_TrabajadorPlanilla trab ON trab.I_TrabajadorPlanillaID = ctp.I_TrabajadorPlanillaID
		INNER JOIN dbo.TR_Planilla pla ON pla.I_PlanillaID = trab.I_PlanillaID
		WHERE pla.B_Anulado = 0 AND trab.B_Anulado = 0 AND ctp.B_Anulado = 0 AND pla.I_PeriodoID = @I_PeriodoID AND pla.I_CategoriaPlanillaID = @I_CategoriaPlanillaID
	)
	SELECT 
		@Columns = STRING_AGG('[' + T_ConceptoDesc + ']', ',') WITHIN GROUP (ORDER BY I_Orden)
	FROM Tmp_Conceptos;

	SET @SQLString = N'SELECT I_Anio AS [AÑO], T_MesDesc AS [MES], C_TrabajadorCod AS [COD.TRABAJADOR], T_ApellidoPaterno + '' '' +  ISNULL(T_ApellidoMaterno, '''') + '', '' + T_Nombre AS [APELLIDOS Y NOMBRES], 
		T_TipoDocumentoDesc AS [TIP.DOC.] , C_NumDocumento AS [NUM.DOC.], ' + @Columns + ' FROM
		(SELECT per.I_Anio, per.T_MesDesc, trab.C_TrabajadorCod, trab.T_Nombre, trab.T_ApellidoPaterno, trab.T_ApellidoMaterno, trab.T_TipoDocumentoDesc, trab.C_NumDocumento, 
			ctp.T_ConceptoDesc, ctp.M_Monto 
		FROM dbo.TR_Concepto_TrabajadorPlanilla ctp
		INNER JOIN dbo.TR_TrabajadorPlanilla trabpla ON trabpla.I_TrabajadorPlanillaID = ctp.I_TrabajadorPlanillaID
		INNER JOIN dbo.TR_Planilla pla ON pla.I_PlanillaID = trabpla.I_PlanillaID
		INNER JOIN dbo.TR_Periodo per ON per.I_PeriodoID = pla.I_PeriodoID
		INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla tcpl ON tcpl.I_TrabajadorCategoriaPlanillaID = trabpla.I_TrabajadorCategoriaPlanillaID 
		INNER JOIN dbo.VW_Trabajadores trab ON trab.I_TrabajadorID = tcpl.I_TrabajadorID
		WHERE tcpl.B_Eliminado = 0 AND  pla.B_Anulado = 0 AND trabpla.B_Anulado = 0 AND ctp.B_Anulado = 0 AND per.I_PeriodoID = @I_PeriodoID AND pla.I_CategoriaPlanillaID = @I_CategoriaPlanillaID
		) AS SourceTable
	PIVOT (
		SUM(M_Monto) FOR T_ConceptoDesc IN (' + @Columns + ')
	) AS PivotTable';

	SET @ParmDefinition = N'@I_PeriodoID INT, @I_CategoriaPlanillaID INT';

	--PRINT @SQLString

	EXECUTE SP_EXECUTESQL @SQLString, @ParmDefinition,
	  @I_PeriodoID = @I_PeriodoID,
	  @I_CategoriaPlanillaID = @I_CategoriaPlanillaID;
END
GO
