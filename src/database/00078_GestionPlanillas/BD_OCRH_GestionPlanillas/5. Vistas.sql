USE BD_OCRH_GestionPlanillas
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_Trabajadores')
	DROP VIEW [dbo].[VW_Trabajadores]
GO

CREATE VIEW [dbo].[VW_Trabajadores]
AS
SELECT 
	trab.I_TrabajadorID, trab.C_TrabajadorCod, per.I_PersonaID, per.T_Nombre, per.T_ApellidoPaterno, per.T_ApellidoMaterno, 
	tipdoc.I_TipoDocumentoID, tipdoc.T_TipoDocumentoDesc, per.C_NumDocumento, 
	trab.D_FechaIngreso, reg.I_RegimenID, reg.T_RegimenDesc, afp.I_AfpID, afp.T_AfpDesc, trab.T_Cuspp, 
	est.I_EstadoID, est.T_EstadoDesc, vin.I_VinculoID, vin.T_VinculoDesc,
	trabdep.I_TrabajadorDependenciaID, dep.I_DependenciaID, dep.C_DependenciaCod, dep.T_DependenciaDesc,
	cta.I_CuentaBancariaID, cta.T_NroCuentaBancaria, bco.I_BancoID, bco.T_BancoDesc, bco.T_BancoAbrv
FROM dbo.TC_Trabajador AS trab INNER JOIN
	dbo.TC_Persona AS per ON per.I_PersonaID = trab.I_PersonaID INNER JOIN
	dbo.TC_TipoDocumento AS tipdoc ON tipdoc.I_TipoDocumentoID = per.I_TipoDocumentoID LEFT JOIN
	dbo.TC_Regimen AS reg ON reg.I_RegimenID = trab.I_RegimenID LEFT JOIN 
	dbo.TC_Afp AS afp ON afp.I_AfpID = trab.I_AfpID INNER JOIN
	dbo.TC_Estado AS est ON est.I_EstadoID = trab.I_EstadoID INNER JOIN 
	dbo.TC_Vinculo AS vin ON vin.I_VinculoID = trab.I_VinculoID LEFT JOIN
	dbo.TC_Trabajador_Dependencia AS trabdep ON trabdep.I_TrabajadorID = trab.I_TrabajadorID AND trabdep.B_Habilitado = 1 LEFT JOIN
	dbo.TC_Dependencia AS dep ON dep.I_DependenciaID = trabdep.I_DependenciaID LEFT JOIN
	dbo.TC_CuentaBancaria AS cta ON cta.I_TrabajadorID = trab.I_TrabajadorID AND cta.B_Habilitado = 1 LEFT JOIN
	dbo.TC_Banco AS bco ON bco.I_BancoID = cta.I_BancoID
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




--PLANILLA Administrativo
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

--PLANILLA Docente
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



--Lista de tipos de planilla
select p.T_TipoPlanillaDesc, c2.T_ClasePlanillaDesc, c1.T_CategoriaPlanillaDesc from dbo.TC_CategoriaPlanilla c1
inner join dbo.TC_ClasePlanilla c2 ON c1.I_ClasePlanillaID = c2.I_ClasePlanillaID
inner join dbo.TC_TipoPlanilla p ON p.I_TipoPlanillaID = c2.I_TipoPlanillaID
order by 1

--Lista de conceptos
SELECT pp.T_PlantillaPlanillaDesc, cp.T_CategoriaPlanillaDesc, t.T_TipoConceptoDesc, c.I_ConceptoID, c.T_ConceptoDesc, ppc.B_MontoEstaAqui, ppc.M_Monto, ppc.I_Filtro1, ppc.I_Filtro2
FROM dbo.TI_PlantillaPlanilla pp
INNER JOIN dbo.TC_CategoriaPlanilla cp on cp.I_CategoriaPlanillaID = pp.I_CategoriaPlanillaID
INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc on ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
INNER JOIN dbo.TC_Concepto c on c.I_ConceptoID = ppc.I_ConceptoID
INNER JOIN dbo.TC_TipoConcepto t ON t.I_TipoConceptoID = c.I_TipoConceptoID


--PERSONAL DOCENTE
SELECT catpla.T_CategoriaPlanillaDesc, trab.I_TrabajadorID, per.T_Nombre, per.T_ApellidoPaterno, per.T_ApellidoMaterno, tipdoc.T_TipoDocumentoDesc, per.C_NumDocumento, trab.D_FechaIngreso, 
	clapla.T_ClasePlanillaDesc, catdoc.C_CategoriaDocenteCod, catdoc.T_CategoriaDocenteDesc, deddoc.C_DedicacionDocenteCod, deddoc.T_DedicacionDocenteDesc, hordoc.I_Horas
FROM TC_Docente AS doc INNER JOIN
	TC_Trabajador AS trab ON trab.I_TrabajadorID = doc.I_TrabajadorID INNER JOIN
	TC_Persona AS per ON per.I_PersonaID = trab.I_PersonaID INNER JOIN
	TC_TipoDocumento AS tipdoc ON tipdoc.I_TipoDocumentoID = per.I_TipoDocumentoID INNER JOIN
	TC_Trabajador_CategoriaPlanilla AS tracatpla ON tracatpla.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
	TC_CategoriaPlanilla AS catpla ON catpla.I_CategoriaPlanillaID = tracatpla.I_CategoriaPlanillaID INNER JOIN
	TC_ClasePlanilla AS clapla ON clapla.I_ClasePlanillaID = catpla.I_ClasePlanillaID INNER JOIN
	TC_CategoriaDocente AS catdoc ON catdoc.I_CategoriaDocenteID = doc.I_CategoriaDocenteID INNER JOIN
	TC_HorasDocente AS hordoc ON hordoc.I_HorasDocenteID = doc.I_HorasDocenteID INNER JOIN
	TC_DedicacionDocente AS deddoc ON deddoc.I_DedicacionDocenteID = hordoc.I_DedicacionDocenteID
GO

--PERSONAL ADMINISTRATIVO
SELECT catpla.T_CategoriaPlanillaDesc, trab.I_TrabajadorID, per.T_Nombre, per.T_ApellidoPaterno, per.T_ApellidoMaterno, tipdoc.T_TipoDocumentoDesc, per.C_NumDocumento, trab.D_FechaIngreso, clapla.T_ClasePlanillaDesc, 
	grupocup.C_GrupoOcupacionalCod, grupocup.T_GrupoOcupacionalDesc, nivremu.C_NivelRemunerativoCod
FROM TC_Administrativo AS adm INNER JOIN
    TC_Trabajador AS trab ON trab.I_TrabajadorID = adm.I_TrabajadorID INNER JOIN
    TC_Persona AS per ON per.I_PersonaID = trab.I_PersonaID INNER JOIN
    TC_TipoDocumento AS tipdoc ON tipdoc.I_TipoDocumentoID = per.I_TipoDocumentoID INNER JOIN
    TC_Trabajador_CategoriaPlanilla AS tracatpla ON tracatpla.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
    TC_CategoriaPlanilla AS catpla ON catpla.I_CategoriaPlanillaID = tracatpla.I_CategoriaPlanillaID INNER JOIN
    TC_ClasePlanilla AS clapla ON clapla.I_ClasePlanillaID = catpla.I_ClasePlanillaID INNER JOIN
    TC_NivelRemunerativo AS nivremu ON nivremu.I_NivelRemunerativoID = adm.I_NivelRemunerativoID INNER JOIN
    TC_GrupoOcupacional AS grupocup ON grupocup.I_GrupoOcupacionalID = adm.I_GrupoOcupacionalID
GO




SELECT * FROM TC_Trabajador

SELECT * FROM dbo.TC_Administrativo
SELECT * FROM dbo.TC_GrupoOcupacional WHERE B_Eliminado = 0;
SELECT * FROM dbo.TC_NivelRemunerativo WHERE B_Eliminado = 0;

SELECT * FROM TC_Trabajador
select * from dbo.TC_Dependencia
select * from dbo.TC_Trabajador_Dependencia

select * from dbo.TC_CuentaBancaria

select * from dbo.TC_Persona
SELECT * FROM TC_Trabajador



SELECT * FROM dbo.TC_Docente
SELECT * FROM dbo.TC_CategoriaDocente WHERE B_Eliminado = 0;
SELECT * FROM dbo.TC_DedicacionDocente WHERE B_Eliminado = 0 AND I_DedicacionDocenteID = 1;
SELECT * FROM dbo.TC_HorasDocente WHERE B_Eliminado = 0;

select * from dbo.TC_Trabajador
select * from dbo.TC_Trabajador_CategoriaPlanilla

SELECT * FROM dbo.TC_Vinculo
SELECT * FROM dbo.TC_CategoriaPlanilla



