VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAnticipoRenglonTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Public mTipoDeAnticipoCobrado As Boolean
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsAnticipoRenglonTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   If mTipoDeAnticipoCobrado Then
      CM_MESSAGE_NAME = "Anticipo Cobrado"
   Else
      CM_MESSAGE_NAME = "Anticipo Pagado"
   End If
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania,NumeroComprobante,Secuencial"
End Function
Public Function GetTableName() As String
   If Not mTipoDeAnticipoCobrado Then
      GetTableName = "anticipoPagado"
   Else
      GetTableName = "anticipoCobrado"
   End If
End Function
Public Function getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA() As String
   If mTipoDeAnticipoCobrado Then
      getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA = "NumeroCobranza"
   Else
      getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA = "NumeroComprobante"
   End If
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   If mTipoDeAnticipoCobrado Then
      gDbUtil.AddForeignKeySegunTablaPrimaria "cobranza", GetTableName(), "Numero", _
            getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA, Not usarSoloUnElementoEnLaRelacion, _
            "ConsecutivoCompania", "ConsecutivoCompania", deleteOnCascade
   Else
      gDbUtil.AddForeignKeySegunTablaPrimaria "Pago", GetTableName(), "NumeroComprobante", _
            getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA, Not usarSoloUnElementoEnLaRelacion, _
            "ConsecutivoCompania", "ConsecutivoCompania", deleteOnCascade
      
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   If mTipoDeAnticipoCobrado Then
      gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania," & getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA() & ",Secuencial)"
   Else
      gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania," & getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA() & ",Secuencial)"
   End If
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCreateTable(ByVal valShowMessage As Boolean, ByVal valTipoDeAnticipoCobrado As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   mTipoDeAnticipoCobrado = valTipoDeAnticipoCobrado
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   If Not mTipoDeAnticipoCobrado Then
      SQL = SQL & getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA() & " " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "NumeroComp NOT NULL, "
   Else
      SQL = SQL & getFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA() & " " & gUtilSQL.getVarCharTypeForDB(12) & " CONSTRAINT nn" & GetTableName() & "NumeroCobr NOT NULL, "
   End If
   SQL = SQL & "Secuencial " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Secuencial NOT NULL, "
   SQL = SQL & "ConsecutivoAnticipoUsado " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NumeroAnticipo " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "MontoOriginal " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoRestanteAlDia " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "SimboloMoneda " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   SQL = SQL & "Cambio " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "MontoTotalDelAnticipo " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "MontoAplicado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   If mTipoDeAnticipoCobrado Then
      SQL = SQL & "MontoAplicadoMonedaOriginal " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
      SQL = SQL & "CambioAMonedaLocal " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
      SQL = SQL & "SimboloMonedaDelAbono " & gUtilSQL.getVarCharTypeForDB(4) & ", "
   End If
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
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   On Error GoTo h_ERROR
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
