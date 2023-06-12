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



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.DOMAINS WHERE DOMAIN_NAME = 'type_dataTrabajador') BEGIN
	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_GenerarPlanilla_Docente_Administrativo') BEGIN
		DROP PROCEDURE [dbo].[USP_I_GenerarPlanilla_Docente_Administrativo]
	END

	DROP TYPE [dbo].[type_dataTrabajador]
END
GO

CREATE TYPE [dbo].[type_dataTrabajador] AS TABLE(
	I_TrabajadorID int
)
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_GenerarPlanilla_Docente_Administrativo')
	DROP PROCEDURE [dbo].[USP_I_GenerarPlanilla_Docente_Administrativo]
GO


CREATE PROCEDURE [dbo].[USP_I_GenerarPlanilla_Docente_Administrativo]
@Tbl_Trabajador [dbo].[type_dataTrabajador] READONLY,
@I_Anio INT,
@I_Mes INT,
@I_CategoriaPlanillaID INT,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
	BEGIN TRAN

		IF (@I_Anio IS NULL) BEGIN
			RAISERROR('El año es obligatorio.', 11, 1);
		END

		IF (@I_Mes IS NULL) BEGIN
			RAISERROR('El mes es obligatorio.', 11, 1);
		END

		IF (@I_categoriaPlanillaID IS NULL) BEGIN
			RAISERROR('La categoría de planilla es obligatorio.', 11, 1);
		END

		IF (@I_UserID IS NULL) BEGIN
			RAISERROR('No se ha especificado el usuario que está realizando la acción.', 11, 1);
		END

		IF NOT EXISTS (SELECT pr.I_PeriodoID FROM dbo.TR_Periodo pr WHERE pr.I_Anio = @I_Anio AND pr.I_Mes = @I_Mes) BEGIN
			RAISERROR('No existe el periodo.', 11, 1);
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
				--Tipo de Conceptos
				--@I_NroOrden_TipoConceptos INT,
				--@I_Cant_TipoConceptos INT,
				--@I_TipoConceptoID INT,
				--@B_EsAdicion BIT,
				--@B_IncluirEnTotalBruto BIT,
				--Resumen por trabajador
				@I_TrabajadorID INT,
				@I_Filtro1 INT,
				@I_Filtro2 INT,
				@I_TrabajadorPlanillaID INT,
				@I_TotalRemuneracionTrabajador DECIMAL(15,2),
				@I_TotalDescuentoTrabajador DECIMAL(15,2),
				@I_TotalReintegroTrabajador DECIMAL(15,2),
				@I_TotalDeduccionTrabajador DECIMAL(15,2),
				@I_TotalSueldoTrabajador DECIMAL(15,2),
				--Detalle conceptos por trabajador
				@I_NroOrden INT,
				@I_CantConceptos INT,
				@I_ConceptoID INT,
				@C_ConceptoCod VARCHAR(20),
				@T_ConceptoDesc VARCHAR(250),
				@M_Monto DECIMAL(15,2),
				@B_MontoEstaAqui BIT,
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

		--DECLARE @tmp_tipo_concepto TABLE(I_NroOrden INT, I_TipoConceptoID INT, B_EsAdicion BIT, B_IncluirEnTotalBruto BIT);

		DECLARE @tmp_remuneracion TABLE(I_NroOrden INT, I_ConceptoID INT, C_ConceptoCod VARCHAR(20), T_ConceptoDesc VARCHAR(250), M_Monto DECIMAL(15,2), B_MontoEstaAqui BIT);

		DECLARE @tmp_descuento TABLE(I_NroOrden INT, I_ConceptoID INT, C_ConceptoCod VARCHAR(20), T_ConceptoDesc VARCHAR(250), M_Monto DECIMAL(15,2), B_MontoEstaAqui BIT);

		DECLARE @tmp_reintegro TABLE(I_NroOrden INT, I_ConceptoID INT, C_ConceptoCod VARCHAR(20), T_ConceptoDesc VARCHAR(250), M_Monto DECIMAL(15,2), B_MontoEstaAqui BIT);

		DECLARE @tmp_deduccion TABLE(I_NroOrden INT, I_ConceptoID INT, C_ConceptoCod VARCHAR(20), T_ConceptoDesc VARCHAR(250), M_Monto DECIMAL(15,2), B_MontoEstaAqui BIT);

		--1. Obtener valores para la cabecera de la planilla
		SET @I_PeriodoID = (SELECT pr.I_PeriodoID FROM dbo.TR_Periodo pr WHERE pr.I_Anio = @I_Anio AND pr.I_Mes = @I_Mes);

		SET @I_CorrelativoPlanilla = ISNULL((SELECT MAX(pl.I_Correlativo) FROM dbo.TR_Planilla pl 
			WHERE pl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID), 0) + 1;

		--2. Obtener los tipos de conceptos a usar
		--INSERT @tmp_tipo_concepto(I_NroOrden, I_TipoConceptoID, B_EsAdicion, B_IncluirEnTotalBruto)
		--SELECT ROW_NUMBER() OVER(ORDER BY tipcon.I_TipoConceptoID ASC), tipcon.I_TipoConceptoID, tipcon.B_EsAdicion, tipcon.B_IncluirEnTotalBruto FROM dbo.TC_TipoConcepto tipcon
		--WHERE tipcon.B_Habilitado = 1 AND tipcon.B_Eliminado = 0

		--SET @I_NroOrden_TipoConceptos = 1

		--SET @I_Cant_TipoConceptos = (SELECT COUNT(*) FROM @tmp_tipo_concepto)

		--3. Obtener la lista de trabajadores
		IF (@I_CategoriaPlanillaID = @I_ADMINISTRATIVOID) BEGIN
			INSERT #tmp_trabajador(I_TrabajadorID, I_Filtro1, I_Filtro2)
			SELECT adm.I_TrabajadorID, adm.I_GrupoOcupacionalID, adm.I_NivelRemunerativoID 
			FROM dbo.TC_Administrativo adm
			INNER JOIN dbo.TC_Trabajador trab ON trab.I_TrabajadorID = adm.I_TrabajadorID
			INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla tca ON tca.I_TrabajadorID = trab.I_TrabajadorID 
			INNER JOIN dbo.TC_GrupoOcupacional gr ON gr.I_GrupoOcupacionalID = adm.I_GrupoOcupacionalID
			INNER JOIN dbo.TC_NivelRemunerativo nivrem ON nivrem.I_NivelRemunerativoID = adm.I_NivelRemunerativoID
			INNER JOIN @Tbl_Trabajador tmp ON tmp.I_TrabajadorID = trab.I_TrabajadorID
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
			INNER JOIN @Tbl_Trabajador tmp ON tmp.I_TrabajadorID = trab.I_TrabajadorID
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
			--RETURN 0;
			RAISERROR('Todos los trabajadores seleccionados ya tienen una plantilla generada para el presente periodo.', 11, 1);
		END 

		--4. Crear la cabecera de la planilla
		INSERT dbo.TR_Planilla(I_PeriodoID, I_CategoriaPlanillaID, I_Correlativo, I_CantRegistros, 
			I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
		VALUES(@I_PeriodoID, @I_CategoriaPlanillaID, @I_CorrelativoPlanilla, @I_CantRegistros, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

		SET @I_PlantillaID = SCOPE_IDENTITY();

		--4. Recorrer cada trabajador
		WHILE (@I_Indicador <= @I_CantRegistros) BEGIN
		
			SELECT	@I_TrabajadorID = tmp.I_TrabajadorID,
					@I_Filtro1 = tmp.I_Filtro1,
					@I_Filtro2 = tmp.I_Filtro2
			FROM #tmp_trabajador tmp
			WHERE tmp.I_NroOrden = @I_Indicador;

			--5. Crear cabecera por trabajador
			INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorID, I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, 
				I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
			VALUES(@I_PlantillaID, @I_TrabajadorID, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

			SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();

			--6. Obtener REMUNERACIÓN
			SET @I_TotalRemuneracionTrabajador = 0;

			DELETE @tmp_remuneracion;

			INSERT @tmp_remuneracion(I_NroOrden, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_MontoEstaAqui)
			SELECT ROW_NUMBER() OVER(ORDER BY c.I_ConceptoID ASC), c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, ppc.M_Monto, ppc.B_MontoEstaAqui 
			FROM dbo.TI_PlantillaPlanilla pp
			INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
			INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
			WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
				pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Remunerativo;

			SET @I_NroOrden = 1

			SET @I_CantConceptos = (SELECT COUNT(*) FROM @tmp_remuneracion)

			WHILE (@I_NroOrden <= @I_CantConceptos)
			BEGIN
				SELECT	@I_ConceptoID = I_ConceptoID,
						@C_ConceptoCod = C_ConceptoCod,
						@T_ConceptoDesc = T_ConceptoDesc,
						@M_Monto = M_Monto,
						@B_MontoEstaAqui = B_MontoEstaAqui
				FROM @tmp_remuneracion
				WHERE I_NroOrden = @I_NroOrden

				IF (@B_MontoEstaAqui = 0) BEGIN
					SET @M_Monto = ISNULL((SELECT cmt.M_Monto FROM dbo.TI_MontoTrabajador mt 
						INNER JOIN dbo.TI_Concepto_MontoTrabajador cmt ON cmt.I_MontoTrabajadorID = mt.I_MontoTrabajadorID 
						WHERE mt.B_Habilitado = 1 AND mt.B_Eliminado = 0 AND cmt.B_Habilitado = 1 AND cmt.B_Eliminado = 0 AND
							mt.I_TrabajadorID = @I_TrabajadorID AND mt.I_PeriodoID = @I_PeriodoID AND cmt.I_ConceptoID = @I_ConceptoID), 0)
				END

				IF (@M_Monto IS NOT NULL AND @M_Monto > 0) BEGIN
					INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
					VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @M_Monto, 0, @I_UserID, @D_FecRegistro)

					SET @I_TotalRemuneracionTrabajador = @I_TotalRemuneracionTrabajador + @M_Monto
				END

				SET @I_NroOrden = @I_NroOrden + 1
			END

			--6. Obtener DESCUENTOS
			SET @I_TotalDescuentoTrabajador = 0;

			DELETE @tmp_descuento;

			INSERT @tmp_descuento(I_NroOrden, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_MontoEstaAqui)
			SELECT ROW_NUMBER() OVER(ORDER BY c.I_ConceptoID ASC), c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, ppc.M_Monto, ppc.B_MontoEstaAqui 
			FROM dbo.TI_PlantillaPlanilla pp
			INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
			INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
			WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
				pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Descuento;

			SET @I_NroOrden = 1

			SET @I_CantConceptos = (SELECT COUNT(*) FROM @tmp_descuento)

			WHILE (@I_NroOrden <= @I_CantConceptos)
			BEGIN
				SELECT	@I_ConceptoID = I_ConceptoID,
						@C_ConceptoCod = C_ConceptoCod,
						@T_ConceptoDesc = T_ConceptoDesc,
						@M_Monto = M_Monto,
						@B_MontoEstaAqui = B_MontoEstaAqui
				FROM @tmp_descuento
				WHERE I_NroOrden = @I_NroOrden

				IF (@B_MontoEstaAqui = 0) BEGIN
					SET @M_Monto = ISNULL((SELECT cmt.M_Monto FROM dbo.TI_MontoTrabajador mt 
						INNER JOIN dbo.TI_Concepto_MontoTrabajador cmt ON cmt.I_MontoTrabajadorID = mt.I_MontoTrabajadorID 
						WHERE mt.B_Habilitado = 1 AND mt.B_Eliminado = 0 AND cmt.B_Habilitado = 1 AND cmt.B_Eliminado = 0 AND
							mt.I_TrabajadorID = @I_TrabajadorID AND mt.I_PeriodoID = @I_PeriodoID AND cmt.I_ConceptoID = @I_ConceptoID), 0)
				END

				IF (@M_Monto IS NOT NULL AND @M_Monto > 0) BEGIN
					INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
					VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @M_Monto, 0, @I_UserID, @D_FecRegistro)

					SET @I_TotalDescuentoTrabajador = @I_TotalDescuentoTrabajador + @M_Monto
				END

				SET @I_NroOrden = @I_NroOrden + 1
			END

			--7. Obtener REINTEGRO
			SET @I_TotalReintegroTrabajador = 0;

			DELETE @tmp_reintegro;

			INSERT @tmp_reintegro(I_NroOrden, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_MontoEstaAqui)
			SELECT ROW_NUMBER() OVER(ORDER BY c.I_ConceptoID ASC), c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, ppc.M_Monto, ppc.B_MontoEstaAqui 
			FROM dbo.TI_PlantillaPlanilla pp
			INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
			INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
			WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
				pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Reintegro;

			SET @I_NroOrden = 1

			SET @I_CantConceptos = (SELECT COUNT(*) FROM @tmp_reintegro)

			WHILE (@I_NroOrden <= @I_CantConceptos)
			BEGIN
				SELECT	@I_ConceptoID = I_ConceptoID,
						@C_ConceptoCod = C_ConceptoCod,
						@T_ConceptoDesc = T_ConceptoDesc,
						@M_Monto = M_Monto,
						@B_MontoEstaAqui = B_MontoEstaAqui
				FROM @tmp_reintegro
				WHERE I_NroOrden = @I_NroOrden

				IF (@B_MontoEstaAqui = 0) BEGIN
					SET @M_Monto = ISNULL((SELECT cmt.M_Monto FROM dbo.TI_MontoTrabajador mt 
						INNER JOIN dbo.TI_Concepto_MontoTrabajador cmt ON cmt.I_MontoTrabajadorID = mt.I_MontoTrabajadorID 
						WHERE mt.B_Habilitado = 1 AND mt.B_Eliminado = 0 AND cmt.B_Habilitado = 1 AND cmt.B_Eliminado = 0 AND
							mt.I_TrabajadorID = @I_TrabajadorID AND mt.I_PeriodoID = @I_PeriodoID AND cmt.I_ConceptoID = @I_ConceptoID), 0)
				END

				IF (@M_Monto IS NOT NULL AND @M_Monto > 0) BEGIN
					INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
					VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @M_Monto, 0, @I_UserID, @D_FecRegistro)

					SET @I_TotalReintegroTrabajador = @I_TotalReintegroTrabajador + @M_Monto
				END

				SET @I_NroOrden = @I_NroOrden + 1
			END

			--8. Obtener DEDUCCIONES
			SET @I_TotalDeduccionTrabajador = 0;

			DELETE @tmp_deduccion;

			INSERT @tmp_deduccion(I_NroOrden, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_MontoEstaAqui)
			SELECT ROW_NUMBER() OVER(ORDER BY c.I_ConceptoID ASC), c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, ppc.M_Monto, ppc.B_MontoEstaAqui 
			FROM dbo.TI_PlantillaPlanilla pp
			INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
			INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
			WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
				pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Deduccion;

			SET @I_NroOrden = 1

			SET @I_CantConceptos = (SELECT COUNT(*) FROM @tmp_deduccion)

			WHILE (@I_NroOrden <= @I_CantConceptos)
			BEGIN
				SELECT	@I_ConceptoID = I_ConceptoID,
						@C_ConceptoCod = C_ConceptoCod,
						@T_ConceptoDesc = T_ConceptoDesc,
						@M_Monto = M_Monto,
						@B_MontoEstaAqui = B_MontoEstaAqui
				FROM @tmp_deduccion
				WHERE I_NroOrden = @I_NroOrden

				IF (@B_MontoEstaAqui = 0) BEGIN
					SET @M_Monto = ISNULL((SELECT cmt.M_Monto FROM dbo.TI_MontoTrabajador mt 
						INNER JOIN dbo.TI_Concepto_MontoTrabajador cmt ON cmt.I_MontoTrabajadorID = mt.I_MontoTrabajadorID 
						WHERE mt.B_Habilitado = 1 AND mt.B_Eliminado = 0 AND cmt.B_Habilitado = 1 AND cmt.B_Eliminado = 0 AND
							mt.I_TrabajadorID = @I_TrabajadorID AND mt.I_PeriodoID = @I_PeriodoID AND cmt.I_ConceptoID = @I_ConceptoID), 0)
				END

				IF (@M_Monto IS NOT NULL AND @M_Monto > 0) BEGIN
					INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
					VALUES(@I_TrabajadorPlanillaID, @I_ConceptoID, @C_ConceptoCod, @T_ConceptoDesc, @M_Monto, 0, @I_UserID, @D_FecRegistro)

					SET @I_TotalDeduccionTrabajador = @I_TotalDeduccionTrabajador + @M_Monto
				END

				SET @I_NroOrden = @I_NroOrden + 1
			END

			--9. Actualizar totales por trabajador
			SET @I_TotalSueldoTrabajador = @I_TotalRemuneracionTrabajador - @I_TotalDescuentoTrabajador + @I_TotalReintegroTrabajador - @I_TotalDeduccionTrabajador;

			UPDATE  dbo.TR_TrabajadorPlanilla SET 
				I_TotalRemuneracion = @I_TotalRemuneracionTrabajador,
				I_TotalDescuento = @I_TotalDescuentoTrabajador,
				I_TotalReintegro = @I_TotalReintegroTrabajador,
				I_TotalDeduccion = @I_TotalDeduccionTrabajador,
				I_TotalSueldo = @I_TotalSueldoTrabajador
			WHERE I_TrabajadorPlanillaID = @I_TrabajadorPlanillaID

			--10. Acumular para el resumen total de la planilla
			SET @I_TotalRemuneracion = @I_TotalRemuneracion + @I_TotalRemuneracionTrabajador;

			SET @I_TotalDescuento = @I_TotalDescuento + @I_TotalDescuentoTrabajador;

			SET @I_TotalReintegro = @I_TotalReintegro + @I_TotalReintegroTrabajador;

			SET @I_TotalDeduccion = @I_TotalDeduccion + @I_TotalDeduccionTrabajador;

			SET @I_TotalSueldo = @I_TotalSueldo + @I_TotalSueldoTrabajador;

			SET @I_Indicador = @I_Indicador + 1;
		END

		--11. Actualizar totales de planilla
		UPDATE dbo.TR_Planilla SET 
			I_TotalRemuneracion = @I_TotalRemuneracion, 
			I_TotalDescuento = @I_TotalDescuento, 
			I_TotalReintegro = @I_TotalReintegro, 
			I_TotalDeduccion = @I_TotalDeduccion,
			I_TotalSueldo = @I_TotalSueldo
		WHERE I_PlanillaID = @I_PlantillaID;

		COMMIT TRAN

		SET @B_Result = 1
		SET @T_Message = 'Planilla generada correctamente.'
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		DECLARE @ERROR_MESSAGE NVARCHAR(4000) = ERROR_MESSAGE(),
				@ERROR_SEVERITY INT = ERROR_SEVERITY(),
				@ERROR_STATE INT = ERROR_STATE()

		RAISERROR (@ERROR_MESSAGE, @ERROR_SEVERITY, @ERROR_STATE);
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
@I_PlantillaPlanillaID INT,
@I_ConceptoID INT,
@B_EsMontoFijo BIT,
@B_MontoEstaAqui BIT,
@M_Monto DECIMAL(15,2) = NULL,
@B_AplicarFiltro1 BIT = NULL,
@I_Filtro1 INT = NULL,
@B_AplicarFiltro2 BIT = NULL,
@I_Filtro2 INT = NULL,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado, I_UsuarioCre, D_FecCre)
		VALUES(@I_PlantillaPlanillaID, @I_ConceptoID, @B_EsMontoFijo, @B_MontoEstaAqui, @M_Monto, @B_AplicarFiltro1, @I_Filtro1, @B_AplicarFiltro2, @I_Filtro2, 1, 0, @I_UserID, GETDATE())

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
@I_PlantillaPlanillaConceptoID INT,
@I_PlantillaPlanillaID INT,
@I_ConceptoID INT,
@B_EsMontoFijo BIT,
@B_MontoEstaAqui BIT,
@M_Monto DECIMAL(15,2) = NULL,
@B_AplicarFiltro1 BIT = NULL,
@I_Filtro1 INT = NULL,
@B_AplicarFiltro2 BIT = NULL,
@I_Filtro2 INT = NULL,
@I_UserID INT,
@B_Result BIT OUTPUT,
@T_Message VARCHAR(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRAN
	BEGIN TRY
		UPDATE dbo.TI_PlantillaPlanilla_Concepto SET
			I_PlantillaPlanillaID = @I_PlantillaPlanillaID,
			I_ConceptoID = @I_ConceptoID,
			B_EsMontoFijo = @B_EsMontoFijo,
			B_MontoEstaAqui = @B_MontoEstaAqui,
			M_Monto = @M_Monto,
			B_AplicarFiltro1 = @B_AplicarFiltro1,
			I_Filtro1 = @I_Filtro1,
			B_AplicarFiltro2 = @B_AplicarFiltro2,
			I_Filtro2 = @I_Filtro2,
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
