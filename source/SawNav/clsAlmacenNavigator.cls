VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAlmacenNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command

   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.Filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_AlmacenINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", valRecordSet("Consecutivo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 5, valRecordSet("Codigo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreAlmacen", 40, valRecordSet("NombreAlmacen").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeAlmacen", 1, valRecordSet("TipoDeAlmacen").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCliente", valRecordSet("ConsecutivoCliente").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoCc", 5, valRecordSet("CodigoCc").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Descripcion", 40, valRecordSet("Descripcion").value)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsAlmacenNavigator", "sRestaurarDatos", "Almacén", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

