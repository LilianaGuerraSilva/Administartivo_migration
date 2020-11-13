VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsImpTransacBancariasNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsImpTransacBancariasNavigator"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Impuesto a las Transacciones Bancarias"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Public Function GetTableName() As String
   GetTableName = "ImpTransacBancarias"
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset)
Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vSQL As String
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   While Not valRecordSet.EOF
      vSQL = "SELECT FechaDeInicioDeVigencia FROM Adm.Gv_ImpTransacBancarias_B1 WHERE FechaDeInicioDeVigencia = " & gUtilSQL.fDateToSQLValue(valRecordSet("FechaDeInicioDeVigencia").value)
      If gDbUtil.fRecordCountOfSql(vSQL) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_ImpTransacBancariasINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@DateFormat", 3, "dmy")
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("FechaDeInicioDeVigencia", gConvert.fConvertStringToDate(valRecordSet("FechaDeInicioDeVigencia").value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("AlicuotaAlDebito", gConvert.fConvierteACurrency(valRecordSet("AlicuotaAlDebito").value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("AlicuotaAlCredito", gConvert.fConvierteACurrency(valRecordSet("AlicuotaAlCredito").value))
         insUtAdoCmd.fExecCmd vCmd
         Set vCmd = Nothing
      End If
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
