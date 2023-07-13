USE [BD_OCRH_GestionPlanillas]
GO

SET IDENTITY_INSERT TC_TipoConcepto ON
GO

INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(1, 'INGRESOS ', 1, 1, 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(2, 'DESCUENTOS ', 0, 0, 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(3, 'APORTES', 1, 0, 0, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(4, 'REINTEGROS', 1, 1, 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(5, 'ENCARGATURAS', 1, 1, 0, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(6, 'OTROS INGRESOS', 1, 0, 0, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(9, 'NETO', 1, 0, 0, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(10, 'DEDUCCIÓN', 0, 1, 1, 0)--AGREGADO
GO

SET IDENTITY_INSERT TC_TipoConcepto OFF
GO



SET IDENTITY_INSERT TC_Concepto ON
GO

INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(1, 1, '001', 'rembas', 'rembas', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(2, 1, '002', 'remper', 'remper', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(3, 1, '003', 'remtra', 'remtra', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(4, 1, '004', 'remsuf', 'remsuf', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(5, 1, '005', 'remreu', 'remreu', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(6, 1, '006', 'remrym', 'remrym', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(7, 1, '007', 'rem276', 'rem276', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(8, 1, '008', 'rem256', 'rem256', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(9, 1, '009', 'rem194', 'rem194', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(10, 1, '010', 'rem090', 'rem090', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(11, 1, '011', 'rem073', 'rem073', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(12, 1, '012', 'rem01199', 'rem01199', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(13, 1, '013', 'rem227', 'rem227', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(14, 1, '014', 'rem037', 'rem037', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(15, 1, '015', 'ha265', 'ha265', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(16, 2, '016', 'difcar1', 'difcar1', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(17, 2, '017', 'difcar4', 'difcar4', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(18, 2, '018', 'difcardu', 'difcardu', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(19, 2, '019', 'ds044', 'ds044', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(20, 2, '020', 'ds107', 'ds107', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(21, 2, '021', 'ley28254', 'ley28254', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(22, 2, '022', 'ds008', 'ds008', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(23, 1, '023', 'rfafp1', 'rfafp1', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(24, 1, '024', 'rfafp2', 'rfafp2', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(25, 1, '025', 'rhafp1', 'rhafp1', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(26, 1, '026', 'rhafp2', 'rhafp2', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(27, 2, '027', 'dessnp', 'dessnp', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(28, 2, '028', 'desfjc', 'desfjc', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(29, 2, '029', 'desrej', 'desrej', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(30, 2, '030', 'desips', 'desips', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(31, 2, '031', 'desobl', 'desobl', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(32, 2, '032', 'desinv', 'desinv', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(33, 2, '033', 'despor', 'despor', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(34, 2, '034', 'desvar', 'desvar', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, T_ConceptoAbrv, B_Habilitado, B_Eliminado) VALUES(35, 10, '035', 'dremtra', 'dremtra', 1, 0)

SET IDENTITY_INSERT TC_Concepto OFF
GO



SET IDENTITY_INSERT TC_TipoPlanilla ON
GO

INSERT TC_TipoPlanilla(I_TipoPlanillaID, C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '01', 'ACTIVOS', 1, 0)
INSERT TC_TipoPlanilla(I_TipoPlanillaID, C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(2, '02', 'PENSIONISTA', 1, 0)
INSERT TC_TipoPlanilla(I_TipoPlanillaID, C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '03', 'BENEFICIARIO', 1, 0)
INSERT TC_TipoPlanilla(I_TipoPlanillaID, C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(4, '04', 'DESCUENTO JUDICIAL', 1, 0)
INSERT TC_TipoPlanilla(I_TipoPlanillaID, C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(5, '05', 'JUDICIAL', 1, 0)
INSERT TC_TipoPlanilla(I_TipoPlanillaID, C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(6, '99', 'OTROS', 1, 0)

SET IDENTITY_INSERT TC_TipoPlanilla OFF
GO



SET IDENTITY_INSERT TC_ClasePlanilla ON
GO

INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, 1, '01', 'HABERES', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(2, 1, '02', 'CAFAE', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, 1, '03', 'CAS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(4, 1, '04', 'FAG', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(5, 1, '05', 'CESIGRA', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(6, 1, '06', 'PRÁCTICAS PREPROFESIONALES', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(7, 2, '07', 'PENSIONISTAS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(8, 3, '08', 'VIUDEZ', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(9, 3, '09', 'ORFANDAD', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(10, 3, '10', 'DEPENDENCIA', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(11, 3, '11', 'MONTEPIO', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(12, 4, '12', 'ALIMENTOS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(13, 5, '13', 'DEVENGADOS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(14, 6, '99', 'OTROS', 1, 0)

SET IDENTITY_INSERT TC_ClasePlanilla OFF
GO


SET IDENTITY_INSERT TC_CategoriaPlanilla ON
GO

INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, 1, 'Haberes Administrativo', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(2, 1, 'Haberes Docente', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, 1, 'Haberes Médico', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(4, 7, 'Pensiones', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(5, 2, 'CAFAE', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(6, 14, 'Docente Investigador', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(7, 6, 'Practicante', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(8, 14, 'Productividad', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(9, 14, 'Generadora de Recursos', 1, 0)
GO

SET IDENTITY_INSERT TC_CategoriaPlanilla OFF
GO


INSERT TI_PlantillaPlanilla(I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(1, 1, 0)
INSERT TI_PlantillaPlanilla(I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(2, 1, 0)
GO


INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('PR', 'PRINCIPAL', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('AS', 'ASOCIADO', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('AX', 'AUXILIAR', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('JP', 'JEFE DE PRÁCTICA', 1, 0)
GO


INSERT TC_DedicacionDocente(C_DedicacionDocenteCod, T_DedicacionDocenteDesc, B_Habilitado, B_Eliminado) VALUES('DE', 'DEDICACIÓN EXCLUSIVA', 1, 0)
INSERT TC_DedicacionDocente(C_DedicacionDocenteCod, T_DedicacionDocenteDesc, B_Habilitado, B_Eliminado) VALUES('TC', 'TIEMPO COMPLETO', 1, 0)
INSERT TC_DedicacionDocente(C_DedicacionDocenteCod, T_DedicacionDocenteDesc, B_Habilitado, B_Eliminado) VALUES('TP', 'TIEMPO PARCIAL', 1, 0)
GO


INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(1, 40, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(2, 40, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 20, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 19, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 18, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 17, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 16, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 15, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 14, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 13, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 12, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 11, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 10, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 9, 1, 0)
INSERT TC_HorasDocente(I_DedicacionDocenteID, I_Horas, B_Habilitado, B_Eliminado) VALUES(3, 8, 1, 0)
GO


INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('SF', 'SERVIDOR FUNCIONARIO', 1, 0)
INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('SP', 'SERVIDOR PROFESIONAL', 1, 0)
INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('ST', 'SERVIDOR TÉCNICO', 1, 0)
INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('SA', 'SERVIDOR AUXILIAR', 1, 0)
GO


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


INSERT dbo.TC_TipoDocumento(T_TipoDocumentoDesc, B_Habilitado, B_Eliminado) VALUES('DNI', 1, 0)
INSERT dbo.TC_TipoDocumento(T_TipoDocumentoDesc, B_Habilitado, B_Eliminado) VALUES('PASAPORTE', 1, 0)
INSERT dbo.TC_TipoDocumento(T_TipoDocumentoDesc, B_Habilitado, B_Eliminado) VALUES('CARNÉ DE EXTRANJERÍA', 1, 0)
GO


INSERT dbo.TC_Estado(T_EstadoDesc, C_EstadoCod, B_Habilitado, B_Eliminado) VALUES('ACTIVO', '0', 1, 0)
INSERT dbo.TC_Estado(T_EstadoDesc, C_EstadoCod, B_Habilitado, B_Eliminado) VALUES('SUSPENDIDO', '1', 1, 0)
INSERT dbo.TC_Estado(T_EstadoDesc, C_EstadoCod, B_Habilitado, B_Eliminado) VALUES('LICENCIA', '2', 1, 0)
INSERT dbo.TC_Estado(T_EstadoDesc, C_EstadoCod, B_Habilitado, B_Eliminado) VALUES('VACANTE', '3', 1, 0)
INSERT dbo.TC_Estado(T_EstadoDesc, C_EstadoCod, B_Habilitado, B_Eliminado) VALUES('ANULADO (BAJA)', '5', 1, 0)
INSERT dbo.TC_Estado(T_EstadoDesc, C_EstadoCod, B_Habilitado, B_Eliminado) VALUES('SIN ASISTENCIA', '6', 1, 0)
INSERT dbo.TC_Estado(T_EstadoDesc, C_EstadoCod, B_Habilitado, B_Eliminado) VALUES('FALLECIDO', '7', 1, 0)
INSERT dbo.TC_Estado(T_EstadoDesc, C_EstadoCod, B_Habilitado, B_Eliminado) VALUES('SUBSIDIADO', '8', 1, 0)
GO


INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('ADMINISTRATIVO PERMANENTE', 'AP', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('ADMINISTRATIVO CONTRATADO', 'AC', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('MÉDICO PERMANENTE', 'MP', 0, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('DOCENTE PERMANENTE', 'DP', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('DOCENTE CONTRATADO', 'DC', 0, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('CESANTE ADMINISTRATIVO', 'CA', 0, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('CESANTE MÉDICO', 'CM', 0, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('SERVIDOR PERMANENTE', 'SP', 0, 0)
GO


INSERT dbo.TC_Regimen(T_RegimenDesc, C_RegimenCod, B_Habilitado, B_Eliminado) VALUES('SIN RÉGIMEN', '0', 1, 0)
INSERT dbo.TC_Regimen(T_RegimenDesc, C_RegimenCod, B_Habilitado, B_Eliminado) VALUES('19990 (ONP)', '1', 1, 0)
INSERT dbo.TC_Regimen(T_RegimenDesc, C_RegimenCod, B_Habilitado, B_Eliminado) VALUES('20530', '2', 1, 0)
INSERT dbo.TC_Regimen(T_RegimenDesc, C_RegimenCod, B_Habilitado, B_Eliminado) VALUES('25897 (AFP)', '3', 1, 0)
GO


INSERT dbo.TC_Banco(T_BancoDesc, C_BancoCod, B_Habilitado, B_Eliminado) VALUES('BANCO DE CRÉDITO', 'CODBAN001', 1, 0)
INSERT dbo.TC_Banco(T_BancoDesc, C_BancoCod,B_Habilitado, B_Eliminado) VALUES('BANCO DE LA NACIÓN', 'CODBAN002', 1, 0)
INSERT dbo.TC_Banco(T_BancoDesc, C_BancoCod,B_Habilitado, B_Eliminado) VALUES('SCOTIABANK', 'CODBAN002', 1, 0)
INSERT dbo.TC_Banco(T_BancoDesc, C_BancoCod,B_Habilitado, B_Eliminado) VALUES('INTERBANK', 'CODBAN003', 1, 0)
INSERT dbo.TC_Banco(T_BancoDesc, C_BancoCod,B_Habilitado, B_Eliminado) VALUES('BBVA', 'CODBAN004', 1, 0)
GO


INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE GESTION DEL EGRESADO', '020700', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('RECTORADO', '010000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE CALIDAD', '010002', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('ORGANO DE CONTROL INSTITUCIONAL', '010100', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE PLANIFICACIÓN', '010200', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE RELACIONES NACIONALES E INTERNACIONALES', '010300', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE ASESORIA JURÍDICA', '010400', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('SECRETARIA GENERAL', '010500', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE COMUNICACIONES E IMAGEN INSTITUCIONAL', '010600', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE BIENESTAR UNIVERSITARIO', '010700', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE GESTION DE TECNOLOGIAS DE LA INFORMACION', '010800', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('CENTRO DE PRODRUCCION DE BIENES Y SERVICIOS', '010900', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('CENTRO PRE UNIVERSITARIO', '010901', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('EDITORIAL UNIVERSITARIA', '010902', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('ESCUELA UNIVERSITARIA DE POST GRADO', '011000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('CENTRO UNIVERSITARIO DE RESPONSABILIDAD SOCIAL', '011300', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('UNIDAD SUPRIMIDA', '011400', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('CENTRO UNIVERSITARIO DE IDIOMAS', '011500', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('VICE RECTORADO ACADEMICO', '020000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE ADMISIÓN', '020100', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE ASUNTOS ACADÉMICOS', '020200', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('INSTITUTO CENTRAL DE GESTION DE LA INVESTIGACION', '020300', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('CENTRO DE GESTIÓN CULTURAL FEDERICO VILLARREAL', '020400', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('INSTITUTO CENTRAL DE RECREACIÓN EDUCACIÓN FÍSICA Y DEPORTES', '020500', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE REGISTROS ACADÉMICOS', '020600', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA DE RECURSOS HUMANOS', '030100', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA DE ABASTECIMIENTO Y SERVICIOS GENERALES', '030200', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA DE CONTABILIDAD / OFICINA DE TESORERIA', '030300', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA DE INVERSIONES', '030500', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA DE PATRIMONIO', '700000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('VICE RECTORADO DE INVESTIGACION', '040000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('DIRECCION GENERAL DE ADMINISTRACION', '050000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('OFICINA CENTRAL DE INNOVACION DESARROLLO Y EMPREND', '060000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE ARQUITECTURA Y URBANISMO', '100000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE ADMINISTRACION', '110000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE CIENCIAS ECONOMICAS', '120000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE CIENCIAS FINANCIERAS Y CONTABLES', '130000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE CIENCIAS NATURALES Y MATEMATICA', '140000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE CIENCIAS SOCIALES', '150000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE HUMANIDADES', '160000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE DERECHO Y CIENCIA  POLITICA', '170000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE EDUCACIÓN', '180000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE INGENIERÍA CIVIL', '190000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE INGENIERÍA GEOGRAFICA,  AMBIENTAL Y ECOTURISMO', '200000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE INGENIERÍA INDUSTRIAL Y DE SISTEMAS', '210000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE MEDICINA "HIPOLITO UNANUE"', '220000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE OCEANOGRAFÍA, PESQUERÍA, CIENCIA ALIMENTARIAS Y ACUICULTUR', '230000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE ODONTOLOGÍA', '240000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE PSICOLOGÍA', '250000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE TECNOLOGÍA MÉDICA', '260000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('FACULTAD DE INGENIERÍA ELECTRÓNICA E INFORMÁTICA', '270000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('BIBLIOTECA CENTRAL', '041000', 1, 0)
INSERT dbo.TC_Dependencia(T_DependenciaDesc, C_DependenciaCod, B_Habilitado, B_Eliminado) VALUES('DEFENSORIA UNIVERSITARIA', '010001', 1, 0)
GO


INSERT dbo.TC_Afp(T_AfpDesc, C_AfpCod, B_Habilitado, B_Eliminado) VALUES('AFP Habitat', '', 1, 0)
INSERT dbo.TC_Afp(T_AfpDesc, C_AfpCod, B_Habilitado, B_Eliminado) VALUES('AFP Integra', '', 1, 0)
INSERT dbo.TC_Afp(T_AfpDesc, C_AfpCod, B_Habilitado, B_Eliminado) VALUES('Prima AFP', '', 1, 0)
INSERT dbo.TC_Afp(T_AfpDesc, C_AfpCod, B_Habilitado, B_Eliminado) VALUES('Profuturo AFP', '', 1, 0)
GO



--DATA EJEMPLO
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 1, 'Enero')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 2, 'Febrero')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 3, 'Marzo')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 4, 'Abril')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 5, 'Mayo')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 6, 'Junio')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 7, 'Julio')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 8, 'Agosto')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 9, 'Setiembre')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 10, 'Octubre')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 11, 'Noviembre')
INSERT dbo.TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 12, 'Diciembre')
GO


INSERT dbo.TC_Proveedor(T_ProveedorDesc, B_Habilitado, B_Eliminado) VALUES('BANCO DE LA NACIÓN', 1, 0)
INSERT dbo.TC_Proveedor(T_ProveedorDesc, B_Habilitado, B_Eliminado) VALUES('BANCO DE CRÉDITO', 1, 0)
INSERT dbo.TC_Proveedor(T_ProveedorDesc, B_Habilitado, B_Eliminado) VALUES('BANCO SCOTIABANK', 1, 0)
GO
