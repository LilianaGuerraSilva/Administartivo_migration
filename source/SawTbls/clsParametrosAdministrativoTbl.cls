VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsParametrosAdministrativoTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True

Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsParametrosAdministrativoTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Parametros Administrativo"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe Secuencial"
End Function
Public Function GetTableName() As String
   GetTableName = "ParametrosAdministrativo"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (Secuencial)"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim sql As String
   On Error GoTo h_ERROR
   sql = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   sql = sql & "Secuencial " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Secuencial NOT NULL, "
   sql = sql & "FormaDeEscogerCompania " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "FormaDeEsc NOT NULL, "
   sql = sql & "EscogerCompaniaAlEntrar " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EscogerCom NOT NULL, "
   sql = sql & "EscogerUltimaCompaniaUsada " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EscogerUlt NOT NULL, "
   sql = sql & "UsarZonaCobranza " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "usarZonaCo NOT NULL, "
   sql = sql & "UsaCodigoClienteEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCodigoC NOT NULL, "
   sql = sql & "UsaCodigoVendedorEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCodigoV NOT NULL, "
   sql = sql & "UsarCodigoProveedorEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsarCodigo NOT NULL, "
   sql = sql & "UsaCodigoBancoEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCodigoB NOT NULL, "
   sql = sql & "VerificarFacturasManualesFaltantes " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "VerificarF NOT NULL, "
   sql = sql & "UsaCodigoConceptoBancarioEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCodigoC NOT NULL, "
   sql = sql & "GenerarMovBancarioDesdePago " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GenerarMov NOT NULL, "
   sql = sql & "NombreMonedaLocal " & gUtilSQL.getVarCharTypeForDB(80) & " CONSTRAINT nn" & GetTableName() & "NombreMone NOT NULL, "
   sql = sql & "NombreMonedaExtranjera " & gUtilSQL.getVarCharTypeForDB(80) & " CONSTRAINT nn" & GetTableName() & "NombreMone NOT NULL, "
   sql = sql & "GenerarMovBancarioDesdeCobro " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GenerarMov NOT NULL, "
   sql = sql & "MandarMensajeNumeroDeMovimientoBancario " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "MandarMens NOT NULL, "
   sql = sql & "SugerirConsecutivoEnCobranza " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SugerirCon NOT NULL, "
   sql = sql & "NumFacturasManualesFaltantes " & gUtilSQL.getNumericTypeForDB(4, 0) & " CONSTRAINT nn" & GetTableName() & "NumFactura NOT NULL, "
   sql = sql & "LongitudCodigoVendedor " & gUtilSQL.getNumericTypeForDB(4, 0) & " CONSTRAINT nn" & GetTableName() & "LongitudCo NOT NULL, "
   sql = sql & "LongitudCodigoCliente " & gUtilSQL.getNumericTypeForDB(4, 0) & " CONSTRAINT nn" & GetTableName() & "LongitudCo NOT NULL, "
   sql = sql & "PermitirEditarIVAenCxC_CxP " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "PermitirEd NOT NULL, "
   sql = sql & "LongitudCodigoProveedor " & gUtilSQL.getNumericTypeForDB(4, 0) & " CONSTRAINT nn" & GetTableName() & "LongitudCo NOT NULL, "
   sql = sql & "ConfirmarImpresionPorSecciones " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ConfirmarI NOT NULL, "
   sql = sql & "GenerarCxpDesdeCompra " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GenerarCxP NOT NULL, "
   sql = sql & "PermitirSobregiro " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "PermitirSo NOT NULL, "
   sql = sql & "SeResincronizaronLosSupervisores " & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "DevolucionReversoSeGeneraComo " & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "AvisoDeClienteConDeuda " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AvisoDeCli NOT NULL, "
   sql = sql & "MontoApartirDelCualEnviarAvisoDeuda " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   sql = sql & "OrdenamientoDeCodigoString " & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "ImprimirComprobanteDeCxc " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCo NOT NULL, "
   sql = sql & "ImprimirComprobanteDeCxp " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCo NOT NULL, "
   sql = sql & "CodigoMonedaLocal " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMone NOT NULL, "
   sql = sql & "CodigoMonedaExtranjera " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMone NOT NULL, "
   sql = sql & "AvisarSiProveedorTieneAnticipos  " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AvisarSiPr NOT NULL, "
   sql = sql & "EsSistemaParaIG " & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "PuedeUsarS10" & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "S10DBName" & gUtilSQL.getVarCharTypeForDB(128) & ", "
   sql = sql & "CodigoEmpresaIntegrada " & gUtilSQL.getCharTypeForDB(4) & ", "
   SQL = SQL & "ValidarRifEnLaWeb" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ValidarRif NOT NULL, "
   sql = sql & "NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   sql = sql & "FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & ", "
   sql = sql & "BuscarClienteXRifAlFacturar" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "BuscarClie NOT NULL, "
   sql = sql & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, sql
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   sModifyOtherPropertiesOfFields
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   fCreateTable = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
