VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCotizacionRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCotizacionRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte Cotización"

Private Enum eCotizacionRpt
   eCotRpt_COTIZACIONES_FACTURADAS_NO_FACTURADAS = 0
   eCotRpt_COTIZACIONES_FACTURADAS_NO_FACTURADAS_POR_CIUDAD
   eCotRpt_COTIZACIONES_ENTRE_FECHAS_CONTACTO
   eCotRpt_COTIZACIONES_ENTRE_FECHAS_MONEDA_Y_CAMBIO
   eCotRpt_COTIZACIONES_POR_ARTICULO
   eCotRpt_COTIZACIONES_POR_ARTICULO_RESUMIDO
   eCotRpt_IMPRIMIR_COTIZACION_ADM_INTERNO
   eCotRpt_COTIZACIONES_POR_VENDEDOR
   eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA
   eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA_DETALLADO
   eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA_CON_DESC
   eCotRpt_ARTICULOS_POR_COTIZACION
   eCotRpt_ARTICULOS_POR_COTIZACION_POR_CIUDAD
   eCotRpt_COTIZACIONES_POR_CIUDAD
   eCotRpt_COTIZACION_POR_USUARIO_DETALLADO
   eCotRpt_COTIZACION_POR_CLIENTE
   eCotRpt_COTIZACIONES_POR_CIUDAD_Y_VENDEDOR
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporte(ByVal valIndexDelReporte As eCotizacionRpt) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eCotRpt_COTIZACIONES_FACTURADAS_NO_FACTURADAS: nombreFileXML = "rpxCotizacionesFacturadasNoFacturadas"
      Case eCotRpt_COTIZACIONES_FACTURADAS_NO_FACTURADAS_POR_CIUDAD: nombreFileXML = "rpxCotizacionesFacturadasNoFacturadasPorCiudad"
      Case eCotRpt_COTIZACIONES_ENTRE_FECHAS_CONTACTO: nombreFileXML = "rpxCotizacionesEntreFechasContacto"
      Case eCotRpt_COTIZACIONES_ENTRE_FECHAS_MONEDA_Y_CAMBIO: nombreFileXML = "rpxCotizacionesEntreFechasMonedaYCambio"
      Case eCotRpt_COTIZACIONES_POR_ARTICULO: nombreFileXML = "rpxCotizacionesPorArticulo"
      Case eCotRpt_COTIZACIONES_POR_ARTICULO_RESUMIDO: nombreFileXML = "rpxCotizacionesPorArticuloResumido"
      Case eCotRpt_IMPRIMIR_COTIZACION_ADM_INTERNO: nombreFileXML = "rpxCotizacionIG"
      Case eCotRpt_COTIZACIONES_POR_VENDEDOR: nombreFileXML = "rpxCotizacionesPorVendedor"
      Case eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA: nombreFileXML = "rpxCotizacionesXVendedorXLineaEntreFechas"
      Case eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA_DETALLADO: nombreFileXML = "rpxCotizacionesXVendedorXLineaEntreFechasDetallado"
      Case eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA_CON_DESC: nombreFileXML = "rpxCotizacionesXVendedorXLineaEntreFechasDesc"
      Case eCotRpt_ARTICULOS_POR_COTIZACION: nombreFileXML = "rpxCotizacionesConArticulosEntreFecha"
      Case eCotRpt_ARTICULOS_POR_COTIZACION_POR_CIUDAD: nombreFileXML = "rpxCotizacionesConArticulosEntreFechaXCiudad"
      Case eCotRpt_COTIZACIONES_POR_CIUDAD: nombreFileXML = "rpxCotizacionesPorCiudad"
      Case eCotRpt_COTIZACION_POR_USUARIO_DETALLADO: nombreFileXML = "rpxCotizacionPorUsuarioDetallado"
      Case eCotRpt_COTIZACION_POR_CLIENTE: nombreFileXML = "rpxCotizacionPorCliente"
      Case eCotRpt_COTIZACIONES_POR_CIUDAD_Y_VENDEDOR: nombreFileXML = "rpxCotizacionesPorCiudadYVendedor"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreReporteParaMensajes(valIndexDelReporte))
   Else
      fNombreFileDeImpresionDelReporte = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreReporteParaMensajes(ByVal valIndexDelReporte As eCotizacionRpt) As String
   Dim nombreReporte As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eCotRpt_COTIZACIONES_FACTURADAS_NO_FACTURADAS: nombreReporte = "Cotizaciones Facturadas / No Facturadas"
      Case eCotRpt_COTIZACIONES_FACTURADAS_NO_FACTURADAS_POR_CIUDAD: nombreReporte = "Cotizaciones Facturadas / No Facturadas - Por Ciudad"
      Case eCotRpt_COTIZACIONES_ENTRE_FECHAS_CONTACTO: nombreReporte = "Cotizaciones entre Fechas - Contacto"
      Case eCotRpt_COTIZACIONES_ENTRE_FECHAS_MONEDA_Y_CAMBIO: nombreReporte = "Cotizaciones entre Fechas - Moneda y Cambio"
      Case eCotRpt_COTIZACIONES_POR_ARTICULO: nombreReporte = "Cotizaciones por Artículo"
      Case eCotRpt_COTIZACIONES_POR_ARTICULO_RESUMIDO: nombreReporte = "Cotizaciones por Artículo - Resumido"
      Case eCotRpt_IMPRIMIR_COTIZACION_ADM_INTERNO: nombreReporte = "Cotización - " '& gDefgen.fNombreInformaticaGalac
      Case eCotRpt_COTIZACIONES_POR_VENDEDOR: nombreReporte = "Cotizaciones por Vendedor"
      Case eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA: nombreReporte = "Cotizaciones por Vendedor Y Línea - Resumido"
      Case eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA_DETALLADO: nombreReporte = "Cotizaciones por Vendedor Y Línea - Detallado"
      Case eCotRpt_COTIZACIONES_POR_CIUDAD: nombreReporte = "Cotizaciones por Ciudad"
      Case eCotRpt_COTIZACION_POR_CLIENTE: nombreReporte = "Cotizaciones por Cliente"
      Case eCotRpt_COTIZACIONES_POR_CIUDAD_Y_VENDEDOR: nombreReporte = "Cotizaciones por Ciudad y Vendedor"
      Case Else: nombreReporte = ""
   End Select
h_EXIT:
   fNombreReporteParaMensajes = nombreReporte
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreReporteParaMensajes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraReporteDecotizacionesFacturadasYNoFacturadas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraPromociones As Boolean, ByVal valAgrupadoPorCiudad As Boolean, ByVal valFacturadas As Boolean, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim tituloDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   If valFacturadas Then
      tituloDelReporte = "Cotizaciones Facturadas"
   Else
      tituloDelReporte = "Cotizaciones No Facturadas"
   End If
   If valAgrupadoPorCiudad Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_FACTURADAS_NO_FACTURADAS_POR_CIUDAD)
      tituloDelReporte = tituloDelReporte & " - Por Ciudad"
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_FACTURADAS_NO_FACTURADAS)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DataControl1", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTitulo", tituloDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTitulo", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCiudad", "", "Ciudad"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaDeCotizacion", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCotizacion", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefonos", "", "Telefono"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFax", "", "Fax"
         If GetEsSistemaInfotax Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPromociones", "", "Promocion"
         End If
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPromociones", valMuestraPromociones
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPromociones", valMuestraPromociones
         gUtilMargins.sAsignarMargenesGenerales refRpt
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraReporteDecotizacionesFacturadasYNoFacturadas = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraReporteDecotizacionesFacturadasYNoFacturadas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCotizacionPorArticulo(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String, ByVal valEfecturaCambioABs As Boolean, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_ARTICULO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Cotizaciones por Artículo"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblArticulo", "Artículo:"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPrecioSinIVA", "Precio Sin " & gGlobalization.fPromptIVA
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", valMensajesDeMonedaParaInformes
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCotizacion", "", "NumeroCotizacion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaCotizacion", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "NombreMoneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDoc", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambio", "", "Cambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoRenglon", "", "TotalRenglon"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadRenglon", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioSinIVA", "", "PrecioSinIVA", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCotizado", "", "MontoCotizado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoCotizado", "", "MontoCotizado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglon", "", "TotalRenglon"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         If valEfecturaCambioABs Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "GHmoneda", False
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHmoneda", "NombreMoneda", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambio", False
         End If
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GArticulo", "CodigoArticulo", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteCotizacionPorArticulo = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCotizacionPorArticulo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCotizacionPorArticuloResumido(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String, ByVal valEfectuaraCambioaBs As Boolean, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_ARTICULO_RESUMIDO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Cotizaciones por Artículo - Resumido"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigoArticulo", "Codigo del Artículo"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblDescripcionArticulo", "Descripción del Artículo"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", valMensajesDeMonedaParaInformes
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArt"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cant", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "TotalReng"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "NombreMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCantidad", "", "Cant", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglon", "", "TotalReng"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDoc", "", "MonedaReporte"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         If valEfectuaraCambioaBs Then
'            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GMoneda", False
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GMoneda", "MonedaReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
'            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "Detail", "CodigoArt", ddGrpAll, ddRepeatAll, True, ddNPNone
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GMoneda", "MonedaReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
         End If
         Call refRpt.AddNamedItem("insRptCotizacionConfiguracion", Me)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteCotizacionPorArticuloResumido = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCotizacionPorArticuloResumido", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraReporteCotizacionesEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valVerContacto As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valEfectuaCambio As Boolean, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   If valVerContacto Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_ENTRE_FECHAS_CONTACTO)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_ENTRE_FECHAS_MONEDA_Y_CAMBIO)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoIVA", "Monto " & gGlobalization.fPromptIVA
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioABolivares"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "TotalBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBruto", "", "MontoBruto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "TotalIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "TxtMontoTotal", "", "TotalFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoBruto", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIVA", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoTotal", "", ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDoc", "", "Moneda"
         If valEfectuaCambio Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader1", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPBeforeAfter
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader2", "", ddGrpAll, ddRepeatAll, True, ddNPBeforeAfter
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader1", "Moneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader2", "", ddGrpNone, ddRepeatOnPage, True, ddNPNone
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraReporteCotizacionesEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraReporteCotizacionesEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarImprimirCotizacion(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valImprimirTextoParaPoliza As Boolean, ByVal valImprimirTotales As Boolean, ByVal valMostrarNombreOperador As Boolean, ByVal valMostrarCargoDelOperador As Boolean, ByVal valVerMontosDeLosRenglones As Boolean, ByVal valPorcentajeAlicuota As String, ByVal valUsuarioActualNombreYApellido As String, ByVal valUsuarioActualCargo As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_IMPRIMIR_COTIZACION_ADM_INTERNO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaCotizacion", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDeCotizacion", "", "NumeroCotizacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefonos", "", "ClienteTelf"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFax", "", "ClienteFax"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArt"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRif", "", "ClienteRIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direc"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cant", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConcepto", "", "Concepto"
         If valMostrarNombreOperador Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreOperador", valUsuarioActualNombreYApellido
            If valMostrarCargoDelOperador Then
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCargoDesempeñado", valUsuarioActualCargo
            Else
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCargoDesempeñado", ""
            End If
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreOperador", ""
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCargoDesempeñado", ""
         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAlicuota", gConvert.FormatoNumerico(valPorcentajeAlicuota, False)
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNotaFinal1", "", "DescripcionNota1"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNotaFinal2", "", "DescripcionNota2"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtObservaciones", "", "ObservacionCotizacion"
         If Not valImprimirTextoParaPoliza Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMensaje1", "Indicamos a continuación una cotización de los productos y servicios solicitados."
         End If
         If Not valVerMontosDeLosRenglones Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecioUnitario", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecioUnitario", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotal", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotal", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblDescuento", False
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioUnitario", "", "PrecioSIVA", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDescuento", "", "PorcentajeDesc"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "TotalReng"
         End If
         If Not valImprimirTotales Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSubTotalRenglones", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSubTotalRenglones", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblDescuentoTotal", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPorcentajeDescuentoGeneralizado", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMontoTotalConDescuento", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblIVA", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtAlicuota", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalIVA", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalBs", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalBs", False
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDescuentoGeneralizado", "", ""
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalConDescuento", "", ""
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPreciosNoIncluyenIVA", "Atención: Los Precios detallados en esta cotización NO INCLUYEN IVA."
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubTotalRenglones", "", "TotalReng"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDescuentoGeneralizado", "", "PorDescGeneral"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalConDescuento", "", "DescuentoTotal"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalIVA", "", "TotalIva"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBs", "", "TotalCotizacion"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPreciosNoIncluyenIVA", ""
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarImprimirCotizacion = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarImprimirCotizacion()", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraReporteCotizacionesPorVendedor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valEfecturaCambioABs As Boolean, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_VENDEDOR)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoIVA", "Monto " & gGlobalization.fPromptIVA
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglones", "", "TotalRenglones"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "TotalIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "TxtMontoTotal", "", "TotalCotizacion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIVA", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoTotal", "", ""
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoVendedor", ddGrpAll, ddRepeatNone, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_VENDEDOR)
         If valEfecturaCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "CodigoVendedor", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "MonedaReporte"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaReporte", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "moneda"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "Moneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraReporteCotizacionesPorVendedor = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraReporteCotizacionesPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraReporteCotizacionesPorVendedorYLinea(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valPorVendedor As Boolean, _
                  ByVal valResumido As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valConDesc As Boolean, _
                  ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim valTituloReporte As String
   On Error GoTo h_ERROR
   exito = False
   If valResumido Then
      If valConDesc Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA_CON_DESC)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA)
      End If
      valTituloReporte = fNombreReporteParaMensajes(eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA_DETALLADO)
      valTituloReporte = fNombreReporteParaMensajes(eCotRpt_COTIZACIONES_POR_VENDEDOR_Y_LINEA_DETALLADO)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoIVA", "Monto " & gGlobalization.fPromptIVA
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         If valPorVendedor Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "Codigo"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "Nombre"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoLinea", " "
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "Codigo", GrpKeepTogether.ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHLineaDeProducto", "LineaDeProducto", GrpKeepTogether.ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblVendedor", "Vendedor"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLineaDeProducto", "Línea de Producto"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalesPorLinea", "Totales por Línea de Producto"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalesPorVendedor", "Totales por Vendedor"
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", " "
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "LineaDeProducto"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "Nombre"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoLinea", "", "Codigo"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "LineaDeProducto", GrpKeepTogether.ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHLineaDeProducto", "Codigo", GrpKeepTogether.ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblVendedor", "Línea de Producto"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblLineaDeProducto", "Vendedor"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalesPorLinea", "Totales por Vendedor"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalesPorVendedor", "Totales por Línea de Producto"
         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBruto", "", "MontoBruto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "Descuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "BaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "MontoIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "TxtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMBLineaProducto", "MontoBruto", eSF_SUM, "GHLineaDeProducto", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescLineaProducto", "Descuento", eSF_SUM, "GHLineaDeProducto", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotaltBILineaProducto", "BaseImponible", eSF_SUM, "GHLineaDeProducto", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIVaLineaProducto", "MontoIVA", eSF_SUM, "GHLineaDeProducto", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoTotalLineaProducto", "MontoTotal", eSF_SUM, "GHLineaDeProducto", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMBVendedor", "MontoBruto", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescVendedor", "Descuento", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtBIVendedor", "BaseImponible", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIVAVendedor", "MontoIVA", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoTotalVendedor", "MontoTotal", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         If valResumido Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoBruto", "MontoBruto", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescuento", "Descuento", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponible", "BaseImponible", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIVA", "MontoIVA", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoTotal", "MontoTotal", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCotizacion", "Numero", GrpKeepTogether.ddGrpAll, ddRepeatOnPage, True, ddNPNone
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtArticulo", "", "Descripcion"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblArticulo", "Artículo"
         End If
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMBMoneda", "MontoBruto", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescMoneda", "Descuento", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtBIMoneda", "BaseImponible", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIVAMoneda", "MontoIVA", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoTotalMoneda", "MontoTotal", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "Moneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, PrtOrientation.ddOPortrait
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraReporteCotizacionesPorVendedorYLinea = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraReporteCotizacionesPorVendedorYLinea", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraReporteArticulosPorCotizacion(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object, ByVal valAgrupaXCiudad As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim tituloDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   If valAgrupaXCiudad Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_ARTICULOS_POR_COTIZACION_POR_CIUDAD)
      tituloDelReporte = "Artículos Por Cotización Y Ciudad entre Fecha"
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_ARTICULOS_POR_COTIZACION)
      tituloDelReporte = "Artículos Por Cotización entre Fecha"
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", tituloDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigoArticulo", "Cod. Artículo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHTitulos", "", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCotizacion", "Numero", ddGrpNone, ddRepeatNone, False, ddNPNone
         
         If valAgrupaXCiudad Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCiudad", "", "Ciudad"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCiudad", "Ciudad", ddGrpAll, ddRepeatAll, True, ddNPBeforeAfter
         End If

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, tituloDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraReporteArticulosPorCotizacion = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraReporteArticulosPorCotizacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraReporteCotizacionesPorCiudad(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valEfecturaCambioABs As Boolean, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_CIUDAD)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoIVA", "Monto " & gGlobalization.fPromptIVA
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCiudad", "", "Ciudad"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccionDespacho", "", "Direccion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDescuento", "", "PorcentajeDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglones", "", "TotalBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "TotalIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "TxtMontoTotal", "", "TotalCotizacion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDescuento", "", "Descuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBruto", "", "MontoBruto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIVA", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoTotal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDescuento", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoBruto", "", ""
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponibleCiudad", "TotalBaseImponible", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIVACiudad", "TotalIVA", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoTotalCiudad", "TotalCotizacion", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescuentoCiudad", "Descuento", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoBrutoCiudad", "MontoBruto", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         If valEfecturaCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCiudad", "Ciudad", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCiudad", "Ciudad", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "moneda"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "Moneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponibleMoneda", "TotalBaseImponible", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIVAMoneda", "TotalIVA", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoTotalMoneda", "TotalCotizacion", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescuentoMoneda", "Descuento", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHMoneda", True
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GroupFooter1", True, 450
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoBrutoMoneda", "MontoBruto", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_CIUDAD)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraReporteCotizacionesPorCiudad = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraReporteCotizacionesPorCiudad", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCotizacionPorUsuario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                  ByVal MonedaDeLosReportes As String, ByVal valNombreCompaniaParaInformes As String, _
                  ByRef gGlobalization As Object, ByVal valImprimirRenglones As Boolean, _
                  ByVal valUsaPrecioSinIvaSegunTipoDocumento As Boolean, ByVal valTipoDeMensajeDeCambio As String, _
                  ByVal valTituloReporte As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False

   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACION_POR_USUARIO_DETALLADO)

   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFechaFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigoArticulo", "Código de Artículo"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFechaFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtUsuario", "", "Usuario"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDeGrupo", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkTotalizarPorMoneda", True
         If MonedaDeLosReportes <> gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal) Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMensajeDelCambioDeLaMoneda", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", Trim(gEnumReport.getMensajesDeMonedaParaInformes(valTipoDeMensajeDeCambio))
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "GHMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "GFMoneda", False
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMonedaDeGrupo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMensajeDelCambioDeLaMoneda", False
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", ""
         End If
         If valImprimirRenglones Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "Monto"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDcto", "", "Descuento"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIva", "", "Iva"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRenglon", "", "Total"
            If valUsaPrecioSinIvaSegunTipoDocumento Then
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioSinIva", False, ddTXRight, formatoNDecimales
            Else
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioConIva", False, ddTXRight, formatoNDecimales
            End If
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoArticulo", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHFactura", True, 650
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioConIva", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         End If
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaReporte", ddGrpAll, ddRepeatOnPage, True, ddNPBeforeAfter
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHUsuario", "Usuario", ddGrpNone, ddRepeatOnPage, True, ddNPBeforeAfter
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHFactura", "Numero", ddGrpNone, ddRepeatOnPage, True, ddNPNone
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMonto", "Monto", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDcto", "Descuento", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIva", "Iva", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalRenglon", "Total", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "Total", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalUsuario", "Total", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL

         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT:   On Error GoTo 0
   fConfigurarDatosDelReporteCotizacionPorUsuario = exito
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCotizacionPorUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCotizacionPorCliente(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                ByVal valSqlDelReporte As String, ByVal valImprimirRenglones As Boolean, _
                                                 ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                   ByVal valEfecturaCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, _
                                                    ByRef refInsUltimaTasaDeCambio As Object, ByRef gGlobalization As Object)
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim valTituloReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False

   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACION_POR_CLIENTE)
   valTituloReporte = fNombreReporteParaMensajes(eCotRpt_COTIZACION_POR_CLIENTE)

   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblProducto", "Cód. Artículo"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloReporte
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtProducto", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "MonedaReporte"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "PorcDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuentoGeneral", "", "PorcentajeDescuentoGeneral"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBruto", "", "Monto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "Descuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "BaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIva", "", "Iva"
          gUtilReports.sDefaultConfigurationForLabels refRpt, "lblIVA", gGlobalization.fPromptIVA
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "Total"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "lblCambioOriginalODelDia", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia), ""
         If valEfecturaCambioABs Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHMoneda", False
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCambioOriginalODelDia", False
         End If
         If valImprimirRenglones = False Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblProducto", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCant", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblDescuento", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalPorRenglon", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", False
         End If
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoBrutoCotizacion", "Monto", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtDescuentoCotizacion", "Descuento", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtBICotizacion", "BaseImponible", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtIvaCotizacion", "Iva", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCotizacion", "Total", eSF_SUM, "GHCotizacion", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoBrutoTotal", "Monto", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtDescuentoTotal", "Descuento", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtBITotal", "BaseImponible", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txIvaTotal", "Iva", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoTotal", "Total", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCotizacion", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "NombreCliente", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
         fConfigurarDatosDelReporteCotizacionPorCliente = exito
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarDatosDelReporteCotizacionPorCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraReporteCotizacionesPorCiudadYVendedor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valEfecturaCambioABs As Boolean, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_CIUDAD_Y_VENDEDOR)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoIVA", "Monto " & gGlobalization.fPromptIVA
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCiudad", "", "Ciudad"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDescuento", "", "PorcentajeDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglones", "", "TotalBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "TotalIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "TxtMontoTotal", "", "TotalCotizacion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDescuento", "", "Descuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoBruto", "", "MontoBruto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIVA", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoTotal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDescuento", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoBruto", "", ""
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponibleCiudad", "TotalBaseImponible", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIVACiudad", "TotalIVA", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoTotalCiudad", "TotalCotizacion", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescuentoCiudad", "Descuento", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoBrutoCiudad", "MontoBruto", eSF_SUM, "GHCiudad", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponibleVendedor", "TotalBaseImponible", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIVAVendedor", "TotalIVA", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoTotalVendedor", "TotalCotizacion", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescuentoVendedor", "Descuento", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoBrutoVendedor", "MontoBruto", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         
         If valEfecturaCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "NombreVendedor", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCiudad", "Ciudad", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         Else
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCiudad", "Ciudad", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "moneda"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "Moneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponibleMoneda", "TotalBaseImponible", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIVAMoneda", "TotalIVA", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoTotalMoneda", "TotalCotizacion", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDescuentoMoneda", "Descuento", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHMoneda", True
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GroupFooter1", True, 450
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoBrutoMoneda", "MontoBruto", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreFileDeImpresionDelReporte(eCotRpt_COTIZACIONES_POR_CIUDAD)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraReporteCotizacionesPorCiudadYVendedor = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraReporteCotizacionesPorCiudad", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
