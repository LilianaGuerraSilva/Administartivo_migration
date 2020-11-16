VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxPSpExec"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCxPSpExec"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Execute CxP Sp"
End Function


Public Function fExecuteSGeneraCxP_RIS(ByVal valNumeroFactura As String) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_GeneraCxPRis", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroFactura", 11, valNumeroFactura)
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSGeneraCxP_RIS = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSGeneraCxP_RIS", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fExecuteSEliminaCxP_RIS(ByVal valNumeroFactura As String) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_EliminaCxPRis", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroFactura", 11, valNumeroFactura)
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSEliminaCxP_RIS = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSEliminaCxP_RIS", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

