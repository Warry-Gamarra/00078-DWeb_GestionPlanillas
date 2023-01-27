USE [BD_OCRH_GestionPlanillas]
GO

SELECT * FROM TC_TipoConcepto

INSERT TC_TipoConcepto(T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES('REMUNERATIVO', 1, 0)
INSERT TC_TipoConcepto(T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES('DESCUENTO', 1, 0)
INSERT TC_TipoConcepto(T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES('REINTEGRO', 1, 0)
INSERT TC_TipoConcepto(T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES('DEDUCCÓN', 1, 0)
GO


SELECT * FROM dbo.TC_Concepto
--REMUNERACION
INSERT TC_Concepto(I_TipoConceptoID, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(1, 'BASICO', 0.0, 1, 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(1, 'BONIF.D.S.276', 7.5, 1, 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(1, 'DEC.URG.N°666', 15.4, 1, 1, 0)

--DESCUENTO
INSERT TC_Concepto(I_TipoConceptoID, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(2, 'DESCTO.AFP', 0.08, 0, 1, 0)

--REINTEGRO
INSERT TC_Concepto(I_TipoConceptoID, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(3, '', 0, 0, 1, 0)

--DEDUCCION
INSERT TC_Concepto(I_TipoConceptoID, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(4, '', 0, 0, 1, 0)
GO


SELECT * FROM dbo.TC_TipoPlanilla

INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('01', 'ACTIVOS', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('02', 'PENSIONISTA', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('03', 'BENEFICIARIO', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('04', 'DESCUENTO JUDICIAL', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('05', 'JUDICIAL', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('99', 'OTROS', 1, 0)
GO


SELECT * FROM TC_ClasePlanilla

INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '01', 'HABERES', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '02', 'CAFAE', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '03', 'CAS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '04', 'FAG', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '05', 'CESIGRA', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '06', 'PRACTICAS PREPROFESIONALES', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(2, '07', 'PENSIONISTAS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '08', 'VIUDEZ', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '09', 'ORFANDAD', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '10', 'DEPENDENCIA', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '11', 'MONTEPIO', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(4, '12', 'ALIMENTOS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(5, '13', 'DEVENGADOS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(6, '99', 'OTROS', 1, 0)
GO


SELECT * FROM dbo.TI_PlantillaPlanilla

INSERT TI_PlantillaPlanilla(I_ClasePlanillaID, B_Habilitado, B_Eliminado) VALUES(1, 1, 0)
GO


SELECT * FROM dbo.TI_PlantillaPlanilla_Concepto

INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 1, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 2, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 3, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 4, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 5, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 6, 1, 0)
GO


SELECT * FROM dbo.TC_CategoriaDocente

INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('PR', 'PRINCIPAL', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('AS', 'ASOCIADO', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('AX', 'AUXILIAR', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('JP', 'JEFE DE PRACTICA', 1, 0)
GO


SELECT * FROM dbo.TC_DedicacionDocente

INSERT TC_DedicacionDocente(C_DedicacionDocenteCod, T_DedicacionDocenteDesc, B_Habilitado, B_Eliminado) VALUES('DE', 'DEDICACION EXCLUSIVA', 1, 0)
INSERT TC_DedicacionDocente(C_DedicacionDocenteCod, T_DedicacionDocenteDesc, B_Habilitado, B_Eliminado) VALUES('TC', 'TIEMPO COMPLETO', 1, 0)
INSERT TC_DedicacionDocente(C_DedicacionDocenteCod, T_DedicacionDocenteDesc, B_Habilitado, B_Eliminado) VALUES('TP', 'TIEMPO PARCIAL', 1, 0)
GO


SELECT * FROM dbo.TC_GrupoOcupacional

INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('SF', 'SERVIDOR FUNCIONARIO', 1, 0)
INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('SP', 'SERVIDOR PROFESIONAL', 1, 0)
INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('ST', 'SERVIDOR TECNICO', 1, 0)
INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('SA', 'SERVIDOR AUXILIAR', 1, 0)
GO


SELECT * FROM dbo.TC_NivelRemunerativo

INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('1', '1', 1, 0)
INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('2', '2', 1, 0)
INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('3', '3', 1, 0)
INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('4', '4', 1, 0)
INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('A', 'A', 1, 0)
INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('B', 'B', 1, 0)
INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('C', 'C', 1, 0)
INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('D', 'D', 1, 0)
INSERT TC_NivelRemunerativo(C_NivelRemunerativoCod, T_NivelRemunerativoDesc, B_Habilitado, B_Eliminado) VALUES('E', 'E', 1, 0)
GO



SELECT t.T_TipoConceptoDesc, c.T_ConceptoDesc, c.M_Valor, c.B_EsFijo FROM dbo.TC_Concepto c
INNER JOIN dbo.TC_TipoConcepto t ON t.I_TipoConceptoID = c.I_TipoConceptoID

select tp.C_TipoPlanillaCod, tp.T_TipoPlanillaDesc, cl.C_ClasePlanillaCod, cl.T_ClasePlanillaDesc, 
t.T_TipoConceptoDesc, c.T_ConceptoDesc, c.M_Valor, c.B_EsFijo from dbo.TI_PlantillaPlanilla pp
INNER JOIN dbo.TC_ClasePlanilla cl on cl.I_ClasePlanillaID = pp.I_ClasePlanillaID
INNER JOIN dbo.TC_TipoPlanilla tp on tp.I_TipoPlanillaID = cl.I_TipoPlanillaID
INNER JOIN  dbo.TI_PlantillaPlanilla_Concepto ppc on ppc.I_PlantillaPlanillaID = pp.I_PlantillaPlanillaID
INNER JOIN dbo.TC_Concepto c on c.I_ConceptoID = ppc.I_ConceptoID
INNER JOIN dbo.TC_TipoConcepto t ON t.I_TipoConceptoID = c.I_TipoConceptoID

