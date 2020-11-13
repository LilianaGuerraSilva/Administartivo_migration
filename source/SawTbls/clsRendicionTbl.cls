VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRendicionTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsRendicionTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Rendicion"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function GetTableName() As String
   GetTableName = "Rendicion"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Proveedor", GetTableName(), "codigoProveedor", _
         "CodigoBeneficiario", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade

h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,Consecutivo)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "Consecutivo" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "CodigoBeneficiario" & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "CodigoBene NOT NULL, "
   SQL = SQL & "FechaApertura" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaCierre" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "Status" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TotalAdelantos" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TotalCxP" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "GeneraReembolso" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GeneraReem NOT NULL, "
   SQL = SQL & "CodigoCuentaBancaria" & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "CodigoConceptoBancario" & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "NumeroDocumento" & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "Observaciones" & gUtilSQL.getVarCharTypeForDB(100) & ", "
   SQL = SQL & "NombreOperador" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   Dim insDetalleDeRendicion As clsDetalleDeRendicionTbl
   Set insDetalleDeRendicion = New clsDetalleDeRendicionTbl
   If insDetalleDeRendicion.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insDetalleDeRendicion = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   On Error GoTo h_ERROR
   Dim insDetalleDeRendicion As clsDetalleDeRendicionTbl
   Set insDetalleDeRendicion = New clsDetalleDeRendicionTbl
   insDetalleDeRendicion.sDropTable
   Set insDetalleDeRendicion = Nothing

   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

