VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxPRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsCxPRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte CxP"
Private mNombrePlantillaRetencionImpuestoMunicipal As String
Private Enum eCxPRpt
   eCxPRpt_LIBRO_DE_COMPRAS = 0
   eCxPRpt_ANALISIS_DE_VENCIMIENTO_CXP_DETALLADO
   eCxPRpt_ANALISIS_DE_VENCIMIENTO_CXP_RESUMIDO
   eCxPRpt_RELACION_DE_COMPRAS_CONTRIBUYENTES_FORMALES
   eCxPRpt_LISTA_DE_PROVEEDORES
   eCxPRpt_SUB_LIBRO_DE_COMPRAS
   eCxPRpt_ANALISIS_CxP_HISTORICO
   eCxPRpt_ESTADO_DE_CUENTA_PROVEEDOR
   eCxPRpt_HISTORICO_DE_PROVEEDOR
   eCxPRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS
   eCxPRpt_CxP_ENTRE_FECHAS
   eCxPRpt_CxP_PENDIENTES_ENTRE_FECHAS
   eCxPRpt_COMPROBANTES_DE_CUENTA_POR_PAGAR
   eCxPRpt_RET_IVA_EN_CXP
   eCxPRpt_CX_PSIN_RETENCIONES
   eCxPRpt_COMPROBANTE_DE_RETENCION
   eCxPRpt_COMPROBANTE_DE_RETENCION_DE_ACTIVIDADES_ECONOMICAS
   eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS
   eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_POR_MUNICIPIO
   eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_CRISTOBAL_ROJAS
End Enum
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporteDeCxP(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eCxPRpt_LIBRO_DE_COMPRAS: nombreFileXML = "rpxLibroDeCompraAXLS"
      Case eCxPRpt_ANALISIS_DE_VENCIMIENTO_CXP_DETALLADO: nombreFileXML = "rpxAnalisisDeVencimientoDeCxPDetallado"
      Case eCxPRpt_ANALISIS_DE_VENCIMIENTO_CXP_RESUMIDO: nombreFileXML = "rpxAnalisisDeVencimientoDeCxPResumido"
      Case eCxPRpt_RELACION_DE_COMPRAS_CONTRIBUYENTES_FORMALES: nombreFileXML = "rpxRelacionDeComprasClientesFormales"
      Case eCxPRpt_LISTA_DE_PROVEEDORES: nombreFileXML = "rpxListaDeProveedores"
      Case eCxPRpt_SUB_LIBRO_DE_COMPRAS: nombreFileXML = "rpxSubLibroDeCompraAXLSAjustes"
      Case eCxPRpt_ANALISIS_CxP_HISTORICO: nombreFileXML = "rpxAnalisisDeCxPHistorico"
      Case eCxPRpt_ESTADO_DE_CUENTA_PROVEEDOR: nombreFileXML = "rpxEstadoDeCuentaProveedor"
      Case eCxPRpt_HISTORICO_DE_PROVEEDOR: nombreFileXML = "rpxHistoricoDeProveedor"
      Case eCxPRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS: nombreFileXML = "rpxAnalisisDeVencimientoDeCxPEntreFechas"
      Case eCxPRpt_CxP_ENTRE_FECHAS: nombreFileXML = "rpxCxPEntreFechas"
      Case eCxPRpt_CxP_PENDIENTES_ENTRE_FECHAS: nombreFileXML = "rpxCxPPendientesEntreFechas"
      Case eCxPRpt_COMPROBANTES_DE_CUENTA_POR_PAGAR: nombreFileXML = "rpxComprobanteDeCxP"
      Case eCxPRpt_RET_IVA_EN_CXP: nombreFileXML = "rpxRetencionesIvaEnCxP"
      Case eCxPRpt_CX_PSIN_RETENCIONES: nombreFileXML = "rpxCxPSinRetenciones"
      Case eCxPRpt_COMPROBANTE_DE_RETENCION: nombreFileXML = "rpxComprobanteDeRetencionEnCxP"
      Case eCxPRpt_COMPROBANTE_DE_RETENCION_DE_ACTIVIDADES_ECONOMICAS: nombreFileXML = fNombreDelRpxDeComprobanteDeRetencionDeActividadesEconomicas()
      Case eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS: nombreFileXML = "rpxResumenRetencionDeActividadesEconomicas"
      Case eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_POR_MUNICIPIO: nombreFileXML = "rpxResumenRetencionDeActividadesEconomicasXMunicipio"
      Case eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_CRISTOBAL_ROJAS: nombreFileXML = "rpxResumenRetencionDeActividadesEconomicasCRISTOBAL ROJAS-MIRANDA"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeCxP = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fEnumReporteDeCxPToString(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeCxP = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDeImpresionDelReporteDeCxP", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEnumReporteDeCxPToString(ByVal valReporte As Integer) As String
   Select Case valReporte
      Case eCxPRpt_LIBRO_DE_COMPRAS: fEnumReporteDeCxPToString = "Libro de Compras en XLS"
      Case eCxPRpt_ANALISIS_DE_VENCIMIENTO_CXP_DETALLADO: fEnumReporteDeCxPToString = "Análisis de Vencimiento de CxP - Detallado"
      Case eCxPRpt_ANALISIS_DE_VENCIMIENTO_CXP_RESUMIDO: fEnumReporteDeCxPToString = "Análisis de Vencimiento de CxP - Resumido"
      Case eCxPRpt_RELACION_DE_COMPRAS_CONTRIBUYENTES_FORMALES: fEnumReporteDeCxPToString = "Libro de Compras Clientes Formales"
      Case eCxPRpt_LISTA_DE_PROVEEDORES: fEnumReporteDeCxPToString = "Listado de Proveedores"
      Case eCxPRpt_SUB_LIBRO_DE_COMPRAS: fEnumReporteDeCxPToString = "SubReporte-Ajustes Libro de Compras en XLS"
      Case eCxPRpt_ANALISIS_CxP_HISTORICO: fEnumReporteDeCxPToString = "Análisis de Cuentas por Pagar Histórico"
      Case eCxPRpt_ESTADO_DE_CUENTA_PROVEEDOR: fEnumReporteDeCxPToString = "Estado de Cuente de Proveedor"
      Case eCxPRpt_HISTORICO_DE_PROVEEDOR: fEnumReporteDeCxPToString = "Histórico de Proveedor"
      Case eCxPRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS: fEnumReporteDeCxPToString = "Análisis de Vencimiento de CxP entre Fechas"
      Case eCxPRpt_CxP_ENTRE_FECHAS: fEnumReporteDeCxPToString = "Cuentas por Pagar entre Fechas"
      Case eCxPRpt_CxP_PENDIENTES_ENTRE_FECHAS: fEnumReporteDeCxPToString = "Cuentas por Pagar Pendientes entre Fechas"
      Case eCxPRpt_COMPROBANTES_DE_CUENTA_POR_PAGAR: fEnumReporteDeCxPToString = "Comprobante de Cuenta por Pagar"
      Case eCxPRpt_RET_IVA_EN_CXP: fEnumReporteDeCxPToString = "Retenciones de Iva en CxP"
      Case eCxPRpt_CX_PSIN_RETENCIONES: fEnumReporteDeCxPToString = "CXP Sin Retenciones I.S.L.R"
      Case eCxPRpt_COMPROBANTE_DE_RETENCION_DE_ACTIVIDADES_ECONOMICAS: fEnumReporteDeCxPToString = "Comprobante de retencion de actividades economicas"
      Case eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS: fEnumReporteDeCxPToString = "Resumen Retencion De Actividades Economicas"
      Case eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_POR_MUNICIPIO: fEnumReporteDeCxPToString = "Resumen Retencion De Actividades Economicas"
      Case eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_CRISTOBAL_ROJAS: fEnumReporteDeCxPToString = "Resumen Retencion De Actividades Economicas"
      Case Else: fEnumReporteDeCxPToString = ""
   End Select
End Function

Public Function fConfiguraLasSeccionesDeListaDeProveedor(ByRef refReporteActual As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valUsaRetencion As Boolean, ByVal NombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim varNombreDelFile As String
   On Error GoTo h_ERROR
   fConfiguraLasSeccionesDeListaDeProveedor = False
   varNombreDelFile = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_LISTA_DE_PROVEEDORES)
   If varNombreDelFile = "" Then
      gMessage.Advertencia "No se ha seleccionado el formato de impresión del reporte"
   Else
      If gUtilReports.fLoadLayout(refReporteActual, varNombreDelFile) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refReporteActual, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtNombreProveedor", "", "Nombre"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtRifProveedor", "", "NumeroRifProveedor"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtContacto", "", "ContactoProveedor"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtTelefono", "", "TelefonoProveedor"
            'gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtFax", "", "FaxProveedor"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtDireccion", "", "DireccionProveedor"
            gUtilReports.sDefaultConfigurationForNumericFields refReporteActual, "txtPorcentajeRetencionIVa", "", "PorcentajeDeIva"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtCodigoRetencionUsual", "", "CodigoRetencionUsual"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtTipoDePersona", "", "TipoDePersona"
            If valUsaRetencion Then
               gUtilReports.sDefaultConfigurationForCheckBox refReporteActual, "chkUsaModuloRetencion", True, ""
            End If
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblFechaYHoraDeEmision", "Emitido el " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblNombreDeLaCompania", NombreCompaniaParaInformes
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblPorcentajeRetencionIva", "% Retención " & gGlobalization.fPromptIVA
            gUtilReports.sConfiguraEncabezado refReporteActual, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
            gUtilMargins.sAsignarMargenesGenerales refReporteActual
            fConfiguraLasSeccionesDeListaDeProveedor = True
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraLasSeccionesDeListaDeProveedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfiguraLasSeccionesDeLibroDeComprasEnXLS(ByRef refRpt As DDActiveReports2.ActiveReport, _
                     ByVal valSqlDelReporte As String, ByVal valSqlDelSubReporte As String, _
                     ByVal valMes As String, ByVal ValAno As String, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim subReporte As DDActiveReports2.ActiveReport
   On Error GoTo h_ERROR
   fConfiguraLasSeccionesDeLibroDeComprasEnXLS = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_LIBRO_DE_COMPRAS)
   sConfiguraElReporteDeLibroDeComprasEnXLS refRpt, nombreDelFileXML, valSqlDelReporte, valMes, ValAno, NombreCompaniaParaInformes
   Set subReporte = New DDActiveReports2.ActiveReport
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_SUB_LIBRO_DE_COMPRAS)
   sConfiguraElReporteDeLibroDeComprasEnXLS subReporte, nombreDelFileXML, valSqlDelSubReporte, valMes, ValAno, NombreCompaniaParaInformes
   Call subReporte.AddNamedItem("gUtilReports", gUtilReports)
   gUtilReports.fAsignaSubReporteSiExite refRpt, subReporte, "srptAjustes"
   Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
   fConfiguraLasSeccionesDeLibroDeComprasEnXLS = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraLasSeccionesDeLibroDeComprasEnXLS", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sConfiguraElReporteDeLibroDeComprasEnXLS(ByRef refRpt As DDActiveReports2.ActiveReport, _
                           ByVal valNombreDelFileXML As String, ByVal valSqlDelReporte As String, ByVal valMes As String, _
                           ByVal ValAno As String, ByVal NombreCompaniaParaInformes As String)
   On Error GoTo h_ERROR
   If gUtilReports.fLoadLayout(refRpt, valNombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMesYAno", "Mes " & valMes & " - Año " & ValAno
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoRif", "", "NumeroRif"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoProveedor", "", "TipoDeProveedorLibros"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobante", "", "NumeroComprobante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroPlanilla", "", "NumeroPlanilla"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroExpediente", "", "NumeroExpediente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoFactura", "", "NumeroDeFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroControlFactura", "", "NumeroControlFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroNotaDebito", "", "NumeroDeNotaDeDebito"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroNotaCredito", "", "NumeroDeNotaDeCredito"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeTransaccion", "", "TipoDeTransaccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFacturaAfectada", "", "FacturaAfectada"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompraIncluyendoIVA", "", "TotalComprasIncluyendoIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtComprasSinDerechoACredito", "", "ComprasSinDerechoACreditoFiscal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravableAlicuotaGeneral", "", "MontoGravableAlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravableAlicuota2", "", "MontoGravableAlicuota2"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravableAlicuota3", "", "MontoGravableAlicuota3"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajesAlicuotas", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeAlicGeneral", "", "PorcentajeAlicGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeAlic2", "", "PorcentajeAlic2"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeAlic3", "", "PorcentajeAlic3"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImpuestoIVA", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVAAlicuotaGeneral", "", "MontoIVAAlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVAAlicuota2", "", "MontoIVAAlicuota2"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVAAlicuota3", "", "MontoIVAAlicuota3"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalImportacionIncluyendoIVA", "", "TotalImportacionIncluyendoIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImportacionesSinDerechoACredito", "", "ImportacionesSinDerechoACreditoFiscal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponibleImp", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravableAlicuotaGeneralImp", "", "MontoGravableAlicuotaGeneralImp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravableAlicuota2Imp", "", "MontoGravableAlicuota2Imp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravableAlicuota3Imp", "", "MontoGravableAlicuota3Imp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajesAlicuotasImp", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeAlicGeneralImp", "", "PorcentajeAlicGeneralImp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeAlic2Imp", "", "PorcentajeAlic2Imp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeAlic3Imp", "", "PorcentajeAlic3Imp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImpuestoIVAImp", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVAAlicuotaGeneralImp", "", "MontoIVAAlicuotaGeneralImp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVAAlicuota2Imp", "", "MontoIVAAlicuota2Imp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVAAlicuota3Imp", "", "MontoIVAAlicuota3Imp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIvaRetenido", "", "IVARetenidoAlVendedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIvaRetenidoATerceros", "", "IVARetenidoATerceros"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAnticipoDeImportacion", "", "AnticipoDeIVAImportacion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComprasIncluyendoIVA", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComprasSinDerechoACredito", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalImportacionesIncluyendoIVA", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalImportacionesSinDerechoACredito", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFSubTotalMontoExento", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalComprasAlicuotaGeneralCIM", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalComprasAlicuota3CIM", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalComprasAlicuota2CIM", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalComprasAlicuotaGeneralCIN", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalComprasAlicuota3CIN", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalComprasAlicuota2CIN", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalBaseImponible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalIVAAlicuotaGeneralCIM", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalIVAAlicuota3CIM", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalIVAlicuota2CIM", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalIVAAlicuotaGeneralCIN", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalIVAAlicuota3CIN", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalIVAAlicuota2CIN", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFTotalCreditoFiscal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRFSubTotalCompras", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCreditosFiscalesTotalmenteDeducible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCreditosFiscalesParcialmenteDeducible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCreditosFiscalesDeducibles", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDeducible", "", "MontoDeducible"
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfiguraElReporteDeLibroDeComprasEnXLS", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fConfigurarDatosDelLibroDeComprasClientesFormales(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valMesAplicacionDesde As String, ByVal valAnoAplicacionDesde As String, _
                  ByVal valMesAplicacionHasta As String, ByVal valAnoAplicacionHasta As String, ByVal NombreCompaniaParaInformes As String, _
                  ByRef gGlobalization As Object, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, Optional ByVal valAplicaAContribuyenteEspecial As Boolean = False) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_RELACION_DE_COMPRAS_CONTRIBUYENTES_FORMALES)
   NombreDelReporte = fEnumReporteDeCxPToString(eCxPRpt_RELACION_DE_COMPRAS_CONTRIBUYENTES_FORMALES)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         
         If valAplicaAContribuyenteEspecial Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDelReporte", "Relación de Compras del " & gConvert.dateToString(valFechaDesde) & " al " & gConvert.dateToString(valFechaHasta)
         Else
            If gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesAplicacionDesde, valAnoAplicacionDesde, True) <> _
               gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesAplicacionHasta, valAnoAplicacionHasta, True) Then
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDelReporte", "Relación de Compras desde el mes de " & gEnumReport.enumMesToString(valMesAplicacionDesde) & " - " & valAnoAplicacionDesde _
                  & " hasta " & gEnumReport.enumMesToString(valMesAplicacionHasta) & " - " & valAnoAplicacionHasta
            Else
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDelReporte", "Relación de Compras del Mes de  " & gEnumReport.enumMesToString(valMesAplicacionDesde) & " - " & valAnoAplicacionDesde
            End If
         End If
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreProveedor", "Nº " & gGlobalization.fPromptRIF & " / Proveedor"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoFacturado", "Total Compras Incluyendo " & gGlobalization.fPromptIVA
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoFactura", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoRif", "", "NumeroRIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeDocumento", "", "TipoDeDocumentoEnSiglas"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTiposDeCompras", "", "TipoDeCompraEnSiglas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExento", "0", "MontoExentoGlobal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFacturado", "0", "MontoFacturado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponibleAlic", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAlicuota", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIvaAlic", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIvaAlicGeneral", "0", "MontoIvaAlicGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBaseImponibleAlicGeneral", "0", "MontoGravableAlicGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAlicuotaGeneral", "0", "AlicuotaGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIvaAlic2", "0", "MontoIvaAlic2"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBaseImponibleAlic2", "0", "MontoGravableAlic2"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAlicuota2", "0", "Alicuota2"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIvaAlic3", "0", "MontoIvaAlic3"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBaseImponibleAlic3", "0", "MontoGravableAlic3"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAlicuota3", "0", "Alicuota3"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoBaseImponibleAlicGeneral", "0", "MontoGravableAlicGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoBaseImponibleAlic2", "0", "MontoGravableAlic2"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoBaseImponibleAlic3", "0", "MontoGravableAlic3"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoFacturadoDelReporte", "0", "MontoFacturado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoExentoDelReporte", "0", "MontoExentoGlobal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIvaAlicGeneral", "0", "MontoIvaAlicGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIvaAlic2", "0", "MontoIvaAlic2"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIvaAlic3", "0", "MontoIvaAlic3"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRFSubTotalesAlicuotaGeneral", "Totales Alicuota General  . . ."
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRFSubTotalesAlicuota2", "Totales Alicuota Reducida  . . ."
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRFSubTotalesAlicuota3", "Totales Alicuota Adicional . . ."
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusDocumento", "", "STATUSDOCUMENTO"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblNombreDelReporte", _
            "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelLibroDeComprasClientesFormales = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fConfigurarDatosDelLibroDeComprasClientesFormales", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Public Function fConfiguraLasSeccionesDelReporteDeAnalisisDeVencimiento(ByRef refRpt As DDActiveReports2.ActiveReport, _
'                  ByVal valSQL As String, ByVal valDetallado As Boolean, ByVal valTasaDeCambioOriginal As Boolean, _
'                  ByVal valMonedaOriginal As Boolean, ByVal valTituloDeFecha As String, ByVal valPropAnalisisVencActual As Boolean, _
'                  ByVal NombreCompaniaParaInformes As String, ByVal valPrimerVencimiento As Long, ByVal valSegundoVencimiento As Long, _
'                  ByVal valTercerVencimiento As Long, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
'   Dim nombreFileXML As String
'   Dim exito As Boolean
'   On Error GoTo h_ERROR
'   exito = False
'   If valPropAnalisisVencActual Then
'      If valDetallado Then
'         nombreFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_ANALISIS_DE_VENCIMIENTO_CXP_DETALLADO)
'      Else
'         nombreFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_ANALISIS_DE_VENCIMIENTO_CXP_RESUMIDO)
'      End If
'      If gUtilReports.fLoadLayout(refRpt, nombreFileXML) Then
'         If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
'            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
'            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
'            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", valTituloDeFecha
'            gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
'               "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
'               If valMonedaOriginal Then
'                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", ""
'               Else
'                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", valMensajesDeMonedaParaInformes
'               End If
'            gMessage.Advertencia "asigna taza de cambio"
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
'            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango1Vto", "1-" & valPrimerVencimiento
'            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango2Vto", valPrimerVencimiento + 1 & "-" & valSegundoVencimiento
'            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango3Vto", valSegundoVencimiento + 1 & "-" & valTercerVencimiento
'            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango4Vto", valTercerVencimiento + 1 & " +"
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoProveedor", "", "CodigoProveedor"
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxP", "", "Numero"
'            gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
'            gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVto", "", "FechaVencimiento", eFT_DATE
'            gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDiasVecidos", "", "DiasVencidos"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCxC", "", "MtoTotal"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMontoNoVencido", "", "MtoNoVencido"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMto1Vto", "", "Mto1Vto"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMto2Vto", "", "Mto2Vto"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMto3Vto", "", "Mto3Vto"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMto4Vto", "", "Mto4Vto"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailTotalProveedor", "", "MtoRestante"
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFCCodigoProveedor", "", "CodigoProveedor"
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFCNombreProveedor", "", "NombreProveedor"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMontoNoVencido", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMto1Vto", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMto2Vto", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMto3Vto", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMto4Vto", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCTotalProveedor", "", ""
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMMoneda", "", "MonedaReporte"
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMontoNoVencido", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto1Vto", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto2Vto", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto3Vto", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto4Vto", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMTotalProveedor", "", ""
'            gUtilMargins.sAsignarMargenesGenerales refRpt
'            Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
'            exito = True
'         End If
'      End If
'   End If
'h_EXIT:
''   Set InsAdmPropAnalisisVenc = Nothing
'   fConfiguraLasSeccionesDelReporteDeAnalisisDeVencimiento = exito
'   On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fConfiguraLasSeccionesDelReporteDeAnalisisDeVencimiento", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function

Public Function fConfigurarElLibroDeComprasXLS(ByRef refRpt As DDActiveReports2.ActiveReport, ByRef refNombreXLS As String, ByVal valMesStr As String, ByVal valAnoStr As String, _
                                    ByVal SqlDelReporte As String, ByVal SqlDelSubReporte As String, ByVal NombreCompaniaParaInformes As String, ByVal valCompaniaCodigo As String) As Boolean
   Dim exito As Boolean
   Dim fechaDeHoy As Date
   On Error GoTo h_ERROR
   exito = False
   fechaDeHoy = gUtilDate.getFechaDeHoy
   If fConfiguraLasSeccionesDeLibroDeComprasEnXLS(refRpt, SqlDelReporte, SqlDelSubReporte, valMesStr, valAnoStr, NombreCompaniaParaInformes) Then
      refNombreXLS = "LibroDeCompras_" & valAnoStr & "_" & gTexto.llenaConCaracterALaIzquierda(valMesStr, "0", 2) & _
                   "_" & valCompaniaCodigo & "_"
      exito = True
   End If
h_EXIT:
   fConfigurarElLibroDeComprasXLS = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarElLibroDeComprasXLS", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


'
'Private Function fSQLTipoDeProveedorParaLibroDeCompra() As String
'   Dim insProveedor As clsProveedorNavigator
'   Dim SQL As String
'   Dim sqlNumRif As String
'   On Error GoTo h_ERROR
'   Set insProveedor = New clsProveedorNavigator
'   sqlNumRif = insProveedor.GetTableName & ".NumeroRIF"
'   sqlNumRif = sqlNumRif & " = " & gUtilSQL.fSimpleSqlValue("") & " OR "
'   sqlNumRif = sqlNumRif & gUtilSQL.DfSQLIsNull(insProveedor.GetTableName & ".NumeroRIF")
'   sqlNumRif = sqlNumRif & " OR " & gUtilSQL.fLen(insProveedor.GetTableName, "NumeroRIF")
'   sqlNumRif = sqlNumRif & " = 0 "
'   If PuedeUsarOptDeContribEspecial Or valUsaRetencion Then
'      If PuedeUsarOptDeContribEspecial Then
'         SQL = gUtilSQL.getIIF(sqlNumRif & " OR " & insProveedor.GetTableName & ".TipoDeProveedorDeLibrosFiscales = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeProveedorDeLibrosFiscales.eTD_SIN_RIF), gUtilSQL.fSimpleSqlValue("SR"), _
'         gUtilSQL.getIIF(insProveedor.GetTableName & ".TipoDeProveedorDeLibrosFiscales = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_RESIDENCIADO), gUtilSQL.fSimpleSqlValue("NR"), _
'            gUtilSQL.getIIF(insProveedor.GetTableName & ".TipoDeProveedorDeLibrosFiscales = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_DOMICILIADO), gUtilSQL.fSimpleSqlValue("ND"), _
'         gUtilSQL.fSimpleSqlValue(""), True), True), True)
'      Else
'         SQL = gUtilSQL.getIIF(sqlNumRif, gUtilSQL.fSimpleSqlValue("SR"), _
'         gUtilSQL.getIIF(insProveedor.GetTableName & ".TipoDePersona = " & _
'            gUtilSQL.fSQLSimpleValueForEnum(enum_TipodePersonaRetencion.eTP_PN_NORESIDENTE), gUtilSQL.fSimpleSqlValue("NR"), _
'         gUtilSQL.getIIF(insProveedor.GetTableName & ".TipoDePersona = " & _
'            gUtilSQL.fSQLSimpleValueForEnum(enum_TipodePersonaRetencion.eTP_PJ_NODOMICILIADA), gUtilSQL.fSimpleSqlValue("ND"), _
'         gUtilSQL.fSimpleSqlValue(""), True), True), True)
'      End If
'   Else
'      SQL = gUtilSQL.getIIF(sqlNumRif, gUtilSQL.fSimpleSqlValue("SR"), gUtilSQL.fSimpleSqlValue(""), True)
'   End If
'   Set insProveedor = Nothing
'h_EXIT: On Error GoTo 0
'   fSQLTipoDeProveedorParaLibroDeCompra = SQL
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'       "fSQLTipoDeProveedorParaLibroDeCompra", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
Public Function fConfigurarDatosDelReporteAnalisisDeCxPHistorico(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioABs As Boolean, ByVal valUnaPaginaPorCliente As Boolean, _
                  ByVal NombreCompaniaParaInformes As String, ByVal MensajeMonedaReporteCambioOriginal As String, ByVal gGlobalization As Object) As Boolean
   
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_ANALISIS_CxP_HISTORICO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                    gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", MensajeMonedaReporteCambioOriginal
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoReporte", "", "ColumnaParaGrupoTipoReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTituloTipoReporte", "", "TituloDelGrupo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicialSinAbonos", "", "SaldoInicialSinAbonos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicialConAbonos", "", "SaldoInicialConAbonos"
         If gGlobalization.fEsCodigoPeru Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "TipoDocumentoLey"
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "TipoDocumento"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoActual", "", "MontoActual"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocPagado", "", "TipoDocPagado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocPagado", "", "NumeroDocPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonadoDocPagado", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoTotal", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoActual", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicialMasMontoActual", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicialCxP", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicialAnticipos", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicialPagos", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalReposiciones", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicialReposiciones", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicial", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCxP", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalAnticipos", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalPagos", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneral", "0"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHProveedor", "Codigo", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHTipoReporte", "ColumnaParaGrupoTipoReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHDetalle", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         If valUnaPaginaPorCliente Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GFProveedor", "", ddGrpAll, ddRepeatOnPage, True, ddNPAfter
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddODefault, fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_ANALISIS_CxP_HISTORICO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteAnalisisDeCxPHistorico = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelReporteAnalisisDeCxPHistorico", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarReporteEstadoDeCuenta(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valUsarMonedaOriginal As Boolean, ByVal valUsarCambioOriginal As Boolean, ByVal valUnaPaginaPorCliente As Boolean, _
                                       ByVal NombreCompaniaParaInformes As String, ByVal MensajeMonedaReporteCambioOriginal As String, ByVal MensajeMonedaReporteCambioDelDia As String, ByVal gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim notaDeCambio As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_ESTADO_DE_CUENTA_PROVEEDOR)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", _
                                    gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
            If valUsarMonedaOriginal Then
               notaDeCambio = ""
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambio", "", ""
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", ""
            Else
               If valUsarCambioOriginal Then
                  notaDeCambio = MensajeMonedaReporteCambioOriginal
                  gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioBs"
               Else
                  notaDeCambio = MensajeMonedaReporteCambioDelDia
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambio", "", "CambioBs"
               'gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
               'gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", ""
               End If
            End If
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", notaDeCambio
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoReporte", "", "TipoReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTituloTipoReporte", "", "TituloTipoReporte"
         If gGlobalization.fEsCodigoPeru Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "TipoDocumentoLey"
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "TipoDocumento"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDiasVencidos", "", "DiasVencidos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoVencido", "", "MontoVencido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoNoVencido", "", "MontoNoVencido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoVencido", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoNoVencido", "", ""
         If valUnaPaginaPorCliente Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GFProveedor", "", ddGrpAll, ddRepeatOnPage, True, ddNPAfter
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarReporteEstadoDeCuenta = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarReporteEstadoDeCuenta", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteHistoricoProveedor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioABs As Boolean, ByVal valUnaPaginaPorCliente As Boolean, ByVal NombreCompaniaParaInformes As String, _
                  ByVal MensajeMonedaReporteCambioDelDia As String, ByVal gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_HISTORICO_DE_PROVEEDOR)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                    gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", MensajeMonedaReporteCambioDelDia
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoReporte", "", "TipoReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTituloTipoReporte", "", "TituloTipoReporte"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicial", "", "SaldoInicial"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaDocumento", eFT_DATE
         If gGlobalization.fEsCodigoPeru Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoGH", "", "TipoDocumentoLey"
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoGH", "", "TipoDeDocumento"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoDocumentoParaAgrupar", "", "NumeroDocumentoGrupo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoActual", "", "SaldoActual"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusPago", "", "StatusPago"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoDetail", "", "TipoDocumentoDetalle"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoCheque", "", "NumeroPago"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaPago", "", "FechaPago", eFT_DATE
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoPagado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoOriginal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoPagado", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoActual", "", ""
         If valUnaPaginaPorCliente Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GFMoneda", "", ddGrpAll, ddRepeatOnPage, True, ddNPAfter
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteHistoricoProveedor = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelReporteHistoricoProveedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteAnalisisDeVencimientoEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMonedaOriginal As Boolean, _
                                                               ByVal valTotalizaPorMes As Boolean, ByVal valPropAnalisisVencActual As Boolean, ByVal NombreCompaniaParaInformes As String, ByVal MensajeMonedaReporteCambioOriginal As String, ByVal valPrimerVencimiento As Long, _
                                                               ByVal valSegundoVencimiento As Long, ByVal valTercerVencimiento As Long) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
'   Dim InsAdmPropAnalisisVenc As clsPropAnalisisVencNavigator
   On Error GoTo h_ERROR
   exito = False
'   Set InsAdmPropAnalisisVenc = New clsPropAnalisisVencNavigator
   If valPropAnalisisVencActual Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS)
      If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
            gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
            gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                       gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
               If valMonedaOriginal Then
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", ""
               Else
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", MensajeMonedaReporteCambioOriginal
               End If
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango1Vto", "1-" & valPrimerVencimiento
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango2Vto", valPrimerVencimiento + 1 & "-" & valSegundoVencimiento
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango3Vto", valSegundoVencimiento + 1 & "-" & valTercerVencimiento
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango4Vto", valTercerVencimiento + 1 & " +"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
            gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes", "", "Mes"
            gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtAno", "", "Ano"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoProveedor", "", "CodigoProveedor"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxP", "", "Numero"
            gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
            gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVto", "", "FechaVencimiento", eFT_DATE
            gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDiasVecidos", "", "DiasVencidos"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMtoNoVencido", "", "MtoNoVencido"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMto1Vto", "", "Mto1Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMto2Vto", "", "Mto2Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMto3Vto", "", "Mto3Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMto4Vto", "", "Mto4Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "MtoTotal"
            gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtGFMes", "", ""
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMtoNoVencido", "", "MtoNoVencido"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMto1Vto", "", "Mto1Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMto2Vto", "", "Mto2Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMto3Vto", "", "Mto3Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMto4Vto", "", "Mto4Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFTotal", "", "MtoTotal"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMMoneda", "", "MonedaReporte"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMtoNoVencido", "", "MtoNoVencido"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto1Vto", "", "Mto1Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto2Vto", "", "Mto2Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto3Vto", "", "Mto3Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto4Vto", "", "Mto4Vto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMTotal", "", "MtoTotal"
            If valTotalizaPorMes Then
               gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMes", "Mes", ddGrpAll, ddRepeatOnPage, True, ddNPNone
            Else
               gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHMes", False
               gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFMes", False
            End If
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS)
            Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
            exito = True
        
         End If
      End If
   End If
'   Set InsAdmPropAnalisisVenc = Nothing
   fConfigurarDatosDelReporteAnalisisDeVencimientoEntreFechas = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelReporteAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCxPEntreFechasCxPPendientesEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                                  ByVal valSqlDelReporte As String, _
                                                                                   ByVal valFechaInicial As Date, _
                                                                                    ByVal valFechaFinal As Date, _
                                                                                     ByVal valMonedaOriginal As Boolean, _
                                                                                      ByVal valCambioOriginal As Boolean, _
                                                                                       ByVal valSoloPendientes As Boolean, _
                                                                                        ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   exito = False
   Set gEnumReport = New clsEnumReport
   If valSoloPendientes Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_CxP_PENDIENTES_ENTRE_FECHAS)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_CxP_ENTRE_FECHAS)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                    "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, _
                                    gProyParametros.GetImprimirFechaDeEmision
         If Not valMonedaOriginal Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaDocumento", False
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaDocumento", ""
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", ""
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaReporte", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoProveedor", "", "CodigoProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDocumento", "", "MonedaDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "MontoRestante"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontos", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMonedaReporte", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", ""
         gUtilMargins.sAsignarMargenesGenerales refRpt, , fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_CxP_ENTRE_FECHAS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteCxPEntreFechasCxPPendientesEntreFechas = exito
   Set gEnumReport = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelReporteCxPEntreFechasCxPPendientesEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelComprobanteCxP(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal Fecha As Date, ByVal NombreCompaniaParaInformes As String, _
                                          ByRef insRetPago As Object, ByRef insRetDocPagado As Object, ByRef gAdmAlicuotaIvaActual As Object, ByVal valUsaRetencion As Boolean, ByVal valSeHizoLaRetencion As Boolean, _
                                          ByVal valNumeroCxP As String, ByVal PuedeUsarOptDeContribEspecial As Boolean, ByVal valSqlDelSubReporte As String, ByVal valRetieneEnCxp As Boolean, _
                                          ByVal valEsUnaCuentaDeTerceros As Boolean, ByVal valNombreMonedaActual As String, ByRef gGlobalization As Object, ByVal valAlicuotaEspecialUsada As String) As Boolean
   Dim nombreDelFileXML  As String
   Dim exito As Boolean
   'Dim insRetPago As clsRetPagoNavigator
   'Dim insRetDocPagado As clsRetDocumentoPagadoNavigator
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_COMPROBANTES_DE_CUENTA_POR_PAGAR)
      If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
          gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeCxP", "", "TipoDeCxP"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroControl", "", "NumeroControl"
          gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
          gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDiasVencimiento", "", "DiasVencimiento"
          gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCancelacion", "", "FechaCancelacion", eFT_DATE
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCxPHistorico", "", "CxPHistorico"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambioABolivares", "", "CambioABolivares"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoProveedor", "", "CodigoProveedor"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRifProveedor", "", "NumeroRif"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtObservaciones", "", "Observaciones"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "Total"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonado", "", "MontoAbonado"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRestaPorPagar", "", "RestaPorPagar"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMes", "", "Mes"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAno", "", "Ano"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipodeCompra", "", "TipodeCompra"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPlanillaImportacion", "", "PlanillaImportacion"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtExpedienteImportacion", "", "ExpedienteImportacion"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoTransaccion", "", "TipoTransaccion"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFacturaAfectada", "", "FacturaAfectada"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCreditoFiscal", "", "CreditoFiscal"
          If valAlicuotaEspecialUsada <> "" Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAlicuotaGeneral", valAlicuotaEspecialUsada
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAlicuotaGeneralIVA", valAlicuotaEspecialUsada
          Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAlicuotaGeneral", gAdmAlicuotaIvaActual.GetAlicuotaIVA(Fecha, eTD_ALICUOTAGENERAL)
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAlicuotaGeneralIVA", gAdmAlicuotaIvaActual.GetAlicuotaIVA(Fecha, eTD_ALICUOTAGENERAL)
          End If
          gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAlicuota2", gAdmAlicuotaIvaActual.GetAlicuotaIVA(Fecha, eTD_ALICUOTA2)
          gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAlicuota2IVA", gAdmAlicuotaIvaActual.GetAlicuotaIVA(Fecha, eTD_ALICUOTA2)
          gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAlicuota3", gAdmAlicuotaIvaActual.GetAlicuotaIVA(Fecha, eTD_ALICUOTA3)
          gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAlicuota3IVA", gAdmAlicuotaIvaActual.GetAlicuotaIVA(Fecha, eTD_ALICUOTA3)
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExentoIVA", "", "MontoExento"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAGMontoGravable", "", "AGMontoGravable"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAGMontoIVA", "", "AGMontoIVA"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtA2MontoGravable", "", "A2MontoGravable"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtA2MontoIVA", "", "A2MontoIVA"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtA3MontoGravable", "", "A3MontoGravable"
          gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtA3MontoIVA", "", "A3MontoIVA"
          
          If valEsUnaCuentaDeTerceros Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblProveedorServicio", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodProvServicio", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNombreProvServicio", True
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodProvServicio", "", "CodigoProvOriginal"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProvServicio", "", "NombreProvOriginal"
          Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblProveedorServicio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodProvServicio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNombreProvServicio", False
          End If
          
        If valUsaRetencion Then
          If valSeHizoLaRetencion Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeNoRetencion", False
'            Set insRetDocPagado = New clsRetDocumentoPagadoNavigator
'            Set insRetPago = New clsRetPagoNavigator
            If insRetDocPagado.fSearchByField("NumeroDelDocumento", valNumeroCxP) Then
               If insRetPago.fSearchByNumericField("NumeroComprobante", insRetDocPagado.GetNumeroComprobante, True) Then
                     If (valRetieneEnCxp) Then
                        gUtilReports.fAsignaSubReporteSiExite refRpt, getSubReportComprobanteDeRetencionEnCxP(valSqlDelSubReporte, insRetPago.GetNroComprobanteRetISLR), "srptComprobanteRetencionISLR"
                        gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHRetencion", False, 0
                     Else
                       gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHRetencionISLREnCxp", False, 0
                     End If
                     gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaRetencion", insRetPago.GetFecha, "", eFT_DATE
                     gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobante", insRetPago.GetNumeroComprobante, ""
                     gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroReferencia", insRetPago.GetNumeroReferencia, ""
                     gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoRetencion", insRetDocPagado.GetCodigoRetencion, ""
                     gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", gConvert.fNumeroStringToStringConSeparadorDeMiles(insRetDocPagado.GetMontoOriginal), ""
                     gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", gConvert.fNumeroStringToStringConSeparadorDeMiles(insRetDocPagado.GetMontoBaseImponible), ""
                     gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDeRetencion", gConvert.fNumeroStringToStringConSeparadorDeMiles(insRetDocPagado.GetMontoRetencion), ""
                End If
            End If
          Else
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeNoRetencion", True
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeNoRetencion", "Sobre esta Cuenta por Pagar no se generó monto de Retención de I.S.L.R. o este monto es Cero (0)."
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblFechaRetencion", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtFechaRetencion", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNumeroComprobante", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNumeroComprobante", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNumeroReferencia", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNumeroReferencia", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoRetencion", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodigoRetencion", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMontoOriginal", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMontoOriginal", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblBaseImponible", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtBaseImponible", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMontoDeRetencion", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMontoDeRetencion", False
                 
         End If
       Else
           gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHRetencionISLREnCxp", False, 0
           gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHRetencion", False, 0
                
       End If
          If PuedeUsarOptDeContribEspecial Then
             gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHRetencionIVA", True, 1440
             gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteRetencionIVA", "", "NumeroComprobanteRetencionIVA"
             gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaAplicacion", "", "FechaAplicacion", eFT_DATE
             gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtModuloDeOrigen", "", "ModuloDeOrigen"
             gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPorcentajeRetenido", "", "PorcentajeRetenido"
             gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoRetencionIVA", "", "MontoRetencionIVA"
          Else
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHRetencionIVA", False, 0
          End If
          gUtilMargins.sAsignarMargenesGenerales refRpt, ddODefault, fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_COMPROBANTES_DE_CUENTA_POR_PAGAR)
          Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
          exito = True
      End If
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRifProveedor", gGlobalization.fPromptIVA & " Proveedor"
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambioABolivares", "Cambio a " & valNombreMonedaActual
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblDatosDelIVA", "Datos del " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblAGMontoIVA", "% Monto " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblA2MontoIVA", "% Monto " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblA3MontoIVA", "% Monto " & gGlobalization.fPromptIVA
      If gGlobalization.fEsCodigoPeru Then
        gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoExentoIVA", "Monto No Gravable"
        gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHRetencionISLREnCxp", False, 0
        gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHRetencionIVA", False, 0
      End If
    End If
h_EXIT:
    fConfigurarDatosDelComprobanteCxP = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelComprobanteCxP", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteRetencionesDeIvaEnCxP(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                                  ByVal valSqlDelReporte As String, _
                                                                                   ByVal valFechaInicial As Date, _
                                                                                    ByVal valFechaFinal As Date, _
                                                                                     ByVal valMonedaOriginal As Boolean, _
                                                                                      ByVal valCambioOriginal As Boolean, _
                                                                                        ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   exito = False
   Set gEnumReport = New clsEnumReport
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_RET_IVA_EN_CXP)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                    "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, _
                                    gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaReporte", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoProveedor", "", "CodigoProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcRetencion", "", "porcentajeRetencionAplicado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRetencion", "", "MontoRetenido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "MontoRestante"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontos", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFTotalRetencion", "", "MontoRetenido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMonedaReporte", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaReporte", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, , fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_RET_IVA_EN_CXP)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteRetencionesDeIvaEnCxP = exito
   Set gEnumReport = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelReporteCxPEntreFechasCxPPendientesEntreFechas1", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConfigurarDatosDelReporteCxPSinRetenciones(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_CX_PSIN_RETENCIONES)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
                gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
       
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                    "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, _
                                    gProyParametros.GetImprimirFechaDeEmision
    

         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroControl", "", "NumeroControl"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreOperado", "", "NombreOperador"



         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_CX_PSIN_RETENCIONES)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteCxPSinRetenciones = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteCxPSinRetenciones = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCxPSinRetenciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function getSubReportComprobanteDeRetencionEnCxP(ByVal valSqlDelReporte As String, ByVal valNumeroComprobante As String) As DDActiveReports2.ActiveReport
   Dim rptSubReport As New DDActiveReports2.ActiveReport
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Set rptSubReport = New DDActiveReports2.ActiveReport
   nombreFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_COMPROBANTE_DE_RETENCION)
   
   If gUtilReports.fLoadLayout(rptSubReport, nombreFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(rptSubReport, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtNoComprobante", valNumeroComprobante
         gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtNumeroDelDocumento", "", "NumeroDelDocumento"
         gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtNumeroControl", "", "NumeroControl"
         gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtConceptoDelPago", "", "TipoDePago"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtBaseImponible", "", "MontoBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTarifa", "", "Tarifa"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoRetenido", "", "MontoRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtSustraendo", "", "Sustraendo"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalBaseImponible", "", "MontoBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalMontoRetenido", "", "MontoRetencion"
         gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtFechaFact", "", "FechaFact"
       
            rptSubReport.PageSettings.LeftMargin = 600
            rptSubReport.PageSettings.TopMargin = 400
            rptSubReport.PageSettings.RightMargin = 100
            rptSubReport.PageSettings.BottomMargin = 1000
      End If
   End If
   Call rptSubReport.AddNamedItem("gUtilReports", gUtilReports)
   Set getSubReportComprobanteDeRetencionEnCxP = rptSubReport
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "getSubReportComprobanteDeRetencionEnCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelRpxDeComprobanteDeRetencionDeActividadesEconomicas() As String
   Dim nombreFile As String
   On Error GoTo h_ERROR
   nombreFile = mNombrePlantillaRetencionImpuestoMunicipal
   If nombreFile = "" Then
      nombreFile = "rpxComprobanteDeRetencionDeActividadesEconomicas"
   End If
h_EXIT:
   fNombreDelRpxDeComprobanteDeRetencionDeActividadesEconomicas = nombreFile
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelRpxDeComprobanteDeRetencionDeActividadesEconomicas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteComprobanteDeRetencionDeActividadesEconomicas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNombrePlantillaNombrePlantillaRetencionImpuestoMunicipal, ByVal valRutaNombreExtencionArchivoImagenLogo As String, ByVal valRutaNombreExtencionArchivoImagenSello As String, ByVal valRutaNombreExtencionArchivoImagenFirma As String, ByVal valLibSaw As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   
   mNombrePlantillaRetencionImpuestoMunicipal = valNombrePlantillaNombrePlantillaRetencionImpuestoMunicipal
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_COMPROBANTE_DE_RETENCION_DE_ACTIVIDADES_ECONOMICAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                    "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, _
                                    gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRUC", "", "NumeroRUC"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMunicipio", "", "NombreMunicipio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobante", "", "NumeroComprobante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRazonSocialAgente", "", "RazonSocialAgente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroLicenciaActividadEconominca", "", "NumeroLicenciaActividadEconominca"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroRifAgente", "", "NumeroRifAgente"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCxp", "", "FechaCxp", eFT_DATE_LONG
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaRetencion", "", "FechaAplicacionImpuestoMunicipal", eFT_DATE_LONG
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccionAgente", "", "DireccionAgente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRazonSocialSujetoRetenido", "", "RazonSocialSujetoRetenido"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroLicenciaActividadEconomincaRetenido", "", "NumeroLicenciaActividadEconomincaRetenido"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroRifAgenteSujetoRetenido", "", "NumeroRifAgenteSujetoRetenido"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtYear", "", "Year"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMES", "", "MES"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccionSujetoRetenido", "", "DireccionSujetoRetenido"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeOperacion", "", "TipoDeOperacion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCxpDetalle", "", "FechaCxpDetalle", eFT_DATE_LONG
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxp", "", "NumeroCxp"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroControlFactura", "", "NumeroControlFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDeNotaDebito", "", "NumeroDeNotaDebito"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDeNotaCREDITO", "", "NumeroDeNotaCREDITO"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeTransaccion", "", "TipoDeTransaccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDeLaFacturaAfectada", "", "NumeroDeLaFacturaAfectada"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComprasSinIVABaseImponible", "", "TotalComprasSinIVABaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtALICUOTA", "", "ALICUOTA"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGRUPO", "", "GRUPO"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImpuestoMunicipalRetenido", "", "ImpuestoMunicipalRetenido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExento", "", "MontoExentoCxp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravado", "", "MontoGravadoCxp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIva", "", "MontoIvaCxp"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFacturado", "", "MontoTotalFacturadoCxp"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionActividad", "", "DescripcionActividad"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroComprobante", "", "NroComprobanteCxp"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefonoAgenteSujetoRetenido", "", "TelefonoAgenteSujetoRetenido"
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalComprasSinIVABaseImponible_SecAgenteRetencion", "TotalComprasSinIVABaseImponible", eSF_SUM, "GHSecAgenteRetencion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_ALICUOTA_SecAgenteRetencion", "ALICUOTA", eSF_SUM, "GHSecAgenteRetencion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_ImpuestoMunicipalRetenido_SecAgenteRetencion", "ImpuestoMunicipalRetenido", eSF_SUM, "GHSecAgenteRetencion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalComprasSinIVABaseImponible_SecAgenteRetencion", "", "TotalComprasSinIVABaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_ALICUOTA_SecAgenteRetencion", "", "ALICUOTA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_ImpuestoMunicipalRetenido_SecAgenteRetencion", "", "ImpuestoMunicipalRetenido"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecAgenteRetencion", "Numero", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
                  
         valLibSaw.sConfiguracionParaCampoImagenRpt refRpt, "imgLogo", valLibSaw.fRutaNombreExtencionArchivoImagen(valRutaNombreExtencionArchivoImagenLogo)
         valLibSaw.sConfiguracionParaCampoImagenRpt refRpt, "imgFirma", valLibSaw.fRutaNombreExtencionArchivoImagen(valRutaNombreExtencionArchivoImagenFirma)
         valLibSaw.sConfiguracionParaCampoImagenRpt refRpt, "imgSello", valLibSaw.fRutaNombreExtencionArchivoImagen(valRutaNombreExtencionArchivoImagenSello)
                  
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_COMPROBANTE_DE_RETENCION_DE_ACTIVIDADES_ECONOMICAS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteComprobanteDeRetencionDeActividadesEconomicas = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteComprobanteDeRetencionDeActividadesEconomicas = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprobanteDeRetencionDeActividadesEconomicas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteResumenRetencionDeActividadesEconomicas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valEsPorMunicipio As Boolean, ByVal valEsMunicipioCristobalRojas As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valEsPorMunicipio = True Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_POR_MUNICIPIO)
   Else
      If valEsMunicipioCristobalRojas Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_CRISTOBAL_ROJAS)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS)
      End If
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                    "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, _
                                    gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroRifAgente", "", "NumeroRifAgente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroLicenciaActividadEconominca", "", "NumeroLicenciaActividadEconominca"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRazonSocialAgente", "", "RazonSocialAgente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccionAgente", "", "DireccionAgente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtYear", "", "Year"
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMES", "", "MES"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMunicipio", "", "NombreMunicipio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroRifAgenteSujetoRetenido", "", "NumeroRifAgenteSujetoRetenido"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRazonSocialSujetoRetenido", "", "RazonSocialSujetoRetenido"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaAplicacionImpuestoMunicipal", "", "FechaAplicacionImpuestoMunicipal", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaFactura", "", "Fecha", eFT_DATE_LONG
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxp", "", "NumeroCxp"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroControlFactura", "", "NumeroControlFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroPatente", "", "NumeroRuc"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoActividad", "", "CodigoActividad"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoContribuyente", "", "CodigoContribuyente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadPagada", "", "CantidadPagada"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBaseImponible", "", "MontoBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtALICUOTA", "", "AlicuotaRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImpuestoMunicipalRetenido", "", "MontoRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtContador", "", "Contador"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CantidadPagada_SecAgenteRetencion", "CantidadPagada", eSF_SUM, "GHSecAgenteRetencion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_MontoBaseImponible_SecAgenteRetencion", "MontoBaseImponible", eSF_SUM, "GHSecAgenteRetencion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_ALICUOTA_SecAgenteRetencion", "AlicuotaRetencion", eSF_SUM, "GHSecAgenteRetencion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_ImpuestoMunicipalRetenido_SecAgenteRetencion", "MontoRetencion", eSF_SUM, "GHSecAgenteRetencion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Contador_SecAgenteRetencion", "Contador", eSF_SUM, "GHSecAgenteRetencion", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CantidadPagada_SecAgenteRetencion", "", "CantidadPagada"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_MontoBaseImponible_SecAgenteRetencion", "", "MontoBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_ALICUOTA_SecAgenteRetencion", "", "AlicuotaRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_ImpuestoMunicipalRetenido_SecAgenteRetencion", "", "MontoRetencion"
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtT_Contador_SecAgenteRetencion", "", "Contador"
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivo", "", "NumeroConsecutivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumento", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExento", "", "MontoExento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobante", "", "NumeroComprobanteImpuestoMunicipal"
         If valEsPorMunicipio = True Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS_POR_MUNICIPIO)
         Else
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreFileDeImpresionDelReporteDeCxP(eCxPRpt_RESUMEN_RETENCION_DE_ACTIVIDADES_ECONOMICAS)
         End If
               
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteResumenRetencionDeActividadesEconomicas = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteResumenRetencionDeActividadesEconomicas = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteResumenRetencionDeActividadesEconomicas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

