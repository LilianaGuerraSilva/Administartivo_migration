VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsDocumentoCobradoTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsDocumentoCobradoTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Documento Cobrado"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "DocumentoCobrado"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Cobranza", GetTableName(), "Numero", _
         "NumeroCobranza", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", deleteOnCascade
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   sAddPrimaryKey
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sAddPrimaryKey()
   On Error GoTo h_ERROR 'dejar public, se usa en control version saw
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,NumeroCobranza,NumeroDelDocumentoCobrado,TipoDeDocumentoCobrado)"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAddPrimaryKey", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroDelDocumentoCobrado"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaComprobanteRetIva", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
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
   SQL = SQL & "NumeroCobranza " & gUtilSQL.getVarCharTypeForDB(12) & " CONSTRAINT nn" & GetTableName() & "NumeroCobr NOT NULL, "
   SQL = SQL & "NumeroDelDocumentoCobrado " & gUtilSQL.getVarCharTypeForDB(20) & " CONSTRAINT nn" & GetTableName() & "NroDocCobr NOT NULL, "
   SQL = SQL & "TipoDeDocumentoCobrado " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "TipoDeDocu NOT NULL, "
   SQL = SQL & "MontoOriginalRestanteAlDiaDelc " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "SimboloMonedaDeCxC " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "MontoTotalDeCxC " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "CodigoMonedaDeCxC" & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "CambioAMonedaDeCobranza" & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "MontoEnMonedaOriginalDeCxC" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIvaDeCxC " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIvaRetenido " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NumeroComprobanteRetIva " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   SQL = SQL & "FechaComprobanteRetIva " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "SeRetuvoIva " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeRetuvoIV NOT NULL, "
   SQL = SQL & "SeContabilizoIvaDiferido" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeContabil NOT NULL, "
   SQL = SQL & "MontoAbonadoEnMonedaOriginal " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "CambioAMonedaLocal " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "SimboloMonedaDelAbono " & gUtilSQL.getVarCharTypeForDB(4) & ", "
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

