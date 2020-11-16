VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCuentaSpExec"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCuentaSpExec"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Execute Cuenta SP"
End Function

Public Function fExecuteS10GpCuentaINS(ByVal valConsecutivoCompaniaS10 As Integer, ByVal valCodigoCuenta As String, ByVal valDescripcionCuenta As String, ByVal valNaturaleza As Integer) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   Dim insCuentaSp As clsCuentaSp
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set insCuentaSp = New clsCuentaSp
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_InvS10_CuentaINS", gDefDatabase.Conexion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaS10)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@CodigoCuenta", 30, valCodigoCuenta)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@DescripcionCuenta", 40, valDescripcionCuenta)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterEnum("@Naturaleza", valNaturaleza)
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteS10GpCuentaINS = vResult
   Set insCuentaSp = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteS10GpCuentaINS", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExecuteS10GpCuentaUPD(ByVal valConsecutivoCompaniaS10 As Integer, ByVal valCodigoCuenta As String, ByVal valDescripcionCuenta As String, ByVal valNaturaleza As Integer) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   Dim insCuentaSp As clsCuentaSp
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set insCuentaSp = New clsCuentaSp
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_InvS10_CuentaUPD", gDefDatabase.Conexion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaS10)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@CodigoCuenta", 30, valCodigoCuenta)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@DescripcionCuenta", 40, valDescripcionCuenta)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterEnum("@Naturaleza", valNaturaleza)
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteS10GpCuentaUPD = vResult
   Set insCuentaSp = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteS10GpCuentaUPD", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExecuteS10GpCuentaDEL(ByVal valConsecutivoCompaniaS10 As Integer, ByVal valCodigoCuenta As String) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   Dim insCuentaSp As clsCuentaSp
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set insCuentaSp = New clsCuentaSp
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_InvS10_CuentaDEL", gDefDatabase.Conexion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaS10)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@CodigoCuenta", 30, valCodigoCuenta)
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteS10GpCuentaDEL = vResult
   Set insCuentaSp = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteS10GpCuentaDEL", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Public Function fExecuteCiudadINS(ByVal valNombreCiudad As String) As ADODB.Recordset
'   Dim insUtilAdoCmd As clsUtilAdoCmd
'   Dim vCmd As ADODB.Command
'   Dim vResult As ADODB.Recordset
'   On Error GoTo h_ERROR
'   Set insUtilAdoCmd = New clsUtilAdoCmd
'   Set vCmd = insUtilAdoCmd.fCreateCommand("p_Table_1", gDefDatabase.Conexion)
'   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@col1", 10)      ', valNombreCiudad)
'   Set vResult = insUtilAdoCmd.fExecuteCommand(vCmd)
'   Set insUtilAdoCmd = Nothing
'   Set fExecuteCiudadINS = vResult
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteCiudadINS", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function


