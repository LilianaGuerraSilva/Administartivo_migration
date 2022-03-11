VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsMovimientoBancarioTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsMovimientoBancarioTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Movimiento Bancario"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "MovimientoBancario"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Saw.CuentaBancaria", GetTableName(), "Codigo", _
         "CodigoCtaBancaria", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Adm.ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "CodigoConcepto", Not deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,CodigoCtaBancaria,CodigoConcepto,Fecha,ConsecutivoMovimiento)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroDocumento"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Descripcion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NroMovimientoRelacionado"
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
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "ConsecutivoMovimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "CodigoCtaBancaria " & gUtilSQL.getVarCharTypeForDB(5) & " CONSTRAINT nn" & GetTableName() & "CodigoCtaB NOT NULL, "
   SQL = SQL & "CodigoConcepto " & gUtilSQL.getVarCharTypeForDB(8) & " CONSTRAINT nn" & GetTableName() & "CodigoConc NOT NULL, "
   SQL = SQL & "Fecha " & gUtilSQL.getDateTypeForDB() & " CONSTRAINT nn" & GetTableName() & "Fecha NOT NULL, "
   SQL = SQL & "TipoConcepto " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Monto " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NumeroDocumento" & gUtilSQL.getVarCharTypeForDB(25) & ", "
   SQL = SQL & "Descripcion " & gUtilSQL.getVarCharTypeForLargeTextForDB() & ", "
   SQL = SQL & "GeneraImpuestoBancario" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GeneraImpu NOT NULL, "
   SQL = SQL & "AlicuotaImpBancario " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NroMovimientoRelacionado " & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "GeneradoPor " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "ImprimirCheque " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCh NOT NULL, "
   SQL = SQL & "ConciliadoSn " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Conciliado NOT NULL, "
   SQL = SQL & "NroConciliacion " & gUtilSQL.getVarCharTypeForDB(9) & " CONSTRAINT nn" & GetTableName() & "NroConcili NOT NULL, "
   SQL = SQL & "GenerarAsientoDeRetiroEnCuenta" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GenerarAsi NOT NULL, "
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

