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


INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(1, '001', 'BÁSICO', 135.0, 1, 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(1, '002','BONIF.D.S.276', 7.5, 1, 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(1, '003','DEC.URG.N°666', 15.4, 1, 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(2, '004','DESCTO.AFP', 0.08, 0, 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(3, '005','', 0, 0, 1, 0)
INSERT TC_Concepto(I_TipoConceptoID, C_ConceptoCod, T_ConceptoDesc, M_Valor, B_EsFijo, B_Habilitado, B_Eliminado) VALUES(4, '006','', 0, 0, 1, 0)
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


INSERT TI_PlantillaPlanilla(I_ClasePlanillaID, B_Habilitado, B_Eliminado) VALUES(1, 1, 0)
GO


INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 1, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 2, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 3, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 4, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 5, 1, 0)
INSERT TI_PlantillaPlanilla_Concepto(I_PlantillaPlanillaID, I_ConceptoID, B_Habilitado, B_Eliminado) VALUES(1, 6, 1, 0)
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


INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_ClasePlanillaID, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(1, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_ClasePlanillaID, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(2, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_ClasePlanillaID, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(3, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_ClasePlanillaID, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(4, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_ClasePlanillaID, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(5, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_ClasePlanillaID, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(6, '20230101', 1, 1, 1, 0)
INSERT dbo.TC_Trabajador(I_PersonaID, D_FechaIngreso, I_ClasePlanillaID, I_VinculoID, B_Habilitado, B_Eliminado) VALUES(7, '20230101', 1, 1, 1, 0)
GO

INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado) VALUES(1, 1, 1, 1, 0)
INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado) VALUES(2, 2, 2, 1, 0)
INSERT dbo.TC_Docente(I_TrabajadorID, I_CategoriaDocenteID, I_HorasDocenteID, B_Habilitado, B_Eliminado) VALUES(3, 3, 3, 1, 0)
GO

INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado) VALUES(4, 1, 3, 1, 0)
INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado) VALUES(5, 2, 5, 1, 0)
INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado) VALUES(6, 3, 6, 1, 0)
INSERT dbo.TC_Administrativo(I_TrabajadorID, I_GrupoOcupacionalID, I_NivelRemunerativoID, B_Habilitado, B_Eliminado) VALUES(7, 4, 7, 1, 0)
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


SELECT * FROM DBO.TC_Vinculo

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


SELECT * FROM dbo.TC_Persona
SELECT * FROM dbo.TC_Trabajador
SELECT * FROM dbo.TC_Docente
SELECT * FROM dbo.TC_Administrativo


--PERSONAL DOCENTE
SELECT p.T_Nombre, p.T_ApellidoPaterno, p.T_ApellidoMaterno, td.T_TipoDocumentoDesc, p.C_NumDocumento, t.D_FechaIngreso, cp.T_ClasePlanillaDesc, 
	cd.C_CategoriaDocenteCod, cd.T_CategoriaDocenteDesc, dd.C_DedicacionDocenteCod, dd.T_DedicacionDocenteDesc, hd.I_Horas
FROM dbo.TC_Docente d
INNER JOIN dbo.TC_Trabajador t ON t.I_TrabajadorID = d.I_TrabajadorID
INNER JOIN dbo.TC_Persona p ON p.I_PersonaID = t.I_PersonaID
INNER JOIN dbo.TC_TipoDocumento td ON td.I_TipoDocumentoID = p.I_TipoDocumentoID
INNER JOIN dbo.TC_ClasePlanilla cp ON cp.I_ClasePlanillaID = t.I_ClasePlanillaID
INNER JOIN dbo.TC_CategoriaDocente cd ON cd.I_CategoriaDocenteID = d.I_CategoriaDocenteID
INNER JOIN dbo.TC_HorasDocente hd ON hd.I_HorasDocenteID = d.I_HorasDocenteID
INNER JOIN dbo.TC_DedicacionDocente dd ON dd.I_DedicacionDocenteID = hd.I_DedicacionDocenteID


--PERSONAL ADMINISTRATIVO
SELECT p.T_Nombre, p.T_ApellidoPaterno, p.T_ApellidoMaterno, td.T_TipoDocumentoDesc, p.C_NumDocumento, t.D_FechaIngreso, cp.T_ClasePlanillaDesc,
	gr.C_GrupoOcupacionalCod, gr.T_GrupoOcupacionalDesc, nr.C_NivelRemunerativoCod
FROM dbo.TC_Administrativo a
INNER JOIN dbo.TC_Trabajador t ON t.I_TrabajadorID = a.I_TrabajadorID
INNER JOIN dbo.TC_Persona p ON p.I_PersonaID = t.I_PersonaID
INNER JOIN dbo.TC_TipoDocumento td ON td.I_TipoDocumentoID = p.I_TipoDocumentoID
INNER JOIN dbo.TC_ClasePlanilla cp ON cp.I_ClasePlanillaID = t.I_ClasePlanillaID
INNER JOIN dbo.TC_NivelRemunerativo nr ON nr.I_NivelRemunerativoID = a.I_NivelRemunerativoID
INNER JOIN dbo.TC_GrupoOcupacional gr ON gr.I_GrupoOcupacionalID = a.I_GrupoOcupacionalID



