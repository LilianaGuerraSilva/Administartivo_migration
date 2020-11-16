VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCobranzaRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Public mAcumMontoAbonado As Currency
Public mAcumComision As Currency
Public mTotalMontoComisionable As Currency
Public mRepUsarCambioOriginal As Boolean
Public mCambioABsCur As Currency
Public rTotalSunmaComision As Currency
Public rReporteResumen As Integer
Private gUtilDate As clsUtilDate

Private Const CM_FILE_NAME As String = "clsCobranzaRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reportes Cobranza"

Private Const CM_RPT_COMISION_DE_VENDEDOR_DETALLADO As Integer = 0
Private Const CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO As Integer = 1
Private Const CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCS_COBRADOS_COBRANZAS As Integer = 2
Private Const CM_RPT_COMPARATIVO_COBRANZA_POR_ANOS As Integer = 5
Private Const CM_RPT_INFORME_DE_RETENCION_IVA As Integer = 6
Private Const CM_RPT_COBRANZAS_CON_RETENCION_IVA_PENDIENTE_POR_DISTRIBUIR As Integer = 7
Private Const CM_RPT_COBRANZAS_ENTRE_FECHAS As Integer = 8
Private Const CM_RPT_COBRANZAS_POR_DIA As Integer = 9
Private Const CM_RPT_COBRANZAS_POR_VENDEDOR As Integer = 10
Private Const CM_RPT_COBRANZAS_POR_VENDEDOR_DETALLADO As Integer = 11
Private Const CM_RPT_COMPROBANTE_ANTICIPOS_EN_COBRANZA As Integer = 12
Private Const CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_VENTA As Integer = 13
Private Const CM_RPT_COMISION_DE_VENDEDOR_DIAS_VENCIDOS As Integer = 14
Private Const CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO As Integer = 15
Private Const CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO_DOCUMENTOS_COBRADOS As Integer = 16
Private Const CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCUMENTOS_COBRADOS As Integer = 17
Private Const CN_RPT_COMISION_DE_VENTAS_POR_VENDEDOR_Y_ARTICULO As Integer = 18
Private Const CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO_POR_ARTICULO As Integer = 19
Private Const CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO_COBRANZAS As Integer = 20
Private Const CM_RPT_COMISION_DE_AGENTES As Integer = 21
Private Const CM_RPT_COMISION_DE_AGENTES_X_LINEA_DE_PRODUCTO As Integer = 22
Private Const CM_RPT_DESGLOSE_DE_COBRANZA_X_DIA As Integer = 23
Private Const CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO_EN_COBRANZA As Integer = 24
Private Const CM_RPT_DESGLOSE_DE_COBRANZA_X_CLIENTE As Integer = 25
Private Const CM_RPT_COMISION_POR_VENDEDOR_SIN_MONEDA As Integer = 26
Private Const CM_RPT_COBRANZAS_ENTRE_FECHAS_AGRUPADO As Integer = 27
Private Const CM_RPT_COBRANZAS_POR_ZONA = 28
Private Const CM_RPT_COBRANZA_RENTENCION_IVA_FORMAL = 29
Private Const CN_RPT_COMISION_DE_COBRANZA_POR_VENDEDOR_Y_ARTICULO As Integer = 30
Private Const CN_RPT_COMISION_DE_COBRANZA_POR_VENDEDOR_Y_ARTICULO_RESUMIDO As Integer = 31
Private Const CM_RPT_INFORME_DE_RETENCION_ISLR As Integer = 32
Private Const CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCS_COBRADOS_COBRANZAS_DIASTRANS As Integer = 33

Private Enum enum_OpcionInfCobranzaEntreFecha
   eOC_Cobrador = 0
   eOC_Cliente
   eOC_Banco
End Enum
Public Function CM_RPT_COMPROBANTE_DE_COBRANZA() As Integer
   CM_RPT_COMPROBANTE_DE_COBRANZA = 3
End Function

Public Function CM_RPT_COMISION_X_COBRANZA_X_VENDEDORES_ENTRE_FECHAS() As Integer
   CM_RPT_COMISION_X_COBRANZA_X_VENDEDORES_ENTRE_FECHAS = 4
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Sub Class_Initialize()
   Set gUtilDate = New clsUtilDate
End Sub

Private Sub Class_Terminate()
   Set gUtilDate = Nothing
End Sub

Private Function fNombreFileDelRptDeInformesCobranza(ByVal valReporte As Integer, Optional ByVal NombrePlantillaCompobanteCobranza As String) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_COMISION_POR_VENDEDOR_SIN_MONEDA: nombreFileXML = "rpxComisionPorVendedorSinMoneda"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCUMENTOS_COBRADOS: nombreFileXML = "rpxComisionXVendedorXDiasVencidoXDocCobrados"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO_DOCUMENTOS_COBRADOS: nombreFileXML = "rpxComisionXVendedorXDocumentoXMonto"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_VENTA: nombreFileXML = "rpxComisionPorVendedor"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO: nombreFileXML = "rpxComisionPorVendedor"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO: nombreFileXML = "rpxComisionPorVendedorPorMontoCobranza"
      Case CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO: nombreFileXML = "rpxComisionXVendedorResumido"
      Case CM_RPT_COMISION_DE_VENDEDOR_DIAS_VENCIDOS: nombreFileXML = "rpxComisionPorVendedorPorDiasVencido"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCS_COBRADOS_COBRANZAS: nombreFileXML = "rpxComisionXVendedorXDiasVencidosXDocCobrado"
      Case CM_RPT_COMPROBANTE_DE_COBRANZA: nombreFileXML = fNombreDelRpxDeComprobanteDeCobranza(NombrePlantillaCompobanteCobranza)
      Case CM_RPT_COMISION_X_COBRANZA_X_VENDEDORES_ENTRE_FECHAS: nombreFileXML = "rpxComisionXCobranzaXVendEntreFech"
      Case CM_RPT_COMPARATIVO_COBRANZA_POR_ANOS: nombreFileXML = "rpxComparativoDeCobranzaPorAno"
      Case CM_RPT_INFORME_DE_RETENCION_IVA: nombreFileXML = "rpxCobranzaRetencionIVA"
      Case CM_RPT_COBRANZAS_CON_RETENCION_IVA_PENDIENTE_POR_DISTRIBUIR: nombreFileXML = "rpxCobranzasConRetIvaPendientePorDistribuir"
      Case CM_RPT_COBRANZAS_POR_DIA: nombreFileXML = "rpxCobranzasPorDia"
      Case CM_RPT_COBRANZAS_ENTRE_FECHAS: nombreFileXML = "rpxCobranzasEntreFechas"
      Case CM_RPT_COBRANZAS_POR_VENDEDOR: nombreFileXML = "rpxCobranzasXVendedor"
      Case CM_RPT_COBRANZAS_POR_VENDEDOR_DETALLADO: nombreFileXML = "rpxCobranzasXVendedorDetallado"
      Case CM_RPT_COMPROBANTE_ANTICIPOS_EN_COBRANZA: nombreFileXML = "rpxSubComprobanteDeAnticiposCobro"
      Case CN_RPT_COMISION_DE_VENTAS_POR_VENDEDOR_Y_ARTICULO: nombreFileXML = "rpxComisionPorVentasDeVendedorYPorArticulo"
      Case CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO_POR_ARTICULO: nombreFileXML = "rpxComisionPorVentasDeVendedorYPorArticuloResumido"
      Case CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO_COBRANZAS: nombreFileXML = "rpxComisionXVendedorResumidoCobranzas"
      Case CM_RPT_COMISION_DE_AGENTES: nombreFileXML = "rpxComisionDeAgentes"
      Case CM_RPT_COMISION_DE_AGENTES_X_LINEA_DE_PRODUCTO: nombreFileXML = "rpxComisionesAgentesXLineaDeProducto"
      Case CM_RPT_DESGLOSE_DE_COBRANZA_X_DIA: nombreFileXML = "rpxCobranzaDesglosePorDia"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO_EN_COBRANZA: nombreFileXML = "rpxComisionPorVendedorEnCobranzas"
      Case CM_RPT_DESGLOSE_DE_COBRANZA_X_CLIENTE: nombreFileXML = "rpxCobranzaDesglosePorCliente"
      Case CM_RPT_COBRANZAS_ENTRE_FECHAS_AGRUPADO: nombreFileXML = "rpxCobranzasEntreFechasAgrupado"
      Case CM_RPT_COBRANZAS_POR_ZONA: nombreFileXML = "rpxCobranzasPorZona"
      Case CM_RPT_COBRANZA_RENTENCION_IVA_FORMAL: nombreFileXML = "rpxCobranzaRentencionIvaFormal"
      Case CN_RPT_COMISION_DE_COBRANZA_POR_VENDEDOR_Y_ARTICULO: nombreFileXML = "rpxComisionPorCobranzaDeVendedorYPorArticulo"
      Case CN_RPT_COMISION_DE_COBRANZA_POR_VENDEDOR_Y_ARTICULO_RESUMIDO: nombreFileXML = "rpxComisionPorCobranzaDeVendedorYPorArticuloResumido"
      Case CM_RPT_INFORME_DE_RETENCION_ISLR: nombreFileXML = "rpxCobranzaRetencionISLR"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCS_COBRADOS_COBRANZAS_DIASTRANS: nombreFileXML = "rpxComisionXVendedorXDiasVencidosDiasTransc"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDelRptDeInformesCobranza = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeCobranzas(valReporte))
   Else
      fNombreFileDelRptDeInformesCobranza = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDelRptDeAnticiposCobrados", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fNombreDelReporteDeCobranzas(ByVal valReporte As Integer, Optional ByVal valOpcion As Integer = -1) As String
   Select Case valReporte
      Case CM_RPT_COMISION_POR_VENDEDOR_SIN_MONEDA: fNombreDelReporteDeCobranzas = "Comisión por Vendedor - Detallado"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCUMENTOS_COBRADOS: fNombreDelReporteDeCobranzas = ""
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO_DOCUMENTOS_COBRADOS: fNombreDelReporteDeCobranzas = ""
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_VENTA: fNombreDelReporteDeCobranzas = ""
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO: fNombreDelReporteDeCobranzas = ""
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO: fNombreDelReporteDeCobranzas = ""
      Case CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO: fNombreDelReporteDeCobranzas = "Comisión De Vendedores Resumido"
      Case CM_RPT_COMISION_DE_VENDEDOR_DIAS_VENCIDOS: fNombreDelReporteDeCobranzas = "Comisión de Vendedores por Dias Vencidos"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCS_COBRADOS_COBRANZAS: fNombreDelReporteDeCobranzas = "Comisión de Vendedores por Dias Vencidos Detallado"
      Case CM_RPT_COMPROBANTE_DE_COBRANZA:  fNombreDelReporteDeCobranzas = "Comprobante de Cobranza"
      Case CM_RPT_COMISION_X_COBRANZA_X_VENDEDORES_ENTRE_FECHAS: fNombreDelReporteDeCobranzas = "Comisión x Cobranza x Vendedores entre Fechas"
      Case CM_RPT_COMPARATIVO_COBRANZA_POR_ANOS: fNombreDelReporteDeCobranzas = "Comparativo de Cobranza Por Año"
      Case CM_RPT_INFORME_DE_RETENCION_IVA: fNombreDelReporteDeCobranzas = "Listado de Retención de IVA"
      Case CM_RPT_COBRANZAS_CON_RETENCION_IVA_PENDIENTE_POR_DISTRIBUIR: fNombreDelReporteDeCobranzas = "Cobranzas con Retención de Iva Pendiente por Distribuir"
      Case CM_RPT_COBRANZAS_ENTRE_FECHAS: fNombreDelReporteDeCobranzas = fNombreDelReporteDeCobranzasEntreFechas(valOpcion)
      Case CM_RPT_COBRANZAS_POR_VENDEDOR: fNombreDelReporteDeCobranzas = "Cobranzas por Vendedor"
      Case CM_RPT_COBRANZAS_POR_VENDEDOR_DETALLADO: fNombreDelReporteDeCobranzas = "Cobranzas por Vendedor Detallado"
      Case CM_RPT_COMPROBANTE_ANTICIPOS_EN_COBRANZA: fNombreDelReporteDeCobranzas = "Sub Reporte - Comprobante de Anticipos de Cobranza"
      Case CM_RPT_COMISION_DE_AGENTES: fNombreDelReporteDeCobranzas = "Comisión de Agentes"
      Case CM_RPT_COMISION_DE_AGENTES_X_LINEA_DE_PRODUCTO: fNombreDelReporteDeCobranzas = "Comisión de Agentes por Línea de Producto"
      Case CM_RPT_DESGLOSE_DE_COBRANZA_X_DIA: fNombreDelReporteDeCobranzas = "Desglose de cobranza por Día"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO_EN_COBRANZA: fNombreDelReporteDeCobranzas = ""
      Case CM_RPT_DESGLOSE_DE_COBRANZA_X_CLIENTE: fNombreDelReporteDeCobranzas = "Desglose de cobranza por Cliente"
      Case CM_RPT_COBRANZAS_POR_ZONA: fNombreDelReporteDeCobranzas = "Cobranzas por Zona"
      Case CM_RPT_COBRANZA_RENTENCION_IVA_FORMAL: fNombreDelReporteDeCobranzas = "Informe De Retención De Iva"
      Case CN_RPT_COMISION_DE_COBRANZA_POR_VENDEDOR_Y_ARTICULO: fNombreDelReporteDeCobranzas = "Comisión x Cobranza x Vendedores x Porcentaje de Articulos"
      Case CN_RPT_COMISION_DE_COBRANZA_POR_VENDEDOR_Y_ARTICULO_RESUMIDO: fNombreDelReporteDeCobranzas = "Comisión x Cobranza x Vendedores x Porcentaje de Articulos Resumido"
      Case CM_RPT_INFORME_DE_RETENCION_ISLR: fNombreDelReporteDeCobranzas = "Listado de Retención de ISLR"
      Case CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCS_COBRADOS_COBRANZAS_DIASTRANS: fNombreDelReporteDeCobranzas = " Comision X Vendedor X Dias Vencidos - Por días Transcurridos"
      Case Else: fNombreDelReporteDeCobranzas = ""
   End Select
End Function

Public Function fConfigurarComisionPorVendedorSinMoneda(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim tituloDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_POR_VENDEDOR_SIN_MONEDA)
   tituloDelReporte = fNombreDelReporteDeCobranzas(CM_RPT_COMISION_POR_VENDEDOR_SIN_MONEDA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
    If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", tituloDelReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
           "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
           gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelVendedor", "", "CodigoCobrador"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVenededor"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaCobro", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroCobro"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDelDocumento", "", "TotalDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionableDelDocumento", "", "MontoComisionable"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoCobrador", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumento", "TotalDocumento", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoComisionable", "MontoComisionable", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentoGeneral", "TotalDocumento", eSF_SUM, "GHVendedor", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoComisionableGeneral", "MontoComisionable", eSF_SUM, "GHVendedor", eSR_GROUP, eST_GRAND_TOTAL
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
    End If
  End If
h_EXIT: On Error GoTo 0
   fConfigurarComisionPorVendedorSinMoneda = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarComisionPorVendedorSinMoneda", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelComprobante(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                      ByVal valSqlDelReporte As String, _
                                                       ByVal valInsCobranza As Object, _
                                                        ByVal valMonedaDocumentos As String, _
                                                         ByVal NombreCompaniaParaInformes As String, _
                                                          ByVal NombrePlantillaCompobanteCobranza As String, _
                                                           ByVal valSqlAnticipoUsado As String, _
                                                            ByRef gGlobalization As Object)
   Dim nombreDelFile As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFile = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMPROBANTE_DE_COBRANZA, NombrePlantillaCompobanteCobranza)
   If nombreDelFile = "" Then
      gMessage.Advertencia "No se ha seleccionado el formato de impresión del reporte"
   Else
      If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", NombreCompaniaParaInformes
            If valInsCobranza.GetTipoDeDocumentoAsEnum = enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion Then
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoAbonado", "Total Retención"
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblComprobanteDePago", "Comprobante de Aplicación de Retención en Cobranza"
            End If
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCobranza", "", "Numero"
            gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCuentaBancaria", "", "CodigoCuentaBancaria"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuentaBancaria", "", "NombreCuenta"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaCuentaBancaria", "", "NombreDeLaMonedaCuentaBancaria"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Cliente"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCobrador", "", "CodigoCobrador"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCobrador", "", "Cobrador"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoConcepto", "", "CodigoConcepto"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionConcepto", "", "DescripcionConcepto"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDocumentos", valMonedaDocumentos
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioAbolivares"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoDocumento", "", "NumeroDelDocumentoCobrado"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "MontoOriginalRestanteConvertido" 'antes MontoOriginalRestanteAlDiaDelC
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonado", "", "MontoAbonado"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIvaRetenido", "", "MontoIvaRetenido"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobateRetIVA", "", "NumeroComprobanteRetIVA"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteRetISLR", "", "NumeroComprobanteRetISLR"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoRestante", "", "MontoRestante"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRetISLR", "", gUtilReports.fNumericFormat("RetencionISRL")
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRetIva", "", gUtilReports.fNumericFormat("RetencionIVA")
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtOtros", "", gUtilReports.fNumericFormat("TotalOtros")
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEfectivo", "", gUtilReports.fNumericFormat("CobradoEfectivo")
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCheque", "", gUtilReports.fNumericFormat("CobradoCheque")
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescPP", "", gUtilReports.fNumericFormat("DescProntoPago")
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumCheque", "", "NumerodelCheque"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBancoCheque", "", "NombreBanco"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTarjeta", "", gUtilReports.fNumericFormat("CobradoTarjeta")
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumTarjeta", "", "NroDeLaTarjeta"
            If Len(valInsCobranza.GetNroDeLaTarjeta) > 0 Then
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoTarjeta", "", "CualTarjeta"
            Else
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoTarjeta", ""
            End If
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAnticipoCobrado", "", gUtilReports.fNumericFormat("CobradoAnticipo")
            If valInsCobranza.GetCobradoAnticipo <> 0 Then
               gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubReportAnticipos(valSqlAnticipoUsado), "srptAnticiposCobrados"
            End If
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", gUtilReports.fNumericFormat("Vuelto")
            gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gGlobalization", gGlobalization)
         exito = True
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelComprobante = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraLasSeccionesDelComprobante", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraRpxComisionXCobranzaXVendedoresEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valCantidadDeVendedoresAImprimir As Integer, _
                  ByVal valNombreDelVendedor As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                  ByVal NombreCompaniaParaInformes As String, ByVal insVendedor As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_X_COBRANZA_X_VENDEDORES_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaDeLaCobranza", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "NumeroDeLaCobranza"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroFactura", "", "NumeroFact"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaDeVencimiento", "", "FechaVenc", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDiasVencidos", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonado", "", "Abono"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeComision", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtComision", ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreDelVendedor", " ", "NombreDelVend"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDiasVencidos1", "", "DiasVencidos1"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDiasVencidos2", "", "DiasVencidos2"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDiasVencidos3", "", "DiasVencidos3"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDiasVencidos4", "", "DiasVencidos4"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDiasVencidos5", "", "DiasVencidos5"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeComision1", "", "PorcentajeComision1"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeComision2", "", "PorcentajeComision2"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeComision3", "", "PorcentajeComision3"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeComision4", "", "PorcentajeComision4"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeComision5", "", "PorcentajeComision5"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSunmaComision", "", rTotalSunmaComision
        
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", _
                                           "lblFechaHoraDeEmision", "lblTituloDelReporte", _
                                           "lblNumeroDePagina", "lblFechaInicialYFinal", False, True
         refRpt.Sections("GHVendedor").DataField = "NombreDelVend"
         refRpt.Sections("GHVendedor").GrpKeepTogether = ddGrpAll
         refRpt.Sections("GHVendedor").KeepTogether = True
         refRpt.Sections("GHVendedor").Repeat = ddRepeatOnPage
         refRpt.Sections("GHVendedor").NewPage = ddNPAfter
         refRpt.Sections("GHNumeroDeLaCobranza").DataField = "NumeroDeLaCobranza"
         refRpt.Sections("GHNumeroDeLaCobranza").GrpKeepTogether = ddGrpAll
         refRpt.Sections("GHNumeroDeLaCobranza").KeepTogether = True
         refRpt.Sections("GHNumeroDeLaCobranza").Repeat = ddRepeatOnPage
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("insRptCobranzaConfigurar", insVendedor)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         Call refRpt.AddNamedItem("gAdmParametrosAdmini", gProyParametros)
         exito = True
     End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraRpxComisionXCobranzaXVendedoresEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fConfiguraRpxComisionXCobranzaXVendedoresEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeRetencionIVA(ByRef refReporte As DDActiveReports2.ActiveReport, _
                     ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_INFORME_DE_RETENCION_IVA)
   If gUtilReports.fLoadLayout(refReporte, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refReporte, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblNombreDelReporte", fNombreDelReporteDeCobranzas(CM_RPT_INFORME_DE_RETENCION_IVA)
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblNombreCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblFechaYHoraDeEmision", "Emitido el " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblLineaAdicionalDeEncabezado", "Del " & gConvert.fechaAString(valFechaInicial) & " al " & gConvert.fechaAString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refReporte, "lblNombreCompania", "lblFechaYHoraDeEmision", _
                        "lblNombreDelReporte", "lblNumeroDePagina", "lblLineaAdicionalDeEncabezado", _
                        gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroDocCobrado", "", "NumeroDelDocumentoCobrado"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalCobrado", "", "TotalCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtRetencionIVA", "", "RetencionIva"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtRIF", "", "NumeroRIF"
'         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "TxtDocCobrado", "", "MontoEnMonedaOriginalDeCxC"
'         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtRetencionxDoc", "", "IvaRetenidoXDoc"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNroComprobanteRetIVA", "", "NumeroComprobanteRetIVA"
         gUtilMargins.sAsignarMargenesGenerales refReporte
         Call refReporte.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraElInformeDeRetencionIVA = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeRetencionIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteComisionXVendedorResumido(ByRef refRpt As DDActiveReports2.ActiveReport, _
               ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
               ByVal valComisionesPorVentas As Boolean, ByVal valUsarCambioOriginal As Boolean, _
               ByVal valOpcionesComisiones As enum_CalculoParaComisionesSobreCobranzaEnBaseA, _
               ByVal NombreCompaniaParaInformes As String, ByVal NombreCompaniaParaInformesFalse As String, _
               ByRef insVendedor As Object) As Boolean
   On Error GoTo h_ERROR
   fConfigurarDatosDelReporteComisionXVendedorResumido = False
   mRepUsarCambioOriginal = valUsarCambioOriginal
'       If valComisionesPorVentas Then
'            sConfigurarDatosDeComisionDeVendedor refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valComisionesPorVentas, NombreCompaniaParaInformesFalse
'         Else
'            sConfigurarDatosDeComisionDeVendedorCobranzas refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valUsarCambioOriginal, True, NombreCompaniaParaInformes
'         End If
'         Call refRpt.AddNamedItem("insRptCobranzaConfigurar", insVendedor)
'         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
'         fConfigurarDatosDelReporteComisionXVendedorResumido = True
   Select Case valOpcionesComisiones
      Case enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_MONTO
         If valComisionesPorVentas Then
            sConfigurarDatosDeComisionDeVendedor refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valComisionesPorVentas, NombreCompaniaParaInformesFalse
         Else
            sConfigurarDatosDeComisionDeVendedorCobranzas refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valUsarCambioOriginal, True, NombreCompaniaParaInformes
         End If
         Call refRpt.AddNamedItem("insRptCobranzaConfigurar", insVendedor)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         fConfigurarDatosDelReporteComisionXVendedorResumido = True
      Case enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_PORCENTAJE_POR_ARTICULO And valComisionesPorVentas
         sConfigurarDatosDeComisionDeVendedorPorArticuloResumido refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valComisionesPorVentas, NombreCompaniaParaInformesFalse
         fConfigurarDatosDelReporteComisionXVendedorResumido = True
      Case enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_PORCENTAJE_POR_ARTICULO And Not valComisionesPorVentas
         sConfigurarDatosDeComisionDeVendedorPorCobranzaPorArticuloResumido refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valComisionesPorVentas, NombreCompaniaParaInformesFalse
         fConfigurarDatosDelReporteComisionXVendedorResumido = True
      End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fConfigurarDatosDelReporteComisionXVendedorResumido", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteComisionXVendedorDetallado(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
                  ByVal valComisionesPorVentas As Boolean, ByVal valUsarCambioOriginal As Boolean, _
                  ByVal valUsarMonedaDelReporteBs As Boolean, ByVal valTotalizarPorVendedor As Boolean, _
                  ByVal valCobrazaporDiasVencido As Boolean, ByVal valTipoDeMensajeDeCambio As enum_MensajeMonedaParaInformes, _
                  ByVal valCobradosYCobranzas As Boolean, valDocumentosCobrados As Boolean, ByVal valOpcionesComisiones As enum_CalculoParaComisionesSobreCobranzaEnBaseA, _
                  ByVal valTipoReporte As enum_ReporteComisionesPorMontoYDiasVenc, ByVal NombreCompaniaParaInformes As String, _
                  ByVal valAsignaComisionDeVendedorEnCobranza As Boolean, ByVal insVendedor As Object, ByVal valMostrarDiasTranscurridos As Boolean) As Boolean
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   mRepUsarCambioOriginal = valUsarCambioOriginal
   Select Case valOpcionesComisiones
      Case eCP_DIASVENCIDOS:
         exito = fConfigurarDatosDeComisionDeVendedorDiasVencidos(refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valComisionesPorVentas, valUsarMonedaDelReporteBs, valTotalizarPorVendedor, valTipoDeMensajeDeCambio, valCobradosYCobranzas, valDocumentosCobrados, valTipoReporte, NombreCompaniaParaInformes, valMostrarDiasTranscurridos)
      Case eCP_MONTO
         exito = fConfigurarDatosDeComisionDeVendedorPorMonto(refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valComisionesPorVentas, valUsarMonedaDelReporteBs, valTotalizarPorVendedor, valTipoDeMensajeDeCambio, valCobradosYCobranzas, valDocumentosCobrados, valTipoReporte, NombreCompaniaParaInformes, valAsignaComisionDeVendedorEnCobranza)
      Case eCP_PORCENTAJE_POR_ARTICULO And valComisionesPorVentas
         exito = fConfigurarDatosDeComisionDeVentasPorVendedorYPorAriculo(refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valComisionesPorVentas, valUsarMonedaDelReporteBs, valTotalizarPorVendedor, valTipoDeMensajeDeCambio, valCobradosYCobranzas, valDocumentosCobrados, NombreCompaniaParaInformes)
      Case eCP_PORCENTAJE_POR_ARTICULO And Not valComisionesPorVentas
         exito = fConfigurarDatosDeComisionDeCobranzaPorVendedorYPorAriculo(refRpt, valSqlDelReporte, valfechaInicio, valFechaFinal, valComisionesPorVentas, valUsarMonedaDelReporteBs, valTotalizarPorVendedor, valTipoDeMensajeDeCambio, valCobradosYCobranzas, valDocumentosCobrados, NombreCompaniaParaInformes)
   End Select
   Call refRpt.AddNamedItem("insRptCobranzaConfigurar", insVendedor)
   Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
   fConfigurarDatosDelReporteComisionXVendedorDetallado = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fConfigurarDatosDelReporteComisionXVendedorDetallado", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
  
Private Sub sConfigurarDatosDeComisionDeVendedor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
               ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valComisionesXventas As Boolean, ByVal NombreCompaniaParaInformes As String)
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   rTotalSunmaComision = 0
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte, , 90) Then
         rReporteResumen = 0
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Comisión de Vendedores Por Ventas(Resumido)"
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreDelVendedor", "", "NombreDelVendedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCobrado", "", "TotalDelDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCobrado", "", "TotalDelDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionable", "", "MontoComisionable"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComisionable", "", "MontoComisionable"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSunmaComision", "", rTotalSunmaComision
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtReporteResumen", "", rReporteResumen
         
         gUtilMargins.sAsignarMargenesGenerales refRpt
           
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "sConfigurarDatosDeComisionDeVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fConfigurarDatosDeComisionDeVendedorPorMonto(ByRef refRpt As DDActiveReports2.ActiveReport, _
               ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
               ByVal valComisionesXventas As Boolean, ByVal valUsarMonedaDelReporteBs As Boolean, _
               ByVal valTotalizarPorVendedor As Boolean, ByVal valTipoDeMensajeDeCambio As enum_MensajeMonedaParaInformes, _
               ByVal valCobradosYCobranza As Boolean, ByVal valDocumentosCobrados As Boolean, ByVal valTipoReporte As enum_ReporteComisionesPorMontoYDiasVenc, _
               ByVal NombreCompaniaParaInformes As String, ByVal valAsignaComisionDeVendedorEnCobranza As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   rTotalSunmaComision = 0
   Select Case valTipoReporte
      Case eRC_Sencillo
         If Not valAsignaComisionDeVendedorEnCobranza Then
            nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO)
         Else
            nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO_EN_COBRANZA)
         End If
      Case eRC_DocumentoCobradosYCobranza
         nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DETALLADO)
      Case eRC_SoloDocumentosCobrados
         nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_MONTO_DOCUMENTOS_COBRADOS)
   End Select
   If valComisionesXventas Then
      nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_POR_VENTA)
   End If
   exito = False
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         If valComisionesXventas Then
            rReporteResumen = 0
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblReporteDeComisionesPorVendedor", "Comisión de Vendedores Por Ventas(Detallado)"
            gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkEsComisionsPorCobranza", False
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "SimboloDeLaMoneda"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTasaDeCambio", "", "CambioABolivares"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSunmaComision", "", rTotalSunmaComision
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "SimboloDeLaMoneda"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTasaDeCambio", "", "CambioABolivares"
            If valCobradosYCobranza Then
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblReporteDeComisionesPorVendedor", "Comisión de Vendedores por Cobranzas(Detallado) Cobrados y Cobranza"
            ElseIf valDocumentosCobrados Then
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblReporteDeComisionesPorVendedor", "Comisión de Vendedores Por Cobranzas(Detallado) Solo Doc Cobrados"
            Else
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblReporteDeComisionesPorVendedor", "Comisión de Vendedores Por Cobranzas(Detallado)"
            End If
            gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkEsComisionsPorCobranza", True
            rReporteResumen = 1
         End If
         Select Case valTipoReporte
         Case eRC_Sencillo
            If Not valAsignaComisionDeVendedorEnCobranza Then
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSunmaComision", "", rTotalSunmaComision
            End If
         Case Else
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSunmaComision", "", rTotalSunmaComision
         End Select
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblReporteDeComisionesPorVendedor", _
               "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelVendedor", "", "CodigoDelVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreDelVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroFactura", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaFactura", "", "FechaFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumentoCxC", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIva", "", "MontoIva"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "FechaDelDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDelDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaCobranza", "", "DescripcionDeLaMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDelDocumento", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumento", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumentoMoneda", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralMontoComisionable", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDeComision", "", "PorcentajeDeComision" '*****
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionableDelDocumento", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionable", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralComision", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralComisiones", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionableBs", "", "MontoComisionBs"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralMontoComisionable", "", "MontoComision"
             
            gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkTotalizarPorVendedor", valTotalizarPorVendedor
            If valUsarMonedaDelReporteBs Then
               gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkMostrarMoneda", False
            Else
               gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkMostrarMoneda", True
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambioOriginalODelDia", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambioOriginalODelDia", Trim(gEnumReport.getMensajesDeMonedaParaInformes(valTipoDeMensajeDeCambio))
         If valAsignaComisionDeVendedorEnCobranza Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDeComision", "", "PorcentajeDeComision"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtComisionVendedor", "", "ComisionVendedor"
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalComision", "ComisionVendedor", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalGeneralComision", "ComisionVendedor", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         End If
         If valDocumentosCobrados Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         ElseIf valCobradosYCobranza Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         ElseIf valAsignaComisionDeVendedorEnCobranza Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Else
            gUtilMargins.sAsignarMargenesGenerales refRpt
         End If
         exito = True
      End If
   End If
   fConfigurarDatosDeComisionDeVendedorPorMonto = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDeComisionDeVendedorPorMonto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfigurarDatosDeComisionDeVendedorDiasVencidos(ByRef refRpt As DDActiveReports2.ActiveReport, _
               ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
               ByVal valComisionesXventas As Boolean, ByVal valUsarMonedaDelReporteBs As Boolean, _
               ByVal valTotalizarPorVendedor As Boolean, ByVal valTipoDeMensajeDeCambio As enum_MensajeMonedaParaInformes, _
               ByVal valCobradosYCobranzas As Boolean, valDocumentosCobrados As Boolean, ByVal valTipoReporte As enum_ReporteComisionesPorMontoYDiasVenc, ByVal NombreCompaniaParaInformes As String, ByVal valMostrarDiasTranscurridos As Boolean) As Boolean
   
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   rTotalSunmaComision = 0
   Select Case valTipoReporte
      Case eRC_Sencillo
         nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DIAS_VENCIDOS)
      Case eRC_DocumentoCobradosYCobranza
         If valMostrarDiasTranscurridos Then
            nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCS_COBRADOS_COBRANZAS_DIASTRANS)
         Else
            nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCS_COBRADOS_COBRANZAS)
         End If
      Case eRC_SoloDocumentosCobrados
         nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_DETALLADO_DIAS_VENCIDOS_DOCUMENTOS_COBRADOS)
   End Select
   exito = False
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkEsComisionsPorCobranza", True
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblReporteDeComisionesPorVendedor", "Comisión de Vendedores Por Cobranzas(Detallado)"
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblReporteDeComisionesPorVendedor", "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                                   gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelVendedor", "", "CodigoDelVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreDelVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "FechaDelDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDelDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         If Not valCobradosYCobranzas Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "SimboloDeLaMoneda"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTasaDeCambio", "", "CambioABolivares"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroFactura", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaFactura", "", "FechaFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumentoCxC", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIva", "", "MontoIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDelDocumento", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumento", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralMontoComisionable", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionableDelDocumento", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionable", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionableBs", "", "MontoComisionBs"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralComision", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralComisiones", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionablePorPorcentaje", "", "MontoComisionablePorPorcentaje"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionablePorPorcentaje", "", "MontoComisionablePorPorcentaje"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDeComision", "", "PorcentajedeComisionPorDiasVencidos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComision", "", "MontoComisionablePorPorcentaje"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralMontoComisionable", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralComision", "", "MontoComisionablePorPorcentaje"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaCobranza", "", "DescripcionDeLaMoneda"
            gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkTotalizarPorVendedor", valTotalizarPorVendedor
            If valUsarMonedaDelReporteBs Then
               gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkMostrarMoneda", False
            Else
               gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkMostrarMoneda", True
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambioOriginalODelDia", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambioOriginalODelDia", Trim(gEnumReport.getMensajesDeMonedaParaInformes(valTipoDeMensajeDeCambio))
         If valDocumentosCobrados Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Else
            gUtilMargins.sAsignarMargenesGenerales refRpt
         End If
         If valMostrarDiasTranscurridos Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiasnTransc", "", "DiasTranscurridos"
         End If
         exito = True
      End If
   End If
   fConfigurarDatosDeComisionDeVendedorDiasVencidos = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDeComisionDeVendedorDiasVencidos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCalculaMontoComisionable(ByVal valMontoIva As Currency, ByVal valMontoGravado As Currency, ByVal valMontoExento As Currency, ByVal valMontoAbonado As Currency) As Currency
   Dim Factor As Currency
   Dim relacion As Currency
   Dim Divisor As Currency
   On Error GoTo h_ERROR
   Divisor = valMontoIva
   Divisor = Divisor + valMontoGravado
   Divisor = Divisor + valMontoExento
   relacion = valMontoIva / Divisor
   Factor = 1 - relacion
   fCalculaMontoComisionable = valMontoAbonado * Factor
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fCalculaMontoComisionable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarComparativoCobranzaPorAnos(ByRef refRptComparativoCobranzaPorAnos As DDActiveReports2.ActiveReport, _
                  ByVal valSQL As String, ByVal valAnoInicial As Integer, ByVal valAnoFinal As Integer, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim nombreFileXML As String
   Dim ValAno As Integer
   Dim i As Integer
   Dim j As Integer
   Dim labelAno As String
   On Error GoTo h_ERROR
   nombreFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMPARATIVO_COBRANZA_POR_ANOS)
   If gUtilReports.fLoadLayout(refRptComparativoCobranzaPorAnos, nombreFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRptComparativoCobranzaPorAnos, "dcOrigenData", valSQL) Then
         Call refRptComparativoCobranzaPorAnos.AddNamedItem("gEnumReport", gEnumReport)
         Call refRptComparativoCobranzaPorAnos.AddNamedItem("gUtilReports", gUtilReports)
         gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno1", ""
         gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno2", ""
         gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno3", ""
         gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno4", ""
         gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno5", ""
         gUtilReports.sConfiguraEncabezado refRptComparativoCobranzaPorAnos, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelInforme", _
            "LblNumeroDePagina", "lblAñoInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblNombreDeLaCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAñoInicialYFinal", "Desde el " & valAnoInicial & " Hasta " & valAnoFinal
         gUtilReports.sDefaultConfigurationForStrFields refRptComparativoCobranzaPorAnos, "txtDetailMes", "", "Mes      "
         gUtilReports.sDefaultConfigurationForStrFields refRptComparativoCobranzaPorAnos, "txtMes", "", "Mes"
         gUtilReports.sDefaultConfigurationForStrFields refRptComparativoCobranzaPorAnos, "txtMoneda", "", "MonedaDoc"
         j = 1
         For i = valAnoInicial To valAnoFinal
            If j = 1 Then
               gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno1", "Año" & i
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtDetailAno1", "", "TotalCobradoAno0"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtAno1", "", "TotalCobradoAno0"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtTotalAno1", "", "TotalCobradoAno0"
            ElseIf j = 2 Then
               gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno2", "Año" & i
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtDetailAno2", "", "TotalCobradoAno1"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtAno2", "", "TotalCobradoAno1"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtTotalAno2", "", "TotalCobradoAno1"
            ElseIf j = 3 Then
               gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno3", "Año" & i
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtDetailAno3", "", "TotalCobradoAno2"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtAno3", "", "TotalCobradoAno2"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtTotalAno3", "", "TotalCobradoAno2"
            ElseIf j = 4 Then
               gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno4", "Año" & i
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtDetailAno4", "", "TotalCobradoAno3"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtAno4", "", "TotalCobradoAno3"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtTotalAno4", "", "TotalCobradoAno3"
            ElseIf j = 5 Then
               gUtilReports.sDefaultConfigurationForLabels refRptComparativoCobranzaPorAnos, "lblAno5", "Año" & i
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtDetailAno5", "", "TotalCobradoAno4"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtAno5", "", "TotalCobradoAno4"
               gUtilReports.sDefaultConfigurationForNumericFields refRptComparativoCobranzaPorAnos, "txtTotalAno5", "", "TotalCobradoAno4"
            End If
            j = j + 1
          Next
         refRptComparativoCobranzaPorAnos.Sections("GroupHeader2").DataField = "Mes"
         refRptComparativoCobranzaPorAnos.Sections("GroupHeader2").GrpKeepTogether = ddGrpAll
         refRptComparativoCobranzaPorAnos.Sections("GroupHeader2").KeepTogether = True
         refRptComparativoCobranzaPorAnos.Sections("GroupHeader2").Repeat = ddRepeatOnPage
         gUtilMargins.sAsignarMargenesGenerales refRptComparativoCobranzaPorAnos
         fConfigurarComparativoCobranzaPorAnos = True
      Else
         fConfigurarComparativoCobranzaPorAnos = False
      End If
   Else
      fConfigurarComparativoCobranzaPorAnos = False
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
  "fConfigurarComparativoCobranzaPorAnos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelRpxDeComprobanteDeCobranza(ByVal NombrePlantillaCompobanteCobranza As String) As String
   Dim nombreDelFile As String
   On Error GoTo h_ERROR
   nombreDelFile = ""
   nombreDelFile = NombrePlantillaCompobanteCobranza
   If nombreDelFile = "" Then
      nombreDelFile = "rpxComprobanteDeCobro"
   End If
h_EXIT:
   fNombreDelRpxDeComprobanteDeCobranza = nombreDelFile
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreDelRpxDeComprobanteDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCobranzasConRetencionIVAPendientePorDistribuir(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COBRANZAS_CON_RETENCION_IVA_PENDIENTE_POR_DISTRIBUIR)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDoc", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCobrado", "", "TotalCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRetencionIVA", "", "RetencionIVA"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCobranzasConRetencionIVAPendientePorDistribuir = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCobranzasConRetencionIVAPendientePorDistribuir", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCobranzaXDia(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valUsarCambioOriginal As Boolean, ByVal NombreCompaniaParaInformes As String, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   mRepUsarCambioOriginal = valUsarCambioOriginal
   rTotalSunmaComision = 0
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COBRANZAS_POR_DIA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Cobranza por Dia"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         If valUsarCambioOriginal Then
             gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
         End If
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtTotalFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFacturado", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoFacturado", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDelReporte", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "MonedaDelReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTotalMoneda", "", "MonedaDelReporte"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSimboloMonedaOriginal", "", "MonedaDeCxC"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSimboloMonedaOriginal", "", "MonedaDeCxC"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtNumerosDeDocumentoCobrados", "", "NumeroDeCobranzas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalNumerosDeDocumentoCobrados", "", "NumeroDeCobranzas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMayorDocumentoCobrado", "", "MaximoDocCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMayorDocumentoCobrado", "", "MaximoDocCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalNumeroDocCobrados", "", "NumeroDeCobranzas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCambio", "", "Cambio"
         If valMostrarCobranzasPorVentasDiferidas Then
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMostrarFacturasDebitoFiscalDiferido", True
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("insRptCobranzaConfigurar", Me)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteCobranzaXDia = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fConfigurarDatosDelReporteCobranzaXDia", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCobranzasEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valUsarCambioOriginal As Boolean, ByVal valAgrupadoPorCobrador As Boolean, _
                  ByVal valEfectuaCambio As String, ByVal NombreCompaniaParaInformes As String, ByVal UsaModuloDeContabilidad As Boolean, _
                  ByVal valOpcionInforme As Integer, ByVal valAgruparInforme As Boolean, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim mensajeDeMoneda As String
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   If valAgruparInforme Then
      nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COBRANZAS_ENTRE_FECHAS_AGRUPADO)
   Else
      nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COBRANZAS_ENTRE_FECHAS)
   End If
   NombreDelReporte = fNombreDelReporteDeCobranzas(CM_RPT_COBRANZAS_ENTRE_FECHAS, valOpcionInforme)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL, , 150) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", mensajeDeMoneda
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCobranza", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCobrador", "", "CodigoCobrador"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCobrador", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDeCobro", "", "MonedaCobro"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusAnulada", "", "Status"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCobrado", "", "TotalCobrado"
         Select Case valOpcionInforme
            Case enum_OpcionInfCobranzaEntreFecha.eOC_Cobrador
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtvar", "", "NombreVendedor"
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblvar", "Cobrador"
            Case enum_OpcionInfCobranzaEntreFecha.eOC_Cliente
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtvar", "", "CodigoCliente"
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblvar", "Codigo Cliente"
            Case enum_OpcionInfCobranzaEntreFecha.eOC_Banco
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtvar", "", "NombreCuenta"
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblvar", "Cuenta Bancaria"
         End Select
         If Not UsaModuloDeContabilidad Then
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If
         If valEfectuaCambio = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal) Then
            If valUsarCambioOriginal Then
               mensajeDeMoneda = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMonedaDeCobro", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaDelDocumento", False
            Else
               mensajeDeMoneda = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
         End If
         If valAgruparInforme Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaCobro", ddGrpAll, ddRepeatAll, True, ddNPNone
            Select Case valOpcionInforme
               Case enum_OpcionInfCobranzaEntreFecha.eOC_Cobrador
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoFiltro", "", "NombreVendedor"
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampo", "Cobrador:"
                  gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCampoFiltro", "NombreVendedor", ddGrpAll, ddRepeatAll, True, ddNPBefore
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtvar", "", "NombreCuenta"
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblvar", "Cuenta Bancaria"
               Case enum_OpcionInfCobranzaEntreFecha.eOC_Cliente
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoFiltro", "", "NombreCliente"
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampo", "Cliente:"
                  gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCampoFiltro", "NombreCliente", ddGrpAll, ddRepeatAll, True, ddNPBefore
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtvar", "", "NombreVendedor"
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblvar", "Cobrador"
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreCliente", "Cuenta Bancaria"
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCuenta"
               Case enum_OpcionInfCobranzaEntreFecha.eOC_Banco
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoFiltro", "", "NombreCuenta"
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampo", "Cuenta:"
                  gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCampoFiltro", "NombreCuenta", ddGrpAll, ddRepeatAll, True, ddNPBefore
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtvar", "", "NombreVendedor"
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblvar", "Cobrador"
            End Select
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalCobradoCampoFiltro", "TotalCobrado", eSF_SUM, "GHCampoFiltro", eSR_GROUP, eST_SUB_TOTAL
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaCobro", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         End If
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotalCobrado", "TotalCobrado", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         If valMostrarCobranzasPorVentasDiferidas Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMostrarFacturasDebitoFiscalDiferido", True
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCobranzasEntreFechas = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCobranzasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCobranzasPorVendedor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valUsarCambioOriginal As Boolean, _
                  ByVal valIncluirDocs As Boolean, valEfectuaCambio As String, ByVal NombreCompaniaParaInformes As String, _
                  ByVal UsaModuloDeContabilidad As Boolean, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim mensajeDeMoneda As String
   On Error GoTo h_ERROR
   exito = False
   If valIncluirDocs Then
      nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COBRANZAS_POR_VENDEDOR_DETALLADO)
      NombreDelReporte = fNombreDelReporteDeCobranzas(CM_RPT_COBRANZAS_POR_VENDEDOR_DETALLADO)
   Else
      nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COBRANZAS_POR_VENDEDOR)
      NombreDelReporte = fNombreDelReporteDeCobranzas(CM_RPT_COBRANZAS_POR_VENDEDOR)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambio", False
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSimboloMoneda", False
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambioABolivares", False
         If valEfectuaCambio = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambio", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSimboloMoneda", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambioABolivares", True
               If valUsarCambioOriginal Then
                  mensajeDeMoneda = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSimboloMoneda", " ", "SimboloMonedaCobro"
                  gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambioABolivares", gUtilReports.fNumericFormat(1), "CambioABolivares"
               Else
                  mensajeDeMoneda = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
                  gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSimboloMoneda", " ", "SimboloMonedaCobro"
                  gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambioABolivares", gUtilReports.fNumericFormat(1), "CambioABolivares"
               End If
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaCobro", ddGrpAll, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCobranzas", "MtoAbonado", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sChangePositionAndSize refRpt, "lblNumeroDelDocumentoCobrado", 2340, 225, 1890, 225
            gUtilReports.sChangePositionAndSize refRpt, "txtNumeroDelDocumentoCobrado", 2340, 0, 1890, 225
         End If
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", mensajeDeMoneda
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCobrador", "", "CodigoCobrador"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaCobro", "", "MonedaCobro"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCobranza", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCobrado", "", "MtoTotalCobrado"
         If UsaModuloDeContabilidad Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDelDocumentoCobrado", "", "NumeroDelDocumentoCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginalRestanteAlDiaDelC", "", "MtoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonado", "", "MtoAbonado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMoneda", "", "MonedaCobro"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCobranzas", "", ""
         If valMostrarCobranzasPorVentasDiferidas Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMostrarFacturasDebitoFiscalDiferido", True
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gUltimaTasaDeCambio", gUltimaTasaDeCambio)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCobranzasPorVendedor = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCobranzasPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSubReportAnticipos(ByVal valSqlDelReporte As String) As DDActiveReports2.ActiveReport
   Dim rptSubReport As DDActiveReports2.ActiveReport
   Dim nombreDelFileXML As String
   Dim SqlContable As String
   On Error GoTo h_ERROR
   Set rptSubReport = New DDActiveReports2.ActiveReport
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMPROBANTE_ANTICIPOS_EN_COBRANZA)
   If gUtilReports.fLoadLayout(rptSubReport, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(rptSubReport, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForIntegerFields rptSubReport, "txtConsecutivoAnticipo", "", "ConsecutivoAnticipoUsado"
         gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtNumeroAnticipo", "", "NumeroAnticipo"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoRestanteAlDia", "", "MontoRestanteAlDia"
         gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAplicado", "", "MontoAplicado"
         rptSubReport.PageSettings.LeftMargin = 0
         rptSubReport.PageSettings.TopMargin = 0
         rptSubReport.PageSettings.RightMargin = 0
         rptSubReport.PageSettings.BottomMargin = 0
         Call rptSubReport.AddNamedItem("gUtilReports", gUtilReports)
      End If
   End If
   Set GetSubReportAnticipos = rptSubReport
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GetSubReportAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfigurarDatosDeComisionDeVentasPorVendedorYPorAriculo(ByRef refRpt As DDActiveReports2.ActiveReport, _
               ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
               ByVal valComisionesXventas As Boolean, ByVal valUsarMonedaDelReporteBs As Boolean, _
               ByVal valTotalizarPorVendedor As Boolean, ByVal valTipoDeMensajeDeCambio As enum_MensajeMonedaParaInformes, _
               ByVal valCobradosYCobranzas As Boolean, valDocumentosCobrados As Boolean, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   rTotalSunmaComision = 0
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CN_RPT_COMISION_DE_VENTAS_POR_VENDEDOR_Y_ARTICULO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Comisión de Vendedores Por Ventas de Articulos(Detallado)"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaFactura", "", "MonedaDeFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtColumnaParaGrupoTipoReporte", "", "ColumnaParaGrupoTipoReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLabelTipoGrupo", "", "ColumnaParaGrupoTipoReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "FechaDeFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroFactura", "", "NumeroDeFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreDelCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDocumento", "", "PrecioConIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoDocumento", "", "PrecioConIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionable", "", "PrecioSinIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionable", "", "PrecioSinIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComisionableVendedor", "", "PrecioSinIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComisionableMoneda", "", "PrecioSinIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDeComision", "", "PorcentajeComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad"
         
            gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkTotalizarPorVendedor", valTotalizarPorVendedor
            If valUsarMonedaDelReporteBs Then
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaCobranza", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMonedaCobranza", False
            Else
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaCobranza", True
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMonedaCobranza", True
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambioOriginalODelDia", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambioOriginalODelDia", Trim(gEnumReport.getMensajesDeMonedaParaInformes(valTipoDeMensajeDeCambio))
         gUtilMargins.sAsignarMargenesGenerales refRpt
         exito = True
      End If
   End If
   fConfigurarDatosDeComisionDeVentasPorVendedorYPorAriculo = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDeComisionDeVentasPorVendedorYPorAriculo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
  
Private Sub sConfigurarDatosDeComisionDeVendedorCobranzas(ByRef refRpt As DDActiveReports2.ActiveReport, _
               ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
               ByVal valUsarMonedaDelReporteBs As Boolean, _
               ByVal valTotalizarPorVendedor As Boolean, ByVal NombreCompaniaParaInformes As String)
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO_COBRANZAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "SimboloDeLaMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTasaDeCambio", "", "cambioABolivares"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblReporteDeComisionesPorVendedor", "Comisión de Vendedores por Cobranzas(Resumido)"
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkEsComisionsPorCobranza", True
         rReporteResumen = 1
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblReporteDeComisionesPorVendedor", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelVendedor", "", "CodigoDelVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreDelVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroFactura", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaFactura", "", "FechaFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumentoCxC", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIva", "", "MontoIvaDeCxC"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "FechaDelDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDelDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaCobranza", "", "DescripcionDeLaMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDelDocumento", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumento", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDocumentoMoneda", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralMontoComisionable", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDeComision", "", "PorcentajeDeComision" '*****
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionableDelDocumento", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionable", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralComision", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralComisiones", "", "MontoComision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionableBs", "", "MontoComisionBs"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralMontoComisionable", "", "MontoComision"
            
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSunmaComision", "", rTotalSunmaComision
           
            gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkTotalizarPorVendedor", valTotalizarPorVendedor
            If valUsarMonedaDelReporteBs Then
               gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkMostrarMoneda", False
            Else
               gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkMostrarMoneda", True
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambioOriginalODelDia", True
         gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHEncabezado", False
         gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", False
         gUtilMargins.sAsignarMargenesGenerales refRpt
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "sConfigurarDatosDeComisionDeVendedorCobranzas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fConfiguraElInformeDeComisionDeAgentes(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_AGENTES)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCobranza", "", "FechaCobranza", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCobranza", "", "NumeroCobranza"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCobrado", "", "MontoCobranza"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaFactura", "", "FechaFactura", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroFactura", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFacturado", "", "MontoFacturado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreLineaProducto", "", "NombreLineaProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglon", "", "TotalRenglon"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoVendedor", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCobranza", "NumeroCobranza", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHFactura", "NumeroFactura", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHLineaProducto", "NombreLineaProducto", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilScript", gUtilScript)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeComisionDeAgentes = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeComisionDeAgentes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeComisionDeAgentesXLineaDeProducto(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_AGENTES_X_LINEA_DE_PRODUCTO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCobranza", "", "FechaCobranza", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCobranza", "", "NumeroCobranza"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCobrado", "", "MontoCobranza"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaFactura", "", "FechaFactura", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroFactura", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreLineaProducto", "", "NombreLineaProducto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoXLinea", "", "MontoXLinea"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeLinea", "", "PorcentajeLinea"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonado", "", "montoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionable", "", "MontoComisionable"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoVendedor", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPAfter
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCobranza", "NumeroCobranza", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHFactura", "NumeroFactura", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHLineaProducto", "NombreLineaProducto", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeCobranzas(CM_RPT_COMISION_DE_AGENTES_X_LINEA_DE_PRODUCTO)
         Call refRpt.AddNamedItem("gUtilScript", gUtilScript)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeComisionDeAgentesXLineaDeProducto = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeComisionDeAgentesXLineaDeProducto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDesgloseDeCobranza(ByRef refReporte As DDActiveReports2.ActiveReport, _
                     ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valAgrupadoPorCliente As Boolean, ByVal NombreCompaniaParaInformes As String, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valAgrupadoPorCliente Then
      nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_DESGLOSE_DE_COBRANZA_X_CLIENTE)
   Else
      nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_DESGLOSE_DE_COBRANZA_X_DIA)
   End If
   If gUtilReports.fLoadLayout(refReporte, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refReporte, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblNombreCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblLineaAdicionalDeEncabezado", "Del " & gConvert.fechaAString(valFechaInicial) & " al " & gConvert.fechaAString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refReporte, "lblNombreCompania", "lblFechaYHoraDeEmision", _
                        "lblNombreDelReporte", "lblNumeroDePagina", "lblLineaAdicionalDeEncabezado", _
                        gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumero", "", "Numero"
              gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtRetISLR", "", "RetencionISLR"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtEfectivo", "", "CobradoEfectivo"
            gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtCheque", "", "CobradoCheque"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumCheque", "", "NumerodelCheque"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtCobTarjeta", "", "CobradoTarjeta"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumTarjeta", "", "NroDeLaTarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalCobrado", "", "TotalCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtRetencionIVA", "", "MontoIvaRetenido"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNroComprobanteRetIVA", "", "NumeroComprobanteRetIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtOtros", "", "TotalOtros"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtBanco", "", "NombreBanco"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtTipoTarjeta", "", "TipoTarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalEfectivo", "", "CobradoEfectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalCheque", "", "CobradoCheque"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalTarjeta", "", "CobradoTarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalOtros", "", "TotalOtros"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalRetIVA", "", "MontoIvaRetenido"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalRetSL", "", "RetencionISLR"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalCobrar", "", "TotalCobrado"
         If valAgrupadoPorCliente Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refReporte, "GHCliente", "Nombre", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNombre", "", "Nombre"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refReporte, "GHMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPBefore
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtMonedaDoc", "", "Moneda"
            gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtFecha", "", "Fecha", eFT_DATE
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refReporte, "GHFecha", "Fecha", ddGrpAll, ddRepeatAll, False, ddNPBefore
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtFecha", "", "Fecha"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtMonedaDoc", "", "Moneda"
            gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtNombreCliente", "", "Nombre", eFT_DATE
         End If
         If valMostrarCobranzasPorVentasDiferidas Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refReporte, "lblMostrarFacturasDebitoFiscalDiferido", True
         End If
         gUtilMargins.sAsignarMargenesGenerales refReporte, ddOLandscape
         Call refReporte.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraElInformeDesgloseDeCobranza = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDesgloseDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeCobranzasEntreFechas(ByVal valOpcion As Integer) As String
   On Error GoTo h_ERROR
   Select Case valOpcion
      Case enum_OpcionInfCobranzaEntreFecha.eOC_Cobrador
         fNombreDelReporteDeCobranzasEntreFechas = "Cobranzas entre Fechas - Cobrador"
      Case enum_OpcionInfCobranzaEntreFecha.eOC_Cliente
         fNombreDelReporteDeCobranzasEntreFechas = "Cobranzas entre Fechas - Cliente"
      Case enum_OpcionInfCobranzaEntreFecha.eOC_Banco
         fNombreDelReporteDeCobranzasEntreFechas = "Cobranzas entre Fechas - Cuenta Bancaria"
      Case Else
         fNombreDelReporteDeCobranzasEntreFechas = "Cobranzas entre Fechas"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreDelReporteDeCobranzasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fConfiguraElInformeDeCobranzasPorZona(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                  ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim mensajeDeMoneda As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COBRANZAS_POR_ZONA)
   NombreDelReporte = fNombreDelReporteDeCobranzas(CM_RPT_COBRANZAS_POR_ZONA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtZona", "", "NombreZona"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCobrado", "", "TotalCobrado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtVendedor", "", "NombreCobrador"
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalPorCobrador", "TotalCobrado", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCobranzas", "TotalCobrado", eSF_SUM, "GHZona", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalPorMoneda", "TotalCobrado", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "CodigoMoneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPAfter
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHZona", "NombreZona", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoCobrador", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         gUtilMargins.sAsignarMargenesGenerales refRpt
         
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCobranzasPorZona = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCobranzasPorZona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sConfigurarDatosDeComisionDeVendedorPorArticuloResumido(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
               ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valComisionesXventas As Boolean, ByVal NombreCompaniaParaInformes As String)
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   rTotalSunmaComision = 0
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COMISION_DE_VENDEDOR_RESUMIDO_POR_ARTICULO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte, , 90) Then
         rReporteResumen = 0
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Comisión de Vendedores Por Ventas(Resumido)"
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPorcentajeComision", "", "PorcentajeComision"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCantidad", "", "Cantidad"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMontoDocumentos", "", "PrecioConIva"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMontoComisionable", "", "PrecioSinIva"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtComision", "", "Comision"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoDocumentos", "PrecioConIVA", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoComisionable", "PrecioSinIVA", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalComision", "Comision", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoVendedor", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
           
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "sConfigurarDatosDeComisionDeVendedorPorArticuloResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fConfigurarDatosDelReporteCobranzaRentencionIvaFormal(ByRef refRpt As DDActiveReports2.ActiveReport, _
                     ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal NombreCompaniaParaInformes As String, ByVal OrdenarPorFechaComprobante As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_COBRANZA_RENTENCION_IVA_FORMAL)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
        gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", fNombreDelReporteDeCobranzas(CM_RPT_COBRANZA_RENTENCION_IVA_FORMAL)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteIva", "", "NumeroComprobanteRetIva"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaRetencion", "", "Fecha de Retencion", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "Numero del Documento Cobrado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroControl", "", "NumeroControl"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaDocumento", "", "fecha del Documento", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRifCliente", "", "NumeroRif"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravable", "", "Monto Gravable"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSinIva", "", "MontoSinIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIva", "", "MontoIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIvaRetenido", "", "MontoIvaRetenido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalFactura", "", "Total Factura"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtGranTotalFactura", "Total Factura", eSF_SUM, "GH", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoGravable", "Monto Gravable", eSF_SUM, "GH", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIva", "MontoIva", eSF_SUM, "GH", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIvaRetenido", "MontoIvaRetenido", eSF_SUM, "GH", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoSinIva", "MontoSinIva", eSF_SUM, "GH", eSR_ALL, eST_SUB_TOTAL
         If (OrdenarPorFechaComprobante) Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblOrdenadoPorFechaComprobante", True
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblOrdenadoPorFechaCobranza", True
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeCobranzas(CM_RPT_COBRANZA_RENTENCION_IVA_FORMAL)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteCobranzaRentencionIvaFormal = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteCobranzaRentencionIvaFormal = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCobranzaRentencionIvaFormal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfigurarDatosDeComisionDeCobranzaPorVendedorYPorAriculo(ByRef refRpt As DDActiveReports2.ActiveReport, _
               ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
               ByVal valComisionesXventas As Boolean, ByVal valUsarMonedaDelReporteBs As Boolean, _
               ByVal valTotalizarPorVendedor As Boolean, ByVal valTipoDeMensajeDeCambio As enum_MensajeMonedaParaInformes, _
               ByVal valCobradosYCobranzas As Boolean, valDocumentosCobrados As Boolean, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   rTotalSunmaComision = 0
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CN_RPT_COMISION_DE_COBRANZA_POR_VENDEDOR_Y_ARTICULO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Comisión de Vendedores Por Cobranza de Articulos(Detallado)"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaFactura", "", "MonedaDeFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelVendedor", "", "codigovendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLabelTipoGrupo", "", "Articulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "fechafactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroFactura", "", "numerofactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "nombredelcliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDocumento", "", "totalfactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoDocumento", "", "totalfactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionable", "", "comision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoComisionable", "", "PrecioSinIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComisionableVendedor", "", "PrecioSinIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComisionableMoneda", "", "PrecioSinIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDeComision", "", "porcentajecomision"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "cantidad"
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkTotalizarPorVendedor", valTotalizarPorVendedor
         If valUsarMonedaDelReporteBs Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaCobranza", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMonedaCobranza", False
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaCobranza", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMonedaCobranza", True
         End If
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambioOriginalODelDia", True
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambioOriginalODelDia", Trim(gEnumReport.getMensajesDeMonedaParaInformes(valTipoDeMensajeDeCambio))
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoDocumento", "totalfactura", eSF_SUM, "GFArticulo", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoComisionable", "comision", eSF_SUM, "GFArticulo", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalComision", "comision", eSF_SUM, "GFArticulo", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalComisionableVendedor", "comision", eSF_SUM, "GFVendedor", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalComisionVendedor", "comision", eSF_SUM, "GFVendedor", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalComisionMoneda", "comision", eSF_SUM, "GFMoneda", eSR_ALL, eST_SUB_TOTAL


         gUtilMargins.sAsignarMargenesGenerales refRpt
         exito = True
      End If
   End If
   fConfigurarDatosDeComisionDeCobranzaPorVendedorYPorAriculo = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDeComisionDeCobranzaPorVendedorYPorAriculo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sConfigurarDatosDeComisionDeVendedorPorCobranzaPorArticuloResumido(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
               ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valComisionesXventas As Boolean, ByVal NombreCompaniaParaInformes As String)
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   rTotalSunmaComision = 0
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CN_RPT_COMISION_DE_COBRANZA_POR_VENDEDOR_Y_ARTICULO_RESUMIDO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte, , 90) Then
         rReporteResumen = 0
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Comisión de Vendedores Por Cobranza Por Articulo(Resumido)"
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "nombrevendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "codigovendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "codigoarticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "descripcionarticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "cantidad"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDocumentos", "", "totalfactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComisionable", "", "totasiniva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtComision", "", "comision"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoDocumentos", "PrecioConIVA", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoComisionable", "PrecioSinIVA", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalComision", "Comision", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoVendedor", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
           
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "sConfigurarDatosDeComisionDeVendedorPorCobranzaPorArticuloResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

'MOISES
Public Function fConfiguraElInformeDeRetencionISLR(ByRef refReporte As DDActiveReports2.ActiveReport, _
                     ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDelRptDeInformesCobranza(CM_RPT_INFORME_DE_RETENCION_ISLR)
   If gUtilReports.fLoadLayout(refReporte, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refReporte, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblNombreDelReporte", fNombreDelReporteDeCobranzas(CM_RPT_INFORME_DE_RETENCION_ISLR)
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblNombreCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblFechaYHoraDeEmision", "Emitido el " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refReporte, "lblLineaAdicionalDeEncabezado", "Del " & gConvert.fechaAString(valFechaInicial) & " al " & gConvert.fechaAString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refReporte, "lblNombreCompania", "lblFechaYHoraDeEmision", _
                        "lblNombreDelReporte", "lblNumeroDePagina", "lblLineaAdicionalDeEncabezado", _
                        gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refReporte, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroDocCobrado", "", "NumeroDelDocumentoCobrado"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtTotalCobrado", "", "TotalCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refReporte, "txtRetencionISLR", "", "RetencionISLR"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNroComprobanteISLR", "", "NumeroDeComprobanteISLR"
         gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtRIF", "", "NumeroRIF"
         gUtilMargins.sAsignarMargenesGenerales refReporte
         Call refReporte.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraElInformeDeRetencionISLR = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeRetencionISLR", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

