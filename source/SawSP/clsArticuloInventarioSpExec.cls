VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsArticuloInventarioSpExec"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsArticuloInventarioSpExec"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Execute Articulo Inventario Sp"
End Function





Public Function fExecuteSActualizaCostosyMargenes(ByVal valConsecutivoCompania As Integer, ByVal valAlicuotaGeneral As Currency, ByVal valAlicuota2 As Currency, ByVal valAlicuota3 As Currency, ByVal valFechaOperacion As Date, ByVal valNumero As String, ByVal valPrecio As Boolean, Optional ByVal valPrecio2 As Boolean, _
                  Optional ByVal valPrecio3 As Boolean, Optional ByVal valPrecio4 As Boolean, _
                  Optional ByVal valMargen As Currency, Optional ByVal valMargen2 As Currency, _
                  Optional ByVal valMargen3 As Currency, Optional ByVal valMargen4 As Currency) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_InventarioActualizarCostosyMargenes", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@valConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen", valMargen)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen2", valMargen2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen3", valMargen3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen4", valMargen4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio", valPrecio)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio2", valPrecio2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio3", valPrecio3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio4", valPrecio4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuotaGeneral", valAlicuotaGeneral)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota2", valAlicuota2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota3", valAlicuota3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterDate("@valFecha", valFechaOperacion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroDocumento", 20, valNumero)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valTipoDeDocumento", 15, "compra")
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSActualizaCostosyMargenes = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSActualizaCostosyMargenes", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExecuteSActualizaCostos(ByVal valConsecutivoCompania As Integer, ByVal valAlicuotaGeneral As Currency, ByVal valAlicuota2 As Currency, ByVal valAlicuota3 As Currency, ByVal valFechaOperacion As Date, ByVal valNumero As String, ByVal valPrecio As Boolean, Optional ByVal valPrecio2 As Boolean, _
                  Optional ByVal valPrecio3 As Boolean, Optional ByVal valPrecio4 As Boolean) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_ArticuloInventarioActualizarCostos", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@valConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio", valPrecio)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio2", valPrecio2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio3", valPrecio3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio4", valPrecio4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuotaGeneral", valAlicuotaGeneral)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota2", valAlicuota2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota3", valAlicuota3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterDate("@valFecha", valFechaOperacion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroDocumento", 20, valNumero)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valTipoDeDocumento", 15, "compra")
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSActualizaCostos = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSActualizaCostos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function




Public Function fExecuteSActualizaCostosyMargenesFormulaOpcional(ByVal valConsecutivoCompania As Integer, ByVal valAlicuotaGeneral As Currency, ByVal valAlicuota2 As Currency, ByVal valAlicuota3 As Currency, ByVal valFechaOperacion As Date, ByVal valNumero As String, ByVal valPrecio As Boolean, Optional ByVal valPrecio2 As Boolean, _
                  Optional ByVal valPrecio3 As Boolean, Optional ByVal valPrecio4 As Boolean, _
                  Optional ByVal valMargen As Currency, Optional ByVal valMargen2 As Currency, _
                  Optional ByVal valMargen3 As Currency, Optional ByVal valMargen4 As Currency) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_InventarioActualizarCostosyMargenesFormulaOpcional", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@valConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen", valMargen)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen2", valMargen2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen3", valMargen3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen4", valMargen4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio", valPrecio)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio2", valPrecio2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio3", valPrecio3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio4", valPrecio4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuotaGeneral", valAlicuotaGeneral)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota2", valAlicuota2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota3", valAlicuota3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterDate("@valFecha", valFechaOperacion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroDocumento", 20, valNumero)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valTipoDeDocumento", 15, "compra")
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSActualizaCostosyMargenesFormulaOpcional = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSActualizaCostosyMargenesFormulaOpcional", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExecuteSActualizaCostosFormulaOpcional(ByVal valConsecutivoCompania As Integer, ByVal valAlicuotaGeneral As Currency, ByVal valAlicuota2 As Currency, ByVal valAlicuota3 As Currency, ByVal valFechaOperacion As Date, ByVal valNumero As String, ByVal valPrecio As Boolean, Optional ByVal valPrecio2 As Boolean, _
                  Optional ByVal valPrecio3 As Boolean, Optional ByVal valPrecio4 As Boolean) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_ArticuloInventarioActualizarCostosFormulaOpcional", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@valConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio", valPrecio)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio2", valPrecio2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio3", valPrecio3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio4", valPrecio4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuotaGeneral", valAlicuotaGeneral)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota2", valAlicuota2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota3", valAlicuota3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterDate("@valFecha", valFechaOperacion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroDocumento", 20, valNumero)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valTipoDeDocumento", 15, "compra")
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSActualizaCostosFormulaOpcional = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSActualizaCostosFormulaOpcional", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function





Public Function fExecuteSActualizaCostosyMargenesFormulaOpcionalxUltimaCompra(ByVal valConsecutivoCompania As Integer, ByVal valAlicuotaGeneral As Currency, ByVal valAlicuota2 As Currency, ByVal valAlicuota3 As Currency, ByVal valFechaOperacion As Date, ByVal valNumero As String, ByVal valPrecio As Boolean, Optional ByVal valPrecio2 As Boolean, _
                  Optional ByVal valPrecio3 As Boolean, Optional ByVal valPrecio4 As Boolean, _
                  Optional ByVal valMargen As Currency, Optional ByVal valMargen2 As Currency, _
                  Optional ByVal valMargen3 As Currency, Optional ByVal valMargen4 As Currency) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_InventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@valConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen", valMargen)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen2", valMargen2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen3", valMargen3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen4", valMargen4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio", valPrecio)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio2", valPrecio2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio3", valPrecio3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio4", valPrecio4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuotaGeneral", valAlicuotaGeneral)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota2", valAlicuota2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota3", valAlicuota3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterDate("@valFecha", valFechaOperacion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroDocumento", 20, valNumero)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valTipoDeDocumento", 15, "compra")
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSActualizaCostosyMargenesFormulaOpcionalxUltimaCompra = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSActualizaCostosyMargenesFormulaOpcionalxUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExecuteSActualizaCostosFormulaOpcionalxUltimaCompra(ByVal valConsecutivoCompania As Integer, ByVal valAlicuotaGeneral As Currency, ByVal valAlicuota2 As Currency, ByVal valAlicuota3 As Currency, ByVal valFechaOperacion As Date, ByVal valNumero As String, ByVal valPrecio As Boolean, Optional ByVal valPrecio2 As Boolean, _
                  Optional ByVal valPrecio3 As Boolean, Optional ByVal valPrecio4 As Boolean) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_ArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@valConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio", valPrecio)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio2", valPrecio2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio3", valPrecio3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio4", valPrecio4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuotaGeneral", valAlicuotaGeneral)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota2", valAlicuota2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota3", valAlicuota3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterDate("@valFecha", valFechaOperacion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroDocumento", 20, valNumero)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valTipoDeDocumento", 15, "compra")
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSActualizaCostosFormulaOpcionalxUltimaCompra = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSActualizaCostosFormulaOpcionalxUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function






Public Function fExecuteSActualizaCostosyMargenesxUltimaCompra(ByVal valConsecutivoCompania As Integer, ByVal valAlicuotaGeneral As Currency, ByVal valAlicuota2 As Currency, ByVal valAlicuota3 As Currency, ByVal valFechaOperacion As Date, ByVal valNumero As String, ByVal valPrecio As Boolean, Optional ByVal valPrecio2 As Boolean, _
                  Optional ByVal valPrecio3 As Boolean, Optional ByVal valPrecio4 As Boolean, _
                  Optional ByVal valMargen As Currency, Optional ByVal valMargen2 As Currency, _
                  Optional ByVal valMargen3 As Currency, Optional ByVal valMargen4 As Currency) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_InventarioActualizarCostosyMargenesxUltimaCompra", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@valConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen", valMargen)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen2", valMargen2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen3", valMargen3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valMargen4", valMargen4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio", valPrecio)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio2", valPrecio2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio3", valPrecio3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio4", valPrecio4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuotaGeneral", valAlicuotaGeneral)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota2", valAlicuota2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota3", valAlicuota3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterDate("@valFecha", valFechaOperacion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroDocumento", 20, valNumero)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valTipoDeDocumento", 15, "compra")
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSActualizaCostosyMargenesxUltimaCompra = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSActualizaCostosyMargenesxUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExecuteSActualizaCostosxUltimaCompra(ByVal valConsecutivoCompania As Integer, ByVal valAlicuotaGeneral As Currency, ByVal valAlicuota2 As Currency, ByVal valAlicuota3 As Currency, ByVal valFechaOperacion As Date, ByVal valNumero As String, ByVal valPrecio As Boolean, Optional ByVal valPrecio2 As Boolean, _
                  Optional ByVal valPrecio3 As Boolean, Optional ByVal valPrecio4 As Boolean) As ADODB.Recordset
   Dim insUtilAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vResult As ADODB.Recordset
   On Error GoTo h_ERROR
   Set insUtilAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtilAdoCmd.fCreateCmd("Gp_ArticuloInventarioActualizarCostosxUltimaCompra", gDefDatabase.Conexion)
   
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterInteger("@valConsecutivoCompania", valConsecutivoCompania)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio", valPrecio)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio2", valPrecio2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio3", valPrecio3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterBooleanSN("@valPrecio4", valPrecio4)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuotaGeneral", valAlicuotaGeneral)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota2", valAlicuota2)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterCurrency("@valAlicuota3", valAlicuota3)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterDate("@valFecha", valFechaOperacion)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valNumeroDocumento", 20, valNumero)
   vCmd.Parameters.Append insUtilAdoCmd.fCreateInputParameterString("@valTipoDeDocumento", 15, "compra")
   
   Set vResult = insUtilAdoCmd.fExecCmd(vCmd)
   Set insUtilAdoCmd = Nothing
   Set fExecuteSActualizaCostosxUltimaCompra = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExecuteSActualizaCostosxUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



