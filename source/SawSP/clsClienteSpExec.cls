VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsClienteSpExec"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsClienteSpExec"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Execute Cliente Sp"
End Function

Public Function fExecuteSGrabaAseguradoraPrefijoRIS(ByVal valPrefijo As String, ByVal valRif As String) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_GrabaAseguradoraPrefijoRIS", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valPrefijo", 2, valPrefijo)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valRif", 20, valRif)
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSGrabaAseguradoraPrefijoRIS = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSGrabaAseguradoraPrefijoRIS", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExecuteSEliminaClienteRIS(ByVal valCodigoCliente As String) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_EliminaClienteRIS", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valCodigoCliente", 10, valCodigoCliente)
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSEliminaClienteRIS = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSEliminaClienteRIS", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

