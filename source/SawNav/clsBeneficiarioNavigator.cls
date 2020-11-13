VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsBeneficiarioNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsBeneficiarioNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Beneficiario"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.Filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_BeneficiarioINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", valRecordSet("Consecutivo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 10, valRecordSet("Codigo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NumeroRIF", 20, valRecordSet("NumeroRIF").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreBeneficiario", 80, valRecordSet("NombreBeneficiario").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@Origen", 1, valRecordSet("Origen").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeBeneficiario", 1, valRecordSet("TipoDeBeneficiario").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, gProyUsuarioActual.GetNombreDelUsuario)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.Filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub

Public Sub sInsertarValorporDefecto(ByVal valConsecutivoCompania As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   
   
   Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_BeneficiarioINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompania)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", gDbUtil.fNextLngConsecutive("Saw.Beneficiario", "Consecutivo", gUtilSQL.fSQLNumberValueWithAnd("", "ConsecutivoCompania", valConsecutivoCompania)))
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 10, gLibSaw.fGenerateNextCodigo(True, 12, "Saw.Beneficiario"))
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NumeroRIF", 20, "J-00000")
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreBeneficiario", 80, "Beneficiario")
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@Origen", 1, 0)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeBeneficiario", 1, 0)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, gProyUsuarioActual.GetNombreDelUsuario)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
      insUtAdoCmd.fExecCmd vCmd
   Set vCmd = Nothing
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub
 


Public Sub sInsertarValorporDefectoEnCreacionDeTabla()
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " INSERT INTO Saw.Beneficiario  (ConsecutivoCompania, Consecutivo,Codigo,NumeroRIF,NombreBeneficiario,Origen,TipoDeBeneficiario) "
   vSql = vSql & " SELECT ConsecutivoCompania, "
   vSql = vSql & gUtilSQL.fNumToStrSQL(1) & " , "
   vSql = vSql & gTexto.fIncrementaNumeroString(0, 10) & " , "
   vSql = vSql & gUtilSQL.fSimpleSqlValue("J-00000") & " , "
   vSql = vSql & gUtilSQL.fSimpleSqlValue("Beneficiario") & " , "
   vSql = vSql & gUtilSQL.fNumToStrSQL(0) & " , "
   vSql = vSql & gUtilSQL.fNumToStrSQL(0) & "  "
   vSql = vSql & " FROM Compania "
   gDbUtil.Execute gDefDatabase.Conexion, vSql
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInsertarValorporDefectoEnCreacionDeTabla", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub
 
