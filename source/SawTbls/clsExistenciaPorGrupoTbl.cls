VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsExistenciaPorGrupoTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsExistenciaPorGrupoTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Existencia Por Grupo"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania, CodigoArticulo, codigoGrupo, CodigoTalla, CodigoCOlor"
End Function
Public Function GetTableName() As String
   GetTableName = "ExistenciaPorGrupo"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,CodigoArticulo,CodigoGrupo,CodigoTalla,CodigoColor,Serial,Rollo)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
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
   SQL = SQL & "CodigoArticulo " & gUtilSQL.getVarCharTypeForDB(30) & " CONSTRAINT nn" & GetTableName() & "CodigoArti NOT NULL, "
   SQL = SQL & "CodigoGrupo " & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "codigoGrup NOT NULL, "
   SQL = SQL & "CodigoTalla " & gUtilSQL.getVarCharTypeForDB(3) & " CONSTRAINT nn" & GetTableName() & "CodigoTall NOT NULL, "
   SQL = SQL & "CodigoColor " & gUtilSQL.getVarCharTypeForDB(3) & " CONSTRAINT nn" & GetTableName() & "CodigoColo NOT NULL, "
   SQL = SQL & "Existencia " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Serial" & gUtilSQL.getVarCharTypeForDB(50) & " CONSTRAINT nn" & GetTableName() & "Serial NOT NULL, "
   SQL = SQL & "Rollo" & gUtilSQL.getVarCharTypeForDB(20) & "CONSTRAINT nn" & GetTableName() & "Rollo NOT NULL, "
   SQL = SQL & "CantReservada" & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "Fecha" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "Ubicacion" & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "CodigoLote" & gUtilSQL.getVarCharTypeForDB(10) & ", "
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
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
