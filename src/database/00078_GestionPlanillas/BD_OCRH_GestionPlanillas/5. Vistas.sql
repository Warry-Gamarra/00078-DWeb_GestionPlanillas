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


--Administrativo
SELECT        cap.T_CategoriaPlanillaDesc, per.I_Anio, per.T_MesDesc, pl.I_Correlativo, p.T_Nombre, p.T_ApellidoPaterno, p.T_ApellidoMaterno, nvr.C_NivelRemunerativoCod, 
                         grup.C_GrupoOcupacionalCod, ctpl.C_ConceptoCod, ctpl.T_ConceptoDesc, ctpl.M_Monto
FROM            TR_Planilla AS pl INNER JOIN
                         TR_TrabajadorPlanilla AS tpl ON pl.I_PlanillaID = tpl.I_PlanillaID INNER JOIN
                         TR_Concepto_TrabajadorPlanilla AS ctpl ON tpl.I_TrabajadorPlanillaID = ctpl.I_TrabajadorPlanillaID INNER JOIN
                         TC_CategoriaPlanilla AS cap ON cap.I_CategoriaPlanillaID = pl.I_CategoriaPlanillaID INNER JOIN
                         TC_Trabajador AS t ON tpl.I_TrabajadorID = t.I_TrabajadorID INNER JOIN
                         TC_Administrativo AS adm ON t.I_TrabajadorID = adm.I_TrabajadorID INNER JOIN
                         TC_Persona AS p ON t.I_PersonaID = p.I_PersonaID INNER JOIN
                         TR_Periodo AS per ON pl.I_PeriodoID = per.I_PeriodoID INNER JOIN
                         TC_NivelRemunerativo AS nvr ON adm.I_NivelRemunerativoID = nvr.I_NivelRemunerativoID INNER JOIN
                         TC_GrupoOcupacional AS grup ON adm.I_GrupoOcupacionalID = grup.I_GrupoOcupacionalID
WHERE pl.I_CategoriaPlanillaID = 1 AND per.I_Anio = 2022 AND per.I_Mes = 4
GO