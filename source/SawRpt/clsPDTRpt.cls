VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPDTRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsPDTRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte PDT"

Private Enum ePDTRpt
   ePDTRpt_0621_RENTA_MENSUAL = 0
   ePDTRpt_SUBREPORTE_RENTA_MENSUAL
   ePDTRpt_0615_ISC
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporteDePDTs(ByVal valReporte As ePDTRpt) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case ePDTRpt_0621_RENTA_MENSUAL: nombreFileXML = "rpxPDTRentaMensual"
      Case ePDTRpt_SUBREPORTE_RENTA_MENSUAL: nombreFileXML = "rpxSubRptRentaMensual"
      Case ePDTRpt_0615_ISC: nombreFileXML = "rpxISC"
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDePDTs = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDePDTs(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDePDTs = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDeImpresionDelReporteDePDTs", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDePDTs(ByVal valReporte As ePDTRpt) As String
   Select Case valReporte
      Case ePDTRpt_0621_RENTA_MENSUAL: fNombreDelReporteDePDTs = "PDT-0621-Renta Mensual"
      Case ePDTRpt_0615_ISC: fNombreDelReporteDePDTs = "PDT-0615-Impuesto al Consumo Selectivo"
      Case Else: fNombreDelReporteDePDTs = ""
   End Select
End Function

Public Function fConfiguraLasSeccionesDelReporteDePDTRentaMensual(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valSqlDelSubReporte As String, _
                                                ByVal valMes As Integer, ByVal ValAno As Integer, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFile As String
   Dim NombreDelReporte As String
   Dim nombreDelFileXML As String
   Dim valFechaInicial As Date
   Dim valFechaFinal As Date
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFile = fNombreFileDeImpresionDelReporteDePDTs(ePDTRpt_0621_RENTA_MENSUAL)
   NombreDelReporte = fNombreDelReporteDePDTs(ePDTRpt_0621_RENTA_MENSUAL)
   valFechaInicial = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, ValAno, True)
   valFechaFinal = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, ValAno, False)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Período del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         '***IGVVENTAS***
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseVentasNetas", "", "VentasIntBaseAlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseDescuentosConcebidosDevolucionesVentas", "", "DescVentasGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseVentasAsumidasEstado", "", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseDescConcebidosDevVentasAsumidasEstado", "", "DescConcebidosDevVentasAsumidasEstado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseVentasBienes", "", "VentasBienes"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseFacturadasEnPeriodo", "", "VentasDeExportacion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseEmbarcadasEnPeriodo", "", "VentasDeExportacion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseVentasNoGravadasSinExp", "", "VentasInternasNoGravadas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseVentasNoGravadasSinRatio", "", "VentasNoGravadasSinRatio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseOtrasVentas", "", "OtrasVentas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseDescYDev", "", "DescYDev"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTributoVentasNetas", "", "VentasIntIvaAlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTributoDescuentosConcebidosDevolucionesVentas", "", "DescIvaAlicuotaGeneralVentas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTributoVentasAsumidasEstado", "", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTributoDescConcebidosDevVentasAsumidasEstado", "", "TributoDescConcebidosDevVentasAsumidasEstado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTributoDescConcebidosDevVentas", "", "TributoDescConcebidosDevVentas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTributoVentasBienes", "", "TributoVentasBienes"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTributoDescYDev", "", "TributoDescYDev"
         '***TOTALESIGVVENTAS***
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTributoIGVTotalVentas", "", "TotaIvaGeneral"
         gUtilReports.fAsignaSubReporteSiExite refRpt, fConfigurarDatosSubReporteRentaMensual(valSqlDelSubReporte), "SubRptRentaMensual"
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelReporteDePDTRentaMensual = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraLasSeccionesDelReporteDePDTRentaMensual", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfigurarDatosSubReporteRentaMensual(ByVal valSqlSubReporte As String) As DDActiveReports2.ActiveReport
   Dim vSubRpt As DDActiveReports2.ActiveReport
   Dim vNombreDelFileXML As String
   On Error GoTo h_ERROR
   Set vSubRpt = New DDActiveReports2.ActiveReport
   vNombreDelFileXML = fNombreFileDeImpresionDelReporteDePDTs(ePDTRpt_SUBREPORTE_RENTA_MENSUAL)
   If gUtilReports.fLoadLayout(vSubRpt, vNombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(vSubRpt, "dcOrigenData", valSqlSubReporte) Then
         '***IGVCOMPRAS***
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtBaseVentasGravadasNac", "", "ComprasIntBaseAlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtBaseVentasGravadasNoGravadasNac", "", "ComprasIntGravadasNoGravadasInt"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtBaseVentasNoGravadasNac", "", "MontoExento"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtBaseVentasGravadasImp", "", "ComprasImpBaseAlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtBaseVentasGravadasNoGravadasImp", "", "ComprasIntGravadasNoGravadasImp"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtBaseVentasNoGravadasImp", "", "ComprasIntGravadasNoGravadasImp"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtBaseNoGravadasInternas", "", "MontoExentoInt"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtBaseNoGravadasImportadas", "", "MontoExentoImp"

         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtTributoVentasGravadasNac", "", "ComprasIntIvaAlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtTributoVentasGravadasNoGravadasNac", "", "ComprasIvaGenGravadasNoGravadasInt"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtTributoVentasGravadasImp", "", "ComprasImpIvaAlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtTributoVentasGravadasNoGravadasImp", "", "ComprasIvaGenGravadasNoGravadasImp"
         '***TOTALESIGVCOMPRAS***
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtTributoIGVTotalCompras", "", "TotalCreditosDeducibles"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtCreditoFiscalEspecial", "", "0"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtOtrosCreditos", "", "0"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtOtrosCreditosIVAP", "", "0"
         gUtilReports.sDefaultConfigurationForNumericFields vSubRpt, "txtCoeficiente", "", "CoeficienteAplicable"
         
         Call vSubRpt.AddNamedItem("gUtilReports", gUtilReports)
      End If
   End If
   Set fConfigurarDatosSubReporteRentaMensual = vSubRpt
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosSubReporteRentaMensual = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarDatosSubReporteRentaMensual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteDePDTISC(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valMes As String, ByVal ValAno As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFile As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   NombreDelReporte = fNombreDelReporteDePDTs(ePDTRpt_0615_ISC)
   nombreDelFile = fNombreFileDeImpresionDelReporteDePDTs(ePDTRpt_0615_ISC)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
      
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Período de Presentación " & valMes & "/" & ValAno
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMes", "", "Mes"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "BaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoISC", "", "Tributo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaReporte", "", ""
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "", ddGrpAll, ddRepeatOnPage, True, ddNPAfter
      End If
      gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
      Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
   End If
   exito = True
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelReporteDePDTISC = exito
   Exit Function
h_ERROR: fConfiguraLasSeccionesDelReporteDePDTISC = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraLasSeccionesDelReporteDePDTISC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

