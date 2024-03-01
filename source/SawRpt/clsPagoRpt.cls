VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPagoRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsPagoRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reportes Pago"
Private Const CM_RPT_LIBRO_RETENCIONES_RENTA As Integer = 17
Private Const GetGender = Enum_Gender.eg_Male

Private Const CM_RPT_BORRADOR_PLANILLA_DE_PAGO As Integer = 4
Private Const CM_RPT_PAGOS_ENTRE_FECHAS As Integer = 6
Private Const CM_RPT_PAGOS_ENTRE_FECHAS_AGRUPADO_POR_PROVEEDOR As Integer = 7
Private Const CM_RPT_INFORME_RETENCIONES_POR_MES_ANO_TIPO_DE_PERSONA As Integer = 10
Private Const CM_RPT_RET_PAGOS_POR_CONCEPTO As Integer = 12
Private Const CM_RPT_RET_PAGOS_MENS_POR_TIPOPERSONA As Integer = 13
Private Const CM_RPT_DECLARACION_SALARIOS_OTROS As Integer = 14
Private Const CM_RPT_DECLARACION_DIVIDENDO_ACCIONES As Integer = 15
Private Const CM_RPT_GANANCIAS_FORTUITAS As Integer = 16

Public Function fNombreFileDeImpresionDelReporteDePago(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_BORRADOR_PLANILLA_DE_PAGO: nombreFileXML = "rpxBorradorPlanillaDeDeclaracionYPago"
      Case CM_RPT_PAGOS_ENTRE_FECHAS: nombreFileXML = "rpxPagosEntreFechas"
      Case CM_RPT_PAGOS_ENTRE_FECHAS_AGRUPADO_POR_PROVEEDOR: nombreFileXML = "rpxPagosEntreFechasAgrupadoPorProveedor"
      Case CM_RPT_INFORME_RETENCIONES_POR_MES_ANO_TIPO_DE_PERSONA: nombreFileXML = "rpxRetencionesPorMesAnoTipoDePersona"
      Case CM_RPT_RET_PAGOS_POR_CONCEPTO: nombreFileXML = "rpxRetencionesPorConcepto"
      Case CM_RPT_RET_PAGOS_MENS_POR_TIPOPERSONA: nombreFileXML = "rpxRetencionesMensualesPorTipoDePersona"
      Case CM_RPT_LIBRO_RETENCIONES_RENTA: nombreFileXML = "rpxLibroDeRetencionesRenta"
      Case CM_RPT_DECLARACION_SALARIOS_OTROS: nombreFileXML = "rpxDeclaracionSalariosYOtros"
      Case CM_RPT_DECLARACION_DIVIDENDO_ACCIONES: nombreFileXML = "rpxDeclaracionDividendoYAcciones"
      Case CM_RPT_GANANCIAS_FORTUITAS: nombreFileXML = "rpxDeclaracionGanaciasFortuitas"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDePago = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDePago(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDePago = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDePago", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fNombreDelReporteDePago(ByVal valReporte As Integer) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_BORRADOR_PLANILLA_DE_PAGO: fNombreDelReporteDePago = "Borrador de la Planilla de Pago"
      Case CM_RPT_PAGOS_ENTRE_FECHAS: fNombreDelReporteDePago = "Pagos entre Fechas"
      Case CM_RPT_PAGOS_ENTRE_FECHAS_AGRUPADO_POR_PROVEEDOR: fNombreDelReporteDePago = "Pagos entre Fechas - Agrupados por Proveedor"
      Case CM_RPT_INFORME_RETENCIONES_POR_MES_ANO_TIPO_DE_PERSONA: fNombreDelReporteDePago = "Retenciones por Mes/Año y Tipo de Persona"
      Case CM_RPT_RET_PAGOS_POR_CONCEPTO: fNombreDelReporteDePago = "Retenciones por Concepto"
      Case CM_RPT_RET_PAGOS_MENS_POR_TIPOPERSONA: fNombreDelReporteDePago = "Retenciones Mensuales por Tipo de Persona"
      Case CM_RPT_LIBRO_RETENCIONES_RENTA: fNombreDelReporteDePago = "Libro de Retenciones Incisos E) y F) del Art. 34 de la Ley del Impuesto a la Renta"
      Case CM_RPT_DECLARACION_SALARIOS_OTROS: fNombreDelReporteDePago = "Declaración Mensual - Sueldos, Salarios Y Otros"
      Case CM_RPT_DECLARACION_DIVIDENDO_ACCIONES: fNombreDelReporteDePago = "Declaración Mensual - Dividendo Y Acciones"
      Case CM_RPT_GANANCIAS_FORTUITAS: fNombreDelReporteDePago = "Declaración Mensual - Ganancias Fortuitas Distintas a Loterías"
      Case Else: fNombreDelReporteDePago = ""
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelReporteDePago", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDePagosEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valAgrupado As Boolean, ByVal valFNProveedorPorPagina As String, ByVal valUsarCambioOriginal As Boolean, _
                        ByVal valUnaPaginaPorProveedor As Boolean, ByVal valReporteEnMonedaLocal As Boolean, _
                        ByVal valNombreCompaniaParaInformes As String, ByVal valFechaDeHoy As Date, _
                        ByVal valHoraActualString As String, ByVal UsaModuloDeContabilidad As Boolean, _
                        ByVal MostrarObservaciones As Boolean, Optional ByVal valMensajesDeMonedaParaInformes As String = "") As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   If valAgrupado Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDePago(CM_RPT_PAGOS_ENTRE_FECHAS_AGRUPADO_POR_PROVEEDOR)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDePago(CM_RPT_PAGOS_ENTRE_FECHAS)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(valFechaDeHoy) & " a las " & valHoraActualString
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaTotal", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoProveedor", "", "CodigoProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoProveedorTotal", "", "CodigoProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCheque", "", "NumeroCheque"
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtNumeroComprobante", "", "NumeroComprobante"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalOtros", "", "TotalOtros"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRetencion", "", "TotalRetenido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRetencionIVA", "", "TotalRetenidoIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRetencionMunicipal", "", "TotalRetImpMunicipal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCheque", "", "MontoCheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalAnticipos", "", "TotalAnticipo"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalOtros", "TotalOtros", eSF_SUM, "GHMoneda", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalRetencion", "TotalRetenido", eSF_SUM, "GHMoneda", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalRetencionIVA", "TotalRetenidoIva", eSF_SUM, "GHMoneda", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalRetencionMunicipal", "TotalRetImpMunicipal", eSF_SUM, "GHMoneda", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoCheque", "MontoCheque", eSF_SUM, "GHMoneda", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalAnticipos", "TotalAnticipo", eSF_SUM, "GHMoneda", eSR_GROUP, eST_GRAND_TOTAL
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalOtrosProv", "TotalOtros", eSF_SUM, "GHProveedor", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalRetencionProv", "TotalRetenido", eSF_SUM, "GHProveedor", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalRetencionIVAProv", "TotalRetenidoIva", eSF_SUM, "GHProveedor", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalRetencionMunicipalProv", "TotalRetImpMunicipal", eSF_SUM, "GHProveedor", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoChequeProv", "MontoCheque", eSF_SUM, "GHProveedor", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalAnticiposProv", "TotalAnticipo", eSF_SUM, "GHProveedor", eSR_GROUP, eST_GRAND_TOTAL
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "Moneda", ddGrpFirstDetail, ddRepeatAll, True, ddNPNone
         If valUnaPaginaPorProveedor Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHProveedor", "", ddGrpAll, ddRepeatOnPage, True, ddNPBefore
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHProveedor", "", ddGrpAll, ddRepeatOnPage, True, ddNPNone
         End If
         
         If valReporteEnMonedaLocal Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLeyendaDeTasaDeCambio", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLeyendaDeTasaDeCambio", False
         End If
         If Not UsaModuloDeContabilidad Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "ComprobanteContable"
         End If
         If MostrarObservaciones Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtdescripcionPago", "", "descripcionPago"
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblDescripcionPago", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtDescripcionPago", True
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbDescripcionPago", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtDescripcionPago", False
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfigurarDatosDePagosEntreFechas = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDePagosEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarInformeDeRetencionesPorMesAnoTipoDePersona(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                       ByVal valSQL As String, _
                                                                        ByVal valMesStr As String, _
                                                                         ByVal valAnoStr As String, _
                                                                          ByVal valTipoDePersona As String, _
                                                                           ByVal valNombreCompaniaParaInformes As String, _
                                                                            ByVal gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreFileXML As String
   Dim tituloMesAnoTipoDePersona As String
   On Error GoTo h_ERROR
   exito = False
   nombreFileXML = fNombreFileDeImpresionDelReporteDePago(CM_RPT_INFORME_RETENCIONES_POR_MES_ANO_TIPO_DE_PERSONA)
   tituloMesAnoTipoDePersona = "Mes " & valMesStr & " / Año " & valAnoStr & " - " & valTipoDePersona
   If gUtilReports.fLoadLayout(refRpt, nombreFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtcompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMesAnoTipoDePersona", tituloMesAnoTipoDePersona
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNoRif", "N° " & gGlobalization.fPromptRIF
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "txtcompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblMesAnoTipoDePersona", _
                     gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "TipoDePersona"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoRetencion", "", "CodigoRetencion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoRif", "", "NumeroRIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtChequeComprobante", "", "ChequeComprobante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaCxP", "", "FechaCxP"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxP", "", "NumeroCxP"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumControlCxP", "", "NumControlCxP"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBaseImponible", "", "MontoBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoRetenido", "", "MontoRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoOriginal", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoBaseImponible", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoRetenido", ""
         gUtilMargins.sAsignarMargenesGenerales refRpt, PrtOrientation.ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarInformeDeRetencionesPorMesAnoTipoDePersona = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarInformeDeRetencionesPorMesAnoTipoDePersona", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarRetPagosXConcepto(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                              ByVal valSqlDelReporte As String, _
                                               ByVal valMes As String, _
                                                ByVal ValAno As String, _
                                                 ByVal valTipoPersona As String, _
                                                  ByVal valCodRetencion As String, _
                                                   ByVal valDescRetencion As String, _
                                                     ByVal valSearchAllComboBox As String, _
                                                      ByVal valNombreCompaniaParaInformes As String, _
                                                       ByVal valFechaDeHoy As Date, _
                                                        ByVal valHoraActualString As String, _
                                                          ByVal gGlobalization As Object) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   On Error GoTo h_ERROR
   varContinuar = False
   varFileName = fNombreFileDeImpresionDelReporteDePago(CM_RPT_RET_PAGOS_POR_CONCEPTO)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
      gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoRIF", "", "NumeroRif"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoRetencion", "", "MontoRetencion"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "MontoBaseImponible"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoOriginal"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPagosMayoresDe", "", "ParaPagosMayoresDe"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoFactura", "", "NumeroDelDocumento"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcRetencion", "", "Tarifa"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBase", "", "BaseImponible"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSustraendo", "", "Sustraendo"
      gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoRetencion", "MontoRetencion", eSF_SUM, "", eSR_NONE, eST_GRAND_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponible", "MontoBaseImponible", eSF_SUM, "", eSR_NONE, eST_GRAND_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoFactura", "MontoOriginal", eSF_SUM, "", eSR_NONE, eST_GRAND_TOTAL
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRetenciones", "Reporte de Retenciones Mes " & valMes & " Año " & ValAno
      If valTipoPersona = valSearchAllComboBox Then
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPersona", False
      Else
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPersona", "Sólo Persona " & valTipoPersona
      End If
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDeRetencion", valCodRetencion & " - " & valDescRetencion
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "TxtCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNoRif", "Nº " & gGlobalization.fPromptRIF
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(valFechaDeHoy) & " a las " & valHoraActualString
      gUtilReports.sConfiguraEncabezado refRpt, "TxtCompania", "lblFechaYHoraDeEmision", "lblRetenciones", "lblNumeroDePagina", "lblPersona", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
      gUtilMargins.sAsignarMargenesGenerales refRpt
      refRpt.AddNamedItem "gUtilReports", gUtilReports
   End If
   fConfigurarRetPagosXConcepto = varContinuar
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarRetPagosXConcepto", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarRetPagosMensualesPorTipoDePersona(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                              ByVal valSqlDelReporte As String, _
                                                               ByVal valTipoDePersona As String, _
                                                                ByVal valMes As String, _
                                                                 ByVal ValAno As String, _
                                                                   ByVal valNombreCompaniaParaInformes As String, _
                                                                    ByVal valFechaDeHoy As Date, _
                                                                     ByVal valHoraActualString As String) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   varContinuar = False
   varFileName = fNombreFileDeImpresionDelReporteDePago(CM_RPT_RET_PAGOS_MENS_POR_TIPOPERSONA)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "TxtCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPersona", "Mes " & valMes & " Año " & ValAno
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(valFechaDeHoy) & " a las " & valHoraActualString
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDeRetencion", "", "CodigoRet"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDePago", "", "NumPago"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDePersona", "", "TipoDePersona"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "TotalImponible"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRetencion", "", "TotalRetenido"
      gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponible", "TotalImponible", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalRetencion", "TotalRetenido", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
      'NOTA:BUSCAR PARA TOMAR COMO EJEMPLO
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader1", "TipoDePersona", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPAfter
      gUtilReports.sConfiguraEncabezado refRpt, "TxtCompania", "lblFechaYHoraDeEmision", "lblTitulo", "lblNumeroDePagina", "lblPersona", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
      gUtilMargins.sAsignarMargenesGenerales refRpt
      refRpt.AddNamedItem "gUtilReports", gUtilReports
      refRpt.AddNamedItem "gEnumProyecto", gEnumProyecto
   End If
   fConfigurarRetPagosMensualesPorTipoDePersona = varContinuar
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarRetPagosMensualesPorTipoDePersona", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fConfigurarDatosDelLibroRetencionesRenta(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                          ByVal valSQL As String, _
                                                           ByVal valFechaInicial As Date, _
                                                            ByVal valFechaFinal As Date, _
                                                             ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDePago(CM_RPT_LIBRO_RETENCIONES_RENTA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "TxtCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", fNombreDelReporteDePago(CM_RPT_LIBRO_RETENCIONES_RENTA)
         gUtilReports.sConfiguraEncabezado refRpt, "TxtCompania", "lblFechaYHoraDeEmision", _
                                          "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                          gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoIdentidad", "", "TipoDocumentoIdentificacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumentoIdentidad", "", "NumeroRIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBruto", "", "MontoBruto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRetencion", "", "TotalRetenido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoNeto", "", "MontoNeto"
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfigurarDatosDelLibroRetencionesRenta = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelLibroRetencionesRenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteDeGananciasFortuitas( _
            ByRef refReporte As DDActiveReports2.ActiveReport, ByVal valMes As String, _
            ByVal ValAno As String, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDePago(CM_RPT_GANANCIAS_FORTUITAS)
   If nombreDelFileXML = "" Then
      fConfiguraLasSeccionesDelReporteDeGananciasFortuitas = False
   Else
      refReporte.LoadLayout nombreDelFileXML
      If gUtilReports.fDefaultConfigurationForDataControl(refReporte, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtcompania", valNombreCompaniaParaInformes
'         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refReporte, "txtMesYAnoDelReporte", gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2) & "/" & ValAno
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMontoPagado16", "", "PagadoGanancias16"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMontoPagado34", "", "PagadoGanancias34"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMontoRetenido16", "", "retGanancias16"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMontoRetenido34", "", "retGanancias34"
         
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalPagado", "", "SumaMtoRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalRetencion", "", "SumaMtoPagado"
         
         gUtilReports.sConfiguraEncabezado refReporte, "txtcompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilMargins.sAsignarMargenesGenerales refReporte
         fConfiguraLasSeccionesDelReporteDeGananciasFortuitas = True
      Else
         fConfiguraLasSeccionesDelReporteDeGananciasFortuitas = False
      End If
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfiguraLasSeccionesDelReporteDeGananciasFortuitas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteDeSalariosYOtros(ByRef refReporte As DDActiveReports2.ActiveReport, _
               ByVal valMes As Integer, ByVal ValAno As Long, ByVal valSQL As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNumeroDeRif As String, ByVal valNombre As String) As Boolean
   On Error GoTo h_ERROR
   If fConfiguraLasSeccionesDelReporteDeSalariosYOtros(refReporte, valMes, ValAno, valSQL, valNombreCompaniaParaInformes, valNumeroDeRif, valNombre) Then
      Call refReporte.AddNamedItem("gConvert", gConvert)
      Call refReporte.AddNamedItem("gUtilReports", gUtilReports)
      fConfigurarDatosDelReporteDeSalariosYOtros = True
   Else
      fConfigurarDatosDelReporteDeSalariosYOtros = False
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelReporteDeSalariosYOtros", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteDeDividendosYAcciones(ByRef refReporte As DDActiveReports2.ActiveReport, _
               ByVal valMes As Integer, ByVal ValAno As Long, ByVal valSQL As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNumeroDeRif As String, ByVal valNombre As String) As Boolean
   On Error GoTo h_ERROR
   If fConfiguraLasSeccionesDelReporteDeDividendoYAcciones(refReporte, valMes, ValAno, valSQL, valNombreCompaniaParaInformes, valNumeroDeRif, valNombre) Then
      Call refReporte.AddNamedItem("gConvert", gConvert)
      Call refReporte.AddNamedItem("gUtilReports", gUtilReports)
      fConfigurarDatosDelReporteDeDividendosYAcciones = True
   Else
      fConfigurarDatosDelReporteDeDividendosYAcciones = False
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelReporteDeDividendosYAcciones", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfiguraLasSeccionesDelReporteDeSalariosYOtros(ByRef refReporte As DDActiveReports2.ActiveReport, _
      ByVal valMes As Integer, ByVal ValAno As Long, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, _
      ByVal valNumeroDeRif As String, ByVal valNombre As String) As Boolean
   Dim nombreDelFileXML As String
   Dim vFechaDesde As Date
   Dim vFechaHasta As Date
   On Error GoTo h_ERROR
   On Error GoTo h_ERROR
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDePago(CM_RPT_DECLARACION_SALARIOS_OTROS)
   If nombreDelFileXML = "" Then
      fConfiguraLasSeccionesDelReporteDeSalariosYOtros = False
   Else
      refReporte.LoadLayout nombreDelFileXML
      If gUtilReports.fDefaultConfigurationForDataControl(refReporte, "dcOrigenData", valSqlDelReporte) Then
         vFechaDesde = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2), gConvert.fConvierteAString(ValAno), True)
         vFechaHasta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2), gConvert.fConvierteAString(ValAno), False)
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtcompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtMesYAnoDelReporte", gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2) & "/" & gConvert.fConvierteAString(ValAno)
         gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtFechaDesde", vFechaDesde, "", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtFechaHasta", vFechaHasta, "", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroRIF", valNumeroDeRif, ""
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNombreContribuyente", valNombre, ""
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtRifProveedor", "", "RifRetenido"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtReferencia", "", "NumeroReferencia"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroDocumento", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroControl", "", "NumeroControl"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtCodigoRetencion", "", "CodigoRetencion"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtCodigoSeniat", "", "CodigoConcepto"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMontoOriginal", "", "MontoOperacion"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtPorcentajeRetencion", "", "PorcentajeRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMontoRetencion", "", "MontoRetencion"
         gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtFechaOperacion", "", "FechaOperacion", eFT_DATE_LONG
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMarcarDoc", "", "MarcaNoDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMarcarControl", "", "MarcaNoControl"

         gUtilReports.sDefaultConfigurationForSummaryFields refReporte, "txtTotalFacturas", "MontoOperacion", eSF_SUM, "GH", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refReporte, "txtTotalRetenciones", "MontoRetencion", eSF_SUM, "GH", eSR_GROUP, eST_SUB_TOTAL
'         gUtilReports.sChangeWordWrapPropertyForControl( refReporte, "txtTotalRetenciones", "MontoRetencion", eSF_SUM, "GH", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sConfiguraEncabezado refReporte, "txtcompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilMargins.sAsignarMargenesGenerales refReporte
         fConfiguraLasSeccionesDelReporteDeSalariosYOtros = True
      Else
         fConfiguraLasSeccionesDelReporteDeSalariosYOtros = False
      End If
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfiguraLasSeccionesDelReporteDeSalariosYOtros", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
             
Private Function fConfiguraLasSeccionesDelReporteDeDividendoYAcciones(ByRef refReporte As DDActiveReports2.ActiveReport, _
      ByVal valMes As Integer, ByVal ValAno As Long, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNumeroDeRif As String, ByVal valNombre As String) As Boolean
   Dim nombreDelFileXML As String
   Dim vFechaDesde As Date
   Dim vFechaHasta As Date
   On Error GoTo h_ERROR
   On Error GoTo h_ERROR
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDePago(CM_RPT_DECLARACION_DIVIDENDO_ACCIONES)
   If nombreDelFileXML = "" Then
      fConfiguraLasSeccionesDelReporteDeDividendoYAcciones = False
   Else
      refReporte.LoadLayout nombreDelFileXML
      If gUtilReports.fDefaultConfigurationForDataControl(refReporte, "dcOrigenData", valSqlDelReporte) Then

         vFechaDesde = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2), gConvert.fConvierteAString(ValAno), True)
         vFechaHasta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2), gConvert.fConvierteAString(ValAno), False)
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtcompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtMesYAnoDelReporte", gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2) & "/" & gConvert.fConvierteAString(ValAno)
         gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtFechaDesde", vFechaDesde, "", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtFechaHasta", vFechaHasta, "", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroRIF", valNumeroDeRif, ""
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNombreContribuyente", valNombre, ""
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtRifProveedor", "", "RifRetenido"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtReferencia", "", "NumeroReferencia"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroDocumento", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroControl", "", "NumeroControl"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtCodigoRetencion", "", "CodigoRetencion"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtTipoOperacion", "", "Tipo"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMontoOriginal", "", "MontoOperacion"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtPorcentajeRetencion", "", "PorcentajeRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMontoRetencion", "", "MontoRetencion"
         
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMarcarDoc", "", "MarcaNoDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtMarcarControl", "", "MarcaNoControl"

         gUtilReports.sDefaultConfigurationForSummaryFields refReporte, "txtTotalFacturas", "MontoOperacion", eSF_SUM, "GH", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refReporte, "txtTotalRetenciones", "MontoRetencion", eSF_SUM, "GH", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sConfiguraEncabezado refReporte, "txtcompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilMargins.sAsignarMargenesGenerales refReporte

         fConfiguraLasSeccionesDelReporteDeDividendoYAcciones = True
      Else
         fConfiguraLasSeccionesDelReporteDeDividendoYAcciones = False
      End If
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfiguraLasSeccionesDelReporteDeDividendoYAcciones", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'PROVEEDOR
Public Function fConfigurarDatosDelReporteDeGananciasFortuitas(ByRef refReporte As DDActiveReports2.ActiveReport, _
               ByVal valMes As Integer, ByVal ValAno As Long, ByVal valSQL As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   On Error GoTo h_ERROR
   If fConfiguraLasSeccionesDelReporteDeGananciasFortuitas(refReporte, valMes, ValAno, valSQL, valNombreCompaniaParaInformes) Then
      Call refReporte.AddNamedItem("gConvert", gConvert)
      Call refReporte.AddNamedItem("gUtilReports", gUtilReports)
      fConfigurarDatosDelReporteDeGananciasFortuitas = True
   Else
      fConfigurarDatosDelReporteDeGananciasFortuitas = False
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelReporteDeGananciasFortuitas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

