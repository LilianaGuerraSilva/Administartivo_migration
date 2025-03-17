VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxCTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCxCTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Cx C"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania, Numero, TipoCxC"
End Function
Public Function GetTableName() As String
   GetTableName = "CxC"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Adm.Vendedor", GetTableName(), "Consecutivo", _
         "ConsecutivoVendedor", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Moneda", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "CodigoMoneda", Not deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   sAddPrimaryKey
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sAddPrimaryKey()
   Dim campos As String
   On Error GoTo h_ERROR
   campos = "ConsecutivoCompania,Numero,TipoCxc"
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (" & campos & ")"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAddPrimaryKey", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Descripcion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroDocumentoOrigen"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoLote"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Moneda"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "Fecha", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaCancelacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaVencimiento", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaAnulacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "Numero " & gUtilSQL.getVarCharTypeForDB(20) & " CONSTRAINT nn" & GetTableName() & "Numero NOT NULL, "
   SQL = SQL & "Status " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TipoCxc " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "TipoCxC NOT NULL, "
   SQL = SQL & "CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "ConsecutivoVendedor " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "VenCon NOT NULL, "
   SQL = SQL & "Origen " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Fecha " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaCancelacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaVencimiento " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaAnulacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoGravado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Descripcion " & gUtilSQL.getVarCharTypeForLargeTextForDB() & ", "
   SQL = SQL & "Moneda " & gUtilSQL.getVarCharTypeForDB(80) & ", "
   SQL = SQL & "CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "CodigoCc " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "CentroDeCostos " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "SeRetuvoIva " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeRetuvoIv NOT NULL, "
   SQL = SQL & "NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "RefinanciadoSn " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NoAplicaParaLibroDeVentas " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "NoAplicaPa NOT NULL, "
   SQL = SQL & "CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMoneda NOT NULL, "
   SQL = SQL & "NumeroControl" & gUtilSQL.getVarCharTypeForDB(11) & ", "
   SQL = SQL & "NumeroComprobanteFiscal" & gUtilSQL.getVarCharTypeForDB(12) & ", "
   SQL = SQL & "FechaLimiteCambioAMonedaLocal" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "VieneDeCreditoElectronico" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "nnCxCVieneDeCre NOT NULL, "
   SQL = SQL & "NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
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
