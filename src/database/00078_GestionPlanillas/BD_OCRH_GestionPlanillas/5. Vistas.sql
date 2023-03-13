USE BD_OCRH_GestionPlanillas
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_Trabajadores')
	DROP VIEW [dbo].[VW_Trabajadores]
GO


CREATE VIEW [dbo].[VW_Trabajadores]
AS
SELECT 
	trab.I_TrabajadorID, per.T_Nombre, per.T_ApellidoPaterno, per.T_ApellidoMaterno, tipdoc.I_TipoDocumentoID, tipdoc.T_TipoDocumentoDesc, per.C_NumDocumento, 
	trab.D_FechaIngreso, reg.I_RegimenID, reg.T_RegimenDesc, est.I_EstadoID, est.T_EstadoDesc, vin.I_VinculoID, vin.T_VinculoDesc
FROM dbo.TC_Trabajador AS trab INNER JOIN
	dbo.TC_Persona AS per ON per.I_PersonaID = trab.I_PersonaID INNER JOIN
	dbo.TC_TipoDocumento AS tipdoc ON tipdoc.I_TipoDocumentoID = per.I_TipoDocumentoID LEFT JOIN
	dbo.TC_Regimen AS reg ON reg.I_RegimenID = trab.I_RegimenID INNER JOIN 
	dbo.TC_Estado AS est ON est.I_EstadoID = trab.I_EstadoID INNER JOIN 
	dbo.TC_Vinculo AS vin ON vin.I_VinculoID = trab.I_VinculoID
WHERE trab.B_Eliminado = 0 AND per.B_Eliminado = 0
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_ResumenPlanillaTrabajador')
	DROP VIEW [dbo].[VW_ResumenPlanillaTrabajador]
GO


CREATE VIEW [dbo].[VW_ResumenPlanillaTrabajador]
AS
SELECT
	trab.I_TrabajadorID, trab.T_Nombre, trab.T_ApellidoPaterno, trab.T_ApellidoMaterno, trab.I_TipoDocumentoID, trab.T_TipoDocumentoDesc, trab.C_NumDocumento, 
	trab.D_FechaIngreso, trab.I_RegimenID, trab.T_RegimenDesc, trab.I_EstadoID, trab.T_EstadoDesc, trab.I_VinculoID, trab.T_VinculoDesc,
	trabpla.I_TrabajadorPlanillaID, trabpla.I_TotalRemuneracion, trabpla.I_TotalDescuento, trabpla.I_TotalReintegro, trabpla.I_TotalDeduccion, trabpla.I_TotalSueldo,
	pla.I_PlanillaID, pla.I_PeriodoID, per.I_Anio, per.T_MesDesc, pla.I_CategoriaPlanillaID, catpla.T_CategoriaPlanillaDesc
FROM dbo.VW_Trabajadores AS trab INNER JOIN
	dbo.TR_TrabajadorPlanilla AS trabpla ON trabpla.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
	dbo.TR_Planilla AS pla ON pla.I_PlanillaID = trabpla.I_PlanillaID INNER JOIN
	dbo.TR_Periodo AS per ON per.I_PeriodoID = pla.I_PeriodoID INNER JOIN
	dbo.TC_CategoriaPlanilla AS catpla ON catpla.I_CategoriaPlanillaID = pla.I_CategoriaPlanillaID
WHERE trabpla.B_Anulado = 0 AND pla.B_Anulado = 0
GO

SELECT * FROM VW_ResumenPlanillaTrabajador