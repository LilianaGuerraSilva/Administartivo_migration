VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRenglonComisionesDeVendTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsRenglonComisionesDeVendedorTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Renglon Comisiones De Vendedor"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania, CodigoVendedor, ConsecutivoRenglon"
End Function
Public Function GetTableName() As String
   GetTableName = "RenglonComisionesDeVendedor"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Vendedor", GetTableName(), "Consecutivo", _
         "ConsecutivoVendedor", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (" & "ConsecutivoCompania" & "," & "CodigoVendedor" & "," & "ConsecutivoRenglon" & ")"
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
   SQL = SQL & "ConsecutivoVendedor " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "ConsecVend NOT NULL, "
   SQL = SQL & "ConsecutivoRenglon " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NombreDeLineaDeProducto " & gUtilSQL.getVarCharTypeForDB(20) & " CONSTRAINT nn" & GetTableName() & "NombreDeLi NOT NULL, "
   SQL = SQL & "TipoDeComision " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "TipoDeComi NOT NULL, "
   SQL = SQL & "Monto " & gUtilSQL.getNumericTypeForDB(12, 2) & " CONSTRAINT nn" & GetTableName() & "Monto NOT NULL, "
   SQL = SQL & "Porcentaje " & gUtilSQL.getNumericTypeForDB(12, 2) & " CONSTRAINT nn" & GetTableName() & "Porcentaje NOT NULL, "
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
