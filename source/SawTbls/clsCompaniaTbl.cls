VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCompaniaTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCompaniaTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Compañía"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "COMPANIA"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
'   gDbUtil.AddForeignKeySegunTablaPrimaria "Comun.Ciudad", GetTableName(), "", _
'         "", usarSoloUnElementoEnLaRelacion, "NombreCiudad", "Ciudad", _
'         Not deleteOnCascade
'   gDbUtil.AddForeignKeySegunTablaPrimaria "Comun.Ciiu", GetTableName(), "Codigo", _
'         "CodigoCiiu", usarSoloUnElementoEnLaRelacion, "TipoTabla", "TablaCiiu", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Comun.Pais", GetTableName(), "", _
         "", usarSoloUnElementoEnLaRelacion, "Codigo", "CodigoPais", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Comun.MunicipioCiudad", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Consecutivo", "ConsecutivoMunicipio", Not deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName & "  add CONSTRAINT p_" & GetTableName & " primary key (ConsecutivoCompania)"
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName & "  add CONSTRAINT u_CompaniaCodigo UNIQUE(Codigo)"
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName & "  add CONSTRAINT u_CompaniaNombre UNIQUE(Nombre)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "Codigo " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "Codigo NOT NULL, "
   SQL = SQL & "Nombre " & gUtilSQL.getVarCharTypeForDB(50) & " CONSTRAINT nn" & GetTableName() & "Nombre NOT NULL, "
   SQL = SQL & "NumeroDeRif " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NumeroNit " & gUtilSQL.getVarCharTypeForDB(12) & ", "
   SQL = SQL & "TipoDeContribuyente " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Direccion " & gUtilSQL.getVarCharTypeForDB(255) & ", "
   SQL = SQL & "Ciudad" & gUtilSQL.getVarCharTypeForDB(100) & " CONSTRAINT nn" & GetTableName() & "Ciudad NOT NULL, "
   SQL = SQL & "Estado " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & ", "
   SQL = SQL & "Telefono1 " & gUtilSQL.getVarCharTypeForDB(9) & ", "
   SQL = SQL & "Telefono2 " & gUtilSQL.getVarCharTypeForDB(9) & ", "
   SQL = SQL & "Telefono3 " & gUtilSQL.getVarCharTypeForDB(9) & ", "
   SQL = SQL & "CodArea1 " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "CodArea2 " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "CodArea3 " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "Fax " & gUtilSQL.getVarCharTypeForDB(9) & ", "
   SQL = SQL & "CodAreafax " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "FechaConversion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "CompaniaConvertida " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "CompaniaCo NOT NULL, "
   SQL = SQL & "UsaModuloDeActivoFijo " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaModuloD NOT NULL, "
   SQL = SQL & "UsaConexionConIslr " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UConexISLR NOT NULL, "
   SQL = SQL & "UsaConexionConAxi" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UConexAxi NOT NULL, "
   SQL = SQL & "UsaCentroDeCostos" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCentroD NOT NULL, "
   SQL = SQL & "UsaCostoDeVentas" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCostoDe NOT NULL, "
   SQL = SQL & "UsaAuxiliares" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaAuxilia NOT NULL, "
   SQL = SQL & "UsaModuloDeContabilidad" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaModuCon NOT NULL, "
   SQL = SQL & "EsCatalogoGeneral " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsCatalogo NOT NULL, "
   SQL = SQL & "TipoDeContribuyenteIva " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TablaCiiu " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "TablaCIIU NOT NULL, "
   SQL = SQL & "CodigoCiiu " & gUtilSQL.getVarCharTypeForDB(6) & " CONSTRAINT nn" & GetTableName() & "CodigoCIIU NOT NULL, "
   SQL = SQL & "PorcentajeDePatente " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PersonaFiscal " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Pais" & gUtilSQL.getVarCharTypeForDB(80) & ", "
   SQL = SQL & "CodigoPais" & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoPais NOT NULL, "
   SQL = SQL & "EsAgenteDeRetencionIVA" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsAgenteDe NOT NULL, "
   SQL = SQL & "S10Ids" & gUtilSQL.getVarCharTypeForDB(4000) & ", "
   SQL = SQL & "UsaS10" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TipoDocumentoIdentificacion" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "EstaIntegradaConNomina" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EstaIntegr NOT NULL, "
   SQL = SQL & "CodigoDeIntegracion" & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "ConsecutivoMunicipio" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "CodigoContribuyente" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CodigoMoneda" & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "StatusCompania" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "StatusComp NOT NULL, "
   SQL = SQL & "StatusMonetaryReconversionProcess" & gUtilSQL.getVarCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "StatusMone NOT NULL, "
   SQL = SQL & "ConectadaConG360" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ConecConG360 NOT NULL, "
   SQL = SQL & "UsaInformesFinancieros" & " " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaInfFinan NOT NULL, "
   SQL = SQL & "NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   fCreateTable = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   On Error GoTo h_ERROR
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

