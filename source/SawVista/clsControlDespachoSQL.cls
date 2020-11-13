VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsControlDespachoSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsControlDespachoSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Control Despacho"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLImpresionComprobanteControlDespacho(ByVal valConsecutivoCompania As String, _
                                                         ByVal valNumero As String, _
                                                          ByVal valCantidadAImprimir As String, _
                                                           ByVal valcodigoCliente As String) As String
   Dim SQL As String
   Dim sqlMontoTotal As String
   Dim sqlMontoUsado As String
   Dim sqlMontodevuelto As String
   Dim sqlMontoDiferenciaEnDevolucion As String
   Dim usarTasaOriginal As Boolean
   Dim ReporteEnMonedaLocal As Boolean
   Dim sqlCampoCambioABs As String
   Dim sqlCampoMoneda As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
  

   'SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Anticipo.Status", enum_StatusAnticipo.eSDA_VIGENTE, gEnumProyecto.fenumStatusAnticipoToStrinInArray(True), "status") & ", "

'   SQL = SQL & " WHERE ControlDespacho.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = "SELECT Numero, NumeroEmpaque," & vbCrLf
   SQL = SQL & " SUM(CantidadDespachada) AS CantidadDespachada ," & vbCrLf
   SQL = SQL & " CodigoArticulo, DescripcionArticulo, NumeroCotizacion, CodigoCliente," & vbCrLf
   SQL = SQL & " FechaApertura, FechaCierre, " & vbCrLf
   SQL = SQL & gUtilSQL.getIIF("StatusControl = " & enum_StatusControlDespacho.eSCD_Anulada, gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_Anulada)), _
   gUtilSQL.getIIF("StatusControl = " & enum_StatusControlDespacho.eSCD_Procesada, gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_Procesada)), _
   gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_PorProcesar)))) & " AS StatusControl" & vbCrLf
   SQL = SQL & ", Observaciones, cliente.nombre as NombreCliente" & vbCrLf
   SQL = SQL & " FROM ControlDespacho INNER JOIN DetalleDeControlDespacho" & vbCrLf
   SQL = SQL & " ON (ControlDespacho.ConsecutivoCompania = DetalleDeControlDespacho.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ControlDespacho.Numero = DetalleDeControlDespacho.NumeroControlDespacho)" & vbCrLf
   SQL = SQL & " INNER JOIN Cliente" & vbCrLf
   SQL = SQL & " ON (ControlDespacho.ConsecutivoCompania = Cliente.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ControlDespacho.CodigoCliente = Cliente.Codigo)" & vbCrLf
   SQL = SQL & " WHERE ControlDespacho.ConsecutivoCompania = " & valConsecutivoCompania & vbCrLf
   If valNumero <> "" Then
      SQL = SQL & " AND numero = " & gUtilSQL.fSimpleSqlValue(valNumero) & vbCrLf
   End If
   If valCantidadAImprimir = enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND CodigoCliente = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente) & vbCrLf
   End If
   SQL = SQL & " GROUP BY Numero, NumeroEmpaque, NumeroCotizacion, CodigoCliente, FechaApertura, FechaCierre" & vbCrLf
   SQL = SQL & ", StatusControl, Observaciones, CodigoArticulo, DescripcionArticulo, cliente.nombre" & vbCrLf
   SQL = SQL & " ORDER BY Numero, FechaApertura" & vbCrLf

   fSQLImpresionComprobanteControlDespacho = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLImpresionComprobanteControlDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLListadoDeDespacho(ByVal valConsecutivoCompania As String, _
                                                         ByVal valNumero As String, _
                                                          ByVal valCantidadAImprimir As String, _
                                                           ByVal valcodigoCliente As String, _
                                                            ByVal valFechaApertura As Date, _
                                                             ByVal valFechaCierre As Date, _
                                                              ByVal valStatus As String) As String
   Dim SQL As String
   Dim sqlMontoTotal As String
   Dim sqlMontoUsado As String
   Dim sqlMontodevuelto As String
   Dim sqlMontoDiferenciaEnDevolucion As String
   Dim usarTasaOriginal As Boolean
   Dim ReporteEnMonedaLocal As Boolean
   Dim sqlCampoCambioABs As String
   Dim sqlCampoMoneda As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
  
   SQL = "SELECT Numero, "
   SQL = SQL & gUtilSQL.getIIF("StatusControl = " & enum_StatusControlDespacho.eSCD_Anulada, gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_Anulada)), _
   gUtilSQL.getIIF("StatusControl = " & enum_StatusControlDespacho.eSCD_Procesada, gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_Procesada)), _
   gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_PorProcesar)))) & " AS StatusControl" & vbCrLf
   SQL = SQL & ", NumeroCotizacion, CodigoCliente" & vbCrLf
   SQL = SQL & ", FechaApertura, FechaCierre " & vbCrLf
   SQL = SQL & ", Observaciones, cliente.nombre as NombreCliente" & vbCrLf
   SQL = SQL & " FROM ControlDespacho "
   SQL = SQL & " INNER JOIN Cliente" & vbCrLf
   SQL = SQL & " ON (ControlDespacho.ConsecutivoCompania = Cliente.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ControlDespacho.CodigoCliente = Cliente.Codigo)" & vbCrLf
   SQL = SQL & " WHERE ControlDespacho.ConsecutivoCompania = " & valConsecutivoCompania & vbCrLf
   If valNumero <> "" Then
      SQL = SQL & " AND numero = " & gUtilSQL.fSimpleSqlValue(valNumero) & vbCrLf
   End If
   If valCantidadAImprimir = enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND CodigoCliente = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente) & vbCrLf
   End If
   If valStatus <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND StatusControl = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strStatusControlDespachoToNum(valStatus))
   End If
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("FechaCierre", valFechaApertura, valFechaCierre)
   SQL = SQL & " GROUP BY StatusControl, Numero, NumeroCotizacion, CodigoCliente, FechaApertura, FechaCierre" & vbCrLf
   SQL = SQL & ", Observaciones, cliente.nombre" & vbCrLf
   SQL = SQL & " ORDER BY Numero, StatusControl" & vbCrLf

   fSQLListadoDeDespacho = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLListadoDeDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


