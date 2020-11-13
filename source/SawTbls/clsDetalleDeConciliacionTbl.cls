VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsDetalleDeConciliacionTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsDetalleDeConciliacionTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Detalle De Conciliacion"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania, NroConciliacion, CodigoCuenta, Consecutivo"
End Function
Public Function GetTableName() As String
   GetTableName = "DetalleDeConciliacion"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   Dim arrCampos(1 To 3, 1 To 3) As String
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Adm.ConceptoBancario", GetTableName(), "Codigo", _
          "CodConcepto", usarSoloUnElementoEnLaRelacion, _
          "Codigo", "CodConcepto", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Conciliacion", GetTableName(), "NroConciliacion", _
          "NroConciliacion", Not usarSoloUnElementoEnLaRelacion, _
          "ConsecutivoCompania", "ConsecutivoCompania", deleteOnCascade
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,NroConciliacion,CodigoCuenta,Consecutivo)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaRef", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "ConsecCia NOT NULL, "
   SQL = SQL & "NroConciliacion" & gUtilSQL.getVarCharTypeForDB(9) & " CONSTRAINT nn" & GetTableName() & "NroConcili NOT NULL, "
   SQL = SQL & "CodigoCuenta" & gUtilSQL.getVarCharTypeForDB(5) & " CONSTRAINT nn" & GetTableName() & "CodigoCuen NOT NULL, "
   SQL = SQL & "Consecutivo" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "CodConcepto" & gUtilSQL.getVarCharTypeForDB(8) & " CONSTRAINT nn" & GetTableName() & "CodConcept NOT NULL, "
   SQL = SQL & "TipoConcepto" & gUtilSQL.getVarCharTypeForDB(7) & ", "
   SQL = SQL & "FechaRef" & gUtilSQL.getDateTypeForDB() & " CONSTRAINT nn" & GetTableName() & "FechaRef NOT NULL, "
   SQL = SQL & "NumeroDocumento" & gUtilSQL.getVarCharTypeForDB(25) & " CONSTRAINT nn" & GetTableName() & "NumeroDocu NOT NULL, "
   SQL = SQL & "DescripcionDet" & gUtilSQL.getVarCharTypeForDB(50) & " CONSTRAINT nn" & GetTableName() & "Descripcio NOT NULL, "
   SQL = SQL & "Monto" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
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


