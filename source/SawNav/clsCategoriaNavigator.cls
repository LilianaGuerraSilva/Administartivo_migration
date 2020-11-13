VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCategoriaNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private mConsecutivoCompania As Long
Private mConsecutivo As Long
Private mDescripcion As String
Private Const CM_FILE_NAME = "clsCategoriaNavigator"
Private Const CM_MESSAGE_NAME = "Categoria"
Private Const GetGender = eg_Male

Public Function GetConsecutivoCompania() As Long
   GetConsecutivoCompania = mConsecutivoCompania
End Function
Public Function GetConsecutivo() As Long
   GetConsecutivo = mConsecutivo
End Function

Public Function GetDescripcion() As String
   GetDescripcion = mDescripcion
End Function


Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vConsecutivoActual As Long
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.Filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   vConsecutivoActual = 1
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_CategoriaINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      If gDbUtil.FieldInRecordSet(valRecordSet, "ConsecutivoCategoria") Then
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", vConsecutivoActual)
         vConsecutivoActual = vConsecutivoActual + 1
   Else
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", valRecordSet("Consecutivo").value)
   End If
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Descripcion", 20, valRecordSet("Descripcion").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").value)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.Filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInsertarValorporDefecto(ByVal valConsecutivoCompania As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_CategoriaINS", gDefDatabase.Conexion)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", gDbUtil.fNextLngConsecutive("Saw.categoria", "Consecutivo", gUtilSQL.fSQLNumberValueWithAnd("", "ConsecutivoCompania", valConsecutivoCompania)))
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Descripcion", 20, "CATEGORIA")
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, gProyUsuarioActual.GetNombreDelUsuario)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
   insUtAdoCmd.fExecCmd vCmd
   Set vCmd = Nothing
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fSearchByField(ByVal fieldName As String, ByVal fieldValue As String) As Boolean
   Dim mrsCategoria As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByField = False
      Set mrsCategoria = New ADODB.Recordset
   If gDbUtil.fOpenRecordSetAllParameters(mrsCategoria, gProyCompaniaActual.getSQLSearchByFieldStringAndConsecutivo("Saw.Categoria", fieldName, fieldValue), _
         gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsCategoria.RecordCount = 0 Then
         fSearchByField = False
      Else
         fSearchByField = True
         mConsecutivoCompania = mrsCategoria("ConsecutivoCompania").value
         mConsecutivo = mrsCategoria("Consecutivo").value
         mDescripcion = mrsCategoria("Descripcion").value
      End If
   End If
   gDbUtil.sDestroyRecordSet mrsCategoria
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByField", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


