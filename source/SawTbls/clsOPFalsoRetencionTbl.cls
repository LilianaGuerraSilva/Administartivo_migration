VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsOPFalsoRetencionTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsOPFalsoRetencionTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "O PFalso Retencion"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "OPFalsoRetencion"
End Function

Private Sub sAddForeignKeys()

   On Error GoTo h_ERROR

h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,NumeroPago,CodigoRetencion)"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoRetencion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "LeyendaSeAplicoRetencion"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(8, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NumeroPago " & gUtilSQL.getVarCharTypeForDB(25) & " CONSTRAINT nn" & GetTableName() & "NumeroPago NOT NULL, "
   SQL = SQL & "NumeroControl " & gUtilSQL.getVarCharTypeForDB(11) & ", "
   SQL = SQL & "MontoRestanteAlDiaDelPago " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CodigoRetencion " & gUtilSQL.getVarCharTypeForDB(6) & " CONSTRAINT nn" & GetTableName() & "CodigoRete NOT NULL, "
   SQL = SQL & "MontoOriginalCxp " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NumeroDelDocumentoPagado " & gUtilSQL.getVarCharTypeForDB(25) & ", "
   SQL = SQL & "PorcentajeRetencion " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Sustraendo " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeBaseImponible " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoBaseImponible " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoRetencion " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "ParaPagosMayoresDe " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "SeHizoLaRetencion " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeHizoLaRe NOT NULL, "
   SQL = SQL & "LeyendaSeAplicoRetencion " & gUtilSQL.getVarCharTypeForDB(1) & ", "
   SQL = SQL & "Secuencial " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoCambio " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CodigoMonedaDeCxP " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "SimboloMonedaDeCxP " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "CambioAMonedaDelPago " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoEnMonedaOriginalDeCxP " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoTotalDeCxP " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "RetenerIva " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "RetenerIVA NOT NULL, "
   SQL = SQL & "MontoIvaRetenido " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "SeHizoLaRetencionIva " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeHizoLaRe NOT NULL, "
   SQL = SQL & "EsUnaCuentaATerceros" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsUnaCuent NOT NULL, "
   SQL = SQL & "CodigoProveedorOriginalServicio" & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "CodigoProv NOT NULL, "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
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

