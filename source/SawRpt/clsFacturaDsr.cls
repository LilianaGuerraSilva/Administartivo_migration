VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsFacturaDsr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsFacturaDsr"
Private Const CM_MESSAGE_NAME As String = "Dsr Factura"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConfigurarDsrFacturacionPorDia(ByVal valValorDelSql As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMonedaDeLosReportes As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNombreMonedaLocal As String) As DDActiveReports2.ActiveReport
   Dim reporte As rptFacturacionPorDia
   On Error GoTo h_ERROR
   Set reporte = New rptFacturacionPorDia
   reporte.sConfigurarDatosDelReporte valValorDelSql, valFechaInicial, valFechaFinal, valMonedaDeLosReportes, valNombreCompaniaParaInformes, valNombreMonedaLocal
   Set fConfigurarDsrFacturacionPorDia = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrFacturacionPorDia", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrResumenFacturacMensual(ByVal valValorDelSql As String, ByVal valMesDesde As String, ByVal valMesHasta As String, ByVal valAnoDesde As String, ByVal valAnoHasta As String, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCambio As Boolean, ByRef gGlobalization As Object) As DDActiveReports2.ActiveReport
   Dim reporte As rptResumenFacturacMensual
   On Error GoTo h_ERROR
   Set reporte = New rptResumenFacturacMensual
   reporte.sConfigurarDatosDelReporte valValorDelSql, valMesDesde, valMesHasta, valAnoDesde, valAnoHasta, valNombreCompaniaParaInformes, valUsaCambio, gGlobalization
   Set fConfigurarDsrResumenFacturacMensual = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrResumenFacturacMensual", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrFacturacionPorVendedor(ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCodigoVendedorEnPantalla As Boolean, ByRef gGlobalization As Object) As DDActiveReports2.ActiveReport
   Dim reporte As rptFacturacionPorVendedor
   On Error GoTo h_ERROR
   Set reporte = New rptFacturacionPorVendedor
   reporte.sConfigurarDatosDelReporte valSqlDelReporte, valFechaInicial, valFechaFinal, valNombreCompaniaParaInformes, valUsaCodigoVendedorEnPantalla, gGlobalization
   Set fConfigurarDsrFacturacionPorVendedor = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrFacturacionPorVendedor", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrFacturacionPorVendedorMonedaOrig(ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCodigoVendedorEnPantalla As Boolean) As DDActiveReports2.ActiveReport
   Dim reporte As rptFacturacionPorVendedorMonedaOrig
   On Error GoTo h_ERROR
   Set reporte = New rptFacturacionPorVendedorMonedaOrig
   reporte.sConfigurarDatosDelReporte valSqlDelReporte, valFechaInicial, valFechaFinal, valNombreCompaniaParaInformes, valUsaCodigoVendedorEnPantalla
   Set fConfigurarDsrFacturacionPorVendedorMonedaOrig = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrFacturacionPorVendedorMonedaOrig", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrImprimirFactura(ByVal valValorDelSql As String, _
                  ByVal valVendedorNombre As String, ByVal valClienteCodigo As String, _
                     ByVal valClienteNombre As String, ByVal valClienteNumeroRIF As String, _
                        ByVal valClienteNumeroNIT As String, ByVal valClienteCiudad As String, _
                           ByVal valClienteFax As String, ByVal valClienteTelefono As String, _
                              ByVal valClienteContacto As String, ByVal valClienteDireccion As String, _
                                 ByVal valNombreCompaniaParaInformes As String, ByVal valConsecutivoCompania As Long, ByRef refInsFactura As Object, _
                                    ByRef refInsRenglon As Object, ByRef refInsAdmAlicuotaIvaActual As Object) As DDActiveReports2.ActiveReport
   Dim reporte As RptImprimirFactura
   On Error GoTo h_ERROR
   Set reporte = New RptImprimirFactura
   reporte.sConfigurarDatosDelReporte valValorDelSql, valVendedorNombre, valClienteCodigo, valClienteNombre, valClienteNumeroRIF, valClienteNumeroNIT, valClienteCiudad, valClienteFax, valClienteTelefono, valClienteContacto, valClienteDireccion, valNombreCompaniaParaInformes, valConsecutivoCompania, refInsFactura, refInsAdmAlicuotaIvaActual, refInsRenglon
   Set fConfigurarDsrImprimirFactura = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrImprimirFactura", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrLibroDeVentasNAlicuotas(ByVal valSqlDelReporte As String, ByVal valImprimirDetalles As Boolean, ByVal valMes As String, ByVal ValAno As String, ByVal valNombreCompaniaParaInformes As String, ByRef refInsFactura As Object, ByRef refAdmAlicuotaIvaActual As Object, ByRef gGlobalization As Object) As DDActiveReports2.ActiveReport
   Dim reporte As rptLibroDeVentasNAlicuotas
   On Error GoTo h_ERROR
   Set reporte = New rptLibroDeVentasNAlicuotas
   reporte.sConfigurarDatosDelReporte valSqlDelReporte, valImprimirDetalles, valMes, ValAno, valNombreCompaniaParaInformes, refInsFactura, refAdmAlicuotaIvaActual, gGlobalization
   Set fConfigurarDsrLibroDeVentasNAlicuotas = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrLibroDeVentasNAlicuotas", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrLibroDeVentas(ByVal valSqlDelReporte As String, ByVal valMes As String, ByVal ValAno As String, ByVal valNombreCompaniaParaInformes As String, ByRef refInsAdmAlicuotaIvaActual As Object, ByRef gGlobalization As Object) As DDActiveReports2.ActiveReport
   Dim reporte As rptLibroDeVentas
   On Error GoTo h_ERROR
   Set reporte = New rptLibroDeVentas
   reporte.sConfigurarDatosDelReporte valSqlDelReporte, valMes, ValAno, valNombreCompaniaParaInformes, refInsAdmAlicuotaIvaActual, gGlobalization
   Set fConfigurarDsrLibroDeVentas = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrLibroDeVentas", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


