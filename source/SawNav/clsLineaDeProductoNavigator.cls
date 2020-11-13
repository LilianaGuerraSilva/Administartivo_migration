VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsLineaDeProductoNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME = "clsLineaDeProductoNavigator"
Private Const CM_MESSAGE_NAME = "Linea De Producto"
Private Const GetGender = eg_Male
Private Const GetTableName = "LineaDeProducto"


Private Sub Class_Initialize()
   On Error GoTo h_ERROR
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Initialize", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Class_Terminate()
   On Error GoTo h_ERROR
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Terminate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sAddDirectoDesdeRecordsetExterno(ByVal valConsecutivoCompaniaNew As Long, ByVal valConsecutivoCompaniaOld As Long, ByVal valRecordSet As ADODB.Recordset)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_LineaDeProductoINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", valRecordSet("Consecutivo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 20, valRecordSet("Nombre").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeComision", valRecordSet("PorcentajeComision").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CentroDeCosto", 20, valRecordSet("CentroDeCosto").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").value)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.filter = adFilterNone
   Set insUtAdoCmd = Nothing
      
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: If gUtilRespaldo.fIgnorarElErrorDeRestauracion(Err.Number, Err.HelpContext, Err.Source, Err.Description, valRecordSet) Then
      GoTo h_EXIT
   Else
      Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddDirectoDesdeRecordsetExterno", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
   End If
End Sub

Public Sub sInsertaLineaDeProductoDesdeExportarImportar(ByVal valConsecutivoCompania As Long, ByVal valConsecutivo As Long, _
                                                         ByVal valNombre As String, ByVal valPorcentajeComisionas As Currency, _
                                                         ByVal valCentroDeCosto As String, ByVal valNombreOperador As String, _
                                                         ByVal valFechaUltimaModificacion As Date)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_LineaDeProductoINS", gDefDatabase.Conexion)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", valConsecutivo)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 20, valNombre)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeComision", valPorcentajeComisionas)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CentroDeCosto", 20, valCentroDeCosto)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valNombreOperador)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valFechaUltimaModificacion)
   insUtAdoCmd.fExecCmd vCmd
   Set vCmd = Nothing
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInsertaLineaDeProductoDesdeExportarImportar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function getNextConsecutivo(ByVal valConsecutivoCompania As Integer) As Integer
   Dim vResult As Integer
   Dim objWrapper As Object
   Dim xmlstring As String
   On Error GoTo h_ERROR
   vResult = False
   Set objWrapper = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("Tablas", "wrpLineaDeProducto"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
   xmlstring = objWrapper.GetNextConsecutivoLineaDeProducto(gProyCompaniaActual.GetConsecutivoCompania)
   If (gTexto.DfLen(xmlstring) > 0) Then
   vResult = gConvert.ConvierteAInteger(xmlstring)
   Else
   vResult = 0
   End If
   Set objWrapper = Nothing
   getNextConsecutivo = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: getNextConsecutivo = 0
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "getNextConsecutivo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
