USE BD_OCRH_GestionPlanillas
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_Trabajadores')
	DROP VIEW [dbo].[VW_Trabajadores]
GO
--reemplazar consultas por B_CategoriaPrincipal
CREATE VIEW [dbo].[VW_Trabajadores]
AS
SELECT 
	trab.I_TrabajadorID, trab.C_TrabajadorCod, per.I_PersonaID, per.T_Nombre, per.T_ApellidoPaterno, per.T_ApellidoMaterno, 
	tipdoc.I_TipoDocumentoID, tipdoc.T_TipoDocumentoDesc, per.C_NumDocumento, 
	trab.D_FechaIngreso, reg.I_RegimenID, reg.T_RegimenDesc, afp.I_AfpID, afp.T_AfpDesc, trab.T_Cuspp, 
	est.I_EstadoID, est.T_EstadoDesc, vin.I_VinculoID, vin.T_VinculoDesc,
	dep.I_DependenciaID, dep.C_DependenciaCod, dep.T_DependenciaDesc,
	cta.I_CuentaBancariaID, cta.T_NroCuentaBancaria, bco.I_BancoID, bco.T_BancoDesc, bco.T_BancoAbrv
FROM 
	dbo.TC_Persona AS per INNER JOIN
	dbo.TC_Trabajador AS trab ON trab.I_PersonaID = per.I_PersonaID INNER JOIN
	dbo.TC_Trabajador_CategoriaPlanilla AS trabcat ON trabcat.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
	dbo.TC_TipoDocumento AS tipdoc ON tipdoc.I_TipoDocumentoID = per.I_TipoDocumentoID LEFT JOIN
	dbo.TC_Regimen AS reg ON reg.I_RegimenID = trab.I_RegimenID LEFT JOIN 
	dbo.TC_Afp AS afp ON afp.I_AfpID = trab.I_AfpID INNER JOIN
	dbo.TC_Estado AS est ON est.I_EstadoID = trab.I_EstadoID INNER JOIN 
	dbo.TC_Vinculo AS vin ON vin.I_VinculoID = trab.I_VinculoID INNER JOIN
	dbo.TC_Dependencia AS dep ON dep.I_DependenciaID = trabcat.I_DependenciaID LEFT JOIN
	dbo.TC_CuentaBancaria AS cta ON cta.I_TrabajadorID = trab.I_TrabajadorID AND cta.B_Habilitado = 1 LEFT JOIN
	dbo.TC_Banco AS bco ON bco.I_BancoID = cta.I_BancoID
WHERE per.B_Eliminado = 0 AND trab.B_Eliminado = 0 AND trabcat.B_Eliminado = 0 AND trabcat.B_CategoriaPrincipal = 1
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_TrabajadoresCategoriaPlanilla')
	DROP VIEW [dbo].[VW_TrabajadoresCategoriaPlanilla]
GO
--VERIFICAR QUE B_CategoriaPrincipal Y B_PlanillaCabecera ESTEN EL LA CLASE C#
CREATE VIEW [dbo].[VW_TrabajadoresCategoriaPlanilla]
AS
SELECT 
	trab.I_TrabajadorID, trab.C_TrabajadorCod, per.I_PersonaID, per.T_Nombre, per.T_ApellidoPaterno, per.T_ApellidoMaterno, 
	tipdoc.I_TipoDocumentoID, tipdoc.T_TipoDocumentoDesc, per.C_NumDocumento,
	est.I_EstadoID, est.T_EstadoDesc, vin.I_VinculoID, vin.T_VinculoDesc,
	trabcat.I_TrabajadorCategoriaPlanillaID, catpla.I_CategoriaPlanillaID, catpla.T_CategoriaPlanillaDesc,
	trabcat.B_CategoriaPrincipal, catpla.B_PlanillaCabecera
FROM 
	dbo.TC_Persona AS per INNER JOIN
	dbo.TC_Trabajador AS trab ON trab.I_PersonaID = per.I_PersonaID INNER JOIN
	dbo.TC_TipoDocumento AS tipdoc ON tipdoc.I_TipoDocumentoID = per.I_TipoDocumentoID INNER JOIN
	dbo.TC_Estado AS est ON est.I_EstadoID = trab.I_EstadoID INNER JOIN 
	dbo.TC_Vinculo AS vin ON vin.I_VinculoID = trab.I_VinculoID INNER JOIN
	dbo.TC_Trabajador_CategoriaPlanilla AS trabcat ON trabcat.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
	dbo.TC_CategoriaPlanilla AS catpla ON catpla.I_CategoriaPlanillaID = trabcat.I_CategoriaPlanillaID
WHERE per.B_Eliminado = 0 AND trab.B_Eliminado = 0 AND trabcat.B_Eliminado = 0 
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_Conceptos')
	DROP VIEW [dbo].[VW_Conceptos]
GO

CREATE VIEW [dbo].[VW_Conceptos]
AS
SELECT 
	c.I_ConceptoID, c.I_TipoConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, c.T_ConceptoAbrv, c.B_Habilitado, tc.T_TipoConceptoDesc
FROM dbo.TC_Concepto c
INNER JOIN dbo.TC_TipoConcepto tc ON tc.I_TipoConceptoID = c.I_TipoConceptoID
WHERE c.B_Eliminado = 0
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
	hdoc.I_HorasDocenteID, hdoc.I_Horas, dedoc.I_DedicacionDocenteID, dedoc.C_DedicacionDocenteCod, dedoc.T_DedicacionDocenteDesc,
	doc.B_Habilitado
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
	nivrem.I_NivelRemunerativoID, nivrem.C_NivelRemunerativoCod, nivrem.T_NivelRemunerativoDesc,
	adm.B_Habilitado
FROM dbo.VW_Trabajadores AS trab INNER JOIN
	dbo.TC_Administrativo AS adm ON adm.I_TrabajadorID = trab.I_TrabajadorID INNER JOIN
	dbo.TC_GrupoOcupacional AS grup ON grup.I_GrupoOcupacionalID = adm.I_GrupoOcupacionalID INNER JOIN
	dbo.TC_NivelRemunerativo AS nivrem ON nivrem.I_NivelRemunerativoID = adm.I_NivelRemunerativoID
WHERE adm.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_PlantillasPlanilla')
	DROP VIEW [dbo].[VW_PlantillasPlanilla]
GO

CREATE VIEW [dbo].[VW_PlantillasPlanilla]
AS
SELECT
	pp.I_PlantillaPlanillaID, pp.T_PlantillaPlanillaDesc, pp.B_Habilitado, cp.I_CategoriaPlanillaID, 
	cp.T_CategoriaPlanillaDesc, cl.I_ClasePlanillaID, cl.T_ClasePlanillaDesc
FROM dbo.TI_PlantillaPlanilla pp
INNER JOIN dbo.TC_CategoriaPlanilla cp ON cp.I_CategoriaPlanillaID = pp.I_CategoriaPlanillaID
INNER JOIN dbo.TC_ClasePlanilla cl ON cl.I_ClasePlanillaID = cp.I_ClasePlanillaID
WHERE pp.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_ConceptosAsignados_Plantilla')
	DROP VIEW [dbo].[VW_ConceptosAsignados_Plantilla]
GO

CREATE VIEW [dbo].[VW_ConceptosAsignados_Plantilla]
AS
SELECT
	ppc.I_PlantillaPlanillaConceptoID,
	pp.I_PlantillaPlanillaID, pp.T_PlantillaPlanillaDesc, 
	cp.I_CategoriaPlanillaID, cp.T_CategoriaPlanillaDesc, 
	cl.I_ClasePlanillaID, cl.T_ClasePlanillaDesc,
	t.I_TipoConceptoID, t.T_TipoConceptoDesc, t.B_EsAdicion, t.B_IncluirEnTotalBruto, 
	c.I_ConceptoID, c.C_ConceptoCod, c.T_ConceptoDesc, c.T_ConceptoAbrv, 
	ppc.B_EsValorFijo, ppc.B_ValorEsExterno, ppc.M_ValorConcepto, 
	ppc.B_AplicarFiltro1, 
	ppc.I_Filtro1, 
	ppc.B_AplicarFiltro2, 
	ppc.I_Filtro2,
	ppc.B_Habilitado
FROM dbo.TI_PlantillaPlanilla pp
INNER JOIN dbo.TC_CategoriaPlanilla cp on cp.I_CategoriaPlanillaID = pp.I_CategoriaPlanillaID
INNER JOIN dbo.TC_ClasePlanilla cl ON cl.I_ClasePlanillaID = cp.I_ClasePlanillaID
INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc on ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
INNER JOIN dbo.TC_Concepto c on c.I_ConceptoID = ppc.I_ConceptoID
INNER JOIN dbo.TC_TipoConcepto t ON t.I_TipoConceptoID = c.I_TipoConceptoID
WHERE ppc.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_ConceptosReferencia_Plantilla')
	DROP VIEW [dbo].[VW_ConceptosReferencia_Plantilla]
GO

CREATE VIEW [dbo].[VW_ConceptosReferencia_Plantilla]
AS
SELECT
	ref.I_ID, ref.I_PlantillaPlanillaConceptoID, ref.I_ConceptoReferenciaID, con.C_ConceptoCod, con.T_ConceptoDesc, con.T_ConceptoAbrv
FROM dbo.TI_PlantillaPlanilla_Concepto_Referencia ref
INNER JOIN dbo.TC_Concepto con ON con.I_ConceptoID = ref.I_ConceptoReferenciaID
WHERE ref.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_ValoresExternos')
	DROP VIEW [dbo].[VW_ValoresExternos]
GO

CREATE VIEW [dbo].[VW_ValoresExternos]
AS
	SELECT excon.I_ConceptoExternoValorID, per.I_PeriodoID, per.I_Anio, per.I_Mes, per.T_MesDesc, trab.I_TrabajadorID, trab.T_TipoDocumentoDesc, trab.C_NumDocumento, trab.T_ApellidoPaterno, trab.T_ApellidoMaterno, trab.T_Nombre,
		catrab.I_TrabajadorCategoriaPlanillaID, catrab.I_CategoriaPlanillaID, cat.T_CategoriaPlanillaDesc, con.I_ConceptoID, con.C_ConceptoCod, con.T_ConceptoDesc, con.T_TipoConceptoDesc, excon.M_ValorConcepto, pro.I_ProveedorID, pro.T_ProveedorDesc
	FROM dbo.TI_ValorExternoPeriodo exper
	INNER JOIN dbo.TI_ValorExternoConcepto excon ON exper.I_ValorExternoPeriodoID = excon.I_ValorExternoPeriodoID
	INNER JOIN dbo.TC_Trabajador_CategoriaPlanilla catrab ON catrab.I_TrabajadorCategoriaPlanillaID = exper.I_TrabajadorCategoriaPlanillaID
	INNER JOIN dbo.VW_Trabajadores trab ON trab.I_TrabajadorID = catrab.I_TrabajadorID
	INNER JOIN dbo.TC_CategoriaPlanilla cat ON cat.I_CategoriaPlanillaID = catrab.I_CategoriaPlanillaID
	INNER JOIN dbo.TR_Periodo per ON per.I_PeriodoID = exper.I_PeriodoID
	INNER JOIN dbo.VW_Conceptos con ON con.I_ConceptoID = excon.I_ConceptoID
	INNER JOIN dbo.TC_Proveedor pro ON pro.I_ProveedorID = excon.I_ProveedorID
	WHERE excon.B_Eliminado = 0 AND exper.B_Eliminado = 0 AND catrab.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_Usuarios')
	DROP VIEW [dbo].[VW_Usuarios]
GO

CREATE VIEW [dbo].[VW_Usuarios]
AS
SELECT U.UserId, U.UserName, U.D_FecActualizaPassword, U.B_CambiaPassword, U.B_Habilitado, U.I_UsuarioCre, DU.I_DatosUsuarioID, DU.N_NumDoc, 
    DU.T_NomPersona, DU.T_CorreoUsuario, UDU.D_FecAlta, UIR.RoleId, R.RoleName, U.I_DependenciaID, dep.T_DependenciaDesc
FROM [dbo].[TC_Usuario] U
    INNER JOIN [dbo].[webpages_UsersInRoles] UIR ON U.UserId = UIR.UserId
	INNER JOIN [dbo].[webpages_Roles] R ON UIR.RoleId = R.RoleId
	INNER JOIN [dbo].[TI_UsuarioDatosUsuario] UDU ON UDU.UserId = U.UserId
	INNER JOIN [dbo].[TC_DatosUsuario] DU ON DU.I_DatosUsuarioID = UDU.I_DatosUsuarioID
	LEFT JOIN dbo.TC_Dependencia AS dep ON dep.I_DependenciaID = U.I_DependenciaID
WHERE U.B_Eliminado = 0
GO



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_DepActividadMeta')
	DROP VIEW [dbo].[VW_DepActividadMeta]
GO

CREATE VIEW [dbo].[VW_DepActividadMeta]
AS
SELECT
	dam.I_DepActividadMetaID,
	dam.I_Anio,
	cp.I_CategoriaPlanillaID,
	cp.T_CategoriaPlanillaDesc,
	dep.I_DependenciaID,
	dep.C_DependenciaCod,
	dep.T_DependenciaDesc,
	dam.T_Descripcion,
	act.I_ActividadID,
	act.C_ActividadCod,
	act.T_ActividadDesc,
	met.I_MetaID,
	met.C_MetaCod,
	met.T_MetaDesc,
	catp.I_CategoriaPresupuestalID,
	catp.T_CategoriaPresupCod,
	catp.T_CategoriaPresupDesc
FROM dbo.TC_DepActividadMeta dam
INNER JOIN dbo.TC_CategoriaPlanilla cp ON cp.I_CategoriaPlanillaID = dam.I_CategoriaPlanillaID
INNER JOIN dbo.TC_Dependencia dep ON dep.I_DependenciaID = dam.I_DependenciaID
INNER JOIN dbo.TC_Actividad act ON act.I_ActividadID = dam.I_ActividadID
INNER JOIN dbo.TC_Meta met ON met.I_MetaID = dam.I_MetaID
INNER JOIN dbo.TC_CategoriaPresupuestal catp ON catp.I_CategoriaPresupuestalID = dam.I_CategoriaPresupuestalID
GO






/*

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

*/