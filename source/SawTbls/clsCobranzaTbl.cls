VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCobranzaTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCobranzaTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Cobranza"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Public Function GetTableName() As String
   GetTableName = "Cobranza"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Cliente", GetTableName(), "Codigo", _
         "CodigoCliente", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Vendedor", GetTableName(), "Codigo", _
         "CodigoCobrador", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Moneda", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "CodigoMoneda", Not deleteOnCascade
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sAddPrimaryKey()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,Numero)"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   sAddPrimaryKey
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumerodelCheque"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NroDeLaTarjeta"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreBanco"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoCuentaBancaria"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoConcepto"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NroComprobanteRetIva"
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
   Dim insAnticipoCobrado As clsAnticipoRenglonTbl
   Dim insDocumentoCobrado As clsDocumentoCobradoTbl
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "Numero" & gUtilSQL.getVarCharTypeForDB(12) & " CONSTRAINT nn" & GetTableName() & "Numero NOT NULL, "
   SQL = SQL & "StatusCobranza" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "StatusCobr NOT NULL, "
   SQL = SQL & "Fecha" & gUtilSQL.getDateTypeForDB & " CONSTRAINT nn" & GetTableName() & "Fecha NOT NULL, "
   SQL = SQL & "FechaAnulacion" & gUtilSQL.getDateTypeForDB & ", "
   SQL = SQL & "CodigoCliente" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "CodigoCobrador" & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "TotalDocumentos" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "RetencionIslr" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TotalCobrado" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CobradoEfectivo" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CobradoCheque" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NumerodelCheque" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "CobradoTarjeta" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CualTarjeta" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NroDeLaTarjeta" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "Origen" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TotalOtros" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NombreBanco" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CodigoCuentaBancaria" & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "CodigoConcepto" & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "Moneda" & gUtilSQL.getVarCharTypeForDB(80) & ", "
   SQL = SQL & "CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "RetencionIva" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NroComprobanteRetIva" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "StatusRetencionIva " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "GeneraMovBancario " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GeneraMovB NOT NULL, "
   SQL = SQL & "CobradoAnticipo " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Vuelto " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "DescProntoPago" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "DescProntoPagoPorc" & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "ComisionVendedor" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "AplicaCreditoBancario" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AplicaCred NOT NULL, "
   SQL = SQL & "CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMoneda NOT NULL, "
   SQL = SQL & "TipoDeDocumento" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "TipoDeDocu NOT NULL, "
   SQL = SQL & "NumeroDeComprobanteISLR" & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   SQL = SQL & "NombreOperador" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion" & gUtilSQL.getDateTypeForDB & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   sModifyOtherPropertiesOfFields
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   Set insDocumentoCobrado = New clsDocumentoCobradoTbl
   If insDocumentoCobrado.fCreateTable(valShowMessage) Then
      fCreateTable = True
      Set insAnticipoCobrado = New clsAnticipoRenglonTbl
      If insAnticipoCobrado.fCreateTable(valShowMessage, True) Then
         fCreateTable = True
      End If
      Set insAnticipoCobrado = Nothing
   End If
   Set insDocumentoCobrado = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   Dim insAnticipoCobrado As clsAnticipoRenglonTbl
   Dim insDocumentoCobrado As clsDocumentoCobradoTbl
   On Error GoTo h_ERROR
   Set insDocumentoCobrado = New clsDocumentoCobradoTbl
   insDocumentoCobrado.sDropTable
   Set insDocumentoCobrado = Nothing
   Set insAnticipoCobrado = New clsAnticipoRenglonTbl
   insAnticipoCobrado.mTipoDeAnticipoCobrado = True
   insAnticipoCobrado.sDropTable
   Set insAnticipoCobrado = Nothing
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

