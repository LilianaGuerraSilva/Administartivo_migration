VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsNotaDeEntradaSalidaTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsNotaDeEntradaSalidaTbl"
Private Const CM_MESSAGE_NAME As String = "Nota de Entrada/Salida"
Private Const GetGender = Enum_Gender.eg_Male

Public Function GetTableName() As String
   GetTableName = "NotaDeEntradaSalida"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Cliente", GetTableName(), "Codigo", "CodigoCliente", Not usarSoloUnElementoEnLaRelacion, "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.Execute gDefDatabase.Conexion, "SET DATEFORMAT dmy  ALTER TABLE NotaDeEntradaSalida ADD CONSTRAINT fk_NotaDeEntradaSalidaAlma FOREIGN KEY (ConsecutivoCompania,ConsecutivoAlmacen) REFERENCES Saw.Almacen (ConsecutivoCompania,Consecutivo) ON UPDATE NO ACTION ON DELETE NO ACTION"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,NumeroDocumento)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Comentarios"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoLote"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "Fecha", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   Dim insRenglonNotaES As clsRenglonNotaESTbl
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NumeroDocumento " & gUtilSQL.getVarCharTypeForDB(11) & " CONSTRAINT nn" & GetTableName() & "NumeroDocu NOT NULL, "
   SQL = SQL & "TipodeOperacion " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "CodigoAlmacen " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "Fecha " & gUtilSQL.getDateTypeForDB() & " CONSTRAINT nn" & GetTableName() & "Fecha NOT NULL, "
   SQL = SQL & "Comentarios " & gUtilSQL.getVarCharTypeForDB(255) & ", "
   SQL = SQL & "CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "StatusNotaEntradaSalida" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ConsecutivoAlmacen" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "GeneradoPor" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "_NotDeEntSalGePo DEFAULT '0',"
   SQL = SQL & "ConsecutivoDocumentoOrigen" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "_NotDeEntSalCoDoOr DEFAULT (0), "
   SQL = SQL & "TipoNotaProduccion" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "_NotDeEntSalTiNoPr DEFAULT '0', "
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
   Set insRenglonNotaES = New clsRenglonNotaESTbl
   If insRenglonNotaES.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insRenglonNotaES = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   Dim insRenglonNotaES As clsRenglonNotaESTbl
   On Error GoTo h_ERROR
   Set insRenglonNotaES = New clsRenglonNotaESTbl
   insRenglonNotaES.sDropTable
   Set insRenglonNotaES = Nothing
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
