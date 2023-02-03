VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsVendedorTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsVendedorTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Vendedor"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania, Codigo"
End Function
Public Function GetTableName() As String
   GetTableName = "Vendedor"
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
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,Codigo)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   Dim insRenglonComisionesDeVendedor As clsRenglonComisionesDeVendTbl
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "Codigo " & gUtilSQL.getVarCharTypeForDB(5) & " CONSTRAINT nn" & GetTableName() & "Codigo NOT NULL, "
   SQL = SQL & "Nombre " & gUtilSQL.getVarCharTypeForDB(35) & " CONSTRAINT nn" & GetTableName() & "Nombre NOT NULL, "
   SQL = SQL & "RIF" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "StatusVendedor " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Direccion " & gUtilSQL.getVarCharTypeForDB(255) & ", "
   SQL = SQL & "Ciudad" & gUtilSQL.getVarCharTypeForDB(100) & " CONSTRAINT nn" & GetTableName() & "Ciudad NOT NULL, "
   SQL = SQL & "ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & ", "
   SQL = SQL & "Telefono " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "Fax " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "Email " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "Notas " & gUtilSQL.getVarCharTypeForDB(255) & ", "
   SQL = SQL & "ComisionPorVenta " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "ComisionPorCobro " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeInicialVenta1 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalVenta1 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeVentas1 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalVenta2 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeVentas2 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalVenta3 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeVentas3 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalVenta4 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeVentas4 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalVenta5 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeVentas5 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeInicialCobranza1 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalCobranza1 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeCobranza1 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalCobranza2 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeCobranza2 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalCobranza3 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeCobranza3 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalCobranza4 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeCobranza4 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TopeFinalCobranza5 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeCobranza5 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "UsaComisionPorVenta " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaComiVtas NOT NULL, "
   SQL = SQL & "UsaComisionPorCobranza " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaComiCob NOT NULL, "
   SQL = SQL & "CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "TipoDocumentoIdentificacion" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "RutaDeComercializacion" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   fCreateTable = True
   Set insRenglonComisionesDeVendedor = New clsRenglonComisionesDeVendTbl
   If insRenglonComisionesDeVendedor.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insRenglonComisionesDeVendedor = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
