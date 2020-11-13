VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPagoTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsPagoTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Pago"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "Pago"
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
   gDbUtil.AddForeignKeySegunTablaPrimaria "Saw.CuentaBancaria", GetTableName(), "Codigo", _
         "CodigoCuentaBancaria", Not usarSoloUnElementoEnLaRelacion, _
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
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoConcepto"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "DescripcionPago"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Moneda"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Beneficiario"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "Fecha", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaAnulacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   Dim insRetPago As clsRetPagoTbl
   Dim insDocumentoPagado As clsDocumentoPagadoTbl
   Dim insOPFalsoRetencion As clsOPFalsoRetencionTbl
   Dim insAnticipoPagado As clsAnticipoRenglonTbl
   
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NumeroComprobante " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "NumeroComp NOT NULL, "
   SQL = SQL & "NumeroCheque " & gUtilSQL.getVarCharTypeForDB(25) & " CONSTRAINT nn" & GetTableName() & "NumeroCheq NOT NULL, "
   SQL = SQL & "StatusOrdenDePago " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "StatusOrde NOT NULL, "
   SQL = SQL & "Fecha " & gUtilSQL.getDateTypeForDB() & " CONSTRAINT nn" & GetTableName() & "Fecha NOT NULL, "
   SQL = SQL & "FechaAnulacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "TotalDocumentos " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TotalRetenido " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TotalOtros " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoCheque " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "FormaDePago " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CodigoCuentaBancaria " & gUtilSQL.getVarCharTypeForDB(5) & " CONSTRAINT nn" & GetTableName() & "CodigoCuen NOT NULL, "
   SQL = SQL & "CodigoConcepto " & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "AplicaDebitoBancario " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AplicaDebi NOT NULL, "
   SQL = SQL & "DescripcionPago " & gUtilSQL.getVarCharTypeForLargeTextForDB & ", "
   SQL = SQL & "Moneda " & gUtilSQL.getVarCharTypeForDB(80) & ", "
   SQL = SQL & "CambioaBolivares " & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "TotalRetenidoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "EsPagoHistorico " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsPagoHist NOT NULL, "
   SQL = SQL & "PagadoAnticipo " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TotalAnticiposSinUsar " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Beneficiario " & gUtilSQL.getVarCharTypeForDB(60) & ", "
   SQL = SQL & "CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMoneda NOT NULL, "
  
   SQL = SQL & "Origen " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Origen NOT NULL, "
   SQL = SQL & "NumeroLote " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "NumeroLote NOT NULL, "
   
   SQL = SQL & "TotalRetenidoImpuestoMunicipal" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
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
   Set insOPFalsoRetencion = New clsOPFalsoRetencionTbl
   If insOPFalsoRetencion.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insOPFalsoRetencion = Nothing

   Set insDocumentoPagado = New clsDocumentoPagadoTbl
   If insDocumentoPagado.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insDocumentoPagado = Nothing

   Set insRetPago = New clsRetPagoTbl
   If insRetPago.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insRetPago = Nothing
   Set insAnticipoPagado = New clsAnticipoRenglonTbl
   If insAnticipoPagado.fCreateTable(valShowMessage, False) Then
      fCreateTable = True
   End If
   Set insAnticipoPagado = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   Dim insRetPagoNavigator As clsRetPagoTbl
   Dim insDocumentoPagado As clsDocumentoPagadoTbl
   Dim insAnticipoPagado As clsAnticipoRenglonTbl
   Dim insOPFalsoRetencion As clsOPFalsoRetencionTbl
   On Error GoTo h_ERROR
   Set insRetPagoNavigator = New clsRetPagoTbl
   insRetPagoNavigator.sDropTable
   Set insRetPagoNavigator = Nothing
   
   Set insOPFalsoRetencion = New clsOPFalsoRetencionTbl
   insOPFalsoRetencion.sDropTable
   Set insOPFalsoRetencion = Nothing
   Set insDocumentoPagado = New clsDocumentoPagadoTbl
   insDocumentoPagado.sDropTable
   Set insDocumentoPagado = Nothing
   Set insAnticipoPagado = New clsAnticipoRenglonTbl
   insAnticipoPagado.mTipoDeAnticipoCobrado = False
   insAnticipoPagado.sDropTable
   Set insAnticipoPagado = Nothing
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

