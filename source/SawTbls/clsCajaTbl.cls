VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCajaTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCajaTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Caja"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "Caja"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (" & "ConsecutivoCompania" & "," & "ConsecutivoCaja" & ")"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Comando"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "SerialDeMaquinaFiscal"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "PrimerNumeroCompFiscal"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania" & " " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "ConsecutivoCaja" & " " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NombreCaja" & " " & gUtilSQL.getVarCharTypeForDB(60) & " CONSTRAINT nn" & GetTableName() & "NombreCaja NOT NULL, "
   SQL = SQL & "UsaGaveta" & " " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaGaveta NOT NULL, "
   SQL = SQL & "Puerto" & " " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Comando" & " " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "PermitirAbrirSinSupervisor" & " " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "PermitirAb NOT NULL, "
   SQL = SQL & "UsaAccesoRapido" & " " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaAccesoR NOT NULL, "
   SQL = SQL & "UsaMaquinaFiscal" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaMaquina NOT NULL, "
   SQL = SQL & "ModeloDeMaquinaFiscal" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "SerialDeMaquinaFiscal" & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "PuertoMaquinaFiscal" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "AbrirGavetaDeDinero" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AbrirGavet NOT NULL, "
   SQL = SQL & "PrimerNumeroCompFiscal" & gUtilSQL.getVarCharTypeForDB(12) & ", "
   SQL = SQL & "NumeroNCFiscal" & gUtilSQL.getVarCharTypeForDB(12) & ", "
   SQL = SQL & "IpParaConexion" & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "MascaraSubred" & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "Gateway" & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "NumeroDeCaja" & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "TipoConexion" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombreOperador" & " " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion" & " " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "PermitirDescripcionDelArticuloExtendida" & gUtilSQL.getCharTypeForDB(1) & " , "
   SQL = SQL & "PermitirNombreDelClienteExtendido" & gUtilSQL.getCharTypeForDB(1) & " , "
   SQL = SQL & "UsarModoDotNet" & gUtilSQL.getCharTypeForDB(1) & " , "
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

