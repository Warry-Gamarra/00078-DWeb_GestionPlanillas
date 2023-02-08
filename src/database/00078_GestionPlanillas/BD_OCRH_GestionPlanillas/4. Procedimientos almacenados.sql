USE [BD_OCRH_GestionPlanillas]
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.DOMAINS WHERE DOMAIN_NAME = 'type_dataTrabajador') BEGIN
	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_GenerarPlanillaDocente') BEGIN
		DROP PROCEDURE [dbo].[USP_I_GenerarPlanillaDocente]
	END

	DROP TYPE [dbo].[type_dataTrabajador]
END
GO

CREATE TYPE [dbo].[type_dataTrabajador] AS TABLE(
	I_TrabajadorID int
)
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'USP_I_GenerarPlanillaDocente')
	DROP PROCEDURE [dbo].[USP_I_GenerarPlanillaDocente]
GO

--Pregunta: La planilla de haberes se dibide por administrativo, cas y docente, o los 3 están en la misma planilla?
CREATE PROCEDURE [dbo].[USP_I_GenerarPlanillaDocente]
@Tbl_Docentes [dbo].[type_dataTrabajador] READONLY,
@I_Anio INT,
@I_Mes INT,
@I_CategoriaPlanillaID INT,
@I_UserID INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
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
				@I_Correlativo INT,
				@I_Indicador INT,
				@I_CantRegistros INT,
				@I_PlantillaID INT,
				--Resumen cabecera planilla
				@I_TotalRemuneracion DECIMAL(15,2) = 0,
				@I_TotalDescuento DECIMAL(15,2) = 0,
				@I_TotalReintegro DECIMAL(15,2) = 0,
				@I_TotalDeduccion DECIMAL(15,2) = 0,
				@I_TotalSueldo DECIMAL(15,2) = 0,
				--Resumen por trabajador
				@I_TrabajadorID INT,
				@I_CategoriaDocenteID INT,
				@I_HorasDocenteID INT,
				@I_TrabajadorPlanillaID INT,
				@I_TotalRemuneracionTrabajador DECIMAL(15,2),
				@I_TotalDescuentoTrabajador DECIMAL(15,2),
				@I_TotalReintegroTrabajador DECIMAL(15,2),
				@I_TotalDeduccionTrabajador DECIMAL(15,2),
				@I_TotalSueldoTrabajador DECIMAL(15,2),
				--Variables generales
				@D_FecRegistro DATETIME  = GETDATE(),
				@I_Remunerativo INT = 1,
				@I_Descuento INT = 2,
				@I_Reintegro INT = 3,
				@I_Deduccion INT = 4

		CREATE TABLE #tmp_docentes
		(
			I_NroOrden INT IDENTITY(1,1),
			I_TrabajadorID INT NOT NULL,
			I_CategoriaDocenteID INT NOT NULL,
			I_HorasDocenteID INT NOT NULL
		);

		--1. Obtener valores para la cabecera de la planilla
		SET @I_PeriodoID = (SELECT pr.I_PeriodoID FROM dbo.TR_Periodo pr WHERE pr.I_Anio = @I_Anio AND pr.I_Mes = @I_Mes);

		SET @I_Correlativo = ISNULL((SELECT MAX(pl.I_Correlativo) FROM dbo.TR_Planilla pl 
			WHERE pl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID), 0) + 1;

		INSERT #tmp_docentes(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID)
		SELECT d.I_TrabajadorID, cd.I_CategoriaDocenteID, hd.I_HorasDocenteID FROM dbo.TC_Docente d
		INNER JOIN dbo.TC_Trabajador t ON t.I_TrabajadorID = d.I_TrabajadorID
		INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla tca ON tca.I_TrabajadorID = t.I_TrabajadorID 
		INNER JOIN dbo.TC_CategoriaDocente cd ON cd.I_CategoriaDocenteID = d.I_CategoriaDocenteID
		INNER JOIN dbo.TC_HorasDocente hd ON hd.I_HorasDocenteID = d.I_HorasDocenteID
		INNER JOIN @Tbl_Docentes tmp ON tmp.I_TrabajadorID = d.I_TrabajadorID
		WHERE t.B_Habilitado = 1 AND t.B_Eliminado = 0 AND 
			d.B_Habilitado = 1 AND d.B_Eliminado = 0 AND 
			tca.B_Habilitado = 1 AND tca.B_Eliminado = 0 AND tca.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND
			NOT EXISTS(SELECT pl.I_PlanillaID FROM dbo.TR_Planilla pl 
				INNER JOIN dbo.TR_TrabajadorPlanilla tpl ON tpl.I_PlanillaID = pl.I_PlanillaID 
				WHERE tpl.B_Anulado = 0 AND pl.I_PeriodoID = @I_PeriodoID AND pl.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND tpl.I_TrabajadorID = t.I_TrabajadorID);--**Falta confirmar si se repite el trabajador en un mismo mes en una misma planilla

		SET @I_Indicador = 1;

		SET @I_CantRegistros = (SELECT COUNT(*) FROM #tmp_docentes);

		IF (@I_CantRegistros = 0) BEGIN
			RETURN 0;
		END 

		--2. Crear la cabecera de la planilla
		INSERT dbo.TR_Planilla(I_PeriodoID, I_CategoriaPlanillaID, I_Correlativo, I_CantRegistros, 
			I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
		VALUES(@I_PeriodoID, @I_CategoriaPlanillaID, @I_Correlativo, @I_CantRegistros, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

		SET @I_PlantillaID = SCOPE_IDENTITY();

		--3. Recorrer cada trabajador
		WHILE (@I_Indicador <= @I_CantRegistros) BEGIN
		
			SELECT	@I_TrabajadorID = tmp.I_TrabajadorID,
					@I_CategoriaDocenteID = tmp.I_CategoriaDocenteID,
					@I_HorasDocenteID = tmp.I_HorasDocenteID
			FROM #tmp_docentes tmp
			WHERE tmp.I_NroOrden = @I_Indicador;

			--4. Crear cabecera por trabajador
			INSERT dbo.TR_TrabajadorPlanilla(I_PlanillaID, I_TrabajadorID, I_TotalRemuneracion, I_TotalDescuento, I_TotalReintegro, I_TotalDeduccion, 
				I_TotalSueldo, B_Anulado, I_UsuarioCre, D_FecCre)
			VALUES(@I_PlantillaID, @I_TrabajadorID, 0, 0, 0, 0, 0, 0, @I_UserID, @D_FecRegistro);

			SET @I_TrabajadorPlanillaID = SCOPE_IDENTITY();

			--5. Obtener REMUNERACIONES
			INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
			SELECT @I_TrabajadorPlanillaID, ppc.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, ppc.M_Monto, 0, @I_UserID, @D_FecRegistro FROM dbo.TI_PlantillaPlanilla pp
			INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
			INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
			WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
				pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Remunerativo;

			SET @I_TotalRemuneracionTrabajador = (SELECT SUM(ppc.M_Monto) from dbo.TI_PlantillaPlanilla pp
				INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
				INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
				WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
					pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Remunerativo);

			--6. Obtener DESCUENTOS
			INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
			SELECT @I_TrabajadorPlanillaID, ppc.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, ppc.M_Monto, 0, @I_UserID, @D_FecRegistro FROM dbo.TI_PlantillaPlanilla pp
			INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
			INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
			WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
				pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Descuento;

			SET @I_TotalDescuentoTrabajador = (SELECT SUM(ppc.M_Monto) FROM dbo.TI_PlantillaPlanilla pp
				INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
				INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
				WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
					pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Descuento);

			--7. Obtener REINTEGRO
			INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
			SELECT @I_TrabajadorPlanillaID, ppc.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, ppc.M_Monto, 0, @I_UserID, @D_FecRegistro FROM dbo.TI_PlantillaPlanilla pp
			INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
			INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
			WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
				pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Reintegro;

			SET @I_TotalReintegroTrabajador = (SELECT SUM(ppc.M_Monto) FROM dbo.TI_PlantillaPlanilla pp
				INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
				INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
				WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
					pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Reintegro);

			--8. Obtener DEDUCCIONES
			INSERT dbo.TR_Concepto_TrabajadorPlanilla(I_TrabajadorPlanillaID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, B_Anulado, I_UsuarioCre, D_FecCre)
			SELECT @I_TrabajadorPlanillaID, ppc.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, ppc.M_Monto, 0, @I_UserID, @D_FecRegistro FROM dbo.TI_PlantillaPlanilla pp
			INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
			INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
			WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
				pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Deduccion;

			SET @I_TotalDeduccionTrabajador = (SELECT SUM(ppc.M_Monto) FROM dbo.TI_PlantillaPlanilla pp
				INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc ON ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
				INNER JOIN dbo.TC_Concepto c ON c.I_ConceptoID = ppc.I_ConceptoID
				WHERE pp.B_Habilitado = 1  AND pp.B_Eliminado = 0 AND ppc.B_Habilitado = 1 AND ppc.B_Eliminado = 0 AND 
					pp.I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND c.I_TipoConceptoID = @I_Deduccion);

			SET @I_TotalSueldoTrabajador = @I_TotalRemuneracionTrabajador - @I_TotalDescuentoTrabajador + @I_TotalReintegroTrabajador - @I_TotalDeduccionTrabajador;

			--9. Actualizar totales por trabajador
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
	END TRY
	BEGIN CATCH
		DECLARE @ERROR_MESSAGE NVARCHAR(4000) = ERROR_MESSAGE(),
				@ERROR_SEVERITY INT = ERROR_SEVERITY(),
				@ERROR_STATE INT = ERROR_STATE()
   
		RAISERROR (@ERROR_MESSAGE, @ERROR_SEVERITY, @ERROR_STATE);
	END CATCH
END
GO




--Ejecución SP
DECLARE @tmp_docentes AS type_dataTrabajador,
		@I_Anio INT = 2022,
		@I_Mes INT = 1,
		@I_CategoriaPlanillaID INT = 2,
		@I_UserID INT = 1,
		@return_status INT

INSERT @tmp_docentes(I_TrabajadorID) VALUES(1), (2), (3)

EXEC @return_status = USP_I_GenerarPlanillaDocente @Tbl_Docentes = @tmp_docentes, @I_Anio = @I_Anio, @I_Mes = @I_Mes, @I_CategoriaPlanillaID = @I_CategoriaPlanillaID, @I_UserID = @I_UserID

SELECT 'return_status' = @return_status
GO


--Consutlas


SELECT * FROM dbo.TR_Planilla

SELECT * FROM dbo.TR_TrabajadorPlanilla

SELECT * FROM dbo.TR_Concepto_TrabajadorPlanilla

SELECT        cap.T_CategoriaPlanillaDesc, per.I_Anio, per.T_MesDesc, pl.I_Correlativo, p.T_Nombre, p.T_ApellidoPaterno, p.T_ApellidoMaterno, cd.C_CategoriaDocenteCod, 
                         dd.C_DedicacionDocenteCod, hd.I_Horas, ctpl.C_ConceptoCod, ctpl.T_ConceptoDesc
FROM            TR_Planilla AS pl INNER JOIN
                         TR_TrabajadorPlanilla AS tpl ON pl.I_PlanillaID = tpl.I_PlanillaID INNER JOIN
                         TR_Concepto_TrabajadorPlanilla AS ctpl ON tpl.I_TrabajadorPlanillaID = ctpl.I_TrabajadorPlanillaID INNER JOIN
                         TC_CategoriaPlanilla AS cap ON cap.I_CategoriaPlanillaID = pl.I_CategoriaPlanillaID INNER JOIN
                         TC_Trabajador AS t ON tpl.I_TrabajadorID = t.I_TrabajadorID INNER JOIN
                         TC_Docente AS doc ON t.I_TrabajadorID = doc.I_TrabajadorID INNER JOIN
                         TC_Persona AS p ON t.I_PersonaID = p.I_PersonaID INNER JOIN
                         TR_Periodo AS per ON pl.I_PeriodoID = per.I_PeriodoID INNER JOIN
                         TC_CategoriaDocente AS cd ON doc.I_CategoriaDocenteID = cd.I_CategoriaDocenteID INNER JOIN
                         TC_HorasDocente AS hd ON doc.I_HorasDocenteID = hd.I_HorasDocenteID INNER JOIN
                         TC_DedicacionDocente AS dd ON hd.I_DedicacionDocenteID = dd.I_DedicacionDocenteID
GO

--DELETE TR_Concepto_TrabajadorPlanilla
--DELETE TR_TrabajadorPlanilla
--DELETE TR_Planilla
--DBCC CHECKIDENT (TR_TrabajadorPlanilla, RESEED, 0)
--DBCC CHECKIDENT (TR_Planilla, RESEED, 0)


select * from dbo.TC_TipoPlanilla
select * from dbo.TC_Concepto

