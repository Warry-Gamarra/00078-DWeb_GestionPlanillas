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
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_EsAdicion, B_IncluirEnTotalBruto, B_Habilitado, B_Eliminado) VALUES(10, 'DEDUCCI�N', 0, 1, 1, 0)--AGREGADO
GO

SET IDENTITY_INSERT TC_TipoConcepto OFF
GO



SET IDENTITY_INSERT TC_Concepto ON
GO

INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, 1, '001', 'rembas', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(2, 1, '002', 'remper', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(3, 1, '003', 'remtra', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(4, 1, '004', 'remsuf', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(5, 1, '005', 'remreu', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(6, 1, '006', 'remrym', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(7, 1, '007', 'rem276', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(8, 1, '008', 'rem256', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(9, 1, '009', 'rem194', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(10, 1, '010', 'rem090', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(11, 1, '011', 'rem073', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(12, 1, '012', 'rem01199', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(13, 1, '013', 'rem227', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(14, 1, '014', 'rem037', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(15, 1, '015', 'ha265', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(16, 1, '016', 'difcar1', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(17, 1, '017', 'difcar4', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(18, 1, '018', 'difcardu', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(19, 1, '019', 'ds044', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(20, 1, '020', 'ds107', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(21, 1, '021', 'ley28254', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(22, 1, '022', 'ds008', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(23, 1, '023', 'rfafp1', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(24, 1, '024', 'rfafp2', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(25, 1, '025', 'rhafp1', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(26, 1, '026', 'rhafp2', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(27, 1, '027', 'dessnp', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(28, 2, '028', 'desfjc', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(29, 2, '029', 'desrej', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(30, 2, '030', 'desips', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(31, 2, '031', 'desobl', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(32, 2, '032', 'desinv', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(33, 2, '033', 'despor', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(34, 2, '034', 'desvar', 1, 0)
INSERT TC_Concepto(I_ConceptoID, I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(35, 10, '035', 'dremtra', 1, 0)

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
INSERT dbo.TC_ClasePlanilla(I_ClasePlanillaID, I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(6, 1, '06', 'PR�CTICAS PREPROFESIONALES', 1, 0)
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
INSERT dbo.TC_CategoriaPlanilla(I_CategoriaPlanillaID, I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, 1, 'Haberes M�dico', 1, 0)
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

--CONCEPTOS ADMINISTRATIVO
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 1, 1, 1, 50, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 2, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 3, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 4, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 5, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 6, 1, 1, 6.16, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 7, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 8, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 9, 1, 1, 50, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 10, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 11, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 12, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 13, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 14, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 15, 1, 0, NULL, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 16, 1, 1, 0, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 17, 1, 1, 0, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 18, 1, 1, 0, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 19, 1, 1, 100, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 20, 1, 1, 50, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 21, 1, 1, 50, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 22, 1, 1, 0, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 23, 1, 1, 0, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 24, 1, 1, 0, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 25, 1, 1, 0, 0, NULL, 0, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_EsMontoFijo, B_MontoEstaAqui, M_Monto, B_AplicarFiltro1, I_Filtro1, B_AplicarFiltro2, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 26, 1, 1, 0, 0, NULL, 0, NULL, 1, 0)
GO



INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('PR', 'PRINCIPAL', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('AS', 'ASOCIADO', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('AX', 'AUXILIAR', 1, 0)
INSERT TC_CategoriaDocente(C_CategoriaDocenteCod, T_CategoriaDocenteDesc, B_Habilitado, B_Eliminado) VALUES('JP', 'JEFE DE PR�CTICA', 1, 0)
GO


INSERT TC_DedicacionDocente(C_DedicacionDocenteCod, T_DedicacionDocenteDesc, B_Habilitado, B_Eliminado) VALUES('DE', 'DEDICACI�N EXCLUSIVA', 1, 0)
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
INSERT TC_GrupoOcupacional(C_GrupoOcupacionalCod, T_GrupoOcupacionalDesc, B_Habilitado, B_Eliminado) VALUES('ST', 'SERVIDOR T�CNICO', 1, 0)
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
INSERT dbo.TC_TipoDocumento(T_TipoDocumentoDesc, B_Habilitado, B_Eliminado) VALUES('CARN� DE EXTRANJER�A', 1, 0)
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
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('M�DICO PERMANENTE', 'MP', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('DOCENTE PERMANENTE', 'DP', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('DOCENTE CONTRATADO', 'DC', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('CESANTE ADMINISTRATIVO', 'CA', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('CESANTE M�DICO', 'CM', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, C_VinculoCod, B_Habilitado, B_Eliminado) VALUES('SERVIDOR PERMANENTE', 'SP', 1, 0)
GO


INSERT dbo.TC_Regimen(T_RegimenDesc, C_RegimenCod, B_Habilitado, B_Eliminado) VALUES('SIN R�GIMEN', '0', 1, 0)
INSERT dbo.TC_Regimen(T_RegimenDesc, C_RegimenCod, B_Habilitado, B_Eliminado) VALUES('19990 (ONP)', '1', 1, 0)
INSERT dbo.TC_Regimen(T_RegimenDesc, C_RegimenCod, B_Habilitado, B_Eliminado) VALUES('20530', '2', 1, 0)
INSERT dbo.TC_Regimen(T_RegimenDesc, C_RegimenCod, B_Habilitado, B_Eliminado) VALUES('25897 (AFP)', '3', 1, 0)
GO


INSERT dbo.TC_Banco(T_BancoDesc, B_Habilitado, B_Eliminado) VALUES('BANCO DE CR�DITO', 1, 0)
INSERT dbo.TC_Banco(T_BancoDesc, B_Habilitado, B_Eliminado) VALUES('BANCO DE LA NACI�N', 1, 0)
INSERT dbo.TC_Banco(T_BancoDesc, B_Habilitado, B_Eliminado) VALUES('SCOTIABANK', 1, 0)
INSERT dbo.TC_Banco(T_BancoDesc, B_Habilitado, B_Eliminado) VALUES('INTERBANK', 1, 0)
INSERT dbo.TC_Banco(T_BancoDesc, B_Habilitado, B_Eliminado) VALUES('BBVA', 1, 0)
GO


--DATA EJEMPLO
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 1, 'Enero')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 2, 'Febrero')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 3, 'Marzo')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 4, 'Abril')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 5, 'Mayo')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 6, 'Junio')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 7, 'Julio')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 8, 'Agosto')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 9, 'Setiembre')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 10, 'Octubre')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 11, 'Noviembre')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 12, 'Diciembre')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 1, 'Enero')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 2, 'Febrero')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 3, 'Marzo')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 4, 'Abril')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 5, 'Mayo')
GO


INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345678', 'Juan', 'Rodrigo', 'Jim�nez', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345679', 'Jos�', 'Medrano', 'Estrada', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345680', 'Abel', 'Ayuso', 'Alcalde', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345681', 'Manolo', 'Chaves', 'Trillo', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345682', 'Carol', 'Toledo', 'Monz�n', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345683', 'Rosa', 'Cardona', 'D�az', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345684', 'Sof�a', 'Terejo', 'Crespo', 1, 0)
GO


INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, I_EstadoID, B_Habilitado, B_Eliminado) VALUES(1, '20230101', 4, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, I_EstadoID, B_Habilitado, B_Eliminado) VALUES(2, '20230101', 4, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, I_EstadoID, B_Habilitado, B_Eliminado) VALUES(3, '20230101', 4, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, I_EstadoID, B_Habilitado, B_Eliminado) VALUES(4, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, I_EstadoID, B_Habilitado, B_Eliminado) VALUES(5, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, I_EstadoID, B_Habilitado, B_Eliminado) VALUES(6, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, I_EstadoID, B_Habilitado, B_Eliminado) VALUES(7, '20230101', 1, 1, 1, 0)
GO

--docente
INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(1, 2, 1, 0)
INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(2, 2, 1, 0)
INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(3, 2, 1, 0)
GO

INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado) VALUES(1, 1, 1, 1, 0)
INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado) VALUES(2, 2, 2, 1, 0)
INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado) VALUES(3, 3, 3, 1, 0)
GO

--INSERT dbo.TI_MontoTrabajador(I_TrabajadorID, I_PeriodoID, B_Habilitado, B_Eliminado) VALUES(1, 1, 1, 0)
--GO

--INSERT dbo.TI_Concepto_MontoTrabajador(I_MontoTrabajadorID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, T_EntidadOrigen, B_Habilitado, B_Eliminado) VALUES(1, 4, '', '', 30, 'BCP', 1, 0)
--INSERT dbo.TI_Concepto_MontoTrabajador(I_MontoTrabajadorID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, T_EntidadOrigen, B_Habilitado, B_Eliminado) VALUES(1, 5, '', 'REINTEGRO', 75, 'UNFV', 1, 0)
--INSERT dbo.TI_Concepto_MontoTrabajador(I_MontoTrabajadorID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, T_EntidadOrigen, B_Habilitado, B_Eliminado) VALUES(1, 3, '', 'REMU DEMO', 164.45, '-', 1, 0)
--GO


--administrativo
INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(4, 1, 1, 0)
INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(5, 1, 1, 0)
INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(6, 1, 1, 0)
INSERT dbo.TC_Trabajador_CategoriaPlanilla(I_TrabajadorID, I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(7, 1, 1, 0)
GO

INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado) VALUES(4, 1, 3, 1, 0)
INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado) VALUES(5, 2, 5, 1, 0)
INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado) VALUES(6, 3, 6, 1, 0)
INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado) VALUES(7, 4, 7, 1, 0)
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


SELECT * FROM dbo.TC_Persona
SELECT * FROM dbo.TC_Trabajador
SELECT * FROM dbo.TC_Docente
SELECT * FROM dbo.TC_Administrativo

select * from dbo.TI_Concepto_MontoTrabajador


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

