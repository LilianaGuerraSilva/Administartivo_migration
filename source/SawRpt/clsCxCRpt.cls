VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxCRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCxCRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte CxC"

Private Enum eCxCRpt
   eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_RESUMIDO = 0
   eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO
   eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR
   eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS
   eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR_POR_CLIENTE
   eCxCRpt_FACTURAS_ANULADAS_CON_CXC_VIGENTES
   eCxCRpt_CUENTAS_X_COBRAR_CON_DESCRIPCION
   eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_MONEDA_ORIGINAL
   eCxCRpt_ANALISIS_CxC_HISTORICO
   eCxCRpt_HISTORICO_DE_CLIENTE
   eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS
   eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_VENDEDOR
   eCxCRpt_COMPROBANTES_DE_CUENTA_POR_COBRAR
   eCxCRpt_VENDEDOR_XLIENA_DE_PRODUCTO
   eCxCRpt_LISTADO_DE_VENDEDORES
   eCxCRpt_LISTADO_DE_CLIENTES_POR_VENDEDOR
   eCxCRpt_LISTADO_DE_FACTURAS_SIN_CXC
   eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO_AGRUPADO
   eCxCRpt_LISTADO_DE_CHEQUES_DEVUELTOS
   eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO
   eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO_MONEDA_ORIGINAL
   eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA
   eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA_MONEDA_ORIGINAL
   eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA
   eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_VENDEDOR_POR_ZONA_DE_COBRANZA
  End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporteDeCxC(ByVal valReporte As eCxCRpt) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_RESUMIDO: nombreFileXML = "rpxAnalisisDeVencimientoDeCxCResumido"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO: nombreFileXML = "rpxAnalisisDeVencimientoDeCxCDetallado"
      Case eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR: nombreFileXML = "rpxCxCxVendedor"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS: nombreFileXML = "rpxCxCEntreFechas"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_MONEDA_ORIGINAL: nombreFileXML = "rpxCxCEntreFechasMonedaOrig"
      Case eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR_POR_CLIENTE: nombreFileXML = "rpxCxCxVendedorPorCliente"
      Case eCxCRpt_FACTURAS_ANULADAS_CON_CXC_VIGENTES: nombreFileXML = "rpxFacturasAnuladasVsCxCVigentes"
      Case eCxCRpt_CUENTAS_X_COBRAR_CON_DESCRIPCION: nombreFileXML = "rpxCxCConDescripcion"
      Case eCxCRpt_ANALISIS_CxC_HISTORICO: nombreFileXML = "rpxAnalisisDeCxCHistorico"
      Case eCxCRpt_HISTORICO_DE_CLIENTE: nombreFileXML = "rpxHistoricoDeClientes"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS: nombreFileXML = "rpxAnalisisDeVencimientoDeCxCEntreFechas"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_VENDEDOR: nombreFileXML = "rpxAnalisisDeVencimientoDeCxCEntreFechasPorVendedor"
      Case eCxCRpt_COMPROBANTES_DE_CUENTA_POR_COBRAR: nombreFileXML = "rpxComprobanteDeCxC"
      Case eCxCRpt_VENDEDOR_XLIENA_DE_PRODUCTO: nombreFileXML = "rpxVendedorXLienaDeProducto"
      Case eCxCRpt_LISTADO_DE_VENDEDORES: nombreFileXML = "rpxListadoDeVendedores"
      Case eCxCRpt_LISTADO_DE_CLIENTES_POR_VENDEDOR: nombreFileXML = "rpxListadodeClientesPorVendedor"
      Case eCxCRpt_LISTADO_DE_FACTURAS_SIN_CXC: nombreFileXML = "rpxListadoDeFacturasSinCxC"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO_AGRUPADO: nombreFileXML = "rpxAnalisisDeVencimientoDeCxCDetalladoAgrupado"
      Case eCxCRpt_LISTADO_DE_CHEQUES_DEVUELTOS: nombreFileXML = "rpxChequesDevueltos"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO: nombreFileXML = "rpxCxCEntreFechasPorSectorDeNegocio"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO_MONEDA_ORIGINAL: nombreFileXML = "rpxCxCEntreFechasPorSectorDeNegocioMonedaOrig"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA: nombreFileXML = "rpxCxCEntreFechasPorZonaDeCobranza"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA_MONEDA_ORIGINAL: nombreFileXML = "rpxCxCEntreFechasPorZonaDeCobranzaMonedaOrig"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA: nombreFileXML = "rpxAnalisisDeVencimientoDeCxCEntreFechasPorZonaDeCobranza"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_VENDEDOR_POR_ZONA_DE_COBRANZA:     nombreFileXML = "rpxAnalisisDeVencimientoDeCxCEntreFechasPorVendedorPorZonaDeCobranza"
      Case Else: nombreFileXML = ""
   End Select
  
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeCxC = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeCxC(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeCxC = ""
   End If
  
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDeImpresionDelReporteDeArticuloDeInventario", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeCxC(ByVal valReporte As eCxCRpt) As String
   Select Case valReporte
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_RESUMIDO: fNombreDelReporteDeCxC = "Análisis de Vencimiento de CxC - Resumido"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO: fNombreDelReporteDeCxC = "Análisis de Vencimiento de CxC - Detallado"
      Case eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR: fNombreDelReporteDeCxC = "Cuentas por Cobrar por Vendedor"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS: fNombreDelReporteDeCxC = "Cuentas por Cobrar entre Fechas"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_MONEDA_ORIGINAL: fNombreDelReporteDeCxC = "Cuentas por Cobrar entre Fechas - Moneda Original"
      Case eCxCRpt_FACTURAS_ANULADAS_CON_CXC_VIGENTES: fNombreDelReporteDeCxC = "Facturas Anuladas vs. Cuentas por Cobrar Vigentes"
      Case eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR_POR_CLIENTE: fNombreDelReporteDeCxC = "Cuentas por Cobrar por Vendedor por Cliente"
      Case eCxCRpt_CUENTAS_X_COBRAR_CON_DESCRIPCION: fNombreDelReporteDeCxC = "Cuentas por Cobrar con Descripción"
      Case eCxCRpt_ANALISIS_CxC_HISTORICO: fNombreDelReporteDeCxC = "Análisis de Cuentas por Cobrar Histórico"
      Case eCxCRpt_HISTORICO_DE_CLIENTE: fNombreDelReporteDeCxC = "Histórico de Cliente"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS: fNombreDelReporteDeCxC = "Análisis de Vencimiento de CxC entre Fechas"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_VENDEDOR: fNombreDelReporteDeCxC = "Análisis de Vencimiento de CxC entre Fechas - por Vendedor"
      Case eCxCRpt_COMPROBANTES_DE_CUENTA_POR_COBRAR: fNombreDelReporteDeCxC = "Comprobante de Cuenta por Cobrar"
      Case eCxCRpt_VENDEDOR_XLIENA_DE_PRODUCTO: fNombreDelReporteDeCxC = "Listado de Vendedores por Línea"
      Case eCxCRpt_LISTADO_DE_VENDEDORES: fNombreDelReporteDeCxC = "Listado de Vendedores"
      Case eCxCRpt_LISTADO_DE_CLIENTES_POR_VENDEDOR: fNombreDelReporteDeCxC = "Listado de Clientes Por Vendedor"
      Case eCxCRpt_LISTADO_DE_FACTURAS_SIN_CXC: fNombreDelReporteDeCxC = "Listado de Facturas sin CxC"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO_AGRUPADO: fNombreDelReporteDeCxC = "Análisis de Vencimiento de CxC - Detallado"
      Case eCxCRpt_LISTADO_DE_CHEQUES_DEVUELTOS: fNombreDelReporteDeCxC = "Informe de Cheques Devueltos"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO: fNombreDelReporteDeCxC = "Cuentas por Cobrar entre Fechas - Sector de Negocio"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO_MONEDA_ORIGINAL: fNombreDelReporteDeCxC = "Cuentas por Cobrar entre Fechas - Sector de Negocio - Moneda Original"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA: fNombreDelReporteDeCxC = "Cuentas por Cobrar entre Fechas - Zona de Cobranza"
      Case eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA_MONEDA_ORIGINAL: fNombreDelReporteDeCxC = "Cuentas por Cobrar entre Fechas - Zona de Cobranza - Moneda Original"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA: fNombreDelReporteDeCxC = "Análisis de Vencimiento de CxC entre Fechas Por Zona de Cobranza"
      Case eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_VENDEDOR_POR_ZONA_DE_COBRANZA: fNombreDelReporteDeCxC = "Análisis de Vencimiento de CxC entre Fechas - por Vendedor Por Zona de Cobranza"
      Case Else: fNombreDelReporteDeCxC = ""
   End Select
End Function

Public Function fConfiguraLasSeccionesDelReporteDeCxCEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valEnMonedaOriginal As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCamposDefiniblesFactura As Boolean, ByVal valUsaModuloDeContabilidad As Boolean, ByVal usarContacto As Boolean) As Boolean
   Dim nombreDelFile As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valEnMonedaOriginal Then
      nombreDelFile = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_MONEDA_ORIGINAL)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_MONEDA_ORIGINAL)
   Else
      nombreDelFile = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS)
   End If
   exito = fConfigurarReportesCxCEntreFechas(nombreDelFile, refRpt, valSqlDelReporte, valFechaInicial, valFechaFinal, NombreDelReporte, valNombreCompaniaParaInformes, valUsaCamposDefiniblesFactura, valUsaModuloDeContabilidad, usarContacto)
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelReporteDeCxCEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraLasSeccionesDelReporteDeCxCEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfigurarReportesCxCEntreFechas(ByVal valNombreDelFile As String, ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCamposDefiniblesFactura As Boolean, ByVal valUsaModuloDeContabilidad As Boolean, ByVal usarContacto As Boolean)
   Dim exito As Boolean
   On Error GoTo h_ERROR
   If gUtilReports.fLoadLayout(refRpt, valNombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDelGrupo", "", "MonedaParaGrupo"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDocumento", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "Monto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioAMonedaLocal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "MontoEnMonedaLocal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInformacionAdicional", "", "InformacionAdicional"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoOriginal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", ""
         If (usarContacto) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         End If
         
         If Not valUsaCamposDefiniblesFactura Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblInformacionAdicional", False
         End If
         If Not valUsaModuloDeContabilidad Then
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, , valNombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarReportesCxCEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarReportesCxCEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraFaxDeMudanza(ByRef refReporte As DDActiveReports2.ActiveReport, ByVal valNombre As String) As Boolean
   Dim nombreDelFile As String
   On Error GoTo h_ERROR
   nombreDelFile = "C:\cvsHEAD\vba\Administrativo\Reportes\Informatica\rpxFaxDeMudanza.rpx"
   refReporte.LoadLayout nombreDelFile
   gUtilMargins.sAsignarMargenesGenerales refReporte
   fConfiguraFaxDeMudanza = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraFaxDeMudanza", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteDeCxCxVendedor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valMontosConIva As Boolean, ByVal valTotalizarPorCliente As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String, ByVal valMonedaOriginal As Boolean, ByRef gGlobalization As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraUltimaFecha As Boolean, ByVal usarContacto As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML  As String
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   If valTotalizarPorCliente Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR_POR_CLIENTE)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR_POR_CLIENTE)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_POR_VENDEDOR)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", " del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valMontosConIva Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoConOSinIVA", "Montos Con " & gGlobalization.fPromptIVA
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoConOSinIVA", "Montos Sin " & gGlobalization.fPromptIVA
         End If
            If valMonedaOriginal Then
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", ""
            Else
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", valMensajesDeMonedaParaInformes
            End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxC", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaDoc", "", "FechaDoc", eFT_DATE
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDiasVencidos", "", "DiasVencidos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCxC", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonado", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoRestante", "", "MontoRestante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFCNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCTotalCxC", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMontoAbonado", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMontoRestante", "", ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMMoneda", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMTotalCxC", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMontoAbonado", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMontoRestante", "", ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaUltimoPago", "", "UltimaFechAbono"
         If (usarContacto) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         End If
         
         If (Not valMuestraUltimaFecha) Then
          gUtilReports.sDefaultConfigurationForLabels refRpt, "lblUltimoPago", ""
         End If
         If valTotalizarPorCliente Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Else
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
         End If
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelReporteDeCxCxVendedor = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraLasSeccionesDelReporteDeCxCxVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteDeAnalisisDeVencimiento(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valDetallado As Boolean, ByVal valTituloFecha As String, ByVal valPrimerVencimiento As Long, ByVal valSegundoVencimiento As Long, ByVal valTercerVencimiento As Long, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String, ByVal valMonedaOriginal As Boolean, ByVal valAgruparPorTipo As Boolean, Optional ByVal valCodigoVendedor As String, Optional ByVal valNombreVendedor As String) As Boolean
   Dim exito As Boolean
   Dim nombreFileXML As String
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   If valDetallado Then
      If valAgruparPorTipo Then
         NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO_AGRUPADO)
         nombreFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO_AGRUPADO)
      Else
         NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO)
         nombreFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_DETALLADO)
      End If
   Else
      nombreFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_RESUMIDO)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_CXC_RESUMIDO)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", valTituloFecha
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valMonedaOriginal Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", ""
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", valMensajesDeMonedaParaInformes
         End If
         If valCodigoVendedor <> "" Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblVendedor", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodigoVendedor", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNombreVendedor", True
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", valCodigoVendedor
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", valNombreVendedor
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblVendedor", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodigoVendedor", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNombreVendedor", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "TxtGHTipoDocumento", "", "Tipo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtZonaDeCobranza", "", "ZonaDeCobranza"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango1Vto", "1-" & gConvert.fConvierteAString(valPrimerVencimiento)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango2Vto", gConvert.fConvierteAString(valPrimerVencimiento) + 1 & "-" & gConvert.fConvierteAString(valSegundoVencimiento)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango3Vto", gConvert.fConvierteAString(valSegundoVencimiento) + 1 & "-" & gConvert.fConvierteAString(valTercerVencimiento)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango4Vto", gConvert.fConvierteAString(valTercerVencimiento) + 1 & " +"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxC", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVto", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDiasVecidos", "", "DiasVencidos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCxC", "", "MtoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMontoNoVencido", "", "MtoNoVencido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMto1Vto", "", "Mto1Vto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMto2Vto", "", "Mto2Vto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMto3Vto", "", "Mto3Vto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailMto4Vto", "", "Mto4Vto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDetailTotalCliente", "", "MtoRestante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFCCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFCNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMontoNoVencido", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMto1Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMto2Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMto3Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCMto4Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFCTotalCliente", "", ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFZonaDeCobranza", "", "ZonaDeCobranza"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFZMontoNoVencido", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFZMto1Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFZMto2Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFZMto3Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFZMto4Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFZTotalCliente", "", ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFMMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMontoNoVencido", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto1Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto2Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto3Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMMto4Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFMTotalCliente", "", ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "TxtGFTipoDocumento", "", "Tipo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFTMontoNoVencido", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFTMto1Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFTMto2Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFTMto3Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFTMto4Vto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGFTTotalCliente", "", ""
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumento", "", "Tipo"
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelReporteDeAnalisisDeVencimiento = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraLasSeccionesDelReporteDeAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteDeFacturasAnuladasVsCxCVigentes(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_FACTURAS_ANULADAS_CON_CXC_VIGENTES)
   NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_FACTURAS_ANULADAS_CON_CXC_VIGENTES)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroFactura", "", "NumeroFact"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaFactura", "", "FechaFact", eFT_DATE
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalFactura", "", "TotalFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusFactura", "", "StatusFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxC", "", "NumeroCxC"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCxC", "", "TotalCxC"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCxC", "", "StatusCxC"
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelReporteDeFacturasAnuladasVsCxCVigentes = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraLasSeccionesDelReporteDeFacturasAnuladasVsCxCVigentes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCxCConDescripcion(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal usarContacto As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_CON_DESCRIPCION)
   NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_CON_DESCRIPCION)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "CxC Con Descripción"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", "Nota: Los Montos en Monedas extranjeras son cálculadas a Bolivares tomando en cuenta la última tasa de cambio"
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", ""
         End If
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtZonaCobranza", "", "ZonaCobranza"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaPorGrupo", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtZona", "", "zonaCobranza"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefono", "", "Telefono"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direccion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalLinea", "", "Monto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralMoneda", "", "Monto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralCliente", "", "Monto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneralZona", "", "Monto"
         If (usarContacto) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("insRptCxCConfiguracion", Me)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteCxCConDescripcion = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCxCConDescripcion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteAnalisisDeCxCHistorico(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String, ByVal valCambioABs As Boolean, ByVal gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_ANALISIS_CxC_HISTORICO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaCobroRetencion", "Nota: (*) Aplicación de retención realizada en cobranza"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
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
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocCobrado", "", "TipoDocCobrado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocCobrado", "", "NumeroDocCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonadoDocCobrado", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoTotal", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoActual", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicialMasMontoActual", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicialCxC", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicialAnticipos", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicialCobranzas", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoInicial", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCxC", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalAnticipos", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCobranzas", "0"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalGeneral", "0"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Codigo", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHTipoReporte", "ColumnaParaGrupoTipoReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHDetalle", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteAnalisisDeCxCHistorico = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteAnalisisDeCxCHistorico", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteHistoricoDeCliente(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String, ByVal valCambioABs As Boolean, ByVal valUnaPaginaPorCliente As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_HISTORICO_DE_CLIENTE)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaCobroRetencion", "Nota: (*) Aplicación de retención realizada en cobranza"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
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
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
'         If gGlobalization.fEsCodigoPeru Then
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoGH", "", "TipoDocumentoLey"
'         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoGH", "", "TipoDeDocumento"
'         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoDocumentoParaAgrupar", "", "NumeroDocumentoGrupo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoActual", "", "SaldoActual"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCobranza", "", "StatusCobranza"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoDetail", "", "TipoDocumentoDetalle"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoCobranza", "", "NumeroCobranza"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCobranza", "", "FechaCobranza", eFT_DATE
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCobrado", "", "MontoCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoOriginal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoCobrado", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSaldoActual", "", ""
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoOriginal", "MontoOriginal", eSF_SUM, "GHTipoReporte", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalSaldoActual", "SaldoActual", eSF_SUM, "GHTipoReporte", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoCobrado", "MontoCobrado", eSF_SUM, "GHTipoReporte", eSR_GROUP, eST_SUB_TOTAL

         If valUnaPaginaPorCliente Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Codigo", ddGrpAll, ddRepeatOnPage, True, ddNPAfter
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Codigo", ddGrpAll, ddRepeatAll, True, ddNPNone
         End If
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHTipoReporte", "TipoReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHDetalle", "NumeroDocumentoGrupo", ddGrpAll, ddRepeatAll, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteHistoricoDeCliente = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteHistoricoDeCliente", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteAnalisisDeVencimientoEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valTotalizaPorMes As Boolean, ByVal valAgruparPorVendedor As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valPrimerVencimiento As Long, ByVal valSegundoVencimiento As Long, ByVal valTercerVencimiento As Long, ByVal valMensajesDeMonedaParaInformes As String, ByVal valMonedaOriginal As Boolean, ByVal usarFiltroZonaCobranza As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valAgruparPorVendedor Then
      If usarFiltroZonaCobranza Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_VENDEDOR_POR_ZONA_DE_COBRANZA)
      Else
          nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_VENDEDOR)
      End If
      Else
      If usarFiltroZonaCobranza Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA)
      Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS)
      End If
      End If
      
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", valMensajesDeMonedaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango1Vto", "1-" & valPrimerVencimiento
               If valMonedaOriginal Then
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", ""
               Else
                  gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", valMensajesDeMonedaParaInformes
               End If
               
         If usarFiltroZonaCobranza Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtZonaDeCobranza", "", "ZonaDeCobranza"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHZonaDeCobranza", "ZonaDeCobranza", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         End If
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango2Vto", valPrimerVencimiento + 1 & "-" & valSegundoVencimiento
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango3Vto", valSegundoVencimiento + 1 & "-" & valTercerVencimiento
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRango4Vto", valTercerVencimiento + 1 & " +"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes", "", "Mes"
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtAno", "", "Ano"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCxC", "", "Numero"
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
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGFNombreVendedor", "", "NombreVendedor"
         If valTotalizaPorMes Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMes", "Mes", ddGrpAll, ddRepeatOnPage, True, ddNPNone
         Else
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHMes", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFMes", False
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeCxC(eCxCRpt_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteAnalisisDeVencimientoEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelComprobanteCxC(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML  As String
   Dim exito As Boolean
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_COMPROBANTES_DE_CUENTA_POR_COBRAR)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoCxC", "", "TipoCxC"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambioABolivares", "", "CambioABolivares"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtVencimiento", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCentroDeCostos", "", "CentroDeCostos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExento", "", "MontoExento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravado", "", "MontoGravado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "MontoIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonado", "", "MontoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRestaPorPagar", "", "RestaPorPagar"
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddODefault, fNombreDelReporteDeCxC(eCxCRpt_COMPROBANTES_DE_CUENTA_POR_COBRAR)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelComprobanteCxC = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelComprobanteCxC", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteVendedorXLienaDeProducto(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valListadoVendedores As Boolean, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valListadoVendedores Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_LISTADO_DE_VENDEDORES)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_VENDEDOR_XLIENA_DE_PRODUCTO)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRIF", gGlobalization.fPromptRIF
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreDeLineaDeProducto", "", "Linea"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeParticiapacion", "", "Porcentaje"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "Monto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRIF", "", "RIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefono", "", "Telefono"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtEmail", "", "Email"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "Codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeCxC(eCxCRpt_VENDEDOR_XLIENA_DE_PRODUCTO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteVendedorXLienaDeProducto = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteVendedorXLienaDeProducto", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
   
Public Function fConfigurarDatosDelReporteClientesPorVendedor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valListadoVendedores As Boolean, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_LISTADO_DE_CLIENTES_POR_VENDEDOR)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRIF", gGlobalization.fPromptRIF
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtClienteNombre", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRIF", "", "NumeroRif"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefono", "", "Telefono"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtNroClientes", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoVendedor", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeCxC(eCxCRpt_LISTADO_DE_CLIENTES_POR_VENDEDOR)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteClientesPorVendedor = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteClientesPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturasSinCxC(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_LISTADO_DE_FACTURAS_SIN_CXC)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeDocumento", "", "TipoDeDocumento"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "TotalFactura"
   
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeCxC(eCxCRpt_LISTADO_DE_FACTURAS_SIN_CXC)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteFacturasSinCxC = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturasSinCxC", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteChequesDevueltos(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valReporteMonedaLocal As Boolean, _
                                                            ByVal valMensajesDeMonedaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_LISTADO_DE_CHEQUES_DEVUELTOS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "MonedaDelReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCheque", "", "StatusStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "NumeroDocumentoOrigen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Cliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtVendedor", "", "Vendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambio", "", "CambioABolivares"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCheque", "", "MontoCheque"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCancelacion", "", "FechaCancelacion", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaAnulacion", "", "FechaAnulacion", eFT_DATE
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCheque", "", "MontoCheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAbonado", "", "TotalAbonado"
         If valReporteMonedaLocal Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", valMensajesDeMonedaParaInformes
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", "Cambio"
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", True
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLeyendaDeTasaDeCambio", ""
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
         End If
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaDelReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHStatus", "StatusStr", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalXMoneda", "MontoCheque", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalXStatus", "MontoCheque", eSF_SUM, "GHStatus", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeCxC(eCxCRpt_LISTADO_DE_CHEQUES_DEVUELTOS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteChequesDevueltos = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteChequesDevueltos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteDeCxCEntreFechasPorSectorDeNegocio(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valEnMonedaOriginal As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCamposDefiniblesFactura As Boolean, ByVal valUsaModuloDeContabilidad As Boolean, ByVal usarContacto As Boolean) As Boolean
   Dim nombreDelFile As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valEnMonedaOriginal Then
      nombreDelFile = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO_MONEDA_ORIGINAL)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO_MONEDA_ORIGINAL)
   Else
      nombreDelFile = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_SECTOR_DE_NEGOCIO)
   End If
   exito = fConfigurarReportesCxCEntreFechasPorSectorDeNegocio(nombreDelFile, refRpt, valSqlDelReporte, valFechaInicial, valFechaFinal, NombreDelReporte, valNombreCompaniaParaInformes, valUsaCamposDefiniblesFactura, valUsaModuloDeContabilidad, usarContacto)
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelReporteDeCxCEntreFechasPorSectorDeNegocio = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraLasSeccionesDelReporteDeCxCEntreFechasPorSectorDeNegocio", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteDeCxCEntreFechasPorZonaDeCobranza(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valEnMonedaOriginal As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCamposDefiniblesFactura As Boolean, ByVal valUsaModuloDeContabilidad As Boolean, ByVal usarContacto As Boolean) As Boolean
   Dim nombreDelFile As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valEnMonedaOriginal Then
      nombreDelFile = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA_MONEDA_ORIGINAL)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA_MONEDA_ORIGINAL)
   Else
      nombreDelFile = fNombreFileDeImpresionDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA)
      NombreDelReporte = fNombreDelReporteDeCxC(eCxCRpt_CUENTAS_X_COBRAR_ENTRE_FECHAS_POR_ZONA_DE_COBRANZA)
   End If
   exito = fConfigurarReportesCxCEntreFechasPorZonaDeCobranza(nombreDelFile, refRpt, valSqlDelReporte, valFechaInicial, valFechaFinal, NombreDelReporte, valNombreCompaniaParaInformes, valUsaCamposDefiniblesFactura, valUsaModuloDeContabilidad, usarContacto)
h_EXIT: On Error GoTo 0
   fConfiguraLasSeccionesDelReporteDeCxCEntreFechasPorZonaDeCobranza = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraLasSeccionesDelReporteDeCxCEntreFechasPorZonaCobranza", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfigurarReportesCxCEntreFechasPorSectorDeNegocio(ByVal valNombreDelFile As String, ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCamposDefiniblesFactura As Boolean, ByVal valUsaModuloDeContabilidad As Boolean, ByVal usarContacto As Boolean)
   Dim exito As Boolean
   On Error GoTo h_ERROR
   If gUtilReports.fLoadLayout(refRpt, valNombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSectorDeNegocio", "", "SectorDeNegocio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDelGrupo", "", "MonedaParaGrupo"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDocumento", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "Monto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioAMonedaLocal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "MontoEnMonedaLocal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInformacionAdicional", "", "InformacionAdicional"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoOriginal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", ""
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSectorDeNegocio", "SectorDeNegocio", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         If (usarContacto) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         End If

         If Not valUsaCamposDefiniblesFactura Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblInformacionAdicional", False
         End If
         If Not valUsaModuloDeContabilidad Then
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, , valNombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarReportesCxCEntreFechasPorSectorDeNegocio = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarReportesCxCEntreFechasPorSectorDeNegocio", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConfigurarReportesCxCEntreFechasPorZonaDeCobranza(ByVal valNombreDelFile As String, ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCamposDefiniblesFactura As Boolean, ByVal valUsaModuloDeContabilidad As Boolean, ByVal usarContacto As Boolean)
   Dim exito As Boolean
   On Error GoTo h_ERROR
   If gUtilReports.fLoadLayout(refRpt, valNombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtZonaDeCobranza", "", "ZonaDeCobranza"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDelGrupo", "", "MonedaParaGrupo"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDocumento", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "Monto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioAMonedaLocal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "MontoEnMonedaLocal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInformacionAdicional", "", "InformacionAdicional"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoOriginal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", ""
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHZonaDeCobranza", "ZonaDeCobranza", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         If (usarContacto) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         End If

         If Not valUsaCamposDefiniblesFactura Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblInformacionAdicional", False
         End If
         If Not valUsaModuloDeContabilidad Then
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, , valNombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarReportesCxCEntreFechasPorZonaDeCobranza = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarReportesCxCEntreFechasPorSectorDeNegocio", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

