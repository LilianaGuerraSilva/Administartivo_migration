VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRenglonNotaESTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsRenglonNotaESTbl"
Private Const CM_MESSAGE_NAME As String = "Renglon Nota ES"
Private Const GetGender = Enum_Gender.eg_Male

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "NotaDeEntradaSalida", "RenglonNotaES", "NumeroDocumento", "NumeroDocumento", Not usarSoloUnElementoEnLaRelacion, "ConsecutivoCompania", "ConsecutivoCompania", deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE RenglonNotaES  add CONSTRAINT p_RenglonNotaES primary key (ConsecutivoCompania,NumeroDocumento,ConsecutivoRenglon)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo "RenglonNotaES", "Serial"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo "RenglonNotaES", "Rollo"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable("RenglonNotaES") & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nnRenglonNotaESConsecutiv NOT NULL, "
   SQL = SQL & "NumeroDocumento " & gUtilSQL.getVarCharTypeForDB(11) & " CONSTRAINT nnRenglonNotaESNumeroDoc NOT NULL, "
   SQL = SQL & "ConsecutivoRenglon " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nnRenglonNotaESconsecutiv NOT NULL, "
   SQL = SQL & "CodigoArticulo " & gUtilSQL.getVarCharTypeForDB(30) & " CONSTRAINT nnRenglonNotaESCodigoArti NOT NULL, "
   SQL = SQL & "Cantidad " & gUtilSQL.getNumericTypeForDB(12, 2) & " CONSTRAINT nnRenglonNotaESCantidad NOT NULL, "
   SQL = SQL & "TipoArticuloInv" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nnRenglonNotaESTipoArticu NOT NULL, "
   SQL = SQL & "Serial" & gUtilSQL.getVarCharTypeForDB(50) & " CONSTRAINT nnRenglonNotaESSerial NOT NULL, "
   SQL = SQL & "Rollo" & gUtilSQL.getVarCharTypeForDB(20) & "CONSTRAINT nnRenglonNotaESRollo NOT NULL, "
   SQL = SQL & "CostoUnitario " & gUtilSQL.getNumericTypeForDB(12, 4) & " CONSTRAINT d_RenNotESCoUn DEFAULT (0), "
   SQL = SQL & "CostoUnitarioME " & gUtilSQL.getNumericTypeForDB(12, 4) & " CONSTRAINT d_RenNotESCoUnME DEFAULT (0), "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   fCreateTable = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   On Error GoTo h_ERROR
   gDbUtil.ExecuteDropTable "RenglonNotaES", True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
