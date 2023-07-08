USE [BD_OCRH_GestionPlanillas]
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarTrabajador')
	DROP PROCEDURE [dbo].[USP_I_RegistrarTrabajador]
GO

CREATE PROCEDURE [dbo].[USP_I_RegistrarTrabajador]
@C_TrabajadorCod VARCHAR(20),
@T_ApellidoPaterno VARCHAR(250),
@T_ApellidoMaterno VARCHAR(250),
@T_Nombre VARCHAR(250),
@I_TipoDocumentoID INT,
@C_NumDocumento VARCHAR(20),
@D_FechaIngreso DATETIME = NULL,
@I_RegimenID INT,
@I_EstadoID INT,
@I_VinculoID INT,
@I_BancoID INT = NULL,
@T_NroCuentaBancaria VARCHAR(250) = NULL,
@I_DependenciaID INT = NULL,
@I_AfpID INT = NULL,
@T_Cuspp VARCHAR(20) = NULL,
@I_CategoriaDocenteID INT = NULL,
@I_HorasDocenteID INT = NULL,
@I_GrupoOcupacionalID INT = NULL,
@I_NivelRemunerativoID INT = NULL,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	--CONSTANTE
	DECLARE @I_ADMINISTRATIVOID INT = 1,
			@I_DOCENTEID INT = 2

	DECLARE @I_PersonaID INT,
			@I_TrabajadorID INT,
			@D_FecCre DATETIME

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecCre = GETDATE()

		INSERT dbo.TC_Persona(T_ApellidoPaterno, T_ApellidoMaterno, T_Nombre, I_TipoDocumentoID, C_NumDocumento, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@T_ApellidoPaterno, @T_ApellidoMaterno, @T_Nombre, @I_TipoDocumentoID, @C_NumDocumento, 1, 0, @I_UserID, @D_FecCre)

		SET @I_PersonaID = SCOPE_IDENTITY()

		INSERT dbo.TC_Trabajador(I_PersonaID, C_TrabajadorCod, D_FechaIngreso, I_EstadoID, I_VinculoID, I_RegimenID, I_AfpID, T_Cuspp, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_PersonaID, @C_TrabajadorCod, @D_FechaIngreso, @I_EstadoID, @I_VinculoID, @I_RegimenID, @I_AfpID, @T_Cuspp, 1, 0, @I_UserID, @D_FecCre)

		SET @I_TrabajadorID = SCOPE_IDENTITY()

		INSERT dbo.TC_Trabajador_Dependencia(I_TrabajadorID, I_DependenciaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_TrabajadorID, @I_DependenciaID, 1, 0, @I_UserID, @D_FecCre)

		INSERT dbo.TC_CuentaBancaria(I_TrabajadorID, I_BancoID, T_NroCuentaBancaria, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_TrabajadorID, @I_BancoID, @T_NroCuentaBancaria, 1, 0, @I_UserID, @D_FecCre)

		IF (@I_VinculoID IN (1,2)) BEGIN
			INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@I_TrabajadorID, @I_GrupoOcupacionalID, @I_NivelRemunerativoID, 1, 0, @I_UserID, @D_FecCre)

			INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@I_TrabajadorID, @I_ADMINISTRATIVOID, 1, 0, @I_UserID, @D_FecCre)
		END

		IF (@I_VinculoID = 4) BEGIN
			INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@I_TrabajadorID, @I_CategoriaDocenteID, @I_HorasDocenteID, 1, 0, @I_UserID, @D_FecCre)

			INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@I_TrabajadorID, @I_DOCENTEID, 1, 0, @I_UserID, @D_FecCre)
		END

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



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarTrabajador')
	DROP PROCEDURE [dbo].[USP_U_ActualizarTrabajador]
GO

CREATE PROCEDURE [dbo].[USP_U_ActualizarTrabajador]
@I_trabajadorID INT,
@C_TrabajadorCod VARCHAR(20),
@T_ApellidoPaterno VARCHAR(250),
@T_ApellidoMaterno VARCHAR(250),
@T_Nombre VARCHAR(250),
@I_TipoDocumentoID INT,
@C_NumDocumento VARCHAR(20),
@D_FechaIngreso DATETIME = NULL,
@I_RegimenID INT,
@I_EstadoID INT,
@I_VinculoID INT,
@I_BancoID INT = NULL,
@T_NroCuentaBancaria VARCHAR(250) = NULL,
@I_DependenciaID INT = NULL,
@I_AfpID INT = NULL,
@T_Cuspp VARCHAR(20) = NULL,
@I_CategoriaDocenteID INT = NULL,
@I_HorasDocenteID INT = NULL,
@I_GrupoOcupacionalID INT = NULL,
@I_NivelRemunerativoID INT = NULL,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @D_FecMod DATETIME

	BEGIN TRAN
	BEGIN TRY
		SET @D_FecMod = GETDATE()


		--Actualizar Persona
		UPDATE per SET 
			per.T_ApellidoPaterno = @T_ApellidoPaterno,
			per.T_ApellidoMaterno = @T_ApellidoMaterno,
			per.T_Nombre = @T_Nombre,
			per.I_TipoDocumentoID = @I_TipoDocumentoID,
			per.C_NumDocumento = @C_NumDocumento,
			per.I_UsuarioMod = @I_UserID,
			per.D_FecMod = @D_FecMod
		FROM dbo.TC_Persona per
		INNER JOIN dbo.TC_Trabajador trab ON trab.I_PersonaID = per.I_PersonaID
		WHERE trab.I_TrabajadorID = @I_trabajadorID

		--Actualizar Trabajador
		UPDATE dbo.TC_Trabajador SET
			C_TrabajadorCod = @C_TrabajadorCod,
			D_FechaIngreso = @D_FechaIngreso,
			I_EstadoID = @I_EstadoID,
			I_VinculoID = @I_VinculoID,
			I_RegimenID = @I_RegimenID,
			I_AfpID = @I_AfpID,
			T_Cuspp = @T_Cuspp,
			I_UsuarioMod = @I_UserID,
			D_FecMod = @D_FecMod
		WHERE I_TrabajadorID = @I_TrabajadorID

		--Actualizar Dependencia
		IF (@I_DependenciaID IS NULL) BEGIN

			UPDATE dbo.TC_Trabajador_Dependencia SET 
				B_Habilitado = 0,
				I_UsuarioMod = @I_UserID,
				D_FecMod = @D_FecMod
			WHERE I_TrabajadorID = @I_trabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

		END ELSE BEGIN

			IF EXISTS(SELECT I_trabajadorID FROM dbo.TC_Trabajador_Dependencia 
				WHERE I_TrabajadorID = @I_trabajadorID AND I_DependenciaID = @I_DependenciaID AND B_Eliminado = 0)
			BEGIN
				IF NOT EXISTS(SELECT I_trabajadorID FROM dbo.TC_Trabajador_Dependencia 
					WHERE I_TrabajadorID = @I_trabajadorID AND I_DependenciaID = @I_DependenciaID AND B_Habilitado = 1 AND B_Eliminado = 0)
				BEGIN
					UPDATE dbo.TC_Trabajador_Dependencia SET 
						B_Habilitado = 0,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecMod
					WHERE I_TrabajadorID = @I_trabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

					UPDATE dbo.TC_Trabajador_Dependencia SET 
						B_Habilitado = 1,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecMod
					WHERE I_TrabajadorID = @I_trabajadorID AND I_DependenciaID = @I_DependenciaID AND B_Eliminado = 0
				END
			
			END ELSE BEGIN 
				UPDATE dbo.TC_Trabajador_Dependencia SET 
					B_Habilitado = 0,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_trabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

				INSERT dbo.TC_Trabajador_Dependencia(I_TrabajadorID, I_DependenciaID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorID, @I_DependenciaID, 1, 0, @I_UserID, @D_FecMod)
			END

		END

		--Actualizar Cuentas Bancarias
		IF (@I_BancoID IS NULL) BEGIN

			UPDATE dbo.TC_CuentaBancaria SET
				B_Habilitado = 0,
				I_UsuarioMod = @I_UserID,
				D_FecMod = @D_FecMod
			WHERE I_TrabajadorID = @I_trabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

		END ELSE BEGIN

			IF EXISTS(SELECT I_TrabajadorID FROM dbo.TC_CuentaBancaria 
				WHERE I_TrabajadorID = @I_trabajadorID AND I_BancoID = @I_BancoID AND B_Eliminado = 0)
			BEGIN
				IF EXISTS(SELECT I_TrabajadorID FROM dbo.TC_CuentaBancaria 
					WHERE I_TrabajadorID = @I_trabajadorID AND I_BancoID = @I_BancoID AND B_Habilitado = 1 AND B_Eliminado = 0)
				BEGIN
					UPDATE dbo.TC_CuentaBancaria SET
						T_NroCuentaBancaria = @T_NroCuentaBancaria,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecMod
					WHERE I_TrabajadorID = @I_trabajadorID AND I_BancoID = @I_BancoID AND B_Habilitado = 1 AND B_Eliminado = 0
				END ELSE BEGIN
					UPDATE dbo.TC_CuentaBancaria SET
						B_Habilitado = 0,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecMod
					WHERE I_TrabajadorID = @I_trabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

					UPDATE dbo.TC_CuentaBancaria SET
						T_NroCuentaBancaria = @T_NroCuentaBancaria,
						B_Habilitado = 1,
						I_UsuarioMod = @I_UserID,
						D_FecMod = @D_FecMod
					WHERE I_TrabajadorID = @I_trabajadorID AND I_BancoID = @I_BancoID AND B_Eliminado = 0
				END
			END ELSE BEGIN
				UPDATE dbo.TC_CuentaBancaria SET
					B_Habilitado = 0,
					I_UsuarioMod = @I_UserID,
					D_FecMod = @D_FecMod
				WHERE I_TrabajadorID = @I_trabajadorID AND B_Habilitado = 1 AND B_Eliminado = 0

				INSERT dbo.TC_CuentaBancaria(I_TrabajadorID, I_BancoID, T_NroCuentaBancaria, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorID, @I_BancoID, @T_NroCuentaBancaria, 1, 0, @I_UserID, @D_FecMod)
			END

		END

		IF (@I_VinculoID IN (1,2)) BEGIN
			UPDATE dbo.TC_Administrativo SET 
				I_GrupoOcupacionalID = @I_GrupoOcupacionalID,
				I_NivelRemunerativoID = @I_NivelRemunerativoID,
				I_UsuarioMod = @I_UserID,
				D_FecMod = @D_FecMod
			WHERE I_TrabajadorID = @I_TrabajadorID
		END

		IF (@I_VinculoID = 4) BEGIN
			UPDATE dbo.TC_Docente SET 
				I_CategoriaDocenteID = @I_CategoriaDocenteID,
				I_HorasDocenteID = @I_HorasDocenteID,
				I_UsuarioMod = @I_UserID,
				D_FecMod = @D_FecMod
			WHERE I_TrabajadorID = @I_TrabajadorID
		END
		
		COMMIT TRAN

		SET @B_Result = 1

		SET @T_Message = 'Actualización correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0

		SET @T_Message = ERROR_MESSAGE()
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
			ppc.B_AplicarFiltro1 = 1 AND ppc.I_Filtro1 = @I_Filtro1 AND ppc.B_AplicarFiltro2 = 1 AND ppc.I_Filtro2 = @I_Filtro2
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
				ppc.B_AplicarFiltro1 = 1 AND ppc.I_Filtro1 = @I_Filtro1 AND ppc.B_AplicarFiltro2 = 0
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
					ppc.B_AplicarFiltro1 = 0 AND ppc.B_AplicarFiltro2 = 1 AND ppc.I_Filtro2 = @I_Filtro2
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
						ppc.B_AplicarFiltro1 = 0 AND ppc.B_AplicarFiltro2 = 0
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
			@I_TotalDescuento DECIMAL(15,2) = 0,
			@I_TotalReintegro DECIMAL(15,2) = 0,
			@I_TotalDeduccion DECIMAL(15,2) = 0,
			@I_TotalSueldo DECIMAL(15,2) = 0,
			@I_CantPlanillasGeneradas INT = 0,
			--Resumen por trabajador
			@I_TrabajadorID INT,
			@I_Filtro1 INT,-->Grupo Ocupacional | Categoria
			@I_Filtro2 INT,-->Nivel Remunerativo | Dedicacion y horas
			@I_TrabajadorPlanillaID INT,
			@I_TotalRemuneracionTrabajador DECIMAL(15,2),
			@I_TotalDescuentoTrabajador DECIMAL(15,2),
			@I_TotalReintegroTrabajador DECIMAL(15,2),
			@I_TotalDeduccionTrabajador DECIMAL(15,2),
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
		I_TrabajadorID INT NOT NULL,
		I_Filtro1 INT NOT NULL,
		I_Filtro2 INT NOT NULL
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
		INSERT #tmp_trabajador(I_TrabajadorID, I_Filtro1, I_Filtro2)
		SELECT adm.I_TrabajadorID, adm.I_GrupoOcupacionalID, adm.I_NivelRemunerativoID 
		FROM dbo.TC_Administrativo adm
		INNER JOIN dbo.TC_Trabajador trab ON trab.I_TrabajadorID = adm.I_TrabajadorID
		INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla tca ON tca.I_TrabajadorID = trab.I_TrabajadorID 
		INNER JOIN dbo.TC_GrupoOcupacional gr ON gr.I_GrupoOcupacionalID = adm.I_GrupoOcupacionalID
		INNER JOIN dbo.TC_NivelRemunerativo nivrem ON nivrem.I_NivelRemunerativoID = adm.I_NivelRemunerativoID
		INNER JOIN @Tbl_Trabajador tmp ON tmp.I_ID = trab.I_TrabajadorID
		WHERE trab.B_Habilitado = 1 AND trab.B_Eliminado = 0 AND 
			adm.B_Habilitado = 1 AND adm.B_Eliminado = 0 AND 
			tca.B_Habilitado = 1 AND tca.B_Eliminado = 0 AND tca.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND
			NOT EXISTS(SELECT pl.I_PlanillaID FROM dbo.TR_Planilla pl 
				INNER JOIN dbo.TR_TrabajadorPlanilla tpl ON tpl.I_PlanillaID = pl.I_PlanillaID 
				WHERE tpl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND tpl.I_TrabajadorID = trab.I_TrabajadorID);
	END

	IF (@I_CategoriaPlanillaID = @I_DOCENTEID) BEGIN
		INSERT #tmp_trabajador(I_TrabajadorID, I_Filtro1, I_Filtro2)
		SELECT doc.I_TrabajadorID, doc.I_CategoriaDocenteID, doc.I_HorasDocenteID FROM dbo.TC_Docente doc
		INNER JOIN dbo.TC_Trabajador trab ON trab.I_TrabajadorID = doc.I_TrabajadorID
		INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla tca ON tca.I_TrabajadorID = trab.I_TrabajadorID 
		INNER JOIN dbo.TC_CategoriaDocente cd ON cd.I_CategoriaDocenteID = doc.I_CategoriaDocenteID
		INNER JOIN dbo.TC_HorasDocente hd ON hd.I_HorasDocenteID = doc.I_HorasDocenteID
		INNER JOIN @Tbl_Trabajador tmp ON tmp.I_ID = trab.I_TrabajadorID
		WHERE trab.B_Habilitado = 1 AND trab.B_Eliminado = 0 AND 
			doc.B_Habilitado = 1 AND doc.B_Eliminado = 0 AND 
			tca.B_Habilitado = 1 AND tca.B_Eliminado = 0 AND tca.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND
			NOT EXISTS(SELECT pl.I_PlanillaID FROM dbo.TR_Planilla pl 
				INNER JOIN dbo.TR_TrabajadorPlanilla tpl ON tpl.I_PlanillaID = pl.I_PlanillaID 
				WHERE tpl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND tpl.I_TrabajadorID = trab.I_TrabajadorID);
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
			I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
		VALUES(@I_PeriodoID, @I_CategoriaPlanillaID, @I_CorrelativoPlanilla, 0, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

		SET @I_PlantillaID = SCOPE_IDENTITY();

		--4. Recorrer cada trabajador
		WHILE (@I_Indicador <= @I_CantRegistros) BEGIN
		
			SELECT	@I_TrabajadorID = tmp.I_TrabajadorID,
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
							WHERE per.B_Habilitado = 1 AND per.B_Eliminado = 0 AND conc.B_Habilitado = 1 AND conc.B_Eliminado = 0 AND
								per.I_TrabajadorID = @I_TrabajadorID AND per.I_PeriodoID = @I_PeriodoID AND conc.I_ConceptoID = @I_ConceptoID), 0);
					END

					IF (@M_ValorConcepto IS NOT NULL AND @M_ValorConcepto > 0) BEGIN
						IF (@B_EsValorFijo = 0 AND @I_TrabajadorPlanillaID > 0) BEGIN
							SET @M_SumConceptos = (SELECT SUM(p.M_Monto) FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia base
								INNER JOIN dbo.TI_PlantillaPlanilla_Concepto ref ON ref.I_PlantillaPlanillaConceptoID = base.I_PlantillaPlanillaConceptoReferenciaID
								INNER JOIN dbo.TR_Concepto_TrabajadorPlanilla p ON p.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID AND p.I_ConceptoID = ref.I_ConceptoID
								WHERE	base.B_Eliminado = 0 AND 
										ref.B_Habilitado = 1 AND ref.B_Eliminado = 0 AND 
										p.B_Anulado = 0 AND base.I_PlantillaPlanillaConceptoBaseID = @I_PlantillaPlanillaConceptoID);

							SET @M_ValorConcepto = (@M_ValorConcepto/100) * @M_SumConceptos;
						END

						IF (@I_TrabajadorPlanillaID = 0) BEGIN
							INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorID, I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, 
								I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
							VALUES(@I_PlantillaID, @I_TrabajadorID, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

							SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();
						END
					
						INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
						VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, @M_ValorConcepto, 0, @I_UserID, @D_FecRegistro);
					
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
							WHERE per.B_Habilitado = 1 AND per.B_Eliminado = 0 AND conc.B_Habilitado = 1 AND conc.B_Eliminado = 0 AND
								per.I_TrabajadorID = @I_TrabajadorID AND per.I_PeriodoID = @I_PeriodoID AND conc.I_ConceptoID = @I_ConceptoID), 0);
					END

					IF (@M_ValorConcepto IS NOT NULL AND @M_ValorConcepto > 0) BEGIN
						IF (@B_EsValorFijo = 0 AND @I_TrabajadorPlanillaID > 0) BEGIN
							SET @M_SumConceptos = (SELECT SUM(p.M_Monto) FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia base
								INNER JOIN dbo.TI_PlantillaPlanilla_Concepto ref ON ref.I_PlantillaPlanillaConceptoID = base.I_PlantillaPlanillaConceptoReferenciaID
								INNER JOIN dbo.TR_Concepto_TrabajadorPlanilla p ON p.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID AND p.I_ConceptoID = ref.I_ConceptoID
								WHERE	base.B_Eliminado = 0 AND 
										ref.B_Habilitado = 1 AND ref.B_Eliminado = 0 AND 
										p.B_Anulado = 0 AND base.I_PlantillaPlanillaConceptoBaseID = @I_PlantillaPlanillaConceptoID);

							SET @M_ValorConcepto = (@M_ValorConcepto/100) * @M_SumConceptos;
						END

						IF (@I_TrabajadorPlanillaID = 0) BEGIN
							INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorID, I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, 
								I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
							VALUES(@I_PlantillaID, @I_TrabajadorID, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

							SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();
						END

						INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
						VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, @M_ValorConcepto, 0, @I_UserID, @D_FecRegistro);

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
							WHERE per.B_Habilitado = 1 AND per.B_Eliminado = 0 AND conc.B_Habilitado = 1 AND conc.B_Eliminado = 0 AND
								per.I_TrabajadorID = @I_TrabajadorID AND per.I_PeriodoID = @I_PeriodoID AND conc.I_ConceptoID = @I_ConceptoID), 0);
					END

					IF (@M_ValorConcepto IS NOT NULL AND @M_ValorConcepto > 0) BEGIN
						IF (@B_EsValorFijo = 0 AND @I_TrabajadorPlanillaID > 0) BEGIN
							SET @M_SumConceptos = (SELECT SUM(p.M_Monto) FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia base
								INNER JOIN dbo.TI_PlantillaPlanilla_Concepto ref ON ref.I_PlantillaPlanillaConceptoID = base.I_PlantillaPlanillaConceptoReferenciaID
								INNER JOIN dbo.TR_Concepto_TrabajadorPlanilla p ON p.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID AND p.I_ConceptoID = ref.I_ConceptoID
								WHERE	base.B_Eliminado = 0 AND 
										ref.B_Habilitado = 1 AND ref.B_Eliminado = 0 AND 
										p.B_Anulado = 0 AND base.I_PlantillaPlanillaConceptoBaseID = @I_PlantillaPlanillaConceptoID);

							SET @M_ValorConcepto = (@M_ValorConcepto/100) * @M_SumConceptos;
						END

						IF (@I_TrabajadorPlanillaID = 0) BEGIN
							INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorID, I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, 
								I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
							VALUES(@I_PlantillaID, @I_TrabajadorID, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

							SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();
						END

						INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
						VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, @M_ValorConcepto, 0, @I_UserID, @D_FecRegistro);

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
							WHERE per.B_Habilitado = 1 AND per.B_Eliminado = 0 AND conc.B_Habilitado = 1 AND conc.B_Eliminado = 0 AND
								per.I_TrabajadorID = @I_TrabajadorID AND per.I_PeriodoID = @I_PeriodoID AND conc.I_ConceptoID = @I_ConceptoID), 0);
					END

					IF (@M_ValorConcepto IS NOT NULL AND @M_ValorConcepto > 0) BEGIN
						IF (@B_EsValorFijo = 0 AND @I_TrabajadorPlanillaID > 0) BEGIN
							SET @M_SumConceptos = (SELECT SUM(p.M_Monto) FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia base
								INNER JOIN dbo.TI_PlantillaPlanilla_Concepto ref ON ref.I_PlantillaPlanillaConceptoID = base.I_PlantillaPlanillaConceptoReferenciaID
								INNER JOIN dbo.TR_Concepto_TrabajadorPlanilla p ON p.I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID AND p.I_ConceptoID = ref.I_ConceptoID
								WHERE	base.B_Eliminado = 0 AND 
										ref.B_Habilitado = 1 AND ref.B_Eliminado = 0 AND 
										p.B_Anulado = 0 AND base.I_PlantillaPlanillaConceptoBaseID = @I_PlantillaPlanillaConceptoID);

							SET @M_ValorConcepto = (@M_ValorConcepto/100) * @M_SumConceptos;
						END

						IF (@I_TrabajadorPlanillaID = 0) BEGIN
							INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorID, I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, 
								I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
							VALUES(@I_PlantillaID, @I_TrabajadorID, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

							SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();
						END

						INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
						VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @T_ConceptoAbrv, @M_ValorConcepto, 0, @I_UserID, @D_FecRegistro);

						SET @I_TotalDeduccionTrabajador = @I_TotalDeduccionTrabajador + @M_ValorConcepto;
					END
				END

				SET @I_NroOrden = @I_NroOrden + 1;
			END

			IF (@I_TrabajadorPlanillaID > 0) BEGIN
				--9. Actualizar totales por trabajador
				SET @I_TotalSueldoTrabajador = @I_TotalRemuneracionTrabajador - @I_TotalDescuentoTrabajador + @I_TotalReintegroTrabajador - @I_TotalDeduccionTrabajador;

				IF (@I_TotalSueldoTrabajador > 0 ) BEGIN

					UPDATE  dbo.TR_TrabajadorPlanilla SET 
						I_TotalRemuneracion = @I_TotalRemuneracionTrabajador,
						I_TotalDescuento = @I_TotalDescuentoTrabajador,
						I_TotalReintegro = @I_TotalReintegroTrabajador,
						I_TotalDeduccion = @I_TotalDeduccionTrabajador,
						I_TotalSueldo = @I_TotalSueldoTrabajador
					WHERE I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID;

					--10. Acumular para el resumen total de la planilla
					SET @I_TotalRemuneracion = @I_TotalRemuneracion + @I_TotalRemuneracionTrabajador;

					SET @I_TotalDescuento = @I_TotalDescuento + @I_TotalDescuentoTrabajador;

					SET @I_TotalReintegro = @I_TotalReintegro + @I_TotalReintegroTrabajador;

					SET @I_TotalDeduccion = @I_TotalDeduccion + @I_TotalDeduccionTrabajador;

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

		IF (@I_TotalSueldo > 0) BEGIN

			--11. Actualizar totales de planilla
			UPDATE dbo.TR_Planilla SET 
				I_TotalRemuneracion = @I_TotalRemuneracion, 
				I_TotalDescuento = @I_TotalDescuento, 
				I_TotalReintegro = @I_TotalReintegro, 
				I_TotalDeduccion = @I_TotalDeduccion,
				I_TotalSueldo = @I_TotalSueldo,
				I_CantRegistros = @I_CantPlanillasGeneradas
			WHERE I_PlanillaID = @I_PlantillaID;

			COMMIT TRAN
			
			SET @B_Result = 1;
			SET @T_Message = 'Planilla generada correctamente.';

		END ELSE BEGIN
			ROLLBACK TRAN

			SET @B_Result = 0;
			SET @T_Message = 'La generación de planilla resultó en un monto negativo, por lo que se canceló el proceso.';
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

CREATE PROCEDURE USP_I_RegistrarConcepto
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

CREATE PROCEDURE USP_U_ActualizarConcepto
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
		WHERE I_ConceptoID = @I_ConceptoID

		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Actualización correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoConcepto')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoConcepto]
GO

CREATE PROCEDURE USP_U_CambiarEstadoConcepto
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
		WHERE I_ConceptoID = @I_ConceptoID

		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Actualización correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarPlantillaPlanilla')
	DROP PROCEDURE [dbo].[USP_I_RegistrarPlantillaPlanilla]
GO

CREATE PROCEDURE USP_I_RegistrarPlantillaPlanilla
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
		VALUES(@I_CategoriaPlanillaID, @T_PlantillaPlanillaDesc, 1, 0, @I_UserID, GETDATE())

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



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarPlantillaPlanilla')
	DROP PROCEDURE [dbo].[USP_U_ActualizarPlantillaPlanilla]
GO

CREATE PROCEDURE USP_U_ActualizarPlantillaPlanilla
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
		WHERE I_PlantillaPlanillaID = @I_PlantillaPlanillaID

		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Actualización correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoPlantillaPlanilla')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoPlantillaPlanilla]
GO

CREATE PROCEDURE USP_U_CambiarEstadoPlantillaPlanilla
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
		WHERE I_PlantillaPlanillaID = @I_PlantillaPlanillaID

		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Actualización correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_RegistrarPlantillaPlanillaConcepto')
	DROP PROCEDURE [dbo].[USP_I_RegistrarPlantillaPlanillaConcepto]
GO

CREATE PROCEDURE USP_I_RegistrarPlantillaPlanillaConcepto
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

		INSERT dbo.TI_PlantillaPlanilla_Concepto_Referencia(I_PlantillaPlanillaConceptoBaseID, I_PlantillaPlanillaConceptoReferenciaID, B_Eliminado, I_UsuarioCre, D_FecCre)
		SELECT @I_PlantillaPlanillaConceptoID, I_ID, 0, @I_UserID, @D_FecCre FROM @Tbl_ConceptoReferencia

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



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_ActualizarPlantillaPlanillaConcepto')
	DROP PROCEDURE [dbo].[USP_U_ActualizarPlantillaPlanillaConcepto]
GO

CREATE PROCEDURE USP_U_ActualizarPlantillaPlanillaConcepto
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
		WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID

		UPDATE c SET
			B_Eliminado = 1, 
			I_UsuarioMod = @I_UserID, 
			D_FecMod = @D_FechaActual
		FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia c
		LEFT JOIN @Tbl_ConceptoReferencia r ON r.I_ID = c.I_PlantillaPlanillaConceptoReferenciaID
		WHERE 
			c.I_PlantillaPlanillaConceptoBaseID = @I_PlantillaPlanillaConceptoID AND 
			c.B_Eliminado = 0 AND 
			r.I_ID IS NULL

		INSERT dbo.TI_PlantillaPlanilla_Concepto_Referencia(I_PlantillaPlanillaConceptoBaseID, I_PlantillaPlanillaConceptoReferenciaID, B_Eliminado, I_UsuarioCre, D_FecCre)
		SELECT @I_PlantillaPlanillaConceptoID, I_ID, 0, @I_UserID, @D_FechaActual 
		FROM @Tbl_ConceptoReferencia r
		WHERE NOT EXISTS(SELECT c.I_ID FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia c 
			WHERE c.B_Eliminado = 0 AND c.I_PlantillaPlanillaConceptoBaseID = @I_PlantillaPlanillaConceptoID AND c.I_PlantillaPlanillaConceptoReferenciaID = r.I_ID)

		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Actualización correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_CambiarEstadoPlantillaPlanillaConcepto')
	DROP PROCEDURE [dbo].[USP_U_CambiarEstadoPlantillaPlanillaConcepto]
GO

CREATE PROCEDURE USP_U_CambiarEstadoPlantillaPlanillaConcepto
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
		WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID

		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Actualización correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
	END CATCH
END
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_U_EliminarPlantillaPlanillaConcepto')
	DROP PROCEDURE [dbo].[USP_U_EliminarPlantillaPlanillaConcepto]
GO

CREATE PROCEDURE USP_U_EliminarPlantillaPlanillaConcepto
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
		WHERE I_PlantillaPlanillaConceptoBaseID = @I_PlantillaPlanillaConceptoID

		UPDATE dbo.TI_PlantillaPlanilla_Concepto SET
			B_Habilitado = 0,
			B_Eliminado = 1,
			I_UsuarioMod = @I_UserID,
			D_FecMod = GETDATE()
		WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID
		
		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Actualización correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
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
	I_ID INT IDENTITY(1,1) NOT NULL,
	I_TrabajadorID INT NOT NULL,
	I_PeriodoID INT NOT NULL,
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
			@I_TrabajadorID INT,
			@I_PeriodoID INT,
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

			SELECT @I_TrabajadorID = I_TrabajadorID,
					@I_PeriodoID = I_PeriodoID,
					@I_ConceptoID = I_ConceptoID,
					@M_ValorConcepto = M_ValorConcepto,
					@I_ProveedorID = I_ProveedorID
			FROM @Tbl_ValorExterno
			WHERE I_ID = @I_Indicador

			IF NOT EXISTS(SELECT I_ValorExternoPeriodoID FROM dbo.TI_ValorExternoPeriodo WHERE I_TrabajadorID = @I_TrabajadorID AND I_PeriodoID = @I_PeriodoID AND B_Eliminado = 0)
			BEGIN
				INSERT dbo.TI_ValorExternoPeriodo(I_TrabajadorID, I_PeriodoID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
				VALUES(@I_TrabajadorID, @I_PeriodoID, 1, 0, @I_UserID, @D_FecActual);

				SET @I_ValorExternoPeriodoID = SCOPE_IDENTITY();
			END
			ELSE
			BEGIN
				SET @I_ValorExternoPeriodoID = (SELECT I_ValorExternoPeriodoID FROM dbo.TI_ValorExternoPeriodo 
					WHERE I_TrabajadorID = @I_TrabajadorID AND I_PeriodoID = @I_PeriodoID AND B_Eliminado = 0);
			END

			INSERT dbo.TI_ValorExternoConcepto(I_ValorExternoPeriodoID, I_ConceptoID, M_ValorConcepto, I_ProveedorID, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
			VALUES(@I_ValorExternoPeriodoID, @I_ConceptoID, @M_ValorConcepto, @I_ProveedorID, 1, 0, @I_UserID, @D_FecActual);

			SET @I_Indicador = @I_Indicador + 1
		END

		COMMIT TRAN
		SET @B_Result = 1
		SET @T_Message = 'Grabación correcta.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @B_Result = 0
		SET @T_Message = ERROR_MESSAGE()
	END CATCH
END
GO