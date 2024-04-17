VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsConciliacionRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsConciliacionRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte Conciliación"

Private Enum eConciliacionRpt
   eConRpt_DATOS_RESUMIDOS = 0
   eConRpt_MOVIMIENTOS_DE_CONCILIACION
   eConRpt_MOVIMIENTOS_CONCILIADOS
 
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Private Function fNombreReporteParaMensajes(ByVal valIndexDelReporte As eConciliacionRpt) As String
   Dim varNombreReporte As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eConRpt_DATOS_RESUMIDOS: varNombreReporte = "Informe Conciliación - Datos Resumidos"
      Case eConRpt_MOVIMIENTOS_DE_CONCILIACION: varNombreReporte = "Movimientos de Conciliación"
      Case eConRpt_MOVIMIENTOS_CONCILIADOS: varNombreReporte = "Movimientos (no) conciliados"
      Case Else: varNombreReporte = ""
   End Select
h_EXIT: On Error GoTo 0
   fNombreReporteParaMensajes = varNombreReporte
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreReporteParaMensajes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreFileDeImpresionDelReporte(ByVal valIndexDelReporte As Integer) As String
   Dim varNombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eConRpt_DATOS_RESUMIDOS: varNombreFileXML = "rpxConciliacionDatosResumidos"
      Case eConRpt_MOVIMIENTOS_DE_CONCILIACION: varNombreFileXML = "rpxConciliacion"
      Case eConRpt_MOVIMIENTOS_CONCILIADOS: varNombreFileXML = "rpxMovConcxCuentas"
      Case Else: varNombreFileXML = ""
   End Select
   If varNombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(varNombreFileXML, True, fNombreReporteParaMensajes(valIndexDelReporte))
   Else
      fNombreFileDeImpresionDelReporte = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreFileDeImpresionDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraReporteDatosResumidos(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
               ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eConRpt_DATOS_RESUMIDOS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Informe de Conciliación - Datos Resumidos"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConciliacion", "", "NroConciliacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMes", "", "Mes"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAno", "", "Ano"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCuenta", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCuenta", "", "NumeroCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "NombreDeLaMoneda"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHConciliacion", "NroConciliacion", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoLibro", "", "SaldoFinal"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtEgresos", "Egreso", eSF_SUM, "GHConciliacion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtIngresos", "Ingreso", eSF_SUM, "GHConciliacion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoBanco", "", "SaldoEdoCuentaBancario"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSimboloMoneda1", "", "SimboloDeLaMoneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSimboloMoneda2", "", "SimboloDeLaMoneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSimboloMoneda3", "", "SimboloDeLaMoneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSimboloMoneda4", "", "SimboloDeLaMoneda"
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreFileDeImpresionDelReporte(eConRpt_DATOS_RESUMIDOS)
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraReporteDatosResumidos = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraReporteDatosResumidos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConfiguraLasSeccionesDeMovimientosDeConciliacion(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML   As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eConRpt_MOVIMIENTOS_DE_CONCILIACION)
 
     
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
 
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroConciliacion", "", "NroConciliacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAnoDeConciliacion", "", "AnoDeAplicacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMesDeLaConciliacion", "", "MesDeAplicacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCuenta", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtfecha", "", "Fecha"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader1", "NroConciliacion", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
      
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoConcepto", "", "CodigoConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivo", "", "ConsecutivoMovimiento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDebe", "", "MontoEgreso"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtHaber", "", "MontoIngreso"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDebe", "", "MontoEgreso"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalHaber", "", "MontoIngreso"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gConvert", gConvert)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDeMovimientosDeConciliacion = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraLasSeccionesDeMovimientosDeConciliacion", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConfiguraLasSeccionesDelInformeDeMovimientosConciliados(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                  ByVal valTitulo As String, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML     As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
    nombreDelFileXML = fNombreFileDeImpresionDelReporte(eConRpt_MOVIMIENTOS_CONCILIADOS)
  If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
       If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialyFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Movimientos Bancarios " & valTitulo
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroCuenta", "", "NumeroCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtfecha", "", "Fecha"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroDocumento", "", "NDoc"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoConcepto", "", "CodigoConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivo", "", "ConsecutivoMov"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDebe", "", "MontoIngreso"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtHaber", "", "MontoEgreso"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumConciliacion", "", "NumConciliacion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDebe", "", "MontoIngreso"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalHaber", "", "MontoEgreso"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gConvert", gConvert)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelInformeDeMovimientosConciliados = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraLasSeccionesDelInformeDeMovimientosConciliados", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
