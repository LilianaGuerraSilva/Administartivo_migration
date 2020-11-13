VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRetPagoTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsRetPagoTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Ret Pago"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "RetPago"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Adm.Proveedor", GetTableName(), "CodigoProveedor", _
         "CodigoProveedor", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,NumeroComprobante)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoLote"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "GeneradoPor"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CampoExtra1"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CampoExtra2"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CampoExtra3"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "Fecha", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   Dim insRetDocumentoPagado As clsRetDocumentoPagadoTbl
   
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NumeroComprobante " & gUtilSQL.getNumericTypeForDB(9, 0) & " NOT NULL, "
   SQL = SQL & "NumeroReferencia " & gUtilSQL.getVarCharTypeForDB(25) & " CONSTRAINT nn" & GetTableName() & "NumeroRefe NOT NULL, "
   SQL = SQL & "Fecha " & gUtilSQL.getDateTypeForDB() & " CONSTRAINT nn" & GetTableName() & "Fecha NOT NULL, "
   SQL = SQL & "MesAplicacion " & gUtilSQL.getNumericTypeForDB(2, 0) & ", "
   SQL = SQL & "AnoAplicacion " & gUtilSQL.getNumericTypeForDB(2, 0) & ", "
   SQL = SQL & "CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "GeneradoPor " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CampoExtra1 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CampoExtra2 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CampoExtra3 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OrdenadoPorCodRet " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "OrdenadoPo NOT NULL, "
   SQL = SQL & "PrimerNumeroCompRetencion" & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   SQL = SQL & "NroComprobanteRetISLR" & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   SQL = SQL & "Origen" & gUtilSQL.getCharTypeForDB(1) & ", "
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
   
   Set insRetDocumentoPagado = New clsRetDocumentoPagadoTbl
   If insRetDocumentoPagado.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insRetDocumentoPagado = Nothing
   
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   Dim insRetDocumentoPagado As clsRetDocumentoPagadoTbl
   
   On Error GoTo h_ERROR
   Set insRetDocumentoPagado = New clsRetDocumentoPagadoTbl
   insRetDocumentoPagado.sDropTable
   Set insRetDocumentoPagado = Nothing
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

