VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAnticipoTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsAnticipoTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Anticipo"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "anticipo"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
    gDbUtil.AddForeignKeySegunTablaPrimaria "Moneda", "Anticipo", "", "", usarSoloUnElementoEnLaRelacion, "Codigo", "CodigoMoneda", Not deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (" & "ConsecutivoCompania" & "," & "ConsecutivoAnticipo" & ")"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Numero"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Descripcion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoCliente"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoProveedor"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoCuentaBancaria"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoConceptoBancario"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "Fecha", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaAnulacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaCancelacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaDevolucion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim sql As String
   On Error GoTo h_ERROR
   sql = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   sql = sql & "ConsecutivoCompania" & " " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   sql = sql & "ConsecutivoAnticipo" & " " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   sql = sql & "Status" & " " & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "Tipo" & " " & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "Fecha" & " " & gUtilSQL.getDateTypeForDB() & ", "
   sql = sql & "Numero" & " " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   sql = sql & "CodigoCliente" & " " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   sql = sql & "CodigoProveedor" & " " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "CodigoBeneficiario" & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "CodigoBene NOT NULL, "
   sql = sql & "NombreBeneficiario" & gUtilSQL.getVarCharTypeForDB(60) & ", "
   sql = sql & "Moneda" & " " & gUtilSQL.getVarCharTypeForDB(80) & ", "
   sql = sql & "Cambio" & " " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   sql = sql & "GeneraMovBancario" & " " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GeneraMovB NOT NULL, "
   sql = sql & "CodigoCuentaBancaria" & " " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   sql = sql & "CodigoConceptoBancario" & " " & gUtilSQL.getVarCharTypeForDB(8) & ", "
   sql = sql & "GeneraImpuestoBancario" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GeneraImpu NOT NULL, "
   sql = sql & "FechaAnulacion" & " " & gUtilSQL.getDateTypeForDB() & ", "
   sql = sql & "FechaCancelacion" & " " & gUtilSQL.getDateTypeForDB() & ", "
   sql = sql & "FechaDevolucion" & " " & gUtilSQL.getDateTypeForDB() & ", "
   sql = sql & "Descripcion" & " " & gUtilSQL.getVarCharTypeForLargeTextForDB() & ", "
   sql = sql & "MontoTotal" & " " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   sql = sql & "MontoUsado" & " " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   sql = sql & "MontoDevuelto" & " " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   sql = sql & "MontoDiferenciaEnDevolucion" & " " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   sql = sql & "DiferenciaEsIdb" & " " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Diferencia NOT NULL, "
   sql = sql & "EsUnaDevolucion" & " " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "esUnaDevol NOT NULL, "
   sql = sql & "NumeroDelAnticipoDevuelto" & " " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   sql = sql & "NumeroCheque" & " " & gUtilSQL.getVarCharTypeForDB(15) & ", "
   sql = sql & "AsociarAnticipoACotiz" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AsociarAnt NOT NULL, "
   sql = sql & "NumeroCotizacion" & gUtilSQL.getVarCharTypeForDB(11) & ", "
   sql = sql & "CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMoneda NOT NULL, "
   sql = sql & "ConsecutivoRendicion" & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   sql = sql & "NombreOperador" & " " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   sql = sql & "FechaUltimaModificacion" & " " & gUtilSQL.getDateTypeForDB() & ", "
   sql = sql & "AsociarAnticipoACaja" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AsociarACaja NOT NULL, "
   sql = sql & "ConsecutivoCaja" & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   sql = sql & "Generadopor" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Generadopo NOT NULL, "
   sql = sql & "NumeroDeCobranzaAsociado" & gUtilSQL.getVarCharTypeForDB(12) & ", "
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
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


