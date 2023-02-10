USE [BD_OCRH_GestionPlanillas]
GO

SET IDENTITY_INSERT TC_TipoConcepto ON
GO

INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, 'INGRESOS ', 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES(2, 'DESCUENTOS ', 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES(3, 'APORTES', 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES(4, 'REINTEGROS', 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES(5, 'ENCARGATURAS', 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES(6, 'OTROS INGRESOS', 1, 0)
INSERT TC_TipoConcepto(I_TipoConceptoID, T_TipoConceptoDesc, B_Habilitado, B_Eliminado) VALUES(9, 'NETO', 1, 0)

GO

SET IDENTITY_INSERT TC_TipoConcepto OFF
GO


INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '001', 'BÁSICO', 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '002','BONIF.D.S.276', 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '003','DEC.URG.N°666', 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(2, '004','DESCTO.AFP', 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(3, '005','', 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(4, '006','', 1, 0)
GO


INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('01', 'ACTIVOS', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('02', 'PENSIONISTA', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('03', 'BENEFICIARIO', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('04', 'DESCUENTO JUDICIAL', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('05', 'JUDICIAL', 1, 0)
INSERT TC_TipoPlanilla(C_TipoPlanillaCod, T_TipoPlanillaDesc, B_Habilitado, B_Eliminado) VALUES('99', 'OTROS', 1, 0)
GO


INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '01', 'HABERES', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '02', 'CAFAE', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '03', 'CAS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '04', 'FAG', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '05', 'CESIGRA', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, '06', 'PRÁCTICAS PREPROFESIONALES', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(2, '07', 'PENSIONISTAS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '08', 'VIUDEZ', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '09', 'ORFANDAD', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '10', 'DEPENDENCIA', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(3, '11', 'MONTEPIO', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(4, '12', 'ALIMENTOS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(5, '13', 'DEVENGADOS', 1, 0)
INSERT dbo.TC_ClasePlanilla(I_TipoPlanillaID, C_ClasePlanillaCod, T_ClasePlanillaDesc, B_Habilitado, B_Eliminado) VALUES(6, '99', 'OTROS', 1, 0)
GO

INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, 'Haberes Administrativo', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, 'Haberes Docente', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(1, 'Haberes Médico', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(7, 'Pensiones', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(2, 'CAFAE', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(14, 'Docente Investigador', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(6, 'Practicante', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(14, 'Productividad', 1, 0)
INSERT dbo.TC_CategoriaPlanilla(I_ClasePlanillaID, T_CategoriaPlanillaDesc, B_Habilitado, B_Eliminado) VALUES(14, 'Generadora de Recursos', 1, 0)
GO


INSERT TI_PlantillaPlanilla(I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(1, 1, 0)
INSERT TI_PlantillaPlanilla(I_CategoriaPlanillaID, B_Habilitado, B_Eliminado) VALUES(2, 1, 0)
GO


INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 1, 1, 130, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 2, 1, 130, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 3, 1, 130, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 4, 0, 0, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 5, 1, 0, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(1, 6, 1, 0, NULL, NULL, 1, 0)
GO

INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(2, 1, 1, 130, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(2, 2, 1, 130, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(2, 3, 1, 130, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(2, 4, 0, 0, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(2, 5, 1, 0, NULL, NULL, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, I_Filtro1, I_Filtro2, B_Habilitado, B_Eliminado) VALUES(2, 6, 1, 0, NULL, NULL, 1, 0)
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



--DATA EJEMPLO
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 1, 'Enero')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2022, 2, 'Febreo')
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
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 2, 'Febreo')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 3, 'Marzo')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 4, 'Abril')
INSERT TR_Periodo(I_Anio, I_Mes, T_MesDesc)VALUES(2023, 5, 'Mayo')
GO


INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345678', 'Juan', 'Rodrigo', 'Jiménez', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345679', 'José', 'Medrano', 'Estrada', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345680', 'Abel', 'Ayuso', 'Alcalde', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345681', 'Manolo', 'Chaves', 'Trillo', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345682', 'Carol', 'Toledo', 'Monzón', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345683', 'Rosa', 'Cardona', 'Díaz', 1, 0)
INSERT dbo.TC_Persona(I_TipoDocumentoID, C_NumDocumento, T_Nombre, T_ApellidoPaterno, T_ApellidoMaterno, B_Habilitado, B_Eliminado) VALUES(1, '12345684', 'Sofía', 'Terejo', 'Crespo', 1, 0)
GO


INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(1, '20230101', 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(2, '20230101', 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(3, '20230101', 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(4, '20230101', 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(5, '20230101', 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(6, '20230101', 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(7, '20230101', 1, 1, 0)
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

INSERT dbo.TI_MontoTrabajador(I_TrabajadorID, I_PeriodoID, B_Habilitado, B_Eliminado) VALUES(1, 1, 1, 0)
GO

INSERT dbo.TI_Concepto_MontoTrabajador(I_MontoTrabajadorID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, T_EntidadOrigen, B_Habilitado, B_Eliminado) VALUES(1, 4, '', '', 30, 'BCP', 1, 0)
INSERT dbo.TI_Concepto_MontoTrabajador(I_MontoTrabajadorID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, T_EntidadOrigen, B_Habilitado, B_Eliminado) VALUES(1, 5, '', 'REINTEGRO', 75, 'UNFV', 1, 0)
INSERT dbo.TI_Concepto_MontoTrabajador(I_MontoTrabajadorID, I_ConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Monto, T_EntidadOrigen, B_Habilitado, B_Eliminado) VALUES(1, 3, '', 'REMU DEMO', 164.45, '-', 1, 0)
GO


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


--Estado 1
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('ACTIVO', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('SUSPENDIDO', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('LICENCIA', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('VACANTE', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('ANULADO (BAJA)', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('SIN ASISTENCIA', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('FALLECIDO', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('SUBSIDIADO', 1, 0)

--Estado 2
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('ADMINISTRATIVO PERMANENTE', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('ADMINISTRATIVO CONTRATADO', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('MÉDICO PERMANENTE', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('DOCENTE PERMANENTE', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('DOCENTE CONTRATADO', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('CESANTE ADMINISTRATIVO', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('CESANTE MÉDICO', 1, 0)
INSERT dbo.TC_Vinculo(T_VinculoDesc, B_Habilitado, B_Eliminado) VALUES('SERVIDOR PERMANENTE', 1, 0)


select * from TC_TipoPlanilla
select * from TC_ClasePlanilla
select * from TC_CategoriaPlanilla


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






INSERT dbo.TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '0001', 'MUC', 1, 0)
INSERT dbo.TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '0002', 'BET FIJO', 1, 0)
INSERT dbo.TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '0003', 'BET VARIABLE', 1, 0)
INSERT dbo.TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '0004', 'REMUNERACIÓN DOCENTE', 1, 0)
INSERT dbo.TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '0700', 'FALTAS Y TARDANZAS', 1, 0)
INSERT dbo.TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, B_Habilitado, B_Eliminado) VALUES(1, '5040', 'DSCTO. A FAVOR DE BANCO DE CREDITO DEL PERU', 1, 0)


INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, B_Habilitado, B_Eliminado) VALUES(1, 7, 1, 700, 1, 0)
INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, B_Habilitado, B_Eliminado) VALUES(1, 8, 1, 350, 1, 0)
INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, B_Habilitado, B_Eliminado) VALUES(1, 9, 0, 0, 1, 0)
INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, B_Habilitado, B_Eliminado) VALUES(1, 11, 0, 0, 1, 0)
INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, B_Habilitado, B_Eliminado) VALUES(1, 12, 0, 0, 1, 0)

INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, B_Habilitado, B_Eliminado) VALUES(2, 10, 1, 1100, 1, 0)
INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, B_Habilitado, B_Eliminado) VALUES(2, 11, 0, 0, 1, 0)
INSERT dbo.TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_MontoEstaAqui, M_Monto, B_Habilitado, B_Eliminado) VALUES(2, 12, 0, 0, 1, 0)