USE BD_OCRH_GestionPlanillas
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_Trabajadores')
	DROP VIEW [dbo].[VW_Trabajadores]
GO

CREATE VIEW [dbo].[VW_Trabajadores]
AS
SELECT 
	trab.I_TrabajadorID, trab.C_TrabajadorCod, per.T_Nombre, per.T_ApellidoPaterno, per.T_ApellidoMaterno, tipdoc.I_TipoDocumentoID, tipdoc.T_TipoDocumentoDesc, per.C_NumDocumento, 
	trab.D_FechaIngreso, reg.I_RegimenID, reg.T_RegimenDesc, est.I_EstadoID, est.T_EstadoDesc, vin.I_VinculoID, vin.T_VinculoDesc
FROM dbo.TC_Trabajador AS trab INNER JOIN
	dbo.TC_Persona AS per ON per.I_PersonaID = trab.I_PersonaID INNER JOIN
	dbo.TC_TipoDocumento AS tipdoc ON tipdoc.I_TipoDocumentoID = per.I_TipoDocumentoID LEFT JOIN
	dbo.TC_Regimen AS reg ON reg.I_RegimenID = trab.I_RegimenID INNER JOIN 
	dbo.TC_Estado AS est ON est.I_EstadoID = trab.I_EstadoID INNER JOIN 
	dbo.TC_Vinculo AS vin ON vin.I_VinculoID = trab.I_VinculoID
WHERE trab.B_Eliminado = 0 AND per.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_Docentes')
	DROP VIEW [dbo].[VW_Docentes]
GO

CREATE VIEW [dbo].[VW_Docentes]
AS
SELECT 
	trab.I_TrabajadorID, trab.C_TrabajadorCod, trab.T_Nombre, trab.T_ApellidoPaterno, trab.T_ApellidoMaterno, trab.I_TipoDocumentoID, trab.T_TipoDocumentoDesc, trab.C_NumDocumento, 
	trab.D_FechaIngreso, trab.I_RegimenID, trab.T_RegimenDesc, trab.I_EstadoID, trab.T_EstadoDesc, trab.I_VinculoID, trab.T_VinculoDesc,
	doc.I_DocenteID, catdoc.I_CategoriaDocenteID, catdoc.C_CategoriaDocenteCod, catdoc.T_CategoriaDocenteDesc,
	hdoc.I_HorasDocenteID, hdoc.I_Horas, dedoc.I_DedicacionDocenteID, dedoc.C_DedicacionDocenteCod, dedoc.T_DedicacionDocenteDesc
FROM dbo.VW_Trabajadores AS trab INNER JOIN
	dbo.TC_Docente AS doc ON doc.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
	dbo.TC_CategoriaDocente AS catdoc ON catdoc.I_CategoriaDocenteID = doc.I_CategoriaDocenteID INNER JOIN
	dbo.TC_HorasDocente AS hdoc ON hdoc.I_HorasDocenteID = doc.I_HorasDocenteID INNER JOIN
	dbo.TC_DedicacionDocente dedoc ON dedoc.I_DedicacionDocenteID = hdoc.I_DedicacionDocenteID
WHERE doc.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_Administrativo')
	DROP VIEW [dbo].[VW_Administrativo]
GO

CREATE VIEW [dbo].[VW_Administrativo]
AS
SELECT 
	trab.I_TrabajadorID, trab.C_TrabajadorCod, trab.T_Nombre, trab.T_ApellidoPaterno, trab.T_ApellidoMaterno, trab.I_TipoDocumentoID, trab.T_TipoDocumentoDesc, trab.C_NumDocumento, 
	trab.D_FechaIngreso, trab.I_RegimenID, trab.T_RegimenDesc, trab.I_EstadoID, trab.T_EstadoDesc, trab.I_VinculoID, trab.T_VinculoDesc,
	adm.I_AdministrativoID, grup.I_GrupoOcupacionalID, grup.C_GrupoOcupacionalCod, grup.T_GrupoOcupacionalDesc, 
	nivrem.I_NivelRemunerativoID, nivrem.C_NivelRemunerativoCod, nivrem.T_NivelRemunerativoDesc
FROM dbo.VW_Trabajadores AS trab INNER JOIN
	dbo.TC_Administrativo AS adm ON adm.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
	dbo.TC_GrupoOcupacional AS grup ON grup.I_GrupoOcupacionalID = adm.I_GrupoOcupacionalID INNER JOIN
	dbo.TC_NivelRemunerativo AS nivrem ON nivrem.I_NivelRemunerativoID = adm.I_NivelRemunerativoID
WHERE adm.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_ResumenPlanillaTrabajador')
	DROP VIEW [dbo].[VW_ResumenPlanillaTrabajador]
GO

CREATE VIEW [dbo].[VW_ResumenPlanillaTrabajador]
AS
SELECT
	trab.I_TrabajadorID, trab.C_TrabajadorCod, trab.T_Nombre, trab.T_ApellidoPaterno, trab.T_ApellidoMaterno, trab.I_TipoDocumentoID, trab.T_TipoDocumentoDesc, trab.C_NumDocumento, 
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




SELECT * FROM dbo.TC_TipoDocumento
SELECT * FROM dbo.TC_Banco





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

--Docente
SELECT        cap.T_CategoriaPlanillaDesc, per.I_Anio, per.T_MesDesc, pl.I_Correlativo, p.T_Nombre, p.T_ApellidoPaterno, p.T_ApellidoMaterno, cd.C_CategoriaDocenteCod, 
                         dd.C_DedicacionDocenteCod, hd.I_Horas, ctpl.C_ConceptoCod, ctpl.T_ConceptoDesc, ctpl.M_Monto
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
WHERE pl.I_CategoriaPlanillaID = 2 AND per.I_Anio = 2022 AND per.I_Mes = 4
GO



--delete from TR_Concepto_TrabajadorPlanilla
--delete from TR_TrabajadorPlanilla
--delete from TR_Planilla


